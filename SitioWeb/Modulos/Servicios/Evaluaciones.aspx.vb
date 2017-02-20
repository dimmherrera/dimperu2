Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Cls_Evaluaciones
    Inherits System.Web.UI.Page


#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Evaluación"
    Dim CG As New ConsultasGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim cmc As New ClaseComercial
#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

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

                cmc.EvaluacionesServicio_Devuelve(Est_Dsd, Est_Hst, Replace(Txt_Rut_Cli.Text, ".", ""), GV_Evaluaciones, Txt_Fecha_Dsd.Text, Txt_Fecha_Hst.Text)

                If GV_Evaluaciones.Rows.Count <= 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No se encontraron evaluaciones", TipoDeMensaje._Informacion, Nothing, False)
                Else
                    IB_Informe.Enabled = True
                    IB_Buscar.Enabled = False
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try

            'Limpiamos TextBox
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Fecha_Dsd.Text = ""

            GV_Evaluaciones.DataSource = Nothing
            GV_Evaluaciones.DataBind()

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Session("Cliente") = Nothing

            Txt_Rut_Cli.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click
        RutCli = Txt_Rut_Cli.Text
        RW.AbrePopup(Me, 2, "../Carp.%20Comercial/rigthframe_archivos/Reporte_EvaluacionCliDeu.aspx?id=" & HF_Nro_Eva.Value, "RepEvaCliDeu", 1280, 1024, 0, 0)

    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Cliente a buscar", TipoDeMensaje._Informacion, Nothing, False)
                Exit Function
            End If

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Function
            End If


            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            HabilitaDesabilitaCliente(False)

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Response.Cache.SetNoStore()

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Focus()

            CG.ParametrosDevuelve(TablaParametro.EstadoEvaluacion, True, DP_Estados)

        Else

            If Txt_Rut_Cli.Text = "" Then

                If Not IsNothing(Session("Cliente")) Then

                    Dim cli As cli_cls

                    cli = Session("Cliente")

                    Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                    Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                    If CargaDatosCliente() Then
                        IB_Informe.Enabled = True
                        IB_Buscar.Enabled = False
                    End If

                End If

            End If

        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close();")

        'IB_Nuevo.Attributes.Add("onClick", "Negociación('PopUpNegociacion.aspx', 1280, 1024, 0, 0);")
        'IB_Detalle.Attributes.Add("onClick", "DetalleNegociacion('PopUpNegociacion.aspx', 1280, 1024, 0, 0);")

    End Sub

    Protected Sub GV_Evaluaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Evaluaciones.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_Evaluaciones, 'selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_Evaluaciones, 'formatable')")
            e.Row.Attributes.Add("onClick", "ClickEvaluacion2(GV_Evaluaciones, 'clicktable', 'formatable', 'selectable')")
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

End Class
