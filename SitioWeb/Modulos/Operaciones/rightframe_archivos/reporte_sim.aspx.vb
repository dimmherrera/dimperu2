Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports System.IO
Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_reporte_sim
    Inherits System.Web.UI.Page

    Dim fc As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Response.Expires = -1
            Try
                Genera_reporte()
            Catch ex As Exception

            End Try

        End If

    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Genera_reporte()
    End Sub

    Public Sub Genera_reporte()

        Dim data As New Dataset_Operacion.Sp_Reporte_SimulaciónDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_SimulaciónTableAdapter

        Dim dsi As New Dataset_Operacion.Sp_Reporte_Simulación_DocDataTable
        Dim tab_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Simulación_DocTableAdapter

        Dim doc_apl As New Dataset_Operacion.sp_reporte_sim_doc_aplDataTable
        Dim tab_doc_apl As New Dataset_OperacionTableAdapters.sp_reporte_sim_doc_aplTableAdapter


        Dim cxc_apl As New Dataset_Operacion.sp_reporte_sim_cxc_aplDataTable
        Dim tab_cxc_apl As New Dataset_OperacionTableAdapters.sp_reporte_sim_cxc_aplTableAdapter

        Dim lr As New LocalReport
        ReportViewer1.LocalReport.DataSources.Clear()
        If Me.RadioButtonList1.SelectedValue = 0 Then
            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("reportsim.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Reportsim.rdlc"
        Else
            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("reportesimu.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Reportesimu.rdlc"
        End If

        data = tab.GetData(ID_OPE_RPT)
        dsi = tab_doc.GetData(ID_OPE_RPT)
        doc_apl = tab_doc_apl.GetData(ID_OPE_RPT, 1)
        cxc_apl = tab_cxc_apl.GetData(ID_OPE_RPT, 1)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim doc_ap As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim cxc_ap As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = data

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Simulación", dt)

        Dim dt2 As DataTable

        dt2 = dsi

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Simulación_Doc", dt2)

        Dim dt3 As DataTable

        dt3 = doc_apl

        doc_ap = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_sim_doc_apl", dt3)

        Dim dt4 As DataTable

        dt4 = cxc_apl

        cxc_ap = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_sim_cxc_apl", dt4)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.LocalReport.DataSources.Add(rds)
        ReportViewer1.LocalReport.DataSources.Add(doc_ap)
        ReportViewer1.LocalReport.DataSources.Add(cxc_ap)

        'Dim rv As New Reporting
        'rv.InsertaLogo(ReportViewer1)

        ReportViewer1.DataBind()
        'ReportViewer1.LocalReport.Refresh()

    End Sub

End Class




