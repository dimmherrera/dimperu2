Imports Microsoft.Reporting.WebForms
Imports System.Data

Partial Class Modulos_Pagos_rightframe_archivos_InformeDePagos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1

        If Not Page.IsPostBack Then

            Try
                ' Genera_reporte()
                If Request.QueryString("id") <> "" Then
                    Genera_reporte()
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Sub Genera_reporte()

        Try

            Dim Ing As New DataSet_Pagos.Sp_Reporte_Pago_Directo_CabeceraDataTable
            Dim Tab_Ing As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_Directo_CabeceraTableAdapter

            Dim Dpo As New DataSet_Pagos.Sp_Reporte_Pago_Directo_DpoDataTable
            Dim Tab_Dpo As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_Directo_DpoTableAdapter

            Dim Ing_Sec As New DataSet_Pagos.Sp_Reporte_Pago_Directo_Doctos_PagadosDataTable
            Dim Tab_Ing_Sec As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_Directo_Doctos_PagadosTableAdapter

            Dim cxc As New DataSet_Pagos.Sp_Reporte_Pago_Directo_CxC_PagadosDataTable
            Dim Tab_cxc As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_Directo_CxC_PagadosTableAdapter


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Pagos\Reportes\Informe_Pagos.rdlc"

            Dim id As Integer
            'id = 1
            id = Request.QueryString("id")

            Ing = Tab_Ing.GetData(id)
            Dpo = Tab_Dpo.GetData(id)
            Ing_Sec = Tab_Ing_Sec.GetData(id)
            cxc = Tab_cxc.GetData(id)


            Dim dt As DataTable

            dt = Ing

            Dim I As New Microsoft.Reporting.WebForms.ReportDataSource
            I = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Directo_Cabecera", dt)


            Dim dt2 As DataTable

            dt2 = Dpo

            Dim D As New Microsoft.Reporting.WebForms.ReportDataSource

            D = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Directo_Dpo", dt2)

            Dim dt3 As DataTable

            dt3 = Ing_Sec

            Dim S As New Microsoft.Reporting.WebForms.ReportDataSource
            S = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Directo_Doctos_Pagados", dt3)

            Dim dt4 As DataTable

            dt4 = cxc

            Dim C As New Microsoft.Reporting.WebForms.ReportDataSource
            C = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Directo_CxC_Pagados", dt4)

            ReportViewer1.LocalReport.DataSources.Add(D)
            ReportViewer1.LocalReport.DataSources.Add(I)
            ReportViewer1.LocalReport.DataSources.Add(S)
            ReportViewer1.LocalReport.DataSources.Add(C)

            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

End Class
