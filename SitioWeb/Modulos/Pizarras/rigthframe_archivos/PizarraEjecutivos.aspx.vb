Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos

Partial Class PizarraEjecutivos
    Inherits System.Web.UI.Page

#Region "Declaraciones de Variables"

    Dim CG As New ConsultasGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim FC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra Comercial"
    Dim Msj As New ClsMensaje
    Dim clasecli As New ClaseClientes
    Dim CMC As New ClaseComercial
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
    
            NroPaginacion = 0

            Response.Expires = -1

            CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)
            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Dim con As New con_cls
            If CodEje <> 0 Then

                DP_Ejecutivos.ClearSelection()
                DP_Ejecutivos.Enabled = False
                DP_Ejecutivos.CssClass = "clsDisabled"
                CB_TodosEje.Checked = True

                Txt_Fec_Desde.Text = Format(Date.Now.AddDays(-1), Var.FMT_FECHA)
                Txt_Fec_Hasta.Text = Format(Date.Now, Var.FMT_FECHA)

                Txt_Fec_Desde.Focus()

                AscRut.Value = "True"
                AscNom.Value = "True"
                AscMon.Value = "True"
                AscMto.Value = "True"
                AscFec.Value = "True"

            
            Else
                Response.Redirect("../../../Index.aspx", False)
            End If

        End If


        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

    End Sub

    Protected Sub CB_TodosEje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TodosEje.CheckedChanged

        DP_Ejecutivos.ClearSelection()

        Try
            DP_Ejecutivos.Items.FindByValue(0).Selected = True

            If CB_TodosEje.Checked Then
                DP_Ejecutivos.Enabled = False
                DP_Ejecutivos.CssClass = "clsDisabled"

            Else
                DP_Ejecutivos.Enabled = True
                DP_Ejecutivos.CssClass = "clsMandatorio"
                DP_Ejecutivos.Focus()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged

        If CB_Cliente.Checked Then

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli.Focus()

            Me.Txt_Raz_Soc.Text = ""
            Me.Txt_Rut_Cli.Text = ""
            Me.Txt_Dig_Cli.Text = ""

            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Me.IB_AyudaCli.Enabled = True
        Else
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Me.IB_AyudaCli.Enabled = False
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"

            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Me.Txt_Raz_Soc.Text = ""

        End If

    End Sub

    Protected Sub GV_Evaluaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Evaluaciones.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim rut As Long
            Dim neg As Integer
            Dim apb As Integer
            Dim sim As Integer
            Dim oto As Integer

            rut = e.Row.Cells(0).Text
            
            neg = coll_ope_anu.Item(e.Row.DataItemIndex + 1).Neg 'e.Row.Cells(10).Text
            apb = coll_ope_anu.Item(e.Row.DataItemIndex + 1).Apb 'e.Row.Cells(12).Text
            sim = coll_ope_anu.Item(e.Row.DataItemIndex + 1).Sim 'e.Row.Cells(12).Text
            oto = coll_ope_anu.Item(e.Row.DataItemIndex + 1).Oto 'e.Row.Cells(13).Text

            If neg > 0 Then
                CType(e.Row.FindControl("Img_Neg"), Image).Attributes.Add("onMouseOver", "showToolTip(event,'" & neg & "', '" & rut & "', 1)")
                CType(e.Row.FindControl("Img_Neg"), Image).Attributes.Add("onMouseOut", "hideTooltip(event)")
            End If

            If sim > 0 Then
                CType(e.Row.FindControl("Img_Sim"), Image).Attributes.Add("onMouseOver", "showToolTip(event,'" & sim & "', '" & rut & "', 2)")
                CType(e.Row.FindControl("Img_Sim"), Image).Attributes.Add("onMouseOut", "hideTooltip(event)")

                'Aprobaciones (solo tiene cuando se creo la Negociación)
                CType(e.Row.FindControl("Img_Apb"), Image).Attributes.Add("onMouseOver", "Aprobacion(" & e.Row.DataItemIndex & ")")
                CType(e.Row.FindControl("Img_Apb"), Image).Attributes.Add("onMouseOut", "Aprobacion(0)")
                CType(e.Row.FindControl("Img_Apb"), Image).Attributes.Add("onClick", "WinOpen(2, 'Aprobaciones.aspx?neg=" & neg & "&rut=" & rut & "&sim=" & sim & "', 'Aprobaciones',1000,350,250,400)")
                CType(e.Row.FindControl("Img_Apb"), Image).Attributes.Add("Style", "cursor: hand")
            Else
                CType(e.Row.FindControl("Img_Apb"), Image).Enabled = False
            End If

            If oto > 0 Then
                CType(e.Row.FindControl("Img_Oto"), Image).Attributes.Add("onMouseOver", "showToolTip(event,'" & oto & "', '" & rut & "', 3)")
                CType(e.Row.FindControl("Img_Oto"), Image).Attributes.Add("onMouseOut", "hideTooltip(event)")
            End If

        End If

    End Sub

    Private Sub CargaGrilla(ByVal Orden As Integer, Optional ByVal Ascendiente As Boolean = True)

        Try

            Dim Eje_Dsd As Integer
            Dim Eje_Hst As Integer

            Dim Rut_Dsd As Long
            Dim Rut_Hst As Long

            '---------------------------------------------------------------------------------------
            'Cargamos las variables segun criterio de busqueda
            '---------------------------------------------------------------------------------------

            If DP_Ejecutivos.SelectedIndex > 0 Then
                Eje_Dsd = DP_Ejecutivos.SelectedValue
                Eje_Hst = DP_Ejecutivos.SelectedValue
            Else
                Eje_Dsd = 0
                Eje_Hst = 999
            End If

            If Txt_Fec_Desde.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar fecha desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Me.Txt_Fec_Desde.Text) Then
                Msj.Mensaje(Me, "Atención", "Fecha desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Fec_Desde.Text = ""
                Exit Sub
            End If

            If Txt_Fec_Hasta.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar fecha hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Me.Txt_Fec_Hasta.Text) Then
                Msj.Mensaje(Me, "Atención", "Fecha hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Fec_Hasta.Text = ""
                Exit Sub
            End If

            If CB_Cliente.Checked Then

                If Txt_Rut_Cli.Text <> "" And Txt_Dig_Cli.Text <> "" Then
                    Rut_Dsd = Txt_Rut_Cli.Text.Replace(",", "")
                    Rut_Hst = Txt_Rut_Cli.Text.Replace(",", "")
                Else
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Cliente a Buscar", TipoDeMensaje._Informacion)
                    Exit Sub
                End If
            Else
                Rut_Dsd = 0
                Rut_Hst = 999999999999
            End If

            coll_ope_anu = New Collection

            coll_ope_anu = CMC.EvaluacionFlujo_Devuelve(Eje_Dsd, Eje_Hst, _
                                                        Rut_Dsd, Rut_Hst, _
                                                        Txt_Fec_Desde.Text, Txt_Fec_Hasta.Text)


            Dim VariableOrden As String = ""

            'ORDEN
            '1.- RUT
            '2.- NOMBRE
            '3.- MONEDA
            '4.- MONTO
            '5.- FECHA

            Select Case Orden
                Case 1 : VariableOrden = "RutCli"
                Case 2 : VariableOrden = "Razon"
                Case 3 : VariableOrden = "id_Mon"
                Case 4 : VariableOrden = "Monto"
                Case 5 : VariableOrden = "Fecha"
            End Select

            FC.SortCollection(coll_ope_anu, VariableOrden, Ascendiente)

            GV_Evaluaciones.DataSource = coll_ope_anu
            GV_Evaluaciones.DataBind()

            Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

            '---------------------------------------------------------------------------------------
            'Le damos formato a la grilla
            '---------------------------------------------------------------------------------------
            If GV_Evaluaciones.Rows.Count > 0 Then

                For I = 0 To GV_Evaluaciones.Rows.Count - 1

                    'NIT del Cliente
                    Dim rut As Long = GV_Evaluaciones.Rows(I).Cells(0).Text

                    GV_Evaluaciones.Rows(I).Cells(0).Text = Format(Rut, Fmt.FCMSD) & "-" & FC.Vrut(Rut)


                    'Formato de la moneda
                    Select Case coll_ope_anu.Item(I + 1).id_Mon
                        Case 1
                            GV_Evaluaciones.Rows(I).Cells(3).Text = Format(coll_ope_anu.Item(I + 1).Monto, Fmt.FCMSD)
                        Case 2
                            GV_Evaluaciones.Rows(I).Cells(3).Text = Format(coll_ope_anu.Item(I + 1).Monto, Fmt.FCMCD4)
                        Case 3, 4
                            GV_Evaluaciones.Rows(I).Cells(3).Text = Format(coll_ope_anu.Item(I + 1).Monto, Fmt.FCMCD)
                    End Select

                    If coll_ope_anu.item(I + 1).Estado = 5 Then 'Cambiar color de las anuladas
                        Me.GV_Evaluaciones.Rows(I).BackColor = System.Drawing.ColorTranslator.FromHtml("#c0c0c0")
                        GV_Evaluaciones.Rows(I).Cells(13).Text = ""
                    End If

                Next

                If coll_ope_anu.Count < 8 Then
                    IB_Next.Enabled = False
                Else
                    IB_Next.Enabled = True
                End If

            Else
                Msj.Mensaje(Me.Page, Caption, "No se encontraron Evaluaciones para el criterio de búsqueda utilizado", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Comercial"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click


        NroPaginacion += 8
        NroPagina += 1
        Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

        CargaGrilla(1)

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion > 1 Then

            IB_Next.Enabled = True

            NroPaginacion -= 8
            NroPagina -= 1
            Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

            CargaGrilla(1)

        Else
            Msj.Mensaje(Me.Page, Caption, "No existe pagina anterior", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try

            Dim RC As New FuncionesGenerales.FComunes

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar Digito del NIT del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If

            If UCase(Txt_Dig_Cli.Text) <> RC.Vrut(Txt_Rut_Cli.Text.Replace(".", "")) Then
                Msj.Mensaje(Me.Page, Caption, "Digito Incorrecto del Cliente", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If

            Dim Cliente As cli_cls

            Cliente = clasecli.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)
            If valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If IsNothing(Cliente) Then
                Msj.Mensaje(Me.Page, Caption, "Cliente no existe", TipoDeMensaje._Exclamacion)
                Exit Sub
            Else

                'CB_Cliente.Enabled = False

                If Cliente.id_P_0044 = 1 Then
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                Else
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim
                End If

                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"

                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.ReadOnly = True

                IB_AyudaCli.Enabled = False


            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Ib_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_limpiar.Click

        Me.CB_Cliente.Checked = False

        Me.Txt_Rut_Cli.Text = ""
        Me.Txt_Rut_Cli.CssClass = "clsDisabled"
        Me.Txt_Dig_Cli.Text = ""
        Me.Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Me.Txt_Fec_Desde.Text = PrimerDiaMes(Date.Now)
        Me.Txt_Fec_Hasta.Text = UltimoDiaMes(Date.Now)

        Me.CB_TodosEje.Checked = True
        Me.DP_Ejecutivos.SelectedValue = 0
        Me.GV_Evaluaciones.DataSource = Nothing
        Me.GV_Evaluaciones.DataBind()
        Txt_Raz_Soc.Text = ""
        DP_Ejecutivos.Enabled = False
        DP_Ejecutivos.CssClass = "clsDisabled"

        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        IB_AyudaCli.Enabled = False
        NroPaginacion = 0
        IB_Next.Enabled = False
        IB_Prev.Enabled = False
    End Sub

    Public Function UltimoDiaMes(ByVal dtFecha As Date) As Date

        UltimoDiaMes = DateSerial(Year(dtFecha), Month(dtFecha) + 1, 0)

    End Function

    Public Function PrimerDiaMes(ByVal dtFecha As Date) As Date

        PrimerDiaMes = DateSerial(Year(dtFecha), Month(dtFecha), 1)

    End Function

#End Region


#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        NroPaginacion = 0
        NroPagina = 1
        CargaGrilla(1)
        Lbl_Pagina.Text = "Pagina N° 1"

    End Sub

#End Region

End Class
