Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos
Imports ClsSession

Partial Class Modulos_Cobranzas_rigthframe_archivos_Default
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim fc As New FuncionesGenerales.FComunes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim sesion As New ClsSession.ClsSession
    Dim var As New FuncionesGenerales.Variables
    Dim fmoneda As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim Posicion As Integer = 0
    Dim Agt As New Perfiles.Cls_Principal
    Dim CBZ As New ClaseCobranza
#End Region

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged

        If Me.Ch_deu.Checked = True Then
            Me.txt_rut_deu.CssClass = "clsMandatorio"
            Me.txt_rut_deu.Enabled = True
            Me.txt_dig_deu.Enabled = True
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayuda_deu.Enabled = True
        Else
            Me.txt_rut_deu.CssClass = "clsDisabled"
            Me.txt_rut_deu.Enabled = False
            Me.txt_dig_deu.Enabled = False
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"

            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayuda_deu.Enabled = False
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            arreglo = New ArrayList

            cg.ParametrosDevuelve(TablaParametro.TipoDocumento, True, dr_tip_doc)
            ' cg.ParametrosDevuelve(TablaParametro.EstadoDocumento, True, dr_est_doc)
            cg.SucursalesDevuelve(codeje, True, Me.dr_suc)
            Coll_DOC = New Collection
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            Me.Txt_Rut_Cli.Focus()

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            Ib_ayuda_deu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',650,410,200,150);")

        End If

    End Sub

    Sub CargaDeudor()

        Dim deu As deu_cls


        If Me.Txt_Rut_Deu.Text = "" Then
            Msj.Mensaje(Me.Page, "Atencion", "Debe ingresar NIT", 2)
            Exit Sub
        End If

        If Me.Txt_Dig_Deu.Text.ToUpper <> fc.Vrut(Me.Txt_Rut_Deu.Text).ToUpper Then
            Msj.Mensaje(Me.Page, "Atencion", "Digito Incorrecto", 2)
            Exit Sub
        End If


        deu = cg.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT))



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
            Ib_ayuda_deu.Enabled = False
        End If
    End Sub

    Protected Sub ib_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If Not Agt.ValidaAccesso(20, 20020307, Usr, "PRESIONO BOTON BUSCAR DOCUMENTOS") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try

            If Me.Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar el NIT del Cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Validacion de fechas
            If txt_venc_des.Text <> "" And txt_venc_has.Text = "" Then
                Msj.Mensaje(Page, "Atención", "Ingrese fecha vencimiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_venc_des.Text = "" And txt_venc_has.Text <> "" Then
                Msj.Mensaje(Page, "Atención", "Ingrese fecha vencimiento desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_venc_des.Text <> "" And txt_venc_has.Text <> "" Then
                If Not IsDate(txt_venc_des.Text) Then
                    Msj.Mensaje(Page, "Atención", "Fecha vencimiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If Not IsDate(txt_venc_has.Text) Then
                    Msj.Mensaje(Page, "Atención", "Fecha vencimiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_venc_has.Text = ""
                    Exit Sub
                End If

                If CDate(txt_venc_des.Text) > CDate(txt_venc_has.Text) Then
                    Msj.Mensaje(Page, "Atención", "Fecha desde no puede ser mayor a fecha vencimiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If txt_mto_des.Text <> "" And txt_mto_has.Text = "" Then
                Msj.Mensaje(Page, "Atención", "Ingrese monto hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_mto_des.Text = "" And txt_mto_has.Text <> "" Then
                Msj.Mensaje(Page, "Atención", "Ingrese monto desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If txt_mto_des.Text <> "" And txt_mto_has.Text <> "" Then

                If txt_mto_des.Text > txt_mto_has.Text Then
                    Msj.Mensaje(Page, "Atención", "Monto desde no puede ser mayor a monto hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            buscar()
            deshabilita_criterios(True)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub deshabilita_criterios(ByVal est As Boolean)

        If est Then


            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Rut_Cli.ReadOnly = True

            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.ReadOnly = True

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Rut_Deu.ReadOnly = True

            Txt_Dig_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.ReadOnly = True

            'dr_est_doc.CssClass = "clsDisabled"
            'dr_est_doc.Enabled = False

            dr_tip_doc.CssClass = "clsDisabled"
            dr_tip_doc.Enabled = False

            txt_doc_des.CssClass = "clsDisabled"
            txt_doc_des.ReadOnly = True

            txt_oto_des.CssClass = "clsDisabled"
            txt_oto_des.ReadOnly = True

            txt_mto_des.CssClass = "clsDisabled"
            txt_mto_des.ReadOnly = True

            txt_mto_has.CssClass = "clsDisabled"
            txt_mto_has.ReadOnly = True


            txt_venc_des.CssClass = "clsDisabled"
            txt_venc_des.ReadOnly = True

            txt_venc_has.CssClass = "clsDisabled"
            txt_venc_has.ReadOnly = True

            IB_Buscar.Enabled = False

            txt_doc_des.ReadOnly = True
            'txt_doc_des_MaskedEditExtender.Enabled = False
            txt_mto_des.ReadOnly = True
            txt_mto_has.ReadOnly = True
            txt_venc_des_MaskedEditExtender.Enabled = False
            txt_venc_has_MaskedEditExtender.Enabled = False
            CalendarExtender1.Enabled = False
            CalendarExtender3.Enabled = False

            MaskedEditExtender2.Enabled = False
            MaskedEditExtender3.Enabled = False
            MaskedEditExtender1.Enabled = False
        Else


            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli.ReadOnly = False

            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Rut_Deu.ReadOnly = True

            Txt_Dig_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.ReadOnly = True

            'dr_est_doc.CssClass = "clsTxt"
            'dr_est_doc.Enabled = True

            dr_tip_doc.CssClass = "clsTxt"
            dr_tip_doc.Enabled = True

            txt_doc_des.CssClass = "clsTxt"
            txt_doc_des.ReadOnly = False

            txt_oto_des.CssClass = "clsTxt"
            txt_oto_des.ReadOnly = False

            txt_mto_des.CssClass = "clsTxt"
            txt_mto_des.ReadOnly = False

            txt_mto_has.CssClass = "clsTxt"
            txt_mto_has.ReadOnly = False

            txt_venc_des.CssClass = "clsTxt"
            txt_venc_des.ReadOnly = False

            txt_venc_has.CssClass = "clsTxt"
            txt_venc_has.ReadOnly = False
            IB_Buscar.Enabled = True

            txt_doc_des.ReadOnly = False
            'txt_doc_des_MaskedEditExtender.Enabled = True
            txt_mto_des.ReadOnly = False
            txt_mto_has.ReadOnly = False
            txt_venc_des_MaskedEditExtender.Enabled = True
            txt_venc_has_MaskedEditExtender.Enabled = True
            CalendarExtender1.Enabled = True
            CalendarExtender3.Enabled = True

            MaskedEditExtender2.Enabled = True
            MaskedEditExtender3.Enabled = True
            MaskedEditExtender1.Enabled = True
        End If





    End Sub

    Public Sub buscar()


        Dim deu_ide, deu_ide1 As String
        Dim deudor As Boolean
        Dim venc_des, venc_has As DateTime
        Dim otg_num, otg_num2 As Long
        Dim doc_num, doc_num2 As String
        Dim mto_des, mto_has As Double
        Dim ESTADO1, ESTADO2 As Integer
        Dim TIPO1, TIPO2 As Integer


        If Me.txt_venc_des.Text = "" Then
            venc_des = "01/01/1900"
        Else
            venc_des = Me.txt_venc_des.Text
        End If

        If Me.txt_venc_has.Text = "" Then
            venc_has = "31/12/2100"
        Else
            venc_has = Me.txt_venc_has.Text
        End If

        If Me.txt_oto_des.Text = "" Then
            otg_num = 0
            otg_num2 = 999999
        Else
            otg_num = txt_oto_des.Text
            otg_num2 = txt_oto_des.Text
        End If

        If Me.txt_doc_des.Text = "" Then
            doc_num = "0"
            doc_num2 = "Z"
        Else
            doc_num = txt_doc_des.Text
            doc_num2 = txt_doc_des.Text
        End If

        If Me.Txt_Rut_Deu.Text <> "" Then
            deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)
            deu_ide1 = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)
            deudor = True
        Else
            deu_ide = "000000000000"
            deu_ide1 = "9999999999999"
        End If


        If Me.txt_mto_des.Text = "" Then
            mto_des = 0
        Else
            mto_des = Me.txt_mto_des.Text
        End If

        If Me.txt_mto_has.Text = "" Then
            mto_has = 9999999999999
        Else
            mto_has = Me.txt_mto_has.Text
        End If

        ESTADO1 = 0
        ESTADO2 = 999


        If Me.dr_tip_doc.SelectedValue = 0 Then
            TIPO1 = 0
            TIPO2 = 999
        Else
            TIPO1 = dr_tip_doc.SelectedValue
            TIPO2 = dr_tip_doc.SelectedValue
        End If

        Coll_DOC = CBZ.Doctos_asig_esp_retorna(Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT), deu_ide, deu_ide1, deudor, 1, venc_des, venc_has, otg_num, otg_num2, doc_num, doc_num2, mto_des, mto_has, ESTADO1, ESTADO2, TIPO1, TIPO2)

        Dim col As New Collection
        page_dig = 0
        Dim indice As Integer = page_dig + 8

        If indice > Coll_DOC.Count Then
            indice = Coll_DOC.Count
        End If

        For i = page_dig + 1 To indice
            col.Add(Coll_DOC.Item(i))
        Next

        Me.GridView1.DataSource = col
        Me.GridView1.DataBind()

        If Me.GridView1.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Atención", "No se han encontrado documentos segun criterios proporcionados", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        Else
            hf_nro_pag.Value = 1
        End If

        For i = 0 To Me.GridView1.Rows.Count - 1

            Me.GridView1.Rows(i).Cells(1).Text = Format(CLng(Me.GridView1.Rows(i).Cells(1).Text), fmoneda.FCMSD) & "-" & fc.Vrut(CLng(Me.GridView1.Rows(i).Cells(1).Text))

            If col.Item(i + 1).id_p_0023 = 2 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD4)

            ElseIf col.Item(i + 1).id_p_0023 = 3 Or col.Item(i + 1).id_p_0023 = 4 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD)

            ElseIf col.Item(i + 1).id_p_0023 = 1 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMSD)

            End If

            If col.Item(i + 1).id_dor <> 0 Then
                Me.GridView1.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#FF7F50")
            End If

        Next

    End Sub

    Protected Sub IB_GuardaGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GuardaGestion.Click
        If Not Agt.ValidaAccesso(20, 20010307, Usr, "PRESIONO BOTON GUARDAR ASIGNACION") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Dim sel As Boolean

        'For i = 0 To Me.GridView1.Rows.Count - 1

        '    Dim ch As CheckBox
        '    ch = Me.GridView1.Rows(i).FindControl("ch_grid")

        '    If ch.Checked Then
        '        sel = True
        '    End If

        'Next

        If arreglo.Count > 0 Then
            sel = True
        End If


        If Not sel Then
            Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar al menos un documento para guardar", 2)
        Else
            Msj.Mensaje(Me.Page, "Atención", "¿ Desea guardar ?", 4, lb_guarda.UniqueID)

        End If

    End Sub

    Protected Sub IB_CancelarGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CancelarGestion.Click

        IB_AyudaCli.Enabled = True

        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
        Me.Txt_Rut_Cli.ReadOnly = False

        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
        Me.Txt_Dig_Cli.ReadOnly = False

        Me.Txt_Raz_Soc.Text = ""


        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Dig_Deu.CssClass = "clsDisabled"
        Me.Txt_Rut_Deu.ReadOnly = True

        Me.Txt_Rso_Deu.Text = ""

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Rut_Deu.CssClass = "clsDisabled"
        Me.Txt_Rut_Deu.Enabled = False
        Me.Txt_Dig_Deu.Enabled = False

        Me.Ch_deu.Checked = False

        ' Me.dr_est_doc.SelectedValue = 0
        Me.dr_suc.SelectedValue = 0
        Me.dr_tip_doc.SelectedValue = 0

        Me.txt_doc_des.Text = ""
        Me.txt_oto_des.Text = ""
        Me.txt_mto_des.Text = ""
        Me.txt_mto_has.Text = ""

        Me.txt_venc_des.Text = ""
        Me.txt_venc_has.Text = ""



        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()

        Me.GridView1.Controls.Clear()

        deshabilita_criterios(False)

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Modulo = "Cobranza"
        Pagina = Page.AppRelativeVirtualPath
        CambioTema(Page)
    End Sub

    Sub CargaCliente()

        Dim cli As cli_cls
        Dim CLSCLI As New ClaseClientes
        Dim rut As String


        rut = CLng(Me.Txt_Rut_Cli.Text)
        cli = CLSCLI.ClientesDevuelve(rut, Me.Txt_Dig_Cli.Text.ToUpper)

        If sesion.valida_cliente <> "" Then
            Msj.Mensaje(Me, "Atención", sesion.valida_cliente, 2)
        Else

            If IsNothing(cli) Then
                Msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Session("Cliente") = cli

            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False
            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            Me.IB_Buscar.Enabled = True
        End If



    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        CargaCliente()
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        CargaDeudor()
    End Sub

    Protected Sub IB_SeleccionDoctos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_SeleccionDoctos.Click

        For i = 0 To Me.GridView1.Rows.Count - 1
            Dim val As Boolean
            Dim ch As CheckBox

            ch = Me.GridView1.Rows(i).FindControl("ch_grid")

            If i = 0 Then

                If ch.Checked = True Then

                    val = False

                Else
                    val = True

                End If

            End If

            ch.Checked = val
            Dim txt As Label

            txt = GridView1.Rows(i).FindControl("id_doc")

            If arreglo.Count > 0 Then

                If arreglo.Contains(txt.Text) Then

                    For x = 0 To arreglo.Count - 1
                        If arreglo.Item(x) = txt.Text Then
                            arreglo.Remove(arreglo.Item(x))
                            Exit For
                        End If
                    Next

                Else
                    arreglo.Add(txt.Text)
                End If

            Else
                arreglo.Add(txt.Text)
            End If

        Next

    End Sub

    Protected Sub btn_next_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_otg.Click
        'Dim deu_ide, deu_ide1 As String
        'Dim deudor As Boolean
        'Dim venc_des, venc_has As DateTime
        'Dim otg_num, otg_num2 As Long
        'Dim doc_num, doc_num2 As String
        'Dim mto_des, mto_has As Double
        'Dim ESTADO1, ESTADO2 As Integer
        'Dim TIPO1, TIPO2 As Integer

        If Me.GridView1.Rows.Count < 8 Then
            Msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
            Exit Sub
        End If
        page_dig = page_dig + 8

        Dim col As New Collection

        Dim indice As Integer = page_dig + 8

        For i = page_dig + 1 To indice
            If Coll_DOC.Count >= i Then
                col.Add(Coll_DOC.Item(i))
            End If
        Next

        Me.GridView1.DataSource = col
        Me.GridView1.DataBind()

        'Coll_DOC = CBZ.Doctos_asig_esp_retorna(Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT), deu_ide, deu_ide1, deudor, 1, venc_des, venc_has, otg_num, otg_num2, doc_num, doc_num2, mto_des, mto_has, ESTADO1, ESTADO2, TIPO1, TIPO2)


        'Dim col As New Collection
        ''page_dig = 0
        'Dim indice As Integer = page_dig + 8
        'If indice > Coll_DOC.Count Then

        '    indice = Coll_DOC.Count
        'End If

        'For i = page_dig + 1 To indice

        '    col.Add(Coll_DOC.Item(i))

        'Next

        'Me.GridView1.DataSource = col
        'Me.GridView1.DataBind()





        If Me.GridView1.Rows.Count = 0 Then


            Msj.Mensaje(Me, "Atención", "No se han encontrado documentos segun criterios proporcionados", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        For i = 0 To Me.GridView1.Rows.Count - 1
            Me.GridView1.Rows(i).Cells(1).Text = Format(CLng(Me.GridView1.Rows(i).Cells(1).Text), fmoneda.FCMSD) & "-" & fc.Vrut(CLng(Me.GridView1.Rows(i).Cells(1).Text))
            If col.Item(i + 1).id_p_0023 = 2 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD4)

            ElseIf col.Item(i + 1).id_p_0023 = 3 Or col.Item(i + 1).id_p_0023 = 4 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD)

            ElseIf col.Item(i + 1).id_p_0023 = 1 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMSD)

            End If

            If col.Item(i + 1).id_dor <> 0 Then

                'Dim COLOR As New System.Drawing.Color
                'COLOR = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                Me.GridView1.Rows(i).BackColor = Drawing.Color.Coral

            End If

        Next
        'buscar()
        hf_nro_pag.Value = Val(hf_nro_pag.Value) + 1
        'paginas.Text = "Pagina " & hf_nro_pag.Value & " de " & CInt(NroRow / 15)
        valida_chequeados()
    End Sub

    Protected Sub btn_prev_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_otg.Click

        If hf_nro_pag.Value = 1 Then
            Msj.Mensaje(Me, "Atención", "Ya está en la primera página de la lista", 2)
            Exit Sub
        End If
        page_dig = page_dig - 8

        Dim col As New Collection

        Dim indice As Integer = page_dig + 8

        For i = page_dig + 1 To indice

            col.Add(Coll_DOC.Item(i))

        Next

        Me.GridView1.DataSource = col
        Me.GridView1.DataBind()

        If Me.GridView1.Rows.Count = 0 Then


            Msj.Mensaje(Me, "Atención", "No se han encontrado documentos segun criterios proporcionados", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        For i = 0 To Me.GridView1.Rows.Count - 1

            Me.GridView1.Rows(i).Cells(1).Text = Format(CLng(Me.GridView1.Rows(i).Cells(1).Text), fmoneda.FCMSD) & "-" & fc.Vrut(CLng(Me.GridView1.Rows(i).Cells(1).Text))



            If col.Item(i + 1).id_p_0023 = 2 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD4)

            ElseIf col.Item(i + 1).id_p_0023 = 3 Or col.Item(i + 1).id_p_0023 = 4 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMCD)

            ElseIf col.Item(i + 1).id_p_0023 = 1 Then

                Me.GridView1.Rows(i).Cells(8).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(8).Text), fmoneda.FCMSD)

            End If

            If col.Item(i + 1).id_dor <> 0 Then

                'Dim COLOR As New System.Drawing.Color
                'COLOR = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                Me.GridView1.Rows(i).BackColor = Drawing.Color.Coral

            End If

        Next

        hf_nro_pag.Value = Val(hf_nro_pag.Value) - 1
        valida_chequeados()

    End Sub

    Public Sub valida_chequeados()

        For i = 0 To GridView1.Rows.Count - 1

            Dim txt As Label
            Dim ch As CheckBox



            ch = GridView1.Rows(i).FindControl("ch_grid")
            txt = GridView1.Rows(i).FindControl("id_doc")

            If arreglo.Count > 0 Then



                If arreglo.Contains(txt.Text) Then

                    ch.Checked = True


                End If

            End If

        Next

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'e.Row.Attributes.Add("onClick", "ClickDocto(ctl00_ContentPlaceHolder1_GridView1, 'clicktable', 'formatable', 'selectable', " & Posicion & ");")

            Posicion = Posicion + 1

        End If
    End Sub

    Protected Sub ch_grid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim cb As CheckBox

        cb = sender

        If arreglo.Count > 0 Then

            If arreglo.Contains(cb.ToolTip) Then
                For i = 0 To arreglo.Count - 1

                    If arreglo.Item(i) = cb.ToolTip Then

                        arreglo.Remove(i)
                        Exit For
                    End If

                Next

            Else
                arreglo.Add(cb.ToolTip)
            End If



        Else
            arreglo.Add(cb.ToolTip)
        End If


    End Sub

    Protected Sub lb_guarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guarda.Click

        Dim sel As Boolean

        For i = 0 To arreglo.Count - 1

            'Guardar en la Dor

            Dim dor As New dor_cls

            With dor

                .id_doc = arreglo.Item(i)
                .dor_est = "N"
                .dor_fec_sol = Date.Now.ToShortDateString
                .id_suc = Me.dr_suc.SelectedValue
                .id_suc_orn = Me.dr_suc.SelectedValue
                If Me.dr_suc.SelectedValue <> var.TAB_SUCREC Then
                    .dor_otr_suc = "S"
                Else
                    .dor_otr_suc = "N"
                End If

            End With

            If CBZ.dor_inserta(dor) Then
                sel = True
            End If

        Next

        If Not sel Then
            Msj.Mensaje(Me.Page, "Atención", "Debes seleccionar al menos un documento para guardar", 2)
        Else
            Msj.Mensaje(Me.Page, "Atencion", "Registro Guardado", 2)
            page_dig = 0
            arreglo = New ArrayList

            buscar()
            deshabilita_criterios(True)
        End If
    End Sub

End Class
