Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class Modulos_Recaudacion_rigthframe_archivos_pop_up_plaza
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim Msj As New ClsMensaje
    Dim Caption As String = "Plaza"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            NroPaginacion = 0

            gr_fact.DataSource = cg.plaza_retorna(12)
            gr_fact.DataBind()
        End If

    End Sub

    Protected Sub gr_fact_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_fact.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btn As ImageButton = CType(e.Row.FindControl("Btn_ver"), ImageButton)
            e.Row.Attributes.Add("onClick", "Plaza('" & btn.ToolTip & "', '" & e.Row.Cells(2).Text & " ');")

        End If

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
        '    e.Row.Attributes.Add("onClick", "celClickSinBtnPza(gr_fact, 'clicktable', 'formatable', 'selectable')")
        'End If
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion > 12 Then
                NroPaginacion -= 12
                gr_fact.DataSource = cg.plaza_retorna(12)
                gr_fact.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If gr_fact.Rows.Count < 12 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If gr_fact.Rows.Count = 12 Then
                NroPaginacion += 12
                gr_fact.DataSource = cg.plaza_retorna(12)
                gr_fact.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim btn As ImageButton = CType(sender, ImageButton)

    '    For i = 0 To gr_fact.Rows.Count - 1
    '        If (btn.ToolTip = gr_fact.Rows(i).Cells(1).Text) Then

    '        End If
    '    Next

    'End Sub
End Class
