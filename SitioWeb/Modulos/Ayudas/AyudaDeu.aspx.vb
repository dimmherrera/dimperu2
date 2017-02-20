Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos
Partial Class Modulos_Ayudas_AyudaDeu
    Inherits System.Web.UI.Page

    Dim ClsDeu As New ConsultasGenerales
    Dim Msj As New ClsMensaje

    Private Sub CargaGrilla(ByVal Rut_Desde As Long, ByVal Rut_Hasta As Long)


        Dim Sesion As New ClsSession.ClsSession
        Dim Coll_DEU As New Collection

        Coll_DEU = ClsDeu.DeudorDevuelveAyuda(Rut_Desde, Rut_Hasta, 0, 999999999, 0, 999999999, Txt_Rzo.Text, 1)

        GV_Deudores.DataSource = Coll_DEU
        GV_Deudores.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
            Response.Expires = -1
            NroPaginacionPag = 0
            CargaGrilla(0, 9999999999999)

        End If

    End Sub

    Protected Sub GV_Deudores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Deudores.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)

            If Request.QueryString("cartola") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaDeudorCartola('" & img.ToolTip & "', '" & e.Row.Cells(1).Text & "');")
            End If

            If Request.QueryString("wc") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaDeudorWebControl('" & img.ToolTip & "', '" & e.Row.Cells(1).Text & "');")
            End If

            If Request.QueryString("wc") = "" And Request.QueryString("cartola") = "" Then
                img.Attributes.Add("onClick", "javascript:AceptaDeudor('" & img.ToolTip & "', '" & e.Row.Cells(1).Text & "');")
            End If

            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            '    If SW = 99 Then
            '        e.Row.Attributes.Add("onClick", "funcionDeudores(GV_Deudores, 'clicktable', 'formatable', 'selectable','" & Request.QueryString("valor") & "','" & Request.QueryString("valor2") & "','" & Request.QueryString("valor3") & "','" & Request.QueryString("lb") & "')  ")
            '    Else
            '        e.Row.Attributes.Add("onClick", "celClickSinBtnDeu(GV_Deudores, 'clicktable', 'formatable', 'selectable')")
            '    End If



            'If SW = 99 Or Request.QueryString("SW") = 99 Then
            '    e.Row.Attributes.Add("onClick", "funcionDeudores(GV_Deudores, 'clicktable', 'formatable', 'selectable','" & Request.QueryString("valor") & "','" & Request.QueryString("valor2") & "','" & Request.QueryString("valor3") & "','" & Request.QueryString("lb") & "')  ")
            'Else
            '    If SW = 1 Then 'Web_Control
            '        e.Row.Attributes.Add("onClick", "funcionDeudoresEnWebControl(GV_Deudores, 'clicktable', 'formatable', 'selectable','" & Request.QueryString("valor") & "','" & Request.QueryString("valor2") & "','" & Request.QueryString("valor3") & "','" & Request.QueryString("lb") & "')  ")
            '        Exit Sub
            '    End If

            '    e.Row.Attributes.Add("onClick", "celClickSinBtnDeu(GV_Deudores, 'clicktable', 'formatable', 'selectable')")
            'End If

        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Buscar.Click
        NroPaginacionPag = 0
        If Txt_Rut.Text <> "" Then

            CargaGrilla(Txt_Rut.Text, 9999999999999)
        Else
            CargaGrilla(0, 9999999999999)
        End If
        
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        'If NroPaginacionPag <= 8 Then
        '    NroPaginacionPag += 8
        '    CargaGrilla(Val(Txt_Rut.Text), 9999999999999)
        'End If

        'se mejora forma de recorrer grilla
        If GV_Deudores.Rows.Count < 8 Then
            Msj.Mensaje(Page, "Atencion", "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If GV_Deudores.Rows.Count = 8 Then
            NroPaginacionPag += 8
            CargaGrilla(Val(Txt_Rut.Text), 9999999999999)
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        'Era 4 no 8
        'If NroPaginacionPag >= 8 Then
        '    NroPaginacionPag -= 8
        '    CargaGrilla(Val(Txt_Rut.Text), 9999999999999)
        'End If

        'se mejora forma de recorrer grilla
        If NroPaginacionPag = 0 Then
            Msj.Mensaje(Me, "Atencion", "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If
        If NroPaginacionPag >= 8 Then
            NroPaginacionPag -= 8
            CargaGrilla(Val(Txt_Rut.Text), 9999999999999)
        End If

    End Sub

End Class
