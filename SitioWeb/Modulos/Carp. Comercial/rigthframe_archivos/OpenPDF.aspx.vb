Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.FComunes
Imports System.IO

Partial Class Comercial_rigthframe_archivos_OpenPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim CA As New ClaseArchivos
        Dim TipoRep As Integer
        Dim extension As String = ".pdf"

        If Request.QueryString("Nro").Trim() <> "" Then

            TipoRep = Request.QueryString("Inf").Trim()

            Select Case TipoRep
                Case 1

                    'Negociación
                    Dim abytFileData As Byte() = CA.DespliegaArchivoNegPDF(Request.QueryString("Nro").Trim)

                    If abytFileData.Length <> 0 Then

                        'Response.Buffer = False
                        'Response.Expires = -1
                        'Response.ContentType = "application/pdf"
                        'Response.AddHeader("Content-Type", "application/pdf")
                        'Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                        'Response.AddHeader("Content-Disposition", "attachment;filename=" & archivo & "")
                        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        'Response.BinaryWrite(abytFileData)
                        'Response.End()

                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                        Response.AddHeader("cache-control", "private")
                        Response.AddHeader("Expires", "0")
                        Response.AddHeader("Pragma", "cache")
                        Response.AddHeader("content-disposition", "attachment; filename=Negociacion_" & Request.QueryString("Nro") & ".pdf")
                        Response.AddHeader("Accept-Ranges", "none")
                        Response.BinaryWrite(abytFileData)
                        Response.Flush()
                        Response.End()

                    End If

                Case 2

                    'Evaluacion
                    Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(Request.QueryString("Nro").Trim())

                    If abytFileData.Length <> 0 Then

                        'Response.Buffer = False
                        'Response.Expires = -1
                        'Response.ContentType = "application/pdf"
                        'Response.AddHeader("Content-Type", "application/pdf")
                        'Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                        'Response.AddHeader("Content-Disposition", "attachment;filename=Evaluacion_" & Request.QueryString("Nro") & ".pdf")
                        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        'Response.BinaryWrite(abytFileData)
                        'Response.End()

                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                        Response.AddHeader("cache-control", "private")
                        Response.AddHeader("Expires", "0")
                        Response.AddHeader("Pragma", "cache")
                        Response.AddHeader("content-disposition", "attachment; filename=Evaluacion_" & Request.QueryString("Nro") & ".pdf")
                        Response.AddHeader("Accept-Ranges", "none")
                        Response.BinaryWrite(abytFileData)
                        Response.Flush()
                        Response.End()

                    End If

            End Select
        End If

    End Sub

End Class
