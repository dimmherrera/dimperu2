Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports CapaDatos

Public Class ClaseCuentas

    Dim Var As New FuncionesGenerales.Variables
    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb

#Region "Consultas Cuentas"

#Region "CUENTAS POR COBRAR"

    Public Function CuentasPorCobrarDevuelve(ByVal RutCliente As Long, _
                                         ByVal TipoCxC_Desde As Integer, ByVal TipoCxC_Hasta As Integer, _
                                         ByVal QueSePaga_Desde As Integer, ByVal QueSePaga_Hasta As Integer, _
                                         ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                         Optional ByVal NroAplicacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente
        'Creado por P Gatica .
        'Fecha Creacion: 22/12/2008
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext

            Dim Cuentas = From C In data.cxc_cls Order By C.cxc_fec _
                          Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                C.id_P_0041 >= TipoCxC_Desde And _
                                C.id_P_0041 <= TipoCxC_Hasta And _
                                C.id_P_0057 >= Estado_Desde And _
                                C.id_P_0057 <= Estado_Hasta _
                          Select C

            For Each p In Cuentas

                Dim sal_ing = (From ing_sec1 In data.ing_sec_cls Where ing_sec1.id_cxc = p.id_cxc _
                                And ing_sec1.id_P_0053 >= QueSePaga_Desde _
                                And ing_sec1.id_P_0053 <= QueSePaga_Hasta _
                                And ing_sec1.ing_pro = "N" _
                                And (ing_sec1.ing_vld_rcz = "I" _
                                Or ing_sec1.ing_vld_rcz = "S" _
                                Or ing_sec1.ing_vld_rcz = "V" _
                                Or ing_sec1.ing_vld_rcz = "C") _
                                Select ing_sec1.ing_mto_abo / ing_sec1.ing_fac_cam).Sum()

                Dim VALOR As Double

                If IsNothing(sal_ing) Then
                    VALOR = 0
                Else
                    VALOR = sal_ing.Value
                End If

                Dim Ctas = From C In data.cxc_cls _
                          Where C.id_cxc = p.id_cxc And _
                                (C.cxc_sal - VALOR) >= 0 _
                                Order By C.cxc_fec _
             Select New With { _
                    C.cli_idc, _
                    .TipoCta = C.id_P_0041, _
                    .Descrip_Cta = C.cxc_des, _
                    C.id_P_0023, _
                    .Moneda = C.P_0023_cls.pnu_atr_003, _
                    C.id_cxc, _
                    C.cxc_des, _
                    C.cxc_sal, _
                    C.cxc_int, _
                    C.cxc_ful_pgo, _
                    C.id_doc, _
                    C.cxc_fec, _
                    C.p_0041_cls.pnu_atr_002, _
                    C.cxc_fac_cam, _
                    C.cxc_mto, _
                    C.id_fct, _
                    C.p_0041_cls.pnu_atr_005, _
                    .Interes = 0, _
                    .InteresDevolver = 0.0, _
                    .Tasa = 0, _
                    .TipoCuenta = C.p_0041_cls.pnu_des, _
                    .MontoPagar = 0, _
                    .Cantidad_Ingresos = (From I In data.ing_sec_cls Where I.id_cxc = C.id_cxc And _
                                                                           I.id_P_0053 = 1 And _
                                                                          (I.ing_vld_rcz = "I" Or _
                                                                           I.ing_vld_rcz = "V") And _
                                                                           I.ing_pro = "N" _
                                          Select I.id_ing_sec).Count, _
                     .Aplicacion = (From I In data.ing_sec_cls Where I.id_cxc = C.id_cxc And _
                                                                     I.id_P_0053 = 1 And _
                                                                     I.egr_sec_cls.egr_cls.id_apl = NroAplicacion _
                                    Select I.id_ing_sec).Count}

                For Each x In Ctas
                    Coll.Add(x)
                Next

            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelve(ByVal RutCliente As Long, _
                                             ByVal TipoCxC_Desde As Integer, ByVal TipoCxC_Hasta As Integer, _
                                             ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                             ByVal NroOtorg_Desde As Integer, ByVal NroOtorg_Hasta As Integer, _
                                             ByVal NroDocto_Desde As Integer, ByVal NroDocto_Hasta As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por J Lagos .
        'Fecha Creacion: 29/01/2009
        'Quien Modifica     Fecha Descripcion
        'JLagos             24-03-2009        -Se agrega los campos cxc_sal, id_p_0023 y moneda a la consulta (solicitado por asaldivar)
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext

            Dim Cuentas = From C In data.cxc_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                 (C.id_P_0041 >= TipoCxC_Desde) And _
                                 (C.id_P_0041 <= TipoCxC_Hasta) And _
                                 (C.id_P_0057 >= Estado_Desde) And _
                                 (C.id_P_0057 <= Estado_Hasta) _
                                 Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                 C.id_cxc, _
                                 C.cxc_fec, _
                                 C.cxc_mto, _
                                 C.cxc_des, _
                                 C.id_doc, _
                                 C.id_opo, _
                                 C.cxc_sal, _
                                 C.id_P_0023, _
                                 Moneda = C.P_0023_cls.pnu_des, _
                                 Estado = C.P_0057_cls.pnu_des, _
                                 Cantidad = (From e In data.ing_sec_cls Where e.id_cxc = C.id_cxc _
                                And (e.egr_sec_cls.egr_vld_rcz = "I" _
                                Or e.egr_sec_cls.egr_vld_rcz = "S" _
                                Or e.egr_sec_cls.egr_vld_rcz = "V" _
                                Or e.egr_sec_cls.egr_vld_rcz = "C") _
                                Select e.id_egr_sec).Count


            For Each x In Cuentas
                If (Val(x.id_opo) >= NroOtorg_Desde) And (Val(x.id_opo) <= NroOtorg_Hasta) And _
                   (Val(x.id_doc) >= NroDocto_Desde) And (Val(x.id_doc) <= NroDocto_Hasta) Then

                    Coll.Add(x)
                End If
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelve(ByVal RutCliente As Long, _
                                             ByVal TipoCxC_Desde As Integer, ByVal TipoCxC_Hasta As Integer, _
                                             ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                             Optional ByVal paginacion As Boolean = False, _
                                             Optional ByVal nropaginacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por J Lagos .
        'Fecha Creacion: 29/01/2009
        'Quien Modifica      Fecha          Descripcion
        'S. Henriquez       24/07/2012      Se agrega numero de contrato
        'S. Henriquez       01/10/2012      Se corrige numero de contrato
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext
            If paginacion Then

                Dim Cuentas = (From C In data.cxc_cls _
                              Join DC In data.doc_con_cls On C.id_doc Equals DC.id_doc _
                              Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                    C.id_P_0041 >= TipoCxC_Desde And _
                                    C.id_P_0041 <= TipoCxC_Hasta And _
                                    C.id_P_0057 >= Estado_Desde And _
                                    C.id_P_0057 <= Estado_Hasta _
                                    Order By C.id_cxc _
                    Select New With { _
                                    C.cli_idc, _
                                    .Cliente = If(C.cli_cls.id_P_0044 = 1, _
                                                  C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                  C.cli_cls.cli_rso.Trim), _
                                    .TipoCta = C.id_P_0041, _
                                    .Descrip_Cta = C.p_0041_cls.pnu_des, _
                                    C.id_P_0023, _
                                    .Moneda = C.P_0023_cls.pnu_des, _
                                    C.id_cxc, _
                                    C.cxc_des, _
                                    C.cxc_sal, _
                                    C.cxc_int, _
                                    C.cxc_ful_pgo, _
                                    C.id_doc, _
                                    C.cxc_fec, _
                                    C.p_0041_cls.pnu_atr_002, _
                                    C.cxc_fac_cam, _
                                    C.cxc_mto, _
                                    C.id_fct, _
                                    C.p_0041_cls.pnu_atr_005, _
                                    .Interes = 0.0, _
                                    .InteresDevolver = 0.0, _
                                    .TipoCuenta = C.p_0041_cls.pnu_des, _
                                    .MontoPagar = 0.0, _
                                    .Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                                 If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                                 If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                    .Tasa = 0.0, _
                                    .Pago = (From I In data.ing_sec_cls Where C.id_cxc = I.id_cxc And I.ing_pro = "N" And I.ing_vld_rcz <> "R" And I.ing_vld_rcz <> "A").Count}).Skip(nropaginacion).Take(200)


                For Each P In Cuentas
                    Coll.Add(P)
                Next

            Else
                Dim Cuentas = (From C In data.cxc_cls _
                              Join DC In data.doc_con_cls _
                              On C.id_doc Equals DC.id_doc _
                              Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                    C.id_P_0041 >= TipoCxC_Desde And _
                                    C.id_P_0041 <= TipoCxC_Hasta And _
                                    C.id_P_0057 >= Estado_Desde And _
                                    C.id_P_0057 <= Estado_Hasta _
                    Select New With { _
                                    C.cli_idc, _
                                    .Cliente = If(C.cli_cls.id_P_0044 = 1, _
                                                  C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                  C.cli_cls.cli_rso.Trim), _
                                    .TipoCta = C.id_P_0041, _
                                    .Descrip_Cta = C.p_0041_cls.pnu_des, _
                                    C.id_P_0023, _
                                    .Moneda = C.P_0023_cls.pnu_des, _
                                    C.id_cxc, _
                                    C.cxc_des, _
                                    C.cxc_sal, _
                                    C.cxc_int, _
                                    C.cxc_ful_pgo, _
                                    C.id_doc, _
                                    C.cxc_fec, _
                                    C.p_0041_cls.pnu_atr_002, _
                                    C.cxc_fac_cam, _
                                    C.cxc_mto, _
                                    C.id_fct, _
                                    C.p_0041_cls.pnu_atr_005, _
                                    .Interes = 0.0, _
                                    .InteresDevolver = 0.0, _
                                    .TipoCuenta = C.p_0041_cls.pnu_des, _
                                    .MontoPagar = 0.0, _
                                    .Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                                 If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                                 If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                    .Tasa = 0.0, _
                                    .Pago = (From I In data.ing_sec_cls Where C.id_cxc = I.id_cxc And I.ing_pro = "N" And I.ing_vld_rcz <> "R" And I.ing_vld_rcz <> "A").Count})

                For Each P In Cuentas
                    Coll.Add(P)
                Next

            End If

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelveTotalRegistros(ByVal RutCliente As Long, _
                                             ByVal TipoCxC_Desde As Integer, ByVal TipoCxC_Hasta As Integer, _
                                             ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer) As Integer

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las cantidad de Cuentas por Cobrar de un Cliente 
        'Creado por J Lagos .
        'Fecha Creacion: 13-06-2014
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim data As New DataClsFactoringDataContext


            Dim Cuentas = (From C In data.cxc_cls _
                          Join DC In data.doc_con_cls On C.id_doc Equals DC.id_doc _
                          Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                C.id_P_0041 >= TipoCxC_Desde And _
                                C.id_P_0041 <= TipoCxC_Hasta And _
                                C.id_P_0057 >= Estado_Desde And _
                                C.id_P_0057 <= Estado_Hasta).Count 
            Return Cuentas

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelve(ByVal RutCliente As Long, _
                                                ByVal TipoCxC As Integer, _
                                                ByVal NroOtorg As Integer, ByVal NroDocto As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un un Cliente por rut , tipo de cuenta , numero de otorgamiento y numero de documento
        'Creado por A Saldivar.
        'Fecha Creacion: 21/07/2009
        'Quien modifica         Fecha                       Descripción
        'A Saldivar             16/11/2010      Se consulta por cliente o por cliente y tipo de cuenta
        'A Saldivar             20/12/2010      Se agrega paginacion
        'S Henriquez            12/07/2012      Se agrega numero de contrato y se corrige numero documento
        'S Henriquez            01/10/2012      Se corrige numero de contrato
        '**************************************************************************************************************************************************
        Dim Coll As New Collection
        Try
            ' Dim val As Double
            ' Dim Var As New FuncionesGenerales.Variables

            Dim data As New DataClsFactoringDataContext
            'Dim Cuentas As New Object
            Dim Sesion As New ClsSession.ClsSession

            If TipoCxC = 0 Then 'Todas

                Dim Cuentas = (From C In data.cxc_cls _
                               Join DC In data.doc_con_cls _
                               On C.id_doc Equals DC.id_doc _
                            Where (C.cli_idc = Format(RutCliente, Var.FMT_RUT)) _
                            Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                    C.id_cxc, _
                                    C.cli_idc, _
                                    C.cli_cls.cli_rso, _
                                    C.cli_cls.cli_ape_ptn, _
                                    C.cli_cls.cli_ape_mtn, _
                                    RSocial = C.cli_cls.cli_rso & " " & C.cli_cls.cli_ape_ptn & " " & C.cli_cls.cli_ape_mtn, _
                                    C.cli_cls.id_eje_cod_eje, _
                                    DesEje = C.cli_cls.eje_cls.eje_des_cra, _
                                    C.cxc_fec, _
                                    C.cxc_fec_ctb, _
                                    C.cxc_ful_pgo, _
                                    C.cxc_mto, _
                                    C.cxc_des, _
                                    C.id_P_0023, _
                                    SGMoneda = C.P_0023_cls.pnu_atr_003, _
                                    Moneda = C.P_0023_cls.pnu_des, _
                                    C.id_doc, _
                                    C.id_opo, _
                                    C.cxc_int, _
                                    Estado = C.P_0057_cls.pnu_des, _
                                    C.cxc_fec_int, _
                                    C.cxc_sal, _
                                    Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                               If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                               If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                    Numero = C.doc_cls.dsi_cls.dsi_num).Skip(Sesion.NroPaginacion)


                For Each x In Cuentas.Take(8)
                    Coll.Add(x)
                Next

            Else
                'Otro tipo de cuenta
                Dim Cuentas = (From C In data.cxc_cls _
                               Join DC In data.doc_con_cls _
                               On C.id_doc Equals DC.id_doc _
                               Where (C.cli_idc = Format(RutCliente, Var.FMT_RUT)) And _
                                      (C.id_P_0041 = TipoCxC) _
                                 Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                 C.id_cxc, _
                                 C.cli_idc, _
                                 C.cli_cls.cli_rso, _
                                 C.cli_cls.cli_ape_ptn, _
                                 C.cli_cls.cli_ape_mtn, _
                                 RSocial = C.cli_cls.cli_rso & " " & C.cli_cls.cli_ape_ptn & " " & C.cli_cls.cli_ape_mtn, _
                                 id_eje_cod_eje = C.id_eje, _
                                 DesEje = (From e In data.eje_cls Where e.id_eje = C.id_eje Select e.eje_des_cra).First(), _
                                 C.cxc_fec, _
                                 C.cxc_fec_ctb, _
                                 C.cxc_ful_pgo, _
                                 C.cxc_mto, _
                                 C.cxc_des, _
                                 C.id_P_0023, _
                                 SGMoneda = C.P_0023_cls.pnu_atr_003, _
                                 Moneda = C.P_0023_cls.pnu_des, _
                                 C.id_doc, _
                                 C.id_opo, _
                                 C.cxc_int, _
                                 Estado = C.P_0057_cls.pnu_des, _
                                 C.cxc_fec_int, _
                                 C.cxc_sal, _
                                 Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                               If(DC.dig_ver_uno IsNot Nothing, DC.dig_ver_uno.ToString(), "") & If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                               If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                 Numero = C.doc_cls.dsi_cls.dsi_num).Skip(Sesion.NroPaginacion)



                For Each x In Cuentas.Take(8)
                    Coll.Add(x)
                Next

            End If

            'For Each x In cuentas
            '    Coll.Add(x)
            'Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelve(ByVal RutClientedsd As Long, ByVal RutClihst As Long, _
                                                    ByVal TipoCxC_Desde As Integer, ByVal TipoCxC_Hasta As Integer, _
                                                    ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                                    ByVal NroOtorg_Desde As Integer, ByVal NroOtorg_Hasta As Integer, _
                                                    ByVal NroDocto_Desde As Integer, ByVal NroDocto_Hasta As Integer, _
                                                    ByVal Mon_dsd As Integer, ByVal Mon_hst As Integer, ByVal Fecha_dsd As String, _
                                                    ByVal Fecha_hst As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por A Saldivar .
        'Fecha Creacion: 29/01/2009
        'Quien Modifica         Fecha               Descripcion
        'A Saldivar            30/03/2009           Se agregaron campos de busqueda(Moneda desde , Moneda Hasta, echa desde,Fecha Hsta) 
        '                                           y se agrego Razon Social cliente ,cod_eje, descripcion de ejecutivo
        'A Saldivar            25/08/2010           Rescata R. social de cliente segun tipo de cliente 
        'A Saldivar            20/12/2010           Se agrega paginacion
        'S Henriquez           14/07/2012           Se agrega nro contrato.  
        'S Henriquez           01/10/2012           Se corrige nro contrato.
        '**************************************************************************************************************************************************
        Dim Coll As New Collection
        Try
            ' Dim val As Double
            ' Dim Var As New FuncionesGenerales.Variables

            Dim data As New DataClsFactoringDataContext
            Dim Sesion As New ClsSession.ClsSession

            Dim Cuentas = (From C In data.cxc_cls _
                           Join DC In data.doc_con_cls _
                           On C.id_doc Equals DC.id_doc _
                                    Where (C.cli_idc >= Format(RutClientedsd, Var.FMT_RUT)) And _
                                    (C.cli_idc <= Format(RutClihst, Var.FMT_RUT)) And _
                                    (C.id_P_0041 >= TipoCxC_Desde) And _
                                    (C.id_P_0041 <= TipoCxC_Hasta) And _
                                    (C.id_P_0057 >= Estado_Desde) And _
                                    (C.id_P_0057 <= Estado_Hasta) And _
                                    (C.id_P_0023 >= Mon_dsd) And _
                                    (C.id_P_0023 <= Mon_hst) And _
                                    (C.cxc_fec >= Fecha_dsd) And _
                                    (C.cxc_fec <= Fecha_hst) Order By C.id_cxc _
                        Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                C.id_cxc, _
                                C.cli_idc, _
                                C.cli_cls.cli_dig_ito, _
                                RSocial = If(C.cli_cls.id_P_0044 = 1, _
                                             C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                             C.cli_cls.cli_rso.Trim), _
                                id_eje_cod_eje = C.id_eje, _
                                DesEje = (From e In data.eje_cls Where e.id_eje = C.id_eje Select e.eje_des_cra).First(), _
                                C.cxc_fec, _
                                C.cxc_fec_ctb, _
                                C.cxc_ful_pgo, _
                                C.cxc_mto, _
                                C.cxc_des, _
                                C.id_P_0023, _
                                SGMoneda = C.P_0023_cls.pnu_atr_003, _
                                Moneda = C.P_0023_cls.pnu_des, _
                                C.id_doc, _
                                C.id_opo, _
                                C.cxc_int, _
                                Estado = C.P_0057_cls.pnu_des, _
                                C.cxc_fec_int, _
                                Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                            If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                            If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                C.cxc_sal).Skip(Sesion.NroPaginacionCXC)


            For Each x In Cuentas.Take(12)
                'If (Val(x.opo_cls.opo_otg) >= NroOtorg_Desde) And (Val(x.opo_cls.opo_otg) <= NroOtorg_Hasta) And _
                '   (Val(x.dsi_cls.dsi_num) >= NroDocto_Desde) And (Val(x.dsi_cls.dsi_num) <= NroDocto_Hasta) Then
                Coll.Add(x)
                '  End If
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorCobrarDevuelveGMF(ByVal RutCliente As Long, _
                                                ByVal NroOperacion As Integer) As Object


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por J Lagos .
        'Fecha Creacion: 20/11/2012
        'Quien Modifica      Fecha          Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext

            Dim Cuentas = From C In data.cxc_cls _
                          Join S In data.sim_cxc_cls _
                          On C.id_cxc Equals S.id_cxc _
                          Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                C.id_P_0041 = 23 And _
                                C.id_P_0057 = 1 And _
                                S.id_ope = NroOperacion _
                Select New With { _
                                C.cli_idc, _
                                .Cliente = If(C.cli_cls.id_P_0044 = 1, _
                                              C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                              C.cli_cls.cli_rso.Trim), _
                                .TipoCta = C.id_P_0041, _
                                .Descrip_Cta = C.p_0041_cls.pnu_des, _
                                C.id_P_0023, _
                                .Moneda = C.P_0023_cls.pnu_des, _
                                C.id_cxc, _
                                C.cxc_des, _
                                C.cxc_sal, _
                                C.cxc_int, _
                                C.cxc_ful_pgo, _
                                C.id_doc, _
                                C.cxc_fec, _
                                C.p_0041_cls.pnu_atr_002, _
                                C.cxc_fac_cam, _
                                C.cxc_mto, _
                                C.id_fct, _
                                C.p_0041_cls.pnu_atr_005, _
                                .Interes = 0.0, _
                                .InteresDevolver = 0.0, _
                                .TipoCuenta = C.p_0041_cls.pnu_des, _
                                .MontoPagar = 0.0, _
                                .Contrato = "", _
                                .Tasa = 0.0}

            Return Cuentas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function PreguntaPorOtor(ByVal NroOtor As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Consulta por Numero de Otorgamiento 
        'Creado por: Antonio Saldivar M.
        'Fecha Creacion: 20/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NumOto As opo_cls = (From o In data.opo_cls Where o.opo_otg = NroOtor).First

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function PreguntaPorDoc(ByVal NroDoc As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Consulta por Numero de Documento 
        'Creado por: Antonio Saldivar M.
        'Fecha Creacion: 20/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NumDoc As dsi_cls = (From d In data.dsi_cls Where d.dsi_num = NroDoc).First

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function PreguntaPorOtor(ByVal NroOtor As Integer, ByVal rut As Long) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Consulta por Numero de Otorgamiento 
        'Creado por: Antonio Saldivar M.
        'Fecha Creacion: 20/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NumOto As opo_cls = (From o In data.opo_cls Where o.opo_otg = NroOtor And o.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(rut), Var.FMT_RUT) And o.ope_cls.id_P_0030 = 3).First
            Return NumOto
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function PreguntaPorDoc(ByVal NroDoc As Integer, ByVal rut As Long) As doc_cls
        '**************************************************************************************************************************************************
        'Descripcion: Consulta por Numero de Documento 
        'Creado por: Antonio Saldivar M.
        'Fecha Creacion: 20/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim data As New DataClsFactoringDataContext
            Dim NumDoc As doc_cls = (From d In data.doc_cls Where d.dsi_cls.dsi_num = NroDoc And d.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(rut), Var.FMT_RUT) And d.opo_cls.ope_cls.id_P_0030 = 3).First

            Return NumDoc

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function VerificaNOperacion(ByVal RutCli As Long, ByVal NOpe As Integer) As cxc_cls
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NOperacion As cxc_cls = (From c In data.cxc_cls Where c.cli_idc = RutCli And c.id_opo = NOpe).First
            Return NOperacion
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function VerificaNroCuenta(ByVal Nrocxc As Integer) As cxc_cls
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NroCuenta As cxc_cls = (From c In data.cxc_cls Where c.id_cxc = Nrocxc).First
            Return NroCuenta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function VerificaNroCuentaCXP(ByVal Nrocxp As Integer) As cxp_cls
        Try
            Dim data As New DataClsFactoringDataContext
            Dim NroCuenta As cxp_cls = (From c In data.cxp_cls Where c.id_cxp = Nrocxp).First
            Return NroCuenta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaGrillaCXC_Cliente(ByVal rut As Long, _
                                           ByVal Llenagrid As Boolean, _
                                           Optional ByVal GV As GridView = Nothing) As Object
        Try
            Dim data As New DataClsFactoringDataContext
            Dim CXC = From c In data.cxc_cls Where c.cli_idc = rut _
                                  Select New With {.pnu_des = c.p_0041_cls.pnu_des, _
                                                   .id_cxc = c.id_cxc, _
                                                   .cxc_fec = c.cxc_fec, _
                                                   .cxc_mto = c.cxc_mto, _
                                                   .cxc_int = c.cxc_int, _
                                                   .cxc_des = c.cxc_des, _
                                                   .cxc_fec_int = c.cxc_fec_int, _
                                                   .cxc_sal = c.cxc_sal, _
                                                   .Estado = c.P_0057_cls.pnu_des}


            If Llenagrid Then
                GV.DataSource = CXC
                GV.DataBind()
                Return Nothing
            Else
                Return CXC
            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DocumentosNOCedidosdevuelveTodos(ByVal RutClientedsd As Long, ByVal RutClihst As Long, _
                                                    ByVal Mon_dsd As Integer, ByVal Mon_hst As Integer, ByVal Fecha_dsd As String, _
                                                    ByVal Fecha_hst As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por A Saldivar.
        'Fecha Creacion: 30/03/2009
        'Quien Modifica             Fecha           Descripcion
        'A. Saldivar                201/12/2010     Se agrega paginacion
        'S. Henriquez               14/07/2012      Se agrega nro contrato
        'S. Henriquez               01/10/2012      Se corrige nro contrato
        '**************************************************************************************************************************************************
        Dim Coll As New Collection
        Try

            ' Dim val As Double
            Dim Var As New FuncionesGenerales.Variables
            Dim Sesion As New ClsSession.ClsSession
            Dim data As New DataClsFactoringDataContext

            Dim Cuentas = (From C In data.ing_sec_cls _
                           Join DC In data.doc_con_cls _
                           On C.nce_cls.id_doc Equals DC.id_doc _
                           Where (C.nce_cls.cli_idc >= Format(RutClientedsd, Var.FMT_RUT)) And _
                                     (C.nce_cls.cli_idc <= Format(RutClihst, Var.FMT_RUT)) And _
                                     (C.nce_cls.id_p_0023 >= Mon_dsd) And _
                                     (C.nce_cls.id_p_0023 <= Mon_hst) And _
                                     (C.nce_cls.nce_fec_ing >= Fecha_dsd) And _
                                     (C.nce_cls.nce_fec_ing <= Fecha_hst) And _
                                       C.id_P_0053 = 7 _
                             Select C.id_nce, _
                                    RutCli = C.cli_idc, _
                                    C.cli_cls.cli_rso, _
                                    C.cli_cls.cli_dig_ito, _
                                    C.cli_cls.cli_ape_ptn, _
                                    C.cli_cls.cli_ape_mtn, _
                                    RSocialCli = If(C.cli_cls.id_P_0044 = 1, _
                                                    C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                    C.cli_cls.cli_rso.Trim), _
                                   C.cli_cls.id_eje_cod_eje, _
                                   DesEje = C.cli_cls.eje_cls.eje_des_cra, _
                                   RutDeudor = C.nce_cls.deu_ide, _
                                   C.nce_cls.deu_cls.deu_dig_ito, _
                                   C.nce_cls.deu_cls.deu_nom, _
                                   C.nce_cls.deu_cls.deu_ape_ptn, _
                                   C.nce_cls.deu_cls.deu_ape_mtn, _
                                   RSocialDeu = If(C.nce_cls.deu_cls.id_P_0044 = 1, _
                                                   C.nce_cls.deu_cls.deu_rso.Trim & " " & C.nce_cls.deu_cls.deu_ape_ptn & " " & C.nce_cls.deu_cls.deu_ape_mtn, _
                                                   C.nce_cls.deu_cls.deu_rso.Trim), _
                                   C.nce_cls.nce_est_nom, _
                                   C.nce_cls.nce_mto, _
                                   C.nce_cls.id_p_0023, _
                                   SGMoneda = C.nce_cls.P_0023_cls.pnu_atr_003, _
                                   Moneda = C.nce_cls.P_0023_cls.pnu_des, _
                                   C.nce_cls.id_p_0031, _
                                   TipoDocumento = C.nce_cls.P_0031_cls.pnu_des, _
                                   C.nce_cls.nce_fec_ing, _
                                   C.nce_cls.nce_fec_vcto, _
                                   Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                               If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                               If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                   C.nce_cls.nce_fec_pft).Skip(Sesion.NroPaginacionDNC)



            For Each c In Cuentas.Take(12)
                Coll.Add(c)
            Next
            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

#End Region

#Region "CUENTAS POR PAGAR"

    Public Function CuentasPorPagarDevuelve(ByVal RutCliente As Long, ByVal TipoConsulta As Integer, _
                                            ByVal NroAplicacion As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar por su tipo de cuenta
        'Creado por: Jorge Lagos
        'Fecha Creacion: 10/03/2009
        'Quien Modifica                  Fecha              Descripcion
        'jlagos                         15-05-2014          se agrega numero de documento, antes lo iba a buscar por separado!!!!
        '**************************************************************************************************************************************************
        Try
            Dim Coll As New Collection
            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext


            Dim Cuentas = From C In data.cxp_cls _
                          Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                              (C.id_P_0057 = 1 Or C.id_P_0057 = 2) Order By C.id_cxp _
                          Select New With { _
                               C.id_P_0041, _
                               .TipoCuenta = C.p_0041_cls.pnu_des, _
                               C.id_cxp, _
                               C.cxp_fec, _
                               C.cxp_mto, _
                               C.cxp_des, _
                               C.id_opo, _
                               C.id_doc, _
                               .DSI_NUM = C.doc_cls.dsi_cls.dsi_num, _
                               C.id_P_0023, _
                               .Moneda = C.P_0023_cls.pnu_des, _
                               .Estado = C.P_0057_cls.pnu_des, _
                               .Cantidad_Egresos = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp And _
                                                                            (e.egr_vld_rcz = "I" Or _
                                                                             e.egr_vld_rcz = "S" Or _
                                                                             e.egr_vld_rcz = "V") And _
                                                                             e.egr_pro = "N" And _
                                                                             e.egr_cls.id_apl > 0 _
                                                                             Select e.id_egr_sec).Count, _
                               .Aplicacion = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp And _
                                                                               e.egr_cls.id_apl > 0 And _
                                                                               (e.egr_vld_rcz = "I" Or e.egr_vld_rcz = "V" Or e.egr_vld_rcz = "S") _
                                                                             Select e.id_egr_sec).Count, _
                                .Seleccion = 0}
            '05-08-2014 se agrega validacion aplicacion que este aprobada
            For Each x In Cuentas '.Take(900)
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CuentasPorPagarDevuelve(ByVal RutCliente As Long, _
                                           ByVal TipoCxP_Desde As Integer, ByVal TipoCxP_Hasta As Integer, _
                                           ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                           ByVal NroOtorg_Desde As Integer, ByVal NroOtorg_Hasta As Integer, _
                                           ByVal NroDocto_Desde As Integer, ByVal NroDocto_Hasta As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente
        'Creado por P Gatica .
        'Fecha Creacion: 22/12/2008
        'Quien Modifica                  Fecha              Descripcion
        'A Saldivar                      04/05/2009         Se Agregaron mas paramtros de busqueda
        '**************************************************************************************************************************************************
        Try
            Dim Coll As New Collection
            ' Dim val As Double
            Dim data As New DataClsFactoringDataContext

            Dim Cuentas = From C In data.cxp_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                (C.id_P_0041 >= TipoCxP_Desde) And _
                                (C.id_P_0041 <= TipoCxP_Hasta) And _
                                (C.id_P_0057 >= Estado_Desde) And _
                                (C.id_P_0057 <= Estado_Hasta) Order By C.id_cxp _
                                Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                C.id_cxp, _
                                C.cxp_fec, _
                                C.cxp_mto, _
                                C.cxp_des, _
                                C.id_opo, _
                                C.id_doc, _
                                Moneda = C.P_0023_cls.pnu_des, _
                                Estado = C.P_0057_cls.pnu_des, _
                                Cantidad = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp _
                                And (e.egr_vld_rcz = "I" _
                                Or e.egr_vld_rcz = "S" _
                                Or e.egr_vld_rcz = "V" _
                                Or e.egr_vld_rcz = "C") _
                                Select e.id_egr_sec).Count

            For Each x In Cuentas
                If (Val(x.id_opo) >= NroOtorg_Desde) And (Val(x.id_opo) <= NroOtorg_Hasta) And _
                   (Val(x.id_doc) >= NroDocto_Desde) And (Val(x.id_doc) <= NroDocto_Hasta) Then
                    Coll.Add(x)
                End If

            Next

            Return Coll

        Catch ex As Exception

        End Try

    End Function

    Public Function CuentasPorPagarDevuelve(ByVal RutCliente As Long, _
                                     ByVal TipoCxP As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Pagar de un Cliente por rut , tipo de cuenta , numero de otorgamiento y numero de documento
        'Creado por A Saldivar .
        'Fecha Creacion: 21/07/2009
        'Quien modifica          Fecha        Descripción
        'A Saldivar              16/11/2010   se deja consulta por todas las cuentas o por cuenta especifica
        'A Saldivar              20/12/2010   se agrega paginacion
        'S Henriquez             12/07/2012   Se agrega numero de contrato y se corrige numero documento. 
        'S Henriquez             01/10/2012   Se corrige numero contrato
        '**************************************************************************************************************************************************
        Dim Coll As New Collection
        Try

            Dim data As New DataClsFactoringDataContext
            'Dim cuentas As New Object
            Dim Sesion As New ClsSession.ClsSession
            If TipoCxP = 0 Then 'todas

                Dim cuentas = (From C In data.cxp_cls _
                               Join DC In data.doc_con_cls _
                               On C.id_doc Equals DC.id_doc _
                               Order By C.cxp_fec Where (C.cli_idc = Format(RutCliente, Var.FMT_RUT)) _
                                 Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                        C.id_cxp, _
                                        C.cli_idc, _
                                        C.cli_cls.cli_rso, _
                                        C.cli_cls.cli_ape_ptn, _
                                        C.cli_cls.cli_ape_mtn, _
                                        RSocial = If(C.cli_cls.id_P_0044 = 1, _
                                                             C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                             C.cli_cls.cli_rso.Trim), _
                                        C.cli_cls.id_eje_cod_eje, _
                                        DesEje = C.cli_cls.eje_cls.eje_des_cra, _
                                        C.cxp_fec, _
                                        C.cxp_fec_ctb, _
                                        C.cxp_mto, _
                                        C.cxp_des, _
                                        C.id_P_0023, _
                                        SGMoneda = C.P_0023_cls.pnu_atr_003, _
                                        Moneda = C.P_0023_cls.pnu_des, _
                                        C.id_doc, _
                                        C.id_opo, _
                                        Estado = C.P_0057_cls.pnu_des, _
                                        C.cxp_fac_cam, _
                                        Numero = C.doc_cls.dsi_cls.dsi_num, _
                                        Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                                    If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                                    If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                        Cantidad = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp _
                                        And (e.egr_vld_rcz = "I" _
                                        Or e.egr_vld_rcz = "S" _
                                        Or e.egr_vld_rcz = "V" _
                                        Or e.egr_vld_rcz = "C") _
                                        Select e.id_egr_sec)).Skip(Sesion.NroPaginacion)

                For Each x In cuentas.Take(8)
                    Coll.Add(x)
                Next

            Else
                Dim cuentas = (From C In data.cxp_cls _
                               Join DC In data.doc_con_cls _
                               On C.id_doc Equals DC.id_doc _
                               Where (C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                          (C.id_P_0041 = TipoCxP)) _
                       Select TipoCuenta = C.p_0041_cls.pnu_des, _
                       C.id_cxp, _
                       C.cli_idc, _
                       C.cli_cls.cli_rso, _
                       C.cli_cls.cli_ape_ptn, _
                       C.cli_cls.cli_ape_mtn, _
                        RSocial = If(C.cli_cls.id_P_0044 = 1, _
                                                             C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                             C.cli_cls.cli_rso.Trim), _
                       id_eje_cod_eje = C.id_eje, _
                       DesEje = (From e In data.eje_cls Where e.id_eje = C.id_eje Select e.eje_des_cra).First(), _
                       C.cxp_fec, _
                       C.cxp_fec_ctb, _
                       C.cxp_mto, _
                       C.cxp_des, _
                       C.id_P_0023, _
                       SGMoneda = C.P_0023_cls.pnu_atr_003, _
                       Moneda = C.P_0023_cls.pnu_des, _
                       C.id_doc, _
                       C.id_opo, _
                       Estado = C.P_0057_cls.pnu_des, _
                       C.cxp_fac_cam, _
                       Numero = C.doc_cls.dsi_cls.dsi_num, _
                       Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                   If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                   If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                       Cantidad = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp _
                       And (e.egr_vld_rcz = "I" _
                       Or e.egr_vld_rcz = "S" _
                       Or e.egr_vld_rcz = "V" _
                       Or e.egr_vld_rcz = "C") _
                       Select e.id_egr_sec)).Skip(Sesion.NroPaginacion)

                For Each x In cuentas.Take(8)
                    Coll.Add(x)
                Next
            End If

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CuentasPorPagarDevuelve(ByVal RutClientedsd As Long, ByVal RutClihst As Long, _
                                                     ByVal TipoCxP_Desde As Integer, ByVal TipoCxP_Hasta As Integer, _
                                                     ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                                     ByVal NroOtorg_Desde As Integer, ByVal NroOtorg_Hasta As Integer, _
                                                     ByVal NroDocto_Desde As Integer, ByVal NroDocto_Hasta As Integer, _
                                                     ByVal Mon_dsd As Integer, ByVal Mon_hst As Integer, ByVal Fecha_dsd As String, _
                                                     ByVal Fecha_hst As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente solo por rut y tipo de cuenta
        'Creado por J Lagos .
        'Fecha Creacion: 29/01/2009
        'Quien Modifica         Fecha               Descripcion
        'A Saldivar            30/03/2009           Se agregaron campos de busqueda(Moneda desde , Moneda Hasta, echa desde,Fecha Hsta) 
        '                                           y se agrego Razon Social cliente ,cod_eje, descripcion de ejecutivo
        'A Saldivar            20/12/2010           Se agrega paginacion
        'S Henriquez           14/07/2012           Se agrega nro contrato. 
        'S Henriquez           01/10/2012           Se corrige nro contrato.
        '**************************************************************************************************************************************************
        Dim Coll As New Collection
        Try

            ' Dim val As Double
            ' Dim Var As New FuncionesGenerales.Variables

            Dim data As New DataClsFactoringDataContext
            Dim Sesion As New ClsSession.ClsSession

            Dim Cuentas = (From C In data.cxp_cls _
                           Join DC In data.doc_con_cls _
                           On C.id_doc Equals DC.id_doc _
                           Where (C.cli_idc >= Format(RutClientedsd, Var.FMT_RUT)) And _
                                    (C.cli_idc <= Format(RutClihst, Var.FMT_RUT)) And _
                                    (C.id_P_0041 >= TipoCxP_Desde) And _
                                    (C.id_P_0041 <= TipoCxP_Hasta) And _
                                    (C.id_P_0057 >= Estado_Desde) And _
                                    (C.id_P_0057 <= Estado_Hasta) And _
                                    (C.id_P_0023 >= Mon_dsd) And _
                                    (C.id_P_0023 <= Mon_hst) And _
                                    (C.cxp_fec >= Fecha_dsd) And _
                                    (C.cxp_fec <= Fecha_hst) Order By C.id_cxp _
                                 Select TipoCuenta = C.p_0041_cls.pnu_des, _
                                 C.id_cxp, _
                                 C.cli_idc, _
                                 C.cli_cls.cli_dig_ito, _
                                 C.cli_cls.cli_rso, _
                                 C.cli_cls.cli_ape_ptn, _
                                 C.cli_cls.cli_ape_mtn, _
                                 RSocial = If(C.cli_cls.id_P_0044 = 1, _
                                                 C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                 C.cli_cls.cli_rso.Trim), _
                                 id_eje_cod_eje = C.id_eje, _
                                 DesEje = (From e In data.eje_cls Where e.id_eje = C.id_eje Select e.eje_des_cra).First(), _
                                 C.cxp_fec, _
                                 C.cxp_fec_ctb, _
                                 C.cxp_mto, _
                                 C.cxp_des, _
                                 C.id_P_0023, _
                                 SGMoneda = C.P_0023_cls.pnu_atr_003, _
                                 Moneda = C.P_0023_cls.pnu_des, _
                                 C.id_doc, _
                                 C.id_opo, _
                                 Estado = C.P_0057_cls.pnu_des, _
                                 C.cxp_fac_cam, _
                                 Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                             If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                             If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                                 Cantidad = (From e In data.egr_sec_cls Where e.id_cxp = C.id_cxp _
                                 And (e.egr_vld_rcz = "I" _
                                 Or e.egr_vld_rcz = "S" _
                                 Or e.egr_vld_rcz = "V" _
                                 Or e.egr_vld_rcz = "C") _
                                 Select e.id_egr_sec).Count).Skip(Sesion.NroPaginacionCXP)



            For Each x In Cuentas.Take(12)
                'If (Val(x.opo_otg) >= NroOtorg_Desde) And (Val(x.opo_otg) <= NroOtorg_Hasta) And _
                '   (Val(x.dsi_num) >= NroDocto_Desde) And (Val(x.dsi_num) <= NroDocto_Hasta) Then
                Coll.Add(x)
                '               End If

            Next
            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

#End Region

#Region "OTRAS CXP"

    Public Function CargaObjetoOtraCXP(ByVal RutCliente As Long, ByVal TpCuenta As Integer) As Collection

        Try
            Dim data As New DataClsFactoringDataContext
            Dim col As New Collection

            Dim Cuentas = From C In data.cxp_cls _
            Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And C.id_P_0041 = TpCuenta _
             Select C.p_0041_cls.pnu_des, _
            C.id_cxp, _
            C.cxp_fec, _
            Moneda = C.P_0023_cls.pnu_atr_003, _
            C.cxp_mto, _
            C.cxp_des, _
            Estado = C.P_0057_cls.pnu_des, _
            (From ing In data.ing_sec_cls Where ing.id_ing_sec = C.id_cxp _
                                          And ing.id_P_0053 = 1 _
                                          And ing.ing_pro = "N" _
                                          And (ing.ing_vld_rcz = "I" _
                                           Or ing.ing_vld_rcz = "S" _
                                           Or ing.ing_vld_rcz = "V" _
                                           Or ing.ing_vld_rcz = "C")).Count

            For Each p In Cuentas
                col.Add(p)

            Next

            If col.Count > 0 Then
                Return col

            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '*******************************************************
    'Verificar si la consulta de  pnu_atr_004 esta correcto*
    '*******************************************************
    Public Function TipoCuentaPorPagarDevuelve(ByVal Llenadrop As Boolean, _
                                Optional ByVal DP As DropDownList = Nothing) As Object
        Try
            Dim data As New DataClsFactoringDataContext
            Dim tpCuenta = From tp In data.p_0041_cls Where tp.pnu_est = "A" _
                           And tp.id_P_0041 > 3 _
                           And tp.pnu_atr_004 = 2 _
                           Or tp.pnu_atr_004 = 3 Select tp.id_P_0041, tp.pnu_des

            If Llenadrop Then
                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(tpCuenta, "id_P_0041", "pnu_des", DP, 0, "Seleccionar")
                Return Nothing
            Else
                Return tpCuenta
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "OTRAS CXC"

    Public Function CargaObjetoOtraCXC(ByVal RutCliente As Long, ByVal TpCuenta As Integer) As Collection

        Try
            Dim data As New DataClsFactoringDataContext
            Dim col As New Collection

            Dim Cuentas = From C In data.cxc_cls _
            Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And C.id_P_0041 = TpCuenta _
             Select C.p_0041_cls.pnu_des, _
            C.id_cxc, _
            C.cxc_fec, _
            C.cxc_mto, _
            C.cxc_int, _
            C.cxc_des, _
            C.cxc_fec_int, _
            C.cxc_sal, _
            Estado = C.P_0057_cls.pnu_des, _
            (From ing In data.ing_sec_cls Where ing.id_cxc = C.id_cxc _
            And ing.id_P_0053 = 1 And ing.ing_pro = "N" _
            And (ing.ing_vld_rcz = "I" _
            Or ing.ing_vld_rcz = "S" _
            Or ing.ing_vld_rcz = "V" _
            Or ing.ing_vld_rcz = "C")).Count

            For Each p In Cuentas
                col.Add(p)

            Next

            If col.Count > 0 Then
                Return col

            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

    Public Function NroDoctoDevuelve(ByVal id As String) As Integer
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve numero documento
        'Creado por : Sebastian Henriquez C.
        'Fecha Creacion: 12/07/2012
        'Quien Modifica             Fecha           Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext

            Dim nro = (From D In Data.doc_cls Where D.dsi_cls.dsi_num = id Select D.id_doc).First

            Return nro

        Catch ex As Exception
            Return 0
        End Try

    End Function

#End Region

#Region "Actualizaciones Cuentas"

#Region "Cuentas X Cobrar"

    Public Function CxcInserta(ByVal Id_obj As cxc_cls) As Integer

        'Modificado por Pablo Gatica
        'Se agrega un integer como valor , para que retorne el ultimo id ingresado con el fin 
        ' de generar el Nub de Operaciones
        Try
            Dim data As New DataClsFactoringDataContext
            data.cxc_cls.InsertOnSubmit(Id_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CXCModificaEstado(ByVal idCXC As Integer, _
                                 ByVal Estado As String) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModiCXC As cxc_cls = (From c In data.cxc_cls Where c.id_cxc = idCXC).First
            With ModiCXC
                .id_P_0057 = Estado
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CXCElimina(ByVal Id As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim CXCEli As cxc_cls = (From c In data.cxc_cls Where c.id_cxc = Id).First
            data.cxc_cls.DeleteOnSubmit(CXCEli)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "Cuentas X Pagar"

    Public Function CXPInserta(ByVal Id_obj As cxp_cls) As Integer
        'Modificado por Pablo Gatica
        'Se agrega un integer como valor , para que retorne el ultimo id ingresado con el fin 
        ' de generar el Nub de Operaciones
        Try
            Dim data As New DataClsFactoringDataContext
            data.cxp_cls.InsertOnSubmit(Id_obj)
            data.SubmitChanges()
            Return Id_obj.id_cxp
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function CXPModificaEstado(ByVal idCXP As Integer, _
                                ByVal Estado As String) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModiCXP As cxp_cls = (From c In data.cxp_cls Where c.id_cxp = idCXP).First
            With ModiCXP
                .id_P_0057 = Estado
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CXPElimina(ByVal Id As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim CXPEli As cxp_cls = (From c In data.cxp_cls Where c.id_cxp = Id).First
            data.cxp_cls.DeleteOnSubmit(CXPEli)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



#End Region

#End Region

End Class
