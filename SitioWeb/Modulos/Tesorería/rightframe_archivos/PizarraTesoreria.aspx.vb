Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.SesionPagos

Partial Class Modulos_Pizarras_rigthframe_archivos_PizarraTesoreria
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra Tesoreria"
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Pagos As New ClsSession.SesionPagos
    Dim msj As New ClsMensaje
    Dim PGO As New ClasePagos
    Dim TSR As New ClaseTesoreria
    Dim RW As New FuncionesGenerales.RutinasWeb

#End Region

#Region "BOTONERA"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Me.Txt_Nro_Nomina.Text = ""
            Me.Txt_Fec_Vto.Text = ""
            Me.Txt_Mto_Dco.Text = ""
            Me.Txt_Orden.Text = ""
            Me.Txt_Fec_Emi.Text = ""
            Me.Txt_Cta_Cte.Text = ""
            'Me.Txt_Cant_Egresos.Text = ""
            Me.Txt_NroDocto.Text = ""
            Me.Txt_Orden.Text = ""
            'Me.Txt_Total_Egreso.Text = ""
            Me.Txt_Total_Ing.Text = ""

            Me.DP_Banco.ClearSelection()
            Me.DP_Banco_Egr.ClearSelection()
            Me.DP_Banco_Ing.ClearSelection()
            Me.DP_EncargadoDep.ClearSelection()
            Me.DP_FormaPago_Ing.ClearSelection()
            Me.DP_OrigenFondo.ClearSelection()
            Me.DP_Plaza_Ing.ClearSelection()
            Me.DP_PlazaBanco.ClearSelection()

            Me.GV_DetallePago.Controls.Clear()
            Me.GV_Egresos.Controls.Clear()
            Me.Gv_Pagos.Controls.Clear()

            GV_DetallePago.DataSource = New Collection
            GV_DetallePago.DataBind()

            GV_Egresos.DataSource = New Collection
            GV_Egresos.DataBind()

            Gv_Pagos.DataSource = New Collection
            Gv_Pagos.DataBind()

            'RB_Pagos.SelectedValue = "S"
            'RB_Origen.SelectedValue = 1
            'RB_Antes.SelectedValue = "S"
            DP_FormaPago_Egr.ClearSelection()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            If Not ValidaBusqueda() Then Exit Sub

            If CB_Nomina.Checked Then

                If Txt_Nro_Nomina.Text <> "" And Txt_Nro_Nomina.Text <> "0" Then

                    Dim ing As New DataSet_Cheques.sp_Reporte_Nomina_IngresoDataTable
                    Dim ingreso As New DataSet_ChequesTableAdapters.sp_Reporte_Nomina_IngresoTableAdapter

                    ing = ingreso.GetData(Txt_Nro_Nomina.Text.Replace(".", ""))

                    If ing.Count <= 0 Then
                        msj.Mensaje(Page, "Atención", "No se encontro nómina con el N° ingresado", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Txt_Nro_Nomina.Text = ""
                        Txt_Nro_Nomina.Focus()
                        Exit Sub
                    Else
                        AbrePopup(Page, 1, "ReporteIngresoNomina.aspx?Nomina=" & Txt_Nro_Nomina.Text.Replace(".", "") & "", "InformeNomina", 1150, 800, 500, 500)
                    End If

                Else
                    msj.Mensaje(Me, "Atención", "Debe ingresar n° de nomina a buscar", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            Else
	                   If Me.TabContainer1.ActiveTabIndex = 0 Then
			                      BuscaIngresos()
					                      Else
                    BuscaEgresos()
                End If
            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            'Verificamos que es lo que esta guardando
            Select Case TabContainer1.ActiveTabIndex

                Case 0 'Ingresos

                    If Me.TabContainer1.ActiveTabIndex = 0 Then
                        If DP_EncargadoDep.SelectedIndex <= 0 Then
                            msj.Mensaje(Me, Caption, "Debe seleccionar encargado de depósito", TipoDeMensaje._Informacion)
                            Exit Sub
                        End If
                    End If

                    If Gv_Pagos.Rows.Count <= 0 Then
                        msj.Mensaje(Me, Caption, "Deben haber ingresos para guardar", TipoDeMensaje._Informacion)
                        Exit Sub
                    End If

                    Dim cantidad As Integer
                    Dim monto As Double
                    Dim coll_dpo As New Collection

                    For I = 0 To Gv_Pagos.Rows.Count - 1

                        If CType(Gv_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                            Dim lb As Label
                            lb = Me.Gv_Pagos.Rows(I).FindControl("lb_dpo")
                            Dim id_dpo As Integer
                            id_dpo = Gv_Pagos.Rows(I).Cells(2).Text
                            cantidad = cantidad + 1
                            monto = monto + CDbl(Gv_Pagos.Rows(I).Cells(4).Text)
                            coll_dpo.Add(id_dpo)
                        End If

                    Next

                    If cantidad <= 0 Then
                        msj.Mensaje(Me, Caption, "Debe seleccionar al menos un ingreso para guardar", TipoDeMensaje._Informacion)
                        Exit Sub
                    End If

                    msj.Mensaje(Page, Caption, "¿Desea guardar ingresos?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Ingreso.UniqueID)

                    'Case 1 'Egresos

                    '    If GV_Egresos.Rows.Count <= 0 Then
                    '        msj.Mensaje(Me, Caption, "Deben haber egresos para guardar", TipoDeMensaje._Informacion)
                    '        Exit Sub
                    '    End If

                    '    Dim Cantidad_Eg As Integer = 0

                    '    For i = 0 To GV_Egresos.Rows.Count - 1
                    '        If CType(GV_Egresos.Rows(i).FindControl("CB"), CheckBox).Checked = True Then
                    '            Cantidad_Eg = Cantidad_Eg + 1
                    '            Exit For
                    '        End If
                    '    Next

                    '    If Cantidad_Eg = 0 Then
                    '        msj.Mensaje(Me, Caption, "Debe seleccionar al menos un egreso", ClsMensaje.TipoDeMensaje._Excepcion)
                    '        Exit Select
                    '    End If

                    '    MP_DoctoPago.Show()

                    '    'If CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, DP_FormaPago_Egr.SelectedValue).Item(1).pnu_atr_002 = "N" Then
                    '    '    MP_DoctoPago.Show()
                    '    'Else
                    '    '    LB_Egreso_Click(sender, e)
                    '    'End If

            End Select

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "SUB"

    Private Sub BuscaIngresos()

        Dim Coll_Ingreso As Collection
        Dim FormaPago_Desde As Integer
        Dim FormaPago_Hasta As Integer
        Dim Banco_Desde As Integer
        Dim Banco_Hasta As Integer
        Dim col As System.Drawing.Color
        Dim Plaza_Desde As String
        Dim Plaza_Hasta As String

        Txt_Total_Ing.Text = 0


        If DP_FormaPago_Ing.SelectedIndex > 0 Then
            FormaPago_Desde = DP_FormaPago_Ing.SelectedValue
            FormaPago_Hasta = DP_FormaPago_Ing.SelectedValue
        Else
            FormaPago_Desde = 0
            FormaPago_Hasta = 999
        End If

        If DP_Banco_Ing.SelectedIndex > 0 Then
            Banco_Desde = DP_Banco_Ing.SelectedValue
            Banco_Hasta = DP_Banco_Ing.SelectedValue
        Else
            Banco_Desde = 0
            Banco_Hasta = 999
        End If

        If DP_Plaza_Ing.SelectedIndex > 0 Then
            Plaza_Desde = DP_Plaza_Ing.SelectedValue
            Plaza_Hasta = DP_Plaza_Ing.SelectedValue
        Else
            Plaza_Desde = "000000"
            Plaza_Hasta = "999999"
        End If

        If Me.Txt_Nro_Nomina.Text = "" Then
            Me.Txt_Nro_Nomina.Text = 0
        End If

        Coll_Ingreso = TSR.PagosNominaIngreso_Devuelve(Val(Txt_Nro_Nomina.Text), _
                                                       FormaPago_Desde, _
                                                       FormaPago_Hasta, _
                                                       Banco_Desde, _
                                                       Banco_Hasta, _
                                                       Plaza_Desde, _
                                                       Plaza_Hasta)

        Gv_Pagos.DataSource = Coll_Ingreso
        Gv_Pagos.DataBind()

        If Gv_Pagos.Rows.Count = 0 Then
            msj.Mensaje(Me, "Atención", "No se encuentran Ingresos", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'marcamos los que han sido entregados
        For I = 0 To Gv_Pagos.Rows.Count - 1

            If Coll_Ingreso.Item(I + 1).ing_pgo_hre = "S" Then 'Pago Hoja de recaudacion
                col = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
            Else 'pago directo
                col = System.Drawing.ColorTranslator.FromHtml("#C2DAFA")
            End If

            Gv_Pagos.Rows(I).BackColor = col

        Next

    End Sub

    Private Sub BuscaEgresos()

        Dim TipoEgreso_Desde As Integer
        Dim TipoEgreso_Hasta As Integer
        Dim QuienPaga_Desde As Integer
        Dim QuienPaga_hasta As Integer
        Dim Fecha_Desde As String
        Dim Fecha_hasta As String
        Dim Banco_Desde As Integer
        Dim Banco_Hasta As Integer
        Dim col As System.Drawing.Color

        'Txt_Total_Egreso.Text = 0
        'Txt_Cant_Egresos.Text = 0

        
        If DP_Banco_Egr.SelectedIndex > 0 Then
            Banco_Desde = DP_Banco_Egr.SelectedValue
            Banco_Hasta = DP_Banco_Egr.SelectedValue
        Else
            Banco_Desde = 0
            Banco_Hasta = 999
        End If

        If DP_FormaPago_Egr.SelectedIndex > 0 Then
            TipoEgreso_Desde = DP_FormaPago_Egr.SelectedValue
            TipoEgreso_Hasta = DP_FormaPago_Egr.SelectedValue
        Else
            TipoEgreso_Desde = 0
            TipoEgreso_Hasta = 999
        End If

        QuienPaga_Desde = 1
        QuienPaga_hasta = 5

        If Txt_FechaDeposito.Text <> "" Then
            Fecha_Desde = Txt_FechaDeposito.Text
            Fecha_hasta = Txt_FechaDeposito.Text
        Else
            Fecha_Desde = "01-01-1900"
            Fecha_hasta = "01-01-3000"
        End If

        coll_egr = New Collection

        coll_egr = TSR.NominaEgreso_Devuelve(Val(Txt_Nro_Nomina.Text.Replace(".", "")), _
                                                Banco_Desde, _
                                                Banco_Hasta, _
                                                TipoEgreso_Desde, _
                                                TipoEgreso_Hasta, _
                                                QuienPaga_Desde, _
                                                QuienPaga_hasta, _
                                                Fecha_Desde, _
                                                Fecha_hasta)

        GV_Egresos.DataSource = coll_egr
        GV_Egresos.DataBind()

        If Not Me.GV_Egresos.Rows.Count > 0 And Not Me.Gv_Pagos.Rows.Count > 0 Then
            msj.Mensaje(Me, "Atención", "No se encuentran Egresos", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'marcamos los que han sido entregados
        For I = 0 To GV_Egresos.Rows.Count - 1

            If coll_egr.Item(I + 1).Entregado = "S" Then
                col = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                GV_Egresos.Rows(I).BackColor = col
                CType(GV_Egresos.Rows(I).FindControl("Btn_Entregar"), Button).Visible = False
            End If

            Me.GV_Egresos.Rows(I).Cells(3).Text = IIf(GV_Egresos.Rows(I).Cells(3).Text = "S", "SI", "NO")
            Me.GV_Egresos.Rows(I).Cells(4).Text = Format(CLng(GV_Egresos.Rows(I).Cells(4).Text), Fmt.FCMSD) & "-" & RC.Vrut(GV_Egresos.Rows(I).Cells(4).Text)

        Next

        HabilitaEncargadoDeposito(True)

        IB_Guardar.Enabled = True

    End Sub

    Private Sub LimpiaIngresos()

        Try

            Gv_Pagos.DataSource = Nothing
            Gv_Pagos.DataBind()

            GV_DetallePago.DataSource = Nothing
            GV_DetallePago.DataBind()

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub LimpiaEgresos()

        Try

            GV_Egresos.DataSource = Nothing
            GV_Egresos.DataBind()

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaEncargadoDeposito(ByVal Estado As Boolean)

        DP_EncargadoDep.Enabled = Estado

        If Estado Then
            DP_EncargadoDep.CssClass = "clsMandatorio"
        Else
            DP_EncargadoDep.CssClass = "clsDisabled"
        End If

    End Sub

    Private Function ValidaBusqueda() As Boolean
        Try

            If CB_Nomina.Checked Then
                If Txt_Nro_Nomina.Text = "" Then
                    msj.Mensaje(Me, Caption, "Debe ingresar n° de nomina a buscar", TipoDeMensaje._Informacion)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Function

    Private Sub CargaDrop()

        Try

            'Ingreso
            CG.ParametrosDevuelve(TablaParametro.TipoIngreso, True, DP_FormaPago_Ing)
            CG.BancosDevuelveTodos(DP_Banco_Ing)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_Plaza_Ing)

            'Egresos
            CG.ParametrosDevuelve(TablaParametro.TipoEgreso, True, DP_FormaPago_Egr)
            CG.BancosDevuelveTodos(DP_Banco_Egr)
            CG.EjecutivosDevuelve(DP_EncargadoDep, CodEje, 34) '31

            'modal de egreso
            CG.BancosDevuelveTodos(DP_Banco)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_PlazaBanco)
            CG.ParametrosDevuelveTodos(TablaParametro.OrigenFondo, True, DP_OrigenFondo)



        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub BloqueaCriterioIngresos(ByVal Estado As Boolean)

        DP_FormaPago_Ing.Enabled = Not Estado
        DP_Banco_Ing.Enabled = Not Estado
        DP_Plaza_Ing.Enabled = Not Estado

        If Not Estado Then
            DP_FormaPago_Ing.CssClass = "clsTxt"
            DP_Banco_Ing.CssClass = "clsTxt"
            DP_Plaza_Ing.CssClass = "clsTxt"
        Else
            DP_FormaPago_Ing.CssClass = "clsDisabled"
            DP_Banco_Ing.CssClass = "clsDisabled"
            DP_Plaza_Ing.CssClass = "clsDisabled"
        End If

    End Sub

    Private Sub BloqueaCriterioEgresos(ByVal Estado As Boolean)

        DP_FormaPago_Egr.Enabled = Not Estado
        DP_Banco_Egr.Enabled = Not Estado
        'RB_Pagos.Enabled = Not Estado
        'RB_Origen.Enabled = Not Estado
        'RB_Antes.Enabled = Not Estado

        If Not Estado Then
            DP_FormaPago_Egr.CssClass = "clsTxt"
            DP_Banco_Egr.CssClass = "clsTxt"
        Else
            DP_FormaPago_Egr.CssClass = "clsDisabled"
            DP_Banco_Egr.CssClass = "clsDisabled"
        End If

    End Sub

    Private Sub Marca_GV_Pagos(ByVal nro As Integer)
        Try

            For I = 0 To Gv_Pagos.Rows.Count - 1
                'If Val(HF_Pos_Ing.Value) - 1 = I Then
                If nro = Trim(Replace(Gv_Pagos.Rows(I).Cells(2).Text, ".", "")) Then

                    If (I Mod 2) = 0 Then
                        Gv_Pagos.Rows(I).CssClass = "selectable"
                        'Gv_Pagos.Rows(I).CssClass = "clicktable"
                    Else
                        Gv_Pagos.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        Gv_Pagos.Rows(I).CssClass = "formatUltcell"
                    Else
                        Gv_Pagos.Rows(I).CssClass = "formatUltcellAlt"
                        'Gv_Pagos.Rows(I).CssClass = "formatable"
                    End If
                End If
            Next

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                Response.Expires = -1

                IB_AyudaNom.Enabled = False
                CB_Nomina.Checked = False
                CB_Nomina_CheckedChanged(Me, e)


                Txt_Nro_Nomina.Focus()
                CargaDrop()

                Txt_Nro_Nomina.Attributes.Add("Style", "Text-align:right")
                Txt_NroDocto.Attributes.Add("Style", "Text-align:right")
                'Txt_Total_Egreso.Attributes.Add("Style", "Text-align:right")
                'Txt_Cant_Egresos.Attributes.Add("Style", "Text-align:right")
                Txt_Total_Ing.Attributes.Add("Style", "Text-align:right")

                BuscaIngresos()

            End If

            IB_AyudaNom.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaNom.aspx','PopUpNomina',580,410,200,150);")

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Not IsPostBack Then
                Modulo = "Tesoreria"
                'Esto de abajo es para los skins
                Pagina = Page.AppRelativeVirtualPath
                CambioTema(Page)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Pagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gv_Pagos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
        End If
    End Sub

    Protected Sub CB_Nomina_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Nomina.CheckedChanged

        Try

            If CB_Nomina.Checked Then
                Txt_Nro_Nomina.Text = ""
                Txt_Nro_Nomina.ReadOnly = False
                Txt_Nro_Nomina.CssClass = "clsMandatorio"

                Txt_FechaDeposito.Text = ""
                Txt_FechaDeposito.ReadOnly = True
                Txt_FechaDeposito.CssClass = "clsDisabled"

                BloqueaCriterioIngresos(True)
                BloqueaCriterioEgresos(True)
                IB_AyudaNom.Enabled = True

            Else
                Txt_FechaDeposito.Text = Date.Now.ToShortDateString
                Txt_FechaDeposito.ReadOnly = False
                Txt_FechaDeposito.CssClass = "clsMandatorio"

                Txt_Nro_Nomina.Text = ""
                Txt_Nro_Nomina.ReadOnly = True
                Txt_Nro_Nomina.CssClass = "clsDisabled"
                BloqueaCriterioIngresos(False)
                BloqueaCriterioEgresos(False)
                IB_AyudaNom.Enabled = False
            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_BuscarDetallePago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarDetallePago.Click

    End Sub

    Protected Sub CB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        
            Dim Monto As Double = 0
            Dim Sumatoria As Double = 0
            Dim CB As CheckBox

            For I = 0 To Gv_Pagos.Rows.Count - 1
                CB = CType(Gv_Pagos.Rows(I).FindControl("CB"), CheckBox)
                If CB.Checked Then
                    Monto = Gv_Pagos.Rows(I).Cells(4).Text
                    Sumatoria += Monto
                    CB.Focus()
                End If
            Next

            Txt_Total_Ing.Text = Format(Sumatoria, Fmt.FCMSD)

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub TabButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim containerId As String = "TabContent" & TabContainer1.ActiveTabIndex

            Dim panel As Panel = TabContainer1.ActiveTab.FindControl(containerId)

            If (Not panel Is Nothing) Then
                panel.Visible = True
            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged

        If Me.TabContainer1.ActiveTabIndex = 0 Then
            Me.DP_EncargadoDep.Enabled = True
            Me.DP_EncargadoDep.CssClass = "clsMandatorio"
            BuscaIngresos()
            IB_Guardar.Enabled = True
        Else
            Me.DP_EncargadoDep.Enabled = False
            Me.DP_EncargadoDep.SelectedValue = 0
            Me.DP_EncargadoDep.CssClass = "clsDisabled"
            BuscaEgresos()
            IB_Guardar.Enabled = False
        End If

    End Sub

    Protected Sub LB_Ingreso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Ingreso.Click
        Try

            Dim Coll_dpo As New Collection
            Dim Cantidad As Integer
            Dim Monto As Double

            For I = 0 To Gv_Pagos.Rows.Count - 1

                If CType(Gv_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                    Dim lb As Label
                    lb = Me.Gv_Pagos.Rows(I).FindControl("lb_dpo")
                    Dim id_dpo As Integer
                    id_dpo = Gv_Pagos.Rows(I).Cells(2).Text
                    Cantidad = Cantidad + 1
                    Monto = Monto + CDbl(Gv_Pagos.Rows(I).Cells(4).Text)
                    Coll_dpo.Add(id_dpo)
                End If

            Next

            Dim Nma As New nma_cls

            Nma.nma_fec = Date.Now
            Nma.nma_fec_dep = Txt_FechaDeposito.Text
            Nma.nma_mto = Monto
            Nma.nma_tot_dpo = Cantidad
            Nma.nma_ioe = "I"
            Nma.id_eje_rpb = CodEje
            Nma.id_eje_dep = DP_EncargadoDep.SelectedValue
            Nma.id_bco = Nothing

            Dim id_Nomina As Integer
            id_Nomina = AG.NominaIngreso_Inserta(Nma, Coll_dpo)

            AbrePopup(Page, 1, "ReporteIngresoNomina.aspx?Nomina=" & id_Nomina & "", "InformeNomina", 1150, 800, 500, 500)


            DP_EncargadoDep.ClearSelection()
            Txt_Total_Ing.Text = ""
            LimpiaIngresos()
            Dim img_arg As System.Web.UI.ImageClickEventArgs
            IB_Buscar_Click(Me, img_arg)

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_Egreso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Egreso.Click

        Try

            Dim coll_egr_dep As New Collection

            For I = 1 To coll_egr.Count

                If coll_egr(I).id_egre = HF_Egreso.Value Then

                    '----------------------------------------------------
                    'Se genera una nueva nomina de egreso por los pagos
                    '----------------------------------------------------

                    Dim Nma As New nma_cls

                    Nma.nma_fec = Date.Now
                    Nma.nma_fec_dep = Txt_FechaDeposito.Text
                    Nma.nma_mto = coll_egr(I).Monto_Egreso
                    Nma.nma_tot_dpo = 1
                    Nma.nma_ioe = "E"
                    Nma.id_eje_rpb = CodEje
                    Nma.id_eje_dep = Nothing
                    Nma.id_bco = Nothing

                    '--------------------------------------------------------------------
                    'Se genera el deposito para los egresos con atributo pnu_atr_002 = N
                    '--------------------------------------------------------------------
                    Dim DPO As New dpo_cls

                    If Txt_NroDocto.Text <> "" Then
                        DPO.dpo_num = Txt_NroDocto.Text
                    End If

                    DPO.id_bco = DP_Banco.SelectedValue
                    DPO.id_PL_000047 = DP_PlazaBanco.SelectedValue
                    DPO.id_P_0052 = 1
                    DPO.id_P_0054 = 4 'PAGO CHEQUE BANCO
                    DPO.id_P_0023 = 1
                    DPO.dpo_fec_emi = Txt_Fec_Emi.Text
                    DPO.dpo_fev = Txt_Fec_Vto.Text
                    DPO.dpo_cct = Txt_Cta_Cte.Text
                    DPO.id_P_0087 = DP_OrigenFondo.SelectedValue
                    DPO.dpo_aor = Txt_Orden.Text.Trim
                    DPO.dpo_num = Txt_NroDocto.Text
                    DPO.dpo_mto = Txt_Mto_Dco.Text

                    Dim id_Nomina As Integer

                    coll_egr_dep.Add(coll_egr(I).id_egre)

                    id_Nomina = AG.NominaEgreso_Inserta(Nma, DPO, coll_egr_dep)

                    AbrePopup(Page, 1, "ReporteIngresoNomina.aspx?Nomina=" & id_Nomina & "", "InformeNomina", 1150, 800, 500, 500)

                    Dim img_arg As System.Web.UI.ImageClickEventArgs
                    IB_Buscar_Click(Me, img_arg)

                    msj.Mensaje(Me, "Atención", "Se ha guardado correctamente", ClsMensaje.TipoDeMensaje._Exclamacion)

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)

        Try
            If btn.ToolTip <> "" Then
                'HF_Id_Ing.Value = btn.ToolTip
                Dim Coll_Detalle As Collection

                Coll_Detalle = CG.IngresoDevuelveModoDePago(btn.ToolTip)

                GV_DetallePago.DataSource = Coll_Detalle
                GV_DetallePago.DataBind()

                Marca_GV_Pagos(btn.ToolTip)

                Dim Formato As String = ""

                For I = 0 To GV_DetallePago.Rows.Count - 1

                    Select Case Coll_Detalle.Item(I + 1).id_p_0023
                        Case 1 : Formato = Fmt.FCMSD
                        Case 2, 4 : Formato = Fmt.FCMCD4
                        Case 3 : Formato = Fmt.FCMCD
                    End Select
                    GV_DetallePago.Rows(I).Cells(3).Text = Format(CDbl(GV_DetallePago.Rows(I).Cells(3).Text), Formato)
                Next


            End If
        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_NOMINA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_NOMINA.Click
        'Txt_Nro_Nomina.Text = HF_NOMINA.Value
        Txt_Nro_Nomina.Text = 1
    End Sub

    Protected Sub Btn_Entregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim btn As Button = sender
            Dim coll_egr_dep As New Collection

            'Buscamos el egreso seleccionado
            For I = 1 To coll_egr.Count

                If coll_egr(I).id_egre = btn.ToolTip Then

                    If CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, _
                                                      coll_egr(I).id_FormaPago).Item(1).pnu_atr_004 = "S" Then

                        'Si es cheque solicitara los datos de este
                        Txt_Mto_Dco.Text = Format(coll_egr(I).Monto_Egreso, RC.DevuelveFormatoMoneda(coll_egr(I).Id_Moneda))
                        HF_Egreso.Value = coll_egr(I).id_egre()
                        MP_DoctoPago.Show()

                        Exit Sub

                    End If

                    coll_egr_dep.Add(coll_egr(I).id_egre)
                    '----------------------------------------------------
                    'Se genera una nueva nomina de egreso por los pagos
                    '----------------------------------------------------
                    Dim Nma As New nma_cls

                    Nma.nma_fec = Date.Now
                    Nma.nma_fec_dep = Txt_FechaDeposito.Text
                    Nma.nma_mto = coll_egr(I).Monto_Egreso
                    Nma.nma_tot_dpo = 1
                    Nma.nma_ioe = "E"
                    Nma.id_eje_rpb = CodEje
                    Nma.id_eje_dep = Nothing
                    Nma.id_bco = Nothing

                    '--------------------------------------------------------------------
                    'Se genera el deposito para los egresos 
                    '--------------------------------------------------------------------
                    Dim DPO As New dpo_cls
                    Dim id_Nomina As Integer

                    DPO.id_bco = Nothing
                    DPO.id_P_0052 = 1

                    If coll_egr(I).id_FormaPago = 3 Then
                        DPO.id_P_0054 = 1 'EFECTIVO
                    Else
                        DPO.id_P_0054 = 8 'CUENTA INTERNA
                    End If

                    DPO.id_P_0023 = CInt(coll_egr(I).Id_Moneda)
                    DPO.dpo_mto = coll_egr(I).Monto_Egreso
                    DPO.dpo_cct = coll_egr(I).CtaCte

                    If coll_egr(I).Id_Banco.ToString <> "" And coll_egr(I).Id_Banco.ToString <> "0" Then
                        DPO.id_bco = CInt(coll_egr(I).Id_Banco)
                    End If

                    DPO.id_P_0087 = 1 'ABONO CTA. CTE.

                    id_Nomina = AG.NominaEgreso_Inserta(Nma, DPO, coll_egr_dep)

                    If id_Nomina > 0 Then

                        'AbrePopupUpdatePanel(UP_General, 1, "ReporteIngresoNomina.aspx?Nomina=" & id_Nomina & "", "InformeNomina", 1150, 800, 500, 500)
                        AbrePopup(Page, 1, "ReporteIngresoNomina.aspx?Nomina=" & id_Nomina & "", "InformeNomina", 1150, 800, 500, 500)

                        Dim img_arg As System.Web.UI.ImageClickEventArgs
                        IB_Buscar_Click(Me, img_arg)

                        msj.Mensaje(Me, "Atención", "Se ha guardado correctamente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Else
                        msj.Mensaje(Me, "Atención", "No se ha guardado correctamente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    End If

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Sub

    Protected Sub Gv_Pagos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Gv_Pagos.Sorting

        Try

            If e.SortExpression = "todo" Then

                Dim Monto As Double = 0
                Dim Sumatoria As Double = 0
                Dim CB As CheckBox

                For I = 0 To Gv_Pagos.Rows.Count - 1
                    CB = CType(Gv_Pagos.Rows(I).FindControl("CB"), CheckBox)
                    CB.Checked = True
                    If CB.Checked Then
                        Monto = Gv_Pagos.Rows(I).Cells(4).Text
                        Sumatoria += Monto
                        CB.Focus()
                    End If
                Next

                Txt_Total_Ing.Text = Format(Sumatoria, Fmt.FCMSD)

            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub


#End Region

#Region "Modal Egreso"

    Protected Sub IB_AceptarCheque_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AceptarCheque.Click

        Try

            If HF_Egreso.Value = "" Then
                msj.Mensaje(Me, Caption, "No encuentra el egreo", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            'Validamos que los campos no esten vacios
            If DP_Banco.SelectedIndex = 0 Then
                msj.Mensaje(Me, Caption, "Debe seleccionar banco", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If DP_PlazaBanco.SelectedIndex = 0 Then
                msj.Mensaje(Me, Caption, "Debe seleccionar plaza", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Txt_NroDocto.Text = "" Then
                msj.Mensaje(Me, Caption, "Debe ingresar n° del documento", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If


            If Txt_Fec_Emi.Text = "" Then
                msj.Mensaje(Me, Caption, "Debe ingresar fecha de emisión", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Txt_Fec_Vto.Text = "" Then
                msj.Mensaje(Me, Caption, "Debe ingresar fecha de vencimiento", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Emi.Text) Then
                msj.Mensaje(Me, Caption, "Fecha de emisión errónea", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Vto.Text) Then
                msj.Mensaje(Me, Caption, "Fecha de vencimiento errónea", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Txt_Cta_Cte.Text = "" Then
                msj.Mensaje(Me, Caption, "Debe ingresar cuenta corriente", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If DP_OrigenFondo.SelectedIndex = 0 Then
                msj.Mensaje(Me, Caption, "Debe seleccionar origen", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If


            If DateDiff(DateInterval.Day, CDate(Me.Txt_Fec_Emi.Text), CDate(Me.Txt_Fec_Vto.Text)) < 0 Then
                msj.Mensaje(Me, Caption, "La fecha de vencimiento debe ser mayor que la de emisión", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If

            If Txt_Orden.Text = "" Then
                msj.Mensaje(Me, Caption, "Debe ingresar a orden de quien", ClsMensaje.TipoDeMensaje._Excepcion)
                MP_DoctoPago.Show()
                Exit Sub
            End If


            msj.Mensaje(Page, Caption, "¿Confirma aceptar cheque?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Egreso.UniqueID)


            'GuardaEgreso()


        Catch ex As Exception
            msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_CancelarCheque_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CancelarCheque.Click

    End Sub

#End Region


    
End Class
