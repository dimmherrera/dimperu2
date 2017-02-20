Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Errores
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class _Deudores_MDeudores
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim CG As New ConsultasGenerales
    Dim Nro As Integer
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim Caption As String = "Pagadores"
    Dim fc As New FuncionesGenerales.FComunes
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1

            Try

                Me.Txt_Rut_Deu.Focus()
                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

                CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, DP_TipoDeudor)
                CG.ParametrosDevuelve(TablaParametro.Segmentos, True, DP_Segmentos)
                'Se agrega funcion LlenaListado(1) para mostrar grilla cuando se carga la pantalla
                LlenaListado(1)

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Se ha producido el siguiente error:")
            End Try

        End If

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Administracion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If

    End Sub


    Protected Sub GrDeudor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrDeudor.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
        End If
    End Sub

    Protected Sub GrDeudor_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrDeudor.PageIndexChanging
        Try
            Me.GrDeudor.PageIndex = e.NewPageIndex
            AvanzarGrilla()


        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        Try

            If Not agt.ValidaAccesso(20, 20030201, Usr, "PRESIONO NUEVO DEUDOR") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Response.Redirect("IngDeudor.aspx", False)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            If Not agt.ValidaAccesso(20, 20010201, Usr, "PRESIONO DETALLE DE UN DEUDOR") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            NroPaginacion = 0
            TxtNro.Value = ""
            If Me.Txt_Rut_Deu.Text <> "" Then
                LlenaListado(4)
            Else
                LlenaListado(1)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)

        End Try

    End Sub


    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GrDeudor.Rows.Count < 8 Then
            Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If GrDeudor.Rows.Count = 8 Then
            NroPaginacion += 8
            LlenaListado(1)
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 8 Then
            NroPaginacion -= 8
            LlenaListado(1)
        End If

    End Sub


    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Txt_Rso_Deu.Text = ""
        Txt_Rut_Deu.Text = ""
        TxtNro.Value = ""
        NroPaginacion = 0
        DP_Segmentos.ClearSelection()
        DP_TipoDeudor.ClearSelection()
        GrDeudor.DataSource = New Collection
        GrDeudor.DataBind()
        'LlenaListado(1)
    End Sub
#End Region


#Region "Sub"

    Public Sub LlenaListado(ByVal Orden As Int16)

        Try

            Dim Rut_Dsd As Long
            Dim Rut_Hst As Long

            Dim Tip_Dsd As Integer
            Dim Tip_Hst As Integer

            Dim Seg_Dsd As Integer
            Dim Seg_Hst As Integer


            If Txt_Rut_Deu.Text <> "" Then
                Rut_Dsd = CLng(Txt_Rut_Deu.Text)
                Rut_Hst = CLng(Txt_Rut_Deu.Text)
            Else
                Rut_Dsd = 0
                Rut_Hst = 999999999999
            End If


            If DP_TipoDeudor.SelectedIndex <> 0 Then
                Tip_Dsd = DP_TipoDeudor.SelectedValue
                Tip_Hst = DP_TipoDeudor.SelectedValue
            Else
                Tip_Dsd = 0
                Tip_Hst = 99999999
            End If

            If DP_Segmentos.SelectedIndex <> 0 Then
                Seg_Dsd = DP_Segmentos.SelectedValue
                Seg_Hst = DP_Segmentos.SelectedValue
            Else
                Seg_Dsd = 0
                Seg_Hst = 99999999
            End If


            GrDeudor.DataSource = CG.DeudorDevuelveTodos(Rut_Dsd, Rut_Hst, Tip_Dsd, Tip_Hst, Seg_Dsd, Seg_Hst, Txt_Rso_Deu.Text.Trim, Orden)
            GrDeudor.DataBind()

            If GrDeudor.Rows.Count = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "No se han encontrado datos segun criterio de busqueda", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Public Sub AvanzarGrilla()
        Try
            'Me.GrDeudor.DataSource = Coll_DEU
            Me.GrDeudor.DataBind()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

#End Region

#Region "Link para ordenar grilla"

    'Protected Sub LB_Orden_Rut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Orden_Rut.Click
    '    LlenaListado(1)
    'End Sub

    'Protected Sub LB_Orden_Nombre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Orden_Nombre.Click
    '    LlenaListado(2)
    'End Sub

    'Protected Sub LB_Orden_Tipo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Orden_Tipo.Click
    '    LlenaListado(3)
    'End Sub




#End Region


    'Protected Sub GV_Deudores_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GV_Deudores.Sorting
    '    e.SortExpression = ""
    'End Sub


    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Valida acceso a permisos
        If Not agt.ValidaAccesso(20, 20020201, Usr, "PRESIONO DETALLE DEL DEUDOR") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim rg As New FuncionesGenerales.FComunes


        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        Response.Redirect("IngDeudor.aspx?Nro=" & rg.LimpiaRut(boton_ver.ToolTip), False)


    End Sub

End Class
