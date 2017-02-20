Imports CapaDatos
Imports System.Data
Imports System.IO

Partial Class gene_archivos

    Inherits System.Web.UI.Page

    Dim sesion As New ClsSession.ClsSession
    Dim msj As New ClsMensaje


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim i As Integer

            For i = 0 To RadioButtonList1.Items.Count - 1

                'If i = 0 Or i = 1 Or i = 2 Or i = 3 Or i = 4 Or i = 5 Or i = 6 Or i = 8 Or i = 9 Or i = 10 Or i = 11 Or i >= 12 Then
                '    RadioButtonList1.Items(i).Enabled = True
                'Else
                '    RadioButtonList1.Items(i).Enabled = False
                'End If

                If i <> 17 Then
                    RadioButtonList1.Items(i).Enabled = False
                Else
                    dp_mes.ClearSelection()
                    RadioButtonList1.Items(i).Selected = True
                    txt_ano.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Label.Visible = True
                    panel.Visible = True
                    dp_mes.Items.FindByValue(Date.Now.Month).Selected = True
                    txt_ano.Text = Date.Now.Year
                End If

            Next

        End If

        'If Me.RadioButtonList1.SelectedValue = 11 Or Me.RadioButtonList1.SelectedValue = 5 Or Me.RadioButtonList1.SelectedValue = 18 Then
        '    txt_ano.Attributes.Add("Style", "TEXT-ALIGN: right")
        '    Label.Visible = True
        '    panel.Visible = True
        'Else
        '    Label.Visible = False
        '    panel.Visible = False
        '    txt_ano.Text = ""
        '    dp_mes.SelectedValue = 0
        'End If

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            sesion.Modulo = "Mantencion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)

        End If
    End Sub

    Protected Sub Btn_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Guardar.Click

        Try

            Dim path As String = ""
            Dim CA As New ClaseArchivos
            Dim NombreArchivo As String = ""

            If Me.RadioButtonList1.SelectedValue = 1 Then
                NombreArchivo = "DMF6016" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)
                If Not CA.Archivo_Sinacofi(path) Then
                    Return
                End If
            ElseIf Me.RadioButtonList1.SelectedValue = 2 Then

                NombreArchivo = "D24 Mensual" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_D24_Nuevo(path, "D", Date.Now.AddMonths(-1)) Then
                    Return
                End If
            ElseIf Me.RadioButtonList1.SelectedValue = 3 Then

                NombreArchivo = "D24 Diario" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_D24_Nuevo(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 4 Then

                NombreArchivo = "Auditoria_cuadratura_diario" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_auditoria(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 5 Then

                If dp_mes.SelectedValue = 0 Then
                    msj.Mensaje(Me, "Atención", "Seleccione un mes", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If txt_ano.Text < 1900 Or txt_ano.Text > Now.Year Or txt_ano.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Año debe ser mayor a 1900 y menor o igual a año actual", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                NombreArchivo = "auditoria mensual" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo) '"archivo\Auditoria mensual.txt"

                If Not CA.Archivo_auditoria(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 6 Then
                NombreArchivo = "Sinacofi1" & ".txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_sinacofi1(path) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 9 Then
                NombreArchivo = "FF841020" & ".txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_ART8485(path) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 10 Then
                Dim fec As Date
                fec = Date.Now

                NombreArchivo = "FFRV1103" & ".txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_Riesgo_Varios(path, "A", fec) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 11 Then
                Dim fec As Date
                Dim mes As Int16

                If dp_mes.SelectedValue = 0 Then
                    msj.Mensaje(Me, "Atención", "Seleccione un mes", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                If txt_ano.Text < 1900 Or txt_ano.Text > Now.Year Or txt_ano.Text = "" Then
                    msj.Mensaje(Me, "Atención", "Año debe ser mayor a 1900 y menor o igual a año actual", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                mes = dp_mes.SelectedValue
                fec = Format(CDate("01" & "/" & mes & "/" & txt_ano.Text), "dd/MM/yyyy")

                NombreArchivo = "FFRV1103" & ".txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_Riesgo_Varios(path, "M", fec.ToShortDateString) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 12 Then

                NombreArchivo = "P14 Diario" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P14(path, "D", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 13 Then

                NombreArchivo = "P14" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P14(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 14 Then

                NombreArchivo = "P15 Diario" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P15(path, "D", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 15 Then

                NombreArchivo = "P15" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P15(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 16 Then

                NombreArchivo = "P16 Diario" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P16(path, "D", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 17 Then

                NombreArchivo = "P16" & Format(DateAdd("d", -1, CDate(Format(Now, "dd/MM/yyyy"))), "yyyyMMdd") & "1.txt"
                path = Server.MapPath("archivo\" & NombreArchivo)

                If Not CA.Archivo_P16(path, "M", Date.Now) Then
                    Return
                End If

            ElseIf Me.RadioButtonList1.SelectedValue = 18 Then '04-04-2013 jlagos historico de cliente mensual

                'If dp_mes.SelectedValue = 0 Then
                '    msj.Mensaje(Me, "Atención", "Seleccione un mes", ClsMensaje.TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                'If txt_ano.Text < 1900 Or txt_ano.Text > Now.Year Or txt_ano.Text = "" Then
                '    msj.Mensaje(Me, "Atención", "Año debe ser mayor a 1900 y menor o igual a año actual", ClsMensaje.TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                NombreArchivo = "Historico_Mensual_Al_" & Format(Now, "yyyyMMdd") & ".xls"

                Dim sb As StringBuilder = New StringBuilder()
                Dim sw As StringWriter = New StringWriter(sb)
                Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
                Dim pagina As Page = New Page
                Dim form = New HtmlForm

                GridView1.DataSource = CA.Archivo_Historico(dp_mes.SelectedValue, txt_ano.Text)
                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then

                    GridView1.EnableViewState = False
                    pagina.EnableEventValidation = False
                    pagina.DesignerInitialize()
                    pagina.Controls.Add(form)

                    form.Controls.Add(GridView1)
                    pagina.RenderControl(htw)
                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment;filename=" & NombreArchivo)
                    Response.Charset = "UTF-8"
                    Response.ContentEncoding = Encoding.Default
                    Response.Write(sb.ToString())
                    Response.End()

                Else
                    msj.Mensaje(Me, "Atención", "No existen operaciones otorgadas para el dia de hoy " & Date.Now.ToShortDateString(), ClsMensaje.TipoDeMensaje._Exclamacion)
                End If

                Return

            End If

            Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(path)

            If targetFile.Exists Then

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                Response.AddHeader("Content-Length", targetFile.Length.ToString)
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(targetFile.FullName)
                dp_mes.SelectedValue = 0
                txt_ano.Text = ""
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class
