Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class Modulos_Operaciones_rightframe_archivos_AlertasCondiciones
    Inherits System.Web.UI.Page

    Private CD As New ClaseControlDual
    Private CG As New ConsultasGenerales
    Private Msj As New ClsMensaje
    Private RW As New FuncionesGenerales.RutinasWeb

    Dim Caption As String = "Alerta Condiciones"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        CD.CondicionesDevuelve((Txt_Rut_Cli.Text), DP_Ejecutivos.SelectedValue, DP_EstadoCondicion.SelectedValue, True, GV_Condiciones)

        If GV_Condiciones.Rows.Count = 0 Then
            Msj.Mensaje(Page, "Alertas Condiciones", "No se encontraron condiciones", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    'Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

    'End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        Me.IB_AyudaCli.Enabled = False
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        IB_AyudaCli.Enabled = False
        CB_Cliente.Checked = False
        IB_Buscar.Enabled = True

        DP_Ejecutivos.ClearSelection()
        DP_EstadoCondicion.ClearSelection()
        GV_Condiciones.DataSource = Nothing
        GV_Condiciones.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            Txt_Rut_Cli.Attributes.Add("Style", "Text-Align: right")

            'Estado de Condicion
            CG.ParametrosDevuelve(TablaParametro.EstadoCondicion, True, DP_EstadoCondicion)
            CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)


        End If

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        Try
            If CB_Cliente.Checked Then

                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Me.IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False

                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"

                Txt_Rut_Cli.Focus()
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True
            Else
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Me.IB_AyudaCli.Enabled = False
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Condiciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Condiciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim img As ImageButton = e.Row.FindControl("Img_Ver")
            img.Attributes.Add("onClick", "WinOpen(2, 'PopUpCondicion.aspx?id=" & img.ToolTip & "', 'PopUpCondicion', 500, 300, 150, 150);")

        End If
    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_buscar.Click

        GV_Condiciones.DataSource = Nothing
        GV_Condiciones.DataBind()

        CD.CondicionesDevuelve((Txt_Rut_Cli.Text), _
                               DP_Ejecutivos.SelectedValue, _
                               DP_EstadoCondicion.SelectedValue, _
                               True, _
                               GV_Condiciones)

        If GV_Condiciones.Rows.Count = 0 Then
            Msj.Mensaje(Page, "Alertas Condiciones", "No se encontraron condiciones", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim ClsCli As New ClaseClientes
        Dim RG As New FuncionesGenerales.FComunes
        Dim Sesion As New ClsSession.ClsSession
        Dim Cli As cli_cls



        Try


            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)


            If valida_cliente <> "" Then

                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                Exit Function
            Else
                If IsNothing(Cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Function
                End If

                Me.IB_AyudaCli.Enabled = False
                If Cli.id_P_008 <> 3 Then
                    Me.Txt_Raz_Soc.Text = Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn
                Else
                    Msj.Mensaje(Me.Page, Caption, "Cliente se encuentra deshabilitado", TipoDeMensaje._Exclamacion, "", True)
                    Exit Function
                End If

                'Sesion.Sucursal = Cli.id_suc
                Sesion.RutCli = Replace(Txt_Rut_Cli.Text, ".", "")

                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True

                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                Return True
            End If
        Catch ex As Exception

            Return False

        End Try


    End Function

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        If Me.CargaDatosCliente() Then
            Lb_buscar_Click(Me, e)
            IB_Buscar.Enabled = False
        End If
    End Sub
End Class
