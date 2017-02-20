Imports Microsoft.VisualBasic
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports System

Public Class FormulasGenerales

    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes

    Public Function MAX(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        MAX = IIf(Par1 > Par2, Par1, Par2)
    End Function

    Public Function MIN(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        MIN = IIf(Par1 < Par2, Par1, Par2)
    End Function

    'Public Function RetornaCalculoInteres(ByVal parFechaDePago As String, _
    '                                      ByVal parDiasRetencionPago As Integer, _
    '                                      ByVal parTasaAplicarPago As Double, _
    '                                      ByVal parMontoPago As Double, _
    '                                      ByVal parFechaSimulacion As String, _
    '                                      ByVal parFechaVctoReal As String, _
    '                                      ByVal parCantidadDiasCalculoDifPre As Integer, _
    '                                      ByVal parSaldoCliente As Double, _
    '                                      ByVal parFechaUltimoPago As String, _
    '                                      ByVal parDiasDevolverInteres As Integer, _
    '                                      ByVal parOperacionLineal As String, _
    '                                      ByVal parTasaMensualAnual As String, _
    '                                      ByVal parTasaNegocio As Object, _
    '                                      ByVal parTasaRenovacion As Object, _
    '                                      ByVal parMontoAnticipo As Double, _
    '                                      ByVal parFechaVctoOriginal As String, _
    '                                      ByVal parNroRenovacion As Integer, _
    '                                      ByVal id_doc As Double, _
    '                                      ByVal BaseDiasMora As Char) As Double

    '    Dim varFechaRetencionDocto As String
    '    Dim varFechaLiberacionPago As String
    '    Dim varDiasRetencionDocto As Integer
    '    Dim varDiasCalculoMora As Integer
    '    Dim varDiasDevolucionInteres As Integer
    '    Dim IDX As Integer
    '    Dim varInteresMora As Double
    '    Dim varInteresRetencion As Double
    '    Dim varInteresDevolver As Double
    '    Dim varDifDias As Integer
    '    Dim varTasaDevInt As Object
    '    Dim varDifPrecio As Double
    '    Dim fun As New ClaseComercial
    '    Dim BaseAnual As Integer
    '    'Dim DiasMinDevInt As Integer


    '    '-----------------------------------------Interes de Mora---------------------------------------------------------------------------
    '    'Calcula Fecha y Días de Retención Docto
    '    varDifDias = parCantidadDiasCalculoDifPre - DateDiff("d", parFechaSimulacion, parFechaVctoOriginal)
    '    varFechaRetencionDocto = parFechaVctoReal
    '    varDiasRetencionDocto = varDifDias

    '    'Calcula Fecha Liberacion de Pago
    '    varFechaLiberacionPago = parFechaDePago
    '    For IDX = 1 To parDiasRetencionPago
    '        varFechaLiberacionPago = DateAdd("d", 1, varFechaLiberacionPago)
    '        varFechaLiberacionPago = fun.DiaHabilDevuelve(varFechaLiberacionPago)
    '    Next

    '    'Se Cobra mora solo si FechaPago <> FechaVctoReal o FechaPago es Vispera de No Habil
    '    fun.DiaHabilDevuelve(DateAdd("d", 1, parFechaDePago))
    '    If parFechaDePago = parFechaVctoReal And _
    '       DateDiff("d", parFechaDePago, fun.DiaHabilDevuelve(DateAdd("d", 1, parFechaDePago))) = 1 Then
    '        varDiasCalculoMora = 0
    '    Else
    '        'Calcula Días de Mora a Cobrar  
    '        varDiasCalculoMora = DateDiff("d", MAX(CDate(varFechaRetencionDocto), CDate(parFechaUltimoPago)), varFechaLiberacionPago) * _
    '                             MAX(MIN(DateDiff("d", varFechaRetencionDocto, varFechaLiberacionPago), 1), 0)
    '    End If


    '    '-----------------------------------------Fin Interes de Mora---------------------------------------------------------------------------

    '    'DiasMinDevInt = CG.SistemaDevuelve().sis_dia_pgo

    '    'Interes de Retencion Cobrada en Otorgamiento 
    '    varInteresRetencion = 0
    '    'FIN Interes de Retencion Cobrada en Otorgamiento 

    '    '-----------------------------------------Interes a Devolver ---------------------------------------------------------------------------

    '    'Si desde la fecha de simulacion se le suman los dias de sistema y es mayor a la fecha del pago y menor a la fecha de vcto.        
    '    If DateTime.Parse(parFechaSimulacion).AddDays(CG.SistemaDevuelve().sis_dia_pgo) > DateTime.Parse(varFechaLiberacionPago) And _
    '       DateTime.Parse(parFechaSimulacion).AddDays(CG.SistemaDevuelve().sis_dia_pgo) < DateTime.Parse(parFechaVctoReal) Then
    '        'se toma la diferencia entre la fecha de simulacion + dias de sistema hasta la fecha de vcto.
    '        varDiasDevolucionInteres = DateDiff("d", _
    '                                            DateTime.Parse(parFechaSimulacion).AddDays(CG.SistemaDevuelve().sis_dia_pgo), _
    '                                            varFechaRetencionDocto)
    '    Else
    '        'se toma desde la fecha de pago hasta la fecha de vcto.
    '        varDiasDevolucionInteres = DateDiff("d", _
    '                                            varFechaLiberacionPago, _
    '                                            varFechaRetencionDocto)
    '    End If


    '    'Si Docto.Renovado Cantidad de Dias Fecha de Vencimiento original + Retencion hasta Fecha de Vcto prorrogada
    '    varTasaDevInt = parTasaNegocio

    '    If parNroRenovacion <> 0 Then
    '        parCantidadDiasCalculoDifPre = DateDiff("d", _
    '                                                DateAdd("d", varDifDias, parFechaVctoOriginal), _
    '                                                parFechaVctoReal)
    '        varTasaDevInt = parTasaRenovacion
    '    End If

    '    If BaseDiasMora = "C" Then
    '        BaseAnual = 360
    '    Else
    '        If FC.IsAñoBisiesto(CDate(parFechaDePago).Year) Then
    '            BaseAnual = 366
    '        Else
    '            BaseAnual = 365
    '        End If
    '    End If

    '    'Calcula Interes de Mora solo si Dias de Mora es > a 0
    '    varInteresMora = 0

    '    'Si la fecha del pago es mayor al vencimiento y no tiene pagos validados a la fecha del mismo, devuelve la mora. 
    '    If DateTime.Parse(parFechaDePago) > DateTime.Parse(parFechaVctoReal) Then
    '        varInteresMora = parSaldoCliente * ((1 + (parTasaAplicarPago / 100)) ^ (1 / BaseAnual) - 1) * varDiasCalculoMora
    '    End If

    '    If parDiasDevolverInteres < varDiasDevolucionInteres Then
    '        If parOperacionLineal = "S" Then
    '            If parTasaMensualAnual = "M" Then
    '                varDifPrecio = ((parMontoAnticipo * varDiasDevolucionInteres * (varTasaDevInt / 100)) / 30)
    '            Else
    '                varDifPrecio = ((parMontoAnticipo * varDiasDevolucionInteres * (varTasaDevInt / 100)) / BaseAnual)
    '            End If
    '            If parCantidadDiasCalculoDifPre > 0 Then
    '                varInteresDevolver = (varDifPrecio / parCantidadDiasCalculoDifPre) * varDiasDevolucionInteres * (MAX(MIN(varDiasDevolucionInteres, 1), 0) * MAX(IIf((parMontoPago - parSaldoCliente) >= 0, 1, 0), 0)) * MAX(MIN(parSaldoCliente, 1), 0)
    '            End If
    '        Else
    '            If parTasaMensualAnual = "M" Then
    '                varDifPrecio = parMontoAnticipo / ((1 + (varTasaDevInt / 100)) ^ (varDiasDevolucionInteres / 30))
    '            Else
    '                varDifPrecio = parMontoAnticipo / ((1 + (varTasaDevInt / 100)) ^ (varDiasDevolucionInteres / BaseAnual))
    '            End If

    '            varDifPrecio = (parMontoAnticipo - varDifPrecio)

    '            If varDiasDevolucionInteres > 0 Then
    '                varInteresDevolver = varDifPrecio * (MAX(MIN(varDiasDevolucionInteres, 1), 0) * MAX(IIf((parMontoPago - parSaldoCliente) >= 0, 1, 0), 0)) * MAX(MIN(parSaldoCliente, 1), 0)
    '            End If
    '        End If
    '    Else
    '        varInteresDevolver = 0
    '    End If

    '    '-----------------------------------------Fin Interes a Devolver ---------------------------------------------------------------------------

    '    RetornaCalculoInteres = Format(varInteresMora, "##0.00") - Format(varInteresRetencion, "##0.00") - Format(varInteresDevolver, "##0.00")

    'End Function

    Public Function RetornaCalculoInteres(ByVal parFechaDePago As String, _
                                          ByVal parDiasRetencionPago As Integer, _
                                          ByVal parTasaAplicarPago As Double, _
                                          ByVal parMontoPago As Double, _
                                          ByVal parFechaSimulacion As String, _
                                          ByVal parFechaVctoReal As String, _
                                          ByVal parCantidadDiasCalculoDifPre As Integer, _
                                          ByVal parSaldoCliente As Double, _
                                          ByVal parFechaUltimoPago As String, _
                                          ByVal parDiasDevolverInteres As Integer, _
                                          ByVal parOperacionLineal As String, _
                                          ByVal parTasaMensualAnual As String, _
                                          ByVal parTasaNegocio As Object, _
                                          ByVal parTasaRenovacion As Object, _
                                          ByVal parMontoAnticipo As Double, _
                                          ByVal parFechaVctoOriginal As String, _
                                          ByVal parNroRenovacion As Integer, _
                                          ByVal id_doc As Double, _
                                          ByVal BaseDiasMora As Char) As Double

        Dim varFechaRetencionDocto As String
        Dim varFechaLiberacionPago As String
        Dim varDiasRetencionDocto As Integer
        Dim varDiasCalculoMora As Integer
        Dim varDiasDevolucionInteres As Integer
        Dim IDX As Integer
        Dim varInteresMora As Double
        Dim varInteresRetencion As Double
        Dim varInteresDevolver As Double
        Dim varDifDias As Integer
        Dim varTasaDevInt As Object
        Dim varDifPrecio As Double
        Dim fun As New ClaseComercial
        Dim BaseAnual As Integer

        '/****** Interes de Mora ***********************************************************************************************/
        'Calcula Fecha y Días de Retención Docto
        varDifDias = parCantidadDiasCalculoDifPre - DateDiff("d", parFechaSimulacion, parFechaVctoOriginal)
        varFechaRetencionDocto = parFechaVctoReal
        varDiasRetencionDocto = varDifDias

        '/*Calcula Fecha Liberacion de Pago
        varFechaLiberacionPago = parFechaDePago
        For IDX = 1 To parDiasRetencionPago
            varFechaLiberacionPago = DateAdd("d", 1, varFechaLiberacionPago)
            varFechaLiberacionPago = fun.DiaHabilDevuelve(varFechaLiberacionPago)
        Next

        '/*Se Cobra mora solo si FechaPago <> FechaVctoReal o FechaPago es Vispera de No Habil
        fun.DiaHabilDevuelve(DateAdd("d", 1, parFechaDePago))
        If parFechaDePago = parFechaVctoReal And _
           DateDiff("d", parFechaDePago, fun.DiaHabilDevuelve(DateAdd("d", 1, parFechaDePago))) = 1 Then
            varDiasCalculoMora = 0
        Else
            '/*Calcula Días de Mora a Cobrar  
            varDiasCalculoMora = DateDiff("d", MAX(CDate(varFechaRetencionDocto), CDate(parFechaUltimoPago)), varFechaLiberacionPago) * _
                                 MAX(MIN(DateDiff("d", varFechaRetencionDocto, varFechaLiberacionPago), 1), 0)
        End If

        If BaseDiasMora = "C" Then
            BaseAnual = 360
        Else
            If FC.IsAñoBisiesto(CDate(parFechaDePago).Year) Then
                BaseAnual = 366
            Else
                BaseAnual = 365
            End If
        End If

        'Calcula Interes de Mora solo si Dias de Mora es > a 0
        varInteresMora = 0

        'Si la fecha del pago es mayor al vencimiento y no tiene pagos validados a la fecha del mismo, devuelve la mora. 
        If DateTime.Parse(parFechaDePago) > DateTime.Parse(parFechaVctoReal) Then
            varInteresMora = parSaldoCliente * ((1 + (parTasaAplicarPago / 100)) ^ (1 / BaseAnual) - 1) * varDiasCalculoMora
        End If

        varInteresRetencion = 0

        '/****** Interes a Devolver **********************************************************************/
        varDiasDevolucionInteres = DateDiff("d", varFechaLiberacionPago, varFechaRetencionDocto)

        'Si Docto.Renovado Cantidad de Dias Fecha de Vencimiento original + Retencion hasta Fecha de Vcto prorrogada
        varTasaDevInt = parTasaNegocio

        If parNroRenovacion <> 0 Then
            parCantidadDiasCalculoDifPre = DateDiff("d", DateAdd("d", varDifDias, parFechaVctoOriginal), parFechaVctoReal)
            varTasaDevInt = parTasaRenovacion
        End If

        If (parDiasDevolverInteres < varDiasDevolucionInteres) Then

            If parOperacionLineal = "S" Then

                If parTasaMensualAnual = "M" Then
                    varDifPrecio = ((parMontoAnticipo * parCantidadDiasCalculoDifPre * (varTasaDevInt / 100)) / 30)
                Else
                    varDifPrecio = ((parMontoAnticipo * parCantidadDiasCalculoDifPre * (varTasaDevInt / 100)) / BaseAnual)
                End If

                If parCantidadDiasCalculoDifPre > 0 Then
                    varInteresDevolver = (varDifPrecio / parCantidadDiasCalculoDifPre) * varDiasDevolucionInteres * (MAX(MIN(varDiasDevolucionInteres, 1), 0) * MAX(IIf((parMontoPago - parSaldoCliente) >= 0, 1, 0), 0)) * MAX(MIN(parSaldoCliente, 1), 0)
                End If

            Else

                If parTasaMensualAnual = "M" Then
                    varDifPrecio = parMontoAnticipo / ((1 + (varTasaDevInt / 100)) ^ (varDiasDevolucionInteres / 30))
                Else
                    varDifPrecio = parMontoAnticipo / ((1 + (varTasaDevInt / 100)) ^ (varDiasDevolucionInteres / BaseAnual))
                End If

                varDifPrecio = (parMontoAnticipo - varDifPrecio)

                If parCantidadDiasCalculoDifPre > 0 Then
                    varInteresDevolver = varDifPrecio * (MAX(MIN(varDiasDevolucionInteres, 1), 0) * MAX(IIf((parMontoPago - parSaldoCliente) >= 0, 1, 0), 0)) * MAX(MIN(parSaldoCliente, 1), 0)
                End If

            End If

        Else
            varInteresDevolver = 0
        End If

        'If (parMontoPago < parMontoAnticipo) Then
        '    varInteresDevolver = 0
        'End If

        '/****** FIN Interes a Devolver ******************************************************************/
        RetornaCalculoInteres = Format(varInteresMora, "##0.00") - Format(varInteresRetencion, "##0.00") - Format(varInteresDevolver, "##0.00")

    End Function

    Public Function DiferenciaDePrecio(ByVal FechaVctoReal As Date, ByVal FechaNegociacion As Date, _
                                       ByVal MtoAnticipo As Double, ByVal TasaNeg As Decimal, _
                                       ByVal parTasaMensualAnual As Char, ByVal Lineal As Char, _
                                       ByVal BaseDiasMora As Char) As Double

        Try

            Dim NroDiaVcto As Integer
            Dim BaseAnual As Integer

            If BaseDiasMora = "C" Then
                BaseAnual = 360
            Else
                If FC.IsAñoBisiesto(FechaNegociacion.Year) Then
                    BaseAnual = 366
                Else
                    BaseAnual = 365
                End If
            End If

            NroDiaVcto = DateDiff("d", FechaNegociacion, FechaVctoReal)

            If Lineal = "S" Then
                'LINEAL
                If parTasaMensualAnual = "M" Then
                    DiferenciaDePrecio = ((MtoAnticipo * (NroDiaVcto) * (TasaNeg / 100)) / 30)
                Else
                    DiferenciaDePrecio = ((MtoAnticipo * (NroDiaVcto) * (TasaNeg / 100)) / BaseAnual)
                End If
            Else
                'EXPONENCIAL
                If parTasaMensualAnual = "M" Then
                    DiferenciaDePrecio = MtoAnticipo / ((1 + (TasaNeg / 100)) ^ (NroDiaVcto / 30))
                Else
                    DiferenciaDePrecio = MtoAnticipo / ((1 + (TasaNeg / 100)) ^ (NroDiaVcto / BaseAnual))
                End If
                DiferenciaDePrecio = (MtoAnticipo - DiferenciaDePrecio)
            End If

        Catch ex As Exception
            DiferenciaDePrecio = 0
        End Try

    End Function

    Public Function Prorrogas_CalculaInteres(ByVal TasaInteres As Double, ByVal FechaVcto As DateTime, ByVal NvaFechaVcto As DateTime, ByVal SaldoDocumento As Double) As Double
        Dim CantidadDias As Int16

        CantidadDias = DateDiff("d", FechaVcto, NvaFechaVcto)
        Prorrogas_CalculaInteres = (((TasaInteres / 100) / 30) * (SaldoDocumento)) * CantidadDias

    End Function

    Public Sub Crea_Ing_sec(ByVal Tipo As Int16, ByVal Indice_DPO As Integer, ByVal Monto_Abonado As Double, _
                           ByVal Interes_Abonado As Double, ByVal Objeto As Object)
        '-------------------------------------------------------------------------------------------------------
        'P Gatica 04/05/2009 -Se modifica agregandole el item documentos no cedidos
        'J Lagos 13/11/2012  -Se agrega interes a devolver
        '-------------------------------------------------------------------------------------------------------

        Try

            Dim ABONO_CLIENTE, EXCEDENTE, MAYOR_PAGO, MONTO_MENOR, REAJUSTE As Double
            Dim SaldoPorPagar As Double = 0
            Dim MONTO As Double
            Dim INTERES As Double
            Dim mto_abo_real As Double
            Dim Ing_Sec As New ing_sec_cls
            Dim Pagos As New ClsSession.SesionPagos
            Dim Formulas As New FormulasGenerales


            If Tipo = 1 Then

                '1.- CUENTAS POR COBRAR

                Ing_Sec.cli_idc = Objeto.cli_idc
                Ing_Sec.id_ing_sec = Nothing
                Ing_Sec.id_ing = Nothing
                Ing_Sec.id_dpo = Indice_DPO
                Ing_Sec.id_egr_sec = Indice_DPO
                Ing_Sec.ing_qpa = Pagos.Pagador
                Ing_Sec.ing_vld_rcz = CChar("I")
                Ing_Sec.ing_pro = "N"
                Ing_Sec.ing_tas_apl = CDec(Objeto.Tasa)

                FACTOR_CAMBIO = CG.ParidadDevuelve(Objeto.id_p_0023, Date.Now.ToShortDateString).par_val

                Ing_Sec.id_P_0053 = 1

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                'Toma el factor de cuando se ingreso la cuenta
                FACTOR_CAMBIO = CG.ParidadDevuelve(Objeto.id_p_0023, Date.Now.ToShortDateString).par_val
                Ing_Sec.id_cxc = Objeto.id_cxc
                Ing_Sec.doc_sdo_ddr = 0

                If Interes_Abonado > 0 Then
                    mto_abo_real = Monto_Abonado - Interes_Abonado
                Else
                    mto_abo_real = Monto_Abonado
                End If

                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli)

                'MONTO_MENOR = Formulas.MIN((Ing_Sec.doc_sdo_cli / FACTOR_CAMBIO_HOY), (Ing_Sec.ing_mto_abo / FACTOR_CAMBIO_HOY))
                'REAJUSTE = Formulas.MIN((ABONO_CLIENTE / FACTOR_CAMBIO_HOY), (MONTO_MENOR) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO))
                MONTO_MENOR = Formulas.MIN(Ing_Sec.doc_sdo_cli, Ing_Sec.ing_mto_abo)
                REAJUSTE = Formulas.MIN(ABONO_CLIENTE, MONTO_MENOR)

                Ing_Sec.ing_pag_deu = CChar("N")

                'MONTO = Monto_Abonado / FACTOR_CAMBIO_HOY
                'INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                MONTO = Monto_Abonado
                INTERES = Interes_Abonado

                Ing_Sec.ing_fac_cam = FACTOR_CAMBIO_HOY

                If IsNothing(Ing_Sec.doc_sdo_cli) Then
                    'Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar) * Ing_Sec.ing_fac_cam
                    Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar)
                End If

                Ing_Sec.ing_rea_mon = REAJUSTE

                'Ojo ver cuando el interes es negativo
                'Ing_Sec.ing_mto_int = INTERES * Ing_Sec.ing_fac_cam
                Ing_Sec.ing_mto_int = INTERES
                Ing_Sec.ing_mto_abo = mto_abo_real
                Ing_Sec.ing_mto_tot = Ing_Sec.ing_mto_abo + Ing_Sec.ing_mto_int
                'Ing_Sec.ing_int_dev = CDbl(Objeto.InteresDevolver * Ing_Sec.ing_fac_cam)
                Ing_Sec.ing_int_dev = CDbl(Objeto.InteresDevolver)
                Ing_Sec.ing_fac_cam_obs = FACTOR_CAMBIO

                If Val(Ing_Sec.ing_mto_tot) >= Val(Ing_Sec.doc_sdo_cli) Then
                    Ing_Sec.ing_tot_par = "T"
                Else
                    Ing_Sec.ing_tot_par = "P"
                End If

            ElseIf Tipo = 2 Then

                '2.- DOCUMENTOS
                Ing_Sec.cli_idc = Objeto.cli_idc
                Ing_Sec.id_ing_sec = Nothing
                Ing_Sec.id_ing = Nothing
                Ing_Sec.id_dpo = Indice_DPO
                Ing_Sec.id_egr_sec = Indice_DPO
                Ing_Sec.ing_qpa = Pagos.Pagador
                Ing_Sec.ing_vld_rcz = CChar("I")
                Ing_Sec.ing_pro = "N"
                Ing_Sec.ing_tas_apl = CDbl(Objeto.Tasa)

                FACTOR_CAMBIO = CG.ParidadDevuelve(Objeto.id_p_0023, Date.Now.ToShortDateString).par_val

                Ing_Sec.id_P_0053 = 2
                Ing_Sec.id_doc = Objeto.id_doc
                FACTOR_CAMBIO_HOY = Objeto.ope_fac_cam

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                '/*Calcula todo como Pago Deudor***************************************************/

                If Interes_Abonado > 0 Then
                    mto_abo_real = Monto_Abonado - Interes_Abonado
                Else
                    mto_abo_real = Monto_Abonado
                End If

                If Pagos.Pagador <> "D" Then
                    ABONO_CLIENTE = MAX(MIN(mto_abo_real, Objeto.doc_sdo_cli), 0)
                Else
                    '10-12-2014 JLAGOS
                    ABONO_CLIENTE = Formulas.MIN(mto_abo_real, Objeto.doc_sdo_cli + Objeto.dsi_mto - Objeto.dsi_mto_ant)
                    'ABONO_CLIENTE = Formulas.MIN(mto_abo_real, Objeto.doc_sdo_ddr + Objeto.dsi_mto - Objeto.dsi_mto_ant)
                End If

                EXCEDENTE = Formulas.MAX(Formulas.MIN(Ing_Sec.ing_mto_abo - Objeto.doc_sdo_cli, Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)
                MAYOR_PAGO = Formulas.MAX(Ing_Sec.ing_mto_abo - Objeto.doc_sdo_cli - (Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)

                '/*Calculo de Reajuste **************************************************/
                'REAJUSTE = Formulas.MIN(ABONO_CLIENTE, Objeto.dsi_mto) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO)
                REAJUSTE = Formulas.MIN(ABONO_CLIENTE, Objeto.dsi_mto)

                Ing_Sec.ing_pag_deu = CChar(Objeto.PagaDeudor)

                'MONTO = ABONO_CLIENTE / FACTOR_CAMBIO_HOY
                'INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                MONTO = ABONO_CLIENTE
                INTERES = Interes_Abonado

                Ing_Sec.ing_fac_cam = FACTOR_CAMBIO_HOY

                Select Case Ing_Sec.ing_qpa

                    Case "C", "E", "O"

                        If IsNothing(Ing_Sec.doc_sdo_cli) Then
                            'Ing_Sec.doc_sdo_cli = Objeto.doc_sdo_cli * Ing_Sec.ing_fac_cam ' CDec(Objeto.MontoPagar)
                            Ing_Sec.doc_sdo_cli = Objeto.doc_sdo_cli
                            Ing_Sec.doc_sdo_cli = Objeto.doc_sdo_cli
                        End If

                        'Ing_Sec.doc_sdo_ddr = Objeto.doc_sdo_ddr * Ing_Sec.ing_fac_cam
                        Ing_Sec.doc_sdo_ddr = Objeto.doc_sdo_ddr

                    Case "D"

                        If IsNothing(Ing_Sec.doc_sdo_ddr) Then
                            'Ing_Sec.doc_sdo_ddr = Objeto.doc_sdo_ddr * Ing_Sec.ing_fac_cam 'CDec(Objeto.MontoPagar)
                            Ing_Sec.doc_sdo_ddr = Objeto.doc_sdo_ddr
                            Ing_Sec.doc_sdo_ddr = Objeto.doc_sdo_ddr
                        End If

                        'Ing_Sec.doc_sdo_cli = Objeto.doc_sdo_cli * Ing_Sec.ing_fac_cam
                        Ing_Sec.doc_sdo_cli = Objeto.doc_sdo_cli

                End Select

                Ing_Sec.ing_rea_mon = REAJUSTE

                'Ojo ver cuando el interes es negativo
                'Ing_Sec.ing_mto_int = INTERES * Ing_Sec.ing_fac_cam
                'Ing_Sec.ing_mto_abo = MONTO * Ing_Sec.ing_fac_cam
                'Ing_Sec.ing_int_dev = CDbl(Objeto.InteresDevolver * Ing_Sec.ing_fac_cam)
                Ing_Sec.ing_mto_int = INTERES
                Ing_Sec.ing_mto_abo = MONTO
                Ing_Sec.ing_int_dev = CDbl(Objeto.InteresDevolver)
                Ing_Sec.doc_sdo_exc = CDec(Objeto.doc_sdo_exc)

                If Pagos.Pagador = "C" Then
                    If Ing_Sec.ing_mto_int < 0 Then
                        Ing_Sec.ing_mto_tot = Monto_Abonado
                    Else
                        Ing_Sec.ing_mto_tot = Monto_Abonado
                    End If
                Else
                    If Ing_Sec.ing_mto_int < 0 Then
                        Ing_Sec.ing_mto_tot = Monto_Abonado
                    Else
                        Ing_Sec.ing_mto_tot = Monto_Abonado
                    End If

                End If

                Ing_Sec.ing_fac_cam_obs = FACTOR_CAMBIO

                Select Case Ing_Sec.ing_qpa

                    Case "C"

                        If Val(Ing_Sec.ing_mto_tot) >= Val(Ing_Sec.doc_sdo_cli) Then
                            'If Ing_Sec.doc_sdo_cli = 0 Then
                            Ing_Sec.ing_tot_par = "T"
                        Else
                            Ing_Sec.ing_tot_par = "P"
                        End If

                    Case "D"

                        If Val(Ing_Sec.ing_mto_tot) >= Val(Ing_Sec.doc_sdo_ddr) Then
                            'If Ing_Sec.doc_sdo_cli = 0 Then
                            Ing_Sec.ing_tot_par = "T"
                        Else
                            Ing_Sec.ing_tot_par = "P"
                        End If

                End Select

            ElseIf Tipo = 3 Then

                '3 Documentos No Cedidos
                With Ing_Sec

                    .id_P_0053 = 7
                    .id_ing_sec = Nothing
                    .id_ing = Nothing
                    .id_dpo = Indice_DPO

                    If Objeto.S_CLIENTE = 0 Then
                        .ing_qpa = "D"
                    Else
                        .ing_qpa = "C"
                    End If

                    .ing_vld_rcz = CChar("I")
                    .id_nce = CInt(Objeto.ID_NCE)
                    .cli_idc = Objeto.RUT_CLI
                    .ing_rea_mon = REAJUSTE

                    'Ojo ver cuando el interes es negativo
                    .ing_mto_int = 0
                    .ing_fac_cam = FACTOR_CAMBIO_HOY 'Pagos.DollarCobranza
                    .ing_fac_cam_obs = CDbl(FACTOR_CAMBIO_OBS_HOY)  'Pagos.DollarObservador
                    '.ing_mto_abo = CDbl(Objeto.DOC_MTO) * Ing_Sec.ing_fac_cam
                    '.ing_mto_tot = CDbl(Objeto.DOC_MTO) * Ing_Sec.ing_fac_cam
                    .ing_mto_abo = CDbl(Objeto.DOC_MTO)
                    .ing_mto_tot = CDbl(Objeto.DOC_MTO)
                    .ing_tas_apl = 0
                    .ing_tot_par = "T"

                End With

            End If

            'Crea la collection con los objetos para que luego sean grabados
            Pagos.Coll_Ing_Sec.Add(Ing_Sec)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub CargaCollection_Ingresos(ByVal Coll_Dpo_Egr As Collection, _
                                        ByVal Coll_Cxc_Seleccionados As Collection, _
                                        ByVal Coll_Doctos_Seleccionados As Collection, _
                                        ByVal Dpo_Egr As Int16, _
                                        ByVal Fecha As DateTime)

        Try

            '05-03-2014 jlagos

            Dim Pagos As New ClsSession.SesionPagos
            Dim Ind_cxc As Integer = 1
            Dim Ind_doc As Integer = 1
            Dim Ind_dpo As Integer = 1
            Dim Saldo_DPO As Double = 0
            Dim MTO_TOTAL As Double = 0
            Dim INTERES As Double = 0

            Dim ind_cxc_aux As Integer = 1
            Dim ind_doc_aux As Integer = 1

            Pagos.Coll_Ing_Sec = New Collection

            For Ind_dpo = 1 To Coll_Dpo_Egr.Count

                If Dpo_Egr = 1 Then
                    Saldo_DPO = Coll_Dpo_Egr.Item(Ind_dpo).dpo_mto 'Asignamos el saldo del documento de pago
                ElseIf Dpo_Egr = 2 Then
                    Saldo_DPO = Coll_Dpo_Egr.Item(Ind_dpo).egr_mto 'Asignamos el saldo del egreso
                End If

                'CXC
                For Ind_cxc = ind_cxc_aux To Coll_Cxc_Seleccionados.Count

                    FACTOR_CAMBIO_HOY = Coll_Cxc_Seleccionados.Item(Ind_cxc).cxc_fac_cam
                    FACTOR_CAMBIO_OBS = CG.RETORNA_VALOR_MONEDA(1, Coll_Cxc_Seleccionados.Item(Ind_cxc).id_p_0023, 1, Fecha)

                    Select Case Coll_Cxc_Seleccionados.Item(Ind_cxc).id_p_0023
                        Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                        Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                        Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                        Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                    End Select

                    If (Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar) <= Saldo_DPO Then
                        MTO_TOTAL = Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar
                        INTERES = Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes
                        Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    Else
                        If Saldo_DPO <= (Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes) Then
                            INTERES = Saldo_DPO
                            MTO_TOTAL = INTERES
                        Else
                            INTERES = Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes
                            MTO_TOTAL = Saldo_DPO
                        End If
                        Saldo_DPO = 0
                    End If
                    'If (Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar * FACTOR_CAMBIO_HOY) <= Saldo_DPO Then
                    '    MTO_TOTAL = Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar * FACTOR_CAMBIO_HOY
                    '    INTERES = Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes * FACTOR_CAMBIO_HOY
                    '    Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    'Else
                    '    If Saldo_DPO <= (Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes * FACTOR_CAMBIO_HOY) Then
                    '        INTERES = Saldo_DPO
                    '        MTO_TOTAL = INTERES
                    '    Else
                    '        INTERES = Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes * FACTOR_CAMBIO_HOY
                    '        MTO_TOTAL = Saldo_DPO
                    '    End If
                    '    Saldo_DPO = 0
                    'End If

                    Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar = Coll_Cxc_Seleccionados.Item(Ind_cxc).MontoPagar - MTO_TOTAL
                    Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes = Coll_Cxc_Seleccionados.Item(Ind_cxc).Interes - INTERES

                    Crea_Ing_sec(1, Ind_dpo, (MTO_TOTAL), INTERES, Coll_Cxc_Seleccionados.Item(Ind_cxc))

                    If Saldo_DPO <= 0 Then
                        GoTo Ciclo_DPO
                    End If

                    ind_cxc_aux += 1

                Next

                '------------------------------------------------------------------------------------------------------------------------------
                '------------------------------------------------------------------------------------------------------------------------------
                '------------------------------------------------------------------------------------------------------------------------------

                'DOC
                For Ind_doc = ind_doc_aux To Coll_Doctos_Seleccionados.Count

                    FACTOR_CAMBIO_HOY = Coll_Doctos_Seleccionados.Item(Ind_doc).ope_fac_cam
                    FACTOR_CAMBIO_OBS = CG.RETORNA_VALOR_MONEDA(1, Coll_Doctos_Seleccionados.Item(Ind_doc).id_p_0023, 1, Fecha)

                    Select Case Coll_Doctos_Seleccionados.Item(Ind_doc).id_p_0023
                        Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                        Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                        Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                        Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                    End Select

                    'If (Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar * FACTOR_CAMBIO_HOY) <= Saldo_DPO Then
                    '    MTO_TOTAL = Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar * FACTOR_CAMBIO_HOY
                    '    INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                    '    Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    'Else
                    '    If (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY) > 0 Then
                    '        If Saldo_DPO <= (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY) Then
                    '            INTERES = Saldo_DPO
                    '            MTO_TOTAL = INTERES
                    '        Else
                    '            INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                    '            MTO_TOTAL = Saldo_DPO
                    '        End If
                    '    Else
                    '        INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                    '        MTO_TOTAL = Saldo_DPO
                    '    End If
                    '    Saldo_DPO = 0
                    'End If

                    If (Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar) <= Saldo_DPO Then
                        MTO_TOTAL = Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar
                        INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes
                        Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    Else
                        If (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes) > 0 Then
                            If Saldo_DPO <= (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes) Then
                                INTERES = Saldo_DPO
                                MTO_TOTAL = INTERES
                            Else
                                INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes
                                MTO_TOTAL = Saldo_DPO
                            End If
                        Else
                            INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes
                            MTO_TOTAL = Saldo_DPO
                        End If
                        Saldo_DPO = 0
                    End If

                    Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar = Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar - MTO_TOTAL
                    Coll_Doctos_Seleccionados.Item(Ind_doc).Interes = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes - INTERES

                    Crea_Ing_sec(2, Ind_dpo, (MTO_TOTAL), INTERES, Coll_Doctos_Seleccionados.Item(Ind_doc))

                    Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_cli = Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_cli - (MTO_TOTAL - INTERES)

                    If Pagos.Pagador = "D" Then
                        Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_ddr = Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_ddr - MTO_TOTAL
                    End If

                    If Saldo_DPO <= 0 Then
                        GoTo Ciclo_DPO
                    End If

                    ind_doc_aux += 1

                Next
Ciclo_DPO:
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Sub CargaCollection_IngresosRec(ByVal Coll_Dpo_Egr As Collection, _
                                           ByVal Coll_Doctos_Seleccionados As Collection, _
                                           ByVal Coll_NCE As Collection, _
                                           ByVal Dpo_Egr As Int16, _
                                           ByVal Fecha As DateTime)

        Try

            '05-03-2014 jlagos

            Dim Pagos As New ClsSession.SesionPagos
            Dim Ind_nce As Integer = 1
            Dim Ind_doc As Integer = 1
            Dim Ind_dpo As Integer = 1
            Dim Saldo_DPO As Double = 0
            Dim MTO_TOTAL As Double = 0
            Dim INTERES As Double = 0

            Dim ind_cxc_aux As Integer = 1
            Dim ind_doc_aux As Integer = 1

            Pagos.Coll_Ing_Sec = New Collection

            For Ind_dpo = 1 To Coll_Dpo_Egr.Count

                If Dpo_Egr = 1 Then
                    Saldo_DPO = Coll_Dpo_Egr.Item(Ind_dpo).dpo_mto 'Asignamos el saldo del documento de pago
                ElseIf Dpo_Egr = 2 Then
                    Saldo_DPO = Coll_Dpo_Egr.Item(Ind_dpo).egr_mto 'Asignamos el saldo del egreso
                End If

                'CXC
                For Ind_nce = ind_cxc_aux To Coll_NCE.Count

                    'FACTOR_CAMBIO_HOY = Coll_NCE.Item(Ind_nce).nce_fac_cam
                    FACTOR_CAMBIO_OBS = CG.RETORNA_VALOR_MONEDA(1, Coll_NCE.Item(Ind_nce).D_MONEDA, 1, Fecha)

                    Select Case Coll_NCE.Item(Ind_nce).D_MONEDA
                        Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                        Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                        Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                        Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                    End Select

                    If (Coll_NCE.Item(Ind_nce).MON_DOCTO * FACTOR_CAMBIO_HOY) <= Saldo_DPO Then
                        MTO_TOTAL = Coll_NCE.Item(Ind_nce).MON_DOCTO * FACTOR_CAMBIO_HOY
                        INTERES = Coll_NCE.Item(Ind_nce).INTERES * FACTOR_CAMBIO_HOY
                        Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    Else

                        If Saldo_DPO <= (Coll_NCE.Item(Ind_nce).INTERES * FACTOR_CAMBIO_HOY) Then
                            INTERES = Saldo_DPO
                            MTO_TOTAL = INTERES
                        Else
                            INTERES = Coll_NCE.Item(Ind_nce).INTERES * FACTOR_CAMBIO_HOY
                            MTO_TOTAL = Saldo_DPO
                        End If

                        Saldo_DPO = 0

                    End If

                    Coll_NCE.Item(Ind_nce).MON_DOCTO = Coll_NCE.Item(Ind_nce).MON_DOCTO - MTO_TOTAL
                    Coll_NCE.Item(Ind_nce).INTERES = Coll_NCE.Item(Ind_nce).INTERES - INTERES

                    Crea_Ing_sec(3, Ind_dpo, (MTO_TOTAL), INTERES, Coll_NCE.Item(Ind_nce))

                    If Saldo_DPO <= 0 Then
                        GoTo Ciclo_DPO
                    End If

                    ind_cxc_aux += 1

                Next

                '------------------------------------------------------------------------------------------------------------------------------
                '------------------------------------------------------------------------------------------------------------------------------
                '------------------------------------------------------------------------------------------------------------------------------

                'DOC
                For Ind_doc = ind_doc_aux To Coll_Doctos_Seleccionados.Count

                    FACTOR_CAMBIO_HOY = Coll_Doctos_Seleccionados.Item(Ind_doc).ope_fac_cam
                    FACTOR_CAMBIO_OBS = CG.RETORNA_VALOR_MONEDA(1, Coll_Doctos_Seleccionados.Item(Ind_doc).id_p_0023, 1, Fecha)

                    Select Case Coll_Doctos_Seleccionados.Item(Ind_doc).id_p_0023
                        Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                        Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                        Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                        Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                    End Select

                    If (Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar * FACTOR_CAMBIO_HOY) <= Saldo_DPO Then
                        MTO_TOTAL = Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar * FACTOR_CAMBIO_HOY
                        INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                        Saldo_DPO = Saldo_DPO - MTO_TOTAL
                    Else

                        If (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY) > 0 Then
                            If Saldo_DPO <= (Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY) Then
                                INTERES = Saldo_DPO
                                MTO_TOTAL = INTERES
                            Else
                                INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                                MTO_TOTAL = Saldo_DPO
                            End If
                        Else
                            INTERES = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes * FACTOR_CAMBIO_HOY
                            MTO_TOTAL = Saldo_DPO
                        End If

                        Saldo_DPO = 0

                    End If

                    Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar = Coll_Doctos_Seleccionados.Item(Ind_doc).MontoPagar - MTO_TOTAL
                    Coll_Doctos_Seleccionados.Item(Ind_doc).Interes = Coll_Doctos_Seleccionados.Item(Ind_doc).Interes - INTERES

                    Crea_Ing_sec(2, Ind_dpo, (MTO_TOTAL), INTERES, Coll_Doctos_Seleccionados.Item(Ind_doc))

                    Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_cli = Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_cli - (MTO_TOTAL - INTERES)

                    If Pagos.Pagador = "D" Then
                        Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_ddr = Coll_Doctos_Seleccionados.Item(Ind_doc).doc_sdo_ddr - MTO_TOTAL
                    End If

                    If Saldo_DPO <= 0 Then
                        GoTo Ciclo_DPO
                    End If

                    ind_doc_aux += 1

                Next
Ciclo_DPO:
            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
