Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ClsAsigEjeCob
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.RutinasWeb
    Dim Caption As String = "Ejecutivos de Cobranza"
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim fmt As New FuncionesGenerales.Variables
    Dim fc As New FuncionesGenerales.FComunes
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Response.Expires = -1
            NroPaginacion = 0
            NroPaginacion_Eje = 0

            CargaForm()

            Me.IB_AsigAnt.Attributes.Add("onclick", "WinOpen(2,'Asg_Anterior.aspx','Popup', 820,480,200,200);")
            Me.IB_Reemplazo.Attributes.Add("onclick", "WinOpen(2,'Reemplazos.aspx','Popup',500,480,200,200);")

        End If
        SW = 99
        IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',580,410,200,150);")
        'IB_Next.Enabled = True
    End Sub

    Protected Sub GV_Ejecutivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Ejecutivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "CelEje(ctl00_ContentPlaceHolder1_GV_Ejecutivos, 'clicktable', 'formatable', 'selectable');")
        End If
    End Sub

    Protected Sub IB_Seleccionar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Seleccionar.Click
        Try

            Dim I As Integer
            Dim Flat As Boolean

            If CB_Sel.Checked Then
                Flat = False
            Else
                Flat = True
            End If

            CB_Sel.Checked = Flat

            For I = 0 To Me.GV_Deudores.Rows.Count - 1
                Dim CB As CheckBox = GV_Deudores.Rows(I).FindControl("CB_Seleccionar")
                CB.Checked = Flat
            Next

        Catch ex As Exception
            MsgBox(Me, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            If Not IsPostBack Then

                Modulo = "Cobranza"

                'Esto de abajo es para los skins
                Pagina = Page.AppRelativeVirtualPath

                CambioTema(Page)

            End If
        End If
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub CB_Eje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Eje.CheckedChanged

        Try

            If CB_Eje.Checked Then
                Dp_Ejecutivos.Enabled = True
                Dp_Ejecutivos.CssClass = "clsMandatorio"
            Else
                Dp_Ejecutivos.Enabled = False
                Dp_Ejecutivos.CssClass = "clsDisabled"
                Dp_Ejecutivos.ClearSelection()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cli.CheckedChanged


        Try

            If CB_Cli.Checked Then

                Txt_Rut_Deu.Enabled = True
                Txt_Dig_Deu.Enabled = True

                Txt_Rut_Deu.ReadOnly = False
                Txt_Dig_Deu.ReadOnly = False

                Txt_Rso_Deu.Text = ""

                Txt_Rut_Deu.CssClass = "clsMandatorio"
                Txt_Dig_Deu.CssClass = "clsMandatorio"
                IB_AyudaDeu.Enabled = True

                Txt_Rut_Deu.Focus()

            Else

                IB_AyudaDeu.Enabled = False

                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.ReadOnly = True

                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.CssClass = "clsDisabled"

                Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
                Txt_Rso_Deu.Text = ""

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        CargaDeudor()
    End Sub

    Protected Sub IB_Prev_Cob_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Cob.Click

        Try

            If NroPaginacion_Eje = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion_Eje >= 4 Then
                NroPaginacion_Eje -= 4
                CargaGrid()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Next_Cob_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Cob.Click

        Try

            If GV_Ejecutivos.Rows.Count < 4 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If GV_Ejecutivos.Rows.Count = 4 Then
                NroPaginacion_Eje = +4
                CargaGrid()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        NroPaginacion_Eje = 0

        CargaForm()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For I = 0 To GV_Ejecutivos.Rows.Count - 1

            If (btn.ToolTip = GV_Ejecutivos.Rows(I).Cells(1).Text) Then
                TxtCodEje.Value = btn.ToolTip
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

#Region "Procedimientos"

    Sub CargaDeudor()

        Dim deu As deu_cls


        If Me.Txt_Rut_Deu.Text = "" Then
            Msj.Mensaje(Me.Page, "Atencion", "Debe ingresar NIT", 2)
            Exit Sub
        End If

        If Me.Txt_Dig_Deu.Text.ToUpper <> fc.Vrut(Me.Txt_Rut_Deu.Text).ToUpper Then
            Msj.Mensaje(Me.Page, "Atencion", "Digito Incorrecto", 2)
            Exit Sub
        End If


        deu = CG.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT))



        Session("Deudor") = deu

        If Not IsNothing(deu) Then
            'Datos Deudor


            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True
            IB_AyudaDeu.Enabled = False
        End If
    End Sub

    Private Function BUSCA_DEUDORES_ASIGNADOS() As Boolean

        Try

            Dim FC As New FuncionesGenerales.FComunes

            If Me.CB_Cli.Checked Then

                If Trim(Txt_Dig_Deu.Text) <> "" Then
                    If Trim(Txt_Rut_Deu.Text) <> "" And Txt_Dig_Deu.Text <> "" Then

                        'If Txt_Dig_Deu.Text <> FC.Vrut(Txt_Rut_Deu.Text) Then
                        '    Msj.Mensaje(Me.Page, Caption, "Digito Incorrecto", TipoDeMensaje._Exclamacion)
                        '    Exit Function
                        'End If

                        Dim Deudor As deu_cls

                        Deudor = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)

                        If IsNothing(Deudor) Then
                            Msj.Mensaje(Me.Page, Caption, "Pagador no existe", TipoDeMensaje._Informacion)
                            Exit Function
                        Else
                            'If Deudor.id_eje_cod_cob = CodEje Then
                            '    Msj.Mensaje(Me.page,Caption, "Cliente asignado a otro ejecutivo", TipoDeMensaje._Informacion)
                            '    Exit Function
                            'End If
                        End If

                        Me.Txt_Rso_Deu.Text = Deudor.deu_rso

                    Else
                        Msj.Mensaje(Me.Page, Caption, "Debe Ingresar Pagador", TipoDeMensaje._Exclamacion)
                        Txt_Rut_Deu.Focus()
                        Exit Function
                    End If
                End If

            End If

            'Extrae Tipo_Consulta
            Dim COD_EJE As Integer = 0
            Dim rut_deudor1 As Long = 0
            Dim rut_deudor2 As Long = 0
            Dim TIPO_CONSULTA As Int16
            Dim CODEJE_DSD As Integer
            Dim CODEJE_HST As Integer

            If Not CB_Eje.Checked And CB_Cli.Checked Then
                rut_deudor1 = Txt_Rut_Deu.Text
                rut_deudor2 = Txt_Rut_Deu.Text
                TIPO_CONSULTA = 1
            End If

            If CB_Eje.Checked And CB_Cli.Checked Then
                COD_EJE = Dp_Ejecutivos.SelectedValue
                rut_deudor1 = Txt_Rut_Deu.Text
                rut_deudor2 = Txt_Rut_Deu.Text
                TIPO_CONSULTA = 2
            End If

            If CB_Eje.Checked And Not CB_Cli.Checked Then
                rut_deudor1 = 0
                rut_deudor2 = 9999999999999
                COD_EJE = Dp_Ejecutivos.SelectedValue
                TIPO_CONSULTA = 3
            End If

            If CB_Eje.Checked = True Then
                CODEJE_DSD = Dp_Ejecutivos.SelectedValue
                CODEJE_HST = Dp_Ejecutivos.SelectedValue
            Else
                CODEJE_DSD = 0
                CODEJE_HST = 999
            End If


            'EN EL CASO QUE NO SEA NINGUNO DE LOS DOS, DEBE TRAER TODOS LOS PAGADORES QUE NO TENGAN ASIGNADO UN COBRADOR
            If Not CB_Cli.Checked And Not CB_Eje.Checked Then
                rut_deudor1 = 0
                rut_deudor2 = 9999999999999

                CODEJE_DSD = 0
                CODEJE_HST = 999
            End If

            CG.DeudorReasignaEjecutivo(GV_Deudores, CODEJE_DSD, CODEJE_HST, rut_deudor1, rut_deudor2)

            If GV_Deudores.Rows.Count > 0 And GV_Deudores.Rows.Count = 7 Then
                IB_Next.Enabled = True
                IB_Prev.Enabled = True
            ElseIf GV_Deudores.Rows.Count > 0 And GV_Deudores.Rows.Count = 7 Then
                IB_Next.Enabled = False
                IB_Prev.Enabled = True
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Sub CargaForm()

        CG.EjecutivosDevuelve(Dp_Ejecutivos, CodEje, 29)
        '*************************************************************
        'Se agrega Sin Ejecutivo asignado
        If Dp_Ejecutivos.SelectedValue = 0 Then
            Dp_Ejecutivos.SelectedItem.Text = "SIN EJECUTIVO ASIGNADO"
        End If

        '*************************************************************
        CargaGrid()

        GV_Deudores.DataSource = New Collection
        GV_Deudores.DataBind()

        CB_Eje.Checked = False
        CB_Cli.Checked = False

        TxtCodEje.Value = ""
        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        Txt_Rso_Deu.Text = ""

        Dp_Ejecutivos.ClearSelection()

        Txt_Rut_Deu.Enabled = False
        Txt_Dig_Deu.Enabled = False
        Dp_Ejecutivos.Enabled = False

        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Dig_Deu.CssClass = "clsDisabled"
        Dp_Ejecutivos.CssClass = "clsDisabled"

        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")

        Dim I As Integer

        For I = 0 To Me.GV_Deudores.Rows.Count - 1
            Dim CB As CheckBox = GV_Deudores.Rows(I).FindControl("CB_Seleccionar")
            CB.Checked = False
        Next

        IB_Next.Enabled = False
        IB_Prev.Enabled = False


    End Sub

    Private Sub CargaGrid()

        Try

            GV_Ejecutivos.DataSource = CG.EjecutivosAsignarCobradoresDevuelve(Sucursal, 29, False, Nothing, 4)
            GV_Ejecutivos.DataBind()

            If GV_Ejecutivos.Rows.Count = 0 Then
                Msj.Mensaje(Page, Caption, "No se encontraron ejecutivos", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            'MsgBox(Me, MsgBoxStyle.Critical, "Error")
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)

        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Not agt.ValidaAccesso(20, 20010207, Usr, "PRESIONO GUARDAR ASIGNACION DE COBRADOR TELEFONICO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Valida Escojer Cobrador
            If Len(TxtCodEje.Value) = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe Escoger un Cobrador a Reasignar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de asignar los pagadores a otro cobrador?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Buscar.Click

        Try

            If Not agt.ValidaAccesso(20, 20020207, Usr, "PRESIONO GUARDAR ASIGNACION DE COBRADOR TELEFONICO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            NroPaginacion = 0

            BUSCA_DEUDORES_ASIGNADOS()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Protected Sub IB_AsigAnt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_AsigAnt.Click

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Deudores.Rows.Count < 7 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)

        End If
        NroPaginacion += 7
        NroPagina += 1

        BUSCA_DEUDORES_ASIGNADOS()
        IB_Prev.Enabled = True
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion > 1 Then

            IB_Next.Enabled = True

            NroPaginacion -= 7
            NroPagina -= 1
            'Lbl_Pagina.Text = "Pagina N°: " & CStr(NroPagina)

            BUSCA_DEUDORES_ASIGNADOS()

        Else
            Msj.Mensaje(Me.Page, Caption, "No existe una pagina anterior", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        Try

            Dim I As Integer
            Dim Arr As New Collection
            Dim asignacion As New Collection

            For I = 0 To Me.GV_Deudores.Rows.Count - 1

                Dim CB As CheckBox = GV_Deudores.Rows(I).FindControl("CB_Seleccionar")

                If CB.Checked Then

                    Dim Deu As New deu_cls

                    Deu.deu_ide = GV_Deudores.Rows(I).Cells(1).Text
                    Deu.id_eje_cod_cob = TxtCodEje.Value

                    Arr.Add(Deu)

                    Dim Ant As New has_cls
                    Ant.deu_ide = GV_Deudores.Rows(I).Cells(1).Text
                    Ant.id_eje_nue = TxtCodEje.Value
                    Ant.fec_asi_rea = Date.Now
                    If Dp_Ejecutivos.SelectedValue = 0 Then
                        Ant.id_eje_ant = Nothing
                    Else
                        Ant.id_eje_ant = Dp_Ejecutivos.SelectedValue
                    End If

                    Ant.id_eje = CodEje
                    asignacion.Add(Ant)
                End If

            Next

            Dim Eje As New ActualizacionesGenerales
            Dim IAA As New ClaseCobranza
            IAA.Asig_anteriorInserta(asignacion)
            If Eje.AsignaEjecutivoCobrador(Arr) Then
                Msj.Mensaje(Me.Page, Caption, "Cobrador Reasignado", TipoDeMensaje._Informacion)
            End If

            CargaForm()

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
