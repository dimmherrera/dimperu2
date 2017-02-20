Imports Microsoft.Reporting
Imports CapaDatos
Imports System.Data

Partial Class InformeDNC
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim fmt As New FuncionesGenerales.ClsLocateInfo

    Dim fc As New FuncionesGenerales.FComunes
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            If Not Page.IsPostBack Then
                If Request.QueryString("rut_dsd") <> "" Then
                    GeneraInforme()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GeneraInforme()
        Dim dnc As New DataSet_Cuentas.sp_Reporte_Devuelve_NCEDataTable
        Dim d As New DataSet_CuentasTableAdapters.sp_Reporte_Devuelve_NCETableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = ("Modulos\Adm. Cuentas\Reporte_Cuenta\ReporteDNC.rdlc")

        Dim rut_desde As String
        Dim rut_hsta As String
        Dim fecha_desde As String
        Dim fecha_hasta As String
        Dim moneda_desde As Integer
        Dim moneda_hasta As Integer
        Dim RG As New FuncionesGenerales.FComunes

        rut_desde = Format(CLng(Request.QueryString("rut_dsd")), var.FMT_RUT)
        rut_hsta = Format(CLng(Request.QueryString("rut_hst")), var.FMT_RUT)
        fecha_desde = CDate(Request.QueryString("Fecha_dsd"))
        fecha_hasta = CDate(Request.QueryString("Fecha_hst"))
        moneda_desde = Request.QueryString("Moneda_dsd")
        moneda_hasta = Request.QueryString("Moneda_hst")

        dnc = d.GetData(rut_desde, rut_hsta, fecha_desde, fecha_hasta, moneda_desde, moneda_hasta)
        Dim Doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim datos As DataTable
        datos = dnc
        Doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cuentas_sp_Reporte_Devuelve_NCE", datos)

        ReportViewer1.LocalReport.DataSources.Add(Doc)

        ReportViewer1.DataBind()

    End Sub


End Class
