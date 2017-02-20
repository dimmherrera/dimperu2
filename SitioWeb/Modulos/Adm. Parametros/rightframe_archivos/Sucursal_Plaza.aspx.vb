'Imports FacWebCiti.ClsParametros
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class SucursalPlaza
    Inherits System.Web.UI.Page


#Region "Declaracion de variables"

    Dim aux As Integer
    Dim i As Integer
    Dim CodInt As String
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim caption As String = "Banco Sucursal"
    Dim sesion As New ClsSession.ClsSession
    Dim Msj As New ClsMensaje


#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Mantencion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                NroPaginacion_Sucursal = 0
                NroPaginacion_Plaza = 0
                'CodInt = ""

                Response.Cache.SetNoStore()
                DeshabilitaHab()
                Txt_CodSuc.Enabled = False
                DeshabilitaSuc()


                Txt_Dias_Reten.ReadOnly = True
                Dp_Plaza_Suc.Enabled = False

            End If

            'IB_Zona.Attributes.Add("onClick", "Zona('Zonas.aspx', 600, 500, 0, 0);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Sucursal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            If RB_Sucursal.Checked = True Then

                GR_Sucursales.Enabled = True

                CargaDrop()

                If Txt_CodSuc.Text <> "" Then
                    HabilitaSuc()
                    BloqueaPlaza()

                End If

                sesion.NroPaginacion_Sucursal = 0

                GrillaSucursal()

                'Me.RB_Plaza.CssClass = ""
                Txt_CodInt.CssClass = "clsMandatorio"
                Txt_CodInt.ReadOnly = False

                Txt_CodSuc_Oculto.Enabled = True
                Dp_Plaza_Suc.CssClass = "clsDisabled"
                Txt_Dias_Reten.CssClass = "clsDisabled"
                'GR_Sucursales.Enabled = True

                Dp_Plaza_Suc.ClearSelection()
                Txt_Dias_Reten.Text = ""

                If Gr_Plaza.Rows.Count > 0 Then
                    For x = 0 To Gr_Plaza.Rows.Count - 1
                        Gr_Plaza.Rows(x).Enabled = False
                    Next
                End If

                For x = 0 To GR_Sucursales.Rows.Count - 1
                    GR_Sucursales.Rows(x).Enabled = True
                Next

                IB_Limpiar.Enabled = True
                IB_Nuevo.Enabled = True
                'IB_Zona.Enabled = True

            End If
        Catch ex As Exception
            'Msj(caption, ex.Message)
        End Try

    End Sub

    Protected Sub RB_Plaza_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            RB_Sucursal.Checked = False
            'Me.RB_Plaza.CssClass = "Cabecera"
            DeshabilitaSuc()
            IB_Nuevo.Enabled = True
            Gr_Plaza.Enabled = True
            Gr_Plaza.Enabled = True
            IB_Guardar.Enabled = True
            If Gr_Plaza.Rows.Count > 0 Then
                For x = 0 To Gr_Plaza.Rows.Count - 1
                    Gr_Plaza.Rows(x).Enabled = True
                Next
            End If
            For x = 0 To GR_Sucursales.Rows.Count - 1
                GR_Sucursales.Rows(x).Enabled = False
            Next
            IB_Guardar.Enabled = False
            IB_Zona.Enabled = False
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GR_Sucursales_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GR_Sucursales.RowDataBound 'SH-02-05-2012 Se reemplaza por boton en grilla sucursales

        'If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
        '    e.Row.Attributes.Add("onClick", "SeleccionaGrSucursal(ctl00_ContentPlaceHolder1_GR_Sucursales, 'clicktable', 'formatable', 'selectable')")
        'End If
    End Sub

    Protected Sub Gr_Plaza_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Plaza.RowDataBound  'SH-02-05-2012 Se reemplaza por boton en grilla plaza
        'If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
        '    e.Row.Attributes.Add("onClick", "SeleccionaGrPlaza(ctl00_ContentPlaceHolder1_Gr_Plaza, 'clicktable', 'formatable', 'selectable')")
        'End If
    End Sub

    Protected Sub LinkB_GrSuc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_GrSuc.Click 'SH-02-05-2012 Se comenta e inserta en evento de boton en grilla sucursal
        Try
            ' If HF_Po.Value >= 0 And HF_CodSuc.Value > 0 Then
            RB_Plaza.Enabled = True
            HabilitaSuc()
            'GR_Sucursales.Rows(HF_Po.Value).CssClass = "Clicktable"
            'For x = 0 To GR_Sucursales.Rows.Count - 1
            '    GR_Sucursales.Rows(x).CssClass = "formatable"
            '    If HF_Po.Value >= 0 And HF_CodSuc.Value > 0 Then
            '        GR_Sucursales.Rows(HF_Po.Value - 1).CssClass = "clicktable"
            '    End If
            'Next

            IB_Guardar.Enabled = True

            IB_Zona.Enabled = True

            Dim su As New suc_cls
            su = CG.SucursalDevuelve(HF_CodSuc.Value)
            Txt_CodSuc.Text = su.id_suc
            Txt_Descripcion.Text = su.suc_nom
            Txt_Descripcion_corta.Text = su.suc_des_cra
            Txt_telefono.Text = su.suc_tel
            Txt_Direccion.Text = su.suc_dir
            Txt_fax.Text = su.suc_fax
            If su.suc_cod_reg <> Nothing Then
                Dp_Cod_Region.SelectedValue = su.suc_cod_reg.Trim()
            Else
                Dp_Cod_Region.SelectedValue = 0
            End If

            '            Dp_PlazaBanco.SelectedValue = su.id_PL_000047



            CG.PlazaDevuelveXSucursal(HF_CodSuc.Value, True, Gr_Plaza, 4)
            'For x = 0 To Gr_Plaza.Rows.Count - 1
            '    Gr_Plaza.Rows(x).Enabled = False
            'Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LinkB_CodPla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_CodPla.Click 'SH-02-05-2012 Se comenta e inserta en evento de boton en grilla plaza
        'Try

        '    For x = 0 To Gr_Plaza.Rows.Count - 1
        '        Gr_Plaza.Rows(x).CssClass = "formatable"
        '        If HF_PoPL.Value >= 0 And HF_IdPL.Value > 0 Then
        '            Gr_Plaza.Rows(HF_PoPL.Value - 1).CssClass = "clicktable"
        '        End If
        '    Next

        '    Dim p As New pds_cls
        '    p = CG.PlazaDevuelveXId(HF_IdPL.Value)
        '    Dp_Plaza_Suc.SelectedValue = p.id_PL_000047
        '    Txt_Dias_Reten.Text = p.pds_ret
        '    Txt_Dias_Reten.ReadOnly = False
        '    Txt_Dias_Reten.CssClass = "clsMandatorio"
        '    IB_Guardar.Enabled = True
        '    If HF_IdPL.Value > 0 Then
        '        'IB_Eliminar.Enabled = True
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            If RB_Sucursal.Checked Then

                If Txt_CodSuc.Text = "" Then
                    Dim s As New suc_cls
                    's.id_suc = Nothing
                    's.id_suc = Txt_CodSuc.Text
                    s.suc_cod_ftg = Txt_CodInt.Text
                    s.suc_nom = UCase(Txt_Descripcion.Text)
                    s.suc_dir = UCase(Txt_Direccion.Text)
                    s.suc_fax = Txt_fax.Text
                    s.suc_tel = Txt_telefono.Text
                    s.suc_cod_reg = Dp_Cod_Region.SelectedValue
                    s.suc_des_cra = UCase(Txt_Descripcion_corta.Text)
                    's.id_PL_000047 = Dp_PlazaBanco.SelectedValue
                    AG.SucursalInserta(s)
                    Msj.Mensaje(Me.Page, caption, "Sucursal Insertada", TipoDeMensaje._Informacion)
                Else
                    AG.SucursalModifica(Txt_CodSuc.Text, Txt_CodInt.Text, UCase(Txt_Descripcion.Text), UCase(Txt_Descripcion_corta.Text), Dp_PlazaBanco.SelectedValue _
                                        , Dp_Cod_Region.SelectedValue, UCase(Txt_Direccion.Text), Txt_telefono.Text, Txt_fax.Text)
                    Msj.Mensaje(Me.Page, caption, "Sucursal Modificada", TipoDeMensaje._Informacion)
                End If
                Limpia()
                DeshabilitaSuc()
                GrillaSucursal()

            End If

            If RB_Plaza.Checked Then
                If HF_IdPL.Value = "" Then 'Aqui dejere el cod de Plaza
                    Dim p As New pds_cls 'Plaza
                    p.id_pds = Nothing
                    p.id_suc = Txt_CodSuc.Text
                    p.pds_ret = Txt_Dias_Reten.Text
                    p.id_PL_000047 = Dp_Plaza_Suc.SelectedValue
                    AG.PlazaInserta(p)
                    CG.PlazaDevuelveXSucursal(Txt_CodSuc.Text, True, Gr_Plaza)
                    Msj.Mensaje(Me.Page, caption, "Datos Insertados", TipoDeMensaje._Informacion)
                Else
                    AG.PlazaModifica(HF_IdPL.Value, Txt_Dias_Reten.Text)
                    CG.PlazaDevuelveXSucursal(Txt_CodSuc.Text, True, Gr_Plaza)
                    Msj.Mensaje(Me.Page, caption, "Datos Modificados", TipoDeMensaje._Informacion)
                End If
                BloqueaPlaza()


            End If
            HF_IdPL.Value = ""
            Txt_CodSuc.Text = ""
            Txt_CodSuc.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Sub y Function"

    Sub BloqueaPlaza()
        Txt_Dias_Reten.ReadOnly = True
        Dp_Plaza_Suc.Enabled = False
        Txt_Dias_Reten.CssClass = "clsDisabled"
        Dp_Plaza_Suc.CssClass = "clsDisabled"
        Txt_Dias_Reten.Text = ""
        Dp_Plaza_Suc.ClearSelection()

    End Sub

    Sub DesbloqueaPlaza()
        Txt_Dias_Reten.ReadOnly = True
        Dp_Plaza_Suc.Enabled = False
        Txt_Dias_Reten.CssClass = "clsMandatorio"
        Dp_Plaza_Suc.CssClass = "clsMandatorio"

    End Sub

    Sub CargaDrop()
        'CG.RegionesDevuelve(True, Dp_Cod_Region)
        CG.ParametrosDevuelve(1, True, Dp_Cod_Region)
        CG.ParametrosAlfanumericoDevuelve(2, True, Dp_PlazaBanco)
        CG.ParametrosAlfanumericoDevuelve(2, True, Dp_Plaza_Suc)
        'CG.Ciudad_P_0084_DevuelveTodo(True, Dp_Ciudad)
    End Sub

    Sub Deshabilita()


        Txt_CodSuc.Enabled = False
        Txt_CodSuc_Oculto.Enabled = False
        Txt_Descripcion.Enabled = False
        Txt_Descripcion_corta.Enabled = False
        Dp_PlazaBanco.Enabled = False
        Dp_Cod_Region.Enabled = False
        Txt_Direccion.Enabled = False
        Txt_telefono.Enabled = False
        Txt_fax.Enabled = False

        ' Me.GR_Sucursales.Enabled = False

    End Sub

    Sub GrillaSucursal()
        CG.SucursalDevuelveTodo(GR_Sucursales, 4, Txt_CodInt.Text.Trim())
    End Sub

    Sub Limpia()

        Txt_CodSuc.Text = ""
        Txt_CodInt.Text = ""
        Txt_CodInt_Oculto.Text = ""
        Txt_CodSuc_Oculto.Text = ""
        Txt_Descripcion.Text = ""
        Txt_Descripcion_corta.Text = ""
        Txt_Direccion.Text = ""
        Txt_telefono.Text = ""
        Txt_fax.Text = ""
        Dp_Cod_Region.ClearSelection()
        Dp_Plaza_Suc.ClearSelection()
        Dp_PlazaBanco.ClearSelection()
        HF_Codigo.Value = ""
        HF_CodSuc.Value = ""
        HF_IdPL.Value = ""
        HF_Po.Value = ""
        HF_PoPL.Value = ""
    End Sub

    Sub DeshabilitaHab()

        Me.IB_Zona.Visible = True
        Me.IB_Nuevo.Visible = True
        Me.IB_Guardar.Visible = True
        Me.IB_Limpiar.Visible = True
        'Me.IB_Eliminar.Visible = True
        IB_Guardar.Enabled = False
        IB_Nuevo.Enabled = False


    End Sub

    Sub DeshabilitaSuc()
        Txt_CodSuc.CssClass = "clsDisabled"
        Txt_CodInt.CssClass = "clsDisabled"
        Txt_Descripcion.CssClass = "clsDisabled"
        Txt_Descripcion_corta.CssClass = "clsDisabled"
        Txt_Direccion.CssClass = "clsDisabled"
        Txt_fax.CssClass = "clsDisabled"
        Txt_telefono.CssClass = "clsDisabled"
        Dp_PlazaBanco.CssClass = "clsDisabled"
        Dp_Plaza_Suc.CssClass = "clsDisabled"
        Dp_Cod_Region.CssClass = "clsDisabled"

        Txt_CodSuc.ReadOnly = True
        Txt_CodInt.ReadOnly = True
        Txt_Descripcion.ReadOnly = True
        Txt_Descripcion_corta.ReadOnly = True
        Txt_Direccion.ReadOnly = True
        Txt_fax.ReadOnly = True
        Txt_telefono.ReadOnly = True
        Dp_PlazaBanco.Enabled = False
        Dp_Plaza_Suc.Enabled = False
        Dp_Cod_Region.Enabled = False




    End Sub

    Sub HabilitaSuc()

        Txt_CodSuc.CssClass = "clsMandatorio"
        Txt_CodInt.CssClass = "clsMandatorio"
        Txt_Descripcion.CssClass = "clsMandatorio"
        Txt_Descripcion_corta.CssClass = "clsMandatorio"
        Txt_Direccion.CssClass = "clsTxt"
        Txt_fax.CssClass = "clsTxt"
        Txt_telefono.CssClass = "clsTxt"
        Dp_PlazaBanco.CssClass = "clsMandatorio"
        Dp_Cod_Region.CssClass = "clsMandatorio"


        Txt_CodInt.ReadOnly = False
        Txt_Descripcion.ReadOnly = False
        Txt_Descripcion_corta.ReadOnly = False
        Txt_Direccion.ReadOnly = False
        Txt_fax.ReadOnly = False
        Txt_telefono.ReadOnly = False
        Dp_PlazaBanco.Enabled = True
        Dp_Cod_Region.Enabled = True

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020612, Usr, "PRESIONO NUEVO ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            If RB_Sucursal.Checked = True Then
                HabilitaSuc()
                Limpia()
                BloqueaPlaza()
                Txt_CodInt_Oculto.Text = ""


            End If
            If RB_Plaza.Checked = True Then

                Dp_Plaza_Suc.Enabled = True
                Dp_Plaza_Suc.CssClass = "clsMandatorio"
                Txt_Dias_Reten.CssClass = "clsMandatorio"

                Dp_Plaza_Suc.ClearSelection()
                Txt_Dias_Reten.ReadOnly = False
                Txt_Dias_Reten.Text = ""
                HF_IdPL.Value = ""
                IB_Nuevo.Enabled = True
                IB_Guardar.Enabled = True
                IB_Limpiar.Enabled = True
                Txt_Dias_Reten.Text = ""

            End If
            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = True
            IB_Limpiar.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)


        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20030612, Usr, "PRESIONO GUARDAR") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Try
            '*****************Inserta o modifica Sucursal***************************
            If RB_Sucursal.Checked Then
                If Me.Txt_Descripcion.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Descripción", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_Descripcion_corta.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Descripción corta", TipoDeMensaje._Exclamacion)
                End If

                If Me.Dp_PlazaBanco.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione plaza banco origen", TipoDeMensaje._Exclamacion)
                End If

                If Me.Dp_Cod_Region.SelectedIndex = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione región", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_CodInt.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese codigo interno", TipoDeMensaje._Exclamacion)
                    Txt_CodInt.Focus()
                    Exit Sub
                End If

                If Trim(Txt_CodInt_Oculto.Text) <> Trim(Txt_CodInt.Text) Then
                    If CG.SucursalValidaCodInt(Txt_CodInt.Text) Then
                        Msj.Mensaje(Me.Page, caption, "Codigo ingresado se encuentra asociado a otra sucursal", TipoDeMensaje._Exclamacion)
                        Txt_CodSuc.Focus()
                        Exit Sub
                    End If
                End If
                If Txt_CodSuc.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de insertar sucursal", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                Else
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar sucursal", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                End If
            End If

            '*******************************Inserta o modifica Plaza**************************************************
            If RB_Plaza.Checked Then

                If Dp_Plaza_Suc.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione Plaza sucursal", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


                If Txt_Dias_Reten.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese dias de retención", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


                If HF_IdPL.Value = "" Then
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de insertar plaza", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                Else
                    Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar plaza", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Limpia()
            DeshabilitaSuc()
            BloqueaPlaza()
            Me.RB_Sucursal.Checked = False

            Me.RB_Plaza.Checked = False
            Me.RB_Plaza.Enabled = False
            Limpia()

            IB_Limpiar.Enabled = False
            IB_Guardar.Enabled = False
            IB_Nuevo.Enabled = False
            IB_Zona.Enabled = False

            GR_Sucursales.Controls.Clear()
            Gr_Plaza.Controls.Clear()
            Txt_CodSuc.Enabled = False

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Zona_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Zona.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010612, Usr, "PRESIONO ZONAS") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try

            'sesion.Sucursal = HF_CodSuc.Value

            If Val(HF_CodSuc.Value) <> 0 Then
                RW.AbrePopup(Me, 2, "Zonas.aspx?Sucursal=" & HF_CodSuc.Value, "Zonas", 600, 400, 250, 250)

            Else
                Msj.Mensaje(Me.Page, caption, "Seleccione Sucursal", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

    Protected Sub IB_Prev_Suc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Suc.Click

        Try

            If NroPaginacion_Sucursal = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            NroPaginacion_Sucursal -= 4
            GrillaSucursal()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Suc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Suc.Click
        Try
            'NroPaginacion_Sucursal()
            If GR_Sucursales.Rows.Count < 4 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            NroPaginacion_Sucursal += 4
            GrillaSucursal()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Pla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Pla.Click
        Try
            If NroPaginacion_Plaza = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            NroPaginacion_Plaza -= 4
            LinkB_GrSuc_Click(Me, e)
            IB_Zona.Enabled = False
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Pla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Pla.Click
        Try
            If Gr_Plaza.Rows.Count < 4 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            NroPaginacion_Plaza += 4
            LinkB_GrSuc_Click(Me, e)
            IB_Zona.Enabled = False
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_CodSuc.Value = btn.ToolTip

        For i = 0 To GR_Sucursales.Rows.Count - 1

            Dim img As ImageButton = CType(GR_Sucursales.Rows(i).FindControl("Btn_ver"), ImageButton)

            If (btn.ToolTip = img.ToolTip) Then

                If (i Mod 2) = 0 Then
                    GR_Sucursales.Rows(i).CssClass = "selectable"
                Else
                    GR_Sucursales.Rows(i).CssClass = "selectableAlt"
                End If

                Try

                    RB_Plaza.Enabled = True
                    HabilitaSuc()

                    IB_Guardar.Enabled = True
                    IB_Zona.Enabled = True

                    Dim su As New suc_cls

                    su = CG.SucursalDevuelve(img.ToolTip)
                    Txt_CodSuc.Text = su.id_suc
                    Txt_CodInt.Text = su.suc_cod_ftg
                    Txt_CodInt_Oculto.Text = su.suc_cod_ftg
                    Txt_Descripcion.Text = su.suc_nom
                    Txt_Descripcion_corta.Text = su.suc_des_cra
                    Txt_telefono.Text = su.suc_tel
                    Txt_Direccion.Text = su.suc_dir
                    Txt_fax.Text = su.suc_fax

                    If su.suc_cod_reg <> Nothing Then
                        Dp_Cod_Region.SelectedValue = su.suc_cod_reg.Trim()
                    Else
                        Dp_Cod_Region.SelectedValue = 0
                    End If

                    'Dp_PlazaBanco.SelectedValue = su.id_PL_000047
                    sesion.NroPaginacion_Plaza = 0
                    CG.PlazaDevuelveXSucursal(img.ToolTip, True, Gr_Plaza, 4)


                Catch ex As Exception

                End Try

            Else
                If (i Mod 2) = 0 Then
                    GR_Sucursales.Rows(i).CssClass = "formatUltcell"
                Else
                    GR_Sucursales.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

    Protected Sub Btn_ver2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gr_Plaza.Rows.Count - 1

            If (btn.ToolTip = Gr_Plaza.Rows(i).Cells(1).Text) Then

                If (i Mod 2) = 0 Then
                    Gr_Plaza.Rows(i).CssClass = "selectable"
                Else
                    Gr_Plaza.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Plaza.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Plaza.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Plaza.Rows(i).Cells(1).Text = btn.ToolTip) Then

                Try
                    Dim p As New pds_cls
                    p = CG.PlazaDevuelveXId(Gr_Plaza.Rows(i).Cells(1).Text)
                    Dp_Plaza_Suc.SelectedValue = p.id_PL_000047
                    Txt_Dias_Reten.Text = p.pds_ret
                    Txt_Dias_Reten.ReadOnly = False
                    Txt_Dias_Reten.CssClass = "clsMandatorio"
                    IB_Guardar.Enabled = True
                    If HF_IdPL.Value > 0 Then
                        'IB_Eliminar.Enabled = True
                    End If
                Catch ex As Exception

                End Try

            End If
        Next
    End Sub

    Protected Sub Txt_CodSuc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_CodInt.TextChanged
        GrillaSucursal()
    End Sub

End Class
