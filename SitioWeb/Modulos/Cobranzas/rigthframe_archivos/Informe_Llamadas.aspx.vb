Imports Microsoft.Reporting
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Data

Partial Class Informe_Llamadas
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim CG As New ConsultasGenerales
    Dim FW As New FuncionesGenerales.RutinasWeb

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaDrop()
                RB_Mes.Checked = True
                Drop_mes.SelectedValue = Date.Now.Month
                txt_Año.Text = Date.Now.Year
            End If

            IB_Volver.Attributes.Add("onClick", "javascript:window.close();")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Mes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Mes.CheckedChanged
        Try
            If RB_Mes.Checked = True Then

                Drop_mes.Enabled = True
                Drop_mes.CssClass = "clsMandatorio"
                RB_Año.Checked = False
                Drop_mes.SelectedValue = Date.Now.Month

                ReportViewer1.Reset()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Año_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Año.CheckedChanged
        Try
            If RB_Año.Checked = True Then
                Drop_mes.Enabled = False
                Drop_mes.CssClass = "clsDisabled"
                Drop_mes.ClearSelection()
                RB_Mes.Checked = False

                ReportViewer1.Reset()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

        Try

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

        Try

            GeneraInforme()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Function o Sub"
    Public Sub CargaDrop()
        CG.ParametrosDevuelve(85, True, Drop_mes)
        ' Drop_mes.SelectedIndex = Format(Now, "mn") - 1
        'CX_MES.ListIndex = Format(Now, "mm") - 1
    End Sub

    Public Sub GeneraInforme()

        Try

            Dim LlaAño As New DataSet_Llamadas.sp_Reporte_Devuelve_control_cobrDataTable
            Dim año As New DataSet_LlamadasTableAdapters.sp_Reporte_Devuelve_control_cobrTableAdapter


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            If RB_Mes.Checked = True Then
                ReportViewer1.LocalReport.ReportPath = ("Modulos/Cobranzas/Reportes/Reporte_LlaMes.rdlc")
            ElseIf RB_Año.Checked = True Then
                ReportViewer1.LocalReport.ReportPath = ("Modulos/Cobranzas/Reportes/Reporte_LlamAño.rdlc")
            End If

            Dim mes_dsd As Integer
            Dim mes_hst As Integer
            Dim mes As String

            If Drop_mes.SelectedValue = 0 Then
                mes_dsd = 0
                mes_hst = 999
                mes = 0
            Else
                mes = Me.Drop_mes.SelectedItem.Text
                mes_dsd = Drop_mes.SelectedValue
                mes_hst = Drop_mes.SelectedValue
            End If

            LlaAño = año.GetData(mes_dsd, mes_hst, txt_Año.Text, Sucursal, mes)

            Dim dt As DataTable

            dt = LlaAño

            Dim Llamadas As New Microsoft.Reporting.WebForms.ReportDataSource

            Llamadas = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Llamadas_sp_Reporte_Devuelve_control_cobr", dt)

            ReportViewer1.LocalReport.DataSources.Add(Llamadas)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try
    End Sub



#End Region

End Class
