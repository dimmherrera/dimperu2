Imports CapaDatos
Partial Class Modulos_Gestion_rigthframe_archivos_Gestion
    Inherits System.Web.UI.Page
    Dim msj As New ClsMensaje
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim SESION As New ClsSession.ClsSession
    Dim fmt As New FuncionesGenerales.Variables
    Dim clasecli As New ClaseClientes

    Protected Sub Dr_tip_inf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_tip_inf.SelectedIndexChanged
        If Me.Dr_tip_inf.SelectedValue = 1 Then

            Me.Txt_fec_dde.ReadOnly = False
            Me.Txt_fec_dde.CssClass = "clsMandatorio"
            Me.Txt_fec_dde.Text = ""
            Me.Txt_fec_dde.Enabled = True

            Me.Txt_fec_hta.ReadOnly = False
            Me.Txt_fec_hta.CssClass = "clsMandatorio"
            Me.Txt_fec_hta.Text = ""
            Me.Txt_fec_hta.Enabled = True

            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Rut_Cli.Enabled = False

            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.Enabled = False
            Me.Txt_Dig_Cli.ReadOnly = True

            Me.Txt_Per.CssClass = "clsDisabled"
            Me.Txt_Per.Text = ""
            Me.Txt_Per.ReadOnly = True
            Me.Txt_Per.Enabled = False

            Me.Txt_Raz_Soc.Text = ""

            Me.IB_AyudaCli.Enabled = False


        ElseIf Me.Dr_tip_inf.SelectedValue = 4 Or Me.Dr_tip_inf.SelectedValue = 5 Then


            Me.Txt_fec_dde.ReadOnly = True
            Me.Txt_fec_dde.CssClass = "clsDisabled"
            Me.Txt_fec_dde.Text = ""

            Me.Txt_fec_hta.ReadOnly = True
            Me.Txt_fec_hta.CssClass = "clsDisabled"
            Me.Txt_fec_hta.Text = Date.Now.ToShortDateString

            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.Enabled = False

            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.Enabled = False

            Me.Txt_Per.CssClass = "clsDisabled"
            Me.Txt_Per.Text = ""
            Me.Txt_Per.ReadOnly = True
            Me.Txt_Per.Enabled = False

            Me.Txt_Raz_Soc.Text = ""
            Me.Txt_fec_dde.Enabled = False
            Me.Txt_fec_hta.Enabled = False
            Me.Txt_fec_dde.ReadOnly = True
            Me.Txt_fec_hta.ReadOnly = True

            Me.IB_AyudaCli.Enabled = False


        ElseIf Me.Dr_tip_inf.SelectedValue = 7 Then

            Me.Txt_fec_dde.ReadOnly = True
            Me.Txt_fec_dde.CssClass = "clsDisabled"
            Me.Txt_fec_dde.Text = ""
            Me.Txt_fec_dde.Enabled = False

            Me.Txt_fec_hta.ReadOnly = False
            Me.Txt_fec_hta.CssClass = "clsDisabled"
            Me.Txt_fec_hta.Text = ""
            Me.Txt_fec_hta.Enabled = False

            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.Enabled = True
            Me.IB_AyudaCli.Enabled = True


            Me.Txt_Per.ReadOnly = False
            Me.Txt_Per.CssClass = "clsMandatorio"
            Me.Txt_Per.Text = ""
            Me.Txt_Per.Enabled = True


            Me.Txt_Dig_Cli.ReadOnly = False
            Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.Enabled = True
            Me.Txt_Raz_Soc.Text = ""

        Else

            Me.Txt_fec_dde.ReadOnly = False
            Me.Txt_fec_dde.CssClass = "clsMandatorio"
            Me.Txt_fec_dde.Text = ""
            Me.Txt_fec_dde.Enabled = True
            Me.Txt_fec_hta.ReadOnly = False
            Me.Txt_fec_hta.CssClass = "clsMandatorio"
            Me.Txt_fec_hta.Text = ""
            Me.Txt_fec_hta.Enabled = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.Enabled = False
            Me.Txt_Rut_Cli.ReadOnly = True 'FY 07-05-2012
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.Enabled = False
            Me.Txt_Dig_Cli.ReadOnly = True 'FY 07-05-2012
            Me.Txt_Per.CssClass = "clsDisabled"
            Me.Txt_Per.Text = ""
            Me.Txt_Per.Enabled = False

            Me.IB_AyudaCli.Enabled = False

        End If
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Select Case Dr_tip_inf.SelectedValue
            Case 0
                msj.Mensaje(Me, "Atención", "Debe seleccionar un informe para generar", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Case 7
                If Me.Txt_Rut_Cli.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar el nit para generar el informe ", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_Dig_Cli.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar el digito para generar el informe ", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_Per.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar el periodo para generar el informe ", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_Per.Text < 1900 Then
                    msj.Mensaje(Me, "Atención", "Periodo no corresponde a un valor valido", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            Case 4, 5
                'no se realizan validaciones

            Case Else

                If Me.Txt_fec_dde.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar una fecha desde para generar el informe. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    If IsDate(Me.Txt_fec_dde.Text) = False Then 'FY 07-05-2012
                        msj.Mensaje(Me, "Atención", "ingrese fecha valida ", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If

                If Me.Txt_fec_hta.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar una fecha hasta para generar el informe. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    If IsDate(Me.Txt_fec_hta.Text) = False Then 'FY 07-05-2012
                        msj.Mensaje(Me, "Atención", "ingrese fecha valida ", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If

        End Select

        If Me.Txt_Rut_Cli.Text = "" Then
            Me.Txt_Rut_Cli.Text = 0
        End If

        rw.AbrePopup(Me, 1, "Inf_gestion.aspx?fec1=" & Me.Txt_fec_dde.Text & _
                                            "&fec2=" & Me.Txt_fec_hta.Text & _
                                            "&periodo=" & Me.Txt_Per.Text & _
                                            "&RutCliente=" & Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT) & "&tipo=" & Me.Dr_tip_inf.SelectedValue & " ", "InformesGestion", 1200, 1000, 50, 100)

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            SESION.Modulo = "Gestion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Me.Dr_tip_inf.SelectedValue = 0

        Me.Txt_fec_dde.ReadOnly = True
        Me.Txt_fec_dde.CssClass = "clsDisabled"
        Me.Txt_fec_dde.Text = ""
        Me.Txt_fec_dde.Enabled = False

        Me.Txt_fec_hta.ReadOnly = True
        Me.Txt_fec_hta.CssClass = "clsDisabled"
        Me.Txt_fec_hta.Text = ""
        Me.Txt_fec_hta.Enabled = False

        Me.Txt_Rut_Cli.ReadOnly = True
        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Rut_Cli.Text = ""


        Me.Txt_Dig_Cli.ReadOnly = True
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.Enabled = False


        Me.Txt_Raz_Soc.ReadOnly = True
        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
        Me.Txt_Raz_Soc.Text = ""
        Me.Txt_Raz_Soc.Enabled = False

        Me.Txt_Per.ReadOnly = True
        Me.Txt_Per.CssClass = "clsDisabled"
        Me.Txt_Per.Text = ""
        Me.Txt_Per.Enabled = False


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Dim Cli As cli_cls

        Cli = clasecli.ClientesDevuelve(Txt_Rut_Cli.Text.Trim, Txt_Dig_Cli.Text)

        'Validaciones de RUT y Digito Verificador
        'si cliente no existe sale de  function

        If IsNothing(Cli) Then
            msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Exit Sub
        End If

        Session("Cliente") = Cli

        'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
        Me.Txt_Rut_Cli.ReadOnly = True
        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Dig_Cli.ReadOnly = True
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
        IB_AyudaCli.Enabled = False
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False

        'Asigna Razón Social / Nombre a Campo Cliente
        Me.Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)
    End Sub
End Class
