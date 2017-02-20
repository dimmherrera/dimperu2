Imports Microsoft.Reporting.WebForms
Imports System.Data

Partial Class Modulos_Tesorería_rightframe_archivos_ReporteIngresoNomina
    Inherits System.Web.UI.Page

    Dim msj As New ClsMensaje
    Dim rw As New FuncionesGenerales.RutinasWeb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1

                GeneraReporte()

            End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub GeneraReporte()
        Try
            Dim ing As New DataSet_Cheques.sp_Reporte_Nomina_IngresoDataTable
            Dim ingreso As New DataSet_ChequesTableAdapters.sp_Reporte_Nomina_IngresoTableAdapter


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = "Modulos\Tesorería\Reportes\InformeIngresoNomina.rdlc"

            Dim nomina As Integer
            nomina = Request.QueryString("Nomina")

            ing = ingreso.GetData(nomina)

            Dim dt As DataTable

            dt = ing

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cheques_sp_Reporte_Nomina_Ingreso", dt)

            ReportViewer1.LocalReport.DataSources.Add(A)

            ReportViewer1.DataBind()

            If ing.Count <= 0 Then
                msj.Mensaje(Page, "Atención", "No se encontro nómina con el N° ingresado", ClsMensaje.TipoDeMensaje._Exclamacion, LinkButton1.ClientID, False)
                Exit Sub

            End If


        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        rw.ClosePag(Page)
    End Sub

End Class
