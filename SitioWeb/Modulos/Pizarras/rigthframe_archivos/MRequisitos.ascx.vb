Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_MRequisitos
    Inherits System.Web.UI.UserControl

    Private sw As Boolean
    Private Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim CD As New ClaseControlDual
    
    Protected Sub IB_GuardarRequisitos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GuardarRequisitos.Click

        Try

            If Not agt.ValidaAccesso(20, 20020103, Usr, "PRESIONO GUARDAR REQUISITOS") Then
                Msj.Mensaje(Me.Page, "Requisitos", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '***************************************************************************************
            'Valida que todos los requisitos se cumplan y los guarda. 

            Dim AG As New ActualizacionesGenerales


            For I = 0 To Gr_Requisitos.Rows.Count - 1

                Dim rxo As New rxo_cls
                Dim CB As CheckBox

                rxo.id_rxo = Gr_Requisitos.Rows(I).Cells(1).Text
                rxo.id_ope = NroOperacion

                CB = Gr_Requisitos.Rows(I).FindControl("CB_Req")

                If CB.Checked Then
                    rxo.id_eje = CodEje
                    rxo.rxo_est = "A"
                    rxo.rxo_fec_apb = Date.Now
                Else
                    rxo.id_eje = CodEje
                    rxo.rxo_est = "P"
                    rxo.rxo_fec_apb = Date.Now
                End If

                CD.RequisitosPorOperacionAprueba(rxo)

            Next

            LlenaRequisitos()

            Msj.Mensaje(Me.Page, "Requisitos", "Requisitos Guardados para esta Operación", ClsMensaje.TipoDeMensaje._Informacion)

            '***************************************************************************************
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            HF_Ope.Value = 0
        Else
            If NroOperacion <> HF_Ope.value Then
                HF_Ope.value = NroOperacion
                LlenaRequisitos()
            End If
        End If

    End Sub

    Private Sub LlenaRequisitos()

        Try

            Dim Coll As Collection
            Dim CG As New ConsultasGenerales

            'Coll = CG.RequisitosDevuelveTodos(False)

            Coll = CD.RequisitosDevuelvePorOperacion(NroOperacion)

            Gr_Requisitos.DataSource = Coll
            Gr_Requisitos.DataBind()


            For x = 0 To Gr_Requisitos.Rows.Count - 1

                'For I = 0 To 

                Dim Cb As CheckBox
                Cb = Gr_Requisitos.Rows(x).FindControl("CB_Req")

                Select Case Coll.Item(x + 1).Estado
                    Case "P"
                        Cb.Checked = False
                    Case "A"
                        Cb.Checked = True
                End Select

                'Next

            Next

            'HabilitaCondicion(True)


        Catch ex As Exception

        End Try


    End Sub


    'Protected Sub IB_Todos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Todos.Click

    '    Try


    '        For I = 0 To Gr_Requisitos.Rows.Count - 1

    '            Dim CB As CheckBox

    '            CB = Gr_Requisitos.Rows(I).FindControl("CB_Req")
    '            CB.Checked = True

    '        Next

    '    Catch ex As Exception

    '    End Try

    'End Sub


    Protected Sub IB_Todos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

     
            Dim cb As New CheckBox
            For i = 0 To Gr_Requisitos.Rows.Count - 1
                cb = Gr_Requisitos.Rows(i).FindControl("CB_Req")
                cb.Checked = True
            Next


        Catch ex As Exception
            Msj.Mensaje(Page, "Requisitos", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

End Class
