Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_MCondiciones
    Inherits System.Web.UI.UserControl

    Dim Caption As String = "Condiciones"
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Private Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim CD As New ClaseControlDual

    Protected Sub IB_Ok_Con_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Ok_Con.Click
        Me.Txt_Msj_Con.Text = ""
    End Sub

    Protected Sub IB_Agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Agregar.Click

        Try

            If Not agt.ValidaAccesso(20, 20030103, Usr, "PRESIONO AGREGAR CONDICION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Validaciones para agregar una condicion
            If NroOperacion = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una Operacion ", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Fecha_Cumplimiento.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar fecha de cumplimiento de la condición", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_EstadoCondicion.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar Estado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Descripcion.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar descripción de cumplimiento de la condición", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CDate(Txt_Fecha_Cumplimiento.Text) <= Date.Now.ToShortDateString Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar fecha de cumplimiento mayor a hoy", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '**************************************************************************************************************

            Dim CDN As New cdn_cls

            CDN.id_ope = NroOperacion
            CDN.cdn_fec_com = CDate(Txt_Fecha_Cumplimiento.Text)
            CDN.cdn_des = Txt_Descripcion.Text.Trim.ToUpper
            CDN.id_p_0111 = DP_EstadoCondicion.SelectedValue

            If DP_EstadoCondicion.SelectedValue = 2 Then
                CDN.id_eje_apb = CodEje
                CDN.cdn_fec_apb = Date.Today
            End If

            If HF_NroCon.Value = "" Then

                CDN.cdn_fec_ing = Date.Today
                CDN.id_eje_ing = CodEje

                If CD.CondicionInserta(CDN) Then
                    Msj.Mensaje(Me.Page, Caption, "Se agrego la condición a la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                    CargaCondiciones()
                    HabilitaCondicion(True)
                Else
                    Msj.Mensaje(Me.Page, Caption, "No se agrego la condición a la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                End If

            Else

                CDN.id_cdn = HF_NroCon.Value

                If CD.CondicionActualiza(CDN) Then
                    Msj.Mensaje(Me.Page, Caption, "Se Actualizo la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                    CargaCondiciones()
                    HabilitaCondicion(True)
                Else
                    Msj.Mensaje(Me.Page, Caption, "No se Actualizo la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                End If

            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Exclamacion)
        End Try

    End Sub

    Protected Sub IB_Quitar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Quitar.Click

        Try

            If Not agt.ValidaAccesso(20, 20040103, Usr, "PRESIONO QUITAR CONDICION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If HF_NroCon.Value = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una condición para quitar", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CD.CondicionElimina(HF_NroCon.Value) Then
                Msj.Mensaje(Me.Page, Caption, "Se Elimino la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                CargaCondiciones()
                HabilitaCondicion(True)
            Else
                Msj.Mensaje(Me.Page, Caption, "No se Elimino la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub HabilitaCondicion(ByVal Estado As Boolean)

        Select Case Estado

            Case True

                Txt_Fecha_Cumplimiento.ReadOnly = False
                Txt_Descripcion.ReadOnly = False
                DP_EstadoCondicion.Enabled = True

                Txt_Fecha_Cumplimiento.CssClass = "clsMandatorio"
                Txt_Descripcion.CssClass = "clsMandatorio"
                DP_EstadoCondicion.CssClass = "clsMandatorio"

            Case False

                Txt_Fecha_Cumplimiento.ReadOnly = True
                Txt_Descripcion.ReadOnly = True
                DP_EstadoCondicion.Enabled = False

                Txt_Fecha_Cumplimiento.CssClass = "clsDisabled"
                Txt_Descripcion.CssClass = "clsDisabled"
                DP_EstadoCondicion.CssClass = "clsDisabled"

        End Select

        Txt_Fecha_Cumplimiento.Text = ""
        Txt_Descripcion.Text = ""
        HF_NroCon.Value = ""


    End Sub

    Private Sub CargaCondiciones()

        CD.CondicionesDevuelveTodosPorOperacion(NroOperacion, True, GV_Condiciones)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1
        If Not IsPostBack Then

            'Estado de Condicion
            CG.ParametrosDevuelve(TablaParametro.EstadoCondicion, True, DP_EstadoCondicion)
            HF_Ope.Value = 0
            NroPaginacion_Condicion = 0

          
        Else

            If NroOperacion <> HF_Ope.Value Then
                HF_Ope.Value = NroOperacion
                CargaCondiciones()
                HabilitaCondicion(True)
            End If

        End If

    End Sub

    Protected Sub GV_Condiciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Condiciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_GV_Condiciones, 'selectable');")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_GV_Condiciones, 'formatable');")
            'e.Row.Attributes.Add("onClick", "ClickCondicion(ctl00_ContentPlaceHolder1_TabContainer1_TabPanelCon_MCondiciones1_GV_Condiciones, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img_ver As ImageButton = CType(sender, ImageButton)
        DP_EstadoCondicion.ClearSelection()

        For i = 0 To GV_Condiciones.Rows.Count - 1

            If img_ver.ToolTip = GV_Condiciones.Rows(i).Cells(0).Text Then

                HabilitaCondicion(True)

                HF_NroCon.Value = GV_Condiciones.Rows(i).Cells(0).Text
                Txt_Descripcion.Text = GV_Condiciones.Rows(i).Cells(1).Text
                Txt_Fecha_Cumplimiento.Text = GV_Condiciones.Rows(i).Cells(2).Text
                DP_EstadoCondicion.Items.FindByText(GV_Condiciones.Rows(i).Cells(5).Text).Selected = True

                If (i Mod 2) = 0 Then
                    GV_Condiciones.Rows(i).CssClass = "selectable"
                Else
                    GV_Condiciones.Rows(i).CssClass = "selectableAlt"
                End If

            Else
                If (i Mod 2) = 0 Then
                    GV_Condiciones.Rows(i).CssClass = "formatUltcell"
                Else
                    GV_Condiciones.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion_Condicion < 12 Then
            Msj.Mensaje(Page, Caption, "Ha llegado al comienzo de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If NroPaginacion_Condicion >= 12 Then
            NroPaginacion_Condicion -= 12
            CargaCondiciones()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        If GV_Condiciones.Rows.Count < 12 Then
            Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If


        If GV_Condiciones.Rows.Count = 12 Then
            NroPaginacion_Condicion += 12
            CargaCondiciones()

        End If
    End Sub

End Class
