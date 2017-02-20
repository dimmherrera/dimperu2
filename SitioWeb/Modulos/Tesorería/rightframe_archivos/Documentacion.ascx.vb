Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_Documentacion
    Inherits System.Web.UI.UserControl

    Dim documentos As Collection
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim Msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
            If NroNegociacion <> 0 And NroNegociacion <> HF_Ope.Value Then
                Session("documentos") = New Collection
                HF_Ope.Value = NroNegociacion
                LLenaDocumentacion()
            End If
        Else
            HF_Ope.Value = "0"
        End If

    End Sub

    Private Sub LLenaDocumentacion()

        Dim doctos As IQueryable = CG.DocConDevuelvePorNegociacion(NroNegociacion, DP_Tipo.SelectedValue)

        Me.Gr_DocCom.DataSource = doctos
        Me.Gr_DocCom.DataBind()

        Dim cb As CheckBox

        documentos = Session("documentos")

        For Each d In doctos

            For i = 0 To Gr_DocCom.Rows.Count - 1

                cb = CType(Gr_DocCom.Rows(i).FindControl("CB_DOC"), CheckBox)

                If cb.ToolTip = d.id And d.estado = "A" Then

                    Dim existe As Boolean = False

                    For x = 1 To documentos.Count
                        If documentos.Item(x) = cb.ToolTip Then
                            existe = True
                            Exit For
                        End If
                    Next

                    If Not existe Then
                        documentos.Add(cb.ToolTip)
                    End If

                    cb.Checked = True
                    Exit For

                End If

            Next

        Next

        If Gr_DocCom.Rows.Count <= 0 Then
            Msj.Mensaje(Me.Page, "Verificación", "No se encuentran Documentación a Verificar", ClsMensaje.TipoDeMensaje._Informacion)
        End If

        Session("documentos") = documentos

    End Sub

    Protected Sub DP_Tipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Tipo.SelectedIndexChanged
        LLenaDocumentacion()
    End Sub

    Protected Sub IB_GuardarRequisitos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GuardarRequisitos.Click

        documentos = Session("documentos")

        'Limpiamos todos antes de actualizar los aprobados
        AG.DocComsPorOperacionAoR_LImpia(NroNegociacion)

        For x = 1 To documentos.Count

            Dim dxn As New dxn_cls

            dxn.id_dxn = documentos(x).ToString()
            dxn.id_opn = NroNegociacion
            dxn.id_eje = CodEje
            dxn.est_dxd = "A"
            dxn.dxn_fec_apb = Date.Now

            AG.DocComsPorOperacionAoR_Actualiza(dxn)

        Next

        Msj.Mensaje(Me.Page, "Verificación", "Confirmación de Documentos Guardados para esta Operación", ClsMensaje.TipoDeMensaje._Informacion)

        LLenaDocumentacion()


    End Sub


    Protected Sub IB_TodosDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        documentos = Session("documentos")
        
        Dim cb As New CheckBox

        For i = 0 To Gr_DocCom.Rows.Count - 1
            cb = Gr_DocCom.Rows(i).FindControl("CB_DOC")
            If Not documentos.Contains(cb.ToolTip) Then
                documentos.Add(cb.ToolTip)
                cb.Checked = True
            End If
        Next

        Session("documentos") = documentos

    End Sub

    Protected Sub CB_DOC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Agrega o quita documento de collection
        documentos = Session("documentos")
        Dim cb As CheckBox = sender

        If cb.Checked Then

            Dim existe As Boolean = False

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    existe = True
                    Exit For
                End If
            Next

            If existe Then
                Exit Sub
            Else
                documentos.Add(cb.ToolTip)
            End If

        Else

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    documentos.Remove(I)
                    Exit For
                End If
            Next

        End If

        Session("documentos") = documentos

    End Sub

End Class
