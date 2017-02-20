Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Cobranzas_rigthframe_archivos_GestionesAnteriores
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim cbz As New ClaseCobranza

    Private Sub CargaGestiones(ByVal RutCliente As String, ByVal RutDeudor As String, _
                               ByVal NroOperacion As Integer, ByVal NroDocto As String, _
                               ByVal Cuota As Integer)


        Try

            '---------------------------------------------------------------------
            'Retorna Gestiones Anteriores
            '---------------------------------------------------------------------
            Dim Temporal_gsn = cbz.Gestiones_Retorna(0, 999999999999, _
                                                    RutCliente, RutCliente, _
                                                    RutDeudor, RutDeudor, _
                                                    NroOperacion, NroOperacion, _
                                                    0, 999, _
                                                    NroDocto, NroDocto, _
                                                    Cuota, Cuota)


            Dim StrLinea As String
            RB_GesAnteriores.Items.Clear()

            For Each gsn1 In Temporal_gsn

                StrLinea = Format(gsn1.gsn_fec, "dd/MM/yyyy") & Space(10) & CDate(gsn1.gsn_hor).ToShortTimeString & Space(20) & gsn1.eje_nom

                Dim p As New ListItem(StrLinea, gsn1.id_gsn)

                RB_GesAnteriores.Items.Add(p)

            Next



        Catch ex As Exception

        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            If Request.QueryString("cli") <> "" And Request.QueryString("deu") <> "" Then

                CargaGestiones(Request.QueryString("cli"), Request.QueryString("deu"), _
                               Request.QueryString("ope"), Request.QueryString("doc"), _
                               Request.QueryString("cuo"))
            Else

            End If

            IB_VolverGestion.Attributes.Add("onClick", "javascript:window.close();")

        End If

    End Sub

    Protected Sub RB_GesAnteriores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_GesAnteriores.SelectedIndexChanged

        If Not String.IsNullOrEmpty(RB_GesAnteriores.SelectedValue) Then

            PCExt_GesAnteriores.Commit(RB_GesAnteriores.SelectedItem.Text)

            'Retorna Datos de Gestion seleccionada
            RetornaDatosGestionAnterios(Request.QueryString("cli"), Request.QueryString("deu"), _
                                        Request.QueryString("ope"), Request.QueryString("doc"), _
                                        Request.QueryString("cuo"))

        Else

            PCExt_GesAnteriores.Cancel()

        End If

        RB_GesAnteriores.ClearSelection()

    End Sub

    Protected Sub RetornaDatosGestionAnterios(ByVal RutCliente As String, ByVal RutDeudor As String, _
                                              ByVal NroOperacion As Integer, ByVal NroDocto As String, _
                                              ByVal Cuota As Integer)

        Try

            Dim Gestion_id As Int64

            Gestion_id = RB_GesAnteriores.SelectedValue

            Dim Temporal_gsn = cbz.Gestiones_Retorna(Gestion_id, Gestion_id, RutCliente, RutCliente, RutDeudor, RutDeudor, NroOperacion, NroOperacion, 0, 999, NroDocto, NroDocto, Cuota, Cuota)

            For Each gsn1 In Temporal_gsn

                txt_GAFechaPago.Text = IIf(IsNothing(gsn1.gsn_fec_pag), "", Format(gsn1.gsn_fec_pag, "dd/MM/yyyy"))
                txt_GAHoraPagodde.Text = IIf(IsNothing(gsn1.gsn_hor_pag_dde), "", Format(gsn1.gsn_hor_pag_dde, "HH:MM"))
                txt_GAHoraPagoHta.Text = IIf(IsNothing(gsn1.gsn_hor_pag), "", Format(gsn1.gsn_hor_pag, "HH:MM"))
                txt_GAFechaProxGestion.Text = IIf(IsNothing(gsn1.gsn_fec_prx), "", Format(gsn1.gsn_fec_prx, "dd/MM/yyyy"))
                txt_GAHoraGestion.Text = IIf(IsNothing(gsn1.gsn_hor_prx), "", Format(gsn1.gsn_hor_prx, "HH:MM"))
                txt_GAAlaOrden.Text = IIf(IsNothing(gsn1.gsn_alo_obs), "", gsn1.gsn_alo_obs)

                txt_GACodCobranza.Text = IIf(IsNothing(gsn1.cco_num), "", gsn1.cco_num)
                txt_GADesCodCobranza.Text = IIf(IsNothing(gsn1.cco_des), "", gsn1.cco_des)
                Txt_GAdireccion.Text = IIf(IsNothing(gsn1.gsn_dir_cbz), "", gsn1.gsn_dir_cbz)
                txt_GADoctoNec.Text = IIf(IsNothing(gsn1.gsn_doc_rtr_pag), "", gsn1.gsn_doc_rtr_pag)
                txt_GAObservacion.Text = gsn1.gsn_obs

                If gsn1.cmn_des Is Nothing Then
                    txt_GAComuna.Text = ""
                    txt_GACiudad.Text = ""
                    txt_GAZona.Text = ""
                Else
                    txt_GAComuna.Text = IIf(IsNothing(gsn1.cmn_des), "", gsn1.cmn_des)
                    txt_GACiudad.Text = IIf(IsNothing(gsn1.ciu_des), "", gsn1.ciu_des)
                    txt_GAZona.Text = IIf(IsNothing(gsn1.zon_des), "", gsn1.zon_des)
                End If
                Txt_GAdireccion.Text = IIf(IsNothing(gsn1.gsn_dir_cbz), "", gsn1.gsn_dir_cbz)
                txt_GADoctoNec.Text = IIf(IsNothing(gsn1.gsn_doc_rtr_pag), "", gsn1.gsn_doc_rtr_pag)
                txt_GAObservacion.Text = gsn1.gsn_obs

                'txt_GAObservacion.Text = IIf(IsNothing(gsn1.gsn_obs), "", gsn1.gsn_obs & IIf(IsNothing(gsn1.gsn_obs_1), "", gsn1.gsn_obs_1 & IIf(IsNothing(gsn1.gsn_obs_2), "", gsn1.gsn_obs_2)))

            Next

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_VolverGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_VolverGestion.Click

    End Sub
End Class
