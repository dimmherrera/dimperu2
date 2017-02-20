Imports CapaDatos
Imports ClsSession.ClsSession
Imports ClsSession.SesionProrrogas
Imports FuncionesGenerales

Partial Class Modulos_Prorrogas_rightframe_archivos_PopUpObservacion
    Inherits System.Web.UI.Page

    Dim AG As New ActualizacionesGenerales
    Dim Sesion As New ClsSession.ClsSession
    Dim VAR As New FuncionesGenerales.Variables
    Dim RW As New RutinasWeb
    Dim SesionPro As New ClsSession.SesionProrrogas

    '    Dim Coll_SPG = New Collection

    Protected Sub btn_apro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_apro.Click

        Try

            If txt_apro.Text <> "" Then

                If Request.QueryString("Accion") = 1 Then
                    aprueba()
                ElseIf Request.QueryString("Accion") = 2 Then
                    rechaza()
                ElseIf Request.QueryString("Accion") = 3 Then
                    anula()
                End If

                txt_apro.CssClass = "clsDisabled"
                txt_apro.ReadOnly = True

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Page.ClientScript.GetPostBackClientHyperlink(Me.btn_apro, String.Empty)
        Dim sb As New StringBuilder
        sb.Append(Me.Page.ClientScript.GetPostBackEventReference(Me.btn_apro, vbNullString))
        sb.Append(";")
        btn_apro.Attributes("onclick") = sb.ToString()

        If Not IsPostBack Then
            If Request.QueryString("id") <> "" Then

                If Request.QueryString("Accion") = 1 Then
                    btn_apro.Text = "Aprobar"
                ElseIf Request.QueryString("Accion") = 2 Then
                    btn_apro.Text = "Rechazar"
                End If
                HF_ID.Value = ""
                HF_ID.Value = Request.QueryString("id")
                txt_apro.CssClass = "clsMandatorio"
                txt_apro.ReadOnly = False
            End If
        End If

    End Sub

    Protected Sub aprueba()
        Try

            '--------------------------------------------------------------------------------------------------------------------------------
            'Guarda Validación de Solicitud de Prorroga
            '--------------------------------------------------------------------------------------------------------------------------------
            AG.Prorroga_GuardaProrroga(Coll_SPG.Item(CLng(HF_ID.Value)).id_spg, 2, txt_apro.Text)
            '--------------------------------------------------------------------------------------------------------------------------------

            SesionPro.Accion_Pro = 2

            RW.ClosePag(Me)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rechaza()
        Try
            '--------------------------------------------------------------------------------------------------------------------------------
            'Guarda Validación de Solicitud de Prorroga
            '--------------------------------------------------------------------------------------------------------------------------------
            AG.Prorroga_GuardaProrroga(Coll_SPG.Item(CLng(HF_ID.Value)).id_spg, 3, txt_apro.Text)

            SesionPro.Accion_Pro = 3

            RW.ClosePag(Me)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub anula()
        Try

            '--------------------------------------------------------------------------------------------------------------------------------
            'Guarda Validación de Solicitud de Prorroga
            '--------------------------------------------------------------------------------------------------------------------------------
            AG.Prorroga_GuardaProrroga(Coll_SPG.Item(CLng(HF_ID.Value)).id_spg, 4, txt_apro.Text)
            '--------------------------------------------------------------------------------------------------------------------------------

            SesionPro.Accion_Pro = 4

            RW.ClosePag(Me)
        Catch ex As Exception

        End Try
    End Sub

End Class
