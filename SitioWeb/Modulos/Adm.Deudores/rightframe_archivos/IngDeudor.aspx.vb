Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Data

Partial Class Deudores_IngDeudor
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim CLSCLI As New ClaseClientes
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim FW As New FuncionesGenerales.RutinasWeb
    Dim Sesion As New ClsSession.ClsSession
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Mantención de Pagadores" 'FY 19-05-2012
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

#End Region

#Region "Eventos Generales"

    Private Sub Nuevo()

        Try

            Txt_Rut_Deu.CssClass = "clsMandatorio"
            Txt_Dig_Deu.CssClass = "clsMandatorio"
            DropTipoDeudor.CssClass = "clsMandatorio"
            DropAbrRazSoc.CssClass = "clsMandatorio"
            DropActEcoDeu.CssClass = "clsMandatorio"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_Nro_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Nro_Cli.TextChanged
        If Txt_Nro_Cli.Text.Length = 8 Then
            If CLSCLI.NroClienteDevuelve(Txt_Nro_Cli.Text.Trim) Then
                Msj.Mensaje(Page, "Administración Pagadores", "Nro. de cliente asociado a otro cliente", TipoDeMensaje._Exclamacion)
                Txt_Nro_Cli.Text = ""
                Txt_Nro_Cli.Focus()
            End If
        Else
            Msj.Mensaje(Page, "Administración Pagadores", "Nro. Cliente debe ser de largo 8", TipoDeMensaje._Exclamacion)
            Txt_Nro_Cli.Text = ""
            Txt_Nro_Cli.Focus()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                Response.Expires = -1
                NroPaginacion_Deu = 0

                Try

                    Coll_Cupo = New Collection
                    'Sesion.TipoContacto = 2
                    ClsSession.ClsSession.TipoDeContacto = 2
                    'Sesion.CodigoSucursal = Var.CODIGO_SUCURSAL
                    CargaDrop()
                    Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")


                    'Txt_ATR_CAR.Attributes.Add("Style", "TEXT-ALIGN: left")
                    If Request.QueryString("Nro") <> "" Then

                        TraeDatosDeudor(CLng(Trim(Request.QueryString("Nro"))))
                        Sw.Text = "UPDATE"

                        Txt_Rut_Deu.CssClass = "clsDisabled"
                        Txt_Dig_Deu.CssClass = "clsDisabled"

                        Txt_Rut_Deu.ReadOnly = True
                        Txt_Dig_Deu.ReadOnly = True

                        'If Txt_Rut_Deu.Text <> "" Then
                        '    IB_Contactos.Attributes.Add("onClick", "var x=window.showModalDialog('../../Contactos/Contactos.aspx?Rut= " & CLng(Txt_Rut_Deu.Text) & "&tipo=" & 2 & " ', window, 'scroll:no;status:off;dialogWidth:620px;dialogHeight:630px;dialogLeft:400px;dialogTop:200px');")
                        'End If
                        'Se habilita cuando el deudor ya esta insertado
                        IB_AgrCli.Enabled = True

                        llena_grilla_MON() 'FY 24-05-2012
                        'Tab_Linea_Finan.Visible = True 'FY 04-07-2012
                        IB_Pago.Enabled = True
                    Else
                        'Tab_Linea_Finan.Visible = False 'FY 04-07-2012
                        Sw.Text = "INSERT"
                        RBtn_Todos.Enabled = False
                        RBtn_Cli.Enabled = False
                        CollapsiblePanelExtenderAntGen.AutoExpand = True
                        'Se inhabilta cuando se esta insertando un deudor
                        IB_AgrCli.Enabled = False
                        IB_Pago.Enabled = False
                    End If

                    Txt_Rut_Deu.Focus()
                    botonera_linea_finan(False, True, False) 'FY 24-05-2012

                    HF_ACCION_LIN.Value = ""

                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

            End If

            'IB_AgrCli.Attributes.Add("onClick", "var x=window.showModalDialog('AgregarCliente.aspx', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:400px;dialogLeft:400px;dialogTop:200px');")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GrClientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrClientes.RowDataBound

    End Sub

    Protected Sub RBtn_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBtn_Todos.CheckedChanged
        Try
            'TabContRecYCob.Enabled = False
            TxtNro.Value = ""
            CargaGrillaCliXdeu("T")
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub RBtn_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBtn_Cli.CheckedChanged
        Try
            'TabContRecYCob.Enabled = False
            TxtNro.Value = ""
            CargaGrillaCliXdeu("A")
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Administracion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion_Deu = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub

            End If

            NroPaginacion_Deu -= 3
            CargaGrillaCliXdeu("T")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, 2)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            'If GrClientes.Rows.Count < 4 Then
            '    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            '    Exit Sub
            'End If
            GrClientes.DataSource = New Collection
            GrClientes.DataBind()

            NroPaginacion_Deu += 3
            CargaGrillaCliXdeu("T")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, 2)
        End Try
    End Sub

    Protected Sub DropDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDepto.SelectedIndexChanged 'FY 23-05-2012
        DropCiudadDeu.ClearSelection()
        DropComunaDeu.ClearSelection()
        CG.MunicipioDevuelve(DropDepto.SelectedValue, True, DropCiudadDeu)
    End Sub

    'Protected Sub Txt_Rut_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Rut_Deu.TextChanged

    '    If Txt_Rut_Deu.Text.Length > 0 Then

    '        If Not IsNothing(CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)) Then
    '            Msj.Mensaje(Page, "Administración Clentes", "NIT ya existe", TipoDeMensaje._Exclamacion)
    '            Txt_Rut_Deu.Text = ""
    '            Txt_Rut_Deu.Focus()
    '            Exit Sub
    '        End If

    '        Dim cli As cli_cls
    '        Dim ciu As ciu_cls

    '        cli = CLSCLI.ClienteDevuelveUnificado(CLng(Txt_Rut_Deu.Text))
    '        ciu = CLSCLI.ClienteCiudadDevuelveUnificado(CLng(Txt_Rut_Deu.Text))

    '        If Not IsNothing(cli) Then

    '            Txt_Dig_Deu.Text = cli.cli_dig_ito
    '            Txt_Nro_Cli.Text = cli.cli_nro_cli
    '            Txt_Rso_Deu.Text = cli.cli_rso
    '            Txt_Raz_Soc.Value = cli.cli_rso
    '            TxtApePat.Text = cli.cli_ape_ptn
    '            TxtApeMat.Text = cli.cli_ape_ptn

    '            DropTipoIdentificacion.ClearSelection()
    '            DP_Corasu.ClearSelection()
    '            DropTipoDeudor.ClearSelection()

    '            DropTipoIdentificacion.Items.FindByValue(cli.id_P_0119).Selected = True
    '            DP_Corasu.Items.FindByValue(cli.id_P_0313).Selected = True
    '            DropTipoDeudor.Items.FindByValue(cli.id_P_0044).Selected = True

    '            If Not IsNothing(ciu) Then

    '                CG.MunicipioDevuelve(ciu.id_p_001, True, DropCiudadDeu)

    '                DropCiudadDeu.ClearSelection()
    '                DropDepto.ClearSelection()

    '                DropDepto.Items.FindByValue(ciu.id_p_001).Selected = True
    '                DropCiudadDeu.Items.FindByValue(ciu.id_ciu).Selected = True

    '                CG.ComunaDevuelve(Me.DropCiudadDeu.SelectedItem.Value, True, Me.DropComunaDeu)
    '                DropComunaDeu.ClearSelection()
    '                DropComunaDeu.SelectedIndex = 1

    '            End If

    '            TipoDeudor()

    '            Dim coll_identificacion As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIdentificacion, _
    '                                                                                   DropTipoIdentificacion.SelectedValue)

    '            If coll_identificacion(1).pnu_vld_dig = "N" Then
    '                Txt_Dig_Deu.Text = "0"
    '                Txt_Dig_Deu.ReadOnly = True
    '                Txt_Dig_Deu.CssClass = "clsDisabled"
    '            End If

    '            '@GIRO VARCHAR(20),    
    '            If Not IsNothing(cli.id_PL_000006) Then
    '                Me.DropGiroDeu.ClearSelection()
    '                BuscaCombo(Me.DropGiroDeu, CStr(cli.id_PL_000006))
    '            End If

    '            CG.ActividadEconomicaDevuelve(DropGiroDeu.SelectedValue, DropActEcoDeu)

    '            '@ACT_ECO INT,    
    '            If Not IsNothing(cli.id_P_0064) Then
    '                Me.DropActEcoDeu.ClearSelection()
    '                BuscaCombo(Me.DropActEcoDeu, CStr(Val(cli.id_P_0064)))
    '            End If

    '            '@SEXO INT,    


    '            '@COD_EJE VARCHAR(10),      
    '            If Not IsNothing(cli.id_eje_cod_eje) Then
    '                Me.DP_Eje_Fac.ClearSelection()
    '                BuscaCombo(Me.DP_Eje_Fac, CStr(Val(cli.id_eje_cod_eje)))
    '            End If

    '            '@COD_SUC VARCHAR(10),    
    '            If Not IsNothing(cli.id_suc) Then

    '                Me.DropSucursal.ClearSelection()
    '                BuscaCombo(Me.DropSucursal, CStr(Val(cli.id_suc)))

    '                CG.GestorDevuelve(cli.id_suc, DP_Gestor)
    '                CG.GestorNegocioDevuelve(cli.id_suc, DP_GestorNeg)

    '                '@EJE_OFICINA VARCHAR(150),    
    '                'Txt_Eje_Bco.Text = cli.cli_eje_ofc
    '                If Not IsNothing(cli.cli_cod_ges) Then
    '                    Me.DP_Gestor.ClearSelection()
    '                    BuscaCombo(Me.DP_Gestor, cli.cli_cod_ges)
    '                    Txt_Cod_Ges.Text = cli.cli_cod_ges
    '                End If

    '                If Not IsNothing(cli.CLI_GEST_COD) Then
    '                    Me.DP_GestorNeg.ClearSelection()
    '                    BuscaCombo(Me.DP_GestorNeg, cli.CLI_GEST_COD)
    '                    Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(cli.CLI_GEST_COD).eje_mail
    '                End If

    '            End If

    '            'CANAL Y SUB CANAL
    '            If Not IsNothing(cli.CLI_CAN_AL) Then
    '                Me.DP_Canal.ClearSelection()
    '                BuscaCombo(Me.DP_Canal, CStr(cli.CLI_CAN_AL))
    '                CG.SubCanalDevuelve(cli.CLI_CAN_AL, DP_SubCanal)
    '                If Not IsNothing(cli.CLI_SUB_CAN_AL) Then
    '                    Me.DP_SubCanal.ClearSelection()
    '                    BuscaCombo(Me.DP_SubCanal, CStr(cli.CLI_SUB_CAN_AL))
    '                End If
    '            End If


    '            '@CORREO VARCHAR(50))     
    '        Else
    '            Txt_Nro_Cli.Text = ""
    '            Txt_Rso_Deu.Text = ""
    '            Txt_Raz_Soc.Value = ""
    '            TxtApePat.Text = ""
    '            TxtApeMat.Text = ""

    '            DP_Corasu.ClearSelection()
    '            DropTipoDeudor.ClearSelection()
    '            DropCiudadDeu.ClearSelection()
    '            DropDepto.ClearSelection()
    '        End If

    '    End If

    'End Sub

#End Region

#Region "Drop"

    Protected Sub DropGiroDeu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropGiroDeu.SelectedIndexChanged

        Dim Drop As New ConsultasGenerales
        Drop.ActividadEconomicaDevuelve(DropGiroDeu.SelectedValue, DropActEcoDeu)

        If DropActEcoDeu.Items.Count > 0 Then
            DropActEcoDeu.SelectedIndex = 1
        End If

    End Sub

    Protected Sub DropCiudadDeu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropCiudadDeu.SelectedIndexChanged
        Try

            CG.ComunaDevuelve(Me.DropCiudadDeu.SelectedItem.Value, True, Me.DropComunaDeu)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub DropTipoDeudor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropTipoDeudor.SelectedIndexChanged
        Try
            TipoDeudor()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        Try

            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Pagador", TipoDeMensaje._Informacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verificador", TipoDeMensaje._Informacion)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            If Txt_Dig_Deu.Text.ToUpper <> FC.VrutCambio(Txt_Rut_Deu.Text, DropTipoIdentificacion.SelectedValue).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, " NIT incorrecto", TipoDeMensaje._Informacion)
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            'If Not IsNothing(CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT))) Then
            '    Msj.Mensaje(Me.Page, Caption, " Pagador Ya Existe", TipoDeMensaje._Informacion)
            '    Txt_Rut_Deu.Text = ""
            '    Txt_Dig_Deu.Text = ""
            '    Txt_Rut_Deu.Focus()
            'End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DropTipoIdentificacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropTipoIdentificacion.SelectedIndexChanged

        'If DropTipoIdentificacion.SelectedValue > 0 Then

        Dim coll_identificacion As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIdentificacion, _
                                                                               DropTipoIdentificacion.SelectedValue)


        'If coll_identificacion(1).pnu_vld_dig = "N" Then
        '    Txt_Dig_Deu.Text = "0"
        Txt_Dig_Deu.ReadOnly = True
        Txt_Dig_Deu.CssClass = "clsDisabled"
        'Else
        'Txt_Dig_Deu.CssClass = "clcDisabled"
        'Txt_Dig_Deu.ReadOnly = False
        'Txt_Dig_Deu.CssClass = "clsMandatorio"
        'Txt_Dig_Deu.Text = "7"
        'End If

        DropTipoDeudor.Focus()

        'End If

    End Sub

    Protected Sub DropSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropSucursal.SelectedIndexChanged

        Try
            'DropSucursal
            If DropSucursal.SelectedIndex > 0 Then

                Dim suc As suc_cls = CG.SucursalDevuelve(DropSucursal.SelectedValue)

                If suc.suc_dug <> Nothing Then

                    Dim zon As suc_cls = CG.ZonaBancaTerritorialDevuelve(30, suc.SUC_DUG)
                    'Txt_Zona.Text = zon.suc_cod_ftg & " " & zon.suc_nom '

                    If suc.suc_dug <> Nothing Then
                        Dim ter As suc_cls = CG.ZonaBancaTerritorialDevuelve(50, zon.SUC_TERR_ITO)
                        'Txt_Territorial.Text = ter.suc_cod_ftg & " " & ter.suc_nom '

                        If suc.suc_dug <> Nothing Then
                            'Dim ban As suc_cls = CG.ZonaBancaTerritorialDevuelve(60, ter.suc_area_ope)
                            'Txt_Banca.Text = ban.suc_cod_ftg & " " & ban.suc_nom
                        End If
                    End If

                End If

            Else
                'Txt_Zona.Text = ""
                Txt_Banca.Text = ""
                'Txt_Territorial.Text = ""
            End If

            'Txt_Cod_Ges.Text = ""
            'Txt_Ema_Ges.Text = ""

            'CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 29)
            'CG.EjecutivosDevuelve(DP_Eje_Fac, CodEje, 15)
            'CG.GestorDevuelve(DropSucursal.SelectedValue, DP_Gestor)
            'CG.GestorNegocioDevuelve(DropSucursal.SelectedValue, DP_GestorNeg)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try

    End Sub

    'Protected Sub DP_Canal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Canal.SelectedIndexChanged
    '    'Canal
    '    CG.SubCanalDevuelve(Me.DP_Canal.SelectedValue, DP_SubCanal)

    'End Sub

    'Protected Sub DP_Gestor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Gestor.SelectedIndexChanged

    '    'Gestores
    '    Txt_Cod_Ges.Text = DP_Gestor.SelectedValue

    'End Sub

    'Protected Sub DP_GestorNeg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_GestorNeg.SelectedIndexChanged

    '    'Gestores
    '    'Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(DP_GestorNeg.SelectedValue).eje_mail
    '    'Txt_Ema_Ges.ReadOnly = False
    '    'Txt_Ema_Ges.CssClass = "clsMandatorio"

    'End Sub

#End Region

#Region "BOTONERAS"

    Protected Sub AgregaCli_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub LnkBtnTraeDDR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkBtnTraeDDR.Click
        Try

            CargaGrillaCliXdeu("T")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub IB_Contactos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Contactos.Click
        Try

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "../../Contactos/Contactos.aspx?Rut= " & Txt_Rut_Deu.Text & "", "Contactos", 630, 630, 400, 200)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Contactos/Contactos.aspx?Rut= " & Txt_Rut_Deu.Text & "', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:450px;dialogLeft:400px;dialogTop:200px');")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try

    End Sub

    Protected Sub IB_AgrCli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AgrCli.Click
        Try
            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "AgregarCliente.aspx", "Agregar Cliente", 600, 420, 400, 200)
            'EjecutaJScript(Me, "var x=window.showModalDialog('AgregarCliente.aspx', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:320px;dialogLeft:400px;dialogTop:200px');")
            'FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "../../Ayudas/AyudaCli.aspx", "Agregar Cliente", 600, 450, 400, 200)
            'EjecutaJScript(Me, "var x=window.showModalDialog('../../Ayudas/AyudaCli.aspx', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:320px;dialogLeft:400px;dialogTop:200px');")
        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

        Try

            Response.Redirect("MDeudores.aspx", False)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Validacion() Then

                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "Se ha producido el siguiente error:" & ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        '/*/*/*/*/*//*/*/*/*/*/*/'''''''''''''''''''''''''''''''''''''''/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
        Dim Deu As deu_cls = CargaDeudor()

        If Not agt.ValidaAccesso(20, 20040201, Usr, "PRESIONO GUARDAR DEUDOR " & Deu.deu_ide) Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim conf As String
        conf = "Registro Insertado"

        If Sw.Text = "INSERT" Then

            If AG.DeudorInserta(Deu, IIf(DropCiudadDeu.SelectedIndex > 0, DropCiudadDeu.SelectedValue, 0), Txt_Ema_Ges.Text) Then

                Msj.Mensaje(Me.Page, Caption, "Registro Insertado", TipoDeMensaje._Informacion, "location.href = 'MDeudores.aspx'")

                'Msj.Mensaje(Me.Page, Caption, "Registro Insertado", TipoDeMensaje._Informacion)
                'rw.MensajeRedirecciónScript(this.Page, "La solicitud se ingresó con Exito!!!!!!", "SolicitarSala.aspx");
                '  Response.Redirect("MDeudores.aspx", False)
                'Mensaje_Conf('"Registro Insertado"','" &  Response.Redirect("MDeudores.aspx") "')

                'FuncionesGenerales.RutinasWeb.Mensaje_Conf(Me.Page, "Registro Insertado", "MDeudores.aspx")

            Else
                Msj.Mensaje(Me.Page, Caption, "Problemas al insertar registro", TipoDeMensaje._Informacion)

            End If

        ElseIf Sw.Text = "UPDATE" Then

            If AG.DeudorUpdate(Deu, IIf(DropCiudadDeu.SelectedIndex > 0, DropCiudadDeu.SelectedValue, 0), Txt_Ema_Ges.Text) Then

                Msj.Mensaje(Me.Page, Caption, "Registro Actualizado", TipoDeMensaje._Informacion)
                Response.Redirect("MDeudores.aspx", False)
            Else
                Msj.Mensaje(Me.Page, Caption, "Problemas al actualizar registro", TipoDeMensaje._Informacion)
            End If

        End If
    End Sub

    Protected Sub IB_Nuevo_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Limpiar()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "Se ha producido el siguiente error:" & ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Pago_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Pago.Click

        Try

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "CalendarioDePago.aspx", "Calendario", 800, 800, 400, 200)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

#End Region

#Region "Sub"

    Public Sub TraeDatosDeudor(ByVal rut As Long)

        Try
            Dim clscli As New ClaseClientes
            Dim Deu As deu_cls
            Dim cli As cli_cls

            Deu = CG.DeudorDevuelvePorRut(Rut)
            cli = clscli.ClientesDevuelve(Rut, Deu.deu_dig_ito)

            If Not IsNothing(cli) Then
                If cli.cli_nro_cli <> "" Then
                    Txt_Nro_Cli.Text = Trim(cli.cli_nro_cli)
                End If
            End If

            If IsNothing(Deu) Then
                Msj.Mensaje(Me.Page, Caption, "No se Encontro el Pagador", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Session("Deudor") = Deu

            With Deu

                Txt_Rut_Deu.CssClass = "clsdisabled"
                Txt_Dig_Deu.CssClass = "clsdisabled"

                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.ReadOnly = True
                Txt_Ema_Ges.ReadOnly = True
                Txt_Rut_Deu.Text = Rut

                'Sesion.RutDeu = Txt_Rut_Deu.Text
                ClsSession.ClsSession.RutDeu = Txt_Rut_Deu.Text

                'Deudor
                Me.Txt_Rut_Deu.Text = Val(.deu_ide)
                Me.Txt_Dig_Deu.Text = FC.Vrut(Txt_Rut_Deu.Text)
                Me.DropTipoDeudor.ClearSelection()

                Me.DropTipoIdentificacion.ClearSelection()
                BuscaCombo(DropTipoIdentificacion, CStr(.id_p_0119))

                BuscaCombo(DropTipoDeudor, CStr(.id_P_0044))
                TipoDeudor()

                Lbl_Nat_Jur.Text = DropTipoDeudor.SelectedItem.ToString()

                'Juridico o Natural
                Me.Txt_Rso_Deu.Text = .deu_rso
                Me.TxtApePat.Text = .deu_ape_ptn
                Me.TxtApeMat.Text = .deu_ape_mtn

                'Antecedentes Generales
                Me.TxtDirDeudor.Text = .deu_dml

                'CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 29, DropSucursal.SelectedValue)
                'CG.EjecutivosDevuelve(DP_Eje_Fac, CodEje, 15, DropSucursal.SelectedValue)

                If Not IsNothing(.suc_cls) Then

                    DropSucursal.ClearSelection()
                    BuscaCombo(Me.DropSucursal, CStr(Val(.id_suc)))

                    'CG.GestorDevuelve(.id_suc, DP_Gestor)
                    'CG.GestorNegocioDevuelve(.id_suc, DP_GestorNeg)

                    If DropSucursal.SelectedIndex > 0 Then

                        Dim suc As suc_cls = CG.SucursalDevuelve(DropSucursal.SelectedValue)

                        'If suc.suc_dug <> Nothing And suc.suc_dug.Trim <> "0" Then

                        '    'Dim zon As suc_cls = CG.ZonaBancaTerritorialDevuelve(30, suc.suc_dug)
                        '    'Txt_Zona.Text = zon.suc_cod_ftg & " " & zon.suc_nom

                        '    If suc.suc_dug <> Nothing Then
                        '        'Dim ter As suc_cls = CG.ZonaBancaTerritorialDevuelve(50, zon.suc_terr_ito)
                        '        'Txt_Territorial.Text = ter.suc_cod_ftg & " " & ter.suc_nom

                        '        If suc.suc_dug <> Nothing Then
                        '            'Dim ban As suc_cls = CG.ZonaBancaTerritorialDevuelve(60, ter.suc_area_ope)
                        '            'Txt_Banca.Text = ban.suc_cod_ftg & " " & ban.suc_nom
                        '        End If
                        '    End If

                        'End If

                    End If

                Else
                    'Txt_Zona.Text = ""
                    Txt_Banca.Text = ""
                    'Txt_Territorial.Text = ""
                End If

                If Not IsNothing(.cmn_cls) Then

                    If Not IsNothing(.id_cmn) Then
                        Me.DropComunaDeu.ClearSelection()

                        'BuscaCombo(Me.DropComunaDeu, CStr(Val(.id_cmn)))
                        'depto
                        Me.DropDepto.ClearSelection()
                        DropDepto.Items.FindByValue(.cmn_cls.ciu_cls.id_p_001).Selected = True

                        'municipio-ciudad
                        CG.MunicipioDevuelve(CInt(DropDepto.SelectedValue), True, DropCiudadDeu)
                        Me.DropCiudadDeu.ClearSelection()
                        DropCiudadDeu.Items.FindByValue(.cmn_cls.id_ciu).Selected = True
                        CG.ComunaDevuelve(DropCiudadDeu.SelectedItem.Value, True, Me.DropComunaDeu)

                        'Comuna
                        Me.DropComunaDeu.ClearSelection()
                        Me.DropComunaDeu.Items.FindByValue(.id_cmn).Selected = True

                    End If

                End If


                If Not IsNothing(.id_PL_000006) Then
                    Me.DropGiroDeu.ClearSelection()
                    BuscaCombo(Me.DropGiroDeu, CStr(.id_PL_000006))
                End If

                If Not IsNothing(.id_P_0063) Then
                    Me.DropAbrRazSoc.ClearSelection()
                    BuscaCombo(Me.DropAbrRazSoc, CStr(Val(.id_P_0063)))
                End If

                If Not IsNothing(.id_P_0064) Then
                    Me.DropActEcoDeu.ClearSelection()
                    BuscaCombo(Me.DropActEcoDeu, CStr(Val(.id_P_0064)))
                End If

                If Not IsNothing(.id_P_0076) Then
                    Me.DropSeg.ClearSelection()
                    BuscaCombo(Me.DropSeg, CStr(Val(.id_P_0076)))
                End If

                'Antecedentes de Cobranza
                If Not IsNothing(.id_P_003) Then
                    Me.DropEstadoDeu.ClearSelection()
                    BuscaCombo(Me.DropEstadoDeu, CStr(Val(.id_P_003)))
                End If

                If Not IsNothing(.id_eje_cod_cob) Then
                    DP_Ejecutivo.ClearSelection()
                    BuscaCombo(DP_Ejecutivo, .id_eje_cod_cob)
                End If

                If Not IsNothing(.id_P_0313) Then
                    DP_Corasu.ClearSelection()
                    BuscaCombo(DP_Corasu, .id_P_0313)
                End If

                TxtGiradoA.Text = .deu_chq_gir

                If IsNothing(.deu_pct_vta) Then
                    TxtVtasDeu.Text = "0.0"
                Else
                    TxtVtasDeu.Text = .deu_pct_vta
                End If

                If Not IsNothing(.deu_ntf) Then

                    If .deu_ntf = "S" Then
                        RB_Not_Si.Checked = True
                        RB_Not_No.Checked = False
                    Else
                        RB_Not_Si.Checked = False
                        RB_Not_No.Checked = True
                    End If

                End If

                If Not IsNothing(.deu_atr_car) Then

                    If .deu_atr_car = "S" Then
                        RB_Carta_Si.Checked = True
                        RB_Carta_No.Checked = False
                    Else
                        RB_Carta_Si.Checked = False
                        RB_Carta_No.Checked = True
                    End If

                End If

                If Trim(.deu_des_car) = "" Then
                    Txt_ATR_CAR.Text = ""
                Else
                    Txt_ATR_CAR.Text = .deu_des_car
                End If

                Txt_ConDef.Text = clscli.ContactosPorDefectoDevuelve(ClsSession.ClsSession.TipoDeContacto, ClsSession.ClsSession.RutDeu)
                'Txt_ConDef.Text = clscli.ContactosPorDefectoDevuelve(Sesion.TipoDeContacto, Sesion.RutDeu)

                If Not IsNothing(.deu_obs_gsn) Then
                    Txt_Obs_Gsn.Text = .deu_obs_gsn
                End If

                If Not IsNothing(.deu_fec_obs) Then
                    Txt_Fec_Obs.Text = FuncionesGenerales.FComunes.FUNFecReg(.deu_fec_obs)
                End If

                If Not IsNothing(.deu_rad_ori_fac) Then

                    If .deu_rad_ori_fac = "S" Then
                        RB_Rad_Si.Checked = True
                        RB_Rad_No.Checked = False
                    Else
                        RB_Rad_Si.Checked = False
                        RB_Rad_No.Checked = True
                    End If

                End If

                If Not IsNothing(.deu_rad_dia_vcto) Then
                    txt_dias_rad.Text = .deu_rad_dia_vcto
                End If

                If Not IsNothing(.id_eje) And DP_Eje_Fac.Items.Count > 0 Then
                    DP_Eje_Fac.ClearSelection()
                    BuscaCombo(DP_Eje_Fac, .id_eje)
                    Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(.id_eje).eje_mail
                    Txt_Ema_Ges.ReadOnly = True
                    Txt_Ema_Ges.CssClass = "clsMandatorio"
                End If

                'If Not IsNothing(.ejecutivo) And DP_Gestor.Items.Count > 0 Then
                '    DP_Gestor.ClearSelection()
                '    BuscaCombo(DP_Gestor, .ejecutivo)
                '    Txt_Cod_Ges.Text = .deu_cod_ges
                'End If

                'codigo gestor negocio
                'If Not IsNothing(.id_eje) And DP_Eje_Fac.Items.Count > 0 Then
                '    Me.DP_Eje_Fac.ClearSelection()
                '    BuscaCombo(Me.DP_Eje_Fac, .id_eje)
                '    Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(.id_eje).eje_mail
                '    Txt_Ema_Ges.ReadOnly = False
                '    Txt_Ema_Ges.CssClass = "clsMandatorio"
                'End If


                Txt_Nro_Cli.Text = .deu_nro_cli

                'If Not IsNothing(.ejecutivo) Then
                '    DP_Gestor.ClearSelection()
                '    BuscaCombo(DP_Gestor, .ejecutivo)
                'End If

                'If Not IsNothing(.DEU_CAN_AL) Then

                '    DP_Canal.ClearSelection()
                '    BuscaCombo(DP_Canal, .DEU_CAN_AL)

                '    CG.SubCanalDevuelve(.DEU_CAN_AL, DP_SubCanal)
                '    If Not IsNothing(.DEU_SUB_CAN_AL) Then
                '        DP_SubCanal.ClearSelection()
                '        BuscaCombo(DP_SubCanal, .DEU_SUB_CAN_AL)
                '    End If

                'End If

            End With

            CargaGrillaCliXdeu("T")

            Me.RBtn_Todos.Checked = True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "Se ha producido el siguiente error:" & ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function CargaDeudor() As deu_cls

        Try

            Dim Deu As New deu_cls

            With Deu

                'Deudor
                .deu_ide = Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT)
                .deu_dig_ito = Txt_Dig_Deu.Text.Trim
                .id_P_0044 = DropTipoDeudor.SelectedValue
                .id_p_0119 = DropTipoIdentificacion.SelectedValue
                .deu_nro_cli = Trim(Txt_Nro_Cli.Text)

                'Tipo de Deudor
                .deu_rso = UCase(Me.Txt_Rso_Deu.Text)
                .deu_ape_ptn = UCase(Me.TxtApePat.Text)
                .deu_ape_mtn = UCase(Me.TxtApeMat.Text)

                'Antecedentes Generales
                .deu_dml = Me.TxtDirDeudor.Text

                If DropCiudadDeu.SelectedIndex > 0 Then
                    If CInt(DropComunaDeu.SelectedValue) > 0 Then
                        .id_cmn = CInt(DropComunaDeu.SelectedValue)
                    Else
                        .id_cmn = Nothing
                    End If
                Else
                    .id_cmn = Nothing
                End If

                .id_P_0063 = IIf(DropAbrRazSoc.SelectedIndex > 0, CInt(DropAbrRazSoc.SelectedValue), Nothing)
                .id_P_0076 = IIf(DropSeg.SelectedIndex > 0, CInt(DropSeg.SelectedValue), Nothing)
                .id_PL_000006 = IIf(DropGiroDeu.SelectedIndex > 0, DropGiroDeu.SelectedValue.Trim(), Nothing)
                .id_P_0064 = IIf(DropActEcoDeu.SelectedIndex > 0, CInt(DropActEcoDeu.SelectedValue), Nothing)
                .id_suc = IIf(DropSucursal.SelectedIndex > 0, CInt(DropSucursal.SelectedValue), Nothing)
                .ID_P_0313 = IIf(DP_Corasu.SelectedIndex > 0, CInt(DP_Corasu.SelectedValue), Nothing)

                'Antecedentes Cobranza
                .id_P_003 = IIf(DropEstadoDeu.SelectedIndex > 0, CInt(DropEstadoDeu.SelectedValue), Nothing)
                .deu_chq_gir = TxtGiradoA.Text.Trim
                .deu_pct_vta = Val(TxtVtasDeu.Text)

                If DP_Eje_Fac.SelectedIndex > 0 Then
                    .id_eje = DP_Eje_Fac.SelectedValue
                End If

                ''If DP_Gestor.SelectedIndex > 0 Then
                ''    .ejecutivo = DP_Gestor.SelectedValue
                ''End If

                '.deu_cod_ges = Txt_Cod_Ges.Text.Trim

                If DP_Ejecutivo.SelectedIndex > 0 Then
                    .id_eje_cod_cob = DP_Ejecutivo.SelectedValue
                End If

                If RB_Not_Si.Checked Then
                    .deu_ntf = CChar("S")
                ElseIf RB_Not_No.Checked Then
                    .deu_ntf = CChar("N")
                End If

                .deu_obs_gsn = Txt_Obs_Gsn.Text.Trim

                If Not IsNothing(Session("Deudor")) Then

                    If Session("Deudor").deu_Obs_Gsn <> Txt_Obs_Gsn.Text Then
                        .deu_fec_obs = Date.Now
                    Else
                        .deu_fec_obs = Session("Deudor").deu_fec_obs
                    End If

                Else
                    .deu_fec_obs = Date.Now
                End If

                'Falta contacto por defecto

                If RB_Carta_Si.Checked Then
                    .deu_atr_car = CChar("S")
                ElseIf RB_Carta_No.Checked Then
                    .deu_atr_car = CChar("N")
                End If

                .deu_des_car = Txt_ATR_CAR.Text

                If RB_Rad_Si.Checked Then
                    .deu_rad_ori_fac = "S"
                Else
                    .deu_rad_ori_fac = "N"
                End If

                If txt_dias_rad.Text = "" Then
                    txt_dias_rad.Text = 0
                End If

                .deu_rad_dia_vcto = CShort(txt_dias_rad.Text)

                'If DP_Canal.SelectedIndex > 0 Then
                '    .DEU_CAN_AL = DP_Canal.SelectedValue
                'End If

                'If DP_SubCanal.SelectedIndex > 0 Then
                '    .DEU_SUB_CAN_AL = DP_SubCanal.SelectedValue
                'End If

                'If DP_GestorNeg.SelectedIndex > 0 Then
                '    .DEU_GEST_COD = DP_GestorNeg.SelectedValue
                'End If

            End With

            Return Deu



        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return Nothing
        End Try

    End Function

    Private Sub CargaGrillaCliXdeu(ByVal TodosActivos As Char)

        Try

            GrClientes.DataSource = New Collection
            GrClientes.DataBind()


            CLSCLI.ClientesDeudoresDevuelve(Replace(Txt_Rut_Deu.Text.Trim, ".", ""), _
                                            TodosActivos, _
                                            True, _
                                            GrClientes, _
                                            3)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try

    End Sub

    Private Sub CargaDrop()

        Try

            CG.ParametrosDevuelve(TablaParametro.TipoIdentificacion, True, Me.DropTipoIdentificacion)
            CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, Me.DropTipoDeudor)
            CG.ParametrosDevuelve(TablaParametro.Region, True, Me.DropDepto) 'FY 23-05-2012
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Me.DP_MON) 'FY 24-05-2012
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Giro, True, Me.DropGiroDeu)
            CG.ParametrosDevuelve(TablaParametro.RazonesSociales, True, Me.DropAbrRazSoc)
            CG.ParametrosDevuelve(TablaParametro.ActividadEconomica, True, Me.DropActEcoDeu)
            CG.ParametrosDevuelve(TablaParametro.EstadoDeudor, True, Me.DropEstadoDeu)
            CG.ParametrosDevuelve(TablaParametro.Segmentos, True, Me.DropSeg)
            CG.SucursalesDevuelve(CodEje, True, Me.DropSucursal)
            'CG.MParametrosDevuelve(id_suc(), Me.DropSucursal)


            CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 39)
            CG.EjecutivosDevuelve(DP_Eje_Fac, CodEje, 15)

            CG.ParametrosDevuelve(TablaParametro.CORASU, True, Me.DP_Corasu)

            'Canal
            'CG.CanalDevuelve(Me.DP_Canal)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub TipoDeudor()

        Try

            If Me.DropTipoDeudor.SelectedItem.Value <> 1 Then
                Lbl_Nat_Jur.Text = "" & DropTipoDeudor.SelectedItem.ToString()
                Label3.Text = "Nombre / Razón Social Pagador"
                Label4.Visible = False
                Label5.Visible = False
                TxtApePat.Visible = False
                TxtApeMat.Visible = False

            Else
                Lbl_Nat_Jur.Text = "" & DropTipoDeudor.SelectedItem.ToString()
                Label3.Text = "Nombre / Razón Social Pagador"

                Label4.Visible = True
                Label5.Visible = True
                TxtApePat.Visible = True
                TxtApeMat.Visible = True

            End If

            Txt_Rso_Deu.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Function Validacion() As Boolean

        Try
            If CInt(DropTipoIdentificacion.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Tipo de Identificación", TipoDeMensaje._Exclamacion)
                Return False
            Else
                If Txt_Rut_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Pagador", TipoDeMensaje._Informacion)
                    Txt_Rut_Deu.Focus()
                    Return False
                End If

                If Txt_Dig_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verificador", TipoDeMensaje._Informacion)
                    Txt_Dig_Deu.Focus()
                    Return False
                End If

                If Not Txt_Dig_Deu.Enabled Then
                    If Txt_Dig_Deu.Text.ToUpper <> FC.Vrut(Txt_Rut_Deu.Text).ToUpper Then
                        Msj.Mensaje(Me.Page, Caption, " Nit incorrecto", TipoDeMensaje._Informacion)
                        Txt_Rut_Deu.Text = ""
                        Txt_Dig_Deu.Text = ""
                        Txt_Rut_Deu.Focus()
                        Return False
                    End If
                End If

            End If

            If DropTipoDeudor.SelectedItem.Value = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar tipo de Pagador", 2)
                DropTipoDeudor.Focus()
                Return False
            End If

            If DP_Corasu.SelectedItem.Value = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar el CORASU", 2)
                DP_Corasu.Focus()
                Return False
            End If

            If DropTipoDeudor.SelectedItem.Value = 1 Then

                If Txt_Rso_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, "Atención", "Debe ingresar nombre", 2)
                    Txt_Rso_Deu.Focus()
                    Return False
                End If

                If TxtApePat.Text = "" Then
                    Msj.Mensaje(Me.Page, "Atención", "Debe ingresar apellido paterno", 2)
                    TxtApePat.Focus()
                    Return False
                End If

                If TxtApeMat.Text = "" Then
                    Msj.Mensaje(Me.Page, "Atención", "Debe ingresar apellido materno", 2)
                    TxtApeMat.Focus()
                    Return False
                End If

            ElseIf DropTipoDeudor.SelectedItem.Value = 2 Then

                If Txt_Rso_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, "Atención", "Debe ingresar razon social", 2)
                    Txt_Rso_Deu.Focus()
                    Return False
                End If

            End If

            If DropAbrRazSoc.SelectedItem.Value = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar Abr. Raz. Soc", 2)
                DropAbrRazSoc.Focus()
                Return False
            End If

            If DropActEcoDeu.SelectedItem.Value = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar Actividad Económica", 2)
                DropActEcoDeu.Focus()
                Return False
            End If

            If DropEstadoDeu.SelectedItem.Value = 0 Then
                Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar estado del Pagador", 2)
                DropEstadoDeu.Focus()
                Return False
            End If

            If TxtVtasDeu.Text <> "" Then
                If CDec(Replace(TxtVtasDeu.Text, ".", ",")) > 100 Then
                    Msj.Mensaje(Me.Page, Caption, " % Anticipo No Puede Ser Mayor A 100 ", TipoDeMensaje._Informacion)
                    TxtVtasDeu.Focus()
                    Return False
                End If
            End If


            If CInt(DropSucursal.SelectedItem.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Sucursal", TipoDeMensaje._Exclamacion)
                Return False
            End If

            'If DP_Gestor.SelectedIndex = 0 Then
            '    Msj.Mensaje(Me.Page, Caption, "Seleccione Gestor Factoring del Pagador", TipoDeMensaje._Exclamacion)
            '    Return False
            'End If

            'If CInt(DP_Canal.SelectedValue) = 0 Then
            '    Msj.Mensaje(Me.Page, Caption, "Seleccione Canal del Pagador", TipoDeMensaje._Exclamacion)
            '    Return False
            'End If

            'If CInt(DP_SubCanal.SelectedValue) = 0 Then
            '    Msj.Mensaje(Me.Page, Caption, "Seleccione Sub Canal del Pagador", TipoDeMensaje._Exclamacion)
            '    Return False
            'End If

            Return True


        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Atención", "Se ha producido el siguiente error:" & ex.Message, 1)
        End Try

    End Function

    Sub Limpiar()

        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        DropTipoDeudor.ClearSelection()
        Txt_Rso_Deu.Text = ""
        Txt_Rso_Deu.Text = ""
        TxtApeMat.Text = ""
        TxtDirDeudor.Text = ""
        DropCiudadDeu.ClearSelection()
        DropComunaDeu.ClearSelection()
        DropGiroDeu.ClearSelection()
        DropAbrRazSoc.ClearSelection()
        DropActEcoDeu.ClearSelection()
        DropSeg.ClearSelection()
        DropSucursal.ClearSelection()
        DP_Eje_Fac.ClearSelection()
        'DP_Gestor.ClearSelection()
        TxtVtasDeu.Text = ""
        TxtGiradoA.Text = ""
        Txt_Obs_Gsn.Text = ""
        CargaDrop()
        Txt_Rut_Deu.Focus()
        NroPaginacion_Deu = 0

    End Sub

#End Region

#Region "Linea cupo global"

    Protected Sub btn_nuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_nuevo.Click
        Txt_Mto_Apr.CssClass = "clsMandatorio"
        Txt_Mto_Apr.ReadOnly = False
        Txt_Obs_Deu.CssClass = "clsMandatorio"
        Txt_Obs_Deu.ReadOnly = False
        botonera_linea_finan(True, False, False)
        HF_ACCION_LIN.Value = "NUEVO"
        DP_MON.SelectedIndex = 0
        Txt_Mto_Apr.Text = ""

        Txt_Fec_Vto.Text = ""
        Txt_Fec_Vto.CssClass = "clsMandatorio"
        Txt_Fec_Vto.ReadOnly = False

    End Sub

    Protected Sub Btn_Con_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Con.Click

        Try
            If Validacion_Mon() Then

                GUARDA_DEU_MON()
                llena_grilla_MON()
                Txt_Mto_Apr.Text = ""
                Txt_Mto_Apr.ReadOnly = True
                Txt_Mto_Apr.CssClass = "clsDisabled"

                Txt_Obs_Deu.Text = ""
                Txt_Obs_Deu.ReadOnly = True
                Txt_Obs_Deu.CssClass = "clsDisabled"

                Txt_Fec_Vto.Text = ""
                Txt_Fec_Vto.ReadOnly = True
                Txt_Fec_Vto.CssClass = "clsDisabled"

                botonera_linea_finan(False, True, False)
                DP_MON.SelectedValue = 0
                HF_ACCION_LIN.Value = ""

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, 2)
        End Try
    End Sub

    Protected Sub Btn_Limp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Limp.Click
        Txt_Mto_Apr.Text = ""
        Txt_Fec_Vto.Text = ""
        botonera_linea_finan(False, True, False)
        DP_MON.Enabled = True
        DP_MON.SelectedIndex = 0
        HF_ACCION_LIN.Value = ""
    End Sub

    Private Sub botonera_linea_finan(ByVal CONF As Boolean, ByVal NUEVO As Boolean, ByVal ELIMINAR As Boolean) 'FY 24-05-2012
        Btn_Con.Enabled = CONF
        btn_nuevo.Enabled = NUEVO
    End Sub

    Private Function Validacion_Mon() As Boolean

        Validacion_Mon = False

        If DP_MON.SelectedIndex = 0 Then
            Msj.Mensaje(Me, Caption, "Selecciones tipo de moneda", 2)
            Exit Function
        End If

        If Txt_Fec_Vto.Text = "" Then
            Msj.Mensaje(Me, Caption, "Ingrese la fecha de vencimiento del cupo", 2)
            Exit Function
        Else

            If Not IsDate(Txt_Fec_Vto.Text) Then
                Msj.Mensaje(Me, Caption, "Fecha de vencimiento del cupo no valida", 2)
                Exit Function
            End If

            If CDate(Txt_Fec_Vto.Text) < DateTime.Now Then
                Msj.Mensaje(Me, Caption, "Fecha de vencimiento del cupo no puede ser menor a hoy", 2)
                Exit Function
            End If

            If Txt_Fec_Vto.Text = DateTime.Now.ToShortDateString() Then
                Msj.Mensaje(Me, Caption, "Fecha de vencimiento del cupo no puede ser igual a hoy", 2)
                Exit Function
            End If

        End If

        If Txt_Mto_Apr.Text = "" Then
            Msj.Mensaje(Me, Caption, "Ingrese Monto a aprobar", 2)
            Exit Function
        End If

        'Se agrega validacion de que no puede crear una nueva linea menor a lo ocupado
        Dim ocupado As Double = 0
        Dim nuevomonto As Double = 0

        nuevomonto = Txt_Mto_Apr.Text.Replace(".", "").Replace(",", ".")

        For I = 0 To GV_MON_DEU.Rows.Count - 1
            Dim lb As LinkButton = CType(GV_MON_DEU.Rows(I).FindControl("LB_Mto_Ocu"), LinkButton)
            ocupado = lb.Text.Replace(".", "").Replace(",", ".")
            ocupado = lb.Text.Replace(".", "").Replace(",", ".")
            Exit For
        Next

        If (nuevomonto < ocupado) Then
            Msj.Mensaje(Me, Caption, "Monto a aprobar debe ser mayor al utilizado", 2)
            Exit Function
        End If

        Validacion_Mon = True

    End Function

    Private Sub llena_grilla_MON() 'FY 24-05-2012

        Try

            Coll_Cupo = CG.Devuelvelineaglobaldeudor(CLng(Txt_Rut_Deu.Text))

            GV_MON_DEU.DataSource = Coll_Cupo
            GV_MON_DEU.DataBind()

            If GV_MON_DEU.Rows.Count > 0 Then
                FORMATEO_GRILLA()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, 2)
        End Try

    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim img As ImageButton
        Try
            If btn.ToolTip <> "" Then
                For i = 0 To GV_MON_DEU.Rows.Count - 1
                    HF_ACCION_LIN.Value = "MODIFICA"
                    img = GV_MON_DEU.Rows(i).FindControl("Img_ver")
                    If btn.ToolTip = img.ToolTip Then
                        marca_grilla_finan(btn.ToolTip)
                        DP_MON.SelectedValue = btn.ToolTip
                        DP_MON.Enabled = False
                        DP_MON_SelectedIndexChanged(Me, e)
                        Txt_Mto_Apr.Text = GV_MON_DEU.Rows(i).Cells(2).Text
                        Txt_Fec_Vto.Text = GV_MON_DEU.Rows(i).Cells(6).Text
                        botonera_linea_finan(False, True, True)
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, 2)
        End Try
    End Sub

    Protected Sub FORMATEO_GRILLA() 'FY 25-05-2012
        For i = 1 To Coll_Cupo.Count

            If Coll_Cupo.Item(i).codigo <> 0 Then

                GV_MON_DEU.Rows(i - 1).Cells(1).Text = Format(CDbl(GV_MON_DEU.Rows(i - 1).Cells(1).Text), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))
                GV_MON_DEU.Rows(i - 1).Cells(2).Text = Format(CDbl(GV_MON_DEU.Rows(i - 1).Cells(2).Text), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))
                'GV_MON_DEU.Rows(i - 1).Cells(4).Text = Format(If(GV_MON_DEU.Rows(i - 1).Cells(4).Text = "&nbsp;", 0, CDbl(GV_MON_DEU.Rows(i - 1).Cells(4).Text)), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))
                'GV_MON_DEU.Rows(i - 1).Cells(5).Text = Format(If(GV_MON_DEU.Rows(i - 1).Cells(5).Text = "&nbsp;", 0, CDbl(GV_MON_DEU.Rows(i - 1).Cells(5).Text)), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))

                Dim LB_Mto_Ocu As LinkButton = CType(GV_MON_DEU.Rows(i - 1).FindControl("LB_Mto_Ocu"), LinkButton)
                Dim LB_Mto_CR As LinkButton = CType(GV_MON_DEU.Rows(i - 1).FindControl("LB_Mto_CR"), LinkButton)
                Dim LB_Mto_SR As LinkButton = CType(GV_MON_DEU.Rows(i - 1).FindControl("LB_Mto_SR"), LinkButton)

                LB_Mto_Ocu.Enabled = False
                LB_Mto_CR.Enabled = False
                LB_Mto_SR.Enabled = False

                If Coll_Cupo.Item(i).id_estado <> 1 Then
                    LB_Mto_Ocu.Text = 0
                    LB_Mto_CR.Text = 0
                    LB_Mto_SR.Text = 0
                    GV_MON_DEU.Rows(i - 1).Cells(4).Text = 0
                    GV_MON_DEU.Rows(i - 1).Cells(5).Text = 0

                Else

                    If CDbl(LB_Mto_Ocu.Text) > 0 Then
                        LB_Mto_Ocu.Enabled = True
                    End If

                    If CDbl(LB_Mto_CR.Text) > 0 Then
                        LB_Mto_CR.Enabled = True
                    End If

                    If CDbl(LB_Mto_SR.Text) > 0 Then
                        LB_Mto_SR.Enabled = True
                    End If

                    LB_Mto_Ocu.Text = Format(CDbl(LB_Mto_Ocu.Text), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))
                    LB_Mto_CR.Text = Format(CDbl(LB_Mto_CR.Text), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))
                    LB_Mto_SR.Text = Format(CDbl(LB_Mto_SR.Text), FC.DevuelveFormatoMoneda(Coll_Cupo.Item(i).codigo))

                End If

            End If

        Next
    End Sub

    Protected Sub Btn_Elimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Btn_Elimina.Click

        If Not agt.ValidaAccesso(20, 20030201, Usr, "PRESIONO NUEVO DEUDOR") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If AG.DeuMonBorra(CLng(Txt_Rut_Deu.Text), DP_MON.SelectedValue) Then

            llena_grilla_MON()

            Txt_Mto_Apr.Text = ""
            Txt_Mto_Apr.ReadOnly = True
            Txt_Mto_Apr.CssClass = "clsDisabled"

            Txt_Fec_Vto.Text = ""
            Txt_Fec_Vto.ReadOnly = True
            Txt_Fec_Vto.CssClass = "clsDisabled"

            Txt_Obs_Deu.Text = ""
            Txt_Obs_Deu.ReadOnly = True
            Txt_Obs_Deu.CssClass = "clsDisabled"

            botonera_linea_finan(False, True, False)
            DP_MON.SelectedValue = 0
            DP_MON.Enabled = True
            HF_ACCION_LIN.Value = ""

        End If

    End Sub

    Protected Sub marca_grilla_finan(ByVal NUM As Integer) 'FY 25-05-2012
        Dim img As ImageButton

        For i = 0 To GV_MON_DEU.Rows.Count - 1
            img = GV_MON_DEU.Rows(i).FindControl("Img_Ver")
            If NUM = img.ToolTip Then
                If (i Mod 2) = 0 Then
                    GV_MON_DEU.Rows(i).CssClass = "selectable"

                Else
                    GV_MON_DEU.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    GV_MON_DEU.Rows(i).CssClass = "formatUltcell"
                Else
                    GV_MON_DEU.Rows(i).CssClass = "formatUltcellAlt"
                End If

            End If
        Next

    End Sub

    Protected Sub DP_MON_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_MON.SelectedIndexChanged

        Dim mto As Double
        MaskedEditExtender_Txt_Mto_Apr.Mask = Nothing

        If Txt_Mto_Apr.Text <> "" Then
            mto = Txt_Mto_Apr.Text.Replace(".", "").Replace(",", ".")
        End If

        Select Case DP_MON.SelectedValue
            Case 1
                MaskedEditExtender_Txt_Mto_Apr.Mask = "999,999,999,999,999"
            Case 2
                'MaskedEditExtender_Txt_Mto_Apr.Mask = "999,999,999,999,999.9999"
                MaskedEditExtender_Txt_Mto_Apr.Mask = "9,999,999,999,999.99"
            Case 3, 4
                MaskedEditExtender_Txt_Mto_Apr.Mask = "9,999,999,999,999.99"
            Case Else
                MaskedEditExtender_Txt_Mto_Apr.Mask = "999,999,999,999,999"
        End Select

    End Sub

    Function GUARDA_DEU_MON() As Boolean

        Try

            If Not agt.ValidaAccesso(20, 20030201, Usr, "PRESIONO NUEVO DEUDOR") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Function
            End If

            Dim deu_mon As deu_mon_cls

            deu_mon = New deu_mon_cls

            Dim ocupado As Double = 0
            Dim nuevomonto As Double = 0

            nuevomonto = Txt_Mto_Apr.Text.Replace(".", "").Replace(",", ".")

            For I = 0 To GV_MON_DEU.Rows.Count - 1
                If GV_MON_DEU.Rows(I).Cells(5).Text = "VIGENTE" Then
                    Dim lb As LinkButton = CType(GV_MON_DEU.Rows(I).FindControl("LB_Mto_Ocu"), LinkButton)
                    ocupado = lb.Text.Replace(".", "").Replace(",", ".")
                End If
            Next

            With deu_mon
                .deu_ide = Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT)
                .id_p_0023 = DP_MON.SelectedValue
                .deu_mon_apr = Txt_Mto_Apr.Text
                .deu_mon_dis = .deu_mon_apr - ocupado
                .deu_mon_ocu = ocupado
                .id_p_0029 = 1
                .id_eje = CodEje
                .deu_mon_obs = Txt_Obs_Deu.Text.Trim()
                .deu_mon_fec = Date.Now
                .deu_fec_vto = Txt_Fec_Vto.Text
            End With

            If AG.DeuMoninserta(deu_mon) Then

                HF_ACCION_LIN.Value = ""
                botonera_linea_finan(False, True, False)
                Return True

            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Sub LB_Mto_Ocu_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tabla As New DataLineaCredito.sp_DevuelveDetalleCuposPagadorDataTable
        Dim adaptador As New DataLineaCreditoTableAdapters.sp_DevuelveDetalleCuposPagadorTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Adm.Deudores\Reportes\CupoPagador.rdlc"

        tabla = adaptador.GetData(Format(CLng(Txt_Rut_Deu.Text.Trim()), Var.FMT_RUT), _
                                  CInt(sender.tooltip))

        Dim origen As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = tabla

        origen = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_sp_DevuelveDetalleCuposPagador", dt)

        ReportViewer1.LocalReport.DataSources.Add(origen)
        ReportViewer1.DataBind()

        Modal_CupoPagador.Show()

    End Sub

    Protected Sub LB_Mto_CR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tabla As New DataLineaCredito.sp_DevuelveDetalleCuposPagadorRecursoDataTable
        Dim adaptador As New DataLineaCreditoTableAdapters.sp_DevuelveDetalleCuposPagadorRecursoTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Adm.Deudores\Reportes\CupoPagadorRecurso.rdlc"

        tabla = adaptador.GetData(Format(CLng(Txt_Rut_Deu.Text.Trim()), Var.FMT_RUT), _
                                  CInt(sender.tooltip), _
                                  "1")

        Dim origen As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = tabla

        origen = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_sp_DevuelveDetalleCuposPagadorRecurso", dt)

        ReportViewer1.LocalReport.DataSources.Add(origen)
        ReportViewer1.DataBind()

        Modal_CupoPagador.Show()

    End Sub

    Protected Sub LB_Mto_SR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tabla As New DataLineaCredito.sp_DevuelveDetalleCuposPagadorRecursoDataTable
        Dim adaptador As New DataLineaCreditoTableAdapters.sp_DevuelveDetalleCuposPagadorRecursoTableAdapter

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.Reset()
        ReportViewer1.LocalReport.ReportPath = "Modulos\Adm.Deudores\Reportes\CupoPagadorRecurso.rdlc"

        tabla = adaptador.GetData(Format(CLng(Txt_Rut_Deu.Text.Trim()), Var.FMT_RUT), _
                                  CInt(sender.tooltip), _
                                  "0")

        Dim origen As New Microsoft.Reporting.WebForms.ReportDataSource

        Dim dt As DataTable

        dt = tabla

        origen = New Microsoft.Reporting.WebForms.ReportDataSource("DataLineaCredito_sp_DevuelveDetalleCuposPagadorRecurso", dt)

        ReportViewer1.LocalReport.DataSources.Add(origen)
        ReportViewer1.DataBind()

        Modal_CupoPagador.Show()

    End Sub

    Protected Sub Txt_Rut_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Rut_Deu.TextChanged

        Txt_Dig_Deu.Text = FC.Vrut(Txt_Rut_Deu.Text.Trim())


        If Txt_Rut_Deu.Text.Length() > 0 Then

            If Not IsNothing(CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text.Trim())) Then
                Msj.Mensaje(Page, "Administración Clentes", "NIT ya existe", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Text = ""
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            'FC.Vrut(Txt_Rut_Deu.Text)

            Dim cli As cli_cls
            Dim ciu As ciu_cls

            cli = CLSCLI.ClienteDevuelveUnificado(CLng(Txt_Rut_Deu.Text.Trim()))
            ciu = CLSCLI.ClienteCiudadDevuelveUnificado(CLng(Txt_Rut_Deu.Text.Trim()))

            If Not IsNothing(cli) Then

                Txt_Dig_Deu.Text = cli.cli_dig_ito
                Txt_Nro_Cli.Text = cli.cli_nro_cli
                Txt_Rso_Deu.Text = cli.cli_rso
                Txt_Raz_Soc.Value = cli.cli_rso
                TxtApePat.Text = cli.cli_ape_ptn
                TxtApeMat.Text = cli.cli_ape_ptn

                DropTipoIdentificacion.ClearSelection()
                DP_Corasu.ClearSelection()
                DropTipoDeudor.ClearSelection()

                DropTipoIdentificacion.Items.FindByValue(cli.id_P_0119).Selected = True
                DP_Corasu.Items.FindByValue(cli.id_P_0313).Selected = True
                DropTipoDeudor.Items.FindByValue(cli.id_P_0044).Selected = True

                If Not IsNothing(ciu) Then

                    CG.MunicipioDevuelve(ciu.id_p_001, True, DropCiudadDeu)

                    DropCiudadDeu.ClearSelection()
                    DropDepto.ClearSelection()

                    DropDepto.Items.FindByValue(ciu.id_p_001).Selected = True
                    DropCiudadDeu.Items.FindByValue(ciu.id_ciu).Selected = True

                    CG.ComunaDevuelve(Me.DropCiudadDeu.SelectedItem.Value, True, Me.DropComunaDeu)
                    DropComunaDeu.ClearSelection()
                    DropComunaDeu.SelectedIndex = 1

                End If

                TipoDeudor()

                Dim coll_identificacion As Collection = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoIdentificacion, _
                                                                                       DropTipoIdentificacion.SelectedValue)

                If coll_identificacion(1).pnu_vld_dig = "N" Then
                    Txt_Dig_Deu.Text = "0"
                    Txt_Dig_Deu.ReadOnly = True
                    Txt_Dig_Deu.CssClass = "clsDisabled"
                End If

                '@GIRO VARCHAR(20),    
                If Not IsNothing(cli.id_PL_000006) Then
                    Me.DropGiroDeu.ClearSelection()
                    BuscaCombo(Me.DropGiroDeu, CStr(cli.id_PL_000006))
                End If

                CG.ActividadEconomicaDevuelve(DropGiroDeu.SelectedValue, DropActEcoDeu)

                '@ACT_ECO INT,    
                If Not IsNothing(cli.id_P_0064) Then
                    Me.DropActEcoDeu.ClearSelection()
                    BuscaCombo(Me.DropActEcoDeu, CStr(Val(cli.id_P_0064)))
                End If

                '@SEXO INT,    
                '@COD_SUC VARCHAR(10),    
                If Not IsNothing(cli.id_suc) Then
                    Me.DropSucursal.ClearSelection()
                    BuscaCombo(Me.DropSucursal, CStr(Val(cli.id_suc)))
                End If

                '@COD_GESTOR VARCHAR(20),    
                'Txt_Cod_Ges.Text = cli.cli_cod_ges

                '@EJE_OFICINA VARCHAR(150),    
                'Txt_Eje_Bco.Text = cli.cli_eje_ofc

                '@COD_EJE VARCHAR(10),      
                If Not IsNothing(cli.id_eje_cod_eje) Then
                    Me.DP_Ejecutivo.ClearSelection()
                    BuscaCombo(Me.DP_Ejecutivo, CStr(Val(cli.id_eje_cod_eje)))
                End If

                '@CORREO VARCHAR(50))     


            Else
                Txt_Nro_Cli.Text = ""
                Txt_Rso_Deu.Text = ""
                Txt_Raz_Soc.Value = ""
                TxtApePat.Text = ""
                TxtApeMat.Text = ""

                DP_Corasu.ClearSelection()
                DropTipoDeudor.ClearSelection()
                DropCiudadDeu.ClearSelection()
                DropDepto.ClearSelection()
            End If

        End If

    End Sub

#End Region

    Protected Sub DP_Eje_Fac_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DP_Eje_Fac.SelectedIndexChanged
        'Dim Deu As deu_cls
        'Deu = CG.DeudorDevuelvePorRut(Rut)
        'If Not IsNothing(.id_eje) And DP_Eje_Fac.Items.Count > 0 Then
        '    Me.DP_Eje_Fac.ClearSelection()
        '    BuscaCombo(Me.DP_Eje_Fac, .id_eje)
        '    Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(.id_eje).eje_mail
        '    Txt_Ema_Ges.ReadOnly = False
        '    Txt_Ema_Ges.CssClass = "clsMandatorio"
        'End If
        'If Not DP_Eje_Fac.Items.Count > 0 Then
        '    DP_Eje_Fac.ClearSelection()
        '    BuscaCombo(DP_Eje_Fac, DP_Eje_Fac.SelectedValue)
        '    Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(DP_Eje_Fac.SelectedValue).eje_mail
        '    Txt_Ema_Ges.ReadOnly = True
        '    Txt_Ema_Ges.CssClass = "clsMandatorio"
        'End If
        If DP_Eje_Fac.SelectedValue <> 0 Then
            Txt_Ema_Ges.Text = CG.EjecutivoDevuelve(DP_Eje_Fac.SelectedValue).eje_mail
            Txt_Ema_Ges.ReadOnly = False
            Txt_Ema_Ges.CssClass = "clsMandatorio"
        End If
    End Sub
End Class


