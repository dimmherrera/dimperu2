Imports CapaDatos
Partial Class NuevoPagare
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim caption As String = "Pagaré"
    Dim CG As New ConsultasGenerales
    Dim FG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim sesionDocto As New ClsSession.SesionPagos
    Dim Msj As New ClsMensaje
    Dim FechaProtesto As String
    Dim Motpro As String
    Dim CL As New ConsultasLegales
    Dim AL As New ActualizacionesLegales
    Dim sesion As New ClsSession.ClsSession

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txt_Rut_Cli.Focus()
                Response.Expires = -1
                Cargadrop()
                txt_NPagare.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Monto.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Monto.CssClass = "clsDisabled"
                If Request.QueryString("Docto") <> "" Then
                    HF_Id_Docto.Value = Request.QueryString("Docto")
                    CargaDatos()
                    'txt_NPagare.Enabled = False
                    txt_NPagare.CssClass = "clsDisabled"
                    Txt_Rut_Cli.ReadOnly = True
                    Txt_Dig_Cli.ReadOnly = True
                    Txt_Rut_Cli.CssClass = "clsDisabled"
                    Txt_Dig_Cli.CssClass = "clsDisabled"
                    '***************probando******************
                    txt_Monto.CssClass = "clsMandatorio"
                    txt_FSuscripcion.CssClass = "clsDisabled"
                    txt_FSuscripcion.ReadOnly = True
                    txt_FSuscripcion_CalendarExtender.Enabled = False
                    txt_FVecto.CssClass = "clsDisabled"
                    txt_FVecto.ReadOnly = True
                    txt_FVecto_CalendarExtender.Enabled = False
                    '*****************************************
                    IB_Limpiar.Enabled = False
                    'txt_NPagare.ReadOnly = True
                    'txt_NPagare.CssClass = "clsDisabled"
                    IB_AyudaCli.Enabled = False
                    txt_Rut_Cli_MaskedEditExtender.Enabled = False
                Else
                    txt_Monto_MaskedEditExtender.Enabled = True
                    txt_Monto.ReadOnly = True
                End If
            End If
            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',570 ,400,100,100);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lb_cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cli.Click
        Try
            TraeCliente()
        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub CB_SinNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_SinNum.CheckedChanged
        Try
            If CB_SinNum.Checked = True Then
                txt_NPagare.ReadOnly = True
                txt_NPagare.Text = 0
            Else
                txt_NPagare.ReadOnly = False
                txt_NPagare.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_estado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_estado.SelectedIndexChanged
        Try
            If Drop_estado.SelectedValue = 3 Then
                txt_FProtesto.ReadOnly = False
                txt_FProtesto.CssClass = "clsMandatorio"
                Drop_MotivoProtesto.Enabled = True
                Drop_MotivoProtesto.CssClass = "clsMandatorio"
                txt_FProtesto_CalendarExtender.Enabled = True
                txt_FProtesto_MaskedEditExtender.Enabled = True
            Else
                txt_FProtesto.ReadOnly = True
                txt_FProtesto.CssClass = "clsDisabled"
                Drop_MotivoProtesto.Enabled = False
                Drop_MotivoProtesto.CssClass = "clsDisabled"
                Drop_MotivoProtesto.ClearSelection()
                txt_FProtesto.Text = ""
                'Drop_estado.ClearSelection()
                txt_FProtesto_CalendarExtender.Enabled = False
                txt_FProtesto_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub LinkB_Moneda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Moneda.Click
    '    Try
    '        If Drop_Moneda.SelectedValue = 0 Then
    '            Msj.Mensaje(Me.Page, caption, "Seleccione Moneda", TipoDeMensaje._Exclamacion, "", )
    '            Exit Sub
    '        End If

    '        Select Case Drop_Moneda.SelectedValue
    '            Case 1 'Pesos
    '                txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMSD)
    '            Case 2 'UF
    '                txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD4)
    '            Case 3 ' dolar y euro
    '                txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD)

    '        End Select


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Drop_Moneda_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Moneda.TextChanged
        Try
            txt_Monto.Text = ""
            Select Case Drop_Moneda.SelectedValue
                Case 1 'Pesos
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999"
                    'txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMSD)
                Case 2 'UF
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999.9999"
                    'txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD4)
                Case 3, 4 ' dolar y euro
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999.99"
                    'txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD)
            End Select

            If Drop_Moneda.SelectedValue <> 0 Then
                txt_Monto.CssClass = "clsMandatorio"
                txt_Monto.Focus()
                txt_Monto.ReadOnly = False
            Else
                txt_Monto.CssClass = "clsDisabled"
                txt_Monto.ReadOnly = True

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Dig_Cli.TextChanged
        Try
            If txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion, "", )
                Exit Sub
            End If
            TraeCliente()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub txt_FProtesto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FProtesto.TextChanged
    '    Try
    'If Trim(txt_FProtesto.Text) <> "" Then
    '    If Not IsDate(txt_FProtesto.Text) Then
    '        Msj.Mensaje(Page, caption, "Fecha erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
    '        txt_FProtesto.Text = ""
    '        'txt_FProtesto.Focus()
    '    End If
    'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub txt_FSuscripcion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FSuscripcion.TextChanged
    '    Try
    'If Trim(txt_FSuscripcion.Text) <> "" Then
    '    If Not IsDate(txt_FSuscripcion.Text) Then
    '        Msj.Mensaje(Page, caption, "Fecha suscripción erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
    '        txt_FSuscripcion.Text = ""
    '        ' txt_FSuscripcion.Focus()
    '    End If
    'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub txt_FVecto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FVecto.TextChanged
    '    Try
    'If Trim(txt_FVecto.Text) <> "" And txt_FVecto.Text <> "__/__/____" Then
    '    If Not IsDate(txt_FVecto.Text) Then
    '        Msj.Mensaje(Page, caption, "Fecha vencimiento erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
    '        txt_FVecto.Text = ""
    '        'txt_FVecto.Focus()
    '    End If
    'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Link_Guarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guarda.Click
        Try

            If txt_FProtesto.Text = "" Then
                FechaProtesto = CDate("01/01/1900")
            Else
                FechaProtesto = txt_FProtesto.Text
            End If

            If Drop_MotivoProtesto.SelectedValue = 0 Then
                Motpro = Nothing
            Else
                Motpro = Drop_MotivoProtesto.SelectedValue
            End If

            Dim Doctodsd As Integer
            Dim DoctoHst As Integer
            If txt_NPagare.Text = "" Then
                Doctodsd = 0
                DoctoHst = 99999
            Else
                Doctodsd = txt_NPagare.Text
                DoctoHst = txt_NPagare.Text
            End If

            If HF_Id_Docto.Value = "" Then
                Dim coll As New Collection
                coll = CL.PagareDevuelve(Txt_Rut_Cli.Text, Txt_Rut_Cli.Text, "01/01/1900", "31/12/2999", _
                                        "01/01/1900", "31/12/2999", 0, 99999, 0, 9999999999999, "A", "Z", _
                                        0, 999999, 0, 999999, 0, 999999, txt_NPagare.Text, txt_NPagare.Text, 0, 999, 13)

                If Not IsNothing(coll) Then
                    If coll.Count > 0 Then
                        Msj.Mensaje(Me.Page, caption, "Nº pagaré ya existe para cliente", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If
                Dim p As New pgr_cls
                p.id_pgr = Nothing
                p.cli_idc = Format(CLng(txt_Rut_Cli.Text), "000000000000")
                p.id_cxc = Nothing
                p.id_ope = Nothing
                p.id_P_0021 = Drop_TipoPagare.SelectedValue
                p.id_P_0022 = Drop_estado.SelectedValue
                p.id_P_0023 = Drop_Moneda.SelectedValue
                If Motpro = Nothing Then
                    p.id_P_0061 = Nothing
                Else
                    p.id_P_0061 = Motpro
                End If
                p.pgr_anc = UCase(txt_Antecedentes.Text)
                p.pgr_fec_ing = txt_FSuscripcion.Text
                p.pgr_fec_otv = CDate("01/01/1900")
                p.pgr_fec_prt = FechaProtesto
                p.pgr_fev = txt_FVecto.Text
                p.pgr_ftm = CDate("01/01/1900")
                p.pgr_imp = Nothing
                p.pgr_mdt = RB_Mandato.SelectedValue
                If txt_Monto.Text = "" Then
                    txt_Monto.Text = 0
                End If
                p.pgr_mto = Format(CDbl(txt_Monto.Text), FMT.FSMSD)
                p.pgr_num = txt_NPagare.Text
                p.pgr_pag_tye = Nothing
                p.pgr_tim = Nothing
                p.PgrAsoc = 1 'Asociado al cliente
                If AL.PagareInserta(p) = True Then
                    Response.Redirect("Pagare.aspx", False)
                End If
                LimpiarControles()
                'HF_Est.Value = 1
            Else
                
                Dim p As New pgr_cls
                p.id_pgr = HF_Id_Docto.Value
                p.id_P_0021 = Drop_TipoPagare.SelectedValue
                p.id_P_0022 = Drop_estado.SelectedValue
                p.id_P_0023 = Drop_Moneda.SelectedValue
                If Motpro = Nothing Then
                    p.id_P_0061 = Nothing
                Else
                    p.id_P_0061 = Motpro
                End If
                p.pgr_anc = UCase(txt_Antecedentes.Text)
                p.pgr_fec_ing = txt_FSuscripcion.Text
                p.pgr_fec_prt = FechaProtesto
                p.pgr_fev = txt_FVecto.Text
                p.pgr_mdt = RB_Mandato.SelectedValue
                If txt_Monto.Text = "" Then
                    txt_Monto.Text = 0
                End If
                p.pgr_mto = txt_Monto.Text
                p.pgr_num = txt_NPagare.Text
                p.PgrAsoc = 1 'Asociado al cliente
                If AL.PagareModifica(p) = True Then
                    Response.Redirect("Pagare.aspx", False)
                End If
                'HF_Est.Value = 1
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub LinkB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Eliminar.Click
        Try
            If HF_Id_Docto.Value > 0 Then
                Dim p As New pgr_cls
                p = CL.PagareDevuelveObjeto(HF_Id_Docto.Value)
                If p.id_cxc > 0 Then
                    Msj.Mensaje(Me.Page, caption, "No se puede eliminar, el pagaré tiene una cuenta por cobrar asociada", TipoDeMensaje._Exclamacion)
                Else
                    AL.PagareElimina(HF_Id_Docto.Value)
                    'CargaGr()
                    'Msj.Mensaje(Me.Page, caption, "Pagare eliminado", TipoDeMensaje._Exclamacion)
                    Response.Redirect("Pagare.aspx", False)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub RB_Mandato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Mandato.SelectedIndexChanged
        Try
            If RB_Mandato.SelectedValue = "N" Then
                txt_Monto.CssClass = "clsMandatorio"
                txt_Monto.Focus()
            Else
                txt_Monto.CssClass = "clstxt"
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "Function y Sub"
    Sub Cargadrop()
        CG.ParametrosDevuelve(21, True, Drop_TipoPagare)
        CG.ParametrosDevuelve(22, True, Drop_estado)
        CG.ParametrosDevuelve(23, True, Drop_Moneda)
        CG.ParametrosDevuelve(61, True, Drop_MotivoProtesto)
    End Sub
    Sub LimpiarControles()
        txt_Antecedentes.Text = ""
        Txt_Dig_Cli.Text = ""
        txt_FProtesto.Text = ""
        txt_FSuscripcion.Text = ""
        txt_FVecto.Text = ""
        txt_Monto.Text = ""
        txt_NPagare.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Rut_Cli.Text = ""
        txt_FProtesto.CssClass = "clsDisabled"
        Drop_MotivoProtesto.CssClass = "clsDisabled"
        Drop_MotivoProtesto.ClearSelection()
        HF_Id_Docto.Value = ""
        Txt_Rut_Cli.ReadOnly = False
        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.ReadOnly = False
        Txt_Dig_Cli.CssClass = "clsMandatorio"
        Drop_estado.ClearSelection()
        Drop_TipoPagare.ClearSelection()
        Drop_Moneda.ClearSelection()
        Drop_MotivoProtesto.Enabled = False
        txt_FProtesto.ReadOnly = True
        txt_FProtesto_CalendarExtender.Enabled = False
        txt_FProtesto_MaskedEditExtender.Enabled = False
        txt_Rut_Cli_MaskedEditExtender.Enabled = True
        IB_AyudaCli.Enabled = True
    End Sub
    Sub CargaDatos()
        Try
            Dim p As New pgr_cls
            p = CL.PagareDevuelveObjeto(HF_Id_Docto.Value)
            txt_Monto.Text = p.pgr_mto
            Select Case p.id_P_0023
                Case 1 'pesos
                    txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMSD)
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2 'uf
                    txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD4)
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999.9999"
                Case 3, 4 ' dolar y euro
                    txt_Monto.Text = Format(CLng(txt_Monto.Text), FMT.FCMCD)
                    txt_Monto_MaskedEditExtender.Mask = "999,999,999,999.99"

            End Select

            If p.id_P_0022 = 3 Then
                txt_FProtesto.ReadOnly = False
                txt_FProtesto.CssClass = "clsMandatorio"
                Drop_MotivoProtesto.Enabled = True
                Drop_MotivoProtesto.CssClass = "clsMandatorio"
            End If
            If IsNothing(p.pgr_num) Then
                CB_SinNum.Checked = True
            Else
                CB_SinNum.Checked = False
            End If
            txt_NPagare.Text = p.pgr_num
            RB_Mandato.SelectedValue = p.pgr_mdt
            Txt_Rut_Cli.Text = Format(CDbl(p.cli_cls.cli_idc), FMT.FCMSD)
            Txt_Dig_Cli.Text = p.cli_cls.cli_dig_ito
            Txt_Raz_Soc.Text = p.cli_cls.cli_rso & " " & p.cli_cls.cli_ape_ptn & " " & p.cli_cls.cli_ape_mtn
            Drop_estado.SelectedValue = p.id_P_0022
            Drop_TipoPagare.SelectedValue = p.id_P_0021
            Drop_Moneda.SelectedValue = p.id_P_0023
            If p.id_P_0061 > 0 Then
                Drop_MotivoProtesto.SelectedValue = p.id_P_0061
                txt_FProtesto.Text = p.pgr_fec_prt
            End If
            txt_FSuscripcion.Text = p.pgr_fec_ing
            txt_FVecto.Text = p.pgr_fev
            txt_Antecedentes.Text = p.pgr_anc
            IB_Elimina.Enabled = True
            If Not IsNothing(p.id_cxc) Then
                CB_SinNum.Enabled = False
                RB_Mandato.Enabled = False
                Drop_Moneda.Enabled = False
                Drop_Moneda.CssClass = "clsDisabled"
                Drop_TipoPagare.Enabled = False
                Drop_TipoPagare.CssClass = "clsDisabled"
                txt_FSuscripcion.ReadOnly = True
                txt_FSuscripcion.CssClass = "clsDisabled"
                txt_FSuscripcion_MaskedEditExtender.Enabled = False
                txt_FSuscripcion_CalendarExtender.Enabled = False
                txt_FVecto.ReadOnly = True
                txt_FVecto.CssClass = "clsDisabled"
                txt_FVecto_CalendarExtender.Enabled = False
                txt_FVecto_MaskedEditExtender.Enabled = False
                Drop_estado.Enabled = False
                Drop_estado.CssClass = "clsDisabled"
                txt_Monto.ReadOnly = True
                txt_Monto_MaskedEditExtender.Enabled = False
                txt_Monto.CssClass = "clsDisabled"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TraeCliente()

        Dim cli As cli_cls
        Dim CLSCLI As New ClaseClientes

        cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)

        If sesion.valida_cliente <> "" Then
            Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion, "", )
        Else

            If IsNothing(cli) Then
                Msj.Mensaje(Me.Page, caption, "Cliente no existe", TipoDeMensaje._Exclamacion, , )
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If


            Session("Cliente") = cli
            Txt_Rut_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.ReadOnly = True
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Raz_Soc.ReadOnly = True
            Txt_Raz_Soc.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False
            txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            txt_NPagare.Focus()
        End If

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            LimpiarControles()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            'If HF_Est.Value = "" Then
            '    RW.ClosePag(Me)
            'Else
            '    RW.CloseModal(Me, "ctl00$ContentPlaceHolder1$Link_Actualiza")
            '    'Exit Sub
            'End If
            Response.Redirect("Pagare.aspx", False)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try

            Dim Cliente As cli_cls
            Dim CLSCLI As New ClaseClientes

            Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion, "", )
                Exit Sub
            Else
                Session("Cliente") = Cliente
            End If

            If txt_NPagare.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese número de pagaré", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Drop_TipoPagare.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de pagaré", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If RB_Mandato.SelectedValue = "N" Then
                If txt_Monto.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese monto", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If txt_FSuscripcion.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese fecha de suscripción", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_FVecto.Text = "" Then
                Msj.Mensaje(Page, caption, "Ingrese fecha de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If HF_Id_Docto.Value = "" Then

                If CDate(txt_FSuscripcion.Text) < Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year Then
                    Msj.Mensaje(Page, caption, "Fecha de suscripción no puede ser menor a fecha actual", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If CDate(txt_FSuscripcion.Text) >= CDate(txt_FVecto.Text) Then
                Msj.Mensaje(Page, caption, "Fecha de suscripción no puede ser mayor o igual a fecha de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Drop_estado.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione estado de pagaré", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Drop_Moneda.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de Moneda", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_Monto.Text = "" Then
                Msj.Mensaje(Page, caption, "Ingrese monto del pagaré", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Drop_estado.SelectedValue = 3 Then 'protestado
                If Trim(txt_FProtesto.Text) = "" Then
                    Msj.Mensaje(Page, caption, "Ingrese fecha de protesto", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    If Not IsDate(txt_FProtesto.Text) Then
                        Msj.Mensaje(Page, caption, "Fecha de protesto errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If
                If CDate(txt_FProtesto.Text) < CDate(txt_FVecto.Text) Then
                    Msj.Mensaje(Me.Page, caption, "Fecha de protesto debe ser mayor o igual a fecha de vencimiento", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


                If Drop_MotivoProtesto.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione motivo de protesto", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


            End If

            If HF_Id_Docto.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guarda.UniqueID, True)
            Else

                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guarda.UniqueID, True)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Elimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Elimina.Click
        Try
            Msj.Mensaje(Me.Page, caption, "¿Esta seguro de eliminar este pagaré?", ClsMensaje.TipoDeMensaje._Confirmacion, LinkB_Eliminar.UniqueID, True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_AyudaCli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AyudaCli.Click

    End Sub
#End Region

End Class


