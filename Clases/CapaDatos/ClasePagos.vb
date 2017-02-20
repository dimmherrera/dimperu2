Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Transactions
Imports ClsSession.SesionOperaciones
Imports CapaDatos
Public Class ClasePagos

    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables


#Region "Consultas Pagos"

    Public Function Colilla_Devuelve(ByVal Custodia As Integer, _
                                         ByVal FDesde As DateTime, _
                                         ByVal FHasta As DateTime) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Cheque Por Custodia ,Fecha_Desde y Fecha_Hasta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'Antonio Saldivar            19/02/2009         no agrupa 
        '                                               no suma mxontos
        'A. Saldivar                 09/02/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim chr = ((From c In data.chr_cls Where c.id_P_0112 = Custodia _
                      And c.id_P_0113 = 4 _
                      And c.cdp_cls.cdp_fec >= FDesde _
                      And c.cdp_cls.cdp_fec <= FHasta _
                      Select c.cdp_cls.id_cdp, _
                      c.cdp_cls.cdp_mto, _
                      c.cdp_cls.cdp_fec).Distinct).Skip(sesion.NroPaginacion).Take(2)


            '    And c.id_P_0113 = 3 _
            Return chr

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DocumentosOtorgagosPagos_RetornaDoctos(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                            ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                            ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, _
                                                            ByVal tipo_docto1 As Integer, ByVal tipo_docto2 As Integer, _
                                                            ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                            ByVal nro_cuota1 As Integer, ByVal nro_cuota2 As Integer, _
                                                            ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                            ByVal estado1 As Integer, ByVal estado2 As Integer, ByVal estado3 As Integer, _
                                                            ByVal estado4 As Integer, ByVal estado5 As Integer, ByVal estado6 As Integer, _
                                                            ByVal estado7 As Integer, ByVal estado8 As Integer, ByVal estado9 As Integer, _
                                                            ByVal estado10 As Integer, ByVal estado11 As Integer, ByVal estado12 As Integer, _
                                                            Optional ByVal Mto_dsd As Long = 0, Optional ByVal Mto_hst As Long = 999999999999, _
                                                            Optional ByVal NroAplicacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documentos que se pueden pagar 
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/09/2010         Se agrega rango por monto(opcional)
        'A. Saldivar                 22/12/2010         Se reduce numero de paginacion (de 15 a 6)        
        '**************************************************************************************************************************************************************

        Try

            Dim Coll As New Collection
            Dim Sesion As New ClsSession.ClsSession
            Dim Data As New DataClsFactoringDataContext

            Data.ObjectTrackingEnabled = False
            Dim FD, FH As String

            FD = Format(fec_vcto1, "yyyy/MM/dd") & " 00:00:01"
            FH = Format(fec_vcto2, "yyyy/MM/dd") & " 23:59:59"


            Dim Temporal_doc = (From doc1 In Data.doc_cls _
            Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                  (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
                  (doc1.opo_cls.opo_otg >= nro_operacion1 And doc1.opo_cls.opo_otg <= nro_operacion2) And _
                  (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                  (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
                  (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                  (doc1.dsi_cls.dsi_fev_rea >= FD And doc1.dsi_cls.dsi_fev_rea <= FH) And _
                  (doc1.dsi_cls.dsi_mto >= Mto_dsd And doc1.dsi_cls.dsi_mto <= Mto_hst) And _
                  (doc1.dsi_cls.id_P_0011 = estado1 Or _
                   doc1.dsi_cls.id_P_0011 = estado2 Or _
                   doc1.dsi_cls.id_P_0011 = estado3 Or _
                   doc1.dsi_cls.id_P_0011 = estado4 Or _
                   doc1.dsi_cls.id_P_0011 = estado5 Or _
                   doc1.dsi_cls.id_P_0011 = estado6 Or _
                   doc1.dsi_cls.id_P_0011 = estado7 Or _
                   doc1.dsi_cls.id_P_0011 = estado8 Or _
                   doc1.dsi_cls.id_P_0011 = estado9 Or _
                   doc1.dsi_cls.id_P_0011 = estado10 Or _
                   doc1.dsi_cls.id_P_0011 = estado11 Or _
                   doc1.dsi_cls.id_P_0011 = estado12) And _
                   doc1.dsi_cls.dsi_flj = "N" _
             Select doc1.id_doc, doc1.doc_sdo_cli, doc1.dsi_cls.ope_cls.opn_cls.id_P_0012).Skip(Sesion.NroPaginacion)

            For Each D In Temporal_doc.Take(15)

                Dim Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = D.id_doc And _
                                                                           I.id_P_0053 = 2 And _
                                                                          (I.ing_vld_rcz = "S" Or _
                                                                             I.ing_vld_rcz = "I" Or _
                                                                             I.ing_vld_rcz = "V" Or _
                                                                             I.ing_vld_rcz = "C" Or _
                                                                             I.ing_vld_rcz = "L") And _
                                                                             I.ing_pro = "N" _
                                                                     Select (I.ing_mto_abo / I.ing_fac_cam)).Sum
                If Ingresos Is Nothing Then Ingresos = 0

                Dim Doctos = From doc1 In Data.doc_cls Where doc1.id_doc = D.id_doc And ((D.doc_sdo_cli - Ingresos) > 0 Or D.id_P_0012 = 3) _
            Select New With {doc1.id_doc, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                             doc1.dsi_cls.deu_ide, _
                            .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn.Trim.ToUpper, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper), _
                            .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                             doc1.id_opo, _
                             doc1.opo_cls.opo_otg, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
                            .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                             doc1.dsi_cls.id_P_0011, _
                            .EstadoDocto = doc1.dsi_cls.P_0011_cls.pnu_des, _
                             doc1.dsi_cls.dsi_num, _
                             doc1.dsi_cls.dsi_mto, _
                             doc1.dsi_cls.dsi_flj, _
                             doc1.dsi_cls.dsi_flj_num, _
                             doc1.doc_num_ren, _
                             doc1.dsi_cls.dsi_cei, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0012, _
                             doc1.opo_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                             doc1.dsi_cls.dsi_ntf, _
                             doc1.dsi_cls.id_P_0040, _
                            .EstadoVerifica = doc1.dsi_cls.P_0040_cls.pnu_des, _
                             doc1.dsi_cls.dsi_fec_emi, _
                            .doc_fev_ori = doc1.dsi_cls.dsi_fev_ori, _
                            .doc_fev = doc1.dsi_cls.dsi_fev, _
                            .doc_fev_rea = doc1.dsi_cls.dsi_fev_rea, _
                             doc1.dsi_cls.dsi_mto_ant, _
                             doc1.dsi_cls.dsi_ctd_dia, _
                             doc1.dsi_cls.dsi_pre_com, _
                             doc1.dsi_cls.dsi_dif_pre, _
                             doc1.dsi_cls.dsi_sal_pen, _
                             doc1.dsi_cls.dsi_sal_pag, _
                             doc1.dsi_cls.dsi_cms, _
                             doc1.dsi_cls.dsi_iva_cms, _
                             doc1.doc_gto, _
                             doc1.doc_ful_pgo, _
                             doc1.doc_int_dvg, _
                             doc1.doc_ful_dvg, _
                             doc1.doc_fct, _
                             doc1.id_suc_cbz, _
                            .SucCobranza = doc1.suc_cls.suc_des_cra, _
                             doc1.id_suc_rcd, _
                            .SucRecauda = doc1.suc_cls.suc_des_cra, _
                             doc1.id_cco, _
                             doc1.cco_cls.cco_num, _
                            .EstadoCobranza = doc1.cco_cls.cco_des, _
                             doc1.doc_fec_cco, _
                             doc1.cco_cls.cco_des, _
                             doc1.dsi_cls.dsi_cbz, _
                             doc1.dsi_cls.dsi_cbz_son, _
                             doc1.dsi_cls.id_PL_000047, _
                             .doc_sdo_cli = (doc1.doc_sdo_cli - Ingresos), _
                             .doc_sdo_ddr = (doc1.doc_sdo_ddr - Ingresos), _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                             doc1.doc_not_cre, _
                            .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                            .tasa = 0.0, _
                            .nota_cred = 0.0, _
                            .interes = 0.0, _
                            .MontoPagar = 0.0, _
                            .PagaDeudor = "S", _
                            doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa, _
                             doc1.opo_cls.ope_cls.ope_fac_cam, _
                             doc1.opo_cls.ope_cls.ope_fec_sim, _
                             doc1.opo_cls.ope_cls.ope_dif_pre, _
                             doc1.opo_cls.ope_cls.ope_lnl, _
                             doc1.doc_tas_ren, _
                             doc1.dsi_cls.dsi_fev_ori, _
                            .Cantidad_Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                           I.id_P_0053 = 2 And _
                                                                          (I.ing_vld_rcz = "I" Or _
                                                                           I.ing_vld_rcz = "V" Or _
                                                                           I.ing_vld_rcz = "C") And _
                                                                           I.ing_pro = "N" _
                                                   Select I.id_ing_sec).Count, _
                            .Aplicacion = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                            I.id_P_0053 = 2 And _
                                                                            I.egr_sec_cls.egr_cls.id_apl = NroAplicacion _
                                            Select I.id_ing_sec).Count}

                For Each P In Doctos
                    Coll.Add(P)
                Next


            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosOtorgagosPagos_RetornaDoctos_Prorroga(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                                   ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                                   ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, _
                                                                   ByVal tipo_docto1 As Integer, ByVal tipo_docto2 As Integer, _
                                                                   ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                                   ByVal nro_cuota1 As Integer, ByVal nro_cuota2 As Integer, _
                                                                   ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                                   ByVal estado1 As Integer, ByVal estado2 As Integer, ByVal estado3 As Integer, _
                                                                   ByVal estado4 As Integer, ByVal estado5 As Integer, ByVal estado6 As Integer, _
                                                                   ByVal estado7 As Integer, ByVal estado8 As Integer, ByVal estado9 As Integer, _
                                                                   ByVal estado10 As Integer, ByVal estado11 As Integer, ByVal estado12 As Integer, _
                                                                   Optional ByVal Mto_dsd As Long = 0, Optional ByVal Mto_hst As Long = 999999999999, _
                                                                   Optional ByVal NroAplicacion As Integer = 0, Optional ByVal Paginacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documentos que se pueden pagar 
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/09/2010         Se agrega rango por monto(opcional)
        'A. Saldivar                 22/12/2010         Se reduce numero de paginacion (de 15 a 6)        
        'J. Lagos                    04/06/2012         se agrega dias antes de vcto. para prorroga
        'S. Henriquez                23/07/2012         Se corrige variable FD se concatenaba un segundo menos, por lo cual no buscaba datos para un solo dia
        '**************************************************************************************************************************************************************

        Try

            Dim Coll As New Collection
            Dim Sesion As New ClsSession.ClsSession
            Dim Data As New DataClsFactoringDataContext
            Dim FD, FH As String

            FD = Format(fec_vcto1, "yyyy/MM/dd") & " 00:00:00"
            FH = Format(fec_vcto2, "yyyy/MM/dd") & " 23:59:59"

            Data.ObjectTrackingEnabled = False


            Dim Temporal_doc = (From doc1 In Data.doc_cls _
                                  Join DC In Data.doc_con_cls On doc1.id_doc Equals DC.id_doc _
            Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                  (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
                  (doc1.opo_cls.ope_cls.id_opn >= nro_operacion1 And doc1.opo_cls.ope_cls.id_opn <= nro_operacion2) And _
                  (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                  (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
                  (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                  (doc1.dsi_cls.dsi_fev_rea >= FD And doc1.dsi_cls.dsi_fev_rea <= FH) And _
                  (doc1.dsi_cls.dsi_mto >= Mto_dsd And doc1.dsi_cls.dsi_mto <= Mto_hst) And _
                  (doc1.dsi_cls.id_P_0011 = estado1 Or _
                   doc1.dsi_cls.id_P_0011 = estado2 Or _
                   doc1.dsi_cls.id_P_0011 = estado3 Or _
                   doc1.dsi_cls.id_P_0011 = estado4 Or _
                   doc1.dsi_cls.id_P_0011 = estado5 Or _
                   doc1.dsi_cls.id_P_0011 = estado6 Or _
                   doc1.dsi_cls.id_P_0011 = estado7 Or _
                   doc1.dsi_cls.id_P_0011 = estado8 Or _
                   doc1.dsi_cls.id_P_0011 = estado9 Or _
                   doc1.dsi_cls.id_P_0011 = estado10 Or _
                   doc1.dsi_cls.id_P_0011 = estado11 Or _
                   doc1.dsi_cls.id_P_0011 = estado12) And _
                   doc1.dsi_cls.dsi_flj = "N" _
                   Order By doc1.dsi_cls.dsi_num _
                   Select New With {doc1.id_doc, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                             doc1.dsi_cls.deu_ide, _
                             doc1.dsi_cls.deu_cls.deu_dig_ito, _
                            .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn.Trim.ToUpper, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper), _
                            .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                             doc1.id_opo, _
                             doc1.opo_cls.opo_otg, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
                            .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                             doc1.dsi_cls.id_P_0011, _
                            .EstadoDocto = doc1.dsi_cls.P_0011_cls.pnu_des, _
                             doc1.dsi_cls.dsi_num, _
                             doc1.dsi_cls.dsi_mto, _
                             doc1.dsi_cls.dsi_flj, _
                             doc1.dsi_cls.dsi_flj_num, _
                             doc1.doc_num_ren, _
                             doc1.dsi_cls.dsi_cei, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0012, _
                             doc1.opo_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                             doc1.dsi_cls.dsi_ntf, _
                             doc1.dsi_cls.id_P_0040, _
                            .EstadoVerifica = doc1.dsi_cls.P_0040_cls.pnu_des, _
                             doc1.dsi_cls.dsi_fec_emi, _
                            .doc_fev_ori = doc1.dsi_cls.dsi_fev_ori, _
                            .doc_fev = doc1.dsi_cls.dsi_fev, _
                            .doc_fev_rea = doc1.dsi_cls.dsi_fev_cal, _
                             doc1.dsi_cls.dsi_mto_ant, _
                             doc1.dsi_cls.dsi_ctd_dia, _
                             doc1.dsi_cls.dsi_pre_com, _
                             doc1.dsi_cls.dsi_dif_pre, _
                             doc1.dsi_cls.dsi_sal_pen, _
                             doc1.dsi_cls.dsi_sal_pag, _
                             doc1.dsi_cls.dsi_cms, _
                             doc1.dsi_cls.dsi_iva_cms, _
                             doc1.doc_gto, _
                             doc1.doc_ful_pgo, _
                             doc1.doc_int_dvg, _
                             doc1.doc_ful_dvg, _
                             doc1.doc_fct, _
                             .fec_ven = doc1.dsi_cls.dsi_fev_rea, _
                             doc1.id_suc_cbz, _
                            .SucCobranza = doc1.suc_cls.suc_des_cra, _
                             doc1.id_suc_rcd, _
                            .SucRecauda = doc1.suc_cls.suc_des_cra, _
                             doc1.id_cco, _
                             doc1.cco_cls.cco_num, _
                            .EstadoCobranza = doc1.cco_cls.cco_des, _
                             doc1.doc_fec_cco, _
                             doc1.cco_cls.cco_des, _
                             doc1.dsi_cls.dsi_cbz, _
                             doc1.dsi_cls.dsi_cbz_son, _
                             doc1.dsi_cls.id_PL_000047, _
                             .doc_sdo_cli = doc1.doc_sdo_cli, _
                            .doc_sdo_ddr = doc1.doc_sdo_ddr, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                             doc1.doc_not_cre, _
                            .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                            .tasa = 0.0, _
                            .nota_cred = 0.0, _
                            .Interes = 0.0, _
                            .InteresDevolver = 0.0, _
                            .MontoPagar = 0.0, _
                            .PagaDeudor = "S", _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa, _
                             doc1.opo_cls.ope_cls.ope_fac_cam, _
                             doc1.opo_cls.ope_cls.ope_fec_sim, _
                             doc1.opo_cls.ope_cls.ope_dif_pre, _
                             doc1.opo_cls.ope_cls.ope_lnl, _
                             doc1.doc_tas_ren, _
                             doc1.dsi_cls.dsi_fev_ori, _
                             doc1.opo_cls.ope_cls.id_opn, _
                             doc1.dsi_cls.dsi_fev_cal, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_bas, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_spr_ead, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_pto_spr, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_neg, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
                             .Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                                   If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                                   If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                            .Cantidad_Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                               I.id_P_0053 = 2 And _
                                                                              (I.ing_vld_rcz = "S" Or _
                                                                               I.ing_vld_rcz = "I" Or _
                                                                               I.ing_vld_rcz = "V" Or _
                                                                               I.ing_vld_rcz = "C" Or _
                                                                               I.ing_vld_rcz = "L") And _
                                                                               I.ing_pro = "N" _
                                             Select (I.ing_mto_abo / I.ing_fac_cam)).Sum, _
                            .Aplicacion = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                            I.id_P_0053 = 2 And _
                                                                            I.egr_sec_cls.egr_cls.id_apl > 0 _
                                            Select I.id_ing_sec).Count}).Skip(Paginacion).Take(200)

            For Each d In Temporal_doc
                Coll.Add(d)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosOtorgagosPagos_RetornaDoctos_WC(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                              ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                              ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, _
                                                              ByVal tipo_docto1 As Integer, ByVal tipo_docto2 As Integer, _
                                                              ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                              ByVal nro_cuota1 As Integer, ByVal nro_cuota2 As Integer, _
                                                              ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                              ByVal estado1 As Integer, ByVal estado2 As Integer, ByVal estado3 As Integer, _
                                                              ByVal estado4 As Integer, ByVal estado5 As Integer, ByVal estado6 As Integer, _
                                                              ByVal estado7 As Integer, ByVal estado8 As Integer, ByVal estado9 As Integer, _
                                                              ByVal estado10 As Integer, ByVal estado11 As Integer, ByVal estado12 As Integer, _
                                                              Optional ByVal Mto_dsd As Long = 0, Optional ByVal Mto_hst As Long = 999999999999, _
                                                              Optional ByVal Cobranza As Integer = 0, Optional ByVal Paginacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documentos que se pueden pagar 
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/09/2010         Se agrega rango por monto(opcional)
        'A. Saldivar                 22/12/2010         Se reduce numero de paginacion (de 15 a 6) 
        'S. Henriquez                24/07/2012         Se agrega numero de contrato
        'J. Lagos                    28/08/2012         Se quita nro de aplicacion
        'S. Henriquez                01/10/2012         Se corrige nro contrato
        'J. Lagos                    04/02/2014         Se agrega condicion, para que no traiga doctos antes de su fecha de otorgamiento
        '**************************************************************************************************************************************************************

        Try

            Dim Coll As New Collection
            Dim Sesion As New ClsSession.ClsSession
            Dim Data As New DataClsFactoringDataContext
            Dim Pagos As New ClsSession.SesionPagos
            Dim FD, FH As String
            Dim FD1, FH1 As String

            FD = Format(fec_vcto1, "yyyy/MM/dd") & " 00:00:00"
            FH = Format(fec_vcto2, "yyyy/MM/dd") & " 23:59:59"

            Data.ObjectTrackingEnabled = False

            FH1 = Format(Pagos.FechaPago, "yyyy/MM/dd") & " 23:59:59"

            Dim Temporal_doc = (From doc1 In Data.doc_cls _
                                  Join DC In Data.doc_con_cls On doc1.id_doc Equals DC.id_doc _
            Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                  (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
                  (doc1.opo_cls.ope_cls.id_opn >= nro_operacion1 And doc1.opo_cls.ope_cls.id_opn <= nro_operacion2) And _
                  (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                  (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
                  (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                  (doc1.dsi_cls.dsi_fev_rea >= FD And doc1.dsi_cls.dsi_fev_rea <= FH) And _
                  (doc1.dsi_cls.dsi_mto >= Mto_dsd And doc1.dsi_cls.dsi_mto <= Mto_hst) And _
                  (doc1.dsi_cls.id_P_0011 = estado1 Or _
                   doc1.dsi_cls.id_P_0011 = estado2 Or _
                   doc1.dsi_cls.id_P_0011 = estado3 Or _
                   doc1.dsi_cls.id_P_0011 = estado4 Or _
                   doc1.dsi_cls.id_P_0011 = estado5 Or _
                   doc1.dsi_cls.id_P_0011 = estado6 Or _
                   doc1.dsi_cls.id_P_0011 = estado7 Or _
                   doc1.dsi_cls.id_P_0011 = estado8 Or _
                   doc1.dsi_cls.id_P_0011 = estado9 Or _
                   doc1.dsi_cls.id_P_0011 = estado10 Or _
                   doc1.dsi_cls.id_P_0011 = estado11 Or _
                   doc1.dsi_cls.id_P_0011 = estado12) And _
                   doc1.dsi_cls.dsi_flj = "N" And _
                   doc1.opo_cls.opo_fec_oto <= FH1 _
                   Order By doc1.dsi_cls.dsi_num, doc1.dsi_cls.dsi_flj_num _
                   Select New With {doc1.id_doc, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                             doc1.dsi_cls.deu_ide, _
                            .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn.Trim.ToUpper, _
                                                          doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper), _
                            .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                                          doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                             doc1.id_opo, _
                             doc1.opo_cls.opo_otg, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
                            .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                             doc1.dsi_cls.id_P_0011, _
                            .EstadoDocto = doc1.dsi_cls.P_0011_cls.pnu_des, _
                             doc1.dsi_cls.dsi_num, _
                             doc1.dsi_cls.dsi_mto, _
                             doc1.dsi_cls.dsi_flj, _
                             doc1.dsi_cls.dsi_flj_num, _
                             doc1.doc_num_ren, _
                             doc1.dsi_cls.dsi_cei, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0012, _
                             doc1.opo_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                             doc1.dsi_cls.dsi_ntf, _
                             doc1.dsi_cls.id_P_0040, _
                            .EstadoVerifica = doc1.dsi_cls.P_0040_cls.pnu_des, _
                             doc1.dsi_cls.dsi_fec_emi, _
                            .doc_fev_ori = doc1.dsi_cls.dsi_fev_ori, _
                            .doc_fev = doc1.dsi_cls.dsi_fev, _
                            .doc_fev_rea = doc1.dsi_cls.dsi_fev_cal, _
                             doc1.dsi_cls.dsi_mto_ant, _
                             doc1.dsi_cls.dsi_ctd_dia, _
                             doc1.dsi_cls.dsi_pre_com, _
                             doc1.dsi_cls.dsi_dif_pre, _
                             doc1.dsi_cls.dsi_sal_pen, _
                             doc1.dsi_cls.dsi_sal_pag, _
                             doc1.dsi_cls.dsi_cms, _
                             doc1.dsi_cls.dsi_iva_cms, _
                             doc1.doc_gto, _
                             doc1.doc_ful_pgo, _
                             doc1.doc_int_dvg, _
                             doc1.doc_ful_dvg, _
                             doc1.doc_fct, _
                             .fec_ven = doc1.dsi_cls.dsi_fev_rea, _
                             doc1.id_suc_cbz, _
                            .SucCobranza = doc1.suc_cls.suc_des_cra, _
                             doc1.id_suc_rcd, _
                            .SucRecauda = doc1.suc_cls.suc_des_cra, _
                             doc1.id_cco, _
                             doc1.cco_cls.cco_num, _
                            .EstadoCobranza = doc1.cco_cls.cco_des, _
                             doc1.doc_fec_cco, _
                             doc1.cco_cls.cco_des, _
                             doc1.dsi_cls.dsi_cbz, _
                             doc1.dsi_cls.dsi_cbz_son, _
                             doc1.dsi_cls.id_PL_000047, _
                             .doc_sdo_cli = doc1.doc_sdo_cli, _
                            .doc_sdo_ddr = doc1.doc_sdo_ddr, _
                             doc1.doc_sdo_exc, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                             doc1.doc_not_cre, _
                            .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                            .Saldo = 0.0, _
                            .tasa = 0.0, _
                            .nota_cred = 0.0, _
                            .Interes = 0.0, _
                            .InteresDevolver = 0.0, _
                            .MontoPagar = 0.0, _
                            .PagaDeudor = "S", _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa, _
                             doc1.opo_cls.ope_cls.ope_fac_cam, _
                             doc1.opo_cls.ope_cls.ope_fec_sim, _
                             doc1.opo_cls.ope_cls.ope_dif_pre, _
                             doc1.opo_cls.ope_cls.ope_lnl, _
                             doc1.doc_tas_ren, _
                             doc1.dsi_cls.dsi_fev_ori, _
                             doc1.opo_cls.ope_cls.id_opn, _
                             doc1.dsi_cls.dsi_fev_cal, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_bas, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_spr_ead, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_pto_spr, _
                             doc1.opo_cls.ope_cls.opn_cls.opn_tas_neg, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
                             doc1.dsi_cls.deu_cls.deu_dig_ito, _
                             .Contrato = (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                                   If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                                   If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "")), _
                            .Cantidad_Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                               I.id_P_0053 = 2 And _
                                                                              (I.ing_vld_rcz = "S" Or _
                                                                               I.ing_vld_rcz = "I" Or _
                                                                               I.ing_vld_rcz = "V" Or _
                                                                               I.ing_vld_rcz = "C" Or _
                                                                               I.ing_vld_rcz = "L") And _
                                                                               I.ing_pro = "N" _
                                             Select (I.ing_mto_abo / I.ing_fac_cam)).Sum, _
                            .Aplicacion = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                            I.id_P_0053 = 2 And _
                                                                            I.egr_sec_cls.egr_cls.id_apl > 0 _
                                            Select I.id_ing_sec).Count}).Skip(Paginacion).Take(200)

            For Each d In Temporal_doc
                If Pagos.Pagador = "D" Then
                    d.Saldo = d.doc_sdo_ddr
                    If d.doc_sdo_ddr > 0 Then
                        Coll.Add(d)
                    End If
                Else
                    d.Saldo = d.doc_sdo_cli
                    If d.doc_sdo_cli > 0 Then
                        Coll.Add(d)
                    End If
                End If
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function DocumentosOtorgagosPagos_RetornaDoctos_WC_Total(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                                    ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                                    ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, _
                                                                    ByVal tipo_docto1 As Integer, ByVal tipo_docto2 As Integer, _
                                                                    ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                                    ByVal nro_cuota1 As Integer, ByVal nro_cuota2 As Integer, _
                                                                    ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                                    ByVal estado1 As Integer, ByVal estado2 As Integer, ByVal estado3 As Integer, _
                                                                    ByVal estado4 As Integer, ByVal estado5 As Integer, ByVal estado6 As Integer, _
                                                                    ByVal estado7 As Integer, ByVal estado8 As Integer, ByVal estado9 As Integer, _
                                                                    ByVal estado10 As Integer, ByVal estado11 As Integer, ByVal estado12 As Integer, _
                                                                    Optional ByVal Mto_dsd As Long = 0, Optional ByVal Mto_hst As Long = 999999999999, _
                                                                    Optional ByVal Cobranza As Integer = 0, Optional ByVal Paginacion As Integer = 0) As Long

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve la cantidad de registros, para paginacion
        'Creado por Jorge Lagos
        'Fecha Creacion: 02/04/2015
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************************

        Try

            Dim Coll As New Collection
            Dim Sesion As New ClsSession.ClsSession
            Dim Data As New DataClsFactoringDataContext
            Dim Pagos As New ClsSession.SesionPagos
            Dim FD, FH As String
            Dim FD1, FH1 As String

            FD = Format(fec_vcto1, "yyyy/MM/dd") & " 00:00:00"
            FH = Format(fec_vcto2, "yyyy/MM/dd") & " 23:59:59"

            Data.ObjectTrackingEnabled = False

            FH1 = Format(Pagos.FechaPago, "yyyy/MM/dd") & " 23:59:59"

            Dim Temporal_doc = (From doc1 In Data.doc_cls _
                                  Join DC In Data.doc_con_cls On doc1.id_doc Equals DC.id_doc _
            Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                  (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
                  (doc1.opo_cls.ope_cls.id_opn >= nro_operacion1 And doc1.opo_cls.ope_cls.id_opn <= nro_operacion2) And _
                  (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                  (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
                  (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                  (doc1.dsi_cls.dsi_fev_rea >= FD And doc1.dsi_cls.dsi_fev_rea <= FH) And _
                  (doc1.dsi_cls.dsi_mto >= Mto_dsd And doc1.dsi_cls.dsi_mto <= Mto_hst) And _
                  (doc1.dsi_cls.id_P_0011 = estado1 Or _
                   doc1.dsi_cls.id_P_0011 = estado2 Or _
                   doc1.dsi_cls.id_P_0011 = estado3 Or _
                   doc1.dsi_cls.id_P_0011 = estado4 Or _
                   doc1.dsi_cls.id_P_0011 = estado5 Or _
                   doc1.dsi_cls.id_P_0011 = estado6 Or _
                   doc1.dsi_cls.id_P_0011 = estado7 Or _
                   doc1.dsi_cls.id_P_0011 = estado8 Or _
                   doc1.dsi_cls.id_P_0011 = estado9 Or _
                   doc1.dsi_cls.id_P_0011 = estado10 Or _
                   doc1.dsi_cls.id_P_0011 = estado11 Or _
                   doc1.dsi_cls.id_P_0011 = estado12) And _
                   doc1.dsi_cls.dsi_flj = "N" And _
                   doc1.opo_cls.opo_fec_oto <= FH1 _
                   Order By doc1.dsi_cls.dsi_num, doc1.dsi_cls.dsi_flj_num).Count



            Return Temporal_doc

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function DocumentosARecaudar_RetornaDoctos_WC(ByVal id_hre As Integer, Optional ByVal NroAplicacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documentos que se pueden pagar 
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/09/2010         Se agrega rango por monto(opcional)
        'A. Saldivar                 22/12/2010         Se reduce numero de paginacion (de 15 a 6)        
        '**************************************************************************************************************************************************************
        Dim col As New Collection
        Try

            Dim data As New DataClsFactoringDataContext

            Dim doc = From c In data.drc_cls Where c.hre_cls.id_hre = id_hre Select _
                       c.gsn_cls.doc_cls.id_doc, _
                                             c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                                             Nombre = If(c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                         c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim & " " & c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim & " " & c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim, _
                                                         c.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim), _
                                             RutDeu = c.gsn_cls.doc_cls.dsi_cls.deu_ide, _
                                             NombreDeu = c.gsn_cls.doc_cls.dsi_cls.deu_cls.deu_rso.Trim, _
                                             c.gsn_cls.doc_cls.opo_cls.ope_cls.opn_cls.id_P_0031, _
                                             c.gsn_cls.doc_cls.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                                             c.gsn_cls, _
                                            c.gsn_cls.doc_cls.dsi_cls.dsi_num, _
                                            c.gsn_cls.doc_cls.dsi_cls.id_P_0011, _
                                                 c.gsn_cls.doc_cls.dsi_cls.deu_ide, _
                                            doc_fev_rea = c.gsn_cls.doc_cls.dsi_cls.dsi_fev_rea, _
                                            c.gsn_cls.doc_cls.doc_sdo_cli, _
                                            c.gsn_cls.doc_cls.doc_sdo_ddr, _
                                            c.gsn_cls.doc_cls.dsi_cls.dsi_mto, _
                                            c.gsn_cls.doc_cls.dsi_cls.dsi_fev_rea, _
                                            c.gsn_cls.doc_cls.dsi_cls.ope_cls.ope_fac_cam, _
                                            c.gsn_cls.doc_cls.doc_num_ren



            For Each p In doc
                col.Add(p)
            Next
        Catch ex As Exception

        End Try

        Dim Coll As New Collection
        Try


            For X = 1 To col.Count



                Dim Sesion As New ClsSession.ClsSession
                Dim Data As New DataClsFactoringDataContext

                Data.ObjectTrackingEnabled = False
                Dim FD, FH As String


                Dim nro_doc As String
                nro_doc = col.Item(X).id_doc

                Dim Temporal_doc = (From doc1 In Data.doc_cls _
                Where doc1.id_doc = nro_doc _
                 Select doc1.id_doc, doc1.doc_sdo_cli, doc1.dsi_cls.ope_cls.opn_cls.id_P_0012)

                For Each D In Temporal_doc

                    Dim Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = D.id_doc And _
                                                                               I.id_P_0053 = 2 And _
                                                                              (I.ing_vld_rcz = "S" Or _
                                                                                 I.ing_vld_rcz = "I" Or _
                                                                                 I.ing_vld_rcz = "V" Or _
                                                                                 I.ing_vld_rcz = "C" Or _
                                                                                 I.ing_vld_rcz = "L") And _
                                                                                 I.ing_pro = "N" _
                                                                                 Select (I.ing_mto_abo / I.ing_fac_cam)).Sum
                    If Ingresos Is Nothing Then Ingresos = 0

                    Dim Doctos = From doc1 In Data.doc_cls Where doc1.id_doc = D.id_doc And ((D.doc_sdo_cli - Ingresos) > 0 Or D.id_P_0012 = 3) _
                Select New With {doc1.id_doc, _
                                 doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                                 doc1.dsi_cls.deu_ide, _
                                .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                              doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn.Trim.ToUpper, _
                                                              doc1.dsi_cls.deu_cls.deu_rso.Trim.ToUpper), _
                                .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                              doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                                              doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                                 doc1.id_opo, _
                                 doc1.opo_cls.opo_otg, _
                                 doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
                                .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                                .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                                 doc1.dsi_cls.id_P_0011, _
                                .EstadoDocto = doc1.dsi_cls.P_0011_cls.pnu_des, _
                                 doc1.dsi_cls.dsi_num, _
                                 doc1.dsi_cls.dsi_mto, _
                                 doc1.dsi_cls.dsi_flj, _
                                 doc1.dsi_cls.dsi_flj_num, _
                                 doc1.doc_num_ren, _
                                 doc1.dsi_cls.dsi_cei, _
                                 doc1.opo_cls.ope_cls.opn_cls.id_P_0012, _
                                 doc1.opo_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                                 doc1.dsi_cls.dsi_ntf, _
                                 doc1.dsi_cls.id_P_0040, _
                                .EstadoVerifica = doc1.dsi_cls.P_0040_cls.pnu_des, _
                                 doc1.dsi_cls.dsi_fec_emi, _
                                .doc_fev_ori = doc1.dsi_cls.dsi_fev_ori, _
                                .doc_fev = doc1.dsi_cls.dsi_fev, _
                                .doc_fev_rea = doc1.dsi_cls.dsi_fev_cal, _
                                 doc1.dsi_cls.dsi_mto_ant, _
                                 doc1.dsi_cls.dsi_ctd_dia, _
                                 doc1.dsi_cls.dsi_pre_com, _
                                 doc1.dsi_cls.dsi_dif_pre, _
                                 doc1.dsi_cls.dsi_sal_pen, _
                                 doc1.dsi_cls.dsi_sal_pag, _
                                 doc1.dsi_cls.dsi_cms, _
                                 doc1.dsi_cls.dsi_iva_cms, _
                                 doc1.doc_gto, _
                                 doc1.doc_ful_pgo, _
                                 doc1.doc_int_dvg, _
                                 doc1.doc_ful_dvg, _
                                 doc1.doc_fct, _
                                 .fec_ven = doc1.dsi_cls.dsi_fev_rea, _
                                 doc1.id_suc_cbz, _
                                .SucCobranza = doc1.suc_cls.suc_des_cra, _
                                 doc1.id_suc_rcd, _
                                .SucRecauda = doc1.suc_cls.suc_des_cra, _
                                 doc1.id_cco, _
                                 doc1.cco_cls.cco_num, _
                                .EstadoCobranza = doc1.cco_cls.cco_des, _
                                 doc1.doc_fec_cco, _
                                 doc1.cco_cls.cco_des, _
                                 doc1.dsi_cls.dsi_cbz, _
                                 doc1.dsi_cls.dsi_cbz_son, _
                                 doc1.dsi_cls.id_PL_000047, _
                                 .doc_sdo_cli = (doc1.doc_sdo_cli - Ingresos), _
                                 .doc_sdo_ddr = (doc1.doc_sdo_ddr - Ingresos), _
                                 doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                                 doc1.doc_not_cre, _
                                .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                                .tasa = 0.0, _
                                .nota_cred = 0.0, _
                                .interes = 0.0, _
                                .MontoPagar = 0.0, _
                                .PagaDeudor = "S", _
                                doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa, _
                                 doc1.opo_cls.ope_cls.ope_fac_cam, _
                                 doc1.opo_cls.ope_cls.ope_fec_sim, _
                                 doc1.opo_cls.ope_cls.ope_dif_pre, _
                                 doc1.opo_cls.ope_cls.ope_lnl, _
                                 doc1.doc_tas_ren, _
                                 doc1.dsi_cls.dsi_fev_ori, _
                                .Cantidad_Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                               I.id_P_0053 = 2 And _
                                                                              (I.ing_vld_rcz = "I" Or _
                                                                               I.ing_vld_rcz = "V" Or _
                                                                               I.ing_vld_rcz = "C") And _
                                                                               I.ing_pro = "N" _
                                                       Select I.id_ing_sec).Count, _
                                .Aplicacion = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                                                I.id_P_0053 = 2 And _
                                                                                I.egr_sec_cls.egr_cls.id_apl = NroAplicacion _
                                                Select I.id_ing_sec).Count}

                    For Each P In Doctos
                        Coll.Add(P)
                    Next


                Next

            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Pagos_Devuelve(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                       ByVal Fecha_Desde As String, ByVal Fecha_Hasta As String, _
                                       ByVal NroDoctoPago_Desde As Integer, ByVal NroDoctoPago_Hasta As Integer, _
                                       ByVal MontoPago_Desde As Double, ByVal MontoPago_Hasta As Double, _
                                       ByVal Estado_Desde As Integer, ByVal Estado_Hasta As Integer, _
                                       ByVal Banco_Desde As Integer, ByVal Banco_Hasta As Integer, _
                                       ByVal Sucursal_Desde As Integer, ByVal Sucursal_Hasta As Integer, _
                                       ByVal Plaza_Desde As Integer, ByVal Plaza_Hasta As Integer, _
                                        Optional ByVal Pag As Integer = 9999) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos de pagos segun criterio de busqueda el Nro. Docto Pago, Monto, Estado, Banco y Plaza ( en forma de rango)
        'Creado por Jorge Lagos
        'Fecha Creacion: 17/02/2009
        'Quien Modifica     Fecha       Descripcion
        'A. Saldivar        10/02/2011  Se agrega paginacion
        'JLagos             09/08/2012  Se quita el cliente, ya que cuando paga deudor no aplica
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext
        Dim sesion As New ClsSession.ClsSession

        Try

            'And (I.ing_qpa = "C" Or I.ing_qpa = "D") _

            'Dim Pagos = (From I In data.ing_sec_cls _
            '                Where (CLng(I.cli_idc) >= RutCliente_Desde And CLng(I.cli_idc) <= RutCliente_Hasta) And _
            '                       (I.ing_cls.ing_fec >= Fecha_Desde And I.ing_cls.ing_fec <= Fecha_Hasta) And _
            '                       (I.id_ing >= NroDoctoPago_Desde And I.id_ing <= NroDoctoPago_Hasta) And _
            '                       (I.dpo_cls.dpo_mto >= MontoPago_Desde And I.dpo_cls.dpo_mto <= MontoPago_Hasta) And _
            '                       (I.dpo_cls.id_P_0052 >= Estado_Desde And I.dpo_cls.id_P_0052 <= Estado_Hasta) And _
            '                       (I.cli_cls.id_suc >= Sucursal_Desde And I.cli_cls.id_suc <= Sucursal_Hasta) _
            '                       And (I.ing_qpa = "C" Or I.ing_qpa = "D") _
            '                Order By I.id_ing Descending _
            '                Select Tipo = I.dpo_cls.p_0054_cls.pnu_des, _
            '                       Nro = I.id_dpo, _
            '                       Monto = I.dpo_cls.dpo_mto, _
            '                       id_bco = I.dpo_cls.id_bco, _
            '                       Banco = I.dpo_cls.bco_cls.bco_des, _
            '                       Fec_Emision = I.dpo_cls.dpo_fec_emi, _
            '                       Fec_Vcto = I.dpo_cls.dpo_fev, _
            '                       id_plaza = I.dpo_cls.id_PL_000047, _
            '                       Plaza = I.dpo_cls.PL_000047_cls.pal_des, _
            '                       id_mon = I.dpo_cls.id_P_0023, _
            '                       id_ing = I.id_ing, _
            '                       Moneda = I.dpo_cls.P_0023_cls.pnu_des, _
            '                       Cta = I.dpo_cls.dpo_cct, _
            '                       id_est = I.dpo_cls.id_P_0052, _
            '                       Estado = I.dpo_cls.P_0052_cls.pnu_des, _
            '                       I.dpo_cls.dpo_anl, _
            '                       Motivo = I.dpo_cls.dpo_mot_prt Distinct).Skip(sesion.NroPaginacion_Recaudacion).Take(13)

            Dim Pagos = (From I In data.ing_sec_cls _
                           Where (CLng(I.cli_idc) >= RutCliente_Desde And CLng(I.cli_idc) <= RutCliente_Hasta) And _
                                  (I.ing_cls.ing_fec >= Fecha_Desde And I.ing_cls.ing_fec <= Fecha_Hasta) And _
                                  (I.id_ing >= NroDoctoPago_Desde And I.id_ing <= NroDoctoPago_Hasta) And _
                                  (I.dpo_cls.dpo_mto >= MontoPago_Desde And I.dpo_cls.dpo_mto <= MontoPago_Hasta) And _
                                  (I.dpo_cls.id_P_0052 >= Estado_Desde And I.dpo_cls.id_P_0052 <= Estado_Hasta) And _
                                  (I.cli_cls.id_suc >= Sucursal_Desde And I.cli_cls.id_suc <= Sucursal_Hasta) _
                                  And (I.ing_qpa = "C" Or I.ing_qpa = "D") _
                           Order By I.id_ing Descending _
                           Select Tipo = Nothing, _
                                  Nro = I.id_dpo, _
                                  Monto = I.dpo_cls.dpo_mto, _
                                  id_bco = I.dpo_cls.id_bco, _
                                  Banco = I.dpo_cls.bco_cls.bco_des, _
                                  Fec_Emision = I.dpo_cls.dpo_fec_emi, _
                                  Fec_Vcto = I.dpo_cls.dpo_fev, _
                                  id_plaza = I.dpo_cls.id_PL_000047, _
                                  Plaza = I.dpo_cls.PL_000047_cls.pal_des, _
                                  id_mon = I.dpo_cls.id_P_0023, _
                                  id_ing = I.id_ing, _
                                  Moneda = I.dpo_cls.P_0023_cls.pnu_des, _
                                  Cta = I.dpo_cls.dpo_cct, _
                                  id_est = I.dpo_cls.id_P_0052, _
                                  Estado = I.dpo_cls.P_0052_cls.pnu_des, _
                                  I.dpo_cls.dpo_anl, _
                                  Motivo = I.dpo_cls.dpo_mot_prt Distinct).Skip(sesion.NroPaginacion_Recaudacion).Take(13)


            If (Banco_Desde > 0 And Banco_Hasta < 999) Or (Plaza_Desde > 0 And Plaza_Hasta < 999) Then

                For Each P In Pagos

                    If (P.id_bco >= Banco_Desde And P.id_bco <= Banco_Desde) Or _
                       (P.id_plaza >= Plaza_Hasta And P.id_plaza <= Plaza_Hasta) Then

                        Coll.Add(P)

                    End If

                Next

            Else

                For Each P In Pagos
                    Coll.Add(P)
                Next

            End If

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function PagosValidaEstados(ByVal Num_Doc As Long, ByVal ModuloQueValida As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Cobrar de un Cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 22/12/2008
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try


            '/* * * * *  Carpeta Comercial - Ingreso de Pagos  (Boton Aplicaciones) * * * * */
            '/* * * * *  Pago Directo      - Ingreso de Pagos  (Boton Pagos)        * * * * */

            If ModuloQueValida = "APLI-INGR" Or ModuloQueValida = "PDIRECTO-INGR" Then

                Dim Pagos = From I In data.ing_sec_cls Where (I.id_doc = Num_Doc Or I.id_cxc = Num_Doc) _
                                                          And I.ing_pro = "N" _
                                                         And (I.ing_vld_rcz = "I" Or _
                                                              I.ing_vld_rcz = "S" Or _
                                                              I.ing_vld_rcz = "V") _
                                      Select New With { _
                                                         I.id_doc, _
                                                         I.id_cxc, _
                                                         I.ing_cls.ing_fec, _
                                                         I.ing_vld_rcz, _
                                                         .Estado = "", _
                                                         .Modulo = "", _
                                                         I.ing_mto_tot, _
                                                         I.ing_qpa, _
                                                         I.ing_cls.id_eje, _
                                                         .Ejecutivo = I.ing_cls.eje_cls.eje_nom}


                For Each P In Pagos

                    Select Case P.ing_vld_rcz
                        Case "I" : P.Estado = "INGRESADO"
                        Case "S" : P.Estado = "SIMULADO"
                        Case "V" : P.Estado = "VALIDADO"
                        Case "C" : P.Estado = "CANJE"
                    End Select

                    If (P.ing_qpa = "C" Or P.ing_qpa = "D") And (P.id_eje > 0 Or Not IsNothing(P.id_eje)) Then
                        P.Modulo = "RECAUDACION"
                    Else
                        If P.ing_qpa = "E" Then
                            P.Modulo = "EXC.-CARP.COMERIAL"
                        Else
                            If P.ing_qpa = "O" Then
                                P.Modulo = "OPERACIONES"
                            Else
                                If (P.ing_qpa = "C" Or P.ing_qpa = "D") And (P.id_eje = 0 Or IsNothing(P.id_eje)) Then
                                    P.Modulo = "PAGO DIRECTO"
                                End If
                            End If
                        End If
                    End If

                    Coll.Add(P)

                Next

            End If

            Return Coll



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosDePagos_Devuelve(ByVal id As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las los ingresos de un cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 30/01/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            Dim DPO = From D In data.ing_sec_cls Where D.id_ing = id 'And D.ing_cls.ing_rec = "N"

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function PagosValidaParaEliminar(ByVal id_dpo As Integer, ByVal TipoConsulta As String) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Valida Pagos para ver si se pueden eliminar (sp_te_valida_pago_a_eliminar)
        'Creado por Jorge Lagos
        'Fecha Creacion: 12/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            Select Case TipoConsulta
                Case "EXCED"

                    Dim EXCED = From E In data.egr_sec_cls Where E.id_P_0055 = 3 And _
                                (E.egr_vld_rcz = "I" Or _
                                 E.egr_vld_rcz = "S" Or _
                                 E.egr_vld_rcz = "V" Or _
                                 E.egr_vld_rcz = "L" Or _
                                 E.egr_vld_rcz = "C") And _
                                 (From I In data.ing_sec_cls Where I.id_egr_sec = E.id_egr And _
                                                                   I.id_dpo = id_dpo And _
                                                                   I.cli_idc = E.egr_cls.cli_idc And _
                                                                   I.ing_vld_rcz = "L" And _
                                                                   I.dpo_cls.id_P_0054 = 2 And _
                                                                   I.id_nce = Nothing).Any And _
                                 (From D In data.doc_cls Where D.id_doc = E.id_doc And _
                                                               D.dsi_cls.id_P_0011 = 3 And _
                                                               D.dsi_cls.dsi_flj = "N").Any _
                                  Select Rutcli = E.egr_cls.cli_idc, _
                                         Nombre = E.egr_cls.cli_cls.cli_rso.Trim & " " & E.egr_cls.cli_cls.cli_ape_ptn & "" & E.egr_cls.cli_cls.cli_ape_mtn, _
                                         num_doc = E.id_doc, _
                                         flj_num = E.doc_cls.dsi_cls.dsi_flj_num



                    For Each E In EXCED
                        Coll.Add(E)
                    Next

                Case "NOCED"

                    Dim NOCED = From E In data.egr_sec_cls Where E.id_P_0055 = 2 And _
                              (E.egr_vld_rcz = "I" Or _
                               E.egr_vld_rcz = "S" Or _
                               E.egr_vld_rcz = "V") And _
                               (From I In data.ing_sec_cls Where I.id_egr_sec = E.id_egr And _
                                                                 I.id_dpo = id_dpo And _
                                                                 I.cli_idc = E.egr_cls.cli_idc And _
                                                                 I.ing_vld_rcz = "L" And _
                                                                 I.dpo_cls.id_P_0054 = 2 And _
                                                                 I.id_nce <> Nothing).Any _
                                          Select Rutcli = E.egr_cls.cli_idc, _
                                         Nombre = E.egr_cls.cli_cls.cli_rso.Trim & " " & E.egr_cls.cli_cls.cli_ape_ptn & "" & E.egr_cls.cli_cls.cli_ape_mtn, _
                                         num_doc = E.id_doc, _
                                         flj_num = E.doc_cls.dsi_cls.dsi_flj_num


                    For Each E In NOCED
                        Coll.Add(E)
                    Next


                Case "CXPPA"

                    Dim CXPPA = From E In data.egr_sec_cls Where E.id_P_0055 = 3 And _
                                (E.egr_vld_rcz = "I" Or _
                                 E.egr_vld_rcz = "S" Or _
                                 E.egr_vld_rcz = "V") And _
                                 (From I In data.ing_sec_cls Where I.id_egr_sec = E.id_egr And _
                                                                   I.id_dpo = id_dpo And _
                                                                   I.cli_idc = E.egr_cls.cli_idc And _
                                                                   I.ing_vld_rcz = "L" And _
                                                                   I.dpo_cls.id_P_0054 = 2 And _
                                                                   I.id_nce = Nothing).Any _
                                  Select Rutcli = E.egr_cls.cli_idc, _
                                         Nombre = E.egr_cls.cli_cls.cli_rso.Trim & " " & E.egr_cls.cli_cls.cli_ape_ptn & "" & E.egr_cls.cli_cls.cli_ape_mtn, _
                                         num_doc = E.id_doc, _
                                         flj_num = E.doc_cls.dsi_cls.dsi_flj_num

                    For Each E In CXPPA
                        Coll.Add(E)
                    Next

                Case "CXPMA"

                    Dim CXPMA = From E In data.egr_sec_cls Where E.id_P_0055 = 1 And _
                                (E.egr_vld_rcz = "I" Or _
                                 E.egr_vld_rcz = "S" Or _
                                 E.egr_vld_rcz = "V") And _
                                 (From I In data.ing_sec_cls Where I.id_egr_sec = E.id_egr And _
                                                                   I.id_dpo = id_dpo And _
                                                                   I.cli_idc = E.egr_cls.cli_idc And _
                                                                   I.dpo_cls.id_P_0054 = 2 And _
                                                                   I.id_nce = Nothing And _
                                                                   (From C In data.cxp_cls Where C.cli_idc = I.cli_idc And _
                                                                                                 C.id_P_0041 = 12).Any).Any _
                                  Select Rutcli = E.egr_cls.cli_idc, _
                                         Nombre = E.egr_cls.cli_cls.cli_rso.Trim & " " & E.egr_cls.cli_cls.cli_ape_ptn & "" & E.egr_cls.cli_cls.cli_ape_mtn, _
                                         cxp_num = E.id_cxp

                    For Each E In CXPMA
                        Coll.Add(E)
                    Next

            End Select

            Return Coll


        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function PagosPorId_Devuelve(ByVal ID As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las los documento de pagos (dpo) por su Numero unico
        'Creado por Jorge Lagos
        'Fecha Creacion: 03/02/2008
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            'Dim DoctoPago = From D In data.dpo_cls _
            '                Join I In data.ing_sec_cls On D.id_dpo Equals I.id_dpo Where I.id_dpo = ID _
            '                Select D.id_P_0054, _
            '                       I.id_ing, _
            '                       Tipo = D.p_0054_cls.pnu_des, _
            '                       D.id_dpo, _
            '                       D.dpo_mto, _
            '                       D.id_bco, _
            '                       Banco = D.bco_cls.bco_des, _
            '                       D.dpo_fec_emi, _
            '                       D.dpo_fev, _
            '                       D.id_PL_000047, _
            '                       Plaza = D.PL_000047_cls.pal_des, _
            '                       D.dpo_cct, _
            '                       D.dpo_aor, _
            '                       I.ing_vld_rcz, _
            '                       D.id_P_0052, _
            '                       Estado = D.P_0052_cls.pnu_des _
            '                       Distinct

            Dim DoctoPago = From D In data.dpo_cls _
                           Join I In data.ing_sec_cls On D.id_dpo Equals I.id_dpo Where I.id_dpo = ID _
                           Select D.id_P_0054, _
                                  I.id_ing, _
                                  Tipo = Nothing, _
                                  D.id_dpo, _
                                  D.dpo_mto, _
                                  D.id_bco, _
                                  Banco = D.bco_cls.bco_des, _
                                  D.dpo_fec_emi, _
                                  D.dpo_fev, _
                                  D.id_PL_000047, _
                                  Plaza = D.PL_000047_cls.pal_des, _
                                  D.dpo_cct, _
                                  D.dpo_aor, _
                                  I.ing_vld_rcz, _
                                  D.id_P_0052, _
                                  Estado = D.P_0052_cls.pnu_des _
                                  Distinct

            For Each x In DoctoPago
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function Totales_A_Pagar_Devuelve(ByVal RutCliente As Long, ByVal RutDeudor As Long, _
                                            ByVal Pagador As Char, ByVal FechaPago As DateTime) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los totales a pagar de un cliente o deudor
        'Creado por Jorge Lagos
        'Fecha Creacion: 04/03/2009
        'Quien Modifica Fecha Descripcion
        'jlagos 22-06-2012 -se agrega (D.dsi_cls.id_P_0011 = 3 And)
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            Dim Excedentes = From D In data.doc_cls Where D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                          D.opo_cls.ope_cls.id_P_0030 = 3 And _
                                                          D.dsi_cls.id_P_0011 = 3 And _
                                                          D.doc_sdo_exc > 0 And _
                                                          D.dsi_cls.dsi_flj = "N" _
                            Select Saldo = (D.doc_sdo_exc), _
                                   Moneda = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023, _
                                   Factor = If(D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023 = 1, _
                                               1, _
                                               (From P In data.par_cls Where P.id_P_0023 = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023 And _
                                                                             P.par_fec = FechaPago Select P.par_val).First)

            Dim SumatorioExcedentes As Double

            For Each E In Excedentes
                SumatorioExcedentes += (E.Saldo * Val(E.Factor))
            Next

            Coll.Add(SumatorioExcedentes)

            '**********************************************************************************************************************************************
            Dim Cuentas_X_Pagar = From C In data.cxp_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                          C.id_P_0057 = 1 Or C.id_P_0057 = 2 _
                            Select Saldo = C.cxp_mto, _
                                   Moneda = C.id_P_0023, _
                                   Factor = If(C.id_P_0023 = 1, _
                                               1, _
                                               (From P In data.par_cls Where P.id_P_0023 = C.id_P_0023 And _
                                                                             P.par_fec = FechaPago Select P.par_val).First)

            Dim SumatorioCuentasPorPagar As Double

            For Each E In Cuentas_X_Pagar
                SumatorioCuentasPorPagar += (E.Saldo * Val(E.Factor))
            Next

            Coll.Add(SumatorioCuentasPorPagar)

            '**********************************************************************************************************************************************
            Dim Doctos_No_Cedidos = From N In data.nce_cls _
                                    Join I In data.ing_sec_cls On N.id_nce Equals I.id_nce _
                                                            Where N.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                                      N.nce_pro = "N" And _
                                                                      I.id_P_0053 = 7 And _
                                                                      I.ing_vld_rcz = "L" And Not _
                                                                      I.dpo_cls.id_P_0052 = 4 _
                                   Select Saldo = N.nce_mto, _
                                   Moneda = N.id_p_0023, _
                                   Factor = If(N.id_p_0023 = 1, _
                                               1, _
                                               (From P In data.par_cls Where P.id_P_0023 = N.id_p_0023 And _
                                                                             P.par_fec = FechaPago Select P.par_val).First)


            Dim SumatorioDocumentosNoCedidos As Double

            For Each E In Doctos_No_Cedidos
                SumatorioDocumentosNoCedidos += (E.Saldo * E.Factor)
            Next

            Coll.Add(SumatorioDocumentosNoCedidos)

            '**********************************************************************************************************************************************
            Dim Cuentas_X_Cobrar = From C In data.cxc_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                       (C.id_P_0057 = 1 Or C.id_P_0057 = 2) And _
                                                        C.cxc_sal > 0 _
                         Select Saldo = C.cxc_sal, _
                                Moneda = C.id_P_0023, _
                                Factor = If(C.id_P_0023 = 1, _
                                            1, _
                                            (From P In data.par_cls Where P.id_P_0023 = C.id_P_0023 And _
                                                                          P.par_fec = FechaPago Select P.par_val).First)

            Dim SumatorioCuentasPorCobrar As Double

            For Each E In Cuentas_X_Cobrar
                Try

                    SumatorioCuentasPorCobrar += (E.Saldo * E.Factor)

                Catch ex As Exception

                End Try
            Next

            Coll.Add(SumatorioCuentasPorCobrar)

            '**********************************************************************************************************************************************

            Dim TodosLosDocumentos = From D In data.doc_cls Where D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                         D.dsi_cls.dsi_flj = "N" And _
                                                        (D.dsi_cls.id_P_0011 = 1 Or _
                                                         D.dsi_cls.id_P_0011 = 2 Or _
                                                         D.dsi_cls.id_P_0011 = 4 Or _
                                                         D.dsi_cls.id_P_0011 = 9 Or _
                                                         D.dsi_cls.id_P_0011 = 11 Or _
                                                         D.dsi_cls.id_P_0011 = 12) And _
                                                         If(Pagador = "C", D.doc_sdo_cli, D.doc_sdo_ddr) > 0 _
                           Select D.id_cco, D.cco_cls.cco_num, _
                                  Saldo = If(Pagador = "C", D.doc_sdo_cli, D.doc_sdo_ddr), _
                                  Moneda = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023, _
                                  Factor = If(D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023 = 1, _
                                              1, _
                                              (From P In data.par_cls Where P.id_P_0023 = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023 And _
                                                                            P.par_fec = FechaPago Select P.par_val).First)


            Dim SumatorioDocumentos As Double

            For Each E In TodosLosDocumentos
                SumatorioDocumentos += (E.Saldo * Val(E.Factor))

            Next

            Coll.Add(SumatorioDocumentos)

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#Region "Colilla Deposito"

    Public Function Cheque_Devuelve_Ultima_Fecha(ByVal Cust As Integer) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Pregunta por custodia y Devuelve la Ultima Fecha de un cheque 
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim ch = (From c In data.chr_cls Where c.id_P_0112 = Cust And c.id_P_0113 = 3 Order By c.chr_fev_rea Descending Select c.chr_fev_rea).First

            Return ch
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function BancoDevuelve(ByVal idCHR As Integer) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Pregunta Por cheque y rescata Id_Banco 
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim CHR = (From c In data.chr_cls Where c.id_chr = idCHR Select c.id_bco).First
            Return CHR
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Cheque_Devuelve(ByVal Custodia As Integer, _
                                           ByVal FDesde As DateTime, _
                                           ByVal FHasta As DateTime) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Cheque Por Custodia ,Fecha_Desde y Fecha_Hasta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'Antonio Saldivar            19/02/2009         no agrupa 
        '                                               no suma montos
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim chr = From c In data.chr_cls Where c.id_P_0112 = Custodia _
                      And c.chr_fev_rea >= FDesde _
                      And c.chr_fev_rea <= FHasta _
                      Select c.id_chr, _
                      c.chr_fev, _
                      c.chr_mto, _
                      c.chr_cli_deu, _
                      c.chr_fev_rea, _
                      c.chr_num, _
                      c.chr_tip_cli, _
                      c.cta_cte, _
                      c.id_bco, _
                      c.id_ope, _
                      c.id_P_0112, _
                      c.id_P_0113, _
                      c.id_PL_000047

            Return chr

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Documentos_Simulados_Devuelve_Por_Cheque(ByVal idCHR As Integer) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Documentos Simulados  Por Id_cheque
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'Antonio Saldivar            20/01/2009         Se Modifica la consulta
        '                                               
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim nrddevuelve = From n In data.nrd_cls Join D In data.doc_cls On n.id_dsi Equals D.id_dsi Where n.chr_cls.id_chr = idCHR _
                                Select n.id_dsi, _
                                        n.dsi_cls.deu_ide, _
                                        n.dsi_cls.dsi_fec_emi, _
                                        n.dsi_cls.dsi_cbz, _
                                        n.dsi_cls.dsi_fev, _
                                        n.dsi_cls.dsi_sal_pag, _
                                        n.dsi_cls.dsi_sal_pen, _
                                        n.chr_cls.id_ope, _
                                        n.dsi_cls.dsi_iva_cms, _
                                        n.dsi_cls.dsi_mto, _
                                        n.dsi_cls.dsi_dif_pre, _
                                        n.dsi_cls.dsi_env_bci, _
                                        n.dsi_cls.dsi_est_rsp, _
                                        n.dsi_cls.dsi_fev_rea, _
                                        n.dsi_cls.dsi_mto_ant, _
                                        n.dsi_cls.dsi_tot_gir, _
                                        n.dsi_cls.dsi_mto_fin, _
                                        n.dsi_cls.dsi_num, _
                                        n.dsi_cls.dsi_fev_ori, _
                                        n.dsi_cls.cta_cte, _
                                        n.dsi_cls.dsi_ctd_dia, _
                                        n.dsi_cls.dsi_flj, _
                                        n.dsi_cls.dsi_flj_num, _
                                        n.dsi_cls.dsi_gto, _
                                        n.dsi_cls.dsi_inc, _
                                        n.dsi_cls.dsi_ntf, _
                                        n.dsi_cls.dsi_num_ren, _
                                        n.dsi_cls.dsi_pre_com, _
                                        n.dsi_cls.dsi_rsp, _
                                        n.dsi_cls.id_bco, _
                                        Estado = n.dsi_cls.P_0011_cls.pnu_des, _
                                        n.dsi_cls.id_P_0040, _
                                        n.dsi_cls.id_P_0061, _
                                        n.dsi_cls.id_P_0065, _
                                        n.dsi_cls.id_P_0112, _
                                        n.dsi_cls.id_PL_000047, _
                                        Moneda = n.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                                        deudor = n.dsi_cls.deu_cls.deu_rso, _
                                        tipo_doc = n.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                                        saldo_cli = D.doc_sdo_cli, _
                                        saldo_deu = D.doc_sdo_ddr, _
            n.id_chr, _
            n.dsi_cls.ope_cls.ope_fac_cam, _
            D.id_doc, _
            D.doc_sdo_cli, _
            D.opo_cls.id_opo, _
            D.doc_dev_cli


            Return nrddevuelve
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Cheque_DevuelveObjeto(ByVal Custodia As Integer, ByVal Fecha As String, _
                                           ByVal Estado As Integer, Optional ByVal est2 As Integer = 0) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve cheque Por Fecha , Estado Asociado y Custodia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'Antonio Saldivar            20/01/2009         Se Modifica Los datos rescatados
        '                                               Se suma monto
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            Dim cust_desde As Integer
            Dim cust_hasta As Integer

            Dim FechaDesde As DateTime
            Dim FechaHasta As DateTime

            Dim est_desde As Integer
            Dim est_hasta As Integer

            Dim cliDesde As Integer
            Dim cliHasta As Integer

            If Custodia = 0 Then
                cust_desde = 0
                cust_hasta = 999
            Else
                cust_desde = Custodia
                cust_hasta = Custodia
            End If

            If Fecha = Nothing Then
                FechaDesde = "1900/01/01"
                FechaHasta = "2999/12/31"
            Else
                FechaDesde = CDate(Fecha).ToShortDateString
                FechaHasta = CDate(Fecha).ToShortDateString

            End If




            If Estado = 0 Then
                est_desde = 0
                est_hasta = 999
            Else
                If est2 <> 0 Then
                    est_desde = Estado
                    est_hasta = est2
                Else
                    est_desde = Estado
                    est_hasta = Estado
                End If
            End If




            Dim cheque = From c In data.nrd_cls Where (c.chr_cls.id_P_0112 >= cust_desde) _
                                              And (c.chr_cls.id_P_0112 <= cust_hasta) _
                                              And (c.chr_cls.chr_fev_rea >= FechaDesde And c.chr_cls.chr_fev_rea <= FechaHasta) _
                                              And (c.chr_cls.id_P_0113 >= est_desde) _
                                              And (c.chr_cls.id_P_0113 <= est_hasta) _
                                              Group By c.chr_cls.id_chr, _
                                                    c.chr_cls.bco_cls.bco_des, _
                                                    c.chr_cls.chr_cli_deu, _
                                                    c.chr_cls.chr_fev, _
                                                    c.chr_cls.chr_fev_rea, _
                                                    c.chr_cls.id_ope, _
                                                    c.chr_cls.chr_num, _
                                                    c.chr_cls.PL_000047_cls.pal_des, _
                                                    c.chr_cls.p_0113_cls.pnu_des, _
                                                    Moneda = c.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                                                    rut_cli = c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc, _
                                                    Cliente = If(c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                    c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                                    c.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                                                    c.chr_cls.id_bco, _
                                                    c.chr_cls.cta_cte, _
                                                    c.chr_cls.chr_tip_cli, _
                                                    c.chr_cls.id_cdp, _
                                                    c.chr_cls.id_P_0112, _
                                                    c.chr_cls.id_P_0113, _
                                                    c.chr_cls.id_PL_000047, _
                                                    c.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                                                    c.dsi_cls.ope_cls.ldc_cls.cli_idc, _
                                                    Monto_cheque = c.chr_cls.chr_mto _
                                                    Into Monto_Ocupado = Sum(c.mto_resp)


            Return cheque


            Return cheque

        Catch ex As Exception

        End Try
    End Function

#End Region

#End Region

#Region "Actualizaciones Pagos"

#Region "PAGOS"

    Public Function PagosInserta(ByVal Coll_dpo As Collection, _
                                     ByVal Ing As ing_cls, _
                                     ByVal Coll_Ing_Sec As Collection) As Integer

        '**************************************************************************************************************************************************
        'Descripcion: Inserta los documentos de pagos y los documentos que paga
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 26/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Using ts = New TransactionScope

                'Insertamos todos los documento de pagos para que nos genere su identificador (id) para asociarlos a su Ingreso (Ing_Sec).
                For I = 1 To Coll_dpo.Count
                    data.dpo_cls.InsertOnSubmit(Coll_dpo.Item(I))
                Next
                
                'Insertamos la cabecera de Ingresos (Ing)para que nos genere su identificador (id) para asociarlos a su Ingreso (Ing_Sec).
                data.ing_cls.InsertOnSubmit(Ing)

                'Insertamos los registros
                data.SubmitChanges()

                For X = 1 To Coll_Ing_Sec.Count

                    Dim Indice_dpo As Integer = Coll_Ing_Sec.Item(X).id_dpo

                    'si cumple la condicion ya que fueron engresados en forma secuencial le asigna sus id
                    Coll_Ing_Sec.Item(X).id_ing = Ing.id_ing
                    Coll_Ing_Sec.Item(X).id_dpo = Coll_dpo.Item(Indice_dpo).id_dpo
                    Coll_Ing_Sec.Item(X).id_egr_sec = Nothing

                    data.ing_sec_cls.InsertOnSubmit(Coll_Ing_Sec.Item(X))

                    'FALTA****************************************************************************
                    'Si que se Paga son doctos y nota de credito o pagado por el deudor es igual a SI
                    If Coll_Ing_Sec.Item(X).id_p_0053 = 2 And Coll_Ing_Sec.Item(X).ing_pag_deu = "S" Then


                        Dim id As Integer = Coll_Ing_Sec.Item(X).id_doc

                        Try

                            Dim Doctos As doc_cls = (From D In data.doc_cls Where D.id_doc = id).First
                            Doctos.doc_not_cre = 0
                            Doctos.doc_pag_ddr = "S"

                        Catch ex As Exception

                        End Try



                    End If

                Next


                data.SubmitChanges()

                ts.Complete()

                Return Ing.id_ing


            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Pagos_Operacion_Inserta(ByVal Ing As ing_cls, _
                                     ByVal Coll_Ing_Sec As Collection) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta los documentos de pagos y los documentos que paga
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 26/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Using ts = New TransactionScope

                '*************************************************************************************************
                'Insertamos todos los documento de pagos para que nos genere su identificador (id) para asociarlos a su Ingreso (Ing_Sec).
                'For I = 1 To Coll_dpo.Count
                '    data.dpo_cls.InsertOnSubmit(Coll_dpo.Item(I))
                'Next
                ''*************************************************************************************************

                'Insertamos la cabecera de Ingresos (Ing)para que nos genere su identificador (id) para asociarlos a su Ingreso (Ing_Sec).
                data.ing_cls.InsertOnSubmit(Ing)

                '*************************************************************************************************
                'Insertamos los registros
                data.SubmitChanges()

                '  For I = 1 To Coll_dpo.Count

                For X = 1 To Coll_Ing_Sec.Count

                    'If Coll_Ing_Sec.Item(X).id_dpo = I Then

                    'si cumple la condicion ya que fueron engresados en forma secuencial le asigna sus id
                    Coll_Ing_Sec.Item(X).id_ing = Ing.id_ing
                    '          Coll_Ing_Sec.Item(X).id_dpo = Coll_dpo.Item(I).id_dpo

                    data.ing_sec_cls.InsertOnSubmit(Coll_Ing_Sec.Item(X))


                    'FALTA****************************************************************************
                    'Si que se Paga son doctos y nota de credito o pagado por el deudor es igual a SI
                    If Coll_Ing_Sec.Item(X).id_p_0053 = 2 And Coll_Ing_Sec.Item(X).ing_pag_deu = "S" Then


                        'Dim id As Integer = Coll_Ing_Sec.Item(X).id_doc
                        'Dim Doctos As doc_cls = (From D In data.doc_cls Where D.id_doc = id).First

                        ' if @nota_credito <> 0 or @pagado_deudor = "S"
                        '           begin()
                        '           update doc set doc_not_cre = @nota_credito,
                        '                          doc_pag_ddr = @pagado_deudor
                        '               where cli_idc = @rut_cliente
                        '                 and ddr_ide = @rut_deudor
                        '                 and pnu_tip_doc = @tipo_docto
                        '                 and doc_num = @nro_docto
                        '                 and doc_cod_flj = @codigo_flujo
                        '                 and doc_flj_num = @nro_cuota
                        '                 and pnu_est <> 5
                        '                 and doc_flj = "N"
                        '           End

                    End If
                    'FALTA****************************************************************************

                    'Else
                    'Exit For
                    ' End If
                Next

                '   Next


                data.SubmitChanges()

                ts.Complete()

                Return True


            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Pagos_ApruebaRechaza(ByVal id_ing As Integer, ByVal id_dpo As Integer, ByVal ApruebaRechaza As Char) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Aprueba o Rechaza pagos por su id_dpo
        'Creado por Jorge Lagos
        'Fecha Creacion: 11/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Try



            Dim ING = From D In data.ing_sec_cls Where D.id_ing = id_ing And D.id_dpo = id_dpo

            For Each D In ING
                D.ing_vld_rcz = ApruebaRechaza
            Next

            data.SubmitChanges()

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Pagos_Actualiza(ByVal dpo As dpo_cls) As Integer

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza los documentos de pagos
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 27/02/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Using ts = New TransactionScope


                Dim DocPag = From D In data.dpo_cls Where D.id_dpo = dpo.id_dpo

                For Each D In DocPag

                    D.id_nma = dpo.id_nma
                    D.id_P_0052 = dpo.id_P_0052
                    'Por ahora solo numero de nomina y estado
                Next


                data.SubmitChanges()

                ts.Complete()

                Return 0


            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function AnulaAplicaciones(ByVal NroAplicacion As Integer) As Boolean


        Dim data As New DataClsFactoringDataContext

        Try

            Dim CrearCuentas As String
            Dim MontoTotalEgreso As Double
            Dim CXCReversaEgreso As String
            Dim RutCliente As String
            Dim CrearCXC As String
            Dim descrip As String
            Dim idCXP As Integer
            Dim idNCE As Integer
            Dim idDOC As Integer
            Dim idEGRSEC As Integer
            Dim EGRFEC As String

            'Traemos todos los documentos que han sido procesados por su numero de egreso
            Dim tempo_egr = From E In data.egr_sec_cls Where E.egr_cls.id_apl = NroAplicacion

            CrearCuentas = "NO"
            MontoTotalEgreso = 0

            For Each t In tempo_egr

                CXCReversaEgreso = "R"

                RutCliente = t.egr_cls.cli_idc

                Select Case t.id_P_0055
                    Case 1
                        '---------------------------------------------------------------------------------------
                        'Para Egresos de Ctas. X Pagar
                        '---------------------------------------------------------------------------------------
                        Try

                            'Elimina Cuentas X CXP ingresadas en Hoja de Recauda
                            Dim CXP As cxp_cls = (From C In data.cxp_cls Where C.id_cxp = t.id_cxp).First
                            idDOC = CXP.id_doc
                            CXP.id_P_0057 = 1

                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene CXP
                        End Try

                    Case 2
                        '---------------------------------------------------------------------------------------
                        'Para Egresos de Doctos No Cedidos  
                        '---------------------------------------------------------------------------------------
                        Try
                            Dim NCE As nce_cls = (From N In data.nce_cls Where N.id_nce = t.id_nce).First
                            idDOC = NCE.id_doc
                            NCE.nce_pro = "N"

                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene NCE
                        End Try
                    Case 3

                        '---------------------------------------------------------------------------------------
                        'Para Egresos de Excedentes
                        '---------------------------------------------------------------------------------------
                        Try

                            Dim sqlquery As New FuncionesGenerales.SqlQuery
                            Dim DOC = From D In data.doc_cls Where D.id_doc = t.id_doc Select D.id_dsi, D.opo_cls.id_ope

                            For Each d In DOC
                                sqlquery.ExecuteNonQuery("Update doc set doc_sdo_exc = doc_sdo_exc + " & CDbl(t.egr_mto) & " Where id_doc = " & CInt(t.id_doc) & " ")
                                sqlquery.ExecuteNonQuery("Update dsi set id_P_0011 = 3 Where id_dsi = " & CInt(d.id_dsi) & " ")
                                sqlquery.ExecuteNonQuery("Update ope set id_P_0030 = 3 Where id_ope = " & CInt(d.id_ope) & " ")

                            Next

                            idDOC = t.id_doc

                            't.doc_cls.dsi_cls.id_P_0011 = 3
                            't.doc_cls.doc_sdo_exc = t.doc_cls.doc_sdo_exc + t.egr_mto
                            't.doc_cls.opo_cls.ope_cls.id_P_0030 = 3 'la operacion la debe dejar otorgada.


                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene Excedentes
                        End Try

                End Select

                '---------------------------------------------------------------------------------------
                'Valida Tipo de Egreso (C/S Giro)
                '---------------------------------------------------------------------------------------
                CrearCXC = "NO"
                idEGRSEC = t.id_egr_sec
                EGRFEC = t.egr_cls.egr_fec

                Select Case t.id_P_0056
                    Case 5 'Sin Giro
                        Try
                            Dim ING As ing_sec_cls = (From I In data.ing_sec_cls Where I.id_egr_sec = idEGRSEC).First

                            If Not IsNothing(ING) Then
                                If AnulaPagoAplicacion(0, 0, "A", "A", idEGRSEC) = False Then
                                    Return False
                                End If
                            Else
                                Try
                                    'Validar Existencia de CXP
                                    Dim CXP_2 As cxp_cls = (From C In data.cxp_cls Where C.cxp_fec = EGRFEC _
                                                        And C.cxp_des.Contains("POR SDO. APLICACION S/DEVOLUCION NRO : " & NroAplicacion)).First

                                    'Si CXP esta Pagada Crar CXC
                                    If CXP_2.id_P_0057 = 3 Then
                                        CrearCXC = "SI"
                                    End If

                                    'Si CXP esta Vigente Eliminar
                                    If CXP_2.id_P_0057 = 1 Then
                                        CXP_2.id_P_0057 = 4
                                        CXCReversaEgreso = "E"
                                    End If

                                Catch ex As Exception
                                    'si ocurre una excepcion es que no tiene Cuenta por pagar asociada a 
                                End Try

                            End If
                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene Ingresos sin giro
                        End Try
                    Case Else 'Con Giro
                        CrearCXC = "SI"
                End Select

                'Crea CXC cuando Corresponda
                If CrearCXC = "SI" Then
                    CrearCuentas = "SI"
                    CXCReversaEgreso = "C"
                    MontoTotalEgreso = MontoTotalEgreso + t.egr_mto
                End If

                'Modifica Egreso a estado anulado
                Dim EGR_SEC As egr_sec_cls = (From E In data.egr_sec_cls Where E.id_egr_sec = idEGRSEC).First

                EGR_SEC.egr_vld_rcz = "A"
                EGR_SEC.egr_rev_cxc = CXCReversaEgreso

            Next

            data.SubmitChanges()

            'Crear Unica Cuenta por Todos los Egresos
            If CrearCuentas = "SI" Then

                Dim CXC_2 As New cxc_cls

                descrip = "ANULACION APLICACIÓN [" & NroAplicacion & "]"

                CXC_2.cli_idc = RutCliente
                CXC_2.id_P_0041 = 1
                CXC_2.id_doc = idDOC
                CXC_2.cxc_fec = Now.Date 'Format(Now, "dd/MM/yyyy")
                CXC_2.cxc_mto = MontoTotalEgreso
                CXC_2.cxc_des = descrip
                CXC_2.cxc_sal = MontoTotalEgreso
                CXC_2.cxc_ful_pgo = Now.Date 'Format(Now, "yyyyMMdd")
                CXC_2.id_P_0057 = 1
                CXC_2.id_fct = Nothing
                CXC_2.cxc_fct = "N"
                CXC_2.id_P_0023 = 1
                CXC_2.cxc_fac_cam = 1

                data.cxc_cls.InsertOnSubmit(CXC_2)
                data.SubmitChanges()

            End If

            'Actualiza agregando fecha de anulación a aplicación
            Dim APL As apl_cls = (From A In data.apl_cls Where A.id_apl = NroAplicacion).First

            APL.apl_fec_anl = Now.Date 'Format(Now, "dd/MM/yyyy")

            data.SubmitChanges()


            Try

                'jlagos 23-10-2012 -se agrega la rebaja automatica de saldos
                data.sp_op_cierre_cliente(RutCliente, _
                                          RutCliente, _
                                          DateTime.Now())

            Catch ex As Exception

            End Try


            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AnulaPagoAplicacion(ByVal NroDpo As Integer, _
                                        ByVal MotivoProtesto As Integer, _
                                        ByVal ProtestaAnula As Char, _
                                        ByVal RecaudacionesAplicaciones As Char, _
                                        ByVal NroEgreso As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Anula o Protesta un pagos por su id_dpo ([sp_te_protesta_anula_pago])
        'Creado por Jorge Lagos
        'Fecha Creacion: 11/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim RutCliente As String
        Dim idDOC As Long
        Dim coll_clientes As New Collection
        Dim FG As New FuncionesGenerales.FComunes

        Try

            Dim tempo_rec As Object

            Select Case RecaudacionesAplicaciones

                Case "R" 'Recaudacion
                    'Traemos todos los documentos que han sido procesados con el documento de pago (id_dpo)
                    tempo_rec = From i In data.ing_sec_cls Where i.id_dpo = NroDpo _
                                Order By i.id_ing, i.id_ing_sec 'And i.ing_pro = "S" _

                    '******************************************************************************************
                    'Buscamos el documento de pago por su Identificador unico
                    Dim Dpo = From D In data.dpo_cls Where D.id_dpo = NroDpo


                    Select Case ProtestaAnula
                        Case "P" 'Protesto
                            For Each d In Dpo
                                d.id_P_0052 = 6 'ENDOSO IRREGULAR
                                d.id_P_0061 = MotivoProtesto
                                d.dpo_anl = "P"
                            Next
                        Case "A" 'Anula
                            For Each d In Dpo
                                d.dpo_anl = "S"
                            Next
                    End Select
                    '******************************************************************************************

                    'Actualiza documentos de pago recaudaciones
                    Dim Ing = From I In data.ing_sec_cls Where I.id_dpo = NroDpo 'And I.ing_pro = "N" 'NO procesado

                    Dim BorraNCE As Boolean = False

                    For Each I In Ing
                        I.ing_vld_rcz = ProtestaAnula
                        I.ing_fec_prt_anl = Date.Now

                        If Not IsNothing(I.id_nce) Then
                            BorraNCE = True
                        End If
                    Next
                    '******************************************************************************************

                    If BorraNCE Then


                        'Borra documentos no cedidos de pago recaudaciones
                        Try

                            Dim NCE = From N In data.nce_cls Where (From I In data.ing_sec_cls Where I.id_dpo = NroDpo And _
                                                                                                     I.id_doc = N.nce_num_doc And _
                                                                                                     I.id_nce <> Nothing).Any

                            data.nce_cls.DeleteOnSubmit(NCE)

                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene documento cedido

                        End Try
                    End If
                    '******************************************************************************************
                    Try

                        'Elimina Cuentas X CXP ingresadas en Hoja de Recauda
                        Dim CXP = From C In data.cxp_cls Where C.id_P_0057 = 4 And _
                                (From I In data.ing_sec_cls Where I.id_dpo = NroDpo And _
                                                                  C.id_cxp = I.egr_sec_cls.id_cxp And _
                                                                  I.id_nce <> Nothing).Any


                        If CXP.Count <> 0 Then
                            data.cxp_cls.DeleteOnSubmit(CXP)
                        End If

                    Catch ex As Exception
                        'si ocurre una excepcion es que no tiene CXP
                    End Try

                    '******************************************************************************************


                Case Else
                    'Traemos todos los documentos que han sido procesados por su numero de egreso (se quita que esten procesados)
                    tempo_rec = From i In data.ing_sec_cls Where i.id_egr_sec = NroEgreso _
                                Order By i.id_ing, i.id_ing_sec


                    'Cambia estado de pagos a anulados
                    Dim Ing = From I In data.ing_sec_cls Where I.id_egr_sec = NroEgreso And I.ing_pro = "N" 'NO procesado
                    For Each I In Ing
                        I.ing_vld_rcz = ProtestaAnula
                        I.ing_fec_prt_anl = Date.Now
                    Next

            End Select

            data.SubmitChanges()

            For Each t In tempo_rec

                Dim DocNoCed As Char

                If IsNothing(t.id_nce) Then
                    DocNoCed = "N"
                Else
                    DocNoCed = "S"
                End If

                Select Case t.id_p_0053
                    Case 1 'Cuentas por Cobrar
                        If Not Pagos_Anula_Conceptos(t.id_ing, _
                                                     t.id_ing_sec, _
                                                     0, _
                                                     t.id_cxc, _
                                                     0, _
                                                     "CXC", _
                                                     DocNoCed, _
                                                     ProtestaAnula) Then
                            Return False
                        End If
                    Case 2 'Documentos
                        If Not Pagos_Anula_Conceptos(t.id_ing, _
                                                     t.id_ing_sec, _
                                                     t.doc_cls.id_opo, _
                                                     t.id_doc, _
                                                     t.doc_cls.dsi_cls.dsi_flj_num, _
                                                     "DOC", _
                                                     DocNoCed, _
                                                     ProtestaAnula) Then
                            Return False
                        End If
                End Select

            Next

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AnulaPagos(ByVal NroDpo As Integer, _
                                        ByVal MotivoProtesto As Integer, _
                                        ByVal ProtestaAnula As Char, _
                                        ByVal RecaudacionesAplicaciones As Char, _
                                        ByVal NroEgreso As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Anula o Protesta un pagos por su id_dpo ([sp_te_protesta_anula_pago])
        'Creado por Jorge Lagos
        'Fecha Creacion: 11/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim RutCliente As String
        Dim coll_clientes As New Collection
        Dim FG As New FuncionesGenerales.FComunes

        Try

            Dim tempo_rec As Object

            Select Case RecaudacionesAplicaciones

                Case "R" 'Recaudacion
                    'Traemos todos los documentos que han sido procesados con el documento de pago (id_dpo)
                    'tempo_rec = From i In data.ing_sec_cls Where i.id_dpo = NroDpo _
                    '            Order By i.id_ing, i.id_ing_sec _
                    '            Select i.id_ing, i.id_ing_sec, i.id_cxc, i.id_doc, i.id_dpo, i.doc_cls.id_opo, i.doc_cls.id_dsi, i.doc_cls.dsi_cls.dsi_flj_num, i.id_nce, i.id_P_0053, i.cli_idc

                    tempo_rec = From i In data.ing_sec_cls Where i.id_dpo = NroDpo _
                                Order By i.id_ing, i.id_ing_sec _
                                Select i.id_ing, i.id_ing_sec, i.id_cxc, i.id_doc, i.id_dpo, i.id_nce, i.id_P_0053, i.cli_idc

                    '******************************************************************************************
                    'Buscamos el documento de pago por su Identificador unico
                    Dim Dpo = From D In data.dpo_cls Where D.id_dpo = NroDpo


                    Select Case ProtestaAnula
                        Case "P" 'Protesto
                            For Each d In Dpo
                                d.id_P_0052 = 6 'ENDOSO IRREGULAR
                                d.id_P_0061 = MotivoProtesto
                                d.dpo_anl = "P"
                            Next
                        Case "A" 'Anula
                            For Each d In Dpo
                                d.dpo_anl = "S"
                            Next
                    End Select
                    '******************************************************************************************

                    'Actualiza documentos de pago recaudaciones
                    Dim Ing = From I In data.ing_sec_cls Where I.id_dpo = NroDpo 'And I.ing_pro = "N" 'NO procesado

                    Dim BorraNCE As Boolean = False

                    For Each I In Ing
                        I.ing_vld_rcz = ProtestaAnula
                        I.ing_fec_prt_anl = Date.Now

                        If Not IsNothing(I.id_nce) Then
                            BorraNCE = True
                        End If
                    Next
                    '******************************************************************************************

                    If BorraNCE Then


                        'Borra documentos no cedidos de pago recaudaciones
                        Try

                            Dim NCE = From N In data.nce_cls Where (From I In data.ing_sec_cls Where I.id_dpo = NroDpo And _
                                                                                                     I.id_doc = N.nce_num_doc And _
                                                                                                     I.id_nce <> Nothing).Any

                            data.nce_cls.DeleteOnSubmit(NCE)

                        Catch ex As Exception
                            'si ocurre una excepcion es que no tiene documento cedido

                        End Try
                    End If
                    '******************************************************************************************
                    Try

                        'Elimina Cuentas X CXP ingresadas en Hoja de Recauda
                        Dim CXP = From C In data.cxp_cls Where C.id_P_0057 = 4 And _
                                (From I In data.ing_sec_cls Where I.id_dpo = NroDpo And _
                                                                  C.id_cxp = I.egr_sec_cls.id_cxp And _
                                                                  I.id_nce <> Nothing).Any


                        If CXP.Count <> 0 Then
                            data.cxp_cls.DeleteOnSubmit(CXP)
                        End If

                    Catch ex As Exception
                        'si ocurre una excepcion es que no tiene CXP
                    End Try

                    '******************************************************************************************


                Case Else
                    'Traemos todos los documentos que han sido procesados por su numero de egreso (se quita que esten procesados)
                    tempo_rec = From i In data.ing_sec_cls Where i.id_egr_sec = NroEgreso _
                                Order By i.id_ing, i.id_ing_sec _
                                Select i.id_ing, i.id_ing_sec, i.id_cxc, i.id_doc, i.id_dpo, i.id_nce, i.id_P_0053, i.cli_idc
                    'Select i.id_ing, i.id_ing_sec, i.id_cxc, i.id_doc, i.id_dpo, i.doc_cls.id_opo, i.doc_cls.id_dsi, i.doc_cls.dsi_cls.dsi_flj_num, i.id_nce, i.id_P_0053, i.cli_idc


                    'Cambia estado de pagos a anulados
                    Dim Ing = From I In data.ing_sec_cls Where I.id_egr_sec = NroEgreso 'And  I.ing_pro = "N" 'NO procesado
                    For Each I In Ing
                        I.ing_vld_rcz = ProtestaAnula
                        I.ing_fec_prt_anl = Date.Now
                    Next

            End Select

            data.SubmitChanges()

            For Each t In tempo_rec

                Dim doc As Decimal = t.id_doc
                Dim DocNoCed As Char

                If IsNothing(t.id_nce) Then
                    DocNoCed = "N"
                Else
                    DocNoCed = "S"
                End If

                Select Case t.id_p_0053
                    Case 1 'Cuentas por Cobrar
                        If Not Pagos_Anula_Conceptos(t.id_ing, _
                                                     t.id_ing_sec, _
                                                     0, _
                                                     t.id_cxc, _
                                                     0, _
                                                     "CXC", _
                                                     DocNoCed, _
                                                     ProtestaAnula) Then
                            Return False
                        End If
                    Case 2 'Documentos
                        Dim doctos As Object = (From d In data.doc_cls Where d.id_doc = doc Select d.id_opo, d.id_dsi, d.dsi_cls.dsi_flj_num, d.id_doc).First
                        If Not Pagos_Anula_Conceptos(t.id_ing, _
                                                     t.id_ing_sec, _
                                                     doctos.id_opo, _
                                                     doctos.id_doc, _
                                                     doctos.dsi_flj_num, _
                                                     "DOC", _
                                                     DocNoCed, _
                                                     ProtestaAnula) Then
                            Return False
                        End If
                End Select

                RutCliente = t.cli_idc

                If Not FG.BuscaCollection(coll_clientes, RutCliente) Then
                    coll_clientes.Add(RutCliente)
                End If

            Next

            Try

                'jlagos 06-05-2012 -se agrega la rebaja automatica de saldos
                For I = 1 To coll_clientes.Count
                    data.sp_op_cierre_cliente(coll_clientes(I).ToString(), _
                                              coll_clientes(I).ToString(), _
                                              DateTime.Now())
                Next

            Catch ex As Exception

            End Try

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Pagos_Anula_Conceptos(ByVal NroPago As Integer, _
                                          ByVal SecPago As Integer, _
                                          ByVal NroOperacion As Integer, _
                                          ByVal NroConcepto As Integer, _
                                          ByVal NroCuota_doc As Integer, _
                                          ByVal Concepto As String, _
                                          ByVal DoctoNoCedido As Char, _
                                          ByVal ProtestoAnulacion As Char) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Proceso donde anula Pagos por concepto de cuentas [sp_te_anula_pagos_conceptos]
        'Creado por Jorge Lagos
        'Fecha Creacion: 13/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Try

            Dim MontoIngresoCli As Double
            Dim MontoIngresoDdr As Double
            Dim MontoAbonado As Double
            Dim MontoInteres As Double
            Dim Pagador As Char
            Dim NroDSI As Integer
            Dim pago_procesado As Char
            Dim MontoIngresoCliPesos As Double
            Dim fecha_ingreso As DateTime
            '----------------------------------
            Dim EstadoDocto As String
            Dim FechaVcto As DateTime
            Dim SaldoClienteDoc As Double
            Dim SaldoClienteIng As Double
            Dim SaldoDeudorIng As Double
            Dim SaldoReservaIng As Double
            Dim InteresDev As Double
            Dim ConCobranza As Char
            Dim RutCliente As String
            '----------------------------------
            Dim NroMaxPago As Integer
            Dim SecMaxPago As Integer
            Dim FecMaxUltPago As DateTime
            '----------------------------------
            Dim Monto_Egreso As Double
            Dim factor_cambio_cuenta As Double
            Dim Fecha_Egreso As DateTime
            '----------------------------------
            Dim cantidad_de_dias_ven As Integer
            Dim CXCReversa As Char
            Dim CreaCuenta As String
            Dim descrip As String
            '----------------------------------
            Dim EstadoCXPMPAGO As Integer
            Dim NroCXPMPAGO As Integer
            Dim ValorCXPMPAGO As Double
            Dim moneda_docto_cuenta As Integer
            Dim ValidaCXPMPAGO As Char
            Dim variable_de_pregunta As Int16 = 0
            Dim dato_final_accion As String = ""
            '----------------------------------
            Dim CodigoCobranza As String
            Dim FechaCodCobranza As String

            '------------------------------------------------------------------------------------------------
            'IMPORTANTE: Los montos de las tablas ING_SEC, DPO, EGR se guardar en la moneda de origen
            '------------------------------------------------------------------------------------------------
            
            'Buscamos el documento pagado por su identificador unico
            Dim ING = From I In data.ing_sec_cls Where I.id_ing = NroPago And I.id_ing_sec = SecPago And I.ing_vld_rcz = "A" _
                      Select MtoIngresoCli = I.ing_mto_tot, _
                             MtoIngresoDdr = I.ing_mto_tot, _
                             MtoAbonado = I.ing_mto_abo, _
                             MtoInteres = I.ing_mto_int, _
                             Pag = I.ing_qpa, _
                             NroDocPgo = I.id_dpo, _
                             pgo_procesado = I.ing_pro, _
                             MtoIngresoCliPesos = I.ing_mto_tot * I.ing_fac_cam, _
                             fecha_ing = I.ing_cls.ing_fec, _
                             I.doc_sdo_cli, I.doc_sdo_ddr, I.doc_sdo_exc

            For Each I In ING
                MontoIngresoCli = I.MtoIngresoCli
                MontoIngresoDdr = I.MtoIngresoDdr
                MontoAbonado = I.MtoAbonado
                MontoInteres = I.MtoInteres
                Pagador = I.Pag
                pago_procesado = I.pgo_procesado
                MontoIngresoCliPesos = I.MtoIngresoCliPesos
                fecha_ingreso = I.fecha_ing

                SaldoClienteIng = I.doc_sdo_cli
                SaldoDeudorIng = I.doc_sdo_ddr

                If IsNothing(I.doc_sdo_exc) Then
                    SaldoReservaIng = 0
                Else
                    SaldoReservaIng = I.doc_sdo_exc
                End If

            Next



            '------------------------------------------------------------------------------------------------
            'Si lo que se anula es un documento y no es esta cedido
            If Concepto = "DOC" And DoctoNoCedido = "N" Then

                'Buscamos el documento por su identificador unico, numero de cuota y estado del documento sea distinto a renovado
                Dim Documento = From D In data.doc_cls Where D.id_doc = NroConcepto And _
                                                       D.opo_cls.id_opo = NroOperacion And _
                                                       D.dsi_cls.id_P_0011 <> 5 And _
                                                       D.dsi_cls.dsi_flj = "N" And _
                                                       D.dsi_cls.dsi_flj_num = NroCuota_doc _
                                Select D.dsi_cls.id_P_0011, D.dsi_cls.dsi_fev_rea, D.doc_sdo_cli, D.doc_sdo_ddr, D.doc_int_dev, D.dsi_cls.dsi_cbz_son, D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, D.id_dsi

                For Each D In Documento
                    EstadoDocto = D.id_P_0011
                    FechaVcto = D.dsi_fev_rea
                    InteresDev = D.doc_int_dev
                    ConCobranza = D.dsi_cbz_son
                    RutCliente = D.cli_idc
                    NroDSI = D.id_dsi
                    SaldoClienteDoc = D.doc_sdo_cli
                Next
                
                NroMaxPago = 0
                SecMaxPago = 0
                FecMaxUltPago = "01-01-1900"

                'Buscamos los max del documento de pago (ultimo pago)
                Dim MaxIngSec = From I In data.ing_sec_cls Where I.id_ing < NroPago And _
                                                                 I.doc_cls.id_opo = NroOperacion And _
                                                                 I.id_doc = NroConcepto And _
                                                                (I.ing_vld_rcz = "L" Or _
                                                                 I.ing_vld_rcz = "V" Or _
                                                                 I.ing_vld_rcz = "C") And _
                                                                 I.id_P_0053 = 2 _
                                                                 Order By I.id_ing_sec Descending _
                              Select New With {.NroMax = I.id_ing, _
                                               .SecMax = I.id_ing_sec, _
                                               .FecMaxUlt = I.ing_cls.ing_fec, _
                                               .SaldoCli = I.doc_sdo_cli, _
                                               .SaldoDeu = I.doc_sdo_ddr, _
                                               .SaldoExc = I.doc_sdo_exc}


                For Each M In MaxIngSec
                    NroMaxPago = M.NroMax
                    SecMaxPago = M.SecMax
                    FecMaxUltPago = M.FecMaxUlt

                    'SaldoClienteIng = M.SaldoCli
                    'SaldoDeudorIng = M.SaldoDeu
                    'SaldoReservaIng = M.SaldoExc
                    Exit For
                Next

                'si el estado es excedente liberado 
                If EstadoDocto = 8 And (NroPago + SecPago) > (NroMaxPago + SecMaxPago) Then

                    'Buscamos todos los egreso 
                    Dim Egreso = From E In data.egr_sec_cls Where E.egr_cls.id_opo = NroOperacion And _
                                                              E.id_doc = NroConcepto And _
                                                              E.doc_cls.dsi_cls.dsi_flj_num = NroCuota_doc And _
                                                              E.id_P_0053 = 3 And _
                                                              E.egr_vld_rcz = "L" Order By E.egr_cls.egr_fec Descending _
                                                              Select E

                    'Actualizamos todos los egreso 
                    For Each E In Egreso
                        Monto_Egreso = Monto_Egreso + E.egr_mto
                        Fecha_Egreso = E.egr_cls.egr_fec
                        E.egr_vld_rcz = "E"
                    Next

                    data.SubmitChanges()

                    If Egreso.Count > 0 Then

                        'Buscamos la moneda de la operacion
                        Dim Operacion As opo_cls = (From O In data.opo_cls Where O.id_opo = NroOperacion).First

                        moneda_docto_cuenta = Operacion.ope_cls.opn_cls.id_P_0023

                        If moneda_docto_cuenta = 1 Then
                            Monto_Egreso = Monto_Egreso
                            factor_cambio_cuenta = 1
                        Else

                            factor_cambio_cuenta = (From P In data.par_cls Where P.par_fec = Fecha_Egreso And _
                                                                                 P.id_P_0023 = moneda_docto_cuenta _
                                                                                 Select P.par_val).First
                            Monto_Egreso = Monto_Egreso / factor_cambio_cuenta

                        End If

                        descrip = "ANUL. PAGO EXC. ENTR. DE DOCTO.*" & CStr(NroConcepto) & "-" & CStr(NroCuota_doc) & "*"

                        Dim cxc As New cxc_cls

                        With cxc
                            .id_cxc = Nothing
                            .cli_idc = RutCliente
                            .id_opo = NroOperacion
                            .id_doc = NroConcepto
                            .cxc_fec = Date.Now
                            .cxc_mto = Monto_Egreso
                            .cxc_des = descrip
                            .cxc_sal = Monto_Egreso
                            .cxc_ful_pgo = Date.Now
                            .id_P_0057 = 1
                            .id_P_0041 = 1
                            .id_fct = Nothing
                            .cxc_fct = "N"
                            .id_P_0023 = moneda_docto_cuenta
                            .cxc_fac_cam = factor_cambio_cuenta
                        End With

                        data.cxc_cls.InsertOnSubmit(cxc)
                        variable_de_pregunta = 1

                    End If

                End If

                data.SubmitChanges()
                CreaCuenta = "NO"

                'Si no es el ultimo pago genero CXC
                If (NroPago + SecPago) < (NroMaxPago + SecMaxPago) Then

                    'Buscamos la moneda de la operacion
                    Dim Operacion As opo_cls = (From O In data.opo_cls Where O.id_ope = NroOperacion).First

                    moneda_docto_cuenta = Operacion.ope_cls.opn_cls.id_P_0023

                    If moneda_docto_cuenta = 1 Then
                        'Monto_Egreso = Monto_Egreso
                        factor_cambio_cuenta = 1
                    Else
                        factor_cambio_cuenta = (From P In data.par_cls Where P.par_fec = fecha_ingreso And _
                                                                             P.id_P_0023 = moneda_docto_cuenta _
                                                Select P.par_val).First
                    End If

                    CreaCuenta = "SI"

                Else

                    Try

                        Dim GSN As gsn_cls = (From G In data.gsn_cls Where G.doc_cls.id_opo = NroOperacion And _
                                                                    G.id_doc = NroConcepto And _
                                                                    G.doc_cls.dsi_cls.dsi_flj_num = NroCuota_doc _
                                              Select G Order By G.id_gsn).First


                        CodigoCobranza = GSN.id_gsn
                        FechaCodCobranza = GSN.gsn_fec

                    Catch ex As Exception

                        If ConCobranza = "N" Then
                            CodigoCobranza = 14
                        Else
                            CodigoCobranza = 1
                        End If

                        FechaCodCobranza = "01-01-1900"

                    End Try


                    '------------------------------------------------------------------------------------------------
                    ' Reversa Pago
                    If (Pagador = "C" Or Pagador = "E" Or Pagador = "O") Then MontoIngresoDdr = 0

                    If (SaldoClienteDoc + MontoAbonado) > 0 Then

                        If FechaVcto < Date.Now.ToShortDateString Then
                            cantidad_de_dias_ven = DateDiff(DateInterval.Day, FechaVcto, Date.Now)
                            If cantidad_de_dias_ven < 30 Then
                                EstadoDocto = 2 'Moroso
                            Else
                                EstadoDocto = 4 'Vencido
                            End If
                        Else
                            EstadoDocto = 1
                        End If


                        If MontoInteres > 0 Then MontoInteres = 0

                        If pago_procesado = "N" Then
                            MontoAbonado = 0
                            MontoIngresoDdr = 0
                        End If

                        'Actualizamos los campos del documento
                        Dim SqlQuery As New FuncionesGenerales.SqlQuery
                        Dim query As String = ""

                        'query = "Update DOC set doc_sdo_cli = doc_sdo_cli + " & MontoAbonado & ", " & _
                        'query = "Update DOC set doc_sdo_cli = doc_sdo_cli + " & MontoAbonado & " - (doc_int_dev + " & (MontoInteres * -1) & ") - doc_sdo_exc , " & _

                        query = "Update DOC set doc_sdo_cli =  " & SaldoClienteIng & ", " & _
                                               "doc_sdo_ddr =  " & SaldoDeudorIng & ", " & _
                                               "doc_sdo_exc =  " & SaldoReservaIng & ", " & _
                                               "doc_int_dev = doc_int_dev + " & MontoInteres & ", " & _
                                               "doc_ful_pgo = '" & RG.FUNFechaJul(FecMaxUltPago) & "', " & _
                                               "doc_fec_cco = '" & RG.FUNFechaJul(FechaCodCobranza) & "', " & _
                                               "id_cco = " & CodigoCobranza & " " & _
                                               "Where id_doc = " & NroConcepto

                        SqlQuery.ExecuteNonQuery(query)

                        SqlQuery.ExecuteNonQuery("UPDATE DSI SET ID_P_0011 = " & EstadoDocto & " WHERE ID_DSI = " & NroDSI)

                        'Documento.dsi_cls.id_P_0011 = EstadoDocto
                        'Documento.doc_sdo_cli = Documento.doc_sdo_cli + MontoAbonado

                        'Documento.doc_sdo_ddr = Documento.doc_sdo_ddr + MontoIngresoDdr
                        'Documento.doc_int_dev = Documento.doc_int_dev + (MontoInteres * -1)
                        'Documento.doc_ful_pgo = FecMaxUltPago
                        'Documento.doc_fec_cco = FechaCodCobranza
                        'Documento.id_cco = CodigoCobranza

                        Try

                            'Cambia estado a Opera
                            Dim Operacion As opo_cls = (From O In data.opo_cls Where O.id_opo = NroOperacion).First

                            Operacion.ope_cls.id_P_0030 = 3

                            CXCReversa = "R"
                            CreaCuenta = "NO"

                        Catch ex As Exception

                        End Try

                    End If

                    If CreaCuenta = "NO" Then

                        'Valida Existencia de CXP Mayor Pago
                        Dim CXP = From C In data.cxp_cls Where C.cli_idc = RutCliente And C.id_P_0041 = 12 And C.id_doc = NroConcepto And C.id_P_0057 = 1

                        For Each C In CXP
                            EstadoCXPMPAGO = C.id_P_0057
                            NroCXPMPAGO = C.id_cxp
                            ValorCXPMPAGO = C.cxp_mto
                            moneda_docto_cuenta = C.id_P_0023
                        Next

                        If CXP.Count <> 0 Then

                            If EstadoCXPMPAGO = 1 Then
                                'Borra CXP Mayor Pago
                                Dim CuentaXpagar = From C In data.cxp_cls Where C.id_P_0041 = 12 And C.id_cxp = NroCXPMPAGO

                                For Each c In CuentaXpagar
                                    c.id_P_0057 = 4 'Anulada
                                Next
                                data.SubmitChanges()
                                ValidaCXPMPAGO = "B"

                            Else
                                'Crea CXC
                                If moneda_docto_cuenta = 1 Then
                                    factor_cambio_cuenta = 1
                                Else
                                    factor_cambio_cuenta = (From p In data.par_cls Where p.par_fec = fecha_ingreso And _
                                                                                         p.id_P_0023 = moneda_docto_cuenta _
                                                            Select p.par_val).First
                                End If

                                descrip = "ANULACION CXP MAYOR PAGO [" & CStr(NroPago) & CStr(SecPago) & "]"

                                Dim CxC As New cxc_cls
                                With CxC
                                    .cli_idc = RutCliente
                                    .id_opo = NroOperacion
                                    .id_doc = NroConcepto
                                    .id_cxc = Nothing
                                    .cxc_fec = Date.Now
                                    .cxc_mto = ValorCXPMPAGO
                                    .cxc_des = descrip
                                    .cxc_sal = ValorCXPMPAGO
                                    .cxc_ful_pgo = Date.Now
                                    .id_P_0057 = 1
                                    .id_P_0041 = 1
                                    .id_fct = Nothing
                                    .cxc_fct = "N"
                                    .id_P_0023 = moneda_docto_cuenta
                                    .cxc_fac_cam = factor_cambio_cuenta
                                End With

                                data.cxc_cls.InsertOnSubmit(CxC)
                                ValidaCXPMPAGO = "C"

                            End If
                        Else
                            ValidaCXPMPAGO = ""
                        End If

                    End If

                End If

            End If

            data.SubmitChanges()

            Dim NroDocto As String

            If Concepto = "DOC" And DoctoNoCedido = "S" Then

                Dim Egr = From e In data.egr_sec_cls Where e.egr_cls.cli_idc = RutCliente And _
                                                           e.id_doc = NroConcepto And _
                                                           e.egr_vld_rcz = "L" And _
                                                           e.id_P_0055 = 2


                If Egr.Count <> 0 Then

                    Dim NCE = From N In data.nce_cls Where N.nce_num_doc = NroConcepto

                    For Each n In NCE
                        moneda_docto_cuenta = n.id_p_0023
                        nrodocto = n.id_doc
                    Next

                    If moneda_docto_cuenta = 1 Then
                        factor_cambio_cuenta = 1
                    Else
                        factor_cambio_cuenta = (From p In data.par_cls Where p.par_fec = fecha_ingreso And _
                                                                             p.id_P_0023 = moneda_docto_cuenta _
                                                         Select p.par_val).First
                    End If

                    CreaCuenta = "SI"
                    ValidaCXPMPAGO = "F"

                Else
                    Dim NCE As nce_cls = (From N In data.nce_cls Where N.nce_num_doc = NroConcepto).First

                    data.nce_cls.DeleteOnSubmit(NCE)
                    ValidaCXPMPAGO = "F"

                End If

            End If

            If Concepto = "DOC" And DoctoNoCedido = "C" Then
                Dim Egr = From e In data.egr_sec_cls Where e.egr_cls.cli_idc = RutCliente And _
                                                           e.id_doc = NroConcepto And _
                                                           e.egr_vld_rcz = "L" And _
                                                           e.id_P_0055 = 1 'Entrega de CXP

                If Egr.Count <> 0 Then

                    Dim CuentaXpagar = From C In data.cxp_cls Where C.id_cxp = NroCXPMPAGO And _
                                                                    C.cli_idc = RutCliente

                    For Each c In CuentaXpagar
                        moneda_docto_cuenta = c.id_P_0023
                    Next

                    If moneda_docto_cuenta = 1 Then
                        factor_cambio_cuenta = 1
                    Else
                        factor_cambio_cuenta = (From p In data.par_cls Where p.par_fec = fecha_ingreso And _
                                                                        p.id_P_0023 = moneda_docto_cuenta _
                                                   Select p.par_val).First
                    End If

                    CreaCuenta = "SI"
                    ValidaCXPMPAGO = "E"
                Else

                    'Borra CXP 
                    Dim CuentaXpagar = From C In data.cxp_cls Where C.id_P_0041 = 12 And C.id_cxp = NroCXPMPAGO

                    For Each c In CuentaXpagar
                        c.id_P_0057 = 4 'Anulada
                    Next
                    ValidaCXPMPAGO = "E"

                End If

            End If

            If Concepto = "DOC" Then nrodocto = NroConcepto

            If Concepto = "CXC" Then

                ValidaCXPMPAGO = ""

                'Verifica si es Ultimo Pago
                Dim MaxIngSec = From I In data.ing_sec_cls Where I.id_ing <> NroPago And _
                                                                 I.id_cxc = NroConcepto And _
                                                                (I.ing_vld_rcz = "L" Or _
                                                                 I.ing_vld_rcz = "V" Or _
                                                                 I.ing_vld_rcz = "C") And _
                                                                 I.id_P_0053 = 1 _
                              Group By id = I.id_ing Into NroMax = Max(I.id_ing), _
                                                          SecMax = Max(I.id_ing_sec), _
                                                          FecMaxUlt = Max(I.ing_cls.ing_fec) _
                              Select NroMax, SecMax, FecMaxUlt

                If MaxIngSec.Count > 0 Then
                    For Each M In MaxIngSec
                        NroMaxPago = M.NroMax
                        SecMaxPago = M.SecMax
                        FecMaxUltPago = M.FecMaxUlt
                    Next
                Else
                    NroMaxPago = 0
                    SecMaxPago = 0
                    FecMaxUltPago = "01/01/1900"
                End If

                'Si Pago no es el ultimo
                If (NroPago + SecPago) < (NroMaxPago + SecMaxPago) Then
                    CreaCuenta = "SI"

                    Dim CuentaXcobrar = From C In data.cxc_cls Where C.id_cxc = NroConcepto

                    For Each c In CuentaXcobrar
                        moneda_docto_cuenta = c.id_P_0023
                        nrodocto = c.id_doc
                    Next

                    If moneda_docto_cuenta = 1 Then
                        factor_cambio_cuenta = 1
                    Else
                        factor_cambio_cuenta = (From p In data.par_cls Where p.par_fec = fecha_ingreso And _
                                                                             p.id_P_0023 = moneda_docto_cuenta _
                                                   Select p.par_val).First
                    End If


                Else

                    'Reversa Pago
                    If pago_procesado = "N" Then MontoAbonado = 0

                    Dim CuentaXcobrar = From C In data.cxc_cls Where C.id_cxc = NroConcepto And _
                                                                     C.id_P_0057 <> 4
                    For Each C In CuentaXcobrar
                        C.id_P_0057 = 1
                        C.cxc_sal = C.cxc_sal + MontoAbonado
                        C.cxc_ful_pgo = FecMaxUltPago
                        nrodocto = C.id_doc
                    Next

                    CXCReversa = "R"
                    CreaCuenta = "NO"

                End If

                If CreaCuenta = "NO" Then

                    'Valida Existencia de CXP Mayor Pago
                    Dim CXP = From C In data.cxp_cls Where C.id_P_0041 = 12 _
                                                     And C.cxp_des.Contains("[" & NroPago & SecPago & "]")

                    For Each C In CXP
                        EstadoCXPMPAGO = C.id_P_0057
                        NroCXPMPAGO = C.id_cxp
                        ValorCXPMPAGO = C.cxp_mto
                        moneda_docto_cuenta = C.id_P_0023
                        nrodocto = C.id_doc
                    Next

                    If CXP.Count <> 0 Then

                        If EstadoCXPMPAGO = 1 Then
                            'Borra CXP Mayor Pago
                            Dim CuentaXpagar = From C In data.cxp_cls Where C.id_P_0041 = 12 And C.id_cxp = NroCXPMPAGO

                            For Each c In CuentaXpagar
                                c.id_P_0057 = 4 'Anulada
                            Next

                            ValidaCXPMPAGO = "B"

                        Else

                            'Crea CXC
                            If moneda_docto_cuenta = 1 Then
                                factor_cambio_cuenta = 1
                            Else
                                factor_cambio_cuenta = (From p In data.par_cls Where p.par_fec = fecha_ingreso And _
                                                                                     p.id_P_0023 = moneda_docto_cuenta _
                                                        Select p.par_val).First
                            End If

                            descrip = "ANULACION CXP MAYOR PAGO [" & CStr(NroPago) & CStr(SecPago) & "]"

                            Dim CxC As New cxc_cls
                            With CxC
                                .cli_idc = RutCliente
                                .id_opo = NroOperacion
                                .id_doc = nrodocto
                                .id_cxc = Nothing
                                .cxc_fec = Date.Now
                                .cxc_mto = ValorCXPMPAGO
                                .cxc_des = descrip
                                .cxc_sal = ValorCXPMPAGO
                                .cxc_ful_pgo = Date.Now
                                .id_P_0057 = 1
                                .id_P_0041 = 1
                                .id_fct = Nothing
                                .cxc_fct = "N"
                                .id_P_0023 = moneda_docto_cuenta
                                .cxc_fac_cam = factor_cambio_cuenta
                            End With

                            data.cxc_cls.InsertOnSubmit(CxC)
                            ValidaCXPMPAGO = "C"

                        End If

                    Else
                        ValidaCXPMPAGO = ""
                    End If

                End If

            End If

            'Creacion Cuenta X Cobrar
            If CreaCuenta = "SI" Then

                'Lo traspasamos a la moneda de la cuenta
                MontoIngresoCliPesos = MontoIngresoCliPesos / factor_cambio_cuenta

                If ProtestoAnulacion = "A" Then
                    descrip = "ANULACION PAGO [" & CStr(NroPago) & CStr(SecPago) & "]"
                Else
                    descrip = "PROTESTO DOC.PAGO [" & CStr(NroPago) & CStr(SecPago) & "]"
                End If

                Dim CxC As New cxc_cls

                With CxC
                    .cli_idc = RutCliente
                    .id_opo = NroOperacion
                    .id_doc = nrodocto
                    .id_cxc = Nothing
                    .cxc_fec = Date.Now
                    .cxc_mto = MontoIngresoCliPesos
                    .cxc_des = descrip
                    .cxc_sal = MontoIngresoCliPesos
                    .cxc_ful_pgo = Date.Now
                    .id_P_0057 = 1
                    .id_P_0041 = 1
                    .id_fct = Nothing
                    .cxc_fct = "N"
                    .id_P_0023 = moneda_docto_cuenta
                    .cxc_fac_cam = factor_cambio_cuenta
                End With

                data.cxc_cls.InsertOnSubmit(CxC)

                CXCReversa = "C"

            End If

            '---------------------------------------------------------------------------------------------
            ' La accion al realizar la Anulacion(ing_rev_cxc) tiene los siguiente posibles valores :  --

            '        R  => Reversa Saldos                                                             --
            '        RB => Reversa Saldos y Elimina CXP M.Pago                                        --
            '        RC => Reversa Saldos y crea CXC X valor CXP M.Pago                               --
            '        C  => Crea CXC por Monto Pagado                                                  --
            '        CB => Crea CXC por Monto Pagado y Elimina CXP M.Pago                             --
            '        CC => Crea CXC por Monto Pagado y crea CXC X valor CXP M.Pago                    --
            '        F  => Elimina Docto. No Cecido                                                   --
            '        CF => Crea CXC por Monto Pagado y crea CXC X valor FNC                           --
            '        E  => ANULA CXP                                                                  --
            '        RE => Reversa Saldos y Crea CXC por EXC Ent                                      --
            '        RP => Reversa Saldos y Elimina CXP M.Pago y Crea CXC por EXC Ent                 --
            '        RT => Reversa Saldos y crea CXC X valor CXP M.Pago y Crea CXC por EXC Ent        --
            '---------------------------------------------------------------------------------------------

            If variable_de_pregunta = 1 Then

                If CXCReversa & ValidaCXPMPAGO = "R" Then
                    dato_final_accion = "RE"
                Else
                    If CXCReversa & ValidaCXPMPAGO = "RB" Then
                        dato_final_accion = "RP"
                    Else
                        If CXCReversa & ValidaCXPMPAGO = "RC" Then
                            dato_final_accion = "RT"
                        Else
                            dato_final_accion = CXCReversa & ValidaCXPMPAGO
                        End If
                    End If
                End If

            Else
                dato_final_accion = (CXCReversa + ValidaCXPMPAGO)
            End If

            Dim Ingresos = From I In data.ing_sec_cls Where I.id_ing_sec = SecPago And I.id_ing = NroPago

            For Each I In Ingresos
                I.ing_vld_rcz = ProtestoAnulacion
                I.ing_fec_prt_anl = Date.Now
                I.ing_rev_cxc = dato_final_accion
            Next

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Colilla Deposito"



    Public Function Colilla_ingresa(ByVal cdp As cdp_cls) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Cambia estado al  cheque al generar colilla , para no volverlo a utilizar
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 21/09/2010

        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            data.cdp_cls.InsertOnSubmit(cdp)
            data.SubmitChanges()

            Return cdp.id_cdp



        Catch ex As Exception

        End Try
    End Function
    Public Function Cheque_Actualiza_pago(ByVal id As ArrayList, ByVal id_colilla As Int64) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Cambia estado al  cheque al generar colilla , para no volverlo a utilizar
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 21/09/2010

        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            Dim n_cheque As Int32
            For i = 0 To id.Count - 1
                n_cheque = id.Item(i)

                Dim cheque As chr_cls = (From c In data.chr_cls Where c.id_chr = n_cheque).First

                cheque.id_P_0113 = 4
                cheque.id_cdp = id_colilla
                data.SubmitChanges()
            Next






        Catch ex As Exception

        End Try
    End Function

    Public Function ingreso_Actualiza_pago(ByVal id_ing As Integer, ByVal id_colilla As Int64) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Cambia estado al  cheque al generar colilla , para no volverlo a utilizar
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 21/09/2010

        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            Dim n_cheque As Int32

            Dim ingreso As ing_cls = (From i In data.ing_cls Where i.id_ing = id_ing).First

            ingreso.id_cdp = id_colilla

            data.SubmitChanges()







        Catch ex As Exception

        End Try
    End Function

#End Region

#End Region


End Class
