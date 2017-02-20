Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.ClsLocateInfo
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.Errores
Imports System.Web.UI.UserControl
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Public Class ClsIngOpe
    Inherits System.Web.UI.Page

#Region "Declaracion de variables"

    Dim err As New FuncionesGenerales.Errores
    Dim VAR As New FuncionesGenerales.Variables
    Dim resp As String
    Dim sesion As New ClsSession.ClsSession
    Dim clasecli As New ClaseClientes
    Dim CG As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FC As New FuncionesGenerales.FComunes
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim OP As New ClaseOperaciones
    Dim msj As New ClsMensaje
    Private idx As Int16
    Dim uc As System.Web.UI.UserControl
    Dim validacion As Integer
    Dim Agt As New Perfiles.Cls_Principal

#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'Introducir aquí el código de usuario para inicializar la página
        Try


            If Not Page.IsPostBack Then

                page_dig = 0
                valida_cliente = ""

                'Llena DroList con Parámetros de Sistema.
                CargaDrop_Form()

                'Inhabilita Campos al entrar por primera vez.
                InhabilitaCampos_Form()

                coll_ope = New Collection
                Coll_DSI = New Collection

                Me.Txt_Rut_Cli.Focus()
                alinea_textos()
                'Btn_gua_ope.Visible = False

                If Not IsNothing(Request.QueryString("id")) Then

                    Me.Txt_Rut_Cli.Text = RutCli
                    Me.Txt_Dig_Cli.Text = FC.Vrut(Me.Txt_Rut_Cli.Text)

                    Lb_buscar_Click(Me, e)

                    If coll_ope.Count = 0 Then
                        Exit Sub
                    End If

                    RetornaDoc()

                End If
            Else

                If Me.NoSeleccion.Text = "1" Then
                    msj.Mensaje(Me.Page, "Atención", "Debes Seleccionar un Documento", 2)
                End If

                If Txt_ItemOPE.Text <> "" Then

                    RetornaDoc()
                    ValidaOperacionCuadrada()
                    MarcaOperacionesDescuadradas()
                    RefrescaDocumentos()
                    marcagrilla()
                    'Lb_buscar_Click(Me, e)

                    If Txt_ItemDSI.Text <> "" Then
                        MarcaGrillaDoctos()
                    End If

                End If

            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',620,410,200,150);")

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub RefrescaDocumentos()

        Dim itemIdx As Integer

        itemIdx = Txt_ItemOPE.Text

        Try


            'Refresca Collection para establecer los cambios ocurridos al ingresar y guardar un documento
            idx = 0
            Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemIdx).id_ope, coll_ope.Item(itemIdx).id_ope)

            Gr_Documentos.DataSource = Coll_DSI
            Gr_Documentos.DataBind()

            If Not IsNothing(Coll_DSI) Then
                If Coll_DSI.Count > 0 Then
                    Btn_ModDoc.Enabled = True
                Else
                    Btn_ModDoc.Enabled = False
                End If
            Else
                Btn_ModDoc.Enabled = False
            End If


            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                If coll_ope.Item(itemIdx).ope_cuo = "S" Then

                    ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO

                    'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                    '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                    '    Gr_Documentos.Rows(i).Enabled = False
                    'End If

                End If

                Me.Gr_Documentos.Rows(i).Cells(1).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text))
                'Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD)

                If coll_ope.Item(itemIdx).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMSD)

                ElseIf coll_ope.Item(itemIdx).id_p_0023 = 3 Or coll_ope.Item(itemIdx).id_p_0023 = 4 Then

                    Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD)

                Else

                    Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD4)

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Private Sub CargaDrop_Form()

        'Parámetro Moneda
        CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_Moneda)

        'Parámetro Tipo Operación
        CG.ParametrosDevuelve(TablaParametro.TipoOperacion, True, DP_TipoOperacion)

        'Parámetro Modo Operación
        CG.ParametrosDevuelve(TablaParametro.CaracteristicaOperación, True, dp_mod_ope)

        'Parámetro Tipo Documento
        CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocumento)

    End Sub

    Private Sub InhabilitaCampos_Form()
        'TextBox
        'Validadores
        Me.Txt_RefrescaCollDSI.Text = 0
        Me.Txt_ItemDeudorProblemas.Text = 0
        Me.Txt_ItemOPE.Text = ""
        Me.Txt_ItemDSI.Text = ""

        'Campos a ingresar

        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.ReadOnly = False
        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"

        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.ReadOnly = False
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"

        Me.Txt_Raz_Soc.Text = ""
        Me.Txt_Raz_Soc.ReadOnly = True
        Me.Txt_Raz_Soc.CssClass = "clsDisabled"

        Me.Txt_FecIngreso.Text = ""
        Me.Txt_FecIngreso.ReadOnly = True
        Me.Txt_FecIngreso.CssClass = "clsDisabled"

        Me.Txt_CanDocumentos.Text = ""
        Me.Txt_CanDocumentos.ReadOnly = True
        Me.Txt_CanDocumentos.CssClass = "clsDisabled"

        Me.Txt_MontoFinanciar.Text = ""
        Me.Txt_MontoFinanciar.ReadOnly = True
        Me.Txt_MontoFinanciar.CssClass = "clsDisabled"
        'DropList
        Me.DP_Moneda.ClearSelection()
        Me.DP_Moneda.Enabled = False
        Me.DP_Moneda.CssClass = "clsDisabled"

        Me.DP_TipoOperacion.ClearSelection()
        Me.DP_TipoOperacion.Enabled = False
        Me.DP_TipoOperacion.CssClass = "clsDisabled"

        Me.DP_TipoDocumento.ClearSelection()
        Me.DP_TipoDocumento.Enabled = False
        Me.DP_TipoDocumento.CssClass = "clsDisabled"

        Me.dp_mod_ope.CssClass = "clsDisabled"
        Me.dp_mod_ope.Enabled = False
        'RadioButton

        Me.RBConCuotas.Enabled = False
        Me.RBConCuotas.Enabled = False
        Me.RB_ModoOpera.Enabled = False
        Me.RB_OpePuntual.Enabled = False
        Me.RB_Responsabilidad.Enabled = False
        'Button
        Me.Btn_Buscar.Enabled = True

        Me.Btn_Limpiar.Enabled = True

        Me.Btn_IntMas.Enabled = False

        Me.Btn_Anular.Enabled = False

        Me.Btn_Ingdoc.Enabled = False

        btn_asoc_cheque.Enabled = False
        btn_ing_chq.Enabled = False

        coll_ope = New Collection
        Coll_DSI = New Collection

        Me.Btn_ModDoc.Enabled = False

        Me.Btn_EliDoc.Enabled = False

        Me.Gr_Documentos.Controls.Clear()
        Me.GR_OPERACIONES.Controls.Clear()
        IB_AyudaCli.Enabled = True

        Txt_Rut_Cli_MaskedEditExtender.Enabled = True

        GR_OPERACIONES.DataSource = New Collection
        GR_OPERACIONES.DataBind()

        Gr_Documentos.DataSource = New Collection
        Gr_Documentos.DataBind()


        Me.Btn_Actas.Enabled = False

    End Sub

    Private Sub RetornaDoc()

        Try

            Dim itemIdx As Int16
            Dim ope As New Object

            'Buscamos la posicion dentro de la grilla
            itemIdx = Txt_ItemOPE.Text

            RUT_CLI_RPT = coll_ope.Item(itemIdx).cli_idc
            ID_OPE_RPT = coll_ope.Item(itemIdx).id_ope
            NroRow = Val(itemIdx)

            Try

                With coll_ope.Item(itemIdx)
                    'TextBox
                    Me.Txt_FecIngreso.Text = CDate(.opn_fec_neg).ToShortDateString
                    Me.Txt_CanDocumentos.Text = .opn_can_doc
                    'Me.Txt_CanDocumentos.CssClass = "clstxt"
                    Me.Txt_CanDocumentos.ReadOnly = False

                    If coll_ope.Item(itemIdx).id_p_0023 = 1 Then
                        Me.Txt_MontoFinanciar.Text = Format(.opn_mto_doc, fmt.FCMSD)
                    ElseIf coll_ope.Item(itemIdx).id_p_0023 = 3 Or coll_ope.Item(itemIdx).id_p_0023 = 4 Then
                        Me.Txt_MontoFinanciar.Text = Format(.opn_mto_doc, fmt.FCMCD)
                    Else
                        Me.Txt_MontoFinanciar.Text = Format(.opn_mto_doc, fmt.FCMCD4)
                    End If

                    Me.Txt_MontoFinanciar.ReadOnly = False

                    'DropList
                    Me.DP_TipoOperacion.ClearSelection()
                    BuscaCombo(Me.DP_TipoOperacion, CStr(.id_P_0012))
                    Me.DP_TipoDocumento.ClearSelection()
                    BuscaCombo(Me.DP_TipoDocumento, CStr(.id_P_0031))
                    Me.DP_Moneda.ClearSelection()
                    BuscaCombo(Me.DP_Moneda, CStr(.id_P_0023))
                    Me.dp_mod_ope.ClearSelection()
                    BuscaCombo(Me.dp_mod_ope, CStr(.id_p_0030))

                    If Trim(.ope_cuo) = "S" Then
                        Me.RBConCuotas.SelectedIndex = 0
                    Else
                        Me.RBConCuotas.SelectedIndex = 1
                    End If

                    If Trim(.ope_ptl) = "S" Then
                        Me.RB_OpePuntual.SelectedIndex = 0
                    Else
                        Me.RB_OpePuntual.SelectedIndex = 1
                    End If

                    If Trim(.ope_res_son) = 1 Then
                        Me.RB_Responsabilidad.SelectedIndex = 0
                    Else
                        Me.RB_Responsabilidad.SelectedIndex = 1
                    End If

                    If Trim(.ope_lnl) = "S" Or IsNothing(.ope_lnl) Then
                        Me.RB_ModoOpera.SelectedIndex = 0
                    Else
                        Me.RB_ModoOpera.SelectedIndex = 1
                    End If

                    'Retorna Collection Documentos (DSI) y Despliega Grilla
                    idx = 1

                    Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemIdx).id_ope, coll_ope.Item(itemIdx).id_ope)

                    Gr_Documentos.DataSource = Coll_DSI
                    Gr_Documentos.DataBind()

                    If Me.Gr_Documentos.Rows.Count > 0 Then

                        Me.btn_next_otg.Enabled = True
                        Me.btn_prev_otg.Enabled = True
                        Btn_ModDoc.Enabled = True ' se habilita solo si existen documentos para modificar
                    Else
                        Btn_ModDoc.Enabled = False

                    End If

                    If coll_ope.Item(itemIdx).id_p_0031 <> 3 Then 'se habilita solo si tipo documento negociacion es diferente a cheque
                        Me.btn_ing_chq.Enabled = True
                        Me.btn_asoc_cheque.Enabled = True
                    Else
                        Me.btn_ing_chq.Enabled = False
                        Me.btn_asoc_cheque.Enabled = False
                    End If

                    For i = 0 To Me.Gr_Documentos.Rows.Count - 1

                        If coll_ope.Item(itemIdx).ope_cuo = "S" Then
                            ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO
                            'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                            '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                            '    Gr_Documentos.Rows(i).Enabled = False
                            'End If
                        End If

                        Me.Gr_Documentos.Rows(i).Cells(1).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text))

                        If coll_ope.Item(itemIdx).id_p_0023 = 1 Then
                            Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMSD)
                        ElseIf coll_ope.Item(itemIdx).id_p_0023 = 3 Or coll_ope.Item(itemIdx).id_p_0023 = 4 Then
                            Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD)
                        Else
                            Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD4)
                        End If

                    Next

                    If coll_ope.Item(itemIdx).id_p_0023 = 2 Then
                        Me.Txt_mto_MaskedEditExtender.Enabled = False
                        Txt_MontoFinanciar.Attributes.Add("Style", "TEXT-ALIGN: right")

                    ElseIf coll_ope.Item(itemIdx).id_p_0023 = 3 Or 4 Then
                        Me.Txt_mto_MaskedEditExtender.Enabled = False
                        Txt_MontoFinanciar.Attributes.Add("Style", "TEXT-ALIGN: right")
                    End If

                    'Valida Operación Descuadrada
                    ValidaOperacionCuadrada()

                End With

                marcagrilla()


                'Button
                Me.Btn_Buscar.Enabled = False
                Me.Btn_Limpiar.Enabled = True

                Me.Btn_IntMas.Enabled = True
                Me.Btn_Anular.Enabled = True
                Me.Btn_EliDoc.Enabled = True
                Me.Btn_Ingdoc.Enabled = True
                Me.Btn_Ingdoc.Enabled = True

                If Me.RBConCuotas.SelectedIndex = 0 Then
                    'Cuo = 1
                ElseIf Me.RBConCuotas.SelectedIndex = 1 Then
                    ' Cuo = 0
                End If

                Btn_Ingdoc.Attributes.Add("onClick", "WinOpen(2, 'IngresoDoctos.aspx?itemOPE=" & Txt_ItemOPE.Text & "','OpenPopupIngDoctos',900,450,100,100);")
                Btn_IntMas.Attributes.Add("onClick", "WinOpen(2, 'carga_masiva.aspx?itemOPE=" & Txt_ItemOPE.Text & "','IntegracionMasiva', 1200, 580, 100, 100);")

            Catch ex As Exception
                msj.Mensaje(Me.Page, "Error", ex.Message, 1)
            End Try

            Me.Txt_CanDocumentos.ReadOnly = True
            Me.Txt_MontoFinanciar.ReadOnly = True

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Private Function CargaOperacion() As ope_cls
        Try

            Dim Ope As New ope_cls

            With Ope

                .id_P_0030 = 1
                'RadioButton            
                If Me.RBConCuotas.SelectedIndex = 0 Then
                    .ope_cuo = "S"
                Else
                    .ope_cuo = "N"
                End If
                If Me.RB_OpePuntual.SelectedIndex = 0 Then
                    .ope_ptl = "S"
                Else
                    .ope_ptl = "N"
                End If

                If Me.RB_Responsabilidad.SelectedIndex = 0 Then
                    .ope_res_son = 1
                Else
                    .ope_res_son = 0
                End If
                If Me.RB_ModoOpera.SelectedIndex = 0 Then
                    .ope_lnl = "S"
                Else
                    .ope_lnl = "N"
                End If

                If Gr_Documentos.Rows.Count > 0 Then
                    If Me.Gr_Documentos.Rows(0).Cells(5).Text = Me.Txt_MontoFinanciar.Text Then
                        .ope_cdo = "S"
                    Else
                        .ope_cdo = "N"
                    End If
                Else
                    .ope_cdo = "N"
                End If
                .ope_cnt = ""
            End With

            Session("Operacion") = Ope
            Return Ope

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub RefrescaFormAfterUpdate()

        Try

            Me.Txt_RefrescaCollDSI.Text = 0
            Me.Txt_ItemDeudorProblemas.Text = 0
            Me.Txt_ItemOPE.Text = ""
            Me.Txt_ItemDSI.Text = ""

            'Campos a ingresar
            Me.Txt_FecIngreso.Text = ""
            Me.Txt_FecIngreso.ReadOnly = True
            Me.Txt_FecIngreso.CssClass = "clsDisabled"
            Me.Txt_CanDocumentos.Text = ""
            Me.Txt_CanDocumentos.ReadOnly = True
            Me.Txt_CanDocumentos.CssClass = "clsDisabled"
            Me.Txt_MontoFinanciar.Text = ""
            Me.Txt_MontoFinanciar.ReadOnly = True
            Me.Txt_MontoFinanciar.CssClass = "clsDisabled"

            'DropList
            Me.DP_Moneda.ClearSelection()
            Me.DP_Moneda.Enabled = False
            Me.DP_Moneda.CssClass = "clsDisabled"
            Me.DP_TipoOperacion.ClearSelection()
            Me.DP_TipoOperacion.Enabled = False
            Me.DP_TipoOperacion.CssClass = "clsDisabled"
            Me.DP_TipoDocumento.ClearSelection()
            Me.DP_TipoDocumento.Enabled = False
            Me.DP_TipoDocumento.CssClass = "clsDisabled"
            Me.dp_mod_ope.Enabled = False
            Me.dp_mod_ope.CssClass = "clsDisabled"

            'RadioButton
            Me.RBConCuotas.Enabled = False
            Me.RBConCuotas.Enabled = False
            Me.RB_ModoOpera.Enabled = False
            Me.RB_OpePuntual.Enabled = False
            Me.RB_Responsabilidad.Enabled = False

            'Button
            Me.Btn_Buscar.Enabled = False
            Me.Btn_Limpiar.Enabled = True
            Me.Btn_IntMas.Enabled = False
            Me.Btn_Anular.Enabled = False
            Me.Btn_Ingdoc.Enabled = False

            'Llena Objeto OPE (ingresadas)
            Me.GR_OPERACIONES.SelectedIndex = 0
            Me.GR_OPERACIONES.DataBind()

            idx = 0
            coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 1, True, GR_OPERACIONES)

            If Trim(Me.Txt_Dig_Cli.Text) <> FC.Vrut(Me.Txt_Rut_Cli.Text) Then
                Exit Sub
            End If

            Me.GR_OPERACIONES.DataSource = coll_ope
            Me.GR_OPERACIONES.DataBind()
            Dim I As Integer, X = 1

            DESCUADRE.Clear()

            For I = 0 To GR_OPERACIONES.Rows.Count - 1

                If coll_ope.Item(X).OPE_CDO = "N" Then
                    DESCUADRE.Add(X - 1)
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.Red
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.Red
                    Me.GR_OPERACIONES.Rows(I).Cells(1).Font.Bold = True
                    Me.GR_OPERACIONES.Rows(I).Cells(0).Font.Bold = True
                End If
                X = X
            Next

            marcagrilla()
            RefrescaDocumentos()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ValidaOperacionCuadrada()

        Dim CantDocumentos As Long
        Dim MontoDocumentos As Double
        Dim Calificacion As String = ""
        Dim i As Integer

        Try

            'Validar Descuadre de Operación Solo si operación es modificada (No valida para operaciones nuevas)
            If Me.Txt_ItemOPE.Text <> "" Then
                CantDocumentos = 0
                MontoDocumentos = 0
                If IsNothing(Coll_DSI) = False Then
                    For i = 1 To Coll_DSI.Count
                        If Coll_DSI(i).dsi_flj_num >= 0 Then
                            CantDocumentos = CantDocumentos + 1
                        End If
                        If Coll_DSI(i).dsi_flj = "N" Then
                            MontoDocumentos = MontoDocumentos + CDbl(Coll_DSI(i).dsi_mto_fin)
                        End If
                        If Coll_DSI(i).cal_oto_gam = "" Then
                            Calificacion &= Coll_DSI(i).dsi_num & ", "
                        End If
                    Next
                End If

                If Calificacion.Length > 0 Then
                    msj.Mensaje(Me.Page, "Atención ", "Existen documentos sin Calificación de Otorgamiento: " & Calificacion, ClsMensaje.TipoDeMensaje._Excepcion)
                    Exit Sub
                End If

                If (Math.Round(MontoDocumentos, 4) <> Math.Round(CDbl(Me.Txt_MontoFinanciar.Text), 4) Or (CantDocumentos <> CLng(Me.Txt_CanDocumentos.Text))) Then

                    MontoDocumentos = CDbl(Me.Txt_MontoFinanciar.Text) - MontoDocumentos
                    CantDocumentos = CLng(Me.Txt_CanDocumentos.Text) - CantDocumentos

                    Try

                        msj.Mensaje(Me.Page, "Atención ", "Operación  Descuadrada: " & Chr(13) & _
                                                          "Cant.Doctos: " & CantDocumentos & "" & Chr(13) & _
                                                          "Monto.Operación: " & Format(MontoDocumentos, fmt.FCMSD) & "", 3)

                    Catch ex As Exception
                        'msj.mensaje(me.page,"Error", ex.Message, 1)
                    End Try

                    If Me.Txt_CanDocumentos.Text < Me.Gr_Documentos.Rows.Count Then
                        Me.Btn_Ingdoc.Enabled = False
                    Else
                        Me.Btn_Ingdoc.Enabled = True
                    End If

                End If

                Me.Btn_EliDoc.Enabled = True

            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub GR_OPERACIONES_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GR_OPERACIONES.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
            End If
        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Public Sub alinea_textos()
        Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_MontoFinanciar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_CanDocumentos.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Protected Sub Gr_Documentos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gr_Documentos.PageIndexChanging
        Try

            Dim itemidx As Integer

            Gr_Documentos.PageIndex = e.NewPageIndex

            'itemidx = Val(Me.Txt_ItemOPE.Text) + 1
            itemidx = Val(Me.Txt_ItemOPE.Text)
            Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemidx).id_ope, coll_ope.Item(itemidx).id_ope)

            Gr_Documentos.DataSource = Coll_DSI
            Gr_Documentos.DataBind()


            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                If coll_ope.Item(itemidx).ope_cuo = "S" Then

                    ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO
                    'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                    '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                    '    Gr_Documentos.Rows(i).Enabled = False
                    'End If

                End If

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text))
                'Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)

                If coll_ope.Item(itemidx).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)

                ElseIf coll_ope.Item(itemidx).id_p_0023 = 3 Or coll_ope.Item(itemidx).id_p_0023 = 4 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)

                Else

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)

                End If

            Next

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Gr_Documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Documentos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Not IsPostBack Then
                sesion.Modulo = "Operacion"
                Pagina = Page.AppRelativeVirtualPath
                CambioTema(Page)
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Public Sub marcagrilla()

        Try

            Dim i, x As Integer

            For i = 0 To Me.GR_OPERACIONES.Rows.Count - 1

                If i = Val(Txt_ItemOPE.Text - 1) Then
                    GR_OPERACIONES.Rows(i).BackColor = Nothing
                    GR_OPERACIONES.Rows(i).CssClass = "selectable"
                Else
                    If DESCUADRE.Contains(i) Then
                        For x = 0 To DESCUADRE.Count - 1
                            If DESCUADRE.Item(x) = i Then
                                Me.GR_OPERACIONES.Rows(i).BackColor = Drawing.Color.OrangeRed
                                Me.GR_OPERACIONES.Rows(i).CssClass = "formatUltcell"
                                Exit For
                            End If
                        Next
                    Else
                        GR_OPERACIONES.Rows(i).BackColor = Nothing
                        GR_OPERACIONES.Rows(i).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

            If Val(Me.Txt_RefrescaCollDSI.Text) > 0 Then

                RefrescaDocumentos()

                If Trim(Me.Txt_Dig_Cli.Text) <> FC.Vrut(Me.Txt_Rut_Cli.Text) Then
                    msj.Mensaje(Me.Page, "Error", "NIT de Cliente Incorrecto", 1)
                    Exit Sub
                End If

                Me.Txt_RefrescaCollDSI.Text = 0

            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub btn_prev_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_otg.Click

        Try

            Dim itemidx As Integer

            'itemidx = Val(Me.Txt_ItemOPE.Text) + 1
            itemidx = Val(Me.Txt_ItemOPE.Text)

            If page_dig = 0 Then

                msj.Mensaje(Me, "Atención", "Ya has llegado al comienzo de la lista", 2)
                Exit Sub

            End If
            page_otg = page_otg - 8

            Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemidx).id_ope, coll_ope.Item(itemidx).id_ope)

            Gr_Documentos.DataSource = Coll_DSI
            Gr_Documentos.DataBind()



            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                If coll_ope.Item(itemidx).ope_cuo = "S" Then

                    ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO
                    'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                    '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                    '    Gr_Documentos.Rows(i).Enabled = False
                    'End If

                End If

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text))
                'Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)

                If coll_ope.Item(itemidx).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)

                ElseIf coll_ope.Item(itemidx).id_p_0023 = 3 Or coll_ope.Item(itemidx).id_p_0023 = 4 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)

                Else

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)

                End If

            Next


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try

    End Sub

    Protected Sub btn_next_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_otg.Click

        Try

            Dim itemidx As Integer

            itemidx = Val(Me.Txt_ItemOPE.Text)

            If Me.Gr_Documentos.Rows.Count < 8 Then
                msj.Mensaje(Me, "Atención", "Ya está en la ultima página de la lista", 2)
                Exit Sub
            End If
            page_otg = page_otg + 8

            Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemidx).id_ope, coll_ope.Item(itemidx).id_ope)

            Gr_Documentos.DataSource = Coll_DSI
            Gr_Documentos.DataBind()



            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                If coll_ope.Item(itemidx).ope_cuo = "S" Then

                    ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO
                    'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                    '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                    '    Gr_Documentos.Rows(i).Enabled = False
                    'End If

                End If

                Me.Gr_Documentos.Rows(i).Cells(0).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(0).Text))
                'Me.Gr_Documentos.Rows(i).Cells(2).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(2).Text), fmt.FCMSD)

                If coll_ope.Item(itemidx).id_p_0023 = 1 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMSD)

                ElseIf coll_ope.Item(itemidx).id_p_0023 = 3 Or coll_ope.Item(itemidx).id_p_0023 = 4 Then

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD)

                Else

                    Me.Gr_Documentos.Rows(i).Cells(4).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(4).Text), fmt.FCMCD4)

                End If




            Next


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try
    End Sub

    Protected Sub btn_prev_ope_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_ope.Click

        Try


            If page_dig = 0 Then

                msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub

            End If

            page_dig = page_dig - 5

            coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 1)

            If Not IsNothing(coll_ope) Then

                If coll_ope.Count > 0 Then

                    Me.Txt_ItemOPE.Text = ""
                    Me.Gr_Documentos.DataSource = Nothing
                    Me.Gr_Documentos.DataBind()
                End If
            End If


            Me.GR_OPERACIONES.DataSource = coll_ope
            Me.GR_OPERACIONES.DataBind()
            Dim x As Integer = 1
            Dim I As Integer
            DESCUADRE.Clear()

            For I = 0 To GR_OPERACIONES.Rows.Count - 1
                If coll_ope.Item(x).ope_cdo = "N" Then
                    DESCUADRE.Add(x - 1)
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                    Me.GR_OPERACIONES.Rows(I).Cells(1).Font.Bold = True
                    Me.GR_OPERACIONES.Rows(I).Cells(0).Font.Bold = True
                Else
                    Me.GR_OPERACIONES.Rows(I).CssClass = "formatable"

                End If
                x = x + 1
            Next


            'End If

            If Me.Txt_ItemOPE.Text <> "" Then
                RefrescaDocumentos()

            Else
                ''Button
                Me.Btn_Buscar.Enabled = False
                Me.Btn_Limpiar.Enabled = True
                'Me.Btn_IntMas.Enabled = True
                Me.Btn_Anular.Enabled = False
                Me.Btn_Ingdoc.Enabled = False
            End If


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try
    End Sub

    Protected Sub btn_next_ope_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_ope.Click
        Try



            If Me.GR_OPERACIONES.Rows.Count < 5 Or GR_OPERACIONES.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            page_dig = page_dig + 5

            coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 1)



            If Not IsNothing(coll_ope) Then

                If coll_ope.Count > 0 Then

                    Me.Txt_ItemOPE.Text = ""
                    Me.Gr_Documentos.DataSource = Nothing
                    Me.Gr_Documentos.DataBind()
                End If
            End If

            GR_OPERACIONES.DataSource = coll_ope
            GR_OPERACIONES.DataBind()


            If GR_OPERACIONES.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If


            Dim x As Integer = 1
            Dim I As Integer
            DESCUADRE.Clear()

            For I = 0 To GR_OPERACIONES.Rows.Count - 1
                If coll_ope.Item(x).ope_cdo = "N" Then
                    DESCUADRE.Add(x - 1)
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                    Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                    Me.GR_OPERACIONES.Rows(I).Cells(1).Font.Bold = True
                    Me.GR_OPERACIONES.Rows(I).Cells(0).Font.Bold = True
                Else
                    Me.GR_OPERACIONES.Rows(I).CssClass = "formatable"

                End If
                x = x + 1
            Next




            If Me.Txt_ItemOPE.Text <> "" Then

                If Not IsNothing(coll_ope) Then
                    If coll_ope.Count > 0 Then
                        RefrescaDocumentos()
                    End If
                End If
            Else
                ''Button
                Me.Btn_Buscar.Enabled = False
                Me.Btn_Limpiar.Enabled = True
                'Me.Btn_IntMas.Enabled = True
                Me.Btn_Anular.Enabled = False
                Me.Btn_Ingdoc.Enabled = False
            End If





        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try

    End Sub

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Lb_buscar_Click(Me, e)
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'Detalle de la operacion
        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            'Buscamos la posicion dentro de la grilla
            For i = 1 To coll_ope.Count
                If img.ToolTip = coll_ope.Item(i).id_ope Then
                    Txt_ItemOPE.Text = i
                    Exit For
                End If
            Next

            If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) > 1 Then
                msj.Mensaje(Page, "Atención", "Operación se encuentra simulada u otorgada.", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            RetornaDoc()

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub Img_Ver_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Detalle del documento
        Try

            Dim img As ImageButton = CType(sender, ImageButton)

            For i = 1 To Coll_DSI.Count
                If Coll_DSI(i).dsi_num = img.ToolTip And Coll_DSI(i).dsi_flj_num = img.AlternateText Then
                    Txt_ItemDSI.Text = i
                    Exit For
                End If
            Next

            If Txt_ItemDSI.Text <> "" Then

                If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) > 1 Then
                    msj.Mensaje(Page, "Atención", "Operación se encuentra simulada u otorgada.", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                MarcaGrillaDoctos()
                Btn_ModDoc.Attributes.Add("onClick", "WinOpen(2, 'IngresoDoctos.aspx?itemDSI=" & Txt_ItemDSI.Text & "&itemOPE=" & Txt_ItemOPE.Text & "','OpenPopupModDoctos',800,450,100,100);")
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub GR_OPERACIONES_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GR_OPERACIONES.SelectedIndexChanged

    End Sub

    Private Sub cargadoctos()

        Dim itemIdx As Integer = Txt_ItemOPE.Text
        Coll_DSI = OP.documentosIngresados_RetornaSinPag(coll_ope.Item(itemIdx).id_ope, coll_ope.Item(itemIdx).id_ope)

        Gr_Documentos.DataSource = Coll_DSI
        Gr_Documentos.DataBind()

        If Me.Gr_Documentos.Rows.Count > 0 Then
            Me.btn_next_otg.Enabled = True
            Me.btn_prev_otg.Enabled = True
            Btn_ModDoc.Enabled = True ' se habilita solo si existen documentos para modificar
        Else
            Btn_ModDoc.Enabled = False
            Exit Sub
        End If

        For i = 0 To Me.Gr_Documentos.Rows.Count - 1
            If coll_ope.Item(itemIdx).ope_cuo = "S" Then

                ''SE COMENTA PARA MANTENER ACTIVO BOTONES Y QUE NO LOS MARQUE COMO FLUJO DE PAGO
                'If Gr_Documentos.Rows(i).Cells(4).Text = 0 Then
                '    Gr_Documentos.Rows(i).BackColor = Drawing.Color.Yellow
                '    Gr_Documentos.Rows(i).Enabled = False
                'End If

            End If

            Me.Gr_Documentos.Rows(i).Cells(1).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & FC.Vrut(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text))
            'Me.Gr_Documentos.Rows(i).Cells(3).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(3).Text), fmt.FCMSD)

            If coll_ope.Item(itemIdx).id_p_0023 = 1 Then

                Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMSD)

            ElseIf coll_ope.Item(itemIdx).id_p_0023 = 3 Or coll_ope.Item(itemIdx).id_p_0023 = 4 Then

                Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD)

            Else

                Me.Gr_Documentos.Rows(i).Cells(5).Text = Format(CDbl(Me.Gr_Documentos.Rows(i).Cells(5).Text), fmt.FCMCD4)

            End If




        Next
    End Sub

    Private Sub MarcaGrillaDoctos()

        For I = 0 To Gr_Documentos.Rows.Count - 1

            If (Coll_DSI(CInt(Txt_ItemDSI.Text)).dsi_num = Gr_Documentos.Rows(I).Cells(3).Text) And Coll_DSI(CInt(Txt_ItemDSI.Text)).dsi_flj_num = Gr_Documentos.Rows(I).Cells(4).Text Then
                If (I Mod 2) = 0 Then
                    Gr_Documentos.Rows(I).CssClass = "selectable"
                Else
                    Gr_Documentos.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    Gr_Documentos.Rows(I).CssClass = "formatUltcellAlt"
                Else
                    Gr_Documentos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next
    End Sub

    Private Sub MarcaOperacionesDescuadradas()
        Dim x As Integer = 1

        DESCUADRE.Clear()

        For i = 0 To GR_OPERACIONES.Rows.Count - 1
            If coll_ope.Item(x).ope_cdo = "N" Then
                DESCUADRE.Add(x - 1)
                Me.GR_OPERACIONES.Rows(i).BackColor = Drawing.Color.OrangeRed
                Me.GR_OPERACIONES.Rows(i).Cells(0).Font.Bold = True
            Else
                Me.GR_OPERACIONES.Rows(i).CssClass = "formatUltcellAlt"
            End If
            x = x + 1
        Next
    End Sub

#End Region

#Region "Botonera"

    Protected Sub Btn_Actas_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Actas.Click
        RW.AbrePopup(Me, 1, "Popup_ver_Actas.aspx?ID=" & Txt_Rut_Cli.Text, "Actas Asociadas", 650, 310, 100, 100)
    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Buscar.Click
        Try


            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20010205, Usr, "PRESIONA BOTON BUSCAR OPERACIONES") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If IsNothing(Session("Cliente")) Then
                Me.Lb_buscar_Click(Lb_buscar, e)
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Btn_ModDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_ModDoc.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal
            Dim sw As Boolean = False


            If Not Agt.ValidaAccesso(20, 20080205, Usr, "PRESIONA BOTON MODIFICAR DOCUMENTOS") Then

                Me.Btn_ModDoc.Attributes.Add("OnClick", "DenegaAcceso();")

            Else

                If Txt_ItemDSI.Text = "" Then
                    msj.Mensaje(Page, "Atención", "Seleccione documento para modificar", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Txt_ItemOPE.Text = Val(Txt_ItemOPE.Text)

            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try

    End Sub

    Protected Sub Btn_Ingdoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Ingdoc.Click

        If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) > 1 Then
            msj.Mensaje(Page, "Atención", "No se puede ingresar documentos", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Not Agt.ValidaAccesso(20, 20070205, Usr, "PRESIONA BOTON INGRESAR DOCUMENTOS") Then
            Me.Btn_Ingdoc.Attributes.Add("OnClick", "DenegaAcceso();")
            Exit Sub
        End If


        'RW.AbrePopup(Me, 2, "IngresoDoctos.aspx?itemOPE=" & Txt_ItemOPE.Text, "OpenPopupIngDoctos", 800, 450, 100, 100)

    End Sub

    Protected Sub Btn_Anular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Anular.Click
        Try


            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20030205, Usr, "PRESIONA BOTON ANULAR OPERACIONES") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_ItemOPE.Text = "" Then
                msj.Mensaje(Page, "Atención", "Seleccione una operación para anular", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) > 1 Then
                msj.Mensaje(Page, "Atención", "Operación no se puede anular. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            msj.Mensaje(Me.Page, "Atención", "¿Desea Anular la Operación?", 4, lb_anu.UniqueID)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Btn_EliDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_EliDoc.Click
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20090205, Usr, "PRESIONA BOTON ELIMINAR DOCUMENTOS") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Txt_ItemDSI.Text = "" Then
                msj.Mensaje(Me.Page, "Atención", "Debe seleccionar un documento para eliminar", 3)
                Exit Sub
            End If

            If OP.ValidaEstadoOperacion(coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) > 1 Then
                msj.Mensaje(Page, "Atención", "Documento no se puede eliminar", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            msj.Mensaje(Me.Page, "Atención", "¿Desea eliminar el documento?", 4, lb_eli_doc.UniqueID)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpiar.Click
        InhabilitaCampos_Form()
        Me.Txt_Rut_Cli.Focus()
    End Sub

    Protected Sub Btn_gua_ope_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_gua_ope.Click
        Try

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20040205, Usr, "PRESIONA BOTON MODIFICAR OPERACION") Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Raz_Soc.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If IsNothing(GR_OPERACIONES) Then
                If GR_OPERACIONES.Rows.Count = 0 Then
                    msj.Mensaje(Page, "Atención", "Este cliente no tiene operaciones", ClsMensaje.TipoDeMensaje._Exclamacion)
                End If
                Exit Sub
            End If

            If Txt_ItemOPE.Text = "" Then
                msj.Mensaje(Page, "Atención", "Seleccione una operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            msj.Mensaje(Me.Page, "Atención", "¿Desea Modificar la Operación?", 4, lb_guar.UniqueID)

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub btn_ing_chq_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ing_chq.Click

        If Not Agt.ValidaAccesso(20, 20050205, Usr, "PRESIONA BOTON INGRESO DE CHEQUES") Then
            msj.Mensaje(Page, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Error)
            Exit Sub
        End If

        RW.AbrePopup(Me, 2, "Ingreso Cheques.aspx?itemOPE=" & Txt_ItemOPE.Text, "IngresoCheques", 700, 500, 100, 100)

    End Sub

    Protected Sub btn_asoc_cheque_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asoc_cheque.Click

        If Not Agt.ValidaAccesso(20, 20060205, Usr, "PRESIONA BOTON RESPALDO DE CHEQUES") Then
            msj.Mensaje(Page, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Error)
            Exit Sub
        End If

        RW.AbrePopup(Me, 2, "Resp_Doctos.aspx?itemOPE=" & Txt_ItemOPE.Text, "IngresoCheques", 1000, 500, 100, 100)

    End Sub

    Protected Sub Btn_IntMas_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_IntMas.Click
        If Not Agt.ValidaAccesso(20, 20020205, Usr, "PRESIONA BOTON INTEGRACION MASIVA") Then
            Me.Btn_Ingdoc.Attributes.Add("OnClick", "DenegaAcceso();")
            Exit Sub
        End If
    End Sub

#End Region

#Region "LinkButton"

    Public Sub RetornaDoctos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetornaDoctos.Click

        Try

            RetornaDoc()
        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub Lb_grilla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        marcagrilla()

    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim Sesion As New ClsSession.ClsSession
            Dim evnt As System.Web.UI.ImageClickEventArgs
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

            Dim Cli As cli_cls

            Cli = clasecli.ClientesDevuelve(Txt_Rut_Cli.Text.Trim, Txt_Dig_Cli.Text)

            ' Validaciones de RUT y Digito Verificador
            'si cliente no existe sale de  function


            If valida_cliente <> "" Then
                msj.Mensaje(Me.Page, "Atención", valida_cliente, 3)
                Me.Btn_Limpiar_Click(Me.Btn_Limpiar, evnt)
                Exit Sub
            Else
                If IsNothing(Cli) Then
                    msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                Session("Cliente") = Cli

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Me.Btn_Actas.Enabled = True

                'Asigna Razón Social / Nombre a Campo Cliente
                Me.Txt_Raz_Soc.Text = Trim(Cli.cli_rso) & " " & Trim(Cli.cli_ape_ptn) & " " & Trim(Cli.cli_ape_mtn)

                coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 1, True, GR_OPERACIONES)

                'Buscamos la posicion dentro de la grilla
                Dim i As Integer

                For i = 1 To coll_ope.Count
                    If Request.QueryString("id") = coll_ope.Item(i).id_ope Then
                        Txt_ItemOPE.Text = i
                        Exit For
                    End If
                Next

                Txt_Rut_Cli.Text = Format(CDbl(Txt_Rut_Cli.Text), fmt.FCMSD)

                If coll_ope.Count = 0 Then
                    InhabilitaCampos_Form()
                    Me.Txt_Rut_Cli.Focus()
                    msj.Mensaje(Me.Page, "Atención", "No existen Operaciones Digitadas para este cliente", 2)
                    Exit Sub
                End If

                Me.btn_next_ope.Enabled = True
                Me.btn_prev_ope.Enabled = True

                MarcaOperacionesDescuadradas()


            End If

            If Me.Txt_ItemOPE.Text <> "" Then
                RefrescaDocumentos()
                marcagrilla()
            Else
                Me.Btn_Buscar.Enabled = False
                Me.Btn_Limpiar.Enabled = True
                Me.Btn_Anular.Enabled = False
                Me.Btn_Ingdoc.Enabled = False
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub lb_eli_doc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_eli_doc.Click

        Try

            Dim o, d As Integer

            o = Me.Txt_ItemOPE.Text
            d = Me.Txt_ItemDSI.Text

            OP.Documentos_Elimina(Coll_DSI.Item(d).ID_DSI, _
                                  coll_ope.Item(o).ID_OPE, _
                                  Me.Txt_Rut_Cli.Text, _
                                  Me.DP_TipoDocumento.SelectedValue, _
                                  Coll_DSI.Item(d).dsi_flj_num)

            Txt_ItemDSI.Text = ""

            Select Case OP.EstadoOperacion
                Case 1, 2
                    msj.Mensaje(Me, "Atención", OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
                Case 999
                    msj.Mensaje(Me, "Atención", "Error: " & OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
            End Select

            RetornaDoc()

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub

    Protected Sub lb_guar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guar.Click

        Try

            Dim ope As New ope_cls
            
            With ope
                .id_P_0104 = Me.dp_mod_ope.SelectedValue
                .id_ope = coll_ope.Item(Val(Txt_ItemOPE.Text)).id_ope
                .ope_ptl = Me.RB_OpePuntual.SelectedValue
                .ope_res_son = RB_Responsabilidad.SelectedValue
                .ope_cuo = RBConCuotas.SelectedValue
                .ope_lnl = RB_ModoOpera.SelectedValue
            End With

            OP.OperacionModifica(ope, 1)

            Select Case OP.EstadoOperacion
                Case 1, 2
                    msj.Mensaje(Me, "Atención", OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
                Case 999
                    msj.Mensaje(Me, "Atención", "Error: " & OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
            End Select

            Lb_buscar_Click(Me, e)
            RetornaDoc()

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try

    End Sub

    Protected Sub lb_anu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_anu.Click
        Try

            If OP.Operaciones_Anula(Me.Txt_Rut_Cli.Text, coll_ope.Item(Val(Me.Txt_ItemOPE.Text)).id_ope) = True Then
                msj.Mensaje(Page, "Atención", "Operación anulada", 2)
                coll_ope = New Collection

                coll_ope = OP.OperacionesPorClienteDevuelve(Me.Txt_Rut_Cli.Text, 1, True, GR_OPERACIONES)

                Me.btn_next_ope.Enabled = True
                Me.btn_prev_ope.Enabled = True
                Dim x As Integer = 1
                Dim I As Integer

                DESCUADRE.Clear()

                For I = 0 To GR_OPERACIONES.Rows.Count - 1

                    If coll_ope.Item(x).ope_cdo = "N" Then


                        DESCUADRE.Add(x - 1)
                        Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                        Me.GR_OPERACIONES.Rows(I).BackColor = Drawing.Color.OrangeRed
                        Me.GR_OPERACIONES.Rows(I).Cells(1).Font.Bold = True
                        Me.GR_OPERACIONES.Rows(I).Cells(0).Font.Bold = True
                    Else
                        Me.GR_OPERACIONES.Rows(I).CssClass = "formatUltcell"

                    End If
                    x = x + 1
                Next
                Me.Txt_FecIngreso.Text = ""
                Me.Txt_CanDocumentos.Text = ""
                Me.Txt_MontoFinanciar.Text = ""
                'DropList
                Me.DP_Moneda.ClearSelection()
                Me.DP_TipoOperacion.ClearSelection()
                Me.DP_TipoDocumento.ClearSelection()
                Me.dp_mod_ope.CssClass = "clsDisabled"
                Me.dp_mod_ope.Enabled = False
                'RadioButton
                Me.RBConCuotas.Enabled = False

                Me.RB_ModoOpera.Enabled = False
                Me.RB_OpePuntual.Enabled = False
                Me.RB_Responsabilidad.Enabled = False
                'Button


                Me.RBConCuotas.Enabled = False
                Me.RBConCuotas.Enabled = False
                Me.RB_ModoOpera.Enabled = False
                Me.RB_OpePuntual.Enabled = False
                Me.RB_Responsabilidad.Enabled = False


                'Button
                Me.Btn_Buscar.Enabled = False
                Me.Btn_Limpiar.Enabled = True
                Me.Btn_gua_ope.Enabled = False
                Me.Btn_ModDoc.Enabled = False

                Me.Btn_IntMas.Enabled = False
                Me.Btn_Anular.Enabled = False
                Me.btn_ing_chq.Enabled = False
                Me.btn_asoc_cheque.Enabled = False
                'Me.Btn_ModDoc.Enabled = True
                Me.Btn_EliDoc.Enabled = False
                Me.Btn_Ingdoc.Enabled = False
                Me.Btn_Ingdoc.Enabled = False

                Gr_Documentos.DataSource = New Collection
                Gr_Documentos.DataBind()

                Me.Txt_ItemDSI.Text = ""
                Me.Txt_ItemOPE.Text = ""

            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)
        End Try
    End Sub

#End Region

End Class
