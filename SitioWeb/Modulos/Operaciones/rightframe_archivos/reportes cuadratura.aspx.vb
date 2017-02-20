Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports FuncionesGenerales.FComunes
Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_reportes_cuadratura
    Inherits System.Web.UI.Page
    Dim msj As New ClsMensaje
    Dim fc As New FuncionesGenerales.FComunes
    Protected Sub ib_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_buscar.Click

        If Me.dr_informes.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un informe para generar", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Me.txt_fec.Text = "" Then
            msj.Mensaje(Me, "Atención", "Debe ingresar una fecha para generar", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Genera_reporte()
    End Sub

    Public Sub Genera_reporte()

        Dim mensaje As String
        mensaje = ""
        'Negociación
        Dim ng As New Dataset_Operacion.sp_op_inf_hoja_negocioDataTable
        Dim get_neg As New Dataset_OperacionTableAdapters.sp_op_inf_hoja_negocioTableAdapter

        'Simulacion

        Dim dsi As New Dataset_Operacion.Sp_Reporte_Simulación_DocDataTable
        Dim get_doc As New Dataset_OperacionTableAdapters.Sp_Reporte_Simulación_DocTableAdapter

        'Egresos con deposito antes o despues

        Dim egr As New Dataset_Operacion.sp_reporte_cuadratura_egresosDataTable
        Dim get_egr As New Dataset_OperacionTableAdapters.sp_reporte_cuadratura_egresosTableAdapter

        'Cartera Vigente Morosa

        Dim cvg As New Dataset_Operacion.sp_op_inf_cartera_vigenteDataTable
        Dim get_cvg As New Dataset_OperacionTableAdapters.sp_op_inf_cartera_vigenteTableAdapter

        'Otorgamientos Diarios

        Dim oto As New Dataset_Operacion.sp_op_reporte_cierres_otoDataTable
        Dim get_oto As New Dataset_OperacionTableAdapters.sp_op_reporte_cierres_otoTableAdapter

        'Documentos con o sin Cobranza

        Dim cbr As New Dataset_Operacion.sp_op_reporte_doctos_cbrDataTable
        Dim get_cbr As New Dataset_OperacionTableAdapters.sp_op_reporte_doctos_cbrTableAdapter

        'Documentos con o sin Cobranza

        Dim spr As New Dataset_Operacion.sp_op_reporte_doctos_respDataTable
        Dim get_spr As New Dataset_OperacionTableAdapters.sp_op_reporte_doctos_respTableAdapter

        'Operaciones diarias Mto a girar 

        Dim odr As New Dataset_Operacion.sp_op_informe_lista_mto_girarDataTable
        Dim get_odr As New Dataset_OperacionTableAdapters.sp_op_informe_lista_mto_girarTableAdapter

        'Excedentes

        Dim exc As New Dataset_Operacion.sp_op_cuadratura_excedentesDataTable
        Dim get_exc As New Dataset_OperacionTableAdapters.sp_op_cuadratura_excedentesTableAdapter

        'Comisiones a Facturar

        Dim CF As New Dataset_Operacion.sp_fa_inf_comision1DataTable
        Dim GET_CF As New Dataset_OperacionTableAdapters.sp_fa_inf_comision1TableAdapter


        Dim lr As New LocalReport
        ReportViewer1.LocalReport.DataSources.Clear()

        If Me.dr_informes.SelectedValue = 1 Then
            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("reporte_hoja_negocio.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\reporte_hoja_negocio.rdlc"

            ng = get_neg.GetData(CDate(Me.txt_fec.Text))

            If ng.Count = 0 Then
                mensaje = "No se encontraron datos para la fecha indicada"

            End If

            Dim dt As DataTable

            dt = ng

            Dim neg As New Microsoft.Reporting.WebForms.ReportDataSource
            neg = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_inf_hoja_negocio", dt)
            ReportViewer1.LocalReport.DataSources.Add(neg)


        ElseIf Me.dr_informes.SelectedValue = 2 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Informe Doctos Cobr.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe Doctos Cobr.rdlc"

            'Documentos con Cobranza

            cbr = get_cbr.GetData("S", CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = cbr

            Dim cb As New Microsoft.Reporting.WebForms.ReportDataSource

            cb = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_reporte_doctos_cbr", cb)

            ReportViewer1.LocalReport.DataSources.Add(cb)

        ElseIf Me.dr_informes.SelectedValue = 3 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Informe doctos sin cbr.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe doctos sin cbr.rdlc"

            'Documentos sin Cobranza

            cbr = get_cbr.GetData("N", CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = cbr

            Dim cb As New Microsoft.Reporting.WebForms.ReportDataSource

            cb = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_reporte_doctos_cbr", dt)

            ReportViewer1.LocalReport.DataSources.Add(cb)

        ElseIf Me.dr_informes.SelectedValue = 4 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Doctos_con_resp.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Doctos_con_resp.rdlc"

            'Documentos con Responsabilidad

            spr = get_spr.GetData(1, CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = spr


            Dim rp As New Microsoft.Reporting.WebForms.ReportDataSource

            rp = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_reporte_doctos_resp", dt)

            ReportViewer1.LocalReport.DataSources.Add(rp)

        ElseIf Me.dr_informes.SelectedValue = 5 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Doctos_sin_resp.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Doctos_sin_resp.rdlc"

            'Documentos sin Responsabilidad

            spr = get_spr.GetData(2, CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = spr

            Dim rp As New Microsoft.Reporting.WebForms.ReportDataSource

            rp = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_reporte_doctos_resp", dt)

            ReportViewer1.LocalReport.DataSources.Add(rp)




        ElseIf Me.dr_informes.SelectedValue = 6 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Cartera_vigente_totales.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Cartera_vigente_totales.rdlc"


            ' Cartera Vigente Morosa

            cvg = get_cvg.GetData(CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = cvg


            Dim cvgm As New Microsoft.Reporting.WebForms.ReportDataSource

            cvgm = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_inf_cartera_vigente", dt)

            ReportViewer1.LocalReport.DataSources.Add(cvgm)



        ElseIf Me.dr_informes.SelectedValue = 7 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Egresos_ant_14.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Egresos_ant_14.rdlc"


            ' Egresos con deposito antes de 14 hrs 

            egr = get_egr.GetData(CDate(Me.txt_fec.Text), "S")

            Dim dt As DataTable

            dt = egr

            Dim egr_Ant As New Microsoft.Reporting.WebForms.ReportDataSource

            egr_Ant = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_cuadratura_egresos", dt)

            ReportViewer1.LocalReport.DataSources.Add(egr_Ant)

        ElseIf Me.dr_informes.SelectedValue = 8 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Egresos_dps_14.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Egresos_dps_14.rdlc"


            ' Egresos con deposito despues de 14 hrs 

            egr = get_egr.GetData(CDate(Me.txt_fec.Text), "N")

            Dim dt As DataTable

            dt = egr

            Dim egr_Ant As New Microsoft.Reporting.WebForms.ReportDataSource

            egr_Ant = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_reporte_cuadratura_egresos", dt)

            ReportViewer1.LocalReport.DataSources.Add(egr_Ant)



        ElseIf Me.dr_informes.SelectedValue = 9 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Report_otg_diarios.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Report_otg_diarios.rdlc"


            'Otorgamiento Diario

            oto = get_oto.GetData(CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = oto

            Dim otg As New Microsoft.Reporting.WebForms.ReportDataSource

            otg = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_reporte_cierres_oto", dt)

            ReportViewer1.LocalReport.DataSources.Add(otg)



        ElseIf Me.dr_informes.SelectedValue = 10 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Informe_Ope_diarias_mto.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe_Ope_diarias_mto.rdlc"


            'Mto a girar Operacion Diario

            odr = get_odr.GetData(CDate(Me.txt_fec.Text))

            Dim dt As DataTable

            dt = odr

            Dim opd As New Microsoft.Reporting.WebForms.ReportDataSource

            opd = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_informe_lista_mto_girar", dt)

            ReportViewer1.LocalReport.DataSources.Add(opd)

        ElseIf Me.dr_informes.SelectedValue = 11 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Informe_exc_ant_14.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe_exc_ant_14.rdlc"


            'Excedentes

            exc = get_exc.GetData(CDate(Me.txt_fec.Text), "S")

            For i = 0 To exc.Count - 1
                If exc.Item(i).pnu_atr_003 = "PES" Then
                    exc.Item(i).total_peso = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "UF" Then
                    exc.Item(i).total_uf = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "DOL" Then
                    exc.Item(i).total_dolar = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "EUR" Then
                    exc.Item(i).total_euro = exc.Item(i).egr_mto

                End If
            Next

            Dim dt As DataTable

            dt = exc

            Dim exd As New Microsoft.Reporting.WebForms.ReportDataSource

            exd = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_cuadratura_excedentes", dt)


            ReportViewer1.LocalReport.DataSources.Add(exd)


        ElseIf Me.dr_informes.SelectedValue = 12 Then

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Informe_exc_dps_14.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Informe_exc_dps_14.rdlc"


            'Excedentes

            exc = get_exc.GetData(CDate(Me.txt_fec.Text), "N")

            For i = 0 To exc.Count - 1
                If exc.Item(i).pnu_atr_003 = "PES" Then
                    exc.Item(i).total_peso = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "UF" Then
                    exc.Item(i).total_uf = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "DOL" Then
                    exc.Item(i).total_dolar = exc.Item(i).egr_mto
                ElseIf exc.Item(i).pnu_atr_003 = "EUR" Then
                    exc.Item(i).total_euro = exc.Item(i).egr_mto

                End If
            Next

            Dim dt As DataTable

            dt = exc

            Dim exd As New Microsoft.Reporting.WebForms.ReportDataSource

            exd = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_op_cuadratura_excedentes", dt)

            ReportViewer1.LocalReport.DataSources.Add(exd)

        ElseIf Me.dr_informes.SelectedValue = 13 Then

            Dim fecha_dde As Date
            Dim ult_dia As Short
            Dim fecha_hta As Date

            ReportViewer1.Reset()
            lr.ReportPath = Server.MapPath("Report_com_fact.rdlc")
            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\Report_com_fact.rdlc"


            'Comisiones a Facturar

            fecha_dde = "01/" & CDate(Me.txt_fec.Text).Month & "/" & CDate(Me.txt_fec.Text).Year
            ult_dia = fc.DevuelveUltimoDiaDelMes(CDate(Me.txt_fec.Text).Month, CDate(Me.txt_fec.Text).Year)
            fecha_hta = ult_dia & "/" & CDate(Me.txt_fec.Text).Month & "/" & CDate(Me.txt_fec.Text).Year

            CF = GET_CF.GetData(fecha_dde, fecha_hta, 1, 1, CodEje)

            Dim dt As DataTable

            dt = CF

            Dim cft As New Microsoft.Reporting.WebForms.ReportDataSource

            cft = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_sp_fa_inf_comision1", dt)

            ReportViewer1.LocalReport.DataSources.Add(cft)


        End If

        'If DirectCast(DirectCast(Me.ReportViewer1.LocalReport.DataSources.Item(0).Value, System.Object), dataset.item(0).table).Count > 0 Then
        '    msj.Mensaje(Me, "Atención", mensaje, ClsMensaje.TipoDeMensaje._Exclamacion)
        'End If

        ReportViewer1.DataBind()

    End Sub

    Protected Sub ib_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_limpiar.Click
        Me.dr_informes.SelectedValue = 0
        Me.txt_fec.Text = Date.Now.ToShortDateString
        ReportViewer1.Reset()
        ReportViewer1.Dispose()
    End Sub
End Class
