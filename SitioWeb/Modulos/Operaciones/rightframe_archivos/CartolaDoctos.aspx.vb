Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports ClsSession.SesionAplicaciones

Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_CartolaDoctos
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim Sesion As New ClsSession.ClsSession
    Dim Sesion_Op As New ClsSession.SesionOperaciones
    Dim Caption As String = "Documentos"
    Dim clasecli As New ClaseClientes

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales

    Dim FC As New FuncionesGenerales.FComunes
    Dim RG As New FuncionesGenerales.RutinasWeb
    Dim Var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Agt As New Perfiles.Cls_Principal
    Dim OP As New ClaseOperaciones
    Dim cbz As New ClaseCobranza
    'Dim Coll_Otorgamiento As New Collection
    'Dim Coll_Recaudacion As New Collection
    'Dim Coll_Excedentes As New Collection
    'Dim Coll_Cuentas As New Collection
    'Dim Coll_GestionDocto As New Collection
    'Dim Cli As New cli_cls
    'Dim Deu As New deu_cls
    'Dim Cco As cco_cls


    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
            If Not IsPostBack Then
                sesion.Modulo = "Operacion"
                Pagina = Page.AppRelativeVirtualPath
                CambioTema(Page)
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                valida_cliente = ""
                Sesion_Op.Coll_DetalleDocto = New Collection

                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

                Me.Txt_Doc_Dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_Doc_Hta.Attributes.Add("Style", "TEXT-ALIGN: right")

                Me.Txt_Oto_Dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_Oto_Hta.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Deu2.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Cli_2.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Num_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Cod_Flj.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Cuota.Attributes.Add("Style", "TEXT-ALIGN: right")

                LlenaDrop()
                Limpiar()
                NroPaginacion = 0
                page_gestion_doc = 0
                page_cuen = 0
                page_exc = 0
                page_recau = 0
                page_otg = 0

            End If

            SW = 99


            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?cartola=1','PopUpCliente',580,410,200,150);")
            IB_Cancelar2.Attributes.Add("onClick", "Javascript:window.close();")
            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx?cartola=1','PopUpDeudor',580,410,200,150);")
            'Response.Redirect("ModDoctos.aspx?Nro=" & (CInt(Sesion_Op.Coll_DOC(CInt(Posicion)).cli_idc)) & "&Posi=" & Posicion, False)
            'IB_Modificar.Attributes.Add("onClick", "var x=window.showModalDialog('ModDoctos.aspx?Nro=' " & (CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_idc)) & ", window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:400px;dialogLeft:400px;dialogTop:200px');")
            'IB_Modificar.Attributes.Add("onClick", "WinOpen(2,'ModDoctos.aspx','PopUpModDoctos',700,600,200,150)")
            'IB_Modificar.Attributes.Add("onClick", "WinOpen(2,'ModDoctos.aspx?Nro=" & RetornarValores() & "','PopUpModDoctos',630,420,200,150)")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub ChKB_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_Cli.CheckedChanged
        Try
            If ChKB_Cli.Checked Then
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Rut_Cli.Focus()
                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Else
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.Text = ""
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.Focus()
                IB_AyudaCli.Enabled = False
                Txt_Raz_Soc.Text = ""
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ChkB_Docto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkB_Docto.CheckedChanged
        Try
            If ChkB_Docto.Checked Then
                Txt_Nro_Doc.CssClass = "clsMandatorio"
                Txt_Cuota2.CssClass = "clsMandatorio"
                Txt_Nro_Doc.ReadOnly = False
                Txt_Cuota2.ReadOnly = False
                Txt_Nro_Doc.Focus()
            Else
                Txt_Nro_Doc.CssClass = "clsDisabled"
                Txt_Cuota2.CssClass = "clsDisabled"
                Txt_Nro_Doc.ReadOnly = True
                Txt_Cuota2.ReadOnly = True
            End If
            MlPopupExt_ModDoctos.Show()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ChKB_Deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_Deu.CheckedChanged
        Try
            If ChKB_Deu.Checked Then
                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Rut_Deu.ReadOnly = False
                Txt_Dig_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.ReadOnly = False
                Txt_Rut_Deu.Focus()
                IB_AyudaDeu.Enabled = True
                Txt_Rut_Deu_MaskedEditExtender.Enabled = True
            Else
                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Rut_Deu.Text = ""
                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.Text = ""
                Txt_Dig_Deu.ReadOnly = True
                IB_AyudaDeu.Enabled = False
                Txt_Rso_Deu.Text = ""
                Txt_Rut_Deu_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ChkB_Deudor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkB_Deudor.CheckedChanged
        Try
            If ChkB_Deudor.Checked Then
                Txt_Rut_Deu2.CssClass = "clsMandatorio"
                Txt_Rut_Deu2.ReadOnly = False
                Txt_Dig_Deu2.CssClass = "clsMandatorio"
                Txt_Dig_Deu2.ReadOnly = False
                Txt_Rut_Deu2.Focus()
                Txt_Rut_Deu2.Text = ""
                Txt_Dig_Deu2.Text = ""

            Else
                Txt_Rut_Deu2.CssClass = "clsDisabled"
                Txt_Rut_Deu2.ReadOnly = True
                Txt_Dig_Deu2.CssClass = "clsDisabled"
                Txt_Dig_Deu2.ReadOnly = True
            End If
            MlPopupExt_ModDoctos.Show()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ChkB_CarDocto_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Sesion_Op.Coll_DetalleDocto = New Collection

            hf_exc.Value = ""
            hf_ges.Value = ""
            hf_otor.Value = ""
            hf_otr_cta.Value = ""
            hf_rec.Value = ""
            'MarcaGrilla()
            For I = 0 To GV_Documentos.Rows.Count - 1

                If CType(GV_Documentos.Rows(I).FindControl("ChkB_CarDocto"), CheckBox).Checked Then

                    Txt_PosGv.Value = I
                    TraeDetalleDoctos()
                    
                    Sesion_Op.Coll_DetalleDocto.Add(Sesion_Op.Coll_DOC.Item(I + 1))
                    Exit For
                Else
                    Txt_PosGv.Value = ""
                End If

            Next

            If hf_exc.Value <> "" Then
                Chk_Exce.Enabled = True
            End If

            If hf_ges.Value <> "" Then
                Chk_Ges.Enabled = True
            End If

            If hf_otor.Value <> "" Then
                Chk_Oto.Enabled = True
            End If

            If hf_rec.Value <> "" Then
                ChK_Rec.Enabled = True
            End If

            If hf_otr_cta.Value <> "" Then
                Chk_Cuentas.Enabled = True
            End If

            MarcaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Dp_Ejecu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Ejecu.SelectedIndexChanged
        Try
            Select Case Dp_Ejecu.SelectedValue
                Case 0
                    Rb_Eje.Checked = True
                Case Else
                    Rb_Eje.Checked = False
            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dp_Est_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Est.SelectedIndexChanged
        Try
            Select Case Dp_Est.SelectedValue
                Case 0
                    Rb_Est.Checked = True
                Case Else
                    Rb_Est.Checked = False
            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dp_Moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Moneda.SelectedIndexChanged
        Try
            Select Case Dp_Moneda.SelectedValue
                Case 0
                    Rb_Mon.Checked = True
                Case Else
                    Rb_Mon.Checked = False
            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Documentos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
                'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
                'e.Row.Attributes.Add("onClick", "CelClick_GrillaCartola(ctl00_ContentPlaceHolder1_GV_Documentos, 'clicktable', 'formatable', 'selectable')")
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Fec_Ini_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Ini.TextChanged
        'Try
        '    If Not IsDate(Txt_Fec_Ini.Text) Then
        '        Msj.Mensaje(Page, Caption, "formato de fecha no valido", ClsMensaje.TipoDeMensaje._Exclamacion)
        '        Txt_Fec_Ini.Text = ""
        '        Txt_Fec_Ini.Focus()
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub Txt_Fec_Fin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Fin.TextChanged
        'Try

        '    If Txt_Fec_Fin.Text <> "" And Txt_Fec_Fin.Text <> "__/__/____" Then


        '        If Not isdate(Txt_Fec_Fin.Text) Then
        '            Msj.Mensaje(Me.Page, Caption, "Fecha erronea", TipoDeMensaje._Exclamacion)
        '            Txt_Fec_Ini.Focus()
        '            Exit Sub
        '        End If

        '        If Trim(Txt_Fec_Ini.Text) <> "" And Trim(Txt_Fec_Fin.Text) <> "" Then
        '            If CDate(Txt_Fec_Fin.Text) < CDate(Txt_Fec_Ini.Text) Then
        '                Msj.Mensaje(Me.Page, Caption, "Fecha desde no puede ser mayor a fecha hasta", TipoDeMensaje._Exclamacion)
        '                Txt_Fec_Ini.Text = ""
        '                Txt_Fec_Ini.Focus()
        '                Exit Sub
        '            End If
        '        End If

        '        If Txt_Fec_Fin.Text <> "" Then

        '            If Not IsDate(Txt_Fec_Fin.Text) Then
        '                Msj.Mensaje(Page, Caption, "formato de fecha no valido", ClsMensaje.TipoDeMensaje._Exclamacion)
        '                Txt_Fec_Fin.Text = ""
        '                Txt_Fec_Fin.Focus()
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub Txt_Fec_OtoDsd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_OtoDsd.TextChanged
        'Try
        '    If Txt_Fec_OtoDsd.Text = "" Then
        '        Exit Sub
        '    End If

        '    If Not IsDate(Txt_Fec_OtoDsd.Text) And Txt_Fec_OtoDsd.Text <> "__/__/____" Then
        '        Msj.Mensaje(Page, Caption, "formato de fecha no valido", ClsMensaje.TipoDeMensaje._Exclamacion)
        '        Txt_Fec_OtoDsd.Text = ""
        '        Txt_Fec_OtoDsd.Focus()
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub Txt_Fec_OtoHta_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_OtoHta.TextChanged
        Try

            'If Txt_Fec_OtoDsd.Text <> "" Or Txt_Fec_OtoDsd.Text = "__/__/____" Then
            '    Exit Sub
            'End If
            'If Not IsDate(Txt_Fec_OtoDsd.Text) Then
            '    Msj.Mensaje(Page, Caption, "Fecha otorgamiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Txt_Fec_OtoDsd.Text = ""
            '    Exit Sub
            'End If

            'If Txt_Fec_OtoHta.Text <> "" Then


            '    If Not IsDate(Txt_Fec_OtoHta.Text) And Txt_Fec_OtoHta.Text <> "__/__/____" Then
            '        Msj.Mensaje(Page, Caption, "Fecha otorgamiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '        Txt_Fec_OtoHta.Text = ""
            '        Exit Sub
            '    End If
            '    If Txt_Fec_OtoDsd.Text <> "" And Txt_Fec_OtoDsd.Text <> "__/__/____" Then

            '        If CDate(Txt_Fec_OtoDsd.Text) > "31/12/2999" Then
            '            Msj.Mensaje(Page, Caption, "Fecha otorgamiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '            Txt_Fec_OtoDsd.Text = ""
            '            Exit Sub
            '        End If

            '    End If
            '    If Txt_Fec_OtoHta.Text <> "__/__/____" Then

            '        If CDate(Txt_Fec_OtoHta.Text) > "31/12/2999" Then
            '            Msj.Mensaje(Page, Caption, "Fecha otorgamiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '            Txt_Fec_OtoHta.Text = ""
            '            Exit Sub
            '        End If
            '        If Trim(Txt_Fec_OtoDsd.Text) <> "" And Trim(Txt_Fec_OtoHta.Text) <> "" Then
            '            If CDate(Txt_Fec_OtoHta.Text) < CDate(Txt_Fec_OtoDsd.Text) Then
            '                Msj.Mensaje(Me.Page, Caption, "Fecha desde no puede ser mayor a fecha desde", TipoDeMensaje._Exclamacion)
            '                Txt_Fec_OtoDsd.Text = ""
            '                Txt_Fec_OtoDsd.Focus()
            '                Exit Sub
            '            End If
            '        End If


            '        If Txt_Fec_OtoHta.Text <> "" Then
            '            If Not IsDate(Txt_Fec_OtoHta.Text) Then
            '                Msj.Mensaje(Page, Caption, "formato de fecha no valido", ClsMensaje.TipoDeMensaje._Exclamacion)
            '                Txt_Fec_OtoHta.Text = ""
            '                Txt_Fec_OtoHta.Focus()
            '            End If
            '        End If
            '    End If

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Eje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Eje.CheckedChanged
        Try
            If Rb_Eje.Checked Then
                Dp_Ejecu.Enabled = False
                Dp_Ejecu.ClearSelection()
            Else
                Dp_Ejecu.Enabled = True
                Dp_Ejecu.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChKB_Suc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChKB_Suc.CheckedChanged
        Try
            If ChKB_Suc.Checked Then
                DropSucursal.ClearSelection()
                DropSucursal.Enabled = False
            Else
                DropSucursal.ClearSelection()
                DropSucursal.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Est_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Est.CheckedChanged
        Try
            If Rb_Est.Checked Then
                Dp_Est.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Mon_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Mon.CheckedChanged
        Try
            If Rb_Mon.Checked Then
                Dp_Moneda.ClearSelection()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Cob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Cob.CheckedChanged
        Try
            If Rb_Cob.Checked Then
                Dp_Cobranza.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Dp_Cobranza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Cobranza.SelectedIndexChanged
        Try
            If Dp_Cobranza.SelectedValue > 0 Then
                Rb_Cob.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Dp_Res_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Res.SelectedIndexChanged
        Try
            If Dp_Res.SelectedValue > 0 Then
                Rb_Res.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Res_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Res.CheckedChanged
        Try
            If Rb_Res.Checked Then
                Dp_Res.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20030805, Usr, "PRESIONA BOTON BUSCAR DOCUMENTOS") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not ValidaBlancos() Then
                Exit Sub
            End If

            If ChKB_Cli.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Dim Cli As cli_cls

                Cli = clasecli.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)

                If valida_cliente <> "" Then

                    Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else



                    If Cli.id_P_0044 <> 1 Then
                        Txt_Raz_Soc.Text = Cli.cli_rso.Trim
                    Else
                        Txt_Raz_Soc.Text = Cli.cli_rso.Trim & " " & Cli.cli_ape_ptn.Trim & " " & Cli.cli_ape_mtn.Trim
                    End If

                    Txt_Nom_Cli2.Text = Txt_Raz_Soc.Text
                End If
                If IsNothing(Cli) Then
                    Msj.Mensaje(Me, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


            End If

            If ChKB_Deu.Checked Then

                If Trim(Txt_Rut_Deu.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Ingrese NIT deudor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If

                If Trim(Txt_Dig_Deu.Text).ToUpper <> FC.Vrut(CLng(Txt_Rut_Deu.Text)).ToUpper Then
                    Msj.Mensaje(Me.Page, Caption, "NIT de Pagador incorrecto", TipoDeMensaje._Exclamacion)
                End If

                Dim Deu As deu_cls

                Deu = CG.DeudorDevuelvePorRut(CLng(Txt_Rut_Deu.Text))
                If IsNothing(Deu) Then
                    Msj.Mensaje(Page, Caption, "Deudor no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Txt_Rso_Deu.Text = Deu.deu_rso
                Txt_Nom_Deu2.Text = Deu.deu_rso

            End If

            NroPaginacion = 0
            Sesion_Op.Coll_DOC = New Collection
            LlenaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Cartola_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cartola.Click
        Try

            If Not Agt.ValidaAccesso(20, 20020805, Usr, "PRESIONA BOTON VER CARTOLA") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If



            Dim sw As Integer = 0
            Dim cb As New CheckBox
            For i = 0 To GV_Documentos.Rows.Count - 1
                cb = CType(GV_Documentos.Rows(i).FindControl("ChkB_CarDocto"), CheckBox)
                If cb.Checked Then
                    sw = 1
                    Exit For
                End If
            Next

            If sw = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione un documento", ClsMensaje.TipoDeMensaje._Exclamacion, , True)
                Exit Sub
            End If



            MlPopupExt_DetalleDoctos.Show()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If ChkB_Deudor.Checked Then
                If Trim(Txt_Rut_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de deudor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de deudor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) <> FC.Vrut(Txt_Rut_Deu2.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Rut de duedor incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If

                Dim Deu As deu_cls

                Deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT))
                If IsNothing(Deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                Txt_Nom_Deu2.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar los cambios?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not Agt.ValidaAccesso(20, 20040805, Usr, "PRESIONA BOTON IMPRIMIR CARTOLA") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Sesion_Op.Coll_DetalleDocto.Count = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar documentos a imprimir", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not Chk_Oto.Checked And Not ChK_Rec.Checked And Not Chk_Exce.Checked And Not Chk_Cuentas.Checked And Not Chk_Ges.Checked Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar al menos una opción de cartola de documento", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Dim cont As Integer = 0
            For i = 0 To GV_Documentos.Rows.Count - 1
                If CType(GV_Documentos.Rows(i).FindControl("ChkB_CarDocto"), CheckBox).Checked = True Then
                    cont = cont + 1
                End If
            Next

            If cont > 1 Then
                Msj.Mensaje(Me, Caption, "Seleccione solo un documento para imprimir", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            AbrePopup(Me, 1, "InfCartolaDocto.aspx?Otorgamiento=" & Chk_Oto.Checked & "&Recaudacion=" & ChK_Rec.Checked & _
                      "&Excedentes=" & Chk_Exce.Checked & "&Cuentas=" & Chk_Cuentas.Checked & _
                      "&GestionDoctos=" & Chk_Ges.Checked & "", "Documentos a Verificar", 1100, 750, 50, 50)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpiar()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Modificar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Modificar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20010805, Usr, "PRESIONA BOTON MODIFICAR DOCUMENTOS") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim sw As Integer = 0
            Dim cb As New CheckBox
            For i = 0 To GV_Documentos.Rows.Count - 1
                cb = CType(GV_Documentos.Rows(i).FindControl("ChkB_CarDocto"), CheckBox)
                If cb.Checked Then
                    sw = 1
                    Exit For
                End If
            Next

            If sw = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione un documento", ClsMensaje.TipoDeMensaje._Exclamacion, , True)
                Exit Sub
            End If

            MlPopupExt_ModDoctos.Show()
            'Dim Posicion As Integer = Txt_PosGv.Value + 1
            'Dim Nro As Integer = (CInt(Sesion_Op.Coll_DOC(CInt(Posicion)).cli_idc))
            'Response.Write("<script>window.open('ModDoctos.aspx?Nro=','popup','width=800,height=500') </script>")
            'IB_Modificar.Attributes.Add("onClick", "WinOpen(2,'ModDoctos.aspx?Nro='" & (CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_idc)) & ",'PopUpModDoctos',630,420,200,150)")
            'EjecutaJScript(Me, "var x=window.showModalDialog('ModDoctos.aspx?Nro= " & (CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_idc)) & "', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:450px;dialogLeft:400px;dialogTop:200px');")
            'FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "../../ModDoctos.aspx?Nro=" & Nro & "", "Modificar Documento", 630, 630, 100, 100)
            'AbrePopup(Me, 1, "ModDoctos.aspx?Nro=" & 147852411 & "&Posi=" & 1 & "", "blablabla", 630, 420, 400, 200)
            'Response.Redirect("ModDoctos.aspx?Nro=" & (CInt(Sesion_Op.Coll_DOC(CInt(Posicion)).cli_idc)) & "&Posi=" & Posicion, False)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Documentos.Rows.Count < 10 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Documentos.Rows.Count = 10 Then

            NroPaginacion += 10
            NroPagina += 1

            LlenaGrilla()

        End If

        'Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)
        'CargaGrilla(1)

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion > 1 Then

            IB_Next.Enabled = True

            NroPaginacion -= 10
            NroPagina -= 1

            LlenaGrilla()
            'Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)
            'CargaGrilla(1)

        Else
            Msj.Mensaje(Me.Page, Caption, "No existe pagina anterior", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub IB_Prev_Detalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Detalle.Click
        Select Case TabCntr_DetalleDocto.ActiveTabIndex
            Case 0 'otorgamientos
                If page_otg = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_otg >= 6 Then
                    page_otg -= 6
                    TraeDetalleDoctos()
                End If
            Case 1 'Recaudacion
                If page_recau = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_recau >= 6 Then
                    page_recau -= 6
                    TraeDetalleDoctos()
                End If


            Case 2 'Excedentes
                If page_exc = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_exc >= 6 Then
                    page_exc -= 6
                    TraeDetalleDoctos()
                End If
            Case 3 ' cuentas
                If page_cuen = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_cuen >= 6 Then
                    page_cuen -= 6
                    TraeDetalleDoctos()
                End If
            Case 4 ' Gestion documentos

                If page_gestion_doc = 0 Then
                    Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_gestion_doc >= 6 Then
                    page_gestion_doc -= 6
                    TraeDetalleDoctos()
                End If

        End Select
    End Sub

    Protected Sub IB_Next_Detalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Detalle.Click
        Select Case TabCntr_DetalleDocto.ActiveTabIndex
            Case 0 'otorgamientos
                If GV_Otorgamiento.Rows.Count < 6 Then
                    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()

                    Exit Sub
                End If

                If GV_Otorgamiento.Rows.Count = 6 Then
                    page_otg += 6
                    TraeDetalleDoctos()
                End If

            Case 1 'Recaudacion
                If GV_Recaudacion.Rows.Count < 6 Then
                    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_recau = 6 Then
                    page_recau += 6
                    TraeDetalleDoctos()
                End If
            Case 2 'Excedentes
                If GV_Excedentes.Rows.Count < 6 Then
                    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_exc = 6 Then
                    page_exc += 6
                    TraeDetalleDoctos()
                End If

            Case 3 ' cuentas
                If GV_Cuentas.Rows.Count < 6 Then
                    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_cuen = 6 Then
                    page_cuen += 6
                    TraeDetalleDoctos()
                End If
            Case 4 ' Gestion documentos
                If GV_Gestion.Rows.Count < 6 Then
                    Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                    MlPopupExt_DetalleDoctos.Show()
                    Exit Sub
                End If

                If page_gestion_doc = 6 Then
                    page_gestion_doc += 6
                    TraeDetalleDoctos()

                End If

        End Select
    End Sub

#End Region

#Region "Funciones y Procedimientos Generales"


    Private Sub LimpiaControlesModificaDocto()
        Txt_Rut_Cli2.Text = ""
        Txt_Dig_Cli2.Text = ""
        Txt_Nom_Cli2.Text = ""
        Txt_Rut_Deu2.Text = ""
    End Sub


    'Se agrega dropDownlist con Sucursales
    Private Sub LlenaDrop()

        Try
            CG.SucursalesDevuelve(CodEje, True, Me.DropSucursal)
            CG.EjecutivosDevuelve(Dp_Ejecu, CodEje, 15)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Moneda)
            CG.ParametrosDevuelve(TablaParametro.EstadoDocumento, True, Dp_Est)
            cbz.CobranzaEstadosDevuelve(Dp_EstCob)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub Limpiar()
        Try
            ChKB_Suc.Checked = True
            DropSucursal.ClearSelection()
            DropSucursal.Enabled = False
            Rb_Eje.Checked = True
            Dp_Ejecu.ClearSelection()
            Dp_Ejecu.Enabled = False
            ChKB_Cli.Checked = False
            ChKB_Deu.Checked = False
            Txt_Rut_Cli.Text = ""
            Txt_Rut_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.Text = ""
            Txt_Dig_Cli.ReadOnly = True
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Raz_Soc.Text = ""
            Txt_Rut_Deu.Text = ""
            Txt_Rut_Deu.ReadOnly = True
            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.Text = ""
            Txt_Dig_Deu.ReadOnly = True
            Txt_Dig_Deu.CssClass = "clsDisabled"
            Txt_Rso_Deu.Text = ""
            Rb_Est.Checked = True
            Dp_Est.ClearSelection()
            Rb_Mon.Checked = True
            Dp_Moneda.ClearSelection()
            Rb_Cob.Checked = True
            Dp_Cobranza.ClearSelection()
            Txt_Oto_Dsd.Text = ""
            Txt_Oto_Hta.Text = ""
            Txt_Doc_Dsd.Text = ""
            Txt_Doc_Hta.Text = ""
            Txt_Fec_Ini.Text = ""
            Txt_Fec_Fin.Text = ""
            Txt_Fec_OtoDsd.Text = ""
            Txt_Fec_OtoHta.Text = ""
            Rb_Res.Checked = True
            Dp_Res.ClearSelection()
            Dp_Cobranza.ClearSelection()
            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()
            GV_Otorgamiento.DataSource = Nothing
            GV_Otorgamiento.DataBind()
            GV_Recaudacion.DataSource = Nothing
            GV_Recaudacion.DataBind()
            GV_Excedentes.DataSource = Nothing
            GV_Excedentes.DataBind()
            GV_Cuentas.DataSource = Nothing
            GV_Cuentas.DataBind()
            GV_Gestion.DataSource = Nothing
            GV_Gestion.DataBind()

            Chk_Oto.Checked = False
            Chk_Oto.Enabled = False

            ChK_Rec.Checked = False
            ChK_Rec.Enabled = False

            Chk_Exce.Checked = False
            Chk_Exce.Enabled = False

            Chk_Cuentas.Checked = False
            Chk_Cuentas.Enabled = False

            Chk_Ges.Checked = False
            Chk_Ges.Enabled = False

            Dp_EstCob.ClearSelection()
            hf_exc.Value = ""
            hf_ges.Value = ""
            hf_otor.Value = ""
            hf_otr_cta.Value = ""
            hf_rec.Value = ""
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Txt_Rut_Deu_MaskedEditExtender.Enabled = False
            LimpiaControlesModificaDocto()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Function ValidaBlancos() As Boolean
        Try
            'Nro Operación
            If Trim(Txt_Oto_Dsd.Text) <> "" And Trim(Txt_Oto_Hta.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar nro. operación hasta", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Oto_Hta.Focus()
                Exit Function
            End If

            If Trim(Txt_Oto_Dsd.Text) = "" And Trim(Txt_Oto_Hta.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar nro. operación desde", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Oto_Dsd.Focus()
                Exit Function
            End If

            If Trim(Txt_Oto_Dsd.Text) <> "" And Trim(Txt_Oto_Hta.Text) <> "" Then
                If CInt(Txt_Oto_Dsd.Text) > CInt(Txt_Oto_Hta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Nro. operación hasta no puede ser menor a nro. operación desde", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Oto_Hta.Focus()
                    Exit Function
                End If
            End If

            'Nro Documento
            If Trim(Txt_Doc_Dsd.Text) <> "" And Trim(Txt_Doc_Hta.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar nro. documento hasta", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Doc_Hta.Focus()
                Exit Function
            End If

            If Trim(Txt_Doc_Dsd.Text) = "" And Trim(Txt_Doc_Hta.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar nro. documento desde", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Doc_Dsd.Focus()
                Exit Function
            End If

            If Trim(Txt_Doc_Dsd.Text) <> "" And Trim(Txt_Doc_Hta.Text) <> "" Then
                If CInt(Txt_Doc_Dsd.Text) > CInt(Txt_Doc_Hta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Nro. documento hasta no puede ser menor a nro. documento desde", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Doc_Hta.Focus()
                    Exit Function
                End If
            End If

            'Fecha de vencimiento.



            If Trim(Txt_Fec_Ini.Text) <> "" And Trim(Txt_Fec_Fin.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha vencimiento hasta", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_Fin.Focus()
                Exit Function
            End If

            If Not isdate(Txt_Fec_Fin.Text) And Txt_Fec_Fin.Text <> "" And Txt_Fec_Fin.Text = "__/__/____" Then
                Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento hasta erronea", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_Fin.Focus()
                Exit Function
            End If


            If Trim(Txt_Fec_Ini.Text) = "" And Trim(Txt_Fec_Fin.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha vencimiento desde", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_Ini.Focus()
                Exit Function
            End If

            If Not isdate(Txt_Fec_Ini.Text) And Txt_Fec_Ini.Text <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento desde erronea", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_Ini.Focus()
                Exit Function
            End If

            If Txt_Fec_Fin.Text <> "" Then
                If Not IsDate(Txt_Fec_Fin.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento hasta erronea", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Fec_Fin.Text = ""
                    Exit Function
                End If
            End If
            If Trim(Txt_Fec_Ini.Text) <> "" And Trim(Txt_Fec_Fin.Text) <> "" Then
                If CDate(Txt_Fec_Ini.Text) > CDate(Txt_Fec_Fin.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento hasta no puede ser menor a Fecha vencimiento desde", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Fec_Ini.Focus()
                    Exit Function
                End If
            End If

            'Fecha de otorgamiento.
            If Trim(Txt_Fec_OtoDsd.Text) <> "" And Trim(Txt_Fec_OtoHta.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha otorgamiento hasta", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_OtoHta.Focus()
                Exit Function
            End If

            If Trim(Txt_Fec_OtoDsd.Text) = "" And Trim(Txt_Fec_OtoHta.Text) <> "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha otorgamiento desde", TipoDeMensaje._Exclamacion)
                ValidaBlancos = False
                Txt_Fec_OtoDsd.Focus()
                Exit Function
            End If

            If Trim(Txt_Fec_OtoDsd.Text) <> "" And Trim(Txt_Fec_OtoHta.Text) <> "" Then
                If Not IsDate(Txt_Fec_OtoDsd.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha otorgamiento desde erronea", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Fec_OtoDsd.Text = ""
                    Exit Function
                End If

                If Not IsDate(Txt_Fec_OtoHta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha otorgamiento hasta erronea", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Fec_OtoHta.Text = ""
                    Exit Function
                End If

                If CDate(Txt_Fec_OtoDsd.Text) > CDate(Txt_Fec_OtoHta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento hasta no puede ser menor a Fecha vencimiento desde", TipoDeMensaje._Exclamacion)
                    ValidaBlancos = False
                    Txt_Fec_OtoDsd.Focus()
                    Exit Function
                End If
            End If

            ValidaBlancos = True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Function

    Private Sub LlenaGrilla()

        Try

            Dim Res1 As Int16, Res2 As Int16
            Dim Mon1 As Int16, Mon2 As Int16
            Dim Suc1 As String, Suc2 As String
            Dim Ope1 As String, Ope2 As String
            Dim Doc1 As String, Doc2 As String
            Dim Tip1 As String, Tip2 As String
            Dim Eje1 As Integer, Eje2 As Integer
            Dim Est1 As Integer, Est2 As Integer
            Dim RutDeu As String, RutCli As String
            Dim RutDeu2 As String, RutCli2 As String
            Dim ConCob1 As String, ConCob2 As String
            Dim FecVto1 As String, FecVto2 As String
            Dim FecOto1 As String, FecOto2 As String
            Dim CodCob As Integer, TipoConsulta As Int16

            If ChKB_Suc.Checked Then
                Suc1 = "000"
                Suc2 = "999"
            Else
                Suc1 = DropSucursal.SelectedValue
                Suc2 = DropSucursal.SelectedValue
                'Suc1 = Sesion.Sucursal
                'Suc2 = Sesion.Sucursal
            End If

            If Dp_Ejecu.SelectedValue = 0 Then
                Eje1 = "0000"
                Eje2 = "9999"
            Else
                Eje1 = Dp_Ejecu.SelectedValue
                Eje2 = Dp_Ejecu.SelectedValue
            End If

            If Rb_Est.Checked Then
                Est1 = 0
                Est2 = 999
            Else
                Est1 = Dp_Est.SelectedValue
                Est2 = Dp_Est.SelectedValue
            End If


            If Rb_Mon.Checked Then
                Mon1 = 0
                Mon2 = 9999
            Else
                Mon1 = Dp_Moneda.SelectedValue
                Mon2 = Dp_Moneda.SelectedValue
            End If

            'Fecha Vencimiento
            If Trim(Txt_Fec_Ini.Text) = "" Then
                FecVto1 = "1800/01/01"
                FecVto2 = "9999/12/31"
            Else
                If Not IsDate(Txt_Fec_Ini.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha vcto. desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                'Txt_Fec_Ini
                If CDate(Txt_Fec_Ini.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, Caption, "Fecha vcto. desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_Ini.Text = ""
                    Exit Sub
                End If

                If CDate(Txt_Fec_Fin.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, Caption, "Fecha vcto. hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_Fin.Text = ""
                    Exit Sub
                End If

                If Not IsDate(Txt_Fec_Fin.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha vcto. hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_Fin.Text = ""
                    Exit Sub
                End If
                FecVto1 = Format(CDate(Txt_Fec_Ini.Text), "yyyy/MM/dd")
                FecVto2 = Format(CDate(Txt_Fec_Fin.Text), "yyyy/MM/dd")
            End If

            'Fecha de Operación
            If Trim(Txt_Fec_OtoDsd.Text) = "" Then
                FecOto1 = "1800/01/01"
                FecOto2 = "9999/12/31"
            Else
                If Not IsDate(Txt_Fec_OtoDsd.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha otorgamiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoDsd.Text = ""
                    Exit Sub
                End If

                If CDate(Txt_Fec_OtoDsd.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, Caption, "Fecha otorgamiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoDsd.Text = ""
                    Exit Sub
                End If

                If Not IsDate(Txt_Fec_OtoHta.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha otorgamiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoHta.Text = ""
                    Exit Sub
                End If

                If CDate(Txt_Fec_OtoHta.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, Caption, "Fecha otorgamiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoHta.Text = ""
                    Exit Sub
                End If

                FecOto1 = Format(CDate(Txt_Fec_OtoDsd.Text), "yyyy/MM/dd")
                FecOto2 = Format(CDate(Txt_Fec_OtoHta.Text), "yyyy/MM/dd" & " 23:59:59")
            End If

            If ChKB_Cli.Checked Then
                Tip1 = 1
            Else
                Tip1 = 2
            End If

            If Trim(Txt_Oto_Dsd.Text) = "" Then
                Ope1 = "0000000000"
                Ope2 = "9999999999"
            Else
                Ope1 = Txt_Oto_Dsd.Text
                Ope2 = Txt_Oto_Dsd.Text
            End If

            If Trim(Txt_Doc_Dsd.Text) = "" Then
                Doc1 = "0"
                Doc2 = "Z"
            Else
                Doc1 = Txt_Doc_Dsd.Text.Trim
                Doc2 = Txt_Doc_Hta.Text.Trim
            End If

            If ChKB_Cli.Checked Then
                RutCli = Format(CLng(Txt_Rut_Cli.Text), "000000000000")
                RutCli2 = Format(CLng(Txt_Rut_Cli.Text), "000000000000")
            Else
                RutCli = "0"
                RutCli2 = "9999999999999"
            End If

            If ChKB_Deu.Checked Then
                RutDeu = Format(CLng(Txt_Rut_Deu.Text), "000000000000")
                'RutDeu2 = Format(CLng(Txt_Rut_Deu.Text), "000000000000")
            Else
                RutDeu = "0"
            End If

            CodCob = IIf(Dp_EstCob.SelectedValue = "", 0, Dp_EstCob.SelectedValue)

            If Rb_Cob.Checked Then
                ConCob1 = "S"
                ConCob2 = "N"
            Else
                If Dp_Cobranza.SelectedValue > 0 Then
                    If Dp_Cobranza.SelectedValue = 1 Then
                        ConCob1 = "S"
                        ConCob2 = "S"
                    Else
                        ConCob1 = "N"
                        ConCob2 = "N"
                    End If
                Else
                    ConCob1 = "S"
                    ConCob2 = "N"
                End If
            End If

            If Rb_Res.Checked Then
                Res1 = 0
                Res2 = 1
            Else
                If Dp_Res.SelectedValue > 0 Then
                    If Dp_Res.SelectedValue = 1 Then
                        Res1 = 0
                        Res2 = 0
                    Else
                        Res1 = 1
                        Res2 = 1
                    End If
                Else
                    Res1 = 0
                    Res2 = 1
                End If
            End If

            If RutCli <> "0" And RutDeu <> "0" And CodCob <> 0 Then TipoConsulta = 1
            If RutCli <> "0" And RutDeu <> "0" And CodCob = 0 Then TipoConsulta = 2
            If RutCli <> "0" And RutDeu = "0" And CodCob <> 0 Then TipoConsulta = 3
            If RutCli <> "0" And RutDeu = "0" And CodCob = 0 Then TipoConsulta = 4
            If RutCli = "0" And RutDeu <> "0" And CodCob <> 0 Then TipoConsulta = 5
            If RutCli = "0" And RutDeu <> "0" And CodCob = 0 Then TipoConsulta = 6
            If RutCli = "0" And RutDeu = "0" And CodCob <> 0 Then TipoConsulta = 7
            If RutCli = "0" And RutDeu = "0" And CodCob = 0 Then TipoConsulta = 8



            Sesion_Op.Coll_DOC = OP.OperacionCartolaDocumentos_Retorna(TipoConsulta, RutCli, RutCli2, RutDeu, _
                                                                                     Suc1, Suc2, _
                                                                                     Eje1, Eje2, _
                                                                                     Mon1, Mon2, _
                                                                                     FecVto1, FecVto2, _
                                                                                     Est1, Est2, _
                                                                                     Ope1, Ope2, _
                                                                                     Doc1, Doc2, _
                                                                                     CodCob, ConCob1, ConCob2, _
                                                                                     Res1, Res2, _
                                                                                     FecOto1, FecOto2, _
                                                                                     True, GV_Documentos)
            If Not IsNothing(Sesion_Op.Coll_DOC) Then
                If Sesion_Op.Coll_DOC.Count > 0 Then
                    FormatoGrillaCartolaDoctos()
                Else
                    Msj.Mensaje(Page, Caption, "No se encontraron documentos", ClsMensaje.TipoDeMensaje._Exclamacion)
                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub FormatoGrillaCartolaDoctos()
        Try

            For i = 1 To Sesion_Op.Coll_DOC.Count

                GV_Documentos.Rows(i - 1).Cells(1).Text = Format(CDbl(Sesion_Op.Coll_DOC(i).cli_idc), FMT.FCMSD) & "-" & FC.Vrut(CLng(Sesion_Op.Coll_DOC(i).cli_idc))
                GV_Documentos.Rows(i - 1).Cells(3).Text = Format(CDbl(Sesion_Op.Coll_DOC(i).deu_ide), FMT.FCMSD) & "-" & FC.Vrut(CLng(Sesion_Op.Coll_DOC(i).deu_ide))

                If Sesion_Op.Coll_DOC(i).id_P_0023 = 1 Then
                    GV_Documentos.Rows(i - 1).Cells(8).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(9).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto_fin, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_cli, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(15).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_ddr, FMT.FCMSD)
                ElseIf 2 Then
                    GV_Documentos.Rows(i - 1).Cells(8).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(9).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto_fin, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_cli, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(15).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_ddr, FMT.FCMCD4)
                Else
                    GV_Documentos.Rows(i - 1).Cells(8).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(9).Text = Format(Sesion_Op.Coll_DOC(i).dsi_mto_fin, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_cli, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(15).Text = Format(Sesion_Op.Coll_DOC(i).doc_sdo_ddr, FMT.FCMCD)
                End If

                GV_Documentos.Rows(i - 1).Cells(10).Text = Format(Sesion_Op.Coll_DOC(i).dsi_fev_ori, "dd/MM/yyyy")
                GV_Documentos.Rows(i - 1).Cells(11).Text = Format(Sesion_Op.Coll_DOC(i).dsi_fev_rea, "dd/MM/yyyy")

                GV_Documentos.Rows(i - 1).Cells(1).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(3).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(5).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(6).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(7).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(8).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(9).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(14).HorizontalAlign = HorizontalAlign.Right
                GV_Documentos.Rows(i - 1).Cells(15).HorizontalAlign = HorizontalAlign.Right

            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoGrillaOtorgamiento(ByVal Coll_Otorgamiento As Collection)

        Try

            For i = 1 To Coll_Otorgamiento.Count

                GV_Otorgamiento.Rows(i - 1).Cells(2).Text = Format(Coll_Otorgamiento(i).ope_fec_sim, "dd/MM/yyyy")
                GV_Otorgamiento.Rows(i - 1).Cells(3).Text = Format(Coll_Otorgamiento(i).opo_fec_oto, "dd/MM/yyyy")

                If Coll_Otorgamiento(i).id_P_0023 = 1 Then
                    GV_Otorgamiento.Rows(i - 1).Cells(7).Text = Format(Coll_Otorgamiento(i).dsi_mto, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(8).Text = Format(Coll_Otorgamiento(i).dsi_mto_ant, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(9).Text = Format(Coll_Otorgamiento(i).ope_tot_gir, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(10).Text = Format(Coll_Otorgamiento(i).ope_pre_com, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(11).Text = Format(Coll_Otorgamiento(i).ope_dif_pre, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(12).Text = Format(Coll_Otorgamiento(i).ope_sal_pen, FMT.FCMSD)
                    GV_Otorgamiento.Rows(i - 1).Cells(13).Text = Format(Coll_Otorgamiento(i).ope_sal_pag, FMT.FCMSD)
                ElseIf 2 Then
                    GV_Otorgamiento.Rows(i - 1).Cells(7).Text = Format(Coll_Otorgamiento(i).dsi_mto, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(8).Text = Format(Coll_Otorgamiento(i).dsi_mto_ant, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(9).Text = Format(Coll_Otorgamiento(i).ope_tot_gir, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(10).Text = Format(Coll_Otorgamiento(i).ope_pre_com, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(11).Text = Format(Coll_Otorgamiento(i).ope_dif_pre, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(12).Text = Format(Coll_Otorgamiento(i).ope_sal_pen, FMT.FCMCD4)
                    GV_Otorgamiento.Rows(i - 1).Cells(13).Text = Format(Coll_Otorgamiento(i).ope_sal_pag, FMT.FCMCD4)
                Else
                    GV_Otorgamiento.Rows(i - 1).Cells(7).Text = Format(Coll_Otorgamiento(i).dsi_mto, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(8).Text = Format(Coll_Otorgamiento(i).dsi_mto_ant, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(9).Text = Format(Coll_Otorgamiento(i).ope_tot_gir, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(10).Text = Format(Coll_Otorgamiento(i).ope_pre_com, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(11).Text = Format(Coll_Otorgamiento(i).ope_dif_pre, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(13).Text = Format(Coll_Otorgamiento(i).ope_sal_pen, FMT.FCMCD)
                    GV_Otorgamiento.Rows(i - 1).Cells(10).Text = Format(Coll_Otorgamiento(i).ope_sal_pag, FMT.FCMCD)
                End If

                GV_Otorgamiento.Rows(i - 1).Cells(0).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(1).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(4).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(6).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(7).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(8).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(9).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(10).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(11).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(12).HorizontalAlign = HorizontalAlign.Right
                GV_Otorgamiento.Rows(i - 1).Cells(13).HorizontalAlign = HorizontalAlign.Right

            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub FormatoGrillaRecaudacion(ByVal Coll_Recaudacion As Collection)

        Try

            For i = 1 To Coll_Recaudacion.Count
                GV_Recaudacion.Rows(i - 1).Cells(0).Text = Format(Coll_Recaudacion(i).ing_fec, "dd/MM/yyyy")
                If GV_Recaudacion.Rows(i - 1).Cells(4).Text = "C" Then
                    GV_Recaudacion.Rows(i - 1).Cells(4).Text = "Cliente"
                ElseIf GV_Recaudacion.Rows(i - 1).Cells(4).Text = "D" Then
                    GV_Recaudacion.Rows(i - 1).Cells(4).Text = "Deudor"
                ElseIf GV_Recaudacion.Rows(i - 1).Cells(4).Text = "E" Then
                    GV_Recaudacion.Rows(i - 1).Cells(4).Text = "Excedentes"
                ElseIf GV_Recaudacion.Rows(i - 1).Cells(4).Text = "O" Then
                    GV_Recaudacion.Rows(i - 1).Cells(4).Text = "Operación"
                End If

                If Coll_Recaudacion(i).id_P_0023 = 1 Then
                    GV_Recaudacion.Rows(i - 1).Cells(7).Text = Format(Coll_Recaudacion(i).ing_mto_int, FMT.FCMSD)
                    GV_Recaudacion.Rows(i - 1).Cells(8).Text = Format(Coll_Recaudacion(i).ing_rea_mon, FMT.FCMSD)
                    GV_Recaudacion.Rows(i - 1).Cells(9).Text = Format(Coll_Recaudacion(i).ing_mto_tot, FMT.FCMSD)
                    GV_Recaudacion.Rows(i - 1).Cells(10).Text = Format(Coll_Recaudacion(i).ing_mto_abo, FMT.FCMSD)
                    GV_Recaudacion.Rows(i - 1).Cells(13).Text = Format(Coll_Recaudacion(i).doc_sdo_cli, FMT.FCMSD)

                ElseIf 2 Then
                    GV_Recaudacion.Rows(i - 1).Cells(7).Text = Format(Coll_Recaudacion(i).ing_mto_int, FMT.FCMCD4)
                    GV_Recaudacion.Rows(i - 1).Cells(8).Text = Format(Coll_Recaudacion(i).ing_rea_mon, FMT.FCMCD4)
                    GV_Recaudacion.Rows(i - 1).Cells(9).Text = Format(Coll_Recaudacion(i).ing_mto_tot, FMT.FCMCD4)
                    GV_Recaudacion.Rows(i - 1).Cells(10).Text = Format(Coll_Recaudacion(i).ing_mto_abo, FMT.FCMCD4)
                    GV_Recaudacion.Rows(i - 1).Cells(13).Text = Format(Coll_Recaudacion(i).doc_sdo_cli, FMT.FCMCD4)
                Else
                    GV_Recaudacion.Rows(i - 1).Cells(7).Text = Format(Coll_Recaudacion(i).ing_mto_int, FMT.FCMCD)
                    GV_Recaudacion.Rows(i - 1).Cells(8).Text = Format(Coll_Recaudacion(i).ing_rea_mon, FMT.FCMCD)
                    GV_Recaudacion.Rows(i - 1).Cells(9).Text = Format(Coll_Recaudacion(i).ing_mto_tot, FMT.FCMCD)
                    GV_Recaudacion.Rows(i - 1).Cells(10).Text = Format(Coll_Recaudacion(i).ing_mto_abo, FMT.FCMCD)
                    GV_Recaudacion.Rows(i - 1).Cells(13).Text = Format(Coll_Recaudacion(i).doc_sdo_cli, FMT.FCMCD)

                End If

                GV_Recaudacion.Rows(i - 1).Cells(2).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(3).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(7).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(8).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(9).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(10).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(11).HorizontalAlign = HorizontalAlign.Right
                GV_Recaudacion.Rows(i - 1).Cells(13).HorizontalAlign = HorizontalAlign.Right

            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub FormatoGrillaExcedentes(ByVal Coll_Excedentes As Collection)
        Try
            For i = 1 To Coll_Excedentes.Count
                GV_Excedentes.Rows(i - 1).Cells(0).Text = Format(Coll_Excedentes(i).egr_fec, "dd/MM/yyyy")

                'If Coll_Excedentes(i).id_P_0023 = 1 Then
                '    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(Coll_Excedentes(i).ing_rea_mon, FMT.FCMSD)
                'ElseIf 2 Then
                '    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(Coll_Excedentes(i).ing_rea_mon, FMT.FCMCD4)
                'Else
                '    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(Coll_Excedentes(i).ing_rea_mon, FMT.FCMCD)
                'End If

                If Coll_Excedentes(i).id_P_0023 = 1 Then
                    GV_Excedentes.Rows(i - 1).Cells(6).Text = Format(CDbl(Coll_Excedentes(i).egr_mto), FMT.FCMSD)
                    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(CDbl(Coll_Excedentes(i).dsi_mto), FMT.FCMSD)
                ElseIf 2 Then
                    GV_Excedentes.Rows(i - 1).Cells(6).Text = Format(CDbl(Coll_Excedentes(i).egr_mto), FMT.FCMCD4)
                    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(CDbl(Coll_Excedentes(i).dsi_mto), FMT.FCMSD)
                Else
                    GV_Excedentes.Rows(i - 1).Cells(6).Text = Format(CDbl(Coll_Excedentes(i).egr_mto), FMT.FCMCD)
                    GV_Excedentes.Rows(i - 1).Cells(4).Text = Format(CDbl(Coll_Excedentes(i).dsi_mto), FMT.FCMSD)
                End If

                GV_Excedentes.Rows(i - 1).Cells(2).HorizontalAlign = HorizontalAlign.Right
                GV_Excedentes.Rows(i - 1).Cells(3).HorizontalAlign = HorizontalAlign.Right
                GV_Excedentes.Rows(i - 1).Cells(4).HorizontalAlign = HorizontalAlign.Right
                GV_Excedentes.Rows(i - 1).Cells(6).HorizontalAlign = HorizontalAlign.Right
            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub FormatoGrillaOtrasCuentas(ByVal Coll_Cuentas As Collection)
        Try
            For i = 1 To Coll_Cuentas.Count
                GV_Cuentas.Rows(i - 1).Cells(0).Text = Format(Coll_Cuentas(i).cxc_fec, "dd/MM/yyyy")
                GV_Cuentas.Rows(i - 1).Cells(5).Text = Format(Coll_Cuentas(i).cxc_ful_pgo, "dd/MM/yyyy")

                If Coll_Cuentas(i).id_P_0023 = 1 Then
                    GV_Cuentas.Rows(i - 1).Cells(3).Text = Format(Coll_Cuentas(i).cxc_mto, FMT.FCMSD)
                    GV_Cuentas.Rows(i - 1).Cells(4).Text = Format(Coll_Cuentas(i).cxc_sal, FMT.FCMSD)
                ElseIf 2 Then
                    GV_Cuentas.Rows(i - 1).Cells(3).Text = Format(Coll_Cuentas(i).cxc_mto, FMT.FCMCD4)
                    GV_Cuentas.Rows(i - 1).Cells(4).Text = Format(Coll_Cuentas(i).cxc_sal, FMT.FCMCD4)
                Else
                    GV_Cuentas.Rows(i - 1).Cells(3).Text = Format(Coll_Cuentas(i).cxc_mto, FMT.FCMCD)
                    GV_Cuentas.Rows(i - 1).Cells(4).Text = Format(Coll_Cuentas(i).cxc_sal, FMT.FCMCD)
                End If
                GV_Cuentas.Rows(i - 1).Cells(2).HorizontalAlign = HorizontalAlign.Right
                GV_Cuentas.Rows(i - 1).Cells(3).HorizontalAlign = HorizontalAlign.Right
                GV_Cuentas.Rows(i - 1).Cells(4).HorizontalAlign = HorizontalAlign.Right
            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub FormatoGrillaGestion(ByVal Coll_GestionDocto As Collection)
        Try
            For i = 1 To Coll_GestionDocto.Count
                GV_Gestion.Rows(i - 1).Cells(0).Text = Format(Coll_GestionDocto(i).gsn_fec, "dd/MM/yyyy")
                GV_Gestion.Rows(i - 1).Cells(1).Text = Format(Coll_GestionDocto(i).gsn_hor, "HH:MM")
                GV_Gestion.Rows(i - 1).Cells(3).Text = Format(Coll_GestionDocto(i).gsn_fec_pag, "dd/MM/yyyy")
                GV_Gestion.Rows(i - 1).Cells(4).Text = Format(Coll_GestionDocto(i).gsn_hor_pag, "HH:MM")
                GV_Gestion.Rows(i - 1).Cells(5).Text = Format(Coll_GestionDocto(i).gsn_fec_prx, "dd/MM/yyyy")
                GV_Gestion.Rows(i - 1).Cells(6).Text = Format(Coll_GestionDocto(i).gsn_hor_prx, "HH:MM")
            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub TraeDetalleDoctos()



        Try
            LimpiaControlesModificaDocto()
            'Cartola Documento
            Txt_Rut_Cli_2.Text = CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_idc)
            Txt_Dig_Cli_2.Text = FC.Vrut(Txt_Rut_Cli_2.Text)

            Txt_Tip_Doc.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).tipo_docto
            Txt_Num_Doc.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_num
            Txt_Cuota.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_flj_num

            Txt_Raz_SocCli.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_rso
            Txt_Raz_SocDeu.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).deu_rso

            Txt_Fec_Ven.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_fev_ori
            Txt_Fec_Pag.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).doc_ful_pgo

            'Modificación Documento
            Txt_Rut_Cli2.Text = CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_idc)
            Txt_Dig_Cli2.Text = FC.Vrut(Txt_Rut_Cli2.Text)
            Txt_Nom_Cli2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).cli_rso
            Txt_Rut_Deu2.Text = CInt(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).deu_ide)
            Txt_Dig_Deu2.Text = FC.Vrut(Txt_Rut_Deu2.Text)
            Txt_Nom_Deu2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).deu_rso
            Txt_Nro_Ope2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_ope
            Txt_Nro_Oto2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).opo_otg
            Txt_Tip_Doc2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).tipo_docto
            Txt_Est_Doc.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).Est_Docto
            Txt_Fec_Vto.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_fev_ori

            Txt_Mon.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).moneda

            If Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_P_0023 = 1 Then
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_mto, FMT.FCMSD)
            ElseIf Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_P_0023 = 2 Then
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_mto, FMT.FCMCD4)
            Else
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_mto, FMT.FCMCD)
            End If

            Txt_Nro_Doc.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_num
            Txt_Cuota2.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_flj_num
            Txt_Cod_Flj.Text = Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_flj

            coll_ope_otg = New Collection
            coll_ope_otg = OP.OperacionDetalleDoctos(1, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_dsi)

            Coll_Cobranza = New Collection
            Coll_Cobranza = OP.OperacionDetalleDoctos(2, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_dsi)

            Coll_EXC_Seleccionados = New Collection
            Coll_EXC_Seleccionados = OP.OperacionDetalleDoctos(3, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_dsi)

            coll_CXC = New Collection
            coll_CXC = OP.OperacionDetalleDoctos(4, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_dsi)

            coll_Gto = New Collection
            coll_Gto = OP.OperacionDetalleDoctos(5, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).id_dsi)


            If coll_ope_otg.Count = 0 Then
                Chk_Oto.Checked = False
            Else
                Chk_Oto.Checked = True

                hf_otor.Value = 1
                GV_Otorgamiento.DataSource = coll_ope_otg
                GV_Otorgamiento.DataBind()
                FormatoGrillaOtorgamiento(coll_ope_otg)
            End If

            If Coll_Cobranza.Count = 0 Then
                ChK_Rec.Enabled = False
            Else
                hf_rec.Value = 1

                ChK_Rec.Enabled = True
                GV_Recaudacion.DataSource = Coll_Cobranza
                GV_Recaudacion.DataBind()
                FormatoGrillaRecaudacion(Coll_Cobranza)
            End If

            If Coll_EXC_Seleccionados.Count = 0 Then
                Chk_Exce.Enabled = False
            Else
                hf_exc.Value = 0

                Chk_Exce.Enabled = True
                GV_Excedentes.DataSource = Coll_EXC_Seleccionados
                GV_Excedentes.DataBind()
                FormatoGrillaExcedentes(Coll_EXC_Seleccionados)
            End If

            If coll_CXC.Count = 0 Then
                Chk_Cuentas.Enabled = False
            Else
                hf_otr_cta.Value = 1

                Chk_Cuentas.Enabled = True
                GV_Cuentas.DataSource = coll_CXC
                GV_Cuentas.DataBind()
                FormatoGrillaOtrasCuentas(coll_CXC)

            End If

            If coll_Gto.Count = 0 Then
                Chk_Ges.Enabled = False
            Else
                hf_ges.Value = 1

                Chk_Ges.Enabled = True
                GV_Gestion.DataSource = coll_Gto
                GV_Gestion.DataBind()
                FormatoGrillaGestion(coll_Gto)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub MarcaGrilla()

        'For i = 0 To GV_Documentos.Rows.Count - 1
        '    GV_Documentos.Rows(i).CssClass = "formatable"

        '    If Val(Txt_PosGv.Value) >= 0 Then
        '        GV_Documentos.Rows(Val(Txt_PosGv.Value)).CssClass = "clicktable"
        '    Else
        '        GV_Documentos.Rows(i).CssClass = "formatable"
        '    End If
        'Next

        For I = 0 To GV_Documentos.Rows.Count - 1

            If (Val(Txt_PosGv.Value) = I And Txt_PosGv.Value <> "") Then

                If (I Mod 2) = 0 Then
                    GV_Documentos.Rows(I).CssClass = "selectable"
                Else
                    GV_Documentos.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    GV_Documentos.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Documentos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

#End Region

#Region "LinkButton"

    Protected Sub LB_Valida_FechasVto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Valida_FechasVto.Click
        Try
            If Trim(Txt_Fec_Ini.Text) <> "" Then
                If Not IsDate(Txt_Fec_Ini.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha desde incorrecta", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Ini.Focus()
                    Exit Sub
                End If
            End If
            If Trim(Txt_Fec_Fin.Text) <> "" Then
                If Not IsDate(Txt_Fec_Fin.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha hasta incorrecta", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Fin.Focus()
                    Exit Sub
                End If
            End If

            If Trim(Txt_Fec_Ini.Text) <> "" And Trim(Txt_Fec_Fin.Text) <> "" Then
                If CDate(Txt_Fec_Fin.Text) < CDate(Txt_Fec_Ini.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha desde no puede ser mayor a fecha desde", TipoDeMensaje._Exclamacion)
                    Txt_Fec_Ini.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_Valida_FechasOto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Valida_FechasOto.Click
        Try
            If Trim(Txt_Fec_OtoDsd.Text) <> "" Then
                If Not IsDate(Txt_Fec_OtoDsd.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha otorgamiento desde incorrecta", TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoDsd.Focus()
                    Exit Sub
                End If
            End If
            If Trim(Txt_Fec_OtoHta.Text) <> "" Then
                If Not IsDate(Txt_Fec_OtoHta.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha otorgamiento hasta incorrecta", TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoHta.Focus()
                    Exit Sub
                End If
            End If

            If Trim(Txt_Fec_OtoDsd.Text) <> "" And Trim(Txt_Fec_OtoHta.Text) <> "" Then
                If CDate(Txt_Fec_OtoHta.Text) < CDate(Txt_Fec_OtoDsd.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Fecha desde no puede ser mayor a fecha desde", TipoDeMensaje._Exclamacion)
                    Txt_Fec_OtoDsd.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_DetalleDoctos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            TraeDetalleDoctos()
            MarcaGrilla()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            Dim Cli As cli_cls

            Cli = clasecli.ClientesDevuelve(Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT), Me.Txt_Dig_Cli.Text)
            If valida_cliente <> "" Then
                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Else

                If IsNothing(Cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If


                If Cli.id_P_0044 <> 1 Then
                    Txt_Raz_Soc.Text = Trim(Cli.cli_rso)
                Else
                    Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)
                End If
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.IB_AyudaCli.Enabled = False
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            If ChKB_Deu.Checked Then
                If Trim(Txt_Rut_Deu.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu.Focus()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu.Text).ToUpper <> FC.Vrut(Trim(Txt_Rut_Deu.Text)).ToUpper Then
                    Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT))
                If IsNothing(deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Text = ""
                    Txt_Dig_Deu.Text = ""
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.IB_AyudaDeu.Enabled = False
            End If
            'ChKB_Deu
            If ChkB_Deudor.Checked Then
                If Trim(Txt_Rut_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) <> FC.Vrut(Trim(Txt_Rut_Deu2.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT))
                If IsNothing(deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                Txt_Nom_Deu2.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub




    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try

            Dim StrMsj As String

            StrMsj = OP.OperacionModificaDocto(Format(CLng(Txt_Rut_Cli2.Text), Var.FMT_RUT), Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT), _
                                               Format(CLng(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).deu_ide), Var.FMT_RUT), _
                                               Txt_Nro_Ope2.Text, Txt_Nro_Doc.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_num, _
                                               Txt_Cuota2.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_flj_num)

            'StrMsj = OP.OperacionModificaDocto(Format(CLng(Txt_Rut_Cli2.Text), Var.FMT_RUT), Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT), _
            '                                   Format(CLng(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value)).deu_ide), Var.FMT_RUT), _
            '                                   Txt_Nro_Ope2.Text, Txt_Nro_Doc.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value)).dsi_num, _
            '                                   Txt_Cuota2.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value)).dsi_flj_num)

            LlenaGrilla()
            MarcaGrilla()

            Msj.Mensaje(Me.Page, Caption, StrMsj, TipoDeMensaje._Exclamacion)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub L_cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles L_cli.Click
        Try

            Dim Cli As cli_cls

            Cli = clasecli.ClientesDevuelve(Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT), Txt_Dig_Cli.Text)

            If valida_cliente <> "" Then
                Msj.Mensaje(Me, "Atencion", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Else

                If Cli.id_P_0044 <> 1 Then
                    Txt_Raz_Soc.Text = Trim(Cli.cli_rso)
                Else
                    Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub L_deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles L_deu.Click
        Try
            If ChKB_Deu.Checked Then
                If Trim(Txt_Rut_Deu.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu.Focus()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu.Text).ToUpper <> FC.Vrut(Trim(Txt_Rut_Deu.Text)).ToUpper Then
                    Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT))
                If IsNothing(deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu.Focus()
                    Exit Sub
                End If
                Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            End If

            If ChkB_Deudor.Checked Then
                If Trim(Txt_Rut_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de duedor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu2.Focus()
                    MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) <> FC.Vrut(Trim(Txt_Rut_Deu2.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT))
                If IsNothing(deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                Txt_Nom_Deu2.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub LB_BuscaDeudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDeudor.Click
        Try
            If Txt_Rut_Deu.Text.Trim = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar un Deudor", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If UCase(Txt_Dig_Deu.Text) <> UCase(FC.Vrut(Txt_Rut_Deu.Text)) Then
                Msj.Mensaje(Me, Caption, "Rut Incorrecto del Deudor", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Deu As deu_cls

            Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

            If Not IsNothing(Deu) Then


                If Deu.id_P_0044 = 1 Then
                    'Txt_Rso_Deu.Text = Deu.deu_nom.Trim & " " & Deu.deu_ape_ptn.Trim & "" & Deu.deu_ape_mtn.Trim
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim & " " & Deu.deu_ape_ptn.Trim & "" & Deu.deu_ape_mtn.Trim
                Else
                    Txt_Rso_Deu.Text = Deu.deu_rso.Trim
                End If
                'Txt_Mto_Doc_MaskedEditExtender.Enabled = True
            Else
                Msj.Mensaje(Me, Caption, "No Existe Deudor", TipoDeMensaje._Exclamacion)
                Txt_Rso_Deu.CssClass = "clsMandatorio"
                Txt_Rso_Deu.ReadOnly = False
            End If





        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
