Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_pop_up_gest_hoy
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim sesion As New ClsSession.SesionOperaciones
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim msj As New ClsMensaje
    Dim REC As New ClaseRecaudación

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)

        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1

        If Not Me.IsPostBack Then
            NroPaginacion_Deu = 0
            NroPaginacion_Recaudacion = 0
            Carga_Deudores("A", 0, 999)
            Carga_Recaudadores()
            btn_guardar.Enabled = False
            btn_asoc.Enabled = False
        End If
        
        
        'marcagrilla()
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    'Protected Sub marcagrilla()

    '    If Me.pos_rec.Value <> "" Then


    '        For i = 0 To Me.gr_recaudadores.Rows.Count - 1

    '            If i = Val(pos_rec.Value) Then

    '                gr_recaudadores.Rows(Val(pos_rec.Value)).BackColor = Nothing
    '                gr_recaudadores.Rows(Val(pos_rec.Value)).CssClass = "clicktable"

    '            Else

    '                gr_recaudadores.Rows(i).BackColor = Nothing
    '                gr_recaudadores.Rows(i).CssClass = "formatable"


    '            End If

    '        Next

    '    End If


    '    If Me.pos_deu.Value <> "" Then



    '        For i = 0 To Me.gr_deudores.Rows.Count - 1

    '            If i = Val(pos_deu.Value) Then

    '                gr_deudores.Rows(Val(pos_deu.Value)).BackColor = Nothing
    '                gr_deudores.Rows(Val(pos_deu.Value)).CssClass = "clicktable"

    '            Else

    '                gr_deudores.Rows(i).BackColor = Nothing
    '                gr_deudores.Rows(i).CssClass = "formatable"


    '            End If

    '        Next

    '    End If




    'End Sub

    Public Sub Carga_Recaudadores()

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

            zona = 0

            Me.gr_recaudadores.DataSource = REC.Recaudador_asignar_devuelve_Rec(sucursales, 14, cod_suc, CDate(Date.Now).ToShortDateString, Me.RB_HORA.SelectedValue, zona, CodEje)
            Me.gr_recaudadores.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Carga_Deudores(ByVal Hora As String, ByVal CodSuc_dsd As Integer, ByVal CodSuc_hst As Integer)
        Try

            ClsSession.SesionOperaciones.Coll_DOC = New Collection
            ClsSession.SesionOperaciones.arreglo = New ArrayList
            'ClsSession.SesionOperaciones.Coll_DOC = cg.Doctos_Gestion_dia_devuelve("A", 0, 999)
            ClsSession.SesionOperaciones.Coll_DOC = REC.Doctos_Gestion_dia_devuelve(Hora, CodSuc_dsd, CodSuc_hst, 14)
            Me.gr_deudores.DataSource = ClsSession.SesionOperaciones.Coll_DOC
            Me.gr_deudores.DataBind()

            If CodEje = 0 Then
                CodEje = 1
            End If

            If gr_deudores.Rows.Count > 0 Then


                For i = 0 To Me.gr_deudores.Rows.Count - 1

                    If sesion.Coll_DOC.Item(i + 1).id_p_0023 = 1 Then
                        gr_deudores.Rows(i).Cells(6).Text = Format(CDbl(Me.gr_deudores.Rows(i).Cells(6).Text), FMT.FCMSD)
                    ElseIf sesion.Coll_DOC.Item(i + 1).id_p_0023 = 3 Or sesion.Coll_DOC.Item(i + 1).id_p_0023 = 4 Then
                        gr_deudores.Rows(i).Cells(6).Text = Format(CDbl(Me.gr_deudores.Rows(i).Cells(6).Text), FMT.FCMCD)
                    Else
                        gr_deudores.Rows(i).Cells(6).Text = Format(CDbl(Me.gr_deudores.Rows(i).Cells(6).Text), FMT.FCMCD4)
                    End If

                Next
                btn_asoc.Enabled = True
            Else
                btn_asoc.Enabled = False
                msj.Mensaje(Me.Page, "Atención", "No se han encontrado Pagadores segun criterio", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        Try

            Dim sucursales As Integer

            If Me.ch_suc.Checked = True Then
                Carga_Deudores(RB_HORA.SelectedValue, 0, 99)
            Else
                Carga_Deudores(RB_HORA.SelectedValue, Sucursal, Sucursal)
            End If



        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_recaudadores.RowDataBound
    '    Dim strLineAction As String

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(gr_recaudadores, 'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(gr_recaudadores, 'formatable')")

    '        strLineAction = "TraspasoCollCampos(gr_recaudadores, "
    '        strLineAction &= "2,"
    '        strLineAction &= "'clicktable', 'formatable', 'selectable')"
    '        e.Row.Attributes.Add("onClick", strLineAction)
    '    End If
    'End Sub

    Protected Sub gr_deudores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_deudores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ver As ImageButton = e.Row.FindControl("Btn_VerDeu")
            ver.ToolTip = e.Row.RowIndex
        End If
    End Sub

    Protected Sub btn_asoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asoc.Click

        If pos_deu.Value = "" Then

            msj.Mensaje(Me.Page, "Atención", "Debe seleccionar pagador", 3)
            Exit Sub
        End If


        If pos_rec.Value = "" Then
            msj.Mensaje(Me.Page, "Atención", "Debe seleccionar recaudador", 3)

            Exit Sub
        End If


        Me.gr_deudores.Rows(pos_deu.Value).Cells(2).Text = Me.gr_recaudadores.Rows(pos_rec.Value).Cells(2).Text

        If sesion.arreglo.Contains(pos_deu.Value) = False Then
            sesion.arreglo.Add(pos_deu.Value)
            btn_guardar.Enabled = True

            msj.Mensaje(Me.Page, "Atención", "Debe guardar para efectuar los cambios", 3)
        Else
            msj.Mensaje(Me.Page, "Atención", "Ya esta agregada la relación", 3)
        End If


    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click
        Try

            Dim rut_deudor As String
            Dim zona_deudor As Integer
            Dim nro_hoja_recaudacion As Integer
            Dim codigo_ejecutivo As Integer
            Dim fecha_hoja As DateTime
            Dim am_pm As String

            am_pm = Me.RB_HORA.SelectedValue

            If sesion.arreglo.Count <= 0 Then
                msj.Mensaje(Page, "Atención", "No hay datos para guardar", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If


            For i = 0 To sesion.arreglo.Count - 1

                rut_deudor = sesion.Coll_DOC.Item(Val(sesion.arreglo.Item(i)) + 1).deu_ide
                codigo_ejecutivo = Me.gr_deudores.Rows(Val(sesion.arreglo.Item(i))).Cells(2).Text
                nro_hoja_recaudacion = REC.hoja_recaudacion_devuelve(codigo_ejecutivo, Date.Now.ToShortDateString)
                zona_deudor = sesion.Coll_DOC.Item(Val(sesion.arreglo.Item(i)) + 1).id_zon

                REC.asignacion_recaudador_guarda(rut_deudor, zona_deudor, nro_hoja_recaudacion, codigo_ejecutivo, Date.Now.ToShortDateString, am_pm, 999)

                Carga_Deudores("A", 0, 999)
                Carga_Recaudadores()
                btn_guardar.Enabled = False
                btn_asoc.Enabled = False
                msj.Mensaje(Me.Page, "Atención", "Se han guardado los datos", 3)

            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        Me.gr_deudores.DataSource = Nothing
        Me.gr_recaudadores.DataSource = Nothing
        btn_guardar.Enabled = False
        btn_asoc.Enabled = False
        'btn_buscar_Click(Me, e)
    End Sub

    Protected Sub IB_Prev_Deu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Deu.Click
        Try
            If NroPaginacion_Deu = 0 Then
                msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            'If NroPaginacion_Deu < 7 Then
            NroPaginacion_Deu -= 7
            Carga_Deudores("A", 0, 999)
            'End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Deu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Deu.Click
        Try
            If gr_deudores.Rows.Count < 7 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            'If gr_deudores.Rows.Count = 7 Then
            NroPaginacion_Deu += 7
            Carga_Deudores("A", 0, 999)
            'End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Recau_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Recau.Click
        Try
            If NroPaginacion_Recaudacion = 0 Then
                msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            'If NroPaginacion_Recaudacion >= 7 Then
            NroPaginacion_Recaudacion -= 7

            Dim sucursales As Integer
            Dim zona As Integer
            Dim cod_suc As String



            If Me.ch_suc.Checked = True Then
                sucursales = 1
                cod_suc = ""
            Else
                sucursales = 0
                cod_suc = Sucursal
            End If

            zona = 0S

            Me.gr_recaudadores.DataSource = REC.Recaudador_asignar_devuelve_Rec(sucursales, 14, cod_suc, CDate(Date.Now).ToShortDateString, Me.RB_HORA.SelectedValue, zona, CodEje)
            Me.gr_recaudadores.DataBind()
            'marcagrilla()
            'End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Recau_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Recau.Click
        Try
            If gr_recaudadores.Rows.Count < 7 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            'If gr_recaudadores.Rows.Count = 7 Then
            NroPaginacion_Recaudacion += 7

            Dim sucursales As Integer
            Dim zona As Integer
            Dim cod_suc As String

            If Me.ch_suc.Checked = True Then
                sucursales = 1
                cod_suc = ""
            Else
                sucursales = 0
                cod_suc = Sucursal
            End If

            zona = 0S

            Me.gr_recaudadores.DataSource = REC.Recaudador_asignar_devuelve_Rec(sucursales, 14, cod_suc, CDate(Date.Now).ToShortDateString, Me.RB_HORA.SelectedValue, zona, CodEje)
            Me.gr_recaudadores.DataBind()
            'marcagrilla()
            'End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Btn_VerDeu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_deudores.Rows.Count - 1

            If (CType(gr_deudores.Rows(i).FindControl("Btn_VerDeu"), ImageButton).ToolTip = btn.ToolTip) Then
                pos_deu.Value = i
                If (i Mod 2) = 0 Then
                    gr_deudores.Rows(i).CssClass = "selectable"
                Else
                    gr_deudores.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_deudores.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_deudores.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next
    End Sub

    Protected Sub Btn_VerRec_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn2 As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_recaudadores.Rows.Count - 1

            If (gr_recaudadores.Rows(i).Cells(2).Text = btn2.ToolTip) Then
                pos_rec.Value = i
                If (i Mod 2) = 0 Then
                    gr_recaudadores.Rows(i).CssClass = "selectable"
                Else
                    gr_recaudadores.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_recaudadores.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_recaudadores.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next
    End Sub

End Class
