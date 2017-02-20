Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes


Partial Class Clientes_pop_up_com_cli
    Inherits System.Web.UI.Page
#Region "Declaracion de variables para la clase"
    Private CG As New ConsultasGenerales
    Dim Msj As New ClsMensaje
    Dim Caption As String = "Mantención Comision"
    Dim Sesion As New ClsSession.ClsSession
    Dim Var As New FuncionesGenerales.Variables
    Private RG As New FuncionesGenerales.FComunes
    Dim CLSCOM As New ActualizacionesGenerales
    Dim CLSQRY As New ConsultasGenerales
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Expires = -1
        If Not Page.IsPostBack Then

            Me.Txt_Rut.Text = Request.QueryString("rut")
            Me.Txt_Dig.Text = RG.Vrut(Me.Txt_Rut.Text)
            CargaDatosCliente()
            carga_drop()
            RetornaComision()
        End If
        Txt_Rut.Attributes.Add("Style", "Text-Align:right")
        txt_com_f.Attributes.Add("Style", "Text-Align:right")
        txt_maxima.Attributes.Add("Style", "Text-Align:right")
        txt_minima.Attributes.Add("Style", "Text-Align:right")
        txt_comision.Attributes.Add("Style", "Text-Align:right")

        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim ClsCli As New ClaseClientes
        Dim RG As New FuncionesGenerales.FComunes
        Dim Sesion As New ClsSession.ClsSession
        Dim Cli As cli_cls

        Try

            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut.Text, ".", ""), Me.Txt_Dig.Text)

            If valida_cliente <> "" Then

                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                Exit Function
            Else
                If IsNothing(Cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Function
                End If

                Me.Txt_Raz_Soc.Text = Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn

                Return True
            End If
        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Sub RetornaComision()
        Dim CDC As New CDC_cls
        Dim coll As New Collection
        Try

            coll = CLSQRY.BuscaComisionCli(Request.QueryString("rut"))
            If Not IsNothing(coll) Then
                If coll.Count > 0 Then


                    'Comision por documento

                    If coll.Item(1).id_P_0023_com = 2 Then

                        txt_minima.Text = Format(CDbl(coll.Item(1).cli_com_min), fmt.FCMCD4)
                        txt_maxima.Text = Format(CDbl(coll.Item(1).cli_com_max), fmt.FCMCD4)

                        txt_minima_MaskedEditExtender.Mask = "999,999,999.9999"
                        txt_maxima_MaskedEditExtender.Mask = "999,999,999.9999"

                    ElseIf coll.Item(1).id_P_0023_com = 3 Or coll.Item(1).id_P_0023_com = 4 Then

                        txt_minima.Text = Format(CDbl(coll.Item(1).cli_com_min), fmt.FCMCD)
                        txt_maxima.Text = Format(CDbl(coll.Item(1).cli_com_max), fmt.FCMCD)

                        txt_minima_MaskedEditExtender.Mask = "999,999,999.99"
                        txt_maxima_MaskedEditExtender.Mask = "999,999,999.99"

                    ElseIf coll.Item(1).id_P_0023_com = 1 Then

                        txt_minima.Text = Format(CDbl(coll.Item(1).cli_com_min), fmt.FCMSD)
                        txt_maxima.Text = Format(CDbl(coll.Item(1).cli_com_max), fmt.FCMSD)
                        txt_minima_MaskedEditExtender.Mask = "999,999,999,999"
                        txt_maxima_MaskedEditExtender.Mask = "999,999,999,999"

                    End If
                    Dp_moneda.SelectedIndex = coll.Item(1).id_P_0023_com

                    If Me.Dp_moneda.SelectedValue <> 0 Then
                        Me.Ch_doc.Checked = True

                        Me.txt_comision.CssClass = "clsMandatorio"
                        Me.txt_minima.CssClass = "clsMandatorio"
                        Me.txt_maxima.CssClass = "clsMandatorio"
                        Me.txt_comision.ReadOnly = False
                        Me.txt_minima.ReadOnly = False
                        Me.txt_maxima.ReadOnly = False
                        Me.Dp_ComFla.CssClass = "clsMandatorio"

                    End If
                    'Comisión Flat

                    If coll.Item(1).id_P_0023_fla = 2 Then


                        txt_com_f.Text = Format(CDbl(coll.Item(1).cli_com_fla), fmt.FCMCD4)


                        txt_com_f_MaskedEditExtender.Mask = "999,999,999.9999"

                    ElseIf coll.Item(1).id_P_0023_fla = 3 Or coll.Item(1).id_P_0023_fla = 4 Then

                        txt_com_f.Text = Format(CDbl(coll.Item(1).cli_com_fla), fmt.FCMCD)

                        txt_com_f_MaskedEditExtender.Mask = "999,999,999.99"

                    ElseIf coll.Item(1).id_P_0023_fla = 1 Then

                        txt_com_f.Text = Format(CDbl(coll.Item(1).cli_com_fla), fmt.FCMSD)

                        txt_com_f_MaskedEditExtender.Mask = "999,999,999,999"


                    End If
                    Dp_ComFla.SelectedIndex = coll.Item(1).id_P_0023_fla

                    If Me.Dp_ComFla.SelectedValue <> 0 Then
                        Me.Ch_flat.Checked = True
                        Me.Dp_ComFla.Enabled = True
                        Me.Dp_ComFla.CssClass = "clsMandatorio"
                        Me.txt_com_f.CssClass = "clsMandatorio"
                        Me.txt_com_f.ReadOnly = False

                    End If

                    txt_comision.Text = coll.Item(1).cli_por_com

                End If


            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub


    Public Sub carga_drop()
        CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_moneda)
        CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_ComFla)
    End Sub


    Protected Sub Dp_moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_moneda.SelectedIndexChanged

        txt_minima.Text = 0
        txt_maxima.Text = 0

        If Me.txt_minima.Text = "" Then
            txt_minima.Text = 0
        End If

        If Me.txt_maxima.Text = "" Then
            txt_maxima.Text = 0
        End If

        'Comision por documento
        If Me.Dp_moneda.SelectedValue = 2 Then

            txt_minima.Text = Format(CDbl(txt_minima.Text), fmt.FCMCD4)
            txt_maxima.Text = Format(CDbl(txt_maxima.Text), fmt.FCMCD4)

            txt_minima_MaskedEditExtender.Mask = "999,999,999.9999"
            txt_maxima_MaskedEditExtender.Mask = "999,999,999.9999"

        ElseIf Me.Dp_moneda.SelectedValue = 3 Or Me.Dp_moneda.SelectedValue = 4 Then

            txt_minima.Text = Format(CDbl(txt_minima.Text), fmt.FCMCD)
            txt_maxima.Text = Format(CDbl(txt_maxima.Text), fmt.FCMCD)

            txt_minima_MaskedEditExtender.Mask = "999,999,999.99"
            txt_maxima_MaskedEditExtender.Mask = "999,999,999.99"

        ElseIf Me.Dp_moneda.SelectedValue = 1 Then

            txt_minima.Text = Format(CDbl(txt_minima.Text), fmt.FCMSD)
            txt_maxima.Text = Format(CDbl(txt_maxima.Text), fmt.FCMSD)
            txt_minima_MaskedEditExtender.Mask = "999,999,999,999"
            txt_maxima_MaskedEditExtender.Mask = "999,999,999,999"

        End If
        txt_minima.Focus()
    End Sub

    Protected Sub Dp_ComFla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_ComFla.SelectedIndexChanged

        txt_com_f.Text = 0
        If Me.txt_com_f.Text = "" Then
            Me.txt_com_f.Text = 0
        End If
        If Me.Dp_ComFla.SelectedValue = 2 Then


            txt_com_f.Text = Format(CDbl(txt_com_f.Text), fmt.FCMCD4)


            txt_com_f_MaskedEditExtender.Mask = "999,999,999.9999"

        ElseIf Me.Dp_ComFla.SelectedValue = 3 Or Me.Dp_moneda.SelectedValue = 4 Then

            txt_com_f.Text = Format(CDbl(txt_com_f.Text), fmt.FCMCD)

            txt_com_f_MaskedEditExtender.Mask = "999,999,999.99"

        ElseIf Me.Dp_ComFla.SelectedValue = 1 Then

            txt_com_f.Text = Format(CDbl(txt_com_f.Text), fmt.FCMSD)

            txt_com_f_MaskedEditExtender.Mask = "999,999,999,999"


        End If
        txt_com_f.Focus()
    End Sub


    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        If Me.txt_comision.Text = "" Then
            Me.txt_comision.Text = Format(0, "##.##")
        End If

        If Txt_Rut.Text = "" Then
            Msj.Mensaje(Me, "Atención", "Ingrese NIT.", ClsMensaje.TipoDeMensaje._Exclamacion)
            Txt_Rut.Focus()
            Exit Sub
        Else
            If Txt_Dig.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese digito.", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Dig.Focus()
                Exit Sub
            End If
        End If

        'If txt_comision.Text = "" Then
        '    Msj.Mensaje(Me, "Atención", "Ingrese comision. ", ClsMensaje.TipoDeMensaje._Exclamacion)
        '    txt_comision.Focus()
        '    Exit Sub
        'End If
        If Me.Ch_doc.Checked Then

            If Me.Dp_moneda.SelectedValue = 0 Then
                Msj.Mensaje(Me, "Atención", "Seleccione moneda. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_minima.Text = "" Or txt_minima.Text = "0" Then
                Msj.Mensaje(Me, "Atención", "Ingrese monto Mínimo. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_minima.Focus()
                Exit Sub
            End If

            If txt_maxima.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese monto Máximo. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_maxima.Focus()
                Exit Sub
            End If


            If txt_comision.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese porcentaje de comisión. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_comision.Focus()
                Exit Sub
            End If
            'Validar que Mínimo no sea mayor
            If Double.Parse(txt_minima.Text) >= Double.Parse(txt_maxima.Text) Then
                Msj.Mensaje(Me, "Atención", "Monto Mínimo no puede ser igual o mayor al monto Máximo ", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_maxima.Focus()
                Exit Sub
            End If

        End If

        If Me.Ch_flat.Checked Then
            If Me.Dp_ComFla.SelectedValue = 0 Then
                Msj.Mensaje(Me, "Atención", "Seleccione tipo de moneda Flat.", ClsMensaje.TipoDeMensaje._Exclamacion)
                Dp_ComFla.Focus()
                Exit Sub
            End If

            If txt_com_f.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese comision flat. ", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_com_f.Focus()
                Exit Sub
            End If

        End If

        Try

            Dim CDC As New cdc_cls

            CDC.cli_idc = Format(CLng(Txt_Rut.Text), Var.FMT_RUT)

            If Me.Dp_ComFla.SelectedValue = 0 Then
                CDC.id_P_0023_fla = Nothing
            Else
                CDC.id_P_0023_fla = Dp_ComFla.SelectedItem.Value
            End If

            If Me.Dp_moneda.SelectedValue = 0 Then

                CDC.id_P_0023_com = Nothing
            Else

                CDC.id_P_0023_com = Dp_moneda.SelectedItem.Value
            End If

            If Me.txt_com_f.Text = "" Then
                txt_com_f.Text = 0
            End If


            CDC.cli_com_fla = txt_com_f.Text
            txt_minima.Text = IIf(txt_minima.Text = "", 0, txt_minima.Text)
            txt_maxima.Text = IIf(txt_maxima.Text = "", 0, txt_maxima.Text)
            CDC.cli_com_min = txt_minima.Text
            CDC.cli_com_max = txt_maxima.Text
            If txt_comision.Text = "" Then
                CDC.cli_por_com = 0
            Else
                CDC.cli_por_com = CDec(txt_comision.Text)
            End If


            'Elimina Comisiones
            CLSCOM.EliminaComisionCli(Me.Txt_Rut.Text)

            If CLSCOM.ComisionInserta(CDC) Then
                Msj.Mensaje(Me, "Atención", "Comision guardada.", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        LimpiarCampos()
    End Sub

    Private Sub LimpiarCampos()
        Me.txt_com_f.Text = ""
        Me.txt_comision.Text = ""
        Me.txt_maxima.Text = ""
        Me.txt_minima.Text = ""
        Me.Dp_ComFla.SelectedIndex = -1
        Me.Dp_moneda.SelectedIndex = -1
    End Sub


    Protected Sub Ib_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Eliminar.Click
        Dim CDC As New CDC_cls

        If CLSCOM.EliminaComisionCli(Me.Txt_Rut.Text) Then
            Msj.Mensaje(Me, Caption, "Registro eliminado", TipoDeMensaje._Exclamacion)
            LimpiarCampos()
        End If

    End Sub

    Protected Sub Ch_doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_doc.CheckedChanged
        If Me.Ch_doc.Checked Then

            Me.txt_comision.CssClass = "clsMandatorio"
            Me.txt_minima.CssClass = "clsMandatorio"
            Me.txt_maxima.CssClass = "clsMandatorio"
            Me.txt_comision.ReadOnly = False
            Me.txt_minima.ReadOnly = False
            Me.txt_maxima.ReadOnly = False
            Me.Dp_moneda.CssClass = "clsMandatorio"
            Me.Dp_moneda.Enabled = True
        Else

            Me.txt_comision.CssClass = "clsDisabled"
            Me.txt_minima.CssClass = "clsDisabled"
            Me.txt_maxima.CssClass = "clsDisabled"
            Me.Dp_moneda.CssClass = "clsDisabled"
            Me.txt_comision.ReadOnly = True
            Me.txt_minima.ReadOnly = True
            Me.txt_maxima.ReadOnly = True
            Me.Dp_moneda.Enabled = False
            Me.txt_minima.Text = ""
            Me.txt_maxima.Text = ""
            Me.txt_comision.Text = ""
            Me.Dp_moneda.SelectedValue = 0

        End If
    End Sub

    Protected Sub Ch_flat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_flat.CheckedChanged
        If Me.Ch_flat.Checked Then


            Me.Dp_ComFla.Enabled = True
            Me.Dp_ComFla.CssClass = "clsMandatorio"
            Me.txt_com_f.CssClass = "clsMandatorio"
            Me.txt_com_f.ReadOnly = False


        Else

            Me.Dp_ComFla.Enabled = False
            Me.Dp_ComFla.CssClass = "clsDisabled"
            Me.txt_com_f.CssClass = "clsDisabled"
            Me.txt_com_f.ReadOnly = True
            Me.txt_com_f.Text = ""
            Me.Dp_ComFla.SelectedValue = 0


        End If
    End Sub
End Class
