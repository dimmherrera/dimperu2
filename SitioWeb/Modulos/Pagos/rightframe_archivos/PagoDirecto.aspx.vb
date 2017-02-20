Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Pagos_rightframe_archivos_PagoDirecto
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pago Directo"
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Pagos As New ClsSession.SesionPagos
    Dim Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
    Dim sesion As New ClsSession.ClsSession
    Dim CMC As New ClaseComercial
    Dim PGO As New ClasePagos
    Dim OP As New ClaseOperaciones
    Dim CL As New ClaseClientes

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Txt_FecSimulacion.CssClass = "clsDisabled"
            Me.Txt_FecSimulacion.Text = Date.Now.ToShortDateString
            Dim COLL_DIARIOS As New Collection

            COLL_DIARIOS = CMC.DatosDiariosDevuelve(CDate(Me.Txt_FecSimulacion.Text))

            If IsNothing(COLL_DIARIOS) = False Then
                Me.vdolar.Text = Format(CDbl(COLL_DIARIOS.Item(2)), Fmt.FCMCD)
                Me.vtmc.Text = COLL_DIARIOS.Item(3)
            End If

            If Not IsPostBack Then

                Response.Expires = -1

                CargaDrop()
                CargaDatos()

                Coll_DPO = New Collection
                Coll_Doctos_Seleccionados = New Collection
                Coll_Cxc_Seleccionados = New Collection
                Coll_Ing_Sec = New Collection

                BloqueaFormaDePago(True)

            Else
                If SW = 1 Then
                    Coll_Doctos_Seleccionados = New Collection
                    Coll_Cxc_Seleccionados = New Collection
                End If

                IB_Guardar.Enabled = True
                actualiza_totales()
            End If

            SW = 3
            IB_Doctos.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx?rut_deu=" & IIf(Me.Txt_Rut_Deu.Text <> "", CLng(Val(Me.Txt_Rut_Deu.Text)), 0) & " ', window, 'scroll:no;status:off;dialogWidth:1250;dialogHeight:650px;dialogLeft:10px;dialogTop:50px');")

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?pago=1','PopUpCliente',620,410,200,150);")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeudor.aspx?pago=1','PopUpDeudor',540,430,200,150);")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub actualiza_totales()

        If Coll_Doctos_Seleccionados.Count = 0 And Coll_Cxc_Seleccionados.Count = 0 Then
            If Confirma_seleccion = False Then
                Coll_Doctos_Seleccionados = New Collection
                Coll_Cxc_Seleccionados = New Collection
                Txt_Tot_Int_Dev.Text = 0
            End If
        End If

        'Variables Totales para Documentos
        Dim TotalCancelar As Double
        Dim TotalInteres As Double
        Dim TotalDoctos As Double
        Dim TotalNotaCre As Double
        Dim totalSeleccionado As Double

        'Variables Totales para Cuentas Por Cobrar
        Dim TotalInteresCxc As Double
        Dim TotalCancelarCxc As Double
        Dim TotalCxc As Double

        Dim doc_sel As Boolean = False
        Dim cxc_sel As Boolean = False

        Txt_Tot_Int_Dev.Text = 0
        Txt_Tot_Int.Text = 0

        cxc_sel = False
        doc_sel = False

        'Si seleciono al menos un documento 
        If Not IsNothing(Coll_Doctos_Seleccionados) Then
            'And Coll_Doctos_Seleccionados.Count > 0
            Gr_Documentos.DataSource = Coll_Doctos_Seleccionados
            Gr_Documentos.DataBind()

            Dim Monto As Double
            Dim Interes As Double

            DP_Moneda.ClearSelection()

            For I = 1 To Coll_Doctos_Seleccionados.Count
                doc_sel = True
                Monto = Coll_Doctos_Seleccionados.Item(I).MontoPagar ' * Coll_Doctos_Seleccionados.Item(I).ope_fac_cam)
                Interes = Coll_Doctos_Seleccionados.Item(I).Interes '* Coll_Doctos_Seleccionados.Item(I).ope_fac_cam)
                TotalNotaCre = TotalNotaCre + Coll_Doctos_Seleccionados.Item(I).nota_cred

                TotalInteres = TotalInteres + Interes

                If Pagos.Pagador = "C" Then
                    TotalDoctos = TotalDoctos + Monto + (Interes * -1)
                Else
                    TotalDoctos = TotalDoctos + Monto
                End If

                totalSeleccionado = totalSeleccionado + Monto

                If Pagos.Pagador = "D" Then
                    TotalCancelar = TotalCancelar + Monto
                Else
                    TotalCancelar = TotalCancelar + Monto + (Interes * -1)
                End If

                DP_Moneda.Items.FindByValue(Coll_Doctos_Seleccionados.Item(I).id_p_0023).Selected = True
            Next

            HF_SaldoDoctos.Value = TotalDoctos + TotalInteres

            AsignaSource()

        End If

        'Si seleciono al menos una cuenta por cobrar
        If Not IsNothing(Coll_Cxc_Seleccionados) Then
            'And Coll_Cxc_Seleccionados.Count > 0

            gV_CxC.DataSource = Coll_Cxc_Seleccionados
            GV_CxC.DataBind()

            Dim Monto As Double
            Dim Interes As Double

            For i = 1 To Coll_Cxc_Seleccionados.Count
                cxc_sel = True
                Monto = Coll_Cxc_Seleccionados.Item(i).MontoPagar '* Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)
                Interes = Coll_Cxc_Seleccionados.Item(i).Interes '* Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)

                TotalCxc = TotalCxc + Monto
                TotalInteresCxc = TotalInteresCxc + Interes
                TotalCancelarCxc = TotalCancelarCxc + (Monto + Interes)
                DP_Moneda.Items.FindByValue(Coll_Cxc_Seleccionados.Item(i).id_p_0023).Selected = True
            Next

            HF_SaldoCxC.Value = TotalCxc + TotalInteresCxc
            AsignaSourceCxC()

        End If

        'Si hay algun documento o cuenta para pagar
        If cxc_sel Or doc_sel Then

            BloqueaFormaDePago(False)

            Dim Formato As String = ""

            DP_Moneda.Enabled = False
            Select Case DP_Moneda.SelectedItem.Value
                Case 1 : Formato = Fmt.FCMSD
                Case 2, 4 : Formato = Fmt.FCMCD4
                Case 3 : Formato = Fmt.FCMCD
            End Select

            Select Case DP_Moneda.SelectedValue
                Case 1
                    Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4
                    Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999.99"
            End Select

            Txt_Tot_CXC.Text = Format(TotalCxc, Formato)
            Txt_Tot_Doc.Text = Format(TotalDoctos, Formato)
            Txt_Tot_Sel.Text = Format(totalSeleccionado + TotalCxc, Formato)
            Txt_Tot_Int.Text = Format(TotalInteres + TotalInteresCxc, Formato)

            If Pagos.Pagador = "D" Then
                Txt_Tot_Not_Cre.Text = Format(TotalNotaCre, Formato)
            End If

            Txt_Total_Cobrar.Text = Format(totalSeleccionado + TotalCxc - TotalNotaCre, Formato)

            If Me.Txt_Tot_Int.Text <> "" Then
                If Me.Txt_Tot_Int.Text < 0 Then
                    Txt_Tot_Int_Dev.Text = Format(Txt_Tot_Int.Text * -1, Formato)
                    Txt_Tot_Int.Text = 0
                End If
            End If

            If Pagos.Pagador = "D" Then
                Txt_Tot_Int_Dev.Text = 0
                Txt_Tot_Int.Text = 0
            End If

        End If

    End Sub

    Protected Sub IB_Doctos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles IB_Doctos.Click
        Try

            'Coll_Doctos_Seleccionados = New Collection
            'Coll_Cxc_Seleccionados = New Collection
            Txt_Orden.Text = DP_Banco.SelectedItem.Text

            NroPaginacionCXC = 0
            NroPaginaCxC = 1

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try

            Dim Pagos As New ClsSession.SesionPagos

            With Pagos
                .TasaInteresCalculo = CDbl(Txt_Tasa.Text)
            End With

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Try

            If Txt_DiasDevInt.Text = "" Then
                Txt_DiasDevInt.Text = 0
            End If
            Dim Pagos As New ClsSession.SesionPagos

            With Pagos
                .DiasDevolverInteres = Val(Txt_DiasDevInt.Text)
            End With

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Fec_Pag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Pag.TextChanged
        Try
            If Txt_Fec_Pag.Text <> "" And Txt_Fec_Pag.Text <> "__/__/____" Then
                If Not IsDate(Txt_Fec_Pag.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha de pago erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_Pag.Text = ""
                    Exit Sub
                End If
            Else

            End If
            Pagos.FechaPago = Txt_Fec_Pag.Text
            CalculaInteres()
            actualiza_totales()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Cancelacion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub Txt_Tasa_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Tasa.TextChanged
        Try
            If Txt_Tasa.Text = "" Then
                Txt_Tasa.Text = 0
            End If

            If Val(CDbl(Txt_Tasa.Text)) > 100 Then
                Msj.Mensaje(Me, "Atención", "Tasa no debe ser mayor a 100", ClsMensaje.TipoDeMensaje._Excepcion)
                Exit Sub
            End If

            Pagos.TasaInteresCalculo = CDbl(Txt_Tasa.Text)
            CalculaInteres()
            actualiza_totales()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CalculaInteres()

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
            Dim Tasa_Base As Double
            Dim Spread As Double
            Dim Puntos As Double


            'Buscamos el documento para traer todas sus relaciones

            For i = 1 To Coll_Doctos_Seleccionados.Count

                Dim DOC As doc_cls = OP.DocumentoOtorgagoDevuelvePorId(Coll_Doctos_Seleccionados.Item(i).id_doc)
                'Rescatamos el saldo del documento
                Select Case Pagador
                    Case "C"
                        Saldo = Coll_Doctos_Seleccionados.Item(i).MontoPagar
                    Case "D"
                        Saldo = Coll_Doctos_Seleccionados.Item(i).MontoPagar
                End Select

                'Monto a pagar por defecto toma el saldo completo
                '            MtoAPagar = CDbl(Mto_A_Pagar.Text)
                MtoAPagar = Saldo

                'validamos si la fecha de ultimo pago viene nula
                If IsNothing(DOC.doc_ful_pgo) Then
                    FechaUltPago = "01/01/1900"
                Else
                    FechaUltPago = DOC.doc_ful_pgo
                End If

                FechaSimula = DOC.opo_cls.ope_cls.ope_fec_sim
                FechaVctoRea = DOC.dsi_cls.dsi_fev_cal
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

                If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas) Then
                    Tasa_Base = 0
                Else
                    Tasa_Base = DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas
                End If

                If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead) Then
                    Spread = 0
                Else
                    Spread = DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead
                End If

                If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr) Then
                    Puntos = 0
                Else
                    Puntos = DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr
                End If


                TasaNegocio = Tasa_Base + Spread + Puntos

                Interes = Formulas.RetornaCalculoInteres(Pagos.FechaPago, _
                                                         Pagos.DiasRetencionPago, _
                                                         Pagos.TasaInteresCalculo, _
                                                         MtoAPagar, _
                                                         FechaSimula, _
                                                         FechaVctoRea, _
                                                         CantidadDias, _
                                                         Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli, _
                                                         FechaUltPago, _
                                                         Pagos.DiasDevolverInteres, _
                                                         Lineal, _
                                                         TasaAnuMen, _
                                                         TasaNegocio, _
                                                         TasaRenova, _
                                                         MtoAnticip, _
                                                         FecVctoOri, _
                                                         NroRenovac, _
                                                         DOC.id_doc, _
                                                         DOC.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas)



                Coll_Doctos_Seleccionados.Item(i).Tasa = Pagos.TasaInteresCalculo

                If Pagador = "D" Then

                    If Interes > 0 Then


                        If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMCD)
                            ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        Else
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMSD)
                            ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        End If

                    Else

                        If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMCD)
                            '  Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMSD)

                        Else
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMSD)
                            'Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        End If

                    End If

                Else

                    If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                        Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMCD)
                        'Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(Interes + MtoAPagar, Fmt.FCMSD)
                    Else
                        Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, Fmt.FCMSD)
                        ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(Interes + MtoAPagar, Fmt.FCMSD)
                    End If

                End If

            Next
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Cliente.CheckedChanged
        Txt_Orden.Text = "Cliente"
        Txt_Orden.ReadOnly = True
        Txt_Orden.CssClass = "clsDisabled"
        MP_DoctoPago.Show()
    End Sub

#End Region

#Region "Sub / Function"

    Private Sub CargaDatos()

        Try

            Txt_Fec_Pag.Text = Format(Date.Now, "dd/MM/yyyy")
            Txt_DiasDevInt.Text = Val(CG.SistemaDevuelve().sis_dia_dev)
            Txt_Dia_Ret.Text = 0

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Mto_Rec.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_DiasDevInt.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Dia_Ret.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tasa.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_Dolar_Cob.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Dolar_Obs.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_Tot_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Sel.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Not_Cre.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Int.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Total_Cancelar.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Total_Cobrar.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_Tot_CXC.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Int_Dev.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_NroDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Mto_Dco.Attributes.Add("Style", "TEXT-ALIGN: right")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub LimpiarCampos(ByVal ctrlParent As Control, Optional ByVal txtcampo As String = "" _
     , Optional ByVal cboCampo As String = "")
        Try

            'Clareamos todos los cuadros de texto 
            For Each ctrlChild As Control In ctrlParent.Controls()
                If TypeOf ctrlChild Is TextBox AndAlso ctrlChild.ID <> txtcampo Then
                    DirectCast(ctrlChild, TextBox).Text = ""


                ElseIf TypeOf ctrlChild Is DropDownList AndAlso ctrlChild.ID <> cboCampo Then
                    '     If IndiceLista(DirectCast(ctrlChild, DropDownList)) Then _ 
                    '         DirectCast(ctrlChild, DropDownList).SelectedValue = "-1" 


                    'ElseIf TypeOf ctrlChild Is ListBox Then
                    '    DirectCast(ctrlChild, ListBox).Items.Clear()
                End If
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub LimpiaDPO()
        'DP_Moneda.ClearSelection()
        DP_FormaPago.ClearSelection()
        Txt_Mto_Rec.Text = ""
    End Sub

    Private Sub CargaDrop()

        Try

            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_Moneda)
            CG.ParametrosDevuelve(TablaParametro.TipoIngreso, True, DP_FormaPago)
            CG.ParametrosDevuelve(TablaParametro.OrigenFondo, True, DP_OrigenFondo)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_Plaza)
            CG.BancosDevuelveTodos(DP_Banco)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_PlazaBanco)



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub BloqueaFormaDePago(ByVal Estado As Boolean)

        Try

            If Estado Then

                DP_Moneda.Enabled = False
                DP_FormaPago.Enabled = False
                Txt_Mto_Rec.ReadOnly = True

                DP_Moneda.CssClass = "clsDisabled"
                DP_FormaPago.CssClass = "clsDisabled"
                Txt_Mto_Rec.CssClass = "clsDisabled"

                IB_Agregar.Enabled = False
                IB_Quitar.Enabled = False

            Else

                DP_Moneda.Enabled = True
                DP_FormaPago.Enabled = True
                Txt_Mto_Rec.ReadOnly = False

                DP_Moneda.CssClass = "clsMandatorio"
                DP_FormaPago.CssClass = "clsMandatorio"
                Txt_Mto_Rec.CssClass = "clsMandatorio"

                IB_Agregar.Enabled = True
                IB_Quitar.Enabled = True

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub BloqueaDatosBanco(ByVal Estado As Boolean)

        If Estado Then

            DP_Banco.Enabled = False
            DP_PlazaBanco.Enabled = False
            DP_OrigenFondo.Enabled = False

            Txt_NroDocto.ReadOnly = True
            Txt_Fec_Emi.ReadOnly = True
            Txt_Fec_Vto.ReadOnly = True
            Txt_Cta_Cte.ReadOnly = True
            Txt_Mto_Dco.ReadOnly = True
            Txt_Orden.ReadOnly = True

            RB_Banco.Enabled = False
            RB_Cliente.Enabled = False
            RB_Tercero.Enabled = False

            DP_Banco.CssClass = "clsDisabled"
            DP_PlazaBanco.CssClass = "clsDisabled"
            DP_OrigenFondo.CssClass = "clsDisabled"

            Txt_NroDocto.CssClass = "clsDisabled"
            Txt_Fec_Emi.CssClass = "clsDisabled"
            Txt_Fec_Vto.CssClass = "clsDisabled"
            Txt_Cta_Cte.CssClass = "clsDisabled"
            Txt_Mto_Dco.CssClass = "clsDisabled"
            Txt_Orden.CssClass = "clsDisabled"

        Else

            DP_Banco.Enabled = True
            DP_PlazaBanco.Enabled = True
            DP_OrigenFondo.Enabled = True

            Txt_NroDocto.ReadOnly = False
            Txt_Fec_Emi.ReadOnly = False
            Txt_Fec_Vto.ReadOnly = False
            Txt_Cta_Cte.ReadOnly = False
            Txt_Mto_Dco.ReadOnly = False
            Txt_Orden.ReadOnly = False

            RB_Banco.Enabled = True
            RB_Cliente.Enabled = True
            RB_Tercero.Enabled = True

            DP_Banco.CssClass = "clsMandatorio"
            DP_PlazaBanco.CssClass = "clsMandatorio"
            DP_OrigenFondo.CssClass = "clsMandatorio"

            Txt_NroDocto.CssClass = "clsMandatorio"
            Txt_Fec_Emi.CssClass = "clsMandatorio"
            Txt_Fec_Vto.CssClass = "clsMandatorio"
            Txt_Cta_Cte.CssClass = "clsMandatorio"
            Txt_Mto_Dco.CssClass = "clsMandatorio"
            Txt_Orden.CssClass = "clsMandatorio"

        End If


    End Sub

    Private Sub LimpiaDatosBanco()

        DP_Banco.ClearSelection()
        DP_PlazaBanco.ClearSelection()
        DP_OrigenFondo.ClearSelection()
        Txt_NroDocto.Text = ""
        Txt_Fec_Emi.Text = ""
        Txt_Fec_Vto.Text = ""
        Txt_Cta_Cte.Text = ""
        Txt_Mto_Dco.Text = ""
        Txt_Orden.Text = ""
        RB_Banco.Checked = True
        RB_Cliente.Checked = False
        RB_Tercero.Checked = False

    End Sub

    Private Sub AsignaSource()
        Try

            Dim Pagos As New ClsSession.SesionPagos

            'Select Case Pagos.Pagador
            '    Case "C"
            '        Gr_Documentos.Columns(7).Visible = True
            '        Gr_Documentos.Columns(8).Visible = False
            '    Case "D"
            '        Gr_Documentos.Columns(7).Visible = False
            '        Gr_Documentos.Columns(8).Visible = True
            'End Select
            Dim Formato As String = ""

            For I = 0 To Gr_Documentos.Rows.Count - 1
                'Rut Deudor
                Gr_Documentos.Rows(I).Cells(0).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(0).Text)) & "-" & Coll_Doctos_Seleccionados.Item(I + 1).deu_dig_ito 'RC.Vrut(CLng(Gr_Documentos.Rows(I).Cells(0).Text))

                'If Pagos.Pagador = "C" Then
                '    Gr_Documentos.Rows(I).Cells(7).Text = RC.FormatoMiles(CDbl(Gr_Documentos.Rows(I).Cells(8).Text))
                'Else
                '    Gr_Documentos.Rows(I).Cells(8).Text = RC.FormatoMiles(CDbl(Gr_Documentos.Rows(I).Cells(8).Text))
                'End If

                Select Case Coll_Doctos_Seleccionados.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                Gr_Documentos.Rows(I).Cells(7).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(7).Text), Formato)
                Gr_Documentos.Rows(I).Cells(8).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(8).Text), Formato)
                Gr_Documentos.Rows(I).Cells(9).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(9).Text), Formato)
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub AsignaSourceCxC()
        Try

            Dim Formato As String = ""

            For I = 0 To GV_CxC.Rows.Count - 1

                Select Case Coll_Cxc_Seleccionados.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                GV_CxC.Rows(I).Cells(5).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text), Formato)
                GV_CxC.Rows(I).Cells(6).Text = Format(CDbl(GV_CxC.Rows(I).Cells(6).Text), Formato)
                GV_CxC.Rows(I).Cells(7).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text) + CDbl(GV_CxC.Rows(I).Cells(6).Text), Formato)
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Function ValidaPagosConIngresos() As Boolean
        Try

            Dim Pagos As New ClsSession.SesionPagos
            Dim Total_a_Pagar As Double = 0

            If Txt_Tot_Sel.Text = 0 Then
                Msj.Mensaje(Me.Page, "Seleccion de Documentos", "No ha seleccionado documentos o cuentas para pagar", TipoDeMensaje._Exclamacion)
                Return False
            End If

            If Txt_Total_Cancelar.Text = "" Then
                Msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe modificar total a pagar en doctos. o CXC total a cancelar es distinto a total a cobrar", TipoDeMensaje._Exclamacion)
                Return False
            End If

            If Txt_Total_Cobrar.Text = "" Then
                Msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe modificar total a pagar en doctos. o CXC total a cancelar es distinto a total a cobrar", TipoDeMensaje._Exclamacion)
                Return False
            End If

            Total_a_Pagar = CDbl(Txt_Total_Cobrar.Text.Replace(".", ""))

            If Total_a_Pagar <> Pagos.TotalRecaudado Then
                Msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe modificar total a pagar en doctos. o CXC total a cancelar es distinto a total a cobrar", TipoDeMensaje._Exclamacion)
                Return False
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Function

    Private Function ValidaPago() As Boolean
        Try

   
            'Valida Campos C/Información
            If DP_Moneda.SelectedIndex = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar moneda", TipoDeMensaje._Informacion)
                DP_Moneda.Focus()
                Return False
                Exit Function
            End If

            'Valida Forma de Pago
            If DP_FormaPago.SelectedIndex = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar toda la información", TipoDeMensaje._Informacion)
                DP_FormaPago.Focus()
                Return False
                Exit Function
            End If

            If DP_Banco.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Banco", TipoDeMensaje._Informacion)
                DP_FormaPago.Focus()
                Return False
                Exit Function
            End If
            If DP_PlazaBanco.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione plaza", TipoDeMensaje._Informacion)
                DP_FormaPago.Focus()
                Return False
                Exit Function

            End If


            If Txt_NroDocto.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese número de documento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
                Exit Function
            End If


            If DP_Banco.SelectedIndex > 0 Then
                'Valida Mto del cheque > 0
                If Val(Txt_Mto_Dco.Text) <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto del cheque mayor a cero ", TipoDeMensaje._Informacion)
                    Txt_Mto_Rec.Focus()
                    Return False
                    Exit Function
                End If
            Else
                'Valida Mto Recaudado > 0
                If Val(Txt_Mto_Rec.Text) <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto recaudación mayor a cero ", TipoDeMensaje._Informacion)
                    Txt_Mto_Rec.Focus()
                    Return False
                    Exit Function

                End If
            End If


            If Txt_Fec_Emi.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese fecha emision", TipoDeMensaje._Informacion)
                Return False
                Exit Function

            End If

            If Not IsDate(Txt_Fec_Emi.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha emision erronea", TipoDeMensaje._Informacion)
                Txt_Fec_Emi.Text = ""
                Return False
                Exit Function

            End If


            If Txt_Fec_Vto.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Fecha emision erronea", TipoDeMensaje._Informacion)
                Return False
                Exit Function

            End If


            If Not IsDate(Txt_Fec_Vto.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento erronea", TipoDeMensaje._Informacion)
                Txt_Fec_Vto.Text = ""
                Return False
                Exit Function
            End If


            If Txt_Cta_Cte.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese cuente corriente", TipoDeMensaje._Informacion)
                Return False
                Exit Function
            End If


            If DP_OrigenFondo.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione origen de fondo", TipoDeMensaje._Informacion)
                Return False
                Exit Function
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Function

    Private Sub CreaObjetos()
        Try

            Dim Saldo_DPO As Double
            Dim Saldo_CxC_Docto As Double
            Dim Saldo_Interes As Double
            Dim TotalPago As Double
            Dim Monto As Double
            Dim Interes As Double
            Dim TotalAPagarPorDocto As Double
            Dim SaldoPorPagar As Double
            Dim Ind_cxc As Integer = 1
            Dim Ind_doc As Integer = 1
            Dim Ind_dpo As Integer = 1

            Coll_Ing_Sec = New Collection

            Saldo_CxC_Docto = 0
            Saldo_Interes = 0
            For Indice_DPO = Ind_dpo To Coll_DPO.Count

                'Asignamos el saldo del documento de pago
                Saldo_DPO = Coll_DPO.Item(Indice_DPO).dpo_mto

                'Haga mientras quede saldo
                While Saldo_DPO > 0

                    'Cuentas por Cobrar
                    If Ind_cxc - 1 <> Coll_Cxc_Seleccionados.Count Then

                        For Indice_cxc = Ind_cxc To Coll_Cxc_Seleccionados.Count

                            FACTOR_CAMBIO_HOY = Coll_Cxc_Seleccionados.Item(Indice_cxc).cxc_fac_cam

                            Select Case Coll_Cxc_Seleccionados.Item(Indice_cxc).id_p_0023
                                Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                                Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                                Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                                Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                            End Select

                            If Saldo_CxC_Docto > 0 Then
                                Monto = Saldo_CxC_Docto
                                Interes = 0
                            Else
                                Monto = Coll_Cxc_Seleccionados.Item(Indice_cxc).MontoPagar * FACTOR_CAMBIO_HOY
                                Interes = Coll_Cxc_Seleccionados.Item(Indice_cxc).Interes * FACTOR_CAMBIO_HOY
                                If Saldo_Interes > 0 Then
                                    Interes = Saldo_Interes
                                End If
                            End If

                            If (Monto + Interes) <= Saldo_DPO Then
                                TotalAPagarPorDocto = (Monto)
                            Else
                                If Interes > Saldo_DPO Then
                                    TotalAPagarPorDocto = 0
                                    Saldo_Interes = Interes - Saldo_DPO
                                    Interes = Saldo_DPO
                                    Monto = 0
                                Else
                                    TotalAPagarPorDocto = Saldo_DPO - Interes
                                End If
                            End If

                            TotalPago = Monto + Interes

                            If (TotalAPagarPorDocto + Interes) > TotalPago Then
                                Saldo_CxC_Docto = (TotalAPagarPorDocto + Interes) - TotalPago
                            Else
                                Saldo_CxC_Docto = TotalPago - (TotalAPagarPorDocto + Interes)
                            End If

                            SaldoPorPagar = Saldo_DPO - (TotalAPagarPorDocto + Interes)

                            If SaldoPorPagar <= 0 Then
                                Saldo_DPO = 0
                            Else
                                'Descontamos el monto pagado al saldo de la DPO para ver cuantos Cxc puede pagar
                                Saldo_DPO = Saldo_DPO - (TotalAPagarPorDocto + Interes)
                            End If

                            Crea_Ing_sec(1, Indice_DPO, TotalAPagarPorDocto, Interes, Coll_Cxc_Seleccionados.Item(Indice_cxc))

                            If Saldo_DPO <= 0 Then Exit For

                            Ind_cxc += 1

                        Next
                    Else
                        Exit For
                    End If

                End While

                Ind_dpo += 1

            Next

            Saldo_Interes = 0

            For Indice_DPO = Ind_dpo To Coll_DPO.Count

                'Asignamos el saldo del documento de pago
                If SaldoPorPagar > 0 Then
                    Saldo_DPO = SaldoPorPagar
                Else
                    Saldo_DPO = Coll_DPO.Item(Indice_DPO).dpo_mto
                End If

                'Haga mientras quede saldo
                While Saldo_DPO > 0

                    'Documentos
                    If Ind_doc - 1 <> Coll_Doctos_Seleccionados.Count Then

                        For Indice_doc = Ind_doc To Coll_Doctos_Seleccionados.Count

                            FACTOR_CAMBIO_HOY = Coll_Doctos_Seleccionados.Item(Indice_doc).ope_fac_cam

                            Select Case Coll_Doctos_Seleccionados.Item(Indice_doc).id_p_0023
                                Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                                Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                                Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                                Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                            End Select

                            If Saldo_CxC_Docto > 0 Then
                                Monto = Saldo_CxC_Docto
                                Interes = 0
                            Else
                                Monto = Coll_Doctos_Seleccionados.Item(Indice_doc).MontoPagar * FACTOR_CAMBIO_HOY
                                Interes = Coll_Doctos_Seleccionados.Item(Indice_doc).Interes * FACTOR_CAMBIO_HOY
                                If Saldo_Interes > 0 Then
                                    Interes = Saldo_Interes
                                End If
                            End If

                            If (Monto + Interes) <= Saldo_DPO Then
                                TotalAPagarPorDocto = (Monto)
                            Else
                                If Interes > Saldo_DPO Then
                                    TotalAPagarPorDocto = 0
                                    Interes = Saldo_DPO
                                    Saldo_Interes = Interes - Saldo_DPO
                                    Monto = 0
                                Else
                                    TotalAPagarPorDocto = Saldo_DPO - Interes
                                End If
                            End If

                            TotalPago = Monto + Interes

                            If TotalAPagarPorDocto > TotalPago Then
                                Saldo_CxC_Docto = (TotalAPagarPorDocto + Interes) - TotalPago
                            Else
                                Saldo_CxC_Docto = TotalPago - (TotalAPagarPorDocto + Interes)
                            End If

                            SaldoPorPagar = Saldo_DPO - (TotalAPagarPorDocto + Interes)

                            If SaldoPorPagar <= 0 Then
                                Saldo_DPO = 0
                            Else
                                'Descontamos el monto pagado al saldo de la DPO para ver cuantos Cxc puede pagar
                                Saldo_DPO = Saldo_DPO - (TotalAPagarPorDocto + Interes)
                            End If

                            Crea_Ing_sec(2, Indice_DPO, TotalAPagarPorDocto, Interes, Coll_Doctos_Seleccionados.Item(Indice_doc))

                            If Saldo_DPO <= 0 Then Exit For

                            Ind_doc += 1

                        Next
                    Else
                        Exit For
                    End If

                End While

                Ind_dpo += 1

            Next


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub Crea_Ing_sec(ByVal Tipo As Int16, ByVal Indice_DPO As Integer, ByVal Monto_Abonado As Double, ByVal Interes_Abonado As Double, ByVal Objeto As Object)
        Try

            Dim Formulas As New FormulasGenerales
            Dim SaldoPorPagar As Double = 0
            Dim MONTO As Double
            Dim INTERES As Double

            'recorremos cuantos Documentos se pueden pagar con un DPO

            Dim Ing_Sec As New ing_sec_cls
            Dim ABONO_CLIENTE, EXCEDENTE, MAYOR_PAGO, MONTO_MENOR, REAJUSTE As Double

            Ing_Sec.id_ing_sec = Nothing
            Ing_Sec.id_ing = Nothing
            Ing_Sec.id_dpo = Indice_DPO
            Ing_Sec.ing_qpa = Pagos.Pagador
            Ing_Sec.ing_vld_rcz = CChar("I")

            If Tipo = 1 Then

                ' 1.- CUENTAS POR COBRAR
                Ing_Sec.id_P_0053 = 1

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                'Toma el factor de cuando se ingreso la cuenta
                FACTOR_CAMBIO_HOY = Objeto.cxc_fac_cam

                Ing_Sec.id_cxc = Objeto.id_cxc
                Ing_Sec.doc_sdo_ddr = 0

                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli)

                MONTO_MENOR = Formulas.MIN((Ing_Sec.doc_sdo_cli / FACTOR_CAMBIO_HOY), (Ing_Sec.ing_mto_abo / FACTOR_CAMBIO_HOY))
                REAJUSTE = Formulas.MIN((ABONO_CLIENTE / FACTOR_CAMBIO_HOY), (MONTO_MENOR) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO))

                Ing_Sec.ing_pag_deu = CChar("N")

                MONTO = Monto_Abonado / FACTOR_CAMBIO_HOY
                INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                If IsNothing(Ing_Sec.doc_sdo_cli) Then
                    Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar)
                End If

                Ing_Sec.doc_sdo_cli = CDec(Ing_Sec.doc_sdo_cli - MONTO)

                'MONTO = CDbl(Objeto.MontoPagar) * FACTOR_CAMBIO_HOY
                'INTERES = CDbl(Objeto.Interes) * FACTOR_CAMBIO_HOY

            Else

                ' 2.- DOCUMENTOS
                Ing_Sec.id_P_0053 = 2
                Ing_Sec.id_doc = Objeto.id_doc
                FACTOR_CAMBIO_HOY = Objeto.ope_fac_cam

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                '/*Calcula todo como Pago Deudor***************************************************/
                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli + Objeto.dsi_mto - Objeto.dsi_mto_ant)
                EXCEDENTE = Formulas.MAX(Formulas.MIN(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli, Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)
                MAYOR_PAGO = Formulas.MAX(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli - (Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)

                '/*Calculo de Reajuste*************************************************************/
                REAJUSTE = Formulas.MIN(ABONO_CLIENTE, Objeto.dsi_mto) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO)

                Ing_Sec.ing_pag_deu = CChar(Objeto.PagaDeudor)

                MONTO = Monto_Abonado / FACTOR_CAMBIO_HOY
                INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                Select Case Ing_Sec.ing_qpa
                    Case "C"

                        If IsNothing(Ing_Sec.doc_sdo_cli) Then
                            Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar)
                        End If

                        Ing_Sec.doc_sdo_cli = CDec(Ing_Sec.doc_sdo_cli - MONTO)
                        Ing_Sec.doc_sdo_ddr = 0

                    Case "D"
                        If IsNothing(Ing_Sec.doc_sdo_ddr) Then
                            Ing_Sec.doc_sdo_ddr = CDec(Objeto.MontoPagar)
                        End If
                        Ing_Sec.doc_sdo_ddr = CDec(Objeto.MontoPagar - MONTO)
                        Ing_Sec.doc_sdo_cli = 0

                End Select

            End If

            Ing_Sec.ing_rea_mon = REAJUSTE

            'Ojo ver cuando el interes es negativo
            Ing_Sec.ing_mto_int = INTERES
            Ing_Sec.ing_mto_abo = MONTO
            Ing_Sec.ing_mto_tot = Ing_Sec.ing_mto_abo + Ing_Sec.ing_mto_int
            Ing_Sec.ing_fac_cam = FACTOR_CAMBIO_HOY 'Pagos.DollarCobranza
            Ing_Sec.ing_fac_cam_obs = FACTOR_CAMBIO_OBS_HOY 'Pagos.DollarObservador
            Ing_Sec.doc_sdo_exc = CDbl(Objeto.doc_sdo_exc)

            'If CDec(Ing_Sec.ing_mto_tot) >= (Ing_Sec.doc_sdo_cli) Then
            If Ing_Sec.doc_sdo_cli = 0 Then
                Ing_Sec.ing_tot_par = "T"
            Else
                Ing_Sec.ing_tot_par = "P"
            End If

            'Crea la collection con los objetos para que luego sean grabados
            Coll_Ing_Sec.Add(Ing_Sec)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Drop SelectedIndexChange"

    Protected Sub DP_Plaza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Plaza.SelectedIndexChanged

        Try
            If Not IsNothing(CMC.DiasDeRetencionDoctoPagoDevuelve(Sucursal, DP_Plaza.SelectedValue)) Then
                Txt_Dia_Ret.Text = CMC.DiasDeRetencionDoctoPagoDevuelve(Sucursal, DP_Plaza.SelectedValue).pds_ret
            Else
                Txt_Dia_Ret.Text = 0
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_FormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_FormaPago.SelectedIndexChanged
        Try

            Dim TipoIngreso As p_0054_cls
            Dim Coll As Collection


            If DP_FormaPago.SelectedValue = 0 Then
                Exit Sub
            End If

            Coll = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIngreso, DP_FormaPago.SelectedValue)

            If Not IsNothing(Coll) Then
                If Coll.Count > 0 Then
                    TipoIngreso = Coll.Item(1)
                End If
            End If

            If Not IsNothing(TipoIngreso) Then
                If TipoIngreso.pnu_atr_003 = "S" Then
                    BloqueaDatosBanco(False)
                    Txt_Orden.ReadOnly = True

                    MP_DoctoPago.Show()
                End If
            End If
            RB_Banco.Checked = True

            If RB_Banco.Checked Then
                RB_Banco.Checked = True
                Txt_Orden.Text = "Banco"
                Txt_Orden.ReadOnly = True
                Txt_Orden.CssClass = "clsDisabled"
                'ElseIf RB_Cliente.Checked Then

                '    Txt_Orden.Text = "Cliente"
                '    Txt_Orden.ReadOnly = True
                '    Txt_Orden.CssClass = "clsDisabled"
                'ElseIf RB_Tercero.Checked Then
                '    Txt_Orden.Text = ""
                '    Txt_Orden.ReadOnly = False
                '    Txt_Orden.CssClass = "clsDisabled"
            End If
            Txt_Mto_Rec.Focus()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_Moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Moneda.SelectedIndexChanged

        Dim Mto As Double

        Txt_Mto_Rec_MaskedEditExtender.Mask = Nothing

        If Txt_Mto_Rec.Text <> "" Then
            Mto = Txt_Mto_Rec.Text.Replace(".", "").Replace(",", ".")
        End If

        Select Case DP_Moneda.SelectedValue
            Case 1
                Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999,999"
            Case 2
                Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999.9999"
            Case 3, 4
                Txt_Mto_Rec_MaskedEditExtender.Mask = "999,999,999.99"
        End Select

        If Mto > 0 Then
            Txt_Mto_Rec.Text = Mto
        End If

    End Sub

#End Region

#Region "Radio CheckedChanged"

    Protected Sub RB_Banco_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Banco.CheckedChanged
        Txt_Orden.Text = "Banco"
        Txt_Orden.ReadOnly = True
        Txt_Orden.CssClass = "clsDisabled"
        MP_DoctoPago.Show()

    End Sub

    Protected Sub RB_Tercero_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Tercero.CheckedChanged
        Txt_Orden.Text = ""
        Txt_Orden.CssClass = "clsMandatorio"
        Txt_Orden.ReadOnly = False
        Txt_Orden.Focus()
        MP_DoctoPago.Show()
    End Sub

#End Region

#Region "Eventos del Cliente"

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        Try

            LimpiaCliente()
            Ayuda = True

            If CB_Cliente.Checked Then

                BloqueaCliente(False)
                RB_Pag.SelectedValue = "C"

                If CB_Deudor.Checked Then
                    RB_Pag.Enabled = True

                Else
                    'RB_Pag_Cli.Enabled = False
                    'RB_Pag_Deu.Enabled = False
                End If

                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.Focus()

            Else

                BloqueaCliente(True)
                RB_Pag.SelectedValue = "D"

                If CB_Deudor.Checked Then
                    RB_Pag.Enabled = True

                Else
                    RB_Pag.SelectedValue = "C"

                End If

                IB_AyudaCli.Enabled = False
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_BuscaCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaCliente.Click
        Try
            Dim Cliente As cli_cls
            Dim CLSCLI As New ClaseClientes

            Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion, "", )
            Else
                CB_Cliente.Enabled = False

                If Cliente.id_P_0044 = 1 Then
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                Else
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim
                End If

                Txt_Tasa.Text = CMC.TasaRetorna(2, Txt_Rut_Cli.Text, 0)

                BloqueaCliente(True)

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub LimpiaCliente()
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
    End Sub

    Private Sub BloqueaCliente(ByVal Estado As Boolean)

        If Estado Then

            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Else
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
        End If

    End Sub

    Private Sub BloqueaParametros(ByVal Estado As Boolean)

        If Estado Then
            Txt_Tasa.ReadOnly = True
            Txt_DiasDevInt.ReadOnly = True
            DP_Plaza.Enabled = False
            Txt_Dolar_Cob.ReadOnly = True

            DP_Plaza.CssClass = "clsDisabled"
            Txt_Tasa.CssClass = "clsDisabled"
            Txt_DiasDevInt.CssClass = "clsDisabled"
            Txt_Dolar_Cob.CssClass = "clsDisabled"

        Else

            Txt_Tasa.ReadOnly = False
            Txt_DiasDevInt.ReadOnly = False
            DP_Plaza.Enabled = True
            Txt_Dolar_Cob.ReadOnly = False

            DP_Plaza.CssClass = "clsTxt"
            Txt_Tasa.CssClass = "clsTxt"
            Txt_DiasDevInt.CssClass = "clsTxt"
            Txt_Dolar_Cob.CssClass = "clsTxt"

            ImageButton2.Enabled = True
            ImageButton1.Enabled = True

        End If

    End Sub

    Private Sub CargaValorMonedas()
        Try

            Pagos.DollarCobranza = CG.ParidadDevuelve(3, Txt_Fec_Pag.Text).par_val_cob
            Pagos.DollarObservador = CG.ParidadDevuelve(3, Txt_Fec_Pag.Text).par_val

            Txt_Dolar_Cob.Text = Format(Pagos.DollarCobranza, Fmt.FCMCD)
            Txt_Dolar_Obs.Text = Format(Pagos.DollarObservador, Fmt.FCMCD)
            VALOR_UF = CG.ParidadDevuelve(2, Txt_Fec_Pag.Text).par_val_cob
            VALOR_EURO = CG.ParidadDevuelve(4, Txt_Fec_Pag.Text).par_val_cob

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito del NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If

            'If UCase(Txt_Dig_Cli.Text) <> RC.Vrut(Txt_Rut_Cli.Text.Replace(".", "")) Then
            '    Msj.Mensaje(Me.Page, Caption, "Digito incorrecto del cliente", TipoDeMensaje._Exclamacion)
            '    Txt_Dig_Cli.Focus()
            '    Exit Sub
            'End If

            Dim Cliente As cli_cls
            Dim CLSCLI As New ClaseClientes

            Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)

            If IsNothing(Cliente) Then
                Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion, "", )
            Else

                CB_Cliente.Enabled = False
                IB_AyudaCli.Enabled = False


                If Cliente.id_P_0044 = 1 Then
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                Else
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim
                End If


                Txt_Tasa.Text = CMC.TasaRetorna(2, Txt_Rut_Cli.Text, 0)

                BloqueaCliente(True)
                Me.IB_AyudaDeu.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Eventos del Deudor"

    Protected Sub LB_BuscaDeudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDeudor.Click
        Try

            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito del NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            'If UCase(Txt_Dig_Deu.Text) <> RC.Vrut(Txt_Rut_Deu.Text.Replace(".", "")) Then
            '    Msj.Mensaje(Me.Page, Caption, "Digito incorrecto del Pagador", TipoDeMensaje._Exclamacion)
            '    Txt_Dig_Cli.Focus()
            '    Exit Sub
            'End If  no se define q rut se ocupara Miguel Herrera

            Dim Deudor As deu_cls

            Deudor = CG.DeudorDevuelvePorRut(CLng(Txt_Rut_Deu.Text))

            If Not IsNothing(Deudor) Then
                CB_Deudor.Enabled = False
                Txt_Rso_Deu.Text = Deudor.deu_nom & " " & Deudor.deu_ape_ptn.Trim & " " & Deudor.deu_ape_mtn.Trim
                BloqueaDeudor(True)
                Me.IB_AyudaDeu.Enabled = False
                IB_AyudaDeu.Enabled = False
            Else
                Me.IB_AyudaDeu.Enabled = True
                Txt_Rut_Deu.Focus()
                Msj.Mensaje(Me.Page, Caption, "Pagador no existe", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub LimpiaDeudor()
        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        txt_rso_deu.Text = ""
    End Sub

    Private Sub BloqueaDeudor(ByVal Estado As Boolean)

        If Estado Then

            Txt_Rut_Deu.ReadOnly = True
            Txt_Dig_Deu.ReadOnly = True

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.CssClass = "clsDisabled"

        Else

            Txt_Rut_Deu.ReadOnly = False
            Txt_Dig_Deu.ReadOnly = False

            Txt_Rut_Deu.CssClass = "clsMandatorio"
            Txt_Dig_Deu.CssClass = "clsMandatorio"

        End If

    End Sub

    Protected Sub CB_Deudor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Deudor.CheckedChanged
        Try

            LimpiaDeudor()

            If CB_Deudor.Checked Then

                BloqueaDeudor(False)
                RB_Pag.SelectedValue = "D"

                'If CB_Cliente.Checked Then

                '    RB_Pag.Enabled = True

                'Else
                '    RB_Pag_Cli.Enabled = False
                '    RB_Pag_Deu.Enabled = False
                'End If

                Txt_Rut_Deu.Focus()
                IB_AyudaDeu.Enabled = True

            Else

                BloqueaDeudor(True)

                RB_Pag.SelectedValue = "C"

                If CB_Cliente.Checked Then

                Else

                    RB_Pag.Enabled = True

                End If

                IB_AyudaDeu.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try

            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar dígito del NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            'If UCase(Txt_Dig_Deu.Text) <> RC.Vrut(Txt_Rut_Deu.Text.Replace(".", "")) Then
            '    Msj.Mensaje(Me.Page, Caption, "Dígito incorrecto del Pagador", TipoDeMensaje._Exclamacion)
            '    Txt_Dig_Cli.Focus()
            '    Exit Sub
            'End If comentado por definir rut 

            Dim Deudor As deu_cls

            Deudor = CG.DeudorDevuelvePorRut(CLng(Txt_Rut_Deu.Text))

            If Not IsNothing(Deudor) Then
                CB_Deudor.Enabled = False

                If Deudor.id_P_0044 <> 1 Then
                    Txt_Rso_Deu.Text = Deudor.deu_rso
                ElseIf Deudor.id_P_0044 = 1 Then
                    'se cambio nombre a Razón Social de deudor para que en la ayuda y en esta pantalla se muestre lo mismo
                    Txt_Rso_Deu.Text = Deudor.deu_rso & " " & Deudor.deu_ape_ptn.Trim & " " & Deudor.deu_ape_mtn.Trim
                End If

                BloqueaDeudor(True)
                Me.IB_AyudaDeu.Enabled = False
            Else
                Me.IB_AyudaDeu.Enabled = True
                Txt_Rut_Deu.Focus()
                Msj.Mensaje(Me.Page, Caption, "Pagador no existe", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Paleta de Documentos de Pagos"

    Protected Sub IB_Agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Agregar.Click

        Try
            'Valida Moneda
            If DP_Moneda.SelectedIndex = 0 Then
                Msj.Mensaje(Me, "Pago Directo", "Debe ingresar moneda", ClsMensaje.TipoDeMensaje._Exclamacion)
                DP_Moneda.Focus()
                Exit Sub
            End If

            'Valida Forma de Pago
            If DP_FormaPago.SelectedIndex = 0 Then
                Msj.Mensaje(Me, "Pago Directo", "Debe ingresar toda la información", ClsMensaje.TipoDeMensaje._Exclamacion)
                DP_FormaPago.Focus()
                Exit Sub
            End If

            Dim ValorMoneda As Double
            Select Case DP_Moneda.SelectedItem.Value
                Case 1 : ValorMoneda = 1
                Case 2 : ValorMoneda = VALOR_UF
                Case 3, 4 : ValorMoneda = Pagos.DollarCobranza
            End Select

            If ValorMoneda <= 0 Then
                Msj.Mensaje(Page, Caption, "Debe ingresar valor de moneda para pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Mto_Rec.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese monto de pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim DPO As New dpo_cls

            If Txt_NroDocto.Text <> "" Then
                DPO.dpo_num = Txt_NroDocto.Text
            End If

            DPO.id_P_0054 = DP_FormaPago.SelectedValue
            DPO.id_P_0023 = DP_Moneda.SelectedValue
            DPO.dpo_mto = CDbl(Txt_Mto_Rec.Text)
            DPO.id_P_0052 = 1

            'Datos del cheque ingresado
            If DP_Banco.SelectedIndex > 0 Then

                DPO.id_bco = DP_Banco.SelectedValue
                DPO.id_PL_000047 = DP_PlazaBanco.SelectedValue
                DPO.dpo_fec_emi = Txt_Fec_Emi.Text
                DPO.dpo_fev = Txt_Fec_Vto.Text
                DPO.dpo_cct = Txt_Cta_Cte.Text
                DPO.id_P_0087 = DP_OrigenFondo.SelectedValue
                DPO.dpo_aor = Txt_Orden.Text.Trim
                DPO.dpo_num = Txt_NroDocto.Text
                DPO.dpo_mto = Txt_Mto_Dco.Text
            End If

            Coll_DPO.Add(DPO)

            GV_Pagos.DataSource = Coll_DPO
            GV_Pagos.DataBind()


            FormatoGrillaDPO()
            LimpiaDatosBanco()
            LimpiaDPO()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Quitar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Quitar.Click

        Try

            If HF_Pos_DPO.Value = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione un registro para eliminar", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Coll_DPO.Remove(CInt(HF_Pos_DPO.Value))

            GV_Pagos.DataSource = Coll_DPO
            GV_Pagos.DataBind()

            FormatoGrillaDPO()
            HF_Pos_DPO.Value = ""
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoGrillaDPO()
        Try

            Dim SumaRec As Double = 0
            Dim Pagos As New ClsSession.SesionPagos

            For I = 0 To GV_Pagos.Rows.Count - 1

                Dim Formato As String = ""
                Dim ValorMoneda As Double

                CType(GV_Pagos.Rows(I).FindControl("Img_Ver"), ImageButton).ToolTip = I + 1

                For x = 0 To DP_FormaPago.Items.Count
                    If GV_Pagos.Rows(I).Cells(0).Text = DP_FormaPago.Items(x).Value Then
                        GV_Pagos.Rows(I).Cells(0).Text = DP_FormaPago.Items(x).Text
                        Exit For
                    End If
                Next

                For x = 1 To DP_Moneda.Items.Count

                    If GV_Pagos.Rows(I).Cells(2).Text = DP_Moneda.Items(x).Value Then

                        GV_Pagos.Rows(I).Cells(2).Text = DP_Moneda.Items(x).Text

                        Select Case DP_Moneda.Items(x).Value
                            Case 1 : Formato = Fmt.FCMSD : ValorMoneda = 1
                            Case 2 : Formato = Fmt.FCMCD4 : ValorMoneda = VALOR_UF
                            Case 3, 4 : Formato = Fmt.FCMCD : ValorMoneda = Pagos.DollarCobranza
                        End Select

                        Exit For

                    End If

                Next

                GV_Pagos.Rows(I).Cells(3).Text = Format(CDbl(GV_Pagos.Rows(I).Cells(3).Text), Formato)

                SumaRec = SumaRec + (CDbl(GV_Pagos.Rows(I).Cells(3).Text)) '* ValorMoneda)

            Next

            Txt_Total_Cancelar.Text = Format(SumaRec, Fmt.FCMSD)
            Pagos.TotalRecaudado = SumaRec


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To GV_Pagos.Rows.Count - 1

            If CType(GV_Pagos.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip = btn.ToolTip Then
                HF_Pos_DPO.Value = i + 1
                If (i Mod 2) = 0 Then
                    GV_Pagos.Rows(i).CssClass = "selectable"
                Else
                    GV_Pagos.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    GV_Pagos.Rows(i).CssClass = "formatUltcell"
                Else
                    GV_Pagos.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next


    End Sub

#End Region

#Region "Documento de Pago (Ingreso de Cheque)"

    Protected Sub IB_AceptarCheque_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AceptarCheque.Click

        Try

            If ValidaPago() Then

                Dim DPO As New dpo_cls

                If Txt_NroDocto.Text <> "" Then
                    DPO.dpo_num = Txt_NroDocto.Text
                End If

                DPO.id_P_0052 = 1
                DPO.id_P_0054 = DP_FormaPago.SelectedValue
                DPO.id_P_0023 = DP_Moneda.SelectedValue
                DPO.dpo_mto = CDbl(Txt_Mto_Dco.Text)

                If DP_Banco.SelectedIndex > 0 Then
                    'Datos del cheque ingresado
                    DPO.id_bco = DP_Banco.SelectedValue
                    DPO.id_PL_000047 = DP_PlazaBanco.SelectedValue
                    DPO.dpo_num = Txt_NroDocto.Text
                    DPO.dpo_fec_emi = Txt_Fec_Emi.Text
                    DPO.dpo_fev = Txt_Fec_Vto.Text
                    DPO.dpo_cct = Txt_Cta_Cte.Text
                    DPO.dpo_mto = Txt_Mto_Dco.Text
                    DPO.id_P_0087 = DP_OrigenFondo.SelectedValue
                    DPO.dpo_aor = Txt_Orden.Text.Trim
                End If

                Coll_DPO.Add(DPO)
                'Convert.ToInt32(Coll_DPO.id_dpo)
                GV_Pagos.DataSource = Coll_DPO
                GV_Pagos.DataBind()

                FormatoGrillaDPO()
                LimpiaDatosBanco()
                LimpiaDPO()

                MP_DoctoPago.Hide()

            Else
                MP_DoctoPago.Show()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
        LimpiarCampos(Txt_Cta_Cte, Txt_NroDocto.Text, Txt_Mto_Dco.Text)
    End Sub

    Protected Sub IB_CancelarCheque_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CancelarCheque.Click

        Try
            RB_Banco.Checked = True

            RB_Cliente.Checked = False
            RB_Tercero.Checked = False
            Txt_Orden.Text = "Banco"
            Txt_Orden.ReadOnly = True
            Txt_Orden.CssClass = "clsDisabled"
        Catch ex As Exception

        End Try
        LimpiarCampos(Txt_Cta_Cte, Txt_NroDocto.Text, Txt_Mto_Dco.Text)
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20010110, Usr, "PRESIONO BOTON BUSCAR CXC Y DOCTOS.") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not CB_Cliente.Checked And Not CB_Deudor.Checked Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar un cliente o Pagador", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            If CB_Cliente.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "ingrese digito verificador cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                'If UCase(Txt_Dig_Cli.Text) <> RC.Vrut(Txt_Rut_Cli.Text.Replace(".", "")) Then
                '    Msj.Mensaje(Me.Page, Caption, "Digito incorrecto del cliente", TipoDeMensaje._Exclamacion)
                '    Txt_Dig_Cli.Focus()
                '    Exit Sub
                'End If por porblema con el digito Miguel Herrera

                Dim Cliente As cli_cls
                Dim CLSCLI As New ClaseClientes

                Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)
                If IsNothing(Cliente) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                If sesion.valida_cliente <> "" Then
                    Msj.Mensaje(Me.Page, Caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion, "", )
                    Exit Sub
                Else
                    CB_Cliente.Enabled = False

                    If Cliente.id_P_0044 <> 1 Then
                        Txt_Raz_Soc.Text = Cliente.cli_rso.Trim
                    Else
                        Txt_Raz_Soc.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                    End If

                    Txt_Tasa.Text = CMC.TasaRetorna(2, Txt_Rut_Cli.Text, 0)

                End If

                If Txt_Raz_Soc.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar un cliente", TipoDeMensaje._Informacion)
                    Txt_Rut_Cli.Focus()
                    Exit Sub
                End If

                LB_Cliente.Text = Txt_Rut_Cli.Text & "-" & Txt_Dig_Cli.Text & " " & Txt_Raz_Soc.Text

            End If

            If CB_Deudor.Checked Then

                If Txt_Rso_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar un Pagador", TipoDeMensaje._Informacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If

                LB_Deudor.Text = Txt_Rut_Deu.Text & "-" & Txt_Dig_Deu.Text & " " & Txt_Rso_Deu.Text

                Txt_Tasa.Text = Format(0, Fmt.FCMCD)

            End If

            CargaValorMonedas()

            Accordion1.SelectedIndex = 1
            IB_Doctos.Enabled = True

            BloqueaCliente(True)
            BloqueaParametros(False)

            Txt_Observacion.ReadOnly = False
            Txt_Observacion.CssClass = "clsMandatorio"
            IB_Agregar.Enabled = True
            IB_Quitar.Enabled = True

            Dim Pagos As New ClsSession.SesionPagos

            With Pagos

                .IniciarSesionPagos()

                .RutCliente = Val(Txt_Rut_Cli.Text.Replace(".", ""))
                .RutDeudor = Val(Txt_Rut_Deu.Text.Replace(".", ""))
                .Pagador = RB_Pag.SelectedValue

                .FechaPago = Txt_Fec_Pag.Text
                .DiasRetencionPago = Txt_Dia_Ret.Text
                .TasaInteresCalculo = CDbl(Txt_Tasa.Text)
                .DiasDevolverInteres = Txt_DiasDevInt.Text
                Coll_Cxc_Seleccionados = New Collection

            End With

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Limpiar.Click

        Accordion1.SelectedIndex = 0

        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Total_Cobrar.Text = ""
        Me.Txt_Total_Cancelar.Text = ""
        Me.Txt_Tot_Sel.Text = ""
        Me.Txt_Tot_Not_Cre.Text = ""
        Me.Txt_Tot_Int_Dev.Text = ""
        Me.Txt_Tot_Int.Text = ""
        Me.Txt_Tot_Doc.Text = ""
        Me.Txt_Tot_CXC.Text = ""
        Me.Txt_Tasa.Text = ""
        Me.Txt_Orden.Text = ""
        Me.Txt_Observacion.Text = ""
        Me.Txt_NroDocto.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        Me.Txt_Raz_Soc.Text = ""
        Me.Txt_Mto_Rec.Text = ""
        Me.Txt_Mto_Dco.Text = ""
        Me.IB_AyudaDeu.Enabled = False
        Me.LB_Cliente.Text = "Cliente"
        Me.LB_Deudor.Text = "Pagador"
        Me.LB_DoctoPago.Text = ""

        Me.CB_Cliente.Checked = False
        Me.CB_Cliente.Enabled = True
        Me.CB_Deudor.Enabled = True
        Me.CB_Deudor.Checked = False

        Me.DP_Banco.SelectedIndex = -1
        Me.DP_FormaPago.SelectedIndex = -1
        Me.DP_Moneda.SelectedIndex = -1
        Me.DP_Plaza.SelectedIndex = -1
        Me.DP_PlazaBanco.SelectedIndex = -1

        Me.GV_Pagos.Controls.Clear()
        Me.Gr_Documentos.Controls.Clear()

        Coll_DPO = New Collection
        Coll_Doctos_Seleccionados = New Collection
        Coll_Cxc_Seleccionados = New Collection
        Coll_Ing_Sec = New Collection

        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.CssClass = "clsDisabled"
        IB_AyudaCli.Enabled = False

        IB_AyudaDeu.Enabled = False
        Txt_Dig_Deu.ReadOnly = True
        Txt_Rut_Deu.ReadOnly = True
        Txt_Dig_Deu.CssClass = "clsDisabled"
        Txt_Rut_Deu.CssClass = "clsDisabled"
        RB_Pag.Enabled = True

        DP_Moneda.Enabled = False
        DP_FormaPago.Enabled = False
        Txt_Mto_Rec.ReadOnly = True

        DP_Moneda.ClearSelection()
        DP_FormaPago.ClearSelection()
        Txt_Mto_Rec.Text = ""

        DP_Moneda.CssClass = "clsDisabled"
        DP_FormaPago.CssClass = "clsDisabled"
        Txt_Mto_Rec.CssClass = "clsDisabled"

        IB_Agregar.Enabled = False
        IB_Quitar.Enabled = False
        IB_Guardar.Enabled = False

        IB_Doctos.Enabled = False
        Txt_Observacion.ReadOnly = True
        Txt_Observacion.CssClass = "clsDisabled"

        Dim Pagos As New ClsSession.SesionPagos
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Not ValidaPagosConIngresos() Then Exit Sub

            Msj.Mensaje(Me.Page, Caption, "¿Desea guardar el pago?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            If Not Agt.ValidaAccesso(20, 20020110, Usr, "PRESIONO BOTON GUARDAR PAGOS") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim AG As New ActualizacionesGenerales
            Dim FG As New FormulasGenerales

            'Crea Cabecera del Ingreso
            Dim ING As New ing_cls

            ING.id_ing = Nothing
            ING.id_eje = CodEje
            ING.ing_obs = Txt_Observacion.Text
            ING.ing_fec = Txt_Fec_Pag.Text
            ING.ing_sis_fec = Date.Now
            ING.ing_pgo_hre = CChar("N")

            'La funcion llena una collection de sesion que se llama coll_ing_sec
            FG.CargaCollection_Ingresos(Coll_DPO, Coll_Cxc_Seleccionados, Coll_Doctos_Seleccionados, 1, Txt_Fec_Pag.Text)

            If Coll_Ing_Sec.Count > 0 Then

                Dim Id As Integer

                Id = PGO.PagosInserta(Coll_DPO, ING, Coll_Ing_Sec)

                If Id > 0 Then
                    IB_Guardar.Enabled = False
                    Dim RW As New FuncionesGenerales.RutinasWeb
                    RW.AbrePopup(Me, 1, "InformeDePagos.aspx?id=" & Id, "Pagos", 1200, 800, 10, 10)
                    Dim x As ImageClickEventArgs
                    IB_limpiar_Click(Me, x)

                Else
                    Msj.Mensaje(Me.Page, Caption, "No se pudo guardar pago", TipoDeMensaje._Exclamacion)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

End Class


