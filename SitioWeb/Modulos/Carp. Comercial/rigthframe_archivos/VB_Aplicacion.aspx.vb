Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class VB_Aplicacion
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Aplicaciones"
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            NroPaginacion = 0
            Response.Expires = -1
            CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)
            Txt_Fecha_Desde.Text = Format(CDate(Date.Now), "dd/MM/yyyy")
            Txt_Fecha_Hasta.Text = Format(CDate(Date.Now), "dd/MM/yyyy")
            IB_Next.Enabled = False
            IB_Prev.Enabled = False

        Else

            'For i = 0 To Me.GV_Aplicaciones.Rows.Count - 1

            '    If GV_Aplicaciones.Rows(i).Cells(13).Text = HF_NroAplicacion.Value Then
            '        GV_Aplicaciones.Rows(i).BackColor = Nothing
            '        GV_Aplicaciones.Rows(i).CssClass = "clicktable"
            '    End If

            'Next
            'BuscarAplicaciones()

            'If GV_Aplicaciones.Rows.Count > 0 Then
            '    LB_Refrescar_Click(Me, e)
            'End If


            End If

        Lb_informe.Attributes.Add("onClick", "WinOpen(2,'Informe Aplicaciones.aspx?id_apl=" & HF_NroAplicacion.Value & "','Detalle Aplicación',650,410,200,150);")
        
    End Sub

    Protected Sub CB_TodosEje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TodosEje.CheckedChanged

        If CB_TodosEje.Checked = False Then

            DP_Ejecutivos.ClearSelection()
            DP_Ejecutivos.CssClass = "clsMandatorio"
            DP_Ejecutivos.Enabled = True

        Else

            DP_Ejecutivos.ClearSelection()
            DP_Ejecutivos.CssClass = "clsDisabled"
            DP_Ejecutivos.Enabled = False

        End If

    End Sub

    Protected Sub GV_Aplicaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Aplicaciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Aplicaciones, 'selectable')")
            ' e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Aplicaciones, 'formatable')")
            ' e.Row.Attributes.Add("onClick", "Click_GV_VB(ctl00_ContentPlaceHolder1_GV_Aplicaciones, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click

        NroPaginacion = 0
        BuscarAplicaciones()

        If GV_Aplicaciones.Rows.Count < 0 Then
            IB_Next.Enabled = False
            IB_Prev.Enabled = False
        Else
            IB_Next.Enabled = True
            IB_Prev.Enabled = True
        End If

    End Sub

    Protected Sub Lb_informe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_informe.Click
        Dim rut As String
        rut = Trim(HF_CLI.Value)
        rut = rut.Substring(0, rut.Length - 2)
        rut = Format(CLng(Rut), Var.FMT_RUT)

        Dim apl As New apl_cls

        apl = CG.Aplicacion_Devuelve(HF_NroAplicacion.Value)

        If IsNothing(apl.apl_apb_com) Then
            apl.apl_apb_com = "X"
        End If

        'If apl.apl_apb_com = "S" Then
        'Me.IB_Aprobar.Enabled = False
        Me.IB_Rechazar.Enabled = True
        'ElseIf apl.apl_apb_com = "N" Then
        Me.IB_Aprobar.Enabled = True
        'Me.IB_Rechazar.Enabled = False
        'End If

        MARCA_GRILLA() 'FY 05-05-2012


    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Control Dual"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Private Sub BuscarAplicaciones()
        Try

            Dim Coll As Collection
            Dim eje_dsd As Integer
            Dim eje_hst As Integer

            If DP_Ejecutivos.SelectedValue = 0 Then
                eje_dsd = 0
                eje_hst = 999
            Else
                eje_dsd = DP_Ejecutivos.SelectedValue
                eje_hst = DP_Ejecutivos.SelectedValue
            End If

            Coll = CG.Aplicacion_Devuelve(0, 999999999999, Txt_Fecha_Desde.Text, Txt_Fecha_Hasta.Text, 3, "", eje_dsd, eje_hst, 15)

            GV_Aplicaciones.DataSource = Coll
            GV_Aplicaciones.DataBind()

            If Coll.Count > 0 Then

                IB_Aprobar.Enabled = True
                IB_Rechazar.Enabled = True
                IB_Informe.Enabled = True

                For I = 0 To GV_Aplicaciones.Rows.Count - 1

                    Dim rut As Long = GV_Aplicaciones.Rows(I).Cells(1).Text

                    GV_Aplicaciones.Rows(I).Cells(1).Text = Format(Rut, Fmt.FCMSD) & "-" & FC.Vrut(Rut)
                    GV_Aplicaciones.Rows(I).Cells(5).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(6).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(6).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(7).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(7).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(8).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(8).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(9).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(9).Text), Fmt.FCMSD)

                    GV_Aplicaciones.Rows(I).Cells(10).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(10).Text), Fmt.FCMCD)
                    GV_Aplicaciones.Rows(I).Cells(11).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(11).Text), Fmt.FCMCD)

                    GV_Aplicaciones.Rows(I).Cells(12).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(12).Text), Fmt.FCMSD)

                    Dim boton As ImageButton = GV_Aplicaciones.Rows(I).FindControl("Img_Ver")

                    If Coll.Item(I + 1).apl_apb_com = "S" Then

                        'boton.Enabled = False
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Aplicaciones.Rows(I).BackColor = col

                    ElseIf Coll.Item(I + 1).apl_apb_com = "N" Then

                        'boton.Enabled = False
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#FF9999")
                        GV_Aplicaciones.Rows(I).BackColor = col

                        If Coll.Item(I + 1).VB = "A" Or Coll.Item(I + 1).VB = "R" Then
                            boton.Enabled = False
                        End If

                    End If

                Next

            Else
                Msj.Mensaje(Me.Page, Caption, "No se encontraron aplicaciones", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Fecha_Desde_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fecha_Desde.TextChanged

        If Not IsDate(Me.Txt_Fecha_Desde.Text) Then
            Msj.Mensaje(Me, "Atención", "Fecha desde incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Fecha_Desde.Text = ""
        End If

        If Not IsDate(Me.Txt_Fecha_Hasta.Text) Then
            Exit Sub
        End If

        If DateDiff(DateInterval.Day, CDate(Me.Txt_Fecha_Desde.Text), CDate(Me.Txt_Fecha_Hasta.Text)) < 0 Then

            Msj.Mensaje(Me, "Atención", "Fecha desde no puede ser mayor a fecha hasta ", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Fecha_Desde.Text = ""
        End If

    End Sub

    Protected Sub Txt_Fecha_Hasta_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fecha_Hasta.TextChanged

        If Not IsDate(Me.Txt_Fecha_Hasta.Text) Then
            Msj.Mensaje(Me, "Atención", "Fecha hasta incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Fecha_Hasta.Text = ""
            Exit Sub
        End If

        If Not IsDate(Me.Txt_Fecha_Desde.Text) Then
            Exit Sub
        End If

        If DateDiff(DateInterval.Day, CDate(Me.Txt_Fecha_Desde.Text), CDate(Me.Txt_Fecha_Hasta.Text)) < 0 Then

            Msj.Mensaje(Me, "Atención", "Fecha hasta no puede ser menor a fecha desde ", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Fecha_Hasta.Text = ""
        End If

    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click

        If HF_CLI.Value <> "" Then
            Dim rut As String
            rut = Trim(HF_CLI.Value)
            rut = rut.Substring(0, rut.Length - 2)
            rut = Format(CLng(Rut), Var.FMT_RUT)
            RW.AbrePopup(Me, 1, "Informe Aplicaciones.aspx?id_apl=" & HF_NroAplicacion.Value & "&rut=" & rut & " ", "ObservacionVB", 1000, 700, 100, 100)
        Else
            Msj.Mensaje(Me.Page, "Atención", "Debe seleccionar una aplicación", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        Try
            If btn.ToolTip <> "" Then

                HF_NroAplicacion.Value = btn.ToolTip

                For i = 0 To GV_Aplicaciones.Rows.Count - 1

                    If btn.ToolTip = GV_Aplicaciones.Rows(i).Cells(14).Text Then

                        HF_CLI.Value = GV_Aplicaciones.Rows(i).Cells(1).Text

                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")

                        If GV_Aplicaciones.Rows(i).BackColor <> Nothing Then
                            IB_Aprobar.Enabled = False
                            IB_Rechazar.Enabled = False
                        Else
                            IB_Aprobar.Enabled = True
                            IB_Rechazar.Enabled = True
                            IB_Informe.Enabled = True

                            If HF_NroAplicacion.Value <> "" Then
                                IB_Aprobar.Attributes.Add("onClick", "WinOpen(2, 'ObservacionVB.aspx?Aprobada=S&id=" & HF_NroAplicacion.Value & "', 'ObservacionVB', 500,300,100,100);")
                                IB_Rechazar.Attributes.Add("onClick", "WinOpen(2, 'ObservacionVB.aspx?Aprobada=N&id=" & HF_NroAplicacion.Value & "', 'ObservacionVB', 500,300,100,100);")
                            End If

                        End If

                        Exit For

                    End If

                Next

                'Lb_informe_Click(sender, e)

            End If

            

            MARCA_GRILLA()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, "Error al seleccionar: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub MARCA_GRILLA()

        For I = 0 To GV_Aplicaciones.Rows.Count - 1

            If Me.GV_Aplicaciones.Rows(I).Cells(14).Text = HF_NroAplicacion.Value Then

                If (I Mod 2) = 0 Then
                    GV_Aplicaciones.Rows(I).CssClass = "selectable"
                Else
                    GV_Aplicaciones.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    GV_Aplicaciones.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Aplicaciones.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub


#End Region

#Region "BOTONERA"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        NroPaginacion = 0
        BuscarAplicaciones()

        If GV_Aplicaciones.Rows.Count < 0 Then
            IB_Next.Enabled = False
            IB_Prev.Enabled = False
        Else
            IB_Next.Enabled = True
            IB_Prev.Enabled = True
        End If

        HF_NroAplicacion.Value = ""
        IB_Aprobar.Enabled = False
        IB_Rechazar.Enabled = False

    End Sub

    'Protected Sub IB_Aprobar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aprobar.Click

    '    If Not agt.ValidaAccesso(20, 20010603, Usr, "PRESIONO APROBAR APLICACION") Then
    '        Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
    '        Exit Sub
    '    End If

    '    RW.AbrePopup(Me, 2, "ObservacionVB.aspx?Aprobada=S&id=" & HF_NroAplicacion.Value & "", "ObservacionVB", 500, 300, 100, 100)
    '    'IB_Aprobar.Attributes.Add("onClick", "WinOpen(2, 'ObservacionVB.aspx?Aprobada=S&id=" & HF_NroAplicacion.Value & "', 'ObservacionVB', 500,300,100,100);") 'FY 05-05-2012
    'End Sub

    'Protected Sub IB_Rechazar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Rechazar.Click

    '    If Not agt.ValidaAccesso(20, 20010603, Usr, "PRESIONO RECHAZAR APLICACION") Then
    '        Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
    '        Exit Sub
    '    End If

    '    RW.AbrePopup(Me, 2, "ObservacionVB.aspx?Aprobada=N&id=" & HF_NroAplicacion.Value & "", "ObservacionVB", 500, 300, 100, 100)

    '    'IB_Rechazar.Attributes.Add("onClick", "WinOpen(2, 'ObservacionVB.aspx?Aprobada=N&id=" & HF_NroAplicacion.Value & "', 'ObservacionVB', 500,300,100,100);") 'FY 05-05-2012

    'End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

   
        DP_Ejecutivos.ClearSelection()
        GV_Aplicaciones.DataSource = New Collection
        GV_Aplicaciones.DataBind()
        DP_Ejecutivos.Enabled = False
        DP_Ejecutivos.CssClass = "clsDisabled"
        CB_TodosEje.Checked = True

        Txt_Fecha_Desde.Text = Format(CDate(Date.Now), "dd/MM/yyyy")
        Txt_Fecha_Hasta.Text = Format(CDate(Date.Now), "dd/MM/yyyy")
        NroPaginacion = 0
        HF_NroAplicacion.Value = ""
        IB_Next.Enabled = False
        IB_Prev.Enabled = False

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion < 15 Then
            Msj.Mensaje(Page, Caption, "Ha llegado al comienzo de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If NroPaginacion >= 15 Then
            NroPaginacion -= 15
            BuscarAplicaciones()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Aplicaciones.Rows.Count < 15 Then
            Msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If GV_Aplicaciones.Rows.Count = 15 Then
            NroPaginacion += 15
            BuscarAplicaciones()
        End If
    End Sub
#End Region

End Class
