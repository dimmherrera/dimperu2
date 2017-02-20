Imports System.Data.SqlClient
Imports FuncionesGenerales.FComunes

Public Class ClsServicios

    Public Function RetornaDatosDiarios() As Collection

        Dim SQL As String
        Dim Lee As SqlDataReader
        Dim Query As New DbQuery
        Dim Coll As New Collection

        Try

            Dim Fecha As String
            If Date.Now.Day < 10 Then

                Fecha = 0 & Date.Now.Day & "/"
                If Date.Now.Month < 10 Then
                    Fecha = Fecha & 0 & Date.Now.Month & "/" & Date.Now.Year
                Else
                    Fecha = Fecha & Date.Now.Month & "/" & Date.Now.Year
                End If
            Else
                Fecha = Date.Now.Day & "/"
                If Date.Now.Month < 10 Then
                    Fecha = Fecha & 0 & Date.Now.Month & "/" & Date.Now.Year
                Else
                    Fecha = Fecha & Date.Now.Month & "/" & Date.Now.Year
                End If
            End If

            'SQL = "Exec sp_op_retorna_datos_diarios '" & Date.Now.Year & Date.Now.Month & Date.Now.Day & "'"

            SQL = "Exec sp_op_retorna_datos_diarios '" & FUNFechaJul(Fecha) & "'"

            Lee = Query.SelecQuery(SQL)

            If Lee.Read Then
                Coll.Add(Lee!UF)
                Coll.Add(Lee!DOLLAR)
                Coll.Add(Lee!TMC)
                Lee.Close()
            End If

            Return Coll


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class
