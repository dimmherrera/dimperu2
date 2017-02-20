
Public Class ClsLocateInfo


    Public const_Sig_Money As String  '/*Signo Moneda*/
    Public const_Dec_Money As String  '/*Separador Decimal Money*/
    Public const_Mil_Money As String  '/*Separador Miles Money*/
    Public const_Dec_Number As String '/*Separador Decimal Number*/
    Public const_Mil_Number As String '/*Separador Miles Number*/
    Public const_Fec_Date As String   '/*Formato Fecha Corta*/
    Public const_Sep_Date As String   '/*Formato Separador Fecha*/
    Public const_Sep_Hours As String  '/*Formato Separador Fecha*/

    '**************************************************************************
    Public fmt_ConMilConDec As String 'Nro. Sep. de Miles C/ Decimales
    Public fmt_SinMilConDec As String 'Nro. sin Sep. de Miles C/ Decimales
    Public fmt_ConMilSinDec As String 'Nro. Sep. de Miles S/ Decimales
    Public fmt_SinMilSinDec As String 'Nro. sin Sep. de Miles S/ Decimales
    Public fmt_SinMilConDec4 As String 'Nro. sin Sep. de Miles C/ 4Decimales
    Public fmt_ConMilConDec4 As String 'Nro. sin Sep. de Miles C/ 4Decimales
    '**************************************************************************

    Public Sub New()

        const_Mil_Number = ","
        const_Dec_Number = "."

        'const_Mil_Number = "."
        'const_Dec_Number = ","

        'Nro. Sep. de Miles C/ Decimales
        fmt_ConMilConDec = "###" & const_Mil_Number & "###" & const_Mil_Number & "##0" & const_Dec_Number & "00"

        'Nro. sin Sep. de Miles C/ Decimales
        fmt_SinMilConDec = "########0" & const_Dec_Number & "00"

        'Nro. Sep. de Miles S/ Decimales
        fmt_ConMilSinDec = "###" & const_Mil_Number & "###" & const_Mil_Number & "##0"

        'Nro. sin Sep. de Miles S/ Decimales
        fmt_SinMilSinDec = "########0"

        'Nro. sin Sep. de Miles c/ 4 Decimales
        fmt_SinMilConDec4 = "########0" & const_Dec_Number & "0000"

        'Nro. con Sep. de Miles c/ 4 Decimales
        fmt_ConMilConDec4 = "###" & const_Mil_Number & "###" & const_Mil_Number & "##0" & const_Dec_Number & "0000"
        
    End Sub


    Public Function FCMCD() As String
        FCMCD = fmt_ConMilConDec '###,###,##0.00
    End Function

    Public Function FSMCD() As String
        FSMCD = fmt_SinMilConDec '########0.00
    End Function

    Public Function FSMSD() As String
        FSMSD = fmt_SinMilSinDec '########0
    End Function

    Public Function FCMSD() As String
        FCMSD = fmt_ConMilSinDec '###,###,###,##0
    End Function

    Public Function FSMCD4() As String
        FSMCD4 = fmt_SinMilConDec4 '########0.0000
    End Function

    Public Function FCMCD4() As String
        FCMCD4 = fmt_ConMilConDec4 '###,###,##0.0000
    End Function

End Class
