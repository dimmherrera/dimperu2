Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Cls_Negociaciones
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim cmc As New ClaseComercial
    Dim Caption As String = "Negociacón"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Response.Cache.SetNoStore()

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Focus()

            CG.ParametrosDevuelve(TablaParametro.EstadoNegociacion, True, DP_Estados)

            If Not IsNothing(Session("Cliente")) Then

                Dim cli As cli_cls

                cli = Session("Cliente")

                Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                If CargaDatosCliente() Then

                    cmc.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                    If GV_Negociacion.Rows.Count <= 0 Then
                        Msj.Mensaje(Me.Page, Caption, "No posee Negociaciones", TipoDeMensaje._Informacion, Nothing, False)
                    Else
                        HF_Nro_Neg.Value = ""
                        IB_Imprimir.Enabled = True
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

            End If

        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close();")

        'IB_Detalle.Attributes.Add("OnClick", "Javascript:DetalleNegociacion('PopUpNegociacion.aspx', 1280, 1030, 0, 0);")
        'IB_Nuevo.Attributes.Add("OnClick", "Javascript:Negociación('PopUpNegociacion.aspx', 1280, 1030, 0, 0);")

    End Sub

    Protected Sub GV_Negociacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Negociacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_Negociacion, 'selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_Negociacion, 'formatable')")
            e.Row.Attributes.Add("onClick", "ClickNegociacion(GV_Negociacion, 'clicktable', 'formatable', 'selectable')")
        End If

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If CargaDatosCliente() Then
                Txt_Fecha_Dsd.Focus()
            Else
                Txt_Rut_Cli.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Buscar.Click

        Try

            If CargaDatosCliente() Then

                Dim Est_Dsd As Integer
                Dim Est_Hst As Integer

                If DP_Estados.SelectedValue > 0 Then
                    Est_Dsd = DP_Estados.SelectedValue
                    Est_Hst = DP_Estados.SelectedValue
                Else
                    Est_Dsd = 0
                    Est_Hst = 999
                End If

                cmc.NegociacionesAnterioresDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion, Txt_Fecha_Dsd.Text, Txt_Fecha_Hst.Text, Est_Dsd, Est_Hst)

                If GV_Negociacion.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No posee Negociaciones", TipoDeMensaje._Informacion, Nothing, False)
                Else
                    IB_Imprimir.Enabled = True
                    IB_Buscar.Enabled = False
                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        IB_Buscar.Enabled = True
        IB_Imprimir.Enabled = False

        'Limpiamos TextBox
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        
        GV_Negociacion.DataSource = Nothing
        GV_Negociacion.DataBind()

        Txt_Rut_Cli.ReadOnly = False
        Txt_Dig_Cli.ReadOnly = False

        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"

        Txt_Rut_Cli.Focus()
        HF_Nro_Neg.Value = ""

        Session("Cliente") = Nothing

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese NIT", TipoDeMensaje._Exclamacion, Nothing, False)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text <> RG.Vrut(Replace(Txt_Rut_Cli.Text, ".", "")) Then
                Msj.Mensaje(Me.Page, Caption, "Rut Incorrecto del Cliente", TipoDeMensaje._Informacion, Nothing, False)
                Exit Sub
            End If

            If GV_Negociacion.Rows.Count = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe Buscar Negociaciones del Cliente", TipoDeMensaje._Informacion, Nothing, False)
                Exit Sub
            End If
            If HF_Nro_Neg.Value = "" Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Una Operacion Para Mostrar Informe", TipoDeMensaje._Informacion, Nothing, False)
                Exit Sub
            End If

            RW.AbrePopup(Me, 2, "../Carp.%20Comercial/rigthframe_archivos/Informe_Negociacion.aspx?Rut=" & Txt_Rut_Cli.Text & "&IdOpn=" & HF_Nro_Neg.Value _
                     , "Pagos", 1500, 1200, 10, 10)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            If Txt_Dig_Cli.Text.ToUpper <> CLI.cli_dig_ito.ToString.ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "Digito Incorrecto", TipoDeMensaje._Informacion, Nothing, False)
                Txt_Rut_Cli.Focus()
                Exit Function
            End If

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Function
            End If

            Session("Cliente") = CLI

            
            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            HabilitaDesabilitaCliente(False)

            'Deja el Rut y Nombre del cliente en la cabecera 
            'LB_Cliente.text = RG.FormatoMiles(Txt_Rut_Cli.text) & "-" & Txt_Dig_Cli.text & "   " & Me.Txt_Cliente.text

            'Cambia a AcordionPanel de Negociaciones Anteriores
            'Accordion1.SelectedIndex = 1

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
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

#End Region

End Class
