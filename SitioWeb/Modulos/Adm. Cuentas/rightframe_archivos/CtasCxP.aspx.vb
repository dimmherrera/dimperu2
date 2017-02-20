Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports ClsSession.SesionOperaciones
Partial Class ClsCxP
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim agt As New Perfiles.Cls_Principal
    Dim caption As String = "Cuentas por Pagar"
    Dim AG As New ActualizacionesGenerales
    Dim cta As New ClaseCuentas
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CG As New ConsultasGenerales
    Dim Msj As New ClsMensaje
    Dim sesion As New ClsSession.ClsSession
    Dim FG As New FuncionesGenerales.FComunes

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
                sesion.NroPaginacion = 0
                CargaDrop()
                IB_Imprimir.Enabled = False
                Txt_Monto.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_nro_doc.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Contrato.Attributes.Add("Style", "TEXT-ALIGN: right")
                CG.ParametrosDevuelve(41, True, dropTipoCuenta)
                IB_AyudaDoc.Enabled = False

            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            txt_Contrato.Attributes.Add("readonly", "true")
            txt_nro_doc.Attributes.Add("readonly", "true")


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

            Dim cxp As New cxp_cls
            cxp = cta.VerificaNroCuentaCXP(HF_Id.Value)
            If cxp.id_P_0057 = 0 Or cxp.id_P_0057 = 1 Or cxp.id_P_0057 = 2 Or cxp.id_P_0057 = 3 Then 'Vigente
                IB_Anular.Enabled = True
            Else
                IB_Anular.Enabled = False
            End If
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

    Protected Sub GV_Cuentas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Cuentas.PageIndexChanging
        GV_Cuentas.PageIndex = e.NewPageIndex
        AvanzaGrilla()
    End Sub

    Private Sub AvanzaGrilla()
        'GV_Cuentas.DataSource = Coll_CXP
        GV_Cuentas.DataBind()
    End Sub

    Protected Sub drop_TpMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drop_TpMoneda.SelectedIndexChanged
        Try
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

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click

        Try

            Dim cxp As New cxp_cls

            cxp.id_eje = CodEje
            cxp.cli_idc = Format(CLng(Txt_Rut_Cli.Text), "000000000000")
            cxp.id_P_0041 = dropTipoCuenta.SelectedValue
            cxp.cxp_fec = Txt_Fec_Cxc.Text
            cxp.cxp_des = UCase(Txt_Descripcion.Text)
            cxp.id_P_0023 = drop_TpMoneda.SelectedValue
            cxp.cxp_mto = Txt_Monto.Text
            cxp.id_P_0057 = 1
            Me.HF_Id.Value = cta.NroDoctoDevuelve(txt_nro_doc.Text)
            cxp.id_doc = HF_Id.Value

            cxp.cxp_fac_cam = CG.ParidadDevuelve(drop_TpMoneda.SelectedValue, Txt_Fec_Cxc.Text).par_val

            If cta.CXPInserta(cxp) <> 0 Then
                Msj.Mensaje(Me.Page, caption, "Cuenta ingresada", TipoDeMensaje._Informacion)
                'Se modifica, al guardar una cxp muestre todas las cxp del cliente
                'Busqueda()

                Dim Coll_OtraCXP As New Collection
                Coll_OtraCXP = cta.CuentasPorPagarDevuelve(Txt_Rut_Cli.Text, 0)
                If Not IsNothing(Coll_OtraCXP) Then
                    If Coll_OtraCXP.Count > 0 Then
                        Me.Txt_Rut_Cli.ReadOnly = True
                        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                        Me.Txt_Dig_Cli.ReadOnly = True
                        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                        Me.Txt_Raz_Soc.ReadOnly = True
                        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                        dropTipoCuenta.Enabled = False
                        dropTipoCuenta.CssClass = "clsDisabled"
                        dropTipoCuenta.ClearSelection()
                        GV_Cuentas.DataSource = Coll_OtraCXP
                        GV_Cuentas.DataBind()
                        IB_Nuevo.Enabled = True
                        IB_Imprimir.Enabled = True
                        IB_Buscar.Enabled = False
                        For i = 1 To Coll_OtraCXP.Count
                            If Coll_OtraCXP.Item(i).id_P_0023 = 1 Then 'pesos
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_OtraCXP.Item(i).cxp_mto), FMT.FCMSD)
                            ElseIf Coll_OtraCXP.Item(i).id_P_0023 = 2 Then ' UF
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(Coll_OtraCXP.Item(i).cxp_mto, FMT.FCMCD4)
                            ElseIf Coll_OtraCXP.Item(i).id_P_0023 = 3 Or Coll_OtraCXP.Item(i).id_P_0023 = 4 Then ' dolar y euro
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(Coll_OtraCXP.Item(i).cxp_mto, FMT.FCMCD)
                            End If

                        Next
                    Else
                        Msj.Mensaje(Me.Page, caption, "El cliente no tiene cuentas por pagar", TipoDeMensaje._Exclamacion)
                        IB_Nuevo.Enabled = True
                        dropTipoCuenta.Enabled = False
                        dropTipoCuenta.CssClass = "clsDisabled"
                        dropTipoCuenta.ClearSelection()
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
            Dim cxp As New cxp_cls

            cxp = cta.VerificaNroCuentaCXP(HF_Id.Value)
            If IsNothing(cxp.id_cxp) Then
                Msj.Mensaje(Me.Page, caption, "No se puede anular porque existen conceptos asociados", TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                cta.CXPModificaEstado(HF_Id.Value, 4)
                Busqueda()
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

            If sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else

                If IsNothing(cli) Then
                    Msj.Mensaje(Page, caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                IB_Nuevo.Enabled = True


                dropTipoCuenta.Enabled = True
                dropTipoCuenta.ClearSelection()
                dropTipoCuenta.CssClass = "clsTxt"



            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
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
        IB_AyudaDoc.Enabled = True
        IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" + Txt_Rut_Cli.Text + "','PopUpCuentas Por Pagar',1220,610,200,150);")
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

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click 'Handles IB_Limpiar.Click
        Try

            IB_AyudaDoc.Enabled = False

            Txt_Monto.CssClass = "clsDisabled"
            Txt_Descripcion.CssClass = "clsDisabled"

            Txt_Monto.ReadOnly = True
            Txt_Descripcion.ReadOnly = True

            'TabStrip1.Enabled = True
            'MultiPage1.Enabled = True

            Txt_Monto.Text = ""
            Txt_Descripcion.Text = ""
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            TipoCuenta.Text = ""
            NroCuenta.Text = ""
            'drop_TpMoneda.SelectedValue = ""
            drop_TpMoneda.ClearSelection()
            drop_TpMoneda.Enabled = False
            drop_TpMoneda.CssClass = "clsDisabled"
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            IB_AyudaCli.Visible = True
            'Txt_Rso_Deu.CssClass = "clsMandatorio"
            IB_AyudaCli.Enabled = True

            dropTipoCuenta.Enabled = False


            dropTipoCuenta.CssClass = "clsTxt"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"


            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            IB_Guardar.Enabled = False
            IB_Nuevo.Enabled = False
            IB_Buscar.Enabled = True
            IB_Imprimir.Enabled = False
            dropTipoCuenta.ClearSelection()

            txt_Contrato.Text = ""
            txt_nro_doc.Text = ""
            GV_Cuentas.DataSource = ""
            GV_Cuentas.DataBind()

            Habilita()
            NroPaginacion = 0

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click 'Handles IB_Guardar.Click
        Try

            If Not agt.ValidaAccesso(20, 20040401, Usr, "PRESIONO GUARDAR CXP") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT cliente", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If drop_TpMoneda.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo moneda", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Monto.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese monto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Not IsNumeric(Txt_Monto.Text) Then
                Msj.Mensaje(Me.Page, caption, "Ingrese solo números", TipoDeMensaje._Error)
                Txt_Monto.Text = ""
                Txt_Monto.Focus()
                Exit Sub
            End If


            If Txt_Descripcion.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese descripción", TipoDeMensaje._Exclamacion)
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
            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar este registro?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Anular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Anular.Click
        Try

            If Not agt.ValidaAccesso(20, 20020401, Usr, "PRESIONO ANULAR CXP") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de anular este registro?", TipoDeMensaje._Confirmacion, Link_Anular.UniqueID, True)


        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Not agt.ValidaAccesso(20, 20010401, Usr, "PRESIONO BUSCAR CXP") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            sesion.NroPaginacion = 0
            Busqueda()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click


        Try

            If Not agt.ValidaAccesso(20, 20030401, Usr, "PRESIONO NUEVO CXP") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            IB_Guardar.Enabled = True
            IB_Buscar.Enabled = False
            IB_Imprimir.Enabled = False
            IB_Anular.Enabled = False
            dropTipoCuenta.Enabled = True
            dropTipoCuenta.CssClass = "clsMandatorio"


            Txt_Monto.CssClass = "clsMandatorio"
            Txt_Descripcion.CssClass = "clsMandatorio"
            Me.Txt_Fec_Cxc.Text = FUNFecReg(Date.Now)

            Txt_Monto.ReadOnly = False
            Txt_Descripcion.ReadOnly = False


            drop_TpMoneda.Enabled = True
            drop_TpMoneda.CssClass = "clsMandatorio"
            dropTipoCuenta.Enabled = True
            drop_TpMoneda.Focus()

            For i = 0 To GV_Cuentas.Rows.Count - 1
                GV_Cuentas.Rows(i).CssClass = "formatable"
                'GV_Cuentas.Rows(i).Enabled = False
            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles IB_Cli.Click
        Try
            Dim FG As New FuncionesGenerales.FComunes


            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Dig_Cli.Text <> FG.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                Msj.Mensaje(Me.Page, caption, "Digito incorrecto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
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

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)

                IB_Nuevo.Enabled = True
                dropTipoCuenta.Enabled = False


                CG.ParametrosDevuelve(41, True, dropTipoCuenta)

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_AyudaCli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AyudaCli.Click
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try


            If Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedValue <= 0 Then
                'por cliente

                RW.AbrePopup(Me, 1, "InformeCXP.aspx?rut_dsd=" & Txt_Rut_Cli.Text & "&rut_hst=" & Txt_Rut_Cli.Text _
                               & "&Fecha_dsd=" & "01/01/1900" & "&Fecha_hst=" & "30/12/2999" & "&Moneda_dsd=" & 0 _
                               & "&Moneda_hst=" & 999 & "&Est_dsd=" & 0 & "&Est_hst=" & 999 & "&numope=" & 0 _
                               & "&numdoc=" & 0 & "&tpct=" & 5 & "&OtraTPct=" & 0 & "&Informe=" & 1, "informeCXP", 1100, 900, 10, 10)

            ElseIf Txt_Rut_Cli.Text <> "" And dropTipoCuenta.SelectedValue >= 0 Then
                'por Otras Cuentas
                RW.AbrePopup(Me, 1, "InformeCXP.aspx?rut_dsd=" & Txt_Rut_Cli.Text & "&rut_hst=" & Txt_Rut_Cli.Text _
                                   & "&Fecha_dsd=" & "01/01/1900" & "&Fecha_hst=" & "30/12/2999" & "&Moneda_dsd=" & 0 _
                                   & "&Moneda_hst=" & 999 & "&Est_dsd=" & 0 & "&Est_hst=" & 999 & "&numope=" & 0 _
                                   & "&numdoc=" & 0 & "&tpct=" & 4 & "&OtraTPct=" & dropTipoCuenta.SelectedValue _
                                   & "&Informe=" & 1, "informeCXP", 1100, 900, 10, 10)

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
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

#End Region


#Region "Sub y Function"

    Sub CargaDrop()
        Try

            CG.ParametrosDevuelve(23, True, drop_TpMoneda)
            CG.ParametrosDevuelve(41, True, dropTipoCuenta)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Sub CargaRutCli() 'Carga razon social
        Try

            Dim FG As New FuncionesGenerales.FComunes

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Dig_Cli.Text <> FG.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                Msj.Mensaje(Me.Page, caption, "dígito incorrecto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Dim cli As cli_cls

            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
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

                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                IB_Buscar.Enabled = True
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub Deshabilita()
        Me.Txt_Monto.Text = ""
        Me.Txt_Monto.CssClass = "clsDisabled"
        Me.Txt_Monto.ReadOnly = True

        Me.Txt_Descripcion.Text = ""
        Me.Txt_Descripcion.CssClass = "clsDisabled"
        Me.Txt_Descripcion.ReadOnly = True

        dropTipoCuenta.Enabled = False
        dropTipoCuenta.CssClass = "clsDisabled"
        dropTipoCuenta.ClearSelection()

        drop_TpMoneda.Enabled = False
        drop_TpMoneda.CssClass = "clsDisabled"
        drop_TpMoneda.ClearSelection()

        Me.txt_nro_doc.Text = ""
        Me.txt_Contrato.Text = ""

        IB_Guardar.Enabled = False

        dropTipoCuenta.Enabled = False
        dropTipoCuenta.CssClass = "clsDisabled"

    End Sub

    Private Sub Habilita()

        dropTipoCuenta.Enabled = True

        dropTipoCuenta.CssClass = "clsTxt"
        Txt_Rut_Cli_MaskedEditExtender.Enabled = True

    End Sub
    Private Sub TraeCtasXcliente()

        Try
            Dim FG As New FuncionesGenerales.FComunes

            Dim Coll_CXP As New Collection
            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Debe ingresar un cliente", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion)
            End If
            If UCase(Txt_Dig_Cli.Text) <> FG.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                Msj.Mensaje(Me.Page, caption, "Dígito incorrecto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim cli As cli_cls

            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                IB_Nuevo.Enabled = True
                Coll_CXP = cta.CuentasPorPagarDevuelve(Txt_Rut_Cli.Text, dropTipoCuenta.SelectedValue)
                If Not IsNothing(Coll_CXP) Then
                    If Coll_CXP.Count > 0 Then
                        Me.Txt_Rut_Cli.ReadOnly = True
                        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                        Me.Txt_Dig_Cli.ReadOnly = True
                        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                        Me.Txt_Raz_Soc.ReadOnly = True
                        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                        GV_Cuentas.DataSource = Coll_CXP
                        GV_Cuentas.DataBind()
                        IB_Imprimir.Enabled = True
                        IB_Nuevo.Enabled = True
                        IB_Buscar.Enabled = False
                        For i = 1 To Coll_CXP.Count
                            If Coll_CXP.Item(i).id_P_0023 = 1 Then 'pesos
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXP.Item(i).cxp_mto), FMT.FCMSD)
                            ElseIf Coll_CXP.Item(i).id_P_0023 = 2 Then ' UF
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXP.Item(i).cxp_mto), FMT.FCMCD4)
                            ElseIf Coll_CXP.Item(i).id_P_0023 = 3 Or Coll_CXP.Item(i).id_P_0023 = 4 Then ' dolar y euro
                                GV_Cuentas.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_CXP.Item(i).cxp_mto), FMT.FCMCD)
                            End If
                        Next
                    Else
                        Msj.Mensaje(Me.Page, caption, "El cliente no tiene cuentas por pagar", TipoDeMensaje._Informacion)
                        IB_Nuevo.Enabled = True
                        dropTipoCuenta.ClearSelection()
                        ' Exit Sub
                    End If
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
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If
            TraeCtasXcliente() 'Cliente
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

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
                Dim cxp As New cxp_cls
                cxp = cta.VerificaNroCuentaCXP(HF_Id.Value)

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


                If cxp.id_P_0057 = 0 Or cxp.id_P_0057 = 1 Or cxp.id_P_0057 = 2 Or cxp.id_P_0057 = 3 Then 'Vigente
                    IB_Anular.Enabled = True
                Else
                    IB_Anular.Enabled = False
                End If

            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub


End Class
