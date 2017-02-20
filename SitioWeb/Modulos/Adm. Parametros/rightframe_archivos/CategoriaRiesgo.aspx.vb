Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class CategoriaRiesgo
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim caption As String = "Categoria de Riesgo"
    Dim CG As New ConsultasGenerales
    Dim sesion As New ClsSession.ClsSession
    Dim AG As New ActualizacionesGenerales
    
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                Coll_Ctr = New Collection

                If Request.QueryString("Id_Docto") <> 0 Then
                    txt_Id.Text = Request.QueryString("Id_Docto")
                    txt_Des.Text = Request.QueryString("Documento")
                    CargaGr()
                    If Gr_Ctr.Rows.Count = 0 Then
                        LlenaGr()
                    End If
                End If
                txt_Dia_dsd.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Dia_hst.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Id.Attributes.Add("Style", "TEXT-ALIGN: right")
                'txt_TPRnumero.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_TpRLetra.Attributes.Add("Style", "TEXT-ALIGN: right")




            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Gr_Ctr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Ctr.RowDataBound
        Try
            If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
                e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
                e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
                e.Row.Attributes.Add("onClick", "ClickGrCtr(Gr_Ctr, 'clicktable', 'formatable', 'selectable')")
            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub LinkB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB.Click
        Try
            For i = 0 To Gr_Ctr.Rows.Count - 1
                Gr_Ctr.Rows(i).CssClass = "formatable"
                If HF_Po.Value >= 0 And HF_Id.Value >= 0 Then
                    Gr_Ctr.Rows(HF_Po.Value).CssClass = "clicktable"
                Else
                    Gr_Ctr.Rows(HF_Po.Value).CssClass = "formatable"
                End If
            Next

            If HF_Po.Value >= 0 And HF_Id.Value >= 0 Then
                txt_TpRLetra.Text = Gr_Ctr.Rows(HF_Po.Value).Cells(0).Text
                txt_TPRnumero.Text = Gr_Ctr.Rows(HF_Po.Value).Cells(1).Text
                txt_Dia_dsd.Text = ""
                txt_Dia_hst.Text = ""
                txt_Dia_dsd.Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            Dim coll As New Collection

            AG.CategoriaRiesgoElimina(txt_Id.Text)
            For i = 0 To Gr_Ctr.Rows.Count - 1
                Dim c As New ctr_cls
                With c
                    c.id_ctr = Nothing
                    c.id_p_0031 = txt_Id.Text
                    c.id_P_0065 = Gr_Ctr.Rows(i).Cells(0).Text
                    c.ctr_dias_des = Gr_Ctr.Rows(i).Cells(2).Text
                    c.ctr_dias_hst = Gr_Ctr.Rows(i).Cells(3).Text
                End With
                coll.Add(c)

            Next
            If AG.CategoriaRiesgoInserta(coll) = True Then
                Msj.Mensaje(Me.Page, caption, "Datos guardados", TipoDeMensaje._Informacion, "", False)

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Function y Sub"
    Sub LlenaGr()
        Try
            'Dim coll As New Collection

            'Coll_Ctr = CG.ParametrosDevuelve(65)
            Coll_Ctr = CG.TipoRiesgoDevuelve

            Gr_Ctr.DataSource = Coll_Ctr
            Gr_Ctr.DataBind()
            For i = 0 To Gr_Ctr.Rows.Count - 1
                Gr_Ctr.Rows(i).Cells(2).Text = ""
                Gr_Ctr.Rows(i).Cells(3).Text = ""
            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub CargaGr()
        Coll_Ctr = CG.CategoriaRiesgoDevuelve(txt_Id.Text)
        Gr_Ctr.DataSource = Coll_Ctr
        Gr_Ctr.DataBind()
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            RW.ClosePag(Me)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        'txt_Des.Text = ""
        txt_Dia_dsd.Text = ""
        txt_Dia_hst.Text = ""
        txt_TpRLetra.Text = ""
        txt_TPRnumero.Text = ""
        LlenaGr()
    End Sub

    Protected Sub IB_Agregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Agregar.Click
        Try

            'If Not IsNothing(Con.CategoriaRiesgoDevuelve(txt_Id.Text)) Then
            '    LlenaGr()
            'End If

            If txt_TpRLetra.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de riesgo", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_Dia_dsd.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese días desde", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_Dia_hst.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese días hasta", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Val(txt_Dia_dsd.Text) = Val(txt_Dia_hst.Text) Then
                Msj.Mensaje(Me.Page, caption, "Día desde no puede ser igual a día hasta", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Val(txt_Dia_dsd.Text) > Val(txt_Dia_hst.Text) Then
                Msj.Mensaje(Me.Page, caption, "Día hasta debe ser mayor a día desde", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If HF_Po.Value <> 0 Then
                If Gr_Ctr.Rows(HF_Po.Value - 1).Cells(2).Text = "" And Gr_Ctr.Rows.Count <> 1 Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese rangos anteriores", TipoDeMensaje._Informacion, "", False)
                    Exit Sub
                End If
            End If

            For i = 0 To Gr_Ctr.Rows.Count - 1
                If Gr_Ctr.Rows(i).Cells(2).Text = "" Then
                    Gr_Ctr.Rows(i).Cells(2).Text = txt_Dia_dsd.Text
                    Gr_Ctr.Rows(i).Cells(3).Text = txt_Dia_hst.Text
                    Exit For
                End If
            Next


            txt_Dia_dsd.Text = ""
            txt_Dia_hst.Text = ""
            Dim SW As Integer=0
            For i = 0 To Gr_Ctr.Rows.Count - 1
                If Gr_Ctr.Rows(i).Cells(2).Text = "" Or Gr_Ctr.Rows(i).Cells(3).Text = "" Then
                    SW = 1
                End If
            Next

            If SW = 0 Then
                IB_Guardar.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try
            If IsNothing(CG.CategoriaRiesgoDevuelve(txt_Id.Text)) Then
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de insertar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, False)
            Else
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, False)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region



   
End Class
