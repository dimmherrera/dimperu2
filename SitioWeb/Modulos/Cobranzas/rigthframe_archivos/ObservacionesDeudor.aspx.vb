Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_ObservacionesDeudor
    Inherits System.Web.UI.Page

    Dim Caption As String = "Observación Pagador"
    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim Deu As New deu_cls
    Dim Msj As New ClsMensaje
    Dim cbz As New ClaseCobranza
#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            If Not IsPostBack Then
                Sesion.RutCli = Request.QueryString("RutCli")
                Sesion.RutDeu = Request.QueryString("RutDeu")

                ObservacionDevuelve()

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not IsDate(Txt_FechaAct.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha incorrecta", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_FechaAct.Focus()
                Exit Sub
            End If

            If Txt_Obs.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese observación", TipoDeMensaje._Exclamacion, Nothing, False)
                Exit Sub
            End If

            If cbz.ObservacionDeudorUpdate(Sesion.RutDeu, Txt_FechaAct.Text, Txt_Obs.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Observación ingresada", TipoDeMensaje._Exclamacion, Nothing, False)
            Else
                Msj.Mensaje(Me.Page, Caption, "Observación no ingresada", TipoDeMensaje._Exclamacion, Nothing, False)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            ClosePag(Me.Page)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

#Region "Procedimientos y Funciones Generales"

    Private Sub ObservacionDevuelve()
        Try
            Deu = CG.DeudorDevuelvePorRut(Sesion.RutDeu)

            If Not IsNothing(Deu) Then
                Txt_FechaAct.Text = Format(Deu.deu_fec_obs, "dd/MM/yyyy")
                If Trim(Txt_FechaAct.Text) = "" Then
                    Txt_FechaAct.Text = Format(Date.Now, "dd/MM/yyyy")
                End If
                Txt_Obs.Text = Deu.deu_obs_gsn
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

End Class
