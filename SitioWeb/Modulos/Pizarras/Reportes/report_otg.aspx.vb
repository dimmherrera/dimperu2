Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports FuncionesGenerales.RutinasWeb
Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_report_otg
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

        Dim data As New Dataset_Operacion.Sp_Reporte_OtorgamientoDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_OtorgamientoTableAdapter

        Dim docu As New Dataset_Operacion.Sp_Reporte_Otorgamiento_DocDataTable
        Dim tab_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Otorgamiento_DocTableAdapter

        Dim doc_apl As New Dataset_Operacion.sp_reporte_sim_doc_aplDataTable
        Dim tab_doc_apl As New Dataset_OperacionTableAdapters.sp_reporte_sim_doc_aplTableAdapter


        Dim cxc_apl As New Dataset_Operacion.sp_reporte_sim_cxc_aplDataTable
        Dim tab_cxc_apl As New Dataset_OperacionTableAdapters.sp_reporte_sim_cxc_aplTableAdapter


        ReportViewer1.LocalReport.DataSources.Clear()

        If Me.RadioButtonList1.SelectedValue = 0 Then
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = "Modulos\Pizarras\Reportes\ReportOtgDet.rdlc"
        Else
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = "Modulos\Pizarras\Reportes\ReporteOtg.rdlc"

        End If

        Dim fmt As New FuncionesGenerales.Variables

        data = tab.GetData(NroOperacion)

        docu = tab_doc.GetData(NroOperacion)

        doc_apl = tab_doc_apl.GetData(NroOperacion, 2)

        cxc_apl = tab_cxc_apl.GetData(NroOperacion, 2)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim doc_ap As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim cxc_ap As New Microsoft.Reporting.WebForms.ReportDataSource


        Dim dt As DataTable

        dt = data

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_c", dt)

        Dim dt2 As DataTable

        dt2 = docu

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Otorgamiento_Doc", dt2)

        Dim dt3 As DataTable

        dt3 = doc_apl

        doc_ap = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_sim_doc_apl", dt3)

        Dim dt4 As DataTable

        dt4 = cxc_apl

        cxc_ap = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_sim_cxc_apl", dt4)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        'ReportViewer1.DataBind()

        ReportViewer1.LocalReport.DataSources.Add(rds)
        ReportViewer1.LocalReport.DataSources.Add(doc_ap)
        ReportViewer1.LocalReport.DataSources.Add(cxc_ap)

        ReportViewer1.DataBind()

    End Sub

End Class
