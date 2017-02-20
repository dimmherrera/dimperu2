Imports FuncionesGenerales.RutinasWeb
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Xml
Imports System.IO
Imports FuncionesGenerales.Variables

Public Class FComunes

    Dim FMT As New clslocateinfo

#Region "MANEJO DE FECHA Y HORA "

    Public Function transforma_minutos_en_hora_minuto(ByVal Tiempo As Integer) As String
        Try
            Dim horas As Integer
            Dim minutos As Integer
            Dim hora_minutos As String
            If Tiempo > 60 Then
                horas = Tiempo / 60
                minutos = Tiempo Mod 60
                hora_minutos = horas.ToString("00") & ":" & minutos.ToString("00")
                Return hora_minutos
            Else
                hora_minutos = "00" & ":" & Tiempo.ToString("00")
                Return hora_minutos
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DevuelveUltimoDiaDelMes(ByVal Mes As Int16, ByVal Ano As Int16) As Int16

        Select Case Mes

            Case 1, 3, 5, 7, 8, 10, 12

                Return 31

            Case 2

                If (Ano Mod 4) = 0 Then
                    Return 29
                Else
                    Return 28
                End If

            Case 4, 6, 9, 11

                Return 30

        End Select

    End Function

    Public Function CANTIDADDEHORAS(ByVal FECHA As String, ByVal FECHA_APR As String, ByVal ESTADO As Integer) As String
        Dim AUX As String
        Dim fecha_ing As String
        Dim fecha_sis As String
        Dim FECHA_AUX As String
        Dim fecha_con As Integer
        CANTIDADDEHORAS = 0
        If ESTADO = 1 Then
            If FECHA = "" Then
                CANTIDADDEHORAS = ""
                AUX = CANTIDADDEHORAS

            Else
                fecha_ing = FECHA
                If FECHA_APR = "" Then
                    fecha_sis = Now
                Else
                    fecha_sis = FECHA_APR
                End If

                'Dim DifFechas As TimeSpan
                'DifFechas = CDate(fecha_sis) - CDate(fecha_ing)
                'If DifFechas.Minutes <= 0 Then
                'End If

                FECHA_AUX = DateDiff("n", fecha_ing, fecha_sis)

                If FECHA_AUX <= "0" Then
                    FECHA_AUX = (FECHA_AUX - FECHA_AUX) - FECHA_AUX
                End If

                Do Until Val(FECHA_AUX) <= 59
                    If (Val(FECHA_AUX) > 59) Then
                        fecha_con = fecha_con + 1
                        FECHA_AUX = FECHA_AUX - 60
                    End If
                Loop
                CANTIDADDEHORAS = Format(CLng(fecha_con), "00") & ":" & Format(CLng(FECHA_AUX), "00")
                AUX = CANTIDADDEHORAS

            End If
            Return AUX
        End If

        If ESTADO = 2 Then
            If FECHA = "" Then
                CANTIDADDEHORAS = ""
                AUX = CANTIDADDEHORAS
                Return AUX
            Else
                fecha_ing = FECHA
                fecha_sis = FECHA_APR
                'If FECHA_APR = "" Then fecha_sis = Now Else  fecha_sis = FECHA_APR End If
                FECHA_AUX = DateDiff("n", fecha_ing, fecha_sis)
                Do
                    If (Val(FECHA_AUX) > 59) Then
                        fecha_con = fecha_con + 1
                        FECHA_AUX = FECHA_AUX - 60
                    End If
                Loop Until Val(FECHA_AUX) <= 59
                CANTIDADDEHORAS = Format(CDbl(fecha_con), "00") & ":" & Format(CDbl(FECHA_AUX), "00")
                AUX = CANTIDADDEHORAS

            End If
            Return AUX
        End If



    End Function

    Public Sub SortCollection(ByVal col As Collection, _
                                         ByVal psSortPropertyName As String, ByVal pbAscending As Boolean, _
                                        Optional ByVal psKeyPropertyName As String = "")
        Dim obj As Object
        Dim i As Integer
        Dim j As Integer
        Dim iMinMaxIndex As Integer
        Dim vMinMax As Object
        Dim vValue As Object
        Dim bSortCondition As Boolean
        Dim bUseKey As Boolean
        Dim sKey As String
        bUseKey = (psKeyPropertyName <> "")
        For i = 1 To col.Count - 1
            obj = col(i)
            vMinMax = CallByName(obj, psSortPropertyName, vbGet)
            iMinMaxIndex = i
            For j = i + 1 To col.Count
                obj = col(j)
                vValue = CallByName(obj, _
                    psSortPropertyName, vbGet)
                If (pbAscending) Then
                    bSortCondition = (vValue < vMinMax)
                Else
                    bSortCondition = (vValue > vMinMax)
                End If
                If (bSortCondition) Then
                    vMinMax = vValue
                    iMinMaxIndex = j
                End If
                obj = Nothing
            Next j
            If (iMinMaxIndex <> i) Then
                obj = col(iMinMaxIndex)
                col.Remove(iMinMaxIndex)
                If (bUseKey) Then
                    sKey = CStr(CallByName(obj, _
                       psKeyPropertyName, vbGet))
                    col.Add(obj, sKey, i)
                Else
                    col.Add(obj, , i)
                End If
                obj = Nothing
            End If
            obj = Nothing
        Next i
    End Sub

    Public Shared Function FUNFecReg(ByVal pFecha As String) As String
        Dim LocFecha As String
        Dim dia As Integer
        Dim mes As Integer
        Dim ano As Integer

        'Dim formato As String = ConfigurationSettings.AppSettings("Fecha")

        dia = Microsoft.VisualBasic.Day(pFecha)
        mes = Microsoft.VisualBasic.Month(pFecha)
        ano = Microsoft.VisualBasic.Year(pFecha)

        'Select Case formato
        'Case "dd/MM/yyyy"
        LocFecha = Microsoft.VisualBasic.Format(dia, "00") & "-" & Microsoft.VisualBasic.Format(mes, "00") & "-" & Microsoft.VisualBasic.Format(ano, "0000")
        '    Case "MM/dd/yyyy"
        'LocFecha = Microsoft.VisualBasic.Format(mes, "00") & "/" & Microsoft.VisualBasic.Format(dia, "00") & "/" & Microsoft.VisualBasic.Format(ano, "0000")
        '    Case "yyyyMMdd"
        'LocFecha = Microsoft.VisualBasic.Format(ano, "00000") & Microsoft.VisualBasic.Format(mes, "00") & Microsoft.VisualBasic.Format(dia, "00")
        'End Select

        Return LocFecha

    End Function

    Public Shared Function FUNFechaJul(ByVal pFecha As String) As String

        Dim LocFecha As String
        Dim dia As Int16
        Dim mes As Int16
        Dim ano As Int16

        ano = Mid(pFecha, 7, 4)
        mes = Mid(pFecha, 4, 2)
        dia = Mid(pFecha, 1, 2)

        LocFecha = Format(ano, "0000") & Format(mes, "00") & Format(dia, "00")

        Return LocFecha

    End Function

    Public Shared Function devHora() As String
        Return Format(Date.Now.Hour, "00") & ":" & Format(Date.Now.Minute, "00") & ":" & Format(Date.Now.Second, "00")
    End Function

    Public Function RetornaDia(ByVal Dia As Int16) As String

        Select Case Dia

            Case 1

                Return "LUNES"

            Case 2

                Return "MARTES"

            Case 3

                Return "MIERCOLES"

            Case 4

                Return "JUEVES"

            Case 5

                Return "VIERNES"

            Case 6

                Return "SABADO"

            Case 0

                Return "DOMINGO"

        End Select

        Return Nothing

    End Function

#End Region

#Region "MANEJO DE RUT"

    Public Function Vrut(ByVal unNit As String) As String

        Dim sql As New SqlQuery
        Dim data As DataSet
        Dim digito As String

        unNit = Replace(Trim(unNit), ".", "")

        Try

            data = sql.ExecuteDataSet("Select dbo.CalculaDigitoRut(" & unNit & ")")

            If data.Tables(0).Rows.Count > 0 Then
                digito = data.Tables(0).Rows(0).Item(0).ToString()
            End If

        Catch ex As Exception
        Finally
            data.Dispose()
        End Try

        Return digito

    End Function

    Public Function VrutCambio(ByVal unNit As String, ByVal tipo As Integer) As String

        Dim sql As New SqlQuery
        Dim data As DataSet
        Dim digito As String

        unNit = Replace(Trim(unNit), ".", "")

        Try

            data = sql.ExecuteDataSet("Select dbo.CalculaDigitoRutCambia(" & unNit & ", '" & tipo & "')")

            If data.Tables(0).Rows.Count > 0 Then
                digito = data.Tables(0).Rows(0).Item(0).ToString()
            End If

        Catch ex As Exception
        Finally
            data.Dispose()
        End Try

        Return digito

    End Function

    Public Function LimpiaRut(ByVal Rut As String) As String

        Try

            Rut = Rut.Replace(".", "")
            Rut = Rut.Replace("-", "")
            Rut = Rut.Substring(0, Rut.Length - 1)

            Return Rut

        Catch ex As Exception
            Return Nothing
        End Try


    End Function

#End Region

#Region "MANEJO DE COLLECTION"

    Public Shared Function AgregaCom(ByVal StrValor As String) As String
        StrValor = Replace(StrValor, "'", "")
        StrValor = Replace(StrValor, ",", "")
        StrValor = "'" & UCase(Trim(StrValor)) & "'"
        AgregaCom = StrValor
    End Function

    Public Shared Sub BorraCollection(ByVal Coll As Collection)

        Try

            Dim I As Integer
            For I = 1 To Coll.Count
                If Coll.Count <= I Then I = 1
                If Coll.Count = 0 Then Exit For
                Coll.Remove(I)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function BuscaCollection(ByVal Coll As Collection, ByVal valor As String) As Boolean

        Try

            Dim I As Integer

            For I = 1 To Coll.Count
                If Coll(I) = valor Then
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "MANEJO DE NUMEROS"

    Public Function FormatoMiles(ByVal monto As String) As String
        Dim lugar As Integer
        Dim paso As String
        Dim negativo As String
        Dim lugarIII As Integer
        Dim miles As String
        Dim decimales As String
        Dim aux As String
        Dim i As Integer
        Dim cont As Integer
        Dim lugarII As Integer

        If InStr(1, monto, ".") > 0 Then
            FormatoMiles = Trim(monto)
        Else

            ' si es negativo
            '-------------------------------
            lugar = InStr(1, monto, "-")
            If lugar <> 0 Then
                paso = Mid(monto, 2)
                negativo = "-"
            Else
                paso = monto
                negativo = ""
            End If
            '-------------------------------

            ' si es con decimal
            '-------------------------------

            lugarIII = InStr(1, paso, ",")
            If lugarIII <> 0 Then
                miles = Mid(paso, 1, lugarIII - 1)
                decimales = Mid(paso, lugarIII + 1, Len(paso))
            Else
                miles = paso
                decimales = ""
            End If

            '-----------------------------



            aux = ""
            i = Len(miles)
            cont = 0
            Do While i > 0
                If cont = 3 Then
                    aux = "." & aux
                    cont = 0
                End If
                aux = Mid(miles, i, 1) & aux
                cont = cont + 1
                i = i - 1
            Loop



            lugarII = InStr(1, aux, ".,")

            If lugarII <> 0 Then
                aux = Mid(aux, 1, lugarII - 1) & Mid(aux, lugarII + 1, Len(aux))
            End If


            If Len(Trim(decimales)) > 0 Then
                FormatoMiles = Trim(negativo & aux & "," & decimales)
            Else
                FormatoMiles = Trim(negativo & aux)
            End If

        End If

    End Function

    Public Shared Function Pone_Decimal(ByVal Cadena As String) As String

        Dim c As Integer

        Cadena = CStr(Cadena)
        c = InStr(1, Cadena, ",")

        If c > 0 Then
            Select Case Len(Mid(Cadena, c + 1))
                Case 1
                    Return Mid(Cadena, 1, c) & Mid(Cadena, c + 1) & "0"
                Case 2
                    Return Cadena
            End Select

            Return Nothing
        Else
            Return Cadena & ",00"
        End If

    End Function

    Public Shared Function Round(ByVal Expresion As Object, ByVal DigitoDecimal As Integer) As String
        If Len(Trim(CStr(Expresion))) = 0 Then Return ""
        Return FormatNumber(Expresion, DigitoDecimal, , , TriState.False)
    End Function

    Public Function comasXptos(ByVal Monto As String) As String

        Dim Mto As String
        Mto = Replace(Monto, ".", "")
        comasXptos = Replace(Mto, ",", ".")

    End Function

    Public Function MAX(ByVal Par1 As Object, ByVal Par2 As Object) As Object

        MAX = IIf(Par1 > Par2, Par1, Par2)

    End Function

    Public Function MIN(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        MIN = IIf(Par1 < Par2, Par1, Par2)
    End Function

    Public Function RETORNA_NUMERO(ByVal NUMERO_DECIMAL As String) As String
        Dim VALOR_ENTERO As String
        Dim VALOR_DECIMAL As String

        RETORNA_NUMERO = 0
        If InStr(NUMERO_DECIMAL, FMT.FCMCD) > 0 Then
            VALOR_ENTERO = Mid(NUMERO_DECIMAL, 1, InStr(NUMERO_DECIMAL, FMT.FCMCD) - 1)
            VALOR_DECIMAL = Mid(NUMERO_DECIMAL, InStr(NUMERO_DECIMAL, FMT.FCMCD) + 1)
            RETORNA_NUMERO = VALOR_ENTERO & FMT.FCMCD & VALOR_DECIMAL
        End If

    End Function

#End Region

#Region "MANEJO DE MONEDA"

    Public Function DevuelveFormatoMoneda(ByVal moneda As Integer) As String

        Dim Locate As New ClsLocateInfo
        Dim Formato As String = ""

        Select Case moneda
            Case 0, 1 : Formato = Locate.FCMSD
            Case 2 : Formato = Locate.FCMCD4
            Case 3, 4 : Formato = Locate.FCMCD
        End Select

        Return Formato

    End Function

    Public Enum TipoMoneda As Byte
        PESOS = 1
        'UF = 2
        DOLLAR = 3
        EURO = 4
    End Enum

#End Region

    Public Function ExtraerExtension(ByVal Path As String, ByVal Caracter As String) As String
        Dim ret As String
        If Caracter = "." And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))

        If Caracter = "|" And InStr(Path, Caracter) = 0 Then Exit Function
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))

        ' -- Retorna el valor  
        Return ret
    End Function

    Public Function EliminarAcentos(ByVal texto) As String

        Dim i As Integer
        Dim s1 As String
        Dim s2 As String
        s1 = "¡¿…»Õœ”“⁄‹·‡ËÈÌÔÛÚ˙¸ÒÁ"
        s2 = "AAEEIIOOUUaaeeiioouunc"
        If Len(texto) <> 0 Then
            For i = 1 To Len(s1)
                texto = Replace(texto, Mid(s1, i, 1), Mid(s2, i, 1))
            Next
        End If

        Return texto

    End Function

    Public Function IsAÒoBisiesto(ByVal YYYY As Integer) As Boolean
        Return YYYY Mod 4 = 0 _
                    And (YYYY Mod 100 <> 0 Or YYYY Mod 400 = 0)
    End Function

    Public Function FechaJuliana(ByVal fecha As String) As String
        Return DateTime.Parse(fecha).ToString("yyyyMMdd")
    End Function

End Class
