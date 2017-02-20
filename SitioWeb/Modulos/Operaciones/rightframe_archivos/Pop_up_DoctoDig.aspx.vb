Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
'Imports System.Transactions
Imports System.IO
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_Pop_up_DoctoDig
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim agt As New Perfiles.Cls_Principal
    Dim RG As New FuncionesGenerales.FComunes
    Dim Caption As String = "Actas por LDC"
    Dim Var As New FuncionesGenerales.Variables
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim CA As New ClaseArchivos

#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            Txt_id_ope.Text = Request.QueryString("ID").Trim
            CargaGrilla()
            Botonera(True, False, False)

        End If
        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")
    End Sub
#End Region

#Region "VOID Y SUB"

    Public Sub CargaGrilla()

        Try
            Dim CG As New ConsultasGenerales
            CG.DoctoDevuelvePorOpe(GV_Docto, CInt(Txt_id_ope.Text.Trim))
            If GV_Docto.Rows.Count < 1 Then
                Msj.Mensaje(Me.Page, Caption, "No existen documentos asociados a la operacion", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub Botonera(ByVal Nuevo As Boolean, ByVal Guardar As Boolean, ByVal Cancelar As Boolean)
        IB_Nuevo.Enabled = Nuevo
        IB_Guardar.Enabled = Guardar
        IB_Cancelar.Enabled = Cancelar
    End Sub

#End Region

#Region "Botonera"
    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Botonera(False, True, True)
        Fileupload_Docto.Enabled = True
        Fileupload_Docto.CssClass = "clsMandatorio"
        Label3.Visible = True
        Txt_descr.Visible = True
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try

            If ValidaCampos() Then

                Fileupload_Docto.SaveAs(MapPath("../Documentos/" + "OPE" + Txt_id_ope.Text.Trim + "_" + Fileupload_Docto.FileName))

                Dim archivo As String = Fileupload_Docto.FileName
                Dim desc As String = Txt_descr.Text.Trim & "(" & RG.EliminarAcentos(archivo) & ")"
                Dim path As String = Server.MapPath("../Documentos/" + "OPE" + Txt_id_ope.Text.Trim + "_" + archivo)
                Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read)
                Dim ImageData As Byte() = New Byte(fs.Length - 1) {}

                fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length))
                fs.Close()
                File.Delete(path)

                CA.GuardaDocto(CInt(Txt_id_ope.Text.Trim), desc, ImageData)

                Limpiar()

                CargaGrilla()
            Else
                Msj.Mensaje(Page, "Documentos Adjuntos", "Debe Completar todos los campos requeridos", TipoDeMensaje._Exclamacion)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cancelar.Click
        Limpiar()
    End Sub

    Public Sub Limpiar()
        Botonera(True, False, False)
        Fileupload_Docto.Enabled = False
        Fileupload_Docto.CssClass = "clsDisabled"
        Label3.Visible = False
        Txt_descr.Visible = False
        Txt_descr.Text = ""
    End Sub

    Public Function ValidaCampos() As Boolean

        If Fileupload_Docto.FileName = "" Then
            Msj.Mensaje(Page, "Documentos Adjuntos", "Debe seleccionar archivo a cargar", TipoDeMensaje._Exclamacion)
            Return False
        End If

        If Txt_descr.Text = "" Then
            Msj.Mensaje(Page, "Documentos Adjuntos", "Debe Descripcion", TipoDeMensaje._Exclamacion)
            Return False
        End If

        Return True
    End Function


    Protected Sub IB_Delete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            CA.EliminaDocto(CInt(btn.ToolTip))
            CargaGrilla()
        Catch ex As Exception
            Msj.Mensaje(Page, "Docuemtos Digitalizados por operacion", "Error " & ex.ToString, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            Dim nombrearchivo As String = ""
            Dim aux As String = ""

            For i = 0 To GV_Docto.Rows.Count - 1
                If (btn.ToolTip = GV_Docto.Rows(i).Cells(1).Text) Then
                    If (i Mod 2) = 0 Then
                        GV_Docto.Rows(i).CssClass = "selectable"
                    Else
                        GV_Docto.Rows(i).CssClass = "selectableAlt"
                    End If
                Else
                    If (i Mod 2) = 0 Then
                        GV_Docto.Rows(i).CssClass = "formatUltcell"
                    Else
                        GV_Docto.Rows(i).CssClass = "formatUltcellAlt"
                    End If
                End If
                If (btn.ToolTip = GV_Docto.Rows(i).Cells(1).Text) Then
                    aux = GV_Docto.Rows(i).Cells(3).Text
                End If
            Next

            Dim fc As New FuncionesGenerales.FComunes

            Dim archivo As Byte() = CA.DevuelveDocto(CInt(btn.ToolTip.Trim))
            nombrearchivo = fc.ExtraerExtension(aux, "(").Replace(")", "")
            nombrearchivo = nombrearchivo.Replace(" ", "_")
            Dim extension As String = fc.ExtraerExtension(nombrearchivo, ".")
            Dim _ctype As String = ""

            If archivo.Length <> 0 Then

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
            Msj.Mensaje(Page, "Documentos Digitalizados por Operacion", "Error " & ex.ToString, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

End Class
