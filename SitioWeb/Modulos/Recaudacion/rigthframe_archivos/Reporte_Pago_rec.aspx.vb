Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Recaudacion_rigthframe_archivos_Reporte_Pago_rec
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
                cg.EjecutivosDevuelve(Dr_Rec, CodEje, 14)
                'Genera reporte en blanco
                Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
                Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
                Dim data As New DataSet_Pagos.Sp_Reporte_Pago_RecaudaciónDataTable
                Dim tab As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_RecaudaciónTableAdapter

                Me.Txt_Fec_Rec.Text = Date.Now.ToShortDateString
                data = tab.GetData("01/01/1900", 1, 1, "A", 1)

                Dim dt As DataTable

                dt = data

                rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Recaudación", dt)
                ReportViewer1.LocalReport.DataSources.Insert(0, rds)
                ReportViewer1.DataBind()

                If Request.QueryString("fecha") <> "" Then
                    Me.Txt_Fec_Rec.Text = Request.QueryString("fecha")
                    Me.Dr_Rec.SelectedValue = Request.QueryString("eje")
                    Me.rb_hora.SelectedValue = Request.QueryString("hora")

                    Genera_reporte()

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Sub Genera_reporte()

        Dim data As New DataSet_Pagos.Sp_Reporte_Pago_RecaudaciónDataTable
        Dim tab As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_RecaudaciónTableAdapter

        Dim dsi As New DataSet_Pagos.Sp_Reporte_Pago_RecaudaciónDataTable
        Dim tab_doc As New DataSet_PagosTableAdapters.Sp_Reporte_Pago_RecaudaciónTableAdapter

        Dim lr As New LocalReport

        ReportViewer1.Reset()

        ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Reporte_Hoja_Recaudación.rdlc"

        data = tab.GetData(Me.Txt_Fec_Rec.Text, Me.Dr_Rec.SelectedValue, 1, Me.rb_hora.SelectedValue, 1)
        dsi = tab_doc.GetData(Me.Txt_Fec_Rec.Text, Me.Dr_Rec.SelectedValue, 1, Me.rb_hora.SelectedValue, 2)

        Dim dt As DataTable

        dt = data

        lr.DataSources.Add(New ReportDataSource("Sim", dt))

        dsi.Merge(data)

        Dim doc As New Microsoft.Reporting.WebForms.ReportDataSource
        Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

      

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Recaudación", dt)
        doc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_Sp_Reporte_Pago_Recaudación", dt)

        ReportViewer1.LocalReport.DataSources.Insert(0, doc)
        ReportViewer1.DataBind()

    End Sub

    Protected Sub ib_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_buscar.Click
        Genera_reporte()
    End Sub
End Class
