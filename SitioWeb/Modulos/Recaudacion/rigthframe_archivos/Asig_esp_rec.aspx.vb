Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_Asi_esp_rec
    Inherits System.Web.UI.Page

    Dim fc As New FuncionesGenerales.FComunes
    Dim cg As New ConsultasGenerales
    Dim clasecli As New ClaseClientes
    Dim ag As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim sesion As New ClsSession.SesionOperaciones
    Dim fmoneda As New FuncionesGenerales.ClsLocateInfo
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim REC As New ClaseRecaudación
    Dim CBZ As New ClaseCobranza

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Public Sub alineatextos()

        Me.Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

        Me.txt_doc_des.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_oto_des.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_mto_des.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_mto_has.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

    End Sub

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged

        If Me.Ch_deu.Checked = True Then
            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.ReadOnly = False

            Me.Ib_ayudadeu.Enabled = True
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Dig_Deu.ReadOnly = False
            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.Focus()
        Else
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Rso_Deu.Text = ""
            Me.Ib_ayudadeu.Enabled = False
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Response.Expires = -1
            'cg.ParametrosDevuelve(TablaParametro.Ciudad, True, Me.DP_Ciudad)
            'cg.ParametrosDevuelve(TablaParametro.ComunaLocalidad, True, DP_Comuna)
            cg.ParametrosDevuelve(TablaParametro.Region, True, DP_Depto)
            cg.ParametrosDevuelve(TablaParametro.TipoDocumento, True, dr_tip_doc)
            cg.ParametrosDevuelve(TablaParametro.EstadoDocumento, True, dr_est_doc)
            cg.SucursalesDevuelve(codeje, True, Me.DP_SucRecaudacion)
            DP_CodCobranza.DataSource = CBZ.CodigoCobranza_RetornaGestionar(1) 'Retorna Codigos de Cobranza
            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()
            DP_CodCobranza.ClearSelection()

            Coll_DOC = New Collection
            habilita_campos(False)
            alineatextos()
            Me.Txt_Rut_Cli.Focus()
        End If

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx?PopUp=1','PopUpCliente',580,410,200,150);")
        Ib_ayudadeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/Ayudadeudor.aspx','PopUpCliente',580,410,200,150);")
        IB_VolverGestion.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub ib_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_buscar.Click
        Try

            Dim deu_ide, deu_ide1 As String
            Dim deudor As Boolean
            Dim venc_des, venc_has As DateTime
            Dim otg_num, otg_num2 As Long
            Dim doc_num, doc_num2 As String
            Dim mto_des, mto_has As Double

            If Txt_Rut_Cli.Text = "" Then
                msj.Mensaje(Me, "Atención", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                msj.Mensaje(Me, "Atención", "Ingrese dígito cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Me.txt_venc_des.Text = "" Then
                venc_des = "01/01/1900"
            Else
                If Not IsDate(txt_venc_des.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_venc_des.Text = ""
                    Exit Sub
                End If
                venc_des = Me.txt_venc_des.Text
            End If

            If Me.txt_venc_has.Text = "" Then
                venc_has = "31/12/2100"
            Else
                If Not IsDate(txt_venc_has.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha vencimiento hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_venc_has.Text = ""
                    Exit Sub
                End If
                venc_has = Me.txt_venc_has.Text
            End If

            If Me.txt_oto_des.Text = "" Then
                otg_num = 0
                otg_num2 = 9999999
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
                deudor = False
            End If


            If Me.txt_mto_des.Text = "" Then
                mto_des = 0
            Else
                mto_des = Me.txt_mto_des.Text
            End If

            If Me.txt_mto_has.Text = "" Then
                mto_has = 999999999
            Else
                mto_has = Me.txt_mto_has.Text
            End If


            If mto_des > mto_has Then
                msj.Mensaje(Me, "Atención", "Monto desde no puede ser mayor a monto hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CDate(venc_des) > CDate(venc_has) Then
                msj.Mensaje(Me, "Atención", "Fecha vencimiento desde no puede ser mayor a fecha vencimiento hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Coll_DOC = REC.Doctos_asig_esp_retorna(Format(CLng(Me.Txt_Rut_Cli.Text), fmt.FMT_RUT), deu_ide, deu_ide1, deudor, 0, venc_des, venc_has, otg_num, otg_num2, doc_num, doc_num2, mto_des, mto_has, dr_est_doc.SelectedValue, dr_tip_doc.SelectedValue)

            Me.GridView1.DataSource = Coll_DOC
            Me.GridView1.DataBind()


            For i = 0 To Me.GridView1.Rows.Count - 1
                Me.GridView1.Rows(i).Cells(1).Text = Format(CLng(Me.GridView1.Rows(i).Cells(1).Text), fmoneda.FCMSD) & "-" & fc.Vrut(CLng(Me.GridView1.Rows(i).Cells(1).Text))


                Select Case Coll_DOC.Item(i + 1).id_p_0023
                    Case 1
                        GridView1.Rows(i).Cells(8).Text = Format(CDbl(Coll_DOC.Item(i + 1).dsi_mto), fmoneda.FCMSD)
                    Case 2
                        GridView1.Rows(i).Cells(8).Text = Format(CDbl(Coll_DOC.Item(i + 1).dsi_mto), fmoneda.FCMCD4)
                    Case 3, 4
                        GridView1.Rows(i).Cells(8).Text = Format(CDbl(Coll_DOC.Item(i + 1).dsi_mto), fmoneda.FCMCD)
                End Select




            Next

            If Me.GridView1.Rows.Count > 0 Then

                IB_GuardaGestion.Enabled = True
                habilita_campos(True)
                Me.txt_FechaPago.Text = Date.Now.ToShortDateString
            Else
                habilita_campos(False)
                msj.Mensaje(Me, "Atención", "No se han encontrado datos ", 2)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub habilita_campos(ByVal valor As Boolean)

        If valor Then
            Me.Txt_direccion.CssClass = "clsMandatorio"
            Me.Txt_direccion.ReadOnly = False
            Me.DP_CodCobranza.CssClass = "clsMandatorio"
            Me.DP_CodCobranza.Enabled = True
            Me.DP_Depto.CssClass = "clsMandatorio"
            Me.DP_Depto.Enabled = True
            Me.DP_Ciudad.CssClass = "clsMandatorio"
            Me.DP_Ciudad.Enabled = True
            Me.DP_Comuna.CssClass = "clsMandatorio"
            Me.DP_Comuna.Enabled = True
            Me.txt_FechaPago.CssClass = "clsMandatorio"
            Me.txt_FechaPago.ReadOnly = False
            Me.RB_HORA.CssClass = "clsTxt"
            Me.RB_HORA.Enabled = True
            Me.txt_ObservacionGestion.CssClass = "clsMandatorio"
            Me.txt_ObservacionGestion.ReadOnly = False
            Me.DP_SucRecaudacion.CssClass = "clsMandatorio"
            Me.DP_SucRecaudacion.Enabled = True
            Me.CALE_FechaPago.Enabled = True
            Me.DP_SucRecaudacion.Enabled = True
            Me.Txt_direccion.ReadOnly = False

            Me.DP_CodCobranza.Enabled = True
            Me.DP_Ciudad.Enabled = True
            Me.txt_FechaPago.ReadOnly = False
            Me.RB_HORA.Enabled = True
            Me.txt_ObservacionGestion.ReadOnly = False
            Me.DP_Comuna.Enabled = True
        Else
            Me.DP_Depto.CssClass = "clsDisabled"
            Me.DP_Comuna.CssClass = "clsDisabled"
            Me.CALE_FechaPago.Enabled = False
            Me.Txt_direccion.CssClass = "clsDisabled"
            Me.txt_GESIdZona.CssClass = "clsDisabled"
            Me.DP_CodCobranza.CssClass = "clsDisabled"
            Me.DP_SucRecaudacion.CssClass = "clsDisabled"
            Me.DP_Ciudad.CssClass = "clsDisabled"
            Me.txt_FechaPago.CssClass = "clsDisabled"
            Me.RB_HORA.CssClass = "clsDisabled"
            Me.txt_ObservacionGestion.CssClass = "clsDisabled"

            Me.DP_SucRecaudacion.Enabled = False
            Me.Txt_direccion.ReadOnly = True
            Me.txt_GESIdZona.ReadOnly = True
            Me.DP_CodCobranza.Enabled = False
            Me.DP_Ciudad.Enabled = False
            Me.txt_FechaPago.ReadOnly = True
            Me.RB_HORA.Enabled = False
            Me.txt_ObservacionGestion.ReadOnly = True
            Me.DP_Comuna.Enabled = False
            Me.DP_Depto.Enabled = False

            Me.DP_Depto.ClearSelection()
            Me.DP_Comuna.ClearSelection()
            Me.Txt_direccion.Text = ""
            Me.txt_GESIdZona.Text = ""
            Me.DP_CodCobranza.ClearSelection()
            Me.DP_SucRecaudacion.ClearSelection()
            Me.DP_Ciudad.ClearSelection()
            Me.txt_FechaPago.Text = ""
            Me.RB_HORA.ClearSelection()
            Me.txt_ObservacionGestion.Text = ""

        End If

    End Sub

    Protected Sub ch_grid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub txt_FechaPago_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FechaPago.TextChanged
        If Trim(txt_FechaPago.Text) = "" Then
            'Retorna Codigos de Cobranza 
            DP_CodCobranza.DataSource = CBZ.CodigoCobranza_RetornaGestionar(1)
            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()


        Else
            'Retorna Codigos de Cobranza según su proridad
            DP_CodCobranza.DataSource = CBZ.CodigoCobranza_RetornaGestionar(2)
            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()

        End If
    End Sub

    Protected Sub IB_GuardaGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_GuardaGestion.Click
        Dim IDX As Integer

        Try

            If txt_FechaPago.Text = "" Then
                msj.Mensaje(Me, "Atención", "Ingrese fecha de pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(txt_FechaPago.Text) Then
                msj.Mensaje(Me, "Atención", "Fecha de pago erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_FechaPago.Text = ""
                Exit Sub
            End If


            If DP_SucRecaudacion.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar sucursal recaudación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_Depto.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar departamento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_Ciudad.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar municipio", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_Comuna.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar localidad", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_direccion.Text = "" Then
                msj.Mensaje(Me, "Atención", "Debe ingresar dirección de pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_CodCobranza.SelectedValue = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar estado de cobranza", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_ObservacionGestion.Text = "" Then
                msj.Mensaje(Me, "Atención", "Debe ingresar observación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim ch As CheckBox
            For i = 0 To GridView1.Rows.Count - 1
                ch = Me.GridView1.Rows(i).FindControl("ch_grid")
                If ch.Checked = True Then
                    IDX = i + 1
                    Exit For
                End If
            Next

            If IDX = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar  al menos un documento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            msj.Mensaje(Me, "Atención", "¿Desea guardar estos registros?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, )


        Catch ex As Exception
            msj.Mensaje(Me, "Error", ex.Message, 1)
        End Try
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        Try
            Dim gsn_ingreso As New gsn_cls
            Dim rst As Boolean
            Dim ch_pend As Integer
            Dim ch As CheckBox
            Dim ddi As New ddi_cls

            'Ciclo que recorre Documentos Seleccionados
            For i = 0 To GridView1.Rows.Count - 1

                ch = Me.GridView1.Rows(i).FindControl("ch_grid")

                If ch.Checked = True Then
                    IDX = i + 1

                    gsn_ingreso = New gsn_cls


                    gsn_ingreso.id_doc = sesion.Coll_DOC.Item(IDX).id_doc
                    gsn_ingreso.id_P_0011 = sesion.Coll_DOC.Item(IDX).id_P_0011
                    gsn_ingreso.doc_fev_rea = sesion.Coll_DOC.Item(IDX).dsi_fev_rea
                    gsn_ingreso.doc_sdo_cli = sesion.Coll_DOC.Item(IDX).doc_sdo_cli
                    gsn_ingreso.doc_sdo_ddr = sesion.Coll_DOC.Item(IDX).doc_sdo_ddr

                    gsn_ingreso.id_eje_cob = CodEje

                    ddi = New ddi_cls

                    ddi.id_cmn = Me.DP_Comuna.SelectedValue
                    ddi.deu_ide = sesion.Coll_DOC.Item(IDX).deu_ide
                    ddi.ddr_dml_cbz = "S"

                    gsn_ingreso.id_cco = DP_CodCobranza.SelectedValue
                    gsn_ingreso.gsn_fec = Date.Now.ToShortDateString
                    gsn_ingreso.gsn_hor = Date.Now ' TimeOfDay
                    gsn_ingreso.gsn_fec_pag = IIf(Me.txt_FechaPago.Text.Trim = "", Nothing, CDate(Me.txt_FechaPago.Text.Trim))
                    'gsn_ingreso.gsn_hor_pag_dde = Nothing
                    gsn_ingreso.gsn_hor_pag_dde = Date.Now
                    gsn_ingreso.gsn_hor_pag = Nothing
                    gsn_ingreso.gsn_obs = Mid(Me.txt_ObservacionGestion.Text.Trim, 1, 250)
                    gsn_ingreso.gsn_obs_1 = Mid(Me.txt_ObservacionGestion.Text.Trim, 251, 250)
                    gsn_ingreso.gsn_obs_2 = Mid(Me.txt_ObservacionGestion.Text.Trim, 501, 250)
                    gsn_ingreso.gsn_dir_cbz = IIf(Me.Txt_direccion.Text.Trim = "", Nothing, Me.Txt_direccion.Text.Trim)

                    rst = CBZ.GuardaGestion(gsn_ingreso, ddi, True, _
                                            Me.DP_SucRecaudacion.SelectedValue, _
                                            Me.DP_SucRecaudacion.SelectedValue, _
                                            ch_pend)
                    If Not rst Then
                        Exit For
                    End If

                End If

            Next

            If rst Then
                msj.Mensaje(Me, "Atención", "Se ha guardado la gestión de forma exitosa", 3)
                habilita_campos(False)
                rw.ClosePag(Me.Page)
            Else
                msj.Mensaje(Me, "Atención", CBZ.descripcionconsulta, 3)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DP_Ciudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Ciudad.SelectedIndexChanged
        'Retorna Comunas
        'cg.ComunaDevuelve(CStr(DP_Ciudad.SelectedIndex), True, DP_Comuna)

        Try
            'Comuna
            cg.ComunaDevuelve(CInt(DP_Ciudad.SelectedItem.Value), True, Me.DP_Comuna)
            DP_Comuna.ClearSelection()
            ' If Err.CodigoError = 99 Then Msj.Mensaje(Me.Page,Err.MsgError, TipoMsg.Exclamacion)
        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error ", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Function ValidaGuardaGestion() As Boolean

        ValidaGuardaGestion = False


        'Validación Fecha de Pago
        If Trim(txt_FechaPago.Text) <> "" Then







            'Validación Seleccion de Documento
            Dim CuentaDoctosSeleccionados As Int16 = 0
            For i = 0 To GridView1.Rows.Count - 1
                Dim varCHKBox As CheckBox

                'Busca Control CheckBox
                varCHKBox = GridView1.Rows(i).FindControl("CHB_SelDocto")

                'Valida Seleccion
                If varCHKBox.Checked Then
                    CuentaDoctosSeleccionados = 1
                    Exit For
                End If
            Next
            If CuentaDoctosSeleccionados = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un documento", 2)

                Exit Function
            End If

            'Valida si no existe fecha de pago ingrese Observación
            If Trim(txt_FechaPago.Text) = "" And Trim(txt_ObservacionGestion.Text) = "" Then
                msj.Mensaje(Me, "Atención", "Debe ingresar al menos observación de gestión", 2)
                MsgBox("Debe ingresar al menos observación de gestión", MsgBoxStyle.Exclamation, "Titulo Valor")
                Exit Function
            End If

            'Valida Dirección de Pago si ingresa fecha de pago
            If Trim(txt_FechaPago.Text) <> "" And (Trim(DP_Ciudad.Text) = "Seleccionar" Or Trim(DP_Ciudad.Text) = "" Or _
                                                   Trim(DP_Comuna.Text) = "Seleccionar" Or Trim(DP_Comuna.Text) = "" Or _
                                                   Trim(Txt_direccion.Text) = "") Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un documento", 2)
                Exit Function
                msj.Mensaje(Me, "Atención", "Al ingresar fecha de pago debe ingresar tambien :" & Chr(13) & "- Ciudad" _
                                                                            & Chr(13) & "- Comuna" _
                                                                            & Chr(13) & "- Dirección de Pago", 2)
                Exit Function

                ValidaGuardaGestion = True
            End If




        End If
    End Function

    Protected Sub DP_Comuna_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Comuna.SelectedIndexChanged
        Dim Temporal_zon = cg.ZonasComunaDevuelve(DP_SucRecaudacion.SelectedItem.Value, DP_Comuna.SelectedItem.Value)


        txt_GESZona0.Text = ""
        For Each zon1 In Temporal_zon
            txt_GESIdZona.Text = IIf(IsNothing(zon1.id_zon), 0, zon1.id_zon)
            txt_GESZona0.Text = IIf(IsNothing(zon1.zon_des), "", zon1.zon_des)
        Next

        Temporal_zon = Nothing
    End Sub

    Protected Sub IB_CancelarGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CancelarGestion.Click

        habilita_campos(False)
        Me.IB_AyudaCli.Enabled = True
        Me.dr_tip_doc.SelectedValue = 0
        Me.dr_est_doc.SelectedValue = 0
        Me.txt_oto_des.Text = ""
        Me.txt_doc_des.Text = ""
        Me.txt_mto_des.Text = ""
        Me.txt_mto_has.Text = ""


        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
        Me.Txt_Rut_Cli.ReadOnly = False

        Me.Txt_Raz_Soc.Text = ""

        Me.Txt_Raz_Soc.ReadOnly = True

        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
        Me.Txt_Dig_Cli.ReadOnly = False

        Me.Ch_deu.Checked = False

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Rut_Deu.CssClass = "clsDisabled"
        Me.Txt_Rut_Deu.ReadOnly = True

        Me.Txt_Rso_Deu.Text = ""
        Me.Txt_Rso_Deu.CssClass = "clsDisabled"
        Me.Txt_Rso_Deu.ReadOnly = True

        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Dig_Deu.CssClass = "clsDisabled"
        Me.Txt_Dig_Deu.ReadOnly = True

        txt_venc_des.Text = ""
        txt_venc_has.Text = ""

        'Limpiar Rut 
        '******************************************
        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()
        '******************************************

        'txt_FechaPago.Text = Date.Now.ToShortDateString
        txt_FechaPago.Text = ""
        DP_SucRecaudacion.ClearSelection()
        DP_Ciudad.ClearSelection()
        DP_Comuna.ClearSelection()
        txt_GESIdZona.Text = ""
        txt_GESZona0.Text = ""
        Txt_direccion.Text = ""
        DP_CodCobranza.ClearSelection()
        txt_ObservacionGestion.Text = ""


    End Sub

    Protected Sub txt_dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Dim cli As cli_cls
        Dim rut As String


        rut = CLng(Me.Txt_Rut_Cli.Text)
        cli = clasecli.ClientesDevuelve(rut, Me.Txt_Dig_Cli.Text)

        Session("Cliente") = cli

        If valida_cliente <> "" Then
            msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        Else
            If IsNothing(cli) Then
                msj.Mensaje(Me, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If

            Me.IB_AyudaCli.Enabled = False
            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut_Cli.ReadOnly = True
            Me.Txt_Rut_Cli.CssClass = "clsDisabled"
            Me.Txt_Dig_Cli.ReadOnly = True
            Me.Txt_Dig_Cli.CssClass = "clsDisabled"
            Me.Txt_Raz_Soc.ReadOnly = True
            Me.Txt_Raz_Soc.CssClass = "clsDisabled"

            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)

        End If

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Dim deu As deu_cls


        If Me.Txt_Rut_Deu.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Debe ingresar NIT", 2)
            Exit Sub
        End If
        If UCase(Me.Txt_Dig_Deu.Text) <> fc.Vrut(Me.Txt_Rut_Deu.Text) Then
            msj.Mensaje(Me, "Atencion", "Digito Incorrecto", 2)
            Exit Sub
        End If

        deu = cg.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT))



        Session("Deudor") = deu

        If Not IsNothing(deu) Then
            'Datos Deudor
            Me.Ib_ayudadeu.Enabled = False

            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True

        End If
    End Sub

    Protected Sub DP_Depto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Depto.SelectedIndexChanged

        Dim depto As Integer

        If DP_Depto.SelectedIndex <> 0 Then
            depto = DP_Depto.SelectedValue
            cg.MunicipioDevuelve(depto, True, DP_Ciudad)
            DP_Ciudad.ClearSelection()
            DP_Comuna.ClearSelection()
        End If

    End Sub

    Protected Sub DP_SucRecaudacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_SucRecaudacion.SelectedIndexChanged
        DP_Comuna.ClearSelection()
    End Sub

End Class
