Imports System.Data.SqlClient
Imports FuncionesGenerales.RutinasWeb
Imports System.Web.UI.WebControls
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.FComunes

Public Class Class_LlenaCombo

    Private _codigo As String
    Public ReadOnly Property codigo() As String
        Get
            Return _codigo
        End Get
    End Property

    Private _descripcion As String
    Public ReadOnly Property descripcion() As String
        Get
            Return _descripcion
        End Get
    End Property

    Public Sub New(ByVal pCod As String, ByVal pDes As String)
        With Me
            ._codigo = pCod
            ._descripcion = pDes
        End With
    End Sub

End Class

