Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_reporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Try

                Response.Expires = -1

                If Request.QueryString("rut").Trim <> "" And Request.QueryString("id").Trim <> "" Then
                    Genera_reporte()
                End If

            Catch ex As Exception

            End Try

        End If

    End Sub



    Public Sub Genera_reporte()


        Dim ldc As New DataLineaCredito.Sp_Reporte_LDCDataTable
        Dim tab_ldc As New DataLineaCreditoTableAdapters.Sp_Reporte_LDCTableAdapter

        Dim ldc_ant As New DataLineaCredito.Sp_Reporte_LDC_AnticipoDataTable
        Dim tab_ldc_ant As New DataLineaCreditoTableAdapters.Sp_Reporte_LDC_AnticipoTableAdapter

        Dim sldc As New DataLineaCredito.Sp_Reporte_LDC_Sublinea_docDataTable
        Dim tab_sldc As New DataLineaCreditoTableAdapters.Sp_Reporte_LDC_Sublinea_docTableAdapter

        Dim sldc_deu As New DataLineaCredito.Sp_Reporte_LDC_Sublinea_deuDataTable
        Dim tab_sldc_deu As New DataLineaCreditoTableAdapters.Sp_Reporte_LDC_Sublinea_deuTableAdapter

        Dim comi_cli As New DataLineaCredito.Sp_Reporte_ComisionGastosDataTable
        Dim tab_comi_cli As New DataLineaCreditoTableAdapters.Sp_Reporte_ComisionGastosTableAdapter


        Dim gast_cli As New DataLineaCredito.Sp_Reporte_Gastos_Por_ClienteDataTable
        Dim tab_gast_cli As New DataLineaCreditoTableAdapters.Sp_Reporte_Gastos_Por_ClienteTableAdapter
        'DataLineaCredito_Sp_Reporte_Gastos_Por_Cliente


        Dim lr As New LocalReport
        ReportViewer1.LocalReport.DataSources.Clear()

        Dim id As Integer = Request.QueryString("id").Trim
        Dim FMT As New FuncionesGenerales.Variables

        comi_cli = tab_comi_cli.GetData(Format(CLng(Request.QueryString("rut").Trim), FMT.FMT_RUT))
        gast_cli = tab_gast_cli.GetData(Format(CLng(Request.QueryString("rut").Trim), FMT.FMT_RUT))
        ldc = tab_ldc.GetData(Format(CLng(Request.QueryString("rut").Trim), FMT.FMT_RUT), id)
        ldc_ant = tab_ldc_ant.GetData(id)
        sldc = tab_sldc.GetData(id)
        sldc_deu = tab_sldc_deu.GetData(id)


        Dim dt As DataTable

        dt = ldc

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_LDC", dt))

        Dim dt2 As DataTable

        dt2 = ldc_ant

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Anticipo", dt2))

        Dim dt3 As DataTable

        dt3 = sldc

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Sublinea_doc", dt3))

        Dim dt4 As DataTable

        dt4 = sldc_deu

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Sublinea_deu", dt4))

        Dim dt5 As DataTable

        dt5 = comi_cli

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_ComisionGastos", dt5))

        Dim dt6 As DataTable

        dt6 = gast_cli

        lr.DataSources.Add(New ReportDataSource("DataLineaCredito_Sp_Reporte_Gastos_Por_Cliente", dt6))

        'DataLineaCredito_Sp_Reporte_LDC'

        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim ant As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim deu As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim cli As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim gas As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt7 As DataTable

        dt7 = ldc

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_LDC", dt7)

        Dim dt8 As DataTable

        dt8 = ldc_ant

        ant = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Anticipo", dt8)

        Dim dt9 As DataTable

        dt9 = sldc

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Sublinea_doc", dt9)

        Dim dt10 As DataTable

        dt10 = sldc_deu

        deu = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_LDC_Sublinea_deu", dt10)

        Dim dt11 As DataTable

        dt11 = comi_cli

        cli = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_ComisionGastos", dt11)

        Dim dt12 As DataTable

        dt12 = gast_cli

        gas = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_Sp_Reporte_Gastos_Por_Cliente", dt12)

        ReportViewer1.LocalReport.DataSources.Add(rds)
        ReportViewer1.LocalReport.DataSources.Add(ant)
        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.LocalReport.DataSources.Add(deu)
        ReportViewer1.LocalReport.DataSources.Add(cli)
        ReportViewer1.LocalReport.DataSources.Add(gas)

        ReportViewer1.DataBind()

    End Sub

End Class
