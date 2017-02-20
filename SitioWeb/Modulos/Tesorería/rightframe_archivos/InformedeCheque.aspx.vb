Imports Microsoft.Reporting.WebForms
Imports System.Data

Partial Class Modulos_ArqueoDeCheques_rightframe_archivos_InformedeCheque
    Inherits System.Web.UI.Page

    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Caption As String = "Informe de Arqueo"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Response.Expires = -1

            If Request.QueryString("RutDesde") <> "" Then
                Genera_reporte()
            End If
        End If

    End Sub

    Sub Genera_reporte()

        Try
            Dim FC As New FuncionesGenerales.FComunes
            Dim rut_desde As String
            Dim rut_hasta As String
            Dim fecha_desde As String
            Dim fecha_hasta As String
            Dim custodia_desde As Integer
            Dim custodia_hasta As Integer
            Dim Est_Dsd, Est_Hst As Integer
            Dim TipC_dsd, TipC_Hst As String

            rut_desde = Format(CLng(Request.QueryString("RutDesde")), var.FMT_RUT)
            rut_hasta = Format(CLng(Request.QueryString("RutHasta")), var.FMT_RUT)
            fecha_desde = FC.FUNFechaJul(Request.QueryString("FechaInicio"))
            fecha_hasta = FC.FUNFechaJul(Request.QueryString("FechaTermino"))
            fecha_desde = fecha_desde.Replace("-", "")
            fecha_hasta = fecha_hasta.Replace("-", "")
            custodia_desde = Request.QueryString("CustInicio")
            custodia_hasta = Request.QueryString("CustTermino")
            Est_Dsd = Request.QueryString("Estdsd")
            Est_Hst = Request.QueryString("Esthst")
            TipC_dsd = Request.QueryString("TipChr_dsd")
            TipC_Hst = Request.QueryString("tipchr_hst")

            Dim chr As New DataSet_Cheques.Sp_Reporte_ChequesDataTable
            Dim cheq As New DataSet_ChequesTableAdapters.Sp_Reporte_ChequesTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Tesorería\Reportes\Informe_Cheque.rdlc"

            chr = cheq.GetData(rut_desde, rut_hasta, _
                               fecha_desde, fecha_hasta, _
                               custodia_desde, custodia_hasta, _
                               Est_Dsd, Est_Hst, _
                               TipC_dsd, TipC_Hst)

            Dim dt As DataTable

            dt = chr


            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cheques_Sp_Reporte_Cheques", dt)

            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub


End Class
