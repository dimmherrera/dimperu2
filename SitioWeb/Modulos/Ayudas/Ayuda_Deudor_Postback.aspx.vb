Imports CapaDatos
Partial Class Modulos_Ayudas_Ayuda_Deudor_Postback
    Inherits System.Web.UI.Page

    Dim ClsDeu As New ConsultasGenerales

    Private Sub CargaGrilla(ByVal Rut_Desde As Long, ByVal Rut_Hasta As Long)


        Dim Sesion As New ClsSession.ClsSession
        Dim Coll_DEU As New Collection

        Coll_DEU = ClsDeu.DeudorDevuelveTodos(Rut_Desde, Rut_Hasta, 0, 999999999, 0, 999999999, Txt_Rzo.Text, 2)

        GV_Deudores.DataSource = Coll_DEU
        GV_Deudores.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1
            CargaGrilla(0, 9999999999999)

        End If

    End Sub

    Protected Sub GV_Deudores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Deudores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            e.Row.Attributes.Add("onClick", "celClickSinBtn(GV_Deudores, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If Txt_Rut.Text <> "" Then
            CargaGrilla(Txt_Rut.Text, 9999999999999)
        Else
            CargaGrilla(0, 9999999999999)
        End If

    End Sub


End Class
