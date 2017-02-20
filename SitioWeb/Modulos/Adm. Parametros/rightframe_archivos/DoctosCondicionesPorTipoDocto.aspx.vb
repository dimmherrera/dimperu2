Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class DoctosCondicionesPorTipoDocto
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim Caption As String = "Requisitos Por Tipo Docto."
    Dim Msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocumentos)
        Else

            If RadioButtonList1.SelectedValue <> "" And DP_TipoDocumentos.SelectedIndex > 0 Then
                btn_Guardar.Enabled = True
            Else
                btn_Guardar.Enabled = False
            End If

        End If

    End Sub

    Protected Sub DP_TipoDocumentos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoDocumentos.SelectedIndexChanged
        LlenaGrilla()
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        LlenaGrilla()
    End Sub

    Private Sub LlenaGrilla()

        Gr_DocCon.DataSource = CG.DocConComDevuelve(RadioButtonList1.SelectedValue)
        Gr_DocCon.DataBind()

        Dim doccon As IQueryable = CG.DocConComDevuelvePorTipoDocto(RadioButtonList1.SelectedValue, DP_TipoDocumentos.SelectedValue)

        For I = 0 To Gr_DocCon.Rows.Count - 1

            If Gr_DocCon.Rows(I).Cells(3).Text = "1" Then
                Gr_DocCon.Rows(I).Cells(3).Text = "ACTIVO"
            Else
                Gr_DocCon.Rows(I).Cells(3).Text = "INACTIVO"
            End If

            For Each dc In doccon
                If dc.id = Gr_DocCon.Rows(I).Cells(1).Text Then
                    Dim Cb As CheckBox
                    Cb = Gr_DocCon.Rows(I).FindControl("CB_DocCon")
                    Cb.Checked = True
                End If
            Next

        Next

    End Sub

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Guardar.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20011112, Usr, "PRESIONO GUARDAR REQUISITO POR TIPO DE DOCUMENTO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Msj.Mensaje(Me, "Atención", "¿Desea Guardar los cambios?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_gua.UniqueID)

    End Sub

    Protected Sub lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_gua.Click

        Try

            Dim Valida_Ingreso As Boolean = False
            Dim Valida_Req As Boolean = False
            Dim tipo As String

            'Select RadioButtonList1.SelectedValue
            '    Case "D"
            '        AG.DocComPorDoctoElimina(DP_TipoDocumentos.SelectedValue)
            '    Case "C"
            '        AG.ConComPorDoctoElimina(DP_TipoDocumentos.SelectedValue)
            'End Select

            For I = 0 To Gr_DocCon.Rows.Count - 1

                Dim Cb As CheckBox
                Cb = Gr_DocCon.Rows(I).FindControl("CB_DocCon")

                'Select RadioButtonList1.SelectedValue
                '    Case "D"
                '        AG.DocComPorDoctoElimina(DP_TipoDocumentos.SelectedValue, CInt(Gr_DocCon.Rows(I).Cells(1).Text))
                '        Valida_Ingreso = True
                '    Case "C"
                '        AG.ConComPorDoctoElimina(DP_TipoDocumentos.SelectedValue, CInt(Gr_DocCon.Rows(I).Cells(1).Text))
                '        Valida_Ingreso = True
                'End Select

                If Cb.Checked Then

                    Select Case RadioButtonList1.SelectedValue
                        Case "D"
                            tipo = "Documento"
                            Dim dxd As New dxd_cls

                            dxd.id_p_031 = DP_TipoDocumentos.SelectedValue
                            dxd.id_doc_com = CInt(Gr_DocCon.Rows(I).Cells(1).Text)

                            If AG.DocComPorDoctoInserta(dxd) Then
                                Valida_Ingreso = True
                            Else
                                Valida_Ingreso = False
                            End If

                        Case "C"
                            tipo = "Otras Condiciones"
                            Dim cxd As New cxd_cls

                            cxd.id_p_0031 = DP_TipoDocumentos.SelectedValue
                            cxd.id_con_com = CInt(Gr_DocCon.Rows(I).Cells(1).Text)

                            If AG.ConComPorDoctoInserta(cxd) Then
                                Valida_Ingreso = True
                            Else
                                Valida_Ingreso = False
                            End If
                    End Select

                    Valida_Req = True

                Else
                    If Not Valida_Req Then
                        Valida_Req = False
                    End If
                End If

            Next

            If Valida_Req Then
                'If Valida_Ingreso Then
                Msj.Mensaje(Me.Page, Caption, "Se Guardo Plantilla de " & tipo, TipoDeMensaje._Informacion)
                LlenaGrilla()
                btn_Guardar.Enabled = False
                'Else
                '    Msj.Mensaje(Me.Page, Caption, "No Se Guardo Plantilla de " & tipo, TipoDeMensaje._Exclamacion)
                'End If
            Else
                Msj.Mensaje(Me.Page, Caption, "Seleccione al menos un registro " & tipo, TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CB_DocCon_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim CB As CheckBox = CType(sender, CheckBox)

        'For i = 0 To Gr_DocCon.Rows.Count - 1

        '    If CB.ToolTip.ToString() = Gr_DocCon.Rows(i).Cells(1).Text Then

        '    End If

        'Next

    End Sub
End Class
