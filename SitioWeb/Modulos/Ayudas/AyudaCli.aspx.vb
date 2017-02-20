Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos
Partial Class Modulos_Ayudas_AyudaCli
    Inherits System.Web.UI.Page

    Dim Msj As New ClsMensaje
    Dim session As New ClsSession.SesionOperaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Sesion As New ClsSession.ClsSession
            Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
            Response.Expires = -1
            NroPaginacionCli = 0
            CargaGrilla(0)

        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")
    End Sub

    Private Sub CargaGrilla(ByVal Rut As String)

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
            Rut_Hst = 9999999999999
        End If

        Tip_Dsd = 0
        Tip_Hst = 999999999

        '  CLI.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, Sesion.CodEje, Sesion.CodEje, Tip_Dsd, Tip_Hst, Txt_Rzo.Text.Trim, 1)
        CLI.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, 0, 999, Tip_Dsd, Tip_Hst, Txt_Rzo.Text.Trim, 1)

        If GV_Clientes.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron cliente según criterio o asignados al ejecutivo", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
        End If

    End Sub

    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)
            Dim digito As String = img.ToolTip.Trim().Substring(img.ToolTip.Trim().Length - 1, 1)

            If Request.QueryString("cartola") <> "" Then
                HiddenField1.Value = Request.QueryString("cartola")
                img.Attributes.Add("onClick", "javascript:AceptaClienteCartola('" & img.ToolTip & "');")
            ElseIf Request.QueryString("pago") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaClientePago('" & img.ToolTip & "');")
            ElseIf Request.QueryString("PopUp") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaCli('" & img.ToolTip & "');")
            ElseIf Request.QueryString("anulapago") <> "" Then
                If Request.QueryString("anulapago") = "1" Then
                    img.Attributes.Add("onClick", "javascript:AceptaClienteAnulacionPago('" & img.ToolTip & "', 1);")
                ElseIf Request.QueryString("anulapago") = "2" Then
                    img.Attributes.Add("onClick", "javascript:AceptaClienteAnulacionPago('" & img.ToolTip & "', 2);")
                End If
            ElseIf Request.QueryString("hojarecaudacion") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaClienteHoja('" & img.ToolTip & "');")
            Else
                img.Attributes.Add("onClick", "javascript:AceptaCliente('" & img.ToolTip & "', '" & digito & "');")
            End If

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            If Ayuda = False Then
                If SW = 99 Or Request.QueryString("SW") = 99 Then
                    HiddenField1.Value = Request.QueryString("valor")
                    e.Row.Attributes.Add("onClick", "funcionprueba(GV_Clientes, 'clicktable', 'formatable', 'selectable','" & Request.QueryString("valor") & "','" & Request.QueryString("valor2") & "','" & Request.QueryString("valor3") & "')  ")
                Else
                    e.Row.Attributes.Add("onClick", "celClickSinBtnCli(GV_Clientes, 'clicktable', 'formatable', 'selectable')")
                End If
            Else
                If SW = 99 Then
                    e.Row.Attributes.Add("onClick", "funcionprueba(GV_Clientes, 'clicktable', 'formatable', 'selectable')")
                Else
                    e.Row.Attributes.Add("onClick", "ClickContenedora(GV_Clientes, 'clicktable', 'formatable', 'selectable')")
                End If
            End If

        End If
       

    End Sub

    Private Sub FindCargaGrilla()

        Dim Sesion As New ClsSession.ClsSession
        Dim CLI As New ClaseClientes

        If Me.Txt_Rut.Text = "" Then
            CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, "", Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)
        Else
            CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, Replace(CLng(Me.Txt_Rut.Text), ".", ""), Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)
        End If


        If GV_Clientes.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron cliente segun criterio o asignados al ejecutivo", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
        End If


    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        'GV_Clientes.PageIndex = 0
        NroPaginacionCli = 0


        If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
            'FindCargaGrilla()
            CargaGrilla(Txt_Rut.Text)
        Else
            CargaGrilla(0)
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        'NroPaginacionCli += 8
        'If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
        '    FindCargaGrilla()
        'Else
        '    CargaGrilla(0)
        'End If
        'Dim CLI As New ClaseClientes


        'If NroPaginacionCli <= CLI.CantidadClientesActivos() Then
        '    NroPaginacionCli += 8
        '    CargaGrilla(Txt_Rut.Text)
        'End If

        'Se cambia tipo de desplazamiento de grilla mas optimo
        If GV_Clientes.Rows.Count < 8 Then
            Msj.Mensaje(Page, "Atencion", "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If GV_Clientes.Rows.Count = 8 Then
            NroPaginacionCli += 8
            CargaGrilla(Txt_Rut.Text)
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        'Dim CLI As New ClaseClientes
        'NroPaginacionCli -= 8
        'If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
        '    FindCargaGrilla()
        'Else
        '    CargaGrilla(0)
        'End If

        'If NroPaginacionCli >= 8 Then
        '    'If NroPaginacionCli >= CLI.CantidadClientesActivos() Then
        '    NroPaginacionCli -= 8
        '    CargaGrilla(Txt_Rut.Text)
        'End If

        'Se cambia tipo de desplazamiento de grilla mas optimo
        If NroPaginacionCli = 0 Then
            Msj.Mensaje(Me, "Atencion", "Ya a llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacionCli >= 8 Then
            NroPaginacionCli -= 8
            CargaGrilla(Txt_Rut.Text)
        End If


    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
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
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub


    Protected Sub IB_Aceptar_Click(sender As Object, e As ImageClickEventArgs) Handles IB_Aceptar.Click
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


End Class
