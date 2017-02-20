Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.Reporting.WebForms
Imports FuncionesGenerales
Imports System.Web.HttpServerUtility

Public Class Reporting
    Implements IDisposable

    Private clsdrop As New ClsLog
    Public m_currentPageIndex As Integer
    Public m_streams As IList(Of Stream)

    Public Sub Export(ByVal report As LocalReport)
        Try
            Dim deviceInfo As String = _
            "<DeviceInfo>" + _
            " <OutputFormat>EMF</OutputFormat>" + _
            " <PageWidth>8.5in</PageWidth>" + _
            " <PageHeight>11in</PageHeight>" + _
            " <MarginTop>0.15in</MarginTop>" + _
            " <MarginLeft>0.15in</MarginLeft>" + _
            " <MarginRight>0.15in</MarginRight>" + _
            " <MarginBottom>0.15in</MarginBottom>" + _
            "</DeviceInfo>"
            Dim warnings() As Warning = Nothing
            m_streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            Dim stream As Stream
            For Each stream In m_streams
                stream.Position = 0
            Next
        Catch ex As Exception
            clsdrop.eLog("Error:" & ex.Message)
        End Try
    End Sub

    Public Function CreateStream(ByVal name As String, _
                                ByVal fileNameExtension As String, _
                                ByVal encoding As Encoding, ByVal mimeType As String, _
                                ByVal willSeek As Boolean) As Stream

        Try

            Dim stream As Stream = _
            New FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "\" + name + "." + fileNameExtension, FileMode.OpenOrCreate)
            m_streams.Add(stream)

            Return stream

        Catch ex As Exception
            clsdrop.eLog("Error:" & ex.Message)
            Return Nothing
        End Try

    End Function

    Public Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Try

            Dim pageImage As New Metafile(m_streams(m_currentPageIndex))
            ev.Graphics.DrawImage(pageImage, ev.PageBounds)
            m_currentPageIndex += 1
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count)

        Catch ex As Exception
            clsdrop.eLog("Error:" & ex.Message)
        End Try

    End Sub

    Public Sub Print()
        Const printerName As String = "Microsoft Office Document Image Writer"
        If m_streams Is Nothing OrElse m_streams.Count = 0 Then
            Return
        End If
        Dim printDoc As New PrintDocument()

        ' printDoc.PrinterSettings..PrinterName = printerName
        If Not printDoc.PrinterSettings.IsValid Then
            Dim msg As String = String.Format( _
            "Can't find printer ""{0}"".", printerName)
            Console.WriteLine(msg)
            Return
        End If
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        printDoc.PrinterSettings.Duplex = Duplex.Horizontal
        printDoc.Print()
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        If Not (m_streams Is Nothing) Then
            Dim stream As Stream
            For Each stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub

    Public Sub InsertaLogo(ByVal rp As ReportViewer)
        Dim cg As New CapaDatos.ConsultasGenerales

        rp.LocalReport.EnableExternalImages = True
        Dim siteUrl As String = "file:\\\" & System.Web.Configuration.WebConfigurationManager.AppSettings("Logo").ToString()

        Dim logo As ReportParameter = New ReportParameter("Ruta_imagen", siteUrl)
        rp.LocalReport.SetParameters(New ReportParameter() {logo})

    End Sub

End Class
