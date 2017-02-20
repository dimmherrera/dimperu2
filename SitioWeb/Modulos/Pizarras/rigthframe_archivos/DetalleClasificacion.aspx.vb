Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class DetalleClasificacion
    Inherits System.Web.UI.Page
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial
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
        Dim RG As New FuncionesGenerales.FComunes

        sb.Append("{'Clasificacion':{ 'Items': [")

        Dim Col As Collection
        Dim RSC As Object
        Dim LDC As ldc_cls
        Dim NEG As opn_cls
        Dim OPE As ope_cls
        Dim Deuda As Double
        Dim Sobregiro As Double

        If Not IsNothing(LineaCredito) Then
            LDC = LineaCredito
        Else
            LDC = CG.LineaDeCreditoDevuelve(RutCli, 1)
        End If

        If Not IsNothing(ResumenCliente) Then
            RSC = ResumenCliente
        Else
            RSC = CMC.ResumenClienteDevuelve(RutCli, CodEje)
        End If

        If Not IsNothing(Negociacion) Then
            NEG = Negociacion
        Else
            NEG = CMC.NegociacionDevuelve(RutCli, NroNegociacion)
        End If

        If Not IsNothing(Operacion) Then
            OPE = Operacion
        Else
            OPE = OP.OperacionDevuelve(RutCli, NroOperacion)
        End If

        Col = CD.DevuelveDetalleClasificacion(Request.QueryString("id").Trim)

        'Rescato el valor de resumen de linea ocupada vs linea aprobada
        '---------------------------------------------------------------------------------------------------------------
        Dim Mto_Ocupado As Double
        Dim Mto_Aprobado As Double
        Dim Mto_Operacion As Double

        Try

            Mto_Ocupado = RSC.rsc_mto_ocu


        Catch ex As Exception
            Try
                Mto_Aprobado = LDC.ldc_mto_apb
            Catch et As Exception
            End Try
        Finally

            'Asigno porcentaje de deuda a la variables
            Mto_Operacion = NEG.opn_mto_doc * OPE.ope_fac_cam
            Deuda = ((Mto_Ocupado + Mto_Operacion) * 100) / Mto_Aprobado

            If (Mto_Aprobado - (Mto_Ocupado + Mto_Operacion)) < 0 Then
                Sobregiro = ((Mto_Aprobado - (Mto_Ocupado + Mto_Operacion)) * 100) / Mto_Aprobado
            Else
                Sobregiro = 0
            End If

        End Try

        
        For I = 1 To Col.Count

            sb.Append("{'Descripcion': '")
            sb.Append(Col.Item(I).cfc_obs)
            sb.Append("', 'Marca': '")

            Dim Valida As Double = 0

            Select Case Col.Item(I).ID_P_0069

                Case 1 'SPREAD
                    Valida = NEG.opn_spr_ead
                Case 2 'MONTO DE OPERACION
                    Valida = Mto_Operacion
                Case 3 'SOBREGIRO
                    Valida = Sobregiro
                Case 4 'DEUDA VS LINEA
                    Valida = Deuda
                Case 5 'COMISION
                    Valida = NEG.opn_por_com
            End Select

            If Valida >= Col.Item(I).cfc_dde And Valida <= Col.Item(I).cfc_hta Then
                sb.Append(1)
            Else
                sb.Append(2)
            End If


            sb.Append("', 'Desde': '")
            sb.Append(RG.FormatoMiles(Col.Item(I).cfc_dde))
            sb.Append("', 'Hasta': '")
            sb.Append(RG.FormatoMiles(Col.Item(I).cfc_hta))
            sb.Append("'}, ")

        Next


        sb = sb.Remove(sb.Length - 1, 1)
        sb.Append("] }}")


        Return sb.ToString()

    End Function

End Class
