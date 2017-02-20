Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class ClsCtasXCobrar
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim CG As New ConsultasGenerales
    Dim cta As New ClaseCuentas
    Dim AG As New ActualizacionesGenerales
    Dim Coll_CXC As New Collection
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FG As New FuncionesGenerales.FComunes
    Dim caption As String = "Cuentas por Cobrar"
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim Sesion As New ClsSession.ClsSession
#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Administracion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Response.Expires = -1

            If Not Me.IsPostBack Then
                Sesion.NroPaginacion = 0
                Txt_Monto.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Contrato.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Contrato.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_nro_doc.Attributes.Add("Style", "TEXT-ALIGN: right")
                CargaDrop()
                Txt_Rut_Cli.Focus()
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                IB_AyudaDoc.Visible = True
                IB_AyudaDoc.Enabled = False
                'txt_Contrato.Attributes.Add("readonly", "true")
                'txt_nro_doc.Attributes.Add("readonly", "true")

            End If


            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            txt_Contrato.Attributes.Add("readonly", "true")
            txt_nro_doc.Attributes.Add("readonly", "true")


        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Cuentas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Cuentas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'e.Row.Attributes.Add("onClick", "celClickCXC(ctl00_ContentPlaceHolder1_GV_Cuentas, 'clicktable', 'formatable', 'selectable')")

        End If
    End Sub

    Protected Sub lb_cli_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cli.Click

        Try

            Dim FG As New FuncionesGenerales.FComunes
            Dim CLSCLI As ClaseClientes
            Dim cli As cli_cls

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else

                Session("Cliente") = cli
                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"

                'Asigna Razon Social / Nombre a Campo Cliente
                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                IB_Nuevo.Enabled = True
                IB_AyudaCli.Enabled = False
                dropTipoCuenta.Enabled = True

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LinkbN_Cuenta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkbN_Cuenta.Click
        Try

            For i = 0 To GV_Cuentas.Rows.Count - 1
                GV_Cuentas.Rows(i).CssClass = "formatable"

                If HF_Id.Value >= 0 And (HF_Pos.Value - 1) = i Then

                    GV_Cuentas.Rows(HF_Pos.Value - 1).CssClass = "clicktable"
                Else
                    GV_Cuentas.Rows(i).CssClass = "formatable"
                End If
            Next

            Dim cxc As New cxc_cls
            cxc = cta.VerificaNroCuenta(HF_Id.Value)
            If cxc.id_P_0057 = 0 Or cxc.id_P_0057 = 1 Or cxc.id_P_0057 = 2 Or cxc.id_P_0057 = 3 Then 'Vigente
                IB_Anular.Enabled = True
            Else
                IB_Anular.Enabled = False
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub drop_TpMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drop_TpMoneda.SelectedIndexChanged

        Try

            'Se modifica para que al seleccionar tipo moneda cambie la mascara de monto
            If drop_TpMoneda.SelectedValue <> 0 Then
                Txt_Monto_MaskedEditExtender.Enabled = True
                Txt_Monto.Text = ""
                Txt_Monto.Focus()
            Else
                Txt_Monto_MaskedEditExtender.Enabled = False
            End If

            Select Case drop_TpMoneda.SelectedValue
                Case 1 'Pesos
                    Txt_Monto_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2 'UF
                    Txt_Monto_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3 ' dolar y euro
                    Txt_Monto_MaskedEditExtender.Mask = "999,999,999.99"
            End Select

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Link_Guarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guarda.Click
        Try

            Dim cxc As New cxc_cls
            cxc.cli_idc = Format(CLng(Txt_Rut_Cli.Text), "000000000000")

            If dropTipoCuenta.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de cuenta", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            'If dropTipoCuenta.SelectedValue = 3 Then
            cxc.id_eje = CodEje
            cxc.id_P_0041 = dropTipoCuenta.SelectedValue
            cxc.cxc_fec = Txt_Fec_Cxc.Text
            cxc.cxc_mto = Txt_Monto.Text
            cxc.cxc_sal = Txt_Monto.Text
            cxc.cxc_des = UCase(Txt_Descripcion.Text)
            cxc.id_P_0023 = drop_TpMoneda.SelectedValue
            cxc.id_P_0057 = 1
            Me.HF_Id.Value = cta.NroDoctoDevuelve(txt_nro_doc.Text)
            cxc.id_doc = Me.HF_Id.Value

            'Else
            'cxc.id_P_0041 = dropTipoCuenta.SelectedValue
            'cxc.cxc_fec = Txt_Fec_Cxc.Text
            'cxc.cxc_mto = Txt_Monto.Text
            'cxc.cxc_sal = Txt_Monto.Text
            'cxc.cxc_des = UCase(Txt_Descripcion.Text)
            'cxc.id_P_0023 = drop_TpMoneda.SelectedValue
            'cxc.id_P_0057 = 1
            'End If

            cxc.cxc_fac_cam = CG.ParidadDevuelve(drop_TpMoneda.SelectedValue, Txt_Fec_Cxc.Text).par_val

            If cta.CxcInserta(cxc) = True Then
                Msj.Mensaje(Me.Page, caption, "Cuenta ingresada", TipoDeMensaje._Informacion)
                'Se deja comentareada por que al guardar no mostraba las demas cuentas
                Me.dropTipoCuenta.SelectedValue = 0
                Dim ev As System.Web.UI.ImageClickEventArgs
                IB_Buscar_Click(Me, ev)
                Deshabilita()
                Exit Sub
                'Busqueda()

                'TraeOtrasCtasXCobrar()
                ' HabilitaTab()
                Dim Coll_OtraCXC As New Collection

                Coll_OtraCXC = cta.CuentasPorCobrarDevuelve(Txt_Rut_Cli.Text, 0, 0, 0)

                Dim cli As cli_cls
                Dim CLSCLI As New ClaseClientes

                cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

                If Sesion.valida_cliente <> "" Then
                    IB_AyudaCli.Enabled = True
                    Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                Else

                    'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

                    Me.Txt_Rut_Cli.ReadOnly = True
                    Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                    Me.Txt_Dig_Cli.ReadOnly = True
                    Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                    Me.Txt_Raz_Soc.ReadOnly = True
                    Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                    'Asigna Razon Social / Nombre a Campo Cliente
                    Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                    Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                    'CG.CargaGrillaCXC_Cliente(Txt_Rut_Cli.Text, True, GV_Cuentas)
                    IB_Nuevo.Enabled = True
                    dropTipoCuenta.Enabled = True
                    dropTipoCuenta.CssClass = "clsTxt"
                    IB_AyudaCli.Enabled = False
                End If
                If Not IsNothing(Coll_OtraCXC) Then
                    If Coll_OtraCXC.Count > 0 Then
                        Me.Txt_Rut_Cli.ReadOnly = True
                        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                        Me.Txt_Dig_Cli.ReadOnly = True
                        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                        Me.Txt_Raz_Soc.ReadOnly = True
                        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                        dropTipoCuenta.Enabled = False
                        dropTipoCuenta.CssClass = "clsDisabled"
                        GV_Cuentas.DataSource = Coll_OtraCXC
                        GV_Cuentas.DataBind()
                        IB_Nuevo.Enabled = True
                        IB_Imprimir.Enabled = True
                        IB_Buscar.Enabled = False
                        IB_Nuevo.Enabled = True
                        For i = 1 To Coll_OtraCXC.Count
                            If Coll_OtraCXC.Item(i).id_P_0023 = 1 Then 'pesos
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMSD)
                                GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMSD)
                            ElseIf Coll_OtraCXC.Item(i).id_P_0023 = 2 Then ' UF
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format((Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMCD4)
                                GV_Cuentas.Rows(i - 1).Cells(9).Text = Format((Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMCD4)
                            ElseIf Coll_OtraCXC.Item(i).id_P_0023 = 3 Or Coll_OtraCXC.Item(i).id_P_0023 = 4 Then ' dolar y euro
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format((Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMCD)
                                GV_Cuentas.Rows(i - 1).Cells(9).Text = Format((Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMCD)
                            End If
                        Next
                    End If


                End If
            End If

            Deshabilita()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Link_Anular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Anular.Click
        Try
            Dim cxc As New cxc_cls

            cxc = cta.VerificaNroCuenta(HF_Id.Value)
            If IsNothing(cxc.id_cxc) Then
                Msj.Mensaje(Me.Page, caption, "No se puede anular porque existen conceptos Asociados", TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                If cta.CXCModificaEstado(HF_Id.Value, 4) = True Then
                    Msj.Mensaje(Me.Page, caption, "Cuenta por cobrar anulada", TipoDeMensaje._Informacion, "", True)
                    Busqueda()
                End If

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            Dim FG As New FuncionesGenerales.FComunes
            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else

                If IsNothing(cli) Then
                    Msj.Mensaje(Me.Page, caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)

                IB_Nuevo.Enabled = True
                dropTipoCuenta.Enabled = True
                IB_AyudaCli.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Cuentas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Cuentas.PageIndexChanging
        GV_Cuentas.PageIndex = e.NewPageIndex
        AvanzaGrilla()
    End Sub

    Protected Sub lb_id_doc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_id_doc.Click

        Dim posicion As Integer = HF_Pos.Value
        Dim formatmoneda As String = FG.DevuelveFormatoMoneda(Coll_DOC.Item(posicion).id_P_0023)

        Me.HF_Id.Value = Coll_DOC.Item(posicion).ID_DOC

        Me.txt_nro_doc.Text = Coll_DOC.Item(posicion).DSI_NUM
        Me.txt_Contrato.Text = Format(Coll_DOC.Item(posicion).DSI_MTO, formatmoneda)
        
    End Sub

    Protected Sub dropTipoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTipoCuenta.SelectedIndexChanged
        'If dropTipoCuenta.SelectedValue = 3 Then
        IB_AyudaDoc.Visible = True
        IB_AyudaDoc.Enabled = True
        IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" + Txt_Rut_Cli.Text + "','PopUpCuentas Por Cobrar',1220,610,200,150);")
        Label27.Visible = True
        Label28.Visible = True
        txt_Contrato.Visible = True
        txt_nro_doc.Visible = True

        'Else
        'Label27.Visible = False
        'Label28.Visible = False
        'txt_Contrato.Visible = False
        'txt_nro_doc.Visible = False
        'IB_AyudaDoc.Visible = False
        'End If
    End Sub


#End Region

#Region "Sub y Function"

    Private Sub CargaDrop()
        Try

            CG.ParametrosDevuelve(23, True, drop_TpMoneda)
            CG.ParametrosDevuelve(41, True, dropTipoCuenta)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CargaRutCli()
        Try

            Dim FG As New FuncionesGenerales.FComunes
            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else
                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                'CargaGrCuenta()
                'IB_Nuevo.Enabled = True
                IB_Buscar.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub TraeCtasXcliente()
        Try
            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                'CG.CargaGrillaCXC_Cliente(Txt_Rut_Cli.Text, True, GV_Cuentas)
                IB_Nuevo.Enabled = True
                dropTipoCuenta.Enabled = True
                IB_AyudaCli.Enabled = False
            End If

            Coll_CXC = cta.CuentasPorCobrarDevuelve(Txt_Rut_Cli.Text, dropTipoCuenta.SelectedValue, 0, 999)

            If Not IsNothing(Coll_CXC) Then
                If Coll_CXC.Count > 0 Then
                    Me.Txt_Rut_Cli.ReadOnly = True
                    Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                    Me.Txt_Dig_Cli.ReadOnly = True
                    Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                    Me.Txt_Raz_Soc.ReadOnly = True
                    Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                    GV_Cuentas.DataSource = Coll_CXC
                    GV_Cuentas.DataBind()
                    IB_Imprimir.Enabled = True
                    IB_Buscar.Enabled = False
                    IB_Nuevo.Enabled = True

                    For i = 1 To Coll_CXC.Count
                        If Coll_CXC.Item(i).id_P_0023 = 1 Then 'pesos
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXC.Item(i).cxc_mto), FMT.FCMSD)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_CXC.Item(i).cxc_sal), FMT.FCMSD)
                        ElseIf Coll_CXC.Item(i).id_P_0023 = 2 Then ' UF
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXC.Item(i).cxc_mto), FMT.FCMCD4)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_CXC.Item(i).cxc_sal), FMT.FCMCD4)
                        ElseIf Coll_CXC.Item(i).id_P_0023 = 3 Or Coll_CXC.Item(i).id_P_0023 = 4 Then ' dolar y euro
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXC.Item(i).cxc_mto), FMT.FCMCD)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_CXC.Item(i).cxc_sal), FMT.FCMCD)
                        End If
                    Next

                Else
                    Msj.Mensaje(Me.Page, caption, "El cliente no tiene cuentas por cobrar", TipoDeMensaje._Informacion)
                    'Txt_Nro_Doc.Text = ""
                    'Txt_Nro_Oto.Text = ""
                    dropTipoCuenta.ClearSelection()
                    ' Exit Sub   
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub TraeOtrasCtasXCobrar()
        Try

            Dim FG As New FuncionesGenerales.FComunes

            If dropTipoCuenta.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de cuenta", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Coll_OtraCXC As New Collection

            Coll_OtraCXC = cta.CuentasPorCobrarDevuelve(Txt_Rut_Cli.Text, dropTipoCuenta.SelectedValue, 0, 0)

            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If Sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla

                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                'CG.CargaGrillaCXC_Cliente(Txt_Rut_Cli.Text, True, GV_Cuentas)
                IB_Nuevo.Enabled = True
                dropTipoCuenta.Enabled = True
                IB_AyudaCli.Enabled = False
            End If
            If Not IsNothing(Coll_OtraCXC) Then
                If Coll_OtraCXC.Count > 0 Then
                    Me.Txt_Rut_Cli.ReadOnly = True
                    Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                    Me.Txt_Dig_Cli.ReadOnly = True
                    Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                    Me.Txt_Raz_Soc.ReadOnly = True
                    Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                    dropTipoCuenta.Enabled = False
                    dropTipoCuenta.CssClass = "clsDisabled"
                    GV_Cuentas.DataSource = Coll_OtraCXC
                    GV_Cuentas.DataBind()
                    IB_Nuevo.Enabled = True
                    IB_Imprimir.Enabled = True
                    IB_Buscar.Enabled = False
                    IB_Nuevo.Enabled = True
                    For i = 1 To Coll_OtraCXC.Count
                        If Coll_OtraCXC.Item(i).id_P_0023 = 1 Then 'pesos
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMSD)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMSD)
                        ElseIf Coll_OtraCXC.Item(i).id_P_0023 = 2 Then ' UF
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMCD4)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMCD4)
                        ElseIf Coll_OtraCXC.Item(i).id_P_0023 = 3 Or Coll_OtraCXC.Item(i).id_P_0023 = 4 Then ' dolar y euro
                            GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_mto), FMT.FCMCD)
                            GV_Cuentas.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_OtraCXC.Item(i).cxc_sal), FMT.FCMCD)

                        End If
                    Next
                Else
                    Msj.Mensaje(Me.Page, caption, "El cliente no tiene cuentas por cobrar", TipoDeMensaje._Exclamacion)
                    IB_Nuevo.Enabled = True
                    'Txt_Nro_Doc.Text = ""
                    'Txt_Nro_Oto.Text = ""
                    dropTipoCuenta.ClearSelection()
                End If


            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub Busqueda()
        Try
            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            'If Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedIndex <= 0 Then
            TraeCtasXcliente()

            'dropTipoCuenta.Enabled = False
            'dropTipoCuenta.CssClass = "clsDisabled"
            'ElseIf Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedIndex > 0 Then
            'TraeOtrasCtasXCobrar()
            'End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub Deshabilita()
        Me.Txt_Monto.Text = ""
        Me.Txt_Monto.CssClass = "clsDisabled"
        Me.Txt_Monto.ReadOnly = True

        Me.Txt_Descripcion.Text = ""
        Me.Txt_Descripcion.CssClass = "clsDisabled"
        Me.Txt_Descripcion.ReadOnly = True
        dropTipoCuenta.ClearSelection()

        Me.txt_nro_doc.Text = ""
        Me.txt_Contrato.Text = ""

        IB_Guardar.Enabled = False
        dropTipoCuenta.Enabled = False
        dropTipoCuenta.CssClass = "clsDisabled"
        drop_TpMoneda.Enabled = False
        drop_TpMoneda.CssClass = "clsDisabled"
        drop_TpMoneda.ClearSelection()
        For i = 0 To GV_Cuentas.Rows.Count - 1
            GV_Cuentas.Rows(i).Enabled = True
        Next

    End Sub

    Private Sub AvanzaGrilla()
        ' GV_Cuentas.DataSource = Coll_CXC
        ' GV_Cuentas.DataBind()
    End Sub

    Private Sub HabilitaTab()
        'Me.TabPanel1.Enabled = True
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click 'Handles IB_Buscar.Click 'Handles IB_Buscar.Click 

        Try

            If Not agt.ValidaAccesso(20, 20010301, Usr, "PRESIONO BUSCAR CXC") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Sesion.NroPaginacion = 0
            Busqueda()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Nuevo.Click

        Try

            If Not agt.ValidaAccesso(20, 20030301, Usr, "PRESIONO NUEVO CXC") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            IB_Guardar.Enabled = True
            IB_Buscar.Enabled = False
            IB_Imprimir.Enabled = False
            IB_Anular.Enabled = False
            IB_Nuevo.Enabled = False

            dropTipoCuenta.Enabled = True
            dropTipoCuenta.CssClass = "clsMandatorio"
            Txt_Monto.CssClass = "clsMandatorio"
            Txt_Descripcion.CssClass = "clsMandatorio"
            Txt_Fec_Cxc.Text = FUNFecReg(Date.Now)

            drop_TpMoneda.Enabled = True
            drop_TpMoneda.CssClass = "clsMandatorio"

            Txt_Monto.ReadOnly = False
            Txt_Descripcion.ReadOnly = False
            dropTipoCuenta.SelectedIndex = 0
            drop_TpMoneda.SelectedIndex = 0
            Txt_Monto.Text = ""
            Txt_Descripcion.Text = ""
            txt_Contrato.Text = ""
            txt_nro_doc.Text = ""

            drop_TpMoneda.Focus()


            For i = 0 To GV_Cuentas.Rows.Count - 1
                ' GV_Cuentas.Rows(i).Enabled = False
                GV_Cuentas.Rows(i).CssClass = "formatable"
            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click 'Handles IB_Guardar.Click
        Try

            If Not agt.ValidaAccesso(20, 20040301, Usr, "PRESIONO GUARDAR CXC") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT cliente", TipoDeMensaje._Error)
                Exit Sub
            End If

            If drop_TpMoneda.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo moneda", TipoDeMensaje._Error)
                Exit Sub
            End If

            If Txt_Monto.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese monto", TipoDeMensaje._Error)
                Exit Sub
            End If

            If Not IsNumeric(Txt_Monto.Text) Then
                Msj.Mensaje(Me.Page, caption, "Ingrese solo números", TipoDeMensaje._Error)
                Txt_Monto.Text = ""
                Txt_Monto.Focus()
                Exit Sub
            End If

            If Txt_Descripcion.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese descripción", TipoDeMensaje._Error)
                Exit Sub
            End If

            If dropTipoCuenta.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de cuenta", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            'If dropTipoCuenta.SelectedValue = 3 Then
            If Me.txt_nro_doc.Text = "" Then
                Msj.Mensaje(Me, caption, "Debe seleccionar un contrato desde la ayuda ,para poder generar esta cuenta", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If
            'End If


            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar este registro?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guarda.UniqueID, True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Anular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Anular.Click 'Handles IB_Anular.Click 'Handles IB_Anular.Click

        Try

            If Not agt.ValidaAccesso(20, 20020301, Usr, "PRESIONO ANULAR CXC") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de anular este registro?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Anular.UniqueID, True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            If GV_Cuentas.Rows.Count > 0 Then
                If Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedIndex <= 0 Then
                    'cliente
                    RW.AbrePopup(Me, 1, "InformeCXC.aspx?rut_dsd=" & Txt_Rut_Cli.Text & "&rut_hst=" & Txt_Rut_Cli.Text _
                                 & "&Fecha_dsd=" & "01/01/1900" _
                                 & "&Fecha_hst=" & "30/12/2999" & "&Moneda_dsd=" & 0 & "&Moneda_hst=" & 999 & "&Est_dsd=" & 0 _
                                 & "&Est_hst=" & 999 & "&numope=" & 0 _
                                               & "&numdoc=" & 0 & "&tpct=" & 5 & "&OtraTPct=" & 0 & "&Informe=" & 1, "CXC", 1100, 900, 10, 10)

                ElseIf Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedIndex > 0 Then
                    '  OtrasCuentas
                    RW.AbrePopup(Me, 1, "InformeCXC.aspx?rut_dsd=" & Txt_Rut_Cli.Text & "&rut_hst=" & Txt_Rut_Cli.Text & "&Fecha_dsd=" _
                                 & "01/01/1900" & "&Fecha_hst=" & "30/12/2999" & "&Moneda_dsd=" & 0 & "&Moneda_hst=" & 999 _
                                 & "&Est_dsd=" & 0 & "&Est_hst=" & 999 & "&numope=" & 0 _
                                 & "&numdoc=" & 0 & "&tpct=" & 4 & "&OtraTPct=" & dropTipoCuenta.SelectedValue _
                                 & "&Informe=" & 1, "CXC", 1100, 900, 10, 10)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try

            Txt_Monto.CssClass = "clsDisabled"
            Txt_Descripcion.CssClass = "clsDisabled"

            Txt_Monto.ReadOnly = True
            Txt_Descripcion.ReadOnly = True
            Txt_Monto.Text = ""
            Txt_Descripcion.Text = ""
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            TipoCuenta.Value = ""
            NroCuenta.Value = ""
            txt_Contrato.Text = ""
            txt_nro_doc.Text = ""
            hf_id_doc.Value = ""
            'Label27.Visible = False
            'Label28.Visible = False
            'txt_Contrato.Visible = False
            'txt_nro_doc.Visible = False
            'IB_AyudaDoc.Visible = False
            IB_AyudaDoc.Enabled = False
            Me.Txt_Rut_Cli.ReadOnly = False
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.ReadOnly = False
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"

            IB_AyudaCli.Enabled = True
            dropTipoCuenta.Enabled = False
            dropTipoCuenta.CssClass = "clsTxt"
            dropTipoCuenta.ClearSelection()
            IB_Nuevo.Enabled = False
            IB_Guardar.Enabled = False
            drop_TpMoneda.Enabled = False
            drop_TpMoneda.CssClass = "clsDisabled"
            drop_TpMoneda.ClearSelection()
            'BorraCollection(Coll_CXC)
            Txt_Fec_Cxc.Text = ""
            'Txt_Fec_Cxc.Enabled = False
            GV_Cuentas.DataSource = Nothing
            GV_Cuentas.DataBind()
            dropTipoCuenta.Enabled = True
            'dropTipoCuenta.CssClass = "clsMandatorio"
            IB_Imprimir.Enabled = False
            'dropTipoCuenta.Enabled = False
            IB_Buscar.Enabled = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True

            NroPaginacion = 0
            For i = 0 To GV_Cuentas.Rows.Count - 1
                GV_Cuentas.Rows(i).Enabled = True
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_AyudaCli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AyudaCli.Click
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 8 Then
            NroPaginacion -= 8
            Busqueda()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Cuentas.Rows.Count < 8 Then
            Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Cuentas.Rows.Count = 8 Then
            NroPaginacion += 8
            Busqueda()
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim rg As New FuncionesGenerales.FComunes
        Dim boton_ver As ImageButton = CType(sender, ImageButton)
        Try

            HF_Id.Value = boton_ver.ToolTip
            For I = 0 To GV_Cuentas.Rows.Count - 1
                If (HF_Id.Value = GV_Cuentas.Rows(I).Cells(2).Text) Then
                    If (I Mod 2) = 0 Then
                        GV_Cuentas.Rows(I).CssClass = "selectable"
                    Else
                        GV_Cuentas.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_Cuentas.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Cuentas.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

                If (HF_Id.Value = GV_Cuentas.Rows(I).Cells(2).Text) Then
                    drop_TpMoneda.ClearSelection()
                    drop_TpMoneda.Items.FindByText(GV_Cuentas.Rows(I).Cells(6).Text).Selected = True

                    Txt_Monto.Text = (GV_Cuentas.Rows(I).Cells(7).Text)
                    Txt_Descripcion.Text = Server.HtmlDecode(GV_Cuentas.Rows(I).Cells(8).Text)
                    Txt_Fec_Cxc.Text = (GV_Cuentas.Rows(I).Cells(3).Text)
                    txt_Contrato.Text = (GV_Cuentas.Rows(I).Cells(4).Text)
                    txt_nro_doc.Text = (GV_Cuentas.Rows(I).Cells(5).Text)

                    'End If
                End If

            Next

            Dim cxc As New cxc_cls

            cxc = cta.VerificaNroCuenta(HF_Id.Value)

            If cxc.id_P_0057 = 0 Or cxc.id_P_0057 = 1 Or cxc.id_P_0057 = 2 Or cxc.id_P_0057 = 3 Then 'Vigente
                IB_Anular.Enabled = True
            Else
                IB_Anular.Enabled = False
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub

#End Region

End Class
