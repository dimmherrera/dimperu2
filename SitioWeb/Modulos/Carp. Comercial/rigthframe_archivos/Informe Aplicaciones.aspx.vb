Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Comercial_rigthframe_archivos_Informe_Aplicaciones

    Inherits System.Web.UI.Page
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1
            genera_reportes()
        End If

    End Sub

    Public Sub genera_reportes()
        Try



            Dim cx As New DataSet_Pagos.sp_reporte_detalle_apli_cxpDataTable
            Dim cxp As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_cxpTableAdapter

            Dim cc As New DataSet_Pagos.sp_reporte_detalle_apli_CxcDataTable
            Dim cxc As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_CxcTableAdapter

            Dim nc As New DataSet_Pagos.sp_reporte_detalle_apli_nceDataTable
            Dim nce As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_nceTableAdapter

            Dim dc As New DataSet_Pagos.sp_reporte_detalle_apli_docDataTable
            Dim Doc As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_docTableAdapter

            Dim eg As New DataSet_Pagos.sp_reporte_detalle_giroDataTable
            Dim egr As New DataSet_PagosTableAdapters.sp_reporte_detalle_giroTableAdapter

            Dim ap As New DataSet_Pagos.sp_reporte_detalle_apli_aplDataTable
            Dim apl As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_aplTableAdapter

            Dim ap_giro As New DataSet_Pagos.sp_reporte_detalle_giro_egresoDataTable
            Dim apl_giro As New DataSet_PagosTableAdapters.sp_reporte_detalle_giro_egresoTableAdapter


            Dim doc_ap As New DataSet_Pagos.sp_reporte_detalle_apli_doc_excDataTable
            Dim doc_apl As New DataSet_PagosTableAdapters.sp_reporte_detalle_apli_doc_excTableAdapter
            'Cuentas X Pagar

            cx = cxp.GetData(Request.QueryString("id_apl"), 1)

            'Cuentas X Cobrar

            cc = cxc.GetData(Request.QueryString("id_apl"))

            For Each p In cc
                If p.dev_pnu_mon = 1 Then
                    p.formato_moneda = fmt.FCMSD
                ElseIf p.dev_pnu_mon = 2 Then
                    p.formato_moneda = fmt.FCMCD4
                ElseIf p.dev_pnu_mon > 2 Then
                    p.formato_moneda = fmt.FCMCD
                End If
            Next
            'Documentos No Cedido

            nc = nce.GetData(Request.QueryString("id_apl"), 2)

            'Documentos

            dc = Doc.GetData(Request.QueryString("id_apl"), 3)


            'Egresos

            eg = egr.GetData(Request.QueryString("rut"), Request.QueryString("id_apl"), 1, 2)

            'Aplicacion

            ap = apl.GetData(Request.QueryString("id_apl"))

            ap_giro = apl_giro.GetData(Request.QueryString("id_apl"))

            'Documento pagado con excedentes

            doc_ap = doc_apl.GetData(Request.QueryString("id_apl"), 2)


            'Documentos cancelados

            Dim doc_pgd As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = doc_ap

            doc_pgd = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_doc_exc", dt)

            'Cuentas X Pagar
            Dim cxp1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt2 As DataTable

            dt2 = cx

            cxp1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_cxp", dt2)

            'Documentos No Cedido

            Dim nce1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt3 As DataTable

            dt3 = nc

            nce1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_nce", dt3)

            'Documentos

            Dim doc1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt4 As DataTable

            dt4 = dc

            doc1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_doc", dt4)

            'Egresos

            Dim egr1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt5 As DataTable

            dt5 = eg

            egr1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_giro", dt5)

            'Aplicacion

            Dim apl1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt6 As DataTable

            dt6 = ap

            apl1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_apl", dt6)

            'Cuentas Cobrar 

            Dim cxc1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt7 As DataTable

            dt7 = cc

            cxc1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_apli_Cxc", dt7)


            Dim apl_giro1 As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt8 As DataTable

            dt8 = ap_giro

            apl_giro1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_reporte_detalle_giro_egreso", dt8)


            Dim lr As New LocalReport

            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\Reporteaplicaciones.rdlc"

            ReportViewer1.LocalReport.DataSources.Add(doc1)
            ReportViewer1.LocalReport.DataSources.Add(nce1)
            ReportViewer1.LocalReport.DataSources.Add(cxp1)
            ReportViewer1.LocalReport.DataSources.Add(egr1)
            ReportViewer1.LocalReport.DataSources.Add(apl1)
            ReportViewer1.LocalReport.DataSources.Add(cxc1)
            ReportViewer1.LocalReport.DataSources.Add(doc_pgd)
            ReportViewer1.LocalReport.DataSources.Add(apl_giro1)

            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try


    End Sub

End Class
