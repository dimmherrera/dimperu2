Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_Sublineas
    Inherits System.Web.UI.Page

#Region "Declaracion de variables"

    Dim agt As New Perfiles.Cls_Principal
    Dim Caption As String = "Sub Lineas"
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim cg As New ConsultasGenerales
#End Region

#Region "Eventos"

    Protected Sub RB_Deudor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Deudor.CheckedChanged
        Try

            'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")
            Me.MultiView1.SetActiveView(View1)
            CargaDatosSubLineas(GV_SubDDR, 2) 'FY 10-07-2012
            MarcaGridDeudor(Val(sbl_num.Value))

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub RB_Producto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Producto.CheckedChanged
        Try
            'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")
            Me.MultiView1.SetActiveView(View2)
            CargaDatosSubLineas(GV_SubPro, 1)
            MarcaGridProducto(Val(sbl_num.Value))

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                Response.Expires = -1

                Me.MultiView1.SetActiveView(View1)
                Dim ClsDrop As New ConsultasGenerales

                ClsDrop.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Me.DP_Pro_Tip)
                CargaGrid()

                Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Deu_Val.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_Pro_Val.Attributes.Add("Style", "TEXT-ALIGN: right")

            End If

            'IB_Volver.Attributes.Add("onClick", "javascript:CerrarVentana('ctl00$ContentPlaceHolder1$LB_Refrescar');")
            IB_Volver.Attributes.Add("onClick", "window.close();")
            ib_ayudadeu.Attributes.Add("OnClick", "javascript:WinOpen(2,'../../Ayudas/AyudaDeudor.aspx','PopUpCliente',580,410,200,150);")


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub LB_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim ClsDeu As New ConsultasGenerales
            Dim Deu As deu_cls

            Deu = ClsDeu.DeudorDevuelvePorRut(Replace(Me.Txt_Rut_Deu.Text, ".", ""))

            If IsNothing(Deu) Then
                If Deu.id_P_0044 = 1 Then
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
                Else
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso)
                End If

            Else
                Msj.Mensaje(Me, Caption, "No existe pagador", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub GV_SubDDR_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_SubDDR.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_SubDDR, 'selectable')")
                'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_SubDDR, 'formatable')")
                'e.Row.Attributes.Add("onClick", "DetalleGV_SubDDR(GV_SubDDR, 'clicktable', 'formatable', 'selectable');")
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub GV_SubPro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_SubPro.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_SubPro, 'selectable')")
                'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_SubPro, 'formatable')")
                'e.Row.Attributes.Add("onClick", "DetalleGV_SubPro(GV_SubPro, 'clicktable', 'formatable', 'selectable');")
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub Detalle_Deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detalle_Deu.Click
        'Try


        '    Dim FMT As New FuncionesGenerales.ClsLocateInfo
        '    Dim FC As New FuncionesGenerales.FComunes
        '    Dim ClsDeu As New ConsultasGenerales
        '    Dim Sesion As New ClsSession.ClsSession
        '    Dim SBL As New sbl_cls

        '    SBL = ClsDeu.SubLineasDevuelvePorLinea(Sesion.NroLineaCredito, sbl_num.Value)

        '    If Not IsNothing(SBL) Then

        '        Me.Txt_Rut_Deu.Text = Val(SBL.deu_cls.deu_ide)
        '        Me.Txt_Dig_Deu.Text = FC.Vrut(SBL.deu_cls.deu_ide)

        '        If SBL.deu_cls.id_P_0044 = 2 Then
        '            Me.Txt_Rso_Deu.Text = SBL.deu_cls.deu_rso.Trim
        '        Else
        '            Me.Txt_Rso_Deu.Text = SBL.deu_cls.deu_rso.Trim & " " & SBL.deu_cls.deu_ape_ptn.Trim & " " & SBL.deu_cls.deu_ape_mtn.Trim
        '        End If

        '        If SBL.sbl_tip_pct_mto = "M" Then
        '            Me.RB_Deu_Mto.Checked = True
        '            Me.RB_Deu_Por.Checked = False
        '            Me.Txt_Deu_Val.Text = Format(CDbl(SBL.sbl_mto), FMT.FCMSD)
        '        Else
        '            Me.RB_Deu_Mto.Checked = False
        '            Me.RB_Deu_Por.Checked = True
        '            Me.Txt_Deu_Val.Text = Format(CDbl(SBL.sbl_pct), FMT.FCMSD)
        '        End If

        '        Txt_Rut_Deu.CssClass = "clsMandatorio"
        '        Txt_Dig_Deu.CssClass = "clsMandatorio"
        '        Txt_Deu_Val.CssClass = "clsMandatorio"
        '        RB_Deu_Mto.Enabled = True
        '        RB_Deu_Por.Enabled = True
        '        Txt_Deu_Val.ReadOnly = False

        '        MarcaGridDeudor(sbl_num.Value)

        '    End If



        '    'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")

        'Catch ex As Exception
        '    Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        'End Try
    End Sub

    Protected Sub Detalle_Pro_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try

        '    Dim FMT As New FuncionesGenerales.ClsLocateInfo
        '    Dim ClsSBL As New ConsultasGenerales
        '    Dim Sesion As New ClsSession.ClsSession
        '    Dim SBL As New sbl_cls


        '    SBL = ClsSBL.SubLineasDevuelvePorLinea(Sesion.NroLineaCredito, sbl_num.Value)


        '    DP_Pro_Tip.ClearSelection()
        '    DP_Pro_Tip.Items.FindByValue(SBL.id_P_0031).Selected = True

        '    If SBL.sbl_tip_pct_mto = "M" Then
        '        Me.RB_Pro_Mto.Checked = True
        '        Me.RB_Pro_Por.Checked = False
        '        Me.Txt_Pro_Val.Text = Format(CDbl(SBL.sbl_mto), FMT.FCMSD)
        '    Else
        '        Me.RB_Pro_Mto.Checked = False
        '        Me.RB_Pro_Por.Checked = True
        '        Me.Txt_Pro_Val.Text = Format(CDbl(SBL.sbl_pct), FMT.FCMSD)
        '    End If

        '    DP_Pro_Tip.CssClass = "clsMandatorio"
        '    Txt_Pro_Val.CssClass = "clsMandatorio"
        '    DP_Pro_Tip.Enabled = True
        '    RB_Pro_Mto.Enabled = True
        '    RB_Pro_Por.Enabled = True
        '    Txt_Pro_Val.ReadOnly = False

        '    MarcaGridProducto(sbl_num.Value)


        '    ' VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")

        'Catch ex As Exception
        '    Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        'End Try
    End Sub

    Protected Sub Btn_BuscarDeudor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_BuscarDeudor.Click

        Try


            Dim FC As New FuncionesGenerales.FComunes

            If Me.Txt_Rut_Deu.Text = "" Then

                Msj.Mensaje(Me, Caption, "Ingrese NIT del pagador", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.Txt_Dig_Deu.Text.ToUpper = FC.Vrut(CInt(Me.Txt_Rut_Deu.Text)).ToUpper Then

                Dim ClsDeu As New ConsultasGenerales
                Dim Deu As deu_cls

                Deu = ClsDeu.DeudorDevuelvePorRut(CInt(Me.Txt_Rut_Deu.Text))

                If Deu.id_P_0044 = 1 Then
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
                Else
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso)
                End If

                If IsNothing(Deu) Then

                    Msj.Mensaje(Me, Caption, "No Existe pagador", TipoDeMensaje._Exclamacion)
                Else
                    Me.Txt_Rso_Deu.CssClass = "clsDisabled"
                    Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                    Me.Txt_Dig_Deu.CssClass = "clsDisabled"

                    Me.Txt_Rso_Deu.ReadOnly = True
                    Me.Txt_Rut_Deu.ReadOnly = True
                    Me.Txt_Dig_Deu.ReadOnly = True


                End If

            Else
                Msj.Mensaje(Me, Caption, "Digito Incorrecto", TipoDeMensaje._Exclamacion)
            End If


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

#End Region

#Region "Sub "

    Private Sub MarcaGridDeudor(ByVal nro As Integer)

        Try

            For I = 0 To GV_SubDDR.Rows.Count - 1

                If (nro = GV_SubDDR.Rows(I).Cells(5).Text) Then '1 numero de columna FY 30-04-2012
                    If (I Mod 2) = 0 Then
                        GV_SubDDR.Rows(I).CssClass = "selectable"
                    Else
                        GV_SubDDR.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_SubDDR.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_SubDDR.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Private Sub MarcaGridProducto(ByVal nro As Integer)
        Try

            For I = 0 To GV_SubPro.Rows.Count - 1
                If (nro = GV_SubPro.Rows(I).Cells(1).Text) Then
                    If (I Mod 2) = 0 Then
                        GV_SubPro.Rows(I).CssClass = "selectable"
                    Else
                        GV_SubPro.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_SubPro.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_SubPro.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If
            Next


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub CargaGrid()
        Try

            'vez = 1
            CargaDatosSubLineas(Me.GV_SubPro, 1)
            'vez = 2
            CargaDatosSubLineas(Me.GV_SubDDR, 2)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Sub CargaDatosSubLineas(ByVal GV As GridView, ByVal Tipo As Int16)
        Try

            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim ClsSBL As New ConsultasGenerales
            Dim I As Integer

            'If Not IsNothing(CLI) And Not IsNothing(LDC) Then

            Select Case Tipo
                Case 1

                    ClsSBL.SubLineasDevuelvePorLinea(GV, Sesion.NroLineaCredito, TipoSubLinea.TipoDocumento)

                Case 2

                    ClsSBL.SubLineasDevuelvePorLinea(GV, Sesion.NroLineaCredito, TipoSubLinea.Deudor, 1) 'FY 10-07-2012

                    For I = 0 To GV.Rows.Count - 1
                        'GV.Rows(I).Cells(1).Text = Val(GV.Rows(I).Cells(1).Text) & "-" & FC.Vrut(Val(GV.Rows(I).Cells(1).Text))'FY 10-07-2012

                        If GV.Rows(I).Cells(3).Text <> "&nbsp;" Then
                            GV.Rows(I).Cells(3).Text = Format(CDbl(GV.Rows(I).Cells(3).Text), FMT.FCMSD)
                        End If
                        'GV.Rows(I).Cells(4).Text = Format(CDec(GV.Rows(I).Cells(4).Text), FMT.FCMCD)
                    Next

            End Select


            'End If

        Catch ex As Exception
            'vez = 2
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Private Function ValidaCampos()
        Try
            Select Case True
                Case Me.RB_Deudor.Checked
                    If Me.Txt_Rut_Deu.Text = "" Then Msj.Mensaje(Me, Caption, "Ingrese NIT pagador", TipoDeMensaje._Exclamacion, "", False) : Return False
                    If Me.Txt_Dig_Deu.Text = "" Then Msj.Mensaje(Me, Caption, "Ingrese digito pagador", TipoDeMensaje._Exclamacion, "", False) : Return False
                    If Me.Txt_Deu_Val.Text = "" Then Msj.Mensaje(Me, Caption, "Ingrese valor ", TipoDeMensaje._Exclamacion, "", False) : Return False
                    'If FC.Vrut(CLng(Txt_Rut_Deu.Text)).ToUpper <> Me.Txt_Dig_Deu.Text.ToUpper Then Msj.Mensaje(Me, Caption, "Digito incorrecto", TipoDeMensaje._Exclamacion, "", False) : Return False se cae al validar el rut Miguel Herrera
                    'Se Valida que monto seamenor a monto aprobado
                    If Me.Txt_Deu_Val.Text > MtoLineaCreditoApr Then Msj.Mensaje(Me, Caption, "El monto no puede ser superior al monto aprobado", TipoDeMensaje._Exclamacion, "", False) : Return False
                Case Me.RB_Producto.Checked
                    If Me.DP_Pro_Tip.SelectedValue = 0 Then Msj.Mensaje(Me, Caption, "Seleccione tipo de documento ", TipoDeMensaje._Exclamacion, "", False) : Return False
                    If Me.Txt_Pro_Val.Text = "" Then Msj.Mensaje(Me, Caption, "Ingrese valor ", TipoDeMensaje._Exclamacion, "", False) : Return False
                    If Me.Txt_Pro_Val.Text > MtoLineaCreditoApr Then Msj.Mensaje(Me, Caption, "El monto no puede ser superior al monto aprobado", TipoDeMensaje._Exclamacion, "", False) : Return False

                    If sbl_num.Value.Trim = "" Then

                        For i = 0 To Me.GV_SubPro.Rows.Count - 1
                            Dim lb As Label
                            lb = GV_SubPro.Rows(i).FindControl("lb_prod")
                            If Me.DP_Pro_Tip.SelectedValue = lb.Text Then
                                Msj.Mensaje(Me, "Atención", "Ya existe sublinea para este producto", ClsMensaje.TipoDeMensaje._Exclamacion)
                                Exit Function
                            End If

                        Next


                    End If


            End Select

            Return True


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Function

    Private Sub Limpiar()
        'Deudor
        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        Me.Txt_Deu_Val.Text = ""
        'Productos
        Me.DP_Pro_Tip.ClearSelection()
        Me.Txt_Pro_Val.Text = ""
        'ib_ayudadeu.Enabled = False

    End Sub

#End Region

#Region "DEUDORES"

    Private Sub DesabilitaDeudor()

        Me.Txt_Rut_Deu.ReadOnly = True
        Me.Txt_Dig_Deu.ReadOnly = True
        Me.Txt_Deu_Val.ReadOnly = True

        Me.RB_Deu_Mto.Enabled = False
        Me.RB_Deu_Por.Enabled = False

        Me.Txt_Rut_Deu.CssClass = "clsDisabled"
        Me.Txt_Dig_Deu.CssClass = "clsDisabled"
        Me.Txt_Deu_Val.CssClass = "clsDisabled"

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Deu_Val.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        ib_ayudadeu.Enabled = False

    End Sub

    Private Sub HabilitaDeudor()

        Me.Txt_Rut_Deu.ReadOnly = False
        Me.Txt_Dig_Deu.ReadOnly = False
        Me.Txt_Deu_Val.ReadOnly = False

        Me.RB_Deu_Mto.Enabled = True
        Me.RB_Deu_Por.Enabled = True

        Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
        Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
        Me.Txt_Deu_Val.CssClass = "clsMandatorio"

        Me.Txt_Rut_Deu.Text = ""
        Me.Txt_Dig_Deu.Text = ""
        Me.Txt_Deu_Val.Text = ""
        Me.Txt_Rso_Deu.Text = ""
        ib_ayudadeu.Enabled = True

    End Sub

    Protected Sub RB_Deu_Mto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Deu_Mto.CheckedChanged
        Try
            Txt_Deu_Val_MaskedEditExtender.Mask = "999,999,999,999"
            Lbl_Valor_Deu.Text = "Monto"

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub RB_Deu_Por_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Deu_Por.CheckedChanged
        Try

            Txt_Deu_Val_MaskedEditExtender.Mask = "999.99"
            Lbl_Valor_Deu.Text = "Porcentaje"
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

#End Region

#Region "PRODUCTOS"

    Private Sub DesabilitaProducto()

        Me.DP_Pro_Tip.Enabled = False
        Me.Txt_Pro_Val.ReadOnly = True

        Me.RB_Pro_Mto.Enabled = False
        Me.RB_Pro_Por.Enabled = False

        Me.DP_Pro_Tip.CssClass = "clsDisabled"
        Me.Txt_Pro_Val.CssClass = "clsDisabled"

        Me.DP_Pro_Tip.ClearSelection()
        Me.Txt_Pro_Val.Text = ""

    End Sub

    Private Sub HabilitaProducto()

        Me.DP_Pro_Tip.Enabled = True
        Me.Txt_Pro_Val.ReadOnly = False

        Me.RB_Pro_Mto.Enabled = True
        Me.RB_Pro_Por.Enabled = True

        Me.DP_Pro_Tip.CssClass = "clsMandatorio"
        Me.Txt_Pro_Val.CssClass = "clsMandatorio"

        DP_Pro_Tip.ClearSelection()
        Txt_Pro_Val.Text = ""


    End Sub

    Protected Sub RB_Pro_Mto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Pro_Mto.CheckedChanged
        Lbl_Valor_Pro.Text = "Monto"
        Txt_Pro_Val_MaskedEditExtender.Mask = "99,999,999,999"
    End Sub

    Protected Sub RB_Pro_Por_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Pro_Por.CheckedChanged
        Lbl_Valor_Pro.Text = "Porcentaje"
        Txt_Pro_Val_MaskedEditExtender.Mask = "999.99"
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Not agt.ValidaAccesso(20, 20080102, Usr, "PRESIONO GUARDAR SUBLINEA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Sesion As New ClsSession.ClsSession
            Dim FC As New FuncionesGenerales.FComunes
            Dim FMT As New FuncionesGenerales.ClsLocateInfo

            If ValidaCampos() Then

                Dim Monto As Double
                Dim Porcentaje As Decimal

                Dim SBL As New sbl_cls


                'SBL.id_sbl = Nothing
                SBL.id_ldc = Sesion.NroLineaCredito



                If sbl_num.Value.Trim = "" Then
                    SBL.sbl_mto_ocu = CDbl(0)
                Else
                    SBL.id_sbl = sbl_num.Value
                End If

                'deudor
                If Me.RB_Deudor.Checked Then

                    SBL.sbl_tip = "D"
                    SBL.deu_ide = Format(CLng(Me.Txt_Rut_Deu.Text), Var.FMT_RUT)

                    Dim ClsSBL As New ConsultasGenerales

                    If IsNothing(ClsSBL.DeudorDevuelvePorRut(SBL.deu_ide)) Then
                        Msj.Mensaje(Me, Caption, "Pagador no existe, ingrese uno que exista", TipoDeMensaje._Exclamacion, "", False)
                        Exit Sub
                    End If


                    SBL.id_P_0031 = Nothing

                    If Me.RB_Deu_Mto.Checked Then
                        SBL.sbl_tip_pct_mto = "M"
                        Monto = CDbl(FC.comasXptos(Me.Txt_Deu_Val.Text))

                        If Me.Txt_Deu_Val.Text > Sesion.MtoLineaCreditoApr Then
                            Msj.Mensaje(Me, Caption, "El monto de la sublinea no puede superar la linea de credito", TipoDeMensaje._Exclamacion, "", False)
                        End If
                        SBL.sbl_mto = Monto
                    Else

                        SBL.sbl_tip_pct_mto = "P"

                        If CDec(Me.Txt_Deu_Val.Text) <= 100 Then
                            Porcentaje = CDec(Me.Txt_Deu_Val.Text)
                            SBL.sbl_pct = Porcentaje
                        Else
                            Exit Sub
                        End If

                    End If

                    'SBL.sbl_pct = Me.Txt_Deu_Val.Text


                ElseIf Me.RB_Producto.Checked Then

                    SBL.sbl_tip = "P"
                    SBL.deu_ide = Nothing

                    SBL.id_P_0031 = Me.DP_Pro_Tip.SelectedValue

                    If Me.RB_Pro_Mto.Checked Then
                        SBL.sbl_tip_pct_mto = "M"
                        Monto = CDbl(FC.comasXptos(Me.Txt_Pro_Val.Text))
                        SBL.sbl_mto = Monto

                    Else
                        SBL.sbl_tip_pct_mto = "P"
                        Porcentaje = CDec(Me.Txt_Pro_Val.Text)
                        If Porcentaje <= 100 Then
                            SBL.sbl_pct = Porcentaje
                        Else
                            Msj.Mensaje(Me, Caption, "Porcentaje mayor al permitido 100%", TipoDeMensaje._Informacion, "", False)
                            Exit Sub
                        End If
                    End If

                End If

                Dim SubLin As New ActualizacionesGenerales

                If sbl_num.Value.Trim = "" Then
                    If Not SubLin.SubLineaDeCreditoInserta(SBL) Then
                        Msj.Mensaje(Me, Caption, "No se pudo ingresar la sub linea", TipoDeMensaje._Exclamacion, "", False)
                    Else
                        Msj.Mensaje(Me, Caption, "Sub linea ingresada", TipoDeMensaje._Exclamacion, "", False)
                    End If
                Else
                    If Not SubLin.SubLineaDeCreditoUpdate(SBL) Then
                        Msj.Mensaje(Me, Caption, "Sub linea modificada", TipoDeMensaje._Exclamacion, "", False)
                    End If
                End If
                CargaGrid()
                DesabilitaDeudor()
                DesabilitaProducto()
            End If


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")
        Limpiar()
        If RB_Deudor.Checked = True Then
            MarcaGridDeudor(0)
        Else
            MarcaGridProducto(0)
        End If

    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Try

            If Not agt.ValidaAccesso(20, 20070102, Usr, "PRESIONO NUEVA SUBLINEA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            HabilitaDeudor()
            HabilitaProducto()
            sw.Value = "Nuevo"
            sbl_num.Value = ""
            'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub Ib_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Eliminar.Click
        Try

            If Not agt.ValidaAccesso(20, 20090102, Usr, "PRESIONO ELIMINAR SUBLINEA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")
            If RB_Producto.Checked Then
                'If Msj.Mensaje(Me,Caption, ("¿Esta Seguro que quiere eliminar el Producto?", Msj.Mensaje(Me,Caption, Style.OkCancel) = Msj.Mensaje(Me,Caption, Result.Cancel Then
                'Exit Sub
                'End If
            End If

            If RB_Deudor.Checked Then
                'If Msj.Mensaje(Me,Caption, ("¿Esta Seguro que quiere eliminar al Deudor?", Msj.Mensaje(Me,Caption, Style.OkCancel) = Msj.Mensaje(Me,Caption, Result.Cancel Then
                'Exit Sub
                'End If
            End If

            If sbl_num.Value <> "" Then

                Dim SubLin As New ActualizacionesGenerales
                Dim Sesion As New ClsSession.ClsSession
                Dim SBL As New sbl_cls

                SBL.id_ldc = Sesion.NroLineaCredito
                SBL.id_sbl = sbl_num.Value

                If SubLin.SubLineaDeCreditoDelete(SBL) Then
                    CargaGrid()
                    DesabilitaDeudor()
                    DesabilitaProducto()
                    Msj.Mensaje(Me, Caption, "Registro eliminado", TipoDeMensaje._Exclamacion)
                End If

            Else
                Msj.Mensaje(Me, Caption, "Debe seleccionar un registro para eliminar", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    'Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
    '    Try

    '        If RB_Deudor.Checked = True Then
    '            If NroPaginacion_Deu >= 4 Then
    '                NroPaginacion_Deu -= 4
    '                CargaGrid()
    '            End If

    '        End If


    '    Catch ex As Exception
    '        Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
    '    End Try

    'End Sub

    'Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
    '    If GV_SubDDR.Rows.Count = 4 Then
    '        NroPaginacion_Deu += 4
    '        CargaGrid()
    '    End If

    'End Sub
#End Region

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try

            Dim FC As New FuncionesGenerales.FComunes

            If Me.Txt_Dig_Deu.Text.ToUpper = FC.Vrut(CInt(Me.Txt_Rut_Deu.Text)).ToUpper Then

                Dim ClsDeu As New ConsultasGenerales
                Dim Deu As deu_cls

                Deu = ClsDeu.DeudorDevuelvePorRut(Replace(Me.Txt_Rut_Deu.Text, ".", ""))

                If IsNothing(Deu) Then

                    Msj.Mensaje(Me, Caption, "No existe Pagador", TipoDeMensaje._Exclamacion)
                    Exit Sub
                Else
                    Me.Txt_Rso_Deu.CssClass = "clsDisabled"
                    Me.Txt_Rut_Deu.CssClass = "clsDisabled"
                    Me.Txt_Dig_Deu.CssClass = "clsDisabled"

                    Me.Txt_Rso_Deu.ReadOnly = True
                    Me.Txt_Rut_Deu.ReadOnly = True
                    Me.Txt_Dig_Deu.ReadOnly = True
                    ib_ayudadeu.Enabled = False

                End If


                If Deu.id_P_0044 = 1 Then
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
                Else
                    Me.Txt_Rso_Deu.Text = Trim(Deu.deu_rso)
                End If


            Else
                Msj.Mensaje(Me, Caption, "Dígito incorrecto", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim ClsSBL As New ConsultasGenerales
            Dim Sesion As New ClsSession.ClsSession
            Dim SBL As New sbl_cls

            If btn.ToolTip <> "" Then
                sbl_num.Value = btn.ToolTip
                'Detalle_Pro_Click(sender, e)
                SBL = ClsSBL.SubLineasDevuelvePorLinea(Sesion.NroLineaCredito, sbl_num.Value)


                DP_Pro_Tip.ClearSelection()
                DP_Pro_Tip.Items.FindByValue(SBL.id_P_0031).Selected = True

                If SBL.sbl_tip_pct_mto = "M" Then
                    Me.RB_Pro_Mto.Checked = True
                    Me.RB_Pro_Por.Checked = False
                    Me.Txt_Pro_Val.Text = Format(CDbl(SBL.sbl_mto), FMT.FCMSD)
                Else
                    Me.RB_Pro_Mto.Checked = False
                    Me.RB_Pro_Por.Checked = True
                    Me.Txt_Pro_Val.Text = Format(CDbl(SBL.sbl_pct), FMT.FCMSD)
                End If

                DP_Pro_Tip.CssClass = "clsMandatorio"
                Txt_Pro_Val.CssClass = "clsMandatorio"
                DP_Pro_Tip.Enabled = True
                RB_Pro_Mto.Enabled = True
                RB_Pro_Por.Enabled = True
                Txt_Pro_Val.ReadOnly = False

                MarcaGridProducto(sbl_num.Value)

                ' VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender1")
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim btn As ImageButton = CType(sender, ImageButton)
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim FC As New FuncionesGenerales.FComunes
            Dim ClsDeu As New ConsultasGenerales
            Dim Sesion As New ClsSession.ClsSession
            Dim SBL As New sbl_cls
            If btn.ToolTip <> "" Then
                sbl_num.Value = btn.ToolTip
                'Detalle_Deu_Click(sender, e)
                SBL = ClsDeu.SubLineasDevuelvePorLinea(Sesion.NroLineaCredito, sbl_num.Value)

                If Not IsNothing(SBL) Then

                    Me.Txt_Rut_Deu.Text = Val(SBL.deu_cls.deu_ide)
                    Me.Txt_Dig_Deu.Text = FC.Vrut(SBL.deu_cls.deu_ide)

                    If SBL.deu_cls.id_P_0044 <> 1 Then
                        Me.Txt_Rso_Deu.Text = SBL.deu_cls.deu_rso.Trim
                    Else
                        Me.Txt_Rso_Deu.Text = SBL.deu_cls.deu_rso.Trim & " " & SBL.deu_cls.deu_ape_ptn.Trim & " " & SBL.deu_cls.deu_ape_mtn.Trim
                    End If

                    If SBL.sbl_tip_pct_mto = "M" Then
                        Me.RB_Deu_Mto.Checked = True
                        Me.RB_Deu_Por.Checked = False
                        Me.Txt_Deu_Val_MaskedEditExtender.Mask = "999,999,999,999"
                        Me.Txt_Deu_Val.Text = Format(CDbl(SBL.sbl_mto), FMT.FCMSD)
                    Else
                        Me.RB_Deu_Mto.Checked = False
                        Me.RB_Deu_Por.Checked = True
                        Me.Txt_Deu_Val_MaskedEditExtender.Mask = "999.99"
                        Me.Txt_Deu_Val.Text = Format(CDbl(SBL.sbl_pct), FMT.FSMCD)
                    End If

                    'Txt_Rut_Deu.CssClass = "clsMandatorio"
                    'Txt_Dig_Deu.CssClass = "clsMandatorio"
                    Txt_Deu_Val.CssClass = "clsMandatorio"
                    RB_Deu_Mto.Enabled = True
                    RB_Deu_Por.Enabled = True
                    Txt_Deu_Val.ReadOnly = False

                    MarcaGridDeudor(sbl_num.Value)

                End If

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub
End Class
