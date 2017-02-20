Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Public Class PizarraOperaciones
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
   
    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra de Otorgamiento"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim clasecli As New ClaseClientes
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial
    Dim CD As New ClaseControlDual
    Dim CA As New ClaseArchivos

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

                NroPaginacion = 0
                CargaDrop_Form()
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

                'Muestra por defecto fecha actual
                Txt_Fec_Dsd.Text = Format(CDate("01" & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")
                Txt_Fec_Hst.Text = Format(CDate(Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")

                Txt_Rut_Cli.Focus()
                HabilitaCamposBusqueda(True)
                HabilitaCamposBusqueda(True)

                NroOperacion = 0
                NroNegociacion = 0
                RutCli = 0

                Dim evento As ImageClickEventArgs

                IB_Buscar_Click(Nothing, evento)

            Else

                If HF_NroNNC.Value <> "" Then
                    CD.NegociacionFirmasDevuelve(HF_NroNNC.Value, Sucursal, Table_Firmas)
                    Table_Firmas.DataBind()
                End If

            End If

            IB_Evaluacion.Attributes.Add("onClick", "javascript:VerEvaluacion();")
            IB_Negociacion.Attributes.Add("onClick", "javascript:VerNegociacion();")
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_BuscarCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarCliente.Click
        Try
            If CargaDatosCliente() Then
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Ope_Ope_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Ope_Ope.RowDataBound
    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            If HF_NroOpe.Value.Length > 0 And HF_NroNeg.Value.Length > 0 Then

                IB_Otorgar.Enabled = True
                MarcaGrillaOperaciones()

                CD.NegociacionClasificacionDevuelve(HF_NroNeg.Value, GV_Clasificacion)

                Dim CLI As cli_cls
                Dim RSC As Object
                Dim LDC As ldc_cls
                Dim NEG As opn_cls
                Dim OPE As ope_cls
                Dim APC As Object
                Dim Coll_Pgr As Collection

                NroNegociacion = HF_NroNeg.Value
                NroOperacion = HF_NroOpe.Value

                'RutCli = Txt_Rut_Cli.Text.Replace(".", "").Trim

                RutCli = RG.LimpiaRut(HF_RutCli.Value)

                'RutCli = Txt_Rut_Cli.Text.Replace(".", "").Trim
                CLI = clasecli.ClientesDevuelve(RutCli, RG.Vrut(RutCli))

                If valida_cliente <> "" Then
                    Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                    IB_Otorgar.Enabled = False
                Else

                    Session("Cliente") = CLI

                    'Cargamos los objetos que se utilizaran en caso de que esten ya hayan sido cargados los rescatamos
                    LDC = CG.LineaDeCreditoDevuelve(RutCli, 1)
                    If Not IsNothing(LDC) Then
                        LineaCredito = LDC
                        NroLineaCredito = LDC.id_ldc
                        APC = CG.AnticipoDevuelvePorLinea(False, Nothing, LDC.id_ldc, LDC.id_ldc, 1, 1)
                    End If

                    RSC = CMC.ResumenClienteDevuelve(RutCli, CodEje)
                    If Not IsNothing(RSC) Then
                        ResumenCliente = RSC
                    End If

                    If Not IsNothing(APC) Then
                        Anticipos = APC
                    End If

                    NEG = CMC.NegociacionDevuelve(RutCli, NroNegociacion)
                    If Not IsNothing(NEG) Then
                        Negociacion = NEG
                    End If

                    OPE = OP.OperacionDevuelve(RutCli, NroOperacion)
                    If Not IsNothing(OPE) Then
                        Operacion = OPE
                        NroOperacion = OPE.id_ope
                    End If

                    HF_NroEva.Value = NEG.id_eva


                    'Lb_buscar_Click(Me.Lb_buscar_ccf, e)

                    IB_Otorgar.Enabled = True

                    'Msj.Mensaje(Me.Page,Caption, "Se cargo el Detalle de la Operacion", TipoDeMensaje._Informacion)
                    Dim Mensaje_Clasificacion As String

                    Mensaje_Clasificacion = ""

                    If GV_Clasificacion.Rows.Count > 1 Then
                        Mensaje_Clasificacion = "Esta operación esta en " & GV_Clasificacion.Rows.Count & " clasificaciones"
                    ElseIf GV_Clasificacion.Rows.Count = 1 Then
                        Mensaje_Clasificacion = "Esta operación esta en " & GV_Clasificacion.Rows.Count & " clasificación"
                    ElseIf GV_Clasificacion.Rows.Count = 0 Then
                        Mensaje_Clasificacion = "Esta operación no tiene clasificaciones"
                    End If


                End If

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Clasificacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clasificacion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim img As Image
                img = CType(e.Row.FindControl("Image1"), Image)
                img.Attributes.Add("onMouseOver", "showClasificacion(event,'" & img.ToolTip & "')")
                img.Attributes.Add("onMouseOut", "hideTooltip(event)")
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Not IsPostBack Then

                Modulo = "Operacion"

                'Esto de abajo es para los skins
                Pagina = Page.AppRelativeVirtualPath

                CambioTema(Page)

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If CargaDatosCliente() Then
                Txt_Fec_Dsd.Focus()
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Lb_MJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_MJ.Click
        Try
            If HF_NroEva.Value = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione una operación", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Clf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Clf.Click
        If GV_Clasificacion.Rows.Count = 0 Then
            Exit Sub
        End If

        If NroPaginacion_Claf = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion_Claf >= 6 Then
            NroPaginacion_Claf -= 6
            Lb_buscar_Click(Me, e)

        End If

    End Sub

    Protected Sub IB_Next_Clf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Clf.Click
        If GV_Clasificacion.Rows.Count = 0 Then
            Exit Sub
        End If

        If GV_Clasificacion.Rows.Count < 6 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Clasificacion.Rows.Count = 6 Then
            NroPaginacion_Claf += 6
            Lb_buscar_Click(Me, e)

        End If

    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img_ver As ImageButton = CType(sender, ImageButton)

        HF_NroOpe.Value = img_ver.ToolTip
        HF_NroOpn.Value = img_ver.AlternateText.ToString()
        HF_NroNNC.Value = ""

        Try

            IB_Otorgar.Enabled = True
            NroOperacion = HF_NroOpe.Value

            MarcaGrillaOperaciones()

            CD.NegociacionClasificacionDevuelve(HF_NroNeg.Value, GV_Clasificacion)

            Table_Firmas.Rows.Clear()
            Table_Firmas.DataBind()

            Dim CLI As cli_cls
            Dim RSC As Object
            Dim LDC As ldc_cls
            Dim NEG As opn_cls
            Dim OPE As ope_cls
            Dim APC As Object
            Dim SBL_DEU As Object
            Dim SBL_PRO As Object

            NroNegociacion = HF_NroNeg.Value
            RutCli = HF_RutCli.Value

            CLI = clasecli.ClientesDevuelve(RutCli, RG.Vrut(RutCli))

            If valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                IB_Otorgar.Enabled = False
            Else

                Session("Cliente") = CLI

                'Cargamos los objetos que se utilizaran en caso de que esten ya hayan sido cargados los rescatamos

                LDC = CG.LineaDeCreditoDevuelve(RutCli, 1)
                If Not IsNothing(LDC) Then
                    LineaCredito = LDC
                    NroLineaCredito = LDC.id_ldc
                    APC = CG.AnticipoDevuelvePorLinea(False, Nothing, LDC.id_ldc, LDC.id_ldc, 1, 999)
                    SBL_PRO = CG.SubLineasDevuelvePorLinea(Nothing, LDC.id_ldc, TipoSubLinea.TipoDocumento)
                    SBL_DEU = CG.SubLineasDevuelvePorLinea(Nothing, LDC.id_ldc, TipoSubLinea.Deudor)
                End If

                RSC = CMC.ResumenClienteDevuelve(RutCli, CodEje)
                If Not IsNothing(RSC) Then
                    ResumenCliente = RSC
                End If

                If Not IsNothing(APC) Then
                    Anticipos = APC
                End If

                If Not IsNothing(SBL_PRO) Then
                    SubLineaProducto = SBL_PRO
                End If

                If Not IsNothing(SBL_DEU) Then
                    SubLineaDeudor = SBL_DEU
                End If


                NEG = CMC.NegociacionDevuelve(RutCli, NroNegociacion)
                If Not IsNothing(NEG) Then
                    Negociacion = NEG
                End If

                OPE = OP.OperacionDevuelve(RutCli, NroOperacion)
                If Not IsNothing(OPE) Then
                    Operacion = OPE
                    NroOperacion = OPE.id_ope
                End If

                HF_NroEva.Value = NEG.id_eva

                IB_Otorgar.Enabled = True

                Dim Mensaje_Clasificacion As String

                Mensaje_Clasificacion = ""

                If GV_Clasificacion.Rows.Count > 1 Then
                    Mensaje_Clasificacion = "Esta operación esta en " & GV_Clasificacion.Rows.Count & " clasificaciones"
                ElseIf GV_Clasificacion.Rows.Count = 1 Then
                    Mensaje_Clasificacion = "Esta operación esta en " & GV_Clasificacion.Rows.Count & " clasificación"
                ElseIf GV_Clasificacion.Rows.Count = 0 Then
                    Mensaje_Clasificacion = "Esta operación no tiene clasificaciones"
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

        UpdatePanel1.Update()

    End Sub

    Protected Sub Img_Ver1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img_ver As ImageButton = CType(sender, ImageButton)

        Table_Firmas.Controls.Clear()

        HF_NroCCF.Value = img_ver.ToolTip

        Try

            MarcaGrillaOperaciones()
            MarcaGrillaClasificacion()

            Table_Firmas.CssClass = "tablanivel"

            CD.NegociacionFirmasDevuelve(HF_NroNNC.Value, CodEje, Table_Firmas)
            Table_Firmas.DataBind()

            If Table_Firmas.Rows.Count > 0 Then
                IB_Otorgar.Enabled = True
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

        UpdatePanel1.Update()
        
    End Sub

#End Region

#Region "Sub and Function"

    Private Sub MarcaGrillaOperaciones()

        Try

            For I = 0 To GV_Ope_Ope.Rows.Count - 1

                If (NroOperacion = CType(GV_Ope_Ope.Rows(I).FindControl("Img_Ver"), ImageButton).ToolTip) Then

                    HF_NroNeg.Value = GV_Ope_Ope.Rows(I).Cells(1).Text
                    HF_RutCli.Value = RG.LimpiaRut(GV_Ope_Ope.Rows(I).Cells(2).Text)

                    If (I Mod 2) = 0 Then
                        GV_Ope_Ope.Rows(I).CssClass = "selectable"
                    Else
                        GV_Ope_Ope.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Ope_Ope.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Ope_Ope.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub MarcaGrillaClasificacion()

        Try

            For I = 0 To GV_Clasificacion.Rows.Count - 1

                If (HF_NroCCF.Value = CType(GV_Clasificacion.Rows(I).FindControl("Image1"), Image).ToolTip) Then

                    HF_NroNNC.Value = GV_Clasificacion.Rows(I).Cells(0).Text

                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "selectable"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ConsultasGenerales
        Dim CLI As cli_cls

        Try


            If Txt_Rut_Cli.Text <> "" Then

                CLI = clasecli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)
                If valida_cliente <> "" Then

                    Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Else

                    If IsNothing(CLI) Then
                        Msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Txt_Rut_Cli.Text = ""
                        Txt_Dig_Cli.Text = ""
                        Exit Function
                    End If

                    Session("Cliente") = CLI
                    'Tipo de cliente (Natural / Juridico)
                    If CLI.id_P_0044 = 1 Then
                        Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
                    Else
                        Me.Txt_Raz_Soc.Text = CLI.cli_rso
                    End If

                    Return True
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Private Sub CargaDrop_Form()

        Try

            'Ejecutivos de cuentas (confirmar que tipo de ejecutivos mostrar)
            CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)

            'Parámetro Tipo Operación
            CG.ParametrosDevuelve(TablaParametro.TipoOperacion, True, DP_TipoOperacion)

            'Parámetro Caracter de la Operación
            'CG.ParametrosDevuelve(TablaParametro.CaracteristicaOperación, True, DP_CaracterOperacion)

            'Parámetro Tipo Documento
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocumento)

            'Estado de Condicion
            'CG.ParametrosDevuelve(TablaParametro.EstadoCondicion, True, DP_EstadoCondicion)


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function ValidaBusqueda() As Boolean

        Try

            '*******************************************************************************************************************
            If Txt_Fec_Dsd.Text.Trim = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Fecha Desde", TipoDeMensaje._Informacion)
                Txt_Fec_Dsd.Focus()
                Return False
            End If

            If Txt_Fec_Hst.Text.Trim = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Fecha Hasta", TipoDeMensaje._Informacion)
                Txt_Fec_Hst.Focus()
                Return False
            End If

            If Not IsDate(Txt_Fec_Dsd.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha desde erronea", TipoDeMensaje._Informacion)
                Txt_Fec_Dsd.Text = ""
                Return False
            End If

            If Not IsDate(Txt_Fec_Hst.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha hasta erronea", TipoDeMensaje._Informacion)
                Txt_Fec_Hst.Text = ""
                Return False
            End If

            If CDate(Txt_Fec_Dsd.Text) > CDate(Txt_Fec_Hst.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha desde no puede ser mayor a la fecha hasta", TipoDeMensaje._Informacion)
                Txt_Fec_Dsd.Focus()
                Return False
            End If


            Return True
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Sub HabilitaCamposBusqueda(ByVal Estado As Boolean)

        Select Case Estado

            Case True
                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                'Txt_Fec_Dsd.Text = ""
                'Txt_Fec_Hst.Text = ""
                Txt_Raz_Soc.Text = ""

                DP_Ejecutivos.SelectedValue = 0
                DP_TipoDocumento.SelectedValue = 0
                DP_TipoOperacion.SelectedValue = 0

                Txt_Rut_Cli.CssClass = "clsTxt"
                Txt_Dig_Cli.CssClass = "clsTxt"
                Txt_Fec_Dsd.CssClass = "clsMandatorio"
                Txt_Fec_Hst.CssClass = "clsMandatorio"

                DP_Ejecutivos.CssClass = "clsTxt"
                DP_TipoDocumento.CssClass = "clsTxt"
                DP_TipoOperacion.CssClass = "clsTxt"

                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False
                Txt_Fec_Dsd.ReadOnly = False
                Txt_Fec_Hst.ReadOnly = False

                DP_Ejecutivos.Enabled = True
                DP_TipoDocumento.Enabled = True
                DP_TipoOperacion.Enabled = True
                Txt_Fec_Dsd_CalendarExtender.Enabled = True
                Txt_Fec_Hst_CalendarExtender.Enabled = True
            Case False

                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Fec_Dsd.CssClass = "clsDisabled"
                Txt_Fec_Hst.CssClass = "clsDisabled"

                DP_Ejecutivos.CssClass = "clsDisabled"
                DP_TipoDocumento.CssClass = "clsDisabled"
                DP_TipoOperacion.CssClass = "clsDisabled"

                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Fec_Dsd.ReadOnly = True
                Txt_Fec_Hst.ReadOnly = True
                Txt_Fec_Dsd_CalendarExtender.Enabled = False
                Txt_Fec_Hst_CalendarExtender.Enabled = False

                DP_Ejecutivos.Enabled = False
                DP_TipoDocumento.Enabled = False
                DP_TipoOperacion.Enabled = False


        End Select

    End Sub

    Private Sub LimpiaGrillas()
        GV_Ope_Ope.DataSource = Nothing
        GV_Clasificacion.DataSource = Nothing

        GV_Ope_Ope.DataBind()
        GV_Clasificacion.DataBind()
    End Sub


#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020405, Usr, "PRESIONA BOTON BUSCAR OPERACIONES") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try

            If Not ValidaBusqueda() Then
                Exit Sub
            End If

            Dim Eje_Desde, Eje_Hasta As Integer
            Dim Est_Desde, Est_Hasta As Integer
            Dim Fec_Desde, Fec_Hasta As Date
            Dim Tip_Desde, Tip_Hasta As Integer
            Dim Rut_Desde, Rut_Hasta As Long
            Dim Doc_Desde, Doc_Hasta As Integer

            CargaDatosCliente()

            'RutCli = Txt_Rut_Cli.Text.Replace(".", "")

            'Rango de Ejecutivos
            If DP_Ejecutivos.SelectedIndex > 0 Then
                Eje_Desde = DP_Ejecutivos.SelectedValue
                Eje_Hasta = DP_Ejecutivos.SelectedValue
            Else
                Eje_Desde = 0 ' CodEje
                Eje_Hasta = 999 'CodEje
            End If

            'Rango de Fechas
            Fec_Desde = Txt_Fec_Dsd.Text & " 00:00:01"
            Fec_Hasta = Txt_Fec_Hst.Text & " 23:59:59"

            'Rango de Tipo de Operacion<
            If DP_TipoOperacion.SelectedIndex > 0 Then
                Tip_Desde = DP_TipoOperacion.SelectedValue
                Tip_Hasta = DP_TipoOperacion.SelectedValue
            Else
                Tip_Desde = 0
                Tip_Hasta = 999999999
            End If

            'Rut Cliente
            If Txt_Rut_Cli.Text.Trim <> "" Then
                Rut_Desde = CLng(Txt_Rut_Cli.Text.Trim)
                Rut_Hasta = CLng(Txt_Rut_Cli.Text.Trim)
            Else
                Rut_Desde = 0
                Rut_Hasta = 9999999999999
            End If

            'Rango de Tipo de Documento
            If DP_TipoDocumento.SelectedIndex > 0 Then
                Doc_Desde = DP_TipoDocumento.SelectedValue
                Doc_Hasta = DP_TipoDocumento.SelectedValue
            Else
                Doc_Desde = 0
                Doc_Hasta = 999999999
            End If


            'Operaciones simuladas
            Est_Desde = 2
            Est_Hasta = 2

            coll_ope = New Collection

            coll_ope = OP.OperacionesTodasDevuelve(Rut_Desde, Rut_Hasta, _
                                                   Eje_Desde, Eje_Hasta, _
                                                   Est_Desde, Est_Hasta, _
                                                   Fec_Desde, Fec_Hasta, _
                                                   Tip_Desde, Tip_Hasta, _
                                                   Doc_Desde, Doc_Hasta, _
                                                   0, 999999999, _
                                                   5)

            GV_Ope_Ope.DataSource = coll_ope
            GV_Ope_Ope.DataBind()

            Dim Formato_Moneda As String = ""
            Dim Fmt As New FuncionesGenerales.ClsLocateInfo

            For I = 0 To GV_Ope_Ope.Rows.Count - 1

                GV_Ope_Ope.Rows(I).Cells(2).Text = RG.FormatoMiles(GV_Ope_Ope.Rows(I).Cells(2).Text) & "-" & RG.Vrut(CLng(GV_Ope_Ope.Rows(I).Cells(2).Text))

                Select Case GV_Ope_Ope.Rows(I).Cells(7).Text.Trim
                    Case "PESO" : Formato_Moneda = Fmt.FCMSD
                    Case "UF - UF" : Formato_Moneda = Fmt.FCMCD4
                    Case "US$ - DOLAR", "EURO" : Formato_Moneda = Fmt.FCMCD
                End Select

                GV_Ope_Ope.Rows(I).Cells(8).Text = Format(CDbl(GV_Ope_Ope.Rows(I).Cells(8).Text), Formato_Moneda)

            Next

            HabilitaCamposBusqueda(False)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Otorgar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Otorgar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010405, Usr, "PRESIONA BOTON OTORGAR OPERACIONES") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try

            Dim sistema As sis_cls = CG.SistemaDevuelve()



            'Valida que todos los requisitos se cumplan 
            Dim Valida_Requisitos_Cumplidos As Boolean = False
            Dim Coll As Collection

            Coll = CD.RequisitosDevuelvePorOperacion(HF_NroOpe.Value)

            For I = 1 To Coll.Count
                If Coll.Item(I).Estado = "A" Then
                    Valida_Requisitos_Cumplidos = True
                Else
                    Valida_Requisitos_Cumplidos = False
                    Exit For
                End If
            Next

            'DEBEN ESTAR TODOS APROBADOS PARA OTORGAR
            If Not Valida_Requisitos_Cumplidos Then
                Msj.Mensaje(Me.Page, Caption, "Debe tener todos los requisitos cumplidos para aprobar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'valida si la operacion cumple con sus aprobaciones
            If Not CD.ValidaAprobaciones(HF_NroNeg.Value, Sucursal) Then
                Msj.Mensaje(Me.Page, Caption, "Debe cumplir con todas las aprobaciones, un nivel por clasificación", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If sistema.sis_vld_lin = "S" Then

                'valida si tiene sobregiro de linea y si el parametro de sistema lo deja otorgar
                If CMC.RetornaResponsabilidad(Convert.ToInt32(HF_NroOpn.Value)) = True Then
                    If Not CD.ValidaSobregiroDeLineaDelCliente(HF_NroOpe.Value) Then
                        Msj.Mensaje(Me.Page, Caption, "La operación no puede ser otorgada por que la línea de financiamiento del cliente quedara sobregirada (supera % de exceso).", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If

                'valida si tiene sobregiro de linea a los pagadores y si el parametro de sistema lo deja otorgar
                If Not CD.ValidaSobregiroDeLineaDelPagador(HF_NroOpe.Value) Then
                    Msj.Mensaje(Me.Page, Caption, "La operación no puede ser otorgada por que la línea de financiamiento del pagador " & CD.RutDeudor & " quedara sobregirada.", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                ''Valida que toda la documentacion haya sido confirmada
                'Dim valida_confirmacion As Boolean = False
                'Dim doctos_ope As IQueryable = CG.DocConDevuelvePorNegociacion(NroNegociacion, 2)

                'For Each d In doctos_ope
                '    If d.estado <> "A" Then
                '        valida_confirmacion = True
                '        Exit For
                '    End If
                'Next

                'If valida_confirmacion Then
                '    Msj.Mensaje(Me.Page, Caption, "Debe tener todos los documentos de operación confirmados para otorgar", TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                '2015-10-08 jlagos
                If Not OP.ValidaCalculosDeOperacion(HF_NroOpn.Value) Then
                    Msj.Mensaje(Me.Page, "Atención", "La operación no cuadra los calculos de descuentos", TipoDeMensaje._Exclamacion)
                    Exit Sub '(Se deja comentariado para cargar datos)
                End If
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Desea otorgar la operación?", TipoDeMensaje._Confirmacion, LB_Otorgar.UniqueID)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        HabilitaCamposBusqueda(True)

        GV_Ope_Ope.DataSource = Nothing
        GV_Clasificacion.DataSource = Nothing

        GV_Ope_Ope.DataBind()
        GV_Clasificacion.DataBind()

        Table_Firmas.Dispose()
        Table_Firmas.DataBind()
        Table_Firmas.Rows.Clear()

        TabContainer1.ActiveTabIndex = 0
        TabContainer1.DataBind()

        HF_NroOpe.Value = ""
        HF_NroEva.Value = ""
        HF_NroNeg.Value = ""
        HF_NroCCF.Value = ""
        HF_NroNNC.Value = ""
        HF_RutCli.Value = ""


        NroOperacion = 0
        NroNegociacion = 0
        RutCli = 0
        IB_Otorgar.Enabled = False

        Txt_Fec_Dsd.Text = Format(CDate("01" & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")
        Txt_Fec_Hst.Text = Format(CDate(Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")
        NroPaginacion = 0
        NroPaginacion_Claf = 0

    End Sub

    Protected Sub LB_Otorgar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Otorgar.Click

        Try

            'Otorga Operaciones
            Dim Ope As ope_cls
            Coll_DSI = New Collection

            'Carga Operacion Seleccionada 
            Ope = Operacion

            Coll_DSI = OP.DocumentosSimulados_RetornaDoctos(RutCli, RutCli, _
                                                             0, 9999999999999, _
                                                            NroOperacion, NroOperacion, _
                                                             0, 999, _
                                                             0, 999999, _
                                                             0, 9999)
            Dim Opo As New opo_cls

            Opo.id_ope = Ope.id_ope
            Opo.opo_fec_oto = Date.Now
            Opo.id_eje = CodEje
            Opo.opo_otg = OP.NumeroOtorgamientoDevuelve(RutCli) + 1

            NroOperacion = Opo.id_ope

            If OP.OtorgamiendoGuarda(Opo, NroOperacion, Ope.opn_cls.id_P_0012) Then

                Msj.Mensaje(Me.Page, Caption, OP.MensajeOperacion, TipoDeMensaje._Informacion)

                HF_NroOpe.Value = ""
                HF_NroEva.Value = ""
                HF_NroNeg.Value = ""
                HF_NroCCF.Value = ""
                HF_NroNNC.Value = ""
                HF_RutCli.Value = ""

                IB_Otorgar.Enabled = False
                HabilitaCamposBusqueda(True)

                GV_Ope_Ope.DataSource = Nothing
                GV_Clasificacion.DataSource = Nothing

                GV_Ope_Ope.DataBind()
                GV_Clasificacion.DataBind()

                Table_Firmas.Dispose()
                Table_Firmas.DataBind()
                Table_Firmas.Rows.Clear()

                TabContainer1.ActiveTabIndex = 0

                IB_Otorgar.Enabled = False

                RW.AbrePopup(Me, 1, "../rigthframe_archivos/Reporteotg.aspx?rut_cli=" & RutCli & "&NroOperacion=" & NroOperacion & "&Numero=" & NroNegociacion & "", "Informes", 380, 210, 420, 420)

            Else
                Msj.Mensaje(Me.Page, Caption, OP.MensajeOperacion, TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

        Dim j As System.Web.UI.ImageClickEventArgs
        IB_Buscar_Click(Me, j)

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If GV_Ope_Ope.Rows.Count = 0 Then
            Exit Sub
        End If

        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If
        If NroPaginacion >= 5 Then
            NroPaginacion -= 5
            IB_Buscar_Click(Me, e)
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Ope_Ope.Rows.Count = 0 Then
            Exit Sub
        End If
        If GV_Ope_Ope.Rows.Count < 5 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If


        If GV_Ope_Ope.Rows.Count = 5 Then
            NroPaginacion += 5
            IB_Buscar_Click(Me, e)
        End If

    End Sub

    Protected Sub Btn_AplicarCla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_AplicarCla.Click
        If DP_Calificacion.SelectedIndex > 0 Then
            OP.ModificaClasificacionDoctoPorOperacion(HF_NroOpe.Value, DP_Calificacion.SelectedValue)
            Msj.Mensaje(Me, Caption, "Clasificación cambiada", 2)
            DP_Calificacion.ClearSelection()
        Else
            Msj.Mensaje(Me, Caption, "Debe seleccionar una clasificación", 2)
        End If
    End Sub

#End Region

End Class

