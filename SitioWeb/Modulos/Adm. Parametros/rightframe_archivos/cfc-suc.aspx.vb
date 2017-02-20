Imports ClsSession.ClsSession
Imports FuncionesGenerales.Class_LlenaCombo
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class cfc_suc
    Inherits System.Web.UI.Page

    Dim ag As New ActualizacionesGenerales
    Dim cg As New ConsultasGenerales
    Dim cd As New ClaseControlDual
    Dim rg As New FuncionesGenerales.RutinasWeb
    Dim SESION As New ClsSession.ClsSession
    Dim Caption As String = "Sucursales con la que trabajen"
    Dim Msj As New ClsMensaje


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            SESION.coll_cfc = New Collection

            cd.DevuelveCabeceraClasificacion(True, Me.dr_ccf)
            gr_cfc.DataSource = cg.SucursalesDevuelve(CodEje, False, Nothing)
            gr_cfc.DataBind()

            If ccfnum <> 0 Then
                Me.dr_ccf.SelectedValue = ccfnum
                CargaSucursalesPorClasificacion()
            End If

        End If

        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        Try

            Dim I As Integer
            Dim cxs As New cxs_cls
            Dim idx As Integer
            Dim x As Boolean
            Dim COL As New Collection
            Dim OBJ As New obj_cxs

            For I = 0 To Me.gr_cfc.Rows.Count - 1

                Dim ch As CheckBox
                ch = Me.gr_cfc.Rows(I).FindControl("ch_pfl")

                If ch.Checked Then
                    idx = idx + 1
                    cxs = New cxs_cls
                    cxs.id_suc = Me.gr_cfc.Rows(I).Cells(1).Text
                    cxs.id_ccf = Me.dr_ccf.SelectedValue
                    SESION.coll_cfc.Add(cxs)
                End If

            Next

            If Me.dr_ccf.SelectedValue = 0 Then
                'Msj(Caption, "Debes seleccionar una Clasificacion", TipoDeMensaje._Informacion)
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una clasificación", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            If idx = 0 Then
                'Msj(Caption, "Debes seleccionar al menos una Sucursal", TipoDeMensaje._Informacion)
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar al menos una sucursal", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            coll_ccf = cd.CabeceraClasificacion_datos_devuelve(Me.dr_ccf.SelectedValue)

            For I = 1 To coll_cfc.Count

                
                x = cd.clasificacion_x_sucursal_guarda(coll_cfc.Item(I).id_suc, dr_ccf.SelectedValue)

                'If coll_ccf.Item(1).ccf_tip_apb = 2 Then

                OBJ = New obj_cxs

                OBJ.id_cxs = cd.carga_corr_sxa
                OBJ.id_suc = coll_cfc.Item(I).id_suc

                COL.Add(OBJ)

                'End If

            Next

            'If coll_ccf.Item(1).ccf_tip_apb = 2 And COL.Count > 0 Then
            x = cd.Sucursal_de_aprobacion_guarda(COL, Me.dr_ccf.SelectedValue)
            'End If

            If x Then
                Msj.Mensaje(Me.Page, Caption, "Registro Ingresado", TipoDeMensaje._Informacion, , False)
                SESION.coll_cfc = New Collection
            Else
                Msj.Mensaje(Me.Page, Caption, "Error no se ha podido guardar", TipoDeMensaje._Informacion, , False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub dr_ccf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_ccf.SelectedIndexChanged
        CargaSucursalesPorClasificacion()
    End Sub

    Private Sub CargaSucursalesPorClasificacion()
        Dim coll As New Collection
        Dim i As Integer
        Dim X As Integer
        Dim ch As CheckBox


        If Me.dr_ccf.SelectedValue <> 0 Then

            coll = cd.Sucursales_carga_por_clasificacion(Me.dr_ccf.SelectedValue)

            gr_cfc.DataSource = cg.SucursalesDevuelve(CodEje, False, Nothing)
            gr_cfc.DataBind()

            If Not IsNothing(coll) Then


                For X = 1 To coll.Count

                    For i = 0 To gr_cfc.Rows.Count - 1

                        ch = Me.gr_cfc.Rows(i).FindControl("ch_pfl")

                        If Me.gr_cfc.Rows(i).Cells(1).Text = coll.Item(X).id_suc Then
                            ch.Checked = True
                            Exit For
                        End If

                    Next

                Next

            End If

        End If

    End Sub

    Protected Sub CB_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Todos.CheckedChanged

        Try

            Dim I As Integer

            SESION.coll_cfc = New Collection

            If Me.dr_ccf.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una clasificación", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            cd.Sucursales_Elimina_clasificacion(dr_ccf.SelectedValue)

            For I = 0 To Me.gr_cfc.Rows.Count - 1

                Dim ch As CheckBox
                ch = Me.gr_cfc.Rows(I).FindControl("ch_pfl")
                ch.Checked = True

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, "Error", ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

End Class
