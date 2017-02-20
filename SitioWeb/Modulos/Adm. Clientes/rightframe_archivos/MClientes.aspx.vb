Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class MClientes
    Inherits System.Web.UI.Page

    Private Sesion As New ClsSession.ClsSession
    Private ClientesTodos As Object
    Private Caption As String = "Clientes"
    Private FC As New FuncionesGenerales.FComunes
    Private Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

                Dim Sesion As New ClsSession.ClsSession
                Dim CG As New ConsultasGenerales

                Response.Expires = -1

                Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
                CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, DP_TipoCli)
                CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 15)

                NroPaginacion = 0
                NroPaginacionCli = 0
                Txt_Orden.Value = "1"

                If DP_Ejecutivo.Items.Count > 0 Then
                    CargaGrilla()
                Else
                    Msj.Mensaje(Me.Page, Caption, "No existen Ejecutivos de Cuentas para sucursal del Usuario conectado", TipoDeMensaje._Exclamacion)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub PRUEBA(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
        End If
    End Sub

    
    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound


    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20030101, Usr, "PRESIONO NUEVO CLIENTE") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Response.Redirect("IngClientes.aspx", True)

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then

            Modulo = "Administracion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If

    End Sub

    Protected Sub GV_Clientes_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GV_Clientes.Sorting
        e.SortExpression = ""
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If Not agt.ValidaAccesso(20, 20010101, Usr, "PRESIONO BUSCAR CLIENTE") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        NroPaginacion = 0
        NroPaginacionCli = 0
        Txt_Orden.Value = "1"

        CargaGrilla()

    End Sub

    Private Sub CargaGrilla()

        Try

            Dim Rut_Dsd As String
            Dim Rut_Hst As String

            Dim Tip_Dsd As Integer
            Dim Tip_Hst As Integer

            Dim Eje_Dsd As Integer
            Dim Eje_Hst As Integer

            If Txt_Rut.Text <> "" Then
                Rut_Dsd = CLng(Txt_Rut.Text)
                Rut_Hst = CLng(Txt_Rut.Text)
            Else
                Rut_Dsd = "0"
                Rut_Hst = "9999999999999"
            End If

            If DP_TipoCli.SelectedIndex <> 0 Then
                Tip_Dsd = DP_TipoCli.SelectedValue
                Tip_Hst = DP_TipoCli.SelectedValue
            Else
                Tip_Dsd = 0
                Tip_Hst = 999999999
            End If

            If DP_Ejecutivo.SelectedIndex > 0 Then
                Eje_Dsd = DP_Ejecutivo.SelectedValue
                Eje_Hst = DP_Ejecutivo.SelectedValue
            Else
                'Eje_Dsd = CodEje
                'Eje_Hst = CodEje
                If DP_Ejecutivo.SelectedIndex < 0 Then
                    Msj.Mensaje(Me.Page, Caption, "No existen Ejecutivos de Cuentas para sucursal del Usuario conectado", TipoDeMensaje._Exclamacion)
                    Return
                Else
                    'Para que traiga todos los clientes
                    Eje_Dsd = 0
                    Eje_Hst = 999
                End If

            End If

            GV_Clientes.DataSource = Nothing
            GV_Clientes.DataBind()

            Dim CC As New ClaseClientes

            If Txt_Nom.Text <> "" Or Txt_Rut.Text <> "" Then

                Dim Coll_Clientes As New Collection
                Dim Sesion As New ClsSession.ClsSession

                If Me.DP_Ejecutivo.SelectedValue <> 0 Then

                    CC.ClientesActivosLikeDevuelveTodos(GV_Clientes, Rut_Dsd, Txt_Nom.Text, Me.DP_Ejecutivo.SelectedValue, Tip_Dsd, Tip_Hst)
                Else

                    CC.ClientesActivosLikeDevuelveTodos(GV_Clientes, Rut_Dsd, Txt_Nom.Text, Sesion.CodEje, Tip_Dsd, Tip_Hst)
                End If

            Else

                'CC.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, Eje_Dsd, Eje_Hst, Tip_Dsd, Tip_Hst, Txt_Nom.Text.Trim, Txt_Orden.Value.Trim)

                ' grilla de mantenedor clientes.
                CC.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, Eje_Dsd, Eje_Hst, Tip_Dsd, Tip_Hst, Txt_Nom.Text.Trim, Txt_Orden.Value.Trim, True)

            End If

            If GV_Clientes.Rows.Count > 0 Then
                'IB_Detalle.Enabled = True
            Else
                Msj.Mensaje(Me.Page, Caption, "No se encontraron clientes para este criterio de búsqueda", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub BuscaGrilla(ByVal Orden As String)

        Try

            'Response.Write()
            Dim Coll_Clientes As New Collection
            Dim Sesion As New ClsSession.ClsSession

            NroPaginacion = 0
            'ClsCLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, "", Txt_Nom.Text, Sesion.CodEje)


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Clientes.Rows.Count < 8 Then
            Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If GV_Clientes.Rows.Count = 8 Then
            NroPaginacionCli += 8
            CargaGrilla()
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacionCli = 0 Then
            Msj.Mensaje(Me, Caption, "Ya a llegado al comienzo de la lista", 2)
            Exit Sub

        End If

        If NroPaginacionCli >= 8 Then
            NroPaginacionCli -= 8
            CargaGrilla()
        End If
    End Sub



    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        NroPaginacionCli = 0
        Txt_Nom.Text = ""
        Txt_Orden.Value = "1"
        Txt_Rut.Text = ""
        DP_TipoCli.ClearSelection()
        DP_Ejecutivo.ClearSelection()

        GV_Clientes.DataSource = New Collection
        GV_Clientes.DataBind()

        'CargaGrilla()
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Valida acceso a permisos
        If Not agt.ValidaAccesso(20, 20020101, Usr, "PRESIONO DETALLE DEL CLIENTE") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim rg As New FuncionesGenerales.FComunes


        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        Response.Redirect("IngClientes.aspx?id=" & rg.LimpiaRut(boton_ver.ToolTip), False)


    End Sub

End Class
