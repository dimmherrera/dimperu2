Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports System.Transactions
Imports ClsSession.SesionProrrogas
Imports ClsSession.SesionOperaciones
Imports ClsSession.SesionPagos
Imports ClsSession.SesionAplicaciones
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ColillaDeposito
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"


    Dim FG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim Sesion As New ClsSession.ClsSession
    Dim Pagos As New ClsSession.SesionPagos
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim FormGene As New FormulasGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Caption As String = "Colilla Deposito"
    Dim Msj As New ClsMensaje

    Dim CuentaDoctosSeleccionados As Int16 = 0
    '  Dim sesion.Coll_CHRDetalle As New Collection
    Dim Coll_FechaYMonto As New Collection

    ' Dim act_gen As New ActualizacionesGenerales
    Dim cont As Integer
    Dim total As Integer
    Dim PGO As New ClasePagos
    Dim TSR As New ClaseTesoreria
    Dim OPE As New ClaseOperaciones
    Dim CMC As New ClaseComercial
    Dim ContPag As New Integer
#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                txt_FechaProceso.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                Response.Expires = -1
                'Response.Cache.SetNoStore()
                NroPaginacion = 0
                ContPag = 0
                Sesion.Coll_CHRDetalle = New Collection
                'Sesion.iniciarSesion()
                Coll_DSI = New Collection
                Coll_DPO = New Collection
                Coll_Ing_Sec = New Collection
                CG.ParametrosDevuelve(112, True, DP_Custodia)
                Inhabilitar()
                txt_NroCheques.Attributes.Add("Style", "Text-Align: right")
                txt_Tot.Attributes.Add("Style", "TEXT-ALIGN: right")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DP_Custodia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Custodia.SelectedIndexChanged
        If DP_Custodia.SelectedValue > 0 Then
            IB_Procesar.Enabled = True
            'IB_ValidaSolicitud.Enabled = True
            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = False
            RB_Pendientes.Enabled = True
            RB_Todos.Enabled = True
            Label19.Visible = True
            Label22.Visible = True
            'Format(Sesion.Coll_CHRDetalle(i).chr_fev_rea, "dd/MM/yyyy")
            Label22.Text = PGO.Cheque_Devuelve_Ultima_Fecha(DP_Custodia.SelectedValue)
            'Format(CLng(txt_Tot.Text), FMT.FCMSD)
            If Label22.Text = "" Then
                Label22.Text = ""
            Else
                Label22.Text = Format(CDate(Label22.Text), "dd/MM/yyyy")
            End If

            'txt_FechaProceso_CalendarExtender.Enabled = True
            'txt_FechaProceso_MaskedEditExtender.Enabled = True

            txt_FechaColillaDesde_MaskedEditExtender.Enabled = True
            txt_FechaColillaHasta_MaskedEditExtender.Enabled = True
            txt_FechaProceso_CalendarExtender.Enabled = True
            CalendarExtender1.Enabled = True

            txt_FechaProceso_CalendarExtender.Enabled = True
            Habilita()
            RefrescaPantalla(False, True, False, True, True, True)
        Else
            RefrescaPantalla(True, True, True, True, True, True)
        End If
    End Sub

    Protected Sub GV_ColAnteriores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_ColAnteriores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            Dim ver As ImageButton = e.Row.FindControl("Img_Ver")
            ver.Attributes.Add("onClick", "ClickColillaAnterior('" & e.Row.RowIndex & "', '" & e.Row.Cells(0).Text & "');")
        End If
    End Sub

    Protected Sub LinkB_CAnteriores_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_CAnteriores.Click

        Try

            Coll_DSI = New Collection

            GV_Documentos.DataSource = Coll_DSI
            GV_Documentos.DataBind()

            Me.txt_Tot.Text = ""
            txt_NroCheques.Text = ""

            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()

            Sesion.Coll_CHRDetalle = New Collection

            RB_Todos.Checked = False
            RB_Pendientes.Checked = False
            CB_SelecTodo.Checked = False

            Dim Chr = PGO.Cheque_DevuelveObjeto(DP_Custodia.SelectedValue, HF_Id.Value, 4, 4)

            For Each Llena In Chr
                Sesion.Coll_CHRDetalle.Add(Llena)
            Next

            If Sesion.Coll_CHRDetalle.Count > 0 Then

                GV_Cheques.DataSource = Sesion.Coll_CHRDetalle
                GV_Cheques.DataBind()

                CB_SelecTodo.Enabled = True
                CB_SelecTodo.Checked = True

                CB_SelecTodo_CheckedChanged(Me, e)

                HF_Id.Value = ""
                'HF_Pos.Value = ""

                MarcaLosPendientes()
                FormatoGrillaCheques()

                'CargaGrillaDocumentos()

                For I = 0 To GV_ColAnteriores.Rows.Count - 1
                    If (Val(HF_Pos.Value) = I) Then
                        If (I Mod 2) = 0 Then
                            GV_ColAnteriores.Rows(I).CssClass = "selectable"
                        Else
                            GV_ColAnteriores.Rows(I).CssClass = "selectableAlt"
                        End If
                    Else
                        If (I Mod 2) = 0 Then
                            GV_ColAnteriores.Rows(I).CssClass = "formatUltcell"
                        Else
                            GV_ColAnteriores.Rows(I).CssClass = "formatUltcellAlt"
                        End If
                    End If
                Next

            Else
                Msj.Mensaje(Page, Caption, "No se encontraron documentos", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RB_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Todos.CheckedChanged
        Try
            Sesion.Coll_CHRDetalle = New Collection
            If RB_Todos.Checked = True Then
                If DP_Custodia.SelectedValue = 0 Then
                    Msj.Mensaje(Me, Caption, "Seleccione Custodia", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                IB_Guardar.Enabled = False
                IB_Eli_Pendiente.Enabled = False

                RB_Pendientes.Checked = False
                'Dim Coll As New Collection

                Dim TraeColl = PGO.Cheque_DevuelveObjeto(DP_Custodia.SelectedValue, Nothing, 0)
                For Each c In TraeColl
                    Sesion.Coll_CHRDetalle.Add(c)
                Next

                If Sesion.Coll_CHRDetalle.Count > 0 Then

                    GV_Cheques.DataSource = Sesion.Coll_CHRDetalle
                    GV_Cheques.DataBind()
                    FormatoGrillaCheques()
                    CB_SelecTodo.Enabled = True
                Else
                    CB_SelecTodo.Enabled = False
                End If
                CB_SelecTodo.Checked = False
                ' CB_SelecTodo.Enabled = True
            End If
            IB_Guardar.Enabled = False
            CargaGrillaDocumentos()
            GV_ColAnteriores.DataSource = Nothing
            GV_ColAnteriores.DataBind()
            Coll_FechaYMonto = New Collection

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Pendientes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Pendientes.CheckedChanged
        Try
            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()
            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()
            GV_ColAnteriores.DataSource = Nothing
            GV_ColAnteriores.DataBind()

            Sesion.Coll_CHRDetalle = New Collection
            Coll_DSI = New Collection

            If RB_Pendientes.Checked = True Then
                If DP_Custodia.SelectedValue = 0 Then
                    Msj.Mensaje(Me, Caption, "Seleccione Custodia", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                RB_Todos.Checked = False
                txt_NroCheques.Text = ""

                Dim llena = PGO.Cheque_DevuelveObjeto(DP_Custodia.SelectedValue, Nothing, 5)
                'Dim llena = CG.CHRDevuelveTodoXCustYPendiente(DP_Custodia.SelectedValue)
                For Each p In llena
                    Sesion.Coll_CHRDetalle.Add(p)
                Next


                If Sesion.Coll_CHRDetalle.Count > 0 Then
                    GV_Cheques.DataSource = Sesion.Coll_CHRDetalle
                    GV_Cheques.DataBind()
                    IB_Eli_Pendiente.Enabled = True
                Else
                    Msj.Mensaje(Me, Caption, "No se encontraron documentos pendientes", TipoDeMensaje._Exclamacion)
                    'IB_Eli_Pendiente.Enabled = False
                    CB_SelecTodo.Enabled = False
                End If


                MarcaLosPendientes()
                FormatoGrillaCheques()

                CB_SelecTodo.Checked = False
                IB_Guardar.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Tabs_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabs.ActiveTabChanged
        Try
            If Tabs.ActiveTab.ID = "Pn_Nuevo" Then
                RB_Todos.Enabled = False
                RB_Pendientes.Enabled = False
            Else
                RB_Todos.Enabled = True
                RB_Pendientes.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CB_SelecTodo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_SelecTodo.CheckedChanged
        Try
            Dim CB As New CheckBox
            Dim cont As Integer = 0
            Coll_DSI = New Collection

            If CB_SelecTodo.Checked = True Then

                If RB_Todos.Checked = True Or RB_Todos.Enabled = True Then
                    IB_Guardar.Enabled = False
                Else
                    IB_Guardar.Enabled = True
                End If
                For I = 0 To GV_Cheques.Rows.Count - 1
                    CB = CType(GV_Cheques.Rows(I).FindControl("CheckBox1"), CheckBox)

                    CB.Checked = True
                  
                Next

                CheckBox1_CheckedChanged(Me, e)

            Else
                For I = 0 To GV_Cheques.Rows.Count - 1
                    CB = CType(GV_Cheques.Rows(I).FindControl("CheckBox1"), CheckBox)
                      CB.Checked = False
                    GV_Cheques.Rows(I).CssClass = "formatable"
                Next

                GV_Documentos.DataSource = Nothing
                GV_Documentos.DataBind()
                FormatoGV_Documentos()
                Coll_DSI = New Collection
                txt_NroCheques.Text = ""
                txt_Tot.Text = ""
            End If



            'txt_NroCheques.Text = cont
            FormatoGV_Documentos()
            'txt_Tot.Text = total
            'txt_Tot.Text = Format(CLng(txt_Tot.Text), FMT.FCMSD)
            CargaGrillaDocumentos()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Tabs_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabs.DataBinding
        Try
            If Tabs.ActiveTab.ID = "Pn_Nuevo" Then
                RB_Todos.Enabled = False
                RB_Pendientes.Enabled = False
            Else
                RB_Todos.Enabled = True
                RB_Pendientes.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_buscar.Click

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Cancelacion"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub IB_INFORME_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_INFORME.Click


        If HF_Pos.Value <> "" Then
            Dim RW As New FuncionesGenerales.RutinasWeb
            RW.AbrePopup(Me, 2, "REPORTECOLILLA.aspx?id=" & Sesion.Coll_CHRDetalle.Item(CInt(HF_Pos.Value)).id_cdp, "Pagos", 1000, 700, 10, 10)


        Else
            Msj.Mensaje(Me, Caption, "Debe seleccionar una colilla para poder generar su informe", TipoDeMensaje._Informacion)
        End If

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'CUENTA LOS CHEQUES Y SUMA LOS TOTALES
        Dim total As Integer
        Dim id_chr As Int32

        Coll_DSI = New Collection
        GV_Documentos.DataSource = Nothing
        GV_Documentos.DataBind()


        For a = 0 To GV_Cheques.Rows.Count - 1
            Dim cbx As New CheckBox
            cbx = CType(GV_Cheques.Rows(a).FindControl("checkbox1"), CheckBox)
            If cbx.Checked Then
                id_chr = GV_Cheques.Rows(a).Cells(1).Text

                Dim LlenaCollDSI = PGO.Documentos_Simulados_Devuelve_Por_Cheque(id_chr)

                For Each l In LlenaCollDSI
                    Coll_DSI.Add(l)
                Next

                cont = cont + 1
                'total = total + GV_Cheques.Rows(a).Cells(11).Text
                total = total + GV_Cheques.Rows(a).Cells(9).Text
            Else

            End If
        Next

        If Not IsNothing(Coll_DSI) Then
            GV_Documentos.DataSource = Coll_DSI
            GV_Documentos.DataBind()
            FormatoGV_Documentos()
        Else
            IB_Guardar.Enabled = False
        End If

        txt_NroCheques.Text = cont
        txt_Tot.Text = total
        txt_Tot.Text = Format(CLng(txt_Tot.Text), FMT.FCMSD)


        '**



    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            'If NroPaginacion >= 2 Then
            NroPaginacion -= 2
            IB_Nuevo_Click(Me, e)
            'End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If GV_ColAnteriores.Rows.Count < 2 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If GV_ColAnteriores.Rows.Count = 2 Then
                NroPaginacion += 2
                IB_Nuevo_Click(Me, e)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


#End Region

#Region "Private Function y Sub"

    Private Sub CargaGrillaDocumentos()

        Try

            Dim Pagos As New ClsSession.SesionPagos
            Dim conger As New ConsultasGenerales

            Dim Coll_Ing As Collection
            Dim conGen As New ConsultasGenerales

            Coll_Ing = conGen.IngresosDevuelve(Pagos.RutCliente, 0, 2, 2, "01/01/1900", "01/12/3000")

            For Pos_Ing = 1 To Coll_Ing.Count

                For Pos_Doc = 1 To Coll_DSI.Count
                    If Coll_Ing.Item(Pos_Ing).id_doc = Coll_DSI.Item(Pos_Doc).id_doc And Coll_Ing.Item(Pos_Ing).ing_pro = "N" Then
                        '     GV_Documentos.Rows(Pos_Doc - 1).BackColor = Drawing.Color.Bisque
                        HF_Id_Ing.Value = Coll_Ing.Item(Pos_Ing).ID_ING
                        Exit For
                    End If

                Next

            Next

        Catch ex As Exception

        End Try

    End Sub

    Sub LimpiaGV()
        GV_Cheques.DataSource = Nothing
        GV_Cheques.DataBind()
        GV_Documentos.DataSource = Nothing
        GV_Documentos.DataBind()

        Sesion.Coll_CHRDetalle = New Collection
        Coll_DSI = New Collection

        GV_ColAnteriores.DataSource = Nothing
        GV_ColAnteriores.DataBind()
        CB_SelecTodo.Checked = False
        txt_NroCheques.Text = ""
        txt_Tot.Text = ""

    End Sub

    Sub Inhabilitar()

        Me.txt_FechaProceso.CssClass = "clsDisabled"
        Me.txt_FechaProceso.ReadOnly = True
        Me.txt_FechaProceso.Text = ""

        Me.txt_FechaColillaDesde.CssClass = "clsDisabled"
        Me.txt_FechaColillaDesde.ReadOnly = True
        Me.txt_FechaColillaHasta.CssClass = "clsDisabled"
        Me.txt_FechaColillaHasta.ReadOnly = True
        txt_FechaColillaDesde.Text = ""
        txt_FechaColillaHasta.Text = ""
        txt_FechaProceso.Enabled = False

        txt_FechaColillaDesde.Enabled = False
        txt_FechaColillaHasta.Enabled = False

    End Sub

    Sub Habilita()
        Try
            txt_FechaColillaDesde_CalendarExtender.Enabled = True
            RB_Todos.Checked = False
            RB_Pendientes.Checked = False
            txt_NroCheques.Text = ""
            txt_Tot.Text = ""
            CB_SelecTodo.Checked = False
            CB_SelecTodo.Enabled = False

            txt_FechaProceso.Enabled = True
            txt_FechaColillaDesde.Enabled = True
            txt_FechaColillaHasta.Enabled = True


            Me.txt_FechaColillaDesde.CssClass = "clsMandatorio"
            Me.txt_FechaColillaDesde.ReadOnly = False
            Me.txt_FechaColillaHasta.CssClass = "clsMandatorio"
            Me.txt_FechaColillaHasta.ReadOnly = False
            txt_FechaProceso.ReadOnly = False
            txt_FechaProceso.CssClass = "clsMandatorio"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RefrescaPantalla(ByVal ObjNuevo As Boolean, ByVal TextNuevo As Boolean, ByVal ObjAnteriores As Boolean, ByVal TextAnteriores As Boolean, ByVal ListaCheques As Boolean, ByVal ListaDocumentos As Boolean)
        'TabNuevo
        If ObjNuevo Then
            Me.txt_FechaProceso.CssClass = "clsDisabled"
            Me.txt_FechaProceso.ReadOnly = True
        Else
            Me.txt_FechaProceso.CssClass = "clsMandatorio"
            Me.txt_FechaProceso.ReadOnly = False
        End If

        If TextNuevo Then
            Me.txt_FechaProceso.Text = ""
        End If

        'TabAnteriores
        If ObjAnteriores Then
            Me.txt_FechaColillaDesde.CssClass = "clsDisabled"
            Me.txt_FechaColillaDesde.ReadOnly = True
            Me.txt_FechaColillaHasta.CssClass = "clsDisabled"
            Me.txt_FechaColillaHasta.ReadOnly = True
        Else
            Me.txt_FechaColillaDesde.CssClass = "clsMandatorio"
            Me.txt_FechaColillaDesde.ReadOnly = False
            Me.txt_FechaColillaHasta.CssClass = "clsMandatorio"
            Me.txt_FechaColillaHasta.ReadOnly = False
        End If


        If TextAnteriores Then
            Me.txt_FechaColillaDesde.Text = ""
            Me.txt_FechaColillaHasta.Text = ""

            HF_Nro_Neg.Value = -1
            GV_ColAnteriores.DataSource = Nothing
            GV_ColAnteriores.DataBind()
        End If

        If ListaCheques Then
            HF_Nro_Neg.Value = -1
            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()
        End If

        If ListaDocumentos Then
            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()
        End If
    End Sub

    Protected Sub MarcaLosPendientes()
        '-----------------------------------------------------------------------------------------------------------------------------
        'Ciclo que recorre Documentos Desplegados para su formato
        '-----------------------------------------------------------------------------------------------------------------------------
        For i = 1 To Sesion.Coll_CHRDetalle.Count
            Select Case Sesion.Coll_CHRDetalle.Item(i).pnu_des
                Case "Pendiente"
                    GV_Cheques.Rows(i - 1).BackColor = Drawing.Color.GreenYellow
            End Select
        Next

    End Sub

    Protected Sub FormatoGrillaColAnteriores()

        For i = 1 To Coll_FechaYMonto.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0

            GV_ColAnteriores.Rows(i - 1).Cells(0).Text = Format(CDate(GV_ColAnteriores.Rows(i - 1).Cells(0).Text), "dd/MM/yyyy")
            GV_ColAnteriores.Rows(i - 1).Cells(1).Text = Format(CDbl(GV_ColAnteriores.Rows(i - 1).Cells(1).Text), FMT.FCMSD)

        Next

    End Sub

    Protected Sub FormatoGV_Documentos()

        For i = 1 To Coll_DSI.Count

            GV_Documentos.Rows(i - 1).Cells(1).Text = Format(CLng(Coll_DSI.Item(i).deu_ide), FMT.FCMSD) & "-" & FG.Vrut(GV_Documentos.Rows(i - 1).Cells(1).Text)
            GV_Documentos.Rows(i - 1).Cells(6).Text = Format(Coll_DSI.Item(i).dsi_fev, "dd/MM/yyyy")
            GV_Documentos.Rows(i - 1).Cells(8).Text = Format(CLng(Coll_DSI.Item(i).saldo_cli), FMT.FCMSD)
            GV_Documentos.Rows(i - 1).Cells(9).Text = Format(CLng(Coll_DSI.Item(i).saldo_deu), FMT.FCMSD)
            GV_Documentos.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_DSI.Item(i).dsi_mto), FMT.FCMSD)
       
        Next

    End Sub

    Protected Sub FormatoGrillaDetalle()
        Dim Coll_DPG As New Collection


        For i = 1 To Coll_DPG.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0
            GV_Documentos.Rows(i - 1).Cells(0).Text = Format(CLng(Coll_DPG.Item(i).deu_ide), FMT.FCMSD)
            GV_Documentos.Rows(i - 1).Cells(2).Text = Format(CLng(Coll_DPG.Item(i).opo_otg), FMT.FCMSD)
            GV_Documentos.Rows(i - 1).Cells(4).Text = Format(CLng(Coll_DPG.Item(i).dsi_num), FMT.FCMSD)

            Select Case Coll_DPG.Item(i).id_P_0023
                Case 1
                    GV_Documentos.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMSD)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMSD)

                Case 2
                    GV_Documentos.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMCD4)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMCD4)

                Case 3, 4
                    GV_Documentos.Rows(i - 1).Cells(6).Text = Format(Coll_DPG.Item(i).dsi_mto_fin, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(7).Text = Format(Coll_DPG.Item(i).doc_sdo_cli, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(11).Text = Format(Coll_DPG.Item(i).dpg_int_ere, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(12).Text = Format(Coll_DPG.Item(i).dpg_com_isi, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(13).Text = Format(Coll_DPG.Item(i).dpg_iva_com, FMT.FCMCD)
                    GV_Documentos.Rows(i - 1).Cells(14).Text = Format(Coll_DPG.Item(i).TotalGastos, FMT.FCMCD)
            End Select

            GV_Documentos.Rows(i - 1).Cells(9).Text = Format(Coll_DPG.Item(i).doc_fev_rea, "dd/MM/yyyy")
            GV_Documentos.Rows(i - 1).Cells(10).Text = Format(Coll_DPG.Item(i).nva_doc_fev_rea, "dd/MM/yyyy")

        Next

    End Sub

    Protected Sub MarcaGVNegociacion()

        For i = 1 To Sesion.Coll_CHRDetalle.Count
            Select Case Sesion.Coll_CHRDetalle.Item(i).spg_est
                Case 2, 3
                    If Sesion.Coll_CHRDetalle.Item(i).pnu_des = "Pendiente" Then
                        GV_Cheques.Rows(i - 1).BackColor = Drawing.Color.GreenYellow
                    Else
                        GV_Cheques.Rows(i - 1).BackColor = Drawing.Color.OrangeRed
                    End If
                    GV_Cheques.Rows(i - 1).CssClass = "formatable"
            End Select
        Next

    End Sub

    Protected Sub MarcaGV_ChequesPorGuardados()

        For i = 1 To Sesion.Coll_CHRDetalle.Count
            'i-1 por que la collection empieza de 1 y la grilla de 0
            Select Case Sesion.Coll_CHRDetalle.Item(i).spg_est
                Case 2, 3
                    If Sesion.Coll_CHRDetalle.Item(i).pnu_des = "Pendiente" Then
                        GV_Cheques.Rows(i - 1).BackColor = Drawing.Color.GreenYellow
                    Else
                        GV_Cheques.Rows(i - 1).BackColor = Drawing.Color.OrangeRed
                    End If
                    GV_Cheques.Rows(i - 1).CssClass = "formatable"
            End Select
        Next

    End Sub

    Sub ControlaRB()
        Try
            Select Case Tabs.ActiveTab.HeaderText
                Case "Nueva Colilla"
                    RB_Pendientes.Enabled = False
                    RB_Todos.Enabled = False
                Case "Colillas Generadas"
                    RB_Pendientes.Enabled = True
                    RB_Todos.Enabled = True
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub FormatoGrillaCheques()
        For i = 1 To Sesion.Coll_CHRDetalle.Count
            GV_Cheques.Rows(i - 1).Cells(2).Text = Format(CLng(Sesion.Coll_CHRDetalle(i).rut_cli), FMT.FCMSD) & "-" & FG.Vrut(GV_Cheques.Rows(i - 1).Cells(2).Text)
            GV_Cheques.Rows(i - 1).Cells(5).Text = Format(Sesion.Coll_CHRDetalle(i).chr_fev_rea, "dd/MM/yyyy")
            GV_Cheques.Rows(i - 1).Cells(9).Text = Format(CLng(Sesion.Coll_CHRDetalle(i).Monto_cheque), FMT.FCMSD)
        Next
    End Sub
  
    Private Function CalculaInteres(ByVal id As Long) As Double

        Try

            Dim Formulas As New FormulasGenerales
            Dim MtoAPagar As Double
            Dim Interes As Double
            Dim Saldo As Double
            Dim FechaSimula As String
            Dim FechaUltPago As String
            Dim FechaVctoRea As String
            Dim CantidadDias As String
            Dim Lineal As String
            Dim TasaAnuMen As String
            Dim TasaRenova As Decimal
            Dim MtoAnticip As Double
            Dim FecVctoOri As String
            Dim NroRenovac As Integer
            Dim TasaNegocio As Decimal
            'Dim Mto_A_Pagar As TextBox


            Dim CGN As New ConsultasGenerales

            'Buscamos el documento para traer todas sus relaciones
            Dim DOC As doc_cls = OPE.DocumentoOtorgagoDevuelvePorId(id)

            'Rescatamos el saldo del documento
            Saldo = DOC.doc_sdo_cli

            'Monto a pagar por defecto toma el saldo completo
            MtoAPagar = DOC.doc_sdo_cli

            'validamos si la fecha de ultimo pago viene nula
            If IsNothing(DOC.opo_cls.opo_ful_pgo) Then
                FechaUltPago = "01/01/1900"
            Else
                FechaUltPago = DOC.opo_cls.opo_ful_pgo
            End If

            If IsNothing(DOC.opo_cls.ope_cls.ope_fec_sim) Then
                FechaSimula = "01/01/1900"
            Else
                FechaSimula = DOC.opo_cls.ope_cls.ope_fec_sim
            End If

            If IsNothing(DOC.dsi_cls.dsi_fev_rea) Then
                FechaVctoRea = "01/01/1900"
            Else
                FechaVctoRea = DOC.dsi_cls.dsi_fev_rea
            End If

            If IsNothing(DOC.dsi_cls.dsi_ctd_dia) Then
                CantidadDias = 0
            Else
                CantidadDias = DOC.dsi_cls.dsi_ctd_dia
            End If

            If IsNothing(DOC.opo_cls.ope_cls.ope_lnl) Then
                Lineal = "N"
            Else
                Lineal = DOC.opo_cls.ope_cls.ope_lnl
            End If

            If IsNothing(DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa) Then
                TasaAnuMen = 0
            Else
                TasaAnuMen = DOC.opo_cls.ope_cls.opn_cls.opn_tas_moa
            End If

            If IsNothing(DOC.doc_tas_ren) Then
                TasaRenova = 0
            Else
                TasaRenova = DOC.doc_tas_ren
            End If

            If IsNothing(DOC.dsi_cls.dsi_fev) Then
                FecVctoOri = "01/01/1900"
            Else
                FecVctoOri = DOC.dsi_cls.dsi_fev
            End If

            If IsNothing(DOC.doc_num_ren) Then
                NroRenovac = 0
            Else
                NroRenovac = DOC.doc_num_ren
            End If

            MtoAnticip = DOC.opo_cls.ope_cls.ope_mto_ant

            TasaNegocio = DOC.opo_cls.ope_cls.opn_cls.opn_tas_bas + _
                          DOC.opo_cls.ope_cls.opn_cls.opn_spr_ead + _
                          DOC.opo_cls.ope_cls.opn_cls.opn_pto_spr

            Dim COG As New ConsultasGenerales
            Pagos.FechaPago = Date.Now

            Pagos.TasaInteresCalculo = CMC.TasaRetorna(2, HF_Cli.Value, 0)

            Dim CL As New ClaseClientes

            Interes = FormGene.RetornaCalculoInteres(Pagos.FechaPago, _
                                                     Pagos.DiasRetencionPago, _
                                                     Pagos.TasaInteresCalculo, _
                                                     MtoAPagar, _
                                                     FechaSimula, _
                                                     FechaVctoRea, _
                                                     CantidadDias, _
                                                     Saldo, _
                                                     FechaUltPago, _
                                                     Pagos.DiasDevolverInteres, _
                                                     Lineal, _
                                                     TasaAnuMen, _
                                                     TasaNegocio, _
                                                     TasaRenova, _
                                                     MtoAnticip, _
                                                     FecVctoOri, _
                                                     NroRenovac, _
                                                     DOC.id_doc, _
                                                     DOC.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dia_bas)

            Return Interes




        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "Botonera"


    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try

            Inhabilitar()

            'RefrescaPantalla(True, True, True, True, True, True)
            DP_Custodia.ClearSelection()
            IB_Guardar.Enabled = False
            IB_Eli_Pendiente.Enabled = False
            IB_Procesar.Enabled = False
            IB_Nuevo.Enabled = False
            RB_Pendientes.Enabled = False
            RB_Pendientes.Checked = False
            RB_Todos.Checked = False
            RB_Todos.Enabled = False
            Label22.Text = ""
            Label19.Visible = False
            Label22.Visible = False
            CB_SelecTodo.Checked = False
            txt_NroCheques.Text = ""
            CalendarExtender1.Enabled = False
            'CalendarExtender2.Enabled = False
            txt_FechaColillaDesde_CalendarExtender.Enabled = False

            'txt_FechaProceso_CalendarExtender.Enabled = False
            txt_Tot.Text = ""
            Coll_DSI = New Collection
            Sesion.Coll_CHRDetalle = New Collection
            Coll_DPO = New Collection
            Pagos.Coll_Ing_Sec = New Collection



            CB_SelecTodo.Enabled = False
            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()

            GV_ColAnteriores.DataSource = Nothing
            GV_ColAnteriores.DataBind()

            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()
            NroPaginacion = 0
            ContPag = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20030805, Usr, "PRESIONA BOTON BUSCAR DOCUMENTOS") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            Dim ing As New ing_cls
            Dim n_colilla As Int64 = 0
            Dim cbx As New CheckBox
            Coll_DPO = New Collection
            Pagos.Coll_Ing_Sec = New Collection
            Dim sw As Integer
            '************************************************************************************
            'pregunto si selecciono algún cheque si no sale del for y del sub y manda mensaje 
            For a = 0 To GV_Cheques.Rows.Count - 1
                cbx = CType(GV_Cheques.Rows(a).FindControl("checkbox1"), CheckBox)
                If cbx.Checked Then
                    cont = cont + 1
                    sw = 0
                    Exit For
                Else
                    sw = 1
                End If
            Next
            If sw = 1 Then
                Msj.Mensaje(Me, Caption, "Seleccione un Cheque", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            '**************************************************************************************

            If Coll_DSI.Count = 0 Then
                Msj.Mensaje(Me, Caption, "Debe Hacer click en Grilla Cheques", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            arr_gto_def_ = New ArrayList

            For i = 0 To Me.GV_Cheques.Rows.Count - 1


                Dim ch As CheckBox


                ch = GV_Cheques.Rows(i).FindControl("checkbox1")

                If ch.Checked Then
                    '*****************************************
                    'rescata id de moneda para guardar en dpo*
                    '*****************************************


                    Dim d As New dpo_cls

                    HF_IdMoneda.Value = Sesion.Coll_CHRDetalle.Item(i + 1).id_P_0023


                    HF_Bco.Value = Sesion.Coll_CHRDetalle.Item(i + 1).id_bco 'CG.bancoDevuelve(CInt(GV_Cheques.Rows(i).Cells(1).Text))

                    d.id_P_0023 = HF_IdMoneda.Value 'moneda
                    d.dpo_mto = Sesion.Coll_CHRDetalle.Item(i + 1).Monto_cheque ''GV_Cheques.Rows(i).Cells(10).Text 'monto cheque 
                    d.dpo_fec_emi = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                    If Val(HF_Bco.Value) = 0 Then
                        d.id_bco = Nothing 'Banco cheque
                    Else
                        d.id_bco = Val(HF_Bco.Value)
                    End If

                    d.dpo_fev = Sesion.Coll_CHRDetalle.Item(i + 1).chr_fev_rea '''GV_Cheques.Rows(i).Cells(4).Text 'fev_rea del cheque
                    txt_Rut_Deudor.Value = Sesion.Coll_CHRDetalle.Item(i + 1).rut_cli ''''GV_Cheques.Rows(i).Cells(3).Text
                    d.id_P_0054 = 4
                    d.id_P_0052 = 1

                    HF_IdReg.Value = Sesion.Coll_CHRDetalle(i + 1).id_PL_000047
                    HF_Cli.Value = Sesion.Coll_CHRDetalle(i + 1).cli_idc
                    HF_CtaCte.Value = Sesion.Coll_CHRDetalle(i + 1).cta_cte


                    d.id_PL_000047 = HF_IdReg.Value
                    d.dpo_cct = HF_CtaCte.Value
                    Dim ConGe As New ClaseClientes
                    Dim cli As New cli_cls
                    cli = ConGe.ClientesDevuelve(HF_Cli.Value, FG.Vrut(HF_Cli.Value))
                    d.dpo_aor = cli.cli_rso & " " & cli.cli_ape_ptn & " " & cli.cli_ape_mtn

                    Coll_DPO.Add(d)

                    Dim sw1 As New Boolean

                    For x = 1 To Coll_DSI.Count
                        'if de id_chr
                        If GV_Cheques.Rows(i).Cells(1).Text = Coll_DSI.Item(x).id_chr Then
                            sw1 = True
                            arr_gto_def_.Add(Coll_DSI.Item(x).id_chr)

                            'inserta_ing_sec
                            Dim ing_sec As New ing_sec_cls

                            With ing_sec

                                .id_dpo = i + 1
                                .id_doc = Coll_DSI.Item(x).id_doc 'GV_Documentos.Rows(i).Cells(6).Text
                                .doc_sdo_cli = Coll_DSI.Item(x).doc_sdo_cli
                                .ing_pag_deu = "S" '??
                                .ing_mto_tot = Sesion.Coll_CHRDetalle.Item(i + 1).Monto_cheque '.ing_mto_abo + .ing_mto_int
                                .ing_mto_int = CalculaInteres(Coll_DSI.Item(x).id_doc)
                                .ing_mto_abo = .ing_mto_tot - .ing_mto_int
                                .ing_qpa = "C"
                                .ing_tot_par = "T"
                                .id_P_0053 = 2 '7 El cheque se convierte el mismo pago
                                .ing_fac_cam = Coll_DSI.Item(x).ope_fac_cam

                                .ing_vld_rcz = "I"
                                .cli_idc = Format(CLng(HF_Cli.Value), "000000000000")
                                .ing_pro = "N"
                            End With

                            Pagos.Coll_Ing_Sec.Add(ing_sec)

                        End If
                    Next
                    If sw1 = False Then
                        Msj.Mensaje(Me, Caption, "Este Cheque No Tiene Documentos Que Cubrir ,Debe Hacer Click Sobre El cheque", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                End If

            Next

            'LLena objeto Ing -- Cabecera del pago

            ing.ing_sis_fec = Date.Now
            ing.ing_fec = CDate(txt_FechaProceso.Text)

            ing.ing_pgo_hre = "N"
            ing.id_eje = Sesion.CodEje

            HF_Id_Ing.Value = PGO.PagosInserta(Coll_DPO, ing, Pagos.Coll_Ing_Sec)

            If HF_Id_Ing.Value <> "" And n_colilla = 0 Then
                Dim colilla As New cdp_cls

                colilla.cdp_fec = CDate(txt_FechaProceso.Text)
                colilla.cdp_mto = Me.txt_Tot.Text
                n_colilla = PGO.Colilla_ingresa(colilla)
            End If
            If HF_Id_Ing.Value <> "" Then
                PGO.Cheque_Actualiza_pago(arr_gto_def_, n_colilla)
                PGO.ingreso_Actualiza_pago(HF_Id_Ing.Value, n_colilla)
            End If

            'HF_Id_Ing.Value = 95
            If n_colilla > 0 Then
                Dim RW As New FuncionesGenerales.RutinasWeb
                RW.AbrePopup(Me, 2, "REPORTECOLILLA.aspx?id=" & n_colilla, "Pagos", 1000, 700, 10, 10)
                'Ag.CHRModifica(HF_Id_Ing.Value, )
                Msj.Mensaje(Me, Caption, "Registros Guardados", TipoDeMensaje._Informacion)
            Else
                Msj.Mensaje(Me, Caption, "No se pudo guardar", TipoDeMensaje._Informacion)
            End If

            IB_Guardar.Enabled = False
            LimpiaGV()
            Coll_DPO = New Collection

            Pagos.Coll_Ing_Sec = New Collection

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Procesar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Procesar.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20040210, Usr, "PRESIONA BOTON PROCESAR COLILLA") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()
            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()
            Sesion.Coll_CHRDetalle = New Collection
            Coll_DSI = New Collection


            If txt_FechaProceso.Text = "" Then
                Msj.Mensaje(Me, Caption, "Ingrese fecha de proceso", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Dim Traedatos = PGO.Cheque_DevuelveObjeto(DP_Custodia.SelectedValue, txt_FechaProceso.Text, 3)

            For Each Lle In Traedatos
                Sesion.Coll_CHRDetalle.Add(Lle)
            Next

            If Sesion.Coll_CHRDetalle.Count > 0 Then
                GV_Cheques.DataSource = Sesion.Coll_CHRDetalle
                GV_Cheques.DataBind()

                FormatoGrillaCheques()
                IB_Guardar.Enabled = True
                CB_SelecTodo.Enabled = True
                'CargaGrillaDocumentos()
            Else
                Msj.Mensaje(Me, Caption, "No se encontraron documentos", TipoDeMensaje._Informacion)
            End If
            RB_Pendientes.Enabled = False
            RB_Todos.Enabled = False
            CB_SelecTodo.Checked = False
            RB_Todos.Checked = False
            txt_FechaColillaDesde.Text = ""
            txt_FechaColillaHasta.Text = ""
            txt_NroCheques.Text = ""
            txt_Tot.Text = ""
            HF_Id_Ing.Value = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Eli_Pendiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Eli_Pendiente.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20020210, Usr, "PRESIONA BOTON ELIMINAR COLILLA DEPOSITO") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Dim cbx As New CheckBox
            Dim sw As Integer
            For a = 0 To GV_Cheques.Rows.Count - 1
                cbx = CType(GV_Cheques.Rows(a).FindControl("checkbox1"), CheckBox)
                If cbx.Checked Then
                    sw = 0
                    Exit For
                Else
                    sw = 1
                End If
            Next
            If sw = 1 Then
                Msj.Mensaje(Me, Caption, "Seleccione un Cheque", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim CB As New CheckBox
            '**********************************************************************************
            'si esta chequeado modifica estado de cheque
            For i = 0 To GV_Cheques.Rows.Count - 1
                CB = CType(GV_Cheques.Rows(i).FindControl("CheckBox1"), CheckBox)
                If CB.Checked Then
                    '   Ag.CHRModifica(GV_Cheques.Rows(i).Cells(1).Text, 6)
                    Msj.Mensaje(Me, Caption, "Cheques eliminados", TipoDeMensaje._Informacion)
                    'Else
                    '   Msj(Caption, "Seleccione un Registro Pendiente", TipoDeMensaje._Exclamacion)
                    '  Exit Sub
                End If
            Next

            Dim Coll As New Collection

            Dim Llenacol = PGO.Cheque_DevuelveObjeto(DP_Custodia.SelectedValue, Nothing, 5)



            If Coll.Count > 0 Then
                GV_Cheques.DataSource = Llenacol
                GV_Cheques.DataBind()

                FormatoGrillaCheques()
            Else
                GV_Cheques.DataSource = Nothing
                GV_Cheques.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20030210, Usr, "PRESIONA BOTON NUEVO") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            RB_Pendientes.Enabled = True
            RB_Todos.Enabled = True
            RB_Todos.Checked = False
            CB_SelecTodo.Checked = False

            Coll_DSI = New Collection
            Sesion.Coll_CHRDetalle = New Collection

            GV_Cheques.DataSource = Nothing
            GV_Cheques.DataBind()

            GV_Documentos.DataSource = Nothing
            GV_Documentos.DataBind()

            If txt_FechaColillaDesde.Text = "" Then
                Msj.Mensaje(Me, Caption, "Ingrese fecha desde", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If txt_FechaColillaHasta.Text = "" Then
                Msj.Mensaje(Me, Caption, "Ingrese fecha hasta", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CDate(txt_FechaColillaDesde.Text) > CDate(txt_FechaColillaHasta.Text) Then
                Msj.Mensaje(Me, Caption, "Fecha desde no puede ser mayor a fecha hasta", TipoDeMensaje._Exclamacion)
                txt_FechaColillaDesde.Text = ""
                txt_FechaColillaHasta.Text = ""
                Exit Sub
            End If


            'Dim FechaYMonto = CG.Cheque_Devuelve(DP_Custodia.SelectedValue, txt_FechaColillaDesde.Text _
            '                     , txt_FechaColillaHasta.Text)

            Dim FechaYMonto = PGO.Colilla_Devuelve(DP_Custodia.SelectedValue, txt_FechaColillaDesde.Text, txt_FechaColillaHasta.Text)



            For Each f In FechaYMonto
                Coll_FechaYMonto.Add(f)
            Next

            If Coll_FechaYMonto.Count > 0 Then
                GV_ColAnteriores.DataSource = Coll_FechaYMonto
                GV_ColAnteriores.DataBind()

            End If

            FormatoGrillaColAnteriores()


            txt_FechaProceso.Text = ""
            IB_Guardar.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
