Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports [dim].xml

Partial Class MLineaCredito
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim agt As New Perfiles.Cls_Principal
    Dim RG As New FuncionesGenerales.FComunes
    Dim Caption As String = "Linea de Crédito"
    Dim Var As New FuncionesGenerales.Variables
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim FC As New FuncionesGenerales.FComunes

#End Region

#Region "EVENTOS"

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then


            If Me.Request.QueryString("Tipo") <> "" Then

                Dim Tipo As Int16 = Val(Me.Request.QueryString("Tipo"))

                Select Case Tipo
                    Case 1 : Modulo = "Linea Credito"
                    Case 2 : Modulo = "Control Dual"
                    Case Else : Modulo = "Linea Credito"

                End Select

            End If

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Sesion As New ClsSession.ClsSession

            AlineaMonto()
            CargaDrop()
            Txt_Rut_Cli.Focus()
            Accion.Value = ""

            If Me.Request.QueryString("Tipo") <> "" Then

                Dim Tipo As Int16 = Val(Me.Request.QueryString("Tipo"))

                Select Case Tipo
                    Case 1 : Ingreso()
                    Case 2 : Aprobacion()
                    Case Else : Ingreso()

                End Select
            Else
                Ingreso()
            End If

        End If

        IB_SubLinea.Attributes.Add("onClick", "WinOpen(2, 'SubLineas.aspx', 'PopUpCliente',620,410,200,150);")
        IB_Anticipo.Attributes.Add("onClick", "WinOpen(2, 'Anticipos.aspx', 'PopUpCliente',620,410,200,150);")
        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',620,410,200,150);")

        If Txt_Rut_Cli.Text <> "" Then
            IB_HFirma.Attributes.Add("onClick", "WinOpen(1, 'reporte_FC.aspx?id=" & Replace(Txt_Rut_Cli.Text, ".", "") & "', 'ReporteHojaFirmas',1000,2000,15,15);")
            IB_Comision.Attributes.Add("onClick", "WinOpen(2, 'pop_up_com_cli.aspx?rut=" & Txt_Rut_Cli.Text & " &Ra=" & Txt_Raz_Soc.Text & "', 'PopUpCliente',650,410,200,150);")
            IB_Gastos.Attributes.Add("onClick", "WinOpen(2, 'Pop_Up_Gastos.aspx?rut=" & Txt_Rut_Cli.Text & " &Ra=" & Txt_Raz_Soc.Text & "', 'PopUpCliente',650,610,200,150);")
            LB_Refrescar_Click(Me, e)
        End If

    End Sub

    Private Sub Ingreso()

        Titulo.Text = "Ingreso Linea de Financiamiento"

        IB_Ficha.Visible = False
        IB_Actas.Visible = False
        IB_Pagare.Visible = False
        Me.IB_HFirma.Visible = False
        Me.IB_VistoBueno.Visible = False
        Txt_Por_Exc.ReadOnly = True
        Txt_Por_Exc.CssClass = "clsDisabled"
        IB_Anticipo.Enabled = False
        IB_SubLinea.Enabled = False

        Accion.Value = "NUEVO"

    End Sub

    Private Sub Aprobacion()
        Titulo.Text = "Aprobación de Linea de Financiamiento"
        IB_Reglamento.Visible = True
        IB_Reglamento.Enabled = False

        'Me.IB_Ficha.Visible = True
        'Me.IB_Ficha.Enabled = False

        Me.IB_Actas.Visible = True
        Me.IB_Actas.Enabled = False
        Me.IB_Pagare.Visible = True
        Me.IB_Pagare.Enabled = False
        Me.IB_HFirma.Visible = True
        Me.IB_HFirma.Enabled = False
        Me.IB_VistoBueno.Visible = True
        Me.IB_Guardar.Visible = False
        Me.IB_SubLinea.Visible = False
        Me.IB_Anticipo.Visible = False
        Me.IB_Nuevo.Visible = False
        Me.IB_Gastos.Visible = False
        Me.IB_Comision.Visible = False
        Accion.Value = "Aprobacion"
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        If Me.CargaDatosCliente() Then
            CargaDatosLineaCredito()
            IB_Buscar.Enabled = False
        End If
    End Sub


#End Region

#Region "LINKBUTTON"

    Protected Sub LB_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If CargaDatosCliente() Then
            CargaDatosLineaCredito()
        End If

    End Sub


    Protected Sub LB_CargaDatosLinea_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim Pos As Int16

            'Nro de Linea que va a cargar
            If Val(Me.NroLinea.Value) > 0 Then

                Dim ClsLdc As New ConsultasGenerales
                Dim sesion As New ClsSession.ClsSession
                Dim FMT As New FuncionesGenerales.ClsLocateInfo
                Dim LDC As Object

                IB_Anticipo.Enabled = True
                IB_SubLinea.Enabled = True
                IB_Imprimir.Enabled = True
                IB_Guardar.Enabled = True

                sesion.NroLineaCredito = NroLinea.Value

                LDC = ClsLdc.LineaDeCreditoDevuelvePorNro(sesion.RutCli, Me.NroLinea.Value)

                Select Case Val(Me.Request.QueryString("Tipo"))
                    Case 1 : HabilitaIngreso("clsMandatorio", False)
                    Case 2 : HabilitaAprobacion("clsMandatorio", False)
                    Case Else : HabilitaIngreso("clsMandatorio", False)
                End Select

                With LDC

                    Me.Txt_NroLinea.Text = .id_ldc
                    Me.DP_EstadoLinea.ClearSelection()
                    Me.DP_EstadoLinea.Items.FindByValue(.ldc_est).Selected = True
                    Me.Txt_Mto_Dis.Text = Format((.ldc_mto_apb - .ldc_mto_ocp), FMT.FCMSD)
                    Me.Txt_Fec_Sol.Text = .ldc_fec_sol
                    Me.Txt_Mto_Sol.Text = Format(.ldc_mto_sol, FMT.FCMSD)

                    sesion.MtoLineaCredito = Me.Txt_Mto_Sol.Text

                    Me.Txt_Fec_Dsd.Text = If(.ldc_fec_vig_dde = Nothing, "", .ldc_fec_vig_dde)
                    Me.Txt_Fec_Hta.Text = If(.ldc_fec_vig_hta = Nothing, "", .ldc_fec_vig_hta)
                    Me.Txt_Fec_Res.Text = If(.ldc_fec_rsn = Nothing, "", .ldc_fec_rsn)
                    Me.Txt_Mto_Apr.Text = Format(If(.ldc_mto_apb = Nothing, 0, .ldc_mto_apb), FMT.FCMSD)
                    sesion.MtoLineaCreditoApr = Me.Txt_Mto_Apr.Text

                    Select Case .ldc_tip_com
                        Case "N" : Me.RB_Normal.Checked = True : Me.RB_Especial.Checked = False : Me.Txt_Obs_Com.Text = .ldc_des_com
                        Case "E" : Me.RB_Especial.Checked = True : Me.RB_Normal.Checked = False : Me.Txt_Obs_Com.Text = ""
                    End Select

                    Me.Txt_Observacion.Text = .ldc_obs

                    'vez = 1
                    CargaDatosAnticipos()
                    'vez = 2
                    CargaDatosSubLineas()

                    If .ldc_est <> 1 Then

                        Me.IB_Anticipo.Enabled = False
                        Me.IB_SubLinea.Enabled = False

                    End If
                End With


            Else
                LimpiaDetalle()
            End If

            
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub

    Protected Sub IB_CloseInt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click
        Me.CargaDatosSubLineas()
        Me.CargaDatosAnticipos()
    End Sub

#End Region

#Region "CARGA DATOS DE LINEA DE CREDITO"

    Private Sub MarcaGrillaNeg(ByVal nroli As Integer)

        Try
            For I = 0 To GV_LineaCredito.Rows.Count - 1
                If (nroli = GV_LineaCredito.Rows(I).Cells(1).Text) Then
                    Txt_Pos_Lin.Value = I
                    If (I Mod 2) = 0 Then
                        GV_LineaCredito.Rows(I).CssClass = "selectable"
                    Else
                        GV_LineaCredito.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_LineaCredito.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_LineaCredito.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If
            Next


            'Dim Pos = Val(Txt_Pos_Lin.Value)

            'For i = 0 To GV_LineaCredito.Rows.Count - 1
            '    If i = Pos Then '(Pos - 1) FY 30-04-2012
            '        'Me.GV_LineaCredito.Rows(i).CssClass = "clicktable"
            '        Me.GV_LineaCredito.Rows(i).CssClass = "selectable"
            '        DP_EstadoLinea.CssClass = "clsMandatorio"
            '        DP_EstadoLinea.Enabled = True
            '    Else
            '        Me.GV_LineaCredito.Rows(i).CssClass = "formatable"
            '    End If

            'Next


        Catch ex As Exception

        End Try

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim ClsCli As New ClaseClientes
        Dim RG As New FuncionesGenerales.FComunes
        Dim Sesion As New ClsSession.ClsSession
        Dim Cli As cli_cls



        Try

            Txt_Dig_Cli.Text = RG.Vrut(Replace(Txt_Rut_Cli.Text, ".", ""))

            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)

            If valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                Exit Function
            Else
                If IsNothing(Cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Function
                End If
           
                Me.IB_AyudaCli.Enabled = False

                If Cli.id_P_008 <> 3 Then
                    Me.Txt_Raz_Soc.Text = Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn
                Else
                    Msj.Mensaje(Me.Page, Caption, "Cliente se encuentra deshabilitado", TipoDeMensaje._Exclamacion, "", True)
                    Exit Function
                End If

                If Not IsNothing(Cli.eje_cls) Then
                    Me.Txt_Ejecutivo.Text = Cli.eje_cls.eje_nom.Trim
                Else
                    Me.Txt_Ejecutivo.Text = ""
                End If

                If Not IsNothing(Cli.cli_spr_ead_col) Then
                    hf_spread.Value = Cli.cli_spr_ead_col
                End If

                Me.Txt_Sucursal.Text = Cli.suc_cls.suc_nom
                Sesion.RutCli = Replace(Txt_Rut_Cli.Text, ".", "")

                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True

                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                Me.IB_Comision.Enabled = True
                Me.IB_Gastos.Enabled = True

                Return True

            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub CargaDatosLineaCredito()

        Try

            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim ClsLDC As New ConsultasGenerales

            ClsLDC.LineaDeCreditoDevuelvePorCliente(GV_LineaCredito, Replace(Txt_Rut_Cli.Text, ".", ""), 0, 999999)

            If GV_LineaCredito.Rows.Count < 1 Then
                Msj.Mensaje(Me.Page, Caption, "Cliente no tiene linea de crédito", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'DAMOS FORMATO A LA GRILLA
            Dim I As Integer

            GV_LineaCredito.DataBind()

            For I = 0 To GV_LineaCredito.Rows.Count - 1
                GV_LineaCredito.Rows(I).Cells(9).Text = IIf(GV_LineaCredito.Rows(I).Cells(9).Text = "N", "NORMAL", "ESPECIAL")  'MONTO OCUPADO
            Next

            Txt_Mto_Sol_MaskedEditExtender.Enabled = True
            HabilitaIngreso("clsDisabled", True)
            HabilitaAprobacion("clsDisabled", True)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ActualizaLinea()
        CargaDatosLineaCredito()
    End Sub

    Private Function CargaObjetoLDC() As ldc_cls

        Try

            Dim Sesion As New ClsSession.ClsSession
            Dim FC As New FuncionesGenerales.FComunes
            Dim LDC As New ldc_cls

            LDC.cli_idc = Format(CLng(RG.comasXptos(Txt_Rut_Cli.Text)), Var.FMT_RUT)

            'DATOS DE LINEA
            LDC.id_ldc = Nothing
            LDC.id_P_0029 = DP_EstadoLinea.SelectedValue
            LDC.id_p_0023 = 1

            If Me.Txt_Fec_Sol.Text <> "" Then
                LDC.ldc_fec_sol = Me.Txt_Fec_Sol.Text
            Else
                LDC.ldc_fec_sol = Nothing
            End If

            LDC.ldc_mto_sol = CDbl(Replace(Me.Txt_Mto_Sol.Text, ",", ""))

            If Me.Txt_Por_Exc.Text <> "" Then
                LDC.ldc_por_exc = Txt_Por_Exc.Text
            Else
                LDC.ldc_por_exc = 0
            End If

            'falta monto disponible

            'APROBACION COMITE
            If Me.Txt_Fec_Dsd.Text <> "" Then
                LDC.ldc_fec_vig_dde = Me.Txt_Fec_Dsd.Text
            Else
                LDC.ldc_fec_vig_dde = Nothing
            End If

            If Me.Txt_Fec_Hta.Text <> "" Then
                LDC.ldc_fec_vig_hta = Me.Txt_Fec_Hta.Text
            Else
                LDC.ldc_fec_vig_hta = Nothing
            End If

            If Me.Txt_Fec_Res.Text <> "" Then
                LDC.ldc_fec_rsn = Me.Txt_Fec_Res.Text
            Else
                LDC.ldc_fec_rsn = Nothing
            End If

            LDC.ldc_mto_apb = CDbl(Replace(Me.Txt_Mto_Apr.Text, ",", ""))
            LDC.ldc_spr_col = hf_spread.Value

            'Tipo de Comision
            LDC.ldc_tip_com = CChar(IIf(Me.RB_Especial.Checked, "E", "N"))

            LDC.ldc_adm_mor = "S"
            LDC.ldc_des_com = UCase(Me.Txt_Obs_Com.Text)
            LDC.ldc_obs = UCase(Me.Txt_Observacion.Text)

            Return LDC

        Catch ex As Exception

        End Try

    End Function

    Private Sub LimpiaDetalle()

        Try

            Me.Txt_NroLinea.Text = ""
            Me.Txt_Fec_Sol.Text = ""
            Me.Txt_Mto_Dis.Text = ""
            Me.Txt_Mto_Sol.Text = ""
            Me.Txt_Fec_Dsd.Text = ""
            Me.Txt_Fec_Hta.Text = ""
            Me.Txt_Fec_Res.Text = ""
            Me.Txt_Obs_Com.Text = ""
            Me.Txt_Observacion.Text = ""
            Me.Txt_Mto_Apr.Text = ""

            Me.DP_EstadoLinea.ClearSelection()

            DP_EstadoLinea.CssClass = "clsDisabled"
            Me.Txt_Fec_Sol.CssClass = "clsDisabled"
            Me.Txt_Mto_Sol.CssClass = "clsDisabled"

            Me.DP_EstadoLinea.Enabled = False
            Me.RB_Normal.Enabled = False
            Me.RB_Especial.Enabled = False
            Me.Txt_NroLinea.ReadOnly = True
            Me.Txt_Fec_Sol.ReadOnly = True
            Me.Txt_Mto_Sol.ReadOnly = True

            GV_PorcentajeAnt.Dispose()
            GV_PorcentajeAnt.DataBind()

            GV_Productos.Dispose()
            GV_Productos.DataBind()

            GV_Deudor.Dispose()
            GV_Deudor.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Deshabilita()

        Txt_NroLinea.ReadOnly = True
        Txt_Mto_Sol.ReadOnly = True
        Txt_Mto_Dis.ReadOnly = True
        Txt_Por_Exc.ReadOnly = True
        Txt_Fec_Sol.ReadOnly = True
        DP_EstadoLinea.Enabled = False

        Txt_NroLinea.CssClass = "clsDisabled"
        Txt_Mto_Sol.CssClass = "clsDisabled"
        Txt_Mto_Dis.CssClass = "clsDisabled"
        Txt_Por_Exc.CssClass = "clsDisabled"
        Txt_Fec_Sol.CssClass = "clsDisabled"
        DP_EstadoLinea.CssClass = "clsDisabled"

    End Sub

#End Region

#Region "GRIDVIEW"

    Protected Sub GV_LineaCredito_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_LineaCredito.RowDataBound
    End Sub

    Private Sub CargaDatosAnticipos()

        Try

            Dim ClsAPC As New ConsultasGenerales

            ClsAPC.AnticipoDevuelvePorLinea(True, GV_PorcentajeAnt, NroLinea.Value, NroLinea.Value, 0, 999)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub CargaDatosSubLineas()

        Try

            Dim ClsSBL As New ConsultasGenerales
            Dim Fmt As New FuncionesGenerales.ClsLocateInfo

            ClsSBL.SubLineasDevuelvePorLinea(GV_Productos, NroLinea.Value, TipoSubLinea.TipoDocumento)
            ClsSBL.SubLineasDevuelvePorLinea(GV_Deudor, NroLinea.Value, TipoSubLinea.Deudor)

            For I = 0 To GV_Deudor.Rows.Count - 1
                GV_Deudor.Rows(I).Cells(0).Text = Format(CLng(GV_Deudor.Rows(I).Cells(0).Text), Fmt.FCMSD) & "-" & RG.Vrut(CInt(GV_Deudor.Rows(I).Cells(0).Text))
            Next

        Catch ex As Exception
        End Try

    End Sub

#End Region

#Region "BOTONERAS"

    Protected Sub IB_SubLinea_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_SubLinea.Click
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        If Not agt.ValidaAccesso(20, 20030102, Usr, "PRESIONO GUARDAR LINEA DE FINANCIAMIENTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Not VALIDA_CAMPOS_VACIOS() Then
            Msj.Mensaje(Me.Page, Caption, "¿Está seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)
        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If Not agt.ValidaAccesso(20, 20010102, Usr, "PRESIONO BUSCAR LINEA DE FINANCIAMIENTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.CargaDatosCliente() Then
            CargaDatosLineaCredito()
            IB_Buscar.Enabled = False
        End If

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Limpia()
        CambiaEstilo("clsDisabled")

        IB_Anticipo.Enabled = False
        IB_SubLinea.Enabled = False

        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"

        Txt_Rut_Cli.ReadOnly = False
        Txt_Dig_Cli.ReadOnly = False
        Txt_Rut_Cli.Focus()
        Me.IB_AyudaCli.Enabled = True
        IB_Buscar.Enabled = True
        NroLinea.Value = ""
        Me.IB_Comision.Enabled = False
        Me.IB_Gastos.Enabled = False
        Me.IB_Ficha.Enabled = False
        Me.IB_Actas.Enabled = False
        Me.IB_Pagare.Enabled = False
        Me.IB_HFirma.Enabled = False
       
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click 'Handles IB_Nuevo.Click 'Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20020102, Usr, "PRESIONO NUEVA LINEA DE FINANCIAMIENTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If Txt_Rut_Cli.Text = "" Then
            Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Digito del Cliente a Buscar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If

        Accion.Value = "NUEVO"
        Nuevo()

    End Sub

    Protected Sub IB_VistoBueno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_VistoBueno.Click

        If Not agt.ValidaAccesso(20, 20010203, Usr, "PRESIONO VB LINEA DE FINANCIAMIENTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Not VALIDA_CAMPOS_VACIOS() Then

            '-------------------------------------------------------------------------
            'valida que no tenga un linea vigente
            '-------------------------------------------------------------------------
            Dim CG As New ConsultasGenerales

            If Not IsNothing(CG.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text, 1)) Then
                Msj.Mensaje(Me.Page, Caption, "¿Está seguro de aprobar, si tiene una línea vigente pasara a caducar para dejar esta nueva línea de financiamiento?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Aprobar.UniqueID)
            Else
                Msj.Mensaje(Me.Page, Caption, "¿Está seguro de aprobar la línea de financiamiento?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Aprobar.UniqueID)
            End If

        End If

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

        If Me.Txt_NroLinea.Text = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una Negociación, para ver su informe", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "reporte.aspx?rut=" & Txt_Rut_Cli.Text & "&ID= " & Txt_NroLinea.Text, "ReporteLinea", 1000, 700, 15, 15)
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim ClsLDC As New ActualizacionesGenerales
            Dim LDC As ldc_cls
            Dim RutCli As Long
            Dim coll As New Collection
            Dim I As Integer


            RutCli = CLng(RG.comasXptos(Txt_Rut_Cli.Text))
            LDC = CargaObjetoLDC()

            'si tiene alguna linea selecionada
            If NroLinea.Value <> "" Then

                LDC.id_ldc = Txt_NroLinea.Text

                Dim Tipo As Int16 = Val(Me.Request.QueryString("Tipo"))

                Select Case Tipo
                    Case 1
                        If ClsLDC.LineaDeCreditoUpdate(LDC) Then
                            Msj.Mensaje(Me.Page, Caption, "Línea de Crédito Modificada", TipoDeMensaje._Informacion)
                            CargaDatosLineaCredito()
                            Deshabilita()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Modificar Línea de Crédito", TipoDeMensaje._Informacion)
                        End If
                    Case 2
                        If ClsLDC.LineaDeCreditoVB(LDC) Then
                            Msj.Mensaje(Me.Page, Caption, "Línea de Crédito Aprobado", TipoDeMensaje._Informacion)
                            CargaDatosLineaCredito()
                            Deshabilita()
                        Else
                            Msj.Mensaje(Me.Page, Caption, "No se pudo Aprobado Línea de Crédito", TipoDeMensaje._Informacion)
                        End If
                End Select

            Else

                If Accion.Value = "NUEVO" Then

                    '*********************************************************************************************************
                    'Recorre la grilla y verifica si linea es vigente, si es vigente no permite ingresar otra linea 
                    '*********************************************************************************************************

                    Dim sw As Integer = 0
                    For I = 0 To GV_LineaCredito.Rows.Count - 1
                        If GV_LineaCredito.Rows(I).Cells(1).Text = "VIGENTE" Then
                            sw = 1
                        End If
                    Next


                    If sw = 1 Then
                        Exit Sub
                    End If

                    '*********************************************************************************************************

                    'nueva linea
                    LDC.ldc_mto_ocp = 0
                    LDC.id_P_0029 = 4

                    If ClsLDC.LineaDeCreditoInserta(LDC) Then
                        Msj.Mensaje(Me.Page, Caption, "Línea de Crédito Ingresada", TipoDeMensaje._Informacion)
                        CargaDatosLineaCredito()
                        'Nuevo()
                        Deshabilita()
                        NroLinea.Value = ""
                    Else
                        Msj.Mensaje(Me.Page, Caption, "No se pudo Grabar Línea de Credito", TipoDeMensaje._Informacion)
                    End If

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Comision_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Comision.Click
        If Txt_Rut_Cli.Text = "" Then
            Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Digito del Cliente a Buscar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub IB_Gastos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Gastos.Click, IB_Anticipo.Click
        If Txt_Rut_Cli.Text = "" Then
            Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Digito del Cliente a Buscar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        End If
    End Sub



    Protected Sub LB_Aprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Aprobar.Click

        Try

            Dim ClsLDC As New ActualizacionesGenerales
            Dim LDC As ldc_cls

            LDC = CargaObjetoLDC()
            LDC.id_ldc = Txt_NroLinea.Text
            LDC.id_P_0029 = 1

            If ClsLDC.LineaDeCreditoVB(LDC) Then
                Msj.Mensaje(Me, Caption, "Linea de Crédito Aprobada", TipoDeMensaje._Informacion)
                CargaDatosLineaCredito()
                Deshabilita()
                NroLinea.Value = ""
            Else
                Msj.Mensaje(Me, Caption, "Linea de Crédito No Se Pudo Aprobada", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'BOTON_VER_DETALLE

        Dim btn As ImageButton = CType(sender, ImageButton)

        If btn.ToolTip <> "" Then

            Me.NroLinea.Value = ""
            MarcaGrillaNeg(btn.ToolTip)
            Me.NroLinea.Value = btn.ToolTip

            Dim ClsLdc As New ConsultasGenerales
            Dim sesion As New ClsSession.ClsSession
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim LDC As Object

            IB_Anticipo.Enabled = True
            IB_SubLinea.Enabled = True
            IB_Imprimir.Enabled = True
            IB_Guardar.Enabled = True

            sesion.NroLineaCredito = btn.ToolTip 'NroLinea.Value

            LDC = ClsLdc.LineaDeCreditoDevuelvePorNro(sesion.RutCli, btn.ToolTip) 'Me.NroLinea.Value

            If LDC.ldc_est = 2 And Val(Me.Request.QueryString("Tipo")) = 2 Then
                Msj.Mensaje(Me.Page, "Linea", "Línea de Financiamiento esta caducada, no se puede modificar.", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            For i = 0 To GV_LineaCredito.Rows.Count - 1
                If GV_LineaCredito.Rows(i).Cells(1).Text = Me.NroLinea.Value Then
                    If GV_LineaCredito.Rows(i).Cells(2).Text = "VIGENTE" Then
                        Select Case Val(Me.Request.QueryString("Tipo"))
                            Case 1 : HabilitaIngreso("clsDisabled", True)
                            Case 2 : HabilitaAprobacion("clsDisabled", True)
                            Case Else : HabilitaIngreso("clsMandatorio", False)
                        End Select
                    Else
                        Select Case Val(Me.Request.QueryString("Tipo"))
                            Case 1 : HabilitaIngreso("clsMandatorio", False)
                            Case 2 : HabilitaAprobacion("clsMandatorio", False)
                            Case Else : HabilitaIngreso("clsMandatorio", False)
                        End Select
                    End If
                End If
            Next

            With LDC

                Me.Txt_NroLinea.Text = .id_ldc
                Me.DP_EstadoLinea.ClearSelection()
                Me.DP_EstadoLinea.Items.FindByValue(.ldc_est).Selected = True
                Me.Txt_Mto_Dis.Text = Format((.ldc_mto_apb - .ldc_mto_ocp), FMT.FCMSD)
                Me.Txt_Fec_Sol.Text = .ldc_fec_sol
                Me.Txt_Mto_Sol.Text = Format(.ldc_mto_sol, FMT.FCMSD)

                sesion.MtoLineaCredito = Me.Txt_Mto_Sol.Text

                Me.Txt_Fec_Dsd.Text = If(.ldc_fec_vig_dde = Nothing, FC.FUNFecReg(Date.Now.ToShortDateString), .ldc_fec_vig_dde)
                Me.Txt_Fec_Hta.Text = If(.ldc_fec_vig_hta = Nothing, "", .ldc_fec_vig_hta)
                Me.Txt_Fec_Res.Text = If(.ldc_fec_rsn = Nothing, FC.FUNFecReg(Date.Now.ToShortDateString), .ldc_fec_rsn)
                Me.Txt_Mto_Apr.Text = Format(If(.ldc_mto_apb = Nothing, 0, .ldc_mto_apb), FMT.FCMSD)
                Me.Txt_Por_Exc.Text = Format(If(.ldc_por_exc = Nothing, 0, .ldc_por_exc), FMT.FCMCD)

                sesion.MtoLineaCreditoApr = Me.Txt_Mto_Apr.Text

                Select Case .ldc_tip_com
                    Case "N" : Me.RB_Normal.Checked = True : Me.RB_Especial.Checked = False : Me.Txt_Obs_Com.Text = .ldc_des_com
                    Case "E" : Me.RB_Especial.Checked = True : Me.RB_Normal.Checked = False : Me.Txt_Obs_Com.Text = ""
                End Select

                Me.Txt_Observacion.Text = .ldc_obs

                'vez = 1
                CargaDatosAnticipos()

                'vez = 2
                CargaDatosSubLineas()

                If .ldc_est <> 1 Then
                    Me.IB_Anticipo.Enabled = False
                    Me.IB_SubLinea.Enabled = False
                    IB_Ficha.Enabled = False
                    IB_Actas.Enabled = False
                    IB_Pagare.Enabled = False
                    IB_HFirma.Enabled = False
                    IB_Reglamento.Enabled = False
                Else
                    IB_Reglamento.Enabled = True
                    IB_Ficha.Enabled = True
                    IB_Actas.Enabled = True
                    IB_Pagare.Enabled = True
                    IB_HFirma.Enabled = True
                    IB_Guardar.Enabled = False
                End If

            End With

        Else
            LimpiaDetalle()
        End If

        If GV_PorcentajeAnt.Rows.Count = 0 And GV_Productos.Rows.Count = 0 And GV_Deudor.Rows.Count = 0 Then
            Msj.Mensaje(Me.Page, "Linea", "No existen Condiciones de Línea", TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub IB_Actas_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Actas.Click
        If DP_EstadoLinea.SelectedValue = 1 Then
            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Pop_up_Actas.aspx?ID=" & Txt_NroLinea.Text, "ActasPorLinea", 650, 350, 15, 15)
        Else
            Msj.Mensaje(Page, "Adjuntar Actas", "No es posible adjuntar actas", TipoDeMensaje._Exclamacion)
        End If
    End Sub

    Protected Sub IB_Ficha_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Ficha.Click
        If DP_EstadoLinea.SelectedValue = 1 Then
            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Pop_up_Actas.aspx?ID=" & Txt_NroLinea.Text, "ActasPorLinea", 650, 350, 15, 15)
        Else
            Msj.Mensaje(Page, "Ficha Juridica", "No es posible Fichas", TipoDeMensaje._Exclamacion)
        End If
    End Sub

    Protected Sub IB_Pagare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Pagare.Click

        Try
            Dim pdf As New clsPDF()
            Dim nomb_arch As String = "FC_02_" & DateTime.Now.ToString("yyyyMMddhhmmss") & ".pdf"
            Dim archivo As String = Server.MapPath("..\Pagare\" & nomb_arch)
            Dim imagen As String = Server.MapPath("..\..\..\Imagenes\Logo_BBVA.GIF")
            Dim _ctype As String = "application/pdf"
            Dim ClsCli As New ClaseClientes
            Dim Cli As cli_cls
            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)

            Dim PagareBlanco1 As New PagareBlanco

            PagareBlanco1.rut_deudor = RG.FormatoMiles(CInt(Cli.cli_idc.Trim)) & "-" & Cli.cli_dig_ito
            PagareBlanco1.rsoc_deudor = Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn & "" & Cli.cli_rso

            If (Cli.id_P_0044 = 1) Then
                PagareBlanco1.tip_ident = "cedula de ciudadania"
            Else
                PagareBlanco1.tip_ident = "numero de identificacion tributaria (NIT)"
            End If

            pdf.GeneraPagare(PagareBlanco1, archivo, imagen)

            If (File.Exists(archivo)) Then

                Response.Buffer = False
                Response.ContentType = "application/octet-stream"
                Response.Expires = -1
                Response.AddHeader("Content-Type", _ctype)
                Response.AddHeader("Content-Disposition", "attachment; filename=" & nomb_arch)
                Response.Clear()
                Response.WriteFile(archivo)
                File.Delete(archivo)
                Response.End()

            End If



        Catch ex As Exception
            Msj.Mensaje(Page, "Error ", "Error: " & ex.ToString(), TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Reglamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Reglamento.Click

        Try

            Dim pdf As New clsPDF()
            Dim nomb_arch As String = "REGLAMENTO FACTORING" & DateTime.Now.ToString("yyyyMMddhhmmss") & ".pdf"
            Dim archivo As String = Server.MapPath("..\Pagare\" & nomb_arch)
            Dim _ctype As String = "application/pdf"
            Dim ClsCli As New ClaseClientes
            Dim Cli As cli_cls


            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)

            Dim reglamento As New Reglamento

            reglamento.rut_cliente = RG.FormatoMiles(CInt(Cli.cli_idc.Trim)) & "-" & Cli.cli_dig_ito
            reglamento.razon_cliente = Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn & "" & Cli.cli_rso

            Dim representante As String
            Dim cnc As cnc_cls = ClsCli.ContactosDevuelve(Cli.cli_idc)

            If Not IsNothing(cnc) Then
                representante = cnc.cnc_nom
            End If

            reglamento.representante = "Representante Legal" + representante


            pdf.GeneraReglamento(reglamento, archivo)

            If (File.Exists(archivo)) Then

                Response.Buffer = False
                Response.ContentType = "application/octet-stream"
                Response.Expires = -1
                Response.AddHeader("Content-Type", _ctype)
                Response.AddHeader("Content-Disposition", "attachment; filename=" & nomb_arch)
                Response.Clear()
                Response.WriteFile(archivo)
                File.Delete(archivo)
                Response.End()

            End If



        Catch ex As Exception
            Msj.Mensaje(Page, "Error ", "Error: " & ex.ToString(), TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Anticipo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Anticipos.aspx?ID=" & Txt_NroLinea.Text, "ActasPorLinea", 650, 350, 15, 15)
    End Sub
    '  Protected Sub IB_Anticipo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Anticipo_Click

    'End Sub

#End Region

#Region "CERRAR MODAL"

    Protected Sub IB_CloseModal_SubLinea_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'ModalPopupExtender1.Controls.Clear()
    End Sub

    Protected Sub IB_CloseAnticipo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'ModalPopupExtender2.Controls.Clear()
    End Sub

#End Region

#Region "SUB Y FUNCTION GENERALES"

    Private Sub AlineaMonto()

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Sol.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Dis.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Apr.Attributes.Add("Style", "TEXT-ALIGN: right")

    End Sub

    Private Sub CargaDrop()

        Try

            Dim Drop As New ConsultasGenerales
            Drop.ParametrosDevuelve(TablaParametro.EstadoLinea, True, Me.DP_EstadoLinea)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Nuevo()

        Dim FC As New FuncionesGenerales.FComunes

        Me.Txt_NroLinea.Text = 0
        NroLinea.Value = ""

        Dim CG As New ConsultasGenerales

        Txt_Fec_Sol_CalendarExtender.Enabled = True
        Txt_Fec_Sol.ReadOnly = False
        Txt_Mto_Sol.ReadOnly = False

        Txt_Fec_Sol.CssClass = "clsMandatorio"
        Txt_Fec_Sol.CssClass = "clsMandatorio"
        Txt_Mto_Sol.CssClass = "clsMandatorio"

        Me.Txt_Pos_Lin.Value = ""
        Me.Txt_Fec_Sol.Text = ""
        Me.Txt_Mto_Dis.Text = ""
        Me.Txt_Mto_Sol.Text = ""

        Me.DP_EstadoLinea.ClearSelection()
        DP_EstadoLinea.SelectedValue = 4
        Txt_Fec_Sol.Text = FC.FUNFecReg(Date.Now.ToShortDateString)

        Me.Txt_Fec_Dsd.Text = ""
        Me.Txt_Fec_Hta.Text = ""
        Me.Txt_Fec_Res.Text = ""

        Me.Txt_Mto_Apr.Text = ""
        Me.Txt_Por_Exc.Text = ""
        Txt_Rut_Cli.Focus()

        GV_PorcentajeAnt.DataSource = Nothing
        GV_PorcentajeAnt.DataBind()

        GV_Productos.DataSource = Nothing
        GV_Productos.DataBind()

        GV_Deudor.DataSource = Nothing
        GV_Deudor.DataBind()

        IB_Guardar.Enabled = True

        Txt_Mto_Sol_MaskedEditExtender.Enabled = True

    End Sub

    Private Sub CambiaEstilo(ByVal Estilo As String)

        Me.Txt_Fec_Sol.CssClass = Estilo
        Me.Txt_Mto_Dis.CssClass = Estilo
        Me.Txt_Por_Exc.CssClass = Estilo
        Me.Txt_Mto_Sol.CssClass = Estilo
        Me.Txt_Fec_Dsd.CssClass = Estilo
        Me.Txt_Fec_Hta.CssClass = Estilo
        Me.Txt_Fec_Res.CssClass = Estilo

        Me.Txt_Mto_Apr.CssClass = Estilo
        'Me.DP_EstadoLinea.CssClass = Estilo
        Me.Txt_Obs_Com.CssClass = Estilo
        Me.Txt_Observacion.CssClass = Estilo



    End Sub

    Private Sub Limpia()
        Try

            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Raz_Soc.Text = ""
            Me.Txt_Ejecutivo.Text = ""
            Me.Txt_Sucursal.Text = ""
            'Me.Txt_EjeBanco.Text = ""

            Me.Txt_Pos_Lin.Value = ""

            Me.Txt_NroLinea.Text = ""
            Me.Txt_Fec_Sol.Text = ""
            Me.Txt_Mto_Dis.Text = ""
            Me.Txt_Mto_Sol.Text = ""

            Me.Txt_Por_Exc.Text = ""

            Me.DP_EstadoLinea.ClearSelection()


            Me.Txt_Fec_Dsd.Text = ""
            Me.Txt_Fec_Hta.Text = ""
            Me.Txt_Fec_Res.Text = ""

            Me.Txt_Mto_Apr.Text = ""

            Me.RB_Normal.Enabled = False
            Me.RB_Especial.Enabled = False

            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Me.Txt_NroLinea.ReadOnly = True
            Me.Txt_Fec_Sol.ReadOnly = True
            Me.Txt_Mto_Sol.ReadOnly = True
            Me.Txt_Por_Exc.ReadOnly = True
            'Me.DP_EstadoLinea.Enabled = False
            Me.Txt_Obs_Com.Text = ""
            Me.Txt_Observacion.Text = ""

            GV_LineaCredito.DataSource = Nothing
            GV_LineaCredito.DataBind()

            GV_PorcentajeAnt.DataSource = Nothing
            GV_PorcentajeAnt.DataBind()

            GV_Productos.DataSource = Nothing
            GV_Productos.DataBind()

            GV_Deudor.DataSource = Nothing
            GV_Deudor.DataBind()
            Txt_Mto_Sol_MaskedEditExtender.Enabled = False

        Catch ex As Exception

        End Try


    End Sub

    Public Function VALIDA_CAMPOS_VACIOS() As Boolean

        VALIDA_CAMPOS_VACIOS = False

        'rut cliente
        If Trim(Me.Txt_Rut_Cli.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Seleccione Cliente", TipoDeMensaje._Exclamacion)
            Txt_Rut_Cli.Focus()
            Return True
        End If

        'digito verificador
        If Trim(Txt_Dig_Cli.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Seleccione Cliente", TipoDeMensaje._Exclamacion)
            Txt_Dig_Cli.Focus()
            Return True
        End If

        'linea de credito
        If Trim(Me.Txt_NroLinea.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Línea de Crédito", TipoDeMensaje._Exclamacion)
            Return True
        End If

        'Fecha Solicitud
        If Trim(Me.Txt_Fec_Sol.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Fecha de Solicitud", TipoDeMensaje._Exclamacion)
            Txt_Fec_Sol.Focus()
            Return True
        End If

        'monto solicitado
        If Trim(Me.Txt_Mto_Sol.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Monto Solicitado", TipoDeMensaje._Exclamacion)
            Txt_Mto_Sol.Focus()
            Return True
        End If

        'Porcentaje de exceso permitido
        If CInt(IIf(Trim(Me.Txt_Por_Exc.Text) = "", 0, Trim(Me.Txt_Por_Exc.Text))) > 100 Then
            Msj.Mensaje(Me.Page, Caption, "Porcentaje de exceso debe ser menor o igual a 100", TipoDeMensaje._Exclamacion)
            Txt_Por_Exc.Focus()
            Return True
        End If

        If Accion.Value = "NUEVO" Or Accion.Value = "MODIFICAR" Then Exit Function
        If Titulo.Text = "Ingreso Linea de Credito" Then Exit Function

        'Fecha desde
        If Trim(Me.Txt_Fec_Dsd.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Fecha Desde", TipoDeMensaje._Exclamacion)
            Txt_Fec_Dsd.Focus()
            Return True
        Else
            If Not IsDate(Txt_Fec_Dsd.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha desde erronea", TipoDeMensaje._Exclamacion)
                Txt_Fec_Dsd.Text = ""
                'Txt_Fec_Dsd.Focus()
                Return True
            End If

            'Fecha Hasta
            If Trim(Txt_Fec_Hta.Text) = "" Then
                If Me.DP_EstadoLinea.SelectedValue = 1 Then
                    Msj.Mensaje(Me.Page, Caption, "No puede asignar estado VIGENTE a línea sin ingresar fechas de vigencia", TipoDeMensaje._Exclamacion)
                    'Txt_Fec_Hta.Focus()
                    Return True
                End If
            End If
        End If

        'Fecha Hasta
        If Trim(Me.Txt_Fec_Hta.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese fecha hasta", TipoDeMensaje._Exclamacion)

            Return True
        Else
            If Not IsDate(Txt_Fec_Hta.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha hasta erronea", TipoDeMensaje._Exclamacion)
                Txt_Fec_Hta.Text = ""
                Return True
            End If
        End If

        If Trim(Me.Txt_Fec_Dsd.Text) <> "" Then
            'Fecha de resolución
            If Trim(Me.Txt_Fec_Res.Text) <> "" Then
                If Not IsDate(Txt_Fec_Res.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha resolución erronea", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Res.Text = ""
                    Return True
                End If

                'Fecha desde >= Fecha de resolución
                If CDate(Me.Txt_Fec_Dsd.Text) > CDate(Me.Txt_Fec_Res.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha desde debe ser menor o igual a la fecha resolución", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Dsd.Focus()
                    Return True
                End If
            Else
                Msj.Mensaje(Me.Page, Caption, "Ingrese fecha resolución", TipoDeMensaje._Exclamacion)
                Txt_Fec_Res.Focus()
                Return True
            End If
        End If

        'Fecha de Desde
        If Trim(Me.Txt_Fec_Dsd.Text) <> "" Then

            'Fecha de hasta
            If Trim(Me.Txt_Fec_Hta.Text) <> "" Then
               
                '        fecha desde         fecha solicitud        
                If CDate(Me.Txt_Fec_Dsd.Text) < CDate(Me.Txt_Fec_Sol.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "No puede asignar estado VIGENTE a línea." & Chr(13) & "Fecha Desde debe ser mayor a fecha solicitada", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Dsd.Focus()
                    Return True
                End If

                If CDate(Me.Txt_Fec_Hta.Text) <= CDate(Me.Txt_Fec_Dsd.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "No puede asignar estado VIGENTE a línea." & Chr(13) & "Fecha hasta es menor o igual a fecha desde", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Hta.Focus()
                    Return True
                End If
                'End If
            End If
        End If

        'Fecha de resolución
        If Trim(Me.Txt_Fec_Res.Text) <> "" Then
            If Trim(Me.Txt_Fec_Sol.Text) <> "" Then
                'Fecha solicitud < Fecha de resolución
                If CDate(Txt_Fec_Sol.Text) > CDate(Txt_Fec_Res.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha Solicitud debe ser Menor o Igual a la Fecha Resolución", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Res.Focus()
                    Return True
                End If
            End If
        End If

        If Txt_Mto_Apr.Text = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto aprobado", TipoDeMensaje._Exclamacion)
            Return True
        End If

        If Txt_Mto_Apr.Text = 0 Then
            Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto aprobado", TipoDeMensaje._Exclamacion)
            Return True
        End If

        If Me.Txt_Obs_Com.Text = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe ingresar Observación de Comisión ", TipoDeMensaje._Exclamacion)
            Return True
        End If

        If Me.Txt_Observacion.Text = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe ingresar Observación ", TipoDeMensaje._Exclamacion)
            Return True
        End If

    End Function

    Private Function ValidaEstadoLinea(ByVal Forma As String) As Boolean
        Select Case Me.DP_EstadoLinea.SelectedValue
            Case 2
                Msj.Mensaje(Me.Page, Caption, "No Puede Crear " & Forma & " en una Línea Caducada  ", TipoDeMensaje._Exclamacion)
                Return False
            Case 3
                Msj.Mensaje(Me.Page, Caption, "No Puede Crear " & Forma & " en una Línea Bloqueda  ", TipoDeMensaje._Exclamacion)
                Return False
            Case 4
                Msj.Mensaje(Me.Page, Caption, "No Puede Crear " & Forma & " en una Línea Solicitada  ", TipoDeMensaje._Exclamacion)
                Return False
        End Select

        Return True

    End Function

    Private Sub HabilitaAprobacion(ByVal Estilo As String, ByVal Estado As Boolean)

        Me.Txt_Fec_Dsd.CssClass = Estilo
        Me.Txt_Fec_Hta.CssClass = Estilo
        Me.Txt_Fec_Res.CssClass = Estilo
        Me.Txt_Mto_Apr.CssClass = Estilo

        Me.Txt_Por_Exc.CssClass = Estilo



        Me.Txt_Obs_Com.CssClass = Estilo
        Me.Txt_Observacion.CssClass = Estilo


        Me.Txt_Fec_Dsd.ReadOnly = Estado
        Me.Txt_Fec_Hta.ReadOnly = Estado
        Me.Txt_Fec_Res.ReadOnly = Estado
        Me.Txt_Mto_Apr.ReadOnly = Estado
        Me.Txt_Por_Exc.ReadOnly = Estado
        Me.Txt_Obs_Com.ReadOnly = Estado
        Me.Txt_Observacion.ReadOnly = Estado
        Me.RB_Especial.Enabled = Not Estado
        Me.RB_Normal.Enabled = Not Estado
        Me.RB_Especial.Checked = True
        Me.RB_Normal.Checked = False

        Txt_Fec_Hta_CalendarExtender.Enabled = True
        Txt_Fec_Res_CalendarExtender.Enabled = True
        Txt_Fec_Dsd_CalendarExtender.Enabled = True
        Txt_Fec_Sol_CalendarExtender.Enabled = False


    End Sub

    Private Sub HabilitaIngreso(ByVal Estilo As String, ByVal Estado As Boolean)

        Me.Txt_Fec_Sol.CssClass = Estilo
        Me.Txt_Mto_Sol.CssClass = Estilo
        
        Me.Txt_Fec_Sol.ReadOnly = Estado
        Me.Txt_Mto_Sol.ReadOnly = Estado
        
        Txt_Fec_Sol_CalendarExtender.Enabled = True
        Txt_Fec_Hta_CalendarExtender.Enabled = False
        Txt_Fec_Res_CalendarExtender.Enabled = False
        Txt_Fec_Dsd_CalendarExtender.Enabled = False
        Txt_Fec_Sol_CalendarExtender.Enabled = False

    End Sub

    Private Sub formatea_grilla(ByVal GV As GridView) 'FY 23-05-2012
        Dim fmt As New FuncionesGenerales.ClsLocateInfo

        For I = 0 To GV.Rows.Count - 1
            Select Case GV.ID
                Case "GV_Deudor"
                    GV.Rows(I).Cells(0).Text = Format(CLng(GV.Rows(I).Cells(0).Text), Fmt.FCMSD) & "-" & RG.Vrut(CInt(GV.Rows(I).Cells(0).Text))
                    GV.Rows(I).Cells(2).Text = Format(CLng(GV.Rows(I).Cells(2).Text), fmt.FCMSD)
                Case "GV_Productos"
                    GV.Rows(I).Cells(1).Text = Format(CLng(GV.Rows(I).Cells(1).Text), fmt.FCMSD)
            End Select
        Next

    End Sub
#End Region

   
    

End Class
