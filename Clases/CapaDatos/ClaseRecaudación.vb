Imports Microsoft.VisualBasic
Imports CapaDatos
Public Class ClaseRecaudación

#Region "Consultas Recaudación"

    Public Function hre_DiasInt_devuelve() As Integer
        'Descripcion: Devuelve cantidad de dias devolucion interes
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 12/05/2012
        'Quien Modifica              Fecha              Descripcion

        Try

            Dim data As New DataClsFactoringDataContext

            Dim dias = (From i In data.sis_cls Select i.sis_dia_dev).First


            If Not IsNothing(dias) Then
                Return dias
            Else
                Return 0
            End If

        Catch ex As Exception

            Return 0

        End Try

    End Function

    Public Function nce_fnc_devuelve(ByVal cli_idc1 As String, ByVal cli_idc2 As String, _
                                     ByVal factoring As String, _
                                     ByVal fecha_dde As Date, ByVal fecha_hta As Date, _
                                     ByVal est_nom1 As String, ByVal est_nom2 As String, _
                                     Optional ByVal tipo As Integer = 0) As Collection

        'Descripcion: Devuelve documentos no cedidoc segun cliente y factoring
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/06/2009
        'Quien Modifica              Fecha              Descripcion
        'P GATICA                    22/06/2009         Solo Muestra los que han sido Pagados

        Try

            Dim data As New DataClsFactoringDataContext

            If factoring <> "0" And tipo = 0 Then

                Dim col As New Collection

                Dim dn = From i In data.ing_sec_cls Where i.nce_cls.cli_idc >= cli_idc1 And i.nce_cls.cli_idc <= cli_idc2 And _
                                                          i.nce_cls.id_PL_000069 = factoring And _
                                                          i.nce_cls.nce_fec_gen >= fecha_dde And i.nce_cls.nce_fec_gen <= fecha_hta And _
                                                          i.nce_cls.nce_est_nom >= est_nom1 And i.nce_cls.nce_est_nom <= est_nom2 _
                          Select _
                         i.nce_cls.cli_idc, _
                         Cliente = If(i.nce_cls.cli_cls.id_P_0044 = 1, _
                                      i.nce_cls.cli_cls.cli_rso.Trim & " " & i.nce_cls.cli_cls.cli_ape_ptn.Trim & " " & i.nce_cls.cli_cls.cli_ape_mtn.Trim, _
                                      i.nce_cls.cli_cls.cli_rso.Trim), _
                         i.nce_cls.deu_ide, _
                  deu_rso = i.nce_cls.deu_cls.deu_rso + " " + i.nce_cls.deu_cls.deu_ape_ptn + " " + i.nce_cls.deu_cls.deu_ape_mtn, _
                  i.nce_cls.PL_000069_cls.pal_des, _
                  i.nce_cls.fac_cam, _
                  i.nce_cls.id_hre, _
                  i.nce_cls.nce_num_doc, _
                  i.nce_cls.nce_mto, _
                  i.nce_cls.id_p_0023, _
                  moneda = i.nce_cls.P_0023_cls.pnu_des, _
                  i.nce_cls.nce_fec_vcto, _
                  i.nce_cls.nce_obs, _
                  i.nce_cls.id_nce, _
                  i.nce_cls.id_p_0031, _
                  tipo_docto = i.nce_cls.P_0031_cls.pnu_atr_003, _
                  NCE_FEC_ING = If(i.nce_cls.nce_fec_ing = Nothing, CDate("01/01/1900"), i.nce_cls.nce_fec_ing), _
                  i.id_ing


                For Each p In dn
                    col.Add(p)
                Next

                Return col

            ElseIf tipo = 0 And factoring = "0" Then

                Dim col As New Collection

                Dim dn = From i In data.ing_sec_cls Where i.nce_cls.cli_idc >= cli_idc1 And i.nce_cls.cli_idc <= cli_idc2 _
                          And i.nce_cls.nce_fec_gen >= fecha_dde And i.nce_cls.nce_fec_gen <= fecha_hta And i.nce_cls.nce_est_nom <> "N" _
                          Select _
                         i.nce_cls.cli_idc, _
                         Cliente = If(i.nce_cls.cli_cls.id_P_0044 = 1, _
                                      i.nce_cls.cli_cls.cli_rso.Trim & " " & i.nce_cls.cli_cls.cli_ape_ptn.Trim & " " & i.nce_cls.cli_cls.cli_ape_mtn.Trim, _
                                      i.nce_cls.cli_cls.cli_rso.Trim), _
                         i.nce_cls.deu_ide, _
                  deu_rso = i.nce_cls.deu_cls.deu_rso + " " + i.nce_cls.deu_cls.deu_ape_ptn + " " + i.nce_cls.deu_cls.deu_ape_mtn, _
                  i.nce_cls.PL_000069_cls.pal_des, _
                  i.nce_cls.fac_cam, _
                  i.nce_cls.id_hre, _
                  i.nce_cls.nce_num_doc, _
                  i.nce_cls.nce_mto, _
                  i.nce_cls.id_p_0023, _
                  moneda = i.nce_cls.P_0023_cls.pnu_des, _
                  i.nce_cls.nce_fec_vcto, _
                  i.nce_cls.nce_obs, _
                  i.nce_cls.id_nce, _
                  i.nce_cls.id_p_0031, _
                  tipo_docto = i.nce_cls.P_0031_cls.pnu_atr_003, _
                i.nce_cls.nce_fec_ing, _
                 i.id_ing


                For Each p In dn
                    col.Add(p)
                Next

                Return col

            ElseIf tipo = 1 Then

                Dim col As New Collection

                Dim dn = From i In data.ing_sec_cls Where i.nce_cls.cli_idc >= cli_idc1 And i.nce_cls.cli_idc <= cli_idc2 And _
                                                          i.nce_cls.id_PL_000069 = factoring And _
                                                          i.nce_cls.nce_fec_gen >= fecha_dde And i.nce_cls.nce_fec_gen <= fecha_hta And _
                                                          i.nce_cls.nce_est_nom = "N" _
                         Select _
                        i.nce_cls.cli_idc, _
                        Cliente = If(i.nce_cls.cli_cls.id_P_0044 = 1, _
                                     i.nce_cls.cli_cls.cli_rso.Trim & " " & i.nce_cls.cli_cls.cli_ape_ptn.Trim & " " & i.nce_cls.cli_cls.cli_ape_mtn.Trim, _
                                     i.nce_cls.cli_cls.cli_rso.Trim), _
                        i.nce_cls.deu_ide, _
                 deu_rso = i.nce_cls.deu_cls.deu_rso + " " + i.nce_cls.deu_cls.deu_ape_ptn + " " + i.nce_cls.deu_cls.deu_ape_mtn, _
                 i.nce_cls.PL_000069_cls.pal_des, _
                 i.nce_cls.fac_cam, _
                 i.nce_cls.id_hre, _
                 i.nce_cls.nce_num_doc, _
                 i.nce_cls.nce_mto, _
                 i.nce_cls.id_p_0023, _
                 moneda = i.nce_cls.P_0023_cls.pnu_des, _
                 i.nce_cls.nce_fec_vcto, _
                 i.nce_cls.nce_obs, _
                 i.nce_cls.id_nce, _
                 i.nce_cls.id_p_0031, _
                 tipo_docto = i.nce_cls.P_0031_cls.pnu_atr_003, _
               NCE_FEC_ING = If(i.nce_cls.nce_fec_ing = Nothing, CDate("01/01/1900"), i.nce_cls.nce_fec_ing), _
                i.id_ing





                For Each p In dn
                    col.Add(p)
                Next

                Return col



            ElseIf tipo = 2 Then

                Dim col As New Collection

                Dim dn = From i In data.ing_sec_cls Where i.nce_cls.cli_idc >= cli_idc1 And i.nce_cls.cli_idc <= cli_idc2 _
                         And i.nce_cls.nce_fec_gen >= fecha_dde And i.nce_cls.nce_fec_gen <= fecha_hta And i.nce_cls.nce_est_nom = "N" _
                         Select _
                        i.nce_cls.cli_idc, _
                        Cliente = If(i.nce_cls.cli_cls.id_P_0044 = 1, _
                                     i.nce_cls.cli_cls.cli_rso.Trim & " " & i.nce_cls.cli_cls.cli_ape_ptn.Trim & " " & i.nce_cls.cli_cls.cli_ape_mtn.Trim, _
                                     i.nce_cls.cli_cls.cli_rso.Trim), _
                        i.nce_cls.deu_ide, _
                 deu_rso = i.nce_cls.deu_cls.deu_rso + " " + i.nce_cls.deu_cls.deu_ape_ptn + " " + i.nce_cls.deu_cls.deu_ape_mtn, _
                 i.nce_cls.PL_000069_cls.pal_des, _
                 i.nce_cls.fac_cam, _
                 i.nce_cls.id_hre, _
                 i.nce_cls.nce_num_doc, _
                 i.nce_cls.nce_mto, _
                 i.nce_cls.id_p_0023, _
                 moneda = i.nce_cls.P_0023_cls.pnu_des, _
                 i.nce_cls.nce_fec_vcto, _
                 i.nce_cls.nce_obs, _
                 i.nce_cls.id_nce, _
                 nce = If(i.nce_cls.id_PL_000069 = Nothing, "0", i.nce_cls.id_PL_000069), _
                i.nce_cls.id_p_0031, _
                tipo_docto = i.nce_cls.P_0031_cls.pnu_atr_003, _
              NCE_FEC_ING = If(i.nce_cls.nce_fec_ing = Nothing, CDate("01/01/1900"), i.nce_cls.nce_fec_ing), _
               i.id_ing





                For Each p In dn
                    If p.nce = Nothing Then

                        col.Add(p)
                    End If
                Next

                Return col

            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function documentos_a_recaudar_devuelve(ByVal nro_hoja As Integer) As Collection
        Try

            Dim col As New Collection
            Dim data As New DataClsFactoringDataContext

            Dim doc = From c In data.drc_cls Where c.hre_cls.id_hre = nro_hoja Select _
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


            Return col


        Catch ex As Exception
            Return Nothing

        End Try




    End Function

    Public Function gastos_rec_retorna(ByVal id_hr As Integer) As Collection

        ' retorna gastos asociados a una Hoja de Recaudación y un deudor
        ' P.Gatica
        ' 05/06/2009

        Try

            Dim col As New Collection
            Dim data As New DataClsFactoringDataContext

            Dim gto = From g In data.gga_cls Where g.id_hre = id_hr Select g.id_gga, _
                                                                            id_hre = id_hr, _
                                                                            g.deu_ide, _
                                                                            g.id_p_0051, _
                                                                            Deudor = g.deu_cls.deu_rso & " " & g.deu_cls.deu_ape_ptn & " " & g.deu_cls.deu_ape_mtn, _
                                                                            g.gga_rec_ext, _
                                                                            g.gga_vld, _
                                                                            g.gga_mto, _
                                                                            tipo_gasto = g.P_0051_cls.pnu_des



            For Each p In gto
                col.Add(p)
            Next

            Return col


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Doctos_asig_esp_retorna(ByVal rut_cli As String, ByVal rut_deu As String, _
                                            ByVal rut_deu1 As String, ByVal con_deudor As Boolean, _
                                            ByVal modulo As Integer, _
                                            ByVal fecha_dde As DateTime, ByVal fecha_hta As DateTime, _
                                            ByVal num_otg As Integer, ByVal num_otg1 As Integer, _
                                            ByVal num_doc As String, ByVal num_doc1 As String, _
                                            ByVal mto_des As Integer, ByVal mto_has As Integer, _
                                            Optional ByVal estado As Integer = 0, Optional ByVal tipo_doc As Integer = 0) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Documentos para asignar a Recaudador 
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 20/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'jlagos                     26-08-2010          se agrega estado 13 (anulado)
        '*********************************************************************************************************************************

        Dim Data As New DataClsFactoringDataContext

        Dim col As New Collection

        Dim est1 As Integer
        Dim est2 As Integer

        Dim tipo_dde As Integer
        Dim tipo_hta As Integer

        If modulo = 1 Then



            'Modulo Cobranza - Con Deudor

            Dim doc = From d In Data.doc_cls _
                      Where (d.dsi_cls.dsi_mto >= mto_des _
                      And d.dsi_cls.dsi_mto <= mto_has) And (d.dsi_cls.dsi_fev_rea >= fecha_dde And d.dsi_cls.dsi_fev_rea <= fecha_hta) _
                      And d.dsi_cls.dsi_flj = "N" And d.opo_cls.ope_cls.id_P_0030 = 3 _
                      And (d.opo_cls.opo_otg >= num_otg And d.opo_cls.opo_otg <= num_otg1) _
                      And (d.dsi_cls.dsi_num >= num_doc And d.dsi_cls.dsi_num <= num_doc1) _
                         And d.dsi_cls.id_P_0011 <> 5 _
                         And d.dsi_cls.id_P_0011 <> 3 _
                         And d.dsi_cls.id_P_0011 <> 13 _
                         And (d.dsi_cls.deu_ide >= rut_deu _
                         And d.dsi_cls.deu_ide <= rut_deu1) _
                         And d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                    Select d.dsi_cls.deu_ide, _
                    d.dsi_cls.deu_cls.deu_rso, _
                    d.dsi_cls.ope_cls.opn_cls.id_P_0031, _
                    d.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                    d.opo_cls.opo_otg, _
                    d.dsi_cls.dsi_num, _
                    d.dsi_cls.dsi_flj_num, _
                    d.dsi_cls.dsi_fev_rea, _
                    d.dsi_cls.dsi_mto, _
                    d.dsi_cls.id_P_0011, _
                    d.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                    Moneda_ope = d.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                    d.doc_sdo_cli, _
                    d.id_doc, _
                    d.dsi_cls.id_ope, _
                    d.doc_sdo_ddr, _
                      id_dor = If(((From dor In Data.dor_cls Where dor.id_doc = d.id_doc _
                                                             And dor.dor_est = "N" _
                                                             Select dor.id_dor).Count > 0), _
                                  (From dor In Data.dor_cls Where dor.id_doc = d.id_doc _
                                                              And dor.dor_est = "N" _
                                                              Select dor.id_dor).First, 0), _
                    d.id_cco

            For Each p In doc
                col.Add(p)
            Next

            Return col

        Else

            If estado = 0 Then
                est1 = 0
                est2 = 999
            Else
                est1 = estado
                est2 = estado
            End If

            If tipo_doc = 0 Then
                tipo_dde = 0
                tipo_hta = 999
            Else
                tipo_dde = tipo_doc
                tipo_hta = tipo_doc
            End If

            'Modulo Recaudación - Con Deudor

            If con_deudor Then

                Dim doc = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                        Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi _
                                     Where d.dsi_cls.dsi_mto >= mto_des _
                                        And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                        And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                        And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                        And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                           And dsi.id_P_0011 <> 5 _
                                           And d.dsi_cls.id_P_0011 <> 3 _
                                           And dsi.id_P_0011 <> 13 _
                                           And dsi.id_P_0011 >= est1 And dsi.id_P_0011 <= est2 _
                                           And dsi.ope_cls.opn_cls.id_P_0031 >= est1 And dsi.ope_cls.opn_cls.id_P_0031 <= est2 _
                                           And dsi.deu_ide = rut_deu _
                                           And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                                                                               Select d.id_doc, d.id_dsi

                For Each p In doc

                    Dim val As Integer

                    Try


                        Dim dr = From drc In Data.drc_cls Where drc.gsn_cls.id_doc = p.id_doc Select drc

                        If dr.Count = 0 Then


                            Dim doc1 = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                                 Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
                                 And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                 And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                 And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                 And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                    And dsi.id_P_0011 <> 5 _
                                    And d.dsi_cls.id_P_0011 <> 3 _
                                    And dsi.id_P_0011 <> 13 _
                                    And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                    And d.id_doc = p.id_doc _
                                    And d.dsi_cls.deu_ide = rut_deu _
                            Select dsi.deu_ide, _
                            dsi.deu_cls.deu_rso, _
                            dsi.ope_cls.opn_cls.id_P_0031, _
                            dsi.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            opo.opo_otg, _
                            dsi.dsi_num, _
                            dsi.dsi_flj_num, _
                            dsi.dsi_fev_rea, _
                            dsi.dsi_mto, _
                            dsi.id_P_0011, _
                            dsi.ope_cls.opn_cls.id_P_0023, _
                            Moneda_ope = dsi.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            d.doc_sdo_cli, _
                            dsi.id_ope, _
                            d.doc_sdo_ddr, _
                            d.id_doc, _
                            d.id_cco

                            For Each px In doc1
                                col.Add(px)
                            Next

                        End If

                    Catch ex As Exception
                        val = 1
                    End Try

                Next

                Return col

            Else
                'Modulo Recaudación - Sin Deudor
                Dim doc = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
             Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
             And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
             And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
             And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
             And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                And dsi.id_P_0011 <> 5 _
                And d.dsi_cls.id_P_0011 <> 3 _
                And dsi.id_P_0011 <> 13 _
                And dsi.id_P_0011 >= est1 And dsi.id_P_0011 <= est2 _
                And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
Select d.id_doc, d.id_dsi


                'And Not (From drc In Data.drc_cls Where drc.gsn_cls.id_doc = d.id_doc Select drc.gsn_cls.id_doc).First _

                For Each p In doc

                    Dim val As Integer


                    Try


                        Dim dr = From drc In Data.drc_cls Where drc.gsn_cls.id_doc = p.id_doc Select drc

                        If dr.Count = 0 Then


                            Dim doc1 = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                                 Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
                                 And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                 And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                 And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                 And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                    And dsi.id_P_0011 <> 5 _
                                    And d.dsi_cls.id_P_0011 <> 3 _
                                    And dsi.id_P_0011 <> 13 _
                                    And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                    And d.id_doc = p.id_doc _
                            Select dsi.deu_ide, _
                            dsi.deu_cls.deu_rso, _
                            dsi.ope_cls.opn_cls.id_P_0031, _
                            dsi.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            opo.opo_otg, _
                            dsi.dsi_num, _
                            dsi.dsi_flj_num, _
                            dsi.dsi_fev_rea, _
                            dsi.dsi_mto, _
                            dsi.id_P_0011, _
                            dsi.ope_cls.opn_cls.id_P_0023, _
                            Moneda_ope = dsi.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            d.doc_sdo_cli, _
                            dsi.id_ope, _
                            d.doc_sdo_ddr, _
                            d.id_doc, _
                            d.id_cco

                            For Each px In doc1
                                col.Add(px)
                            Next

                        End If

                    Catch ex As Exception
                        val = 1
                    End Try

                Next

                Return col

            End If



        End If
    End Function
   
    Public Function Recaudador_asignar_devuelve(ByVal todas As Integer, ByVal tipo_eje As Integer, ByVal cod_suc As String, ByVal fecha_rec As DateTime, ByVal am_pm As String, ByVal zona As Integer, ByVal cod_eje As Integer, Optional ByVal Pag As Integer = 999) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve la cantidad de documentos y deudores de un recaudador para una fecha determinada
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 15/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'A. Saldivar                 07/02/2011         Se agrega paginacion
        'S. Henriquez                09/08/2012         Se Corrigen las consultas
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim col As New Collection
            Dim sesion As New ClsSession.ClsSession


            If todas = 0 Then

                If zona = 0 Then

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And c.id_P0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       And nes.id_suc = cod_suc _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
 _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje _
                                             And dr.drc_est = "N" _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                               Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                                Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                                Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                                doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje _
                                                And dr.drc_est = "N" _
                                                Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
 _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion).Take(Pag)

                    For Each p In eje
                        col.Add(p)
                    Next


                Else

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And c.id_P0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Join s In Data.suc_cls On s.id_suc Equals nes.id_suc _
                                                                                       Join z In Data.zon_cls On z.id_suc Equals s.id_suc _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       And nes.id_suc = cod_suc _
                                                                                       And z.id_zon = zona _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
 _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Join ddi In Data.ddi_cls On ddi.id_ddi Equals g.id_ddi _
                                             Join cmn In Data.cmn_cls On ddi.id_cmn Equals cmn.id_cmn _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje And cmn.id_zon = zona _
                                             And dr.drc_est = "N" _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                               Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                                Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                                Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                                doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje _
                                                And dr.drc_est = "N" _
                                                Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
 _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion).Take(Pag)



                    For Each p In eje
                        col.Add(p)
                    Next



                End If

            Else
                If zona = 0 Then

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And c.id_P0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
 _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             h.id_eje = d.id_eje _
                                             And dr.drc_est = "N" _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                               Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                                Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                                Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                                h.id_eje = d.id_eje _
                                                And dr.drc_est = "N" _
                                                Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
 _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion).Take(Pag)

                    For Each p In eje
                        col.Add(p)
                    Next

                Else

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And c.id_P0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Join s In Data.suc_cls On s.id_suc Equals nes.id_suc _
                                                                                       Join z In Data.zon_cls On z.id_suc Equals s.id_suc _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       And z.id_zon = zona _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
 _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Join ddi In Data.ddi_cls On ddi.id_ddi Equals g.id_ddi _
                                             Join cmn In Data.cmn_cls On ddi.id_cmn Equals cmn.id_cmn _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             h.id_eje = d.id_eje And cmn.id_zon = zona _
                                             And dr.drc_est = "N" _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                               Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                                Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                                Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                                h.id_eje = d.id_eje _
                                                And dr.drc_est = "N" _
                                                Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
 _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion).Take(Pag)
                    For Each p In eje
                        col.Add(p)
                    Next




                End If
            End If

            If Not IsNothing(col) Then
                If col.Count > 0 Then
                    Return col
                Else
                    Return Nothing
                End If

            End If





        Catch ex As Exception

        End Try
    End Function



    Public Function Recaudador_asignar_devuelve_Rec(ByVal todas As Integer, ByVal tipo_eje As Integer, ByVal cod_suc As String, ByVal fecha_rec As DateTime, ByVal am_pm As String, ByVal zona As Integer, ByVal cod_eje As Integer) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve la cantidad de documentos y deudores de un recaudador para una fecha determinada
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 15/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'A. Saldivar                 07/02/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim col As New Collection
            Dim sesion As New ClsSession.ClsSession


            If todas = 0 Then

                If zona = 0 Then

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And c.id_P0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       And nes.id_suc = cod_suc _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
 _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje _
                                             And dr.drc_est = "N" _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                               Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                                Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                                Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                                doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje _
                                                And dr.drc_est = "N" _
                                                Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
 _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion_Recaudacion).Take(14)

                    For Each p In eje
                        col.Add(p)
                    Next


                Else

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje _
                                                                       And d.id_P_0045 = tipo_eje Select d.id_eje).First _
                                                                       And d.id_eje = (From nes In Data.nes_cls _
                                                                                       Where nes.id_eje = cod_eje _
                                                                                       And nes.id_suc = cod_suc _
                                                                                       Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                             Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Join ddi In Data.ddi_cls On ddi.id_ddi Equals g.id_ddi _
                                             Join cmn In Data.cmn_cls On ddi.id_cmn Equals cmn.id_cmn _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             dr.drc_est = "N" And _
                                             doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje And cmn.id_zon = zona _
                                             Select dr.id_drc).Distinct.Count, _
 _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                 Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc Join ddi In Data.ddi_cls On ddi.id_ddi Equals g.id_ddi _
                                             Join cmn In Data.cmn_cls On ddi.id_cmn Equals cmn.id_cmn _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                             dr.drc_est = "N" And _
                                 doc.id_suc_rcd = cod_suc And h.id_eje = d.id_eje Select doc.id_doc).Distinct.Count, _
                                 d.suc_cls.suc_des_cra, _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre _
                                  Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                  Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion_Recaudacion).Take(14)


                    For Each p In eje
                        col.Add(p)
                    Next



                End If

            Else
                If zona = 0 Then

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje And d.id_P_0045 = tipo_eje Select d.id_eje).First And d.id_eje = (From nes In Data.nes_cls Where nes.id_eje = cod_eje And nes.id_suc = cod_suc Select nes.id_eje).First _
                          Select d.id_eje, d.eje_nom, _
                                 Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                             Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                             Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                              h.id_eje = d.id_eje And dr.drc_est = "N" Select dr.id_drc).Distinct.Count, _
                                 Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                 Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                 h.id_eje = d.id_eje And dr.drc_est = "N" Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion_Recaudacion).Take(14)

                    For Each p In eje
                        col.Add(p)
                    Next



                Else

                    Dim eje = (From d In Data.eje_cls Where d.id_eje = (From c In Data.nef_cls Where c.id_eje = d.id_eje And d.id_P_0045 = tipo_eje Select d.id_eje).First And d.id_eje = (From nes In Data.nes_cls Where nes.id_eje = cod_eje And nes.id_suc = cod_suc Select nes.id_eje).First _
                    Select d.id_eje, d.eje_nom, _
                           Deudores = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                                       h.id_eje = d.id_eje And dr.drc_est = "N" Select dr.id_drc).Distinct.Count, _
                           Documentos = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                           Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc Where h.hre_fec = fecha_rec And h.hre_mna_tde = am_pm And _
                           h.id_eje = d.id_eje And dr.drc_est = "N" Select doc.id_doc).Distinct.Count, d.suc_cls.suc_des_cra, _
                    id_eje_cob = (From dr In Data.drc_cls Join h In Data.hre_cls On h.id_hre Equals dr.id_hre Join g In Data.gsn_cls On g.id_gsn Equals dr.id_gsn _
                                       Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc _
                                       Where dr.drc_est = "N" _
                                       Select g.id_eje_cob).First).Skip(sesion.NroPaginacion_Recaudacion).Take(14)

                    For Each p In eje
                        col.Add(p)
                    Next




                End If
            End If

            If Not IsNothing(col) Then
                If col.Count > 0 Then
                    Return col
                Else
                    Return Nothing
                End If

            End If





        Catch ex As Exception

        End Try
    End Function

    Public Function DocumentosOtorgagosPagos_Cantidad(ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                                            ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                                            ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                                            ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                                            ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                                            ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16, _
                                                            ByVal fec_vcto1 As DateTime, ByVal fec_vcto2 As DateTime, _
                                                            ByVal estado1 As Int16, ByVal estado2 As Int16, ByVal estado3 As Int16, _
                                                            ByVal estado4 As Int16, ByVal estado5 As Int16, ByVal estado6 As Int16, _
                                                            ByVal estado7 As Int16, ByVal estado8 As Int16, ByVal estado9 As Int16, _
                                                            ByVal estado10 As Int16, ByVal estado11 As Int16, ByVal estado12 As Int16, _
                                                            Optional ByVal NroAplicacion As Integer = 0) As Integer

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve la cantidad de Documentos que se pueden pagar 
        'Creado por Jorge Lagos
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Cantidad As Integer = 0

            Data.ObjectTrackingEnabled = False

            Dim Temporal_doc = From doc1 In Data.doc_cls _
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
                   doc1.dsi_cls.dsi_flj = "N" _
             Select doc1.id_doc, doc1.doc_sdo_cli, doc1.dsi_cls.ope_cls.opn_cls.id_P_0012

            For Each D In Temporal_doc

                Dim Ingresos = (From I In Data.ing_sec_cls Where I.id_doc = D.id_doc And _
                                                                           I.id_P_0053 = 2 And _
                                                                          (I.ing_vld_rcz = "S" Or _
                                                                             I.ing_vld_rcz = "I" Or _
                                                                             I.ing_vld_rcz = "V" Or _
                                                                             I.ing_vld_rcz = "C" Or _
                                                                             I.ing_vld_rcz = "L") And _
                                                                             I.ing_pro = "N" And _
                                                                             I.egr_sec_cls.egr_cls.id_apl <> NroAplicacion _
                                                                     Select (I.ing_mto_abo / I.ing_fac_cam)).Sum
                If Ingresos Is Nothing Then Ingresos = 0

                Dim Doctos = From doc1 In Data.doc_cls Where doc1.id_doc = D.id_doc And ((D.doc_sdo_cli - Ingresos) > 0 Or D.id_P_0012 = 3)

                If Doctos.Count > 0 Then
                    Cantidad += 1
                End If

            Next

            Return Cantidad

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosOtorgagosPagos_RetornaDoctos_por_id(ByVal nro_docto1 As String, Optional ByVal NroAplicacion As Integer = 0) As Collection

        '**************************************************************************************************************************************************************
        'Descripcion: Devuelve Datos de Documentos que se pueden pagar 
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 20/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/09/2010         Se agrega rango por monto(opcional)
        '**************************************************************************************************************************************************************

        Try

            Dim Coll As New Collection
            Dim Sesion As New ClsSession.ClsSession
            Dim Data As New DataClsFactoringDataContext

            Data.ObjectTrackingEnabled = False



            Dim Temporal_doc = (From doc1 In Data.doc_cls _
            Where (doc1.dsi_cls.dsi_flj = "N" And doc1.id_doc = nro_docto1) _
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
                            .Deudor = doc1.dsi_cls.deu_cls.deu_rso, _
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
                            .MontoPagar = doc1.doc_sdo_ddr, _
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

    Public Function hoja_recauda_valida_deudor(ByVal DEUDOR As String, ByVal HOJA As Integer) As String


        '*********************************************************************************************************************************
        'Descripcion: Valida que Deudor exista en Hoja de Recaudación
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 07/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************
        Dim DATA As New DataClsFactoringDataContext
        Dim VLC As Boolean

        Try

            Dim DRC = From D In DATA.drc_cls Where D.id_hre = HOJA And D.gsn_cls.doc_cls.dsi_cls.deu_ide = DEUDOR

            VLC = True
        Catch ex As Exception
            VLC = False
        End Try

        If VLC = False Then


            Dim ING = (From D In DATA.ing_sec_cls Where D.doc_cls.dsi_cls.deu_ide = DEUDOR And D.ing_cls.hre_cls.id_hre = HOJA _
                      Select D.id_ing_sec).Count
            Return True
            'Else
            '    Return False
        End If

        Return VLC

    End Function

    Public Function hoja_recauda_nueva_valida(ByVal cod_ejecutivo As Integer, _
                                                 ByVal fec_pago As Date, _
                                                 ByVal am_pm As String) As Integer


        '*********************************************************************************************************************************
        'Descripcion: Verifica que exista una hoja para el EJecutivo y la Fecha  , de no haberla crea una y devuelve su codigo
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 30/03/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim nro_hoja As Integer

        Try

            Dim hr As hre_cls = (From h In data.hre_cls Where h.hre_fec = fec_pago And h.hre_mna_tde = am_pm And h.id_eje = cod_ejecutivo Select h).First


            nro_hoja = hr.id_hre


        Catch ex As Exception
            nro_hoja = 0
        End Try


        If nro_hoja = 0 Then

            Dim hre As New hre_cls

            With hre
                .id_eje = cod_ejecutivo
                .hre_fec = fec_pago
                .hre_mna_tde = am_pm
                .hre_fec_ges = Date.Now
                .hre_hor_ges = Date.Now
            End With


            data.hre_cls.InsertOnSubmit(hre)
            data.SubmitChanges()


            Dim hoja = From hr In data.hre_cls Where hr.id_eje = cod_ejecutivo And hr.hre_fec = fec_pago And hr.hre_mna_tde = am_pm _
      Select hr

            nro_hoja = hre.id_hre

            Return nro_hoja
        Else

            Return nro_hoja

        End If

    End Function

    Public Function HOJA_REC_VALIDA(ByVal COD_EJE As Integer, ByVal FECHA As Date, ByVal AM_PM As String) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Verifica que exista una hoja para el EJecutivo y devuelve su codigo 
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 30/03/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        '*********************************************************************************************************************************

        Dim DATA As New DataClsFactoringDataContext

        Try

            Dim HOJA = From H In DATA.hre_cls Where H.id_eje = COD_EJE And CDate(H.hre_fec) = FECHA And H.hre_mna_tde = AM_PM

            For Each P In HOJA

                Return P.id_hre
            Next

        Catch ex As Exception
            Return 0
        End Try


    End Function

    Public Function Docto_pagos_hoja_rec_valida(ByVal nro_hoja As Integer, ByVal rut_cliente As String, ByVal rut_deudor As String, _
                                                ByVal nro_docto As Integer, ByVal nro_cuota As Integer, ByVal tipo_docto As Integer) As Object


        '*********************************************************************************************************************************
        'Descripcion: Valida que el Documento no haya sido Pagado
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 07/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext


        Try

            Dim ing = From i In data.ing_sec_cls Where i.cli_idc = rut_cliente And i.doc_cls.dsi_cls.deu_ide = rut_deudor _
                     And i.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto And i.doc_cls.dsi_cls.dsi_num = nro_docto _
                     And i.doc_cls.dsi_cls.dsi_flj_num = nro_cuota And i.ing_cls.id_hre = nro_hoja _
                     And (i.ing_vld_rcz = "I" Or _
                          i.ing_vld_rcz = "V" Or _
                          i.ing_vld_rcz = "L") Select i.id_ing

            For Each p In ing
                Return p
            Next
            '"! Existe Docto Pagado Para hoja de Recaudación ¡"


        Catch EX As Exception
            Return Nothing
        End Try



    End Function

    Public Function docto_rechazado_valida(ByVal nro_hoja As Integer, ByVal rut_cliente As String, ByVal rut_deudor As String, ByVal nro_docto As Integer, ByVal nro_cuota As Integer, ByVal tipo_docto As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Valida que el Documento no haya sido Rechazado en el Pago
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 08/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try
            Dim ing = From i In data.ing_sec_cls Where i.cli_idc = rut_cliente And i.doc_cls.dsi_cls.deu_ide = rut_deudor _
                   And i.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto And i.ing_cls.hre_cls.id_hre = nro_hoja And _
                   i.ing_vld_rcz = "R" Select i


            For Each p In ing
                Return True
            Next

            Return False


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function doctos_no_cedidos_valida(ByVal rut_deudor As String, _
                                             ByVal nro_docto As Integer, _
                                             ByVal nro_cuota As Integer, _
                                             ByVal tipo_docto As Integer, _
                                             ByVal rut_cliente As String, _
                                            ByVal nro_operacion As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Valida que el Documento no exista en el flujo factoring, para asi poder determinar que es no cedido
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 11/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Try

            If rut_cliente = 0 Then
                'Busca por Deudor 

                Dim doc = From d In data.doc_cls Where d.dsi_cls.deu_ide = rut_deudor And d.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                          And d.dsi_cls.dsi_num = nro_docto And d.dsi_cls.dsi_flj_num = nro_cuota
                For Each p In doc
                    Return False
                Next

                Return True

            Else

                'Busca por Cliente

                Dim doc = From d In data.doc_cls Where d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente And d.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
              And d.dsi_cls.dsi_num = nro_docto And d.dsi_cls.dsi_flj_num = nro_cuota

                For Each p In doc
                    Return False
                Next

                Return True

            End If

        Catch ex As Exception
            Return True
        End Try


    End Function

    Public Function doctos_no_cedidos_rec_devuelve(ByVal rut_cliente As String, ByVal rut_deudor As String, ByVal tipo_docto As Integer, _
                                      ByVal nro_docto As Integer, ByVal nro_hoja_rec As Integer, ByVal monto_docto As Double, ByVal Modificable As Integer, _
                                      Optional ByVal tipo_moneda As Integer = 1) As Collection


        '*********************************************************************************************************************************
        'Descripcion:Busca documentos no cedidos segun Nº, cliente ,rut  y tipo , en caso de existir y que el monto ingresado sea distinto al original se modifica.
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 14/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************


        Dim data As New DataClsFactoringDataContext

        Dim col As New Collection

        If Modificable = True Then


            Try


                Dim nc = From d In data.nce_cls Where d.deu_ide = rut_deudor And d.id_hre = nro_hoja_rec And d.nce_num_doc = nro_docto _
                         And d.id_p_0031 = tipo_docto

                For Each p In nc
                    p.nce_mto = monto_docto
                Next

                data.SubmitChanges()
            Catch ex As Exception

            End Try

        End If

        If rut_cliente <> "" Then


            Try

                Dim no_ced = From n In data.nce_cls Where n.cli_idc = rut_cliente And n.id_hre = nro_hoja_rec _
                                                             And n.nce_num_doc = nro_docto _
                                                             And n.id_p_0031 = tipo_docto _
          Select tip_doc = n.P_0031_cls.pnu_atr_003, _
           n.nce_num_doc, _
           n.cli_idc, _
           deu_rso = n.deu_cls.deu_rso + " " + n.deu_cls.deu_ape_ptn + " " + n.deu_cls.deu_ape_mtn, _
           cli_rso = n.cli_cls.cli_rso + " " + n.cli_cls.cli_ape_ptn + " " + n.cli_cls.cli_ape_mtn, _
           n.nce_mto, _
           n.nce_fec_gen, _
           n.id_hre, _
           n.id_PL_000069, _
           n.PL_000069_cls.pal_des, _
           n.deu_ide, _
           n.nce_obs, _
           moneda = n.P_0023_cls.pnu_atr_003, _
           mnd = n.P_0023_cls.pnu_des

                For Each p In no_ced
                    col.Add(p)

                Next

                Return col


            Catch ex As Exception
                Return Nothing
            End Try

        Else
            Try

                Dim no_ced = From n In data.nce_cls Where n.deu_ide = rut_deudor And n.id_hre = nro_hoja_rec _
                                                             And n.nce_num_doc = nro_docto _
                                                             And n.id_p_0031 = tipo_docto _
          Select tip_doc = n.P_0031_cls.pnu_atr_003, _
           n.nce_num_doc, _
           n.cli_idc, _
            cli_rso = n.cli_cls.cli_rso + " " + n.cli_cls.cli_ape_ptn + " " + n.cli_cls.cli_ape_mtn, _
           deu_rso = n.deu_cls.deu_rso + " " + n.deu_cls.deu_ape_ptn + " " + n.deu_cls.deu_ape_mtn, _
           n.nce_mto, _
           n.nce_fec_gen, _
           n.id_hre, _
           n.id_PL_000069, _
           n.PL_000069_cls.pal_des, _
           n.deu_ide, _
           n.nce_obs, _
           moneda = n.P_0023_cls.pnu_atr_003, _
           mnd = n.P_0023_cls.pnu_des


                For Each p In no_ced
                    col.Add(p)

                Next

                Return col


            Catch ex As Exception
                Return Nothing
            End Try

        End If


    End Function

    Public Function docto_hoja_rec_valida(ByVal nro_hoja As Integer, ByVal rut_deudor As String, ByVal nro_docto As Integer, ByVal nro_cuota As Integer, ByVal tipo_docto As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion:Valida que el documento se haya ingresado en la tabla de Documentos a Recaudar
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 15/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************


        Dim data As New DataClsFactoringDataContext

        Try

            Dim dr = From d In data.drc_cls Where d.gsn_cls.doc_cls.dsi_cls.deu_ide = rut_deudor _
                     And d.gsn_cls.doc_cls.dsi_cls.dsi_num = nro_docto _
                     And d.gsn_cls.doc_cls.dsi_cls.dsi_flj_num = nro_cuota _
                     And d.gsn_cls.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                     And d.hre_cls.id_hre = nro_hoja

            For Each p In dr

                Return True
            Next

            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function documentos_rec_devuelve(ByVal rut_deudor As String, ByVal nro_docto As Integer, ByVal nro_cuota As Integer, _
                                          ByVal tipo_docto As Integer, ByVal tipo_consulta As Integer, ByVal hoja_ruta As Integer, _
                                          ByVal rut_cliente As String, ByVal nro_operacion As Integer) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve documento si existe , sino avisa que es un Docto no Cedido
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 07/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'jlagos                     26-08-2010          se agrega estado 13(anulado)
        'A. Saldivar                22-02-2011          En consulta por cliente se saca rut deudor
        '*********************************************************************************************************************************

        Dim COL As New Collection
        Dim data As New DataClsFactoringDataContext
        Dim existe As Boolean

        If rut_cliente <> "" Then

            Try

                'Dim dc = From d In data.doc_cls Where d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente And _
                '                                      d.dsi_cls.id_ope = nro_operacion And d.dsi_cls.deu_ide = rut_deudor _
                '                                      And d.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                '                                      And d.dsi_cls.dsi_num = nro_docto And d.dsi_cls.dsi_flj_num = nro_cuota
                Dim dc As New Object
                If nro_operacion = 0 Then
                    dc = From d In data.doc_cls Where d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente _
                                                         And d.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                                         And d.dsi_cls.dsi_num = nro_docto And d.dsi_cls.dsi_flj_num = nro_cuota


                    'For Each x In dc
                    '    existe = True
                    'Next
                    'If Not IsNothing(dc) Then
                    '    existe = True
                    'End If

                Else
                    dc = From doc In data.doc_cls Where doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente _
                                                        And doc.dsi_cls.id_ope = nro_operacion _
                                                        And doc.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                                        And doc.dsi_cls.dsi_num = nro_docto And doc.dsi_cls.dsi_flj_num = nro_cuota


                    'If Not IsNothing(dc) Then
                    '    existe = True
                    'End If

                End If

                For Each p In dc
                    existe = True
                Next



            Catch ex As Exception
                existe = False
            End Try


            If existe = True Then


                If tipo_consulta = 1 Then

                    Try

                        'Se saca numero de operacion por que no rescata datos
                        'And doc1.dsi_cls.id_ope = nro_operacion _

                        Dim doc = From doc1 In data.doc_cls Where doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente _
                                                        And doc1.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                                        And doc1.dsi_cls.dsi_num = nro_docto _
                                                        And doc1.dsi_cls.dsi_flj = "N" _
                                                        And doc1.dsi_cls.id_P_0011 <> 5 _
                                                        And doc1.dsi_cls.id_P_0011 <> 13 _
                     Select New With {doc1.id_doc, _
                               doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
              doc1.dsi_cls.deu_ide, _
  .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
             .Deudor = doc1.dsi_cls.deu_cls.deu_rso & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn, _
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
              .doc_sdo_cli = doc1.doc_sdo_cli - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" _
                                                 Select (I.ing_mto_abo / I.ing_fac_cam)).Sum, _
              .doc_sdo_ddr = doc1.doc_sdo_ddr - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" And _
                                                            I.ing_qpa = "D" _
                                                 Select (I.ing_mto_tot / I.ing_fac_cam)).Sum, _
              doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
              doc1.doc_not_cre, _
             .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
             .Interes = 0.0, _
             .MontoPagar = 0.0, _
             .PagaDeudor = "S", _
             doc1.opo_cls.ope_cls.ope_fac_cam, _
             doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa}
                        'opn_tas_moa}



                        For Each P In doc

                            COL.Add(P)

                        Next

                        Return COL

                    Catch ex As Exception

                        Return Nothing
                    End Try

                Else

                    Try

                        Dim doc = From doc1 In data.doc_cls Where doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente _
                                      And doc1.dsi_cls.deu_ide = rut_deudor _
                                      And doc1.dsi_cls.id_ope = nro_operacion _
                                     And doc1.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                     And doc1.dsi_cls.dsi_num = nro_docto _
                                     And doc1.dsi_cls.dsi_flj_num <> 0 _
                                     And doc1.dsi_cls.dsi_flj = "N" _
                                     And doc1.dsi_cls.id_P_0011 <> 5 _
                                     And doc1.dsi_cls.id_P_0011 <> 13 _
    Select New With {doc1.id_doc, _
              doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
              doc1.dsi_cls.deu_ide, _
                 .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                               doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                               doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
             .Deudor = doc1.dsi_cls.deu_cls.deu_rso & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn, _
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
              .doc_sdo_cli = doc1.doc_sdo_cli - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" _
                                                 Select (I.ing_mto_abo / I.ing_fac_cam)).Sum, _
              .doc_sdo_ddr = doc1.doc_sdo_ddr - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" And _
                                                            I.ing_qpa = "D" _
                                                 Select (I.ing_mto_tot / I.ing_fac_cam)).Sum, _
              doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
              doc1.doc_not_cre, _
             .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
             .Interes = 0.0, _
             .MontoPagar = 0.0, _
             .PagaDeudor = "S", _
             doc1.opo_cls.ope_cls.ope_fac_cam, _
             doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa}



                        For Each P In doc

                            COL.Add(P)

                        Next

                        Return COL

                    Catch ex As Exception

                        Return Nothing
                    End Try




                End If

            End If
        Else

            If hoja_ruta = 0 Then

                Return Nothing
            Else

                Try

                    Dim doc = From doc1 In data.doc_cls Where doc1.dsi_cls.deu_ide = rut_deudor _
                                     And doc1.dsi_cls.ope_cls.opn_cls.id_P_0031 = tipo_docto _
                                     And doc1.dsi_cls.dsi_num = nro_docto _
                                     And doc1.dsi_cls.dsi_flj = "N" _
                                     And doc1.dsi_cls.id_P_0011 <> 5 _
                                     And doc1.dsi_cls.id_P_0011 <> 13 _
           Select New With {doc1.id_doc, _
              doc1.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                 .Cliente = If(doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                               doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn & " " & doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn, _
                               doc1.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso), _
             .Deudor = doc1.dsi_cls.deu_cls.deu_rso & " " & doc1.dsi_cls.deu_cls.deu_ape_ptn & " " & doc1.dsi_cls.deu_cls.deu_ape_mtn, _
              doc1.dsi_cls.deu_ide, _
              doc1.id_opo, _
              doc1.opo_cls.opo_otg, _
              doc1.opo_cls.ope_cls.opn_cls.id_P_0031, _
             .TipoDocto = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
             .TipoDoctoCorta = doc1.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
              doc1.dsi_cls.id_P_0011, _
              doc1.dsi_cls.id_ope, _
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
              doc1.dsi_cls.ope_cls.ope_fec_sim, _
              doc1.dsi_cls.ope_cls.ope_lnl, _
              doc1.doc_tas_ren, _
              doc1.doc_gto, _
              doc1.dsi_cls.dsi_fev_ori, _
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
              .doc_sdo_cli = doc1.doc_sdo_cli - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" _
                                                 Select (I.ing_mto_abo / I.ing_fac_cam)).Sum, _
              .doc_sdo_ddr = doc1.doc_sdo_ddr - (From I In data.ing_sec_cls Where I.id_doc = doc1.id_doc And _
                                                            I.id_P_0053 = 2 And _
                                                           (I.ing_vld_rcz = "S" Or _
                                                            I.ing_vld_rcz = "I" Or _
                                                            I.ing_vld_rcz = "V" Or _
                                                            I.ing_vld_rcz = "C" Or _
                                                            I.ing_vld_rcz = "L") And _
                                                            I.ing_pro = "N" And _
                                                            I.ing_qpa = "D" _
                                                 Select (I.ing_mto_tot / I.ing_fac_cam)).Sum, _
              doc1.opo_cls.ope_cls.opn_cls.id_P_0023, _
              doc1.doc_not_cre, _
             .Moneda = doc1.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
             .Interes = 0.0, _
             .MontoPagar = 0.0, _
             .PagaDeudor = "S", _
             doc1.opo_cls.ope_cls.ope_fac_cam, _
             doc1.opo_cls.ope_cls.opn_cls.opn_tas_moa}

                    For Each P In doc
                        COL.Add(P)
                    Next

                    Return COL


                Catch ex As Exception
                    Return Nothing
                End Try

            End If
        End If



    End Function

    Public Function DOCTO_NCE_VALIDA_PAGO(ByVal rut_cliente As String, _
                                        ByVal rut_deudor As String, _
                                        ByVal tipo_docto As Integer, _
                                        ByVal nro_docto As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Valida la existencia del Pago de un Documento no Cedido
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 19/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************
        Dim mensaje As String

        mensaje = ""

        Dim data As New DataClsFactoringDataContext

        Try

            Dim val = From i In data.ing_sec_cls Where i.nce_cls.nce_num_doc = nro_docto And i.nce_cls.cli_idc = rut_cliente _
                      And i.nce_cls.deu_ide = rut_deudor And i.nce_cls.id_p_0031 = tipo_docto And (i.ing_vld_rcz = "I" Or _
                                                            i.ing_vld_rcz = "V" Or _
                                                            i.ing_vld_rcz = "C" Or _
                                                            i.ing_vld_rcz = "L")
            For Each p In val
                Return True
            Next
            Return False

        Catch ex As Exception

            Return False

        End Try


    End Function

    Public Function Doctos_Gestion_dia_devuelve(ByVal am_pm As String, ByVal suc1 As Integer, ByVal suc2 As Integer, _
                                                 Optional ByVal Pag As Integer = 999) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve documentos a recaudar segun sucursal y am/pm
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 13/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'A. Saldivar                 08/02/2011         Se agrega paginacion
        '*********************************************************************************************************************************

        Dim Data As New DataClsFactoringDataContext
        Dim col As New Collection
        Dim sesion As New ClsSession.ClsSession


        Try


            Dim rec = (From drc In Data.drc_cls Join g In Data.gsn_cls On drc.id_gsn Equals g.id_gsn _
                     Join h In Data.hre_cls On drc.id_hre Equals h.id_hre Join doc In Data.doc_cls On g.id_doc Equals doc.id_doc Where drc.drc_fec_pag = Date.Now.ToShortDateString _
                      And h.hre_mna_tde = am_pm And drc.gsn_cls.doc_cls.id_suc_rcd >= suc1 And _
                     drc.gsn_cls.doc_cls.id_suc_rcd <= suc2 _
            Select drc.gsn_cls.doc_cls.dsi_cls.deu_ide, _
             drc.gsn_cls.doc_cls.dsi_cls.deu_cls.deu_rso, _
             drc.gsn_cls.ddi_cls.cmn_cls.id_zon, _
             drc.gsn_cls.ddi_cls.cmn_cls.zon_cls.zon_des, _
            cant_doctos = (From d In Data.drc_cls Join gs In Data.gsn_cls On d.id_gsn Equals gs.id_gsn _
                            Join h1 In Data.hre_cls On d.id_hre Equals h1.id_hre Where d.drc_fec_pag = Date.Now.ToShortDateString _
                            And h1.hre_mna_tde = am_pm And d.gsn_cls.doc_cls.id_suc_rcd >= suc1 And _
                            d.gsn_cls.doc_cls.id_suc_rcd <= suc2 Select d.id_drc).Distinct.Count, _
             monto = (From d In Data.drc_cls Join gs In Data.gsn_cls On d.id_gsn Equals gs.id_gsn _
                      Join h1 In Data.hre_cls On d.id_hre Equals h1.id_hre Where d.drc_fec_pag = Date.Now.ToShortDateString _
                      And h1.hre_mna_tde = am_pm And d.gsn_cls.doc_cls.id_suc_rcd >= suc1 And _
                      d.gsn_cls.doc_cls.id_suc_rcd <= suc2 Select g.doc_cls.doc_sdo_ddr * g.doc_cls.dsi_cls.ope_cls.ope_fac_cam).Sum, _
            g.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0023, _
            g.gsn_dir_cbz, _
            g.gsn_hor_pag_dde, _
            g.gsn_hor_pag, _
            drc.drc_ntf_rec, _
            g.id_eje_cob, _
            g.id_cco).Skip(sesion.NroPaginacion_Deu).Take(Pag)

            For Each p In rec
                col.Add(p)
            Next


            If Not IsNothing(col) Then

                If col.Count > 0 Then
                    Return col
                Else
                    Return Nothing
                End If

            End If



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Doctos_no_cedidos_deudor_retorna(ByVal rut_cliente As String, ByVal rut_deudor As String, ByVal tipo As String) As Collection


        '*********************************************************************************************************************************
        'Descripcion: Devuelve documentos no Cedidos por deudor o cliente.
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 13/04/2009                                                                                                                     
        'Quien Modifica              Fecha          Descripcion          
        'A Saldivar                  08/02/2011     Se agrega  paginacion

        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim col As New Collection
        Dim sesion As New ClsSession.ClsSession

        If tipo = "D" Then

            Try

                Dim ing = (From d In data.nce_cls Where d.deu_ide = rut_deudor _
                          And Not (From N In data.nce_cls _
                                          Join I In data.ing_sec_cls On N.id_nce Equals I.id_nce _
                                                                  Where N.deu_ide = rut_deudor _
                                                                           And N.nce_pro = "N" And _
                                                                            I.id_P_0053 = 7 And _
                                                                            I.ing_vld_rcz <> "R" And _
                                                                            I.ing_vld_rcz <> "A").Count > 0 _
                            Select d.id_p_0031, _
                            d.P_0031_cls.pnu_des, _
                            d.nce_num_doc, _
                            d.cli_idc, _
                            cli_rso = d.cli_cls.cli_rso + " " + d.cli_cls.cli_ape_ptn + " " + d.cli_cls.cli_ape_mtn, _
                            d.nce_mto, _
                            d.nce_fec_gen, _
                            d.id_hre, _
                            d.id_PL_000069, _
                            d.PL_000069_cls.pal_des, _
                            d.deu_ide, _
                            d.nce_obs, _
                            moneda = d.P_0023_cls.pnu_atr_003, _
                            mnd = d.P_0023_cls.pnu_des).Skip(sesion.NroPaginacion).Take(11)



                For Each p In ing

                    col.Add(p)

                Next

                Return col
            Catch ex As Exception
                Return Nothing
            End Try



        Else


            Try


                Dim ing = From d In data.nce_cls Where d.cli_idc = rut_cliente _
                             And Not (From N In data.nce_cls _
                                             Join I In data.ing_sec_cls On N.id_nce Equals I.id_nce _
                                                                     Where N.cli_idc = rut_cliente _
                                                                              And N.nce_pro = "N" And _
                                                                               I.id_P_0053 = 7 And _
                                                                               I.ing_vld_rcz <> "R" And _
                                                                               I.ing_vld_rcz <> "A").Count > 0 _
                               Select d.id_p_0031, _
                               d.P_0031_cls.pnu_des, _
                               d.nce_num_doc, _
                               d.cli_idc, _
                               cli_rso = d.cli_cls.cli_rso + " " + d.cli_cls.cli_ape_ptn + " " + d.cli_cls.cli_ape_mtn, _
                               d.nce_mto, _
                               d.nce_fec_gen, _
                               d.id_hre, _
                               d.id_PL_000069, _
                               d.PL_000069_cls.pal_des, _
                               d.deu_ide, _
                               d.nce_obs, _
                               moneda = d.P_0023_cls.pnu_atr_003, _
                               mnd = d.P_0023_cls.pnu_des


                For Each p In ing

                    col.Add(p)

                Next


                Return col
            Catch ex As Exception

                Return Nothing

            End Try

        End If








    End Function

    Public Function Pagos_Recaudación_Retorna(ByVal fecha As DateTime, ByVal am_pm As String, ByVal eje As Integer, ByVal estado As String) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Retorna los pagos hechos por hoja de Recaudación.
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 04/05/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          


        'And (From d In data.ing_sec_cls Where i.id_ing = d.id_ing Select d.ing_vld_rcz).First <> "L" _
        'And (From d In data.ing_sec_cls Where i.id_ing = d.id_ing Select d.ing_vld_rcz).First <> "C" _
        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim col As New Collection
        Try
            If estado = "T" Then



                Dim pago = From i In data.ing_cls Where i.id_hre = i.hre_cls.id_hre _
                                  And i.hre_cls.hre_mna_tde = am_pm _
                                  And i.ing_fec = fecha _
                                  And i.hre_cls.id_eje = eje _
           Select i.id_ing, _
                                 i.ing_fec, _
                                 i.ing_obs, _
                                 Total = (From ing In data.ing_sec_cls Where ing.id_ing = i.id_ing Select ing.ing_mto_tot).Sum, _
                                 est_pgo = (From ing In data.ing_sec_cls Where ing.id_ing = i.id_ing Select ing.ing_vld_rcz).First

                For Each p In pago
                    col.Add(p)
                Next
                Return col

            Else

                Dim pago = From i In data.ing_cls Where i.id_hre = i.hre_cls.id_hre _
                                                  And i.hre_cls.hre_mna_tde = am_pm _
                                                  And i.ing_fec = fecha _
                                                  And i.hre_cls.id_eje = eje _
                                                  And (From d In data.ing_sec_cls Where i.id_ing = d.id_ing Select d.ing_vld_rcz).First = estado _
                        Select i.id_ing, _
                                 i.ing_fec, _
                                 i.ing_obs, _
                                 Total = (From ing In data.ing_sec_cls Where ing.id_ing = i.id_ing Select ing.ing_mto_tot).Sum, _
                                 est_pgo = (From ing In data.ing_sec_cls Where ing.id_ing = i.id_ing Select ing.ing_vld_rcz).First
                '
                For Each p In pago
                    col.Add(p)
                Next
                Return col

            End If



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function hoja_recaudacion_devuelve(ByVal codigo_ejecutivo As Integer, ByVal fecha_hoja As DateTime) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Devuelve codigo de Hoja por ejecutivo y fecha
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 10/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          



        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Try

            Dim hre = (From h In data.hre_cls Where h.id_eje = codigo_ejecutivo And h.hre_fec = fecha_hoja Select h.id_hre).First

            If Not IsNothing(hre) Then

                Return hre

            Else

                Return 0

            End If

        Catch ex As Exception
            Return 0
        End Try

    End Function

#End Region


#Region "Actualización Recaudacion"


#Region "Recaudación"

    Public Function Doctos_no_cedidos_modifica(ByVal nce As nce_cls) As Integer
        '*********************************************************************************************************************************
        'Descripcion:Guarda Documentos no Cedidos
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 22/06/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Try


            Dim nocedido = From n In data.nce_cls Where n.cli_idc = nce.cli_idc _
                                                      And n.deu_ide = nce.deu_ide _
                                                      And n.id_p_0031 = nce.id_p_0031 _
                                                      And n.nce_num_doc = nce.nce_num_doc

            For Each p In nocedido

                With p

                    .id_PL_000069 = nce.id_PL_000069

                End With
            Next

            data.SubmitChanges()



        Catch ex As Exception
            Return 0
        End Try




    End Function


    Public Function doctos_no_cedidos_paga(ByVal id_nce As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext

            Dim nce = From d In data.nce_cls Where d.id_nce = id_nce

            For Each p In nce

                With p
                    .nce_fec_pft = Date.Now
                    .nce_est_nom = "P"


                End With

            Next

            data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try



    End Function

    Public Function doctos_no_cedidos_anula(ByVal id_nce As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext

            Dim nce = From d In data.nce_cls Where d.id_nce = id_nce

            For Each p In nce

                With p
                    .nce_fec_pft = "1900/01/01"
                    .nce_est_nom = "N"
                    .id_PL_000069 = Nothing

                End With

            Next

            data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try



    End Function
    Public Function gastos_valida(ByVal codigo As Integer) As Boolean
        Try

            Dim data As New DataClsFactoringDataContext

            Dim gto = From g In data.gga_cls Where g.id_gga = codigo

            For Each p In gto
                p.gga_vld = "S"
            Next

            data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Function gastos_rec_guarda(ByVal gga As Collection, ByVal id_hre As Integer, ByVal deu_ide As String) As Boolean

        ' Guardar gastos asociados a una Hoja de Recaudación y un deudor
        ' P.Gatica
        ' 05/06/2009

        Try


            Dim data As New DataClsFactoringDataContext

            Dim contador = (From g In data.gga_cls Where g.id_hre = id_hre And g.deu_ide = deu_ide).Count

            If contador > 0 Then

                For i = 0 To contador - 1

                    Dim gto As gga_cls = (From g In data.gga_cls Where g.id_hre = id_hre And g.deu_ide = deu_ide).First

                    If Not IsNothing(gto) Then

                        data.gga_cls.DeleteOnSubmit(gto)
                        data.SubmitChanges()


                    End If

                Next

            End If
            For i = 1 To gga.Count
                Dim GG As New gga_cls

                With GG

                    .deu_ide = gga.Item(i).DEU_IDE
                    .gga_fec = gga.Item(i).gga_fec
                    .gga_mto = CDbl(gga.Item(i).gga_mto)
                    .gga_rec_ext = gga.Item(i).gga_rec_ext
                    .gga_vld = gga.Item(i).gga_vld
                    .id_hre = CInt(gga.Item(i).id_hre)
                    .id_p_0051 = CInt(gga.Item(i).id_p_0051)

                End With

                data.gga_cls.InsertOnSubmit(GG)
                data.SubmitChanges()

            Next

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function
    Public Function Documento_a_recaudar_inserta(ByVal NRO_HOJA As Integer, ByVal ID_GESTION As Integer, ByVal ID_DOC As Integer, ByVal FEC_PAG As DateTime) As Boolean


        '*********************************************************************************************************************************
        'Descripcion: Ingresa documentos en la Drc, para que sean a Recaudar
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 25/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          



        '*********************************************************************************************************************************

        Dim ts As New System.Transactions.TransactionScope
        Try


            Using ts



                Dim data As New DataClsFactoringDataContext

                Dim CodigoDocumentoRecaudar As Int32
                Dim Temporal_drc = From DRC In data.drc_cls _
                                   Where DRC.gsn_cls.doc_cls.id_doc = ID_DOC _
                                   And DRC.id_hre = NRO_HOJA _
                                   And DRC.drc_fec_pag = FEC_PAG _
                Select DRC


                For Each p In Temporal_drc
                    CodigoDocumentoRecaudar = p.id_drc
                    Exit For
                Next

                If CodigoDocumentoRecaudar = Nothing Or CodigoDocumentoRecaudar = 0 Then
                    Dim drc_ingreso As New drc_cls

                    drc_ingreso.id_hre = NRO_HOJA
                    drc_ingreso.id_gsn = ID_GESTION
                    drc_ingreso.id_P_0103 = Nothing
                    drc_ingreso.drc_est = "S"
                    drc_ingreso.drc_fec_pag = FEC_PAG
                    drc_ingreso.drc_ntf_rec = Nothing
                    drc_ingreso.drc_pen = Nothing
                    drc_ingreso.drc_fec_pen = Nothing

                    data.drc_cls.InsertOnSubmit(drc_ingreso)
                    data.SubmitChanges()
                Else
                    Dim drc_modifica As drc_cls = (From DRC In data.drc_cls _
                            Where DRC.id_drc = CodigoDocumentoRecaudar _
                            Select DRC).First()

                    drc_modifica.id_hre = NRO_HOJA
                    drc_modifica.id_gsn = ID_GESTION
                    drc_modifica.id_P_0103 = Nothing
                    drc_modifica.drc_est = "S"
                    drc_modifica.drc_fec_pag = FEC_PAG
                    drc_modifica.drc_ntf_rec = Nothing
                    drc_modifica.drc_pen = Nothing
                    drc_modifica.drc_fec_pen = Nothing

                    data.SubmitChanges()
                End If

            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function asignacion_recaudador_guarda(ByVal rut_deudor As String, _
                                          ByVal zona_deudor As Integer, _
                                          ByVal nro_hoja_recaudacion As Integer, _
                                          ByVal codigo_ejecutivo As Integer, _
                                          ByVal fecha_hoja As DateTime, _
                                          ByVal am_pm As String, _
                                          ByVal suc_rec As Integer) As Boolean



        '*********************************************************************************************************************************
        'Descripcion: Guarda documentos a recaudar para un ejecutivo especifico
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 10/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          



        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Dim ts As New System.Transactions.TransactionScope

        Using ts




            Dim drc_ntf_rec As String

            If codigo_ejecutivo = 0 Then

                Dim cod = From e In data.eje_cls Where e.id_suc = suc_rec And e.eje_aux = "S" And e.id_P_0045 = 14

            End If

            If suc_rec = 999 Then

                drc_ntf_rec = "S"

            End If



            Try

                Dim hre = (From h In data.hre_cls Where h.id_eje = codigo_ejecutivo And h.hre_fec = fecha_hoja And h.hre_mna_tde = am_pm Select h.id_hre).First



            Catch ex As Exception


                Dim hr As New hre_cls

                With hr
                    .id_eje = codigo_ejecutivo
                    .hre_fec = fecha_hoja
                    .hre_mna_tde = am_pm
                    .hre_fec_ges = Date.Now.ToShortDateString
                End With


                data.hre_cls.InsertOnSubmit(hr)
                data.SubmitChanges()
                nro_hoja_recaudacion = hr.id_hre
            End Try
            If suc_rec <> 999 Then


                Dim drc = From dr In data.drc_cls Where dr.gsn_cls.doc_cls.dsi_cls.deu_ide = rut_deudor _
                          And dr.drc_fec_pag = fecha_hoja And dr.gsn_cls.doc_cls.dsi_cls.deu_cls.cmn_cls.id_zon = zona_deudor _
                          Select ntf_rec = dr.drc_ntf_rec
                For Each p In drc
                    drc_ntf_rec = p
                Next



            End If


            Dim docrc As drc_cls = (From d In data.drc_cls Where d.gsn_cls.doc_cls.dsi_cls.deu_ide = rut_deudor _
                  And d.drc_fec_pag = fecha_hoja And d.gsn_cls.ddi_cls.cmn_cls.id_zon = zona_deudor _
                  Select d).First


            docrc.drc_ntf_rec = drc_ntf_rec
            docrc.id_hre = nro_hoja_recaudacion


            data.SubmitChanges()



        End Using

    End Function

    Public Function Doctos_no_cedidos_guarda(ByVal nce As nce_cls) As Integer
        '*********************************************************************************************************************************
        'Descripcion:Guarda Documentos no Cedidos
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 22/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          

        '*********************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext
        Dim exist As Boolean
        Try


            Dim nocedido = From n In data.nce_cls Where n.cli_idc = nce.cli_idc _
                      And n.deu_ide = nce.deu_ide _
                      And n.id_p_0031 = nce.id_p_0031 _
                      And n.nce_num_doc = nce.nce_num_doc


            data.nce_cls.InsertOnSubmit(nce)
            data.SubmitChanges()

            Return nce.id_nce

        Catch ex As Exception
            Return 0
        End Try


        If exist = False Then


        End If


    End Function

    Public Function Pagos_Recaudación_ApruebaRechaza(ByVal id_ing As Integer, ByVal ApruebaRechaza As Char) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Aprueba o Rechaza pagos por su id_ing
        'Creado por Jorge Lagos
        'Fecha Creacion: 11/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Try


            Dim ING = From D In data.ing_sec_cls Where D.id_ing = id_ing

            For Each D In ING
                D.ing_vld_rcz = ApruebaRechaza
            Next

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#End Region


End Class
