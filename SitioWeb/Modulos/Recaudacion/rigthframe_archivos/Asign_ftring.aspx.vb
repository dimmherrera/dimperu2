Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_Asign_ftring
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim fmt As New FuncionesGenerales.Variables
    Dim fm As New FuncionesGenerales.ClsLocateInfo
    Dim fc As New FuncionesGenerales.FComunes
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

        If Not Me.IsPostBack Then

            Response.Expires = -1

            coll_DNC = New Collection
            coll_DNC = REC.nce_fnc_devuelve("000000000000", "9999999999999", 0, CDate("01/01/1900"), CDate("01/01/2099"), "", "Z", 2)

            Me.gr_fact.DataSource = cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring)
            Me.gr_fact.DataBind()

            Me.gr_fnc.DataSource = coll_DNC
            Me.gr_fnc.DataBind()

            formatogrilla()

        End If

        btn_volver.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Protected Sub btn_asoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_asoc.Click

        Try

            If pos_deu.Value = "" Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar un Factoring para asociar", ClsMensaje.TipoDeMensaje._Informacion, False)
                Exit Sub
            End If


            For i = 0 To Me.gr_fnc.Rows.Count - 1

                Dim dr As New CheckBox

                dr = gr_fnc.Rows(i).FindControl("ch_sel")

                If dr.Checked Then

                    SW = SW + 1

                    Dim nce As New nce_cls

                    With nce
                        nce.id_PL_000069 = pos_deu.Value
                        nce.nce_num_doc = Me.gr_fnc.Rows(i).Cells(2).Text
                        nce.cli_idc = Format(CLng(fc.LimpiaRut(Me.gr_fnc.Rows(i).Cells(7).Text)), fmt.FMT_RUT)
                        nce.deu_ide = Format(CLng(fc.LimpiaRut(Me.gr_fnc.Rows(i).Cells(9).Text)), fmt.FMT_RUT)
                        nce.id_p_0031 = Me.gr_fnc.Rows(i).Cells(13).Text
                    End With

                    REC.Doctos_no_cedidos_modifica(nce)

                End If

            Next

            If SW = 0 Then

                msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un Documento para asociar", ClsMensaje.TipoDeMensaje._Informacion, False)
                Exit Sub

            Else

                coll_DNC = New Collection
                coll_DNC = REC.nce_fnc_devuelve("000000000000", "9999999999999", pos_deu.Value, CDate("01/01/1900"), CDate("01/01/2099"), "", "Z", 1)
                Me.gr_fnc.DataSource = coll_DNC
                Me.gr_fnc.DataBind()

                formatogrilla()

            End If

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion, False)
        End Try

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        coll_DNC = New Collection
        coll_DNC = REC.nce_fnc_devuelve("000000000000", "9999999999999", 0, CDate("01/01/1900"), CDate("01/01/2099"), "", "Z", 1)
        Me.gr_fact.DataSource = cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring)
        Me.gr_fact.DataBind()
        Me.gr_fnc.DataSource = coll_DNC
        Me.gr_fnc.DataBind()

        pos_deu.Value = ""

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

            ''N ING
            'Me.gr_fnc.Rows(i).Cells(13).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(13).Text), fm.FCMSD)
            'Me.gr_fnc.Rows(i).Cells(13).HorizontalAlign = HorizontalAlign.Right

            ''N HOJA
            'Me.gr_fnc.Rows(i).Cells(14).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(14).Text), fm.FCMSD)
            'Me.gr_fnc.Rows(i).Cells(14).HorizontalAlign = HorizontalAlign.Right

            ''FACTOR DE CAMBIO
            'If coll_DNC.Item(i + 1).id_p_0023 = 1 Then

            '    Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMSD)
            '    Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            'ElseIf coll_DNC.Item(i + 1).id_p_0023 = 3 Or coll_DNC.Item(i + 1).id_p_0023 = 4 Then

            '    Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD)
            '    Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            'Else
            '    Me.gr_fnc.Rows(i).Cells(15).Text = Format(CDbl(Me.gr_fnc.Rows(i).Cells(15).Text), fm.FCMCD4)
            '    Me.gr_fnc.Rows(i).Cells(15).HorizontalAlign = HorizontalAlign.Right

            'End If

        Next

    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        pos_deu.Value = btn.ToolTip

        For i = 0 To gr_fact.Rows.Count - 1

            If (btn.ToolTip = gr_fact.Rows(i).Cells(1).Text) Then

                If (i Mod 2) = 0 Then
                    gr_fact.Rows(i).CssClass = "selectable"
                Else
                    gr_fact.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    gr_fact.Rows(i).CssClass = "formatUltcell"
                Else
                    gr_fact.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

End Class
