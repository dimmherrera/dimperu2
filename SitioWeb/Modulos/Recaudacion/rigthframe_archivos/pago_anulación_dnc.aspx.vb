Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_pago_anulación_dnc
    Inherits System.Web.UI.Page
    Dim clasecli As New ClaseClientes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim fmt As New FuncionesGenerales.Variables
    Dim fm As New FuncionesGenerales.ClsLocateInfo
    Dim fc As New FuncionesGenerales.FComunes
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim rec As New ClaseRecaudación
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub Ch_cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_cli.CheckedChanged

        If Me.Ch_cli.Checked Then

            Me.Txt_Rut.ReadOnly = False
            Me.Txt_Rut.Text = ""
            Me.Txt_Rut.CssClass = "clsMandatorio"

            Me.Txt_Dig.ReadOnly = False
            Me.Txt_Dig.Text = ""
            Me.Txt_Dig.CssClass = "clsMandatorio"

            Me.Txt_Rso.Text = ""
            Me.Txt_Rut.Focus()
        Else

            Me.Txt_Rut.ReadOnly = True
            Me.Txt_Rut.Text = ""
            Me.Txt_Rut.CssClass = "clsDisabled"

            Me.Txt_Dig.ReadOnly = True
            Me.Txt_Dig.Text = ""
            Me.Txt_Dig.CssClass = "clsDisabled"

            Me.Txt_Rso.Text = ""


        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, Dr_fact)
            
        End If
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")
    End Sub

    Protected Sub btn_aprobar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_aprobar.Click
        Try
          


            For i = 0 To Me.gr_fnc.Rows.Count - 1

                Dim dr As New CheckBox

                dr = gr_fnc.Rows(i).FindControl("ch_doc")

                If dr.Checked Then
                    SW = SW + 1
                    rec.doctos_no_cedidos_paga(coll_DNC.Item(i + 1).id_nce)

                End If

            Next

            If SW = 0 Then

                msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un Documento para Pagar", ClsMensaje.TipoDeMensaje._Informacion, False)

                Exit Sub
            Else
                btn_buscar_Click(Me, e)
            End If




        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion, False)
        End Try
    End Sub

    Protected Sub btn_anular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_anular.Click
        Try



            For i = 0 To Me.gr_fnc.Rows.Count - 1

                Dim dr As New CheckBox

                dr = gr_fnc.Rows(i).FindControl("ch_doc")

                If dr.Checked Then
                    SW = SW + 1
                    rec.doctos_no_cedidos_anula(coll_DNC.Item(i + 1).id_nce)

                End If

            Next

            If SW = 0 Then

                msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un Documento para Anular", ClsMensaje.TipoDeMensaje._Informacion, False)

                Exit Sub
            Else
                btn_buscar_Click(Me, e)
            End If




        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion, False)
        End Try
    End Sub

    Protected Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_buscar.Click
        Try


            Dim cli_idc1 As String, cli_idc2 As String, est_nom1 As String, est_nom2 As String
            Dim fecha_dde As Date, fecha_hta As Date



            If Me.Ch_cli.Checked Then

                If Txt_Rut.Text <> "" Then


                    cli_idc1 = Format(CLng(Me.Txt_Rut.Text), fmt.FMT_RUT)
                    cli_idc2 = Format(CLng(Me.Txt_Rut.Text), fmt.FMT_RUT)

                End If

            Else
                cli_idc1 = "000000000000"
                cli_idc2 = "9999999999999"

            End If


            If Not IsDate(Me.txt_fec.Text) Then

                fecha_dde = "01/01/1900"
                fecha_hta = "31/12/2999"

            Else
                fecha_dde = Me.txt_fec.Text
                fecha_hta = Me.txt_fec.Text


            End If

            If Me.Dr_fact.SelectedValue = 0 Then

                msj.Mensaje(Me, "Atención", "Debe seleccionar un factoring", ClsMensaje.TipoDeMensaje._Informacion, False)
                Exit Sub
            End If

            If Me.Dr_est.SelectedIndex = 0 Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar un estado", ClsMensaje.TipoDeMensaje._Informacion, False)
                Exit Sub
            Else

                If Me.Dr_est.SelectedValue = "T" Then

                    est_nom1 = ""
                    est_nom2 = "Z"
                Else
                    est_nom1 = Me.Dr_est.SelectedValue
                    est_nom2 = Me.Dr_est.SelectedValue

                End If


            End If

            coll_DNC = New Collection
            coll_DNC = rec.nce_fnc_devuelve(cli_idc1, cli_idc2, Me.Dr_fact.SelectedValue, fecha_dde, fecha_hta, est_nom1, est_nom2)
            Me.gr_fnc.DataSource = coll_DNC
            Me.gr_fnc.DataBind()

            If Me.gr_fnc.Rows.Count = 0 Then
                msj.Mensaje(Me, "Atención", "No se encontraron datos ", ClsMensaje.TipoDeMensaje._Informacion)
                Exit Sub
            Else
                btn_gen_nom.Enabled = True

            End If
            formatogrilla()



        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion, False)
        End Try

    End Sub

    Public Sub formatogrilla()

        For i = 0 To Me.gr_fnc.Rows.Count - 1

            'Numero doc

            Me.gr_fnc.Rows(i).Cells(2).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(2).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(2).HorizontalAlign = HorizontalAlign.Right

            'MONTO DOC
            If coll_DNC.Item(i + 1).id_p_0023 = 1 Then

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMSD)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            ElseIf coll_DNC.Item(i + 1).id_p_0023 = 3 Or coll_DNC.Item(i + 1).id_p_0023 = 4 Then

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMCD)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            Else

                Me.gr_fnc.Rows(i).Cells(4).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(4).Text), fm.FCMCD4)
                Me.gr_fnc.Rows(i).Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If

            'FECHA 
            Me.gr_fnc.Rows(i).Cells(5).Text = Format(CDate(Me.gr_fnc.Rows(i).Cells(5).Text), fmt.FMT_FECHA)

            'RUT CLI
            Me.gr_fnc.Rows(i).Cells(7).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(7).Text), fm.FCMSD) & "-" & fc.Vrut(CDbl(Me.gr_fnc.Rows(i).Cells(7).Text))
            Me.gr_fnc.Rows(i).Cells(7).HorizontalAlign = HorizontalAlign.Right

            'RUT DEU

            Me.gr_fnc.Rows(i).Cells(9).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(9).Text), fm.FCMSD) & "-" & fc.Vrut(CDbl(Me.gr_fnc.Rows(i).Cells(9).Text))
            Me.gr_fnc.Rows(i).Cells(9).HorizontalAlign = HorizontalAlign.Right

            'FECHA ING
            Me.gr_fnc.Rows(i).Cells(11).Text = Format(CDate(Me.gr_fnc.Rows(i).Cells(11).Text), fmt.FMT_FECHA)

            'N ING
            Me.gr_fnc.Rows(i).Cells(13).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(13).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(13).HorizontalAlign = HorizontalAlign.Right

            'N HOJA
            Me.gr_fnc.Rows(i).Cells(14).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(14).Text), fm.FCMSD)
            Me.gr_fnc.Rows(i).Cells(14).HorizontalAlign = HorizontalAlign.Right

            'FACTOR DE CAMBIO
            If coll_DNC.Item(i + 1).id_p_0023 = 1 Then

                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMSD)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            ElseIf coll_DNC.Item(i + 1).id_p_0023 = 3 Or coll_DNC.Item(i + 1).id_p_0023 = 4 Then

                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            Else
                Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD4)
                Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            End If




        Next

    End Sub

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig.TextChanged
        Dim cli As cli_cls

        cli = clasecli.ClientesDevuelve(CLng(Me.Txt_Rut.Text), Me.Txt_Rut.Text)



        Session("Cliente") = cli


        If valida_cliente <> "" Then

            msj.Mensaje(Me, "Atención", valida_cliente, 3)
            Me.Txt_Rut.Text = ""
            Me.Txt_Dig.Text = ""

        Else

            'Inhabilita Textos de RUT y Digito para evitar cambiar RUT sin Limpiar Pantalla
            Me.Txt_Rut.ReadOnly = True
            Me.Txt_Rut.CssClass = "clsDisabled"
            Me.Txt_Dig.ReadOnly = True
            Me.Txt_Dig.CssClass = "clsDisabled"
            Me.Txt_Rso.ReadOnly = True
            Me.Txt_Rso.CssClass = "clsDisabled"

            'Asigna Razón Social / Nombre a Campo Cliente
            Me.Txt_Rso.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)

            msj.Mensaje(Me, "Atención ", "Cliente no encontrado", ClsMensaje.TipoDeMensaje._Informacion)

        End If

    End Sub

    Protected Sub btn_gen_nom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gen_nom.Click
        caso = 1

        Dim rut1 As String
        Dim rut2 As String
        Dim fec_gen As String
        Dim fact As String

        If Txt_Rut.Text = "" Then

            rut1 = "0"
            rut2 = "9999999999999"

        Else

            rut1 = fc.LimpiaRut(Txt_Rut.Text)
            rut2 = fc.LimpiaRut(Txt_Rut.Text)

        End If

        If txt_fec.Text = "" Then
            fec_gen = Date.Now.ToString()
        Else
            fec_gen = txt_fec.Text
        End If

        fact = Dr_fact.SelectedValue

        rw.AbrePopup(Me, 2, "report_nomina_nce.aspx?cli1= " & rut1 & "&cli2= " & rut2 & "&fec1= " & fec_gen & "&fec2= " & fec_gen & "&fact= " & fact & "", "Nomina no Cedidos", 1100, 900, 100, 50)

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click

        Me.Txt_Rut.ReadOnly = True
        Me.Txt_Rut.Text = ""
        Me.Txt_Rut.CssClass = "clsDisabled"

        Me.Txt_Dig.ReadOnly = True
        Me.Txt_Dig.Text = ""
        Me.Txt_Dig.CssClass = "clsDisabled"

        Me.Txt_Rso.Text = ""
        Me.Ch_cli.Checked = False
        coll_DNC = New Collection

        Me.gr_fnc.DataSource = Nothing
        Me.gr_fnc.DataBind()

        Me.Dr_fact.ClearSelection()
        Me.Dr_est.ClearSelection()
        Me.txt_fec.Text = ""
        btn_gen_nom.Enabled = False
    End Sub

    Protected Sub IB_SeleccionDoctos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub
End Class
