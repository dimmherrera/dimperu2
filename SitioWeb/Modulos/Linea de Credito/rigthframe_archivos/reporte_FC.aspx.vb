Imports System.IO
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports Microsoft.Reporting.WebForms
Imports [dim].xml

Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_reporte_FC
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim agt As New Perfiles.Cls_Principal
    Dim RG As New FuncionesGenerales.FComunes
    Dim Caption As String = "Linea de Crédito"
    Dim Var As New FuncionesGenerales.Variables
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("id").Trim() <> "" Then
            RenderReport()
        Else
            Msj.Mensaje(Page, "Hoja_de_Firmas", "Debe ingresar NIT cliente", TipoDeMensaje._Error)
        End If


    End Sub

    Private Sub RenderReport()

        Dim ClsCli As New ClaseClientes
        Dim Cli As cli_cls
        Cli = ClsCli.ClienteDevuelvePorRut(Request.QueryString("id").Trim())

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()

        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.LocalReport.ReportPath = "Modulos\Linea de Credito\Reporte\FC_03_2012.rdlc"

        Dim aux As String = ""
        If Not IsNothing(Cli.cmn_cls) Then
            aux = Cli.cmn_cls.ciu_cls.ciu_des.ToString
        Else
            aux = ""
        End If

        Dim ciu As ReportParameter = New ReportParameter("Ciudad", aux)
        Dim dir As ReportParameter = New ReportParameter("Direccion", Cli.cli_dml.ToString)
        Dim id As ReportParameter = New ReportParameter("ID", (RG.FormatoMiles(CInt(Cli.cli_idc.Trim)) & "-" & Cli.cli_dig_ito).ToString)
        Dim raz As ReportParameter = New ReportParameter("Raz_Soc", (Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn & " " & Cli.cli_rso).ToString)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {ciu, dir, id, raz})
        'ReportViewer1.LocalReport.Refresh()

        Dim archivo As String = "FC_03_" & DateTime.Now.ToString("yyyyMMddhhmmss") & ".pdf"
        Dim path As String = Server.MapPath("..\Pagare\" & archivo)

        Dim reportType As String = "PDF"
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim fileNameExtension As String = Nothing
        Dim streams As String() = Nothing
        Dim war As Warning() = Nothing
        Dim renderedBytes As Byte()

        'renderedBytes = ReportViewer1.LocalReport.Render(reportType, _
        '                                                 Nothing, _
        '                                                 mimeType, _
        '                                                 encoding, _
        '                                                 fileNameExtension, _
        '                                                 streams, _
        '                                                 war)

        'Response.Buffer = False
        'Response.Expires = -1
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("Content-Type", "application/pdf")
        'Response.AddHeader("Content-Length", renderedBytes.Length.ToString)
        'Response.AddHeader("Content-Disposition", "attachment;filename=" & archivo & "")
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.BinaryWrite(renderedBytes)
        'Response.End()

    End Sub
End Class
