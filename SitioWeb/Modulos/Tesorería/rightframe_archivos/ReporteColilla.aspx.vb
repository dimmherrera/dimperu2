Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Tesorería_rightframe_archivos_ReporteColilla
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                GeneraReporte()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Sub GeneraReporte()
        Try

            Dim ing As Integer

            Dim Cab As New DataSet_Cheques.sp_Reporte_Colilla_ChequesDataTable
            Dim Cabecera As New DataSet_ChequesTableAdapters.sp_Reporte_Colilla_ChequesTableAdapter

            Dim Det As New DataSet_Cheques.sp_Reporte_Colilla_DocumentosDataTable
            Dim Detalle As New DataSet_ChequesTableAdapters.sp_Reporte_Colilla_DocumentosTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Tesorería\Reportes\Reporte_Colilla.rdlc"

            ing = Request.QueryString("id")


            Cab = Cabecera.GetData(ing)

            For i = 0 To Cab.Count - 1
                Cab.Item(i).cant_cheques = 1
            Next
            Det = Detalle.GetData(ing)

            Dim dt As DataTable

            dt = Cab

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cheques_sp_Reporte_Colilla_Cheques", dt)


            Dim dt2 As DataTable

            dt2 = Det
            Dim B As New Microsoft.Reporting.WebForms.ReportDataSource
            B = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cheques_sp_Reporte_Colilla_Documentos", dt2)



            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.LocalReport.DataSources.Add(B)

            ReportViewer1.DataBind()


        Catch ex As Exception

        End Try
    End Sub
End Class
