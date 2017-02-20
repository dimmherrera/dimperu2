Imports CapaDatos
Imports System.Data

Partial Class InformeAval
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                If Request.QueryString("Ruthst") <> 0 Then
                    CargaInforme()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CargaInforme()
        Try
            Dim aval As New DataSet_Legal.Sp_Reporte_AvalesDataTable
            Dim avl As New DataSet_LegalTableAdapters.Sp_Reporte_AvalesTableAdapter
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InformeAval.rdlc")

            Dim rutdsd As String
            Dim ruthst As String
            Dim EjeDsd As Integer
            Dim Ejehst As Integer
            Dim SucDsd As Integer
            Dim Suchst As Integer
            Dim Avldsd As Integer
            Dim Avlhst As Integer


            rutdsd = Format(CLng(Request.QueryString("Rutdsd")), var.FMT_RUT)
            ruthst = Format(CLng(Request.QueryString("Ruthst")), var.FMT_RUT)
            EjeDsd = Request.QueryString("Ejedsd")
            Ejehst = Request.QueryString("Ejehst")
            SucDsd = Request.QueryString("Sucdsd")
            Suchst = Request.QueryString("Suchst")
            Avldsd = Request.QueryString("AvalDsd")
            Avlhst = Request.QueryString("AvalHst")

            aval = avl.GetData(rutdsd, ruthst, SucDsd, Suchst, Avldsd, Avlhst, EjeDsd, Ejehst)

            Dim dt As DataTable

            dt = aval

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_Sp_Reporte_Avales", dt)

            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.DataBind()


        Catch ex As Exception

        End Try
    End Sub

End Class
