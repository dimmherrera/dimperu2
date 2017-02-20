Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Bancos
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim caption As String = "Bancos"
    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                BloqueaControlesBco()
                BloqueaControlesSuc()
                CargaDrop()
                RB_Suc.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Bco_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Bco.CheckedChanged
        Try
            If RB_Bco.Checked = True Then
                BloqueaControlesSuc()
                DesbloqueaControlesBco()
                CG.BancoDevuelve(True, Gr_Bco)
                RB_Suc.Checked = False
                IB_Nuevo.Enabled = True
                Gr_Suc.DataSource = ""
                Gr_Suc.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Suc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Suc.CheckedChanged
        Try
            If RB_Suc.Checked = True Then
                RB_Bco.Checked = False
                BloqueaControlesBco()
                'DesbloqueaControlesSuc()
                For i = 0 To Gr_Suc.Rows.Count - 1
                    Gr_Suc.Rows(i).Enabled = True
                Next
            End If
            IB_Guardar.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub Gr_Bco_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Bco.RowDataBound
    '    If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '        e.Row.Attributes.Add("onClick", "DetalleBco(ctl00_ContentPlaceHolder1_Gr_Bco, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub

    'Protected Sub Gr_Suc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Suc.RowDataBound
    '    If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '        e.Row.Attributes.Add("onClick", "DetalleSuc(ctl00_ContentPlaceHolder1_Gr_Suc, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub

    'Protected Sub Link_Bco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Bco.Click
    '    Try

    '        txt_Des_Bco.Text = ""
    '        txt_id_Bco.Text = ""

    '        txt_id_Bco.Text = HF_Idbco.Value 'Gr_Bco.Rows(HF_Idbco.Value - 1).Cells(0).Text
    '        txt_id_Bco.ReadOnly = True
    '        txt_id_Bco.CssClass = "clsDisabled"
    '        txt_Des_Bco.ReadOnly = False
    '        txt_Des_Bco.CssClass = "clsMandatorio"

    '        txt_Des_Bco.Text = Gr_Bco.Rows(HF_PosBco.Value).Cells(1).Text

    '        For i = 0 To Gr_Bco.Rows.Count - 1
    '            'Gr_Bco.Rows(i).CssClass = "formatable"
    '            If HF_PosBco.Value >= 0 And HF_Idbco.Value >= 0 Then
    '                Gr_Bco.Rows(HF_PosBco.Value).CssClass = "clicktable"
    '                Gr_Bco.Rows(i).CssClass = "formatable"
    '            End If
    '        Next

    '        CG.SBCDevuelveporBanco(HF_Idbco.Value, True, Gr_Suc)
    '        RB_Suc.Enabled = True

    '        IB_Guardar.Enabled = True
    '        For i = 0 To Gr_Suc.Rows.Count - 1
    '            Gr_Suc.Rows(i).Enabled = False

    '        Next

    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Protected Sub Link_Suc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Suc.Click
    '    Try
    '        txt_ID_Suc.Text = ""
    '        txt_Des_Suc.Text = ""

    '        For i = 0 To Gr_Suc.Rows.Count - 1
    '            Gr_Suc.Rows(i).CssClass = "formatable"
    '            If HF_PosSuc.Value >= 0 And HF_IdSuc.Value >= 0 Then
    '                Gr_Suc.Rows(HF_PosSuc.Value).CssClass = "clicktable"

    '            End If
    '        Next
    '        Dim s As New sbc_cls
    '        s = CG.SucursalbancocDevuelveObjeto(HF_IdSuc.Value)
    '        txt_ID_Suc.Text = s.id_sbc
    '        txt_Des_Suc.Text = s.sbc_des
    '        Drop_Plaza.SelectedValue = s.id_pl_000047

    '        txt_Des_Suc.ReadOnly = False
    '        Drop_Plaza.Enabled = True
    '        txt_Des_Suc.CssClass = "clsMandatorio"
    '        Drop_Plaza.CssClass = "clsMandatorio"
    '        'IB_Eliminar.Enabled = True
    '        IB_Guardar.Enabled = True

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try

            If RB_Bco.Checked = True Then
                'Inserta
                If HF_Idbco.Value = "" Then
                    Dim b As New bco_cls
                    b.bco_des = UCase(txt_Des_Bco.Text)
                    b.id_bco = Me.txt_id_Bco.Text
                    'b.bco_cod = b.id_bco
                    AG.BancoInserta(b)
                    Msj.Mensaje(Me.Page, caption, "Banco Guardado", TipoDeMensaje._Exclamacion)
                    txt_Des_Bco.Text = ""
                    BloqueaControlesBco()
                    CG.bancoDevuelve(True, Gr_Bco)
                Else 'Modifica

                    AG.BancoModifica(txt_id_Bco.Text, UCase(txt_Des_Bco.Text))
                    Msj.Mensaje(Me.Page, caption, "Banco Modificado", TipoDeMensaje._Exclamacion)
                    txt_id_Bco.Text = ""
                    txt_Des_Bco.Text = ""
                    BloqueaControlesBco()
                    CG.bancoDevuelve(True, Gr_Bco)
                End If
            End If


            If RB_Suc.Checked = True Then
                If txt_ID_Suc.Text = "" Then
                    Dim s As New sbc_cls
                    s.id_bco = HF_Idbco.Value
                    s.id_pl_000047 = Drop_Plaza.SelectedValue
                    s.sbc_des = UCase(txt_Des_Suc.Text)
                    AG.SBCInserta(s)
                    Msj.Mensaje(Me.Page, caption, "Sucursal Guardada", TipoDeMensaje._Exclamacion)
                    BloqueaControlesSuc()
                    txt_Des_Suc.Text = ""
                    Drop_Plaza.ClearSelection()
                    CG.SBCDevuelveporBanco(HF_Idbco.Value, True, Gr_Suc)
                Else
                    AG.SucModifica(txt_ID_Suc.Text, UCase(txt_Des_Suc.Text), Drop_Plaza.SelectedValue)
                    Msj.Mensaje(Me.Page, caption, "Sucursal Modificada", TipoDeMensaje._Exclamacion)
                    BloqueaControlesSuc()
                    txt_ID_Suc.Text = ""
                    txt_Des_Suc.Text = ""
                    Drop_Plaza.ClearSelection()
                    CG.SBCDevuelveporBanco(HF_Idbco.Value, True, Gr_Suc)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Public y Sub"
    Public Sub BloqueaControlesBco()
        Try
            txt_id_Bco.ReadOnly = True
            txt_Des_Bco.ReadOnly = True
            txt_id_Bco.CssClass = "clsDisabled"
            txt_Des_Bco.CssClass = "clsDisabled"
            txt_id_Bco.Text = ""
            IB_Guardar.Enabled = False
            For i = 0 To Gr_Bco.Rows.Count - 1
                Gr_Bco.Rows(i).Enabled = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BloqueaControlesSuc()
        Try
            txt_ID_Suc.ReadOnly = True
            txt_Des_Suc.ReadOnly = True
            Drop_Plaza.Enabled = False

            txt_ID_Suc.Text = ""
            txt_Des_Suc.Text = ""
            Drop_Plaza.ClearSelection()
            txt_ID_Suc.CssClass = "clsDisabled"
            txt_Des_Suc.CssClass = "clsDisabled"
            Drop_Plaza.CssClass = "clsDisabled"
            IB_Guardar.Enabled = False
            For i = 0 To Gr_Suc.Rows.Count - 1
                Gr_Suc.Rows(i).Enabled = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Public Sub DesbloqueaControlesBco()
        txt_Des_Bco.ReadOnly = False
        txt_id_Bco.CssClass = "clsDisabled"
        'txt_Des_Bco.CssClass = "clsMandatorio"
        For i = 0 To Gr_Bco.Rows.Count - 1
            Gr_Bco.Rows(i).Enabled = True
        Next
    End Sub
    Public Sub DesbloqueaControlesSuc()
        txt_Des_Suc.ReadOnly = False
        Drop_Plaza.Enabled = True

        ' txt_ID_Suc.CssClass = "clsMandatorio"
        txt_Des_Suc.CssClass = "clsMandatorio"
        Drop_Plaza.CssClass = "clsMandatorio"
        Drop_Plaza.ClearSelection()
        For i = 0 To Gr_Suc.Rows.Count - 1
            Gr_Suc.Rows(i).Enabled = True
        Next
    End Sub
    Public Sub Limpia()
        txt_id_Bco.Text = ""
        txt_id_Bco.CssClass = "clsDisabled"
        txt_id_Bco.ReadOnly = True
        txt_id_Bco.Text = ""
        txt_ID_Suc.Text = ""
        txt_Des_Bco.Text = ""
        RB_Bco.Checked = False
        RB_Suc.Checked = False
        RB_Suc.Enabled = False

        txt_Des_Suc.Text = ""
        Drop_Plaza.ClearSelection()

        txt_Des_Bco.ReadOnly = True
        txt_Des_Suc.ReadOnly = True
        Drop_Plaza.Enabled = False
        txt_Des_Bco.CssClass = "clsDisabled"
        txt_Des_Suc.CssClass = "clsDisabled"
        Drop_Plaza.CssClass = "clsDisabled"

        Gr_Bco.DataSource = Nothing
        Gr_Bco.DataBind()
        Gr_Suc.DataSource = Nothing
        Gr_Suc.DataBind()
        IB_Guardar.Enabled = False
        IB_Nuevo.Enabled = False



    End Sub

    Public Sub CargaDrop()
        CG.ParametrosAlfanumericoDevuelve(2, True, Drop_Plaza)

    End Sub


#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20021312, Usr, "PRESIONA BOTON NUEVO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        HF_PosBco.Value = ""
        Try
            If RB_Bco.Checked = True Then
                txt_id_Bco.Text = ""
                txt_Des_Bco.Text = ""
                '   DesbloqueaControlesBco()

                txt_Des_Bco.ReadOnly = False

                txt_id_Bco.CssClass = "clsMandatorio"
                txt_id_Bco.Enabled = True
                txt_id_Bco.ReadOnly = False

                'txt_Des_Bco.CssClass = "clsMandatorio"
                For i = 0 To Gr_Bco.Rows.Count - 1
                    Gr_Bco.Rows(i).Enabled = True
                Next

                txt_Des_Bco.CssClass = "clsMandatorio"
                txt_Des_Bco.Focus()
            End If
            If RB_Suc.Checked = True Then
                txt_ID_Suc.Text = ""
                txt_Des_Suc.Text = ""
                DesbloqueaControlesSuc()
                txt_Des_Suc.Focus()
            End If

            IB_Guardar.Enabled = True
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20011312, Usr, "PRESIONA BOTON GUARDAR") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            'Guarda Banco 
            If RB_Bco.Checked = True Then


                If txt_id_Bco.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Codigo Banco", TipoDeMensaje._Exclamacion)
                    txt_Des_Bco.Focus()
                    Exit Sub
                End If


                If txt_Des_Bco.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Descripcion Banco", TipoDeMensaje._Exclamacion)
                    txt_Des_Bco.Focus()
                    Exit Sub
                End If

            End If
            If RB_Suc.Checked = True Then
                If txt_Des_Suc.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Sucursal", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If Drop_Plaza.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione Plaza", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If RB_Bco.Checked = True Then
                If HF_PosBco.Value = "" Then
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar banco?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                Else
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar banco?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                End If
            End If

            If RB_Suc.Checked = True Then
                If txt_ID_Suc.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar sucursal?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                Else
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar sucursal?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpia()
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Eliminar.Click
    '    Try
    '        If RB_Bco.Checked = True Then

    '        End If

    '        If RB_Suc.Checked = True Then
    '            Actualiza.SbcElimina(txt_ID_Suc.Text)
    '            Msj(caption, "Sucursal Eliminada", TipoDeMensaje._Exclamacion)
    '            'cons.SBCDevuelveporBanco(HF_Idbco.Value, True, Gr_Suc)
    '            BloqueaControlesSuc()
    '            IB_Eliminar.Enabled = False
    '        End If
    '    Catch ex As Exception
    '        Msj(caption, ex.Message, TipoDeMensaje._Error)
    '    End Try
    'End Sub
#End Region


    Protected Sub txt_id_Bco_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_id_Bco.TextChanged
        If Me.txt_id_Bco.CssClass = "clsMandatorio" Then



            If Not IsNothing(CG.CodigoBancoValida(txt_id_Bco.Text)) Then

                Msj.Mensaje(Me.Page, caption, "Este codigo ya ha sido asignado a un banco , favor ingrese uno diferente", TipoDeMensaje._Exclamacion, , )
                Me.txt_id_Bco.Text = ""
                Exit Sub

            End If

        End If
    End Sub

    Protected Sub BtnVerBanco_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_Idbco.Value = btn.ToolTip

        For i = 0 To Gr_Bco.Rows.Count - 1
            If (Gr_Bco.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Bco.Rows(i).CssClass = "selectable"
                Else
                    Gr_Bco.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Bco.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Bco.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Bco.Rows(i).Cells(1).Text = btn.ToolTip) Then

                txt_Des_Bco.Text = ""
                txt_id_Bco.Text = ""

                txt_id_Bco.Text = Gr_Bco.Rows(i).Cells(1).Text
                txt_id_Bco.ReadOnly = True
                txt_id_Bco.CssClass = "clsDisabled"
                txt_Des_Bco.ReadOnly = False
                txt_Des_Bco.CssClass = "clsMandatorio"

                txt_Des_Bco.Text = Gr_Bco.Rows(i).Cells(2).Text

                CG.SBCDevuelveporBanco(CInt(txt_id_Bco.Text), True, Gr_Suc)
                RB_Suc.Enabled = True

                IB_Guardar.Enabled = True
                
            End If
            For x = 0 To Gr_Suc.Rows.Count - 1
                Gr_Suc.Rows(x).Enabled = False
            Next
        Next
    End Sub

    Protected Sub BtnVerSucursal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_IdSuc.Value = btn.ToolTip

        For i = 0 To Gr_Suc.Rows.Count - 1
            If (Gr_Suc.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Suc.Rows(i).CssClass = "selectable"
                Else
                    Gr_Suc.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Suc.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Suc.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Suc.Rows(i).Cells(1).Text = btn.ToolTip) Then

                txt_ID_Suc.Text = ""
                txt_Des_Suc.Text = ""

                Dim s As New sbc_cls
                s = CG.SucursalbancocDevuelveObjeto(Gr_Suc.Rows(i).Cells(1).Text)
                txt_ID_Suc.Text = s.id_sbc
                txt_Des_Suc.Text = s.sbc_des
                Drop_Plaza.SelectedValue = s.id_pl_000047

                txt_Des_Suc.ReadOnly = False
                Drop_Plaza.Enabled = True
                txt_Des_Suc.CssClass = "clsMandatorio"
                Drop_Plaza.CssClass = "clsMandatorio"
                'IB_Eliminar.Enabled = True
                IB_Guardar.Enabled = True

            End If
        Next
    End Sub
End Class
