Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_Preliminar
    Inherits System.Web.UI.UserControl

    Dim CG As New ConsultasGenerales

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub CargaDrop()

        CG.ParametrosDevuelve(TablaParametro.TipoPagare, True, Dp_Tipo)
        CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Moneda)
        CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_TipoDocto)
        CG.ParametrosDevuelve(TablaParametro.TipoEnvio, True, Dp_TipoEnvio)

        CG.ParametrosDevuelve(TablaParametro.EstadoVerificacion, True, Dp_Estado_Ver)

        'reemplazar por el codigo del ejecutivo verificador
        CG.EjecutivosDevuelve(Dp_Verificador, CodEje, 1)


    End Sub

    Private Sub CargaDatos()

        Try

            If Not IsNothing(Session("Operacion")) Then

                Dim OPE As ope_cls
                Dim Coll_PGR As Collection

                OPE = Session("Operacion")
                Coll_PGR = Session("Pagares")

                If Coll_PGR.Count > 0 Then
                    Dim PGR As pgr_cls

                    PGR = Coll_PGR.Item(1)

                    With PGR
                        Dp_Tipo.SelectedValue = .id_P_0021
                        Dp_Moneda.SelectedValue = .id_P_0023
                        Txt_Monto.Text = .pgr_mto
                        Select Case .pgr_mdt
                            Case "S"
                                Rb_Mandato.Items(0).Selected = True
                                Rb_Mandato.Items(1).Selected = False
                            Case "N"
                                Rb_Mandato.Items(0).Selected = False
                                Rb_Mandato.Items(1).Selected = True
                        End Select
                        Txt_Can_Dom.Text = "?"
                    End With

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

End Class
