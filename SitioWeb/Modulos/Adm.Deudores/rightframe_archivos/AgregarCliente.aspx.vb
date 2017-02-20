Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Deudor_Agregar_Cliente
    Inherits System.Web.UI.Page

    Dim Msj As New ClsMensaje
    Dim caption As String = "Ayuda Cliente"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim Sesion As New ClsSession.ClsSession
            NroPaginacion = 0
            Response.Expires = -1

            CargaGrilla()

        End If

    End Sub

    Private Sub CargaGrilla()

        Dim Sesion As New ClsSession.ClsSession
        Dim CLI As New ClaseClientes

        Dim Rut_Dsd As Long
        Dim Rut_Hst As Long
        Dim Tip_Dsd As Integer
        Dim Tip_Hst As Integer

        If Txt_Rut.Text <> "" Then
            Rut_Dsd = Txt_Rut.Text
            Rut_Hst = Txt_Rut.Text
        Else
            Rut_Dsd = 0
            Rut_Hst = 999999999999
        End If

        Tip_Dsd = 0
        Tip_Hst = 999999999
        
        'GV_Clientes.DataSource = Nothing

        CLI.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, 0, 999, Tip_Dsd, Tip_Hst, Txt_Rzo.Text.Trim, 2)

        If GV_Clientes.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron cliente según criterio o asignados al ejecutivo", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
        End If


    End Sub


    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        
        Dim rg As New FuncionesGenerales.FComunes
        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        For I = 0 To GV_Clientes.Rows.Count - 1

            If (boton_ver.ToolTip = GV_Clientes.Rows(I).Cells(0).Text) Then

                Txt_Rut.Text = rg.LimpiaRut(boton_ver.ToolTip)

                If (I Mod 2) = 0 Then
                    GV_Clientes.Rows(I).CssClass = "selectable"
                Else
                    GV_Clientes.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    GV_Clientes.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Clientes.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next


    End Sub


    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "celClickSinBtn(GV_Clientes, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Private Sub FindCargaGrilla()

        Dim Sesion As New ClsSession.ClsSession
        Dim CLI As New ClaseClientes

        CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, Me.Txt_Rut.Text, Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)


    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        GV_Clientes.PageIndex = 0
        FindCargaGrilla()
    End Sub

    Protected Sub IB_Aceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aceptar.Click

        Try

            Dim agt As New Perfiles.Cls_Principal

            If Not agt.ValidaAccesso(20, 20040201, Usr, "PRESIONO DETALLE DE UN DEUDOR") Then
                Msj.Mensaje(Me.Page, "Clientes", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim FW As New FuncionesGenerales.RutinasWeb
            Dim Sesion As New ClsSession.ClsSession
            Dim CLI As New ActualizacionesGenerales
            Dim DDR As New ddr_cls
            Dim Var As New FuncionesGenerales.Variables

            DDR.cli_idc = Replace(Txt_Rut.Text.Trim, ".", "")
            DDR.deu_ide = Sesion.RutDeu

            DDR.cli_idc = Format(CLng(DDR.cli_idc), Var.FMT_RUT)
            DDR.deu_ide = Format(CLng(DDR.deu_ide), Var.FMT_RUT)

            If CLI.ClientesDeudoresInserta(DDR) Then
                Msj.Mensaje(Me, "Clientes", "Cliente agregado para el Pagador", ClsMensaje.TipoDeMensaje._Informacion)
                'FW.CloseModal(Me, "ctl00$ContentPlaceHolder1$LnkBtnTraeDDR")
            Else
                Msj.Mensaje(Me, "Clientes", "Cliente ya esta asociado al Pagador", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

            'Dim rg As New FuncionesGenerales.FComunes
            'Dim boton_ver As ImageButton = CType(sender, ImageButton)
            'Response.Redirect("IngDeudor.aspx?Nro=" & rg.LimpiaRut(boton_ver.ToolTip), False)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            'If NroPaginacion = 0 Then
            '    Msj.Mensaje(Page, caption, "Ya ha llegado al comienzo de la lista", 2)
            'End If

            'If NroPaginacion > 8 Then
            '    NroPaginacion += 8
            '    CargaGrilla()
            'End If

            If NroPaginacion >= 8 Then
                NroPaginacion -= 8
                If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
                    FindCargaGrilla()
                Else
                    CargaGrilla()
                End If

            End If


        Catch ex As Exception
            Msj.Mensaje(Page, "Atencion", ex.Message, 2)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            'If GV_Clientes.Rows.Count < 8 Then
            '    Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
            '    Exit Sub
            'End If

            'If GV_Clientes.Rows.Count = 8 Then
            '    NroPaginacion += 8
            '    CargaGrilla()
            'End If


            NroPaginacion += 8
            If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
                FindCargaGrilla()
            Else
                CargaGrilla()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, "Atencion", ex.Message, 2)
        End Try
    End Sub
End Class
