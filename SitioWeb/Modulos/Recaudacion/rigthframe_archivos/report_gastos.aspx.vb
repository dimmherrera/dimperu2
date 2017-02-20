Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Recaudacion_rigthframe_archivos_report_gastos

    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
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
            Try

                hf_nro_hoja.Value = Request.QueryString("nro_hoja")
                Genera_reporte()


            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Sub Genera_reporte()

        Dim data As New DataSet_Pagos.Sp_Reporte_Gastos_recDataTable
        Dim tab As New DataSet_PagosTableAdapters.Sp_Reporte_Gastos_recTableAdapter

        Dim lr As New LocalReport

        ReportViewer1.Reset()

        ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Reporte_gastos_Rec.rdlc"





        data = tab.GetData(Val(hf_nro_hoja.Value))

        Dim dt As DataTable

        dt = data

        lr.DataSources.Add(New ReportDataSource("Sim", dt))




        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource


        rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Gastos_rec", dt)


        ReportViewer1.LocalReport.DataSources.Insert(0, rds)
        ReportViewer1.DataBind()

    End Sub



End Class
