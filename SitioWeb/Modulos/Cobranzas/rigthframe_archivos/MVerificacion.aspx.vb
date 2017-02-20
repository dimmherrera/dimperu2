Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_Verificacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim Sesion As New ClsSession.ClsSession
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FC As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Verificación"
    Dim AG As New ActualizacionesGenerales
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim ClsCli As New ClaseClientes
    Dim cbz As New ClaseCobranza
    Dim ope As New ClaseOperaciones
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                Response.Expires = -1
                CargaDrop()
                AlinearDerecha()
                Txt_Rut_Cli.Focus()
                NroPaginacion = 0
            Else
                MarcaGrilla(Txt_Id_Deudor.Value)
            End If

            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',640,400,100,100);")

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Cobranza"
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)
        End If
    End Sub

    Protected Sub Dp_Suc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Suc.SelectedIndexChanged
        Try
            Dp_Eje.ClearSelection()

            CG.EjecutivosDevuelve(Dp_Eje, CodEje, 15)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Dp_Tipo_Cli_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Tipo_Cli.SelectedIndexChanged
        If Dp_Tipo_Cli.SelectedValue = 1 Then
            Lbl_Raz_Soc.Text = "Nombre"
            
        ElseIf Dp_Tipo_Cli.SelectedValue = 2 Then
            Lbl_Raz_Soc.Text = "Razón Social"
        End If
    End Sub

    Protected Sub Dp_Est_Ver_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Est_Ver.SelectedIndexChanged
        Try
            Select Case Dp_Est_Ver.SelectedValue
                Case 0
                    Rb_Est_Veri.Checked = True
                Case Else
                    Rb_Est_Veri.Checked = False
            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Verificacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Verificacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "CelClick_GV_Verificacion(ctl00_ContentPlaceHolder1_GV_Verificacion, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try
            If Trim(Txt_Rut_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If
            If Trim(Txt_Dig_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verificador", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If
            If UCase(Txt_Dig_Cli.Text) <> FC.Vrut(Trim(Replace(Txt_Rut_Cli.Text, ",", ""))).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "NIT no es válido", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            ClienteDevuelve()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img_ver As ImageButton = CType(sender, ImageButton)

        Txt_Id_Deudor.Value = FC.LimpiaRut(img_ver.ToolTip)

        MarcaGrilla(Txt_Id_Deudor.Value)

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Guardar_Cli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not agt.ValidaAccesso(20, 20010107, Usr, "PRESIONO GUARDAR CLIENTE EN VERIFICACION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Select Case Dp_Tipo_Cli.SelectedValue
                Case 0
                    Msj.Mensaje(Me.Page, Caption, "Debe seleccionar tipo de Cliente", TipoDeMensaje._Exclamacion)
                    Dp_Tipo_Cli.Focus()
                    Exit Sub
                Case 1
                    If Trim(Txt_Raz_Soc.Text) = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Debe ingresar nombre de Cliente", TipoDeMensaje._Exclamacion)
                        Txt_Raz_Soc.Focus()
                        Exit Sub
                    End If
                Case 2
                    If Trim(Txt_Raz_Soc.Text) = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Debe ingresar razón social de Cliente", TipoDeMensaje._Exclamacion)
                        Txt_Raz_Soc.Focus()
                        Exit Sub
                    End If
            End Select

            If Dp_Suc.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar sucursal", TipoDeMensaje._Exclamacion)
                Dp_Suc.Focus()
                Exit Sub
            End If
            If Dp_Eje.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar ejecutivo", TipoDeMensaje._Exclamacion)
                Dp_Eje.Focus()
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de Grabar?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Ingreso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Txt_Raz_Soc.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim querystringSeguro As TSHAK.Components.SecureQueryString
            querystringSeguro = New TSHAK.Components.SecureQueryString(New Byte() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 8})

            querystringSeguro("Id_Cli") = Txt_Rut_Cli.Text
            querystringSeguro("Nom_Cli") = Txt_Raz_Soc.Text.Trim()

            AbrePopup(Me, 1, "Verificacion.aspx?data=" & HttpUtility.UrlEncode(querystringSeguro.ToString()) & "", "Documentos", 900, 900, 100, 100)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Modificar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        If Txt_Id_Deudor.Value = "" Or GV_Verificacion.Rows.Count = 0 Then
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un registro", TipoDeMensaje._Exclamacion)
        Else
            AbrePopup(Me, 2, "ModVerificacion.aspx?RutCli=" & Txt_Rut_Cli.Text & "&RutDeu=" & Txt_Id_Deudor.Value & _
                      "&MtoDsd=" & Txt_Mto_Dsd.Text & "&MtoHta=" & Txt_Mto_Hta.Text & _
                      "&FecDsd=" & Txt_Fec_Dsd.Text & "&FecHta=" & Txt_Fec_Hta.Text & _
                      "&NroOpe=" & Txt_Nro_ope.Text & "&NroDoc=" & Txt_Nro_Doc.Text & _
                      "&RB_Ver=" & Rb_Est_Veri.Checked & "&EstVer=" & Dp_Est_Ver.SelectedValue & _
                      "&TipDoc=" & Dp_Tip_Doc.SelectedValue & "&TipMon=" & Dp_Tip_Mon.SelectedValue & _
                      "", "Documentos a Verificar", 800, 650, 100, 100)
        End If
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        If Not agt.ValidaAccesso(20, 20070107, Usr, "PRESIONO BOTON BUSCAR VERIFICACION") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim MtoDsd As Double
        Dim MtoHta As Double
        Dim FecDsd As Date
        Dim FecHta As Date
        Dim NroOpe1 As Long
        Dim NroOpe2 As Long
        Dim NroDoc1 As Integer
        Dim NroDoc2 As Integer
        Dim EstVer1 As Integer
        Dim EstVer2 As Integer
        Dim TipDoc1 As Integer
        Dim TipDoc2 As Integer
        Dim TipMon As Integer


        Dim Cli As cli_cls

        Try
            '-----------------------------------------------------------------------------------------------------
            'Validación de campos vacios
            '-----------------------------------------------------------------------------------------------------

            'Rut Cliente
            If Trim(Txt_Rut_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            'Digito del rut
            If Trim(Txt_Dig_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar Digito Verificador", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If

            'Validación del rut
            If Trim(Txt_Dig_Cli.Text).ToUpper <> FC.Vrut(Trim(Replace(Txt_Rut_Cli.Text, ",", ""))).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "NIT no es válido", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            'Moneda
            If Dp_Tip_Mon.SelectedIndex = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar Moneda", TipoDeMensaje._Exclamacion)
                Dp_Tip_Mon.Focus()
                Exit Sub
            End If

            'Rango Monto
            If Trim(Txt_Mto_Dsd.Text) = "" And Trim(Txt_Mto_Hta.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto desde", TipoDeMensaje._Exclamacion)
                Txt_Mto_Dsd.Focus()
                Exit Sub
            End If

            If Txt_Mto_Dsd.Text <> "" And Txt_Mto_Hta.Text <> "" Then
                If CInt(Txt_Mto_Dsd.Text) > CInt(Txt_Mto_Hta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Monto desde debe ser menor a monto hasta", TipoDeMensaje._Exclamacion)
                    Txt_Mto_Dsd.Focus()
                    Exit Sub
                End If
            End If

            'Rango Fecha
            If Trim(Txt_Fec_Dsd.Text) = "" And Trim(Txt_Fec_Hta.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha desde", TipoDeMensaje._Exclamacion)
                Txt_Fec_Dsd.Focus()
                Exit Sub
            End If

            If CDate(FecDsd) > CDate(FecHta) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha desde debe ser menor a fecha hasta", TipoDeMensaje._Exclamacion)
                Txt_Fec_Dsd.Focus()
                Exit Sub
            End If


            If Trim(Txt_Fec_Dsd.Text) <> "" Then
                If Not IsDate(Txt_Fec_Dsd.Text) Then
                    Msj.Mensaje(Page, Caption, "Formato de fecha desde erroneo", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Dsd.Text = ""
                    Txt_Fec_Dsd.Focus()
                    Exit Sub
                End If
            End If

            If Trim(Txt_Fec_Hta.Text) <> "" Then
                If Not IsDate(Txt_Fec_Hta.Text) Then
                    Msj.Mensaje(Page, Caption, "Formato de fecha hasta es erroneo", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_Hta.Text = ""
                    Txt_Fec_Hta.Focus()
                    Exit Sub
                End If
            End If



            '-----------------------------------------------------------------------------------------------------
            'Llenamos las variables de criterio de busqueda segun campos llenados
            '-----------------------------------------------------------------------------------------------------

            If Trim(Txt_Mto_Dsd.Text) = "" Or Trim(Txt_Mto_Hta.Text) = "" Then
                MtoDsd = 0
                MtoHta = 9999999999999
            Else
                MtoDsd = Txt_Mto_Dsd.Text
                MtoHta = Txt_Mto_Hta.Text
            End If

            If Trim(Txt_Fec_Dsd.Text) = "" Then
                FecDsd = "1900/01/01"
                FecHta = "9999/01/01"
            Else
                FecDsd = CDate(Txt_Fec_Dsd.Text)
                FecHta = CDate(Txt_Fec_Hta.Text)
                '        fec_des = CDate("01/01/1900 0:00:00")
                '        fec_has = CDate("31/12/9999 23:59:59")
                'Else
                '        fec_des = CDate(txt_fec_des.Text)
                '        fec_has = CDate(txt_fec_has.Text)

            End If

            'Rango Nro Operación
            If Trim(Txt_Nro_ope.Text) = "" Then
                NroOpe1 = 0
                NroOpe2 = 9999999999
            Else
                NroOpe1 = CInt(Txt_Nro_ope.Text)
                NroOpe2 = CInt(Txt_Nro_ope.Text)
            End If

            'Rango Nro Documento
            If Trim(Txt_Nro_Doc.Text) = "" Then
                NroDoc1 = "0"
                NroDoc2 = 999999999
            Else
                NroDoc1 = CInt(Txt_Nro_Doc.Text)
                NroDoc2 = CInt(Txt_Nro_Doc.Text)
            End If

            'Rango Estado Verificación
            If Rb_Est_Veri.Checked = False And Dp_Est_Ver.SelectedValue = 0 Then
                EstVer1 = 0
                EstVer2 = 999999999
            End If

            If Rb_Est_Veri.Checked = True And Dp_Est_Ver.SelectedValue = 0 Then
                EstVer1 = 0
                EstVer2 = 999999999
            End If

            If Rb_Est_Veri.Checked = False And Dp_Est_Ver.SelectedValue <> 0 Then
                EstVer1 = Dp_Est_Ver.SelectedValue
                EstVer2 = Dp_Est_Ver.SelectedValue
            End If

            'Rango Tipo Documento
            If Dp_Tip_Doc.SelectedValue = 0 Then
                TipDoc1 = "0"
                TipDoc2 = 999
            Else
                TipDoc1 = Dp_Tip_Doc.SelectedValue
                TipDoc2 = Dp_Tip_Doc.SelectedValue
            End If

            'Tipo Moneda
            TipMon = Dp_Tip_Mon.SelectedValue

            Cli = ClsCli.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

            If valida_cliente <> "" Then  'Sesion.valida_cliente
                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                'ClienteDevuelve()

                Dp_Tipo_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.CssClass = "clsDisabled"
                Dp_Suc.CssClass = "clsDisabled"
                Dp_Eje.CssClass = "clsDisabled"

                IB_Ingreso.Enabled = True
                Dp_Tipo_Cli.Enabled = False
                Txt_Raz_Soc.ReadOnly = True
                Dp_Suc.Enabled = False
                Dp_Eje.Enabled = False
                Dp_Tip_Mon.Focus()
            End If
            If IsNothing(Cli) Then
                Msj.Mensaje(Page, Caption, "Este cliente no existe ,no puede seguir con la busqueda", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Limpia()

                Exit Sub


            End If

            If cbz.DeudorCliente_DoctoLista(GV_Verificacion, Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT), MtoDsd, MtoHta, _
                                         CDate(FecDsd), CDate(FecHta), NroOpe1, NroOpe2, NroDoc1, NroDoc2, EstVer1, EstVer2, TipDoc1, _
                                          TipDoc2, TipMon, NroPaginacion) Then

                IB_Modificar.Enabled = True
            Else
                IB_Modificar.Enabled = False
            End If



            If GV_Verificacion.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No existen Documentos Para el criterio de busqueda", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        NroPaginacion = 0
        Limpia()
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim Cliente As New cli_cls

            Cliente = CargaCliente()

            If Not IsNothing(Cliente) Then
                If ClsCli.ClienteInsertar(Cliente, "", 0, "") Then
                    'ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), Var.FMT_RUT), _
                    '                                    Txt_Nro_Doc.Text, , Dp_Tip_Doc.SelectedValue, 2)
                    Msj.Mensaje(Me.Page, Caption, "Cliente ingresado", TipoDeMensaje._Informacion)
                    Dp_Tipo_Cli.CssClass = "clsDisabled"
                    Txt_Raz_Soc.CssClass = "clsDisabled"
                    Dp_Suc.CssClass = "clsDisabled"
                    Dp_Eje.CssClass = "clsDisabled"
                    Dp_Tipo_Cli.Enabled = False
                    Txt_Raz_Soc.ReadOnly = True
                    Dp_Suc.Enabled = False
                    Dp_Eje.Enabled = False
                    IB_Modificar.Enabled = True
                    IB_Ingreso.Enabled = True
                    IB_Guardar_Cli.Enabled = False
                End If
            End If

            IB_Ingreso.Enabled = True
            IB_Modificar.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LB_Cliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Cliente.Click

        Try

            Dp_Tipo_Cli.CssClass = "clsMandatorio"
            Txt_Raz_Soc.CssClass = "clsMandatorio"

            Dp_Suc.CssClass = "clsMandatorio"
            Dp_Eje.CssClass = "clsMandatorio"

            IB_Ingreso.Enabled = False
            IB_Modificar.Enabled = False
            IB_Guardar_Cli.Enabled = True

            Dp_Tipo_Cli.Enabled = True
            Txt_Raz_Soc.ReadOnly = False
            Dp_Suc.Enabled = True
            Dp_Eje.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Sub y funciones Generales"

    Sub Limpia()

        Txt_Rut_Cli.Focus()
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = False

        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = False
        Txt_Raz_Soc.Text = ""

        Dp_Tipo_Cli.ClearSelection()
        Dp_Suc.ClearSelection()
        Dp_Eje.ClearSelection()

        Txt_Raz_Soc.CssClass = "clsDisabled"

        Dp_Tipo_Cli.CssClass = "clsDisabled"
        Dp_Suc.CssClass = "clsDisabled"
        Dp_Eje.CssClass = "clsDisabled"

        Dp_Tipo_Cli.Enabled = False
        Dp_Suc.Enabled = False
        Dp_Eje.Enabled = False

        Dp_Tip_Doc.SelectedValue = 0
        Dp_Tip_Mon.SelectedValue = 0

        Txt_Nro_Doc.Text = ""
        Me.Txt_Nro_ope.Text = ""

        Me.Txt_Mto_Dsd.Text = ""
        Me.Txt_Mto_Hta.Text = ""

        Me.Txt_Fec_Dsd.Text = ""
        Me.Txt_Fec_Hta.Text = ""

        Txt_Raz_Soc.ReadOnly = True

        GV_Verificacion.DataSource = Nothing
        GV_Verificacion.DataBind()
        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"
        IB_AyudaCli.Enabled = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = True
        IB_Ingreso.Enabled = False
        IB_Modificar.Enabled = False
        NroPaginacion = 0

    End Sub

    Sub CargaDrop()
        Try
            CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, Dp_Tipo_Cli)
            CG.ParametrosDevuelve(TablaParametro.EstadoVerificacion, True, Dp_Est_Ver)
            CG.SucursalesDevuelve(codeje, True, Dp_Suc)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Tip_Mon)
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_Tip_Doc)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Function CargaCliente() As cli_cls

        Try

            Dim Cliente As New cli_cls

            With Cliente
                '**************************************************************************************************************************
                'DATOS PRINCIPALES
                .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                .id_P_0044 = CInt(Dp_Tipo_Cli.SelectedItem.Value)


                If CInt(Dp_Tipo_Cli.SelectedItem.Value) = 1 Then
                    'NATURAL
                    .cli_rso = UCase(Txt_Raz_Soc.Text)
                Else
                    'JURIDICO
                    .cli_rso = UCase(Txt_Raz_Soc.Text)
                End If

                .cli_sex = ""
                .cli_fec_nac = CDate("01-01-1900")

                '**************************************************************************************************************************
                'ANTECEDENTES GENERALES
                .cli_dml = Nothing
                .id_suc = Dp_Suc.SelectedItem.Value
                .id_cmn = Nothing

                .id_cmn = Nothing
                .cli_ema = Nothing
                .cli_cod_pot = Nothing

                .id_P_0076 = Nothing
                .id_P_0064 = Nothing
                .id_PL_000006 = Nothing

                '**************************************************************************************************************************
                'ANTECEDENTES CON FACTORING
                If CInt(Dp_Eje.SelectedItem.Value) = 0 Then
                    .id_eje_cod_eje = Nothing
                Else
                    .id_eje_cod_eje = CInt(Dp_Eje.SelectedItem.Value)
                End If

                .id_P_007 = Nothing
                .id_crt = Nothing
                .id_P_0067 = Nothing

                'Spread Mora
                .cli_tas_mor_aux = Nothing
                'Spread Colocación
                .cli_spr_ead_col = Nothing

                .cli_fec_cre = Date.Now
                .cli_fec_act_eje = Date.Now
                .cli_fec_ini_ope = CDate("01-01-1900")
                .cli_fec_ult_ope = CDate("01-01-1900")
                .cli_fec_est = CDate("01-01-1900")
                .cli_con_cbz = "N"

                .id_P_008 = Nothing
                .id_P_0058 = Nothing
                .id_P_0068 = Nothing
                .cli_snf = Nothing
                .cli_cob_ant = Nothing

                '**************************************************************************************************************************
                'ANTECEDENTES CON BANCO
                .cli_eje_ofc = Nothing
                .cli_eje_bci = Nothing
                .cli_eje_anx = Nothing

                .id_PL_000066 = Nothing

                '**************************************************************************************************************************

            End With

            Return Cliente

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return Nothing
        End Try

    End Function

    Sub ClienteDevuelve()
        Try

            'Se elimina la function VerificaClienteDevuelve por existir una funcion que hace lo mismo ClientesDevuelve
            Dim CLSCLI As New ClaseClientes
            Dim Cliente As cli_cls


            Cliente = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)


            If Sesion.valida_cliente <> "" Then

                If Not IsNothing(Cliente) Then
                    Msj.Mensaje(Me.Page, Caption, "Cliente no existe, ¿Desea ingresarlo?", TipoDeMensaje._Confirmacion, LB_Cliente.UniqueID)
                Else
                    Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                End If

                Exit Sub
            Else

                If IsNothing(Cliente) Then
                    Msj.Mensaje(Me.Page, Caption, "Cliente no existe, ¿Desea ingresarlo?", TipoDeMensaje._Confirmacion, LB_Cliente.UniqueID)
                    Exit Sub
                    'Else
                    '    If Not IsNothing(Cliente) Then
                    '        Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                    '        Exit Sub
                    '    End If
                End If

                Dp_Tipo_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.CssClass = "clsDisabled"
                Dp_Suc.CssClass = "clsDisabled"
                Dp_Eje.CssClass = "clsDisabled"
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True

                IB_Ingreso.Enabled = True
                Dp_Tipo_Cli.Enabled = False
                Txt_Raz_Soc.ReadOnly = True
                Dp_Suc.Enabled = False
                Dp_Eje.Enabled = False
                Dp_Tip_Mon.Focus()
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_Ingreso.Enabled = True
            End If

            Dp_Tipo_Cli.ClearSelection()
            Dp_Tipo_Cli.SelectedValue = Cliente.id_P_0044

            With Cliente
                If CInt(Dp_Tipo_Cli.SelectedItem.Value) = 1 Then
                    'NATURAL
                    Txt_Raz_Soc.Text = .cli_rso.Trim.ToUpper & " " & .cli_ape_ptn.Trim.ToUpper & " " & .cli_ape_mtn.Trim.ToUpper
                Else
                    'JURIDICO
                    Txt_Raz_Soc.Text = .cli_rso.Trim.ToUpper
                End If

                Dp_Suc.SelectedValue = .id_suc
                Dp_Eje.ClearSelection()
                CG.EjecutivosDevuelve(Dp_Eje, CodEje, 15)

                Dp_Eje.SelectedValue = .id_eje_cod_eje

            End With

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Sub MarcaGrilla(ByVal id As Integer)

        Try

            For I = 0 To GV_Verificacion.Rows.Count - 1

                If (id = FC.LimpiaRut(GV_Verificacion.Rows(I).Cells(1).Text)) Then

                    Txt_Nom_Deudor.Value = GV_Verificacion.Rows(I).Cells(2).Text.Trim()

                    If (I Mod 2) = 0 Then
                        GV_Verificacion.Rows(I).CssClass = "selectable"
                    Else
                        GV_Verificacion.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Verificacion.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Verificacion.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Sub AlinearDerecha()

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Hta.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Nro_ope.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Nro_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub


#End Region

#Region "Link Button"

    Protected Sub LB_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If Trim(Txt_Rut_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If
            If Trim(Txt_Dig_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verificador", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If
            If Trim(Txt_Dig_Cli.Text).ToUpper <> FC.Vrut(Trim(Replace(Txt_Rut_Cli.Text, ",", ""))).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "NIT no es válido", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            ClienteDevuelve()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
    '    Try
    '        If NroPaginacion = 0 Then
    '            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
    '            Exit Sub
    '        End If
    '        If NroPaginacion = 10 Then
    '            NroPaginacion -= 10
    '            IB_Buscar_Click(Me, e)
    '        End If
    '    Catch ex As Exception
    '        Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
    '    End Try
    'End Sub

    'Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
    '    Try
    '        If GV_Verificacion.Rows.Count < 10 Then
    '            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
    '            Exit Sub
    '        End If

    '        If GV_Verificacion.Rows.Count = 10 Then
    '            NroPaginacion += 10
    '            IB_Buscar_Click(Me, e)
    '        End If
    '    Catch ex As Exception
    '        Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
    '    End Try
    'End Sub

    Protected Sub GV_Verificacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV_Verificacion.SelectedIndexChanged
        If GV_Verificacion.Rows.Count <> 0 Then
            IB_Modificar.Enabled = True
        End If
    End Sub

#End Region

End Class
