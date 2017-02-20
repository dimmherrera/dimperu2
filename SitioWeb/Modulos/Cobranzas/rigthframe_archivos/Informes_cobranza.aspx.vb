Imports ClsSession.ClsSession
Imports Microsoft.Reporting.WebForms
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_Informes_cobranza
    Inherits System.Web.UI.Page
    Dim fc As New FuncionesGenerales.FComunes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim sesion As New ClsSession.ClsSession
    Dim var As New FuncionesGenerales.Variables
    Dim fmoneda As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim rw As New FuncionesGenerales.RutinasWeb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            cg.ParametrosDevuelve(31, True, Me.Dr_tip_doc)
            cg.EjecutivosDevuelve(Dr_eje, CodEje, 29)
        End If

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.txt_cco_des0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_cco_has0.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.txt_mto_des0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_mto_has0.Attributes.Add("Style", "TEXT-ALIGN: right")

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
        Ib_ayuda_deu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged

        If Me.Ch_deu.Checked = True Then
            Me.txt_rut_deu.CssClass = "clsMandatorio"
            Me.txt_rut_deu.Enabled = True
            Me.txt_dig_deu.Enabled = True
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayuda_deu.Enabled = True
        Else
            Me.txt_rut_deu.CssClass = "clsDisabled"
            Me.txt_rut_deu.Enabled = False
            Me.txt_dig_deu.Enabled = False
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"

            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayuda_deu.Enabled = False
        End If


    End Sub



    Sub CargaCliente()

        Dim cli As cli_cls
        Dim CLSCLI As New ClaseClientes
        Dim rut As String


        rut = CLng(Me.Txt_Rut_Cli.Text)
        cli = CLSCLI.ClientesDevuelve(rut, Me.Txt_Dig_Cli.Text.ToUpper)

        If sesion.valida_cliente <> "" Then
            Msj.Mensaje(Me, "Atención", sesion.valida_cliente, 2)
        Else

            Session("Cliente") = cli

            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False
            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            Me.IB_Buscar.Enabled = True
        End If



    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        CargaCliente()
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        CargaDeudor()
    End Sub

    Sub CargaDeudor()

        Dim deu As deu_cls


        If Me.Txt_Rut_Deu.Text = "" Then
            Msj.Mensaje(Me.Page, "Atencion", "Debe ingresar NIT", 2)
            Exit Sub
        End If

        If Me.Txt_Dig_Deu.Text.ToUpper <> fc.Vrut(Me.Txt_Rut_Deu.Text).ToUpper Then
            Msj.Mensaje(Me.Page, "Atencion", "Digito Incorrecto", 2)
            Exit Sub
        End If


        deu = cg.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT))



        Session("Deudor") = deu

        If Not IsNothing(deu) Then
            'Datos Deudor


            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True
            Ib_ayuda_deu.Enabled = False
        End If
    End Sub


    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Dim suc_1, suc_2, eje_1, eje_2, tdc_1, tdc_2 As Integer
        Dim cli_1, cli_2, ddr_1, ddr_2, cco_1, cco_2 As String
        Dim mto1, mto2 As Double
        Dim fec_1, fec_2, fug_1, fug_2, fec_prc_1, fec_prc_2 As DateTime


        'Tipo de Informe

        If Me.Dr_tip_inf.SelectedValue = 0 Then
            Msj.Mensaje(Me, "Atención", "Debe seleccionar el tipo de Informe a Generar", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub

        End If

        'Fecha Proceso  

        If Me.txt_proc_des.Text = "" Then
            fec_prc_1 = "01/01/1900"
        Else
            fec_prc_1 = CDate(Me.txt_proc_des.Text)
        End If


        If Me.txt_proc_has.Text = "" Then
            fec_prc_2 = "31/12/2999"
        Else
            fec_prc_2 = CDate(Me.txt_proc_has.Text)
        End If


        'Codigos de Cobranza
        If Me.txt_cco_des0.Text = "" And Me.txt_cco_has0.Text = "" Then
            cco_1 = "0000"
            cco_2 = "9999"
        Else
            cco_1 = Format(CDbl(Me.txt_cco_des0.Text), "0000")
            cco_2 = Format(CDbl(Me.txt_cco_has0.Text), "0000")
        End If

        'Sucursal
        If Me.Ch_suc0.Checked Then
            suc_1 = 0
            suc_2 = 999
        Else
            suc_1 = Sucursal
            suc_2 = Sucursal
        End If

        'Ejecutivo 
        If Me.Dr_eje.SelectedValue <> 0 Then
            eje_1 = Me.Dr_eje.SelectedValue
            eje_2 = Me.Dr_eje.SelectedValue
        Else
            eje_1 = 0
            eje_2 = 999
        End If

        'Tipo de Documentos
        If Me.Dr_tip_doc.SelectedValue <> 0 Then
            tdc_1 = Me.Dr_tip_doc.SelectedValue
            tdc_2 = Dr_tip_doc.SelectedValue
        Else
            tdc_1 = 0
            tdc_2 = 999
        End If

        'Fecha Ultima Gestión

        If Me.txt_ult_ges_dde.Text <> "" Then

            If IsDate(Me.txt_ult_ges_dde.Text) Then
                fug_1 = Me.txt_ult_ges_dde.Text
            Else
                Msj.Mensaje(Me, "Atención", "Fecha Ultima Gestión desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Else
            fug_1 = "01/01/1900"
        End If

        'Hasta
        If Me.txt_ult_ges_hta.Text <> "" Then

            If IsDate(Me.txt_ult_ges_hta.Text) Then
                fug_2 = Me.txt_ult_ges_hta.Text
            Else
                Msj.Mensaje(Me, "Atención", "Fecha Ultima Gestión hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Else

            fug_2 = "31/12/2999"
        End If



        'Fecha Vencimiento

        'Desde
        If Me.txt_venc_des.Text <> "" Then

            If IsDate(Me.txt_venc_des.Text) Then
                fec_1 = Me.txt_venc_des.Text
            Else
                Msj.Mensaje(Me, "Atención", "Fecha vcto. desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Else
            fec_1 = "01/01/1900"
        End If

        'Hasta
        If Me.txt_venc_has.Text <> "" Then

            If IsDate(Me.txt_venc_has.Text) Then
                fec_2 = Me.txt_venc_has.Text
            Else
                Msj.Mensaje(Me, "Atención", "Fecha vcto. hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Else

            fec_2 = "31/12/2999"
        End If

        'Cliente
        If Me.Ch_cli.Checked = True Then
            If Me.Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar NIT del Cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If Me.Txt_Rut_Cli.Text <> "" Then
            cli_1 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
            cli_2 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
        Else
            cli_1 = "000000000000"
            cli_2 = "9999999999999"
        End If


        'Deudor 

        If Me.Ch_deu.Checked Then
            If Me.Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar NIT del Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If Me.Txt_Rut_Deu.Text <> "" Then
            ddr_1 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
            ddr_2 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
        Else
            ddr_1 = "000000000000"
            ddr_2 = "9999999999999"
        End If

        'Monto de Documento

        If Me.txt_mto_des0.Text <> "" Then
            mto1 = Me.txt_mto_des0.Text
        Else
            mto1 = 0
        End If

        If Me.txt_mto_has0.Text <> "" Then
            mto2 = Me.txt_mto_has0.Text
        Else
            mto2 = 9999999999
        End If

        'Dim cb_no_re As New DataSet_GestionCobranza.sp_co_gestiones_del_diaDataTable
        'Dim get_cb_no_re As New DataSet_GestionCobranzaTableAdapters.sp_co_gestiones_del_diaTableAdapter


        'Dim cb_310 As New DataSet_GestionCobranza.sp_co_dev_doc_con_codi_autDataTable
        'Dim get_310 As New DataSet_GestionCobranzaTableAdapters.sp_co_dev_doc_con_codi_autTableAdapter

        'Dim cb_400 As New DataSet_GestionCobranza.sp_co_mod_inf_310o400DataTable
        'Dim get_400 As New DataSet_GestionCobranzaTableAdapters.sp_co_mod_inf_310o400TableAdapter


        'Doc_Ope.Visible = False
        'cli_ddr.Visible = False
        'Info.Visible = False
        'ReportViewer1.Height = 600
        'Dim lr As New LocalReport
        'ReportViewer1.LocalReport.DataSources.Clear()

        'ReportViewer1.Reset()


        'If Me.Dr_tip_inf.SelectedValue = 1 Then

        '    lr.ReportPath = Server.MapPath("Reporte_gest_no.rdlc")
        '    ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_gest_no.rdlc"

        '    cb_no_re = get_cb_no_re.GetData(suc_1, suc_2, eje_1, eje_2, cli_1, cli_2, ddr_1, ddr_2, tdc_1, tdc_2, cco_1, cco_2, mto1, mto2, fec_1, fec_2, fug_1, fug_2)
        '    Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
        '    gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_gestiones_del_dia", cb_no_re)
        '    ReportViewer1.LocalReport.DataSources.Add(gest)

        '    ReportViewer1.DataBind()

        'ElseIf Me.Dr_tip_inf.SelectedValue = 2 Then

        '    lr.ReportPath = Server.MapPath("Reporte_100_400.rdlc")
        '    ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_100_400.rdlc"

        '    cb_400 = get_400.GetData(suc_1, suc_2, eje_1, eje_2, cli_1, cli_2, ddr_1, ddr_2, tdc_1, tdc_2, cco_1, cco_2, mto1, mto2, fec_1, fec_2, fug_1, fug_2)
        '    Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
        '    gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_mod_inf_310o400", cb_400)
        '    ReportViewer1.LocalReport.DataSources.Add(gest)

        '    ReportViewer1.DataBind()


        'ElseIf Me.Dr_tip_inf.SelectedValue = 3 Then

        '    ReportViewer1.Reset()
        '    lr.ReportPath = Server.MapPath("Reporte_310.rdlc")
        '    ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_310.rdlc"


        '    cb_310 = get_310.GetData(cli_1, cli_2, suc_1, suc_2, ddr_1, ddr_2, tdc_1, tdc_2, mto1, mto2, fec_1, fec_2, fec_prc_1, fec_prc_2)
        '    Dim neg As New Microsoft.Reporting.WebForms.ReportDataSource
        '    neg = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_dev_doc_con_codi_aut", cb_310)
        '    ReportViewer1.LocalReport.DataSources.Add(neg)
        'End If


        rw.AbrePopup(Me, 2, "Informes_cbz.aspx?cli_1=" & cli_1 & "&cli_2=" & cli_2 & "&suc_1=" & suc_1 & "&suc_2=" & suc_2 & _
                     "&ddr_1=" & ddr_1 & "&ddr_2=" & ddr_2 & "&tdc_1=" & tdc_1 & "&tdc_2=" & tdc_2 & "&mto1=" & mto1 & _
                     "&mto2=" & mto2 & "&fec_1=" & fec_1 & "&fec_2=" & fec_2 & "&fug_1=" & fug_1 & "&fug_2=" & fug_2 & _
                     "&tipo=" & Me.Dr_tip_inf.SelectedValue & "&fec_prc_1=" & fec_prc_1 & "&fec_prc_2=" & fec_prc_2 & _
                     "&eje_1=" & eje_1 & "&eje_2=" & eje_2 & "&cco_1=" & cco_1 & "&cco_2=" & cco_2 & " ", "Reporte Pagos", 1200, 800, 50, 100)


    End Sub

    Protected Sub Ch_tip0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_tip0.CheckedChanged
        If Me.Ch_tip0.Checked Then
            Me.Dr_tip_doc.SelectedValue = 0
            Me.Dr_tip_doc.Enabled = False
            Me.Dr_tip_doc.CssClass = "clsDisabled"
        Else

            Me.Dr_tip_doc.Enabled = True
            Me.Dr_tip_doc.CssClass = "clsMandatorio"
        End If
    End Sub

    Protected Sub Dr_tip_inf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_tip_inf.SelectedIndexChanged

        If Me.Dr_tip_inf.SelectedValue = 3 Then


            Me.txt_proc_des.Enabled = True
            Me.txt_proc_des.CssClass = "clsTxt"

            Me.txt_proc_has.Enabled = True
            Me.txt_proc_has.CssClass = "clsTxt"

            Me.txt_ult_ges_dde.Enabled = False
            Me.txt_ult_ges_dde.CssClass = "clsDisabled"

            Me.txt_ult_ges_hta.Enabled = False
            Me.txt_ult_ges_hta.CssClass = "clsDisabled"

            Me.txt_cco_des0.Text = ""
            Me.txt_cco_des0.CssClass = "clsTxt"
            Me.txt_cco_des0.ReadOnly = False

            Me.txt_cco_has0.Text = ""
            Me.txt_cco_has0.CssClass = "clsTxt"
            Me.txt_cco_has0.ReadOnly = False

        ElseIf Me.Dr_tip_inf.SelectedValue = 2 Then

            Me.txt_proc_des.Enabled = False
            Me.txt_proc_des.CssClass = "clsDisabled"

            Me.txt_proc_has.Enabled = False
            Me.txt_proc_has.CssClass = "clsDisabled"

            Me.txt_ult_ges_dde.Enabled = True
            Me.txt_ult_ges_dde.CssClass = "clsTxt"

            Me.txt_ult_ges_hta.Enabled = True
            Me.txt_ult_ges_hta.CssClass = "clsTxt"

            Me.txt_cco_des0.Text = ""
            Me.txt_cco_des0.CssClass = "clsDisabled"
            Me.txt_cco_des0.ReadOnly = True

            Me.txt_cco_has0.Text = ""
            Me.txt_cco_has0.CssClass = "clsDisabled"
            Me.txt_cco_has0.ReadOnly = True

        ElseIf Me.Dr_tip_inf.SelectedValue = 1 Then

            Me.txt_proc_des.Enabled = False
            Me.txt_proc_des.CssClass = "clsDisabled"

            Me.txt_proc_has.Enabled = False
            Me.txt_proc_has.CssClass = "clsDisabled"

            Me.txt_ult_ges_dde.Enabled = True
            Me.txt_ult_ges_dde.CssClass = "clsTxt"

            Me.txt_ult_ges_hta.Enabled = True
            Me.txt_ult_ges_hta.CssClass = "clsTxt"


            Me.txt_cco_des0.Text = ""
            Me.txt_cco_des0.CssClass = "clsTxt"
            Me.txt_cco_des0.ReadOnly = False

            Me.txt_cco_has0.Text = ""
            Me.txt_cco_has0.CssClass = "clsTxt"
            Me.txt_cco_has0.ReadOnly = False

        End If

    End Sub

    Protected Sub Ch_cob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cob.CheckedChanged
        If Me.Ch_cob.Checked Then
            Me.Dr_eje.SelectedValue = 0
            Me.Dr_eje.CssClass = "clsDisabled"
            Me.Dr_eje.Enabled = False
        Else
            Me.Dr_eje.SelectedValue = 0
            Me.Dr_eje.CssClass = "clsMandatorio"
            Me.Dr_eje.Enabled = True
        End If
    End Sub

    Protected Sub Ch_cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cli.CheckedChanged

        If Me.Ch_cli.Checked = True Then
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
            Me.Txt_Rut_Cli.Enabled = True
            Me.Txt_Dig_Cli.Enabled = True
            Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Dig_Cli.ReadOnly = False
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Raz_Soc.Text = ""
            Me.IB_AyudaCli.Enabled = True
        Else
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.Enabled = False
            Me.Txt_Dig_Cli.Enabled = False
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"

            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Raz_Soc.Text = ""
            Me.IB_AyudaCli.Enabled = False
        End If
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Me.Info.Visible = True
        Me.cli_ddr.Visible = True
        Me.Doc_Ope.Visible = True

        'Fecha vcto

        Me.txt_venc_des.Text = ""
        Me.txt_venc_has.Text = ""

        'Deudor

        Me.Txt_Rut_Deu.CssClass = "clsDisabled"
        Me.Txt_Rut_Deu.Enabled = False
        Me.Txt_Dig_Deu.Enabled = False
        Me.Txt_Dig_Deu.CssClass = "clsDisabled"

        Me.Txt_Rut_Deu.ReadOnly = True
        Me.Txt_Dig_Deu.ReadOnly = True
        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        Me.Ib_ayuda_deu.Enabled = False

        'Cliente

        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Rut_Cli.Enabled = False
        Me.Txt_Dig_Cli.Enabled = False
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"

        Me.Txt_Rut_Cli.ReadOnly = True
        Me.Txt_Dig_Cli.ReadOnly = True
        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Raz_Soc.Text = ""
        Me.IB_AyudaCli.Enabled = False

        'Tipo Informe
        Me.Dr_tip_inf.SelectedValue = 0

        Me.txt_proc_des.Enabled = False
        Me.txt_proc_des.CssClass = "clsDisabled"
        Me.txt_proc_des.Text = ""

        Me.txt_proc_has.Enabled = False
        Me.txt_proc_has.CssClass = "clsDisabled"
        Me.txt_proc_has.Text = ""

        Me.txt_ult_ges_dde.Enabled = True
        Me.txt_ult_ges_dde.CssClass = "clsTxt"
        Me.txt_ult_ges_dde.Text = ""

        Me.txt_ult_ges_hta.Enabled = True
        Me.txt_ult_ges_hta.CssClass = "clsTxt"
        Me.txt_ult_ges_hta.Text = ""

        'Monto

        Me.txt_mto_des0.Text = ""
        Me.txt_mto_has0.Text = ""

        'Codigo Cobranza

        Me.txt_cco_des0.Text = ""
        Me.txt_cco_has0.Text = ""

        Me.Ch_tip0.Checked = True
        Me.Dr_tip_doc.SelectedValue = 0

        Me.Ch_cob.Checked = True
        Me.Dr_eje.SelectedValue = 0

    
    End Sub
End Class
