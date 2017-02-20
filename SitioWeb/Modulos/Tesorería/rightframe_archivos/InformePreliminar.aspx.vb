Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports FuncionesGenerales.FComunes
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Tesorería_rightframe_archivos_Default
    Inherits System.Web.UI.Page
    Dim msj As New ClsMensaje
    Dim fc As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CG.EjecutivosDevuelve(Me.dp_ejecutivos, CodEje, 13)
        'Me.txt_fec_desde.Text = Date.Now.ToShortDateString
        'Me.txt_fec_hasta.Text = Date.Now.ToShortDateString
    End Sub
#End Region

#Region "Botonera"
    Protected Sub ib_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_limpiar.Click
        Me.dp_ejecutivos.SelectedValue = 0
        Me.txt_fec_desde.Text = Date.Now.ToShortDateString
        Me.txt_fec_hasta.Text = Date.Now.ToShortDateString
        ReportViewer1.Reset()
        ReportViewer1.Dispose()
    End Sub

    Protected Sub ib_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_buscar.Click

        'If Me.dp_ejecutivos.SelectedValue = 0 Then
        '    msj.Mensaje(Me, "Atención", "Debe seleccionar un ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion)
        '    Exit Sub
        'End If

        If Me.txt_fec_desde.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar una fecha de inicio para generar", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.txt_fec_hasta.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar una fecha de termino para generar", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Genera_reporte()
    End Sub

#End Region

#Region "Reporte"
    Public Sub Genera_reporte()
        Try
            Dim eje1 As Integer
            Dim eje2 As Integer
            Dim lr As New LocalReport
            ReportViewer1.LocalReport.DataSources.Clear()

            Dim ng As New DataSet_Cheques.sp_cc_neg_ingresos_prelimDataTable
            Dim get_neg As New DataSet_ChequesTableAdapters.sp_cc_neg_ingresos_prelimTableAdapter

            Dim mensaje As String
            mensaje = ""



            'If Me.dp_ejecutivos.SelectedIndex = -1 Then
            eje1 = 0
            eje2 = 9999
            'Else
            '    eje1 = dp_ejecutivos.SelectedValue
            '    eje2 = dp_ejecutivos.SelectedValue
            'End If


            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("IngresPreliminar.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Tesorería\Reportes\IngresPreliminar.rdlc"

            ng = get_neg.GetData(eje1, eje2, CDate(Me.txt_fec_desde.Text), CDate(Me.txt_fec_hasta.Text))

            If ng.Count = 0 Then
                mensaje = "No se encontraron datos para la fecha indicada"
            End If

            Dim dt As DataTable

            dt = ng

            Dim neg As New Microsoft.Reporting.WebForms.ReportDataSource
            neg = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Cheques_sp_cc_neg_ingresos_prelim", dt)

            ReportViewer1.LocalReport.DataSources.Add(neg)

            ReportViewer1.DataBind()

        Catch ex As Exception
        End Try

    End Sub
#End Region

End Class
