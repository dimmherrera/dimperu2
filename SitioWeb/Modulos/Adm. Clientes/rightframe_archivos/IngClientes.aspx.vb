Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb

Partial Class IngCliente
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES PRIVADAS"

    Dim Sesion As New ClsSession.ClsSession
    Dim Caption As String = "Mantención de Cliente"
    Dim CLSCLI As New ClaseClientes
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim RW As New FuncionesGenerales.RutinasWeb

#End Region

#Region "Carga de Pagina"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Administracion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1

            CargaDrop()
            Sesion.TipoDeContacto = 1
            Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Cod_Pos.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Anx_Bco.Attributes.Add("Style", "TEXT-ALIGN: right")

            Dim agt As New Perfiles.Cls_Principal

            Dim spread_moras As ArrayList = agt.ObjetoRetornaPuertasHabitadas(20, 20000101, Usr)
            Dim moras As Boolean = False

            'For Each MenuItem In spread_moras

            '    If MenuItem = "20170101" Then
            '        moras = True
            '        Exit For
            '    End If

            'Next

            If moras Then
                Txt_Spr_Mor.ReadOnly = False
                Txt_Spr_Mor.CssClass = "clsMandatorio"
            Else
                Txt_Spr_Mor.ReadOnly = True
                Txt_Spr_Mor.CssClass = "clsDisabled"
            End If

            If Trim(Request.QueryString("id")) <> "" Then
                DP_TipoIdentificacion.CssClass = "clsDisabled"
                DP_TipoCliente.CssClass = "clsDisabled"
                Txt_Rut.CssClass = "clsDisabled"
                Txt_nro_cli.CssClass = "clsDisabled"
                DP_Corasu.CssClass = "clsDisabled"

                DP_TipoIdentificacion.Enabled = False
                DP_TipoCliente.Enabled = False
                Txt_Rut.ReadOnly = True
                Txt_nro_cli.ReadOnly = True
                DP_Corasu.Enabled = False



                SW.Value = "UPDATE"
                Sesion.RutCli = CLng(Trim(Request.QueryString("id")))
                TraeCliente()

                If DP_Estado.SelectedValue = 5 Then 'Clientes.id_P_008 = 5 Then
                    'valida_cliente = "¡ Cliente Vetado !"
                    Msj.Mensaje(Me.Page, Caption, "Cliente Vetado !", TipoDeMensaje._Exclamacion)
                    'Exit Sub
                ElseIf DP_Estado.SelectedValue = 2 Then 'Clientes.id_P_008 = 2 Then
                    'valida_cliente = "¡ Cliente Caducado !"
                    Msj.Mensaje(Me.Page, Caption, "Cliente Caducado !", TipoDeMensaje._Exclamacion)
                ElseIf DP_Estado.SelectedValue = 3 Then 'Clientes.id_P_008 = 3 Then
                    'valida_cliente = "¡ Cliente Deshabilitado !"
                    Msj.Mensaje(Me.Page, Caption, "Cliente Deshabilitado !", TipoDeMensaje._Exclamacion)
                    'Exit Function
                ElseIf DP_Estado.SelectedValue = 4 Then 'Clientes.id_P_008 = 4 Then
                    'valida_cliente = "¡ Cliente Con Problemas !"
                    Msj.Mensaje(Me.Page, Caption, "Cliente Con Problemas !", TipoDeMensaje._Exclamacion)
                    'Exit Function
                End If
                If IsNothing(DP_Estado.SelectedValue) Then
                    'valida_cliente = "¡ Cliente no existe !"
                    Msj.Mensaje(Me.Page, Caption, "Cliente no existe !", TipoDeMensaje._Exclamacion)
                    'Exit Function
                End If

            Else
                SW.Value = "INSERT"
            End If

            DP_TipoCliente.Focus()

        End If

    End Sub

#End Region

#Region "DROPDOWNLIST"

    Protected Sub DP_Giro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Giro.SelectedIndexChanged
        If DP_Giro.SelectedIndex <> 0 Then


            Dim Drop As New ConsultasGenerales
            Drop.ActividadEconomicaDevuelve(DP_Giro.SelectedValue, DP_ActEco)

            If DP_ActEco.Items.Count > 0 Then
                DP_ActEco.SelectedIndex = 1
                'DP_ActEco.CssClass = "clsMandatorio"
                'DP_ActEco.Enabled = True
            End If
        End If
    End Sub

    Protected Sub DP_TipoIdentificacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoIdentificacion.SelectedIndexChanged
        Txt_Rut.Text = ""
        Txt_Dig.Text = ""
    End Sub

    Protected Sub DP_TipoCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoCliente.SelectedIndexChanged
        If DP_TipoCliente.SelectedIndex <> 0 Then
            Dim coll_tipo As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoCliente, _
                                                                         DP_TipoCliente.SelectedValue)
            If coll_tipo(1).pnu_atr_001 = Nothing Then
                Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                MultiView2.ActiveViewIndex = 1
            Else
                If coll_tipo(1).pnu_atr_001.ToString().Equals("F") Then
                    Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                    MultiView2.ActiveViewIndex = 0
                End If
            End If
            Lbl_NIT.Focus()
        End If


    End Sub



    Protected Sub DP_Sucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Sucursal.SelectedIndexChanged

        Try

            If DP_Sucursal.SelectedIndex > 0 Then

                Dim suc As suc_cls = CG.SucursalDevuelve(DP_Sucursal.SelectedValue)

                If suc.SUC_DUG <> Nothing Then

                    Dim zon As suc_cls = CG.ZonaBancaTerritorialDevuelve(30, suc.SUC_DUG)
                    Txt_Zona.Text = zon.suc_cod_ftg & " " & zon.suc_nom

                    If suc.SUC_DUG <> Nothing Then
                        Dim ter As suc_cls = CG.ZonaBancaTerritorialDevuelve(50, zon.SUC_TERR_ITO)
                        Txt_Terrotorial.Text = ter.suc_cod_ftg & " " & ter.suc_nom
                        If suc.SUC_DUG <> Nothing Then
                            Dim ban As suc_cls = CG.ZonaBancaTerritorialDevuelve(60, ter.suc_area_ope)
                            Txt_Banca.Text = ban.suc_cod_ftg & " " & ban.suc_nom

                        End If
                    End If

                End If

            Else
                Txt_Zona.Text = ""
                Txt_Terrotorial.Text = ""
                Txt_Banca.Text = ""
            End If

            'Txt_Cod_Ges.Text = ""
            'Txt_Ema_Ges.Text = ""

            'Ejecutivo
            CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 15)
            If DP_Sucursal.SelectedValue = "0" Then
                DP_Ejecutivo.Items.Clear()
            End If

            'CG.GestorDevuelve(DP_Sucursal.SelectedValue, DP_Gestor)
            'CG.GestorNegocioDevuelve(DP_Sucursal.SelectedValue, DP_GestorNeg)

            If IsNothing(DP_Ejecutivo.DataSource) Then
                Msj.Mensaje(Page, Caption, "No existen ejecutivos para esta sucursal", ClsMensaje.TipoDeMensaje._Exclamacion)
                DP_Sucursal.ClearSelection()
                DP_Ejecutivo.Items.Clear()
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_Ciudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Ciudad.SelectedIndexChanged
        Try
            'Comuna
            CG.ComunaDevuelve(CInt(DP_Ciudad.SelectedItem.Value), True, Me.DP_Comuna)
            DP_Comuna.ClearSelection()
            ' If Err.CodigoError = 99 Then Msj.Mensaje(Me.Page,Err.MsgError, TipoMsg.Exclamacion)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CargaDrop()

        Dim Drop As New ConsultasGenerales

        Try

            'Tipo de identificacion
            Drop.ParametrosDevuelve(TablaParametro.TipoIdentificacion, True, DP_TipoIdentificacion)

            'Tipo de Cliente 
            Drop.ParametrosDevuelve(TablaParametro.TipoCliente, True, DP_TipoCliente)

            'Sexo
            Drop.SexoDevuelve(Dp_Sexo)

            'Sucursales o oficinas
            Drop.SucursalesDevuelve(CodEje, True, DP_Sucursal)

            'Ejecutivos
            CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 15)

            'Actividad Economica
            Drop.ParametrosDevuelve(TablaParametro.ActividadEconomica, True, Me.DP_ActEco)

            'CIIU
            Drop.CIIUDevuelve(DP_Giro)

            'Sucursal Banco
            '?
            Drop.SucursalesDevuelveTodas(True, DP_Suc_Bco)

            'Segmento
            Drop.ParametrosDevuelve(TablaParametro.Segmentos, True, Me.DP_Segmento)

            'Estado
            Drop.ParametrosDevuelve(TablaParametro.EstadoCliente, True, Me.DP_Estado)

            'Modo de Operacion
            Drop.ParametrosDevuelve(TablaParametro.ModoOperacion, True, Me.DP_ModoOpe)

            'Cartera
            Drop.CarteraClienteDevuelve(1, DP_EstCartera, 1)

            'Tipo de Informacion
            Drop.ParametrosDevuelve(TablaParametro.TipoEnvioInformacion, True, Me.DP_EstadoInf)

            'Categoria de Riesgo
            Drop.ParametrosDevuelve(TablaParametro.CategoriaRiesgo, True, Me.DP_CateRiesgo)

            'Forma de Envio
            Drop.ParametrosDevuelve(TablaParametro.FormaEnvio, True, Me.DP_FormaEnvio)

            'Banca Cliente
            'Drop.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.BancaCliente, True, DP_BancaCli)
            'Cuidad
            'CG.CiudadDevuelve(True, DP_Ciudad)

            'Departamentos
            Drop.ParametrosDevuelve(TablaParametro.Region, True, DP_Depto)

            'Clasificacion
            Drop.ParametrosDevuelve(TablaParametro.ClasificacionCliente, True, Me.DP_Clacificacion)

            'CORASU
            Drop.ParametrosDevuelve(TablaParametro.CORASU, True, Me.DP_Corasu)

            'Canal
            'Drop.CanalDevuelve(Me.DP_Canal)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_Comuna_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Comuna.SelectedIndexChanged
        If DP_Comuna.SelectedIndex <> 0 Then
            Txt_Dom_Par.ReadOnly = False
        End If
    End Sub

    Protected Sub DP_Depto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Depto.SelectedIndexChanged

        Dim depto As Integer

        If DP_Depto.SelectedIndex <> 0 Then

            depto = DP_Depto.SelectedValue
            CG.MunicipioDevuelve(depto, True, DP_Ciudad)
            DP_Ciudad.ClearSelection()
            DP_Comuna.ClearSelection()
            'CG.CiudadDevuelve(True, DP_Ciudad)
        End If
    End Sub

    Protected Sub DP_TipoTasa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoTasa.SelectedIndexChanged

        Select Case DP_TipoTasa.SelectedValue
            Case "F"
                Lbl_TipoTasa.Text = "Tasa Fija E.A."
            Case "V"
                Lbl_TipoTasa.Text = "Spread E.A."
        End Select

    End Sub

    'Protected Sub DP_Canal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Canal.SelectedIndexChanged
    '    'Canal
    '    CG.SubCanalDevuelve(Me.DP_Canal.SelectedValue, DP_SubCanal)

    'End Sub

    Protected Sub DP_Gestor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Gestor.SelectedIndexChanged

        'Gestores
        Txt_Cod_Ges.Text = DP_Gestor.SelectedValue

    End Sub

    Protected Sub DP_GestorNeg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_GestorNeg.SelectedIndexChanged

        'Gestore
        Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(DP_GestorNeg.SelectedValue).eje_mail
        Txt_Ema_Ges.ReadOnly = False
        Txt_Ema_Ges.CssClass = "clsMandatorio"

    End Sub


#End Region

#Region "SUB Y FUNCTION"

    Private Sub TraeCliente()

        Try

            Dim RG As New FuncionesGenerales.FComunes
            Dim Drop As New ConsultasGenerales
            Dim CG As New ConsultasGenerales
            Dim Cliente As cli_cls

            Cliente = CLSCLI.ClienteDevuelvePorRut(Sesion.RutCli)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Sub
            Else

                With Cliente

                    'DATOS PRINCIPALES
                    Txt_Rut.CssClass = "clsDisabled"
                    Txt_Dig.CssClass = "clsDisabled"
                    Txt_nro_cli.CssClass = "clsDisabled"
                    'DP_TipoCliente.CssClass = "clsDisabled"
                    'DP_TipoIdentificacion.CssClass = "clsDisabled"

                    Txt_Rut.ReadOnly = True
                    Txt_Dig.ReadOnly = True
                    Txt_nro_cli.ReadOnly = True
                    'DP_TipoCliente.Enabled = False
                    'DP_TipoIdentificacion.Enabled = False

                    Txt_Rut.Text = RG.FormatoMiles(CLng(.cli_idc))
                    Txt_Dig.Text = .cli_dig_ito
                    Txt_nro_cli.Text = .cli_nro_cli

                    DP_TipoCliente.ClearSelection()
                    DP_TipoCliente.Items.FindByValue(.id_P_0044).Selected = True

                    DP_TipoIdentificacion.ClearSelection()
                    DP_TipoIdentificacion.Items.FindByValue(.id_P_0119).Selected = True

                    Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()

                    DP_Corasu.ClearSelection()
                    DP_Corasu.Items.FindByValue(.id_P_0313).Selected = True

                    Dim coll_nit_ciudadania As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoCliente, _
                                                                                           .id_P_0044)

                    If coll_nit_ciudadania(1).pnu_atr_001 = Nothing Then
                        'JURIDICO
                        Me.Txt_Raz_Soc.Text = .cli_rso.Trim
                        If Not IsNothing(.cli_fec_nac) Then
                            Me.Txt_Fec_Con.Text = FC.FUNFecReg(.cli_fec_nac)
                        End If
                        MultiView2.ActiveViewIndex = 1

                    Else
                        If coll_nit_ciudadania(1).pnu_atr_001.ToString().Trim().ToUpper() = "F" Then

                            'NATURAL
                            Me.Txt_Nom_Bre.Text = .cli_rso.Trim
                            Me.Txt_Ape_Pat.Text = .cli_ape_ptn.Trim
                            Me.Txt_Ape_Mat.Text = .cli_ape_mtn.Trim

                            Dp_Sexo.ClearSelection()
                            If Not IsNothing(.cli_sex) Then
                                Me.Dp_Sexo.Items.FindByValue(.cli_sex).Selected = True
                            End If

                            If Not IsNothing(.cli_fec_nac) Then
                                Me.Txt_Fec_Nac.Text = FC.FUNFecReg(.cli_fec_nac)
                            End If

                            MultiView2.ActiveViewIndex = 0
                        End If
                    End If

                    'Ciudad y Comuna
                    If Not IsNothing(.id_cmn) Then

                        'Departamento
                        Me.DP_Depto.ClearSelection()
                        DP_Depto.Items.FindByValue(.cmn_cls.ciu_cls.id_p_001).Selected = True

                        'Cuidad
                        Drop.MunicipioDevuelve(CInt(DP_Depto.SelectedValue), True, DP_Ciudad)
                        Me.DP_Ciudad.ClearSelection()
                        DP_Ciudad.Items.FindByValue(.cmn_cls.id_ciu).Selected = True
                        Drop.ComunaDevuelve(DP_Ciudad.SelectedItem.Value, True, Me.DP_Comuna)

                        'Comuna
                        Me.DP_Comuna.ClearSelection()
                        DP_Comuna.Items.FindByValue(.id_cmn).Selected = True

                    End If

                    'ANTECEDENTES GENERALES
                    If Trim(.cli_dml) = "" Then
                        Txt_Dom_Par.Text = ""
                    Else
                        Txt_Dom_Par.Text = .cli_dml
                    End If

                    DP_Sucursal.ClearSelection()

                    If Not IsNothing(.id_suc) Or .id_suc > 0 Then

                        DP_Sucursal.Items.FindByValue(.id_suc).Selected = True

                        'Ejecutivo (gerente factoring)
                        Drop.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 15)
                        'Dp_gestornegocio esta sin uso debido a que no esta en la base de datos
                        'gestor
                        'Drop.GestorDevuelve(.id_suc, DP_Gestor)
                        'CG.GestorNegocioDevuelve(.id_suc, DP_GestorNeg)

                        'Se comento porque al momento de ver el cliente lanzaba error, se debe a que estos datos no estan en la bd
                        If DP_Sucursal.SelectedIndex > 0 Then

                            Dim suc As suc_cls = CG.SucursalDevuelve(DP_Sucursal.SelectedValue)

                            'If Not IsNothing(suc.SUC_DUG) And suc.SUC_DUG.Trim <> "0" Then
                            'Dim zon As suc_cls = CG.ZonaBancaTerritorialDevuelve(30, suc.SUC_DUG)
                            'If Not IsNothing(zon) Then
                            '    Txt_Zona.Text = zon.suc_cod_ftg & " " & zon.suc_nom
                            '    Dim ter As suc_cls = CG.ZonaBancaTerritorialDevuelve(50, zon.SUC_TERR_ITO)
                            'If Not IsNothing(ter) Then
                            '    Txt_Terrotorial.Text = ter.suc_cod_ftg & " " & ter.suc_nom
                            '    Dim ban As suc_cls = CG.ZonaBancaTerritorialDevuelve(60, ter.suc_area_ope)
                            'If Not IsNothing(ban) Then
                            '    Txt_Banca.Text = ban.suc_cod_ftg & " " & ban.suc_nom
                            '    'End If
                            'End If
                            'End If
                            'End If
                        End If
                    End If

                    If Trim(.cli_ema) = "" Then
                        Txt_Mai.Text = ""
                    Else
                        Me.Txt_Mai.Text = .cli_ema
                    End If

                    Me.Txt_Cod_Pos.Text = CStr(.cli_cod_pot)

                    'Segmento
                    Me.DP_Segmento.ClearSelection()
                    If Not IsNothing(.id_P_0076) Then
                        DP_Segmento.Items.FindByValue(.id_P_0076).Selected = True
                    End If

                    '----------------------------------------------------------------------------------------------------------------
                    'Versión: 12122013.V1
                    '----------------------------------------------------------------------------------------------------------------
                    'Giro CIIU
                    Drop.CIIUDevuelve(DP_Giro)
                    If Not IsNothing(.id_PL_000006) Then
                        Me.DP_Giro.ClearSelection()
                        DP_Giro.Enabled = True
                        DP_Giro.CssClass = "clsMandatorio"
                        BuscaCombo(Me.DP_Giro, CStr(.id_PL_000006.Trim()))
                    End If

                    'Actividad Economica
                    If Not IsNothing(.id_P_0064) Then
                        Me.DP_ActEco.ClearSelection()
                        BuscaCombo(Me.DP_ActEco, CStr(.id_P_0064))
                    End If

                    'ANTECEDENTES CON BANCO
                    Me.DP_Suc_Bco.ClearSelection()
                    Me.DP_Suc_Bco.SelectedValue = .cli_eje_ofc
                    'Me.Txt_Suc_Bco.Text = .cli_eje_ofc'
                    'Me.Txt_Eje_Bco.Text = .cli_eje_bci

                    If Not IsNothing(.cli_eje_anx) Then
                        Me.Txt_Anx_Bco.Text = .cli_eje_anx
                    End If

                    'CANAL
                    'If Not IsNothing(.CLI_CAN_AL) Then
                    '    Me.DP_Canal.ClearSelection()
                    '    BuscaCombo(Me.DP_Canal, CStr(.CLI_CAN_AL))
                    '    Drop.SubCanalDevuelve(.CLI_CAN_AL, DP_SubCanal)
                    '    If Not IsNothing(.CLI_SUB_CAN_AL) Then
                    '        Me.DP_SubCanal.ClearSelection()
                    '        BuscaCombo(Me.DP_SubCanal, CStr(.CLI_SUB_CAN_AL))
                    '    End If
                    'End If

                    'codigo gestor oficina
                    'If Not IsNothing(.cli_gest_cod) And DP_Gestor.Items.Count > 0 Then
                    'Me.DP_Gestor.ClearSelection()
                    'BuscaCombo(Me.DP_Gestor, .cli_gest_cod)
                    'Txt_Cod_Ges.Text = .cli_gest_cod
                    'End If

                    'codigo gestor negocio
                    'If Not IsNothing(.cli_gest_cod) And DP_Gestor.Items.Count > 0 Then
                    'Me.DP_GestorNeg.ClearSelection()
                    'BuscaCombo(Me.DP_GestorNeg, .cli_gest_cod)
                    'Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(.cli_gest_cod).eje_mail
                    'Txt_Ema_Ges.ReadOnly = False
                    'Txt_Ema_Ges.CssClass = "clsMandatorio"
                    'End If

                    '----------------------------------------------------------------------------------------------------------------
                    'ANTECEDENTES CON FACTORING
                    'Ejecutivo
                    Try
                        If Not IsNothing(.id_eje_cod_eje) Then
                            Me.DP_Ejecutivo.ClearSelection()
                            If DP_Ejecutivo.Items.Count > 0 Then
                                DP_Ejecutivo.Items.FindByValue(.id_eje_cod_eje).Selected = True
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    'Modo de Operacion
                    If Not IsNothing(.id_P_007) Then
                        Me.DP_ModoOpe.ClearSelection()
                        DP_ModoOpe.Items.FindByValue(.id_P_007).Selected = True
                    End If

                    'Sucursal Banco DP
                    If Not IsNothing(.id_suc) Then
                        Me.DP_Suc_Bco.ClearSelection()
                        DP_Suc_Bco.Items.FindByValue(.id_suc).Selected = True
                    End If

                    'Cartera Cliente
                    If Not IsNothing(.id_crt) Then
                        Me.DP_EstCartera.ClearSelection()
                        DP_EstCartera.Items.FindByValue(.id_crt).Selected = True
                    End If


                    'Forma de Envio
                    If Not IsNothing(.id_P_0067) Then
                        Me.DP_EstadoInf.ClearSelection()
                        DP_EstadoInf.Items.FindByValue(.id_P_0067).Selected = True
                    End If

                    'Me.DP_TipoInf.ClearSelection()
                    'BuscaCombo(Me.DP_TipoInf, CStr(CInt(.PNU_TIP_ENV_INF)))
                    'Me.Txt_Bie_Ser.Text = .BIENES

                    If Not IsNothing(.cli_fec_cre) Then
                        Me.Txt_Fec_Cre.Text = FC.FUNFecReg(.cli_fec_cre)
                    Else
                        Me.Txt_Fec_Cre.Text = Date.Now
                    End If

                    If Not IsNothing(.cli_fec_ini_ope) Then
                        Me.Txt_Fec_Ope.Text = FC.FUNFecReg(.cli_fec_ini_ope)
                    Else
                        Me.Txt_Fec_Ope.Text = "01-01-1900"
                    End If

                    'Estado Cliente
                    If Not IsNothing(.id_P_008) Then
                        Me.DP_Estado.ClearSelection()
                        DP_Estado.Items.FindByValue(.id_P_008).Selected = True
                    End If

                    'Categori de Riesgo
                    If Not IsNothing(.id_P_0058) Then
                        Me.DP_CateRiesgo.ClearSelection()
                        DP_CateRiesgo.Items.FindByValue(.id_P_0058).Selected = True
                    End If

                    'Forma de Envio
                    If Not IsNothing(.id_P_0068) Then
                        Me.DP_FormaEnvio.ClearSelection()
                        DP_FormaEnvio.Items.FindByValue(.id_P_0068).Selected = True
                    End If

                    If Not IsNothing(.cli_tas_mor) Then
                        Me.Txt_Spr_Mor.Text = CStr(CDec(.cli_tas_mor))
                    Else
                        Me.Txt_Spr_Mor.Text = 0
                    End If

                    If Not IsNothing(.cli_spr_ead_col) Then
                        Me.Txt_Spr_Col.Text = CStr(CDec(.cli_spr_ead_col))
                    Else
                        Me.Txt_Spr_Col.Text = 0
                    End If

                    If Not IsNothing(.cli_cob_ant) Then

                        If CStr(.cli_cob_ant) = "S" Then
                            CB_CobranzaAnt.Checked = True
                        ElseIf CStr(.cli_cob_ant) = "N" Then
                            CB_CobranzaAnt.Checked = False
                        End If

                    End If

                    'Clasificacion Cliente
                    If Not IsNothing(.id_p_0118) Then
                        Me.DP_Clacificacion.ClearSelection()
                        DP_Clacificacion.Items.FindByValue(.id_p_0118).Selected = True
                    End If

                    'Dias base de Mora (jlagos 21-12-2012)
                    If Not IsNothing(.cli_dia_bas) Then
                        Me.RB_BaseDias.ClearSelection()
                        RB_BaseDias.Items.FindByValue(.cli_dia_bas).Selected = True
                    End If

                    DP_TipoTasa.ClearSelection()
                    DP_TipoTasa.SelectedValue = .CLI_TIP_TAS

                    IB_Contacto.Enabled = True
                    IB_Bancos.Enabled = True
                    IB_Empresa.Enabled = True
                    IB_Organigrama.Enabled = True

                End With
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function ValidaCampos() As Boolean
        'se comento debido a que no se usan (dp_gestor, dp_canal) 
        Try

            Dim FG As New FuncionesGenerales.FComunes
            Dim localDate = DateTime.Today.ToString("dd/MM/yyyy")
            Dim EdadM As Integer
            Dim Edad As Integer
            Edad = DateDiff("yyyy", CDate(Txt_Fec_Nac.Text), localDate)
            EdadM = Format(Edad)

            If CInt(DP_TipoIdentificacion.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Tipo de Identificación", TipoDeMensaje._Exclamacion)
                Return True
            Else
                If Txt_Rut.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese Identificación", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                If Txt_Dig.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese Dígito", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                If Not Txt_Dig.Enabled Then
                    If Txt_Dig.Text.Trim().ToUpper() = FC.Vrut(Txt_Rut.Text).Trim().ToUpper() Then
                        Msj.Mensaje(Me.Page, Caption, "Dígito Incorrecto", TipoDeMensaje._Exclamacion)
                        Return True
                    End If
                End If

            End If

            If CInt(DP_TipoCliente.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Tipo de Cliente", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_Corasu.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione CORASU", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If Txt_nro_cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Nro Cliente", TipoDeMensaje._Exclamacion)
                Return True
            End If

            'Natural
            If CInt(DP_TipoCliente.SelectedValue) = 1 Or CInt(DP_TipoCliente.SelectedValue) = 12 Then

                If Txt_Nom_Bre.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese Nombre del Cliente", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                If Txt_Ape_Pat.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese Apellido Paterno", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                If Txt_Ape_Mat.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese Apellido Materno", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                If CInt(Dp_Sexo.SelectedIndex) = 0 Then
                    Msj.Mensaje(Me.Page, Caption, "Seleccione Sexo", TipoDeMensaje._Exclamacion)
                    Return True
                End If

                'Se creo validacion para lanzar error al ser menor a 18 y si es mayor a la fecha actual
                If Txt_Fec_Nac.Text <> "" And Txt_Fec_Nac.Text <> "__/__/____" Then
                    If Not IsDate(Txt_Fec_Nac.Text) Then
                        Msj.Mensaje(Page, Caption, "Fecha de nacimiento incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Return True
                    Else
                        If Convert.ToDateTime(Txt_Fec_Nac.Text) > Convert.ToDateTime(localDate) Then
                            Msj.Mensaje(Page, Caption, "Fecha de nacimiento incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Return True
                        Else
                            If EdadM < 18 Then
                                Msj.Mensaje(Page, Caption, "Hay que ser Mayor a 18 Años", ClsMensaje.TipoDeMensaje._Exclamacion)
                                Return True
                            End If
                        End If
                    End If

                    'Juridico
                Else
                    If Txt_Raz_Soc.Text = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Ingrese Razon Social de la empresa", TipoDeMensaje._Exclamacion)
                        Return True
                    End If

                    If Txt_Fec_Con.Text <> "" And Txt_Fec_Con.Text <> "__/__/____" Then
                        If Not IsDate(Txt_Fec_Con.Text) Then
                            Msj.Mensaje(Page, Caption, "Fecha constitución incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Return True
                        End If
                    End If
                End If
            End If

            If CInt(DP_ActEco.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione la Actividad Economica", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_Giro.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione CIIU", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_Suc_Bco.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Sucursal", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_Ejecutivo.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Ejecutivo", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_ModoOpe.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Modo de Operación", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_EstCartera.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Estado de Cartera", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If CInt(DP_Estado.SelectedValue) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Estado del Cliente", TipoDeMensaje._Exclamacion)
                Return True
            End If

            ' If CInt(DP_Gestor.SelectedValue) = 0 Then
            'Msj.Mensaje(Me.Page, Caption, "Seleccione Gestor Factoring del Cliente", TipoDeMensaje._Exclamacion)
            'Return True
            'End If

            'If CInt(DP_Canal.SelectedValue) = 0 Then
            'Msj.Mensaje(Me.Page, Caption, "Seleccione Canal del Cliente", TipoDeMensaje._Exclamacion)
            'Return True
            'End If

            'If CInt(DP_SubCanal.SelectedValue) = 0 Then
            'Msj.Mensaje(Me.Page, Caption, "Seleccione Sub Canal del Cliente", TipoDeMensaje._Exclamacion)
            'Return True
            'End If

            'Valida tasa mora no sea mayor a la maxima legal
            If (CDec(Txt_Spr_Mor.Text.Replace(".", ",")) > CG.TmcDevuelveActivaDevuelve()) Then
                Msj.Mensaje(Me.Page, Caption, "Tasa de Mora no puede ser mayor a la tasa máxima legal " & CG.TmcDevuelveActivaDevuelve(), TipoDeMensaje._Exclamacion)
                Return True
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Function CargaCliente() As cli_cls
        'Se comento Dp_canal, dp_gestorneg debido a que no estan en la base de datos 
        Try

            Dim CLI As New cli_cls

            With CLI
                '**************************************************************************************************************************
                'DATOS PRINCIPALES
                .id_P_0119 = DP_TipoIdentificacion.SelectedValue
                .cli_idc = Format(Val(Replace(Txt_Rut.Text, ".", "")), Var.FMT_RUT)
                .cli_dig_ito = Txt_Dig.Text.Trim
                .cli_nro_cli = Txt_nro_cli.Text.Trim
                .id_P_0044 = CInt(DP_TipoCliente.SelectedItem.Value) 'Tipo de Cliente
                .id_P_0313 = CInt(DP_Corasu.SelectedItem.Value) 'CORASU

                If CInt(DP_TipoCliente.SelectedItem.Value) = 1 Then
                    'NATURAL
                    .cli_rso = UCase(Me.Txt_Nom_Bre.Text)
                    .cli_ape_ptn = UCase(Me.Txt_Ape_Pat.Text)
                    .cli_ape_mtn = UCase(Me.Txt_Ape_Mat.Text)
                    .cli_sex = Dp_Sexo.SelectedItem.Value

                    'Valida que la haya ingresado la Fecha de Nacimiento
                    If Txt_Fec_Nac.Text = "" Then
                        .cli_fec_nac = CDate("01-01-1900")
                    Else
                        .cli_fec_nac = CDate(Me.Txt_Fec_Nac.Text)
                    End If

                Else
                    'JURIDICO O GUBERNAMENTAL
                    .cli_rso = Trim(UCase(Me.Txt_Raz_Soc.Text))

                    'Valida que la haya ingresado la Fecha de Constitucion
                    If Txt_Fec_Con.Text = "" Then
                        .cli_fec_nac = CDate("01-01-1900")
                    Else
                        .cli_fec_nac = CDate(Me.Txt_Fec_Con.Text)
                    End If

                End If


                '**************************************************************************************************************************
                'ANTECEDENTES GENERALES
                .cli_dml = Trim(Me.Txt_Dom_Par.Text)
                .id_suc = DP_Sucursal.SelectedItem.Value


                If CInt(DP_Depto.SelectedItem.Value) = 0 Then
                    .id_cmn = Nothing
                Else
                    If CInt(DP_Ciudad.SelectedItem.Value) = 0 Then
                        '.ciu_num = Nothing
                        .id_cmn = Nothing
                    Else

                        '.ciu_num = CInt(DP_Ciudad.SelectedItem.Value)

                        If CInt(DP_Comuna.SelectedItem.Value) <> 0 Then
                            .id_cmn = CInt(DP_Comuna.SelectedItem.Value)
                        Else
                            .id_cmn = Nothing
                        End If

                    End If
                End If

                .cli_ema = Me.Txt_Mai.Text
                '.cli_cod_pot = CStr(Me.Txt_Cod_Pos.Text)

                If CInt(DP_Segmento.SelectedItem.Value) = 0 Then
                    .id_P_0076 = Nothing
                Else
                    .id_P_0076 = CInt(DP_Segmento.SelectedItem.Value)
                End If

                If DP_ActEco.SelectedItem.Value = 0 Then
                    .id_P_0064 = Nothing
                Else
                    .id_P_0064 = DP_ActEco.SelectedItem.Value
                End If

                If DP_Giro.SelectedItem.Value = 0 Then
                    .id_PL_000006 = Nothing
                Else
                    .id_PL_000006 = DP_Giro.SelectedItem.Value.Trim()
                End If

                'If DP_Canal.SelectedItem.Value = 0 Then
                '    .CLI_CAN_AL = Nothing
                'Else
                '    .CLI_CAN_AL = DP_Canal.SelectedItem.Value.Trim()

                '    If DP_Canal.SelectedItem.Value <> 0 Then
                '        .CLI_SUB_CAN_AL = DP_SubCanal.SelectedItem.Value.Trim()
                '    End If
                'End If

                'If DP_GestorNeg.SelectedItem.Value = 0 Then
                '.cli_gest_cod = Nothing
                'Else
                '.cli_gest_cod = DP_GestorNeg.SelectedItem.Value.Trim()
                'End If

                '.cli_gest_cod = Txt_Cod_Ges.Text
                '**************************************************************************************************************************
                'ANTECEDENTES CON FACTORING


                If CInt(DP_Ejecutivo.SelectedItem.Value) = 0 Then
                    .id_eje_cod_eje = Nothing
                Else
                    .id_eje_cod_eje = CInt(DP_Ejecutivo.SelectedItem.Value)
                End If

                If CInt(DP_ModoOpe.SelectedItem.Value) = 0 Then
                    .id_P_007 = Nothing
                Else
                    .id_P_007 = CInt(DP_ModoOpe.SelectedItem.Value)
                End If

                If CInt(DP_EstCartera.SelectedItem.Value) = 0 Then
                    .id_crt = Nothing
                Else
                    .id_crt = CInt(DP_EstCartera.SelectedItem.Value)
                End If

                If CInt(DP_EstadoInf.SelectedItem.Value) = 0 Then
                    .id_P_0067 = Nothing
                Else
                    .id_P_0067 = CInt(DP_EstadoInf.SelectedItem.Value)
                End If

                If SW.Value = "UPDATE" Then
                    .cli_fec_cre = CDate(Txt_Fec_Cre.Text)
                    .cli_fec_ini_ope = CDate(Txt_Fec_Ope.Text)

                    If DP_Giro.SelectedIndex > 0 Then
                        .id_PL_000006 = DP_Giro.SelectedValue.Trim()
                    End If

                Else
                    .cli_fec_cre = Date.Now
                    .cli_fec_act_eje = Date.Now
                    .cli_fec_ini_ope = CDate("01-01-1900")
                    .cli_fec_ult_ope = CDate("01-01-1900")
                    .cli_fec_est = CDate("01-01-1900")
                    .cli_con_cbz = "N"
                End If

                If CInt(DP_Estado.SelectedItem.Value) = 0 Then
                    .id_P_008 = Nothing
                Else
                    .id_P_008 = CInt(DP_Estado.SelectedItem.Value)
                End If

                If CInt(DP_CateRiesgo.SelectedItem.Value) = 0 Then
                    .id_P_0058 = Nothing
                Else
                    .id_P_0058 = CInt(DP_CateRiesgo.SelectedItem.Value)
                End If

                If CInt(DP_FormaEnvio.SelectedItem.Value) = 0 Then
                    .id_P_0068 = Nothing
                Else
                    .id_P_0068 = CInt(DP_FormaEnvio.SelectedItem.Value)
                End If

                If CB_CobranzaAnt.Checked Then
                    .cli_cob_ant = CChar("S")
                ElseIf Not CB_CobranzaAnt.Checked Then
                    .cli_cob_ant = CChar("N")
                End If

                '**************************************************************************************************************************
                'ANTECEDENTES CON BANCO
                .cli_eje_ofc = UCase(DP_Suc_Bco.SelectedItem.Text)
                '.cli_eje_bci = DP_Gestor.SelectedItem.Text
                .cli_eje_anx = Val(Me.Txt_Anx_Bco.Text)

                '-------------------------------------------------------
                'Versión: 12122013.V1

                '-------------------------------------------------------

                '**************************************************************************************************************************
                'CLASIFICACION DEL CLIENTE

                'If DP_Clacificacion.SelectedItem.Value = "0" Then
                '    .id_p_0118 = Nothing
                'Else
                '    .id_p_0118 = DP_Clacificacion.SelectedItem.Value
                'End If

                '**************************************************************************************************************************
                'CONDICIONES FINANCIERAS
                .CLI_TIP_TAS = DP_TipoTasa.SelectedValue
                .cli_spr_ead_col = Val(Txt_Spr_Col.Text.Replace(",", ".")) 'Spread o DFT
                .cli_tas_mor_aux = Val(Txt_Spr_Mor.Text.Replace(",", ".")) 'Spread Mora
                .cli_dia_bas = RB_BaseDias.SelectedValue
            End With

            Return CLI

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return Nothing
        End Try

    End Function

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig.TextChanged

        IB_Contacto.Enabled = False
        IB_Bancos.Enabled = False
        IB_Empresa.Enabled = False
        IB_Organigrama.Enabled = False

        Dim coll_identificacion As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIdentificacion, _
                                                                               DP_TipoIdentificacion.SelectedValue)

        If coll_identificacion(1).pnu_vld_dig = "S" Then

            If (FC.VrutCambio(Txt_Rut.Text.Trim().ToUpper(), DP_TipoIdentificacion.SelectedValue) <> Txt_Dig.Text.Trim().ToUpper()) Then

                Msj.Mensaje(Me.Page, Caption, "Dígito de la Identificación no válida", TipoDeMensaje._Exclamacion)

                Txt_Rut.Text = ""
                Txt_Dig.Text = ""
                Txt_Rut.Focus()

                Exit Sub

            End If

            If Not IsNothing(CLSCLI.ClientesDevuelve(Txt_Rut.Text, Txt_Dig.Text.Trim())) Then

                Msj.Mensaje(Me.Page, Caption, "Cliente Ya Existe", TipoDeMensaje._Exclamacion)

                Txt_Rut.Text = ""
                Txt_Dig.Text = ""
                Txt_Rut.Focus()

                Exit Sub

            End If

            Txt_Dig.ReadOnly = False
            Txt_Dig.CssClass = "clsMandatorio"

        Else
            Txt_Dig.Text = "0"
            Txt_Dig.ReadOnly = True
            Txt_Dig.CssClass = "clsDisabled"
        End If

        IB_Contacto.Enabled = True
        IB_Bancos.Enabled = True
        IB_Empresa.Enabled = True
        IB_Organigrama.Enabled = True

    End Sub

    Protected Sub Txt_nro_cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_nro_cli.TextChanged

        If Txt_nro_cli.Text.Length = 8 Then
            If CLSCLI.NroClienteDevuelve(Txt_nro_cli.Text.Trim) Then
                Msj.Mensaje(Page, "Administración Clentes", "Nro. de cliente asociado a otro cliente", TipoDeMensaje._Exclamacion)
                Txt_nro_cli.Text = ""
                Txt_nro_cli.Focus()
            End If
        Else
            Msj.Mensaje(Page, "Administración Clentes", "Nro. Cliente debe ser de largo 8", TipoDeMensaje._Exclamacion)
            Txt_nro_cli.Text = ""
            Txt_nro_cli.Focus()
        End If

    End Sub




    Protected Sub Txt_Rut_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Rut.TextChanged


        If Txt_Rut.Text.Trim().Length > 0 Then

            If Not IsNothing(CLSCLI.ClienteDevuelvePorRut(Txt_Rut.Text)) Then
                Msj.Mensaje(Page, "Administración Clentes", "NIT ya existe", TipoDeMensaje._Exclamacion)
                Txt_Rut.Text = ""
                Txt_Dig.Text = ""
                Exit Sub
            Else
                Txt_Dig.Text = FC.Vrut(Txt_Rut.Text.Trim().ToUpper())
            End If

            Dim cli As cli_cls
            Dim ciu As ciu_cls

            cli = CLSCLI.ClienteDevuelveUnificado(CLng(Txt_Rut.Text))
            ciu = CLSCLI.ClienteCiudadDevuelveUnificado(CLng(Txt_Rut.Text))

            If Not IsNothing(cli) Then

                Txt_Dig.Text = cli.cli_dig_ito
                Txt_nro_cli.Text = cli.cli_nro_cli
                Txt_Nom_Bre.Text = cli.cli_rso
                Txt_Raz_Soc.Text = cli.cli_rso
                Txt_Ape_Pat.Text = cli.cli_ape_ptn
                Txt_Ape_Mat.Text = cli.cli_ape_ptn
                Txt_Dom_Par.Text = cli.cli_dml

                If Not IsNothing(ciu) Then

                    CG.MunicipioDevuelve(ciu.id_p_001, True, DP_Ciudad)

                    DP_Ciudad.ClearSelection()
                    DP_Depto.ClearSelection()

                    DP_Depto.Items.FindByValue(ciu.id_p_001).Selected = True
                    DP_Ciudad.Items.FindByValue(ciu.id_ciu).Selected = True

                    CG.ComunaDevuelve(Me.DP_Ciudad.SelectedItem.Value, True, Me.DP_Comuna)
                    DP_Comuna.ClearSelection()
                    DP_Comuna.SelectedIndex = 1

                End If

                DP_TipoIdentificacion.ClearSelection()
                DP_Corasu.ClearSelection()
                DP_TipoCliente.ClearSelection()

                DP_TipoIdentificacion.Items.FindByValue(cli.id_P_0119).Selected = True
                DP_Corasu.Items.FindByValue(cli.id_P_0313).Selected = True
                DP_TipoCliente.Items.FindByValue(cli.id_P_0044).Selected = True

                Dim coll_tipo As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoCliente, _
                                                                             DP_TipoCliente.SelectedValue)
                If coll_tipo(1).pnu_atr_001 = Nothing Then
                    Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                    MultiView2.ActiveViewIndex = 1
                Else
                    If coll_tipo(1).pnu_atr_001.ToString().Equals("F") Then
                        Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                        MultiView2.ActiveViewIndex = 0
                    End If
                End If

                'If coll_tipo(1).pnu_atr_001.ToString().Equals("F") Then
                '    Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                '    MultiView2.ActiveViewIndex = 0
                'Else
                '    Me.Titulo_MV.Text = "" & DP_TipoCliente.SelectedItem.ToString()
                '    MultiView2.ActiveViewIndex = 1
                'End If

                Dim coll_identificacion As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIdentificacion, _
                                                                                       DP_TipoIdentificacion.SelectedValue)

                If coll_identificacion(1).pnu_vld_dig = "N" Then
                    Txt_Dig.Text = "0"
                    Txt_Dig.ReadOnly = True
                    Txt_Dig.CssClass = "clsDisabled"
                End If

                '@GIRO VARCHAR(20),    
                If Not IsNothing(cli.id_PL_000006) Then
                    Me.DP_Giro.ClearSelection()
                    BuscaCombo(Me.DP_Giro, CStr(cli.id_PL_000006))
                End If

                CG.ActividadEconomicaDevuelve(DP_Giro.SelectedValue, DP_ActEco)

                '@ACT_ECO INT,    
                If Not IsNothing(cli.id_P_0064) Then
                    Me.DP_ActEco.ClearSelection()
                    BuscaCombo(Me.DP_ActEco, CStr(Val(cli.id_P_0064)))
                End If

                '@SEXO INT,    
                If Not IsNothing(cli.cli_sex) Then
                    Me.Dp_Sexo.ClearSelection()
                    BuscaCombo(Me.Dp_Sexo, CStr(Val(cli.cli_sex)))
                End If

                '@COD_SUC VARCHAR(10),    
                If Not IsNothing(cli.id_suc) Then

                    Me.DP_Sucursal.ClearSelection()
                    BuscaCombo(Me.DP_Sucursal, CStr(Val(cli.id_suc)))

                    'CG.GestorDevuelve(cli.id_suc, DP_Gestor)
                    'CG.GestorNegocioDevuelve(DP_Sucursal.SelectedValue, DP_GestorNeg)

                    'codigo gestor oficina
                    'If Not IsNothing(cli.cli_cod_ges) Then
                    'Me.DP_Gestor.ClearSelection()
                    'BuscaCombo(Me.DP_Gestor, cli.cli_gest_cod)
                    'End If

                    'Txt_Cod_Ges.Text = cli.cli_gest_cod

                    'codigo gestor negocio
                    'If Not IsNothing(cli.cli_gest_cod) Then
                    'Me.DP_GestorNeg.ClearSelection()
                    'BuscaCombo(Me.DP_GestorNeg, cli.cli_gest_cod)
                    'End If

                    'Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(cli.cli_gest_cod).eje_mail
                    'Txt_Ema_Ges.ReadOnly = False
                    'Txt_Ema_Ges.CssClass = "clsMandatorio"

                End If

                '@COD_EJE VARCHAR(10),      
                If Not IsNothing(cli.id_eje_cod_eje) Then
                    Me.DP_Ejecutivo.ClearSelection()
                    BuscaCombo(Me.DP_Ejecutivo, CStr(Val(cli.id_eje_cod_eje)))
                End If

                '@CORREO VARCHAR(50))   
                Txt_Mai.Text = cli.cli_ema

                'CANAL Y SUB CANAL
                'If Not IsNothing(cli.CLI_CAN_AL) Then
                'Me.DP_Canal.ClearSelection()
                'BuscaCombo(Me.DP_Canal, CStr(cli.CLI_CAN_AL))
                'CG.SubCanalDevuelve(cli.CLI_CAN_AL, DP_SubCanal)
                'If Not IsNothing(cli.CLI_SUB_CAN_AL) Then
                'Me.DP_SubCanal.ClearSelection()
                'BuscaCombo(Me.DP_SubCanal, CStr(cli.CLI_SUB_CAN_AL))
                'End If
                'End If

            Else
                Txt_nro_cli.Text = ""
                Txt_Nom_Bre.Text = ""
                Txt_Ape_Pat.Text = ""
                Txt_Ape_Mat.Text = ""
                DP_Corasu.ClearSelection()
                DP_TipoCliente.ClearSelection()
                DP_Ciudad.ClearSelection()
                DP_Depto.ClearSelection()
            End If
        End If

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Response.Redirect("MClientes.aspx", False)
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Dim agt As New Perfiles.Cls_Principal

        If Not agt.ValidaAccesso(20, 20040101, Usr, "PRESIONO GUARDAR CLIENTE") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)

            Exit Sub
        End If

        If Not ValidaCampos() Then
            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

        End If

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim Cliente As cli_cls

            Cliente = CargaCliente()

            If Not IsNothing(Cliente) Then

                Select Case SW.Value

                    Case Trim("INSERT")

                        If CLSCLI.ClienteInsertar(Cliente, Txt_Bie_Ser.Text, IIf(DP_Ciudad.SelectedIndex > 0, DP_Ciudad.SelectedValue, 0), Txt_Ema_Ges.Text) Then
                            Msj.Mensaje(Me.Page, Caption, "Cliente Ingresado", TipoDeMensaje._Informacion)
                        Else
                            Msj.Mensaje(Me.Page, Caption, "Cliente con ese Rut ya existe", TipoDeMensaje._Informacion)
                        End If

                    Case Trim("UPDATE")
                        If CLSCLI.ClienteUpdate(Cliente, IIf(DP_Ciudad.SelectedIndex > 0, DP_Ciudad.SelectedValue, 0), Txt_Ema_Ges.Text) Then
                            Msj.Mensaje(Me.Page, Caption, "Cliente Modificado", TipoDeMensaje._Informacion)
                        End If

                End Select

                Response.Redirect("MClientes.aspx", False)

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Empresa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Empresa.Click
        'var x=window.showModalDialog(url + '?rut=' + rut, window, 'scroll:no;status:off;dialogWidth:'+pWidth+'px;dialogHeight:'+pHeight+'px;dialogLeft:'+pLeft+'px;dialogTop:'+pTop+'px'); 

        Dim rut As String = Txt_Rut.Text

        Try
            If rut = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Cliente", TipoDeMensaje._Exclamacion)
                Return
            End If

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Empresas.aspx?rut=" & rut & "", "Empresa", 550, 450, 100, 100)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Empresas.aspx?rut= " & rut & "', window, 'scroll:no;status:off;dialogWidth:550px;dialogHeight:450px;dialogLeft:100px;dialogTop:100px');")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atencion", "Se ha producido un error: ", ex.Message, 1)
        End Try

    End Sub

    Protected Sub IB_Contacto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Contacto.Click
        Dim rut As String = Txt_Rut.Text
        Try
            If rut = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Cliente", TipoDeMensaje._Exclamacion)
                Return
            End If

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "../../Contactos/Contactos.aspx?rut=" & rut & "", "Contactos", 650, 650, 100, 100)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Contactos/Contactos.aspx?rut= " & rut & "', window, 'scroll:no;status:off;dialogWidth:650px;dialogHeight:650px;dialogLeft:100px;dialogTop:100px');")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atencion", "Se ha producido un error: ", ex.Message, 1)
        End Try

    End Sub

    Protected Sub IB_Bancos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Bancos.Click
        Dim rut As String = Txt_Rut.Text

        Try

            If rut = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Cliente", TipoDeMensaje._Exclamacion)
                Return
            End If

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Bancos.aspx?rut=" & rut & "", "Bancos", 740, 500, 100, 100)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Bancos.aspx?rut= " & rut & "', window, 'scroll:no;status:off;dialogWidth:740px;dialogHeight:500px;dialogLeft:100px;dialogTop:100px');")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atencion", "Se ha producido un error: ", ex.Message, 1)
        End Try

    End Sub

    Protected Sub IB_Organigrama_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Organigrama.Click
        Dim rut As String = Txt_Rut.Text

        Try
            If rut = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Cliente", TipoDeMensaje._Exclamacion)
                Return
            End If

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Organigramas.aspx?rut=" & rut & "", "Organigramas", 550, 550, 100, 100)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Organigramas.aspx?rut= " & rut & "', window, 'scroll:no;status:off;dialogWidth:550px;dialogHeight:550px;dialogLeft:100px;dialogTop:100px');")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atencion", "Se ha producido un error: ", ex.Message, 1)
        End Try

    End Sub

#End Region

End Class
