Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Recaudacion_rigthframe_archivos_reporte_hoja_ruta
    Inherits System.Web.UI.Page
    Dim fc As New FuncionesGenerales.FComunes

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Response.Expires = -1
            fecha.Value = Request.QueryString("fecha")
            eje.Value = Request.QueryString("eje")
            suc1.Value = Request.QueryString("suc1")
            suc2.Value = Request.QueryString("suc2")
            am_pm.Value = Request.QueryString("am_pm")

            Genera_reporte()


        End If

    End Sub

    Public Sub Genera_reporte()

        Dim data As New DataSet_Pagos.sp_inf_hoja_recaudacionDataTable
        Dim tab As New DataSet_PagosTableAdapters.sp_inf_hoja_recaudacionTableAdapter

        Dim lr As New LocalReport

        ReportViewer1.Reset()

        ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Hoja_Ruta.rdlc"

        data = tab.GetData(fecha.Value, am_pm.Value, eje.Value, suc1.Value, suc2.Value)

        Dim dt As DataTable

        dt = data

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_inf_hoja_recaudacion", dt)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.DataBind()


    End Sub



End Class
