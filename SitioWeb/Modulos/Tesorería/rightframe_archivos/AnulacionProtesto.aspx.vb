Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos


Partial Class Modulos_Tesorería_rightframe_archivos_AnulacionProtesto
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra Tesoreria"
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Pagos As New ClsSession.SesionPagos
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
    Dim clasecli As New ClaseClientes
    Dim PGO As New ClasePagos
    Dim sesionPgo As New ClsSession.SesionPagos


#End Region

#Region "BOTONERA"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        BloqueaBusqueda(False)
        LimpiaCriterio()
        LimpiaGridView()

        GV_Pagos.DataSource = Nothing
        GV_Pagos.DataBind()

        GV_Aplicaciones.DataSource = Nothing
        GV_Aplicaciones.DataBind()

        TabContainer1.Tabs(0).Enabled = True
        TabContainer1.Tabs(1).Enabled = True

        'Tb_Pagos.Visible = True
        'Tb_Aplicaciones.Visible = False

        HF_Id_Dpo.Value = ""
        HF_ID_APL.Value = ""
        BloqueaCriterioAplicaciones(false)
        RB_Clientes.SelectedValue = 0
        Txt_Fec_Apl_Dsd.Text = ""
        Txt_Fec_Apl_Hst.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"

        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"

        Txt_Rut_Cli_MaskedEditExtender.Enabled = False

        Ib_ayu_cli.Enabled = False

        NroPaginacion = 0

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

        Try
            If Not Agt.ValidaAccesso(20, 20040211, Usr, "PRESIONA BOTON IMPRIMIR PAGO") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Select Case TabContainer1.ActiveTabIndex
                Case 0 'Pagos/Rec
                    If Val(HF_id_ing.Value) > 0 Then
                        RW.AbrePopup(Me, 1, "../../Pagos/rightframe_archivos/InformeDePagos.aspx?id=" & HF_id_ing.Value, "Pagos", 1280, 800, 10, 10)
                    Else
                        Msj.Mensaje(Page, Caption, "Debe seleccionar un pago", TipoDeMensaje._Informacion)
                    End If

                Case 1 'Aplicaciones
                    If Val(HF_ID_APL.Value) > 0 Then
                        RW.AbrePopup(Me, 1, "../../Carp.%20Comercial/rigthframe_archivos/Informe%20Aplicaciones.aspx?id_apl=" & HF_ID_APL.Value & "&rut=" & HF_RUT_CLI.Value & " ", "Pagos", 1280, 800, 10, 10)
                    Else
                        Msj.Mensaje(Page, Caption, "Debe seleccionar una aplicación", TipoDeMensaje._Informacion)
                    End If

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_BuscarPagos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_BuscarPagos.Click

        Try

            If Not Agt.ValidaAccesso(20, 20010211, Usr, "PRESIONA BOTON BUSCAR PAGO") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            NroPaginacion = 0

            Select Case TabContainer1.ActiveTabIndex
                Case 0 'Pagos/Rec
                    'Tb_Pagos.Visible = True
                    'Tb_Aplicaciones.Visible = False
                    'TabContainer1.Tabs(0).Enabled = True
                    'TabContainer1.Tabs(1).Enabled = False
                    BuscarPagos()

                Case 1 'Aplicacion
                    'Tb_Pagos.Visible = False
                    'Tb_Aplicaciones.Visible = True
                    'TabContainer1.Tabs(0).Enabled = False
                    'TabContainer1.Tabs(1).Enabled = True
                    BuscaAplicacion()
            End Select


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Protesto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Protesto.Click

        Try

            If Not Agt.ValidaAccesso(20, 20020211, Usr, "PRESIONA BOTON ANULAR PROTESTO") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function ValidaPagosMarcado() As Boolean

        For I = 0 To GV_Pagos.Rows.Count - 1

            If CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                Return True
            End If
        Next

        Return False

    End Function

    Protected Sub IB_Anular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Anular.Click

        If Not Agt.ValidaAccesso(20, 20030211, Usr, "PRESIONA BOTON ANULAR PAGO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Val(HF_ID_APL.Value) > 0 Then
            Msj.Mensaje(Page, Caption, "¿Desea anular la aplicación?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Anular.UniqueID, True)
            Exit Sub
        End If

    End Sub

    Protected Sub LB_Anular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Anular.Click

        Try

            If PGO.AnulaAplicaciones(HF_ID_APL.Value) Then
                BuscaAplicacion()
                Msj.Mensaje(Page, Caption, "Registro anulado", ClsMensaje.TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function ValidaCondicionesDelPago() As Boolean
        Try

            Dim EXISTEN_DATOS As Boolean
            Dim Existen_Pagos As Boolean
            Dim DOCTOS As String

            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            'VALIDAMOS SI SE PUEDE ANULAR O PROTESTAR UN PAGO, PARA ESO REVISAMOS SI EXITEN CUENTAS PENDIENTES
            '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            Dim Coll_Exced As Collection

            DOCTOS = ""
            EXISTEN_DATOS = False

            Coll_Exced = PGO.PagosValidaParaEliminar(HF_Id_Dpo.Value, "EXCED")

            If Coll_Exced.Count > 0 Then
                EXISTEN_DATOS = True
                HF_RutCliente.Value = Format(Coll_Exced.Item(1).Rutcli, "###,###,###") & "-" & RC.Vrut(Coll_Exced.Item(1).Rutcli)
                DOCTOS = DOCTOS & "Cliente :" & Format(Coll_Exced.Item(1).Rutcli, "###,###,###") & "-" & RC.Vrut(Coll_Exced.Item(1).Rutcli) & " " & Coll_Exced.Item(1).Rutcli & " " & Coll_Exced.Item(1).Nombre & _
                                  " N° Docto: " & Coll_Exced.Item(1).Num_Doc & "-" & Coll_Exced.Item(1).flj_num & Chr(13)
            End If

            If EXISTEN_DATOS Then
                Msj.Mensaje(Page, Caption, "Los siguientes doctos. estan siendo devueltos. Se debe rechazar aplicación: " & Chr(13) & DOCTOS, TipoDeMensaje._Exclamacion)
                'VALIDA_CONDICIONES_DEL_PAGO = True
                Return False
            End If

            '************************************************************************************************************************************************************************
            Dim Coll_NOCED As Collection

            DOCTOS = ""
            EXISTEN_DATOS = False

            Coll_NOCED = PGO.PagosValidaParaEliminar(HF_Id_Dpo.Value, "NOCED")

            If Coll_NOCED.Count > 0 Then
                EXISTEN_DATOS = True
                DOCTOS = DOCTOS & "Cliente :" & Format(Coll_Exced.Item(1).Rutcli, "############") & "-" & RC.Vrut(Coll_Exced.Item(1).Rutcli) & " " & Coll_Exced.Item(1).Rutcli & " " & Coll_Exced.Item(1).Nombre & _
                                  " N° Docto: " & Coll_Exced.Item(1).Num_Doc & Chr(13)
            End If

            If EXISTEN_DATOS Then
                Msj.Mensaje(Page, Caption, "Los siguientes doctos. No cedidos estan siendo devueltos. Se debe rechazar aplicación: " & Chr(13) & DOCTOS, TipoDeMensaje._Exclamacion)
                'VALIDA_CONDICIONES_DEL_PAGO = True
                Return False
            End If

            '************************************************************************************************************************************************************************
            Dim Coll_CXPPA As Collection

            DOCTOS = ""
            EXISTEN_DATOS = False

            Coll_CXPPA = PGO.PagosValidaParaEliminar(HF_Id_Dpo.Value, "CXPPA")

            If Coll_CXPPA.Count > 0 Then
                EXISTEN_DATOS = True
                DOCTOS = DOCTOS & "Cliente :" & Format(Coll_CXPPA.Item(1).Rutcli, "############") & "-" & RC.Vrut(Coll_CXPPA.Item(1).Rutcli) & " " & Coll_CXPPA.Item(1).Nombre & _
                                  " N° Cuenta: " & Coll_CXPPA.Item(1).cxp_num & Chr(13)
            End If

            If EXISTEN_DATOS Then
                Msj.Mensaje(Page, Caption, "Las siguientes CXP (ingresadas) estan siendo devueltas. Se debe rechazar aplicación: " & Chr(13) & DOCTOS, TipoDeMensaje._Exclamacion)
                'VALIDA_CONDICIONES_DEL_PAGO = True
                Return False
            End If
            '************************************************************************************************************************************************************************

            Dim Coll_CXPMA As Collection

            DOCTOS = ""
            EXISTEN_DATOS = False

            Coll_CXPMA = PGO.PagosValidaParaEliminar(HF_Id_Dpo.Value, "CXPMA")

            If Coll_CXPMA.Count > 0 Then
                EXISTEN_DATOS = True
                DOCTOS = DOCTOS & "Cliente :" & Format(Coll_CXPMA.Item(1).Rutcli, "############") & "-" & RC.Vrut(Coll_CXPMA.Item(1).Rutcli) & " " & Coll_CXPMA.Item(1).Nombre & _
                                  " N° Cuenta: " & Coll_CXPMA.Item(1).cxp_num & Chr(13)
            End If

            If EXISTEN_DATOS Then
                Msj.Mensaje(Page, Caption, "Las siguientes CXP (automaticas) estan siendo devueltas. Se debe rechazar aplicación: " & Chr(13) & DOCTOS, TipoDeMensaje._Exclamacion)
                'VALIDA_CONDICIONES_DEL_PAGO = True
                Return False
            End If
            '************************************************************************************************************************************************************************

            Return True

        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "EVENTOS DEL CLIENTE"

    Protected Sub LB_BuscarClienteApli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarClienteApli.Click
        Try

          

            Dim Cliente As cli_cls

            Cliente = clasecli.ClientesDevuelve(CLng(Txt_Rut_Cli_Apl.Text), Me.Txt_Dig_Cli_Apl.Text)

            If valida_cliente <> "" Then


            Else
                CB_Cliente.Enabled = False
                Txt_Dig_Cli_Apl.Text = Cliente.cli_dig_ito 
                Txt_Rut_Cli_Apl.Text = Cliente.cli_idc
                Txt_Rut_Cli_Apl.Text = format(CDbl(Txt_Rut_Cli_Apl.Text ),fmt.FCMSD)
                Txt_Rut_Cli_Apl.ReadOnly=True
                Txt_Dig_Cli_Apl.ReadOnly=True
                Txt_Dig_Cli_Apl.CssClass="clsDisabled"
                Txt_Rut_Cli_Apl.CssClass="clsDisabled"
                Ib_ayu_cli_apl .Enabled =False
                Ib_ayu_cli_apl.Enabled = False
                MaskedEditExtender1.Enabled = False

                If Cliente.id_P_0044 = 1 Then
                    Txt_Nom_Cli_Apl.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                Else
                    Txt_Nom_Cli_Apl.Text = Cliente.cli_rso.Trim
                End If


                'Txt_Tasa.Text = CG.TasaRetorna(2, Txt_Rut_Cli.Text, 0)

                'BloqueaCriterioAplicaciones(True)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LB_BuscarCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarCliente.Click

        Try

          

            Dim Cliente As cli_cls

            Cliente = clasecli.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)



            If valida_cliente <> "" Then

                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub

            Else
                If IsNothing(Cliente) Then
                    Msj.Mensaje(Me, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                CB_Cliente.Enabled = False

                If Cliente.id_P_0044 = 1 Then
                    Txt_Nom_Cli.Text = Trim(Cliente.cli_rso) & " " & Trim(Cliente.cli_ape_ptn) & " " & Trim(Cliente.cli_ape_mtn)
                Else
                    Txt_Nom_Cli.Text = Trim(Cliente.cli_rso)
                End If


                'Txt_Tasa.Text = CG.TasaRetorna(2, Txt_Rut_Cli.Text, 0)

                BloqueaCliente(True)


            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LimpiaCliente()
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Nom_Cli.Text = ""
    End Sub

    Private Sub BloqueaCliente(ByVal Estado As Boolean)

        If Estado Then

            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True

            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"

            Ib_ayu_cli.Enabled = False
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Else

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Nom_Cli.Text = ""
            Ib_ayu_cli.Enabled = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True



        End If

    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged

        Try

            LimpiaCliente()

            If CB_Cliente.Checked Then

                BloqueaCliente(False)
                Txt_Rut_Cli.Focus()

            Else

                BloqueaCliente(True)

            End If


        Catch ex As Exception

        End Try


    End Sub

#End Region

#Region "SUB"

    Private Sub BuscarPagos()

        Try

            Dim RutCli_Desde As Long
            Dim RutCli_Hasta As Long
            Dim Fecha_Desde As String
            Dim Fecha_Hasta As String
            Dim NroDpo_Desde As Integer
            Dim NroDpo_Hasta As Integer
            Dim Monto_Desde As Double
            Dim Monto_Hasta As Double
            Dim Banco_Desde As Integer
            Dim Banco_Hasta As Integer
            Dim Plaza_Desde As Integer
            Dim Plaza_Hasta As Integer
            Dim Estado_Desde As Integer
            Dim Estado_Hasta As Integer
            Dim Sucursal_Desde As Integer
            Dim Sucursal_Hasta As Integer

            If CB_Cliente.Checked Then
                RutCli_Desde = Txt_Rut_Cli.Text
                RutCli_Hasta = Txt_Rut_Cli.Text
            Else
                RutCli_Desde = 0
                RutCli_Hasta = 999999999999
            End If

            If Txt_FechaDesde.Text = "" And Txt_FechaHasta.Text = "" Then
                Fecha_Desde = "01/01/1900"
                Fecha_Hasta = "01/01/3000"
            Else
                Fecha_Desde = Txt_FechaDesde.Text & " 00:00:00"
                Fecha_Hasta = Txt_FechaHasta.Text & " 23:59:59"
            End If

            If Txt_NroDpo.Text = "" Then
                NroDpo_Desde = 0
                NroDpo_Hasta = 999999999
            Else
                NroDpo_Desde = Txt_NroDpo.Text
                NroDpo_Hasta = Txt_NroDpo.Text
            End If

            If Txt_Monto.Text = "" Then
                Monto_Desde = 0
                Monto_Hasta = 9999999999999
            Else
                Monto_Desde = Txt_Monto.Text
                Monto_Hasta = Txt_Monto.Text
            End If

            If DP_Bancos.SelectedIndex = 0 Then
                Banco_Desde = 0
                Banco_Hasta = 999
            Else
                Banco_Desde = DP_Bancos.SelectedValue
                Banco_Hasta = DP_Bancos.SelectedValue
            End If

            If DP_Plazas.SelectedIndex = 0 Then
                Plaza_Desde = 0
                Plaza_Hasta = 999
            Else
                Plaza_Desde = DP_Plazas.SelectedValue
                Plaza_Hasta = DP_Plazas.SelectedValue
            End If

            If DP_Estados.SelectedIndex = 0 Then
                Estado_Desde = 0
                Estado_Hasta = 999
            Else
                Estado_Desde = DP_Estados.SelectedValue
                Estado_Hasta = DP_Estados.SelectedValue
            End If

            If CB_Sucursal.Checked Then
                Sucursal_Desde = 0
                Sucursal_Hasta = 999
            Else
                Sucursal_Desde = Sucursal
                Sucursal_Hasta = Sucursal
            End If

            Dim Coll As Collection

            If Not IsDate(Fecha_Desde) Then
                Msj.Mensaje(Page, Caption, "Fecha desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Fecha_Hasta) Then
                Msj.Mensaje(Page, Caption, "Fecha hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CDate(Fecha_Desde) > CDate(Fecha_Hasta) Then
                Msj.Mensaje(Page, Caption, "Fecha desde no puede ser mayor a fecha hasta ", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            IB_Anular.Enabled = False
            IB_Protesto.Enabled = False
            IB_Imprimir.Enabled = False

            HF_Pos_Ing.Value = ""
            HF_Id_Dpo.Value = ""
            HF_RutCliente.Value = ""
            HF_ID_APL.Value = ""

            Coll = PGO.Pagos_Devuelve(RutCli_Desde, RutCli_Hasta, _
                                     Fecha_Desde, Fecha_Hasta, _
                                     NroDpo_Desde, NroDpo_Hasta, _
                                     Monto_Desde, Monto_Hasta, _
                                     Estado_Desde, Estado_Hasta, _
                                     Banco_Desde, Banco_Hasta, _
                                     Sucursal_Desde, Sucursal_Hasta, _
                                     Plaza_Desde, Plaza_Hasta, _
                                     13)

            GV_Pagos.DataSource = Coll
            GV_Pagos.DataBind()

            If Coll.Count > 0 Then

                'recorreros la grilla para darle formato a los numeros
                For I = 0 To GV_Pagos.Rows.Count - 1

                    Dim mto As Double = GV_Pagos.Rows(I).Cells(4).Text
                    Dim Formato_Moneda As String = ""

                    Select Case Coll.Item(I + 1).id_mon
                        Case 1 : Formato_Moneda = Fmt.FCMSD
                        Case 2 : Formato_Moneda = Fmt.FCMCD4
                        Case 3, 4 : Formato_Moneda = Fmt.FCMCD
                    End Select

                    GV_Pagos.Rows(I).Cells(4).Text = Format(mto, Formato_Moneda)

                    Select Case Coll.Item(I + 1).id_est

                        Case 6, 9 'Protestado
                            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                            GV_Pagos.Rows(I).BackColor = col
                    End Select

                    If Coll.Item(I + 1).dpo_anl = "S" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Pagos.Rows(I).BackColor = col
                    End If

                Next

                BloqueaBusqueda(True)
            Else
                Msj.Mensaje(Page, Caption, "No se encontraron pagos para el criterio utilazado.", TipoDeMensaje._Informacion)
            End If

            MarcaCxC()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BloqueaBusqueda(ByVal Estado As Boolean)

        'Cliente
        CB_Cliente.Enabled = Not Estado
        'Txt_Rut_Cli.ReadOnly = Estado
        'Txt_Dig_Cli.ReadOnly = Estado
        'Fechas
        Txt_FechaDesde.ReadOnly = Estado
        Txt_FechaDesde_CalendarExtender.Enabled = Not Estado
        Txt_FechaHasta.ReadOnly = Estado
        Txt_FechaHasta_CalendarExtender.Enabled = Not Estado

        Txt_NroDpo.ReadOnly = Estado
        Txt_Monto.ReadOnly = Estado

        DP_Bancos.Enabled = Not Estado
        DP_Plazas.Enabled = Not Estado
        DP_Estados.Enabled = Not Estado

        CB_Sucursal.Enabled = Not Estado

        If Estado Then
            'Txt_Rut_Cli.CssClass = "clsDisabled"
            'Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_FechaDesde.CssClass = "clsDisabled"


            Txt_FechaHasta.CssClass = "clsDisabled"

            Txt_NroDpo.CssClass = "clsDisabled"
            Txt_Monto.CssClass = "clsDisabled"

            'Txt_FechaHasta.Text = ""
            'Txt_Monto.Text = ""
            'Txt_FechaDesde.Text = ""
            'Me.DP_Bancos.ClearSelection()
            'Me.DP_Estados.ClearSelection()
            'Me.DP_Plazas.ClearSelection()

            DP_Bancos.CssClass = "clsDisabled"
            DP_Plazas.CssClass = "clsDisabled"
            DP_Estados.CssClass = "clsDisabled"
        Else
            'Txt_Rut_Cli.CssClass = "clsTxt"
            'Txt_Dig_Cli.CssClass = "clsTxt"
            Txt_FechaDesde.CssClass = "clsTxt"
            Txt_FechaHasta.CssClass = "clsTxt"
            Txt_NroDpo.CssClass = "clsTxt"
            Txt_Monto.CssClass = "clsTxt"
            DP_Bancos.CssClass = "clsTxt"
            DP_Plazas.CssClass = "clsTxt"
            DP_Estados.CssClass = "clsTxt"

            Txt_FechaHasta.Text = ""
            Txt_Monto.Text = ""
            Txt_NroDpo.Text = ""
            Txt_FechaDesde.Text = ""
            Me.DP_Bancos.ClearSelection()
            Me.DP_Estados.ClearSelection()
            Me.DP_Plazas.ClearSelection()

        End If

        IB_BuscarPagos.Enabled = Not Estado


    End Sub

    Private Sub LimpiaCriterio()

        'Txt_FechaPago.Text = ""
        CB_Cliente.Checked = False
        'CB_Deudor.Checked = False

        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Nom_Cli.Text = ""

        'txt_rut_deu.text = ""
        'txt_dig_deu.text = ""
        'txt_nom_deu.text = ""

    End Sub

    Private Sub LimpiaGridView()

        GV_Pagos.DataSource = Nothing
        GV_Pagos.DataBind()
        
    End Sub

    Private Sub MarcaCxC()
        For I = 0 To GV_Pagos.Rows.Count - 1
            If I = (HF_Pos_Ing.Value - 1) Then
                If (I Mod 2) = 0 Then
                    GV_Pagos.Rows(I).CssClass = "selectable"
                Else
                    GV_Pagos.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    GV_Pagos.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Pagos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

    Private Sub Marca_aplicaciones()

        For I = 0 To GV_Aplicaciones.Rows.Count - 1
            If I = (HF_Pos_Ing.Value - 1) Then
                If (I Mod 2) = 0 Then
                    GV_Aplicaciones.Rows(I).CssClass = "selectable"
                Else
                    GV_Aplicaciones.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    GV_Aplicaciones.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Aplicaciones.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

    Private Sub LlenaDrop()

        Try

            CG.BancosDevuelveTodos(DP_Bancos)
            CG.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Plazas, True, DP_Plazas)
            CG.ParametrosDevuelveTodos(TablaParametro.EstadoDctoPago, True, DP_Estados)

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            valida_cliente = ""
            LlenaDrop()

            sesionPgo.Coll_Apli = New Collection

            Txt_Rut_Cli_Apl.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

            NroPaginacion_Recaudacion = 0
            NroPaginacion = 0

            Txt_Fec_Apl_Dsd.Text = Format(Now, "dd/MM/yyyy")
            Txt_Fec_Apl_Hst.Text = Format(Now, "dd/MM/yyyy")

            IB_Anular.Enabled = False
            IB_Protesto.Enabled = False
            IB_Imprimir.Enabled = False

        End If

        If TabContainer1.TabIndex = 0 And CB_Cliente.Enabled = False Then

            'BuscarPagos()

        End If

        Ib_ayu_cli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?anulapago=1','PopUpCliente',620,410,200,150);")
        Ib_ayu_cli_apl.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?anulapago=2','PopUpCliente',620,410,200,150);")

    End Sub

    Protected Sub GV_Pagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Pagos.RowDataBound
    End Sub

    Protected Sub GV_Aplicaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Aplicaciones.RowDataBound
    End Sub

    Protected Sub LB_BuscarDetallePago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarDetallePago.Click

        Try

            Dim Coll_Dpo As Collection

            Coll_Dpo = CG.IngresoDevuelveModoDePago(HF_Id_Dpo.Value)

            GV_Pagos.DataSource = Coll_Dpo
            GV_Pagos.DataBind()

            For I = 0 To GV_Pagos.Rows.Count - 1

                GV_Pagos.Rows(I).Cells(3).Text = Format(CDbl(GV_Pagos.Rows(I).Cells(3).Text), Fmt.FCMSD)

                Select Case Coll_Dpo.Item(I + 1).ing_vld_rcz
                    Case "V" : GV_Pagos.Rows(I).BackColor = Drawing.Color.LightGreen
                    Case "R" : GV_Pagos.Rows(I).BackColor = Drawing.Color.Red
                End Select
            Next

            Dim Coll As Collection

            Coll = CG.TipoDeIngresoDevuelve(HF_Id_Dpo.Value)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LB_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Buscar.Click
        BuscarPagos()
    End Sub

    Protected Sub LB_CargaPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_CargaPagos.Click
        BuscarPagos()
    End Sub

    Protected Sub Lb_gri_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_gri.Click

        Try

            For i = 0 To Me.GV_Pagos.Rows.Count - 1

                Dim lb As Label
                Dim LB2 As Label

                lb = Me.GV_Pagos.Rows(i).FindControl("Lb_est")
                LB2 = Me.GV_Pagos.Rows(i).FindControl("lb_tip")

                If (Val(HF_Pos_Ing.Value - 1)) <> i Then

                    Select Case LB2.Text
                        Case 6, 9 'Protestado
                            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                            GV_Pagos.Rows(i).BackColor = col
                    End Select

                    If lb.Text = "S" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Pagos.Rows(i).BackColor = col
                    End If

                Else

                    Dim lb_ing As ImageButton
                    lb_ing = Me.GV_Pagos.Rows(i).FindControl("Img_Ver")
                    HF_id_ing.Value = lb_ing.ToolTip

                    Me.GV_Pagos.Rows(i).BackColor = Nothing

                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Lb_gri_Apl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_gri_Apl.Click

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Tesoreria"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        LB_BuscarCliente_Click(Me, e)
    End Sub

    Protected Sub Txt_Dig_Cli_Apl_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli_Apl.TextChanged
        LB_BuscarClienteApli_Click(Me, e)
    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        Try
            If TabContainer1.ActiveTab.HeaderText = "Recaudaciones" Then
                If GV_Pagos.Rows.Count > 0 Then
                    IB_BuscarPagos.Enabled = False
                Else
                    IB_BuscarPagos.Enabled = True
                End If
            Else
                If GV_Aplicaciones.Rows.Count > 0 Then
                    IB_BuscarPagos.Enabled = False
                Else
                    IB_BuscarPagos.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If TabContainer1.ActiveTabIndex = 0 Then 'Recaudaciones

                If NroPaginacion_Recaudacion = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub
                End If

                NroPaginacion_Recaudacion -= 13
                BuscarPagos()

            Else 'Aplicaciones
                If NroPaginacion = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    Exit Sub
                End If

                NroPaginacion -= 13
                BuscaAplicacion()

            End If


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If TabContainer1.ActiveTabIndex = 0 Then 'Recaudaciones

                NroPaginacion_Recaudacion += 13
                BuscarPagos()

            Else  'Aplicaciones
                NroPaginacion += 13
                BuscaAplicacion()

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'aplicaciones

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim lb_apl As Label
        Dim lb As Label
        Dim LB2 As Label

        HF_Pos_Ing.Value = ""
        HF_Id_Dpo.Value = ""
        HF_RutCliente.Value = ""
        HF_ID_APL.Value = ""

        IB_Anular.Attributes.Remove("OnClick")
        IB_Protesto.Attributes.Remove("OnClick")

        IB_Anular.Enabled = False
        IB_Protesto.Enabled = False
        IB_Imprimir.Enabled = False

        Try

            If btn.ToolTip <> "" Then

                For i = 0 To GV_Aplicaciones.Rows.Count - 1

                    lb_apl = Me.GV_Aplicaciones.Rows(i).FindControl("Lb_id_apl")

                    If btn.ToolTip = lb_apl.Text Then

                        HF_Pos_Ing.Value = i + 1
                        HF_Id_Dpo.Value = GV_Aplicaciones.Rows(i).Cells(4).Text
                        HF_RutCliente.Value = GV_Aplicaciones.Rows(i).Cells(1).Text
                        HF_ID_APL.Value = lb_apl.Text

                        IB_Anular.Enabled = True
                        IB_Protesto.Enabled = True
                        IB_Imprimir.Enabled = True

                        Exit For
                    Else
                        HF_Pos_Ing.Value = ""
                        HF_Id_Dpo.Value = ""
                        HF_RutCliente.Value = ""
                        HF_ID_APL.Value = ""
                    End If

                Next

                For i = 0 To Me.GV_Aplicaciones.Rows.Count - 1

                    lb = Me.GV_Aplicaciones.Rows(i).FindControl("Lb_est")
                    LB2 = Me.GV_Aplicaciones.Rows(i).FindControl("lb_tip")

                    If Not IsNothing(sesionPgo.Coll_Apli.Item(i + 1).apl_fec_anl) Then 'Pregunto si esta anulada         
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Aplicaciones.Rows(i).BackColor = col
                        'IB_Anular.Enabled = False
                    End If

                    If (Val(HF_Pos_Ing.Value) - 1) = i Then
                        HF_RUT_CLI.Value = Format(CLng(CStr(Me.GV_Aplicaciones.Rows(i).Cells(1).Text).Substring(0, CStr(Me.GV_Aplicaciones.Rows(i).Cells(1).Text).Trim.Length - 2)), Var.FMT_RUT)
                    End If

                Next

                Marca_aplicaciones()

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'pagos

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_Pos_Ing.Value = ""
        HF_Id_Dpo.Value = ""
        HF_RutCliente.Value = ""
        HF_ID_APL.Value = ""

        IB_Anular.Enabled = False
        IB_Protesto.Enabled = False
        IB_Imprimir.Enabled = False

        Try

            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")

            If btn.ToolTip <> "" Then

                For i = 0 To GV_Pagos.Rows.Count - 1

                    If btn.ToolTip = Trim(Replace(GV_Pagos.Rows(i).Cells(2).Text, ".", "")) Then

                        HF_Pos_Ing.Value = i + 1
                        HF_Id_Dpo.Value = CType(GV_Pagos.Rows(i).FindControl("Lb_id_ing"), Label).Text
                        HF_Estado.Value = GV_Pagos.Rows(i).Cells(10).Text

                        If GV_Pagos.Rows(i).BackColor <> col Then
                            IB_Anular.Enabled = True
                            IB_Protesto.Enabled = True
                            IB_Imprimir.Enabled = True
                        Else
                            IB_Anular.Enabled = False
                            IB_Protesto.Enabled = False
                            IB_Imprimir.Enabled = False
                        End If

                        Exit For

                    End If

                Next

                Lb_gri_Click(sender, e)
                MarcaCxC()

                Try

                    Select Case TabContainer1.ActiveTabIndex
                        Case 0 'Protesto
                            If HF_Id_Dpo.Value <> "" Then
                                If ValidaCondicionesDelPago() Then
                                    Pagos.RutCliente = RC.LimpiaRut(HF_RutCliente.Value)
                                    'RW.AbrePopup(Me, 2, "DetallePagos.aspx?id=" & HF_Id_Dpo.Value & "&AP=A", "Pagos", 1000, 600, 10, 10)
                                    IB_Anular.Attributes.Add("OnClick", "WinOpen(2, 'DetallePagos.aspx?id=" & HF_Id_Dpo.Value & "&AP=A', 'Pagos', 1000, 600, 100, 100);")
                                    IB_Protesto.Attributes.Add("OnClick", "WinOpen(2, 'DetallePagos.aspx?id=" & HF_Id_Dpo.Value & "&AP=P', 'Pagos', 1000, 600, 100, 100);")
                                End If
                            Else
                                Msj.Mensaje(Me, "Atención", "Debe seleccionar un registro", ClsMensaje.TipoDeMensaje._Exclamacion)
                            End If

                        Case 1 'Anulación
                            If Val(HF_ID_APL.Value) = 0 Then
                                Msj.Mensaje(Page, Caption, "Debe seleccionar una aplicación", TipoDeMensaje._Informacion)
                                Exit Sub
                            End If

                            Msj.Mensaje(Page, Caption, "¿Desea anular?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Anular.UniqueID, True)

                    End Select

                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "APLICACIONES"

    Protected Sub RB_Clientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Clientes.SelectedIndexChanged

        Try
            Select Case RB_Clientes.SelectedValue
                Case 0
                    BloqueaCriterioAplicaciones(False)
                Case 1
                    BloqueaCriterioAplicaciones(True)
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BloqueaCriterioAplicaciones(ByVal Estado As Boolean)

        Try

            Txt_Rut_Cli_Apl.ReadOnly = Not Estado
            Txt_Dig_Cli_Apl.ReadOnly = Not Estado

            If Estado Then
                Ib_ayu_cli_apl.Enabled = True
                Txt_Rut_Cli_Apl.Text = ""
                Txt_Dig_Cli_Apl.Text = ""

                Txt_Rut_Cli_Apl.CssClass = "clsMandatorio"
                Txt_Dig_Cli_Apl.CssClass = "clsMandatorio"
                Txt_Rut_Cli_Apl.Focus()
                MaskedEditExtender1.Enabled = True

            Else
                Ib_ayu_cli_apl.Enabled = False
                Txt_Rut_Cli_Apl.CssClass = "clsDisabled"
                Txt_Dig_Cli_Apl.CssClass = "clsDisabled"
                Txt_Rut_Cli_Apl.Text = ""
                Txt_Dig_Cli_Apl.Text = ""
                Txt_Nom_Cli_Apl.Text = ""
                MaskedEditExtender1.Enabled = False

            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub BuscaAplicacion()

        Try

            If ValidaCriterioAplicacion() Then

                Dim RutCli_Desde As Long
                Dim RutCli_Hasta As Long
                Dim Fecha_Desde As String
                Dim Fecha_Hasta As String

                If RB_Clientes.SelectedValue = 1 Then
                    RutCli_Desde = Txt_Rut_Cli_Apl.Text
                    RutCli_Hasta = Txt_Rut_Cli_Apl.Text

                Else
                    RutCli_Desde = 0
                    RutCli_Hasta = 999999999999
                End If



                Fecha_Desde = Txt_Fec_Apl_Dsd.Text
                Fecha_Hasta = Txt_Fec_Apl_Hst.Text

                'Dim Coll As Collection

                sesionPgo.Coll_Apli = CG.Aplicacion_Devuelve(RutCli_Desde, RutCli_Hasta, _
                                                             Fecha_Desde, Fecha_Hasta, _
                                                             2, "S", _
                                                             CodEje, CodEje, _
                                                             13)

                GV_Aplicaciones.DataSource = sesionPgo.Coll_Apli
                GV_Aplicaciones.DataBind()

                Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")

                If sesionPgo.Coll_Apli.Count > 0 Then

                    'Si encuentra pagos habilitamos los botones para realizar una accion sobre el pago
                    IB_Protesto.Enabled = False
                    IB_Anular.Enabled = True
                    IB_BuscarPagos.Enabled = False

                    For I = 0 To GV_Aplicaciones.Rows.Count - 1

                        Dim rut As Long = GV_Aplicaciones.Rows(I).Cells(1).Text
                        GV_Aplicaciones.Rows(I).Cells(1).Text = Format(Rut, Fmt.FCMSD) & "-" & RC.Vrut(Rut)

                        'Formato para Montos en $
                        GV_Aplicaciones.Rows(I).Cells(5).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(5).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(6).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(6).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(7).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(7).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(8).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(8).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(9).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(9).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(10).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(10).Text), Fmt.FCMSD)
                        GV_Aplicaciones.Rows(I).Cells(11).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(11).Text), Fmt.FCMCD)
                        GV_Aplicaciones.Rows(I).Cells(12).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(12).Text), Fmt.FCMCD)
                        GV_Aplicaciones.Rows(I).Cells(13).Text = Format(Val(GV_Aplicaciones.Rows(I).Cells(13).Text), Fmt.FCMSD)

                        If Not IsNothing(sesionPgo.Coll_Apli.Item(I + 1).apl_fec_anl) Then 'Pregunto si esta anulada         

                            GV_Aplicaciones.Rows(I).BackColor = col
                        End If

                    Next

                Else
                    Msj.Mensaje(Page, Caption, "No se encontraron aplicaciones", TipoDeMensaje._Informacion)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Error, Nothing)
        End Try

    End Sub

    Private Function ValidaCriterioAplicacion() As Boolean

        If RB_Clientes.SelectedValue = 1 Then

            If Txt_Rut_Cli_Apl.Text = "" Then
                Msj.Mensaje(Page, Caption, "Debe ingresar NIT del Cliente a buscar", TipoDeMensaje._Informacion)
                Return False
            End If

            If Txt_Dig_Cli_Apl.Text = "" Then
                Msj.Mensaje(Page, Caption, "Debe ingresar digito del NIT del Cliente a buscar", TipoDeMensaje._Informacion)
                Return False
            End If

            If Txt_Dig_Cli_Apl.Text <> RC.Vrut(Txt_Rut_Cli_Apl.Text) Then
                Msj.Mensaje(Page, Caption, "Digito del Rut Incorrecto", TipoDeMensaje._Informacion)
                Return False
            End If

        End If

        If Txt_Fec_Apl_Dsd.Text = "" Then
            Msj.Mensaje(Page, Caption, "Debe ingresar fecha desde para buscar", TipoDeMensaje._Informacion)
            Return False
        End If

        If Not IsDate(Txt_Fec_Apl_Dsd.Text) Then
            Msj.Mensaje(Page, Caption, "Fecha desde errónea", TipoDeMensaje._Informacion)
            Return False
        End If

        If Txt_Fec_Apl_Hst.Text = "" Then
            Msj.Mensaje(Page, Caption, "Debe ingresar fecha hasta para busca", TipoDeMensaje._Informacion)
            Return False
        End If

        If Not IsDate(Txt_Fec_Apl_Hst.Text) Then
            Msj.Mensaje(Page, Caption, "Fecha hasta errónea", TipoDeMensaje._Informacion)
            Return False
        End If

        If CDate(Txt_Fec_Apl_Dsd.Text) > CDate(Txt_Fec_Apl_Hst.Text) Then
            Msj.Mensaje(Page, Caption, "Fecha desde debe ser menor que la fecha hasta", TipoDeMensaje._Informacion)
            Return False
        End If

        Return True

    End Function

#End Region

End Class
