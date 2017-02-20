Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_Anticipos
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim Caption As String = "Anticipos"
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Txt_Pro_Val.Attributes.Add("Style", "TEXT-ALIGN: right")
        Response.Expires = -1
        If Not Page.IsPostBack Then

            Dim fc As New FuncionesGenerales.FComunes
            Dim cOLLAS As New Collection

            IB_Guardar.Enabled = True
            NReq.Enabled = False

            fc.SortCollection(cOLLAS, "", True, "")

            CargaDrop()
            CargaDatosAnticipos(1)
            BOTONERA(True, False) 'FY 27-04-2012

        End If

        'IB_Volver.Attributes.Add("onClick", "javascript:CerrarVentana('ctl00$ContentPlaceHolder1$LB_Refrescar');")
        IB_Volver.Attributes.Add("onclick", "window.close();")

    End Sub

    Protected Sub LB_DetalleAPC_Click(ByVal sender As Object, ByVal e As System.EventArgs)



    End Sub

    Protected Sub GV_PorcentajeAnt_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_PorcentajeAnt.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_PorcentajeAnt, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_PorcentajeAnt, 'formatable')")
            'e.Row.Attributes.Add("onClick", "DetalleGV_APC(GV_PorcentajeAnt, 'clicktable', 'formatable', 'selectable');")
        End If
    End Sub

#End Region

#Region "Sub"
    Private Sub MarcaGrilla(ByVal nroapc As Integer)


        Try

            For I = 0 To GV_PorcentajeAnt.Rows.Count - 1
                If (nroapc = GV_PorcentajeAnt.Rows(I).Cells(1).Text) Then
                    If (I Mod 2) = 0 Then
                        GV_PorcentajeAnt.Rows(I).CssClass = "selectable"
                    Else
                        GV_PorcentajeAnt.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_PorcentajeAnt.Rows(I).CssClass = "formatUltcell"

                    Else
                        GV_PorcentajeAnt.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next



        Catch ex As Exception



        End Try





    End Sub

    Private Sub CargaDrop()

        Try

            Dim Drop As New ConsultasGenerales

            Drop.ParametrosDevuelve(TablaParametro.TipoDocumento, True, DP_Pro_Tip)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CargaDatosAnticipos(ByVal Tipo As Int16)

        Try

            If Tipo = 1 Then

                Dim ClsAPC As New ConsultasGenerales
                Dim Sesion As New ClsSession.ClsSession

                ClsAPC.AnticipoDevuelvePorLinea(True, GV_PorcentajeAnt, Sesion.NroLineaCredito, Sesion.NroLineaCredito, 0, 999)

            End If

        Catch ex As Exception
            'Msj.Mensaje(Me,ex.Message, TipoMsg.Errores)
        End Try


    End Sub

    Private Sub LimpiarCampos()
        
        Me.DP_Pro_Tip.ClearSelection()
        Me.Txt_Pro_Val.Text = ""

        Rb_Not.SelectedIndex = 1
        Rb_ver.SelectedIndex = 1
        Rb_Cob.SelectedIndex = 1
    End Sub

    Private Sub CambiaEstilo(ByVal Estilo As String)

        Me.DP_Pro_Tip.CssClass = Estilo
        Me.Txt_Pro_Val.CssClass = Estilo

    End Sub

    Private Sub CambiaEstado(ByVal Estado As Boolean)

        Me.DP_Pro_Tip.Enabled = Estado
        Me.Txt_Pro_Val.Enabled = Estado
        Me.Rb_ver.Enabled = Estado

        Me.Rb_Not.Enabled = Estado

        Me.Rb_Cob.Enabled = Estado


    End Sub

    Private Sub Nuevo()

        Me.DP_Pro_Tip.Enabled = True
        Me.Txt_Pro_Val.ReadOnly = False
        Posicion.Value = ""

        For I = 0 To GV_PorcentajeAnt.Rows.Count - 1
            Me.GV_PorcentajeAnt.Rows(I).CssClass = "formatable"
        Next

    End Sub

    Private Function Valida() As Boolean

        If Me.DP_Pro_Tip.SelectedValue = 0 Then
            Msj.Mensaje(Me, Caption, "Debe Seleccionar un Tipo de Documento", TipoDeMensaje._Exclamacion, "", False)
            Return False
        End If

        If Val(Me.Txt_Pro_Val.Text) > 100 Then
            Msj.Mensaje(Me, Caption, "Porcentaje de Anticipo no puede ser mayor a 100", TipoDeMensaje._Exclamacion, "", False)
            Return False
        End If

        If Me.Txt_Pro_Val.Text = "" Or Me.Txt_Pro_Val.Text = "0" Then

            Msj.Mensaje(Me, Caption, "Debe Ingresar Porcentaje de Anticipo", TipoDeMensaje._Exclamacion, "", False)
            Return False
        End If



        'If Not Me.RB_Ver_Si.Checked And Not Me.RB_Ver_No.Checked Then
        '    Msj.Mensaje(Me, Caption, "Debe Selecionar Verificación", TipoDeMensaje._Exclamacion, "", False)
        '    Return False
        'End If

        'If Not Me.RB_Not_Si.Checked And Not Me.RB_Not_No.Checked Then
        '    Msj.Mensaje(Me, Caption, "Debe Selecionar Notificación", TipoDeMensaje._Exclamacion, "", False)
        '    Return False
        'End If

        'If Not Me.RB_Cob_Si.Checked And Not Me.RB_Cob_No.Checked Then
        '    Msj.Mensaje(Me, Caption, "Debe Selecionar Cobranza", TipoDeMensaje._Exclamacion, "", False)
        '    Return False
        'End If

        Return True

    End Function

#End Region

#Region "Botonera"



    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20050102, Usr, "PRESIONO NUEVO ANTICIPO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        LimpiarCampos()
        CambiaEstilo("clsMandatorio")
        Nuevo()
        CambiaEstado(True)
        apc_num.Value = ""
        BOTONERA(False, True) 'FY 27-04-2012
        'IB_Guardar.Enabled = True

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Guardar.Click ' Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20060102, Usr, "PRESIONO GUARDAR ANTICIPO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Valida() Then
                If apc_num.Value = "" Then
                    For i = 0 To Me.GV_PorcentajeAnt.Rows.Count - 1

                        Dim lb As Label

                        lb = Me.GV_PorcentajeAnt.Rows(i).FindControl("lb_prod")

                        If lb.Text = Me.DP_Pro_Tip.SelectedValue Then
                            Msj.Mensaje(Me, "Atención", "Ya existe un porcentaje para este producto", ClsMensaje.TipoDeMensaje._Exclamacion)
                            Exit Sub

                        End If

                    Next

                End If


                Dim ClsAPC As New ActualizacionesGenerales
                Dim Sesion As New ClsSession.ClsSession
                Dim FMT As New FuncionesGenerales.ClsLocateInfo

                Dim APC As New apc_cls

                APC.id_ldc = Sesion.NroLineaCredito
                APC.id_P_0031 = Me.DP_Pro_Tip.SelectedValue
                APC.apc_pct = CDec(Me.Txt_Pro_Val.Text.Replace(".", ","))
                APC.apc_ver_son = Me.Rb_ver.SelectedValue
                APC.apc_not_son = Me.Rb_Not.SelectedValue
                APC.apc_cob_son = Me.Rb_Cob.SelectedValue

                If apc_num.Value = "" Then

                    APC.id_apc = Nothing

                    If ClsAPC.AnticipoLineaDeCreditoInserta(APC) Then
                        Msj.Mensaje(Me, Caption, "Datos Ingresados", TipoDeMensaje._Informacion, "", False)
                        CargaDatosAnticipos(1)
                    End If

                Else

                    APC.id_apc = apc_num.Value

                    If ClsAPC.AnticipoLineaDeCreditoUpdate(APC) Then
                        Msj.Mensaje(Me, Caption, "Datos Modificados", TipoDeMensaje._Informacion, "", False)
                        CargaDatosAnticipos(1)
                    End If

                End If


                'If ClsAPC.RetornaAPC(LDC.id_ldc, LDC.id_ldc, Me.DP_Pro_Tip.SelectedValue, Me.DP_Pro_Tip.SelectedValue).Count > 0 Then
                '    ClsAPC.UpdateAPC(APC)
                '    Msj.Mensaje(Me,Caption, "Datos Modificados", TipoMsg.Informacion)
                'Else
                '    ClsAPC.NuevoAPC(APC)
                '    Msj.Mensaje(Me,Caption, "Datos Ingresados", TipoMsg.Informacion)
                'End If

                LimpiarCampos()
                CambiaEstilo("clsDisabled")
                CargaDatosAnticipos(1)
                CambiaEstado(False)
                IB_Guardar.Enabled = False
                IB_Nuevo.Enabled = True

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try


    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        'VistaModal(Me.Page, "ctl00$ContentPlaceHolder1$ModalPopupExtender2")
        apc_num.Value = ""
        LimpiarCampos()
        BOTONERA(True, False) 'FY 27-04-2012
        MarcaGrilla(0)
    End Sub

    Protected Sub BOTONERA(ByVal Ib_Nuevo As Boolean, ByVal Ib_Guardar As Boolean)
        Me.IB_Nuevo.Enabled = Ib_Nuevo
        Me.IB_Guardar.Enabled = Ib_Guardar
    End Sub

#End Region

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img_ver As ImageButton = CType(sender, ImageButton)
        Dim Pos As Int16

        For i = 0 To GV_PorcentajeAnt.Rows.Count - 1
            If img_ver.ToolTip = GV_PorcentajeAnt.Rows(i).Cells(1).Text Then
                Pos = i
            End If
        Next

        apc_num.Value = img_ver.ToolTip
        Me.DP_Pro_Tip.ClearSelection()
        Me.DP_Pro_Tip.Items.FindByText(Me.GV_PorcentajeAnt.Rows(Pos).Cells(2).Text).Selected = True

        Me.Txt_Pro_Val.Text = GV_PorcentajeAnt.Rows(Pos).Cells(2).Text

        If GV_PorcentajeAnt.Rows(Pos).Cells(3).Text <> "" Then
            Txt_Pro_Val.Text = GV_PorcentajeAnt.Rows(Pos).Cells(3).Text
        End If

        If GV_PorcentajeAnt.Rows(Pos).Cells(4).Text = "SI" Then
            Rb_ver.SelectedValue = "S"
        ElseIf GV_PorcentajeAnt.Rows(Pos).Cells(4).Text = "NO" Then

            Rb_ver.SelectedValue = "N"
        End If

        If GV_PorcentajeAnt.Rows(Pos).Cells(5).Text = "SI" Then
            Rb_Not.SelectedValue = "S"
        ElseIf GV_PorcentajeAnt.Rows(Pos).Cells(5).Text = "NO" Then
            Rb_Not.SelectedValue = "N"
        End If

        If GV_PorcentajeAnt.Rows(Pos).Cells(6).Text = "SI" Then
            Rb_Cob.SelectedValue = "S"
        ElseIf GV_PorcentajeAnt.Rows(Pos).Cells(6).Text = "NO" Then
            Rb_Cob.SelectedValue = "N"
        End If

        NReq.Enabled = True
        CambiaEstado(True)
        CambiaEstilo("clsMandatorio")
        MarcaGrilla(img_ver.ToolTip)
        IB_Guardar.Enabled = True

    End Sub
End Class
