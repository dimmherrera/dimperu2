Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports ClsSession.SesionOperaciones
Imports ClsSession.SesionAplicaciones

Imports CapaDatos


Partial Class Modulos_Carp_Aplicaciones
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim FG As New FormulasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Aplicaciones"
    Dim Pagos As New ClsSession.SesionPagos
    Dim Posicion As Integer = 0
    Dim Posicion_CxC As Integer = 0
    Dim Posicion_CxP As Integer = 0
    Dim Posicion_DNC As Integer = 0
    Dim RC As New FuncionesGenerales.FComunes

    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim CMC As New ClaseComercial
    Dim PGO As New ClasePagos
    Dim CTA As New ClaseCuentas
    Dim CBR As New ClaseCobranza
    Dim OP As New ClaseOperaciones
    Dim CL As New ClaseClientes
    
#End Region

#Region "BOTONERA"

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20030604, Usr, "PRESIONO GUARDAR APLICACION DE EXCEDENTES") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Validaciones
            '-----------------------------------------------------------------------------------------------------------------------------------

            If Not Valida_Condiciones() Then
                Exit Sub
            End If

            If Not VALIDA_ABONO() Then
                Exit Sub
            End If

            If Me.CB_Sin_Devolucion.Checked = False Then

                If DP_TipoEgreso.SelectedValue = 0 Then
                    Msj.Mensaje(Page, Caption, "Seleccione tipo de egreso", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Dim par As Collection

                par = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, DP_TipoEgreso.SelectedValue)

                If par.Count > 0 Then
                    If par.Item(1).pnu_atr_003 = "S" Then
                        If DP_Banco.SelectedValue = 0 Then
                            Msj.Mensaje(Page, Caption, "Seleccione banco", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If
                        If Txt_Cta_Cte.Text = "" Then
                            Msj.Mensaje(Page, Caption, "Ingrese cta. cte.", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If
                    End If
                End If

            End If

            If Txt_Observacion.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese observación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Msj.Mensaje(Me.Page, Caption, "¿ Desea realizar aplicación ?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)
            '05-08-2014 se agrega boton ajax de confirmacion por validacion en doble click

            Try

                Dim AplicacionCliente As Object

                AplicacionCliente = Session("Aplicacion")

                '-----------------------------------------------------------------------------------------------------------------------------------
                'Cargamos los Egresos
                '-----------------------------------------------------------------------------------------------------------------------------------
                Dim fmt As New FuncionesGenerales.ClsLocateInfo


                coll_egr_sec = New Collection

                CargaCollection_Excedentes()
                CargaCollection_CuentasPorPagar()
                CargaCollection_DoctosNoCedidos()

                '-----------------------------------------------------------------------------------------------------------------------------------
                'Creamos los Ingresos asociados a los egresos
                '-----------------------------------------------------------------------------------------------------------------------------------

                'La funcion llena una collection de sesion que se llama coll_ing_sec
                FG.CargaCollection_Ingresos(coll_egr_sec, _
                                            Coll_Cxc_Seleccionados, _
                                            Coll_Doctos_Seleccionados, _
                                            2, _
                                            Txt_Fecha_Aplicacion.Text)

                '-----------------------------------------------------------------------------------------------------------------------------------
                'Creamos la cabecera del Egreso
                '-----------------------------------------------------------------------------------------------------------------------------------

                Dim Egresos As New egr_cls
                With Egresos
                    .id_egr = Nothing
                    .id_apl = Nothing 'Se le asiganara una vez que se haya ingresado la aplicacion
                    .id_opo = Nothing 'No aplica, solo de operacion y otorgamiento
                    .id_eje = CodEje
                    .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                    .egr_fec = Txt_Fecha_Aplicacion.Text
                    .egr_obs = Txt_Observacion.Text.Trim
                End With

                '-----------------------------------------------------------------------------------------------------------------------------------
                'Creamos la cabecera del Ingreso
                '-----------------------------------------------------------------------------------------------------------------------------------
                Dim Ingresos As New ing_cls
                With Ingresos
                    .id_ing = Nothing
                    .id_eje = CodEje
                    .id_hre = Nothing
                    '.cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                    .ing_fec = Txt_Fecha_Aplicacion.Text
                    .ing_obs = Txt_Observacion.Text.Trim
                    .ing_sis_fec = Date.Now

                    .ing_pgo_hre = "N"
                End With

                '-----------------------------------------------------------------------------------------------------------------------------------
                'Creamos la Aplicacion
                '-----------------------------------------------------------------------------------------------------------------------------------
                Dim Aplicacion As New apl_cls
                With Aplicacion
                    .id_apl = Nothing
                    .id_eje = CodEje
                    .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                    .apl_fec = Txt_Fecha_Aplicacion.Text
                    .apl_num_mod = 0
                    .apl_exc_mto = CDbl(Txt_Pag_Exc.Text)
                    .apl_cxp_mto = CDbl(Txt_Pag_Cxp.Text)
                    .apl_dnc_mto = CDbl(Txt_Pag_Dnc.Text)
                    .apl_cxc_mto = CDbl(Txt_Pag_CxC.Text)

                    If txt_total_doc.Text = "" Then
                        txt_total_doc.Text = 0
                    End If

                    .apl_dvg_mto = CDbl(txt_total_doc.Text)
                    .apl_con_dev = If(CB_Sin_Devolucion.Checked = True, "N", "S")
                    .apl_dia_int_dev = If(IsNothing(AplicacionCliente), 0, AplicacionCliente.apl_dia_int_dev)
                    .apl_tas_cli = CDec(Txt_Tasa_Cliente.Text)
                    .apl_tas_apl = CDec(Txt_Tasa_Aplicar.Text)
                    .apl_sdo = Math.Abs(CDbl(Txt_Saldo.Text))
                    .apl_fec_cre = Date.Now
                    .apl_obs = Txt_Observacion.Text.Trim
                End With

                '-----------------------------------------------------------------------------------------------------------------------------
                'GUARDAMOS LA APLICACION
                '-----------------------------------------------------------------------------------------------------------------------------
                For I = 1 To Coll_Ing_Sec.Count
                    Coll_Ing_Sec.Item(I).ing_qpa = "E"
                Next

                Dim id_apl As Integer

                id_apl = CMC.Aplicaciones_Guarda(coll_egr_sec, Coll_Ing_Sec, Egresos, Ingresos, Aplicacion)

                If id_apl > 0 Then
                    CMC.Aplicacion_devolucion_genera(id_apl)
                    Limpiar()
                    HabilitaBotones(True)
                    HabilitaDesabilitaCliente(True)
                    Msj.Mensaje(Me.Page, Caption, "Aplicación guardada", TipoDeMensaje._Informacion)
                Else
                    Msj.Mensaje(Me.Page, Caption, "Aplicación no guardada", TipoDeMensaje._Informacion)
                End If

            Catch ex As Exception
                Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
            End Try

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            If Not agt.ValidaAccesso(20, 20010604, Usr, "PRESIONO BUSCAR APLICACION DE EXCEDENTES") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CargaDatosCliente() Then

                'Aplicaciones del cliente
                Dim AplicacionCliente As Object
                Dim CODI_EJE_APL As Integer
                Dim VALID As String
                Dim TASA_INT As Object
                Dim NRO_DIAS_DEV_INTERES As Integer
                Dim fechas As String

                CL.BancosDevuelvePorCliente(True, DP_Banco, Nothing, CLng(Txt_Rut_Cli.Text))

                If DP_Banco.Items.Count > 0 Then
                    FormaPago(True)
                End If

                AplicacionCliente = DevuelveAplicacionCliente()

                Session("Aplicacion") = AplicacionCliente

                If Not IsNothing(AplicacionCliente) Then

                    HF_NroAplicacion.Value = AplicacionCliente.Nro_Apli
                    CODI_EJE_APL = AplicacionCliente.id_Ejecutivo
                    VALID = AplicacionCliente.apl_apb_com
                    TASA_INT = AplicacionCliente.Tasa_Apli
                    NRO_DIAS_DEV_INTERES = AplicacionCliente.apl_dia_int_dev
                    fechas = AplicacionCliente.apl_fec

                End If

                Txt_Tasa_Cliente.Text = CMC.TasaRetorna(2, CLng(Txt_Rut_Cli.Text), 0)
                Txt_Tasa_Aplicar.Text = If(TASA_INT = 0, Txt_Tasa_Cliente.Text, TASA_INT)

                Dim Coll_Totales As Collection

                Coll_Totales = PGO.Totales_A_Pagar_Devuelve(Txt_Rut_Cli.Text, 0, "C", Txt_Fecha_Aplicacion.Text)

                If Coll_Totales.Count > 0 Then
                    Txt_Tot_Exc.Text = Format(Coll_Totales.Item(1), Fmt.FCMSD)
                    Txt_Tot_Cxp.Text = Format(Coll_Totales.Item(2), Fmt.FCMSD)
                    Txt_Tot_Dnc.Text = Format(Coll_Totales.Item(3), Fmt.FCMSD)
                    Txt_Tot_CxC.Text = Format(Coll_Totales.Item(4), Fmt.FCMSD)
                    Txt_Tot_Doc.Text = Format(Coll_Totales.Item(5), Fmt.FCMSD)
                End If

                '-----------------------------------------------------------------------------------------------------------------------
                'EXCEDENTES
                CargaExcedentes()

                '-----------------------------------------------------------------------------------------------------------------------
                'DOCUMENTOS NO CEDIDOS
                '-----------------------------------------------------------------------------------------------------------------------
                coll_DNC = New Collection

                coll_DNC = CG.DocumentosNoCedidos_Devuelve(Txt_Rut_Cli.Text, 0, Val(HF_NroAplicacion.Value))

                GV_DNC.DataSource = coll_DNC
                GV_DNC.DataBind()

                FormatoDNC(coll_DNC)

                '-----------------------------------------------------------------------------------------------------------------------
                'CUENTAS POR PAGAR
                '-----------------------------------------------------------------------------------------------------------------------
                Coll_CXP = New Collection

                Coll_CXP = CTA.CuentasPorPagarDevuelve(Txt_Rut_Cli.Text, 1, Val(HF_NroAplicacion.Value))

                GV_CxP.DataSource = Coll_CXP
                GV_CxP.DataBind()

                FormatoGridCxP()

                'Txt_Fecha_Aplicacion.ReadOnly = True
                'Txt_Fecha_Aplicacion.CssClass = "clsDisabled"
                'Txt_Fecha_Aplicacion_CalendarExtender.Enabled = False

                HabilitaBotones(False)
                HabilitaCampos(True)

                '-----------------------------------------------------------------------------------------------------------------------
                'SESION DE PAGOS
                '-----------------------------------------------------------------------------------------------------------------------

                Dim Pagos As New ClsSession.SesionPagos

                With Pagos
                    .IniciarSesionPagos()
                    .RutCliente = Val(Txt_Rut_Cli.Text.Replace(".", ""))
                    .RutDeudor = 0
                    .Pagador = "C"
                    .FechaPago = Me.Txt_Fecha_Aplicacion.Text
                    .DiasRetencionPago = 0 'Hay que ir a buscarlo por tipo de documento
                    .TasaInteresCalculo = CDbl(Me.Txt_Tasa_Aplicar.Text)
                    .DiasDevolverInteres = Val(CG.SistemaDevuelve().sis_dia_dev)
                End With


            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Limpiar()
        HabilitaBotones(True)
        HabilitaDesabilitaCliente(True)

        'limpia colecciones de documentos y cuentas
        Coll_Doctos_Seleccionados = New Collection
        Coll_Cxc_Seleccionados = New Collection

        DP_TipoEgreso.Enabled = False
        DP_TipoEgreso.CssClass = "clsDisabled"

        DP_Banco.Enabled = False
        DP_Banco.CssClass = "clsDisabled"

        Txt_Cta_Cte.ReadOnly = True
        Txt_Cta_Cte.CssClass = "clsDisabled"

        Txt_Observacion.ReadOnly = True
        Txt_Observacion.CssClass = "clsDisabled"

    End Sub

    Protected Sub IB_Aplicaciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aplicaciones.Click

        'Dim RW As New FuncionesGenerales.RutinasWeb
        'RW.AbrePopup(Me, 2, "BusquedaDeAplicaciones.aspx", "BusquedaDeAplicaciones", 600, 500, 100, 100)

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim AplicacionCliente As Object

            AplicacionCliente = Session("Aplicacion")

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Cargamos los Egresos
            '-----------------------------------------------------------------------------------------------------------------------------------
            Dim fmt As New FuncionesGenerales.ClsLocateInfo


            coll_egr_sec = New Collection

            CargaCollection_Excedentes()
            CargaCollection_CuentasPorPagar()
            CargaCollection_DoctosNoCedidos()

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Creamos los Ingresos asociados a los egresos
            '-----------------------------------------------------------------------------------------------------------------------------------

            'La funcion llena una collection de sesion que se llama coll_ing_sec
            FG.CargaCollection_Ingresos(coll_egr_sec, _
                                        Coll_Cxc_Seleccionados, _
                                        Coll_Doctos_Seleccionados, _
                                        2, _
                                        Txt_Fecha_Aplicacion.Text)

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Creamos la cabecera del Egreso
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim Egresos As New egr_cls
            With Egresos
                .id_egr = Nothing
                .id_apl = Nothing 'Se le asiganara una vez que se haya ingresado la aplicacion
                .id_opo = Nothing 'No aplica, solo de operacion y otorgamiento
                .id_eje = CodEje
                .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                .egr_fec = Txt_Fecha_Aplicacion.Text
                .egr_obs = Txt_Observacion.Text.Trim
            End With

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Creamos la cabecera del Ingreso
            '-----------------------------------------------------------------------------------------------------------------------------------
            Dim Ingresos As New ing_cls
            With Ingresos
                .id_ing = Nothing
                .id_eje = CodEje
                .id_hre = Nothing
                '.cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                .ing_fec = Txt_Fecha_Aplicacion.Text
                .ing_obs = Txt_Observacion.Text.Trim
                .ing_sis_fec = Date.Now

                .ing_pgo_hre = "N"
            End With

            '-----------------------------------------------------------------------------------------------------------------------------------
            'Creamos la Aplicacion
            '-----------------------------------------------------------------------------------------------------------------------------------
            Dim Aplicacion As New apl_cls
            With Aplicacion
                .id_apl = Nothing
                .id_eje = CodEje
                .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                .apl_fec = Txt_Fecha_Aplicacion.Text
                .apl_num_mod = 0
                .apl_exc_mto = CDbl(Txt_Pag_Exc.Text)
                .apl_cxp_mto = CDbl(Txt_Pag_Cxp.Text)
                .apl_dnc_mto = CDbl(Txt_Pag_Dnc.Text)
                .apl_cxc_mto = CDbl(Txt_Pag_CxC.Text)

                If txt_total_doc.Text = "" Then
                    txt_total_doc.Text = 0
                End If

                .apl_dvg_mto = CDbl(txt_total_doc.Text)
                .apl_con_dev = If(CB_Sin_Devolucion.Checked = True, "N", "S")
                .apl_dia_int_dev = If(IsNothing(AplicacionCliente), 0, AplicacionCliente.apl_dia_int_dev)
                .apl_tas_cli = CDec(Txt_Tasa_Cliente.Text)
                .apl_tas_apl = CDec(Txt_Tasa_Aplicar.Text)
                .apl_sdo = Math.Abs(CDbl(Txt_Saldo.Text))
                .apl_fec_cre = Date.Now
                .apl_obs = Txt_Observacion.Text.Trim
            End With

            '-----------------------------------------------------------------------------------------------------------------------------
            'GUARDAMOS LA APLICACION
            '-----------------------------------------------------------------------------------------------------------------------------
            For I = 1 To Coll_Ing_Sec.Count
                Coll_Ing_Sec.Item(I).ing_qpa = "E"
            Next

            Dim id_apl As Integer

            id_apl = CMC.Aplicaciones_Guarda(coll_egr_sec, Coll_Ing_Sec, Egresos, Ingresos, Aplicacion)

            If id_apl > 0 Then
                CMC.Aplicacion_devolucion_genera(id_apl)
                Limpiar()
                HabilitaBotones(True)
                HabilitaDesabilitaCliente(True)
                Msj.Mensaje(Me.Page, Caption, "Aplicación guardada", TipoDeMensaje._Informacion)
            Else
                Msj.Mensaje(Me.Page, Caption, "Aplicación no guardada", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            If CargaDatosCliente() Then
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                Txt_Fecha_Aplicacion.CssClass = "clsMandatorio"
                Txt_Fecha_Aplicacion.Focus()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

#Region "FUNCIONES PARA GUARDAR Y VALIDACION"

    Private Sub CreaObjetos()
        Try

            Dim Saldo_Egr As Double
            Dim Saldo_CxC_Docto As Double
            Dim Saldo_Interes As Double
            Dim TotalPago As Double
            Dim Monto As Double
            Dim Interes As Double
            Dim TotalAPagarPorDocto As Double
            Dim SaldoPorPagar As Double
            Dim Ind_cxc As Integer = 1
            Dim Ind_doc As Integer = 1
            Dim Ind_egr As Integer = 1

            Coll_Ing_Sec = New Collection

            Saldo_CxC_Docto = 0
            Saldo_Interes = 0

            For Indice_Egr = Ind_egr To coll_egr_sec.Count

                'Asignamos el saldo del documento de pago
                Saldo_Egr = coll_egr_sec.Item(Indice_Egr).egr_mto
                'coll_egr_sec.Item(Indice_Egr).id_egr_sec = Indice_Egr

                'Haga mientras quede saldo
                While Saldo_Egr > 0

                    'Cuentas por Cobrar
                    If Ind_cxc - 1 <> Coll_Cxc_Seleccionados.Count Then

                        For Indice_cxc = Ind_cxc To Coll_Cxc_Seleccionados.Count

                            FACTOR_CAMBIO_HOY = CG.RETORNA_VALOR_MONEDA_COBRANZA(1, Coll_Cxc_Seleccionados.Item(Indice_cxc).id_p_0023, Txt_Fecha_Aplicacion.Text, 0, 0, 1)
                            FACTOR_CAMBIO_OBS = CG.RETORNA_VALOR_MONEDA(1, Coll_Cxc_Seleccionados.Item(Indice_cxc).id_p_0023, 1, Txt_Fecha_Aplicacion.Text)

                            Select Case Coll_Cxc_Seleccionados.Item(Indice_cxc).id_p_0023
                                Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                                Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                                Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                                Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                            End Select

                            If Saldo_CxC_Docto > 0 Then
                                Monto = Saldo_CxC_Docto
                                Interes = 0
                            Else
                                Monto = Coll_Cxc_Seleccionados.Item(Indice_cxc).MontoPagar * FACTOR_CAMBIO_HOY
                                Interes = Coll_Cxc_Seleccionados.Item(Indice_cxc).Interes * FACTOR_CAMBIO_HOY
                                If Saldo_Interes > 0 Then
                                    Interes = Saldo_Interes
                                End If
                            End If

                            If (Monto + Interes) <= Saldo_Egr Then
                                TotalAPagarPorDocto = (Monto)
                            Else
                                If Interes > Saldo_Egr Then
                                    TotalAPagarPorDocto = 0
                                    Saldo_Interes = Interes - Saldo_Egr
                                    Interes = Saldo_Egr
                                    Monto = 0
                                Else
                                    TotalAPagarPorDocto = Saldo_Egr - Interes
                                End If
                            End If

                            TotalPago = Monto + Interes

                            If (TotalAPagarPorDocto + Interes) > TotalPago Then
                                Saldo_CxC_Docto = (TotalAPagarPorDocto + Interes) - TotalPago
                            Else
                                Saldo_CxC_Docto = TotalPago - (TotalAPagarPorDocto + Interes)
                            End If

                            SaldoPorPagar = Saldo_Egr - (TotalAPagarPorDocto + Interes)

                            If SaldoPorPagar <= 0 Then
                                Saldo_Egr = 0
                            Else
                                'Descontamos el monto pagado al saldo de la DPO para ver cuantos Cxc puede pagar
                                Saldo_Egr = Saldo_Egr - (TotalAPagarPorDocto + Interes)
                            End If

                            Crea_Ing_sec(1, Indice_Egr, TotalAPagarPorDocto, Interes, Coll_Cxc_Seleccionados.Item(Indice_cxc))

                            If Saldo_Egr <= 0 Then Exit For

                            Ind_cxc += 1

                        Next
                    Else
                        Exit For
                    End If

                End While

                Ind_egr += 1

            Next

            Saldo_Interes = 0

            For Indice_Egr = Ind_egr To coll_egr_sec.Count

                'Asignamos el saldo del egreso

                If SaldoPorPagar > 0 Then
                    Saldo_Egr = SaldoPorPagar
                Else
                    Saldo_Egr = coll_egr_sec.Item(Indice_Egr).egr_mto
                End If

                coll_egr_sec.Item(Indice_Egr).id_egr_sec = Indice_Egr

                'Haga mientras quede saldo
                While Saldo_Egr > 0

                    'Documentos
                    If Ind_doc - 1 <> Coll_Doctos_Seleccionados.Count Then

                        For Indice_doc = Ind_doc To Coll_Doctos_Seleccionados.Count

                            FACTOR_CAMBIO_HOY = Coll_Doctos_Seleccionados.Item(Indice_doc).ope_fac_cam

                            Select Case Coll_Doctos_Seleccionados.Item(Indice_doc).id_p_0023
                                Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                                Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                                Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                                Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                            End Select

                            If Saldo_CxC_Docto > 0 Then
                                Monto = Saldo_CxC_Docto
                                Interes = 0
                            Else
                                Monto = Coll_Doctos_Seleccionados.Item(Indice_doc).MontoPagar * FACTOR_CAMBIO_HOY
                                Interes = Coll_Doctos_Seleccionados.Item(Indice_doc).Interes * FACTOR_CAMBIO_HOY
                                If Saldo_Interes > 0 Then
                                    Interes = Saldo_Interes
                                End If
                            End If

                            If (Monto + Interes) <= Saldo_Egr Then
                                TotalAPagarPorDocto = (Monto)
                            Else
                                If Interes > Saldo_Egr Then
                                    TotalAPagarPorDocto = 0
                                    Interes = Saldo_Egr
                                    Saldo_Interes = Interes - Saldo_Egr
                                    Monto = 0
                                Else
                                    TotalAPagarPorDocto = Saldo_Egr - Interes
                                End If
                            End If

                            TotalPago = Monto + Interes

                            If TotalAPagarPorDocto > TotalPago Then
                                Saldo_CxC_Docto = (TotalAPagarPorDocto + Interes) - TotalPago
                            Else
                                Saldo_CxC_Docto = TotalPago - (TotalAPagarPorDocto + Interes)
                            End If

                            SaldoPorPagar = Saldo_Egr - (TotalAPagarPorDocto + Interes)

                            If SaldoPorPagar <= 0 Then
                                Saldo_Egr = 0
                            Else
                                'Descontamos el monto pagado al saldo de la DPO para ver cuantos Cxc puede pagar
                                Saldo_Egr = Saldo_Egr - (TotalAPagarPorDocto + Interes)
                            End If

                            Crea_Ing_sec(2, Indice_Egr, TotalAPagarPorDocto, Interes, Coll_Doctos_Seleccionados.Item(Indice_doc))

                            If Saldo_Egr <= 0 Then Exit For

                            Ind_doc += 1

                        Next
                    Else
                        Exit For
                    End If

                End While

                Ind_egr += 1

            Next


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub Crea_Ing_sec(ByVal Tipo As Int16, ByVal Indice_Egr As Integer, ByVal Monto_Abonado As Double, ByVal Interes_Abonado As Double, ByVal Objeto As Object)


        Try

            Dim Formulas As New FormulasGenerales
            Dim SaldoPorPagar As Double = 0
            Dim MONTO As Double
            Dim INTERES As Double

            'recorremos cuantos Documentos se pueden pagar con un DPO

            Dim Ing_Sec As New ing_sec_cls
            Dim ABONO_CLIENTE, EXCEDENTE, MAYOR_PAGO, MONTO_MENOR, REAJUSTE As Double

            Ing_Sec.id_ing_sec = Nothing
            Ing_Sec.id_ing = Nothing
            Ing_Sec.id_egr_sec = Indice_Egr
            Ing_Sec.ing_qpa = Pagos.Pagador
            Ing_Sec.ing_vld_rcz = CChar("I")

            If Tipo = 1 Then

                ' 1.- CUENTAS POR COBRAR
                Ing_Sec.id_P_0053 = 1

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                'Toma el factor de cuando se ingreso la cuenta
                FACTOR_CAMBIO_HOY = Objeto.cxc_fac_cam

                Ing_Sec.id_cxc = Objeto.id_cxc
                Ing_Sec.doc_sdo_ddr = 0

                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli)

                MONTO_MENOR = Formulas.MIN((Ing_Sec.doc_sdo_cli / FACTOR_CAMBIO_HOY), (Ing_Sec.ing_mto_abo / FACTOR_CAMBIO_HOY))
                REAJUSTE = Formulas.MIN((ABONO_CLIENTE / FACTOR_CAMBIO_HOY), (MONTO_MENOR) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO))

                Ing_Sec.ing_pag_deu = CChar("N")

                MONTO = Monto_Abonado / FACTOR_CAMBIO_HOY
                INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                If IsNothing(Ing_Sec.doc_sdo_cli) Then
                    Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar)
                End If

                Ing_Sec.doc_sdo_cli = CDec(Ing_Sec.doc_sdo_cli - MONTO)

                'MONTO = CDbl(Objeto.MontoPagar) * FACTOR_CAMBIO_HOY
                'INTERES = CDbl(Objeto.Interes) * FACTOR_CAMBIO_HOY

            Else

                ' 2.- DOCUMENTOS
                Ing_Sec.id_P_0053 = 2
                Ing_Sec.id_doc = Objeto.id_doc
                FACTOR_CAMBIO_HOY = Objeto.ope_fac_cam

                Select Case Objeto.id_p_0023
                    Case 1 : FACTOR_CAMBIO_OBS_HOY = 1
                    Case 2 : FACTOR_CAMBIO_OBS_HOY = VALOR_UF
                    Case 3 : FACTOR_CAMBIO_OBS_HOY = Pagos.DollarObservador
                    Case 4 : FACTOR_CAMBIO_OBS_HOY = VALOR_EURO
                End Select

                '/*Calcula todo como Pago Deudor***************************************************/
                ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli + Objeto.dsi_mto - Objeto.dsi_mto_ant)
                EXCEDENTE = Formulas.MAX(Formulas.MIN(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli, Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)
                MAYOR_PAGO = Formulas.MAX(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli - (Objeto.dsi_mto - Objeto.dsi_mto_ant), 0)

                ''/*Calculo de Reajuste **************************************************/
                REAJUSTE = Formulas.MIN(ABONO_CLIENTE, Objeto.dsi_mto) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO)

                Ing_Sec.ing_pag_deu = CChar(Objeto.PagaDeudor)

                MONTO = Monto_Abonado / FACTOR_CAMBIO_HOY
                INTERES = Interes_Abonado / FACTOR_CAMBIO_HOY

                Select Case Ing_Sec.ing_qpa
                    Case "C"

                        If IsNothing(Ing_Sec.doc_sdo_cli) Then
                            Ing_Sec.doc_sdo_cli = CDec(Objeto.MontoPagar)
                        End If

                        Ing_Sec.doc_sdo_cli = CDec(Ing_Sec.doc_sdo_cli - MONTO)
                        Ing_Sec.doc_sdo_ddr = 0

                    Case "D"

                        If IsNothing(Ing_Sec.doc_sdo_ddr) Then
                            Ing_Sec.doc_sdo_ddr = CDec(Objeto.MontoPagar)
                        End If

                        Ing_Sec.doc_sdo_ddr = CDec(Objeto.MontoPagar - MONTO)
                        Ing_Sec.doc_sdo_cli = 0

                End Select

            End If



            Ing_Sec.ing_rea_mon = REAJUSTE

            'Ojo ver cuando el interes es negativo
            Ing_Sec.ing_mto_int = INTERES
            Ing_Sec.ing_mto_abo = MONTO
            Ing_Sec.ing_mto_tot = Ing_Sec.ing_mto_abo + Ing_Sec.ing_mto_int
            Ing_Sec.ing_fac_cam = FACTOR_CAMBIO_HOY 'Pagos.DollarCobranza
            Ing_Sec.ing_fac_cam_obs = FACTOR_CAMBIO_OBS_HOY 'Pagos.DollarObservador

            'If CDec(Ing_Sec.ing_mto_tot) >= (Ing_Sec.doc_sdo_cli) Then
            If Ing_Sec.doc_sdo_cli = 0 Then
                Ing_Sec.ing_tot_par = "T"
            Else
                Ing_Sec.ing_tot_par = "P"
            End If

            'Crea la collection con los objetos para que luego sean grabados
            Coll_Ing_Sec.Add(Ing_Sec)


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Function Valida_Condiciones() As Boolean
        Try

            Dim IDX As Integer
            Dim MARCA_EXC As Boolean

            MARCA_EXC = False

            Valida_Condiciones = True

            'For IDX = 0 To GV_Excedentes.Rows.Count - 1
            '    If CType(GV_Excedentes.Rows(IDX).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked = True Then
            '        MARCA_EXC = True
            '        Exit For
            '    End If
            'Next

            'If Not MARCA_EXC Then
            '    For IDX = 0 To GV_CxP.Rows.Count - 1
            '        If CType(GV_CxP.Rows(IDX).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked Then
            '            MARCA_EXC = True
            '            Exit For
            '        End If
            '    Next
            'End If

            'If Not MARCA_EXC Then
            '    For IDX = 0 To GV_DNC.Rows.Count - 1
            '        If CType(GV_DNC.Rows(IDX).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked Then
            '            MARCA_EXC = True
            '            Exit For
            '        End If
            '    Next
            'End If

            If Txt_Pag_Exc.Text = "0" And Txt_Pag_Cxp.Text = "0" And Txt_Pag_Dnc.Text = "0" Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar al menos una reserva, cuenta por pagar o doccumento no cedidos.", TipoDeMensaje._Informacion)
                Valida_Condiciones = False
                Exit Function
            End If

            'Si los Egresos Mayores que Los Ingresos
            'Debe Elegir Tipo Egreso

            If CDbl(Txt_Saldo.Text) < 0 Then
                If DP_TipoEgreso.SelectedIndex < 0 And Not CB_Sin_Devolucion.Checked Then
                    Msj.Mensaje(Me.Page, Caption, " Debe Elegir Tipo de Egreso o Sin Devolución ", TipoDeMensaje._Informacion)
                    Valida_Condiciones = False
                    Exit Function
                Else
                    If Not CB_Sin_Devolucion.Checked Then
                        If Txt_Observacion.Text.Trim = "" Then
                            Msj.Mensaje(Me.Page, Caption, " Está devolviendo, debe ingresar una observación ", TipoDeMensaje._Informacion)
                            Valida_Condiciones = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            'Si Tasas son Distintas Dígito Operaciones
            If (Txt_Tasa_Cliente.Text <> Txt_Tasa_Aplicar.Text) And Txt_Observacion.Text.Trim = "" Then
                Msj.Mensaje(Me.Page, Caption, "Existen diferencias entre tasas, debe dígitar observación", TipoDeMensaje._Informacion)
                Valida_Condiciones = False
                Exit Function
            End If

            'Si Aplica Doctos no Cedidos Agrego Alerta(observación)
            If Txt_Pag_Dnc.Text <> "0" Then
                Txt_Observacion.Text &= Chr(13) & " APLICO DOC. NO CED.¡ATENCION! NECESITA CARTA CLIENTE"
            End If

            ''Sin Devolución
            'If Not CB_Sin_Devolucion.Checked Then
            '    'Valido que Ingrese Cta Corriente
            '    If Trim(Txt_Cta_Cte.Text) = "" Then 'And TB_CTA_CTE.Tag = "S" Then '/*Cta Cte*/
            '        Msj.Mensaje(Me.Page, Caption, "Ingrese Cta. Cte.", TipoDeMensaje._Informacion)
            '        If Txt_Cta_Cte.Enabled Then Txt_Cta_Cte.Focus()
            '        Valida_Condiciones = False
            '        Exit Function
            '    End If
            'End If

            'Valida Total a Pagar
            If CDbl(Txt_Saldo.Text) > 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe modificar total a pagar en doctos. monto a aplicar insuficiente", TipoDeMensaje._Informacion)
                Valida_Condiciones = False
                Exit Function
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Function

    Public Function VALIDA_ABONO() As Boolean
        Try
            Dim IDX As Integer
            Dim TOTAL_A_PAGAR As Double
            Dim TOTAL_INTERES As Double

            VALIDA_ABONO = True

            TOTAL_A_PAGAR = 0
            TOTAL_INTERES = 0

            'Recorre Cuentas por Cobrar y valida que monto abono no sea menor que Interes
            For IDX = 1 To GV_CxC.Rows.Count - 1
                If CType(GV_CxC.Rows(IDX).FindControl("CB_Seleccionar_Cxc"), CheckBox).Checked Then
                    TOTAL_A_PAGAR = GV_CxC.Rows(IDX).Cells(6).Text
                    TOTAL_INTERES = GV_CxC.Rows(IDX).Cells(7).Text
                    If TOTAL_A_PAGAR < TOTAL_INTERES Then
                        'nro
                        Msj.Mensaje(Me.Page, Caption, "Para cuenta por cobrar n° " & GV_CxC.Rows(IDX).Cells(5).Text & " Total a pagar no debe ser menor a monto interes", TipoDeMensaje._Informacion)
                        VALIDA_ABONO = False
                        Exit For
                    End If
                End If
            Next

            'Recorre Documentos Vigentes y valida que monto abono no sea menor que Interes
            For IDX = 1 To Gr_Documentos.Rows.Count - 1
                If CType(Gr_Documentos.Rows(IDX).FindControl("CB_Seleccionar_Doc"), CheckBox).Checked Then
                    TOTAL_A_PAGAR = Gr_Documentos.Rows(IDX).Cells(6).Text
                    TOTAL_INTERES = Gr_Documentos.Rows(IDX).Cells(7).Text
                    If TOTAL_A_PAGAR < TOTAL_INTERES Then
                        'Nro y cuota
                        Msj.Mensaje(Me.Page, Caption, "Para docto. n° " & Gr_Documentos.Rows(IDX).Cells(6).Text & "-" & Gr_Documentos.Rows(IDX).Cells(6).Text & " Total a pagar no debe ser menor a monto interes", TipoDeMensaje._Informacion)
                        VALIDA_ABONO = False
                        Exit For
                    End If
                End If
            Next

            'Recorre Documentos Morosos y valida que monto abono no sea menor que Interes (TENEMOS LAS MISMA GRID)



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Function

    Public Function FormaPago(ByVal Estado As Boolean) As Boolean

        If Estado = True Then
            DP_TipoEgreso.Enabled = True
            DP_Banco.Enabled = True
            Txt_Observacion.ReadOnly = False
            DP_TipoEgreso.CssClass = "clsMandatorio"
            DP_Banco.CssClass = "clsMandatorio"
            Txt_Observacion.CssClass = "clsMandatorio"
        Else
            DP_TipoEgreso.Enabled = False
            DP_Banco.Enabled = False
            Txt_Observacion.ReadOnly = True
            DP_TipoEgreso.CssClass = "clsDisabled"
            DP_Banco.CssClass = "clsDisabled"
            Txt_Observacion.CssClass = "clsDisabled"
        End If

        Txt_Cta_Cte.ReadOnly = True
        Txt_Cta_Cte.CssClass = "clsDisabled"

    End Function

#End Region

#Region "EVENTOS Y FUNCTION DE CLIENTE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Response.Expires = -1
                Txt_Rut_Cli.Focus()
                CargaDrop()
                AlineaTextBoxDerecha()
                Txt_Fecha_Aplicacion.Text = Format(CDate(Date.Now), "dd/MM/yyyy")
                Coll_Doctos_Seleccionados = New Collection
                Coll_Cxc_Seleccionados = New Collection
            End If


            If Not IsNothing(Coll_Doctos_Seleccionados) And Coll_Doctos_Seleccionados.Count > 0 Then
                Gr_Documentos.DataSource = Coll_Doctos_Seleccionados
                Gr_Documentos.DataBind()
                AsignaSource()
            End If

            'Si seleciono al menos una cuenta por cobrar
            If Not IsNothing(Coll_Cxc_Seleccionados) And Coll_Cxc_Seleccionados.Count > 0 Then

                GV_CxC.DataSource = Coll_Cxc_Seleccionados
                GV_CxC.DataBind()

                Dim Monto As Double
                Dim Interes As Double
                'Dim TotalCxc As Double

                Txt_Pag_CxC.Text = 0

                For i = 1 To Coll_Cxc_Seleccionados.Count

                    Monto = (Coll_Cxc_Seleccionados.Item(i).MontoPagar * Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)
                    Interes = (Coll_Cxc_Seleccionados.Item(i).Interes * Coll_Cxc_Seleccionados.Item(i).cxc_fac_cam)
                    Txt_Pag_CxC.Text = Format(Txt_Pag_CxC.Text + Monto, "###,###,##0")


                    'If Me.HF_Interes_CxC.Value = "" Then
                    '    Me.HF_Interes_CxC.Value = 0
                    'End If

                    'Me.HF_Interes_CxC.Value = Val(Me.HF_Interes_CxC.Value) + Interes

                    'If Me.Txt_Tot_CxC.Text = "" Then
                    '    Me.Txt_Tot_CxC.Text = 0
                    'End If

                    'Me.Txt_Tot_CxC.Text = Format(CDbl(Me.Txt_Tot_CxC.Text) + Monto, Fmt.FCMSD)
                    '   TotalCancelarCxc = TotalCancelarCxc + (Monto + Interes)

                Next

                ' HF_SaldoCxC.Value = TotalCxc + TotalInteresCxc

                AsignaSourceCxC()

            End If

            IB_Aplicaciones.Attributes.Add("onClick", "WinOpen(2, 'BusquedaDeAplicaciones.aspx', 'BusquedaDeAplicaciones', 750, 620, 100, 100)")
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpDeudor',650,410,200,150);")
            Btn_Criterio.Attributes.Add("onClick", "var x=window.showModalDialog('../../Web_Controles/PaginaDePrueba.aspx?rut_deu=" & CInt(Val(Me.Txt_Rut_Deu.Text)) & " ', window, 'scroll:no;status:off;dialogWidth:1250;dialogHeight:650px;dialogLeft:50px;dialogTop:100px');")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub AsignaSourceCxC()
        Try

            Dim Formato As String = ""

            For I = 0 To GV_CxC.Rows.Count - 1

                Select Case Coll_Cxc_Seleccionados.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                GV_CxC.Rows(I).Cells(5).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text), Formato)
                GV_CxC.Rows(I).Cells(6).Text = Format(CDbl(GV_CxC.Rows(I).Cells(6).Text), Formato)
                GV_CxC.Rows(I).Cells(7).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text) + CDbl(GV_CxC.Rows(I).Cells(6).Text), Formato)
            Next
            Deuda_Total()
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub AsignaSource()
        Try
            HF_Interes_Doc.Value = 0
            Dim Pagos As New ClsSession.SesionPagos

            Select Case Pagos.Pagador
                Case "C"
                    Gr_Documentos.Columns(7).Visible = True
                    Gr_Documentos.Columns(8).Visible = False
                Case "D"
                    Gr_Documentos.Columns(7).Visible = False
                    Gr_Documentos.Columns(8).Visible = True
            End Select

            For I = 0 To Gr_Documentos.Rows.Count - 1

                If Coll_Doctos_Seleccionados.Item(I + 1).cli_idc.ToString().Split("-").ToArray().Count > 1 Then
                    Coll_Doctos_Seleccionados.Item(I + 1).cli_idc = Format(CLng(RC.LimpiaRut(Coll_Doctos_Seleccionados.Item(I + 1).cli_idc)), "000000000000")
                End If

                'Rut Deudor
                Gr_Documentos.Rows(I).Cells(0).Text = RC.FormatoMiles(CLng(RC.LimpiaRut(Gr_Documentos.Rows(I).Cells(0).Text))) & "-" & Coll_Doctos_Seleccionados(I + 1).deu_dig_ito

                If Pagos.Pagador = "C" Then
                    Gr_Documentos.Rows(I).Cells(7).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(7).Text))
                Else
                    Gr_Documentos.Rows(I).Cells(8).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(8).Text))
                End If

                Gr_Documentos.Rows(I).Cells(9).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(9).Text))
                Gr_Documentos.Rows(I).Cells(10).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(10).Text))
                'Gr_Documentos.Rows(I).Cells(12).Text = RC.FormatoMiles(CInt(Gr_Documentos.Rows(I).Cells(12).Text))
            Next

            'Variables Totales para Documentos
            Dim TotalCancelar As Double
            Dim TotalInteres As Double
            Dim TotalDoctos As Double
            Dim TotalNotaCre As Double
            Dim totalSeleccionado As Double

            Dim doc_sel As Boolean = False
            Dim cxc_sel As Boolean = False

            'Si seleciono al menos un documento 
            If Not IsNothing(Coll_Doctos_Seleccionados) Then
                
                doc_sel = True

                If Coll_Doctos_Seleccionados.Count > 0 Then

                    Dim Monto As Double
                    Dim Interes As Double

                    For I = 1 To Coll_Doctos_Seleccionados.Count

                        Monto = (Coll_Doctos_Seleccionados.Item(I).MontoPagar * Coll_Doctos_Seleccionados.Item(I).ope_fac_cam)
                        Interes = (Coll_Doctos_Seleccionados.Item(I).Interes * Coll_Doctos_Seleccionados.Item(I).ope_fac_cam)

                        TotalNotaCre = TotalNotaCre + Coll_Doctos_Seleccionados.Item(I).nota_cred

                        TotalInteres = TotalInteres + Interes

                        If Pagos.Pagador = "C" Then
                            TotalDoctos = TotalDoctos + Monto + (Interes * -1)
                        Else
                            TotalDoctos = TotalDoctos + Monto
                        End If

                        totalSeleccionado = totalSeleccionado + Monto

                        If Pagos.Pagador = "D" Then
                            TotalCancelar = TotalCancelar + Monto
                        Else
                            TotalCancelar = TotalCancelar + Monto + (Interes * -1)
                        End If


                    Next


                    HF_Interes_Doc.Value = Val(HF_Interes_Doc.Value) + TotalInteres
                    txt_total_doc.Text = Format(CDbl(TotalDoctos + TotalInteres), Fmt.FCMSD)

                    Deuda_Total()
                End If
            End If

            SumatoriaDocumentos()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        If Me.Txt_Rut_Cli.Text = "" Then
            Msj.Mensaje(Me, "Atención", "Debe ingresar el Identificación del Cliente a consultar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Function
        End If

        Try

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Function
            End If
            If IsNothing(CLI) Then
                Msj.Mensaje(Me.Page, Caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                Exit Function
            End If

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
            If Not IsNothing(CLI.eje_cls) Then
                Me.Txt_Ejecutivo.Text = CLI.eje_cls.eje_nom.Trim()
            End If

            HabilitaDesabilitaCliente(False)

            'Deja el Rut y Nombre del cliente en la cabecera 
            'LB_Cliente.text = RG.FormatoMiles(Txt_Rut_Cli.text) & "-" & Txt_Dig_Cli.text & "   " & Me.Txt_Cliente.text

            'Cambia a AcordionPanel de Negociaciones Anteriores
            'Accordion1.SelectedIndex = 1

            Return True

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
        End If


    End Sub

    Private Sub AlineaTextBoxDerecha()

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tasa_Cliente.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tasa_Aplicar.Attributes.Add("Style", "TEXT-ALIGN: right")

        txt_total_cxc.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_total_cxp.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_total_dnc.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_total_dnc.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_total_doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        txt_total_exc.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Tot_Exc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Cxp.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Dnc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_Exc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_Cxp.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_Dnc.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Tot_CxC.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Tot_Mor.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_CxC.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pag_Dnc.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Total_A_Pagar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Int_Devolver.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Interes.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Deuda_Total.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Saldo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Private Sub CargaDrop()

        Try

            CG.ParametrosDevuelve(TablaParametro.TipoEgreso, True, DP_TipoEgreso)
            'CG.BancosDevuelveTodos(DP_Banco)

            Dim Coll_Obj As Object
            Dim Coll_Est As New Collection

            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocto)

            Coll_Obj = CG.ParametrosDevuelve(TablaParametro.EstadoDocumento, False, Nothing)

            For Each O In Coll_Obj
                Select Case O.codigo
                    Case 1, 2, 4, 9, 11, 12
                        Coll_Est.Add(O)
                End Select
            Next

            DP_Estados.DataSource = Coll_Est
            DP_Estados.DataTextField = "descripcion"
            DP_Estados.DataValueField = "codigo"
            DP_Estados.DataBind()
            DP_Estados.ClearSelection()
            DP_Estados.Items.Insert(0, New ListItem("Seleccionar", 0))
            DP_Estados.Items.Insert(1, New ListItem("TODOS", 999))
            DP_Estados.Items.Item(1).Selected = True


            DP_CodCobranza.DataSource = CBR.CodigoCobranza_RetornaGestionar(1)

            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()
            DP_CodCobranza.ClearSelection()

            DP_CodCobranza.Items.Insert(0, New ListItem("Seleccionar", 0))
            DP_CodCobranza.Items.Item(0).Selected = True

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function DevuelveAplicacionCliente() As Object
        Try


            Dim RutCli_Desde As Long
            Dim RutCli_Hasta As Long
            Dim Fecha_Desde As String
            Dim Fecha_Hasta As String

            RutCli_Desde = Txt_Rut_Cli.Text
            RutCli_Hasta = Txt_Rut_Cli.Text

            Fecha_Desde = "01-01-1900"
            Fecha_Hasta = "01-01-3000"

            Dim Coll As Collection

            Coll = CG.Aplicacion_Devuelve(RutCli_Desde, RutCli_Hasta, Fecha_Desde, Fecha_Hasta, 1, "")

            If Coll.Count > 0 Then
                DevuelveAplicacionCliente = Coll.Item(1)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Function

    Private Sub HabilitaBotones(ByVal Estado As Boolean)

        IB_Buscar.Enabled = Estado
        IB_Guardar.Enabled = Not Estado
        'IB_SeleccionarDoctos.Enabled = Not Estado
        IB_Aplicaciones.Enabled = Not Estado
        Btn_Criterio.Enabled = Not Estado

    End Sub

    Private Sub Total_A_Pagar()


        Try

            Txt_Total_A_Pagar.Text = Format(CDbl(Txt_Pag_Exc.Text) + _
                                            CDbl(Txt_Pag_Cxp.Text) + _
                                            CDbl(Txt_Pag_Dnc.Text), Fmt.FCMSD)

            CalculoDeSaldo()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub Deuda_Total()
        Try
            If txt_total_doc.Text = "" Then
                txt_total_doc.Text = 0
            End If
            Txt_Deuda_Total.Text = Format(CDbl(Txt_Pag_CxC.Text) + CDbl(txt_total_doc.Text), Fmt.FCMSD)
            Txt_Interes.Text = Format(Val(HF_Interes_CxC.Value) + Val(HF_Interes_Doc.Value), Fmt.FCMSD)

            CalculoDeSaldo()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CalculoDeSaldo()

        Try

            Txt_Saldo.Text = Format(CDbl(Txt_Deuda_Total.Text) - CDbl(Txt_Total_A_Pagar.Text), Fmt.FCMSD)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaCampos(ByVal Estado As Boolean)

        CB_Sin_Devolucion.Enabled = Estado
        CB_Antes14.Enabled = Estado

        'DP_TipoEgreso.Enabled = Estado
        'DP_Banco.Enabled = Estado

        'Txt_Cta_Cte.ReadOnly = Not Estado
        'Txt_Observacion.ReadOnly = Not Estado


        If Estado Then

            'DP_TipoEgreso.CssClass = "clsMandatorio"
            'DP_Banco.CssClass = "clsMandatorio"

            'Txt_Cta_Cte.CssClass = "clsMandatorio"
            'Txt_Observacion.CssClass = "clsMandatorio"
        Else
            DP_TipoEgreso.CssClass = "clsDisabled"
            DP_Banco.CssClass = "clsDisabled"

            Txt_Cta_Cte.CssClass = "clsDisabled"
            Txt_Observacion.CssClass = "clsDisabled"

        End If

    End Sub

    Protected Sub CB_Sin_Devolucion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Sin_Devolucion.CheckedChanged

        If CB_Sin_Devolucion.Checked Then
            DP_TipoEgreso.Enabled = False
            DP_Banco.Enabled = False
            CB_Antes14.Enabled = False
            DP_TipoEgreso.CssClass = "clsDisabled"
            DP_Banco.CssClass = "clsDisabled"
        Else
            DP_TipoEgreso.Enabled = True
            DP_Banco.Enabled = True
            CB_Antes14.Enabled = False
            DP_TipoEgreso.CssClass = "clsMandatorio"
            DP_Banco.CssClass = "clsMandatorio"
        End If

    End Sub

    Private Sub Limpiar()

        Try

            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Sucursal.Text = ""
            Txt_Ejecutivo.Text = ""
            Txt_Tasa_Cliente.Text = ""
            Txt_Tasa_Cliente.Text = ""
            Txt_Fecha_Aplicacion.Text = Format(CDate(Date.Now), "dd/MM/yyyy")

            Txt_Tot_Exc.Text = 0
            Txt_Tot_Cxp.Text = 0
            Txt_Tot_Dnc.Text = 0
            Txt_Tot_CxC.Text = 0
            Txt_Tot_Doc.Text = 0

            Txt_Pag_CxC.Text = 0
            Txt_Pag_Doc.Text = 0
            Txt_Pag_Exc.Text = 0
            Txt_Pag_Cxp.Text = 0
            Txt_Pag_Dnc.Text = 0
            Txt_Pag_CxC.Text = 0
            Txt_Pag_Doc.Text = 0

            txt_total_cxc.Text = 0
            txt_total_cxp.Text = 0
            txt_total_dnc.Text = 0
            txt_total_dnc.Text = 0
            txt_total_doc.Text = 0
            txt_total_exc.Text = 0

            Txt_Total_A_Pagar.Text = 0
            Txt_Int_Devolver.Text = 0
            Txt_Interes.Text = 0
            Txt_Deuda_Total.Text = 0
            Txt_Saldo.Text = 0

            CB_Sin_Devolucion.Checked = False
            CB_Antes14.Checked = False

            DP_TipoEgreso.ClearSelection()
            DP_Banco.ClearSelection()
            Txt_Cta_Cte.Text = ""
            Txt_Observacion.Text = ""

            GV_Excedentes.DataSource = Nothing
            GV_Excedentes.DataBind()

            GV_CxP.DataSource = Nothing
            GV_CxP.DataBind()

            GV_DNC.DataSource = Nothing
            GV_DNC.DataBind()

            GV_CxC.DataSource = Nothing
            GV_CxC.DataBind()

            Gr_Documentos.DataSource = Nothing
            Gr_Documentos.DataBind()
            IB_AyudaCli.Enabled = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Txt_Rut_Deu_MaskedEditExtender.Enabled = True


            'Controles criterio de busqueda

            CB_Deudor.Checked = False
            Txt_Rut_Deu.Text = ""
            Txt_Rut_Deu.CssClass = "clsDisabled"

            Txt_Dig_Deu.Text = ""
            Txt_Dig_Deu.CssClass = "clsDisabled"
            Txt_Rso_Deu.Text = ""
            DP_Estados.ClearSelection()
            DP_TipoDocto.ClearSelection()
            Txt_Nro_Oto.Text = ""
            Txt_Nro_Doc.Text = ""
            DP_CodCobranza.ClearSelection()
            Txt_Mto_Dsd.Text = ""
            Txt_Mto_Hst.Text = ""
            Txt_Fec_Dsd.Text = ""
            Txt_Fec_Hst.Text = ""
            IB_AyudaDeu.Enabled = False

            DP_Estados.ClearSelection()
            DP_Estados.Items.Insert(0, New ListItem("Seleccionar", 0))
            DP_Estados.Items.Insert(1, New ListItem("TODOS", 999))
            DP_Estados.Items.Item(1).Selected = True
            Coll_Doctos_Seleccionados = New Collection
            Coll_Cxc_Seleccionados = New Collection
            Txt_Tasa_Aplicar.Text = ""
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            Dim FG As New FuncionesGenerales.FComunes

            If Txt_Dig_Deu.Text <> "" And Txt_Rut_Deu.Text <> "" Then

                Dim deu As deu_cls

                deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

                If Txt_Dig_Deu.Text.ToUpper <> deu.deu_dig_ito.ToString.ToUpper Then
                    Msj.Mensaje(Me.Page, Caption, "Dígito del rut no válido", ClsMensaje.TipoDeMensaje._Excepcion)
                    MP_Criterio.Show()
                    Exit Sub
                End If

                If Not IsNothing(deu) Then
                    If deu.id_P_0044 <> 1 Then
                        Txt_Rso_Deu.Text = deu.deu_rso
                    Else
                        Txt_Rso_Deu.Text = deu.deu_rso & " " & deu.deu_ape_ptn & " " & deu.deu_ape_mtn
                    End If
                Else
                    Msj.Mensaje(Page, Caption, "Deudor no existe", ClsMensaje.TipoDeMensaje._Excepcion)
                    Exit Sub
                End If

            End If

            Txt_Rut_Deu.Text = Format(CDbl(Txt_Rut_Deu.Text), Fmt.FCMSD)

            MP_Criterio.Show()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DP_TipoEgreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoEgreso.SelectedIndexChanged

        Dim par As Collection

        par = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, DP_TipoEgreso.SelectedValue)

        If par.Count > 0 Then

            If par.Item(1).pnu_atr_003 = "S" Then
                Me.DP_Banco.CssClass = "clsMandatorio"
                Me.DP_Banco.Enabled = True
            Else
                Me.DP_Banco.CssClass = "clsDisabled"
                DP_Banco.Enabled = False
            End If

            Me.DP_Banco.ClearSelection()
            Me.Txt_Cta_Cte.CssClass = "clsDisabled"
            Me.Txt_Cta_Cte.Text = ""
            Me.Txt_Cta_Cte.ReadOnly = True

        End If

    End Sub

    Protected Sub DP_Banco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Banco.SelectedIndexChanged
        'Trae el numero de cta. cte. del banco del cliente
        If DP_Banco.SelectedIndex > 0 Then
            Txt_Cta_Cte.Text = CL.BancosDevuelvePorClienteYBanco(CLng(Txt_Rut_Cli.Text), DP_Banco.SelectedValue).nbc_cct
        Else
            Txt_Cta_Cte.Text = ""
        End If
    End Sub

    Protected Sub Lb_Banco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_Banco.Click
        Txt_Cta_Cte.Text = CL.BancosDevuelvePorClienteYBanco(CLng(Txt_Rut_Cli.Text), DP_Banco.SelectedValue).nbc_cct
    End Sub


#End Region

#Region "Excedentes"

    Protected Sub CB_Seleccionar_Exc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            'Dim Mto As TextBox
            Dim swExc As Integer = 0
            'Mto = GV_Excedentes.Rows(HF_Posicion_Exc.Value).FindControl("Txt_MtoPagar_Exc")

            If CType(GV_Excedentes.Rows(HF_Posicion_Exc.Value).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then
                swExc = 1
            End If

            For i = 0 To GV_CxP.Rows.Count - 1
                If CType(GV_CxP.Rows(i).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked = True Then
                    swExc = 1
                    Exit For
                End If
            Next

            For i = 0 To GV_DNC.Rows.Count - 1
                If CType(GV_DNC.Rows(i).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked = True Then
                    swExc = 1
                    Exit For
                End If
            Next


            SumatoriaExcedentes()

            If swExc = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_MtoPagar_Exc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim formato As String
            Dim Txt_Mto As TextBox

            Select Case Coll_EXC.Item(HF_Posicion_Exc.Value + 1).id_p_0023
                Case 1 : formato = Fmt.FCMSD
                Case 2, 4 : formato = Fmt.FCMCD4
                Case 3 : formato = Fmt.FCMCD
            End Select

            'Txt_Mto = CType(GV_Excedentes.Rows(HF_Posicion_Exc.Value).FindControl("Txt_MtoPagar_Exc"), TextBox)

            Txt_Mto.Text = Format(Coll_EXC.Item(HF_Posicion_Exc.Value + 1).excedente, formato)

            SumatoriaExcedentes()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Excedentes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Excedentes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaExc(" & Posicion & ");")
            Posicion = Posicion + 1
        End If
    End Sub

    Private Sub CargaExcedentes()

        Try
            Dim Suma As Double = 0

            Coll_EXC = New Collection

            Coll_EXC = CG.Excedentes_Devuelve(Txt_Rut_Cli.Text, _
                                              0, _
                                              If(RB_Excedentes.SelectedValue = 1, True, False))

            For I = 0 To GV_Excedentes.Rows.Count - 1

                Dim Txt_Mto As TextBox
                Suma += CG.RETORNA_VALOR_MONEDA(CDbl(Coll_EXC.Item(I + 1).excedente), Coll_EXC.Item(I + 1).id_P_0023, 1, Txt_Fecha_Aplicacion.Text)

            Next

            GV_Excedentes.DataSource = Coll_EXC
            GV_Excedentes.DataBind()

            FormatoExc()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoExc()
        Try


            For I = 0 To GV_Excedentes.Rows.Count - 1

                Dim formato As String

                Select Case Coll_EXC.Item(I + 1).id_p_0023
                    Case 1 : formato = Fmt.FCMSD
                    Case 2, 4 : formato = Fmt.FCMCD4
                    Case 3 : formato = Fmt.FCMCD
                End Select


                GV_Excedentes.Rows(I).Cells(1).Text = Format(CDbl(GV_Excedentes.Rows(I).Cells(1).Text), Fmt.FCMSD) & "-" & Coll_EXC.Item(I + 1).deu_dig_ito

                GV_Excedentes.Rows(I).Cells(8).Text = Format(Coll_EXC.Item(I + 1).excedente, formato)

                'Excedentes por liberar
                If Coll_EXC.Item(I + 1).ingresos = 1 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                    GV_Excedentes.Rows(I).BackColor = col
                    CType(GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc"), CheckBox).Enabled = False 'jlagos 13-06-2012
                End If

                'Aplic ctas y exc.
                If Coll_EXC.Item(I + 1).Aplicacion > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CC99FF")
                    GV_Excedentes.Rows(I).BackColor = col
                    CType(GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc"), CheckBox).Enabled = False 'jlagos 13-06-2012
                End If

                'pagos sin procesar
                If Coll_EXC.Item(I + 1).Cantidad_Egresos > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                    GV_Excedentes.Rows(I).BackColor = col
                    CType(GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc"), CheckBox).Enabled = False 'jlagos 13-06-2012
                End If

            Next
            txt_total_exc.Text = Txt_Tot_Exc.Text
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub SumatoriaExcedentes()
        Try

            Dim Suma As Double = 0

            For I = 0 To GV_Excedentes.Rows.Count - 1

                If CType(GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then

                    'Dim Txt_Mto As TextBox
                    'Txt_Mto = CType(GV_Excedentes.Rows(I).FindControl("Txt_MtoPagar_Exc"), TextBox)

                    Suma += CG.RETORNA_VALOR_MONEDA(CDbl(Coll_EXC.Item(I + 1).excedente), Coll_EXC.Item(I + 1).id_P_0023, 1, Txt_Fecha_Aplicacion.Text)
                End If

            Next

            Txt_Pag_Exc.Text = Format(Suma, Fmt.FCMSD)
            Total_A_Pagar()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaCollection_Excedentes()

        Try

            'Instanciamos la collection en caso de tenga datos anteriores

            For I = 0 To GV_Excedentes.Rows.Count - 1

                If CType(GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then

                    Dim lbl As Label
                    lbl = GV_Excedentes.Rows(I).FindControl("lbl_id_doc")

                    Dim Egreso As New egr_sec_cls

                    With Egreso

                        .id_doc = lbl.Text 'GV_Excedentes.Rows(I).Cells(10).Text
                        .id_egr = Nothing  'Nro Egreso Cabecera, se le asigna una vez que se ingresa
                        .id_egr_sec = Nothing  'Nro Egreso Secuencial
                        .egr_mto = CDbl(GV_Excedentes.Rows(I).Cells(8).Text)  'Monto egreso se guardar en pesos
                        .id_P_0055 = 3 'Que se va a Pagar 3 = Excedente
                        .id_P_0053 = Nothing 'Que se paga, se ve cuando se asocie a una cxc o documento
                        .egr_doc_nce = "N"
                        'Tipo de Egreso, Con o sin Devolucion

                        If CB_Sin_Devolucion.Checked Then
                            .id_P_0056 = 5
                        Else
                            .id_P_0056 = DP_TipoEgreso.SelectedValue

                            If DP_Banco.SelectedValue = 0 Then

                                .id_bco = Nothing

                            Else
                                .id_bco = DP_Banco.SelectedValue.Trim()
                            End If
                            .egr_cta_cte = Txt_Cta_Cte.Text.Trim
                            .egr_dep_ant = If(CB_Antes14.Checked = True, "S", "N")
                        End If


                        'Estados de validacion
                        .egr_pro = "N"
                        .egr_vld_rcz = "I"
                        .egr_ent = "N"

                    End With

                    coll_egr_sec.Add(Egreso)

                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_ExcTodos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim SwExc As Integer = 0
            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
            
            For I = 0 To GV_Excedentes.Rows.Count - 1
                If GV_Excedentes.Rows(I).BackColor <> col Then
                    Dim Check As CheckBox
                    Check = GV_Excedentes.Rows(I).FindControl("CB_Seleccionar_Exc")
                    Check.Checked = True
                    SwExc = 1
                End If
            Next

            If SwExc = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If

            SumatoriaExcedentes()
            'UP_Totales.Update()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Cuentas Por Pagar"

    Private Sub FormatoGridCxP()

        For I = 0 To GV_CxP.Rows.Count - 1

            Try

                Dim Formato As String = ""
                Dim Mto As Double
                Dim cb As CheckBox

                cb = Me.GV_CxP.Rows(I).FindControl("CB_Seleccionar_CxP")

                Mto = CDbl(GV_CxP.Rows(I).Cells(5).Text)

                GV_CxP.Rows(I).Cells(5).Text = Format(Mto, Formato)

                For x = 1 To Coll_CXP.Count

                    If Coll_CXP(x).id_cxp = GV_CxP.Rows(I).Cells(2).Text Then

                        Select Case Coll_CXP.Item(x).id_p_0023
                            Case 1 : Formato = Fmt.FCMSD
                            Case 2, 4 : Formato = Fmt.FCMCD4
                            Case 3 : Formato = Fmt.FCMCD
                        End Select

                        'Aplic ctas y exc.
                        If Coll_CXP.Item(x).Aplicacion > 0 Then
                            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CC99FF")
                            GV_CxP.Rows(I).BackColor = col
                            cb.Enabled = False
                        End If

                        'pagos sin procesar
                        If Coll_CXP.Item(x).Cantidad_Egresos > 0 Then
                            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                            GV_CxP.Rows(I).BackColor = col
                            cb.Enabled = False
                        End If

                        'Marca si fue seleccionado antes
                        If Coll_CXP.Item(x).Seleccion > 0 Then
                            cb.Checked = True
                        End If

                        Exit For

                    End If

                Next

            Catch ex As Exception

            End Try

        Next

        Dim SumatoriaCxP As Double = 0

        For I = 0 To GV_CxP.Rows.Count - 1

            Dim Paridad As Double
            Dim Mto As Double

            Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(Coll_CXP.Item(I + 1).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

            If Paridad > 0 Then
                Mto = GV_CxP.Rows(I).Cells(5).Text
                SumatoriaCxP += Mto * Paridad
            End If

        Next

        txt_total_cxp.Text = Txt_Tot_Cxp.Text

    End Sub

    Private Sub SumatoriaCuentasPorPagar()

        Try

            Dim SumatoriaCxP As Double = 0
            Dim Paridad As Double
            Dim Mto As Double

            For I = 1 To Coll_CXP.Count

                If Coll_CXP(I).Seleccion = 1 Then

                    Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(Coll_CXP.Item(I).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

                    If Paridad > 0 Then
                        Mto = Coll_CXP.Item(I).cxp_mto
                        SumatoriaCxP += Mto * Paridad
                    End If

                End If

            Next

            Txt_Pag_Cxp.Text = Format(SumatoriaCxP, Fmt.FCMSD)

            Total_A_Pagar()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaCollection_CuentasPorPagar()

        Try

            'Instanciamos la collection en caso de tenga datos anteriores

            For I = 0 To GV_CxP.Rows.Count - 1

                If CType(GV_CxP.Rows(I).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked Then

                    Dim Egreso As New egr_sec_cls

                    With Egreso

                        .id_egr = Nothing  'Nro Egreso Cabecera, se le asigna una vez que se ingresa
                        .id_egr_sec = Nothing  'Nro Egreso Secuencial
                        .id_cxp = GV_CxP.Rows(I).Cells(2).Text 'Monto egreso se guardar en pesos
                        .egr_mto = GV_CxP.Rows(I).Cells(5).Text 'Monto egreso se guardar en pesos
                        .id_P_0055 = 1 'Que se va a Pagar 3 = Cuentas Por Pagar
                        .id_P_0053 = Nothing 'Que se paga, se ve cuando se asocie a una cxc o documento
                        .egr_doc_nce = "N"
                        '-----------------------------------------------------------------------------------------------------------------------------------
                        'Tipo de Egreso, Con o sin Devolucion
                        '-----------------------------------------------------------------------------------------------------------------------------------
                        If CB_Sin_Devolucion.Checked Then
                            .id_P_0056 = 5
                        Else
                            .id_P_0056 = DP_TipoEgreso.SelectedValue
                            If DP_Banco.SelectedValue = 0 Then

                                .id_bco = Nothing

                            Else

                                .id_bco = DP_Banco.SelectedValue

                            End If

                            .egr_cta_cte = Txt_Cta_Cte.Text.Trim
                            .egr_dep_ant = If(CB_Antes14.Checked = True, "S", "N")
                        End If


                        'Estados de validacion
                        .egr_pro = "N"
                        .egr_vld_rcz = "I"
                        .egr_ent = "N"

                    End With

                    coll_egr_sec.Add(Egreso)

                End If

            Next



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_CxP_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_CxP.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaCxp(" & Posicion_CxP & ");")
            Posicion_CxP = Posicion_CxP + 1

            'Dim chkBxSelect As CheckBox = e.Row.Cells(0).FindControl("chkBxSelect")
            'Dim chkBxHeader As CheckBox = GV_CxP.HeaderRow.FindControl("chkBxHeader")
            'Dim hdnFldId As HiddenField = e.Row.Cells(0).FindControl("hdnFldId")
            'chkBxSelect.Attributes("onclick") = String.Format("javascript:ChildClick(this,document.getElementById('{0}'),'{1}');", chkBxHeader.ClientID, hdnFldId.Value.Trim())
        End If

        
   
    End Sub

    Protected Sub GV_CxP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_CxP.PageIndexChanging

        GV_CxP.PageIndex = e.NewPageIndex

        GV_CxP.DataSource = CTA.CuentasPorPagarDevuelve(Txt_Rut_Cli.Text, 1, Val(HF_NroAplicacion.Value))
        GV_CxP.DataBind()

        FormatoGridCxP()

    End Sub

    Protected Sub CB_Seleccionar_CxP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim swcxp As Integer = 0

            If CType(GV_CxP.Rows(HF_Posicion_CxP.Value).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked = True Then
                For x = 1 To Coll_CXP.Count
                    If Coll_CXP(x).id_cxp = GV_CxP.Rows(HF_Posicion_CxP.Value).Cells(2).Text Then
                        Coll_CXP(x).Seleccion = 1
                        swcxp = 1
                        Exit For
                    End If
                Next
            End If


            'For i = 0 To GV_Excedentes.Rows.Count - 1
            '    If CType(GV_Excedentes.Rows(i).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then
            '        swcxp = 1
            '    End If
            'Next

            'For i = 0 To GV_DNC.Rows.Count - 1
            '    If CType(GV_DNC.Rows(i).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked = True Then
            '        swcxp = 1
            '        Exit For
            '    End If
            'Next

            SumatoriaCuentasPorPagar()

            If swcxp = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Seleccion_CxP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim swcxp As Integer = 0
            Dim Check As CheckBox

            For I = 0 To GV_CxP.Rows.Count - 1
                For x = 1 To Coll_CXP.Count
                    If Coll_CXP(x).id_cxp = GV_CxP.Rows(I).Cells(2).Text Then
                        Check = GV_CxP.Rows(I).FindControl("CB_Seleccionar_CxP")
                        Check.Checked = True
                        Coll_CXP(x).Seleccion = 1
                        swcxp = 1
                        Exit For
                    End If
                Next
            Next


            'For i = 0 To GV_Excedentes.Rows.Count - 1
            '    If CType(GV_Excedentes.Rows(i).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then
            '        swcxp = 1
            '    End If
            'Next

            'For i = 0 To GV_DNC.Rows.Count - 1
            '    If CType(GV_DNC.Rows(i).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked = True Then
            '        swcxp = 1
            '        Exit For
            '    End If
            'Next


            If swcxp = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If

            SumatoriaCuentasPorPagar()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Documentos No Cedidos"

    Private Sub FormatoDNC(ByVal Coll As Collection)
        Try

            For I = 0 To GV_DNC.Rows.Count - 1
                Dim cb As CheckBox
                cb = GV_DNC.Rows(I).FindControl("CB_Seleccionar_DNC")

                Dim formato As String

                Select Case Coll.Item(I + 1).id_p_0023
                    Case 1 : formato = Fmt.FCMSD
                    Case 2, 4 : formato = Fmt.FCMCD4
                    Case 3 : formato = Fmt.FCMCD
                End Select

                GV_DNC.Rows(I).Cells(1).Text = Format(CDbl(GV_DNC.Rows(I).Cells(1).Text), Fmt.FCMSD) & "-" & Coll.Item(I + 1).deu_dig_ito
                GV_DNC.Rows(I).Cells(6).Text = Format(CDbl(GV_DNC.Rows(I).Cells(6).Text), formato)

                'Aplic ctas y exc.
                If Coll.Item(I + 1).Aplicacion > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CC99FF")
                    GV_DNC.Rows(I).BackColor = col
                    cb.Enabled = False
                End If

                'pagos sin procesar
                If Coll.Item(I + 1).Cantidad_Egresos > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                    GV_DNC.Rows(I).BackColor = col
                    cb.Enabled = False
                End If

            Next
            txt_total_dnc.Text = Txt_Tot_Dnc.Text
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub SumatoriaDoctosNoCedidos()
        Try

            Dim Suma As Double = 0

            For I = 0 To GV_DNC.Rows.Count - 1

                If CType(GV_DNC.Rows(I).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked Then
                    Suma += CG.RETORNA_VALOR_MONEDA(CDbl(GV_DNC.Rows(I).Cells(6).Text), coll_DNC.Item(I + 1).id_P_0023, 1, Txt_Fecha_Aplicacion.Text)
                End If

            Next

            Txt_Pag_Dnc.Text = Format(Suma, Fmt.FCMSD)
            Total_A_Pagar()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Seleccion_Dnc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Seleccion_Dnc.Click
        Try
            Dim swdnc As Integer = 0
            For I = 0 To GV_DNC.Rows.Count - 1

                Dim Check As CheckBox

                Check = GV_DNC.Rows(I).FindControl("CB_Seleccionar_DNC")
                Check.Checked = True
                swdnc = 1
            Next

            For i = 0 To GV_CxP.Rows.Count - 1
                If CType(GV_CxP.Rows(i).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked = True Then
                    swdnc = 1
                    Exit For
                End If
            Next

            For i = 0 To GV_Excedentes.Rows.Count - 1
                If CType(GV_Excedentes.Rows(i).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then
                    swdnc = 1
                    Exit For
                End If
            Next


            SumatoriaDoctosNoCedidos()

            If swdnc = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub CB_Seleccionar_DNC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim swDnc As Integer = 0

            For i = 0 To GV_DNC.Rows.Count - 1
                If CType(GV_DNC.Rows(i).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked = True Then
                    swDnc = 1
                    Exit For
                End If
            Next

            For i = 0 To GV_CxP.Rows.Count - 1
                If CType(GV_CxP.Rows(i).FindControl("CB_Seleccionar_CxP"), CheckBox).Checked = True Then
                    swDnc = 1
                    Exit For
                End If
            Next

            For i = 0 To GV_Excedentes.Rows.Count - 1
                If CType(GV_Excedentes.Rows(i).FindControl("CB_Seleccionar_Exc"), CheckBox).Checked Then
                    swDnc = 1
                    Exit For
                End If
            Next

            SumatoriaDoctosNoCedidos()
            If swDnc = 1 Then
                FormaPago(True)
            Else
                FormaPago(False)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaCollection_DoctosNoCedidos()

        Try

            'Instanciamos la collection en caso de tenga datos anteriores

            For I = 0 To GV_DNC.Rows.Count - 1

                If CType(GV_DNC.Rows(I).FindControl("CB_Seleccionar_DNC"), CheckBox).Checked Then
                    Dim lbl As Label
                    lbl = GV_DNC.Rows(I).FindControl("lb_id_nce")
                    Dim Egreso As New egr_sec_cls

                    With Egreso
                        .id_egr = Nothing  'Nro Egreso Cabecera, se le asigna una vez que se ingresa
                        .id_egr_sec = Nothing  'Nro Egreso Secuencial
                        .id_nce = lbl.Text.Trim 'Monto egreso se guardar en pesos
                        .egr_mto = GV_DNC.Rows(I).Cells(6).Text 'Monto egreso se guardar en pesos
                        .id_P_0055 = 2 'Que se va a Pagar 2 = Documentos No Cedidos
                        .id_P_0053 = Nothing 'Que se paga, se ve cuando se asocie a una cxc o documento
                        .egr_doc_nce = "S"

                        If CB_Sin_Devolucion.Checked Then
                            .id_P_0056 = 5
                        Else
                            .id_P_0056 = DP_TipoEgreso.SelectedValue
                            If DP_Banco.SelectedValue = 0 Then

                                .id_bco = Nothing

                            Else
                                .id_bco = DP_Banco.SelectedValue
                            End If

                            .egr_cta_cte = Txt_Cta_Cte.Text.Trim
                            .egr_dep_ant = If(CB_Antes14.Checked = True, "S", "N")
                        End If


                        'Estados de validacion
                        .egr_pro = "N"
                        .egr_vld_rcz = "I"
                        .egr_ent = "N"

                    End With

                    coll_egr_sec.Add(Egreso)

                End If

            Next



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub GV_DNC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_DNC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaDnc(" & Posicion_DNC & ");")
            Posicion_DNC = Posicion_DNC + 1
        End If
    End Sub
#End Region

#Region "Cuentas Por Cobrar"

    Protected Sub GV_CxC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_CxC.RowDataBound
    End Sub

    Private Sub CargaGrillaCxC()
        Try
            Dim Pagos As New ClsSession.SesionPagos
            Dim Coll_CxC_Temp As New Collection

            coll_CXC = New Collection

            'CLIENTE, OPERACION Y DOCUMENTOS
            coll_CXC = CTA.CuentasPorCobrarDevuelve(Txt_Rut_Cli.Text, 1, 99, 1, 1, 1, 2, Val(HF_NroAplicacion.Value))

            GV_CxC.DataSource = coll_CXC
            GV_CxC.DataBind()

            If GV_CxC.Rows.Count > 0 Then
                FormatoGridCxC()
            End If

            Dim Coll_Ing As Collection

            Coll_Ing = CG.IngresosDevuelve(Txt_Rut_Cli.Text, 0, 1, 1, "01/01/1900", "01/01/3000")

            For Pos_Ing = 1 To Coll_Ing.Count

                For Pos_cxc = 1 To coll_CXC.Count

                    If Coll_Ing.Item(Pos_Ing).id_cxc = coll_CXC.Item(Pos_cxc).id_cxc And Coll_Ing.Item(Pos_Ing).ing_pro = "N" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                        GV_CxC.Rows(Pos_cxc - 1).BackColor = col
                        Exit For
                    End If

                Next

            Next

            Dim SumatoriaCxC As Double = 0
            Dim SumatoriaCxC_Int As Double = 0

            For I = 0 To GV_CxC.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox
                Dim Paridad As Double

                Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(coll_CXC.Item(I + 1).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

                If Paridad > 0 Then

                    'Mto_A_Pagar = CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_CxC"), TextBox)
                    'SumatoriaCxC += CDbl(Mto_A_Pagar.Text) * Paridad
                    'SumatoriaCxC_Int += CDbl(GV_CxC.Rows(I).Cells(7).Text) * Paridad

                End If
            Next

            'txt_total_cxc.Text = Format(SumatoriaCxC, Fmt.FCMSD)
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoGridCxC()

        For I = 0 To GV_CxC.Rows.Count - 1

            Try

                Dim Formato As String = ""
                Dim Mto As Double
                Dim Int As Double

                Select Case coll_CXC.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                Mto = CDbl(GV_CxC.Rows(I).Cells(6).Text)

                If GV_CxC.Rows(I).Cells(7).Text = "" Or GV_CxC.Rows(I).Cells(7).Text = "0" Then
                    Int = 0
                Else
                    Int = CDbl(GV_CxC.Rows(I).Cells(7).Text)
                End If


                GV_CxC.Rows(I).Cells(6).Text = Format(Mto, Formato)
                GV_CxC.Rows(I).Cells(7).Text = Format(Int, Formato)

                'Aplic ctas y exc.
                If coll_CXC.Item(I + 1).Aplicacion > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CC99FF")
                    GV_CxC.Rows(I).BackColor = col
                End If

                'pagos sin procesar
                If coll_CXC.Item(I + 1).Cantidad_Ingresos > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                    GV_CxC.Rows(I).BackColor = col
                End If

                txt_total_cxc.Text = Format(CDbl(Txt_Tot_CxC.Text), Formato)

            Catch ex As Exception
                Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
            End Try

        Next

    End Sub

    Private Sub CalculaInteresCxC(ByVal Pos As Integer)

        Try

            Dim Mto_A_Pagar As TextBox
            Dim CB As CheckBox
            Dim Saldo As Double
            Dim Interes As Double
            Dim DIF_DIA As Integer
            Dim Formato As String = ""

            'CB = CType(GV_CxC.Rows(Pos).FindControl("CB_Seleccionar_CxC"), CheckBox)

            ''Validamos que tenga factor de cambio del dia de la cuenta, sino no lo deja agregar

            'If coll_CXC.Item(Pos + 1).cxc_fac_cam <= 0 And CB.Checked Then
            '    Msj.Mensaje(Me.Page, "Cuentas Por Cobrar", "No se puede agregar esta CXC por no tener factor de cambio del dia de la cuenta", TipoDeMensaje._Exclamacion)
            '    CB.Checked = False
            '    Exit Sub
            'End If

            'Mto_A_Pagar = CType(GV_CxC.Rows(Pos).FindControl("Txt_MtoPagar_CxC"), TextBox)

            Saldo = GV_CxC.Rows(Pos).Cells(6).Text
            Interes = GV_CxC.Rows(Pos).Cells(7).Text

            Select Case coll_CXC.Item(Pos + 1).id_p_0023
                Case 1 : Formato = Fmt.FCMSD
                Case 2, 4 : Formato = Fmt.FCMCD4
                Case 3 : Formato = Fmt.FCMCD
            End Select

            If coll_CXC.Item(Pos + 1).pnu_atr_005 = "S" Then
                DIF_DIA = DateDiff("d", CDate(GV_CxC.Rows(Val(Pos)).Cells(9).Text), Txt_Fecha_Aplicacion.Text)
                Interes = (Saldo / 30) * DIF_DIA * (Txt_Tasa_Aplicar.Text / 100)
                Me.GV_CxC.Rows(Pos).Cells(7).Text = Format(Interes, Formato)
            End If

            If CB.Checked Then
                Mto_A_Pagar.CssClass = "clsMandatorio"
                Mto_A_Pagar.ReadOnly = False
                Mto_A_Pagar.Text = Format(Saldo + Interes, Formato)

            Else

                Mto_A_Pagar.Text = 0
                Mto_A_Pagar.CssClass = "clsDisabled"
                Mto_A_Pagar.ReadOnly = True
                'GV_CxC.Rows(Val(Pos)).Cells(7).Text = 0

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Documentos Vigentes, Morosos y con Problemas"

    Protected Sub Gr_Documentos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Gr_Documentos.Sorting

        If e.SortExpression = "Todos" Then

            For I = 0 To Gr_Documentos.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox
                Dim CB As CheckBox
                Dim TD As CheckBox

                CB = CType(Gr_Documentos.Rows(I).FindControl("CB_Seleccionar"), CheckBox)
                Mto_A_Pagar = CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox)
                TD = CType(Gr_Documentos.Rows(I).FindControl("CB_PD"), CheckBox)

                If CB.Checked = False Then

                    CB.Checked = True
                    Mto_A_Pagar.CssClass = "clsMandatorio"
                    Mto_A_Pagar.ReadOnly = False
                    TD.Checked = True
                    CalculaInteresDoctos(I)

                Else

                    CB.Checked = False
                    Mto_A_Pagar.Text = 0
                    Mto_A_Pagar.CssClass = "clsDisabled"
                    Mto_A_Pagar.ReadOnly = True
                    TD.Checked = False
                    Gr_Documentos.Rows(I).Cells(10).Text = 0
                    'Exit Sub
                End If

            Next


        End If

    End Sub

    Protected Sub btn_acep_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn_acep.Click
        Try
            MP_Criterio.Show()
            NroPaginacion = 0
            CargaGrillaDoctos()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_canc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_canc.Click
        Try

            CB_Deudor.Checked = False
            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""
            Txt_Rso_Deu.Text = ""
            IB_AyudaDeu.Enabled = False
            DP_Estados.ClearSelection()
            DP_TipoDocto.ClearSelection()
            Txt_Nro_Oto.Text = ""
            Txt_Nro_Doc.Text = ""
            Txt_Mto_Dsd.Text = ""
            Txt_Mto_Hst.Text = ""
            Txt_Fec_Dsd.Text = ""
            Txt_Fec_Hst.Text = ""
            DP_CodCobranza.ClearSelection()
            DP_Estados.SelectedValue = 999

            MP_Criterio.Hide()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Seleccionar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Mto_A_Pagar As TextBox
        Dim CB As CheckBox
        Dim TD As CheckBox

        CB = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_Seleccionar"), CheckBox)

        'Validamos que tenga factor de cambio del dia de la cuenta, sino no lo deja agregar
        If Coll_DOC.Item(HF_Posicion.Value + 1).ope_fac_cam <= 0 And CB.Checked Then
            Msj.Mensaje(Me.Page, "Cuentas Por Cobrar", "No se puede agregar esta CXC por no tener factor de cambio del dia de la cuenta", TipoDeMensaje._Exclamacion)
            CB.Checked = False
            Exit Sub
        End If

        Mto_A_Pagar = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("Txt_MtoPagar"), TextBox)
        TD = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_PD"), CheckBox)

        If CB.Checked Then

            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False
            TD.Checked = True
            CalculaInteresDoctos(HF_Posicion.Value)

            If Not ValidaSeleccionDoctos(Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text) Then

                Coll_DOC.Item(HF_Posicion.Value + 1).MontoPagar = CDbl(CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("Txt_MtoPagar"), TextBox).Text) - CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text)
                Coll_DOC.Item(HF_Posicion.Value + 1).Interes = Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text
                Coll_DOC.Item(HF_Posicion.Value + 1).Tasa = Txt_Tasa_Aplicar.Text.Replace(",", ".")
                Coll_DOC.Item(HF_Posicion.Value + 1).PagaDeudor = "S"

                Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(HF_Posicion.Value + 1))

            End If

            '* * * * * * * *  Valida si existe otro pago no este procesado  * * * * * * *
            Dim Coll As Collection

            Coll = PGO.PagosValidaEstados(Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text, "APLI-INGR")

            If Coll.Count > 0 Then
                GridPagos.DataSource = Coll
                GridPagos.Columns(1).Visible = False
                GridPagos.DataBind()
                ModalPopupExtender1.Show()
            End If



            '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        Else

            If EliminaSeleccionDoctos(Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text) Then

                Mto_A_Pagar.Text = 0
                Mto_A_Pagar.CssClass = "clsDisabled"
                Mto_A_Pagar.ReadOnly = True
                TD.Checked = False
                'Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text = 0
            End If


        End If

        SumatoriaDocumentos()


    End Sub

    Protected Sub CB_PD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For I = 1 To Coll_Doctos_Seleccionados.Count

            If Coll_Doctos_Seleccionados.Item(I).id_doc = Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text Then

                If CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_PD"), CheckBox).Checked Then
                    Coll_Doctos_Seleccionados.Item(I).PagaDeudor = "S"
                Else
                    Coll_Doctos_Seleccionados.Item(I).PagaDeudor = "N"
                End If

                Exit For

            End If

        Next

    End Sub

    Protected Sub Txt_MtoPagar_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For I = 1 To Coll_Doctos_Seleccionados.Count

            If Coll_Doctos_Seleccionados.Item(I).id_doc = Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text Then

                Dim Mto_A_Pagar As TextBox

                Mto_A_Pagar = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("Txt_MtoPagar"), TextBox)
                Coll_Doctos_Seleccionados.Item(I).MontoPagar = CDbl(Mto_A_Pagar.Text)
                Mto_A_Pagar.Text = Format(CDbl(Mto_A_Pagar.Text), Fmt.FCMSD)
                Exit For

            End If

        Next

        SumatoriaDocumentos()

    End Sub

    Protected Sub IB_SeleccionarDoctos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_SeleccionarDoctos.Click

        For I = 0 To Gr_Documentos.Rows.Count - 1

            Dim Mto_A_Pagar As TextBox
            Dim CB As CheckBox
            Dim TD As CheckBox

            CB = Gr_Documentos.Rows(I).FindControl("CB_Seleccionar")
            Mto_A_Pagar = CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox)
            TD = CType(Gr_Documentos.Rows(I).FindControl("CB_PD"), CheckBox)

            CB.Checked = True
            TD.Checked = True
            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False

            CalculaInteresDoctos(I)

            If Not ValidaSeleccionDoctos(Gr_Documentos.Rows(I).Cells(5).Text) Then

                Coll_DOC.Item(I + 1).MontoPagar = CDbl(CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox).Text) - CDbl(Gr_Documentos.Rows(I).Cells(9).Text)
                Coll_DOC.Item(I + 1).Interes = Gr_Documentos.Rows(I).Cells(9).Text
                Coll_DOC.Item(I + 1).Tasa = Txt_Tasa_Aplicar.Text.Replace(",", ".")

                Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(I + 1))

            End If

        Next

    End Sub

    Private Sub CargaGrillaDoctos()

        Try

            If IsNothing(Coll_Doctos_Seleccionados) Then
                Coll_Doctos_Seleccionados = New Collection
            End If


            'If Coll_DPO.Count <= 0 Then
            '    MsgBox("Debe Ingresar primero los pagos")
            '    Exit Sub
            'End If

            Dim Rut_Cli_Desde As Long
            Dim Rut_Cli_Hasta As Long

            Dim Rut_Deu_Desde As Long
            Dim Rut_Deu_Hasta As Long

            Dim TipoDoc_Desde As Integer
            Dim TipoDoc_Hasta As Integer

            Dim NroOtor_Desde As Long
            Dim NroOtor_Hasta As Long

            Dim NroDoct_Desde As String
            Dim NroDoct_Hasta As String

            Dim Fec_Vto_Desde As DateTime
            Dim Fec_Vto_Hasta As DateTime

            Dim Mto_Dsd As Long
            Dim Mto_hst As Long

            'Criterio de Busqueda

            Rut_Cli_Desde = Txt_Rut_Cli.Text
            Rut_Cli_Hasta = Txt_Rut_Cli.Text

            Rut_Deu_Desde = 0
            Rut_Deu_Hasta = 9999999999999

            If DP_Estados.SelectedValue = 0 Then
                Msj.Mensaje(Page, Caption, "Debe seleccionar estado", ClsMensaje.TipoDeMensaje._Excepcion)

                Exit Sub
            End If


            'Si busca por Deudor
            If CB_Deudor.Checked Then
                If Txt_Rut_Deu.Text = "" Then
                    Rut_Deu_Desde = 0
                    Rut_Deu_Hasta = 9999999999999
                Else
                    Rut_Deu_Desde = Txt_Rut_Deu.Text
                    Rut_Deu_Hasta = Txt_Rut_Deu.Text
                End If
            End If

            'Montos de documentos
            If Txt_Mto_Dsd.Text <> "" Then
                Mto_Dsd = Txt_Mto_Dsd.Text
            Else
                Mto_Dsd = 0
            End If
            If Txt_Mto_Hst.Text <> "" Then
                Mto_hst = Txt_Mto_Hst.Text
            Else
                Mto_hst = 9999999999999
            End If

            If Mto_Dsd > Mto_hst Then
                Msj.Mensaje(Page, Caption, "Monto desde no puede ser mayor a monto hasta", ClsMensaje.TipoDeMensaje._Excepcion)
                Txt_Mto_Dsd.Text = ""
                Txt_Mto_Hst.Text = ""
                Exit Sub
            End If



            'Tipo Docto
            If DP_TipoDocto.SelectedValue = 0 Then
                TipoDoc_Desde = 0
                TipoDoc_Hasta = 999
            Else
                TipoDoc_Desde = DP_TipoDocto.SelectedValue
                TipoDoc_Hasta = DP_TipoDocto.SelectedValue
            End If

            'Nro Otorgamiento
            If Txt_Nro_Oto.Text = "" Then
                NroOtor_Desde = 0
                NroOtor_Hasta = 999999999
            Else
                NroOtor_Desde = Txt_Nro_Oto.Text
                NroOtor_Hasta = Txt_Nro_Oto.Text
            End If

            'Nro Documento
            If Txt_Nro_Doc.Text = "" Then
                NroDoct_Desde = "0"
                NroDoct_Hasta = "Z"
            Else
                NroDoct_Desde = Txt_Nro_Doc.Text
                NroDoct_Hasta = Txt_Nro_Doc.Text
            End If


            'Fecha Vcto
            If Txt_Fec_Dsd.Text = "" And Txt_Fec_Hst.Text = "" Then
                Fec_Vto_Desde = "01/01/1900"
                Fec_Vto_Hasta = "01/01/2100" 'Date.Now.ToShortDateString
            Else
                If Txt_Fec_Dsd.Text <> "" Then
                    If Not IsDate(Txt_Fec_Dsd.Text) Then
                        Msj.Mensaje(Page, Caption, "fecha desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If

                If Txt_Fec_Hst.Text <> "" Then
                    If Not IsDate(Txt_Fec_Hst.Text) Then
                        Msj.Mensaje(Page, Caption, "fecha hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If

                Fec_Vto_Desde = Txt_Fec_Dsd.Text
                Fec_Vto_Hasta = Txt_Fec_Hst.Text
            End If

            If CDate(Fec_Vto_Desde) > CDate(Fec_Vto_Hasta) Then
                Msj.Mensaje(Page, Caption, "Fecha desde no puede ser mayor a fecha hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Fec_Dsd.Text = ""
                Txt_Fec_Hst.Text = ""
                Exit Sub
            End If

            Coll_DOC = New Collection

            Dim Estado1, Estado2, Estado3, Estado4, Estado5, Estado6, Estado7, Estado8, Estado9, Estado10, Estado11, Estado12 As Integer


            If DP_Estados.SelectedValue = 999 Then
                Estado1 = 1
                Estado2 = 2
                Estado3 = 4
                Estado4 = 9
                Estado5 = 11
                Estado6 = 12
                Estado7 = 1
                Estado8 = 1
                Estado9 = 1
                Estado10 = 1
                Estado11 = 1
                Estado12 = 1
            Else
                Estado1 = DP_Estados.SelectedValue
                Estado2 = DP_Estados.SelectedValue
                Estado3 = DP_Estados.SelectedValue
                Estado4 = DP_Estados.SelectedValue
                Estado5 = DP_Estados.SelectedValue
                Estado6 = DP_Estados.SelectedValue
                Estado7 = DP_Estados.SelectedValue
                Estado8 = DP_Estados.SelectedValue
                Estado9 = DP_Estados.SelectedValue
                Estado10 = DP_Estados.SelectedValue
                Estado11 = DP_Estados.SelectedValue
                Estado12 = DP_Estados.SelectedValue
            End If


            Dim Coll_Obj = PGO.DocumentosOtorgagosPagos_RetornaDoctos(Format(Rut_Cli_Desde, Var.FMT_RUT), Format(Rut_Cli_Hasta, Var.FMT_RUT), _
                                                                     Format(Rut_Deu_Desde, Var.FMT_RUT), Format(Rut_Deu_Hasta, Var.FMT_RUT), _
                                                                     NroOtor_Desde, NroOtor_Hasta, _
                                                                     TipoDoc_Desde, TipoDoc_Hasta, _
                                                                     NroDoct_Desde, NroDoct_Hasta, _
                                                                     0, 9999, _
                                                                     Fec_Vto_Desde, Fec_Vto_Hasta, _
                                                                     Estado1, _
                                                                     Estado2, _
                                                                     Estado3, _
                                                                     Estado4, _
                                                                     Estado5, _
                                                                     Estado6, _
                                                                     Estado7, _
                                                                     Estado8, _
                                                                     Estado9, _
                                                                     Estado10, _
                                                                     Estado11, _
                                                                     Estado12, _
                                                                     Mto_Dsd, Mto_hst, _
                                                                     Val(HF_NroAplicacion.Value))



            For Each Obj In Coll_Obj

                If DP_CodCobranza.SelectedIndex > 0 Then
                    If Obj.id_cco = DP_CodCobranza.SelectedValue Then
                        Coll_DOC.Add(Obj)
                    End If
                Else
                    Coll_DOC.Add(Obj)
                End If

            Next

            Gr_Documentos.DataSource = Coll_DOC
            Gr_Documentos.DataBind()


            If Gr_Documentos.Rows.Count > 0 Then
                FormatoGridDoc()
                IB_Next.Enabled = True
                IB_Prev.Enabled = True
                MP_Criterio.Hide()

            Else
                Msj.Mensaje(Me.Page, "Doctos. de Pagos", "No existen documentos para pagar", ClsMensaje.TipoDeMensaje._Excepcion)
            End If


            If Coll_DOC.Count < 15 Then
                IB_Next.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FormatoGridDoc()

        Dim Formato As String = ""

        For I = 0 To Gr_Documentos.Rows.Count - 1

            Try

                Select Case Coll_DOC.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                'Rut Cliente
                Gr_Documentos.Rows(I).Cells(1).Text = FC.FormatoMiles(CInt(Gr_Documentos.Rows(I).Cells(1).Text)) & "-" & Coll_DOC.Item(I + 1).cli_dig_ito

                CalculaInteresDoctos(I)

                'Saldo
                If Gr_Documentos.Rows(I).Cells(8).Text = "" Then
                    Gr_Documentos.Rows(I).Cells(8).Text = 0
                Else
                    Gr_Documentos.Rows(I).Cells(8).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(8).Text), Formato)
                End If

                'Interes
                If Gr_Documentos.Rows(I).Cells(9).Text = "" Then
                    Gr_Documentos.Rows(I).Cells(9).Text = 0
                Else
                    Gr_Documentos.Rows(I).Cells(9).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(9).Text), Formato)
                End If

                CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox).Text = 0
                CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox).Attributes.Add("Style", "TEXT-ALIGN: right")

                'Aplic ctas y exc.
                If Coll_DOC.Item(I + 1).Aplicacion > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CC99FF")
                    Gr_Documentos.Rows(I).BackColor = col
                End If

                'pagos sin procesar
                If Coll_DOC.Item(I + 1).Cantidad_Ingresos > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                    Gr_Documentos.Rows(I).BackColor = col
                End If

                Dim SumatoriaDoctos As Double = 0
                Dim SumatoriaDoctos_Int As Double = 0

                For x = 1 To Coll_Doctos_Seleccionados.Count


                    Dim Mto_A_Pagar As Double
                    Dim Paridad As Double

                    Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(Coll_Doctos_Seleccionados.Item(x).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

                    If Paridad > 0 Then

                        Mto_A_Pagar = Coll_Doctos_Seleccionados.Item(x).MontoPagar

                        SumatoriaDoctos += Coll_Doctos_Seleccionados.Item(x).MontoPagar * Paridad

                    End If

                Next
                txt_total_doc.Text = Txt_Tot_Doc.Text 'Format(SumatoriaDoctos, Fmt.FCMSD)


            Catch ex As Exception

            End Try

        Next

    End Sub

    Private Sub CalculaInteresDoctos(ByVal PosicionGV As Long)

        Try

            Dim Formulas As New FormulasGenerales
            Dim MtoAPagar As Double
            Dim Interes As Double
            Dim Saldo As Double
            Dim FechaSimula As String
            Dim FechaUltPago As String
            Dim FechaVctoRea As String
            Dim CantidadDias As String
            Dim Lineal As String
            Dim TasaAnuMen As String
            Dim TasaRenova As Decimal
            Dim MtoAnticip As Double
            Dim FecVctoOri As String
            Dim NroRenovac As Integer
            Dim TasaNegocio As Decimal
            Dim Tasa_Base As Double
            Dim Spread As Double
            Dim Puntos As Double
            Dim Mto_A_Pagar As TextBox
            Dim TD As CheckBox

            Mto_A_Pagar = CType(Gr_Documentos.Rows(PosicionGV).FindControl("Txt_MtoPagar"), TextBox)

            'If Habilita Then
            '    Mto_A_Pagar.CssClass = "clsMandatorio"
            '    Mto_A_Pagar.ReadOnly = False
            'End If

            TD = CType(Gr_Documentos.Rows(PosicionGV).FindControl("CB_PD"), CheckBox)
            TD.Checked = True

            'Buscamos el documento para traer todas sus relaciones
            Dim DOC As doc_cls = OP.DocumentoOtorgagoDevuelvePorId(Coll_DOC.Item(PosicionGV + 1).id_doc)

            'Rescatamos el saldo del documento
            Saldo = CDbl(Gr_Documentos.Rows(PosicionGV).Cells(8).Text)

            'Monto a pagar por defecto toma el saldo completo
            MtoAPagar = CDbl(Gr_Documentos.Rows(PosicionGV).Cells(8).Text)

            'validamos si la fecha de ultimo pago viene nula
            If IsNothing(DOC.opo_cls.opo_ful_pgo) Then
                FechaUltPago = "01/01/1900"
            Else
                FechaUltPago = DOC.opo_cls.opo_ful_pgo
            End If

            FechaSimula = DOC.opo_cls.ope_cls.ope_fec_sim
            FechaVctoRea = DOC.dsi_cls.dsi_fev_rea
            CantidadDias = DOC.dsi_cls.dsi_ctd_dia

            If IsNothing(DOC.opo_cls.ope_cls.ope_lnl) Then
                Lineal = "N"
            Else
                Lineal = DOC.opo_cls.ope_cls.ope_lnl
            End If

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa) Then
                TasaAnuMen = 0
            Else
                TasaAnuMen = DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa
            End If

            If IsNothing(DOC.doc_tas_ren) Then
                TasaRenova = 0
            Else
                TasaRenova = DOC.doc_tas_ren
            End If

            If IsNothing(DOC.dsi_cls.dsi_fev) Then
                FecVctoOri = "01/01/1900"
            Else
                FecVctoOri = DOC.dsi_cls.dsi_fev
            End If

            If IsNothing(DOC.doc_num_ren) Then
                NroRenovac = 0
            Else
                NroRenovac = DOC.doc_num_ren
            End If

            MtoAnticip = DOC.dsi_cls.dsi_mto_ant

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas) Then
                Tasa_Base = 0
            Else
                Tasa_Base = DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas
            End If

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead) Then
                Spread = 0
            Else
                Spread = DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead
            End If

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr) Then
                Puntos = 0
            Else
                Puntos = DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr
            End If


            'TasaNegocio = Tasa_Base + Spread + Puntos
            TasaNegocio = DOC.opo_cls.ope_cls.opn_cls.opn_tas_neg
            'FechaSimula = "03/03/2008"
            'FechaVctoRea = "25/04/2008"
            'CantidadDias = 15
            'Lineal = "S"
            'TasaAnuMen = "M"
            'TasaRenova = 1.15
            'MtoAnticip = 500000
            'FecVctoOri = "17/03/2008"
            'NroRenovac = 1

            Interes = Formulas.RetornaCalculoInteres(Txt_Fecha_Aplicacion.Text, _
                                                     0, _
                                                     Txt_Tasa_Aplicar.Text, _
                                                     MtoAPagar, _
                                                     FechaSimula, _
                                                     FechaVctoRea, _
                                                     CantidadDias, _
                                                     Saldo, _
                                                     FechaUltPago, _
                                                     CG.SistemaDevuelve.sis_dia_dev, _
                                                     Lineal, _
                                                     TasaAnuMen, _
                                                     TasaNegocio, _
                                                     TasaRenova, _
                                                     MtoAnticip, _
                                                     FecVctoOri, _
                                                     NroRenovac, _
                                                     DOC.id_doc, _
                                                     DOC.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas)



            If DOC.opo_cls.ope_cls.opn_cls.id_P_0023 <> 1 Then
                Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMCD)
                Mto_A_Pagar.Text = Format(Interes + Saldo, Fmt.FCMCD)
            Else
                Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMSD)
                Mto_A_Pagar.Text = Format(Interes + Saldo, Fmt.FCMSD)
            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub SumatoriaDocumentos()

        Try

            Dim SumatoriaDoctos As Double = 0
            Dim SumatoriaDoctos_Int As Double = 0

            For I = 1 To Coll_Doctos_Seleccionados.Count


                Dim Mto_A_Pagar As Double
                Dim Paridad As Double

                Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(Coll_Doctos_Seleccionados.Item(I).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

                If Paridad > 0 Then

                    Mto_A_Pagar = Coll_Doctos_Seleccionados.Item(I).MontoPagar

                    SumatoriaDoctos += Coll_Doctos_Seleccionados.Item(I).MontoPagar * Paridad
                    SumatoriaDoctos_Int += Coll_Doctos_Seleccionados.Item(I).Interes * Paridad

                End If

            Next

            'For I = 0 To Gr_Documentos.Rows.Count - 1

            '    If CType(Gr_Documentos.Rows(I).FindControl("CB_Seleccionar"), CheckBox).Checked Then

            '        Dim Mto_A_Pagar As TextBox
            '        Dim Paridad As Double

            '        Paridad = CG.RETORNA_VALOR_MONEDA_COBRANZA(Coll_DOC.Item(I + 1).id_p_0023, 1, Txt_Fecha_Aplicacion.Text, 0, 0, 1)

            '        If Paridad > 0 Then

            '            Mto_A_Pagar = CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox)

            '            SumatoriaDoctos += CDbl(Mto_A_Pagar.Text) * Paridad
            '            SumatoriaCxC_Int += CDbl(Gr_Documentos.Rows(I).Cells(9).Text) * Paridad

            '        End If

            '    End If

            'Next

            Txt_Pag_Doc.Text = Format(SumatoriaDoctos, Fmt.FCMSD)
            HF_Interes_Doc.Value = SumatoriaDoctos_Int
            Deuda_Total()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DP_Orden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Orden.SelectedIndexChanged

        Try

            Select Case DP_Orden.SelectedValue

                Case 1 'RUT DEUDOR
                    FC.SortCollection(Coll_DOC, "deu_ide", True, "")

                Case 2 'N° OTORG.
                    FC.SortCollection(Coll_DOC, "id_opo", True, "")

                Case 3 'N° DOCTO.
                    FC.SortCollection(Coll_DOC, "id_doc", True, "")

                Case 4 'FECHA DE VCTO.
                    FC.SortCollection(Coll_DOC, "doc_fev_rea", True, "")

                Case 5

                    Select Case Pagos.Pagador
                        Case "C"
                            FC.SortCollection(Coll_DOC, "doc_sdo_cli", True, "")
                        Case "D"
                            FC.SortCollection(Coll_DOC, "doc_sdo_ddr", True, "")
                    End Select

                Case 6 'ESTADO DEL DOCUMENTO
                    FC.SortCollection(Coll_DOC, "id_p_0011", True, "")

            End Select

            Gr_Documentos.DataSource = Coll_DOC
            Gr_Documentos.DataBind()

            FormatoGridDoc()


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        NroPaginacion += 15

        CargaGrillaDoctos()
        MarcaDoctosSeleccionados()

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion > 0 Then
            NroPaginacion -= 15

            CargaGrillaDoctos()
            MarcaDoctosSeleccionados()

        End If

    End Sub

    Private Sub MarcaDoctosSeleccionados()

        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count

                For X = 0 To Gr_Documentos.Rows.Count - 1

                    If Coll_Doctos_Seleccionados.Item(I).id_doc = Gr_Documentos.Rows(X).Cells(5).Text Then

                        Dim Mto_A_Pagar As TextBox
                        Dim CB As CheckBox
                        Dim TD As CheckBox

                        CB = Gr_Documentos.Rows(X).FindControl("CB_Seleccionar")
                        Mto_A_Pagar = CType(Gr_Documentos.Rows(X).FindControl("Txt_MtoPagar"), TextBox)
                        TD = CType(Gr_Documentos.Rows(X).FindControl("CB_PD"), CheckBox)

                        CB.Checked = True
                        TD.Checked = True
                        Mto_A_Pagar.CssClass = "clsMandatorio"
                        Mto_A_Pagar.ReadOnly = False

                        Mto_A_Pagar.Text = Coll_Doctos_Seleccionados.Item(I).MontoPagar

                        CalculaInteresDoctos(X)

                    End If

                Next
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Function ValidaSeleccionDoctos(ByVal id_doc As Integer) As Boolean

        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count


                If Coll_Doctos_Seleccionados.Item(I).id_doc = id_doc Then
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function

    Private Function EliminaSeleccionDoctos(ByVal nro_doc As String) As Boolean

        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count

                If Coll_Doctos_Seleccionados.Item(I).dsi_num = nro_doc Then
                    Coll_Doctos_Seleccionados.Remove(I)
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function

    Protected Sub CB_Deudor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Deudor.CheckedChanged
        Try

            If CB_Deudor.Checked Then


                Txt_Rut_Deu.ReadOnly = False
                Txt_Dig_Deu.ReadOnly = False
                Txt_Dig_Deu.Text = ""
                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Rso_Deu.Text = ""

                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.CssClass = "clsMandatorio"

                Txt_Rut_Deu.Focus()
                IB_AyudaDeu.Enabled = True

                Txt_Rut_Deu_MaskedEditExtender.Enabled = True

            Else

                Txt_Dig_Deu.Text = ""
                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Rso_Deu.Text = ""

                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.ReadOnly = True

                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.CssClass = "clsDisabled"
                IB_AyudaDeu.Enabled = False
                Txt_Rut_Deu_MaskedEditExtender.Enabled = False

            End If

            MP_Criterio.Show()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Sub

#End Region

#Region "TASA Y FECHA DE APLICACION"

    Protected Sub IB_AplicarTasa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AplicarTasa.Click
        Try
            If Trim(Txt_Tasa_Aplicar.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese tasa mora", TipoDeMensaje._Informacion)
                Txt_Tasa_Aplicar.Focus()
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "Modificó tasa. total a pagar de doctos. y cxc cambiará", TipoDeMensaje._Informacion)

            'DOCUMENTOS
            FormatoGridDoc()
            FormatoGridCxC()

            'For i = 0 To Gr_Documentos.Rows.Count - 1

            '    If CType(Gr_Documentos.Rows(i).FindControl("CB_Seleccionar"), CheckBox).Checked Then
            '        CalculaInteresDoctos(i)
            '    End If
            'Next

            ''CUENTAS POR COBRAR
            'For i = 0 To GV_CxC.Rows.Count - 1
            '    If CType(GV_CxC.Rows(i).FindControl("CB_Seleccionar_CxC"), CheckBox).Checked Then
            '        CalculaInteresCxC(i)
            '    End If
            'Next



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Sub

    Protected Sub IB_AplicarFecha_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AplicarFecha.Click
        Try
            If Trim(Txt_Fecha_Aplicacion.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese fecha de aplicación", TipoDeMensaje._Informacion)
                Txt_Fecha_Aplicacion.Focus()
                Exit Sub
            End If

            If Not IsDate(Txt_Fecha_Aplicacion.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha de aplicación errónea", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "Modificó fecha de pago. total a pagar de doctos. y cxc cambiará", TipoDeMensaje._Informacion)

            'DOCUMENTOS
            For i = 0 To Gr_Documentos.Rows.Count - 1

                If CType(Gr_Documentos.Rows(i).FindControl("CB_Seleccionar"), CheckBox).Checked Then
                    CalculaInteresDoctos(i)
                End If
            Next

            'CUENTAS POR COBRAR
            For i = 0 To GV_CxC.Rows.Count - 1
                If CType(GV_CxC.Rows(i).FindControl("CB_Seleccionar_CxC"), CheckBox).Checked Then
                    CalculaInteresCxC(i)
                End If
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region
    
    
    
    
    
   
End Class


