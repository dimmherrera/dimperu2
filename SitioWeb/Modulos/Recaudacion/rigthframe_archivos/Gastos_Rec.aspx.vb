Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_Gastos_Rec
    Inherits System.Web.UI.Page
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim clasecli As New ClaseClientes
    Dim fc As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim REC As New ClaseRecaudación

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            Coll_Cobranza = New Collection
            coll_DNC = New Collection
            cg.ParametrosDevuelve(51, True, Me.dr_tip_gas)
            cg.EjecutivosDevuelve(Dr_Rec, CodEje, 14)
            Me.Txt_Fec_Rec.Text = Date.Now.ToShortDateString
            habilitacampos("I")

            Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
            Me.Txt_mto_gto.Attributes.Add("Style", "TEXT-ALIGN: right")


        End If
        IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/Ayudadeu.aspx','PopUpCliente',580,410,200,150);")
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If

    End Sub

    Protected Sub btn_ok_gr_pgo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ok_gr_pgo.Click

        Dim obj As New obj_gastos_rec
        Dim x As System.EventArgs



        With obj

            .id_hre = nro_hoja.Value
            .gga_fec = Me.Txt_Fec_Rec.Text
            .deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), fmt.FMT_RUT)
            .gga_mto = Me.Txt_mto_gto.Text
            .id_p_0051 = Me.dr_tip_gas.SelectedValue
            .gga_rec_ext = "N"
            .tipo_gasto = Me.dr_tip_gas.SelectedItem.Text
            .gga_vld = "N"
            .deudor = Me.Txt_Rso_Deu.Text
        End With
        coll_DNC.Add(obj)



        Me.Gr_gastos.DataSource = coll_DNC
        Me.Gr_gastos.DataBind()

        For i = 0 To Gr_gastos.Rows.Count - 1

            Gr_gastos.Rows(i).Cells(1).Text = Format(CLng(Gr_gastos.Rows(i).Cells(1).Text), "###,###,###") & "-" & fc.Vrut(Gr_gastos.Rows(i).Cells(1).Text)
            Gr_gastos.Rows(i).Cells(4).Text = Format(CDbl(Gr_gastos.Rows(i).Cells(4).Text), "###,###,###")
        Next

        'marcagrilla_Click(Me, x)

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        Me.Txt_Rut_Deu.ReadOnly = False
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
        Me.Txt_Dig_Deu.ReadOnly = False
        Me.Txt_Rso_Deu.Text = ""
        Me.dr_tip_gas.SelectedValue = 0
        Me.Txt_mto_gto.Text = ""
        '  habilitacampos("H")

    End Sub

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged

        If Me.Txt_Dig_Deu.Text <> "" Then
            Me.lb_buscar_Click(Me, e)
        Else
            Msj.Mensaje(Me.Page, "Atención", "Ingrese Digito Verificador", 3)
        End If
    End Sub

    Protected Sub lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_buscar.Click


        Dim deu As deu_cls

        If Me.Txt_Dig_Deu.Text <> fc.Vrut(Me.Txt_Rut_Deu.Text) Then

            Msj.Mensaje(Me.Page, "Atención", "Digito Verificador incorrecto", 2)
            Exit Sub
        End If

        deu = cg.DeudorDevuelvePorRut(Me.Txt_Rut_Deu.Text)

        Session("Deudor") = deu

        If Not IsNothing(deu) Then
            'Datos Deudor
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.ReadOnly = True
            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.Text = Trim(deu.deu_rso) & " " & Trim(deu.deu_ape_ptn) & " " & Trim(deu.deu_ape_mtn)
            Me.Txt_Rso_Deu.ReadOnly = True

        End If

    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Buscar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20010408, Usr, "PRESIONO BOTON BUSCAR GASTOS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim sucursales As Integer

        Dim cod_suc As String

        Try

            If Me.Ch_Suc.Checked = True Then
                sucursales = 1
                cod_suc = ""
            Else
                sucursales = 0
                cod_suc = Sucursal
            End If

            Dim val As Integer
            coll_apc = New Collection
            Coll_Cobranza = New Collection



            If Me.rb_hora.SelectedValue = "A" Then

                val = REC.HOJA_REC_VALIDA(Me.Dr_Rec.SelectedValue, CDate(Me.Txt_Fec_Rec.Text), Me.rb_hora.SelectedValue)

            Else

                val = REC.HOJA_REC_VALIDA(Me.Dr_Rec.SelectedValue, CDate(Me.Txt_Fec_Rec.Text), Me.rb_hora.SelectedValue)


            End If

            nro_hoja.Value = val

            If val <> 0 Then

                habilitacampos("H")
                Dim col As New Collection

                col = REC.gastos_rec_retorna(nro_hoja.Value)

                If Not IsNothing(col) Then
                    coll_DNC = New Collection
                    For i = 1 To col.Count

                        Dim obj As New obj_gastos_rec

                        With obj

                            .gga_mto = col.Item(i).gga_mto
                            .gga_rec_ext = col.Item(i).gga_rec_ext
                            .gga_vld = col.Item(i).gga_vld
                            .id_gga = col.Item(i).id_gga

                            .tipo_gasto = col.Item(i).tipo_gasto
                            .id_hre = col.Item(i).id_hre

                            .deudor = col.Item(i).deudor
                            .deu_ide = col.Item(i).deu_ide
                        End With
                        coll_DNC.Add(obj)

                    Next

                End If


                Gr_gastos.DataSource = coll_DNC
                Gr_gastos.DataBind()

                For i = 0 To Gr_gastos.Rows.Count - 1

                    Gr_gastos.Rows(i).Cells(1).Text = Format(CLng(Gr_gastos.Rows(i).Cells(1).Text), "###,###,###") & "-" & fc.Vrut(Gr_gastos.Rows(i).Cells(1).Text)
                    Gr_gastos.Rows(i).Cells(4).Text = Format(CDbl(Gr_gastos.Rows(i).Cells(4).Text), "###,###,###")
                Next

                'marcagrilla_Click(Me, e)
                Me.Btn_Buscar.Enabled = False
            Else

                msj.Mensaje(Me.Page, "Atención", "No existen datos para Recaudador", 2)

            End If





        Catch ex As Exception

        End Try

    End Sub

    Public Sub habilitacampos(ByVal est As String)


        If est = "I" Then

            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.ReadOnly = True

            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.ReadOnly = True

            Me.Txt_Rso_Deu.CssClass = "clsDisabled"
            Me.Txt_Rso_Deu.Text = ""
            Me.Txt_Rso_Deu.ReadOnly = True

            Me.Txt_mto_gto.CssClass = "clsDisabled"
            Me.Txt_mto_gto.Text = ""
            Me.Txt_mto_gto.ReadOnly = True

            Me.dr_tip_gas.CssClass = "clsDisabled"
            Me.dr_tip_gas.Enabled = False

        Else

            Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
            Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.ReadOnly = False

            Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.ReadOnly = False

            Me.dr_tip_gas.CssClass = "clsMandatorio"
            Me.dr_tip_gas.Enabled = True

            Me.Txt_mto_gto.CssClass = "clsMandatorio"
            Me.Txt_mto_gto.Text = ""
            Me.Txt_mto_gto.ReadOnly = False
            Me.Txt_Rut_Deu.Focus()
        End If

    End Sub

    Protected Sub Btn_gua_rec_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_gua_rec.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20020408, Usr, "PRESIONO BOTON GUARDAR GASTOS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try

            If Me.Gr_gastos.Rows.Count > 0 Then

                If coll_DNC.Count > 0 Then

                    REC.gastos_rec_guarda(coll_DNC, coll_DNC.Item(1).id_hre, coll_DNC.Item(1).deu_ide)

                End If


                Dim col As New Collection

                col = REC.gastos_rec_retorna(nro_hoja.Value)

                If Not IsNothing(col) Then
                    coll_DNC = New Collection
                    For i = 1 To col.Count

                        Dim obj As New obj_gastos_rec

                        With obj

                            .gga_mto = col.Item(i).gga_mto
                            .gga_rec_ext = col.Item(i).gga_rec_ext
                            .gga_vld = col.Item(i).gga_vld
                            .id_gga = col.Item(i).id_gga

                            .tipo_gasto = col.Item(i).tipo_gasto
                            .id_hre = col.Item(i).id_hre

                            .deudor = col.Item(i).deudor
                            .deu_ide = col.Item(i).deu_ide
                        End With
                        coll_DNC.Add(obj)

                    Next

                End If
                msj.Mensaje(Me.Page, "Atención", "Se han guardado los gastos correctamente", 2)
            Else

                'Msje
                msj.Mensaje(Me.Page, "Atención", "No hay gastos para guardar", 2)

            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try



    End Sub

    Protected Sub Btn_apb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_apb.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20030408, Usr, "PRESIONO BOTON APROBAR GASTOS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If HF_Pos_doc.Value = "" Then
            msj.Mensaje(Me.Page, "Atención", "Debe seleccionar un gasto para aprobar", 2)
            Exit Sub
        End If

        If Not IsNothing(coll_DNC) Then
            If coll_DNC.Item(Val(HF_Pos_doc.Value)).id_gga = Nothing Then
                msj.Mensaje(Me.Page, "Atención", "Debes Guardar antes de dar visto Bueno", 2)
            Else
                REC.gastos_valida(coll_DNC.Item(Val(HF_Pos_doc.Value)).id_gga)
                msj.Mensaje(Me.Page, "Atención", "Gastos Aprobados", ClsMensaje.TipoDeMensaje._Informacion)
            End If

        End If


    End Sub

    Protected Sub Gr_gastos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_gastos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ver As ImageButton = e.Row.FindControl("Btn_ver")
            ver.ToolTip = e.Row.RowIndex
        End If

    End Sub

    Protected Sub marcagrilla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles marcagrilla.Click

        For i = 0 To Me.Gr_gastos.Rows.Count - 1

            Dim rut As String = CStr(Me.Gr_gastos.Rows(i).Cells(0).Text).Substring(0, Trim(Me.Gr_gastos.Rows(i).Cells(0).Text).Length - 2)

            Me.Gr_gastos.Rows(i).Cells(0).Text = Format(CLng(Rut), "###,###,###") & "-" & fc.Vrut(rut)

            Me.Gr_gastos.Rows(i).Cells(3).Text = Format(CDbl(Me.Gr_gastos.Rows(i).Cells(3).Text), "###,###,###")

            If HF_Pos_doc.Value <> "" Then

                If i = Val(HF_Pos_doc.Value - 1) Then


                    Me.Gr_gastos.Rows(i).CssClass = "clicktable"
                    Me.Gr_gastos.Rows(i).ForeColor = Drawing.Color.Gray

                Else
                    Me.Gr_gastos.Rows(i).CssClass = "formatable"

                End If

            Else
                If coll_DNC.Item(i + 1).gga_vld = "S" Then
                    Me.Gr_gastos.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                Else
                    Me.Gr_gastos.Rows(i).CssClass = "formatable"
                End If


            End If

        Next

    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Limpiar.Click

        habilitacampos("I")

        Me.Gr_gastos.Controls.Clear()

        Me.Gr_gastos.DataSource = Nothing
        Me.Gr_gastos.DataBind()

        coll_nce = New Collection
        Me.Btn_Buscar.Enabled = True
        Me.Dr_Rec.SelectedValue = 0
        Me.dr_tip_gas.SelectedValue = 0
        Me.HF_Pos_doc.Value = ""
    End Sub

    Protected Sub btn_canc_gr_pgo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_canc_gr_pgo.Click

        Dim i As Integer

        If HF_Pos_doc.Value <> "" Then

            i = Val(HF_Pos_doc.Value)

            coll_DNC.Remove(i)

            Me.Gr_gastos.DataSource = coll_DNC
            Me.Gr_gastos.DataBind()

            'marcagrilla_Click(Me, e)

        Else
            msj.Mensaje(Me.Page, "Atención", "Debe seleccionar un gasto para poder elmiminar", 2)

        End If

        HF_Pos_doc.Value = ""
    End Sub

    Protected Sub Btn_imp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_imp.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20040408, Usr, "PRESIONO BOTON IMPRIMIR GASTOS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        RW.AbrePopup(Me, 1, "Report_gastos.aspx?nro_hoja=" & nro_hoja.Value & " ", "ReportePagos", 1100, 750, 0, 0)
    End Sub

    Protected Sub Btn_imp_control_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_imp_control.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20050408, Usr, "PRESIONO BOTON IMPRIMIR CONTROL DE GASTOS DE RECAUDACION") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        RW.AbrePopup(Me, 1, "reporte_control_recaudador.aspx", "ReportePagos", 1100, 750, 0, 0)
    End Sub

    Protected Sub Txt_Fec_Rec_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Rec.TextChanged
        If Not IsDate(Me.Txt_Fec_Rec.Text) Then
            msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Informacion)
            Me.Txt_Fec_Rec.Text = ""
            Exit Sub
        End If
    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = sender

        HF_Pos_doc.Value = btn.ToolTip + 1

        For i = 0 To Gr_gastos.Rows.Count - 1

            If (btn.ToolTip = CType(Gr_gastos.Rows(i).FindControl("Btn_ver"), ImageButton).ToolTip) Then

                If (i Mod 2) = 0 Then
                    Gr_gastos.Rows(i).CssClass = "selectable"
                Else
                    Gr_gastos.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_gastos.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_gastos.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If
        Next
    End Sub
End Class
