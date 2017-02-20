Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Recaudacion_doctos_deudor
    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim rc As New FuncionesGenerales.FComunes
    Dim pagos As New ClsSession.SesionPagos
    Dim Posicion As Integer = 0
    Dim CMC As New ClaseComercial
    Dim PGO As New ClasePagos
    Dim OP As New ClaseOperaciones


#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1
            hf_deu.Value = Request.QueryString("rut")
            CargaGrillaDoctos()
            hf_tmc.Value = CMC.TasaMaximaConvencionalDevuelve.tmc_val
            pagos.Pagador = Request.QueryString("tipo_persona")
        End If
        Okbutton.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$LB_DOC_DDR');")
    End Sub

    Protected Sub txt_nota_cre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim txt_tot As Double
        Dim txt_not_cre As TextBox
        Dim txt_tot_pag As TextBox

        If pagos.Pagador = "D" Then
            txt_tot = Me.gr_documentos.Rows(hf_posicion.Value).Cells(9).Text
        Else
            txt_tot = Me.gr_documentos.Rows(hf_posicion.Value).Cells(8).Text
        End If
        txt_not_cre = Me.gr_documentos.Rows(hf_posicion.Value).FindControl("Txt_nota_cre")
        txt_tot_pag = Me.gr_documentos.Rows(hf_posicion.Value).FindControl("Txt_Mto_Pag")

        txt_tot_pag.Text = txt_tot - txt_not_cre.Text
        CalculaTotalPago()
        Coll_DOC.Item(Val(hf_posicion.Value + 1)).nota_cred = txt_not_cre.Text
    End Sub

    Protected Sub txt_mto_pag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        CalculaTotalPago()
    End Sub

    Protected Sub txt_tmc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim txt_mto As TextBox

        Dim txt As TextBox
        txt_mto = Me.gr_documentos.Rows(Me.hf_posicion.Value).FindControl("Txt_Mto_Pag")
        txt = Me.gr_documentos.Rows(Me.hf_posicion.Value).FindControl("txt_tmc")
        If txt.Text > hf_tmc.Value Then

            Msj("Atención", "Tasa no puede ser Superior a la Maxima convencional", 2)
            Exit Sub
        Else

            Coll_DOC.Item(Val(hf_posicion.Value) + 1).tasa = txt.Text
            CalculaInteres(Val(hf_posicion.Value))
            txt_mto.Focus()

        End If
    End Sub

    Protected Sub Ch_Doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Mto_A_Pagar As TextBox
        Dim CB As CheckBox
        Dim not_cre As TextBox
        Dim tasa As TextBox


        CB = CType(gr_documentos.Rows(hf_posicion.Value).FindControl("Ch_Doc"), CheckBox)

        'Validamos que tenga factor de cambio del dia de la cuenta, sino no lo deja agregar
        If Coll_DOC.Item(hf_posicion.Value + 1).ope_fac_cam <= 0 And CB.Checked Then
            '    Msj("Cuentas Por Cobrar", "No se puede agregar esta CXC por no tener factor de cambio del dia de la cuenta", TipoDeMensaje._Exclamacion)
            CB.Checked = False
            Exit Sub
        End If

        Mto_A_Pagar = CType(gr_documentos.Rows(hf_posicion.Value).FindControl("Txt_Mto_Pag"), TextBox)
        not_cre = CType(gr_documentos.Rows(hf_posicion.Value).FindControl("Txt_Nota_Cre"), TextBox)
        tasa = CType(gr_documentos.Rows(hf_posicion.Value).FindControl("txt_tmc"), TextBox)
        ' TD = CType(gr_documentos.Rows(hf_posicion.Value).FindControl("CB_PD"), CheckBox)

        If CB.Checked Then

            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False
            ' TD.Checked = True
            CalculaInteres(hf_posicion.Value)

            '* * * * * * * *  Valida si existe otro pago no este procesado  * * * * * * *
            Dim Coll As New Collection

            Coll = PGO.PagosValidaEstados(gr_documentos.Rows(hf_posicion.Value).Cells(5).Text, "PDIRECTO-INGR")

            If Coll.Count > 0 Then
                GridPagos.DataSource = Coll
                GridPagos.DataBind()
                GridPagos.Columns(1).Visible = False
                ModalPopupExtender1.Show()
            End If



            '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        Else

            Mto_A_Pagar.Text = 0
            Mto_A_Pagar.CssClass = "clsDisabled"
            Mto_A_Pagar.ReadOnly = True
            not_cre.Text = 0
            not_cre.CssClass = "clsDisabled"
            not_cre.ReadOnly = True
            tasa.Text = 0
            tasa.CssClass = "clsDisabled"
            tasa.ReadOnly = True

            '  TD.Checked = False
            gr_documentos.Rows(hf_posicion.Value).Cells(10).Text = 0
            CalculaTotalPago()

        End If

        Mto_A_Pagar.Focus()
    End Sub

    Protected Sub gr_documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_documentos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim MtoPagar As String
            MtoPagar = CType(e.Row.FindControl("Txt_Mto_Pag"), TextBox).ClientID
            CType(e.Row.FindControl("Txt_Mto_Pag"), TextBox).Attributes.Add("Style", "TEXT-ALIGN: right")
            CType(e.Row.FindControl("Txt_nota_cre"), TextBox).Attributes.Add("Style", "TEXT-ALIGN: right")
            CType(e.Row.FindControl("txt_tmc"), TextBox).Attributes.Add("Style", "TEXT-ALIGN: right")

            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaDocto(" & Posicion & ");")
            Posicion = Posicion + 1
        End If
    End Sub

    Protected Sub IB_SeleccionDoctos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_SeleccionDoctos.Click
        For I = 0 To gr_documentos.Rows.Count - 1

            Dim Check As CheckBox

            Check = gr_documentos.Rows(I).FindControl("Ch_doc")

            Check.Checked = True
            CalculaInteres(I)

        Next
    End Sub

    Protected Sub btn_aceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_aceptar.Click

        Try

            Dim FC As New FuncionesGenerales.FComunes


            Coll_Doctos_Seleccionados = New Collection

            CargaCollection()

            'Ordena los documentos por fecha de vencimiento
            FC.SortCollection(Coll_Doctos_Seleccionados, "doc_fev_rea", True)

            If Coll_Doctos_Seleccionados.Count > 0 Then
                Msj("Seleccion de Documentos", "Doctos se asociaron exitosamente, cierre la pantalla", TipoDeMensaje._Informacion)
            Else
                Msj("Seleccion de Documentos", "Debes Seleccionar al menos un Documento", TipoDeMensaje._Exclamacion)
            End If


        Catch ex As Exception
            'Msj("Error", ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Sub Msj(ByVal Caption As String, ByVal Mensaje As String, ByVal Tipo As TipoDeMensaje)

        Me.TextBox1.Text = Mensaje
        Me.Lbl_error.Text = Caption

        Select Case Tipo

            Case TipoDeMensaje._Error
                btn_acepta.Visible = False
                imgerror.Visible = True
                imgexclam.Visible = False
                imginfo.Visible = False
                Me.Img_pregunta.Visible = False
                Me.Okbutton.Visible = True

            Case TipoDeMensaje._Exclamacion
                btn_acepta.Visible = True
                imgerror.Visible = False
                Me.ModalPopupExtender.OkControlID = "btn_acepta"
                imgexclam.Visible = True
                imginfo.Visible = False
                Me.Img_pregunta.Visible = False
                Me.Okbutton.Visible = False

            Case TipoDeMensaje._Informacion
                btn_acepta.Visible = False
                imgerror.Visible = False
                imgexclam.Visible = False
                imginfo.Visible = True
                Me.Img_pregunta.Visible = False
                Me.Okbutton.Visible = True

            Case TipoDeMensaje._Confirmacion
                btn_acepta.Visible = True
                imgerror.Visible = False
                imgexclam.Visible = False
                imginfo.Visible = False
                Img_pregunta.Visible = True
                canc.Visible = True
                Me.Okbutton.Visible = False

        End Select

        Me.ModalPopupExtender.Show()

    End Sub

    Private Sub CalculaTotalPago()

        Try


            txt_tot_pag.Text = 0

            For I = 0 To gr_documentos.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox

                Mto_A_Pagar = CType(gr_documentos.Rows(I).FindControl("Txt_Mto_Pag"), TextBox)

                txt_tot_pag.Text = Format(CDbl(txt_tot_pag.Text) + (CDbl(Mto_A_Pagar.Text) * Coll_DOC.Item(hf_posicion.Value + 1).ope_fac_cam), fmt.FCMSD)
                Coll_DOC.Item(I + 1).MontoPagar = Mto_A_Pagar.Text

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalculaInteres(ByVal PosicionGV As Long)

        Try

            Dim Formulas As New FormulasGenerales
            Dim MtoAPagar As Double
            Dim Interes As Double
            Dim Saldo As Double
            Dim FechaSimula As String
            Dim FechaUltPago As String
            Dim FechaVctoRea As String
            Dim CantidadDias As String
            Dim Lineal As String
            Dim TasaAnuMen As String
            Dim TasaRenova As Decimal
            Dim MtoAnticip As Double
            Dim FecVctoOri As String
            Dim NroRenovac As Integer
            Dim TasaNegocio As Decimal
            Dim Mto_A_Pagar As TextBox
            Dim not_cre As TextBox
            Dim tasa As TextBox
            Dim TD As CheckBox

            If pagos.Pagador = "C" Then

                tasa = CType(gr_documentos.Rows(PosicionGV).FindControl("txt_tmc"), TextBox)
                tasa.CssClass = "clsMandatorio"
                tasa.ReadOnly = False
            Else


                not_cre = CType(gr_documentos.Rows(PosicionGV).FindControl("Txt_nota_cre"), TextBox)
                not_cre.CssClass = "clsMandatorio"
                not_cre.ReadOnly = False

            End If

            Mto_A_Pagar = CType(gr_documentos.Rows(PosicionGV).FindControl("Txt_Mto_Pag"), TextBox)
            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False




            TD = CType(gr_documentos.Rows(PosicionGV).FindControl("Ch_Doc"), CheckBox)
            TD.Checked = True

            'Buscamos el documento para traer todas sus relaciones
            Dim DOC As doc_cls = OP.DocumentoOtorgagoDevuelvePorId(Coll_DOC.Item(PosicionGV + 1).id_doc)

            'Rescatamos el saldo del documento
            If pagos.Pagador = "C" Then
                Saldo = CDbl(gr_documentos.Rows(PosicionGV).Cells(8).Text)
            Else
                Saldo = CDbl(gr_documentos.Rows(PosicionGV).Cells(9).Text)
            End If


            'Monto a pagar por defecto toma el saldo completo
            If pagos.Pagador = "C" Then
                MtoAPagar = CDbl(gr_documentos.Rows(PosicionGV).Cells(8).Text)
            Else
                MtoAPagar = CDbl(gr_documentos.Rows(PosicionGV).Cells(9).Text)
            End If

            'validamos si la fecha de ultimo pago viene nula
            If IsNothing(DOC.opo_cls.opo_ful_pgo) Then
                FechaUltPago = "01/01/1900"
            Else
                FechaUltPago = DOC.opo_cls.opo_ful_pgo
            End If

            FechaSimula = DOC.opo_cls.ope_cls.ope_fec_sim
            FechaVctoRea = DOC.dsi_cls.dsi_fev_rea
            CantidadDias = DOC.dsi_cls.dsi_ctd_dia

            If IsNothing(DOC.opo_cls.ope_cls.ope_lnl) Then
                Lineal = "N"
            Else
                Lineal = DOC.opo_cls.ope_cls.ope_lnl
            End If

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa) Then
                TasaAnuMen = 0
            Else
                TasaAnuMen = DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa
            End If

            If IsNothing(DOC.doc_tas_ren) Then
                TasaRenova = 0
            Else
                TasaRenova = DOC.doc_tas_ren
            End If

            If IsNothing(DOC.dsi_cls.dsi_fev) Then
                FecVctoOri = "01/01/1900"
            Else
                FecVctoOri = DOC.dsi_cls.dsi_fev
            End If

            If IsNothing(DOC.doc_num_ren) Then
                NroRenovac = 0
            Else
                NroRenovac = DOC.doc_num_ren
            End If

            MtoAnticip = DOC.dsi_cls.dsi_mto_ant

            TasaNegocio = DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas + _
                          DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead + _
                          DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr


            Dim CL As New ClaseClientes

            Interes = Formulas.RetornaCalculoInteres(Date.Now, _
                                                       0, _
                                                     Coll_DOC.Item(hf_posicion.Value + 1).tasa, _
                                                     MtoAPagar, _
                                                     FechaSimula, _
                                                     FechaVctoRea, _
                                                     CantidadDias, _
                                                     Saldo, _
                                                     FechaUltPago, _
                                                     pagos.DiasDevolverInteres, _
                                                     Lineal, _
                                                     TasaAnuMen, _
                                                     TasaNegocio, _
                                                     TasaRenova, _
                                                     MtoAnticip, _
                                                     FecVctoOri, _
                                                     NroRenovac, _
                                                     DOC.id_doc, _
                                                     DOC.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas)

            'Coll_DOC.Item(PosicionGV + 1).Interes = Interes



            If pagos.Pagador = "C" Then



                gr_documentos.Rows(PosicionGV).Cells(11).Text = Format(Interes, rc.DevuelveFormatoMoneda(DOC.opo_cls.ope_cls.opn_cls.id_P_0023))
                Mto_A_Pagar.Text = Format(Interes + Saldo, rc.DevuelveFormatoMoneda(DOC.opo_cls.ope_cls.opn_cls.id_P_0023))

            Else

                gr_documentos.Rows(PosicionGV).Cells(11).Text = Format(Interes, rc.DevuelveFormatoMoneda(DOC.opo_cls.ope_cls.opn_cls.id_P_0023))
                Mto_A_Pagar.Text = Format(Interes + Saldo, rc.DevuelveFormatoMoneda(DOC.opo_cls.ope_cls.opn_cls.id_P_0023))

            End If




            CalculaTotalPago()


        Catch ex As Exception

        End Try

    End Sub

    Private Sub CargaGrillaDoctos()

        Try

            If IsNothing(Coll_Doctos_Seleccionados) Then
                Coll_Doctos_Seleccionados = New Collection
            End If


            Dim Rut_Cli_Desde As String
            Dim Rut_Cli_Hasta As String

            Dim Rut_Deu_Desde As String
            Dim Rut_Deu_Hasta As String

            Dim TipoDoc_Desde As Integer
            Dim TipoDoc_Hasta As Integer

            Dim NroOtor_Desde As Long
            Dim NroOtor_Hasta As Long

            Dim NroDoct_Desde As String
            Dim NroDoct_Hasta As String

            'Dim EstCobr_Desde As Integer
            'Dim EstCobr_Hasta As Integer

            Dim Fec_Vto_Desde As DateTime
            Dim Fec_Vto_Hasta As DateTime
            Dim Pagos As New ClsSession.SesionPagos

            If Modulo = "Operacion" Then
                Rut_Cli_Desde = Pagos.RutCliente
                Rut_Cli_Hasta = Pagos.RutCliente
                Rut_Deu_Desde = "000000000000"
                Rut_Deu_Hasta = "9999999999999"
            Else

                Dim txt_not_cre As TextBox

                txt_not_cre = Me.gr_documentos.FindControl("txt_nota_cre")

                Rut_Cli_Desde = "000000000000"
                Rut_Cli_Hasta = "9999999999999"

                'txt_not_cre.ReadOnly = False
                'txt_not_cre.CssClass = "clsMandatorio"



            End If

            'Criterio de Busqueda

            'Si busca por Deudor
            If hf_deu.Value = "" Then

                Rut_Deu_Desde = "000000000000"
                Rut_Deu_Hasta = "9999999999999"
            Else
                Rut_Deu_Desde = hf_deu.Value
                Rut_Deu_Hasta = hf_deu.Value
            End If


            'Tipo Docto
            TipoDoc_Desde = 0
            TipoDoc_Hasta = 999

            'Nro Otorgamiento
            NroOtor_Desde = 0
            NroOtor_Hasta = 999999999

            'Nro Documento
            NroDoct_Desde = "0"
            NroDoct_Hasta = "Z"

            'Fecha Vcto
            Fec_Vto_Desde = "01/01/1900"
            Fec_Vto_Hasta = "31/12/2100" 'Date.Now.ToShortDateString

            Coll_DOC = New Collection

            Dim Estado1, Estado2, Estado3, Estado4, Estado5, Estado6, Estado7, Estado8, Estado9, Estado10, Estado11, Estado12 As Integer

            Estado1 = 1
            Estado2 = 2
            Estado3 = 4
            Estado4 = 9
            Estado5 = 11
            Estado6 = 12
            Estado7 = 1
            Estado8 = 1
            Estado9 = 1
            Estado10 = 1
            Estado11 = 1
            Estado12 = 1

            Dim Coll_Obj = PGO.DocumentosOtorgagosPagos_RetornaDoctos(Rut_Cli_Desde, Rut_Cli_Hasta, _
                                                                Rut_Deu_Desde, Rut_Deu_Hasta, _
                                                                NroOtor_Desde, NroOtor_Hasta, _
                                                                TipoDoc_Desde, TipoDoc_Hasta, _
                                                                NroDoct_Desde, NroDoct_Hasta, _
                                                                0, 9999, _
                                                                Fec_Vto_Desde, Fec_Vto_Hasta, _
                                                                Estado1, _
                                                                Estado2, _
                                                                Estado3, _
                                                                Estado4, _
                                                                Estado5, _
                                                                Estado6, _
                                                                Estado7, _
                                                                Estado8, _
                                                                Estado9, _
                                                                Estado10, _
                                                                Estado11, _
                                                                Estado12)

            For Each Obj In Coll_Obj
                Coll_DOC.Add(Obj)
            Next

            For i = 1 To Coll_DOC.Count
                Coll_DOC.Item(i).tasa = CMC.TasaRetorna(2, Coll_DOC.Item(i).cli_idc, Coll_DOC.Item(i).id_opo)
            Next

            Me.gr_documentos.DataSource = Coll_DOC
            gr_documentos.DataBind()

            If gr_documentos.Rows.Count > 0 Then
                AsignaSource()
            End If

            Dim Coll_Ing As Collection

            Coll_Ing = cg.IngresosDevuelve(Pagos.RutCliente, 0, 2, 2, "01/01/1900", "01/01/3000")

            For Pos_Ing = 1 To Coll_Ing.Count

                For Pos_Doc = 1 To Coll_DOC.Count

                    If Coll_Ing.Item(Pos_Ing).id_doc = Coll_DOC.Item(Pos_Doc).id_doc And Coll_Ing.Item(Pos_Ing).ing_pro = "N" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                        Me.gr_documentos.Rows(Pos_Doc - 1).BackColor = col
                        Exit For
                    End If

                Next

            Next

            If Coll_Doctos_Seleccionados.Count > 0 Then

                For I = 1 To Coll_Doctos_Seleccionados.Count

                    For X = 1 To Coll_DOC.Count

                        If Coll_DOC.Item(X).id_doc = Coll_Doctos_Seleccionados.Item(I).id_doc Then

                            Dim Mto_A_Pagar As TextBox

                            CType(gr_documentos.Rows(X - 1).FindControl("CB_Seleccionar"), CheckBox).Checked = True
                            Me.gr_documentos.Rows(X - 1).Cells(10).Text = Coll_Doctos_Seleccionados.Item(I).Interes
                            Mto_A_Pagar = gr_documentos.Rows(X - 1).FindControl("Txt_MtoPagar")

                            If Coll_Doctos_Seleccionados.Item(I).id_p_0023 = 1 Then
                                Mto_A_Pagar.Text = Format(Coll_Doctos_Seleccionados.Item(I).MontoPagar + Coll_Doctos_Seleccionados.Item(I).Interes, fmt.FCMSD)
                            Else
                                Mto_A_Pagar.Text = Format(Coll_Doctos_Seleccionados.Item(I).MontoPagar + Coll_Doctos_Seleccionados.Item(I).Interes, fmt.FCMCD)
                            End If

                            Mto_A_Pagar.CssClass = "clsMandatorio"

                            Exit For

                        End If

                    Next
                Next

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub AsignaSource()

        Dim Formato As String = ""



        For I = 0 To gr_documentos.Rows.Count - 1

            Try

                Select Case Coll_DOC.Item(I + 1).id_p_0023
                    Case 1 : Formato = fmt.FCMSD
                    Case 2, 4 : Formato = fmt.FCMCD4
                    Case 3 : Formato = fmt.FCMCD
                End Select


                gr_documentos.Rows(I).Cells(1).Text = rc.FormatoMiles(CInt(gr_documentos.Rows(I).Cells(1).Text)) & "-" & rc.Vrut(CLng(gr_documentos.Rows(I).Cells(1).Text))

                '*****************************************************************************************
                'Se modifica , pues estaba desplegando mal los montos 
                'Gr_Documentos.Rows(I).Cells(8).Text = Format(Val(Gr_Documentos.Rows(I).Cells(8).Text), Formato)
                'Gr_Documentos.Rows(I).Cells(9).Text = Format(Val(Gr_Documentos.Rows(I).Cells(9).Text), Formato)

                ' Fin Modificacion 
                '*****************************************************************************************
                If gr_documentos.Rows(I).Cells(8).Text = "" Then
                    gr_documentos.Rows(I).Cells(8).Text = 0
                Else
                    gr_documentos.Rows(I).Cells(8).Text = Format(CDbl(gr_documentos.Rows(I).Cells(8).Text), Formato)
                End If

                If gr_documentos.Rows(I).Cells(9).Text = "" Then
                    gr_documentos.Rows(I).Cells(9).Text = 0
                Else
                    gr_documentos.Rows(I).Cells(9).Text = Format(CDbl(gr_documentos.Rows(I).Cells(9).Text), Formato)
                End If

                CType(gr_documentos.Rows(I).FindControl("txt_mto_pag"), TextBox).Text = 0

                'Gr_Documentos.Rows(I).Cells(10).Text = 0
                gr_documentos.Rows(I).Cells(11).Text = Format(CDbl(gr_documentos.Rows(I).Cells(11).Text), Formato)
                gr_documentos.Rows(I).Cells(13).Text = Format(CDbl(gr_documentos.Rows(I).Cells(13).Text), Formato)
                gr_documentos.Rows(I).Cells(15).Text = Format(CDbl(gr_documentos.Rows(I).Cells(15).Text), Formato)
                gr_documentos.Rows(I).Cells(16).Text = Format(CDbl(gr_documentos.Rows(I).Cells(16).Text), Formato)
            Catch ex As Exception

            End Try

        Next

    End Sub

    Private Sub CargaCollection()

        '
        Try


            For PosicionGV = 0 To gr_documentos.Rows.Count - 1

                If CType(gr_documentos.Rows(PosicionGV).FindControl("Ch_Doc"), CheckBox).Checked Then

                    'Le pasamos nuevamente el monto en caso de que este haya variado
                    Coll_DOC.Item(PosicionGV + 1).MontoPagar = CType(gr_documentos.Rows(PosicionGV).FindControl("Txt_Mto_Pag"), TextBox).Text - CDbl(gr_documentos.Rows(PosicionGV).Cells(11).Text)
                    Coll_DOC.Item(PosicionGV + 1).Interes = CDbl(gr_documentos.Rows(PosicionGV).Cells(11).Text)

                    If pagos.Pagador <> "C" Then
                        Coll_DOC.Item(PosicionGV + 1).PagaDeudor = "S"
                    Else
                        Coll_DOC.Item(PosicionGV + 1).PagaDeudor = "N"
                    End If

                    'Cargo los Documentos seleccionados para que sean mostrados en la pagina principal
                    Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(PosicionGV + 1))

                End If

            Next



        Catch ex As Exception

            Msj("Error", ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

End Class
