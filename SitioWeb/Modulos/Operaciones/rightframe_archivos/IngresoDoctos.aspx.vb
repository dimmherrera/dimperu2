Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb

Partial Class Modulos_Operaciones_rightframe_archivos_IngresoDoctos
    Inherits System.Web.UI.Page
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim rutinas As New FuncionesGenerales.RutinasWeb
    Dim clasecli As New ClaseClientes
    Dim Cuo As Integer
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim var As New FuncionesGenerales.Variables
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial
    Dim ope As New ClaseOperaciones

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Response.Expires = -1
            Txt_Rut_Deu.Attributes.Add("Style", "Text-Align: right")

            caso = Nothing

            'Llena DroList con Parámetros de Sistema.
            CargaDrop_Form()

            
            'Asigna posicion en collection de Operación
            Me.Txt_ItemOpe.Value = Val(Request.QueryString("itemOPE"))
            
            HabilitaInhabilitaDatosDeudor("I")

            '------------------------------------------------------------------------------------------------------
            'Traemos parametros de linea para proponerlos (cobranza y notificacion)
            Dim ldc As ldc_cls

            ldc = CG.LineaDeCreditoDevuelve(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, 1)


            If Not IsNothing(ldc) Then

                Dim ant As apc_cls

                ant = CG.AnticipoDevuelvePorLinea(ldc.id_ldc, _
                                                  coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031)

                If Not IsNothing(ant) Then

                    If ant.apc_cob_son = "S" Then
                        Rb_Cob.Items(0).Selected = True
                        Rb_Cob.Items(1).Selected = False
                    Else
                        Rb_Cob.Items(0).Selected = False
                        Rb_Cob.Items(1).Selected = True
                    End If

                    If ant.apc_not_son = "S" Then
                        Rb_Not.Items(0).Selected = True
                        Rb_Not.Items(1).Selected = False
                    Else
                        Rb_Not.Items(0).Selected = False
                        Rb_Not.Items(1).Selected = True
                    End If

                End If

            End If
            '------------------------------------------------------------------------------------------------------



            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.Enabled = True
            Me.Txt_Rut_Deu.ReadOnly = False

            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.Enabled = True
            Me.Txt_Dig_Deu.ReadOnly = False
            alinea_textos()
            If Txt_ItemOpe.Value <> 0 Then


                If coll_ope.Item(Val(Txt_ItemOpe.Value)).id_p_0023 = 1 Then

                    Txt_valor_MaskedEditExtender.Mask = "999,999,999,999"
                    Me.rb_respaldo.Enabled = False

                ElseIf coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 3 Or coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 4 Then
                    Txt_valor_MaskedEditExtender.Mask = "999,999,999.99"
                    Me.rb_respaldo.Enabled = False


                End If
            End If

            If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_ope) > 1 Then
                msj.Mensaje(Page, "Atención", "Documento no se puede modificar", ClsMensaje.TipoDeMensaje._Exclamacion)
                ClosePag(Me)
                Exit Sub
            End If

            If Val(Trim(Request.QueryString("itemDSI"))) > "0" Then
                sw.Value = "UPDATE"
                'Retorna Datos de Documento Seleccionado
                RetonaDatosObj_DSI(CInt(Trim(Request.QueryString("itemDSI"))))
                'Asigna Nro de Documento Cuando corresponde
                Me.Txt_ItemDSI.Value = Trim(Request.QueryString("itemDSI"))
            Else
                sw.Value = "INSERT"
                'Inicializa Campos en estado Nuevo
                EstadoNuevoDSI()
                'If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then
                '    Me.Dr_bco.Enabled = True
                '    Me.Dr_bco.CssClass = "clsMandatorio"

                'End If
            End If

            Txt_DiaVto.Text = coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).opn_dia_vto

        Else

            Me.Txt_Rut_Deu.Focus()

        End If

        'btn_volver.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$Lb_buscar');")
        btn_volver.Attributes.Add("onclick", "JavaScript:window.close();")
        IB_AyudaDeu.Attributes.Add("onclick", "WinOpen(2,'../../Ayudas/AyudaDeudor.aspx','PopUpCliente',580,410,200,150);")

    End Sub

    Private Sub DP_TipoDeudor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DP_TipoDeudor.SelectedIndexChanged
        If DP_TipoDeudor.SelectedItem.Value = 2 Then
            Me.Lab_Paterno.Visible = False
            Me.Lab_Materno.Visible = False
            Me.Txt_ApeMaterno.Visible = False
            Me.Txt_ApePaterno.Visible = False
        Else
            Me.Lab_Paterno.Visible = True
            Me.Lab_Materno.Visible = True
            Me.Txt_ApeMaterno.Visible = True
            Me.Txt_ApePaterno.Visible = True
        End If
    End Sub

    Private Sub Txt_FecVcto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FecVcto.TextChanged

        Dim cw As New FuncionesGenerales.FComunes
        Dim sist As New sis_cls

        If Txt_FecVcto.Text = "__/__/____" Then
            'Txt_FecVcto.Focus()
            Exit Sub
        End If

        If Txt_FecVcto.Text = "" Or Txt_FecVcto.Text = "__/__/____" Then
            'Txt_FecVcto.Focus()
            Exit Sub
        End If


        If Not IsDate(Txt_FecVcto.Text) Then
            msj.Mensaje(Page, "Atención", "Fecha de vencimiento errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
            Txt_FecVcto.Text = ""
            Exit Sub
        End If

        sist = CG.SistemaDevuelve



        If IsDate(Me.Txt_FecVcto.Text) Then

            If CDate(Me.Txt_FecVcto.Text) < CDate(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).opn_fec_neg).AddDays(sist.sis_dia_vto) Then

                msj.Mensaje(Me, "Atención", "Fecha de vencimiento no debe ser menor al " & Format(CDate(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).opn_fec_neg).AddDays(sist.sis_dia_vto), "dd/MM/yyyy"), ClsMensaje.TipoDeMensaje._Exclamacion)
                Me.Txt_FecVcto.Text = ""
                Exit Sub

            End If

        End If



        If IsDate(Me.Txt_FecEmision.Text) And IsDate(Me.Txt_FecVcto.Text) Then

            If DateDiff(DateInterval.Day, CDate(Me.Txt_FecEmision.Text), CDate(Me.Txt_FecVcto.Text)) <= 0 Then

                msj.Mensaje(Me, "Atención", "Fecha de emisión no debe ser mayor a la de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Me.Txt_FecVcto.Text = ""
                Exit Sub
            End If
        End If

        If IsDate(Me.Txt_FecVcto.Text) Then

            HF_FacVctoCal.Value = CG.calcula_vcto_real(Txt_Rut_Deu.Text, _
                                                       CDate(Txt_FecVcto.Text).AddDays(Txt_DiaVto.Text), _
                                                       coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_suc, _
                                                       CStr(Me.Dr_plaza.SelectedValue), _
                                                       coll_ope.Item(Val(Txt_ItemOpe.Value)).id_p_0031)

            Txt_VctoReal.Text = Format(CDate(FECHA_VCTO_AUX), "dd/MM/yyyy")
            HF_FacVctoCal.Value = Format(CDate(FECHA_VCTO_CALCULO), "dd/MM/yyyy")

            Dim fecha As String = CG.DevuelveCalendarioPagoDeudor(Txt_Rut_Deu.Text, _
                                                                 CDate(Txt_FecVcto.Text).AddDays(Txt_DiaVto.Text))

            If Txt_FecVcto.Text <> fecha And fecha <> CDate(Txt_FecVcto.Text).AddDays(Txt_DiaVto.Text) Then
                Lbl_Msj_Vto.Visible = True
                Lbl_Msj_Vto.Text = "Pagador tiene calendarización de pago, para la fecha " & fecha
            End If

        End If

    End Sub

    Protected Sub Btn_Limpia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        If Val(Me.Txt_ItemDSI.Value) > 0 Then
            RetonaDatosObj_DSI(CInt(Trim(Me.Txt_ItemDSI.Value)))
        Else
            EstadoNuevoDSI()
        End If
    End Sub

    Protected Sub Dr_plaza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_plaza.SelectedIndexChanged

        Me.Txt_PlazaDes.Text = Me.Dr_plaza.SelectedValue

        HF_FacVctoCal.Value = CG.calcula_vcto_real(Txt_Rut_Deu.Text, _
                                                       Txt_FecVcto.Text, _
                                                       coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_suc, _
                                                       CStr(Me.Dr_plaza.SelectedValue), _
                                                       coll_ope.Item(Val(Txt_ItemOpe.Value)).id_p_0031)

        Txt_VctoReal.Text = Format(CDate(FECHA_VCTO_AUX), "dd/MM/yyyy")
        HF_FacVctoCal.Value = Format(CDate(FECHA_VCTO_CALCULO), "dd/MM/yyyy")

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn_guardar.Click

        Try

            Dim MensajeStr As String

            Dim ValidaStr As String
            Dim cw As New FuncionesGenerales.FComunes

            If Txt_Rut_Deu.Text = "" Or Txt_Dig_Deu.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If TipoInsert <> "Deu" Then
                If Me.Txt_NroDocto.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar Nº de documento", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_MontoFinanciar0.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar el monto del documento", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_FecEmision.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar fecha de emisión", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Me.Txt_FecVcto.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar fecha de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Not IsDate(Txt_FecEmision.Text) Then
                    msj.Mensaje(Page, "Exclamación", "Fecha de emisión errónea", ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                If Not IsDate(Txt_FecVcto.Text) Then
                    msj.Mensaje(Page, "Exclamación", "Fecha de vencimiento errónea", ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                If DateDiff(DateInterval.Day, CDate(Me.Txt_FecEmision.Text), CDate(Me.Txt_FecVcto.Text)) < 0 Then

                    msj.Mensaje(Me, "Exclamación", "Fecha de vencimiento no puede ser menor a la fecha de ingreso", 3)
                    Exit Sub
                End If

                'If Txt_Cal_oto.Text.Trim = "" Then
                '    msj.Mensaje(Me, "Exclamación", "Debe ingresar calificacion para el documentos", 3)
                '    Exit Sub
                'Else

                '    Select Case Txt_Cal_oto.Text.Trim
                '        Case "AA"
                '        Case "A"
                '        Case "BB"
                '        Case Else
                '            msj.Mensaje(Me, "Exclamación", "Debe ingresar calificacion valida para el documentos", 3)
                '            Exit Sub
                '    End Select

                'End If


                If Me.rb_cust.SelectedValue = "S" Then
                    If Me.dr_custodia.SelectedValue = 0 Then
                        msj.Mensaje(Me, "Atención", "Debe seleccionar custodia", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub

                    End If

                End If

                If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then

                    If Me.Dr_bco.SelectedValue = 0 Then

                        msj.Mensaje(Me, "Exclamación", "Debes seleccionar un banco para realizar el ingreso", 3)

                        Exit Sub
                    End If

                    If Me.Dr_plaza.SelectedIndex = 0 Then
                        msj.Mensaje(Me, "Exclamación", "Debes seleccionar una plaza para realizar el ingreso", 3)
                        Exit Sub

                    End If

                    If Me.txt_cta_cte.Text = "" Then
                        msj.Mensaje(Me, "Exclamación", "Debe digitar la cuenta corriente para realizar el ingreso", 3)
                        Exit Sub

                    End If

                    If Me.dr_custodia.SelectedValue = 0 Then
                        msj.Mensaje(Me, "Atención", "Debe seleccionar una custodia para el documento", ClsMensaje.TipoDeMensaje._Excepcion)
                        Exit Sub
                    End If


                ElseIf coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 2 Then

                    If Me.Dr_plaza.SelectedValue = 0 Then
                        msj.Mensaje(Me, "Exclamación", "Debe seleccionar una plaza para realizar el ingreso", 3)
                        Exit Sub
                    End If
                End If
            End If

            If Val(Me.Txt_ItemDSI.Value) > 0 Then
                caso = 1
                ValidaStr = "DOC"
                'msj.Mensaje(Me, "Confirmación", "¿Desea actualizar documento?", 4, lb_guar.UniqueID, False)

                Guardar()
                ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), var.FMT_RUT), Txt_NroDocto.Text, Txt_MontoFinanciar0.Text, 1, 1)
                HabilitaInhabilitaDatosDocumento("H")

            Else
                If TipoInsert = "Deu" Then
                    caso = 2
                    ValidaStr = "DEU"
                    'msj.Mensaje(Me, "Confirmación", "¿Desea ingresar Pagador?", 4, lbl_cli_ddr.UniqueID, False)
                    If TipoInsert = "Deu" Then
                        IngresaDeudor()
                    End If
                    AceptaNuevaRelacionClienteDeudor()
                Else
                    caso = 1
                    ValidaStr = "DOC"
                    'msj.Mensaje(Me, "Confirmación", "¿Desea ingresar documento?", 4, lb_guar.UniqueID, False)

                    Guardar()
                    ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), var.FMT_RUT), Txt_NroDocto.Text, Txt_MontoFinanciar0.Text, 1, 1)
                    HabilitaInhabilitaDatosDocumento("H")
                End If
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Lb_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_Buscar.Click
        'Validaciones de RUT y Digito Verificaor
        If Trim(Me.Txt_Rut_Deu.Text) = "" Then
            msj.Mensaje(Me, "Exclamación", "Debe ingresar NIT del Pagador", 3)
            Exit Sub
        End If

        If Trim(Me.Txt_Dig_Deu.Text) = "" Then
            msj.Mensaje(Me, "Exclamación", "Debe ingresar dígito verificador", 3)
            Exit Sub
        End If

        'If UCase(Trim(Me.Txt_Dig_Deu.Text)) <> RG.Vrut(Me.Txt_Rut_Deu.Text) Then
        '    msj.Mensaje(Me, "Error", "NIT de Pagador incorrecto", 1)

        '    Exit Sub
        'End If

        If CLng(Me.Txt_Rut_Deu.Text) = CLng(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc) Then
            msj.Mensaje(Me, "Atención", "No puede ingresar como pagador al cliente de la operación", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rut_Deu.Focus()
            Exit Sub
        End If
        'Llena Objeto DEU 

        Dim deu As New deu_cls
        Dim rel As Boolean
        deu = CG.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), "000000000000"))

        If IsNothing(deu) = False Then
            Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

            'Datos Deudor
            With deu


                rel = clasecli.RelacionClienteDeudorDevuelve(Me.Txt_Rut_Deu.Text, "A", coll_ope.Item(1).cli_idc)

                If rel = False Then


                    caso = 2
                    msj.Mensaje(Me, "Atención ", "La relación cliente pagador no existe ¿desea establecerla?", 4, lbl_cli_ddr.UniqueID)

                Else
                    'Datos Deudor  
                    HabilitaInhabilitaDatosDeudor("I")

                    'Textos
                    Me.Txt_relacioncliddr.value = "S"
                    '  Me.Txt_Rut_Deu.Text = .deu_ide
                    Me.Txt_Dig_Deu.Text = RG.Vrut(Me.Txt_Rut_Deu.Text)
                    Me.Txt_Rso_Deu.Text = .deu_rso
                    Me.Txt_ApePaterno.Text = .deu_ape_ptn
                    Me.Txt_ApeMaterno.Text = .deu_ape_mtn

                    'DropList

                    Me.DP_TipoDeudor.SelectedValue = .id_P_0044
                    If .id_P_0044 <> 1 Then
                        Me.Lab_Paterno.Visible = False
                        Me.lab_materno.Visible = False
                        Me.Txt_ApeMaterno.Visible = False
                        Me.Txt_ApePaterno.Visible = False
                    Else
                        Me.Lab_Paterno.Visible = True
                        Me.lab_materno.Visible = True
                        Me.Txt_ApeMaterno.Visible = True
                        Me.Txt_ApePaterno.Visible = True
                    End If

                    If IsNothing(.id_P_0063) Then
                        Me.DP_AbrRazSoc.SelectedValue = 0
                    Else
                        Me.DP_AbrRazSoc.SelectedValue = .id_P_0063
                    End If


                    'Datos Documento
                    HabilitaInhabilitaDatosDocumento("H")
                    Me.Txt_NroDocto.Focus()
                    TipoInsert = "Doc"
                    Me.IB_AyudaDeu.Enabled = False
                End If

            End With
        Else

            msj.Mensaje(Me, "Exclamación", "El pagador no existe , ¿desea ingresarlo ahora?", 4, lb_deu.UniqueID)
            HabilitaInhabilitaDatosDocumento("I")

            TipoInsert = "Deu"
        End If

    End Sub

    Protected Sub Lb_cuo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_cuo.Click

        Dim RST As Integer

        If Not (Me.Txt_NroDocto.Text = "") Then

            RST = OP.Documentos_cuota_valida(Me.Txt_NroDocto.Text, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031)

            If RST = 999 Then

                msj.Mensaje(Me, "Atención", "Este nº de documento ya existe para este cliente , favor ingresar otro ", 3)

                Me.Txt_NroDocto.Text = ""
                Me.Txt_NroDocto.Focus()
                Exit Sub
            ElseIf RST = 998 Then
                Txt_Cuota.Text = 0
                Txt_Cuota.Enabled = False
                Txt_Cuota.CssClass = "clsDisabled"
                Txt_valor_MaskedEditExtender.Enabled = True
                Txt_valor_MaskedEditExtender.Focus()
                Txt_MontoFinanciar0.Focus()
            Else
                Txt_Cuota.Text = RST
                Txt_Cuota.Enabled = False
                Txt_Cuota.CssClass = "clsDisabled"
                Txt_valor_MaskedEditExtender.Enabled = True
                Txt_valor_MaskedEditExtender.Focus()
                Txt_MontoFinanciar0.Focus()
            End If

        End If


    End Sub

    Protected Sub rb_cust_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_cust.SelectedIndexChanged

        If Me.rb_cust.SelectedValue = "S" Then

            Me.dr_custodia.Enabled = True
            Me.dr_custodia.CssClass = "clsMandatorio"
            Me.dr_custodia.Visible = True
        Else
            Me.dr_custodia.Enabled = False
            Me.dr_custodia.CssClass = "clsDisabled"
            Me.dr_custodia.Visible = False
        End If

    End Sub

    Protected Sub rb_respaldo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_respaldo.SelectedIndexChanged
        'If Me.rb_respaldo.SelectedValue = "S" Then
        '    Me.Dr_bco.Enabled = True
        '    Me.Dr_bco.CssClass = "clsMandatorio"
        'Else
        '    Me.Dr_bco.Enabled = False
        '    Me.Dr_bco.CssClass = "clsDisabled"
        'End If
    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        HabilitaInhabilitaDatosDocumento("I")
        HabilitaInhabilitaDatosDeudor("N")
        IB_AyudaDeu.Enabled = True
        Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        Me.Txt_Rut_Deu.Enabled = True
        Me.Txt_Rut_Deu.ReadOnly = False

        Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
        Me.Txt_Dig_Deu.Enabled = True
        Me.Txt_Dig_Deu.ReadOnly = False
        Txt_FecEmision_CalendarExtender.Enabled = False
        'Txt_FecEmision_MaskedEditExtender.Enabled = False

        Txt_FecVcto_CalendarExtender.Enabled = False
        'Txt_FecVcto_MaskedEditExtender.Enabled = False

        Txt_valor_MaskedEditExtender.Enabled = False
        Txt_VctoReal_CalendarExtender.Enabled = False
        'Txt_VctoReal_MaskedEditExtender.Enabled = False


    End Sub

    Protected Sub lb_guar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guar.Click
        Guardar()
        ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), var.FMT_RUT), Txt_NroDocto.Text, Txt_MontoFinanciar0.Text, 1, 1)
        HabilitaInhabilitaDatosDocumento("H")
    End Sub

    Protected Sub lbl_cli_ddr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbl_cli_ddr.Click
        If TipoInsert = "Deu" Then
            IngresaDeudor()
        End If
        AceptaNuevaRelacionClienteDeudor()
    End Sub

    Protected Sub lb_deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_deu.Click
        HabilitaInhabilitaDatosDeudor("H")
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        Lb_Buscar_Click(Me, e)
    End Sub

    Protected Sub Txt_NroDocto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_NroDocto.TextChanged
        Try

            Dim RST As Integer

            If Not (Me.Txt_NroDocto.Text = "") Then

                RST = OP.Documentos_cuota_valida(Me.Txt_NroDocto.Text, _
                                                 coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                                 coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo, _
                                                 coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031)

                If RST = 999 Then
                    msj.Mensaje(Me, "Atención", "Este nº de documento ya existe para este cliente , favor ingresar otro ", 3)
                    Me.Txt_NroDocto.Text = ""
                    Txt_valor_MaskedEditExtender.Enabled = True
                    Me.Txt_NroDocto.Focus()
                    Exit Sub
                ElseIf RST = 998 Then
                    Me.Txt_Cuota.Text = 0
                    Me.Txt_Cuota.Enabled = False
                    Me.Txt_Cuota.CssClass = "clsDisabled"
                    Txt_valor_MaskedEditExtender.Enabled = True
                    Txt_MontoFinanciar0.Text = "0"
                Else
                    Me.Txt_Cuota.Text = RST
                    Me.Txt_Cuota.Enabled = False
                    Me.Txt_Cuota.CssClass = "clsDisabled"
                    Txt_valor_MaskedEditExtender.Enabled = True
                End If

                'revisamos si el documento tuvo verificacion
                Dim DVF As dvf_cls = OP.Documentos_verificar_valida(CInt(Me.Txt_NroDocto.Text), _
                                                                    Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT))

                If Not IsNothing(DVF) Then

                    'Documento se encuentra verificado (debemos proponer los datos)
                    Txt_valor_MaskedEditExtender.Enabled = False
                    Txt_FecVcto.Text = Format(DVF.dvf_fev, "dd-MM-yyyy")
                    Txt_MontoFinanciar0.Text = DVF.dvf_mto
                    Txt_VctoReal.Text = Format(DVF.dvf_fev_rea, "dd-MM-yyyy")

                    Lbl_Msj_Vto.Text = "Documento se encontraba verificado, por lo que se traen los datos como referencia"

                End If

            End If

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 2 Then
                Txt_MontoFinanciar0.Text = CDbl(Txt_MontoFinanciar0.Text)
                Txt_valor_MaskedEditExtender.Mask = "999,999,999.9999"
            ElseIf coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 3 Or coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 4 Then
                Txt_MontoFinanciar0.Text = CDbl(Txt_MontoFinanciar0.Text)
                Txt_valor_MaskedEditExtender.Mask = "999,999,999.99"
            ElseIf coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 1 Then
                Txt_MontoFinanciar0.Text = CLng(Txt_MontoFinanciar0.Text)
                Txt_valor_MaskedEditExtender.Mask = "999,999,999,999"
            End If
            Txt_valor_MaskedEditExtender.Enabled = True
            Me.Txt_MontoFinanciar0.Focus()

            Txt_FecVcto_TextChanged(Me, e)

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Sub IngresaDeudor()
        Try


            Dim Deu As New deu_cls

            With Deu

                .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
                .deu_dig_ito = Me.Txt_Dig_Deu.Text
                .deu_rso = Me.Txt_Rso_Deu.Text
                .id_suc = Sucursal
                .deu_nom = Me.Txt_Rso_Deu.Text
                .deu_ape_ptn = Me.Txt_ApePaterno.Text
                .deu_ape_mtn = Me.Txt_ApeMaterno.Text
                .id_P_0044 = Me.DP_TipoDeudor.SelectedItem.Value
                .id_P_0063 = Me.DP_AbrRazSoc.SelectedItem.Value
                .deu_var_ddr = "N"



            End With

            AG.DeudorInserta(Deu, 0, "")
            msj.Mensaje(Me, "Atención", "Registro Ingresado", 3)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ActualizaDocumento()

        Try

            Dim ingresaOmodifica As String

            Dim var As New FuncionesGenerales.Variables
            Dim dsi As New dsi_cls
            ingresaOmodifica = "M"

            With dsi

                .dsi_cei = "S"

                If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo = "S" Then

                    If Val(Me.Txt_Cuota.Text) > 0 Then
                        .dsi_flj = "N"
                    Else
                        .dsi_flj = "S"
                    End If

                Else
                    .dsi_flj = "N"
                End If

                .dsi_num_ren = 0
                .dsi_mto_ant = CDbl(CDbl(Me.Txt_MontoFinanciar0.Text) * (coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_por_ant / 100))
                .dsi_num = Me.Txt_NroDocto.Text
                .dsi_flj_num = Val(Me.Txt_Cuota.Text)
                .dsi_mto_fin = Me.Txt_MontoFinanciar0.Text
                .dsi_mto = Me.Txt_MontoFinanciar0.Text
                .dsi_fec_emi = CDate(Me.Txt_FecEmision.Text)
                .dsi_fev = CDate(Me.Txt_FecVcto.Text)
                .dsi_fev_rea = CDate(Me.Txt_VctoReal.Text)
                .dsi_fev_cal = CDate(Me.HF_FacVctoCal.Value)

                .dsi_ntf = Me.Rb_Not.SelectedValue
                .dsi_cbz_son = Me.Rb_Cob.SelectedValue

                '.dsi_cbz_son = Me.Rb_Cob_Dct.SelectedValue
                If Me.Dr_plaza.SelectedItem.Text = "Seleccionar" Then
                    .id_PL_000047 = Nothing
                Else
                    .id_PL_000047 = Me.Dr_plaza.SelectedItem.Text
                End If

                .id_ope = coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_ope
                If Me.rb_cust.SelectedValue = "S" Then
                    .id_P_0112 = dr_custodia.SelectedValue
                Else
                    .id_P_0112 = Nothing
                End If

                .dsi_rsp = Me.rb_respaldo.SelectedValue

                .cta_cte = Me.txt_cta_cte.Text

                .dsi_env_bci = Me.rb_cust.SelectedValue

                If Me.rb_cust.SelectedValue = "S" Then
                    .id_P_0112 = dr_custodia.SelectedValue
                Else
                    .id_P_0112 = Nothing
                End If

                If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 4 Then
                    .id_bco = Me.Dr_bco.SelectedValue
                Else
                    .id_bco = Nothing
                End If

            End With

            OP.Documentos_Ingresa(dsi, _
                                  coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                  Coll_DSI.Item(Val(Me.Txt_ItemDSI.Value)).id_p_0031, _
                                  ingresaOmodifica, _
                                  coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).OPE_CUO, _
                                  coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cal_oto_gam)

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo = "S" Then
                ingresaOmodifica = "M"
                OP.cabecera_documentos_guarda(dsi, coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031, ingresaOmodifica, coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo)
                msj.Mensaje(Me, "Atención", "Documento actualizado", ClsMensaje.TipoDeMensaje._Informacion)
            End If

            msj.Mensaje(Me, "Atención", "Documento actualizado", ClsMensaje.TipoDeMensaje._Informacion)


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Exclamacion)
        End Try

    End Sub

    Private Sub IngresaDocumento()

        Dim rslt As Boolean
        Dim ingresaOmodifica As String
        Dim var As New FuncionesGenerales.Variables
        Dim dsi As New dsi_cls
        Dim RST As Integer


        ingresaOmodifica = "I"

        With dsi

            .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
            .id_ope = coll_ope(CInt(Me.Txt_ItemOpe.Value)).id_ope
            .dsi_cei = "S"

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).OPE_CUO = "N" Then
                .dsi_flj = "N"
            Else
                If Val(Me.Txt_Cuota.Text) > 0 Then
                    .dsi_flj = "N"
                Else
                    .dsi_flj = "S"
                End If
            End If

            .dsi_num_ren = 0
            .id_P_0011 = 1
            .dsi_mto_ant = CDbl(Me.Txt_MontoFinanciar0.Text * (coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_por_ant / 100))
            .dsi_num = Me.Txt_NroDocto.Text

            RST = OP.Documentos_cuota_valida(Me.Txt_NroDocto.Text, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo, _
                                             coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031)

            If RST <> 999 And RST <> 998 Then
                Me.Txt_Cuota.Text = RST
                Me.Txt_Cuota.Enabled = False
                Me.Txt_Cuota.CssClass = "clsDisabled"
                Txt_valor_MaskedEditExtender.Enabled = True
            End If

            .dsi_flj_num = Val(Me.Txt_Cuota.Text)
            .dsi_mto_fin = Me.Txt_MontoFinanciar0.Text
            .dsi_mto = Me.Txt_MontoFinanciar0.Text
            .dsi_fec_emi = CDate(Me.Txt_FecEmision.Text)
            .dsi_fev = CDate(Me.Txt_FecVcto.Text)
            .dsi_fev_rea = CDate(Me.Txt_VctoReal.Text)
            .dsi_fev_ori = CDate(Me.Txt_FecVcto.Text)
            .dsi_fev_cal = CDate(Me.HF_FacVctoCal.Value)

            If Me.rb_cust.SelectedValue = "S" Then
                .id_P_0112 = dr_custodia.SelectedValue
            Else
                .id_P_0112 = Nothing
            End If

            .dsi_env_bci = Me.rb_cust.SelectedValue
            .dsi_ntf = Me.Rb_Not.SelectedValue
            .dsi_cbz_son = Me.Rb_Cob.SelectedValue

            If Me.Dr_plaza.SelectedItem.Text = "Seleccionar" Then
                .id_PL_000047 = Nothing
            Else
                .id_PL_000047 = Me.Dr_plaza.SelectedItem.Text
            End If

            .dsi_rsp = Me.rb_respaldo.SelectedValue
            .id_ope = coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_ope
            .cta_cte = Me.txt_cta_cte.Text
            .dsi_est_rsp = "N"

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then
                .id_bco = Me.Dr_bco.SelectedValue
            Else
                .id_bco = Nothing
            End If

        End With

        rslt = OP.Documentos_Ingresa(dsi, _
                                     coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                     coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031, _
                                     ingresaOmodifica, _
                                     coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo, _
                                     coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cal_oto_gam)

        If rslt Then
            msj.Mensaje(Me, "Documentos", "Registro ingresado", 3)
        Else
            msj.Mensaje(Me, "Documentos", "Registro no pudo ser ingresado", 3)
        End If

        ingresaOmodifica = "M"
        OP.cabecera_documentos_guarda(dsi, _
                                      coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc, _
                                      coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031, _
                                      ingresaOmodifica, _
                                      coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).ope_cuo)


        
    End Sub

    Private Sub EstadoNuevoDSI()
        'Datos Deudor
        'HabilitaInhabilitaDatosDeudor("H")

        'Datos Documento
        HabilitaInhabilitaDatosDocumento("I")
    End Sub

    Private Sub AceptaNuevaRelacionClienteDeudor()
        Dim ddr As New ddr_cls
        Dim e As New System.EventArgs
        Try

            With ddr
                .cli_idc = coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).cli_idc
                .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), "000000000000")

            End With

            clasecli.ClientesDeudoresInserta(ddr)
            Me.Lb_Buscar_Click(Me.Txt_Dig_Deu, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub HabilitaInhabilitaDatosDocumento(ByVal HI As String)

        If HI = "I" Then
            'Textos

            Me.Txt_MontoFinanciar0.Text = ""
            Me.Txt_MontoFinanciar0.CssClass = "clsDisabled"
            Me.Txt_MontoFinanciar0.ReadOnly = True

            Me.Txt_NroDocto.Text = ""
            Me.Txt_NroDocto.CssClass = "clsDisabled"
            Me.Txt_NroDocto.ReadOnly = True

            Me.Dr_plaza.CssClass = "clsDisabled"
            Me.Dr_plaza.Enabled = False
            'Me.Dr_plaza.SelectedIndex = 0
            Me.Txt_Cuota.Text = ""
            Me.Txt_Cuota.CssClass = "clsDisabled"
            Me.Txt_Cuota.ReadOnly = True



            Me.Txt_FecEmision.Text = ""
            Me.Txt_FecEmision.CssClass = "clsDisabled"
            Me.Txt_FecEmision.ReadOnly = True

            Me.Txt_FecVcto.Text = ""
            Me.Txt_FecVcto.CssClass = "clsDisabled"
            Me.Txt_FecVcto.ReadOnly = True

            Me.Txt_VctoReal.Text = ""
            Me.Txt_VctoReal.CssClass = "clsDisabled"
            Me.Txt_VctoReal.ReadOnly = True

            Me.Txt_PlazaDes.Text = ""
            Me.Txt_PlazaDes.CssClass = "clsDisabled"
            Me.Txt_PlazaDes.ReadOnly = True

            'RadioButton
            Me.Rb_Not.Enabled = False
            Me.Rb_Cob.Enabled = False
            Me.rb_cust.Enabled = False
            Me.rb_cust.SelectedValue = "N"
            Me.Dr_bco.Enabled = False
            Me.Dr_bco.CssClass = "clsDisabled"

            Me.dr_custodia.Enabled = False
            Me.dr_custodia.CssClass = "clsDisabled"

            Me.rb_respaldo.Enabled = False

            Me.Rb_Cob_Dct.Enabled = False

            Me.Rb_Obl.Enabled = False

            Me.txt_cta_cte.ReadOnly = True
            Me.txt_cta_cte.CssClass = "clsDisabled"
            Me.txt_cta_cte.Text = ""


        Else
            'Textos
            Me.Txt_NroDocto.Text = ""
            Me.Txt_NroDocto.CssClass = "clsMandatorio"
            Me.Txt_NroDocto.ReadOnly = False

            Me.Txt_Cuota.Text = ""
            Me.Txt_Cuota.CssClass = "clsDisabled"
            Me.Txt_Cuota.ReadOnly = True

            Me.Txt_MontoFinanciar0.Text = ""
            Me.Txt_MontoFinanciar0.CssClass = "clsMandatorio"
            Me.Txt_MontoFinanciar0.ReadOnly = False

            Me.Txt_FecEmision.Text = ""
            Me.Txt_FecEmision.CssClass = "clsMandatorio"
            Me.Txt_FecEmision.ReadOnly = False

            Me.Txt_FecVcto.Text = ""
            Me.Txt_FecVcto.CssClass = "clsMandatorio"
            Me.Txt_FecVcto.ReadOnly = False

            Me.Txt_VctoReal.Text = ""
            Me.Txt_VctoReal.CssClass = "clsDisabled"
            Me.Txt_VctoReal.ReadOnly = True

            Me.Txt_PlazaDes.Text = ""
            Me.Txt_PlazaDes.CssClass = "clsDisabled"
            Me.Txt_PlazaDes.ReadOnly = True


            Txt_FecEmision_CalendarExtender.Enabled = True
            'Txt_FecEmision_MaskedEditExtender.Enabled = True

            Txt_FecVcto_CalendarExtender.Enabled = True
            'Txt_FecVcto_MaskedEditExtender.Enabled = True

            Txt_valor_MaskedEditExtender.Enabled = True
            Txt_VctoReal_CalendarExtender.Enabled = True
            'Txt_VctoReal_MaskedEditExtender.Enabled = True

            Me.Dr_plaza.CssClass = "clstxt"
            Me.Dr_plaza.Enabled = True
            Me.Dr_plaza.SelectedIndex = 0

            Me.txt_cta_cte.Text = ""
            'RadioButton
            Me.Rb_Not.Enabled = True
            Me.Rb_Cob.Enabled = True
            Me.rb_cust.SelectedValue = "N"
            Me.rb_cust.Enabled = True
            Me.rb_respaldo.Enabled = True
            Me.Rb_Cob_Dct.Enabled = True
            Me.Rb_Obl.Enabled = True
            Me.txt_cta_cte.ReadOnly = False
            Me.txt_cta_cte.CssClass = "clsTxt"


            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then

                Me.Dr_bco.Enabled = True
                Me.Dr_bco.CssClass = "clsMandatorio"
                Me.Dr_plaza.CssClass = "clsMandatorio"

                Me.rb_cust.SelectedValue = "S"
                Me.rb_cust.Enabled = False
                Me.dr_custodia.SelectedValue = 0
                Me.dr_custodia.Enabled = True
                Me.dr_custodia.CssClass = "clsMandatorio"
                Me.dr_custodia.Visible = True

            End If

            Me.dr_custodia.SelectedValue = 0
            Me.dr_custodia.Enabled = True
            Me.dr_custodia.CssClass = "clsMandatorio"

        End If

        Lbl_Msj_Vto.Visible = False

    End Sub

    Private Sub HabilitaInhabilitaDatosDeudor(ByVal HI As String)
        If HI = "I" Then
            'Me.Txt_Rso_Deu.Text = ""
            'Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True
            '    Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            '   Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True
            '  Me.Txt_ApePaterno.Text = ""
            Me.Txt_ApePaterno.CssClass = "clsDisabled"
            Me.Txt_ApePaterno.ReadOnly = True
            ' Me.Txt_ApeMaterno.Text = ""
            Me.Txt_ApeMaterno.CssClass = "clsDisabled"
            Me.Txt_ApeMaterno.ReadOnly = True

            'DropList
            ' Me.DP_TipoDeudor.ClearSelection()
            Me.DP_TipoDeudor.Enabled = False
            Me.DP_TipoDeudor.CssClass = "clsDisabled"
            ' Me.DP_AbrRazSoc.ClearSelection()
            Me.DP_AbrRazSoc.Enabled = False
            Me.DP_AbrRazSoc.CssClass = "clsDisabled"
            ' Me.help.Visible = False
        ElseIf HI = "N" Then


            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.ReadOnly = False

            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.ReadOnly = False

            Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True

            Me.Txt_ApePaterno.Text = ""
            Me.Txt_ApePaterno.CssClass = "clsDisabled"
            Me.Txt_ApePaterno.ReadOnly = True

            Me.Txt_ApeMaterno.Text = ""
            Me.Txt_ApeMaterno.CssClass = "clsDisabled"
            Me.Txt_ApeMaterno.ReadOnly = True


            Me.Txt_ApePaterno.Text = ""
            Me.Txt_ApePaterno.CssClass = "clstxt"
            Me.Txt_ApePaterno.ReadOnly = False

            Me.Txt_ApeMaterno.Text = ""
            Me.Txt_ApeMaterno.CssClass = "clstxt"
            Me.Txt_ApeMaterno.ReadOnly = False

            'DropList
            Me.DP_TipoDeudor.ClearSelection()
            Me.DP_TipoDeudor.Enabled = True
            Me.DP_TipoDeudor.CssClass = "clstxt"
            Me.DP_AbrRazSoc.ClearSelection()
            Me.DP_AbrRazSoc.Enabled = True
            Me.DP_AbrRazSoc.CssClass = "clstxt"
            Me.Rb_Cob_Dct.Enabled = False
            Me.Rb_Obl.Enabled = False
            Me.txt_cta_cte.ReadOnly = True
            Me.txt_cta_cte.CssClass = "clsDisabled"

        Else

            'Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True

            'Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True

            Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Rso_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rso_Deu.ReadOnly = False


            Me.Txt_ApePaterno.Text = ""
            Me.Txt_ApePaterno.CssClass = "clstxt"
            Me.Txt_ApePaterno.ReadOnly = False

            Me.Txt_ApeMaterno.Text = ""
            Me.Txt_ApeMaterno.CssClass = "clstxt"
            Me.Txt_ApeMaterno.ReadOnly = False

            'DropList
            Me.DP_TipoDeudor.ClearSelection()
            Me.DP_TipoDeudor.Enabled = True
            Me.DP_TipoDeudor.CssClass = "clstxt"
            Me.DP_AbrRazSoc.ClearSelection()
            Me.DP_AbrRazSoc.Enabled = True
            Me.DP_AbrRazSoc.CssClass = "clstxt"

            Me.Rb_Cob_Dct.Enabled = False
            Me.Rb_Obl.Enabled = False
            Me.txt_cta_cte.ReadOnly = True
            Me.txt_cta_cte.CssClass = "clsDisabled"
            'Textos

        End If
    End Sub

    Private Function StrMsgValidacionCampos(ByVal IngresoDeudorDocto As String) As String

        Dim ValidaciondeCampos As Boolean

        ValidaciondeCampos = True
        StrMsgValidacionCampos = ""
        If IngresoDeudorDocto = "DEU" Then
            If Trim(Me.Txt_Rso_Deu.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar razón social de pagador", 3)
                Exit Function
            End If
            'Juridico
            If Me.DP_TipoDeudor.SelectedItem.Value = 1 And ValidaciondeCampos Then
                If Trim(Me.Txt_ApePaterno.Text) = "" Then
                    ValidaciondeCampos = False
                    msj.Mensaje(Me, "Exclamación", "Debe ingresar apellido paterno de pagador", 3)
                    Exit Function
                End If
                If Trim(Me.Txt_ApeMaterno.Text) = "" Then
                    ValidaciondeCampos = False

                    msj.Mensaje(Me, "Exclamación", "Debe ingresar apellido materno de pagador", 3)
                    Exit Function
                End If
            End If
        Else
            If Trim(Me.Txt_NroDocto.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar nro. de documento", 3)
                Exit Function
            End If
            'Cuota
            If Trim(Me.Txt_Cuota.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar nro. de cuota, si no tiene cuota ingrese un 0", 3)
                Exit Function
            End If
            'Monto Financiar
            If Trim(Me.Txt_MontoFinanciar0.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar monto a financiar de documento", 3)
                Exit Function
            End If
            'Fecha Emisión
            If Trim(Me.Txt_FecEmision.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar fecha de emisión de documento", 3)
                Exit Function
            End If
            'Fecha Vcto.
            If Trim(Me.Txt_FecVcto.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar fecha de vencimiento de documento", 3)
                Exit Function
            End If
            'Fecha Vcto. Real
            If Trim(Me.Txt_VctoReal.Text) = "" Then
                ValidaciondeCampos = False
                msj.Mensaje(Me, "Exclamación", "Debe ingresar fecha de vencimiento real de documento", 3)
                Exit Function
            End If

            If Not IsDate(Txt_FecEmision.Text) Then
                msj.Mensaje(Page, "Exclamación", "Fecha de emisión errónea", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Function
            End If

            If Not IsDate(Txt_FecVcto.Text) Then
                msj.Mensaje(Page, "Exclamación", "Fecha de vencimiento errónea", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Function
            End If

            If DateDiff(DateInterval.Day, CDate(Me.Txt_FecEmision.Text), CDate(Me.Txt_FecVcto.Text)) < 0 Then

                msj.Mensaje(Me, "Exclamación", "Fecha de Vencimiento no puede ser menor a la Fecha de Ingreso", 3)
                Exit Function
            End If

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then

                If Me.Dr_bco.SelectedValue = 0 Then

                    msj.Mensaje(Me, "Exclamación", "Debes seleccionar un banco para realizar el ingreso", 3)

                    Exit Function
                End If

                If Me.Dr_plaza.SelectedIndex = 0 Then
                    msj.Mensaje(Me, "Exclamación", "Debes seleccionar una plaza para realizar el ingreso", 3)
                    Exit Function

                End If

                If Me.txt_cta_cte.Text = "" Then
                    msj.Mensaje(Me, "Exclamación", "Debes digitar la cuenta corriente para realizar el ingreso", 3)
                    Exit Function

                End If



            End If

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0031 = 3 Then
                If Me.dr_custodia.SelectedValue = 0 Then
                    msj.Mensaje(Me, "Atención", "Debe seleccionar una custodia para el documento", ClsMensaje.TipoDeMensaje._Excepcion)
                    Exit Function
                End If

            End If

            'If Me.rb_respaldo.SelectedValue = "S" Then
            '    If Me.Dr_bco.SelectedValue = 0 Then
            '        msj.Mensaje(Me, "Exclamación", "Debes seleccionar una banco para realizar el ingreso", 3)
            '        Exit Function

            '    End If
            '    If Me.Dr_plaza.SelectedIndex = 0 Then
            '        msj.Mensaje(Me, "Exclamación", "Debes seleccionar una plaza para realizar el ingreso", 3)
            '        Exit Function

            '    End If

            '    If Me.txt_cta_cte.Text = "" Then
            '        msj.Mensaje(Me, "Exclamación", "Debes digitar la cuenta corriente para realizar el ingreso", 3)
            '        Exit Function

            '    End If

            'End If
            'Validción de Existencia de Documento y cuota (solo para Ingreso de Documento)
            If Val(Me.Txt_ItemDSI.Value) <= 0 Then
                'StrMsgValidacionCampos = vClsDSI.ValidaDoctoIng_RetornaCodFlujo(CLI.CLI_IDC, Coll_DEU(1).deu_ide, Me.Txt_NroDocto.Text, Me.Txt_Cuota.Text, Coll_OPE(CInt(Me.Txt_ItemOpe.value)).pnu_tip_doc, "E")
                If Trim(StrMsgValidacionCampos) <> "" Then
                    ValidaciondeCampos = False
                    Exit Function
                End If
            End If
        End If

    End Function

    Private Sub Guardar()

        If Val(Me.Txt_ItemDSI.Value) > 0 Then
            ActualizaDocumento()
        Else

            Dim deu As New deu_cls
            deu = CG.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

            'Datos Deudor                        
            If IsDBNull(deu) = False Then

                With deu
                    Me.Txt_Rut_Deu.Text = CLng(.deu_ide)
                    Me.Txt_Rso_Deu.Text = .deu_rso
                    Me.Txt_ApePaterno.Text = .deu_ape_ptn
                    Me.Txt_ApeMaterno.Text = .deu_ape_mtn
                    Me.DP_TipoDeudor.SelectedValue = CStr(.id_P_0044)

                    If IsNothing(.id_P_0063) Then
                        Me.DP_AbrRazSoc.SelectedValue = 0
                    Else
                        Me.DP_AbrRazSoc.SelectedValue = CStr(.id_P_0063)
                    End If

                End With

            End If

            If IsDBNull(deu) Then
                IngresaDeudor()
            Else

                deu = CG.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)
                If IsDBNull(deu) Then
                    With deu
                        Me.Txt_Rut_Deu.Text = CLng(.deu_ide)
                        Me.Txt_Rso_Deu.Text = .deu_rso
                        Me.Txt_ApePaterno.Text = .deu_ape_ptn
                        Me.Txt_ApeMaterno.Text = .deu_ape_mtn
                        Me.DP_TipoDeudor.SelectedValue = CStr(.id_P_0044)
                        Me.DP_AbrRazSoc.SelectedValue = CStr(.id_P_0063)
                    End With
                End If
            End If

            IngresaDocumento()

        End If


    End Sub

    Public Sub alinea_textos()
        Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_NroDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_MontoFinanciar0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Cuota.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_cta_cte.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Private Sub CargaDrop_Form()

        'Parámetro Tipo Deudor
        CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, DP_TipoDeudor)

        'Parámetro Abr. Ras. Soc
        CG.ParametrosDevuelve(TablaParametro.RazonesSociales, True, DP_AbrRazSoc)

        'Parametro Plaza

        CG.ParametrosAlfanumericoDevuelveCodigos(TablaAlfanumerico.Plazas, True, Me.Dr_plaza)

        'Bancos

        CG.BancosDevuelveTodos(Dr_bco)

        'Custodia

        CG.ParametrosDevuelve(TablaParametro.Custodia, True, dr_custodia)

    End Sub

    Private Sub RetonaDatosObj_DSI(ByVal vItem As Int16)
        ' Retornar Campos DSI
        With Coll_DSI(vItem)
            'Datos deudor relacionado al Documento
            'Llena Objeto DEU 

            Dim Deu As deu_cls

            If Coll_DSI.Count > 0 Then
                Deu = CG.DeudorDevuelvePorRut(.deu_ide)
            Else
                Deu = CG.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)
            End If


            'Datos Deudor
            With Deu
                'Datos Deudor
                HabilitaInhabilitaDatosDeudor("I")

                'Textos
                Me.Txt_Rut_Deu.Text = CLng(.deu_ide)
                Me.Txt_Dig_Deu.Text = RG.Vrut(Me.Txt_Rut_Deu.Text)
                Me.Txt_Rso_Deu.Text = .deu_rso
                Me.Txt_ApePaterno.Text = .deu_ape_ptn
                Me.Txt_ApeMaterno.Text = .deu_ape_mtn

                'DropList
                Me.DP_TipoDeudor.SelectedValue = .id_P_0044
                'Si Tipo Deudor es Juridico esconde campos apellidos Paterno y Materno
                If .id_P_0044 <> 1 Then
                    Me.Lab_Paterno.Visible = False
                    Me.lab_materno.Visible = False
                    Me.Txt_ApeMaterno.Visible = False
                    Me.Txt_ApePaterno.Visible = False
                Else
                    Me.Lab_Paterno.Visible = True
                    Me.lab_materno.Visible = True
                    Me.Txt_ApeMaterno.Visible = True
                    Me.Txt_ApePaterno.Visible = True
                End If
                If IsNothing(.id_P_0063) Then
                    Me.DP_AbrRazSoc.SelectedValue = 0
                Else
                    Me.DP_AbrRazSoc.SelectedValue = .id_P_0063
                End If

            End With
            rb_respaldo.SelectedValue = .dsi_rsp
            'Datos Documento 
            HabilitaInhabilitaDatosDocumento("H")
            HF_FacVctoCal.Value = .dsi_fev_cal
            'Textos
            Me.Txt_NroDocto.Text = .dsi_num
            Me.Txt_NroDocto.CssClass = "clsDisabled"
            Me.Txt_NroDocto.ReadOnly = True
            Me.Txt_Cuota.Text = .dsi_flj_num
            Me.Txt_Cuota.CssClass = "clsDisabled"
            Me.Txt_Cuota.ReadOnly = True
            Me.Txt_MontoFinanciar0.Text = Format(.dsi_mto, RG.DevuelveFormatoMoneda(coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023))

            If coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 2 Then
                Txt_valor_MaskedEditExtender.Mask = "999,999,999.9999"
            ElseIf coll_ope.Item(Val(Me.Txt_ItemOpe.Value)).id_p_0023 = 3 Then
                Txt_valor_MaskedEditExtender.Mask = "999,999,999.99"
            End If

            Me.Txt_FecEmision.Text = Format(CDate(.dsi_fec_emi), "dd/MM/yyyy")
            Me.Txt_FecVcto.Text = Format(CDate(.dsi_fev), "dd/MM/yyyy")
            Me.Txt_VctoReal.Text = Format(CDate(.dsi_fev_rea), "dd/MM/yyyy")
            Me.Txt_VctoReal.CssClass = "clsDisabled"
            Me.Txt_VctoReal.ReadOnly = True

            'Me.Txt_Cal_oto.Text = .cal_oto_gam

            Dim i As Integer

            For i = 0 To Dr_plaza.Items.Count - 1
                If Trim(Me.Dr_plaza.Items(i).Text) = Trim(.id_pl_000047) Then
                    Me.Dr_plaza.SelectedIndex = i
                End If

            Next

            Me.Txt_PlazaDes.Text = Me.Dr_plaza.SelectedValue


            'RadioButton

            Rb_Not.SelectedValue = .dsi_ntf
            Rb_Cob.SelectedValue = .dsi_cbz_son
            rb_cust.SelectedValue = .dsi_env_bci

            If rb_cust.SelectedValue = "S" Then
                Me.dr_custodia.Visible = True
                Me.dr_custodia.CssClass = "clsMandatorio"
            End If

            If Not IsNothing(.id_p_0112) Then
                Me.dr_custodia.SelectedValue = .id_p_0112
            End If

            Me.txt_cta_cte.Text = .cta_cte


        End With

    End Sub

#End Region

    
End Class
