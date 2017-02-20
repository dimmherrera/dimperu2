Imports Microsoft.Reporting
Imports System.Data

Partial Class Reporte_EstadoDoc
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
#End Region




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                If Request.QueryString("rutdsd") <> "" Then
                    GeneraInforme()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GeneraInforme()
        Try
            Dim doc As New DataSet_doc.sp_Reporte_Devuelve_Estado_DocDataTable()
            Dim d As New DataSet_docTableAdapters.sp_Reporte_Devuelve_Estado_DocTableAdapter()

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Reporte_Est_Deu.rdlc"

            Dim rut_dsd As String
            Dim rut_hst As String
            Dim suc_dsd As Integer
            Dim suc_hst As Integer
            Dim resp_dsd As Integer
            Dim resp_hst As Integer
            Dim nomsucursal As String
            Dim cont As Integer


            rut_dsd = Format(CLng(Request.QueryString("rutdsd")), var.FMT_RUT)
            rut_hst = Format(CLng(Request.QueryString("ruthst")), var.FMT_RUT)
            suc_dsd = Request.QueryString("sucdsd")
            suc_hst = Request.QueryString("suchst")
            resp_dsd = Request.QueryString("respdsd")
            resp_hst = Request.QueryString("resphst")
            nomsucursal = Request.QueryString("NomSucursal")
            cont = Request.QueryString("Contador")

            doc = d.GetData(rut_dsd, rut_hst, suc_dsd, suc_hst, resp_dsd, resp_hst, nomsucursal, cont)

            Dim dt As DataTable

            dt = doc

            Dim documentos As New Microsoft.Reporting.WebForms.ReportDataSource
            documentos = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_doc_sp_Reporte_Devuelve_Estado_Doc", dt)

            ReportViewer1.LocalReport.DataSources.Add(documentos)
            ReportViewer1.DataBind()



        Catch ex As Exception

        End Try
    End Sub


End Class