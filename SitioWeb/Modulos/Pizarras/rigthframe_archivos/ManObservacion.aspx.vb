Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_ManObservacion
    Inherits System.Web.UI.Page


    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim CD As New ClaseControlDual

    Protected Sub Btn_GuardarObs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_GuardarObs.Click

        Try

            Dim agt As New Perfiles.Cls_Principal

            'If Request.QueryString("Est") = 1 Then
            '    Msj.Mensaje(Page, "Atencion", "Esta operación ya esta aprobada", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If

            'If Request.QueryString("Est") = 2 Then
            '    Msj.Mensaje(Page, "Atencion", "Esta operación ya esta rechazada", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If

           
            Select Case HF_Estado.Value

                Case 1

                    'If Not agt.ValidaAccesso(20, 20110101, Usr, "PRESIONO GUARDAR ORGANIGRAMA") Then
                    '    Msj.Mensaje(Me.Page, "Observación de Aprobación", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If
                    'Se valida que se ingrese observacion
                    If Txt_Observacion.Text = "" Then
                        Msj.Mensaje(Page, "Observación de Aprobación", "Debe ingresar observación", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    Msj.Mensaje(Me.Page, "Observación de Aprobación", "¿Esta seguro de aprobar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Accion.UniqueID)

                Case 2


                    'If Not agt.ValidaAccesso(20, 20110101, Usr, "PRESIONO GUARDAR ORGANIGRAMA") Then
                    '    Msj.Mensaje(Me.Page, "Observación de Rechazo", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If

                    If Txt_Observacion.Text = "" Then
                        Msj.Mensaje(Page, "Observación de Rechazo", "Debe ingresar observación", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    Msj.Mensaje(Me.Page, "Observación de Rechazo", "¿Esta seguro de rechazar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Accion.UniqueID)

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Btn_GuardarObs.Enabled = False

            If Request.QueryString("Estado") <> "" And Request.QueryString("clasificacion") <> "" And NroNegociacion > 0 Then

                HF_Estado.Value = Request.QueryString("Estado").Trim
                HF_NroNeg.Value = NroNegociacion
                HF_Clasificacion.Value = Request.QueryString("clasificacion").Trim

                Select Case HF_Estado.Value
                    Case 1
                        Label_Obs.Text = "Observación de Aprobación"
                        Btn_GuardarObs.Text = "Aprobar"
                        'Btn_GuardarObs_ConfirmButtonExtender.ConfirmText = "¿Esta Seguro de Querer Aprobar?"
                    Case 2
                        Label_Obs.Text = "Observación de Rechazo"
                        Btn_GuardarObs.Text = "Rechazar"
                        'Btn_GuardarObs_ConfirmButtonExtender.ConfirmText = "¿Esta Seguro de Querer Rechazar?"
                End Select

                Btn_GuardarObs.Enabled = True

            Else
                RW.ClosePag(Me)
            End If

        End If

    End Sub

    Protected Sub LB_Accion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Accion.Click

        Dim AG As New ActualizacionesGenerales

        'PFL EN DURO(1)


        If CD.AprobacionUpdate(HF_NroNeg.Value, HF_Clasificacion.Value, HF_Estado.Value, Txt_Observacion.Text.Trim, Pfl, CodEje) Then

            Select Case HF_Estado.Value
                Case 1
                    Msj.Mensaje(Me.Page, "Observación de Aprobación", "Aprobación guardada, puede cerrar la pagina", ClsMensaje.TipoDeMensaje._Informacion)
                Case 2
                    Msj.Mensaje(Me.Page, "Observación de Rechazo", "Rechazo guardado, puede cerrar la pagina", ClsMensaje.TipoDeMensaje._Informacion)
            End Select

            'RW.CloseModal(Me, "ctl00$ContentPlaceHolder1$LB_buscar_frm")
            RW.ClosePag(Me)

        End If



    End Sub

End Class
