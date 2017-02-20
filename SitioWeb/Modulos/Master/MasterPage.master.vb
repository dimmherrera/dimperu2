Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos

Partial Class Modulos_Master_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Cada ves que se ejecute un postback de una pagina, validamos que la variable de 
        'sesion este en memoria, de lo contrario la envia a una pagina de "sesion expirada"
        SW = 0
        Ayuda = False

        If Not Me.IsPostBack Then
            Dim rg As New FuncionesGenerales.Conection

            SW = Nothing
            Lbl_Usuario.Text = "Usuario Conectado: " & Usr
            Lbl_Cargo.Text = "Cargo/Perfil: " & Perfil
            'Lbl_Cargo.Text &= rg.Servidor
        End If

        If IsNothing(Session.Item("valida_sesion")) Then
            Response.Redirect("~/FinSesion.aspx")
        End If



    End Sub


End Class

