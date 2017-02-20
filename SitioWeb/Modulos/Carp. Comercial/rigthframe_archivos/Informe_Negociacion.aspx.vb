Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports ClsSession.ClsSession
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Data

Partial Class Modulos_Reportes_rigthframe_archivos_Informe_Negociacion
    Inherits System.Web.UI.Page


#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim CA As New ClaseArchivos

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1

            If Not Page.IsPostBack Then

                If Request.QueryString("Informe") <> "" Then
                    GeneraInformeInstructivo()
                Else

                    Dim nro As Integer

                    nro = Request.QueryString("IdOpn")

                    Dim abytFileData As Byte() = CA.DespliegaArchivoNegPDF(nro)

                    If abytFileData.Length <> 0 Then

                        Dim archivo As String = "Negociacion_" & nro & ".pdf"

                        Response.Buffer = False
                        Response.Expires = -1
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("Content-Type", "application/pdf")
                        Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                        Response.AddHeader("Content-Disposition", "attachment;filename=" & archivo & "")
                        Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        Response.BinaryWrite(abytFileData)
                        Response.End()
                    Else
                        GeneraInforme()
                    End If

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub GeneraInforme()

        Try

            Dim op As New DataSet_Negociacion.sp_Reporte_Devuelve_opera_anteriores_negociacionDataTable
            Dim opn As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_opera_anteriores_negociacionTableAdapter

            Dim inf As New DataSet_Negociacion.sp_Informe_deu_cli_hoj_negDataTable
            Dim i As New DataSet_NegociacionTableAdapters.sp_Informe_deu_cli_hoj_negTableAdapter

            Dim rep As New DataSet_Negociacion.sp_Reporte_tas_hoj_negDataTable
            Dim r As New DataSet_NegociacionTableAdapters.sp_Reporte_tas_hoj_negTableAdapter

            Dim opngr As New DataSet_Negociacion.sp_Reporte_Devuelve_opn_cli_negDataTable
            Dim opgr As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_opn_cli_negTableAdapter

            Dim spread As New DataSet_Negociacion.sp_Reporte_Devuelve_spread_bancaDataTable
            Dim sp As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_spread_bancaTableAdapter

            Dim TAnt As New DataSet_Negociacion.sp_Reporte_Devuelve_Tasa_Anterior_cliDataTable
            Dim ta As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Tasa_Anterior_cliTableAdapter

            Dim gto As New DataSet_Negociacion.sp_Reporte_Devuelve_Gastos_DefinidosDataTable
            Dim g As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Gastos_DefinidosTableAdapter

            Dim gfn As New DataSet_Negociacion.sp_Reporte_Devuelve_Gastos_FijosDataTable
            Dim gf As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Gastos_FijosTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\Reporte_Negociacion.rdlc"

            Dim rut As String
            Dim nro As Integer

            rut = Format(CLng(Request.QueryString("Rut")), var.FMT_RUT)
            nro = Request.QueryString("IdOpn")

            op = opn.GetData(nro)
            inf = i.GetData(rut)
            rep = r.GetData(rut)
            inf = i.GetData(rut)
            rep = r.GetData(rut)
            'gto = g.GetData("D", nro)
            gto = g.GetData(nro)
            opngr = opgr.GetData(rut)
            spread = sp.GetData(nro)
            TAnt = ta.GetData(rut)
            gfn = gf.GetData(nro)


            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = op

            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_opera_anteriores_negociacion", dt)

            Dim B As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt2 As DataTable

            dt2 = inf

            B = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Informe_deu_cli_hoj_neg", dt2)


            'inf.Item(0).Ctas_Cobrar = Format(CDbl(inf.Item(0).Ctas_Cobrar), "###,###,###,###")


            Dim C As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt3 As DataTable

            dt3 = rep

            C = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_tas_hoj_neg", dt3)

            Dim D As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt4 As DataTable

            dt4 = gto

            D = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Gastos_Definidos", dt4)

            Dim E As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt5 As DataTable

            dt5 = opngr

            E = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_opn_cli_neg", dt5)

            Dim F As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt6 As DataTable

            dt6 = spread

            F = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_spread_banca", dt6)

            Dim Ge As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt7 As DataTable

            dt7 = TAnt

            Ge = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Tasa_Anterior_cli", dt7)

            Dim H As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt8 As DataTable

            dt8 = gfn

            H = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Gastos_Fijos", dt8)


            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.LocalReport.DataSources.Add(B)
            ReportViewer1.LocalReport.DataSources.Add(C)
            ReportViewer1.LocalReport.DataSources.Add(D)
            ReportViewer1.LocalReport.DataSources.Add(E)
            ReportViewer1.LocalReport.DataSources.Add(F)
            ReportViewer1.LocalReport.DataSources.Add(Ge)
            ReportViewer1.LocalReport.DataSources.Add(H)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub GeneraInformeInstructivo()

        Try



            Dim op As New DataSet_Negociacion.sp_Reporte_Devuelve_opera_anteriores_negociacionDataTable
            Dim opn As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_opera_anteriores_negociacionTableAdapter

            Dim de As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_DeudoresDataTable
            Dim deu As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_DeudoresTableAdapter

            Dim cl As New DataSet_Cliente.SP_WS_CLIENTE_DEVUELVE_POR_RUTDataTable
            Dim cli As New DataSet_ClienteTableAdapters.SP_WS_CLIENTE_DEVUELVE_POR_RUTTableAdapter


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\Instructivo.rdlc"

            Dim rut As String
            Dim nro As Integer
            Dim cls As New ClaseComercial
            Dim idEva As Decimal


            Dim neg As opn_cls

            rut = Format(CLng(RutCli), var.FMT_RUT)
            nro = Request.QueryString("IdOpn")

            neg = cls.NegociacionDevuelve(rut, nro)
            idEva = neg.id_eva


            op = opn.GetData(nro)
            de = deu.GetData(idEva)
            cl = cli.GetData(rut)

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = op

            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_opera_anteriores_negociacion", dt)

            Dim B As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt2 As DataTable

            dt2 = de

            B = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Deudores", dt2)

            Dim C As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt3 As DataTable

            dt3 = cl

            C = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cliente_SP_WS_CLIENTE_DEVUELVE_POR_RUT", dt3)

            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.LocalReport.DataSources.Add(B)
            ReportViewer1.LocalReport.DataSources.Add(C)

            ReportViewer1.DataBind()

            'EXPORTACION DEL REPORTE
            'Dim archivo As String = "Negociacion_" & rut & "_ID_" & nro & ".pdf"
            'Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            'Dim deviceInfo = String.Empty
            'Dim type As String = "PDF"
            'Dim encoding As String = String.Empty
            'Dim mimeType As String = String.Empty
            'Dim extension = String.Empty
            'Dim warnings() As Warning = Nothing
            'Dim streamIDs As String() = Nothing

            'Dim pdfContent As Byte() = ReportViewer1.LocalReport.Render("PDF", _
            '                                                            String.Empty, _
            '                                                            mimeType, _
            '                                                            encoding, _
            '                                                            extension, _
            '                                                            streamIDs, _
            '                                                            warnings)

            ''Dim pdfPath As String = path
            ''Dim pdfFile As New FileStream(pdfPath, System.IO.FileMode.Create)

            ''pdfFile.Write(pdfContent, 0, pdfContent.Length)
            ''pdfFile.Close()

            'CA.GuardaNegPDF(nro, pdfContent)


        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

    '    Dim rpt As New Reporting

    '    rpt.m_currentPageIndex = 0
    '    rpt.Export(ReportViewer1.LocalReport)
    '    rpt.Print()

    'End Sub

End Class
