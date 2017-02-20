Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports ClsSession.SesionProrrogas
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Prorrogas_Ingreso_Prorroga
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim FG As New FormulasGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Caption As String = "Solicitud de Prorrogas"
    Dim CuentaDoctosSeleccionados As Int16 = 0
    Dim Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
    Dim OP As New ClaseOperaciones
    Dim PGO As New ClasePagos
    Dim CMC As New ClaseComercial
    Dim CL As New ClaseClientes

#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            NroPaginacion = 0
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_Tip_Doc)

            HabilitaDesabilitaCliente(True)
            HabilitaDesabilitaCreaCXC(False)

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_ComisionProrroga.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_TasaPeriodo.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_FechaApremora.Text = Format(Date.Now, VAR.FMT_FECHA)
            Txt_Rut_Cli.Focus()
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpDeudor',580,410,200,150);")

            IB_Calcular.Enabled = False

        End If

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            If CargaDatosCliente() Then
                '**************** AQUI ************************
                '                CG.NegociacionesAnterioresDevuelde(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                '               If GV_Negociacion.Rows.Count <= 0 Then
                ' Msj.Mensaje(Me.page,Caption, "No posee Documentos según Criterio de Busqueda", TipoDeMensaje._Informacion)
                'End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_Negociacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Negociacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Negociacion, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Negociacion, 'formatable')")
            'e.Row.Attributes.Add("onClick", "ClickSolicitud(ctl00_ContentPlaceHolder1_GV_Negociacion, 'clicktable', 'formatable', 'selectable')")
        End If

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If CargaDatosCliente() Then
                '**************** AQUI ************************
                '                CG.NegociacionesAnterioresDevuelde(Replace(Txt_Rut_Cli.Text, ".", ""), GV_Negociacion)

                '               If GV_Negociacion.Rows.Count <= 0 Then
                ' Msj.Mensaje(Me.page,Caption, "No posee Documentos según Criterio de Busqueda", TipoDeMensaje._Informacion)
                'End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        CargaDatosDeudor()
    End Sub

    Protected Sub txt_fecvcto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fecvcto.TextChanged

        Try

            If txt_fecvcto.Text <> "" Then
                If Not IsDate(txt_fecvcto.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fecvcto.Text = ""
                    Exit Sub
                End If
            End If

         
            HF_Fev_Cal.Value = CG.calcula_vcto_real(Txt_Rut_Deu.Text, txt_fecvcto.Text, Sucursal, "", DP_Tip_Doc.SelectedValue)
            txt_fecVctoReal.Text = FECHA_VCTO_AUX


            Msj.Mensaje(Page, Caption, "Fecha de vcto. variara segun calendarización del pagador", ClsMensaje.TipoDeMensaje._Exclamacion)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Prorroga"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim BTN As ImageButton = CType(sender, ImageButton)

        Try

            If BTN.ToolTip <> "" Then
                For i = 0 To GV_Negociacion.Rows.Count - 1
                    If Trim(Replace(GV_Negociacion.Rows(i).Cells(7).Text, ".", "")) = BTN.ToolTip Then 'FY 08-05-2012
                        HF_Nro_Neg.Value = i + 1
                        Exit For
                    End If
                Next
                marca_grilla(BTN.ToolTip) 'FY 09-05-2012
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CHB_SelDocto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim check As CheckBox

        check = CType(sender, CheckBox)

        For i = 1 To Coll_DocumentosProrroga.Count
            If check.ToolTip = Coll_DocumentosProrroga(i).id_doc Then
                If check.Checked Then
                    If ValidaDocumentoNoEste(Coll_DocumentosProrroga(i).id_doc) Then
                        Coll_DocumentosProrroga_Seleccionados.Add(Coll_DocumentosProrroga(i))
                    End If
                Else
                    QuitaRegistroSeleccionado(Coll_DocumentosProrroga(i).id_doc)
                End If
                Exit Sub
            End If
        Next

    End Sub

    Protected Sub GV_Negociacion_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GV_Negociacion.Sorting

        If e.SortExpression = "Todos" Then

            For I = 0 To GV_Negociacion.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox
                Dim CB As CheckBox
                Dim TD As CheckBox

                CB = CType(GV_Negociacion.Rows(I).FindControl("CHB_SelDocto"), CheckBox)

                If CB.Enabled Then

                    If CB.Checked Then

                        'If EliminaSeleccionDoctos(Coll_DOC.Item(I + 1).cli_idc, Gr_Documentos.Rows(I).Cells(5).Text, Gr_Documentos.Rows(I).Cells(6).Text) Then
                        CB.Checked = False
                        '    Mto_A_Pagar.CssClass = "clsDisabled"
                        '    Mto_A_Pagar.ReadOnly = True
                        '    TD.Checked = False

                        'End If

                    Else

                        CB.Checked = True

                        'If Not ValidaSeleccionDoctos(Coll_DOC.Item(I + 1).cli_idc, Gr_Documentos.Rows(I).Cells(5).Text, Gr_Documentos.Rows(I).Cells(6).Text) Then

                        '    Coll_DOC.Item(I + 1).MontoPagar = CDbl(CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox).Text)
                        '    Coll_DOC.Item(I + 1).Interes = CDbl(Gr_Documentos.Rows(I).Cells(9).Text)
                        '    Coll_DOC.Item(I + 1).Tasa = Pagos.TasaInteresCalculo
                        '    Coll_DOC.Item(I + 1).PagaDeudor = "S"
                        '    Coll_DOC.Item(I + 1).Contrato = Gr_Documentos.Rows(I).Cells(17).Text
                        '    Coll_DOC.Item(I + 1).InteresDevolver = 0

                        '    If Coll_DOC.Item(I + 1).Interes < 0 Then
                        '        Coll_DOC.Item(I + 1).InteresDevolver = Coll_DOC.Item(I + 1).Interes
                        '    End If

                        '    Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(I + 1))

                        'End If

                    End If

                End If

            Next

        End If

        'CalculaTotalPago()
        AgregaDocumentosSeleccionados()

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Buscar.Click 

        Try

            If Not CargaDatosCliente() Then
                Exit Sub
            End If

            If Not CargaDatosDeudor() Then
                Exit Sub
            End If

            If Me.txt_FechaVctoDesde.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar Fecha Desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(txt_FechaVctoDesde.Text) Then
                Msj.Mensaje(Me, "Atención", "Fecha desde Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
                Me.txt_FechaVctoDesde.Text = ""

            End If

            If Date.Parse(txt_FechaVctoDesde.Text).ToString("yyyyMMdd") < Date.Now.ToString("yyyyMMdd") Then
                Msj.Mensaje(Me, "Atención", "Fecha desde incorrecta, no puede ser menor a hoy.", ClsMensaje.TipoDeMensaje._Informacion)
                Me.txt_FechaVctoDesde.Text = ""

            End If

            If Me.txt_FechaVctoHasta.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar Fecha Hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(txt_FechaVctoHasta.Text) Then
                Msj.Mensaje(Me, "Atención", "Fecha hasta Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
                Me.txt_FechaVctoHasta.Text = ""
                Exit Sub
            End If

            If CDate(txt_FechaVctoDesde.Text) > CDate(txt_FechaVctoHasta.Text) Then
                Msj.Mensaje(Me, "Atención", "Fecha desde no puede ser mayor a fecha hasta", ClsMensaje.TipoDeMensaje._Informacion)
                Me.txt_FechaVctoHasta.Text = ""
                Exit Sub
            End If

            If DP_Tip_Doc.SelectedIndex = 0 Then
                Msj.Mensaje(Me, "Atención", "Debe seleccionar un tipo de documento", ClsMensaje.TipoDeMensaje._Informacion)
                Me.DP_Tip_Doc.Focus()
                Exit Sub
            End If

            Dim fc As New FuncionesGenerales.FComunes

            'BorraCollection(Coll_DocumentosProrroga)

            If Not Agt.ValidaAccesso(20, 20010109, Usr, "PRESIONO BOTON BUSCAR DOCUMENTOS") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Rut_Deu.Text = "" Then
                Txt_Rut_Deu.Text = "0"
            End If

            NroPaginacion = 0
            NroPagina = 1
            '-----------------------------------------------------------------------------------------------------------------------
            'Retorna Documentos Para ser Prorrogados 
            '-----------------------------------------------------------------------------------------------------------------------
            Coll_DocumentosProrroga_Seleccionados = New Collection
            Coll_DocumentosProrroga = New Collection

            Coll_DocumentosProrroga = PGO.DocumentosOtorgagosPagos_RetornaDoctos_Prorroga(Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT), Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT), _
                                                                                  Format(CLng(Txt_Rut_Deu.Text), VAR.FMT_RUT), Format(CLng(Txt_Rut_Deu.Text), VAR.FMT_RUT), _
                                                                                  0, 999999999, _
                                                                                  DP_Tip_Doc.SelectedValue, DP_Tip_Doc.SelectedValue, _
                                                                                  "0", "Z", _
                                                                                  0, 999, _
                                                                                  txt_FechaVctoDesde.Text, txt_FechaVctoHasta.Text, _
                                                                                  1, 2, 9, 11, 12, 12, 12, 12, 12, 12, 12, 12)

            '-----------------------------------------------------------------------------------------------------------------------
            'Llena Collection Para ser Ocupada con Posterioridad (Guardar)
            '-----------------------------------------------------------------------------------------------------------------------
            'Coll_DocumentosProrroga = New Collection

            'For Each a In Temporal_dor
            '    Coll_DocumentosProrroga.Add(a)
            'Next

            '-----------------------------------------------------------------------------------------------------------------------
            'Asocia Collection a Grilla de Despliegue y Selección
            '-----------------------------------------------------------------------------------------------------------------------
            GV_Negociacion.DataSource = Coll_DocumentosProrroga
            GV_Negociacion.DataBind()

            If GV_Negociacion.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No posee Documentos según Criterio de Búsqueda", TipoDeMensaje._Informacion)
            Else
                FormatoGrilla()
                HabilitaDesabilitaCreaCXC(True)
                AgregaDocumentosSeleccionados()
                Lbl_Pagina.Text = "Pagina N°: 1"
                IB_Calcular.Enabled = True
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Calcular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Calcular.Click 'Handles IB_Calcular.Click

        If Not Agt.ValidaAccesso(20, 20030109, Usr, "PRESIONO BOTON CALCULAR") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Not IsDate(txt_fecvcto.Text) Then
            Msj.Mensaje(Me, "Atención", "Fecha incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'Valida Info Ingresada Antes de Guardar Gestión
        If Not ValidaGuardaSolicitudProrroga() Then
            Exit Sub
        End If

        '-----------------------------------------------------------------------------------------------------------------------------
        'Calcula Solicitud de Prorroga
        '-----------------------------------------------------------------------------------------------------------------------------Ç
        Msj.Mensaje(Me, "Atención", "¿Desea guardar solicitud de prorroga?", ClsMensaje.TipoDeMensaje._Confirmacion, Lb_guardar.UniqueID)


    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        NroPaginacion = 0
        RefrescaPantalla(True, True, True, True, True)
    End Sub

    Protected Sub IB_GestAnt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GestAnt.Click 'Handles IB_GestAnt.Click

        If Not Agt.ValidaAccesso(20, 20020109, Usr, "PRESIONO BOTON GESTIONES ANTERIORES") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Trim(HF_Nro_Neg.Value) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe hacer click sobre un Documento", TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'Llamar Pantalla de Reporte (Solicitud de Prorroga)
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim StrPagina As String = "GestionesAnteriores.aspx?Item=" & HF_Nro_Neg.Value
        RW.AbrePopup(Me, 1, StrPagina, "RepGesAnteriores", 550, 530, 0, 0)

    End Sub

    Protected Sub Lb_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_guardar.Click

        Try

            Dim ch As CheckBox

            For i = 1 To Coll_DocumentosProrroga_Seleccionados.Count
                If AG.Prorroga_ValidaDocumentos(Coll_DocumentosProrroga_Seleccionados.Item(i).id_doc) Then
                    Msj.Mensaje(Me, "Atención", "Documento N° " & Coll_DocumentosProrroga.Item(i).dsi_num & " ya fue prorrogado en otra solicitud, favor desmarcar documento para continuar.", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            Next

            '------------------------------------------------------------------------------------------------------------------------------
            'Retorna % IVA Para calculo según la comisión por Documento
            '------------------------------------------------------------------------------------------------------------------------------
            Dim iva As Decimal = CG.SistemaDevuelve.sis_iva

            '------------------------------------------------------------------------------------------------------------------------------
            'TRANSACCION
            '------------------------------------------------------------------------------------------------------------------------------
            'Using ts = New TransactionScope

            Dim spg_ingreso As New spg_cls
            Dim ComisionPorDocumento As Double
            Dim IvaComisionPorDocumento As Double
            Dim SumaDoctosSeleccionados As Double
            Dim parTasaMensualAnual As Char
            Dim Lineal As Char

            '-----------------------------------------------------------------------------------------------------------------------------
            'Elimina Solicitud y Detalle de Prorroga con estado 0 para cliente seleccionado
            '-----------------------------------------------------------------------------------------------------------------------------
            'AG.Prorroga_EliminaSolicitudTemporal(Format(CLng(Txt_Rut_Cli.Text.Trim), VAR.FMT_RUT))

            '-----------------------------------------------------------------------------------------------------------------------------
            'Ingreso de Solicitud de Prorroga
            '-----------------------------------------------------------------------------------------------------------------------------

            For i = 1 To Coll_DocumentosProrroga_Seleccionados.Count
                CuentaDoctosSeleccionados = CuentaDoctosSeleccionados + 1
                SumaDoctosSeleccionados = SumaDoctosSeleccionados + CDbl(Coll_DocumentosProrroga_Seleccionados(i).dsi_mto)
            Next

            spg_ingreso.cli_idc = Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT)
            spg_ingreso.spg_fec = txt_FechaApremora.Text.Trim
            spg_ingreso.id_eje_sol = Sesion.CodEje
            spg_ingreso.spg_tas = txt_TasaPeriodo.Text.Trim
            spg_ingreso.spg_com = CLng(txt_ComisionProrroga.Text.Trim)
            spg_ingreso.id_eje_apb = Nothing
            spg_ingreso.spg_obs = txt_observacion.Text.Trim
            spg_ingreso.spg_fec_apb = Nothing
            spg_ingreso.spg_est = 1 'Se cambia estado a 1 para que en vbProrroga aparesca la solicitud ingresada

            AG.Prorroga_GuardaSolicitud(spg_ingreso)

            ComisionPorDocumento = (spg_ingreso.spg_com / CuentaDoctosSeleccionados)

            IvaComisionPorDocumento = (ComisionPorDocumento * (iva / 100))

            If RB_Cal1.Checked = True Then
                Lineal = "S"
            Else
                Lineal = "N"
            End If

            If RB_Int1.Checked = True Then
                parTasaMensualAnual = "M"
            Else
                parTasaMensualAnual = "A"
            End If

            '-----------------------------------------------------------------------------------------------------------------------------
            'Ciclo que recorre Documentos Seleccionados
            '-----------------------------------------------------------------------------------------------------------------------------
            Dim DiaBase As Char = CL.ClienteDevuelvePorRut(Txt_Rut_Cli.Text).cli_dia_bas

            For i = 1 To Coll_DocumentosProrroga_Seleccionados.Count

                Dim dpg_ingreso As New dpg_cls

                dpg_ingreso.id_spg = spg_ingreso.id_spg
                dpg_ingreso.id_doc = Coll_DocumentosProrroga_Seleccionados.Item(i).id_doc
                dpg_ingreso.id_P_0011 = Coll_DocumentosProrroga_Seleccionados.Item(i).id_P_0011
                dpg_ingreso.doc_sdo_cli = Coll_DocumentosProrroga_Seleccionados.Item(i).doc_sdo_cli
                dpg_ingreso.doc_fev_rea = Coll_DocumentosProrroga_Seleccionados.Item(i).doc_fev_rea
                dpg_ingreso.nva_doc_fev_rea = CDate(txt_fecVctoReal.Text.Trim)
                dpg_ingreso.doc_fev_cal = CDate(HF_Fev_Cal.Value)

                dpg_ingreso.dpg_int_ere = Format(FG.DiferenciaDePrecio(dpg_ingreso.nva_doc_fev_rea, _
                                                                       dpg_ingreso.doc_fev_rea, _
                                                                       dpg_ingreso.doc_sdo_cli, _
                                                                       spg_ingreso.spg_tas, _
                                                                       parTasaMensualAnual, _
                                                                       Lineal, _
                                                                       DiaBase), _
                                                 RG.DevuelveFormatoMoneda(Coll_DocumentosProrroga_Seleccionados.Item(i).id_P_0023))

                If Coll_DocumentosProrroga_Seleccionados.Item(i).id_P_0023 <> 1 Then
                    dpg_ingreso.dpg_fac_cam = CG.ParidadDevuelve(Coll_DocumentosProrroga_Seleccionados.Item(i).id_P_0023, _
                                                                 Date.Now).par_val
                Else
                    dpg_ingreso.dpg_fac_cam = 1
                End If

                dpg_ingreso.dpg_com_isi = ((spg_ingreso.spg_com * (dpg_ingreso.doc_sdo_cli / SumaDoctosSeleccionados)) / dpg_ingreso.dpg_fac_cam)
                dpg_ingreso.dpg_iva_com = (IvaComisionPorDocumento / dpg_ingreso.dpg_fac_cam)

                AG.Prorroga_GuardaDetalle(dpg_ingreso)

            Next

            GV_Negociacion.DataSource = Nothing
            GV_Negociacion.DataBind()
            IB_Calcular.Enabled = False

            'ts.Complete()
            'End Using

            '--------------------------------------------------------------------------------------------------------------------------------
            'Llamar Pantalla de Reporte (Solicitud de Prorroga)
            '--------------------------------------------------------------------------------------------------------------------------------
            'Dim StrPagina As String = "Reporte_SolicitudProrroga.aspx?RUTCLIENTE=" & Format(CLng(Txt_Rut_Cli.Text.Trim), VAR.FMT_RUT) & "&IDSPG=" & spg_ingreso.id_spg & "&MES=" & Month(Me.txt_FechaApremora.Text) & "&ESTADO=0"
            'Se cambia estadso a 1 para que rescate registros con estado en 1
            Dim StrPagina As String = "Reporte_SolicitudProrroga.aspx?RUTCLIENTE=" & Format(CLng(Txt_Rut_Cli.Text.Trim), VAR.FMT_RUT) & "&IDSPG=" & spg_ingreso.id_spg & "&MES=" & Month(Me.txt_FechaApremora.Text) & "&ESTADO=1"
            RW.AbrePopup(Me, 1, StrPagina, "RepSolProrroga", 1200, 1024, 50, 0)

            '--------------------------------------------------------------------------------------------------------------------------------
            'Limpia Objetos despues de Calcular Solicitud de Prorroga Excepto Cliente y Criterios
            '--------------------------------------------------------------------------------------------------------------------------------
            RefrescaPantalla(False, False, False, False, True)
            '--------------------------------------------------------------------------------------------------------------------------------
            IB_Buscar_Click(sender, e)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        Dim desde As Integer = NroPaginacion

        
        NroPaginacion += 200
        NroPagina += 1

        CargaGrilla()
        MarcaDocumentosSeleccionados()
        Lbl_Pagina.Text = "Pagina N°: " & NroPagina
        
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        Dim desde As Integer = NroPaginacion

        If NroPaginacion > 1 Then

            NroPaginacion -= 200
            NroPagina -= 1

            CargaGrilla()
            MarcaDocumentosSeleccionados()
            Lbl_Pagina.Text = "Pagina N°: " & NroPagina

        Else
            Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "No existen documentos anteriores", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

#End Region

#Region "Private Function y Sub"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls
        Dim eje As eje_cls

        Try


            CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)


            If valida_cliente <> "" Then

                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Function

            Else
                If IsNothing(CLI) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Function
                End If

                IB_AyudaCli.Enabled = False
                Session("Cliente") = CLI


                'Tipo de cliente (Natural / Juridico)
                If CLI.id_P_0044 = 1 Then
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
                Else
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso
                End If


                'Sucursal
                If Not IsNothing(CLI.suc_cls) Then
                    Me.Txt_Sucursal.Text = CLI.suc_cls.suc_des_cra
                End If

                'Ejecutivo
                'Function para obtener ejecutivo
                eje = ClsCli.EjecutivoDevuelve(CLI.id_eje_cod_eje)
                Txt_Ejecutivo.Text = eje.eje_nom

                'If Not IsNothing(CLI.eje_cls1) Then
                '    Me.Txt_Ejecutivo.Text = CLI.eje_cls1.eje_nom.Trim
                'End If

                HabilitaDesabilitaCliente(False)

                Return True
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Private Function CargaDatosDeudor() As Boolean

        Try

            'No trae cliente
            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Page, "Atención", "Debe Ingresar NIT del Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            'No trae digito
            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje(Page, "Atención", "Debe Ingresar Digito Verificador del Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            'valida 
            'If UCase(Txt_Dig_Deu.Text) <> UCase(RG.Vrut(Txt_Rut_Deu.Text)) Then
            '    Msj.Mensaje(Page, "Atención", "Digito Incorrecto Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Function
            'End If comenta hasta saber que rut se validara 

            Dim deu As deu_cls

            deu = CG.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

            Session("Deudor") = deu

            If Not IsNothing(deu) Then
                'Datos Deudor
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
                Me.Txt_Rso_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.ReadOnly = True
                IB_AyudaDeu.Enabled = False
                Txt_Rut_Deu.Text = Format(CDbl(Txt_Rut_Deu.Text), FMT.FCMSD)
            Else
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                Msj.Mensaje(Page, "Atención", "Pagador no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

            Return True

        Catch ex As Exception
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
        End If


    End Sub

    Private Sub HabilitaDesabilitaCreaCXC(ByVal Estado As Boolean)

        txt_TasaPeriodo.ReadOnly = Not Estado
        'txt_TasaPeriodo_MaskedEditExtender.Enabled = Estado

        txt_ComisionProrroga_MaskedEditExtender.Enabled = Estado
        txt_ComisionProrroga.ReadOnly = Not Estado

        txt_fecvcto.ReadOnly = Not Estado
        CalendarExtender3.Enabled = Estado
        txt_fecvcto_MaskedEditExtender.Enabled = Estado
        txt_observacion.ReadOnly = Not Estado

        RB_Cal1.Enabled = Estado
        RB_Cal2.Enabled = Estado
        RB_Int1.Enabled = Estado
        RB_Int2.Enabled = Estado

        If Not Estado Then
            txt_TasaPeriodo.CssClass = "clsDisabled"
            txt_ComisionProrroga.CssClass = "clsDisabled"
            txt_fecvcto.CssClass = "clsDisabled"
            txt_observacion.CssClass = "clsDisabled"
        Else
            txt_TasaPeriodo.CssClass = "clsMandatorio"
            txt_ComisionProrroga.CssClass = "clsMandatorio"
            txt_fecvcto.CssClass = "clsMandatorio"
            txt_observacion.CssClass = "clsMandatorio"
            txt_ComisionProrroga_MaskedEditExtender.Enabled = True

        End If


    End Sub

    Protected Function ValidaGuardaSolicitudProrroga() As Boolean

        ValidaGuardaSolicitudProrroga = False

        '--------------------------------------------------------------------------------------------------------------------------------
        'Validación Seleccion de Documento
        '--------------------------------------------------------------------------------------------------------------------------------
        For i = 0 To GV_Negociacion.Rows.Count - 1
            Dim varCHKBox As CheckBox

            'Busca Control CheckBox
            varCHKBox = GV_Negociacion.Rows(i).FindControl("CHB_SelDocto")

            'Valida Seleccion
            If varCHKBox.Checked Then
                CuentaDoctosSeleccionados = CuentaDoctosSeleccionados + 1
            End If
        Next

        If CuentaDoctosSeleccionados = 0 Then
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar al menos un documento", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        '--------------------------------------------------------------------------------------------------------------------------------
        'Valida ingreso Tasa Operativa
        '--------------------------------------------------------------------------------------------------------------------------------
        If Trim(txt_TasaPeriodo.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Tasa Operativa", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        '--------------------------------------------------------------------------------------------------------------------------------
        'Valida ingreso Comisión Prorroga
        '--------------------------------------------------------------------------------------------------------------------------------
        If Trim(txt_ComisionProrroga.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Comisión de Prorrogas", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        '--------------------------------------------------------------------------------------------------------------------------------
        'Valida ingreso Nueva Fecha de Vencimiento Prorroga
        '--------------------------------------------------------------------------------------------------------------------------------
        If Trim(txt_fecvcto.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "Ingrese Nueva Fecha de Vencimiento", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        If Trim(txt_fecVctoReal.Text) = "" Then
            Msj.Mensaje(Me.Page, Caption, "No se a Generado el cálculo de la fecha de Vcto.Real ", TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        If CDate(txt_fecvcto.Text) < CDate(Date.Now().ToShortDateString()) Then
            Msj.Mensaje(Me, "Atención", "Nueva fecha de vencimiento no puede ser menor a hoy", ClsMensaje.TipoDeMensaje._Informacion)
            Me.txt_FechaVctoHasta.Text = ""
            Exit Function
        End If

        '--------------------------------------------------------------------------------------------------------------------------------
        'Valida que se ingrese observacion 
        '--------------------------------------------------------------------------------------------------------------------------------
        If Trim(txt_observacion.Text) = "" Then
            Msj.Mensaje(Page, Caption, "Ingrese observación", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        If txt_observacion.Text.Trim.Length > 250 Then
            Msj.Mensaje(Page, Caption, "Observación supera los 250 caracteres, favor corregir.", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Function
        End If

        Dim DiasAntesVctoPro As Integer

        DiasAntesVctoPro = CG.SistemaDevuelve().sis_dia_pro
        '--------------------------------------------------------------------------------------------------------------------------------
        'Valida que Nueva Fecha de Vencimiento sea mayor a la actual fecha de vencimiento de los documentos seleccionados
        '--------------------------------------------------------------------------------------------------------------------------------
        For i = 0 To GV_Negociacion.Rows.Count - 1
            Dim varCHKBox As CheckBox

            'Busca Control CheckBox
            varCHKBox = GV_Negociacion.Rows(i).FindControl("CHB_SelDocto")

            'Valida Seleccion
            If varCHKBox.Checked Then

                If CDate(GV_Negociacion.Rows(i).Cells(10).Text) >= CDate(txt_fecVctoReal.Text) Then
                    'Msj.Mensaje(Me.Page, Caption, "! Atención ¡ Debe Seleccionar Sólo Doctos." & Chr(13) & " con Fecha Venc. Real  Menor a la Nueva Fecha de Venc.", TipoDeMensaje._Exclamacion)
                    Msj.Mensaje(Page, Caption, "Nueva fecha de Vencimiento ingresada,  no puede ser menor al vencimiento del documento seleccionado", TipoDeMensaje._Exclamacion)
                    Exit Function
                End If

                If DateDiff(DateInterval.Day, Now(), CDate(GV_Negociacion.Rows(i).Cells(10).Text)) + 1 < DiasAntesVctoPro Then
                    Msj.Mensaje(Me, "Atención", "No puede superar los dias antes del vcto. para solicitud de prorroga. Para el documento cuyo N° " & GV_Negociacion.Rows(i).Cells(7).Text, ClsMensaje.TipoDeMensaje._Informacion)
                    Me.txt_FechaVctoHasta.Text = ""
                    Exit Function
                End If

            End If

        Next

        ValidaGuardaSolicitudProrroga = True

    End Function

    Protected Sub RefrescaPantalla(ByVal DatosCliente As Boolean, ByVal DatosDeudor As Boolean, ByVal Criterios As Boolean, ByVal ListaDocumentos As Boolean, ByVal CreaCXC As Boolean)

        If DatosCliente Then
            IB_AyudaCli.Enabled = True
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Sucursal.Text = ""
            Txt_Ejecutivo.Text = ""
            txt_fecvcto_MaskedEditExtender.Enabled = False
            txt_ComisionProrroga_MaskedEditExtender.Enabled = False
            CalendarExtender3.Enabled = False
        End If

        If DatosDeudor Then
            IB_AyudaDeu.Enabled = True
            Txt_Rut_Deu.ReadOnly = False
            Txt_Dig_Deu.ReadOnly = False
            Txt_Rut_Deu.CssClass = "clsMandatorio"
            Txt_Dig_Deu.CssClass = "clsMandatorio"
            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""
            Txt_Rso_Deu.Text = ""
        End If

        If Criterios Then
            txt_FechaVctoDesde.Text = ""
            txt_FechaVctoHasta.Text = ""
            DP_Tip_Doc.ClearSelection()
            DP_Tip_Doc.CssClass = "clsMandatorio"
        End If

        If ListaDocumentos Then
            GV_Negociacion.DataSource = Nothing
            GV_Negociacion.DataBind()
        End If

        If CreaCXC Then
            txt_TasaPeriodo.ReadOnly = True
            txt_ComisionProrroga.ReadOnly = True
            txt_fecvcto.ReadOnly = True
            txt_observacion.ReadOnly = True
            txt_TasaPeriodo.CssClass = "clsDisabled"
            txt_ComisionProrroga.CssClass = "clsDisabled"
            txt_fecvcto.CssClass = "clsDisabled"
            txt_observacion.CssClass = "clsDisabled"
            txt_TasaPeriodo.Text = ""
            txt_ComisionProrroga.Text = ""
            txt_fecvcto.Text = ""
            txt_fecVctoReal.Text = ""
            txt_observacion.Text = ""

            RB_Cal1.Checked = False
            RB_Cal2.Checked = True
            RB_Int1.Checked = False
            RB_Int2.Checked = True
            RB_Cal1.Enabled = False
            RB_Cal2.Enabled = False
            RB_Int1.Enabled = False
            RB_Int2.Enabled = False


        End If


    End Sub

    Protected Sub MarcaDocumentosConSolicitud()

        Dim Temporal_spg = CG.Prorroga_DevuelvedocumentosConSolicitud(Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT))

        For Each spg_element In Temporal_spg
            '-----------------------------------------------------------------------------------------------------------------------------
            'Ciclo que recorre Documentos Seleccionados
            '-----------------------------------------------------------------------------------------------------------------------------
            For i = 1 To Coll_DocumentosProrroga.Count
                'i-1 por que la collection empieza de 1 y la grilla de 0
                If spg_element.id_doc = Coll_DocumentosProrroga.Item(i).id_doc Then
                    GV_Negociacion.Rows(i - 1).BackColor = System.Drawing.ColorTranslator.FromHtml("#FAEBD7")

                    GV_Negociacion.Rows(i - 1).CssClass = "formatable"
                    CType(GV_Negociacion.Rows(i - 1).FindControl("CHB_SelDocto"), CheckBox).Enabled = False
                    Exit For
                End If
            Next
        Next
    End Sub

    Protected Sub FormatoGrilla()
        '1-DataField="deu_ide"
        '2-DataField="Deudor">
        '3-DataField="opo_otg">
        '4-DataField="TipoDocto">
        '5-DataField="Moneda">
        '6-DataField="dsi_num">
        '7-DataField="dsi_flj_num">
        '8-DataField="doc_num_ren">
        '9-DataField="doc_fev_rea">
        '10-DataField="dsi_mto">
        '11-DataField="doc_sdo_cli">
        '12-DataField="cco_num">
        '13-DataField="cco_des">
        '14-Dias de Mora>

        '-----------------------------------------------------------------------------------------------------------------------------
        'Ciclo que recorre Documentos Desplegados para su formato
        '-----------------------------------------------------------------------------------------------------------------------------
        Try

            Dim Temporal_spg = CG.Prorroga_DevuelvedocumentosConSolicitud(Format(CLng(Replace(Txt_Rut_Cli.Text, ".", "")), VAR.FMT_RUT))
            Dim check As CheckBox

            For i = 1 To Coll_DocumentosProrroga.Count
                'i-1 por que la collection empieza de 1 y la grilla de 0
                GV_Negociacion.Rows(i - 1).Cells(2).Text = Format(CLng(Coll_DocumentosProrroga.Item(i).deu_ide), FMT.FCMSD) & "-" & Coll_DocumentosProrroga.Item(i).deu_dig_ito 'RG.Vrut(Format(CLng(Coll_DocumentosProrroga.Item(i).deu_ide), FMT.FCMSD))
                GV_Negociacion.Rows(i - 1).Cells(7).Text = Coll_DocumentosProrroga.Item(i).dsi_num
                GV_Negociacion.Rows(i - 1).Cells(10).Text = Format(Coll_DocumentosProrroga.Item(i).doc_fev_rea, "dd/MM/yyyy")

                check = CType(GV_Negociacion.Rows(i - 1).FindControl("CHB_SelDocto"), CheckBox)
                'check.Checked = True

                For Each spg_element In Temporal_spg
                    If spg_element.id_doc = Coll_DocumentosProrroga.Item(i).id_doc Then
                        GV_Negociacion.Rows(i - 1).BackColor = System.Drawing.ColorTranslator.FromHtml("#FAEBD7")
                        check.Enabled = False
                        check.Checked = False
                        Exit For
                    End If
                Next

                Select Case CInt(Coll_DocumentosProrroga.Item(i).id_P_0023)
                    Case 1
                        GV_Negociacion.Rows(i - 1).Cells(11).Text = Format(Coll_DocumentosProrroga.Item(i).dsi_mto, FMT.FCMSD)
                        GV_Negociacion.Rows(i - 1).Cells(12).Text = Format(Coll_DocumentosProrroga.Item(i).doc_sdo_cli, FMT.FCMSD)
                    Case 2
                        GV_Negociacion.Rows(i - 1).Cells(11).Text = Format(Coll_DocumentosProrroga.Item(i).dsi_mto, FMT.FCMCD4)
                        GV_Negociacion.Rows(i - 1).Cells(12).Text = Format(Coll_DocumentosProrroga.Item(i).doc_sdo_cli, FMT.FCMCD4)
                    Case 3, 4
                        GV_Negociacion.Rows(i - 1).Cells(11).Text = Format(Coll_DocumentosProrroga.Item(i).dsi_mto, FMT.FCMCD)
                        GV_Negociacion.Rows(i - 1).Cells(12).Text = Format(Coll_DocumentosProrroga.Item(i).doc_sdo_cli, FMT.FCMCD)
                End Select

                'Si fecha de vencimiento es mayor a fecha actual dias demora en blanco
                If CDate(GV_Negociacion.Rows(i - 1).Cells(10).Text) < CDate(txt_FechaApremora.Text) Then
                    GV_Negociacion.Rows(i - 1).Cells(14).Text = DateDiff(DateInterval.Day, CDate(GV_Negociacion.Rows(i - 1).Cells(10).Text), CDate(txt_FechaApremora.Text))
                    GV_Negociacion.Rows(i - 1).Cells(14).Text = Replace(GV_Negociacion.Rows(i - 1).Cells(14).Text, "-", "")
                End If

                If GV_Negociacion.Rows(i - 1).Cells(14).Text = "" Then
                    GV_Negociacion.Rows(i - 1).Cells(14).Text = 0
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub marca_grilla(ByVal nro As Integer) 'FY 09-05-2012
        Try

            For i = 0 To GV_Negociacion.Rows.Count - 1
                If nro = Trim(Replace(GV_Negociacion.Rows(i).Cells(7).Text, ".", "")) Then
                    If (i Mod 2) = 0 Then
                        GV_Negociacion.Rows(i).CssClass = "selectable"
                    Else
                        GV_Negociacion.Rows(i).CssClass = "selectableAlt"
                    End If
                Else
                    If (i Mod 2) = 0 Then
                        GV_Negociacion.Rows(i).CssClass = "formatUltcell"
                    Else
                        GV_Negociacion.Rows(i).CssClass = "formatUltcellAlt"

                    End If
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CargaGrilla()
        '-----------------------------------------------------------------------------------------------------------------------
        'Retorna Documentos Para ser Prorrogados 
        '-----------------------------------------------------------------------------------------------------------------------
        Dim Temporal_dor = PGO.DocumentosOtorgagosPagos_RetornaDoctos_Prorroga(Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT), Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT), _
                                                                              Format(CLng(Txt_Rut_Deu.Text), VAR.FMT_RUT), Format(CLng(Txt_Rut_Deu.Text), VAR.FMT_RUT), _
                                                                              0, 999999999, _
                                                                              DP_Tip_Doc.SelectedValue, DP_Tip_Doc.SelectedValue, _
                                                                              "0", "Z", _
                                                                              0, 999, _
                                                                              txt_FechaVctoDesde.Text, txt_FechaVctoHasta.Text, _
                                                                              1, 2, 9, 11, 12, 12, 12, 12, 12, 12, 12, 12, 0, 999999999999, 0, NroPaginacion)

        '-----------------------------------------------------------------------------------------------------------------------
        'Llena Collection Para ser Ocupada con Posterioridad (Guardar)
        '-----------------------------------------------------------------------------------------------------------------------
        Coll_DocumentosProrroga = New Collection

        For Each a In Temporal_dor
            Coll_DocumentosProrroga.Add(a)
        Next

        '-----------------------------------------------------------------------------------------------------------------------
        'Asocia Collection a Grilla de Despliegue y Selección
        '-----------------------------------------------------------------------------------------------------------------------
        GV_Negociacion.DataSource = Coll_DocumentosProrroga
        GV_Negociacion.DataBind()

        If GV_Negociacion.Rows.Count <= 0 Then
            Msj.Mensaje(Me.Page, Caption, "No posee Documentos según Criterio de Búsqueda", TipoDeMensaje._Informacion)
        Else
            FormatoGrilla()
            HabilitaDesabilitaCreaCXC(True)
            AgregaDocumentosSeleccionados()
        End If
    End Sub

    Private Sub AgregaDocumentosSeleccionados()

        Dim check As CheckBox

        For i = 0 To GV_Negociacion.Rows.Count - 1

            check = CType(GV_Negociacion.Rows(i).FindControl("CHB_SelDocto"), CheckBox)

            If check.Checked Then

                If Not AG.Prorroga_ValidaDocumentos(Coll_DocumentosProrroga.Item(i + 1).id_doc) Then
                    If ValidaDocumentoNoEste(Coll_DocumentosProrroga(i + 1).id_doc) Then
                        Coll_DocumentosProrroga_Seleccionados.Add(Coll_DocumentosProrroga(i + 1))
                    End If
                Else
                    check.Checked = False
                    check.Enabled = False
                    GV_Negociacion.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#FAEBD7")
                End If

            End If

        Next

    End Sub

    Private Sub MarcaDocumentosSeleccionados()

        Dim check As CheckBox

        For i = 0 To GV_Negociacion.Rows.Count - 1

            check = CType(GV_Negociacion.Rows(i).FindControl("CHB_SelDocto"), CheckBox)

            For x = 1 To Coll_DocumentosProrroga_Seleccionados.Count
                If Coll_DocumentosProrroga(i + 1).dsi_num = Coll_DocumentosProrroga_Seleccionados(x).dsi_num Then
                    check.Checked = True
                    Exit For
                End If
            Next

        Next

    End Sub

    Private Function ValidaDocumentoNoEste(ByVal id_doc As Integer) As Boolean

        Dim I As Integer

        For I = 1 To Coll_DocumentosProrroga_Seleccionados.Count
            If Coll_DocumentosProrroga_Seleccionados(I).id_doc = id_doc Then
                Return False
            End If
        Next

        Return True

    End Function

    Private Sub QuitaRegistroSeleccionado(ByVal id_doc As Integer)
        For i = 1 To Coll_DocumentosProrroga_Seleccionados.Count
            If id_doc = Coll_DocumentosProrroga_Seleccionados(i).id_doc Then
                Coll_DocumentosProrroga_Seleccionados.Remove(i)
                Exit Sub
            End If
        Next
    End Sub

#End Region

    
End Class
