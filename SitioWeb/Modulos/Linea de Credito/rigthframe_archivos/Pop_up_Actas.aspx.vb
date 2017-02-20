Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports System.Transactions
Imports System.IO
Imports CapaDatos



Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_Pop_up_Actas
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

            Txt_nro_ldc.Text = Request.QueryString("ID").Trim
            CargaGrilla()
            Botonera(True, False, False)

        End If
        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")
    End Sub
#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        FileUpload_Actas.Enabled = True
        Botonera(False, True, True)
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try
            FileUpload_Actas.SaveAs(MapPath("../Actas/" + "LDC" + Txt_nro_ldc.Text.Trim + "_" + FileUpload_Actas.FileName))

            Dim archivo As String = FileUpload_Actas.FileName
            Dim desc As String = RG.EliminarAcentos(archivo)
            Dim path As String = Server.MapPath("../Actas/" + "LDC" + Txt_nro_ldc.Text.Trim + "_" + archivo)
            Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read)
            Dim ImageData As Byte() = New Byte(fs.Length - 1) {}

            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length))
            fs.Close()
            File.Delete(path)

            CA.GuardaActa(CInt(Txt_nro_ldc.Text.Trim), desc, ImageData)

            CargaGrilla()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cancelar.Click

        'CargaGrilla()
        Botonera(True, False, False)
        FileUpload_Actas.Enabled = False

    End Sub

    Protected Sub IB_Delete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            CA.EliminaActa(CInt(btn.ToolTip))
            CargaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Page, "Actas por linea de financiamiento", "Error " & ex.ToString, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            Dim nombrearchivo As String = ""

            For i = 0 To GV_Actas.Rows.Count - 1

                If (btn.ToolTip = GV_Actas.Rows(i).Cells(1).Text) Then
                    If (i Mod 2) = 0 Then
                        GV_Actas.Rows(i).CssClass = "selectable"
                    Else
                        GV_Actas.Rows(i).CssClass = "selectableAlt"
                    End If
                Else
                    If (i Mod 2) = 0 Then
                        GV_Actas.Rows(i).CssClass = "formatUltcell"
                    Else
                        GV_Actas.Rows(i).CssClass = "formatUltcellAlt"
                    End If
                End If

                If (btn.ToolTip = GV_Actas.Rows(i).Cells(1).Text) Then
                    nombrearchivo = GV_Actas.Rows(i).Cells(3).Text
                End If

            Next

            Dim fc As New FuncionesGenerales.FComunes

            Dim archivo As Byte() = CA.DevuelveActa(CInt(btn.ToolTip.Trim))
            'Dim path As String = Server.MapPath("../Actas/") & nombrearchivo
            nombrearchivo = nombrearchivo.Replace(" ", "_")
            Dim extension As String = fc.ExtraerExtension(nombrearchivo, ".")
            Dim _ctype As String = ""
            'ConvertBytesToImageFile(archivo, path)

            If archivo.Length <> 0 Then

                Dim path2 As String = "Acta_" & btn.ToolTip
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
            Msj.Mensaje(Page, "Actas por linea de financiamiento", "Error " & ex.ToString, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

#Region "VOID Y SUB"

    Public Sub CargaGrilla()

        Try
            Dim CG As New ConsultasGenerales
            CG.ActasDevuelvePorLDC(GV_Actas, CInt(Txt_nro_ldc.Text.Trim))
            If GV_Actas.Rows.Count < 1 Then
                Msj.Mensaje(Me.Page, Caption, "No existen actas asociadas a la linea de financiamiento", TipoDeMensaje._Exclamacion)
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
End Class