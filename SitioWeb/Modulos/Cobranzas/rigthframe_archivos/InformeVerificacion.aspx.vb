Imports Microsoft.Reporting.WebForms
Imports CapaDatos
Imports System.Data

Partial Class Modulos_Cobranzas_Reportes_InformeVerificacion
    Inherits System.Web.UI.Page

#Region "Eventos"

    Dim cg As New CapaDatos.ConsultasGenerales

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1

            If Not IsPostBack Then
                If Request.QueryString("REGISTRO") = "" Then
                    Genera_reporte()
                Else
                    Genera_reporteRegistro()
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Procedimientos y funciones Generales"

    Public Sub Genera_reporte()

        Try

            Dim DocAVer As New DataSetVerificacion.Sp_Reporte_Documentos_A_VerificarDataTable

            Dim Tab_DocVer As New DataSetVerificacionTableAdapters.Sp_Reporte_Documentos_A_VerificarTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()

            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\ReporteVerificacion.rdlc"


            Dim RutCli As String
            Dim RutDeudsd As String
            Dim Rutdeuhst As String
            Dim FecIni As String
            Dim FecFin As String
            Dim TipDoc1 As Integer
            Dim TipDoc2 As Integer

            RutCli = Request.QueryString("RutCli")
            RutDeudsd = Request.QueryString("RutDeudsd")
            Rutdeuhst = Request.QueryString("RutDeuhst")
            FecIni = Request.QueryString("FecIni")
            FecFin = Request.QueryString("FecFin")
            TipDoc1 = Request.QueryString("TiDoc1")

            If TipDoc1 = "0" Then
                TipDoc1 = "0"
                TipDoc2 = 999
            Else
                TipDoc2 = TipDoc1
            End If


            'MANEJO FACTORES DE CAMBIOS
            Dim UF As Double
            Dim DOLAR As Double
            Dim EURO As Double


            Dim coll_par As New Collection
            coll_par = cg.ParidadesDelDiaDevuelve(Date.Now)

            For S = 1 To coll_par.Count

                If coll_par.Item(S).id_p_0023 = 2 Then

                    UF = coll_par.Item(S).par_val

                ElseIf coll_par.Item(S).id_p_0023 = 3 Then

                    DOLAR = coll_par.Item(S).par_val

                ElseIf coll_par.Item(S).id_p_0023 = 4 Then

                    EURO = coll_par.Item(S).par_val

                End If

            Next

            DocAVer = Tab_DocVer.GetData(RutCli, RutDeudsd, Rutdeuhst, FecIni, FecFin, TipDoc1, TipDoc2)

            Dim X As Integer

            For X = 0 To DocAVer.Count - 1

                If DocAVer.Item(X).TipoMon = 2 Then

                    DocAVer.Item(X).Monto = DocAVer.Item(X).Monto * UF

                ElseIf DocAVer.Item(X).TipoMon = 3 Then

                    DocAVer.Item(X).Monto = DocAVer.Item(X).Monto * DOLAR

                ElseIf DocAVer.Item(X).TipoMon = 3 Then

                    DocAVer.Item(X).Monto = DocAVer.Item(X).Monto * EURO

                End If


                If DocAVer.Item(X).TipoVer = 1 Then
                    DocAVer.Item(X).mto1 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 2 Then
                    DocAVer.Item(X).mto2 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 3 Then
                    DocAVer.Item(X).mto3 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 4 Then
                    DocAVer.Item(X).mto4 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 5 Then
                    DocAVer.Item(X).mto5 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 6 Then
                    DocAVer.Item(X).mto6 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 7 Then
                    DocAVer.Item(X).mto7 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 8 Then
                    DocAVer.Item(X).mto8 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 9 Then
                    DocAVer.Item(X).mto9 = DocAVer.Item(X).Monto
                ElseIf DocAVer.Item(X).TipoVer = 10 Then
                    DocAVer.Item(X).mto10 = DocAVer.Item(X).Monto

                End If
            Next

            Dim dt As DataTable

            dt = DocAVer

            Dim I As New Microsoft.Reporting.WebForms.ReportDataSource

            I = New Microsoft.Reporting.WebForms.ReportDataSource("DataSetVerificacion_Sp_Reporte_Documentos_A_Verificar", dt)

            ReportViewer1.LocalReport.DataSources.Add(I)

            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Genera_reporteRegistro()

        Try


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = "Modulos\Cobranzas\Reportes\RegistroConfirmacionAceptacion.rdlc"

            Dim RutCli As String
            Dim RutDeudsd As String
            Dim Rutdeuhst As String
            Dim FecIni As String
            Dim FecFin As String
            Dim TipDoc1 As Integer
            Dim TipDoc2 As Integer

            RutCli = Request.QueryString("RutCli")
            RutDeudsd = Request.QueryString("RutDeudsd")
            Rutdeuhst = Request.QueryString("RutDeuhst")
            FecIni = Request.QueryString("FecIni")
            FecFin = Request.QueryString("FecFin")
            TipDoc1 = Request.QueryString("TiDoc1")


            'Dim I As New Microsoft.Reporting.WebForms.ReportDataSource
            'I = New Microsoft.Reporting.WebForms.ReportDataSource("DataSetVerificacion_Sp_Reporte_Documentos_A_Verificar", DocAVer)

            'ReportViewer1.LocalReport.DataSources.Add(I)

            ReportViewer1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class
