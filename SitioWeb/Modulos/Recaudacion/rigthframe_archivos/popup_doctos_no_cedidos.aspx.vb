Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_popup_doctos_no_cedidos
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim clasecli As New ClaseClientes
    Dim fc As New FuncionesGenerales.FComunes
    Dim msj As New ClsMensaje
    Dim LI As New FuncionesGenerales.ClsLocateInfo
    Dim REC As New ClaseRecaudación

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
            IB_AyudaDoc.Enabled = False
        End If

        txt_Contrato.Attributes.Add("readonly", "true")
        txt_nro_doc.Attributes.Add("readonly", "true")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1

        If Not Me.IsPostBack Then

            NroPaginacion = 0

            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Me.dr_tipdoc)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Me.dr_moneda)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, Me.dr_factoring)
            coll_nce = New Collection

            val_cli_deu.Value = Request.QueryString("tipo_persona")
            id_hre.Value = Request.QueryString("id_hre")

            If val_cli_deu.Value = "C" Then

                Me.Txt_Rut_Cli.Text = Request.QueryString("rut_cli")
                Me.Txt_Dig_Cli.Text = fc.Vrut(CInt(Me.Txt_Rut_Cli.Text))
                Me.Txt_Raz_Soc.Text = Request.QueryString("rso")

                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                IB_Ayudacli.Enabled = False

                Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
                Me.Txt_Rut_Deu.ReadOnly = False
                Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
                Me.Txt_Dig_Deu.ReadOnly = False
                'Me.Txt_Rso_Deu.CssClass = "clsMandatorio"
                Me.Txt_Rso_Deu.ReadOnly = True
                IB_AyudaDeu.Enabled = True

                IB_AyudaDoc.Enabled = True
                IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" & Txt_Rut_Cli.Text & "&DNC=" & 1 & "', 'PopUpCuentas Por Pagar',1220,610,200,150);")


            Else

                Me.Txt_Rut_Deu.Text = Format(CDbl(Request.QueryString("rut_cli")), "##,###,###")
                Me.Txt_Dig_Deu.Text = fc.Vrut(CLng(Me.Txt_Rut_Deu.Text))
                Me.Txt_Rso_Deu.Text = Request.QueryString("rso")
                IB_AyudaDeu.Enabled = False

                Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
                Me.Txt_Rut_Cli.ReadOnly = False
                Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
                Me.Txt_Dig_Cli.ReadOnly = False
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                IB_Ayudacli.Enabled = True

                'Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
                'Me.Txt_Rut_Deu.ReadOnly = True
                'Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
                Me.Txt_Dig_Deu.ReadOnly = True
                '     Me.Txt_Rso_Deu.CssClass = "clsMandatorio"
                Me.Txt_Rso_Deu.ReadOnly = True


                If Request.QueryString("rut_cli") <> "" Then
                    'IB_AyudaDeu.Enabled = False

                    Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                    Me.Txt_Rut_Deu.ReadOnly = True
                    Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                    Me.Txt_Dig_Deu.ReadOnly = True

                End If

            End If

            alinea_textos()

        End If

        IB_Ayudacli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?PopUp=1','PopUpCliente',650,410,200,150);")
        'BTN_VOLVER.Attributes.Add("onclick", "CerrarVentana('ctl00$ContentPlaceHolder1$LB_NCE');")
        'BTN_VOLVER.Attributes.Add("onclick", "NCedidos();")
        BTN_VOLVER.Attributes.Add("onclick", "window.close();")
        'BTN_VOLVER.Attributes.Add("onclick", "ClosePopUp();")

        IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/Ayudadeudor.aspx','PopUpCliente',580,410,200,150);")

    End Sub

    Public Sub alinea_textos()

        Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_mto_doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_nro_doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_nro_doc2.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_Contrato.Attributes.Add("Style", "TEXT-ALIGN: right")
        If Me.val_cli_deu.Value = "C" Then
            Me.Txt_Rut_Cli.Focus()
        Else
            Me.Txt_Rut_Deu.Focus()
        End If

    End Sub

    Protected Sub lb_cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cli.Click

        Dim cli As cli_cls
        Dim rel As Boolean

        cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

        Session("Cliente") = cli

        If valida_cliente <> "" Then
            msj.Mensaje(Me.Page, "Atención", valida_cliente, 3)
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Rut_Cli.Text = ""
            Exit Sub
        Else

            rel = clasecli.RelacionClienteDeudorDevuelve(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), "A", Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT))
            If rel = False Then
                msj.Mensaje(Me.Page, "Atención", "Relación cliente Deudor no Existe ¿Desea crearla?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_rel_deu.UniqueID)

                Exit Sub
            End If

            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"

            'Asigna Razon Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            Me.btn_guardar.Enabled = True
            IB_Ayudacli.Enabled = False
            IB_AyudaDoc.Enabled = True
            IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" & Txt_Rut_Cli.Text & "&DNC=" & 1 & "', 'PopUpCuentas Por Pagar',1220,610,200,150);")

        End If





    End Sub

    Protected Sub lb_deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_deu.Click


        Dim deu As deu_cls
        Dim rel As Boolean
        deu = CG.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

        Session("Deudor") = deu

        If Not IsNothing(deu) Then
            'Datos Deudor
            rel = clasecli.RelacionClienteDeudorDevuelve(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), "A", Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT))
            If rel = False Then
                msj.Mensaje(Me.Page, "Atención", "Relación cliente Deudor no Existe", 2)
                Me.Txt_Dig_Deu.Text = ""
                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Rut_Deu.Focus()
                Exit Sub
            End If
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            If deu.id_P_0044 = 1 Then
                Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            Else
                Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso)
            End If

            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True

            Me.btn_guardar.Enabled = True
        End If


    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click
        Try

            If txt_Contrato.Text = "" Then
                msj.Mensaje(Page, "Atención", "Seleccione contrato desde la ayuda", TipoDeMensaje._Exclamacion, False)
                Exit Sub
            End If

            If txt_nro_doc2.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese nro. Documento", TipoDeMensaje._Exclamacion, False)
                Exit Sub
            End If

            If dr_tipdoc.SelectedIndex = 0 Then
                msj.Mensaje(Page, "Atención", "Seleccione Tipo de Documento", TipoDeMensaje._Exclamacion, False)
                Exit Sub
            End If

            If txt_fec_vcto.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese fecha de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            If Not IsDate(txt_fec_vcto.Text) Then
                txt_fec_vcto.Text = ""
                msj.Mensaje(Page, "Atención", "Fecha erronea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub

            End If



            If ch_fct.Checked = False Then
                If dr_factoring.SelectedValue = 0 Then
                    msj.Mensaje(Page, "Atención", "Seleccione factoring", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                    Exit Sub
                End If
            End If


            If txt_obs.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese observación", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            msj.Mensaje(Page, "Atención", "¿Desea guardar los cambios?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CARGA_OBJ_GRILLA()

        Dim obj As New obj_rec
        Dim col As New Collection
        obj.NOMBRE_DEUDOR = Me.Txt_Rso_Deu.Text '1
        obj.RUT_DEUDOR = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)  '2 
        obj.T_DOCTO = Me.dr_tipdoc.SelectedValue    '3 
        obj.N_DOCTO = Me.txt_nro_doc2.Text         '4 
        obj.RUT_CLI = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)          '5
        obj.N_CLIENTE = Me.Txt_Raz_Soc.Text          '6
        obj.N_OPERACION = Nothing
        obj.D_CEDIDO = "N" '8   

        If val_cli_deu.Value = "D" Then
            obj.S_DEUDOR = Me.txt_mto_doc.Text
            obj.S_CLIENTE = 0
        Else
            obj.S_DEUDOR = 0
            obj.S_CLIENTE = Me.txt_mto_doc.Text
        End If
        '10
        obj.T_MONEDA = Me.dr_moneda.SelectedValue    '11
        obj.D_MONEDA = Me.dr_moneda.Text       '12
        obj.MTO_ANTICIPO = 0 '13
        obj.TS_APLICACION = 0
        obj.F_VCTO = CDate(Me.txt_fec_vcto.Text)
        obj.E_DOCTO = 1                         '15
        obj.DOC_MTO = Me.txt_mto_doc.Text                             '16
        obj.FEC_VCTO = CDate(Me.txt_fec_vcto.Text)                      '17
        obj.FECHA_SIM = Nothing                   '18
        obj.DIF_PRECIO = 0                      '19
        obj.CANT_DIAS = 0                      '20
        obj.TASA_MOA = "M"   '21
        obj.OPER_LNL = 0                         '22
        obj.TASA_REN = 0                       '23
        obj.FVTO_ORI = CDate(Me.txt_fec_vcto.Text)                         '24
        obj.NRO_REN = 0                      '25
        obj.MTO_RECAUDADO = Me.txt_mto_doc.Text '26
        obj.MON_DOCTO = Me.txt_mto_doc.Text '26
        obj.INTERES = 0                          '27
        obj.MTO_A_RECAUDAR = val_mto_rec.Value   '28
        obj.TIPO_DOC_REC = "C"                   '29
        obj.TIPO_PAGO = "T"                      '30
        obj.ID_NCE = ID_NCE.Value                '31
        col = CG.Parametros_Detalle_Devuelve(31, dr_tipdoc.SelectedValue)
        obj.DES_TIP_DOC = col.Item(1).pnu_atr_003
        obj.CONTRATO = Me.txt_Contrato.Text.Trim
        coll_nce.Add(obj)

    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        Dim coll As New Collection

        If Txt_Rut_Cli.Text = "" Then
            msj.Mensaje(Page, "Atención", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
            Exit Sub
        End If

        coll = CG.DocumentosNoCedidos_Devuelve(CInt(Me.Txt_Rut_Cli.Text), CInt(Me.Txt_Rut_Deu.Text), 0)

        If Not IsNothing(coll) Then
            If coll.Count = 0 Then
                msj.Mensaje(Page, "Atención", "No se encontraron datos", ClsMensaje.TipoDeMensaje._Exclamacion, , False)

            End If
        End If

    End Sub

    Private Sub AceptaNuevaRelacionClienteDeudor()
        Dim ddr As New ddr_cls
        Dim e As New System.EventArgs
        Try

            With ddr
                .cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
                .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)

            End With

            AG.ClientesDeudoresInserta(ddr)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ch_fct_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ch_fct.CheckedChanged
        If Me.ch_fct.Checked = False Then

            Me.dr_factoring.Enabled = True
            Me.dr_factoring.CssClass = "clsMandatorio"
        Else
            Me.dr_factoring.Enabled = False
            Me.dr_factoring.CssClass = "clsDisabled"
        End If
    End Sub

    Protected Sub txt_dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        If Me.Txt_Dig_Cli.Text <> "" Then
            lb_cli_Click(Me, e)
        End If

    End Sub

    Protected Sub lb_rel_deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_rel_deu.Click

        AceptaNuevaRelacionClienteDeudor()
        Me.btn_guardar.Enabled = True

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        Try

            Dim nce As New nce_cls
            Dim cta As New ClaseCuentas

            With nce

                .cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
                .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)
                .id_p_0031 = Me.dr_tipdoc.SelectedValue
                .id_p_0023 = Me.dr_moneda.SelectedValue
                If Me.ch_fct.Checked Then
                    .id_PL_000069 = Nothing
                Else

                    .id_PL_000069 = Me.dr_factoring.SelectedValue
                End If
                .nce_fec_gen = Date.Now.ToShortDateString
                .nce_fec_ing = Date.Now.ToShortDateString
                .nce_obs = Me.txt_obs.Text
                .id_doc = cta.NroDoctoDevuelve(Me.txt_nro_doc.Text)
                .nce_num_doc = Me.txt_nro_doc2.Text
                .nce_fec_vcto = Me.txt_fec_vcto.Text
                .fac_cam = CG.ParidadDevuelve(Me.dr_moneda.SelectedValue, Date.Now.ToShortDateString).par_val
                .nce_mto = Me.txt_mto_doc.Text
                .id_hre = id_hre.Value
                .nce_est_nom = "N"
                'Se agrega nce_pro = "N" para que rescate datos en la consulta 
                .nce_pro = "N"

            End With
            val_mto_rec.Value = Me.txt_mto_doc.Text
            ID_NCE.Value = REC.Doctos_no_cedidos_guarda(nce)

            CARGA_OBJ_GRILLA()

            '  Llena_Grilla()
            msj.Mensaje(Me.Page, "Atención", "Se ha guardado el documento, presione volver para regresar a Recaudación", 3)


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        lb_deu_Click(Me, e)
    End Sub

End Class
