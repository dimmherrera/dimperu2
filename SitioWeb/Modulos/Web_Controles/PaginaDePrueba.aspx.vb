Imports ClsSession.SesionPagos

Imports CapaDatos
Partial Class Modulos_Web_Controles_PaginaDePrueba
    Inherits System.Web.UI.Page
    Dim OP As New ClaseOperaciones
    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Coll_Doctos_Seleccionados = New Collection

            'Dim Pagos As New ClsSession.SesionPagos
            'With Pagos

            '    .IniciarSesionPagos()

            '    .RutCliente = 1
            '    .RutDeudor = 0
            '    .Pagador = "C"
            '    .FechaPago = Date.Now
            '    .DiasRetencionPago = 0
            '    .TasaInteresCalculo = 5.3
            '    .DiasDevolverInteres = 2

            'End With

            'CargaGrillaDoctos()




        End If

    End Sub

    Private Sub CargaGrillaDoctos()

        Try

            Dim Rut_Cli_Desde As Long
            Dim Rut_Cli_Hasta As Long

            Dim Rut_Deu_Desde As Long
            Dim Rut_Deu_Hasta As Long

            Dim TipoDoc_Desde As Integer
            Dim TipoDoc_Hasta As Integer

            Dim NroOtor_Desde As Long
            Dim NroOtor_Hasta As Long

            Dim NroDoct_Desde As String
            Dim NroDoct_Hasta As String

            'Dim EstCobr_Desde As Integer
            'Dim EstCobr_Hasta As Integer

            Dim Fec_Vto_Desde As DateTime
            Dim Fec_Vto_Hasta As DateTime
            Dim Pagos As New ClsSession.SesionPagos

            Select Case Pagos.Pagador
                Case "C"
                    If IsNothing(Pagos.RutCliente) Or Pagos.RutCliente = 0 Then
                        Rut_Cli_Desde = 0
                        Rut_Cli_Hasta = 999999999999
                    Else
                        Rut_Cli_Desde = Pagos.RutCliente
                        Rut_Cli_Hasta = Pagos.RutCliente
                    End If
                Case "D"
                    If IsNothing(Pagos.RutDeudor) Or Pagos.RutDeudor = 0 Then
                        Rut_Deu_Desde = 0
                        Rut_Deu_Hasta = 999999999999
                    Else
                        Rut_Deu_Desde = Pagos.RutDeudor
                        Rut_Deu_Hasta = Pagos.RutDeudor
                    End If
            End Select

            'Criterio de Busqueda

            'Rut Deudor
            Rut_Deu_Desde = 0
            Rut_Deu_Hasta = 999999999999

            'Tipo Docto
            TipoDoc_Desde = 0
            TipoDoc_Hasta = 999

            'Nro Otorgamiento
            NroOtor_Desde = 0
            NroOtor_Hasta = 999999999

            'Nro Documento
            NroDoct_Desde = "0"
            NroDoct_Hasta = 999999999

            'Fecha Vcto
            Fec_Vto_Desde = "01/01/1900"
            Fec_Vto_Hasta = Date.Now.ToShortDateString


            Dim Estado1, Estado2, Estado3, Estado4, Estado5, Estado6, Estado7, Estado8, Estado9, Estado10, Estado11, Estado12 As Integer



            Estado1 = 1
            Estado2 = 2
            Estado3 = 4
            Estado4 = 9
            Estado5 = 11
            Estado6 = 12
            Estado7 = 1
            Estado8 = 1
            Estado9 = 1
            Estado10 = 1
            Estado11 = 1
            Estado12 = 1


            Dim Coll_Obj = OP.DocumentosOtorgagos_RetornaDoctos(Rut_Cli_Desde, Rut_Cli_Hasta, _
                                                                Rut_Deu_Desde, Rut_Deu_Hasta, _
                                                                NroOtor_Desde, NroOtor_Hasta, _
                                                                TipoDoc_Desde, TipoDoc_Hasta, _
                                                                NroDoct_Desde, NroDoct_Hasta, _
                                                                0, 9999, _
                                                                Fec_Vto_Desde, Fec_Vto_Hasta, _
                                                                Estado1, _
                                                                Estado2, _
                                                                Estado3, _
                                                                Estado4, _
                                                                Estado5, _
                                                                Estado6, _
                                                                Estado7, _
                                                                Estado8, _
                                                                Estado9, _
                                                                Estado10, _
                                                                Estado11, _
                                                                Estado12)



            'Gr_Documentos.DataSource = Coll_Obj
            'Gr_Documentos.DataBind()


           
        Catch ex As Exception

        End Try

    End Sub

End Class
