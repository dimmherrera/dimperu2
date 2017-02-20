Imports ClsSession.ClsSession
Imports FuncionesGenerales.Class_LlenaCombo
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class Suc_Aprobacion
    Inherits System.Web.UI.Page

    Dim ag As New ActualizacionesGenerales
    Dim cg As New ConsultasGenerales
    Dim cd As New ClaseControlDual
    Dim rg As New FuncionesGenerales.RutinasWeb
    Dim SESION As New ClsSession.ClsSession
    Dim Caption As String = "Sucursales de Aprobación"
    Dim Msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            SESION.coll_cfc = New Collection
            SESION.coll_sxa = New Collection
            cd.DevuelveCabeceraClasificacion(True, Me.dr_ccf)
            cg.SucursalesDevuelve(codeje, True, dr_suc)
            

            If ccfnum <> 0 Then
                Me.dr_ccf.SelectedValue = ccfnum
                CargaSucursales()
            End If
        End If
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click
        Dim I As Integer
        Dim sxa As New sxa_cls
        Dim idx As Integer
        Dim x As Boolean
 

        If Me.dr_ccf.SelectedValue = 0 Then
            Msj.Mensaje(Me.Page, Caption, "Debes seleccionar una clasificacion", TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        'If idx = 0 Then
        '    MsgBox("Debes seleccionar al menos un perfil")
        '    Exit Sub
        'End If

        If coll_sxa.Count > 0 Then

            x = cd.Sucursal_de_aprobacion_guarda(coll_sxa, dr_ccf.SelectedValue)

            If x = True Then
                Msj.Mensaje(Me.Page, Caption, "Registro Ingresado", TipoDeMensaje._Exclamacion)
            Else
                Msj.Mensaje(Me.Page, Caption, "Error no se ha podido Guardar", TipoDeMensaje._Exclamacion)
            End If
        End If

        'For I = 1 To coll_sxa.Count


        'Next


    End Sub



    Protected Sub dr_ccf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_ccf.SelectedIndexChanged

        CargaSucursales()
       
    End Sub

    Protected Sub btn_asoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asoc.Click
        Dim col As New Collection

        ' Aqui se Agrega el item de la Grilla
        Dim i As Integer
        Dim x As Integer
        Dim obj As New apb_cxs

        For i = 0 To Me.gr_cfc.Rows.Count - 1



            Dim ch As CheckBox
            ch = Me.gr_cfc.Rows(i).FindControl("ch_pfl")

            If ch.Checked = True Then


                For x = 1 To SESION.coll_sxa.Count

                    If coll_ccf.Item(1).ccf_tip_apb = 1 Then

                        If SESION.coll_sxa.Item(x).id_cxs = Me.gr_cfc.Rows(i).Cells(1).Text And SESION.coll_sxa.Item(x).id_suc = Me.dr_suc.SelectedValue Then
                            Msj.Mensaje(Me.Page, Caption, "Esta sucursal ya está asociada", TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If



                    End If


                Next

                obj = New apb_cxs


                obj.id_suc = Me.dr_suc.SelectedValue
                obj.id_cxs = Me.gr_cfc.Rows(i).Cells(1).Text
                obj.nom_suc = Me.gr_cfc.Rows(i).Cells(2).Text
                obj.nom_suc_apb = Me.dr_suc.SelectedItem.Text

                SESION.coll_sxa.Add(obj)
            End If



        Next

        Me.Gr_apb.DataSource = coll_sxa
        Me.Gr_apb.DataBind()

        If coll_ccf.Item(1).ccf_tip_apb = 1 Then


            Me.dr_suc.Enabled = True
            Me.dr_suc.CssClass = "clsMandatorio"

        End If
    End Sub

    Protected Sub btn_asoc0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asoc0.Click

        ' Aqui se quita el item de la Grilla
        Dim x As Boolean
        Dim col As Collection
        Dim del As New ArrayList
        Dim z As Integer
        z = 0
        For i = 0 To Me.Gr_apb.Rows.Count - 1


            Dim ch As CheckBox
            ch = Me.Gr_apb.Rows(i).FindControl("Ch_apb")

            If ch.Checked = True Then


                del.Add(i + 1)

            End If



        Next

        For i = 0 To del.Count - 1



            If SESION.coll_sxa.Count = 1 Then
                Msj.Mensaje(Me.Page, Caption, "Debe tener al menos una sucursal asociada", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            SESION.coll_sxa.Remove(del.Item(i) - z)
            z = z + 1

        Next



        Me.Gr_apb.DataSource = SESION.coll_sxa
        Me.Gr_apb.DataBind()

        If coll_ccf.Item(1).ccf_tip_apb = 1 Then
            If Me.Gr_apb.Rows.Count = 0 Then
                Me.dr_suc.Enabled = True
                Me.dr_suc.CssClass = "clsMandatorio"
            End If

        End If
    End Sub

    Private Sub CargaSucursales()
        Dim coll As New Collection
        If Me.dr_ccf.SelectedValue <> 0 Then

            Me.Gr_apb.Controls.Clear()
            Me.Gr_apb.DataBind()

            gr_cfc.DataSource = cd.Sucursales_carga_por_clasificacion(Me.dr_ccf.SelectedValue)
            gr_cfc.DataBind()

            coll_ccf = cd.CabeceraClasificacion_datos_devuelve(Me.dr_ccf.SelectedValue)

            SESION.coll_sxa = New Collection


            SESION.coll_sxa = cd.Sucursales_de_Aprobación_Devuelve(Me.dr_ccf.SelectedValue)

            Me.Gr_apb.DataSource = SESION.coll_sxa
            Me.Gr_apb.DataBind()
            If Not IsNothing(SESION.coll_sxa) Then


                If SESION.coll_sxa.Count > 0 Then
                    If coll_ccf.Item(1).ccf_tip_apb = 1 Then

                        Me.dr_suc.SelectedValue = SESION.coll_sxa.Item(1).id_suc
                        Me.dr_suc.Enabled = False
                        Me.dr_suc.CssClass = "clsDisabled"

                    End If

                End If


            End If
        End If

    End Sub

End Class
