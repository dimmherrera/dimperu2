Imports Microsoft.VisualBasic
Imports CapaDatos
Public Class ClaseTesoreria

    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables

#Region "Consultas Tesoreria"

#Region "TESORERIA"

    Public Function PagosNominaIngreso_Devuelve(ByVal nro_nomina As Long, _
                                                ByVal FormaPago_Desde As Integer, ByVal FormaPago_Hasta As Integer, _
                                                ByVal Banco_Desde As Integer, ByVal Banco_Hasta As Integer, _
                                                ByVal Plaza_Desde As String, ByVal Plaza_Hasta As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los pagos que fueron realizados con o sin nomina
        'Creado por Jorge Lagos
        'Fecha Creacion: 25/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            If nro_nomina > 0 Then

                Dim Ingresos = From I In data.ing_sec_cls Where I.dpo_cls.id_nma = nro_nomina _
                                   Group By _
                                     I.id_ing, _
                                     I.ing_cls.ing_fec, _
                                     I.ing_cls.ing_obs, _
                                     I.ing_cls.ing_pgo_hre, _
                                     I.id_dpo _
                                    Into Monto = Sum(I.ing_mto_tot) _
                            Select id_ing, ing_fec, ing_obs, Monto, ing_pgo_hre, id_dpo

                For Each x In Ingresos
                    Coll.Add(x)
                Next

            Else

                Dim Ingresos = From I In data.ing_sec_cls Where I.dpo_cls.id_P_0054 <> 7 And _
                                                                I.ing_vld_rcz = "V" And _
                                                               (I.dpo_cls.id_P_0054 >= FormaPago_Desde And _
                                                                I.dpo_cls.id_P_0054 <= FormaPago_Hasta) And _
                                                                I.ing_mto_tot > 0 _
                                   Group By _
                                     I.id_ing, _
                                     I.ing_cls.ing_fec, _
                                     I.ing_cls.ing_obs, _
                                     I.dpo_cls.id_bco, _
                                     I.dpo_cls.id_PL_000047, _
                                     I.ing_cls.ing_pgo_hre, _
                                     I.id_dpo _
                                    Into Monto = Sum(I.ing_mto_tot) _
                            Select id_ing, ing_fec, ing_obs, id_bco, id_PL_000047, Monto, ing_pgo_hre, id_dpo

                For Each x In Ingresos

                    If (Val(x.id_bco) >= Banco_Desde And Val(x.id_bco) <= Banco_Hasta) And _
                       (Val(x.id_PL_000047) >= Plaza_Desde And Val(x.id_PL_000047) <= Plaza_Hasta) Then Coll.Add(x)
                Next

            End If

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function NominaEgreso_Devuelve(ByVal nro_nomina As Long, _
                                          ByVal Banco_Desde As Integer, ByVal Banco_Hasta As Integer, _
                                          ByVal TipoEgreso_Desde As Integer, ByVal TipoEgreso_Hasta As Integer, _
                                          ByVal QuePaga_Desde As Integer, ByVal QuePaga_Hasta As Integer, _
                                          ByVal Fecha_Desde As DateTime, ByVal Fecha_Hasta As DateTime) As Collection
        '-------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve los pagos que fueron realizados con o sin nomina
        'Creado por Jorge Lagos
        'Fecha Creacion: 25/02/2009
        '-------------------------------------------------------------------------------------------------------------------------
        'Quien Modifica                 Fecha           Descripcion
        'A. Saldivar                    24/08/2010      Se valida si cxp_fac_cam esta vacio cuando id_P_0055 es igual a 1
        'A. Saldivar                    22/02/2011      Se agrega horas a fechas 
        'J. Lagos                       30/08/2012      Se quita el parametro atr_003 (Nomina_SN)
        'J. Lagos                       20/08/2013      Se agrega validacion de que operacion este otorgada
        '-------------------------------------------------------------------------------------------------------------------------

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext
        Dim FH As String
        Dim CG As New ConsultasGenerales

        Try

            FH = Format(Fecha_Hasta, "yyyy/MM/dd") & " 23:59:59"

           
            If nro_nomina > 0 Then

                Dim Egresos = From E In data.egr_sec_cls Where E.dpo_cls.id_nma = nro_nomina _
                                                            And E.id_P_0056 <> 5 _
                                                          Group By id_egr = E.id_egr, _
                                    RutCliente = E.egr_cls.cli_idc, _
                                    RazonSocial = If(E.egr_cls.cli_cls.id_P_0044 = 1, _
                                                     E.egr_cls.cli_cls.cli_rso.Trim & " " & _
                                                     E.egr_cls.cli_cls.cli_ape_ptn.Trim & " " & _
                                                     E.egr_cls.cli_cls.cli_ape_mtn.Trim, _
                                                     E.egr_cls.cli_cls.cli_rso.Trim), _
                                    tipo = E.id_P_0055, _
                                    Fecha_Egreso = E.egr_cls.egr_fec, _
                                    Observacion = E.egr_cls.egr_obs, _
                                    id_FormaPago = E.id_P_0056, _
                                    FormaPago = E.P_0056_cls.pnu_des, _
                                    CtaCte = E.egr_cta_cte, _
                                    Entregado = E.egr_ent, _
                                    id_egre = E.id_egr, _
                                    QuePaga = E.P_0055_cls.pnu_des, _
                                    Nomina = E.dpo_cls.id_nma, _
                                    E.egr_dep_ant, _
                                    E.nbc_cls _
                            Into Monto_Anticipo = Sum(If(E.id_P_0055 = 4, E.egr_cls.opo_cls.ope_cls.ope_mto_ant, 0)), _
                                 Monto_Egreso = Sum(E.egr_mto) _
                                Select New With {id_egr, _
                                       RutCliente, _
                                       RazonSocial, _
                                       .Id_Moneda = "", _
                                       .Factor = "", _
                                       Monto_Anticipo, _
                                       Monto_Egreso, _
                                       tipo, _
                                       .Moneda = "", _
                                       Fecha_Egreso, _
                                       Observacion, _
                                       .Id_Banco = 0, _
                                       .Banco = "", _
                                       id_FormaPago, _
                                       FormaPago, _
                                       CtaCte, _
                                       Entregado, _
                                       id_egre, _
                                       QuePaga, _
                                       Nomina, _
                                       egr_dep_ant, _
                                       nbc_cls}

                'Monto Va en moneda de origen
                For Each E In Egresos
                    If Not IsNothing(E.nbc_cls) Then

                        Dim banco As bco_cls = (From b In data.nbc_cls Where b.id_nbc = E.nbc_cls.id_nbc Select b.sbc_cls.bco_cls).First()

                        E.Id_Banco = banco.id_bco
                        E.Banco = banco.bco_des.Trim()

                    End If
                    Coll.Add(E)
                Next


            Else

                'Id_Banco = E.nbc_cls.sbc_cls.id_bco, _
                'Banco = E.nbc_cls.sbc_cls.bco_cls.bco_des, _

                Dim Egresos = From E In data.egr_sec_cls Where _
                                                   (E.id_P_0056 >= TipoEgreso_Desde And E.id_P_0056 <= TipoEgreso_Hasta) And _
                                                   (E.id_P_0055 >= QuePaga_Desde And E.id_P_0055 <= QuePaga_Hasta) And _
                                                   (E.egr_vld_rcz = "V" Or E.egr_vld_rcz = "L") And _
                                                   (E.egr_cls.egr_fec >= Format(CDate(Fecha_Desde), "yyyy/MM/dd") And E.egr_cls.egr_fec <= FH) _
                                                   And E.id_P_0056 <> 5 _
                            Group By id_egr = E.id_egr, _
                                    RutCliente = E.egr_cls.cli_idc, _
                                    RazonSocial = If(E.egr_cls.cli_cls.id_P_0044 = 1, _
                                                     E.egr_cls.cli_cls.cli_rso.Trim & " " & _
                                                     E.egr_cls.cli_cls.cli_ape_ptn.Trim & " " & _
                                                     E.egr_cls.cli_cls.cli_ape_mtn.Trim, _
                                                     E.egr_cls.cli_cls.cli_rso.Trim), _
                                    tipo = E.id_P_0055, _
                                    Fecha_Egreso = E.egr_cls.egr_fec, _
                                    Observacion = E.egr_cls.egr_obs, _
                                    id_FormaPago = E.id_P_0056, _
                                    FormaPago = E.P_0056_cls.pnu_des, _
                                    CtaCte = E.egr_cta_cte, _
                                    Entregado = E.egr_ent, _
                                    id_egre = E.id_egr, _
                                    QuePaga = E.P_0055_cls.pnu_des, _
                                    Nomina = E.dpo_cls.id_nma, _
                                    E.egr_dep_ant, _
                                    E.nbc_cls _
                            Into Monto_Anticipo = Sum(If(E.id_P_0055 = 4, E.egr_cls.opo_cls.ope_cls.ope_mto_ant, 0)), _
                                 Monto_Egreso = Sum(E.egr_mto) _
                                Select New With {id_egr, _
                                       RutCliente, _
                                       RazonSocial, _
                                       .Id_Moneda = "", _
                                       .Factor = "", _
                                       Monto_Anticipo, _
                                       Monto_Egreso, _
                                       tipo, _
                                       .Moneda = "", _
                                       Fecha_Egreso, _
                                       Observacion, _
                                       .Id_Banco = 0, _
                                       .Banco = "", _
                                       id_FormaPago, _
                                       FormaPago, _
                                       CtaCte, _
                                       Entregado, _
                                       id_egre, _
                                       QuePaga, _
                                       Nomina, _
                                       egr_dep_ant, _
                                       nbc_cls}


                For Each p In Egresos

                    If Not IsNothing(p.nbc_cls) Then

                        Dim banco As bco_cls = (From b In data.nbc_cls Where b.id_nbc = p.nbc_cls.id_nbc Select b.sbc_cls.bco_cls).First()

                        p.Id_Banco = banco.id_bco
                        p.Banco = banco.bco_des.Trim()

                    End If


                    Select Case p.tipo

                        Case 4 'ANTICIPO

                            Try

                                Dim factor = (From E In data.egr_sec_cls _
                                              Where E.id_egr = p.id_egr And E.egr_cls.opo_cls.ope_cls.id_P_0030 = 3 _
                                              Select E.egr_cls.opo_cls.ope_cls.opn_cls.eva_cls.P_0023_cls.pnu_des, _
                                                     E.egr_cls.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023, _
                                                     E.egr_cls.opo_cls.ope_cls.ope_fac_cam).First

                                p.Moneda = factor.pnu_des
                                p.Id_Moneda = factor.id_P_0023
                                p.Factor = factor.ope_fac_cam

                            Catch ex As Exception


                            End Try

                        Case 1, 2, 3, 5
                            Try

                                Dim factor = (From E In data.egr_sec_cls _
                                              Where E.id_egr = p.id_egr And E.egr_cls.apl_cls.apl_apb_com = "S").First

                                p.Moneda = CG.Parametros_Detalle_Devuelve(TablaParametro.Moneda, 1).Item(1).pnu_des
                                p.Id_Moneda = 1
                                p.Factor = 1

                            Catch ex As Exception

                            End Try

                    End Select

                    If (p.Id_Banco >= Banco_Desde And p.Id_Banco <= Banco_Hasta) Then
                        Coll.Add(p)
                    End If


                Next

            End If

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

#End Region

#Region "ARQUEO DE CHEQUES"

    Public Function Cheques_DevuelveChrPorCliente(ByVal FechaDesde As String, ByVal FechaHasta As String, _
                                       ByVal Cliente As String, _
                                       ByVal Custodia As Integer, ByVal Est_Dsd As Integer, ByVal Est_Hst As Integer, _
                                       ByVal Tip_Dsd As String, ByVal Tip_Hst As String, _
                                       Optional ByVal Pag As Integer = 9999) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve cheque Por Fechadesde ,Fechahasta, Cliente y Custodia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'Antonio Saldivar            20/01/2009         Se Modifica Los datos rescatados
        '                                               Se suma monto
        'Antonio Saldivar            15/09/2010         Se valida por estado de cliente  ,ademas si es cliente o deudor
        'Antonio Saldivar            21/09/2010         se Agrega Estado y tipo de cheque en criterio de busqueda 
        'A. Saldivar                 10/02/2011         Se agrega paginacion   
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            'Dim VR As New FuncionesGenerales.Variables
            Dim FDesde As String
            Dim FHasta As String

            Dim cliDesde As String
            Dim cliHasta As String

            Dim Custdesde As Integer
            Dim custhasta As Integer

            If FechaDesde = Nothing Then
                FDesde = "01/01/1900"
                'FHasta = "31/12/2999"
            Else
                FDesde = FechaDesde
                'FHasta = FechaDesde
            End If

            If FechaHasta = Nothing Then
                FHasta = "31/12/2999"
            Else
                FHasta = FechaHasta
            End If

            If Cliente = "" Then
                cliDesde = 0
                cliHasta = 99999999999
            Else
                cliDesde = Cliente
                cliHasta = Cliente
            End If

            If Custodia = 0 Then
                Custdesde = 1
                custhasta = 999
            Else
                Custdesde = Custodia
                custhasta = Custodia
            End If
            Dim Cheque = (From n In data.nrd_cls Where (n.chr_cls.chr_fev_rea >= FDesde) _
                         And (n.chr_cls.chr_fev_rea <= FHasta) _
                         And (n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_idc >= Format(CLng(cliDesde), Var.FMT_RUT)) _
                         And (n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_idc <= Format(CLng(cliHasta), Var.FMT_RUT)) _
                         And (n.chr_cls.id_P_0112 >= Custdesde) _
                         And (n.chr_cls.id_P_0112 <= custhasta) _
                         And (n.chr_cls.chr_tip_cli = "C") _
                         And (n.chr_cls.id_P_0113 >= Est_Dsd And n.chr_cls.id_P_0113 <= Est_Hst) _
                         And (n.chr_cls.chr_tip >= Tip_Dsd And n.chr_cls.chr_tip <= Tip_Hst) _
                                                  Group By n.chr_cls.id_chr, _
                                                        n.chr_cls.bco_cls.bco_des, _
                                                        n.chr_cls.chr_cli_deu, _
                                                        n.chr_cls.chr_fev, _
                                                        n.chr_cls.chr_fev_rea, _
                                                        n.chr_cls.id_ope, _
                                                        n.chr_cls.chr_num, _
                                                        n.chr_cls.PL_000047_cls.pal_des, _
                                                        n.chr_cls.p_0113_cls.pnu_des, _
                                                        Moneda = n.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                                                        n.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                                                        n.chr_cls.id_bco, _
                                                        n.chr_cls.cta_cte, _
                                                        n.chr_cls.chr_tip_cli, _
                                                        n.chr_cls.id_P_0112, _
                                                        n.chr_cls.id_P_0113, _
                                                        n.chr_cls.id_PL_000047, _
                                                        n.dsi_cls.ope_cls.ldc_cls, _
                                                        n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_ape_ptn, _
                                                        n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_ape_mtn, _
                                                        Estado = n.chr_cls.p_0113_cls.pnu_des, _
                                                        TipoCheque = If(n.chr_cls.chr_tip = "R", _
                                                                        "RESPALDO", _
                                                                        "FLUJO"), _
                                                        Nombre = If(n.chr_cls.chr_tip_cli = "C", _
                                                                    If(n.dsi_cls.ope_cls.ldc_cls.cli_cls.id_P_0044 = 1, _
                                                                    n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_rso & " " & n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_ape_ptn & " " & n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_ape_mtn, _
                                                                    n.dsi_cls.ope_cls.ldc_cls.cli_cls.cli_rso), _
                                                                    If(n.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                                       n.dsi_cls.deu_cls.deu_rso & " " & n.dsi_cls.deu_cls.deu_ape_ptn & " " & n.dsi_cls.deu_cls.deu_ape_mtn, n.dsi_cls.deu_cls.deu_rso)), _
                                                        Monto_cheque = n.chr_cls.chr_mto _
                                                        Into Monto_Ocupado = Sum(n.mto_resp)).Skip(sesion.NroPaginacion).Take(Pag)


            'And (n.chr_cls.chr_tip = Tip_Dsd And n.chr_cls.chr_tip = Tip_Hst) _
            Return Cheque
        Catch ex As Exception

        End Try
    End Function

#End Region

#End Region

#Region "Actualizaciones Tesoreria"



#End Region

End Class
