Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class VB_Pagos
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Visto Bueno Pagos"
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Pagos As New ClsSession.SesionPagos
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim clasecli As New ClaseClientes
    Dim PGO As New ClasePagos

#End Region

#Region "BOTONERA"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click


        CB_Cliente.Checked = False
        CB_Cliente.Enabled = True
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        HF_Id_Ing.Value = "" 'FY 03-05-2012
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False

        LimpiaCriterio()
        LimpiaGridView()

        IB_BuscarPagos.Enabled = True
        IB_AyudaCli.Enabled = False

        Txt_FechaPago.CssClass = "clsMandatorio"
        Txt_FechaPago.ReadOnly = False
        Txt_FechaPago.Text = Format(Date.Now, Var.FMT_FECHA)
        Txt_FechaPago_CalendarExtender.Enabled = True
        Txt_FechaPago_MaskedEditExtender.Enabled = True
        Txt_FechaPago.Focus()

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            If Not agt.ValidaAccesso(20, 20050303, Usr, "PRESIONO BUSCAR CXC") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Val(HF_Id_Ing.Value) > 0 Then

                Dim RW As New FuncionesGenerales.RutinasWeb
                RW.AbrePopup(Me, 1, "../../Pagos/rightframe_archivos/InformeDePagos.aspx?id=" & HF_Id_Ing.Value, "Pagos", 1200, 800, 10, 10)
            Else
                Msj.Mensaje(Me.Page, Caption, "Debe Seleccionar un pago", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_BuscarPagos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_BuscarPagos.Click

        Try


            If Not agt.ValidaAccesso(20, 20020303, Usr, "PRESIONO BUSCAR PAGOS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CB_Cliente.Checked Then

                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT cliente", TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar dígito verificador", TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                Dim FC As New FuncionesGenerales.FComunes

                If Txt_Dig_Cli.Text.Trim.ToUpper <> FC.Vrut(CLng(Txt_Rut_Cli.Text)).ToUpper Then
                    Msj.Mensaje(Me.Page, Caption, "Dígito incorrecto", TipoDeMensaje._Informacion)
                    Exit Sub
                End If

            End If

            If Not IsDate(Txt_FechaPago.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha del pago a buscar", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            BuscarPagos()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Rechazar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Rechazar.Click
        Try
            If Not agt.ValidaAccesso(20, 20040303, Usr, "PRESIONO RECHAZAR PAGO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If ValidaPagosMarcado() = False Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar pagos para rechazar", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Msj.Mensaje(Page, Caption, "¿Desea rechazar pago?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Rechazar.UniqueID)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Validar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Validar.Click

        Try

            If Not agt.ValidaAccesso(20, 20010303, Usr, "PRESIONO APROBAR PAGO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If ValidaPagosMarcado() = False Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar pagos para validar", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Msj.Mensaje(Page, Caption, "¿Desea aprobar pago?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Aprobar.UniqueID)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function ValidaPagosMarcado() As Boolean
        Try

            For I = 0 To GV_Pagos.Rows.Count - 1

                If CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

#End Region

#Region "EVENTOS DEL CLIENTE"


    Private Sub LimpiaCliente()
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
    End Sub

    Private Sub BloqueaCliente(ByVal Estado As Boolean)

        If Estado Then

            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True

            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Else

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True

            IB_AyudaCli.Enabled = True
        End If

    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged

        Try

            LimpiaCliente()

            If CB_Cliente.Checked Then
                IB_AyudaCli.Enabled = True
                BloqueaCliente(False)
                Txt_Rut_Cli.Focus()

            Else

                BloqueaCliente(True)

            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub

#End Region

#Region "SUB"

    Private Sub BuscarPagos()
        Try

            Dim RutCli_Desde As Long
            Dim RutCli_Hasta As Long

            Dim Fecha_Desde As String
            Dim Fecha_Hasta As String

            If CB_Cliente.Checked Then
                RutCli_Desde = Txt_Rut_Cli.Text
                RutCli_Hasta = Txt_Rut_Cli.Text
            Else
                RutCli_Desde = 0
                RutCli_Hasta = 999999999999
            End If

            If Txt_FechaPago.Text = "" Then
                Fecha_Desde = "01/01/1900"
                Fecha_Hasta = Date.Now.ToShortDateString
            Else
                Fecha_Desde = Txt_FechaPago.Text
                Fecha_Hasta = Txt_FechaPago.Text
            End If
            Dim Coll As Collection

            Coll = CG.IngresoDevuelveCabecera(RutCli_Desde, RutCli_Hasta, Fecha_Desde, Fecha_Hasta)

            GV_Cli_Deu.DataSource = Coll
            GV_Cli_Deu.DataBind()
            HF_Id_Ing.Value = "" 'FY 05-05-2012

            If Coll.Count > 0 Then

                For I = 0 To GV_Cli_Deu.Rows.Count - 1

                    GV_Cli_Deu.Rows(I).Cells(2).Text = Format(CDbl(GV_Cli_Deu.Rows(I).Cells(2).Text), Fmt.FCMSD) & "-" & RC.Vrut(CDbl(GV_Cli_Deu.Rows(I).Cells(2).Text))

                    'Select Case GV_Cli_Deu.Rows(I).Cells(4).Text.Trim()
                    '    Case "CLIENTE"
                    '        GV_Cli_Deu.HeaderRow.Cells(2).Text = "NIT Cliente"
                    '    Case "PAGADOR"
                    '        GV_Cli_Deu.HeaderRow.Cells(2).Text = "NIT Pagador"
                    'End Select

                Next

                BloqueaBusqueda(False)

            Else
                Msj.Mensaje(Me.Page, Caption, "No se encontraron pagos a la fecha ingresada", TipoDeMensaje._Informacion)
                LimpiaGridView() 'FY 03-05-2012
                HF_Id_Ing.Value = ""
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub BloqueaBusqueda(ByVal Estado As Boolean)

        If Estado Then

            Txt_FechaPago.ReadOnly = False
            Txt_FechaPago_CalendarExtender.Enabled = True
            Txt_FechaPago.CssClass = "clsMandatorio"

            '  CB_Cliente.Enabled = False
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"

        Else

            Txt_FechaPago.ReadOnly = True
            Txt_FechaPago_CalendarExtender.Enabled = False
            Txt_FechaPago.CssClass = "clsDisabled"

            CB_Cliente.Enabled = True
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True


            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"

        End If

    End Sub

    Private Sub LimpiaCriterio()

        Txt_FechaPago.Text = ""
        CB_Cliente.Checked = False
        'CB_Deudor.Checked = False

        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""

        'Txt_Rut_Deu.Text = ""
        'Txt_Dig_Deu.Text = ""
        'Txt_Nom_Deu.Text = ""

    End Sub

    Private Sub LimpiaGridView()
        Try

            GV_Cli_Deu.DataSource = Nothing
            GV_Cli_Deu.DataBind()

            GV_Pagos.DataSource = Nothing
            GV_Detalle.DataSource = Nothing

            GV_Pagos.DataBind()
            GV_Detalle.DataBind()

            Gv_Doctos.DataSource = Nothing
            Gv_Doctos.DataBind()

            GV_CxC.DataSource = Nothing
            GV_CxC.DataBind()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub MarcaPago()
        Try

            For I = 0 To GV_Cli_Deu.Rows.Count - 1

                'If I = (HF_Pos_Ing.Value - 1) Then
                If HF_Id_Ing.Value = GV_Cli_Deu.Rows(I).Cells(1).Text Then 'FY 03-05-2012
                    If (I Mod 2) = 0 Then
                        GV_Cli_Deu.Rows(I).CssClass = "selectable" 'GV_Cli_Deu.Rows(HF_Pos_Ing.Value - 1).CssClass = "clicktable"
                    Else
                        GV_Cli_Deu.Rows(I).CssClass = "selectableAlt"
                    End If
                Else

                    If (I Mod 2) = 0 Then
                        GV_Cli_Deu.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Cli_Deu.Rows(I).CssClass = "formatUltcellAlt" 'GV_Cli_Deu.Rows(I).CssClass = "formatable"
                    End If
                End If
            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub MarcaTipo(ByVal TIPO As Integer)
        Try

            For I = 0 To GV_Detalle.Rows.Count - 1

                ' If I = (HF_Pos_Doc_CxC.Value - 1) Then
                If TIPO = GV_Detalle.Rows(I).Cells(1).Text Then 'FY 03-05-2012
                    'GV_Detalle.Rows(HF_Pos_Doc_CxC.Value - 1).CssClass = "clicktable"
                    If (I Mod 2) = 0 Then
                        GV_Detalle.Rows(I).CssClass = "selectable"
                    Else
                        GV_Detalle.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_Detalle.Rows(I).CssClass = "formatUltcell" 'GV_Detalle.Rows(I).CssClass = "formatable"
                    Else
                        GV_Detalle.Rows(I).CssClass = "formatUltcellAlt"
                    End If

                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub MarcaDocto()
        Try

            For I = 0 To Gv_Doctos.Rows.Count - 1
                If I = (HF_Pos_Ing.Value - 1) Then
                    Gv_Doctos.Rows(HF_Pos_Ing.Value - 1).CssClass = "clicktable"
                Else
                    Gv_Doctos.Rows(I).CssClass = "formatable"
                End If
            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub MarcaCxC()
        Try

            For I = 0 To GV_Pagos.Rows.Count - 1

                If I = (HF_Pos_Ing.Value - 1) Then
                    GV_Pagos.Rows(HF_Pos_Ing.Value - 1).CssClass = "clicktable"
                Else
                    GV_Pagos.Rows(I).CssClass = "formatable"
                End If
            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Response.Expires = -1
                valida_cliente = ""
                Txt_FechaPago.Focus()
                Txt_FechaPago.Text = Format(Date.Now, Var.FMT_FECHA)
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            End If
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub GV_Cli_Deu_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Cli_Deu.RowDataBound
    End Sub

    Protected Sub GV_Detalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Detalle.RowDataBound
    End Sub

    Protected Sub GV_Pagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Pagos.RowDataBound
    End Sub

    Protected Sub LB_BuscarDetallePago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscarDetallePago.Click


    End Sub

    Protected Sub LB_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Buscar.Click
        Try

            BuscarPagos()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_BuscaDocCxC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDocCxC.Click
    End Sub

    Protected Sub LB_CargaPagos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_CargaPagos.Click
        Try

            BuscarPagos()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try
            Dim Cliente As cli_cls
            Cliente = clasecli.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Me.Txt_Dig_Cli.Text)
            If valida_cliente <> "" Then
                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                If IsNothing(Cliente) Then
                    Msj.Mensaje(Me, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                'Se valida que tipo de cliente
                If Cliente.id_P_0044 = 1 Then
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim & " " & Cliente.cli_ape_ptn.Trim & " " & Cliente.cli_ape_mtn.Trim
                Else
                    Txt_Raz_Soc.Text = Cliente.cli_rso.Trim
                End If

                BloqueaCliente(True)

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Not IsPostBack Then
                Modulo = "Control Dual"
                'Esto de abajo es para los skins
                Pagina = Page.AppRelativeVirtualPath
                CambioTema(Page)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_Aprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Aprobar.Click
        Try

            Dim Id_Dpo As Integer

            For I = 0 To GV_Pagos.Rows.Count - 1
                If CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                    Id_Dpo = GV_Pagos.Rows(I).Cells(2).Text
                    PGO.Pagos_ApruebaRechaza(HF_Id_Ing.Value, Id_Dpo, "V")
                End If
            Next

            Msj.Mensaje(Me.Page, Caption, "Pagos validados", TipoDeMensaje._Informacion)
            LimpiaGridView()
            BuscarPagos()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LB_Rechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Rechazar.Click
        Try

            Dim Id_Dpo As Integer
            For I = 0 To GV_Pagos.Rows.Count - 1
                If CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                    Id_Dpo = GV_Pagos.Rows(I).Cells(2).Text
                    PGO.Pagos_ApruebaRechaza(HF_Id_Ing.Value, Id_Dpo, "R")
                End If
            Next
            Msj.Mensaje(Me.Page, Caption, "Pagos rechazados", TipoDeMensaje._Informacion)
            LimpiaGridView()
            BuscarPagos()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'FY 03-05-2012

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim Coll_Dpo As Collection

        Try


            If btn.ToolTip <> "" Then

                HF_Id_Ing.Value = btn.ToolTip

                Coll_Dpo = CG.IngresoDevuelveModoDePago(HF_Id_Ing.Value)

                GV_Pagos.DataSource = Coll_Dpo
                GV_Pagos.DataBind()

                For I = 0 To GV_Pagos.Rows.Count - 1

                    GV_Pagos.Rows(I).Cells(3).Text = Format(CDbl(GV_Pagos.Rows(I).Cells(3).Text), Fmt.FCMSD)

                    Select Case Coll_Dpo.Item(I + 1).ing_vld_rcz
                        Case "V", "L", "C"
                            GV_Pagos.Rows(I).BackColor = Drawing.ColorTranslator.FromHtml("#CCFFCC")
                            CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Enabled = False

                            IB_Rechazar.Enabled = False
                            IB_Validar.Enabled = False

                        Case "R"
                            GV_Pagos.Rows(I).BackColor = Drawing.Color.LightCoral
                            CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Enabled = False

                            IB_Rechazar.Enabled = False
                            IB_Validar.Enabled = False
                            'Case "L"
                            '    GV_Pagos.Rows(I).BackColor = Drawing.ColorTranslator.FromHtml("#CCFFCC")
                            '    CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Enabled = False

                            '    IB_Rechazar.Enabled = False
                            '    IB_Validar.Enabled = False
                        Case Else
                            IB_Rechazar.Enabled = True
                            IB_Validar.Enabled = True
                    End Select

                Next

                Dim Coll As Collection

                Coll = CG.TipoDeIngresoDevuelve(HF_Id_Ing.Value)

                GV_Detalle.DataSource = Coll
                GV_Detalle.DataBind()

                For I = 0 To GV_Detalle.Rows.Count - 1
                    GV_Detalle.Rows(I).Cells(3).Text = Format(CDbl(GV_Detalle.Rows(I).Cells(3).Text), Fmt.FCMSD)
                Next


                MarcaPago()

                Gv_Doctos.DataSource = Nothing
                Gv_Doctos.DataBind()

                GV_CxC.DataSource = Nothing
                GV_CxC.DataBind()

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'FY 03-05-2012
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim Coll As Collection

        If btn.ToolTip <> "" Then
            'HF_Tipo.Value = 


            GV_CxC.DataSource = Nothing
            GV_CxC.DataBind()

            Gv_Doctos.DataSource = Nothing
            Gv_Doctos.DataBind()

            Select Case btn.ToolTip 'HF_Tipo.Value

                Case 1

                    Coll = CG.PagosDocumentosCxCDevuelveDetalle(HF_Id_Ing.Value, 1)

                    GV_CxC.DataSource = Coll
                    GV_CxC.DataBind()

                    For I = 0 To GV_CxC.Rows.Count - 1
                        GV_CxC.Rows(I).Cells(4).Text = Format(CDbl(GV_CxC.Rows(I).Cells(4).Text), Fmt.FCMSD)
                        GV_CxC.Rows(I).Cells(5).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    Next

                Case 2

                    Coll = CG.PagosDocumentosCxCDevuelveDetalle(HF_Id_Ing.Value, 2)

                    Gv_Doctos.DataSource = Coll
                    Gv_Doctos.DataBind()

                    For I = 0 To Gv_Doctos.Rows.Count - 1
                        Gv_Doctos.Rows(I).Cells(0).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(0).Text), Fmt.FCMSD) & "-" & RC.Vrut(CDbl(Gv_Doctos.Rows(I).Cells(0).Text))
                        Gv_Doctos.Rows(I).Cells(4).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(4).Text), Fmt.FCMSD)
                        Gv_Doctos.Rows(I).Cells(5).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    Next

            End Select


            MarcaTipo(btn.ToolTip)

        End If

    End Sub

#End Region

End Class

