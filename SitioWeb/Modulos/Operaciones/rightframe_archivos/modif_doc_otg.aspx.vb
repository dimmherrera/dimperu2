Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_protesto
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim OP As New ClaseOperaciones
    Dim msj As New ClsMensaje
    Dim CTA As New ClaseCuentas

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click
        If Me.dr_moneda.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atencion", "Debe Seleccionar Moneda", 2)
            Exit Sub
        End If

        If Me.txt_mto_comi.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Debe Ingresar Comisión ", 2)
            Exit Sub
        End If

        If Me.dr_mot.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atencion", "Debe Seleccionar Motivo", 2)
            Exit Sub
        End If

        msj.Mensaje(Me, "Atención", "¿Desea Modificar los Documentos?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_guardar.UniqueID)

    End Sub


    Private Sub guardar()
        Dim pagado_ddr As String, estado1 As Integer, _
         fec_cob As Date, fec_cas As Date, tipo As Integer, _
        notif As String, cobr1 As String, env_bco As String, obl As String, motivo As Integer, seleccion As Boolean = False
        Dim count As Integer

        'Asigna variables segun corresponda


        If Me.dr_mot.SelectedValue = 0 Then
            motivo = Nothing
        Else
            motivo = dr_mot.SelectedValue
        End If

        If Me.dr_moneda.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atencion", "Debe Seleccionar Moneda", 2)
            Exit Sub
        End If

        If Me.txt_mto_comi.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Debe Ingresar Comisión ", 2)
            Exit Sub
        End If

        If Me.dr_mot.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atencion", "Debe Seleccionar Motivo", 2)
            Exit Sub
        End If

        count = arreglo.Count

        If count = 0 Then

            msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un Documento para Modificar", ClsMensaje.TipoDeMensaje._Exclamacion)

        Else

            msj.Mensaje(Me, "Atención", "Registros Actualizados", ClsMensaje.TipoDeMensaje._Exclamacion)
        End If



        OP.documentos_otorgados_modifica(9, pagado_ddr, notif, cobr1, Me.dr_mot.SelectedValue, env_bco, fec_cas, fec_cob, "", obl)

        Coll_DOC = New Collection
        Coll_DOC = OP.Documentos_a_cxc_retorna

        For i = 1 To Coll_DOC.Count


            Dim cxc As New cxc_cls

            cxc.id_P_0041 = 5
            cxc.cxc_int = 0
            cxc.id_P_0057 = 1
            cxc.cxc_fct = "N"
            cxc.cxc_mto = CDbl(Me.txt_mto_comi.Text) + CDbl(Me.txt_iva.Text)
            cxc.cli_idc = Format(CLng(Coll_DOC.Item(i).cli_idc), "000000000000")
            cxc.cxc_sal = CDbl(Me.txt_mto_comi.Text) + CDbl(Me.txt_iva.Text)
            cxc.id_doc = Coll_DOC.Item(i).id_doc
            cxc.id_opo = Coll_DOC.Item(i).id_opo
            cxc.cxc_fec = Date.Now
            cxc.cxc_fac_cam = cg.ParidadDevuelve(dr_moneda.SelectedValue, Date.Now).par_val
            cxc.cxc_ful_pgo = Coll_DOC.Item(i).doc_ful_pgo
            cxc.cxc_des = "Protesto Para nro: " & Coll_DOC.Item(i).dsi_num
            cxc.id_P_0023 = dr_moneda.SelectedValue

            CTA.CxcInserta(cxc)

        Next





    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            cg.ParametrosDevuelve(23, True, Me.dr_moneda)
            cg.ParametrosDevuelve(61, True, dr_mot)
        End If
        Me.txt_mto_comi.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.txt_iva.Attributes.Add("Style", "TEXT-ALIGN: right")
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub lb_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guardar.Click
        guardar()
    End Sub

    Protected Sub lb_formato_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_formato.Click

        If txt_mto_comi.Text = "" Then
            txt_mto_comi.Text = 0
        End If

        Me.txt_iva.Text = CDbl(Me.txt_mto_comi.Text) * (cg.SistemaDevuelveDatos().sis_iva / 100)

        If Me.dr_moneda.SelectedValue = 0 Or Me.dr_moneda.SelectedValue = 1 Then

            Me.txt_mto_comi.Text = Format(CDbl(Me.txt_mto_comi.Text), "###,###,###")
            Me.txt_iva.Text = Format(CDbl(Me.txt_iva.Text), "###,###,###")

        ElseIf Me.dr_moneda.SelectedValue = 2 Or Me.dr_moneda.SelectedValue = 3 Then

            Me.txt_mto_comi.Text = Format(CDbl(Me.txt_mto_comi.Text), "###,###,###.00")
            Me.txt_iva.Text = Format(CDbl(Me.txt_iva.Text), "###,###,###.00")

        ElseIf Me.dr_moneda.SelectedValue = 4 Then

            Me.txt_mto_comi.Text = Format(CDbl(Me.txt_mto_comi.Text), "###,###,###.0000")
            Me.txt_iva.Text = Format(CDbl(Me.txt_iva.Text), "###,###,###.0000")

        End If
    End Sub

    Protected Sub txt_mto_comi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_mto_comi.TextChanged

        Me.lb_formato_click(Me, e)

    End Sub

    Protected Sub dr_moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_moneda.SelectedIndexChanged
        If Me.dr_moneda.SelectedValue = 1 Then
            Me.txt_mto_comi.Text = ""
            Txt_valor_MaskedEditExtender.Mask = "999,999,999,999"
        ElseIf dr_moneda.SelectedValue = 2 Then
            Me.txt_mto_comi.Text = ""
            Txt_valor_MaskedEditExtender.Mask = "999,999,999.9999"
        Else
            Me.txt_mto_comi.Text = ""
            Txt_valor_MaskedEditExtender.Mask = "999,999,999.99"
        End If
    End Sub
End Class
