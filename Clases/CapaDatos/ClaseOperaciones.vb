Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports System.Transactions
Imports CapaDatos
Imports System
Imports System.Web.UI
Imports FuncionesGenerales.FComunes
Imports System.Data.SqlClient


Public Class ClaseOperaciones

#Region "Variables y Propiedades"

    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim cg As New ConsultasGenerales
    Dim cmc As New ClaseComercial
    Dim ag As New ActualizacionesGenerales

    Private _EstadoOperacion As String
    Public Property EstadoOperacion() As String
        Get
            Return _EstadoOperacion
        End Get
        Set(ByVal value As String)
            _EstadoOperacion = value
        End Set
    End Property

    Private _GastosAfecto As Double
    Public Property GastoAfecto() As Double
        Get
            Return _GastosAfecto
        End Get
        Set(ByVal value As Double)
            _GastosAfecto = value
        End Set
    End Property

    Private _GastosExento As Double
    Public Property GastosExento() As Double
        Get
            Return _GastosExento
        End Get
        Set(ByVal value As Double)
            _GastosExento = value
        End Set
    End Property

    Private _MensajeOperacion As String
    Public Property MensajeOperacion() As String
        Get
            Return _MensajeOperacion
        End Get
        Set(ByVal value As String)
            _MensajeOperacion = value
        End Set
    End Property

#End Region

#Region "Consultas Operaciones"

#Region "Cartola de Documentos"

    Public Function OperacionCartolaDocumentos_Retorna(ByVal TipoConsulta As Int16, ByVal rut_cliente As String, _
                                                          ByVal rut_cliente2 As String, ByVal rut_deudor As String, _
                                                          ByVal codigo_sucursal1 As String, ByVal codigo_sucursal2 As String, _
                                                          ByVal eje_cod1 As Integer, ByVal eje_cod2 As Integer, ByVal mon1 As Int16, _
                                                          ByVal mon2 As Int16, ByVal fec_des As String, ByVal fec_has As String, _
                                                          ByVal est1 As Integer, ByVal est2 As Integer, ByVal nro_ope1 As String, _
                                                          ByVal nro_ope2 As String, ByVal nro_doc1 As String, ByVal nro_doc2 As String, _
                                                          ByVal codigo_cob As Integer, ByVal con_cob1 As String, ByVal con_cob2 As String, _
                                                          ByVal con_res1 As Int16, ByVal con_res2 As Int16, ByVal fec_oto1 As String, _
                                                          ByVal fec_oto2 As String, ByVal LlenaGrilla As Boolean, _
                                                          Optional ByVal GridView As GridView = Nothing) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve informe Cartola Documentos
        'Creado por= Yonathan Cabezas V.
        'Fecha Creacion: 12/03/2009
        'Quien Modifica              Fecha              Descripcion
        ' P.GATICA                   20/06/2010         Se agrega Cliente hasta
        ' jlagos                     26-08-2010         Se agrega estado 13 (anulado)
        '*********************************************************************************************************************************

        Try
            Dim var As New FuncionesGenerales.Variables
            Dim CollDoctos As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim ContIng As Integer = 0
            Dim Sesion As New ClsSession.ClsSession

            Dim rut_deudor_desde As String
            Dim rut_deudor_hasta As String

            If (rut_deudor > 0) Then
                rut_deudor_desde = rut_deudor
                rut_deudor_hasta = rut_deudor
            Else
                rut_deudor_desde = "000000000000"
                rut_deudor_hasta = "999999999999"
            End If

            Dim Temp_Doctos = (From Doc In Data.doc_cls Where (Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje_cod1 And Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje_cod2) And _
                                                              (Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente And Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                                                              (Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_suc >= codigo_sucursal1 And Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_suc <= codigo_sucursal2) And _
                                                              (Doc.dsi_cls.id_P_0011 >= est1 And Doc.dsi_cls.id_P_0011 <= est2) And _
                                                              (Doc.opo_cls.opo_otg >= nro_ope1 And Doc.opo_cls.opo_otg <= nro_ope2) And _
                                                              (Doc.dsi_cls.ope_cls.opn_cls.id_P_0023 >= mon1 And Doc.dsi_cls.ope_cls.opn_cls.id_P_0023 <= mon2) And _
                                                              (Doc.dsi_cls.dsi_num >= nro_doc1 And Doc.dsi_cls.dsi_num <= nro_doc2) And _
                                                              (Doc.dsi_cls.dsi_fev_rea >= CDate(fec_des) And Doc.dsi_cls.dsi_fev_rea <= CDate(fec_has)) And _
                                                              (Doc.dsi_cls.dsi_cbz_son = con_cob1 Or Doc.dsi_cls.dsi_cbz_son = con_cob2) And _
                                                              (Doc.dsi_cls.ope_cls.ope_res_son >= con_res1 And Doc.dsi_cls.ope_cls.ope_res_son <= con_res2) And _
                                                              (Doc.opo_cls.opo_fec_oto >= CDate(fec_oto1) And Doc.opo_cls.opo_fec_oto <= CDate(fec_oto2)) And _
                                                              (Doc.dsi_cls.deu_ide >= rut_deudor_desde And Doc.dsi_cls.deu_ide <= rut_deudor_hasta) And _
                                                               Doc.dsi_cls.id_P_0011 <> 5 And Doc.dsi_cls.id_P_0011 <> 13 _
                                                             Select New With {Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc, _
                                                                              .cli_rso = If(Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                                                            Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                                                                                            Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
                                                                              Doc.dsi_cls.deu_cls.deu_ide, _
                                                                              .deu_rso = If(Doc.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                                                            Doc.dsi_cls.deu_cls.deu_rso & " " & Doc.dsi_cls.deu_cls.deu_ape_ptn & " " & Doc.dsi_cls.deu_cls.deu_ape_mtn, _
                                                                                            Doc.dsi_cls.deu_cls.deu_rso), _
                                                                              Doc.dsi_cls.ope_cls.id_ope, _
                                                                              Doc.opo_cls.id_opo, _
                                                                              Doc.id_cco, _
                                                                              Doc.dsi_cls.id_dsi, _
                                                                              Doc.dsi_cls.dsi_num, _
                                                                              Doc.dsi_cls.dsi_flj_num, _
                                                                              Doc.dsi_cls.dsi_mto, _
                                                                              Doc.dsi_cls.dsi_mto_fin, _
                                                                              .dsi_fev_ori = Doc.dsi_cls.dsi_fev_ori, _
                                                                              .dsi_fev_rea = Doc.dsi_cls.dsi_fev_rea, _
                                                                              Doc.dsi_cls.dsi_flj, _
                                                                              Doc.dsi_cls.id_P_0011, _
                                                                              .Est_Docto = Doc.dsi_cls.P_0011_cls.pnu_des, _
                                                                              Doc.dsi_cls.id_P_0040, _
                                                                              .est_veri = Doc.dsi_cls.P_0040_cls.pnu_des, _
                                                                              Doc.dsi_cls.ope_cls.opn_cls.id_P_0031, _
                                                                              .tipo_docto = Doc.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                                                                              Doc.dsi_cls.id_PL_000047, _
                                                                              Doc.dsi_cls.PL_000047_cls.pal_des, _
                                                                              Doc.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                                                                              .moneda = Doc.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                                                                              Doc.doc_sdo_cli, _
                                                                              Doc.doc_sdo_ddr, _
                                                                              Doc.doc_ful_pgo, _
                                                                              Doc.opo_cls.opo_otg, _
                                                                              .dsi_cbz_son = IIf(Doc.dsi_cls.dsi_cbz_son = "S", "SI", "NO"), _
                                                                              .ope_res_son = IIf(Doc.dsi_cls.ope_cls.ope_res_son = 1, "SI", "NO"), _
                                                                              .CountIng = (From I In Data.ing_sec_cls Where I.cli_idc = Doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc Select I.id_ing Distinct).Count, _
                                                                              Doc.opo_cls.ope_cls.id_opn} _
                                                                              ).Skip(Sesion.NroPaginacion).Take(10)
            ' (Doc.dsi_cls.dsi_cbz_son = con_cob1 Or Doc.dsi_cls.dsi_cbz_son <= con_cob2) And _
            Select Case TipoConsulta
                Case 1
                    For Each d In Temp_Doctos
                        If d.deu_ide = rut_deudor And d.id_cco = codigo_cob Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 2
                    For Each d In Temp_Doctos
                        If d.deu_ide = rut_deudor Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 3
                    For Each d In Temp_Doctos
                        If d.id_cco = codigo_cob Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 4
                    For Each d In Temp_Doctos
                        If d.cli_idc = rut_cliente Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 5
                    For Each d In Temp_Doctos
                        If d.deu_ide = rut_deudor And d.id_cco = codigo_cob Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 6
                    For Each d In Temp_Doctos
                        If d.deu_ide = rut_deudor Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 7
                    For Each d In Temp_Doctos
                        If d.id_cco = codigo_cob Then
                            CollDoctos.Add(d)
                        End If
                    Next
                Case 8
                    For Each d In Temp_Doctos
                        CollDoctos.Add(d)
                    Next
            End Select


            If LlenaGrilla Then
                GridView.DataSource = CollDoctos
                GridView.DataBind()
            End If

            Return CollDoctos


        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function OperacionDetalleDoctos(ByVal TipoConsulta As Int16, ByVal DocNum As Integer) As Collection
        'SE MODIFICO EL TIPO DE CONSULTA 4 (OTRAS CUENTAS)
        Try

            '*********************************************************************************************************************************
            'Descripcion: Devuelve detalle de documentos
            'Creado por= Yonathan Cabezas V.
            'Fecha Creacion: 16/03/2009
            'Quien Modifica              Fecha              Descripcion
            ' P.Gatica                    18/06/2010        Se cambian los parametros , por solo el id , pues la consulta era muy lenta
            'A Saldivar                   03/09/2010        Se agrega dsi_num a excedentes
            'A Saldivar                   12/01/2011        Se agrega paginacion
            'S Henriquez 		  24/09/2012	    Se agrega id_ope
            '*********************************************************************************************************************************

            Dim Data As New DataClsFactoringDataContext
            Dim CollDetalleDoctos As New Collection
            'Dim TempDoctos 
            Dim sesion As New ClsSession.SesionOperaciones

            Select Case TipoConsulta
                Case 1

                    'Otorgamiento
                    Dim TempDoctos = (From D In Data.doc_cls Where _
                                     D.dsi_cls.id_dsi = DocNum _
                    Select New With {.id_opo = If(D.opo_cls.id_opo = Nothing, 0, D.opo_cls.id_opo), _
                                     D.opo_cls.opo_otg, _
                                     D.opo_cls.id_ope, _
                                     D.dsi_cls.ope_cls.ope_fec_sim, _
                                     D.opo_cls.opo_fec_oto, _
                                     D.dsi_cls.ope_cls.opn_cls.opn_por_ant, _
                                     D.dsi_cls.ope_cls.opn_cls.id_P_0012, _
                                     D.dsi_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                                     D.dsi_cls.ope_cls.opn_cls.opn_can_doc, _
                                     D.dsi_cls.dsi_mto, _
                                     D.dsi_cls.dsi_mto_ant, _
                                     D.dsi_cls.ope_cls.ope_tot_gir, _
                                     D.dsi_cls.ope_cls.ope_pre_com, _
                                     D.dsi_cls.ope_cls.ope_dif_pre, _
                                     D.dsi_cls.ope_cls.ope_sal_pen, _
                                     D.dsi_cls.ope_cls.ope_sal_pag, _
                                     D.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                                     .Moneda = D.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des}).Skip(sesion.page_otg)



                    For Each T In TempDoctos.Take(6)
                        CollDetalleDoctos.Add(T)
                    Next


                Case 2 'Recaudación
                    'Dim TempDoctos = (From I In Data.ing_sec_cls Where I.doc_cls.dsi_cls.id_dsi = DocNum _
                    '             And I.id_P_0053 = 2 _
                    '             Select New With {I.ing_cls.ing_fec, _
                    '                             .id_p_0054 = IIf(IsNothing(I.dpo_cls.id_P_0054), I.egr_sec_cls.id_P_0056, I.dpo_cls.id_P_0054), _
                    '                              .pnu_des = IIf(IsNothing(I.dpo_cls.p_0054_cls.pnu_des), I.egr_sec_cls.P_0056_cls.pnu_des, I.dpo_cls.p_0054_cls.pnu_des), _
                    '                              I.doc_cls.dsi_cls.dsi_num, _
                    '                              I.doc_cls.dsi_cls.dsi_flj_num, _
                    '                              .ing_qpa = I.ing_qpa, _
                    '                              I.ing_tot_par, _
                    '                              I.ing_mto_int, _
                    '                              I.ing_rea_mon, _
                    '                              I.ing_mto_tot, _
                    '                              I.ing_mto_abo, _
                    '                              I.ing_tas_apl, _
                    '                              I.ing_vld_rcz, _
                    '                              I.doc_sdo_cli, _
                    '                              I.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0023}).Skip(sesion.page_recau)

                    Dim TempDoctos = (From I In Data.ing_sec_cls Where I.doc_cls.dsi_cls.id_dsi = DocNum _
                                And I.id_P_0053 = 2 _
                                Select New With {I.ing_cls.ing_fec, _
                                                .id_p_0054 = IIf(IsNothing(I.dpo_cls.id_P_0054), I.egr_sec_cls.id_P_0056, I.dpo_cls.id_P_0054), _
                                                 .pnu_des = Nothing, _
                                                 I.doc_cls.dsi_cls.dsi_num, _
                                                 I.doc_cls.dsi_cls.dsi_flj_num, _
                                                 .ing_qpa = I.ing_qpa, _
                                                 I.ing_tot_par, _
                                                 I.ing_mto_int, _
                                                 I.ing_rea_mon, _
                                                 I.ing_mto_tot, _
                                                 I.ing_mto_abo, _
                                                 I.ing_tas_apl, _
                                                 I.ing_vld_rcz, _
                                                 I.doc_sdo_cli, _
                                                 I.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0023}).Skip(sesion.page_recau)

                    '(select pnu_des from pnu where pnu_cod_tbl = 54 and pnu_cod = I.pnu_ing_tip) as "TIPO",

                    'If Not IsNothing(TempDoctos) Then

                    For Each T In TempDoctos.Take(6)
                        CollDetalleDoctos.Add(T)
                    Next

                    Return CollDetalleDoctos

                    ' End If
                Case 3

                    'Excedentes
                    Dim TempDoctos = (From E In Data.egr_sec_cls Where E.doc_cls.dsi_cls.id_dsi = DocNum And _
                                E.id_P_0055 = 3 _
                                Select New With {E.egr_cls.egr_fec, _
                                                 E.id_P_0056, _
                                                 E.P_0056_cls.pnu_des, _
                                                 E.doc_cls.dsi_cls.dsi_flj_num, _
                                                 E.egr_mto, _
                                                 E.egr_vld_rcz, _
                                                 E.doc_cls.dsi_cls.dsi_mto, _
                                                 E.doc_cls.opo_cls.ope_cls.opn_cls.id_P_0023, _
                                                 .moneda = E.doc_cls.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                                                 E.doc_cls.dsi_cls.dsi_num, _
                                                 E.doc_cls.opo_cls.ope_cls.opn_cls.opn_por_ant}).Skip(sesion.page_exc)

                    'E.egr_cls.opo_cls.ope_cls.opn_cls.id_P_0023, 

                    For Each T In TempDoctos.Take(6)
                        CollDetalleDoctos.Add(T)
                    Next

                Case 4

                    'Otras Cuentas
                    'Dim TempDoctos = (From C In Data.cxc_cls Join doc In Data.doc_cls On C.id_doc Equals doc.id_doc _
                    '             Where C.doc_cls.dsi_cls.id_dsi = DocNum And _
                    '             C.id_P_0041 = 3 _
                    '             Select New With {C.cxc_fec, _
                    '                              C.cxc_des, _
                    '                              C.id_cxc, _
                    '                              C.P_0023_cls.pnu_des, _
                    '                              C.cxc_mto, _
                    '                              C.cxc_sal, _
                    '                              C.cxc_ful_pgo, _
                    '                              C.id_P_0023}).Skip(sesion.page_cuen)

                    Dim TempDoctos = (From C In Data.cxc_cls _
                                Where C.doc_cls.dsi_cls.id_dsi = DocNum And _
                                C.id_P_0041 = 3 _
                                Select New With {C.cxc_fec, _
                                                 C.cxc_des, _
                                                 C.id_cxc, _
                                                 C.P_0023_cls.pnu_des, _
                                                 C.cxc_mto, _
                                                 C.cxc_sal, _
                                                 C.cxc_ful_pgo, _
                                                 C.id_P_0023}).Skip(sesion.page_cuen)


                    For Each T In TempDoctos.Take(6)
                        CollDetalleDoctos.Add(T)
                    Next


                Case 5

                    'Gestión Documento
                    Dim TempDoctos = (From G In Data.gsn_cls Where G.doc_cls.dsi_cls.id_dsi = DocNum _
                                 Order By G.id_gsn Descending _
                                 Select New With {G.id_gsn, _
                                                  G.gsn_fec, _
                                                  G.gsn_hor, _
                                                  G.eje_cls.eje_nom, _
                                                  G.gsn_fec_pag, _
                                                  G.gsn_hor_pag, _
                                                  G.gsn_fec_prx, _
                                                  G.gsn_hor_prx, _
                                                  G.gsn_obs}).Skip(sesion.page_gestion_doc)



                    For Each T In TempDoctos.Take(6)
                        CollDetalleDoctos.Add(T)
                    Next

            End Select

            If Not IsNothing(CollDetalleDoctos) Then

                Return CollDetalleDoctos

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosDevuelve(ByVal id_doc As Integer) As doc_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve documentos por rango de rut cliente sucursal y responsabilidad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  04/08/2010         Se modifica al rescatar cliente segun tipo Cliente    
        'A Saldivar                  12/01/2011         Se agrega paginacion        
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Documentos As doc_cls = (From o In data.doc_cls Where o.id_doc = id_doc).First


            Return Documentos


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DocumentosDevuelve(ByVal Rut_dsd As Long, ByVal rut_hst As Long, ByVal Res_dsd As Integer _
                                      , ByVal Res_hst As Integer, ByVal Suc_dsd As Integer, ByVal Suc_hst As Integer) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve documentos por rango de rut cliente sucursal y responsabilidad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  04/08/2010         Se modifica al rescatar cliente segun tipo Cliente    
        'A Saldivar                  12/01/2011         Se agrega paginacion        
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Documentos = (From o In data.doc_cls Where (o.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= Rut_dsd) _
             And (o.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_hst) _
             And (o.opo_cls.ope_cls.ope_res_son >= Res_dsd And o.opo_cls.ope_cls.ope_res_son <= Res_hst) _
             And (o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_suc >= Suc_dsd) _
             And (o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_suc <= Suc_hst) _
             And (o.opo_cls.ope_cls.id_P_0030 = 2 Or o.opo_cls.ope_cls.id_P_0030 = 3 Or o.opo_cls.ope_cls.id_P_0030 = 4) _
             And (o.doc_sdo_cli > 0) _
             Select o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc, _
                         Nombre_Cliente = If(o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                             o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim & " " & o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim & " " & o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim, _
                                             o.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim), _
                                o.dsi_cls.deu_cls.deu_ide, _
                                Nombre_Deudor = If(o.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                   o.dsi_cls.deu_cls.deu_rso & " " & o.dsi_cls.deu_cls.deu_ape_ptn & " " & o.dsi_cls.deu_cls.deu_ape_mtn, _
                                                   o.dsi_cls.deu_cls.deu_rso), _
                                o.opo_cls.ope_cls.opn_cls.id_P_0031, _
                                TIP_DOC = o.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                                o.id_doc, _
                                N_Doc = o.dsi_cls.dsi_num, _
                                N_de_Cuotas = o.dsi_cls.dsi_flj_num, _
                                o.opo_cls.ope_cls.opn_cls.opn_mto_doc, _
                                Monto_Doc = o.dsi_cls.dsi_mto, _
                                MONTO_ANTICIPO = o.dsi_cls.dsi_mto_ant, _
                                Saldo_Neto = o.doc_sdo_cli, _
                                Saldo_Mora = (o.doc_sdo_cli * o.opo_cls.ope_cls.ope_fac_cam), _
                                Fecha_Otog = o.opo_cls.opo_fec_oto, _
                                Fecha_Vto = o.opo_cls.ope_cls.opn_cls.opn_fev, _
                                o.opo_cls.ope_cls.opn_cls.id_P_0023, _
                                Moneda = o.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                                o.opo_cls.ope_cls.ope_res_son).Skip(sesion.NroPaginacion).Take(15)




            Return Documentos


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function gastos_ope_calcula(ByVal nro_ope As Integer) As Double

        Dim data As New DataClsFactoringDataContext
        Dim opn As opn_cls = (From o In data.opn_cls Where o.id_opn = nro_ope Select o).First()
        Dim mto_gasto, mtoafecto, mtoexento As Double

        Try

            Dim gas_def = From d In data.gdn_cls Where d.id_opn = nro_ope Select d.gto_cls

            For Each d In gas_def

                If d.id_P_0036 = 2 Then
                    mto_gasto = (d.gto_mto * opn.opn_can_ddr)
                ElseIf d.id_P_0036 = 3 Then
                    mto_gasto = (d.gto_mto * opn.opn_can_doc)
                Else
                    mto_gasto = d.gto_mto
                End If

                If d.gto_iva.Trim().ToUpper() = "S" Then
                    mtoafecto += mto_gasto
                Else
                    mtoexento += mto_gasto
                End If

            Next

        Catch ex As Exception
            mto_gasto = 0
        End Try

        Try

            Dim gas_fij = From d In data.gfn_cls Where d.id_opn = nro_ope Select d

            For Each d In gas_fij
                mtoexento += d.gfn_mto
            Next

        Catch ex As Exception
            mto_gasto = 0
        End Try

        _GastosAfecto = mtoafecto
        _GastosExento = mtoexento

        Return mtoafecto + mtoexento

    End Function

#End Region

    Public Function DocumentosOtorgagos_RetornaDoctos(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                              ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                              ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                              ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                              ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                              ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16, _
                                              ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                              ByVal estado1 As Int16, ByVal estado2 As Int16, ByVal estado3 As Int16, ByVal estado4 As Int16, ByVal estado5 As Int16, ByVal estado6 As Int16, ByVal estado7 As Int16, ByVal estado8 As Int16, ByVal estado9 As Int16, ByVal estado10 As Int16, ByVal estado11 As Int16, ByVal estado12 As Int16, _
                                              Optional ByVal NroAplicacion As Integer = 0) As Object

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documento en particular 
        'Creado por Jaime Santos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        'JSANTOS                    10/12/2008        - Se agrega Criterio de Selección fecha de Vcto.
        '                                             - Se agregan Campos: Nombre Deudor, Nro. Otorgamiento, Descripción Estado Cobranza 
        'JSANTOS                    17/12/2008        - Se agregan Criterios de Estado 
        'JLAGOS                     18/03/2009        - Se agrega como criterio de busqueda, el numero de la aplicaion y la devolucion de K de ingresos y aplicaciones
        'JLAGOS                     19/03/2009        - Se cambia los saldos de cliente y deudor, descontandole los pagos pendientes
        'SHenriquez                 01/10/2012        - Se agregan calificaciones por documento
        '**************************************************************************************************************************************************************

        Try

            Dim coll As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            '       Dim Temporal_doc = (From doc1 In Data.doc_cls _
            '                           Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
            '     (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
            '     (doc1.id_opo >= nro_operacion1 And doc1.id_opo <= nro_operacion2) And _
            '     (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
            '     (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
            '     (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
            '     (doc1.dsi_cls.dsi_fev_rea >= CDate(fec_vcto1) And doc1.dsi_cls.dsi_fev_rea <= CDate(fec_vcto2)) And _
            '     (doc1.dsi_cls.id_P_0011 = estado1 Or _
            '      doc1.dsi_cls.id_P_0011 = estado2 Or _
            '      doc1.dsi_cls.id_P_0011 = estado3 Or _
            '      doc1.dsi_cls.id_P_0011 = estado4 Or _
            '      doc1.dsi_cls.id_P_0011 = estado5 Or _
            '      doc1.dsi_cls.id_P_0011 = estado6 Or _
            '      doc1.dsi_cls.id_P_0011 = estado7 Or _
            '      doc1.dsi_cls.id_P_0011 = estado8 Or _
            '      doc1.dsi_cls.id_P_0011 = estado9 Or _
            '      doc1.dsi_cls.id_P_0011 = estado10 Or _
            '      doc1.dsi_cls.id_P_0011 = estado11 Or _
            '      doc1.dsi_cls.id_P_0011 = estado12) And _
            '      doc1.dsi_cls.dsi_flj = "N" _
            'Select doc1.id_doc, doc1.doc_sdo_cli, doc1.dsi_cls.ope_cls.opn_cls.id_P_0012)

            '       For Each D In Temporal_doc

            '           Dim Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = D.id_doc And _
            '                                                                      I.id_P_0053 = 2 And _
            '                                                                     (I.ing_vld_rcz = "S" Or _
            '                                                                        I.ing_vld_rcz = "I" Or _
            '                                                                        I.ing_vld_rcz = "V" Or _
            '                                                                        I.ing_vld_rcz = "C" Or _
            '                                                                        I.ing_vld_rcz = "L") And _
            '                                                                        I.ing_pro = "N" And _
            '                                                                        I.egr_sec_cls.egr_cls.id_apl <> NroAplicacion _
            '                                                                Select (I.ing_mto_abo / I.ing_fac_cam)).Sum
            '           If Ingresos Is Nothing Then Ingresos = 0

            '           Dim Doctos = From doc1 In Data.doc_cls _
            '                        Join c In Data.clf_cls On c.id_dsi Equals doc1.dsi_cls.id_dsi _
            '                        Where doc1.id_doc = D.id_doc And ((D.doc_sdo_cli - Ingresos) > 0 Or D.id_P_0012 = 3) _
            '       Select New With {doc1.id_doc, _
            '                        doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
            '                        doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
            '                        doc1.dsi_cls.deu_ide, _
            '                       .Deudor = IIf(doc1.dsi_cls.deu_cls.id_P_0044 = 1, doc1.dsi_cls.deu_cls.deu_rso & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn & "  " & doc1.dsi_cls.deu_cls.deu_ape_ptn, doc1.dsi_cls.deu_cls.deu_rso), _
            '                       .Cliente = doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
            '                        doc1.id_opo, _
            '                        doc1.opo_cls.opo_otg, _
            '                        doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
            '                       .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
            '                       .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
            '                        doc1.dsi_cls.id_P_0011, _
            '                       .EstadoDocto = doc1.dsi_cls.P_0011_cls.pnu_des, _
            '                        doc1.dsi_cls.dsi_num, _
            '                        doc1.dsi_cls.dsi_mto, _
            '                        doc1.dsi_cls.dsi_flj, _
            '                        doc1.dsi_cls.dsi_flj_num, _
            '                        doc1.doc_num_ren, _
            '                        doc1.dsi_cls.dsi_cei, _
            '                        doc1.opo_cls.ope_cls.opn_cls.id_P_0012, _
            '                        doc1.opo_cls.ope_cls.opn_cls.P_0012_cls.pnu_des, _
            '                        doc1.dsi_cls.dsi_ntf, _
            '                        doc1.dsi_cls.id_P_0040, _
            '                       .EstadoVerifica = doc1.dsi_cls.P_0040_cls.pnu_des, _
            '                        doc1.dsi_cls.dsi_fec_emi, _
            '                       .doc_fev_ori = doc1.dsi_cls.dsi_fev_ori, _
            '                       .doc_fev = doc1.dsi_cls.dsi_fev, _
            '                       .doc_fev_rea = doc1.dsi_cls.dsi_fev_rea, _
            '                        doc1.dsi_cls.dsi_mto_ant, _
            '                        doc1.dsi_cls.dsi_ctd_dia, _
            '                        doc1.dsi_cls.dsi_pre_com, _
            '                        doc1.dsi_cls.dsi_dif_pre, _
            '                        doc1.dsi_cls.dsi_sal_pen, _
            '                        doc1.dsi_cls.dsi_sal_pag, _
            '                        doc1.dsi_cls.dsi_cms, _
            '                        doc1.dsi_cls.dsi_iva_cms, _
            '                        doc1.doc_gto, _
            '                        doc1.doc_ful_pgo, _
            '                        doc1.doc_int_dvg, _
            '                        doc1.doc_ful_dvg, _
            '                        doc1.doc_fct, _
            '                        doc1.id_suc_cbz, _
            '                       .SucCobranza = doc1.suc_cls.suc_des_cra, _
            '                        doc1.id_suc_rcd, _
            '                       .SucRecauda = doc1.suc_cls.suc_des_cra, _
            '                        doc1.id_cco, _
            '                        doc1.cco_cls.cco_num, _
            '                       .EstadoCobranza = doc1.cco_cls.cco_des, _
            '                        doc1.doc_fec_cco, _
            '                        doc1.cco_cls.cco_des, _
            '                        doc1.dsi_cls.dsi_cbz, _
            '                        doc1.dsi_cls.dsi_cbz_son, _
            '                        doc1.dsi_cls.id_PL_000047, _
            '                        .doc_sdo_cli = (doc1.doc_sdo_cli - Ingresos), _
            '                        .doc_sdo_ddr = (doc1.doc_sdo_ddr - Ingresos), _
            '                        doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
            '                        doc1.doc_not_cre, _
            '                       .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
            '                       .tasa = 0.0, _
            '                       .nota_cred = 0.0, _
            '                       .interes = 0.0, _
            '                       .MontoPagar = 0.0, _
            '                       .PagaDeudor = "S", _
            '                        doc1.opo_cls.ope_cls.ope_fac_cam, _
            '                        doc1.opo_cls.ope_cls.ope_fec_sim, _
            '                        doc1.opo_cls.ope_cls.ope_dif_pre, _
            '                        doc1.opo_cls.ope_cls.ope_lnl, _
            '                        doc1.doc_tas_ren, _
            '                        doc1.dsi_cls.dsi_fev_ori, _
            '                        c.cal_oto_gam, _
            '                        c.cal_obj_eti, _
            '                        c.cal_sub_jet, _
            '                        c.cal_arr_ast, _
            '                        c.cal_def_ini, _
            '                       .Cantidad_Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
            '                                                                      I.id_P_0053 = 2 And _
            '                                                                     (I.ing_vld_rcz = "I" Or _
            '                                                                      I.ing_vld_rcz = "V" Or _
            '                                                                      I.ing_vld_rcz = "C") And _
            '                                                                      I.ing_pro = "N" _
            '                                              Select I.id_ing_sec).Count, _
            '                       .Aplicacion = (From I In Data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
            '                                                                       I.id_P_0053 = 2 And _
            '                                                                       I.egr_sec_cls.egr_cls.id_apl = NroAplicacion _
            '                                       Select I.id_ing_sec).Count}

            '           For Each P In Doctos
            '               coll.Add(P)
            '           Next


            '       Next

            Dim Temporal_doc = From doc1 In Data.doc_cls _
                               Join c In Data.clf_cls On c.id_dsi Equals doc1.dsi_cls.id_dsi _
                                Where (doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) And _
                                      (doc1.dsi_cls.deu_ide >= rut_deudor1 And doc1.dsi_cls.deu_ide <= rut_deudor2) And _
                                      (doc1.id_opo >= nro_operacion1 And doc1.id_opo <= nro_operacion2) And _
                                      (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                                      (doc1.dsi_cls.dsi_num >= nro_docto1 And doc1.dsi_cls.dsi_num <= nro_docto2) And _
                                      (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                                      (doc1.dsi_cls.dsi_fev_rea >= CDate(fec_vcto1) And doc1.dsi_cls.dsi_fev_rea <= CDate(fec_vcto2)) And _
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
                                       doc1.dsi_cls.dsi_flj = "N" And (doc1.doc_sdo_cli > 0 Or doc1.dsi_cls.ope_cls.opn_cls.id_P_0012 = 3) _
             Select New With {doc1.id_doc, _
                                    doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                                    doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
                                    doc1.dsi_cls.deu_ide, _
                                   .Deudor = IIf(doc1.dsi_cls.deu_cls.id_P_0044 = 1, doc1.dsi_cls.deu_cls.deu_rso & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn & "  " & doc1.dsi_cls.deu_cls.deu_ape_ptn, doc1.dsi_cls.deu_cls.deu_rso), _
                                   .Cliente = doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
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
                                    .doc_sdo_cli = (doc1.doc_sdo_cli), _
                                    .doc_sdo_ddr = (doc1.doc_sdo_ddr), _
                                    doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                                    doc1.doc_not_cre, _
                                   .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                                   .tasa = 0.0, _
                                   .nota_cred = 0.0, _
                                   .interes = 0.0, _
                                   .MontoPagar = 0.0, _
                                   .PagaDeudor = "S", _
                                    doc1.opo_cls.ope_cls.ope_fac_cam, _
                                    doc1.opo_cls.ope_cls.ope_fec_sim, _
                                    doc1.opo_cls.ope_cls.ope_dif_pre, _
                                    doc1.opo_cls.ope_cls.ope_lnl, _
                                    doc1.doc_tas_ren, _
                                    doc1.dsi_cls.dsi_fev_ori, _
                                    c.cal_oto_gam, _
                                    c.cal_obj_eti, _
                                    c.cal_sub_jet, _
                                    c.cal_arr_ast, _
                                    c.cal_def_ini, _
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


            For Each P In Temporal_doc
                coll.Add(P)
            Next

            Return coll
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentoOtorgagoDevuelvePorId(ByVal id_doc As Long) As doc_cls

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve el objeto doc por su id
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 15/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_doc As doc_cls = (From doc1 In Data.doc_cls Where doc1.id_doc = id_doc).First

            Return Temporal_doc

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Documentos_a_cxc_retorna() As Collection

        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim col As New Collection

            For i = 0 To arreglo.Count - 1
                Dim valor As Integer
                valor = arreglo.Item(i)
                Dim doc = From d In data.doc_cls Where d.id_doc = valor Select _
                          d.id_opo, d.doc_ful_pgo, d.id_doc, d.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, d.dsi_cls.dsi_num

                For Each p In doc
                    col.Add(p)
                Next


            Next

            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub Columna_Archivo_Devuelve(ByVal dp As DropDownList, ByVal tipo As Integer)



        If tipo = 1 Then
            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim parametro = From p In data.pca_cls Select Codigo = p.id_pca, Descripcion = p.pca_des


            Dim RG As New FuncionesGenerales.RutinasWeb
            RG.Llenar_Drop(parametro, "Codigo", "Descripcion", dp)

        Else
            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim parametro = From p In data.pca_cls Where p.id_pca <> 9 And p.id_pca <> 10 Select Codigo = p.id_pca, Descripcion = p.pca_des


            Dim RG As New FuncionesGenerales.RutinasWeb
            RG.Llenar_Drop(parametro, "Codigo", "Descripcion", dp)


        End If

    End Sub

    Public Function DocumentosOtorgados_a_Modificar_Retorna(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                  ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                  ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                                  ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                                  ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                  ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16, _
                                                  ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                  ByVal estado1 As Int16, ByVal estado2 As Int16, ByVal estado3 As Int16, ByVal estado4 As Int16, ByVal estado5 As Int16, ByVal estado6 As Int16, ByVal estado7 As Int16, ByVal estado8 As Int16, ByVal estado9 As Int16, ByVal estado10 As Int16, ByVal estado11 As Int16, ByVal estado12 As Int16, _
                                                  ByVal nro_otg1 As Int64, ByVal nro_otg2 As Int64, _
                                                  ByVal cobr1 As String, ByVal cobr2 As String, _
                                                  ByVal fec_otg As DateTime, ByVal fec_otg2 As DateTime, _
                                                  ByVal obl As String, ByVal obl2 As String _
                                                  ) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documento en particular 
        'Creado por Jaime Santos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        'JSANTOS                    10/12/2008          Se agrega Criterio de Selección fecha de Vcto.
        '                                               Se agregan Campos: Nombre Deudor, Nro. Otorgamiento, Descripción Estado Cobranza 
        'JSANTOS                    17/12/2008          Se agregan Criterios de Estado 
        'Pgatica 
        'SHenriquez                 12/07/2012          Se agrega nro contrato
        'SHenriquez                 27/09/2012          Se agregan calificaciones por documento
        'SHenriquez                 01/10/2012          Se corrige nro de contrato
        '**************************************************************************************************************************************************
        Try

            Dim col As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_doc = From doc1 In Data.doc_cls _
                               Join DC In Data.doc_con_cls _
                               On doc1.id_doc Equals DC.id_doc _
                               Join Clf In Data.clf_cls _
                               On Clf.id_dsi Equals doc1.dsi_cls.id_dsi _
          Where (CLng(doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) >= CLng(rut_cliente1) And _
                 CLng(doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) <= CLng(rut_cliente2)) _
            And (CLng(doc1.dsi_cls.deu_ide) >= CLng(rut_deudor1) And _
                 CLng(doc1.dsi_cls.deu_ide) <= CLng(rut_deudor2)) _
                 And (doc1.dsi_cls.ope_cls.id_opn >= nro_otg1 And _
                        doc1.dsi_cls.ope_cls.id_opn <= nro_otg2) _
                 And (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And _
                        doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                       (doc1.dsi_cls.dsi_num >= nro_docto1 And _
                        doc1.dsi_cls.dsi_num <= nro_docto2) And _
                       (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And _
                        doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                       (doc1.dsi_cls.dsi_fev_rea >= CDate(fec_vcto1) And _
                        doc1.dsi_cls.dsi_fev_rea <= CDate(fec_vcto2)) And _
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
                        doc1.dsi_cls.id_P_0011 = estado12 And _
                        doc1.dsi_cls.id_P_0011 <> 5) And _
                        doc1.dsi_cls.dsi_flj = "N" _
                        And (doc1.opo_cls.opo_fec_oto >= CDate(fec_otg) _
                        And doc1.opo_cls.opo_fec_oto <= CDate(fec_otg2)) _
                        And (doc1.doc_con_obl = obl Or doc1.doc_con_obl = obl2) _
                        And (doc1.dsi_cls.dsi_cbz_son = cobr1 Or doc1.dsi_cls.dsi_cbz_son = cobr2) _
                        Order By doc1.dsi_cls.id_ope, doc1.dsi_cls.dsi_num _
            Select New With {doc1.id_doc, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                             doc1.dsi_cls.deu_ide, _
                            .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, doc1.dsi_cls.deu_cls.deu_rso & " " & If(doc1.dsi_cls.deu_cls.deu_ape_ptn = Nothing, "", doc1.dsi_cls.deu_cls.deu_ape_ptn) & " " & If(doc1.dsi_cls.deu_cls.deu_ape_mtn = Nothing, "", doc1.dsi_cls.deu_cls.deu_ape_mtn), doc1.dsi_cls.deu_cls.deu_rso), _
                            .cli_rso = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn = Nothing, "", doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn) & " " & If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn = Nothing, "", doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn), doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
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
                             doc1.doc_sdo_cli, _
                             doc1.doc_sdo_ddr, _
                             doc1.doc_obs_cob, _
                             doc1.doc_fec_dem, _
                             doc1.doc_fec_cas, _
                             doc1.doc_pag_ddr, _
                             doc1.dsi_cls.deu_cls.deu_con_cbz, _
                             doc1.opo_cls.opo_fec_oto, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                            .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            .Interes = 0, _
                            .MontoPagar = 0, _
                            doc1.doc_not_cre, _
                            doc1.dsi_cls.id_ope, _
                            doc1.dsi_cls.id_dsi, _
                            .PagaDeudor = "S", _
                            Clf.cal_oto_gam, _
                            Clf.cal_obj_eti, _
                            Clf.cal_sub_jet, _
                            Clf.cal_arr_ast, _
                            Clf.cal_def_ini, _
                            .Contrato = If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                        If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                        If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), ""), _
                            doc1.opo_cls.ope_cls.id_opn, _
                             .recurso = If(doc1.opo_cls.ope_cls.opn_cls.opn_res_son = "0", "NO", "SI")}

            If NroRow = 0 Then
                NroRow = Temporal_doc.Count
            End If

            For Each P In Temporal_doc.Skip(page_dig).Take(15)
                col.Add(P)
            Next
            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosOtorgados_a_Modificar_Retorna2(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                              ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                              ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                              ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                              ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                              ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16, _
                                              ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                              ByVal estado1 As Int16, ByVal estado2 As Int16, ByVal estado3 As Int16, ByVal estado4 As Int16, ByVal estado5 As Int16, ByVal estado6 As Int16, ByVal estado7 As Int16, ByVal estado8 As Int16, ByVal estado9 As Int16, ByVal estado10 As Int16, ByVal estado11 As Int16, ByVal estado12 As Int16, _
                                              ByVal nro_otg1 As Int64, ByVal nro_otg2 As Int64, _
                                              ByVal cobr1 As String, ByVal cobr2 As String, _
                                              ByVal fec_otg As DateTime, ByVal fec_otg2 As DateTime, _
                                              ByVal obl As String, ByVal obl2 As String, ByVal contr As String _
                                              ) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documento en particular segun numero de contrato
        'Creado por Sebastian Henriquez C.
        'Fecha Creacion: 20/09/1012
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                 27/09/2012          Se agregan calificaciones por documento 
        'SHenriquez                 01/10/2012          Se corrige nro de contrato
        '**************************************************************************************************************************************************
        Try

            Dim col As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_doc = From doc1 In Data.doc_cls _
                               Join DC In Data.doc_con_cls _
                               On doc1.id_doc Equals DC.id_doc _
                               Join Clf In Data.clf_cls _
                               On Clf.id_dsi Equals doc1.dsi_cls.id_dsi _
          Where (CLng(doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) >= CLng(rut_cliente1) And _
                 CLng(doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) <= CLng(rut_cliente2)) _
            And (CLng(doc1.dsi_cls.deu_ide) >= CLng(rut_deudor1) And _
                 CLng(doc1.dsi_cls.deu_ide) <= CLng(rut_deudor2)) _
                 And (doc1.opo_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And _
                        doc1.opo_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) And _
                       (doc1.dsi_cls.dsi_num >= nro_docto1 And _
                        doc1.dsi_cls.dsi_num <= nro_docto2) And _
                       (doc1.dsi_cls.dsi_flj_num >= nro_cuota1 And _
                        doc1.dsi_cls.dsi_flj_num <= nro_cuota2) And _
                       (doc1.dsi_cls.dsi_fev_rea >= CDate(fec_vcto1) And _
                        doc1.dsi_cls.dsi_fev_rea <= CDate(fec_vcto2)) And _
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
                        doc1.dsi_cls.id_P_0011 = estado12 And _
                        doc1.dsi_cls.id_P_0011 <> 5) And _
                        doc1.dsi_cls.dsi_flj = "N" _
                        And (doc1.opo_cls.ope_cls.id_opn >= nro_otg1 _
                        And doc1.opo_cls.ope_cls.id_opn <= nro_otg2) _
                        And (doc1.opo_cls.opo_fec_oto >= CDate(fec_otg) _
                        And doc1.opo_cls.opo_fec_oto <= CDate(fec_otg2)) _
                        And (doc1.doc_con_obl = obl Or doc1.doc_con_obl = obl2) _
                        And (doc1.dsi_cls.dsi_cbz_son = cobr1 Or doc1.dsi_cls.dsi_cbz_son = cobr2) _
                        And (If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                             If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                             If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), "") = contr) _
                        Order By doc1.dsi_cls.id_ope, doc1.dsi_cls.dsi_num _
            Select New With {doc1.id_doc, _
                             doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                             doc1.dsi_cls.deu_ide, _
                            .Deudor = If(doc1.dsi_cls.deu_cls.id_P_0044 = 1, doc1.dsi_cls.deu_cls.deu_rso & " " & If(doc1.dsi_cls.deu_cls.deu_ape_ptn = Nothing, "", doc1.dsi_cls.deu_cls.deu_ape_ptn) & " " & If(doc1.dsi_cls.deu_cls.deu_ape_mtn = Nothing, "", doc1.dsi_cls.deu_cls.deu_ape_mtn), doc1.dsi_cls.deu_cls.deu_rso), _
                            .cli_rso = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn = Nothing, "", doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn) & " " & If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn = Nothing, "", doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn), doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
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
                             doc1.doc_sdo_cli, _
                             doc1.doc_sdo_ddr, _
                             doc1.doc_obs_cob, _
                             doc1.doc_fec_dem, _
                             doc1.doc_fec_cas, _
                             doc1.doc_pag_ddr, _
                             doc1.dsi_cls.deu_cls.deu_con_cbz, _
                             doc1.opo_cls.opo_fec_oto, _
                             doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
                            .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            .Interes = 0, _
                            .MontoPagar = 0, _
                            doc1.doc_not_cre, _
                            doc1.dsi_cls.id_ope, _
                            doc1.dsi_cls.id_dsi, _
                            .PagaDeudor = "S", _
                            Clf.cal_oto_gam, _
                            Clf.cal_obj_eti, _
                            Clf.cal_sub_jet, _
                            Clf.cal_arr_ast, _
                            Clf.cal_def_ini, _
                            .Contrato = If(DC.cod_emp IsNot Nothing, DC.cod_emp.ToString(), "") & If(DC.ofi_cin IsNot Nothing, DC.ofi_cin.ToString(), "") & _
                                        If(DC.pro_duc IsNot Nothing, DC.pro_duc.ToString(), "") & _
                                        If(DC.con_tra IsNot Nothing, DC.con_tra.ToString(), "") & If(DC.dig_ver_doc IsNot Nothing, DC.dig_ver_doc.ToString(), ""), _
                             doc1.opo_cls.ope_cls.id_opn, _
                             .recurso = If(doc1.opo_cls.ope_cls.opn_cls.opn_res_son = "0", "NO", "SI")}

            If NroRow = 0 Then
                NroRow = Temporal_doc.Count
            End If

            For Each P In Temporal_doc.Skip(page_dig).Take(15)
                col.Add(P)
            Next
            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Clientes_Deudores_retorna_por_Operación(ByVal id_ope As Integer, ByVal cli_deu As Char, Optional ByVal dr_deu As DropDownList = Nothing, Optional ByVal llenardrop As Boolean = False) As Collection
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los clientes asociados a un Deudor 
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 25/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Dim col As New Collection
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            If cli_deu = "C" Then

                Dim ClientesAsociados = From ope In Data.ope_cls _
                               Where ope.id_ope = id_ope _
                               Select New With {.cli_idc = ope.opn_cls.eva_cls.cli_cls.cli_idc, _
                                                        .cli_rso = If(ope.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, ope.opn_cls.eva_cls.cli_cls.cli_rso.Trim & "  " & ope.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim & "  " & ope.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim, _
                                                                      ope.opn_cls.eva_cls.cli_cls.cli_rso)}


                For Each p In ClientesAsociados
                    col.Add(p)

                Next
                Return col
                ''If LlenaGrid Then
                ''    Gv.DataSource = ClientesAsociados
                ''    Gv.DataBind()
                ''Else
                'Return ClientesAsociados
                '      End If



            End If

            If cli_deu = "D" Then

                Dim DeudoresAsociados = From dsi In Data.dsi_cls _
                               Where CInt(dsi.id_ope) = id_ope _
                               Select New With {.cli_idc = dsi.deu_ide, _
                                                        .cli_rso = If(dsi.deu_cls.id_P_0044 = 1, _
                                                                     dsi.deu_cls.deu_rso.Trim & " " & dsi.deu_cls.deu_ape_ptn.Trim & " " & dsi.deu_cls.deu_ape_mtn.Trim, dsi.deu_cls.deu_rso.Trim)} Distinct



                If llenardrop Then

                    Dim RG As New FuncionesGenerales.RutinasWeb
                    RG.Llenar_Drop(DeudoresAsociados, "cli_idc", "cli_rso", dr_deu)

                Else

                    Return DeudoresAsociados

                End If


                'If LlenaGrid Then
                '    Gv.DataSource = ClientesAsociados
                '    Gv.DataBind()
                'Else

                'For Each o In DeudoresAsociados
                '    If o.cli_idc = cliente Then

                '        Return True

                '    Else

                '        Return False

                ' 
            End If



        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function OperacionesPorClienteDevuelve(ByVal RutCliente As String, ByVal tipo As Integer, Optional ByVal LlenaGrid As Boolean = False, Optional ByVal GV As GridView = Nothing) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las operaciones anteriores de un cliente en particular
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 30/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion         
        'A Saldivar                  10/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try
            'Ingresadas con y sin cuadrar
            If tipo = 1 Then

                Dim Data As New CapaDatos.DataClsFactoringDataContext
                Dim operacion As New ope_cls
                Dim col As New Collection
                Dim sesion As New ClsSession.SesionOperaciones


                Dim OpeAnt = (From Ope In Data.ope_cls Where Ope.opn_cls.eva_cls.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And Ope.id_P_0030 = 1 And Ope.opn_cls.id_P_0082 = 2 Order By Ope.id_ope Descending _
                Select New With {Ope.id_ope, _
                Ope.opn_cls.id_opn, _
                Ope.id_ldc, _
                Ope.id_fct, _
                Ope.opn_cls.id_P_0023_com, _
                Ope.opn_cls.id_P_0023_fla, _
                Ope.id_P_0030, _
                Ope.id_P_0070, _
                Ope.id_P_0077, _
                Ope.id_P_0083, _
                Ope.id_P_0104, _
                Ope.ope_cdo, _
                Ope.ope_cnt, _
                Ope.opn_cls.id_suc, _
                Ope.opn_cls.opn_com_fla, _
                Ope.ope_com_fog, _
                Ope.opn_cls.opn_com_max, _
                Ope.opn_cls.opn_com_min, _
                Ope.opn_cls.opn_por_com, _
                Ope.opn_cls.id_bco, _
                Ope.opn_cls.opn_cta_cte, _
                Ope.ope_com_tot, _
                Ope.ope_com_tot_pes, _
                Ope.ope_con_cje, _
                Ope.ope_cuo, _
                Ope.ope_dif_pre, _
                Ope.ope_fac_cam, _
                Ope.ope_fac_cam_pag, _
                Ope.ope_fec_anl, _
                Ope.ope_fec_ctb, _
                Ope.ope_obs_sim, _
                Ope.ope_fec_sim, _
                Ope.ope_fev, _
                Ope.ope_fog_gar, _
                Ope.ope_fog_mto_lin, _
                Ope.ope_fog_son, _
                Ope.ope_fog_ven, _
                Ope.opn_cls.id_P_0023, _
                Ope.opn_cls.id_P_0031, _
                Ope.opn_cls.id_P_0056, _
                Ope.opn_cls.id_P_0082, _
                Ope.opn_cls.opn_fec, _
                Ope.opn_cls.opn_fec_neg, _
                Ope.opn_cls.opn_fev, _
                Ope.opn_cls.opn_can_doc, _
                Ope.opn_cls.id_P_0012, _
                Ope.ope_ptl, _
                Ope.ope_lnl, _
                Ope.ope_res_son, _
                Ope.opn_cls.opn_spr_ead, _
                Ope.opn_cls.opn_tas_bas, _
                Ope.opn_cls.opn_tas_neg, _
                Ope.opn_cls.opn_tas_moa, _
                Ope.opn_cls.eva_cls.cli_idc, _
                Ope.ope_mto_ant, _
                Ope.ope_tot_gir, _
                Ope.ope_imp_ope, _
                Ope.ope_por_ant, _
                Ope.ope_int_dev, _
                Ope.ope_iva_com, _
                Ope.ope_mon_gas, _
                Ope.opn_cls.opn_pto_spr, _
                Ope.opn_cls.opn_mto_doc, _
                Ope.ope_mto_scb, _
                .TipoDocumento = Ope.opn_cls.P_0031_cls.pnu_des, _
                Ope.P_0104_cls.pnu_des(), _
                Ope.opn_cls.opn_ant_014, _
                Ope.opn_cls.opn_gen_gmf, _
                Ope.ope_val_gmf, _
                Ope.ope_mon_gas_afe, _
                Ope.opn_cls.cal_oto_gam, _
                Ope.opn_cls.opn_dia_vto}).Skip(sesion.page_dig)

                For Each p In OpeAnt.Take(5)
                    col.Add(p)
                Next

                If LlenaGrid Then

                    GV.DataSource = col
                    GV.DataBind()

                    Return col
                Else
                    Return col
                End If

                'Ingresadas  y cuadradas
            ElseIf tipo = 2 Then

                Dim Data As New CapaDatos.DataClsFactoringDataContext
                Dim operacion As New ope_cls
                Dim col As New Collection
                Dim sesion As New ClsSession.SesionOperaciones

                Dim OpeAnt = (From Ope In Data.ope_cls Where Ope.opn_cls.eva_cls.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And _
                                                             Ope.id_P_0030 = 1 And _
                                                             Ope.ope_cdo = "S" And _
                                                             Ope.opn_cls.id_P_0082 = 2 _
                             Order By Ope.id_ope Descending _
                Select New With {Ope.id_ope, _
                Ope.opn_cls.id_opn, _
                Ope.id_ldc, _
                Ope.id_fct, _
                Ope.opn_cls.id_P_0023_com, _
                Ope.opn_cls.id_P_0023_fla, _
                Ope.opn_cls.id_bco, _
                Ope.opn_cls.opn_cta_cte, _
                Ope.id_P_0030, _
                Ope.id_P_0070, _
                Ope.id_P_0077, _
                Ope.id_P_0083, _
                Ope.id_P_0104, _
                Ope.ope_cdo, _
                Ope.ope_cnt, _
                Ope.opn_cls.id_suc, _
                Ope.opn_cls.opn_com_fla, _
                   Ope.ope_com_fog, _
                   Ope.opn_cls.opn_com_max, _
                   Ope.opn_cls.opn_com_min, _
                   Ope.opn_cls.opn_por_com, _
                   Ope.ope_com_tot, _
                   Ope.ope_com_tot_pes, _
                   Ope.ope_con_cje, _
                   Ope.ope_cuo, _
                   Ope.ope_dif_pre, _
                   Ope.ope_fac_cam, _
                   Ope.ope_fac_cam_pag, _
                   Ope.ope_fec_anl, _
                   Ope.ope_fec_ctb, _
                   Ope.ope_fec_sim, _
                   Ope.ope_fev, _
                   Ope.ope_fog_gar, _
                   Ope.ope_fog_mto_lin, _
                   Ope.ope_fog_son, _
                   Ope.ope_fog_ven, _
                   Ope.opn_cls.id_P_0023, _
                   Ope.opn_cls.id_P_0031, _
                   Ope.opn_cls.id_P_0056, _
                   Ope.opn_cls.id_P_0082, _
                   Ope.opn_cls.opn_fec, _
                   Ope.opn_cls.opn_fec_neg, _
                   Ope.opn_cls.opn_fev, _
                   Ope.ope_lnl, _
                   Ope.opn_cls.opn_can_doc, _
                   Ope.opn_cls.id_P_0012, _
                   Ope.ope_ptl, _
                   Ope.ope_res_son, _
                   Ope.opn_cls.opn_spr_ead, _
                   Ope.opn_cls.opn_tas_bas, _
                   Ope.opn_cls.opn_tas_neg, _
                   Ope.opn_cls.opn_tas_moa, _
                   Ope.ope_por_ant, _
                   Ope.opn_cls.eva_cls.cli_idc, _
                   Ope.ope_mto_ant, _
                   Ope.ope_imp_ope, _
                   Ope.ope_int_dev, _
                   Ope.ope_tot_gir, _
                   Ope.ope_iva_com, _
                   Ope.ope_mon_gas, _
                    Ope.ope_obs_sim, _
                    Ope.ope_mto_scb, _
                   Ope.opn_cls.opn_pto_spr, _
                   .TipoDocumento = Ope.opn_cls.P_0031_cls.pnu_des, _
                   Ope.opn_cls.opn_mto_doc, _
                   Ope.P_0104_cls.pnu_des(), _
                Ope.opn_cls.opn_ant_014, _
                Ope.opn_cls.opn_gen_gmf, _
                Ope.ope_val_gmf, _
                Ope.ope_mon_gas_afe, _
                Ope.opn_cls.cal_oto_gam}).Skip(sesion.page_dig)


                For Each p In OpeAnt.Take(5)

                    col.Add(p)

                Next

                If LlenaGrid Then

                    GV.DataSource = col
                    GV.DataBind()


                    Return col
                Else

                    Return col

                End If

                'SIMULADAS
            ElseIf tipo = 3 Then

                Dim Data As New CapaDatos.DataClsFactoringDataContext
                Dim operacion As New ope_cls
                Dim col As New Collection
                Dim sesion As New ClsSession.SesionOperaciones

                Dim OpeAnt = (From Ope In Data.ope_cls Where Ope.opn_cls.eva_cls.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And Ope.id_P_0030 = 2 And Ope.ope_cdo = "S" And Ope.opn_cls.id_P_0082 = 3 Order By Ope.id_ope Descending _
                Select New With {Ope.id_ope, _
                Ope.opn_cls.id_opn, _
                Ope.id_ldc, _
                Ope.id_fct, _
                Ope.opn_cls.id_P_0023_com, _
                Ope.opn_cls.id_P_0023_fla, _
                Ope.id_P_0030, _
                Ope.id_P_0070, _
                Ope.id_P_0077, _
                Ope.ope_lnl, _
                Ope.id_P_0083, _
                Ope.ope_obs_sim, _
                Ope.id_P_0104, _
                Ope.ope_por_ant, _
                Ope.ope_cdo, _
                Ope.ope_cnt, _
                Ope.opn_cls.opn_por_com, _
                Ope.opn_cls.id_bco, _
                Ope.opn_cls.opn_cta_cte, _
                Ope.opn_cls.opn_com_fla, _
                Ope.ope_com_fog, _
                Ope.opn_cls.opn_com_max, _
                Ope.opn_cls.opn_com_min, _
                Ope.opn_cls.id_suc, _
                Ope.ope_com_tot, _
                Ope.ope_com_tot_pes, _
                Ope.ope_con_cje, _
                Ope.ope_cuo, _
                Ope.ope_dif_pre, _
                Ope.ope_fac_cam, _
                Ope.ope_fac_cam_pag, _
                Ope.ope_fec_anl, _
                Ope.ope_fec_ctb, _
                Ope.ope_tot_gir, _
                Ope.ope_fec_sim, _
                Ope.ope_fev, _
                Ope.ope_fog_gar, _
                Ope.ope_fog_mto_lin, _
                Ope.ope_fog_son, _
                Ope.ope_fog_ven, _
                Ope.opn_cls.id_P_0023, _
                Ope.opn_cls.id_P_0031, _
                Ope.opn_cls.id_P_0056, _
                Ope.opn_cls.id_P_0082, _
                Ope.opn_cls.opn_fec, _
                Ope.opn_cls.opn_fec_neg, _
                Ope.opn_cls.opn_fev, _
                Ope.opn_cls.opn_can_doc, _
                Ope.opn_cls.id_P_0012, _
                Ope.ope_ptl, _
                Ope.ope_res_son, _
                Ope.ope_mon_gas, _
                Ope.opn_cls.opn_spr_ead, _
                Ope.opn_cls.opn_tas_bas, _
                Ope.opn_cls.opn_tas_neg, _
                Ope.opn_cls.opn_tas_moa, _
                Ope.opn_cls.eva_cls.cli_idc, _
                Ope.ope_mto_ant, _
                Ope.ope_imp_ope, _
                Ope.ope_int_dev, _
                Ope.ope_iva_com, _
                Ope.opn_cls.opn_pto_spr, _
                Ope.ope_mto_scb, _
                Ope.opn_cls.opn_mto_doc, _
                 .TipoDocumento = Ope.opn_cls.P_0031_cls.pnu_des, _
                Ope.P_0104_cls.pnu_des(), _
                Ope.opn_cls.opn_ant_014, _
                Ope.opn_cls.opn_gen_gmf, _
                Ope.ope_val_gmf, _
                Ope.ope_mon_gas_afe, _
                Ope.opn_cls.cal_oto_gam}).Skip(sesion.page_sim)

                For Each p In OpeAnt.Take(5)
                    col.Add(p)
                Next

                If LlenaGrid Then
                    GV.DataSource = col
                    GV.DataBind()
                End If

                Return col

            End If

        Catch ex As Exception

        End Try

    End Function

    Public Function documentosIngresados_RetornaSinPag(ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, Optional ByVal Todos As Boolean = False) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de dsiumento en particular 
        'Creado por Pablo Gatica.
        'Fecha Creacion: 16/09/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     03/12/2008          Se quita el NIT del Cliente como criterio de busqueda,
        '                                               ya que id_ope en unico
        'SHenriquez                 27/09/2012          Se agrega calificacion de otorgamiento
        '**************************************************************************************************************************************************
        Try

            Dim col As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            If Todos = False Then

                Dim Temporal_dsi = From dsi1 In Data.dsi_cls _
            Where dsi1.id_ope = nro_operacion2 _
           Select New With {dsi1.id_dsi, _
                   dsi1.ope_cls.opn_cls.eva_cls.cli_idc, _
                   dsi1.deu_ide, _
                   dsi1.deu_cls.deu_nom, _
                   dsi1.deu_cls.deu_ape_ptn, _
                   dsi1.deu_cls.deu_ape_mtn, _
                   .deu_rso = IIf(dsi1.deu_cls.id_P_0044 = 1, dsi1.deu_cls.deu_rso & " " & dsi1.deu_cls.deu_ape_ptn & "  " & dsi1.deu_cls.deu_ape_ptn, dsi1.deu_cls.deu_rso), _
                   dsi1.id_ope, _
                   dsi1.ope_cls.opn_cls.id_P_0031, _
                   .Tipodsito = dsi1.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                   dsi1.id_P_0011, _
                   .Estadodsito = dsi1.P_0011_cls.pnu_des, _
                   dsi1.dsi_num, _
                   dsi1.dsi_mto, _
                   dsi1.dsi_mto_fin, _
                   dsi1.dsi_flj, _
                   dsi1.dsi_flj_num, _
                   dsi1.dsi_num_ren, _
                   dsi1.dsi_cei, _
                   dsi1.ope_cls.opn_cls.id_P_0012, _
                   dsi1.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                   dsi1.dsi_ntf, _
                   dsi1.id_P_0040, _
                   .EstadoVerifica = dsi1.P_0040_cls.pnu_des, _
                   dsi1.dsi_fec_emi, _
                   dsi1.dsi_fev_ori, _
                   dsi1.dsi_fev, _
                   dsi1.dsi_fev_rea, _
                   dsi1.dsi_mto_ant, _
                   dsi1.dsi_ctd_dia, _
                   dsi1.dsi_pre_com, _
                   dsi1.dsi_dif_pre, _
                   dsi1.dsi_sal_pen, _
                   dsi1.dsi_sal_pag, _
                   dsi1.dsi_cms, _
                   dsi1.dsi_env_bci, _
                   dsi1.dsi_iva_cms, _
                   dsi1.dsi_gto, _
                   dsi1.dsi_tot_gir, _
                   dsi1.dsi_cbz, _
                   dsi1.dsi_rsp, _
                   dsi1.dsi_cbz_son, _
                   dsi1.id_PL_000047, _
                   dsi1.id_P_0112, _
                   dsi1.dsi_fev_cal, _
                   dsi1.cta_cte, _
                   .cal_oto_gam = (From c In Data.clf_cls Where c.id_dsi = dsi1.id_dsi Select c.cal_oto_gam).First}


                If Temporal_dsi.Count = 0 Then
                    Return Nothing
                Else

                    For Each p In Temporal_dsi
                        col.Add(p)
                    Next

                    Return col

                End If

            Else

                Dim Temporal_dsi = From dsi1 In Data.dsi_cls _
            Where dsi1.id_ope = nro_operacion2 _
           Select New With {dsi1.id_dsi, _
                   dsi1.ope_cls.opn_cls.eva_cls.cli_idc, _
                   dsi1.deu_ide, _
                   dsi1.deu_cls.deu_nom, _
                   dsi1.deu_cls.deu_ape_ptn, _
                   dsi1.deu_cls.deu_ape_mtn, _
                   .deu_rso = IIf(dsi1.deu_cls.id_P_0044 = 1, dsi1.deu_cls.deu_rso & " " & dsi1.deu_cls.deu_ape_ptn & "  " & dsi1.deu_cls.deu_ape_ptn, dsi1.deu_cls.deu_rso), _
                   dsi1.id_ope, _
                   dsi1.ope_cls.opn_cls.id_P_0031, _
                   .Tipodsito = dsi1.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                   dsi1.id_P_0011, _
                   .Estadodsito = dsi1.P_0011_cls.pnu_des, _
                   dsi1.dsi_num, _
                   dsi1.dsi_mto, _
                   dsi1.dsi_mto_fin, _
                   dsi1.dsi_flj, _
                   dsi1.dsi_flj_num, _
                   dsi1.dsi_num_ren, _
                   dsi1.dsi_cei, _
                   dsi1.ope_cls.opn_cls.id_P_0012, _
                   dsi1.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                   dsi1.dsi_ntf, _
                   dsi1.id_P_0040, _
                   .EstadoVerifica = dsi1.P_0040_cls.pnu_des, _
                   dsi1.dsi_fec_emi, _
                   dsi1.dsi_fev_ori, _
                   dsi1.dsi_fev, _
                   dsi1.dsi_fev_rea, _
                   dsi1.dsi_mto_ant, _
                   dsi1.dsi_ctd_dia, _
                   dsi1.dsi_pre_com, _
                   dsi1.dsi_dif_pre, _
                   dsi1.dsi_sal_pen, _
                   dsi1.dsi_sal_pag, _
                   dsi1.dsi_cms, _
                   dsi1.dsi_env_bci, _
                   dsi1.dsi_iva_cms, _
                   dsi1.dsi_gto, _
                   dsi1.dsi_tot_gir, _
                   dsi1.dsi_cbz, _
                   dsi1.dsi_rsp, _
                   dsi1.dsi_cbz_son, _
                   dsi1.id_PL_000047, _
                   dsi1.id_P_0112, _
                   dsi1.dsi_fev_cal, _
                   dsi1.cta_cte, _
                   .cal_oto_gam = (From c In Data.clf_cls Where c.id_dsi = dsi1.id_dsi Select c.cal_oto_gam).First}

                If Temporal_dsi.Count = 0 Then
                    Return Nothing
                Else

                    For Each p In Temporal_dsi
                        col.Add(p)
                    Next

                    Return col

                End If


            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function documentosIngresados_Retorna(ByVal nro_operacion1 As Integer, ByVal nro_operacion2 As Integer, Optional ByVal Todos As Boolean = False) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de dsiumento en particular 
        'Creado por Pablo Gatica.
        'Fecha Creacion: 16/09/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     03/12/2008          Se quita el NIT del Cliente como criterio de busqueda,
        '                                               ya que id_ope en unico
        'A Saldivar                 04/01/2011          se agrega busqueda paginada
        'S Henriquez                27/092012           Se agrega calificacion de otorgamiento
        'S Henriquez                01/10/2012          Se agregan calificaciones restantes    
        '**************************************************************************************************************************************************
        Try

            Dim col As New Collection
            Dim sesion As New ClsSession.ClsSession
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            If Todos = False Then
                Dim Temporal_dsi = (From dsi1 In Data.dsi_cls _
                                    Join c In Data.clf_cls On c.id_dsi Equals dsi1.id_dsi _
                                    Group Join dvf1 In Data.dvf_cls _
                                    On dvf1.id_dsi Equals dsi1.id_dsi Into JoinedDSI_DVF = Group _
                                    From dsi In JoinedDSI_DVF.DefaultIfEmpty() _
            Where dsi1.id_ope = nro_operacion2 _
           Select dsi1.id_dsi, _
                   dsi1.ope_cls.opn_cls.eva_cls.cli_idc, _
                   dsi1.deu_ide, _
                   dsi1.deu_cls.deu_nom, _
                   dsi1.deu_cls.deu_ape_ptn, _
                   dsi1.deu_cls.deu_ape_mtn, _
                   deu_rso = IIf(dsi1.deu_cls.id_P_0044 = 1, dsi1.deu_cls.deu_rso & " " & dsi1.deu_cls.deu_ape_ptn & "  " & dsi1.deu_cls.deu_ape_ptn, dsi1.deu_cls.deu_rso), _
                   dsi1.id_ope, _
                   dsi1.ope_cls.opn_cls.id_P_0031, _
                   Tipodsito = dsi1.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                   dsi1.id_P_0011, _
                   Estadodsito = dsi1.P_0011_cls.pnu_des, _
                   dsi1.dsi_num, _
                   dsi1.dsi_mto, _
                   dsi1.dsi_mto_fin, _
                   dsi1.dsi_flj, _
                   dsi1.dsi_flj_num, _
                   dsi1.dsi_num_ren, _
                   dsi1.dsi_cei, _
                   dsi1.ope_cls.opn_cls.id_P_0012, _
                   dsi1.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                   dsi1.dsi_ntf, _
                   dsi1.id_P_0040, _
                   EstadoVerifica = dsi1.P_0040_cls.pnu_des, _
                   dsi1.dsi_fec_emi, _
                   dsi1.dsi_fev_ori, _
                   dsi1.dsi_fev, _
                   dsi1.dsi_fev_rea, _
                   dsi1.dsi_mto_ant, _
                   dsi1.dsi_ctd_dia, _
                   dsi1.dsi_pre_com, _
                   dsi1.dsi_dif_pre, _
                   dsi1.dsi_sal_pen, _
                   dsi1.dsi_sal_pag, _
                   dsi1.dsi_cms, _
                   dsi1.dsi_env_bci, _
                   dsi1.dsi_iva_cms, _
                   dsi1.dsi_gto, _
                   dsi1.dsi_tot_gir, _
                   dsi1.dsi_cbz, _
                   dsi1.dsi_rsp, _
                   dsi1.dsi_cbz_son, _
                   dsi1.id_PL_000047, _
                   dsi1.id_P_0112, _
                   dsi1.cta_cte, _
                   dsi1.dsi_fev_cal, _
                   dsi.dvf_fev_rea, _
                   c.cal_oto_gam, _
                   c.cal_obj_eti, _
                   c.cal_sub_jet, _
                   c.cal_arr_ast, _
                   c.cal_def_ini).Skip(sesion.NroPaginacion_DetalleOpe)

                If Temporal_dsi.Count = 0 Then
                    Return Nothing
                Else

                    For Each p In Temporal_dsi
                        col.Add(p)
                    Next

                    Return col

                End If

            Else

                Dim Temporal_dsi = (From dsi1 In Data.dsi_cls _
                                    Join c In Data.clf_cls On c.id_dsi Equals dsi1.id_dsi _
                                    Group Join dvf1 In Data.dvf_cls _
                                    On dvf1.id_dsi Equals dsi1.id_dsi Into JoinedDSI_DVF = Group _
                                    From dsi In JoinedDSI_DVF.DefaultIfEmpty() _
                    Where dsi1.id_ope = nro_operacion2 _
                   Select dsi1.id_dsi, _
                           dsi1.ope_cls.opn_cls.eva_cls.cli_idc, _
                           dsi1.deu_ide, _
                           dsi1.deu_cls.deu_nom, _
                           dsi1.deu_cls.deu_ape_ptn, _
                           dsi1.deu_cls.deu_ape_mtn, _
                           deu_rso = IIf(dsi1.deu_cls.id_P_0044 = 1, dsi1.deu_cls.deu_rso & " " & dsi1.deu_cls.deu_ape_ptn & "  " & dsi1.deu_cls.deu_ape_ptn, dsi1.deu_cls.deu_rso), _
                           dsi1.id_ope, _
                           dsi1.ope_cls.opn_cls.id_P_0031, _
                           Tipodsito = dsi1.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                           dsi1.id_P_0011, _
                           Estadodsito = dsi1.P_0011_cls.pnu_des, _
                           dsi1.dsi_num, _
                           dsi1.dsi_mto, _
                           dsi1.dsi_mto_fin, _
                           dsi1.dsi_flj, _
                           dsi1.dsi_flj_num, _
                           dsi1.dsi_num_ren, _
                           dsi1.dsi_cei, _
                           dsi1.ope_cls.opn_cls.id_P_0012, _
                           dsi1.ope_cls.opn_cls.P_0012_cls.pnu_des, _
                           dsi1.dsi_ntf, _
                           dsi1.id_P_0040, _
                           EstadoVerifica = dsi1.P_0040_cls.pnu_des, _
                           dsi1.dsi_fec_emi, _
                           dsi1.dsi_fev_ori, _
                           dsi1.dsi_fev, _
                           dsi1.dsi_fev_rea, _
                           dsi1.dsi_mto_ant, _
                           dsi1.dsi_ctd_dia, _
                           dsi1.dsi_pre_com, _
                           dsi1.dsi_dif_pre, _
                           dsi1.dsi_sal_pen, _
                           dsi1.dsi_sal_pag, _
                           dsi1.dsi_cms, _
                           dsi1.dsi_env_bci, _
                           dsi1.dsi_iva_cms, _
                           dsi1.dsi_gto, _
                           dsi1.dsi_tot_gir, _
                           dsi1.dsi_cbz, _
                           dsi1.dsi_rsp, _
                           dsi1.dsi_cbz_son, _
                           dsi1.id_PL_000047, _
                           dsi1.id_P_0112, _
                           dsi1.cta_cte, _
                           dsi1.dsi_fev_cal, _
                           dsi.dvf_fev_rea, _
                           c.cal_oto_gam, _
                           c.cal_obj_eti, _
                           c.cal_sub_jet, _
                       c.cal_arr_ast, _
                       c.cal_def_ini).Skip(sesion.NroPaginacion_DetalleOpe)

                If Temporal_dsi.Count = 0 Then
                    Return Nothing
                Else

                    For Each p In Temporal_dsi
                        col.Add(p)
                    Next

                    Return col

                End If


            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ValidacionOperacionPuntual(ByVal rut_cliente As String, ByVal Ejecutivo As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos si un cliente puede crear una operacion puntual
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 24/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Cliente As cli_cls
            Dim Sistema As sis_cls
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Sistema = cg.SistemaDevuelve()

            Dim Cantidad As Integer = (From O In Data.opo_cls Where O.ope_cls.opn_cls.eva_cls.cli_idc = Format(rut_cliente, Var.FMT_RUT) _
                                                                And O.ope_cls.ope_ptl = "S" _
                                                                And O.ope_cls.id_P_0030 = 3).Count

            If Cantidad > Cliente.cli_ope_ptl Then
                MsgBox("Existen Operaciones Puntuales para Cliente," & Chr(13) & "Este Supera Rango", 64)
                Return False
            End If

            If Cantidad >= Sistema.sis_ope_ptl Then
                MsgBox("Existen Operaciones Puntuales para Cliente," & Chr(13) & "Este Supera Rango", 64)
                Return False
            End If

            Return True



        Catch ex As Exception
            Return True
        End Try

    End Function

    Public Sub OperacionesSimuladasTodasDevuelve(ByVal GV As GridView, _
                                                ByVal CodigoEje_Desde As Integer, _
                                                ByVal CodigoEje_Hasta As Integer, _
                                                ByVal Perfil As Integer, _
                                                ByVal RutCli_Desde As Long, _
                                                ByVal RutCli_Hasta As Long)

        '---------------------------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: devuelve todas las operaciones ingresadas a traves de una negociacion para que el perfil apruebe
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/09/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     03/12/2008        - Se agrega ejecutivo hasta y se quita criterio por estado.
        'JLagos                     27/04/2009        - Se agrega el cliente como criterio.
        'JLagos                     26/06/2009        - Se agrega validacion para que traiga solo las operaciones que esten listas, 
        '                                               esto quiere decir, que sea la primera firma o que la firma anterior ya fue realizada.
        'A. Saldivar                03/01/2011        - Se agrega paginacion
        '---------------------------------------------------------------------------------------------------------------------------------------------------

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll_Operaciones As New Collection

            '---------------------------------------------------------------------------------------------------------------
            'Traemos todas las operaciones segun criterio de busqueda
            '---------------------------------------------------------------------------------------------------------------
            Dim OperacionesIngresadas = (From O In Data.ope_cls Where O.id_P_0030 = 2 _
                                                                 And (O.opn_cls.eva_cls.id_eje >= CodigoEje_Desde _
                                                                 And O.opn_cls.eva_cls.id_eje <= CodigoEje_Hasta) _
                                                                 And (O.opn_cls.eva_cls.cli_idc >= Format(RutCli_Desde, Var.FMT_RUT) _
                                                                 And O.opn_cls.eva_cls.cli_idc <= Format(RutCli_Hasta, Var.FMT_RUT)) _
                                                                 And O.id_opn > 0 _
                                                                 And (O.opn_cls.id_P_0082 >= 2 _
                                                                 And O.opn_cls.id_P_0082 <= 3) Order By O.id_ope Descending _
                                            Select New With { _
                                                   .NroOpe = O.id_ope, _
                                                   .NroNeg = O.id_opn, _
                                                      .Rut = CLng(O.opn_cls.eva_cls.cli_idc), _
                                                  .Cliente = If(O.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                                O.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & _
                                                                O.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & _
                                                                O.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, O.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                                                 .FechaNeg = O.opn_cls.opn_fec_neg.Value.ToShortDateString, _
                                                   .MtoOpe = O.opn_cls.opn_mto_doc, _
                                                   .Factor = O.ope_fac_cam, _
                                                 .FechaIng = O.opn_cls.opn_fec, _
                                                   .PorAnt = O.ope_por_ant, _
                                                   .CanDeu = O.dsi_cls.Count, _
                                                   .CanDoc = O.opn_cls.opn_can_doc, _
                                                   .TipDoc = O.opn_cls.P_0031_cls.pnu_des, _
                                                   .Moneda = O.opn_cls.P_0023_cls.pnu_des, _
                                                .id_p_0023 = O.opn_cls.id_P_0023, _
                                                   .Estado = O.P_0030_cls.pnu_des, _
                                                 .Sucursal = O.opn_cls.id_suc, _
                                                   .Spread = O.opn_cls.opn_spr_ead, _
                                                 .Comision = O.opn_cls.opn_por_com, _
                                                 .Sobregiro = 0.0, _
                                                    .Deuda = 0.0}).Skip(sesion.NroPaginacion)



            '---------------------------------------------------------------------------------------------------------------
            'Recorro todas las operaciones que estan para ser aprobadas
            '---------------------------------------------------------------------------------------------------------------
            For Each O In OperacionesIngresadas
                If Coll_Operaciones.Count = 5 Then
                    Exit For
                End If
                '---------------------------------------------------------------------------------------------------------------
                'Rescato el valor de resumen de linea ocupada vs linea aprobada
                '---------------------------------------------------------------------------------------------------------------
                Dim Mto_Ocupado As Double
                Dim Mto_Aprobado As Double

                Try

                    Mto_Ocupado = (From R In Data.rsc_cls Where R.cli_idc = Format(O.Rut, Var.FMT_RUT) Select R.rsc_mto_ocu).First
                    Mto_Aprobado = (From L In Data.ldc_cls Where L.cli_idc = Format(O.Rut, Var.FMT_RUT) And L.id_P_0029 = 1 Select L.ldc_mto_apb).First

                Catch ex As Exception

                Finally

                    'Asigno porcentaje de deuda a la variables
                    Dim Monto As Double

                    Monto = O.MtoOpe * O.Factor

                    If Mto_Aprobado = 0 Then

                        O.Deuda = 0
                        O.Sobregiro = 0

                    Else

                        'porcentaje de deuda
                        O.Deuda = Format(((Mto_Ocupado + Monto) * 100) / Mto_Aprobado, Fmt.FSMCD)

                        'porcentaje de Sobregiro de Linea
                        O.Sobregiro = Format(((Mto_Aprobado - (Mto_Ocupado + Monto)) * 100) / Mto_Aprobado, Fmt.FSMCD)

                    End If


                End Try


                '---------------------------------------------------------------------------------------------------------------
                'Traigo las aprobaciones de la negociacion
                '---------------------------------------------------------------------------------------------------------------
                Dim Aprobaciones = From A In Data.apb_cls Where A.nnc_cls.id_opn = O.NroNeg And A.frm_cls.frm_est = "A" Order By A.frm_cls.id_p_005


                Dim Firmas_Ant As apb_cls
                Dim Vueltas As Integer = 0

                '---------------------------------------------------------------------------------------------------------------
                'Debe traer solo la operaciones que tiene la firma del usuario anterior o que sean los primeros
                '---------------------------------------------------------------------------------------------------------------
                'Traemos todos los perfiles del ejecutivo
                Dim perfiles = From n In Data.nef_cls Join e In Data.eje_cls On n.id_eje Equals e.id_eje Where e.eje_des_cra = Usr Select n.id_P0045
                Dim NroOperacion As Integer = 0

                For Each A In Aprobaciones '.Take(5)

                    'si es la primera firma del nivel
                    If Vueltas = 0 Then
                        'Validamos que el ejecutivo este asignado a firmar

                        If A.frm_cls.id_p_0045 = Perfil And NroOperacion <> O.NroOpe Then
                            Coll_Operaciones.Add(O)
                            NroOperacion = O.NroOpe
                            Exit For
                        Else
                            For Each p In perfiles
                                If A.frm_cls.id_p_0045 = p And NroOperacion <> O.NroOpe Then
                                    Coll_Operaciones.Add(O)
                                    NroOperacion = O.NroOpe
                                    Exit For
                                End If
                            Next
                        End If

                    Else

                        '---------------------------------------------------------------------------------------------------------------
                        'Si no es el mismo nivel al anterior, vuelve a 0 vueltas
                        '---------------------------------------------------------------------------------------------------------------
                        If A.frm_cls.id_p_005 <> Firmas_Ant.frm_cls.id_p_005 Then
                            Vueltas = 0
                        End If

                        If Vueltas = 0 Then

                            '---------------------------------------------------------------------------------------------------------------
                            'si es la primera firma del nivel
                            '---------------------------------------------------------------------------------------------------------------


                            If A.frm_cls.id_p_0045 = Perfil And NroOperacion = O.NroOpe Then
                                Coll_Operaciones.Add(O)
                                NroOperacion = O.NroOpe
                                Exit For
                            Else
                                For Each p In perfiles
                                    If A.frm_cls.id_p_0045 = p And NroOperacion = O.NroOpe Then
                                        Coll_Operaciones.Add(O)
                                        NroOperacion = O.NroOpe
                                        Exit For
                                    End If
                                Next
                            End If

                        Else

                            '---------------------------------------------------------------------------------------------------------------
                            'Sino es la primera vuelta, valida el perfil solo si la anterior esta Aprobada (1) y que pertenezca al mismo nivel
                            '---------------------------------------------------------------------------------------------------------------
                            Dim Estado As Char = If(IsNothing(Firmas_Ant.apb_est_ado), "2", "1")


                            If A.frm_cls.id_p_0045 = Perfil And Estado = "1" And A.frm_cls.id_p_005 = Firmas_Ant.frm_cls.id_p_005 Then
                                Coll_Operaciones.Add(O)
                                Exit For
                            Else
                                For Each p In perfiles
                                    If A.frm_cls.id_p_0045 = p And Estado = "1" And A.frm_cls.id_p_005 = Firmas_Ant.frm_cls.id_p_005 Then
                                        Coll_Operaciones.Add(O)
                                        Exit For
                                    End If
                                Next
                            End If

                        End If

                    End If

                    ' Vueltas += 1
                    Firmas_Ant = A


                Next

            Next


            GV.DataSource = Coll_Operaciones
            GV.DataBind()




            If Coll_Operaciones.Count > 0 Then

                Dim I As Integer = 0

                For Each Ope In Coll_Operaciones

                    GV.Rows(I).Cells(1).Text = RG.FormatoMiles(Ope.Rut) & "-" & RG.Vrut(Ope.Rut)

                    Dim Formato As String

                    Select Case Ope.id_p_0023
                        Case 1 : Formato = Fmt.FCMSD
                        Case 2 : Formato = Fmt.FCMCD4
                        Case 3, 4 : Formato = Fmt.FCMCD
                    End Select

                    GV.Rows(I).Cells(7).Text = Format(Ope.MtoOpe, Formato)

                    I = I + 1

                Next

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function OperacionesTodasDevuelve(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                                                                    ByVal CodigoEje_Desde As Integer, ByVal CodigoEje_Hasta As Integer, _
                                                                                    ByVal EstadoOpe_Desde As Integer, ByVal EstadoOpe_Hasta As Integer, _
                                                                                    ByVal Fecha_Desde As DateTime, ByVal Fecha_Hasta As DateTime, _
                                                                                    ByVal TipoOpe_Desde As Integer, ByVal TipoOpe_Hasta As Integer, _
                                                                                    ByVal TipoDoc_Desde As Integer, ByVal TipoDoc_Hasta As Integer, _
                                                                                    ByVal NroOpe_Desde As Integer, ByVal NroOpe_Hasta As Integer, _
                                                                                     ByVal CantPaginacion As Integer) As Collection


        '**************************************************************************************************************************************************
        'Descripcion: devuelve todas las operaciones ingresadas a traves de una negociacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/09/2008
        'Quien Modifica              Fecha              Descripcion
        'JLAGOS                     01-06-2010          Se agrega hora a los rango de fecha para la busqueda
        'A Saldivar                 10/01/2011          Se agrega paginacion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim FD, FH As String

            FD = Fecha_Desde.ToString
            FH = Format(Fecha_Hasta, "dd/MM/yyyy") & " 23:59:59"

            Dim OperacionesIngresadas = (From O In Data.ope_cls Where (CLng(O.opn_cls.eva_cls.cli_idc) >= RutCliente_Desde And CLng(O.opn_cls.eva_cls.cli_idc) <= RutCliente_Hasta) _
                                                                   And (O.opn_cls.eva_cls.id_eje >= CodigoEje_Desde And O.opn_cls.eva_cls.id_eje <= CodigoEje_Hasta) _
                                                                   And (O.id_P_0030 >= EstadoOpe_Desde And O.id_P_0030 <= EstadoOpe_Hasta) _
                                                                   And (O.ope_fec_sim >= FD And O.ope_fec_sim <= FH) _
                                                                   And (O.opn_cls.id_P_0012 >= TipoOpe_Desde And O.opn_cls.id_P_0012 <= TipoOpe_Hasta) _
                                                                   And (O.opn_cls.id_P_0031 >= TipoDoc_Desde And O.opn_cls.id_P_0031 <= TipoDoc_Hasta) _
                                                                   And (O.opn_cls.id_opn >= NroOpe_Desde And O.opn_cls.id_opn <= NroOpe_Hasta) _
                                                                   And (O.opn_cls.id_P_0082 >= 2 And O.opn_cls.id_P_0082 <= 3) _
                                                                   And O.id_opn > 0 Order By O.id_ope Descending _
                                              Select NroOpe = O.id_ope, _
                                                     NroNeg = O.id_opn, _
                                                        Rut = CLng(O.opn_cls.eva_cls.cli_idc), _
                                                     Digito = O.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
                                                    Cliente = If(O.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                                 O.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper & " " & O.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim.ToUpper & " " & O.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim.ToUpper, _
                                                                 O.opn_cls.eva_cls.cli_cls.cli_rso.Trim.ToUpper), _
                                                   FechaNeg = O.opn_cls.opn_fec_neg.Value.ToShortDateString, _
                                                   FechaSim = O.ope_fec_sim.Value.ToShortDateString, _
                                                     MtoOpe = O.opn_cls.opn_mto_doc, _
                                                   FechaIng = O.opn_cls.opn_fec, _
                                                     PorAnt = O.ope_por_ant, _
                                                     CanDeu = O.dsi_cls.Count, _
                                                     CanDoc = O.opn_cls.opn_can_doc, _
                                                     TipDoc = O.opn_cls.P_0031_cls.pnu_des, _
                                                     Moneda = O.opn_cls.P_0023_cls.pnu_des, _
                                                     O.opn_cls.id_P_0023, _
                                                     Estado = O.P_0030_cls.pnu_des, _
                                                     TipoOperacion = O.opn_cls.P_0012_cls.pnu_des).Skip(sesion.NroPaginacion)

            For Each O In OperacionesIngresadas.Take(CantPaginacion)
                coll.Add(O)
            Next

            Return coll

        Catch ex As Exception

        End Try

    End Function


    Public Function OperacionUltimaDevuelve(ByVal RutCliente As Long) As opo_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la ultima operacion de un cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            'SELECT @fecult = ISNULL(MAX(opo_fec_sim),"99991231") FROM opo WHERE cli_idc = @rutcli

            Dim UltimaOpo As opo_cls = (From O In Data.opo_cls Where O.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) Order By O.ope_cls.ope_fec_sim Descending).First

            Return UltimaOpo

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function OperacionDevuelve(ByVal RutCliente As Long, ByVal NroOperacion As Integer) As ope_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve una operacion de un cliente por su id
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Operacion As ope_cls = (From O In Data.ope_cls Where O.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And O.id_ope = NroOperacion).First

            Return Operacion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function OperacionUltimosAnosDevuelve(ByVal Ano_Desde As Integer, ByVal Ano_Hasta As Integer, ByVal RutCliente As Long) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: devuelve operaciones cursadas entre rango de años 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 09/09/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Operaciones = From O In Data.opo_cls Where O.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                                       And O.opo_fec_oto.Value.Year >= Ano_Desde _
                                                       And O.opo_fec_oto.Value.Year <= Ano_Hasta


            Dim Coll As New Collection

            For Each Opo In Operaciones
                Coll.Add(Opo)
            Next

            Return Coll


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GastosDeOperacionDevuelve(ByVal NroOperacion As Integer) As Object


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve una los gastos asociados a una operacion 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 29/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Gastos = From G In Data.gos_cls Where G.id_ope = NroOperacion

            If Gastos.Count > 0 Then
                Return Gastos
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function PagareDeOperacionDevuelve(ByVal NroOperacion As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los pagare asociados a una operacion 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 23/10/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Coll_Pagares As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Pagares = From P In Data.pgr_cls Where P.id_ope = NroOperacion


            For Each Pag In Pagares
                Coll_Pagares.Add(Pag)
            Next

            Return Coll_Pagares

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosSimulados_RetornaDoctos(ByVal rut_cliente1 As Long, ByVal rut_cliente2 As Long, _
                                                      ByVal rut_deudor1 As Long, ByVal rut_deudor2 As Long, _
                                                      ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                                      ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                                      ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                      ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documento simulados  
        'Creado por Jorge Lagos
        'Fecha Creacion: 03/11/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     26-08-2010          se agrega distinto de 13 (anulado)
        '**************************************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_dsi = From dsi In Data.dsi_cls _
            Where (dsi.ope_cls.opn_cls.eva_cls.cli_idc >= Format(rut_cliente1, Var.FMT_RUT) _
               And dsi.ope_cls.opn_cls.eva_cls.cli_idc <= Format(rut_cliente2, Var.FMT_RUT)) _
               And (dsi.deu_ide >= Format(rut_deudor1, Var.FMT_RUT) And dsi.deu_ide <= Format(rut_deudor2, Var.FMT_RUT)) _
               And (dsi.id_ope >= nro_operacion1 And dsi.id_ope <= nro_operacion2) _
               And (dsi.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 _
               And dsi.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) _
               And (dsi.dsi_num >= nro_docto1 And dsi.dsi_num <= nro_docto2) _
               And (dsi.dsi_flj_num >= nro_cuota1 And dsi.dsi_flj_num <= nro_cuota2) _
               And dsi.id_P_0011 <> 5 _
               And dsi.id_P_0011 <> 13 _
               And dsi.dsi_flj = "N"


            Dim Coll As New Collection

            For Each D In Temporal_dsi
                Coll.Add(D)
            Next

            Return Coll



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function VALIDA_DATOS_LINEA_Y_PUNTUAL(ByVal cliente As String, ByVal id_ope As Integer) As Boolean

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim PTL As String
            Dim Ope = From O In Data.ope_cls Where O.opn_cls.eva_cls.cli_idc = cliente _
                                                    And O.id_ope = id_ope _
                                                    Select O.ope_ptl, O.id_ldc


            For Each P In Ope
                If IsNothing(P.ope_ptl) Then
                    PTL = "N"
                Else
                    PTL = P.ope_ptl
                End If

                If P.id_ldc = 0 And PTL = "N" Then
                    Return False

                Else
                    Return True
                End If

            Next








        Catch ex As Exception

        End Try



    End Function

    Public Function Documentos_cuota_valida(ByVal dsinum As String, ByVal Cliente As String, ByVal TIPO As String, ByVal id_Docto As Integer) As Integer
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Cuota
        'Creado por 
        'Fecha Creacion: 
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                  11/08/2010        se agrega tipo de documento si es sin cuotas 
        'JLagos                       21/08/2012        se agrega que sean distintos de 6 y 13(eliminados y anulados)
        '**************************************************************************************************************************************************
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim num As Integer

        'Operación con Cuotas'ww
        If UCase(TIPO) = "S" Then

            Dim dsi_ = From d In Data.dsi_cls Where d.ope_cls.opn_cls.eva_cls.cli_idc = Cliente And _
                                                                            d.dsi_num = dsinum And _
                                                          d.ope_cls.opn_cls.id_P_0031 = id_Docto And _
                                                          d.id_P_0011 <> 6 And d.id_P_0011 <> 13 _
                       Order By d.dsi_flj_num Ascending Select d.dsi_flj_num

            For Each p In dsi_
                num = p
            Next

            num = num + 1

            Return num

        ElseIf UCase(TIPO) = "N" Then

            Dim ds = From d In Data.dsi_cls Where d.ope_cls.opn_cls.eva_cls.cli_idc = Cliente And _
                                                                          d.dsi_num = dsinum And _
                                                        d.ope_cls.opn_cls.id_P_0031 = id_Docto And _
                                                        d.id_P_0011 <> 6 And d.id_P_0011 <> 13

            If ds.Count > 0 Then
                Return 999
            Else
                Return 998
            End If

        End If







    End Function

    Public Function NumeroOtorgamientoDevuelve(ByVal RutCliente As Long) As Integer

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la cantidad de otorgamiento de un cliente
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 11/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            'SELECT @fecult = ISNULL(MAX(opo_fec_sim),"99991231") FROM opo WHERE cli_idc = @rutcli

            Dim Otorgamiento As Integer = (From O In Data.opo_cls Where O.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) Select O.opo_otg).Max

            Return Otorgamiento

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CXC_AbonoAnticipo_Devuelve(ByVal RutCliente As Long, ByVal NroAplicacion As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Cuentas por Anticipo de un Cliente
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 10/12/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  11/01/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim val As Double
            Dim sesion As New ClsSession.ClsSession
            Dim data As New CapaDatos.DataClsFactoringDataContext



            Dim Cuentas = From C In data.cxc_cls _
                          Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And (C.id_P_0041 = 8 Or C.id_P_0041 = 24) _
            Select C

            For Each p In Cuentas

                Dim sal_ing = (From ing_sec1 In data.ing_sec_cls Where ing_sec1.id_cxc = p.id_cxc _
                                                                 And ing_sec1.id_P_0053 = 1 _
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
                Dim Ctas = (From C In data.cxc_cls _
                              Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) And (C.id_P_0041 = 8 Or C.id_P_0041 = 24) And C.id_cxc = p.id_cxc _
                               And C.cxc_sal - VALOR > 0 _
                                              Select C.id_P_0041, _
                                                     C.p_0041_cls.pnu_des, _
                                                     C.id_cxc, _
                                                     C.cxc_des, _
                                                     C.cxc_sal, _
                                                     C.cxc_int, _
                                                     C.cxc_ful_pgo, _
                                                     C.id_doc, _
                                                     C.cxc_fec, _
                                                     C.p_0041_cls.pnu_atr_002, _
                                                     C.id_P_0023, _
                                                     C.cxc_fac_cam, _
                                                     C.cxc_mto).Skip(sesion.NroPaginacion)




                For Each x In Ctas.Take(12)


                    Coll.Add(x)

                Next

            Next


            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function Documentos_verificar_valida(ByVal dsinum As Integer, ByVal deu_ide As String) As dvf_cls

        'Descripcion: Valida que documento exista en verificación
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 12/06/2009
        'Quien Modifica              Fecha              Descripcion

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim dvf As dvf_cls = (From d In Data.dvf_cls _
                                  Where d.deu_ide = deu_ide And _
                                        d.dvf_num = dsinum _
                                  Select d).First
            Return dvf

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function Documentos_verificar_valida_DSI(ByVal dsi_num As Integer, ByVal deu_ide As String, ByVal tip_doc As Integer) As Boolean
        '-------------------------------------------------------------------------------------------------------------
        'Descripcion: Valida que documento este en estado ingresado (sino no lo puede verificar)
        'Creado por= Jorge Lagos
        'Fecha Creacion: 16/06/2012
        'Quien Modifica              Fecha              Descripcion
        '-------------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim dsi_ As dsi_cls = (From d In Data.dsi_cls _
                                   Where d.deu_ide = Format(CLng(deu_ide), Var.FMT_RUT) And _
                                         d.dsi_num = dsi_num And _
                                         d.ope_cls.opn_cls.id_P_0031 = tip_doc _
                                   Select d).First

            _EstadoOperacion = dsi_.ope_cls.P_0030_cls.pnu_des

            If dsi_.ope_cls.id_P_0030 = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            'No existe documento
            Return True
        End Try

    End Function

    Public Function Operaciones_varios_criterios_Devuelve(ByVal RutCliente1 As Long, ByVal RutCliente2 As Long, _
                                                              ByVal fechaing As Date, ByVal fechaing2 As Date, _
                                                              ByVal fecha_venc As Date, ByVal fecha_venc2 As Date, _
                                                              ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                              ByVal nrootg As Integer, ByVal nrootg2 As Int64, _
                                                              ByVal tipo As Integer, _
                                                              ByVal nrodoc As String, ByVal nrodoc2 As String, _
                                                              ByVal moneda1 As Integer, ByVal moneda2 As Integer, _
                                                              ByVal resp As String, ByVal resp2 As String, _
                                                              ByVal rutdeu1 As Integer, ByVal rutdeu2 As Int64, _
                                                              ByVal eje1 As Integer, ByVal eje2 As Integer, _
                                                              Optional ByVal LlenaGrid As Boolean = False, _
                                                              Optional ByVal GV As GridView = Nothing) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las operaciones Segun multiples Criterios
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 30/11/2008                                                                                                                     
        'Quien Modifica         Fecha              Descripcion                 
        'A Saldivar             11/11/2010          No trae datos si se consulta por otro deudor, o numero de documento      
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim operacion As New ope_cls
            Dim col As New Collection

            Data.ObjectTrackingEnabled = False

            If tipo = 1 Then
                '**********************************************************************************************************************************
                'Ingresadas con y sin cuadrar
                '**********************************************************************************************************************************
                'Se modifica tipo de moneda id_P_0023 por id_P_0023_fla

                'Dim OpeAnt = (From Ope In Data.ope_cls _
                '               Join Dsi In Data.dsi_cls _
                '              On Ope.id_ope Equals Dsi.id_ope _
                '              Where Ope.opn_cls.eva_cls.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) _
                '             And Ope.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT) _
                '             And Ope.opn_cls.opn_fec_neg >= Format(CDate(fechaing), "yyyy/MM/dd" & " 00:00:00") And Ope.opn_cls.opn_fec_neg <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59") _
                '             And Ope.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd" & " 00:00:00") And Ope.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59") _
                '             And Ope.opn_cls.id_P_0023 >= moneda1 And Ope.opn_cls.id_P_0023 <= moneda2 _
                '             And Ope.id_P_0030 = 6 And Ope.opn_cls.id_P_0082 = 5 _
                '             And (Dsi.dsi_num >= nrodoc And Dsi.dsi_num <= nrodoc2) _
                '             And ((Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) Or _
                '                       ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                '             And (Dsi.deu_ide >= Format(CLng(rutdeu1), Var.FMT_RUT) And Dsi.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                '             And (Ope.opn_cls.opn_res_son >= resp And Ope.opn_cls.opn_res_son <= resp2) _
                '             And (Ope.id_opn >= nrootg And Ope.id_opn <= nrootg2) _
                '     Select id_ope = Ope.id_ope Distinct).Skip(page_anu).Take(12)

                Dim OpeAnt = (From Ope In Data.ope_cls _
                             Join Dsi In Data.dsi_cls _
                             On Ope.id_ope Equals Dsi.id_ope _
                            Where (Ope.opn_cls.eva_cls.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) And Ope.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT)) _
                              And (CDate(Ope.opn_cls.opn_fec_neg) >= Format(CDate(fechaing), "yyyy/MM/dd") And CDate(Ope.opn_cls.opn_fec_neg) <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59")) _
                              And (Ope.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd") And Ope.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59")) _
                              And (Ope.opn_cls.id_P_0023_fla >= moneda1 And Ope.opn_cls.id_P_0023_fla <= moneda2) _
                              And Ope.id_P_0030 = 1 _
                              And (Dsi.dsi_num >= nrodoc And Dsi.dsi_num <= nrodoc2) _
                              And ((Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) And _
                                 ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                              And (Dsi.deu_ide >= Format(CLng(rutdeu1), Var.FMT_RUT) And Dsi.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                              And (Ope.opn_cls.opn_res_son >= resp And Ope.opn_cls.opn_res_son <= resp2) _
                              And (Ope.id_opn >= nrootg And Ope.id_opn <= nrootg2) _
                    Select id_ope = Ope.id_ope Distinct).Skip(page_dig).Take(12)

                'Dim OpeAnt = (From Ope In Data.ope_cls _
                '              Join Dsi In Data.dsi_cls _
                '              On Ope.id_ope Equals Dsi.id_ope _
                '             Where (Ope.opn_cls.eva_cls.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) And Ope.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT)) _
                '               And (CDate(Ope.opn_cls.opn_fec_neg) >= Format(CDate(fechaing), "yyyy/MM/dd") And CDate(Ope.opn_cls.opn_fec_neg) <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59")) _
                '               And (Ope.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd") And Ope.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59")) _
                '               And (Ope.opn_cls.id_P_0023 >= moneda1 And Ope.opn_cls.id_P_0023 <= moneda2) _
                '               And Ope.id_P_0030 = 1 _
                '               And (Dsi.dsi_num >= nrodoc And Dsi.dsi_num <= nrodoc2) _
                '               And ((Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) Or _
                '                  ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                '               And (Dsi.deu_ide >= Format(CLng(rutdeu1), Var.FMT_RUT) And Dsi.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                '               And (Ope.opn_cls.opn_res_son >= resp And Ope.opn_cls.opn_res_son <= resp2) _
                '               And (Ope.id_opn >= nrootg And Ope.id_opn <= nrootg2) _
                '     Select id_ope = Ope.id_ope Distinct).Skip(page_dig).Take(12)

                For Each p In OpeAnt

                    Dim rst = From ope In Data.ope_cls Where ope.id_ope = p _
                    Select New With { _
                     ope.id_ope, _
                     ope.id_ldc, _
                     ope.id_fct, _
                     .MONEDA = ope.opn_cls.P_0023_cls.pnu_des, _
                     .tipdoc = ope.opn_cls.P_0031_cls.pnu_des, _
                     .estope = ope.P_0030_cls.pnu_des, _
                     .Cliente = If(ope.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                  ope.opn_cls.eva_cls.cli_cls.cli_rso & " " & ope.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & ope.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                                  ope.opn_cls.eva_cls.cli_cls.cli_rso), _
                     ope.opn_cls.id_P_0023_com, _
                     ope.opn_cls.id_P_0023_fla, _
                     ope.id_P_0030, _
                     ope.id_P_0070, _
                     ope.id_P_0077, _
                     ope.id_P_0083, _
                     ope.id_P_0104, _
                     ope.ope_cdo, _
                     ope.ope_cnt, _
                     ope.opn_cls.id_suc, _
                     ope.opn_cls.opn_com_fla, _
                     ope.ope_com_fog, _
                     ope.opn_cls.opn_com_max, _
                     ope.opn_cls.opn_com_min, _
                     ope.opn_cls.opn_por_com, _
                     ope.ope_com_tot, _
                     ope.ope_com_tot_pes, _
                     ope.ope_con_cje, _
                     ope.ope_cuo, _
                     ope.ope_dif_pre, _
                     ope.ope_fac_cam, _
                     ope.ope_fac_cam_pag, _
                     ope.ope_fec_anl, _
                     ope.ope_fec_ctb, _
                     ope.ope_obs_sim, _
                     ope.ope_fec_sim, _
                     ope.ope_fev, _
                     ope.ope_fog_gar, _
                     ope.ope_fog_mto_lin, _
                     ope.ope_fog_son, _
                     ope.ope_fog_ven, _
                     ope.opn_cls.id_P_0023, _
                     ope.opn_cls.id_P_0031, _
                     ope.opn_cls.id_P_0056, _
                     ope.opn_cls.id_P_0082, _
                     ope.opn_cls.opn_fec_neg, _
                     ope.opn_cls.opn_fev, _
                     ope.opn_cls.opn_can_doc, _
                     ope.opn_cls.id_P_0012, _
                     .ope_ptl = IIf(ope.ope_ptl = Nothing, "NO", IIf(ope.ope_ptl = "N", "NO", "SI")), _
                     .ope_lnl = IIf(ope.ope_lnl = Nothing, "NO", IIf(ope.ope_lnl = "N", "NO", "SI")), _
                     .ope_res_son = IIf(ope.ope_res_son = Nothing, "NO", IIf(ope.opn_cls.opn_res_son = "0", "NO", "SI")), _
                     ope.opn_cls.opn_spr_ead, _
                     ope.opn_cls.opn_tas_bas, _
                     ope.opn_cls.eva_cls.cli_idc, _
                     ope.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                     ope.opn_cls.eva_cls.cli_cls.cli_ape_ptn, _
                     ope.opn_cls.eva_cls.cli_cls.cli_rso, _
                     ope.ope_mto_ant, _
                     ope.ope_imp_ope, _
                     ope.ope_por_ant, _
                     ope.ope_int_dev, _
                     ope.ope_iva_com, _
                     ope.ope_mon_gas, _
                     ope.ope_sal_pag, _
                     ope.ope_sal_pen, _
                     ope.opn_cls.opn_pto_spr, _
                     ope.opn_cls.opn_mto_doc, _
                     ope.P_0104_cls.pnu_des, _
                     .TipoOperacion = ope.opn_cls.P_0012_cls.pnu_des, _
                     ope.id_opn}

                    For Each d In rst
                        col.Add(d)
                    Next

                Next

                If LlenaGrid Then
                    GV.DataSource = OpeAnt
                    GV.DataBind()

                    Return col
                Else
                    Return col
                End If


            ElseIf tipo = 2 Then
                '**********************************************************************************************************************************
                'SIMULADAS
                '**********************************************************************************************************************************
                Dim OpeAnt = (From Ope In Data.ope_cls _
                               Join Dsi In Data.dsi_cls _
                              On Ope.id_ope Equals Dsi.id_ope _
                              Where Ope.opn_cls.eva_cls.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) _
                             And Ope.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT) _
                             And Ope.opn_cls.opn_fec_neg >= Format(CDate(fechaing), "yyyy/MM/dd") And Ope.opn_cls.opn_fec_neg <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59") _
                             And Ope.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd") And Ope.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59") _
                            And Ope.opn_cls.id_P_0023_fla >= moneda1 And Ope.opn_cls.id_P_0023_fla <= moneda2 _
                             And Ope.id_P_0030 = 2 And Ope.opn_cls.id_P_0082 = 3 _
                               And (Dsi.dsi_num >= nrodoc And Dsi.dsi_num <= nrodoc2) _
                               And ((Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) And _
                                    ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                             And (Dsi.deu_ide >= Format(CLng(rutdeu1), Var.FMT_RUT) And Dsi.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                             And (Ope.opn_cls.opn_res_son >= resp And Ope.opn_cls.opn_res_son <= resp2) _
                             And (Ope.id_opn >= nrootg And Ope.id_opn <= nrootg2) _
                     Select id_ope = Ope.id_ope Distinct).Skip(page_sim).Take(12)

                For Each p In OpeAnt

                    Dim rst = From ope In Data.ope_cls Where ope.id_ope = p _
                     Select New With {ope.id_ope, _
                     ope.id_ldc, _
                     ope.id_fct, _
                     .MONEDA = ope.opn_cls.P_0023_cls.pnu_des, _
                     .tipdoc = ope.opn_cls.P_0031_cls.pnu_des, _
                     .estope = ope.P_0030_cls.pnu_des, _
                     .Cliente = If(ope.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                              ope.opn_cls.eva_cls.cli_cls.cli_rso & " " & ope.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & ope.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                              ope.opn_cls.eva_cls.cli_cls.cli_rso), _
                     ope.opn_cls.id_P_0023_com, _
                     ope.opn_cls.id_P_0023_fla, _
                     ope.id_P_0030, _
                     ope.id_P_0070, _
                     ope.id_P_0077, _
                     ope.id_P_0083, _
                     ope.id_P_0104, _
                     ope.ope_cdo, _
                     ope.ope_cnt, _
                     ope.opn_cls.id_suc, _
                     ope.opn_cls.opn_com_fla, _
                     ope.ope_com_fog, _
                     ope.opn_cls.opn_com_max, _
                     ope.opn_cls.opn_com_min, _
                     ope.opn_cls.opn_por_com, _
                     ope.ope_com_tot, _
                     ope.ope_com_tot_pes, _
                     ope.ope_con_cje, _
                     ope.ope_cuo, _
                     ope.ope_dif_pre, _
                     ope.ope_fac_cam, _
                     ope.ope_fac_cam_pag, _
                     ope.ope_fec_anl, _
                     ope.ope_fec_ctb, _
                     ope.ope_obs_sim, _
                     ope.ope_fec_sim, _
                     ope.ope_fev, _
                     ope.ope_fog_gar, _
                     ope.ope_fog_mto_lin, _
                     ope.ope_fog_son, _
                     ope.ope_fog_ven, _
                     ope.opn_cls.id_P_0023, _
                     ope.opn_cls.id_P_0031, _
                     ope.opn_cls.id_P_0056, _
                     ope.opn_cls.id_P_0082, _
                     ope.opn_cls.opn_fec_neg, _
                     ope.opn_cls.opn_fev, _
                     ope.opn_cls.opn_can_doc, _
                     ope.opn_cls.id_P_0012, _
                     .ope_ptl = IIf(ope.ope_ptl = Nothing, "NO", IIf(ope.ope_ptl = "N", "NO", "SI")), _
                     .ope_lnl = IIf(ope.ope_lnl = Nothing, "NO", IIf(ope.ope_lnl = "N", "NO", "SI")), _
                     .ope_res_son = IIf(ope.ope_res_son = Nothing, "NO", IIf(ope.opn_cls.opn_res_son = "0", "NO", "SI")), _
                     ope.opn_cls.opn_spr_ead, _
                     ope.opn_cls.opn_tas_bas, _
                     ope.opn_cls.eva_cls.cli_idc, _
                     ope.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                     ope.opn_cls.eva_cls.cli_cls.cli_ape_ptn, _
                     ope.opn_cls.eva_cls.cli_cls.cli_rso, _
                     ope.ope_mto_ant, _
                     ope.ope_imp_ope, _
                     ope.ope_por_ant, _
                     ope.ope_int_dev, _
                     ope.ope_iva_com, _
                     ope.ope_mon_gas, _
                     ope.ope_sal_pag, _
                     ope.ope_sal_pen, _
                     ope.opn_cls.opn_pto_spr, _
                     ope.opn_cls.opn_mto_doc, _
                     ope.P_0104_cls.pnu_des, _
                     .TipoOperacion = ope.opn_cls.P_0012_cls.pnu_des, _
                     ope.id_opn}

                    For Each d In rst
                        col.Add(d)
                    Next

                Next

                If LlenaGrid Then
                    GV.DataSource = OpeAnt
                    GV.DataBind()

                    Return col
                Else
                    Return col
                End If



            ElseIf tipo = 3 Then

                '**********************************************************************************************************************************
                'Otorgadas
                '**********************************************************************************************************************************

                Dim OpeAnt = (From Opo In Data.opo_cls _
                              Join Doc In Data.doc_cls On Opo.id_opo Equals Doc.id_opo _
                              Where Opo.ope_cls.opn_cls.eva_cls.cli_idc >= Format(RutCliente1, Var.FMT_RUT) _
                             And Opo.ope_cls.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT) _
                             And Opo.ope_cls.opn_cls.opn_fec_neg >= Format(CDate(fechaing), "yyyy/MM/dd") And Opo.ope_cls.opn_cls.opn_fec_neg <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59") _
                             And Opo.ope_cls.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd") And Opo.ope_cls.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59") _
                             And Opo.ope_cls.opn_cls.id_P_0023_fla >= moneda1 And Opo.ope_cls.opn_cls.id_P_0023_fla <= moneda2 _
                             And Opo.ope_cls.id_P_0030 = 3 And Opo.ope_cls.opn_cls.id_P_0082 = 4 _
                             And (Opo.ope_cls.id_opn >= nrootg And Opo.ope_cls.id_opn <= nrootg2) _
                             And (Doc.dsi_cls.dsi_num >= nrodoc And Doc.dsi_cls.dsi_num <= nrodoc2) _
                             And ((Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) And _
                                  ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                             And (Doc.dsi_cls.deu_ide >= Format(rutdeu1, Var.FMT_RUT) And Doc.dsi_cls.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                             And (Opo.ope_cls.opn_cls.opn_res_son >= resp And Opo.ope_cls.opn_cls.opn_res_son <= resp2) _
                            Select id_opo = Opo.id_opo Distinct).Skip(page_otg).Take(12)


                For Each p In OpeAnt


                    Dim rst = From Opo In Data.opo_cls Where Opo.id_opo = p _
                     Select New With {Opo.ope_cls.id_ope, _
                     Opo.ope_cls.id_ldc, _
                     Opo.ope_cls.id_fct, _
                     .MONEDA = Opo.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                     .tipdoc = Opo.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                     .estope = Opo.ope_cls.P_0030_cls.pnu_des, _
                     .Cliente = If(Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                  Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                                  Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
                     Opo.ope_cls.opn_cls.id_P_0023_com, _
                     Opo.ope_cls.opn_cls.id_P_0023_fla, _
                     Opo.ope_cls.id_P_0030, _
                     Opo.ope_cls.id_P_0070, _
                     Opo.ope_cls.id_P_0077, _
                     Opo.ope_cls.id_P_0083, _
                     Opo.ope_cls.id_P_0104, _
                     Opo.ope_cls.ope_cdo, _
                     Opo.ope_cls.ope_cnt, _
                     Opo.ope_cls.opn_cls.id_suc, _
                     Opo.ope_cls.opn_cls.opn_com_fla, _
                     Opo.ope_cls.ope_com_fog, _
                     Opo.ope_cls.opn_cls.opn_com_max, _
                     Opo.ope_cls.opn_cls.opn_com_min, _
                     Opo.ope_cls.opn_cls.opn_por_com, _
                     Opo.ope_cls.ope_com_tot, _
                     Opo.ope_cls.ope_com_tot_pes, _
                     Opo.ope_cls.ope_con_cje, _
                     Opo.ope_cls.ope_cuo, _
                     Opo.ope_cls.ope_dif_pre, _
                     Opo.ope_cls.ope_fac_cam, _
                     Opo.ope_cls.ope_fac_cam_pag, _
                     Opo.ope_cls.ope_fec_anl, _
                     Opo.ope_cls.ope_fec_ctb, _
                     Opo.ope_cls.ope_obs_sim, _
                     Opo.ope_cls.ope_fec_sim, _
                     Opo.ope_cls.ope_fev, _
                     Opo.ope_cls.id_opn, _
                     Opo.ope_cls.opn_cls.id_eva, _
                     Opo.ope_cls.ope_fog_gar, _
                     Opo.ope_cls.ope_fog_mto_lin, _
                     Opo.ope_cls.ope_fog_son, _
                     Opo.ope_cls.ope_fog_ven, _
                     Opo.ope_cls.opn_cls.id_P_0023, _
                     Opo.ope_cls.opn_cls.id_P_0031, _
                     Opo.ope_cls.opn_cls.id_P_0056, _
                     Opo.ope_cls.opn_cls.id_P_0082, _
                     Opo.ope_cls.opn_cls.opn_fec_neg, _
                     Opo.ope_cls.opn_cls.opn_fev, _
                     Opo.ope_cls.opn_cls.opn_can_doc, _
                     Opo.ope_cls.opn_cls.id_P_0012, _
                     Opo.id_opo, _
                     .ope_ptl = IIf(Opo.ope_cls.ope_ptl = Nothing, "NO", IIf(Opo.ope_cls.ope_ptl = "N", "NO", "SI")), _
                     .ope_lnl = IIf(Opo.ope_cls.ope_lnl = Nothing, "NO", IIf(Opo.ope_cls.ope_lnl = "N", "NO", "SI")), _
                     .ope_res_son = IIf(Opo.ope_cls.ope_res_son = Nothing, "NO", IIf(Opo.ope_cls.opn_cls.opn_res_son = "0", "NO", "SI")), _
                     Opo.ope_cls.opn_cls.opn_spr_ead, _
                     Opo.ope_cls.opn_cls.opn_tas_bas, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_idc, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso, _
                     Opo.ope_cls.ope_mto_ant, _
                     Opo.ope_cls.ope_imp_ope, _
                     Opo.ope_cls.ope_por_ant, _
                     Opo.ope_cls.ope_int_dev, _
                     Opo.ope_cls.ope_iva_com, _
                     Opo.ope_cls.ope_mon_gas, _
                     Opo.ope_cls.ope_sal_pag, _
                     Opo.ope_cls.ope_sal_pen, _
                     Opo.ope_cls.opn_cls.opn_pto_spr, _
                     Opo.ope_cls.opn_cls.opn_mto_doc, _
                     Opo.opo_otg, _
                     Opo.ope_cls.P_0104_cls.pnu_des, _
                     .TipoOperacion = Opo.ope_cls.opn_cls.P_0012_cls.pnu_des}

                    For Each c In rst
                        col.Add(c)
                    Next

                Next

                If LlenaGrid Then
                    GV.DataSource = OpeAnt
                    GV.DataBind()
                    Return col
                Else
                    Return col
                End If



            ElseIf tipo = 4 Then

                '**********************************************************************************************************************************
                'Anulada
                '**********************************************************************************************************************************
                Dim OpeAnt = (From Ope In Data.ope_cls _
                               Join Dsi In Data.dsi_cls _
                              On Ope.id_ope Equals Dsi.id_ope _
                              Where Ope.opn_cls.eva_cls.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) _
                             And Ope.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT) _
                             And Ope.opn_cls.opn_fec_neg >= Format(CDate(fechaing), "yyyy/MM/dd" & " 00:00:00") And Ope.opn_cls.opn_fec_neg <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59") _
                             And Ope.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd" & " 00:00:00") And Ope.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59") _
                             And Ope.opn_cls.id_P_0023_fla >= moneda1 And Ope.opn_cls.id_P_0023_fla <= moneda2 _
                             And Ope.id_P_0030 = 6 And Ope.opn_cls.id_P_0082 = 5 _
                             And (Dsi.dsi_num >= nrodoc And Dsi.dsi_num <= nrodoc2) _
                             And ((Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Ope.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) And _
                                       ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                             And (Dsi.deu_ide >= Format(CLng(rutdeu1), Var.FMT_RUT) And Dsi.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                             And (Ope.opn_cls.opn_res_son >= resp And Ope.opn_cls.opn_res_son <= resp2) _
                             And (Ope.id_opn >= nrootg And Ope.id_opn <= nrootg2) _
                     Select id_ope = Ope.id_ope Distinct).Skip(page_anu).Take(12)

                For Each ope In OpeAnt
                    Dim rst = From op In Data.ope_cls Where op.id_ope = ope _
                   Select New With {op.id_ope, _
                   op.id_ldc, _
                   op.id_fct, _
                   .MONEDA = op.opn_cls.P_0023_cls.pnu_des, _
                   .tipdoc = op.opn_cls.P_0031_cls.pnu_des, _
                   .estope = op.P_0030_cls.pnu_des, _
                   .Cliente = If(op.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                op.opn_cls.eva_cls.cli_cls.cli_rso & " " & op.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & op.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                                op.opn_cls.eva_cls.cli_cls.cli_rso), _
                   op.opn_cls.id_P_0023_com, _
                   op.opn_cls.id_P_0023_fla, _
                   op.id_P_0030, _
                   op.id_P_0070, _
                   op.id_P_0077, _
                   op.id_P_0083, _
                   op.id_P_0104, _
                   op.ope_cdo, _
                   op.ope_cnt, _
                   op.opn_cls.id_suc, _
                   op.opn_cls.opn_com_fla, _
                   op.ope_com_fog, _
                   op.opn_cls.opn_com_max, _
                   op.opn_cls.opn_com_min, _
                   op.opn_cls.opn_por_com, _
                   op.ope_com_tot, _
                   op.ope_com_tot_pes, _
                   op.ope_con_cje, _
                   op.ope_cuo, _
                   op.ope_dif_pre, _
                   op.ope_fac_cam, _
                   op.ope_fac_cam_pag, _
                   op.ope_fec_anl, _
                   op.ope_fec_ctb, _
                   op.ope_obs_sim, _
                   op.ope_fec_sim, _
                   op.ope_fev, _
                   op.ope_fog_gar, _
                   op.ope_fog_mto_lin, _
                   op.ope_fog_son, _
                   op.ope_fog_ven, _
                   op.opn_cls.id_P_0023, _
                   op.opn_cls.id_P_0031, _
                   op.opn_cls.id_P_0056, _
                   op.opn_cls.id_P_0082, _
                   op.opn_cls.opn_fec_neg, _
                   op.opn_cls.opn_fev, _
                   op.opn_cls.opn_can_doc, _
                   op.opn_cls.id_P_0012, _
                   .ope_ptl = IIf(op.ope_ptl = Nothing, "NO", IIf(op.ope_ptl = "N", "NO", "SI")), _
                   .ope_lnl = IIf(op.ope_lnl = Nothing, "NO", IIf(op.ope_lnl = "N", "NO", "SI")), _
                   .ope_res_son = IIf(op.ope_res_son = Nothing, "NO", IIf(op.opn_cls.opn_res_son = "0", "NO", "SI")), _
                   op.opn_cls.opn_spr_ead, _
                   op.opn_cls.opn_tas_bas, _
                   op.opn_cls.eva_cls.cli_idc, _
                   op.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                   op.opn_cls.eva_cls.cli_cls.cli_ape_ptn, _
                   op.opn_cls.eva_cls.cli_cls.cli_rso, _
                   op.ope_mto_ant, _
                   op.ope_imp_ope, _
                   op.ope_por_ant, _
                   op.ope_int_dev, _
                   op.ope_iva_com, _
                   op.ope_mon_gas, _
                   op.ope_sal_pag, _
                   op.ope_sal_pen, _
                   op.opn_cls.opn_pto_spr, _
                   op.opn_cls.opn_mto_doc, _
                   op.P_0104_cls.pnu_des, _
                   .TipoOperacion = op.opn_cls.P_0012_cls.pnu_des, _
                   op.id_opn}

                    For Each p In rst
                        col.Add(p)
                    Next

                Next

                If LlenaGrid Then
                    GV.DataSource = OpeAnt
                    GV.DataBind()

                    Return col
                Else
                    Return col
                End If

            ElseIf tipo = 5 Then
                '**********************************************************************************************************************************
                'PAGADA
                '**********************************************************************************************************************************
                Dim OpeAnt = (From Opo In Data.opo_cls _
                              Join Doc In Data.doc_cls On Opo.id_opo Equals Doc.id_opo _
                              Where Opo.ope_cls.opn_cls.eva_cls.cli_idc >= Format(RutCliente1, Var.FMT_RUT) _
                             And Opo.ope_cls.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT) _
                             And Opo.ope_cls.opn_cls.opn_fec_neg >= Format(CDate(fechaing), "yyyy/MM/dd") And Opo.ope_cls.opn_cls.opn_fec_neg <= Format(CDate(fechaing2), "yyyy/MM/dd" & " 23:59:59") _
                             And Opo.ope_cls.ope_fev >= Format(CDate(fecha_venc), "yyyy/MM/dd") And Opo.ope_cls.ope_fev <= Format(CDate(fecha_venc2), "yyyy/MM/dd" & " 23:59:59") _
                             And Opo.ope_cls.opn_cls.id_P_0023_fla >= moneda1 And Opo.ope_cls.opn_cls.id_P_0023_fla <= moneda2 _
                             And Opo.ope_cls.id_P_0030 = 5 _
                             And (Opo.ope_cls.id_opn >= nrootg And Opo.ope_cls.id_opn <= nrootg2) _
                             And (Doc.dsi_cls.dsi_num >= nrodoc And Doc.dsi_cls.dsi_num <= nrodoc2) _
                             And ((Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje >= eje1 And Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje <= eje2) And _
                                  ((From n In Data.nes_cls Where n.id_eje >= eje1 And n.id_eje <= eje2 Select n.id_suc).Count > 0)) _
                             And (Doc.dsi_cls.deu_ide >= Format(rutdeu1, Var.FMT_RUT) And Doc.dsi_cls.deu_ide <= Format(rutdeu2, Var.FMT_RUT)) _
                             And (Opo.ope_cls.opn_cls.opn_res_son >= resp And Opo.ope_cls.opn_cls.opn_res_son <= resp2) _
                            Select id_opo = Opo.id_opo Distinct).Skip(page_otg).Take(12)


                For Each p In OpeAnt


                    Dim rst = From Opo In Data.opo_cls Where Opo.id_opo = p _
                     Select New With {Opo.ope_cls.id_ope, _
                     Opo.ope_cls.id_ldc, _
                     Opo.ope_cls.id_fct, _
                     .MONEDA = Opo.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                     .tipdoc = Opo.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                     .estope = Opo.ope_cls.P_0030_cls.pnu_des, _
                     .Cliente = If(Opo.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                  Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                                  Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
                     Opo.ope_cls.opn_cls.id_P_0023_com, _
                     Opo.ope_cls.opn_cls.id_P_0023_fla, _
                     Opo.ope_cls.id_P_0030, _
                     Opo.ope_cls.id_P_0070, _
                     Opo.ope_cls.id_P_0077, _
                     Opo.ope_cls.id_P_0083, _
                     Opo.ope_cls.id_P_0104, _
                     Opo.ope_cls.ope_cdo, _
                     Opo.ope_cls.ope_cnt, _
                     Opo.ope_cls.opn_cls.id_suc, _
                     Opo.ope_cls.opn_cls.opn_com_fla, _
                     Opo.ope_cls.ope_com_fog, _
                     Opo.ope_cls.opn_cls.opn_com_max, _
                     Opo.ope_cls.opn_cls.opn_com_min, _
                     Opo.ope_cls.opn_cls.opn_por_com, _
                     Opo.ope_cls.ope_com_tot, _
                     Opo.ope_cls.ope_com_tot_pes, _
                     Opo.ope_cls.ope_con_cje, _
                     Opo.ope_cls.ope_cuo, _
                     Opo.ope_cls.ope_dif_pre, _
                     Opo.ope_cls.ope_fac_cam, _
                     Opo.ope_cls.ope_fac_cam_pag, _
                     Opo.ope_cls.ope_fec_anl, _
                     Opo.ope_cls.ope_fec_ctb, _
                     Opo.ope_cls.ope_obs_sim, _
                     Opo.ope_cls.ope_fec_sim, _
                     Opo.ope_cls.ope_fev, _
                     Opo.ope_cls.id_opn, _
                     Opo.ope_cls.opn_cls.id_eva, _
                     Opo.ope_cls.ope_fog_gar, _
                     Opo.ope_cls.ope_fog_mto_lin, _
                     Opo.ope_cls.ope_fog_son, _
                     Opo.ope_cls.ope_fog_ven, _
                     Opo.ope_cls.opn_cls.id_P_0023, _
                     Opo.ope_cls.opn_cls.id_P_0031, _
                     Opo.ope_cls.opn_cls.id_P_0056, _
                     Opo.ope_cls.opn_cls.id_P_0082, _
                     Opo.ope_cls.opn_cls.opn_fec_neg, _
                     Opo.ope_cls.opn_cls.opn_fev, _
                     Opo.ope_cls.opn_cls.opn_can_doc, _
                     Opo.ope_cls.opn_cls.id_P_0012, _
                     Opo.id_opo, _
                     .ope_ptl = IIf(Opo.ope_cls.ope_ptl = Nothing, "NO", IIf(Opo.ope_cls.ope_ptl = "N", "NO", "SI")), _
                     .ope_lnl = IIf(Opo.ope_cls.ope_lnl = Nothing, "NO", IIf(Opo.ope_cls.ope_lnl = "N", "NO", "SI")), _
                     .ope_res_son = IIf(Opo.ope_cls.ope_res_son = Nothing, "NO", IIf(Opo.ope_cls.opn_cls.opn_res_son = "0", "NO", "SI")), _
                     Opo.ope_cls.opn_cls.opn_spr_ead, _
                     Opo.ope_cls.opn_cls.opn_tas_bas, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_idc, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn, _
                     Opo.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso, _
                     Opo.ope_cls.ope_mto_ant, _
                     Opo.ope_cls.ope_imp_ope, _
                     Opo.ope_cls.ope_por_ant, _
                     Opo.ope_cls.ope_int_dev, _
                     Opo.ope_cls.ope_iva_com, _
                     Opo.ope_cls.ope_mon_gas, _
                     Opo.ope_cls.ope_sal_pag, _
                     Opo.ope_cls.ope_sal_pen, _
                     Opo.ope_cls.opn_cls.opn_pto_spr, _
                     Opo.ope_cls.opn_cls.opn_mto_doc, _
                     Opo.opo_otg, _
                     Opo.ope_cls.P_0104_cls.pnu_des, _
                     .TipoOperacion = Opo.ope_cls.opn_cls.P_0012_cls.pnu_des}

                    For Each c In rst
                        col.Add(c)
                    Next

                Next

                If LlenaGrid Then
                    GV.DataSource = OpeAnt
                    GV.DataBind()
                    Return col
                Else
                    Return col
                End If


            End If


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CANTIDAD_DEUDORES_DEVUELVE(ByVal id_ope As Integer) As Integer

        Try

            Dim i As Integer

            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim ddr = (From d In data.dsi_cls Where d.id_ope = id_ope Select d.deu_ide).Distinct.Count()

            i = ddr


            Return i


        Catch ex As Exception

            Return 0
        End Try

    End Function

    Public Function ValidaMontos(ByVal spr As String, ByVal Mto As Double, ByVal giro As Double, ByVal deu As Double, _
                                 ByVal comi As String) As String


        '**************************************************************************************************************************************************
        'Descripcion: Consulta si monto esta dentro del nivel de riesgo
        'Creado por A Saldivar.
        'Fecha Creacion: 22/06/2010
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim msj As String
            Dim spread = From c In data.cfc_cls Where c.id_P_0069 = 1 And c.cfc_dde <= spr And c.cfc_hta >= spr
            If spread.Count = 0 Then
                'Return False
                ' msj = "Monto de Spread Superior al de clasificación"
                msj = "Negociación no cae en ninguna clasificación de riesgo"
                'Return msj
                'Exit Function
            Else
                msj = Nothing
                Return msj
                Exit Function
            End If

            Dim mtoope = From c In data.cfc_cls Where c.id_P_0069 = 2 And c.cfc_dde <= Mto And c.cfc_hta >= Mto
            If mtoope.Count = 0 Then
                'Return False
                ' msj = "Monto de Spread Superior al de clasificación"
                msj = "Negociación no cae en ninguna clasificación de riesgo"
                'Return msj
                'Exit Function
            Else
                msj = Nothing
                Return msj
                Exit Function
            End If

            If giro > 0 Then
                Dim girolin = From c In data.cfc_cls Where c.id_P_0069 = 3 And c.cfc_dde <= giro And c.cfc_hta >= giro
                If girolin.Count = 0 Then
                    'Return False
                    ' msj = "Monto de Spread Superior al de clasificación"
                    msj = "Negociación no cae en ninguna clasificación de riesgo"
                    'Return msj
                    'Exit Function
                Else
                    msj = Nothing
                    Return msj
                    Exit Function
                End If
            End If

            If deu > 0 Then
                Dim deuda = From c In data.cfc_cls Where c.id_P_0069 = 4 And c.cfc_dde <= deu And c.cfc_hta >= deu
                If deuda.Count = 0 Then
                    'Return False
                    ' msj = "Monto de Spread Superior al de clasificación"
                    msj = "Negociación no cae en ninguna clasificación de riesgo"
                    'Return msj
                    'Exit Function
                Else
                    msj = Nothing
                    Return msj
                    Exit Function
                End If
            End If
            If comi <> "" Then
                Dim comision = From c In data.cfc_cls Where c.id_P_0069 = 5 And c.cfc_dde <= comi And c.cfc_hta >= comi
                If comision.Count = 0 Then
                    'Return False
                    ' msj = "Monto de Spread Superior al de clasificación"
                    msj = "Negociación no cae en ninguna clasificación de riesgo"
                    'Return msj
                    'Exit Function
                Else
                    msj = Nothing
                    Return msj
                    Exit Function
                End If
            End If
            'Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function cheques_respaldo_retorna(ByVal id_ope As Integer) As Collection
        Dim col As New Collection
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Try

            Dim cnslt = From c In Data.chr_cls Where c.id_ope = id_ope And c.id_P_0113 = 1 _
            Select New With {.pnu_des = c.p_0113_cls.pnu_des, c.chr_num, c.chr_mto, c.chr_fev, c.chr_cli_deu, c.cta_cte, c.chr_tip_cli, c.id_ope, c.id_bco, c.PL_000047_cls.pal_des, c.id_chr}

            For Each p In cnslt

                col.Add(p)

            Next

            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Documentos_con_respaldo_retorna(ByVal id_ope As Integer) As Collection

        Dim col As New Collection
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Try

            Dim cnslt = From d In Data.dsi_cls Where d.id_ope = id_ope And d.dsi_rsp = "S" And d.dsi_flj = "N" And d.dsi_est_rsp = "N" _
            Select New With {.pnu_des = d.P_0011_cls.pnu_des, d.dsi_num, d.dsi_flj_num, d.dsi_fev_rea, d.deu_ide, d.PL_000047_cls.pal_des, d.deu_cls.deu_rso, d.dsi_mto, d.id_dsi, d.dsi_fec_emi, d.id_ope}

            For Each p In cnslt

                col.Add(p)

            Next

            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Cliente_por_cheque_retorna(ByVal rut As Long, ByVal tipo As String) As String
        Dim col As New Collection
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim STR As String
        Try

            If tipo = "D" Then

                Dim DDR = From D In Data.deu_cls Where CInt(D.deu_ide) = rut

                For Each P In DDR
                    If P.id_P_0044 = 2 Then

                        STR = P.deu_rso.Trim
                    Else
                        STR = P.deu_rso.Trim & " " & P.deu_ape_ptn.Trim & " " & P.deu_ape_mtn
                    End If

                Next

                Return STR

            ElseIf tipo = "C" Then

                Dim CLI = From C In Data.cli_cls Where CInt(C.cli_idc) = rut

                For Each P In CLI

                    If P.id_P_0044 = 2 Then
                        STR = P.cli_rso.Trim
                    Else
                        STR = P.cli_rso.Trim & " " & P.cli_ape_ptn.Trim & " " & P.cli_ape_mtn
                    End If

                Next

                Return STR

            End If


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function cheques_asociados_retorna(ByVal id_dsi As Integer) As chr_cls
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim id_ch As Integer
        Dim nrd = From d In Data.nrd_cls Where d.id_dsi = id_dsi Select d.id_chr

        For Each p In nrd
            id_ch = p
        Next

        Dim chr = From c In Data.chr_cls Where c.id_chr = id_ch

        If chr.Count > 0 Then

            Return chr
        Else
            Return Nothing
        End If


    End Function

    Public Function DevuelveDeudoresDeUnaOperacion(ByVal nro_operacion As Integer, ByVal collpagos As Collection) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los pagadores y cupo disponible por una operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 23/10/2012
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     31-10-2012          Se agrega descuentos por deudores
        '**************************************************************************************************************************************************
        Try

            Dim col As New Collection
            Dim sesion As New ClsSession.ClsSession
            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Dim Temporal_dsi = From dsi1 In Data.dsi_cls _
                                Join deu1 In Data.deu_mon_cls On dsi1.deu_ide Equals deu1.deu_ide And deu1.id_p_0023 Equals dsi1.ope_cls.opn_cls.id_P_0023 _
                                Where dsi1.id_ope = nro_operacion _
                                And deu1.id_p_0029 = 1 _
                                Group By deudor = CLng(dsi1.deu_ide) & "-" & dsi1.deu_cls.deu_dig_ito & " " & dsi1.deu_cls.deu_rso.Trim, _
                                         rutdeudor = dsi1.deu_ide _
                                Into montodoc = Sum(dsi1.dsi_mto), _
                                   disponible = Max(deu1.deu_mon_dis), _
                                      ocupado = Max(deu1.deu_mon_ocu) _
                                Select New With { _
                                                 .disponible = disponible, _
                                                 .ocupado = ocupado, _
                                                 montodoc, _
                                                 deudor, rutdeudor}

            If Temporal_dsi.Count > 0 Then

                Dim dsctodeudores As Double = 0

                For Each p In Temporal_dsi
                    'sumamos todos los documentos que se esta pagando de un deudor
                    For i = 1 To collpagos.Count
                        If p.rutdeudor = collpagos(i).deu_ide And collpagos(i).PAGADEUDOR = "S" Then
                            dsctodeudores = dsctodeudores + collpagos(i).deu_ide
                        End If
                    Next

                    p.disponible = p.disponible + dsctodeudores

                    col.Add(p)

                Next

            End If

            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Actualizaciones Operaciones"

    Public Sub Guarda_CalificacionArraste(ByVal cal As String, ByVal CTO As String)

        '    '**************************************************************************************************************************************************
        '    'Descripcion: Guarda calificacion segun numero de contrato asignado 
        '    'Creado por Sebastian Henriquez C.
        '    'Fecha Creacion: 01/10/2012
        '    'Quien Modifica              Fecha              Descripcion
        '    '--------------------------------------------------------------------------------------------------------------------------------------------------

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim aux As Integer = (From d In Data.doc_cls _
                                 Join dc In Data.doc_con_cls On dc.id_doc Equals d.id_doc _
                                 Where (If(dc.cod_emp IsNot Nothing, dc.cod_emp.ToString(), "") & _
                                        If(dc.ofi_cin IsNot Nothing, dc.ofi_cin.ToString(), "") & _
                                        If(dc.pro_duc IsNot Nothing, dc.pro_duc.ToString(), "") & _
                                        If(dc.con_tra IsNot Nothing, dc.con_tra.ToString(), "") & _
                                        If(dc.dig_ver_doc IsNot Nothing, dc.dig_ver_doc.ToString(), "") = CTO) Select d.id_dsi).First()
            Dim Docto As clf_cls = (From c In Data.clf_cls Where c.id_dsi = aux).First

            With Docto
                .cal_arr_ast = cal
            End With

            Data.SubmitChanges()

        Catch ex As Exception

        End Try

    End Sub

    Public Function sincroniza_verificacion(ByVal rut_deu As Long, ByVal nro_doc As String, ByVal monto As Double, _
                                            ByVal tipo_docto As Integer, ByVal tipo As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: sincroniza las verificaciones en la tabla dvf con la tabla dsi
        'Creado por = Cristian Arce 
        'Fecha Creacion: 12/09/2011             
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim DATA As New CapaDatos.DataClsFactoringDataContext

            Dim DVF_CNS As dvf_cls = (From D In DATA.dvf_cls _
                         Where D.deu_ide = Format(CLng(rut_deu), Var.FMT_RUT) _
                         And D.id_P_0031 = tipo_docto _
                         And D.dvf_num = nro_doc _
                         And D.dvf_mto = monto Select D).First

            If IsNothing(DVF_CNS) Then
                Return False
            End If

            'no existe documento en dsi (ingreso documentos)
            If tipo = 1 Then

                Dim DSI_CNS As dsi_cls = (From C In DATA.dsi_cls _
                              Where C.deu_ide = Format(CLng(rut_deu), Var.FMT_RUT) _
                              And C.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                              And C.dsi_num = nro_doc _
                              And C.dsi_mto = monto Select C).First

                If Not IsNothing(DSI_CNS) Then

                    With DVF_CNS
                        DVF_CNS.id_dsi = DSI_CNS.id_dsi
                        DSI_CNS.id_P_0040 = DVF_CNS.id_P_0040
                    End With

                    DATA.SubmitChanges()

                End If

            End If



            'existe documento en dsi (verificacion)
            If tipo = 2 Then
                Try

                    Dim DSI_CNS As dsi_cls = (From C In DATA.dsi_cls _
                                  Where C.deu_ide = Format(CLng(rut_deu), Var.FMT_RUT) _
                                  And C.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                  And C.dsi_num = nro_doc _
                                  And C.dsi_mto = monto Select C).First

                    If IsNothing(DSI_CNS) Then
                        Return False
                    Else

                        With DVF_CNS
                            DVF_CNS.id_dsi = DSI_CNS.id_dsi
                            DSI_CNS.id_P_0040 = DVF_CNS.id_P_0040
                            DSI_CNS.dsi_fev_cal = cg.calcula_vcto_real(DSI_CNS.deu_ide, DSI_CNS.dsi_fev, Sucursal, "", tipo_docto)
                            DSI_CNS.dsi_fev_rea = FECHA_VCTO_CALCULO
                        End With

                        DATA.SubmitChanges()

                    End If

                Catch ex As Exception

                End Try

            End If

            Return True

        Catch ex As Exception

        End Try

    End Function

    Public Sub documentos_otorgados_modifica(ByVal estado_nuevo As Integer, ByVal pagado_ddr As String, _
                                        ByVal notificacion As String, _
                                         ByVal con_cobranza As String, ByVal pnu_mot As Integer, _
                                         ByVal env_bci As String, ByVal fech_cas As Date, _
                                         ByVal fech_dem As Date, ByVal obs_cobra As String, _
                                         ByVal con_obl As String)

        Dim DATA As New CapaDatos.DataClsFactoringDataContext

        Try



            For i = 0 To arreglo.Count - 1


                'Consulta a la tabla dsi , para seleccionar el registro a modificar


                Dim DSI_CNS = From D In DATA.dsi_cls Join doc1 In DATA.doc_cls On D.id_dsi Equals doc1.id_dsi _
                           Where doc1.id_doc = CLng(arreglo.Item(i)) _
                         Select D

                For Each p In DSI_CNS

                    With p
                        If .id_P_0011 <> estado_nuevo Then
                            .id_P_0011 = estado_nuevo
                        End If

                        If pnu_mot <> 0 Then
                            .id_P_0061 = pnu_mot
                        End If

                        .dsi_env_bci = env_bci
                        If estado_nuevo = 1 Then
                            .dsi_cbz_son = con_cobranza
                        End If
                        If estado_nuevo = 11 Then
                            .dsi_ntf = notificacion
                        End If
                    End With

                Next
                DATA.SubmitChanges()


            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Function OperacionModificaDocto(ByVal RutCli As String, ByVal NueRutDeu As String, ByVal RutDeuAnt As String, ByVal NroOpe As Integer, _
                                           ByVal Nuenrodoc As String, ByVal NroDocAnt As Integer, ByVal NueNroCuota As Integer, _
                                           ByVal NroCuotaAnt As Integer) As String

        '*********************************************************************************************************************************
        'Descripcion: Modifica Deudor, Nro. y Cuota de un Documento
        'Creado por= Yonathan Cabezas V.
        'Fecha Creacion: 25/03/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                  23/07/2010        Se cambio orden para que se pueda modificar
        '*********************************************************************************************************************************

        Dim StrMsj As String

        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext



            Dim Doctos = (From D In Data.dsi_cls Where D.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc = RutCli And _
                            D.ope_cls.id_ope = NroOpe And _
                            D.dsi_num = NroDocAnt And _
                            D.dsi_flj_num = NroCuotaAnt).First

            If Not IsNothing(Doctos) Then
                ' StrMsj = "Datos ya existen"
                Doctos.deu_ide = NueRutDeu
                Doctos.dsi_num = Nuenrodoc
                Doctos.dsi_flj_num = NueNroCuota

                Data.SubmitChanges()
                'Modifica Deudor
                Dim Deu = (From DDR In Data.ddr_cls Where DDR.cli_idc = RutCli And _
                           DDR.deu_ide = RutDeuAnt).First

                If Not IsNothing(Deu) Then
                    Deu.deu_ide = NueRutDeu
                    Data.SubmitChanges()
                End If
                StrMsj = "Documento modificado"

            Else

                'Dim Docto = (From D In Data.dsi_cls Where D.ope_cls.opn_cls.eva_cls.cli_cls.cli_idc = RutCli And _
                '            D.ope_cls.id_ope = NroOpe And _
                '            D.dsi_num = NueNroDoc And _
                '            D.dsi_flj_num = NueNroCuota And _
                '            D.deu_ide = NueRutDeu).First


                If Not IsNothing(Doctos) Then
                    'Docto.deu_ide = NueRutDeu
                    'Docto.dsi_num = NueNroDoc
                    'Docto.dsi_flj_num = NueNroCuota
                End If

                'Data.SubmitChanges()

                'Dim Deu = (From DDR In Data.ddr_cls Where DDR.cli_idc = RutCli And _
                '           DDR.deu_ide = RutDeuAnt).First

                'If Not IsNothing(Deu) Then
                '    Deu.deu_ide = NueRutDeu
                '    Data.SubmitChanges()
                'End If

            End If


            Return StrMsj
        Catch ex As Exception
            StrMsj = ex.Message
            Return StrMsj
        End Try
    End Function

    Public Function documento_masivo_inserta(ByVal coldsi As Collection, ByVal rutcli As String, ByVal tipo As Integer, _
                                             ByVal colclf As Collection, ByVal id_ope As Integer) As Boolean

        'P.Gatica 
        'Ingreso documentos a la dsi a traves de una planilla

        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim Calificacion As New clf_cls
        Dim aux As Integer
        Dim FC As New FuncionesGenerales.FComunes

        Try

            'Ordenamos documentos por su numero
            FC.SortCollection(coldsi, "dsi_num", True)


            'Insertamos los documentos

            For i = 1 To coldsi.Count

                Dim documentos As New dsi_cls

                documentos = coldsi.Item(i)
                Data.dsi_cls.InsertOnSubmit(documentos)

                If tipo = 3 Then
                    Dim CHR As New chr_cls

                    With CHR
                        .chr_cli_deu = coldsi.Item(i).deu_ide
                        .chr_fev = coldsi.Item(i).dsi_fev
                        .chr_mto = coldsi.Item(i).dsi_mto
                        .chr_num = coldsi.Item(i).dsi_num
                        .id_P_0112 = coldsi.Item(i).id_P_0112
                        .id_PL_000047 = coldsi.Item(i).id_PL_000047
                        .id_bco = coldsi.Item(i).id_bco
                        .id_P_0113 = 1
                        .chr_tip_cli = "D"
                    End With

                    Data.chr_cls.InsertOnSubmit(CHR)

                End If

                Data.SubmitChanges()

                aux = (From d In Data.dsi_cls Where d.id_ope = CLng(documentos.id_ope) And _
                                                               d.dsi_num = CLng(documentos.dsi_num) _
                       Select d.id_dsi).First()

                Calificacion = colclf.Item(i)

                With Calificacion
                    .id_dsi = aux
                End With

                Data.clf_cls.InsertOnSubmit(Calificacion)
                Data.SubmitChanges()

            Next

            'Si es una operacion con cuotas, se agrega correlativo de cuotas
            Dim operacion As ope_cls = (From o In Data.ope_cls Where o.id_ope.Equals(id_ope)).First()
            Dim dsi = From d In Data.dsi_cls Where d.id_ope.Equals(id_ope)

            If operacion.ope_cuo = "S" Then

                Dim RST As Integer = 0

                For Each d In dsi

                    RST = Documentos_cuota_valida(d.dsi_num, _
                                                  rutcli, _
                                                  "S", _
                                                  operacion.opn_cls.id_P_0031)

                    If RST = 999 Or RST = 998 Then
                        Return False
                    Else
                        d.dsi_flj_num = RST
                    End If

                Next

                Data.SubmitChanges()

            End If

            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub cabeceras_documento_marca(ByVal id_ope As Integer)

        Dim count As Integer

        Try

            'P.Gatica 
            'Marca documentos a la dsi a traves de una planilla

            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim deudores As New ArrayList
            Dim registro As String

            Dim doc = (From d In data.dsi_cls _
                       Where d.id_ope = id_ope And _
                             d.dsi_flj_num <> 0 _
                       Order By d.deu_ide _
                       Select d.dsi_num, d.deu_ide).Distinct


            If doc.Count > 0 Then

                For Each p In doc
                    registro = CStr(p.deu_ide)
                    registro = registro & CStr(p.dsi_num)

                    If deudores.Contains(registro) = False Then


                        Dim ds = From d In data.dsi_cls Where d.dsi_num = p.dsi_num And d.deu_ide = p.deu_ide And d.id_ope = id_ope And d.dsi_flj_num = 0 Select d

                        For Each c In ds

                            c.dsi_flj = "S"
                        Next


                        data.SubmitChanges()

                        deudores.Add(registro)
                        count = count + 1
                    End If





                Next

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Operación_masiva_cuadra(ByVal id_ope As Integer)

        Dim monto As Double
        Dim monto_ope As Double
        Dim cant As Integer

        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext
            '*******************************************************************************************************************
            'Cuadratura de Operaciones al pasar por el ultimo documento
            '*******************************************************************************************************************

            Dim ope = From op In data.ope_cls Where op.id_ope = id_ope Select op.opn_cls.opn_can_doc, op.opn_cls.opn_mto_doc

            For Each p In ope

                monto_ope = p.opn_mto_doc
                cant = p.opn_can_doc
            Next

            Dim dsi2 = From ds In data.dsi_cls Where ds.id_ope = id_ope


            For Each p In dsi2

                monto = monto + p.dsi_mto

            Next


            If Math.Round(monto, 4) = Math.Round(monto_ope, 4) And cant = dsi2.Count Then

                Dim opera = From oper In data.ope_cls Where oper.id_ope = id_ope

                For Each p In opera

                    Dim dsi_cns = (From d In data.dsi_cls Where d.id_ope = p.id_ope Select d.deu_ide).Distinct.Count()

                    Try

                        Dim opnn As opn_cls = (From o In data.ope_cls Where o.id_ope = p.id_ope Select o.opn_cls).First

                        opnn.opn_can_ddr = dsi_cns

                    Catch ex As Exception

                    End Try

                    p.ope_cdo = "S"

                Next

                data.SubmitChanges()

            Else
                Dim opera = From oper In data.ope_cls Where oper.id_ope = id_ope

                For Each p In opera
                    p.ope_cdo = "N"
                Next

                data.SubmitChanges()

            End If

        Catch ex As Exception

        End Try



    End Sub

    Public Function categoria_riesgo_guarda_dsi(ByVal nro_docto As Integer, ByVal tip_reg As String) As Boolean

        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim clf = From d In data.clf_cls Where (d.id_dsi = nro_docto)

            For Each p In clf
                p.cal_sub_jet = tip_reg
            Next

            data.SubmitChanges()

            Return True

        Catch ex As Exception

            Return False

        End Try


    End Function

    Public Function OTORGAMIENTO_ANULA(ByVal id_ope As Integer, ByVal id_opn As Integer, ByVal id_Eva As Integer) As Boolean

        Dim SQL As New FuncionesGenerales.SqlQuery
        Dim DATA As New CapaDatos.DataClsFactoringDataContext
        Dim rut_cli As String

        'Using ts = New TransactionScope

        'Cambia el estado de la Evaluacio a Cursada
        Dim Eva As eva_cls = (From O In DATA.eva_cls Where O.id_eva = id_Eva).First

        Eva.id_P_0110 = 5
        rut_cli = Eva.cli_idc

        'Cambia el estado de la Negociacion a Asociada 
        Dim Opn As opn_cls = (From O In DATA.opn_cls Where O.id_opn = id_opn).First

        Opn.id_P_0082 = 5

        'Cambia el estado de Operacion Otorgada a Anulada
        Dim Ope As ope_cls = (From O In DATA.ope_cls Where O.id_ope = id_ope).First

        Ope.ope_fec_anl = Date.Now
        Ope.id_P_0030 = 6


        Dim dsi = From d In DATA.dsi_cls Where d.id_ope = id_ope

        For Each D In dsi
            D.id_P_0011 = 13
        Next

        DATA.SubmitChanges()

        '--------------------------------------------------------------------------------------------
        'JLAGOS 11-10-2012 SE AGREGA RECHAZO DE EGRESOS NO ENTREGADOS
        Dim Opo As opo_cls = (From O In DATA.opo_cls Where O.id_ope = id_ope).First

        Try

            Dim egr = (From E In DATA.egr_cls Where E.id_opo = Opo.id_opo).First

            Dim egr_sec = From e In DATA.egr_sec_cls Where e.id_egr = egr.id_egr And e.egr_vld_rcz <> "E"


            For Each e In egr_sec

                e.egr_vld_rcz = "A"

                Dim ing_sec = From i In DATA.ing_sec_cls Where i.id_egr_sec = e.id_egr_sec

                For Each i In ing_sec

                    i.ing_vld_rcz = "A"

                    SQL.ExecuteNonQuery("update doc set doc_sdo_cli = " & Double.Parse(i.doc_sdo_cli) & " where id_doc = " & i.id_doc)

                    SQL.ExecuteNonQuery("update dsi set id_P_0011 = case when convert(varchar(10), dsi_fev_rea, 112)  >= convert(varchar(10), getdate(), 112)  then 1 " & _
                                                                         "when convert(varchar(10), dsi_fev_rea, 112)  <= convert(varchar(10), getdate(), 112)  then 2 " & _
                                        "End from dsi inner join doc on dsi.id_dsi = doc.id_dsi where id_doc = " & i.id_doc)

                    'Dim doc = From d In DATA.doc_cls Where d.id_doc = i.id_doc

                    'For Each d In doc
                    '    d.doc_sdo_cli = i.doc_sdo_cli
                    '    If (d.dsi_cls.dsi_fev_rea > DateTime.Now) Then
                    '        d.dsi_cls.id_P_0011 = 2
                    '    Else
                    '        d.dsi_cls.id_P_0011 = 1
                    '    End If
                    'Next

                Next

            Next

        Catch ex As Exception

        End Try

        ''--------------------------------------------------------------------------------------------
        ''JLAGOS 13-10-2012 SE ANULA DE CXC POR CONCEPTO GVM 4*1000
        'Dim cxc = From c In DATA.cxc_cls Where c.id_opo = Opo.id_opo AND
        'For Each c In cxc
        '    c.id_P_0057 = 1
        'Next

        DATA.SubmitChanges()

        'ts.Complete()

        'End Using

        '-------------------------------------------------------------------------------
        'shenriquez 21-09-2012 -se agrega la rebaja automatica de saldos
        DATA.sp_op_cierre_cliente(rut_cli, rut_cli, DateTime.Now())
        '-------------------------------------------------------------------------------

    End Function

    Public Sub documentos_otorgados_modifica(ByVal rut_cliente As String, ByVal rut_deudor As String, _
                                            ByVal operacion As Integer, ByVal tipo_docto As Integer, _
                                           ByVal estado_actual As Integer, ByVal nro_docto As Integer, _
                                           ByVal nro_cuota As Integer, _
                                           ByVal estado_nuevo As Integer, ByVal pagado_ddr As String, _
                                            ByVal tipo_update As String, ByVal notificacion As String, _
                                            ByVal con_cobranza As String, ByVal pnu_mot As Integer, _
                                            ByVal env_bci As String, ByVal fech_cas As Date, _
                                            ByVal fech_dem As Date, ByVal obs_cobra As String, _
                                            ByVal con_obl As String)

        Dim DATA As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim VAL = From D In DATA.doc_cls Where D.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(rut_cliente), Var.FMT_RUT) _
                                               And D.dsi_cls.deu_ide = Format(CLng(rut_deudor), Var.FMT_RUT) _
                                               And D.dsi_cls.id_ope = operacion _
                                               And D.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                               And D.dsi_cls.id_P_0011 = estado_actual _
                                               And D.dsi_cls.id_dsi = nro_docto _
                                               Select saldo = D.doc_sdo_cli()


            'Consulta a la tabla dsi , para seleccionar el registro a modificar
            Dim DSI_CNS = From D In DATA.dsi_cls _
                       Where D.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(rut_cliente), Var.FMT_RUT) _
                        And D.deu_ide = Format(CLng(rut_deudor), Var.FMT_RUT) _
                        And D.id_ope = operacion _
                        And D.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                        And D.id_P_0011 = estado_actual _
                        And D.id_dsi = nro_docto _
                        And D.dsi_flj_num = nro_cuota Select D

            For Each p In DSI_CNS

                With p
                    .id_P_0011 = estado_nuevo
                    If pnu_mot <> 0 Then
                        .id_P_0061 = pnu_mot
                    End If

                    .dsi_env_bci = env_bci
                    If estado_nuevo = 1 Then
                        .dsi_cbz_son = con_cobranza
                    End If
                    If estado_nuevo = 11 Then
                        .dsi_ntf = notificacion
                    End If
                End With

            Next


            'Consulta a la tabla doc, para seleccionar el registro a modificar

            Dim doc = From D In DATA.doc_cls Where D.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(rut_cliente), Var.FMT_RUT) _
                                     And D.dsi_cls.deu_ide = Format(CLng(rut_deudor), Var.FMT_RUT) _
                                     And D.dsi_cls.id_ope = operacion _
                                     And D.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                     And D.dsi_cls.id_P_0011 = estado_actual _
                                     And D.dsi_cls.id_dsi = nro_docto _
                                     Select D
            For Each p In doc

                With p
                    .doc_pag_ddr = pagado_ddr


                    If estado_nuevo = 12 Then
                        .doc_fec_cas = fech_cas
                    End If
                    If estado_nuevo = 11 Then
                        .doc_fec_dem = fech_dem
                    End If

                    .doc_obs_cob = obs_cobra
                    .doc_con_obl = con_obl

                End With

            Next


            DATA.SubmitChanges()


        Catch ex As Exception

        End Try

    End Sub

    Public Sub ModificaClasificacionDoctoPorOperacion(ByVal nrooperacion As Integer, ByVal cal As String)

        Dim Data As New CapaDatos.DataClsFactoringDataContext

        'traemos los documentos de operacion para modificar su calificacion
        Dim documentos = From d In Data.dsi_cls Where d.id_ope = nrooperacion Select d.id_dsi

        For Each d In documentos

            Try

                Dim dsi As clf_cls = (From c In Data.clf_cls Where c.id_dsi = d Select c).First
                dsi.cal_oto_gam = cal

            Catch ex As Exception
                'si el documento no tiene clasifacion, se crea...
                Dim clf_ins As New clf_cls
                clf_ins.id_dsi = d
                clf_ins.cal_oto_gam = cal
                Data.clf_cls.InsertOnSubmit(clf_ins)
            End Try

            Data.SubmitChanges()

        Next




    End Sub

    Public Function Documentos_Ingresa(ByVal dsi As dsi_cls, ByVal rut As Long, ByVal tipdoc As Integer, _
                                       ByVal tipo As String, ByVal CUOTA As String, ByVal cal As String) As Boolean

        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim monto As Double
        Dim monto_ope As Double
        Dim cant As Integer
        Dim aux As Integer

        If CUOTA = "S" And tipo = "M" Then

            Dim cnslt = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                  And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                  And ds.ope_cls.opn_cls.eva_cls.cli_idc = Format(rut, Var.FMT_RUT) _
                                                  And ds.dsi_flj_num = dsi.dsi_flj_num _
                                                  And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13

            For Each p In cnslt

                p.dsi_num = dsi.dsi_num
                p.dsi_mto = dsi.dsi_mto
                p.id_PL_000047 = dsi.id_PL_000047
                p.dsi_mto_fin = dsi.dsi_mto_fin
                p.dsi_fev_rea = dsi.dsi_fev_rea
                p.dsi_fev_cal = dsi.dsi_fev_cal
                p.dsi_fec_emi = dsi.dsi_fec_emi
                p.dsi_flj = dsi.dsi_flj
                p.dsi_flj_num = dsi.dsi_flj_num
                p.dsi_fev = dsi.dsi_fev
                p.dsi_ntf = dsi.dsi_ntf
                p.dsi_cbz_son = dsi.dsi_cbz_son
                p.dsi_ntf = dsi.dsi_ntf
                p.dsi_rsp = dsi.dsi_rsp
                p.dsi_mto_ant = dsi.dsi_mto_ant
                p.id_P_0112 = dsi.id_P_0112
                p.dsi_rsp = dsi.dsi_rsp

            Next

            Data.SubmitChanges()

        ElseIf CUOTA = "S" And tipo = "I" Then

            dsi.dsi_flj = "N"

            Data.dsi_cls.InsertOnSubmit(dsi)

        End If

        If tipo = "I" And CUOTA = "N" Then

            Dim cnslt = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                And ds.ope_cls.opn_cls.eva_cls.cli_idc = Format(rut, Var.FMT_RUT) _
                                                And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13

            If cnslt.Count > 0 Then
                Return False
                Exit Function
            End If

            Data.dsi_cls.InsertOnSubmit(dsi)

            If tipdoc = 3 Then
                Dim CHR As New chr_cls

                With CHR
                    .chr_cli_deu = dsi.deu_ide
                    .chr_fev = dsi.dsi_fev
                    .chr_fev_rea = dsi.dsi_fev_rea
                    '.dsi_fev_cal = dsi.dsi_fev_cal
                    .chr_mto = dsi.dsi_mto
                    .chr_num = dsi.dsi_num
                    .id_P_0112 = dsi.id_P_0112
                    .id_PL_000047 = dsi.id_PL_000047
                    .id_ope = dsi.id_ope
                    .id_bco = dsi.id_bco
                    .cta_cte = dsi.cta_cte
                    .id_P_0113 = 1
                    .chr_tip_cli = "D"
                    .chr_tip = "F"
                End With

                Try

                    Data.chr_cls.InsertOnSubmit(CHR)
                    Data.SubmitChanges()

                    Dim nrd As New nrd_cls

                    With nrd
                        .id_chr = CHR.id_chr
                        .id_dsi = dsi.id_dsi
                        .mto_resp = dsi.dsi_mto

                    End With
                    Data.nrd_cls.InsertOnSubmit(nrd)
                    Data.SubmitChanges()
                    '  Return True

                Catch ex As Exception
                    Return False
                End Try
            End If

        ElseIf tipo = "M" And CUOTA = "N" Then

            Dim RESP As String
            Dim cnslt = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                   And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                   And ds.ope_cls.opn_cls.eva_cls.cli_idc = Format(rut, Var.FMT_RUT) _
                                                   And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13

            For Each p In cnslt

                p.dsi_num = dsi.dsi_num
                p.dsi_mto = dsi.dsi_mto
                p.id_PL_000047 = dsi.id_PL_000047
                p.dsi_mto_fin = dsi.dsi_mto_fin
                p.dsi_fev_rea = dsi.dsi_fev_rea
                p.dsi_fev_cal = dsi.dsi_fev_cal
                p.dsi_fec_emi = dsi.dsi_fec_emi
                p.dsi_flj = dsi.dsi_flj
                p.dsi_flj_num = dsi.dsi_flj_num
                p.dsi_fev = dsi.dsi_fev
                p.dsi_ntf = dsi.dsi_ntf
                p.dsi_cbz_son = dsi.dsi_cbz_son
                p.dsi_ntf = dsi.dsi_ntf
                p.dsi_mto_ant = dsi.dsi_mto_ant
                p.dsi_env_bci = dsi.dsi_env_bci
                p.id_P_0112 = dsi.id_P_0112
                p.dsi_rsp = dsi.dsi_rsp

                'p.id_ope = dsi.id_opE
                'p.dsi_est_rsp = dsi.dsi_est_rsp

            Next

            Data.SubmitChanges()

        End If


        Dim dsi_axu = (From d In Data.dsi_cls Select d.dsi_fev_rea).Max

        Dim opn As opn_cls = (From o In Data.ope_cls Where dsi.id_ope = o.id_ope Select o.opn_cls).First()

        opn.opn_fev = opn.opn_fev_ori

        If opn.opn_fev < dsi_axu Then
            opn.opn_fev = dsi.dsi_fev_rea

            Dim dias_per As Integer
            'Lista Detalle Documentos
            dias_per = DateDiff("d", opn.opn_fev, opn.opn_fec)

            If dias_per < 0 Then
                dias_per = dias_per * -1
            End If

            Dim TYP As typ_cls

            TYP = cmc.TasaPlazosDevuelve(opn.id_P_0023, dias_per)

            If IsNothing(TYP) Then

                opn.opn_tas_bas = 0
            Else
                opn.opn_tas_bas = TYP.typ_mto
            End If
        End If

        Data.SubmitChanges()

        Dim ope = From op In Data.ope_cls Where op.id_ope = dsi.id_ope Select op.opn_cls.opn_can_doc, op.opn_cls.opn_mto_doc

        For Each p In ope

            monto_ope = p.opn_mto_doc
            cant = p.opn_can_doc
        Next

        Dim dsi2 = From ds In Data.dsi_cls Where ds.id_ope = dsi.id_ope


        For Each p In dsi2

            monto = monto + p.dsi_mto

        Next


        If monto = monto_ope And cant = dsi2.Count Then
            Dim opera = From oper In Data.ope_cls Where oper.id_ope = dsi.id_ope
            For Each p In opera
                p.ope_cdo = "S"
                Dim dsi_cns = (From d In Data.dsi_cls Where d.id_ope = dsi.id_ope Select d.deu_ide).Distinct.Count()

                Dim opnn As opn_cls = (From o In Data.ope_cls Where o.id_ope = dsi.id_ope Select o.opn_cls).First

                opnn.opn_can_ddr = dsi_cns
            Next
            Data.SubmitChanges()
        Else
            Dim opera = From oper In Data.ope_cls Where oper.id_ope = dsi.id_ope
            For Each p In opera
                p.ope_cdo = "N"
            Next
            Data.SubmitChanges()
        End If

        Dim operac = From op In Data.ope_cls Where op.id_ope = dsi.id_ope

        For Each p In operac

            If CDate(p.ope_fev) < dsi.dsi_fev_rea Then
                p.ope_fev = dsi.dsi_fev_rea
                'p.dsi_fev_cal = dsi.dsi_fev_cal
            End If

            Data.SubmitChanges()

        Next

        aux = (From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                  And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                  And ds.ope_cls.opn_cls.eva_cls.cli_idc = Format(rut, Var.FMT_RUT) _
                                                  And ds.dsi_flj_num = dsi.dsi_flj_num _
                                                  And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13 _
                                                  Select ds.id_dsi).First

        '***********************************************************
        'Se guarda calificacion 
        '***********************************************************
        Try
            Dim dsi_clf As clf_cls = (From c In Data.clf_cls Where c.id_dsi = aux Select c).First

            With dsi_clf
                .cal_oto_gam = cal
            End With
            Data.SubmitChanges()

        Catch ex As Exception
            Dim clf_ins As New clf_cls
            clf_ins.id_dsi = aux
            clf_ins.cal_oto_gam = cal
            Data.clf_cls.InsertOnSubmit(clf_ins)
            Data.SubmitChanges()
        End Try

        Return True

    End Function

    Public Function Documentos_Elimina(ByVal dsi As Integer, ByVal ID_OPE As Integer, ByVal rut As String, ByVal tipdoc As Integer, ByVal cuo As Integer) As Boolean

        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim monto As Double
        Dim monto_ope As Double
        Dim cant As Integer


        

        Try

            'Dim doc = From d In Data.doc_cls Where d.id_dsi = dsi And d.opo_cls.ope_cls.id_P_0030 = 1

            'If doc.Count > 0 Then
            '    Data.doc_cls.DeleteAllOnSubmit(doc)
            '    Data.SubmitChanges()
            'End If

            'Solo puede eliminar documentos cuando la operacion este en estado ingresado, si paso a simulada u otorgado NO PUEDE.
            Try

                Dim cnsl As dsi_cls = (From ds In Data.dsi_cls Where ds.id_dsi = dsi And ds.ope_cls.id_P_0030 = 1).First

                Dim nr = From n In Data.nrd_cls Where n.id_dsi = dsi And n.dsi_cls.ope_cls.id_P_0030 = 1

                Data.nrd_cls.DeleteAllOnSubmit(nr)
                Data.SubmitChanges()

                Data.dsi_cls.DeleteOnSubmit(cnsl)
                Data.SubmitChanges()

            Catch ex As Exception
                _EstadoOperacion = 2
                _MensajeOperacion = "No se puede eliminar documentos de una operación simulada u otorgada, favor volver a buscar operación..."
                Exit Function
            End Try

            Dim ope = From op In Data.ope_cls Where op.id_ope = ID_OPE Select op.opn_cls.opn_can_doc, op.opn_cls.opn_mto_doc

            For Each p In ope
                monto_ope = p.opn_mto_doc
                cant = p.opn_can_doc
            Next

            Dim dsi2 = From ds In Data.dsi_cls Where ds.id_ope = ID_OPE

            For Each p In dsi2
                monto = monto + p.dsi_mto
            Next

            If monto = monto_ope And cant = dsi2.Count Then
                Dim opera = From oper In Data.ope_cls Where oper.id_ope = ID_OPE
                For Each p In opera
                    p.ope_cdo = "S"
                Next
                Data.SubmitChanges()
            Else
                Dim opera = From oper In Data.ope_cls Where oper.id_ope = ID_OPE
                For Each p In opera
                    p.ope_cdo = "N"
                Next
                Data.SubmitChanges()
            End If

            _EstadoOperacion = 1
            _MensajeOperacion = "Documento Eliminado"

        Catch ex As Exception
            _EstadoOperacion = 999
            _MensajeOperacion = ex.Message
        End Try

    End Function

    Public Function OperacionInserta(ByVal OPE As ope_cls, ByVal TipoDocto As Integer) As Integer

        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Inserta Operacion de un deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 22/08/2008
        'Quien Modifica              Fecha              Descripcion
        '  JLagos                   28/01/2009          Se cambia cuando asocia a la planilla de aprobaciones desde guardar negociacion a cuando graba la operacion
        '  JLagos                   10/07/2009          Se cambia cuando asocia a la planilla de aprobaciones desde envia a operacion a cuando se simula
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------

        Try


            Using ts = New TransactionScope

                Dim Data As New CapaDatos.DataClsFactoringDataContext

                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                'Cambia el estado de la negociacion a 2 Asociada
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                Dim opn As opn_cls = (From O In Data.opn_cls Where O.id_opn = OPE.id_opn And O.id_P_0082 <= 2).First

                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                'si la negociacion esta simulada no puede modificarla   
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                If opn.id_P_0082 = 3 Then
                    Return 0
                End If

                ' la cambia a estado asociada
                opn.id_P_0082 = 2

                Data.SubmitChanges()

                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                'Pregunta si hay una operacion con esa negociacion , de ser asi , actualiza
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                Dim op = From o In Data.ope_cls Where o.id_opn = OPE.id_opn And o.id_P_0030 = 1

                If op.Count > 0 Then

                    For Each p In op

                        OPE.id_ope = p.id_ope
                        p.id_P_0030 = 1
                        p.ope_cuo = OPE.ope_cuo
                        p.ope_com_tot = OPE.ope_com_tot
                        p.ope_por_ant = OPE.ope_por_ant
                        p.ope_fev = OPE.ope_fev
                        p.ope_mto_ant = OPE.ope_mto_ant
                        p.ope_cnt = OPE.ope_cnt
                        p.ope_res_son = OPE.ope_res_son
                        p.ope_fac_cam = OPE.ope_fac_cam
                        p.ope_dif_pre = OPE.ope_dif_pre
                        p.ope_fec_sim = OPE.ope_fec_sim
                        p.id_eje = OPE.id_eje
                        p.ope_mon_gas = OPE.ope_mon_gas
                        p.ope_iva_com = OPE.ope_iva_com
                        p.ope_pre_com = OPE.ope_pre_com
                        p.ope_sal_pag = OPE.ope_sal_pag
                        p.ope_sal_pen = OPE.ope_sal_pen
                        p.ope_tot_gir = OPE.ope_tot_gir
                        p.ope_tot_gir_ant = OPE.ope_tot_gir_ant
                        p.ope_ptl = OPE.ope_ptl
                        p.ope_res_son = OPE.ope_res_son
                        p.ope_cuo = OPE.ope_cuo
                        p.ope_lnl = OPE.ope_lnl
                        p.ope_val_gmf = OPE.ope_val_gmf

                    Next

                    Data.SubmitChanges()

                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    'Valida que la operacion siga cuadrada  , en caso de haberlo estado
                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    'Valida que la operacion siga cuadrada, en caso de haberlo estado (Versión: 12122013.V1)
                    Dim CantDocumentos As Int16
                    Dim MontoDocumentos As Double
                    Dim dsi = From D In Data.dsi_cls Where D.id_ope = OPE.id_ope And D.id_P_0011 = 1
                    Dim contador As Integer = 1

                    CantDocumentos = 0
                    MontoDocumentos = 0

                    For Each d In dsi

                        d.dsi_mto_ant = d.dsi_mto_fin * (opn.opn_por_ant / 100)

                        If d.dsi_flj_num >= 0 Then
                            CantDocumentos = CantDocumentos + 1
                        End If

                        If d.dsi_flj = "N" Then
                            MontoDocumentos = MontoDocumentos + d.dsi_mto_fin
                        End If

                        If (From C In Data.clf_cls Where C.id_dsi = d.id_dsi Select C.id_dsi).Count = 0 Then

                            Dim clf_ins As New clf_cls

                            clf_ins.id_dsi = d.id_dsi
                            clf_ins.cal_oto_gam = opn.cal_oto_gam

                            Data.clf_cls.InsertOnSubmit(clf_ins)

                        End If

                        If contador = 5000 Or contador = 10000 Or contador = 15000 Or contador = 20000 Then
                            Data.SubmitChanges()
                        End If

                        contador = contador + 1

                    Next

                    Data.SubmitChanges()
                    'Valida que la operacion siga cuadrada, en caso de haberlo estado
                    Dim CDO = From D In Data.ope_cls Where D.id_opn = OPE.id_opn And D.id_P_0030 = 1

                    For Each c In CDO

                        If (Math.Round(MontoDocumentos, 4) <> Math.Round(CDbl(opn.opn_mto_doc), 4) Or _
                            (CantDocumentos <> opn.opn_can_doc)) Then
                            c.ope_cdo = "N"
                        Else
                            c.ope_cdo = "S"
                        End If

                    Next

                Else
                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    'En caso de que no haya operacion asociada a la negociacion, crea una nueva operacion
                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                    Data.ope_cls.InsertOnSubmit(OPE)

                End If

                Data.SubmitChanges()

                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                'Buscamos los gastos (fijos y definidos) asociados a la negociacion para darle el id_ope
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                Dim Gastos = From G In Data.gos_cls Where G.gdn_cls.id_opn = OPE.id_opn Or G.gfn_cls.id_opn = OPE.id_opn And G.ope_cls.id_P_0030 = 1

                For Each G In Gastos
                    G.id_ope = OPE.id_ope
                Next

                Data.SubmitChanges()

                ts.Complete()

            End Using

            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub ProrrateoDeDocumentos(ByVal id_ope As Integer)
        '---------------------------------------------------------------------------
        '25-07-2012 jlagos -se agrega prorroteo de documentos (gastos)
        '22-11-2012 jlagos -se cambia el prorrateo al final del ciclo de simulacion

        Dim Data As New DataClsFactoringDataContext

        Data.sp_ope_prorrateo_montos_X_doctos(id_ope)

    End Sub

    Public Function simulacion_ingresa(ByVal ope As ope_cls, _
                                       ByVal rut_cliente As String, _
                                       ByVal coll_dsi As Collection, _
                                       ByVal mto_gmf As Double) As Boolean

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim TipoOperacion As Integer

            Dim Operacion = From O In Data.ope_cls Where O.opn_cls.eva_cls.cli_idc = rut_cliente _
                                                    And O.id_ope = ope.id_ope And O.opn_cls.id_P_0082 = 2


            If IsNothing(Operacion) Then
                Return False
            End If

            For Each P In Operacion

                P.ope_fec_sim = ope.ope_fec_sim
                P.ope_val_duf = ope.ope_val_duf
                P.ope_imp_ope = ope.ope_imp_ope
                P.ope_val_dus = ope.ope_val_dus
                P.ope_tmc_dsi = ope.ope_tmc_dsi
                P.id_P_0030 = ope.id_P_0030
                P.ope_dif_pre = ope.ope_dif_pre
                P.ope_sal_pag = ope.ope_sal_pag
                P.ope_pre_com = ope.ope_pre_com
                P.ope_sal_pen = ope.ope_sal_pen
                P.ope_com_tot = ope.ope_com_tot
                P.ope_int_dev = ope.ope_int_dev
                P.ope_mon_gas = ope.ope_mon_gas
                P.ope_fog_ven = ope.ope_fog_ven
                P.ope_fog_son = ope.ope_fog_son
                P.opn_cls.id_P_0082 = 3
                P.opn_cls.eva_cls.id_P_0110 = 3
                P.ope_tot_gir = ope.ope_tot_gir
                P.ope_mto_ant = ope.ope_mto_ant
                P.ope_iva_com = ope.ope_iva_com
                P.id_eje = ope.id_eje
                P.ope_mto_scb = ope.ope_mto_scb
                P.ope_fac_cam = ope.ope_fac_cam
                P.ope_val_gmf = ope.ope_val_gmf
                P.ope_mon_gas_afe = ope.ope_mon_gas_afe

                TipoOperacion = P.opn_cls.id_P_0012

                If IsNothing(ope.id_P_0070) Or ope.id_P_0070 = 0 Then
                    P.id_P_0070 = Nothing
                Else
                    P.id_P_0070 = ope.id_P_0070
                End If

            Next

            Data.SubmitChanges()

            '-----------------------------------------------------------------------------------------------------
            '14-07-2012 jlagos -se agrega condicion que busca clasificacion para todas las operacion que sean 
            '                   distinto de tipo operacion de anticipo
            '-----------------------------------------------------------------------------------------------------
            If TipoOperacion <> 4 Then
                'Data.sp_AsociacionClasificacion(ope.id_opn)
                Dim Sql As New FuncionesGenerales.SqlQuery
                Sql.ExecuteNonQuery("Exec sp_AsociacionClasificacion " & ope.id_opn & " ")
            End If

            '--------------------------------------------------------------------------------------------------------------------------------------------------------------
            'elimina la relacion de requisitos por operacion
            '--------------------------------------------------------------------------------------------------------------------------------------------------------------

            Try

                Dim rxo = From R In Data.rxo_cls Where R.id_ope = ope.id_ope

                If rxo.Count > 0 Then
                    Data.rxo_cls.DeleteAllOnSubmit(rxo)
                    Data.SubmitChanges()
                End If

            Catch ex As Exception
                Return False
            End Try

            '--------------------------------------------------------------------------------------------------------------------------------------------------------------
            'Inserta la relacion de requisitos por tipo de documento, traemos los requisitos de un tipo de documento
            '--------------------------------------------------------------------------------------------------------------------------------------------------------------

            Try

                Dim opn As opn_cls = (From o In Data.opn_cls Where o.id_opn = ope.id_opn Select o).First


                Dim ReqDoc = From R In Data.rxd_cls Where R.id_p_0031 = opn.id_P_0031 And R.req_cls.req_est = "A" Order By R.id_req

                For Each r In ReqDoc

                    Dim rxo As New rxo_cls

                    rxo.id_ope = ope.id_ope
                    rxo.id_rxd = r.id_rxd
                    rxo.id_eje = Nothing
                    rxo.rxo_est = "P"

                    Data.rxo_cls.InsertOnSubmit(rxo)

                Next

                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                'Si la negociacion fue guardada con GMF, se debe crear una CXC y debe aplicar cuenta
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------
                If opn.opn_gen_gmf = "S" Then

                    Dim cxc As New cxc_cls

                    cxc.id_P_0023 = opn.id_P_0023
                    cxc.id_P_0041 = 23 'Tipo de cuenta GMF
                    cxc.id_P_0057 = 1 'Estado cuenta (VIGENTE)
                    cxc.cxc_fec = Date.Now
                    cxc.cxc_des = "CXC POR CONCEPTO DE GMF A LA OPERACION N° " & ope.id_ope
                    cxc.cxc_fac_cam = ope.ope_fac_cam
                    cxc.cxc_mto = mto_gmf '(sistema.sis_can_gmf * (ope.ope_tot_gir / 1000))
                    cxc.cxc_sal = mto_gmf
                    cxc.cli_idc = rut_cliente
                    cxc.id_eje = opn.eva_cls.cli_cls.id_eje_cod_eje

                    Data.cxc_cls.InsertOnSubmit(cxc)
                    Data.SubmitChanges()

                    'Se asocia a la operacion
                    Dim sim_cxc As New sim_cxc_cls

                    sim_cxc.id_ope = ope.id_ope
                    sim_cxc.id_cxc = cxc.id_cxc

                    Data.sim_cxc_cls.InsertOnSubmit(sim_cxc)
                    Data.SubmitChanges()

                    '-------------------------------------------------------------------------------------------------
                    'genero egreso sin giro por cxc de gmf
                    '-------------------------------------------------------------------------------------------------

                    Try

                        Dim egr As New egr_cls

                        With egr
                            .cli_idc = rut_cliente
                            .egr_fec = Date.Now
                            .egr_obs = "POR CONCEPTO DE GMF POR N° OPE. " & ope.id_ope.ToString()
                            .id_eje = CodEje
                            .id_opo = Nothing ' se le asigna en el otorgamiento
                        End With

                        Data.egr_cls.InsertOnSubmit(egr)
                        Data.SubmitChanges()

                        Dim sim_egr As New sim_egr_cls

                        sim_egr.id_ope = ope.id_ope
                        sim_egr.id_egr = egr.id_egr

                        Data.sim_egr_cls.InsertOnSubmit(sim_egr)
                        Data.SubmitChanges()

                        Dim Egr_Sec As New egr_sec_cls

                        QUE_SE_PAGA = 4
                        TIPO_EGRESO = 5
                        ANTES_DE_14_HRS = "N"
                        OBSERVACION_EGR = ""
                        BancoEgreso = Nothing
                        CtaCteEgreso = ""

                        With Egr_Sec
                            .id_egr = egr.id_egr
                            .egr_mto = Round(mto_gmf, 2)
                            .id_P_0056 = TIPO_EGRESO

                            If NRO_CXP = 0 Then
                                .id_cxp = Nothing
                            Else
                                .id_cxp = NRO_CXP
                            End If

                            .id_P_0055 = 4
                            .egr_dep_ant = ANTES_DE_14_HRS
                            .egr_vld_rcz = "S"
                            .egr_ent = "N"

                            If BancoEgreso = 0 Then
                                .id_bco = Nothing
                            Else
                                .id_bco = BancoEgreso
                            End If

                            .egr_cta_cte = CtaCteEgreso
                        End With

                        Data.egr_sec_cls.InsertOnSubmit(Egr_Sec)
                        Data.SubmitChanges()

                        '-------------------------------------------------------------------------------------------------
                        'Genera ingreso
                        '-------------------------------------------------------------------------------------------------
                        Dim ABONO_CLIENTE, MONTO_MENOR, REAJUSTE As Double
                        Dim SaldoPorPagar As Double = 0
                        Dim MONTO As Double
                        Dim INTERES As Double
                        Dim mto_abo_real As Double
                        Dim Ing_Sec As New ing_sec_cls
                        Dim Interes_Abonado As Double = 0

                        Dim ing As New ing_cls

                        ing.ing_sis_fec = Date.Now
                        ing.ing_fec = cxc.cxc_fec

                        Data.ing_cls.InsertOnSubmit(ing)
                        Data.SubmitChanges()

                        Ing_Sec.cli_idc = rut_cliente
                        Ing_Sec.id_ing = ing.id_ing
                        Ing_Sec.id_egr_sec = Egr_Sec.id_egr_sec
                        Ing_Sec.ing_qpa = "C" 'cliente
                        Ing_Sec.ing_vld_rcz = CChar("I")
                        Ing_Sec.ing_pro = "N"
                        Ing_Sec.ing_tas_apl = 0 'CDec(Objeto.Tasa)

                        'Toma el factor de cuando se ingreso la cuenta
                        FACTOR_CAMBIO = cg.ParidadDevuelve(cxc.id_P_0023, Date.Now.ToShortDateString).par_val

                        Ing_Sec.id_P_0053 = 1
                        'FACTOR_CAMBIO_OBS_HOY = 1

                        'Select Case cxc.id_P_0023
                        '    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                        '    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                        '    Case 3 : FACTOR_CAMBIO_OBS_HOY = pagos.DollarObservador
                        '    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                        'End Select

                        Ing_Sec.id_cxc = cxc.id_cxc
                        Ing_Sec.doc_sdo_ddr = 0

                        mto_abo_real = cxc.cxc_mto
                        Ing_Sec.ing_pag_deu = CChar("N")

                        Ing_Sec.ing_fac_cam = FACTOR_CAMBIO

                        If IsNothing(Ing_Sec.doc_sdo_cli) Then
                            Ing_Sec.doc_sdo_cli = CDec(cxc.cxc_mto) * Ing_Sec.ing_fac_cam
                        End If

                        Ing_Sec.ing_rea_mon = REAJUSTE

                        'Ojo ver cuando el interes es negativo
                        Ing_Sec.ing_mto_int = 0
                        Ing_Sec.ing_mto_abo = mto_abo_real
                        Ing_Sec.ing_mto_tot = mto_abo_real
                        Ing_Sec.ing_int_dev = 0
                        Ing_Sec.ing_fac_cam_obs = FACTOR_CAMBIO

                        If Val(Ing_Sec.ing_mto_tot) >= Val(Ing_Sec.doc_sdo_cli) Then
                            Ing_Sec.ing_tot_par = "T"
                        Else
                            Ing_Sec.ing_tot_par = "P"
                        End If

                        Data.ing_sec_cls.InsertOnSubmit(Ing_Sec)
                        Data.SubmitChanges()

                    Catch ex As Exception

                    End Try

                    '-------------------------------------------------------------------------------------------------

                End If

                Data.SubmitChanges()

            Catch ex As Exception
                Return False
            End Try

            If ope.ope_cuo = "S" Then

                Dim dsi = From D In Data.dsi_cls Where D.id_ope = ope.id_ope And D.dsi_flj_num <> 0 Order By D.id_dsi Ascending

                For Each p In dsi

                    For i = 1 To coll_dsi.Count

                        If coll_dsi.Item(i).dsi_num = p.dsi_num And coll_dsi.Item(i).dsi_flj_num = p.dsi_flj_num Then
                            p.dsi_sal_pag = coll_dsi.Item(i).dsi_sal_pag
                            p.dsi_sal_pen = coll_dsi.Item(i).dsi_sal_pen
                            p.dsi_iva_cms = coll_dsi.Item(i).dsi_iva_cms
                            p.dsi_dif_pre = coll_dsi.Item(i).dsi_dif_pre
                            p.dsi_mto_ant = coll_dsi.Item(i).dsi_mto_ant
                            p.dsi_cms = coll_dsi.Item(i).dsi_cms
                            p.dsi_ctd_dia = coll_dsi.Item(i).dsi_ctd_dia
                            Exit For
                        End If

                    Next

                Next

                Data.SubmitChanges()

            Else

                Dim dsi = From D In Data.dsi_cls Where D.id_ope = ope.id_ope Order By D.id_dsi Ascending

                For Each p In dsi

                    For i = 1 To coll_dsi.Count
                        If coll_dsi.Item(i).dsi_num = p.dsi_num Then
                            p.dsi_sal_pag = coll_dsi.Item(i).dsi_sal_pag
                            p.dsi_sal_pen = coll_dsi.Item(i).dsi_sal_pen
                            p.dsi_iva_cms = coll_dsi.Item(i).dsi_iva_cms
                            p.dsi_dif_pre = coll_dsi.Item(i).dsi_dif_pre
                            p.dsi_mto_ant = coll_dsi.Item(i).dsi_mto_ant
                            p.dsi_cms = coll_dsi.Item(i).dsi_cms
                            p.dsi_ctd_dia = coll_dsi.Item(i).dsi_ctd_dia
                            Exit For
                        End If

                    Next

                Next

                Data.SubmitChanges()

            End If

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function MIN(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        MIN = IIf(Par1 < Par2, Par1, Par2)
    End Function

    Public Sub OperacionActualizaGastos(ByVal id_ope As Integer, ByVal mto As Double)
        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim ope As ope_cls = (From p In Data.ope_cls Where p.id_ope = id_ope).First

            With ope
                ope.ope_mon_gas = mto
            End With

            Data.SubmitChanges()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub OperacionModifica(ByVal OPE As ope_cls, ByVal tipo As Integer, Optional ByVal idsuc As Long = Nothing)
        '*********************************************************************************************************************************
        'Descripcion: Inserta Operacion de un deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 22/08/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     23-10-2012          se agrega validacion de descuadre
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim monto_ope As Double
            Dim cant As Integer

            Dim o = From op In Data.ope_cls Where op.id_ope = OPE.id_ope And op.id_P_0030 = 1 Select op.opn_cls.opn_can_doc, op.opn_cls.opn_mto_doc

            If o.Count <= 0 Then
                _MensajeOperacion = "No se puede guardar una operación que no esta con estado ingresada, favor consultar nuevamente."
                _EstadoOperacion = 999
                Exit Try
            End If

            For Each p In o
                monto_ope = p.opn_mto_doc
                cant = p.opn_can_doc
            Next

            Dim dsi2 = From ds In Data.dsi_cls Join c In Data.clf_cls On c.id_dsi Equals ds.id_dsi Where ds.id_ope = OPE.id_ope Select ds.dsi_mto

            For Each p In dsi2
                MONTO = MONTO + Decimal.Parse(p.Value)
            Next

            Dim opera = From oper In Data.ope_cls Where oper.id_ope = OPE.id_ope

            If MONTO = monto_ope And cant = dsi2.Count Then
                For Each p In opera
                    p.ope_cdo = "S"
                Next
            Else
                For Each p In opera
                    p.ope_cdo = "N"
                Next
            End If

            Data.SubmitChanges()

            If tipo = 1 Then

                Dim operacion As ope_cls = (From p In Data.ope_cls Where p.id_ope = OPE.id_ope And p.opn_cls.id_P_0082 = 2).First

                With operacion
                    .ope_ptl = OPE.ope_ptl
                    .ope_res_son = OPE.ope_res_son
                    .ope_cuo = OPE.ope_cuo
                    .ope_lnl = OPE.ope_lnl
                    .id_P_0104 = OPE.id_P_0104
                End With

            ElseIf tipo = 2 Then

                Dim operacion As ope_cls = (From p In Data.ope_cls Where p.id_ope = OPE.id_ope And p.opn_cls.id_P_0082 = 4).First

                With operacion
                    .ope_ptl = OPE.ope_ptl
                    .ope_res_son = OPE.ope_res_son
                    .ope_cuo = OPE.ope_cuo
                    .ope_lnl = OPE.ope_lnl
                    .id_P_0104 = OPE.id_P_0104
                End With

            End If

            If idsuc <> Nothing Or idsuc > 0 Then
                Dim neg As opn_cls = (From opn In Data.opn_cls Where opn.id_opn = OPE.id_opn).First
                neg.id_suc = idsuc
            End If

            Data.SubmitChanges()

            _MensajeOperacion = "Operación actualizada correctamente."
            _EstadoOperacion = 1

        Catch ex As Exception
            _MensajeOperacion = ex.Message
            _EstadoOperacion = 999
        End Try

    End Sub

    Public Function ValidaEstadoOperacion(ByVal ope_num As Integer) As Integer

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim operacion As ope_cls = (From o In Data.ope_cls Where o.id_ope = ope_num).First

            Return operacion.id_P_0030

        Catch ex As Exception
            Return 0
        End Try


    End Function

    Public Function Operaciones_Anula(ByVal RutCliente As String, ByVal ope_num As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Anula las operaciones 
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 30/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                 
        'ASaldivar                   15/07/2010         Se anula La evaluacion asociada a la opn
        'jlagos                      21/08/2012         se agrega eliminacion (CAMBIO DE ESTADO) de doctos de la operacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim operacion As New ope_cls
            Dim col As New Collection

            '--------------------------------------------------------------------------------------------------------
            'OPERACION
            Dim OpeAnt As ope_cls = (From Ope In Data.ope_cls _
                                     Where Ope.opn_cls.eva_cls.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And _
                                           Ope.id_ope = ope_num).First

            OpeAnt.id_P_0030 = 6 'ESTADO ANULADA
            '--------------------------------------------------------------------------------------------------------

            'NEGOCIACION
            Dim opn As opn_cls = (From o In Data.opn_cls Where o.id_opn = OpeAnt.id_opn).First

            opn.id_P_0082 = 5 'ESTADO ELIMINADA

            '--------------------------------------------------------------------------------------------------------

            'EVALUACION
            Dim eva As eva_cls = (From e In Data.eva_cls Where e.id_eva = opn.id_eva).First

            eva.id_P_0110 = 5 'ESTADO ANULADA

            '--------------------------------------------------------------------------------------------------------
            'DOCUMENTOS DE LA OPERACION
            Dim doctos = From d In Data.dsi_cls Where d.id_ope = ope_num

            For Each d In doctos
                d.id_P_0011 = 6 'ESTADO ELIMINADO
            Next

            '--------------------------------------------------------------------------------------------------------

            Data.SubmitChanges()

            Return True



        Catch ex As Exception
            Return False
        End Try



    End Function

    Public Function cabecera_documentos_guarda(ByVal dsi As dsi_cls, ByVal rut As Long, ByVal tipdoc As Integer, ByVal tipo As String, ByVal CUOTA As String) As Boolean

        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim monto As Double
        Dim monto_ope As Double
        Dim cant As Integer

        If CUOTA = "S" And tipo = "M" Then

            Dim cn = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                And ds.ope_cls.opn_cls.eva_cls.cli_idc = rut _
                                                And ds.id_ope = dsi.id_ope _
                                                And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13

            'And ds.dsi_flj_num = 0 _


            If cn.Count > 0 Then

                Dim cns = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                And ds.ope_cls.opn_cls.eva_cls.cli_idc = rut _
                                                And ds.dsi_flj_num <> 0 _
                                                And ds.id_ope = dsi.id_ope _
                                                And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13

                For Each p In cns
                    monto = monto + p.dsi_mto
                Next

                'For Each p In cn
                '    p.dsi_mto = monto
                '    p.dsi_mto_fin = monto
                'Next

                'Data.SubmitChanges()

            End If

        ElseIf CUOTA = "S" And tipo = "I" Then

            Data.Refresh(RefreshMode.KeepChanges)

            Dim cnslt = From ds In Data.dsi_cls Where ds.dsi_num = dsi.dsi_num _
                                                And ds.ope_cls.opn_cls.id_P_0031 = tipdoc _
                                                And ds.ope_cls.opn_cls.eva_cls.cli_idc = rut _
                                                And ds.dsi_flj_num = 0 _
                                                And ds.id_ope = dsi.id_ope _
                                                And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13


            If cnslt.Count > 0 Then

                Return False
                Exit Function

            End If

            dsi.dsi_flj_num = 0
            dsi.dsi_flj = "S"
            dsi.id_dsi = Nothing

            Data.dsi_cls.InsertOnSubmit(dsi)
            Data.SubmitChanges()

        End If

        monto = 0

        Dim ope = From op In Data.ope_cls _
                  Where op.id_ope = dsi.id_ope _
                  Select op.opn_cls.opn_can_doc, _
                         op.opn_cls.opn_mto_doc

        For Each p In ope
            monto_ope = p.opn_mto_doc
            cant = p.opn_can_doc
        Next

        Dim dsi2 = From ds In Data.dsi_cls _
                            Where ds.id_ope = dsi.id_ope _
                            And ds.id_P_0011 <> 6 And ds.id_P_0011 <> 13
        'And ds.dsi_flj_num = 0 _

        For Each p In dsi2
            monto = monto + p.dsi_mto
        Next

        If monto = monto_ope And cant = dsi2.Count Then

            Dim opera = From oper In Data.ope_cls Where oper.id_ope = dsi.id_ope
            For Each p In opera
                p.ope_cdo = "S"
            Next

        Else

            Dim opera = From oper In Data.ope_cls Where oper.id_ope = dsi.id_ope
            For Each p In opera
                p.ope_cdo = "N"
            Next

        End If

        Data.SubmitChanges()

    End Function

    Public Function cxc_cxp_egr_asocia_por_operacion(ByVal tipo As Variables.TipoDocumentoAsociar, Optional ByVal egr As sim_egr_cls = Nothing, Optional ByVal cxc As sim_cxc_cls = Nothing, Optional ByVal cxp As sim_cxp_cls = Nothing) As Boolean

        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            If tipo = 1 Then 'Egreso

                data.sim_egr_cls.InsertOnSubmit(egr)
                data.SubmitChanges()
                Return True
            ElseIf tipo = 2 Then ' Cuenta por Cobrar

                data.sim_cxc_cls.InsertOnSubmit(cxc)
                data.SubmitChanges()
                Return True
            ElseIf tipo = 3 Then ' Cuenta por Pagar

                data.sim_cxp_cls.InsertOnSubmit(cxp)
                data.SubmitChanges()
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function egreso_inserta(ByVal egr As egr_cls, ByVal egr_sec As Collection) As Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext
        Dim col As New Collection
        Try
            data.egr_cls.InsertOnSubmit(egr)
            data.SubmitChanges()

            For i = 1 To egr_sec.Count
                Dim egs As New egr_sec_cls

                egs = egr_sec.Item(i)
                egr_sec.Item(i).id_egr = egr.id_egr

                data.egr_sec_cls.InsertOnSubmit(egs)
                data.SubmitChanges()

                col.Add(egs)


            Next



            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function AnticipoDevuelvePorLinea(ByVal Linea As Integer, ByVal TipoDocto As Integer) As apc_cls


        '*********************************************************************************************************************************
        'Descripcion: Devuelve los anticipos que ha realizado el cliente por una linea de credito
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext




            Dim Anticipos As apc_cls = (From A In Data.apc_cls Where A.id_ldc = Linea And _
                                                                     A.id_P_0031 = TipoDocto _
                                        Select A).First()


            Return Anticipos


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Simulación_Anula(ByVal ope_num As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Anula las operaciones 
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 30/01/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'P.Gatica                    13/03/2009       Se agrega reversa a las cuentas y egresos Generados por la Operación
        'J.lagos                     15/06/2012       Se agrega eliminacion de cxc por concepto de GMF

        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim operacion As New ope_cls
            Dim col As New Collection
            Dim sql As New FuncionesGenerales.SqlQuery

            Dim OpeAnt As ope_cls = (From Ope In Data.ope_cls Where Ope.id_ope = ope_num And Ope.id_P_0030 = 2 And Ope.opn_cls.id_P_0082 = 3).First

            'For Each p In OpeAnt
            'Vuelve la Operación a Digitada y la Negociación a Asociada
            OpeAnt.id_P_0030 = 1
            OpeAnt.opn_cls.id_P_0082 = 2
            OpeAnt.opn_cls.eva_cls.id_P_0110 = 2

            'Next

            Data.SubmitChanges()

            Try

                Dim strsql As String = ""

                strsql = "delete from apb where id_nnc in (select id_nnc  from nnc where ID_OPN = " & OpeAnt.id_opn & ")"
                sql.ExecuteNonQuery(strsql)

                strsql = "delete from nnc where ID_OPN = " & OpeAnt.id_opn & ""
                sql.ExecuteNonQuery(strsql)

                'Dim op = From ope In Data.ope_cls Where ope.id_ope = ope_num Select ope.id_opn

                'For Each p In op

                '    'Busca datos del Nub Clasificación , Neg para borrar las clasificaciones
                '    'Eliminamos la asociacion de clasificacion con la negociacion
                '    Dim NubNeg = From nnc In Data.nnc_cls Where nnc.id_opn = p

                '    For Each d In NubNeg

                '        Try
                '            Dim ap = From apb In Data.apb_cls Where apb.id_nnc = d.id_nnc

                '            Data.apb_cls.DeleteAllOnSubmit(ap)
                '            Data.SubmitChanges()

                '        Catch ex As Exception

                '        End Try
                '        'Busca datos de las aprobaciones , para borrar 


                '    Next
                '    Data.nnc_cls.DeleteAllOnSubmit(NubNeg)
                '    Data.SubmitChanges()
                'Next


            Catch ex As Exception

            End Try

            Try

                'Busca Egresos Asociados a Operación
                Dim egresos = From egr In Data.egr_cls Join s In Data.sim_egr_cls On egr.id_egr Equals s.id_egr _
                              Where s.id_ope = ope_num Select egr

                If Not IsNothing(egresos) Then

                    For Each egr In egresos

                        'Trae la secuencia de egresos
                        Dim egr_sec = From e In Data.egr_sec_cls Where e.id_egr = egr.id_egr

                        For Each p In egr_sec

                            Try
                                Dim id_ing As Integer

                                'busca si tiene pago ese egresos
                                Dim ing_sec = From i In Data.ing_sec_cls Where i.id_egr_sec = p.id_egr_sec

                                For Each x In ing_sec
                                    id_ing = x.id_ing
                                    Exit For
                                Next

                                Data.ing_sec_cls.DeleteAllOnSubmit(ing_sec)
                                Data.SubmitChanges()

                                Dim ing As ing_cls = (From d In Data.ing_cls Where d.id_ing = id_ing).First()

                                Data.ing_cls.DeleteOnSubmit(ing)
                                Data.SubmitChanges()

                            Catch ex As Exception

                            End Try

                        Next


                        Dim sim_egr = From s In Data.sim_egr_cls Where s.id_ope = ope_num
                        Dim egreso = From s In Data.egr_cls Where s.id_egr = egr.id_egr

                        'Borra Nub de Egresos asociados a la Operación
                        Data.sim_egr_cls.DeleteAllOnSubmit(sim_egr)

                        'Borra Detalle egresos asociados a la Operación
                        Data.egr_sec_cls.DeleteAllOnSubmit(egr_sec)

                        'Borra egresos asociados a la Operación
                        Data.egr_cls.DeleteAllOnSubmit(egreso)

                        Data.SubmitChanges()

                    Next

                End If

            Catch ex As Exception

            End Try

            Try

                'Borra CXP de Egresos asociados a la Operación
                Dim sim_cxp = From s In Data.sim_cxp_cls Where s.id_ope = ope_num

                Data.sim_cxp_cls.DeleteAllOnSubmit(sim_cxp)
                ' Data.SubmitChanges()

                For Each p In sim_cxp


                    Dim cxp As cxp_cls = (From c In Data.cxp_cls Where c.id_cxp = p.id_cxp Select c).First

                    Data.cxp_cls.DeleteOnSubmit(cxp)
                    Data.SubmitChanges()

                Next

            Catch ex As Exception

            End Try

            'Borra CXC de Egresos asociados a la Operación
            Try

                Dim sim_cxc = From s In Data.sim_cxc_cls Where s.id_ope = ope_num

                Data.sim_cxc_cls.DeleteAllOnSubmit(sim_cxc)
                '  Data.SubmitChanges()

                For Each p In sim_cxc


                    Dim cxc As cxc_cls = (From c In Data.cxc_cls Where c.id_cxc = p.id_cxc Select c).First

                    Data.cxc_cls.DeleteOnSubmit(cxc)

                Next

                Data.SubmitChanges()

            Catch ex As Exception

            End Try



            Return True



        Catch ex As Exception

        End Try

    End Function

    Public Function OtorgamiendoGuarda(ByVal Opo As opo_cls, ByVal id_Ope As Integer, ByVal TipoOperacion As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Guarda el otorgamiento de una operacion con sus doctos correspondiente (ope -> opo; dsi ->doc).
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 03/11/2008
        'Quien Modifica              Fecha              Descripcion
        '--------------------------------------------------------------------------------------------------------------------------------------------------
        'JLagos                     13-03-2009          Se agrega modificacion de tablas de traspaso de la simulacion.
        'SHENRIQUEZ                 17-07-2012          Se agrega procedimiento almacenado para creacion y asignacion de contratos
        'JLagos                     06-08-2012          Se quita la generacion de nomina para el egreso, se debe hacer manual
        '**************************************************************************************************************************************************

        'Dim ts = New TransactionScope
        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Dim rutcliente As String
        Dim sql As New FuncionesGenerales.SqlQuery

        Try

            'Using ts

            Dim opo_cnsl As opo_cls

            Try

                opo_cnsl = (From o In Data.opo_cls Where o.id_ope = id_Ope Select o).First()

                'solo le cambiamos de estado a la operacion y negociacion
                opo_cnsl.ope_cls.id_P_0030 = 3
                opo_cnsl.ope_cls.opn_cls.id_P_0082 = 4

                Data.SubmitChanges()
                'ts.Complete()

                _MensajeOperacion = "Operación Otorgada Exitosamente (Solo se cambio el estado a la operación)"

                Return True

            Catch ex As Exception


            End Try

            Data.opo_cls.InsertOnSubmit(Opo)

            Data.SubmitChanges()

            Dim NroOpo As Integer = Opo.id_opo

            '------------------------------------------------------------------------------------------------------
            'Cambia el estado de Operacion Simulada a Otorgada
            '------------------------------------------------------------------------------------------------------
            Dim Ope As ope_cls

            Try


                Ope = (From O In Data.ope_cls Where O.id_ope = Opo.id_ope And O.id_P_0030 = 2).First

                Ope.id_P_0030 = 3
                Ope.ope_fec_anl = Nothing

            Catch ex As Exception
                _MensajeOperacion = "Operación no se puede otorgar por estar en estado: " & Ope.P_0030_cls.pnu_des
                Return False
            End Try

            '------------------------------------------------------------------------------------------------------
            Try

                Dim chq = From c In Data.chr_cls Where c.id_ope = Ope.id_ope

                For Each x In chq
                    x.id_P_0113 = 3
                Next

                Data.SubmitChanges()

            Catch ex As Exception

            End Try


            sql.ExecuteNonQuery("Exec sp_otorga_documentos " & id_Ope & ", " & NroOpo & ", " & Ope.opn_cls.eva_cls.cli_cls.id_suc)

            '------------------------------------------------------------------------------------------------------
            'Cambia el estado de la Negociacion a Cursada
            '------------------------------------------------------------------------------------------------------
            Dim Opn As opn_cls = (From O In Data.opn_cls Where O.id_opn = Ope.id_opn And O.id_P_0082 = 3).First

            Opn.id_P_0082 = 4 'Otorgada

            '------------------------------------------------------------------------------------------------------
            'Cambia el estado de la Evaluacio a Cursada
            '------------------------------------------------------------------------------------------------------
            Dim Eva As eva_cls = (From O In Data.eva_cls Where O.id_eva = Opn.id_eva And O.id_P_0110 = 3).First

            rutcliente = Eva.cli_idc
            Eva.id_P_0110 = 4 'Otorgada

            '------------------------------------------------------------------------------------------------------
            'Asigna numero de otorgamiento a Cuentas por Cobrar 
            '------------------------------------------------------------------------------------------------------
            Dim CuentasCobrar = From C In Data.sim_cxc_cls Where C.id_ope = Opo.id_ope Select C.cxc_cls

            For Each C In CuentasCobrar
                C.id_opo = Opo.id_opo
                C.id_P_0057 = 1 'Vigente
            Next

            '------------------------------------------------------------------------------------------------------
            'Asigna numero de otorgamiento a Cuentas por Pagar
            '------------------------------------------------------------------------------------------------------
            Dim CuentasPagar = From C In Data.sim_cxp_cls Where C.id_ope = Opo.id_ope Select C.cxp_cls

            For Each C In CuentasPagar
                C.id_opo = Opo.id_opo
                C.cli_idc = Opo.ope_cls.opn_cls.eva_cls.cli_idc
                C.cxp_fac_cam = Opo.ope_cls.ope_fac_cam
                C.id_P_0057 = 1 'Vigente
                C.cxp_fec = Date.Now
            Next

            '------------------------------------------------------------------------------------------------------
            'le asignamos el numero de otorgamiento para todos los egresos de la operacion
            '------------------------------------------------------------------------------------------------------

            Dim Egresos_Simulados = From C In Data.sim_egr_cls Where C.id_ope = Opo.id_ope Select C.egr_cls

            For Each Egr In Egresos_Simulados

                Dim Egresos = From E In Data.egr_cls Where E.id_egr = Egr.id_egr

                For Each e In Egresos
                    e.id_opo = Opo.id_opo
                    e.egr_fec = Date.Now
                Next
            Next

            Data.SubmitChanges()

            '------------------------------------------------------------------------------------------------------
            'Buscamos los egreso de la operacion para cambiar Estado de Ingresos cuando es comision y sin giro
            '------------------------------------------------------------------------------------------------------

            Dim NroEgreso = From E In Data.egr_sec_cls Where E.egr_cls.id_opo = Opo.id_opo _
                                                         And E.id_P_0055 = 4 _
                                                         And E.id_P_0056 = 5 _
                                                         And E.egr_vld_rcz <> "A"

            For Each E In NroEgreso

                'Actualiza estado de  los ingresos que cumplen la condicion en que caso que existan ingresos
                Dim Ingresos = From I In Data.ing_sec_cls Where I.id_egr_sec = E.id_egr_sec

                For Each I In Ingresos
                    I.ing_vld_rcz = "L"
                Next

            Next

            '------------------------------------------------------------------------------------------------------
            'Liberamos todos los egresos
            '------------------------------------------------------------------------------------------------------

            Dim EgresosEstadoValidacion = From E In Data.egr_sec_cls Where E.egr_cls.id_opo = Opo.id_opo

            For Each E In EgresosEstadoValidacion
                E.egr_vld_rcz = "L"
            Next

            '------------------------------------------------------------------------------------------------------
            'Cambiamos el estado del Egreso cuando se paga cxc, doc, gastos y comision, sin giro
            '------------------------------------------------------------------------------------------------------
            Dim Egresos1 = From E In Data.egr_sec_cls Where E.egr_cls.id_opo = Opo.id_opo _
                           And E.id_P_0053 >= 1 _
                           And E.id_P_0053 <= 4 _
                           And E.id_P_0056 = 5 _
                           And E.egr_vld_rcz <> "A"

            For Each Egr In Egresos1
                Egr.egr_vld_rcz = "V"
            Next

            '------------------------------------------------------------------------------------------------------
            'Actualiza el monto ocupado de la linea de credito
            '------------------------------------------------------------------------------------------------------

            'Dim cmc As New ClaseComercial
            'Dim Responsabilidad As Char = (From o In Data.opn_cls Where o.id_opn = Ope.id_opn Select o.opn_res_son).First

            'If Responsabilidad = "1" Then

            '    Dim Monto_Anticipar As Double

            '    If Ope.opn_cls.id_P_0023 = 1 Then
            '        Monto_Anticipar = Ope.ope_mto_ant * 1
            '    Else
            '        Monto_Anticipar = Ope.ope_mto_ant * Ope.ope_fac_cam
            '    End If

            '    Dim LineaCredito = From L In Data.ldc_cls Where L.cli_idc = Ope.opn_cls.eva_cls.cli_idc And L.id_P_0029 = 1

            '    For Each L In LineaCredito

            '        Dim Mto_Ocp As Double

            '        If IsNothing(L.ldc_mto_ocp) Then
            '            Mto_Ocp = 0
            '        Else
            '            Mto_Ocp = L.ldc_mto_ocp
            '        End If

            '        L.ldc_mto_ocp = Mto_Ocp + Monto_Anticipar

            '    Next

            'End If


            '------------------------------------------------------------------------------------------------------
            'Si no ha ocurrido ningun problema confirmamos los cambios sobre la BD
            '------------------------------------------------------------------------------------------------------

            Data.SubmitChanges()

            '------------------------------------------------------------------------------------------------------
            'Asignamos id documento a CXC y CXP simuladas
            '------------------------------------------------------------------------------------------------------

            Dim doc_aux As Integer = (From d In Data.doc_cls Where d.dsi_cls.id_ope = id_Ope Select d.id_doc).First

            Dim cxc_o = From l In Data.cxc_cls Join s In Data.sim_cxc_cls On s.id_cxc Equals l.id_cxc _
                                  Where l.opo_cls.id_ope = id_Ope Select l

            For Each c As cxc_cls In cxc_o
                c.id_doc = doc_aux
            Next

            Dim cxp_o = From l In Data.cxp_cls Join s In Data.sim_cxp_cls On s.id_cxp Equals l.id_cxp _
                                  Where l.opo_cls.id_ope = id_Ope Select l
            For Each c As cxp_cls In cxp_o
                c.id_doc = doc_aux
            Next

            Data.SubmitChanges()

            'ts.Complete()


            'End Using

        Catch ex As Exception
            'ts.Dispose()
            _MensajeOperacion = "Error en transacción: " & ex.Message
            Return False
        End Try

        Try

            '------------------------------------------------------------------------------------------------------
            'Se ejecuta procedimiento almacenado para creacion y asignacion de contratos
            '------------------------------------------------------------------------------------------------------
            Data.sp_prc_asignacontrato()

        Catch ex As Exception
            _MensajeOperacion = "Error en asignación de contratos: " & ex.Message
        End Try

        Try

            'jlagos 25-07-2012 -se agrega la rebaja automatica de saldos
            'Data.sp_op_cierre_cliente(rutcliente, _
            '                          rutcliente, _
            '                          DateTime.Now())
            sql.ExecuteNonQuery("Exec sp_op_cierre_cliente '" & rutcliente & "', '" & rutcliente & "', '" & DateTime.Now.ToString("yyyMMdd") & "'")

        Catch ex As Exception
            _MensajeOperacion = "Error en cierre cliente: " & ex.Message
        End Try

        _MensajeOperacion = "Operación Otorgada Exitosamente"

        Return True



    End Function

#Region "Cheques"

    Public Function cheques_respaldo_Ingresa(ByVal chr As chr_cls) As Boolean

        Dim Data As New CapaDatos.DataClsFactoringDataContext
        Try


            Data.chr_cls.InsertOnSubmit(chr)
            Data.SubmitChanges()
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Function cheques_respaldo_asocia(ByVal nrd As nrd_cls) As Boolean

        Dim Data As New CapaDatos.DataClsFactoringDataContext

        Try

            Using ts = New TransactionScope

                Data.nrd_cls.InsertOnSubmit(nrd)
                Data.SubmitChanges()


                Dim chr = From c In Data.chr_cls Where c.id_chr = nrd.id_chr Select c

                For Each p In chr
                    p.id_P_0113 = 2
                Next


                Dim dsi = From d In Data.dsi_cls Where d.id_dsi = nrd.id_dsi

                For Each p In dsi
                    p.dsi_est_rsp = "S"
                Next

                Data.SubmitChanges()

                ts.Complete()

            End Using

            Return True

        Catch ex As Exception

            Return False
        End Try




    End Function

#End Region

#Region "Abono Anticipo"

    Public Function CXC_AbonoAnticipo_Elimina(ByVal RutCliente As Long, ByVal id_cxc As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Elimina las Cuentas por Abono de un Cliente
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 10/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim val As Double

            Dim data As New CapaDatos.DataClsFactoringDataContext




            Dim Ant As cxc_cls = (From C In data.cxc_cls Where C.id_cxc = id_cxc).First

            data.cxc_cls.DeleteOnSubmit(Ant)


            Dim cxp As cxp_cls = (From d In data.cxp_cls Where d.cxp_fec = Ant.cxc_fec And d.cxp_mto = Ant.cxc_mto And d.id_P_0023 = Ant.id_P_0023).First

            data.cxp_cls.DeleteOnSubmit(cxp)

            Dim EGR_sec As egr_sec_cls = (From E In data.egr_sec_cls _
                              Where E.id_cxp = cxp.id_cxp).First



            Dim egr As egr_cls = (From e In data.egr_cls Where e.id_egr = EGR_sec.id_egr).First



            data.egr_sec_cls.DeleteOnSubmit(EGR_sec)
            data.SubmitChanges()


            data.egr_cls.DeleteOnSubmit(egr)
            data.SubmitChanges()


            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function abonoanticipo_guarda(ByVal cxc As cxc_cls, ByVal cxp As cxp_cls, ByVal egr As egr_cls, ByVal egr_sec As egr_sec_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Guarda Abono anticipo y sus respectivos egresos
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 26/12/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim data As New CapaDatos.DataClsFactoringDataContext

        data.cxp_cls.InsertOnSubmit(cxp)
        data.SubmitChanges()


        data.cxc_cls.InsertOnSubmit(cxc)
        data.SubmitChanges()

        data.egr_cls.InsertOnSubmit(egr)
        data.SubmitChanges()

        egr_sec.id_egr = egr.id_egr
        egr_sec.id_cxp = cxp.id_cxp
        data.egr_sec_cls.InsertOnSubmit(egr_sec)
        data.SubmitChanges()

        Dim Nma As New nma_cls

        Nma.nma_fec = Date.Now
        Nma.nma_fec_dep = Date.Now ' Txt_FechaDeposito.Text
        Nma.nma_mto = egr_sec.egr_mto 'mto_egreso
        Nma.nma_tot_dpo = 1 'Cantidad
        Nma.nma_ioe = "E"
        Nma.id_eje_rpb = CodEje
        Nma.id_eje_dep = CodEje
        Nma.id_bco = Nothing

        Dim DPO As New dpo_cls

        DPO.dpo_num = 1


        DPO.id_bco = Nothing 'DP_Banco.SelectedValue
        DPO.id_PL_000047 = Nothing 'DP_PlazaBanco.SelectedValue
        DPO.id_P_0052 = 1
        DPO.id_P_0054 = 4
        DPO.id_P_0023 = 1
        DPO.dpo_fec_emi = Date.Now
        DPO.dpo_fev = Date.Now
        DPO.dpo_cct = 1 ' Siempre 1
        DPO.id_P_0087 = Nothing ' nothing
        DPO.dpo_aor = "Cliente" 'Cliente
        DPO.dpo_num = 1 '1
        DPO.dpo_mto = egr_sec.egr_mto 'Mto_egreso

        Dim col As New Collection
        col.Add(egr_sec)
        'Genera nomina y Dpo , ademas libera el egreso

        ag.NominaEgreso_Inserta(Nma, DPO, col)

        'Inserta movimiento a Contabilidad
        '@pDblRutCli         numeric(10,0),
        '@pDblMtoGir          numeric(15,2),
        '@pintCodBco          int,
        '@pVcharCtaCte        varchar(20),
        '@pcharFormaPago      char(1),
        '@pstrNroOpe          decimal,
        '@pintTipoEgreso      varchar(5),
        '@pintFormaPago      varchar(250)= ''

        'data.sp_contab_InsertaMovimientoFactoring(CInt(cxp.cli_idc), _
        '                                          (egr_sec.egr_mto * cg.ParidadDevuelve(cxp.id_P_0023, cxp.cxp_fec).par_val), _
        '                                          egr_sec.id_bco, _
        '                                          egr_sec.egr_cta_cte.ToString, _
        '                                          "C", _
        '                                          0, _
        '                                          400, _
        '                                          egr_sec.P_0056_cls.pnu_des)


    End Function

#End Region


#End Region

    Public Sub CuotaDocumentos(ByVal id_ope As Integer)

        Dim Data As New CapaDatos.DataClsFactoringDataContext

        Dim operacion As ope_cls = (From o In Data.ope_cls Where o.id_ope.Equals(id_ope)).First()
        Dim dsi = From d In Data.dsi_cls Where d.id_ope.Equals(id_ope)

        If operacion.ope_cuo = "S" Then

            Dim RST As Integer = 0

            For Each d In dsi

                RST = Documentos_cuota_valida(d.dsi_num, _
                                              operacion.opn_cls.eva_cls.cli_idc, _
                                              "S", _
                                              operacion.opn_cls.id_P_0031)

                If RST <> 999 And RST <> 998 Then
                    d.dsi_flj_num = RST
                End If

            Next

            Data.SubmitChanges()

        End If

    End Sub

    Public Sub Guarda_dsi_masivo_query(ByVal col As Collection)

        '    '**************************************************************************************************************************************************
        '    'Descripcion: Guarda calificacion segun numero de contrato asignado 
        '    'Creado por Sebastian Henriquez C.
        '    'Fecha Creacion: 01/10/2012
        '    'Quien Modifica              Fecha              Descripcion
        '    '--------------------------------------------------------------------------------------------------------------------------------------------------

        Try
            Dim constring As String = System.Configuration.ConfigurationManager.ConnectionStrings("FACTORConnectionString").ConnectionString
            Dim con As New SqlConnection(constring)


            For i = 1 To col.Count

                Dim sqc As New SqlCommand(col.Item(i), con)
                con.Open()
                sqc.ExecuteNonQuery()
                con.Close()
            Next

            con.Close()

        Catch ex As Exception

        End Try

    End Sub

    Public Function ValidaCalculosDeOperacion(ByVal id_opn As Integer) As Boolean

        Try

            Dim Sql As New FuncionesGenerales.SqlQuery
            Dim ds As DataSet

            ds = Sql.ExecuteDataSet("Exec sp_validaCalculosOperacion " & id_opn & " ")

            If ds.Tables(0).Rows.Count > 0 Then
                Return False 'calculos errados
            Else
                Return True 'calculos correctos
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
