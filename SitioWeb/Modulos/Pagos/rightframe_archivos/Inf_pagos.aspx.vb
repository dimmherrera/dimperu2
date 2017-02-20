Imports ClsSession.ClsSession
Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Partial Class Modulos_Pagos_rightframe_archivos_Inf_pagos
    Inherits System.Web.UI.Page
    Dim msj As New ClsMensaje
    Dim var As New FuncionesGenerales.Variables
    Dim cg As New ConsultasGenerales
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim sesion As New ClsSession.ClsSession
    Dim fmt As New FuncionesGenerales.Variables
    Dim fc As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.IsPostBack Then

                cg.EjecutivosDevuelve(Me.Dr_eje, CodEje, 15)
                Txt_Rut_Cli.Attributes.Add("Style", "Text-align:right")
                Txt_Rut_Deu.Attributes.Add("Style", "Text-align:right")
                txt_nro_dde.Attributes.Add("Style", "Text-align:right")

            End If

            'IB_Buscar.Attributes.Add("onClick", "WinOpen(2, 'InformePagosRealizados.aspx', 'inf', 1200,500, 50,100);")
            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
            Ib_ayuda_deu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeu.aspx','PopUpCliente',580,410,200,150);")

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try

            Dim cli_idc, ddr_ide, est1, est2 As String
            Dim suc1, suc2, eje1, eje2, tipo, tip_bus As Integer
            Dim fec_des, fec_has As String
            Dim nro_dde As String
            Dim nro_hta As String

            'Sucursal
            If Me.Ch_suc0.Checked Then
                suc1 = 0
                suc2 = 999
            Else
                suc1 = Sucursal
                suc2 = Sucursal
            End If

            'Ejecutivo
            If Me.Ch_cob.Checked Then
                eje1 = 0
                eje2 = 999
            Else
                If Me.Dr_eje.SelectedValue = 0 Then
                    msj.Mensaje(Me, "Atención", "Debe seleccionar un ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    eje1 = Me.Dr_eje.SelectedValue
                    eje2 = Me.Dr_eje.SelectedValue
                End If
            End If

            'Tipo de Pago
            If Ch_tip.Checked = False Then
                If Me.Dr_ti_pgo.SelectedValue = 0 Then
                    msj.Mensaje(Me, "Atención", "Debe seleccionar un tipo de pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    tip_bus = Dr_ti_pgo.SelectedValue
                End If

            Else
                tip_bus = 5
            End If

            ' Clientes
            If Me.Ch_cli.Checked Then

                If Me.Txt_Rut_Cli.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar NIT del Cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    cli_idc = Format(CLng(Me.Txt_Rut_Cli.Text), var.FMT_RUT)
                End If
            Else
                cli_idc = ""
            End If

            'Deudores

            If Me.Ch_deu.Checked Then

                If Me.Txt_Rut_Deu.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Debe ingresar NIT del Cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    ddr_ide = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
                End If

            Else

                ddr_ide = ""

            End If

            'Fechas de Pago

            If Me.txt_fec_pag_dde.Text = "" Then
                fec_des = "01/01/1900"
            End If

            If Me.txt_fec_pag_hta.Text = "" Then
                fec_has = "31/12/2999"
            End If


            If txt_fec_pag_dde.Text <> "" Then
                If Not IsDate(txt_fec_pag_dde.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha desde incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_pag_dde.Text = ""
                    Exit Sub
                End If
            End If

            If txt_fec_pag_hta.Text <> "" Then
                If Not IsDate(txt_fec_pag_hta.Text) Then
                    msj.Mensaje(Me, "Atención", "Fecha hasta incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_fec_pag_hta.Text = ""
                    Exit Sub
                End If
            End If

            If IsDate(Me.txt_fec_pag_dde.Text) And IsDate(Me.txt_fec_pag_hta.Text) Then

                If DateDiff(DateInterval.Day, CDate(Me.txt_fec_pag_dde.Text), CDate(Me.txt_fec_pag_hta.Text)) < 0 Then
                    msj.Mensaje(Me, "Atención", "Fecha hasta , debe ser mayor que fecha desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    fec_des = CDate(Me.txt_fec_pag_dde.Text)
                    fec_has = CDate(Me.txt_fec_pag_hta.Text)
                End If

            End If

            'Estado de Pago
            If Me.Ch_est.Checked = False Then

                If Me.Dr_est_doc.SelectedIndex = 0 Then
                    msj.Mensaje(Me, "Atención", "Debe seleccionar estado de pago", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    est1 = Me.Dr_est_doc.SelectedValue
                    est2 = Me.Dr_est_doc.SelectedValue
                End If

            Else
                est1 = "A"
                est2 = "Z"
            End If

            If cli_idc = "" And ddr_ide = "" Then
                tipo = 1
            ElseIf cli_idc = "" And ddr_ide <> "" Then
                tipo = 2
            ElseIf cli_idc <> "" And ddr_ide = "" Then
                tipo = 3
            ElseIf cli_idc <> "" And ddr_ide <> "" Then
                tipo = 4
            End If

            If txt_nro_dde.Text = "" Then
                nro_dde = "0"
                nro_hta = "Z"
            Else
                nro_dde = txt_nro_dde.Text
                nro_hta = txt_nro_dde.Text
            End If

            FuncionesGenerales.RutinasWeb.AbrePopup(Me, 1, "InformePagosRealizados.aspx?cli=" & cli_idc & "&ddr=" & ddr_ide & "&est1=" & est1 & "&est2=" & est2 & "&eje1=" & eje1 & "&eje2=" & eje2 & "&fec1=" & fec_des & "&fec2=" & fec_has & "&tipo=" & tipo & "&tipbus=" & tip_bus & "&suc1=" & suc1 & "&suc2=" & suc2 & "&nro_dde=" & nro_dde & "&nro_hta=" & nro_hta & " ", "ReportePagos", 1200, 900, 50, 100)

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub



    Protected Sub Ch_tip_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_tip.CheckedChanged
        Try

            If Me.Ch_tip.Checked Then
                Me.Dr_ti_pgo.Enabled = False
                Me.Dr_ti_pgo.CssClass = "clsDisabled"
                Me.Dr_ti_pgo.SelectedValue = 0
            Else
                Me.Dr_ti_pgo.Enabled = True
                Me.Dr_ti_pgo.CssClass = "clsMandatorio"
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ch_est_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_est.CheckedChanged
        Try
            If Me.Ch_est.Checked Then

                Me.Dr_est_doc.Enabled = False
                Me.Dr_est_doc.CssClass = "clsDisabled"
                Me.Dr_est_doc.SelectedValue = 0
            Else
                Me.Dr_est_doc.Enabled = True
                Me.Dr_est_doc.CssClass = "clsMandatorio"

            End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ch_deu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_deu.CheckedChanged
        Try
            If Me.Ch_deu.Checked Then
                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
                Me.Txt_Rut_Deu.ReadOnly = False
                Me.Txt_Dig_Deu.Text = ""
                Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
                Me.Txt_Dig_Deu.ReadOnly = False
                Me.Txt_Rso_Deu.Text = ""
                Me.Ib_ayuda_deu.Enabled = True
                Txt_Rut_Deu_MaskedEditExtender.Enabled = True
            Else
                Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                Me.Txt_Rut_Deu.ReadOnly = True
                Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                Me.Txt_Dig_Deu.ReadOnly = True
                Me.Ib_ayuda_deu.Enabled = False
                Txt_Rut_Deu_MaskedEditExtender.Enabled = False
                Txt_Rso_Deu.Text = ""
                Me.Txt_Rut_Deu.Text = ""
                Txt_Dig_Deu.Text = ""
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Ch_cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cli.CheckedChanged
        Try

            If Me.Ch_cli.Checked Then
                Me.Txt_Rut_Cli.Text = ""
                Me.Txt_Rut_Cli.CssClass = "clsMandatorio"
                Me.Txt_Rut_Cli.ReadOnly = False
                Me.Txt_Dig_Cli.Text = ""
                Me.Txt_Dig_Cli.CssClass = "clsMandatorio"
                Me.Txt_Dig_Cli.ReadOnly = False
                Me.Txt_Raz_Soc.Text = ""
                Me.IB_AyudaCli.Enabled = True
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Else
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Raz_Soc.Text = ""
                Me.Txt_Dig_Cli.Text = ""
                Me.Txt_Rut_Cli.Text = ""
                Me.IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Ch_cob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cob.CheckedChanged
        Try

            If Me.Ch_cob.Checked Then
                Me.Dr_eje.Enabled = False
                Me.Dr_eje.CssClass = "clsDisabled"
                Me.Dr_eje.SelectedValue = 0
            Else
                Me.Dr_eje.Enabled = True
                Me.Dr_eje.CssClass = "clsMandatorio"
                Me.Dr_eje.SelectedValue = 0
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub


    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged


        CargaCliente()
    End Sub

    Sub CargaCliente()
        Try
            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes
            Dim rut As String

            If Txt_Rut_Cli.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese NIT cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese dígito cliente", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            rut = CLng(Me.Txt_Rut_Cli.Text)
            cli = CLSCLI.ClientesDevuelve(rut, Me.Txt_Dig_Cli.Text.ToUpper)

            If sesion.valida_cliente <> "" Then
                msj.Mensaje(Me, "Atención", sesion.valida_cliente, 2)
            Else
                If IsNothing(cli) Then
                    msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Session("Cliente") = cli

                'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Raz_Soc.ReadOnly = True
                Me.Txt_Raz_Soc.CssClass = "clsDisabled"
                IB_AyudaCli.Enabled = False
                'Asigna Razón Social / Nombre a Campo Cliente
                Me.Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                Me.IB_Buscar.Enabled = True
            End If


        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        CargaDeudor()
    End Sub

    Sub CargaDeudor()
        Try

            Dim deu As deu_cls
            If Me.Txt_Rut_Deu.Text = "" Then
                msj.Mensaje(Me.Page, "Atencion", "Ingrese NIT deudor", 2)
                Exit Sub
            End If

            If Txt_Dig_Deu.Text = "" Then
                msj.Mensaje(Page, "Atención", "Ingrese dígito deudor", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Txt_Dig_Deu.Text.ToUpper <> fc.Vrut(Me.Txt_Rut_Deu.Text).ToUpper Then
                msj.Mensaje(Me.Page, "Atencion", "Dígito Incorrecto", 2)
                Exit Sub
            End If

            deu = cg.DeudorDevuelvePorRut(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT))
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
                Ib_ayuda_deu.Enabled = False
            Else
                msj.Mensaje(Page, "Atención", "Deudor no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Ch_suc0.Checked = False
        Ch_cob.Checked = True
        Ch_cli.Checked = False
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Txt_Raz_Soc.Text = ""
        Ch_deu.Checked = False
        IB_AyudaCli.Enabled = False
        Ib_ayuda_deu.Enabled = False
        Txt_Rut_Deu.CssClass = "clsDisabled"
        Txt_Rut_Deu.ReadOnly = True
        Txt_Rut_Deu.Text = ""
        Txt_Rut_Deu_MaskedEditExtender.Enabled = False
        Txt_Rso_Deu.Text = ""
        Ch_tip.Checked = True
        Dr_ti_pgo.Enabled = False
        Dr_ti_pgo.CssClass = "clsDisabled"
        Dr_ti_pgo.ClearSelection()
        Ch_est.Checked = True
        Dr_est_doc.ClearSelection()
        Dr_est_doc.Enabled = False
        Dr_est_doc.CssClass = "clsDisabled"
        txt_fec_pag_dde.Text = ""
        txt_fec_pag_hta.Text = ""
        Txt_Dig_Deu.Text = ""
        Txt_Dig_Deu.ReadOnly = True
        Txt_Dig_Deu.CssClass = "clsDisabled"

        Dr_eje.ClearSelection()
        Dr_eje.Enabled = False
        Dr_eje.CssClass = "clsDisabled"
        txt_nro_dde.Text = ""
    End Sub

End Class
