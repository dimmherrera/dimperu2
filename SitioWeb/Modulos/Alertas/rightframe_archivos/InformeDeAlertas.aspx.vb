Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Alertas_rightframe_archivos_InformeDeAlertas
    Inherits System.Web.UI.Page

    Dim Var As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                If Not agt.ValidaAccesso(20, 20020504, Usr, "PRESIONO IMPRIMIR ALERTAS") Then
                    Msj.Mensaje(Me.Page, "Alertas", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Response.Expires = -1

                Dim RutCliente_Desde As String
                Dim RutCliente_Hasta As String

                If Val(RutCli) <> 0 Then
                    RutCliente_Desde = Format(RutCli, Var.FMT_RUT)
                    RutCliente_Hasta = Format(RutCli, Var.FMT_RUT)
                Else
                    RutCliente_Desde = "000000000000"
                    RutCliente_Hasta = "9999999999999"
                End If

                'CodEje = 18
                ReportViewer1.LocalReport.ReportPath = "Modulos\Alertas\Reportes\ReporteDeAlertas.rdlc"

                '---------------------------------------------------------------------------------------------------------
                'CABECERA
                Dim Cab As New DataSet_Alertas.sp_Reporte_Alertas_CabeceraDataTable
                Dim Cab_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_CabeceraTableAdapter

                If RutCliente_Desde = "000000000000" Then
                    'Cab = Cab_Table.GetData("", CodEje)
                    Cab = Cab_Table.GetData("", Ejecutivo)

                Else
                    Dim aux As String = Format(RutCli, Var.FMT_RUT)
                    Cab = Cab_Table.GetData(aux, Ejecutivo)
                End If

                '---------------------------------------------------------------------------------------------------------
                'DOCUMENTO POR VENCER
                Dim PorVencer As New DataSet_Alertas.sp_Reporte_Alertas_PorVencerDataTable
                Dim PorVencer_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_PorVencerTableAdapter

                PorVencer = PorVencer_Table.GetData(RutCliente_Desde, RutCliente_Hasta, Ejecutivo)

                '---------------------------------------------------------------------------------------------------------
                'DOCUMENTO EN MORA
                Dim EnMora As New DataSet_Alertas.sp_Reporte_Alertas_MoraDataTable
                Dim EnMora_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_MoraTableAdapter

                EnMora = EnMora_Table.GetData(RutCliente_Desde, RutCliente_Hasta, Ejecutivo)

                Dim str As String
                If EnMora.Count > 0 Then

                    For I = 0 To EnMora.Count - 1

                        If I = EnMora.Count - 1 Then

                            str = str & "" & EnMora.Item(I).id_dsi & ""

                        Else

                            str = str & "" & EnMora.Item(I).id_dsi & ","

                        End If

                    Next

                End If

                Dim Mora_Detalle As New DataSet_Alertas.sp_Reporte_Alertas_Mora_DetalleDataTable
                Dim Mora_Detalle_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_Mora_DetalleTableAdapter

                Mora_Detalle = Mora_Detalle_Table.GetData(str)

                '---------------------------------------------------------------------------------------------------------
                'DOCUMENTO EN MORA
                Dim Linea As New DataSet_Alertas.sp_Reporte_Alertas_LineasDataTable
                Dim Linea_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_LineasTableAdapter

                Linea = Linea_Table.GetData(RutCliente_Desde, RutCliente_Hasta, Ejecutivo)

                '---------------------------------------------------------------------------------------------------------
                'DOCUMENTO EN MORA
                Dim Excedentes As New DataSet_Alertas.sp_Reporte_Alertas_ExcedentesDataTable
                Dim Excedentes_Table As New DataSet_AlertasTableAdapters.sp_Reporte_Alertas_ExcedentesTableAdapter

                Excedentes = Excedentes_Table.GetData(RutCliente_Desde, RutCliente_Hasta, Ejecutivo)

                '---------------------------------------------------------------------------------------------------------
                'ASIGNAMOS TABLAS A DATASET

                Dim DATA As New Microsoft.Reporting.WebForms.ReportDataSource

                Dim dt As DataTable

                dt = Cab

                DATA = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Alertas_sp_Reporte_Alertas_Cabecera", dt)

                Dim DATA1 As New Microsoft.Reporting.WebForms.ReportDataSource

                Dim dt2 As DataTable

                dt2 = PorVencer

                DATA1 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Alertas_sp_Reporte_Alertas_PorVencer", dt2)

                Dim DATA2 As New Microsoft.Reporting.WebForms.ReportDataSource

                Dim dt3 As DataTable

                dt3 = Mora_Detalle

                DATA2 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Alertas_sp_Reporte_Alertas_Mora_Detalle", dt3)

                Dim DATA3 As New Microsoft.Reporting.WebForms.ReportDataSource

                Dim dt4 As DataTable

                dt4 = Linea

                DATA3 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Alertas_sp_Reporte_Alertas_Lineas", dt4)

                Dim DATA4 As New Microsoft.Reporting.WebForms.ReportDataSource

                Dim dt5 As DataTable

                dt5 = Excedentes

                DATA4 = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Alertas_sp_Reporte_Alertas_Excedentes", dt5)


                ReportViewer1.LocalReport.DataSources.Add(DATA)
                ReportViewer1.LocalReport.DataSources.Add(DATA1)
                ReportViewer1.LocalReport.DataSources.Add(DATA2)
                ReportViewer1.LocalReport.DataSources.Add(DATA3)
                ReportViewer1.LocalReport.DataSources.Add(DATA4)
                ReportViewer1.DataBind()

            End If

        Catch ex As Exception

        End Try

    End Sub

End Class
