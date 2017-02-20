Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_Hoja_rec
    Inherits System.Web.UI.Page

    Dim pagos As New ClsSession.SesionPagos
    Dim clasecli As New ClaseClientes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim fmts As New FuncionesGenerales.ClsLocateInfo
    Dim fcc As New FuncionesGenerales.FComunes
    Dim msj As New ClsMensaje
    Dim REC As New ClaseRecaudación
    Dim CBZ As New ClaseCobranza
    Dim PGO As New ClasePagos
    Dim CMC As New ClaseComercial
    Dim frm As New FuncionesGenerales.ClsLocateInfo
    Dim op As New CapaDatos.ClaseOperaciones

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            'Limpia colecciones
            NroHojaRuta = Nothing
            Coll_DPO = New Collection
            Coll_DSI = New Collection
            coll_nce = New Collection
            Coll_Doctos_Seleccionados = New Collection
            Coll_Ing_Sec = New Collection
            Coll_Cobranza = New Collection
            Txt_tasa.Text = Format(0, frm.FCMCD)

            alinea_textos()
            cg.EjecutivosDevuelve(Dr_Rec, CodEje, 14)
            cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_PlazaBanco)
            cg.ParametrosDevuelveTodos(TablaParametro.OrigenFondo, True, DP_OrigenFondo)

            cg.ParametrosDevuelve(54, True, Dr_for_pgo)
            cg.ParametrosDevuelve(23, True, Me.Dr_mon_rec)
            Me.dr_pgdr.SelectedIndex = 2

            activa_desactiva_datos_docto("I")
            activa_desactiva_datos_cli_deu("I")
            Me.Txt_Fec_Rec.Text = Format(CDate(Date.Now.ToShortDateString), "dd/MM/yyyy")

            HF_IdPlaza.Value = 0
            HF_Dias.Value = 0

        End If

        If SW_Rec = 3 Or SW_Rec = 0 Then
            carga_doctos_wc()
        End If

        If TabContainer1.ActiveTabIndex = 0 Or gr_doctos.Rows.Count <= 0 Then
            LB_NCE_Click(Me, e)
        End If


        txt_mto_pgo.Attributes.Add("Style", "Text-Align: right")
        txt_can_doc_pag.Attributes.Add("Style", "Text-Align: right")
        txt_tot_rec.Attributes.Add("Style", "Text-Align: right")
        txt_cant_pgo.Attributes.Add("Style", "Text-Align: right")
        txt_tot_dcto_pag.Attributes.Add("Style", "Text-Align: right")
        Txt_tasa.Attributes.Add("Style", "Text-Align: right")
        'Valida si hay documentos seleccionados en el PopUp Documentos Deudor


        If Me.Rb_cli_deu.SelectedValue = "D" Then
            If Me.Txt_Rut_Deu.Text = "" Then
                Me.btn_doc_deu.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx?rut_deu=" & Nothing & " &Rec=S ', window, 'scroll:no;status:off;dialogWidth:1230;dialogHeight:650px;dialogLeft:100px;dialogTop:100px');")
            Else
                Me.btn_doc_deu.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx?rut_deu=" & CInt(Me.Txt_Rut_Deu.Text) & " &Rec=S ', window, 'scroll:no;status:off;dialogWidth:1230;dialogHeight:650px;dialogLeft:100px;dialogTop:100px');")
            End If
            cg.BancosDevuelveTodos(DP_Banco)
        Else
            Me.btn_doc_deu.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx?rut_deu=" & Nothing & " &Rec=S ', window, 'scroll:no;status:off;dialogWidth:1230;dialogHeight:650px;dialogLeft:100px;dialogTop:100px');")
            If Me.Txt_Rut_Deu.Text <> "" Then
                clasecli.BancosDevuelvePorCliente(True, DP_Banco, Nothing, CLng(Txt_Rut_Deu.Text))
            End If

        End If


        Me.btn_doc_nce.Attributes.Add("onclick", "var x=window.showModalDialog('popup_doctos_no_cedidos.aspx?rut_cli=" & Txt_Rut_Deu.Text & "&tipo_persona=" & Me.Rb_cli_deu.SelectedValue & "&rso=" & Me.Txt_Rso_Deu.Text & "&id_hre=" & Me.nro_hoja.Value & "', window, 'scroll:no;status:off;dialogWidth=650px;dialogHeight:450px;dialogLeft:200px;dialogTop:200px');")

        Me.Ib_ayu_deu.Attributes.Add("onclick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',620,410,200,150);")
        Me.Ib_ayuda_cli.Attributes.Add("onclick", "WinOpen(2,'../../Ayudas/Ayudacli.aspx?SW=" & 99 & "&hojarecaudacion=1" & "&valor=" & Me.Txt_Rut_Deu.UniqueID & "&valor2=" & Me.Txt_Dig_Deu.UniqueID & "&valor3=" & Me.Txt_Rso_Deu.UniqueID & " ','PopUpCliente',620,410,200,150);")

    End Sub

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        If Me.Txt_Dig_Deu.Text <> "" Then
            Dim x As System.Web.UI.ImageClickEventArgs
            Btn_Buscar_Click(Me, x)
            Me.lb_buscar_Click(Me, e)
        Else
            msj.Mensaje(Me.Page, "Atención", "Ingrese Digito Verificador", 3)
        End If
    End Sub

    Protected Sub lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_buscar.Click


        If Me.Rb_cli_deu.SelectedValue = "C" Then

            If UCase(Me.Txt_Dig_Deu.Text) <> UCase(fcc.Vrut(Me.Txt_Rut_Deu.Text)) Then
                msj.Mensaje(Me.Page, "Atención", "Digito Verificador Incorrecto", 3)
                Exit Sub

            End If
            Dim cli As cli_cls
            cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Deu.Text), Txt_Dig_Deu.Text)


            Session("Cliente") = cli

            'Txt_tasa.Text = CMC.TasaRetorna(2, CLng(Txt_Rut_Deu.Text), 0)

            If valida_cliente <> "" Then

                msj.Mensaje(Me.Page, "Atención", valida_cliente, 3)
                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Dig_Deu.Text = ""

            Else

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.ReadOnly = True
                Me.Txt_Rso_Deu.CssClass = "clsDisabled"

                'Asigna Razon Social / Nombre a Campo Cliente
                Me.Txt_Rso_Deu.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                Me.Rb_cli_deu.Enabled = False
                activa_desactiva_datos_docto("H")

            End If

            Txt_tasa.Text = CMC.TasaRetorna(2, Txt_Rut_Deu.Text, 0)
            Dim Pagos As New ClsSession.SesionPagos

            With Pagos

                .IniciarSesionPagos()

                If Me.Rb_cli_deu.SelectedValue = "C" Then
                    .RutCliente = Val(Txt_Rut_Deu.Text.Replace(".", ""))
                Else
                    .RutDeudor = Val(Txt_Rut_Deu.Text.Replace(".", ""))
                End If


                .RutDeudor = 0
                .Pagador = "C"

                .FechaPago = Me.Txt_Fec_Rec.Text
                .DiasRetencionPago = 0 'Hay que ir a buscarlo por tipo de documento
                .TasaInteresCalculo = CDbl(Me.Txt_tasa.Text)
                '.DiasDevolverInteres = 0  'Buscar en tabla SIS 
                .DiasDevolverInteres = REC.hre_DiasInt_devuelve()

            End With
            NroHojaRuta = nro_hoja.Value
        Else

            If UCase(Me.Txt_Dig_Deu.Text) <> fcc.Vrut(Me.Txt_Rut_Deu.Text) Then
                msj.Mensaje(Me.Page, "Atención", "Digito Verificador Incorrecto", 3)
                Exit Sub

            End If
            Dim deu As deu_cls

            deu = cg.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

            Session("Deudor") = deu

            If Not IsNothing(deu) Then
                'Datos Deudor
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.ReadOnly = True
                Me.Txt_Rso_Deu.CssClass = "clsDisabled"
                If deu.id_P_0044 = 1 Then
                    Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
                Else
                    Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso)
                End If

                Me.Txt_Rso_Deu.ReadOnly = True
                Me.Rb_cli_deu.Enabled = False
                Dim Pagos As New ClsSession.SesionPagos

                With Pagos

                    .IniciarSesionPagos()


                    .RutCliente = 0



                    .RutDeudor = Val(Txt_Rut_Deu.Text.Replace(".", ""))
                    .Pagador = "D"

                    .FechaPago = Me.Txt_Fec_Rec.Text
                    .DiasRetencionPago = 0 'Hay que ir a buscarlo por tipo de documento
                    .TasaInteresCalculo = CDbl(Me.Txt_tasa.Text)
                    '.DiasDevolverInteres = 0    'Buscar en tabla SIS
                    .DiasDevolverInteres = REC.hre_DiasInt_devuelve()

                End With

            Else

                msj.Mensaje(Me.Page, "Atención", "Este Pagador no Existe", 3)
            End If




            Dim msje As Boolean

            msje = REC.hoja_recauda_valida_deudor(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), Val(nro_hoja.Value))

            If msje = False Then
                caso = 3
                msj.Mensaje(Me.Page, "Atención", "Pagador no pertenece a la hoja de Ruta ", 2)
                activa_desactiva_datos_docto("H")

            Else

                NroHojaRuta = nro_hoja.Value
                activa_desactiva_datos_docto("H")

            End If

        End If



    End Sub

    Protected Sub Rb_cli_deu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_cli_deu.SelectedIndexChanged
        If Me.Rb_cli_deu.SelectedValue = "C" Then

            lbl_cli_deu.Text = "Cliente"
            Me.dr_pgdr.SelectedIndex = 1
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayu_deu.Visible = False
            Me.Ib_ayuda_cli.Visible = True
            Ib_ayuda_cli.Enabled = True

        Else
            lbl_cli_deu.Text = "Pagador"
            Me.dr_pgdr.SelectedIndex = 2
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayu_deu.Visible = True
            Me.Ib_ayuda_cli.Visible = False
            Ib_ayu_deu.Enabled = True
        End If
    End Sub

    Protected Sub btn_ok_gr_pgo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ok_gr_pgo.Click
        Try

            If IsNothing(Coll_DPO) Then

                Coll_DPO = New Collection
            End If

            'Valida Moneda
            If Me.Dr_mon_rec.SelectedIndex = 0 Then
                msj.Mensaje(Me.Page, "Atención", "Debe Ingresar Moneda", 2)
                Dr_mon_rec.Focus()
                Exit Sub
            End If

            'Valida Forma de Pago
            If Dr_for_pgo.SelectedIndex = 0 Then
                msj.Mensaje(Me.Page, "Atención", "Debe Ingresar Toda la Información", 2)

                Dr_for_pgo.Focus()
                Exit Sub
            End If

            Dim DPO As New dpo_cls

            If Txt_NroDocto.Text <> "" Then
                DPO.dpo_num = Txt_NroDocto.Text
            End If

            DPO.id_P_0054 = Dr_for_pgo.SelectedValue
            DPO.id_P_0023 = Dr_mon_rec.SelectedValue
            DPO.dpo_mto = CDbl(txt_mto_pgo.Text)
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

            Me.txt_cant_pgo.Text = Coll_DPO.Count
            If Me.Dr_mon_rec.SelectedValue <> 1 Then

                Me.txt_tot_dcto_pag.Text = CDbl(Me.txt_tot_dcto_pag.Text) + CDbl(Me.txt_mto_pgo.Text * cg.ParidadDevuelve(Me.Dr_mon_rec.SelectedValue, Me.Txt_Fec_Rec.Text).par_val)

            Else
                Me.txt_tot_dcto_pag.Text = CDbl(Me.txt_tot_dcto_pag.Text) + CDbl(Me.txt_mto_pgo.Text)

            End If

            gr_recau.DataSource = Coll_DPO
            gr_recau.DataBind()


            FormatoGrillaDPO()
            LimpiaDatosBanco()
            LimpiaDPO()



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_canc_gr_pgo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_canc_gr_pgo.Click
        Try

            Coll_DPO.Remove(CInt(HF_Pos_DPO.Value) + 1)

            gr_recau.DataSource = Coll_DPO
            gr_recau.DataBind()

            FormatoGrillaDPO()

        Catch ex As Exception

        End Try
    End Sub

    Private Function ValidaPagosConIngresos() As Boolean

        Dim Pagos As New ClsSession.SesionPagos
        Dim Total_a_Pagar As Double = 0

        If txt_tot_dcto_pag.Text = "" Then
            msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe Modificar Total a Pagar en Doctos. o CXC Total a Cancelar es Distinto a Total a Cobrar", TipoDeMensaje._Exclamacion)
            Return False
        End If

        If txt_tot_rec.Text = "" Then
            msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe Modificar Total a Pagar en Doctos. o CXC Total a Cancelar es Distinto a Total a Cobrar", TipoDeMensaje._Exclamacion)
            Return False
        End If

        Total_a_Pagar = CDbl(txt_tot_dcto_pag.Text.Replace(".", ""))

        If Total_a_Pagar <> Pagos.TotalRecaudado Then
            msj.Mensaje(Me.Page, "Seleccion de Documentos", "Debe Modificar Total a Pagar en Doctos. o CXC Total a Cancelar es Distinto a Total a Cobrar", TipoDeMensaje._Exclamacion)
            Return False
        End If

        Return True

    End Function


    Protected Sub gr_doctos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_doctos.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_gr_doctos, 'selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_gr_doctos, 'formatable')")
        '    e.Row.Attributes.Add("onClick", "SeleccionaDoctos(ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_gr_doctos, 'clicktable', 'formatable', 'selectable');")
        'End If

    End Sub

    Protected Sub LB_NUE_HOJA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_NUE_HOJA.Click
        Me.nro_hoja.Value = REC.hoja_recauda_nueva_valida(Dr_Rec.SelectedValue, Me.Txt_Fec_Rec.Text, Me.rb_hora.SelectedValue)
        activa_desactiva_datos_cli_deu("H")
        activa_desactiva_datos_Criterio("I")
        Me.Txt_Rut_Deu.Focus()
    End Sub

    Protected Sub btn_pza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_pza.Click
        RW.AbrePopup(Me, 2, "pop_up_plaza.aspx", "Plazas", 880, 500, 0, 0)
    End Sub

    Protected Sub Txt_tasa_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_tasa.TextChanged

        If Me.Txt_tasa.Text <> "" Then
            TasaInteresCalculo = CDbl(Me.Txt_tasa.Text)
            CalculaInteres()
        End If
        MARCAGRILLA()
    End Sub

    Protected Sub dr_pgdr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_pgdr.SelectedIndexChanged
        If Me.dr_pgdr.SelectedValue <> 0 Then
            'pagos.Rec = "S" SH
            If Me.dr_pgdr.SelectedValue = 1 Then
                pagos.Pagador = "C"
            Else
                pagos.Pagador = "D"
            End If
        End If

    End Sub

    Protected Sub LB_Dias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Dias.Click
        'UpdatePanel5.Update()
        Txt_Pza.Text = HF_Dias.Value
    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_recau.Rows.Count - 1

            If (btn.ToolTip = CType(gr_recau.Rows(i).FindControl("Btn_ver"), ImageButton).ToolTip) Then

                HF_Pos_DPO.Value = i

                If (i Mod 2) = 0 Then
                    gr_recau.Rows(i).CssClass = "selectable"
                Else
                    gr_recau.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_recau.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_recau.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

    Protected Sub Dr_for_pgo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_for_pgo.SelectedIndexChanged
        Try

            Dim TipoIngreso As p_0054_cls
            Dim Coll As Collection


            Coll = cg.Parametros_Detalle_Devuelve(TablaParametro.TipoIngreso, Dr_for_pgo.SelectedValue)

            If Coll.Count > 0 Then
                TipoIngreso = Coll.Item(1)
            End If


            If TipoIngreso.pnu_atr_003 = "S" Then
                BloqueaDatosBanco(False)
                MP_DoctoPago.Show()
            End If


            'txt_mto_rec.Focus()

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Metodos"

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

                Dim DOC As doc_cls = op.DocumentoOtorgagoDevuelvePorId(Coll_Doctos_Seleccionados.Item(i).id_doc)
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

                Interes = Formulas.RetornaCalculoInteres(pagos.FechaPago, _
                                                         pagos.DiasRetencionPago, _
                                                         pagos.TasaInteresCalculo, _
                                                         MtoAPagar, _
                                                         FechaSimula, _
                                                         FechaVctoRea, _
                                                         CantidadDias, _
                                                         Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli, _
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


                Coll_Doctos_Seleccionados.Item(i).Tasa = pagos.TasaInteresCalculo

                If Pagador = "D" Then

                    If Interes > 0 Then


                        If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMCD)
                            ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        Else
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMSD)
                            ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        End If

                    Else

                        If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMCD)
                            '  Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMSD)

                        Else
                            Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMSD)
                            'Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(MtoAPagar, Fmt.FCMCD)
                        End If

                    End If

                Else

                    If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                        Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMCD)
                        'Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(Interes + MtoAPagar, Fmt.FCMSD)
                    Else
                        Coll_Doctos_Seleccionados.Item(i).Interes = Format(Interes, frm.FCMSD)
                        ' Coll_Doctos_Seleccionados.Item(i).MontoPagar = Format(Interes + MtoAPagar, Fmt.FCMSD)
                    End If

                End If

                For X = 1 To coll_dsi_simu.Count
                    If coll_dsi_simu.Item(X).N_DOCTO = Coll_Doctos_Seleccionados.Item(i).dsi_num Then
                        coll_dsi_simu.Item(X).INTERES = Coll_Doctos_Seleccionados.Item(i).Interes
                        Exit For
                    End If
                Next
            Next
            Me.gr_doctos.DataSource = coll_dsi_simu
            Me.gr_doctos.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub activa_desactiva_datos_cli_deu(ByVal accion As String)

        If accion = "I" Then

            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.Text = ""
            Me.Rb_cli_deu.Enabled = False
            Me.Txt_Rso_Deu.Text = ""
            Ib_ayu_deu.Enabled = False
            Ib_ayuda_cli.Enabled = True

        Else

            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.ReadOnly = False
            Me.Rb_cli_deu.Enabled = True
            Ib_ayu_deu.Enabled = True
            Ib_ayuda_cli.Enabled = False
        End If

    End Sub

    Public Sub activa_desactiva_datos_docto(ByVal accion As String)

        If accion = "I" Then

            Me.txt_mto_pgo.ReadOnly = True
            Me.txt_mto_pgo.Text = ""
            Me.txt_mto_pgo.CssClass = "clsDisabled"


            Dr_mon_rec.Enabled = False
            Dr_mon_rec.CssClass = "clsDisabled"

            Dr_for_pgo.Enabled = False
            Dr_for_pgo.CssClass = "clsDisabled"


            btn_ok_gr_pgo.Enabled = False
            btn_canc_gr_pgo.Enabled = False


            Me.btn_doc_deu.Enabled = False
            Me.btn_doc_nce.Enabled = False
            Me.btn_no_rec.Enabled = False

            Me.dr_pgdr.CssClass = "clsDisabled"
            Me.dr_pgdr.Enabled = False

            Me.gr_doctos.DataSource = Nothing
            Me.gr_doctos.DataBind()

            Me.gr_recau.DataSource = Nothing
            Me.gr_recau.DataBind()

            coll_dsi_simu = New Collection
            Coll_DPO = New Collection
            Me.Ib_ayuda_cli.Visible = False
            Me.Rb_cli_deu.SelectedValue = "D"
        Else



            Me.txt_mto_pgo.Text = ""
            Me.txt_mto_pgo.ReadOnly = False


            Me.txt_mto_pgo.CssClass = "clsMandatorio"

            Dr_mon_rec.Enabled = True
            Dr_mon_rec.CssClass = "clsMandatorio"
            Dr_for_pgo.Enabled = True
            Dr_for_pgo.CssClass = "clsMandatorio"
            Me.txt_mto_pgo.CssClass = "clsMandatorio"
            Me.txt_mto_pgo.ReadOnly = False

            btn_ok_gr_pgo.Enabled = True
            btn_canc_gr_pgo.Enabled = True


            Me.btn_doc_deu.Enabled = True
            Me.btn_doc_nce.Enabled = True

            Me.dr_pgdr.CssClass = "clsMandatorio"
            Me.dr_pgdr.Enabled = True


        End If

    End Sub

    Public Sub alinea_textos()

        Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Pza.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_can_doc_pag.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_cant_pgo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_tot_dcto_pag.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_tot_rec.Attributes.Add("Style", "TEXT-ALIGN: right")


    End Sub

    Public Sub activa_desactiva_datos_Criterio(ByVal accion As String)

        If accion = "I" Then

            Me.Dr_Rec.Enabled = False
            Me.Dr_Rec.CssClass = "clsDisabled"
            Me.Txt_Fec_Rec.CssClass = "clsDisabled"
            Me.Txt_Fec_Rec.ReadOnly = True
            Me.rb_hora.Enabled = False
            Me.Btn_Buscar.Enabled = False
            Me.Btn_gua_rec.Enabled = True


        Else


            Me.Dr_Rec.Enabled = True
            Me.Dr_Rec.CssClass = "clsMandatorio"
            Me.Txt_Fec_Rec.CssClass = "clsMandatorio"
            Me.Txt_Fec_Rec.ReadOnly = False
            Me.rb_hora.Enabled = True
            Me.Btn_Buscar.Enabled = True
            Me.Btn_gua_rec.Enabled = False
            
        End If

    End Sub

    Private Sub LimpiaDPO()
        Dr_mon_rec.ClearSelection()
        Dr_for_pgo.ClearSelection()
        txt_mto_pgo.Text = ""
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

    Private Sub FormatoGrillaDPO()

        Dim SumaRec As Double = 0
        Dim Pagos As New ClsSession.SesionPagos

        For I = 0 To gr_recau.Rows.Count - 1

            Dim Formato As String = ""
            Dim ValorMoneda As Double

            CType(gr_recau.Rows(I).FindControl("Btn_ver"), ImageButton).ToolTip = I + 1

            For x = 0 To Dr_for_pgo.Items.Count

                If gr_recau.Rows(I).Cells(1).Text = Dr_for_pgo.Items(x).Value Then
                    gr_recau.Rows(I).Cells(1).Text = Dr_for_pgo.Items(x).Text
                    Exit For
                End If
            Next

            For x = 1 To Dr_mon_rec.Items.Count

                If gr_recau.Rows(I).Cells(3).Text = Dr_mon_rec.Items(x).Value Then

                    gr_recau.Rows(I).Cells(3).Text = Dr_mon_rec.Items(x).Text

                    Select Case Dr_mon_rec.Items(x).Value
                        Case 1 : Formato = fmts.FCMSD : ValorMoneda = 1
                        Case 2 : Formato = fmts.FCMCD4 : ValorMoneda = VALOR_UF
                        Case 3, 4 : Formato = fmts.FCMCD : ValorMoneda = Pagos.DollarCobranza
                    End Select

                    Exit For

                End If

            Next

            gr_recau.Rows(I).Cells(4).Text = Format(CDbl(gr_recau.Rows(I).Cells(4).Text), Formato)

            SumaRec = SumaRec + (CDbl(gr_recau.Rows(I).Cells(4).Text) * ValorMoneda)

        Next

        Me.txt_tot_dcto_pag.Text = Format(SumaRec, fmts.FCMSD)
        Me.txt_cant_pgo.Text = Me.gr_recau.Rows.Count
        Pagos.TotalRecaudado = SumaRec

    End Sub

    Public Function Existe_docto_en_Grilla(ByVal T_DOCTO As Integer, ByVal N_DOCTO As String, ByVal NRO_CUOTA As Integer, ByVal R_CLIENTE As String, ByVal N_CLIENTE As String) As Boolean

        For i = 0 To Me.gr_doctos.Rows.Count - 1

            If coll_dsi_simu.Item(i + 1).T_DOCTO = T_DOCTO _
            And coll_dsi_simu.Item(i + 1).N_DOCTO = N_DOCTO _
            And coll_dsi_simu.Item(i + 1).NRO_CUOTA = NRO_CUOTA _
            And coll_dsi_simu.Item(i + 1).R_CLIENTE = R_CLIENTE Then

                Return True

            End If

        Next

        Return False
    End Function

    Public Sub MARCAGRILLA()

        For i = 0 To Me.gr_doctos.Rows.Count - 1

            If coll_dsi_simu.Item(i + 1).TIPO_DOC_REC = "C" Then

                Dim COLOR As New System.Drawing.Color

                COLOR = System.Drawing.ColorTranslator.FromHtml("#FB9FA6")

                Me.gr_doctos.Rows(i).BackColor = COLOR

            End If

            Me.gr_doctos.Rows(i).Cells(4).Text = Format(CLng(Me.gr_doctos.Rows(i).Cells(4).Text), "###,###,###") & "-" & fcc.Vrut(Me.gr_doctos.Rows(i).Cells(4).Text)
            Me.gr_doctos.Rows(i).Cells(6).Text = Format(CLng(Me.gr_doctos.Rows(i).Cells(6).Text), "###,###,###") & "-" & fcc.Vrut(Me.gr_doctos.Rows(i).Cells(6).Text)
        Next

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
            Ing_Sec.ing_qpa = Pagador
            Ing_Sec.ing_vld_rcz = CChar("I")

            If Tipo = 1 Then

                ' 1.- CUENTAS POR COBRAR
                Ing_Sec.id_P_0053 = 1

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = DollarObservador
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
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                '/*Calcula todo como Pago Pagador***************************************************/
                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli + Objeto.dsi_mto - Objeto.dsi_mto_ant)
                EXCEDENTE = Formulas.MAX(Formulas.MIN(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli, Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)
                MAYOR_PAGO = Formulas.MAX(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli - (Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)

                ''/*Calculo de Reajuste **************************************************/
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
            Ing_Sec.cli_idc = Objeto.CLI_IDC
            Ing_Sec.ing_tas_apl = Objeto.TASA

            If Ing_Sec.doc_sdo_cli = 0 Then
                Ing_Sec.ing_tot_par = "T"
            Else
                Ing_Sec.ing_tot_par = "P"
            End If

            'Crea la collection con los objetos para que luego sean grabados
            Coll_Ing_Sec.Add(Ing_Sec)


        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Carga de Colecciones para Grilla "

    Protected Sub LB_NCE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_NCE.Click

        Try


            If Not IsNothing(coll_dsi_simu) Then


                If coll_dsi_simu.Contains(coll_nce.Item(1).N_DOCTO) = False Then

                    For i = 1 To coll_dsi_simu.Count

                        If coll_dsi_simu.Item(i).n_docto = coll_nce.Item(1).n_docto _
                        And coll_dsi_simu.Item(i).t_docto = coll_nce.Item(1).t_docto _
                        And coll_dsi_simu.Item(i).rut_deudor = coll_nce.Item(1).rut_deudor Then
                            msj.Mensaje(Me.Page, "Atención", "Documento ya existe en la Lista", 3)
                            Exit Sub
                        End If

                    Next

                    coll_dsi_simu.Add(coll_nce.Item(1))

                    Me.gr_doctos.DataSource = coll_dsi_simu
                    Me.gr_doctos.DataBind()
                    MARCAGRILLA()


                    If coll_nce.Item(1).T_MONEDA <> 1 Then
                        Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(coll_nce.Item(1).MTO_A_RECAUDAR * cg.ParidadDevuelve(coll_nce.Item(1).T_MONEDA, Me.Txt_Fec_Rec.Text).par_val)
                    Else
                        Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(coll_nce.Item(1).MTO_A_RECAUDAR)
                    End If

                    Me.txt_tot_rec.Text = Format(CDbl(Me.txt_tot_rec.Text), fmts.FCMSD)
                    Me.txt_can_doc_pag.Text = coll_dsi_simu.Count
                End If

            Else
                'coll_dsi_simu = New Collection
                'coll_nce.Item(1).mto_a_recaudar = Me.txt_tot.Text
                'coll_nce.Item(1).mto_recaudado = Me.txt_mto_rec.Text
                'coll_dsi_simu.Add(coll_nce.Item(1))

                Me.gr_doctos.DataSource = coll_dsi_simu
                Me.gr_doctos.DataBind()
                MARCAGRILLA()

                If coll_nce.Item(1).T_MONEDA <> 1 Then
                    Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(coll_nce.Item(1).MTO_A_RECAUDAR * cg.ParidadDevuelve(coll_nce.Item(1).T_MONEDA, Me.Txt_Fec_Rec.Text).par_val)
                Else
                    Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(coll_nce.Item(1).MTO_A_RECAUDAR)
                End If


                Me.txt_can_doc_pag.Text = coll_dsi_simu.Count
            End If
            activa_desactiva_datos_docto("H")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub carga_doctos_wc()
        Try

            If Not IsNothing(coll_dsi_simu) Then
                Dim array As New ArrayList
                If coll_dsi_simu.Count > 0 Then
                    Dim col_aux As New Collection
                    Me.txt_tot_rec.Text = 0
                    For i = 1 To coll_dsi_simu.Count

                        If coll_dsi_simu.Item(i).D_CEDIDO = "N" Then '

                            col_aux.Add(coll_dsi_simu.Item(i))
                            Me.txt_tot_rec.Text = CDbl(Me.txt_tot_rec.Text) + coll_dsi_simu.Item(i).MTO_A_RECAUDAR

                        End If

                    Next
                    coll_dsi_simu = New Collection
                    coll_dsi_simu = col_aux
                    gr_doctos.DataSource = coll_dsi_simu
                    gr_doctos.DataBind()
                    MARCAGRILLA()
                    Me.txt_can_doc_pag.Text = coll_dsi_simu.Count
                End If


                If IsNothing(Coll_Doctos_Seleccionados) = False Then

                    If Coll_Doctos_Seleccionados.Count > 0 Then
                        'Validación Aplicara mas adelante

                        ''If coll_dsi_simu.Count > 0 Then


                        ''    For i = 1 To coll_dsi_simu.Count
                        ''        If coll_dsi_simu.Item(i).D_CEDIDO = "S" Then
                        ''            coll_dsi_simu.Remove(i)
                        ''        End If
                        ''    Next

                        ''End If
                        Dim obj As New obj_rec
                        For i = 1 To Coll_Doctos_Seleccionados.Count


                            obj = New obj_rec

                            With obj
                                obj.CONTRATO = Coll_Doctos_Seleccionados.Item(i).Contrato
                                obj.NOMBRE_DEUDOR = Coll_Doctos_Seleccionados.Item(i).deudor '1
                                obj.RUT_DEUDOR = Coll_Doctos_Seleccionados.Item(i).deu_ide  '2 
                                obj.T_DOCTO = Coll_Doctos_Seleccionados.Item(i).id_p_0031    '3 
                                obj.N_DOCTO = Coll_Doctos_Seleccionados.Item(i).dsi_num         '4 
                                obj.RUT_CLI = Coll_Doctos_Seleccionados.Item(i).cli_idc            '5
                                obj.N_CLIENTE = Coll_Doctos_Seleccionados.Item(i).cliente          '6
                                obj.N_OPERACION = Coll_Doctos_Seleccionados.Item(i).id_opo      '7
                                obj.D_CEDIDO = "S" '8
                                obj.S_DEUDOR = Coll_Doctos_Seleccionados.Item(i).doc_sdo_ddr '9
                                obj.S_CLIENTE = Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli '10
                                obj.T_MONEDA = Coll_Doctos_Seleccionados.Item(i).ID_P_0023    '11
                                obj.D_MONEDA = Coll_Doctos_Seleccionados.Item(i).MONEDA       '12
                                obj.MTO_ANTICIPO = Coll_Doctos_Seleccionados.Item(i).dsi_mto_ant '13
                                obj.DES_TIP_DOC = Coll_Doctos_Seleccionados.Item(i).TipoDoctoCorta


                                If Not IsNothing(Coll_Doctos_Seleccionados.Item(i).doc_ful_pgo) Then
                                    obj.F_VCTO = Format(Coll_Doctos_Seleccionados.Item(i).doc_ful_pgo, "dd/MM/yyyy")
                                Else
                                    obj.F_VCTO = Format(Coll_Doctos_Seleccionados.Item(i).ope_fec_sim, "dd/MM/yyyy") '14
                                End If
                                obj.E_DOCTO = Coll_Doctos_Seleccionados.Item(i).EstadoDocto                         '15
                                obj.DOC_MTO = Coll_Doctos_Seleccionados.Item(i).dsi_mto                             '16
                                obj.FEC_VCTO = Coll_Doctos_Seleccionados.Item(i).fec_ven                        '17
                                obj.FECHA_SIM = Coll_Doctos_Seleccionados.Item(i).ope_fec_sim                       '18
                                obj.DIF_PRECIO = Coll_Doctos_Seleccionados.Item(i).ope_dif_pre                      '19
                                obj.CANT_DIAS = Coll_Doctos_Seleccionados.Item(i).dsi_ctd_dia                       '20
                                obj.TASA_MOA = Coll_Doctos_Seleccionados.Item(i).opn_tas_moa                        '21
                                obj.OPER_LNL = Coll_Doctos_Seleccionados.Item(i).ope_lnl                            '22
                                obj.TASA_REN = Coll_Doctos_Seleccionados.Item(i).doc_tas_ren                        '23
                                obj.FVTO_ORI = Coll_Doctos_Seleccionados.Item(i).dsi_fev_ori                        '24
                                obj.NRO_REN = Coll_Doctos_Seleccionados.Item(i).doc_num_ren                         '25
                                obj.MTO_RECAUDADO = Coll_Doctos_Seleccionados.Item(i).MontoPagar                    '26
                                obj.INTERES = Coll_Doctos_Seleccionados.Item(i).INTERES                             '27
                                obj.NOTA_CRED = Coll_Doctos_Seleccionados.Item(i).nota_cred                         '28

                                obj.MTO_A_RECAUDAR = Coll_Doctos_Seleccionados.Item(i).MontoPagar

                                '   obj.MTO_A_RECAUDAR = Coll_Doctos_Seleccionados.Item(i).MontoPagar + Coll_Doctos_Seleccionados.Item(i).INTERES               '29

                                If Me.dr_pgdr.SelectedValue = 2 Then
                                    If Coll_Doctos_Seleccionados.Item(i).MontoPagar >= Coll_Doctos_Seleccionados.Item(i).doc_sdo_ddr Then
                                        obj.TIPO_PAGO = "T"
                                    Else
                                        obj.TIPO_PAGO = "P"
                                    End If

                                End If
                                If Me.dr_pgdr.SelectedValue = 1 Then

                                    If Coll_Doctos_Seleccionados.Item(i).MontoPagar >= (Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli + Coll_Doctos_Seleccionados.Item(i).interes) Then
                                        obj.TIPO_PAGO = "T"
                                    Else
                                        obj.TIPO_PAGO = "P"
                                    End If

                                End If

                                obj.TIPO_DOC_REC = "D"


                            End With
                            If Existe_docto_en_Grilla(obj.T_DOCTO, obj.N_DOCTO, obj.NRO_CUOTA, obj.R_CLIENTE, obj.N_CLIENTE) = False Then
                                coll_dsi_simu.Add(obj)
                                If obj.T_MONEDA <> 1 Then
                                    Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(obj.MTO_A_RECAUDAR * cg.ParidadDevuelve(obj.T_MONEDA, Me.Txt_Fec_Rec.Text).par_val)
                                Else
                                    Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(obj.MTO_A_RECAUDAR)
                                End If
                            End If



                        Next

                        'For x = 1 To Coll_Doctos_Seleccionados.Count



                        '    For i = 1 To coll_dsi_simu.Count

                        '        If coll_dsi_simu.Item(i).n_docto = Coll_Doctos_Seleccionados.Item(x).dsi_num _
                        '        And coll_dsi_simu.Item(i).t_docto = Coll_Doctos_Seleccionados.Item(x).id_p_0031 _
                        '        And coll_dsi_simu.Item(i).rut_deudor = Coll_Doctos_Seleccionados.Item(x).deu_ide Then

                        '            msj.Mensaje(Me.Page, "Atención", "Documento ya existe en la Lista", 3)
                        '            Exit Sub
                        '        End If

                        '    Next

                        'Next


                        Me.gr_doctos.DataSource = coll_dsi_simu
                        Me.gr_doctos.DataBind()


                        MARCAGRILLA()




                        Me.txt_can_doc_pag.Text = coll_dsi_simu.Count
                    End If
                End If
            Else

                ' Si no tiene elementos Instancia la Coleccion 


                coll_dsi_simu = New Collection

                'Valida que vengan documentos seleccionados 

                If IsNothing(Coll_Doctos_Seleccionados) = False Then
                    Dim obj As New obj_rec
                    For i = 1 To Coll_Doctos_Seleccionados.Count


                        obj = New obj_rec

                        With obj


                            obj.NOMBRE_DEUDOR = Coll_Doctos_Seleccionados.Item(i).deudor '1
                            obj.RUT_DEUDOR = Coll_Doctos_Seleccionados.Item(i).deu_ide  '2 
                            obj.T_DOCTO = Coll_Doctos_Seleccionados.Item(i).id_p_0031    '3 
                            obj.N_DOCTO = Coll_Doctos_Seleccionados.Item(i).dsi_num         '4 
                            obj.RUT_CLI = Coll_Doctos_Seleccionados.Item(i).cli_idc            '5
                            obj.N_CLIENTE = Coll_Doctos_Seleccionados.Item(i).cliente          '6
                            obj.N_OPERACION = Coll_Doctos_Seleccionados.Item(i).id_opo      '7
                            obj.D_CEDIDO = "S" '8
                            obj.S_DEUDOR = Coll_Doctos_Seleccionados.Item(i).doc_sdo_ddr '9
                            obj.S_CLIENTE = Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli '10
                            obj.T_MONEDA = Coll_Doctos_Seleccionados.Item(i).ID_P_0023    '11
                            obj.D_MONEDA = Coll_Doctos_Seleccionados.Item(i).MONEDA       '12
                            obj.MTO_ANTICIPO = Coll_Doctos_Seleccionados.Item(i).dsi_mto_ant '13
                            ' obj.TS_APLICACION = TB_TASA.Text

                            If Not IsNothing(Coll_Doctos_Seleccionados.Item(i).doc_ful_pgo) Then
                                obj.F_VCTO = Format(Coll_Doctos_Seleccionados.Item(i).doc_ful_pgo, "dd/MM/yyyy")
                            Else
                                obj.F_VCTO = Format(Coll_Doctos_Seleccionados.Item(i).ope_fec_sim, "dd/MM/yyyy") '14
                            End If


                            obj.E_DOCTO = Coll_Doctos_Seleccionados.Item(i).EstadoDocto                         '15
                            obj.DOC_MTO = Coll_Doctos_Seleccionados.Item(i).dsi_mto                             '16
                            obj.FEC_VCTO = Coll_Doctos_Seleccionados.Item(i).fec_ven                       '17
                            obj.FECHA_SIM = Coll_Doctos_Seleccionados.Item(i).ope_fec_sim                       '18
                            obj.DIF_PRECIO = Coll_Doctos_Seleccionados.Item(i).ope_dif_pre                      '19
                            obj.CANT_DIAS = Coll_Doctos_Seleccionados.Item(i).dsi_ctd_dia                       '20
                            obj.TASA_MOA = Coll_Doctos_Seleccionados.Item(i).opn_tas_moa                        '21
                            obj.OPER_LNL = Coll_Doctos_Seleccionados.Item(i).ope_lnl                            '22
                            obj.TASA_REN = Coll_Doctos_Seleccionados.Item(i).doc_tas_ren                        '23
                            obj.FVTO_ORI = Coll_Doctos_Seleccionados.Item(i).dsi_fev_ori                        '24
                            obj.NRO_REN = Coll_Doctos_Seleccionados.Item(i).doc_num_ren                         '25
                            obj.MTO_RECAUDADO = Coll_Doctos_Seleccionados.Item(i).MontoPagar                    '26
                            obj.INTERES = Coll_Doctos_Seleccionados.Item(i).INTERES                             '27
                            obj.NOTA_CRED = Coll_Doctos_Seleccionados.Item(i).nota_cred                         '28
                            obj.MTO_A_RECAUDAR = Coll_Doctos_Seleccionados.Item(i).MontoPagar '29
                            obj.DES_TIP_DOC = Coll_Doctos_Seleccionados.Item(i).TipoDoctoCorta

                            If Me.dr_pgdr.SelectedValue = 2 Then
                                If Coll_Doctos_Seleccionados.Item(i).MontoPagar >= Coll_Doctos_Seleccionados.Item(i).doc_sdo_ddr Then
                                    obj.TIPO_PAGO = "T"
                                Else
                                    obj.TIPO_PAGO = "P"
                                End If

                            End If
                            If Me.dr_pgdr.SelectedValue = 1 Then

                                If Coll_Doctos_Seleccionados.Item(i).MontoPagar >= (Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli + Coll_Doctos_Seleccionados.Item(i).interes) Then
                                    obj.TIPO_PAGO = "T"
                                Else
                                    obj.TIPO_PAGO = "P"
                                End If

                            End If

                            obj.TIPO_DOC_REC = "D"
                            obj.ID_DOC = Coll_Doctos_Seleccionados.Item(i).id_doc

                        End With

                        If Existe_docto_en_Grilla(obj.T_DOCTO, obj.N_DOCTO, obj.NRO_CUOTA, obj.R_CLIENTE, obj.N_CLIENTE) = False Then
                            coll_dsi_simu.Add(obj)
                            If obj.T_MONEDA <> 1 Then
                                Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(obj.MTO_A_RECAUDAR * cg.ParidadDevuelve(obj.T_MONEDA, Me.Txt_Fec_Rec.Text).par_val)
                            Else
                                Me.txt_tot_rec.Text = CDbl(txt_tot_rec.Text) + CDbl(obj.MTO_A_RECAUDAR)
                            End If
                        End If

                    Next
                    coll_dsi_simu.Add(obj)
                    Me.gr_doctos.DataSource = coll_dsi_simu
                    Me.gr_doctos.DataBind()
                    MARCAGRILLA()



                    Me.txt_can_doc_pag.Text = coll_dsi_simu.Count
                End If

            End If
            SW_Rec = 0
        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub LB_DOC_DDR_CLICK(ByVal sender As Object, ByVal e As System.EventArgs)

        carga_doctos_wc()

    End Sub

#End Region

  
#Region "BOTONERA"

    Protected Sub Btn_gua_rec_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_gua_rec.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20020308, Usr, "PRESIONO BOTON GUARDAR HOJAS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'Generar Pagos
        Try

            If txt_can_doc_pag.Text = 0 Or Me.txt_cant_pgo.Text = 0 Then
                msj.Mensaje(Me.Page, "Atención", "Debe tener Documentos para pagar y generar sus documentos de Pago", 2)
                Exit Sub
            End If

            If CDbl(Me.txt_tot_dcto_pag.Text) <> CDbl(Me.txt_tot_rec.Text) Then
                msj.Mensaje(Me.Page, "Atención", "Monto a Pagar debe ser igual a monto de Pago", 2)
                Exit Sub
            Else

                If Not ValidaPagosConIngresos() Then Exit Sub



                Dim AG As New ActualizacionesGenerales
                Dim FG As New FormulasGenerales


                nro_hoja.Value = REC.hoja_recauda_nueva_valida(Dr_Rec.SelectedValue, Me.Txt_Fec_Rec.Text, Me.rb_hora.SelectedValue)
                'Crea el Ingreso
                Dim ING As New ing_cls

                ING.id_ing = Nothing
                '  ING.cli_idc = Format(Pagos.RutCliente, Var.FMT_RUT)
                ING.id_eje = CodEje
                ING.ing_obs = ""
                ING.ing_pgo_hre = CChar("S")
                ING.id_hre = nro_hoja.Value
                '  ING.ing_rec = CChar("S")
                ING.ing_fec = Txt_Fec_Rec.Text
                ING.ing_sis_fec = Date.Now
                'ING.ing_tas_apl = Txt_tasa.Text
                ING.ing_pgo_hre = CChar("S")

                '**********************************************************
                'Gestion
                '**********************************************************
                Dim gsn_ingreso As New gsn_cls
                Dim ddi As New ddi_cls
                Dim rst As Integer
                Dim cb_rec As Boolean
                Dim ch_pend As Integer
                If Not IsNothing(Coll_Doctos_Seleccionados) Then

                    If Coll_Doctos_Seleccionados.Count > 0 Then

                        For i = 1 To Coll_Doctos_Seleccionados.Count

                            gsn_ingreso.id_doc = Coll_Doctos_Seleccionados.Item(i).id_doc
                            gsn_ingreso.id_P_0011 = Coll_Doctos_Seleccionados.Item(i).id_P_0011
                            gsn_ingreso.doc_fev_rea = Coll_Doctos_Seleccionados.Item(i).doc_fev_rea
                            gsn_ingreso.doc_sdo_cli = Coll_Doctos_Seleccionados.Item(i).doc_sdo_cli
                            gsn_ingreso.doc_sdo_ddr = Coll_Doctos_Seleccionados.Item(i).doc_sdo_ddr

                            gsn_ingreso.id_eje_cob = CodEje
                            gsn_ingreso.id_cnc = Nothing
                            ' gsn_ingreso.id_cmn = DP_Comuna.SelectedValue
                            'gsn_ingreso.id_zon = IIf(CInt(txt_GESIdZona.Text) = 0, Nothing, CInt(txt_GESIdZona.Text))
                            ddi.id_cmn = Nothing
                            ddi.deu_ide = Coll_Doctos_Seleccionados.Item(i).DEU_IDE
                            ddi.ddr_dml_cbz = "S"
                            gsn_ingreso.id_cco = Nothing
                            gsn_ingreso.gsn_fec = Date.Now.ToShortDateString
                            gsn_ingreso.gsn_hor = Date.Now ' TimeOfDay

                            gsn_ingreso.gsn_fec_pag = CDate(Me.Txt_Fec_Rec.Text)
                            If gsn_ingreso.gsn_fec_pag Is Nothing Then
                                gsn_ingreso.gsn_hor_pag_dde = Nothing
                                gsn_ingreso.gsn_hor_pag = Nothing
                            Else
                                gsn_ingreso.gsn_hor_pag_dde = CDate(Me.Txt_Fec_Rec.Text)
                                gsn_ingreso.gsn_hor_pag = CDate(Me.Txt_Fec_Rec.Text)
                            End If

                            gsn_ingreso.gsn_tlf = ""
                            gsn_ingreso.gsn_fax = ""

                            gsn_ingreso.gsn_obs = ""
                            gsn_ingreso.gsn_obs_1 = ""
                            gsn_ingreso.gsn_obs_2 = ""

                            gsn_ingreso.gsn_doc_rtr_pag = Nothing

                            gsn_ingreso.gsn_fec_prx = Nothing
                            gsn_ingreso.gsn_hor_prx = Nothing

                            gsn_ingreso.gsn_dir_cbz = Nothing

                            If Me.RB_Banco.Checked = True Then
                                gsn_ingreso.gsn_alo = "B"
                            Else
                                gsn_ingreso.gsn_alo = "C"
                            End If

                            gsn_ingreso.gsn_alo_obs = Nothing
                            gsn_ingreso.gsn_con_hor = "N"
                            ch_pend = 0
                            cb_rec = True

                            rst = CBZ.Gestion_guarda(gsn_ingreso)

                            REC.Documento_a_recaudar_inserta(nro_hoja.Value, rst, Coll_Doctos_Seleccionados.Item(i).ID_DOC, Me.Txt_Fec_Rec.Text)

                        Next

                    End If
                    ' FG.CargaCollection_Ingresos_Rec(Coll_DPO, Coll_Doctos_Seleccionados, coll_dsi_simu, Txt_Fec_Rec.Text)
                End If

                'CreaObjetos()
                Dim coll_nce As New Collection
                For x = 1 To coll_dsi_simu.Count

                    If coll_dsi_simu.Item(x).TIPO_DOC_REC = "C" Then
                        coll_nce.Add(coll_dsi_simu.Item(x))
                    End If

                Next

                FG.CargaCollection_IngresosRec(Coll_DPO, Coll_Doctos_Seleccionados, coll_nce, 1, Txt_Fec_Rec.Text)

                If Coll_Ing_Sec.Count > 0 Then

                    Dim Id As Integer

                    Id = PGO.PagosInserta(Coll_DPO, ING, Coll_Ing_Sec)

                    If Id > 0 Then


                        msj.Mensaje(Me.Page, "Atención", "Se ha Generado el Pago", 2)
                        Dim RW As New FuncionesGenerales.RutinasWeb
                        RW.AbrePopup(Me, 1, "Reporte_Pago_rec.aspx?fecha=" & Me.Txt_Fec_Rec.Text & "&eje=" & Me.Dr_Rec.SelectedValue & "&hora=" & Me.rb_hora.SelectedValue & " ", "ReportePagos", 1100, 750, 0, 0)
                        Me.Btn_Limpiar_Click(Me, e)

                    Else
                        msj.Mensaje(Me.Page, "Atención", "No se pudo guardar pago", TipoDeMensaje._Exclamacion)
                    End If

                End If


            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Btn_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Imprimir.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20030308, Usr, "PRESIONO BOTON IMPRIMIR HOJAS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        RW.AbrePopup(Me, 2, "Reporte_Pago_rec.aspx?fecha=" & Me.Txt_Fec_Rec.Text & "&eje=" & Me.Dr_Rec.SelectedValue & "&hora=" & Me.rb_hora.SelectedValue & " ", "Reporte Pagos", 1200, 850, 0, 0)

    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Buscar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20010308, Usr, "PRESIONO BOTON BUSCAR HOJAS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim val As Integer

        val = REC.HOJA_REC_VALIDA(Me.Dr_Rec.SelectedValue, Me.Txt_Fec_Rec.Text, Me.rb_hora.SelectedValue)

        If val = 0 Then

            caso = 1
            msj.Mensaje(Me.Page, "Confirmación", "No existen datos para recaudar  , ¿Desea crearlos?", 4, LB_NUE_HOJA.UniqueID)

        Else

            activa_desactiva_datos_cli_deu("H")
            activa_desactiva_datos_Criterio("I")
            nro_hoja.Value = val
            Me.Txt_Rut_Deu.Focus()
            Me.Btn_Imprimir.Enabled = True

        End If


    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpiar.Click

        'Limpia colecciones
        coll_dsi_simu = New Collection
        Coll_DPO = New Collection
        Coll_DSI = New Collection
        coll_nce = New Collection
        Coll_Doctos_Seleccionados = New Collection
        Coll_Ing_Sec = New Collection
        Coll_Cobranza = New Collection

        activa_desactiva_datos_cli_deu("I")
        activa_desactiva_datos_docto("I")
        activa_desactiva_datos_Criterio("H")
        txt_can_doc_pag.Text = 0
        txt_tot_rec.Text = 0
        txt_cant_pgo.Text = 0
        txt_tot_dcto_pag.Text = 0
        Dr_Rec.ClearSelection()
        lbl_cli_deu.Text = "Pagador"
        Me.Ib_ayu_deu.Visible = True

        Dr_for_pgo.ClearSelection()
        Dr_mon_rec.ClearSelection()

        Me.Txt_Fec_Rec.Text = Format(CDate(Date.Now.ToShortDateString), "dd/MM/yyyy")

        LimpiaDatosBanco()

    End Sub

#End Region

End Class
