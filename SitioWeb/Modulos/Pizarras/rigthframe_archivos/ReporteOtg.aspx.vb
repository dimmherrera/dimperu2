Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports Microsoft.Reporting.WebForms
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports System.Transactions
Imports System.IO

Partial Class Modulos_Pizarras_rigthframe_archivos_ReporteOtg
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    Dim Caption As String = "Reportes de Otorgamiento"
    Dim Msj As New ClsMensaje
    Dim cli As New ClaseClientes
    Dim evaluacion As Integer
    Dim CMC As New ClaseComercial
    Dim CA As New ClaseArchivos
    Dim url As String
#End Region

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click

        Select Case Me.DP_Reporte.SelectedIndex

            Case 1
                AbrePopup(Me, 1, "../Reportes/report_otg.aspx?NroOperacion=" & Request.QueryString("NroOperacion").Trim & "", "RepOtor", 1280, 1024, 0, 0)
            Case 2

                'Evaluacion
                Dim abytFileData As Byte() = CA.DespliegaArchivoPDF(HF_NroEva.Value)

                If abytFileData.Length <> 0 Then

                    Response.Buffer = False
                    Response.Expires = -1
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("Content-Type", "application/pdf")
                    Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                    Response.AddHeader("Content-Disposition", "attachment;filename=Evaluacion_" & Request.QueryString("Numero") & ".pdf")
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(abytFileData)
                    Response.End()
                Else
                    IB_Informe.Attributes.Add("onClick", "javascript:SendToPdf('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx'," & HF_NroEva.Value & "," & 2 & ");")
                End If

            Case 3
                'Negociación
                Dim abytFileData As Byte() = CA.DespliegaArchivoNegPDF(Request.QueryString("Numero").Trim)

                If abytFileData.Length <> 0 Then

                    Dim archivo As String = "Negociacion_" & Request.QueryString("Numero") & ".pdf"

                    Response.Buffer = False
                    Response.Expires = -1
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("Content-Type", "application/pdf")
                    Response.AddHeader("Content-Length", abytFileData.Length.ToString)
                    Response.AddHeader("Content-Disposition", "attachment;filename=" & archivo & "")
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(abytFileData)
                    Response.End()
                Else
                    IB_Informe.Attributes.Add("onClick", "javascript:SendToPdf('../../Carp. Comercial/rigthframe_archivos/OpenPDF.aspx'," & Request.QueryString("Numero").Trim & "," & 1 & ");")
                End If

        End Select

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1
            If Request.QueryString("rut_cli").Trim <> "" And Request.QueryString("NroOperacion").Trim <> "" Then
                NegoRetorna()
            End If
        End If



    End Sub

    Private Sub NegoRetorna()
        Dim NEG As opn_cls

        NEG = CMC.NegociacionRetorna(Request.QueryString("Numero").Trim)

        HF_NroEva.Value = NEG.id_eva

    End Sub

End Class
