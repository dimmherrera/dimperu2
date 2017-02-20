Imports System.Configuration


Public Class Cls_Principal

#Region "Propiedades"

    Private CN As New Cls_ClasesNeg
    
    Private CodigoError As String
    Public Function getcodMsg() As String
        Return CodigoError
    End Function


    Private errorMsg As String
    Public Function getErrMsg() As String
        Return errorMsg
    End Function

#End Region

#Region "Metodos Publicos"

    'Devuelve objeto USER con login de usuario y perfil
    Public Function BuscaUsuario(ByVal pUser As String) As Usuario

        Try

            Dim usr As New Usuario

            usr = CN.findUsuario(Trim(pUser))

            If IsNothing(usr) Then
                errorMsg = "Usuario no encontrado"
                Return Nothing
            Else
                Return usr
            End If

        Catch ex As Exception
            CodigoError = 99
            errorMsg &= "---BuscaUsuario: " & ex.Message & CN.getErrMsg
            Return Nothing
        End Try

    End Function


    'Devuelve codigo de perfil y descripcion de un usuario por su login
    Public Function BuscaPerfil(ByVal idPerfil As String) As Perfil

        Try

            Dim pfl As New Perfil

            pfl = CN.findPerfil(idPerfil)

            If IsNothing(pfl) Then
                Me.errorMsg = "Perfil no encontrado"
                Return Nothing
            Else
                errorMsg = ""
                Return pfl
            End If

        Catch ex As Exception
            CodigoError = 99
            errorMsg &= "---BuscaPerfil: " & ex.Message & CN.getErrMsg
            Return Nothing
        End Try

    End Function


    'Valida si tiene acceso a una puerta de un sistema
    Public Function ValidaAccesso(ByVal id_sistema As Integer, ByVal id_puerta As Integer, _
                                  ByVal pUser As String, ByVal Obs As String) As Boolean
        
        
        Try

            If id_sistema <> 1 Then
                If SistemaBloqueado(id_sistema) = True Then
                    Me.errorMsg = "Sistema Bloqueado"
                    Return False
                End If
            End If

            If BloqueadoGeneral() Then 'Mensaje es el ingresado en mantencion
                Return False
            End If

            Dim usr As Usuario
            Dim pfl As Perfil

            usr = CN.findUsuario(Trim(pUser))

            If IsNothing(usr) Then
                Me.errorMsg = "Usuario no encontrado " & CN.getErrMsg()
                Return False
            End If

            pfl = CN.findPerfil(usr.idPerfil)

            If IsNothing(pfl) Then
                Me.errorMsg = "Perfil no encontrado"
                Return False
            End If

            If CN.AccessWeb(id_sistema, id_puerta, Obs, pUser) Then
                CodigoError = 0
                Return True
            Else
                CodigoError = 1
                errorMsg = "Acceso Denegado"
                Return False
            End If

        Catch ex As Exception
            CodigoError = 99
            errorMsg &= "---ValidaAcceso: " & ex.Message & CN.getErrMsg
            Return False
        End Try

    End Function


    'Devuelve un arreglo con los codigos de las puertas habilitadas segun codigo superior (hijos).
    Public Function ObjetoRetornaPuertasHabitadas(ByVal id_sistema As String, _
                                                  ByVal id_puerta_padre As String, _
                                                  ByVal pUser As String) As ArrayList

        
        Try

            Return CN.ObjetoRetornaPueHabiWeb(id_sistema, id_puerta_padre, pUser)


        Catch ex As Exception
            errorMsg &= "---ObjetoRetornaPuertasHabitadas: " & ex.Message & CN.getErrMsg
            Return Nothing
        End Try


    End Function


    'Devuelve un booleano, para indicar si esta o habilitado (true) o deshabilitado (false).
    Public Function SistemaBloqueado(ByVal id_sistema As String) As Boolean

        Try
            If id_sistema <> 1 Then
                Return CN.SisBloqueado(id_sistema)
            End If
        Catch ex As Exception
            CodigoError = 99
            errorMsg &= "---SistemaBloqueado: " & ex.Message & CN.getErrMsg
            Return False
        End Try
    End Function


    'Devuelve un booleano, para indicar si todos los sistemas se encuentran bloqueados.
    Public Function BloqueadoGeneral() As Boolean
        Try

            Return CN.Bloqueo(2)

        Catch ex As Exception
            CodigoError = 99
            errorMsg &= "---BloqueadoGeneral: " & ex.Message & CN.getErrMsg
            Return False
        Finally
            errorMsg = CN.getErrMsg
        End Try

    End Function


#End Region

End Class
