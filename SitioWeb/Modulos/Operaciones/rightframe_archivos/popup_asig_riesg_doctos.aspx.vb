Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_popup_asig_riesg_doctos
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim rg As New FuncionesGenerales.FComunes
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1

        If Not Me.IsPostBack Then

            Me.Gr_Documentos.DataSource = Coll_DOC
            Me.Gr_Documentos.DataBind()

            For i = 0 To Me.Gr_Documentos.Rows.Count - 1

                Me.Gr_Documentos.Rows(i).Cells(1).Text = Format(CLng(Me.Gr_Documentos.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & rg.Vrut(Me.Gr_Documentos.Rows(i).Cells(1).Text)
            Next

            CG.ParametrosDevuelve(TablaParametro.TipoRiesgo, True, Me.dr_riesgo)

        End If

        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        msj.Mensaje(Me, "Atención", "¿Desea Guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_guardar.UniqueID, False)

    End Sub


    Protected Sub lb_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_guardar.Click
        Dim rsl As Boolean = False

        If Me.dr_riesgo.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "Debe seleccionar un Riesgo", ClsMensaje.TipoDeMensaje._Exclamacion, False)
            Exit Sub
        End If

        Dim contador As Integer

        For i = 0 To Me.Gr_Documentos.Rows.Count - 1

            Dim ch As CheckBox
            ch = Me.Gr_Documentos.Rows(i).FindControl("ch_sel")

            If ch.Checked Then
                contador = contador + 1
                rsl = OP.categoria_riesgo_guarda_dsi(Coll_DOC.Item(i + 1).id_dsi, Me.dr_riesgo.SelectedValue)
            End If


        Next

        If contador = 0 Then

            msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un documento", ClsMensaje.TipoDeMensaje._Exclamacion, False)
            Exit Sub

        End If

        If rsl Then
            msj.Mensaje(Me, "Atención", "Se ha Guardado Correctamente", ClsMensaje.TipoDeMensaje._Informacion, False)
            Exit Sub
        End If

    End Sub

End Class
