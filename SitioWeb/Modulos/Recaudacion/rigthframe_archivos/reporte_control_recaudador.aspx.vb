Imports Microsoft.Reporting.WebForms
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Recaudacion_rigthframe_archivos_reporte_control_recaudador
    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
    Dim fc As New FuncionesGenerales.FComunes
    Dim msj As New ClsMensaje

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Response.Expires = -1
            Try

                SEMANA.Visible = False
                mes_año.Visible = True
                Me.dr_mes.Enabled = False
                Me.dr_mes.CssClass = "clsDisabled"
                ' hf_nro_hoja.Value = Request.QueryString("nro_hoja")
                cg.ParametrosDevuelve(TablaParametro.Meses, True, dr_mes)
                Me.txt_año.Text = Date.Now.Year

                Me.rb_criterio.SelectedValue = "A"
                Genera_reporte()


            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Sub Genera_reporte()

        Try

            Dim data As New DataSet_Pagos.sp_Reporte_control_recaudadorDataTable
            Dim tab As New DataSet_PagosTableAdapters.sp_Reporte_control_recaudadorTableAdapter

            Dim smn As New DataSet_Pagos.sp_Reporte_control_recaudador_semanalDataTable
            Dim tabsmn As New DataSet_PagosTableAdapters.sp_Reporte_control_recaudador_semanalTableAdapter

            Dim lr As New LocalReport


            ReportViewer1.Reset()





            If Me.rb_criterio.SelectedValue = "A" Then

                data = tab.GetData(1, 12, Me.txt_año.Text, Sucursal, "")
                ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Reporte_rec_Año.rdlc"



            ElseIf Me.rb_criterio.SelectedValue = "M" Then

                data = tab.GetData(CInt(Me.dr_mes.SelectedValue), CInt(Me.dr_mes.SelectedValue), Me.txt_año.Text, Sucursal, Me.dr_mes.SelectedItem.Text)
                ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Reporte_rec_Mes.rdlc"

            ElseIf Me.rb_criterio.SelectedValue = "S" Then

                smn = tabsmn.GetData(Me.txt_fec_des.Text, Me.txt_fec_has.Text, Sucursal)
                ReportViewer1.LocalReport.ReportPath = "Modulos\Recaudacion\Reportes\Reporte_Rec_Sem.rdlc"




            End If

            Dim dt As DataTable

            dt = data

            lr.DataSources.Add(New ReportDataSource("Sim", dt))




            Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource

            If rb_criterio.SelectedValue = "S" Then

                Dim dt2 As DataTable

                dt2 = smn

                rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_Reporte_control_recaudador_semanal", dt2)
            Else

                Dim dt2 As DataTable

                dt2 = data

                rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Pagos_sp_Reporte_control_recaudador", dt2)
            End If





            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub rb_criterio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_criterio.SelectedIndexChanged



        If Me.rb_criterio.SelectedValue = "A" Then

            SEMANA.Visible = False
            mes_año.Visible = True
            Me.dr_mes.Enabled = False
            Me.dr_mes.CssClass = "clsDisabled"

            ReportViewer1.Reset()



        ElseIf Me.rb_criterio.SelectedValue = "S" Then

            SEMANA.Visible = True
            mes_año.Visible = False
            Me.dr_mes.CssClass = "clsMandatorio"


            ReportViewer1.Reset()

        ElseIf Me.rb_criterio.SelectedValue = "M" Then

            SEMANA.Visible = False
            mes_año.Visible = True
            Me.dr_mes.Enabled = True
            Me.dr_mes.CssClass = "clsMandatorio"


            ReportViewer1.Reset()


        End If

    End Sub



    Protected Sub txt_fec_des_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec_des.TextChanged
        Try

            If Not IsDate(Me.txt_fec_des.Text) Then
                msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If

            Me.txt_fec_has.Text = ""
            Dim dia As Integer

            dia = CDate(Me.txt_fec_des.Text).DayOfWeek

            If dia = 1 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(4)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text)

            ElseIf dia = 2 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(3)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-1)


            ElseIf dia = 3 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(2)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-2)

            ElseIf dia = 4 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(1)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-3)

            ElseIf dia = 5 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-4)

            ElseIf dia = 6 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(-1)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-5)

            ElseIf dia = 7 Then


                Me.txt_fec_has.Text = CDate(Me.txt_fec_des.Text).AddDays(-2)
                Me.txt_fec_des.Text = CDate(Me.txt_fec_des.Text).AddDays(-6)

            End If


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Exclamacion)
        End Try
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click

        If Me.rb_criterio.SelectedValue = "S" Then
            If Not IsDate(Me.txt_fec_des.Text) Then
                msj.Mensaje(Me, "Atención", "Debe Ingresar fecha desde", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If
            If Not IsDate(Me.txt_fec_has.Text) Then
                msj.Mensaje(Me, "Atención", "Debe Ingresar fecha hasta", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            End If
        End If

        Genera_reporte()



    End Sub
End Class
