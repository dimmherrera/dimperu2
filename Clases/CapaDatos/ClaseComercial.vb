Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports System.Transactions
Imports CapaDatos
Public Class ClaseComercial
    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim CD As New CapaDatos.ClaseControlDual
    Dim CG As New CapaDatos.ConsultasGenerales
    Dim ag As New ActualizacionesGenerales

    Private pdescripcion As String
    Public Property descripcion() As String
        Get
            Return pdescripcion
        End Get
        Set(ByVal value As String)
            pdescripcion = value
        End Set
    End Property


#Region "Consultas Comerciales"

#Region "Modulo Control Ejecutivos"

    Public Function EvaluacionFlujo_Devuelve(ByVal Eje_Desde As Integer, ByVal Eje_Hasta As Integer, _
                                            ByVal Cli_Desde As Long, ByVal Cli_Hasta As Long, _
                                            ByVal Fec_Desde As DateTime, ByVal Fec_Hasta As DateTime) As Collection
        Try

            '*********************************************************************************************************************************
            'Descripcion: Devuelve las evaluacion y en que parte del flujo va la operacion
            'Creado por= Jorge Lagos C.
            'Fecha Creacion: 17/04/2009
            'Quien Modifica              Fecha              Descripcion
            'JLagos                     26-04-2012          se ordena por fecha de evaluacion (descendiente)
            '*********************************************************************************************************************************

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            'ORDEN
            '1.- RUT
            '2.- NOMBRE
            '3.- MONEDA
            '4.- MONTO
            '5.- FECHA


            Dim Sesion As New ClsSession.ClsSession

            Dim Evaluaciones = (From E In Data.eva_cls Where (E.id_eje >= Eje_Desde And E.id_eje <= Eje_Hasta) And _
                                                            (E.cli_idc >= Format(Cli_Desde, Var.FMT_RUT) And _
                                                             E.cli_idc <= Format(Cli_Hasta, Var.FMT_RUT)) And _
                                                            (E.eva_fec_cre >= Format(Fec_Desde, "dd/MM/yyy") & " 00:00:00" And _
                                                             E.eva_fec_cre <= Format(Fec_Hasta, "dd/MM/yyy") & " 23:59:59") _
                                                             Order By E.eva_fec_cre Descending _
                               Select New With {E.id_eva, _
                                                 .RutCli = E.cli_idc, _
                                                 .Razon = If(E.cli_cls.id_P_0044 = 1, _
                                                              E.cli_cls.cli_rso.Trim & " " & E.cli_cls.cli_ape_ptn.Trim & " " & E.cli_cls.cli_ape_mtn.Trim, _
                                                              E.cli_cls.cli_rso.Trim), _
                                                 .id_Mon = E.id_P_0023, _
                                                 .Moneda = E.P_0023_cls.pnu_atr_003, _
                                                 .Fecha = E.eva_fec_cre, _
                                                 .Estado = E.id_P_0110, _
                                                 .Sucursal = E.cli_cls.id_suc, _
                                                 .Ejecutivo = E.eje_cls.eje_des_cra, _
                                                 .APB = 0, _
                                                 .Neg = 0, _
                                                 .Sim = 0, _
                                                 .Oto = 0, _
                                                 .Monto = 0.0, _
                                                 .PorAnt = E.eva_por, _
                                                 .Tasa = 0.0, _
                                                 .Plazo = 0.0}).Skip(Sesion.NroPaginacion)
            If NroRow = 0 Then
                NroRow = Evaluaciones.Count
            End If


            For Each T In Evaluaciones.Take(8)

                Try
                    T.Monto = (From x In Data.exd_cls Where x.id_eva = T.id_eva Select x.mto_eva).Sum
                Catch ex As Exception
                    T.Monto = 0
                End Try

                Select Case T.Estado

                    Case 2 'Trae N° de Negociacion

                        Try

                            Dim neg As opn_cls = NegociacionPorId_Eva_Devuelve(T.id_eva)

                            T.Neg = neg.id_opn
                            T.Tasa = neg.opn_tas_neg
                            T.Plazo = neg.opn_fev.Value.Subtract(neg.opn_fec).Days

                        Catch ex As Exception
                            T.Neg = 0
                        End Try


                    Case 3 'Trae N° de Simulacion

                        Dim neg As opn_cls = NegociacionPorId_Eva_Devuelve(T.id_eva)

                        Try
                            T.Neg = neg.id_opn
                            T.Tasa = neg.opn_tas_neg
                            T.Plazo = neg.opn_fev.Value.Subtract(neg.opn_fec).Days

                        Catch ex As Exception
                            T.Neg = 0
                            T.Sim = 0
                        End Try

                        Try

                            Dim ope As ope_cls = OperacionPorId_opn_Devuelve(neg.id_opn)

                            T.Sim = ope.id_ope
                            T.Tasa = ope.opn_cls.opn_tas_neg
                            T.Plazo = ope.opn_cls.opn_fev.Value.Subtract(ope.opn_cls.opn_fec).Days

                        Catch ex As Exception
                            T.Sim = 0
                        End Try

                    Case 4 'Trae N° de Otorgamiento

                        Try

                            Dim neg As opn_cls = NegociacionPorId_Eva_Devuelve(T.id_eva)
                            Dim ope As ope_cls = OperacionPorId_opn_Devuelve(neg.id_opn)

                            T.Neg = neg.id_opn
                            T.Sim = ope.id_ope
                            T.Tasa = neg.opn_tas_neg
                            T.Plazo = ope.opn_cls.opn_fev.Value.Subtract(ope.opn_cls.opn_fec).Days

                        Catch ex As Exception
                            T.Neg = 0
                            T.Sim = 0
                            T.Oto = 0
                        End Try

                        Try
                            T.Oto = OperacionPorId_ope_Devuelve(T.Sim).id_ope
                        Catch ex As Exception
                            T.Oto = 0
                        End Try

                End Select

                'Si tienes todas sus aprobaciones cumplidas 
                If CD.ValidaAprobaciones(T.Neg, T.Sucursal) Then
                    T.APB = 1
                End If

                Coll.Add(T)

            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function NegociacionPorId_Eva_Devuelve(ByVal id_eva As Integer) As opn_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las evaluacion y en que parte del flujo va la operacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 17/04/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            '2=En Negociacion 
            Dim Negociacion As opn_cls = (From N In Data.opn_cls Where N.id_eva = id_eva And _
                                                                      (N.eva_cls.id_P_0110 = 2 Or _
                                                                       N.eva_cls.id_P_0110 = 3 Or _
                                                                       N.eva_cls.id_P_0110 = 4) And _
                                                                       N.id_P_0082 > 0).First
            Return Negociacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function OperacionPorId_opn_Devuelve(ByVal id_opn As Integer) As ope_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las evaluacion y en que parte del flujo va la operacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 17/04/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            '3 = Simulada 
            Dim Operacion As ope_cls = (From O In Data.ope_cls Where O.id_opn = id_opn And _
                                                                    (O.opn_cls.id_P_0082 = 2 Or _
                                                                     O.opn_cls.id_P_0082 = 3 Or _
                                                                     O.opn_cls.id_P_0082 = 4)).First

            Return Operacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function OperacionPorId_ope_Devuelve(ByVal id_ope As Integer) As opo_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las evaluacion y en que parte del flujo va la operacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 17/04/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            '3 = Otorgada
            Dim Operacion As opo_cls = (From O In Data.opo_cls Where O.id_ope = id_ope And _
                                                                    (O.ope_cls.id_P_0030 = 2 Or _
                                                                     O.ope_cls.id_P_0030 = 3 Or _
                                                                     O.ope_cls.id_P_0030 = 4)).First

            Return Operacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    Public Function RetornaResponsabilidad(ByVal ope As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext

            Dim Responsabilidad As Char = (From o In data.opn_cls Where o.id_opn = ope Select o.opn_res_son).First

            If Responsabilidad = "1" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function NegociacionRetorna(ByVal Numero As Integer) As opn_cls

        '**************************************************************************************************************************************************
        'Descripcion: 
        'Creado por Victor Alvarez Rojas
        'Fecha Creacion: 02/04/2012
        'retorna evaluacion para reportes
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Negociacion As opn_cls = (From O In Data.opn_cls Where O.id_opn = Numero).First


            Return Negociacion


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function NegociacionValidaModificaciones() As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la tasa maxima convencional
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function TasaMaximaConvencionalDevuelve() As tmc_cls
        '**************************************************************************************************************************************************
        'Descripcion: devuelve la tasa maxima convencional
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext

            Dim TMC As tmc_cls = (From T In Data.tmc_cls Where T.tmc_est = "A").First


            Return TMC

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function TasaRetorna(ByVal TipoConsulta As Integer, ByVal RutCliente As Long, ByVal NroOpo As Integer) As Decimal

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve la tasa de un cliente segun tipo de consulta
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 18/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim data As New DataClsFactoringDataContext
            Dim Tasa As Decimal

            Select Case TipoConsulta

                Case 1 'Retorna la tasa del negocio

                    Try

                        Tasa = (From O In data.opo_cls Where O.id_opo = NroOpo _
                                And O.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                               Select TasaNegocio = O.ope_cls.opn_cls.opn_tas_neg).First

                    Catch ex As Exception
                        Tasa = 0
                    End Try

                Case 2 'Retorna la tasa del cliente

                    Try

                        Tasa = (From C In data.cli_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) Select C.cli_tas_mor).First

                        If Tasa = 0 Then

                            Tasa = (From C In data.cli_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) Select C.cli_tas_mor_aux).First

                            If Tasa = 0 Then
                                Tasa = TasaMaximaConvencionalDevuelve.tmc_mor
                            
                            End If
                        Else '21-11-2014 se agrega que si la tasa mora asignada al cliente, tome la TML
                            If Tasa > TasaMaximaConvencionalDevuelve.tmc_mor Then
                                Tasa = TasaMaximaConvencionalDevuelve.tmc_mor
                            End If
                        End If


                    Catch ex2 As Exception

                        Try
                            Tasa = (From C In data.cli_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) Select C.cli_tas_mor_aux).First

                            If Tasa = 0 Then
                                Tasa = TasaMaximaConvencionalDevuelve.tmc_mor
                            End If

                        Catch ex1 As Exception
                            Tasa = TasaMaximaConvencionalDevuelve.tmc_mor

                        End Try
                    End Try

            End Select

            Return Tasa

        Catch ex As Exception

        End Try

    End Function

    Public Function ResumenClienteDevuelve(ByVal RutCliente As Long, ByVal CodEjecutivo As Integer) As Object
        '**************************************************************************************************************************************************
        'Descripcion: devuelve el resumen del cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion

        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            'Dim RSC As rsc_cls = (From R In Data.rsc_cls Where R.cli_idc = Format(RutCliente, Var.FMT_RUT) _
            '                                               And R.cli_cls.id_eje_cod_eje = CodEjecutivo).First

            Dim RSC = (From R In Data.rsc_cls _
                 Group Join D In Data.dsb_cls On R.cli_idc Equals D.cli_idc _
                             Into Monto_Mora = Sum(D.dsb_mor_001 + D.dsb_mor_002 + D.dsb_mor_003 + D.dsb_mor_004 + D.dsb_mor_005), _
                                  Monto_Venc = Sum(D.dsb_ven_001 + D.dsb_ven_002 + D.dsb_ven_003 + D.dsb_ven_004 + D.dsb_ven_005) _
                      Where R.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                         Select R.id_rsc, R.cli_idc, _
                                R.rsc_col_001, R.rsc_col_002, R.rsc_col_003, R.rsc_col_004, R.rsc_col_005, R.rsc_col_006, _
                                R.rsc_col_007, R.rsc_col_008, R.rsc_col_009, R.rsc_col_010, R.rsc_col_011, R.rsc_col_012, _
                                R.rsc_col_tot, R.rsc_ddr_ctd, R.rsc_ctd_doc, R.rsc_prm_dia_pag, _
                                R.rsc_mto_prt, R.rsc_mto_cbz, R.rsc_mto_cxc, R.rsc_mto_exd, R.rsc_mto_cxp, R.rsc_mto_dnc, R.rsc_mto_int_dev, R.rsc_mto_ocu, _
                                R.rsc_ctd_mor, R.rsc_ctd_prt, R.rsc_ctd_cbz, R.rsc_ctd_dnc, R.rsc_pro, _
                                R.rsc_mto_vig, R.rsc_mto_cob_jud, R.rsc_mto_cnj_mpl, R.rsc_mto_cnj_opl, R.rsc_mto_che_prt, R.rsc_mto_let_prt, R.rsc_mto_pgr_prt, R.rsc_ctd_vig, _
                                R.rsc_ctd_mpl, R.rsc_ctd_opl, R.rsc_ctd_che_prt, R.rsc_ctd_let_prt, R.rsc_ctd_pgr_prt, _
                                R.rsc_mto_ant, R.rsc_ctd_ope, R.rsc_mto_pgr_vig, R.rsc_ctd_doc_cob_jud, R.rsc_ctd_pgr_vig, R.rsc_mto_int_cal, _
                                R.rsc_mto_let_mor, R.rsc_ctd_let_mor, R.rsc_ctd_vig_500, R.rsc_ctd_mor_500, R.rsc_mto_mor_otr, _
                                R.rsc_deu_efa, R.rsc_can_doc_efa, R.rsc_sdo_exc, R.pro_dia_fac_vig, R.pro_dia_let_vig, R.pro_dia_otr_vig, _
                                R.rsc_deu_cli_fac, R.rsc_deu_cli_let, R.rsc_deu_cli_otr, R.rsc_doc_pro_fac, _
                                R.rsc_doc_pro_let, R.rsc_doc_pro_otr, R.rsc_deu_ven_fac, R.rsc_deu_ven_let, R.rsc_deu_ven_otr, _
                                Monto_Mora, Monto_Venc).First

            'And R.cli_cls.id_eje_cod_eje = CodEjecutivo)

            Return RSC

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DistribucionMorasaCliente_Devuelve(ByVal RutCliente As Long, ByVal CodEjecutivo As Integer) As Object

        '**************************************************************************************************************************************************
        'Descripcion: devuelve el resumen del cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 23/03/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Distribucion = From R In Data.dsb_cls Where R.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                               And R.cli_cls.id_eje_cod_eje = CodEjecutivo _
                                               Group By Rut = R.cli_idc Into Monto_Mora = Sum(R.dsb_mor_001 + R.dsb_mor_002 + R.dsb_mor_003 + R.dsb_mor_004 + R.dsb_mor_005), _
                                                                             Monto_Venc = Sum(R.dsb_ven_001 + R.dsb_ven_002 + R.dsb_ven_003 + R.dsb_ven_004 + R.dsb_ven_005) _
                               Select Rut, Monto_Mora, Monto_Venc

            Return Distribucion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ResumenClienteDeudorDevuelve(ByVal RutCliente As Long, ByVal RutDeudor As Long) As Object

        '**************************************************************************************************************************************************
        'Descripcion: devuelve el resumen del cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim RSD = From R In Data.rsd_cls Where R.ddr_cls.cli_idc <> Format(RutCliente, Var.FMT_RUT) _
                                                                                  And R.ddr_cls.deu_ide = Format(RutDeudor, Var.FMT_RUT)


            If RSD.Count > 0 Then
                Return RSD
            Else
                Return Nothing
            End If


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function InteresesCalculadosDevuelve(ByVal RutCliente As Long, ByVal CodEjecutivo As Integer, ByVal Fecha As DateTime) As ArrayList

        '**************************************************************************************************************************************************
        'Descripcion: Inserta una nueva operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 24/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext
            Dim TasaMaxConv As Decimal
            Dim TasaPorcentaje As Decimal
            Dim Paridad As Decimal
            Dim mto_int_cal As Double
            Dim int_cal_doc As Double
            Dim int_cal_let As Double
            Dim Obj_Intereses As Object
            Dim Coll_Intereses As New Collection
            Dim doc_sdo_cli As Double
            Dim ctd_ful_pgo As Integer
            Dim ctd_fec_rea As Integer
            Dim moneda As Integer
            Dim id_P_0031 As Integer
            Dim tas_mor_cli As Decimal
            Dim Arr_Interes As New ArrayList




            TasaMaxConv = TasaMaximaConvencionalDevuelve.tmc_val
            TasaPorcentaje = (TasaMaxConv / 100) / 30

            Dim InteresesCalculados = From Doc In Data.doc_cls _
                                                 Where Doc.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                                    And Doc.dsi_cls.id_P_0011 = 2 _
                                                    And Doc.dsi_cls.id_P_0011 = 2 _
                                                    And Doc.dsi_cls.dsi_flj = "N" _
                                                    And Doc.doc_sdo_cli >= 0

            'Cambiar 
            'Doc.id_P_0011 = 2 y 4


            For Each I In InteresesCalculados

                mto_int_cal = 0

                doc_sdo_cli = I.doc_sdo_cli * CG.ParidadDevuelve(I.opo_cls.ope_cls.opn_cls.id_P_0023, Fecha).par_val

                If Not IsNothing(I.doc_ful_pgo) Then
                    ctd_ful_pgo = DateDiff(DateInterval.Day, Date.Now, CDate(I.doc_ful_pgo))
                Else
                    ctd_ful_pgo = DateDiff(DateInterval.Day, CDate("01/01/1900"), Date.Now)
                End If

                If Not IsNothing(I.dsi_cls.dsi_fev_rea) Then
                    ctd_fec_rea = DateDiff(DateInterval.Day, CDate(I.dsi_cls.dsi_fev_rea), Date.Now)
                Else
                    ctd_fec_rea = DateDiff(DateInterval.Day, CDate("01/01/1900"), Date.Now)
                End If

                moneda = I.opo_cls.ope_cls.opn_cls.id_P_0023
                id_P_0031 = I.opo_cls.ope_cls.opn_cls.id_P_0031
                tas_mor_cli = I.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_tas_mor

                If ctd_ful_pgo > ctd_fec_rea Then

                    If tas_mor_cli = 0 Then
                        mto_int_cal = mto_int_cal + (I.doc_sdo_cli * ((TasaMaxConv / 100) / 30) * ctd_fec_rea)
                    Else
                        mto_int_cal = mto_int_cal + (I.doc_sdo_cli * ((tas_mor_cli / 100) / 30) * ctd_fec_rea)
                    End If

                Else

                    If tas_mor_cli = 0 Then
                        mto_int_cal = mto_int_cal + (I.doc_sdo_cli * ((TasaMaxConv / 100) / 30) * ctd_ful_pgo)
                    Else
                        mto_int_cal = mto_int_cal + (I.doc_sdo_cli * ((tas_mor_cli / 100) / 30) * ctd_ful_pgo)
                    End If

                End If

                If id_P_0031 <> 2 Then
                    int_cal_doc += mto_int_cal
                Else
                    int_cal_let += mto_int_cal
                End If

            Next

            Arr_Interes.Add(int_cal_doc)
            Arr_Interes.Add(int_cal_let)

            Return Arr_Interes

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function VisitasDevuelve(ByVal RutCliente As Long) As vst_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve el ultima visita segun su mayor nº de visita
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext
            Dim Visitas As vst_cls = (From V In Data.vst_cls Where V.cli_idc = Format(RutCliente, Var.FMT_RUT) Order By V.id_vst Descending).First

            Return Visitas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function OperacionConProblemasCobranzaDevuelve(ByVal RutCliente As Long) As ArrayList

        '**************************************************************************************************************************************************
        'Descripcion: devuelve las operaciones con problemas 460
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim mto_sld_cli_cod_may_460 As Double
            Dim num_doc_cli_cod_may_460 As Double
            Dim num_deu_cli_cod_may_460 As Double
            Dim Arr_Datos As New ArrayList

            Dim OpoProCob = From D In Data.doc_cls Where D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                                     And D.dsi_cls.dsi_flj = "N" _
                                                     And ((D.dsi_cls.id_P_0011 = 2 _
                                                    Or D.dsi_cls.id_P_0011 = 4 _
                                                    Or D.dsi_cls.id_P_0011 = 9 _
                                                    Or D.dsi_cls.id_P_0011 = 11 _
                                                    Or D.dsi_cls.id_P_0011 = 12) Or _
                                                    (D.dsi_cls.id_P_0011 = 1 And D.id_cco > 23 And D.id_cco < 39)) _
                            Select D.dsi_cls.id_P_0011, _
                                   cco_num = D.id_cco, _
                                   D.doc_sdo_cli, _
                                   D.opo_cls.ope_cls.opn_cls.id_P_0023, _
                                   D.dsi_cls.deu_ide
            'Or (D.dsi_cls.id_P_0011 = 1 And D.cco_cls.cco_num > "0500" And D.cco_cls.cco_num < "9999")


            Dim array As New ArrayList
            For Each D In OpoProCob

                'If (D.id_P_0011 = 2 Or D.id_P_0011 = 4 Or D.id_P_0011 = 9 Or D.id_P_0011 = 11 Or D.id_P_0011 = 12) Or _
                '    (D.id_P_0011 = 1 And D.cco_num > 23 And D.cco_num < 39) Then

                mto_sld_cli_cod_may_460 += D.doc_sdo_cli * CG.ParidadDevuelve(D.id_P_0023, Date.Now.ToShortDateString).par_val
                num_doc_cli_cod_may_460 += 1


                If array.Contains(D.deu_ide) = False Then
                    array.Add(D.deu_ide)
                End If


                'End If

            Next
            num_deu_cli_cod_may_460 = array.Count
            Arr_Datos.Add(mto_sld_cli_cod_may_460)
            Arr_Datos.Add(num_doc_cli_cod_may_460)
            Arr_Datos.Add(num_deu_cli_cod_may_460)

            Return Arr_Datos

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DeudorDeudaDevuelve(ByVal RutCliente As Long, ByVal RutDeudor As Long) As ArrayList

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la Deuda del deudor monto utilizado y monto ocupado
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     07-06-2010          Se agrega valida de montos cuando estos son nothing
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Arr_Mtos As New ArrayList


            Dim rsd_mto_ocu = Aggregate D In Data.rsd_cls Where D.ddr_cls.deu_ide = Format(RutDeudor, Var.FMT_RUT) Into Sum(D.rsd_sdo_ddr)

            Dim rsd_mto_ocu2 = Aggregate D In Data.rsd_cls Where D.ddr_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                                    And D.ddr_cls.deu_ide = Format(RutDeudor, Var.FMT_RUT) _
                                                    Into Sum(D.rsd_sdo_ddr)

            If IsNothing(rsd_mto_ocu) Then
                rsd_mto_ocu = 0
            End If

            If IsNothing(rsd_mto_ocu2) Then
                rsd_mto_ocu2 = 0
            End If


            Arr_Mtos.Add(rsd_mto_ocu2)
            Arr_Mtos.Add(rsd_mto_ocu)

            Return Arr_Mtos

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub NegociacionesAnterioresDevuelve(ByVal RutCliente As Long, ByVal GV As GridView)

        '**************************************************************************************************************************************************
        'Descripcion: devuelve todas las negociaciones anteriores de un cliente en particular que estes ingresadas o asociadas
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 08/08/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     10-07-2009         -Se cambia el estado a 2 (ingresada o asociada)
        'A Saldivar                 07/01/2011         -Se agrega paginacion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim coll As New Collection

            Dim NegociacionesAnteriores = (From O In Data.opn_cls _
                                           Where O.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                          And O.id_P_0082 <= 2 _
                              Order By O.opn_fec Descending _
                                          Select NroNeg = O.id_opn, _
                                                 FechaNeg = O.opn_fec_neg.Value.ToShortDateString, _
                                                 FechaVcto = O.opn_fev.Value.ToShortDateString, _
                                                 MtoNeg = O.opn_mto_doc, _
                                                 FechaIng = O.opn_fec, _
                                                 PorAnt = O.opn_por_ant, _
                                                 CanDeu = O.opn_can_ddr, _
                                                 CanDoc = O.opn_can_doc, _
                                                 TipDoc = O.P_0031_cls.pnu_des, _
                                                 id_Moneda = O.id_P_0023, _
                                                 Moneda = O.P_0023_cls.pnu_des, _
                                                 Estado = O.P_0082_cls.pnu_des, _
                                                 FechaVctoReal = O.opn_fev_ori.Value.ToShortDateString).Skip(sesion.NroPaginacion)

            For Each x In NegociacionesAnteriores.Take(15)
                coll.Add(x)
            Next

            GV.DataSource = coll
            GV.DataBind()

            'GV.DataSource = NegociacionesAnteriores
            'GV.DataBind()

            Dim I As Integer = 0

            For Each Neg In NegociacionesAnteriores

                Select Case Neg.id_Moneda
                    Case 1
                        GV.Rows(I).Cells(7).Text = Format(Neg.MtoNeg, Fmt.FCMSD)
                    Case 2
                        GV.Rows(I).Cells(7).Text = Format(Neg.MtoNeg, Fmt.FCMCD4)
                    Case 3, 4
                        GV.Rows(I).Cells(7).Text = Format(Neg.MtoNeg, Fmt.FCMCD)
                End Select
                I += 1

            Next


        Catch ex As Exception

        End Try

    End Sub

    Public Sub NegociacionesAnterioresDevuelve(ByVal RutCliente As Long, ByVal GV As GridView, _
                                               ByVal Fecha_Desde As String, ByVal Fecha_Hasta As String, _
                                               ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer)

        '**************************************************************************************************************************************************
        'Descripcion: devuelve todas las negociaciones anteriores de un cliente en particular que estes ingresadas o asociadas
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 08/08/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     10-07-2009         -Se cambia el estado a 2 (ingresada o asociada)
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim NegociacionesAnteriores = From O In Data.opn_cls Where O.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                          (O.id_P_0082 >= Estado_Desde And O.id_P_0082 <= Estado_Hasta) And _
                                          (O.opn_fec_neg >= Fecha_Desde And O.opn_fec_neg <= Fecha_Hasta) _
                                          Order By O.opn_fec Descending _
                                          Select NroNeg = O.id_opn, _
                                                 FechaNeg = O.opn_fec_neg.Value.ToShortDateString, _
                                                 FechaVcto = O.opn_fec.Value.ToShortDateString, _
                                                 MtoNeg = O.opn_mto_doc, _
                                                 FechaIng = O.opn_fec, _
                                                 PorAnt = O.opn_por_ant, _
                                                 CanDeu = O.opn_can_ddr, _
                                                 CanDoc = O.opn_can_doc, _
                                                 TipDoc = O.P_0031_cls.pnu_des, _
                                                 id_Moneda = O.id_P_0023, _
                                                 Moneda = O.P_0023_cls.pnu_des, _
                                                  Estado = O.P_0082_cls.pnu_des


            GV.DataSource = NegociacionesAnteriores
            GV.DataBind()

            Dim I As Integer = 0

            For Each Neg In NegociacionesAnteriores

                Select Case Neg.id_Moneda
                    Case 1
                        GV.Rows(I).Cells(6).Text = Format(Neg.MtoNeg, Fmt.FCMSD)
                    Case 2
                        GV.Rows(I).Cells(6).Text = Format(Neg.MtoNeg, Fmt.FCMCD4)
                    Case 3, 4
                        GV.Rows(I).Cells(6).Text = Format(Neg.MtoNeg, Fmt.FCMCD)
                End Select
                I += 1

            Next


        Catch ex As Exception

        End Try

    End Sub

    Public Sub NegociacionesTodasDevuelve(ByVal GV As GridView)

        '**************************************************************************************************************************************************
        'Descripcion: devuelve todas las negociaciones anteriores de un cliente en particular
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 08/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim NegociacionesAnteriores = From O In Data.opn_cls Where O.id_P_0082 = 1 Order By O.id_opn Descending _
                                          Select NroNeg = O.id_opn, _
                                                         Rut = CInt(O.eva_cls.cli_idc), _
                                                         O.eva_cls.cli_cls.cli_dig_ito, _
                                                   Cliente = O.eva_cls.cli_cls.cli_rso.Trim & " " & O.eva_cls.cli_cls.cli_ape_ptn.Trim, _
                                                FechaNeg = O.opn_fec_neg.Value.ToShortDateString, _
                                                FechaVcto = O.opn_fec.Value.ToShortDateString, _
                                                   MtoNeg = O.opn_mto_doc, _
                                                 FechaIng = O.opn_fec, _
                                                    PorAnt = O.opn_por_ant, _
                                                   CanDeu = O.opn_can_ddr, _
                                                   CanDoc = O.opn_can_doc, _
                                                   TipDoc = O.P_0031_cls.pnu_des, _
                                                   Moneda = O.P_0023_cls.pnu_des, _
                                                    Estado = O.P_0082_cls.pnu_des


            GV.DataSource = NegociacionesAnteriores
            GV.DataBind()

            For I = 0 To GV.Rows.Count - 1
                GV.Rows(I).Cells(1).Text = RG.FormatoMiles(GV.Rows(I).Cells(1).Text) & "-" & Negociacion(I + 1).cli_dig_ito
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Function NegociacionDevuelve(ByVal RutCliente As Long, ByVal NroNegociacion As Integer) As opn_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve una negociacion por su nro y cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 11/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Negociacion As opn_cls = (From O In Data.opn_cls Where O.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And O.id_opn = NroNegociacion).First


            Return Negociacion


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function NegociacionDevuelvexEva(ByVal NroEva As Integer) As opn_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve una negociacion por su nro y cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 11/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Negociacion As opn_cls = (From O In Data.opn_cls Where O.id_eva = NroEva).First


            Return Negociacion


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DatosDiariosDevuelve(ByVal Fecha As DateTime) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las zonas asociados a una comuna y sucursal
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 25/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try
            'Dim Data As New DataClsFactoringDataContext
            Dim Data As New DataClsFactoringDataContext
            Dim Coll_Datos As New Collection

            Try

                Dim UF As par_cls = (From U In Data.par_cls Where U.id_P_0023 = 2 And U.par_fec = Fecha.ToShortDateString).First

                Coll_Datos.Add(UF.par_val)

            Catch ex As Exception
                Coll_Datos.Add(0)
            End Try

            Try

                Dim US As par_cls = (From U In Data.par_cls Where U.id_P_0023 = 3 And U.par_fec = Fecha.ToShortDateString).First

                Coll_Datos.Add(US.par_val)

            Catch ex As Exception
                Coll_Datos.Add(0)
            End Try

            Try
                'Dim TMC As tmc_cls = (From T In Data.tmc_cls Where T.tmc_est = "A").First

                Dim TMC As tmc_cls = (From T In Data.tmc_cls Where T.tmc_est = "A").First
                'Dim TMC As tmc_cls = From T In Data.tmc_cls Where T.tmc_est = "A"

                Coll_Datos.Add(TMC.tmc_val)

            Catch ex As Exception
                Coll_Datos.Add(0)
            End Try

            Return Coll_Datos

        Catch ex As Exception

        End Try

    End Function

    Public Function GastosDefinidosDevuelve(ByVal Id_suc As Integer) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas los gastos
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 12/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll_Datos As New Collection

            Dim Gastos = From G In Data.gto_cls Where G.id_suc = Id_suc And G.gto_est = "A" _
                                Select Codigo = G.id_gto, _
                                       G.id_P_0036, _
                                         Tipo = G.P_0036_cls.pnu_des, _
                                        Monto = G.gto_mto, _
                                  Descripcion = G.gto_des, _
                                          iva = G.gto_iva.Trim()

            Return Gastos

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DiaHabilDevuelve(ByVal FECHA_VCTO As String) As String

        '*********************************************************************************************************************************
        'Descripcion: Devuelve dia habil en el caso que el dia sea feriado o fin de semana
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 12/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Dim var As New FuncionesGenerales.Variables
        Dim CICLO As Boolean

        Try

            CICLO = True

            While CICLO = True

                If UCase(WeekdayName(Weekday(FECHA_VCTO, Microsoft.VisualBasic.FirstDayOfWeek.Monday), , Microsoft.VisualBasic.FirstDayOfWeek.Monday)) <> "SATURDAY" And UCase(WeekdayName(Weekday(FECHA_VCTO, Microsoft.VisualBasic.FirstDayOfWeek.Monday), , Microsoft.VisualBasic.FirstDayOfWeek.Monday)) <> "SÁBADO" _
                And UCase(WeekdayName(Weekday(FECHA_VCTO, Microsoft.VisualBasic.FirstDayOfWeek.Monday), , Microsoft.VisualBasic.FirstDayOfWeek.Monday)) <> "SUNDAY" And UCase(WeekdayName(Weekday(FECHA_VCTO, Microsoft.VisualBasic.FirstDayOfWeek.Monday), , Microsoft.VisualBasic.FirstDayOfWeek.Monday)) <> "DOMINGO" Then


                    Dim Data As New DataClsFactoringDataContext

                    Dim Feriado = From F In Data.fer_cls Where F.fer_fec = FECHA_VCTO

                    If Feriado.Count > 0 Then
                        FECHA_VCTO = DateAdd("D", 1, FECHA_VCTO)
                    Else
                        Return FECHA_VCTO 'Format(FECHA_VCTO, "dd/mm/yyyy")
                        CICLO = False
                    End If

                Else
                    FECHA_VCTO = DateAdd("D", 1, FECHA_VCTO)
                End If

            End While

            Return Nothing

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function TasaPlazosDevuelve(ByVal Moneda As Integer, ByVal Dias As Integer) As typ_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve tasas y plazos para un tipo de moneda y dias
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 12/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim TYP As typ_cls = (From T In Data.typ_cls Where T.id_P_0023 = Moneda And T.typ_dde <= Dias And T.typ_hta >= Dias And T.typ_est = "A").First

            Return TYP

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function UltimaTasaAplicadaDevuelve(ByVal RutCliente As Long) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las ultimas tasas aplicadas a una operacion de un clinete especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 12/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion    
        'jlagos                     08-08-2013          -se agrega la tasa de negocio
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll_Tasas As New Collection


            Dim Tasas = From O In Data.opo_cls Where O.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                     O.ope_cls.id_P_0030 <> 6 _
                        Order By O.id_opo Descending _
                              Select Base = O.ope_cls.opn_cls.opn_tas_bas, _
                                     Spread = O.ope_cls.opn_cls.opn_spr_ead, _
                                     Puntos = O.ope_cls.opn_cls.opn_pto_spr, _
                                     TasaNegocio = O.ope_cls.opn_cls.opn_tas_neg

            If Tasas.Count > 0 Then

                For Each T In Tasas
                    Coll_Tasas.Add(T.Base)
                    Coll_Tasas.Add(T.Spread)
                    Coll_Tasas.Add(T.Puntos)
                    Coll_Tasas.Add(T.TasaNegocio)
                    Exit For
                Next

            Else
                Coll_Tasas.Add(0)
                Coll_Tasas.Add(0)
                Coll_Tasas.Add(0)
                Coll_Tasas.Add(0)
            End If

            Return Coll_Tasas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DiasDeRetencionDevuelve(ByVal id_Sucursal As Integer, ByVal id_Plaza As String, ByVal TipoDocumento As Integer) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los dias de retencion para un tipo de documento
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 17/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Dias1 = From P In Data.P_0031_cls Where P.id_P_0031 = TipoDocumento And P.pnu_atr_002 = "T"


            If Dias1.Count = 0 Then

                Dim Dias2 = From A In Data.pds_cls Where A.id_suc = id_Sucursal And A.id_PL_000047 = id_Plaza _
                            Select pds_ret = A.pds_ret, _
                              pnu_atr_005 = (From P In Data.P_0031_cls Where P.id_P_0031 = TipoDocumento)

                For Each D In Dias2
                    Coll.Add(D.pds_ret)
                    Coll.Add(D.pnu_atr_005)
                Next

            Else

                For Each D In Dias1
                    Coll.Add(D.pnu_atr_001)
                    Coll.Add(D.pnu_atr_005)
                Next

            End If


            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DiasDeRetencionDoctoPagoDevuelve(ByVal id_Sucursal As Integer, ByVal id_Plaza As String) As pds_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los dias de retencion de pagos
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 17/12/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Dias As pds_cls = (From A In Data.pds_cls Where A.id_suc = id_Sucursal And A.id_PL_000047 = id_Plaza).First

            Return Dias

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DatosDeSistemaDevuelve() As sis_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos del sistema como el iva, etc.
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Sistema As sis_cls = (From S In Data.sis_cls).First

            Return Sistema

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ImpuestoDevuelve() As tim_cls


        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos del sistema como el iva, etc.
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Impuesto As tim_cls = (From I In Data.tim_cls).First

            Return Impuesto

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GastosDeNegociacionDevuelve(ByVal id_opn As Integer, ByVal TipoDeGasto As Int16) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los gastos definidos de una negociacion
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 22/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Select Case TipoDeGasto

                Case 1
                    'Gastos Definidos
                    Dim GasDef = From G In Data.gdn_cls Where G.id_opn = id_opn Group By G.id_gto, _
                                                                                         gto_des = G.gto_cls.gto_des, _
                                                                                         gto_est = G.gto_cls.gto_est, _
                                                                                         gto_mto = G.gto_cls.gto_mto, _
                                                                                         id_p_0036 = G.gto_cls.id_P_0036, _
                                                                                         Descrip = G.gto_cls.P_0036_cls.pnu_des, _
                                                                                         AfectoExento = G.gto_cls.gto_iva _
                                                                                         Into Suma = Sum(G.gto_cls.gto_mto) _
                                Select id_gto, gto_des, gto_est, gto_mto, Suma, id_p_0036, AfectoExento

                    Return GasDef

                Case 2
                    'Gastos Definidos
                    Dim GasFij = From G In Data.gfn_cls Where G.id_opn = id_opn

                    Return GasFij

            End Select



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Evaluaciones_Devuelve(ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                          ByVal RutCliente As Long, ByVal GV As GridView, _
                                          Optional ByVal LlenaGrilla As Boolean = True) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Llena grilla con las evaluaciones del cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 27/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion     
        'JLAGOS                     25-04-2012          no se encuentra evaluacion cuando supera mas de la posicion 15
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim Evaluacion = (From E In Data.exd_cls Where ((E.eva_cls.id_P_0110 >= Estado_Desde And E.eva_cls.id_P_0110 <= Estado_Hasta) Or _
                                                            (E.eva_cls.id_P_0110 <> 4 And E.eva_cls.id_P_0110 <> 5)) And _
                                                             E.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                  Group By Codigo = E.id_eva, _
                                       Porcentaje = E.eva_cls.eva_por, _
                                       Moneda = E.eva_cls.P_0023_cls.pnu_des, _
                                       Ejecutivo = E.eva_cls.eje_cls.eje_nom, _
                                       Estado = E.eva_cls.P_0110_cls.pnu_des, _
                                       id_p_0110 = E.eva_cls.id_P_0110, _
                                       Id_Moneda = E.eva_cls.id_P_0023, _
                                       Fecha = E.eva_cls.eva_fec_cre _
                                       Into _
                                       Monto = Sum(E.mto_eva), _
                                       Monto_doc = Sum(E.mto_eva_tot), _
                                       Deudores = Count() Order By Codigo Descending).Skip(sesion.NroPaginacion)

            If LlenaGrilla Then

                For Each E In Evaluacion.Take(15)
                    Coll.Add(E)
                Next

            Else

                For Each E In Evaluacion
                    Coll.Add(E)
                Next

            End If

            If LlenaGrilla Then

                GV.DataSource = Coll
                GV.DataBind()

                For I = 0 To GV.Rows.Count - 1

                    Select Case Coll.Item(I + 1).Id_Moneda
                        Case 1
                            GV.Rows(I).Cells(4).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMSD)
                        Case 2
                            GV.Rows(I).Cells(4).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD4)
                        Case 3, 4
                            GV.Rows(I).Cells(4).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD)
                    End Select

                Next

                Return Nothing
            Else
                Return Coll
            End If








        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Evaluaciones_Libres_Devuelve(ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                      ByVal RutCliente As Long, ByVal GV As GridView, _
                                     Optional ByVal LlenaGrilla As Boolean = True) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Llena grilla con las evaluaciones del cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 27/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'JLagos                     02-06-2010          Se agrego como criterio de busqueda que el estado sea Anulada (5)
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Evaluacion = From E In Data.exd_cls Where E.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                         ((E.eva_cls.id_P_0110 >= Estado_Desde And _
                                                           E.eva_cls.id_P_0110 <= Estado_Hasta) Or _
                                                          (E.eva_cls.id_P_0110 <> 4 And E.eva_cls.id_P_0110 <> 5)) _
                              Group By Codigo = E.id_eva, _
                                   Porcentaje = E.eva_cls.eva_por, _
                                   Moneda = E.eva_cls.P_0023_cls.pnu_atr_003, _
                                   Ejecutivo = E.eva_cls.eje_cls.eje_nom, _
                                   Estado = E.eva_cls.P_0110_cls.pnu_des, _
                                   id_P_0110 = E.eva_cls.id_P_0110, _
                                   Id_Moneda = E.eva_cls.id_P_0023, _
                                   Fecha = E.eva_cls.eva_fec_cre _
                                   Into _
                                   Monto = Sum(E.mto_eva), _
                                     Monto_doc = Sum(E.mto_eva_tot), _
                                   Deudores = Count() Order By Codigo Descending


            For Each E In Evaluacion

                'Muestra evaluacion que solo esten asociadas a negociaciones con estado 1
                'Dim o As Integer = (From op In Data.opn_cls Where op.id_eva = E.Codigo And op.id_P_0082 = 1 Select op).Count

                'If o > 0 Then
                '    Coll.Add(E)
                'End If

                Coll.Add(E)

            Next

            If LlenaGrilla Then

                GV.DataSource = Coll
                GV.DataBind()

                For I = 0 To GV.Rows.Count - 1

                    Select Case Coll.Item(I + 1).Id_Moneda
                        Case 1
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMSD)
                        Case 2
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD4)
                        Case 3, 4
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD)
                    End Select

                Next

                Return Nothing
            Else
                Return Coll
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function EvaluacionesServicio_Devuelve(ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                          ByVal RutCliente As Long, ByVal GV As GridView, _
                                          ByVal Fecha_Desde As String, ByVal Fecha_Hasta As String, _
                                          Optional ByVal LlenaGrilla As Boolean = True) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Llena grilla con las evaluaciones del cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 27/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection


            Dim Evaluacion = From E In Data.exd_cls Where E.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                         (E.eva_cls.eva_fec_cre >= Fecha_Desde And _
                                                          E.eva_cls.eva_fec_cre <= Fecha_Hasta) And _
                                                         (E.eva_cls.id_P_0110 >= Estado_Desde And _
                                                          E.eva_cls.id_P_0110 <= Estado_Hasta) _
                              Group By Codigo = E.id_eva, _
                                   Porcentaje = E.eva_cls.eva_por, _
                                   Moneda = E.eva_cls.P_0023_cls.pnu_atr_003, _
                                   Ejecutivo = E.eva_cls.eje_cls.eje_nom, _
                                   Estado = E.eva_cls.P_0110_cls.pnu_des, _
                                   Id_Moneda = E.eva_cls.id_P_0023, _
                                   Fecha = E.eva_cls.eva_fec_cre _
                                   Into _
                                   Monto = Sum(E.mto_eva), _
                                     Monto_doc = Sum(E.mto_eva_tot), _
                                   Deudores = Count() Order By Codigo Descending

            For Each E In Evaluacion
                Coll.Add(E)
            Next

            If LlenaGrilla Then

                GV.DataSource = Coll
                GV.DataBind()

                For I = 0 To GV.Rows.Count - 1

                    Select Case Coll.Item(I + 1).Id_Moneda
                        Case 1
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMSD)
                        Case 2
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD4)
                        Case 3, 4
                            GV.Rows(I).Cells(3).Text = Format(Coll.Item(I + 1).Monto, Fmt.FCMCD)
                    End Select

                Next

                Return Nothing
            Else
                Return Coll
            End If








        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function EvaluacionDevuelvePorId(ByVal Id_Evaluacion As Integer) As eva_cls

        Try

            '*********************************************************************************************************************************
            'Descripcion: Devuelve evaluacion de cliente por su Id
            'Creado por= Jorge Lagos C.                                                                                                                       
            'Fecha Creacion: 24/10/2008                                                                                                                     
            'Quien Modifica              Fecha              Descripcion                                                                                   
            '*********************************************************************************************************************************

            Dim Data As New DataClsFactoringDataContext

            Dim Evaluacion As eva_cls = (From E In Data.eva_cls Where E.id_eva = Id_Evaluacion).First


            Return Evaluacion

        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Function EvaluacionDevuelveDeudoresPorIdEva(ByVal Id_Evaluacion As Integer) As Collection

        Try

            '*********************************************************************************************************************************
            'Descripcion: Devuelve los deudores de una evaluacion por Id de evaluacion
            'Creado por= Jorge Lagos C.                                                                                                                       
            'Fecha Creacion: 24/10/2008                                                                                                                     
            'Quien Modifica              Fecha              Descripcion                                                                                   
            '*********************************************************************************************************************************

            Dim Data As New DataClsFactoringDataContext

            Dim Evaluacion = From E In Data.exd_cls Where E.id_eva = Id_Evaluacion _
                              Select RutDeu = E.deu_rut, _
                                     DigDeu = E.deu_cls.deu_dig_ito, _
                                     NomDeu = E.deu_nom.ToUpper, _
                                     MtoEva = E.mto_eva, _
                                     MtoDoc = E.mto_eva_tot, _
                                     Moneda = E.eva_cls.P_0023_cls.pnu_des, _
                                     PorCli = E.exd_por_cli


            Dim Coll As New Collection

            For Each E In Evaluacion
                Coll.Add(E)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Function LdcDevuelveObjeto(ByVal cli As Long) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Rescata rut de cliente
        'Creado por= A Saldivar.
        'Fecha Creacion: 20/05/2010
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim Ldc As ldc_cls = (From s In Data.ldc_cls Where s.cli_idc = Format(cli, Var.FMT_RUT)).First
            Return Ldc.id_ldc
        Catch ex As Exception
            Return Nothing

        End Try

    End Function


    Public Function SBLDevuelveObjeto(ByVal id_Sbl As Integer, ByVal Id_Doc As Integer) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Rescata monto
        'Creado por= A Saldivar.
        'Fecha Creacion: 20/05/2010
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim SBL As sbl_cls = (From s In Data.sbl_cls Where s.id_ldc = id_Sbl And s.id_P_0031 = Id_Doc).First
            Return SBL.sbl_mto

        Catch ex As Exception
            Return Nothing

        End Try

    End Function


    Public Function SubLineaDevuelveObjeto(ByVal id_ldc As Integer) As Collection
        '*********************************************************************************************************************************
        'Descripcion: Rescata Sub linea/deu
        'Creado por= A Saldivar.
        'Fecha Creacion: 08/11/2010
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim coll As New Collection

            Dim SBL = From s In Data.sbl_cls Where s.id_ldc = id_ldc

            For Each p In SBL
                coll.Add(p)
            Next

            If coll.Count > 0 Then
                Return coll
            End If

        Catch ex As Exception
            Return Nothing

        End Try

    End Function



#End Region

#Region "Actualizaciones Comerciales"

#Region "Modulo Comercial"

    Public Function EvaluacionInsertar(ByVal Eva As eva_cls) As Integer

        '*********************************************************************************************************************************
        'Descripcion: Inserta evaluacion de un cliente.
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sql As New FuncionesGenerales.SqlQuery

            Data.eva_cls.InsertOnSubmit(Eva)
            Data.SubmitChanges()

            sql.ExecuteNonQuery("Exec sp_op_cierre_cliente '" & Eva.cli_idc & "', '" & Eva.cli_idc & "', '" & DateTime.Now.ToString("yyyyMMdd") & "'")


            'Dim id As Integer = (From E In Data.eva_cls Order By E.id_eva Descending Select E.id_eva).First
            Return Eva.id_eva

        Catch ex As Exception
            Return 0
        Finally

        End Try

    End Function

    Public Function EvaluacionActualiza(ByVal Eva As eva_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza evaluacion de un cliente.
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 27/10/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Evaluacion As eva_cls = (From E In Data.eva_cls Where E.id_eva = Eva.id_eva).First

            Evaluacion.id_eje = Eva.id_eje
            Evaluacion.id_P_0023 = Eva.id_P_0023

            If Not IsNothing(Eva.id_P_0110) Then
                Evaluacion.id_P_0110 = Eva.id_P_0110
            End If

            Evaluacion.cli_idc = Eva.cli_idc
            Evaluacion.eva_por = Eva.eva_por

            Data.SubmitChanges()

            Return True


        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function EvaluacionDeudorElimina(ByVal id As Integer, ByVal Rut_Deudor As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: elimina un registro del deudor asociado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/08/2008
        'Quien Modifica              Fecha              Descripcion
        'ASaldivar                   08/06/2010         Se cambio deu_ide a deu_rut por que no elimina
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext
            Dim EXD = From E In Data.exd_cls Where E.id_eva = id And E.deu_rut = Format(Rut_Deudor, Var.FMT_RUT)

            Data.exd_cls.DeleteAllOnSubmit(EXD)
            Data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function EvaluacionDeudorElimina(ByVal id As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: elimina los registros del deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext
            Dim EXD = From E In Data.exd_cls Where E.id_eva = id

            Data.exd_cls.DeleteAllOnSubmit(EXD)
            Data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function EvaluacionElimina(ByVal id As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: elimina evaluacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext
            Dim EVA = From E In Data.eva_cls Where E.id_eva = id

            Data.eva_cls.DeleteAllOnSubmit(EVA)
            Data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function EvaluacionDeudorInsertar(ByVal EXD As exd_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta evaluacion de un deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try

            Dim Data As New DataClsFactoringDataContext

            Data.exd_cls.InsertOnSubmit(EXD)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function



#End Region

#Region "Hoja de Negociacion"

    Public Function NegociacionInserta(ByVal OPN As opn_cls, ByVal RutCliente As Long) As Integer

        '*********************************************************************************************************************************
        'Descripcion: Inserta evaluacion de un deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '  JLagos                   28/01/2009          Se quita cuando asocia a la planilla de aprobaciones desde guardar negociacion a cuando simula la operacion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Try

                Dim ev As opn_cls = (From e In Data.opn_cls Where e.id_eva = OPN.id_eva).First

                If Not IsNothing(ev) Then
                    descripcion = ""
                    Return 0
                    Exit Function
                End If

            Catch ex As Exception

            End Try

            Data.opn_cls.InsertOnSubmit(OPN)

            '------------------------------------------------------------------------------------------------
            'Actualiza el estado de la evaluacion a Asociada (2)
            '------------------------------------------------------------------------------------------------

            Dim Evaluacion As eva_cls = (From E In Data.eva_cls Where E.id_eva = OPN.id_eva).First

            Evaluacion.id_P_0110 = 2

            Data.SubmitChanges()

            Return OPN.id_opn

        Catch ex As Exception
            descripcion = ex.Message
            Return 99
        End Try

    End Function

    Public Function NegociacionActualiza(ByVal OPN As opn_cls, ByVal RutCliente As Long) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una hoja de negociacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 20/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            'Actualiza el estado de la evaluacion a Asociada (2)
            Dim Eval As eva_cls = (From E In Data.eva_cls Where E.id_eva = OPN.id_eva And E.id_P_0110 <= 2).First

            Eval.id_P_0110 = 2

            Dim OPNEG As opn_cls = (From O In Data.opn_cls Where O.id_opn = OPN.id_opn And O.id_P_0082 <= 2).First

            '----------------------------------------------------------------------------------------------------------------------------------------------------
            'reproceso si cambia el porcentaje de anticipo debe cambiar la evaluacion (Versión: 12122013.V1)
            If OPNEG.opn_por_ant <> OPN.opn_por_ant Then

                Eval.eva_por = OPN.opn_por_ant

                Dim evaxdeu = From e In Data.exd_cls Where e.id_eva = Eval.id_eva
                Dim SumatoriaDeuda As ArrayList
                Dim factor As Double
                Dim mto_tot_deu_cli As Double
                Dim suma_mto_eval As Double
                Dim HTodoLoPaga As Double
                Dim HPagFactoring As Double

                factor = CG.ParidadDevuelve(Eval.id_P_0023, Eval.eva_fec_cre).par_val

                For Each e In evaxdeu

                    SumatoriaDeuda = DeudorDeudaDevuelve(Eval.cli_idc, e.deu_ide)
                    SumatoriaDeuda.Item(0) = SumatoriaDeuda.Item(0) / factor
                    SumatoriaDeuda.Item(1) = SumatoriaDeuda.Item(1) / factor

                    If SumatoriaDeuda.Count > 0 Then
                        HTodoLoPaga = SumatoriaDeuda.Item(0)
                        HPagFactoring = SumatoriaDeuda.Item(1)
                    Else
                        HTodoLoPaga = 0
                        HPagFactoring = 0
                    End If

                    e.mto_eva = e.mto_eva_tot * (OPN.opn_por_ant / 100)
                    suma_mto_eval = e.mto_eva
                    e.exd_por_cli = ((e.mto_eva + HTodoLoPaga) / (mto_tot_deu_cli + suma_mto_eval)) * 100

                Next

                Data.SubmitChanges()

            End If
            '----------------------------------------------------------------------------------------------------------------------------------------------------

            OPNEG.id_P_0012 = OPN.id_P_0012
            OPNEG.id_P_0031 = OPN.id_P_0031
            OPNEG.id_P_0023 = OPN.id_P_0023
            OPNEG.id_eva = OPN.id_eva
            OPNEG.opn_mto_doc = OPN.opn_mto_doc
            OPNEG.opn_tas_bas = OPN.opn_tas_bas
            OPNEG.opn_spr_ead = OPN.opn_spr_ead
            OPNEG.opn_pto_spr = OPN.opn_pto_spr
            OPNEG.opn_tas_neg = OPN.opn_tas_neg 'jlagos 08-07-2013
            OPNEG.id_P_0023_com = OPN.id_P_0023_com
            OPNEG.opn_por_com = OPN.opn_por_com
            OPNEG.opn_com_min = OPN.opn_com_min
            OPNEG.opn_com_max = OPN.opn_com_max
            OPNEG.id_P_0023_fla = OPN.id_P_0023_fla
            OPNEG.opn_com_fla = OPN.opn_com_fla
            OPNEG.opn_por_ant = OPN.opn_por_ant
            OPNEG.opn_com_neg = OPN.opn_com_neg
            OPNEG.opn_ins_neg = OPN.opn_ins_neg
            OPNEG.opn_fec = OPN.opn_fec
            OPNEG.opn_fev = OPN.opn_fev
            OPNEG.opn_fev_ori = OPN.opn_fev_ori
            OPNEG.opn_can_doc = OPN.opn_can_doc
            OPNEG.opn_dia_vto = OPN.opn_dia_vto 'jlagos 18-07-2013 Dias a sumar al vencimiento
            OPNEG.id_P_0056 = OPN.id_P_0056
            OPNEG.opn_fec_neg = OPN.opn_fec_neg
            OPNEG.opn_com_tot = OPN.opn_com_tot
            OPNEG.opn_mto_des = OPN.opn_mto_des
            OPNEG.opn_can_ddr = OPN.opn_can_ddr
            OPNEG.cal_oto_gam = OPN.cal_oto_gam 'jlagos 25-02-2013
            OPNEG.id_bco = OPN.id_bco
            OPNEG.opn_cta_cte = OPN.opn_cta_cte
            OPNEG.opn_cto_son = OPN.opn_cto_son
            OPNEG.opn_pag_son = OPN.opn_pag_son
            OPNEG.opn_mdt_son = OPN.opn_mdt_son
            OPNEG.opn_ant_014 = OPN.opn_ant_014
            OPNEG.id_P_0021 = OPN.id_P_0021
            OPNEG.opn_pgr_fec_vto = OPN.opn_pgr_fec_vto
            OPNEG.opn_pgr_mto = OPN.opn_pgr_mto
            OPNEG.opn_pgr_com = OPN.opn_pgr_com
            OPNEG.opn_tas_moa = OPN.opn_tas_moa
            OPNEG.id_P_0082 = 1 'Vuelve la negociación a estado 1
            OPNEG.id_suc = OPN.id_suc
            OPNEG.opn_ptl = OPN.opn_ptl
            OPNEG.opn_res_son = OPN.opn_res_son
            OPNEG.opn_cuo = OPN.opn_cuo
            OPNEG.opn_lnl = OPN.opn_lnl
            OPNEG.opn_gen_gmf = OPN.opn_gen_gmf
            OPNEG.opn_tip_tas = OPN.opn_tip_tas
            OPNEG.opn_dia_vto = OPN.opn_dia_vto

            Data.SubmitChanges()

            'Dim Sql As New FuncionesGenerales.SqlQuery
            'Sql.ExecuteNonQuery("Exec sp_AsociacionClasificacion " & OPN.id_opn & " ")

            'Data.sp_AsociacionClasificacion(OPN.id_opn)
            'Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GastosDefinidosInserta(ByVal GDN As gdn_cls, ByVal id_ope As Integer, ByVal Cantidad_DOC As Integer, ByVal Cantidad_Deu As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un gasto definido para una operacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 22/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext


            Dim GTO As gto_cls = (From G In Data.gto_cls Where G.id_gto = GDN.id_gto).First

            Data.gdn_cls.InsertOnSubmit(GDN)

            Data.SubmitChanges()

            Select Case GTO.id_P_0036

                Case 1

                    Dim GOS As New gos_cls

                    GOS.id_gos = Nothing

                    If id_ope > 0 Then
                        GOS.id_ope = id_ope
                    Else
                        GOS.id_ope = Nothing
                    End If

                    GOS.id_gdn = GDN.id_gdn
                    GOS.id_gfn = Nothing
                    GOS.gos_fec = Date.Now
                    GOS.gos_fij = "D"

                    Data.gos_cls.InsertOnSubmit(GOS)


                Case 2

                    For I = 1 To Cantidad_Deu

                        Dim GOS As New gos_cls

                        GOS.id_gos = Nothing
                        If id_ope > 0 Then
                            GOS.id_ope = id_ope
                        Else
                            GOS.id_ope = Nothing
                        End If
                        GOS.id_gdn = GDN.id_gdn
                        GOS.id_gfn = Nothing
                        GOS.gos_fec = Date.Now
                        GOS.gos_fij = "D"

                        Data.gos_cls.InsertOnSubmit(GOS)

                    Next


                Case 3

                    For I = 1 To Cantidad_DOC

                        Dim GOS As New gos_cls

                        GOS.id_gos = Nothing
                        If id_ope > 0 Then
                            GOS.id_ope = id_ope
                        Else
                            GOS.id_ope = Nothing
                        End If
                        GOS.id_gdn = GDN.id_gdn
                        GOS.id_gfn = Nothing
                        GOS.gos_fec = Date.Now
                        GOS.gos_fij = "D"

                        Data.gos_cls.InsertOnSubmit(GOS)

                    Next
                Case 4

            End Select


            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function GastosFijosInserta(ByVal GFN As gfn_cls, ByVal id_ope As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un gasto fijos para una operacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 22/08/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Data.gfn_cls.InsertOnSubmit(GFN)
            Data.SubmitChanges()

            Dim GOS As New gos_cls

            GOS.id_gos = Nothing

            If id_ope > 0 Then
                GOS.id_ope = id_ope
            Else
                GOS.id_ope = Nothing
            End If

            GOS.id_gdn = Nothing
            GOS.id_gfn = GFN.id_gfn
            GOS.gos_fec = Date.Now
            GOS.gos_fij = "F"

            Data.gos_cls.InsertOnSubmit(GOS)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Gastos_Elimina(ByVal id_opn As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Elimina los gastos de la negociacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 30/03/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            '-----------------------------------------------------------------------------------------------------------------------------
            'Gastos fijos
            '-----------------------------------------------------------------------------------------------------------------------------
            Dim EliminaGastosFijo = From G In Data.gfn_cls Where G.id_opn = id_opn
            Dim EliminaGastosOperacionFijo = From G In Data.gos_cls Where G.gfn_cls.id_opn = id_opn And G.gos_fij = "F"

            Data.gfn_cls.DeleteAllOnSubmit(EliminaGastosFijo)
            Data.gos_cls.DeleteAllOnSubmit(EliminaGastosOperacionFijo)

            '-----------------------------------------------------------------------------------------------------------------------------
            'Gastos Definidos
            '-----------------------------------------------------------------------------------------------------------------------------
            Dim EliminaGastosDefinidos = From G In Data.gdn_cls Where G.id_opn = id_opn
            Dim EliminaGastosOperacionDefinidos = From G In Data.gos_cls Where G.gdn_cls.id_opn = id_opn And G.gos_fij = "D"

            Data.gdn_cls.DeleteAllOnSubmit(EliminaGastosDefinidos)
            Data.gos_cls.DeleteAllOnSubmit(EliminaGastosOperacionDefinidos)

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "APLICACIONES"

    Public Function Aplicaciones_Guarda(ByVal Coll_Egresos As Collection, ByVal coll_Ingresos As Collection, _
                                        ByVal Cabecera_Egreso As egr_cls, ByVal Cabecera_Ingreso As ing_cls, _
                                        ByVal Aplicacion As apl_cls) As Integer

        '*********************************************************************************************************************************
        'Descripcion: Guarda la aplicacion de excedentes, cxp y dnc
        'Creado por: Jorge Lagos C.
        'Fecha Creacion: 18/02/2009
        'Quien Modifica              Fecha              Descripcion
        'JLAGOS                     17-06-2015          SE QUITA ACTUALIZACION DE SALDO EXCEDENTE, AHORA SOLO SE MODIFICARA SI ES APROBADO
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            'Dim ts = New TransactionScope

            'Using ts

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Insertamos la aplicacion
            '-----------------------------------------------------------------------------------------------------------------------------------
            Data.apl_cls.InsertOnSubmit(Aplicacion)
            Data.SubmitChanges()

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Actualizamos la cabecera del egreso con el nuevo n° de aplicacion creado
            '-----------------------------------------------------------------------------------------------------------------------------------
            Cabecera_Egreso.id_apl = Aplicacion.id_apl

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Insertamos la cabecera del Egreso
            '-----------------------------------------------------------------------------------------------------------------------------------

            Data.egr_cls.InsertOnSubmit(Cabecera_Egreso)
            Data.SubmitChanges()

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Insertamos la cabecera del Ingreso
            '-----------------------------------------------------------------------------------------------------------------------------------
            Data.ing_cls.InsertOnSubmit(Cabecera_Ingreso)
            Data.SubmitChanges()

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Ingresamos los egreso secuenciales, pero primero debemos actualizar el id_egr
            '-----------------------------------------------------------------------------------------------------------------------------------
            For E = 1 To Coll_Egresos.Count
                Coll_Egresos.Item(E).id_egr = Cabecera_Egreso.id_egr
                Data.egr_sec_cls.InsertOnSubmit(Coll_Egresos.Item(E))
            Next

            Data.SubmitChanges()

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Ingresamos los ingresos secuenciales, pero primero debemos actualizarle los id_ing y id_egr_sec
            '-----------------------------------------------------------------------------------------------------------------------------------

            For I = 1 To coll_Ingresos.Count

                Dim Indice_egr As Integer = coll_Ingresos.Item(I).id_egr_sec

                coll_Ingresos.Item(I).id_ing = Cabecera_Ingreso.id_ing
                coll_Ingresos.Item(I).id_egr_sec = Coll_Egresos.Item(Indice_egr).id_egr_sec
                coll_Ingresos.Item(I).id_dpo = Nothing

                Data.ing_sec_cls.InsertOnSubmit(coll_Ingresos.Item(I))

            Next

            Data.SubmitChanges()

            ''' '' '' '' '' ''-----------------------------------------------------------------------------------------------------------------------------------
            ''' '' '' '' '' ''Actualizamos los saldos de los excedentes (tabla doc)
            ''' '' '' '' '' ''-----------------------------------------------------------------------------------------------------------------------------------
            '' '' '' '' '' ''For E = 1 To Coll_Egresos.Count

            '' '' '' '' '' ''    'preguntamos si lo que aplica es un excedente
            '' '' '' '' '' ''    If Coll_Egresos(E).id_P_0055 = 3 Then

            '' '' '' '' '' ''        Dim sqlquery As New FuncionesGenerales.SqlQuery

            '' '' '' '' '' ''        sqlquery.ExecuteNonQuery("Update doc set doc_sdo_exc = doc_sdo_exc - " & CDbl(Coll_Egresos(E).egr_mto) & " Where id_doc = " & CInt(Coll_Egresos(E).id_doc) & " ")

            '' '' '' '' '' ''    End If

            '' '' '' '' '' ''Next

            Return Aplicacion.id_apl

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function Aplicaciones_ApruebaRechaza(ByVal id_apl As Integer, ByVal Observacion As String, ByVal AR As Char, ByVal Fecha As DateTime, ByVal Ejecutivo As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Aprueba o Rechaza una aplicacion, guardando su observacion
        'Creado por: Jorge Lagos C.
        'Fecha Creacion: 19/03/2009
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     06-08-2012          -se quita la generacion de nomina automatica
        'JLAGOS                     17-06-2015          -SE AGREGA ACTUALIZACION DE SALDO DE EXCEDENTE CUANDO APRUEBA
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sqlquery As New FuncionesGenerales.SqlQuery
            Dim Devolucion As String
            Dim RutCliente As String
            Dim coll_clientes As New Collection
            Dim FG As New FuncionesGenerales.FComunes

            'Using ts

            Dim Aprob_Recha As Char
            Dim Entrega As Char

            If AR = "S" Then
                Aprob_Recha = "L"
            Else
                Aprob_Recha = "R"
            End If

            '--------------------------------------------------------------
            'Buscamos la aplicacion y damos Aprobacion o Rechazo
            '--------------------------------------------------------------
            Dim Aplicacion = From A In Data.apl_cls Where A.id_apl = id_apl

            For Each A In Aplicacion

                If Not IsNothing(A.apl_apb_com) Then
                    Exit Try
                End If

                A.apl_apb_com = AR

                A.id_eje_com = Ejecutivo 'Aprobacion Comercial 
                A.id_eje_ope = Ejecutivo 'Aprobacion Operacion

                A.apl_obs_com = Observacion
                A.apl_fec_apb_com = Fecha

                If AR = "S" Then

                    '-------------------------------------------------------------------------
                    'En caso de que Apruebe y el saldo sea mayor a 0, crea una cuenta de abono
                    '-------------------------------------------------------------------------
                    Dim Saldo_Aplica As Double

                    If A.apl_con_dev = "N" And A.apl_num_mod = 0 Then
                        Saldo_Aplica = A.apl_sdo
                    End If

                    If Saldo_Aplica > 0 Then

                        Dim CXP As New cxp_cls

                        With CXP
                            .cli_idc = A.cli_idc
                            .id_P_0041 = 1 'Tipo de Cuenta = Abono Anticipo
                            .cxp_fec = Date.Now
                            .cxp_mto = Saldo_Aplica
                            .cxp_des = "POR SDO. APLICACION S/DEVOLUCION NRO : " & A.id_apl
                            .id_P_0057 = 1
                            .id_apl = A.id_apl
                            .id_P_0023 = 1
                            .cxp_fac_cam = 1
                            .cxp_fec = Date.Now

                            'Buscamos documento vigente del cliente o ultimo pago asociado
                            Dim docto As Integer = (From d In Data.doc_cls Where d.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = A.cli_idc And _
                                                    ((d.dsi_cls.id_P_0011 = 1 Or d.dsi_cls.id_P_0011 = 2) Or _
                                                      d.doc_ful_pgo.ToString().Length > 0) Order By d.dsi_cls.id_P_0011, d.id_doc Select d.id_doc).First()

                            .id_doc = docto


                        End With

                        Data.cxp_cls.InsertOnSubmit(CXP)

                    End If



                End If

                Data.SubmitChanges()

                Try

                    '-----------------------------------------------------------------------------
                    'Buscamos los ingresos asociados a la aplicacion, para cambiarle la validacion
                    '-----------------------------------------------------------------------------
                    Dim Ing = From I In Data.ing_sec_cls Where I.cli_idc = A.cli_idc And _
                                                              (I.id_P_0053 = 1 Or I.id_P_0053 = 2) And _
                                                               I.egr_sec_cls.egr_cls.id_apl = A.id_apl


                    For Each I In Ing
                        I.ing_vld_rcz = Aprob_Recha
                    Next

                    Entrega = "N"

                    If A.apl_con_dev = "S" And Aprob_Recha = "L" Then
                        Aprob_Recha = "V"
                    End If

                    RutCliente = A.cli_idc

                    If Not FG.BuscaCollection(coll_clientes, RutCliente) Then
                        coll_clientes.Add(RutCliente)
                    End If

                    Devolucion = A.apl_con_dev

                Catch ex As Exception

                End Try

                '-----------------------------------------------------------------------------
                'Buscamos los egresos asociados a la aplicacion, para cambiarle la validacion
                '-----------------------------------------------------------------------------
                Dim Egr = From E In Data.egr_sec_cls Where E.egr_ent = "N" And _
                                                          (E.id_P_0055 = 1 Or (E.id_P_0055 = 2 And E.egr_doc_nce = "S") Or E.id_P_0055 = 3 Or E.id_P_0055 = 5) And _
                                                           E.egr_cls.id_apl = A.id_apl

                For Each E In Egr

                    If E.id_P_0056 <> 5 Or Aprob_Recha = "R" Then 'SIN GIRO

                        E.egr_vld_rcz = Aprob_Recha
                        E.egr_ent = Entrega

                        '' ''Si es distinto de DEVOLUCION TOTAL APLICACIÓN, actualiza saldo
                        ' ''If E.id_P_0055 <> 5 Then

                        ' ''    'Buscamos documentos para devolver su saldo para cuando es rechazada la aplicacion
                        ' ''    '05-08-2014 se agrega validacion para solo documentos
                        ' ''    If E.id_doc <> Nothing Or E.id_doc > 0 Then
                        ' ''        sqlquery.ExecuteNonQuery("Update doc set doc_sdo_exc = doc_sdo_exc + " & CDbl(E.egr_mto) & " Where id_doc = " & CInt(E.id_doc) & " ")
                        ' ''    End If

                        ' ''End If

                    Else

                        'Si es distinto de DEVOLUCION TOTAL APLICACIÓN, actualiza saldo
                        If E.id_P_0055 <> 5 Then 'EXCEDENTE O RESERVA
                            If E.id_doc <> Nothing Or E.id_doc > 0 Then
                                sqlquery.ExecuteNonQuery("Update doc set doc_sdo_exc = doc_sdo_exc - " & CDbl(E.egr_mto) & " Where id_doc = " & CInt(E.id_doc) & " ")
                            End If
                        End If

                        E.egr_vld_rcz = "L"

                    End If

                Next

            Next

            Data.SubmitChanges()

            'ts.Complete()

            If Devolucion = "N" Then 'sin devolucion ejecuta el cierre
                'jlagos 06-05-2012 -se agrega la rebaja automatica de saldos
                For I = 1 To coll_clientes.Count
                    Data.sp_op_cierre_cliente(coll_clientes(I).ToString(), _
                                              coll_clientes(I).ToString(), _
                                              DateTime.Now())
                Next

            End If

            'End Using

        Catch ex As Exception

        End Try

    End Function

    Public Function Aplicacion_devolucion_genera(ByVal id_apl As Integer)

        Dim data As New DataClsFactoringDataContext

        Dim Aplicacion = From A In data.apl_cls Where A.id_apl = id_apl

        For Each A In Aplicacion

            '-------------------------------------------------------------------------
            'En caso de que Apruebe y el saldo sea mayor a 0, crea una cuenta de abono
            '-------------------------------------------------------------------------
            Dim Saldo_Aplica As Double

            If A.apl_con_dev = "N" And A.apl_num_mod = 0 Then
                Saldo_Aplica = A.apl_sdo
            Else
                '*****************************************************************************
                'Descuenta monto pendiente
                '*****************************************************************************

                '-----------------------------------------------------------------------------
                'Buscamos los egresos para ver cuanta plata les ocupamos
                '-----------------------------------------------------------------------------
                Dim EgrS = From E In data.egr_sec_cls Where E.egr_ent = "N" And _
                           (E.id_P_0055 = 1 Or (E.id_P_0055 = 2 And E.egr_doc_nce = "S") Or E.id_P_0055 = 3) And _
                            E.egr_cls.id_apl = A.id_apl

                Dim EGR_SC As New egr_sec_cls
                For Each E In EgrS
                    Dim valor As Double
                    Try

                        Dim IngS = (From I In data.ing_sec_cls Where I.id_egr_sec = E.id_egr_sec And _
                                                                   (I.id_P_0053 = 1 Or I.id_P_0053 = 2) And _
                                                                    I.egr_sec_cls.egr_cls.id_apl = A.id_apl Select I.ing_mto_tot).Sum()
                        valor = IIf(IsNothing(IngS), 0, IngS)
                    Catch ex As Exception
                        E.id_P_0056 = 5
                        valor = 0
                    End Try

                    If E.egr_mto <= valor Then
                        E.id_P_0056 = 5
                    Else


                        With EGR_SC

                            .egr_mto = A.apl_sdo
                            .egr_fec_ctb = E.egr_fec_ctb
                            .egr_vld_rcz = E.egr_vld_rcz
                            .id_bco = E.id_bco
                            .id_cxp = E.id_cxp
                            .id_doc = E.id_doc
                            .id_dpo = E.id_dpo
                            .id_egr = E.id_egr
                            .id_nce = E.id_nce
                            .id_P_0053 = E.id_P_0053
                            .id_P_0055 = 5
                            .id_P_0056 = E.id_P_0056
                            .egr_cta_cte = E.egr_cta_cte
                            .egr_dep_ant = E.egr_dep_ant
                            .egr_doc_nce = E.egr_doc_nce
                            .egr_ent = E.egr_ent
                            .egr_est_tra = E.egr_est_tra
                            E.id_P_0056 = 5
                        End With

                    End If

                Next

                data.egr_sec_cls.InsertOnSubmit(EGR_SC)
                data.SubmitChanges()
            End If
        Next

    End Function

#End Region

#End Region

End Class
