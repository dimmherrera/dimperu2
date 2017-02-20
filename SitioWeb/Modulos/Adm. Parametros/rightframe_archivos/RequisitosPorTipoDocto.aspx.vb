Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class ClsRequisitosPorTipoDocto
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim Caption As String = "Requisitos Por Tipo Docto."
    Dim Msj As New ClsMensaje
    Dim cd As New ClaseControlDual

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocumentos)
            LlenaRequisitos()

        End If

    End Sub


    Private Sub LlenaRequisitos()
        Dim Coll As Collection

        Coll = cd.RequisitosDevuelveTodos(False)

        Gr_Requisitos.DataSource = Coll
        Gr_Requisitos.DataBind()


        For I = 0 To Gr_Requisitos.Rows.Count - 1

            If Gr_Requisitos.Rows(I).Cells(3).Text = "A" Then
                Gr_Requisitos.Rows(I).Cells(3).Text = "ACTIVO"
            Else
                Gr_Requisitos.Rows(I).Cells(3).Text = "INACTIVO"
            End If

        Next



    End Sub
    

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Guardar.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20011112, Usr, "PRESIONO GUARDAR REQUISITO POR TIPO DE DOCUMENTO") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Msj.Mensaje(Me, "Atención", "¿Desea Guardar los Requisitos?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_gua.UniqueID)


    End Sub


    Protected Sub DP_TipoDocumentos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoDocumentos.SelectedIndexChanged
        Try

            If (DP_TipoDocumentos.SelectedIndex <> 0) Then
                btn_Guardar.Enabled = True
            Else
                btn_Guardar.Enabled = False
            End If

            LlenaRequisitos()

            Dim Coll As Collection

            Coll = cd.RequisitosDevuelvePorTipoDocto(DP_TipoDocumentos.SelectedValue)
            If Coll.Count = 0 Then Exit Sub



            For I = 0 To Gr_Requisitos.Rows.Count - 1

                For x = 1 To Coll.Count

                    If Coll.Item(x).id_req = Gr_Requisitos.Rows(I).Cells(1).Text Then
                        Dim Cb As CheckBox
                        Cb = Gr_Requisitos.Rows(I).FindControl("CB_Req")
                        Cb.Checked = True
                    End If
                Next
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_gua.Click

        Try

            Dim Valida_Ingreso As Boolean = False
            Dim Valida_Req As Boolean = False


            For I = 0 To Gr_Requisitos.Rows.Count - 1

                Dim Cb As CheckBox
                Cb = Gr_Requisitos.Rows(I).FindControl("CB_Req")

                If Cb.Checked Then

                    Dim RXD As New rxd_cls

                    'RXD.id_rxd = Nothing
                    RXD.id_p_0031 = DP_TipoDocumentos.SelectedValue
                    RXD.id_req = CInt(Gr_Requisitos.Rows(I).Cells(1).Text)

                    If cd.RequisitosPorDoctoInserta(RXD) Then
                        Valida_Ingreso = True
                    Else
                        Valida_Ingreso = False
                    End If

                    Valida_Req = True
                Else
                    If Not Valida_Req Then
                        Valida_Req = False
                    End If
                End If

            Next

            If Valida_Req Then

                If Valida_Ingreso Then
                    Msj.Mensaje(Me.Page, Caption, "Se Guardo Plantilla de requisitos", TipoDeMensaje._Informacion)
                    DP_TipoDocumentos.ClearSelection()
                    LlenaRequisitos()
                    btn_Guardar.Enabled = False
                Else
                    Msj.Mensaje(Me.Page, Caption, "No Se Guardo Plantilla de requisitos", TipoDeMensaje._Exclamacion)
                End If
            Else
                Msj.Mensaje(Me.Page, Caption, "Seleccione al menos un requisito ", TipoDeMensaje._Exclamacion)
            End If




        Catch ex As Exception

        End Try
    End Sub
End Class
