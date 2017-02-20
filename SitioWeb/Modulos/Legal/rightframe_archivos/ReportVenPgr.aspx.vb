Imports Microsoft.Reporting
Imports CapaDatos
Imports System.Data

Partial Class ReportVenPgr
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


    Public Sub GeneraReporte()
        Try
            Dim tipoPgr As New DataSet_Legal.Sp_Reporte_Devuelve_TipoPgrDataTable
            Dim tp As New DataSet_LegalTableAdapters.Sp_Reporte_Devuelve_TipoPgrTableAdapter
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()
            Dim Fecha_dsd As String
            Dim Fecha_hst As String
            Dim Eje_dsd As Integer
            Dim Eje_hst As Integer
            Dim Suc_dsd As Integer
            Dim Suc_hst As Integer
            Dim cli_dsd As Integer
            Dim cli_hst As Integer
            Dim TipoPgr_dsd As Integer
            Dim TipoPgr_hst As Integer
            Dim Fecha_Proc As String
            Dim Fecha_Proc_hst As String
            Dim dias As Integer
            Dim Pagare As String
            Dim nomsucursal As String
            Dim ejecutivo As String

            Fecha_dsd = Request.QueryString("f_desde")
            Fecha_hst = Request.QueryString("f_hasta")
            Eje_dsd = Request.QueryString("eje_dsd")
            Eje_hst = Request.QueryString("eje_hst")
            Suc_dsd = Request.QueryString("suc_dsd")
            Suc_hst = Request.QueryString("suc_hst")
            cli_dsd = Request.QueryString("tpcli_dsd")
            cli_hst = Request.QueryString("tpcli_hst")
            TipoPgr_dsd = Request.QueryString("tppgr_dsd")
            TipoPgr_hst = Request.QueryString("tppgr_hst")
            Fecha_Proc = Request.QueryString("Fechaproc")
            Fecha_Proc_hst = Request.QueryString("FechaPro_Hst")
            dias = Request.QueryString("dias")
            Pagare = Request.QueryString("pagare")
            nomsucursal = Request.QueryString("suc")
            ejecutivo = Request.QueryString("eje")

            If Request.QueryString("reporte") = 2 Then
                ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InfoVenPgr.rdlc")

            ElseIf Request.QueryString("reporte") = 1 Then
                ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InformeTipoPgr.rdlc")
            End If

            tipoPgr = tp.GetData(Fecha_dsd, Fecha_hst, Eje_dsd, Eje_hst, Suc_dsd, Suc_hst, cli_dsd, cli_hst, TipoPgr_dsd, TipoPgr_hst, Fecha_Proc _
                          , Fecha_Proc_hst, dias, Pagare, nomsucursal, ejecutivo)

            Dim dt As DataTable

            dt = tipoPgr

            Dim TpPgr As New Microsoft.Reporting.WebForms.ReportDataSource
            TpPgr = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_Sp_Reporte_Devuelve_TipoPgr", dt)
            ReportViewer1.LocalReport.DataSources.Add(TpPgr)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try
    End Sub



End Class

