Imports ClsSession.ClsSession
Imports CapaDatos

Public Class PizarraAprobaciones
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Private CG As New ConsultasGenerales
    Private AG As New ActualizacionesGenerales
    Private RG As New FuncionesGenerales.FComunes
    Private Caption As String = "Pizarra Aprobaciones"
    Private RW As New FuncionesGenerales.RutinasWeb
    Private Msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial
    Dim CD As New ClaseControlDual
    Dim CA As New ClaseArchivos

#End Region

#Region "EVENTOS "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                CB_TodasOpe.Checked = True
                CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)
                DP_Ejecutivos.ClearSelection()

                If Not IsNothing(CodEje) Then

                    Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                    CargaGrillaOperaciones()
                    NroOperacion = 0
                    NroNegociacion = 0
                    NroPaginacion = 0
                    RutCli = 0

                    DP_Ejecutivos.CssClass = "clsDisabled"
                    DP_Ejecutivos.Enabled = False

                End If

            Else

                If HF_NroOpe.Value <> "" Then

                    MarcaGrillaOperacion(HF_NroOpe.Value)

                    If HF_NroNNC.Value <> "" Then
                        CargaFirmas()
                    End If

                End If

            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            IB_Evaluacion.Attributes.Add("onClick", "javascript:VerEvaluacion();")
            IB_Negociacion.Attributes.Add("onClick", "javascript:VerNegociacion();")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "El Ejecutivo debe ser de Tipo Ejecutivo de Cuentas", ClsMensaje.TipoDeMensaje._Exclamacion)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Control Dual"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        Try
            If CB_Cliente.Checked Then
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"

                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""

                Txt_Raz_Soc.Text = ""
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False
                Me.IB_AyudaCli.Enabled = True
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True

                Txt_Rut_Cli.Focus()

            Else
                Me.IB_AyudaCli.Enabled = False
                LimpiaCliente()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GV_OPE.Rows.Count < 5 Then
                Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

            If GV_OPE.Rows.Count = 5 Then
                NroPaginacion += 5
                CargaGrillaOperaciones()
            End If

            GV_Clasificacion.DataSource = New Collection
            GV_Clasificacion.DataBind()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try


            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion >= 5 Then
                NroPaginacion -= 5
                CargaGrillaOperaciones()

            End If


            GV_Clasificacion.DataSource = New Collection
            GV_Clasificacion.DataBind()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim ver As ImageButton = CType(sender, ImageButton)

            For i = 0 To GV_OPE.Rows.Count - 1

                If (ver.ToolTip = CType(GV_OPE.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip) Then

                    HF_NroOpe.Value = ver.ToolTip 'GV_OPE.Rows(i).Cells(0).Text
                    HF_NroNeg.Value = GV_OPE.Rows(i).Cells(0).Text

                    RutCli = RG.LimpiaRut(GV_OPE.Rows(i).Cells(1).Text)

                    Exit For

                End If

            Next

            If HF_NroOpe.Value.Length > 0 And HF_NroNeg.Value.Length > 0 Then

                MarcaGrillaOperacion(HF_NroOpe.Value)

                CD.NegociacionClasificacionDevuelve(HF_NroNeg.Value, GV_Clasificacion)

                Dim CLI As cli_cls
                Dim RSC As Object
                Dim LDC As ldc_cls
                Dim NEG As opn_cls
                Dim OPE As ope_cls
                Dim APC As Object
                Dim SBL_DEU As Object
                Dim SBL_PRO As Object
                Dim Coll_Pgr As Collection

                NroNegociacion = HF_NroNeg.Value
                NroOperacion = HF_NroOpe.Value

                Dim CLSCLI As New ClaseClientes
                Dim Sesion As New ClsSession.ClsSession

                CLI = CLSCLI.ClientesDevuelve(RutCli, RG.Vrut(RutCli))

                If Sesion.valida_cliente <> "" Then
                    Msj.Mensaje(Me, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                Session("Cliente") = CLI

                RutCli = CLI.cli_idc

                'Cargamos los objetos que se utilizaran en caso de que esten ya hayan sido cargados los rescatamos

                If IsNothing(LineaCredito) Then
                    LDC = CG.LineaDeCreditoDevuelve(RutCli, 1)
                    LineaCredito = LDC
                Else
                    LDC = CG.LineaDeCreditoDevuelve(RutCli, 1)
                    LineaCredito = LDC
                End If

                If IsNothing(ResumenCliente) Then
                    RSC = CMC.ResumenClienteDevuelve(RutCli, CodEje)
                    ResumenCliente = RSC
                Else
                    RSC = CMC.ResumenClienteDevuelve(RutCli, CodEje)
                    ResumenCliente = RSC
                End If

                If Not IsNothing(LDC) Then

                    APC = CG.AnticipoDevuelvePorLinea(False, Nothing, LDC.id_ldc, LDC.id_ldc, 1, 999)
                    If Not IsNothing(APC) Then
                        Anticipos = APC
                    End If

                    SBL_DEU = CG.SubLineasDevuelvePorLinea(Nothing, LDC.id_ldc, TipoSubLinea.TipoDocumento)
                    If Not IsNothing(SBL_DEU) Then
                        'SubLineaDeudor = SBL_DEU
                        SubLineaProducto = SBL_DEU
                    End If

                    SBL_PRO = CG.SubLineasDevuelvePorLinea(Nothing, LDC.id_ldc, TipoSubLinea.Deudor)
                    If Not IsNothing(SBL_PRO) Then
                        'SubLineaProducto = SBL_PRO
                        SubLineaDeudor = SBL_PRO
                    End If

                End If

                NEG = CMC.NegociacionDevuelve(RutCli, NroNegociacion)
                Negociacion = NEG

                HF_NroEva.Value = NEG.id_eva

                OPE = OP.OperacionDevuelve(RutCli, NroOperacion)
                Operacion = OPE


                Coll_Pgr = OP.PagareDeOperacionDevuelve(NroOperacion)

                If Coll_Pgr.Count > 0 Then
                    Coll_Pagare = Coll_Pgr
                End If

                Table_Firmas.Controls.Clear()
                Table_Firmas.Dispose()

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub

    Protected Sub Img_Ver_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim ver As ImageButton = CType(sender, ImageButton)

            HF_NroNNC.Value = ver.ToolTip
            CargaFirmas()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_TodasOpe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TodasOpe.CheckedChanged

        DP_Ejecutivos.ClearSelection()

        If CB_TodasOpe.Checked Then
            DP_Ejecutivos.CssClass = "clsDisabled"
            DP_Ejecutivos.Enabled = False
        Else
            DP_Ejecutivos.CssClass = "clsMandatorio"
            DP_Ejecutivos.Enabled = True
            DP_Ejecutivos.Focus()
        End If

    End Sub

#End Region

#Region "PROCEDIMIENTOS Y FUNCIONES"

    Private Sub CargaGrillaOperaciones()

        Try

            Dim Ejecutivo_Desde As Integer
            Dim Ejecutivo_Hasta As Integer

            Dim Cliente_Desde As Long
            Dim Cliente_Hasta As Long

            If CB_TodasOpe.Checked Then
                Ejecutivo_Desde = 0
                Ejecutivo_Hasta = 999999999
            Else
                If DP_Ejecutivos.SelectedIndex > 0 Then
                    Ejecutivo_Desde = DP_Ejecutivos.SelectedValue
                    Ejecutivo_Hasta = DP_Ejecutivos.SelectedValue
                End If
            End If

            If CB_Cliente.Checked Then
                Cliente_Desde = Txt_Rut_Cli.Text
                Cliente_Hasta = Txt_Rut_Cli.Text
            Else
                Cliente_Desde = 0
                Cliente_Hasta = 9999999999999
            End If

            'OP.OperacionesIngresadasTodasDevuelve(GV_OPE, Ejecutivo_Desde, Ejecutivo_Hasta, Pfl, Cliente_Desde, Cliente_Hasta)

            OP.OperacionesSimuladasTodasDevuelve(GV_OPE, Ejecutivo_Desde, Ejecutivo_Hasta, Pfl, Cliente_Desde, Cliente_Hasta)

            GV_Clasificacion.Controls.Clear()

            If GV_OPE.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No existen operaciones para aprobar, por su perfil.", ClsMensaje.TipoDeMensaje._Exclamacion)
            Else
                IB_Next.Enabled = True
                IB_Prev.Enabled = True

            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub MarcaGrillaOperacion(ByVal nrope As Integer)

        Try

            For I = 0 To GV_OPE.Rows.Count - 1

                If (nrope = CType(GV_OPE.Rows(I).FindControl("Img_Ver"), ImageButton).ToolTip) Then
                    If (I Mod 2) = 0 Then
                        GV_OPE.Rows(I).CssClass = "selectable"
                    Else
                        GV_OPE.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_OPE.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_OPE.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub MarcaGrillaClasificacion(ByVal nrocla As Integer)

        Try

            For I = 0 To GV_Clasificacion.Rows.Count - 1

                Dim ver As ImageButton = CType(GV_Clasificacion.Rows(I).FindControl("Img_Ver"), ImageButton)

                If (nrocla = ver.ToolTip) Then

                    HF_NroCCF.Value = CType(GV_Clasificacion.Rows(I).FindControl("HF_CCF"), HiddenField).Value

                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "selectable"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LimpiaCliente()

        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""

        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"


        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        IB_AyudaCli.Enabled = False
        CB_Cliente.Checked = False

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try
            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Function
            End If
            If UCase(Txt_Dig_Cli.Text) <> UCase(RG.Vrut(Replace(Txt_Rut_Cli.Text, ".", ""))) Then
                Msj.Mensaje(Me, Caption, "Rut Incorrecto del Cliente", TipoDeMensaje._Informacion)
                Exit Function
            End If

            CLI = ClsCli.ClientesDevuelve(Replace(Txt_Rut_Cli.Text, ".", ""), Txt_Dig_Cli.Text)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
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

            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True

            'CB_Cliente.Enabled = False
            Me.IB_AyudaCli.Enabled = False
            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Private Sub CargaFirmas()

        Try


            If HF_NroNNC.Value.Length > 0 Then

                MarcaGrillaOperacion(HF_NroOpe.Value)
                MarcaGrillaClasificacion(HF_NroNNC.Value)

                Table_Firmas.CssClass = "tablanivel"

                Table_Firmas.Controls.Clear()
                Table_Firmas.Dispose()

                CD.NegociacionFirmasDevuelve(HF_NroNNC.Value, CodEje, Table_Firmas)
                Table_Firmas.DataBind()

                If Table_Firmas.Rows.Count > 0 Then
                    Dim sw As Integer = 0

                    Dim coll As Collection

                    coll = CD.AprobacionRescataEstado(HF_NroNNC.Value, Sucursal)

                    If Not IsNothing(coll) Then
                        For i = 1 To coll.Count - 1
                            If coll.Item(i).apb_est_ado = "1" Then
                                sw = 1
                                Exit For
                            End If
                        Next
                    End If

                    '**********************************************************************************************************************

                    IB_Aprobar.Enabled = True
                    IB_Rechazar.Enabled = True

                    IB_Aprobar.Attributes.Add("OnClick", "WinOpen(2, 'ManObservacion.aspx?Estado=1&clasificacion=" & HF_NroCCF.Value & "&Est=" & sw & "', 'Aprobar', 470, 300, 250,250);")
                    IB_Rechazar.Attributes.Add("OnClick", "WinOpen(2, 'ManObservacion.aspx?Estado=2&clasificacion=" & HF_NroCCF.Value & "', 'Rechazar', 470, 300, 250,250);")

                End If

                UpdatePanel1.Update()
                UP_Botonera.Update()

            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "Error Carga Firmas: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "BOTONERA "

    Protected Sub IB_Aprobar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Rechazar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    End Sub

    Protected Sub IB_Detalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Detalle.Click

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Limpiar.Click




        LimpiaCliente()
        Me.IB_AyudaCli.Enabled = False
        GV_OPE.DataSource = Nothing
        GV_OPE.DataBind()

        Me.GV_OPE.Controls.Clear()

        GV_Clasificacion.DataSource = Nothing
        GV_Clasificacion.DataBind()
        GV_Clasificacion.Controls.Clear()

        HF_Estado.Value = ""
        HF_NroCCF.Value = ""
        HF_NroEva.Value = ""
        HF_NroNeg.Value = ""
        HF_NroNNC.Value = ""
        HF_NroOpe.Value = ""

        Table_Firmas.Controls.Clear()
        Table_Firmas.Dispose()
        IB_Aprobar.Enabled = False
        IB_Rechazar.Enabled = False

        DP_Ejecutivos.Focus()
        CB_Cliente.Enabled = True

        NroPaginacion = 0
        NroOperacion = 0
        NroNegociacion = 0
        RutCli = 0
        'Me.UpdatePanel1.Update()


        IB_Next.Enabled = False
        IB_Prev.Enabled = False



    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            NroPaginacion = 0
            CargaGrillaOperaciones()
            'UpdatePanel1.Update()
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "SUB GRILLAS"

    Protected Sub GV_Clasificacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clasificacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim id As Integer

            Dim lbl As HiddenField
            Dim img As Image

            lbl = e.Row.FindControl("HF_CCF")
            id = lbl.Value

            img = CType(e.Row.FindControl("Image1"), Image)

            img.Attributes.Add("onMouseOver", "showClasificacion(event,'" & id & "')")
            img.Attributes.Add("onMouseOut", "hideTooltip(event)")

        End If

    End Sub

#End Region

#Region "LINKBUTTON"

    Protected Sub LB_RefrescaFirmas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_RefrescaFirmas.Click

        Try


            MarcaGrillaOperacion(HF_NroOpe.Value)
            MarcaGrillaClasificacion(HF_NroNNC.Value)

            Table_Firmas = New Table
            Table_Firmas.CssClass = "tablanivel"
            CD.NegociacionFirmasDevuelve(HF_NroNNC.Value, Sucursal, Table_Firmas)
            Table_Firmas.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try

            CargaDatosCliente()

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class

