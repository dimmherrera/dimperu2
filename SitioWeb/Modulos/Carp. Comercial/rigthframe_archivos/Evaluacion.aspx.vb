Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class _Default
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Evaluación"
    Dim CG As New ConsultasGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim CMC As New ClaseComercial
    Dim CA As New ClaseArchivos
#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            If Not agt.ValidaAccesso(20, 20010304, Usr, "PRESIONO BUSCAR EVALUACIONES") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CargaDatosCliente() Then

                CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

                If GV_Evaluaciones.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No se encontraron evaluaciones", TipoDeMensaje._Informacion)
                Else

                    IB_Buscar.Enabled = False
                    IB_AyudaCli.Enabled = False

                End If

                IB_Nuevo.Enabled = True

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try
            IB_AyudaCli.Enabled = True
            'Limpiamos TextBox
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Sucursal.Text = ""
            Txt_Ejecutivo.Text = ""
            Txt_TipoCliente.Text = ""
            Txt_Banca.Text = ""


            GV_Evaluaciones.DataSource = Nothing
            GV_Evaluaciones.DataBind()

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Session("Cliente") = Nothing

            Txt_Rut_Cli.Focus()

            Me.IB_Nuevo.Enabled = False
            Me.IB_Buscar.Enabled = True
            IB_AyudaCli.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20020304, Usr, "PRESIONO NUEVA EVALUACION") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Response.Redirect("EvaCliDeu.aspx", False)

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Page, Caption, "Ya a llegado al comienzo de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If NroPaginacion >= 15 Then
            NroPaginacion -= 15
            Carga_GrillaEvaluacion()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Evaluaciones.Rows.Count < 15 Then
            Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        If GV_Evaluaciones.Rows.Count = 15 Then
            NroPaginacion += 15
            Carga_GrillaEvaluacion()
        End If

    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
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
                Me.Txt_Ejecutivo.Text = CLI.eje_cls.eje_nom
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Response.Cache.SetNoStore()
            NroPaginacion = 0
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Focus()

            Session("Coll_RSD") = New Collection


            If Not IsNothing(Session("Cliente")) Then

                Dim cli As cli_cls

                cli = Session("Cliente")

                Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                If CargaDatosCliente() Then

                    CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

                    If GV_Evaluaciones.Rows.Count <= 0 Then
                        Msj.Mensaje(Me.Page, Caption, "No se encontraron evaluaciones", TipoDeMensaje._Informacion)
                        'Se habiltan Botones en caso que cliente exista
                        IB_Buscar.Enabled = False
                    Else
                        IB_Buscar.Enabled = False
                    End If

                    IB_Nuevo.Enabled = True

                End If

            End If

        End If

        SW = 0

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_buscar.Click

        Try

            If CargaDatosCliente() Then

                CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

                If GV_Evaluaciones.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No se encontrar evaluaciones", TipoDeMensaje._Informacion)
                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_Evaluaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Evaluaciones.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Evaluaciones, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Evaluaciones, 'formatable')")
            'e.Row.Attributes.Add("onClick", "ClickEvaluacion(ctl00_ContentPlaceHolder1_GV_Evaluaciones, 'clicktable', 'formatable', 'selectable')")
        End If

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

                CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

                If GV_Evaluaciones.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No se encontraron evaluaciones", TipoDeMensaje._Informacion)
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

            CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

            If GV_Evaluaciones.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No se encontraron evaluaciones", TipoDeMensaje._Informacion)
            Else
                IB_Buscar.Enabled = False
                IB_AyudaCli.Enabled = False
            End If

            IB_Nuevo.Enabled = True

        Catch ex As Exception

        End Try



    End Sub

    Public Sub Carga_GrillaEvaluacion()
        Try
            CMC.Evaluaciones_Devuelve(1, 2, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_Nro_Eva.Value = btn.ToolTip

        If Not agt.ValidaAccesso(20, 20030304, Usr, "PRESIONO DETALLE DE UNA EVALUACIONES") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If HF_Nro_Eva.Value <> "" Then
            'System.Threading.Thread.Sleep(3000)
            Response.Redirect("EvaCliDeu.aspx?id=" & HF_Nro_Eva.Value & "", False)
        Else
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una Evaluación para ver su detalle", TipoDeMensaje._Exclamacion)
        End If

    End Sub

#End Region

    

End Class


