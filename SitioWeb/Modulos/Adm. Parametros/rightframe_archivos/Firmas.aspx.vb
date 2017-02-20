Imports ClsSession.ClsSession
Imports FuncionesGenerales.Class_LlenaCombo
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Partial Class firmas
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.RutinasWeb
    Dim cd As New ClaseControlDual
    Dim Caption As String = "Firmas"
    Dim Msj As New ClsMensaje

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            CG.ParametrosDevuelve(TablaParametro.Niveles, True, Me.dr_prio)
            cd.DevuelveCabeceraClasificacion(True, Me.dr_ccf)


            If Request.QueryString("id").Trim <> "" Then
                dr_ccf.ClearSelection()
                dr_ccf.Items.FindByValue(CInt(Request.QueryString("id"))).Selected = True
            End If

            gr_cfc.DataSource = CG.ParametrosDevuelve(45)
            gr_cfc.DataBind()

            If ccfnum <> 0 Then
                Me.dr_ccf.SelectedValue = ccfnum
            End If

        End If

        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        Try

            Dim I As Integer
            Dim frm As New frm_cls
            Dim idx As Integer
            Dim Coll As New Collection

            If Me.dr_ccf.SelectedIndex = 0 Then
                Msj.Mensaje(Page, Caption, "Debes seleccionar una clasificacion", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            If Me.dr_prio.SelectedIndex = 0 Then
                Msj.Mensaje(Page, Caption, "Debes seleccionar una prioridad", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            For I = 0 To Me.gr_cfc.Rows.Count - 1

                Dim ch As CheckBox

                ch = Me.gr_cfc.Rows(I).FindControl("ch_pfl")

                If ch.Checked Then

                    'Llenamos colleccion con las los perfiles seleccionados
                    frm = New frm_cls

                    idx = idx + 1
                    frm.id_p_0045 = Me.gr_cfc.Rows(I).Cells(1).Text
                    frm.id_p_005 = Me.dr_prio.SelectedValue
                    frm.id_ccf = Me.dr_ccf.SelectedValue

                    Coll.Add(frm)

                End If

            Next

            If idx = 0 Then
                Msj.Mensaje(Page, Caption, "Debes seleccionar al menos un perfil", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            If Not cd.ValidaFirmas(dr_ccf.SelectedValue, dr_prio.SelectedValue, Coll) Then
                Msj.Mensaje(Page, Caption, "No se puede agregar " & dr_prio.SelectedItem.Text.Trim & " por jerarquia de Perfiles", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            

            If Coll.Count > 0 Then

                cd.guarda_firma_clasificacion(Coll, dr_ccf.SelectedValue, dr_prio.SelectedValue)
                Msj.Mensaje(Page, Caption, "Registro Ingresado", TipoDeMensaje._Informacion, "", False)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub dr_prio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_prio.SelectedIndexChanged

        Dim coll As New Collection
        Dim i As Integer
        Dim X As Integer
        Dim ch As CheckBox

        
        If Me.dr_ccf.SelectedValue <> 0 And Me.dr_prio.SelectedValue <> "S" Then
            coll = cd.carga_perfiles_por_clasificacion(Me.dr_ccf.SelectedValue, Me.dr_prio.SelectedValue)

            gr_cfc.DataSource = CG.ParametrosDevuelve(45)
            gr_cfc.DataBind()

            If Not IsNothing(coll) Then
                For X = 1 To coll.Count
                    For i = 0 To gr_cfc.Rows.Count - 1
                        ch = Me.gr_cfc.Rows(i).FindControl("ch_pfl")
                        If Me.gr_cfc.Rows(i).Cells(1).Text = coll.Item(X).id_p_0045 Then
                            ch.Checked = True
                            Exit For
                        End If
                    Next
                Next
            End If
        End If

    End Sub

    Protected Sub dr_ccf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_ccf.SelectedIndexChanged

        Dim coll As New Collection
        Dim i As Integer
        Dim X As Integer
        Dim ch As CheckBox

        If Me.dr_ccf.SelectedValue <> 0 And Me.dr_prio.SelectedValue <> "S" Then
            coll = cd.carga_perfiles_por_clasificacion(Me.dr_ccf.SelectedValue, Me.dr_prio.SelectedValue)

            gr_cfc.DataSource = CG.ParametrosDevuelve(45)
            gr_cfc.DataBind()

            If Not IsNothing(coll) Then


                For X = 1 To coll.Count



                    For i = 0 To gr_cfc.Rows.Count - 1
                        ch = Me.gr_cfc.Rows(i).FindControl("ch_pfl")

                        If Me.gr_cfc.Rows(i).Cells(1).Text = coll.Item(X).id_p_0045 Then
                            ch.Checked = True
                        End If


                    Next
                Next
            End If
        End If
    End Sub



End Class
