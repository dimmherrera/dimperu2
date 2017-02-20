Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Ayudas_AyudaPlaza
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                Dim coll As New Collection
                Dim Plz As New Object

                Plz = cg.ParametrosAlfanumericoDevuelve(2, False, )

                For Each p In Plz
                    coll.Add(p)
                Next

                gr_Plaza.DataSource = coll
                gr_Plaza.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gr_Plaza_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_Plaza.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btn As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)

            e.Row.Attributes.Add("onClick", "Plaza('" & btn.ToolTip & "', '" & e.Row.Cells(2).Text & " ');")

        End If

    End Sub

End Class
