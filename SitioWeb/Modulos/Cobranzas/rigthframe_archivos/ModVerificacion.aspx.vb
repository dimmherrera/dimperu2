Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_ModVerificacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim Caption As String = "Modificación de Doctos a Verificar"
    Dim Var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FC As New FuncionesGenerales.FComunes
    Dim Sesion As ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim agt As New Perfiles.Cls_Principal
    Dim Msj As New ClsMensaje
    Dim CBZ As New ClaseCobranza

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            Dim MtoDsd As String, MtoHta As String
            Dim FecDsd As String, FecHta As String
            Dim NroOpe As String, NroDoc As String
            Dim RB_Ver As Boolean, EstVer As Integer
            Dim TipDoc As String, TipMon As Integer


            If Not IsPostBack Then

                Sesion.RutCli = Request.QueryString("RutCli")
                Sesion.RutDeu = Request.QueryString("RutDeu")

                Dim Deudor As deu_cls
                Dim c As dvf_cls
                Txt_Rut_Deu.Text = Format(CLng(Sesion.RutDeu), FMT.FCMSD)

                Deudor = CG.DeudorDevuelvePorRut(Sesion.RutDeu)

                Txt_Raz_Deu.Text = Deudor.deu_rso
                Txt_Dv_Deu.Text = Deudor.deu_dig_ito

                MtoDsd = Request.QueryString("MtoDsd")
                MtoHta = Request.QueryString("MtoHta")
                FecDsd = Request.QueryString("fecDsd")
                FecHta = Request.QueryString("FecHta")
                NroOpe = Request.QueryString("Nroope")
                NroDoc = Request.QueryString("NroDoc")
                RB_Ver = Request.QueryString("RB_Ver")
                EstVer = Request.QueryString("EstVer")
                TipDoc = Request.QueryString("TipDoc")
                TipMon = Request.QueryString("TipMon")

                HF_Tip_Doc.Value = TipDoc
                CargaContactos()

                CargaDrop()

                If Trim(FecDsd) = "" And Trim(FecHta) = "" Then
                    FecDsd = "01/01/1900"
                    FecHta = "31/12/9999"
                End If

                DoctosAVerificarDevuelve(MtoDsd, MtoHta, FecDsd, FecHta, NroOpe, NroDoc, RB_Ver, EstVer, TipDoc, TipMon)

                Txt_Fec_Veri.Text = Format(Date.Now, "dd/MM/yyyy")

                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub GV_DoctosVerificar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_DoctosVerificar.RowDataBound
        Try
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "CelClick_GV_ModDoctosDvf(GV_DoctosVerificar, 'clicktable', 'formatable', 'selectable')")
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub Dp_Raz_Cnt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Raz_Cnt.SelectedIndexChanged

        Dim C As cnc_cls
        Dim CLSCLI As New ClaseClientes

        C = CLSCLI.ContactosDevuelve(Variables.TipoDeContacto.Deudor, Txt_Rut_Deu.Text, Dp_Raz_Cnt.SelectedValue)

        If Not IsNothing(C) Then
            Txt_Fono_Cnt.Text = C.cnc_tel
            Txt_Fax_Cnt.Text = C.cnc_fax
            Txt_Mail_Cnt.Text = C.cnc_ema
        End If

    End Sub

    Protected Sub lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chb As Boolean
        Dim cb As CheckBox

        chb = False

        For i = 0 To Me.GV_DoctosVerificar.Rows.Count - 1
            cb = GV_DoctosVerificar.Rows(i).FindControl("ChckB_GV_Veri")
            If cb.Checked = True Then
                chb = True
            End If
        Next

        If chb = False Then
            Msj.Mensaje(Me.Page, Caption, "Seleccione un Documento", ClsMensaje.TipoDeMensaje._Exclamacion)
        Else
            UpdateDoctos()
        End If

    End Sub

    Protected Sub GV_DoctosVerificar_SelectedIndexChanged(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GV_DoctosVerificar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "CelClick_GV_Doctosprueba(GV_DoctosVerificar, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click
        CargaContactos()
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Contactos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Contactos.Click

        Try
            Sesion.TipoDeContacto = 2
            AbrePopup(Me, 2, "../../Contactos/Contactos.aspx?Rut=" & Txt_Rut_Deu.Text, "Contactos", 550, 650, 100, 100)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub IB_Dias_Pago_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Dias_Pago.Click

        Try
            AbrePopup(Me, 2, "DiasDePago.aspx?RutCli=" & Sesion.RutCli & "&RutDeu=" & Txt_Rut_Deu.Text & "", "Días de Pago", 470, 260, 300, 300)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            If Not agt.ValidaAccesso(20, 20060107, Usr, "PRESIONO GUARDAR DOCTO. MODIFICACION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Dp_Raz_Cnt.SelectedValue = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione contacto", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            If Txt_Fec_LLeg.Text <> "" Then
                If Not IsDate(Txt_Fec_LLeg.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha llegada a cobranza errónea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                    Exit Sub
                End If
            End If

            If Txt_Fec_Veri.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese fecha de verificación", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If
            If Not IsDate(Txt_Fec_Veri.Text) Then
                Msj.Mensaje(Page, Caption, "Fecha verificación errónea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            If Txt_Fec_Pag.Text <> "" Then
                If Not IsDate(Txt_Fec_Pag.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha de pago errónea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                    Exit Sub
                End If
            End If


            If Txt_Fec_Ges.Text <> "" Then
                If Not IsDate(Txt_Fec_Ges.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha primera gestión errónea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                    Exit Sub
                End If
            End If

            If Dp_Est_Veri.SelectedValue = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione estado de verificación", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            Msj.Mensaje(Me, "Atención", "¿Desea Guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_gua.UniqueID, False)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub IB_Obs_Deudor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Obs_Deudor.Click

        Try
            AbrePopup(Me, 2, "ObservacionesDeudor.aspx?RutCli=" & Sesion.RutCli & "&RutDeu=" & Txt_Rut_Deu.Text & "", "Observación Deudor", 350, 320, 300, 300)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

        Try
            ClosePag(Me.Page)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

#End Region

#Region "Procedimientos y Funciones Generales"

    Private Sub DoctosAVerificarDevuelve(ByVal MtoDsd1 As String, ByVal MtoHta2 As String, ByVal FecDsd1 As Date, ByVal FecHta2 As Date, _
                                                                         ByVal Nro_Ope As String, ByVal Nro_Doc As String, ByVal RB_Ver1 As Boolean, ByVal Est_Ver As Integer, _
                                                                         ByVal Tip_Doc As Integer, ByVal Tip_Mon As Integer)

        Dim Mto1 As Double, Mto2 As Double
        Dim Fec1 As String, Fec2 As String
        Dim NroOpe1 As Long, NroOpe2 As Long
        Dim NroDoc1 As String, nrodoc2 As String
        Dim EstVer1 As Integer, EstVer2 As Integer
        Dim TipDoc1 As Integer, TipDoc2 As Integer
        Dim RBVer As Boolean

        Try
            'Rango Monto
            If Trim(MtoDsd1) = "" Then
                Mto1 = 0
                Mto2 = CDbl("9999999999")
            Else
                Mto1 = CDbl(MtoDsd1)
                Mto2 = CDbl(MtoHta2)
            End If

            'Rango de Fechas
            If Trim(FecDsd1) = "" Then
                Fec1 = "01/01/1900"
                Fec2 = "01/01/9999"
            Else
                Fec1 = Format(FecDsd1, "dd/MM/yyyy")
                Fec2 = Format(FecHta2, "dd/MM/yyyy")
            End If

            'Rango por Nro operación
            If Trim(Nro_Ope) = "" Then
                NroOpe1 = 0
                NroOpe2 = 9999999999
            Else
                NroOpe1 = CLng(Nro_Ope)
                NroOpe2 = CLng(Nro_Ope)
            End If

            'Rango por Nro Documento
            If Trim(Nro_Doc) = "" Then
                NroDoc1 = "0"
                NroDoc2 = 999999999
            Else
                NroDoc1 = CLng(Nro_Doc)
                NroDoc2 = CLng(Nro_Doc)
            End If

            'Rango para estado de verificación
            If RB_Ver1 = False And Est_Ver = 0 Then
                EstVer1 = 0
                EstVer2 = 999999
            End If

            If RB_Ver1 = True And Est_Ver = 0 Then
                EstVer1 = 0
                EstVer2 = 999999
            End If

            If RB_Ver1 = False And Est_Ver <> 0 Then
                EstVer1 = CInt(Est_Ver)
                EstVer2 = CInt(Est_Ver)
            End If

            'Rango para Tipo Documento
            If Tip_Doc = 0 Then
                TipDoc1 = "0"
                TipDoc2 = 999999
            Else
                TipDoc1 = CInt(Tip_Doc)
                TipDoc2 = CInt(Tip_Doc)
            End If

            Sesion.coll_Ver = CBZ.DocumentoAVerificar_Devuelve(Sesion.RutCli, Sesion.RutDeu, Mto1, Mto2, Fec1, Fec2, NroOpe1, NroOpe2, _
                                                              NroDoc1, NroDoc2, EstVer1, EstVer2, TipDoc1, TipDoc2, Tip_Mon, True, GV_DoctosVerificar)

            FormatoGrillaDoctosAVerificar()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub FormatoGrillaDoctosAVerificar()
        For i = 1 To Sesion.coll_Ver.Count
            GV_DoctosVerificar.Rows(i - 1).Cells(4).Text = Format(Sesion.coll_Ver(i).FecVto, "dd/MM/yyyy")
            GV_DoctosVerificar.Rows(i - 1).Cells(5).Text = Format(CLng(Sesion.coll_Ver(i).monto), FMT.FCMSD)
        Next
    End Sub

    Private Sub CargaContactos()
        Try
            Dim Cnc As New Object
            Dim CLSCLI As New ClaseClientes

            Cnc = CLSCLI.ContactosDevuelveTodos(Variables.TipoDeContacto.Deudor, Txt_Rut_Deu.Text, False)

            RW.Llenar_Drop(Cnc, "id_cnc", "cnc_nom", Dp_Raz_Cnt)

            For Each C In Cnc
                If C.cnc_def = "S" Then
                    Dp_Raz_Cnt.SelectedValue = C.id_cnc
                    Txt_Fono_Cnt.Text = C.cnc_tel
                    Txt_Fax_Cnt.Text = C.cnc_fax
                    Txt_Mail_Cnt.Text = C.cnc_ema
                End If
            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Sub CargaDrop()
        Try
            CG.ParametrosDevuelve(TablaParametro.EstadoVerificacion, True, Dp_Est_Veri)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Sub UpdateDoctos()

        Try
            Dim clsope As New ClaseOperaciones
            Dim sw As Integer = 0

            For a = 0 To GV_DoctosVerificar.Rows.Count - 1

                If CType(GV_DoctosVerificar.Rows(a).FindControl("ChckB_GV_Veri"), CheckBox).Checked Then

                    Dim Dvf As New dvf_cls

                    With Dvf

                        'Validamos si el documento existe en una operacion distinta a ingresado
                        If Not clsope.Documentos_verificar_valida_DSI(CInt(Sesion.coll_Ver(a + 1).NroDoc), _
                                                                  Txt_Rut_Deu.Text, _
                                                                  CInt(Sesion.coll_Ver(a + 1).TipoDoc)) Then
                            Msj.Mensaje(Me, Caption, "Documento Nº " & Sesion.coll_Ver(a + 1).NroDoc & " existe en una operacion con estado " & clsope.EstadoOperacion, TipoDeMensaje._Informacion)
                            Exit Sub
                        End If

                        '.deu_ide = CLng(Txt_Rut_Deu.Text.Replace(".", ""))
                        .deu_ide = Format(CLng(Txt_Rut_Deu.Text.Replace(".", "")), Var.FMT_RUT)
                        .cli_idc = Format(CLng(Sesion.RutCli), Var.FMT_RUT)
                        .dvf_num = CInt(Sesion.coll_Ver(a + 1).NroDoc)
                        .id_P_0031 = CInt(Sesion.coll_Ver(a + 1).TipoDoc)
                        .dvf_mto = Sesion.coll_Ver(a + 1).monto
                        'Estado Verificación
                        .id_P_0040 = Dp_Est_Veri.SelectedValue

                        'Fecha de vencimiento
                        .dvf_fev = Sesion.coll_Ver(a + 1).FecVto
                        '.ddv_cls.ddv_fev = Sesion.coll_Ver(a).FecVto


                        'Observación
                        .dvf_obs = Txt_Obs_Docto.Text

                        'Fecha de llegada
                        If Trim(Txt_Fec_LLeg.Text) = "" Then
                            .dvf_fec_lle = Nothing
                        Else

                            'Hora de llegada
                            If Trim(Txt_Hor_LLeg.Text) = "" Then
                                .dvf_hor_lle = Nothing
                            Else
                                .dvf_hor_lle = Txt_Hor_LLeg.Text
                            End If

                            .dvf_fec_lle = Txt_Fec_LLeg.Text '& " " & Txt_Hor_LLeg.Text

                        End If

                        'Hora de llegada
                        If Trim(Txt_Hor_LLeg.Text) = "" Then
                            .dvf_hor_lle = Nothing
                        Else
                            .dvf_hor_lle = Txt_Hor_LLeg.Text
                        End If

                        'Fecha de verificación
                        If Trim(Txt_Fec_Veri.Text) = "" Then
                            .dvf_fec_vfc = Nothing
                        Else

                            'Hora de verificación
                            If Trim(Txt_Hor_Veri.Text) = "" Then
                                .dvf_hor_vfc = Nothing
                                .dvf_fec_vfc = Txt_Fec_Veri.Text
                            Else
                                .dvf_fec_vfc = Txt_Fec_Veri.Text '& " " & Txt_Hor_Veri.Text
                            End If

                        End If

                        'Hora de verificación
                        If Trim(Txt_Hor_Veri.Text) = "" Then
                            .dvf_hor_vfc = Nothing
                        Else
                            .dvf_hor_vfc = Txt_Hor_Veri.Text
                        End If

                        'Fecha de pago
                        If Trim(Txt_Fec_Pag.Text) = "" Then
                            '.dvf_hor_vfc = Nothing
                            .dvf_fec_pag = Nothing
                        Else
                            .dvf_fec_pag = Txt_Fec_Pag.Text
                            If Txt_Hor_Veri.Text = "" Then
                                .dvf_hor_vfc = Nothing
                            Else

                                .dvf_hor_vfc = Txt_Hor_Veri.Text
                            End If
                        End If

                        'Fecha de Primera Gestión
                        If Trim(Txt_Fec_Ges.Text) = "" Then
                            .dvf_fec_pri_gsn = Nothing
                        Else
                            .dvf_fec_pri_gsn = Txt_Fec_Ges.Text
                        End If

                        'Código de Quién Verifica
                        .id_eje_dvf = Sesion.CodEje

                        'Codigo contacto
                        .id_cnc = Dp_Raz_Cnt.SelectedValue

                    End With

                    If CBZ.DocumentoAVerificarUpdate(Dvf) Then
                        sw = 1
                        'Msj.Mensaje(Me.Page, Caption, "Los Cambios fueron realizados", TipoDeMensaje._Informacion, Nothing, False)
                    End If

                Else

                End If
            Next


            If sw = 1 Then
                Msj.Mensaje(Me.Page, Caption, "Los Cambios fueron realizados", TipoDeMensaje._Informacion, Nothing, False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

#Region "Link Button"

    Protected Sub LB_Valida_Fechas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Valida_Fechas.Click
        Try
            If Trim(Txt_Hor_LLeg.Text) <> "" Then
                If Not IsDate(Trim(Txt_Hor_LLeg.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Hora de llegada incorrecta", TipoDeMensaje._Exclamacion, Nothing, False)
                    Txt_Hor_LLeg.Focus()
                    Exit Sub
                End If
            End If

            If Trim(Txt_Hor_Veri.Text) <> "" Then
                If Not IsDate(Trim(Txt_Hor_Veri.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Hora de verificación incorrecta", TipoDeMensaje._Exclamacion, Nothing, False)
                    Txt_Hor_Veri.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub lb_id_doc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_id_doc.Click

        Try

            Dim Dvf As New dvf_cls

            Dim I As Integer = HF_pos_grilla.Value

            Dvf = CBZ.rescata_dvf(coll_Ver.Item(I).deu, _
                                  coll_Ver.Item(I).dsi_num, _
                                  coll_Ver.Item(I).monto, _
                                  coll_Ver.Item(I).TipoDoc)

            Txt_Obs_Docto.Text = Dvf.dvf_obs
            Txt_Fec_LLeg.Text = IIf(IsDBNull(Dvf.dvf_fec_lle), "", Format(CDate(Dvf.dvf_fec_lle), "dd/MM/yyyy"))
            Txt_Fec_Ges.Text = IIf(IsDBNull(Dvf.dvf_fec_pri_gsn), "", Format(CDate(Dvf.dvf_fec_pri_gsn), "dd/MM/yyyy"))
            Txt_Fec_Veri.Text = IIf(IsDBNull(Dvf.dvf_fec_vfc), "", Format(CDate(Dvf.dvf_fec_vfc), "dd/MM/yyyy"))
            Txt_Fec_Pag.Text = IIf(IsDBNull(Dvf.dvf_fec_pag), "", Format(CDate(Dvf.dvf_fec_pag), "dd/MM/yyyy"))
            Txt_Hor_LLeg.Text = IIf(IsDBNull(Dvf.dvf_hor_lle), "", Format(CDate(Dvf.dvf_hor_lle).ToShortTimeString, "hh:mm"))
            Txt_Hor_Veri.Text = IIf(IsDBNull(Dvf.dvf_hor_vfc), "", Format(CDate(Dvf.dvf_hor_vfc).ToShortTimeString, "hh:mm"))
            Dp_Est_Veri.SelectedValue = Dvf.id_P_0040

        Catch ex As Exception
        End Try

    End Sub

#End Region


   
    
End Class
