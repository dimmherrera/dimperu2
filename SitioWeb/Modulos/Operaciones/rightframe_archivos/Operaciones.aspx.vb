Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_Operaciones

    Inherits System.Web.UI.Page
    Dim sesion As New ClsSession.ClsSession
    Dim ses_ope As New ClsSession.SesionOperaciones
    Dim clasecli As New ClaseClientes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim var As New FuncionesGenerales.Variables
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CA As New ClaseArchivos

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Response.Expires = -1

            ses_ope.coll_ope_otg = New Collection
            ses_ope.coll_ope = New Collection
            ses_ope.coll_ope_sim = New Collection
            sesion.coll_ope_anu = New Collection

            valida_cliente = ""

            cg.EjecutivosDevuelve(Me.dr_ejec, CodEje, 15)
            cg.ParametrosDevuelve(23, True, Me.dr_moneda)
            cg.ParametrosDevuelve(30, True, Me.dr_estado)

            txt_oto_des.Attributes.Add("Style", "Text-Align: right")
            txt_oto_has.Attributes.Add("Style", "Text-Align: right")
            txt_doc_des.Attributes.Add("Style", "Text-Align: right")
            txt_doc_has.Attributes.Add("Style", "Text-Align: right")
            Txt_Rut_Deu.Attributes.Add("Style", "Text-Align: right")
            Txt_Rut_Cli.Attributes.Add("Style", "Text-Align: right")

            NroPaginacion_DetalleOpe = 0
            page_anu = 0
            page_sim = 0
            page_pag = 0
            page_otg = 0
            page_dig = 0

            ope_id.Value = ""

            txt_fec_des.Text = "01-" & Now.Month.ToString("00") & "-" & Now.Year.ToString
            txt_fec_has.Text = RG.DevuelveUltimoDiaDelMes(Now.Month, Now.Year).ToString & "-" & Now.Month.ToString("00") & "-" & Now.Year.ToString

            Me.btn_mod_ope.Enabled = False

        End If

        Me.btn_mod_ope.Attributes.Add("OnClick", "OpenPopupModOpe('modope.aspx',420,200,100,100);")

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',640,480,200,150);")
        IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../../Modulos/Ayudas/AyudaDeu.aspx?tipo=2','PopUpCliente',640,480,200,150);")
    End Sub

    Public Sub cargagrillas()

        Dim rut_cli As Long, rut_cli1 As Long, _
        SUC1 As Integer, suc2 As Integer, _
        eje1 As String, eje2 As String, _
        nro_otg1 As Integer, nro_otg2 As Int64, _
        mon1 As String, mon2 As String, _
        nro_doc1 As String, nro_doc2 As String, _
        fec_des As Date, fec_has As Date, _
        tipo, rut_deu As Long, rut_deu1 As Long, _
        fec_vcto1 As Date, fec_vcto2 As Date, _
        fog As String, con_resp1 As String, con_resp2 As String

        Dim indice As Integer

        If Me.Ch_cliente.Checked Then
            rut_cli = Me.Txt_Rut_Cli.Text
            rut_cli1 = Me.Txt_Rut_Cli.Text
        Else
            rut_cli = 0
            rut_cli1 = 9999999999999
        End If

        If Me.Ch_deu.Checked Then
            rut_deu = CLng(Me.Txt_Rut_Deu.Text)
            rut_deu1 = CLng(Me.Txt_Rut_Deu.Text)
        Else
            rut_deu = 0
            rut_deu1 = 9999999999999
        End If

        SUC1 = 0
        suc2 = 999

        If Me.dr_ejec.SelectedValue = 0 Then
            Me.rb_ejec.Checked = True
        End If

        If Me.rb_ejec.Checked Then
            eje1 = 0
            eje2 = 9999
        Else
            eje1 = Me.dr_ejec.SelectedValue
            eje2 = Me.dr_ejec.SelectedValue
        End If

        If Me.dr_moneda.SelectedValue = 0 Then
            Me.Rb_moneda.Checked = True
        End If

        If Me.Rb_moneda.Checked Then
            mon1 = 0
            mon2 = 9999
        Else
            mon1 = Me.dr_moneda.SelectedValue
            mon2 = Me.dr_moneda.SelectedValue
        End If

        If Trim(Me.txt_fec_des.Text) = "" Then
            fec_des = CDate("01/01/1900 0:00:00")
            fec_has = CDate("31/12/9999 23:59:59")
        Else
            fec_des = CDate(txt_fec_des.Text)
            fec_has = CDate(txt_fec_has.Text)
        End If

        If Me.Ch_cliente.Checked Then
            tipo = 1
        Else
            tipo = 2
        End If

        If Me.Rb_est.Checked Then
            indice = 6
            IDX = 1
        Else
            indice = Me.dr_estado.SelectedValue
            IDX = Me.dr_estado.SelectedValue
        End If

        If Trim(Me.txt_oto_des.Text) = "" Then
            nro_otg1 = 0
            nro_otg2 = 999999999
        Else
            nro_otg1 = CDbl(txt_oto_des.Text)
            nro_otg2 = CDbl(txt_oto_has.Text)
        End If

        If Trim(Me.txt_doc_des.Text) = "" Then
            nro_doc1 = "0"
            nro_doc2 = "Z"
        Else
            nro_doc1 = txt_doc_des.Text.Trim
            nro_doc2 = txt_doc_has.Text.Trim
        End If


        If Trim(Me.txt_venc_des.Text) = "" Then
            fec_vcto1 = CDate("01/01/1900")
            fec_vcto2 = CDate("31/12/9999")
        Else
            fec_vcto1 = CDate(txt_venc_des.Text)
            fec_vcto2 = CDate(txt_venc_has.Text)
        End If

        If Me.rb_resp.SelectedValue = "T" Then
            con_resp1 = 0
            con_resp2 = 1
        Else
            con_resp1 = rb_resp.SelectedValue
            con_resp2 = rb_resp.SelectedValue
        End If


        fog = ""

        If dr_estado.SelectedValue = 0 Then


            'INGRESADAS
            cargarGv_Dig(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 1, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            'SIMULADAS 
            cargarGv_sim(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 2, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            'OTORGADAS
            cargarGv_Otg(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 3, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)
            
            'PAGADOS
            cargarGv_Pag(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 5, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            ' ANULADAS
            cargarGv_Anul(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 4, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

        End If

        If dr_estado.SelectedValue = 1 Then

            cargarGv_Dig(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 1, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            If Me.gr_dig.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se encontraron operaciones según los criterios proporcionados", 2)
            End If

        ElseIf dr_estado.SelectedValue = 2 Then

            cargarGv_Sim(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 2, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)
            
            If Me.gr_sim.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se encontraron operaciones según los criterios proporcionados", 2)
            End If

        ElseIf dr_estado.SelectedValue = 3 Then

            cargarGv_Otg(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 3, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            If Me.gr_otg.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se encontraron operaciones según los criterios proporcionados", 2)
            End If


        ElseIf dr_estado.SelectedValue = 5 Then

            cargarGv_Pag(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 5, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)
            
        ElseIf dr_estado.SelectedValue = 6 Then

            cargarGv_Anul(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 4, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, eje1, eje2, False, Nothing)

            If Me.gr_anul.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se encontraron operaciones según los criterios proporcionados", 2)
            End If

        End If

        hab_des_controles("I")

    End Sub

    Public Function TraeRut() As Boolean


        Try
            Dim cli As cli_cls


            cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)



            Session("Cliente") = cli

            If valida_cliente <> "" Then
                msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Informacion)
                TraeRut = True
                Exit Function
            Else
                If IsNothing(cli) Then
                    msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    TraeRut = True
                    Exit Function
                End If

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                MaskedEditExtender1.Enabled = False
                IB_AyudaCli.Enabled = False
                'Asigna Razón Social / Nombre a Campo Cliente
                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            End If


            TraeRut = False
        Catch ex As Exception

        End Try
    End Function

    Protected Sub Ch_cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cliente.CheckedChanged

        If Me.Ch_cliente.Checked Then

            ' Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"

            Me.Txt_Dig_Cli.ReadOnly = False
            Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli.Enabled = True
            Txt_Dig_Cli.Enabled = True
            'Me.txt_fec_des.CssClass = "clsTxt"
            'Me.txt_fec_has.CssClass = "clsTxt"

            'Me.txt_oto_des.ReadOnly = False
            'Me.txt_oto_des.CssClass = "clsTxt"
            'Me.txt_oto_des.Text = ""

            'Me.txt_oto_has.ReadOnly = False
            'Me.txt_oto_has.CssClass = "clsTxt"
            'Me.txt_oto_has.Text = ""
            'Me.RequiredFieldValidator4.Enabled = False
            'Me.RequiredFieldValidator5.Enabled = False
            'help.Visible = True
            MaskedEditExtender1.Enabled = True
            IB_AyudaCli.Enabled = True
            Me.Txt_Rut_Cli.Focus()


            gr_otg.DataSource = New Collection
            gr_otg.DataBind()

            gr_dig.DataSource = New Collection
            gr_dig.DataBind()


            gr_sim.DataSource = New Collection
            gr_sim.DataBind()

        Else

            Me.Txt_Rut_Cli.Enabled = False
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""


            Me.Txt_Dig_Cli.Enabled = False
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.txt_fec_des.CssClass = "clsMandatorio"
            Me.txt_fec_has.CssClass = "clsMandatorio"

            'Me.txt_oto_des.ReadOnly = True
            'Me.txt_oto_des.CssClass = "clsDisabled"
            'Me.txt_oto_des.Text = ""

            'Me.txt_oto_has.ReadOnly = True
            'Me.txt_oto_has.CssClass = "clsDisabled"
            'Me.txt_oto_has.Text = ""

            ' help.Visible = False
            MaskedEditExtender1.Enabled = False
            IB_AyudaCli.Enabled = False
        End If
    End Sub

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged

        If Me.Ch_deu.Checked Then

            Me.Txt_Rut_Deu.ReadOnly = False
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rso_Deu.Text = ""
            'Img1.Visible = True
            IB_AyudaDeu.Enabled = True
            Txt_deu_Cli_MaskedEditExtender.Enabled = True
            Me.Txt_Rut_Deu.Focus()
        Else
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            IB_AyudaDeu.Enabled = False
            Txt_deu_Cli_MaskedEditExtender.Enabled = False
            'Img1.Visible = False
        End If

    End Sub

    Public Sub hab_des_controles(ByVal est As String)

        If est = "H" Then

            Me.Ch_cliente.Enabled = True
            Me.Ch_cliente.Checked = False

            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"

            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"

            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"

            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"

            Me.Ch_deu.Checked = False

            txt_oto_des.ReadOnly = False
            txt_oto_des.CssClass = "clsTxt"
            MaskedEditExtender2.Enabled = True

            txt_oto_has.ReadOnly = False
            txt_oto_has.CssClass = "clsTxt"
            MaskedEditExtender3.Enabled = True
            dr_ejec.CssClass = "clsTxt"
            Me.txt_doc_des.Text = ""
            Me.txt_doc_des.CssClass = "clsTxt"
            Me.txt_doc_has.Text = ""
            Me.txt_doc_has.CssClass = "clsTxt"
            Me.txt_fec_des.Text = ""
            Me.txt_fec_des.CssClass = "clsMandatorio"
            Me.txt_fec_has.Text = ""
            Me.txt_fec_has.CssClass = "clsMandatorio"
            txt_fec_des_CalendarExtender.Enabled = True
            txt_fec_has_CalendarExtender.Enabled = True
            Me.dr_ejec.SelectedValue = 0
            Me.dr_ejec.Enabled = True
            Me.Ch_deu.Enabled = True
            Me.dr_estado.SelectedValue = 0
            Me.dr_estado.Enabled = True
            txt_venc_des_CalendarExtender.Enabled = True
            txt_venc_has_CalendarExtender.Enabled = True

            txt_venc_des.ReadOnly = False
            txt_venc_des.CssClass = "clsTxt"

            txt_venc_has.ReadOnly = False
            txt_venc_has.CssClass = "clsTxt"


            Me.dr_moneda.SelectedValue = 0
            Me.dr_moneda.Enabled = True

            Me.rb_resp.SelectedValue = "T"
            Me.rb_resp.Enabled = True

            Me.txt_doc_des.ReadOnly = False
            Me.txt_doc_has.ReadOnly = False
            Me.txt_fec_des.ReadOnly = False
            Me.txt_fec_has.ReadOnly = False

            Me.gr_anul.Controls.Clear()
            Me.gr_sim.Controls.Clear()
            Me.gr_otg.Controls.Clear()
            Me.gr_pag.Controls.Clear()
            Me.gr_dig.Controls.Clear()

            gr_anul.DataSource = Nothing
            gr_anul.DataBind()

            gr_sim.DataSource = Nothing
            gr_sim.DataBind()

            gr_otg.DataSource = Nothing
            gr_otg.DataBind()

            gr_pag.DataSource = Nothing
            gr_pag.DataBind()

            gr_dig.DataSource = Nothing
            gr_dig.DataBind()

            Me.Btn_Buscar.Enabled = True
            Me.btn_imp.Enabled = False
            Me.Btn_AdjDoc.Enabled = False
            MaskedEditExtender1.Enabled = False
            MaskedEditExtender2.Enabled = True

            MaskedEditExtender3.Enabled = True
            MaskedEditExtender6.Enabled = True
            MaskedEditExtender7.Enabled = True
            MaskedEditExtender8.Enabled = True
            rb_ejec.Checked = True
            Rb_est.Checked = True
            Rb_moneda.Checked = True
            rb_ejec.Enabled = True
            Rb_est.Enabled = True
            Rb_moneda.Enabled = True
            'rb_resp.Enabled = True
            txt_oto_des.Text = ""
            txt_oto_has.Text = ""
            txt_venc_des.Text = ""
            txt_venc_has.Text = ""

        Else
            rb_ejec.Enabled = False
            Rb_est.Enabled = False
            Rb_moneda.Enabled = False

            Me.Ch_cliente.Enabled = False

            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"

            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"

            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"

            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"

            Me.Ch_deu.Enabled = False

            Me.txt_doc_des.ReadOnly = True
            Me.txt_doc_des.CssClass = "clsTxt"

            Me.txt_doc_has.ReadOnly = True
            Me.txt_doc_has.CssClass = "clsTxt"

            txt_fec_des_CalendarExtender.Enabled = False
            Me.txt_fec_des.CssClass = "clsDisabled"

            Me.txt_fec_des.ReadOnly = True
            txt_fec_des_CalendarExtender.Enabled = False
            Me.txt_fec_has.CssClass = "clsDisabled"

            Me.txt_fec_has.ReadOnly = True
            ' Me.dr_ejec.SelectedValue = 0
            Me.dr_ejec.Enabled = True

            txt_venc_des.ReadOnly = True
            txt_venc_des.CssClass = "clsDisabled"
            txt_venc_des_CalendarExtender.Enabled = False


            txt_venc_has.ReadOnly = True
            txt_venc_has.CssClass = "clsDisabled"
            txt_venc_has_CalendarExtender.Enabled = False
            txt_oto_des.CssClass = "clsDisabled"
            txt_oto_has.CssClass = "clsDisabled"
            '  Me.dr_estado.SelectedValue = 0
            Me.dr_estado.Enabled = False
            Me.dr_estado.CssClass = "clsDisabled"

            ' Me.dr_moneda.SelectedValue = 0
            Me.dr_moneda.Enabled = False
            Me.dr_moneda.CssClass = "clsDisabled"
            MaskedEditExtender1.Enabled = False
            MaskedEditExtender2.Enabled = False

            MaskedEditExtender3.Enabled = False
            MaskedEditExtender6.Enabled = False
            MaskedEditExtender7.Enabled = False
            MaskedEditExtender8.Enabled = False


            Me.txt_doc_des.CssClass = "clsDisabled"
            Me.txt_doc_has.CssClass = "clsDisabled"
            Me.Btn_Buscar.Enabled = False
            Me.btn_imp.Enabled = True

            txt_oto_des.ReadOnly = True
            txt_oto_des.CssClass = "clsDisabled"
            MaskedEditExtender2.Enabled = False

            txt_oto_has.ReadOnly = True
            txt_oto_has.CssClass = "clsDisabled"
            MaskedEditExtender3.Enabled = False
            txt_fec_has_CalendarExtender.Enabled = False
            dr_ejec.Enabled = False
            dr_ejec.CssClass = "clsDisabled"
            rb_resp.Enabled = False



        End If
    End Sub

    Protected Sub lb_cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cli.Click
        Dim cli As cli_cls


        cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)



        Session("Cliente") = cli

        If valida_cliente <> "" Then
            msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Informacion)
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Exit Sub
        Else
            If IsNothing(cli) Then
                msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If

            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"
            MaskedEditExtender1.Enabled = False
            IB_AyudaCli.Enabled = False
            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
        End If




    End Sub

    Protected Sub dr_estado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If Me.dr_estado.SelectedValue <> 0 Then
            Me.Rb_est.Checked = False
        Else
            Me.Rb_est.Checked = True
        End If

    End Sub

    Protected Sub dr_moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.dr_moneda.SelectedValue <> 0 Then
            Me.Rb_moneda.Checked = False
        Else
            Me.Rb_moneda.Checked = True
        End If
    End Sub

    Protected Sub dr_ejec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If Me.dr_ejec.SelectedValue <> 0 Then
            Me.rb_ejec.Checked = False
        Else
            Me.rb_ejec.Checked = True
        End If

    End Sub

    Public Sub cargadetalle(ByVal col As Collection)

        If Me.TabGrillas.ActiveTabIndex = 0 Then

            col = OP.documentosIngresados_Retorna(ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Value) + 1).id_ope, _
                                                  ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Value) + 1).id_ope)

            Me.Gr_Documentos.DataSource = col
            Me.Gr_Documentos.DataBind()

            For i = 0 To Gr_Documentos.Rows.Count - 1

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & _
                                                        RG.Vrut(CLng(Gr_Documentos.Rows(i).Cells(0).Text))

                If ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Value) + 1).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)


                ElseIf ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Value) + 1).id_p_0023 > 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD)


                ElseIf ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Value) + 1).id_p_0023 = 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD4)


                End If
            Next

        ElseIf Me.TabGrillas.ActiveTabIndex = 1 Then

            col = OP.documentosIngresados_Retorna(ses_ope.coll_ope_sim.Item(Val(pos_sim.Value) + 1).id_ope, _
                                                  ses_ope.coll_ope_sim.Item(Val(pos_sim.Value) + 1).id_ope)

            Me.Gr_Documentos.DataSource = col
            Me.Gr_Documentos.DataBind()

            For i = 0 To Gr_Documentos.Rows.Count - 1

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & _
                                                        RG.Vrut(CLng(Gr_Documentos.Rows(i).Cells(0).Text))

                If ses_ope.coll_ope_sim.Item(Val(pos_sim.Value) + 1).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)


                ElseIf ses_ope.coll_ope_sim.Item(Val(pos_sim.Value) + 1).id_p_0023 > 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD)


                ElseIf ses_ope.coll_ope_sim.Item(Val(pos_sim.Value) + 1).id_p_0023 = 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD4)


                End If
            Next

        ElseIf Me.TabGrillas.ActiveTabIndex = 2 Then

            Dim otg As Object

            otg = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1)

            With otg

                col = OP.DocumentosOtorgagos_RetornaDoctos(.cli_idc, .cli_idc, _
                                                                               "000000000000", "9999999999999", _
                                                                               .id_opo, .id_opo, _
                                                                               .id_p_0031, .id_p_0031, _
                                                                               "0", "Z", _
                                                                               0, 999, _
                                                                               CDate("01/01/1900"), CDate("31/12/2099"), _
                                                                               1, 2, 3, 4, 5, 13, 7, 8, 9, 10, 11, 12)
                GridView1.DataSource = col
                Me.GridView1.DataBind()

                For i = 0 To GridView1.Rows.Count - 1

                    Me.GridView1.Rows(i).Cells(0).Text = Format(CLng(GridView1.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & col(i + 1).cli_dig_ito

                    If ses_ope.coll_ope_otg.Item(Val(pos_otg.Value) + 1).id_p_0023 = 1 Then

                        Me.GridView1.Rows(i).Cells(3).Text = Format(CDbl(GridView1.Rows(i).Cells(3).Text), fmt.FCMSD)
                        Me.GridView1.Rows(i).Cells(4).Text = Format(CDbl(GridView1.Rows(i).Cells(4).Text), fmt.FCMSD)
                        Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(GridView1.Rows(i).Cells(8).Text), fmt.FCMSD)
                        Me.GridView1.Rows(i).Cells(9).Text = Format(CDbl(GridView1.Rows(i).Cells(9).Text), fmt.FCMSD)

                    ElseIf ses_ope.coll_ope_otg.Item(Val(pos_otg.Value) + 1).id_p_0023 > 2 Then

                        Me.GridView1.Rows(i).Cells(3).Text = Format(CDbl(GridView1.Rows(i).Cells(3).Text), fmt.FCMCD)
                        Me.GridView1.Rows(i).Cells(4).Text = Format(CDbl(GridView1.Rows(i).Cells(4).Text), fmt.FCMCD)
                        Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(GridView1.Rows(i).Cells(8).Text), fmt.FCMCD)
                        Me.GridView1.Rows(i).Cells(9).Text = Format(CDbl(GridView1.Rows(i).Cells(9).Text), fmt.FCMCD)

                    ElseIf ses_ope.coll_ope_otg.Item(Val(pos_otg.Value) + 1).id_p_0023 = 2 Then

                        Me.GridView1.Rows(i).Cells(3).Text = Format(CDbl(GridView1.Rows(i).Cells(3).Text), fmt.FCMCD4)
                        Me.GridView1.Rows(i).Cells(4).Text = Format(CDbl(GridView1.Rows(i).Cells(4).Text), fmt.FCMCD4)
                        Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(GridView1.Rows(i).Cells(8).Text), fmt.FCMCD4)
                        Me.GridView1.Rows(i).Cells(9).Text = Format(CDbl(GridView1.Rows(i).Cells(9).Text), fmt.FCMCD4)

                    End If
                Next


            End With

        ElseIf Me.TabGrillas.ActiveTabIndex = 4 Then


            col = OP.documentosIngresados_Retorna(sesion.coll_ope_anu.Item(Val(pos_anu.Value) + 1).id_ope, _
                                                  sesion.coll_ope_anu.Item(Val(pos_anu.Value) + 1).id_ope)

            Me.Gr_Documentos.DataSource = col
            Me.Gr_Documentos.DataBind()

            For i = 0 To Gr_Documentos.Rows.Count - 1

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & _
                                                        RG.Vrut(CLng(Gr_Documentos.Rows(i).Cells(0).Text))

                If sesion.coll_ope_anu.Item(Val(pos_anu.Value) + 1).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)


                ElseIf sesion.coll_ope_anu.Item(Val(pos_anu.Value) + 1).id_p_0023 > 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD)


                ElseIf sesion.coll_ope_anu.Item(Val(pos_anu.Value) + 1).id_p_0023 = 2 Then

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)
                    Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMCD4)


                End If
            Next

        End If

    End Sub

    Protected Sub Lb_det_opes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_det_opes.Click

        If Me.TabGrillas.ActiveTabIndex = 0 Then

            cargadetalle(ses_ope.coll_ope)

        ElseIf TabGrillas.ActiveTabIndex = 1 Then

            cargadetalle(ses_ope.coll_ope_sim)

        ElseIf TabGrillas.ActiveTabIndex = 2 Then

            cargadetalle(ses_ope.coll_ope_otg)

        ElseIf TabGrillas.ActiveTabIndex = 3 Then
            'Que hace aqui???

        ElseIf TabGrillas.ActiveTabIndex = 4 Then

            cargadetalle(sesion.coll_ope_anu)

        End If

    End Sub

    Protected Sub RetornaDoctos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetornaDoctos.Click

        If Me.TabGrillas.ActiveTabIndex = 0 Then
            marcagrilla(Me.gr_dig, Me.Txt_ItemOPE)
        ElseIf Me.TabGrillas.ActiveTabIndex = 1 Then
            marcagrilla(Me.gr_sim, Me.pos_sim)
        ElseIf Me.TabGrillas.ActiveTabIndex = 2 Then
            marcagrilla(Me.gr_otg, Me.pos_otg)
        ElseIf Me.TabGrillas.ActiveTabIndex = 3 Then
            marcagrilla(Me.gr_pag, Me.pos_pag)
        ElseIf Me.TabGrillas.ActiveTabIndex = 4 Then
            marcagrilla(Me.gr_anul, Me.pos_anu)
        End If

        'Lb_det_opes_Click(Me, e)

    End Sub

    Public Sub marcagrilla(ByVal gr As GridView, ByVal item As HiddenField)


        For I = 0 To gr.Rows.Count - 1

            If (Val(item.Value) = I) Then

                If (I Mod 2) = 0 Then
                    gr.Rows(I).CssClass = "selectable"
                Else
                    gr.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    gr.Rows(I).CssClass = "formatUltcell"
                Else
                    gr.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then
            Modulo = "Operacion"
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)
        End If

    End Sub

    Protected Sub btn_next_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_otg.Click
        Dim val As Integer
        Select Case Me.TabGrillas.ActiveTabIndex

            Case 0

                If Me.gr_dig.Rows.Count < 12 Then
                    msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                    Exit Sub
                End If
                page_dig = page_dig + 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 1
                cargagrillas()
                Me.dr_estado.SelectedValue = val

            Case 1

                If Me.gr_sim.Rows.Count < 12 Then
                    msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                    Exit Sub
                End If

                page_sim = page_sim + 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 2
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                hf_nro_pag_sim.Value = hf_nro_pag_sim.Value - 1
                lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""

            Case 2

                If Me.gr_otg.Rows.Count < 12 Then
                    msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                    Exit Sub
                End If

                page_otg = page_otg + 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 3
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                hf_can_pag.Value = hf_can_pag.Value + 1
                Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""

            Case 3

                If gr_anul.Rows.Count < 12 Then
                    msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                    Exit Sub
                End If

                page_anu = page_anu + 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 4
                cargagrillas()
                Me.dr_estado.SelectedValue = val

            Case 4
                If gr_pag.Rows.Count < 12 Then
                    msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                    Exit Sub
                End If
                page_pag = page_pag + 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 5
                cargagrillas()
                Me.dr_estado.SelectedValue = val
        End Select
    End Sub

    Protected Sub btn_prev_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_otg.Click
        Dim val As Integer
        Select Case Me.TabGrillas.ActiveTabIndex
            Case 0

                If page_dig = 0 Then

                    msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub

                End If
                page_dig = page_dig - 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 1
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                hf_nro_pag_dig.Value = hf_nro_pag_dig.Value - 1
                lb_pag_dig.Text = "Página " & hf_nro_pag_dig.Value & ""



            Case 1

                If page_sim = 0 Then

                    msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub

                End If
                page_sim = page_sim - 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 2
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                hf_nro_pag_sim.Value = hf_nro_pag_sim.Value - 1
                lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""

            Case 2



                If page_otg = 0 Then

                    msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub

                End If


                page_otg = page_otg - 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 3
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                hf_can_pag.Value = hf_can_pag.Value - 1
                Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""

            Case 4
                If page_anu = 0 Then
                    msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub
                End If

                page_anu = page_anu - 12
                val = Me.dr_estado.SelectedValue
                Me.dr_estado.SelectedValue = 4
                cargagrillas()
                Me.dr_estado.SelectedValue = val

                'Case 4
                '    If page_pag = 0 Then
                '        msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                '        Exit Sub
                '    End If
                '    page_pag = page_pag - 12
                '    val = Me.dr_estado.SelectedValue
                '    Me.dr_estado.SelectedValue = 5
                '    cargagrillas()
                '    Me.dr_estado.SelectedValue = val





        End Select

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        lb_cli_Click(Me, e)
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            Dim deu As deu_cls

            deu = cg.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

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
                Txt_deu_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaDeu.Enabled = False
                Txt_Rut_Deu.Text = Format(CDbl(Txt_Rut_Deu.Text), fmt.FCMSD)
            Else
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                msj.Mensaje(Page, "Atención", "Deudor no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rb_ejec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_ejec.CheckedChanged
        Try
            If rb_ejec.Checked Then
                dr_ejec.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_est_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_est.CheckedChanged
        Try
            If Rb_est.Checked Then
                dr_estado.ClearSelection()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_moneda_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_moneda.CheckedChanged
        Try
            If Rb_moneda.Checked Then
                dr_moneda.ClearSelection()
            End If
        Catch ex As Exception

        End Try

    End Sub



#Region "GridView"

    'Digitadas
    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion digitada

        Try

            Dim img As ImageButton = CType(sender, ImageButton)
            'Buscamos la posicion dentro de la grilla
            For i = 0 To gr_dig.Rows.Count - 1
                If img.ToolTip = CType(gr_dig.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                    Txt_ItemOPE.Value = i
                    ope_id.Value = ""
                    ope_id.Value = img.ToolTip.ToString
                    Btn_AdjDoc.Enabled = True
                    btn_detope.Enabled = True

                    Exit For
                End If
            Next

            RetornaDoctos_Click(sender, New System.EventArgs)
        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

        btn_inf_eva.Enabled = False
        btn_inf_neg.Enabled = False
        

    End Sub

    'Simuladas
    Protected Sub Img_Ver_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion simuladas

        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            'Buscamos la posicion dentro de la grilla
            For i = 0 To gr_sim.Rows.Count - 1
                If img.ToolTip = CType(gr_sim.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                    pos_sim.Value = i
                    ope_id.Value = ""
                    ope_id.Value = img.ToolTip.ToString
                    Btn_AdjDoc.Enabled = True
                    btn_detope.Enabled = True
                    Exit For
                End If
            Next

            RetornaDoctos_Click(sender, New System.EventArgs)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

        btn_inf_eva.Enabled = False
        btn_inf_neg.Enabled = False

    End Sub

    'Otorgadas
    Protected Sub Img_Ver_Click2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion otorgadas

        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            'Buscamos la posicion dentro de la grilla
            For i = 0 To gr_otg.Rows.Count - 1
                If img.ToolTip = CType(gr_otg.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                    pos_otg.Value = i
                    ope_id.Value = ""
                    ope_id.Value = img.ToolTip.ToString

                    Btn_AdjDoc.Enabled = True
                    btn_detope.Enabled = True
                    Btn_Anu.Enabled = True
                    btn_inf_otg.Enabled = True
                    btn_inf_eva.Enabled = True
                    btn_inf_neg.Enabled = True

                    Exit For

                End If
            Next

            RetornaDoctos_Click(sender, New System.EventArgs)

            If Me.pos_otg.Value <> "" Then
                Me.btn_mod_ope.Enabled = True
            Else
                Me.btn_mod_ope.Enabled = False
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    'Pagadas
    Protected Sub Img_Ver_Click3(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion digitada

        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            'Buscamos la posicion dentro de la grilla
            For i = 0 To gr_pag.Rows.Count - 1
                If img.ToolTip = CType(gr_pag.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                    pos_pag.Value = i
                    ope_id.Value = ""
                    ope_id.Value = img.ToolTip.ToString
                    Btn_AdjDoc.Enabled = True
                    btn_detope.Enabled = True
                    Exit For
                End If
            Next

            RetornaDoctos_Click(sender, New System.EventArgs)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

        btn_inf_eva.Enabled = False
        btn_inf_neg.Enabled = False

    End Sub

    'Anuladas
    Protected Sub Img_Ver_Click4(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion digitada

        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            'Buscamos la posicion dentro de la grilla
            For i = 0 To gr_anul.Rows.Count - 1
                If img.ToolTip = CType(gr_anul.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                    pos_anu.Value = i
                    ope_id.Value = ""
                    ope_id.Value = img.ToolTip.ToString
                    Btn_AdjDoc.Enabled = True
                    btn_detope.Enabled = True
                    Exit For
                End If
            Next

            RetornaDoctos_Click(sender, New System.EventArgs)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

        btn_inf_eva.Enabled = False
        btn_inf_neg.Enabled = False

    End Sub

    Protected Sub gr_dig_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_dig.RowDataBound
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
        End If
    End Sub

    Protected Sub gr_sim_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_sim.RowDataBound
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

        End If
    End Sub

    Protected Sub gr_otg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_otg.RowDataBound

        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            'Dim imgeva As ImageButton = CType(e.Row.FindControl("Img_VerEva"), ImageButton)
            'Dim imgneg As ImageButton = CType(e.Row.FindControl("Img_VerNeg"), ImageButton)

            'imgeva.Attributes.Add("onClick", "javascript:SendToPdf('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx'," & imgeva.ToolTip & "," & 2 & ");")
            'imgneg.Attributes.Add("onClick", "javascript:SendToPdf('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx'," & imgneg.ToolTip & "," & 1 & ");")

        End If

    End Sub

    Protected Sub gr_pag_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_pag.RowDataBound
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

        End If
    End Sub

    Protected Sub gr_anul_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_anul.RowDataBound
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

        End If
    End Sub

#End Region

#Region "Botonera"

    Protected Sub lb_anu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_anu.Click

        Try
            If pos_otg.Value <> "" Then

                OP.OTORGAMIENTO_ANULA(ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1).id_ope, _
                                      ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1).id_opn, _
                                      ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1).id_eva)

                msj.Mensaje(Me, "Atención", "Operación Anulada ", 2)
                cargagrillas()

            Else
                msj.Mensaje(Page, "Atención", "Debe seleccionar una operación para anular", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try

    End Sub

    Protected Sub Btn_AdjDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_AdjDoc.Click
        FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Pop_up_DoctoDig.aspx?ID=" & Replace(ope_id.Value, ".", ""), "DocumentosDigitalizados", 650, 350, 15, 15)
    End Sub

    Protected Sub Btn_Anu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Anu.Click 'Handles Btn_Anu.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20050705, Usr, "PRESIONA BOTON ANULAR OTORGAMIENTO") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.TabGrillas.ActiveTabIndex = 2 Then

            If pos_otg.Value = "" Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar una operación otorgada para anular", 2)
                Exit Sub
            End If


            msj.Mensaje(Me, "Atención", "¿Desea anular la operación?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_anu.UniqueID)
            Exit Sub
        Else
            msj.Mensaje(Me, "Atención", "Debe seleccionar una operación otorgada para anular", 2)
            Exit Sub
        End If



    End Sub

    Protected Sub btn_inf_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_inf_otg.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20030705, Usr, "PRESIONA BOTON IMPRIMIR OTORGAMIENTO") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.pos_otg.Value <> "" Then

            sesion.RutCli = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1).CLI_IDC
            sesion.NroOperacion = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value) + 1).ID_OPE

            AbrePopup(Me, 1, "../../Pizarras/Reportes/report_otg.aspx", "Informes", 1000, 900, 100, 0)
        Else
            msj.Mensaje(Page, "Atención", "Debe seleccionar una operación otorgada", ClsMensaje.TipoDeMensaje._Informacion)
        End If

    End Sub

    Protected Sub btn_imp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_imp.Click

        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20060705, Usr, "PRESIONA BOTON IMPRIMIR OPERACIONES") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim rut_cli As String, rut_cli1 As String, _
            cli_rso As String, deu_rso As String, _
            SUC1 As Integer, suc2 As Integer, _
            eje1 As String, eje2 As String, _
            nro_otg1 As Integer, nro_otg2 As Int64, _
            mon1 As Integer, mon2 As Integer, _
            estado As Integer, estado2 As Integer, _
            nro_doc1 As String, nro_doc2 As String, _
            fec_des As String, fec_has As String, _
            rut_deu As String, rut_deu1 As String, _
            fec_vcto1 As String, fec_vcto2 As String


            If Me.Ch_cliente.Checked Then

                rut_cli = Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT)
                rut_cli1 = Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT)
                cli_rso = Me.Txt_Raz_Soc.Text
            Else
                rut_cli = "000000000000"
                rut_cli1 = "9999999999999"
                cli_rso = ""
            End If

            If Me.Ch_deu.Checked Then
                rut_deu = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
                rut_deu1 = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
                deu_rso = Me.Txt_Rso_Deu.Text
            Else
                rut_deu = "000000000000"
                rut_deu1 = "9999999999999"
                deu_rso = ""
            End If

            SUC1 = 0
            suc2 = 999

            If Me.dr_ejec.SelectedValue = 0 Then
                Me.rb_ejec.Checked = True
            End If

            If Me.rb_ejec.Checked Then
                eje1 = 0
                eje2 = 9999
            Else
                eje1 = Me.dr_ejec.SelectedValue
                eje2 = Me.dr_ejec.SelectedValue
            End If

            If Me.dr_moneda.SelectedValue = 0 Then
                Me.Rb_moneda.Checked = True
            End If

            If Me.Rb_moneda.Checked Then
                mon1 = 0
                mon2 = 9999
            Else
                mon1 = Me.dr_moneda.SelectedValue
                mon2 = Me.dr_moneda.SelectedValue
            End If

            If Trim(Me.txt_fec_des.Text) = "" Then
                fec_des = "01/01/1900"
                fec_has = "31/12/2999"
            Else
                fec_des = CDate(txt_fec_des.Text)
                fec_has = CDate(txt_fec_has.Text)
            End If

            If Me.Rb_est.Checked Then
                estado = 0
                estado2 = 9999999
            Else
                estado = Me.dr_estado.SelectedValue
                estado2 = Me.dr_estado.SelectedValue
            End If

            Select Case Me.TabGrillas.ActiveTabIndex
                Case 0 'digitadas
                    estado = 1
                    estado2 = 1

                    If gr_dig.Rows.Count <= 0 Then
                        msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 1 'simuladas
                    estado = 2
                    estado2 = 2

                    If gr_sim.Rows.Count <= 0 Then
                        msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 2 'otorgadas
                    estado = 3
                    estado2 = 3

                    If gr_otg.Rows.Count <= 0 Then
                        msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 3
                    'Pagadas
                    estado = 4
                    estado2 = 4

                    If gr_pag.Rows.Count <= 0 Then
                        msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 4 'anulada
                    estado = 6
                    estado2 = 6

                    If gr_anul.Rows.Count <= 0 Then
                        msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

            End Select

            If Trim(Me.txt_oto_des.Text) = "" Then
                nro_otg1 = 0
                nro_otg2 = 999999999
            Else
                nro_otg1 = CDbl(txt_oto_des.Text)
                nro_otg2 = CDbl(txt_oto_has.Text)
            End If

            If Trim(Me.txt_doc_des.Text) = "" Then
                nro_doc1 = "0"
                nro_doc2 = "Z"
            Else
                nro_doc1 = txt_doc_des.Text.Trim
                nro_doc2 = txt_doc_has.Text.Trim
            End If


            If Trim(Me.txt_venc_des.Text) = "" Then
                fec_vcto1 = "01/01/1900"
                fec_vcto2 = "31/12/2999"
            Else
                fec_vcto1 = CDate(txt_venc_des.Text)
                fec_vcto2 = CDate(txt_venc_has.Text)
            End If



            'sesion.RutCli = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value)).CLI_IDC
            'sesion.NroOperacion = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value)).ID_OPE



            If gr_dig.Rows.Count > 0 Or gr_otg.Rows.Count > 0 Or gr_anul.Rows.Count > 0 Or gr_sim.Rows.Count > 0 Then
                AbrePopup(Me, 1, "Lista_Operaciones.aspx?rut_cli=" & rut_cli & "&rut_cli2=" & rut_cli1 & "&cli_rso=" & cli_rso _
                                                                                        & "&rut_deu=" & rut_deu & "&rut_deu2=" & rut_deu1 & "&deu_rso=" & deu_rso _
                                                                                        & "&suc=" & SUC1 & "&suc2=" & suc2 _
                                                                                        & "&eje=" & eje1 & "&eje2=" & eje2 _
                                                                                        & "&fec_dde=" & fec_des & "&fec_has=" & fec_has _
                                                                                        & "&n_ope1=" & nro_otg1 & "&n_ope2=" & nro_otg2 _
                                                                                        & "&nro_doc=" & nro_doc1 & "&nro_doc2=" & nro_doc2 _
                                                                                        & "&vcto_dde=" & fec_vcto1 & "&vcto_hta=" & fec_vcto2 _
                                                                                        & "&est_ope=" & estado & "&est_ope2=" & estado2 _
                                                                                        & "&mon=" & mon1 & "&mon2=" & mon2 _
                                                                                        & "", "Informes", 900, 800, 100, 0)
            Else
                msj.Mensaje(Me, "Atención", "No se encontraron datos para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub btn_detope_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_detope.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20040705, Usr, "PRESIONA BOTON VER DETALLE OPERACIONES") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Txt_ItemOPE.Value = "" And Me.pos_sim.Value = "" And Me.pos_otg.Value = "" And Me.pos_anu.Value = "" Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar una operación para ver su detalle", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        Lb_det_opes_Click(Me, e)

        If Me.TabGrillas.ActiveTabIndex = 0 Or Me.TabGrillas.ActiveTabIndex = 1 Or Me.TabGrillas.ActiveTabIndex = 4 Then
            Me.ModalPopupExtender1.Show()
        ElseIf Me.TabGrillas.ActiveTabIndex = 2 Then
            Me.ModalPopupExtender2.Show()
        End If


    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpiar.Click

        hab_des_controles("H")
        'Img1.Visible = False
        'help.Visible = False
        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Raz_Soc.Text = ""
        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        Txt_Rso_Deu.Text = ""
        dr_estado.Enabled = True
        Me.dr_estado.CssClass = "clsTxt"

        ' Me.dr_moneda.SelectedValue = 0
        Me.dr_moneda.Enabled = True
        Me.dr_moneda.CssClass = "clsTxt"
        NroPaginacion_DetalleOpe = 0
        page_anu = 0
        page_sim = 0
        page_pag = 0
        page_otg = 0
        page_dig = 0
        pos_otg.Value = ""


        txt_fec_des.Text = "01-" & Now.Month.ToString("00") & "-" & Now.Year.ToString
        txt_fec_has.Text = RG.DevuelveUltimoDiaDelMes(Now.Month, Now.Year).ToString & "-" & Now.Month.ToString("00") & "-" & Now.Year.ToString

        Me.btn_mod_ope.Enabled = False
        btn_detope.Enabled = False
        Btn_AdjDoc.Enabled = False
        Btn_Anu.Enabled = False
        btn_inf_otg.Enabled = False
        btn_inf_eva.Enabled = False
        btn_inf_neg.Enabled = False


    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Buscar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010705, Usr, "PRESIONA BOTON BUSCAR OPERACIONES") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Ch_cliente.Checked Then
            If Txt_Rut_Cli.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese dígito verificador cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If Ch_deu.Checked Then
            If Txt_Rut_Deu.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese NIT deudor", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Dig_Deu.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese dígito verificador deudor", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If Trim(txt_fec_des.Text) = "" Then
            msj.Mensaje(Page, "Atención", "Ingrese fecha operación desde", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If Not IsDate(txt_fec_des.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha operación desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_fec_des.Text = ""
                Exit Sub
            End If
        End If

        If Trim(txt_fec_has.Text) = "" Then
            msj.Mensaje(Page, "Atención", "Ingrese fecha operación hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If Not IsDate(txt_fec_has.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha operación hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_fec_has.Text = ""
                Exit Sub
            End If
        End If

        If CDate(txt_fec_des.Text) > CDate(txt_fec_has.Text) Then
            msj.Mensaje(Page, "Atención", "Fecha operación desde no puede ser mayor a fecha operación hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If txt_oto_des.Text <> "" And txt_oto_has.Text <> "" Then
            If txt_oto_des.Text > txt_oto_has.Text Then
                msj.Mensaje(Page, "Atención", "Nº de otorgamiento desde no puede ser mayor a Nº de otorgamiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If txt_doc_des.Text <> "" And txt_doc_has.Text <> "" Then
            If txt_doc_des.Text > txt_doc_has.Text Then
                msj.Mensaje(Page, "Atención", "Nº de documento desde no puede ser mayor a Nº de documento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        If txt_venc_des.Text <> "" And txt_venc_has.Text <> "" Then
            If Not IsDate(txt_venc_des.Text) Then
                msj.Mensaje(Me, "Atención", "Fecha vencimiento desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_venc_des.Text = ""
                Exit Sub
            End If

            If Not IsDate(txt_venc_has.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha de vencimiento hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_venc_has.Text = ""
                Exit Sub
            End If
            If CDate(txt_venc_des.Text) > CDate(txt_venc_has.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha vencimiento desde no puede ser mayor a fecha vencimiento hasta", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If
        End If

        cargagrillas()

        Me.btn_mod_ope.Enabled = False

    End Sub

    Protected Sub btn_inf_eva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_inf_eva.Click

        Try


            Dim id_eva As HiddenField = CType(gr_otg.Rows(pos_otg.Value).FindControl("HF_ID_EVA"), HiddenField)


            Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(id_eva.Value)


            If abytFileData.Length <> 0 Then
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                Response.AddHeader("cache-control", "private")
                Response.AddHeader("Expires", "0")
                Response.AddHeader("Pragma", "cache")
                Response.AddHeader("content-disposition", "attachment; filename=Eva" & Txt_Rut_Cli.Text & "_" & id_eva.Value & ".pdf")
                Response.AddHeader("Accept-Ranges", "none")
                Response.BinaryWrite(abytFileData)
                Response.Flush()
                Response.End()
            End If


        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 2)
        End Try

    End Sub

    Protected Sub btn_inf_neg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_inf_neg.Click

        Try


            Dim id_opn As HiddenField = CType(gr_otg.Rows(pos_otg.Value).FindControl("HF_ID_OPN"), HiddenField)


            Dim abytFileData As Byte() = CA.DespliegaArchivoNegPDF(id_opn.Value)


            If abytFileData.Length <> 0 Then
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                Response.AddHeader("cache-control", "private")
                Response.AddHeader("Expires", "0")
                Response.AddHeader("Pragma", "cache")
                Response.AddHeader("content-disposition", "attachment; filename=Neg" & Txt_Rut_Cli.Text & "_" & id_opn.Value & ".pdf")
                Response.AddHeader("Accept-Ranges", "none")
                Response.BinaryWrite(abytFileData)
                Response.Flush()
                Response.End()
            End If


        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

#End Region
    
    Public Sub cargarGv_Dig(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing)

        hf_nro_pag_dig.Value = 1
        lb_pag_dig.Text = "Página " & hf_nro_pag_dig.Value & ""
        '1
        ClsSession.SesionOperaciones.coll_ope = OP.Operaciones_varios_criterios_Devuelve(RutCliente1, RutCliente2, fechaing, fechaing2, fecha_venc, fecha_venc2,
                                                              suc1, suc2, nrootg, nrootg2, tipo, nrodoc, nrodoc2, moneda1, moneda2, resp, resp2, rutdeu1, rutdeu2,
                                                              eje1, eje2, LlenaGrid, GV)
        Me.gr_dig.DataSource = ClsSession.SesionOperaciones.coll_ope
        Me.gr_dig.DataBind()

        For I = 0 To Me.gr_dig.Rows.Count - 1

            Me.gr_dig.Rows(I).Cells(1).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_dig.Rows(I).Cells(1).Text))
            If coll_ope.Item(I + 1).id_p_0023_fla = 1 Then
                Me.gr_dig.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(7).Text), fmt.FCMSD)
                Me.gr_dig.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(8).Text), fmt.FCMSD)
                Me.gr_dig.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(9).Text), fmt.FCMSD)

            ElseIf coll_ope.Item(I + 1).id_p_0023 > 2 Then
                Me.gr_dig.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(7).Text), fmt.FCMCD)
                Me.gr_dig.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(8).Text), fmt.FCMCD)
                Me.gr_dig.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(9).Text), fmt.FCMCD)

            ElseIf coll_ope.Item(I + 1).id_p_0023 = 2 Then

                Me.gr_dig.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(7).Text), fmt.FCMCD4)
                Me.gr_dig.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(8).Text), fmt.FCMCD4)
                Me.gr_dig.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_dig.Rows(I).Cells(9).Text), fmt.FCMCD4)


            End If
        Next
    End Sub

    Public Sub cargarGv_Sim(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing)
        hf_nro_pag_sim.Value = 1
        lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""
        '2
        ses_ope.coll_ope_sim = OP.Operaciones_varios_criterios_Devuelve(RutCliente1, RutCliente2, fechaing, fechaing2, fecha_venc, fecha_venc2,
                                                              suc1, suc2, nrootg, nrootg2, tipo, nrodoc, nrodoc2, moneda1, moneda2, resp, resp2, rutdeu1, rutdeu2,
                                                              eje1, eje2, LlenaGrid, GV)

        Me.gr_sim.DataSource = ses_ope.coll_ope_sim
        Me.gr_sim.DataBind()


        For I = 0 To Me.gr_sim.Rows.Count - 1

            Me.gr_sim.Rows(I).Cells(1).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_sim.Rows(I).Cells(1).Text))

            If coll_ope_sim.Item(I + 1).id_p_0023 = 1 Then
                Me.gr_sim.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(7).Text), fmt.FCMSD)
                Me.gr_sim.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(8).Text), fmt.FCMSD)
                Me.gr_sim.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(9).Text), fmt.FCMSD)
            ElseIf coll_ope_sim.Item(I + 1).id_p_0023 > 2 Then
                Me.gr_sim.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(7).Text), fmt.FCMCD)
                Me.gr_sim.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(8).Text), fmt.FCMCD)
                Me.gr_sim.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(9).Text), fmt.FCMCD)

            ElseIf coll_ope_sim.Item(I + 1).id_p_0023 = 2 Then

                Me.gr_sim.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(7).Text), fmt.FCMCD4)
                Me.gr_sim.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(8).Text), fmt.FCMCD4)
                Me.gr_sim.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_sim.Rows(I).Cells(9).Text), fmt.FCMCD4)


            End If
        Next
    End Sub

    Public Sub cargarGv_Otg(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing)

        hf_can_pag.Value = 1
        Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""
        '3
        ses_ope.coll_ope_otg = OP.Operaciones_varios_criterios_Devuelve(RutCliente1, RutCliente2, fechaing, fechaing2, fecha_venc, fecha_venc2,
                                                              suc1, suc2, nrootg, nrootg2, tipo, nrodoc, nrodoc2, moneda1, moneda2, resp, resp2, rutdeu1, rutdeu2,
                                                              eje1, eje2, LlenaGrid, GV)

        Me.gr_otg.DataSource = ses_ope.coll_ope_otg
        Me.gr_otg.DataBind()

        For I = 0 To Me.gr_otg.Rows.Count - 1

            Me.gr_otg.Rows(I).Cells(2).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(2).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_otg.Rows(I).Cells(2).Text))
            If coll_ope_otg.Item(I + 1).id_p_0023 = 1 Then
                Me.gr_otg.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(8).Text), fmt.FCMSD)
                Me.gr_otg.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(9).Text), fmt.FCMSD)
                Me.gr_otg.Rows(I).Cells(10).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(10).Text), fmt.FCMSD)

            ElseIf coll_ope_otg.Item(I + 1).id_p_0023 > 2 Then
                Me.gr_otg.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(8).Text), fmt.FCMCD)
                Me.gr_otg.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(9).Text), fmt.FCMCD)
                Me.gr_otg.Rows(I).Cells(10).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(10).Text), fmt.FCMCD)

            ElseIf coll_ope_otg.Item(I + 1).id_p_0023 = 2 Then
                Me.gr_otg.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(8).Text), fmt.FCMCD4)
                Me.gr_otg.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(9).Text), fmt.FCMCD4)
                Me.gr_otg.Rows(I).Cells(10).Text = Format(CDbl(Me.gr_otg.Rows(I).Cells(10).Text), fmt.FCMCD4)


            End If
        Next

    End Sub

    Public Sub cargarGv_Pag(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing)

        Hf_pag_pgd.Value = 1
        Lbl_pag_pgd.Text = "Página " & Hf_pag_pgd.Value & ""

        ses_ope.coll_ope_pag = OP.Operaciones_varios_criterios_Devuelve(RutCliente1, RutCliente2, fechaing, fechaing2, fecha_venc, fecha_venc2,
                                                              suc1, suc2, nrootg, nrootg2, tipo, nrodoc, nrodoc2, moneda1, moneda2, resp, resp2, rutdeu1, rutdeu2,
                                                              eje1, eje2, LlenaGrid, GV)
        Me.gr_pag.DataSource = ses_ope.coll_ope_pag
        Me.gr_pag.DataBind()


        For I = 0 To Me.gr_pag.Rows.Count - 1

            Me.gr_pag.Rows(I).Cells(1).Text = Format(CLng(Me.gr_pag.Rows(I).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_pag.Rows(I).Cells(1).Text))
            If ses_ope.coll_ope_pag.Item(I + 1).id_p_0023 = 1 Then
                Me.gr_pag.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(7).Text), fmt.FCMSD)
                Me.gr_pag.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(8).Text), fmt.FCMSD)
                Me.gr_pag.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(9).Text), fmt.FCMSD)

            ElseIf ses_ope.coll_ope_pag.Item(I + 1).id_p_0023 > 2 Then
                Me.gr_pag.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(7).Text), fmt.FCMCD)
                Me.gr_pag.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(8).Text), fmt.FCMCD)
                Me.gr_pag.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(9).Text), fmt.FCMCD)

            ElseIf ses_ope.coll_ope_pag.Item(I + 1).id_p_0023 = 2 Then

                Me.gr_pag.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(7).Text), fmt.FCMCD4)
                Me.gr_pag.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(8).Text), fmt.FCMCD4)
                Me.gr_pag.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_pag.Rows(I).Cells(9).Text), fmt.FCMCD4)


            End If
        Next
    End Sub

    Public Sub cargarGv_Anul(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing)

        Hf_pag_anu.Value = 1
        lbl_pag_anu.Text = "Página " & Hf_pag_anu.Value & ""

        sesion.coll_ope_anu = OP.Operaciones_varios_criterios_Devuelve(RutCliente1, RutCliente2, fechaing, fechaing2, fecha_venc, fecha_venc2,
                                                              suc1, suc2, nrootg, nrootg2, tipo, nrodoc, nrodoc2, moneda1, moneda2, resp, resp2, rutdeu1, rutdeu2,
                                                              eje1, eje2, LlenaGrid, GV)

        Me.gr_anul.DataSource = sesion.coll_ope_anu
        Me.gr_anul.DataBind()

        For I = 0 To Me.gr_anul.Rows.Count - 1

            Me.gr_anul.Rows(I).Cells(1).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_anul.Rows(I).Cells(1).Text))
            If coll_ope_anu.Item(I + 1).id_p_0023 = 1 Then
                Me.gr_anul.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(7).Text), fmt.FCMSD)
                Me.gr_anul.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(8).Text), fmt.FCMSD)
                Me.gr_anul.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(9).Text), fmt.FCMSD)

            ElseIf coll_ope_anu.Item(I + 1).id_p_0023 > 2 Then
                Me.gr_anul.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(7).Text), fmt.FCMCD)
                Me.gr_anul.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(8).Text), fmt.FCMCD)
                Me.gr_anul.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(9).Text), fmt.FCMCD)

            ElseIf coll_ope_anu.Item(I + 1).id_p_0023 = 2 Then

                Me.gr_anul.Rows(I).Cells(7).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(7).Text), fmt.FCMCD4)
                Me.gr_anul.Rows(I).Cells(8).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(8).Text), fmt.FCMCD4)
                Me.gr_anul.Rows(I).Cells(9).Text = Format(CDbl(Me.gr_anul.Rows(I).Cells(9).Text), fmt.FCMCD4)

            End If
        Next

    End Sub

    'Public Sub grillas()
    '    If Me.TabGrillas.ActiveTabIndex = 0 Then
    '        gr_anul.SelectedIndex = -1
    '        gr_otg.SelectedIndex = -1
    '        gr_pag.SelectedIndex = -1
    '        gr_sim.SelectedIndex = -1
    '        gr_dig.
    '        marcagrilla(Me.gr_dig, Me.Txt_ItemOPE)
    '    ElseIf Me.TabGrillas.ActiveTabIndex = 1 Then
    '        marcagrilla(Me.gr_sim, Me.pos_sim)
    '    ElseIf Me.TabGrillas.ActiveTabIndex = 2 Then
    '        marcagrilla(Me.gr_otg, Me.pos_otg)
    '    ElseIf Me.TabGrillas.ActiveTabIndex = 3 Then
    '        marcagrilla(Me.gr_pag, Me.pos_pag)
    '    ElseIf Me.TabGrillas.ActiveTabIndex = 4 Then
    '        marcagrilla(Me.gr_anul, Me.pos_anu)
    '    End If
    'End Sub
End Class