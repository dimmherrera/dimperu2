
Imports ClsSession.SesionOperaciones
Imports System.Transactions
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ClsGastos
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim caption As String = "Gastos"
    Dim sesionope As New ClsSession.SesionOperaciones
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
    Dim RG As New FuncionesGenerales.FComunes


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

            If Not Me.IsPostBack Then
                Response.Expires = -1
                NroPaginacion = 0
                sesionope.coll_Gto = New Collection
                BloqueaControles()
                Me.Rd_activo.Checked = False
                Me.Rd_Inactivo.Checked = False
                Cargadrop()
                Txt_Mto_Gasto.Attributes.Add("Style", "TEXT-ALIGN: right")
            End If



        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Gr_Gastos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Gastos.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
        '    e.Row.Attributes.Add("onClick", "SeleccionaFilaGrGastos(ctl00_ContentPlaceHolder1_Gr_Gastos, 'clicktable', 'formatable', 'selectable')")
        'End If
    End Sub

    Protected Sub Link_Gto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Gto.Click
        Try
            Rd_activo.Enabled = True
            Rd_Inactivo.Enabled = True
            Rd_activo.Checked = False
            Rd_Inactivo.Checked = False

            Txt_VAL_CON.ReadOnly = False
            Txt_VAL_CON.CssClass = "clsMandatorio"


            For i = 0 To Gr_Gastos.Rows.Count - 1
                Gr_Gastos.Rows(i).CssClass = "formatable"
                If HF_Codgto.Value > 0 And HF_PosGto.Value >= 0 Then
                    Gr_Gastos.Rows(HF_PosGto.Value - 1).CssClass = "clicktable"
                End If
            Next

            For i = 1 To sesionope.coll_Gto.Count
                If sesionope.coll_Gto.Item(i).id_gto = HF_Codgto.Value Then
                    Dp_Tipo_Gasto.SelectedValue = sesionope.coll_Gto.Item(i).id_P_0036
                    Txt_Descripcion.Text = sesionope.coll_Gto.Item(i).gto_des
                    Txt_Mto_Gasto.Text = RG.FormatoMiles(sesionope.coll_Gto(i).gto_mto)
                    Txt_VAL_CON.Text = sesionope.coll_Gto.Item(i).val_con

                    If sesionope.coll_Gto.Item(i).Estado = "A" Then
                        Rd_activo.Checked = True
                    ElseIf sesionope.coll_Gto.Item(i).Estado = "I" Then
                        Rd_Inactivo.Checked = True
                    End If

                End If
            Next

            If Gr_Gastos.Rows.Count > 0 Then
                IB_Guardar.Enabled = True
                IB_Eliminar.Enabled = True
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            Dim Estado As String
            If Me.Rd_activo.Checked Then
                Estado = "A"
            ElseIf Me.Rd_Inactivo.Checked Then
                Estado = "I"
            End If

            Dim IVA As String
            If Me.CB_IVA.Checked Then
                iva = "S"
            Else
                IVA = "N"
            End If

            If HF_Codgto.Value = "" Then
                Dim g As New gto_cls
                g.gto_des = UCase(Txt_Descripcion.Text)
                g.gto_mto = Txt_Mto_Gasto.Text
                g.id_P_0036 = Dp_Tipo_Gasto.SelectedValue
                g.id_suc = Dp_Sucursal.SelectedValue
                g.gto_est = Estado
                g.val_con = Txt_VAL_CON.Text.ToUpper().Trim()
                g.gto_iva = IVA

                AG.GtoInserta(g)

                Msj.Mensaje(Me.Page, caption, "Datos Guardados", TipoDeMensaje._Informacion)
                CargaGrilla()
                Txt_Descripcion.Text = ""
                Txt_Descripcion.ReadOnly = True
                Txt_Mto_Gasto.ReadOnly = True
                Txt_VAL_CON.ReadOnly = True

                Txt_Mto_Gasto.Text = ""
                Txt_VAL_CON.Text = ""

                Txt_Descripcion.CssClass = "clsDisabled"
                Txt_Mto_Gasto.CssClass = "clsDisabled"
                Txt_VAL_CON.CssClass = "clsDisabled"

                Dp_Tipo_Gasto.CssClass = "clsDisabled"
                Dp_Tipo_Gasto.ClearSelection()
                Dp_Tipo_Gasto.Enabled = False
                Rd_activo.Enabled = False
                Rd_Inactivo.Enabled = False
                CB_IVA.Enabled = False
                CB_IVA.Checked = False


            Else
                AG.GtoModifica(HF_Codgto.Value, Estado, Txt_VAL_CON.Text.ToUpper().Trim(), IVA.ToUpper().Trim())
                Msj.Mensaje(Me.Page, caption, "Datos Modificados", TipoDeMensaje._Informacion)
                CargaGrilla()
                Txt_Descripcion.Text = ""
                Txt_Descripcion.ReadOnly = True
                Txt_Mto_Gasto.ReadOnly = True
                Txt_VAL_CON.ReadOnly = True

                Txt_Mto_Gasto.Text = ""
                Txt_VAL_CON.Text = ""
                Txt_Descripcion.CssClass = "clsDisabled"
                Txt_Mto_Gasto.CssClass = "clsDisabled"
                Dp_Tipo_Gasto.CssClass = "clsDisabled"
                Txt_VAL_CON.CssClass = "clsDisabled"
                Dp_Tipo_Gasto.ClearSelection()
                Dp_Tipo_Gasto.Enabled = False
                'Rd_activo.Enabled = True
                'Rd_Inactivo.Enabled = True
                Rd_activo.Enabled = False
                Rd_Inactivo.Enabled = False
                CB_IVA.Enabled = False
                CB_IVA.Checked = False

            End If
            HF_Codgto.Value = ""
            IB_Buscar.Enabled = True
            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = False
            IB_Eliminar.Enabled = False


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Eliminar.Click
        Try
            If AG.GtoElimina(HF_Codgto.Value) = True Then
                Msj.Mensaje(Me.Page, caption, "Registro Eliminado", TipoDeMensaje._Informacion)
                Me.Txt_Mto_Gasto.Text = ""
                Me.Txt_Descripcion.Text = ""
                Txt_Mto_Gasto.ReadOnly = True
                Txt_Descripcion.ReadOnly = True
                Dp_Tipo_Gasto.Enabled = False
                Dp_Tipo_Gasto.ClearSelection()
                IB_Guardar.Enabled = False
                Gr_Gastos.DataSource = New Collection
                Gr_Gastos.DataBind()
                Rd_activo.Enabled = False
                Rd_Inactivo.Enabled = False

                Rd_activo.Checked = True
                Rd_Inactivo.Checked = False

                CargaGrilla()

                'Dim Llena = CG.GastosDevuelvePorSucursal(Dp_Sucursal.SelectedValue)
                'For Each L In Llena
                '    sesionope.coll_Gto.Add(L)
                'Next
                'If sesionope.coll_Gto.Count > 0 Then
                '    Gr_Gastos.DataSource = sesionope.coll_Gto
                '    Gr_Gastos.DataBind()
                '    For i = 1 To sesionope.coll_Gto.Count
                '        Gr_Gastos.Rows(i - 1).Cells(3).Text = Format(CLng(sesionope.coll_Gto.Item(i).gto_mto), FMT.FCMSD)
                '    Next
                'End If

            Else
                Msj.Mensaje(Me.Page, caption, "No se puede eliminar el gastos, esta siendo utilizado en alguna operación", TipoDeMensaje._Informacion)
            End If

            HF_Codgto.Value = ""

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        'NroPaginacion = 12
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            NroPaginacion -= 12
            CargaGrilla()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If Gr_Gastos.Rows.Count < 12 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            NroPaginacion += 12
            CargaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Function y Sub"

    Public Sub CargaGrilla()

        sesionope.coll_Gto = New Collection

        Dim Llena = CG.GastosDevuelvePorSucursal(Dp_Sucursal.SelectedValue, 12)

        For Each L In Llena
            sesionope.coll_Gto.Add(L)
        Next

        If sesionope.coll_Gto.Count > 0 Then

            Gr_Gastos.DataSource = sesionope.coll_Gto
            Gr_Gastos.DataBind()

            For i = 1 To sesionope.coll_Gto.Count

                Gr_Gastos.Rows(i - 1).Cells(3).Text = Format(CLng(sesionope.coll_Gto.Item(i).gto_mto), FMT.FCMSD)

                If sesionope.coll_Gto.Item(i).Estado = "A" Then
                    Gr_Gastos.Rows(i - 1).Cells(5).Text = "ACTIVO"
                Else
                    Gr_Gastos.Rows(i - 1).Cells(5).Text = "INACTIVO"
                End If

            Next

        Else

            Msj.Mensaje(Me.Page, caption, "No se encontraron datos", TipoDeMensaje._Informacion)
            Exit Sub

        End If

    End Sub

    Sub Limpiar()

        'LIMPIA Y DESHABILITA CHECK
        Me.Rd_activo.Checked = False
        Me.Rd_activo.Enabled = False
        Me.Rd_Inactivo.Checked = False
        Me.Rd_Inactivo.Enabled = False

        Me.CB_IVA.Checked = False
        Me.CB_IVA.Enabled = False

        'LIMPIA DROP
        Dp_Sucursal.ClearSelection()
        Dp_Tipo_Gasto.ClearSelection()

        Me.Dp_Tipo_Gasto.Enabled = False
        'LIMPIA CAJAS DE TEXTOS

        Me.Txt_Mto_Gasto.Text = ""
        Me.Txt_Descripcion.Text = ""
        Txt_VAL_CON.Text = ""
        'DESHABILITA CAJAS DE TEXTOS

        Txt_Mto_Gasto.ReadOnly = True
        Txt_Descripcion.ReadOnly = True
        Txt_VAL_CON.ReadOnly = True
        Dp_Tipo_Gasto.Enabled = False

        Txt_Mto_Gasto.CssClass = "clsDisabled"
        Txt_Mto_Gasto.CssClass = "clsDisabled"
        Txt_Descripcion.CssClass = "clsDisabled"
        Dp_Tipo_Gasto.CssClass = "clsDisabled"
        Txt_VAL_CON.CssClass = "clsDisabled"

        Gr_Gastos.DataSource = Nothing
        Gr_Gastos.DataBind()
        sesionope.coll_Gto = New Collection

        IB_Eliminar.Enabled = False
        IB_Guardar.Enabled = False
        IB_Buscar.Enabled = True
        IB_Nuevo.Enabled = True
    End Sub

    Public Sub BloqueaControles()

        Txt_Descripcion.ReadOnly = True
        Txt_Mto_Gasto.ReadOnly = True
        Txt_VAL_CON.ReadOnly = True
        Txt_Descripcion.CssClass = "clsDisabled"
        Txt_VAL_CON.CssClass = "clsDisabled"
        Txt_Mto_Gasto.CssClass = "clsDisabled"
        Dp_Tipo_Gasto.Enabled = False
        Dp_Tipo_Gasto.CssClass = "clsDisabled"

    End Sub

    Public Sub Cargadrop()
        CG.SucursalesDevuelve(codeje, True, DP_Sucursal)
        CG.ParametrosDevuelve(36, True, Dp_Tipo_Gasto)

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Not Agt.ValidaAccesso(20, 20020512, Usr, "PRESIONO NUEVO ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            HF_Codgto.Value = ""
            Rd_activo.Enabled = True
            Rd_Inactivo.Enabled = True
            Rd_activo.Checked = True
            Rd_Inactivo.Checked = False
            CB_IVA.Enabled = True
            CB_IVA.Checked = False
            Txt_Mto_Gasto.Text = ""
            Txt_Descripcion.Text = ""
            Txt_VAL_CON.Text = ""

            Txt_Mto_Gasto.ReadOnly = False
            Txt_Descripcion.ReadOnly = False
            Txt_VAL_CON.ReadOnly = False

            Dp_Tipo_Gasto.Enabled = True
            Dp_Tipo_Gasto.ClearSelection()
            Dp_Sucursal.ClearSelection()
            Txt_Mto_Gasto.CssClass = "clsMandatorio"
            Txt_Descripcion.CssClass = "clsMandatorio"
            Txt_VAL_CON.CssClass = "clsMandatorio"
            Dp_Tipo_Gasto.CssClass = "clsMandatorio"
            IB_Guardar.Enabled = True
            IB_Limpiar.Enabled = True
            IB_Eliminar.Enabled = False
            IB_Buscar.Enabled = False

            sesionope.coll_Gto = New Collection
            Gr_Gastos.DataSource = Nothing
            Gr_Gastos.DataBind()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Guardar.Click

        If Not Agt.ValidaAccesso(20, 20030512, Usr, "PRESIONO GUARDAR ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Try
            If Dp_Sucursal.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione Sucursal", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Dp_Tipo_Gasto.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione Tipo de Gasto", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Mto_Gasto.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese monto de Gasto", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Descripcion.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese Descripcion", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_VAL_CON.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese Validador Contable", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If HF_Codgto.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de Insertar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            Else
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Limpiar()
    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Not Agt.ValidaAccesso(20, 20040512, Usr, "PRESIONO ELIMINAR ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Try

            If HF_Codgto.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "Seleccione registro que desea eliminar", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de eliminar este registro?", TipoDeMensaje._Confirmacion, Link_Eliminar.UniqueID, True)

        Catch ex As Exception

            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If Not Agt.ValidaAccesso(20, 20010512, Usr, "PRESIONO BUSCAR ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Try
            sesionope.coll_Gto = New Collection

            If Dp_Sucursal.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione Sucursal", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            CargaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_Codgto.Value = btn.ToolTip
        MarcaGrillaNeg(btn.ToolTip)
        For i = 0 To Gr_Gastos.Rows.Count - 1
            If (Gr_Gastos.Rows(i).Cells(1).Text = btn.ToolTip) Then

                If (Gr_Gastos.Rows(i).Cells(5).Text = "ACTIVO") Then
                    Rd_activo.Checked = True
                    Rd_Inactivo.Checked = False
                Else
                    Rd_activo.Checked = False
                    Rd_Inactivo.Checked = True
                End If

                If (CType(Gr_Gastos.Rows(i).FindControl("Lb_IVA"), Label).Text().Trim() = "S") Then
                    CB_IVA.Checked = True
                Else
                    CB_IVA.Checked = False
                End If

                Dp_Tipo_Gasto.SelectedValue = CType(Gr_Gastos.Rows(i).FindControl("Lb_TipGasto"), Label).Text()
                Txt_Mto_Gasto.Text = Gr_Gastos.Rows(i).Cells(3).Text
                Txt_Descripcion.Text = Server.HtmlDecode(Gr_Gastos.Rows(i).Cells(4).Text)

                Txt_VAL_CON.Text = CType(Gr_Gastos.Rows(i).FindControl("Lb_Val"), Label).Text()

                Rd_activo.Enabled = True
                Rd_Inactivo.Enabled = True
                CB_IVA.Enabled = True
                Txt_VAL_CON.ReadOnly = False
                Txt_VAL_CON.CssClass = "clsMandatorio"

                IB_Guardar.Enabled = True
                IB_Eliminar.Enabled = True
                IB_Buscar.Enabled = False
                IB_Nuevo.Enabled = False
            End If
        Next

    End Sub

    Private Sub MarcaGrillaNeg(ByVal nrope As Integer)

        Try
            For I = 0 To Gr_Gastos.Rows.Count - 1
                If (nrope = Gr_Gastos.Rows(I).Cells(1).Text) Then

                    If (I Mod 2) = 0 Then
                        Gr_Gastos.Rows(I).CssClass = "selectable"
                    Else
                        Gr_Gastos.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        Gr_Gastos.Rows(I).CssClass = "formatUltcell"
                    Else
                        Gr_Gastos.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class

