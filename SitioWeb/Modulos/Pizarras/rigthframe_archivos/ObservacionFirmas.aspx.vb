Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_ObservacionFirmas
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim CD As New ClaseControlDual
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("id").Trim <> "" Then

                Response.Write(DSToJSON)

            End If
        End If
    End Sub

    Private Function DSToJSON() As String

        Dim sb As New StringBuilder()


        sb.Append("{'Observacion':{ 'Items': [")


        Dim APB As apb_cls


        APB = CD.AprobacionDevuelve(Request.QueryString("id").Trim)

        sb.Append("{'Observacion': '")

        If IsNothing(APB.apb_obs_apb) Then
            sb.Append(APB.apb_obs_apb)
        Else
            sb.Append(APB.apb_obs_apb.ToUpper)
        End If

        sb.Append("', 'Fecha': '")
        sb.Append(APB.apb_fec_apb)

        sb.Append("', 'Usuario': '")
        If IsNothing(APB.eje_cls) Then
            sb.Append("")
        Else
            sb.Append(APB.eje_cls.eje_des_cra.ToUpper)
        End If

        sb.Append("'}, ")

        sb = sb.Remove(sb.Length - 1, 1)
        sb.Append("] }}")


        Return sb.ToString()

    End Function

End Class
