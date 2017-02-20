Imports ClsSession.ClsSession
Imports System.IO

Imports CapaDatos
Partial Class Modulos_Alertas_rightframe_archivos_Alertas
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Alertas"
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

  
#End Region

    Private Sub LimpiaGrid()

        GV_PorVencer.DataSource = Nothing
        GV_PorVencer.DataBind()

        GV_Excedentes.DataSource = Nothing
        GV_Excedentes.DataBind()

        GV_Mora.DataSource = Nothing
        GV_Mora.DataBind()

        GV_Lineas.DataSource = Nothing
        GV_Lineas.DataBind()

    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        Try
            If CB_Cliente.Checked Then

                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Me.IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False

                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"

                Txt_Rut_Cli.Focus()
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True

                DP_Ejecutivos.ClearSelection()
                DP_Ejecutivos.Enabled = False
                DP_Ejecutivos.CssClass = "clsDisabled"
            Else
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Me.IB_AyudaCli.Enabled = False
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False

                DP_Ejecutivos.Enabled = True
                DP_Ejecutivos.CssClass = "clsMandatorio"
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            Dim DocVen As Integer = 0
            Dim DocMor As Integer = 0
            Dim Lin As Integer = 0
            Dim exc As Integer = 0

            If Not agt.ValidaAccesso(20, 20010504, Usr, "PRESIONO BUSCAR ALERTAS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            LimpiaGrid()

            '--------------------------------------------------------------------------------------------------------------------
            'Obtenemos los parametros de alertas (Dias antes)
            '--------------------------------------------------------------------------------------------------------------------
            Dim ParametrosAlertas As pta_cls

            If DP_Ejecutivos.SelectedIndex <= 0 And Not CB_Cliente.Checked Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un ejecutivo", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Dim RutCliente_Desde As Long
            Dim RutCliente_Hasta As Long

            If CB_Cliente.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                RutCliente_Desde = Txt_Rut_Cli.Text
                RutCliente_Hasta = Txt_Rut_Cli.Text
                RutCli = Txt_Rut_Cli.Text

                ParametrosAlertas = CG.AlertasParametros_Devuelve(HF_Ejecutivo.Value)
            Else
                RutCliente_Desde = 0
                RutCliente_Hasta = 999999999999
                RutCli = 0
                ParametrosAlertas = CG.AlertasParametros_Devuelve(DP_Ejecutivos.SelectedValue)

            End If


            ParametrosAlertas = CG.AlertasParametros_Devuelve(IIf(CB_Cliente.Checked, DP_Ejecutivos.SelectedValue, CodEje))

            If IsNothing(ParametrosAlertas) Then
                Msj.Mensaje(Me.Page, Caption, "No se encuentran los parámetros de alerta para este ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Carga_DoctosPorVencer(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)

            Carga_DosctosEnMora(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)

            Carga_Alertas_Lineas(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)

            Carga_AlertasExcedentes(RutCliente_Desde, RutCliente_Hasta)

            Select Case TabContainer1.ActiveTabIndex
                Case 0 'Por vencer 

                    If GV_PorVencer.Rows.Count = 0 Then
                        Msj.Mensaje(Page, Caption, "No se encuentran documentos por vencer", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 1 'mora

                    If GV_Mora.Rows.Count = 0 Then
                        Msj.Mensaje(Page, Caption, "No se encuentran documentos en mora", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                Case 2 'linea

                    If GV_Lineas.Rows.Count = 0 Then

                        Msj.Mensaje(Page, Caption, "No se encuentran lineas", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub

                    End If

                Case 3 'excedente

                    If GV_Excedentes.Rows.Count = 0 Then

                        Msj.Mensaje(Page, Caption, "No se encuentran excedentes", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub

                    End If

            End Select

            If GV_Excedentes.Rows.Count = 0 _
              And GV_Lineas.Rows.Count = 0 _
              And GV_Mora.Rows.Count = 0 _
              And GV_PorVencer.Rows.Count = 0 Then

                IB_Imprimir.Enabled = False
            Else
                IB_Imprimir.Enabled = True
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Response.Expires = -1
                Txt_Rut_Cli.Attributes.Add("Style", "Text-Align: right")
                CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)

                NroPaginacion_AlertaDoctoxVencer = 0
                NroPaginacion_DoctoEnMora = 0
                NroPaginacion_AlertaEnLinea = 0
                NroPaginacion_AlertaExcedente = 0
            End If

            IB_Imprimir.Attributes.Add("onClick", "WinOpen(1, 'InformeDeAlertas.aspx', 'InformeDeAlertas.aspx', 1200, 1000, 5, 5); ")
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Exportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Exportar.Click

        Try


            If Not agt.ValidaAccesso(20, 20030504, Usr, "PRESIONO EXPORTAR ALERTAS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Dim sb As StringBuilder = New StringBuilder()
            Dim sw As StringWriter = New StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Dim pagina As Page = New Page
            Dim form = New HtmlForm

            pagina.EnableEventValidation = False
            pagina.DesignerInitialize()
            pagina.Controls.Add(form)


            Select Case TabContainer1.ActiveTabIndex

                Case 0 'Por vencer 

                    GV_PorVencer.EnableViewState = False

                    ' form.Controls.Add(Tb_Vencidos)
                    form.Controls.Add(GV_PorVencer)

                    pagina.RenderControl(htw)

                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment;filename=DoctosPorVencer.xls")

                Case 1 'mora

                    GV_Mora.EnableViewState = False

                    'form.Controls.Add(Tb_Mora)
                    form.Controls.Add(GV_Mora)

                    pagina.RenderControl(htw)

                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment;filename=DoctosEnMora.xls")


                Case 2 'linea

                    GV_Lineas.EnableViewState = False

                    'form.Controls.Add(Tb_Lineas)
                    form.Controls.Add(GV_Lineas)

                    pagina.RenderControl(htw)

                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment;filename=Lineas.xls")

                Case 3 'excedente

                    GV_Excedentes.EnableViewState = False

                    ' form.Controls.Add(Tb_Excedentes)
                    form.Controls.Add(GV_Excedentes)

                    pagina.RenderControl(htw)

                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment;filename=Excedentes.xls")

            End Select

            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Parametros_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Parametros.Click
        Try


            If DP_Ejecutivos.SelectedIndex <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un ejecutivo", TipoDeMensaje._Informacion)
                Exit Sub
            Else

                'CodEje = DP_Ejecutivos.SelectedValuee
                Ejecutivo = 0


                Ejecutivo = DP_Ejecutivos.SelectedValue


                RW.AbrePopup(Me, 2, "ParametrosDeAlertas.aspx", "ParametrosDeAlertas", 420, 300, 200, 2)

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"

            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"

            If CB_Cliente.Checked Then

                CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), UCase(Txt_Dig_Cli.Text))

                If Sesion.valida_cliente <> "" Then
                    Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Function
                End If

                If IsNothing(CLI) Then
                    Msj.Mensaje(Me.Page, Caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                    Exit Function
                End If

                Session("Cliente") = CLI

                'Tipo de cliente (Natural / Juridico)
                If CLI.id_P_0044 = 1 Then
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
                Else
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso
                End If

                HF_Ejecutivo.Value = CLI.id_eje_cod_eje
                UP_Cliente.Update()

                Return True

            End If



        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            If CargaDatosCliente() Then
                IB_AyudaCli.Enabled = False
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        DP_Ejecutivos.ClearSelection()
        CB_Cliente.Checked = False
        DP_Ejecutivos.Enabled = True
        DP_Ejecutivos.CssClass = "clsMandatorio"
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Raz_Soc.Text = ""

        GV_Excedentes.DataSource = New Collection
        GV_Excedentes.DataBind()

        GV_Lineas.DataSource = New Collection
        GV_Lineas.DataBind()

        GV_Mora.DataSource = New Collection
        GV_Mora.DataBind()

        GV_PorVencer.DataSource = New Collection
        GV_PorVencer.DataBind()

        IB_Imprimir.Enabled = False
        'IB_Exportar.Enabled = False
        TabContainer1.ActiveTabIndex = 0
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False


    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            Dim RutCliente_Desde As Long
            Dim RutCliente_Hasta As Long

            If CB_Cliente.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                RutCliente_Desde = Txt_Rut_Cli.Text
                RutCliente_Hasta = Txt_Rut_Cli.Text
                RutCli = Txt_Rut_Cli.Text
            Else
                RutCliente_Desde = 0
                RutCliente_Hasta = 999999999999
                RutCli = 0
            End If


            ' --------------------------------------------------------------------------------------------------------------------
            'Obtenemos los parametros de alertas (Dias antes)
            '--------------------------------------------------------------------------------------------------------------------
            Dim ParametrosAlertas As pta_cls

            ParametrosAlertas = CG.AlertasParametros_Devuelve(DP_Ejecutivos.SelectedValue)

            If IsNothing(ParametrosAlertas) Then
                Msj.Mensaje(Me.Page, Caption, "No se encuentran los parámetros de alerta para este ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            ' --------------------------------------------------------------------------------------------------------------------

            Select Case TabContainer1.ActiveTabIndex
                Case 0 'Por vencer 
                    If GV_PorVencer.Rows.Count < 15 Then
                        Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                        Exit Sub
                    End If
                    If GV_PorVencer.Rows.Count = 15 Then
                        NroPaginacion_AlertaDoctoxVencer += 15
                        Carga_DoctosPorVencer(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)
                    End If

                Case 1 'mora
                    If GV_Mora.Rows.Count < 15 Then
                        Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                        Exit Sub
                    End If

                    If GV_Mora.Rows.Count = 15 Then
                        NroPaginacion_DoctoEnMora += 15
                        Carga_DosctosEnMora(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)
                    End If

                Case 2 'linea
                    If GV_Lineas.Rows.Count < 15 Then
                        Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                        Exit Sub
                    End If

                    If GV_Lineas.Rows.Count = 15 Then
                        NroPaginacion_AlertaEnLinea += 15
                        Carga_Alertas_Lineas(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)
                    End If
                Case 3 'excedente
                    If GV_Excedentes.Rows.Count < 15 Then
                        Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                        Exit Sub
                    End If

                    If GV_Excedentes.Rows.Count = 15 Then
                        NroPaginacion_AlertaExcedente += 15
                        Carga_AlertasExcedentes(RutCliente_Desde, RutCliente_Hasta)
                    End If
            End Select
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try

            Dim RutCliente_Desde As Long
            Dim RutCliente_Hasta As Long

            If CB_Cliente.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                RutCliente_Desde = Txt_Rut_Cli.Text
                RutCliente_Hasta = Txt_Rut_Cli.Text
                RutCli = Txt_Rut_Cli.Text
            Else
                RutCliente_Desde = 0
                RutCliente_Hasta = 999999999999
                RutCli = 0
            End If


            '--------------------------------------------------------------------------------------------------------------------
            'Obtenemos los parametros de alertas (Dias antes)
            '--------------------------------------------------------------------------------------------------------------------
            Dim ParametrosAlertas As pta_cls

            ParametrosAlertas = CG.AlertasParametros_Devuelve(DP_Ejecutivos.SelectedValue)

            If IsNothing(ParametrosAlertas) Then
                Msj.Mensaje(Me.Page, Caption, "No se encuentran los parámetros de alerta para este ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            '--------------------------------------------------------------------------------------------------------------------


            Select Case TabContainer1.ActiveTabIndex
                Case 0 'Por vencer 

                    If NroPaginacion_AlertaDoctoxVencer = 0 Then
                        Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                        Exit Sub
                    End If

                    If NroPaginacion_AlertaDoctoxVencer >= 15 Then
                        NroPaginacion_AlertaDoctoxVencer -= 15
                        Carga_DoctosPorVencer(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)
                    End If

                Case 1 'mora

                    If NroPaginacion_DoctoEnMora = 0 Then
                        Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                        Exit Sub
                    End If

                    If NroPaginacion_DoctoEnMora >= 15 Then
                        NroPaginacion_DoctoEnMora -= 15
                        Carga_DosctosEnMora(RutCliente_Desde, RutCliente_Hasta, ParametrosAlertas)
                    End If
                Case 2 'linea
                    If NroPaginacion_AlertaEnLinea = 0 Then
                        Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                        Exit Sub
                    End If
                    If NroPaginacion_AlertaEnLinea >= 15 Then
                        NroPaginacion_AlertaEnLinea -= 15
                    End If
                Case 3 'excedente
                    If NroPaginacion_AlertaExcedente = 0 Then
                        Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                        Exit Sub
                    End If
                    If NroPaginacion_AlertaExcedente >= 15 Then
                        NroPaginacion_AlertaExcedente -= 15
                        Carga_AlertasExcedentes(RutCliente_Desde, RutCliente_Hasta)
                    End If
            End Select


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub Carga_DoctosPorVencer(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                     ByVal ParametrosAlertas As Object)
        'Documentos por Vencer
        '--------------------------------------------------------------------------------------------------------------------
        Dim Coll_Doc_Ven As Collection

        Coll_Doc_Ven = CG.Alertas_DoctosPorVencer(RutCliente_Desde, RutCliente_Hasta, DP_Ejecutivos.SelectedValue, ParametrosAlertas.pta_dia_ven)

        If Not IsNothing(Coll_Doc_Ven) Then
            If Coll_Doc_Ven.Count = 0 Then
                ' Msj.Mensaje(Page, Caption, "No se encuentran documentos por vencer", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                GV_PorVencer.DataSource = Coll_Doc_Ven
                GV_PorVencer.DataBind()

                For V = 0 To GV_PorVencer.Rows.Count - 1

                    'Dim Rut_cli As Integer = GV_PorVencer.Rows(V).Cells(0).Text
                    'Dim rut_deu As Long = GV_PorVencer.Rows(V).Cells(2).Text

                    'GV_PorVencer.Rows(V).Cells(0).Text = Format(Rut_cli, Fmt.FCMSD) & "-" & FC.Vrut(Rut_cli)
                    'GV_PorVencer.Rows(V).Cells(2).Text = Format(Rut_deu, Fmt.FCMSD) & "-" & FC.Vrut(Rut_deu)

                    GV_PorVencer.Rows(V).Cells(9).Text = Format(Val(GV_PorVencer.Rows(V).Cells(9).Text), Fmt.FCMSD)
                    GV_PorVencer.Rows(V).Cells(10).Text = Format(Val(GV_PorVencer.Rows(V).Cells(10).Text), Fmt.FCMSD)

                Next
                'IB_Exportar.Enabled = True
                IB_Imprimir.Enabled = True
            End If
        End If
    End Sub

    Public Sub Carga_DosctosEnMora(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, ByVal ParametrosAlertas As Object)
        Try
            '--------------------------------------------------------------------------------------------------------------------
            'Documentos en Mora
            '--------------------------------------------------------------------------------------------------------------------
            Dim Coll_Doc_Mor As Collection

            Coll_Doc_Mor = CG.Alertas_DoctosEnMora(RutCliente_Desde, RutCliente_Hasta, DP_Ejecutivos.SelectedValue, ParametrosAlertas.pta_dia_mor)
            If Not IsNothing(Coll_Doc_Mor) Then
                If Coll_Doc_Mor.Count = 0 Then
                    'Msj.Mensaje(Page, Caption, "No se encuentran documentos en mora", ClsMensaje.TipoDeMensaje._Exclamacion)
                    'DocMor = 1
                Else

                    GV_Mora.DataSource = Coll_Doc_Mor
                    GV_Mora.DataBind()

                    For V = 0 To GV_Mora.Rows.Count - 1

                        Dim Rut_cli As Integer = GV_Mora.Rows(V).Cells(0).Text
                        Dim rut_deu As Long = GV_Mora.Rows(V).Cells(2).Text

                        GV_Mora.Rows(V).Cells(0).Text = Format(Rut_cli, Fmt.FCMSD) & "-" & FC.Vrut(Rut_cli)
                        GV_Mora.Rows(V).Cells(2).Text = Format(Rut_deu, Fmt.FCMSD) & "-" & FC.Vrut(Rut_deu)

                        GV_Mora.Rows(V).Cells(9).Text = Format(Val(GV_Mora.Rows(V).Cells(9).Text), Fmt.FCMSD)
                        GV_Mora.Rows(V).Cells(10).Text = Format(Val(GV_Mora.Rows(V).Cells(10).Text), Fmt.FCMSD)

                    Next
                    'IB_Exportar.Enabled = True
                    IB_Imprimir.Enabled = True
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub Carga_Alertas_Lineas(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, ByVal ParametrosAlertas As Object)
        Try
            Dim Coll_Linea As Collection

            Coll_Linea = CG.Alertas_Lineas(RutCliente_Desde, RutCliente_Hasta, DP_Ejecutivos.SelectedValue, ParametrosAlertas.pta_dia_lin)
            If Not IsNothing(Coll_Linea) Then
                If Coll_Linea.Count = 0 Then
                    ' Msj.Mensaje(Page, Caption, "No se encuentran lineas", ClsMensaje.TipoDeMensaje._Exclamacion)
                    'Lin = 1
                Else
                    GV_Lineas.DataSource = Coll_Linea
                    GV_Lineas.DataBind()
                    Try
                        For L = 0 To GV_Lineas.Rows.Count - 1

                            Dim rut As Long = GV_Lineas.Rows(L).Cells(0).Text
                            GV_Lineas.Rows(L).Cells(0).Text = Format(Rut, Fmt.FCMSD) & "-" & FC.Vrut(Rut)
                            GV_Lineas.Rows(L).Cells(6).Text = Format(Val(GV_Lineas.Rows(L).Cells(6).Text), Fmt.FCMSD)
                            GV_Lineas.Rows(L).Cells(7).Text = Format(Val(GV_Lineas.Rows(L).Cells(7).Text), Fmt.FCMSD)

                            If Coll_Linea.Item(L + 1).Mto_Ocupado > Coll_Linea.Item(L + 1).Mto_Aprobado Then
                                Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FF6666")
                                GV_Lineas.Rows(L).BackColor = col
                            End If
                        Next
                        ' IB_Exportar.Enabled = True
                        IB_Imprimir.Enabled = True
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub Carga_AlertasExcedentes(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long)
        Try
            Dim Coll_Exc As Collection

            Coll_Exc = CG.Alertas_Excedentes(RutCliente_Desde, RutCliente_Hasta, DP_Ejecutivos.SelectedValue)
            If Not IsNothing(Coll_Exc) Then
                If Coll_Exc.Count = 0 Then
                    '  Msj.Mensaje(Page, Caption, "No se encuentran excedentes", ClsMensaje.TipoDeMensaje._Exclamacion)
                    'exc = 1
                Else

                    GV_Excedentes.DataSource = Coll_Exc
                    GV_Excedentes.DataBind()

                    For V = 0 To GV_Excedentes.Rows.Count - 1

                        Dim Rut_cli As Integer = GV_Excedentes.Rows(V).Cells(0).Text

                        GV_Excedentes.Rows(V).Cells(0).Text = Format(Rut_cli, Fmt.FCMSD) & "-" & FC.Vrut(Rut_cli)

                        GV_Excedentes.Rows(V).Cells(2).Text = Format(Val(GV_Excedentes.Rows(V).Cells(2).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(3).Text = Format(Val(GV_Excedentes.Rows(V).Cells(3).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(4).Text = Format(Val(GV_Excedentes.Rows(V).Cells(4).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(5).Text = Format(Val(GV_Excedentes.Rows(V).Cells(5).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(6).Text = Format(Val(GV_Excedentes.Rows(V).Cells(6).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(7).Text = Format(Val(GV_Excedentes.Rows(V).Cells(7).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(8).Text = Format(Val(GV_Excedentes.Rows(V).Cells(8).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(9).Text = Format(Val(GV_Excedentes.Rows(V).Cells(9).Text), Fmt.FCMSD)
                        GV_Excedentes.Rows(V).Cells(10).Text = Format(Val(GV_Excedentes.Rows(V).Cells(10).Text), Fmt.FCMSD)

                    Next
                    'IB_Exportar.Enabled = True
                    IB_Imprimir.Enabled = True

                End If
            End If
            Ejecutivo = DP_Ejecutivos.SelectedValue
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

End Class
