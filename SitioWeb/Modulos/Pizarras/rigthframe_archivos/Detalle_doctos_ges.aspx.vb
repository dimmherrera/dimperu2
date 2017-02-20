Imports CapaDatos
Partial Class Modulos_Cobranzas_rigthframe_archivos_Call_Telefonicas_Detalle_doctos_ges
    Inherits System.Web.UI.Page
    Dim CBZ As New ClaseCobranza

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1
            Dim str As String
            Dim Msj As New ClsMensaje

            Try

                str = CBZ.Retorna_url_img_ope(Request.QueryString("ID")).dsi_img_url
            Catch ex As Exception
                str = ""
                Msj.Mensaje(Page, "Aprobaciones", "No existe la imagen del documento", 5)
            End Try

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
