Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.IO
Imports System.Data

Partial Class Reporte_EvaluacionCliDeu
    Inherits System.Web.UI.Page

    Dim CA As New ClaseArchivos

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        If Not IsPostBack Then

            Response.Expires = -1

            If Request.QueryString("id") <> "" Then
                NroEvaluacion = Request.QueryString("id")
            End If

            If NroEvaluacion > 0 Then

                Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(NroEvaluacion)

                If abytFileData.Length <> 0 Then
                    Response.Clear()
                    Response.Buffer = True
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                    Response.AddHeader("cache-control", "private")
                    Response.AddHeader("Expires", "0")
                    Response.AddHeader("Pragma", "cache")
                    Response.AddHeader("content-disposition", "attachment; filename=Evaluacion_" & NroEvaluacion & ".pdf")
                    Response.AddHeader("Accept-Ranges", "none")
                    Response.BinaryWrite(abytFileData)
                    Response.Flush()
                    Response.End()
                Else
                    Genera_reporte()
                End If

            End If



        End If

    End Sub

    Public Sub Genera_reporte()

        Try

            'Datos generales del cliente
            Dim Resumen As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_CabeceraDataTable
            Dim Tab1 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_CabeceraTableAdapter

            'grilla deudor
            Dim Deudores As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_DeudoresDataTable
            Dim Tab As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_DeudoresTableAdapter

            'grilla pagare vigentes
            Dim Pagares As New DataSet_ReporteEvaluacion.sp_pagares_vigentesDataTable
            Dim Tab2 As New DataSet_ReporteEvaluacionTableAdapters.sp_pagares_vigentesTableAdapter

            'grilla pagare vigentes
            Dim Concentracion As New DataSet_ReporteEvaluacion.Sp_Concentracion_ClienteDataTable
            Dim Tab3 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Concentracion_ClienteTableAdapter

            'Distribucion deuda Morosa y Vctos. Futuros
            Dim Distribucion As New DataSet_ReporteEvaluacion.sp_distribucion_diasDataTable
            Dim Tab4 As New DataSet_ReporteEvaluacionTableAdapters.sp_distribucion_diasTableAdapter

            'Evolucion Mora 

            Dim evolucion As New DataSet_ReporteEvaluacion.sp_cl_evolucion_deudaDataTable
            Dim tab5 As New DataSet_ReporteEvaluacionTableAdapters.sp_cl_evolucion_deudaTableAdapter

            Dim lr As New LocalReport

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()


            lr.ReportPath = Server.MapPath("ReportEvaluacion.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\ReportEvaluacion.rdlc"


            Dim CLI As cli_cls
            Dim Moneda As Integer
            Dim Porcentaje As Decimal
            Dim id_eva As Integer

            If Request.QueryString("Moneda") <> "" Then
                Moneda = Request.QueryString("Moneda")
            End If

            If Request.QueryString("Porcentaje") <> "" Then
                Porcentaje = Request.QueryString("Porcentaje")
            End If

            If Request.QueryString("id") <> "" Then
                id_eva = Request.QueryString("id")
            End If

            CLI = Session("Cliente")

            'Resumen = Tab1.GetData(CLI.cli_idc, Porcentaje, Moneda)

            Resumen = Tab1.GetData(id_eva)

            Dim dt As DataTable

            dt = Resumen

            Deudores = Tab.GetData(id_eva)

            Dim dt2 As DataTable

            dt2 = Deudores

            Pagares = Tab2.GetData(CLI.cli_idc)

            Dim dt3 As DataTable

            dt3 = Pagares

            Concentracion = Tab3.GetData(CLI.cli_idc, 1, 999999999, "CLIENTE COMO CLIENTE", 1, 2)

            Dim dt4 As DataTable

            dt4 = Concentracion

            Distribucion = Tab4.GetData(CLI.cli_idc)

            Dim dt5 As DataTable

            dt5 = Distribucion

            evolucion = tab5.GetData(CLI.cli_idc)

            Dim dt6 As DataTable

            dt6 = evolucion

            Dim rsc As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim pag As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim con As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim dis As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim evo As New Microsoft.Reporting.WebForms.ReportDataSource
            rsc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Cabecera", dt)
            rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Deudores", dt2)
            pag = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_pagares_vigentes", dt3)
            con = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Concentracion_Cliente", dt4)
            dis = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_distribucion_dias", dt5)
            evo = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_cl_evolucion_deuda", dt6)

            ReportViewer1.LocalReport.DataSources.Add(rsc)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(pag)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(con)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(dis)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(evo)
            ReportViewer1.DataBind()

            'Dim archivo As String = "Evaluacion_" & CLI.cli_idc & "_ID_" & id_eva & ".pdf"
            'Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            'Dim mimeType As String = Nothing
            'Dim encoding As String = Nothing
            'Dim fileNameExtension As String = Nothing
            'Dim streams As String() = Nothing
            'Dim war As Warning() = Nothing
            'Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, war)
            'Dim Fs As New FileStream(path, FileMode.Create)
            'Fs.Write(Bit, 0, Bit.Length)
            'Fs.Close()


            Dim archivo As String = "Evaluacion_" & CLI.cli_idc & "_ID_" & id_eva & ".pdf"
            Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim fileNameExtension As String = Nothing
            Dim streams As String() = Nothing
            Dim war As Warning() = Nothing
            Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", _
                                                                 Nothing, _
                                                                 mimeType, _
                                                                 encoding, _
                                                                 fileNameExtension, _
                                                                 streams, _
                                                                 war)
            CA.GuardaPDF(id_eva, Bit)

        Catch ex As Exception

        End Try

    End Sub

End Class
