Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_Lista_Operaciones
    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1

            Try

                Dim fecha_dsd, fecha_hst, fecha_Vcto_dsd, fecha_vcto_hst As String

                rut_cli.Value = Request.QueryString("rut_cli")
                rut_cli2.Value = Request.QueryString("rut_cli2")
                cli_rso.Value = Request.QueryString("cli_rso")
                rut_deu.Value = Request.QueryString("rut_deu")
                rut_deu2.Value = Request.QueryString("rut_deu2")
                deu_rso.Value = Request.QueryString("deu_rso")
                suc.Value = CInt(Request.QueryString("suc"))
                suc2.Value = CInt(Request.QueryString("suc2"))
                eje.Value = CInt(Request.QueryString("eje"))
                eje2.Value = CInt(Request.QueryString("eje2"))
                fecha_dsd = Request.QueryString("fec_dde")
                fecha_hst = Request.QueryString("fec_has")
                fec_dde.Value = fecha_dsd
                fec_hta.Value = fecha_hst
                n_ope1.Value = CInt(Request.QueryString("n_ope1"))
                n_ope2.Value = CLng(Request.QueryString("n_ope2"))
                nro_doc.Value = Request.QueryString("nro_doc")
                nro_doc2.Value = Request.QueryString("nro_doc2")
                fecha_Vcto_dsd = Request.QueryString("vcto_dde")
                fecha_vcto_hst = Request.QueryString("vcto_hta")
                fecha_vcto_hst = fecha_vcto_hst
                vcto_dde.Value = fecha_Vcto_dsd
                vcto_hta.Value = fecha_vcto_hst
                est_ope.Value = CInt(Request.QueryString("est_ope"))
                est_ope2.Value = CLng(Request.QueryString("est_ope2"))
                mon.Value = CInt(Request.QueryString("mon"))
                mon2.Value = CInt(Request.QueryString("mon2"))

                Genera_reporte()

            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Sub Genera_reporte()

        Try

            Dim prb As New Dataset_Operacion.sp_reporte_operaciones_deuda_consolida_en_monedasDataTable
            Dim prb2 As New Dataset_OperacionTableAdapters.sp_reporte_operaciones_deuda_consolida_en_monedasTableAdapter

            Dim data As New Dataset_Operacion.Sp_Reporte_OperacionesDataTable
            Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_OperacionesTableAdapter

            Dim data2 As New Dataset_Operacion.Sp_Reporte_OperacionesDataTable
            Dim tab2 As New Dataset_OperacionTableAdapters.Sp_Reporte_OperacionesTableAdapter

            Dim lr As New LocalReport

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Imprime_Operaciones.rdlc"

            If est_ope.Value = 4 Then
                est_ope.Value = 5
                est_ope2.Value = 5
            End If

            If est_ope.Value <> 0 Then

                If est_ope.Value = 1 Or est_ope.Value = 2 Or est_ope.Value = 6 Then

                    If nro_doc.Value <> 0 Then

                        data = tab.GetData(rut_cli.Value, rut_cli2.Value, cli_rso.Value, _
                                            rut_deu.Value, rut_deu2.Value, deu_rso.Value, _
                                            suc.Value, suc2.Value, eje.Value, eje2.Value, _
                                            Format(CDate(fec_dde.Value), "dd/MM/yyyy"), Format(CDate(fec_hta.Value), "dd/MM/yyyy"), _
                                            n_ope1.Value, n_ope2.Value, _
                                            nro_doc.Value, nro_doc2.Value, _
                                            Format(CDate(vcto_dde.Value), "dd/MM/yyyy"), Format(CDate(vcto_hta.Value), "dd/MM/yyyy"), _
                                            est_ope.Value, est_ope2.Value, mon.Value, mon2.Value, 2)



                    Else

                        data = tab.GetData(rut_cli.Value, rut_cli2.Value, cli_rso.Value, _
                                          rut_deu.Value, rut_deu2.Value, deu_rso.Value, _
                                          suc.Value, suc2.Value, eje.Value, eje2.Value, _
                                          Format(CDate(fec_dde.Value), "dd/MM/yyyy"), Format(CDate(fec_hta.Value), "dd/MM/yyyy"), _
                                          n_ope1.Value, n_ope2.Value, _
                                          nro_doc.Value, nro_doc2.Value, _
                                          Format(CDate(vcto_dde.Value), "dd/MM/yyyy"), Format(CDate(vcto_hta.Value), "dd/MM/yyyy"), _
                                          est_ope.Value, est_ope2.Value, mon.Value, mon2.Value, 1)


                    End If


                ElseIf est_ope.Value = 3 Or est_ope.Value = 5 Then


                    If nro_doc.Value <> 0 Then

                        data = tab.GetData(rut_cli.Value, rut_cli2.Value, cli_rso.Value, _
                                           rut_deu.Value, rut_deu2.Value, deu_rso.Value, _
                                           suc.Value, suc2.Value, eje.Value, eje2.Value, _
                                           Format(CDate(fec_dde.Value), "dd/MM/yyyy"), Format(CDate(fec_hta.Value), "dd/MM/yyyy"), _
                                           n_ope1.Value, n_ope2.Value, _
                                           nro_doc.Value, nro_doc2.Value, _
                                           Format(CDate(vcto_dde.Value), "dd/MM/yyyy"), Format(CDate(vcto_hta.Value), "dd/MM/yyyy"), _
                                           est_ope.Value, est_ope2.Value, mon.Value, mon2.Value, 2)


                    Else

                        prb = prb2.GetData(rut_cli.Value, rut_cli2.Value, cli_rso.Value, _
                                                            rut_deu.Value, rut_deu2.Value, deu_rso.Value, _
                                                            suc.Value, suc2.Value, _
                                                            eje.Value, eje2.Value, _
                                                            Format(CDate(fec_dde.Value), "dd/MM/yyyy"), Format(CDate(fec_hta.Value), "dd/MM/yyyy"), _
                                                            n_ope1.Value, n_ope2.Value, _
                                                            nro_doc.Value, nro_doc2.Value, _
                                                            Format(CDate(vcto_dde.Value), "dd/MM/yyyy"), Format(CDate(vcto_hta.Value), "dd/MM/yyyy"), _
                                                            est_ope.Value, est_ope2.Value, _
                                                            mon.Value, mon2.Value)

                    End If

                    For i = 0 To prb.Count - 1

                        If prb.Item(i).id_P_0023 = 1 Then
                            prb.Item(i).sdo_cli_pesos = prb.Item(i).doc_sdo_cli
                        ElseIf prb.Item(i).id_P_0023 = 2 Then
                            prb.Item(i).sdo_cli_uf = prb.Item(i).doc_sdo_cli
                        ElseIf prb.Item(i).id_P_0023 = 3 Then
                            prb.Item(i).sdo_cli_dolar = prb.Item(i).doc_sdo_cli
                        ElseIf prb.Item(i).id_P_0023 = 4 Then
                            prb.Item(i).sdo_cli_euro = prb.Item(i).doc_sdo_cli
                        End If


                    Next

                End If

            End If

            Dim dt As DataTable

            dt = data

            lr.DataSources.Add(New ReportDataSource("Sim", dt))

            Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt2 As DataTable

            If est_ope.Value = 3 Or est_ope.Value = 5 Then

                dt2 = prb

                rds = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones", dt2)

            Else

                dt2 = data

                rds = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones", dt2)

            End If

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ib_generar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_generar.Click
        Genera_reporte()
    End Sub


End Class
