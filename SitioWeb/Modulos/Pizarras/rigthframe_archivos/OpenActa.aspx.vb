Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_OpenActa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Dim CA As New ClaseArchivos
            Dim nombrearchivo As String = Request.QueryString("File").Trim()
            Dim fc As New FuncionesGenerales.FComunes
            Dim ID As Integer = CInt(Request.QueryString("id").Trim())
            'Dim archivo As Byte() = CA.DevuelveActa(ID)
            'Dim Extension As String = fc.ExtraerExtension(nombrearchivo, ".")
            'Dim _ctype As String = ""

            'If archivo.Length <> 0 Then

            '    Dim path2 As String = "Acta_" & ID.ToString
            '    Select Case Extension
            '        Case "pdf" : _ctype = "application/pdf"
            '        Case "exe" : _ctype = "application/octet-stream"
            '        Case "zip" : _ctype = "application/zip"
            '        Case "doc" : _ctype = "application/msword"
            '        Case "xls" : _ctype = "application/vnd.ms-excel"
            '        Case "ppt" : _ctype = "application/vnd.ms-powerpoint"
            '        Case "gif" : _ctype = "image/gif"
            '        Case "png" : _ctype = "image/png"
            '        Case "jpeg", "jpg" : _ctype = "image/jpg"
            '        Case Else : _ctype = "application/force-download"
            '    End Select

            '    Response.Buffer = False
            '    Response.Expires = -1
            '    Response.AddHeader("Content-Type", _ctype)
            '    Response.AddHeader("Content-Length", archivo.Length.ToString)
            '    Response.AddHeader("Content-Disposition", "attachment;filename=" & nombrearchivo & "")
            '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
            '    Response.BinaryWrite(archivo)
            '    Response.End()
            'End If


            Dim archivo As Byte() = CA.DevuelveActa(ID)
            nombrearchivo = nombrearchivo.Replace(" ", "_")
            Dim extension As String = fc.ExtraerExtension(nombrearchivo, ".")
            Dim _ctype As String = ""

            If archivo.Length <> 0 Then

                Dim path2 As String = "Acta_" & ID.ToString
                Select Case extension
                    Case "pdf" : _ctype = "application/pdf"
                    Case "exe" : _ctype = "application/octet-stream"
                    Case "zip" : _ctype = "application/zip"
                    Case "doc" : _ctype = "application/msword"
                    Case "xls" : _ctype = "application/vnd.ms-excel"
                    Case "ppt" : _ctype = "application/vnd.ms-powerpoint"
                    Case "gif" : _ctype = "image/gif"
                    Case "png" : _ctype = "image/png"
                    Case "jpeg", "jpg" : _ctype = "image/jpg"
                    Case Else : _ctype = "application/force-download"
                End Select

                Response.Buffer = False
                Response.Expires = -1
                Response.AddHeader("Content-Type", _ctype)
                Response.AddHeader("Content-Length", archivo.Length.ToString)
                Response.AddHeader("Content-Disposition", "attachment;filename=" & nombrearchivo & "")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.BinaryWrite(archivo)
                Response.End()

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

End Class
