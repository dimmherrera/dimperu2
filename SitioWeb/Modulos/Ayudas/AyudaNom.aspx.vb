Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos
Partial Class Modulos_Ayudas_AyudaNom
    Inherits System.Web.UI.Page
    Dim Msj As New ClsMensaje
    Dim Fecha_D, Fecha_H As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Dim Sesion As New ClsSession.ClsSession
            Response.Expires = -1
            NroPaginacion = 0
            CargaGrilla(0)

        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click


        If txt_FDesde.Text <> "" Then
            Fecha_D = txt_FDesde.Text
            Fecha_H = txt_FDesde.Text

        Else
            Fecha_D = "31/12/1900"
            Fecha_H = "31/12/2999"
        End If


        GV_Nomina.PageIndex = 0
        NroPaginacion = 0


        If Me.txt_FDesde.Text <> "" Then
            FindCargaGrilla()
        Else
            CargaGrilla(0)
        End If

    End Sub

    Protected Sub GV_Nomina_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Nomina.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "celClickSinBtnPza(GV_Nomina, 'clicktable', 'formatable', 'selectable')")
            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)
            img.Attributes.Add("onClick", "Nomina(" & e.Row.Cells(0).Text & ")")

        End If

    End Sub



    Private Sub CargaGrilla(ByVal Rut As String)

        Dim Sesion As New ClsSession.ClsSession
        Dim Nom As New ConsultasGenerales

        If txt_FDesde.Text <> "" Then
            Fecha_D = txt_FDesde.Text
            Fecha_H = txt_FDesde.Text

        Else
            Fecha_D = "31/12/1900"
            Fecha_H = "31/12/2999"
        End If



        Nom.Nomina_Retorno(GV_Nomina, Fecha_D, Fecha_H)

        If GV_Nomina.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Cliente", "No se encontraron nominas para esta fecha", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
            Me.txt_FDesde.Text = ""
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion >= 8 Then
            NroPaginacion -= 8
            If Me.txt_FDesde.Text <> "" Then
                FindCargaGrilla()
            Else
                CargaGrilla(0)
            End If

        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        NroPaginacion += 8
        If Me.txt_FDesde.Text <> "" Then
            FindCargaGrilla()
        Else
            CargaGrilla(0)
        End If

    End Sub


    Private Sub FindCargaGrilla()

        Dim Sesion As New ClsSession.ClsSession
        Dim Nomi As New ConsultasGenerales

        If Me.txt_FDesde.Text = "" Then
            Nomi.Nomina_Retorno(GV_Nomina, Fecha_D, Fecha_H)
        Else
            Nomi.Nomina_Retorno(GV_Nomina, Fecha_D, Fecha_H)
        End If


        If GV_Nomina.Rows.Count = 0 Then
            Msj.Mensaje(Me, "Ayuda Nomina", "No se encontraron nominas segun criterio", ClsMensaje.TipoDeMensaje._Informacion, Nothing, False)
             Me.txt_FDesde.Text = ""
        End If


    End Sub




End Class
