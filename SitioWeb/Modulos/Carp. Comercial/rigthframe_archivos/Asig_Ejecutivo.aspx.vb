Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class ClsAsigEje
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private Ejec As New ConsultasGenerales
    Private RG As New FuncionesGenerales.RutinasWeb
    Private Var As New FuncionesGenerales.Variables
    Private Sesion As New ClsSession.ClsSession
    Private Caption As String = "Asignación de Ejecutivos"
    Private Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim FC As New FuncionesGenerales.FComunes

#End Region

#Region "Eventos"
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1
            Ejec.EjecutivosDevuelve(Dp_Ejecutivos, CodEje, 15)
            NroPaginacion_Eje = 0
            NroPaginacion = 0
            CargaForm()
            CargaGrid()

        End If

        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")


    End Sub

    Private Sub CargaForm()

        'TIPO_EJECUTIVO = 15

        'CargaGrid()

        GV_Clientes.DataSource = New Collection
        GV_Clientes.DataBind()

        CB_Eje.Checked = False
        CB_Cli.Checked = False

        TxtCodEje.Value = ""
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""

        Dp_Ejecutivos.ClearSelection()

        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Dp_Ejecutivos.Enabled = False

        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Dp_Ejecutivos.CssClass = "clsDisabled"

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

        Dim I As Integer

        For I = 0 To Me.GV_Clientes.Rows.Count - 1
            Dim CB As CheckBox = GV_Clientes.Rows(I).FindControl("CB_Seleccionar")
            CB.Checked = False
        Next

    End Sub

    Private Sub CargaGrid()

        Try

            Coll_Eje = New Collection

            Coll_Eje = Ejec.EjecutivosAsignarComercialesDevuelve(Var.CODIGO_SUCURSAL, 15)

            GV_Ejecutivos.DataSource = Coll_Eje
            GV_Ejecutivos.DataBind()

            For I = 0 To GV_Ejecutivos.Rows.Count - 1
                GV_Ejecutivos.Rows(I).Cells(3).Text = Format(Val(GV_Ejecutivos.Rows(I).Cells(3).Text), "##,###,##0")
                GV_Ejecutivos.Rows(I).Cells(4).Text = Format(Val(GV_Ejecutivos.Rows(I).Cells(4).Text), "##,###,##0")
            Next

        Catch ex As Exception
            'MsgBox(Me, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20020204, Usr, "PRESIONO GUARDAR ASIGNACION DE EJECUTIVOS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Valida Escojer Cobrador
            If Len(TxtCodEje.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe Escoger Ejecutivo de Cuentas a Reasignar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de querer guardar?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Not agt.ValidaAccesso(20, 20010204, Usr, "PRESIONO BUSCAR EJECUTIVOS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            NroPagina = 0
            NroPaginacion = 0
            BUSCA_CLIENTES_ASIGNADOS()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Function BUSCA_CLIENTES_ASIGNADOS() As Boolean
        Try

            'VALIDA EN EL CASO DE HABER SELECCIONADO POR EJECUTIVO
            If Me.CB_Eje.Checked Then
                If Dp_Ejecutivos.SelectedValue = 0 Then
                    Msj.Mensaje(Me.Page, Caption, "Debe Seleccionar Ejecutivo de Cuentas", TipoDeMensaje._Exclamacion)
                    Dp_Ejecutivos.Focus()
                    Exit Function
                End If
            End If

            'VALIDA EN EL CASO DE HABER SELECCIONADO POR CLIENTE
            If Me.CB_Cli.Checked Then

                Dim Cliente As cli_cls
                Dim CLSCLI As New ClaseClientes

                Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

                If Sesion.valida_cliente <> "" Then
                    Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                    Exit Function
                End If

                Me.Txt_Raz_Soc.Text = Cliente.cli_rso

                
            End If
           


            'EN EL CASO QUE NO SEA NINGUNO DE LOS DOS
            If Not CB_Cli.Checked And Not CB_Eje.Checked Then
                Msj.Mensaje(Me.Page, Caption, "Debe Seleccionar Ejecutivo de Cuentas o Cliente", TipoDeMensaje._Exclamacion)
                CB_Eje.Focus()
                Exit Function
            End If

            'Extrae Tipo_Consulta
            'Dim COD_EJE As Integer = 0
            Dim rut_cliente1 As Long = 0
            Dim rut_cliente2 As Long = 0
            Dim TIPO_CONSULTA As Int16
            Dim Cod_EjeDsd As Integer
            Dim Cod_EjeHst As Integer


            If Not CB_Eje.Checked And CB_Cli.Checked Then
                RUT_CLIENTE1 = Txt_Rut_Cli.Text
                RUT_CLIENTE2 = Txt_Rut_Cli.Text
                TIPO_CONSULTA = 1
                'En caso que no se Seleccione ningun Ejecutivo Se cargara variable de ejecutivo que esta conectado.
                'COD_EJE = CodEje
            End If

            If CB_Eje.Checked And CB_Cli.Checked Then
                'COD_EJE = Dp_Ejecutivos.SelectedValue
                RUT_CLIENTE1 = Txt_Rut_Cli.Text
                RUT_CLIENTE2 = Txt_Rut_Cli.Text
                TIPO_CONSULTA = 2
            End If

            If CB_Eje.Checked And Not CB_Cli.Checked Then
                RUT_CLIENTE1 = 0
                rut_cliente2 = 9999999999999
                'COD_EJE = Dp_Ejecutivos.SelectedValue
                TIPO_CONSULTA = 3
            End If

            'Se agrega rango amplio en caso que no se seleccione ejecutivo
            If CB_Eje.Checked = True Then
                Cod_EjeDsd = Dp_Ejecutivos.SelectedValue
                Cod_EjeHst = Dp_Ejecutivos.SelectedValue
            Else
                Cod_EjeDsd = 0
                Cod_EjeHst = 999
            End If

            'Ejec.ClienteReasignaEjecutivo(GV_Clientes, COD_EJE, RUT_CLIENTE1, RUT_CLIENTE2)

            Ejec.ClienteReasignaEjecutivo(GV_Clientes, Cod_EjeDsd, Cod_EjeHst, RUT_CLIENTE1, RUT_CLIENTE2)



            If GV_Clientes.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No se existen cliente para el ejecutivo seleccionado.", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Protected Sub GV_Ejecutivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Ejecutivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Ejecutivos, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Ejecutivos, 'formatable')")
            'e.Row.Attributes.Add("onClick", "CelEje(ctl00_ContentPlaceHolder1_GV_Ejecutivos, 'clicktable', 'formatable', 'selectable');")
        End If
    End Sub

    Protected Sub IB_Seleccionar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim I As Integer
            Dim Flat As Boolean

            If CB_Sel.Checked Then
                Flat = False
            Else
                Flat = True
            End If

            CB_Sel.Checked = Flat

            For I = 0 To Me.GV_Clientes.Rows.Count - 1
                Dim CB As CheckBox = GV_Clientes.Rows(I).FindControl("CB_Seleccionar")
                CB.Checked = Flat
            Next

        Catch ex As Exception

            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim I As Integer
            Dim Arr As New Collection
            Dim FC As New FuncionesGenerales.FComunes
            For I = 0 To Me.GV_Clientes.Rows.Count - 1

                Dim CB As CheckBox = GV_Clientes.Rows(I).FindControl("CB_Seleccionar")

                If CB.Checked Then

                    Dim CLI As New cli_cls

                    CLI.cli_idc = Format(CLng(FC.LimpiaRut(GV_Clientes.Rows(I).Cells(1).Text)), Var.FMT_RUT)
                    CLI.id_eje_cod_eje = TxtCodEje.Value
                    CLI.cli_fec_act_eje = Date.Now

                    Arr.Add(CLI)

                End If

            Next

            Dim Eje As New ActualizacionesGenerales

            If Eje.AsignaEjecutivoDeCuentas(Arr) Then
                Msj.Mensaje(Me.Page, Caption, "Ejecutivo Reasignado", TipoDeMensaje._Informacion)
            End If

            CargaForm()
            CargaGrid()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click


        NroPaginacion += 10
        NroPagina += 1
        'Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

        BUSCA_CLIENTES_ASIGNADOS()

        If GV_Clientes.Rows.Count < 10 Then
            Msj.Mensaje(Page, Caption, "Ya esta en la ultima pagina", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion > 1 Then

            IB_Next.Enabled = True

            NroPaginacion -= 10
            NroPagina -= 1
            'Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

            BUSCA_CLIENTES_ASIGNADOS()

        Else
            Msj.Mensaje(Me.Page, Caption, "No existe una pagina anterior", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub CB_Eje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Eje.CheckedChanged

        Try

            If CB_Eje.Checked Then

                Dp_Ejecutivos.Enabled = True
                Dp_Ejecutivos.CssClass = "clsMandatorio"

                Dp_Ejecutivos.Focus()

            Else

                Dp_Ejecutivos.Enabled = False
                Dp_Ejecutivos.CssClass = "clsDisabled"

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cli.CheckedChanged
        Try
            If CB_Cli.Checked Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli.Focus()
                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            If Me.CB_Cli.Checked Then

                If Trim(Txt_Dig_Cli.Text) <> "" Then
                    If Trim(Txt_Rut_Cli.Text) <> "" And Txt_Dig_Cli.Text <> "" Then

                        Dim Cliente As cli_cls
                        Dim CLSCLI As New ClaseClientes

                        Cliente = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)


                        If Sesion.valida_cliente <> "" Then
                            Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If


                        If Cliente.id_P_0044 <> 1 Then
                            Me.Txt_Raz_Soc.Text = Cliente.cli_rso
                        Else
                            Me.Txt_Raz_Soc.Text = Trim(Cliente.cli_rso) & " " & Trim(Cliente.cli_ape_ptn) & " " & Trim(Cliente.cli_ape_mtn)

                        End If

                        IB_AyudaCli.Enabled = False
                        Txt_Rut_Cli.ReadOnly = True
                        Txt_Dig_Cli.ReadOnly = True
                        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                        Txt_Rut_Cli.CssClass = "clsDisabled"
                        Txt_Dig_Cli.CssClass = "clsDisabled"

                    Else
                        Msj.Mensaje(Me.Page, Caption, "Debe Ingresar Cliente", TipoDeMensaje._Exclamacion)
                        Txt_Rut_Cli.Focus()
                        Exit Sub
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        CargaForm()
        CargaGrid()
    End Sub

    Protected Sub IB_Prev_GV_Ejecutivos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_GV_Ejecutivos.Click
        If NroPaginacion_Eje = 0 Then
            Msj.Mensaje(Page, Caption, "No existe una pagina anterior", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If NroPaginacion_Eje >= 5 Then
            NroPaginacion_Eje -= 5
            CargaGrid()
        End If

    End Sub

    Protected Sub IB_Next_GV_Ejecutivos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_GV_Ejecutivos.Click

        If GV_Ejecutivos.Rows.Count = 5 Then
            NroPaginacion_Eje += 5
            CargaGrid()
        End If

        If GV_Ejecutivos.Rows.Count < 5 Then
            Msj.Mensaje(Page, Caption, "Ya esta en la ultima pagina", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        TxtCodEje.Value = btn.ToolTip

        For I = 0 To GV_Ejecutivos.Rows.Count - 1

            If (TxtCodEje.Value = GV_Ejecutivos.Rows(I).Cells(1).Text) Then
                If (I Mod 2) = 0 Then
                    GV_Ejecutivos.Rows(I).CssClass = "selectable"
                Else
                    GV_Ejecutivos.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    GV_Ejecutivos.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Ejecutivos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

        
    End Sub

#End Region

End Class
