Imports CapaDatos
Partial Class Modulos_Pizarras_rigthframe_archivos_DetalleFlujo
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim CMC As New ClaseComercial
    Dim OP As New ClaseOperaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Request.QueryString("rut").Trim <> "" Then

                If Request.QueryString("neg") <> "" Then
                    Response.Write(sb_Negociacion)

                ElseIf Request.QueryString("apb") <> "" Then
                    Response.Write(sb_Aprobaciones)

                ElseIf Request.QueryString("sim") <> "" Then
                    Response.Write(sb_Simulacion)

                ElseIf Request.QueryString("oto") <> "" Then
                    Response.Write(sb_Otorgamiento)
                End If

            End If

        End If

    End Sub

    Private Function sb_Negociacion() As String

        Dim sb As New StringBuilder()
        Dim FC As New FuncionesGenerales.FComunes


        sb.Append("{'Negociación':{ 'Items': [")


        Dim Col As opn_cls

        Col = CMC.NegociacionDevuelve(Request.QueryString("rut").Trim, Request.QueryString("neg").Trim)

        If Not IsNothing(Col) Then

            Dim tiempo As Integer = DateDiff(DateInterval.Minute, CDate(Col.eva_cls.eva_fec_cre), CDate(Col.opn_fec))

            sb.Append("{'Fecha': '")
            sb.Append(Col.opn_fec)
            sb.Append("', 'Transcurrido': '")
            sb.Append(FC.transforma_minutos_en_hora_minuto(tiempo))
            sb.Append("', 'Ejecutivo': '")
            sb.Append(Col.eje_cls.eje_des_cra.Trim)
            sb.Append("'}, ")

            sb = sb.Remove(sb.Length - 1, 1)
            sb.Append("] }}")

        End If

        Return sb.ToString()

    End Function

    Private Function sb_Aprobaciones() As String

        Dim sb As New StringBuilder()
        Dim FC As New FuncionesGenerales.FComunes


        sb.Append("{'Negociación':{ 'Items': [")


        Dim Col As opn_cls

        Col = CMC.NegociacionDevuelve(Request.QueryString("rut").Trim, Request.QueryString("neg").Trim)

        If Not IsNothing(Col) Then

            Dim tiempo As Integer = DateDiff(DateInterval.Minute, CDate(Col.eva_cls.eva_fec_cre), CDate(Col.opn_fec))

            sb.Append("{'Fecha': '")
            sb.Append(Col.opn_fec)
            sb.Append("', 'Transcurrido': '")
            sb.Append(FC.transforma_minutos_en_hora_minuto(tiempo))
            sb.Append("', 'Ejecutivo': '")
            sb.Append(Col.eje_cls.eje_des_cra.Trim)
            sb.Append("'}, ")

            sb = sb.Remove(sb.Length - 1, 1)
            sb.Append("] }}")

        End If

        Return sb.ToString()

    End Function

    Private Function sb_Simulacion() As String

        Dim sb As New StringBuilder()
        Dim FC As New FuncionesGenerales.FComunes


        sb.Append("{'Simulacion':{ 'Items': [")


        Dim Col As ope_cls

        Col = OP.OperacionDevuelve(Request.QueryString("rut").Trim, Request.QueryString("sim").Trim)

        If Not IsNothing(Col) Then

            Dim tiempo As Integer = DateDiff(DateInterval.Minute, CDate(Col.opn_cls.opn_fec), CDate(Col.ope_fec_sim))

            sb.Append("{'Fecha': '")
            sb.Append(Col.ope_fec_sim)
            sb.Append("', 'Transcurrido': '")
            sb.Append(FC.transforma_minutos_en_hora_minuto(tiempo))
            sb.Append("', 'Ejecutivo': '")
            sb.Append(Col.eje_cls.eje_des_cra.Trim)
            sb.Append("'}, ")

            sb = sb.Remove(sb.Length - 1, 1)
            sb.Append("] }}")

        End If

        Return sb.ToString()

    End Function

    Private Function sb_Otorgamiento() As String

        Dim sb As New StringBuilder()
        Dim FC As New FuncionesGenerales.FComunes


        sb.Append("{'Otorgamiento':{ 'Items': [")


        Dim Col As opo_cls

        Col = CMC.OperacionPorId_ope_Devuelve(Request.QueryString("oto").Trim)

        If Not IsNothing(Col) Then

            Dim tiempo As Integer = DateDiff(DateInterval.Minute, CDate(Col.ope_cls.ope_fec_sim), CDate(Col.opo_fec_oto))

            sb.Append("{'Fecha': '")
            sb.Append(Col.opo_fec_oto)
            sb.Append("', 'Transcurrido': '")
            sb.Append(FC.transforma_minutos_en_hora_minuto(tiempo))
            sb.Append("', 'Ejecutivo': '")
            sb.Append(Col.eje_cls.eje_des_cra.Trim)
            sb.Append("'}, ")

            sb = sb.Remove(sb.Length - 1, 1)
            sb.Append("] }}")

        End If

        Return sb.ToString()

    End Function


    Public Function FirmasDevuelveAprobadas(ByVal NroNeg As Integer)

        

    End Function

End Class
