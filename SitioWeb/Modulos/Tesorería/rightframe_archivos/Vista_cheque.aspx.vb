
Partial Class Modulos_Tesorería_rightframe_archivos_Vista_cheque
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ImageButton1.Attributes.Add("onClick", "javascript:window.close();")
    End Sub
End Class
