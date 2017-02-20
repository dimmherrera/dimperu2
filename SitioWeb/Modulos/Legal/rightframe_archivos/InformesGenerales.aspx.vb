Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class InformesGenerales
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim CG As New ConsultasGenerales
    Dim sesionPagos As New ClsSession.SesionPagos
    Dim sesion As New ClsSession.ClsSession
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim caption As String = "Informes Generales"
    Dim Msj As New ClsMensaje
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim F_dsd As String
    Dim F_hst As String
    Dim F_Hasta As String
    Dim sucdsd As Integer
    Dim suchst As Integer
    Dim estClidsd As Integer
    Dim estClihst As Integer
    Dim EjeDsd As Integer
    Dim EjeHst As Integer
    Dim TipoPgrdsd As Integer
    Dim TipoPgrhst As Integer
    Dim F_Pro As String
    Dim F_PropHst As String
    Dim CL As New ConsultasLegales
#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Legal"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Response.Expires = -1
                NroPaginacion = 0
                CargaDrop()
                sesionPagos.Coll_Pagare = New Collection
                txt_Dia.Attributes.Add("Style", "TEXT-ALIGN:right")
                '("Style", "TEXT-ALIGN: right")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Gr_PGR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gr_PGR.SelectedIndexChanged
        Try
            If txt_Dia.Text = "" Then
                txt_FPro.Text = Date.Now

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Dia.TextChanged
        Try
            Dim col As New Collection
            Dim fecha As Date
            If txt_Dia.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese número de días", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            txt_FPro.Text = Format(CDate(Date.Now.Day + 1 & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")
            'txt_FHasta.Text = Format(CDate(Date.Now.Day + Val(txt_Dia.Text) & "/" & Date.Now.Month & "/" & Date.Now.Year), "dd/MM/yyyy")
            txt_FHasta.Text = txt_FPro.Text
            fecha = txt_fhasta.text
            For i = 1 To Val(txt_Dia.Text)



                fecha = fecha.AddDays(1)

                If fecha.DayOfWeek = DayOfWeek.Saturday Then

                    fecha = fecha.AddDays(1)
                End If

                If fecha.DayOfWeek = DayOfWeek.Sunday Then
                    fecha = fecha.AddDays(1)
                End If
                col = CG.Feriados_Devuelve(CDate(txt_FHasta.Text).Month, CDate(txt_FHasta.Text).Year)
                If Not IsNothing(col) Then

                    If col.Count > 0 Then

                        For x = 1 To col.Count

                            If col.Item(x) = fecha Then
                                fecha = fecha.AddDays(1)
                                Exit For
                            End If

                        Next

                    End If


                End If



            Next

            Me.txt_fhasta.text = Format(fecha, "dd/MM/yyyy")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Est_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Est.SelectedIndexChanged
        Try
            If Drop_Est.SelectedValue > 0 Then
                Radio_Todos.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Radio_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Radio_Todos.CheckedChanged
        Try
            If Radio_Todos.Checked = True Then
                Drop_Est.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_dsd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_dsd.TextChanged
        Try
            'If Trim(txt_dsd.Text) <> "" Then
            '    If Not IsDate(txt_dsd.Text) Then
            '        Msj.Mensaje(page, caption, "Fecha erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '        txt_dsd.Text = ""
            '        txt_dsd.Focus()
            '        Exit Sub
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_hst_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_hst.TextChanged
        Try
            'If Trim(txt_hst.Text) <> "" Then
            '    If Not IsDate(txt_hst.Text) Then
            '        Msj.Mensaje(page, caption, "Fecha erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            '        txt_hst.Text = ""
            '        txt_hst.Focus()
            '        Exit Sub
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Eje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Eje.SelectedIndexChanged
        Try
            If Drop_Eje.SelectedValue > 0 Then
                RB_Eje.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Eje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Eje.CheckedChanged
        Try
            If RB_Eje.Checked Then
                Drop_Eje.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Function y Sub"
    Public Sub CargaDrop()
        CG.ParametrosDevuelve( 21,True,Drop_Est)
        CG.EjecutivosDevuelve(Drop_Eje, CodEje, 15)
    End Sub

    Sub FormatoGr()
        Try
            For i = 1 To Coll_Pagare.Count
                Gr_PGR.Rows(i - 1).Cells(0).Text = Format(CLng(Coll_Pagare.Item(i).Rut_Cliente), FMT.FCMSD) & "-" & Coll_Pagare.Item(i).cli_dig_ito
                If Coll_Pagare.Item(i).id_P_0023 = 1 Then
                    Gr_PGR.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_Pagare.Item(i).Monto), FMT.FCMSD)
                ElseIf Coll_Pagare.Item(i).id_P_0023 = 2 Then ' UF
                    Gr_PGR.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_Pagare.Item(i).Monto), FMT.FCMCD4)
                ElseIf Coll_Pagare.Item(i).id_P_0023 = 3 Or Coll_Pagare.Item(i).id_P_0023 = 4 Then ' dolar y euro
                    Gr_PGR.Rows(i - 1).Cells(7).Text = Format(CLng(Coll_Pagare.Item(i).Monto), FMT.FCMCD)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub FormatoGr2()
        Try
            For i = 1 To Coll_Pagare.Count
                Gr_PGR.Rows(i - 1).Cells(0).Text = Format(CDbl(Coll_Pagare.Item(i).Rut_Cliente), FMT.FCMSD) & "-" & Coll_Pagare.Item(i).cli_dig_ito
                'If Coll_Pagare.Item(i).id_P_0023 = 1 Then
                Gr_PGR.Rows(i - 1).Cells(7).Text = Format(CDbl(Coll_Pagare.Item(i).Monto), FMT.FCMSD)
                Gr_PGR.Rows(i - 1).Cells(8).Text = Format(CDbl(Coll_Pagare.Item(i).Deuda_Total), FMT.FCMSD)

            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub Limpiar()
        Try
            Coll_Pagare = New Collection
            Gr_PGR.DataSource = New Collection
            Gr_PGR.DataBind()

            RB_Eje.Checked = True
            Radio_Cliente.SelectedValue = 1
            Radio_Todos.Checked = True
            Drop_Est.ClearSelection()
            txt_FPro.Text = ""
            txt_FHasta.Text = ""
            txt_Dia.Text = ""

            CheckBox_TodSuc.Checked = False
            RB_EstCli.SelectedValue = 1

            txt_dsd.Text = ""
            txt_hst.Text = ""
            txt_FHasta.Text = ""
            RB_Eje.Checked = True
            Drop_Eje.ClearSelection()
            IB_Imprimir.Enabled = False
            IB_Buscar.Enabled = True
            ChecSuc.Checked = False
            Tab_VencPgr.Enabled = True
            Tab_TipoPagare.Enabled = True
            'txt_FPro.CssClass = "clsDisabled"
            'txt_FHasta.CssClass = "clsDisabled"
            txt_Dia.CssClass = "clsMandatorio"
            Drop_Est.CssClass = "clsTxt"
            txt_dsd.CssClass = "clsTxt"
            txt_hst.CssClass = "clsTxt"
            RB_Eje.Enabled = True
            Radio_Cliente.Enabled = True
            Radio_Todos.Enabled = True
            Drop_Est.Enabled = True
            'txt_FPro.ReadOnly = True
            'txt_FHasta.ReadOnly = False
            txt_Dia.ReadOnly = False
            CheckBox_TodSuc.Enabled = True
            RB_EstCli.Enabled = True
            txt_dsd.ReadOnly = False
            txt_FHasta.ReadOnly = True
            RB_Eje.Enabled = True
            Drop_Eje.Enabled = True
            IB_Imprimir.Enabled = False
            IB_Buscar.Enabled = True
            ChecSuc.Enabled = True
            txt_dsd_CalendarExtender.Enabled = True
            txt_dsd_MaskedEditExtender.Enabled = True
            txt_hst_CalendarExtender.Enabled = True
            txt_hst_MaskedEditExtender.Enabled = True
            txt_hst.ReadOnly = False
            NroPaginacion = 0
        Catch ex As Exception

        End Try
    End Sub

    Sub BloquearControles()
        Try

            RB_Eje.Enabled = False
            Radio_Cliente.Enabled = False
            Radio_Todos.Enabled = False
            Drop_Est.Enabled = False
            txt_Dia.CssClass = "clsDisabled"
            Drop_Est.CssClass = "clsDisabled"
            txt_dsd.CssClass = "clsDisabled"
            txt_hst.CssClass = "clsDisabled"
            txt_FHasta.ReadOnly = True
            txt_Dia.ReadOnly = True
            CheckBox_TodSuc.Enabled = False
            RB_EstCli.Enabled = False
            txt_dsd.ReadOnly = True
            txt_FHasta.ReadOnly = True
            txt_dsd_CalendarExtender.Enabled = False
            txt_dsd_MaskedEditExtender.Enabled = False
            txt_hst_CalendarExtender.Enabled = False
            txt_hst_MaskedEditExtender.Enabled = False
            txt_hst.ReadOnly = True
            RB_Eje.Enabled = False
            Drop_Eje.Enabled = False
            'IB_Imprimir.Enabled = False
            IB_Buscar.Enabled = True
            ChecSuc.Enabled = False
            'Tab_VencPgr.Enabled = False
            'Tab_TipoPagare.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"


    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            'Dim sucdsd As Integer
            'Dim suchst As Integer
            'Dim estClidsd As Integer
            'Dim estClihst As Integer
            'Dim EjeDsd As Integer
            'Dim EjeHst As Integer
            'Dim TipoPgrdsd As Integer
            'Dim TipoPgrhst As Integer

            Select Case TabContainer1.ActiveTab.HeaderText
                Case "Tipo Pagaré"
                    If txt_Dia.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Ingrese número de días", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                    If ChecSuc.Checked = True Then
                        sucdsd = 0
                        suchst = 999
                    Else
                        sucdsd = sesion.Sucursal
                        suchst = sesion.Sucursal
                    End If

                    If Radio_Cliente.SelectedValue = 1 Then
                        estClidsd = 0
                        estClihst = 999
                    Else
                        estClidsd = 1
                        estClihst = 1
                    End If

                    If Radio_Todos.Checked = True Then
                        TipoPgrdsd = 0
                        TipoPgrhst = 999
                    Else
                        TipoPgrdsd = Drop_Est.SelectedValue
                        TipoPgrhst = Drop_Est.SelectedValue
                    End If

                    Coll_Pagare = CL.PagareDevuelveTipoPagare(sucdsd, suchst, estClidsd, estClihst, TipoPgrdsd, TipoPgrhst, txt_FPro.Text, txt_FHasta.Text)

                    If Not IsNothing(Coll_Pagare) Then
                        If Coll_Pagare.Count > 0 Then
                            Gr_PGR.DataSource = Coll_Pagare
                            Gr_PGR.DataBind()
                            IB_Buscar.Enabled = False
                            IB_Imprimir.Enabled = True
                            Tab_VencPgr.Enabled = False
                            'Tab_TipoPagare.Enabled = False
                            BloquearControles()
                            'For i = 0 To Gr_PGR.Rows.Count
                            '    Gr_PGR.Columns(8).Visible = True
                            'Next
                            'Label13.Visible = True
                            'Label20.Visible = True
                       
                        End If
                    Else
                        Msj.Mensaje(Me.Page, caption, "No existen datos según criterio de búsqueda", ClsMensaje.TipoDeMensaje._Informacion)
                        Exit Sub
                    End If

                    FormatoGr2()
                Case "Venc. Pagaré"
                    If CheckBox_TodSuc.Checked = True Then
                        sucdsd = 0
                        suchst = 999
                    Else
                        sucdsd = sesion.Sucursal
                        suchst = sesion.Sucursal
                    End If


                    If RB_EstCli.SelectedValue = 1 Then
                        estClidsd = 0
                        estClihst = 999
                    Else
                        estClidsd = 1
                        estClihst = 1
                    End If
                    If Drop_Eje.SelectedValue = 0 Then
                        EjeDsd = 0
                        EjeHst = 999
                    Else
                        EjeDsd = Drop_Eje.SelectedValue
                        EjeHst = Drop_Eje.SelectedValue
                    End If

                    '**************************************************************************************************
                    'Validacion Fechas
                    '**************************************************************************************************
                    If Trim(txt_dsd.Text) <> "" Then
                        If Not IsDate(txt_dsd.Text) Then
                            Msj.Mensaje(Page, caption, "Fecha desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                            txt_dsd.Text = ""
                            'txt_dsd.Focus()
                            Exit Sub
                        End If
                    End If

                    If Trim(txt_hst.Text) <> "" Then
                        If Not IsDate(txt_hst.Text) Then
                            Msj.Mensaje(Page, caption, "Fecha hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                            txt_hst.Text = ""
                            'txt_hst.Focus()
                            Exit Sub
                        End If
                    End If

                    If txt_dsd.Text <> "" And txt_hst.Text <> "" Then
                        If CDate(txt_dsd.Text) > CDate(txt_hst.Text) Then
                            Msj.Mensaje(Page, caption, "Fecha desde no puede ser mayor a fecha hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If
                    End If

                    '**************************************************************************************************
                    If txt_dsd.Text = "" Then
                        F_dsd = "1900/01/01"
                        'F_hst = "2999/12/30"
                    Else
                        F_dsd = txt_dsd.Text
                    End If
                    If txt_hst.Text = "" Then
                        F_Hasta = "2999/12/30"
                        'F_hst = "2999/12/30"
                    Else
                        F_Hasta = txt_hst.Text
                    End If

                    Coll_Pagare = CL.PagareDevuelve("000000000000", "9999999999999", F_dsd, F_Hasta, "1900/01/01", "2999/12/31", 0, 999 _
                                       , 0, 9999999999999, "A", "Z", EjeDsd, EjeHst, 0, 99999, sucdsd, suchst, 0, 999999999, estClidsd, estClihst, 12)

                    If Not IsNothing(Coll_Pagare) Then
                        If Coll_Pagare.Count > 0 Then
                            Gr_PGR.DataSource = Coll_Pagare
                            Gr_PGR.DataBind()
                            FormatoGr()
                            IB_Buscar.Enabled = False
                            IB_Imprimir.Enabled = True
                            Tab_TipoPagare.Enabled = False
                            'Tab_VencPgr.Enabled = False
                            BloquearControles()
                            For i = 0 To Gr_PGR.Rows.Count
                                Gr_PGR.Columns(8).Visible = True
                                'Gr_PGR.Columns(10).Visible = True
                            Next
                            'Label13.Visible = True
                            'Label20.Visible = True

                        End If
                    Else
                        Msj.Mensaje(Me.Page, caption, "No existen datos según criterio de búsqueda", ClsMensaje.TipoDeMensaje._Informacion)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpiar()
            IB_Imprimir.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            Dim Reporte As Integer
            Dim dia As Integer
            Dim Pagare As String
            Dim nomSucursal As String
            Dim Ejecutivo As String

            If RB_EstCli.SelectedValue = 1 Then
                estClidsd = 0
                estClihst = 999
            Else
                estClidsd = 2
                estClihst = 2
            End If

            If Drop_Eje.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 999
                Ejecutivo = "Todos los Ejecutivos"
            Else
                EjeDsd = Drop_Eje.SelectedValue
                EjeHst = Drop_Eje.SelectedValue
                Ejecutivo = Drop_Eje.SelectedItem.Text
            End If

            If txt_FHasta.Text = "" Then
                F_hst = "2999/12/30"
            Else
                F_hst = txt_FHasta.Text
            End If

            If Radio_Todos.Checked = True Then
                TipoPgrdsd = 0
                TipoPgrhst = 999
                Pagare = "Todos los Tipos"
            Else
                TipoPgrdsd = Drop_Est.SelectedValue
                TipoPgrhst = Drop_Est.SelectedValue
                Pagare = Drop_Est.SelectedItem.Text
            End If


            'If txt_hasta.Text = "" Then
            '    F_PropHst = "2999/12/30"
            'Else
            '    F_PropHst = txt_hst.Text
            'End If

            If txt_Dia.Text = "" Then
                dia = 1
            Else
                dia = txt_Dia.Text
            End If
            Select Case TabContainer1.ActiveTab.HeaderText
                Case "Tipo Pagaré"
                    If ChecSuc.Checked = True Then
                        sucdsd = 0
                        suchst = 999
                        nomSucursal = "Todas las Sucursales"
                    Else
                        sucdsd = sesion.Sucursal
                        suchst = sesion.Sucursal
                        Dim s As suc_cls
                        s = CG.SucursalDevuelve(sesion.Sucursal)
                        nomSucursal = s.suc_nom
                    End If



                    If txt_FPro.Text <> "" Then
                        ' F_Pro = "2009/08/18"
                        F_dsd = txt_FPro.Text
                        F_Pro = txt_FPro.Text
                    End If


                    If txt_FHasta.Text = "" Then
                        F_hst = "2999/12/30"
                    Else
                        F_hst = txt_FHasta.Text
                        F_PropHst = txt_FHasta.Text
                    End If

                    Reporte = 1
                Case "Venc. Pagaré"

                    If CheckBox_TodSuc.Checked = True Then
                        sucdsd = 0
                        suchst = 999
                        nomSucursal = "Todas las Sucursales"

                    Else
                        sucdsd = sesion.Sucursal
                        suchst = sesion.Sucursal
                        Dim s As suc_cls
                        s = CG.SucursalDevuelve(sesion.Sucursal)
                        nomSucursal = s.suc_nom
                    End If

                    If txt_dsd.Text = "" Then
                        F_dsd = "1900/01/01"
                        'F_hst = "2999/12/30"
                    Else
                        F_dsd = txt_dsd.Text
                    End If


                    If txt_hst.Text = "" Then
                        F_hst = "2999/12/30"
                    Else
                        F_hst = txt_hst.Text
                    End If
                    Reporte = 2

            End Select
            RW.AbrePopup(Me, 2, "ReportVenPgr.aspx?f_desde=" & F_dsd & "&f_hasta=" & F_hst & "&eje_dsd=" & EjeDsd _
                             & "&eje_hst=" & EjeHst & "&suc_dsd=" & sucdsd & "&suc_hst=" & suchst & "&tpcli_dsd=" & estClidsd _
                             & "&tpcli_hst=" & estClihst & "&tppgr_dsd=" & TipoPgrdsd & "&tppgr_hst=" & TipoPgrhst & "&Fechaproc=" & F_Pro _
                             & "&FechaPro_Hst=" & F_PropHst & "&Dias=" & dia & "&reporte=" & Reporte & "&pagare=" & Pagare & "&suc=" & nomSucursal _
                             & "&eje=" & Ejecutivo, "reporteVctoPgr", 1120, 900, 100, 100)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try

            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            If NroPaginacion >= 12 Then
                NroPaginacion -= 12
                IB_Buscar_Click(Me, e)
            End If


        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, 2)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If Gr_PGR.Rows.Count < 12 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            If Gr_PGR.Rows.Count = 12 Then
                NroPaginacion += 12
                IB_Buscar_Click(Me, e)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, 2)
        End Try
    End Sub

#End Region


  
    
End Class
