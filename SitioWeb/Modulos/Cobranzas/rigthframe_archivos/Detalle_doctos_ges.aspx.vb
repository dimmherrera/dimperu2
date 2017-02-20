Imports CapaDatos
Partial Class Modulos_Cobranzas_rigthframe_archivos_Call_Telefonicas_Detalle_doctos_ges
    Inherits System.Web.UI.Page
    Dim CBZ As New ClaseCobranza
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Response.Expires = -1
            Dim str As String

            str = CBZ.Retorna_url_img(Request.QueryString("ID")).dsi_img_url
            Imagen_docto.ImageUrl = Trim(str)
            Imagen_docto.DataBind()
        Else
            Dim str As String

            str = Request.QueryString("ID")
            Imagen_docto.ImageUrl = Trim(str)
            Imagen_docto.DataBind()
        End If
    End Sub
End Class
