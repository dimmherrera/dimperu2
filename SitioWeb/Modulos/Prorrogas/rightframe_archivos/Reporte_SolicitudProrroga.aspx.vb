Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Prorrogas_rightframe_archivos_Reporte
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Dim Sesion As New ClsSession.SesionProrrogas
    Dim VAR As New FuncionesGenerales.Variables
    Dim AG As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Me.IsPostBack Then
            Response.Expires = -1
            Dim prorroga As New DataSet_Prorrogas.sp_Reporte_Prorroga_SolicitudDataTable
            Dim fun_prorroga As New DataSet_ProrrogasTableAdapters.sp_Reporte_Prorroga_SolicitudTableAdapter

            Dim det_prorroga As New DataSet_Prorrogas.sp_Reporte_Prorroga_SolicitudDetalleDataTable
            Dim fun_detalle_prorroga As New DataSet_ProrrogasTableAdapters.sp_Reporte_Prorroga_SolicitudDetalleTableAdapter

            '---------------------------------------------------------------------------------------------------------------------
            'Recibe Parametros de Pagina Padre
            '---------------------------------------------------------------------------------------------------------------------
            Dim MES As Int16
            Dim ESTADO As Int16

            Sesion.RutCliente = Request.QueryString("RUTCLIENTE")
            Sesion.ID_SolicitudProrroga = Request.QueryString("IDSPG")
            MES = Request.QueryString("MES")
            ESTADO = Request.QueryString("ESTADO")


            '---------------------------------------------------------------------------------------------------------------------
            'Actualiza DataSets según Criterios Recibidos
            '---------------------------------------------------------------------------------------------------------------------
            prorroga = fun_prorroga.GetData(Format(Sesion.RutCliente, VAR.FMT_RUT), MES, ESTADO, Sesion.ID_SolicitudProrroga)
            det_prorroga = fun_detalle_prorroga.GetData(Format(Sesion.RutCliente, VAR.FMT_RUT), ESTADO, Sesion.ID_SolicitudProrroga)

            Dim prorrogas As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim detalle_prorrogas As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = det_prorroga

            detalle_prorrogas = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Prorrogas_sp_Reporte_Prorroga_SolicitudDetalle", dt)

            Dim dt2 As DataTable

            dt2 = prorroga

            prorrogas = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Prorrogas_sp_Reporte_Prorroga_Solicitud", dt2)

            'If ESTADO = 0 Then
            '    btn_guardar.Visible = True
            '    Me.ReportViewer1.LocalReport.ReportPath = "Modulos\Prorrogas\Reportes\SolicitudProrroga.rdlc"
            'Else
            '    btn_guardar.Visible = False
            '    Me.ReportViewer1.LocalReport.ReportPath = "Modulos\Prorrogas\Reportes\Prorroga.rdlc"
            'End If
            'Se cambia estado a 1 por que con 0 no rescata datos 
            If ESTADO = 1 Then
                ' btn_guardar.Visible = True
                Me.ReportViewer1.LocalReport.ReportPath = "Modulos\Prorrogas\Reportes\SolicitudProrroga.rdlc"
            Else
                btn_guardar.Visible = False
                Me.ReportViewer1.LocalReport.ReportPath = "Modulos\Prorrogas\Reportes\Prorroga.rdlc"
            End If


            Me.ReportViewer1.LocalReport.DataSources.Add(prorrogas)
            Me.ReportViewer1.LocalReport.DataSources.Add(detalle_prorrogas)

            Me.ReportViewer1.DataBind()


        End If
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles btn_guardar.Click

        '-----------------------------------------------------------------------------------------------------------------------------
        'Calcula Solicitud de Prorroga
        '-----------------------------------------------------------------------------------------------------------------------------

        'msj.Mensaje(Me, "Atención", "¿Desea Guardar Solicitud de Prorroga?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_guardar.UniqueID, False)

        If AG.Prorroga_ModificaEstadoSolicitud(Format(Sesion.RutCliente, VAR.FMT_RUT), Sesion.ID_SolicitudProrroga, 1) = True Then
            msj.Mensaje(Me, "Atención", "Registro Guardado Correctamente", ClsMensaje.TipoDeMensaje._Informacion, , False)
        End If

    End Sub

    Protected Sub lb_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles lb_guardar.Click


        If AG.Prorroga_ModificaEstadoSolicitud(Format(Sesion.RutCliente, VAR.FMT_RUT), Sesion.ID_SolicitudProrroga, 1) = True Then
            msj.Mensaje(Me, "Atención", "Registro Guardado Correctamente", ClsMensaje.TipoDeMensaje._Informacion, , False)
        End If


    End Sub
End Class
