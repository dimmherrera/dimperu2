Imports ClsSession.ClsSession
Imports ClsSession.SesionPagos
Imports CapaDatos
Partial Class Pagares
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim caption As String = "Pagaré"
    Dim FG As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
    Dim RW As FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim sesionPagos As New ClsSession.SesionPagos
    Dim sesion As New ClsSession.ClsSession
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim estado As Integer
    Dim CL As New ConsultasLegales
    Dim AL As New ActualizacionesLegales
    Dim Msj As New ClsMensaje

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
                txt_Monto.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                CargaDrop()
                NroPaginacion = 0
                LimpiaControles()
                CargaGrilla()

                'If Not IsNothing(Session("Cliente")) Then
                '    Dim cli As New cli_cls
                '    cli = Session("Cliente")
                '    Txt_Rut_Cli.Text = Format(CDbl(cli.cli_idc), FMT.FCMSD)
                '    Txt_Dig_Cli.Text = cli.cli_dig_ito

                '    CargaRutCli()

                '    CargaGrilla()

                'End If

                sesion.Coll_Pagare = New Collection
            End If
            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',570 ,400,100,100);")
            'IB_Nuevo.Attributes.Add("onClick", "NuevoPagare('NuevoPagare.aspx', 700, 750, 0, 0);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged
        Try
            If CB_Cliente.Checked = True Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                Txt_Rut_Cli.Focus()
                IB_AyudaCli.Enabled = True
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CBox_Monto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBox_Monto.CheckedChanged
        Try
            If CBox_Monto.Checked = True Then
                txt_Monto.CssClass = "clsMandatorio"
                txt_Monto.Text = ""
                txt_Monto.ReadOnly = False
                txt_Monto.Focus()
                txt_Monto_MaskedEditExtender.Enabled = True
            Else
                txt_Monto.CssClass = "clsDisabled"
                txt_Monto.Text = ""
                txt_Monto.ReadOnly = True
                txt_Monto_MaskedEditExtender.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_TPPagare_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_TPPagare.SelectedIndexChanged
        Try
            If Drop_TPPagare.SelectedValue > 0 Then
                Rb_Todos.Checked = False
            Else
                Rb_Todos.Checked = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Todos.CheckedChanged
        Try
            If Rb_Todos.Checked = True Then
                Drop_TPPagare.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Mandato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Mandato.SelectedIndexChanged
        Try
            If Drop_Mandato.SelectedValue = "NADA" Then
                RB_MandatoTodos.Checked = True
            Else
                RB_MandatoTodos.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Gr_Pagare_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Pagare.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Pagare, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Pagare, 'formatable')")
            'e.Row.Attributes.Add("onClick", "ClickPagare(ctl00_ContentPlaceHolder1_Gr_Pagare, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Link_Actualiza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Actualiza.Click
        Try
            Dim rutdsd As String
            Dim ruthst As String
            Dim FDsd As String
            Dim FHst As String
            Dim FProdsd As String
            Dim FProHst As String
            Dim PgrDsd As Integer
            Dim PgrHst As Integer
            Dim MtoDsd As String
            Dim MtoHst As String
            Dim MandatoDsd As String
            Dim MandatoHst As String
            Dim EjeDsd As Integer
            Dim EjeHst As Integer
            Dim SucDsd As Integer
            Dim SucHst As Integer
            If CB_Cliente.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion, "", True)
                    Exit Sub
                End If
                CargaRutCli()
            End If

            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "00000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Txt_Rut_Cli.Text
                ruthst = Txt_Rut_Cli.Text
            End If

            If txt_Fdsd.Text = "" Then
                FDsd = "01/01/1900"
            Else
                FDsd = txt_Fdsd.Text
            End If

            If txt_FHst.Text = "" Then
                FHst = "31/12/2900"
            Else
                FHst = txt_FHst.Text
            End If

            If txt_FechaPro.Text = "" Then
                FProdsd = "01/01/1900"
                FProHst = "31/12/2999"
            Else
                FProdsd = txt_FechaPro.Text
                FProHst = txt_FechaPro.Text
            End If
            If Drop_TPPagare.SelectedValue = 0 Then
                PgrDsd = 0
                PgrHst = 9999
            Else
                PgrDsd = Drop_TPPagare.SelectedValue
                PgrHst = Drop_TPPagare.SelectedValue
            End If
            If txt_Monto.Text = "" Then
                MtoDsd = 0
                MtoHst = 999999999999
            Else
                MtoDsd = txt_Monto.Text
                MtoHst = txt_Monto.Text
            End If

            If Drop_Mandato.SelectedValue = "NADA" Then
                MandatoDsd = "A"
                MandatoHst = "Z"
            Else
                MandatoDsd = Drop_Mandato.SelectedValue
                MandatoHst = Drop_Mandato.SelectedValue
            End If
            If Drop_Ejecutivo.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 9999
            Else
                EjeDsd = Drop_Ejecutivo.SelectedValue
                EjeHst = Drop_Ejecutivo.SelectedValue
            End If


            If CBox_TodasSuc.Checked = True Then
                SucDsd = 0
                SucHst = 9999
            Else
                SucDsd = sesion.Sucursal
                SucHst = sesion.Sucursal

            End If



            sesionPagos.Coll_Pagare = CL.PagareDevuelve(rutdsd, ruthst, FDsd, FHst, _
                             FProdsd, FProHst, PgrDsd, PgrHst, MtoDsd, MtoHst, MandatoDsd, _
                             MandatoHst, EjeDsd, EjeHst, 0, 999999, SucDsd, SucHst, 0, 999999999, 0, 9999, 13)

            If Not IsNothing(sesionPagos.Coll_Pagare) Then
                If sesionPagos.Coll_Pagare.Count > 0 Then
                    Gr_Pagare.DataSource = sesionPagos.Coll_Pagare
                    Gr_Pagare.DataBind()
                    FormatoGrilla()
                    IB_Buscar.Enabled = False
                    IB_Imprime.Enabled = True
                    IB_GeneraImpuesto.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Gr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Gr.Click
        Try

            For i = 0 To Gr_Pagare.Rows.Count - 1
                Gr_Pagare.Rows(i).CssClass = "formatable"
                If HF_Pos.Value >= 0 And HF_Id.Value >= 0 Then
                    Gr_Pagare.Rows(HF_Pos.Value - 1).CssClass = "clicktable"
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            CargaRutCli()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_MandatoTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_MandatoTodos.CheckedChanged
        Try
            If RB_MandatoTodos.Checked = True Then
                Drop_Mandato.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Moneda.SelectedIndexChanged
        If Me.Drop_Moneda.SelectedValue <> 0 Then

            If Me.Drop_Moneda.SelectedValue = 1 Then

                txt_Monto_MaskedEditExtender.Mask = "999,999,999,999"

            ElseIf Me.Drop_Moneda.SelectedValue = 2 Then

                txt_Monto_MaskedEditExtender.Mask = "999,999,999.9999"

            ElseIf Me.Drop_Moneda.SelectedValue = 3 Or Me.Drop_Moneda.SelectedValue = 4 Then

                txt_Monto_MaskedEditExtender.Mask = "999,999,999.99"

            End If

        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img As ImageButton = CType(sender, ImageButton)

        HF_Id.Value = img.ToolTip

        'document.aspnetForm.ctl00_ContentPlaceHolder1_HF_Pos.value = posicion;
        '__doPostBack('ctl00$ContentPlaceHolder1$Link_Gr','');  

        'For i = 0 To Gr_Pagare.Rows.Count - 1
        '    Gr_Pagare.Rows(i).CssClass = "formatable"
        '    If HF_Pos.Value >= 0 And HF_Id.Value >= 0 Then
        '        Gr_Pagare.Rows(HF_Pos.Value - 1).CssClass = "clicktable"
        '    End If
        'Next

        Try

            For I = 0 To Gr_Pagare.Rows.Count - 1

                If (HF_Id.Value = CType(Gr_Pagare.Rows(I).FindControl("Img_Ver"), ImageButton).ToolTip) Then
                    HF_Pos.Value = I + 1

                    If (I Mod 2) = 0 Then
                        Gr_Pagare.Rows(I).CssClass = "selectable"
                    Else
                        Gr_Pagare.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        Gr_Pagare.Rows(I).CssClass = "formatUltcell"
                    Else
                        Gr_Pagare.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try


    End Sub

#End Region

#Region "Function y Sub"
    Sub CargaGr()
        sesionPagos.Coll_Pagare = CL.PagareDevuelve("000000000000", "9999999999999", "01/01/1900", "31/12/2999", _
                            "01/01/1900", "31/12/2999", 0, 99999, 0, 9999999999999, "A", "Z", 0, 99999, 0, 999999, 0, _
                            999999, 0, 999999999, 0, 9999, 13)

        If sesionPagos.Coll_Pagare.Count > 0 Then
            Gr_Pagare.DataSource = sesionPagos.Coll_Pagare
            Gr_Pagare.DataBind()
            FormatoGrilla()
            IB_Buscar.Enabled = False

            Exit Sub
        End If
    End Sub

    Sub CargaRutCli()

        Dim FG As New FuncionesGenerales.FComunes
        Dim CLSCLI As New ClaseClientes
        Dim cli As cli_cls

        cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

        If sesion.valida_cliente <> "" Then
            Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Exit Sub
        Else
            If IsNothing(cli) Then
                Msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If
            Txt_Rut_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.ReadOnly = True
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Raz_Soc.ReadOnly = True
            Txt_Raz_Soc.CssClass = "clsDisabled"
            'CB_Cliente.Checked = False
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            IB_AyudaCli.Enabled = False

        End If

    End Sub

    Sub CargaDrop()
        CG.ParametrosDevuelve(21, True, Drop_TPPagare)
        CG.ParametrosDevuelve(23, True, Me.Drop_Moneda)
        CG.EjecutivosDevuelve(Drop_Ejecutivo, CodEje, 15)
        CG.EjecutivosDevuelve(Drop_Ejecutivo, CodEje, 15)
    End Sub

    Sub FormatoGrilla()
        For i = 1 To sesionPagos.Coll_Pagare.Count
            Select Case sesionPagos.Coll_Pagare.Item(i).id_P_0023
                Case 1 'pesos
                    Gr_Pagare.Rows(i - 1).Cells(2).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Rut_Cliente), FMT.FCMSD) & "-" & sesionPagos.Coll_Pagare.Item(i).cli_dig_ito
                    Gr_Pagare.Rows(i - 1).Cells(9).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Monto), FMT.FCMSD)
                    Gr_Pagare.Rows(i - 1).Cells(14).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Impuesto_Pagare), FMT.FCMSD)
                    'Gr_Pagare.Rows(i - 1).Cells(13).Text = Format(Cdbl(sesionPagos.Coll_Pagare.Item(i).CXC_Cob_Imp), FMT.FCMSD)

                Case 2 'Uf
                    Gr_Pagare.Rows(i - 1).Cells(2).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Rut_Cliente), FMT.FCMSD) & "-" & sesionPagos.Coll_Pagare.Item(i).cli_dig_ito
                    Gr_Pagare.Rows(i - 1).Cells(9).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Monto), FMT.FCMCD4)
                    Gr_Pagare.Rows(i - 1).Cells(14).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Impuesto_Pagare), FMT.FCMCD4)
                    'Gr_Pagare.Rows(i - 1).Cells(13).Text = Format(Cdbl(sesionPagos.Coll_Pagare.Item(i).CXC_Cob_Imp), FMT.FCMCD4)

                Case 3, 4 'n Dolar, Euro
                    Gr_Pagare.Rows(i - 1).Cells(2).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Rut_Cliente), FMT.FCMSD) & "-" & sesionPagos.Coll_Pagare.Item(i).cli_dig_ito
                    Gr_Pagare.Rows(i - 1).Cells(9).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Monto), FMT.FCMCD)
                    Gr_Pagare.Rows(i - 1).Cells(14).Text = Format(CDbl(sesionPagos.Coll_Pagare.Item(i).Impuesto_Pagare), FMT.FCMCD)
                    'Gr_Pagare.Rows(i - 1).Cells(13).Text = Format(Cdbl(sesionPagos.Coll_Pagare.Item(i).CXC_Cob_Imp), FMT.FCMCD)

            End Select

            If sesionPagos.Coll_Pagare.Item(i).id_cxc = 0 Then
                Gr_Pagare.Rows(i - 1).Cells(14).Text = ""
                Gr_Pagare.Rows(i - 1).Cells(15).Text = ""
            End If

            If sesionPagos.Coll_Pagare.Item(i).Mandato = "N" Then
                Gr_Pagare.Rows(i - 1).Cells(6).Text = "No"
            Else
                Gr_Pagare.Rows(i - 1).Cells(6).Text = "Si"
            End If
        Next
        For x = 1 To sesionPagos.Coll_Pagare.Count
            If sesionPagos.Coll_Pagare.Item(x).Fecha_Protesto = "01/01/1900" Then
                Gr_Pagare.Rows(x - 1).Cells(11).Text = ""
            End If
        Next

    End Sub

    Sub LimpiaControles()
        IB_Buscar.Enabled = True
        sesionPagos.Coll_Pagare = New Collection
        Gr_Pagare.DataSource = Nothing
        Gr_Pagare.DataBind()
        CBox_TodasSuc.Checked = True
        CB_Cliente.Checked = False
        txt_Fdsd.Text = ""
        txt_FHst.Text = ""
        txt_FechaPro.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        txt_Monto.Text = ""
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        txt_Monto.CssClass = "clsDisabled"
        txt_Monto.ReadOnly = True
        CBox_Monto.Checked = False
        Rb_Todos.Checked = True
        Drop_TPPagare.ClearSelection()
        RB_MandatoTodos.Checked = True
        Drop_Mandato.ClearSelection()
        Drop_Ejecutivo.ClearSelection()
        IB_Imprime.Enabled = False
        IB_GeneraImpuesto.Enabled = False
        HF_Id.Value = ""
        txt_Fdsd.ReadOnly = False
        txt_FechaPro.ReadOnly = False
        txt_FHst.ReadOnly = False
        txt_Fdsd_CalendarExtender.Enabled = True
        txt_Fdsd_MaskedEditExtender.Enabled = True
        txt_FechaPro_CalendarExtender.Enabled = True
        txt_FechaPro_MaskedEditExtender.Enabled = True
        txt_FHst_CalendarExtender.Enabled = True
        txt_FHst_MaskedEditExtender.Enabled = True
        txt_Monto.ReadOnly = False
        Drop_Ejecutivo.Enabled = True
        Drop_Mandato.Enabled = True
        Drop_TPPagare.Enabled = True
        CB_Cliente.Enabled = True
        CBox_Monto.Enabled = True
        CBox_TodasSuc.Enabled = True
        RB_MandatoTodos.Enabled = True
        Rb_Todos.Enabled = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.ReadOnly = True
        txt_Fdsd.CssClass = "clsTxt"
        txt_FechaPro.CssClass = "clsTxt"
        txt_FHst.CssClass = "clsTxt"
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Drop_Ejecutivo.CssClass = "clsTxt"
        Drop_Mandato.CssClass = "clsTxt"
        Drop_TPPagare.CssClass = "clsTxt"
        txt_Monto.ReadOnly = True
        txt_Monto_MaskedEditExtender.Enabled = False
        IB_Detalle.Enabled = False
        NroPaginacion = 0

    End Sub

    Private Sub CargaGrilla()
        Try
            Dim rutdsd As String
            Dim ruthst As String
            Dim FDsd As String
            Dim FHst As String
            Dim FProdsd As String
            Dim FProHst As String
            Dim PgrDsd As Integer
            Dim PgrHst As Integer
            Dim MtoDsd As String
            Dim MtoHst As String
            Dim MandatoDsd As String
            Dim MandatoHst As String
            Dim EjeDsd As Integer
            Dim EjeHst As Integer
            Dim SucDsd As Integer
            Dim SucHst As Integer

            If CB_Cliente.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                CargaRutCli()
            End If


            If txt_Fdsd.Text <> "" And txt_FHst.Text <> "" Then
                If CDate(txt_Fdsd.Text) > CDate(txt_FHst.Text) Then
                    Msj.Mensaje(Me.Page, caption, "Fecha desde no puede ser mayor a fecha hasta", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "00000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Txt_Rut_Cli.Text
                ruthst = Txt_Rut_Cli.Text
            End If

            If txt_Fdsd.Text = "" Then
                FDsd = "01/01/1900"
            Else
                FDsd = txt_Fdsd.Text
            End If

            If txt_FHst.Text = "" Then
                FHst = "31/12/2900"
            Else
                FHst = txt_FHst.Text
            End If

            If txt_FechaPro.Text = "" Then
                FProdsd = "01/01/1900"
                FProHst = "31/12/2999"
            Else
                FProdsd = txt_FechaPro.Text
                FProHst = txt_FechaPro.Text
            End If
            If Drop_TPPagare.SelectedValue = 0 Then
                PgrDsd = 0
                PgrHst = 9999
            Else
                PgrDsd = Drop_TPPagare.SelectedValue
                PgrHst = Drop_TPPagare.SelectedValue
            End If
            If txt_Monto.Text = "" Then
                MtoDsd = 0
                MtoHst = 9999999999999
            Else
                MtoDsd = txt_Monto.Text
                MtoHst = txt_Monto.Text
            End If

            If Drop_Mandato.SelectedValue = "NADA" Then
                MandatoDsd = "A"
                MandatoHst = "Z"
            Else
                MandatoDsd = Drop_Mandato.SelectedValue
                MandatoHst = Drop_Mandato.SelectedValue
            End If
            If Drop_Ejecutivo.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 9999
            Else
                EjeDsd = Drop_Ejecutivo.SelectedValue
                EjeHst = Drop_Ejecutivo.SelectedValue
            End If

            If CBox_TodasSuc.Checked = True Then
                SucDsd = 0
                SucHst = 9999
            Else
                SucDsd = sesion.Sucursal
                SucHst = sesion.Sucursal
            End If

            sesionPagos.Coll_Pagare = CL.PagareDevuelve(rutdsd, ruthst, FDsd, FHst, _
                             FProdsd, FProHst, PgrDsd, PgrHst, MtoDsd, MtoHst, MandatoDsd, MandatoHst, EjeDsd, EjeHst, 0, _
                             999999, SucDsd, SucHst, 0, 999999999, 0, 9999, 13)

            If Not IsNothing(sesionPagos.Coll_Pagare) Then
                If sesionPagos.Coll_Pagare.Count > 0 Then
                    Gr_Pagare.DataSource = sesionPagos.Coll_Pagare
                    Gr_Pagare.DataBind()
                    FormatoGrilla()
                    IB_Buscar.Enabled = False
                    IB_GeneraImpuesto.Enabled = True
                    IB_Imprime.Enabled = True
                    IB_Detalle.Enabled = True

                    BloqueoControles()
                Else
                    Msj.Mensaje(Me.Page, caption, "No existe pagaré según criterios de búsqueda", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub BloqueoControles()
        'Se agregan Controles de Moneda
        Txt_Dig_Cli.ReadOnly = True
        txt_Fdsd.ReadOnly = True
        txt_FechaPro.ReadOnly = True
        txt_FHst.ReadOnly = True
        txt_Fdsd_CalendarExtender.Enabled = False
        txt_Fdsd_MaskedEditExtender.Enabled = False

        txt_FechaPro_CalendarExtender.Enabled = False
        txt_FechaPro_MaskedEditExtender.Enabled = False
        txt_FHst_CalendarExtender.Enabled = False
        txt_FHst_MaskedEditExtender.Enabled = False
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False
        txt_Monto.ReadOnly = True
        Txt_Rut_Cli.ReadOnly = True
        Drop_Ejecutivo.Enabled = False
        Drop_Mandato.Enabled = False
        Drop_TPPagare.Enabled = False
        Drop_Moneda.Enabled = False
        CB_Cliente.Enabled = False
        CBox_Monto.Enabled = False
        CBox_TodasSuc.Enabled = False
        CBox_Monto.Enabled = False
        RB_MandatoTodos.Enabled = False
        Rb_Todos.Enabled = False
        Rb_Mon.Enabled = False
        Txt_Dig_Cli.CssClass = "clsDisabled"
        txt_Fdsd.CssClass = "clsDisabled"
        txt_FechaPro.CssClass = "clsDisabled"
        txt_FHst.CssClass = "clsDisabled"
        txt_Monto.CssClass = "clsDisabled"
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Drop_Ejecutivo.CssClass = "clsDisabled"
        Drop_Mandato.CssClass = "clsDisabled"
        Drop_TPPagare.CssClass = "clsDisabled"
    End Sub

#End Region

#Region "Botonera"
    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Try
            'RW.AbrePopup(Me, 2, "NuevoPagare.aspx", "NuevoPagare", 680, 450, 250, 250)
            Response.Redirect("NuevoPagare.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        If CB_Cliente.Checked = True Then
            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT de cliente", TipoDeMensaje._Exclamacion, "")
                Exit Sub
            End If
            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese digito verificador", TipoDeMensaje._Exclamacion, "")
                Exit Sub
            End If
        End If
        If CBox_Monto.Checked = True Then
            If txt_Monto.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese monto", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
        End If

        If Trim(txt_FechaPro.Text) <> "" Then
            If Not IsDate(txt_FechaPro.Text) Then
                Msj.Mensaje(Page, caption, "Fecha de protesto errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_FechaPro.Text = ""
                'txt_FechaPro.Focus()
                Exit Sub
            End If
        End If

        If Trim(txt_Fdsd.Text) <> "" Then
            If Not IsDate(txt_Fdsd.Text) Then
                Msj.Mensaje(Page, caption, "Fecha desde errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_Fdsd.Text = ""
                'txt_Fdsd.Focus()
                Exit Sub
            End If
        End If

        If Trim(txt_FHst.Text) <> "" Then
            If Not IsDate(txt_FHst.Text) Then
                Msj.Mensaje(Page, caption, "Fecha hasta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                txt_FHst.Text = ""
                'txt_FHst.Focus()
                Exit Sub
            End If
        End If




        CargaGrilla()

        IB_Detalle.Enabled = True

        'If Gr_Pagare.Rows.Count = 0 Then
        '    Msj.Mensaje(Me.Page, caption, "no existen datos", TipoDeMensaje._Informacion, "", True)
        'End If
    End Sub

    Protected Sub IB_Limpia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpia.Click
        Try
            LimpiaControles()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_GeneraImpuesto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GeneraImpuesto.Click
        Try
            If HF_Pos.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "Seleccione pagaré", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim p As New pgr_cls
            p = CL.PagareDevuelveObjeto(HF_Id.Value)

            If Not IsNothing(p.id_cxc) Then
                Msj.Mensaje(Me.Page, caption, "Pagaré ya tiene impuesto creado", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            RW.AbrePopup(Me, 1, "Impuesto.aspx?Docto=" & HF_Id.Value, "Impuesto", 470, 250, 250, 250)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Imprime_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprime.Click
        Try
            Dim rutdsd As String
            Dim ruthst As String
            Dim FDsd As String
            Dim FHst As String
            Dim FProdsd As String
            Dim FProHst As String
            Dim PgrDsd As Integer
            Dim PgrHst As Integer
            Dim MtoDsd As String
            Dim MtoHst As String
            Dim MandatoDsd As String
            Dim MandatoHst As String
            Dim EjeDsd As Integer
            Dim EjeHst As Integer
            Dim SucDsd As Integer
            Dim SucHst As Integer
            If CB_Cliente.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                CargaRutCli()
            End If

            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "00000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT)
                ruthst = Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT)
            End If

            If txt_Fdsd.Text = "" Then
                FDsd = "01/01/1900"
            Else
                FDsd = txt_Fdsd.Text
            End If

            If txt_FHst.Text = "" Then
                FHst = "31/12/2900"
            Else
                FHst = txt_FHst.Text
            End If

            If txt_FechaPro.Text = "" Then
                FProdsd = "01/01/1900"
                FProHst = "31/12/2999"
            Else
                FProdsd = txt_FechaPro.Text
                FProHst = txt_FechaPro.Text
            End If
            If Drop_TPPagare.SelectedValue = 0 Then
                PgrDsd = 0
                PgrHst = 9999
            Else
                PgrDsd = Drop_TPPagare.SelectedValue
                PgrHst = Drop_TPPagare.SelectedValue
            End If
            If txt_Monto.Text = "" Then
                MtoDsd = 0
                MtoHst = 9999999999999
            Else
                MtoDsd = txt_Monto.Text
                MtoHst = txt_Monto.Text
            End If

            If Drop_Mandato.SelectedValue = "NADA" Then
                MandatoDsd = "A"
                MandatoHst = "Z"
            Else
                MandatoDsd = Drop_Mandato.SelectedValue
                MandatoHst = Drop_Mandato.SelectedValue
            End If
            If Drop_Ejecutivo.SelectedValue = 0 Then
                EjeDsd = 0
                EjeHst = 9999
            Else
                EjeDsd = Drop_Ejecutivo.SelectedValue
                EjeHst = Drop_Ejecutivo.SelectedValue
            End If

            If CBox_TodasSuc.Checked = True Then
                SucDsd = 0
                SucHst = 9999
            Else
                SucDsd = sesion.Sucursal
                SucHst = sesion.Sucursal
            End If

            RW.AbrePopup(Me, 1, "InformePagare.aspx?Rutdsd=" & rutdsd & "&Ruthst=" & ruthst & "&FVigdsd=" & FDsd & "&Fvighst=" _
                         & FHst & "&Fprotdsd=" & FProdsd & "&Fprohst=" & FProHst & "&Pagaredsd=" & PgrDsd & "&Pagarehst=" & PgrHst _
                         & "&Montodsd=" & MtoDsd & "&Montohst=" & MtoHst & "&Mandatodsd=" & MandatoDsd & "&mandatphst=" _
                         & MandatoHst & "&Ejedsd=" & EjeDsd & "&Ejehst=" & EjeHst & "&idPgr_dsd=" & 0 & "&idPgr_hst=" & 999999 _
                         & "&Sucdsd=" & SucDsd & "&Suchst=" & SucHst & "&Doctodsd=" & 0 & "&Doctohst=" & 999999999, "InformePgare" _
                          , 1200, 800, 250, 50)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Detalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Detalle.Click
        Try
            If HF_Id.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "Seleccione un pagaré", ClsMensaje.TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            Else
                Response.Redirect("NuevoPagare.aspx?Docto=" & HF_Id.Value, False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion = 13 Then
                NroPaginacion -= 13
                CargaGrilla()

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If Gr_Pagare.Rows.Count < 13 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If Gr_Pagare.Rows.Count = 13 Then
                NroPaginacion += 13
                CargaGrilla()
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
#End Region

  
  
    
End Class
