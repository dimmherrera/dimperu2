Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos
Partial Class Avales
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim caption As String = "Avales"
    Dim CG As New ConsultasGenerales
    Dim FG As New FuncionesGenerales.FComunes
    Dim CL As New ConsultasLegales
    Dim AL As New ActualizacionesLegales
    Dim Msj As New ClsMensaje
    Dim SesionPagos As New ClsSession.SesionPagos
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim sesion As New ClsSession.ClsSession


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
                NroPaginacion = 0
                CargaDrop()
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

                SesionPagos.Coll_Avales = New Collection

                LimpiaControles()
                ' BusquedaAvales()

                ' If Not IsNothing(Session("Cliente")) Then
                'Dim cli As New cli_cls
                'cli = Session("Cliente")
                'Txt_Rut_Cli.Text = Format(CDbl(cli.cli_idc), FMT.FCMSD)
                'Txt_Dig_Cli.Text = cli.cli_dig_ito
                'TraeCliente()
                BusquedaAvales()

                ' End If

            End If

            'IB_Nuevo.Attributes.Add("onClick", "NuevoAval('NuevoAval.aspx', 700, 750, 0, 0);")
            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',620 ,400,100,100);")

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub CB_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cli.CheckedChanged
        Try
            If CB_Cli.Checked = True Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                Txt_Rut_Cli.Focus()
                IB_AyudaCli.Enabled = True
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Drop_TipoAval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_TipoAval.SelectedIndexChanged
        Try
            If Drop_TipoAval.SelectedValue > 0 Then
                CB_TipoAvalTodos.Checked = False
            Else
                CB_TipoAvalTodos.Checked = True
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub CB_TipoAvalTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TipoAvalTodos.CheckedChanged
        Try
            If CB_TipoAvalTodos.Checked = True Then
                Drop_TipoAval.ClearSelection()
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LinkB_Elimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Elimina.Click
        Try
            AL.EliminaAvales(HF_id_Aval.Value)
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            TraeCliente()

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

   

    'Protected Sub Gr_Avales_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Avales.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Avales, 'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Avales, 'formatable')")
    '        e.Row.Attributes.Add("onClick", "ClickPagare(ctl00_ContentPlaceHolder1_Gr_Avales, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub

    Protected Sub Link_Gr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Gr.Click
        Try
            For i = 0 To Gr_Avales.Rows.Count - 1
                Gr_Avales.Rows(i).CssClass = "formatable"
                If HF_Pos.Value >= 0 And HF_Id.Value > 0 Then
                    Gr_Avales.Rows(HF_Pos.Value - 1).CssClass = "clicktable"

                End If
            Next


        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
#End Region

#Region "Function y Sub"
    Sub CargaDrop()
        Try

            CG.ParametrosDevuelve(26, True, Drop_TipoAval)
            CG.EjecutivosDevuelve(Drop_Eje, CodEje, 15)
            'CG.EjecutivosDevuelve(Drop_Eje, 1, 15)

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub FormatoGr()
        Try

            For i = 1 To SesionPagos.Coll_Avales.Count
                If SesionPagos.Coll_Avales.Item(i).Fecha_est = "01/01/1900" Then
                    Gr_Avales.Rows(i - 1).Cells(21).Text = ""
                End If
                If SesionPagos.Coll_Avales.Item(i).Fecha_Ju = "01/01/1900" Then
                    Gr_Avales.Rows(i - 1).Cells(22).Text = ""
                End If
                Gr_Avales.Rows(i - 1).Cells(2).Text = Format(CLng(SesionPagos.Coll_Avales(i).Rut_Cli), FMT.FCMSD) & "-" & SesionPagos.Coll_Avales(i).Dig_Cli
                Gr_Avales.Rows(i - 1).Cells(4).Text = Format(CLng(SesionPagos.Coll_Avales(i).Rut_Aval), FMT.FCMSD) & "-" & SesionPagos.Coll_Avales(i).Dig_Aval
                Gr_Avales.Rows(i - 1).Cells(13).Text = Format(CLng(SesionPagos.Coll_Avales(i).Monto), FMT.FCMSD)
            Next

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub TraeCliente()
        Try

        
            Dim CLSCLI As New ClaseClientes
            Dim cli As cli_cls

            cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Sub
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                IB_AyudaCli.Enabled = False
                CB_Cli.Checked = False
                CB_Cli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            End If
            Session("Cliente") = cli

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub LimpiaControles()
        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        Txt_Raz_Soc.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        CB_Cli.Checked = False
        CB_TipoAvalTodos.Checked = True
        Drop_Eje.ClearSelection()
        Drop_TipoAval.ClearSelection()
        CB_Suc.Checked = True

        Gr_Avales.DataSource = New Collection
        Gr_Avales.DataBind()
        IB_Detalle.Enabled = False
        IB_Imprimir.Enabled = False
        IB_Buscar.Enabled = True
        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.ReadOnly = True
        Txt_Raz_Soc.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        CB_Cli.Enabled = True
        CB_TipoAvalTodos.Enabled = True
        Drop_Eje.Enabled = True
        Drop_TipoAval.Enabled = True
        Drop_TipoAval.CssClass = "clsTxt"
        CB_Suc.Enabled = True
        Gr_Avales.DataSource = New Collection
        Gr_Avales.DataBind()
        'IB_Detalle.Enabled = False
        IB_Buscar.Enabled = True
    End Sub

    Sub BloqueaControles()
        'Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        ' Txt_Raz_Soc.Text = ""
        'Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        CB_Cli.Enabled = False
        CB_TipoAvalTodos.Enabled = False
        Drop_Eje.Enabled = False
        Drop_TipoAval.Enabled = False
        Drop_TipoAval.CssClass = "clsDisabled"
        CB_Suc.Enabled = False
        'IB_Detalle.Enabled = False
        IB_Buscar.Enabled = True
    End Sub

    Private Sub BusquedaAvales()
        Try
            Dim RutDsd As String
            Dim RutHst As String
            Dim SucDsd As Integer
            Dim SucHst As Integer
            Dim EjeDsd As Integer
            Dim EjeHst As Integer
            Dim AvlDsd As Integer
            Dim AvlHst As Integer

            If CB_Cli.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT de cliente", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese digito verificador", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
            End If
            If Txt_Rut_Cli.Text = "" Then
                RutDsd = "000000000000"
                RutHst = "999999999999"
            Else
                RutDsd = Txt_Rut_Cli.Text
                RutHst = Txt_Rut_Cli.Text
            End If

            If CB_Suc.Checked = True Then
                SucDsd = 0
                SucHst = 9999
            Else
                SucDsd = sesion.Sucursal
                SucHst = sesion.Sucursal
            End If

            If Drop_Eje.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 9999
            Else
                EjeDsd = Drop_Eje.SelectedValue
                EjeHst = Drop_Eje.SelectedValue
            End If





            If Drop_TipoAval.SelectedValue = 0 Then
                AvlDsd = 0
                AvlHst = 9999
            Else
                AvlDsd = Drop_TipoAval.SelectedValue
                AvlHst = Drop_TipoAval.SelectedValue

            End If
            SesionPagos.Coll_Avales = CL.AvalDevueve(RutDsd, RutHst, SucDsd, SucHst, EjeDsd, EjeHst, AvlDsd, AvlHst, 0, 9999999999999)

            If Not IsNothing(SesionPagos.Coll_Avales) Then
                If SesionPagos.Coll_Avales.Count > 0 Then

                    Gr_Avales.DataSource = SesionPagos.Coll_Avales
                    Gr_Avales.DataBind()
                    FormatoGr()
                    IB_Buscar.Enabled = False
                    IB_Detalle.Enabled = True
                    BloqueaControles()
                    IB_Imprimir.Enabled = True
                End If
            Else
                Msj.Mensaje(Me.Page, caption, "No se encontraron datos", TipoDeMensaje._Exclamacion, "", True)
                IB_Imprimir.Enabled = False
                IB_Detalle.Enabled = False
                IB_Buscar.Enabled = True
            End If
            'If Gr_Avales.Rows.Count > 0 Then

            'Else
            '    'Msj.Mensaje(Me.Page, caption, "No se encontraron datos", TipoDeMensaje._Exclamacion, "", True)
            '    IB_Imprimir.Enabled = False
            '    IB_Detalle.Enabled = False
            '    'IB_Buscar.Enabled = True
            'End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub
#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            'If Not Agt.ValidaAccesso(20, 20020206, Usr, "PRESIONA BOTON NUEVO AVAL") Then
            '    Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If


            ' RW.AbrePopup(Me, 2, "NuevoAval.aspx", "NuevoAval", 700, 630, 25, 250)
            Response.Redirect("NuevoAval.aspx", False)
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Limpiar.Click
        Try
            LimpiaControles()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            'If Not Agt.ValidaAccesso(20, 20010206, Usr, "PRESIONA BOTON BUSCAR AVAL") Then
            '    Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If

            If CB_Cli.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT de cliente", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese digito verificador", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
            End If

            BusquedaAvales()
            If Gr_Avales.Rows.Count = 0 Then
                Msj.Mensaje(Me.Page, caption, "No existen datos", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Detalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Detalle.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            'If Not Agt.ValidaAccesso(20, 20030206, Usr, "PRESIONA BOTON DETALLE AVAL") Then
            '    Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            '    Exit Sub
            'End If

            If HF_Id.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "Seleccione un aval", TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                Response.Redirect("NuevoAval.aspx?Id_aval=" & HF_Id.Value, False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20040206, Usr, "PRESIONA BOTON IMPRIMIR INFORME") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim RutDsd As String
            Dim RutHst As String
            Dim SucDsd As Integer
            Dim SucHst As Integer
            Dim EjeDsd As Integer
            Dim EjeHst As Integer
            Dim AvlDsd As Integer
            Dim AvlHst As Integer

            If CB_Cli.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT de cliente", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese digito verificador", TipoDeMensaje._Exclamacion, "")
                    Exit Sub
                End If
            End If
            If Txt_Rut_Cli.Text = "" Then
                RutDsd = "000000000000"
                RutHst = "999999999999"
            Else
                RutDsd = Txt_Rut_Cli.Text
                RutHst = Txt_Rut_Cli.Text
            End If

            If CB_Suc.Checked = True Then
                SucDsd = 0
                SucHst = 9999
            Else
                SucDsd = sesion.Sucursal
                SucHst = sesion.Sucursal
            End If

            If Drop_Eje.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 9999
            Else
                EjeDsd = Drop_Eje.SelectedValue
                EjeHst = Drop_Eje.SelectedValue
            End If

            If Drop_TipoAval.SelectedValue = 0 Then
                AvlDsd = 0
                AvlHst = 9999
            Else
                AvlDsd = Drop_TipoAval.SelectedValue
                AvlHst = Drop_TipoAval.SelectedValue

            End If
            RW.AbrePopup(Me, 1, "InformeAval.aspx?Rutdsd=" & RutDsd & "&Ruthst=" & RutHst & "&Ejedsd=" & EjeDsd _
                         & "&Ejehst=" & EjeHst & "&Sucdsd=" & SucDsd & "&Suchst=" & SucHst & "&AvalDsd=" & AvlDsd & "&AvalHst=" & AvlHst, "InformeAval", 950, 900, 250, 250)

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

#End Region


    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 12 Then
            NroPaginacion -= 12
            BusquedaAvales()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If Gr_Avales.Rows.Count < 12 Then
            Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If Gr_Avales.Rows.Count = 12 Then
            NroPaginacion += 12
            BusquedaAvales()
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img As ImageButton = CType(sender, ImageButton)

        HF_Id.Value = img.ToolTip

        Try


            For I = 0 To Gr_Avales.Rows.Count - 1

                If (HF_Id.Value = Gr_Avales.Rows(I).Cells(1).Text) Then
                    HF_Pos.Value = I + 1

                    If (I Mod 2) = 0 Then
                        Gr_Avales.Rows(I).CssClass = "selectable"
                    Else
                        Gr_Avales.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        Gr_Avales.Rows(I).CssClass = "formatUltcell"
                    Else
                        Gr_Avales.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Sub
End Class
