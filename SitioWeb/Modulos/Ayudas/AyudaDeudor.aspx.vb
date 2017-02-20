Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Ayudas_AyudaDeudor
    Inherits System.Web.UI.Page


    Dim ClsDeu As New ConsultasGenerales

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
            NroPaginacionPag = 0
            Response.Expires = -1
            CargaGrilla(0, 9999999999999)

        End If

    End Sub

    Protected Sub GV_Deudores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Deudores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)

            If Request.QueryString("pago") = "" Then
                e.Row.Attributes.Add("onClick", "AceptaDeudorNormal('" & img.ToolTip & "', '" & e.Row.Cells(2).Text.Trim() & "')")
            Else
                e.Row.Attributes.Add("onClick", "AceptaDeudorPago('" & img.ToolTip & "', '" & e.Row.Cells(2).Text.Trim() & "')")
            End If

        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        NroPaginacionPag = 0
        If Txt_Rut.Text <> "" Then

            CargaGrilla(Txt_Rut.Text, 9999999999999)
        Else
            CargaGrilla(0, 9999999999999)
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        NroPaginacionPag += 8
        CargaGrilla(Val(Txt_Rut.Text), 9999999999999)

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacionPag >= 8 Then
            NroPaginacionPag -= 8
            CargaGrilla(Val(Txt_Rut.Text), 9999999999999)
        End If

    End Sub

End Class
