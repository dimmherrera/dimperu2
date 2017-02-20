Imports Microsoft.Reporting
Imports CapaDatos
Imports System.Data

Partial Class Informe_Gestion
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            If Not IsPostBack Then
                If Request.QueryString("rutclidsd") <> "" Then
                    GeneraInforme()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Lb_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_close.Click
        FuncionesGenerales.RutinasWeb.CloseOpener(Me, "InformeCarteraVigMor.aspx")
    End Sub

    Public Sub GeneraInforme()

        Try
            Dim fc As New FuncionesGenerales.FComunes
            Dim gsn As New DataSet_GestionCobranza.sp_Reporte_Devuelve_GestionDataTable
            Dim g As New DataSet_GestionCobranzaTableAdapters.sp_Reporte_Devuelve_GestionTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = ("Modulos/Cobranzas/Reportes/Reporte_Cob.rdlc")

            Dim rutcli_dsd As String
            Dim rutcli_hst As String
            Dim rutdeu_dsd As String
            Dim rutdeu_hst As String
            Dim cob_dsd As Integer
            Dim cob_hst As Integer
            Dim f_gest_dsd As String
            Dim f_gest_hst As String
            Dim tpdoc_dsd As Integer
            Dim tpdoc_hst As Integer
            Dim estcob_dsd As Integer
            Dim estcob_hst As Integer
            Dim moneda As Integer
            Dim segcli_dsd As Integer
            Dim segcli_hst As Integer
            Dim segdeu_dsd As Integer
            Dim segdeu_hst As Integer
            Dim estdocdsd As Integer
            Dim estdochst As Integer

            rutcli_dsd = Format(CLng(Request.QueryString("rutclidsd")), var.FMT_RUT)
            rutcli_hst = Format(CLng(Request.QueryString("rutclihst")), var.FMT_RUT)

            rutdeu_dsd = Format(CLng(Request.QueryString("rutdeudsd")), var.FMT_RUT)
            rutdeu_hst = Format(CLng(Request.QueryString("rutdeuhst")), var.FMT_RUT)

            cob_dsd = Request.QueryString("cobfodsd")
            cob_hst = Request.QueryString("cobfohst")

            f_gest_dsd = Request.QueryString("fdsd").Replace("-", "")
            f_gest_hst = Request.QueryString("fhst").Replace("-", "")

            tpdoc_dsd = Request.QueryString("tpdocdsd")
            tpdoc_hst = Request.QueryString("tpdochst")

            estcob_dsd = Request.QueryString("estcobdsd")
            estcob_hst = Request.QueryString("estcobhst")

            moneda = Request.QueryString("moneda")

            estdocdsd = Request.QueryString("estdocdsd")
            estdochst = Request.QueryString("estdochst")

            'f_gest_dsd = fc.FUNFechaJul(f_gest_dsd)
            'f_gest_hst = fc.FUNFechaJul(f_gest_hst)

            'Dim i As Int32

            'i = g.GetData(rutcli_dsd, rutcli_hst, rutdeu_dsd, rutdeu_hst, cob_dsd, cob_hst, f_gest_dsd, f_gest_hst, tpdoc_dsd, tpdoc_hst, _
            '                estcob_dsd, estcob_hst, moneda, segcli_dsd, segcli_hst, segdeu_dsd, segdeu_hst, estdocdsd, estdochst).Rows.Count

            'If i = 0 Then
            '    MsgBox("no se encuentran registros")
            '    Exit Sub
            'End If

            gsn = g.GetData(rutcli_dsd, rutcli_hst, rutdeu_dsd, rutdeu_hst, cob_dsd, cob_hst, f_gest_dsd, f_gest_hst, tpdoc_dsd, tpdoc_hst, _
                            estcob_dsd, estcob_hst, moneda, estdocdsd, estdochst)

            If gsn.Rows.Count = 0 Then
                Msj.Mensaje(Me, "Atención", "Los parametros de consulta no arrojaron resultados", ClsMensaje.TipoDeMensaje._Informacion, Me.Lb_close.UniqueID, False)
            End If

            Dim dt As DataTable

            dt = gsn

            'gsn = g.GetData("000000000000", "999999999999", "000000000000", "999999999999", 0, 9999, "1900/01/01", "2999/01/01" _
            '                , 0, 9999, 0, 9999, 1, 0, 9999, 0, 9999, 0, 1)

            Dim gestion As New Microsoft.Reporting.WebForms.ReportDataSource

            gestion = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_Reporte_Devuelve_Gestion", dt)
            'gestion = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_GestionCobranza_sp_Reporte_Devuelve_Gestion", gsn)

            ReportViewer1.LocalReport.DataSources.Add(gestion)
            ReportViewer1.DataBind()



        Catch ex As Exception

        End Try
    End Sub


End Class
