Imports FuncionesGenerales.FComunes
Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class Modulos_Cobranzas_rigthframe_archivos_Asg_Anterior
    Inherits System.Web.UI.Page

    Dim FC As New FuncionesGenerales.FComunes
    Dim fmt As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim RG As New FuncionesGenerales.FComunes
    Dim fm As New FuncionesGenerales.ClsLocateInfo
    Dim caption As String = "Gestión Anterior"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not Me.IsPostBack Then
            Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            NroPaginacion_AlertaDoctoxVencer = 0

        End If
        IB_Volver.Attributes.Add("onClick", "javascript:window.close();")
        IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeudor.aspx','PopUpDeudor',580,410,200,150);")

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Dim CG As New ConsultasGenerales
        Dim Deu As Object
        Try

        'Valida la Seleccion de Fechas de Asignación
            If CB_FechaAsignacion.Checked Then

                If Trim(TxtFechaAsig1.Text) = "" Then

                    Msj.Mensaje(Me, "Atención", "Debe ingresar fecha asignación desde", 2, Nothing, False)
                    TxtFechaAsig1.Focus()
                    Exit Sub
                End If

                If Not IsDate(TxtFechaAsig1.Text) Then
                    Msj.Mensaje(Me, "Atención", "Fecha asignación desde errónea", 2, Nothing, False)
                    TxtFechaAsig1.Text = ""
                    Exit Sub
                End If
                If Trim(TxtFechaAsig2.Text) = "" Then

                    Msj.Mensaje(Me, "Atención", "Debe ingresar fecha asignación hasta", 2, Nothing, False)
                    TxtFechaAsig2.Focus()
                    Exit Sub
                End If

                If Not IsDate(TxtFechaAsig2.Text) Then
                    Msj.Mensaje(Page, "Atención", "Fecha asignacion hasta errónea", 2, Nothing, False)
                    TxtFechaAsig2.Text = ""
                    Exit Sub
                End If


                If CDate(TxtFechaAsig1.Text) > CDate(TxtFechaAsig2.Text) Then

                    Msj.Mensaje(Me, "Atención", "Fecha asignación desde debe ser menor a fecha asignación hasta", 2, Nothing, False)

                    TxtFechaAsig1.Focus()
                    Exit Sub
                End If
            End If

            'Valida la Selección de Deudor
            If CB_Deudores.Checked Then

                If Trim(Txt_Dig_Deu.Text) <> "" Then

                    If Trim(Txt_Rut_Deu.Text) <> "" And Txt_Dig_Deu.Text <> "" Then

                        If Txt_Dig_Deu.Text <> FC.Vrut(Txt_Rut_Deu.Text) Then

                            Msj.Mensaje(Me, "Atención", "Digito Incorrecto", 2, Nothing, False)

                            Exit Sub
                        End If

                        Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)
                        Txt_Rso_Deu.Text = Deu.deu_rso

                    Else

                        Msj.Mensaje(Me, "Atención", "Debe Ingresar Pagador", 2, Nothing, False)

                        Txt_Rut_Deu.Focus()

                        Exit Sub

                    End If

                End If

            End If

            Dim has As Object
            'Valida La selección de al menos un criterio
            If Not CB_FechaAsignacion.Checked And Not CB_Deudores.Checked Then

                Msj.Mensaje(Me, "Atención", "Debe Seleccionar Fecha de Asignación o Deudor", 2, Nothing, False)
                CB_FechaAsignacion.Focus()

                Exit Sub
            End If

            'Trae las gestiones anteriores
            If Me.CB_Deudores.Checked = True And CB_FechaAsignacion.Checked = True Then



                has = CG.HistorialAsignacionDevuelve(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), CDate(Me.TxtFechaAsig1.Text).AddMinutes(1), CDate(Me.TxtFechaAsig2.Text).AddHours(23).AddMinutes(59).AddSeconds(59))

                If IsNothing(has) Then

                    Msj.Mensaje(Me, "Atención", "No se han encontrado datos según criterio", 2, Nothing, False)

                End If


                GridView1.DataSource = has
                GridView1.DataBind()
                For I = 0 To GridView1.Rows.Count - 1
                    GridView1.Rows(I).Cells(0).Text = Format(CLng(Me.GridView1.Rows(I).Cells(0).Text), fm.FCMSD) & "-" & RG.Vrut(CInt(Me.GridView1.Rows(I).Cells(0).Text))
                    GridView1.Rows(I).Cells(4).Text = Format(CDate(Me.GridView1.Rows(I).Cells(4).Text).ToShortDateString)
                Next
            ElseIf Me.CB_Deudores.Checked And Me.CB_FechaAsignacion.Checked = False Then



                has = CG.HistorialAsignacionDevuelve(Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT), CDate("01/01/1900"), CDate("31/12/2999"))

                If IsNothing(has) Then

                    Msj.Mensaje(Me, "Atención", "No se han encontrado datos según criterio", 2, Nothing, False)

                End If


                GridView1.DataSource = has
                GridView1.DataBind()
                For I = 0 To GridView1.Rows.Count - 1
                    GridView1.Rows(I).Cells(0).Text = Format(CLng(Me.GridView1.Rows(I).Cells(0).Text), fm.FCMSD) & "-" & RG.Vrut(CInt(Me.GridView1.Rows(I).Cells(0).Text))
                    GridView1.Rows(I).Cells(4).Text = Format(CDate(Me.GridView1.Rows(I).Cells(4).Text).ToShortDateString)
                Next
            ElseIf Me.CB_Deudores.Checked = False And Me.CB_FechaAsignacion.Checked = True Then

                has = CG.HistorialAsignacionDevuelve("000000000000", "999999999999", CDate(Me.TxtFechaAsig1.Text).AddMinutes(1), CDate(Me.TxtFechaAsig2.Text).AddHours(23).AddMinutes(59).AddSeconds(59))

                If IsNothing(has) Then

                    Msj.Mensaje(Me, "Atención", "No se han encontrado datos según criterio", 2, Nothing, False)

                End If


                GridView1.DataSource = has
                GridView1.DataBind()

                For I = 0 To GridView1.Rows.Count - 1
                    GridView1.Rows(I).Cells(0).Text = Format(CLng(Me.GridView1.Rows(I).Cells(0).Text), fm.FCMSD) & "-" & RG.Vrut(CInt(Me.GridView1.Rows(I).Cells(0).Text))
                    GridView1.Rows(I).Cells(4).Text = Format(CDate(Me.GridView1.Rows(I).Cells(4).Text).ToShortDateString)
                Next
            End If
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Dim CG As New ConsultasGenerales
        Dim Deu As Object

        Try
            If CB_Deudores.Checked Then
                If Trim(Txt_Dig_Deu.Text) <> "" Then
                    If Trim(Txt_Rut_Deu.Text) <> "" And Txt_Dig_Deu.Text <> "" Then
                        If UCase(Txt_Dig_Deu.Text) <> FC.Vrut(Txt_Rut_Deu.Text) Then

                            Msj.Mensaje(Me, "Atención", "Digito Incorrecto", 2, Nothing, False)

                            Exit Sub
                        End If
                        Deu = CG.DeudorDevuelvePorRut(Txt_Rut_Deu.Text)
                        Txt_Rso_Deu.Text = Deu.deu_rso
                        Me.Txt_Rut_Deu.ReadOnly = True
                        Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                        Me.Txt_Dig_Deu.ReadOnly = True
                        Me.Txt_Dig_Deu.CssClass = "clsDisabled"
                        Me.IB_AyudaDeu.Enabled = False

                    Else
                        Msj.Mensaje(Me, "Atención", "Debe Ingresar Deudor", 2, Nothing, False)
                        Txt_Rut_Deu.Focus()
                        Exit Sub
                    End If
                End If

            End If


        Catch ex As Exception
            CB_Deudores.Checked = True
            If CB_Deudores.Checked = True Then
                Txt_Rut_Deu.CssClass = "clsMandatorio"

            End If
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try

            'Criterios de busqueda
            CB_Deudores.Checked = False
            CB_FechaAsignacion.Checked = False

            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""
            Txt_Rso_Deu.Text = ""

            TxtFechaAsig1.Text = ""
            TxtFechaAsig2.Text = ""

            Txt_Rut_Deu.ReadOnly = False
            Txt_Dig_Deu.ReadOnly = False

            TxtFechaAsig1.ReadOnly = False
            TxtFechaAsig2.ReadOnly = False

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.CssClass = "clsDisabled"

            TxtFechaAsig1.CssClass = "clsDisabled"
            TxtFechaAsig2.CssClass = "clsDisabled"

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_Deudores_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Deudores.CheckedChanged

        If CB_Deudores.Checked Then

            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""

            Txt_Rut_Deu.ReadOnly = False
            Txt_Dig_Deu.ReadOnly = False
            IB_AyudaDeu.Enabled = True

            Txt_Rut_Deu.CssClass = "clsMandatorio"
            Txt_Dig_Deu.CssClass = "clsMandatorio"
            Txt_Rut_Deu.Focus()

        Else

            Txt_Rut_Deu.Text = ""
            Txt_Dig_Deu.Text = ""

            IB_AyudaDeu.Enabled = False

            Txt_Rut_Deu.ReadOnly = True
            Txt_Dig_Deu.ReadOnly = True

            Txt_Rut_Deu.CssClass = "clsDisabled"
            Txt_Dig_Deu.CssClass = "clsDisabled"

        End If

    End Sub

    Protected Sub CB_FechaAsignacion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_FechaAsignacion.CheckedChanged

        If CB_FechaAsignacion.Checked Then

            TxtFechaAsig1.Text = ""
            TxtFechaAsig2.Text = ""

            TxtFechaAsig1.ReadOnly = False
            TxtFechaAsig2.ReadOnly = False

            TxtFechaAsig1_CalendarExtender.Enabled = True
            TxtFechaAsig2_CalendarExtender.Enabled = True

            TxtFechaAsig1.CssClass = "clsMandatorio"
            TxtFechaAsig2.CssClass = "clsMandatorio"
            TxtFechaAsig1.Focus()

        Else

            TxtFechaAsig1.Text = ""
            TxtFechaAsig2.Text = ""

            TxtFechaAsig1_CalendarExtender.Enabled = False
            TxtFechaAsig2_CalendarExtender.Enabled = False

            TxtFechaAsig1.ReadOnly = True
            TxtFechaAsig2.ReadOnly = True

            TxtFechaAsig1.CssClass = "clsDisabled"
            TxtFechaAsig2.CssClass = "clsDisabled"

        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        Try
            If NroPaginacion_AlertaDoctoxVencer = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion_AlertaDoctoxVencer >= 5 Then
                NroPaginacion_AlertaDoctoxVencer -= 5
                IB_Buscar_Click(Me, e)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        Try

            If GridView1.Rows.Count < 5 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub

            End If

            If GridView1.Rows.Count <= 5 Then
                NroPaginacion_AlertaDoctoxVencer += 5
                IB_Buscar_Click(Me, e)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub


End Class
