Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes


Partial Class consulta
    Inherits System.Web.UI.Page

#Region "Variables"

    Dim CG As New ConsultasGenerales
    Dim caption As String = "Consulta"
    Dim rw As New FuncionesGenerales.RutinasWeb


#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Rb_n.Checked = True
                Drop_Alfa.Visible = False

                MultiView1.SetActiveView(View1)
            End If
        Catch ex As Exception
            'Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Drop_Nume_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Nume.SelectedIndexChanged
        Try
            If Rb_n.Checked = True Then
                '  cg.ParametrosDevuelveTodos(Me.Drop_par.SelectedItem.Value, True)
            Else
                ' cg.ParametrosAlfanumericoDevuelve(Me.Drop_par.SelectedItem.Value, True)
            End If


            'Gr_con.DataSource = CG.ParametrosDevuelveTodos(Drop_Nume.SelectedValue, False)
            Gr_con.DataSource = CG.ParametrosDevuelveTodos(Drop_Nume.SelectedValue, False)

            Gr_con.DataBind()


            MultiView1.SetActiveView(View1)

        Catch ex As Exception
            'Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub
   
    Protected Sub Drop_Alfa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Alfa.SelectedIndexChanged
        Try
            Gr_alfa.DataSource = Nothing
            Gr_alfa.DataBind()


            Gr_alfa.DataSource = CG.ParametrosAlfanumericoDevuelve(Drop_Alfa.SelectedValue, False)
            Gr_alfa.DataBind()
           
            MultiView1.SetActiveView(View2)

        Catch ex As Exception
            'Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_n_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_n.CheckedChanged

        Try

            If Rb_n.Checked = True Then

                Gr_con.DataSource = Nothing
                Gr_con.DataBind()

                MultiView1.SetActiveView(View1)

                Drop_Nume.Visible = True
                Drop_Alfa.Visible = False
                Rb_a.Checked = False

                Drop_Nume.ClearSelection()
                Drop_Alfa.ClearSelection()


            End If
        Catch ex As Exception
            'Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_a_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_a.CheckedChanged
        Try
            If Rb_a.Checked = True Then
                Gr_alfa.DataSource = Nothing
                Gr_alfa.DataBind()

                MultiView1.SetActiveView(View2)

                Drop_Alfa.Visible = True
                Drop_Nume.Visible = False

                Drop_Nume.ClearSelection()
                Drop_Alfa.ClearSelection()


            End If
        Catch ex As Exception
            'Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            rw.ClosePag(Me)


        Catch ex As Exception
            ' Msj(caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

   


#End Region

End Class
