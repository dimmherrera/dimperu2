Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Alertas_rightframe_archivos_ParametrosDeAlertas
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Alertas"
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            Txt_Por_Vencer.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Mora.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Linea.Attributes.Add("Style", "TEXT-ALIGN: right")

            Dim ParAlertas As pta_cls

            'ParAlertas = CG.AlertasParametros_Devuelve(CodEje)

            'Ejecutivo que se selecciona en drop de pantalla alertas
            ParAlertas = CG.AlertasParametros_Devuelve(Ejecutivo)

            If Not IsNothing(ParAlertas) Then

                Txt_Por_Vencer.Text = ParAlertas.pta_dia_ven
                Txt_Mora.Text = ParAlertas.pta_dia_mor
                Txt_Linea.Text = ParAlertas.pta_dia_lin
                HF_Accion.Value = "MODIFICA"

            Else

                HF_Accion.Value = "INSERTA"

            End If

        End If

        IB_Volver.Attributes.Add("onClick", "javacript:window.close();")

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20040504, Usr, "PRESIONO EXPORTAR ALERTAS") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim pta As New pta_cls

            With pta
                .id_eje = Ejecutivo
                .pta_dia_ven = Val(Txt_Por_Vencer.Text)
                .pta_dia_mor = Val(Txt_Mora.Text)
                .pta_dia_lin = Val(Txt_Linea.Text)
            End With

            Select Case HF_Accion.Value
                Case "INSERTA"
                    If AG.AlertasParametro_Inserta(pta) Then
                        Msj.Mensaje(Me, Caption, "Parámetros Ingresados", TipoDeMensaje._Informacion, Nothing, False)
                        IB_Guardar.Focus()
                    End If
                Case "MODIFICA"
                    If AG.AlertasParametro_Modifica(pta) Then
                        Msj.Mensaje(Me, Caption, "Parámetros Actualizados", TipoDeMensaje._Informacion, Nothing, False)
                        IB_Guardar.Focus()
                    End If
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Txt_Por_Vencer.Text = ""
        Txt_Mora.Text = ""
        Txt_Linea.Text = ""
    End Sub

End Class
