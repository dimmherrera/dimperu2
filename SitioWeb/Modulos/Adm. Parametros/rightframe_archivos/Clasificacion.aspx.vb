Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports FuncionesGenerales.Errores
Imports System.Web.UI.UserControl
Imports CapaDatos
Public Class ClsIngOpe
    Inherits System.Web.UI.Page

#Region "Declaración de Variables privadas"

    Private idx As Int16
    Private sesion As New ClsSession.ClsSession
    Private err As FuncionesGenerales.Errores
    Private cg As New ConsultasGenerales
    Dim cd As New ClaseControlDual
    Private ag As New ActualizacionesGenerales
    Private Caption As String = "Clasificación"
    Private msj As New ClsMensaje

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Response.Expires = -1

            sesion.coll_cfc = New Collection

            cg.ParametrosDevuelve(69, True, Me.Dr_tipo_cfc)

            txt_dde.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_hta.Attributes.Add("Style", "TEXT-ALIGN: right")

            If modifica = False Then
                Me.txt_cod.Text = cd.carga_corr_Ccf

                HabilitaClasificacion(True)
                HabilitaRangos(False)
                txt_descripcion.Focus()
                IB_Guadar.ValidationGroup = "Clasificacion"

                btn_agregar.Enabled = False
                btn_quitar.Enabled = False

            Else

                HabilitaClasificacion(True)
                Me.txt_cod.ReadOnly = True
                HabilitaRangos(True)
                txt_dde.Focus()
                IB_Guadar.ValidationGroup = "Rango"
                btn_agregar.Enabled = True
                btn_quitar.Enabled = True

                Me.txt_cod.Text = ccfnum

                sesion.coll_ccf = cd.CabeceraClasificacion_datos_devuelve(ccfnum)

                Me.dr_apb.SelectedValue = sesion.coll_ccf.Item(1).ccf_tip_apb
                Me.dr_est.SelectedValue = sesion.coll_ccf.Item(1).ccf_est

                sesion.coll_cfc = cd.DevuelveDetalleClasificacion(ccfnum)

                Me.gr_cfc.DataSource = sesion.coll_cfc
                Me.gr_cfc.DataBind()

                Me.txt_descripcion.Text = sesion.coll_ccf.Item(1).Descripcion

            End If

        End If

    End Sub

    Private Function ValidaRangos() As Boolean

        Try

            Dim colrango As New Collection
            Dim Desde As Double
            Dim Hasta As Double
            Dim omite As Integer
            Dim Fila As Integer

            '-----------------------------------------------------------------------------------------------------------------------------
            'Asignamos los valores segun correspondan (decimales o interos)
            '-----------------------------------------------------------------------------------------------------------------------------
            Select Case Dr_tipo_cfc.SelectedValue
                Case 1, 3, 4, 5
                    Desde = Replace(Me.txt_dde.Text, ".", ",")
                    Hasta = Replace(Me.txt_hta.Text, ".", ",")
                Case 2
                    Desde = txt_dde.Text
                    Hasta = txt_hta.Text
            End Select
            
            If Desde > Hasta Then
                msj.Mensaje(Me.Page, Caption, "El valor desde no puede ser mayor que el valor hasta", TipoDeMensaje._Exclamacion)
                Return False
            End If

            If Me.txt_pos.Text <> "" Then
                Fila = Val(txt_pos.Text)
            Else

                '-----------------------------------------------------------------------------------------------------------------------------
                'Valida si criterio ya esta para cuando ingresa uno nuevo
                '-----------------------------------------------------------------------------------------------------------------------------
                For I = 0 To gr_cfc.Rows.Count - 1

                    If Me.Dr_tipo_cfc.SelectedItem.Text = Me.gr_cfc.Rows(I).Cells(1).Text Then

                        msj.Mensaje(Me.Page, Caption, "Ya existe este criterio  " & Me.Dr_tipo_cfc.SelectedItem.Text, TipoDeMensaje._Exclamacion)

                        Return False

                    End If
                Next

            End If



            '-----------------------------------------------------------------------------------------------------------------------------
            'Traemos los todos los rangos que existen para esta clasificacion
            '-----------------------------------------------------------------------------------------------------------------------------
            colrango = cd.carga_rangos_clasificacion(Me.txt_cod.Text)

            For i = 1 To colrango.Count

                '-----------------------------------------------------------------------------------------------------------------------------
                'Solo para cuando modifique se valida
                '-----------------------------------------------------------------------------------------------------------------------------
                If Me.txt_pos.Text <> "" Then

                    If Me.gr_cfc.Rows.Count = 0 Then
                        Return True
                    End If

                    If Me.Dr_tipo_cfc.SelectedItem.Text = Me.gr_cfc.Rows(Fila).Cells(1).Text Then

                        If colrango.Item(i).id_p_0069 = Me.Dr_tipo_cfc.SelectedValue And colrango.Item(i).id_ccf = Me.txt_cod.Text Then
                            omite = i
                        End If

                    End If

                End If



                If i <> omite Then

                    If colrango.Item(i).id_p_0069 = Me.Dr_tipo_cfc.SelectedValue Then

                        If (Desde >= colrango.Item(i).cfc_dde And Desde <= colrango.Item(i).cfc_hta) Or _
                           (Hasta >= colrango.Item(i).cfc_dde And Hasta <= colrango.Item(i).cfc_hta) Then

                            msj.Mensaje(Me.Page, Caption, "Ya existe este rango para el criterio  " & Me.Dr_tipo_cfc.SelectedItem.Text, TipoDeMensaje._Exclamacion)

                            Return False

                        End If

                    End If

                End If

            Next

            If Not IsNothing(sesion.coll_cfc) Then

                For i = 1 To sesion.coll_cfc.Count

                    If Me.Dr_tipo_cfc.SelectedValue = sesion.coll_cfc.Item(i).id_p_0069 And sesion.coll_cfc.Item(i).id_ccf <> Val(Me.txt_cod.Text) Then

                        If (Desde >= sesion.coll_cfc.Item(i).cfc_dde And Desde <= sesion.coll_cfc.Item(i).cfc_hta) Or _
                           (Hasta >= sesion.coll_cfc.Item(i).cfc_dde And Hasta <= sesion.coll_cfc.Item(i).cfc_hta) Then

                            msj.Mensaje(Me.Page, Caption, "Ya existe este rango para el criterio  " & Me.Dr_tipo_cfc.SelectedItem.Text, TipoDeMensaje._Exclamacion)

                            Return False

                        End If

                    End If

                Next

            End If


            Return True

        Catch ex As Exception

        End Try

    End Function

    Protected Sub Dr_tipo_cfc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_tipo_cfc.SelectedIndexChanged

        If Dr_tipo_cfc.SelectedValue <> 2 Then

            txt_dde.MaxLength = 6
            txt_hta.MaxLength = 6

            txt_dde_MaskedEditExtender.Enabled = False
            txt_hta_MaskedEditExtender.Enabled = False

        Else

            txt_dde_MaskedEditExtender.Enabled = True
            txt_hta_MaskedEditExtender.Enabled = True

        End If

        txt_dde.Text = ""
        txt_hta.Text = ""
        txt_dde.Focus()

    End Sub

    Public Sub limpiar()

        'Me.txt_cod.Text = ""

        Dr_tipo_cfc.ClearSelection()
        txt_dde.Text = ""
        txt_hta.Text = ""
        txt_pos.Text = ""

        'Me.txt_descripcion.ReadOnly = False



        'Me.txt_dde.CssClass = "clsMandatorio"
        'Me.txt_hta.CssClass = "clsMandatorio"
        'Me.txt_descripcion.CssClass = "clsMandatorio"


        'Me.gr_cfc.Dispose()
        'Me.gr_cfc.Controls.Clear()
        'Me.dr_est.ClearSelection()
    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        limpiar()
        'Me.txt_cod.Text = cg.carga_corr_Ccf

    End Sub

    Protected Sub LB_CargaDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_CargaDetalle.Click

        'If sesion.caso = Val(Me.txt_pos.Text) Then

        '    Exit Sub
        'Else
        '    sesion.caso = Val(Me.txt_pos.Text)

        'End If

        '    sesion.coll_cfc = cg.DevuelveDetalleClasificacion(ccfnum)

        'If IsNothing(sesion.coll_cfc) = False Then
        '    Me.Dr_tipo_cfc.SelectedValue = sesion.coll_cfc.Item(Val(txt_pos.Text) + 1).id_p_0069
        '    Me.txt_dde.Text = sesion.coll_cfc.Item(txt_pos.Text + 1).cfc_dde
        '    Me.txt_hta.Text = sesion.coll_cfc.Item(txt_pos.Text + 1).cfc_hta
        'Else
        '    For i = 0 To Dr_tipo_cfc.Items.Count - 1
        '        If Me.Dr_tipo_cfc.Items(i).Text = Me.gr_cfc.Rows(Val(Me.txt_pos.Text)).Cells(0).Text Then
        '            Me.Dr_tipo_cfc.SelectedValue = i
        '        End If
        '    Next

        '    Me.txt_dde.Text = Me.gr_cfc.Rows(Me.txt_pos.Text).Cells(1).Text
        '    Me.txt_hta.Text = Me.gr_cfc.Rows(Me.txt_pos.Text).Cells(2).Text

        'End If

        'If Dr_tipo_cfc.SelectedValue <> 2 Then

        '    txt_dde.MaxLength = 6
        '    txt_hta.MaxLength = 6

        '    txt_dde_MaskedEditExtender.Enabled = False
        '    txt_hta_MaskedEditExtender.Enabled = False

        'Else

        '    'txt_dde.MaxLength = 10
        '    'txt_hta.MaxLength = 10

        '    txt_dde_MaskedEditExtender.Enabled = True
        '    txt_hta_MaskedEditExtender.Enabled = True

        'End If

        'MARCAGRILLA()
        'sesion.SW = 1

    End Sub

    'Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_cfc.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '        e.Row.Attributes.Add("onClick", "DetalleCfc(ctl00_ContentPlaceHolder1_gr_cfc, 'clicktable', 'formatable', 'selectable')")
    '    End If

    'End Sub

    Public Sub MARCAGRILLA()

        'For i = 0 To Me.gr_cfc.Rows.Count - 1

        '    If i = txt_pos.Text Then

        '        If Me.gr_cfc.Rows(i).CssClass = "clicktable" Then

        '            Me.gr_cfc.Rows(i).CssClass = "formatable"

        '            limpiar()
        '        Else
        '            Me.gr_cfc.Rows(i).CssClass = "clicktable"

        '        End If

        '    Else
        '        Me.gr_cfc.Rows(i).CssClass = "formatable"

        '    End If
        'Next

    End Sub

    Protected Sub btn_quitar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_quitar.Click

        If txt_pos.Text = "" Then

            msj.Mensaje(Me.Page, "Atención", "Debe Seleccionar algo para Eliminar", TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If IsNothing(sesion.coll_cfc) = False Then
            If coll_cfc.Count > 0 And Me.txt_pos.Text <> "" Then

                Dim valida As Boolean
                Dim val As Integer = CInt(txt_pos.Text)

                Me.Dr_tipo_cfc.ClearSelection()
                Me.txt_hta.Text = ""
                Me.txt_dde.Text = ""
                valida = cd.Valida_Asociaciones_por_clasificacion(Me.txt_cod.Text)
                If valida = True Then
                    msj.Mensaje(Me.Page, "Atención", "Esta clasificación se encuentra asociada a una operación, no se realizaran los cambios", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                cd.borra_detalle_clasificacion(Me.txt_cod.Text, sesion.coll_cfc.Item(val).id_cfc)
                sesion.coll_cfc.Remove(val)
                Me.gr_cfc.DataSource = sesion.coll_cfc
                Me.gr_cfc.DataBind()
            Else
                msj.Mensaje(Me.Page, "Atención", "Debe seleccionar antes de quitar", TipoDeMensaje._Exclamacion)
            End If

            msj.Mensaje(Me.Page, "Atención", "Registro Eliminado", TipoDeMensaje._Informacion)

        End If
    End Sub

    Protected Sub btn_volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_volver.Click
        Response.Redirect("Mantencion CCF.aspx", False)
    End Sub

    Protected Sub IB_Guadar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guadar.Click

        Try

            If modifica = False Then

                msj.Mensaje(Me.Page, Caption, "¿Esta seguro de querer guardar la clasificación?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

            Else

                '-------------------------------------------------------------------------------------------------------------------------
                'Valida los rangos desde y hasta segun su tipo
                '-------------------------------------------------------------------------------------------------------------------------
                msj.Mensaje(Me.Page, Caption, "¿Desea modificar la clasificación?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)


                If ValidaRangos() Then

                    msj.Mensaje(Me.Page, Caption, "¿Desea modificar la clasificación?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)
                Else
                    msj.Mensaje(Me.Page, Caption, "El rango que desea ingresar ya existe para otra clasificación", ClsMensaje.TipoDeMensaje._Excepcion)

                End If

            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub HabilitaClasificacion(ByVal Estado As Boolean)

        If Estado Then

            txt_descripcion.ReadOnly = False
            dr_apb.Enabled = True
            dr_est.Enabled = True

            txt_descripcion.CssClass = "clsMandatorio"
            dr_apb.CssClass = "clsMandatorio"
            dr_est.CssClass = "clsMandatorio"

        Else

            txt_descripcion.ReadOnly = True
            dr_apb.Enabled = False
            dr_est.Enabled = False

            txt_descripcion.CssClass = "clsDisabled"
            dr_apb.CssClass = "clsDisabled"
            dr_est.CssClass = "clsDisabled"

        End If

    End Sub

    Private Sub HabilitaRangos(ByVal Estado As Boolean)

        If Estado Then

            Me.txt_dde.ReadOnly = False
            Me.txt_hta.ReadOnly = False
            Me.Dr_tipo_cfc.Enabled = True

            Me.txt_dde.CssClass = "clsMandatorio"
            Me.txt_hta.CssClass = "clsMandatorio"
            Me.Dr_tipo_cfc.CssClass = "clsMandatorio"

        Else

            Me.txt_dde.ReadOnly = True
            Me.txt_hta.ReadOnly = True
            Me.Dr_tipo_cfc.Enabled = False

            Me.txt_dde.CssClass = "clsDisabled"
            Me.txt_hta.CssClass = "clsDisabled"
            Me.Dr_tipo_cfc.CssClass = "clsDisabled"

        End If

        
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        If modifica = False Then

            '-------------------------------------------------------------------------------------------------------------------------
            'Solo guardar la cabecera de la clasificacion y redirecciona 
            '-------------------------------------------------------------------------------------------------------------------------

            cd.guarda_cabecera_clasificacion(Me.txt_cod.Text, Me.dr_est.SelectedValue, Me.txt_descripcion.Text, dr_apb.SelectedValue)

            Response.Redirect("Mantencion CCF.aspx")

        Else
            cd.cabecera_clasificacion_modifica(Me.txt_cod.Text, Me.dr_est.SelectedValue, Me.txt_descripcion.Text, dr_apb.SelectedValue)
            '-------------------------------------------------------------------------------------------------------------------------
            'Valida que si la clasificacion esta asociacion a una operacion, esta no pueda ser modificada
            '-------------------------------------------------------------------------------------------------------------------------
            If gr_cfc.Rows.Count > 0 And txt_pos.Text <> "" Then


                If Me.txt_dde.Text <> Me.gr_cfc.Rows(Val(txt_pos.Text)).Cells(1).Text Or Me.txt_hta.Text <> Me.gr_cfc.Rows(Val(txt_pos.Text)).Cells(2).Text Then

                    If cd.Valida_Asociaciones_por_clasificacion(Me.txt_cod.Text) Then
                        msj.Mensaje(Me.Page, "Atención", "Esta clasificación se encuentra asociada a una operación, no se realizaran los cambios", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                End If

            End If


            Dim Desde As Double
            Dim Hasta As Double

            '-----------------------------------------------------------------------------------------------------------------------------
            'Asignamos los valores segun correspondan (decimales o interos)
            '-----------------------------------------------------------------------------------------------------------------------------
            Select Case Dr_tipo_cfc.SelectedValue
                Case 1, 3, 4, 5
                    Desde = Replace(Me.txt_dde.Text, ".", ",")
                    Hasta = Replace(Me.txt_hta.Text, ".", ",")
                Case 2
                    Desde = txt_dde.Text
                    Hasta = txt_hta.Text
            End Select


            If txt_pos.Text <> "" Then

                '-------------------------------------------------------------------------------------------------------------------------
                'Modifica rango 
                '-------------------------------------------------------------------------------------------------------------------------

                If Me.Dr_tipo_cfc.SelectedValue = 0 Then
                    cd.modifica_detalle_clasificacion(Me.txt_cod.Text, sesion.coll_cfc.Item(Val(Me.txt_pos.Text) + 1).ID_CFC, Me.txt_descripcion.Text.ToUpper, Me.Dr_tipo_cfc.SelectedValue, Desde, Hasta)
                    msj.Mensaje(Me.Page, Caption, "Registro Modificado", TipoDeMensaje._Informacion)
                    'MARCAGRILLA()
                Else
                    cd.modifica_detalle_clasificacion(Me.txt_cod.Text, sesion.coll_cfc.Item(Val(Me.txt_pos.Text) + 1).ID_CFC, Me.txt_descripcion.Text.ToUpper, Me.Dr_tipo_cfc.SelectedValue, Desde, Hasta)
                    cd.cabecera_clasificacion_modifica(Me.txt_cod.Text, Me.dr_est.SelectedValue, Me.txt_descripcion.Text, dr_apb.SelectedValue)
                    msj.Mensaje(Me.Page, Caption, "Registro Modificado", TipoDeMensaje._Informacion)
                    'MARCAGRILLA()
                End If




            Else

                '-------------------------------------------------------------------------------------------------------------------------
                'Inserta rango
                '-------------------------------------------------------------------------------------------------------------------------
                If Not SW = 1 And modifica = False Then
                    msj.Mensaje(Me.Page, "Atención", "Debe Grabar Cabecera antes de Guardar los Detalles", TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                cd.guarda_detalle_clasificacion(Me.txt_cod.Text, Me.txt_descripcion.Text.ToUpper, Me.Dr_tipo_cfc.SelectedValue, Desde, Hasta)
                msj.Mensaje(Me.Page, Caption, "Registro Insertado", TipoDeMensaje._Informacion)

            End If


            '-------------------------------------------------------------------------------------------------------------------------
            'Vuelve a cargar la grilla y limpia los textos para otro ingreso
            '-------------------------------------------------------------------------------------------------------------------------
            sesion.coll_cfc = New Collection
            sesion.coll_cfc = cd.DevuelveDetalleClasificacion(ccfnum)

            Me.gr_cfc.DataSource = sesion.coll_cfc
            Me.gr_cfc.DataBind()

            Dr_tipo_cfc.ClearSelection()
            Me.txt_hta.Text = ""
            Me.txt_dde.Text = ""

        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_cfc.Rows.Count - 1
            If (gr_cfc.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    gr_cfc.Rows(i).CssClass = "selectable"
                Else
                    gr_cfc.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_cfc.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_cfc.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (gr_cfc.Rows(i).Cells(1).Text = btn.ToolTip) Then

                For x = 1 To coll_cfc.Count
                    If (sesion.coll_cfc.Item(x).cfc_obs = btn.ToolTip) Then
                        txt_pos.Text = x
                    End If
                Next

                If IsNothing(sesion.coll_cfc) = False Then
                    Me.Dr_tipo_cfc.SelectedValue = sesion.coll_cfc.Item(Val(txt_pos.Text)).id_p_0069
                    Me.txt_dde.Text = sesion.coll_cfc.Item(CInt(txt_pos.Text)).cfc_dde
                    Me.txt_hta.Text = sesion.coll_cfc.Item(CInt(txt_pos.Text)).cfc_hta
                Else
                    For z = 0 To Dr_tipo_cfc.Items.Count - 1
                        If Me.Dr_tipo_cfc.Items(z).Text = Me.gr_cfc.Rows(Val(CInt(Me.txt_pos.Text))).Cells(1).Text Then
                            Me.Dr_tipo_cfc.SelectedValue = z
                        End If
                    Next

                    Me.txt_dde.Text = Me.gr_cfc.Rows(CInt(Me.txt_pos.Text)).Cells(2).Text
                    Me.txt_hta.Text = Me.gr_cfc.Rows(CInt(Me.txt_pos.Text)).Cells(3).Text

                End If

                If Dr_tipo_cfc.SelectedValue <> 2 Then

                    txt_dde.MaxLength = 6
                    txt_hta.MaxLength = 6

                    txt_dde_MaskedEditExtender.Enabled = False
                    txt_hta_MaskedEditExtender.Enabled = False

                Else

                    'txt_dde.MaxLength = 10
                    'txt_hta.MaxLength = 10

                    txt_dde_MaskedEditExtender.Enabled = True
                    txt_hta_MaskedEditExtender.Enabled = True

                End If

                sesion.SW = 1

            End If
        Next
    End Sub
End Class
