Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.Errores
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class CiudadComuna

    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"
    Dim CG As New ConsultasGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim Caption As String = "Ciudad/Comuna"

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                Response.Expires = -1
                NroPaginacion = 0
                NroPaginacion_Plaza = 0

                CargaDropSucursales()
                CargaGrillaCiudad()

                Gr_Comuna.DataSource = Nothing
                Gr_Comuna.DataBind()

                Txt_Cod_Comuna.Text = ""
                Txt_Descripcion.Text = ""

                'CargaDropZona()

                'Limpiar()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Dp_Sucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Sucursal.SelectedIndexChanged

        Try

            NroPaginacion = 0
            NroPaginacion_Plaza = 0

            CargaGrillaCiudad()

            Me.Txt_Cod_Suc.Text = Me.Dp_Sucursal.SelectedValue

            Gr_Comuna.DataSource = Nothing
            Gr_Comuna.DataBind()
            Txt_Cod_Comuna.Text = ""
            Txt_Descripcion.Text = ""
            CargaDropZona()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_LimpiarCom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_LimpiarCom.Click
        Limpiar()

    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            If Txt_Cod_Comuna.Text = "" Then

                Dim Com As New cmn_cls
                Com.id_cmn = Nothing
                'Com.id_ciu = txt_CodCiudad.Text
                Com.id_ciu = HF_Ciudad.Value
                Com.id_zon = Dp_Zona_Rec.SelectedValue
                'Com.id_bco = Dp_Com_Banco.SelectedValue
                Com.cmn_des = UCase(Txt_Descripcion.Text)

                If AG.ComunaInserta(Com) Then
                    msj.Mensaje(Me, Caption, "Datos Ingresados", TipoDeMensaje._Informacion)
                    CargaGrillaComuna()
                    Dp_Zona_Rec.CssClass = "clsDisabled"
                    Dp_Zona_Rec.ClearSelection()
                    Dp_Zona_Rec.Enabled = False
                    Txt_Cod_Comuna.ReadOnly = True
                    Txt_Descripcion.ReadOnly = True
                    Txt_Cod_Comuna.Text = ""
                    Txt_Cod_Suc.Text = ""
                    Txt_Descripcion.Text = ""
                    Txt_Descripcion.CssClass = "clsDisabled"
                    Txt_Cod_Comuna.ReadOnly = True
                End If
            Else
                If AG.ComunaModifica(Txt_Cod_Comuna.Text, Dp_Zona_Rec.SelectedValue, UCase(Txt_Descripcion.Text)) Then
                    Txt_Cod_Comuna.Enabled = False
                    msj.Mensaje(Me, Caption, "Datos modificados", TipoDeMensaje._Informacion)
                    CargaGrillaComuna()
                    Dp_Zona_Rec.CssClass = "clsDisabled"
                    Dp_Zona_Rec.ClearSelection()
                    Dp_Zona_Rec.Enabled = False
                    Txt_Cod_Comuna.ReadOnly = True
                    Txt_Descripcion.ReadOnly = True
                    Txt_Cod_Comuna.Text = ""
                    Txt_Cod_Suc.Text = ""
                    Txt_Descripcion.Text = ""
                    Txt_Descripcion.CssClass = "clsDisabled"
                    Txt_Cod_Comuna.ReadOnly = True
                Else
                    msj.Mensaje(Me, Caption, "Error", TipoDeMensaje._Informacion)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Sub Y Function"

    Sub Limpiar()
        Txt_Cod_Comuna.Text = ""
        Txt_Descripcion.Text = ""
        Dp_Zona_Rec.ClearSelection()
        Dp_Zona_Rec.Enabled = False
        Dp_Zona_Rec.CssClass = "clsDisabled"
        Txt_Cod_Comuna.Text = ""
        Txt_Cod_Suc.Text = ""
        Txt_Descripcion.Text = ""
        Txt_Descripcion.CssClass = "clsDisabled"
        Txt_Cod_Comuna.ReadOnly = True
        Txt_Descripcion.ReadOnly = True
        Gr_Ciudad.DataSource = Nothing
        Gr_Ciudad.DataBind()
        Gr_Comuna.DataSource = Nothing
        Gr_Comuna.DataBind()
        IB_GuardarCom.Enabled = False
        IB_GuardarCom.Enabled = False
        Ib_Nuevo.Enabled = False
        Dp_Sucursal.ClearSelection()
    End Sub

    Sub CargaGrillaCiudad()

        Dim coll As New Collection

        Gr_Ciudad.DataSource = CG.CiudadDevuelvePorSucursal(CInt(Dp_Sucursal.SelectedValue), 4)
        Gr_Ciudad.DataBind()

    End Sub

    Sub CargaGrillaComuna()
        CG.ComunaDevuelvePorCiudad(HF_Ciudad.Value, True, Gr_Comuna)
    End Sub

    Sub CargaDropSucursales()
        Dim cong As New ConsultasGenerales
        cong.SucursalesDevuelve(codeje, True, DP_Sucursal)
    End Sub

    Sub CargaDropZona()
        CG.ZonaDevuelvePorSucursal(Dp_Sucursal.SelectedValue, True, Dp_Zona_Rec)
    End Sub

    Sub LimpiarCiudad()
        Dp_Zona_Rec.ClearSelection()
        Dp_Sucursal.ClearSelection()
        Txt_Cod_Comuna.Text = ""
        Txt_Descripcion.Text = ""
        Gr_Ciudad.DataSource = Nothing
        Gr_Ciudad.DataBind()
        Gr_Comuna.DataSource = Nothing
        Gr_Comuna.DataBind()

    End Sub
#End Region

#Region "Botonera"

    Protected Sub Ib_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Nuevo.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010712, Usr, "PRESIONO NUEVO ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            Txt_Cod_Comuna.Text = ""
            Txt_Descripcion.Text = ""

            Txt_Descripcion.ReadOnly = False
            Txt_Descripcion.CssClass = "clsMandatorio"
            Dp_Zona_Rec.Enabled = True
            Dp_Zona_Rec.CssClass = "clsMandatorio"
            Txt_Descripcion.Focus()

            IB_GuardarCom.Enabled = True
            'IB_EliminarCom.Enabled = True

        Catch ex As Exception
            msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub IB_GuardarCom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_GuardarCom.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020712, Usr, "PRESIONO GUARDAR ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            If Txt_Descripcion.Text = "" Then
                msj.Mensaje(Me, Caption, "Ingrese Descripcion", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Dp_Zona_Rec.SelectedValue = 0 Then
                msj.Mensaje(Me, Caption, "Seleccione Zona Recaudación", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Cod_Comuna.Text = "" Then
                msj.Mensaje(Me, Caption, "¿Esta seguro de guardar este registro?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            Else
                msj.Mensaje(Me, Caption, "¿Esta seguro de Modificar este registro?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            End If
        Catch ex As Exception
            msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub


    'Protected Sub IB_EliminarCom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_EliminarCom.Click
    '    Try

    '        If Txt_Cod_Comuna.Text = "" Then
    '            Exit Sub
    '        End If
    '        If AG.ComunaElimina(Txt_Cod_Comuna.Text) Then
    '            CargaGrillaComuna()
    '            Txt_Cod_Comuna.Text = ""
    '            Txt_Cod_Suc.CssClass = "clsDisabled"
    '            Txt_Descripcion.Text = ""
    '            Txt_Descripcion.CssClass = "clsDisabled"
    '            Txt_Descripcion.ReadOnly = True
    '            Dp_Zona_Rec.CssClass = "clsDisabled"
    '            Dp_Zona_Rec.ClearSelection()
    '            Dp_Zona_Rec.Enabled = False
    '            msj.mensaje(me,Caption, "Eliminado", TipoDeMensaje._Informacion)
    '        End If
    '    Catch ex As Exception
    '        msj.mensaje(me,Caption, ex.Message, TipoDeMensaje._Error)
    '    End Try
    'End Sub

#End Region

    Protected Sub IB_Prev_Ciu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Ciu.Click
        Try
            If NroPaginacion = 0 Then
                msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            NroPaginacion -= 4
            CargaGrillaCiudad()

        Catch ex As Exception
            msj.Mensaje(Me, Caption, "Error", TipoDeMensaje._Informacion)
        End Try
    End Sub

    Protected Sub IB_Next_Ciu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Ciu.Click
        Try
            If Gr_Ciudad.Rows.Count < 4 Then
                msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            NroPaginacion += 4
            CargaGrillaCiudad()

        Catch ex As Exception
            msj.Mensaje(Me, Caption, "Error", TipoDeMensaje._Informacion)
        End Try
    End Sub

  
    Protected Sub IB_Prev_Com_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Com.Click
        Try
            If NroPaginacion_Plaza = 0 Then
                msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            NroPaginacion_Plaza -= 4
            CargaGrillaComuna()
        Catch ex As Exception
            msj.Mensaje(Me, Caption, "Error", TipoDeMensaje._Informacion)
        End Try
    End Sub

    Protected Sub IB_Next_Com_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Com.Click
        Try
            If Gr_Comuna.Rows.Count < 4 Then
                msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            NroPaginacion_Plaza += 4
            CargaGrillaComuna()
        Catch ex As Exception
            msj.Mensaje(Me, Caption, "Error", TipoDeMensaje._Informacion)
        End Try
    End Sub

    Protected Sub Btn_ver_ciu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_Ciudad.Value = btn.ToolTip

        For i = 0 To Gr_Ciudad.Rows.Count - 1

            If (btn.ToolTip = Gr_Ciudad.Rows(i).Cells(1).Text) Then

                If (i Mod 2) = 0 Then
                    Gr_Ciudad.Rows(i).CssClass = "selectable"
                Else
                    Gr_Ciudad.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Ciudad.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Ciudad.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Ciudad.Rows(i).Cells(1).Text = btn.ToolTip) Then

                CargaGrillaComuna()
                Ib_Nuevo.Enabled = True
                'Limpiar()
                IB_GuardarCom.Enabled = False
                Txt_Cod_Comuna.Text = ""
                Txt_Descripcion.Text = ""
                Dp_Zona_Rec.ClearSelection()
                Dp_Zona_Rec.Enabled = False
                Dp_Zona_Rec.CssClass = "clsDisabled"

                Txt_Descripcion.CssClass = "clsDisabled"
                Txt_Descripcion.ReadOnly = True

            End If

        Next

    End Sub

    Protected Sub Btn_ver_Cmn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_IdComuna.Value = btn.ToolTip

        For i = 0 To Gr_Comuna.Rows.Count - 1

            If (btn.ToolTip = Gr_Ciudad.Rows(i).Cells(1).Text) Then

                If (i Mod 2) = 0 Then
                    Gr_Comuna.Rows(i).CssClass = "selectable"
                Else
                    Gr_Comuna.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Comuna.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Comuna.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (btn.ToolTip = Gr_Comuna.Rows(i).Cells(1).Text) Then

                Dim CMN As New cmn_cls
                CMN = CG.ComunaDevuelvePorCodigoComuna(HF_IdComuna.Value)

                Txt_Descripcion.Text = CMN.cmn_des
                Txt_Cod_Comuna.Text = CMN.id_cmn

                If Not IsNothing(CMN.id_zon) Then
                    Dp_Zona_Rec.SelectedValue = CMN.id_zon
                End If

                Txt_Descripcion.ReadOnly = False
                Dp_Zona_Rec.Enabled = True

                Txt_Descripcion.CssClass = "clsMandatorio"
                Dp_Zona_Rec.CssClass = "clsMandatorio"
                IB_GuardarCom.Enabled = True

            End If

        Next

    End Sub

End Class
