Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_simulation
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim fr As New FormulasGenerales
    Dim var As New FuncionesGenerales.Variables
    Dim vez As Integer
    Dim clasecli As New ClaseClientes
    Dim validacion As Integer
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim sesion As New ClsSession.ClsSession
    Dim fg As New FuncionesGenerales.FComunes
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim pagos As New ClsSession.SesionPagos
    Dim ses_ope As New ClsSession.SesionOperaciones
    Dim msj As New ClsMensaje
    Dim CMC As New ClaseComercial
    Dim OP As New ClaseOperaciones
    Dim CTA As New ClaseCuentas
    Dim PGO As New ClasePagos
    Private GrIdx As Int16
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Agt As New Perfiles.Cls_Principal

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1

            If Not Me.IsPostBack Then

                NroPaginacion = 0
                page_sim = 0
                page_dig = 0
                valida_cliente = ""
                coll_chr = New Collection
                'Asigna Còdigo de Usuario que entra al Sistema
                alinea_textos()
                'Llena DroList con Parámetros de Sistema.
                CargaDrop_Form()
                ses_ope.iniciarSesionOpe()
                pagos.IniciarSesionPagos()

                'Instancia de Colecciones 
                '***************************************************************************
                pagos.coll_egr = New Collection
                pagos.coll_egr_sec = New Collection
                ses_ope.coll_ope = New Collection
                ses_ope.Coll_DOC = New Collection
                coll_chr = New Collection

                '*************************************************************************
                'Inhabilita Campos al entrar por primera vez.
                InhabilitaCampos()

                cg.ParametrosDevuelve(TablaParametro.Moneda, True, Me.Dr_Moneda)
                cg.ParametrosDevuelve(TablaParametro.TipoEgreso, True, Me.Dr_For_Pgo)
                pagos.Pagador = "C"


                Me.Txt_FecSimulacion.Text = Format(Date.Now, "dd/MM/yyyy")

                '*************************************************************************

                Dim COLL_DIARIOS As New Collection

                COLL_DIARIOS = CMC.DatosDiariosDevuelve(CDate(Me.Txt_FecSimulacion.Text))

                If IsNothing(COLL_DIARIOS) = False Then
                    Me.uf.Value = COLL_DIARIOS.Item(1)
                    Me.dolar.Value = COLL_DIARIOS.Item(2)
                    Me.tmc.Value = COLL_DIARIOS.Item(3)
                    'Me.vuf.Text = Format(CDbl(COLL_DIARIOS.Item(1)), fmt.FCMCD4)
                    Me.vdolar.Text = Format(CDbl(COLL_DIARIOS.Item(2)), fmt.FCMCD)
                    Me.vtmc.Text = COLL_DIARIOS.Item(3)
                    Me.Txt_tas_pag.Text = COLL_DIARIOS.Item(3)

                    Me.Txt_Rut_Cli.Focus()
                    pagos.Coll_Cxc_Seleccionados = New Collection
                    pagos.Coll_Doctos_Seleccionados = New Collection
                End If

                Me.Txt_Rut_Cli.Focus()

                If Rb_Sim.Checked = True Then
                    btn_gast_imp.Enabled = False
                    btn_descto.Enabled = False

                End If

                If Txt_FecSimulacion.Text <> "" Then

                    If IsDate(Txt_FecSimulacion.Text) Then

                        pagos.FechaPago = Me.Txt_FecSimulacion.Text
                        pagos.DiasDevolverInteres = Val(cg.SistemaDevuelve().sis_dia_dev)

                        If Not Agt.ValidaAccesso(20, 20040305, Usr, "PRESIONA BOTON  DESCUENTOS") Then
                            Me.btn_descto.Attributes.Add("OnClick", "DenegaAcceso();")
                        Else
                            'Me.btn_descto.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx', window, 'scroll:no;status:off;dialogWidth:1300;dialogHeight:650px;dialogLeft:50px;dialogTop:100px');")
                            Me.btn_descto.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx', window, 'scroll:no;status:off;dialogWidth:1250;dialogHeight:650px;dialogLeft:10px;dialogTop:50px');")
                        End If

                    End If

                End If

                Me.Btn_negoc.Attributes.Add("OnClick", "javascript:VerNegociacion();")

            Else
                LB_RefrescaGastos_Click(New Object, New EventArgs)
            End If



            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Gr_Operaciones_RowDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Operaciones.RowDataBound

    End Sub

    Protected Sub Rb_Sim_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Sim.CheckedChanged

        Try

            limpia_simulacion()

            Dim idx As Int16
            Dim evnt As ImageClickEventArgs
            idx = 0
            Me.Txt_ItemOPE.Text = ""
            ses_ope.coll_ope = New Collection
            ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 3, True, Me.Gr_Operaciones)
            Me.Gr_Operaciones.Controls.Clear()
            If Not IsNothing(ses_ope.coll_ope) Then

                If (ses_ope.coll_ope.Count > 0) = False Then
                    '  Me.Btn_Limpiar_Click(Me, evnt)
                    ' Me.Txt_Rut.Focus()
                    msj.Mensaje(Me, "Atención", "No existen operaciones simuladas para éste cliente", 2)
                    Exit Sub
                End If
            End If

            Gr_Operaciones.DataSource = ses_ope.coll_ope
            Gr_Operaciones.DataBind()

            Me.btn_descto.Enabled = False
            Me.btn_gast_imp.Enabled = False
            Me.Btn_Anu_Sim.Enabled = True
            IB_Imp_Arc.Enabled = True


            marcagrilla()
            InhabilitaCamposopesim()
            Me.Btn_Anu_Sim.Enabled = True
            Me.IB_Imp_Arc.Enabled = True
            Btn_Imp_Sim.Enabled = True
            Me.btn_guardar.Enabled = False
            btn_descto.Enabled = False
            btn_gast_imp.Enabled = False
            Txt_GastImp.Text = ""


        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Rb_dig_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_dig.CheckedChanged
        Try

            Dim evnt As ImageClickEventArgs
            Dim idx As Int16
            idx = 0
            '*************************
            'para que no pase al cargar documentos
            Txt_ItemOPE.Text = ""

            '*************************
            limpia_simulacion()

            ses_ope.coll_ope = New Collection
            page_dig = 0
            ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 2, True, Me.Gr_Operaciones)
            Me.Gr_Operaciones.Controls.Clear()

            If Not IsNothing(ses_ope.coll_ope) Then

                If (ses_ope.coll_ope.Count > 0) = False Then
                    '  Me.Btn_Limpiar_Click(Me, evnt)
                    'Me.Txt_Rut.Focus()
                    msj.Mensaje(Me, "Atención", "No existen operaciones dígitadas para este cliente", 2)
                    limpia_simulacion()
                    Exit Sub
                End If
            End If

            Gr_Operaciones.DataSource = ses_ope.coll_ope
            Gr_Operaciones.DataBind()
            Me.Btn_Anu_Sim.Enabled = False
            Btn_Imp_Sim.Enabled = False
            Me.btn_descto.Enabled = False
            Me.btn_gast_imp.Enabled = False
            IB_Imp_Arc.Enabled = False

            marcagrilla()
            Me.Txt_ItemOPE.Text = ""
            Me.btn_guardar.Enabled = True
            ' InhabilitaCampos()

        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Excepcion)
        End Try

    End Sub

    Protected Sub Txt_Tnego_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Me.Txt_Negocio.Text = Me.Txt_Tnego.Text
    End Sub

    Protected Sub Txt_Tasa_Base_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        CalculaPuntos()
    End Sub

    Protected Sub Txt_Tnego_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        CalculaPuntos()
    End Sub

    Protected Sub Dr_mone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            If Me.Dr_mone.SelectedValue = 1 Then
                Me.Txt_Comi.Text = 0
            Else
                Me.Txt_Comi.Text = "0,00"
            End If
            'ValidaMonedas()

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Dr_Moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            If Me.Dr_Moneda.SelectedValue = 1 Then
                Me.Txt_Min.Text = 0
                Me.Txt_Max.Text = 0
            Else
                Me.Txt_Min.Text = Format(CDbl(0), fmt.FCMSD)
                Me.Txt_Max.Text = Format(CDbl(0), fmt.FCMSD)
            End If
            '   ValidaMonedas()
        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            Me.Lb_buscar_Click(Me, e)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub acceso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles acceso.Click
        Try

            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_FecSimulacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_FecSimulacion.TextChanged
        Try

            If Txt_FecSimulacion.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese fecha de simulación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Txt_FecSimulacion.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha de simulación erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_FecSimulacion.Text = ""
                Exit Sub
            End If

            pagos.FechaPago = Me.Txt_FecSimulacion.Text
            Dim COLL_DIARIOS As New Collection
            COLL_DIARIOS = CMC.DatosDiariosDevuelve(CDate(Me.Txt_FecSimulacion.Text))

            If IsNothing(COLL_DIARIOS) = False Then
                uf.Value = COLL_DIARIOS.Item(1)
                'Me.vuf.Text = COLL_DIARIOS.Item(1)
                dolar.Value = COLL_DIARIOS.Item(2)
                Me.vdolar.Text = COLL_DIARIOS.Item(2)
                tmc.Value = COLL_DIARIOS.Item(3)
                Me.vtmc.Text = COLL_DIARIOS.Item(3)
                Me.Txt_tas_pag.Text = Me.vtmc.Text
                Me.Txt_Rut_Cli.Focus()

            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub Txt_tas_pag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_tas_pag.TextChanged
        Try

            If Me.Txt_tas_pag.Text <> "" Then
                pagos.TasaInteresCalculo = CDec(Val(Me.Txt_tas_pag.Text))
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If Rb_dig.Checked Then
            If page_dig = 0 Then
                msj.Mensaje(Me, "Atencion", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            If page_dig >= 5 Then
                page_dig -= 5
                CargaCliente()
            End If
        End If


        If Rb_Sim.Checked = True Then
            If page_sim = 0 Then
                msj.Mensaje(Me, "Atencion", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            If page_sim >= 5 Then
                page_sim -= 5
                'Rb_Sim_CheckedChanged(Me, e)
                CargaOpeSimuladas()
            End If
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If Gr_Operaciones.Rows.Count < 5 Then
            msj.Mensaje(Me, "Atencion", "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If Rb_dig.Checked Then
            If Gr_Operaciones.Rows.Count = 5 Then
                page_dig += 5
                CargaCliente()
            End If
        End If


        If Rb_Sim.Checked = True Then
            If Gr_Operaciones.Rows.Count = 5 Then
                page_sim += 5
                'Rb_Sim_CheckedChanged(Me, e)
                CargaOpeSimuladas()
            End If
        End If


    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img As ImageButton = CType(sender, ImageButton)
        btn_guardar.Enabled = False

        For i = 0 To Gr_Operaciones.Rows.Count - 1
            If CType(Gr_Operaciones.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip = img.ToolTip Then
                Txt_ItemOPE.Text = i + 1
                Exit For
            End If
        Next

        HF_N_Ope.Value = img.ToolTip
        HF_N_Opn.Value = img.AlternateText.ToString()
        HF_Pos.Value = Txt_ItemOPE.Text

        Try

            If Txt_FecSimulacion.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese fecha de simulación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Txt_FecSimulacion.Text) Then
                msj.Mensaje(Page, "Atención", "Fecha de simulación erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_FecSimulacion.Text = ""
                Exit Sub
            End If

            ses_ope.Coll_DSI = New Collection

            Dim EstadoOperacion As New Integer

            If Rb_Sim.Checked = True Then
                EstadoOperacion = 2
                Rb_mora.Enabled = False
            ElseIf Rb_dig.Checked = True Then
                EstadoOperacion = 1
                Rb_mora.Enabled = False
            End If

            'Si collecion esta vacia la vuelvo a llenar
            If Not IsNothing(ses_ope.coll_ope) Then
                If ses_ope.coll_ope.Count = 0 Then
                    ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Format(CDbl(Txt_Rut_Cli.Text), "000000000000"), (EstadoOperacion), False, Me.Gr_Operaciones)
                End If
            End If

            Txt_FecSimulacion.Text = ses_ope.coll_ope(1).ope_fec_sim

            ses_ope.Coll_DSI = OP.documentosIngresados_Retorna(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope, _
                                                               ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope)

            Me.Gr_Documentos.DataSource = ses_ope.Coll_DSI
            Me.Gr_Documentos.DataBind()

            pagos.FechaPago = Format(CDate(Txt_FecSimulacion.Text), "dd-MM-yyyy")

            For i = 1 To ses_ope.Coll_DSI.Count
                Dim DSI = New Object

                DSI = ses_ope.Coll_DSI.Item(i)

                With DSI

                    If IsNothing(DSI.cal_oto_gam) Or DSI.cal_oto_gam = "" Then
                        msj.Mensaje(Me, "Atención", "El Documento N° " & Format(DSI.dsi_num, fmt.FSMSD) & " no tiene calificacion de otorgamiento ingresada", 2)
                        Exit Sub
                    Else
                        If DSI.dsi_fev_rea <= CDate(Me.Txt_FecSimulacion.Text) Then

                            validacion = 1
                            msj.Mensaje(Me, "Atención", "El Documento N° " & Format(DSI.dsi_num, fmt.FSMSD) & " se encuentra vencido a la fecha de Simulación", 2)

                            limpia_simulacion()
                            marcagrilla()

                            If validacion = 0 Then
                                btn_guardar.Enabled = False
                                btn_descto.Enabled = False
                                btn_gast_imp.Enabled = False
                                IB_Imp_Arc.Enabled = False
                            End If

                            Exit Sub

                        Else

                            If Rb_dig.Checked = True Then
                                btn_guardar.Enabled = True
                                Btn_Anu_Sim.Enabled = False
                            Else
                                Btn_Anu_Sim.Enabled = True
                            End If

                        End If
                    End If
                End With

            Next

            cargadocs()

            If validacion = 1 Then
                btn_guardar.Enabled = False
                btn_descto.Enabled = False
                btn_gast_imp.Enabled = False
                IB_Imp_Arc.Enabled = False
            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Buscar.Click
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20010305, Usr, "PRESIONA BOTON BUSCAR OPERACIONES") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            CargaCliente()

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Imp_Arc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imp_Arc.Click

        Try

            If HF_N_Ope.Value <> "" Then
                rw.AbrePopup(Me.Page, 1, "Reportes_de_Simulacion.aspx?ID_OPE=" & HF_N_Ope.Value, "Documentos", 1000, 700, 10, 10)
            Else
                msj.Mensaje(Me, "Atención", "Debe seleccionar una operación", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpiar.Click
        IB_AyudaCli.Enabled = True
        Me.Txt_Negocio.Text = ""
        Me.Txt_Comflat.Text = ""
        Txt_ComDocto.Text = ""
        Txt_Dsctos.Text = ""
        Txt_GastImp.Text = ""
        Txt_Antic.Text = ""
        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.Enabled = True
        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
        Me.Txt_Rut_Cli.ReadOnly = False
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
        Me.Txt_Dig_Cli.Enabled = True
        Me.Txt_Dig_Cli.ReadOnly = False

        Me.Txt_Raz_Soc.Text = ""

        'Tasas
        Me.Txt_Tasa_Base.Text = ""
        Me.Txt_Tasa_Base.CssClass = "clsDisabled"
        Me.Txt_Tasa_Base.ReadOnly = True
        Me.Txt_Spread.Text = ""

        Me.Txt_Puntos.Text = ""

        Me.Txt_Tnego.Text = ""

        Me.Txt_Tnego.CssClass = "clsDisabled"
        Me.Txt_Tnego.ReadOnly = True
        'Comisiones
        Me.Txt_Comi.Text = ""
        Me.Txt_Comi.CssClass = "clsDisabled"
        Me.Txt_Comi.ReadOnly = True

        Me.Txt_Max.Text = ""
        Me.Txt_Max.CssClass = "clsDisabled"
        Me.Txt_Max.ReadOnly = True



        Me.Txt_Min.Text = ""
        Me.Txt_Min.CssClass = "clsDisabled"
        Me.Txt_Min.ReadOnly = True
        'Anticipo
        Me.Txt_Porc_Antic.Text = ""
        Me.Txt_Porc_Antic.CssClass = "clsDisabled"
        Me.Txt_Porc_Antic.ReadOnly = True
        'Pago a Cliente
        Me.Txt_Cta_Cte.Text = ""
        Me.Txt_Cta_Cte.CssClass = "clsDisabled"
        Me.Txt_Cta_Cte.ReadOnly = True
        Me.Txt_Porc_com.Text = ""
        Me.Txt_Porc_com.CssClass = "clsDisabled"
        Me.Txt_Porc_com.ReadOnly = True


        'DropList
        Me.Dr_Moneda.ClearSelection()
        Me.Dr_mone.CssClass = "clsDisabled"
        Me.Dr_Moneda.Enabled = False
        Me.Dr_Moneda.CssClass = "clsDisabled"
        Me.Dr_Bco.CssClass = "clsDisabled"
        Me.Dr_mone.ClearSelection()
        Me.Dr_mone.Enabled = False
        Me.Dr_mone.CssClass = "clsDisabled"
        Me.Dr_For_Pgo.ClearSelection()
        Me.Dr_For_Pgo.Enabled = False
        Me.Dr_For_Pgo.CssClass = ""
        Me.Dr_For_Pgo.CssClass = "clsDisabled"
        Me.Dr_Bco.ClearSelection()
        Me.Dr_Bco.Enabled = False
        Me.Dr_Bco.CssClass = "clsDisabled"

        'RadioButton
        Me.Ch_Ats_14hrs.Checked = False
        Me.Ch_Ats_14hrs.Enabled = False
        Me.Rb_dig.Checked = True
        Me.Rb_dig.Enabled = False
        Me.Rb_Sim.Checked = False
        Me.Rb_Sim.Enabled = False
        Me.Btn_Buscar.Enabled = True
        Me.btn_descto.Enabled = False
        Me.Gr_Operaciones.Controls.Clear()
        Me.Gr_Documentos.Controls.Clear()
        ses_ope.coll_ope = New Collection
        ses_ope.Coll_DOC = New Collection
        Coll_Doctos_Seleccionados = New Collection
        Coll_Cxc_Seleccionados = New Collection
        Txt_ItemOPE.Text = ""
        Me.Txt_Rut_Cli.Focus()
        coll_chr = New Collection
        coll_ope = New Collection
        btn_gast_imp.Enabled = False
        btn_guardar.Enabled = False

        HF_Mto.Value = ""
        HF_NroNeg.Value = ""
        limpia_simulacion()
        page_dig = 0
        page_sim = 0

    End Sub

    Protected Sub btn_calc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            VALIDAOPERACION()

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn_guardar.Click

        Try

            If IsNothing(coll_ope) Then
                If coll_ope.Count = 0 Then
                    msj.Mensaje(Page, "Atención", "Seleccione operación para guardar", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            'Se valida que se seleccione una operacion para simular
            If Txt_ItemOPE.Text = "" Then
                msj.Mensaje(Page, "Atención", "Seleccione operación para simular", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'A.Morales no debe permitir simular con saldo negativo.
            If Txt_saldo_total.Text < 0 Then
                msj.Mensaje(Page, "Atención", "No se puede simular operación, monto a girar debe ser mayor a cero", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'J.Lagos valida cupos de pagadores
            Dim idx As Integer
            Dim coll_pagadores As New Collection

            coll_pagadores = OP.DevuelveDeudoresDeUnaOperacion(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_OPE, pagos.Coll_Doctos_Seleccionados)

            For idx = 1 To coll_pagadores.Count

                If coll_pagadores(idx).montodoc > coll_pagadores(idx).disponible Then
                    msj.Mensaje(Page, "Atención", "No se puede simular operación, Cupo del Pagador quedaría sobregirada NIT " & coll_pagadores(idx).deudor, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            Next

            If Txt_saldo_total.Text < 0 Then
                msj.Mensaje(Page, "Atención", "No se puede simular operación, monto a girar debe ser mayor a cero", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Try

                If Txt_saldo_total.Text > 0 Then

                    Dim CD As New ClaseControlDual

                    If CMC.RetornaResponsabilidad(Convert.ToInt32(HF_N_Opn.Value)) = True Then
                        If Not CD.ValidaSobregiroDeLineaDelCliente(HF_N_Ope.Value, Txt_saldo_total.Text) Then
                            msj.Mensaje(Me.Page, "Atención", "La operación no puede ser simulada por que la línea de financiamiento del cliente quedara sobregirada (supera % de exceso).", TipoDeMensaje._Exclamacion)
                            validacion = 1 '(Si validacion es 1 no carga datos Detalle simulacion y se cae al simular)
                            Exit Sub '(Se deja comentariado para cargar datos)
                        End If
                    End If

                End If

                '2015-10-08 jlagos
                If Not OP.ValidaCalculosDeOperacion(HF_N_Opn.Value) Then
                    msj.Mensaje(Me.Page, "Atención", "La operación no cuadra los calculos de descuentos", TipoDeMensaje._Exclamacion)
                    validacion = 1 '(Si validacion es 1 no carga datos Detalle simulacion y se cae al simular)
                    Exit Sub '(Se deja comentariado para cargar datos)
                End If

            Catch ex As Exception

            End Try

            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            'valida lista de chequeo que al menos 2 item tenga aprobados (Versión: 12122013.V1)
            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            Dim valida_confirmacion As Int16
            Dim doctos_ope As IQueryable = cg.DocConDevuelvePorNegociacion(HF_N_Opn.Value, 2)
           
            For Each d In doctos_ope
                If d.estado = "A" Then
                    valida_confirmacion += 1
                End If
            Next

            If valida_confirmacion < 2 Then
                msj.Mensaje(Me.Page, "Atención", "Debe tener al menos 2 documentos de operación confirmados para simular...", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20020305, Usr, "PRESIONA BOTON GUARDAR SIMULACIÓN") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            If ClsSession.SesionOperaciones.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                Mto_a_Girar = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FSMCD)
                Mto_ant_operacion = Format(CDbl(Me.Txt_Monto_anticipar.Text), fmt.FSMCD)
            Else
                Mto_a_Girar = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FSMSD)
                Mto_ant_operacion = Format(CDbl(Me.Txt_Monto_anticipar.Text), fmt.FSMSD)
            End If

            If GUARDA_SIMULACION() Then

                ses_ope.ID_OPE_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_OPE
                ses_ope.RUT_CLI_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).CLI_IDC


                'para que actualize grilla Gr_Operaciones con simuladas
                limpia_simulacion()
                ses_ope.coll_ope = New Collection

                ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT), 3)

                Me.Gr_Operaciones.DataSource = ses_ope.coll_ope
                Me.Gr_Operaciones.DataBind()

                Me.Txt_ItemOPE.Text = ""
                btn_descto.Enabled = False
                btn_gast_imp.Enabled = False
                marcagrilla()

                ' COMPARA_OTORGAMIENTO_NEG()
                'limpia_simulacion()
                Me.btn_guardar.Enabled = False
                Me.Btn_Imp_Sim.Enabled = True

                Dim sim_arg As New System.EventArgs
                Me.Rb_dig.Checked = False
                Rb_Sim.Checked = True
                Rb_Sim_CheckedChanged(Rb_Sim, sim_arg)
                btn_descto.Enabled = False
                btn_gast_imp.Enabled = False

                AbrePopup(Me, 1, "reporte_sim.aspx", "Informes", 1000, 900, 100, 0)

                Dim rut As Long
                Dim dig As Char
                Dim x As System.EventArgs

                rut = CLng(Txt_Rut_Cli.Text)
                dig = Txt_Dig_Cli.Text

                Btn_Limpiar_Click(Me, e)

                Txt_Rut_Cli.Text = rut
                Txt_Dig_Cli.Text = dig
                Txt_Dig_Cli_TextChanged(Me, x)

            End If


        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Btn_Anu_Sim_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20030305, Usr, "PRESIONA BOTON ANULAR SIMULACIÓN") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Rb_Sim.Checked Then
                Dim b As Boolean

                If Me.Txt_ItemOPE.Text <> "" Then

                    msj.Mensaje(Me, "Atención", "¿ Desea anular la simulación ?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_anu.UniqueID)
                    Exit Sub
                Else

                    msj.Mensaje(Me, "Atención", "Debe seleccionar una operación simulada para poder anular", 2)
                    Exit Sub
                End If

            Else
                msj.Mensaje(Me, "Atención", "Debe seleccionar una operación simulada para poder anular", 2)
                Exit Sub
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Btn_Imp_Sim_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Imp_Sim.Click

        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Me.Txt_ItemOPE.Text = "" Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar una simulación para generar el Reportes", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Not Agt.ValidaAccesso(20, 20060305, Usr, "PRESIONA BOTON IMPRIMIR SIMULACIÓN") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            ses_ope.ID_OPE_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_OPE
            ses_ope.RUT_CLI_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).CLI_IDC

            AbrePopup(Me, 1, "reporte_sim.aspx", "Informes", 1100, 900, 100, 0)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_simula_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btn_simula.Click
        Try

            If Val(Me.Txt_Negocio.Text) <= 0 Then

                caso = 3
                validacion = 1

            End If
            simulacion(3)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_det_ope_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_det_ope.Click
        Try

            If Me.Txt_ItemOPE.Text = "" Then
                Exit Sub
            End If
            cargadetalle()
            Me.ModalPopupExtender1.Show()

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_gast_imp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gast_imp.Click

        If Not Agt.ValidaAccesso(20, 20050305, Usr, "PRESIONA BOTON GASTOS SIMULACIÓN") Then
            msj.Mensaje(Page, "Atencion", "Acceso Denegado", ClsMensaje.TipoDeMensaje._Error)
        Else

            If Not IsNothing(coll_ope) Then
                If ses_ope.coll_ope.Count > 0 Then
                    If Me.Txt_ItemOPE.Text <> "" Then
                        If Me.Rb_dig.Checked Then
                            rw.AbrePopup(Page, 2, "gastos.aspx?id_ope=" & coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope & _
                                                             "&id_opn=" & coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_opn & _
                                                             "&can_doc=" & coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).opn_can_doc, _
                                         "PopUpGastos", 750, 400, 100, 0)
                        End If
                    End If
                End If
            End If

        End If

    End Sub

#End Region

#Region "LinkButton"

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Me.ModalPopupExtender2.Show()
    End Sub

    Protected Sub LB_RefrescaGastos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '(Para que muestre los gastos si es digitada o simulada)
        If Me.Txt_ItemOPE.Text <> "" Then
            calcula_dctos()
            calcula_gtos()
        End If
    End Sub

    Protected Sub LB_CargaOpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If ope_est.Value = 1 Then
                Me.Rb_dig.Checked = True
                If Me.Rb_Sim.Checked Then
                    Me.Rb_Sim.Checked = False
                End If

            ElseIf ope_est.Value = 2 Then

                If Rb_dig.Checked = True Then
                    Me.Rb_dig.Checked = False
                End If

                Me.Rb_Sim.Checked = True
            End If

            'Me.Lb_buscar_Click(Me.Lb_buscar, e)
            Me.CargaCliente()

            If Me.Rb_Sim.Checked Then
                Me.Rb_Sim_CheckedChanged(Rb_Sim, e)

            ElseIf Me.Rb_dig.Checked Then
                Me.Rb_dig_CheckedChanged(Rb_dig, e)
            End If

            Dim f As Integer
            Dim g As Integer
            For f = 0 To Me.Gr_Operaciones.Rows.Count - 1
                If Me.Gr_Operaciones.Rows(f).Cells(0).Text = num_ope.Value Then
                    Me.Txt_ItemOPE.Text = f + 1
                    Exit For
                End If
            Next


        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub lb_anu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_anu.Click
        Try

            Dim b As Boolean

            b = OP.Simulación_Anula(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope)
            If b = True Then

                msj.Mensaje(Me, "Atención", "Se ha anulado la simulación", 2)
            End If
            limpia_simulacion()

            ses_ope.coll_ope = New Collection

            ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT), 3)
            Me.Gr_Operaciones.DataSource = ses_ope.coll_ope
            Me.Gr_Operaciones.DataBind()
            Me.Txt_ItemOPE.Text = ""
            btn_descto.Enabled = False
            btn_gast_imp.Enabled = False

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

#Region "Sub y Function"

    Private Sub CargaDrop_Form()
        Try


            'Parámetro Moneda
            cg.ParametrosDevuelve(23, True, Dr_Moneda)
            cg.ParametrosDevuelve(23, True, Dr_mone)

            'FORMA DE PAGO
            cg.ParametrosDevuelve(TablaParametro.TipoEgreso, True, Me.Dr_For_Pgo)


        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub InhabilitaCampos()
        'TabStrip

        'TextBox
        'Validadores

        'Campos a ingresar

        Me.Txt_Tasa_Base.CssClass = "clsDisabled"
        Me.Txt_Tasa_Base.ReadOnly = True
        Me.Txt_Tnego.CssClass = "clsDisabled"
        Me.Txt_Tnego.ReadOnly = True

        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.ReadOnly = False
        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.ReadOnly = False
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
        Me.Txt_Raz_Soc.Text = ""
        Me.Txt_Raz_Soc.ReadOnly = True
        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
        'Tasas
        Me.Txt_Porc_com.Text = ""
        Me.Txt_Porc_com.CssClass = "clsDisabled"
        Me.Txt_Porc_com.ReadOnly = True
        Me.Txt_Tasa_Base.Text = ""
        Me.Txt_Tasa_Base.ReadOnly = True
        Me.Txt_Tasa_Base.CssClass = "clsDisabled"
        Me.Txt_Spread.Text = ""
        Me.Txt_Spread.ReadOnly = True
        Me.Txt_Spread.CssClass = "clsDisabled"
        Me.Txt_Puntos.Text = ""
        Me.Txt_Puntos.ReadOnly = True
        Me.Txt_Puntos.CssClass = "clsDisabled"
        Me.Txt_Tnego.Text = ""
        Me.Txt_Tnego.ReadOnly = True
        Me.Txt_Tnego.CssClass = "clsDisabled"
        'Comisiones
        Me.Txt_Comi.Text = ""
        Me.Txt_Comi.ReadOnly = True
        Me.Txt_Comi.CssClass = "clsDisabled"
        Me.Txt_Max.Text = ""
        Me.Txt_Max.ReadOnly = True
        Me.Txt_Max.CssClass = "clsDisabled"
        Me.Txt_Min.Text = ""
        Me.Txt_Min.ReadOnly = True
        Me.Txt_Min.CssClass = "clsDisabled"

        'Anticipo
        Me.Txt_Porc_Antic.Text = ""
        Me.Txt_Porc_Antic.ReadOnly = True
        Me.Txt_Porc_Antic.CssClass = "clsDisabled"
        'Pago a Cliente
        Me.Txt_Cta_Cte.Text = ""
        Me.Txt_Cta_Cte.ReadOnly = True
        Me.Txt_Cta_Cte.CssClass = "clsDisabled"

        'DropList
        Me.Dr_Moneda.ClearSelection()
        Me.Dr_Moneda.Enabled = False
        Me.Dr_Moneda.CssClass = "clsDisabled"
        Me.Dr_mone.ClearSelection()
        Me.Dr_mone.Enabled = False
        Me.Dr_mone.CssClass = "clsDisabled"
        Me.Dr_For_Pgo.ClearSelection()
        Me.Dr_For_Pgo.Enabled = False
        Me.Dr_For_Pgo.CssClass = "clsDisabled"
        Me.Dr_Bco.ClearSelection()
        Me.Dr_Bco.Enabled = False
        Me.Dr_Bco.CssClass = "clsDisabled"

        'RadioButton
        Me.Ch_Ats_14hrs.Checked = False
        Me.Ch_Ats_14hrs.Enabled = False


        'Button
        Me.Btn_Buscar.Enabled = True
        Me.Btn_Limpiar.Enabled = True
        Me.Btn_Ope.Enabled = True

        IB_Imp_Arc.Enabled = False
        Me.Btn_negoc.Enabled = False
        Me.Btn_Anu_Sim.Enabled = False
        Me.Btn_Ing_pag.Enabled = False
        Me.Btn_Imp_Sim.Enabled = False
        Me.Btn_Asoc.Enabled = False

        'Collections

        BorraCollection(ses_ope.coll_ope)
        BorraCollection(ses_ope.Coll_DSI)
        BorraCollection(sesion.Coll_DEU)

        'DataGrid
        Me.Gr_Operaciones.SelectedIndex = 0
        Me.Gr_Operaciones.DataSource = ses_ope.coll_ope
        Me.Gr_Operaciones.DataBind()
    End Sub

    Private Sub calcula_gtos()
        Try

            'Validación de Descuentos por Gastos
            '******************************************************************************************************************
            Me.Txt_GastImp.Text = 0
            Txt_Gastos.Text = ""
            HF_Mto.Value = 0

            Dim ope As New ope_cls

            If HF_NroNeg.Value <> "" Then

                ope = OP.OperacionDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), HF_NroNeg.Value)

                If Not IsNothing(ope) Then

                    Txt_GastImp.Text = OP.gastos_ope_calcula(ope.id_opn)

                    Txt_Gastos.Text = OP.GastosExento
                    Txt_GastosAfectos.Text = OP.GastoAfecto
                    Txt_GastImp.Text = CDbl(Txt_Gastos.Text) + CDbl(Txt_GastosAfectos.Text) '+ (CDbl(Txt_GastosAfectos.Text) * (CMC.DatosDeSistemaDevuelve.sis_iva / 100))

                    If IsNothing(ope.ope_mon_gas) Or ope.ope_mon_gas = 0 Then
                        HF_Mto.Value = ""
                        HF_Imp.Value = ""
                    Else
                        HF_Mto.Value = Txt_GastImp.Text
                    End If

                    If IsNothing(ope.ope_imp_ope) Or ope.ope_imp_ope = 0 Then
                        HF_Imp.Value = ""
                    Else
                        HF_Imp.Value = ope.ope_imp_ope
                    End If

                    If Txt_GastImp.Text <> "" Then
                        If ope.opn_cls.id_P_0023 > 2 Then
                            Txt_GastImp.Text = Format(CDbl(Txt_GastImp.Text), fmt.FCMCD)
                            Txt_Gastos.Text = Format(CDbl(Txt_Gastos.Text), fmt.FCMCD)
                            Txt_GastosAfectos.Text = Format(CDbl(Txt_GastosAfectos.Text), fmt.FCMCD)
                        ElseIf ope.opn_cls.id_P_0023 = 2 Then
                            Txt_GastImp.Text = Format(CDbl(Txt_GastImp.Text), fmt.FCMCD4)
                            Txt_Gastos.Text = Format(CDbl(Txt_Gastos.Text), fmt.FCMCD4)
                            Txt_GastosAfectos.Text = Format(CDbl(Txt_GastosAfectos.Text), fmt.FCMCD4)
                        Else
                            Txt_GastImp.Text = Format(CDbl(Txt_GastImp.Text), fmt.FCMSD)
                            Txt_Gastos.Text = Format(CDbl(Txt_Gastos.Text), fmt.FCMSD)
                            Txt_GastosAfectos.Text = Format(CDbl(Txt_GastosAfectos.Text), fmt.FCMSD)
                        End If
                    End If

                    'If txt_montos_doctos.Text <> "" Then
                    '    Txt_Gastos.Text = Txt_GastImp.Text
                    'End If

                    If Txt_ItemOPE.Text = "" Then
                        Exit Sub
                    End If

                End If
                If Txt_Comflat.Text <> "" Then
                    If Not IsNothing(ses_ope.coll_ope) Then
                        If ses_ope.coll_ope.Count > 0 Then
                            Select Case ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Text)).id_p_0023_fla
                                Case 1 'PESO
                                    Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMSD)
                                Case 2 'UF
                                    Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMCD4)
                                Case 3, 4
                                    Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMCD)
                            End Select
                        End If
                    End If
                End If
            End If

            If Not IsNothing(coll_chr) Then

                If coll_chr.Count > 0 Then

                    If Txt_ItemOPE.Text = "" Then
                        Exit Sub
                    End If

                    If coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                        Me.Txt_GastImp.Text = Format(CDbl(Me.Txt_GastImp.Text), fmt.FCMSD)
                    ElseIf coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 2 Then
                        Me.Txt_GastImp.Text = Format(CDbl(Me.Txt_GastImp.Text), fmt.FCMCD)
                    Else
                        Me.Txt_GastImp.Text = Format(CDec(Me.Txt_GastImp.Text), fmt.FCMCD4)
                    End If

                End If

            End If

            ''Simula solo si tiene numero de operacion
            'If HF_NroNeg.Value <> "" Then
            '    simulacion(3)
            'End If

            If Txt_Comflat.Text <> "" Then
                Txt_Comflat.Text = Txt_Comi.Text
            End If

            If Txt_Dsctos.Text = "0,0000" Then
                Txt_Dsctos.Text = 0
            End If


        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub calcula_dctos()

        Try

            'Validación de Descuentos por Documentos  y calculos correspondientes

            '******************************************************************************************************************
            Dim fmto As String
            If Not IsNothing(ses_ope.coll_ope) And Me.Txt_ItemOPE.Text <> "" Then
                If ses_ope.coll_ope.Count > 0 Then


                    If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                        fmto = fmt.FCMSD
                    ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                        fmto = fmt.FCMCD4
                    Else
                        fmto = fmt.FCMCD

                    End If

                End If
            End If

            If Not IsNothing(pagos.Coll_Doctos_Seleccionados) Then
                Me.Txt_Dsctos.Text = 0
                If pagos.Coll_Doctos_Seleccionados.Count > 0 Then

                    For i = 1 To pagos.Coll_Doctos_Seleccionados.Count

                        If pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023 <> ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 Then


                            If pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023 <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 = 1 Then

                                Me.Txt_Dsctos.Text = Val(Me.Txt_Dsctos.Text) + ((pagos.Coll_Doctos_Seleccionados.Item(i).montopagar) * (cg.ParidadDevuelve(pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023, pagos.Coll_Doctos_Seleccionados.Item(i).dsi_fec_emi).par_val))

                            ElseIf pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023 = 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 <> 1 Then

                                Me.Txt_Dsctos.Text = Val(Me.Txt_Dsctos.Text) + ((pagos.Coll_Doctos_Seleccionados.Item(i).montopagar / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM))

                            ElseIf pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023 <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 <> 1 Then

                                Me.Txt_Dsctos.Text = Val(Me.Txt_Dsctos.Text) + ((pagos.Coll_Doctos_Seleccionados.Item(i).montopagar * (cg.ParidadDevuelve(pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023, pagos.Coll_Doctos_Seleccionados.Item(i).dsi_fec_emi).par_val))) / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM

                            Else
                                Me.Txt_Dsctos.Text = Val(Me.Txt_Dsctos.Text) + (pagos.Coll_Doctos_Seleccionados.Item(i).montopagar * (cg.ParidadDevuelve(pagos.Coll_Doctos_Seleccionados.Item(i).id_p_0023, pagos.Coll_Doctos_Seleccionados.Item(i).dsi_fec_emi).par_val))

                            End If


                        Else

                            Me.Txt_Dsctos.Text = Val(Me.Txt_Dsctos.Text) + pagos.Coll_Doctos_Seleccionados.Item(i).montopagar
                        End If
                    Next
                    simulacion(3)
                End If
            End If

            '******************************************************************************************************************
            'Validación de Descuentos por CXC  y calculos correspondientes
            '******************************************************************************************************************

            Dim Totalcxc As Double
            Dim total As Double

            If Not IsNothing(pagos.Coll_Cxc_Seleccionados) Then

                If pagos.Coll_Cxc_Seleccionados.Count > 0 Then

                    For i = 1 To pagos.Coll_Cxc_Seleccionados.Count

                        If pagos.Coll_Cxc_Seleccionados.Item(i).id_p_0023 <> ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 Then


                            If pagos.Coll_Cxc_Seleccionados.Item(i).id_p_0023 <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 = 1 Then

                                Totalcxc = Totalcxc + ((pagos.Coll_Cxc_Seleccionados.Item(i).montopagar * pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam))

                            ElseIf pagos.Coll_Cxc_Seleccionados.Item(i).id_p_0023 = 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 <> 1 Then

                                Totalcxc = Totalcxc + ((pagos.Coll_Cxc_Seleccionados.Item(i).montopagar / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM))

                            ElseIf pagos.Coll_Cxc_Seleccionados.Item(i).id_p_0023 <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_P_0023 <> 1 Then

                                Totalcxc = Totalcxc + ((pagos.Coll_Cxc_Seleccionados.Item(i).montopagar * pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)) / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM

                            Else
                                Totalcxc = Totalcxc + (pagos.Coll_Cxc_Seleccionados.Item(i).montopagar * pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)

                            End If


                        Else

                            Totalcxc = Totalcxc + pagos.Coll_Cxc_Seleccionados.Item(i).montopagar
                        End If


                    Next

                    If Txt_Dsctos.Text <> "" Then
                        total = Txt_Dsctos.Text
                    End If
                    total = total + Totalcxc
                    Txt_Dsctos.Text = Format(total, fmto)
                    simulacion(3)

                End If

            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub limpia_simulacion()

        txt_montos_doctos.Text = ""
        Txt_Com_x_dcto.Text = ""
        Txt_Monto_anticipar.Text = ""
        Txt_Com_esp.Text = ""
        Txt_dif_precio.Text = ""
        Txt_Iva.Text = ""
        Txt_Precio_compra.Text = ""
        Txt_GastosAfectos.Text = ""
        Txt_Gastos.Text = ""
        Txt_Saldo_pendiente.Text = ""
        Txt_Impuestos.Text = ""
        Txt_Saldo_pagar.Text = ""
        Txt_DeScuentos.Text = ""
        Txt_saldo_total.Text = ""

        Txt_Tasa_Base.Text = ""
        Txt_Spread.Text = ""
        Txt_Puntos.Text = ""
        Txt_Tnego.Text = ""

        Dr_mone.SelectedValue = 0
        Txt_Porc_Antic.Text = ""
        Txt_Comi.Text = ""

        Dr_Moneda.SelectedValue = 0
        Txt_Porc_com.Text = ""
        Txt_Min.Text = ""
        Txt_Max.Text = ""

        Txt_Comflat.Text = ""
        Txt_Dsctos.Text = ""
        Txt_Negocio.Text = ""
        Txt_Dsctos.Text = ""
        Txt_Antic.Text = ""
        Txt_GastImp.Text = ""
        Txt_ComDocto.Text = ""
        HF_Imp.Value = ""
        HF_Mto.Value = ""
        HF_N_Ope.Value = ""
        HF_NroNeg.Value = ""
        HF_Pos.Value = ""
        Txt_ItemOPE.Text = ""

        Dr_For_Pgo.ClearSelection()
        Dr_Bco.ClearSelection()

        Txt_Cta_Cte.Text = ""
        Ch_Ats_14hrs.Checked = False
        Txt_Valor_GMF.Text = ""

    End Sub

    Public Sub marcagrilla()

        Try

            Dim i As Integer
            Dim i1 As Integer

            'Formateo segun moneda
            For i1 = 0 To Me.Gr_Operaciones.Rows.Count - 1
                If ses_ope.coll_ope.Item(i1 + 1).id_p_0023 = 1 Then
                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0")
                ElseIf ses_ope.coll_ope.Item(i1 + 1).id_p_0023 = 3 Or ses_ope.coll_ope.Item(i1 + 1).id_p_0023 = 4 Then
                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0.00")
                ElseIf ses_ope.coll_ope.Item(i1 + 1).id_p_0023 = 2 Then
                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0.0000")
                End If
            Next


            For i = 0 To Gr_Operaciones.Rows.Count - 1
                If (HF_N_Ope.Value = CType(Gr_Operaciones.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip) Then
                    If (i Mod 2) = 0 Then
                        Gr_Operaciones.Rows(i).CssClass = "selectable"
                    Else
                        Gr_Operaciones.Rows(i).CssClass = "selectableAlt"
                    End If
                Else
                    If ses_ope.coll_ope.Item(i + 1).ID_P_0012 = 1 Then
                        Gr_Operaciones.Rows(i).CssClass = "formatUltcellAlt"
                    ElseIf ses_ope.coll_ope.Item(i + 1).ID_P_0012 = 2 Then
                        'Me.Gr_Operaciones.Rows(i).BackColor = Drawing.Color.LightYellow
                    ElseIf ses_ope.coll_ope.Item(i + 1).ID_P_0012 = 3 Then
                        'Me.Gr_Operaciones.Rows(i).BackColor = Drawing.Color.LightSalmon
                    End If

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Excepcion)
        End Try

    End Sub

    Private Sub cargadocs()
        Try


            If ses_ope.coll_ope(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                If Val(uf.Value) = 0 Then

                    msj.Mensaje(Me, "Atención", "Debe Ingresar valor Uf para poder Simular", 2)
                    Exit Sub
                End If
            End If


            If ses_ope.coll_ope(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 3 Then
                If Val(dolar.Value) = 0 Then

                    msj.Mensaje(Me, "Atención", "Debe Ingresar valor Dolar para poder Simular", 2)
                    Exit Sub
                End If
            End If

            Dim VALIDACION_LDC As String
            Dim itemIdx As Int16
            Dim Idx As Int16
            Dim vSpreadColocacionCliente As Decimal
            Dim ope As Object

            itemIdx = Val(Me.Txt_ItemOPE.Text)
            ope = ses_ope.coll_ope.Item(itemIdx)

            'Asigna datos de Collection Operación a Campos 
            With ope

                '/################################ Retorna Colecciones según Cliente y Operación Seleccionada ################################/ 
                pagos.coll_egr = New Collection
                pagos.coll_egr_sec = New Collection
                ses_ope.Coll_DSI = New Collection
                sesion.coll_ldc = New Collection
                sesion.coll_apc = New Collection
                sesion.coll_pnu = New Collection


                'Retorna Collection Egresos (EGR) 
                pagos.coll_egr = cg.Egresos_Devuelve(.id_ope)
                HF_NroNeg.Value = .id_opn
                Coll_DSI = ses_ope.Coll_DSI 'OP.documentosIngresados_Retorna(.id_ope, .id_ope)

                'Retorna Collection Línea de Créditos/Financiamiento (LDC) 
                Dim ldc As New ldc_cls
                ldc = cg.LineaDeCreditoDevuelve(.CLI_IDC, 1)

                If Not IsNothing(ldc) Then
                    sesion.coll_ldc.Add(ldc)
                End If

                'Retorna Collection Parámetros Numericos (PNU) --> Atributos de Tipo de Documento 
                coll_pnu = cg.Parametros_Detalle_Devuelve(31, .id_p_0031)
                Dim sistema As sis_cls = cg.SistemaDevuelve()

                If sistema.sis_vld_lin = "S" Then

                    '/################################ Validación de Existencia de Línea de Crédito siempre que operación no sea Puntual ################################/ 

                    If CMC.RetornaResponsabilidad(.id_opn) = True Then
                        If IsNothing(ldc) Then
                            If .ope_ptl = "N" Then
                                msj.Mensaje(Me, "Atención", "Cliente No Presenta Línea de Crédito Vigente , Para Modalidad de Operación", 2)
                                Me.Ch_Op_Ptal.Checked = True
                                Me.Ch_Op_Ptal.Enabled = False
                                btn_guardar.Enabled = False
                                Exit Sub
                            End If
                        Else

                            vSpreadColocacionCliente = sesion.coll_ldc(1).ldc_spr_col

                            'Retorna Collection APC ANTICIPO POR PRODUCTO 
                            sesion.coll_apc = New Collection
                            sesion.coll_apc.Add(cg.AnticipoDevuelvePorLinea(False, Nothing, sesion.coll_ldc(1).id_ldc, sesion.coll_ldc(1).id_ldc, ses_ope.coll_ope(Val(Me.Txt_ItemOPE.Text)).id_p_0031, ses_ope.coll_ope(Val(Me.Txt_ItemOPE.Text)).id_p_0031))

                        End If
                    End If

                End If

                'TextBox 
                Txt_FecSimulacion.CssClass = "clsDisabled"
                Txt_FecSimulacion.ReadOnly = True

                'La fecha de simulacion no se puede cambiar, esta es igual a la fecha de negociacion
                Me.Txt_FecSimulacion.Text = Format(CDate(.ope_fec_sim), "dd/MM/yyyy")

                'Spread ************************************/ 
                If Not IsNothing(.opn_tas_moa) Then
                    Me.Rb_mora.SelectedValue = .opn_tas_moa
                Else
                    Me.Rb_mora.SelectedValue = "A"
                End If

                'Spread Solo Lectura Busca su valor en objeto LDC (línea de crédito) 
                If Val(.id_p_0012) = 1 Or Val(.id_p_0012) = 2 Or Val(.id_p_0012) = 4 Then 'Solo Habilita con tipo de Operación Normal Con Giro y Sin Giro:=: 1 y 2 
                    Me.Txt_Spread.CssClass = "clsDisabled"
                    Me.Txt_Spread.ReadOnly = True
                    If .opn_spr_ead = 0 Then
                        Me.Txt_Spread.Text = vSpreadColocacionCliente
                    Else
                        Me.Txt_Spread.Text = .opn_spr_ead
                    End If
                Else
                    If .opn_spr_ead = 0 Then
                        Me.Txt_Spread.Text = vSpreadColocacionCliente
                    Else
                        Me.Txt_Spread.Text = .opn_spr_ead
                    End If
                End If

                'Tasas*********************************/ 
                If Val(.id_p_0012) <> 3 Then
                    Me.Txt_Dsctos.Text = IIf(IsDBNull(.ope_mto_scb), 0, .ope_mto_scb)
                Else
                    Me.Txt_Dsctos.Text = IIf(IsDBNull(.ope_mto_scb), 0, .ope_mto_scb)
                End If

                If Me.Rb_dig.Checked = True Then
                    Me.Txt_Dsctos.Text = 0
                End If

                Txt_GastImp.Text = 0

                If HF_Mto.Value <> "" Or HF_Mto.Value = "0" Then
                    Txt_GastImp.Text = IIf(IsDBNull(HF_Mto.Value), 0, HF_Mto.Value) + IIf(IsDBNull(HF_Imp.Value), 0, HF_Imp.Value)
                    Me.Txt_GastImp.Text = Format(CDbl(Me.Txt_GastImp.Text), fg.DevuelveFormatoMoneda(.id_p_0023))
                Else
                    Me.Txt_GastImp.Text = OP.gastos_ope_calcula(.id_opn)
                    Me.Txt_GastImp.Text = Format(CDbl(Me.Txt_GastImp.Text), fg.DevuelveFormatoMoneda(.id_p_0023))
                End If

                If Txt_GastImp.Text = 0 Then
                    Me.Txt_GastImp.Text = OP.gastos_ope_calcula(.id_opn)
                    Me.Txt_GastImp.Text = Format(CDbl(Me.Txt_GastImp.Text), fg.DevuelveFormatoMoneda(.id_p_0023))
                End If


                '*****************************************************************************************************************************
                Me.Txt_Tasa_Base.Text = Format(CDbl(.opn_tas_bas), fmt.FSMCD4)
                Me.Txt_Spread.Text = Format(CDbl(.opn_spr_ead), fmt.FSMCD4)
                Me.Txt_Puntos.Text = Format(CDbl(.opn_pto_spr), fmt.FSMCD4)

                Me.Txt_Negocio.Text = Format(.opn_tas_neg, fmt.FCMCD4)
                Me.Txt_Tnego.Text = Format(.opn_tas_neg, fmt.FCMCD4)

                Me.Txt_Puntos.CssClass = "clsDisabled"
                Me.Txt_Puntos.ReadOnly = True


                'DropList 
                Me.Dr_Moneda.ClearSelection()
                Me.Dr_Moneda.Enabled = False
                Me.Dr_Moneda.CssClass = "clsDisabled"

                'TextBox 
                Me.Txt_Comi.CssClass = "clsDisabled"
                Me.Txt_Comi.ReadOnly = True
                Me.Txt_Max.CssClass = "clsDisabled"
                Me.Txt_Max.ReadOnly = True
                Me.Txt_Min.CssClass = "clsDisabled"
                Me.Txt_Min.ReadOnly = True

                'Si comision es 0 , simplemente la mantiene 
                Dim fmto_com As String

                If .ID_P_0023_COM = 0 Then
                    BuscaCombo(Me.Dr_Moneda, 1)
                Else
                    BuscaCombo(Me.Dr_Moneda, (.ID_P_0023_COM))
                End If

                If .opn_por_com = 0 Then
                    Me.Txt_Porc_com.Text = 0
                    Me.Txt_ComDocto.Text = 0
                Else
                    Txt_Porc_com.Text = .opn_por_com
                    Me.Txt_ComDocto.Text = .opn_por_com
                End If

                If .opn_com_max = 0 Then
                    Me.Txt_Max.Text = 0
                Else
                    Me.Txt_Max.Text = (.opn_com_max)
                End If

                If .opn_com_min = 0 Then
                    Me.Txt_Min.Text = 0
                Else
                    Me.Txt_Min.Text = (.opn_com_min)
                End If

                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_com = 1 Then
                    fmto_com = fmt.FCMSD
                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_com > 2 Then
                    fmto_com = fmt.FCMCD
                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_com = 2 Then
                    fmto_com = fmt.FCMCD4
                Else
                    fmto_com = fmt.FCMSD()
                End If

                Me.Txt_Porc_com.Text = Format(CDbl(Me.Txt_Porc_com.Text), fmt.FCMCD)
                Me.Txt_Max.Text = Format(CDbl(Me.Txt_Max.Text), fmto_com)
                Me.Txt_Min.Text = Format(CDbl(Me.Txt_Min.Text), fmto_com)

                Dim fmt_fla As String

                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla = 1 Then
                    fmt_fla = fmt.FCMSD
                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla > 2 Then
                    fmt_fla = fmt.FCMCD
                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla = 2 Then
                    fmt_fla = fmt.FCMCD4
                Else
                    fmt_fla = fmt.FCMSD()
                End If


                'Comisión FLAT **********************************/ 

                'TextBox 

                Me.Txt_Porc_com.CssClass = "clsDisabled"
                Me.Txt_Comi.CssClass = "clsDisabled"

                Me.Txt_Comi.Text = (.opn_com_fla)
                Me.Txt_Comi.Text = Format(CDbl(Me.Txt_Comi.Text), fmt_fla)
                If Me.Txt_Comi.Text = "" Then
                    Me.Txt_Comi.Text = "0"
                End If

                Me.Txt_Comi.ReadOnly = True
                Me.Txt_Comflat.Text = Format(.opn_com_fla, fmt_fla)

                Select Case ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Text)).id_p_0023_fla
                    Case 1
                        Txt_Comflat.ToolTip = "MONEDA COMISION PESO"
                    Case 2
                        Txt_Comflat.ToolTip = "MONEDA COMISION UF - UF"
                    Case 3
                        Txt_Comflat.ToolTip = "MONEDA COMISION US$ - DOLAR"
                    Case 4
                        Txt_Comflat.ToolTip = "MONEDA COMISION EURO"

                End Select

                Me.Txt_Comflat.Text = Format(CDbl(Me.Txt_Comflat.Text), fmt_fla)

                'DropList 
                Me.Dr_mone.ClearSelection()
                'BuscaCombo(Me.Dr_mone, CStr(.id_p_0023_fla))

                If Not IsNothing(.id_p_0023_fla) Then
                    Me.Dr_mone.SelectedValue = .id_p_0023_fla
                End If

                Me.Dr_mone.Enabled = False
                Me.Dr_mone.CssClass = "clsDisabled"

                '% Anticipo y Observación ***************************/ 
                'TextBox 
                If .id_p_0012 = 1 Or .id_p_0012 = 2 Or Val(.id_p_0012) = 4 Then 'Solo Habilita con tipo de Operación Normal Con Giro y Sin Giro:=: 1 y 2 

                    Me.Txt_Porc_Antic.CssClass = "clsDisabled"
                    Me.Txt_Porc_Antic.ReadOnly = True
                    Txt_Antic.Text = 0

                    Select Case .id_p_0012
                        Case 1
                            If .ope_por_ant = 0 And coll_apc.Count > 0 Then : Me.Txt_Porc_Antic.Text = Format(CDec(coll_apc(1).apc_pct), "00.00") : Me.Txt_Antic.Text = Format(CDec(coll_apc(1).apc_pct), "00.00") : Else : Me.Txt_Porc_Antic.Text = Format(CDec(.ope_por_ant), "00.00") : Me.Txt_Antic.Text = Format(CDec(.ope_por_ant), "00.00") : End If
                        Case 2, 4
                            Me.Txt_Porc_Antic.Text = .ope_por_ant
                            Me.Txt_Antic.Text = .ope_por_ant
                    End Select

                End If

                'Forma de Pago ************************************/ 
                'Esta consultando por el estado de la operacion y no por forma de pago

                Txt_Cta_Cte.Text = ""
                Dr_For_Pgo.ClearSelection()
                Dr_Bco.ClearSelection()
                Ch_Ats_14hrs.Checked = False

                If .id_p_0012 = 1 Then
                    'DropList 
                    NroPaginacion = 0

                    Me.Dr_Bco.ClearSelection()
                    Me.Dr_Bco.Enabled = False
                    Me.Dr_Bco.CssClass = "clsDisabled"
                    Me.Dr_For_Pgo.ClearSelection()
                    Me.Dr_For_Pgo.Enabled = False
                    Me.Dr_For_Pgo.CssClass = "clsDisabled"

                    'TextBox 
                    Me.Txt_Cta_Cte.CssClass = "clsDisabled"
                    Me.Txt_Cta_Cte.ReadOnly = True

                    Me.Txt_Cta_Cte.Text = ""
                    If Me.Txt_Porc_com.Text = "" Then
                        Txt_Porc_com.Text = "0"
                        Me.Txt_ComDocto.Text = "0"
                    End If

                    Me.Dr_For_Pgo.SelectedValue = ses_ope.coll_ope.Item(CInt(Me.Txt_ItemOPE.Text)).id_p_0056
                    Me.Txt_Cta_Cte.Text = ses_ope.coll_ope.Item(CInt(Me.Txt_ItemOPE.Text)).opn_cta_cte

                    Me.Dr_Bco.SelectedValue = ses_ope.coll_ope.Item(CInt(Me.Txt_ItemOPE.Text)).id_bco

                    'RadioButton 
                    '******************************************************************************************
                    'Antes de las 14 hrs.
                    '******************************************************************************************
                    If ses_ope.coll_ope.Item(CInt(Txt_ItemOPE.Text)).opn_ant_014 = "S" Then
                        Ch_Ats_14hrs.Checked = True
                    Else
                        Ch_Ats_14hrs.Checked = False
                    End If
                    '******************************************************************************************

                    Me.Ch_Ats_14hrs.Enabled = False

                    If IsNothing(pagos.coll_egr) = False Then
                        For Idx = 1 To pagos.coll_egr.Count

                            If coll_egr(Idx).id_p_0056 <> 5 Then

                                'DropList 
                                BuscaCombo(Me.Dr_Bco, CStr(coll_egr(Idx).egr_bco))
                                BuscaCombo(Me.Dr_For_Pgo, CStr(coll_egr(Idx).pnu_egr_tip))

                                'TextBox 
                                Me.Txt_Cta_Cte.Text = coll_egr(Idx).egr_cta_cte

                                'RadioButton 
                                If coll_egr(Idx).egr_dep_ant = "S" Then
                                    Me.Ch_Ats_14hrs.Checked = True
                                Else
                                    Me.Ch_Ats_14hrs.Checked = False
                                End If

                                Exit For

                            End If

                        Next

                    End If
                    Dim VAL As Boolean

                    VAL = OP.VALIDA_DATOS_LINEA_Y_PUNTUAL(.CLI_IDC, .id_ope)

                    If VAL = False Then
                        msj.Mensaje(Me, "Atencion", "Operación NO Puntual, se requiere Ingresar Línea", 2)
                        Exit Sub

                    End If

                End If

                Idx = 1
                If Me.Txt_Min.Text = "" Then
                    Me.Txt_Min.Text = Format(CDbl(0), fmt.FSMCD)
                End If
                If Me.Txt_Max.Text = "" Then
                    Me.Txt_Max.Text = Format(CDbl(0), fmt.FSMCD)
                End If

            End With

            HF_NroNeg.Value = coll_ope.Item(CInt(Me.Txt_ItemOPE.Text)).id_ope

            Me.Btn_negoc.Enabled = True

            marcagrilla()

            calcula_gtos() ' Cargamos los gastos asociados a la operacion

            If Me.Rb_dig.Checked Then
                simulacion(3)
                Me.btn_descto.Enabled = True
                btn_gast_imp.Enabled = True
            End If

            If Me.Rb_Sim.Checked Then
                simulacion(3)
                InhabilitaCamposopesim()
                Me.btn_descto.Enabled = False
                btn_gast_imp.Enabled = False
            End If

            If Txt_Comflat.Text <> "" Then
                Select Case ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Text)).id_p_0023_fla
                    Case 1 'PESO
                        Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMSD)
                    Case 2 'UF
                        Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMCD4)
                    Case 3, 4 'DOLLAR Y EURO
                        Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMCD)
                    Case Else
                        Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMSD)
                End Select

            End If

            If Rb_Sim.Checked = True Then
                'Txt_Dsctos.Text = 0 > '?????????????
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Excepcion)
        End Try

    End Sub

    Public Sub alinea_textos()

        Me.Txt_Negocio.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Comflat.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_ComDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Dsctos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GastImp.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Antic.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_montos_doctos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Monto_anticipar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_dif_precio.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Saldo_pendiente.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Saldo_pagar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Com_x_dcto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Com_esp.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Iva.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Gastos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GastosAfectos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Impuestos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DeScuentos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Saldo_pagar.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Tasas
        Me.Txt_Tasa_Base.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Spread.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Puntos.Attributes.Add("Style", "TEXT-ALIGN: right")
        'vuf.Attributes.Add("Style", "TEXT-ALIGN: right")
        vdolar.Attributes.Add("Style", "TEXT-ALIGN: right")
        vtmc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Tnego.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Comisiones
        Me.Txt_Comi.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Max.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Min.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Anticipo
        Me.Txt_Porc_Antic.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Pago a Cliente
        Me.Txt_Cta_Cte.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Porc_com.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.Txt_saldo_total.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.Txt_Valor_GMF.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.Txt_Precio_compra.Attributes.Add("Style", "TEXT-ALIGN: right")


        'Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Public Sub VALIDAOPERACION()

        Try


            caso = 0
            Dim cantdotdia As Boolean
            Dim contfecdoc As Boolean

            Dim e As New System.EventArgs

            Dim COMISION_APLICADA As Double
            Dim MONTO_MIN As Double
            Dim MONTO_MAX As Double
            Dim SUMA_COMISION_APLICADA As Double

            Dim fun As New FuncionesGenerales.FComunes
            Dim i As Integer = 0

            'Valida q la tasa no sea mayor a 100
            If Val(Me.Txt_Tasa_Base.Text) > 100 Then

                msj.Mensaje(Me, "Atención", "Tasa no puede ser mayor que 100", 5)
                Me.Txt_Tasa_Base.Focus()
                validacion = 1
                Exit Sub
            End If
            'valida q los puntos no sean mayores a 100
            If Val(Me.Txt_Puntos.Text) > 100 Then

                msj.Mensaje(Me, "Atención", "Los puntos no pueden ser mayores que 100", 5)
                Me.Txt_Puntos.Focus()
                validacion = 1
                Exit Sub
            End If

            If Me.Txt_Porc_Antic.Text <> "" Then
                If Val(Me.Txt_Porc_Antic.Text) <> 0 Then
                    If vez <> 2 Then
                        If Txt_Porc_Antic.Text <> Me.Txt_Antic.Text Then
                            caso = 1

                            msj.Mensaje(Me, "Atención", "El Porcentaje de anticipo ha cambiado", 5)
                            Me.Txt_Porc_Antic.Focus()
                            validacion = 1
                            Exit Sub
                        End If
                    End If

                End If
            End If

            Me.Txt_Negocio.Text = (Val(Me.Txt_Tasa_Base.Text) + Val(Me.Txt_Spread.Text) + Val(Me.Txt_Puntos.Text))

            If Val(Me.Txt_Tnego.Text) > 100 Then

                msj.Mensaje(Me, "Atención", "Tasa de negocio no puede ser mayor que 100", 5)
                Me.Txt_Tnego.Focus()
                validacion = 1
                Exit Sub
            End If

            If Me.Txt_Porc_com.Text = "" Then
                Txt_Porc_com.Text = 0
                Me.Txt_ComDocto.Text = 0
            End If

            If Format(CDec(Val(Me.Txt_Porc_com.Text))) > 100 Then

                msj.Mensaje(Me, "Atención", "Comisión no puede ser mayor a 100", 5)
                Me.Txt_Porc_com.Focus()
                validacion = 1
                Exit Sub
            End If

            If Txt_Porc_Antic.Text = "" Then
                Txt_Porc_Antic.Text = 0
            End If

            If Format(CDec(Val(Me.Txt_Porc_Antic.Text))) > 100 Then
                msj.Mensaje(Me, "Atención", "Porcentaje a anticipar no puede ser mayor a 100", 5)
                Me.Txt_Porc_Antic.Focus()
                validacion = 1
                Exit Sub
            End If

            privez.Value = 1

            If Rb_dig.Checked = True Then
                If Me.Txt_FecSimulacion.Text = "" Then

                    msj.Mensaje(Me, "Atención", "Ingrese Fecha de Simulación", 5)
                    cantdotdia = False
                    Me.Txt_FecSimulacion.Focus()
                    validacion = 1
                    Exit Sub
                End If
            End If

            If Me.Txt_Tasa_Base.Text = "" Then
                Txt_Negocio.Text = Format(CDbl(0), fmt.FSMCD)
                msj.Mensaje(Me, "Atención", "Tasa base no debe ser menor a cero", ClsMensaje.TipoDeMensaje._Excepcion)
                validacion = 1
                Exit Sub
            End If

            '----------------------------------FIN VALIDACIONES-----------------------------------------------------------------------------------------------------------

            Me.Txt_Negocio.Text = Me.Txt_Tnego.Text

            If Trim(Me.Txt_Comi.Text) = "" Or Trim(Me.Txt_Comi.Text) = fmt.FCMCD Then
                Me.Txt_Comflat.Text = Format(CDbl(0), fmt.FSMSD)
            Else
                Me.Txt_Comflat.Text = cg.RETORNA_VALOR_MONEDA(Me.Txt_Comi.Text, Dr_mone.SelectedValue, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla, FUNFechaJul(Txt_FecSimulacion.Text))
            End If

            If Val(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla) <> 1 Then
                Me.Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FSMCD)
            Else
                Txt_Comflat.Text = Format(CDbl(Txt_Comflat.Text), fmt.FSMSD)
            End If


            'X DOCTO.
            ses_ope.Coll_DSI = New Collection

            ses_ope.Coll_DSI = OP.documentosIngresados_Retorna(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope)

            Me.Gr_Documentos.DataSource = ses_ope.Coll_DSI
            Me.Gr_Documentos.DataBind()

            For i = 1 To ses_ope.Coll_DSI.Count

                Dim DSI = New Object

                DSI = ses_ope.Coll_DSI.Item(i)

                With DSI

                    contfecdoc = True

                    If DSI.dsi_fev_rea <= CDate(Me.Txt_FecSimulacion.Text) Then
                        contfecdoc = False
                        validacion = 1
                        msj.Mensaje(Me, "Atención", "El Documento N° " & Format(DSI.dsi_num, fmt.FSMSD) & " se encuentra vencido a la fecha de Simulación", 5)
                        SW = True

                        Exit Sub
                    End If

                    '****** % ANTICIPAR
                    Me.Txt_Antic.Text = Me.Txt_Porc_Antic.Text

                    If Me.Rb_Sim.Checked = True Then
                        Me.Btn_Anu_Sim.Enabled = True
                    Else
                        Btn_Anu_Sim.Enabled = False
                    End If

                    Me.btn_calc.Enabled = True

                End With

            Next

            marcagrilla()

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub simulacion(ByVal index As Integer)

        Try

            Dim fun As New FuncionesGenerales.FComunes
            Dim SUMA_MONTO_OPERACION As Double
            Dim SUMA_MONTO_ANTICIPO As Double
            Dim SUMA_DIF_PRECIO As Double
            Dim SUMA_PRECIO_COMPRA As Double
            Dim SUMA_COMISION_APLICADA As Double
            Dim SUMA_SALDO_PENDIENTE As Double
            Dim SUMA_SALDO_A_PAGAR As Double
            Dim COMISION_APLICADA As Double
            Dim DIF_PRECIO As Double
            Dim MONTO_MIN, MONTO_MAX As Double
            Dim CONT_ANT_MENOR_CERO As Boolean
            Dim MONTO_DOCUMENTO As Double
            Dim MONTO_ANTICIPO As Double
            Dim FECHA_VCTO_DOCTO As String
            Dim FECHA_VCTO_AUX As String
            Dim NRO_DIAS_VCTO As Integer
            Dim JDX As Integer
            Dim i As Integer
            Dim X As Integer
            Dim SQL As String
            Dim SALDO_ANTICIPO As Double
            Dim MONTO_ABONADO As Double
            Dim MONTO_INTERES As Double
            Dim TOTAL_ABONADO As Double
            Dim SUMA_COMISIONES As Double
            Dim NUM_ROW As Long
            Dim TASA_NEG As Double
            Dim IMPUESTO_PAGARE_LETRA As Double
            Dim ddrMargen_IDX As Long '******************** Margenes
            Dim SUM_IVA As Double
            Dim FormatoConMiles As String
            Dim FormatoSinMiles As String
            Dim EST_DEU_COD As Integer
            Dim EST_DEU_DES As String
            Dim NUM_OPE As Long
            Dim IDX As Long
            Dim RutDeudorAux As String
            Dim PORCENTAJE_IVA As Object
            Dim FECHA_SIMULACION As String
            FECHA_SIMULACION = Me.Txt_FecSimulacion.Text
            Dim r As New FuncionesGenerales.FComunes
            Dim e As New System.EventArgs
            Dim itemIdx As Int16
            Dim valor_gmf As Integer = cg.SistemaDevuelveDatos().sis_can_gmf
            Dim ope As New Object

            itemIdx = Val(Me.Txt_ItemOPE.Text)
            ope = coll_ope.Item(itemIdx)

            'Rescata Tipo de Operacion y moneda de opn para despues consultar en gastos
            HF_TOp.Value = ope.id_p_0012
            HF_Moneda.Value = ope.id_p_0023

            Select Case index

                Case 3 'Ejecutar Simulación
                    If Me.Rb_Sim.Checked Then
                        Me.Txt_FecSimulacion.Text = Format(CDate(ope.ope_fec_sim), "dd/MM/yyyy")
                        FECHA_SIMULACION = Me.Txt_FecSimulacion.Text
                        'Me.Txt_FecSimulacion.ReadOnly = True
                        'Me.Txt_FecSimulacion_CalendarExtender.Enabled = False
                        'Me.Txt_FecSimulacion.CssClass = "clsDisabled"
                    Else
                        'Me.Txt_FecSimulacion.ReadOnly = False
                        'Me.Txt_FecSimulacion_CalendarExtender.Enabled = True
                        'Me.Txt_FecSimulacion.CssClass = "clsMandatorio"
                    End If

                    Dim fmto As String

                    If ope.id_p_0023 = 1 Then
                        fmto = fmt.FCMSD
                    ElseIf ope.id_p_0023 = 2 Then
                        fmto = fmt.FCMCD4
                    ElseIf ope.id_p_0023 > 2 Then
                        fmto = fmt.FCMCD
                    End If

                    If Not Me.Txt_ItemOPE.Text <> "" Then
                        vez = 2
                        msj.Mensaje(Me, "Atención", "Seleccione una operación", 5)
                        Exit Sub
                    End If

                    'Ejecuta la calculadora antes de procesar lo parámetros de operación

                    VALIDAOPERACION()

                    If validacion <> 0 Then
                        Exit Sub
                    End If

                    ' **** Inicio Valida Estados de Deudores
                    NUM_OPE = ses_ope.coll_ope.Item(itemIdx).id_ope

                    Me.Btn_Anu_Sim.Enabled = False
                    Me.Btn_Imp_Sim.Enabled = False
                    Btn_Asoc.Enabled = False
                    Btn_Buscar.Enabled = True
                    Btn_negoc.Enabled = False 'Negociación
                    Btn_Ing_pag.Enabled = False 'Impuesto

                    Deshab_x_Cambio_Fec_Simul()

                    TASA_NEG = (CDbl(Txt_Tasa_Base.Text) + CDbl(Txt_Spread.Text) + CDbl(Txt_Puntos.Text))

                    If ope.id_p_0012 = 3 Then
                        CALCULAR_PORCENTAJE_ANTICIPAR()
                    End If

                    If ope.id_p_0012 = 1 Or ope.id_p_0012 = 4 Then
                        If Me.Dr_For_Pgo.Text = "" Then '/*Forma de Pago*/
                            validacion = 1
                            msj.Mensaje(Me, "Atención", "Seleccione una Forma de Pago", 5)
                            If Dr_For_Pgo.Enabled Then Dr_For_Pgo.Focus()
                            Exit Sub
                        End If
                    End If

                    'Proceso de Simulacion X docto.

                    ses_ope.Coll_DSI = OP.documentosIngresados_Retorna(ope.id_ope, ope.id_ope, True)

                    SUMA_MONTO_OPERACION = 0
                    SUMA_MONTO_ANTICIPO = 0
                    SUMA_DIF_PRECIO = 0
                    SUMA_COMISION_APLICADA = 0
                    SUM_IVA = 0

                    'Asigna Formato para tipo de moneda según corresponda
                    If ope.id_p_0023 = 1 Then
                        FormatoSinMiles = fmt.FSMSD
                        FormatoConMiles = fmt.FCMSD
                    Else
                        FormatoSinMiles = fmt.FSMCD
                        FormatoConMiles = fmt.FCMCD
                    End If

                    'Retorna Monto en Moneda Requerida (monto,moneda origen,moneda destino,fecha paridad)
                    If ope.id_p_0023 = 2 Then
                        MONTO_MIN = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Min.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), fmt.FCMCD4)
                        MONTO_MAX = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Max.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), fmt.FCMCD4)
                    ElseIf ope.id_p_0023 = 3 Or ope.id_p_0023 = 4 Then
                        MONTO_MIN = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Min.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), FormatoSinMiles)
                        MONTO_MAX = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Max.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), FormatoSinMiles)
                    Else
                        MONTO_MIN = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Min.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), fmt.FCMSD)
                        MONTO_MAX = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Max.Text, Me.Dr_Moneda.SelectedValue, ope.id_p_0023, CDate(FECHA_SIMULACION))), fmt.FCMSD)

                    End If

                    CONT_ANT_MENOR_CERO = False
                    PORCENTAJE_IVA = Format((CMC.DatosDeSistemaDevuelve.sis_iva / 100), fmt.FCMCD)
                    Dim diasbase As Char = clasecli.ClienteDevuelvePorRut(Txt_Rut_Cli.Text).cli_dia_bas

                    For i = 1 To ses_ope.Coll_DSI.Count
                        Dim dsi As New Object
                        dsi = Coll_DSI.Item(i)
                        With dsi


                            If .dsi_flj <> "S" Then
                                '******************************  MARGENES  *****************************************
                                ' Si operacion es Sin Responsabilidad Llena Arreglo con Deudores que participan
                                ' en operacion
                                '***********************************************************************************

                                'Monto de Operacion
                                MONTO_DOCUMENTO = .dsi_mto_fin
                                SUMA_MONTO_OPERACION = SUMA_MONTO_OPERACION + MONTO_DOCUMENTO

                                'Monto Antic. Docto
                                MONTO_ANTICIPO = Format(CDbl(.dsi_mto_fin) * (Txt_Antic.Text / 100), fg.DevuelveFormatoMoneda(Me.Dr_Moneda.SelectedValue))

                                SUMA_MONTO_ANTICIPO = SUMA_MONTO_ANTICIPO + MONTO_ANTICIPO

                                'Dif. de precio
                                FECHA_VCTO_DOCTO = Format(.dsi_fev_cal, "dd/MM/yyyy")
                                NRO_DIAS_VCTO = DateDiff("d", CDate(Txt_FecSimulacion.Text).ToShortDateString, FECHA_VCTO_DOCTO)

                                DIF_PRECIO = fr.DiferenciaDePrecio(FECHA_VCTO_DOCTO, _
                                                                   CDate(Txt_FecSimulacion.Text).ToShortDateString, _
                                                                   MONTO_ANTICIPO, _
                                                                   Txt_Negocio.Text, _
                                                                   Rb_mora.SelectedValue, _
                                                                   ope.ope_lnl, _
                                                                   diasbase)


                                Dim formato As String

                                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                                    formato = fmt.FCMCD4
                                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 3 Or ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 4 Then
                                    formato = fmt.FCMCD
                                Else
                                    formato = fmt.FCMSD
                                End If

                                'Solo redondea cuando moneda es Peso
                                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                    SUMA_DIF_PRECIO = SUMA_DIF_PRECIO + Format(Math.Round(DIF_PRECIO, MidpointRounding.ToEven), formato)
                                Else
                                    SUMA_DIF_PRECIO = SUMA_DIF_PRECIO + Format(DIF_PRECIO, formato)
                                End If


                                'Precio Compra
                                SUMA_PRECIO_COMPRA = SUMA_PRECIO_COMPRA + (Format(CDbl(MONTO_DOCUMENTO), formato) - Format(CDbl(DIF_PRECIO), formato))

                                'Saldo Pendiente
                                SUMA_SALDO_PENDIENTE = SUMA_SALDO_PENDIENTE + (MONTO_DOCUMENTO - MONTO_ANTICIPO)

                                'Saldo a Pagar
                                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                    SUMA_SALDO_A_PAGAR = SUMA_SALDO_A_PAGAR + Format(CDbl(MONTO_ANTICIPO - Math.Round(DIF_PRECIO, MidpointRounding.ToEven)), formato)
                                Else
                                    SUMA_SALDO_A_PAGAR = SUMA_SALDO_A_PAGAR + Format(CDbl(MONTO_ANTICIPO - DIF_PRECIO), formato)
                                End If

                                'Comisión X Docto.
                                COMISION_APLICADA = (CSng(IIf(Trim(Txt_Porc_com.Text) = "", 0, Txt_Porc_com.Text)) / 100) * .dsi_mto

                                Select Case COMISION_APLICADA
                                    Case Is < MONTO_MIN
                                        COMISION_APLICADA = MONTO_MIN
                                    Case Is > MONTO_MAX
                                        COMISION_APLICADA = MONTO_MAX
                                End Select

                                If ope.id_p_0023 = 1 Then
                                    SUMA_COMISION_APLICADA = SUMA_COMISION_APLICADA + Format(Math.Round(COMISION_APLICADA), fmt.FSMCD)
                                ElseIf ope.id_P_0023 = 2 Then
                                    SUMA_COMISION_APLICADA = SUMA_COMISION_APLICADA + Format(COMISION_APLICADA, fmt.FCMCD4)
                                Else
                                    SUMA_COMISION_APLICADA = SUMA_COMISION_APLICADA + Format(COMISION_APLICADA, fmt.FSMCD)
                                End If

                                SUM_IVA = SUM_IVA + Format(CDbl(COMISION_APLICADA * PORCENTAJE_IVA), fmt.FSMCD)

                            End If

                        End With

                    Next

                    ses_ope.RUT_CLI_RPT = Format(CLng(Txt_Rut_Cli.Text), "000000000000")
                    ses_ope.IDX = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                    ses_ope.GMF = ope.opn_gen_gmf

                    Me.btn_descto.Enabled = True

                    If CONT_ANT_MENOR_CERO = False Then
                        'Valida que Moneda de Operacion sea Distinto de Pesos
                        If Val(ope.id_p_0023) = 2 Then
                            '*****  Monto Doctos.
                            txt_montos_doctos.Text = Format(CDbl(SUMA_MONTO_OPERACION), fmt.FCMCD4)

                            '*****  Monto Anticipo
                            Txt_Monto_anticipar.Text = Format(CDbl(SUMA_MONTO_ANTICIPO), fmt.FCMCD4)

                            '*****  Diferencia de Precio
                            Txt_dif_precio.Text = Format(CDbl(SUMA_DIF_PRECIO), fmt.FCMCD4)

                            '***** Precio de Compra
                            Txt_Precio_compra.Text = Format(CDbl(SUMA_PRECIO_COMPRA), fmt.FCMCD4)

                            '***** Saldo Pendiente
                            Txt_Saldo_pendiente.Text = Format(CDbl(SUMA_SALDO_PENDIENTE), fmt.FCMCD4)

                            '***** Saldo a Pagar
                            Txt_Saldo_pagar.Text = Format(CDbl(SUMA_SALDO_A_PAGAR), fmt.FCMCD4)

                            '***** Comisión X Docto.
                            Txt_Com_x_dcto.Text = Format(CDbl(SUMA_COMISION_APLICADA), fmt.FCMCD4)

                            '***** Comisión Especial(Flat)
                            Dim frm_com As String
                            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then

                                frm_com = fmt.FCMSD
                            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                                frm_com = fmt.FCMCD4
                            Else
                                frm_com = fmt.FCMCD
                            End If

                            If Txt_Comflat.Text = "" Then
                                Txt_Comflat.Text = 0
                            End If

                            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 Then
                                Me.Txt_Com_esp.Text = Txt_Comflat.Text
                            Else

                                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                    Me.Txt_Com_esp.Text = Format(Math.Round(CDbl(Txt_Comflat.Text * cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla, CDate(Me.Txt_FecSimulacion.Text)).par_val), MidpointRounding.ToEven), frm_com)
                                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla = 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 1 Then
                                    Me.Txt_Com_esp.Text = Format(Math.Round(Txt_Comflat.Text / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ope_fac_cam, MidpointRounding.ToEven), frm_com)
                                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 1 Then
                                    Me.Txt_Com_esp.Text = Format(Math.Round(CDbl(Txt_Comflat.Text) * cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla, CDate(Me.Txt_FecSimulacion.Text)).par_val / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM, MidpointRounding.ToEven), frm_com)
                                Else
                                    Me.Txt_Com_esp.Text = Format(CDbl(Txt_Comflat.Text), frm_com)
                                End If
                            End If

                            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMSD)
                            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMCD4)
                            Else
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMCD)
                            End If

                            SUM_IVA = SUM_IVA + (SUMA_COMISIONES * (CMC.DatosDeSistemaDevuelve.sis_iva / 100))

                            SUMA_COMISIONES = SUMA_COMISIONES + SUMA_COMISION_APLICADA
                            '***** IVA
                            Txt_Iva.Text = Format((CDbl(Me.Txt_Com_x_dcto.Text) + CDbl(Me.Txt_Com_esp.Text) + CDbl(Me.Txt_GastosAfectos.Text)) * (CMC.DatosDeSistemaDevuelve.sis_iva / 100), fmt.FCMCD4)

                            '***** Gastos
                            IMPUESTO_PAGARE_LETRA = Format(CDbl(cg.RETORNA_VALOR_MONEDA(TOTAL_IMPUESTO + TOTAL_IMPUESTO_Pagare, 1, ope.id_p_0023, FUNFechaJul(FECHA_SIMULACION))), fmt.FCMCD4)

                            Txt_Gastos.Text = Format(CDbl(Me.Txt_Gastos.Text - IMPUESTO_PAGARE_LETRA), fmt.FCMCD4)
                            Txt_GastosAfectos.Text = Format(CDbl(Me.Txt_GastosAfectos.Text), fmt.FCMCD4)

                            If Txt_ItemOPE.Text <> "" Then
                                Dim mon_fac As Double
                                mon_fac = cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, Txt_FecSimulacion.Text).par_val
                                Txt_Gastos.Text = Format(CDbl(Txt_Gastos.Text / mon_fac), fg.DevuelveFormatoMoneda(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023))
                            End If

                            '***** Impuesto
                            Txt_Impuestos.Text = Format(CDbl(IMPUESTO_PAGARE_LETRA), fmt.FCMCD4)

                            '*****  Descuento
                            Txt_DeScuentos.Text = Format(CDbl(Txt_Dsctos.Text), fmt.FCMCD4)

                            '*****  Total a Girar
                            Txt_saldo_total.Text = CDbl(Txt_Saldo_pagar.Text _
                                                     - Txt_Com_x_dcto.Text _
                                                     - Txt_Com_esp.Text _
                                                     - Txt_Iva.Text _
                                                     - Txt_GastosAfectos.Text _
                                                     - Txt_Gastos.Text _
                                                     - Txt_Impuestos.Text _
                                                     - Txt_DeScuentos.Text)


                            '***** Le descuente le GMF
                            If Val(ope.id_p_0012) <> 3 Then

                                If ope.id_P_0030 = 1 Then
                                    If ope.opn_gen_gmf = "S" Then
                                        Txt_Valor_GMF.Text = (valor_gmf * (CDbl(Txt_saldo_total.Text) / 1000))
                                    Else
                                        Txt_Valor_GMF.Text = "0"
                                    End If
                                Else
                                    Txt_Valor_GMF.Text = CDbl(ope.ope_val_gmf)
                                    Txt_saldo_total.Text = CDbl(Txt_saldo_total.Text)
                                End If

                                Txt_saldo_total.Text = Format(CDbl(Txt_saldo_total.Text - CDbl(Txt_Valor_GMF.Text)), fmto)
                            Else
                                Me.Txt_saldo_total.Text = 0
                            End If

                            Txt_Valor_GMF.Text = Format(CDbl(Txt_Valor_GMF.Text), fmto)
                            Txt_saldo_total.Text = Format(CDbl(Txt_saldo_total.Text), fmto)

                        Else

                            '*****  Monto Doctos.
                            txt_montos_doctos.Text = Format(CDbl(SUMA_MONTO_OPERACION), FormatoConMiles)

                            '*****  Monto Anticipo
                            Txt_Monto_anticipar.Text = Format(CDbl(SUMA_MONTO_ANTICIPO), FormatoConMiles)

                            '*****  Diferencia de Precio
                            Txt_dif_precio.Text = Format(CDbl(SUMA_DIF_PRECIO), FormatoConMiles)

                            '***** Precio de Compra
                            Txt_Precio_compra.Text = Format(CDbl(SUMA_PRECIO_COMPRA), FormatoConMiles)

                            '***** Saldo Pendiente
                            Txt_Saldo_pendiente.Text = Format(CDbl(SUMA_SALDO_PENDIENTE), FormatoConMiles)

                            '***** Saldo a Pagar
                            Txt_Saldo_pagar.Text = Format(CDbl(SUMA_SALDO_A_PAGAR), FormatoConMiles)

                            '***** Comisión X Docto.
                            Txt_Com_x_dcto.Text = Format(CDbl(SUMA_COMISION_APLICADA), FormatoConMiles)

                            '***** Comisión Especial(Flat)
                            If Txt_Comflat.Text = "" Then
                                Txt_Comflat.Text = 0
                            End If

                            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                Me.Txt_Com_esp.Text = Format(Math.Round(CDbl(Txt_Comflat.Text * cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla, CDate(Me.Txt_FecSimulacion.Text)).par_val), MidpointRounding.ToEven), fmt.FCMSD)
                            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla = 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 1 Then
                                Me.Txt_Com_esp.Text = Format(Math.Round(CDbl(Txt_Comflat.Text / cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, CDate(Me.Txt_FecSimulacion.Text)).par_val), MidpointRounding.ToEven), fmt.FCMCD)
                            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla <> 1 And ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 1 Then
                                Me.Txt_Com_esp.Text = Format(Math.Round(CDbl(Txt_Comflat.Text) * cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023_fla, CDate(Me.Txt_FecSimulacion.Text)).par_val / ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).OPE_FAC_CAM, MidpointRounding.ToEven), fmt.FCMCD)
                            Else
                                Me.Txt_Com_esp.Text = Format(CDbl(Txt_Comflat.Text), fmt.FCMSD)
                            End If

                            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMSD)
                            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMCD4)
                            Else
                                Me.Txt_Com_esp.Text = Format(CDbl(Me.Txt_Com_esp.Text), fmt.FCMCD)
                            End If



                            SUMA_COMISIONES = Format(CDbl(Txt_Com_esp.Text), FormatoSinMiles)

                            SUM_IVA = SUM_IVA + Format(Format(CDbl(SUMA_COMISIONES), FormatoSinMiles) * (CMC.DatosDeSistemaDevuelve.sis_iva / 100), FormatoSinMiles)

                            SUMA_COMISIONES = SUMA_COMISIONES + SUMA_COMISION_APLICADA

                            '***** IVA
                            Txt_Iva.Text = Format(CDbl(CDbl(Me.Txt_Com_x_dcto.Text) + _
                                                  CDbl(Me.Txt_Com_esp.Text) + _
                                                  CDbl(Me.Txt_GastosAfectos.Text)) * _
                                                  (CMC.DatosDeSistemaDevuelve.sis_iva / 100), FormatoConMiles)

                            '***** Gastos
                            IMPUESTO_PAGARE_LETRA = cg.RETORNA_VALOR_MONEDA(TOTAL_IMPUESTO + TOTAL_IMPUESTO_Pagare, _
                                                                            TipoMoneda.PESOS, _
                                                                            TipoMoneda.PESOS, _
                                                                            FUNFechaJul(Format(CDate(FECHA_SIMULACION), "dd/MM/yyyy")))

                            If Txt_ItemOPE.Text <> "" Then

                                Dim mon_fac As Double
                                mon_fac = cg.ParidadDevuelve(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, Txt_FecSimulacion.Text).par_val

                                Txt_Gastos.Text = Format(CDbl(Txt_Gastos.Text / mon_fac), fg.DevuelveFormatoMoneda(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023))
                                Txt_GastosAfectos.Text = Format(CDbl(Txt_GastosAfectos.Text / mon_fac), fg.DevuelveFormatoMoneda(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023))

                            End If

                            '***** Impuesto
                            Txt_Impuestos.Text = Format(CDbl(IMPUESTO_PAGARE_LETRA), FormatoConMiles)

                            '***** Descuento
                            Txt_DeScuentos.Text = Format(CDbl(Txt_Dsctos.Text), FormatoConMiles)
                            Txt_Dsctos.Text = Format(CDbl(Txt_Dsctos.Text), FormatoConMiles)

                            '***** Monto a Girar

                            Txt_saldo_total.Text = CDbl(Txt_Saldo_pagar.Text _
                                                      - Txt_Com_x_dcto.Text _
                                                      - Txt_Com_esp.Text _
                                                      - Txt_Iva.Text _
                                                      - Txt_GastosAfectos.Text _
                                                      - Txt_Gastos.Text _
                                                      - Txt_Impuestos.Text _
                                                      - Txt_DeScuentos.Text)

                            '***** Monto a Girar
                            If Val(ope.id_p_0012) <> 3 Then
                                If ope.id_P_0030 = 1 Then
                                    If ope.opn_gen_gmf = "S" Then
                                        Txt_Valor_GMF.Text = (valor_gmf * (CDbl(Txt_saldo_total.Text) / 1000))
                                    Else
                                        Txt_Valor_GMF.Text = "0"
                                    End If
                                Else
                                    Txt_Valor_GMF.Text = CDbl(ope.ope_val_gmf)
                                End If

                                Txt_saldo_total.Text = Format(CDbl(Txt_saldo_total.Text - CDbl(Txt_Valor_GMF.Text)), fmto)

                            Else
                                Me.Txt_saldo_total.Text = 0
                            End If

                            Txt_Valor_GMF.Text = Format(CDbl(Txt_Valor_GMF.Text), fmto)
                            Txt_saldo_total.Text = Format(CDbl(Txt_saldo_total.Text), fmto)

                        End If


                        ' ******** VALIDA ANTICIPO > QUE DESCUENTOS
                        SALDO_ANTICIPO = Format(CDbl(Txt_saldo_total.Text), fmto)

                        Select Case ses_ope.coll_ope.Item(Val(Txt_ItemOPE.Text)).id_p_0023
                            Case 1
                                Txt_Com_esp.ToolTip = "MONEDA COMISION PESO"
                            Case 2
                                Txt_Com_esp.ToolTip = "MONEDA COMISION UF - UF"
                            Case 3
                                Txt_Com_esp.ToolTip = "MONEDA COMISION US$ - DOLAR"
                            Case 4
                                Txt_Com_esp.ToolTip = "MONEDA COMISION EURO"

                        End Select

                        If SALDO_ANTICIPO < 0 Then
                            validacion = 1
                            msj.Mensaje(Me, "Atención", "Monto a Girar No Supera Monto de Aplicacion de Descuentos y Gastos", 5)
                            '/*Valida si monto a girar es mayor o igual a cero para habilitar botor guardar */
                            Me.Btn_Anu_Sim.Enabled = False
                            SW = True
                            Btn_Anu_Sim.Enabled = False
                            Exit Sub
                        Else
                            Me.Btn_Anu_Sim.Enabled = True
                        End If

                    End If

            End Select

            'If Rb_Sim.Checked = True Then
            '    'Txt_DeScuentos.Text = 0 '?????????
            'End If

            'If ses_ope.GMF = "S" Then
            '    msj.Mensaje(Me, "Información", "Debe seleccionar el GMF asociado a la operacion", 2)
            'End If

        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message & "Simulacion", 5)
        End Try

    End Sub

    Public Sub Deshab_x_Cambio_Fec_Simul()
        Me.Btn_Anu_Sim.Enabled = False
        Me.Btn_Imp_Sim.Enabled = False
        If Rb_Sim.Checked = False Then

        Else
            Me.Btn_Anu_Sim.Enabled = True
            Me.Btn_Imp_Sim.Enabled = True
        End If
        Txt_Tasa_Base.CssClass = "clsDisabled"
        Txt_Puntos.CssClass = "clsDisabled"
        Txt_Comi.CssClass = "clsDisabled"
        Txt_Porc_com.CssClass = "clsDisabled"
        Txt_Min.CssClass = "clsDisabled"
        Txt_Max.CssClass = "clsDisabled"
        Txt_Porc_Antic.CssClass = "clsDisabled"
        Txt_Cta_Cte.CssClass = "clsDisabled"
        Dr_Moneda.CssClass = "clsDisabled"
        Dr_mone.CssClass = "clsDisabled"
        Me.Dr_For_Pgo.CssClass = "clsDisabled"
        Me.Dr_Bco.CssClass = "clsDisabled"

        Me.btn_calc.Enabled = True

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            sesion.Modulo = "Operacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CargaCliente()
    End Sub

    Private Sub CargaCliente()
        Try
            Dim EstadoOperacion As Integer
            Dim idx As Int16

            'Validaciones de RUT y Digito Verificaor

            'Carga Collection Cliente
            Dim Cli As cli_cls

            Cli = clasecli.ClientesDevuelve(Txt_Rut_Cli.Text.Trim, Me.Txt_Dig_Cli.Text)

            Session("Cliente") = Cli

            If valida_cliente <> "" Then

                msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If IsNothing(Cli) Then
                msj.Mensaje(Page, "Atención", "NIT no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If


            IB_AyudaCli.Enabled = False
            pagos.RutCliente = Me.Txt_Rut_Cli.Text
            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"


            'Asigna Razon Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)

            If Cli.id_P_008 = 5 Then
                msj.Mensaje(Me, " Atencion", "¡ Cliente Vetado !", 2)
            End If

            'Llena Objeto OPE (ingresadas)
            If Me.Rb_Sim.Checked = True Then
                EstadoOperacion = 2
            ElseIf Me.Rb_dig.Checked = True Then
                EstadoOperacion = 1
            End If
            idx = 0

            coll_ope = OP.OperacionesPorClienteDevuelve(Cli.cli_idc, (EstadoOperacion + 1), True, Me.Gr_Operaciones)

            Gr_Operaciones.DataSource = coll_ope
            Gr_Operaciones.DataBind()

            Dim i1, i2 As Integer

            For i1 = 0 To Me.Gr_Operaciones.Rows.Count - 1

                i2 = i1 + 1
                If ses_ope.coll_ope.Item(i2).id_p_0023 = 1 Then

                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0")

                ElseIf ses_ope.coll_ope.Item(i2).id_p_0023 = 3 Or ses_ope.coll_ope.Item(i2).id_p_0023 = 4 Then

                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0.00")

                ElseIf ses_ope.coll_ope.Item(i2).id_p_0023 = 2 Then

                    Gr_Operaciones.Rows(i1).Cells(4).Text = Format(CDbl(Gr_Operaciones.Rows(i1).Cells(4).Text), "###,###,##0.0000")
                End If

            Next

            'Recorre la grilla y marca segun tipo de operacion
            marcagrilla()


            'Button
            Me.Rb_dig.Enabled = True
            Me.Rb_Sim.Enabled = True

            Me.Btn_Buscar.Enabled = False
            Me.IB_Imp_Arc.Enabled = False
            Me.Btn_Limpiar.Enabled = True
            Me.Btn_Ope.Enabled = True
            Me.Btn_negoc.Enabled = False
            Me.Btn_Anu_Sim.Enabled = False

            Me.Btn_Ing_pag.Enabled = False
            Me.Btn_Imp_Sim.Enabled = False
            Me.Btn_Asoc.Enabled = False

            If Gr_Operaciones.Rows.Count > 0 Then
                clasecli.BancosDevuelvePorCliente(True, Me.Dr_Bco, Nothing, Cli.cli_idc)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message, 5)
        End Try


    End Sub

    Public Sub cargadetalle()
        Try

            VALIDAOPERACION()
            ses_ope.Coll_DSI = OP.documentosIngresados_Retorna(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope)
            Me.Gr_Documentos.DataSource = ses_ope.Coll_DSI
            Me.Gr_Documentos.DataBind()

            Dim formato As String

            Select Case coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023
                Case 1 : formato = fmt.FCMSD
                Case 2 : formato = fmt.FCMCD4
                Case 3, 4 : formato = fmt.FCMCD
            End Select

            For i = 0 To Me.Gr_Documentos.Rows.Count - 1

                If Coll_DSI.Item(i + 1).dsi_flj_num = 0 And Coll_DSI.Item(i + 1).dsi_flj = "S" Then
                    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                    Gr_Documentos.Rows(i).Enabled = False
                End If

                'Gr_Documentos.Rows(i).Cells(0).Text = CInt(Gr_Documentos.Rows(i).Cells(0).Text) & "-" & fg.Vrut(Gr_Documentos.Rows(i).Cells(0).Text)
                'Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(ses_ope.Coll_DSI.Item(i + 1).dsi_mto_fin), fmt.FCMSD)


                'Se da formato a rut 
                Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & fg.Vrut(Gr_Documentos.Rows(i).Cells(0).Text)

                'Se da formato a documento
                Gr_Documentos.Rows(i).Cells(4).Text = Format(CDbl(ses_ope.Coll_DSI.Item(i + 1).dsi_mto_fin), formato)

            Next

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CalculaPuntos()

        Try

            If Trim(Me.Txt_Tasa_Base.Text) = "" Then Exit Sub
            If Trim(Me.Txt_Spread.Text) = "" Then Exit Sub
            If Trim(Me.Txt_Tnego.Text) = "" Then Exit Sub
            If Trim(Txt_Tasa_Base.Text) = fmt.FCMCD Then Txt_Tasa_Base.Text = fmt.FSMSD
            If Trim(Txt_Spread.Text) = fmt.FCMCD Then Txt_Spread.Text = Format(CDec(Txt_Spread.Text), fmt.FSMSD)
            If Trim(Txt_Tnego.Text) = fmt.FCMCD Then Txt_Tnego.Text = Format(CDec(Txt_Tnego.Text), fmt.FSMSD)

            If Txt_Spread.Text > 0 Then
                Me.Txt_Puntos.Text = CDec(Txt_Tnego.Text) - CDec(Txt_Spread.Text) - CDec(Txt_Tasa_Base.Text)
            Else
                Txt_Spread.Text = Format(CDec(Txt_Tnego.Text) - CDec(Txt_Tasa_Base.Text), fmt.FSMCD)
                Txt_Puntos.Text = Format(0, fmt.FSMCD)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub CALCULAR_PORCENTAJE_ANTICIPAR()
        Try

            Dim MONTO_DESC_CAL, DESCUENTO_CAL, _
                COMISIONES_CAL, GASTOS_CAL, _
                IVA_CAL As Double
            Dim POR_ANTICIPAR As Double
            Dim MTO_OPE As Double

            If Txt_Antic.Text = "" Then
                Txt_Antic.Text = 0
            End If
            If Me.Dr_Moneda.SelectedValue = 1 Then
                DESCUENTO_CAL = Format(CDbl(Txt_Dsctos.Text), fmt.FCMSD)
                COMISIONES_CAL = CDbl(Format(CDbl(Txt_Negocio.Text), fmt.FCMSD)) + CDbl(Format(CDbl(Txt_Antic.Text), fmt.FCMSD))
                GASTOS_CAL = Format(CDbl(Txt_GastImp.Text), fmt.FCMSD)
                IVA_CAL = Format(CDbl((COMISIONES_CAL) * (cg.SistemaDevuelve.sis_iva / 100)), fmt.FCMSD)

                MONTO_DESC_CAL = Format(CDbl(DESCUENTO_CAL + COMISIONES_CAL + GASTOS_CAL + IVA_CAL), fmt.FCMSD)
                MTO_OPE = Format(CDbl(Me.Gr_Operaciones.Rows(Val(Me.Txt_ItemOPE.Text) - 1).Cells(2).Text), fmt.FCMSD)
            Else
                DESCUENTO_CAL = Format(CDbl(Txt_Dsctos.Text), fmt.FCMCD)
                COMISIONES_CAL = Format(CDbl(Txt_Negocio.Text) + CDbl(Txt_Antic.Text), fmt.FCMCD)
                GASTOS_CAL = Format(CDbl(Txt_GastImp.Text), fmt.FCMCD)
                IVA_CAL = Format((COMISIONES_CAL) * (cg.SistemaDevuelve.sis_iva / 100), fmt.FCMCD)

                MONTO_DESC_CAL = Format(DESCUENTO_CAL + COMISIONES_CAL + GASTOS_CAL + IVA_CAL, fmt.FCMCD)
                MTO_OPE = Format(CDbl(Me.Gr_Operaciones.Rows(Val(Me.Txt_ItemOPE.Text) - 1).Cells(2).Text) * cg.ParidadDevuelve(Dr_Moneda.SelectedValue, Me.Txt_FecSimulacion.Text).par_val, fmt.FCMSD)
            End If

            POR_ANTICIPAR = (MONTO_DESC_CAL * 100) / MTO_OPE

            If POR_ANTICIPAR > 100 Then POR_ANTICIPAR = 100

            If coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0012 <> 3 Then
                Txt_Antic.Text = Format(POR_ANTICIPAR, fmt.FCMCD)
                Me.Txt_Porc_Antic.Text = Txt_Antic.Text
            Else
                Txt_Antic.Text = "0.00"
                Me.Txt_Porc_Antic.Text = "0.00"
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message & ex.TargetSite.ToString, 5)
        End Try

    End Sub

    Public Function MIN(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        Try

            MIN = IIf(Par1 < Par2, Par1, Par2)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Function

    Public Function GUARDA_SIMULACION() As Boolean

        Try

            'GUARDA_SIMULACION = False
            PASO_DESCUENTOS = False

            'Valida si Tasa de Aplicacin de Intereses es = a ""
            Dim tasaapli = 0
            If Trim(tasaapli) = "" Then tasaapli = 0

            INTERES_DEVOLVER_OPERACION = 0
            IND_CXP = 1
            NUMERO_GASTO = 0

            FACTOR_CAMBIO_OPERACION = cg.RETORNA_VALOR_MONEDA(1, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, fg.FUNFechaJul(Me.Txt_FecSimulacion.Text))

            'Factor de Cambio Observado
            FACTOR_CAMBIO_OBS = FACTOR_CAMBIO_OPERACION

            Dim formato As String

            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                formato = fmt.FCMCD4
            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 3 Or ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 4 Then
                formato = fmt.FCMCD
            Else
                formato = fmt.FCMSD
            End If

            MONTO_MIN = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Min.Text, Dr_Moneda.SelectedValue, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, Me.Txt_FecSimulacion.Text)), formato)
            MONTO_MAX = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Max.Text, Dr_Moneda.SelectedValue, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, Me.Txt_FecSimulacion.Text)), formato)

            If Val(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0012) <> 3 Then
                TOTAL_ABONADO = Format(CDbl(Txt_DeScuentos.Text), fmt.FSMCD)
            Else
                TOTAL_ABONADO = 0
            End If

            'Retorna Monto en Moneda Requerida (monto,moneda origen,moneda destino,fecha paridad)
            TOTAL_ABONADO = cg.RETORNA_VALOR_MONEDA(CDbl(TOTAL_ABONADO), ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, fg.FUNFechaJul(Me.Txt_FecSimulacion.Text))
            TOTAL_ABONADO = Format(CDbl(TOTAL_ABONADO), fmt.FSMSD)

            QUE_SE_PAGA = 4
            TIPO_EGRESO = 5
            ANTES_DE_14_HRS = "N"
            OBSERVACION_EGR = ""
            BancoEgreso = Nothing
            CtaCteEgreso = ""

            proceso = True

            Try

                If Coll_Doctos_Seleccionados.Count > 0 Or Coll_Cxc_Seleccionados.Count > 0 Then
                    GRABA_EGRESO_SIN_GIRO()
                End If

            Catch ex As Exception
                msj.Mensaje(Page, "Atencion", "Graba Egreso sin giro: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
                Return False
            End Try

            ''***** FIN EGRESO SIN GIRO (APLICACION) ********************************************************************

            '*** Ciclo de IVA
            If Format(CDbl(Txt_Iva.Text), fmt.FSMCD) > 0 Then

                'Retorna Monto en Moneda Requerida (monto,moneda origen,moneda destino,fecha paridad)
                MONTO_ABONADO = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Me.Txt_Iva.Text, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, Me.Txt_FecSimulacion.Text)), fmt.FSMSD)
                TOTAL_ABONADO = Format(CDbl(cg.RETORNA_VALOR_MONEDA(Txt_Iva.Text, ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, Me.Txt_FecSimulacion.Text)), fmt.FSMSD)

                QUE_SE_PAGA = 5
                PARCIAL_TOTAL = "T"
                DOCTO_CEDIDO = " "

            End If

            Try
                '*** Ciclo de Ctas. X Cobrar
                sesion.coll_dsi_simu = New Collection
                If pagos.Coll_Cxc_Seleccionados.Count <> Nothing Then

                    Dim i As Integer

                    For i = 1 To pagos.Coll_Cxc_Seleccionados.Count


                        Dim SALDO_CLIENTE, SALDO_DEUDOR, MTO_ORIGINAL_CXC, FACTOR_CAMBIO_CXC_ANT As Double
                        Dim tipo_moneda_cxc_ant As Integer
                        SALDO_CLIENTE = 0
                        SALDO_DEUDOR = 0

                        'Extrae Valores
                        If pagos.Coll_Cxc_Seleccionados.Item(i).cxc_mto = 0 Then Exit For
                        PASO_DESCUENTOS = True
                        TIPO_CTA_X_COB = pagos.Coll_Cxc_Seleccionados.Item(i).TipoCta
                        NRO_CTA_X_COB = pagos.Coll_Cxc_Seleccionados.Item(i).id_cxc

                        tipo_moneda_cxc_ant = pagos.Coll_Cxc_Seleccionados.Item(i).id_p_0023
                        FACTOR_CAMBIO_CXC_ANT = pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam
                        FACTOR_CAMBIO_HOY = pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam
                        FACTOR_CAMBIO_OBS = pagos.Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam '6cxc_fac_cam_obs
                        MTO_ORIGINAL_CXC = Round(pagos.Coll_Cxc_Seleccionados.Item(i).cxc_mto * FACTOR_CAMBIO_HOY, 2)

                        MONTO_ABONADO = Round(pagos.Coll_Cxc_Seleccionados.Item(i).cxc_mto * CDbl(FACTOR_CAMBIO_HOY), 2)
                        MONTO_INTERES = Round(pagos.Coll_Cxc_Seleccionados.Item(i).cxc_int * CDbl(FACTOR_CAMBIO_HOY), 2)
                        TOTAL_ABONADO = Val(MONTO_ABONADO) + Val(MONTO_INTERES)
                        SALDO_CLIENTE = Round(pagos.Coll_Cxc_Seleccionados.Item(i).cxc_sal * CDbl(FACTOR_CAMBIO_HOY), 2)

                        REAJUSTE = MIN(CDbl(CDbl(MONTO_ABONADO / FACTOR_CAMBIO_HOY)), MIN(CDbl(SALDO_CLIENTE / FACTOR_CAMBIO_HOY), CDbl(MTO_ORIGINAL_CXC / FACTOR_CAMBIO_HOY))) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO_CXC_ANT)

                        QUE_SE_PAGA = 1
                        PARCIAL_TOTAL = "T"
                        DOCTO_CEDIDO = ""
                    Next
                End If

            Catch ex As Exception
                msj.Mensaje(Page, "Atencion", "Graba CxC: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
                Return False
            End Try

            If Not IsNothing(pagos.Coll_Doctos_Seleccionados) Then
                '*** Ciclo de Doctos.
                Dim SALDO_CLIENTE, SALDO_DEUDOR, MTO_ORIGINAL_CXC, FACTOR_CAMBIO_CXC_ANT As Double
                Dim tipo_moneda_cxc_ant As Integer
                Dim jdx As Integer
                For jdx = 1 To pagos.Coll_Doctos_Seleccionados.Count
                    'Extrae Valores
                    SALDO_CLIENTE = 0
                    SALDO_DEUDOR = 0

                    If pagos.Coll_Doctos_Seleccionados.Item(jdx).id_p_0031 = 0 Then Exit For
                    PASO_DESCUENTOS = True
                    ses_ope.ID_OPE_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ID_OPE
                    Pagador = "C"
                    PAGADO_POR_DEUDOR = pagos.Coll_Doctos_Seleccionados.Item(jdx).PAGADEUDOR

                    'Retorna Monto en Moneda Requerida (monto,moneda origen,moneda destino,fecha paridad)
                    FACTOR_CAMBIO_HOY = pagos.Coll_Doctos_Seleccionados.Item(jdx).OPE_FAC_CAM
                    FACTOR_CAMBIO_OBS = pagos.Coll_Doctos_Seleccionados.Item(jdx).OPE_FAC_CAM 'doc_fac_cam_obs

                    'FACTOR_CAMBIO_HOY = RUTINAS.RETORNA_VALOR_MONEDA(1, DOCTOSAPLICAR(JDX).doc_mon_eda, 1, Format(F_OP02_00_00.MED_DDIARIOS.Text, "dd/mm/yyyy"))
                    If pagos.Coll_Doctos_Seleccionados.Item(jdx).ID_P_0023 = 1 And FACTOR_CAMBIO_HOY = 0 Then FACTOR_CAMBIO_HOY = 1

                    'Asigna Valor a Reajustes
                    REAJUSTE = 0
                    If pagos.Coll_Doctos_Seleccionados.Item(jdx).id_p_0023 <> 1 Then

                        REAJUSTE = MIN(CDbl(pagos.Coll_Doctos_Seleccionados.Item(jdx).dsi_mto / FACTOR_CAMBIO_HOY), CDbl(pagos.Coll_Doctos_Seleccionados.Item(jdx).dsi_mto_ant / FACTOR_CAMBIO_HOY)) * (FACTOR_CAMBIO_HOY - pagos.Coll_Doctos_Seleccionados.Item(jdx).ope_fac_cam)
                    End If

                    FACTOR_CAMBIO = pagos.Coll_Doctos_Seleccionados.Item(jdx).ope_fac_cam

                    'Si Paga Dolar
                    'If DOCTOSAPLICAR(JDX).doc_mon_eda = 3 Then
                    TOTAL_ABONADO = pagos.Coll_Doctos_Seleccionados.Item(jdx).dsi_mto
                    MONTO_ABONADO = pagos.Coll_Doctos_Seleccionados.Item(jdx).dsi_mto
                    MONTO_INTERES = pagos.Coll_Doctos_Seleccionados.Item(jdx).interes
                    SALDO_CLIENTE = pagos.Coll_Doctos_Seleccionados.Item(jdx).doc_sdo_cli
                    SALDO_DEUDOR = pagos.Coll_Doctos_Seleccionados.Item(jdx).doc_sdo_ddr

                    QUE_SE_PAGA = 2
                    'PARCIAL_TOTAL = pagos.Coll_Doctos_Seleccionados.Item(jdx).total_parcial
                    DOCTO_CEDIDO = "N"
                    ses_ope.NUM_DOC = pagos.Coll_Doctos_Seleccionados.Item(jdx).id_doc

                Next
                '***** FIN INGRESO *********************************************************************************

            End If

            'Limpia Variables
            TIPO_CTA_X_COB = 0
            NRO_CTA_X_COB = 0
            MONTO_ABONADO = 0
            MONTO_INTERES = 0
            TOTAL_ABONADO = 0

            sesion.coll_dsi_simu = New Collection
            Dim idx As Integer

            Dim diasbase As Char = clasecli.ClienteDevuelvePorRut(Txt_Rut_Cli.Text).cli_dia_bas


            For idx = 1 To ses_ope.Coll_DSI.Count
                Dim doc As New dsi_cls

                'Monto de Operacion
                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then
                    formato = fmt.FCMCD4
                    MONTO_DOCUMENTO = Format(CDbl(ses_ope.Coll_DSI.Item(idx).dsi_mto), fmt.FCMCD4)
                ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 3 Or ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 4 Then
                    formato = fmt.FCMCD
                    MONTO_DOCUMENTO = Format(CDbl(ses_ope.Coll_DSI.Item(idx).dsi_mto), fmt.FCMCD)
                Else
                    formato = fmt.FCMSD
                    MONTO_DOCUMENTO = Format(CDbl(ses_ope.Coll_DSI.Item(idx).dsi_mto), fmt.FCMSD)
                End If


                'Monto Antic. Docto
                MONTO_ANTICIPO = Format(CDbl(ses_ope.Coll_DSI.Item(idx).dsi_mto * (Me.Txt_Antic.Text / 100)), formato)

                'Monto a Girar Por Documento
                Mto_a_Girar_docto = 0
                If MONTO_ANTICIPO <> 0 Then
                    Ponderacion_Anticipo = Format(CDbl((MONTO_ANTICIPO * 100) / Mto_ant_operacion), fmt.FSMSD)
                    If Mto_a_Girar <> 0 Then
                        Mto_a_Girar_docto = Format(CDbl(Mto_a_Girar * Ponderacion_Anticipo) / 100, fmt.FSMSD)
                    End If
                End If

                NRO_DIAS_VCTO = DateDiff("d", CDate(Me.Txt_FecSimulacion.Text), ses_ope.Coll_DSI.Item(idx).dsi_fev_cal)
                CANT_DIAS = NRO_DIAS_VCTO

                'Diferencia de Precio

                DIF_PRECIO = fr.DiferenciaDePrecio(ses_ope.Coll_DSI.Item(idx).dsi_fev_cal, _
                                                   CDate(Txt_FecSimulacion.Text).ToShortDateString, _
                                                   MONTO_ANTICIPO, _
                                                   Txt_Negocio.Text, _
                                                   Rb_mora.SelectedValue, _
                                                   ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ope_lnl, _
                                                   diasbase)

                'Precio Compra
                PRECIO_COMPRA = Format(CDbl(MONTO_DOCUMENTO - DIF_PRECIO), formato)

                'Saldo Pendiente
                SALDO_PENDIENTE = Format(CDbl(MONTO_DOCUMENTO - MONTO_ANTICIPO), formato)

                'Saldo a Pagar
                SALDO_A_PAGAR = Format(CDbl(MONTO_ANTICIPO - DIF_PRECIO), formato)

                'Comisión X Docto.
                COMISION_APLICADA = Format(CDbl(CSng(IIf(Trim(Txt_Porc_com.Text) = "", 0, Txt_Porc_com.Text)) / 100) * MONTO_DOCUMENTO, formato)


                Select Case COMISION_APLICADA
                    Case Is < MONTO_MIN
                        COMISION_APLICADA = MONTO_MIN

                    Case Is > MONTO_MAX
                        COMISION_APLICADA = MONTO_MAX
                End Select


                COMISION_APLICADA = Format(CDec(COMISION_APLICADA), formato)

                COMISION_TOTAL = COMISION_TOTAL + COMISION_APLICADA

                iva = Format(CDec(COMISION_APLICADA * PORCENTAJE_IVA), formato)

                With doc

                    .dsi_num = Coll_DSI.Item(idx).dsi_num
                    .dsi_sal_pag = SALDO_A_PAGAR
                    .dsi_sal_pen = SALDO_PENDIENTE
                    .dsi_iva_cms = iva

                    If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 <> 1 Then
                        .dsi_dif_pre = DIF_PRECIO
                    Else
                        .dsi_dif_pre = Math.Round(DIF_PRECIO, MidpointRounding.ToEven)
                    End If

                    .dsi_mto_ant = MONTO_ANTICIPO
                    .dsi_cms = COMISION_APLICADA
                    .dsi_ctd_dia = CANT_DIAS
                    .dsi_flj_num = ses_ope.Coll_DSI.Item(idx).dsi_flj_num

                End With

                'jlagos 29-09-2015 -AGREGAMOS VALIDACION  
                If Val(doc.dsi_dif_pre) <= 0 Then
                    msj.Mensaje(Me, "Atención", "El Documento N° " & doc.dsi_num & " se encuentra con descuento cero", 5)
                    Return False
                End If

                sesion.coll_dsi_simu.Add(doc)

            Next


            ''***************************************************************************************************
            ''***** DATOS SIMULACION (OPERACION) ****************************************************************

            ' ses_ope.Coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope = Format(CLng(ses_ope.Coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope), fmt.FSMSD)
            VALOR_UF = fg.RETORNA_NUMERO(Format(CDbl(uf.Value), fmt.FSMCD))

            TASA_BASE = fg.RETORNA_NUMERO(Format(CDbl(Me.Txt_Tasa_Base.Text), fmt.FSMCD))
            SPREAD = fg.RETORNA_NUMERO(Format(CDbl(Me.Txt_Spread.Text), fmt.FSMCD))
            PUNTOS = fg.RETORNA_NUMERO(Format(CDbl(Me.Txt_Puntos.Text), fmt.FSMCD))
            IMPUESTO = Format(CDbl(Me.Txt_Impuestos.Text), fmt.FSMSD)
            VALOR_US = fg.RETORNA_NUMERO(Format(CDbl(uf.Value), fmt.FSMCD))
            TASA_MAX_CONV = fg.RETORNA_NUMERO(Format(CDbl(tmc.Value), fmt.FSMCD))

            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 2 Then


                MONTO_ANTICIPAR = Format(CDbl(Me.Txt_Monto_anticipar.Text), fmt.FCMCD4)
                PRECIO_COMPRA = Format(CDbl(Me.Txt_Precio_compra.Text), fmt.FCMCD4)
                DIFERENCIA_PRECIO = Format(CDbl(Me.Txt_dif_precio.Text), fmt.FCMCD4)
                SALDO_PENDIENTE = Format(CDbl(Me.Txt_Saldo_pendiente.Text), fmt.FCMCD4)
                SALDO_PAGAR = Format(CDbl(Me.Txt_Saldo_pagar.Text), fmt.FCMCD4)
                COMISION_TOTAL = COMISION_TOTAL + CDbl(Me.Txt_Com_esp.Text)
                IVA_COMISION = Format(CDbl(Me.Txt_Iva.Text), fmt.FCMCD4)
                TOTAL_GASTOS = Format(CDbl(Me.Txt_Gastos.Text), fmt.FCMCD4)
                IMPUESTO = Format(CDbl(Me.Txt_Impuestos.Text), fmt.FCMCD4)
                MONTO_GIRAR = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FCMCD4)

            ElseIf ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 3 Or 4 Then


                MONTO_ANTICIPAR = Format(CDbl(Me.Txt_Monto_anticipar.Text), fmt.FCMCD)
                PRECIO_COMPRA = Format(CDbl(Me.Txt_Precio_compra.Text), fmt.FCMCD)
                DIFERENCIA_PRECIO = Format(CDbl(Me.Txt_dif_precio.Text), fmt.FCMCD)
                SALDO_PENDIENTE = Format(CDbl(Me.Txt_Saldo_pendiente.Text), fmt.FCMCD)
                SALDO_PAGAR = Format(CDbl(Me.Txt_Saldo_pagar.Text), fmt.FCMCD)
                COMISION_TOTAL = COMISION_TOTAL + CDbl(Me.Txt_Com_esp.Text)
                IVA_COMISION = Format(CDbl(Me.Txt_Iva.Text), fmt.FCMCD)

                If Txt_Gastos.Text <> "" Then
                    If Not IsNothing(Txt_Gastos.Text) Then
                        TOTAL_GASTOS = Format(CDbl(Me.Txt_Gastos.Text), fmt.FCMCD)
                    End If
                End If
                IMPUESTO = Format(CDbl(Me.Txt_Impuestos.Text), fmt.FCMCD)
                MONTO_GIRAR = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FCMCD)
            Else
                MONTO_ANTICIPAR = Format(CDbl(Me.Txt_Monto_anticipar.Text), fmt.FCMSD)
                PRECIO_COMPRA = Format(CDbl(Me.Txt_Precio_compra.Text), fmt.FCMSD)
                DIFERENCIA_PRECIO = Format(CDbl(Me.Txt_dif_precio.Text), fmt.FCMSD)
                SALDO_PENDIENTE = Format(CDbl(Me.Txt_Saldo_pendiente.Text), fmt.FCMSD)
                SALDO_PAGAR = Format(CDbl(Me.Txt_Saldo_pagar.Text), fmt.FCMSD)
                COMISION_TOTAL = COMISION_TOTAL + CDbl(Me.Txt_Com_esp.Text)
                IMPUESTO = Format(CDbl(Me.Txt_Impuestos.Text), fmt.FCMSD)
                IVA_COMISION = Format(CDbl(Me.Txt_Iva.Text), fmt.FCMSD)
                'TOTAL_GASTOS = Format(CDbl(Me.Txt_Gastos.Text), fmt.FCMSD)
                If Txt_Gastos.Text <> "" Or Not IsNothing(Txt_Gastos.Text) Then
                    TOTAL_GASTOS = Format(CDbl(Me.Txt_Gastos.Text), fmt.FCMCD)
                End If
                MONTO_GIRAR = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FCMSD)
            End If

            MONEDA_COMISION = Dr_Moneda.SelectedValue
            MONEDA_COMISION_FLAT = Me.Dr_mone.SelectedValue
            PORCENTAJE_COMISION = fg.RETORNA_NUMERO(Format(CDbl(Txt_Porc_com.Text), fmt.FSMCD))

            If MONEDA_COMISION <> 1 Then
                COMISION_MINIMA = Format(CDbl(fg.RETORNA_NUMERO(Txt_Min.Text)), fmt.FSMCD)
                COMISION_MAXIMA = Format(CDbl(fg.RETORNA_NUMERO(Txt_Max.Text)), fmt.FSMCD)
            Else
                COMISION_MINIMA = Format(CDbl(Txt_Min.Text), fmt.FSMSD)
                COMISION_MAXIMA = Format(CDbl(Txt_Max.Text), fmt.FSMSD)
            End If

            If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023 = 1 Then
                COMISION_FLAT = cg.RETORNA_VALOR_MONEDA(CStr(Txt_Comi.Text), CInt(MONEDA_COMISION_FLAT), CInt(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023), fg.FUNFechaJul(Me.Txt_FecSimulacion.Text))
            Else
                COMISION_FLAT = cg.RETORNA_VALOR_MONEDA(CStr(Txt_Comi.Text), CInt(MONEDA_COMISION_FLAT), CInt(ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023), fg.FUNFechaJul(Me.Txt_FecSimulacion.Text))
            End If

            If MONEDA_COMISION_FLAT <> 1 Then
                PORCENTAJE_FLAT = Format(CDbl(Txt_Porc_Antic.Text), fmt.FSMCD)
            Else
                PORCENTAJE_FLAT = Format(CDbl(Txt_Porc_Antic.Text), fmt.FSMCD)
            End If

            PORCENTAJE_ANTICIPAR = fg.RETORNA_NUMERO(Format(CDbl(Txt_Antic.Text), fmt.FSMCD))

            TIP_BEN = Nothing
            GARANTIA = " "
            MTO_LIN = 0
            FEC_VTO_FOG = "01/01/1900"
            OBJETIVO = 0
            PorComFogape = 0
            ComisionFogape = 0
            PAIS = Nothing

            ''***** EGRESO ANTICIPO ******************************************************************************
            Select Case coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0012
                Case 1, 4 'Con Giro y Abono Anticipo

                    'Guarda Egreso Sin Giro

                    TOTAL_ABONADO = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FSMCD)
                    TOTAL_ABONADO = cg.RETORNA_VALOR_MONEDA(CDbl(TOTAL_ABONADO), coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, fg.FUNFechaJul(Me.Txt_FecSimulacion.Text))

                    QUE_SE_PAGA = 4
                    TIPO_EGRESO = Me.Dr_For_Pgo.SelectedValue
                    ANTES_DE_14_HRS = IIf(Me.Ch_Ats_14hrs.Checked, "S", "N")

                    If Me.Dr_Bco.SelectedIndex > 0 Then
                        BancoEgreso = Me.Dr_Bco.SelectedValue
                    Else
                        BancoEgreso = 0
                    End If

                    If Trim(Txt_Cta_Cte.Text) = "" Then
                        CtaCteEgreso = ""
                    Else
                        CtaCteEgreso = Trim(Me.Txt_Cta_Cte.Text)
                    End If

                    ' ope_num = Format(CLng(ope_num), fmt.FSMSD)
                    OBSERVACION_EGR = " "
                    NRO_CXP = 0

                    GRABA_EGRESO_CON_GIRO()

                Case 2
                    Dim id_cxp As Integer
                    'Genera cuenta x pagar

                    Dim cxp As New cxp_cls

                    With cxp
                        .id_P_0041 = 1
                        .id_P_0057 = 1
                        .cxp_mto = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FSMCD)
                        .cxp_des = "SALDO OPERACION SIN GIRO: " & ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                        .id_P_0023 = coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023

                    End With

                    MONTO_CXP = Format(CDbl(Me.Txt_saldo_total.Text), fmt.FSMCD)
                    MONTO_CXP = cg.RETORNA_VALOR_MONEDA(CDbl(MONTO_CXP), coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, FUNFechaJul(Me.Txt_FecSimulacion.Text))
                    TIPO_CXP = 1
                    DESCRIP_CXP = "SALDO OPERACION SIN GIRO : " & ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                    ESTADO_CXP = 1
                    CXP_CXC = 1

                    id_cxp = CTA.CXPInserta(cxp)

                    If id_cxp > 0 Then
                        Dim sim_cxp As New sim_cxp_cls

                        sim_cxp.id_ope = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                        sim_cxp.id_cxp = id_cxp

                        'Graba Asociación Cxp_Ope

                        OP.cxc_cxp_egr_asocia_por_operacion(TipoDocumentoAsociar.CXP, , , sim_cxp)

                    End If

                Case 3 'solo cobranza
                    Dim id_cxc As Integer
                    'Genera cuenta x Cobrar       

                    MONTO_CXP = Format(CDbl(Me.Txt_Com_x_dcto.Text), fmt.FSMCD)
                    MONTO_CXP = MONTO_CXP + Format(CDbl(Me.Txt_Iva.Text), fmt.FSMCD)
                    MONTO_CXP = MONTO_CXP + Format(CDbl(Me.Txt_Gastos.Text), fmt.FSMCD)
                    MONTO_CXP = MONTO_CXP + Format(CDbl(Me.Txt_Com_esp.Text), fmt.FSMCD)

                    MONTO_CXP = cg.RETORNA_VALOR_MONEDA(CDbl(MONTO_CXP), coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, 1, Me.Txt_FecSimulacion.Text)
                    TIPO_CXP = 2

                    DESCRIP_CXP = "COMISION OPERACION SOLO COBRANZA NRO : " & ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                    ESTADO_CXP = 1

                    Dim cxc As New cxc_cls

                    With cxc
                        .id_P_0041 = 2
                        .id_P_0057 = 2
                        .cxc_mto = MONTO_CXP
                        .cxc_des = "ANTICIPO OPERACION NRO : " & ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                        .cxc_abo_ant = "N"
                        .id_P_0023 = coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023
                    End With

                    If MONTO_CXP > 0 Then
                        id_cxc = CTA.CxcInserta(cxc)
                        If id_cxc > 0 Then
                            Dim sim_cxc As New sim_cxc_cls

                            sim_cxc.id_ope = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                            sim_cxc.id_cxc = id_cxc

                            'Graba Asociación Cxp_Ope

                            OP.cxc_cxp_egr_asocia_por_operacion(TipoDocumentoAsociar.CXC, , sim_cxc, )

                        End If


                    End If
            End Select

            ''***** FIN EGRESO SIN GIRO (APLICACION) ********************************************************************
            GRABA_DATOS_SIMULACION()

            Return True


        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", "Calculos: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
            Return False
        End Try


    End Function

    Public Sub GRABA_EGRESO_SIN_GIRO()

        Try

            Dim egr As New egr_cls

            With egr
                .cli_idc = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).cli_idc
                .egr_fec = Date.Now
                .egr_obs = OBSERVACION_EGR
                .id_eje = CodEje
                .id_opo = Nothing ' se le asigna en el otorgamiento

            End With

            If IsNothing(pagos.coll_egr) Then
                pagos.coll_egr = New Collection
            End If
            If coll_egr.Count = 0 Then
                pagos.coll_egr.Add(egr)
            End If

            Dim Egr_Sec As New egr_sec_cls

            With Egr_Sec

                .egr_mto = Round(TOTAL_ABONADO, 2)
                .id_P_0056 = TIPO_EGRESO

                If NRO_CXP = 0 Then
                    .id_cxp = Nothing
                Else
                    .id_cxp = NRO_CXP
                End If

                .id_P_0055 = 4
                .egr_dep_ant = ANTES_DE_14_HRS
                .egr_vld_rcz = "S"
                .egr_ent = "N"

                If BancoEgreso = 0 Then
                    .id_bco = Nothing
                Else
                    .id_bco = BancoEgreso
                End If

                .egr_cta_cte = CtaCteEgreso

            End With

            If IsNothing(pagos.coll_egr_sec) Then
                pagos.coll_egr_sec = New Collection
            End If

            pagos.coll_egr_sec.Add(Egr_Sec)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub GRABA_EGRESO_CON_GIRO()
        Try

            Dim egr As New egr_cls

            With egr
                .cli_idc = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).cli_idc
                .egr_fec = Date.Now
                .egr_obs = OBSERVACION_EGR
                .id_eje = CodEje
                .id_opo = Nothing ' se le asigna en el otorgamiento

            End With


            If IsNothing(pagos.coll_egr) Then
                pagos.coll_egr = New Collection
            End If

            If coll_egr.Count = 0 Then
                pagos.coll_egr.Add(egr)
            End If


            Dim Egr_Sec As New egr_sec_cls

            With Egr_Sec
                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_P_0012 = 1 Then
                    .egr_mto = Round(Me.Txt_saldo_total.Text, 2)
                Else
                    .egr_mto = Round(TOTAL_ABONADO, 2)
                End If

                If ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_P_0012 = 1 Then
                    .id_P_0056 = Me.Dr_For_Pgo.SelectedValue
                Else
                    .id_P_0056 = TIPO_EGRESO
                End If


                If NRO_CXP = 0 Then
                    .id_cxp = Nothing
                Else
                    .id_cxp = NRO_CXP
                End If
                ' P_0053 NO APLICA
                '     .id_P_0053 = QUE_SE_PAGA
                .id_P_0055 = 4
                .egr_dep_ant = ANTES_DE_14_HRS
                .egr_vld_rcz = "S"
                .egr_ent = "N"
                If BancoEgreso = 0 Then
                    .id_bco = Nothing
                Else
                    .id_bco = BancoEgreso
                End If

                .egr_cta_cte = CtaCteEgreso
            End With

            If IsNothing(pagos.coll_egr_sec) Then
                pagos.coll_egr_sec = New Collection
            End If


            pagos.coll_egr_sec.Add(Egr_Sec)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub GRABA_INGRESO(ByVal saldo_cli As Double, ByVal saldo_Deu As Double)
        Try


            'RuT_CLIENTE = Format(CLng(RuT_CLIENTE), var.FMT_RUT)

            If QUE_SE_PAGA = 1 Then

                Dim ing As New ing_cls

                With ing


                    .ing_sis_fec = Date.Now
                    .ing_fec = Me.Txt_FecSimulacion.Text

                    'ag.PagosInserta(
                End With


                Dim ing_sec As New ing_sec_cls

                With ing_sec

                    .ing_mto_tot = TOTAL_ABONADO
                    .ing_mto_abo = MONTO_ABONADO
                    .ing_mto_int = MONTO_INTERES
                    .ing_qpa = "O"
                    .id_P_0053 = QUE_SE_PAGA
                    .id_cxc = NRO_CTA_X_COB

                    .ing_rea_mon = REAJUSTE
                    .doc_sdo_cli = 0
                    .doc_sdo_ddr = 0
                    .ing_fac_cam_obs = FACTOR_CAMBIO_OBS
                    .id_doc = Nothing

                    .id_nce = Nothing
                    .id_dpo = Nothing
                    .ing_pro = "N"
                End With

                If IsNothing(pagos.Coll_Ing_Sec) Then
                    pagos.Coll_Ing_Sec = New Collection
                End If

                pagos.Coll_Ing_Sec.Add(ing_sec)

            Else
                Dim ing As New ing_cls

                With ing


                    .ing_sis_fec = Date.Now
                    .ing_fec = Me.Txt_FecSimulacion.Text

                    'ag.PagosInserta(
                End With


                Dim ing_sec As New ing_sec_cls

                With ing_sec

                    .ing_mto_tot = TOTAL_ABONADO
                    .ing_mto_abo = MONTO_ABONADO
                    .ing_mto_int = MONTO_INTERES
                    .ing_qpa = "O"
                    .id_P_0053 = QUE_SE_PAGA
                    .id_cxc = Nothing
                    .ing_rea_mon = REAJUSTE
                    .doc_sdo_cli = 0
                    .doc_sdo_ddr = 0
                    .ing_fac_cam_obs = FACTOR_CAMBIO_OBS
                    .id_doc = ses_ope.NUM_DOC = pagos.Coll_Doctos_Seleccionados.Item(JDX).id_doc
                    .id_nce = Nothing
                    .id_dpo = Nothing
                    .ing_pro = "N"
                End With

                If IsNothing(pagos.Coll_Ing_Sec) Then
                    pagos.Coll_Ing_Sec = New Collection
                End If

                pagos.Coll_Ing_Sec.Add(ing_sec)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub GRABA_DATOS_SIMULACION()

        Try

            Dim OPE As New ope_cls
            Dim vld As Boolean

            With OPE

                OPE.id_ope = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope
                OPE.ope_fec_sim = Format(CDate(Me.Txt_FecSimulacion.Text), "dd/MM/yyyy") & " " & Date.Now.ToLongTimeString
                'OPE.ope_val_duf = vuf.Text
                OPE.ope_imp_ope = fg.comasXptos(IMPUESTO)
                OPE.ope_val_dus = vdolar.Text
                OPE.ope_tmc_dsi = TASA_MAX_CONV
                OPE.id_P_0030 = 2 'Est Ope Simulada
                OPE.ope_dif_pre = DIFERENCIA_PRECIO
                OPE.ope_sal_pag = SALDO_PAGAR
                OPE.ope_pre_com = PRECIO_COMPRA
                OPE.ope_sal_pen = SALDO_PENDIENTE
                OPE.ope_com_tot = CDbl(Me.Txt_Com_x_dcto.Text) + CDbl(Me.Txt_Com_esp.Text) 'COMISION_TOTAL
                OPE.ope_int_dev = INTERES_A_DEVOLVER
                OPE.ope_mon_gas = TOTAL_GASTOS
                OPE.ope_fog_ven = CDate(FEC_VTO_FOG)
                OPE.ope_fog_son = FOGAPE
                OPE.id_P_0070 = PAIS
                OPE.ope_mto_ant = MONTO_ANTICIPAR
                OPE.ope_cuo = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).ope_cuo
                OPE.ope_tot_gir = Me.Txt_saldo_total.Text
                OPE.ope_iva_com = Me.Txt_Iva.Text
                OPE.id_eje = CodEje
                OPE.ope_mto_scb = OPE.ope_com_tot + OPE.ope_iva_com + CDbl(Me.Txt_DeScuentos.Text)
                OPE.id_opn = coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_opn
                OPE.ope_fac_cam = cg.ParidadDevuelve(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_p_0023, Me.Txt_FecSimulacion.Text).par_val
                OPE.ope_val_gmf = CDbl(Me.Txt_Valor_GMF.Text)
                OPE.ope_tot_gir_ant = OPE.ope_tot_gir - OPE.ope_val_gmf
                OPE.ope_mon_gas_afe = CDbl(Txt_GastosAfectos.Text)

            End With

            vld = OP.simulacion_ingresa(OPE, _
                                        Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT), _
                                        sesion.coll_dsi_simu, _
                                        CDbl(Txt_Valor_GMF.Text))

            Dim ARRAY_EGR As New ArrayList
            Dim egreso As New Collection 'Variable que brindara el id_egr y egr_sec asociado al Pago 
            Dim ig_egr_sec_pagos As Integer

            If Not IsNothing(pagos.coll_egr_sec) Then

                If pagos.coll_egr_sec.Count > 0 Then

                    Dim egr As New egr_cls

                    With egr
                        .cli_idc = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).cli_idc
                        .egr_fec = Date.Now
                        .egr_obs = OBSERVACION_EGR
                        .id_eje = CodEje
                        .id_opo = Nothing ' se le asigna en el otorgamiento

                    End With

                    'Devuelve numero de Egreso para asociar en Nub 
                    egreso = OP.egreso_inserta(egr, pagos.coll_egr_sec)

                    For I = 1 To egreso.Count
                        If egreso.Item(I).id_p_0056 = 5 Then
                            ig_egr_sec_pagos = egreso.Item(I).ID_EGR_SEC
                        End If
                        ARRAY_EGR.Add(egreso.Item(I).ID_EGR_SEC)
                    Next

                    If Not IsNothing(egreso) Then


                        'Valida que sea mayor a Cero e inserta la relación
                        If egreso.Count > 0 Then

                            Dim sim_egr As New sim_egr_cls

                            sim_egr.id_egr = egreso.Item(1).id_egr
                            sim_egr.id_ope = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope

                            'Graba Asociación Egr_Ope
                            OP.cxc_cxp_egr_asocia_por_operacion(TipoDocumentoAsociar.EGR, sim_egr)

                        End If
                    End If
                End If
            End If

            '************************************************************************************
            'APLICACION DE DOCUMENTOS Y CUENTAS
            '************************************************************************************
            If Not IsNothing(pagos.Coll_Cxc_Seleccionados) Or Not IsNothing(pagos.Coll_Doctos_Seleccionados) Then
                Dim coll_rec As New Collection

                ' Busca egreso sin giro para generar ing_sec
                For i = 1 To coll_egr_sec.Count
                    If coll_egr_sec.Item(i).id_p_0056 = 5 Then
                        coll_rec.Add(coll_egr_sec.Item(i))
                    End If
                Next

                fr.CargaCollection_Ingresos(coll_rec, pagos.Coll_Cxc_Seleccionados, pagos.Coll_Doctos_Seleccionados, 2, Me.Txt_FecSimulacion.Text)

            End If

            If Not IsNothing(pagos.Coll_Ing_Sec) Then

                'Si posee documentos o Cxc a pagar llena objeto e ingresa 
                If pagos.Coll_Ing_Sec.Count > 0 Then

                    Dim ing As New ing_cls

                    With ing
                        .ing_sis_fec = Date.Now
                        .ing_fec = Me.Txt_FecSimulacion.Text
                    End With

                    For i = 1 To pagos.Coll_Ing_Sec.Count

                        pagos.Coll_Ing_Sec.Item(i).id_egr_sec = ig_egr_sec_pagos
                        pagos.Coll_Ing_Sec.Item(i).id_dpo = Nothing
                        pagos.Coll_Ing_Sec.Item(i).ing_qpa = "O"
                    Next

                    PGO.Pagos_Operacion_Inserta(ing, pagos.Coll_Ing_Sec)

                End If

            End If

            OP.ProrrateoDeDocumentos(OPE.id_ope)

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", "Graba Datos Simulacion: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub InhabilitaCamposopesim()
        Try
            ' Me.Txt_Raz_Soc.Text = ""
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"
            'Tasas


            Me.Txt_Porc_com.CssClass = "clsDisabled"
            Me.Txt_Porc_com.ReadOnly = True

            Me.Txt_Tasa_Base.ReadOnly = True
            Me.Txt_Tasa_Base.CssClass = "clsDisabled"

            Me.Txt_Spread.ReadOnly = True
            Me.Txt_Spread.CssClass = "clsDisabled"

            Me.Txt_Puntos.ReadOnly = True
            Me.Txt_Puntos.CssClass = "clsDisabled"

            Me.Txt_Tnego.ReadOnly = True
            Me.Txt_Tnego.CssClass = "clsDisabled"
            'Comisiones

            Me.Txt_Comi.ReadOnly = True
            Me.Txt_Comi.CssClass = "clsDisabled"

            Me.Txt_Max.ReadOnly = True
            Me.Txt_Max.CssClass = "clsDisabled"

            Me.Txt_Min.ReadOnly = True
            Me.Txt_Min.CssClass = "clsDisabled"


            'Anticipo

            Me.Txt_Porc_Antic.ReadOnly = True
            Me.Txt_Porc_Antic.CssClass = "clsDisabled"
            'Pago a Cliente

            Me.Txt_Cta_Cte.ReadOnly = True
            Me.Txt_Cta_Cte.CssClass = "clsDisabled"

            'DropList
            Me.Dr_Moneda.Enabled = False
            Me.Dr_Moneda.CssClass = "clsDisabled"
            Me.Dr_mone.Enabled = False
            Me.Dr_mone.CssClass = "clsDisabled"
            Me.Dr_For_Pgo.Enabled = False
            Me.Dr_For_Pgo.CssClass = "clsDisabled"
            Me.Dr_Bco.Enabled = False
            Me.Dr_Bco.CssClass = "clsDisabled"

            'RadioButton

            Me.Ch_Ats_14hrs.Enabled = False


            'Button
            Me.Btn_Buscar.Enabled = True
            Me.Btn_Limpiar.Enabled = True
            Me.Btn_Ope.Enabled = False
            Me.Btn_negoc.Enabled = False
            If Me.Rb_Sim.Checked Then
                Me.Btn_Anu_Sim.Enabled = True
            Else
                Me.Btn_Anu_Sim.Enabled = False

            End If

            Me.Btn_Ing_pag.Enabled = False
            Me.Btn_Imp_Sim.Enabled = True
            Me.Btn_Asoc.Enabled = False
            Me.btn_descto.Enabled = False

            Me.btn_calc.Enabled = False
            Me.btn_descto.Enabled = False
            btn_gast_imp.Enabled = False

        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub CargaOpeSimuladas()
        Try

            limpia_simulacion()
            Me.Txt_ItemOPE.Text = ""
            ses_ope.coll_ope = New Collection
            ses_ope.coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 3, True, Me.Gr_Operaciones)
            Me.Gr_Operaciones.Controls.Clear()
            If Not IsNothing(ses_ope.coll_ope) Then

                If (ses_ope.coll_ope.Count > 0) = False Then
                    '  Me.Btn_Limpiar_Click(Me, evnt)
                    ' Me.Txt_Rut.Focus()
                    msj.Mensaje(Me, "Atención", "No existen operaciones simuladas para este cliente", 2)
                    Exit Sub
                End If
            End If

            Gr_Operaciones.DataSource = ses_ope.coll_ope
            Gr_Operaciones.DataBind()
            Me.btn_descto.Enabled = False
            Me.btn_gast_imp.Enabled = False
            Me.Btn_Anu_Sim.Enabled = True
            marcagrilla()
            InhabilitaCamposopesim()
            Me.Btn_Anu_Sim.Enabled = True
            Btn_Imp_Sim.Enabled = True
            Me.btn_guardar.Enabled = False
            btn_descto.Enabled = False
            btn_gast_imp.Enabled = False
            Txt_GastImp.Text = ""



        Catch ex As Exception
            msj.Mensaje(Page, "Atencion", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

End Class
