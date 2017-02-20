Imports ClsSession.ClsSession

Partial Class Modulos_Web_Controles_Mensaje_Deudores
    Inherits System.Web.UI.UserControl

    Protected Sub Okbutton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Okbutton.Click
        Me.Okbutton.Enabled = False
        'Modal_Mensaje.Hide()
    End Sub

    Protected Sub canc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles canc.Click

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Txt_Mensaje.Attributes.Add("Style", "Text-Align:left")
    End Sub

End Class
