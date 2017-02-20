Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports ClsSession.SesionPagos
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_Mod_doctos_otg
    Inherits System.Web.UI.Page
    Dim sesion As New ClsSession.ClsSession

    Dim cg As New ConsultasGenerales
    Dim clasecli As New ClaseClientes
    Dim ag As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim var As New FuncionesGenerales.Variables
    Dim fc As New FuncionesGenerales.FComunes
    Dim Posicion As Integer = 0
    Dim msj As New ClsMensaje
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim OP As New ClaseOperaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Me.IsPostBack Then

                valida_cliente = ""
                hf_nro_pag.Value = 1
                Coll_DOC = New Collection
                Coll_Doctos_Seleccionados = New Collection
                arreglo = New ArrayList

                cg.ParametrosDevuelve(31, True, Me.dr_tip_doc)
                cg.ParametrosDevuelve(11, True, Me.dr_est_doc)

                page_dig = 0
                Valida_Riesgo()
                alinea_textos()

            End If

            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',580,410,200,150);")
            ib_ayudacli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_Cal_Arr.Attributes.Add("onClick", "WinOpen(2, 'Pop_up_MasivaCal.aspx', 'CalificaciónArrastre',650, 230, 15, 15);")

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub Valida_Riesgo()

        If cg.ValidaP_Riesgo(CodEje) Then
            IB_Calif.Visible = True
            IB_Cal_Arr.Visible = True
        Else
            IB_Calif.Visible = False
            IB_Cal_Arr.Visible = False
        End If

        IB_Calif.Enabled = False
        IB_Cal_Arr.Enabled = False

    End Sub

    Public Sub alinea_textos()
        Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        '    Me.txt_mto_comi.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_doc_des.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_oto_des.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")


    End Sub

    Private Sub buscar()

        Try

            Dim rut_cli As Long, rut_cli1 As Long, SUC1 As Integer, suc2 As Integer, eje1 As String, eje2 As String, _
            nro_otg1 As Integer, nro_otg2 As Int64, mon1 As String, mon2 As String, estado As Integer, estado1 As Integer, _
            nro_doc1 As String, nro_doc2 As String, fec_otg As String, fec_otg1 As String, tipo, rut_deu As Long, _
            rut_deu1 As Long, fec_vcto1 As DateTime, fec_vcto2 As DateTime, cobr1 As String, cobr2 As String, obl As String, _
            obl2 As String, TipoDocto_Dsd As Integer, TipoDocto_Hst As Integer

            Dim coll As New Collection

            If Me.Ch_cli.Checked Then

                If Me.Txt_Rut_Cli.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Txt_Dig_Cli.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese dígito cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If UCase(Txt_Dig_Cli.Text) <> fc.Vrut(Me.Txt_Rut_Cli.Text) Then
                    msj.Mensaje(Me, "Atencion", "Dígito cliente ncorrecto", 2)
                    Exit Sub
                End If

                rut_cli = Me.Txt_Rut_Cli.Text
                rut_cli1 = Me.Txt_Rut_Cli.Text

            Else
                rut_cli = 0
                rut_cli1 = 99999999999999
            End If

            If Me.Ch_deu.Checked Then

                If Me.Txt_Rut_Deu.text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese NIT Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Txt_Dig_Deu.text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese dígito Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If UCase(Txt_Dig_Deu.Text) <> fc.Vrut(Me.Txt_Rut_Deu.Text) Then
                    msj.Mensaje(Me, "Atencion", "Dígito Pagador Incorrecto", 2)
                    Exit Sub
                End If

                rut_deu = CLng(Me.Txt_Rut_Deu.Text)
                rut_deu1 = CLng(Me.Txt_Rut_Deu.Text)
            Else
                rut_deu = 0
                rut_deu1 = 99999999999999
            End If

            'Sucursal
            SUC1 = 1
            suc2 = 1
            'Ejecutivo
            eje1 = 0
            eje2 = 9999
            'Moneda
            mon1 = 0
            mon2 = 9999
            'Responsabilidad
            cobr1 = "S"
            cobr2 = "N"

            If Me.Ch_cli.Checked = 1 Then
                tipo = 1
            Else
                tipo = 2
            End If

            If dr_est_doc.SelectedValue = 0 Then
                estado = 0
                estado1 = 999

            Else
                estado = Me.dr_est_doc.SelectedValue
                estado1 = Me.dr_est_doc.SelectedValue

            End If

            If Trim(Me.txt_oto_des.Text) = "" Then
                nro_otg1 = 0
                nro_otg2 = 9999999999
            Else
                nro_otg1 = CDbl(txt_oto_des.Text)
                nro_otg2 = CDbl(txt_oto_des.Text)
            End If

            If Trim(Me.txt_doc_des.Text) = "" Then
                nro_doc1 = "0"
                nro_doc2 = "Z" '99999
            Else
                nro_doc1 = txt_doc_des.Text.Trim
                nro_doc2 = txt_doc_des.Text.Trim
            End If

            'Fecha Otorgamiento
            '**************************************************************************************************
            If Trim(txt_fec_des.Text) <> "" Then
                If Not IsDate(txt_fec_des.Text) Then
                    msj.Mensaje(Page, "Atencion", "Fecha de otorgamiento desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_des.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(txt_fec_has.Text) <> "" Then
                If Not IsDate(txt_fec_has.Text) Then
                    msj.Mensaje(Page, "Atencion", "Fecha de otorgamiento hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_has.Text = ""
                    Exit Sub
                End If

            End If

            If Trim(Me.txt_fec_des.Text) = "" Or Trim(Me.txt_fec_has.Text) = "" Then
                fec_otg = "01/01/1900"
                fec_otg1 = "31/12/2999"
            Else

                If CDate(txt_fec_des.Text) > CDate(txt_fec_has.Text) Then
                    msj.Mensaje(Page, "Atención", "Fecha de otorgamiento desde no puede ser mayor a fecha de otorgamiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                fec_otg = txt_fec_des.Text & " 00:00:00"
                fec_otg1 = txt_fec_has.Text & " 23:59:59"

            End If

            '**************************************************************************************************
            '**************************************************************************************************
            'Fecha de vencimiento
            '**************************************************************************************************

            If Trim(txt_venc_des.Text) <> "" Then
                If Not IsDate(txt_venc_des.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.txt_venc_des.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(txt_venc_has.Text) <> "" Then
                If Not IsDate(txt_venc_has.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.txt_venc_has.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(Me.txt_venc_des.Text) = "" Or Trim(Me.txt_venc_has.Text) = "" Then
                fec_vcto1 = CDate("01/01/1900")
                fec_vcto2 = CDate("31/12/9999")
            Else
                fec_vcto1 = txt_venc_des.Text & " 00:00:00"
                fec_vcto2 = txt_venc_has.Text & " 23:59:59"
            End If


            If CDate(fec_vcto1) > CDate(fec_vcto2) Then
                msj.Mensaje(Page, "Atención", "Fecha de vencimiento desde no puede ser mayor a fecha de vencimiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Ch_obl.Checked Then
                If Me.rb_con_obl.SelectedValue = "S" Then
                    obl = "S"
                    obl2 = "S"
                Else
                    obl = "N"
                    obl2 = "N"
                End If
            Else
                obl = "S"
                obl2 = "N"
            End If

            If dr_tip_doc.SelectedValue = 0 Then
                TipoDocto_Dsd = 0
                TipoDocto_Hst = 9999
            Else
                TipoDocto_Dsd = dr_tip_doc.SelectedValue
                TipoDocto_Hst = dr_tip_doc.SelectedValue
            End If

            NroRow = 0

            If dr_est_doc.SelectedValue <> 0 Then

                Coll_DOC = New Collection

                If txt_Contrato.Text.Trim = "" Then
                    Coll_DOC = OP.DocumentosOtorgados_a_Modificar_Retorna(rut_cli, rut_cli1, rut_deu, rut_deu1, 0, 99999999, _
                            TipoDocto_Dsd, TipoDocto_Hst, nro_doc1, nro_doc2, 0, 9999, fec_vcto1, fec_vcto2, estado, estado1, _
                            estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, nro_otg1, _
                            nro_otg2, cobr1, cobr2, fec_otg, fec_otg1, obl, obl2)
                Else
                    Coll_DOC = OP.DocumentosOtorgados_a_Modificar_Retorna2(rut_cli, rut_cli1, rut_deu, rut_deu1, 0, 99999999, _
                            TipoDocto_Dsd, TipoDocto_Hst, nro_doc1, nro_doc2, 0, 9999, fec_vcto1, fec_vcto2, estado, estado1, _
                            estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, estado1, nro_otg1, _
                            nro_otg2, cobr1, cobr2, fec_otg, fec_otg1, obl, obl2, txt_Contrato.Text.ToUpper.Trim)
                End If

                Me.Gr_Documentos.DataSource = Coll_DOC

            Else

                Coll_DOC = New Collection

                If txt_Contrato.Text.Trim = "" Then
                    Coll_DOC = OP.DocumentosOtorgados_a_Modificar_Retorna(rut_cli, rut_cli1, rut_deu, rut_deu1, 0, 99999999, _
                               TipoDocto_Dsd, TipoDocto_Hst, nro_doc1, nro_doc2, 0, 9999, fec_vcto1, fec_vcto2, 1, 2, 3, _
                               4, 4, 6, 7, 8, 9, 10, 11, 12, nro_otg1, nro_otg2, cobr1, cobr2, fec_otg, fec_otg1, obl, obl2)
                Else
                    Coll_DOC = OP.DocumentosOtorgados_a_Modificar_Retorna2(rut_cli, rut_cli1, rut_deu, rut_deu1, 0, 99999999, _
                               TipoDocto_Dsd, TipoDocto_Hst, nro_doc1, nro_doc2, 0, 9999, fec_vcto1, fec_vcto2, 1, 2, 3, _
                               4, 4, 6, 7, 8, 9, 10, 11, 12, nro_otg1, nro_otg2, cobr1, cobr2, fec_otg, fec_otg1, obl, obl2, txt_Contrato.Text.ToUpper.Trim)
                End If

                Me.Gr_Documentos.DataSource = Coll_DOC

            End If

            If hf_nro_pag.Value = 1 Then
                Dim nro As Integer
                Dim nro_aux As Decimal

                nro = CInt(NroRow / 15)
                nro_aux = NroRow / 15

                If nro_aux > nro Then
                    nro_aux = nro + 1
                End If
                paginas.Text = "Pagina 1 de " & IIf(CInt(NroRow / 15) = 0, 1, Round(nro_aux, 0))
                paginas.CssClass = "Label"
            End If

            Me.Gr_Documentos.DataBind()

            If Me.Gr_Documentos.Rows.Count > 0 Then

                For i = 0 To Me.Gr_Documentos.Rows.Count - 1

                    Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD) & "-" & RG.Vrut(Me.Gr_Documentos.Rows(i).Cells(3).Text)
                    Me.Gr_Documentos.Rows(i).Cells(1).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(Me.Gr_Documentos.Rows(i).Cells(1).Text)
                    'Gr_Documentos.Rows(i).Cells(7).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(7).Text), fmt.FCMSD)

                    If Gr_Documentos.Rows(i).Cells(16).Text = "S" Then
                        Gr_Documentos.Rows(i).Cells(16).Text = "SI"
                    Else
                        Gr_Documentos.Rows(i).Cells(16).Text = "NO"
                    End If

                    If Gr_Documentos.Rows(i).Cells(17).Text = "S" Then
                        Gr_Documentos.Rows(i).Cells(17).Text = "SI"
                    Else
                        Gr_Documentos.Rows(i).Cells(17).Text = "NO"
                    End If
                    Gr_Documentos.Rows(i).Cells(15).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(15).Text), fmt.FCMSD)

                    Gr_Documentos.Rows(i).Cells(14).Text = Format(CDbl(Gr_Documentos.Rows(i).Cells(14).Text), fmt.FCMSD)
                    Gr_Documentos.Rows(i).Cells(2).Text = Trim(Me.Gr_Documentos.Rows(i).Cells(2).Text).ToUpper()

                    'No muestra documentos renovados
                    If Gr_Documentos.Rows(i).Cells(10).Text = "RENOVADO" Then
                        Gr_Documentos.Rows(i).Visible = False
                    End If

                Next

            End If

            If Me.Gr_Documentos.Rows.Count = 0 Then

                msj.Mensaje(Me, "Atención", "No se encontraron registros según criterios ", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            Else
                Me.btn_clasif.Enabled = True
                Ib_modificar.Enabled = True
                Ib_imprimir.Enabled = True
                'IB_Calif.Enabled = True
                IB_Cal_Arr.Enabled = True
            End If

            habilita_criterios(False)

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub habilita_criterios(ByVal Estado As Boolean)

        If Estado Then

            'Cliente 

            Ch_cli.Checked = False
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.ReadOnly = True

            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True

            Txt_Raz_Soc.Text = ""
            ib_ayudacli.Enabled = False
            'Deudor 

            Ch_deu.Checked = False
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True

            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True

            Me.Txt_Rso_Deu.Text = ""
            IB_AyudaDeu.Enabled = False
            'Otros Criterios 

            Me.txt_oto_des.Text = ""
            Me.txt_oto_des.ReadOnly = False
            Me.txt_oto_des.CssClass = "clsTxt"


            Me.txt_doc_des.Text = ""
            Me.txt_doc_des.ReadOnly = False
            Me.txt_doc_des.CssClass = "clsTxt"

            Me.Ch_obl.Checked = False
            Me.Ch_obl.Enabled = True

            rb_con_obl.ClearSelection()

            dr_est_doc.CssClass = "clsTxt"
            dr_est_doc.Enabled = True
            dr_est_doc.SelectedValue = 0

            dr_tip_doc.CssClass = "clsTxt"
            dr_tip_doc.Enabled = True
            dr_tip_doc.SelectedValue = 0

            txt_venc_des.Text = ""
            Me.txt_venc_des_CalendarExtender.Enabled = True
            Me.txt_venc_des.ReadOnly = False
            txt_venc_des.CssClass = "clsTxt"

            txt_venc_has.Text = ""
            Me.txt_venc_has_CalendarExtender.Enabled = True
            Me.txt_venc_has.ReadOnly = False
            txt_venc_has.CssClass = "clsTxt"


            txt_fec_des.Text = ""
            Me.txt_fec_des_CalendarExtender.Enabled = True
            Me.txt_fec_des.ReadOnly = False
            txt_fec_des.CssClass = "clsTxt"

            txt_Contrato.Text = ""
            Me.txt_Contrato.ReadOnly = False
            Me.txt_Contrato.CssClass = "clsTxt"


            txt_fec_has.Text = ""
            Me.txt_fec_has_CalendarExtender.Enabled = True
            Me.txt_fec_has.ReadOnly = False
            txt_fec_has.CssClass = "clsTxt"
            rb_con_obl.Enabled = False
            btn_buscar.Enabled = True
            Txt_deu_Cli_MaskedEditExtender.Enabled = False
            Txt_deu_Cli_MaskedEditExtender5.Enabled = False
        Else



            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.ReadOnly = True


            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True



            'Deudor 


            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True


            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True



            'Otros Criterios 


            Me.txt_oto_des.ReadOnly = True
            Me.txt_oto_des.CssClass = "clsDisabled"



            Me.txt_doc_des.ReadOnly = True
            Me.txt_doc_des.CssClass = "clsDisabled"

            Me.Ch_obl.Enabled = False



            dr_est_doc.CssClass = "clsDisabled"
            dr_est_doc.Enabled = False

            dr_tip_doc.CssClass = "clsDisabled"
            dr_tip_doc.Enabled = False


            Me.txt_venc_des_CalendarExtender.Enabled = False
            Me.txt_venc_des.ReadOnly = True
            Me.txt_venc_des.CssClass = "clsDisabled"

            Me.txt_venc_has_CalendarExtender.Enabled = False
            Me.txt_venc_has.ReadOnly = True
            Me.txt_venc_has.CssClass = "clsDisabled"


            Me.txt_fec_des_CalendarExtender.Enabled = False
            Me.txt_fec_des.ReadOnly = True
            Me.txt_fec_des.CssClass = "clsDisabled"

            Me.txt_fec_has_CalendarExtender.Enabled = False
            Me.txt_fec_has.ReadOnly = True
            Me.txt_fec_has.CssClass = "clsDisabled"

            Me.txt_Contrato.ReadOnly = True
            Me.txt_Contrato.CssClass = "clsDisabled"

            btn_buscar.Enabled = False

        End If




    End Sub

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged

        If Me.Ch_deu.Checked = True Then

            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"

            Me.Txt_Rut_Deu.ReadOnly = False


            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.ReadOnly = False

            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""

            IB_AyudaDeu.Enabled = True
            Txt_deu_Cli_MaskedEditExtender.Enabled = True
            Txt_Rut_Deu.Focus()
        Else

            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""

            Me.Txt_Rut_Deu.CssClass = "clsDisabled"

            Me.Txt_Rut_Deu.ReadOnly = True
            IB_AyudaDeu.Enabled = False

            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Txt_deu_Cli_MaskedEditExtender.Enabled = False


        End If


    End Sub

    Protected Sub Ch_obl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_obl.CheckedChanged
        If Me.Ch_obl.Checked Then
            Me.rb_con_obl.Enabled = True
        Else
            Me.rb_con_obl.Enabled = False
        End If
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20030605, Usr, "PRESIONA BOTON BUSCAR DOCUMENTOS") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            hf_nro_pag.Value = 1
            buscar()

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_limp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limp.Click

        Gr_Documentos.DataSource = Nothing
        Gr_Documentos.DataBind()

        Me.btn_clasif.Enabled = False
        Ib_modificar.Enabled = False
        Ib_imprimir.Enabled = False

        Coll_DOC = New Collection
        Txt_deu_Cli_MaskedEditExtender5.Enabled = False
        Me.paginas.Text = ""
        habilita_criterios(True)
        Me.Gr_Documentos.Controls.Clear()
        page_dig = 0

        Valida_Riesgo()


    End Sub

    Protected Sub btn_clasif_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_clasif.Click
        Try


            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20020605, Usr, "PRESIONA BOTON CLASIFICACION DE RIESGO") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            AbrePopup(Me, 2, "popup_asig_riesg_doctos.aspx", "Asignacion", 1050, 450, 100, 0)

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ch_cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cli.CheckedChanged

        If Me.Ch_cli.Checked Then

            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
            Me.Txt_Dig_Cli.ReadOnly = False

            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
            Me.Txt_Rut_Cli.ReadOnly = False

            Me.Txt_Raz_Soc.Text = ""

            Txt_deu_Cli_MaskedEditExtender5.Enabled = True
            ib_ayudacli.Enabled = True
            Txt_Rut_Cli.Focus()
        Else
            ib_ayudacli.Enabled = False
            Me.Txt_Dig_Cli.Text = ""
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True

            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Rut_Cli.ReadOnly = True

            Me.Txt_Raz_Soc.Text = ""

            Txt_deu_Cli_MaskedEditExtender5.Enabled = False

        End If


    End Sub

    Protected Sub btn_next_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_otg.Click
        Try

            If Me.Gr_Documentos.Rows.Count < 15 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            page_dig = page_dig + 15

            buscar()
            hf_nro_pag.Value = hf_nro_pag.Value + 1
            Dim nro As Integer
            Dim nro_aux As Decimal

            nro = CInt(NroRow / 15)
            nro_aux = NroRow / 15

            If nro_aux > nro Then
                nro_aux = nro + 1
            End If
            paginas.Text = "Pagina " & hf_nro_pag.Value & " de " & Round(nro_aux, 0)
            valida_chequeados()
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub btn_prev_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_otg.Click
        Try

            If hf_nro_pag.Value = 1 Then
                msj.Mensaje(Me, "Atención", "Ya está en la primera página de la lista", 2)
                Exit Sub
            End If
            page_dig = page_dig - 15

            buscar()
            hf_nro_pag.Value = hf_nro_pag.Value - 1
            Dim nro As Integer
            Dim nro_aux As Decimal
            nro = CInt(NroRow / 15)
            nro_aux = NroRow / 15

            If nro_aux > nro Then
                nro_aux = nro + 1
            End If
            paginas.Text = "Pagina " & hf_nro_pag.Value & " de " & Round(nro_aux, 0)
            valida_chequeados()

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ch_sel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim chb As CheckBox
            Dim txt As Label

            chb = CType(sender, CheckBox)

            For i = 0 To Gr_Documentos.Rows.Count - 1

                txt = Gr_Documentos.Rows(i).FindControl("id_doc")

                If chb.ToolTip = txt.Text Then

                    If Trim(Me.Gr_Documentos.Rows(i).Cells(10).Text).ToUpper = "PROTESTADO" Then
                        chb.Checked = False
                        msj.Mensaje(Me, "Atención", "El documento ya ha sido protestado , no se puede modificar", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                End If

            Next

            If chb.Checked = True Then
                arreglo.Add(chb.ToolTip)
            Else
                If arreglo.Count > 0 Then
                    If arreglo.Contains(chb.ToolTip) Then
                        For i = 0 To arreglo.Count - 1
                            If arreglo.Item(i) = chb.ToolTip Then
                                arreglo.RemoveAt(i)
                                Exit For
                            End If

                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Gr_Documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Documentos.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                'e.Row.Attributes.Add("onClick", "ClickDocto(ctl00_ContentPlaceHolder1_Gr_Documentos, 'clicktable', 'formatable', 'selectable', " & Posicion & ");")
                'Posicion = Posicion + 1
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub valida_chequeados()
        Try


            For i = 0 To Gr_Documentos.Rows.Count - 1

                Dim txt As Label
                Dim ch As CheckBox



                ch = Gr_Documentos.Rows(i).FindControl("ch_sel")
                txt = Gr_Documentos.Rows(i).FindControl("id_doc")

                If arreglo.Count > 0 Then



                    If arreglo.Contains(txt.Text) Then

                        ch.Checked = True


                    End If

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ib_modificar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_modificar.Click
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20010605, Usr, "PRESIONA BOTON MODIFICAR DOCUMENTO") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim chb As Boolean
            Dim cb As CheckBox
            chb = False
            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                cb = Gr_Documentos.Rows(i).FindControl("Ch_sel")
                If cb.Checked = True Then
                    chb = True
                End If
            Next
            If chb = False Then
                msj.Mensaje(Me, "Atención", "Debes seleccionar al menos un documento para modificar", ClsMensaje.TipoDeMensaje._Informacion)

            Else
                AbrePopup(Me, 2, "modif_doc_otg.aspx", "AsignacionDeRiesgos", 550, 220, 100, 0)
            End If


            'If arreglo.Count > 0 Then
            '    RUT_CLI_RPT = Me.Txt_Rut_Cli.Text
            '    AbrePopup(Me, 2, "modif_doc_otg.aspx", "Asignacion de riesgos", 550, 220, 100, 0)

            'Else
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub txt_venc_des_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_venc_des.TextChanged
        'If Trim(txt_venc_des.Text) <> "" And txt_venc_des.Text <> "__/__/____" Then
        '    If Not IsDate(txt_venc_des.Text) Then
        '        msj.Mensaje(Me, "Atención", "Fecha vencimiento desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
        '        Me.txt_venc_des.Text = ""
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Protected Sub txt_venc_has_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_venc_has.TextChanged
        'If Trim(txt_venc_has.Text) <> "" And txt_venc_has.Text <> "__/__/____" Then
        '    If Not IsDate(txt_venc_has.Text) Then
        '        msj.Mensaje(Me, "Atención", "Fecha vencimiento hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
        '        Me.txt_venc_has.Text = ""
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try


            Dim deu As deu_cls

            If Me.Txt_Rut_Deu.Text = "" Then
                msj.Mensaje(Me, "Atencion", "Debe ingresar NIT", 2)
                Exit Sub
            End If
            If UCase(Txt_Dig_Deu.Text) <> fc.Vrut(Me.Txt_Rut_Deu.Text) Then
                msj.Mensaje(Me, "Atencion", "Digito Incorrecto", 2)
                Exit Sub
            End If
            deu = cg.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

            Session("Deudor") = deu

            If Not IsNothing(deu) Then
                'Datos Deudor
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
                Me.Txt_Rso_Deu.CssClass = "clsDisabled"
                Me.Txt_Rso_Deu.ReadOnly = True
                Txt_deu_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaDeu.Enabled = False
                Txt_Rut_Deu.Text = Format(CDbl(Txt_Rut_Deu.Text), fmt.FCMSD)
            Else
                msj.Mensaje(Page, "Atencion", "Pagador no encontrado", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        'Try

        '    Dim cli As cli_cls
        '    Dim rut As String

        '    rut = CLng(Me.Txt_Rut_Cli.Text)
        '    cli = clasecli.ClientesDevuelve(rut, Me.Txt_Dig_Cli.Text)

        '    If valida_cliente <> "" Then
        '        msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
        '        Exit Sub

        '    Else

        '        If IsNothing(cli) Then
        '            msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
        '            Txt_Rut_Cli.Text = ""
        '            Txt_Dig_Cli.Text = ""
        '            Exit Sub
        '        End If

        '        Session("Cliente") = cli



        '        'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
        '        Me.Txt_Rut_Cli.ReadOnly = True
        '        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        '        Me.Txt_Dig_Cli.ReadOnly = True
        '        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
        '        Me.Txt_Raz_Soc.ReadOnly = True
        '        Me.Txt_Raz_Soc.CssClass = "clsDisabled"
        '        Txt_deu_Cli_MaskedEditExtender5.Enabled = False

        '        'Asigna Razón Social / Nombre a Campo Client    e
        '        Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
        '        ib_ayudacli.Enabled = False

        '    End If

        'Catch ex As Exception
        '    msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        'End Try
    End Sub

    Protected Sub txt_fec_des_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec_des.TextChanged
        Try
            'If Trim(txt_fec_des.Text) <> "" And txt_fec_des.Text <> "__/__/____" Then
            '    If Not IsDate(txt_fec_des.Text) Then
            '        msj.Mensaje(Page, "Atencion", "Fecha desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
            '        txt_fec_des.Text = ""
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_fec_has_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec_has.TextChanged
        Try
            'If Trim(txt_fec_has.Text) <> "" And txt_fec_has.Text <> "__/__/____" Then
            '    If Not IsDate(txt_fec_has.Text) Then
            '        msj.Mensaje(Page, "Atencion", "Fecha hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
            '        txt_fec_has.Text = ""
            '    End If

            'End If
        Catch ex As Exception

        End Try
    End Sub

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

    Protected Sub Ib_imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_imprimir.Click

        Try

            Dim rut_cli As Long, rut_cli1 As Long, SUC1 As Integer, suc2 As Integer, eje1 As String, eje2 As String, _
            nro_otg1 As Integer, nro_otg2 As Integer, mon1 As String, mon2 As String, estado As Integer, estado1 As Integer, _
            nro_doc1 As String, nro_doc2 As String, fec_otg As String, fec_otg1 As String, tipo, rut_deu As Long, _
            rut_deu1 As Long, fec_vcto1 As DateTime, fec_vcto2 As DateTime, cobr1 As String, cobr2 As String, obl As String, _
            obl2 As String, TipoDocto_Dsd As Integer, TipoDocto_Hst As Integer, Contrato As String


            If Me.Ch_cli.Checked Then

                If Me.Txt_Rut_Cli.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Txt_Dig_Cli.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese dígito cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If UCase(Txt_Dig_Cli.Text) <> fc.Vrut(Me.Txt_Rut_Cli.Text) Then
                    msj.Mensaje(Me, "Atencion", "Dígito cliente ncorrecto", 2)
                    Exit Sub
                End If


                rut_cli = Me.Txt_Rut_Cli.Text
                rut_cli1 = Me.Txt_Rut_Cli.Text
            Else
                rut_cli = 0
                rut_cli1 = 999999999999
            End If

            If Me.Ch_deu.Checked Then

                If Me.Txt_Rut_Deu.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese NIT Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Txt_Dig_Deu.Text = "" Then
                    msj.Mensaje(Page, "Atencion", "Ingrese dígito Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If UCase(Txt_Dig_Deu.Text) <> fc.Vrut(Me.Txt_Rut_Deu.Text) Then
                    msj.Mensaje(Me, "Atencion", "Dígito Pagador Incorrecto", 2)
                    Exit Sub
                End If

                rut_deu = CLng(Me.Txt_Rut_Deu.Text)
                rut_deu1 = CLng(Me.Txt_Rut_Deu.Text)
            Else
                rut_deu = 0
                rut_deu1 = 999999999999
            End If

            'Sucursal
            SUC1 = 1
            suc2 = 1
            'Ejecutivo
            eje1 = 0
            eje2 = 9999
            'Moneda
            mon1 = 0
            mon2 = 9999
            'Responsabilidad
            cobr1 = "S"
            cobr2 = "N"

            If Me.Ch_cli.Checked = 1 Then
                tipo = 1
            Else
                tipo = 2
            End If

            If dr_est_doc.SelectedValue = 0 Then
                estado = 0
                estado1 = 999

            Else
                estado = Me.dr_est_doc.SelectedValue
                estado1 = Me.dr_est_doc.SelectedValue

            End If

            If Trim(Me.txt_oto_des.Text) = "" Then
                nro_otg1 = 0
                nro_otg2 = 999999999
            Else
                nro_otg1 = CDbl(txt_oto_des.Text)
                nro_otg2 = CDbl(txt_oto_des.Text)
            End If

            If Trim(Me.txt_doc_des.Text) = "" Then
                nro_doc1 = "0"
                nro_doc2 = "Z"
            Else
                nro_doc1 = txt_doc_des.Text.Trim
                nro_doc2 = txt_doc_des.Text.Trim
            End If

            'Fecha Otorgamiento
            '**************************************************************************************************
            If Trim(txt_fec_des.Text) <> "" Then
                If Not IsDate(txt_fec_des.Text) Then
                    msj.Mensaje(Page, "Atencion", "Fecha de otorgamiento desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_des.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(txt_fec_has.Text) <> "" Then
                If Not IsDate(txt_fec_has.Text) Then
                    msj.Mensaje(Page, "Atencion", "Fecha de otorgamiento hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_has.Text = ""
                    Exit Sub
                End If

            End If

            If Trim(Me.txt_fec_des.Text) = "" Or Trim(Me.txt_fec_has.Text) = "" Then
                fec_otg = "01/01/1900"
                fec_otg1 = "31/12/2999"
            Else

                If CDate(txt_fec_des.Text) > CDate(txt_fec_has.Text) Then
                    msj.Mensaje(Page, "Atención", "Fecha de otorgamiento desde no puede ser mayor a fecha de otorgamiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                fec_otg = txt_fec_des.Text & " 00:00:00"
                fec_otg1 = txt_fec_has.Text & " 23:59:59"

            End If

            '**************************************************************************************************
            '**************************************************************************************************
            'Fecha de vencimiento
            '**************************************************************************************************

            If Trim(txt_venc_des.Text) <> "" Then
                If Not IsDate(txt_venc_des.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.txt_venc_des.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(txt_venc_has.Text) <> "" Then
                If Not IsDate(txt_venc_has.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.txt_venc_has.Text = ""
                    Exit Sub
                End If
            End If

            If Trim(Me.txt_venc_des.Text) = "" Or Trim(Me.txt_venc_has.Text) = "" Then
                fec_vcto1 = CDate("01/01/1900")
                fec_vcto2 = CDate("31/12/9999")
            Else
                fec_vcto1 = txt_venc_des.Text & " 00:00:00"
                fec_vcto2 = txt_venc_has.Text & " 23:59:59"
            End If


            If CDate(fec_vcto1) > CDate(fec_vcto2) Then
                msj.Mensaje(Page, "Atención", "Fecha de vencimiento desde no puede ser mayor a fecha de vencimiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            If Me.Ch_obl.Checked Then
                If Me.rb_con_obl.SelectedValue = "S" Then
                    obl = "S"
                    obl2 = "S"
                Else
                    obl = "N"
                    obl2 = "N"

                End If
            Else
                obl = "S"
                obl2 = "N"
            End If

            If dr_tip_doc.SelectedValue = 0 Then
                TipoDocto_Dsd = 0
                TipoDocto_Hst = 9999
            Else
                TipoDocto_Dsd = dr_tip_doc.SelectedValue
                TipoDocto_Hst = dr_tip_doc.SelectedValue
            End If

            If txt_Contrato.Text <> "" Then
                Contrato = txt_Contrato.Text.ToUpper.Trim
            Else
                Contrato = ""
            End If


            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "Reporte_ConsultaDocumento.aspx?rutdsd=" & rut_cli & "&ruthst=" & rut_cli1 & "&Rut_deu_d=" & rut_deu _
                     & "&Rut_deu_a=" & rut_deu1 & "&TipoDocto_D=" & TipoDocto_Dsd & "&TipoDocto_A=" & TipoDocto_Hst & "&nro_doc1=" _
                     & nro_doc1 & "&nro_doc2=" & nro_doc2 & "&fec_vcto1=" & fec_vcto1 & "&fec_vcto2=" & fec_vcto2 _
                     & "&estado=" & estado & "&estado1=" & estado1 & "&nro_otg1=" & nro_otg1 & "&nro_otg2=" & nro_otg2 & "&cobr1=" & cobr1 _
                     & "&cobr2=" & cobr2 & "&fec_otg=" & fec_otg & "&fec_otg1=" & fec_otg1 & "&obl=" & obl & "&obl2=" & obl2 & "&contr=" & Contrato, "Informe_Est_Doc", 900, 800, 50, 50)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub IB_ID_Doc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ver As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gr_Documentos.Rows.Count - 1
            If (ver.ToolTip = CType(Gr_Documentos.Rows(i).FindControl("id_dsi"), Label).Text) Then
                HF_ID.Value = ver.ToolTip.ToString
                Exit For
            End If
        Next

        For I = 0 To Gr_Documentos.Rows.Count - 1

            If (ver.ToolTip = CType(Gr_Documentos.Rows(I).FindControl("id_dsi"), Label).Text) Then
                If (I Mod 2) = 0 Then
                    Gr_Documentos.Rows(I).CssClass = "selectable"
                Else
                    Gr_Documentos.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    Gr_Documentos.Rows(I).CssClass = "formatUltcell"
                Else
                    Gr_Documentos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

        IB_Calif.Enabled = True
        IB_Calif.Attributes.Add("onClick", "WinOpen(2, 'Pop_up_Cal.aspx?ID=" & HF_ID.Value & "', 'CalificaciónDocumentos',650, 230, 15, 15);")

    End Sub

End Class
