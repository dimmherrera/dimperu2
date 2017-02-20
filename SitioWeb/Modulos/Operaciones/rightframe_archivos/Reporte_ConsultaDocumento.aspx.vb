Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_ConsultaDocumento
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim FC As New FuncionesGenerales.FComunes
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1

                GeneraInformeDoctos()

            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub GeneraInformeDoctos()
        Try

            Dim ConDoc As New DataSet_doc.sp_Reporte_Consulta_DocDataTable()
            Dim d As New DataSet_docTableAdapters.sp_Reporte_Consulta_DocTableAdapter()

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe_ConsultaDoctos.rdlc"


            Dim rut_cli As String
            Dim rut_cli1 As String

            Dim nro_otg1 As Integer
            Dim nro_otg2 As String
            Dim estado As Integer
            Dim estado1 As Integer
            Dim nro_doc1 As String
            Dim nro_doc2 As String
            Dim fec_otg As String
            Dim fec_otg1 As String
            Dim rut_deu As String
            Dim rut_deu1 As String
            Dim fec_vcto1 As String
            Dim fec_vcto2 As String
            Dim cobr1 As String
            Dim cobr2 As String
            Dim obl As String
            Dim obl2 As String
            Dim TipoDocto_Dsd As Integer
            Dim TipoDocto_Hst As Integer
            Dim contrato As String


            rut_cli = Format(CLng(Request.QueryString("rutdsd")), var.FMT_RUT)
            rut_cli1 = Format(CLng(Request.QueryString("ruthst")), var.FMT_RUT)
            rut_deu = Format(CLng(Request.QueryString("Rut_deu_d")), var.FMT_RUT)
            rut_deu1 = Format(CLng(Request.QueryString("Rut_deu_a")), var.FMT_RUT)

            TipoDocto_Dsd = Request.QueryString("TipoDocto_D")
            TipoDocto_Hst = Request.QueryString("TipoDocto_A")
            nro_doc1 = Request.QueryString("nro_doc1")
            nro_doc2 = Request.QueryString("nro_doc2")
            fec_vcto1 = Request.QueryString("fec_vcto1")
            fec_vcto2 = Request.QueryString("fec_vcto2")
            estado = Request.QueryString("estado")
            estado1 = Request.QueryString("estado1")
            nro_otg1 = Request.QueryString("nro_otg1")
            nro_otg2 = Request.QueryString("nro_otg2")
            cobr1 = Request.QueryString("cobr1")
            cobr2 = Request.QueryString("cobr2")
            fec_otg = Request.QueryString("fec_otg")
            fec_otg1 = Request.QueryString("fec_otg1")
            obl = Request.QueryString("obl")
            obl2 = Request.QueryString("obl2")
            contrato = Request.QueryString("contr")


            ConDoc = d.GetData(rut_cli, rut_cli1, _
                               rut_deu, rut_deu1, _
                               TipoDocto_Dsd, TipoDocto_Hst, _
                               fec_vcto1, fec_vcto2, _
                               nro_otg1, nro_otg2, _
                               obl, obl2, _
                               fec_otg, fec_otg1, _
                               nro_doc1, nro_doc2, _
                               estado, estado1, contrato)

            'ConDoc = d.GetData(0, 9999999999999, 0, 9999999999999, 0, 999, "19000101", "20120101", 0, 999, 0, 999, "19000101", "20120101")


            Dim dt As DataTable

            dt = ConDoc

            Dim ConsultaDocumentos As New Microsoft.Reporting.WebForms.ReportDataSource
            ConsultaDocumentos = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_doc_sp_Reporte_Consulta_Doc", dt)

            ReportViewer1.LocalReport.DataSources.Add(ConsultaDocumentos)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

End Class
