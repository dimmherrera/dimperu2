Imports ClsSession.ClsSession
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_anticipo

    Inherits System.Web.UI.Page

    Dim swant As Boolean = False
    Dim fc As New FuncionesGenerales.FComunes
    Dim VAR As New FuncionesGenerales.Variables
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim swmodal As New Boolean
    Dim validacion As Integer
    Dim guardar As Boolean
    Dim cg As New ConsultasGenerales
    Dim clasecli As New ClaseClientes
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CTA As New ClaseCuentas
    Dim session As New ClsSession.ClsSession
    Dim CMC As New CapaDatos.ClaseComercial


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            session.NroPaginacion = 0
            valida_cliente = ""
            Dim sesion As New ClsSession.ClsSession
            coll_nrd = New Collection
            Response.Expires = -1
            Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Me.Txt_Dig_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_mto.Attributes.Add("Style", "TEXT-ALIGN: right")
            'Asigna Còdigo de Usuario que entra al Sistema
            Me.Txt_Rut_Cli.Focus()
        End If
        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Public Sub LLENA_ANTICIPO()

        cg.ParametrosDevuelve(TablaParametro.TipoEgreso, True, Dr_For_Pgo)
    
        coll_nrd = clasecli.BancosDevuelvePorCliente(False, Me.Dr_Bco, Nothing, CLng(Me.Txt_Rut_Cli.Text))

        If Not IsNothing(coll_nrd) Then
            If coll_nrd.Count > 0 Then

                For i = 1 To coll_nrd.Count

                    coll_nrd.Item(i).banco = coll_nrd.Item(i).banco & "- Nº Cuenta  " & coll_nrd.Item(i).Cuenta_Corriente

                Next
            Else
                msj.Mensaje(Me, "Atención", "No se encontro información ", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

        End If

        Dim RG As New FuncionesGenerales.RutinasWeb
        RG.Llenar_Drop(coll_nrd, "Cuenta_Corriente", "Banco", Dr_Bco)


        cg.ParametrosDevuelve(23, True, Me.Dr_MON)
        Dim col As New Collection
        col = OP.CXC_AbonoAnticipo_Devuelve(CLng(Me.Txt_Rut_Cli.Text), 8)

        Me.Gr_popant.DataSource = col
        Me.Gr_popant.DataBind()

        For i = 0 To Me.Gr_popant.Rows.Count - 1

            If col.Item(i + 1).id_P_0023 = 1 Then

                Me.Gr_popant.Rows(i).Cells(4).Text = Format(CDbl(Gr_popant.Rows(i).Cells(4).Text), fmt.FCMSD)

            ElseIf col.Item(i + 1).id_P_0023 = 2 Then

                Me.Gr_popant.Rows(i).Cells(4).Text = Format(CDbl(Gr_popant.Rows(i).Cells(4).Text), fmt.FCMCD4)

            Else

                Me.Gr_popant.Rows(i).Cells(4).Text = Format(CDbl(Gr_popant.Rows(i).Cells(4).Text), fmt.FCMCD)

            End If



        Next
        Me.txt_Cta_cte.Enabled = True
        Me.txt_des.readonly = False
        Me.txt_mto.readonly = False
        Me.at_14.Enabled = True
        Me.txt_Cta_cte.Text = ""
        Me.txt_des.CssClass = "clsMandatorio"
        Me.txt_mto.CssClass = "clsMandatorio"

        Me.txt_des.Text = ""
        Me.txt_mto.Text = ""
        Me.at_14.Checked = False
        Me.Dr_Bco.SelectedValue = 0
        Me.Dr_Bco.CssClass = "clsMandatorio"
        Me.Dr_For_Pgo.CssClass = "clsMandatorio"
        Me.Dr_For_Pgo.SelectedValue = 0
        Me.Dr_MON.CssClass = "clsMandatorio"
        Me.Dr_MON.SelectedValue = 0
        Me.Dr_MON.Enabled = True
        Me.Dr_For_Pgo.Enabled = True
        Me.Dr_Bco.Enabled = True

        Me.btn_guardar.Enabled = True
        Me.btn_eli.Enabled = True
    End Sub

    Protected Sub Txt_tot_cxc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        swant = True
    End Sub

    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn_eli.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20030505, Usr, "PRESIONA BOTON ELIMINAR ANTICIPO") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.Txt_ItemOPE.Value = "" Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un anticipo para eliminar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        OP.CXC_AbonoAnticipo_Elimina(Me.Txt_Rut_Cli.Text, Me.Gr_popant.Rows(Val(Me.Txt_ItemOPE.Value) - 1).Cells(2).Text)

        msj.Mensaje(Me, "Atención", "Registro Eliminado", ClsMensaje.TipoDeMensaje._Informacion)

        LLENA_ANTICIPO()

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020505, Usr, "PRESIONA BOTON GUARDAR ANTICIPO") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.txt_des.Text = "" Then
            msj.Mensaje(Me, "Atención", "debes ingresar descripción", 2)
            Exit Sub
        End If

        If Me.txt_mto.Text = "" Then
            msj.Mensaje(Me, "Atención", "debes ingresar monto", 2)
            Exit Sub
        End If
        If Me.txt_mto.Text = 0 Then
            msj.Mensaje(Me, "Atención", "El monto debe ser mayor a 0", 2)
            Exit Sub
        End If
        If Me.Dr_For_Pgo.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "debes seleccionar forma de pago", 2)
            Exit Sub
        End If


        If Me.Dr_MON.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "debes seleccionar Moneda ", 2)
            Exit Sub
        End If

        If Me.Dr_For_Pgo.SelectedValue = 4 Or Me.Dr_For_Pgo.SelectedValue = 6 Then


            If Me.txt_Cta_cte.Text = "" Then
                msj.Mensaje(Me, "Atención", "debes ingresar cuenta corriente", 2)
                Exit Sub
            End If
            If CStr(Me.Dr_Bco.SelectedValue) = "0" Then
                msj.Mensaje(Me, "Atención", "debes seleccionar un banco", 2)
                Exit Sub
            End If

        End If

        ABONAR_ANTICIPO()

        msj.Mensaje(Me, "Atención", "Registro Guardado", ClsMensaje.TipoDeMensaje._Informacion)

        'LLENA_ANTICIPO()

    End Sub

    Public Sub ABONAR_ANTICIPO()

        Dim cxc As New cxc_cls
        Dim cxp As New cxp_cls
        Dim egr As New egr_cls
        Dim CXP_CXC As Integer
        Dim NRO_OPERACION As String
        Dim NRO_PAGO_EGRESO_SIN_GIRO As Long


        Dim VALOR As String
        Dim contrato As String
        VALOR = Me.txt_mto.Text
        NRO_OPERACION = 0

        contrato = CTA.NroDoctoDevuelve(CInt(txt_nro_doc.Text))

        'MONTO_CXP = RUTINAS.RETORNA_VALOR_MONEDA(CCur(MONTO_CXP), Val(F_OP02_00_00.GD_OPERACIONES.TextMatrix(F_OP02_00_00.GD_OPERACIONES.Row, 4)), 1, Format(F_OP02_00_00.MED_DDIARIOS.Text, FMT_FECHA))
        cxp.id_P_0041 = 24 '8 se cambia de abono por distribucion
        cxp.cxp_des = Trim(Me.txt_des.Text) '& " NRO.EGR." & "*" & NRO_PAGO_EGRESO_SIN_GIRO & "*"
        cxp.id_P_0057 = 3 ' Se genera en estado pagada
        '  cxp.id_ope = NRO_OPERACION
        cxp.cxp_fec = Format(Now, "dd/MM/yyyy")
        cxp.cxp_fec_ctb = Format(Now, "dd/MM/yyyy")
        CXP_CXC = 2
        cxp.cxp_mto = Format(CDbl(VALOR), fmt.FSMCD)
        cxp.id_P_0023 = Me.Dr_MON.SelectedValue
        cxp.cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), VAR.FMT_RUT)

        cxp.id_doc = contrato

        If cxp.id_P_0023 = 1 Then
            cxp.cxp_fac_cam = 1
        End If
        'Monto Cxc
        cxc.cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), VAR.FMT_RUT)
        cxc.id_P_0041 = 24 '8 se cambia de abono por distribucion
        cxc.cxc_des = Trim(Me.txt_des.Text) '& " NRO.EGR." & "*" & NRO_PAGO_EGRESO_SIN_GIRO & "*"
        cxc.id_P_0057 = 1
        cxc.cxc_fec = Format(Now, "dd/MM/yyyy")
        cxc.id_P_0023 = Me.Dr_MON.SelectedValue
        cxc.cxc_fec_ctb = Format(Now, "dd/MM/yyyy")
        CXP_CXC = 2
        cxc.cxc_mto = Format(CDbl(VALOR), fmt.FSMCD)
        cxc.cxc_sal = Format(CDbl(VALOR), fmt.FSMCD)
        cxc.id_doc = contrato

        Dim COLL_DIARIOS As New Collection

        COLL_DIARIOS = CMC.DatosDiariosDevuelve(Date.Now)
        If IsNothing(COLL_DIARIOS) = False Then
            If cxc.id_P_0023 = 1 Then
                cxc.cxc_fac_cam = 1
            ElseIf cxc.id_P_0023 = 2 Then
                cxc.cxc_fac_cam = COLL_DIARIOS.Item(1)
            ElseIf cxc.id_P_0023 = 3 Then
                cxc.cxc_fac_cam = COLL_DIARIOS.Item(2)
            End If

        End If

        If cxc.id_P_0023 = 1 Then
            cxc.cxc_fac_cam = 1
        End If
        '******* GRABA EGRESO ***********************************
        'Guarda Egreso Sin Giro
        egr.cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), VAR.FMT_RUT)

        Dim egr_sec As New egr_sec_cls

        egr_sec.egr_mto = Format(CDbl(VALOR), fmt.FSMCD)


        'TOTAL_ABONADO = RUTINAS.RETORNA_VALOR_MONEDA(CCur(TOTAL_ABONADO), Val(F_OP02_00_00.GD_OPERACIONES.TextMatrix(F_OP02_00_00.GD_OPERACIONES.Row, 4)), 1, Format(F_OP02_00_00.MED_DDIARIOS.Text, FMT_FECHA))
        egr_sec.id_P_0055 = 1 ' 4 se cambio de anticipo a cuenta por pagar
        egr_sec.id_P_0053 = 1
        egr_sec.id_P_0056 = Me.Dr_For_Pgo.SelectedValue
        egr_sec.egr_ent = "N"

        If Me.at_14.Checked Then
            egr_sec.egr_dep_ant = "S"
        Else
            egr_sec.egr_dep_ant = "N"
        End If
        egr.egr_fec = Date.Now.ToShortDateString
        egr.egr_obs = Trim(Me.txt_des.Text) '& "    NRO. EGR " & "*" & NRO_PAGO_EGRESO_SIN_GIRO & "*"
        If Dr_Bco.SelectedValue = 0 Then
            egr_sec.id_bco = Nothing
        Else
            egr_sec.id_bco = coll_nrd.Item(Me.Dr_Bco.SelectedIndex).codigo()
        End If

        egr_sec.egr_cta_cte = Trim(Me.txt_Cta_cte.Text)
        egr_sec.egr_vld_rcz = "V"
        egr_sec.egr_pro = "N"

        OP.abonoanticipo_guarda(cxc, cxp, egr, egr_sec)


        LLENA_ANTICIPO()



    End Sub

    Protected Sub Btn_Limpia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpia.Click
        limpia_controles(True)

        txt_Contrato.Text = ""
        IB_AyudaDoc.Enabled = False

    End Sub

    Public Sub limpia_controles(ByVal estado As Boolean)


        If estado Then

            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"

            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.ReadOnly = False
            Me.Txt_Dig_Cli.CssClass = "clsMandatorio"

            Me.Txt_Raz_Soc.Text = ""
            Me.txt_des.Text = ""
            Me.txt_des.CssClass = "clsDisabled"
            Me.txt_des.ReadOnly = True

            Me.txt_mto.CssClass = "clsDisabled"
            Me.txt_mto.Text = ""
            Me.txt_mto.ReadOnly = True

            Me.Dr_Bco.Enabled = False

            Me.Dr_Bco.Controls.Clear()
            Me.Dr_MON.Enabled = False
            Me.Dr_MON.CssClass = "clsDisabled"
            Me.Dr_MON.Controls.Clear()
            Me.Dr_For_Pgo.SelectedValue = 0
            Me.Dr_For_Pgo.CssClass = "clsDisabled"
            Me.Dr_For_Pgo.Enabled = False
            Me.Dr_Bco.CssClass = "clsDisabled"
            Me.Gr_popant.DataSource = Nothing
            Me.Gr_popant.DataBind()
            Me.Gr_popant.Controls.Clear()
            MaskedEditExtender1.Enabled = True

            Me.IB_AYUDACLI.ENABLED = True
            btn_eli.enabled = False
            btn_guardar.Enabled = False

            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Else

        End If
    End Sub

    Protected Sub Btn_Cerrar_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        swmodal = False
    End Sub

    Protected Sub Gr_popant_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_popant.RowDataBound
    End Sub

    Public Sub marcagrilla()

        For i = 0 To Gr_popant.Rows.Count - 1
            If (CInt(Txt_ItemOPE.Value) = CInt(Gr_popant.Rows(i).Cells(2).Text)) Then
                Txt_ItemOPE.Value = i + 1
                If (i Mod 2) = 0 Then
                    Gr_popant.Rows(i).CssClass = "selectable"
                Else
                    Gr_popant.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_popant.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_popant.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

    Protected Sub Okbutton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ' If caso = 1 Then
        guardar = True
        Me.ABONAR_ANTICIPO()
        'End If
    End Sub

    Protected Sub buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        busqueda()
    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010505, Usr, "PRESIONA BOTON BUSCAR") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        busqueda()
    End Sub

    Protected Sub RetornaDoctos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetornaDoctos.Click
        marcagrilla()
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img As ImageButton = CType(sender, ImageButton)

        Txt_ItemOPE.Value = img.ToolTip
        marcagrilla()

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        busqueda()
    End Sub

    Public Sub busqueda()

        If Txt_Rut_Cli.Text = "" Or Txt_Rut_Cli.Text = " __.___.___" Then
            msj.Mensaje(Me, "Atencion", "Ingrese NIT", ClsMensaje.TipoDeMensaje._Exclamacion)
            Txt_Dig_Cli.Text = ""
            Exit Sub
        End If

        Dim cli As cli_cls
        cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)


        If valida_cliente <> "" Then
            msj.Mensaje(Me, "Atencion", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If IsNothing(cli) Then
                msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If
            Me.IB_AyudaCli.Enabled = False
            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            MaskedEditExtender1.Enabled = False

            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)

        End If

        LLENA_ANTICIPO()

        IB_AyudaDoc.Enabled = True
        IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" + Txt_Rut_Cli.Text + "','PopUpCuentas Por Cobrar',1220,610,200,150);")

    End Sub

    Protected Sub Dr_Bco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_Bco.SelectedIndexChanged
        If CStr(Me.Dr_Bco.SelectedValue) <> "0" Then

            Me.txt_Cta_cte.Text = Dr_Bco.SelectedValue
        End If
    End Sub

    Protected Sub Dr_MON_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_MON.SelectedIndexChanged
        Try

            Select Case Dr_MON.SelectedValue
                Case 1
                    Txt_Rut_Cli_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_Rut_Cli_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4
                    Txt_Rut_Cli_MaskedEditExtender.Mask = "999,999,999.99"

            End Select
            txt_mto.Text = ""
            txt_mto.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_mto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_mto.TextChanged
        Try
            If Dr_MON.SelectedValue = 0 Then
                msj.Mensaje(Page, "Atención", "Seleccione moneda", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_mto.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 12 Then
            NroPaginacion -= 12
            LLENA_ANTICIPO()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If Gr_popant.Rows.Count < 12 Then
            msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
            Exit Sub
        End If
        If Gr_popant.Rows.Count = 12 Then
            NroPaginacion += 12
            LLENA_ANTICIPO()
        End If
    End Sub

    Protected Sub Dr_For_Pgo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_For_Pgo.SelectedIndexChanged
        If Me.Dr_For_Pgo.SelectedValue <> 4 Or Me.Dr_For_Pgo.SelectedValue <> 6 Then

            Me.txt_Cta_cte.CssClass = "clsTxt"
            Me.txt_Cta_cte.Text = ""

            Me.Dr_Bco.CssClass = "clsTxt"
            Me.Dr_Bco.SelectedValue = 0

        Else
            Me.txt_Cta_cte.CssClass = "clsMandatorio"
            Me.Dr_Bco.CssClass = "clsMandatorio"

        End If
    End Sub

End Class
