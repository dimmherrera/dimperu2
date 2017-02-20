Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ObservacionVB
    Inherits System.Web.UI.Page

    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim CMC As New ClaseComercial

    Protected Sub Btn_GuardarObs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_GuardarObs.Click

        Try

            If Txt_Observacion.Text = "" Then
                Msj.Mensaje(Page, "VB Aplicación", "Ingrese observación", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            Select Case HF_Estado.Value.ToUpper
                Case "S"
                    Msj.Mensaje(Me, "VB Aplicación", "¿Esta seguro de aprobar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)
                Case "N"
                    Msj.Mensaje(Me, "VB Aplicación", "¿Esta seguro de rechazar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)
            End Select

        Catch ex As Exception
            Msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            If Not IsPostBack Then

                Response.Expires = -1
                Btn_GuardarObs.Enabled = False

                If Request.QueryString("Aprobada").Trim <> "" And Request.QueryString("id").Trim <> "" Then

                    HF_Estado.Value = Request.QueryString("Aprobada").Trim
                    HF_NroApl.Value = Request.QueryString("id").Trim

                    Select Case HF_Estado.Value.ToUpper
                        Case "S"
                            Label_Obs.Text = "Observación de Aprobación"
                        Case "N"
                            Label_Obs.Text = "Observación de Rechazo"
                    End Select

                    Btn_GuardarObs.Enabled = True

                Else
                    RW.ClosePag(Me)
                End If

            End If

            'Btn_Volver.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$LB_Refrescar');")
            Btn_Volver.Attributes.Add("onclick", "JavaScript:window.close();")

        Catch ex As Exception
            Msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim AG As New ActualizacionesGenerales

            CMC.Aplicaciones_ApruebaRechaza(HF_NroApl.Value, Txt_Observacion.Text, HF_Estado.Value, Date.Now, CodEje)

            Msj.Mensaje(Me, "VB Aplicacion", "VB Guardado", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)

            Btn_GuardarObs.Enabled = False
            Txt_Observacion.ReadOnly = True
            Txt_Observacion.CssClass = "clsDisabled"

        Catch ex As Exception
            Msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

End Class
