Imports CapaDatos
Imports System.Data

Partial Class Modulos_Cobranzas_rigthframe_archivos_InformeCarteraVigMor
    Inherits System.Web.UI.Page
    Dim Msj As New ClsMensaje
    Dim rw As New FuncionesGenerales.RutinasWeb

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Response.Expires = -1
                Genera_reporte()
            End If
        Catch ex As Exception

        End Try
        Lb_close.Attributes.Add("onClick", "javascript:window.close();")
    End Sub

#End Region

#Region "Procedimientos y funciones Generales"

    Public Sub Genera_reporte()

        Try
            Dim Var As New FuncionesGenerales.Variables

            Dim CartVigMor As New DataSetVerificacion.Sp_Reporte_Co_Cartera_Morosa_SucDataTable

            Dim Tab_CartVigMor As New DataSetVerificacionTableAdapters.Sp_Reporte_Co_Cartera_Morosa_SucTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()

            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\ReporteCarteraVigMor.rdlc"

            Dim FecInf As String
            Dim Estado As Integer, Moneda As Integer
            Dim id_suc1 As String, id_suc2 As String
            Dim id_eje1 As Integer, id_eje2 As Integer
            Dim CodCob1 As String, CodCob2 As String
            Dim TipDoc1 As Integer, TipDoc2 As Integer
            Dim NroOto1 As Long, NroOto2 As Long
            Dim NroDoc1 As String, nrodoc2 As String
            Dim RutCli1 As String, RutCli2 As String
            Dim RutDeu1 As String, RutDeu2 As String
            Dim TipoConsulta As Integer

            FecInf = Format(CDate(Request.QueryString("FecInf")), "yyyyMMdd")
            Estado = Request.QueryString("Estado")
            Moneda = Request.QueryString("Moneda")
            id_suc1 = Request.QueryString("id_suc1")
            id_suc2 = Request.QueryString("id_suc2")
            id_eje1 = Request.QueryString("id_eje1")
            id_eje2 = Request.QueryString("id_eje2")
            CodCob1 = Request.QueryString("CodCob1")
            CodCob2 = Request.QueryString("CodCob2")
            TipDoc1 = Request.QueryString("TipDoc1")
            TipDoc2 = Request.QueryString("TipDoc2")
            NroOto1 = Request.QueryString("NroOto1")
            NroOto2 = Request.QueryString("NroOto2")
            NroDoc1 = Request.QueryString("NroDoc1")
            nrodoc2 = Request.QueryString("NroDoc2")
            RutCli1 = Format(CLng(Request.QueryString("RutCli1")), Var.FMT_RUT)
            RutCli2 = Format(CLng(Request.QueryString("RutCli2")), Var.FMT_RUT)
            RutDeu1 = Format(CLng(Request.QueryString("RutDeu1")), Var.FMT_RUT)
            RutDeu2 = Format(CLng(Request.QueryString("RutDeu2")), Var.FMT_RUT)
            TipoConsulta = Request.QueryString("TipoConsulta")




            CartVigMor = Tab_CartVigMor.GetData(TipoConsulta, id_suc1, id_suc2, id_eje1, id_eje2, CodCob1, CodCob2, _
                                                TipDoc1, TipDoc2, NroOto1, NroOto2, NroDoc1, nrodoc2, FecInf, Estado, _
                                                Moneda, RutCli1, RutDeu1, RutCli2, RutDeu2)

            Dim dt As DataTable

            dt = CartVigMor



            If CartVigMor.Rows.Count = 0 Then
                Msj.Mensaje(Me, "Atención", "Los parametros de consulta no arrojaron resultados", ClsMensaje.TipoDeMensaje._Informacion, Me.Lb_close.UniqueID, False)
            End If

            Dim I As New Microsoft.Reporting.WebForms.ReportDataSource

            I = New Microsoft.Reporting.WebForms.ReportDataSource("DataSetVerificacion_Sp_Reporte_Co_Cartera_Morosa_Suc", dt)

            ReportViewer1.LocalReport.DataSources.Add(I)

            ReportViewer1.DataBind()

        Catch ex As Exception
            Response.Write(ex)
        End Try

    End Sub

#End Region
    Protected Sub Lb_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_close.Click
        FuncionesGenerales.RutinasWeb.CloseOpener(Me, "InformeCarteraVigMor.aspx")
    End Sub

End Class
