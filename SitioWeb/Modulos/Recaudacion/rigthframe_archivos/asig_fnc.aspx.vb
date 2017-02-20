Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_asig_fnc
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim msj As New ClsMensaje
    Dim fmt As New FuncionesGenerales.Variables
    Dim fm As New FuncionesGenerales.ClsLocateInfo
    Dim clasecli As New ClaseClientes
    Dim fc As New FuncionesGenerales.FComunes
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim REC As New ClaseRecaudación

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
            cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, Me.Dr_fact)
            Me.Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Ayuda = False
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

        End If

    End Sub

    Protected Sub Ch_cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cli.CheckedChanged

        If Me.Ch_cli.Checked Then

            Me.Txt_RUT_CLI.ReadOnly = False
            Me.Txt_RUT_CLI.Text = ""
            Me.Txt_RUT_CLI.CssClass = "clsMandatorio"

            Me.Txt_Rut_Cli_MaskedEditExtender.Enabled = True

            Me.Txt_DIG_CLI.ReadOnly = False
            Me.Txt_DIG_CLI.Text = ""
            Me.Txt_DIG_CLI.CssClass = "clsMandatorio"

            Me.Txt_RAZ_SOC.Text = ""
            Me.Txt_RUT_CLI.Focus()
        Else

            Me.Txt_RUT_CLI.ReadOnly = True
            Me.Txt_RUT_CLI.Text = ""
            Me.Txt_RUT_CLI.CssClass = "clsDisabled"

            Me.Txt_Rut_Cli_MaskedEditExtender.Enabled = False

            Me.Txt_DIG_CLI.ReadOnly = True
            Me.Txt_DIG_CLI.Text = ""
            Me.Txt_DIG_CLI.CssClass = "clsDisabled"

            Me.Txt_RAZ_SOC.Text = ""


        End If
    End Sub

    Public Sub ValidaCliente()
        Try
            Dim cli As cli_cls
            cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)


            Session("Cliente") = cli

            If valida_cliente <> "" Then

                msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Me.Txt_Rut_Cli.Text = ""
                Me.Txt_Dig_Cli.Text = ""

                Exit Sub
            Else

                If IsNothing(cli) Then
                    msj.Mensaje(Me, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
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

                'Asigna Razón Social / Nombre a Campo Cliente
                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click

        Try


            Dim cli_idc1 As String, cli_idc2 As String, est_nom1 As String, est_nom2 As String
            Dim fecha_dde As Date, fecha_hta As Date

            If Me.Ch_cli.Checked Then

                If Txt_Rut_Cli.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Ingrese NIT", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Txt_Dig_Cli.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Ingrese dígito", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                cli_idc1 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
                cli_idc2 = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)

            Else
                cli_idc1 = "000000000000"
                cli_idc2 = "9999999999999"
            End If

            fecha_dde = "01/01/1900"
            fecha_hta = "31/12/2999"

            If Me.Dr_fact.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar un Factoring", ClsMensaje.TipoDeMensaje._Exclamacion, False)
                Exit Sub
            End If

            est_nom1 = "A"
            est_nom2 = "Z"

            Dim Agt As New Perfiles.Cls_Principal

            If Agt.ValidaAccesso(20, 20010508, Usr, "PRESIONO BOTON BUSCAR DOCUMENTOS") = False Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            coll_DNC = New Collection
            coll_DNC = REC.nce_fnc_devuelve(cli_idc1, cli_idc2, Me.Dr_fact.SelectedValue, fecha_dde, fecha_hta, est_nom1, est_nom2, 1)

            Me.gr_fnc.DataSource = coll_DNC
            Me.gr_fnc.DataBind()

            If coll_DNC.Count = 0 Then

                msj.Mensaje(Me.Page, "Atención", "No se encuentran documentos segun los criterios de busqueda", ClsMensaje.TipoDeMensaje._Exclamacion)

            End If

            formatogrilla()

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion, False)
        End Try

    End Sub

    Public Sub formatogrilla()

        For i = 0 To Me.gr_fnc.Rows.Count - 1

            'Numero doc

            Me.gr_fnc.Rows(i).Cells(2).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(2).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(2).HorizontalAlign = HorizontalAlign.Right

            'MONTO DOC
            If coll_DNC.Item(i + 1).id_p_0023 = 1 Then

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMSD)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            ElseIf coll_DNC.Item(i + 1).id_p_0023 = 3 Or coll_DNC.Item(i + 1).id_p_0023 = 4 Then

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMCD)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            Else

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMCD4)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If

            'FECHA 
            Me.gr_fnc.Rows(i).Cells(5).Text = Format(CDate(Me.gr_fnc.Rows(i).Cells(5).Text), fmt.FMT_FECHA)

            'RUT CLI
            Me.gr_fnc.Rows(i).Cells(7).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(7).Text), fm.FCMSD) & "-" & fc.Vrut(CDbl(Me.gr_fnc.Rows(i).Cells(7).Text))
            Me.gr_fnc.Rows(i).Cells(7).HorizontalAlign = HorizontalAlign.Right

            'RUT DEU

            Me.gr_fnc.Rows(i).Cells(9).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(9).Text), fm.FCMSD) & "-" & fc.Vrut(CDbl(Me.gr_fnc.Rows(i).Cells(9).Text))
            Me.gr_fnc.Rows(i).Cells(9).HorizontalAlign = HorizontalAlign.Right

            'FECHA ING
            Me.gr_fnc.Rows(i).Cells(11).Text = Format(CDate(Me.gr_fnc.Rows(i).Cells(11).Text), fmt.FMT_FECHA)

            'N ING
            Me.gr_fnc.Rows(i).Cells(13).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(13).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(13).HorizontalAlign = HorizontalAlign.Right

            'N HOJA
            Me.gr_fnc.Rows(i).Cells(14).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(14).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(14).HorizontalAlign = HorizontalAlign.Right

            'FACTOR DE CAMBIO
            If coll_DNC.Item(i + 1).id_p_0023 = 1 Then

                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMSD)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            ElseIf coll_DNC.Item(i + 1).id_p_0023 = 3 Or coll_DNC.Item(i + 1).id_p_0023 = 4 Then

                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            Else
                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD4)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            End If




        Next

    End Sub

    Protected Sub btn_gen_nom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gen_nom.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20040508, Usr, "PRESIONO BOTON GENERAR NOMINA DOCUMENTOS") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If



        Coll_DOC = New Collection



        For i = 0 To Me.gr_fnc.Rows.Count - 1

            Dim ch As CheckBox

            ch = Me.gr_fnc.Rows(i).FindControl("ch_doc")

            If ch.Checked Then
                Coll_DOC.Add(coll_DNC.Item(i + 1).id_nce)
            End If


        Next

        If Coll_DOC.Count = 0 Then
            msj.Mensaje(Me.Page, "Atención", "Debes seleccionar algun documento para generar la Nomina", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        caso = 0
        rw.AbrePopup(Me, 2, "report_nomina_nce.aspx", "Nomina no Cedidos", 1100, 900, 100, 50)

    End Sub

    Protected Sub btn_ing_fac_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ing_fac.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20030508, Usr, "PRESIONO BOTON INGRESAR FACTORING") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        rw.AbrePopup(Me, 2, "ing_factoring.aspx", "Ingreso Factoring", 620, 170, 100, 50)
    End Sub

    Protected Sub btn_cons_nom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cons_nom.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20050508, Usr, "PRESIONO BOTON CONSULTAR NOMINAS") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        rw.AbrePopup(Me, 2, "pago_anulación_dnc.aspx", "Ingreso Factoring", 980, 660, 100, 50)
    End Sub

    Protected Sub Txt_DIG_CLI_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_DIG_CLI.TextChanged

        ValidaCliente()

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        Me.Txt_RUT_CLI.ReadOnly = True
        Me.Txt_RUT_CLI.Text = ""
        Me.Txt_RUT_CLI.CssClass = "clsDisabled"

        Me.Ch_cli.Checked = False
        Me.Txt_DIG_CLI.ReadOnly = True
        Me.Txt_DIG_CLI.Text = ""
        Me.Txt_DIG_CLI.CssClass = "clsDisabled"

        Me.Dr_fact.ClearSelection()

        Me.Txt_RAZ_SOC.Text = ""
        Me.Ch_cli.Checked = False

        coll_DNC = New Collection

        Me.gr_fnc.DataSource = Nothing
        Me.gr_fnc.DataBind()

    End Sub

    Protected Sub btn_asig_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asig.Click

        rw.AbrePopup(Me, 2, "Asign_ftring.aspx", "Asignación Factoring", 880, 600, 100, 50)

        'If Me.Ch_cli.Checked Then
        '    RUT_CLI_RPT = Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT)
        '    rw.AbrePopup(Me, 2, "Asign_ftring.aspx", "Asignación Factoring", 880, 600, 100, 50)
        'Else
        '    msj.Mensaje(Page, "Atención", "Debe seleccionar cliente para asignar", ClsMensaje.TipoDeMensaje._Exclamacion)
        'End If

    End Sub

    Protected Sub IB_SeleccionDoctos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

End Class
