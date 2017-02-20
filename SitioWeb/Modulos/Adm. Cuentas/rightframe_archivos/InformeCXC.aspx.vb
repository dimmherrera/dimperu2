Imports Microsoft.Reporting
Imports CapaDatos
Imports System.Data

Partial Class InformeCXC
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim FC As New FuncionesGenerales.FComunes
    Dim msj As New ClsMensaje
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
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Public Sub GeneraInforme()
        Try
            Dim cxc As New DataSet_Cuentas.sp_Reporte_Devuelve_CXC_ClienteDataTable
            Dim c As New DataSet_CuentasTableAdapters.sp_Reporte_Devuelve_CXC_ClienteTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()
            If Request.QueryString("informe") = 1 Then
                ReportViewer1.LocalReport.ReportPath = "Modulos\Adm. Cuentas\Reporte_Cuenta\InformeCXC.rdlc"
            ElseIf Request.QueryString("informe") = 2 Then
                ReportViewer1.LocalReport.ReportPath = "Modulos\Adm. Cuentas\Reporte_Cuenta\ReporteCXC.rdlc"
            End If

            Dim rut_dsd As String
            Dim rut_hst As String
            Dim fecha_dsd As String
            Dim fecha_hst As String
            Dim moneda_dsd As Integer
            Dim moneda_hst As Integer
            Dim est_dsd As Integer
            Dim est_hst As Integer
            Dim numope As Integer
            Dim numdoc As String
            Dim tpcuenta As Integer
            Dim otratpcuenta As Integer
            Dim RG As New FuncionesGenerales.FComunes

            rut_dsd = Format(CLng(Request.QueryString("rut_dsd")), var.FMT_RUT)
            rut_hst = Format(CLng(Request.QueryString("rut_hst")), var.FMT_RUT)
            fecha_dsd = Request.QueryString("Fecha_dsd") 'FC.FUNFechaJul(Request.QueryString("Fecha_dsd"))
            fecha_hst = Request.QueryString("Fecha_hst") 'FC.FUNFechaJul(Request.QueryString("Fecha_hst"))
            moneda_dsd = Request.QueryString("Moneda_dsd")
            moneda_hst = Request.QueryString("Moneda_hst")
            est_dsd = Request.QueryString("Est_dsd")
            est_hst = Request.QueryString("Est_hst")

            numope = Request.QueryString("numope")
            numdoc = Request.QueryString("numdoc")
            tpcuenta = Request.QueryString("tpct")
            otratpcuenta = Request.QueryString("OtraTPct")
            cxc = c.GetData(rut_dsd, rut_hst, numope, numdoc, fecha_dsd, fecha_hst, moneda_dsd, moneda_hst, est_dsd, est_hst, tpcuenta, otratpcuenta)
            'cxc = c.GetData("000000000000", "999999999999", 0, 0, "1900/01/01", "2999/12/30", 0, 999, 0, 999, 5, 0)
            Dim CuentasXC As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = cxc

            CuentasXC = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cuentas_sp_Reporte_Devuelve_CXC_Cliente", dt)

            ReportViewer1.LocalReport.DataSources.Add(CuentasXC)


            ReportViewer1.DataBind()

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub



End Class

