Imports CapaDatos
Imports System.Data
Imports FuncionesGenerales.Errores
Imports ClsSession.ClsSession
Partial Class Notario
    Inherits System.Web.UI.Page
    
    Private Coll_Drop As New Collection
    Private Coll_Cons As New Collection
    Private Str, Msg As String
    Dim err As FuncionesGenerales.Errores

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Mantencion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Me.IsPostBack Then
                cg.SucursalesDevuelve(CodEje, True, Me.Dr_suc)
                hab_inhab_controles("I")
            End If
        Catch ex As Exception
            Err = New FuncionesGenerales.Errores(99, ex.Message)
        End Try
    End Sub


    Public Sub hab_inhab_controles(ByVal val As String)


        If val = "I" Then
            Me.Txt_corr.Text = ""
            Me.Txt_corr.CssClass = "clsDisabled"
            Me.Txt_corr.ReadOnly = True

            Me.Txt_dir.Text = ""
            Me.Txt_dir.CssClass = "clsDisabled"
            Me.Txt_dir.ReadOnly = True


            Me.Txt_diremp.Text = ""
            Me.Txt_diremp.CssClass = "clsDisabled"
            Me.Txt_diremp.ReadOnly = True

            Me.Txt_nom.Text = ""
            Me.Txt_nom.CssClass = "clsDisabled"
            Me.Txt_nom.ReadOnly = True

            Me.Txt_tel.Text = ""
            Me.Txt_tel.CssClass = "clsDisabled"
            Me.Txt_tel.ReadOnly = True



            Me.Ch_def.Checked = False
            
            Me.btn_guar.Enabled = False
            Me.btn_eli.Enabled = False
            Me.btn_nue.Enabled = True
        Else

            Me.Txt_corr.Text = ""
            Me.Txt_corr.CssClass = "clsMandatorio"
            Me.Txt_corr.ReadOnly = False

            Me.Txt_dir.Text = ""
            Me.Txt_dir.CssClass = "clsMandatorio"
            Me.Txt_dir.ReadOnly = False


            Me.Txt_diremp.Text = ""
            Me.Txt_diremp.CssClass = "clsMandatorio"
            Me.Txt_diremp.ReadOnly = False

            Me.Txt_nom.Text = ""
            Me.Txt_nom.CssClass = "clsMandatorio"
            Me.Txt_nom.ReadOnly = False

            Me.Txt_tel.Text = ""
            Me.Txt_tel.CssClass = "clsMandatorio"
            Me.Txt_tel.ReadOnly = False


            Me.btn_guar.Enabled = True
            Me.btn_eli.Enabled = True
            Me.btn_nue.Enabled = True



        End If



    End Sub

    Protected Sub Dr_suc_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_suc.SelectedIndexChanged
        cg.NotarioDevuelveTodosPorSucursal(Me.Gv_Notario, Me.Dr_suc.SelectedValue)
        Me.btn_nue.Enabled = True
    End Sub

    'Protected Sub Gv_Notario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv_Notario.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gv_Notario,'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gv_Notario,'formatable')")
    '        e.Row.Attributes.Add("onClick", "Detallenotario(ctl00_ContentPlaceHolder1_Gv_Notario,  'clicktable', 'formatable', 'selectable');")
    '    End If
    'End Sub

    Public Sub limpiar()
        Me.Txt_corr.Text = ""
        Me.Txt_dir.Text = ""
        Me.Txt_diremp.Text = ""
        Me.Txt_nom.Text = ""
        Me.Txt_tel.Text = ""
        Me.Dr_suc.SelectedIndex = 0
        Me.Ch_def.Checked = False
        Me.Gv_Notario.Controls.Clear()

        Me.btn_guar.Enabled = False
        Me.btn_eli.Enabled = False
        Me.btn_nue.Enabled = True
    End Sub


    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click

        msj.Mensaje(Me, "Atención", "Desea Eliminar el Registro", ClsMensaje.TipoDeMensaje._Confirmacion, lb_eli.UniqueID)



    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nue.Click

        Try

            hab_inhab_controles("H")

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_limp1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_lim.Click

        limpiar()
        hab_inhab_controles("I")

    End Sub

    'Protected Sub Detalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detalle.Click

    '    Dim i As Integer

    '    Me.Txt_dir.Text = Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(4).Text
    '    Me.Txt_dir.CssClass = "clsMandatorio"
    '    Me.Txt_dir.ReadOnly = False

    '    Me.Txt_diremp.Text = Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(5).Text
    '    Me.Txt_diremp.CssClass = "clsMandatorio"
    '    Me.Txt_diremp.ReadOnly = False



    '    Me.Txt_tel.Text = Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(6).Text
    '    Me.Txt_tel.CssClass = "clsMandatorio"
    '    Me.Txt_tel.ReadOnly = False



    '    For i = 0 To Me.Dr_suc.Items.Count - 1

    '        If Trim(Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(3).Text) = Trim(Me.Dr_suc.Items(i).Text) Then
    '            Me.Dr_suc.SelectedIndex = i
    '        End If
    '    Next

    '    Me.Txt_corr.Text = Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(1).Text
    '    Me.Txt_corr.CssClass = "clsMandatorio"
    '    Me.Txt_corr.ReadOnly = False

    '    Me.Txt_nom.Text = Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(2).Text
    '    Me.Txt_nom.CssClass = "clsMandatorio"
    '    Me.Txt_nom.ReadOnly = False


    '    For i = 0 To Me.Gv_Notario.Rows.Count - 1
    '        If i = Val(pos_gr.Value) Then
    '            Me.Gv_Notario.Rows(i).CssClass = "clicktable"
    '        Else
    '            Me.Gv_Notario.Rows(i).CssClass = "formatable"

    '        End If
    '    Next


    '    If Me.Gv_Notario.Rows(Val(pos_gr.Value)).Cells(7).Text = "S" Then

    '        Me.Ch_def.Checked = True

    '    Else

    '        Me.Ch_def.Checked = False

    '    End If

    '    Me.btn_eli.Enabled = True
    '    Me.btn_guar.Enabled = True
    'End Sub

    Protected Sub btn_guar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guar.Click


        If Me.Txt_nom.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar un Nombre", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.Txt_dir.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar una Dirección", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.Txt_tel.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar un telefono", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.Txt_diremp.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar una Dirección de Empresa", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        If Me.Txt_corr.Text <> "" Then

            msj.Mensaje(Me, "Atención", "Desea Modificar", ClsMensaje.TipoDeMensaje._Confirmacion, lb_guarda.UniqueID)



        Else



            msj.Mensaje(Me, "Atención", "Desea Guardar Notarios", ClsMensaje.TipoDeMensaje._Confirmacion, lb_guarda.UniqueID)


        End If


    End Sub

    Protected Sub lb_guarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guarda.Click

        Dim def As String
        Dim val As Boolean
        Dim cor As Integer


        If Me.Txt_corr.Text = "" Then
            cor = 0
        Else
            cor = Me.Txt_corr.Text
        End If


        If Me.Ch_def.Checked = True Then
            def = "S"
        Else
            def = "N"
        End If
        val = ag.GuardaNotario(cor, Me.Dr_suc.SelectedValue, Me.Txt_dir.Text, Me.Txt_diremp.Text, Me.Txt_nom.Text, Me.Txt_tel.Text, def)

        cg.NotarioDevuelveTodosPorSucursal(Me.Gv_Notario, Me.Dr_suc.SelectedValue)

        If val = True Then
            If cor = 0 Then
                msj.Mensaje(Me, "Atención", "Registro Ingresado", 2)
            Else
                msj.Mensaje(Me, "Atención", "Registro Actualizado", 2)
            End If
            hab_inhab_controles("I")
        Else
            msj.Mensaje(Me, "Error", "No se pudo ingresar el registro", 1)
        End If

    End Sub

    Protected Sub lb_eli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_eli.Click

        Dim i As String

        If Me.Ch_def.Checked = True Then

            i = "S"

        Else

            i = "N"

        End If

        Me.ag.eliminanotario(Me.Txt_corr.Text, Me.Dr_suc.SelectedValue, i)

        msj.Mensaje(Me, "Atención", "Registro Eliminado", 3)

        cg.NotarioDevuelveTodosPorSucursal(Me.Gv_Notario, Me.Dr_suc.SelectedValue)

        Me.DataBind()

       hab_inhab_controles("I")
        Me.btn_guar.Enabled = False
        Me.btn_eli.Enabled = False
        Me.btn_nue.Enabled = True
    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gv_Notario.Rows.Count - 1

            If (Gv_Notario.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gv_Notario.Rows(i).CssClass = "selectable"
                Else
                    Gv_Notario.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gv_Notario.Rows(i).CssClass = "formatUltcell"
                Else
                    Gv_Notario.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gv_Notario.Rows(i).Cells(1).Text = btn.ToolTip) Then

                Me.Txt_dir.Text = Me.Gv_Notario.Rows(i).Cells(4).Text
                Me.Txt_dir.CssClass = "clsMandatorio"
                Me.Txt_dir.ReadOnly = False

                Me.Txt_diremp.Text = Me.Gv_Notario.Rows(i).Cells(5).Text
                Me.Txt_diremp.CssClass = "clsMandatorio"
                Me.Txt_diremp.ReadOnly = False

                Me.Txt_tel.Text = Me.Gv_Notario.Rows(i).Cells(6).Text
                Me.Txt_tel.CssClass = "clsMandatorio"
                Me.Txt_tel.ReadOnly = False

                For a = 0 To Me.Dr_suc.Items.Count - 1

                    If Trim(Me.Gv_Notario.Rows(i).Cells(3).Text) = Trim(Me.Dr_suc.Items(a).Text) Then
                        Me.Dr_suc.SelectedIndex = a
                    End If
                Next

                Me.Txt_corr.Text = Me.Gv_Notario.Rows(i).Cells(1).Text
                Me.Txt_corr.CssClass = "clsMandatorio"
                Me.Txt_corr.ReadOnly = False

                Me.Txt_nom.Text = Me.Gv_Notario.Rows(i).Cells(2).Text
                Me.Txt_nom.CssClass = "clsMandatorio"
                Me.Txt_nom.ReadOnly = False

                If Me.Gv_Notario.Rows(i).Cells(7).Text = "S" Then
                    Me.Ch_def.Checked = True
                Else
                    Me.Ch_def.Checked = False
                End If
            End If
        Next
        Me.btn_eli.Enabled = True
        Me.btn_guar.Enabled = True
    End Sub

End Class
