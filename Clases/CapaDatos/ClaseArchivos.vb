Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ClaseArchivos
    Dim FC As New FuncionesGenerales.FComunes
    Dim tsn As New FuncionesGenerales.SqlQuery
    Dim cg As New CapaDatos.ConsultasGenerales

#Region "Archivos"

    Public Function Archivo_Sinacofi(ByVal pathStr As String) As Boolean
        Dim sinac As DataSet


        Dim fac_vig_cli, fac_vig_cli_00_30, fac_vig_cli_31_60, fac_vig_cli_61_90, fac_vig_cli_mas, _
            let_vig_cli, let_vig_cli_00_30, let_vig_cli_31_60, let_vig_cli_61_90, let_vig_cli_mas, _
            otr_vig_cli, otr_vig_cli_00_30, otr_vig_cli_31_60, otr_vig_cli_61_90, otr_vig_cli_mas, _
            fac_vig_ddr, fac_vig_ddr_00_30, fac_vig_ddr_31_60, fac_vig_ddr_61_90, fac_vig_ddr_mas, _
            let_vig_ddr, let_vig_ddr_00_30, let_vig_ddr_31_60, let_vig_ddr_61_90, let_vig_ddr_mas, _
            otr_vig_ddr, otr_vig_ddr_00_30, otr_vig_ddr_31_60, otr_vig_ddr_61_90, otr_vig_ddr_mas, _
            fac_mor_cli_00_30, fac_mor_cli_31_60, fac_mor_cli_61_90, fac_mor_cli_91, _
            let_mor_cli_00_30, let_mor_cli_31_60, let_mor_cli_61_90, let_mor_cli_91, _
            otr_mor_cli_00_30, otr_mor_cli_31_60, otr_mor_cli_61_90, otr_mor_cli_91 _
            As Double

        Dim TotalVigente, TotalMora1_30, TotalMora31_60, TotalMora61_90, TotalMora91 As Double
        Dim cli_idc As String
        Dim cli_dml As String
        Dim cmn_des As String
        Dim cli_rso As String

        Try

            'Dim NombreArchivo As String
            'Dim Archivo As System.IO.FileStream

            'NombreArchivo = "DMF6016" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
            'Archivo_Sinacofi = true

            'Archivo = System.IO.File.Create(pathStr & "\" & NombreArchivo)

            Dim oSW As New StreamWriter(pathStr)
            Dim Linea As String

            sinac = tsn.ExecuteDataSet("exec sp_arc_dev_cliente_sinacofi")

            For i = 0 To sinac.Tables(0).Rows.Count - 1

                '--------------------------------------------------------------------------------------------------------------------------------
                'Cabecera
                '--------------------------------------------------------------------------------------------------------------------------------
                If i = 0 Then
                    'Variable Para control cambio Cliente 
                    cli_idc = sinac.Tables(0).Rows(i).Item("cli_idc")
                    cli_dml = sinac.Tables(0).Rows(i).Item("cli_dml")
                    cmn_des = sinac.Tables(0).Rows(i).Item("cmn_des")
                    cli_rso = sinac.Tables(0).Rows(i).Item("cli_rso")

                    Linea = Format(sinac.Tables(0).Rows(i).Item("emp_idc"), "000000000") & FC.Vrut(Format(sinac.Tables(0).Rows(i).Item("emp_idc"), "0")) & _
                            Format(DateAdd("d", -1, Format(sinac.Tables(0).Rows(i).Item("fec_ser"), "dd/MM/yyyy")), "yyyyMMdd") & _
                            Format((sinac.Tables(0).Rows(i).Item("tot_reg")) + 1, "000000") & _
                            Space(426)

                    oSW.WriteLine(Linea)
                End If


                If cli_idc <> sinac.Tables(0).Rows(i).Item("cli_idc") Then

Ultimo_Cliente:
                    '--------------------------------------------------------------------------------------------------------------------------------
                    'Forma Linea Detalle
                    '--------------------------------------------------------------------------------------------------------------------------------
                    Linea = Mid(cli_rso, 1, 40) & Space(40 - Len(Mid(cli_rso, 1, 40))) & _
                            Mid(cli_dml, 1, 40) & Space(40 - Len(Mid(cli_dml, 1, 40))) & _
                            Mid(cmn_des, 1, 20) & Space(20 - Len(Mid(cmn_des, 1, 20))) & _
                            Format(cli_idc, "000000000") & _
                            FC.Vrut(cli_idc)

                    '------------------------------------------------------------
                    'Deuda Como Deudor
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_vig_ddr / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_ddr / 1000, "00000000") & _
                                    Format(otr_vig_ddr / 1000, "00000000")

                    Linea = Linea & Format(fac_vig_ddr_00_30 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_ddr_00_30 / 1000, "00000000") & _
                                    Format(otr_vig_ddr_00_30 / 1000, "00000000")

                    Linea = Linea & Format(fac_vig_ddr_31_60 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_ddr_31_60 / 1000, "00000000") & _
                                    Format(otr_vig_ddr_31_60 / 1000, "00000000")

                    Linea = Linea & Format(fac_vig_ddr_61_90 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_ddr_61_90 / 1000, "00000000") & _
                                    Format(otr_vig_ddr_61_90 / 1000, "00000000")

                    Linea = Linea & Format(fac_vig_ddr_mas / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_ddr_mas / 1000, "00000000") & _
                                    Format(otr_vig_ddr_mas / 1000, "00000000")
                    Linea = Linea & Format((fac_vig_ddr + let_vig_ddr + otr_vig_ddr) / 1000, "00000000")

                    '------------------------------------------------------------
                    'Deuda Como Cliente
                    '------------------------------------------------------------
                    '------------------------------------------------------------
                    'DOCTOS. VIGENTES  
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_vig_cli / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_vig_cli / 1000, "00000000") & _
                                    Format(otr_vig_cli / 1000, "00000000")

                    '------------------------------------------------------------
                    'DOCTOS. MORA 1-30 
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_mor_cli_00_30 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_mor_cli_00_30 / 1000, "00000000") & _
                                    Format(otr_mor_cli_00_30 / 1000, "00000000")

                    '------------------------------------------------------------
                    'DOCTOS. MORA 31-60 
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_mor_cli_31_60 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_mor_cli_31_60 / 1000, "00000000") & _
                                    Format(otr_mor_cli_31_60 / 1000, "00000000")

                    '------------------------------------------------------------
                    'DOCTOS. MORA 61-90 
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_mor_cli_61_90 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_mor_cli_61_90 / 1000, "00000000") & _
                                    Format(otr_mor_cli_61_90 / 1000, "00000000")

                    '------------------------------------------------------------
                    'DOCTOS. MORA 91-MAS 
                    '------------------------------------------------------------
                    Linea = Linea & Format(fac_mor_cli_91 / 1000, "00000000") & _
                                    "00000000" & _
                                    Format(let_mor_cli_91 / 1000, "00000000") & _
                                    Format(otr_mor_cli_91 / 1000, "00000000")

                    '------------------------------------------------------------
                    'TOTALES            
                    '------------------------------------------------------------
                    TotalVigente = Format(fac_vig_cli / 1000, "00000000") + Val("00000000") + Format(let_vig_cli / 1000, "00000000") + Format(otr_vig_cli / 1000, "00000000")
                    TotalMora1_30 = Format(fac_mor_cli_00_30 / 1000, "00000000") + Val("00000000") + Format(let_mor_cli_00_30 / 1000, "00000000") + Format(otr_mor_cli_00_30 / 1000, "00000000")
                    TotalMora31_60 = Format(fac_mor_cli_31_60 / 1000, "00000000") + Val("00000000") + Format(let_mor_cli_31_60 / 1000, "00000000") + Format(otr_mor_cli_31_60 / 1000, "00000000")
                    TotalMora61_90 = Format(fac_mor_cli_61_90 / 1000, "00000000") + Val("00000000") + Format(let_mor_cli_61_90 / 1000, "00000000") + Format(otr_mor_cli_61_90 / 1000, "00000000")
                    TotalMora91 = Format(fac_mor_cli_91 / 1000, "00000000") + Val("00000000") + Format(let_mor_cli_91 / 1000, "00000000") + Format(otr_mor_cli_91 / 1000, "00000000")

                    Linea = Linea & Format(TotalVigente + TotalMora1_30 + TotalMora31_60 + TotalMora61_90 + TotalMora91, "00000000")

                    Linea = Linea & Space(4)

                    '------------------------------------------------------------
                    'Escribe Linea en Archivo
                    '------------------------------------------------------------
                    oSW.WriteLine(Linea)
                    oSW.Flush()

                    '------------------------------------------------------------
                    'Valida Si es ultimo registro
                    '------------------------------------------------------------
                    If i = sinac.Tables(0).Rows.Count - 1 Then
                        Exit For
                    End If

                    cli_idc = sinac.Tables(0).Rows(i).Item("cli_idc")
                    cli_rso = sinac.Tables(0).Rows(i).Item("cli_rso")
                    cli_dml = sinac.Tables(0).Rows(i).Item("cli_dml")
                    cmn_des = sinac.Tables(0).Rows(i).Item("cmn_des")

                    '------------------------------------------------------------
                    'Limpia Variables Documentos Factura Vigentes Cliente
                    '------------------------------------------------------------
                    fac_vig_cli = 0 : fac_vig_cli_00_30 = 0
                    fac_vig_cli_31_60 = 0 : fac_vig_cli_61_90 = 0
                    fac_vig_cli_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Documentos Factura Vigentes Deudor
                    '------------------------------------------------------------
                    fac_vig_ddr = 0 : fac_vig_ddr_00_30 = 0
                    fac_vig_ddr_31_60 = 0 : fac_vig_ddr_61_90 = 0
                    fac_vig_ddr_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Documentos Letra Vigentes Cliente
                    '------------------------------------------------------------
                    let_vig_cli = 0 : let_vig_cli_00_30 = 0
                    let_vig_cli_31_60 = 0 : let_vig_cli_61_90 = 0
                    let_vig_cli_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Documentos Letra Vigentes Deudor
                    '------------------------------------------------------------
                    let_vig_ddr = 0 : let_vig_ddr_00_30 = 0
                    let_vig_ddr_31_60 = 0 : let_vig_ddr_61_90 = 0
                    let_vig_ddr_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Otros Documentos Cliente
                    '------------------------------------------------------------
                    otr_vig_cli = 0 : otr_vig_cli_00_30 = 0
                    otr_vig_cli_31_60 = 0 : otr_vig_cli_61_90 = 0
                    otr_vig_cli_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Otros Documentos Deudor
                    '------------------------------------------------------------
                    otr_vig_ddr = 0 : otr_vig_ddr_00_30 = 0
                    otr_vig_ddr_31_60 = 0 : otr_vig_ddr_61_90 = 0
                    otr_vig_ddr_mas = 0
                    '------------------------------------------------------------
                    'Limpia Variables Documentos Factura morosa Cliente 1-30
                    '------------------------------------------------------------
                    fac_mor_cli_00_30 = 0
                    fac_mor_cli_31_60 = 0
                    fac_mor_cli_61_90 = 0
                    fac_mor_cli_91 = 0
                    '------------------------------------------------------------
                    'Limpia Variables Documentos Letra morosa Cliente 1-30
                    '------------------------------------------------------------
                    let_mor_cli_00_30 = 0
                    let_mor_cli_31_60 = 0
                    let_mor_cli_61_90 = 0
                    let_mor_cli_91 = 0
                    '------------------------------------------------------------
                    'Limpia Variables Otros Documentos moroso Cliente
                    '------------------------------------------------------------
                    otr_mor_cli_00_30 = 0
                    otr_mor_cli_31_60 = 0
                    otr_mor_cli_61_90 = 0
                    otr_mor_cli_91 = 0
                End If

                '--------------------------------------------------------------------------------------------------------------------------------
                'Detalle (Calculo Variables)
                '--------------------------------------------------------------------------------------------------------------------------------
                If sinac.Tables(0).Rows(i).Item("pnu_tip_doc") = 1 Then
                    'Documentos Factura Vigentes Cliente
                    fac_vig_cli = sinac.Tables(0).Rows(i).Item("dsb_ven_001") + sinac.Tables(0).Rows(i).Item("dsb_ven_002") + sinac.Tables(0).Rows(i).Item("dsb_ven_003") + _
                                  sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005") + sinac.Tables(0).Rows(i).Item("dsb_mor_001") + _
                                  sinac.Tables(0).Rows(i).Item("dsb_mor_002") '+ Adorec!dsb_mor_001 + Adorec!dsb_mor_002 + Adorec!dsb_mor_003(+Adorec!dsb_mor_004 + Adorec!dsb_mor_005)

                    fac_vig_cli_00_30 = 0
                    fac_vig_cli_31_60 = sinac.Tables(0).Rows(i).Item("dsb_ven_002")
                    fac_vig_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_ven_003")
                    fac_vig_cli_mas = sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005")

                    'Documentos Factura Vigentes Deudor
                    fac_vig_ddr = 0 'Adorec!dsb_ven_ddr_001 + Adorec!dsb_ven_ddr_002 + Adorec!dsb_ven_ddr_003 + Adorec!dsb_ven_ddr_004(+Adorec!dsb_ven_ddr_005 + Adorec!dsb_mor_ddr_001 + Adorec!dsb_mor_ddr_002 + Adorec!dsb_mor_ddr_003 + Adorec!dsb_mor_ddr_004 + Adorec!dsb_mor_ddr_005)

                    fac_vig_ddr_00_30 = 0 'Adorec!dsb_mor_ddr_001
                    fac_vig_ddr_31_60 = 0 'Adorec!dsb_mor_ddr_002
                    fac_vig_ddr_61_90 = 0 'Adorec!dsb_mor_ddr_003
                    fac_vig_ddr_mas = 0 'Adorec!dsb_mor_ddr_004 + Adorec!dsb_mor_ddr_005


                    'Documentos Factura morosa Cliente 1-30
                    fac_mor_cli_00_30 = 0
                    fac_mor_cli_31_60 = 0
                    fac_mor_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_mor_003")
                    fac_mor_cli_91 = sinac.Tables(0).Rows(i).Item("dsb_mor_004") + sinac.Tables(0).Rows(i).Item("dsb_mor_005")

                End If

                If sinac.Tables(0).Rows(i).Item("pnu_tip_doc") = 2 Then
                    'Documentos Letra Vigentes Cliente
                    let_vig_cli = sinac.Tables(0).Rows(i).Item("dsb_ven_001") + sinac.Tables(0).Rows(i).Item("dsb_ven_002") + sinac.Tables(0).Rows(i).Item("dsb_ven_003") + _
                                  sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005") + sinac.Tables(0).Rows(i).Item("dsb_mor_001") + _
                                  sinac.Tables(0).Rows(i).Item("dsb_mor_002") '+ Adorec!dsb_mor_001 + Adorec!dsb_mor_002 + Adorec!dsb_mor_003(+Adorec!dsb_mor_004 + Adorec!dsb_mor_005)


                    let_vig_cli_00_30 = sinac.Tables(0).Rows(i).Item("dsb_ven_001")
                    let_vig_cli_31_60 = sinac.Tables(0).Rows(i).Item("dsb_ven_002")
                    let_vig_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_ven_003")
                    let_vig_cli_mas = sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005")

                    'Documentos Letra Vigentes Deudor
                    let_vig_ddr = 0 'Adorec!dsb_ven_ddr_001 + Adorec!dsb_ven_ddr_002 + Adorec!dsb_ven_ddr_003 + Adorec!dsb_ven_004(+Adorec!dsb_ven_005 + Adorec!dsb_mor_001 + Adorec!dsb_mor_002 + Adorec!dsb_mor_003 + Adorec!dsb_mor_004 + Adorec!dsb_mor_005)

                    let_vig_ddr_00_30 = 0 'Adorec!dsb_ven_ddr_001
                    let_vig_ddr_31_60 = 0 'Adorec!dsb_ven_ddr_002
                    let_vig_ddr_61_90 = 0 'Adorec!dsb_ven_ddr_003
                    let_vig_ddr_mas = 0 'Adorec!dsb_ven_ddr_004 + Adorec!dsb_ven_ddr_005

                    'Documentos Letra morosa Cliente 1-30
                    let_mor_cli_00_30 = 0
                    let_mor_cli_31_60 = 0
                    let_mor_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_mor_003")
                    let_mor_cli_91 = sinac.Tables(0).Rows(i).Item("dsb_mor_004") + sinac.Tables(0).Rows(i).Item("dsb_mor_005")

                End If

                If (sinac.Tables(0).Rows(i).Item("pnu_tip_doc") <> 1) And (sinac.Tables(0).Rows(i).Item("pnu_tip_doc") <> 2) Then
                    'Otros Documentos Cliente
                    otr_vig_cli = sinac.Tables(0).Rows(i).Item("dsb_ven_001") + sinac.Tables(0).Rows(i).Item("dsb_ven_002") + sinac.Tables(0).Rows(i).Item("dsb_ven_003") + _
                    sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005") + sinac.Tables(0).Rows(i).Item("dsb_mor_001") + _
                    sinac.Tables(0).Rows(i).Item("dsb_mor_002") '+ Adorec!dsb_mor_001 + Adorec!dsb_mor_002 + Adorec!dsb_mor_003(+Adorec!dsb_mor_004 + Adorec!dsb_mor_005)

                    otr_vig_cli_00_30 = sinac.Tables(0).Rows(i).Item("dsb_ven_001")
                    otr_vig_cli_31_60 = sinac.Tables(0).Rows(i).Item("dsb_ven_002")
                    otr_vig_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_ven_003")
                    otr_vig_cli_mas = sinac.Tables(0).Rows(i).Item("dsb_ven_004") + sinac.Tables(0).Rows(i).Item("dsb_ven_005")

                    'Otros Documentos Deudor
                    otr_vig_ddr = 0 'Adorec!dsb_ven_ddr_001 + Adorec!dsb_ven_ddr_002 + Adorec!dsb_ven_ddr_003 +                 Adorec!dsb_ven_004(+Adorec!dsb_ven_005 +                 Adorec!dsb_mor_001 + Adorec!dsb_mor_002 + Adorec!dsb_mor_003 +                 Adorec!dsb_mor_004 + Adorec!dsb_mor_005)

                    otr_vig_ddr_00_30 = 0 'Adorec!dsb_ven_ddr_001
                    otr_vig_ddr_31_60 = 0 'Adorec!dsb_ven_ddr_002
                    otr_vig_ddr_61_90 = 0 'Adorec!dsb_ven_ddr_003
                    otr_vig_ddr_mas = 0 'Adorec!dsb_ven_ddr_004 + Adorec!dsb_ven_ddr_005

                    'Documentos otro docto. morosa Cliente 1-30
                    otr_mor_cli_00_30 = 0
                    otr_mor_cli_31_60 = 0
                    otr_mor_cli_61_90 = sinac.Tables(0).Rows(i).Item("dsb_mor_003")
                    otr_mor_cli_91 = sinac.Tables(0).Rows(i).Item("dsb_mor_004") + sinac.Tables(0).Rows(i).Item("dsb_mor_005")
                End If

                If i = sinac.Tables(0).Rows.Count - 1 Then
                    GoTo Ultimo_Cliente
                End If

            Next

            oSW.Close()

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function
    Public Function VALIDA_CARACTERES(ByVal RAZON_SOCIAL_CLI_DEU As String) As String

        Dim IndiceX As Integer
        Dim TextoFinal As String
        Dim TextoParcial As String


        RAZON_SOCIAL_CLI_DEU = RAZON_SOCIAL_CLI_DEU
        TextoFinal = ""
        For IndiceX = 1 To Len(RAZON_SOCIAL_CLI_DEU)
            TextoParcial = UCase(Mid(RAZON_SOCIAL_CLI_DEU, IndiceX, 1))

            If Asc(TextoParcial) >= 48 And Asc(TextoParcial) <= 57 Then
                TextoFinal = TextoFinal + TextoParcial
            Else
                If Asc(TextoParcial) >= 65 And Asc(TextoParcial) <= 90 Then
                    TextoFinal = TextoFinal + TextoParcial
                Else
                    If Asc(TextoParcial) = 32 Or Asc(TextoParcial) = 38 Or Asc(TextoParcial) = 44 Or Asc(TextoParcial) = 47 Then
                        TextoFinal = TextoFinal + TextoParcial
                    Else
                        TextoFinal = TextoFinal + " "
                    End If
                End If
            End If
        Next

        VALIDA_CARACTERES = TextoFinal

    End Function
    Public Function Archivo_D24_Nuevo(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim D24 As DataSet
        Dim FC As New FuncionesGenerales.FComunes



        Dim linea As String
        Dim largo As Integer
        Dim paso As Boolean
        Dim dif_dias As String
        Dim moneda As Long
        Dim lineal As String


        Dim fecha_archivo As Object
        Dim fecha_calculo As Object 'ultimo dia del mes de el archivo sinacofi
        Dim razon_social_cli As String
        Dim actividad_eco_cli As String
        Dim razon_social_deu As String
        Dim actividad_eco_deu As String
        Dim tasa_calculada As Double
        Dim rut_cli_dig As String
        Dim tip_docto As Integer
        Dim rut_deu_dig As String
        Dim ope_gar_son As String
        Dim doc_rgo_adi As String
        Dim decimales As String
        Dim dif_precio As Double
        Dim val_act_neto As Double
        Dim tasa As String
        Dim dif_dia_sim_ori As Integer
        Dim dif_dia_ret As Integer
        Dim val_des As Integer

        Dim oSW As New StreamWriter(path)


        If TIPO = "M" Then
            'Validar que se hayan ingresado fechas ( mes , año)


            'Archivo D24 Mensual

            'fecha archivo d24
            fecha_archivo = FECHA




            fecha_calculo = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(fecha_archivo, "dd/MM/yyyy"))))))

            D24 = tsn.ExecuteDataSet("Exec sp_prc_archivo_d24_todos_nsf '" & Format(fecha_calculo, "yyyyMMdd") & "'")

        Else

            '' '' '' '' '' ''            'Archivo D24 Diario

            fecha_archivo = Format(CDate(Date.Now), "yyyy/MM/dd")

            fecha_calculo = Format(CDate(fecha_archivo), "dd/MM/yyyy")

            FECHA = CDate("31/08/2010")

            D24 = tsn.ExecuteDataSet("Exec sp_prc_archivo_d24_todos_dia '" & Format(FECHA, "yyyyMMdd") & "'")

        End If



        Dim NRO_TOTAL_REG = 0
        Dim MTO_TOT_DOC = 0
        Dim MTO_TOT_DIF_PRE_NO = 0
        Dim MTO_TOT_NET_DOC = 0

        paso = True
        linea = ""

        For i = 0 To D24.Tables(0).Rows.Count - 1



            linea = ""

            If paso Then
                linea = linea & Format(CLng(D24.Tables(0).Rows(i).Item("emp_fac")), "000") & _
                     D24.Tables(0).Rows(i).Item("ide_arc") & _
                     Format(CDate(fecha_archivo), "yyyymm") & _
                     Space(226)
                oSW.WriteLine(linea)
                '   NOM_FACTORING = Adorec!nom_fac
                '  CODIGO_FACTORING = Format(Adorec!emp_fac, "000")
                paso = False
            End If



            'Cantidad de Registros
            NRO_TOTAL_REG = NRO_TOTAL_REG + 1

            'Si Tipo docto es BackFactoring Deudor es Cliente y viceversa
            If D24.Tables(0).Rows(i).Item("id_p_0031") = 41 And D24.Tables(0).Rows(i).Item("opo_res_son") = 1 Then

                'si para BackFactoring va ocurrir los mismo
                If D24.Tables(0).Rows(i).Item("ddr_ide_d24") > 0 Then    ' existe con rut extranjero
                    rut_cli_dig = Format(CLng(D24.Tables(0).Rows(i).Item("dpd_ide_d24")), "000000000") & FC.Vrut(D24.Tables(0).Rows(i).Item("dpd_ide_d24"))
                Else
                    rut_cli_dig = Format(CLng(D24.Tables(0).Rows(i).Item("ddr_ide")), "000000000") & FC.Vrut(D24.Tables(0).Rows(i).Item("ddr_ide"))
                End If

                If D24.Tables(0).Rows(i).Item("pnu_tip_ddr") = 1 Then
                    razon_social_cli = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_ape_ptn"))) & "/" & Trim(VALIDA_CARACTERES(D24.Tables(0).Rows(i).Item("ddr_ape_mtn"))) & "/" & _
                          Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_nom"))) & Space(40), 1, 40), 1, 40)
                Else
                    razon_social_cli = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_rso"))) & Space(40), 1, 40)

                End If

                actividad_eco_cli = Format(D24.Tables(0).Rows(i).Item("pnu_acv_eco"), "00")

                rut_deu_dig = Format(CLng(D24.Tables(0).Rows(i).Item("cli_idc")), "000000000") & _
                FC.Vrut(D24.Tables(0).Rows(i).Item("cli_idc"))

                If D24.Tables(0).Rows(i).Item("tipo_cli") = 1 Then
                    razon_social_deu = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ape_pat_cli"))) & "/" & VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ape_mat_cli"))) & "/" & _
                          Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("nom_cli"))) & Space(40), 1, 40), 1, 40)
                Else
                    razon_social_deu = VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("cli_rso"))) & Space(40)
                End If

                actividad_eco_deu = Format(CLng(D24.Tables(0).Rows(i).Item("PNU_ACV")), "00")
            Else
                'rut deudor
                If D24.Tables(0).Rows(i).Item("ddr_ide_d24") > 0 Then    ' existe con rut extranjero
                    rut_deu_dig = Format(CLng(D24.Tables(0).Rows(i).Item("dpd_ide_d24")), "000000000") & FC.Vrut(D24.Tables(0).Rows(i).Item("dpd_ide_d24"))
                Else
                    rut_deu_dig = Format(CLng(D24.Tables(0).Rows(i).Item("ddr_ide")), "000000000") & FC.Vrut(D24.Tables(0).Rows(i).Item("ddr_ide"))
                End If

                If D24.Tables(0).Rows(i).Item("pnu_tip_ddr") = 1 Then
                    razon_social_deu = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_ape_ptn"))) & "/" & VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_ape_mtn"))) & "/" & _
                          Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_nom"))) & Space(40), 1, 40), 1, 40)
                Else

                    razon_social_deu = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ddr_rso"))) & Space(40), 1, 40)
                End If

                actividad_eco_deu = Format(CLng(D24.Tables(0).Rows(i).Item("pnu_acv_eco")), "00")

                rut_cli_dig = Format(CLng(D24.Tables(0).Rows(i).Item("cli_idc")), "000000000") & _
                FC.Vrut(D24.Tables(0).Rows(i).Item("cli_idc"))

                If D24.Tables(0).Rows(i).Item("tipo_cli") = 1 Then
                    razon_social_cli = Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ape_pat_cli"))) & "/" & VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("ape_mat_cli"))) & "/" & _
                          Mid(VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("nom_cli"))) & Space(40), 1, 40), 1, 40)
                Else
                    razon_social_cli = VALIDA_CARACTERES(Trim(D24.Tables(0).Rows(i).Item("cli_rso"))) & Space(40)
                End If

                actividad_eco_cli = Format(CLng(D24.Tables(0).Rows(i).Item("PNU_ACV")), "00")
            End If

            If InStr(actividad_eco_cli, "-") > 0 Or Len(actividad_eco_cli) > 2 Then actividad_eco_cli = "00"
            If InStr(actividad_eco_deu, "-") > 0 Or Len(actividad_eco_deu) > 2 Then actividad_eco_deu = "00"

            If (IsDBNull(D24.Tables(0).Rows(i).Item("ope_gar_son"))) Then
                ope_gar_son = "1" 'Sin Garantia
            Else
                'ope_gar_son = "2" 'Con Garantia
                ope_gar_son = D24.Tables(0).Rows(i).Item("ope_gar_son") 'Con Garantia
            End If

            If IsDBNull(D24.Tables(0).Rows(i).Item("doc_rgo_adi")) Then
                doc_rgo_adi = "2" 'Sin Riesgo
            Else
                doc_rgo_adi = D24.Tables(0).Rows(i).Item("doc_rgo_adi")
            End If



            If InStr(D24.Tables(0).Rows(i).Item("opo_tas_bas"), ".") > 0 Then
                tasa = Mid(D24.Tables(0).Rows(i).Item("opo_tas_bas"), 1, InStr(D24.Tables(0).Rows(i).Item("opo_tas_bas"), ".") - 1) & "," & Mid(D24.Tables(0).Rows(i).Item("opo_tas_bas"), InStr(D24.Tables(0).Rows(i).Item("opo_tas_bas"), ".") + 1)
            Else
                tasa = D24.Tables(0).Rows(i).Item("opo_tas_bas")
            End If

            decimales = Format(CLng(tasa), "000.00")

            largo = Len(Format(CLng(D24.Tables(0).Rows(i).Item("opo_num")), "00000"))

            If (D24.Tables(0).Rows(i).Item("id_p_0031") = 1 Or D24.Tables(0).Rows(i).Item("id_p_0031") = 25 _
            Or D24.Tables(0).Rows(i).Item("id_p_0031") = 31 Or D24.Tables(0).Rows(i).Item("id_p_0031") = 52) Then
                tip_docto = 1
            Else
                If D24.Tables(0).Rows(i).Item("id_p_0031") = 2 Then
                    tip_docto = D24.Tables(0).Rows(i).Item("id_p_0031")
                Else
                    If D24.Tables(0).Rows(i).Item("id_p_0031") = 50 Then
                        tip_docto = 3
                    Else
                        tip_docto = 4
                    End If
                End If
            End If

            If D24.Tables(0).Rows(i).Item("moneda") = 3 Then
                moneda = 360
            Else
                moneda = 30
            End If

            val_des = IIf(CDate(D24.Tables(0).Rows(i).Item("opo_fec_oto")) > CDate("2007/11/08"), 1, 0)

            'Cantidad de Dias
            dif_dias = DateDiff("d", Format(CDate(fecha_calculo), "dd/MM/yyyy"), Format(CDate(D24.Tables(0).Rows(i).Item("doc_fev_ori")), "dd/MM/yyyy")) - val_des

            If dif_dias > 0 Then
                dif_dia_sim_ori = DateDiff("d", Format(D24.Tables(0).Rows(i).Item("opo_fec_oto"), "dd/MM/yyyy"), Format(D24.Tables(0).Rows(i).Item("doc_fev_ori"), "dd/MM/yyyy"))

                dif_dia_ret = CInt(D24.Tables(0).Rows(i).Item("dsi_ctd_dia")) - dif_dia_sim_ori

                If dif_dia_ret < 0 Then dif_dia_ret = 0

                dif_dias = dif_dias + dif_dia_ret
            End If

            'Si Docto. está Vigente
            If dif_dias > 0 Then

                'Tasa Mensual o Anual
                If (IIf(IsDBNull(D24.Tables(0).Rows(i).Item("opn_tas_moa")), "M", D24.Tables(0).Rows(i).Item("opn_tas_moa"))) = "M" Then
                    moneda = 30
                Else
                    moneda = 360
                End If

                tasa_calculada = Val(D24.Tables(0).Rows(i).Item("dif_pre_fav_fac")) / (Val(D24.Tables(0).Rows(i).Item("doc_mto_ant")) * Val(D24.Tables(0).Rows(i).Item("dsi_ctd_dia"))) * (moneda * 100)

                'jmmc
                'TASA_CALCULADA = 0

                If (IIf(IsDBNull(D24.Tables(0).Rows(i).Item("opo_lin")), "S", D24.Tables(0).Rows(i).Item("opo_lin"))) = "N" Then
                    lineal = "N"
                Else
                    lineal = "S"
                End If

                dif_precio = ((D24.Tables(0).Rows(i).Item("dif_pre_fav_fac") / D24.Tables(0).Rows(i).Item("dsi_ctd_dia")) * dif_dias) * 1


            Else
                dif_precio = "0"
            End If

            '' '' '' '' '' ''            MTO_PAGOS = IIf(D24.Tables(0).Rows(i).Item("pag_par_obn < 0, 0, D24.Tables(0).Rows(i).Item("pag_par_obn)

            '' '' '' '' '' ''            VAL_ACT_NETO = Format(MATH.ROUND(D24.Tables(0).Rows(i).Item("val_nom_doc), "###########0") - Format(MATH.ROUND(D24.Tables(0).Rows(i).Item("dif_pre_no_fin), "###########0") - Format(MATH.ROUND(DIF_PRECIO), "########0") - Format(MATH.ROUND(MTO_PAGOS), "###########0")

            '' '' '' '' '' ''            If VAL_ACT_NETO < 0 Then VAL_ACT_NETO = 0

            '' '' '' '' '' ''            SqlAux = "exec sp_ad_retorna_datos_prov '" & D24.Tables(0).Rows(i).Item("cli_idc & "','" & D24.Tables(0).Rows(i).Item("ddr_ide & "'," & D24.Tables(0).Rows(i).Item("pnu_tip_doc & "," & D24.Tables(0).Rows(i).Item("OPE_NUM & "," & D24.Tables(0).Rows(i).Item("dsi_num & ",0,'" & Trim(Periodo) & "'"

            '' '' '' '' '' ''            If EJECUTA_COMANDO_AUX(SqlAux, RcsetAux) > 0 Then
            '' '' '' '' '' ''                Exit Function
            '' '' '' '' '' ''            End If

            '' '' '' '' '' ''            Porcentaje = 0
            '' '' '' '' '' ''            If Not RcsetAux.EOF Then
            '' '' '' '' '' ''                MontoProvision = RcsetAux.rdoColumns(0)
            '' '' '' '' '' ''                Porcentaje = (MontoProvision / D24.Tables(0).Rows(i).Item("doc_mto_cli) * 100
            '' '' '' '' '' ''            End If
            '' '' '' '' '' ''            RcsetAux.Close()

            '' '' '' '' '' ''            If Porcentaje > CCur("99,9999") Then
            '' '' '' '' '' ''                Porcentaje = CCur("99,9999")
            '' '' '' '' '' ''            End If

            '' '' '' '' '' ''            'Porcentaje provisón
            '' '' '' '' '' ''            PORCENTAJE_PROVISION = Porcentaje

            '' '' '' '' '' ''            If InStr(Porcentaje, ",") > 0 Then
            '' '' '' '' '' ''                PORCENTAJE_PROVISION = Format(Mid(PORCENTAJE_PROVISION, 1, InStr(PORCENTAJE_PROVISION, ",") - 1), "00") & Format(Mid(PORCENTAJE_PROVISION, InStr(PORCENTAJE_PROVISION, ",") + 1), "0000")
            '' '' '' '' '' ''            Else
            '' '' '' '' '' ''                PORCENTAJE_PROVISION = Format(PORCENTAJE_PROVISION, "00") & "0000"
            '' '' '' '' '' ''            End If

            Dim PORCENTAJE_PROVISION As String = "050000"

            linea = rut_deu_dig & Mid(razon_social_deu, 1, 40) & actividad_eco_deu & _
                    rut_cli_dig & _
                    Mid(razon_social_cli, 1, 40) & _
                    actividad_eco_cli & _
                    Format(CLng(D24.Tables(0).Rows(i).Item("opo_num")), "00000") & Space(14 - largo) & _
                    Format(D24.Tables(0).Rows(i).Item("opo_fec_oto"), "yyyyMMdd") & _
                    ope_gar_son & _
                    Format(CLng(D24.Tables(0).Rows(i).Item("dsi_num")), "00000000") & Format(CLng(D24.Tables(0).Rows(i).Item("doc_flj_num")), "00000") & Space(1) & _
                    Format(CLng(tip_docto), "00") & _
                    Format(CLng(D24.Tables(0).Rows(i).Item("pnu_mon")), "000") & _
                    Format(D24.Tables(0).Rows(i).Item("dsi_fev_rea"), "yyyyMMdd") & _
                    Format(CDbl(D24.Tables(0).Rows(i).Item("val_nom_doc")), "00000000000000") & _
                    Format(CDbl(D24.Tables(0).Rows(i).Item("dif_pre_no_fin")), "00000000000000") & _
                    Format(CDbl(Math.Round(dif_precio)), "00000000000000") & _
                    Format(IIf(D24.Tables(0).Rows(i).Item("pag_par_obn") < 0, 0, CDbl(D24.Tables(0).Rows(i).Item("pag_par_obn"))), "00000000000000") & _
                    Format(CDbl(Math.Round(val_act_neto)), "00000000000000") & _
                    Format(Fix(CDec(tasa)), "000") & Mid(decimales, InStr(decimales, ",") + 1, 3) & _
                    PORCENTAJE_PROVISION

            '' '' '' '' '' ''            MTO_TOT_DOC = MTO_TOT_DOC + MATH.ROUND(D24.Tables(0).Rows(i).Item("val_nom_doc) 'OK
            '' '' '' '' '' ''            MTO_TOT_DIF_PRE_NO = MTO_TOT_DIF_PRE_NO + MATH.ROUND(DIF_PRECIO) 'OK
            '' '' '' '' '' ''            MTO_TOT_NET_DOC = MTO_TOT_NET_DOC + MATH.ROUND(VAL_ACT_NETO) 'OK

            'tipo docto backfactoring
            If D24.Tables(0).Rows(i).Item("id_p_0031") = 41 Then
                linea = linea & "2"
            Else
                'tipo docto. renegociación
                If D24.Tables(0).Rows(i).Item("id_p_0031") = 38 Then
                    linea = linea & "1"
                Else
                    If D24.Tables(0).Rows(i).Item("doc_num_ren") > 0 Then
                        linea = linea & "1"
                    Else
                        linea = linea & "2"
                    End If
                End If
            End If

            If D24.Tables(0).Rows(i).Item("opo_res_son") = 1 Then
                linea = linea & "1"
            Else
                linea = linea & "2"
            End If

            If D24.Tables(0).Rows(i).Item("pnu_est_ver") > 0 Then
                linea = linea & "1"
            Else
                linea = linea & "2"
            End If

            If D24.Tables(0).Rows(i).Item("doc_ntf") = "s" Then
                linea = linea & "1"
            Else
                linea = linea & "2"
            End If

            If D24.Tables(0).Rows(i).Item("doc_cob_doi") = 1 Then
                linea = linea & "1"
            Else
                linea = linea & "2"
            End If

            '' '' '' '' '' ''          Print #1, LINEA

            '' '' '' '' '' ''            can_lin = can_lin + 1

            '' '' '' '' '' ''            Adorec.MoveNext()
            oSW.WriteLine(linea)
        Next

        oSW.Close()
        Return True


    End Function
    Public Function Archivo_sinacofi1(ByVal pathStr As String) As Boolean

        Dim DOC_MTO_CAS As String
        Dim LINEA As String
        Dim Sinacofi As DataSet
        Dim oSW As New StreamWriter(pathStr)
        Dim cli_idc As String
        Dim cli_rso As String
        Sinacofi = tsn.ExecuteDataSet("exec sp_arc_sinacofi1")

        LINEA = ""
        For i = 0 To Sinacofi.Tables(0).Rows.Count - 1
            If LINEA = "" Then
                LINEA = Format(Sinacofi.Tables(0).Rows(i).Item("rut_emp"), "000000000") & FC.Vrut(Format(Sinacofi.Tables(0).Rows(i).Item("rut_emp"), "0")) & _
                Format(Sinacofi.Tables(0).Rows(i).Item("num_tot"), "000000") & _
                Format(Sinacofi.Tables(0).Rows(i)("fec_env"), "yyyyMMdd") & _
                Format(Sinacofi.Tables(0).Rows(i)("sum_cob_tot"), "00000000000000") & _
                Format(Sinacofi.Tables(0).Rows(i)("sum_cas_tot"), "00000000000000") & _
                Space(52)
                oSW.WriteLine(LINEA)
                LINEA = ""
            End If

            If InStr(Sinacofi.Tables(0).Rows(i)("DOC_MTO_CAS"), "-") > 0 Then
                DOC_MTO_CAS = Format(Sinacofi.Tables(0).Rows(i)("DOC_MTO_CAS"), "0000000000000")
            Else
                DOC_MTO_CAS = Format(Sinacofi.Tables(0).Rows(i)("DOC_MTO_CAS"), "00000000000000")
            End If

            If i <= Sinacofi.Tables(0).Rows.Count - 1 Then
                'Variable Para control cambio Cliente 
                cli_idc = Sinacofi.Tables(0).Rows(i).Item("cli_idc")
                cli_rso = Sinacofi.Tables(0).Rows(i).Item("rso_cli")

                LINEA = Format(Sinacofi.Tables(0).Rows(i).Item("cli_idc")) & FC.Vrut(Format(Sinacofi.Tables(0).Rows(i).Item("cli_idc"), "0")) & _
                        Format(Sinacofi.Tables(0).Rows(i).Item("rso_cli")) & _
                        Format(Sinacofi.Tables(0).Rows(i).Item("doc_mto_cob"), "00000000000000") & _
                        Format(Sinacofi.Tables(0).Rows(i).Item("fec_dem"), "yyyyMMdd") & _
                        DOC_MTO_CAS & _
                        Format(Sinacofi.Tables(0).Rows(i).Item("fec_cas"), "yyyyMMdd")
                Space(426)
                oSW.WriteLine(LINEA)
            End If

        Next
        oSW.Close()
        Return True
    End Function
    Public Function Archivo_ART8485(ByVal pathStr As String) As Boolean
        Dim RCD As DataSet
        Dim ADO As String
        Dim SQL As String
        Dim RUT_CLIENTE As String
        Dim NRO_CONTRAT As String
        Dim CODIGO_PROD As String
        Dim COD_SUB_PRO As String
        Dim APL_ORI_CON As String
        Dim DIVISA_OPER As String
        Dim IMP_CTB_PES As String
        Dim IMP_CTB_OPE As String
        Dim DIG_CLI As String
        Dim POS As String
        Dim Fec_Actual As String
        Dim DECIMA As String
        Dim IMP_CTB_PES_FMT As String
        Dim IMP_CTB_OPE_FMT As String
        Dim oSW As New StreamWriter(pathStr)
        Dim LINEA As String

        ADO = Date.Now.ToShortDateString
        Fec_Actual = ADO

        SQL = "EXEC sp_ad_archivo_art_84_85 "

        RCD = tsn.ExecuteDataSet("EXEC sp_ad_archivo_art_84_85 ")

        For i = 0 To RCD.Tables(0).Rows.Count - 1
            RUT_CLIENTE = ""
            RUT_CLIENTE = Format(RCD.Tables(0).Rows(i).Item("cli_idc"), "00000000000")
            DIG_CLI = FC.Vrut(RUT_CLIENTE)
            RUT_CLIENTE = Format(RUT_CLIENTE, "0000000000") & DIG_CLI
            NRO_CONTRAT = Format(RCD.Tables(0).Rows(i).Item("id_ope"), "0000000000000000")
            NRO_CONTRAT = 88 & NRO_CONTRAT
            CODIGO_PROD = 26
            COD_SUB_PRO = "FF00"
            APL_ORI_CON = "FF"
            DIVISA_OPER = RCD.Tables(0).Rows(i).Item("pnu_atr_008")
            IMP_CTB_PES = RCD.Tables(0).Rows(i).Item("Mto_doc")
            IMP_CTB_PES_FMT = Format((CStr(Fix(IMP_CTB_PES))), "00000000000000") & "0000"
            IMP_CTB_OPE = RCD.Tables(0).Rows(i).Item("MTO_ANT")
            'Monto
            If RCD.Tables(0).Rows(i).Item("id_p_0023") = 1 Then
                IMP_CTB_OPE_FMT = Format((CStr(Fix(IMP_CTB_OPE))), "00000000000000") & "0000"
            Else
                POS = InStr(IMP_CTB_OPE, ",")
                If POS > 0 Then
                    DECIMA = Format(Mid(IMP_CTB_OPE, POS + 1, 4), "0000")
                    IMP_CTB_OPE_FMT = Format((CStr(Fix(IMP_CTB_OPE))), "00000000000000") & CStr(DECIMA)
                Else
                    IMP_CTB_OPE_FMT = Format((CStr(Fix(IMP_CTB_OPE))), "00000000000000") & "0000"
                End If
            End If

            If i <= RCD.Tables(0).Rows.Count - 1 Then
                LINEA = RUT_CLIENTE & _
                        NRO_CONTRAT & _
                        CODIGO_PROD & _
                        COD_SUB_PRO & _
                        APL_ORI_CON & _
                        DIVISA_OPER & _
                        IMP_CTB_PES_FMT & _
                        IMP_CTB_OPE_FMT
                oSW.WriteLine(LINEA)
            End If

        Next
        oSW.Close()
        Return True
        Exit Function
    End Function
    Public Function Archivo_auditoria(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim Auditoria As DataSet
        Dim SQL As String
        Dim LINEA As String
        Dim CONT1 As Long
        Dim CONT2 As Long
        Dim DECIMALES1 As String
        Dim DECIMALES2 As String
        Dim DECIMALES3 As String
        Dim DECIMALES4 As String
        Dim DECIMALES5 As String
        Dim DOC_INT_DVG As String
        Dim PNU_ACV As String
        Dim PTO_SPR As String
        Dim DOC_MTO As String
        Dim MTO_ANT As String
        Dim SAL_CLI As String
        Dim TASA As String
        Dim PUNTOS As Object
        Dim SPREAD As Object
        Dim TASA_BASE As Object

        Dim SAL_CLI_MON As Object
        Dim DECIMALES6 As String
        Dim FECHA_CALCULO As Object
        Dim FECHA_ARCHIVO As Object
        Dim FechaDePago As String
        Dim SPR_EAD As String
        Dim TAS_BAS As String



        'Meses Anteriores
        If TIPO = "M" Then
            Auditoria = tsn.ExecuteDataSet("Exec sp_arc_auditoria_fin_mes_todos '" & Format(FECHA, "yyyyMMdd") & "'")
        Else
            Auditoria = tsn.ExecuteDataSet("Exec sp_arc_auditoria_todos ")
        End If

        Dim oSW As New StreamWriter(path)

        For i = 0 To Auditoria.Tables(0).Rows.Count - 1

            If i = 0 Then
                LINEA = "RUT CLIENTE;DV;RAZON SOCIAL CLIENTE;RUT DEUDOR;DV;RAZON SOCIAL DEUDOR;COD SUC;SUCURSAL;COD EJEC;EJECUTIVO;TIP DOCTO;NRODOCTO;CUOTA;MTO DOCTO;MTO ANT;SDO CLI;SDO CLI MON;TASA;TASA BASE;SPREAD;PUNTOS;FEC OTO;FECHA PAGO;INTERES DVG;CON NOT;CON COBR;MTO APROB;MTO OCU;FECHA VIG HASTA;COD COBR;DESC COD COBR;MONEDA;ACT ECO;RES;FOGAPE;OPE LINEAL;FECH VCTO ORI;COD BANCA;DESC BANCA;FECH VCTO REAL"
                oSW.WriteLine(LINEA)
            End If


            LINEA = ""
            '     'Saldo Cliente ne Moneda Original
            If Auditoria.Tables(0).Rows(i).Item("pnu_mon") <> 1 Then
                If InStr(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"), ".") > 0 Then
                    SAL_CLI_MON = Mid(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"), 1, InStr(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"), ".") - 1) & "," & Mid(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"), InStr(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"), ".") + 1)
                Else
                    SAL_CLI_MON = Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon")
                End If
            Else
                SAL_CLI_MON = Math.Round(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli_mon"))
            End If

            'tasa base
            If InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_bas"), ".") > 0 Then
                TASA_BASE = Mid(Auditoria.Tables(0).Rows(i).Item("opo_tas_bas"), 1, InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_bas"), ".") - 1) & "," & Mid(Auditoria.Tables(0).Rows(i).Item("opo_tas_bas"), InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_bas"), ".") + 1)
            Else
                TASA_BASE = Auditoria.Tables(0).Rows(i).Item("opo_tas_bas")
            End If
            TAS_BAS = Format(TASA_BASE * 100, "000")

            'spread
            If InStr(Auditoria.Tables(0).Rows(i).Item("opo_spr_ead"), ".") > 0 Then
                SPREAD = Mid(Auditoria.Tables(0).Rows(i).Item("opo_spr_ead"), 1, InStr(Auditoria.Tables(0).Rows(i).Item("opo_spr_ead"), ".") - 1) & "," & Mid(Auditoria.Tables(0).Rows(i).Item("opo_spr_ead"), InStr(Auditoria.Tables(0).Rows(i).Item("opo_spr_ead"), ".") + 1)
            Else
                SPREAD = Auditoria.Tables(0).Rows(i).Item("opo_spr_ead")
            End If
            SPR_EAD = Format(SPREAD * 100, "000")

            'Puntos
            If InStr(Auditoria.Tables(0).Rows(i).Item("opo_pto_spr"), ".") > 0 Then
                PUNTOS = Mid(Auditoria.Tables(0).Rows(i).Item("opo_pto_spr"), 1, InStr(Auditoria.Tables(0).Rows(i).Item("opo_pto_spr"), ".") - 1) & "," & Mid(Auditoria.Tables(0).Rows(i).Item("opo_pto_spr"), InStr(Auditoria.Tables(0).Rows(i).Item("opo_pto_spr"), ".") + 1)
            Else
                PUNTOS = Auditoria.Tables(0).Rows(i).Item("opo_pto_spr")
            End If
            PTO_SPR = Format(PUNTOS * 100, "000")

            'Tasa negocio
            If InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_neg"), ".") > 0 Then
                TASA = Mid(Auditoria.Tables(0).Rows(i).Item("opo_tas_neg"), 1, InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_neg"), ".") - 1) & "," & Mid(Auditoria.Tables(0).Rows(i).Item("opo_tas_neg"), InStr(Auditoria.Tables(0).Rows(i).Item("opo_tas_neg"), ".") + 1)
            Else
                TASA = Auditoria.Tables(0).Rows(i).Item("opo_tas_neg")
            End If

            DECIMALES1 = Format(CDec(TASA), "00.0000")

            DECIMALES3 = "00" 'Format(Auditoria.Tables(0).Rows(i).Item("DOC_MTO"), "0000000000.00")
            DECIMALES4 = "00" 'Format(Auditoria.Tables(0).Rows(i).Item("doc_mto_ant"), "0000000000.00")
            DECIMALES5 = "00" '"Format(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli"), "0000000000.00")
            DECIMALES6 = Format(CDbl(SAL_CLI_MON), "0000000000.00")


            DOC_INT_DVG = IIf(InStr(Auditoria.Tables(0).Rows(i).Item("DOC_INT_DVG"), "-") > 0, Format(Auditoria.Tables(0).Rows(i).Item("DOC_INT_DVG"), "0000000"), Format(Auditoria.Tables(0).Rows(i).Item("DOC_INT_DVG"), "00000000"))
            PNU_ACV = IIf(InStr(Auditoria.Tables(0).Rows(i).Item("PNU_ACV"), "-") > 0, Format(Auditoria.Tables(0).Rows(i).Item("PNU_ACV"), "0000"), Format(Auditoria.Tables(0).Rows(i).Item("PNU_ACV"), "00000"))



            If IIf(IsDBNull(Auditoria.Tables(0).Rows(i).Item("fec_pag")), "19000101", Auditoria.Tables(0).Rows(i).Item("fec_pag")) = "19000101" Then
                FechaDePago = Auditoria.Tables(0).Rows(i).Item("doc_fev_rea")
            Else
                If Auditoria.Tables(0).Rows(i).Item("fec_pag") = "1900/01/01" Then
                    FechaDePago = Auditoria.Tables(0).Rows(i).Item("doc_fev_rea")
                Else
                    FechaDePago = Auditoria.Tables(0).Rows(i).Item("fec_pag")
                End If
            End If


            LINEA = Format(CLng(Auditoria.Tables(0).Rows(i).Item("cli_idc")), "0000000000") & ";" & _
                    FC.Vrut(Format(CLng(Auditoria.Tables(0).Rows(i).Item("cli_idc")), "########0")) & ";" & _
                    Mid(Auditoria.Tables(0).Rows(i).Item("cli_rso") + Space(30), 1, 30) & ";" & _
                    Format(CLng(Auditoria.Tables(0).Rows(i).Item("ddr_ide")), "0000000000") & ";" & _
                    FC.Vrut(Format(CLng(Auditoria.Tables(0).Rows(i).Item("ddr_ide")), "########0")) & ";" & _
                    Mid(Auditoria.Tables(0).Rows(i).Item("ddr_rso") + Space(30), 1, 30) & ";" & _
                    Format(CLng(Auditoria.Tables(0).Rows(i).Item("suc_cod_ftg")), "00000") & ";" & _
                    Mid(Auditoria.Tables(0).Rows(i).Item("suc_nom") + Space(30), 1, 30) & ";" & _
                    Format(CLng(Auditoria.Tables(0).Rows(i).Item("eje_cod_eje")), "00000") & ";" & _
                    Mid(Auditoria.Tables(0).Rows(i).Item("eje_nom") + Space(30), 1, 30) & ";" & _
                    Format(Auditoria.Tables(0).Rows(i).Item("pnu_tip_doc"), "00") & ";" & _
                    Format(CLng(Auditoria.Tables(0).Rows(i).Item("doc_num")), "00000000") & ";" & _
                    Format(CLng(Auditoria.Tables(0).Rows(i).Item("doc_flj_num")), "00000") & ";" & _
                    Format(Fix(Math.Round(CDbl(Auditoria.Tables(0).Rows(i).Item("DOC_MTO")))), "000000000") & DECIMALES3 & ";" & _
                    Format(Fix(Math.Round(CDbl(Auditoria.Tables(0).Rows(i).Item("doc_mto_ant")))), "000000000") & DECIMALES4 & ";" & _
                    Format(Fix(Math.Round(CDbl(Auditoria.Tables(0).Rows(i).Item("doc_sdo_cli")))), "000000000") & DECIMALES5 & ";" & _
                    Format(Fix(CDbl(SAL_CLI_MON)), "000000000") & Mid(DECIMALES6, InStr(DECIMALES6, ",") + 1, 3) & ";" & _
                    Format(Fix(CDbl(TASA)), "00") & Mid(DECIMALES1, InStr(DECIMALES1, ",") + 1, 3) & ";" & _
                    TAS_BAS & ";" & SPR_EAD & ";" & PTO_SPR & ";" & _
                    Auditoria.Tables(0).Rows(i).Item("opo_fec_oto") & ";" & _
                    FechaDePago & ";" & _
                    DOC_INT_DVG & ";" & _
                    IIf(Auditoria.Tables(0).Rows(i).Item("doc_ntf") = "N", "SIN NOTIFICACION         ", "CON NOTIFICACION         ") & ";" & _
                    IIf(Auditoria.Tables(0).Rows(i).Item("doc_cbz_son") = "N", "SIN COBRANZA   ", "CON COBRANZA   ") & ";" & _
                    Format(Mid(CDbl(Auditoria.Tables(0).Rows(i).Item("ldc_mto_apb")), 1, 11), "00000000000") & ";"
            LINEA = LINEA & Format(Mid(CDbl(Auditoria.Tables(0).Rows(i).Item("ldc_mto_ocp")), 1, 11), "00000000000") & ";" & _
            Auditoria.Tables(0).Rows(i).Item("ldc_fec_vig_hta") & ";" & _
            Format(CLng(Auditoria.Tables(0).Rows(i).Item("cco_num")), "0000") & ";" & _
            Mid(Auditoria.Tables(0).Rows(i).Item("cco_des") + Space(40), 1, 40) & ";" & _
            Mid(Auditoria.Tables(0).Rows(i).Item("mon_des"), 1, 3) & ";" & _
            PNU_ACV & Space(2) & ";" & _
            IIf(Auditoria.Tables(0).Rows(i).Item("res") = 1, "C", "S") & ";" & _
            Auditoria.Tables(0).Rows(i).Item("fog") & ";" & _
            IIf(Trim(IIf(IsDBNull(Auditoria.Tables(0).Rows(i).Item("opo_lnl")), "S", Auditoria.Tables(0).Rows(i).Item("opo_lnl"))) = "S", "LINEAL", "NO LINEAL") & ";" & _
            Auditoria.Tables(0).Rows(i).Item("doc_fev_ori") & ";" & _
            Format(Auditoria.Tables(0).Rows(i).Item("pal_tip_ban"), "00") & ";" & _
            Auditoria.Tables(0).Rows(i).Item("pal_des") & ";" & _
            Auditoria.Tables(0).Rows(i).Item("doc_fev_rea")


            oSW.WriteLine(LINEA)
        Next
        oSW.Close()

        Return True


    End Function
    Public Function Archivo_Riesgo_Varios(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim riesgo As New DataSet
        Dim RUT_CLI As String
        Dim RUT_DEU As String
        Dim DIG_CLI As String
        Dim DIG_DEU As String
        Dim MON_DCT As String
        Dim TIP_DCT As String
        Dim EST_DCT As String
        Dim SDO_CLI As String
        Dim SDO_DEU As String
        Dim SDO_CLI_FRM As String
        Dim SDO_DEU_FRM As String
        Dim DECIMA As String
        Dim POS As String
        Dim FECHA_CALCULO As Date
        Dim linea As String
        Dim oSW As New StreamWriter(path)
        'archivo meses anteriores
        'If TIPO = "M" Then
        '    'Fecha Archivo AUDITORIA
        '    FECHA_CALCULO = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(FECHA, "dd/mm/yyyy"))))))
        'End If

        'Meses Anteriores
        Try

            If TIPO = "A" Then
                riesgo = tsn.ExecuteDataSet("Exec sp_ad_archivo_riesgo_varios " & 1 & ",'" & Format(FECHA, "yyyyMMdd") & "'")
            Else
                FECHA_CALCULO = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(FECHA, "dd/MM/yyyy"))))))
                riesgo = tsn.ExecuteDataSet("Exec sp_ad_archivo_riesgo_varios " & 2 & ",'" & Format(FECHA_CALCULO, "yyyyMMdd") & "'")
            End If

            If TIPO = "A" Then
                For I = 0 To riesgo.Tables(0).Rows.Count - 1
                    If I = 0 Then
                        linea = ""
                        oSW.WriteLine(linea)
                    End If
                    RUT_CLI = Format(CDbl(riesgo.Tables(0).Rows(I).Item("cli_idc")), "00000000000")
                    DIG_CLI = FC.Vrut(RUT_CLI)
                    RUT_CLI = Format(RUT_CLI, "00000000000") & DIG_CLI

                    RUT_DEU = Format(CDbl(riesgo.Tables(0).Rows(I).Item("deu_ide")), "00000000000")
                    DIG_DEU = FC.Vrut(RUT_DEU)
                    RUT_DEU = Format(RUT_DEU, "00000000000") & DIG_DEU

                    'RESPONS = IIf(IsDBNull(riesgo.Tables(0).Rows(I).Item("opo_res_son")), 0, riesgo.Tables(0).Rows(I).Item("opo_res_son"))
                    'If RESPONS = 1 Then
                    '    RESPONS = "C"
                    'Else
                    '    RESPONS = "S"
                    'End If

                    MON_DCT = riesgo.Tables(0).Rows(I).Item("pnu_atr_008")
                    TIP_DCT = Format(riesgo.Tables(0).Rows(I).Item("id_P_0031"), "00")
                    EST_DCT = riesgo.Tables(0).Rows(I).Item("id_P_0011")
                    If (EST_DCT = 12 Or EST_DCT = 9) Then EST_DCT = 4
                    SDO_CLI = riesgo.Tables(0).Rows(I).Item("doc_sdo_cli")
                    SDO_DEU = riesgo.Tables(0).Rows(I).Item("doc_sdo_ddr")

                    'Monto

                    If riesgo.Tables(0).Rows(I).Item("id_P_0023") = 1 Then
                        SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & "0000"
                        SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & "0000"
                    Else
                        POS = InStr(SDO_CLI, ",")
                        If POS > 0 Then
                            DECIMA = Format(Mid(SDO_CLI, POS + 1, 4), "0000")
                            SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & CStr(DECIMA)
                        Else
                            SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & "0000"
                        End If

                        POS = InStr(SDO_DEU, ",")
                        If POS > 0 Then
                            DECIMA = Format(Mid(SDO_DEU, POS + 1, 4), "0000")
                            SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & CStr(DECIMA)
                        Else
                            SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & "0000"
                        End If
                    End If

                    linea = RUT_CLI & _
                                    RUT_DEU & _
                                    MON_DCT & _
                                    TIP_DCT & _
                                    EST_DCT & _
                                    SDO_CLI_FRM & _
                                    SDO_DEU_FRM
                    oSW.WriteLine(linea)
                Next
                oSW.Close()
                Return True
            Else
                For I = 0 To riesgo.Tables(0).Rows.Count - 1
                    If I = 0 Then
                        linea = ""
                        oSW.WriteLine(linea)
                    End If
                    RUT_CLI = Format(CDbl(riesgo.Tables(0).Rows(I).Item("cli_idc")), "00000000000")
                    DIG_CLI = FC.Vrut(RUT_CLI)
                    RUT_CLI = Format(RUT_CLI, "00000000000") & DIG_CLI

                    RUT_DEU = Format(CDbl(riesgo.Tables(0).Rows(I).Item("deu_ide")), "00000000000")
                    DIG_DEU = FC.Vrut(RUT_DEU)
                    RUT_DEU = Format(RUT_DEU, "00000000000") & DIG_DEU

                    MON_DCT = riesgo.Tables(0).Rows(I).Item("pnu_atr_008")
                    TIP_DCT = Format(riesgo.Tables(0).Rows(I).Item("id_P_0031"), "00")
                    'EST_DCT = riesgo.Tables(0).Rows(I).Item("id_P_0011")
                    'If (EST_DCT = 12 Or EST_DCT = 9) Then EST_DCT = 4
                    SDO_CLI = riesgo.Tables(0).Rows(I).Item("dom_sdo_cli")
                    SDO_DEU = riesgo.Tables(0).Rows(I).Item("dom_sdo_ddr")

                    'Monto

                    If riesgo.Tables(0).Rows(I).Item("id_P_0023") = 1 Then
                        SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & "0000"
                        SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & "0000"
                    Else
                        POS = InStr(SDO_CLI, ",")
                        If POS > 0 Then
                            DECIMA = Format(Mid(SDO_CLI, POS + 1, 4), "0000")
                            SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & CStr(DECIMA)
                        Else
                            SDO_CLI_FRM = Format((CDbl(Fix(SDO_CLI))), "00000000000") & "0000"
                        End If

                        POS = InStr(SDO_DEU, ",")
                        If POS > 0 Then
                            DECIMA = Format(Mid(SDO_DEU, POS + 1, 4), "0000")
                            SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & CStr(DECIMA)
                        Else
                            SDO_DEU_FRM = Format((CDbl(Fix(SDO_DEU))), "00000000000") & "0000"
                        End If
                    End If

                    linea = RUT_CLI & _
                                    RUT_DEU & _
                                    MON_DCT & _
                                    TIP_DCT & _
                                    EST_DCT & _
                                    SDO_CLI_FRM & _
                                    SDO_DEU_FRM
                    oSW.WriteLine(linea)
                Next
                oSW.Close()
                Return True
            End If

        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "Archivos P14-P15-P16"

    Public Function Archivo_P14(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim P14 As New DataSet
        Dim Fec_Actual As String
        Dim ID_PRODUCTO As String
        Dim LOCALIDAD As String
        Dim moneda As String
        Dim NRO_COL_VIG As String
        Dim NRO_COL_MOR As String
        Dim NRO_COL_VEN As String
        Dim MTO_COL_VIG As String
        Dim MTO_COL_MOR As String
        Dim MTO_COL_VEN As String
        Dim DIA_GEN As String
        Dim MES_GEN As String
        Dim AGE_GEN As String
        Dim NRO_REG As String
        Dim NRO_EMP As String

        Dim oSW As New StreamWriter(path)
        Dim Linea As String


        NRO_EMP = cg.SistemaDevuelve().sis_emp_fac

        If TIPO = "M" Then


            DIA_GEN = Mid(FECHA, 1, 2)
            MES_GEN = Mid(FECHA, 4, 2)
            AGE_GEN = Mid(FECHA, 7, 4)

            ' FECHA_CALCULO = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(FECHA, "dd/MM/yyyy"))))))

            P14 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p14_mes_nsf '" & Format(FECHA, "yyyyMMdd") & "'")

        End If

        If TIPO = "D" Then


            Fec_Actual = Format(Date.Now, "ddmmyyyyhh")
            MES_GEN = Format(Date.Now, "MMM")


            DIA_GEN = Mid(Fec_Actual, 1, 2)
            MES_GEN = Mid(Fec_Actual, 3, 2)
            AGE_GEN = Mid(Fec_Actual, 5, 4)


            P14 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p14_dia_nsf ")

        End If



        For i = 0 To P14.Tables(0).Rows.Count - 1

            '   Open Trim(CMD_INTERFAZ.FileName) For Output As #1
            NRO_REG = Format(CLng(P14.Tables(0).Rows(i).Item("NRO_REG")), "00000000")
            If i = 0 Then
                'Print #1, NRO_EMP & "" & _
                Linea = "P14" & "" & _
                    DIA_GEN & "" & _
                    MES_GEN & "" & _
                    AGE_GEN & "" & _
                    NRO_REG
                oSW.WriteLine(Linea)


            End If

            Linea = ""

            ID_PRODUCTO = "180"
            'LOCALIDAD = "133202"
            LOCALIDAD = "138217"
            moneda = Trim(P14.Tables(0).Rows(i).Item("moneda"))
            NRO_COL_VIG = Format(CLng(P14.Tables(0).Rows(i).Item("NRO_VIG")), "000000")
            NRO_COL_MOR = Format(CLng(P14.Tables(0).Rows(i).Item("NRO_MOR")), "000000")
            NRO_COL_VEN = Format(CLng(P14.Tables(0).Rows(i).Item("NRO_VEN")), "000000")
            MTO_COL_VIG = Format(CDbl(P14.Tables(0).Rows(i).Item("MTO_VIG")), "00000000000000")
            MTO_COL_MOR = Format(CDbl(P14.Tables(0).Rows(i).Item("MTO_MOR")), "00000000000000")
            MTO_COL_VEN = Format(CDbl(P14.Tables(0).Rows(i).Item("MTO_VEN")), "00000000000000")

            Linea = ID_PRODUCTO & "" & _
                   LOCALIDAD & "" & _
                   moneda & "" & _
                   NRO_COL_VIG & "" & _
                   NRO_COL_MOR & "" & _
                   NRO_COL_VEN & "" & _
                   MTO_COL_VIG & "" & _
                   MTO_COL_MOR & "" & _
                   MTO_COL_VEN & ""

            oSW.WriteLine(Linea)

        Next
        oSW.Close()
        Return True

    End Function

    Public Function Archivo_P15(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim oSW As New StreamWriter(path)


        Dim P15 As New DataSet
        Dim Fec_Actual As String
        Dim ID_PRODUCTO As String
        Dim REGION As String
        Dim moneda As String
        Dim ACT_ECO As String
        Dim SALDO_MES As String
        Dim FECHA_CALCULO As Date
        Dim FILLER As String
        Dim DIA_GEN As String
        Dim MES_GEN As String
        Dim AGE_GEN As String
        Dim NRO_REG As String
        Dim NRO_EMP As Integer
        Dim linea As String


        Try



            NRO_EMP = cg.SistemaDevuelve().sis_emp_fac

            If TIPO = "M" Then


                DIA_GEN = Mid(FECHA, 1, 2)
                MES_GEN = Mid(FECHA, 4, 2)
                AGE_GEN = Mid(FECHA, 7, 4)

                FECHA_CALCULO = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(FECHA, "dd/MM/yyyy"))))))
                P15 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p15_mes_nsf '" & Format(FECHA_CALCULO, "yyyyMMdd") & "'")
            End If

            If TIPO = "D" Then


                Fec_Actual = Format(Date.Now, "ddMMyyyyhh")
                MES_GEN = Format(Date.Now, "MMM")


                DIA_GEN = Mid(Fec_Actual, 1, 2)
                MES_GEN = Mid(Fec_Actual, 3, 2)
                AGE_GEN = Mid(Fec_Actual, 5, 4)



                P15 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p15_dia_nsf ")

            End If





            For i = 0 To P15.Tables(0).Rows.Count - 1


                NRO_REG = Format(P15.Tables(0).Rows(i).Item("NRO_REG"), "00000000")

                If i = 0 Then
                    linea = NRO_EMP & "" & _
                     "P15" & "" & _
                     AGE_GEN & "" & _
                     MES_GEN
                    oSW.WriteLine(linea)
                    linea = ""

                End If




                ID_PRODUCTO = "180"
                REGION = Format(Trim(CInt(P15.Tables(0).Rows(i).Item("REGION"))), "00")
                moneda = Trim(P15.Tables(0).Rows(i).Item("Mon"))
                ACT_ECO = Format(CLng(P15.Tables(0).Rows(i).Item("gec_num")), "000")
                SALDO_MES = Format(CDbl(P15.Tables(0).Rows(i).Item("sdo_fin_mes")), "00000000000000")
                FILLER = "0"

                linea = ID_PRODUCTO & "" & _
                       REGION & "" & _
                       moneda & "" & _
                       ACT_ECO & "" & _
                       SALDO_MES & "" & _
                       FILLER
                oSW.WriteLine(linea)

            Next
            oSW.Close()

            If P15.Tables(0).Rows.Count > 0 Then
                Return True
            End If



        Catch ex As Exception
            oSW.Close()
            Return False
        End Try



    End Function

    Public Function Archivo_P16(ByVal path As String, ByVal TIPO As String, ByVal FECHA As DateTime) As Boolean

        Dim p16 As New DataSet
        Dim Fec_Actual As String
        Dim ID_PRODUCTO As String
        Dim REGION As String
        Dim moneda As String
        Dim ACT_ECO As String
        Dim SALDO_MES As String
        Dim FECHA_CALCULO As Date
        Dim DIA_GEN As String
        Dim MES_GEN As String
        Dim AGE_GEN As String
        Dim NRO_REG As String
        Dim NRO_EMP As Integer

        Dim linea As String

        Dim oSW As New StreamWriter(path)

        Try

            NRO_EMP = cg.SistemaDevuelve().sis_emp_fac


            If TIPO = "M" Then


                DIA_GEN = Mid(FECHA, 1, 2)
                MES_GEN = Mid(FECHA, 4, 2)
                AGE_GEN = Mid(FECHA, 7, 4)

                FECHA_CALCULO = CDate(DateAdd("d", -1, CDate(DateAdd("m", 1, CDate(Format(FECHA, "dd/MM/yyyy"))))))
                p16 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p16_mes_nsf '" & Format(FECHA_CALCULO, "yyyyMMdd") & "'")

            ElseIf TIPO = "D" Then


                Fec_Actual = Format(Date.Now, "ddmmyyyyhh")
                MES_GEN = Format(Date.Now, "MMM")

                DIA_GEN = Mid(Fec_Actual, 1, 2)
                MES_GEN = Mid(Fec_Actual, 3, 2)
                AGE_GEN = Mid(Fec_Actual, 5, 4)


                p16 = tsn.ExecuteDataSet("EXEC sp_prc_archivo_p16_dia_nsf ")

            End If


            For i = 0 To p16.Tables(0).Rows.Count - 1



                NRO_REG = Format(p16.Tables(0).Rows(i).Item("NRO_REG"), "00000000")

                If i = 0 Then
                    linea = NRO_EMP & "" & _
                    "P16" & "" & _
                    AGE_GEN & "" & _
                    MES_GEN

                    oSW.WriteLine(linea)
                    linea = ""
                End If


                ID_PRODUCTO = "180"
                REGION = Format(Trim(CInt(p16.Tables(0).Rows(i).Item("REGION"))), "00")
                moneda = Trim(p16.Tables(0).Rows(i).Item("Mon"))
                ACT_ECO = Format(CLng(p16.Tables(0).Rows(i).Item("PNU_ACV")), "00")
                SALDO_MES = Format(CDbl(p16.Tables(0).Rows(i).Item("sdo_fin_mes")), "00000000000000")


                linea = ID_PRODUCTO & "" & _
                       REGION & "" & _
                       moneda & "" & _
                       ACT_ECO & "" & _
                       SALDO_MES

                oSW.WriteLine(linea)

            Next
            oSW.Close()

            If p16.Tables(0).Rows.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            oSW.Close()
            Return False
        End Try

    End Function

    Public Function Archivo_Historico(ByVal mes As Int16, ByVal año As Int16) As DataSet

        Dim dt As New DataSet

        Try

            dt = tsn.ExecuteDataSet("EXEC sp_prc_archivo_historico_operaciones_del_mes '" & mes.ToString("00") & "', '" & año.ToString("0000") & "'")

            Return dt

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GuardaPDF(ByVal id_eva As Integer, ByVal Bit As Byte()) As Boolean

        tsn.ExecuteNonQuery("Update eva set eva_pdf = @@imagen where id_eva=" & id_eva, Bit)

    End Function

    Public Function GuardaNegPDF(ByVal id_opn As Integer, ByVal Bit As Byte()) As Boolean

        tsn.ExecuteNonQuery("Update opn set opn_pdf = @@imagen where id_opn=" & id_opn, Bit)

    End Function

    Public Function DespliegaArchivoNegPDF(ByVal id_opn As Integer) As Byte()
        Dim P19 As New DataSet
        Dim aBytDocumento() As Byte = Nothing


        P19 = tsn.ExecuteDataSet("select isnull(opn_pdf,'') as opn_pdf  from opn where id_opn=" & id_opn)

        If P19.Tables(0).Rows.Count > 0 Then
            aBytDocumento = CType(P19.Tables(0).Rows(0).Item("opn_pdf"), Byte())
        End If


        Return aBytDocumento


    End Function

    Public Function DespliegaArchivoPDF(ByVal id_eva As Integer) As Byte()
        Dim P20 As New DataSet
        Dim aBytDocumento() As Byte = Nothing


        P20 = tsn.ExecuteDataSet("select isnull(eva_pdf,'') as eva_pdf  from eva where id_eva=" & id_eva)

        If P20.Tables(0).Rows.Count > 0 Then
            aBytDocumento = CType(P20.Tables(0).Rows(0).Item("eva_pdf"), Byte())
        End If
        Return aBytDocumento

    End Function

#End Region

#Region "Actas"

    Public Function GuardaActa(ByVal id As Integer, ByVal desc As String, ByVal Bit As Byte()) As Boolean
        tsn.ExecuteNonQuery("Insert into ACT_IMG Values(" & id & ", '" & desc & "', @@imagen)", Bit)
    End Function

    Public Function EliminaActa(ByVal id As Integer) As Boolean
        tsn.ExecuteNonQuery("Delete From ACT_IMG Where act_img_id= " & id)
    End Function

    Public Function DevuelveActa(ByVal id As Integer) As Byte()


        Dim dt As New DataClsFactoringDataContext
        dt.CommandTimeout = 240
        Dim acta As act_img_cls = (From a In dt.act_img_cls Where a.act_img_id = id).First

        Return acta.act_img_file.ToArray()


    End Function

#End Region

#Region "Documentos Digitalizados"

    Public Function GuardaDocto(ByVal id As Integer, ByVal desc As String, ByVal Bit As Byte()) As Boolean
        tsn.ExecuteNonQuery("Insert into doc_dig_ope values(" & id & ", '" & desc & "', @@imagen)", Bit)
    End Function

    Public Function EliminaDocto(ByVal id As Integer) As Boolean
        tsn.ExecuteNonQuery("Delete From doc_dig_ope Where doc_dig_id=" & id)
    End Function

    Public Function DevuelveDocto(ByVal id As Integer) As Byte()
        
        Dim dt As New DataClsFactoringDataContext
        dt.CommandTimeout = 240
        Dim acta As doc_dig_ope_cls = (From a In dt.doc_dig_ope_cls Where a.doc_dig_id = id).First

        Return acta.doc_dig_file.ToArray()


    End Function

#End Region

End Class
