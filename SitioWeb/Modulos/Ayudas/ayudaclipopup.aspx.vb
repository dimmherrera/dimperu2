Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Ayudas_ayudaclipopup
    Inherits System.Web.UI.Page
    Dim Msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Sesion As New ClsSession.ClsSession
            Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
            Response.Expires = -1
            NroPaginacionCli = 0
            CargaGrilla(0)

        End If

    End Sub

    Private Sub CargaGrilla(ByVal Rut As String)

        Dim Sesion As New ClsSession.ClsSession
        Dim CLI As New ClaseClientes

        Dim Rut_Dsd As Long
        Dim Rut_Hst As Long
        Dim Tip_Dsd As Integer
        Dim Tip_Hst As Integer

        If Txt_Rut.Text <> "" Then
            Rut_Dsd = Txt_Rut.Text
            Rut_Hst = Txt_Rut.Text
        Else
            Rut_Dsd = 0
            Rut_Hst = 9999999999999
        End If

        Tip_Dsd = 0
        Tip_Hst = 999999999

        'CLI.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, Sesion.CodEje, Sesion.CodEje, Tip_Dsd, Tip_Hst, Txt_Rzo.Text.Trim, 1)
        CLI.ClientesActivosDevuelveTodos(GV_Clientes, Rut_Dsd, Rut_Hst, 0, 999, Tip_Dsd, Tip_Hst, Txt_Rzo.Text.Trim, 1)

        If GV_Clientes.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron cliente segun criterio o asignados al ejecutivo", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
        End If

    End Sub

    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "celClickSinBtnCliPopup(GV_Clientes, 'clicktable', 'formatable', 'selectable')")
            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)
            img.Attributes.Add("onClick", "javascript:AceptaCli('" & img.ToolTip & "');")
        End If
    End Sub

    Private Sub FindCargaGrilla()

        Dim Sesion As New ClsSession.ClsSession
        Dim CLI As New ClaseClientes

        'CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, Me.Txt_Rut.Text, Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)


        If Me.Txt_Rut.Text = "" Then
            CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, "", Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)
        Else
            CLI.ClientesActivosLikeDevuelveTodos(GV_Clientes, Replace(CLng(Me.Txt_Rut.Text), ".", ""), Me.Txt_Rzo.Text, Sesion.CodEje, 0, 999)
        End If

        If GV_Clientes.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron cliente segun criterio o asignados al ejecutivo", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
        End If


    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        GV_Clientes.PageIndex = 0
        NroPaginacionCli = 0


        If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
            FindCargaGrilla()
        Else
            CargaGrilla(0)
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        NroPaginacionCli += 8
        If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
            FindCargaGrilla()
        Else
            CargaGrilla(0)
        End If


    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacionCli >= 8 Then
            NroPaginacionCli -= 8
            If Txt_Rut.Text <> "" Or Txt_Rzo.Text <> "" Then
                FindCargaGrilla()
            Else
                CargaGrilla(0)
            End If

        End If

    End Sub
End Class
