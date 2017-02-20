Imports System.Data.SqlClient
Imports System.Configuration

Public Class Cls_ClasesNeg

    Private errorMsg As String
    Public Function getErrMsg() As String
        Return errorMsg
    End Function


    '//*1*********************************************************************//
    Public Function findUsuario(ByVal usrLogin As String) As Usuario

        Dim RG As New Cls_ClasesNeg
        Dim dr As DataSet
        Dim usr As New Usuario
        Dim Str As String

        Try
            Str = ""
            Str = " SELECT * FROM USR where USR_LOG_USR = '" & UCase(Trim(usrLogin)) & "' "

            dr = RG.ExecuteDataSet(Str)

            If dr.Tables(0).Rows.Count > 0 Then
                '/*  Login  */
                If dr.Tables(0).Rows(0).Item("USR_LOG_USR").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_LOG_USR")) Then
                    usr.usrLogin = ""
                Else
                    usr.usrLogin = dr.Tables(0).Rows(0).Item("USR_LOG_USR").Trim
                End If

                '/*  PassWord  */
                If dr.Tables(0).Rows(0).Item("USR_PAS_USR").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_PAS_USR")) Then
                    usr.usrPWD = ""
                Else
                    usr.usrPWD = dr.Tables(0).Rows(0).Item("USR_PAS_USR").Trim
                End If

                '/*  Nombres  */
                If dr.Tables(0).Rows(0).Item("USR_NOM_USR").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_NOM_USR")) Then
                    usr.usrNAME = ""
                Else
                    usr.usrNAME = dr.Tables(0).Rows(0).Item("USR_NOM_USR").Trim
                End If

                '/*  Apellidos  */
                If dr.Tables(0).Rows(0).Item("USR_APE_USR").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_APE_USR")) Then
                    usr.usrAPE = ""
                Else
                    usr.usrAPE = dr.Tables(0).Rows(0).Item("USR_APE_USR").Trim
                End If

                '/*  Cargo Perfil */
                If dr.Tables(0).Rows(0).Item("USR_CAR_PFL") = 0 Or IsNothing(dr.Tables(0).Rows(0).Item("USR_CAR_PFL")) Then
                    usr.idPerfil = 0
                Else
                    usr.idPerfil = dr.Tables(0).Rows(0).Item("USR_CAR_PFL")
                End If

                '/* Creacion PassWord*/
                If dr.Tables(0).Rows(0).Item("USR_FEC_CRE") = CDate("01/01/1900") Or IsNothing(dr.Tables(0).Rows(0).Item("USR_FEC_CRE")) Then
                    usr.FecCreacion = "01/01/1900"
                Else
                    usr.FecCreacion = Format(CDate(dr.Tables(0).Rows(0).Item("USR_FEC_CRE").ToString), "dd/MM/yyyy")
                End If

                '/* Vigencia PassWord*/
                If dr.Tables(0).Rows(0).Item("USR_FEC_VIG") = CDate("01/01/1900") Or IsNothing(dr.Tables(0).Rows(0).Item("USR_FEC_VIG")) Then
                    usr.vigPass = "01/01/1900"
                Else
                    usr.vigPass = Format(CDate(dr.Tables(0).Rows(0).Item("USR_FEC_VIG").ToString), "dd/MM/yyyy")
                End If

                '/* Opera Internet*/
                Try
                    If dr.Tables(0).Rows(0).Item("USR_INT_NET") = 0 Or IsNothing(dr.Tables(0).Rows(0).Item("USR_INT_NET")) Then
                        usr.internetUsr = 0
                    Else
                        usr.internetUsr = dr.Tables(0).Rows(0).Item("USR_INT_NET")
                    End If
                Catch ex As Exception
                    usr.internetUsr = 0
                End Try

                '/*  Email */
                Try
                    If dr.Tables(0).Rows(0).Item("USR_COR_REO").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_COR_REO")) Then
                        usr.usrMail = "0"
                    Else
                        usr.usrMail = dr.Tables(0).Rows(0).Item("USR_COR_REO").Trim
                    End If
                Catch ex As Exception

                End Try
                '/*  Estado Usuario  */
                If dr.Tables(0).Rows(0).Item("USR_EST_ADO") = 0 Or IsNothing(dr.Tables(0).Rows(0).Item("USR_EST_ADO")) Then
                    usr.estadoUsr = 0
                Else
                    usr.estadoUsr = dr.Tables(0).Rows(0).Item("USR_EST_ADO")
                End If

                '/*  Ingreso Primera Vez*/
                If dr.Tables(0).Rows(0).Item("USR_PRI_VEZ").Trim = "" Or IsNothing(dr.Tables(0).Rows(0).Item("USR_PRI_VEZ")) Then
                    usr.usrPrivez = CChar("")
                Else
                    usr.usrPrivez = dr.Tables(0).Rows(0).Item("USR_PRI_VEZ")
                End If

                If dr.Tables(0).Rows(0).Item("USR_IDE_EJE") = 0 Or IsNothing(dr.Tables(0).Rows(0).Item("USR_IDE_EJE")) Then
                    usr.usrEjecutivo = ""
                Else
                    usr.usrEjecutivo = dr.Tables(0).Rows(0).Item("USR_IDE_EJE")
                End If

                If dr.Tables(0).Rows(0).Item("USR_SUC_OPE") = 0 Or IsNothing(dr.Tables(0).Rows(0).Item("USR_SUC_OPE")) Then
                    usr.sucursalUsr = 0
                Else
                    usr.sucursalUsr = dr.Tables(0).Rows(0).Item("USR_SUC_OPE")
                End If

                Return usr

            Else
                Return Nothing
            End If
        Catch ex As Exception
            errorMsg &= "---findUsuario:" & ex.Message
            Return Nothing
        End Try

    End Function

    Public Function getLoginUsr(ByVal pUser As String) As String

        Dim Str As String
        Dim Val As String
        Dim Lee As DataSet

        Try

            Str = ""
            Str = "Select ACC_LOG_USR "
            Str &= " From ACC "
            Str &= " Where ACC_LOG_USR = '" & Trim(pUser) & "' "

            Lee = ExecuteDataSet(Str)

            If Lee.Tables(0).Rows.Count > 0 Then
                Val = Lee.Tables(0).Rows(0).Item(0)
                Return Val
            Else
                errorMsg &= "--SQL: " & Str
                Return ""
            End If

        Catch ex As Exception
            errorMsg &= "---getLoginUSR:" & ex.Message
            Return ""
        End Try

    End Function

    '//*2*********************************************************************//
    Public Function findPerfil(ByVal cod As String) As Perfil

        Dim dr As DataSet
        Dim Str As String, msg As String
        Dim pfl As New Perfil

        Try

            Str = ""
            Str &= "Select id_P_0045, pnu_des from P_0045 "
            Str &= "where id_P_0045 = '" & Trim(cod) & "'"

            dr = ExecuteDataSetFactor(Str)

            If dr.Tables(0).Rows.Count > 0 Then
                pfl.idPfl = dr.Tables(0).Rows(0).Item(0)
                pfl.descPfl = dr.Tables(0).Rows(0).Item(1)
                Return pfl
            End If

            Return Nothing
        Catch ex As Exception
            errorMsg &= "---findPerfil:" & ex.Message
            Return Nothing
        End Try

    End Function

    '//*3*********************************************************************//
    Public Function AccessWeb(ByVal sistema As String, ByVal puerta As String, ByVal Obs As String, ByVal login As String) As Boolean

        Dim dr As DataSet
        Dim Str As String

        Try

            Str = ""
            Str &= "SELECT distinct 'PERMISO' "
            Str &= "FROM NPP N, PUE P, SYS S "
            Str &= "WHERE N.NPP_COD_PUE = P.PUE_COD_PUE AND S.SYS_COD = P.SYS_COD AND "
            Str &= "P.PUE_COD_PUE = '" & Trim(puerta) & "' AND P.SYS_COD = " & Trim(sistema) & " AND "
            Str &= "S.SYS_BLK = 1 AND "
            Str &= "(N.NPP_COD_PFL IN (select id_P0045 from " & BD_FACTOR() & "..nef N, " & BD_FACTOR() & "..eje E "
            Str &= "		 		     where N.id_eje = E.id_eje and E.eje_des_cra = '" & login & "') or "
            Str &= "N.NPP_COD_PFL = (select USR_CAR_PFL from usr where USR_LOG_USR = '" & login & "')) 					"

            dr = ExecuteDataSet(Str)

            If dr.Tables(0).Rows.Count > 0 Then

                Try

                    grabarTransaccion(Trim(login), Trim(puerta), Trim(sistema), 1, Trim(Obs.ToString))

                    Return True

                Catch ex As Exception
                    Me.errorMsg = ex.Message
                End Try

            End If

            Me.errorMsg = "No tiene permiso para realizar esta accion por su perfil en la puerta: " & DvPuerta(sistema, puerta)
            Return False

        Catch ex As Exception
            errorMsg &= "---AccesoWeb:" & ex.Message
            Return False
        End Try

    End Function

    Public Function Bloqueo(ByVal sis As String) As Boolean

        Dim dr As DataSet
        Dim Str As String

        Try

            If sis <> "1" Then

                Str = ""
                Str = "SELECT SIS_MSG_BLK "
                Str &= "from  SIS "
                Str &= "Where SIS_BLK_SIS = '" & 2 & "'"

                dr = ExecuteDataSet(Str)

                If dr.Tables(0).Rows.Count > 0 Then
                    Me.errorMsg = dr.Tables(0).Rows(0).Item(0)
                    Return True
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            errorMsg &= "---BloqueoNEG:" & ex.Message
            Return False
        End Try

    End Function

    Public Function SisBloqueado(ByVal Sis As String) As Boolean

        Dim dr As DataSet
        Dim Str As String

        Try

            Str = "SELECT 1 from SYS where SYS_COD = '" & Trim(Sis) & "' and SYS_BLK = '" & 2 & "'"

            dr = ExecuteDataSet(Str)

            If dr.Tables(0).Rows.Count > 0 Then
                Me.errorMsg = "Sistema " & Trim(Sis) & " Bloqueado "
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            errorMsg &= "---SisBloqueado:" & ex.Message
            Return False
        End Try

    End Function

    Public Function getCodPerfil(ByVal pUser As String) As Int32

        Dim Str As String
        Dim Lee As DataSet
        Dim Val As Int32

        Try
            Str = ""
            Str &= " select USR_CAR_PFL "
            Str &= " from USR "
            Str &= " where "
            Str &= " USR_LOG_USR = '" & Trim(pUser) & "' "

            Lee = ExecuteDataSet(Str)

            If Lee.Tables(0).Rows.Count > 0 Then
                Val = Lee.Tables(0).Rows(0).Item(0)
                Return Val
            Else
                Return 0
            End If

        Catch ex As Exception
            errorMsg &= "---getCodPerfil:" & ex.Message
            Return Nothing
        End Try

    End Function

    Public Sub grabarTransaccion(ByVal loginUsr As String, ByVal puerta As String, ByVal sistema As String, ByVal resAcc As Integer, ByVal Obs As String)

        Dim val As String
        Dim SQL As String
        Dim msg As String
        Dim Fecha As String
        Dim Hora As String
        Dim codPerfil As Integer = findUsuario(loginUsr).idPerfil

        Fecha = Format(Date.Now, "yyyyMMdd")
        Hora = Format(Date.Now, "hh:mm:ss")
        val = ""
        val &= "values('" & Trim(loginUsr) & "', "
        val &= " '" & Trim(puerta) & "', "
        val &= " '" & Trim(sistema) & "', "
        val &= " '" & codPerfil & " ', "
        val &= " '" & Trim(Fecha) & " " & Trim(Hora) & " ', "
        val &= "  " & resAcc & ", "
        val &= " '" & Obs & "')"

        Try

            SQL = "INSERT INTO BIT " & val

            Query(SQL)

        Catch ex As Exception
            errorMsg &= "---grabaTransaccion:" & ex.Message
        End Try

    End Sub

    Public Function DvPuerta(ByVal Sys As String, ByVal Cod As String) As String

        Dim Str As String, msg As String
        Dim Lee As DataSet
        Dim val As String

        Try
            Str = ""
            Str &= "SELECT PUE_DES_PUE FROM PUE "
            Str &= "WHERE PUE_COD_PUE ='" & Trim(Cod) & "' and "
            Str &= "      SYS_COD ='" & Trim(Sys) & "' "

            Lee = ExecuteDataSet(Str)

            If Lee.Tables(0).Rows.Count > 0 Then
                val = Lee.Tables(0).Rows(0).Item(0)
                Return val
            Else
                Return ""
            End If
        Catch ex As Exception
            msg = ex.Message
            Return ""
        End Try
    End Function

    '//*4*********************************************************************//

    Public Function ObjetoRetornaPueHabiWeb(ByVal Sistema As String, ByVal Padre As String, ByVal pUser As String) As ArrayList

        Dim dr As DataSet
        Dim Str As String
        Dim usr As Usuario

        Try

            Dim PuertasHab As New ArrayList

            If Sistema <> "1" Then
                If Bloqueo(Sistema.ToString) = True Then
                    PuertasHab.Add("00000")
                    Return PuertasHab
                End If
                If SisBloqueado(Sistema.ToString) = True Then
                    PuertasHab.Add("00000")
                    Return PuertasHab
                End If
            End If

            usr = findUsuario(Trim(pUser.ToString))
            If IsNothing(usr) Then
                Me.errorMsg = "Usuario no encontrado"
                If PuertasHab.Count = 0 Then
                    PuertasHab.Add("00000")
                    Return PuertasHab
                End If
            End If

            Str = "SELECT DISTINCT PUE_COD_PUE, PUE_DES_PUE FROM PUE, NPP "
            Str &= "WHERE PUE_COD_PUE = NPP_COD_PUE AND "
            Str &= "(NPP_COD_PFL IN (select USR_CAR_PFL from USR WHERE USR_LOG_USR = '" & pUser.ToString & "') OR "
            Str &= "      NPP_COD_PFL IN (select id_P0045 from " & BD_FACTOR() & "..nef N, " & BD_FACTOR() & "..eje E "
            Str &= "      where N.id_eje = E.id_eje and E.eje_des_cra = '" & pUser.ToString & "')) AND "
            Str &= "      NPP_SIS_NPP = '" & Trim(Sistema) & "' AND "
            Str &= "      PUE_COD_SUP = '" & Trim(Padre) & "' AND "
            Str &= "      Exists (select 1 from acc where ACC_LOG_USR = '" & pUser.ToString & "') AND "
            Str &= "      Exists (select 1 from sys where sys_cod = NPP_SIS_NPP)"

            dr = ExecuteDataSet(Str)

            For i = 0 To dr.Tables(0).Rows.Count - 1
                PuertasHab.Add(dr.Tables(0).Rows(i).Item(0))
            Next

            If PuertasHab.Count = 0 Then
                PuertasHab.Add("00000")
            End If

            Return PuertasHab

        Catch ex As Exception
            Me.errorMsg &= "---ObjetoRetornaPueHabiWeb: " & ex.Message
            Return Nothing
        End Try

    End Function

    Public Function ExecuteDataSet(ByVal Sql As String) As DataSet

        Dim ds As New DataSet

        Try

            Dim adapter As SqlDataAdapter


            Dim myConnection As New SqlConnection(Conector)
            Dim command As New SqlCommand(Sql, myConnection)

            If myConnection.State <> ConnectionState.Open Then
                myConnection.Open()
            End If

            adapter = New SqlDataAdapter(command)

            adapter.Fill(ds)

            command.Connection.Close()
            myConnection.Close()

            'estadoconsulta = 1
            'descripcionconsulta = "OK"

        Catch ex As Exception
            Me.errorMsg &= "---ExecuteDataSet: " & ex.Message
        End Try


        Return ds

    End Function

    Public Function ExecuteDataSetFactor(ByVal Sql As String) As DataSet

        Dim ds As New DataSet

        Try

            Dim adapter As SqlDataAdapter


            Dim myConnection As New SqlConnection(ConectorFactor)
            Dim command As New SqlCommand(Sql, myConnection)

            If myConnection.State <> ConnectionState.Open Then
                myConnection.Open()
            End If

            adapter = New SqlDataAdapter(command)

            adapter.Fill(ds)

            command.Connection.Close()
            myConnection.Close()

            'estadoconsulta = 1
            'descripcionconsulta = "OK"

        Catch ex As Exception
            Me.errorMsg &= "---ExecuteDataSet: " & ex.Message
        End Try


        Return ds

    End Function

    Public Function Conector() As String

        Try


            Return ConfigurationManager.ConnectionStrings("SEGURIDADConnectionString").ConnectionString


        Catch ex As Exception
            errorMsg &= "---Conector:" & ex.Message
            Return Nothing
        End Try

    End Function

    Public Function ConectorFactor() As String

        Try


            Return ConfigurationManager.ConnectionStrings("FACTORConnectionString").ConnectionString

        Catch ex As Exception
            errorMsg &= "---Conector:" & ex.Message
            Return Nothing
        End Try

    End Function

    Public Function Query(ByVal SQL As String) As Integer

        Try

            Dim myConnection As New SqlConnection(Conector)
            Dim myCommand As New SqlCommand(SQL, myConnection)
            Dim resultado As Integer

            If myConnection.State <> ConnectionState.Open Then
                myConnection.Open()
            End If

            resultado = myCommand.ExecuteNonQuery
            myConnection.Close()

            Return resultado

        Catch ex As Exception
            errorMsg &= "---Query:" & ex.Message
        End Try

    End Function

    Public Function BD_FACTOR() As String
        Try
            Dim Str As String

            Str = ConfigurationManager.AppSettings("BD_FACTOR").ToString

            Return Str

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
