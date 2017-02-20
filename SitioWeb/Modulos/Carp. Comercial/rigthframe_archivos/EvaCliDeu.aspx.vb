Imports Microsoft.Reporting.WebForms
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.IO
Imports System.Data

Partial Class ClsEvaCliDeu
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Coll_RSD As Collection
    Dim Caption As String = "Evaluación Cliente/Pagadores"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Caso As Integer
    Dim Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim CMC As New ClaseComercial
    Dim OP As New ClaseOperaciones
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


        If Not IsPostBack Then

            Response.Expires = -1
            NroPaginacion_Deu = 0
            'Session("Coll_RSD") = New Collection
            AlineaMontosDerecha()
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_TipoMoneda)
            'Coll_RSD = Session("Coll_RSD")
            Try

                If Not CargaDatosCliente() Then Exit Sub

                CargaDatosLineaCredito()

                CargaDatosGral()
                BloqueaTextosClientes()
                Session("ACCION_COMERCIAL") = "NUEVO"



            Catch ex As Exception
                Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            End Try

            If Request.QueryString("id") <> "" Then
                'Msj.Mensaje(Me, Caption, "Hay que buscar la eva y cargarla", TipoDeMensaje._Informacion)
                HF_Accion.Value = "MODIFICA"
                'IB_Guardar_ConfirmButtonExtender.Enabled = False
                CargaEvaluacion()
            Else
                HF_Accion.Value = "NUEVO"
                'IB_Guardar_ConfirmButtonExtender.Enabled = True
                Txt_Rut_Deu.Focus()
            End If

            IB_AyudaDeu.Attributes.Add("onClick", "javascript:WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',580,410,200,150);__doPostBack('ctl00$ContentPlaceHolder1$LB_BuscaDeudor', '');")

        End If


    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            If Not CargaDatosCliente() Then Exit Sub
            CargaDatosLineaCredito()
            CargaDatosAnticipos()
            CargaDatosGral()
            BloqueaTextosClientes()
            Txt_Rut_Deu.Focus()
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_BuscaDeudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDeudor.Click

        Try

            If Txt_Rut_Deu.Text.Trim = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar un Pagador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If UCase(Txt_Dig_Deu.Text) <> UCase(RG.Vrut(Txt_Rut_Deu.Text)) Then
                Msj.Mensaje(Me, Caption, "Nit Incorrecto del pagador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Deu As deu_cls

            Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

            If Not IsNothing(Deu) Then

                BloqueaTextosDeudores()
                HabilitarTxtDcto()
                If Deu.id_P_0044 = 1 Then
                    'Txt_Rso_Deu.Text = Deu.deu_nom.Trim & " " & Deu.deu_ape_ptn.Trim & "" & Deu.deu_ape_mtn.Trim
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim & " " & Deu.deu_ape_ptn.Trim & "" & Deu.deu_ape_mtn.Trim
                Else
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim
                End If
                'Txt_Mto_Doc_MaskedEditExtender.Enabled = True
            Else
                Msj.Mensaje(Me, Caption, "No Existe pagador", TipoDeMensaje._Exclamacion)
                Txt_Rso_Deu.CssClass = "clsMandatorio"
                Txt_Rso_Deu.ReadOnly = False
            End If

            EXISTE_DEUDOR_EVA()

            'If Not CG.RelacionClienteDeudorDevuelve(Txt_Rut_Deu.Text.Replace(".", ""), "A", Txt_Rut_Cli.Text.Replace(".", "")) Then
            '    Msj.Mensaje(Me,Caption, "Este Deudor no tiene relación con este cliente, ¿Desea agregarlo?", TipoDeMensaje._Confirmacion)
            'End If

            Txt_Por_Ant.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Deudores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Deudores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Deudores, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Deudores, 'formatable')")
            'e.Row.Attributes.Add("onClick", "DetalleDeuComercial(ctl00_ContentPlaceHolder1_GV_Deudores, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If Not CargaDatosCliente() Then Exit Sub

            CargaDatosLineaCredito()
            CargaDatosAnticipos()
            CargaDatosGral()
            BloqueaTextosClientes()

            Me.Txt_Rut_Deu.Focus()

            Session("ACCION_COMERCIAL") = "NUEVO"

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_NoExisteDeu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            HabilitarTxtDcto()
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_RelCliDeu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'falta validar relacion
        Try
            HabilitarTxtDcto()
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_TipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoMoneda.SelectedIndexChanged
        Try

            CambiaMascaraMonto()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Mto_Doc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Mto_Doc.TextChanged
        Try
            If Me.Txt_Por_Ant.Text = "__,__" Or Me.Txt_Por_Ant.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar el % de Anticipo", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            CalculaMontoEvaluacion()
            IB_AgregarDeu.Enabled = True
            IB_AgregarDeu.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        Try

            If Txt_Rso_Deu.Text <> "" Then
                Exit Sub
            End If

            If Txt_Rut_Deu.Text.Trim = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar un Pagador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Deu As deu_cls

            Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

            If Not IsNothing(Deu) Then

                'If Txt_Dig_Deu.Text.ToUpper <> RG.Vrut(CLng(Deu.deu_ide)) Then
                '    Msj.Mensaje(Me, Caption, "Digito incorrecto", TipoDeMensaje._Exclamacion)
                '    Txt_Rut_Deu.Focus()
                '    Exit Sub
                'End If

                BloqueaTextosDeudores()
                HabilitarTxtDcto()

                'Si no ha seleccionado nada los habilita (solo una vez)
                If Txt_Por_Ant.Text = "" And DP_TipoMoneda.SelectedValue = 0 Then
                    Me.DP_TipoMoneda.Enabled = True
                    Me.DP_TipoMoneda.CssClass = "clsMandatorio"

                    Me.Txt_Por_Ant.ReadOnly = False
                    Me.Txt_Por_Ant.CssClass = "clsMandatorio"

                End If

                If Deu.id_P_0044 = 1 Then
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim & " " & Deu.deu_ape_ptn.Trim & " " & Deu.deu_ape_mtn.Trim
                Else
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim
                End If
                Txt_Por_Ant.Focus()
            Else
                'If Txt_Dig_Deu.Text.ToUpper <> RG.Vrut(Txt_Rut_Deu.Text.Replace(".", "")) Then
                '    Msj.Mensaje(Me, Caption, "Digito incorrecto", TipoDeMensaje._Exclamacion)
                '    Txt_Rut_Deu.Focus()
                '    Exit Sub
                'End If
                Msj.Mensaje(Me, Caption, "No Existe pagador", TipoDeMensaje._Exclamacion)
                BloqueaTextosDeudores()
                HabilitarTxtDcto()

                'Txt_Rso_Deu.CssClass = "clsMandatorio"
                'Txt_Rso_Deu.ReadOnly = False
                Txt_Rso_Deu.Focus()
                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""

                Txt_Rut_Deu.ReadOnly = False
                Txt_Dig_Deu.ReadOnly = False
                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.CssClass = "clsMandatorio"

                Exit Sub

            End If

            EXISTE_DEUDOR_EVA()

            IB_AyudaDeu.Enabled = False

            'If Not CG.RelacionClienteDeudorDevuelve(Txt_Rut_Deu.Text.Replace(".", ""), "A", Txt_Rut_Cli.Text.Replace(".", "")) Then
            '    Msj.Mensaje(Me, Caption, "Este Deudor no tiene relación con este cliente, ¿Desea agregarlo?", TipoDeMensaje._Confirmacion)
            'End If

            If Txt_Por_Ant.ReadOnly And Not DP_TipoMoneda.Enabled Then
                CambiaMascaraMonto()
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_Deudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Deudor.Click
        Try

            AgregarDeudor()
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Por_Ant_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Por_Ant.TextChanged
        Try
            'Cuando se modifique % de anticipo se borre monto de documento
            DP_TipoMoneda.Focus()
            Txt_Mto_Doc.Text = ""
            Txt_Mto_Eva.Text = ""


            If Txt_Por_Ant.Text > 100 Then
                Msj.Mensaje(Page, Caption, "Porcentaje de anticipo no puede ser mayor a 100", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Por_Ant.Text = ""
            End If
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Txt_Rut_Deu.ReadOnly = True
        Txt_Dig_Deu.ReadOnly = True

        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Dig_Deu.CssClass = "clsDisabled"

        Txt_Mto_Doc.ReadOnly = False
        Txt_Mto_Doc.CssClass = "clsMandatorio"

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim Coll_RSD As New Collection
        Dim Eva As EvaDeu

        Coll_RSD = Session("Coll_RSD")

        For i = 1 To Coll_RSD.Count

            Eva = Coll_RSD.Item(i)

            If Eva.RutDeu = btn.ToolTip Then

                Txt_Rut_Deu.Text = RG.LimpiaRut(Eva.RutDeu)
                Txt_Dig_Deu.Text = RG.Vrut(Txt_Rut_Deu.Text)
                Txt_Rso_Deu.Text = Eva.NomDeu
                Txt_Por_Ant.Text = Eva.PorAnt

                DP_TipoMoneda.ClearSelection()
                DP_TipoMoneda.Items.FindByValue(Eva.TipoMoneda).Selected = True

                CambiaMascaraMonto()

                Dim Formato As String = ""

                Select Case DP_TipoMoneda.SelectedValue
                    Case 1 : Formato = FMT.FCMSD
                    Case 2 : Formato = FMT.FCMCD4
                    Case 3, 4 : Formato = FMT.FCMCD
                End Select

                Txt_Mto_Eva.Text = Format(Eva.MtoEva, Formato)
                Txt_Mto_Doc.Text = Eva.MtoDoc
                HF_Pos.Value = i
                Exit For

            End If

        Next

        'marca grilla seleccionada
        For I = 0 To GV_Deudores.Rows.Count - 1
            If Me.GV_Deudores.Rows(I).Cells(0).Text = btn.ToolTip Then
                If (I Mod 2) = 0 Then
                    GV_Deudores.Rows(I).CssClass = "selectable"
                Else
                    GV_Deudores.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    GV_Deudores.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Deudores.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_AgregarDeu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_AgregarDeu.Click
        Try

            If HF_Accion.Value = "MODIFICA" Then
                If HF_Est.Value = 2 Or HF_Est.Value = 3 Then
                    Msj.Mensaje(Page, Caption, "No se puede modificar por encontrarse con estado " & HF_EstDes.Value, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If Not ValidaAgregarDeu() Then
                Comentarios_Eva()
                AgregarDeudor()
                IB_AyudaDeu.Enabled = True
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_QuitarDeu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_QuitarDeu.Click
        Try

            If HF_Accion.Value = "MODIFICA" Then
                If HF_Est.Value = 2 Or HF_Est.Value = 3 Then
                    Msj.Mensaje(Page, Caption, "No se puede modificar por encontrarse con estado " & HF_EstDes.Value, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If HF_Pos.Value <> "" Then
                Msj.Mensaje(Me, Caption, "¿ Seguro de eliminar al pagador ?", TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Guardar.Click
        Try

            'Validacion para evaluacion que esten asociadas a una negociacion
            If Val(HF_Est.Value) = 2 Or Val(HF_Est.Value) = 3 Then
                Msj.Mensaje(Page, Caption, "No se puede modificar por encontrarse con estado " & HF_EstDes.Value, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not agt.ValidaAccesso(20, 20050304, Usr, "PRESIONO GUARDAR EVALUACION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If GV_Deudores.Rows.Count <= 0 Then
                Msj.Mensaje(Me, Caption, "No tiene pagador para evaluar", TipoDeMensaje._Exclamacion)
            Else
                'If HF_Accion.Value = "NUEVO" Then
                Dim x As New System.EventArgs
                LB_Guardar_Click(Me, x)
                '    Exit Sub
                'End If
                ''Msj.Mensaje(Me, Caption, "¿ Seguro de guardar los datos ?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)
            End If

            'Response.Redirect("Evaluacion.aspx", False)
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click
        Try


            'If HF_NroEva.Value <> "" Then
            If txt_HFEva.Text <> "" Then
                'NroEvaluacion = HF_NroEva.Value
                NroEvaluacion = txt_HFEva.Text

                Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(NroEvaluacion)

                If abytFileData.Length <> 0 Then
                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                    Response.AddHeader("cache-control", "private")
                    Response.AddHeader("Expires", "0")
                    Response.AddHeader("Pragma", "cache")
                    Response.AddHeader("content-disposition", "attachment; filename=Eva" & Txt_Rut_Cli.Text & "_" & NroEvaluacion & ".pdf")
                    Response.AddHeader("Accept-Ranges", "none")
                    Response.BinaryWrite(abytFileData)
                    Response.Flush()
                    Response.End()
                Else
                    RW.AbrePopup(Me, 2, "Reporte_EvaluacionCliDeu.aspx?id=" & NroEvaluacion & "&Moneda=" & DP_TipoMoneda.SelectedValue & "&Porcentaje=" & Txt_Por_Ant.Text & "", "RepEvaCliDeu", 1280, 1024, 0, 0)
                End If

            End If


            'RW.AbrePopup(Me, 2, "Reporte_EvaluacionCliDeu.aspx?id=" & HF_NroEva.Value & "&Moneda=" & DP_TipoMoneda.SelectedValue & "&Porcentaje=" & Txt_Por_Ant.Text & "", "RepEvaCliDeu", 1280, 1024, 0, 0)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Me.LimpiaDatosGenerales()
            Me.LimpiaDatosClientes()
            LimpiaDeudor()
            Me.LimpiaTotales()
            Coll_RSD = New Collection
            Session("Coll_RSD") = Coll_RSD
            Response.Redirect("Evaluacion.aspx")

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles LB_Guardar.Click
        Try

            Dim Var As New FuncionesGenerales.Variables
            Dim Eva As New eva_cls
            Dim Accion As Boolean = False
            Eva.eva_fec_cre = Date.Now

            'Comentarios_Eva()

            If HF_Accion.Value = "NUEVO" Then
                'Using ts As New TransactionScope
                Eva.id_eva = Nothing
                Eva.cli_idc = Format(CLng(Replace(Txt_Rut_Cli.Text.Trim, ".", "")), Var.FMT_RUT)
                'Eva.id_eje = Sesion.CodEje
                Eva.id_eje = ClsSession.ClsSession.CodEje
                Eva.eva_por = Txt_Por_Ant.Text
                Eva.id_P_0023 = DP_TipoMoneda.SelectedValue
                Eva.id_P_0110 = 1
                Eva.eva_obs = Txt_Obs.Text

                Eva.id_eva = CMC.EvaluacionInsertar(Eva)
                HF_NroEva.Value = Eva.id_eva
                txt_HFEva.Text = HF_NroEva.Value

                If Eva.id_eva > 0 Then
                    CMC.EvaluacionDeudorElimina(Eva.id_eva)
                    For I = 0 To GV_Deudores.Rows.Count - 1
                        Dim exd As New exd_cls
                        exd.id_eva = Eva.id_eva
                        Dim CLSCLI As New ClaseClientes

                        If CLSCLI.RelacionClienteDeudorDevuelve(Format(CLng(RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text)), Var.FMT_RUT), "A", Eva.cli_idc) Then
                            exd.deu_ide = Format(CLng(RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text)), Var.FMT_RUT)
                        Else
                            exd.deu_ide = Nothing
                        End If


                        exd.deu_rut = Format(CLng(RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text)), Var.FMT_RUT)
                        exd.deu_nom = GV_Deudores.Rows(I).Cells(1).Text.Trim
                        exd.mto_eva = GV_Deudores.Rows(I).Cells(4).Text
                        exd.mto_eva_tot = GV_Deudores.Rows(I).Cells(5).Text
                        exd.exd_por_cli = GV_Deudores.Rows(I).Cells(7).Text

                        If CMC.EvaluacionDeudorInsertar(exd) Then
                            Accion = True
                        Else
                            Accion = False
                            Exit For
                        End If

                    Next

                    If Accion Then
                        'ts.Complete()
                        Msj.Mensaje(Me, Caption, "Evaluación guardada", TipoDeMensaje._Informacion)
                        'LimpiaDatosClientes()
                        'LimpiaDatosGenerales()
                        'LimpiaDeudor()
                        'LimpiaTotales()

                        Coll_RSD = New Collection
                        Session("Coll_RSD") = Coll_RSD

                        'Response.Redirect("Evaluacion.aspx", False)
                        HF_Accion.Value = "MODIFICA"
                        Genera_reporte()

                    Else
                        Msj.Mensaje(Me, Caption, "Evaluación no guardada", TipoDeMensaje._Informacion)
                    End If

                End If
                'End Using

            Else
                'Using ts As New TransactionScope


                Eva.id_eva = HF_NroEva.Value
                Eva.cli_idc = Format(CLng(Replace(Txt_Rut_Cli.Text.Trim, ".", "")), Var.FMT_RUT)
                Eva.id_eje = Sesion.CodEje
                Eva.eva_por = Txt_Por_Ant.Text
                Eva.id_P_0023 = DP_TipoMoneda.SelectedValue
                Eva.eva_obs = Txt_Obs.Text

                If CMC.EvaluacionActualiza(Eva) Then

                    CMC.EvaluacionDeudorElimina(Eva.id_eva)

                    For I = 0 To GV_Deudores.Rows.Count - 1

                        Dim exd As New exd_cls
                        exd.id_eva = Eva.id_eva
                        exd.deu_ide = Format(CLng(RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text)), Var.FMT_RUT)
                        exd.deu_rut = exd.deu_ide
                        exd.deu_nom = GV_Deudores.Rows(I).Cells(1).Text.Trim
                        exd.mto_eva = GV_Deudores.Rows(I).Cells(4).Text
                        exd.mto_eva_tot = GV_Deudores.Rows(I).Cells(5).Text
                        exd.exd_por_cli = GV_Deudores.Rows(I).Cells(7).Text
                        If CMC.EvaluacionDeudorInsertar(exd) Then
                            Accion = True
                        Else
                            Accion = False
                            Exit For
                        End If

                    Next

                    If Accion Then

                        Msj.Mensaje(Me, Caption, "Evaluación guardada", TipoDeMensaje._Informacion)
                        'LimpiaDatosClientes()
                        'LimpiaDatosGenerales()
                        'LimpiaDeudor()
                        'LimpiaTotales()
                        Coll_RSD = New Collection
                        Session("Coll_RSD") = Coll_RSD
                        'Response.Redirect("Evaluacion.aspx", False)

                        Genera_reporte()

                        'ts.Complete()

                    Else
                        Msj.Mensaje(Me, Caption, "Evaluación no guardada", TipoDeMensaje._Informacion)
                    End If

                End If

                ' End Using
            End If

            'Response.Redirect("Evaluacion.aspx", False)
            IB_Guardar.Enabled = False

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub Genera_reporte()

        Try

            'Datos generales del cliente
            Dim Resumen As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_CabeceraDataTable
            Dim Tab1 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_CabeceraTableAdapter

            'grilla deudor
            Dim Deudores As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_DeudoresDataTable
            Dim Tab As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_DeudoresTableAdapter

            'grilla pagare vigentes
            Dim Pagares As New DataSet_ReporteEvaluacion.sp_pagares_vigentesDataTable
            Dim Tab2 As New DataSet_ReporteEvaluacionTableAdapters.sp_pagares_vigentesTableAdapter

            'grilla pagare vigentes
            Dim Concentracion As New DataSet_ReporteEvaluacion.Sp_Concentracion_ClienteDataTable
            Dim Tab3 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Concentracion_ClienteTableAdapter

            'Distribucion deuda Morosa y Vctos. Futuros
            Dim Distribucion As New DataSet_ReporteEvaluacion.sp_distribucion_diasDataTable
            Dim Tab4 As New DataSet_ReporteEvaluacionTableAdapters.sp_distribucion_diasTableAdapter

            'Evolucion Mora 
            Dim evolucion As New DataSet_ReporteEvaluacion.sp_cl_evolucion_deudaDataTable
            Dim tab5 As New DataSet_ReporteEvaluacionTableAdapters.sp_cl_evolucion_deudaTableAdapter

            Dim CLI As cli_cls
            Dim id_eva As Integer
            Dim Moneda As Integer
            Dim Porcentaje As Decimal

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\ReportEvaluacion.rdlc"

            

            If DP_TipoMoneda.SelectedValue <> "" Then
                Moneda = DP_TipoMoneda.SelectedValue
            End If

            If Txt_Por_Ant.Text <> "" Then
                Porcentaje = Txt_Por_Ant.Text
            End If

            If HF_NroEva.Value <> "" Then
                id_eva = HF_NroEva.Value
            End If

            CLI = Session("Cliente")
            'Msj.Mensaje(Me, "", CLI.cli_idc, ClsMensaje.TipoDeMensaje._Informacion)
            Resumen = Tab1.GetData(id_eva)

            Dim dt As DataTable

            dt = Resumen

            Deudores = Tab.GetData(id_eva)

            Dim dt2 As DataTable

            dt2 = Deudores

            Pagares = Tab2.GetData(CLI.cli_idc)

            Dim dt3 As DataTable

            dt3 = Pagares

            Concentracion = Tab3.GetData(CLI.cli_idc, 1, 999999999, "CLIENTE COMO CLIENTE", 1, 2)

            Dim dt4 As DataTable

            dt4 = Concentracion

            Distribucion = Tab4.GetData(CLI.cli_idc)

            Dim dt5 As DataTable

            dt5 = Distribucion

            evolucion = tab5.GetData(CLI.cli_idc)

            Dim dt6 As DataTable

            dt6 = evolucion

            Dim rsc As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim pag As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim con As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim dis As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim evo As New Microsoft.Reporting.WebForms.ReportDataSource

            rsc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Cabecera", dt)
            rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Deudores", dt2)
            pag = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_pagares_vigentes", dt3)
            con = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Concentracion_Cliente", dt4)
            dis = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_distribucion_dias", dt5)
            evo = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_cl_evolucion_deuda", dt6)

            ReportViewer1.LocalReport.DataSources.Add(rsc)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(pag)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(con)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(dis)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(evo)
            ReportViewer1.DataBind()

            Dim archivo As String = "Evaluacion_" & CLI.cli_idc & "_ID_" & id_eva & ".pdf"
            Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim fileNameExtension As String = Nothing
            Dim streams As String() = Nothing
            Dim war As Warning() = Nothing
            Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", _
                                                                 Nothing, _
                                                                 mimeType, _
                                                                 encoding, _
                                                                 fileNameExtension, _
                                                                 streams, _
                                                                 war)
            'Dim Fs As New FileStream(path, FileMode.Create)
            'Fs.Write(Bit, 0, Bit.Length)
            'Fs.Close()

            CA.GuardaPDF(id_eva, Bit)
            ReportViewer1.Visible = False


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click
        Try

            'AG.EvaluacionElimina(ID)
            Dim RutDeudor As Integer

            RutDeudor = RG.LimpiaRut(GV_Deudores.Rows(Val(HF_Pos.Value) - 1).Cells(0).Text)

            If HF_NroEva.Value <> "" Then
                CMC.EvaluacionDeudorElimina(HF_NroEva.Value, RutDeudor)
            End If

            Coll_RSD = Session("Coll_RSD")

            Coll_RSD.Remove(CInt(HF_Pos.Value))

            Me.GV_Deudores.DataSource = Coll_RSD
            Me.GV_Deudores.DataBind()

            FormatoGrilla()
            LimpiaDeudor()
            HabilitarTxtDcto()


            IB_AgregarDeu.Enabled = False
            Txt_Rso_Deu.Text = ""
            Txt_Rso_Deu.ReadOnly = True
            HF_Pos.Value = ""
            IB_AyudaDeu.Enabled = True

            If GV_Deudores.Rows.Count = 0 Then
                Txt_Por_Ant.Text = ""
                DP_TipoMoneda.ClearSelection()
                LimpiaTotales()
            End If

            Txt_Mto_Doc.Text = ""

            Txt_Mto_Doc.CssClass = "clsDisabled"
            Txt_Mto_Doc.ReadOnly = True
            Txt_Mto_Doc_MaskedEditExtender.Enabled = True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Coll_RSD = New Collection

        Session("Coll_RSD") = Coll_RSD

        LimpiaDeudor()
        LimpiaTxtDcto()
        LimpiaTotales()
        IB_AgregarDeu.Enabled = False
        IB_QuitarDeu.Enabled = False
        Txt_Rut_Deu.Focus()

        GV_Deudores.DataSource = Coll_RSD
        GV_Deudores.DataBind()

        IB_AyudaDeu.Enabled = True


    End Sub

#End Region

#Region "Private Sub y Function"

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ConsultasGenerales
        Dim CLI As cli_cls

        Try

            If IsNothing(Session("Cliente")) Then
                Msj.Mensaje(Me, Caption, "Debe ingresar un cliente", TipoDeMensaje._Informacion)
                Return False
            End If

            CLI = Session("Cliente")

            If IsNothing(CLI) Then
                Msj.Mensaje(Me, Caption, "Cliente no existe", TipoDeMensaje._Informacion)
                Caso = 1
                Exit Function

            End If

            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim

            End If

            Txt_Rut_Cli.Text = Format(CDbl(CLI.cli_idc), FMT.FCMSD)
            Txt_Dig_Cli.Text = RG.Vrut(CDbl(CLI.cli_idc))

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

            Txt_Spread.Text = Format(Val(CLI.cli_spr_ead_col), FMT.FCMCD)

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return False

        End Try


    End Function

    Private Sub CargaDatosLineaCredito()

        Try

            Dim ClsLDC As New ConsultasGenerales
            Dim RG As New FuncionesGenerales.FComunes
            Dim LDC As ldc_cls
            Dim APC As Object

            LDC = ClsLDC.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text, 1)

            If IsNothing(LDC) Then
                Msj.Mensaje(Me, Caption, "Cliente no tiene línea de crédito vigente", TipoDeMensaje._Informacion)
                Me.Txt_Spread.Text = 0
                Me.Txt_LineaApro.Text = 0
                Me.Txt_LineaOcu.Text = 0
                Me.Txt_LineaDis.Text = 0
                Caso = 1
                Exit Sub
            End If

            If Not IsNothing(LDC) Then
                APC = CG.AnticipoDevuelvePorLinea(False, Nothing, LDC.id_ldc, LDC.id_ldc, 0, 999)
                HF_IdSbl.Value = LDC.id_ldc
                If Not IsNothing(APC) Then
                    Session("Anticipos") = APC
                End If
            End If

            Session("LineaCredito") = LDC

            id_ldc.Value = LDC.id_ldc

            Me.Txt_LineaApro.Text = Format(Val(LDC.ldc_mto_apb), FMT.FCMSD)
            Me.Txt_LineaOcu.Text = Format(Val(LDC.ldc_mto_ocp), FMT.FCMSD)
            Me.Txt_LineaDis.Text = Format(Val(LDC.ldc_mto_apb) - Val(LDC.ldc_mto_ocp), FMT.FCMSD)
            Me.Txt_FecVctoLin.Text = RG.FUNFecReg(LDC.ldc_fec_vig_hta)

            CargaDatosAnticipos()



        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDatosAnticipos()

        Try

            CG.AnticipoDevuelvePorLinea(True, GV_LineaCredito, id_ldc.Value, id_ldc.Value, 0, 999)

            If GV_LineaCredito.Rows.Count < 1 Then
                Msj.Mensaje(Me, Caption, "No existen condiciones de línea", TipoDeMensaje._Informacion)
                Caso = 3
            End If

            'Me.GV_LineaCredito.DataSource = Coll_APC
            'Me.GV_LineaCredito.DataBind()


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDatosGral()
        Try
            Dim Interes As ArrayList
            Dim Opo460 As ArrayList
            Dim RSC As Object
            Dim Visitas As vst_cls
            Dim UltOpo As opo_cls
            Dim RSD As Object
            Dim Var As New FuncionesGenerales.FComunes
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim Mto_Ocupado As Double


            Interes = CMC.InteresesCalculadosDevuelve(CLng(Txt_Rut_Cli.Text), Sesion.CodEje, Date.Now)
            RSC = CMC.ResumenClienteDevuelve(CLng(Txt_Rut_Cli.Text), Sesion.CodEje)
            Visitas = CMC.VisitasDevuelve(CLng(Txt_Rut_Cli.Text))
            UltOpo = OP.OperacionUltimaDevuelve(CLng(Txt_Rut_Cli.Text))
            RSD = CMC.ResumenClienteDeudorDevuelve(CLng(Txt_Rut_Cli.Text), CLng(Txt_Rut_Cli.Text))
            Opo460 = CMC.OperacionConProblemasCobranzaDevuelve(CLng(Txt_Rut_Cli.Text))

            If IsNothing(RSC) Then
                Msj.Mensaje(Me, Caption, "Cliente no posee historial", TipoDeMensaje._Informacion)
                Caso = 4
                'Exit Sub
                Me.Txt_ProDiaPag.Text = 0
                Mto_Ocupado = 0
                Me.Txt_Deu_Cli.Text = 0
                Me.Txt_Nro_Deu.Text = 0
            Else
                Session("Gral") = RSC

                Me.Txt_ProDiaPag.Text = RSC.rsc_prm_dia_pag
                Mto_Ocupado = RSC.rsc_mto_ocu
                Me.Txt_Deu_Cli.Text = Format(RSC.rsc_mto_ocu, FMT.FCMSD)
                Me.Txt_Nro_Deu.Text = RSC.rsc_ddr_ctd
            End If

            'Datos Comerciales
            If Not IsNothing(Visitas) Then
                Me.Txt_VisitasCli.Text = Visitas.vst_des_lar
            End If

            If Not IsNothing(UltOpo) Then
                Me.Txt_FecUltOpe.Text = FUNFecReg(UltOpo.ope_cls.ope_fec_sim)
            End If


            Dim rsd_mto_ocu As Double

            Dim rsd_mto_deu As Double

            Dim suma_deu_cli_deu As Double

            If Not IsNothing(RSD) Then

                For Each R In RSD
                    rsd_mto_ocu += R.rsd_mto_ocu
                    rsd_mto_deu += R.rsd_sdo_ddr
                Next

                Me.Txt_Deu_Deu.Text = Var.FormatoMiles(rsd_mto_deu)

            Else

                Me.Txt_Deu_Deu.Text = 0
                rsd_mto_ocu = 0

            End If

            'Deuda Consolidada
            suma_deu_cli_deu = Mto_Ocupado + rsd_mto_ocu

            Me.Txt_Tot_Deu.Text = Format(suma_deu_cli_deu, FMT.FCMSD)


            'Resumen Deuda Con Problemas
            If IsNothing(Opo460) Then
                Me.Txt_Mto_Pro.Text = 0
                Me.Txt_Doc_Pro.Text = 0
                Me.Txt_Deu_Pro.Text = 0
            Else
                Me.Txt_Mto_Pro.Text = Format(Opo460.Item(0), FMT.FCMSD)
                Me.Txt_Doc_Pro.Text = Format(Opo460.Item(1), FMT.FCMSD)
                Me.Txt_Deu_Pro.Text = Format(Opo460.Item(2), FMT.FCMSD)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub BloqueaTextosClientes()
        Me.Txt_Rut_Cli.ReadOnly = True
        Me.Txt_Dig_Cli.ReadOnly = True

        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"

        Me.Txt_Rut_Deu.ReadOnly = False
        Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        Me.Txt_Rut_Deu.Text = ""

        Me.Txt_Dig_Deu.ReadOnly = False
        Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
        Me.Txt_Dig_Deu.Text = ""

    End Sub

    Private Sub BloqueaTextosMontos()

        Me.Txt_Mto_Doc.ReadOnly = True
        Me.Txt_Mto_Doc.CssClass = "clsDisabled"

        Me.Txt_Por_Ant.CssClass = "clsDisabled"
        Me.Txt_Por_Ant.ReadOnly = True

        Me.DP_TipoMoneda.CssClass = "clsDisabled"
        Me.DP_TipoMoneda.Enabled = False

    End Sub

    Private Sub HabilitaTextosMontos()
        Me.Txt_Mto_Doc.ReadOnly = False
        Me.Txt_Por_Ant.ReadOnly = False

        Me.Txt_Mto_Doc.CssClass = "clsMandatorio"
        Me.Txt_Por_Ant.CssClass = "clsMandatorio"
    End Sub

    Private Sub HabilitarTxtDcto()

        'Me.DP_TipoMoneda.Enabled = False
        'Me.DP_TipoMoneda.CssClass = "clsDisabled"

        'Me.Txt_Por_Ant.ReadOnly = True
        'Me.Txt_Por_Ant.CssClass = "clsDisabled"

        Me.Txt_Mto_Doc.ReadOnly = False
        Me.Txt_Mto_Doc.CssClass = "clsMandatorio"
        Me.Txt_Mto_Doc.Text = ""

        Me.Txt_Mto_Eva.ReadOnly = True
        Me.Txt_Mto_Eva.CssClass = "clsDisabled"
        Me.Txt_Mto_Eva.Text = ""

        Me.Txt_Rut_Deu.Focus()


    End Sub

    Private Sub LimpiaTxtDcto()

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
        Me.Txt_Rut_Deu.Focus()

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

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        'Me.Txt_Obs.Text = ""


        Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        Me.Txt_Dig_Deu.CssClass = "clsMandatorio"

        Me.Txt_Rut_Deu.ReadOnly = False
        Me.Txt_Dig_Deu.ReadOnly = False

        Txt_Rut_Deu.Focus()

    End Sub

    Private Sub LimpiaTotales()
        Me.Txt_Tot_Eva.Text = ""
        Me.Txt_Tot_Doc.Text = ""
        Me.Txt_Tot_Deu.Text = ""
        Me.Txt_Deu_Tot.Text = ""
        Me.Txt_Obs.Text = ""
    End Sub

    Sub BloqueaTextosDeudores()

        Txt_Rut_Deu.ReadOnly = True
        Txt_Dig_Deu.ReadOnly = True


        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Dig_Deu.CssClass = "clsDisabled"

    End Sub

    Function EXISTE_DEUDOR_EVA() As Boolean
        Try

            Dim POS As Integer
            Dim i As Integer

            EXISTE_DEUDOR_EVA = False

            For i = 0 To GV_Deudores.Rows.Count - 1

                If GV_Deudores.Rows(i).Cells(0).Text = "" Then Exit For

                POS = InStr(Replace(GV_Deudores.Rows(i).Cells(0).Text, ",", ""), "-")
                Dim Rut As String = Trim(Mid(Replace(GV_Deudores.Rows(i).Cells(0).Text, ",", ""), 1, POS - 1))

                'verifica si el deudor ya esta ingresado
                If Trim(Me.Txt_Rut_Deu.Text) = Replace(Rut, ".", "") Then
                    Session("ACCION_COMERCIAL") = "MODIFICA"
                    EXISTE_DEUDOR_EVA = True
                    Exit For
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Function ValidaAgregarDeu() As Boolean
        Try

            If Me.DP_TipoMoneda.SelectedIndex = 0 Then
                Msj.Mensaje(Me, Caption, "Seleccione tipo de moneda", TipoDeMensaje._Exclamacion)
                Return True
            End If

            Select Case ""
                Case Me.Txt_Rut_Deu.Text
                    Msj.Mensaje(Me, Caption, "Ingrese NIT pagador", TipoDeMensaje._Exclamacion)
                    Return True
                Case Me.Txt_Dig_Deu.Text
                    Msj.Mensaje(Me, Caption, "Ingrese dígito pagador", TipoDeMensaje._Exclamacion)
                    Return True
                Case Me.Txt_Por_Ant.Text
                    Msj.Mensaje(Me, Caption, "Ingrese porcentaje de anticipo", TipoDeMensaje._Exclamacion)
                    Return True
                Case Me.Txt_Mto_Doc.Text
                    Msj.Mensaje(Me, Caption, "Ingrese monto del documento", TipoDeMensaje._Exclamacion)
                    Return True
                Case Me.Txt_Mto_Eva.Text
                    Msj.Mensaje(Me, Caption, "Ingrese monto evaluación", TipoDeMensaje._Exclamacion)
                    Return True
            End Select

            'If Txt_Por_Ant.Text = 0 Or Txt_Por_Ant.Text = "0.0" Then
            '    Msj.Mensaje(Me, Caption, "Seleccione Tipo de Documento", TipoDeMensaje._Exclamacion)
            '    Return True
            'End If

            If Txt_Por_Ant.Text = 0 Or Txt_Por_Ant.Text = "0.0" Then
                Msj.Mensaje(Me, Caption, "Ingrese porcentaje de anticipo", TipoDeMensaje._Exclamacion)
                Return True
            End If

            If Txt_Mto_Doc.Text = 0 Or Txt_Mto_Doc.Text = "0.0" Then
                Msj.Mensaje(Me, Caption, "Ingrese monto del documento", TipoDeMensaje._Exclamacion)
                Return True
            End If
            If CInt(Me.Txt_Por_Ant.Text) > 100 Then
                Msj.Mensaje(Page, Caption, "Porcentaje anticipo no puede ser mayor a 100" & HF_EstDes.Value, ClsMensaje.TipoDeMensaje._Exclamacion)
                Return True
            End If

            For I = 0 To GV_Deudores.Rows.Count - 1

                If Txt_Rut_Deu.Text.Replace(".", "") = RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text) Then
                    Msj.Mensaje(Me, Caption, "Pagador ya existe, ¿Desea cambiar el monto?", TipoDeMensaje._Confirmacion, LB_Deudor.UniqueID)
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
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
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub AlineaMontosDerecha()

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

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
            txt_HFEva.Text = HF_NroEva.Value

            Eva = CMC.EvaluacionDevuelvePorId(txt_HFEva.Text)

            If IsNothing(Eva) Then
                Msj.Mensaje(Me, Caption, "No se puede cargar la evaluación", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Txt_Por_Ant.Text = Eva.eva_por
            DP_TipoMoneda.SelectedValue = Eva.id_P_0023
            HF_Est.Value = Eva.id_P_0110
            HF_EstDes.Value = Eva.P_0110_cls.pnu_des
            Txt_Obs.Text = Eva.eva_obs

            'Traemos el formato de la moneda
            Formato = RG.DevuelveFormatoMoneda(DP_TipoMoneda.SelectedValue)

            Coll_EXD = CMC.EvaluacionDevuelveDeudoresPorIdEva(txt_HFEva.Text)

            'recorremos la collection para realizar los calculos
            For I = 1 To Coll_EXD.Count

                Dim EXD As New EvaDeu

                EXD.RutDeu = CStr(Format(CLng(Coll_EXD.Item(I).RutDeu), FMT.FCMSD)) & "-" & CStr(RG.Vrut(CLng(Coll_EXD.Item(I).RutDeu)))
                EXD.NomDeu = Coll_EXD.Item(I).NomDeu
                EXD.MtoEva = Coll_EXD.Item(I).MtoEva
                EXD.MtoDoc = Coll_EXD.Item(I).MtoDoc
                EXD.Moneda = Coll_EXD.Item(I).Moneda
                EXD.PorAnt = Eva.eva_por
                EXD.TipoMoneda = Eva.id_P_0023


                'trae deuda del deudor
                Dim SumatoriaDeuda As ArrayList
                Dim factor As Double

                SumatoriaDeuda = CMC.DeudorDeudaDevuelve(Txt_Rut_Cli.Text, CLng(Coll_EXD.Item(I).RutDeu))

                factor = CG.ParidadDevuelve(DP_TipoMoneda.SelectedValue, Eva.eva_fec_cre).par_val

                If DP_TipoMoneda.SelectedValue > 1 Then

                    SumatoriaDeuda.Item(0) = SumatoriaDeuda.Item(0) / factor
                    SumatoriaDeuda.Item(1) = SumatoriaDeuda.Item(1) / factor

                End If

                If SumatoriaDeuda.Count > 0 Then
                    HTodoLoPaga = SumatoriaDeuda.Item(0)
                    HPagFactoring = SumatoriaDeuda.Item(1)
                Else
                    HTodoLoPaga = 0
                    HPagFactoring = 0
                End If

                EXD.MtoSbl = CG.SubLineasDevuelvePorDeudor(Coll_EXD.Item(I).RutDeu) * factor
                EXD.Cupo = CG.Devuelvelineaglobaldeudor(Coll_EXD.Item(I).RutDeu, DP_TipoMoneda.SelectedValue)
                EXD.Disponible = EXD.Cupo - HTodoLoPaga

                EXD.DeuAct = HTodoLoPaga
                EXD.DeuFac = HPagFactoring
                EXD.DeuTot = EXD.MtoEva + HTodoLoPaga

                suma_mto_eval = suma_mto_eval + EXD.MtoEva
                Mto_Tot_Doc = Mto_Tot_Doc + EXD.MtoDoc

                EXD.PorCli = (EXD.DeuTot / (mto_tot_deu_cli + suma_mto_eval)) * 100

                If IsNothing(Coll_EXD.Item(I).PorCli) Or Coll_EXD.Item(I).PorCli = 0 Then
                    EXD.PorCli = 0
                Else
                    EXD.PorCli = Format(CDbl(Coll_EXD.Item(I).PorCli), "##.##")
                End If

                'EXD.PorCli = Format(CDbl(Coll_EXD.Item(I).PorCli), "##.##")

                Coll_RSD.Add(EXD)

            Next


            Me.GV_Deudores.DataSource = Coll_RSD
            Me.GV_Deudores.DataBind()

            FormatoGrilla()

            If IsNothing(Session("Coll_RSD")) Then
                Session("Coll_RSD") = Coll_RSD
            Else
                If Session("COLL_RSD").COUNT = 0 Then
                    Session("Coll_RSD") = Coll_RSD
                End If
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
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

            Dim Formato As String = ""

            Select Case DP_TipoMoneda.SelectedValue
                Case 1 : Formato = FMT.FCMSD
                Case 2 : Formato = FMT.FCMCD4
                Case 3, 4 : Formato = FMT.FCMCD
            End Select

            Txt_Mto_Eva.Text = Format(MtoEva, Formato)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub AgregarDeudor()
        Try
            Dim GRAL As Object
            Dim Eva As New EvaDeu
            Dim mto_tot_deu_cli As Double
            Dim suma_mto_eval As Double
            Dim POS As Integer
            Dim I As Integer

            Dim HTodoLoPaga As Double
            Dim HPagFactoring As Double

            Eva.RutDeu = RG.FormatoMiles(Me.Txt_Rut_Deu.Text) & "-" & Txt_Dig_Deu.Text
            Eva.NomDeu = Me.Txt_Rso_Deu.Text.ToUpper

            Eva.PorAnt = Me.Txt_Por_Ant.Text

            'trae deuda del deudor
            Dim SumatoriaDeuda As ArrayList

            SumatoriaDeuda = CMC.DeudorDeudaDevuelve(Txt_Rut_Cli.Text, Txt_Rut_Deu.Text)

            Dim factor As Double
            factor = CG.ParidadDevuelve(DP_TipoMoneda.SelectedValue, Date.Now).par_val

            If DP_TipoMoneda.SelectedValue > 1 Then

                Dim mensaje As String

                Select Case DP_TipoMoneda.SelectedValue
                    Case 2
                        mensaje = "UF"
                    Case 3
                        mensaje = "Dolar"
                    Case 4
                        mensaje = "Euro"
                End Select

                If factor = 0 Then
                    Msj.Mensaje(Page, Caption, "Debe ingresar en pantalla paridad la moneda" & " " & mensaje, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                SumatoriaDeuda.Item(0) = SumatoriaDeuda.Item(0) / factor
                SumatoriaDeuda.Item(1) = SumatoriaDeuda.Item(1) / factor

            End If

            If SumatoriaDeuda.Count > 0 Then
                HTodoLoPaga = SumatoriaDeuda.Item(0)
                HPagFactoring = SumatoriaDeuda.Item(1)
            Else
                HTodoLoPaga = 0
                HPagFactoring = 0
            End If

            Eva.DeuAct = HTodoLoPaga
            Eva.DeuFac = HPagFactoring

            If IsNothing(SumatoriaDeuda.Item(0)) Then
                Eva.HTodoLoPaga = 0
            Else
                Eva.HTodoLoPaga = SumatoriaDeuda.Item(0)
            End If

            If IsNothing(SumatoriaDeuda.Item(1)) Then
                Eva.HPagFactoring = 0
            Else
                Eva.HPagFactoring = SumatoriaDeuda.Item(1)
            End If

            Eva.MtoSbl = CG.SubLineasDevuelvePorDeudor(Txt_Rut_Deu.Text) * factor
            Eva.Cupo = CG.Devuelvelineaglobaldeudor(Txt_Rut_Deu.Text, DP_TipoMoneda.SelectedValue)
            Eva.Disponible = Eva.Cupo - HPagFactoring

            Eva.MtoDoc = CDbl(Me.Txt_Mto_Doc.Text)
            Eva.MtoEva = CDbl(Me.Txt_Mto_Eva.Text)

            Eva.DeuTot = SumatoriaDeuda.Item(1) + CDbl(Eva.MtoDoc)

            'Deuda Total Cliente
            If Not IsNothing(Session("Gral")) Then
                GRAL = Session("Gral")

                mto_tot_deu_cli = (GRAL.rsc_mto_vig + _
                                   GRAL.Monto_Mora + _
                                   GRAL.rsc_mto_let_mor + _
                                   GRAL.rsc_mto_let_prt + _
                                   GRAL.rsc_mto_pgr_prt + _
                                   GRAL.rsc_mto_cob_jud + _
                                   GRAL.rsc_mto_cxc + _
                                   GRAL.rsc_mto_cnj_opl + _
                                   GRAL.rsc_mto_cnj_opl) + _
                                   (GRAL.rsc_mto_exd + _
                                   GRAL.rsc_mto_cxp + _
                                   GRAL.rsc_mto_int_dev)
            Else
                mto_tot_deu_cli = 0
            End If


            'recorremos la grila la ver si el deudor ya esta ingresado
            For I = 0 To GV_Deudores.Rows.Count - 1

                'si no existe un rut en la columna 0 sale
                If GV_Deudores.Rows(I).Cells(0).Text = "" Then Exit For

                'rescatamos la posicion donde se encuentra el guion "-"
                POS = InStr(Replace(GV_Deudores.Rows(I).Cells(0).Text, ",", ""), "-")
                Dim Rut As String = RG.LimpiaRut(GV_Deudores.Rows(I).Cells(0).Text)

                'verifica si el deudor ya esta ingresado
                If Trim(Replace(Me.Txt_Rut_Deu.Text, ".", "")) = Replace(Rut, ".", "") Then

                    'si esta ingresado se deben reemplazar los datos

                    Coll_RSD = Session("Coll_RSD")

                    Me.Txt_Tot_Eva.Text = CDbl(Me.Txt_Tot_Eva.Text) - CDbl(GV_Deudores.Rows(I).Cells(4).Text)
                    Me.Txt_Tot_Doc.Text = CDbl(Me.Txt_Tot_Doc.Text) - CDbl(GV_Deudores.Rows(I).Cells(5).Text)
                    Me.Txt_Deu_Tot.Text = CDbl(Me.Txt_Deu_Tot.Text) - CDbl(GV_Deudores.Rows(I).Cells(6).Text)

                    Coll_RSD.Remove(I + 1)

                    Exit For

                End If

            Next

            If GV_Deudores.Rows.Count = 0 Then
                Coll_RSD = New Collection
                Session("Coll_RSD") = Coll_RSD
            End If

            If Me.Txt_Deu_Tot.Text = "" Then
                Me.Txt_Deu_Tot.Text = CDbl(Eva.DeuTot)
            Else
                Me.Txt_Deu_Tot.Text = CDbl(Me.Txt_Deu_Tot.Text) + CDbl(Eva.DeuTot)
            End If

            If Me.Txt_Tot_Doc.Text = "" Then
                Me.Txt_Tot_Doc.Text = CDbl(Eva.MtoDoc)
            Else
                Me.Txt_Tot_Doc.Text = CDbl(Me.Txt_Tot_Doc.Text) + CDbl(Eva.MtoDoc)
            End If

            If Me.Txt_Tot_Eva.Text = "" Then
                Me.Txt_Tot_Eva.Text = CDbl(Eva.MtoEva)
            Else
                Me.Txt_Tot_Eva.Text = CDbl(Me.Txt_Tot_Eva.Text) + CDbl(Eva.MtoEva)
            End If

            suma_mto_eval = CDbl(Me.Txt_Tot_Eva.Text)

            '% Cliente  = ( Monto Evaluado del Deudor / Monto línea aprobado ) * 100

            Dim MtoLinApr As Double

            If Txt_LineaApro.Text <> "" And Txt_LineaApro.Text <> "0" Then
                MtoLinApr = CDbl(Txt_LineaApro.Text.Replace(".", ""))
                Eva.PorCli = (Eva.MtoEva / MtoLinApr) * 100
            Else
                Eva.PorCli = 0
            End If

            Session("ACCION_COMERCIAL") = "MODIFICA"

            Eva.TipoMoneda = DP_TipoMoneda.SelectedValue
            Eva.Moneda = DP_TipoMoneda.SelectedItem.Text.Trim

            If IsNothing(Session("Coll_RSD")) Then
                Coll_RSD = New Collection
                Coll_RSD.Add(Eva)
                Session("Coll_RSD") = Coll_RSD
            Else
                Coll_RSD = Session("Coll_RSD")
                Coll_RSD.Add(Eva)
            End If

            Me.GV_Deudores.DataSource = Coll_RSD
            Me.GV_Deudores.DataBind()

            FormatoGrilla()

            LimpiaDeudor()
            HabilitarTxtDcto()
            BloqueaTextosMontos()
            IB_AgregarDeu.Enabled = False
            Txt_Rso_Deu.Text = ""
            Txt_Rso_Deu.ReadOnly = True
            'Txt_Mto_Doc_MaskedEditExtender.Enabled = False

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoGrilla()
        Try

            Dim Formato As String = ""
            Dim Suma_Evaluado As Double
            Dim Suma_Documento As Double
            Dim Suma_Total As Double

            Select Case DP_TipoMoneda.SelectedValue
                Case 1 : Formato = FMT.FCMSD
                Case 2 : Formato = FMT.FCMCD4
                Case 3, 4 : Formato = FMT.FCMCD
            End Select

            For I = 0 To GV_Deudores.Rows.Count - 1

                GV_Deudores.Rows(I).Cells(2).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(2).Text), FMT.FCMSD)
                GV_Deudores.Rows(I).Cells(3).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(3).Text), FMT.FCMSD)
                GV_Deudores.Rows(I).Cells(4).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(4).Text), Formato)
                GV_Deudores.Rows(I).Cells(5).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(5).Text), Formato)
                GV_Deudores.Rows(I).Cells(6).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(6).Text), Formato)
                GV_Deudores.Rows(I).Cells(7).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(7).Text), FMT.FCMCD4)
                GV_Deudores.Rows(I).Cells(9).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(9).Text), Formato)

                GV_Deudores.Rows(I).Cells(10).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(10).Text), Formato)
                GV_Deudores.Rows(I).Cells(11).Text = Format(CDbl(GV_Deudores.Rows(I).Cells(11).Text), Formato)

                Suma_Evaluado += CDbl(GV_Deudores.Rows(I).Cells(4).Text)
                Suma_Documento += CDbl(GV_Deudores.Rows(I).Cells(5).Text)
                Suma_Total += CDbl(GV_Deudores.Rows(I).Cells(6).Text)

            Next

            Me.Txt_Tot_Eva.Text = Format(Suma_Evaluado, Formato)
            Me.Txt_Tot_Doc.Text = Format(Suma_Documento, Formato)
            Me.Txt_Deu_Tot.Text = Format(Suma_Total, Formato)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CambiaMascaraMonto()
        Try

            Select Case DP_TipoMoneda.SelectedValue
                Case 1
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999.9999"
                Case 3, 4
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999.99"
            End Select

            Txt_Mto_Doc.Text = ""
            Txt_Mto_Doc.Focus()
            IB_AgregarDeu.Enabled = True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub Comentarios_Eva()
        Try
            Dim Texto As Object


            Dim LDC As ldc_cls = CG.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text, 1)

            If Not IsNothing(LDC) Then


                If LDC.id_P_0029 = 2 Then
                    Msj.Mensaje(Me, Caption, "LINEA DE FINANCIAMIENTO CADUCADA DEL CLIENTE", TipoDeMensaje._Exclamacion)
                    Texto = Texto & " - LINEA DE FINANCIAMIENTO CADUCADA DEL CLIENTE" & Chr(13)
                End If

                If CDbl(Txt_LineaDis.Text.Trim) <= 0 Then
                    If CDbl(Txt_LineaDis.Text.Trim) = 0 Then
                        Msj.Mensaje(Me, Caption, "CLIENTE NO TIENE CUPO DISPONIBLE EN LINEA ", TipoDeMensaje._Exclamacion)
                        Texto = Texto & " - CLIENTE NO TIENE CUPO DISPONIBLE EN LINEA " & Chr(13)
                    Else
                        Msj.Mensaje(Me, Caption, "LINEA DE FINANCIAMIENTO SOBREGIRADA DEL CLIENTE", TipoDeMensaje._Exclamacion)
                        Texto = Texto & " - LINEA DE FINANCIAMIENTO SOBREGIRADA DEL CLIENTE" & Chr(13)
                    End If
                Else
                    If CDbl(Txt_Mto_Eva.Text.Trim) > CDbl(Txt_LineaDis.Text.Trim) Then
                        Msj.Mensaje(Me, Caption, "EVALUACION SUPERA LINEA DE FINANCIAMIENTO DEL CLIENTE", TipoDeMensaje._Exclamacion)
                        Texto = Texto & " - EVALUACION SUPERA LINEA DE FINANCIAMIENTO DEL CLIENTE" & Chr(13)
                    End If
                End If

            Else
                Msj.Mensaje(Me, Caption, "CLIENTE NO TIENE LÍNEA DE FINANCIAMIENTO VIGENTE", TipoDeMensaje._Exclamacion)
                Texto = Texto & " - CLIENTE NO TIENE LÍNEA DE FINANCIAMIENTO VIGENTE" & Chr(13)
            End If

            Dim APC As Object

            If Not IsNothing(Session("Anticipos")) Then
                APC = Session("Anticipos")
                Dim Porcentaje As Decimal

                For Each P In APC
                    If P.id_P_0031 = 1 Then
                        Porcentaje = P.apc_pct

                        If Porcentaje > Txt_Por_Ant.Text Then
                            Msj.Mensaje(Me, Caption, "Porcentaje de anticipo DISMINUIDO por ejecutivo", TipoDeMensaje._Exclamacion)
                            Texto = Texto & " - Porcentaje de anticipo DISMINUIDO por ejecutivo" & Chr(13)
                        End If

                        If Porcentaje < Txt_Por_Ant.Text Then
                            Msj.Mensaje(Me, Caption, "Porcentaje de anticipo AUMENTADO por ejecutivo", TipoDeMensaje._Exclamacion)
                            Texto = Texto & " - Porcentaje de anticipo AUMENTADO por ejecutivo" & Chr(13)
                        End If

                        Exit For

                    End If
                Next
            End If

            Dim mto As Double
            Dim coll As New Collection

            If HF_IdSbl.Value <> "" Then

                '***********************************************************************************************************************
                'Advierte si sobrepasa linea de credito deudor/producto
                'ASaldivar 08/11/2010
                '***********************************************************************************************************************
                coll = CMC.SubLineaDevuelveObjeto(id_ldc.Value)
                If Not IsNothing(coll) Then
                    If coll.Count > 0 Then

                        For i = 1 To coll.Count


                            If coll.Item(i).sbl_tip = "P" Then 'Producto
                                If coll.Item(i).id_P_0031 = 1 Then
                                    If Txt_Mto_Eva.Text > coll.Item(i).sbl_mto Then
                                        Msj.Mensaje(Me, Caption, "Sub Linea de producto sobregirada", TipoDeMensaje._Exclamacion)
                                        Texto = Texto & " - Sub Linea de producto sobregirada" & Chr(13)
                                    End If
                                End If
                            Else 'Deudor
                                If Format(CLng(Txt_Rut_Deu.Text.Trim), VAR.FMT_RUT) = coll.Item(i).deu_ide Then
                                    If Txt_Mto_Eva.Text > coll.Item(i).sbl_mto Then
                                        Msj.Mensaje(Me, Caption, "Sub Linea de pagador sobregirada", TipoDeMensaje._Exclamacion)
                                        Texto = Texto & " - Sub Linea de pagador sobregirada" & Chr(13)
                                    End If
                                End If
                            End If

                        Next

                    End If

                End If

            End If


            Txt_Obs.Text = Texto

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
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


