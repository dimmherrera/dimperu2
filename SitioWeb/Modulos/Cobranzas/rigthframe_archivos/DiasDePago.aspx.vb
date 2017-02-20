Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Partial Class Modulos_Cobranzas_rigthframe_archivos_DiasDePago
    Inherits System.Web.UI.Page

    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim DiasPago As New dpa_cls
    Dim Caption As String = "Días de Pago"
    Dim Msj As New ClsMensaje
    Dim CBZ As New ClaseCobranza
#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1

            If Not IsPostBack Then

                Sesion.RutCli = Request.QueryString("RutCli")
                Sesion.RutDeu = Request.QueryString("RutDeu")

                IniciarPantalla()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            IniciarPantalla()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de Grabar?", TipoDeMensaje._Confirmacion, LinkB_Guardar.UniqueID, False)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            ClosePag(Me)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub LinkB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Guardar.Click
        Try
            DiasPagoGuardar()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Procedimientos y Funciones Generales"

    Private Sub IniciarPantalla()
        Try
            LimpiaPantalla()

            DiasPago = CBZ.DiasPagoDevuelve(Sesion.RutCli, Sesion.RutDeu)
            If Not IsNothing(DiasPago) Then
                'Lunes
                If Trim(DiasPago.dpa_hor_lun) <> "" Then Chck_Lunes.Checked = True
                If Trim(DiasPago.dpa_res_lun) <> "" And Trim(DiasPago.dpa_res_lun) <> "S" Then Chck_Lunes.Checked = True
                Txt_HorLun.Text = DiasPago.dpa_hor_lun
                Select Case Trim(DiasPago.dpa_res_lun)
                    Case "A"
                        Rbt_AM_LU.Checked = True

                    Case "P"
                        Rbt_PM_LU.Checked = True

                    Case "S"
                        Rbt_AM_LU.Checked = False
                        Rbt_PM_LU.Checked = False
                End Select

                'Martes
                If Trim(DiasPago.dpa_hor_mar) <> "" Then Chck_Martes.Checked = True
                If Trim(DiasPago.dpa_res_mar) <> "" And Trim(DiasPago.dpa_res_mar) <> "S" Then Chck_Martes.Checked = True
                Txt_HorMar.Text = DiasPago.dpa_hor_mar
                Select Case Trim(DiasPago.dpa_res_mar)
                    Case "A"
                        Rbt_AM_MA.Checked = True

                    Case "P"
                        Rbt_PM_MA.Checked = True

                    Case "S"
                        Rbt_AM_MA.Checked = False
                        Rbt_PM_MA.Checked = False
                End Select

                'Miercoles
                If Trim(DiasPago.dpa_hor_mie) <> "" Then Chck_Miercoles.Checked = True
                If Trim(DiasPago.dpa_res_mie) <> "" And Trim(DiasPago.dpa_res_mie) <> "S" Then Chck_Miercoles.Checked = True
                Txt_HorMie.Text = DiasPago.dpa_hor_mie
                Select Case Trim(DiasPago.dpa_res_mie)
                    Case "A"
                        Rbt_AM_MI.Checked = True

                    Case "P"
                        Rbt_PM_MI.Checked = True

                    Case "S"
                        Rbt_AM_MI.Checked = False
                        Rbt_PM_MI.Checked = False
                End Select

                'Jueves
                If Trim(DiasPago.dpa_hor_jue) <> "" Then Chck_Jueves.Checked = True
                If Trim(DiasPago.dpa_res_jue) <> "" And Trim(DiasPago.dpa_res_jue) <> "S" Then Chck_Jueves.Checked = True
                Txt_HorJue.Text = DiasPago.dpa_hor_jue
                Select Case Trim(DiasPago.dpa_res_jue)
                    Case "A"
                        Rbt_AM_JU.Checked = True

                    Case "P"
                        Rbt_PM_JU.Checked = True

                    Case "S"
                        Rbt_AM_JU.Checked = False
                        Rbt_PM_JU.Checked = False
                End Select

                'Viernes
                If Trim(DiasPago.dpa_hor_vie) <> "" Then Chck_Viernes.Checked = True
                If Trim(DiasPago.dpa_res_vie) <> "" And Trim(DiasPago.dpa_res_vie) <> "S" Then Chck_Viernes.Checked = True
                Txt_HorVie.Text = DiasPago.dpa_hor_vie
                Select Case Trim(DiasPago.dpa_res_lun)
                    Case "A"
                        Rbt_AM_VI.Checked = True

                    Case "P"
                        Rbt_PM_VI.Checked = True

                    Case "S"
                        Rbt_AM_VI.Checked = False
                        Rbt_PM_VI.Checked = False
                End Select

            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Sub LimpiaPantalla()
        Try
            'Lunes
            Chck_Lunes.Checked = False
            Txt_HorLun.Text = ""
            Rbt_AM_LU.Checked = False
            Rbt_PM_LU.Checked = False

            'Martes
            Chck_Martes.Checked = False
            Txt_HorMar.Text = ""
            Rbt_AM_MA.Checked = False
            Rbt_PM_MA.Checked = False

            'Miercoles
            Chck_Miercoles.Checked = False
            Txt_HorMie.Text = ""
            Rbt_AM_MI.Checked = False
            Rbt_PM_MI.Checked = False

            'Jueves
            Chck_Jueves.Checked = False
            Txt_HorJue.Text = ""
            Rbt_AM_JU.Checked = False
            Rbt_PM_JU.Checked = False

            'Viernes
            Chck_Viernes.Checked = False
            Txt_HorVie.Text = ""
            Rbt_AM_VI.Checked = False
            Rbt_PM_VI.Checked = False

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Sub DiasPagoGuardar()
        Try
            CargaDiasPago()
            If AG.DiasPagoActualiza(DiasPago) Then
                Msj.Mensaje(Me.Page, Caption, "Días de pago ingresados", TipoDeMensaje._Exclamacion)
            Else
                Msj.Mensaje(Me.Page, Caption, "Días de pago no ingresados", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Sub CargaDiasPago()
        Try
            'Rut Deudor
            DiasPago.deu_ide = Sesion.RutDeu

            'Lunes
            DiasPago.dpa_hor_lun = Trim(Txt_HorLun.Text)
            If Rbt_AM_LU.Checked Then
                DiasPago.dpa_res_lun = "A"
            ElseIf Rbt_PM_LU.Checked Then
                DiasPago.dpa_res_lun = "P"
            Else
                DiasPago.dpa_res_lun = "S"
            End If

            'Martes
            DiasPago.dpa_hor_mar = Trim(Txt_HorMar.Text)
            If Rbt_AM_MA.Checked Then
                DiasPago.dpa_res_mar = "A"
            ElseIf Rbt_PM_MA.Checked Then
                DiasPago.dpa_res_mar = "P"
            Else
                DiasPago.dpa_res_mar = "S"
            End If

            'Miercoles
            DiasPago.dpa_hor_mie = Trim(Txt_HorMie.Text)
            If Rbt_AM_MI.Checked Then
                DiasPago.dpa_res_mie = "A"
            ElseIf Rbt_PM_MI.Checked Then
                DiasPago.dpa_res_mie = "P"
            Else
                DiasPago.dpa_res_mie = "S"
            End If

            'Jueves
            DiasPago.dpa_hor_jue = Trim(Txt_HorJue.Text)
            If Rbt_AM_JU.Checked Then
                DiasPago.dpa_res_jue = "A"
            ElseIf Rbt_PM_JU.Checked Then
                DiasPago.dpa_res_jue = "P"
            Else
                DiasPago.dpa_res_jue = "S"
            End If

            'Viernes
            DiasPago.dpa_hor_vie = Trim(Txt_HorVie.Text)
            If Rbt_AM_VI.Checked Then
                DiasPago.dpa_res_vie = "A"
            ElseIf Rbt_PM_VI.Checked Then
                DiasPago.dpa_res_vie = "P"
            Else
                DiasPago.dpa_res_vie = "S"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

  
End Class
