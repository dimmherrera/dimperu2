Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class CalendarioDePago
    Inherits System.Web.UI.Page

    Private CG As New ConsultasGenerales
    Private AG As New ActualizacionesGenerales
    Private Var As New FuncionesGenerales.Variables
    Private rw As New FuncionesGenerales.RutinasWeb
    Private fc As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenaGrilla()
            CB_Dias.Enabled = False
            Txt_Ano.Text = Date.Now.Year
        End If

        Btn_Cerrar.Attributes.Add("onClick", "Javascript:window.close();")

    End Sub


    Protected Sub Btn_Nuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Limpia()
        Botonera(True)
        CB_Dias.Enabled = True
    End Sub

    Protected Sub Btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GRABAR_CALENDARIO()
        LlenaGrilla()
    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Limpiar.Click
        Limpia()
        Botonera(False)
    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ib As ImageButton = CType(sender, ImageButton)

        AG.DeudorCalendarioElimina(ib.ToolTip)
        LlenaGrilla()
        Limpia()

    End Sub


#Region "SUB & FUNCTION"

    Private Sub GRABAR_CALENDARIO()

        Try

            If ValidaCalendario() Then

                Dim cpg As cpg_cls
                Dim anos As Integer

                For anos = Date.Now.Year To Txt_Ano.Text

                    For meses = 1 To 12

                        For dias = 1 To fc.DevuelveUltimoDiaDelMes(meses, anos)

                            Dim fecha As Date = Format(dias, "00") & "-" & Format(meses, "00") & "-" & Txt_Ano.Text

                            For i = 0 To CB_Dias.Items.Count - 1
                                If (fecha.DayOfWeek = CB_Dias.Items(i).Value And CB_Dias.Items(i).Selected) Then
                                    cpg = New cpg_cls
                                    cpg.deu_ide = Format(RutDeu, Var.FMT_RUT)
                                    cpg.fec_cpg = fecha
                                    AG.DeudorCalendarioInserta(cpg)
                                End If
                            Next
                        Next
                    Next
                Next

                LlenaGrilla()
                Limpia()

                Botonera(False)

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Limpia()

        'If bloquea Then
        '    Txt_fecha.Text = ""
        '    Txt_fecha.CssClass = "clsDisabled"
        '    Txt_fecha.ReadOnly = True
        'Else
        '    Txt_fecha.Text = ""
        '    Txt_fecha.CssClass = "clsMandatorio"
        '    Txt_fecha.ReadOnly = False
        'End If
        Txt_Ano.Text = Date.Now.Year

        For I = 0 To CB_Dias.Items.Count - 1
            CB_Dias.Items(I).Enabled = True
            CB_Dias.Items(I).Selected = False
        Next


    End Sub

    Private Sub Botonera(ByVal bloquea As Boolean)

        If Not bloquea Then
            Btn_Guardar.Enabled = False
            Btn_Nuevo.Enabled = True
        Else
            Btn_Guardar.Enabled = True
            Btn_Nuevo.Enabled = False
        End If

    End Sub


    Private Sub LlenaGrilla()

        GV_Fechas.DataSource = CG.DevuelveCalendarioPagoDeudor(RutDeu)
        GV_Fechas.DataBind()

    End Sub

    Private Function ValidaCalendario() As Boolean

        Dim clscom As New ClaseComercial
        Dim FEC_DES As String
        Dim FEC_HAS As String


        'If (Txt_fecha.Text = "") Then
        '    rw.Mensaje(Page, "Debe ingresar una fecha")
        '    Txt_fecha.Focus()
        '    Return False
        'End If

        'If Not IsDate(Txt_fecha.Text) Then
        '    rw.Mensaje(Page, "Ingrese fecha correcta")
        '    Txt_fecha.Focus()
        '    Return False
        'End If

        'FEC_DES = Txt_fecha.Text
        'FEC_HAS = clscom.DiaHabilDevuelve(FEC_DES)

        'If (FEC_DES <> FEC_HAS) Then
        '    rw.Mensaje(Page, "La fecha no es habil")
        '    Txt_fecha.Focus()
        '    Return False
        'End If

        'For i = 0 To GV_Fechas.Rows.Count - 1
        '    If Txt_fecha.Text = GV_Fechas.Rows(i).Cells(1).Text Then
        '        rw.Mensaje(Page, "La fecha ya se encuentra ingresada")
        '        Txt_fecha.Focus()
        '        Return False
        '    End If
        'Next

        Return True

    End Function

#End Region



End Class
