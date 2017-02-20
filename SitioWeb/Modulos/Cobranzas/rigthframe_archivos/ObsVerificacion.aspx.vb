Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_ObsVerificacion
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim CBZ As New ClaseCobranza

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Request.QueryString("id").Trim <> "" Then
                Response.Write(DSToJSON)
            End If

        End If

    End Sub

    Private Function DSToJSON() As String

        Dim sb As New StringBuilder()
        Dim RG As New FuncionesGenerales.FComunes
        Dim dsi As dsi_cls
        Dim Dvf As dvf_cls

        sb.Append("{'Verificacion':{ 'Items': [")

        dsi = CBZ.Retorna_id_dsi(Request.QueryString("id").Trim)
        Dvf = CBZ.rescata_dvf(dsi.deu_ide, dsi.dsi_num, dsi.dsi_mto, dsi.ope_cls.opn_cls.id_P_0031)

        If IsNothing(Dvf) = False Then

            sb.Append("{'Estado': '")
            sb.Append(Dvf.P_0040_cls.pnu_des)
            sb.Append("', 'Observacion': '")
            sb.Append(Dvf.dvf_obs)
            Dim Obs As String = ""

            If Dvf.dvf_obs <> "" Then
                Obs = Trim(Dvf.dvf_obs)
                If Dvf.dvf_obs_001 <> "" Then
                    Obs &= Trim(Dvf.dvf_obs_001)
                End If
            End If

            sb.Append(Obs)
            sb.Append("'}, ")

        Else

            sb.Append("{'Estado': '")
            sb.Append("")
            sb.Append("', 'Observacion': '")
            sb.Append("")
            sb.Append("'}, ")

        End If

        sb = sb.Remove(sb.Length - 1, 1)
        sb.Append("] }}")

        Return sb.ToString()

    End Function
End Class
