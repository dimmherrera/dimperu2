Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_hoja_ruta

    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim REC As New ClaseRecaudación

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            cg.ZonasDevuelveTodas(Sucursal, True, Me.dr_zona)
            coll_apc = New Collection
            Coll_Cobranza = New Collection
            cg.EjecutivosDevuelve(Drop_Recaudador, CodEje, 14)

            NroPaginacion = 0

            NroPaginacion_Recaudacion = 0


            If CodEje = 0 Then
                'CodEje = 1
            End If

            If Sucursal = 0 Then
                'Sucursal = 1
            End If

            Me.txt_fec.Text = Format(Date.Now, "dd/MM/yyyy")

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

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click

        

        Try

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

            zona = Me.dr_zona.SelectedValue

            coll_apc = New Collection
            Coll_Cobranza = New Collection

            If Drop_Recaudador.SelectedValue = 0 Then
                msj.Mensaje(Page, "Atención", "Seleccione recaudador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Agt As New Perfiles.Cls_Principal

            If Agt.ValidaAccesso(20, 20010208, Usr, "PRESIONO BOTON BUSCAR HOJAS DE RUTA") = False Then
                msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Me.RB_HORA.SelectedValue = "T" Then

                coll_apc = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "A", zona, Drop_Recaudador.SelectedValue, 5)
                gr_rec_am.DataSource = coll_apc
                gr_rec_am.DataBind()

                Coll_Cobranza = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "P", zona, Drop_Recaudador.SelectedValue, 5)

                gr_rec_pm.DataSource = Coll_Cobranza
                gr_rec_pm.DataBind()

                If Me.gr_rec_am.Rows.Count = 0 And Me.gr_rec_pm.Rows.Count = 0 Then
                    msj.Mensaje(Me.Page, "Atención", "No se han encontrado datos segun criterio", 2)
                End If

            ElseIf Me.RB_HORA.SelectedValue = "A" Then

                coll_apc = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, Drop_Recaudador.SelectedValue, 5)

                Me.gr_rec_am.DataSource = coll_apc
                Me.gr_rec_am.DataBind()

                If Me.gr_rec_am.Rows.Count = 0 Then
                    msj.Mensaje(Me.Page, "Atención", "No se han encontrado datos segun criterio", 2)
                End If

            Else

                Coll_Cobranza = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, Drop_Recaudador.SelectedValue, 5)

                Me.gr_rec_pm.DataSource = Coll_Cobranza
                Me.gr_rec_pm.DataBind()

                If Me.gr_rec_pm.Rows.Count = 0 Then
                    msj.Mensaje(Me.Page, "Atención", "No se han encontrado datos segun criterio", 2)
                End If

            End If

        Catch ex As Exception

        End Try

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

    Protected Sub gr_rec_am_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_rec_am.RowDataBound
        'Dim strLineAction As String

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_rec_am, 'selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_rec_am, 'formatable')")

        '    strLineAction = "TraspasoCollCampos(ctl00_ContentPlaceHolder1_gr_rec_am, "
        '    strLineAction &= "3,"
        '    strLineAction &= "'clicktable', 'formatable', 'selectable')"
        '    e.Row.Attributes.Add("onClick", strLineAction)
        'End If
    End Sub

    Protected Sub gr_rec_pm_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_rec_pm.RowDataBound
        'Dim strLineAction As String

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_rec_pm, 'selectable')")
        '    e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_gr_rec_pm, 'formatable')")

        '    strLineAction = "TraspasoCollCampos(ctl00_ContentPlaceHolder1_gr_rec_pm, "
        '    strLineAction &= "4,"
        '    strLineAction &= "'clicktable', 'formatable', 'selectable')"
        '    e.Row.Attributes.Add("onClick", strLineAction)
        'End If
    End Sub

    Protected Sub Desmarque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desmarque.Click

        If Me.pos_am.Value = "" Then


            For i = 0 To Me.gr_rec_am.Rows.Count - 1
                Me.gr_rec_am.Rows(i).CssClass = "formatable"
            Next

            For i = 0 To Me.gr_rec_pm.Rows.Count - 1
                Me.gr_rec_pm.Rows(i).CssClass = "formatable"
                If Val(Me.pos_pm.Value) = i + 1 Then
                    Me.gr_rec_pm.Rows(i).CssClass = "clicktable"
                End If
            Next

        ElseIf Me.pos_pm.Value = "" Then


            For i = 0 To Me.gr_rec_pm.Rows.Count - 1
                Me.gr_rec_pm.Rows(i).CssClass = "formatable"
            Next


            For i = 0 To Me.gr_rec_am.Rows.Count - 1

                Me.gr_rec_am.Rows(i).CssClass = "formatable"


                If Val(Me.pos_am.Value) = i + 1 Then

                    Me.gr_rec_am.Rows(i).CssClass = "clicktable"

                End If

            Next


        End If

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click

        Me.gr_rec_am.DataSource = Nothing
        Me.gr_rec_am.DataBind()

        gr_rec_pm.DataSource = Nothing
        Me.gr_rec_pm.DataBind()

        Me.txt_fec.Text = Format(Date.Now, "dd/MM/yyyy")

        Me.dr_zona.SelectedValue = 0
        Me.RB_HORA.SelectedValue = "T"
        Me.ch_suc.Checked = False

        Me.Drop_Recaudador.SelectedValue = 0

        pos_am.Value = ""
        pos_pm.Value = ""


    End Sub

    Protected Sub btn_rpt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_rpt.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20020208, Usr, "PRESIONO BOTON VER INFORME DE  HOJAS DE RUTA") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim suc1, suc2 As Integer
        Dim cod As Integer
        Dim am_pm As String

        If Me.ch_suc.Checked Then

            suc1 = 0
            suc2 = 999

        Else
            suc1 = Sucursal
            suc2 = Sucursal

        End If

        If Me.pos_am.Value = "" And Me.pos_pm.Value = "" Then

            msj.Mensaje(Me.Page, "Atención", "Debe seleccionar un recaudador", 2)
            Exit Sub

        ElseIf Me.pos_am.Value <> "" Then

            cod = coll_apc.Item(Val(pos_am.Value + 1)).id_eje
            am_pm = "A"
        ElseIf Me.pos_pm.Value <> "" Then

            cod = Coll_Cobranza.Item(Val(pos_pm.Value + 1)).id_eje
            am_pm = "P"
        End If

        rw.AbrePopup(Me, 1, "reporte_hoja_ruta.aspx?fecha=" & txt_fec.Text & "&am_pm=" & am_pm _
                             & "&eje=" & cod & "&suc1=" & suc1 & "&suc2=" & suc2 & "", "EmisiónHojadeRuta", 760, 660, 200, 200)
    End Sub

    Protected Sub lb_temas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_temas.Click
        Pagina = Page.AppRelativeVirtualPath
        Response.Redirect(Pagina, False)
    End Sub

    Protected Sub txt_fec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_fec.TextChanged
        If Not IsDate(Me.txt_fec.Text) Then
            msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.txt_fec.Text = ""
            Exit Sub
        End If
    End Sub

    Public Sub CargaGrilla_PM()
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

            coll_apc = New Collection
            Coll_Cobranza = New Collection

            If Drop_Recaudador.SelectedValue = 0 Then
                msj.Mensaje(Page, "Atención", "Seleccione recaudador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Me.RB_HORA.SelectedValue = "T" Then
                'Se cambia parametro CodEje por recaudador que se seleccione en el drop_Recaudador
                'A. Saldivar 10/09/2010
                'coll_apc = cg.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "A", zona, CodEje)

                'coll_apc = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "A", zona, Drop_Recaudador.SelectedValue)
                'gr_rec_am.DataSource = coll_apc
                'gr_rec_am.DataBind()

                Coll_Cobranza = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "P", zona, Drop_Recaudador.SelectedValue, 5)
                gr_rec_pm.DataSource = Coll_Cobranza
                gr_rec_pm.DataBind()

            Else

                Coll_Cobranza = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, Drop_Recaudador.SelectedValue, 5)

                Me.gr_rec_pm.DataSource = Coll_Cobranza
                Me.gr_rec_pm.DataBind()

            End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub Cargagrilla_AM()
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

            coll_apc = New Collection
            Coll_Cobranza = New Collection

            If Drop_Recaudador.SelectedValue = 0 Then
                msj.Mensaje(Page, "Atención", "Seleccione recaudador", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Me.RB_HORA.SelectedValue = "T" Then
                'Se cambia parametro CodEje por recaudador que se seleccione en el drop_Recaudador
                'A. Saldivar 10/09/2010
                'coll_apc = cg.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "A", zona, CodEje)

                coll_apc = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), "A", zona, Drop_Recaudador.SelectedValue, 5)
                gr_rec_am.DataSource = coll_apc
                gr_rec_am.DataBind()


            ElseIf Me.RB_HORA.SelectedValue = "A" Then

                coll_apc = REC.Recaudador_asignar_devuelve(sucursales, 14, cod_suc, CDate(Me.txt_fec.Text), Me.RB_HORA.SelectedValue, zona, Drop_Recaudador.SelectedValue, 5)
                Me.gr_rec_am.DataSource = coll_apc
                Me.gr_rec_am.DataBind()

            End If
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Btn_verAM_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_rec_am.Rows.Count - 1

            If (btn.ToolTip = gr_rec_am.Rows(i).Cells(2).Text) Then
                pos_am.Value = i
                pos_pm.Value = ""
                If (i Mod 2) = 0 Then
                    gr_rec_am.Rows(i).CssClass = "selectable"
                Else
                    gr_rec_am.Rows(i).CssClass = "selectableAlt"

                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_rec_am.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_rec_am.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

        For x = 0 To gr_rec_pm.Rows.Count - 1
            gr_rec_pm.Rows(x).CssClass = "selectableAlt"
            If (x Mod 2) = 0 Then
                gr_rec_pm.Rows(x).CssClass = "formatUltcell"
            Else
                gr_rec_pm.Rows(x).CssClass = "formatUltcellAlt"
            End If
        Next

    End Sub

    Protected Sub Btn_verPM_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To gr_rec_pm.Rows.Count - 1

            If (btn.ToolTip = gr_rec_pm.Rows(i).Cells(2).Text) Then
                pos_am.Value = ""
                pos_pm.Value = i
                If (i Mod 2) = 0 Then
                    gr_rec_pm.Rows(i).CssClass = "selectable"
                Else
                    gr_rec_pm.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_rec_pm.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_rec_pm.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

        For x = 0 To gr_rec_am.Rows.Count - 1
            gr_rec_am.Rows(x).CssClass = "selectableAlt"
            If (x Mod 2) = 0 Then
                gr_rec_am.Rows(x).CssClass = "formatUltcell"
            Else
                gr_rec_am.Rows(x).CssClass = "formatUltcellAlt"
            End If
        Next

    End Sub

End Class
