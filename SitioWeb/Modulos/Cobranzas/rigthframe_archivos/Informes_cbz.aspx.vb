Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Cobranzas_rigthframe_archivos_Informes_cbz
    Inherits System.Web.UI.Page


    Public Sub Genera_Informes()

        Dim cb_no_re As New DataSet_GestionCobranza.sp_co_gestiones_del_diaDataTable
        Dim get_cb_no_re As New DataSet_GestionCobranzaTableAdapters.sp_co_gestiones_del_diaTableAdapter


        Dim cb_310 As New DataSet_GestionCobranza.sp_co_dev_doc_con_codi_autDataTable
        Dim get_310 As New DataSet_GestionCobranzaTableAdapters.sp_co_dev_doc_con_codi_autTableAdapter

        Dim cb_400 As New DataSet_GestionCobranza.sp_co_mod_inf_310o400DataTable
        Dim get_400 As New DataSet_GestionCobranzaTableAdapters.sp_co_mod_inf_310o400TableAdapter

        Dim cb_rad As New DataSet_GestionCobranza.sp_Reporte_Radicar_FacturasDataTable
        Dim get_rad As New DataSet_GestionCobranzaTableAdapters.sp_Reporte_Radicar_FacturasTableAdapter



        ' ReportViewer1.Height = 600
        Dim lr As New LocalReport
        ReportViewer1.LocalReport.DataSources.Clear()

        ReportViewer1.Reset()


        If Request.QueryString("tipo") = 1 Then

            lr.ReportPath = Server.MapPath("Reporte_gest_no.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_gest_no.rdlc"

            cb_no_re = get_cb_no_re.GetData(Request.QueryString("suc_1"), Request.QueryString("suc_2"), Request.QueryString("eje_1"), _
                                            Request.QueryString("eje_2"), Request.QueryString("cli_1"), Request.QueryString("cli_2"), _
                                            Request.QueryString("ddr_1"), Request.QueryString("ddr_2"), Request.QueryString("tdc_1"), _
                                            Request.QueryString("tdc_2"), Request.QueryString("cco_1"), Request.QueryString("cco_2"), _
                                            Request.QueryString("mto1"), Request.QueryString("mto2"), Request.QueryString("fec_1"), _
                                            Request.QueryString("fec_2"), Request.QueryString("fug_1"), Request.QueryString("fug_2"))

            Dim dt As DataTable

            dt = cb_no_re

            Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
            gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_gestiones_del_dia", dt)
            ReportViewer1.LocalReport.DataSources.Add(gest)

            ReportViewer1.DataBind()

        ElseIf Request.QueryString("tipo") = 2 Then

            lr.ReportPath = Server.MapPath("Reporte_100_400.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_100_400.rdlc"

            cb_400 = get_400.GetData(Request.QueryString("suc_1"), Request.QueryString("suc_2"), Request.QueryString("eje_1"), _
                                            Request.QueryString("eje_2"), Request.QueryString("cli_1"), Request.QueryString("cli_2"), _
                                            Request.QueryString("ddr_1"), Request.QueryString("ddr_2"), Request.QueryString("tdc_1"), _
                                            Request.QueryString("tdc_2"), Request.QueryString("cco_1"), Request.QueryString("cco_2"), _
                                            Request.QueryString("mto1"), Request.QueryString("mto2"), Request.QueryString("fec_1"), _
                                            Request.QueryString("fec_2"), Request.QueryString("fug_1"), Request.QueryString("fug_2"))

            Dim dt As DataTable

            dt = cb_400

            Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
            gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_mod_inf_310o400", dt)
            ReportViewer1.LocalReport.DataSources.Add(gest)

            ReportViewer1.DataBind()


        ElseIf Request.QueryString("tipo") = 3 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Reporte_310.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_310.rdlc"


            cb_310 = get_310.GetData(Request.QueryString("cli_1"), Request.QueryString("cli_2"), Request.QueryString("suc_1"), _
                                     Request.QueryString("suc_2"), Request.QueryString("ddr_1"), Request.QueryString("ddr_2"), _
                                     Request.QueryString("tdc_1"), Request.QueryString("tdc_2"), Request.QueryString("mto1"), _
                                     Request.QueryString("mto2"), Request.QueryString("fec_1"), Request.QueryString("fec_2"), _
                                     Request.QueryString("fec_prc_1"), Request.QueryString("fec_prc_2"))

            Dim dt As DataTable

            dt = cb_310

            Dim neg As New Microsoft.Reporting.WebForms.ReportDataSource
            neg = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_co_dev_doc_con_codi_aut", dt)
            ReportViewer1.LocalReport.DataSources.Add(neg)
            ReportViewer1.DataBind()

        ElseIf Request.QueryString("tipo") = 4 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Reporte_rad_fact.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\Reporte_rad_fact.rdlc"

            cb_rad = get_rad.GetData(DateTime.Parse(Request.QueryString("feg")))
            Dim feg As ReportParameter = New ReportParameter("Fec_Gen", Request.QueryString("feg"))
            ReportViewer1.LocalReport.SetParameters(New ReportParameter() {feg})

            Dim dt As DataTable

            dt = cb_rad

            Dim Rad As New Microsoft.Reporting.WebForms.ReportDataSource
            Rad = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_Reporte_Radicar_Facturas", dt)
            ReportViewer1.LocalReport.DataSources.Add(Rad)
            ReportViewer1.DataBind()

        End If

        ReportViewer1.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            Response.Expires = -1
            Genera_Informes()

        End If
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")
    End Sub
End Class
