Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports ClsMensaje

Partial Class Modulos_Pizarras_rigthframe_archivos_VistaEvaluacion
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Coll_RSD As Collection
    Dim Caption As String = "Evaluación Cliente/Deudor"
    Dim Caso As Integer
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CMC As New ClaseComercial
    Dim OP As New ClaseOperaciones
    Dim Msj As New ClsMensaje
    Dim CA As New ClaseArchivos


#End Region

#Region "Eventos"


    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Expires = -1
        If Not Page.IsPostBack Then


            Session("Coll_RSD") = New Collection

            AlineaMontosDerecha()
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_TipoMoneda)

            Try

                If Not CargaDatosCliente() Then Exit Sub

                CargaDatosLineaCredito()
                CargaDatosAnticipos()
                CargaDatosGral()
                BloqueaTextosClientes()
                Session("ACCION_COMERCIAL") = "NUEVO"


            Catch ex As Exception
                Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
            End Try

            If Request.QueryString("id") <> "" Then
                'Msj(Caption, "Hay que buscar la eva y cargarla", TipoDeMensaje._Informacion)
                HF_Accion.Value = "MODIFICA"
                CargaEvaluacion()
            Else
                HF_Accion.Value = "NUEVO"
                'Txt_Rut_Deu.Focus()
            End If

        End If

        'IB_Informe.Attributes.Add("onClick", "javascript:SendToPdf('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx'," & HF_NroEva.Value & "," & 2 & ");")
                


    End Sub

    Protected Sub LB_BuscaDeudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDeudor.Click
        Try

            If Txt_Rut_Cli.Text.Trim = "" Then
                Msj.Mensaje(Page, Caption, "Debe ingresar un cliente", TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            Dim Deu As deu_cls

            'Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

            If Not IsNothing(Deu) Then

                BloqueaTextosDeudores()
                HabilitarTxtDcto()
                If Deu.deu_nom.Trim = "" Then
                    'Txt_Rso_Deu.Text = Deu.deu_nom.Trim & " " & Deu.deu_ape_ptn.Trim & "" & Deu.deu_ape_mtn.Trim
                Else
                    'Txt_Rso_Deu.Text = Deu.deu_rso.Trim
                End If

            Else
                Msj.Mensaje(Page, Caption, "No existe deudor", TipoDeMensaje._Exclamacion, , False)
                'Txt_Rso_Deu.CssClass = "clsMandatorio"
                'Txt_Rso_Deu.ReadOnly = False
            End If

            EXISTE_DEUDOR_EVA()

            'If Not CG.RelacionClienteDeudorDevuelve(Txt_Rut_Deu.Text.Replace(".", ""), "A", Txt_Rut_Cli.Text.Replace(".", "")) Then
            '    Msj(Caption, "Este Deudor no tiene relación con este cliente, ¿Desea agregarlo?", TipoDeMensaje._Confirmacion)
            'End If

            Txt_Por_Ant.Focus()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.LimpiaDatosGenerales()
        Me.LimpiaDatosClientes()
        LimpiaDeudor()
        Me.LimpiaTotales()
        Response.Redirect("Evaluacion.aspx")
    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If Not CargaDatosCliente() Then Exit Sub

            CargaDatosLineaCredito()
            CargaDatosAnticipos()
            CargaDatosGral()
            BloqueaTextosClientes()

            Session("ACCION_COMERCIAL") = "NUEVO"

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub LB_Mensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub LB_NoExisteDeu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        HabilitarTxtDcto()
    End Sub

    Protected Sub LB_RelCliDeu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'falta validar relacion
        HabilitarTxtDcto()
    End Sub

    Protected Sub DP_TipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoMoneda.SelectedIndexChanged
        Try

            Select Case DP_TipoMoneda.SelectedValue
                Case 1
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999.99"
            End Select

            Txt_Mto_Doc.Text = ""
            Txt_Mto_Doc.Focus()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub Txt_Mto_Doc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Mto_Doc.TextChanged
        Try

            CalculaMontoEvaluacion()
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub Txt_Por_Ant_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Por_Ant.TextChanged
        Try

            CalculaMontoEvaluacion()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click

        Try

            If HF_NroEva.Value <> "" Then
                NroEvaluacion = HF_NroEva.Value
            End If


            Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(HF_NroEva.Value)

            If abytFileData.Length <> 0 Then
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                Response.AddHeader("cache-control", "private")
                Response.AddHeader("Expires", "0")
                Response.AddHeader("Pragma", "cache")
                Response.AddHeader("content-disposition", "attachment; filename=Eva" & Txt_Rut_Cli.Text & "_" & HF_NroEva.Value & ".pdf")
                Response.AddHeader("Accept-Ranges", "none")
                Response.BinaryWrite(abytFileData)
                Response.Flush()
                Response.End()

            Else
                RW.AbrePopup(Me, 2, "../../Carp. Comercial/rigthframe_archivos/Reporte_EvaluacionCliDeu.aspx?Moneda=" & DP_TipoMoneda.SelectedValue & "&Porcentaje=" & Txt_Por_Ant.Text & "&id=" & HF_NroEva.Value, "RepEvaCliDeu", 1280, 1024, 0, 0)
            End If



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

#End Region

#Region "Private Sub y Function"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ConsultasGenerales
        Dim CLI As cli_cls

        Try

            If IsNothing(Session("Cliente")) Then
                Msj.Mensaje(Page, Caption, "Debe ingresar un cliente", TipoDeMensaje._Informacion, , False)
                Return False
            End If

            'If UCase(Txt_Dig_Cli.Text) <> UCase(RG.Vrut(Txt_Rut_Cli.Text)) Then
            '    Msj(Caption, "Rut Incorrecto del Cliente", TipoDeMensaje._Informacion)
            '    Caso = 1
            '    Exit Function
            'End If

            CLI = Session("Cliente") 'ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text)

            If IsNothing(CLI) Then
                Msj.Mensaje(Page, Caption, "Cliente no existe", TipoDeMensaje._Informacion, , False)
                Caso = 1
                Exit Function
            Else
                'If CLI.id_eje_cod_eje <> Sesion.CodEje Then
                '    Msj(Caption, "Cliente asignado a otro ejecutivo", TipoDeMensaje._Informacion)
                '    Caso = 1
                '    Exit Function
                'End If
            End If

            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            Txt_Rut_Cli.Text = Format(CDbl(cli.cli_idc), FMT.FCMSD)
            Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))
            LB_Cliente.Text = Txt_Rut_Cli.Text & "-" & Txt_Dig_Cli.Text & " " & Txt_Raz_Soc.Text

            If Not IsNothing(CLI.P_0058_cls) Then
                Me.Txt_CatRiesgo.Text = CLI.P_0058_cls.pnu_des.Trim
            End If

            If Not IsNothing(CLI.eje_cls) Then
                Me.Txt_Ejecutivo.Text = CLI.eje_cls.eje_nom.Trim
            End If

            If Not IsNothing(CLI.crt_cls) Then
                Me.Txt_Cartera.Text = CLI.crt_cls.crt_des.Trim
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
            Return False

        End Try
    End Function

    Private Sub CargaDatosLineaCredito()
        Try

            Dim ClsLDC As New ConsultasGenerales
            Dim RG As New FuncionesGenerales.FComunes
            Dim LDC As ldc_cls

            LDC = ClsLDC.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text, 1)

            If IsNothing(LDC) Then
                Msj.Mensaje(Page, Caption, "Cliente no tiene linea de credito vigente", TipoDeMensaje._Informacion, , False)
                Me.Txt_Spread.Text = 0
                Me.Txt_LineaApro.Text = 0
                Me.Txt_LineaOcu.Text = 0
                Me.Txt_LineaDis.Text = 0
                Caso = 1
                Exit Sub
            End If


            id_ldc.Value = LDC.id_ldc
            'Me.Txt_Spread.Text = Format(LDC.ldc_spr_col, FMT.FCMSD)
            Txt_Spread.Text = LDC.ldc_spr_col
            Me.Txt_LineaApro.Text = Format(LDC.ldc_mto_apb, FMT.FCMSD)
            Me.Txt_LineaOcu.Text = Format(LDC.ldc_mto_ocp, FMT.FCMSD)

            If Txt_LineaOcu.Text = "" Then
                Txt_LineaOcu.Text = 0
            End If

            'Me.Txt_LineaDis.Text = Format(LDC.ldc_mto_sol, FMT.FCMSD)
            Me.Txt_LineaDis.Text = CDbl(Txt_LineaApro.Text) - CDbl(Txt_LineaOcu.Text)

            Txt_LineaDis.Text = Format(CDbl(Txt_LineaDis.Text), FMT.FCMSD)

            Me.Txt_FecVctoLin.Text = RG.FUNFecReg(LDC.ldc_fec_vig_hta)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Private Sub CargaDatosAnticipos()
        Try

            CG.AnticipoDevuelvePorLinea(True, GV_LineaCredito, id_ldc.Value, id_ldc.Value, 0, 999)
            If GV_LineaCredito.Rows.Count < 1 Then
                Msj.Mensaje(Me, Caption, "No existen condiciones de línea", TipoDeMensaje._Informacion, , False)
                Caso = 3
            End If

            'Me.GV_LineaCredito.DataSource = Coll_APC
            'Me.GV_LineaCredito.DataBind()


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub CargaDatosGral()
        Try

            Dim Interes As ArrayList
            Dim Opo460 As ArrayList
            'Dim GRAL As New rsc_cls
            Dim GRAL As New Object
            Dim Visitas As vst_cls
            Dim UltOpo As opo_cls
            Dim RSD As Object
            Dim Var As New FuncionesGenerales.FComunes
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim Mto_Ocupado As Double


            Interes = CMC.InteresesCalculadosDevuelve(CLng(Txt_Rut_Cli.Text), Sesion.CodEje, Date.Now)
            GRAL = CMC.ResumenClienteDevuelve(CLng(Txt_Rut_Cli.Text), Sesion.CodEje)
            Visitas = CMC.VisitasDevuelve(CLng(Txt_Rut_Cli.Text))
            UltOpo = OP.OperacionUltimaDevuelve(CLng(Txt_Rut_Cli.Text))
            RSD = CMC.ResumenClienteDeudorDevuelve(CLng(Txt_Rut_Cli.Text), CLng(Txt_Rut_Cli.Text))
            Opo460 = CMC.OperacionConProblemasCobranzaDevuelve(CLng(Txt_Rut_Cli.Text))

            If IsNothing(GRAL) Then
                Msj.Mensaje(Page, Caption, "Cliente no posee historial", TipoDeMensaje._Informacion, , False)
                Caso = 4
                'Exit Sub
                Me.Txt_ProDiaPag.Text = 0
                Mto_Ocupado = 0
                Me.Txt_Deu_Cli.Text = 0
                Me.Txt_Nro_Deu.Text = 0
            Else
                Session("Gral") = GRAL
                Me.Txt_ProDiaPag.Text = GRAL.rsc_prm_dia_pag
                Mto_Ocupado = GRAL.rsc_mto_ocu
                Me.Txt_Deu_Cli.Text = Format(GRAL.rsc_mto_ocu, FMT.FCMSD)
                Me.Txt_Nro_Deu.Text = GRAL.rsc_ddr_ctd
            End If

            'Datos Comerciales
            If Not IsNothing(Visitas) Then
                Me.Txt_VisitasCli.Text = Visitas.vst_des_lar
            End If

            If Not IsNothing(UltOpo) Then
                Me.Txt_FecUltOpe.Text = FUNFecReg(UltOpo.ope_cls.ope_fec_sim)
            End If


            Dim rsd_mto_ocu As Double
            Dim suma_deu_cli_deu As Double

            If Not IsNothing(RSD) Then

                For Each R In RSD
                    rsd_mto_ocu += R.rsd_mto_ocu
                Next

                Me.Txt_Deu_Deu.Text = Var.FormatoMiles(rsd_mto_ocu)

            Else

                Me.Txt_Deu_Deu.Text = 0
                rsd_mto_ocu = 0

            End If

            'Deuda Consolidada
            suma_deu_cli_deu = Mto_Ocupado + rsd_mto_ocu

            Me.Txt_Tot_Deu.Text = Format(suma_deu_cli_deu, FMT.FCMSD)

            Dim Formato As String = ""
            Select Case DP_TipoMoneda.SelectedValue
                Case 1 : Formato = FMT.FCMSD
                Case 2 : Formato = FMT.FCMCD4
                Case 3, 4 : Formato = FMT.FCMCD
            End Select


            'Resumen Deuda Con Problemas
            If IsNothing(Opo460) Then
                Me.Txt_Mto_Pro.Text = 0
                Me.Txt_Doc_Pro.Text = 0
                Me.Txt_Deu_Pro.Text = 0
            Else
                Me.Txt_Mto_Pro.Text = Format(Opo460.Item(0), Formato)
                Me.Txt_Doc_Pro.Text = Format(Opo460.Item(1), Formato)
                Me.Txt_Deu_Pro.Text = Format(Opo460.Item(2), Formato)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Private Sub BloqueaTextosClientes()
        Me.Txt_Rut_Cli.ReadOnly = True
        Me.Txt_Dig_Cli.ReadOnly = True

        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"

        'Me.Txt_Rut_Deu.ReadOnly = True
        'Me.Txt_Rut_Deu.CssClass = "clsDisabled"
        'Me.Txt_Rut_Deu.Text = ""

        'Me.Txt_Dig_Deu.ReadOnly = True
        'Me.Txt_Dig_Deu.CssClass = "clsDisabled"
        'Me.Txt_Dig_Deu.Text = ""

        Me.Txt_Mto_Doc.ReadOnly = True
        Me.Txt_Mto_Doc.CssClass = "clsDisabled"
        Me.Txt_Mto_Doc.Text = ""

        Me.Txt_Por_Ant.ReadOnly = True
        Me.Txt_Por_Ant.CssClass = "clsDisabled"
        Me.Txt_Por_Ant.Text = ""

    End Sub

    Private Sub BloqueaTextosMontos()
        Me.Txt_Mto_Doc.ReadOnly = True
        Me.Txt_Por_Ant.ReadOnly = True
        Me.Txt_Mto_Doc.CssClass = "clsDisabled"
        Me.Txt_Por_Ant.CssClass = "clsDisabled"
    End Sub

    Private Sub HabilitaTextosMontos()
        Me.Txt_Mto_Doc.ReadOnly = False
        Me.Txt_Por_Ant.ReadOnly = False

        Me.Txt_Mto_Doc.CssClass = "clsMandatorio"
        Me.Txt_Por_Ant.CssClass = "clsMandatorio"
    End Sub

    Private Sub HabilitarTxtDcto()

        Try

      
            If Session("ACCION_COMERCIAL") = "NUEVO" Then

                'If Me.GV_Deudores.Rows.Count < 1 Then

                DP_TipoMoneda.ClearSelection()
                Me.DP_TipoMoneda.Enabled = True
                Me.DP_TipoMoneda.CssClass = "clsMandatorio"
                Me.Txt_Por_Ant.ReadOnly = False
                Me.Txt_Por_Ant.CssClass = "clsMandatorio"
                Me.Txt_Por_Ant.Text = ""
                Me.Txt_Mto_Doc.ReadOnly = False
                Me.Txt_Mto_Doc.CssClass = "clsMandatorio"
                Me.Txt_Mto_Doc.Text = ""
                Me.Txt_Mto_Eva.ReadOnly = True
                Me.Txt_Mto_Eva.CssClass = "clsDisabled"
                Me.Txt_Mto_Eva.Text = ""
                Me.Txt_Por_Ant.Focus()
                'End If
            Else
                Me.DP_TipoMoneda.Enabled = False
                Me.DP_TipoMoneda.CssClass = "clsDisabled"
                Me.Txt_Por_Ant.ReadOnly = True
                Me.Txt_Por_Ant.CssClass = "clsDisabled"
                Me.Txt_Mto_Doc.ReadOnly = False
                Me.Txt_Mto_Doc.CssClass = "clsMandatorio"
                Me.Txt_Mto_Doc.Text = ""
                Me.Txt_Mto_Eva.ReadOnly = True
                Me.Txt_Mto_Eva.CssClass = "clsDisabled"
                Me.Txt_Mto_Eva.Text = ""
                'Me.Txt_Rut_Deu.Focus()

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub LimpiaDatosClientes()
        'Clientes
        Me.Txt_Rut_Cli.ReadOnly = False
        Me.Txt_Dig_Cli.ReadOnly = False
        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Raz_Soc.Text = ""
        Me.Txt_CatRiesgo.Text = ""
        Me.Txt_Ejecutivo.Text = ""
        Me.Txt_Cartera.Text = ""

        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"

        'Linea de Credito
        Me.Txt_Spread.Text = ""

        Me.Txt_VisitasCli.Text = ""
        Me.Txt_FecUltOpe.Text = ""
        Me.Txt_LineaApro.Text = ""
        Me.Txt_LineaOcu.Text = ""
        Me.Txt_LineaDis.Text = ""
        Me.Txt_FecVctoLin.Text = ""
        Me.Txt_ProDiaPag.Text = ""

        'Deuda Consolidada
        Me.Txt_Deu_Cli.Text = ""
        Me.Txt_Deu_Deu.Text = ""
        Me.Txt_Tot_Deu.Text = ""
        Me.Txt_Nro_Deu.Text = ""
        'ResumenDeuda Con Problemas
        Me.Txt_Mto_Pro.Text = ""
        Me.Txt_Doc_Pro.Text = ""
        Me.Txt_Deu_Pro.Text = ""

        ' BorraCollection(Coll_APC)
        ' Me.GV_LineaCredito.DataSource = Coll_APC
        Me.GV_LineaCredito.DataBind()

        Txt_Rut_Cli.Focus()

    End Sub

    Private Sub LimpiaDatosGenerales()
        Me.DP_TipoMoneda.ClearSelection()
        Me.DP_TipoMoneda.Enabled = True
        Me.DP_TipoMoneda.CssClass = "clsDisabled"

        Me.Txt_Por_Ant.ReadOnly = True
        Me.Txt_Mto_Doc.ReadOnly = True
        Me.Txt_Por_Ant.CssClass = "clsDisabled"
        Me.Txt_Mto_Doc.CssClass = "clsDisabled"

        Me.Txt_Por_Ant.Text = ""
        Me.Txt_Mto_Doc.Text = ""
        'Me.Txt_Por_Ant.Text = comasXptos("0,0")

        'BorraCollection(Coll_DEU)
        'BorraCollection(Coll_RSD)

        'Me.GV_Deudores.DataSource = Coll_RSD
        Me.GV_Deudores.DataBind()

    End Sub

    Private Sub LimpiaDeudor()

        'Me.Txt_Rut_Deu.Text = ""
        'Me.Txt_Dig_Deu.Text = ""
        'Me.Txt_Rso_Deu.Text = ""

        'Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        'Me.Txt_Dig_Deu.CssClass = "clsMandatorio"

        'Me.Txt_Rut_Deu.ReadOnly = False
        'Me.Txt_Dig_Deu.ReadOnly = False

        'Txt_Rut_Deu.Focus()

    End Sub

    Private Sub LimpiaTotales()
        Me.Txt_Tot_Eva.Text = ""
        Me.Txt_Tot_Doc.Text = ""
        Me.Txt_Tot_Deu.Text = ""
    End Sub

    Sub BloqueaTextosDeudores()

        'Txt_Rut_Deu.ReadOnly = True
        'Txt_Dig_Deu.ReadOnly = True


        'Txt_Rut_Deu.CssClass = "clsDisabled"
        'Txt_Dig_Deu.CssClass = "clsDisabled"

    End Sub

    Function EXISTE_DEUDOR_EVA() As Boolean
        Try

            'Dim POS As Integer
            'Dim i As Integer

            'EXISTE_DEUDOR_EVA = False

            'For i = 0 To GV_Deudores.Rows.Count - 1

            '    If GV_Deudores.Rows(i).Cells(0).Text = "" Then Exit For

            '    POS = InStr(Replace(GV_Deudores.Rows(i).Cells(0).Text, ",", ""), "-")
            '    Dim Rut As String = Trim(Mid(Replace(GV_Deudores.Rows(i).Cells(0).Text, ",", ""), 1, POS - 1))

            '    'verifica si el deudor ya esta ingresado
            '    If Trim(Me.Txt_Rut_Deu.Text) = Replace(Rut, ".", "") Then
            '        Session("ACCION_COMERCIAL") = "MODIFICA"
            '        EXISTE_DEUDOR_EVA = True
            '        Exit For
            '    End If

            'Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Function

    Private Function ValidaAgregarDeu() As Boolean
        Try

        
            If Me.DP_TipoMoneda.SelectedIndex = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione tipo de documento", TipoDeMensaje._Exclamacion, , False)
                Return True
            End If

            Select Case ""
                Case Me.Txt_Por_Ant.Text
                    Msj.Mensaje(Page, Caption, "Ingrese portcentaje de anticipo", TipoDeMensaje._Exclamacion, , False)
                    Return True
                Case Me.Txt_Mto_Doc.Text
                    Msj.Mensaje(Page, Caption, "Ingrese monto del documento", TipoDeMensaje._Exclamacion, , False)
                    Return True
                Case Me.Txt_Mto_Eva.Text
                    Msj.Mensaje(Page, Caption, "Ingrese monto evaluación", TipoDeMensaje._Exclamacion, , False)
                    Return True
            End Select

            If Txt_Por_Ant.Text = 0 Or Txt_Por_Ant.Text = "0.0" Then
                Msj.Mensaje(Page, Caption, "Seleccione tipo de documento", TipoDeMensaje._Exclamacion, , False)
                Return True
            End If

            If Txt_Mto_Doc.Text = 0 Or Txt_Mto_Doc.Text = "0.0" Then
                Msj.Mensaje(Page, Caption, "Ingrese monto del documento", TipoDeMensaje._Exclamacion, , False)
                Return True
            End If

            Return False

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Function

    Private Sub CargaArchivo(ByVal ruta As String)
        Try

            Dim j As Integer
            Dim txtRuta As String
            Dim linea As String
            Dim nro As String
            Dim I As Int16
            Dim Arc As Object

            txtRuta = ruta

            Dim sr As IO.StreamReader = New IO.StreamReader(txtRuta)

            j = 0

            While sr.Peek() >= 0

                Arc = New Object

                With Arc

                    linea = sr.ReadLine()

                    nro = linea

                    Arc.num_docto = nro.Substring(0, nro.LastIndexOf(";"))

                    j = j + 1

                    I = nro.LastIndexOf(";") + 1

                    Arc.cuo_num = nro.Substring(I, nro.Length - I - 1)

                End With

                'Coll_CLI.Add(Arc)

            End While

            'Me.GV_Deudores.DataSource = Coll_CLI
            'Me.GV_Deudores.DataBind()

            sr.Close()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub AlineaMontosDerecha()

        'Condiciones Comerciales
        Txt_Spread.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_LineaApro.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_LineaOcu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_LineaDis.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Deuda Consolidada
        Txt_Deu_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Deu_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Deuda Consolidada
        Txt_Mto_Pro.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Evaluacion
        Txt_Por_Ant.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Eva.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Tot_Eva.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Deu_Tot.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Doc_Pro.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Deu_Pro.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Nro_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

    End Sub

    Private Sub CargaEvaluacion()

        Try

            Dim mto_tot_deu_cli As Double
            Dim suma_mto_eval As Double
            Dim Mto_Tot_Doc As Double
            Dim HTodoLoPaga As Double
            Dim HPagFactoring As Double
            Dim Coll_EXD As Collection
            Dim Coll_RSD As New Collection
            Dim Eva As eva_cls
            Dim Formato As String

            HF_NroEva.Value = Request.QueryString("id")

            Eva = CMC.EvaluacionDevuelvePorId(HF_NroEva.Value)

            If IsNothing(Eva) Then
                Msj.Mensaje(Page, Caption, "No se puede cargar la evaluación", TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            'Txt_Rut_Cli.Text = Val(Eva.cli_idc)
            'Txt_Dig_Cli.Text = RG.Vrut(Txt_Rut_Cli.Text)
            Txt_Por_Ant.Text = Eva.eva_por
            DP_TipoMoneda.SelectedValue = Eva.id_P_0023


            Select Case DP_TipoMoneda.SelectedValue
                Case 1 : Formato = FMT.FCMSD
                Case 2 : Formato = FMT.FCMCD4
                Case 3, 4 : Formato = FMT.FCMCD
            End Select

            'CargaDatosCliente()
            'CargaDatosLineaCredito()
            'CargaDatosAnticipos()
            'CargaDatosGral()

            'BloqueaTextosClientes()

            Coll_EXD = CMC.EvaluacionDevuelveDeudoresPorIdEva(HF_NroEva.Value)

            'recorremos la collection para realizar los calculos
            For I = 1 To Coll_EXD.Count

                Dim EXD As New EvaDeu

                EXD.RutDeu = CStr(Format(CLng(Coll_EXD.Item(I).RutDeu), FMT.FCMSD)) & "-" & CStr(RG.Vrut(CLng(Coll_EXD.Item(I).RutDeu)))
                EXD.NomDeu = Coll_EXD.Item(I).NomDeu
                EXD.MtoEva = Coll_EXD.Item(I).MtoEva
                EXD.MtoDoc = Coll_EXD.Item(I).MtoDoc
                EXD.Moneda = Coll_EXD.Item(I).Moneda

                

                'trae deuda del deudor
                Dim SumatoriaDeuda As ArrayList

                SumatoriaDeuda = CMC.DeudorDeudaDevuelve(Txt_Rut_Cli.Text, CInt(Coll_EXD.Item(I).RutDeu))

                If SumatoriaDeuda.Count > 0 Then
                    HTodoLoPaga = SumatoriaDeuda.Item(0)
                    HPagFactoring = SumatoriaDeuda.Item(1)
                Else
                    HTodoLoPaga = 0
                    HPagFactoring = 0
                End If

                EXD.DeuAct = HTodoLoPaga
                EXD.DeuFac = HPagFactoring
                EXD.DeuTot = EXD.MtoEva + SumatoriaDeuda.Item(0)

                suma_mto_eval = suma_mto_eval + EXD.MtoEva
                Mto_Tot_Doc = Mto_Tot_Doc + EXD.MtoDoc

                EXD.PorCli = (EXD.DeuTot / (mto_tot_deu_cli + suma_mto_eval)) * 100
                EXD.PorCli = Coll_EXD.Item(I).PorCli

                EXD.MtoSbl = CG.SubLineasDevuelvePorDeudor(RG.LimpiaRut(EXD.RutDeu))
                EXD.Cupo = CG.Devuelvelineaglobaldeudor(RG.LimpiaRut(EXD.RutDeu), _
                                                        DP_TipoMoneda.SelectedValue)
                EXD.Disponible = EXD.Cupo - HTodoLoPaga

                Coll_RSD.Add(EXD)

            Next

            'Me.Txt_Tot_Eva.Text = Format(suma_mto_eval, FMT.FCMCD)
            'Me.Txt_Tot_Doc.Text = Format(Mto_Tot_Doc, FMT.FCMCD)
            'Me.Txt_Deu_Tot.Text = Format(suma_mto_eval, FMT.FCMCD)


            Me.Txt_Tot_Eva.Text = Format(suma_mto_eval, Formato)
            Me.Txt_Tot_Doc.Text = Format(Mto_Tot_Doc, Formato)
            Me.Txt_Deu_Tot.Text = Format(suma_mto_eval, Formato)



            Me.GV_Deudores.DataSource = Coll_RSD
            Me.GV_Deudores.DataBind()

            For I = 0 To GV_Deudores.Rows.Count - 1
                GV_Deudores.Rows(I).Cells(4).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(4).Text), Formato)
                GV_Deudores.Rows(I).Cells(5).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(5).Text), Formato)

                GV_Deudores.Rows(I).Cells(9).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(9).Text), Formato)
                GV_Deudores.Rows(I).Cells(10).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(10).Text), Formato)
                GV_Deudores.Rows(I).Cells(11).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(11).Text), Formato)

            Next

            If IsNothing(Session("Coll_RSD")) Then
                Session("Coll_RSD") = Coll_RSD
            End If


            IB_Informe.Visible = True

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Private Sub CalculaMontoEvaluacion()
        Try


            Dim Porcentaje As Decimal
            Dim Monto As Double
            Dim MtoEva As Double

            Porcentaje = Txt_Por_Ant.Text

            If Txt_Mto_Doc.Text <> "" Then
                Monto = Txt_Mto_Doc.Text
            End If

            MtoEva = (Monto * (Porcentaje / 100))

            Select Case DP_TipoMoneda.SelectedValue
                Case 1
                    Txt_Mto_Eva.Text = Format(MtoEva, FMT.FCMSD)
                Case 2
                    Txt_Mto_Eva.Text = Format(MtoEva, FMT.FCMCD4)
                Case 3, 4
                    Txt_Mto_Eva.Text = Format(MtoEva, FMT.FCMCD)
            End Select

            'por = por.replace(',','.');
            'eva = (mto * (por / 100));

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class

Public Class EvaDeu

    Private _CORR_EVALUACION As Integer
    Public Property CORR_EVALUACION()
        Get
            Return _CORR_EVALUACION
        End Get
        Set(ByVal value)
            _CORR_EVALUACION = value
        End Set
    End Property

    Private _RutDeu As String
    Public Property RutDeu()
        Get
            Return _RutDeu
        End Get
        Set(ByVal value)
            _RutDeu = value
        End Set
    End Property

    Private _NomDeu As String
    Public Property NomDeu()
        Get
            Return _NomDeu
        End Get
        Set(ByVal value)
            _NomDeu = value
        End Set
    End Property

    Private _HTodoLoPaga As String
    Public Property HTodoLoPaga() As String
        Get
            Return _HTodoLoPaga
        End Get
        Set(ByVal value As String)
            _HTodoLoPaga = value
        End Set
    End Property


    Private _HPagFactoring As String
    Public Property HPagFactoring() As String
        Get
            Return _HPagFactoring
        End Get
        Set(ByVal value As String)
            _HPagFactoring = value
        End Set
    End Property

    Private _DeuAct As Double
    Public Property DeuAct() As Double
        Get
            Return _DeuAct
        End Get
        Set(ByVal value As Double)
            _DeuAct = value
        End Set
    End Property

    Private _DeuFac As Double
    Public Property DeuFac() As Double
        Get
            Return _DeuFac
        End Get
        Set(ByVal value As Double)
            _DeuFac = value
        End Set
    End Property

    Private _MtoEva As Double
    Public Property MtoEva() As Double
        Get
            Return _MtoEva
        End Get
        Set(ByVal value As Double)
            _MtoEva = value
        End Set
    End Property

    Private _MtoDoc As Double
    Public Property MtoDoc() As Double
        Get
            Return _MtoDoc
        End Get
        Set(ByVal value As Double)
            _MtoDoc = value
        End Set
    End Property

    Private _DeuTot As Double
    Public Property DeuTot() As Double
        Get
            Return _DeuTot
        End Get
        Set(ByVal value As Double)
            _DeuTot = value
        End Set
    End Property

    Private _PorCli As Decimal
    Public Property PorCli() As Decimal
        Get
            Return _PorCli
        End Get
        Set(ByVal value As Decimal)
            _PorCli = value
        End Set
    End Property

    Private _PorAnt As Decimal
    Public Property PorAnt() As Decimal
        Get
            Return _PorAnt
        End Get
        Set(ByVal value As Decimal)
            _PorAnt = value
        End Set
    End Property

    Private _TipoMoneda As Int16
    Public Property TipoMoneda() As Int16
        Get
            Return _TipoMoneda
        End Get
        Set(ByVal value As Int16)
            _TipoMoneda = value
        End Set
    End Property

    Private _Moneda As String
    Public Property Moneda() As String
        Get
            Return _Moneda
        End Get
        Set(ByVal value As String)
            _Moneda = value
        End Set
    End Property

    Private _MtoSbl As Double
    Public Property MtoSbl() As Double
        Get
            Return _MtoSbl
        End Get
        Set(ByVal value As Double)
            _MtoSbl = value
        End Set
    End Property

    Private _Cupo As Double
    Public Property Cupo() As Double
        Get
            Return _Cupo
        End Get
        Set(ByVal value As Double)
            _Cupo = value
        End Set
    End Property

    Private _Disponible As Double
    Public Property Disponible() As Double
        Get
            Return _Disponible
        End Get
        Set(ByVal value As Double)
            _Disponible = value
        End Set
    End Property

End Class