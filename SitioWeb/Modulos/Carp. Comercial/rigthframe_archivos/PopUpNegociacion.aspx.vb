Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb
Imports Microsoft.Reporting.WebForms
Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.FComunes
Imports System.IO
Imports System.Data

Partial Class ClsPopUpNegociacion
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"

    Private CG As New ConsultasGenerales
    Private AG As New ActualizacionesGenerales
    Dim CMC As New ClaseComercial
    Private RG As New FuncionesGenerales.FComunes
    Private RW As New FuncionesGenerales.RutinasWeb
    Private FMT As New FuncionesGenerales.ClsLocateInfo
    Private VAR As New FuncionesGenerales.Variables
    Private GastosNeg As Collection
    Private gastofijo As Collection
    Private Caption As String = "Negociación"
    Private Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim OP As New ClaseOperaciones
    Dim CA As New ClaseArchivos
    Dim FC As New FuncionesGenerales.FComunes
    Dim CL As New ClaseClientes
    Private documentos As Collection
    Private condiciones As Collection

#End Region

#Region "Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1

            If Not IsPostBack Then

                'Response.Cache.SetNoStore()
                Session("GastosFijos") = New Collection
                Session("Gastos") = New Collection
                Session("Documentos") = New Collection
                Session("Condiciones") = New Collection

                HF_Id_Opn.Value = 0
                IB_Guardar.Enabled = False
                IB_Ejecutar.Enabled = False
                Txt_MtoEva.Text = 0
                Txt_PorAnt.Text = 0
                Txt_CantDeu.Text = 0
                Txt_Rut_Cli.Focus()

                CargaDropDownList()
                AlineaTextBoxDerecha()
                CargaGastos()

                If Not IsNothing(Session("Cliente")) Then

                    If CargaDatosCliente() Then

                        LimpiaHojaDeNegocio(True)
                        CargaDatosNuevaOperacion()

                        '********************************************************************
                        ' CARGA DATOS DE COMISION ASOCIADAS AL CLIENTE
                        '********************************************************************

                        Dim coll As New Collection

                        coll = CG.BuscaComisionCli(CLng(Me.Txt_Rut_Cli.Text))

                        If Not IsNothing(coll) Then

                            If coll.Count > 0 Then
                                DP_MonedaCom.SelectedIndex = coll.Item(1).id_P_0023_com
                                Me.Txt_Minimo.Text = CDbl(coll.Item(1).cli_com_min)
                                Txt_Maximo.Text = CDbl(coll.Item(1).cli_com_max)
                                Me.DP_MonComFlat.SelectedValue = coll.Item(1).id_P_0023_fla
                                Me.Txt_PorComFlat.Text = CDbl(coll.Item(1).cli_com_fla)
                                Me.Txt_PorCom.Text = coll.Item(1).cli_por_com
                            End If

                        End If

                        '********************************************************************
                        'FIN DATOS DE COMISION ASOCIADAS AL CLIENTE
                        '********************************************************************

                        If Request.QueryString("nro") <> "" Then

                            HF_Id_Opn.Value = Request.QueryString("nro").Trim
                            CargaEvaluaciones(True)
                            CargaNegociacionAnterior(HF_Id_Opn.Value)

                            IB_Guardar.Enabled = True
                            IB_Ejecutar.Enabled = True

                            If DP_EstadoNeg.SelectedValue >= 3 Then 'Si estado es 3(Simulado)Deshabilita controles

                                HabilitaDeshabilitaDatosDocumentos(False)
                                HabilitaDesabilitaCliente(False)
                                HabilitaDeshabilitaDatosOperacion(False)
                                HabilitaDeshabilitaObservaciones(False)
                                HabilitaDeshabilitaPagare(False)
                                HabilitaDeshabilitaPago(False)
                                HabilitaDeshabilitaParametrosOperacion(False)
                                HabilitaDeshabilitaTasas(False)
                                DP_Evaluaciones.Enabled = False
                                DP_Evaluaciones.CssClass = "clsDisabled"

                                LB_Gastos.Enabled = False
                                Txt_FecVto_CalendarExtender.Enabled = False

                                IB_Ejecutar.Enabled = False
                                IB_EnviarOpe.Enabled = False
                                IB_Guardar.Enabled = False

                            End If

                            If Request.QueryString("Pizarra") <> "" Then

                                IB_Guardar.Visible = False
                                IB_Ejecutar.Visible = False
                                IB_EnviarOpe.Visible = False

                                HabilitaDeshabilitaDatosDocumentos(False)
                                HabilitaDesabilitaCliente(False)
                                HabilitaDeshabilitaDatosOperacion(False)
                                HabilitaDeshabilitaObservaciones(False)
                                HabilitaDeshabilitaPagare(False)
                                HabilitaDeshabilitaPago(False)
                                HabilitaDeshabilitaParametrosOperacion(False)
                                HabilitaDeshabilitaTasas(False)

                                DP_Evaluaciones.Enabled = False
                                Txt_FechaNegociacion.ReadOnly = True
                                Txt_MtoDescuentos.ReadOnly = True
                                LB_Gastos.Enabled = False

                                DP_Evaluaciones.CssClass = "clsDisabled"
                                Txt_FechaNegociacion.CssClass = "clsDisabled"
                                Txt_MtoDescuentos.CssClass = "clsDisabled"

                            End If

                        Else

                            IB_InfInstructivo.Enabled = False 'FY 06-07-2012
                            IB_Informe.Enabled = False
                            CargaGastosPorCliente()

                            If coll.Count = 0 Then

                                coll_pnu = CG.Parametros_Detalle_Devuelve(31, Me.DP_TipoDocto.SelectedValue)

                                Me.DP_MonedaCom.ClearSelection()

                                If Not IsNothing(coll_pnu) Then

                                    BuscaCombo(Me.DP_MonedaCom, CStr(coll_pnu(1).pnu_atr_007))

                                    Me.Txt_PorCom.Text = coll_pnu(1).pnu_atr_008
                                    Me.Txt_Maximo.Text = coll_pnu(1).pnu_atr_010
                                    Me.Txt_Minimo.Text = coll_pnu(1).pnu_atr_009

                                End If

                                CargaEvaluaciones(False)

                            Else

                                CargaEvaluaciones(False)

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_BuscaDatosDiarios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaDatosDiarios.Click
        Try
            CargaDatosDiarios()
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_CloseInt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CloseInt.Click

    End Sub

    Protected Sub btn_guar_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guar.Click

        Try

            If Me.txt_mto.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar el monto del gasto", ClsMensaje.TipoDeMensaje._Exclamacion)
                LB_Gastos_ModalPopupExtender.Show()
                Exit Sub
            End If


            If Me.txt_des.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar la descripción del gasto", ClsMensaje.TipoDeMensaje._Exclamacion)
                LB_Gastos_ModalPopupExtender.Show()
                Exit Sub
            End If

            Dim Gas As New ClsGastos

            Gas.Código = 0
            Gas.Monto = Format(CDbl(txt_mto.Text), FMT.FCMSD)
            Gas.Descripción = UCase(Me.txt_des.Text)
            Gas.Tipo = "F"
            Gas.AfectoExento = "N"

            gastofijo = New Collection

            ' Me.txt_tot_gto_fij.Text = CDbl(Val(Me.txt_tot_gto_fij.Text)) + RG.comasXptos(Me.txt_mto.Text)

            If Not IsNothing(Session("GastosFijos")) Then
                gastofijo = Session("GastosFijos")
            End If

            gastofijo.Add(Gas)

            Session("GastosFijos") = gastofijo

            Me.gr_gastofijo.DataSource = Session("GastosFijos")
            Me.gr_gastofijo.DataBind()

            Me.txt_des.Text = ""
            Me.txt_mto.Text = ""

            For i = 0 To gr_gastofijo.Rows.Count - 1
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)
                gr_gastofijo.Rows(i).Cells(5).Text = "SI"
            Next

            LB_Gastos_ModalPopupExtender.Show()
            txt_tot_gto_fij.Text = ""

        Catch ex As Exception
            LB_Gastos_ModalPopupExtender.Show()
        End Try


    End Sub

    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click

        Try

            Dim var As Integer = 1
            Dim col As New Collection

            For i = 0 To Me.gr_gastofijo.Rows.Count - 1
                Dim ch As CheckBox
                ch = Me.gr_gastofijo.Rows(i).FindControl("ch_gfijos")

                If ch.Checked Then

                    col = Session("GastosFijos")
                    ' Me.txt_tot_gto_fij.Text = CDbl(Val(Me.txt_tot_gto_fij.Text)) - gr_gastofijo.Rows(i).Cells(3).Text
                    col.Remove(i + var)
                    var = var - 1

                End If

            Next
            Session("GastosFijos") = col

            Me.gr_gastofijo.DataSource = Session("GastosFijos")
            Me.gr_gastofijo.DataBind()
            Me.txt_tot_gto_fij.Text = 0
            For i = 0 To gr_gastofijo.Rows.Count - 1
                Dim ch As CheckBox
                ch = Me.gr_gastofijo.Rows(i).FindControl("ch_gfijos")
                If ch.Checked Then
                    Me.txt_tot_gto_fij.Text = CDbl(Val(Me.txt_tot_gto_fij.Text)) + gr_gastofijo.Rows(i).Cells(3).Text
                End If
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)
            Next
            Me.txt_des.Text = ""
            Me.txt_mto.Text = ""

            LB_Gastos_ModalPopupExtender.Show()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Btn_AceptarGastos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_AceptarGastos.Click

        Try

            AgregaGastos()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_BuscaTasas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_BuscaTasas.Click
        Try

            Dim dias_per As Int16
            Dim FechaVencimiento As String

            If Txt_FecVto.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar fecha de vcto. de los documentos", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            FechaVencimiento = CMC.DiaHabilDevuelve(Txt_FecVto.Text)

            Txt_FecVctoNeg.Text = FechaVencimiento
            Txt_FecVctoReal.Text = FechaVencimiento


            'Lista Detalle Documentos
            dias_per = DateDiff("d", CDate(Txt_FechaNegociacion.Text), CDate(Txt_FecVctoNeg.Text))

            If dias_per < 0 Then
                Msj.Mensaje(Me, Caption, "Documento Ya está Vencido a la fecha de Simulación", TipoDeMensaje._Informacion)
            End If

            'Dim TYP As typ_cls

            'TYP = CMC.TasaPlazosDevuelve(DP_Moneda.SelectedValue, dias_per)

            'If IsNothing(TYP) Then
            '    Msj.Mensaje(Me, Caption, "No Existe Tasa Base para " & dias_per & " días de permanencia en mora para Doctos.", TipoDeMensaje._Informacion)
            'Else
            '    Txt_TasaBase.Text = TYP.typ_mto
            'End If


            'Porcentaje Mto docto sobre el monto total Negociación
            'porc_mto = (MTO_FIN_NEG * 100) / MTO_NEG

            'Tasa promedio
            'TAS_BAS_NEG = (tasa_periodo * porc_mto) / 100

            'Screen.MousePointer = 0

            'Tasa Base
            'Call SPR_NEG.SetText(7, 21, Format(TAS_BAS_NEG, "###,###0.00"))
            'Puntos
            'Call SPR_NEG.SetText(7, 23, Format(PUN_TOS_NEG, "###,###0.00"))

            'Tasa Negocio
            'Txt_TasaNegocio.Text = CDec(Txt_TasaBase.Text) + CDec(Txt_TasaSpread.Text) + CDec(Txt_Puntos.Text)
            Txt_TasaBase.Text = CG.Devolver_DTF(Txt_FechaNegociacion.Text)

            'Tasa Negocio
            'Txt_TasaNegocio.Text = CDec(Txt_TasaBase.Text) + CDec(Txt_TasaSpread.Text) + CDec(Txt_Puntos.Text)
            Txt_TasaNegocio.Text = CG.Devolver_EA(CDec(Txt_TasaBase.Text), CDec(Txt_TasaSpread.Text)) + CDec(Txt_Puntos.Text)

            'TAS_NEG = CDec(TAS_BAS_NEG) + CDec(SPR_EAD_NEG) + CDec(PUN_TOS_NEG)
            'Call SPR_NEG.SetText(7, 24, Format(TAS_NEG, "###,###0.00"))



        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_Evaluaciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Evaluaciones.SelectedIndexChanged

        Try

            If DP_Evaluaciones.SelectedIndex > 0 Then

                Dim col As New Collection
                Dim formato As String
                formato = RG.DevuelveFormatoMoneda(Me.DP_Moneda.SelectedValue)


                col = CMC.DiasDeRetencionDevuelve(Sucursal, Txt_PlazaDocto.Text, DP_TipoDocto.SelectedValue)

                If col.Count > 0 Then
                    Txt_DiaRet.Text = col.Item(1)
                Else
                    Txt_DiaRet.Text = 0
                End If

                Txt_CanDocto.Text = ""
                Txt_FecVto.Text = ""
                Txt_FecVctoReal.Text = ""

                IB_Guardar.Enabled = True
                IB_Ejecutar.Enabled = True

                Txt_PorAnt.Text = Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).Porcentaje
                Txt_CantDeu.Text = Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).Deudores

                DP_Moneda.ClearSelection()
                DP_Moneda.SelectedValue = Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).id_Moneda
                Txt_MtoDoctos.Text = Format(Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).monto_doc, formato)
                Txt_MtoEva.Text = Format(CDbl(Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).Monto), formato)

                If Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).id_P_0110 = 5 Then

                    'Cargar datos de negocio anterior
                    Dim id_neg As Integer

                    Try

                        id_neg = CMC.NegociacionDevuelvexEva(DP_Evaluaciones.SelectedValue).id_opn

                    Catch ex As Exception
                        Msj.Mensaje(Me.Page, Caption, "No se encontro la negociación asociada al evaluación.", TipoDeMensaje._Error)
                        Exit Sub
                    End Try

                    HF_Id_Opn.Value = id_neg
                    CargaNegociacionAnterior(id_neg)

                End If

                CargaDocumentos()
                CargaCondiciones()


            Else

                IB_Guardar.Enabled = False
                IB_Ejecutar.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Ch_gfijos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try



            Me.txt_tot_gto_fij.Text = 0
            For i = 0 To Me.gr_gastofijo.Rows.Count - 1
                Dim ch As CheckBox
                ch = Me.gr_gastofijo.Rows(i).FindControl("ch_gfijos")

                If ch.Checked Then
                    If Me.txt_tot_gto_fij.Text = "" Then
                        Me.txt_tot_gto_fij.Text = 0
                    End If
                    Me.txt_tot_gto_fij.Text = CDbl(Me.txt_tot_gto_fij.Text) + CDbl(Me.gr_gastofijo.Rows(i).Cells(3).Text)


                End If

            Next

            If txt_tot_gto_fij.Text <> "" Then
                txt_tot_gto_fij.Text = Format(CDbl(txt_tot_gto_fij.Text), FMT.FCMSD)
            End If
            LB_Gastos_ModalPopupExtender.Show()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_TipoOperacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoOperacion.SelectedIndexChanged
        TipoOperacion()
    End Sub

    Private Sub TipoOperacion()
        Try

            If Me.DP_TipoOperacion.SelectedValue = 1 Then
                CG.ParametrosDevuelve(TablaParametro.TipoEgreso, True, DP_FormaPago)

                Me.DP_FormaPago.Enabled = True
                Me.DP_FormaPago.CssClass = "clsMandatorio"

                Me.CB_Antes14.CssClass = "clsTxt"
                Me.CB_Antes14.Enabled = True


            ElseIf Me.DP_TipoOperacion.SelectedValue = 3 Then

                CG.ParametrosDevuelve(TablaParametro.TipoEgreso, True, DP_FormaPago)
                Me.DP_FormaPago.Enabled = True
                Me.DP_FormaPago.CssClass = "clsMandatorio"

                Me.CB_Antes14.CssClass = "clsTxt"
                Me.CB_Antes14.Enabled = True


            ElseIf Me.DP_TipoOperacion.SelectedValue = 2 Then

                'Me.DP_FormaPago.Items.Item(5).Enabled = False
                Me.DP_FormaPago.Enabled = False
                'Me.DP_FormaPago.CssClass = "clsDisabled"
                Me.DP_FormaPago.SelectedValue = 0
                Me.Txt_NroCuenta.Text = ""

                Me.DP_BancoCuenta.SelectedValue = 0

                Me.CB_Antes14.Checked = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub
    Protected Sub ch_sel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Me.txt_tot_gto_def.Text = 0
            For i = 0 To Me.gd_gastdef.Rows.Count - 1
                Dim ch As CheckBox
                ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

                If ch.Checked Then

                    Me.txt_tot_gto_def.Text = CDbl(Me.txt_tot_gto_def.Text) + CDbl(Me.gd_gastdef.Rows(i).Cells(3).Text)
                    Me.txt_tot_gto_def.Text = Format(CDbl(Me.txt_tot_gto_def.Text), FMT.FCMSD)

                End If

            Next

            LB_Gastos_ModalPopupExtender.Show()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_PorComFlat_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_PorComFlat.TextChanged
        Try
            If DP_MonComFlat.SelectedValue = 0 Then
                Msj.Mensaje(Page, Caption, "Seleccione moneda de comisión", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_PorComFlat.Text = ""
            End If
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub DP_BancoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_BancoCuenta.SelectedIndexChanged
        Try
            'rescata la cta. cte del cliente
            Dim bco As nbc_cls
            Dim ClsCli As New ClaseClientes
            If Me.DP_BancoCuenta.SelectedValue <> 0 Then
                If DP_FormaPago.SelectedValue <> 3 Or DP_FormaPago.SelectedValue <> 4 Then
                    bco = ClsCli.BancosDevuelvePorClienteYBanco(Txt_Rut_Cli.Text.Replace(".", ""), DP_BancoCuenta.SelectedValue)
                    Txt_NroCuenta.Text = bco.nbc_cct
                Else
                    Txt_NroCuenta.Text = ""
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_FormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_FormaPago.SelectedIndexChanged
        FormaPago()
    End Sub

    Private Sub FormaPago()

        If DP_FormaPago.SelectedIndex > 0 Then

            Dim ClsCli As New ClaseClientes
            Dim Tipo As P_0056_cls


            Tipo = CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, DP_FormaPago.SelectedValue).Item(1)

            If Tipo.pnu_atr_007 = "S" Then
                RB_GMF.Items(0).Selected = True
                RB_GMF.Items(1).Selected = False
            Else
                RB_GMF.Items(0).Selected = False
                RB_GMF.Items(1).Selected = True
            End If

            If Tipo.pnu_atr_003 = "S" Then
                DP_BancoCuenta.Enabled = True
                DP_BancoCuenta.CssClass = "clsMandatorio"
            Else
                Txt_NroCuenta.Text = ""
                DP_BancoCuenta.Enabled = False
                DP_BancoCuenta.CssClass = "clsDisabled"
            End If

            Txt_NroCuenta.Text = ""

            DP_BancoCuenta.ClearSelection()

        End If

    End Sub

    Protected Sub gd_gastdef_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gd_gastdef.SelectedIndexChanged

    End Sub

    Protected Sub DP_TipoDocto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_TipoDocto.SelectedIndexChanged

        Dim col As New Collection

        col = CMC.DiasDeRetencionDevuelve(Sucursal, Txt_PlazaDocto.Text, DP_TipoDocto.SelectedValue)


        If col.Count > 0 Then
            Txt_DiaRet.Text = col.Item(1)
        Else
            Txt_DiaRet.Text = 0
        End If

        CargaDocumentos()
        CargaCondiciones()

    End Sub

    Protected Sub IB_TodosDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            If Not IsNothing(Session("documentos")) Then
                documentos = Session("documentos")
            End If

            Dim cb As New CheckBox

            For i = 0 To Gr_DocCom.Rows.Count - 1
                cb = Gr_DocCom.Rows(i).FindControl("CB_DOC")
                If Not documentos.Contains(cb.ToolTip) Then
                    documentos.Add(cb.ToolTip)
                    cb.Checked = True
                End If
            Next

            Session("documentos") = documentos
            ModalPopupExtender1.Show()

        Catch ex As Exception
            Msj.Mensaje(Page, "Requisitos", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_TodosCon_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try


            Dim cb As New CheckBox
            For i = 0 To Gr_ConCom.Rows.Count - 1
                cb = Gr_ConCom.Rows(i).FindControl("CB_CON")
                cb.Checked = True
            Next

            ModalPopupExtender1.Show()

        Catch ex As Exception
            Msj.Mensaje(Page, "Requisitos", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub DP_Tipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Tipo.SelectedIndexChanged

        CargaDocumentos()
        'ModalPopupExtender1.Show()

    End Sub

    Protected Sub CB_DOC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Agrega o quita documento de collection
        documentos = Session("documentos")
        Dim cb As CheckBox = sender

        If cb.Checked Then

            Dim existe As Boolean = False

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    existe = True
                    Exit For
                End If
            Next

            If existe Then
                Exit Sub
            Else
                documentos.Add(cb.ToolTip)
            End If

        Else

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    documentos.Remove(I)
                    Exit For
                End If
            Next

        End If

        Session("documentos") = documentos
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub RB_Responsabilidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Responsabilidad.SelectedIndexChanged

        If RB_Responsabilidad.SelectedValue = "1" Then
            RB_OpePuntual.SelectedValue = "N"
            RB_OpePuntual.Enabled = True
        Else
            RB_OpePuntual.SelectedValue = "S"
            RB_OpePuntual.Enabled = False
        End If

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Ejecutar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_EnviarOpe.Click, IB_Ejecutar.Click
        Try

            If Validacion() Then

                'Ejecutar Negociacion
                If FechaVcto() Then

                    'Monto y Tasas
                    Txt_MtoNegociacion.Text = Txt_MtoEva.Text * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val

                    ProcesaNegociacion()

                    Dim formato As String
                    formato = RG.DevuelveFormatoMoneda(Me.DP_Moneda.SelectedValue)

                    Txt_MtoNegociacion.Text = Format(CDbl(Txt_MtoNegociacion.Text), FMT.FCMSD)

                End If

            End If


        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Guardar.Click 'Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20050404, Usr, "PRESIONO GUARDAR NEGOCIACIÓN") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Validacion() And FechaVcto() Then

                'Monto y Tasas
                Txt_MtoNegociacion.Text = Txt_MtoEva.Text * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val

                Dim sobregiro As Double
                Dim deuvigente As Double
                If CDbl(Txt_MtoUtilizado.Text) > CDbl(Txt_MtoAprLin.Text) Then
                    sobregiro = ((Txt_MtoAprLin.Text - Txt_MtoUtilizado.Text) * 100) / CDbl(Txt_MtoAprLin.Text)
                Else
                    sobregiro = 0
                End If

                deuvigente = (Txt_MtoUtilizado.Text * 100) / CDbl(Txt_MtoAprLin.Text)

                'Se modifica funcion para mostrar mensaje en caso que algun monto supere los de clasificacion
                Dim Comentario As String

                Comentario = OP.ValidaMontos(Txt_TasaSpread.Text, Txt_MtoDoctos.Text, sobregiro, deuvigente, Txt_PorCom.Text)

                If Not IsNothing(Comentario) Then
                    Msj.Mensaje(Me.Page, Caption, Comentario, ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_MtoNegociacion.Text = Format(CDbl(Txt_MtoNegociacion.Text), FMT.FCMSD)
                    Exit Sub
                End If

                ProcesaNegociacion()

                Txt_MtoNegociacion.Text = Format(CDbl(Txt_MtoNegociacion.Text), FMT.FCMSD)

                Try

                    If HF_Id_Opn.Value = "" Or HF_Id_Opn.Value = "0" Then

                        Dim valor As Integer

                        valor = CMC.NegociacionInserta(CargaObjetoNegociacion(), RutCli)

                        If valor = 0 Then
                            Msj.Mensaje(Me, "Atención", "La evaluación seleccionada ya esta asociada a una Negociación", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub
                        ElseIf valor = 99 Then
                            Msj.Mensaje(Me, "Atención", "Error al guardar negociaciòn: " & CMC.descripcion, ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If

                        HF_Id_Opn.Value = valor

                        If HF_Id_Opn.Value > 0 Then

                            '---------------------------------------------------------------------------------
                            'jlagos 19-05-2012 -guarda y eliminacion por negociacion
                            '---------------------------------------------------------------------------------
                            'Elimina documentos y condiciones
                            AG.DocComPorDoctoNegElimina(HF_Id_Opn.Value)
                            AG.ConComPorDoctoNegElimina(HF_Id_Opn.Value)

                            'Guarda documentos y condiciones
                            If Not IsNothing(Session("documentos")) Then
                                documentos = Session("documentos")
                            End If

                            For i = 1 To documentos.Count

                                Dim dxn As New dxn_cls

                                dxn.id_dxd = documentos(i).ToString()
                                dxn.id_opn = HF_Id_Opn.Value

                                AG.DocComsPorOperacionAoR(dxn)

                            Next

                            For i = 0 To Gr_ConCom.Rows.Count - 1
                                Dim cb As CheckBox = CType(Gr_ConCom.Rows(i).FindControl("CB_CON"), CheckBox)
                                If cb.Checked Then
                                    Dim cxn As New cxn_cls
                                    cxn.id_cxd = cb.ToolTip
                                    cxn.id_opn = HF_Id_Opn.Value
                                    AG.ConComsPorOperacionAoR(cxn)
                                End If
                            Next
                            '-------------------------------------------------------------------

                            If GrabaGastos() Then

                                'LimpiaHojaDeNegocio(False)
                                IB_Guardar.Enabled = False
                                IB_Ejecutar.Enabled = False
                                IB_EnviarOpe.Enabled = True
                                Txt_MtoNegociacion.Text = Format(CDbl(Txt_MtoNegociacion.Text), FMT.FCMSD)

                                Msj.Mensaje(Me, Caption, "Negociación Guardada", TipoDeMensaje._Informacion)

                                GeneraInforme()
                                IB_InfInstructivo.Enabled = True 'FY 06-07-2012
                                IB_Informe.Enabled = True
                            End If

                        End If


                    Else

                        If CMC.NegociacionActualiza(CargaObjetoNegociacion(), RutCli) Then

                            '-------------------------------------------------------------------
                            'jlagos 19-05-2012 -guarda y eliminacion por negociacion
                            '-------------------------------------------------------------------

                            'Elimina documentos y condiciones
                            AG.DocComPorDoctoNegElimina(HF_Id_Opn.Value)
                            AG.ConComPorDoctoNegElimina(HF_Id_Opn.Value)

                            'Guarda documentos y condiciones
                            If Not IsNothing(Session("documentos")) Then
                                documentos = Session("documentos")
                            End If

                            For i = 1 To documentos.Count

                                Dim dxn As New dxn_cls

                                dxn.id_dxd = documentos(i).ToString()
                                dxn.id_opn = HF_Id_Opn.Value

                                AG.DocComsPorOperacionAoR(dxn)

                            Next

                            For i = 0 To Gr_ConCom.Rows.Count - 1
                                Dim cb As CheckBox = CType(Gr_ConCom.Rows(i).FindControl("CB_CON"), CheckBox)
                                If cb.Checked Then
                                    Dim cxn As New cxn_cls
                                    cxn.id_cxd = cb.ToolTip
                                    cxn.id_opn = HF_Id_Opn.Value
                                    AG.ConComsPorOperacionAoR(cxn)
                                End If
                            Next

                            '-------------------------------------------------------------------

                            If GrabaGastos() Then

                                'LimpiaHojaDeNegocio(False)
                                IB_Guardar.Enabled = False
                                IB_Ejecutar.Enabled = False
                                IB_EnviarOpe.Enabled = True

                                Msj.Mensaje(Me, Caption, "Negociación Modificada", TipoDeMensaje._Informacion)

                                GeneraInforme()
                                IB_InfInstructivo.Enabled = True 'FY 06-07-2012
                                IB_Informe.Enabled = True
                            End If

                        Else
                            Msj.Mensaje(Me, Caption, "!Negociación se encuentra en estado simulada, no se puede modificar!", TipoDeMensaje._Informacion)
                            Exit Sub
                        End If

                    End If

                    'rescata las paridades del dia
                    Dim Coll = CG.ParidadesDelDiaDevuelve(Txt_FechaNegociacion.Text)

                    Dim Coll_NMN As New Collection

                    For I = 1 To Coll.Count

                        Dim NMN As New nmn_cls

                        NMN.id_opn = HF_Id_Opn.Value
                        NMN.id_p_0023 = Coll.Item(I).id_p_0023
                        NMN.par_val = Coll.Item(I).par_val
                        NMN.par_val_cob = Coll.Item(I).par_val

                        Coll_NMN.Add(NMN)

                    Next

                    'Genera_reporte_evaluacion (Versión: 12122013.V1)
                    AG.GuardaParidades(Coll_NMN)

                Catch ex As Exception
                    Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
                End Try

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Public Sub GeneraInforme()

        Try

            Dim op As New DataSet_Negociacion.sp_Reporte_Devuelve_opera_anteriores_negociacionDataTable
            Dim opn As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_opera_anteriores_negociacionTableAdapter

            Dim inf As New DataSet_Negociacion.sp_Informe_deu_cli_hoj_negDataTable
            Dim i As New DataSet_NegociacionTableAdapters.sp_Informe_deu_cli_hoj_negTableAdapter

            Dim rep As New DataSet_Negociacion.sp_Reporte_tas_hoj_negDataTable
            Dim r As New DataSet_NegociacionTableAdapters.sp_Reporte_tas_hoj_negTableAdapter

            Dim opngr As New DataSet_Negociacion.sp_Reporte_Devuelve_opn_cli_negDataTable
            Dim opgr As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_opn_cli_negTableAdapter

            Dim spread As New DataSet_Negociacion.sp_Reporte_Devuelve_spread_bancaDataTable
            Dim sp As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_spread_bancaTableAdapter

            Dim TAnt As New DataSet_Negociacion.sp_Reporte_Devuelve_Tasa_Anterior_cliDataTable
            Dim ta As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Tasa_Anterior_cliTableAdapter

            Dim gto As New DataSet_Negociacion.sp_Reporte_Devuelve_Gastos_DefinidosDataTable
            Dim g As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Gastos_DefinidosTableAdapter

            Dim gfn As New DataSet_Negociacion.sp_Reporte_Devuelve_Gastos_FijosDataTable
            Dim gf As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Gastos_FijosTableAdapter

            Dim doc As New DataSet_Negociacion.sp_Reporte_Devuelve_Documentos_NegociacionDataTable
            Dim dc As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_Documentos_NegociacionTableAdapter

            Dim con As New DataSet_Negociacion.sp_Reporte_Devuelve_condiciones_negociacionDataTable
            Dim co As New DataSet_NegociacionTableAdapters.sp_Reporte_Devuelve_condiciones_negociacionTableAdapter


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.ProcessingMode = ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\Reporte_Negociacion.rdlc"

            Dim rut As String
            Dim nro As Integer

            rut = Format(CLng(Txt_Rut_Cli.Text), VAR.FMT_RUT)
            nro = HF_Id_Opn.Value

            op = opn.GetData(nro)
            inf = i.GetData(rut)
            rep = r.GetData(rut)
            inf = i.GetData(rut)
            rep = r.GetData(rut)
            gto = g.GetData(nro)
            opngr = opgr.GetData(rut)
            spread = sp.GetData(nro)
            TAnt = ta.GetData(rut)
            gfn = gf.GetData(nro)

            doc = dc.GetData(nro)
            con = co.GetData(nro)

            Dim A As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt As DataTable

            dt = op

            A = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_opera_anteriores_negociacion", dt)

            Dim B As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt2 As DataTable

            dt2 = inf

            B = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Informe_deu_cli_hoj_neg", dt2)

            Dim C As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt3 As DataTable

            dt3 = rep

            C = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_tas_hoj_neg", dt3)

            Dim D As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt4 As DataTable

            dt4 = gto

            D = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Gastos_Definidos", dt4)

            Dim E As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt5 As DataTable

            dt5 = opngr

            E = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_opn_cli_neg", dt5)

            Dim F As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt6 As DataTable

            dt6 = spread

            F = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_spread_banca", dt6)

            Dim Ge As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt7 As DataTable

            dt7 = TAnt

            Ge = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Tasa_Anterior_cli", dt7)

            Dim H As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt8 As DataTable

            dt8 = gfn

            H = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Gastos_Fijos", dt8)

            Dim J As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt9 As DataTable

            dt9 = doc

            J = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_Documentos_Negociacion", dt9)

            Dim L As New Microsoft.Reporting.WebForms.ReportDataSource

            Dim dt10 As DataTable

            dt10 = con

            L = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_Negociacion_sp_Reporte_Devuelve_condiciones_negociacion", dt10)

            ReportViewer1.LocalReport.DataSources.Add(A)
            ReportViewer1.LocalReport.DataSources.Add(B)
            ReportViewer1.LocalReport.DataSources.Add(C)
            ReportViewer1.LocalReport.DataSources.Add(D)
            ReportViewer1.LocalReport.DataSources.Add(E)
            ReportViewer1.LocalReport.DataSources.Add(F)
            ReportViewer1.LocalReport.DataSources.Add(Ge)
            ReportViewer1.LocalReport.DataSources.Add(H)
            ReportViewer1.LocalReport.DataSources.Add(J)
            ReportViewer1.LocalReport.DataSources.Add(L)
            ReportViewer1.DataBind()


            'EXPORTACION DEL REPORTE
            Dim archivo As String = "Negociacion_" & rut & "_ID_" & nro & ".pdf"
            Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            Dim deviceInfo = String.Empty
            Dim type As String = "PDF"
            Dim encoding As String = String.Empty
            Dim mimeType As String = String.Empty
            Dim extension = String.Empty
            Dim warnings() As Warning = Nothing
            Dim streamIDs As String() = Nothing

            Dim pdfContent As Byte() = ReportViewer1.LocalReport.Render("PDF", _
                                                                        String.Empty, _
                                                                        mimeType, _
                                                                        encoding, _
                                                                        extension, _
                                                                        streamIDs, _
                                                                        warnings)

            'Dim pdfPath As String = path
            'Dim pdfFile As New FileStream(pdfPath, System.IO.FileMode.Create)

            'pdfFile.Write(pdfContent, 0, pdfContent.Length)
            'pdfFile.Close()

            CA.GuardaNegPDF(nro, pdfContent)


        Catch ex As Exception

        End Try

    End Sub

    Public Sub Genera_reporte_evaluacion()

        'Versión: 12122013.V1

        Try

            'Datos generales del cliente
            Dim Resumen As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_CabeceraDataTable
            Dim Tab1 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_CabeceraTableAdapter

            'grilla deudor
            Dim Deudores As New DataSet_ReporteEvaluacion.Sp_Reporte_Evaluacion_DeudoresDataTable
            Dim Tab As New DataSet_ReporteEvaluacionTableAdapters.Sp_Reporte_Evaluacion_DeudoresTableAdapter

            'grilla pagare vigentes
            Dim Pagares As New DataSet_ReporteEvaluacion.sp_pagares_vigentesDataTable
            Dim Tab2 As New DataSet_ReporteEvaluacionTableAdapters.sp_pagares_vigentesTableAdapter

            'grilla pagare vigentes
            Dim Concentracion As New DataSet_ReporteEvaluacion.Sp_Concentracion_ClienteDataTable
            Dim Tab3 As New DataSet_ReporteEvaluacionTableAdapters.Sp_Concentracion_ClienteTableAdapter

            'Distribucion deuda Morosa y Vctos. Futuros
            Dim Distribucion As New DataSet_ReporteEvaluacion.sp_distribucion_diasDataTable
            Dim Tab4 As New DataSet_ReporteEvaluacionTableAdapters.sp_distribucion_diasTableAdapter

            'Evolucion Mora 
            Dim evolucion As New DataSet_ReporteEvaluacion.sp_cl_evolucion_deudaDataTable
            Dim tab5 As New DataSet_ReporteEvaluacionTableAdapters.sp_cl_evolucion_deudaTableAdapter

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.Reset()

            ReportViewer1.LocalReport.ReportPath = "Modulos\Carp. Comercial\Reportes\ReportEvaluacion.rdlc"

            Dim CLI As cli_cls
            Dim id_eva As Integer
            Dim Moneda As Integer
            Dim Porcentaje As Decimal

            Dim neg As opn_cls = CMC.NegociacionDevuelve(Txt_Rut_Cli.Text, HF_Id_Opn.Value)

            Moneda = neg.id_opn
            Porcentaje = neg.opn_por_ant
            id_eva = neg.id_eva

            CLI = Session("Cliente")

            Resumen = Tab1.GetData(id_eva)

            Dim dt As DataTable

            dt = Resumen

            Deudores = Tab.GetData(id_eva)

            Dim dt2 As DataTable

            dt2 = Deudores

            Pagares = Tab2.GetData(CLI.cli_idc)

            Dim dt3 As DataTable

            dt3 = Pagares

            Concentracion = Tab3.GetData(CLI.cli_idc, 1, 999999999, "CLIENTE COMO CLIENTE", 1, 2)

            Dim dt4 As DataTable

            dt4 = Concentracion

            Distribucion = Tab4.GetData(CLI.cli_idc)

            Dim dt5 As DataTable

            dt5 = Distribucion

            evolucion = tab5.GetData(CLI.cli_idc)

            Dim dt6 As DataTable

            dt6 = evolucion


            Dim rsc As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim rds As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim pag As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim con As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim dis As New Microsoft.Reporting.WebForms.ReportDataSource
            Dim evo As New Microsoft.Reporting.WebForms.ReportDataSource

            rsc = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Cabecera", dt)
            rds = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Reporte_Evaluacion_Deudores", dt2)
            pag = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_pagares_vigentes", dt3)
            con = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_Sp_Concentracion_Cliente", dt4)
            dis = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_distribucion_dias", dt5)
            evo = New Microsoft.Reporting.WebForms.ReportDataSource("DataSet_ReporteEvaluacion_sp_cl_evolucion_deuda", dt6)

            ReportViewer1.LocalReport.DataSources.Add(rsc)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(pag)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(con)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(dis)
            ReportViewer1.DataBind()

            ReportViewer1.LocalReport.DataSources.Add(evo)
            ReportViewer1.DataBind()

            Dim archivo As String = "Evaluacion_" & CLI.cli_idc & "_ID_" & id_eva & ".pdf"
            Dim path As String = Server.MapPath("../../../PDF/" & archivo)

            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim fileNameExtension As String = Nothing
            Dim streams As String() = Nothing
            Dim war As Warning() = Nothing
            Dim Bit As Byte() = ReportViewer1.LocalReport.Render("PDF", _
                                                                 Nothing, _
                                                                 mimeType, _
                                                                 encoding, _
                                                                 fileNameExtension, _
                                                                 streams, _
                                                                 war)
            CA.GuardaPDF(id_eva, Bit)
            ReportViewer1.Visible = False

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

        If Request.QueryString("Pizarra") = "" Then
            Response.Redirect("Negociacion.aspx", False)
        End If

    End Sub

    Protected Sub IB_EnviarOpe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_EnviarOpe.Click

        Try

            If Not agt.ValidaAccesso(20, 20050404, Usr, "PRESIONO ENVIAR NEG. A OPERACION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Se crear una operacion de esta negociacion
            Dim OPE As New ope_cls
            Dim ldc As ldc_cls

            ldc = CG.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text, 1)

            OPE.id_ope = Nothing

            If Not IsNothing(ldc) Then
                OPE.id_ldc = ldc.id_ldc
            Else
                OPE.id_ldc = Nothing
            End If

            Dim cli As cli_cls
            cli = Session("Cliente")

            OPE.id_P_0030 = 1
            OPE.ope_com_tot = RG.comasXptos(Txt_ComiPorDocto.Text)
            OPE.ope_por_ant = Txt_PorAnt.Text.Replace(".", ",")
            OPE.ope_fev = Txt_FecVto.Text
            OPE.ope_mto_ant = CDbl(RG.comasXptos(Txt_MtoEva.Text)) '/ (CDbl(Txt_PorAnt.Text.Replace(".", ",")) / 100)
            OPE.ope_dif_pre = CDbl(RG.comasXptos(Txt_DifPrecio.Text))
            OPE.ope_iva_com = Me.Txt_Iva.Text
            OPE.ope_pre_com = Me.Txt_PrecioCompra.Text
            OPE.ope_sal_pag = Me.Txt_SaldoPagado.Text
            OPE.ope_sal_pen = Me.Txt_SaldoPendiente.Text
            OPE.ope_tot_gir = Me.Txt_TotalGirar.Text
            OPE.ope_cdo = "N"
            OPE.ope_cnt = "0"
            OPE.id_opn = HF_Id_Opn.Value
            OPE.ope_fec_sim = Txt_FechaNegociacion.Text
            OPE.id_eje = cli.id_eje_cod_eje 'se cambia por ejecutivo del cliente (jlagos 13-10-2012)
            OPE.ope_mon_gas = Txt_GastosExentos.Text
            OPE.ope_mon_gas_afe = Txt_GastosAfectos.Text 'jlagos 25-06-2012 se agrega el monto del gasto afecto a IVA
            OPE.ope_val_gmf = Txt_GMF.Text 'jlagos 25-06-2012 se agrega el monto del seguro
            OPE.ope_tot_gir_ant = OPE.ope_tot_gir + OPE.ope_val_gmf  'jlagos 20-11-2012 se agrega el total a girar original

            '------------------------------------------------------
            'jlagos 19-05-2012 -se agrega datos hacia la operacion
            '------------------------------------------------------
            OPE.ope_ptl = Me.RB_OpePuntual.SelectedValue
            OPE.ope_res_son = RB_Responsabilidad.SelectedValue
            OPE.ope_cuo = RBConCuotas.SelectedValue
            OPE.ope_lnl = RB_ModoOpera.SelectedValue
            '------------------------------------------------------

            If DP_Moneda.SelectedValue = 1 Then
                OPE.ope_fac_cam = 1
            Else
                Dim Par As par_cls
                Par = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text)
                If IsNothing(Par) Then
                    OPE.ope_fac_cam = 0
                    Msj.Mensaje(Me, Caption, "No existe factor de cambio para la moneda y la fecha seleccionada", TipoDeMensaje._Informacion)
                    Exit Sub
                Else
                    OPE.ope_fac_cam = Par.par_val
                End If
            End If

            If OP.OperacionInserta(OPE, DP_TipoDocto.SelectedValue) = 0 Then
                Msj.Mensaje(Me, Caption, "Negociación no se puede enviar a operaciones, ya esta simulada.", TipoDeMensaje._Informacion)
            Else
                Msj.Mensaje(Me, Caption, "Negociación enviada a Operación", TipoDeMensaje._Informacion)
            End If

            IB_EnviarOpe.Enabled = False
            IB_Guardar.Enabled = False

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_InfInstructivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_InfInstructivo.Click
        Try

            RW.AbrePopup(Me, 1, "Informe_Negociacion.aspx?IdOpn=" & HF_Id_Opn.Value & "&informe=2", "", 1000, 800, 10, 10)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click

        'Negociación
        Dim abytFileData As Byte() = CA.DespliegaArchivoNegPDF(HF_Id_Opn.Value)
        If abytFileData.Length <> 0 Then

            Dim archivo As String = "Negociacion_" & HF_Id_Opn.Value & ".pdf"

            Response.Buffer = False
            Response.Expires = -1
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Type", "application/pdf")
            Response.AddHeader("Content-Length", abytFileData.Length.ToString)
            Response.AddHeader("Content-Disposition", "attachment;filename=" & archivo & "")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.BinaryWrite(abytFileData)
            Response.End()

        End If

    End Sub



#End Region

#Region "Private Function y Sub"

    Private Sub CargaEvaluaciones(ByVal Todas As Boolean)
        Try

            If Request.QueryString("nro") <> "" Then

                Coll_Eva = New Collection
                If Request.QueryString("Pizarra") <> "" Then
                    'trae evaluaciones que esten con estado ingresa, negociacion y simulada
                    Coll_Eva = CMC.Evaluaciones_Devuelve(1, 3, Txt_Rut_Cli.Text, Nothing, False)
                Else
                    'trae evaluaciones que esten con estado ingresa, negociacion
                    Coll_Eva = CMC.Evaluaciones_Devuelve(1, 3, Txt_Rut_Cli.Text, Nothing, False)
                End If


            Else

                Coll_Eva = New Collection

                If Request.QueryString("Pizarra") <> "" Then
                    'trae evaluaciones que esten con estado ingresa, negociacion y simulada
                    Coll_Eva = CMC.Evaluaciones_Devuelve(1, 3, Txt_Rut_Cli.Text, Nothing, False)
                Else
                    'trae evaluaciones que esten con estado ingresadas
                    Coll_Eva = CMC.Evaluaciones_Libres_Devuelve(1, 1, Txt_Rut_Cli.Text, Nothing, False)
                End If

            End If

            Dim C As New Collection

            For I = 1 To Coll_Eva.Count

                Dim formato As String

                formato = RG.DevuelveFormatoMoneda(Coll_Eva.Item(I).Id_Moneda)

                C.Add(New FuncionesGenerales.Class_LlenaCombo(Coll_Eva.Item(I).Codigo, _
                                                              Coll_Eva.Item(I).Moneda & " - " & _
                                                              Format(CDbl(Coll_Eva.Item(I).Monto), formato) & " " & _
                                                              Format(Coll_Eva.Item(I).Porcentaje, FMT.FSMCD) & " " & _
                                                              Format(CDate(Coll_Eva.Item(I).Fecha), "dd/MM/yyyy") & "  #Pag. " & _
                                                              Coll_Eva.Item(I).Deudores))

            Next

            RW.Llenar_Drop(C, "Codigo", "Descripcion", DP_Evaluaciones)

            If C.Count = 0 Then

                Msj.Mensaje(Me, Caption, "No existen evaluaciones para este cliente, no puede continuar.", TipoDeMensaje._Exclamacion)

                IB_Guardar.Enabled = False
                IB_Ejecutar.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function CargaObjetoNegociacion() As opn_cls

        Try

            Dim OPN As New opn_cls
            Dim cli As cli_cls
            Dim MtoDoc As Double

            If Not IsNothing(Session("Cliente")) Then
                cli = Session("Cliente")
            End If

            'OPN.cli_idc = Format(CLng(RG.comasXptos(Txt_Rut_Cli.Text)), VAR.FMT_RUT)

            If HF_Id_Opn.Value = "" Then
                OPN.id_opn = Nothing
            Else
                OPN.id_opn = HF_Id_Opn.Value
            End If

            OPN.id_suc = DP_Sucursal.SelectedValue 'cli.id_suc 'se cambia por sucursal del cliente (jlagos 13-10-2012)
            OPN.id_eva = DP_Evaluaciones.SelectedValue 'HF_NroEva.Value
            OPN.id_eje = CodEje
            OPN.opn_fec = Date.Now
            OPN.id_P_0031 = DP_TipoDocto.SelectedValue
            OPN.opn_can_doc = Val(Txt_CanDocto.Text.Replace(".", ""))
            OPN.opn_dia_vto = Val(Txt_DiaVto.Text)
            OPN.opn_por_ant = Txt_PorAnt.Text.Replace(".", ",")
            OPN.cal_oto_gam = DP_Calificacion.SelectedValue.Trim()
            OPN.opn_tip_tas = DP_TipoTasa.SelectedValue.Trim().ToUpper()

            MtoDoc = Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).monto_doc

            If Me.DP_Moneda.SelectedValue <> 1 Then
                OPN.opn_mto_doc = MtoDoc
            Else
                OPN.opn_mto_doc = Math.Round(MtoDoc, MidpointRounding.AwayFromZero)
            End If

            OPN.id_P_0023 = DP_Moneda.SelectedValue
            OPN.opn_fev = Txt_FecVto.Text
            OPN.opn_fev_ori = Txt_FecVctoReal.Text

            If DP_TipoOperacion.SelectedValue = 0 Then
                OPN.id_P_0012 = Nothing
            Else
                OPN.id_P_0012 = DP_TipoOperacion.SelectedValue
            End If

            OPN.opn_com_neg = Txt_ExcepCondi.Text.Trim
            OPN.opn_ins_neg = Txt_InstrucCursar.Text.Trim
            OPN.opn_fec_neg = Txt_FechaNegociacion.Text

            'confirmar si es estado de operacion o negociacion
            OPN.id_P_0082 = 1
            OPN.opn_mto_des = RG.comasXptos(Txt_MtoDescuentos.Text)

            If Txt_PorCom.Text.Trim = "" Then
                OPN.opn_por_com = 0
            Else
                OPN.opn_por_com = Txt_PorCom.Text.Replace(".", ",")
            End If

            If DP_MonedaCom.SelectedValue = 0 Then
                OPN.id_P_0023_com = Nothing
            Else
                OPN.id_P_0023_com = DP_MonedaCom.SelectedValue
            End If

            If Txt_Minimo.Text.Trim = "" Then
                OPN.opn_com_min = 0
            Else
                OPN.opn_com_min = Txt_Minimo.Text
            End If

            If Txt_Maximo.Text.Trim = "" Then
                OPN.opn_com_max = 0
            Else
                OPN.opn_com_max = Txt_Maximo.Text
            End If

            OPN.opn_com_tot = Val(Hf_com_doc.Value)

            If DP_MonComFlat.SelectedValue = 0 Then
                OPN.id_P_0023_fla = Nothing
            Else
                OPN.id_P_0023_fla = DP_MonComFlat.SelectedValue
            End If

            If Txt_PorComFlat.Text.Trim = "" Then
                OPN.opn_com_fla = 0
            Else
                OPN.opn_com_fla = Txt_PorComFlat.Text
            End If

            OPN.opn_can_ddr = Txt_CantDeu.Text

            '--------------------------------------------------
            'jlagos 19-05-2012 -se agrega datos de la operacion
            '--------------------------------------------------
            'opciones de operacion
            OPN.opn_ptl = Me.RB_OpePuntual.SelectedValue
            OPN.opn_res_son = RB_Responsabilidad.SelectedValue
            OPN.opn_cuo = RBConCuotas.SelectedValue
            OPN.opn_lnl = RB_ModoOpera.SelectedValue
            OPN.opn_gen_gmf = RB_GMF.SelectedValue
            '--------------------------------------------------

            OPN.opn_tas_bas = Txt_TasaBase.Text.Replace(".", ",")
            OPN.opn_spr_ead = Txt_TasaSpread.Text.Replace(".", ",")
            OPN.opn_pto_spr = Txt_Puntos.Text.Replace(".", ",")
            OPN.opn_tas_neg = Txt_TasaNegocio.Text.Replace(".", ",")

            If DP_FormaPago.SelectedValue = 0 Then
                OPN.id_P_0056 = Nothing
            Else
                OPN.id_P_0056 = DP_FormaPago.SelectedValue
            End If

            If DP_BancoCuenta.SelectedValue = 0 Then
                OPN.id_bco = Nothing
            Else
                OPN.id_bco = DP_BancoCuenta.SelectedValue
            End If

            OPN.opn_cta_cte = Txt_NroCuenta.Text

            'check
            'If CB_Contrato.Checked Then
            '    OPN.opn_cto_son = "S"
            'Else
            '    OPN.opn_cto_son = "N"
            'End If

            'If CB_Contrato.Checked Then
            '    OPN.opn_pag_son = "S"
            'Else
            '    OPN.opn_pag_son = "N"
            'End If

            'If CB_Contrato.Checked Then
            '    OPN.opn_mdt_son = "S"
            'Else
            '    OPN.opn_mdt_son = "N"
            'End If

            If CB_Antes14.Checked Then
                OPN.opn_ant_014 = "S"
            Else
                OPN.opn_ant_014 = "N"
            End If

            If DP_TipoPagare.SelectedValue = 0 Then
                OPN.id_P_0021 = Nothing
            Else
                OPN.id_P_0021 = DP_TipoPagare.SelectedValue
            End If

            If Txt_FecVctoPagare.Text = "" Then
                OPN.opn_pgr_fec_vto = "01/01/1900"
            Else
                OPN.opn_pgr_fec_vto = Txt_FecVctoPagare.Text
            End If

            If Txt_MtoPagare.Text.Trim = "" Then
                OPN.opn_pgr_mto = 0
            Else
                OPN.opn_pgr_mto = RG.comasXptos(Txt_MtoPagare.Text)
            End If


            OPN.opn_com_neg = Txt_ExcepCondi.Text.Trim
            OPN.opn_ins_neg = Txt_InstrucCursar.Text.Trim
            OPN.opn_tas_moa = Me.DP_mora.SelectedValue
            'Comision Pagare
            'OPN.opn_pgr_com = Txt_MtoImpuesto.Text

            Return OPN


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return Nothing
        End Try

    End Function

    Private Sub AlineaTextBoxDerecha()

        'Datos Diarios
        'Txt_UF.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Dolar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_TMC.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Tasas
        Txt_TasaBase.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_TasaSpread.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Puntos.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Datos Comerciales
        Txt_MtoAprLin.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_SaldoLinea.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_NuevoSaldoLinea.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoNuevaDeuda.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoProvi.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoUtilizado.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoNegociacion.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoPagareVig.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoDescuentos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_PlazaDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DiaVto.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Saldos Reales
        Txt_DeudaCanje.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_ExcedPendiente.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DeudaMorosa.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_CtasPorCobrar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DeudaVigente.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_LetrasMora.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DeudaPuntual.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_CtasPorPagar.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MontoDeuda.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Totales
        Txt_MtoDoctos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoAnticipo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DifPrecio.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_PrecioCompra.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_SaldoPendiente.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_SaldoPagado.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_ComiPorDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_ComiFlat.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Iva.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GastosExentos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GastosAfectos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GMF.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_TotalGirar.Attributes.Add("Style", "TEXT-ALIGN: right")

        txt_mto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_PorComFlat.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_CanDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DiaVto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_DiaRet.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_PorAnt.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_CantDeu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoEva.Attributes.Add("Style", "TEXT-ALIGN: right")


    End Sub

    Private Sub CargaDatosNuevaOperacion()
        Try

            Dim LDC As ldc_cls
            Dim RSC As Object
            Dim APC As Object
            Dim Mto_Aprobado As Double
            Dim Mto_Ocupado As Double


            LDC = CG.LineaDeCreditoDevuelve(Txt_Rut_Cli.Text.Replace(".", "").Trim, 1)
            RSC = CMC.ResumenClienteDevuelve(Txt_Rut_Cli.Text.Replace(".", "").Trim, CodEje)

            If Not IsNothing(LDC) Then
                APC = CG.AnticipoDevuelvePorLinea(False, Nothing, LDC.id_ldc, LDC.id_ldc, 0, 999)
                HF_IdSbl.Value = LDC.id_ldc

            End If

            If Not IsNothing(LDC) Then
                Session("LineaCredito") = LDC
            End If

            If Not IsNothing(RSC) Then
                Session("ResumenCliente") = RSC
            End If

            If Not IsNothing(APC) Then
                Session("Anticipos") = APC
            End If


            '***********************************************Datos Comerciales*******************************************************
            'Si el monto aprobado es igual a cero se toma lo que esta ocupando en la tabla de resumen del cliente
            If IsNothing(LDC) Then

                Txt_MtoAprLin.Text = 0
                Txt_PorAnt.Text = 0
                '   Txt_TasaSpread.Text = 0
                Txt_Puntos.Text = 0

                If IsNothing(RSC) Then
                    Txt_MtoUtilizado.Text = 0
                    Mto_Aprobado = 0
                Else
                    Txt_MtoUtilizado.Text = Format(RSC.rsc_mto_ocu, FMT.FCMSD)
                    Mto_Ocupado = RSC.rsc_mto_ocu
                End If


            Else

                If IsNothing(APC) Then
                    Txt_PorAnt.Text = 0
                Else
                    For Each A In APC
                        Txt_PorAnt.Text = A.apc_pct
                    Next
                End If

                Txt_FechaVctoLinea.Text = LDC.ldc_fec_vig_hta
                Txt_Puntos.Text = Format(IIf(IsNothing(LDC.ldc_pto_spr), 0, LDC.ldc_pto_spr), FMT.FSMCD)
                Txt_MtoAprLin.Text = Format(LDC.ldc_mto_apb, FMT.FCMSD)
                Txt_MtoUtilizado.Text = Format(LDC.ldc_mto_ocp, FMT.FCMSD)
                Mto_Aprobado = Format(LDC.ldc_mto_apb, FMT.FCMSD)

                If IsNothing(LDC.ldc_mto_ocp) Then
                    Mto_Ocupado = 0
                Else
                    Mto_Ocupado = Format(LDC.ldc_mto_ocp, FMT.FCMSD)
                End If

                If Txt_MtoUtilizado.Text = "" Then
                    Txt_MtoUtilizado.Text = 0
                End If
            End If

            Txt_SaldoLinea.Text = Format(Mto_Aprobado - Mto_Ocupado, FMT.FCMSD)
            Txt_MtoNegociacion.Text = 0
            Txt_NuevoSaldoLinea.Text = 0
            Txt_FecVctoNeg.Text = ""
            Txt_MtoNuevaDeuda.Text = 0
            Txt_MtoDescuentos.Text = 0
            Txt_MtoProvi.Text = 0

            '**************************************************************************************************************************

            '***********************************************Saldos Reales*******************************************************
            If Not IsNothing(RSC) Then

                Txt_MtoPagareVig.Text = Format(RSC.rsc_mto_pgr_vig, FMT.FCMSD)
                Txt_DeudaCanje.Text = 0
                Txt_DeudaVigente.Text = Format(RSC.rsc_mto_vig + RSC.rsc_mto_pgr_vig, FMT.FCMSD)
                If Txt_DeudaVigente.Text = "" Then
                    Txt_DeudaVigente.Text = 0
                End If
                Txt_DeudaMorosa.Text = Format(RSC.Monto_Mora, FMT.FCMSD)

                If Txt_DeudaMorosa.Text = "" Then
                    Txt_DeudaMorosa.Text = 0
                End If

                Txt_ExcedPendiente.Text = Format(RSC.rsc_mto_exd, FMT.FCMSD)

                If Txt_ExcedPendiente.Text = "" Then
                    Txt_ExcedPendiente.Text = 0
                End If

                Txt_LetrasMora.Text = Format(RSC.rsc_mto_let_mor, FMT.FCMSD)

                If Txt_LetrasMora.Text = "" Then
                    Txt_LetrasMora.Text = 0
                End If

                Txt_CtasPorCobrar.Text = Format(RSC.rsc_mto_cxc, FMT.FCMSD)

                If Txt_CtasPorCobrar.Text = "" Then
                    Txt_CtasPorCobrar.Text = 0
                End If

                Txt_CtasPorPagar.Text = Format(RSC.rsc_mto_cxp, FMT.FCMSD)

                If Txt_CtasPorPagar.Text = "" Then
                    Txt_CtasPorPagar.Text = 0
                End If

                If Not IsNothing(LDC) Then
                    Txt_DeudaPuntual.Text = Format(RSC.rsc_mto_ocu - LDC.ldc_mto_ocp, FMT.FCMSD)
                    If Txt_DeudaPuntual.Text < 0 Then
                        Txt_DeudaPuntual.Text = Txt_DeudaPuntual.Text * -1
                        Txt_DeudaPuntual.Text = Format(CDbl(Txt_DeudaPuntual.Text), FMT.FCMSD)
                    End If

                Else
                    Txt_DeudaPuntual.Text = Format(RSC.rsc_mto_ocu, FMT.FCMSD)
                End If


                Txt_MontoDeuda.Text = Format((RSC.rsc_mto_cnj_mpl + RSC.rsc_mto_cnj_opl) + _
                                             (RSC.rsc_mto_vig + RSC.rsc_mto_pgr_vig) + RSC.Monto_Mora - _
                                              RSC.rsc_mto_exd + RSC.rsc_mto_let_mor + RSC.rsc_mto_cxc - _
                                              RSC.rsc_mto_cxp, FMT.FCMSD)

            Else

                Txt_MtoPagareVig.Text = 0
                Txt_DeudaCanje.Text = 0
                Txt_DeudaVigente.Text = 0
                Txt_DeudaMorosa.Text = 0
                Txt_ExcedPendiente.Text = 0
                Txt_LetrasMora.Text = 0
                Txt_CtasPorCobrar.Text = 0
                Txt_CtasPorPagar.Text = 0
                Txt_DeudaPuntual.Text = 0
                Txt_MontoDeuda.Text = 0

            End If


            RB_ModoOpera.Enabled = True
            RB_OpePuntual.Enabled = True
            RB_Responsabilidad.Enabled = True
            RBConCuotas.Enabled = True
            'RB_GMF.Enabled = True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaNegociacionAnterior(ByVal Nro As Integer)
        Try

            Dim OPN As opn_cls

            OPN = CMC.NegociacionDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Nro)

            If IsNothing(OPN) Then
                Msj.Mensaje(Me, Caption, "No se pudo cargar la negociación", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            'Se le asigna la fecha del dia de la negociacion
            Txt_FechaNegociacion.Text = Format(CDate(OPN.opn_fec_neg), "dd/MM/yyyy") 'RG.FUNFecReg(OPN.opn_fec_neg)
            CargaDatosDiarios()

            Dim FormatoMonedaOperacion As String = ""

            DP_Evaluaciones.ClearSelection()
            DP_Evaluaciones.SelectedValue = OPN.id_eva

            FormatoMonedaOperacion = RG.DevuelveFormatoMoneda(DP_Moneda.SelectedValue)

            'Datos de Operacion
            DP_Sucursal.ClearSelection()
            DP_Sucursal.SelectedValue = OPN.id_suc
            DP_TipoDocto.SelectedValue = OPN.id_P_0031
            Txt_PorAnt.Text = OPN.opn_por_ant
            DP_Moneda.SelectedValue = OPN.id_P_0023

            Txt_CantDeu.Text = OPN.opn_can_ddr
            Txt_FecIng.Text = Format(CDate(OPN.opn_fec), "dd/MM/yyyy")
            Txt_MtoEva.Text = Format(OPN.opn_mto_doc, FormatoMonedaOperacion)

            If OPN.id_P_0082 = 5 Then
                DP_EstadoNeg.SelectedValue = 1
            Else
                DP_EstadoNeg.SelectedValue = OPN.id_P_0082
            End If

            '------------------------------------------------------------------------
            'jlagos 19-05-2012 
            '------------------------------------------------------------------------
            CargaDocumentos()
            CargaCondiciones()

            Dim con As IQueryable = CG.DocConDevuelvePorNegociacionPorTipo("C", HF_Id_Opn.Value)

            For i = 0 To Gr_ConCom.Rows.Count - 1

                Dim cb As CheckBox = CType(Gr_ConCom.Rows(i).FindControl("CB_CON"), CheckBox)

                For Each c In con
                    If cb.ToolTip = c Then
                        cb.Checked = True
                        Exit For
                    End If
                Next

            Next

            '------------------------------------------------------------------------
            'Datos Documentos
            Txt_CanDocto.Text = OPN.opn_can_doc
            Txt_PlazaDocto.Text = IIf(IsNothing(OPN.id_PL_000047), "", OPN.id_PL_000047)
            Txt_DiaVto.Text = OPN.opn_dia_vto

            Dim col As New Collection

            col = CMC.DiasDeRetencionDevuelve(Sucursal, Txt_PlazaDocto.Text, DP_TipoDocto.SelectedValue)

            If Not IsNothing(col) Then
                If col.Count > 0 Then
                    Txt_DiaRet.Text = col.Item(1)
                Else
                    Txt_DiaRet.Text = 0
                End If
            End If

            Txt_FecVto.Text = Format(CDate(IIf(IsNothing(OPN.opn_fev), "01/01/1900", OPN.opn_fev)), "dd/MM/yyyy")

            Me.Txt_FecVctoReal.Text = CG.calcula_vcto_real("", _
                                                           CDate(Me.Txt_FecVto.Text).AddDays(Txt_DiaVto.Text), _
                                                           Sucursal, _
                                                           Txt_PlazaDocto.Text, _
                                                           DP_TipoDocto.SelectedValue)

            Txt_FecVctoNeg.Text = Format(CDate(Txt_FecVctoReal.Text), "dd/MM/yyyy")

            'Parametros de la Operacion
            Txt_PorCom.Text = IIf(IsNothing(OPN.opn_por_com), "", OPN.opn_por_com)
            DP_MonedaCom.SelectedValue = IIf(IsNothing(OPN.id_P_0023_com), 0, OPN.id_P_0023_com)

            Txt_Minimo.Text = Format(OPN.opn_com_min, RG.DevuelveFormatoMoneda(DP_MonedaCom.SelectedValue))

            Txt_Maximo.Text = Format(OPN.opn_com_max, RG.DevuelveFormatoMoneda(DP_MonedaCom.SelectedValue))

            Txt_Minimo_MaskedEditExtender.Enabled = True
            Txt_Maximo_MaskedEditExtender.Enabled = True

            Select Case DP_MonedaCom.SelectedValue
                Case 1

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999,999"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999.9999"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999.99"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999.99"
            End Select


            DP_MonComFlat.SelectedValue = IIf(IsNothing(OPN.id_P_0023_fla), 0, OPN.id_P_0023_fla)

            If DP_MonComFlat.SelectedValue = 0 Then 'Para que no pase por textchange Txt_PorComFlat al agregar gastos fijos
                Txt_PorComFlat.Text = 0
            Else
                Txt_PorComFlat.Text = Format(OPN.opn_com_fla, RG.DevuelveFormatoMoneda(DP_MonComFlat.SelectedValue))
            End If

            'Se da formato a comision flat
            Select Case DP_MonComFlat.SelectedValue
                Case 1
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999.99"

            End Select

            'Tasa Fija o DTF
            Txt_TasaBase.Text = OPN.opn_tas_bas
            Txt_TasaSpread.Text = OPN.opn_spr_ead
            Txt_Puntos.Text = OPN.opn_pto_spr
            Txt_TasaNegocio.Text = OPN.opn_tas_neg
            Txt_MtoDoctos.Text = Txt_MtoEva.Text

            DP_TipoTasa.ClearSelection()
            DP_TipoTasa.SelectedValue = IIf(IsNothing(OPN.opn_tip_tas), OPN.eva_cls.cli_cls.CLI_TIP_TAS, OPN.opn_tip_tas)

            Select Case DP_TipoTasa.SelectedValue
                Case "F"
                    LB_TasaBase.Text = "Tasa Fija E.A."
                Case "V"
                    LB_TasaBase.Text = "DTF E.A."
            End Select

            DiferenciaPrecio()

            DP_TipoOperacion.SelectedValue = IIf(IsNothing(OPN.id_P_0012), 0, OPN.id_P_0012)
            TipoOperacion()

            DP_FormaPago.SelectedValue = IIf(IsNothing(OPN.id_P_0056), 0, OPN.id_P_0056)
            FormaPago()

            DP_BancoCuenta.SelectedValue = IIf(IsNothing(OPN.id_bco), 0, OPN.id_bco)
            Txt_NroCuenta.Text = IIf(IsNothing(OPN.opn_cta_cte), "", OPN.opn_cta_cte)

            If DP_TipoOperacion.SelectedValue = 2 Then
                Me.DP_FormaPago.Enabled = False
            End If

            If OPN.opn_ant_014 = "S" Then
                CB_Antes14.Checked = True
            Else
                CB_Antes14.Checked = False
            End If

            'Pagare
            DP_TipoPagare.SelectedValue = IIf(IsNothing(OPN.id_P_0021), 0, OPN.id_P_0021)
            Txt_FecVctoPagare.Text = IIf(IsNothing(OPN.opn_pgr_fec_vto), "01-01-1900", OPN.opn_pgr_fec_vto) 'FY 06-07-2012
            Txt_MtoPagare.Text = IIf(IsNothing(OPN.opn_pgr_mto), 0, OPN.opn_pgr_mto)
            Txt_MtoImpuesto.Text = IIf(IsNothing(OPN.opn_pgr_com), 0, OPN.opn_pgr_com)

            RB_OpePuntual.SelectedValue = If(IsNothing(OPN.opn_ptl), "N", OPN.opn_ptl.ToString())
            RB_Responsabilidad.SelectedValue = If(IsNothing(OPN.opn_res_son), "0", OPN.opn_res_son.ToString())
            RBConCuotas.SelectedValue = If(IsNothing(OPN.opn_cuo), "N", OPN.opn_cuo.ToString())
            RB_ModoOpera.SelectedValue = If(IsNothing(OPN.opn_lnl), "N", OPN.opn_lnl.ToString())
            RB_GMF.SelectedValue = If(IsNothing(OPN.opn_gen_gmf), "N", OPN.opn_gen_gmf.ToString())

            Txt_ExcepCondi.Text = OPN.opn_com_neg
            Txt_InstrucCursar.Text = OPN.opn_ins_neg

            Dim Mto_Des As Double
            Dim Sal_Lin As Double
            Dim Mto_Eva As Double
            Dim Deu_Pun As Double
            Dim Mto_Uti As Double

            If Txt_SaldoLinea.Text.Trim = "" Then
                Sal_Lin = 0
            Else
                Sal_Lin = CDbl(Txt_SaldoLinea.Text)
            End If

            If Txt_MtoEva.Text.Trim = "" Then
                Mto_Eva = 0
            Else
                Mto_Eva = CDbl(Txt_MtoEva.Text)
            End If

            If Txt_MtoDescuentos.Text.Trim = "" Then
                Mto_Des = 0
            Else
                Mto_Des = CDbl(Txt_MtoDescuentos.Text)
            End If

            If Txt_MtoUtilizado.Text.Trim = "" Then
                Mto_Uti = 0
            Else
                Mto_Uti = CDbl(Txt_MtoUtilizado.Text)
            End If

            If Txt_DeudaPuntual.Text.Trim = "" Then
                Deu_Pun = 0
            Else
                Deu_Pun = CDbl(Txt_DeudaPuntual.Text)
            End If

            Txt_NuevoSaldoLinea.Text = Format(Sal_Lin - (Mto_Eva * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val) - Mto_Des, FMT.FCMSD)
            Txt_MtoProvi.Text = Format((Mto_Eva * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val) - Mto_Des, FMT.FCMSD)
            Txt_MtoNuevaDeuda.Text = Format((Mto_Uti + (Mto_Eva * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val)) - Mto_Des + Deu_Pun, FMT.FCMSD)
            Me.Txt_MtoNegociacion.Text = Format(Me.Txt_MtoEva.Text * CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val, FMT.FCMSD)

            If OPN.opn_tas_moa <> Nothing Then
                Me.DP_mora.SelectedValue = OPN.opn_tas_moa
            End If

            CargaGastosDeNegociacion()

            'Calificaciones
            DP_Calificacion.ClearSelection()
            DP_Calificacion.Items.FindByValue(OPN.cal_oto_gam.Trim()).Selected = True

            ProcesaNegociacion()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaGastosDeNegociacion()

        Try

            Dim Def As Object
            Dim Fij As Object
            Dim Gastos As ClsGastos
            Dim GastosExento, GastosAfecto As Double
            Dim GastosNegociacion As Double

            Def = CMC.GastosDeNegociacionDevuelve(HF_Id_Opn.Value, 1)
            Fij = CMC.GastosDeNegociacionDevuelve(HF_Id_Opn.Value, 2)

            GastosNeg = New Collection

            Txt_GastosExentos.Text = "0"
            Txt_GastosAfectos.Text = "0"
            txt_tot_gto_fij.Text = "0"
            txt_tot_gto_def.Text = "0"
            LB_TotalGastos.Text = "0"

            For Each D In Def

                Gastos = New ClsGastos
                Gastos.Código = D.id_gto
                Gastos.Descripción = D.gto_des
                Gastos.Monto = D.gto_mto
                Gastos.Tipo = "D"
                Gastos.AfectoExento = D.AfectoExento

                GastosNeg.Add(Gastos)

                Dim cb As CheckBox

                For I = 0 To gd_gastdef.Rows.Count - 1
                    If gd_gastdef.Rows(I).Cells(1).Text = Gastos.Código Then
                        cb = CType(gd_gastdef.Rows(I).FindControl("ch_sel"), CheckBox)
                        cb.Checked = True
                        Exit For
                    End If
                Next

                Select Case D.id_p_0036
                    Case 1
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + CDbl(Gastos.Monto)
                        'Gastos.Monto = Gastos.Monto
                        GastosNegociacion = Gastos.Monto
                    Case 2 'por deudor
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + (CDbl(Gastos.Monto) * Val(Txt_CantDeu.Text))
                        'Gastos.Monto = Gastos.Monto * Val(Txt_CantDeu.Text)
                        GastosNegociacion = Gastos.Monto * Val(Txt_CantDeu.Text)
                    Case 3 'por documento
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + (CDbl(Gastos.Monto) * Val(Txt_CanDocto.Text))
                        'Gastos.Monto = Gastos.Monto * Val(Txt_CanDocto.Text)
                        GastosNegociacion = Gastos.Monto * Val(Txt_CanDocto.Text)

                End Select

                If Gastos.AfectoExento = "S" Then
                    GastosAfecto += GastosNegociacion
                Else
                    GastosExento += GastosNegociacion
                End If

                'Me.txt_tot_gto_def.Text = CDbl(Me.txt_tot_gto_def.Text) + CDbl(Gastos.Monto)

            Next


            gastofijo = New Collection

            For Each F In Fij

                Gastos = New ClsGastos
                Gastos.Código = F.id_gfn
                Gastos.Descripción = F.gfn_des
                Gastos.Monto = F.gfn_mto
                Gastos.Tipo = "F"
                Gastos.AfectoExento = "N"

                GastosNeg.Add(Gastos)
                gastofijo.Add(Gastos)

                GastosExento += Gastos.Monto
                txt_tot_gto_fij.Text = CDbl(Val(Me.txt_tot_gto_fij.Text)) + CDbl(Gastos.Monto)

            Next

            txt_tot_gto_def.Text = GastosAfecto + GastosExento

            'Lo cambiamos al factor de cambio
            GastosExento = GastosExento / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val
            GastosAfecto = GastosAfecto / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val
            txt_tot_gto_fij.Text = CDbl(txt_tot_gto_fij.Text) / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val

            Me.Txt_GastosExentos.Text = Format(GastosExento, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.Txt_GastosAfectos.Text = Format(GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.txt_tot_gto_fij.Text = Format(CDbl(txt_tot_gto_fij.Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            LB_TotalGastos.Text = Format(GastosExento + GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

            Session("GastosFijos") = gastofijo
            Session("Gastos") = GastosNeg

            gr_gastofijo.DataSource = gastofijo
            gr_gastofijo.DataBind()

            GV_Gastos.DataSource = GastosNeg
            GV_Gastos.DataBind()

            'le damos formato a los montos
            For I = 0 To GV_Gastos.Rows.Count - 1
                GV_Gastos.Rows(I).Cells(1).Text = Format(CDbl(GV_Gastos.Rows(I).Cells(1).Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

                If GV_Gastos.Rows(I).Cells(2).Text.Trim().ToUpper() = "S" Then
                    GV_Gastos.Rows(I).Cells(2).Text = "SI"
                Else
                    GV_Gastos.Rows(I).Cells(2).Text = "NO"
                End If
            Next

            For i = 0 To gr_gastofijo.Rows.Count - 1
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaGastosPorCliente()

        Try

            Dim Def As Object
            Dim Gastos As ClsGastos
            Dim GastosExento, GastosAfecto As Double

            Def = CG.VerificaOpGastos(CLng(Me.Txt_Rut_Cli.Text))

            GastosNeg = New Collection

            For Each D In Def

                Gastos = New ClsGastos
                Gastos.Código = D.id_gto
                Gastos.Descripción = D.gto_cls.gto_des
                Gastos.Monto = D.gto_cls.gto_mto
                Gastos.Tipo = "D"
                Gastos.AfectoExento = D.gto_cls.gto_iva

                GastosNeg.Add(Gastos)

                Dim cb As CheckBox

                For I = 0 To gd_gastdef.Rows.Count - 1
                    cb = CType(gd_gastdef.Rows(I).FindControl("ch_sel"), CheckBox)
                    If D.id_gto = gd_gastdef.Rows(I).Cells(1).Text Then
                        cb.Checked = True
                        Exit For
                    End If
                Next

                Dim can_deu, can_doc As Integer

                If Txt_CantDeu.Text = "" Or Val(Txt_CantDeu.Text) = 0 Then
                    can_deu = 1
                Else
                    can_deu = Val(Txt_CantDeu.Text)
                End If

                If Txt_CanDocto.Text = "" Or Val(Txt_CanDocto.Text) = 0 Then
                    can_doc = 1
                Else
                    can_doc = Val(Txt_CanDocto.Text)
                End If

                Select Case D.gto_cls.id_p_0036
                    Case 1
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + CDbl(Gastos.Monto)
                        Gastos.Monto = Gastos.Monto
                    Case 2
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + (CDbl(Gastos.Monto) * can_deu)
                        Gastos.Monto = Gastos.Monto * can_deu
                    Case 3
                        'Me.txt_tot_gto_def.Text = CDbl(Val(Me.txt_tot_gto_def.Text)) + (CDbl(Gastos.Monto) * can_doc)
                        Gastos.Monto = Gastos.Monto * can_doc
                End Select

                If Gastos.AfectoExento = "S" Then
                    GastosAfecto += Gastos.Monto
                Else
                    GastosExento += Gastos.Monto
                End If

            Next

            'Lo cambiamos al factor de cambio
            GastosExento = GastosExento / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val
            GastosAfecto = GastosAfecto / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val

            Me.Txt_GastosExentos.Text = Format(GastosExento, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.Txt_GastosAfectos.Text = Format(GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.LB_TotalGastos.Text = Format(GastosExento + GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.txt_tot_gto_def.Text = Format(GastosExento + GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

            'Session("GastosFijos") = gastofijo
            'Session("Gastos") = GastosNeg

            'gr_gastofijo.DataSource = Session("GastosFijos")
            'gr_gastofijo.DataBind()

            Session("Gastos") = GastosNeg

            GV_Gastos.DataSource = GastosNeg
            GV_Gastos.DataBind()

            'le damos formato a los montos
            For I = 0 To GV_Gastos.Rows.Count - 1

                GV_Gastos.Rows(I).Cells(1).Text = Format(CDbl(GV_Gastos.Rows(I).Cells(1).Text) / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val, _
                                                         FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

                If GV_Gastos.Rows(I).Cells(2).Text.Trim().ToUpper() = "S" Then
                    GV_Gastos.Rows(I).Cells(2).Text = "SI"
                Else
                    GV_Gastos.Rows(I).Cells(2).Text = "NO"
                End If

            Next

            'For i = 0 To gr_gastofijo.Rows.Count - 1
            '    gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            'Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDropDownList()

        Try

            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_TipoDocto)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_Moneda)
            CG.ParametrosDevuelve(TablaParametro.EstadoNegociacion, True, DP_EstadoNeg)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_MonedaCom)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, DP_MonComFlat)
            CG.ParametrosDevuelve(TablaParametro.TipoEgreso, True, DP_FormaPago)
            CG.ParametrosDevuelve(TablaParametro.TipoOperacion, True, DP_TipoOperacion)
            CG.ParametrosDevuelve(TablaParametro.TipoPagare, True, DP_TipoPagare)
            CG.SucursalesDevuelve(CodEje, True, DP_Sucursal)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            CLI = Session("Cliente")

            If IsNothing(CLI) Then
                Msj.Mensaje(Me, Caption, "Cliente No Existe", TipoDeMensaje._Informacion)
                Exit Function
            End If

            Txt_Rut_Cli.Text = RG.FormatoMiles(CDbl(CLI.cli_idc))
            Txt_Dig_Cli.Text = CLI.cli_dig_ito
            RutCli = CDbl(CLI.cli_idc)
            Txt_TipoCliente.Text = CLI.P_0044_cls.pnu_des.Trim

            'Dim ldc As New ldc_cls
            HF_Ldc.Value = CMC.LdcDevuelveObjeto(Replace(Txt_Rut_Cli.Text, ".", ""))

            'HF_Ldc.Value = ldc.id_ldc


            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Cliente.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Cliente.Text = CLI.cli_rso
            End If

            'LB_Cliente.Text = Txt_Rut_Cli.Text & "-" & Txt_Dig_Cli.Text & " " & Txt_Cliente.Text.Trim
            'Accordion1.SelectedIndex = 1
            If Not IsNothing(CLI.cli_spr_ead_col) Then
                Txt_TasaSpread.Text = CLI.cli_spr_ead_col
            Else
                Txt_TasaSpread.Text = 0
            End If
            'Banca
            If Not IsNothing(CLI.PL_000066_cls) Then
                Me.Txt_Banca.Text = CLI.PL_000066_cls.pal_des.Trim
            End If

            'Sucursal
            If Not IsNothing(CLI.suc_cls) Then
                Me.Txt_Sucursal.Text = CLI.suc_cls.suc_cod_ftg & " " & CLI.suc_cls.suc_nom
            End If

            'Ejecutivo
            If Not IsNothing(CLI.eje_cls) Then
                Me.Txt_Ejecutivo.Text = CLI.eje_cls.eje_nom.Trim
            End If

            If CLI.id_P_007 = 1 Then
                HF_LNL.Value = "S"
            Else
                HF_LNL.Value = "N"
            End If

            RB_ModoOpera.ClearSelection()
            RB_ModoOpera.Items.FindByValue(HF_LNL.Value).Selected = True

            Dim Coll As Collection

            DP_TipoTasa.ClearSelection()
            DP_TipoTasa.SelectedValue = CLI.CLI_TIP_TAS

            Select Case DP_TipoTasa.SelectedValue
                Case "F"
                    LB_TasaBase.Text = "Tasa Fija E.A."
                Case "V"
                    LB_TasaBase.Text = "DTF T.A."
            End Select

            Coll = CMC.UltimaTasaAplicadaDevuelve(Txt_Rut_Cli.Text)

            LB_TasaBase.Text &= " " & Format(Coll.Item(1), FMT.FSMCD)
            LB_Spread.Text &= " " & Format(Coll.Item(2), FMT.FSMCD)
            LB_Puntos.Text &= " " & Format(Coll.Item(3), FMT.FSMCD)

            LB_TasaNegocio.Text &= " " & Format(Coll.Item(4), FMT.FSMCD)

            HabilitaDesabilitaCliente(False)


            ClsCli.BancosDevuelvePorCliente(True, DP_BancoCuenta, Nothing, Txt_Rut_Cli.Text.Replace(".", ""))

            'Deja el Rut y Nombre del cliente en la cabecera 
            'LB_Cliente.text = RG.FormatoMiles(Txt_Rut_Cli.text) & "-" & Txt_Dig_Cli.text & "   " & Me.Txt_Cliente.text

            'Cambia a AcordionPanel de Negociaciones Anteriores
            'Accordion1.SelectedIndex = 1

            Return True

        Catch ex As Exception

            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return False

        End Try

    End Function

    Private Sub HabilitaDesabilitaCliente(ByVal Estado As Boolean)

        Txt_Rut_Cli.ReadOnly = Not Estado
        Txt_Dig_Cli.ReadOnly = Not Estado

        If Not Estado Then
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
        Else
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
        End If


    End Sub

    Private Sub LimpiaHojaDeNegocio(ByVal Estado As Boolean)

        Try

            'Marcamos los campos mandatorios
            Txt_PorAnt.CssClass = "clsMandatorio"
            Txt_PorAnt.ReadOnly = False

            Txt_FechaNegociacion.CssClass = "clsMandatorio"
            Txt_FechaNegociacion.ReadOnly = False

            'Hablitamos Monto de Descuento
            Txt_MtoDescuentos.CssClass = "clsTxt"
            Txt_MtoDescuentos.ReadOnly = False

            'Se le asigna el dia de hoy
            Txt_FecIng.Text = RG.FUNFecReg(Date.Now.ToShortDateString)
            Txt_FechaNegociacion.Text = Format(Date.Now, "dd/MM/yyyy")
            CargaDatosDiarios()

            DP_TipoDocto.ClearSelection()
            DP_Moneda.ClearSelection()
            DP_EstadoNeg.ClearSelection()
            DP_Sucursal.ClearSelection()

            DP_TipoDocto.SelectedValue = 0
            DP_Moneda.SelectedValue = 0
            DP_EstadoNeg.SelectedValue = 0
            DP_Sucursal.SelectedValue = 0

            'Habilitamos todos textbox, drop y check que se necesitan para ingresar una negociacion
            HabilitaDeshabilitaDatosOperacion(Estado)
            HabilitaDeshabilitaDatosDocumentos(Estado)
            HabilitaDeshabilitaParametrosOperacion(Estado)
            HabilitaDeshabilitaTasas(Estado)
            HabilitaDeshabilitaPago(Estado)
            HabilitaDeshabilitaPagare(Estado)
            HabilitaDeshabilitaObservaciones(Estado)

            Txt_PorAnt.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaDatosOperacion(ByVal Estado As Boolean)

        Try


            If Not Estado Then

                DP_TipoDocto.CssClass = "clsDisabled"
                Txt_PorAnt.CssClass = "clsDisabled"
                DP_Moneda.CssClass = "clsDisabled"
                Txt_CantDeu.CssClass = "clsDisabled"
                Txt_FecIng.CssClass = "clsDisabled"
                Txt_MtoEva.CssClass = "clsDisabled"
                DP_EstadoNeg.CssClass = "clsDisabled"

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaDatosDocumentos(ByVal Estado As Boolean)

        Try


            DP_TipoDocto.Enabled = Estado
            DP_Sucursal.Enabled = Estado
            DP_Calificacion.Enabled = Estado

            Txt_DiaVto.ReadOnly = Not Estado
            Txt_CanDocto.ReadOnly = Not Estado
            Txt_PlazaDocto.ReadOnly = Not Estado
            Txt_FecVto.ReadOnly = Not Estado

            If Estado Then
                Txt_DiaVto.CssClass = "clsMandatorio"
                Txt_CanDocto.CssClass = "clsMandatorio"
                Txt_PlazaDocto.CssClass = "clsTxt"
                Txt_FecVto.CssClass = "clsMandatorio"
                DP_TipoDocto.CssClass = "clsMandatorio"
                DP_Sucursal.CssClass = "clsMandatorio"
                DP_Calificacion.CssClass = "clsMandatorio"
            Else
                Txt_DiaVto.CssClass = "clsDisabled"
                Txt_CanDocto.CssClass = "clsDisabled"
                Txt_PlazaDocto.CssClass = "clsDisabled"
                Txt_FecVto.CssClass = "clsDisabled"
                DP_TipoDocto.CssClass = "clsDisabled"
                DP_Sucursal.CssClass = "clsDisabled"
                DP_Calificacion.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaParametrosOperacion(ByVal Estado As Boolean)

        Try

            Txt_PorCom.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Minimo.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Maximo.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_PorComFlat.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_PorCom.ReadOnly = Not Estado
            DP_MonedaCom.Enabled = Estado
            Txt_Minimo.ReadOnly = Not Estado
            Txt_Maximo.ReadOnly = Not Estado
            DP_MonComFlat.Enabled = Estado
            Txt_PorComFlat.ReadOnly = Not Estado

            If Estado Then
                Txt_PorCom.CssClass = "clsTxt"
                DP_MonedaCom.CssClass = "clsTxt"
                Txt_Minimo.CssClass = "clsTxt"
                Txt_Maximo.CssClass = "clsTxt"
                DP_MonComFlat.CssClass = "clsTxt"
                Txt_PorComFlat.CssClass = "clsTxt"
            Else
                Txt_PorCom.CssClass = "clsDisabled"
                DP_MonedaCom.CssClass = "clsDisabled"
                Txt_Minimo.CssClass = "clsDisabled"
                Txt_Maximo.CssClass = "clsDisabled"
                DP_MonComFlat.CssClass = "clsDisabled"
                Txt_PorComFlat.CssClass = "clsDisabled"
            End If


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaTasas(ByVal Estado As Boolean)

        Try

            Txt_TasaNegocio.Attributes.Add("Style", "TEXT-ALIGN: right")

            Txt_TasaNegocio.ReadOnly = Not Estado

            If Estado Then
                Txt_TasaNegocio.CssClass = "clsMandatorio"
                Me.DP_mora.Enabled = True
                Me.DP_mora.CssClass = "clsTxt"
            Else
                Txt_TasaNegocio.CssClass = "clsDisabled"
                Me.DP_mora.Enabled = False
                Me.DP_mora.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaPago(ByVal Estado As Boolean)

        Try

            'DP_FormaPago.Enabled = Estado
            'Txt_NroCuenta.ReadOnly = Not Estado
            'DP_BancoCuenta.Enabled = Estado
            CB_Antes14.Enabled = Estado
            DP_TipoOperacion.Enabled = Estado


            If Estado Then
                'DP_FormaPago.CssClass = "clsTxt"
                'Txt_NroCuenta.CssClass = "clsTxt"
                'DP_BancoCuenta.CssClass = "clsTxt"
                DP_TipoOperacion.CssClass = "clsMandatorio"
                DP_TipoOperacion.ClearSelection()
                DP_TipoOperacion.SelectedValue = 2
            Else
                'DP_FormaPago.CssClass = "clsDisabled"
                'Txt_NroCuenta.CssClass = "clsDisabled"
                'DP_BancoCuenta.CssClass = "clsDisabled"
                DP_TipoOperacion.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaPagare(ByVal Estado As Boolean)

        Try

            Txt_MtoPagare.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_MtoImpuesto.Attributes.Add("Style", "TEXT-ALIGN: right")

            DP_TipoPagare.Enabled = Estado
            Txt_FecVctoPagare.ReadOnly = Not Estado
            Txt_MtoPagare.ReadOnly = Not Estado
            'Txt_MtoImpuesto.ReadOnly = Not Estado

            If Estado Then
                DP_TipoPagare.CssClass = "clsTxt"
                Txt_FecVctoPagare.CssClass = "clsTxt"
                Txt_MtoPagare.CssClass = "clsTxt"
                'Txt_MtoImpuesto.CssClass = "clsTxt"
            Else
                DP_TipoPagare.CssClass = "clsDisabled"
                Txt_FecVctoPagare.CssClass = "clsDisabled"
                Txt_MtoPagare.CssClass = "clsDisabled"
                Txt_MtoImpuesto.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDeshabilitaObservaciones(ByVal Estado As Boolean)

        Try

            Txt_InstrucCursar.ReadOnly = Not Estado
            Txt_ExcepCondi.ReadOnly = Not Estado


            If Estado Then
                Txt_InstrucCursar.CssClass = "clsTxt"
                Txt_ExcepCondi.CssClass = "clsTxt"
            Else
                Txt_InstrucCursar.CssClass = "clsDisabled"
                Txt_ExcepCondi.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDatosDiarios()
        Try
            Dim Coll As Collection

            Coll = CMC.DatosDiariosDevuelve(Txt_FechaNegociacion.Text)

            'Txt_UF.Text = Format(Coll.Item(1), FMT.FCMCD4)
            Txt_Dolar.Text = Format(Coll.Item(2), FMT.FCMCD)
            Txt_TMC.Text = Format(Coll.Item(3), FMT.FCMCD)

            If Txt_Puntos.Text = "" Then
                Txt_Puntos.Text = Format(0, FMT.FSMCD)
            End If

            Select Case DP_TipoTasa.SelectedValue
                Case "F"
                    Txt_TasaBase.Text = Format(CDec(Txt_TasaSpread.Text), FMT.FSMCD)
                    Txt_TasaSpread.Text = Format(0, FMT.FSMCD)
                    Txt_TasaNegocio.Text = Format(CDec(Txt_TasaBase.Text), FMT.FSMCD)
                Case "V"
                    Txt_TasaBase.Text = Format(CG.Devolver_DTF(Txt_FechaNegociacion.Text), FMT.FSMCD)
                    Txt_TasaNegocio.Text = Format(CG.Devolver_EA(CDec(Txt_TasaBase.Text), CDec(Txt_TasaSpread.Text)) + CDec(Txt_Puntos.Text), FMT.FSMCD)
            End Select

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDocumentos()

        Try

            Dim Idoctoslegales As IQueryable
            Dim Idoctosoperacion As IQueryable

            Idoctoslegales = CG.DocConComDevuelvePorTipoDocto("D", DP_TipoDocto.SelectedValue, 1)
            Idoctosoperacion = CG.DocConComDevuelvePorTipoDocto("D", DP_TipoDocto.SelectedValue, 2)

            documentos = Session("documentos")

            For Each l In Idoctoslegales
                documentos.Add(l.id)
            Next

            For Each o In Idoctosoperacion
                documentos.Add(o.id)
            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaCondiciones()

        Try

            Me.Gr_ConCom.DataSource = CG.DocConComDevuelvePorTipoDocto("C", DP_TipoDocto.SelectedValue, 0)
            Me.Gr_ConCom.DataBind()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaGastos()

        Try

            Me.gd_gastdef.DataSource = CMC.GastosDefinidosDevuelve(Sucursal)
            Me.gd_gastdef.DataBind()

            For I = 0 To gd_gastdef.Rows.Count - 1

                Dim Monto As Double = gd_gastdef.Rows(I).Cells(3).Text

                gd_gastdef.Rows(I).Cells(3).Text = Format(Monto, FMT.FCMSD)

                If gd_gastdef.Rows(I).Cells(4).Text = "S" Then
                    gd_gastdef.Rows(I).Cells(4).Text = "SI"
                Else
                    gd_gastdef.Rows(I).Cells(4).Text = "NO"
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub AgregaGastos()
        Try

            Dim i As Integer
            Dim ch As New CheckBox
            Dim GastosNegociacion As Double
            Dim GastosExento, GastosAfecto As Double

            GastosNeg = New Collection

            Me.txt_tot_gto_def.Text = 0
            txt_tot_gto_fij.Text = 0

            'Agrega gastos definidos
            For i = 0 To Me.gd_gastdef.Rows.Count - 1

                ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

                If ch.Checked = True Then

                    Dim Gastos As New ClsGastos

                    Gastos.Código = gd_gastdef.Rows(i).Cells(1).Text
                    Gastos.Monto = gd_gastdef.Rows(i).Cells(3).Text
                    Gastos.Descripción = gd_gastdef.Rows(i).Cells(5).Text
                    Gastos.Tipo = "D"
                    Gastos.AfectoExento = IIf(gd_gastdef.Rows(i).Cells(4).Text.Trim.ToUpper() = "SI", "S", "N")
                    Gastos.GastoPor = gd_gastdef.Rows(i).Cells(6).Text

                    GastosNeg.Add(Gastos)


                    Select Case Gastos.GastoPor
                        Case 1
                            GastosNegociacion = Gastos.Monto
                        Case 2 'por deudor
                            GastosNegociacion = Gastos.Monto * Val(Txt_CantDeu.Text)
                        Case 3 'por documento
                            GastosNegociacion = Gastos.Monto * Val(Txt_CanDocto.Text)
                    End Select

                    If Gastos.AfectoExento = "S" Then
                        GastosAfecto += GastosNegociacion
                    Else
                        GastosExento += GastosNegociacion
                    End If

                End If

            Next

            For i = 0 To Me.gr_gastofijo.Rows.Count - 1

                Dim Monto As Double = gr_gastofijo.Rows(i).Cells(3).Text

                gr_gastofijo.Rows(i).Cells(3).Text = Format(Monto, FMT.FCMSD)

                Dim Gasto As New ClsGastos

                'Gasto.Codigo = gd_gastdef.Rows(i).Cells(1).Text
                Gasto.Descripción = UCase(gr_gastofijo.Rows(i).Cells(2).Text)
                Gasto.Monto = gr_gastofijo.Rows(i).Cells(3).Text
                Gasto.Tipo = "F"
                Gasto.AfectoExento = "N"

                GastosNeg.Add(Gasto)

                GastosExento += Gasto.Monto

                txt_tot_gto_fij.Text = CDbl(Val(Me.txt_tot_gto_fij.Text)) + CDbl(Gasto.Monto)

            Next

            Dim TotalGastos As Double
            Dim TotalGastosMoneda As Double

            txt_tot_gto_def.Text = GastosAfecto + GastosExento

            'Lo cambiamos al factor de cambio
            GastosExento = GastosExento / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val
            GastosAfecto = GastosAfecto / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val
            txt_tot_gto_fij.Text = CDbl(txt_tot_gto_fij.Text) / CG.ParidadDevuelve(Me.DP_Moneda.SelectedValue, Me.Txt_FechaNegociacion.Text).par_val

            Me.Txt_GastosExentos.Text = Format(GastosExento, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.Txt_GastosAfectos.Text = Format(GastosAfecto, FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            Me.txt_tot_gto_fij.Text = Format(CDbl(txt_tot_gto_fij.Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))
            LB_TotalGastos.Text = Format(GastosExento + GastosAfecto + CDbl(txt_tot_gto_fij.Text), FC.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

            GV_Gastos.DataSource = GastosNeg
            GV_Gastos.DataBind()

            For i = 0 To GV_Gastos.Rows.Count - 1

                Dim Monto As Double = GV_Gastos.Rows(i).Cells(1).Text

                GV_Gastos.Rows(i).Cells(1).Text = Format(Monto, FMT.FCMSD)

                If GV_Gastos.Rows(i).Cells(2).Text.Trim.ToUpper() = "S" Then
                    GV_Gastos.Rows(i).Cells(2).Text = "SI"
                Else
                    GV_Gastos.Rows(i).Cells(2).Text = "NO"
                End If

            Next

            Session("Gastos") = GastosNeg

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function FechaVcto() As Boolean

        Try

            If Txt_FecVto.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar fecha de vcto. de los documentos", TipoDeMensaje._Informacion)
                Return False
            End If

            If CDate(Txt_FecVto.Text) < CDate(Txt_FechaNegociacion.Text) Then
                Msj.Mensaje(Me, Caption, "Fecha de vencimiento debe ser mayor a la fecha de Ingreso", TipoDeMensaje._Informacion)
                Return False
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Function Validacion() As Boolean
        Try

            'Valida que elija una evaluacion
            If DP_Evaluaciones.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Debe seleccionar una evaluación", TipoDeMensaje._Informacion)
                Return False
            End If

            'Comisión Flat
            If DP_MonComFlat.SelectedValue > 0 Then
                If Txt_PorComFlat.Text = "" Then
                    Msj.Mensaje(Me, Caption, "Ingrese monto comisión flat", TipoDeMensaje._Informacion)
                    Return False
                End If
            End If

            If Val(Txt_PorComFlat.Text) <> 0 And DP_MonComFlat.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Seleccione Moneda Comisión Flat", TipoDeMensaje._Informacion)
                Return False
            End If

            'Comisión 
            If (Val(Txt_Minimo.Text) <> 0 Or Val(Txt_Maximo.Text) <> 0) And DP_MonedaCom.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Seleccione Moneda Comisión", TipoDeMensaje._Informacion)
                Return False
            End If

            'Comisión Minimo
            If Val(Txt_Minimo.Text.Replace(".", "")) > Val(Txt_Maximo.Text.Replace(".", "")) Then
                Msj.Mensaje(Me, Caption, "Minimo de Comisión, no puede ser mayor al valor Maximo de la comisión", TipoDeMensaje._Informacion)
                Return False
            End If

            'Comisión Minimo
            If Val(Txt_Maximo.Text.Replace(".", "")) < Val(Txt_Minimo.Text.Replace(".", "")) Then
                Msj.Mensaje(Me, Caption, "Maximo de Comisión, no puede ser menor al valor Minimo de la comisión", TipoDeMensaje._Informacion)
                Return False
            End If

            'Tasa Negocio
            If Txt_TasaNegocio.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar la Tasa de Negocio", TipoDeMensaje._Informacion)
                Return False
            End If

            If Txt_TasaNegocio.Text <= 0 Then
                Msj.Mensaje(Me, Caption, "Tasa de Negocio debe ser mayor a Cero", TipoDeMensaje._Informacion)
                Return False
            End If

            If CDec(Txt_TasaNegocio.Text) > CDec(Txt_TMC.Text) Then
                Msj.Mensaje(Me, Caption, "Tasa de negocio NO puede ser mayor a la máxima legal", TipoDeMensaje._Informacion)
                Return False
            End If

            'Validación del Tipo de Operación
            If DP_TipoOperacion.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Debe seleccionar Tipo de Operación ", TipoDeMensaje._Informacion)
                Return False
            End If

            'Normal Sin Giro / Sólo Cobranza
            If (DP_TipoOperacion.SelectedValue = 2 Or DP_TipoOperacion.SelectedValue = 3) And DP_FormaPago.SelectedValue <> 0 Then
                Dim TIPO_DES As String
                TIPO_DES = IIf(DP_TipoOperacion.SelectedValue = 2, "Normal Sin Desembolso", "ABONO ANTICIPO")
                Msj.Mensaje(Me, Caption, "Seleccionó Tipo Operación " & TIPO_DES & " NO debe Seleccionar Tipo de Desembolso", TipoDeMensaje._Informacion)
                Return False
            End If

            'Normal Con Giro
            If DP_TipoOperacion.SelectedValue = 1 And DP_FormaPago.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Seleccionó Tipo Operación Normal Con Desembolso debe Seleccionar El Tipo de Desembolso", TipoDeMensaje._Informacion)
                Return False
            End If

            'Valida solo para atributo 002
            If DP_TipoOperacion.SelectedValue = 1 Then 'con giro

                If CG.Parametros_Detalle_Devuelve(TablaParametro.TipoEgreso, Me.DP_FormaPago.SelectedValue).Item(1).pnu_atr_003 = "S" Then

                    If Me.DP_BancoCuenta.SelectedValue = 0 Then
                        Msj.Mensaje(Me, Caption, "Debe seleccionar Banco , para la transferencia", TipoDeMensaje._Informacion)
                        Return False
                    End If

                    If Me.Txt_NroCuenta.Text = "" Then
                        Msj.Mensaje(Me, Caption, "Debe ingresar cuenta corriente , para la transferencia", TipoDeMensaje._Informacion)
                        Return False
                    End If

                End If
            End If

            'Valida que exista Factor de Cambio para la operacion
            If DP_Moneda.SelectedValue > 1 Then
                If CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text).par_val = 0 Then
                    Msj.Mensaje(Me, Caption, "No existe factor de cambio para la moneda y la fecha seleccionada", TipoDeMensaje._Informacion)
                    Return False
                End If
            End If

            'Valida cantidad de documentos
            If Val(Txt_CanDocto.Text) <= 0 Then
                Msj.Mensaje(Me, Caption, "Debe ingresar la cantidad de documentos, debe ser superior a 0", TipoDeMensaje._Informacion)
            End If

            If Txt_ComiFlat.Text = "" Then
                Me.Txt_ComiFlat.Text = 0
            End If

            If Txt_MtoDoctos.Text = "" Then
                Me.Txt_MtoDoctos.Text = 0
            End If

            'Datos de la operacion
            If Me.RB_OpePuntual.SelectedValue = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione Operación Puntual SI o NO", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            If RB_Responsabilidad.SelectedValue = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione Con o Sin Recurso", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            If RBConCuotas.SelectedValue = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione Cuota SI o NO", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            If RB_ModoOpera.SelectedValue = "" Then
                Msj.Mensaje(Page, Caption, "Seleccione Modo Operación Lineal o Exponencial", ClsMensaje.TipoDeMensaje._Exclamacion)
                Return False
            End If

            If Me.DP_Sucursal.SelectedValue = 0 Then
                Msj.Mensaje(Me, Caption, "Debe seleccionar la sucursal de operación", TipoDeMensaje._Informacion)
                Return False
            End If

            If Me.DP_Calificacion.SelectedValue = "0" Then
                Msj.Mensaje(Me, Caption, "Debe seleccionar la calificación que se le asignara a los documentos", TipoDeMensaje._Informacion)
                Return False
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Function

    Private Sub ProcesaNegociacion()

        Try

            Dim PRECIO_COMPRA_neg As Double
            Dim SALDO_PENDIENTE_neg As Double
            Dim SALDO_PAGAR_neg As Double
            Dim MTO_COMISION_neg As Double

            Dim COM_MIN_NEG As Double
            Dim COM_MAX_NEG As Double
            Dim MTO_NEG As Double
            Dim MTO_ANTICIPO As Double
            Dim COM_NEG As Double

            Dim POR_CEN_NEG As Decimal
            Dim DIF_PRECIO_neg As Double
            Dim COM_FLA_NEG_neg As Double
            Dim IVA_SIS As Decimal
            Dim IVA_neg As Double
            Dim MTO_GMF As Double
            Dim TOTAL_GIRAR_neg As Double
            Dim MTO_GTO_EXE As Double
            Dim MTO_GTO_AFE As Double
            Dim MTO_DES_NEG As Double
            Dim FRMT As String

            AgregaGastos()

            If Me.Txt_FecVctoNeg.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese fecha de vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion, , True)
            End If

            FRMT = (RG.DevuelveFormatoMoneda(Me.DP_Moneda.SelectedValue))
            Txt_MtoAnticipo.Text = Txt_MtoEva.Text

            'Calcula la diferencia de Precio
            DiferenciaPrecio()

            'Rescatamos los valores de los textbox
            POR_CEN_NEG = CDbl(Txt_PorAnt.Text.Replace(".", ","))
            MTO_NEG = Format((Me.Txt_MtoAnticipo.Text / POR_CEN_NEG) * 100, FRMT)

            DIF_PRECIO_neg = Txt_DifPrecio.Text
            MTO_ANTICIPO = CDbl(Txt_MtoAnticipo.Text)

            If Txt_PorCom.Text.Trim <> "" Then
                COM_NEG = CDbl(Txt_PorCom.Text.Replace(". ", ","))
            Else
                COM_NEG = 0
            End If

            If Txt_MtoDescuentos.Text.Trim <> "" Then
                MTO_DES_NEG = CDbl(Txt_MtoDescuentos.Text)
            Else
                MTO_DES_NEG = 0
            End If

            If (Txt_CanDocto.Text = "" Or Txt_CanDocto.Text = "0") Then
                Txt_CanDocto.Text = "0"
            End If

            'Rescatamos el valor del IVA a la tabla de sistema
            IVA_SIS = CMC.DatosDeSistemaDevuelve.sis_iva

            MTO_GTO_AFE = CDbl(Txt_GastosAfectos.Text)
            MTO_GTO_EXE = CDbl(Txt_GastosExentos.Text)

            'Rescatamos el factor de cambio de la operacion
            Dim ParidadNeg As par_cls

            If DP_Moneda.SelectedValue > 1 Then
                ParidadNeg = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text)
            Else
                ParidadNeg = New par_cls
                ParidadNeg.par_val = 1
            End If


            '--------------------------------------------------------------------------------------------------------------------------------
            'Calculo de Comision por documento y Comision Flat
            '--------------------------------------------------------------------------------------------------------------------------------

            Dim mto_com_fc As Double

            If (Txt_CanDocto.Text = "" Or Txt_CanDocto.Text = "0") Then
                Txt_CanDocto.Text = "0"
                MTO_COMISION_neg = 0
            Else
                MTO_COMISION_neg = ((MTO_NEG / CInt(Txt_CanDocto.Text)) * (COM_NEG / 100))
            End If

            COM_MIN_NEG = CDbl(IIf(Txt_Minimo.Text = "", 0, Txt_Minimo.Text))
            COM_MAX_NEG = CDbl(IIf(Txt_Maximo.Text = "", 0, Txt_Maximo.Text))

            'Llevamos los minimo y maximo de la comision a la moneda de la operacion
            If DP_Moneda.SelectedValue <> DP_MonedaCom.SelectedValue Then

                Dim MonedaOperacion As Double
                Dim MonedaComision As Double

                If DP_Moneda.SelectedValue = 1 And (DP_MonedaCom.SelectedValue = 2 Or DP_MonedaCom.SelectedValue = 3 Or DP_MonedaCom.SelectedValue = 4) Then

                    MonedaComision = CG.ParidadDevuelve(DP_MonedaCom.SelectedValue, Txt_FechaNegociacion.Text).par_val

                    COM_MIN_NEG = COM_MIN_NEG * MonedaComision
                    COM_MAX_NEG = COM_MAX_NEG * MonedaComision

                ElseIf DP_Moneda.SelectedValue <> 1 And DP_MonedaCom.SelectedValue <> 1 Then

                    MonedaOperacion = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text).par_val
                    MonedaComision = CG.ParidadDevuelve(DP_MonedaCom.SelectedValue, Txt_FechaNegociacion.Text).par_val

                    COM_MIN_NEG = (COM_MIN_NEG * MonedaComision) / MonedaOperacion
                    COM_MAX_NEG = (COM_MAX_NEG * MonedaComision) / MonedaOperacion

                Else

                    MonedaComision = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text).par_val

                    COM_MIN_NEG = COM_MIN_NEG / MonedaComision
                    COM_MAX_NEG = COM_MAX_NEG / MonedaComision

                End If

            End If

            If Me.Txt_PorComFlat.Text = "" Then
                Me.Txt_PorComFlat.Text = 0
                Txt_PorComFlat_MaskedEditExtender.Enabled = False
            End If

            COM_FLA_NEG_neg = CDbl(IIf(Txt_PorComFlat.Text = "", 0, Txt_PorComFlat.Text))

            'si la moneda flat es distinta a la de operacion
            If DP_Moneda.SelectedValue <> DP_MonComFlat.SelectedValue Then
                'el resultado de comision debe estar en la moneda de la operacion por lo que se multiplica por el factor de cambio
                COM_FLA_NEG_neg = (COM_FLA_NEG_neg * CG.ParidadDevuelve(DP_MonComFlat.SelectedValue, Txt_FechaNegociacion.Text).par_val) / ParidadNeg.par_val
            End If

            'Se revisa dentro de que rango cae
            Select Case MTO_COMISION_neg
                Case Is < COM_MIN_NEG
                    MTO_COMISION_neg = COM_MIN_NEG
                Case Is > COM_MAX_NEG
                    MTO_COMISION_neg = COM_MAX_NEG
            End Select

            Hf_com_doc.Value = MTO_COMISION_neg
            mto_com_fc = MTO_COMISION_neg * CInt(Txt_CanDocto.Text)

            Dim valor_gmf As Integer = CG.SistemaDevuelveDatos().sis_can_gmf

            PRECIO_COMPRA_neg = (MTO_NEG - DIF_PRECIO_neg)
            SALDO_PENDIENTE_neg = (MTO_NEG - MTO_ANTICIPO)
            SALDO_PAGAR_neg = (MTO_ANTICIPO - DIF_PRECIO_neg)
            MTO_DES_NEG = MTO_DES_NEG * ParidadNeg.par_val

            IVA_neg = ((mto_com_fc + COM_FLA_NEG_neg + MTO_GTO_AFE) * (IVA_SIS / 100))

            'TOTAL_GIRAR_neg = (SALDO_PAGAR_neg - mto_com_fc - COM_FLA_NEG_neg - IVA_neg - MTO_GTO_AFE - MTO_GTO_EXE - MTO_DES_NEG)
            TOTAL_GIRAR_neg = SALDO_PAGAR_neg - (mto_com_fc + COM_FLA_NEG_neg + IVA_neg + MTO_GTO_AFE + MTO_GTO_EXE + MTO_DES_NEG)

            If RB_GMF.Items(0).Selected = True Then
                MTO_GMF = (valor_gmf * (CDbl(TOTAL_GIRAR_neg) / 1000))
            Else
                MTO_GMF = 0
            End If

            'Le damos formato de la moneda de la operacion a los totales
            Dim FormatoMonedaOperacion As String = ""
            FormatoMonedaOperacion = RG.DevuelveFormatoMoneda(DP_Moneda.SelectedValue)

            Txt_ComiPorDocto.Text = Format(mto_com_fc, FormatoMonedaOperacion)
            Txt_ComiFlat.Text = Format(COM_FLA_NEG_neg, FormatoMonedaOperacion)
            Txt_MtoDoctos.Text = Format(Coll_Eva.Item(DP_Evaluaciones.SelectedIndex).monto_doc, FormatoMonedaOperacion) 'Format((MTO_ANTICIPO / POR_CEN_NEG) * 100, FormatoMonedaOperacion)
            Txt_MtoAnticipo.Text = Format(MTO_ANTICIPO, FormatoMonedaOperacion)
            Txt_DifPrecio.Text = Format(DIF_PRECIO_neg, FormatoMonedaOperacion)
            Txt_PrecioCompra.Text = Format(PRECIO_COMPRA_neg, FormatoMonedaOperacion)
            Txt_SaldoPendiente.Text = Format(SALDO_PENDIENTE_neg, FormatoMonedaOperacion)
            Txt_SaldoPagado.Text = Format(SALDO_PAGAR_neg, FormatoMonedaOperacion)
            Txt_Iva.Text = Format(IVA_neg, FormatoMonedaOperacion)
            Txt_GMF.Text = Format(MTO_GMF, FormatoMonedaOperacion)
            Txt_TotalGirar.Text = Format(TOTAL_GIRAR_neg - MTO_GMF, FormatoMonedaOperacion)

            'Se agrega gastos para que aparesca datos en control de gastos
            Txt_GastosExentos.Text = Format(MTO_GTO_EXE, FormatoMonedaOperacion)
            Txt_GastosAfectos.Text = Format(MTO_GTO_AFE, FormatoMonedaOperacion)
            '--------------------------------------------------------------------------------------------------------------------------------------------------

            Dim MTO_NEG_PES As Double

            MTO_NEG_PES = CDbl(Txt_MtoEva.Text) * ParidadNeg.par_val

            'Nuevo Monto Deuda
            Txt_MtoNuevaDeuda.Text = Format(CDbl(Txt_MtoUtilizado.Text) + MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text) + CDbl(Txt_DeudaPuntual.Text), FMT.FCMSD)

            'Nuevo Saldo Linea
            Txt_NuevoSaldoLinea.Text = Format(CDbl(Txt_SaldoLinea.Text) - (MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text)), FMT.FCMSD)

            'Monto a Provisionar
            Txt_MtoProvi.Text = Format(MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text), FMT.FCMSD)

            If (CDbl(Txt_ComiFlat.Text) > CDbl(Txt_MtoDoctos.Text)) Then
                Msj.Mensaje(Me, Caption, "Comisión flat es mayor a monto de documentos", TipoDeMensaje._Informacion)
            End If

            COMENTARIOS_NEG()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub COMENTARIOS_NEG()
        Try

            Dim TEXTO As Object
            'Excepciones y/o Comentarios
            TEXTO = " "
            If CDbl(Txt_SaldoLinea.Text) = 0 Then
                TEXTO = TEXTO & " - (3) CLIENTE NO TIENE LINEA" & Chr(13)
            Else
                If CDbl(Txt_TotalGirar.Text) > CDbl(Txt_SaldoLinea.Text) Then
                    TEXTO = TEXTO & " - (1) OPERACION SUPERA LINEA" & Chr(13)
                End If
            End If

            Dim LDC As ldc_cls

            If Not IsNothing(Session("LineaCredito")) Then
                LDC = Session("LineaCredito")
                If LDC.id_P_0029 = 2 Then
                    TEXTO = TEXTO & " - (4) LINEA VENCIDA" & Chr(13)
                End If

                If Val(LDC.ldc_mto_ocp) > Val(LDC.ldc_mto_apb) Then
                    TEXTO = TEXTO & " - CLIENTE NO TIENE CUPO DISPONIBLE EN LINEA" & Chr(13)
                End If

                If CDbl(Txt_MtoAnticipo.Text.Trim) > CDbl(LDC.ldc_mto_apb - Val(LDC.ldc_mto_ocp)) Then
                    TEXTO = TEXTO & " - NEGOCIACION SUPERA MONTO DISPONIBLE A UTILIZAR EN LINEA DE FINANCIAMIENTO" & Chr(13)
                End If

            Else
                TEXTO = TEXTO & " - CLIENTE NO TIENE LINEA DE FINANCIAMIENTO" & Chr(13)
            End If

            If DateDiff("d", CDate(Txt_FechaNegociacion.Text), CDate(Txt_FecVctoReal.Text)) > 120 Then
                TEXTO = TEXTO & " - PLAZO DE OPERACION SUPERIOR A 120 DIAS" & Chr(13)
            End If

            Dim APC As Object

            If Not IsNothing(Session("Anticipos")) Then
                APC = Session("Anticipos")
                Dim Porcentaje As Decimal

                For Each P In APC
                    If DP_TipoDocto.SelectedValue = P.id_P_0031 Then
                        Porcentaje = P.apc_pct

                        If Porcentaje > Txt_PorAnt.Text Then
                            TEXTO = TEXTO & " - Porcentaje de anticipo DISMINUIDO por ejecutivo" & Chr(13)
                        End If

                        If Porcentaje < Txt_PorAnt.Text Then
                            TEXTO = TEXTO & " - Porcentaje de anticipo AUMENTADO por ejecutivo" & Chr(13)
                        End If

                        Exit For

                    End If
                Next


            End If


            'Dim S As New sbl_cls
            Dim mto As Double
            Dim coll As New Collection

            If HF_IdSbl.Value <> "" Then

                '***********************************************************************************************************************
                'Advierte si sobrepasa linea de credito deudor/producto
                'ASaldivar 08/11/2010
                '***********************************************************************************************************************
                coll = CMC.SubLineaDevuelveObjeto(HF_Ldc.Value)
                If Not IsNothing(coll) Then
                    If coll.Count > 0 Then

                        For i = 1 To coll.Count


                            If coll.Item(i).sbl_tip = "P" Then 'Producto
                                If coll.Item(i).id_P_0031 = DP_TipoDocto.SelectedValue Then
                                    If Txt_MtoEva.Text > coll.Item(i).sbl_mto Then
                                        TEXTO = TEXTO & " - Linea de producto sobregirada" & Chr(13)
                                    End If
                                End If
                            Else 'Deudor
                                If Txt_MtoEva.Text > coll.Item(i).sbl_mto Then
                                    TEXTO = TEXTO & " - Linea de pagador sobregirada" & Chr(13)
                                End If

                            End If

                        Next

                    End If

                End If

            End If

            'Cometarios
            Txt_ExcepCondi.Text = TEXTO

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, "Error en Observacion. " & ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub DiferenciaPrecio()

        Try

            Dim FECHA_VCTO_AUX As String
            Dim Mto_ant As Double
            Dim TasaNeg As Decimal
            Dim DIF_PRE As Double

            Dim Formulas As New FormulasGenerales

            Txt_MtoAnticipo.Text = Txt_MtoDoctos.Text * (Txt_PorAnt.Text / 100)

            If Txt_FecVto.Text <> "" Then

                FECHA_VCTO_AUX = Txt_FecVctoReal.Text

                If Txt_MtoAnticipo.Text <> "" Then
                    Mto_ant = Txt_MtoAnticipo.Text
                End If

                TasaNeg = CDec(Txt_TasaNegocio.Text.Replace(".", ","))

                DIF_PRE = Formulas.DiferenciaDePrecio(FECHA_VCTO_AUX, _
                                                      Txt_FechaNegociacion.Text, _
                                                      Mto_ant, _
                                                      TasaNeg, _
                                                      Me.DP_mora.SelectedValue, _
                                                      RB_ModoOpera.SelectedValue, _
                                                      CL.ClienteDevuelvePorRut(Txt_Rut_Cli.Text).cli_dia_bas)

                Txt_DifPrecio.Text = Format(DIF_PRE, RG.DevuelveFormatoMoneda(DP_Moneda.SelectedValue))

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Function GrabaGastos() As Boolean

        Try

            If IsNothing(Session("Gastos")) Then
                Return True
            End If

            Dim Gasto As ClsGastos

            GastosNeg = Session("Gastos")

            CMC.Gastos_Elimina(HF_Id_Opn.Value)

            For I = 1 To GastosNeg.Count

                Gasto = GastosNeg.Item(I)

                Select Case Gasto.Tipo
                    Case "F" 'Fijos

                        Dim gfn As New gfn_cls

                        gfn.id_gfn = Nothing
                        gfn.id_opn = HF_Id_Opn.Value
                        gfn.gfn_mto = Gasto.Monto
                        gfn.gfn_des = Gasto.Descripción

                        If CMC.GastosFijosInserta(gfn, Val(HF_Id_Ope.Value)) Then

                        End If

                    Case "D" 'Definidos

                        Dim gdn As New gdn_cls

                        gdn.id_gdn = Nothing
                        gdn.id_opn = HF_Id_Opn.Value
                        gdn.id_gto = Gasto.Código

                        If CMC.GastosDefinidosInserta(gdn, Val(HF_Id_Ope.Value), Val(Txt_CanDocto.Text), Val(Txt_CantDeu.Text)) Then

                        End If

                End Select


            Next

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

#End Region

#Region "TextBox Text Changed"

    Protected Sub Txt_FecVto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_FecVto.TextChanged

        Try

            If Me.DP_Evaluaciones.SelectedValue = 0 Then
                Msj.Mensaje(Me, "Atencion", "Debe tener seleccionada una evaluación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_FecVto.Text = "" Then
                Msj.Mensaje(Me, "Atencion", "Ingrese Fecha Vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Txt_FecVto.Text) Then
                Msj.Mensaje(Me, "Atencion", "Fecha Erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_FecVto.Text = ""
                Txt_FecVctoReal.Text = ""
                Txt_FecVto.Focus()
                Exit Sub

            End If

            If CDate(Txt_FecVto.Text) > "31/12/2100" Then
                Msj.Mensaje(Me, "Atencion", "Fecha Erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_FecVto.Text = ""
                Txt_FecVctoReal.Text = ""
                Txt_FecVto.Focus()
                Exit Sub
            End If

            Dim dias_per As Int16

            If Txt_FecVto.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar fecha de vcto. de los documentos", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            If Txt_DiaVto.Text = "" Then
                Txt_DiaVto.Text = "0"
            End If

            Me.Txt_FecVctoReal.Text = CG.calcula_vcto_real("", _
                                                          CDate(Me.Txt_FecVto.Text).AddDays(Txt_DiaVto.Text), _
                                                          Sucursal, _
                                                          Txt_PlazaDocto.Text, _
                                                          DP_TipoDocto.SelectedValue)

            Txt_FecVctoNeg.Text = Format(CDate(Txt_FecVctoReal.Text), "dd/MM/yyyy")

            'Lista Detalle Documentos
            dias_per = DateDiff("d", CDate(Txt_FechaNegociacion.Text), CDate(Txt_FecVctoNeg.Text))

            If dias_per < 0 Then
                Msj.Mensaje(Me, Caption, "Documento Ya está Vencido a la fecha de Simulación", TipoDeMensaje._Informacion)
            End If

            'Dim TYP As typ_cls

            'TYP = CMC.TasaPlazosDevuelve(DP_Moneda.SelectedValue, dias_per)

            'If IsNothing(TYP) Then
            '    Msj.Mensaje(Me, Caption, "No Existe Tasa Base para cantidad de " & dias_per & " días.", TipoDeMensaje._Informacion)
            '    Txt_TasaBase.Text = 0
            'Else
            '    Txt_TasaBase.Text = TYP.typ_mto
            'End If


            'Dim FormatoMonedaOperacion As String = ""
            'FormatoMonedaOperacion = RG.DevuelveFormatoMoneda(DP_Moneda.SelectedValue)

            ''Tasa Negocio
            'Txt_TasaNegocio.Text = CDec(Txt_TasaBase.Text) + CDec(Txt_TasaSpread.Text) + CDec(Txt_Puntos.Text)

            'If Val(Txt_MtoAnticipo.Text) <> 0 Then
            '    Txt_MtoAnticipo.Text = Format(CDbl(Txt_MtoEva.Text), FormatoMonedaOperacion)
            'End If

            If Val(Me.Txt_DifPrecio.Text) <> 0 Then
                DiferenciaPrecio()
            End If

            ProcesaNegociacion()

            'Dim formato As String
            'formato = RG.DevuelveFormatoMoneda(Me.DP_Moneda.SelectedValue)

            Txt_MtoNegociacion.Text = Format(CDbl(Txt_MtoNegociacion.Text), FMT.FCMSD)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_MtoEva_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_MtoEva.TextChanged

        'Una vez que sale del TextBox, realiza la convercion al tipo de moneda para sumarlo a los totales

        Try

            Dim Paridad As par_cls
            Dim MTO_NEG_PES As Double
            Dim Mto_Descuento As Double
            Dim Mto_Ope_Puntual As Double
            Dim Mto_Evaluado As Double
            Dim Saldo As Double


            Mto_Evaluado = CDbl(Txt_MtoEva.Text)

            If DP_Moneda.SelectedValue > 1 Then
                'si la moneda es distinta a Pesos $
                Paridad = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text)
                MTO_NEG_PES = Mto_Evaluado * Paridad.par_val
            Else
                MTO_NEG_PES = Mto_Evaluado
            End If


            If Not IsNothing(Session("ResumenCliente")) Then

                Dim RSC As rsc_cls

                RSC = Session("ResumenCliente")

                Mto_Descuento = Val(Txt_MtoDescuentos.Text.Replace(".", ""))
                Saldo = Val(Txt_SaldoLinea.Text.Replace(".", ""))
                Mto_Ope_Puntual = Val(Txt_DeudaPuntual.Text.Replace(".", ""))

                Txt_MtoNuevaDeuda.Text = RSC.rsc_mto_ocu + MTO_NEG_PES - Mto_Descuento + Mto_Ope_Puntual
                Txt_MtoNuevaDeuda.Text = Format(Val(Txt_MtoNuevaDeuda.Text), FMT.FCMSD)

                Txt_NuevoSaldoLinea.Text = Saldo - MTO_NEG_PES - Mto_Descuento
                Txt_NuevoSaldoLinea.Text = Format(Val(Txt_NuevoSaldoLinea.Text), FMT.FCMSD)

                Txt_MtoProvi.Text = MTO_NEG_PES - Mto_Descuento
                Txt_MtoProvi.Text = Format(Val(Txt_MtoProvi.Text), FMT.FCMSD)

            End If

            Txt_CanDocto.Focus()

            'Dim dias_per As Int16
            'Dim FechaVencimiento As String

            'If Txt_FecVto.Text = "" Then
            '    Msj.Mensaje(Me,Caption, "Debe Ingresar fecha de vcto. de los documentos", TipoDeMensaje._Informacion)
            '    Exit Sub
            'End If

            'FechaVencimiento = CG.DiaHabilDevuelve(Txt_FecVto.Text)

            'Txt_FecVctoNeg.Text = FechaVencimiento
            'Txt_FecVctoReal.Text = FechaVencimiento


            ''Lista Detalle Documentos
            'dias_per = DateDiff("d", CDate(Txt_FechaNegociacion.Text), CDate(Txt_FecVctoNeg.Text))

            'If dias_per < 0 Then
            '    Msj.Mensaje(Me,Caption, "Documento Ya está Vencido a la fecha de Simulación", TipoDeMensaje._Informacion)
            'End If

            'Dim TYP As typ_cls

            'TYP = CG.TasaPlazosDevuelve(DP_Moneda.SelectedValue, dias_per)

            'If IsNothing(TYP) Then
            '    Msj.Mensaje(Me,Caption, "No Existe Tasa Base para " & dias_per & " días de permanencia en mora para Doctos.", TipoDeMensaje._Informacion)
            '    Txt_TasaBase.Text = 0
            'Else
            '    Txt_TasaBase.Text = TYP.typ_mto
            'End If

            ''Tasa Negocio
            'Txt_TasaNegocio.Text = CDec(Txt_TasaBase.Text) + CDec(Txt_TasaSpread.Text) + CDec(Txt_Puntos.Text)


        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_MtoDescuentos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_MtoDescuentos.TextChanged

        Try

            Dim MTO_NEG_PES As Double

            If DP_Moneda.SelectedValue > 1 Then

                Dim Paridad As par_cls

                Paridad = CG.ParidadDevuelve(DP_Moneda.SelectedValue, Txt_FechaNegociacion.Text)
                MTO_NEG_PES = CDbl(Txt_MtoNegociacion.Text) * Paridad.par_val

            Else
                MTO_NEG_PES = CDbl(Txt_MtoNegociacion.Text)
            End If

            'MTO_DES_NEG = SPR_NEG.text

            'Nuevo Monto Deuda
            Txt_MtoNuevaDeuda.Text = Format(CDbl(Txt_MtoUtilizado.Text) + MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text) + CDbl(Txt_DeudaPuntual.Text), FMT.FCMSD)
            'Call SPR_NEG.SetText(2, 20, Format(MTO_OCU + MTO_NEG_PES - MTO_DES_NEG + MTO_OCU_PUN, "###,###,###0"))

            'Nuevo Saldo Linea
            Txt_NuevoSaldoLinea.Text = Format(CDbl(Txt_SaldoLinea.Text) - (MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text)), FMT.FCMSD)
            'Call SPR_NEG.SetText(2, 16, Format((IIf(IsNull(MTO_SDO), 0, MTO_SDO) - (MTO_NEG_PES - MTO_DES_NEG)), "###,###,###,###0"))

            'Monto a Provisionar
            Txt_MtoProvi.Text = Format(MTO_NEG_PES - CDbl(Txt_MtoDescuentos.Text), FMT.FCMSD)
            'Call SPR_NEG.SetText(2, 22, Format(MTO_NEG_PES - MTO_DES_NEG, "###,###,###,###0"))

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_MtoPagare_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_MtoPagare.TextChanged

        Try


            If Me.Txt_MtoPagare.Text = " _.___.___.___" Then
                Me.Txt_MtoPagare.Text = ""
            End If
            If CDbl(Val(Txt_MtoPagare.Text)) > 0 And DP_TipoPagare.SelectedValue >= 1 And Txt_FechaNegociacion.Text >= "01/01/1900" And Txt_FecVto.Text >= "01/01/1900" Then

                Dim CALCULA_IMPUESTO As Double
                Dim TASA_VIS As String
                Dim TASA_PLA As String
                Dim Imp As tim_cls
                Dim DIF As Double


                Imp = CMC.ImpuestoDevuelve

                If IsNothing(Imp) Then
                    TASA_VIS = 0
                    TASA_PLA = 0
                Else
                    TASA_VIS = Imp.tim_val_vis
                    TASA_PLA = Imp.tim_val_pla
                End If

                If DP_TipoPagare.SelectedValue = 1 Then

                    CALCULA_IMPUESTO = CDbl(Txt_MtoPagare.Text) * (TASA_VIS / 100)

                Else

                    If Txt_FechaNegociacion.Text = "" Then
                        DIF = 0
                    Else
                        DIF = DateDiff("d", Format(Txt_FechaNegociacion.Text, "yyyy/mm/dd"), Txt_FecVto.Text) / 30
                    End If

                    CALCULA_IMPUESTO = CDbl(Txt_MtoPagare.Text) * CDec(DIF) * (TASA_PLA / 100)

                End If

                Txt_MtoImpuesto.Text = Format(CALCULA_IMPUESTO, FMT.FCMSD)

                'CALCULA_IMPUESTO = Format(CALCULA_IMPUESTO, "############0.00")

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_PlazaDocto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_PlazaDocto.TextChanged

        Try

            DiferenciaPrecio()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_MonedaCom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_MonedaCom.SelectedIndexChanged
        Try

            'Dim MtoMin As Double
            'Dim MtoMax As Double

            Txt_Minimo_MaskedEditExtender.Mask = Nothing
            Txt_Maximo_MaskedEditExtender.Mask = Nothing

            'If Txt_Minimo.Text <> "" Then
            '    MtoMin = Txt_Minimo.Text.Replace(".", "").Replace(",", ".")
            'End If

            'If Txt_Maximo.Text <> "" Then
            '    MtoMax = Txt_Maximo.Text.Replace(".", "").Replace(",", ".")
            'End If

            '###,###,##0.0000

            Txt_Minimo_MaskedEditExtender.Enabled = True
            Txt_Maximo_MaskedEditExtender.Enabled = True

            Select Case DP_MonedaCom.SelectedValue
                Case 1

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999,999"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999.9999"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4

                    Txt_Minimo_MaskedEditExtender.Mask = "999,999,999.99"
                    Txt_Maximo_MaskedEditExtender.Mask = "999,999,999.99"
            End Select

            Txt_Minimo.Text = ""
            Txt_Maximo.Text = ""
            Txt_Minimo.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_MonComFlat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_MonComFlat.SelectedIndexChanged
        Try


            'Dim Mto As Double

            'Txt_PorComFlat_MaskedEditExtender.Mask = Nothing

            'If Txt_PorComFlat.Text <> "" Then
            '    Mto = Txt_PorComFlat.Text.Replace(".", "").Replace(",", ".")
            'End If
            Txt_PorComFlat_MaskedEditExtender.Enabled = True

            Select Case DP_MonComFlat.SelectedValue
                Case 1
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999,999"
                Case 2
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999.9999"
                Case 3, 4
                    Txt_PorComFlat_MaskedEditExtender.Mask = "999,999,999.99"
            End Select

            Txt_PorComFlat.Text = ""
            Txt_PorComFlat.Focus()

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub DP_Moneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Moneda.SelectedIndexChanged
        Try


            Dim Mto As Double

            'Txt_MtoEva_MaskedEditExtender.Mask = Nothing
            Dim FormatoMonedaOperacion As String = ""
            FormatoMonedaOperacion = RG.DevuelveFormatoMoneda(DP_Moneda.SelectedValue)
            If Txt_MtoEva.Text <> "" Then

                Mto = Format(CDbl(Txt_MtoEva.Text), FormatoMonedaOperacion) '.Replace(".", "").Replace(",", ".")
                Txt_MtoEva.Text = ""

            End If

            'Select Case DP_Moneda.SelectedValue
            '    Case 1
            '        Txt_MtoEva_MaskedEditExtender.Mask = "999,999,999,999"
            '    Case 2
            '        Txt_MtoEva_MaskedEditExtender.Mask = "999,999,999.9999"
            '    Case 3, 4
            '        Txt_MtoEva_MaskedEditExtender.Mask = "999,999,999.99"
            'End Select

            Txt_MtoEva.Text = Mto
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_TasaNegocio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_TasaNegocio.TextChanged
        Try

            Txt_TasaNegocio.Text = Txt_TasaNegocio.Text.Replace(".", ",")

            Select Case DP_TipoTasa.SelectedValue
                Case "F"
                    Txt_Puntos.Text = Format(CDec(Txt_TasaNegocio.Text) - CDec(Txt_TasaBase.Text), FMT.FCMCD4)
                    'Txt_TasaBase.Text = Format(CDec(Txt_TasaNegocio.Text), FMT.FCMCD4)
                Case "V"
                    Dim dtf As Decimal = CG.Devolver_EA(CDec(Txt_TasaBase.Text), CDec(Txt_TasaSpread.Text))
                    Dim ea As Decimal = CDec(Txt_TasaNegocio.Text)
                    Txt_Puntos.Text = Format(ea - dtf, FMT.FCMCD4)
            End Select

            Txt_TasaNegocio.Text = Format(CDec(Txt_TasaNegocio.Text), FMT.FCMCD4)
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_FechaNegociacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_FechaNegociacion.TextChanged
        CargaDatosDiarios()
    End Sub

    Protected Sub Txt_DiaVto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_DiaVto.TextChanged
        Txt_FecVto_TextChanged(sender, e)
    End Sub

#End Region

End Class

Public Class ClsGastos

    Public Sub New()
        _Código = 0
        _Descripción = ""
        _Monto = 0
        _Tipo = ""
        _GastoPor = 0
    End Sub

    Private _Código As Integer
    Public Property Código() As Integer
        Get
            Return _Código
        End Get
        Set(ByVal value As Integer)
            _Código = value
        End Set
    End Property

    Private _Descripción As String
    Public Property Descripción() As String
        Get
            Return _Descripción
        End Get
        Set(ByVal value As String)
            _Descripción = value
        End Set
    End Property

    Private _Monto As Double
    Public Property Monto() As Double
        Get
            Return _Monto
        End Get
        Set(ByVal value As Double)
            _Monto = value
        End Set
    End Property

    Private _Tipo As Char
    Public Property Tipo() As Char
        Get
            Return _Tipo
        End Get
        Set(ByVal value As Char)
            _Tipo = value
        End Set
    End Property

    Private _AfectoExento As Char
    Public Property AfectoExento() As Char
        Get
            Return _AfectoExento
        End Get
        Set(ByVal value As Char)
            _AfectoExento = value
        End Set
    End Property

    Private _GastoPor As Int16
    Public Property GastoPor() As Int16
        Get
            Return _GastoPor
        End Get
        Set(ByVal value As Int16)
            _GastoPor = value
        End Set
    End Property


End Class


