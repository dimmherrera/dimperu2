Imports System.Data
Imports System.Text
Imports System.Xml
Imports System.IO



Public Class Conection

    Public Function Conector() As String

        Try

            Return System.Web.Configuration.WebConfigurationManager.ConnectionStrings("FACTORConnectionString").ConnectionString


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Servidor() As String

        Try

            Return System.Web.Configuration.WebConfigurationManager.ConnectionStrings("FACTORConnectionString").ConnectionString(0).ToString()


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class


