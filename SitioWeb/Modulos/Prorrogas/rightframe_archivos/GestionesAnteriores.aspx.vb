Imports ClsSession.SesionOperaciones
Imports CapaDatos
Imports ClsSession.SesionProrrogas

Partial Class Modulos_Prorrogas_rightframe_archivos_Default
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"
    Dim CG As New ConsultasGenerales
    Dim CBZ As New ClaseCobranza
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then


            Dim RutCliente As String
            Dim RutDeudor As String
            Dim TipoDocto As Int16
            Dim NroOperacion As Int64
            Dim NroDocto As Int64
            Dim Cuota As Int16


            HD_Item.Value = Request.QueryString("Item")

            'Ver Cuando despliega Deudor/Cliente o Cliente/Deudor
            RutCliente = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).cli_idc
            RutDeudor = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).deu_ide

            TipoDocto = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).id_P_0031
            NroOperacion = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).id_opo
            NroDocto = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).dsi_num
            Cuota = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).dsi_flj_num

            'Retorna Gestiones Anteriores
            Dim StrLinea As String
            Dim Temporal_gsn = CBZ.Gestiones_Retorna(0, 999999999999, RutCliente, RutCliente, RutDeudor, RutDeudor, NroOperacion, NroOperacion, TipoDocto, TipoDocto, NroDocto, NroDocto, Cuota, Cuota)
            RB_GesAnteriores.Items.Clear()
            For Each gsn1 In Temporal_gsn

                StrLinea = Format(gsn1.gsn_fec, "dd/MM/yyyy") & Space(10) & Format(gsn1.gsn_hor, "HH:MM") & Space(20) & gsn1.eje_nom
                Dim p As New ListItem(StrLinea, gsn1.id_gsn)
                RB_GesAnteriores.Items.Add(p)
            Next
        End If

    End Sub
    Protected Sub RB_GesAnteriores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_GesAnteriores.SelectedIndexChanged
        'If Not String.IsNullOrEmpty(RB_GesAnteriores.SelectedValue) Then
        'PCExt_GesAnteriores.Commit(RB_GesAnteriores.SelectedItem.Text)
        If Not String.IsNullOrEmpty(RB_GesAnteriores.Items(0).Text) Then
            PCExt_GesAnteriores.Commit(RB_GesAnteriores.Items(0).Text)

            'Retorna Datos de Gestion seleccionada
            RetornaDatosGestionAnterios()

        Else
            PCExt_GesAnteriores.Cancel()
        End If

        RB_GesAnteriores.ClearSelection()
    End Sub
    Protected Sub RetornaDatosGestionAnterios()
        Try
            Dim RutCliente As String
            Dim RutDeudor As String
            Dim TipoDocto As Int16
            Dim NroOperacion As Int64
            Dim NroDocto As Int64
            Dim Cuota As Int16
            Dim Gestion_id As Int64

            'Ver Cuando despliega Deudor/Cliente o Cliente/Deudor
            RutCliente = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).cli_idc
            RutDeudor = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).deu_ide

            TipoDocto = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).id_P_0031
            NroOperacion = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).id_opo
            NroDocto = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).dsi_num
            Cuota = Coll_DocumentosProrroga.Item(Val(HD_Item.Value)).dsi_flj_num

            'Gestion_id = RB_GesAnteriores.SelectedValue

            'Gestion_id = RB_GesAnteriores.SelectedItem.Value
            Gestion_id = RB_GesAnteriores.Items(1).Value






            Dim Temporal_gsn = CBZ.Gestiones_Retorna(Gestion_id, Gestion_id, RutCliente, RutCliente, RutDeudor, RutDeudor, NroOperacion, NroOperacion, TipoDocto, TipoDocto, NroDocto, NroDocto, Cuota, Cuota)
            ' Dim gsn1 As New gsn_cls

            For Each gsn1 In Temporal_gsn
                txt_GAFechaPago.Text = IIf(IsNothing(gsn1.gsn_fec_pag), "", Format(gsn1.gsn_fec_pag, "dd/MM/yyyy"))
                txt_GAHoraPagodde.Text = IIf(IsNothing(gsn1.gsn_hor_pag_dde), "", Format(gsn1.gsn_hor_pag_dde, "HH:MM"))
                txt_GAHoraPagoHta.Text = IIf(IsNothing(gsn1.gsn_hor_pag), "", Format(gsn1.gsn_hor_pag, "HH:MM"))
                txt_GAFechaProxGestion.Text = IIf(IsNothing(gsn1.gsn_fec_prx), "", Format(gsn1.gsn_fec_prx, "dd/MM/yyyy"))
                txt_GAHoraGestion.Text = IIf(IsNothing(gsn1.gsn_hor_prx), "", Format(gsn1.gsn_hor_prx, "HH:MM"))
                txt_GAAlaOrden.Text = IIf(IsNothing(gsn1.gsn_alo_obs), "", gsn1.gsn_alo_obs)
                'txt_GACiudad.Text = IIf(IsNothing(gsn1.cmn_cls.ciu_cls.ciu_des), "", gsn1.cmn_cls.ciu_cls.ciu_des)
                txt_GACodCobranza.Text = IIf(IsNothing(gsn1.cco_num), "", gsn1.cco_num)
                txt_GADesCodCobranza.Text = IIf(IsNothing(gsn1.cco_des), "", gsn1.cco_des)
                ' txt_GAComuna.Text = IIf(IsNothing(gsn1.cmn_cls.cmn_des), "", gsn1.cmn_cls.cmn_des)
                Txt_GAdireccion.Text = IIf(IsNothing(gsn1.gsn_dir_cbz), "", gsn1.gsn_dir_cbz)
                txt_GADoctoNec.Text = IIf(IsNothing(gsn1.gsn_doc_rtr_pag), "", gsn1.gsn_doc_rtr_pag)
                txt_GAObservacion.Text = IIf(IsNothing(gsn1.gsn_obs), "", gsn1.gsn_obs)
                'txt_GAObservacion.Text = IIf(IsNothing(gsn1.gsn_obs), "", gsn1.gsn_obs & IIf(IsNothing(gsn1.gsn_obs_1), "", gsn1.gsn_obs_1 & IIf(IsNothing(gsn1.gsn_obs_2), "", gsn1.gsn_obs_2)))

                '  txt_GAZona.Text = IIf(IsNothing(gsn1.zon_cls.zon_des), "", gsn1.zon_cls.zon_des)
            Next

        Catch ex As Exception

        End Try
    End Sub
End Class
