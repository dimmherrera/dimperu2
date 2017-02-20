Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_Reportes_de_Simulacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            DP_Informes.ClearSelection()
        End If

    End Sub

    Protected Sub DP_Informes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Informes.SelectedIndexChanged

        Select Case DP_Informes.SelectedValue
            Case "1"
                CartaSolicitud()
            Case "2"
                Anexo()
            Case "3"
                CartaNotificacion()
            Case "4"
                AceptacionEndoso()
        End Select

    End Sub

    Private Sub CartaSolicitud()

        Dim ID_OPE_RPT As String = Request.QueryString("ID_OPE")

        Dim data As New Dataset_Operacion.Sp_Reporte_SimulaciónDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_SimulaciónTableAdapter

        Dim dsi As New Dataset_Operacion.Sp_Reporte_Simulación_DocDataTable
        Dim tab_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Simulación_DocTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\CARTA_SOLICITUD_DESCUENTO_FACTURAS.rdlc"

        data = tab.GetData(ID_OPE_RPT)
        dsi = tab_doc.GetData(ID_OPE_RPT)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = data

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Data_Simulacion_Sp_Reporte_Simulación", dt)

        Dim dt2 As DataTable

        dt2 = dsi

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("Data_Dsi_Sp_Reporte_Simulación_Doc", dt2)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.LocalReport.DataSources.Add(rds)

        ReportViewer1.DataBind()

    End Sub

    Private Sub Anexo()

        Dim ID_OPE_RPT As String = Request.QueryString("ID_OPE")
        Dim data As New Dataset_Operacion.Sp_Reporte_SimulaciónDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_SimulaciónTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\ANEXO_ENDOSO_1.rdlc"

        data = tab.GetData(ID_OPE_RPT)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = data

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Data_Simulacion_Sp_Reporte_Simulación", dt)

        ReportViewer1.LocalReport.DataSources.Add(rds)

        ReportViewer1.DataBind()

    End Sub

    Private Sub CartaNotificacion()

        Dim ID_OPE_RPT As String = Request.QueryString("ID_OPE")

        Dim data As New Dataset_Operacion.Sp_Reporte_SimulaciónDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_SimulaciónTableAdapter

        Dim dsi As New Dataset_Operacion.Sp_Reporte_Simulación_DocDataTable
        Dim tab_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Simulación_DocTableAdapter

        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\CARTA_NOTIFICACION.rdlc"

        data = tab.GetData(ID_OPE_RPT)
        dsi = tab_doc.GetData(ID_OPE_RPT)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = data

        Dim dt2 As DataTable

        dt2 = dsi

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Simulación", dt)
        doc = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Simulación_Doc", dt2)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.LocalReport.DataSources.Add(rds)

        ReportViewer1.DataBind()


    End Sub

    Private Sub AceptacionEndoso()

        Dim ID_OPE_RPT As String = Request.QueryString("ID_OPE")

        Dim data As New Dataset_Operacion.Sp_Reporte_SimulaciónDataTable
        Dim tab As New Dataset_OperacionTableAdapters.Sp_Reporte_SimulaciónTableAdapter

        Dim dsi As New Dataset_Operacion.Sp_Reporte_Simulación_DocDataTable
        Dim tab_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Simulación_DocTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\COMPROMISO_PAGO.rdlc"

        data = tab.GetData(ID_OPE_RPT)
        dsi = tab_doc.GetData(ID_OPE_RPT)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = data

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("Data_Simulacion_Sp_Reporte_Simulación", dt)

        Dim dt2 As DataTable

        dt2 = dsi

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("Data_Dsi_Sp_Reporte_Simulación_Doc", dt2)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.LocalReport.DataSources.Add(rds)

        ReportViewer1.DataBind()

    End Sub

End Class
