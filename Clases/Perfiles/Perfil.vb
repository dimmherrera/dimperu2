Imports System.Web

Public Class Perfil
    Public Sub New()
        Me.codigo = 0
        Me.descripcion = ""
    End Sub

    Private codigo As Integer
    Public Property idPfl() As Integer
        Get
            Return codigo
        End Get
        Set(ByVal Value As Integer)
            codigo = Value
        End Set
    End Property

    Private descripcion As String
    Public Property descPfl() As String
        Get
            Return descripcion
        End Get
        Set(ByVal Value As String)
            descripcion = Value
        End Set
    End Property

    Public Sub New(ByVal pcodigo As Integer, ByVal pdescripcion As String)
        With Me
            .codigo = pcodigo
            .descripcion = Trim(pdescripcion)
        End With
    End Sub
End Class
