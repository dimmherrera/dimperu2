Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Recaudacion_rigthframe_archivos_report_nomina_nce
    Inherits System.Web.UI.Page
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

            If caso = 1 Then

                cli1.Value = Request.QueryString("cli1")
                cli2.Value = Request.QueryString("cli2")

                fec1.Value = Request.QueryString("fec1")
                fec2.Value = Request.QueryString("fec2")


                fact.Value = Request.QueryString("fact")
                ReportViewer1.Reset()
                Genera_reporte()
            Else
                ReportViewer1.Reset()
                Genera_reporte_por_id()
            End If





        End If

    End Sub

    Public Sub Genera_reporte()

        Dim data As New DataSet_Pagos.sp_inf_nomina_nceDataTable
        Dim tab As New DataSet_PagosTableAdapters.sp_inf_nomina_nceTableAdapter

        Dim lr As New LocalReport

        ReportViewer1.Reset()

        ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\report_nomina_dnc.rdlc"

        data = tab.GetData(cli1.Value, cli2.Value, fec1.Value, fec2.Value, fact.Value)

        Dim dt As DataTable

        dt = data

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_inf_nomina_nce", dt)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.DataBind()


    End Sub

    Public Sub Genera_reporte_por_id()

        Dim data As New DataSet_Pagos.sp_inf_nomina_doctos_nce_por_idDataTable
        Dim tab As New DataSet_PagosTableAdapters.sp_inf_nomina_doctos_nce_por_idTableAdapter

        Dim ds As New DataSet_Pagos.sp_inf_nomina_doctos_nce_por_idDataTable
        Dim lr As New LocalReport

        ReportViewer1.Reset()

        ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Report_nomina_nce_por_id.rdlc"

        For i = 1 To Coll_DOC.Count

            data = tab.GetData(CLng(Coll_DOC.Item(i)))

            If i = 1 Then
                ds = data
            Else
                ds.Merge(data)
            End If


            data = New DataSet_Pagos.sp_inf_nomina_doctos_nce_por_idDataTable

        Next

        Dim dt As DataTable

        dt = ds

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource

        doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_inf_nomina_doctos_nce_por_id", dt)

        ReportViewer1.LocalReport.DataSources.Add(doc)
        ReportViewer1.DataBind()


    End Sub
End Class
