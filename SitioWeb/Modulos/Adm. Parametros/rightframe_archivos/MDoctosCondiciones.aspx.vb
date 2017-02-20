Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class MDoctosCondiciones
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim Caption As String = "Requisitos"
    Dim Msj As New ClsMensaje

    Protected Sub btn_nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nuevo.Click

        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20011012, Usr, "PRESIONO NUEVO ") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Txt_Codigo.ReadOnly = False
            Txt_Descripcion.ReadOnly = False
            DP_Estados.Enabled = True

            Txt_Descripcion.CssClass = "clsMandatorio"
            DP_Estados.CssClass = "clsMandatorio"

            Txt_Codigo.Text = ""
            Txt_Descripcion.Text = ""
            DP_Estados.ClearSelection()
            Me.btn_Guardar.Enabled = True
            Hf_pos.Value = ""
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Guardar.Click

        If Txt_Descripcion.Text = "" Then
            Msj.Mensaje(Me, "Atención", "Debe ingresar la descripcion", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If DP_Estados.SelectedIndex = 0 Then
            Msj.Mensaje(Me, "Atención", "Debe selecciona el estado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20021012, Usr, "PRESIONO GUARDAR REQUISITO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Dim tipo As String = If(RB_Opcion.SelectedValue = "D", "el Documento", "la Condición")

        Msj.Mensaje(Me, "Atención", "¿Desea Guardar " & tipo & "?", ClsMensaje.TipoDeMensaje._Confirmacion, Lb_gua.UniqueID)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        
    End Sub

    Private Sub Limpia()

        Txt_Descripcion.ReadOnly = True
        DP_Estados.Enabled = False

        Txt_Descripcion.CssClass = "clsDisabled"

        Txt_Codigo.Text = ""
        Txt_Descripcion.Text = ""
        DP_Estados.ClearSelection()
        Hf_pos.Value = ""

    End Sub
   
    Protected Sub Lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_gua.Click

        Try

            Select Case RB_Opcion.SelectedValue

                Case "D"

                    Dim doc As New doc_com_cls

                    If Txt_Codigo.Text = "" Then

                        doc.id_doc_com = Nothing
                        doc.des_doc_com = Txt_Descripcion.Text.Trim.ToUpper
                        doc.est_doc_com = DP_Estados.SelectedValue.ToUpper

                        If AG.DocComInserta(doc) Then
                            Msj.Mensaje(Me.Page, Caption, "Documento Comercial Guardado", TipoDeMensaje._Informacion)
                            Limpia()
                            CargaGrilla()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                        End If

                    Else

                        doc.id_doc_com = Txt_Codigo.Text
                        doc.des_doc_com = Txt_Descripcion.Text.Trim.ToUpper
                        doc.est_doc_com = DP_Estados.SelectedValue.ToUpper

                        If AG.DocComActualiza(doc) Then
                            Msj.Mensaje(Me.Page, Caption, "Documento Comercial Modificado", TipoDeMensaje._Informacion)
                            Limpia()
                            CargaGrilla()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                        End If

                    End If

                Case "C"

                    Dim con As New con_com_cls

                    If Txt_Codigo.Text = "" Then

                        con.id_con_com = Nothing
                        con.des_con_com = Txt_Descripcion.Text.Trim.ToUpper
                        con.est_con_com = DP_Estados.SelectedValue.ToUpper

                        If AG.ConComInserta(con) Then
                            Msj.Mensaje(Me.Page, Caption, "Otras Condiciones Comercial Guardado", TipoDeMensaje._Informacion)
                            Limpia()
                            CargaGrilla()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                        End If

                    Else

                        con.id_con_com = Txt_Codigo.Text
                        con.des_con_com = Txt_Descripcion.Text.Trim.ToUpper
                        con.est_con_com = DP_Estados.SelectedValue.ToUpper

                        If AG.ConComActualiza(con) Then
                            Msj.Mensaje(Me.Page, Caption, "Otras Condiciones Modificado", TipoDeMensaje._Informacion)
                            Limpia()
                            CargaGrilla()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                        End If

                    End If


            End Select

            
            btn_Guardar.Enabled = False
            DP_Estados.CssClass = "clsDisabled"

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gr_DocCon.Rows.Count - 1

            If (Gr_DocCon.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_DocCon.Rows(i).CssClass = "selectable"
                Else
                    Gr_DocCon.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_DocCon.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_DocCon.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_DocCon.Rows(i).Cells(1).Text = btn.ToolTip) Then

                Me.btn_Guardar.Enabled = True

                Me.Txt_Codigo.Text = Gr_DocCon.Rows(i).Cells(1).Text

                Me.Txt_Descripcion.Text = Server.HtmlDecode(Gr_DocCon.Rows(i).Cells(2).Text)
                Me.Txt_Descripcion.CssClass = "clsMandatorio"

                Txt_Descripcion.ReadOnly = False

                Me.DP_Estados.CssClass = "clsMandatorio"
                Me.DP_Estados.Enabled = True

                SW = 2

                If Me.Gr_DocCon.Rows(i).Cells(3).Text.StartsWith("A") Then
                    Me.DP_Estados.SelectedValue = 1
                Else
                    Me.DP_Estados.SelectedValue = 0

                End If

            End If

        Next

    End Sub

    Protected Sub RB_Opcion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Opcion.SelectedIndexChanged
        CargaGrilla()
    End Sub

    Private Sub CargaGrilla()

        Gr_DocCon.DataSource = CG.DocConComDevuelve(RB_Opcion.SelectedValue)
        Gr_DocCon.DataBind()

        If Gr_DocCon.Rows.Count <= 0 Then
            Msj.Mensaje(Me, "Atención", "No se encontraron registros", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

        For i = 0 To Gr_DocCon.Rows.Count - 1
            If Gr_DocCon.Rows(i).Cells(3).Text = 1 Then
                Gr_DocCon.Rows(i).Cells(3).Text = "ACTIVO"
            Else
                Gr_DocCon.Rows(i).Cells(3).Text = "INACTIVO"
            End If
        Next

    End Sub

End Class
