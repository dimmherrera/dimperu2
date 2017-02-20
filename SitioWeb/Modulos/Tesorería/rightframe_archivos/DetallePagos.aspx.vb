Imports CapaDatos
Partial Class Modulos_Tesorería_rightframe_archivos_DetallePagos
    Inherits System.Web.UI.Page

#Region "Declaración de Variables Locales"

    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim RC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra Tesoreria"
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim Pagos As New ClsSession.SesionPagos
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim MSJ As New ClsMensaje
    Dim PGO As New ClasePagos
    Dim CTA As New ClaseCuentas
#End Region

#Region "BOTONERA"

    Protected Sub IB_Cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cancelar.Click
        'RW.CloseModal(Me.Page, "ctl00$ContentPlaceHolder1$LB_Buscar")
        RW.ClosePag(Me)
    End Sub

    Protected Sub IB_Aceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aceptar.Click

        Try

            Select Case Request.QueryString("AP")
                Case "A"
                    If PGO.AnulaPagos(Val(Request.QueryString("id")), Nothing, "A", "R", 0) Then
                        MSJ.Mensaje(Me, "Atención", "Se ha guardado correctamente", ClsMensaje.TipoDeMensaje._Exclamacion, False)
                    End If
                Case "P"
                    If ValidaProtesto() Then
                        If PGO.AnulaPagos(Val(Request.QueryString("id")), DP_Motivo.SelectedValue, "P", "R", 0) Then
                            CreaCuentaPorCobrar()
                            MSJ.Mensaje(Me, "Atención", "Se ha guardado correctamente", ClsMensaje.TipoDeMensaje._Exclamacion, False)
                        End If
                    Else
                        Exit Sub
                    End If
            End Select

            IB_Aceptar.Enabled = False

        Catch ex As Exception
            MSJ.Mensaje(Me, "Atención", "Problemas al grabar", ClsMensaje.TipoDeMensaje._Exclamacion, False)
        End Try

    End Sub

#End Region

#Region "SUB"

    Private Sub CreaCuentaPorCobrar()

        Try

            Dim cxc As New cxc_cls

            With cxc
                .id_cxc = Nothing
                .id_P_0023 = DP_Moneda.SelectedValue
                .id_P_0041 = 5
                .id_P_0057 = 1 'Estado
                .cxc_mto = CDbl(Txt_Comision.Text) + CDbl(Txt_Iva.Text)
                .cxc_sal = CDbl(Txt_Comision.Text) + CDbl(Txt_Iva.Text)
                .cxc_fec = Date.Now.ToShortDateString
                .cxc_fac_cam = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Date.Now.ToShortDateString).par_val
                .cxc_des = "PROTESTO NRO. DOC. PAGO:  " & Request.QueryString("id")
                .cli_idc = Format(Pagos.RutCliente, Var.FMT_RUT)
                .id_doc = CInt(Gv_Doctos.Rows(0).Cells(3).Text)
            End With

            If CTA.CxcInserta(cxc) Then

            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Function ValidaPagosMarcado() As Boolean

        For I = 0 To GV_Pagos.Rows.Count - 1
            If CType(GV_Pagos.Rows(I).FindControl("CB"), CheckBox).Checked Then
                Return True
            End If
        Next

        Return False

    End Function

    Private Sub BuscarPagos(ByVal Id As Integer)

        Try

            Dim Coll_Dpo As Collection


            Coll_Dpo = PGO.PagosPorId_Devuelve(Id)

            GV_Pagos.DataSource = Coll_Dpo
            GV_Pagos.DataBind()

            For I = 0 To GV_Pagos.Rows.Count - 1

                GV_Pagos.Rows(I).Cells(2).Text = Format(CDbl(GV_Pagos.Rows(I).Cells(2).Text), Fmt.FCMSD)
                GV_Pagos.Rows(I).CssClass = "selectable"

                Select Case Coll_Dpo.Item(I + 1).ing_vld_rcz
                    Case "V" : GV_Pagos.Rows(I).BackColor = Drawing.Color.LightGreen
                    Case "R" : GV_Pagos.Rows(I).BackColor = Drawing.Color.Red
                End Select

            Next

            Dim Coll As Collection

            HF_Id_Ing.Value = Coll_Dpo.Item(1).id_ing
            Coll = CG.TipoDeIngresoDevuelve(HF_Id_Ing.Value)

            GV_Detalle.DataSource = Coll
            GV_Detalle.DataBind()

            For I = 0 To GV_Detalle.Rows.Count - 1
                GV_Detalle.Rows(I).Cells(2).Text = Format(CDbl(GV_Detalle.Rows(I).Cells(2).Text), Fmt.FCMSD)
            Next

            
            Gv_Doctos.DataSource = Nothing
            Gv_Doctos.DataBind()

            GV_CxC.DataSource = Nothing
            GV_CxC.DataBind()


        Catch ex As Exception
            MSJ.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Private Sub LimpiaGridView()

        'GV_Cli_Deu.DataSource = Nothing
        'GV_Cli_Deu.DataBind()

        GV_Pagos.DataSource = Nothing
        GV_Detalle.DataSource = Nothing

        GV_Pagos.DataBind()
        GV_Detalle.DataBind()

        Gv_Doctos.DataSource = Nothing
        Gv_Doctos.DataBind()

        GV_CxC.DataSource = Nothing
        GV_CxC.DataBind()

    End Sub

    Private Sub MarcaTipo()

        For I = 0 To GV_Detalle.Rows.Count - 1
            If I = (HF_Pos_Doc_CxC.Value - 1) Then
                If (I Mod 2) = 0 Then
                    GV_Detalle.Rows(I).CssClass = "selectable"
                Else
                    GV_Detalle.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    GV_Detalle.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Detalle.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

    End Sub

    Private Function ValidaProtesto() As Boolean

        Try

            If DP_Motivo.SelectedIndex = 0 Then
                MSJ.Mensaje(Me, Caption, "Debe Ingresar el motivo del protesto", TipoDeMensaje._Informacion, , False)
                Return False
            End If

            If DP_Moneda.SelectedIndex = 0 Then
                MSJ.Mensaje(Me, Caption, "Debe Ingresar el Moneda del protesto", TipoDeMensaje._Informacion, , False)
                Return False
            End If

            If Txt_Comision.Text = "" Then
                MSJ.Mensaje(Me, Caption, "Debe Ingresar el Comision del protesto", TipoDeMensaje._Informacion, , False)
                Return False
            End If

            Return True

        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            CG.ParametrosDevuelve(TablaParametro.MotivosDeProtestos, True, DP_Motivo)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_Moneda)

            If Request.QueryString("id") <> "" And Request.QueryString("AP") <> "" Then
                BuscarPagos(Request.QueryString("id"))

                'Si es protesto escondemos los controles 
                Select Case Request.QueryString("AP")
                    Case "A"
                        Lb_Motivo.Visible = False
                        Lb_Moneda.Visible = False
                        Lb_Comision.Visible = False
                        Lb_Iva.Visible = False

                        DP_Motivo.Visible = False
                        DP_Moneda.Visible = False
                        Txt_Comision.Visible = False
                        Txt_Iva.Visible = False
                        IB_Aceptar.ToolTip = "Aceptar Anulación de Pago"
                    Case "P"
                        IB_Aceptar.ToolTip = "Aceptar Protesto de Pago"
                End Select

                IB_Aceptar.Enabled = True

            End If

        End If

        'IB_Cancelar.Attributes.Add("onClick", "window.close();")

    End Sub

    Protected Sub LB_BuscaDocCxC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDocCxC.Click
        Try

            Dim Coll As Collection

            GV_CxC.DataSource = Nothing
            GV_CxC.DataBind()

            Gv_Doctos.DataSource = Nothing
            Gv_Doctos.DataBind()

            Select Case HF_Tipo.Value

                Case 1

                    Coll = CG.PagosDocumentosCxCDevuelveDetalle(HF_Id_Ing.Value, 1)

                    GV_CxC.DataSource = Coll
                    GV_CxC.DataBind()

                    For I = 0 To GV_CxC.Rows.Count - 1
                        If Not IsNothing(Coll.Item(I + 1).id_nce) Then Gv_Doctos.Rows(I).BackColor = Drawing.Color.Orange
                        GV_CxC.Rows(I).Cells(4).Text = Format(CDbl(GV_CxC.Rows(I).Cells(4).Text), Fmt.FCMSD)
                        GV_CxC.Rows(I).Cells(5).Text = Format(CDbl(GV_CxC.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    Next

                Case 2

                    Coll = CG.PagosDocumentosCxCDevuelveDetalle(HF_Id_Ing.Value, 2)

                    Gv_Doctos.DataSource = Coll
                    Gv_Doctos.DataBind()

                    For I = 0 To Gv_Doctos.Rows.Count - 1

                        If Not IsNothing(Coll.Item(I + 1).id_nce) Then Gv_Doctos.Rows(I).BackColor = Drawing.Color.Orange
                        Gv_Doctos.Rows(I).Cells(0).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(0).Text), Fmt.FCMSD) & "-" & RC.Vrut(CDbl(Gv_Doctos.Rows(I).Cells(0).Text))
                        Gv_Doctos.Rows(I).Cells(4).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(4).Text), Fmt.FCMSD)
                        Gv_Doctos.Rows(I).Cells(5).Text = Format(CDbl(Gv_Doctos.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    Next

            End Select


            MarcaTipo()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_Comision_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Comision.TextChanged

        Try

            If Txt_Comision.Text <> "" Then
                Txt_Iva.Text = CDbl(Txt_Comision.Text) * (CG.SistemaDevuelve.sis_iva / 100)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        Try
            If btn.ToolTip <> "" Then

                For i = 0 To GV_Detalle.Rows.Count - 1
                    If btn.ToolTip = CType(GV_Detalle.Rows(i).FindControl("Img_Ver"), ImageButton).ToolTip Then
                        HF_Pos_Doc_CxC.Value = i + 1
                        HF_Tipo.Value = btn.ToolTip
                        Exit For
                    End If

                Next

                LB_BuscaDocCxC_Click(sender, e)

            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
