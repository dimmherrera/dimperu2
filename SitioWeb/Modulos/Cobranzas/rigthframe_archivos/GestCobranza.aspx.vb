Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class GestCobranza
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim caption As String = "Gestion de Cobranza"
    Dim CG As New ConsultasGenerales
    Dim FG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim sesion As New ClsSession.ClsSession

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                Response.Expires = -1

                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Est_Dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Est_Hst.Attributes.Add("Style", "TEXT-ALIGN: right")

                CargaDrop()
                Check_Fono.Checked = True
                If Check_Fono.Checked = True Then
                    Drop_Cobradora.Enabled = False
                    Drop_Cobradora.CssClass = "clsDisabled"
                    Drop_Cobradora.ClearSelection()
                End If
                Check_Tp_Doc.Checked = True
                If Check_Tp_Doc.Checked = True Then
                    Drop_TP_Doc.Enabled = False
                    Drop_TP_Doc.CssClass = "clsDisabled"
                    Drop_TP_Doc.ClearSelection()
                End If
                Check_Ult_Gest.Checked = True
                If Check_Ult_Gest.Checked = True Then
                    txt_Gest_Dsd.Enabled = False
                    txt_Gest_Hst.Enabled = False
                    txt_Gest_Dsd.CssClass = "clsDisabled"
                    txt_Gest_Hst.CssClass = "clsDisabled"
                    txt_Gest_Dsd.Text = ""
                    txt_Gest_Hst.Text = ""
                End If

                Check_EstCob.Checked = True
                If Check_EstCob.Checked = True Then
                    txt_Est_Dsd.Text = ""
                    txt_Est_Hst.Text = ""
                    txt_Est_Dsd.ReadOnly = True
                    txt_Est_Hst.ReadOnly = True
                    txt_Est_Dsd.CssClass = "clsDisabled"
                    txt_Est_Hst.CssClass = "clsDisabled"
                End If

            End If
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpDeudor',650,410,200,150);")

            
        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Check_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Cli.CheckedChanged
        Try
            If Check_Cli.Checked = True Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Rut_Cli.Focus()
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Drop_Segmento.Enabled = False
                Drop_Segmento.CssClass = "clsDisabled"
                Drop_Segmento.ClearSelection()
                txt_cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True
            Else
                Drop_Segmento.Enabled = True
                Drop_Segmento.CssClass = "clsMandatorio"
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                IB_AyudaCli.Enabled = False
                txt_cli_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_Deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Deu.CheckedChanged
        Try
            If Check_Deu.Checked = True Then
                Txt_Rut_Deu.Focus()
                Txt_Rut_Deu.ReadOnly = False
                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.ReadOnly = False
                Txt_Dig_Deu.CssClass = "clsMandatorio"
                Drop_Seg.Enabled = False
                Drop_Seg.CssClass = "clsDisabled"
                Drop_Seg.ClearSelection()
                txt_Rut_Deu_MaskedEditExtender.Enabled = True
                IB_AyudaDeu.Enabled = True
            Else
                Drop_Seg.Enabled = True
                Drop_Seg.CssClass = "clsMandatorio"
                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.ReadOnly = True
                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.CssClass = "clsDisabled"
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                Txt_Rso_Deu.Text = ""
                IB_AyudaDeu.Enabled = False
                txt_Rut_Deu_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_Fono_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Fono.CheckedChanged
        Try
            If Check_Fono.Checked = True Then
                Drop_Cobradora.Enabled = False
                Drop_Cobradora.CssClass = "clsDisabled"
                Drop_Cobradora.ClearSelection()

            Else
                Drop_Cobradora.Enabled = True
                Drop_Cobradora.CssClass = "clsMandatorio"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_Ult_Gest_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Ult_Gest.CheckedChanged
        Try
            If Check_Ult_Gest.Checked = True Then
                txt_Gest_Dsd.Enabled = False
                txt_Gest_Hst.Enabled = False
                txt_Gest_Dsd.CssClass = "clsDisabled"
                txt_Gest_Hst.CssClass = "clsDisabled"
                txt_Gest_Dsd.Text = ""
                txt_Gest_Hst.Text = ""
            Else
                txt_Gest_Dsd.Enabled = True
                txt_Gest_Hst.Enabled = True
                txt_Gest_Dsd.CssClass = "clsMandatorio"
                txt_Gest_Hst.CssClass = "clsMandatorio"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_Tp_Doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Tp_Doc.CheckedChanged
        Try
            If Check_Tp_Doc.Checked Then
                Drop_TP_Doc.Enabled = False
                Drop_TP_Doc.CssClass = "clsDisabled"
                Drop_TP_Doc.ClearSelection()
            Else
                Drop_TP_Doc.Enabled = True
                Drop_TP_Doc.CssClass = "clsMandatorio"
                Drop_TP_Doc.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_EstCob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_EstCob.CheckedChanged
        Try
            If Check_EstCob.Checked = True Then
                txt_Est_Dsd.Text = ""
                txt_Est_Hst.Text = ""
                txt_Est_Dsd.ReadOnly = True
                txt_Est_Hst.ReadOnly = True
                txt_Est_Dsd.CssClass = "clsDisabled"
                txt_Est_Hst.CssClass = "clsDisabled"
            Else
                txt_Est_Dsd.ReadOnly = False
                txt_Est_Hst.ReadOnly = False
                txt_Est_Dsd.CssClass = "clsMandatorio"
                txt_Est_Hst.CssClass = "clsMandatorio"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese Dìgito Verificador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Txt_Dig_Cli.Text.Trim.ToUpper <> FG.Vrut(CLng(Txt_Rut_Cli.Text)).Trim.ToUpper Then
                Msj.Mensaje(Me.Page, caption, "Dìgito Incorrecto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)
            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            Else
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                Session("Cliente") = cli
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                txt_cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese Dìgito Verificador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Dig_Deu.Text.Trim.ToUpper <> FG.Vrut(CLng(Txt_Rut_Deu.Text)).Trim.ToUpper Then
                Msj.Mensaje(Me.Page, caption, "Dìgito Incorrecto", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Dim deu As deu_cls
            deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text.Replace(",", ""))
            Session("Deudor") = deu
            If Not IsNothing(deu) Then

                Txt_Rut_Deu.ReadOnly = True
                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.ReadOnly = True
                Txt_Dig_Deu.CssClass = "clsDisabled"
                Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
                txt_Rut_Deu_MaskedEditExtender.Enabled = False
                IB_AyudaDeu.Enabled = False

            Else

                Msj.Mensaje(Me.Page, caption, "Pagador no encontrado", TipoDeMensaje._Exclamacion)
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpiar()
            Check_Fono.Checked = True
            If Check_Fono.Checked = True Then
                Drop_Cobradora.Enabled = False
                Drop_Cobradora.CssClass = "clsDisabled"
                Drop_Cobradora.ClearSelection()
            End If
            Check_Tp_Doc.Checked = True
            If Check_Tp_Doc.Checked = True Then
                Drop_TP_Doc.Enabled = False
                Drop_TP_Doc.CssClass = "clsDisabled"
                Drop_TP_Doc.ClearSelection()
            End If
            Check_Ult_Gest.Checked = True
            If Check_Ult_Gest.Checked = True Then
                txt_Gest_Dsd.Enabled = False
                txt_Gest_Hst.Enabled = False
                txt_Gest_Dsd.CssClass = "clsDisabled"
                txt_Gest_Hst.CssClass = "clsDisabled"
                txt_Gest_Dsd.Text = ""
                txt_Gest_Hst.Text = ""
            End If

            Check_EstCob.Checked = True
            If Check_EstCob.Checked = True Then
                txt_Est_Dsd.Text = ""
                txt_Est_Hst.Text = ""
                txt_Est_Dsd.ReadOnly = True
                txt_Est_Hst.ReadOnly = True
                txt_Est_Dsd.CssClass = "clsDisabled"
                txt_Est_Hst.CssClass = "clsDisabled"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal
            If Not Agt.ValidaAccesso(20, 20010607, Usr, "PRESIONO BOTON IMPRIMIR GESTION COBRANZA") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Drop_Est.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione Estado de Documento", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Drop_Moneda.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione Moneda", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Check_Cli.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT Cliente", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            End If
            If Check_Deu.Checked = True Then
                If Txt_Rut_Deu.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT Pagador", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            End If

            If Check_Fono.Checked = False Then
                If Drop_Cobradora.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Selecione Cobrador ", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If



            If Check_Tp_Doc.Checked = False Then
                If Drop_TP_Doc.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, caption, "Seleccione Tipo de Documento", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If Check_Ult_Gest.Checked = False Then
                If txt_Gest_Dsd.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese Fecha ultima gestion Desde", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If txt_Gest_Dsd.Text <> "__/__/____" Then
                    If Not IsDate(txt_Gest_Dsd.Text) Then
                        Msj.Mensaje(Page, caption, "Fecha desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                        txt_Gest_Dsd.Text = ""
                        Exit Sub
                    End If
                End If

                If txt_Gest_Hst.Text = "" Then
                    Msj.Mensaje(page, caption, "Ingrese fecha ultima gestion hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If txt_Gest_Hst.Text <> "__/__/____" Then
                    If Not IsDate(txt_Gest_Hst.Text) Then
                        Msj.Mensaje(Page, caption, "Fecha hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                        txt_Gest_Hst.Text = ""
                        Exit Sub
                    End If
                End If

                If CDate(txt_Gest_Dsd.Text) > CDate(txt_Gest_Hst.Text) Then
                    Msj.Mensaje(Me.Page, caption, "Fecha Desde no Puede ser Mayor a Fecha Hasta", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If Check_EstCob.Checked = False Then

                If txt_Est_Dsd.Text = "" Then
                    Msj.Mensaje(Page, caption, "Ingrese codigo de estado de cobranza desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If txt_Est_Hst.Text = "" Then
                    Msj.Mensaje(Page, caption, "Ingrese codigo de estado de cobranza hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Trim(txt_Est_Dsd.Text) > Trim(txt_Est_Hst.Text) Then
                    Msj.Mensaje(Page, caption, "Codigo desde no puede ser mayor a codigo hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            Dim estDoc_dsd As Integer
            Dim estDoc_hst As Integer
            Dim rut_cli_dsd As String
            Dim rut_cli_hst As String
            Dim rut_deu_dsd As String
            Dim rut_deu_hst As String
            Dim seg_cli_dsd As Integer
            Dim seg_cli_hst As Integer
            Dim seg_deu_dsd As Integer
            Dim seg_deu_hst As Integer
            Dim cob_fon_dsd As Integer
            Dim cob_fon_hst As Integer
            Dim tpdoc_dsd As Integer
            Dim tpdoc_hst As Integer
            Dim fgest_dsd As String
            Dim fgest_hst As String
            Dim estcob_dsd As Integer
            Dim estcob_hst As Integer


            If Drop_Est.SelectedValue = 3 Then
                estDoc_dsd = 0
                estDoc_hst = 9999
            Else
                estDoc_dsd = Drop_Est.SelectedValue
                estDoc_hst = Drop_Est.SelectedValue
            End If

            If Txt_Rut_Cli.Text = "" Then
                rut_cli_dsd = "000000000000"
                rut_cli_hst = "999999999999"
            Else
                rut_cli_dsd = Txt_Rut_Cli.Text
                rut_cli_hst = Txt_Rut_Cli.Text
            End If

            If Txt_Rut_Deu.Text = "" Then
                rut_deu_dsd = "000000000000"
                rut_deu_hst = "999999999999"
            Else
                rut_deu_dsd = Txt_Rut_Deu.Text
                rut_deu_hst = Txt_Rut_Deu.Text
            End If

            If Drop_Segmento.SelectedValue = 0 Then
                seg_cli_dsd = 0
                seg_cli_hst = 99
            Else
                seg_cli_dsd = Drop_Segmento.SelectedValue
                seg_cli_hst = Drop_Segmento.SelectedValue
            End If

            If Drop_Seg.SelectedValue = 0 Then
                seg_deu_dsd = 0
                seg_deu_hst = 99
            Else
                seg_deu_dsd = Drop_Seg.SelectedValue
                seg_deu_hst = Drop_Seg.SelectedValue
            End If

            If Drop_Cobradora.SelectedValue = 0 Then
                cob_fon_dsd = 0
                cob_fon_hst = 9999
            Else
                cob_fon_dsd = Drop_Cobradora.SelectedValue
                cob_fon_hst = Drop_Cobradora.SelectedValue
            End If

            If Drop_TP_Doc.SelectedValue = 0 Then
                tpdoc_dsd = 0
                tpdoc_hst = 9999
            Else
                tpdoc_dsd = Drop_TP_Doc.SelectedValue
                tpdoc_hst = Drop_TP_Doc.SelectedValue
            End If

            If txt_Gest_Dsd.Text = "" Then
                fgest_dsd = "19000101"
            Else
                fgest_dsd = FG.FUNFechaJul(txt_Gest_Dsd.Text)
            End If
            If txt_Gest_Hst.Text = "" Then
                fgest_hst = "29990101"
            Else
                fgest_hst = FG.FUNFechaJul(txt_Gest_Hst.Text)
            End If

            If txt_Est_Dsd.Text = "" Then
                estcob_dsd = 0
            Else
                estcob_dsd = txt_Est_Dsd.Text
            End If
            If txt_Est_Hst.Text = "" Then
                estcob_hst = 9999
            Else
                estcob_hst = txt_Est_Hst.Text
            End If

            RW.AbrePopup(Me, 1, "Informe_Gestion.aspx?rutclidsd=" & rut_cli_dsd & "&rutclihst=" & rut_cli_hst & "&rutdeudsd=" _
                                     & rut_deu_dsd & "&rutdeuhst=" & rut_deu_hst & "&estdocdsd=" & estDoc_dsd & "&estdochst=" _
                                     & estDoc_hst & "&moneda=" & Drop_Moneda.SelectedValue & "&segclidsd=" & seg_cli_dsd _
                                     & "&segclihst=" & seg_cli_hst & "&segdeudsd=" & seg_deu_dsd & "&segdeuhst=" & seg_deu_hst _
                                     & "&cobfodsd=" & cob_fon_dsd & "&cobfohst=" & cob_fon_hst & "&tpdocdsd=" & tpdoc_dsd _
                                     & "&tpdochst=" & tpdoc_hst & "&fdsd=" & fgest_dsd & "&fhst=" & fgest_hst & "&estcobdsd=" _
                                     & estcob_dsd & "&estcobhst=" & estcob_hst, "InformeGestion", 1300, 900, 10, 10)


        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Function y Sub"
    Public Sub CargaDrop()
        'CG.ParametrosDevuelve(11,True,Drop_Est)
        CG.ParametrosDevuelve(23, True, Drop_Moneda)
        CG.ParametrosDevuelve(76, True, Drop_Segmento)
        CG.ParametrosDevuelve(76, True, Drop_Seg)
        CG.ParametrosDevuelve(31, True, Drop_TP_Doc)
        CG.EjecutivosAsignarCobradoresDevuelve(1, 29, True, Drop_Cobradora)
    End Sub

    Public Sub Limpiar()
        Drop_Moneda.ClearSelection()
        Drop_Est.ClearSelection()
        Drop_Cobradora.ClearSelection()
        Drop_Cobradora.Enabled = True
        Drop_TP_Doc.Enabled = True



        Drop_Seg.ClearSelection()
        Drop_Segmento.ClearSelection()
        Drop_TP_Doc.ClearSelection()
        Drop_Segmento.Enabled = True
        Drop_Seg.Enabled = True
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        Txt_Rso_Deu.Text = ""
        txt_Gest_Dsd.Text = ""
        txt_Gest_Hst.Text = ""
        txt_Est_Dsd.Text = ""
        txt_Est_Hst.Text = ""
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True

        Txt_Dig_Deu.ReadOnly = True
        Txt_Dig_Deu.ReadOnly = True

        Check_Cli.Checked = False
        Check_Deu.Checked = False

        Check_EstCob.Checked = False
        Check_Fono.Checked = False
        Check_Tp_Doc.Checked = False
        Check_Ult_Gest.Checked = False
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Dig_Deu.CssClass = "clsDisabled"
        txt_Est_Dsd.CssClass = "clsMandatorio"
        txt_Est_Hst.CssClass = "clsMandatorio"
        txt_Gest_Dsd.CssClass = "clsMandatorio"
        txt_Gest_Hst.CssClass = "clsMandatorio"
        Drop_Cobradora.CssClass = "clsMandatorio"
        Drop_Est.CssClass = "clsMandatorio"
        Drop_Seg.CssClass = "clsMandatorio"
        Drop_Segmento.CssClass = "clsMandatorio"
        Drop_TP_Doc.CssClass = "clsMandatorio"
        txt_cli_MaskedEditExtender.Enabled = False
        txt_Rut_Deu_MaskedEditExtender.Enabled = False
    End Sub



#End Region



    'Protected Sub txt_Gest_Dsd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Gest_Dsd.TextChanged
    '    Try
    '        If txt_Gest_Dsd.Text <> "" And txt_Gest_Dsd.Text <> "__/__/____" Then
    '            If Not IsDate(txt_Gest_Dsd.Text) Then
    '                Msj.Mensaje(page,caption,"Fecha erronea",ClsMensaje.TipoDeMensaje._Exclamacion)
    '                Exit Sub
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
