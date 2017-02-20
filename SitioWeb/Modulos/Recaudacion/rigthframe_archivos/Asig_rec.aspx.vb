Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_Asig_rec
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim RC As New ClaseRecaudación

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Response.Expires = -1
            cg.ZonasDevuelveTodas(Sucursal, True, Me.dr_zona)
            Me.txt_fec.Text = Date.Now.ToShortDateString
            cg.EjecutivosDevuelve(Drop_Recaudador, CodEje, 14)
            NroPaginacion = 0
            '   Cargagrilla()
        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Public Sub Cargagrilla()
        Dim sucursales As Integer
        Dim zona As Integer
        Dim cod_suc As String

        Try

            If Me.ch_suc.Checked = True Then
                sucursales = 1
                cod_suc = ""
            Else
                sucursales = 0
                cod_suc = Sucursal
            End If

            zona = Me.dr_zona.SelectedValue

            'Se agrega Drop con ejecutivos recaudadores

            If Drop_Recaudador.SelectedValue = 0 Then
                Drop_Recaudador.Focus()
                msj.Mensaje(Page, "Atención", "Seleccione recaudador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Me.GridView1.DataSource = RC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, Drop_Recaudador.SelectedValue, 14)
            Me.GridView1.DataBind()


            'Me.GridView1.DataSource = cg.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, CodEje)
            'Me.GridView1.DataBind()

            If GridView1.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se han encontrado datos segun criterio de busqueda", 2)
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20010108, Usr, "PRESIONO BOTON BUSCAR  RECAUDADOR") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        Cargagrilla()
    End Sub

    Protected Sub dr_zona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_zona.SelectedIndexChanged
        If Me.dr_zona.SelectedValue <> 0 Then
            Me.rb_zona.Checked = False
        End If
    End Sub

    Protected Sub rb_zona_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_zona.CheckedChanged
        If Me.rb_zona.Checked Then
            Me.dr_zona.SelectedValue = 0
        End If
    End Sub

    Protected Sub btn_asig_esp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asig_esp.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20030108, Usr, "PRESIONO BOTON ASIGNACION ESPECIAL DE RECAUDADOR") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        rw.AbrePopup(Me, 1, "Asig_esp_rec.aspx?", "AsignacionEspecial", 1000, 730, 100, 200)
    End Sub

    Protected Sub btn_gest_hoy_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gest_hoy.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20020108, Usr, "PRESIONO BOTON GESTION RECAUDADOR") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        rw.AbrePopup(Me, 2, "pop_up_gest_hoy.aspx?", "Asignacion Especial", 1000, 660, 200, 200)
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()

        Me.txt_fec.Text = Date.Now.ToShortDateString
        Me.dr_zona.SelectedValue = 0
        Me.RB_HORA.SelectedValue = "A"
        Me.Drop_Recaudador.SelectedValue = 0
    End Sub

    Protected Sub txt_fec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec.TextChanged
        If Not IsDate(Me.txt_fec.Text) Then
            msj.Mensaje(Me, "Atención", "Fecha incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.txt_fec.Text = ""
            Exit Sub
        End If
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            'If NroPaginacion > 14 Then
            NroPaginacion -= 14
            Cargagrilla()
            'End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If GridView1.Rows.Count < 14 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            'If GridView1.Rows.Count = 14 Then
            NroPaginacion += 14
            Cargagrilla()

            'End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
End Class
