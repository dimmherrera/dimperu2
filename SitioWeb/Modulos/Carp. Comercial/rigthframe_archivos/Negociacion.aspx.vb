Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports CapaDatos
Imports Microsoft.Reporting.WebForms
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.FComunes
Imports System.Transactions
Imports System.IO

Partial Class FrmNegociacion
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim Caption As String = "Negociación"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim CMC As New ClaseComercial
    Dim CA As New ClaseArchivos
    Dim var As New FuncionesGenerales.Variables
#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Response.Cache.SetNoStore()
            Sesion.NroPaginacion = 0

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Focus()

            If Not IsNothing(Session("Cliente")) Then

                Dim cli As cli_cls

                cli = Session("Cliente")

                Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                If CargaDatosCliente() Then

                    CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                    If GV_Negociacion.Rows.Count <= 0 Then
                        Msj.Mensaje(Me.Page, Caption, "No posee negociaciones", TipoDeMensaje._Informacion)
                        'Si cliente existe se habilitan botones
                        IB_Nuevo.Enabled = True
                        IB_Buscar.Enabled = False
                        HF_Nro_Neg.Value = ""
                    Else
                        HF_Nro_Neg.Value = ""
                        IB_Nuevo.Enabled = True
                        IB_Buscar.Enabled = False
                    End If

                End If

            End If

        Else

            If Not IsNothing(Session("Cliente")) Then

                Dim cli As cli_cls

                cli = Session("Cliente")

                Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                CargaGrilla()

            End If

            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
        'IB_Nuevo.Attributes.Add("OnClick", "WinOpen(1, 'PopUpNegociacion.aspx','PopUpNegociacion',1280,1030,0,0);")

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            If CargaDatosCliente() Then

                CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                If GV_Negociacion.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No posee negociaciones", TipoDeMensaje._Informacion)
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_Negociacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Negociacion.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    Dim btn As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)

        '    'btn.Attributes.Add("onClick", "WinOpen(1, 'PopUpNegociacion.aspx?nro=" & btn.ToolTip & "','PopUpNegociacion',1280,1030,0,0);")
        '    btn.Attributes.Add("onClick", "Response.Redirect('PopUpNegociacion.aspx')")
        'End If

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Comercial"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If CargaDatosCliente() Then
                Sesion.NroPaginacion = 0
                CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                If GV_Negociacion.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No posee negociaciones", TipoDeMensaje._Informacion)
                Else
                    IB_Buscar.Enabled = False
                    IB_AyudaCli.Enabled = False
                End If

                IB_Nuevo.Enabled = True


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LB_Refescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refescar.Click

        Try

            'UpdatePanel1.Update()

            CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

            If GV_Negociacion.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No posee negociaciones", TipoDeMensaje._Informacion)
            Else
                IB_Buscar.Enabled = False
                IB_AyudaCli.Enabled = False
            End If

            HF_Nro_Neg.Value = ""

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 15 Then
            NroPaginacion -= 15
            CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Negociacion.Rows.Count < 15 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Negociacion.Rows.Count = 15 Then
            NroPaginacion += 15
            CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)
        End If
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20020404, Usr, "PRESIONO NUEVA Negociación") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'RW.AbrePopup(Me, 1, "PopUpNegociacion.aspx", "PopUpNegociacion", 1280, 1030, 0, 0)
        Response.Redirect("PopUpNegociacion.aspx")
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        Response.Redirect("PopUpNegociacion.aspx?nro=" & btn.ToolTip)
       
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click 'Handles IB_Buscar.Click
        CargaGrilla()

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        IB_Buscar.Enabled = True
        IB_Nuevo.Enabled = False

        Me.IB_AyudaCli.Enabled = True
        'Limpiamos TextBox
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Sucursal.Text = ""
        Txt_Ejecutivo.Text = ""
        Txt_Banca.Text = ""
        Txt_TipoCliente.Text = ""


        GV_Negociacion.DataSource = Nothing
        GV_Negociacion.DataBind()

        Txt_Rut_Cli.ReadOnly = False
        Txt_Dig_Cli.ReadOnly = False

        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"

        Txt_Rut_Cli.Focus()
        HF_Nro_Neg.Value = ""
        IB_AyudaCli.Enabled = True

        Session("Cliente") = Nothing

    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Function
            End If


            If IsNothing(CLI) Then
                Msj.Mensaje(Me.Page, Caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                Exit Function
            End If


            Session("Cliente") = CLI

            Txt_TipoCliente.Text = CLI.P_0044_cls.pnu_des.Trim

            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            'Banca
            If Not IsNothing(CLI.PL_000066_cls) Then
                Me.Txt_Banca.Text = CLI.PL_000066_cls.pal_des.Trim
            End If

            'Sucursal
            If Not IsNothing(CLI.suc_cls) Then
                Me.Txt_Sucursal.Text = CLI.suc_cls.suc_des_cra
            End If

            'Ejecutivo
            If Not IsNothing(CLI.eje_cls) Then
                Me.Txt_Ejecutivo.Text = CLI.eje_cls.eje_nom.Trim
            End If

            HabilitaDesabilitaCliente(False)

            'Deja el Rut y Nombre del cliente en la cabecera 
            'LB_Cliente.text = RG.FormatoMiles(Txt_Rut_Cli.text) & "-" & Txt_Dig_Cli.text & "   " & Me.Txt_Cliente.text

            'Cambia a AcordionPanel de Negociaciones Anteriores
            'Accordion1.SelectedIndex = 1
            IB_AyudaCli.Enabled = False
            Return True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Private Sub HabilitaDesabilitaCliente(ByVal Estado As Boolean)

        Txt_Rut_Cli.ReadOnly = Not Estado
        Txt_Dig_Cli.ReadOnly = Not Estado

        If Not Estado Then
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
        Else
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
        End If


    End Sub

    Private Sub CargaGrilla()
        Try

            If Not agt.ValidaAccesso(20, 20010404, Usr, "PRESIONO BUSCAR Negociación") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CargaDatosCliente() Then
                Sesion.NroPaginacion = 0
                CMC.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                If GV_Negociacion.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No posee negociaciones", TipoDeMensaje._Informacion)
                Else
                    IB_Buscar.Enabled = False
                    IB_AyudaCli.Enabled = False
                End If

                IB_Nuevo.Enabled = True

            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class
