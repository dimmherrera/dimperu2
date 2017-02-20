Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class ClsRequisitos
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim cd As New ClaseControlDual
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
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20021012, Usr, "PRESIONO GUARDAR REQUISITO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Msj.Mensaje(Me, "Atención", "¿Desea Guardar el Requisito?", ClsMensaje.TipoDeMensaje._Confirmacion, Lb_gua.UniqueID)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cd.RequisitosDevuelveTodos(True, Gr_Requisitos, 1)
          
        End If

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
    'Protected Sub Gr_Requisitos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Requisitos.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Requisitos, 'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Requisitos, 'formatable')")
    '        e.Row.Attributes.Add("onClick", "DetalleRequisito(ctl00_ContentPlaceHolder1_Gr_Requisitos, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub
    Protected Sub Lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_gua.Click

        Try

            If Txt_Descripcion.Text.Trim = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Descripcion", TipoDeMensaje._Informacion)
                Exit Sub
            End If
            If DP_Estados.SelectedValue = "S" Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Estado", TipoDeMensaje._Informacion)
                Exit Sub
            End If
            Dim Req As New req_cls
            If Hf_pos.Value = "" Then
                Req.id_req = Nothing
                Req.req_des = Txt_Descripcion.Text.Trim.ToUpper
                Req.req_est = DP_Estados.SelectedValue.ToUpper
                If cd.RequisitosInserta(Req) Then
                    Msj.Mensaje(Me.Page, Caption, "Requisito Guardado", TipoDeMensaje._Informacion)
                    Limpia()
                    cd.RequisitosDevuelveTodos(True, Gr_Requisitos, 1)
                Else
                    Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                End If
            Else
                Req.id_req = CInt(Txt_Codigo.Text)
                Req.req_des = Txt_Descripcion.Text.Trim.ToUpper
                Req.req_est = DP_Estados.SelectedValue.ToUpper
                If cd.RequisitosActualiza(Req) Then
                    Msj.Mensaje(Me.Page, Caption, "Requisito Modificado", TipoDeMensaje._Informacion)
                    Limpia()
                    cd.RequisitosDevuelveTodos(True, Gr_Requisitos, 1)
                Else
                    Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar", TipoDeMensaje._Informacion)
                End If
            End If

            btn_Guardar.Enabled = False
            DP_Estados.CssClass = "clsDisabled"
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    'Protected Sub Lb_mod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_mod.Click

    '    Me.btn_Guardar.Enabled = True

    '    Dim posicion As Integer
    '    posicion = Val(Hf_pos.Value)

    '    Me.Txt_Codigo.Text = Gr_Requisitos.Rows(posicion).Cells(0).Text
    '    Me.Txt_Codigo.Text = Gr_Requisitos.Rows(posicion).Cells(0).Text

    '    Me.Txt_Descripcion.Text = Gr_Requisitos.Rows(posicion).Cells(1).Text
    '    Me.Txt_Descripcion.CssClass = "clsMandatorio"

    '    Me.DP_Estados.CssClass = "clsMandatorio"
    '    Me.DP_Estados.Enabled = True

    '    SW = 2

    '    If Me.Gr_Requisitos.Rows(posicion).Cells(2).Text.StartsWith("A") Then
    '        Me.DP_Estados.SelectedValue = "A"
    '    Else
    '        Me.DP_Estados.SelectedValue = "I"

    '    End If

    '    For i = 0 To Gr_Requisitos.Rows.Count - 1

    '        If i = Hf_pos.Value Then
    '            Me.Gr_Requisitos.Rows(i).CssClass = "clicktable"
    '        Else
    '            Me.Gr_Requisitos.Rows(i).CssClass = "formatable"
    '        End If

    '    Next


    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gr_Requisitos.Rows.Count - 1

            If (Gr_Requisitos.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Requisitos.Rows(i).CssClass = "selectable"
                Else
                    Gr_Requisitos.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Requisitos.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Requisitos.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Requisitos.Rows(i).Cells(1).Text = btn.ToolTip) Then

                Me.btn_Guardar.Enabled = True

                Me.Txt_Codigo.Text = Gr_Requisitos.Rows(i).Cells(1).Text

                Me.Txt_Descripcion.Text = Gr_Requisitos.Rows(i).Cells(2).Text
                Me.Txt_Descripcion.CssClass = "clsMandatorio"

                Me.DP_Estados.CssClass = "clsMandatorio"
                Me.DP_Estados.Enabled = True

                SW = 2

                If Me.Gr_Requisitos.Rows(i).Cells(3).Text.StartsWith("A") Then
                    Me.DP_Estados.SelectedValue = "A"
                Else
                    Me.DP_Estados.SelectedValue = "I"

                End If

            End If
        Next
    End Sub
End Class