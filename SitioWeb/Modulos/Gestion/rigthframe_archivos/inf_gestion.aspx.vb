Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Cobranzas_rigthframe_archivos_inf_gestion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1
        If Not IsPostBack Then
            Try

                'Operaciones Vencidas y por vencer
                '***************************************************************************************************************************
                Dim gestion As New DataSet_gestion.Sp_Reporte_gestion_ope_vencDataTable
                Dim get_ges As New DataSet_gestionTableAdapters.Sp_Reporte_gestion_ope_vencTableAdapter
                '***************************************************************************************************************************

                'Operaciones vencidas y por vencer (detalles)
                '***************************************************************************************************************************
                Dim gestion_det As New DataSet_gestion.Sp_Reporte_gestion_ope_venc_detalleDataTable
                Dim get_ges_det As New DataSet_gestionTableAdapters.Sp_Reporte_gestion_ope_venc_detalleTableAdapter
                '***************************************************************************************************************************

                'Evolución Negocios del cliente
                '***************************************************************************************************************************
                Dim gesneg As New DataSet_gestion.sp_reporte_negocios_clienteDataTable
                Dim get_gesneg As New DataSet_gestionTableAdapters.sp_reporte_negocios_clienteTableAdapter
                '***************************************************************************************************************************

                'Detalle evolución del negocio
                '***************************************************************************************************************************
                Dim gesneg_det As New DataSet_gestion.DataTable2DataTable
                Dim get_gesneg_det As New DataSet_gestionTableAdapters.DataTable2TableAdapter
                '***************************************************************************************************************************

                'Cartera vigente y vencida al dia de hoy
                '***************************************************************************************************************************
                Dim carvig_venc As New DataSet_gestion.Sp_Reporte_cartera_vig_vencDataTable
                Dim get_carvig_venc As New DataSet_gestionTableAdapters.Sp_Reporte_cartera_vig_vencTableAdapter
                '***************************************************************************************************************************

                'Detalle Cartera vigente y vencida al dia de hoy
                '***************************************************************************************************************************
                Dim carvig_venc_det As New DataSet_gestion.Sp_Reporte_cartera_vig_venc_detalleDataTable
                Dim get_carvig_venc_det As New DataSet_gestionTableAdapters.Sp_Reporte_cartera_vig_venc_detalleTableAdapter
                '***************************************************************************************************************************

                'Lineas
                '***************************************************************************************************************************
                Dim lin As New DataSet_gestion.sp_Lineas_VencDataTable
                Dim get_lin As New DataSet_gestionTableAdapters.sp_Lineas_VencTableAdapter
                '***************************************************************************************************************************


                'ReportViewer1.Height = 600
                Dim lr As New LocalReport
                ReportViewer1.LocalReport.DataSources.Clear()

                ReportViewer1.Reset()

                If Request.QueryString("tipo") = 1 Then

                    '***************************************************************************************************************************
                    'Operaciones Vencidas a una fecha determinada
                    '***************************************************************************************************************************

                    lr.ReportPath = Server.MapPath("Flujo_ope_ven.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Flujo_ope_ven.rdlc"

                    gestion = get_ges.GetData(Request.QueryString("fec1"), Request.QueryString("fec2"), 1)
                    gestion_det = get_ges_det.GetData(Request.QueryString("fec1"), Request.QueryString("fec2"), 1)

                    Dim dt As DataTable

                    dt = gestion

                    Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
                    gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_gestion_ope_venc", dt)


                    Dim dt2 As DataTable

                    dt2 = gestion_det

                    Dim gest_det As New Microsoft.Reporting.WebForms.ReportDataSource
                    gest_det = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_gestion_ope_venc_detalle", dt2)

                    ReportViewer1.LocalReport.DataSources.Add(gest)
                    ReportViewer1.LocalReport.DataSources.Add(gest_det)

                ElseIf Request.QueryString("tipo") = 2 Then

                    '***************************************************************************************************************************
                    'Operaciones por  Vencer a una fecha determinada
                    '***************************************************************************************************************************

                    lr.ReportPath = Server.MapPath("Flujo_ope_por_ven.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Flujo_ope_por_ven.rdlc"

                    gestion = get_ges.GetData(Request.QueryString("fec1"), Request.QueryString("fec2"), 2)
                    gestion_det = get_ges_det.GetData(Request.QueryString("fec1"), Request.QueryString("fec2"), 2)

                    Dim dt As DataTable

                    dt = gestion

                    Dim gest As New Microsoft.Reporting.WebForms.ReportDataSource
                    gest = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_gestion_ope_venc", dt)

                    Dim dt2 As DataTable

                    dt2 = gestion_det

                    Dim gest_det As New Microsoft.Reporting.WebForms.ReportDataSource
                    gest_det = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_gestion_ope_venc_detalle", dt2)

                    ReportViewer1.LocalReport.DataSources.Add(gest)
                    ReportViewer1.LocalReport.DataSources.Add(gest_det)


                ElseIf Request.QueryString("tipo") = 3 Then

                    '***************************************************************************************************************************
                    'Evolución de Negocios de clientes
                    '***************************************************************************************************************************

                    lr.ReportPath = Server.MapPath("Neg_cli_periodo.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Neg_cli_periodo.rdlc"

                    gesneg = get_gesneg.GetData(Request.QueryString("fec1"), Request.QueryString("fec2"))

                    Dim rut_cli As String

                    For i = 0 To gesneg.Count - 1

                        rut_cli = gesneg.Item(i).cli_idc

                        gesneg_det = get_gesneg_det.GetData(gesneg.Item(i).id_ope)

                        If gesneg_det.Count > 0 Then

                            gesneg.Item(i).sdo_vencido = gesneg_det.Item(0).sdo_venc
                            gesneg.Item(i).tot_cobr = gesneg_det.Item(0).sdo_cobr

                        End If

                        'Calcula promedio de dias
                        If i + 1 <= gesneg.Count - 1 Then

                            If gesneg.Item(i).cli_idc = gesneg.Item(i + 1).cli_idc Then
                                gesneg.Item(i).prom_ope = DateDiff(DateInterval.Day, gesneg.Item(i).opn_fec, gesneg.Item(i + 1).opn_fec)
                            Else
                                gesneg.Item(i).prom_ope = 0
                            End If

                        End If


                    Next

                    Dim dt As DataTable

                    dt = gesneg

                    Dim gest_prueba As New Microsoft.Reporting.WebForms.ReportDataSource
                    gest_prueba = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_DataTable1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(gest_prueba)

                ElseIf Request.QueryString("tipo") = 4 Then

                    '***************************************************************************************************************************
                    'Cartera Vigente al dia de hoy
                    '***************************************************************************************************************************

                    lr.ReportPath = Server.MapPath("Cart_vig_fecha.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Cart_vig_fecha.rdlc"


                    carvig_venc = get_carvig_venc.GetData(1)
                    carvig_venc_det = get_carvig_venc_det.GetData(1)

                    Dim dt As DataTable

                    dt = carvig_venc

                    Dim cartera As New Microsoft.Reporting.WebForms.ReportDataSource
                    cartera = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_cartera_vig_venc", dt)

                    Dim dt2 As DataTable

                    dt2 = carvig_venc_det

                    Dim cartera_det As New Microsoft.Reporting.WebForms.ReportDataSource
                    cartera_det = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_cartera_vig_venc_detalle", dt2)


                    ReportViewer1.LocalReport.DataSources.Add(cartera)
                    ReportViewer1.LocalReport.DataSources.Add(cartera_det)

                ElseIf Request.QueryString("tipo") = 5 Then

                    '***************************************************************************************************************************
                    'Cartera vencida al dia de hoyu
                    '***************************************************************************************************************************
                    lr.ReportPath = Server.MapPath("Cart_venc_fecha.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Cart_venc_fecha.rdlc"


                    carvig_venc = get_carvig_venc.GetData(2)
                    carvig_venc_det = get_carvig_venc_det.GetData(2)

                    Dim dt As DataTable

                    dt = carvig_venc

                    Dim cartera As New Microsoft.Reporting.WebForms.ReportDataSource
                    cartera = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_cartera_vig_venc", dt)

                    Dim dt2 As DataTable

                    dt2 = carvig_venc_det

                    Dim cartera_det As New Microsoft.Reporting.WebForms.ReportDataSource
                    cartera_det = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_Sp_Reporte_cartera_vig_venc_detalle", dt2)


                    ReportViewer1.LocalReport.DataSources.Add(cartera)
                    ReportViewer1.LocalReport.DataSources.Add(cartera_det)

                ElseIf Request.QueryString("tipo") = 6 Then

                    '***************************************************************************************************************************
                    'Lineas
                    '***************************************************************************************************************************

                    lr.ReportPath = Server.MapPath("lineas_venc.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\lineas_venc.rdlc"

                    lin = get_lin.GetData(CDate(Request.QueryString("fec1")), _
                                          CDate(Request.QueryString("fec2")))

                    Dim dt As DataTable

                    dt = lin

                    Dim Lineas As New Microsoft.Reporting.WebForms.ReportDataSource
                    Lineas = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_sp_Lineas_Venc", dt)


                    ReportViewer1.LocalReport.DataSources.Add(Lineas)


                ElseIf Request.QueryString("tipo") = 7 Then

                    ''***************************************************************************************************************************
                    ''Resumen ejecutivo ficha cliente
                    '***************************************************************************************************************************
                    Dim Cli As New DataSet_gestion.sp_Resumen_Eje_FichaClienteDataTable
                    Dim get_cli As New DataSet_gestionTableAdapters.sp_Resumen_Eje_FichaClienteTableAdapter

                    Dim Cli_Cre As New DataSet_gestion.sp_Resumen_Eje_FichaCliente_DocCobDataTable
                    Dim get_cli_Cre As New DataSet_gestionTableAdapters.sp_Resumen_Eje_FichaCliente_DocCobTableAdapter

                    Dim Cli_car As New DataSet_gestion.sp_Resumen_Eje_FichaCliente_CarteraDataTable
                    Dim get_cli_car As New DataSet_gestionTableAdapters.sp_Resumen_Eje_FichaCliente_CarteraTableAdapter

                    '***************************************************************************************************************************


                    lr.ReportPath = Server.MapPath("Resumen_cliente.rdlc")
                    ReportViewer1.LocalReport.ReportPath = "Modulos\gestion\Reportes\Resumen_cliente.rdlc"

                    Cli = get_cli.GetData(Request.QueryString("RutCliente"), Request.QueryString("periodo"))
                    Cli_Cre = get_cli_Cre.GetData(Request.QueryString("RutCliente"), Request.QueryString("periodo"))
                    Cli_car = get_cli_car.GetData(Request.QueryString("RutCliente"))

                    Dim dt As DataTable

                    dt = Cli

                    Dim cliente As New Microsoft.Reporting.WebForms.ReportDataSource
                    cliente = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_sp_Resumen_Eje_FichaCliente", dt)

                    Dim dt2 As DataTable

                    dt2 = Cli_Cre

                    Dim cliente_cre As New Microsoft.Reporting.WebForms.ReportDataSource
                    cliente_cre = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_sp_Resumen_Eje_FichaCliente_DocCob", dt2)

                    Dim dt3 As DataTable

                    dt3 = Cli_car

                    Dim cliente_car As New Microsoft.Reporting.WebForms.ReportDataSource
                    cliente_car = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_gestion_sp_Resumen_Eje_FichaCliente_Cartera", dt3)


                    ReportViewer1.LocalReport.DataSources.Add(cliente)
                    ReportViewer1.LocalReport.DataSources.Add(cliente_cre)
                    ReportViewer1.LocalReport.DataSources.Add(cliente_car)

                End If

                ReportViewer1.DataBind()

            Catch ex As Exception

            End Try


        End If

    End Sub

End Class
