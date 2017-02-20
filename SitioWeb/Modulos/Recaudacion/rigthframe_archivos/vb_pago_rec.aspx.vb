Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_Default
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim ag As New ActualizacionesGenerales
    Dim pagos As New ClsSession.SesionPagos
    Dim msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim PGO As New ClasePagos
    Dim REC As New ClaseRecaudación

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then

            Modulo = "Control Dual"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If


    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click

        If Not IsDate(Me.txt_fec_rec.Text) Then

            msj.Mensaje(Me, "Atención", "Debe ingresar una fecha para consultar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub

        End If

        If Me.dr_recau.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un Recaudador", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub

        End If
        retorna_pago()

        marca_grilla_pago()

        If gr_ing.Rows.Count = 0 Then
            msj.Mensaje(Me.Page, "Atención", "No se encuentran datos segun criterio", 2)
        End If

    End Sub

    Public Sub retorna_pago()
        pagos.Coll_Pagos = New Collection
        pagos.Coll_Pagos = REC.Pagos_Recaudación_Retorna(Me.txt_fec_rec.Text, rb_horario.SelectedValue, Me.dr_recau.SelectedValue, Me.rb_est_pgo.SelectedValue)
        Me.gr_ing.DataSource = pagos.Coll_Pagos
        Me.gr_ing.DataBind()

    End Sub

    Public Sub marca_grilla_pago()

      

        For i = 0 To Me.gr_ing.Rows.Count - 1

            'Dim pos As Integer
            'Dim nro As Integer 'FY 03-05-2012
            If HF_Pos_doc.Value <> "" Then


                'pos = Val(HF_Pos_doc.Value) - 1
                'nro = HF_Pos_doc.Value

                'If HF_Pos_doc.Value <> gr_ing.Rows(i).Cells(1).Text Then 'If pos <> i Then

                If pagos.Coll_Pagos.Item(i + 1).est_pgo = "V" Then
                    Dim COLOR As New System.Drawing.Color

                    COLOR = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                    Me.gr_ing.Rows(i).BackColor = COLOR
                    Me.gr_ing.Rows(i).CssClass = ""
                ElseIf pagos.Coll_Pagos.Item(i + 1).est_pgo = "R" Then
                    Dim COLOR As New System.Drawing.Color

                    COLOR = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                    Me.gr_ing.Rows(i).BackColor = COLOR
                    Me.gr_ing.Rows(i).CssClass = ""
                Else

                    'gr_ing.Rows(i).CssClass = "formatable"
                    gr_ing.Rows(i).CssClass = "selectable"

                End If

                'Else
                '    'gr_ing.Rows(i).BackColor = Nothing 'fy 03-05-2012
                '    'gr_ing.Rows(i).CssClass = "clicktable"
                '    gr_ing.Rows(i).CssClass = "selectable"

                'End If

            Else

                If pagos.Coll_Pagos.Item(i + 1).est_pgo = "V" Then
                    Dim COLOR As New System.Drawing.Color

                    COLOR = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                    Me.gr_ing.Rows(i).BackColor = COLOR
                    Me.gr_ing.Rows(i).CssClass = ""
                ElseIf pagos.Coll_Pagos.Item(i + 1).est_pgo = "R" Then
                    Dim COLOR As New System.Drawing.Color

                    COLOR = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                    Me.gr_ing.Rows(i).BackColor = COLOR
                    Me.gr_ing.Rows(i).CssClass = ""
                End If

            End If





        Next

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            coll_dsi_simu = New Collection
            cg.EjecutivosDevuelve(dr_recau, CodEje, 14)
            Me.txt_fec_rec.Text = Format(CDate(Date.Now.ToShortDateString), "dd/MM/yyyy")
            pagos.Coll_Pagos = New Collection
            NroPaginacion_Docto_a_Pagar = 0
            NroPaginacion_Recaudacion = 0
        End If
        btn_informe.Attributes.Add("onClick", "WinOpen(2,'Reporte_Pago_rec.aspx?eje=" & Me.dr_recau.SelectedValue & "&fecha=" & CDate(Me.txt_fec_rec.Text) & "&Hora=" & rb_horario.SelectedValue & "'  ,'PopUpCliente', 1100,750,100,100);")
    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click

        Me.gr_doctos.DataSource = Nothing
        Me.gr_doctos.DataBind()
        Me.gr_ing.DataSource = Nothing
        Me.gr_ing.DataBind()
        Me.gr_recau.DataSource = Nothing
        Me.gr_recau.DataBind()
        Me.txt_fec_rec.Text = Format(CDate(Date.Now.ToShortDateString), "dd/MM/yyyy")
        Me.dr_recau.SelectedValue = 0

        Me.HF_Pos_doc.Value = ""
        Me.HF_Pos_DPO.Value = ""
        NroPaginacion_Docto_a_Pagar = 0
        NroPaginacion_Recaudacion = 0
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_ing.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_ing, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_ing, 'formatable')")
            'e.Row.Attributes.Add("onClick", "CargaDetalle(ctl00_ContentPlaceHolder1_gr_ing, 'clicktable', 'formatable', 'selectable');")
        End If

    End Sub

    Protected Sub RetornaDoctos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetornaDoctos.Click

        Dim col_doc As New Collection
        Dim col_nce As New Collection

        coll_dsi_simu = New Collection

        '***************************************************
        'Carga de Documentos de Pago
        '***************************************************

        'Me.gr_recau.DataSource = cg.IngresoDevuelveModoDePago(gr_ing.Rows(HF_Pos_doc.Value - 1).Cells(0).Text)
        'Me.gr_recau.DataBind()


        '***************************************************
        'Carga de Documentos no Cedidos
        '***************************************************

        'col_nce = cg.PagosDocumentosCxCDevuelveDetalle(Me.gr_ing.Rows(HF_Pos_doc.Value - 1).Cells(0).Text, 3)
        col_nce = cg.PagosDocumentosCxCDevuelveDetalle(Val(HF_Pos_doc.Value), 3)

        If Not IsNothing(col_nce) Then

            For i = 1 To col_nce.Count
                Dim obj As New obj_rec

                With obj
                    .TIPO_DOC_REC = "C"
                    .TIPO_PAGO = col_nce.Item(i).ing_tot_par
                    .N_DOCTO = col_nce.Item(i).nce_num_doc
                    .RUT_CLI = col_nce.Item(i).cli_idc
                    .N_CLIENTE = col_nce.Item(i).Nombre
                    .RUT_DEUDOR = col_nce.Item(i).RutDeu
                    .NOMBRE_DEUDOR = col_nce.Item(i).NombreDeu
                    .MTO_A_RECAUDAR = col_nce.Item(i).ing_mto_tot
                    .INTERES = col_nce.Item(i).ing_mto_int
                    .DES_TIP_DOC = col_nce.Item(i).pnu_atr_003
                    .FEC_VCTO = col_nce.Item(i).nce_fec_vcto
                End With

                coll_dsi_simu.Add(obj)
            Next

        End If
        Carga_Grilla_Recaudacion()
        Carga_Grilla_Doctos()
        ' ''***************************************************
        ' ''Carga de Documentos 
        ' ''***************************************************

        'col_doc = cg.PagosDocumentosCxCDevuelveDetalle(Me.gr_ing.Rows(HF_Pos_doc.Value - 1).Cells(0).Text, 2)



        'If Not IsNothing(col_doc) Then

        '    For i = 1 To col_doc.Count
        '        Dim obj As New obj_rec

        '        With obj
        '            .TIPO_DOC_REC = "D"
        '            .TIPO_PAGO = col_doc.Item(i).ing_tot_par
        '            .N_DOCTO = col_doc.Item(i).dsi_num
        '            .RUT_CLI = col_doc.Item(i).cli_idc
        '            .N_CLIENTE = col_doc.Item(i).Nombre
        '            .RUT_DEUDOR = col_doc.Item(i).RutDeu
        '            .NOMBRE_DEUDOR = col_doc.Item(i).NombreDeu
        '            .MTO_A_RECAUDAR = col_doc.Item(i).ing_mto_tot
        '            .INTERES = col_doc.Item(i).ing_mto_int
        '            .DES_TIP_DOC = col_doc.Item(i).pnu_atr_003
        '            .FEC_VCTO = col_doc.Item(i).dsi_fev_rea
        '        End With

        '        coll_dsi_simu.Add(obj)
        '    Next
        'End If


        'gr_doctos.DataSource = coll_dsi_simu
        'gr_doctos.DataBind()

        'marcagrilla()

        marca_grilla_pago()


    End Sub

    Public Sub marcagrilla()

        Dim COLOR As New System.Drawing.Color

        For i = 0 To Me.gr_doctos.Rows.Count - 1

            If coll_dsi_simu.Item(i + 1).TIPO_DOC_REC = "D" And coll_dsi_simu.Item(i + 1).TIPO_PAGO = "T" Then
                COLOR = Nothing
            ElseIf coll_dsi_simu.Item(i + 1).TIPO_DOC_REC = "D" And coll_dsi_simu.Item(i + 1).TIPO_PAGO = "P" Then
                COLOR = Nothing
            ElseIf coll_dsi_simu.Item(i + 1).TIPO_DOC_REC = "C" Then
                COLOR = System.Drawing.ColorTranslator.FromHtml("#FB9FA6") 'Docto No Cedido
            End If

            Me.gr_doctos.Rows(i).BackColor = COLOR

            Me.gr_doctos.Rows(i).Cells(2).Text = FC.FormatoMiles(CLng(Me.gr_doctos.Rows(i).Cells(2).Text).ToString()) & "-" & FC.Vrut(Me.gr_doctos.Rows(i).Cells(2).Text)
            Me.gr_doctos.Rows(i).Cells(4).Text = FC.FormatoMiles(CLng(Me.gr_doctos.Rows(i).Cells(4).Text).ToString()) & "-" & FC.Vrut(Me.gr_doctos.Rows(i).Cells(4).Text)

        Next

    End Sub

    Protected Sub marca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles marca.Click
        marca_grilla_pago()
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub btn_validar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_validar.Click


        If Not agt.ValidaAccesso(20, 20010503, Usr, "PRESIONO APROBAR PAGO DE RECAUDACION") Then
            msj.Mensaje(Me.Page, "VB RECAUDACION", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.HF_Pos_doc.Value = "" Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un pago , para poder Aprobar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        If rb_est_pgo.SelectedValue = "V" Then
            msj.Mensaje(Me, "Atención", "El pago ya se encuentra aprobado, no puede volverse a aprobar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        'If REC.Pagos_Recaudación_ApruebaRechaza(Me.gr_ing.Rows(Val(HF_Pos_doc.Value) - 1).Cells(0).Text, "V") = True Then
        If REC.Pagos_Recaudación_ApruebaRechaza(Val(HF_Pos_doc.Value), "V") = True Then

            msj.Mensaje(Me.Page, "Atención", "El pago ha sido Aprobado", 2)

            Me.gr_doctos.DataSource = Nothing
            Me.gr_doctos.DataBind()

            Me.gr_ing.DataSource = Nothing
            Me.gr_ing.DataBind()

            Me.gr_recau.DataSource = Nothing
            Me.gr_recau.DataBind()

            retorna_pago()
            marca_grilla_pago()
        End If

    End Sub

    Protected Sub btn_rechazar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_rechazar.Click

        If Not agt.ValidaAccesso(20, 20020503, Usr, "PRESIONO RECHAZAR PAGO DE RECAUDACION") Then
            msj.Mensaje(Me.Page, "VB RECAUDACION", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If Me.HF_Pos_doc.Value = "" Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un pago , para poder Rechazar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If
        'If REC.Pagos_Recaudación_ApruebaRechaza(Me.gr_ing.Rows(Val(HF_Pos_doc.Value) - 1).Cells(0).Text, "R") = True Then
        If REC.Pagos_Recaudación_ApruebaRechaza(Val(HF_Pos_doc.Value), "R") = True Then

            msj.Mensaje(Me.Page, "Atención", "El pago ha sido Rechazado", 2)
            retorna_pago()
            Me.gr_doctos.DataSource = Nothing
            Me.gr_doctos.DataBind()

            Me.gr_ing.DataSource = Nothing
            Me.gr_ing.DataBind()

            Me.gr_recau.DataSource = Nothing
            Me.gr_recau.DataBind()

            marca_grilla_pago()
        End If

    End Sub

    Protected Sub txt_fec_rec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec_rec.TextChanged
        If Not IsDate(Me.txt_fec_rec.Text) Then
            msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.txt_fec_rec.Text = ""
            Exit Sub
        End If
    End Sub

    Protected Sub dr_recau_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_recau.SelectedIndexChanged
        Me.gr_doctos.DataSource = Nothing
        Me.gr_doctos.DataBind()
        Me.gr_ing.DataSource = Nothing
        Me.gr_ing.DataBind()
        Me.gr_recau.DataSource = Nothing
        Me.gr_recau.DataBind()
        Me.HF_Pos_doc.Value = ""
        Me.HF_Pos_DPO.Value = ""
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion_Docto_a_Pagar = 0 Then
            msj.Mensaje(Me, "Atención", "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion_Docto_a_Pagar > 8 Then
            NroPaginacion_Docto_a_Pagar -= 8
            Carga_Grilla_Doctos()
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If gr_doctos.Rows.Count < 8 Then
            msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If gr_doctos.Rows.Count = 8 Then
            NroPaginacion_Docto_a_Pagar += 8
            Carga_Grilla_Doctos()
        End If

    End Sub
    Protected Sub IB_Prev_gr_recau_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_gr_recau.Click
        If NroPaginacion_Recaudacion = 0 Then
            msj.Mensaje(Me, "Atención", "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion_Recaudacion >= 8 Then
            NroPaginacion_Recaudacion -= 8
            Carga_Grilla_Recaudacion()
        End If
    End Sub

    Protected Sub IB_Next_gr_recau_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_gr_recau.Click
        If gr_recau.Rows.Count < 8 Then
            msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If gr_recau.Rows.Count = 8 Then
            NroPaginacion_Recaudacion += 8
            Carga_Grilla_Recaudacion()
        End If
    End Sub
    Public Sub Carga_Grilla_Doctos()
        Try

            '***************************************************
            'Carga de Documentos 
            '***************************************************
            Dim col_doc As New Collection
            'col_doc = cg.PagosDocumentosCxCDevuelveDetalle(Me.gr_ing.Rows(HF_Pos_doc.Value - 1).Cells(0).Text, 2)
            col_doc = cg.PagosDocumentosCxCDevuelveDetalle(Val(HF_Pos_doc.Value), 2)



            If Not IsNothing(col_doc) Then

                For i = 1 To col_doc.Count
                    Dim obj As New obj_rec

                    With obj
                        .TIPO_DOC_REC = "D"
                        .TIPO_PAGO = col_doc.Item(i).ing_tot_par
                        .N_DOCTO = col_doc.Item(i).dsi_num
                        .RUT_CLI = col_doc.Item(i).cli_idc
                        .N_CLIENTE = col_doc.Item(i).Nombre
                        .RUT_DEUDOR = col_doc.Item(i).RutDeu
                        .NOMBRE_DEUDOR = col_doc.Item(i).NombreDeu
                        .MTO_A_RECAUDAR = col_doc.Item(i).ing_mto_tot
                        .INTERES = col_doc.Item(i).ing_mto_int
                        .DES_TIP_DOC = col_doc.Item(i).pnu_atr_003
                        .FEC_VCTO = col_doc.Item(i).dsi_fev_rea
                    End With

                    coll_dsi_simu.Add(obj)
                Next
            End If


            gr_doctos.DataSource = coll_dsi_simu
            gr_doctos.DataBind()

            marcagrilla()

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub Carga_Grilla_Recaudacion()
        Try

            'Me.gr_recau.DataSource = cg.IngresoDevuelveModoDePago(gr_ing.Rows(HF_Pos_doc.Value - 1).Cells(0).Text)
            Me.gr_recau.DataSource = cg.IngresoDevuelveModoDePago(HF_Pos_doc.Value)
            Me.gr_recau.DataBind()

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub rb_est_pgo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_est_pgo.SelectedIndexChanged
        Me.gr_doctos.DataSource = Nothing
        Me.gr_doctos.DataBind()
        Me.gr_ing.DataSource = Nothing
        Me.gr_ing.DataBind()
        Me.gr_recau.DataSource = Nothing
        Me.gr_recau.DataBind()
        Me.HF_Pos_doc.Value = ""
        Me.HF_Pos_DPO.Value = ""
        NroPaginacion_Docto_a_Pagar = 0
        NroPaginacion_Recaudacion = 0
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'FY 03-05-2012
        Try
            Dim BTN As ImageButton = CType(sender, ImageButton)

            If BTN.ToolTip <> "" Then
                HF_Pos_doc.Value = BTN.ToolTip
                RetornaDoctos_Click(sender, e)
            End If

        Catch EX As Exception
            msj.Mensaje(Page, "Atención", EX.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


  
End Class
