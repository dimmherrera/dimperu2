Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports System.IO
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_RadicarFact
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim agt As New Perfiles.Cls_Principal
    Dim RG As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim Cob As New ClaseCobranza
    
#End Region

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Txt_feg.Text = Format(Date.Now, "dd/MM/yyyy")
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click

        If Txt_feg.Text <> "" Then
            If IsDate(Txt_feg.Text) Then
                If Cob.ExistenDoctosRad(Txt_feg.Text) Then
                    RW.AbrePopup(Me, 1, "Informes_cbz.aspx?feg=" & Txt_feg.Text.Trim & "&tipo=" & 4 & "", "Reportefacturasradicadas", 1000, 900, 10, 10)
                Else
                    Msj.Mensaje(Page, "Radicar Facturas", "No existe facturas a radicar segun criterio", TipoDeMensaje._Exclamacion)
                End If
            Else
                Msj.Mensaje(Page, "Radicar Facturas", "Debe ingresar fecha valida", TipoDeMensaje._Exclamacion)
                Txt_feg.Text = Format(Date.Now, "dd/MM/yyyy")
                Txt_feg.Focus()
            End If
        Else
            Msj.Mensaje(Page, "Radicar Facturas", "Debe ingresar fecha de generacion", TipoDeMensaje._Exclamacion)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Txt_feg.Text = Format(Date.Now, "dd/MM/yyyy")

        End If
    End Sub
End Class
