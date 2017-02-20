Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Text
Imports FuncionesGenerales.FComunes
Imports ClsSession.SesionPagos
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class WC_QuePaga
    Inherits System.Web.UI.UserControl

#Region "DECLARACION DE VARIABLES"

    Dim CG As New ConsultasGenerales
    Dim CTA As New ClaseCuentas
    Dim CBZ As New ClaseCobranza
    Dim OP As New ClaseOperaciones
    Dim PGO As New ClasePagos
    Dim RC As New FuncionesGenerales.FComunes
    Dim Pagos As New ClsSession.SesionPagos
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Posicion As Integer = 0
    Dim Posicion_CxC As Integer = 0
    Dim Msj As New ClsMensaje
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim CC As ClaseClientes
    Dim CMC As New ClaseComercial

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            Confirma_seleccion = False

            Dim Coll_Obj As Object
            Dim Coll_Est As New Collection

            'TabPanel3.Visible = False
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocto)

            Txt_Tasa.Text = Format(Pagos.TasaInteresCalculo, Fmt.FCMCD)
         
            SW = 1

            If Not IsNothing(Coll_Doctos_Cxc) Then
                If Coll_Doctos_Cxc.Count > 0 Then
                    SW = 3
                End If
            End If


            If Not IsNothing(Coll_Doctos_Seleccionados) Then
                If Coll_Doctos_Seleccionados.Count > 0 Then
                    SW = 3
                End If
            End If

            Coll_Obj = CG.ParametrosDevuelve(TablaParametro.EstadoDocumento, False, Nothing)

            For Each O In Coll_Obj

                Select Case O.codigo
                    Case 1, 2, 4, 9, 11, 12
                        Coll_Est.Add(O)
                End Select

            Next

            '****************************************************************************

            IB_volver.Visible = False
            img_volver.Visible = True

            '****************************************************************************
            DP_Estados.DataSource = Coll_Est
            DP_Estados.DataTextField = "descripcion"
            DP_Estados.DataValueField = "codigo"
            DP_Estados.DataBind()
            DP_Estados.ClearSelection()
            DP_Estados.Items.Insert(0, New ListItem("Seleccionar", 0))
            DP_Estados.Items.Insert(1, New ListItem("TODOS", 999))
            DP_Estados.Items.Item(1).Selected = True


            DP_CodCobranza.DataSource = CBZ.CodigoCobranza_RetornaGestionar(1)

            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()
            DP_CodCobranza.ClearSelection()

            DP_CodCobranza.Items.Insert(0, New ListItem("Seleccionar", 0))
            DP_CodCobranza.Items.Item(0).Selected = True

            Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Pag.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Tot_Not.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Mto_Dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Mto_Hst.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Not_Cre.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Not_Cre.Text = 0

            If Not IsNothing(Coll_Doctos_Seleccionados) Then
                If Coll_Doctos_Seleccionados.Count = 0 Then
                    Coll_Doctos_Seleccionados = New Collection
                End If
            End If

            If Not IsNothing(Coll_Cxc_Seleccionados) Then
                If Coll_Cxc_Seleccionados.Count = 0 Then
                    Coll_Cxc_Seleccionados = New Collection
                End If
            End If

            'Cargamos las cuentas por cobrar cuando paga cliente
            If Pagos.Pagador = "C" Then

                Session("CxC") = CTA.CuentasPorCobrarDevuelveTotalRegistros(Pagos.RutCliente, 0, 999, 1, 2)
                CargaGrillaCxC()
                'MarcaCxC()
            Else
                'TabContainer1.Tabs(0).Enabled = False
                TabContainer1.ActiveTabIndex = 1
            End If

            If Request.QueryString("Rec") = "S" Then
                TabContainer1.Tabs(0).Enabled = False
                TabContainer1.ActiveTabIndex = 1
            End If

            IB_Next.Enabled = False
            IB_Prev.Enabled = False

            If Val(Pagos.RutDeudor) <> 0 Then
                Me.Txt_Rut_Deu.Text = Format(Pagos.RutDeudor, Fmt.FCMSD)
                Me.Txt_Dig_Deu.Text = RC.Vrut(Pagos.RutDeudor)
                CB_Deudor.Checked = True
                Dim Deudor As deu_cls

                Deudor = CG.DeudorDevuelvePorRut(CLng(Txt_Rut_Deu.Text))

                If Not IsNothing(Deudor) Then
                    Txt_Rso_Deu.Text = Deudor.deu_rso & " " & Deudor.deu_ape_ptn & " " & Deudor.deu_ape_mtn
                Else
                    Txt_Rut_Deu.Focus()
                    Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Deudor no Existe", TipoDeMensaje._Exclamacion)
                End If

            End If

        End If

        If Request.QueryString("modulo") <> "" Then
            Me.IB_volver.Visible = False
        Else
            Me.IB_volver.Visible = False
            img_volver.Visible = True
        End If

        IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../Ayudas/AyudaDeu.aspx?wc=1','PopUpCliente',580,410,200,150);")

    End Sub

    Protected Sub DP_Orden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Orden.SelectedIndexChanged

        Try
            Select Case DP_Orden.SelectedValue

                Case 1 'RUT DEUDOR
                    RC.SortCollection(Coll_DOC, "deu_ide", True, "")

                Case 2 'N° OTORG.
                    RC.SortCollection(Coll_DOC, "id_opo", True, "")

                Case 3 'N° DOCTO.
                    RC.SortCollection(Coll_DOC, "id_doc", True, "")

                Case 4 'FECHA DE VCTO.
                    RC.SortCollection(Coll_DOC, "doc_fev_rea", True, "")

                Case 5
                    Select Case Pagos.Pagador
                        Case "C"
                            RC.SortCollection(Coll_DOC, "doc_sdo_cli", True, "")
                        Case "D"
                            RC.SortCollection(Coll_DOC, "doc_sdo_ddr", True, "")
                    End Select

                Case 6 'ESTADO DEL DOCUMENTO
                    RC.SortCollection(Coll_DOC, "id_p_0011", True, "")

            End Select


            Gr_Documentos.DataSource = Coll_DOC
            Gr_Documentos.DataBind()

            Dim Coll_Ing As Collection

            Coll_Ing = CG.IngresosDevuelve(Pagos.RutCliente, 0, 2, 2, "01/01/1900", "01/01/3000")

            For Pos_Ing = 1 To Coll_Ing.Count

                For Pos_Doc = 1 To Coll_DOC.Count

                    If Coll_Ing.Item(Pos_Ing).id_doc = Coll_DOC.Item(Pos_Doc).id_doc And Coll_Ing.Item(Pos_Ing).ing_pro = "N" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                        Gr_Documentos.Rows(Pos_Doc - 1).BackColor = col
                        Exit For
                    End If

                Next

            Next
            AsignaSource()


            For i = 0 To Me.Gr_Documentos.Rows.Count - 1
                For x = 1 To Coll_Doctos_Seleccionados.Count
                    If Coll_Doctos_Seleccionados.Item(x).id_doc = Coll_DOC.Item(i + 1).id_doc Then
                        Dim CB_grilla As CheckBox
                        CB_grilla = Gr_Documentos.Rows(i).FindControl("CB_Seleccionar")
                        CB_grilla.Checked = True

                    End If
                Next
            Next

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub IB_Aceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aceptar.Click

        Try

            Dim FC As New FuncionesGenerales.FComunes

            'Coll_Cxc_Seleccionados = New Collection

            'CargaCollectionCXC()

            'Ordena las cuentas por cobrar por fecha de vencimiento
            FC.SortCollection(Coll_Cxc_Seleccionados, "cxc_fec", True)

            'Ordena los documentos por fecha de vencimiento
            FC.SortCollection(Coll_Doctos_Seleccionados, "doc_fev_rea", True)

            If Not IsNothing(Coll_Doctos_Seleccionados) Or Not IsNothing(Coll_Cxc_Seleccionados) Then

                If Coll_Doctos_Seleccionados.Count > 0 Or Coll_Cxc_Seleccionados.Count > 0 Then
                    If ValidaMoneda(Coll_Doctos_Seleccionados) And ValidaMoneda(Coll_Cxc_Seleccionados) Then
                        Confirma_seleccion = True
                        Msj.Mensaje_WebControl(Me.Page, "Selección de Documentos", "Doctos. se asociaron exitosamente", TipoDeMensaje._Informacion)
                        SW = 2
                        SW_Rec = 3

                    Else
                        Confirma_seleccion = False
                        Msj.Mensaje_WebControl(Me.Page, "Selección de Documentos", "No se puede seleccionar documentos de distintas monedas", TipoDeMensaje._Informacion)
                    End If
                    
                Else
                    Confirma_seleccion = False
                    Msj.Mensaje_WebControl(Me.Page, "Selección de Documentos", "No se han asociado Documentos", TipoDeMensaje._Informacion)
                End If

            End If


        Catch ex As Exception
            Msj.Mensaje_WebControl(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Cerrar_Pagos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cerrar_Pagos.Click
        ModalPopupExtender1.Hide()
    End Sub

    Protected Sub btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Limpiar.Click
        LimpiaBusqueda()
        SW = 1

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Coll_Doctos_Seleccionados = New Collection
        Coll_Cxc_Seleccionados = New Collection

        Me.Txt_Tot_Pag.Text = 0
        Me.Txt_Tot_Sel.Text = 0
        Me.Txt_Tot_Not.Text = 0

        For i = 0 To Me.Gr_Documentos.Rows.Count - 1

            Dim Mto_A_Pagar As TextBox
            Dim CB As CheckBox


            CB = Gr_Documentos.Rows(i).FindControl("CB_Seleccionar")
            Mto_A_Pagar = CType(Gr_Documentos.Rows(i).FindControl("Txt_MtoPagar"), TextBox)

            CB.Checked = False
            Mto_A_Pagar.Text = 0
            Mto_A_Pagar.ReadOnly = True
            Mto_A_Pagar.CssClass = "clsDisabled"

        Next

        For i = 0 To Me.GV_CxC.Rows.Count - 1

            Dim Mto_A_Pagar As TextBox
            Dim CB As CheckBox


            CB = GV_CxC.Rows(i).FindControl("CB_Seleccionar_CxC")
            Mto_A_Pagar = CType(GV_CxC.Rows(i).FindControl("Txt_MtoPagar_CxC"), TextBox)

            CB.Checked = False
            Mto_A_Pagar.Text = 0
            Mto_A_Pagar.ReadOnly = True
            Mto_A_Pagar.CssClass = "clsDisabled"

        Next

        'CargaGrillaDoctos()

        'Gr_Documentos.DataSource = Nothing
        'Gr_Documentos.DataBind()

    End Sub

#End Region

#Region "CRITERIO DE BUSQUEDA"

    Protected Sub btn_acep_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_acep.Click


        NroPaginacion = 0
        NroPagina = 1
        Txt_Tot_Pag.Text = 0
        Txt_Tot_Not.Text = 0
        Txt_Tot_Sel.Text = 0

        Coll_DOC = New Collection
        Coll_Doctos_Seleccionados = New Collection

        CargaGrillaDoctos()

        'Lb_NroPagina.Text = "Pagina N: 1 al " & Gr_Documentos.Rows.Count & " de <strong>" & Session("DOC") & " Doctos.</strong>"

        ModalPopupExtender2.Dispose()
        ModalPopupExtender2.Hide()

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        Try

            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Debe Ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            If Txt_Dig_Deu.Text = "" Then
                Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Debe Ingresar Digito del NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            If UCase(Txt_Dig_Deu.Text) <> UCase(RC.Vrut(Txt_Rut_Deu.Text.Replace(".", ""))) Then
                Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Digito Incorrecto del Deudor", TipoDeMensaje._Exclamacion)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            Dim Deudor As deu_cls

            Deudor = CG.DeudorDevuelvePorRut(CLng(Txt_Rut_Deu.Text))

            If Not IsNothing(Deudor) Then
                Txt_Rso_Deu.Text = Deudor.deu_rso & " " & Deudor.deu_ape_ptn & " " & Deudor.deu_ape_mtn
                'BloqueaDeudor(True)
            Else
                Txt_Rut_Deu.Focus()
                Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Deudor no Existe", TipoDeMensaje._Exclamacion)
            End If

            ModalPopupExtender2.Show()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Deudor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Deudor.CheckedChanged

        If CB_Deudor.Checked Then

            Txt_Rut_Deu.ReadOnly = False
            Txt_Dig_Deu.ReadOnly = False

            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""
            Txt_Rso_Deu.Text = ""

            Txt_Rut_Deu.CssClass = "clsMandatorio"
            Txt_Dig_Deu.CssClass = "clsMandatorio"
            Txt_Rut_Deu.Focus()
            IB_AyudaDeu.Enabled = True
        Else

            Txt_Rut_Deu.ReadOnly = True
            Txt_Dig_Deu.ReadOnly = True

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.CssClass = "clsDisabled"
            IB_AyudaDeu.Enabled = False
        End If

        ModalPopupExtender2.Show()

    End Sub

    Protected Sub Btn_Criterio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Criterio.Click
        ModalPopupExtender2.Show()
    End Sub

#End Region

#Region "GRILLA DOCTOS"

    Protected Sub Gr_Documentos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gr_Documentos.PageIndexChanging
        Gr_Documentos.PageIndex = e.NewPageIndex
        CargaGrillaDoctos()
    End Sub

    Protected Sub Gr_Documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Documentos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txt_mto As TextBox = CType(e.Row.FindControl("Txt_MtoPagar"), TextBox)

            txt_mto.Attributes.Add("Style", "TEXT-ALIGN: right")
            txt_mto.Attributes.Add("onClick", "ClickDocto(WC_QuePaga1_TabContainer1_TabPanel2_Gr_Documentos, 'clicktable', 'formatable', 'selectable', " & Posicion & ");")
            txt_mto.ToolTip = Posicion

            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaDocto(" & Posicion & ");")
            e.Row.Attributes.Add("onClick", "ClickDocto(WC_QuePaga1_TabContainer1_TabPanel2_Gr_Documentos, 'clicktable', 'formatable', 'selectable', " & Posicion & ");")

            Posicion = Posicion + 1
           
        End If

    End Sub

    Protected Sub Gr_Documentos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Gr_Documentos.Sorting

        If e.SortExpression = "Todos" Then

            For I = 0 To Gr_Documentos.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox
                Dim CB As CheckBox
                Dim TD As CheckBox

                CB = CType(Gr_Documentos.Rows(I).FindControl("CB_Seleccionar"), CheckBox)
                Mto_A_Pagar = CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox)
                TD = CType(Gr_Documentos.Rows(I).FindControl("CB_PD"), CheckBox)

                If CB.Enabled Then

                    If CB.Checked Then

                        If EliminaSeleccionDoctos(Coll_DOC.Item(I + 1).cli_idc, Gr_Documentos.Rows(I).Cells(5).Text, Gr_Documentos.Rows(I).Cells(6).Text) Then
                            CB.Checked = False
                            Mto_A_Pagar.CssClass = "clsDisabled"
                            Mto_A_Pagar.ReadOnly = True
                            TD.Checked = False

                        End If

                    Else

                        CB.Checked = True
                        Mto_A_Pagar.CssClass = "clsMandatorio"
                        Mto_A_Pagar.ReadOnly = False
                        TD.Checked = True

                        If Not ValidaSeleccionDoctos(Coll_DOC.Item(I + 1).cli_idc, Gr_Documentos.Rows(I).Cells(5).Text, Gr_Documentos.Rows(I).Cells(6).Text) Then

                            Coll_DOC.Item(I + 1).MontoPagar = CDbl(CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox).Text)
                            Coll_DOC.Item(I + 1).Interes = CDbl(Gr_Documentos.Rows(I).Cells(9).Text)
                            Coll_DOC.Item(I + 1).Tasa = Pagos.TasaInteresCalculo
                            Coll_DOC.Item(I + 1).PagaDeudor = "S"
                            Coll_DOC.Item(I + 1).Contrato = Gr_Documentos.Rows(I).Cells(17).Text
                            Coll_DOC.Item(I + 1).InteresDevolver = 0

                            If Coll_DOC.Item(I + 1).Interes < 0 Then
                                Coll_DOC.Item(I + 1).InteresDevolver = Coll_DOC.Item(I + 1).Interes
                            End If

                            Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(I + 1))

                        End If

                    End If

                End If

            Next

        End If

        CalculaTotalPago()



    End Sub

    Private Sub CargaGrillaDoctos()

        Try

            If IsNothing(Coll_Doctos_Seleccionados) Then
                Coll_Doctos_Seleccionados = New Collection
            End If

            Dim Rut_Cli_Desde As Long
            Dim Rut_Cli_Hasta As Long

            Dim Rut_Deu_Desde As Long
            Dim Rut_Deu_Hasta As Long

            Dim TipoDoc_Desde As Integer
            Dim TipoDoc_Hasta As Integer

            Dim NroOtor_Desde As Long
            Dim NroOtor_Hasta As Long

            Dim NroDoct_Desde As String
            Dim NroDoct_Hasta As String

            Dim Fec_Vto_Desde As DateTime
            Dim Fec_Vto_Hasta As DateTime
            Dim Pagos As New ClsSession.SesionPagos

            If Modulo = "Operacion" Then
                Rut_Cli_Desde = Pagos.RutCliente
                Rut_Cli_Hasta = Pagos.RutCliente
                Rut_Deu_Desde = 0
                Rut_Deu_Hasta = 999999999999
            Else

                Select Case Pagos.Pagador
                    Case "C"
                        Txt_Not_Cre.ReadOnly = True
                        Txt_Not_Cre.CssClass = "clsDisabled"

                        If IsNothing(Pagos.RutCliente) Or Pagos.RutCliente = 0 Then
                            Rut_Cli_Desde = 0
                            Rut_Cli_Hasta = 999999999999
                        Else
                            Rut_Cli_Desde = Pagos.RutCliente
                            Rut_Cli_Hasta = Pagos.RutCliente
                            Rut_Deu_Desde = 0
                            Rut_Deu_Hasta = 999999999999
                        End If

                    Case "D"

                        If IsNothing(Pagos.RutDeudor) Or Pagos.RutDeudor = 0 Then
                            Rut_Deu_Desde = 0
                            Rut_Deu_Hasta = 999999999999
                        Else
                            Rut_Deu_Desde = Pagos.RutDeudor
                            Rut_Deu_Hasta = Pagos.RutDeudor
                            Rut_Cli_Desde = 0
                            Rut_Cli_Hasta = 999999999999

                            If Pagos.RutCliente > 0 Then
                                Rut_Cli_Desde = Pagos.RutCliente
                                Rut_Cli_Hasta = Pagos.RutCliente
                            End If
                        End If
                End Select

            End If

            'Criterio de Busqueda
            'Si busca por Deudor
            If CB_Deudor.Checked Then
                If Txt_Rut_Deu.Text = "" Then
                    Rut_Deu_Desde = 0
                    Rut_Deu_Hasta = 999999999999
                Else
                    Rut_Deu_Desde = Txt_Rut_Deu.Text
                    Rut_Deu_Hasta = Txt_Rut_Deu.Text
                End If
            End If

            If Rut_Deu_Hasta = 0 Then
                Rut_Deu_Hasta = Pagos.RutDeudor
            End If
            'Tipo Docto
            If DP_TipoDocto.SelectedValue = 0 Then
                TipoDoc_Desde = 0
                TipoDoc_Hasta = 999
            Else
                TipoDoc_Desde = DP_TipoDocto.SelectedValue
                TipoDoc_Hasta = DP_TipoDocto.SelectedValue
            End If

            'Nro Otorgamiento
            If Txt_Nro_Oto.Text = "" Then
                NroOtor_Desde = 0
                NroOtor_Hasta = 999999999
            Else
                NroOtor_Desde = Txt_Nro_Oto.Text
                NroOtor_Hasta = Txt_Nro_Oto.Text
            End If

            'Nro Documento
            If Txt_Nro_Doc.Text = "" Then
                NroDoct_Desde = "0"
                NroDoct_Hasta = "Z"
            Else
                NroDoct_Desde = Txt_Nro_Doc.Text
                NroDoct_Hasta = Txt_Nro_Doc.Text
            End If

            'Fecha Vcto
            If Txt_Fec_Dsd.Text = "" And Txt_Fec_Hst.Text = "" Then
                Fec_Vto_Desde = Format(CDate("01/01/1900"), "dd/MM/yyyy")
                Fec_Vto_Hasta = Format(CDate("31/12/2900"), "dd/MM/yyyy")
            Else

                If Not IsDate(Me.Txt_Fec_Dsd.Text) Then
                    Msj.Mensaje_WebControl(Page, "Atención", "Fecha desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.Txt_Fec_Dsd.Text = ""
                    Exit Sub
                End If

                If Not IsDate(Me.Txt_Fec_Hst.Text) Then
                    Msj.Mensaje_WebControl(Me.Page, "Atención", "Fecha hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Me.Txt_Fec_Hst.Text = ""
                    Exit Sub
                End If

                If CDate(Me.Txt_Fec_Dsd.Text) > CDate(Me.Txt_Fec_Hst.Text) Then
                    Msj.Mensaje_WebControl(Me.Page, "Atención", "Fecha desde no debe ser mayor que fecha hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Fec_Vto_Desde = Txt_Fec_Dsd.Text
                Fec_Vto_Hasta = Txt_Fec_Hst.Text

            End If

            Coll_DOC = New Collection

            Dim Estado1, Estado2, Estado3, Estado4, Estado5, Estado6, Estado7, Estado8, Estado9, Estado10, Estado11, Estado12 As Integer

            If DP_Estados.SelectedValue = 999 Then
                Estado1 = 1
                Estado2 = 2
                Estado3 = 4
                Estado4 = 9
                Estado5 = 11
                Estado6 = 12
                Estado7 = 1
                Estado8 = 1
                Estado9 = 1
                Estado10 = 1
                Estado11 = 1
                Estado12 = 1
            Else
                Estado1 = DP_Estados.SelectedValue
                Estado2 = DP_Estados.SelectedValue
                Estado3 = DP_Estados.SelectedValue
                Estado4 = DP_Estados.SelectedValue
                Estado5 = DP_Estados.SelectedValue
                Estado6 = DP_Estados.SelectedValue
                Estado7 = DP_Estados.SelectedValue
                Estado8 = DP_Estados.SelectedValue
                Estado9 = DP_Estados.SelectedValue
                Estado10 = DP_Estados.SelectedValue
                Estado11 = DP_Estados.SelectedValue
                Estado12 = DP_Estados.SelectedValue
            End If

            Dim Var As New FuncionesGenerales.Variables

            Pagos.TasaInteresCalculo = CMC.TasaRetorna(2, Pagos.RutCliente, 0)
            Session("DOC") = PGO.DocumentosOtorgagosPagos_RetornaDoctos_WC_Total(Format(Rut_Cli_Desde, Var.FMT_RUT), Format(Rut_Cli_Hasta, Var.FMT_RUT), _
                                                                                     Format(Rut_Deu_Desde, Var.FMT_RUT), Format(Rut_Deu_Hasta, Var.FMT_RUT), _
                                                                                     NroOtor_Desde, NroOtor_Hasta, _
                                                                                     TipoDoc_Desde, TipoDoc_Hasta, _
                                                                                     NroDoct_Desde, NroDoct_Hasta, _
                                                                                     0, 9999, _
                                                                                     Fec_Vto_Desde, Fec_Vto_Hasta, _
                                                                                     Estado1, _
                                                                                     Estado2, _
                                                                                     Estado3, _
                                                                                     Estado4, _
                                                                                     Estado5, _
                                                                                     Estado6, _
                                                                                     Estado7, _
                                                                                     Estado8, _
                                                                                     Estado9, _
                                                                                     Estado10, _
                                                                                     Estado11, _
                                                                                     Estado12, _
                                                                                     0, _
                                                                                     999999999999, _
                                                                                     DP_CodCobranza.SelectedValue, _
                                                                                     NroPaginacion)


            Dim Coll_Obj As Collection = PGO.DocumentosOtorgagosPagos_RetornaDoctos_WC(Format(Rut_Cli_Desde, Var.FMT_RUT), Format(Rut_Cli_Hasta, Var.FMT_RUT), _
                                                                                     Format(Rut_Deu_Desde, Var.FMT_RUT), Format(Rut_Deu_Hasta, Var.FMT_RUT), _
                                                                                     NroOtor_Desde, NroOtor_Hasta, _
                                                                                     TipoDoc_Desde, TipoDoc_Hasta, _
                                                                                     NroDoct_Desde, NroDoct_Hasta, _
                                                                                     0, 9999, _
                                                                                     Fec_Vto_Desde, Fec_Vto_Hasta, _
                                                                                     Estado1, _
                                                                                     Estado2, _
                                                                                     Estado3, _
                                                                                     Estado4, _
                                                                                     Estado5, _
                                                                                     Estado6, _
                                                                                     Estado7, _
                                                                                     Estado8, _
                                                                                     Estado9, _
                                                                                     Estado10, _
                                                                                     Estado11, _
                                                                                     Estado12, _
                                                                                     0, _
                                                                                     999999999999, _
                                                                                     DP_CodCobranza.SelectedValue, _
                                                                                     NroPaginacion)


            Coll_DOC = Coll_Obj

            'le asigna a la grilla documentos la coll_doc
            Gr_Documentos.DataSource = Coll_DOC
            Gr_Documentos.DataBind()

            If Gr_Documentos.Rows.Count > 0 Then
                AsignaSource()
                IB_Next.Enabled = True
                IB_Prev.Enabled = True
                'Lb_NroPagina.Text = "Pagina : 1 al " & Gr_Documentos.Rows.Count & " de <strong>" & Session("DOC") & " Doctos.</strong>"
                Lb_NroPagina.Text = "Pagina N° " & NroPagina & ": " & NroPaginacion + 1 & " al " & NroPaginacion + Gr_Documentos.Rows.Count & " de <strong>" & Session("DOC") & " Doctos.</strong>"

            Else
                Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "No existen documentos para pagar", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

            If Modulo = "Recaudacion" Then

                Pagos.Coll_hre = PGO.DocumentosARecaudar_RetornaDoctos_WC(NroHojaRuta)

                For i = 1 To Pagos.Coll_hre.Count

                    For Pos_Doc = 1 To Coll_DOC.Count

                        If Pagos.Coll_hre.Item(i).id_doc = Coll_DOC.Item(Pos_Doc).id_doc Then

                            Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFCC")
                            Dim col1 As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")

                            If (Gr_Documentos.Rows(Pos_Doc - 1).BackColor <> col1) Then
                                Gr_Documentos.Rows(Pos_Doc - 1).BackColor = col
                            End If

                            Exit For
                        End If

                    Next

                Next

            End If

        Catch ex As Exception
            Msj.Mensaje_WebControl(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaCantidadDoctos()

        Try

            Dim Rut_Cli_Desde As Long
            Dim Rut_Cli_Hasta As Long

            Dim Rut_Deu_Desde As Long
            Dim Rut_Deu_Hasta As Long

            Dim TipoDoc_Desde As Integer
            Dim TipoDoc_Hasta As Integer

            Dim NroOtor_Desde As Long
            Dim NroOtor_Hasta As Long

            Dim NroDoct_Desde As String
            Dim NroDoct_Hasta As String

            Dim Fec_Vto_Desde As DateTime
            Dim Fec_Vto_Hasta As DateTime
            Dim Pagos As New ClsSession.SesionPagos

            If Modulo = "Operacion" Then
                Rut_Cli_Desde = Pagos.RutCliente
                Rut_Cli_Hasta = Pagos.RutCliente
                Rut_Deu_Desde = 0
                Rut_Deu_Hasta = 999999999999
            Else

                Select Case Pagos.Pagador
                    Case "C"
                        Txt_Not_Cre.ReadOnly = True
                        Txt_Not_Cre.CssClass = "clsDisabled"

                        If IsNothing(Pagos.RutCliente) Or Pagos.RutCliente = 0 Then
                            Rut_Cli_Desde = 0
                            Rut_Cli_Hasta = 999999999999
                        Else
                            Rut_Cli_Desde = Pagos.RutCliente
                            Rut_Cli_Hasta = Pagos.RutCliente
                            Rut_Deu_Desde = 0
                            Rut_Deu_Hasta = 999999999999
                        End If
                    Case "D"

                        If IsNothing(Pagos.RutDeudor) Or Pagos.RutDeudor = 0 Then
                            Rut_Deu_Desde = 0
                            Rut_Deu_Hasta = 999999999999
                        Else
                            Rut_Deu_Desde = Pagos.RutDeudor
                            Rut_Deu_Hasta = Pagos.RutDeudor
                            Rut_Cli_Desde = 0
                            Rut_Cli_Hasta = 999999999999
                        End If
                End Select


            End If

            'Criterio de Busqueda

            'Si busca por Deudor
            If CB_Deudor.Checked Then
                If Txt_Rut_Deu.Text = "" Then
                    Rut_Deu_Desde = 0
                    Rut_Deu_Hasta = 999999999999
                Else
                    Rut_Deu_Desde = Txt_Rut_Deu.Text
                    Rut_Deu_Hasta = Txt_Rut_Deu.Text
                End If
            End If

            'Tipo Docto
            If DP_TipoDocto.SelectedValue = 0 Then
                TipoDoc_Desde = 0
                TipoDoc_Hasta = 999
            Else
                TipoDoc_Desde = DP_TipoDocto.SelectedValue
                TipoDoc_Hasta = DP_TipoDocto.SelectedValue
            End If

            'Nro Otorgamiento
            If Txt_Nro_Oto.Text = "" Then
                NroOtor_Desde = 0
                NroOtor_Hasta = 999999999
            Else
                NroOtor_Desde = Txt_Nro_Oto.Text
                NroOtor_Hasta = Txt_Nro_Oto.Text
            End If

            'Nro Documento
            If Txt_Nro_Doc.Text = "" Then
                NroDoct_Desde = "0"
                NroDoct_Hasta = "Z"
            Else
                NroDoct_Desde = Txt_Nro_Doc.Text
                NroDoct_Hasta = Txt_Nro_Doc.Text
            End If

            'Fecha Vcto
            If Txt_Fec_Dsd.Text = "" And Txt_Fec_Hst.Text = "" Then
                Fec_Vto_Desde = "01/01/1900"
                Fec_Vto_Hasta = "01/01/2010" 'Date.Now.ToShortDateString
            Else
                Fec_Vto_Desde = Txt_Fec_Dsd.Text
                Fec_Vto_Hasta = Txt_Fec_Hst.Text
            End If

            Coll_DOC = New Collection

            Dim Estado1, Estado2, Estado3, Estado4, Estado5, Estado6, Estado7, Estado8, Estado9, Estado10, Estado11, Estado12 As Integer


            If DP_Estados.SelectedValue = 999 Then
                Estado1 = 1
                Estado2 = 2
                Estado3 = 4
                Estado4 = 9
                Estado5 = 11
                Estado6 = 12
                Estado7 = 1
                Estado8 = 1
                Estado9 = 1
                Estado10 = 1
                Estado11 = 1
                Estado12 = 1
            Else
                Estado1 = DP_Estados.SelectedValue
                Estado2 = DP_Estados.SelectedValue
                Estado3 = DP_Estados.SelectedValue
                Estado4 = DP_Estados.SelectedValue
                Estado5 = DP_Estados.SelectedValue
                Estado6 = DP_Estados.SelectedValue
                Estado7 = DP_Estados.SelectedValue
                Estado8 = DP_Estados.SelectedValue
                Estado9 = DP_Estados.SelectedValue
                Estado10 = DP_Estados.SelectedValue
                Estado11 = DP_Estados.SelectedValue
                Estado12 = DP_Estados.SelectedValue
            End If

            Dim Var As New FuncionesGenerales.Variables

            Dim Pag As Integer = CDbl(K_Doctos.Text) / 15

            NroPagina = 1

        Catch ex As Exception
            Msj.Mensaje_WebControl(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub AsignaSource()

        Dim Formato As String = ""
        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
        Dim CB As CheckBox
        Dim mtopago As TextBox
        
        Select Case Pagos.Pagador
            Case "D"
                Gr_Documentos.Columns(1).HeaderText = "NIT Cliente"
                Gr_Documentos.Columns(8).HeaderText = "Saldo Pagador"
            Case "C"
                Gr_Documentos.Columns(1).HeaderText = "NIT Pagador"
                Gr_Documentos.Columns(8).HeaderText = "Saldo Cliente"
        End Select

        Dim saldo As Double = 0

        For I = 0 To Gr_Documentos.Rows.Count - 1

            Try

                Select Case Coll_DOC.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                mtopago = CType(Gr_Documentos.Rows(I).FindControl("Txt_MtoPagar"), TextBox)
                saldo = 0
                Select Case Pagos.Pagador
                    Case "C"
                        Gr_Documentos.Rows(I).Cells(2).Text = Coll_DOC.Item(I + 1).Deudor
                        Gr_Documentos.Rows(I).Cells(1).Text = RC.FormatoMiles(CLng(Coll_DOC.Item(I + 1).deu_ide)) & "-" & Coll_DOC.Item(I + 1).deu_dig_ito
                        saldo = Coll_DOC(I + 1).doc_sdo_cli

                        Gr_Documentos.Rows(I).Cells(8).Text = Format(saldo, Formato)
                        Gr_Documentos.Rows(I).Cells(9).Text = Format(CDbl(CalculaInteres(I, saldo)), Formato)

                        mtopago.Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(9).Text) + saldo, Formato)

                    Case "D"
                        Gr_Documentos.Rows(I).Cells(2).Text = Coll_DOC.Item(I + 1).Cliente
                        Gr_Documentos.Rows(I).Cells(1).Text = RC.FormatoMiles(CLng(Coll_DOC.Item(I + 1).cli_idc)) & "-" & Coll_DOC.Item(I + 1).cli_dig_ito
                        saldo = Coll_DOC(I + 1).doc_sdo_ddr

                        Gr_Documentos.Rows(I).Cells(8).Text = Format(saldo, Formato)
                        Gr_Documentos.Rows(I).Cells(9).Text = Format(CDbl(CalculaInteres(I, saldo)), Formato)

                        mtopago.Text = Format(saldo, Formato)

                End Select

                Gr_Documentos.Rows(I).Cells(4).Text = Format(CLng(Gr_Documentos.Rows(I).Cells(4).Text), Formato)
                'Gr_Documentos.Rows(I).Cells(4).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(4).Text))
                'Gr_Documentos.Rows(I).Cells(5).Text = RC.FormatoMiles(CLng(Gr_Documentos.Rows(I).Cells(5).Text))

                If Coll_DOC.Item(I + 1).Cantidad_Ingresos > 0 Then

                    CB = Me.Gr_Documentos.Rows(I).FindControl("CB_Seleccionar")
                    CB.Enabled = False
                    Gr_Documentos.Rows(I).BackColor = col

                    mtopago.ReadOnly = True
                    mtopago.CssClass = "clsDisabled"

                End If

            Catch ex As Exception

            End Try

        Next

    End Sub

    Protected Sub CB_Seleccionar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Mto_A_Pagar As TextBox
        Dim CB As CheckBox
        Dim TD As CheckBox

        CB = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_Seleccionar"), CheckBox)

        'Validamos que tenga factor de cambio del dia de la cuenta, sino no lo deja agregar
        If Coll_DOC.Item(HF_Posicion.Value + 1).ope_fac_cam <= 0 And CB.Checked Then
            Msj.Mensaje_WebControl(Me.Page, "Documentos", "No se puede agregar esta Docto. por no tener factor de cambio del dia", TipoDeMensaje._Exclamacion)
            CB.Checked = False
            Exit Sub
        End If

        Mto_A_Pagar = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("Txt_MtoPagar"), TextBox)

        If Mto_A_Pagar.Text = "" Then
            Mto_A_Pagar.Text = 0
        End If

        If Mto_A_Pagar.Text = 0 Then
            Mto_A_Pagar.Text = CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(8).Text)
        End If

        TD = CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_PD"), CheckBox)

        If CB.Checked Then

            Txt_Not_Cre.ReadOnly = False
            Txt_Not_Cre.CssClass = "clsMandatorio"
            Txt_Not_Cre.Text = Format(Coll_DOC.Item(HF_Posicion.Value + 1).nota_cred, Fmt.FCMSD)

            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False
            TD.Checked = True

            'CalculaInteres(HF_Posicion.Value, Mto_A_Pagar.Text)

            If Not ValidaSeleccionDoctos(Coll_DOC.Item(HF_Posicion.Value + 1).cli_idc, Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text, Gr_Documentos.Rows(HF_Posicion.Value).Cells(6).Text) Then

                Coll_DOC.Item(HF_Posicion.Value + 1).MontoPagar = CDbl(CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("Txt_MtoPagar"), TextBox).Text)
                Coll_DOC.Item(HF_Posicion.Value + 1).Interes = CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text)
                Coll_DOC.Item(HF_Posicion.Value + 1).Tasa = Pagos.TasaInteresCalculo
                Coll_DOC.Item(HF_Posicion.Value + 1).PagaDeudor = "S"
                Coll_DOC.Item(HF_Posicion.Value + 1).Contrato = Gr_Documentos.Rows(HF_Posicion.Value).Cells(17).Text

                Coll_DOC.Item(HF_Posicion.Value + 1).InteresDevolver = 0

                If Coll_DOC.Item(HF_Posicion.Value + 1).Interes < 0 Then
                    Coll_DOC.Item(HF_Posicion.Value + 1).InteresDevolver = Coll_DOC.Item(HF_Posicion.Value + 1).Interes
                End If

                Coll_Doctos_Seleccionados.Add(Coll_DOC.Item(HF_Posicion.Value + 1))

            End If

            '* * * * * * * * * * * * * Valida si existe otro pago no este procesado * * * * * * * * * * * * *
            Dim Coll As Collection

            Coll = PGO.PagosValidaEstados(Coll_DOC.Item(HF_Posicion.Value + 1).id_doc, "PDIRECTO-INGR")

            If Coll.Count > 0 Then
                GridPagos.DataSource = Coll
                GridPagos.DataBind()
                GridPagos.Columns(1).Visible = False
                ModalPopupExtender1.Show()
            End If

            '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        Else

            If EliminaSeleccionDoctos(Coll_DOC.Item(HF_Posicion.Value + 1).cli_idc, Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text, Gr_Documentos.Rows(HF_Posicion.Value).Cells(6).Text) Then

                Mto_A_Pagar.CssClass = "clsDisabled"
                Mto_A_Pagar.ReadOnly = True
                TD.Checked = False


            End If

        End If

        CalculaTotalPago()

        Mto_A_Pagar.Focus()

    End Sub

    Protected Sub Txt_MtoPagar_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Mto_A_Pagar As TextBox

        Mto_A_Pagar = CType(sender, TextBox)
        HF_Posicion.Value = Mto_A_Pagar.ToolTip
        'Falta actualizar coleccion
        For x = 1 To Coll_Doctos_Seleccionados.Count

            If Coll_Doctos_Seleccionados(x).dsi_num = Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text And _
               Coll_Doctos_Seleccionados(x).dsi_flj_num = Gr_Documentos.Rows(HF_Posicion.Value).Cells(6).Text Then

                CalculaInteres(HF_Posicion.Value, Mto_A_Pagar.Text)

                If CDbl(Mto_A_Pagar.Text) >= CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text) Then 'jlagos 07-05-2014 se agrega restriccion para que abone no sea menor al interes

                    Coll_Doctos_Seleccionados.Item(x).MontoPagar = CDbl(Mto_A_Pagar.Text)
                    Coll_Doctos_Seleccionados.Item(x).Interes = Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text

                    Mto_A_Pagar.Text = Format(CDbl(Mto_A_Pagar.Text), Fmt.FCMSD)

                Else

                    Coll_Doctos_Seleccionados.Item(x).MontoPagar = CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text)
                    Coll_Doctos_Seleccionados.Item(x).Interes = Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text

                    Mto_A_Pagar.Text = Format(CDbl(Gr_Documentos.Rows(HF_Posicion.Value).Cells(9).Text), Fmt.FCMSD)
                    Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "Monto a pagar no puede ser menor al interés de mora", ClsMensaje.TipoDeMensaje._Exclamacion)

                End If

                CalculaTotalPago()

                Exit Sub

            End If

        Next

    End Sub

    Private Sub CalculaTotalPago()

        
        Try

            'Falta validar que si pago no es total , no calcula interes a devolver.
            Txt_Tot_Pag.Text = 0
            Txt_Tot_Not.Text = 0

            For x = 1 To Coll_Doctos_Seleccionados.Count
                Txt_Tot_Pag.Text = CDbl(Txt_Tot_Pag.Text) + Coll_Doctos_Seleccionados.Item(x).MontoPagar
            Next

            Txt_Tot_Pag.Text = Format(CDbl(Txt_Tot_Pag.Text), Fmt.FCMSD)
            Txt_Tot_Not.Text = Format(CDbl(Txt_Tot_Not.Text), Fmt.FCMSD)
            Txt_Tot_Sel.Text = Format(Coll_Doctos_Seleccionados.Count, Fmt.FCMSD)


            UpdatePanel2.Update()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Not_Cre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Not_Cre.TextChanged

        If HF_Posicion.Value <> "" Then
            Coll_DOC.Item(HF_Posicion.Value + 1).nota_cred = Txt_Not_Cre.Text
            CalculaTotalPago()
        End If

    End Sub

    Protected Sub LB_MarcaGrilla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_MarcaGrilla.Click

        Try

            Dim EncuentraFila As Boolean = False

            If HF_Posicion.Value <> "" Then

                For i = 0 To Gr_Documentos.Rows.Count - 1

                    If HF_Posicion.Value = i Then

                        Gr_Documentos.Rows(i).CssClass = "clicktable"

                        If CType(Gr_Documentos.Rows(i).FindControl("CB_Seleccionar"), CheckBox).Checked Then
                            EncuentraFila = True

                            Txt_Not_Cre.ReadOnly = False
                            Txt_Not_Cre.CssClass = "clsMandatorio"
                            Txt_Not_Cre.Text = Format(Coll_DOC.Item(i + 1).nota_cred, Fmt.FCMSD)
                            'Else
                            'CType(Gr_Documentos.Rows(i).FindControl("CB_Seleccionar"), CheckBox).Checked = True
                        End If

                    Else
                        Gr_Documentos.Rows(i).CssClass = "formatable"
                    End If

                Next

                If Not EncuentraFila Then
                    Txt_Not_Cre.ReadOnly = True
                    Txt_Not_Cre.CssClass = "clsDisabled"
                    Txt_Not_Cre.Text = 0
                End If

            End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        Dim desde As Integer = NroPaginacion

        NroPaginacion += 200
        NroPagina += 1

        CargaGrillaDoctos()

        'Lb_NroPagina.Text = "Pagina : " & NroPaginacion + 1 & " al " & NroPaginacion + Gr_Documentos.Rows.Count & " de <strong>" & Session("DOC") & " Doctos.</strong>"

        'If Gr_Documentos.Rows.Count > 0 Then
        '    Lb_NroPagina.Text = "Pagina : " & NroPaginacion + 1 & " al " & NroPaginacion + Gr_Documentos.Rows.Count
        'Else
        '    Lb_NroPagina.Text = "Pagina : " & (NroPaginacion - 200) + 1 & " al " & (NroPaginacion - 200) + Gr_Documentos.Rows.Count
        'End If

        MarcaDoctosSeleccionados()

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        Dim desde As Integer = NroPaginacion

        If NroPaginacion > 1 Then

            NroPaginacion -= 200
            NroPagina -= 1

            CargaGrillaDoctos()

            'Lb_NroPagina.Text = "Pagina : " & NroPagina + 1 & " al " & NroPaginacion + Gr_Documentos.Rows.Count & " de <strong>" & Session("DOC") & " Doctos. <strong>"


            MarcaDoctosSeleccionados()

        Else
            Msj.Mensaje_WebControl(Me.Page, "Doctos. de Pagos", "No existen documentos anteriores", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Private Function ValidaSeleccionDoctos(ByVal cliente As String, ByVal nrodocto As String, ByVal cuota As Integer) As Boolean
        'jlagos 08-05-2014 se agrega cliente
        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count

                If Coll_Doctos_Seleccionados.Item(I).cli_idc = cliente And _
                   Coll_Doctos_Seleccionados.Item(I).dsi_num = nrodocto And _
                   Coll_Doctos_Seleccionados.Item(I).dsi_flj_num = cuota Then
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function

    Private Sub MarcaDoctosSeleccionados()

        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count

                For X = 0 To Gr_Documentos.Rows.Count - 1

                    If Coll_Doctos_Seleccionados.Item(I).dsi_num = Gr_Documentos.Rows(X).Cells(5).Text And Coll_Doctos_Seleccionados(I).dsi_flj_num = Gr_Documentos.Rows(X).Cells(6).Text Then

                        Dim Mto_A_Pagar As TextBox
                        Dim CB As CheckBox
                        Dim TD As CheckBox

                        CB = Gr_Documentos.Rows(X).FindControl("CB_Seleccionar")
                        Mto_A_Pagar = CType(Gr_Documentos.Rows(X).FindControl("Txt_MtoPagar"), TextBox)
                        TD = CType(Gr_Documentos.Rows(X).FindControl("CB_PD"), CheckBox)

                        CB.Checked = True
                        TD.Checked = True
                        Mto_A_Pagar.CssClass = "clsMandatorio"
                        Mto_A_Pagar.ReadOnly = False


                        Gr_Documentos.Rows(X).Cells(9).Text = Format(Coll_Doctos_Seleccionados.Item(I).Interes, Fmt.FCMSD)
                        Mto_A_Pagar.Text = Format(Coll_Doctos_Seleccionados.Item(I).MontoPagar, Fmt.FCMSD)

                        Exit For

                    End If

                Next

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Function EliminaSeleccionDoctos(ByVal cliente As String, ByVal nrodocto As String, ByVal cuota As Integer) As Boolean
        'jlagos 08-05-2014 se agrega cliente
        Try

            For I = 1 To Coll_Doctos_Seleccionados.Count

                If Coll_Doctos_Seleccionados.Item(I).cli_idc = cliente And _
                   Coll_Doctos_Seleccionados.Item(I).dsi_num = nrodocto And _
                   Coll_Doctos_Seleccionados.Item(I).dsi_flj_num = cuota Then
                    Coll_Doctos_Seleccionados.Remove(I)
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function
  
    Protected Sub CB_PD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For I = 1 To Coll_Doctos_Seleccionados.Count

            If Coll_Doctos_Seleccionados.Item(I).id_doc = Gr_Documentos.Rows(HF_Posicion.Value).Cells(5).Text Then

                If CType(Gr_Documentos.Rows(HF_Posicion.Value).FindControl("CB_PD"), CheckBox).Checked Then
                    Coll_Doctos_Seleccionados.Item(I).PagaDeudor = "S"
                Else
                    Coll_Doctos_Seleccionados.Item(I).PagaDeudor = "N"
                End If

                Exit For

            End If

        Next

    End Sub

#End Region

#Region "GRILLA CXC"

    Protected Sub GV_CxC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_CxC.PageIndexChanging
        GV_CxC.PageIndex = e.NewPageIndex
        CargaGrillaCxC()
    End Sub

    Private Sub CargaGrillaCxC()

        Try

            coll_CXC = New Collection

            coll_CXC = CTA.CuentasPorCobrarDevuelve(Pagos.RutCliente, 0, 999, 1, 2, True, NroPaginacionCXC)

            GV_CxC.DataSource= coll_CXC
            GV_CxC.DataBind()

            If NroPaginaCxC = 0 Then
                NroPaginaCxC = 1
            End If

            If GV_CxC.Rows.Count > 0 Then
                AsignaSourceCxC()
                'Lb_NroPaginaCxC.Text = "Pagina N° " & NroPaginaCxC & ": 1 al " & GV_CxC.Rows.Count & " de <strong>" & Session("CxC") & " CxC</strong>"
                Lb_NroPaginaCxC.Text = "Pagina N° " & NroPaginaCxC & ": " & NroPaginacionCXC + 1 & " al " & NroPaginacionCXC + GV_CxC.Rows.Count & " de <strong>" & Session("CxC") & " CxC</strong>"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_CxC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_CxC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim MtoPagar As String
            MtoPagar = CType(e.Row.FindControl("Txt_MtoPagar_CxC"), TextBox).ClientID
            CType(e.Row.FindControl("Txt_MtoPagar_CxC"), TextBox).Attributes.Add("Style", "TEXT-ALIGN: right")
            e.Row.Cells(0).Attributes.Add("onClick", "SelecionaCxc(" & Posicion_CxC & ");")
            Posicion_CxC = Posicion_CxC + 1
        End If
    End Sub

    Protected Sub CB_Seleccionar_CxC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Mto_A_Pagar As TextBox
        Dim CB As CheckBox
        Dim Saldo As Double
        Dim Interes As Double
        Dim DIF_DIA As Integer

        CB = CType(sender, CheckBox)

        'For I = 1 To coll_CXC.Count
        '    For x = 1 To GV_CxC.Rows.Count - 1
        '        If coll_CXC(I).id_cxc = GV_CxC.Rows(x).Cells(3).Text Then
        '            HF_Posicion_CxC.Value = I - 1
        '            Exit For
        '        End If
        '    Next
        'Next

        'Validamos que tenga factor de cambio del dia de la cuenta, sino no lo deja agregar
        If coll_CXC.Item(HF_Posicion_CxC.Value + 1).cxc_fac_cam <= 0 And CB.Checked Then
            Msj.Mensaje_WebControl(Me.Page, "Cuentas Por Cobrar", "No se puede agregar esta CXC por no tener factor de cambio del dia de la cuenta", TipoDeMensaje._Exclamacion)
            CB.Checked = False
            Exit Sub
        End If

        Mto_A_Pagar = CType(GV_CxC.Rows(HF_Posicion_CxC.Value).FindControl("Txt_MtoPagar_CxC"), TextBox)

        Saldo = Val(GV_CxC.Rows(HF_Posicion_CxC.Value).Cells(6).Text.Replace(".", ""))
        Interes = Val(GV_CxC.Rows(HF_Posicion_CxC.Value).Cells(7).Text.Replace(".", ""))

        Dim Formato As String = ""

        Select Case coll_CXC.Item(HF_Posicion_CxC.Value + 1).id_p_0023
            Case 1 : Formato = Fmt.FCMSD
            Case 2, 4 : Formato = Fmt.FCMCD4
            Case 3 : Formato = Fmt.FCMCD
        End Select

        If coll_CXC.Item(HF_Posicion_CxC.Value + 1).pnu_atr_005 = "S" Then
            DIF_DIA = DateDiff("d", CDate(GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(10).Text), Date.Now)

            If CDate(GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(10).Text) = "01/01/1900" Then
                DIF_DIA = DateDiff("d", CDate(GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(9).Text), Date.Now)
            End If

            Interes = (Saldo / 30) * DIF_DIA * (Pagos.TasaInteresCalculo / 100)
            GV_CxC.Rows(HF_Posicion_CxC.Value).Cells(7).Text = Format(Interes, Formato)

        End If

        If CB.Checked Then

            Mto_A_Pagar.CssClass = "clsMandatorio"
            Mto_A_Pagar.ReadOnly = False
            Mto_A_Pagar.Text = Format(Saldo + Interes, Formato)

            coll_CXC.Item(HF_Posicion_CxC.Value + 1).MontoPagar = CType(GV_CxC.Rows(HF_Posicion_CxC.Value).FindControl("Txt_MtoPagar_CxC"), TextBox).Text
            coll_CXC.Item(HF_Posicion_CxC.Value + 1).Interes = CDbl(GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(7).Text) 'Interes
            coll_CXC.Item(HF_Posicion_CxC.Value + 1).Tasa = Pagos.TasaInteresCalculo
            coll_CXC.Item(HF_Posicion_CxC.Value + 1).Contrato = GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(11).Text

            If Not ValidaCollectionCXC(GV_CxC.Rows(HF_Posicion_CxC.Value).Cells(3).Text) Then
                Coll_Cxc_Seleccionados.Add(coll_CXC.Item(HF_Posicion_CxC.Value + 1))
            End If



        Else

            Mto_A_Pagar.Text = 0
            Mto_A_Pagar.CssClass = "clsDisabled"
            Mto_A_Pagar.ReadOnly = True
            GV_CxC.Rows(Val(HF_Posicion_CxC.Value)).Cells(7).Text = 0

            EliminaSeleccionCxC(coll_CXC.Item(HF_Posicion_CxC.Value + 1).id_cxc)

        End If

        CalculaTotalPagoCxC()

    End Sub

    Protected Sub Txt_MtoPagar_CxC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        CalculaTotalPagoCxC()
    End Sub

    Private Sub CalculaTotalPagoCxC()

        Try

            Dim suma As Double = 0
            Txt_Tot_Pag.Text = 0

            For I = 1 To Coll_Cxc_Seleccionados.Count
                suma = suma + CDbl(Coll_Cxc_Seleccionados.Item(I).MontoPagar)
            Next

            Txt_Tot_Pag.Text = Format(suma, Fmt.FCMSD)
            Txt_Tot_Sel.text = Format(Coll_Cxc_Seleccionados.Count, Fmt.FCMSD)

            'UP_CxC.Update()
            'UP_Doc.Update()
            'UpdatePanel1.Update()
            'UpdatePanel2.Update()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub AsignaSourceCxC()

        For I = 0 To GV_CxC.Rows.Count - 1

            Try

                Dim Formato As String = ""
                Dim Mto As Double
                Dim Int As Double

                Select Case coll_CXC.Item(I + 1).id_p_0023
                    Case 1 : Formato = Fmt.FCMSD
                    Case 2, 4 : Formato = Fmt.FCMCD4
                    Case 3 : Formato = Fmt.FCMCD
                End Select

                Mto = CDbl(GV_CxC.Rows(I).Cells(6).Text)

                If GV_CxC.Rows(I).Cells(7).Text = "" Or GV_CxC.Rows(I).Cells(7).Text = "0" Then
                    Int = 0
                Else
                    Int = CDbl(GV_CxC.Rows(I).Cells(7).Text)
                End If

                GV_CxC.Rows(I).Cells(6).Text = Format(Mto, Formato)
                GV_CxC.Rows(I).Cells(7).Text = Format(Int, Formato)

                If CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_Cxc"), TextBox).Text = "" Then
                    CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_Cxc"), TextBox).Text = 0
                Else
                    CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_Cxc"), TextBox).Text = Format(CDbl(CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_Cxc"), TextBox).Text), Formato)
                End If

                If coll_CXC(I + 1).Pago > 0 Then
                    Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FFCC99")
                    Dim cb As CheckBox
                    cb = GV_CxC.Rows(I).FindControl("CB_Seleccionar_CxC")
                    cb.Enabled = False
                    GV_CxC.Rows(I).BackColor = col
                End If

            Catch ex As Exception

            End Try

        Next

    End Sub

    Protected Sub GV_CxC_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GV_CxC.Sorting

        If e.SortExpression = "Todos" Then

            For I = 0 To GV_CxC.Rows.Count - 1

                Dim Mto_A_Pagar As TextBox
                Dim Check As CheckBox
                Dim Saldo As Double
                Dim Interes As Double
                Dim DIF_DIA As Integer

                Check = GV_CxC.Rows(I).FindControl("CB_Seleccionar_CxC")
                Mto_A_Pagar = CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_CxC"), TextBox)

                If Not Check.Checked Then

                    Check.Checked = True
                    Saldo = Val(GV_CxC.Rows(I).Cells(6).Text.Replace(".", ""))
                    Interes = Val(GV_CxC.Rows(I).Cells(7).Text.Replace(".", ""))

                    Dim Formato As String = ""

                    Select Case coll_CXC.Item(I + 1).id_p_0023
                        Case 1 : Formato = Fmt.FCMSD
                        Case 2, 4 : Formato = Fmt.FCMCD4
                        Case 3 : Formato = Fmt.FCMCD
                    End Select

                    If coll_CXC.Item(I + 1).pnu_atr_005 = "S" Then
                        DIF_DIA = DateDiff("d", CDate(GV_CxC.Rows(Val(I)).Cells(9).Text), Date.Now)
                        Interes = (Saldo / 30) * DIF_DIA * (Pagos.TasaInteresCalculo / 100)
                        Me.GV_CxC.Rows(I).Cells(7).Text = Format(Interes, Formato)
                    End If

                    Mto_A_Pagar.CssClass = "clsMandatorio"
                    Mto_A_Pagar.ReadOnly = False
                    Mto_A_Pagar.Text = Format(Saldo + Interes, Formato)

                    coll_CXC.Item(I + 1).MontoPagar = CType(GV_CxC.Rows(I).FindControl("Txt_MtoPagar_CxC"), TextBox).Text
                    coll_CXC.Item(I + 1).Interes = CDbl(GV_CxC.Rows(Val(I)).Cells(7).Text) 'Interes
                    coll_CXC.Item(I + 1).Tasa = Pagos.TasaInteresCalculo
                    coll_CXC.Item(I + 1).Contrato = GV_CxC.Rows(Val(I)).Cells(11).Text

                    If Not ValidaCollectionCXC(GV_CxC.Rows(Val(I)).Cells(3).Text) Then
                        Coll_Cxc_Seleccionados.Add(coll_CXC.Item(I + 1))
                    End If

                Else
                    Check.Checked = False
                    Mto_A_Pagar.CssClass = "clsDisabled"
                    Mto_A_Pagar.ReadOnly = True
                    Mto_A_Pagar.Text = 0
                    EliminaSeleccionCxC(coll_CXC.Item(I + 1).id_cxc)
                End If

            Next

            CalculaTotalPagoCxC()

        End If

    End Sub

    Protected Sub IB_Next_CxC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_CxC.Click

        If GV_CxC.Rows.Count > 0 And GV_CxC.Rows.Count = 200 Then

            NroPaginacionCXC += 200
            NroPaginaCxC += 1

            CargaGrillaCxC()
            MarcaCxCSeleccionados()
            'Lb_NroPaginaCxC.Text = "Pagina N° " & NroPaginaCxC & ": " & NroPaginacionCXC + 1 & " al " & NroPaginacionCXC + GV_CxC.Rows.Count & " de <strong>" & Session("CxC") & " CxC</strong>"

        Else
            Msj.Mensaje_WebControl(Me.Page, "Cuentas Por Cobrar", "No existen más páginas de cuentas por cobrar", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If


    End Sub

    Protected Sub IB_Prev_CxC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_CxC.Click

        
        If NroPaginaCxC > 1 Then

            NroPaginacionCXC -= 200
            NroPaginaCxC -= 1

            CargaGrillaCxC()

            'Lb_NroPaginaCxC.Text = "Pagina N° " & NroPaginaCxC & ": " & NroPaginacionCXC + 1 & " al " & NroPaginacionCXC + GV_CxC.Rows.Count & " de <strong>" & Session("CxC") & " CxC</strong>"
            MarcaCxCSeleccionados()

        Else
            Msj.Mensaje_WebControl(Me.Page, "Cuentas Por Cobrar", "No existen páginas de cuentas por cobrar anteriores", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Private Sub MarcaCxCSeleccionados()

        Try

            For I = 1 To Coll_cxc_Seleccionados.Count

                For X = 0 To GV_CxC.Rows.Count - 1

                    If Coll_Cxc_Seleccionados.Item(I).id_cxc = GV_CxC.Rows(X).Cells(3).Text Then

                        Dim Mto_A_Pagar As TextBox
                        Dim CB As CheckBox

                        CB = GV_CxC.Rows(X).FindControl("CB_Seleccionar_CxC")
                        Mto_A_Pagar = CType(GV_CxC.Rows(X).FindControl("Txt_MtoPagar_CxC"), TextBox)

                        CB.Checked = True
                        Mto_A_Pagar.CssClass = "clsMandatorio"
                        Mto_A_Pagar.ReadOnly = False

                        GV_CxC.Rows(X).Cells(7).Text = Format(Coll_Cxc_Seleccionados.Item(I).Interes, Fmt.FCMSD)
                        Mto_A_Pagar.Text = Format(Coll_Cxc_Seleccionados.Item(I).MontoPagar, Fmt.FCMSD)

                        Exit For

                    End If

                Next

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Function EliminaSeleccionCxC(ByVal id_cxc As Long) As Boolean

        Try

            For I = 1 To Coll_Cxc_Seleccionados.Count

                If Coll_Cxc_Seleccionados.Item(I).id_cxc = id_cxc Then
                    Coll_Cxc_Seleccionados.Remove(I)
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

        End Try

    End Function

    Private Sub CargaCollectionCXC()

        'Se agrega try , se incluye carga para cxc 
        Try

            For PosicionGV = 0 To GV_CxC.Rows.Count - 1

                If CType(GV_CxC.Rows(PosicionGV).FindControl("CB_Seleccionar_CxC"), CheckBox).Checked Then

                    'Le pasamos nuevamente el monto en caso de que este haya variado
                    coll_CXC.Item(PosicionGV + 1).MontoPagar = CType(GV_CxC.Rows(PosicionGV).FindControl("Txt_MtoPagar_CxC"), TextBox).Text
                    coll_CXC.Item(PosicionGV + 1).Interes = CDbl(GV_CxC.Rows(Val(PosicionGV)).Cells(7).Text) 'Interes
                    coll_CXC.Item(PosicionGV + 1).Tasa = Pagos.TasaInteresCalculo
                    coll_CXC.Item(PosicionGV + 1).Contrato = GV_CxC.Rows(Val(PosicionGV)).Cells(11).Text

                    'Cargo Cuentas por cobrar 
                    Coll_Cxc_Seleccionados.Add(coll_CXC.Item(PosicionGV + 1))

                End If

            Next


        Catch ex As Exception

            Msj.Mensaje_WebControl(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Function ValidaCollectionCXC(ByVal NroCxC As Integer) As Boolean


        'Se agrega try , se incluye carga para cxc 
        Try

            For i = 1 To Coll_Cxc_Seleccionados.Count

                If Coll_Cxc_Seleccionados(i).id_cxc = NroCxC Then
                    Return True
                End If

            Next

            Return False

        Catch ex As Exception

            Msj.Mensaje_WebControl(Me.Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try
    End Function

#End Region

#Region "SUB / FUNCTION"

    Private Function CalculaInteres(ByVal PosicionGV As Long, ByVal MtoAPagar As Double) As Double

        Try

            Dim Formulas As New FormulasGenerales
            'Dim MtoAPagar As Double
            Dim Interes As Double
            Dim Saldo As Double
            Dim SaldoCliente As Double
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
            Dim Tasa_Base As Double
            Dim Spread As Double
            Dim Puntos As Double
            Dim Mto_A_Pagar As TextBox
            Dim TD As CheckBox

            Mto_A_Pagar = CType(Gr_Documentos.Rows(PosicionGV).FindControl("Txt_MtoPagar"), TextBox)
            'Mto_A_Pagar.CssClass = "clsMandatorio"
            'Mto_A_Pagar.ReadOnly = False

            TD = CType(Gr_Documentos.Rows(PosicionGV).FindControl("CB_PD"), CheckBox)
            'TD.Checked = True

            'Buscamos el documento para traer todas sus relaciones
            'Dim DOC As doc_cls = OP.DocumentoOtorgagoDevuelvePorId(Coll_DOC.Item(PosicionGV + 1).id_doc)

            'Rescatamos el saldo del documento
            Saldo = CDbl(Gr_Documentos.Rows(PosicionGV).Cells(8).Text)

            SaldoCliente = CDbl(Coll_DOC.Item(PosicionGV + 1).doc_sdo_cli)

            'Select Case Pagador
            '    Case "C"
            '        Saldo = CDbl(Gr_Documentos.Rows(PosicionGV).Cells(8).Text)
            '    Case "D"
            '        Saldo = CDbl(Gr_Documentos.Rows(PosicionGV).Cells(9).Text)
            'End Select

            'Saldo = CDbl(Mto_A_Pagar.Text)

            'Monto a pagar por defecto toma el saldo completo
            If Mto_A_Pagar.Text = "" Then
                Mto_A_Pagar.Text = Saldo
            End If

            'MtoAPagar = CDbl(Mto_A_Pagar.Text)

            'validamos si la fecha de ultimo pago viene nula
            If IsNothing(Coll_DOC.Item(PosicionGV + 1).doc_ful_pgo) Then
                FechaUltPago = "01/01/1900"
            Else
                FechaUltPago = Coll_DOC.Item(PosicionGV + 1).doc_ful_pgo
            End If

            FechaSimula = Coll_DOC.Item(PosicionGV + 1).ope_fec_sim
            FechaVctoRea = Coll_DOC.Item(PosicionGV + 1).dsi_fev_cal
            CantidadDias = Coll_DOC.Item(PosicionGV + 1).dsi_ctd_dia

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).ope_lnl) Then
                Lineal = "N"
            Else
                Lineal = Coll_DOC.Item(PosicionGV + 1).ope_lnl
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).opn_tas_moa) Then
                TasaAnuMen = 0
            Else
                TasaAnuMen = Coll_DOC.Item(PosicionGV + 1).opn_tas_moa
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).doc_tas_ren) Then
                TasaRenova = 0
            Else
                TasaRenova = Coll_DOC.Item(PosicionGV + 1).doc_tas_ren
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).doc_fev) Then
                FecVctoOri = "01/01/1900"
            Else
                FecVctoOri = Coll_DOC.Item(PosicionGV + 1).doc_fev
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).doc_num_ren) Then
                NroRenovac = 0
            Else
                NroRenovac = Coll_DOC.Item(PosicionGV + 1).doc_num_ren
            End If

            MtoAnticip = Coll_DOC.Item(PosicionGV + 1).dsi_mto_ant

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).opn_tas_bas) Then
                Tasa_Base = 0
            Else
                Tasa_Base = Coll_DOC.Item(PosicionGV + 1).opn_tas_bas
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).opn_spr_ead) Then
                Spread = 0
            Else
                Spread = Coll_DOC.Item(PosicionGV + 1).opn_spr_ead
            End If

            If IsNothing(Coll_DOC.Item(PosicionGV + 1).opn_pto_spr) Then
                Puntos = 0
            Else
                Puntos = Coll_DOC.Item(PosicionGV + 1).opn_pto_spr
            End If

            TasaNegocio = Coll_DOC.Item(PosicionGV + 1).opn_tas_neg 'Tasa_Base + Spread + Puntos

            'CDbl(Gr_Documentos.Rows(PosicionGV).Cells(8).Text), _
            Interes = Formulas.RetornaCalculoInteres(Pagos.FechaPago, _
                                                     Pagos.DiasRetencionPago, _
                                                     Pagos.TasaInteresCalculo, _
                                                     MtoAPagar, _
                                                     FechaSimula, _
                                                     FechaVctoRea, _
                                                     CantidadDias, _
                                                     SaldoCliente, _
                                                     FechaUltPago, _
                                                     Pagos.DiasDevolverInteres, _
                                                     Lineal, _
                                                     TasaAnuMen, _
                                                     TasaNegocio, _
                                                     TasaRenova, _
                                                     MtoAnticip, _
                                                     FecVctoOri, _
                                                     NroRenovac, _
                                                     Coll_DOC.Item(PosicionGV + 1).id_doc, _
                                                     Coll_DOC.Item(PosicionGV + 1).cli_dia_bas)

            If Pagador = "D" Then

                If Interes > 0 Then

                    If Coll_DOC.Item(PosicionGV + 1).id_P_0023 = 1 Then
                        Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMSD)
                        Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMSD)
                    Else
                        Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMCD)
                        Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMCD)
                    End If

                Else

                    If Coll_DOC.Item(PosicionGV + 1).id_P_0023 = 1 Then
                        Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMSD)
                        Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMSD)

                    Else
                        Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMCD)
                        Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMCD)
                    End If

                End If

            Else

                If Coll_DOC.Item(PosicionGV + 1).id_P_0023 <> 1 Then
                    Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMCD)
                    Mto_A_Pagar.Text = Format(Interes + MtoAPagar, Fmt.FCMCD)
                Else

                    Gr_Documentos.Rows(PosicionGV).Cells(9).Text = Format(Interes, Fmt.FCMSD)

                    If MtoAPagar <= SaldoCliente And MtoAPagar > Interes Then
                        Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMSD)
                    Else
                        If MtoAPagar = Saldo Then
                            Mto_A_Pagar.Text = Format(Interes + MtoAPagar, Fmt.FCMSD)
                        Else
                            Mto_A_Pagar.Text = Format(MtoAPagar, Fmt.FCMSD)
                        End If
                    End If

                End If

            End If

            Return Interes

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Private Sub CreaIngreso(ByVal Coll_Doc_Cxc As Collection, ByVal Indice_DPO As Integer, ByRef Saldo_DPO As Double, ByVal Ind_Doc As Integer)

        Dim Formulas As New FormulasGenerales
        Dim SaldoPorPagar As Double = 0

        'recorremos cuantos Documentos se pueden pagar con un DPO
        For Indice_Doc = Ind_Doc To Coll_Doc_Cxc.Count

            'Monto a pagar esta sin interes, por lo que se le suma
            SaldoPorPagar = (CDec(Coll_Doc_Cxc.Item(Indice_Doc).MontoPagar) + CDec(Coll_Doc_Cxc.Item(Indice_Doc).Interes)) - Saldo_DPO

            If SaldoPorPagar > 0 Then
                Saldo_DPO = 0
            Else
                'Descontamos el monto pagado al saldo de la DPO para ver cuantos Doc o Cxc puede pagar
                Saldo_DPO = Saldo_DPO - (SaldoPorPagar * -1)
                Ind_Doc = Ind_Doc + 1
            End If

            Dim Ing_Sec As New ing_sec_cls
            Dim ABONO_CLIENTE, EXCEDENTE, MAYOR_PAGO, REAJUSTE As Double

            Ing_Sec.id_ing_sec = Nothing
            Ing_Sec.id_ing = 1
            Ing_Sec.id_doc = Coll_Doc_Cxc.Item(Indice_Doc).id_doc
            Ing_Sec.id_dpo = Indice_DPO
            Ing_Sec.ing_mto_int = CDec(Coll_Doc_Cxc.Item(Indice_Doc).Interes)
            Ing_Sec.ing_mto_abo = CDec(Coll_Doc_Cxc.Item(Indice_Doc).MontoPagar)

            Ing_Sec.ing_qpa = Pagos.Pagador

            Select Case Ing_Sec.ing_qpa
                Case "C"
                    Ing_Sec.doc_sdo_cli = CDec(Coll_Doc_Cxc.Item(Indice_Doc).doc_sdo_cli)
                    Ing_Sec.doc_sdo_ddr = 0
                Case "D"
                    'Deudor no se Cobra Interes
                    Ing_Sec.doc_sdo_ddr = CDec(Coll_Doc_Cxc.Item(Indice_Doc).doc_sdo_ddr)
                    Ing_Sec.doc_sdo_cli = 0
            End Select

            '/*Calcula todo como Pago Deudor***************************************************/
            'ABONO_CLIENTE = MIN(PAGO_APLICAR, SALDO_CLIENTE + MTO_DOCTO - MTO_ANTICIPO)
            ABONO_CLIENTE = Formulas.MIN(Ing_Sec.ing_mto_abo, Ing_Sec.doc_sdo_cli + Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto - Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto_ant)

            'EXCEDENTE = MAX(MIN(PAGO_APLICAR - SALDO_CLIENTE, MTO_DOCTO - MTO_ANTICIPO), 0)
            EXCEDENTE = Formulas.MAX(Formulas.MIN(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli, Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto - Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto_ant), 0)

            'MAYOR_PAGO = MAX(PAGO_APLICAR - SALDO_CLIENTE - (MTO_DOCTO - MTO_ANTICIPO), 0)
            MAYOR_PAGO = Formulas.MAX(Ing_Sec.ing_mto_abo - Ing_Sec.doc_sdo_cli - (Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto - Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto_ant), 0)
            ''/*********************************************************************************/

            If CDec(Coll_Doc_Cxc.Item(Indice_Doc).MontoPagar) >= Ing_Sec.doc_sdo_cli + (Ing_Sec.ing_mto_int) Then
                Ing_Sec.ing_tot_par = "T"
            Else
                Ing_Sec.ing_tot_par = "P"
            End If

            '/*Calculo de Reajuste **************************************************/
            REAJUSTE = Formulas.MIN(ABONO_CLIENTE, Coll_Doc_Cxc.Item(Indice_Doc).dsi_mto) * (FACTOR_CAMBIO_HOY - FACTOR_CAMBIO)
            Ing_Sec.ing_rea_mon = REAJUSTE
            '/***********************************************************************/

            'Ojo ver cuando el interes es negativo
            Ing_Sec.ing_mto_tot = Ing_Sec.ing_mto_abo + Ing_Sec.ing_mto_int

            Ing_Sec.id_P_0053 = 2
            Ing_Sec.ing_fac_cam = Pagos.DollarCobranza
            Ing_Sec.ing_fac_cam_obs = Pagos.DollarObservador
            Ing_Sec.ing_pag_deu = Coll_Doc_Cxc.Item(Indice_Doc).PagaDeudor

            'Crea la collection con los objetos para que luego sean grabados
            Coll_Ing_Sec.Add(Ing_Sec)

            If Saldo_DPO = 0 Then Exit For

        Next

    End Sub

    Private Function ValidaPagosConIngresos() As Boolean

        Dim Pagos As New ClsSession.SesionPagos
        Dim Total_a_Pagar As Double = 0

        For I = 1 To Coll_Doctos_Seleccionados.Count
            Total_a_Pagar = Total_a_Pagar + (Coll_Doctos_Seleccionados.Item(I).MontoPagar + Coll_Doctos_Seleccionados.Item(I).Interes)
        Next

        If Total_a_Pagar <> Pagos.TotalRecaudado Then
            Msj.Mensaje_WebControl(Me.Page, "Seleccion de Documentos", "Debe Modificar Total a Pagar en Doctos. o CXC" & Chr(13) & "Total a Cancelar es Distinto a Total a Cobrar", TipoDeMensaje._Exclamacion)
            Return False
        End If

        Return True

    End Function

    Private Sub LimpiaBusqueda()

        CB_Deudor.Checked = False

        Txt_Rut_Deu.Text = ""
        Txt_Rut_Deu.ReadOnly = True
        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Dig_Deu.Text = ""
        Txt_Dig_Deu.ReadOnly = True
        Txt_Dig_Deu.CssClass = "clsDisabled"
        IB_AyudaDeu.Enabled = False
        Txt_Rso_Deu.Text = ""
        DP_TipoDocto.ClearSelection()
        Txt_Nro_Oto.Text = ""
        Txt_Nro_Doc.Text = ""
        DP_CodCobranza.ClearSelection()
        Txt_Mto_Dsd.Text = ""
        Txt_Mto_Hst.Text = ""
        Txt_Fec_Dsd.Text = ""
        Txt_Fec_Hst.Text = ""
        DP_Estados.ClearSelection()
        
    End Sub

    Private Function ValidaMoneda(ByVal coll As Collection) As Boolean
        Dim moneda As Int16 = 0
        Dim moneda1 As Int16 = 0
        Dim co = From c In coll Group By c.id_p_0023 Into Count()

        If co.Count > 1 Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region

End Class

Public Class Ingreso

    Private _RutDeu As String
    Public Property RutDeu() As String
        Get
            Return _RutDeu
        End Get
        Set(ByVal value As String)
            _RutDeu = value
        End Set
    End Property

    Private _NombreDeu As String
    Public Property NombreDeu() As String
        Get
            Return _NombreDeu
        End Get
        Set(ByVal value As String)
            _NombreDeu = value
        End Set
    End Property

    Private _TD As String
    Public Property TD() As String
        Get
            Return _TD
        End Get
        Set(ByVal value As String)
            _TD = value
        End Set
    End Property

    Private _NroOtor As Long
    Public Property NroOtor() As Long
        Get
            Return _NroOtor
        End Get
        Set(ByVal value As Long)
            _NroOtor = value
        End Set
    End Property

    Private _NroDocto As String
    Public Property NroDocto() As Long
        Get
            Return _NroDocto
        End Get
        Set(ByVal value As Long)
            _NroDocto = value
        End Set
    End Property

    Private _NroCuo As Integer
    Public Property NroCuo() As Integer
        Get
            Return _NroCuo
        End Get
        Set(ByVal value As Integer)
            _NroCuo = value
        End Set
    End Property

    Private _FechaVcto As DateTime
    Public Property FechaVcto() As DateTime
        Get
            Return _FechaVcto
        End Get
        Set(ByVal value As DateTime)
            _FechaVcto = value
        End Set
    End Property

    Private _Saldo As Double
    Public Property Saldo() As Double
        Get
            Return _Saldo
        End Get
        Set(ByVal value As Double)
            _Saldo = value
        End Set
    End Property

    Private _Interes As Double
    Public Property Interes() As Double
        Get
            Return _Interes
        End Get
        Set(ByVal value As Double)
            _Interes = value
        End Set
    End Property

    Private _MontoDocto As Double
    Public Property MontoDocto() As Double
        Get
            Return _MontoDocto
        End Get
        Set(ByVal value As Double)
            _MontoDocto = value
        End Set
    End Property

    Private _MontoAnticipo As Double
    Public Property MontoAnticipo() As Double
        Get
            Return _MontoAnticipo
        End Get
        Set(ByVal value As Double)
            _MontoAnticipo = value
        End Set
    End Property

    Private _MontoPagar As Double
    Public Property MontoPagar() As Double
        Get
            Return _MontoPagar
        End Get
        Set(ByVal value As Double)
            _MontoPagar = value
        End Set
    End Property

    Private _id_Moneda As Integer
    Public Property id_Moneda() As Integer
        Get
            Return _id_Moneda
        End Get
        Set(ByVal value As Integer)
            _id_Moneda = value
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

    Private _FechaUltPago As DateTime
    Public Property FechaUltPago() As DateTime
        Get
            Return _FechaUltPago
        End Get
        Set(ByVal value As DateTime)
            _FechaUltPago = value
        End Set
    End Property

    Private _id_EstCob As Integer
    Public Property id_EstCob() As Integer
        Get
            Return _id_EstCob
        End Get
        Set(ByVal value As Integer)
            _id_EstCob = value
        End Set
    End Property

    Private _EstadoCobranza As String
    Public Property EstadoCobranza() As String
        Get
            Return _EstadoCobranza
        End Get
        Set(ByVal value As String)
            _EstadoCobranza = value
        End Set
    End Property

    Private _id_Estado As Integer
    Public Property id_Estado() As Integer
        Get
            Return _id_Estado
        End Get
        Set(ByVal value As Integer)
            _id_Estado = value
        End Set
    End Property

    Private _Estado As String
    Public Property Estado() As String
        Get
            Return _Estado
        End Get
        Set(ByVal value As String)
            _Estado = value
        End Set
    End Property

    Private _PagaDeudor As Char
    Public Property PagaDeudor() As Char
        Get
            Return _PagaDeudor
        End Get
        Set(ByVal value As Char)
            _PagaDeudor = value
        End Set
    End Property

End Class



