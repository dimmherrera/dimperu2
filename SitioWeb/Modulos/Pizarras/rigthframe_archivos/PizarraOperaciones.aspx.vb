Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos


Partial Class Modulos_Pizarras_rigthframe_archivos_PizarraOperaciones
    Inherits System.Web.UI.Page


#Region "DECLARACION DE VARIABLES"

    Private CG As New ConsultasGenerales
    Private AG As New ActualizacionesGenerales
    Private RG As New FuncionesGenerales.FComunes
    Private Caption As String = "Pizarra Operaciones"
    Private RW As New FuncionesGenerales.RutinasWeb
    Private Msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
#End Region


#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Response.Expires = -1
            CB_TodosEje.Checked = True
            CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)
            NroPaginacion = 0

            DP_Ejecutivos.ClearSelection()

            DP_Ejecutivos.CssClass = "clsDisabled"
            DP_Ejecutivos.Enabled = False

            If CodEje > 0 Then
                If Not IsNothing(DP_Ejecutivos.Items.FindByValue(CodEje)) Then
                    DP_Ejecutivos.Items.FindByValue(CodEje).Selected = True
                End If

            Else
                Response.Redirect("../../../Index.aspx", False)
            End If

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            CargaGrillaOperaciones()

        End If
        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then
            Modulo = "Operacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If

    End Sub

    Protected Sub GV_Operaciones_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Operaciones.PageIndexChanging

        GV_Operaciones.PageIndex = e.NewPageIndex

        CargaGrillaOperaciones()

    End Sub

    Protected Sub GV_Operaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Operaciones.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Operaciones, 'selectable');")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Operaciones, 'formatable');")
            'e.Row.Attributes.Add("onClick", "Click_A_Operacion(ctl00_ContentPlaceHolder1_GV_Operaciones, 'clicktable', 'formatable', 'selectable')")
        End If

    End Sub

    


#End Region


    Private Sub CargaGrillaOperaciones()

        Try

            Dim Ejecutivo_Desde As Integer
            Dim Ejecutivo_Hasta As Integer

            Dim Cliente_Desde As Long
            Dim Cliente_Hasta As Long

            If CB_TodosEje.Checked Then
                Ejecutivo_Desde = 0
                Ejecutivo_Hasta = 999999999
            Else
                If DP_Ejecutivos.SelectedIndex > 0 Then
                    Ejecutivo_Desde = DP_Ejecutivos.SelectedValue
                    Ejecutivo_Hasta = DP_Ejecutivos.SelectedValue
                Else
                    Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un Ejecutivo", TipoDeMensaje._Informacion)
                End If
            End If

            If CB_Cliente.Checked Then
                Cliente_Desde = Txt_Rut_Cli.Text
                Cliente_Hasta = Txt_Rut_Cli.Text
            Else
                Cliente_Desde = 0
                Cliente_Hasta = 9999999999999
            End If

            coll_ope = New Collection

            coll_ope = OP.OperacionesTodasDevuelve(Cliente_Desde, Cliente_Hasta, _
                                                   Ejecutivo_Desde, Ejecutivo_Hasta, _
                                                   1, 1, _
                                                   "01-01-1900", "01-01-2100", _
                                                   0, 999, _
                                                   0, 999, _
                                                   0, 999999999, _
                                                   20)

            If coll_ope.Count = 0 Then
                Msj.Mensaje(Me.Page, Caption, "No se encontraron operaciones ingresadas o simuladas", ClsMensaje.TipoDeMensaje._Informacion)
            Else

                GV_Operaciones.DataSource = coll_ope
                GV_Operaciones.DataBind()

                Dim Formato_Moneda As String = ""
                Dim Fmt As New FuncionesGenerales.ClsLocateInfo

                For I = 0 To GV_Operaciones.Rows.Count - 1

                    GV_Operaciones.Rows(I).Cells(2).Text = RG.FormatoMiles(GV_Operaciones.Rows(I).Cells(2).Text) & "-" & RG.Vrut(CLng(GV_Operaciones.Rows(I).Cells(2).Text))

                    Select Case coll_ope.Item(I + 1).ID_p_0023
                        Case 1 : Formato_Moneda = Fmt.FCMSD
                        Case 2 : Formato_Moneda = Fmt.FCMCD4
                        Case 3, 4 : Formato_Moneda = Fmt.FCMCD
                    End Select

                    GV_Operaciones.Rows(I).Cells(8).Text = Format(CDbl(GV_Operaciones.Rows(I).Cells(8).Text), Formato_Moneda)

                Next

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub AvanzaGrid()
        GV_Operaciones.DataSource = coll_ope
        GV_Operaciones.DataBind()
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub CB_TodosEje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TodosEje.CheckedChanged

        If CB_TodosEje.Checked Then
            DP_Ejecutivos.Enabled = False
            DP_Ejecutivos.CssClass = "clsDisabled"
            DP_Ejecutivos.ClearSelection()
        Else
            DP_Ejecutivos.Enabled = True
            DP_Ejecutivos.CssClass = "clsMandatorio"
        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        '20020105

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010105, Usr, "VALIDA INGRESO AL SISTEMA TITULO VALOR") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        GV_Operaciones.DataSource = New Collection
        GV_Operaciones.DataBind()

        CargaGrillaOperaciones()
    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        If CB_Cliente.Checked Then
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli.Focus()
            IB_AyudaCli.Enabled = True
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
        Else
            IB_AyudaCli.Enabled = False
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True

            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Rut_Cli.Focus()
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""

        End If

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        CargaDatosCliente()
    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            If UCase(Txt_Dig_Cli.Text) <> UCase(RG.Vrut(Replace(Txt_Rut_Cli.Text, ".", ""))) Then
                Msj.Mensaje(Me, Caption, "Nit Incorrecto del Cliente", TipoDeMensaje._Informacion)
                Exit Function
            End If

            CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Me.Txt_Dig_Cli.Text)



            If valida_cliente <> "" Then
                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            Else
                If IsNothing(CLI) Then
                    Msj.Mensaje(Page, Caption, "NIT no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Function
                End If
                Session("Cliente") = CLI

                'Tipo de cliente (Natural / Juridico)
                If CLI.id_P_0044 = 1 Then
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
                Else
                    Me.Txt_Raz_Soc.Text = CLI.cli_rso
                End If

                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                IB_AyudaCli.Enabled = False
                CB_Cliente.Enabled = False
                Me.IB_AyudaCli.Enabled = False
                Return True
            End If
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            CB_TodosEje.Checked = True
            DP_Ejecutivos.ClearSelection()
            CB_Cliente.Checked = False
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"


            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            IB_AyudaCli.Enabled = False

            GV_Operaciones.DataSource = Nothing
            GV_Operaciones.DataBind()

            coll_ope = New Collection
            CB_Cliente.Enabled = True
            DP_Ejecutivos.Enabled = False
            DP_Ejecutivos.CssClass = "clsDisabled"
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            HF_NroOpe.Value = ""
            'HF_PosOpe.Value = ""

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 21 Then
            NroPaginacion -= 21
            CargaGrillaOperaciones()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Operaciones.Rows.Count < 21 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Operaciones.Rows.Count = 21 Then
            NroPaginacion += 21
            CargaGrillaOperaciones()

        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim img_ver As ImageButton = CType(sender, ImageButton)

            HF_NroOpe.Value = img_ver.ToolTip

            If HF_NroOpe.Value = "" Then
                Msj.Mensaje(Me, "Atención", "Debe seleccionar una operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            For i = 0 To GV_Operaciones.Rows.Count - 1
                If (HF_NroOpe.Value = CType(GV_Operaciones.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip) Then
                    RutCli = RG.LimpiaRut(GV_Operaciones.Rows(i).Cells(2).Text)
                    Exit For
                End If
            Next

            'If UCase(Trim(Me.GV_Operaciones.Rows(Val(Me.HF_PosOpe.Value - 1)).Cells(8).Text)) <> "DIGITADA" Then
            'Msj.Mensaje(Me, "Atención", "Debe seleccionar una operación Digitada", ClsMensaje.TipoDeMensaje._Exclamacion)
            'Me.GV_Operaciones.Rows(Me.HF_PosOpe.Value - 1).CssClass = "clicktable"
            'Exit Sub
            'End If

            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20020105, Usr, "PRESIONA  BOTON IR A CUADRATURA DE DOCTOS") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If



            Response.Redirect("../../Operaciones/rightframe_archivos/ing-ope.aspx?id=" & HF_NroOpe.Value, False)

        Catch ex As Exception

        End Try


    End Sub

End Class
