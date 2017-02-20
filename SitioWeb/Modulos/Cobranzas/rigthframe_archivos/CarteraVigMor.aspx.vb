Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Cobranzas_rigthframe_archivos_CarteraVigMor
    Inherits System.Web.UI.Page
    Dim Caption As String = "Cartera Vigente/Morosa"
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim sesion As New ClsSession.ClsSession
    Dim Var As New FuncionesGenerales.Variables
    Dim Cli As New cli_cls
    Dim Deu As New deu_cls
    Dim Ver As New ConsultasGenerales
    Dim Msj As New ClsMensaje
    Dim CBZ As New ClaseCobranza
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then
                Response.Expires = -1
                Limpiar()
                Txt_Fec_Inf.CssClass = "clsDisabled"
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Nro_Oto.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Nro_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")

            End If
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpDeudor',650,410,200,150);")

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ChKB_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_Cli.CheckedChanged
        Try
            If ChKB_Cli.Checked Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.Focus()
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                IB_AyudaCli.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChKB_Deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_Deu.CheckedChanged
        Try
            If ChKB_Deu.Checked Then
                Txt_Rut_Deu.ReadOnly = False
                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.ReadOnly = False
                Txt_Dig_Deu.CssClass = "clsMandatorio"

                Txt_Rut_Deu_MaskedEditExtender.Enabled = True
                IB_AyudaDeu.Enabled = True
                Txt_Rut_Deu.Focus()

            Else
                Txt_Rut_Deu.ReadOnly = True
                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.ReadOnly = True
                Txt_Dig_Deu.CssClass = "clsDisabled"
                Txt_Rut_Deu_MaskedEditExtender.Enabled = False
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                Txt_Rso_Deu.Text = ""
                IB_AyudaDeu.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChKB_MorCarSup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_MorCarSup.CheckedChanged
        If ChKB_MorCarSup.Checked = True Then
            Dp_CanDias.Enabled = True
        Else
            Dp_CanDias.Enabled = False
        End If
    End Sub
    Protected Sub Dp_Suc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Suc.SelectedIndexChanged
        Try
            DP_Eje.ClearSelection()
            CG.EjecutivosDevuelve(DP_Eje, CodEje, 15)
            If Dp_Suc.SelectedValue = 0 Then
                Rb_Suc.Checked = True
            Else
                Rb_Suc.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DP_Eje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Eje.SelectedIndexChanged
        Try
            If DP_Eje.SelectedValue = 0 Then
                Rb_Eje.Checked = True
            Else
                Rb_Eje.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try

            Dim id_suc1 As String, id_suc2 As String
            Dim id_eje1 As Integer, id_eje2 As Integer
            Dim CodCob1 As String, CodCob2 As String
            Dim TipDoc1 As Integer, TipDoc2 As Integer
            Dim NroOto1 As Long, NroOto2 As Long
            Dim NroDoc1 As String, nrodoc2 As String
            Dim RutCli1 As String, RutCli2 As String
            Dim RutDeu1 As String, RutDeu2 As String
            Dim TipoConsulta As Integer

            If Txt_Fec_Inf.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese fecha de informe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Inf.Text) Then
                Msj.Mensaje(Page, Caption, "Fecha de informe incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Dp_Est.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Estado", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Dp_Tip_Mon.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Moneda", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If RB_Cob.Checked = False Then
                If DP_CodCobranza_Desde.SelectedValue = 0 Then
                    Msj.Mensaje(Page, Caption, "Seleccione estado cobranza desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If DP_CodCobranza_Hasta.SelectedValue = 0 Then
                    Msj.Mensaje(Page, Caption, "Seleccione estado cobranza hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If DP_CodCobranza_Desde.SelectedValue > DP_CodCobranza_Hasta.SelectedValue Then
                    Msj.Mensaje(Page, Caption, "Codigo cobranza desde no puede ser mayor a codigo cobranza hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            End If

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20010507, Usr, "PRESIONO BOTON IMPRIMIR") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Rb_Suc.Checked Then
                id_suc1 = 0
                id_suc2 = 999
            Else
                id_suc1 = Dp_Suc.SelectedValue
                id_suc2 = Dp_Suc.SelectedValue
            End If

            If Rb_Eje.Checked Then
                id_eje1 = 0
                id_eje2 = 9999
            Else
                id_eje1 = DP_Eje.SelectedValue  'Dp_Est.SelectedValue
                id_eje2 = DP_Eje.SelectedValue  'Dp_Est.SelectedValue
            End If

            If RB_Cob.Checked Then
                CodCob1 = "0000"
                CodCob2 = "9999"
            Else
                CodCob1 = DP_CodCobranza_Desde.SelectedValue
                CodCob2 = DP_CodCobranza_Hasta.SelectedValue
            End If

            If RB_Doc.Checked Then
                TipDoc1 = "0"
                TipDoc2 = 999
            Else
                If Dp_Tip_Doc.SelectedValue = 0 Then
                    TipDoc1 = "0"
                    TipDoc2 = 999
                Else
                    TipDoc1 = Dp_Tip_Doc.SelectedValue
                    TipDoc2 = Dp_Tip_Doc.SelectedValue
                End If
            End If

            If Trim(Txt_Nro_Oto.Text) = "" Then
                NroOto1 = 0
                NroOto2 = 99999999
            Else
                NroOto1 = Txt_Nro_Oto.Text
                NroOto2 = Txt_Nro_Oto.Text
            End If

            If Trim(Txt_Nro_Doc.Text) = "" Then
                NroDoc1 = "0"
                nrodoc2 = "Z"
            Else
                NroDoc1 = Txt_Nro_Doc.Text
                NroDoc2 = Txt_Nro_Doc.Text
            End If

            If ChKB_Cli.Checked Then
                If Trim(Txt_Rut_Cli.Text) = "" Then
                    RutCli1 = "000000000000"
                    RutCli2 = "9999999999999"
                Else
                    If UCase(Trim(Txt_Dig_Cli.Text)) <> FC.Vrut(Trim(Txt_Rut_Cli.Text)) Then
                        Msj.Mensaje(Me.Page, Caption, "Rut de Cliente incorrecto", TipoDeMensaje._Exclamacion)
                        Txt_Rut_Cli.Focus()
                        Exit Sub
                    End If
                    RutCli1 = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                    RutCli2 = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                End If
            Else
                RutCli1 = "000000000000"
                RutCli2 = "9999999999999"
            End If

            If ChKB_Deu.Checked Then
                If Trim(Txt_Rut_Deu.Text) = "" Then
                    TipoConsulta = 3
                    RutDeu1 = "000000000000"
                    RutDeu2 = "9999999999999"
                Else
                    If UCase(Trim(Txt_Dig_Deu.Text)) <> FC.Vrut(Trim(Txt_Rut_Deu.Text)) Then
                        Msj.Mensaje(Me.Page, Caption, "NIT de Pagador incorrecto", TipoDeMensaje._Exclamacion)
                        Txt_Rut_Deu.Focus()
                        Exit Sub
                    End If
                    RutDeu1 = Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT)
                    RutDeu2 = Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT)
                End If
            Else
                TipoConsulta = 0
                RutDeu1 = "000000000000"
                RutDeu2 = "9999999999999"
            End If


            Select Case Dp_Est.SelectedValue
                Case 1
                    TipoConsulta = TipoConsulta + 1 'vigentes
                Case 2
                    TipoConsulta = TipoConsulta + 2 'morosos 
                Case 3
                    TipoConsulta = TipoConsulta + 3 'todos
                Case 4
                    TipoConsulta = TipoConsulta + 4 'menos de 30 dias
            End Select

            ' If Dp_Est.SelectedValue = 3 Then TipoConsulta = TipoConsulta + 1


            AbrePopup(Me, 1, "InformeCarteraVigMor.aspx?FecInf=" & Txt_Fec_Inf.Text & "&Estado=" & Dp_Est.SelectedValue & "&Moneda=" & Dp_Tip_Mon.SelectedValue & _
                                                        "&id_suc1=" & id_suc1 & "&id_suc2=" & id_suc2 & "&id_eje1=" & id_eje1 & "&id_eje2=" & id_eje2 & _
                                                        "&CodCob1=" & CodCob1 & "&CodCob2=" & CodCob2 & "&TipDoc1=" & TipDoc1 & "&TipDoc2=" & TipDoc2 & _
                                                        "&NroOto1=" & NroOto1 & "&NroOto2=" & NroOto2 & "&NroDoc1=" & NroDoc1 & "&NroDoc2=" & NroDoc2 & _
                                                        "&RutCli1=" & RutCli1 & "&RutCli2=" & RutCli2 & "&RutDeu1=" & RutDeu1 & "&RutDeu2=" & RutDeu2 & _
                                                        "&TipoConsulta=" & TipoConsulta, "Documentos", 1200, 800, 100, 10)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpiar()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DP_Car_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Car.SelectedIndexChanged
        Try

            If DP_Car.SelectedValue > 0 Then
                RB_Car.Checked = False
            Else
                RB_Car.Checked = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RB_Car_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Car.CheckedChanged
        Try
            If RB_Car.Checked = True Then
                DP_Car.ClearSelection()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rb_Suc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Suc.CheckedChanged
        Try
            If Rb_Suc.Checked = True Then
                Dp_Suc.ClearSelection()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rb_Eje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Eje.CheckedChanged
        Try
            If Rb_Eje.Checked = True Then
                DP_Eje.ClearSelection()
            End If
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub Dp_Tip_Doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Tip_Doc.SelectedIndexChanged
        Try
            If Dp_Tip_Doc.SelectedValue > 0 Then
                RB_Doc.Checked = False
            Else
                RB_Doc.Checked = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RB_Doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Doc.CheckedChanged
        Try
            If RB_Doc.Checked = True Then
                Dp_Tip_Doc.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Procedimientos y Funciones Generales"

    Sub Limpiar()

        Try

            Txt_Fec_Inf.Text = Format(Date.Now, "dd/MM/yyyy")

            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Tip_Mon)
            CG.SucursalesDevuelve(codeje, True, Dp_Suc)

            DP_Eje.ClearSelection()
            CG.CarteraClienteDevuelve(1, DP_Car, 0)

            Dp_Est.ClearSelection()
            Dp_Tip_Mon.ClearSelection()

            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_Tip_Doc)
            CG.EjecutivosDevuelve(DP_Eje, CodEje, 15)

            For I = 15 To 100
                Dp_CanDias.Items.Add(I)
            Next

            DP_CodCobranza_Desde.ClearSelection()
            DP_CodCobranza_Hasta.ClearSelection()

            CBZ.CobranzaEstadosDevuelve(DP_CodCobranza_Desde)
            CBZ.CobranzaEstadosDevuelve(DP_CodCobranza_Hasta)

            Txt_Nro_Oto.Text = ""
            Txt_Nro_Doc.Text = ""
            ChKB_Cli.Checked = False
            ChKB_Deu.Checked = False

            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""

            Txt_Dig_Deu.Text = ""
            Txt_Rut_Deu.Text = ""
            Txt_Rso_Deu.Text = ""

        Catch ex As Exception

        End Try
    End Sub

#End Region
#Region "LinkButton"
    Protected Sub LB_Cobranza_Dsd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Cobranza_Dsd.Click

        Try
            Dim Cco As New cco_cls

            RB_Cob.Checked = False

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub LB_Cobranza_Hst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Cobranza_Hst.Click
        Try
            Dim Cco As New cco_cls



            RB_Cob.Checked = False

        Catch ex As Exception

        End Try
    End Sub

#End Region
    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            If UCase(Txt_Dig_Cli.Text) <> FC.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            Dim CLSCLI As New ClaseClientes

            Cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)
            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            If IsNothing(Cli) Then
                Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If

            Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False

            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            If UCase(Txt_Dig_Deu.Text) <> FC.Vrut(CLng(Txt_Rut_Deu.Text)) Then
                Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If
            Deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT))
            If IsNothing(Deu) Then
                Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            Txt_Rso_Deu.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
            Txt_Rut_Deu.ReadOnly = True
            Txt_Dig_Deu.ReadOnly = True
            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.CssClass = "clsDisabled"
            Txt_Rut_Deu_MaskedEditExtender.Enabled = False
            IB_AyudaDeu.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DP_CodCobranza_Desde_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_CodCobranza_Desde.SelectedIndexChanged
        Try
            If DP_CodCobranza_Desde.SelectedValue > 0 Then
                RB_Cob.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RB_Cob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Cob.CheckedChanged
        Try
            If RB_Cob.Checked = True Then
                DP_CodCobranza_Desde.ClearSelection()
                DP_CodCobranza_Hasta.ClearSelection()

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
