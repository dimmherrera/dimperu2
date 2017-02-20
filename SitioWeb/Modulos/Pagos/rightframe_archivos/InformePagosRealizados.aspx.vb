Imports Microsoft.Reporting.WebForms
Imports System
Imports System.IO
Imports System.Data




Partial Class Modulos_Pagos_rightframe_archivos_InformePagosRealizados
    Inherits System.Web.UI.Page

    Private Sub GeneraReporte()

        Dim lr As New LocalReport

        lr.ReportPath = Server.MapPath("Report_pagos_realizados.rdlc")
        ReportViewer1.LocalReport.ReportPath = "Modulos\Pagos\Reportes\Report_pagos_realizados.rdlc"


        'ReportViewer1.Height = 600
        Dim cb_pago As New DataSet_Pagos.sp_pd_retorna_pagos_reaDataTable
        Dim get_pago As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_reaTableAdapter

        Dim cb_pago_cxc As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cxcDataTable
        Dim get_pago_cxc As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cxcTableAdapter

        Dim cb_pago_aux As New DataSet_Pagos.sp_pd_retorna_pagos_reaDataTable
        Dim get_pago_aux As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_reaTableAdapter

        Dim cb_pago_aux_cxc As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cxcDataTable
        Dim get_pago_aux_cxc As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cxcTableAdapter



        If Request.QueryString("tipbus") = 0 Then

            cb_pago = get_pago.GetData(Request.QueryString("cli"), _
                                       Request.QueryString("ddr"), _
                                       Request.QueryString("suc1"), _
                                       Request.QueryString("suc2"), _
                                       Request.QueryString("eje1"), _
                                       Request.QueryString("eje2"), _
                                       Request.QueryString("fec1"), _
                                       Request.QueryString("fec2"), _
                                       Request.QueryString("tipo"), _
                                       5, _
                                       Request.QueryString("est1"), _
                                       Request.QueryString("est2"), _
                                       Request.QueryString("nro_dde"), _
                                       Request.QueryString("nro_hta"))

            cb_pago_aux = get_pago_aux.GetData(Request.QueryString("cli"), _
                                               Request.QueryString("ddr"), _
                                               Request.QueryString("suc1"), _
                                               Request.QueryString("suc2"), _
                                               Request.QueryString("eje1"), _
                                               Request.QueryString("eje2"), _
                                               Request.QueryString("fec1"), _
                                               Request.QueryString("fec2"), _
                                               Request.QueryString("tipo"), _
                                               6, _
                                               Request.QueryString("est1"), _
                                               Request.QueryString("est2"), _
                                               Request.QueryString("nro_dde"), _
                                               Request.QueryString("nro_hta"))

            If Request.QueryString("nro_dde") <> "" Then

                cb_pago_cxc = get_pago_cxc.GetData("", _
                                                   "", _
                                                   Request.QueryString("suc1"), _
                                                   Request.QueryString("suc2"), _
                                                   Request.QueryString("eje1"), _
                                                   Request.QueryString("eje2"), _
                                                   Request.QueryString("fec1"), _
                                                   Request.QueryString("fec2"), _
                                                   Request.QueryString("tipo"), _
                                                   5, Request.QueryString("est1"), _
                                                   Request.QueryString("est2"), _
                                                   Request.QueryString("nro_dde"), _
                                                   Request.QueryString("nro_hta"))
            Else

                cb_pago_cxc = get_pago_cxc.GetData(Request.QueryString("cli"), _
                                                   Request.QueryString("ddr"), _
                                                   Request.QueryString("suc1"), _
                                                   Request.QueryString("suc2"), _
                                                   Request.QueryString("eje1"), _
                                                   Request.QueryString("eje2"), _
                                                   Request.QueryString("fec1"), _
                                                   Request.QueryString("fec2"), _
                                                   Request.QueryString("tipo"), _
                                                   5, _
                                                   Request.QueryString("est1"), _
                                                   Request.QueryString("est2"), _
                                                   Request.QueryString("nro_dde"), _
                                                   Request.QueryString("nro_hta"))

                cb_pago_aux_cxc = get_pago_aux_cxc.GetData(Request.QueryString("cli"), _
                                                           Request.QueryString("ddr"), _
                                                           Request.QueryString("suc1"), _
                                                           Request.QueryString("suc2"), _
                                                           Request.QueryString("eje1"), _
                                                           Request.QueryString("eje2"), _
                                                           Request.QueryString("fec1"), _
                                                           Request.QueryString("fec2"), _
                                                           Request.QueryString("tipo"), _
                                                           6, _
                                                           Request.QueryString("est1"), _
                                                           Request.QueryString("est2"), _
                                                           Request.QueryString("nro_dde"), _
                                                           Request.QueryString("nro_hta"))

                cb_pago_cxc.Merge(cb_pago_aux_cxc)

            End If


            cb_pago.Merge(cb_pago_aux)


        Else

            cb_pago = get_pago.GetData(Request.QueryString("cli"), _
                                       Request.QueryString("ddr"), _
                                       Request.QueryString("suc1"), _
                                       Request.QueryString("suc2"), _
                                       Request.QueryString("eje1"), _
                                       Request.QueryString("eje2"), _
                                       Request.QueryString("fec1"), _
                                       Request.QueryString("fec2"), _
                                       Request.QueryString("tipo"), _
                                       Request.QueryString("tipbus"), _
                                       Request.QueryString("est1"), _
                                       Request.QueryString("est2"), _
                                       Request.QueryString("nro_dde"), _
                                       Request.QueryString("nro_hta"))

            cb_pago_cxc = get_pago_cxc.GetData(Request.QueryString("cli"), _
                                               Request.QueryString("ddr"), _
                                               Request.QueryString("suc1"), _
                                               Request.QueryString("suc2"), _
                                               Request.QueryString("eje1"), _
                                               Request.QueryString("eje2"), _
                                               Request.QueryString("fec1"), _
                                               Request.QueryString("fec2"), _
                                               Request.QueryString("tipo"), _
                                               Request.QueryString("tipbus"), _
                                               Request.QueryString("est1"), _
                                               Request.QueryString("est2"), _
                                               Request.QueryString("nro_dde"), _
                                               Request.QueryString("nro_hta"))

        End If


        Dim dt As DataTable

        dt = cb_pago_cxc

        Dim pago_cxc As New Microsoft.Reporting.WebForms.ReportDataSource
        pago_cxc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_pd_retorna_pagos_rea_cxc", dt)


        Dim dt2 As DataTable

        dt2 = cb_pago

        Dim pago As New Microsoft.Reporting.WebForms.ReportDataSource
        pago = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_pd_retorna_pagos_rea", dt2)
        ReportViewer1.LocalReport.DataSources.Add(pago)
        ReportViewer1.LocalReport.DataSources.Add(pago_cxc)

        ReportViewer1.DataBind()


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1
            GeneraReporte()
        End If


    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'carga los datos para la exportacion al archivo CSV 19-02-2016 German Nuñez
        Dim cb_pago_cvs As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cvsDataTable
        Dim get_pago_cvs As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cvsTableAdapter
        Dim cb_pago_cxc_cvs As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cxc_cvsDataTable
        Dim get_pago_cxc_cvs As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cxc_cvsTableAdapter

        Dim cb_pago_aux_cvs As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cvsDataTable
        Dim get_pago_aux_cvs As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cvsTableAdapter

        Dim cb_pago_aux_cxc_cvs As New DataSet_Pagos.sp_pd_retorna_pagos_rea_cxc_cvsDataTable
        Dim get_pago_aux_cxc_cvs As New DataSet_PagosTableAdapters.sp_pd_retorna_pagos_rea_cxc_cvsTableAdapter

        If Request.QueryString("tipbus") = 0 Then

            cb_pago_cvs = get_pago_cvs.GetData(Request.QueryString("cli"), _
                                       Request.QueryString("ddr"), _
                                       Request.QueryString("suc1"), _
                                       Request.QueryString("suc2"), _
                                       Request.QueryString("eje1"), _
                                       Request.QueryString("eje2"), _
                                       Request.QueryString("fec1"), _
                                       Request.QueryString("fec2"), _
                                       Request.QueryString("tipo"), _
                                       5, _
                                       Request.QueryString("est1"), _
                                       Request.QueryString("est2"), _
                                       Request.QueryString("nro_dde"), _
                                       Request.QueryString("nro_hta"))

            cb_pago_aux_cvs = get_pago_aux_cvs.GetData(Request.QueryString("cli"), _
                                               Request.QueryString("ddr"), _
                                               Request.QueryString("suc1"), _
                                               Request.QueryString("suc2"), _
                                               Request.QueryString("eje1"), _
                                               Request.QueryString("eje2"), _
                                               Request.QueryString("fec1"), _
                                               Request.QueryString("fec2"), _
                                               Request.QueryString("tipo"), _
                                               6, _
                                               Request.QueryString("est1"), _
                                               Request.QueryString("est2"), _
                                               Request.QueryString("nro_dde"), _
                                               Request.QueryString("nro_hta"))

            If Request.QueryString("nro_dde") <> "" Then

                cb_pago_cxc_cvs = get_pago_cxc_cvs.GetData("", _
                                                   "", _
                                                   Request.QueryString("suc1"), _
                                                   Request.QueryString("suc2"), _
                                                   Request.QueryString("eje1"), _
                                                   Request.QueryString("eje2"), _
                                                   Request.QueryString("fec1"), _
                                                   Request.QueryString("fec2"), _
                                                   Request.QueryString("tipo"), _
                                                   5, Request.QueryString("est1"), _
                                                   Request.QueryString("est2"), _
                                                   Request.QueryString("nro_dde"), _
                                                   Request.QueryString("nro_hta"))
            Else

                cb_pago_cxc_cvs = get_pago_cxc_cvs.GetData(Request.QueryString("cli"), _
                                                   Request.QueryString("ddr"), _
                                                   Request.QueryString("suc1"), _
                                                   Request.QueryString("suc2"), _
                                                   Request.QueryString("eje1"), _
                                                   Request.QueryString("eje2"), _
                                                   Request.QueryString("fec1"), _
                                                   Request.QueryString("fec2"), _
                                                   Request.QueryString("tipo"), _
                                                   5, _
                                                   Request.QueryString("est1"), _
                                                   Request.QueryString("est2"), _
                                                   Request.QueryString("nro_dde"), _
                                                   Request.QueryString("nro_hta"))

                cb_pago_aux_cxc_cvs = get_pago_aux_cxc_cvs.GetData(Request.QueryString("cli"), _
                                                           Request.QueryString("ddr"), _
                                                           Request.QueryString("suc1"), _
                                                           Request.QueryString("suc2"), _
                                                           Request.QueryString("eje1"), _
                                                           Request.QueryString("eje2"), _
                                                           Request.QueryString("fec1"), _
                                                           Request.QueryString("fec2"), _
                                                           Request.QueryString("tipo"), _
                                                           6, _
                                                           Request.QueryString("est1"), _
                                                           Request.QueryString("est2"), _
                                                           Request.QueryString("nro_dde"), _
                                                           Request.QueryString("nro_hta"))

                cb_pago_cxc_cvs.Merge(cb_pago_aux_cxc_cvs)

            End If


            cb_pago_cvs.Merge(cb_pago_aux_cvs)


        Else

            cb_pago_cvs = get_pago_cvs.GetData(Request.QueryString("cli"), _
                                       Request.QueryString("ddr"), _
                                       Request.QueryString("suc1"), _
                                       Request.QueryString("suc2"), _
                                       Request.QueryString("eje1"), _
                                       Request.QueryString("eje2"), _
                                       Request.QueryString("fec1"), _
                                       Request.QueryString("fec2"), _
                                       Request.QueryString("tipo"), _
                                       Request.QueryString("tipbus"), _
                                       Request.QueryString("est1"), _
                                       Request.QueryString("est2"), _
                                       Request.QueryString("nro_dde"), _
                                       Request.QueryString("nro_hta"))

            cb_pago_cxc_cvs = get_pago_cxc_cvs.GetData(Request.QueryString("cli"), _
                                               Request.QueryString("ddr"), _
                                               Request.QueryString("suc1"), _
                                               Request.QueryString("suc2"), _
                                               Request.QueryString("eje1"), _
                                               Request.QueryString("eje2"), _
                                               Request.QueryString("fec1"), _
                                               Request.QueryString("fec2"), _
                                               Request.QueryString("tipo"), _
                                               Request.QueryString("tipbus"), _
                                               Request.QueryString("est1"), _
                                               Request.QueryString("est2"), _
                                               Request.QueryString("nro_dde"), _
                                               Request.QueryString("nro_hta"))

        End If

        'desde aca hace la exportacion al archivo CSV 19-02-2016 German Nuñez
        Response.Clear()
        Response.Buffer = True
        'el nombre del archivo mas la fecha de consulta 19-02-2016 German Nuñez
        Response.AddHeader("content-disposition", "attachment;filename=InformePagosRealizados" + "_" + DateTime.Now.ToString("dd-MM-yyyy") + "." + "csv")
        Response.Charset = ""
        Response.ContentType = "application/text"
        Dim sb As New StringBuilder
        Dim i As Integer
        Dim k As Integer
        Dim z As Integer
        Dim x As Integer
        Dim y As Integer


        'agrega cabeceras
        For i = 0 To cb_pago_cvs.Columns.Count - 1 Step i + 1

            'agrega separador 
            sb.Append(cb_pago_cvs.Columns(i).ColumnName + ";")

        Next
        'agrega una nueva linea
        sb.Append(vbCr)

        Try
            'recorre las filas 
            For z = 0 To cb_pago_cvs.Rows.Count - 1 Step z + 1
                'recorre las columnas
                For k = 0 To cb_pago_cvs.Columns.Count - 1 Step k + 1

                    'agrega separador
                    sb.Append(cb_pago_cvs.Rows(z)(k).ToString + ";")

                Next

                k = 0
                'agrega una nueva linea
                sb.Append(vbCr)

            Next

        Catch ex As Exception

        End Try

        Try
            'recorre las filas 
            For x = 0 To cb_pago_cxc_cvs.Rows.Count - 1 Step x + 1
                'recorre las columnas
                For y = 0 To cb_pago_cxc_cvs.Columns.Count - 1 Step y + 1

                    'agrega separador
                    sb.Append(cb_pago_cxc_cvs.Rows(x)(y).ToString + ";")

                Next

                y = 0
                'agrega una nueva linea
                sb.Append(vbCr)

            Next

        Catch ex As Exception

        End Try


        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()
    End Sub
End Class
