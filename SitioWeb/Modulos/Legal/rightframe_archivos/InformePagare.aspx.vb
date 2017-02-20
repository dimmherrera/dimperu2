Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Data

Partial Class InformePagare
    Inherits System.Web.UI.Page
    Dim fc As New FuncionesGenerales.FComunes
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim rg As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Response.Expires = -1
                If Request.QueryString("Ruthst") <> 0 Then
                    CargaReporte()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub CargaReporte()
        Try
            Dim pagare As New DataSet_Legal.sp_Reporte_Devuelve_PagareDataTable
            Dim pgr As New DataSet_LegalTableAdapters.sp_Reporte_Devuelve_PagareTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InformePagare.rdlc")

            Dim rutdsd As String
            Dim ruthst As String
            Dim FVigDsd As String
            Dim FVigHst As String
            Dim FProDsd As String
            Dim FProHst As String
            Dim PagareDsd As String
            Dim PagareHst As String
            Dim MtoDsd As String
            Dim MtoHst As String
            Dim MandatoDsd As String
            Dim Mandatohst As String
            Dim EjeDsd As Integer
            Dim EjeHSt As Integer
            Dim PgrDsd As Integer
            Dim PgrHst As Integer
            Dim SucDsd As Integer
            Dim SucHst As Integer
            Dim DoctoDsd As Integer
            Dim DoctoHst As Integer

            rutdsd = Format(CLng(Request.QueryString("Rutdsd")), var.FMT_RUT)
            ruthst = Format(CLng(Request.QueryString("Ruthst")), var.FMT_RUT)
            FVigDsd = Request.QueryString("FVigdsd")
            FVigHst = Request.QueryString("Fvighst")
            FProDsd = Request.QueryString("Fprotdsd")
            FProHst = Request.QueryString("Fprohst")
            PagareDsd = Request.QueryString("Pagaredsd")
            PagareHst = Request.QueryString("Pagarehst")
            MtoDsd = Request.QueryString("Montodsd")
            MtoHst = Request.QueryString("Montohst")
            MandatoDsd = Request.QueryString("Mandatodsd")
            Mandatohst = Request.QueryString("mandatphst")
            EjeDsd = Request.QueryString("Ejedsd")
            EjeHSt = Request.QueryString("Ejehst")
            PgrDsd = Request.QueryString("idPgr_dsd")
            PgrHst = Request.QueryString("idPgr_hst")
            SucDsd = Request.QueryString("Sucdsd")
            SucHst = Request.QueryString("Suchst")
            DoctoDsd = Request.QueryString("Doctodsd")
            DoctoHst = Request.QueryString("Doctohst")

            pagare = pgr.GetData(rutdsd, ruthst, _
                                 FVigDsd, FVigHst, _
                                 FProDsd, FProHst, _
                                 PagareDsd, PagareHst, _
                                 MtoDsd, MtoHst, _
                                 MandatoDsd, Mandatohst, _
                                 EjeDsd, EjeHSt, _
                                 PgrDsd, PgrHst, _
                                 SucDsd, SucHst, _
                                 DoctoDsd, DoctoHst)

            Dim dt As DataTable

            dt = pagare

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_sp_Reporte_Devuelve_Pagare", dt)

            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.DataBind()

            'Dim archivo As String = "PDF.pdf"
            'Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            'Dim mimeType As String = Nothing
            'Dim encoding As String = Nothing
            'Dim fileNameExtension As String = Nothing
            'Dim streams As String() = Nothing
            'Dim war As Warning() = Nothing
            'Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, war)
            'Dim Fs As New FileStream(path, FileMode.Create)
            'Fs.Write(Bit, 0, Bit.Length)
            'Fs.Close()


            'If Bit.Length <> 0 Then


            '    Response.Clear()
            '    Response.Buffer = True
            '    Response.ContentType = "application/pdf"
            '    Response.AddHeader("Content-Length", Bit.Length.ToString)
            '    Response.AddHeader("cache-control", "private")
            '    Response.AddHeader("Expires", "0")
            '    Response.AddHeader("Pragma", "cache")
            '    Response.AddHeader("content-disposition", "attachment; filename=respaldo.pdf")
            '    Response.AddHeader("Accept-Ranges", "none")
            '    Response.BinaryWrite(Bit)
            '    Response.Flush()
            '    Response.End()

            'End If

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub Btn_Excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Excel.Click

    '    Dim pagare As New DataSet_Legal.sp_Reporte_Devuelve_PagareDataTable
    '    Dim pgr As New DataSet_LegalTableAdapters.sp_Reporte_Devuelve_PagareTableAdapter

    '    ReportViewer1.LocalReport.DataSources.Clear()
    '    ReportViewer1.Reset()

    '    ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InformePagare.rdlc")

    '    Dim rutdsd As String
    '    Dim ruthst As String
    '    Dim FVigDsd As String
    '    Dim FVigHst As String
    '    Dim FProDsd As String
    '    Dim FProHst As String
    '    Dim PagareDsd As String
    '    Dim PagareHst As String
    '    Dim MtoDsd As String
    '    Dim MtoHst As String
    '    Dim MandatoDsd As String
    '    Dim Mandatohst As String
    '    Dim EjeDsd As Integer
    '    Dim EjeHSt As Integer
    '    Dim PgrDsd As Integer
    '    Dim PgrHst As Integer
    '    Dim SucDsd As Integer
    '    Dim SucHst As Integer
    '    Dim DoctoDsd As Integer
    '    Dim DoctoHst As Integer

    '    rutdsd = Format(CLng(Request.QueryString("Rutdsd")), var.FMT_RUT)
    '    ruthst = Format(CLng(Request.QueryString("Ruthst")), var.FMT_RUT)
    '    FVigDsd = Request.QueryString("FVigdsd")
    '    FVigHst = Request.QueryString("Fvighst")
    '    FProDsd = Request.QueryString("Fprotdsd")
    '    FProHst = Request.QueryString("Fprohst")
    '    PagareDsd = Request.QueryString("Pagaredsd")
    '    PagareHst = Request.QueryString("Pagarehst")
    '    MtoDsd = Request.QueryString("Montodsd")
    '    MtoHst = Request.QueryString("Montohst")
    '    MandatoDsd = Request.QueryString("Mandatodsd")
    '    Mandatohst = Request.QueryString("mandatphst")
    '    EjeDsd = Request.QueryString("Ejedsd")
    '    EjeHSt = Request.QueryString("Ejehst")
    '    PgrDsd = Request.QueryString("idPgr_dsd")
    '    PgrHst = Request.QueryString("idPgr_hst")
    '    SucDsd = Request.QueryString("Sucdsd")
    '    SucHst = Request.QueryString("Suchst")
    '    DoctoDsd = Request.QueryString("Doctodsd")
    '    DoctoHst = Request.QueryString("Doctohst")

    '    pagare = pgr.GetData(rutdsd, ruthst, _
    '                         FVigDsd, FVigHst, _
    '                         FProDsd, FProHst, _
    '                         PagareDsd, PagareHst, _
    '                         MtoDsd, MtoHst, _
    '                         MandatoDsd, Mandatohst, _
    '                         EjeDsd, EjeHSt, _
    '                         PgrDsd, PgrHst, _
    '                         SucDsd, SucHst, _
    '                         DoctoDsd, DoctoHst)

    '    Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
    '    A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_sp_Reporte_Devuelve_Pagare", pagare)

    '    ReportViewer1.LocalReport.DataSources.Add(A)
    '    ReportViewer1.DataBind()

    '    Dim archivo As String = "excel.xls"
    '    Dim path As String = Server.MapPath("../../../EXCEL/" & archivo)

    '    Dim mimeType As String = Nothing
    '    Dim encoding As String = Nothing
    '    Dim fileNameExtension As String = Nothing
    '    Dim streams As String() = Nothing
    '    Dim war As Warning() = Nothing
    '    Dim Bit As Byte() = ReportViewer1.LocalReport.Render("XLS", Nothing, mimeType, encoding, fileNameExtension, streams, war)
    '    Dim Fs As New FileStream(path, FileMode.Create)
    '    Fs.Write(Bit, 0, Bit.Length)
    '    Fs.Close()


    '    If Bit.Length <> 0 Then


    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.ContentType = "application/octet-stream"
    '        Response.AddHeader("Content-Length", Bit.Length.ToString)
    '        Response.AddHeader("cache-control", "private")
    '        Response.AddHeader("Expires", "0")
    '        Response.AddHeader("Pragma", "cache")
    '        Response.AddHeader("content-disposition", "attachment; filename=" & archivo)
    '        Response.AddHeader("Accept-Ranges", "none")
    '        Response.BinaryWrite(Bit)
    '        Response.Flush()
    '        Response.End()

    '    End If

    'End Sub

    'Protected Sub Brn_PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Brn_PDF.Click

    '    Dim pagare As New DataSet_Legal.sp_Reporte_Devuelve_PagareDataTable
    '    Dim pgr As New DataSet_LegalTableAdapters.sp_Reporte_Devuelve_PagareTableAdapter

    '    ReportViewer1.LocalReport.DataSources.Clear()
    '    ReportViewer1.Reset()

    '    ReportViewer1.LocalReport.ReportPath = ("Modulos/Legal/Reportes/InformePagare.rdlc")

    '    Dim rutdsd As String
    '    Dim ruthst As String
    '    Dim FVigDsd As String
    '    Dim FVigHst As String
    '    Dim FProDsd As String
    '    Dim FProHst As String
    '    Dim PagareDsd As String
    '    Dim PagareHst As String
    '    Dim MtoDsd As String
    '    Dim MtoHst As String
    '    Dim MandatoDsd As String
    '    Dim Mandatohst As String
    '    Dim EjeDsd As Integer
    '    Dim EjeHSt As Integer
    '    Dim PgrDsd As Integer
    '    Dim PgrHst As Integer
    '    Dim SucDsd As Integer
    '    Dim SucHst As Integer
    '    Dim DoctoDsd As Integer
    '    Dim DoctoHst As Integer

    '    rutdsd = Format(CLng(Request.QueryString("Rutdsd")), var.FMT_RUT)
    '    ruthst = Format(CLng(Request.QueryString("Ruthst")), var.FMT_RUT)
    '    FVigDsd = Request.QueryString("FVigdsd")
    '    FVigHst = Request.QueryString("Fvighst")
    '    FProDsd = Request.QueryString("Fprotdsd")
    '    FProHst = Request.QueryString("Fprohst")
    '    PagareDsd = Request.QueryString("Pagaredsd")
    '    PagareHst = Request.QueryString("Pagarehst")
    '    MtoDsd = Request.QueryString("Montodsd")
    '    MtoHst = Request.QueryString("Montohst")
    '    MandatoDsd = Request.QueryString("Mandatodsd")
    '    Mandatohst = Request.QueryString("mandatphst")
    '    EjeDsd = Request.QueryString("Ejedsd")
    '    EjeHSt = Request.QueryString("Ejehst")
    '    PgrDsd = Request.QueryString("idPgr_dsd")
    '    PgrHst = Request.QueryString("idPgr_hst")
    '    SucDsd = Request.QueryString("Sucdsd")
    '    SucHst = Request.QueryString("Suchst")
    '    DoctoDsd = Request.QueryString("Doctodsd")
    '    DoctoHst = Request.QueryString("Doctohst")

    '    pagare = pgr.GetData(rutdsd, ruthst, _
    '                         FVigDsd, FVigHst, _
    '                         FProDsd, FProHst, _
    '                         PagareDsd, PagareHst, _
    '                         MtoDsd, MtoHst, _
    '                         MandatoDsd, Mandatohst, _
    '                         EjeDsd, EjeHSt, _
    '                         PgrDsd, PgrHst, _
    '                         SucDsd, SucHst, _
    '                         DoctoDsd, DoctoHst)

    '    Dim A As New Microsoft.Reporting.WebForms.ReportDataSource
    '    A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Legal_sp_Reporte_Devuelve_Pagare", pagare)

    '    ReportViewer1.LocalReport.DataSources.Add(A)
    '    ReportViewer1.DataBind()

    '    Dim archivo As String = "PDF.pdf"
    '    Dim path As String = Server.MapPath("../../../PDF/" & archivo)

    '    Dim mimeType As String = Nothing
    '    Dim encoding As String = Nothing
    '    Dim fileNameExtension As String = Nothing
    '    Dim streams As String() = Nothing
    '    Dim war As Warning() = Nothing
    '    Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, war)
    '    Dim Fs As New FileStream(path, FileMode.Create)
    '    Fs.Write(Bit, 0, Bit.Length)
    '    Fs.Close()


    '    If Bit.Length <> 0 Then


    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("Content-Length", Bit.Length.ToString)
    '        Response.AddHeader("cache-control", "private")
    '        Response.AddHeader("Expires", "0")
    '        Response.AddHeader("Pragma", "cache")
    '        Response.AddHeader("content-disposition", "attachment; filename=respaldo.pdf")
    '        Response.AddHeader("Accept-Ranges", "none")
    '        Response.BinaryWrite(Bit)
    '        Response.Flush()
    '        Response.End()

    '    End If

    'End Sub

End Class
