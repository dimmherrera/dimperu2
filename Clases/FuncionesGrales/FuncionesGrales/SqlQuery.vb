Imports System.Data.SqlClient
Imports System.Data
Imports FuncionesGenerales

Public Class SqlQuery
    Private msg As String

    Public Property mensaje() As String
        Get
            Return msg
        End Get
        Set(ByVal value As String)
            msg = value
        End Set
    End Property


    Public Function ValidaConexion() As Boolean
        Dim cn As New Conection
        Dim myConnection As New SqlConnection(cn.Conector)


        Try

            myConnection.Open()
            SqlConnection.ClearAllPools()
            myConnection.Close()


            Return True

        Catch ex As Exception
            msg = ex.Message
            Return False
        End Try

    End Function

    Public Function ExecuteNonQuery(ByVal SQL As String) As Integer
        Dim cn As New Conection
        Dim myConnection As New SqlConnection(cn.Conector)

        Try

            Dim myCommand As New SqlCommand(SQL, myConnection)

            If myConnection.State <> ConnectionState.Open Then
                myConnection.Open()
            End If

            myCommand.CommandTimeout = 0

            Return myCommand.ExecuteNonQuery

        Catch ex As Exception
            msg = ex.Message
        Finally
            myConnection.Close()
            myConnection.Dispose()
        End Try

    End Function

    Public Function ExecuteNonQuery(ByVal SQL As String, ByVal rawData As Byte()) As Integer

        Dim cn As New Conection
        Dim myConnection As New SqlConnection(cn.Conector)


        Try

            Dim myCommand As New SqlCommand(SQL, myConnection)

            myCommand.Parameters.Add("@@imagen", rawData)

            If myConnection.State <> ConnectionState.Open Then

                myConnection.Open()

            End If

            Return myCommand.ExecuteNonQuery


        Catch ex As Exception
            msg = ex.Message
        Finally
            myConnection.Close()
            myConnection.Dispose()
        End Try

    End Function


    Public Function ExecuteDataSet(ByVal SQL As String) As DataSet
        Dim cn As New Conection
        Dim myConnection As New SqlConnection(cn.Conector)
        Dim dt As New SqlDataAdapter
        Dim d As New DataSet


        Try

            Dim Adapter As New SqlDataAdapter(SQL, myConnection)

            If myConnection.State <> ConnectionState.Open Then
                'myConnection.ConnectionTimeout = 10
                myConnection.Open()
            End If

            Adapter.SelectCommand.CommandTimeout = 0
            ExecuteDataSet = New DataSet
            Adapter.Fill(ExecuteDataSet)

        Catch ex As Exception
            msg = ex.Message
            Return Nothing
        Finally
            myConnection.Close()
            myConnection.Dispose()
        End Try

    End Function

End Class
