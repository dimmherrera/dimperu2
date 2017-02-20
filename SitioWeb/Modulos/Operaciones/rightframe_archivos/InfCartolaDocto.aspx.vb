Imports CapaDatos
Imports System.Data

Partial Class Modulos_Operaciones_rightframe_archivos_InfCartolaDocto
    Inherits System.Web.UI.Page

    Dim SesionOp As New ClsSession.SesionOperaciones

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            If Not Me.IsPostBack Then
                Genera_reporte()
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Procedimientos y funciones Generales"

    Public Sub Genera_reporte()

        Try
            Dim Var As New FuncionesGenerales.Variables

            Dim Otorg As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_OtorgamientosDataTable
            Dim Recau As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_RecaudacionDataTable
            Dim Recau_aux As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_RecaudacionDataTable

            Dim Exce As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_ExcedentesDataTable
            Dim Cta As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_OtrasCuentasDataTable
            Dim Ges As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_GestionDataTable

            Dim Tab_Otorg As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_OtorgamientosTableAdapter
            Dim Tab_Recau As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_RecaudacionTableAdapter

            Dim Tab_Recau_aux As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_RecaudacionTableAdapter


            Dim Tab_Exce As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_ExcedentesTableAdapter
            Dim Tab_Cta As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_OtrasCuentasTableAdapter
            Dim Tab_Ges As New Dataset_OperacionTableAdapters.Sp_Reporte_Operaciones_Detalle_GestionTableAdapter


            Dim Otorg1 As New Dataset_Operacion.Sp_Reporte_Operaciones_Detalle_OtorgamientosDataTable


            ReportViewer1.LocalReport.DataSources.Clear()

            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Operaciones\Reportes\CartolaDocto.rdlc"

            Dim O As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim Otorgamiento As Boolean
            Dim Recaudacion As Boolean
            Dim Excedentes As Boolean
            Dim Cuentas As Boolean
            Dim GestionDoctos As Boolean
            Dim I As Integer
            Dim str As String
            Dim data As New Data.DataSet

            Otorgamiento = Request.QueryString("Otorgamiento")
            Recaudacion = Request.QueryString("Recaudacion")
            Excedentes = Request.QueryString("Excedentes")
            Cuentas = Request.QueryString("Cuentas")
            GestionDoctos = Request.QueryString("GestionDoctos")

            For I = 1 To SesionOp.Coll_DetalleDocto.Count

                If I = SesionOp.Coll_DetalleDocto.Count Then
                    str = str & "" & SesionOp.Coll_DetalleDocto.Item(I).id_dsi & ""
                Else

                    str = str & "" & SesionOp.Coll_DetalleDocto.Item(I).id_dsi & " , "
                End If
            Next


            'If Otorgamiento Then

            Otorg = Tab_Otorg.GetData(str)

            Dim dt As DataTable

            dt = Otorg

            O = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Otorgamientos", dt)

            ReportViewer1.LocalReport.DataSources.Add(O)

            'Else

            '    Otorg = Tab_Otorg.GetData("0")

            '    O = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Otorgamientos", Otorg)

            '    ReportViewer1.LocalReport.DataSources.Add(O)

            'End If

            If Recaudacion Then
                Recau = Tab_Recau.GetData(str, 1)
                Recau_aux = Tab_Recau_aux.GetData(str, 2)

                Recau.Merge(Recau_aux)

                Dim dt2 As DataTable

                dt2 = Recau

                Dim R As New Microsoft.Reporting.WebForms.ReportDataSource
                R = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Recaudacion", dt2)

                ReportViewer1.LocalReport.DataSources.Add(R)


            Else
                Recau = Tab_Recau.GetData("0", 1)
                Recau_aux = Tab_Recau_aux.GetData("0", 2)

                Recau.Merge(Recau_aux)

                Dim dt2 As DataTable

                dt2 = Recau

                Dim R As New Microsoft.Reporting.WebForms.ReportDataSource
                R = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Recaudacion", dt2)

                ReportViewer1.LocalReport.DataSources.Add(R)

            End If

            If Excedentes Then
                Exce = Tab_Exce.GetData(str)

                Dim dt2 As DataTable

                dt2 = Exce

                Dim E As New Microsoft.Reporting.WebForms.ReportDataSource
                E = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Excedentes", dt2)
                ReportViewer1.LocalReport.DataSources.Add(E)
            Else

                Exce = Tab_Exce.GetData("0")

                Dim dt2 As DataTable

                dt2 = Exce

                Dim E As New Microsoft.Reporting.WebForms.ReportDataSource
                E = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Excedentes", dt2)
                ReportViewer1.LocalReport.DataSources.Add(E)
            End If

            If Cuentas Then
                Cta = Tab_Cta.GetData(str)

                Dim dt2 As DataTable

                dt2 = Cta

                Dim C As New Microsoft.Reporting.WebForms.ReportDataSource
                C = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_OtrasCuentas", dt2)

                ReportViewer1.LocalReport.DataSources.Add(C)

            Else

                Cta = Tab_Cta.GetData("0")

                Dim dt2 As DataTable

                dt2 = Cta

                Dim C As New Microsoft.Reporting.WebForms.ReportDataSource
                C = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_OtrasCuentas", dt2)

                ReportViewer1.LocalReport.DataSources.Add(C)

            End If

            If GestionDoctos Then
                Ges = Tab_Ges.GetData(str)

                Dim dt2 As DataTable

                dt2 = Ges

                Dim G As New Microsoft.Reporting.WebForms.ReportDataSource
                G = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Gestion", dt2)

                ReportViewer1.LocalReport.DataSources.Add(G)

            Else

                Ges = Tab_Ges.GetData("0")

                Dim dt2 As DataTable

                dt2 = Ges

                Dim G As New Microsoft.Reporting.WebForms.ReportDataSource
                G = New Microsoft.Reporting.WebForms.ReportDataSource("Dataset_Operacion_Sp_Reporte_Operaciones_Detalle_Gestion", dt2)

                ReportViewer1.LocalReport.DataSources.Add(G)


            End If



            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class
