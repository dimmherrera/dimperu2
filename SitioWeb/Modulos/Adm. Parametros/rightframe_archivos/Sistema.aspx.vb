Imports System.Data
Imports FuncionesGenerales.Errores
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Sistema

    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub



    Protected Sub Btn_lim_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Txt_elicli.Text = ""
        Me.Txt_int.Text = ""
        Me.Txt_valfec.Text = ""
        Me.Txt_IVA.Text = ""
        Me.Txt_vto.Text = ""
        Me.Txt_Prorroga.Text = ""
        Me.Txt_gmf.Text = ""
        Me.RB_Val_Lin.ClearSelection()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            coll_pnu = New Collection
            coll_pnu.Add(cg.SistemaDevuelveDatos)

            If IsNothing(coll_pnu) = False Then

                If coll_pnu.Count > 0 Then

                    RB_Val_Lin.ClearSelection()

                    Me.Txt_elicli.Text = coll_pnu.Item(1).sis_dia_eli_cli
                    Me.Txt_int.Text = coll_pnu.Item(1).sis_dia_dev
                    Me.Txt_valfec.Text = coll_pnu.Item(1).sis_val_fec_gsn
                    Me.Txt_IVA.Text = coll_pnu.Item(1).sis_iva
                    Me.Txt_vto.Text = coll_pnu.Item(1).sis_dia_vto
                    Me.Txt_Prorroga.Text = coll_pnu.Item(1).sis_dia_pro
                    Me.Txt_gmf.Text = coll_pnu.Item(1).sis_can_gmf
                    Me.Txt_Vto_LDC.Text = coll_pnu.Item(1).sis_vto_ldc
                    Me.RB_Val_Lin.Items.FindByValue(coll_pnu.Item(1).sis_vld_lin).Selected = True

                    Me.Txt_int.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_IVA.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_elicli.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_valfec.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_vto.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_Prorroga.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_gmf.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Me.Txt_Vto_LDC.Attributes.Add("Style", "TEXT-ALIGN: right")

                End If

            End If

        End If

    End Sub



    Protected Sub btn_gua1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20011412, Usr, "PRESIONO GUARDAR ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        ag.guarda_datos_sistema(Txt_elicli.Text, Txt_int.Text, _
                                0, Txt_valfec.Text, _
                                Me.Txt_IVA.Text, Me.Txt_vto.Text, _
                                Txt_Prorroga.Text, Txt_gmf.Text, _
                                Txt_Vto_LDC.Text, RB_Val_Lin.SelectedValue)

        msj.Mensaje(Me, "Atención", "Registro Actualizado...", 2)

    End Sub

End Class
