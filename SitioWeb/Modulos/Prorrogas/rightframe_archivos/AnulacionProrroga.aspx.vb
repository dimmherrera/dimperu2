Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports ClsSession.SesionProrrogas
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Prorrogas_rightframe_archivos_AnulaProrrogas
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Sesion As New ClsSession.ClsSession
    Dim SesionPro As New ClsSession.SesionProrrogas
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Caption As String = "Solicitud de Prorrogas"
    Dim CuentaDoctosSeleccionados As Int16 = 0
    Dim Msj As New ClsMensaje
    'Dim agt As New Perfiles.Cls_Principal
    Dim Agt As New Perfiles.Cls_Principal


#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Response.Expires = -1
            Response.Cache.SetNoStore()

            Coll_SPG = New Collection
            Coll_DPG = New Collection
            NroPaginacion_DetalleSolProrroga = 0
            NroPaginacion = 0

            'Sesion.iniciarSesion()

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

            RefrescaPantalla(True, True, True, True)

        End If

        If HF_Nro_Neg.Value <> "" And HF_Nro_Neg.Value > 0 And SesionPro.Accion_Pro > 0 Then
            Lb_Bus_Pag_Click(Me, e)
        End If

        'IB_ValidaSolicitud.Attributes.Add("onClick", "WinOpen(2,'../../Prorrogas/rightframe_archivos/PopUpObservacion.aspx?id=3','Observacion',500,500,0, 0);)")
        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            CargaDatosCliente()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_Negociacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Negociacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
        End If

    End Sub

    Protected Sub Busqueda_GV_SOLICITUD_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub CBX_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBX_Cliente.CheckedChanged
        chk_cliente(CBX_Cliente.Checked)
        If CBX_Cliente.Checked Then

            HabilitaDesabilitaCliente(True)
        Else
            HabilitaDesabilitaCliente(False)

        End If
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If CargaDatosCliente() Then

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Lb_aprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_aprobar.Click
        Try
            '--------------------------------------------------------------------------------------------------------------------------------
            'Guarda Validación de Solicitud de Prorroga
            '--------------------------------------------------------------------------------------------------------------------------------
            'AG.Prorroga_GuardaProrroga(Coll_SPG.Item(CLng(HF_Nro_Neg.Value)).id_spg, 2)

            '--------------------------------------------------------------------------------------------------------------------------------
            'Llamar Pantalla de Reporte (Solicitud de Prorroga)
            '--------------------------------------------------------------------------------------------------------------------------------
            Dim StrPagina As String = "Reporte_SolicitudProrroga.aspx?RUTCLIENTE=" & Format(CLng(Coll_SPG.Item(CLng(HF_Nro_Neg.Value)).cli_idc), VAR.FMT_RUT) & "&IDSPG=" & Coll_SPG.Item(CLng(HF_Nro_Neg.Value)).id_spg & "&MES=" & Month(Coll_SPG.Item(CLng(HF_Nro_Neg.Value)).spg_fec) & "&ESTADO=2"
            RW.AbrePopup(Me, 1, StrPagina, "RepSolProrroga", 1280, 1024, 0, 0)

            '--------------------------------------------------------------------------------------------------------------------------------
            'Limpia Objetos despues de Calcular Solicitud de Prorroga Excepto Cliente y Criterios
            '--------------------------------------------------------------------------------------------------------------------------------
            'RefrescaPantalla(False, False, True, True)
            Dim x As System.Web.UI.ImageClickEventArgs
            IB_Buscar_Click(Me, x)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Control Dual"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion >= 4 Then
                NroPaginacion -= 4
                IB_Buscar_Click(Me, e)

                GV_DetalleSolicitud.DataSource = New Collection
                GV_DetalleSolicitud.DataBind()


            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If GV_Negociacion.Rows.Count >= 4 Then
                NroPaginacion += 4
                IB_Buscar_Click(Me, e)

                GV_DetalleSolicitud.DataSource = New Collection
                GV_DetalleSolicitud.DataBind()

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_GvDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_GvDetalle.Click
        Try

            If GV_DetalleSolicitud.Rows.Count >= 4 Then
                NroPaginacion_DetalleSolProrroga += 4
                Busqueda_GV_SOLICITUD_Click(Me, e)


            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_GvDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_GvDetalle.Click
        Try
            If NroPaginacion_DetalleSolProrroga >= 4 Then
                NroPaginacion_DetalleSolProrroga -= 4
                Busqueda_GV_SOLICITUD_Click(Me, e)

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim BTN As ImageButton = CType(sender, ImageButton)

        Try

            IB_AnularSolicitud.Enabled = False

            If BTN.ToolTip <> "" Then

                'Busqueda_GV_SOLICITUD_Click(sender, e)
                BorraCollection(Coll_DPG)

                For X = 1 To Coll_SPG.Count
                    If BTN.ToolTip = Coll_SPG.Item(X).id_spg Then
                        HF_Nro_Neg.Value = X
                        Exit For
                    End If
                Next
                '--------------------------------------------------------------------------------------------------------------------------
                'Retorna Detalle Solicitud
                '--------------------------------------------------------------------------------------------------------------------------
                'Dim Temporal_dpg = CG.Prorroga_DevuelveDetalleSolicitud(Coll_SPG.Item(CInt(Me.HF_Nro_Neg.Value)).id_spg)
                'Coll_DPG = CG.Prorroga_DevuelveDetalleSolicitud(Coll_SPG.Item(CInt(Me.HF_Nro_Neg.Value)).id_spg)
                Coll_DPG = CG.Prorroga_DevuelveDetalleSolicitud(BTN.ToolTip) 'FY 04-05-2012
                '-----------------------------------------------------------------------------------------------------------------------
                'Llena Collection Para ser Ocupada con Posterioridad (Guardar)
                '-----------------------------------------------------------------------------------------------------------------------
                'For Each a In Temporal_dpg
                '    Coll_DPG.Add(a)
                'Nexth

                GV_DetalleSolicitud.DataSource = Coll_DPG
                GV_DetalleSolicitud.DataBind()

                '-----------------------------------------------------------------------------------------------------------------------
                'Da Formato a Datos desplegados en Grilla
                '-----------------------------------------------------------------------------------------------------------------------
                FormatoGrillaDetalle()

                MarcaGrilla_GV_Negociacion(BTN.ToolTip)

                If Not ValidaGuardaSolicitudProrroga() Then
                    Exit Sub
                End If

                IB_AnularSolicitud.Enabled = True

                IB_AnularSolicitud.Attributes.Add("OnClick", "WinOpen(2, 'PopUpObservacion.aspx?id=" & HF_Nro_Neg.Value & "&Accion=3', 'Observacion', 500, 300, 0, 0);")

            End If


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try



    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click 'Handles IB_Buscar.Click
        Try

            'If Not Agt.ValidaAccesso(20, 20010209, Usr, "PRESIONA BOTON BUSCAR") Then
            '    Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If

            If Not Agt.ValidaAccesso(20, 20020403, Usr, "PRESIONA BOTON BUSCAR") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            HF_Nro_Neg.Value = -1 'FY 05-05-2012

            Dim fc As New FuncionesGenerales.FComunes
            Dim rutcliente1 As String, rutcliente2 As String
            Dim fechasolicitud1 As String, fechasolicitud2 As String
            Dim estado_dde As Integer, estado_hta As Integer

            BorraCollection(Coll_SPG)
            BorraCollection(Coll_DPG)

            GV_DetalleSolicitud.DataSource = Nothing
            GV_DetalleSolicitud.DataBind()

            If CBX_Cliente.Checked Then
                CargaDatosCliente()
                rutcliente1 = Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT)
                rutcliente2 = Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT)
            Else
                rutcliente1 = "000000000000"
                rutcliente2 = "9999999999999"
            End If

            estado_dde = 2
            estado_hta = 2

            fechasolicitud1 = Format(Date.Now, "dd/MM/yyyy")
            fechasolicitud2 = Format(Date.Now, "dd/MM/yyyy")
            '-----------------------------------------------------------------------------------------------------------------------
            'Retorna Solicitudes Para ser Prorrogados 
            '-----------------------------------------------------------------------------------------------------------------------
            Dim Temporal_spg = CG.Prorroga_DevuelveSolicitudes(rutcliente1, rutcliente2, fechasolicitud1, fechasolicitud2, estado_dde, estado_hta)

            '-----------------------------------------------------------------------------------------------------------------------
            'Llena Collection Para ser Ocupada con Posterioridad (Guardar)
            '-----------------------------------------------------------------------------------------------------------------------
            For Each a In Temporal_spg
                Coll_SPG.Add(a)
            Next

            '-----------------------------------------------------------------------------------------------------------------------
            'Asocia Collection a Grilla de Despliegue y Selección
            '-----------------------------------------------------------------------------------------------------------------------
            GV_Negociacion.DataSource = Coll_SPG
            GV_Negociacion.DataBind()

            If GV_Negociacion.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No posee documentos según criterio de búsqueda", TipoDeMensaje._Informacion)
            Else
                '-----------------------------------------------------------------------------------------------------------------------
                'Marca Documentos con Solicitud de Prorroga
                '-----------------------------------------------------------------------------------------------------------------------
                MarcaSolicitud()

                '-----------------------------------------------------------------------------------------------------------------------
                'Da Formato a Datos desplegados en Grilla
                '-----------------------------------------------------------------------------------------------------------------------
                FormatoGrillaSolicitud()

            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        RefrescaPantalla(True, True, True, True)
        NroPaginacion = 0
        NroPaginacion_DetalleSolProrroga = 0
        HF_Nro_Neg.Value = -1

    End Sub

    Protected Sub Lb_Bus_Pag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_Bus_Pag.Click
        Try
            'CargaDatosCliente()

            'Llamar Pantalla de Reporte (Solicitud de Prorroga)
            '--------------------------------------------------------------------------------------------------------------------------------


            Refrescar()

            'IB_Buscar_Click(Me, e)

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls
        Dim eje As eje_cls
        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Function
            End If
            If UCase(Txt_Dig_Cli.Text) <> RG.Vrut(Replace(Txt_Rut_Cli.Text, ".", "")) Then
                Msj.Mensaje(Me.Page, Caption, "Nit Incorrecto del Cliente", TipoDeMensaje._Informacion)
                Exit Function
            End If

            CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)


            If valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Function
            Else
                If IsNothing(CLI) Then
                    Msj.Mensaje(Me.Page, Caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                    Exit Function
                End If
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Session("Cliente") = CLI

                Txt_TipoCliente.Text = CLI.P_0044_cls.pnu_des.Trim

                'Tipo de cliente (Natural / Juridico)
                If CLI.id_P_0044 = 1 Then
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
                Else
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso
                End If

                'Banca
                If Not IsNothing(CLI.PL_000066_cls) Then
                    Me.Txt_Banca.Text = CLI.PL_000066_cls.pal_des.Trim
                End If

                'Sucursal
                If Not IsNothing(CLI.suc_cls) Then
                    Me.Txt_Sucursal.Text = CLI.suc_cls.suc_des_cra
                End If

                'Ejecutivo
                'If Not IsNothing(CLI.eje_cls1) Then
                '    Me.Txt_Ejecutivo.Text = CLI.eje_cls1.eje_nom.Trim
                'End If

                'Para que muestre Ejecutivo
                eje = ClsCli.EjecutivoDevuelve(CLI.id_eje_cod_eje)
                Txt_Ejecutivo.Text = eje.eje_nom


                HabilitaDesabilitaCliente(False)

                Return True
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Private Sub HabilitaDesabilitaCliente(ByVal Estado As Boolean)

        Txt_Rut_Cli.ReadOnly = Not Estado
        Txt_Dig_Cli.ReadOnly = Not Estado

        If Not Estado Then
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"

        Else
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"

            Txt_Rut_Cli.Focus()
        End If


    End Sub

    Protected Function ValidaGuardaSolicitudProrroga() As Boolean

        ValidaGuardaSolicitudProrroga = False

        '--------------------------------------------------------------------------------------------------------------------------------
        'Validación Seleccion de Solicitud
        '--------------------------------------------------------------------------------------------------------------------------------
        If Val(HF_Nro_Neg.Value) = -1 Then
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar Una Solicitud", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        '--------------------------------------------------------------------------------------------------------------------------------
        'Validación Seleccion de Solicitud no este Rechazado o Validado
        '--------------------------------------------------------------------------------------------------------------------------------
        'Select Case Coll_SPG.Item(CInt(HF_Nro_Neg.Value)).spg_est
        '    Case 2 '2, 3
        '        'Msj.Mensaje(Me.Page, Caption, "Solicitud Aprobada, no puede aprobarla ni rechazarla", TipoDeMensaje._Exclamacion)
        '        Msj.Mensaje(Me.Page, Caption, "Esta Solicitud ya se encuentra Aprobada, por ende no se puede volver a aprobar o rechazar", TipoDeMensaje._Exclamacion)
        '        Exit Function
        '    Case 3
        '        'Msj.Mensaje(Me.Page, Caption, "Debe seleccionar Una Solicitud Sin Aprobar o Rechazar", TipoDeMensaje._Exclamacion)
        '        Msj.Mensaje(Me.Page, Caption, "Esta Solicitud ya se encuentra Rechazada, por ende no se puede volver a rechazar o aprobar", TipoDeMensaje._Exclamacion)
        '        Exit Function

        'End Select

        '--------------------------------------------------------------------------------------------------------------------------------
        'Validaciónn de Cambio de estados de Documentos Solicitados a Prorroga
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim Temporal_dpg = CG.Prorroga_ValidaCambiodeEstadoDocumentosSolicitud(Coll_SPG.Item(CInt(HF_Nro_Neg.Value)).id_spg)

        Dim docto As String = ""
        For Each a In Temporal_dpg
            docto = docto & CStr(a.dsi_num) & "-" & CStr(a.dsi_flj_num) & Chr(13)
        Next

        If docto.Trim <> "" Then
            Msj.Mensaje(Me.Page, Caption, "Los Siguientes Doctos. fueron Pagados o ya estan Vencidos: " & Chr(13) & docto, TipoDeMensaje._Exclamacion)
            Exit Function
        End If


        '--------------------------------------------------------------------------------------------------------------------------------
        'Validaciónn si los Documentos Solicitados a Prorroga estan siendo pagados
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim Temporal_dpg2 = CG.Prorroga_ValidaPagoEnLineaDocumentosSolicitud(Coll_SPG.Item(CInt(HF_Nro_Neg.Value)).id_spg)

        docto = ""
        For Each a In Temporal_dpg2
            docto = docto & CStr(a.dsi_num) & "-" & CStr(a.dsi_flj_num) & Chr(13)
        Next

        If docto.Trim <> "" Then
            Msj.Mensaje(Me.Page, Caption, "Los Siguientes Doctos. estan siendo Pagados : " & Chr(13) & docto, TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        ValidaGuardaSolicitudProrroga = True
    End Function

    Protected Sub chk_cliente(ByVal chk_cliente As Boolean)

        If chk_cliente Then
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Me.Txt_Raz_Soc.Text = ""
            IB_AyudaCli.Enabled = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
        Else
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.Text = ""
            IB_AyudaCli.Enabled = False
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        End If
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_TipoCliente.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Banca.Text = ""
        Txt_Sucursal.Text = ""
        Txt_Ejecutivo.Text = ""

    End Sub

    Protected Sub RefrescaPantalla(ByVal DatosCliente As Boolean, ByVal Criterios As Boolean, ByVal ListaSolicitud As Boolean, ByVal ListaDocumentos As Boolean)

        If DatosCliente Then
            CBX_Cliente.Checked = False
            chk_cliente(False)
        End If

        If ListaDocumentos Then
            HF_Nro_Neg.Value = -1
            GV_Negociacion.DataSource = Nothing
            GV_Negociacion.DataBind()
        End If

        If ListaDocumentos Then
            GV_DetalleSolicitud.DataSource = Nothing
            GV_DetalleSolicitud.DataBind()
        End If
    End Sub

    Protected Sub MarcaSolicitud()
        '-----------------------------------------------------------------------------------------------------------------------------
        'Ciclo que recorre Documentos Desplegados para su formato
        '-----------------------------------------------------------------------------------------------------------------------------
        For i = 1 To Coll_SPG.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0
            Select Case Coll_SPG.Item(i).spg_est
                Case 2, 3
                    If Coll_SPG.Item(i).spg_est = 2 Then
                        GV_Negociacion.Rows(i - 1).BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Negociacion.Rows(i - 1).Font.Bold = True
                    Else
                        GV_Negociacion.Rows(i - 1).BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                        GV_Negociacion.Rows(i - 1).Font.Bold = True
                    End If
                    'GV_Negociacion.Rows(i - 1).CssClass = "formatable"
            End Select
        Next

    End Sub

    Protected Sub FormatoGrillaSolicitud()
        '' ''1-DataField="cli_idc">
        '' ''2-DataField="Cliente">
        '' ''3-DataField="spg_fec">
        '' ''4-DataField="id_spg">
        '' ''5-DataField="eje_des_cra">
        '' ''6-DataField="spg_tas">
        '' ''7-DataField="spg_com">
        '' ''8-DataField="spg_obs">

        '-----------------------------------------------------------------------------------------------------------------------------
        'Ciclo que recorre Documentos Desplegados para su formato
        '-----------------------------------------------------------------------------------------------------------------------------
        For i = 1 To Coll_SPG.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0
            GV_Negociacion.Rows(i - 1).Cells(1).Text = Format(CLng(Coll_SPG.Item(i).cli_idc), FMT.FCMSD) & "-" & Coll_SPG.Item(i).cli_dig_ito
            GV_Negociacion.Rows(i - 1).Cells(3).Text = Format(Coll_SPG.Item(i).spg_fec, "dd/MM/yyyy")
            GV_Negociacion.Rows(i - 1).Cells(4).Text = Format(CLng(Coll_SPG.Item(i).id_spg), FMT.FCMSD)
            GV_Negociacion.Rows(i - 1).Cells(6).Text = Format(CDec(Coll_SPG.Item(i).spg_tas), FMT.FSMCD)
            GV_Negociacion.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_SPG.Item(i).spg_com), FMT.FCMSD)
        Next

    End Sub
    Protected Sub FormatoGrillaDetalle()
        '' ''1-DataField = "deu_ide"
        '' ''2-DataField = "Deudor"
        '' ''3-DataField = "opo_otg"
        '' ''4-DataField = "TipoDocto"
        '' ''5-DataField = "dsi_num"
        '' ''6-DataField = "dsi_flj_num"
        '' ''7-DataField = "dsi_mto_fin"
        '' ''8-DataField = "doc_sdo_cli"
        '' ''9-DataField = "Moneda"
        '' ''10-DataField = "doc_fev_rea"
        '' ''11-DataField = "nva_doc_fev_rea"
        '' ''12-DataField = "dpg_int_ere"
        '' ''13-DataField = "dpg_com_isi"
        '' ''14-DataField = "dpg_iva_com"
        '' ''15-DataField = "TotalGastos"

        '-----------------------------------------------------------------------------------------------------------------------------
        'Ciclo que recorre Documentos Desplegados para su formato
        '-----------------------------------------------------------------------------------------------------------------------------
        For i = 1 To Coll_DPG.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0
            GV_DetalleSolicitud.Rows(i - 1).Cells(0).Text = Format(CLng(Coll_DPG.Item(i).deu_ide), FMT.FCMSD) & "-" & Coll_DPG.Item(i).deu_dig_ito
            GV_DetalleSolicitud.Rows(i - 1).Cells(2).Text = Format(CLng(Coll_DPG.Item(i).opo_otg), FMT.FCMSD)
            GV_DetalleSolicitud.Rows(i - 1).Cells(4).Text = Coll_DPG.Item(i).dsi_num

            Select Case Coll_DPG.Item(i).id_P_0023
                Case 1
                    GV_DetalleSolicitud.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMSD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMSD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMSD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMSD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMSD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMSD)

                Case 2
                    GV_DetalleSolicitud.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMCD4)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMCD4)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMCD4)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMCD4)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMCD4)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMCD4)

                Case 3, 4
                    GV_DetalleSolicitud.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMCD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMCD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMCD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMCD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMCD)
                    GV_DetalleSolicitud.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMCD)
            End Select

            GV_DetalleSolicitud.Rows(i - 1).Cells(9).Text = Format(Coll_DPG.Item(i).doc_fev_rea, "dd/MM/yyyy")
            GV_DetalleSolicitud.Rows(i - 1).Cells(10).Text = Format(Coll_DPG.Item(i).nva_doc_fev_rea, "dd/MM/yyyy")

        Next

    End Sub

    Protected Friend Sub BUSCA1()

    End Sub

    Private Sub Refrescar()
        Try

            HF_Nro_Neg.Value = -1 'FY 05-05-2012
            SesionPro.Accion_Pro = 0

            Dim fc As New FuncionesGenerales.FComunes
            Dim rutcliente1 As String, rutcliente2 As String
            Dim fechasolicitud1 As String, fechasolicitud2 As String

            BorraCollection(Coll_SPG)
            BorraCollection(Coll_DPG)

            GV_DetalleSolicitud.DataSource = Nothing
            GV_DetalleSolicitud.DataBind()


            If CBX_Cliente.Checked Then
                CargaDatosCliente()
                rutcliente1 = Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT)
                rutcliente2 = Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT)
            Else
                rutcliente1 = "000000000000"
                rutcliente2 = "9999999999999"
            End If



            Dim estado_dde As Integer
            Dim estado_hta As Integer


            estado_dde = 2
            estado_hta = 2

            fechasolicitud1 = Format(Date.Now, "dd/MM/yyyy")
            fechasolicitud2 = Format(Date.Now, "dd/MM/yyyy")
            '-----------------------------------------------------------------------------------------------------------------------
            'Retorna Solicitudes Para ser Prorrogados 
            '-----------------------------------------------------------------------------------------------------------------------
            Dim Temporal_spg = CG.Prorroga_DevuelveSolicitudes(rutcliente1, rutcliente2, fechasolicitud1, fechasolicitud2, estado_dde, estado_hta)

            '-----------------------------------------------------------------------------------------------------------------------
            'Llena Collection Para ser Ocupada con Posterioridad (Guardar)
            '-----------------------------------------------------------------------------------------------------------------------
            For Each a In Temporal_spg
                Coll_SPG.Add(a)
            Next

            '-----------------------------------------------------------------------------------------------------------------------
            'Asocia Collection a Grilla de Despliegue y Selección
            '-----------------------------------------------------------------------------------------------------------------------
            GV_Negociacion.DataSource = Coll_SPG
            GV_Negociacion.DataBind()

            If GV_Negociacion.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No posee documentos según criterio de búsqueda", TipoDeMensaje._Informacion)
            Else
                '-----------------------------------------------------------------------------------------------------------------------
                'Marca Documentos con Solicitud de Prorroga
                '-----------------------------------------------------------------------------------------------------------------------
                MarcaSolicitud()

                '-----------------------------------------------------------------------------------------------------------------------
                'Da Formato a Datos desplegados en Grilla
                '-----------------------------------------------------------------------------------------------------------------------
                FormatoGrillaSolicitud()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub MarcaGrilla_GV_Negociacion(ByVal nro As Integer)

        For I = 0 To GV_Negociacion.Rows.Count - 1
            'If I = CInt(HF_Nro_Neg.Value - 1) Then
            If nro = GV_Negociacion.Rows(I).Cells(4).Text Then 'FY 04-05-2012
                'GV_Negociacion.Rows(I).CssClass = "clicktable"
                GV_Negociacion.Rows(I).CssClass = "selectable"
            Else
                GV_Negociacion.Rows(I).CssClass = Nothing
            End If
        Next

    End Sub

#End Region



End Class
