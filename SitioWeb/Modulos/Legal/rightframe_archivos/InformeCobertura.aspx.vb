Imports Microsoft.Reporting
Imports ClsSession.ClsSession
Imports System.Data

Partial Class InformeCobertura
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim fc As New FuncionesGenerales.FComunes
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                GeneraInforme()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GeneraInforme()
        Try
            Dim lista As New DataSet_Legal.sp_Reporte_lista_coberturaDataTable
            Dim d As New DataSet_LegalTableAdapters.sp_Reporte_lista_coberturaTableAdapter
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/ListaCobertura.rdlc")

            Dim fecha As String
            Dim rutdsd As String
            Dim ruthst As String
            Dim sucdsd As Integer
            Dim suchst As Integer
            Dim user As Integer
            fecha = fc.FUNFechaJul(Request.QueryString("fecha"))
            rutdsd = Format(CLng(Request.QueryString("rutdsd")), var.FMT_RUT)
            ruthst = Format(CLng(Request.QueryString("ruthst")), var.FMT_RUT)
            sucdsd = Request.QueryString("sucdsd")
            suchst = Request.QueryString("suchst")
            user = Request.QueryString("user")
            lista = d.GetData(fecha, rutdsd, ruthst, sucdsd, suchst, user)

            Dim dt As DataTable

            dt = lista

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_sp_Reporte_lista_cobertura", dt)
            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try
    End Sub


End Class
