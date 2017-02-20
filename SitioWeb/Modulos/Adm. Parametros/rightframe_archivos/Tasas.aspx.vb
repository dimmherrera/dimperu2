'Imports FacWebCiti.ClsParametros
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class ClsTasas
    Inherits System.Web.UI.Page
    Dim fmt As New FuncionesGenerales.Variables
#Region "Variables"
    Dim CG As New ConsultasGenerales
    Dim Est As String
    Dim Sw As Integer
    Dim Index As Integer
    Dim AG As New ActualizacionesGenerales
    Dim caption As String = "Tasas"
    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Response.Expires = -1
            If Not Me.IsPostBack Then

                '    EstadoDrop()
                'LOS CHECK APARECEN DESHABILITADOS
                ChkDeshabilitados()
                CargaDrop()

                Panel_Tasa_Base.Visible = False
                Panel_Tasa_Impuesto.Visible = False
                Me.Txt_TB_Desde.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_TB_Hasta.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_TB_Porc_Tasa.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_TI_Porc_Plazo.Attributes.Add("Style", "TEXT-ALIGN: right")
                Me.Txt_TMC_Porc_Tasa.Attributes.Add("Style", "TEXT-ALIGN: right")
                NroPaginacion_TasaBase = 0
                NroPaginacion_TasaImpuesto = 0
                NroPaginacion_TasaMax = 0

            End If
            'If Val(HF_ID_Tmc.Value) = 0 Then
            '    ConfirmButtonExtender1.ConfirmText = "¿Desea Guardar Estos Datos?"
            'Else
            '    ConfirmButtonExtender1.ConfirmText = "¿Desea Modificar Estos Datos?"
            'End If

        Catch ex As Exception

        End Try
    End Sub

   
    Protected Sub Rb_Tas_Max_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Tas_Max.CheckedChanged
        Try

            'Rb_Tas_Max.CssClass = "Cabecera"
            'Rb_Tas_bas.CssClass = ""
            'Rb_Tas_Impto.CssClass = ""


            'If Sw2 = 0 Then
            'Me.Gr_TB.Columns.Item(0).Visible = False
            ' Gr_Tasas_Max_Con.Columns.Item(0).Visible = False
            If Rb_Tas_Max.Checked = True Then
                panelTMC()
            End If
            'TasasMaxConAUX()
            'End If
            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Rb_Tas_bas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Tas_bas.CheckedChanged
        Try
            'Rb_Tas_Max.CssClass = ""
            'Rb_Tas_bas.CssClass = "Cabecera"
            'Rb_Tas_Impto.CssClass = ""

            'If Sw2 = 0 Then

            'End If
            If Rb_Tas_bas.Checked = True Then
                Gr_TB.Visible = True
                GrillaTb()
                PanelTB()
            End If
            'oculta columna de GRIDVIEW
            'Me.Gr_TB.Columns.Item(0).Visible = False

            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = False

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Rb_Tas_Impto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Tas_Impto.CheckedChanged
        Try
            'Rb_Tas_Max.CssClass = ""
            'Rb_Tas_bas.CssClass = ""
            'Rb_Tas_Impto.CssClass = "Cabecera"

            'If Sw2 = 0 Then
            If Rb_Tas_Impto.Checked = True Then

                PanelTI()
            End If
            'GrillaTI()
            'End If
            IB_Nuevo.Enabled = True
            IB_Guardar.Enabled = False
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Rb_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Todos.CheckedChanged
        Try

            Me.Gr_Tasas_Max_Con.Controls.Clear()
            
            If Rb_Todos.Checked = True Then
                Est = ""
                LIMPIA_TB()
                DESHABILITA_TB()

                Gr_TB.Visible = True
                GrillaTb()
            End If


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Rb_Act_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Act.CheckedChanged

        Gr_Tasas_Max_Con.Controls.Clear()
        'Gr_Tasa_impto.Controls.Clear()
        If Rb_Act.Checked = True Then
            LIMPIA_TB()
            DESHABILITA_TB()
            CargaTBActivo()
            Est = "A"

        End If

      
    End Sub

    Protected Sub Rb_Inac_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Inac.CheckedChanged

        Gr_Tasas_Max_Con.Controls.Clear()
        'Gr_Tasa_impto.Controls.Clear()
        If Rb_Inac.Checked = True Then
            CargaTBInactivo()
            LIMPIA_TB()
            DESHABILITA_TB()

          
        End If

      

    End Sub
   
    Protected Sub Gr_Tasas_Max_Con_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Tasas_Max_Con.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Tasas_Max_Con,'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Tasas_Max_Con,'formatable')")
            'e.Row.Attributes.Add("onClick", "SeleccionaFilaTasMaxCon(ctl00_ContentPlaceHolder1_Gr_Tasas_Max_Con, 'clicktable', 'formatable', 'selectable')")
        End If

    End Sub

    Protected Sub Gr_TB_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_TB.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_TB,'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_TB,'formatable')")
            'e.Row.Attributes.Add("onClick", "SeleccionaFilaTasBas(ctl00_ContentPlaceHolder1_Gr_TB, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Gr_Ti_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Ti.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Ti,'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Ti,'formatable')")
            'e.Row.Attributes.Add("onClick", "SeleccionaFilaTasImp(ctl00_ContentPlaceHolder1_Gr_Ti, 'clicktable', 'formatable', 'selectable')")
        End If

    End Sub

    Protected Sub RBTMC_Activo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTMC_Activo.CheckedChanged
        Try
            If RBTMC_Activo.Checked = True Then
                RBTMC_Inactivo.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RBTMC_Inactivo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTMC_Inactivo.CheckedChanged
        Try
            If RBTMC_Inactivo.Checked = True Then
                RBTMC_Activo.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_TMC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_TMC.Click
        Try
            For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1
                Gr_Tasas_Max_Con.Rows(i).CssClass = "formatable"
                If HF_ID_Tmc.Value > 0 And HF_Po.Value >= 0 Then
                    Gr_Tasas_Max_Con.Rows(HF_Po.Value - 1).CssClass = "clicktable"
                End If
            Next
            If Gr_Tasas_Max_Con.Rows.Count > 0 Then
                RBTMC_Activo.Checked = False
                RBTMC_Inactivo.Checked = False
                Txt_TMC_Fecha.Text = Gr_Tasas_Max_Con.Rows(HF_Po.Value - 1).Cells(1).Text
                Txt_TMC_Porc_Tasa.Text = Gr_Tasas_Max_Con.Rows(HF_Po.Value - 1).Cells(2).Text
                Txt_TMC_Porc_Tasa.CssClass = "clsMandatorio"
                Txt_TMC_Porc_Tasa.ReadOnly = False
                Txt_TMC_Porc_Tasa.Enabled = True
                If Gr_Tasas_Max_Con.Rows(HF_Po.Value - 1).Cells(3).Text = "ACTIVO" Then
                    RBTMC_Activo.Checked = True
                Else
                    RBTMC_Inactivo.Checked = True
                End If
                RBTMC_Activo.Enabled = True
                RBTMC_Inactivo.Enabled = True
                IB_Guardar.Enabled = True
            End If
          
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_TB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_TB.Click
        Try
            RBTB_Activo.Checked = False
            RBTB_Inactivo.Checked = False
            For i = 0 To Gr_TB.Rows.Count - 1
                Gr_TB.Rows(i).CssClass = "formatable"
                If HF_Po.Value >= 0 And HF_ID_Tmc.Value > 0 Then
                    Gr_TB.Rows(HF_Po.Value - 1).CssClass = "clicktable"
                End If
            Next
            If Gr_TB.Rows.Count >= 0 Then
                RBTB_Activo.Checked = False
                RBTB_Inactivo.Checked = False
                'Txt_TB_Fecha.Text = Gr_TB.Rows(HF_Po.Value).Cells(2).Text
                Txt_TB_Porc_Tasa.Text = Gr_TB.Rows(HF_Po.Value - 1).Cells(3).Text
                Txt_TB_Desde.Text = Gr_TB.Rows(HF_Po.Value - 1).Cells(4).Text
                Txt_TB_Hasta.Text = Gr_TB.Rows(HF_Po.Value - 1).Cells(5).Text
                Txt_TB_Descrip.Text = Gr_TB.Rows(HF_Po.Value - 1).Cells(6).Text
                If Gr_TB.Rows(HF_Po.Value).Cells(7).Text = "ACTIVO" Then
                    RBTB_Activo.Checked = True
                Else
                    RBTB_Inactivo.Checked = True
                End If

                Dim LB As Label
                LB = Me.Gr_TB.Rows(HF_Po.Value - 1).FindControl("Lb_moneda")

                DP_TB_TipoMoneda.SelectedValue = LB.Text

                'If Gr_TB.Rows(HF_Po.Value - 1).Cells(1).Text = "PESO" Then

                'ElseIf Gr_TB.Rows(HF_Po.Value - 1).Cells(1).Text = "UF - UF" Then
                '    DP_TB_TipoMoneda.SelectedValue = 2
                'ElseIf Gr_TB.Rows(HF_Po.Value - 1).Cells(1).Text = "US$ - DOLAR" Then
                '    DP_TB_TipoMoneda.SelectedValue = 3
                'ElseIf Gr_TB.Rows(HF_Po.Value - 1).Cells(1).Text = "EURO" Then
                '    DP_TB_TipoMoneda.SelectedValue = 4
                'End If
                Txt_TB_Fecha.Text = Date.Now
                'Txt_TB_Fecha.ReadOnly
                'HABILITA_TB()
                DP_TB_TipoMoneda.Enabled = False
                DP_TB_TipoMoneda.CssClass = "clsDisabled"
                Txt_TB_Porc_Tasa.CssClass = "clsMandatorio"
                Txt_TB_Desde.CssClass = "clsMandatorio"
                Txt_TB_Hasta.CssClass = "clsMandatorio"
                Txt_TB_Descrip.CssClass = "clsMandatorio"
                Txt_TB_Porc_Tasa.ReadOnly = False
                Txt_TB_Desde.ReadOnly = False
                Txt_TB_Hasta.ReadOnly = False
                Txt_TB_Descrip.ReadOnly = False
                Txt_TB_Descrip.Enabled = True
                RBTB_Activo.Enabled = True
                RBTB_Inactivo.Enabled = True
                IB_Guardar.Enabled = True

            End If
         
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_TI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_TI.Click
        Try
            RBTI_Activo.Checked = False
            RBTI_Inactivo.Checked = False
            HABILITA_TI()
            For i = 0 To Gr_Ti.Rows.Count - 1
                Gr_Ti.Rows(i).CssClass = "formatable"
                If HF_Po.Value >= 0 And HF_ID_Tmc.Value > 0 Then
                    Gr_Ti.Rows(HF_Po.Value - 1).CssClass = "clicktable"
                End If
            Next
            If Gr_Ti.Rows.Count > 0 Then
                Txt_TI_Fecha.Text = Gr_Ti.Rows(HF_Po.Value - 1).Cells(1).Text
                Txt_TI_Porc_Plazo.Text = Gr_Ti.Rows(HF_Po.Value - 1).Cells(2).Text
                Txt_TI_Porc_Vista.Text = Gr_Ti.Rows(HF_Po.Value - 1).Cells(3).Text

                If Gr_Ti.Rows(HF_Po.Value - 1).Cells(4).Text = "ACTIVO" Then
                    RBTI_Activo.Checked = True
                ElseIf Gr_Ti.Rows(HF_Po.Value - 1).Cells(4).Text = "INACTIVO" Then
                    RBTI_Inactivo.Checked = True
                End If
                Txt_TI_Fecha.ReadOnly = True
                IB_Guardar.Enabled = True

                'Txt_TI_Porc_Plazo.CssClass = "clsMandatorio"
                'Txt_TI_Porc_Vista.CssClass = "clsMandatorio"
                'Txt_TI_Porc_Plazo.ReadOnly = False
                'Txt_TI_Porc_Vista.ReadOnly = False
                'RBTI_Activo.Enabled = True
                'RBTI_Inactivo.Enabled = True
                'IB_Guardar.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RBTB_Activo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTB_Activo.CheckedChanged
        Try
            If RBTB_Activo.Checked = True Then
                RBTB_Inactivo.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RBTB_Inactivo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTB_Inactivo.CheckedChanged
        Try
            If RBTB_Inactivo.Checked = True Then
                RBTB_Activo.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RBTI_Activo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTI_Activo.CheckedChanged
        Try
            If RBTI_Activo.Checked = True Then
                RBTI_Inactivo.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RBTI_Inactivo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBTI_Inactivo.CheckedChanged
        Try
            If RBTI_Inactivo.Checked = True Then
                RBTI_Activo.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            Cheked()
            Select Case Index
                Case 1
                    'Tasa Maxima Convencional
                    Dim EstTMC As String


                    If RBTMC_Activo.Checked = True Then
                        EstTMC = "A"
                    ElseIf RBTMC_Inactivo.Checked = True Then
                        EstTMC = "I"
                    End If

                    Sw = 1

                    If HF_ID_Tmc.Value = "" Then

                        For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1
                            Txt_TMC_Fecha.Text = CDate(Txt_TMC_Fecha.Text).ToShortDateString
                            If Gr_Tasas_Max_Con.Rows(i).Cells(1).Text = Txt_TMC_Fecha.Text Then 'Or Gr_Tasas_Max_Con.Rows(i).Cells(3).Text = "ACTIVO" Then
                                Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa para Esta Fecha", TipoDeMensaje._Exclamacion)
                                Exit Sub
                            End If
                        Next

                        Dim tmc As New tmc_cls
                        tmc.id_tmc = Nothing
                        tmc.tmc_est = EstTMC
                        tmc.tmc_fec = CDate(Txt_TMC_Fecha.Text).ToShortDateString
                        tmc.tmc_val = Txt_TMC_Porc_Tasa.Text
                        tmc.tmc_mor = Txt_TML_Mor_Porc.Text
                        AG.Tmcinserta(tmc)
                        GrillaTMC()
                        Msj.Mensaje(Me.Page, caption, "Registro Guardado", TipoDeMensaje._Informacion)

                        LIMPIA_TMC()
                        DESHABILITA_TMC()
                        GrillaTMC()
                    Else

                        AG.TmcModifica(HF_ID_Tmc.Value, Txt_TMC_Porc_Tasa.Text, Txt_TML_Mor_Porc.Text, EstTMC)
                        'Txt_TMC_Porc_Tasa
                        LIMPIA_TMC()
                        DESHABILITA_TMC()
                        GrillaTMC()
                        Msj.Mensaje(Me.Page, caption, "Datos Modificados", TipoDeMensaje._Informacion)

                    End If


                Case 2
                    'Tasa Base

                    Dim Est_BT As String


                    If RBTB_Activo.Checked = True Then
                        Est_BT = "A"
                    Else
                        Est_BT = "I"

                    End If

                    'Dim t As New Collection
                    'Dim llena = CG.DevuelveTasas(DP_TB_TipoMoneda.SelectedValue, Txt_TB_Desde.Text, Txt_TB_Hasta.Text)

                    'For Each l In llena
                    '    t.Add(l)
                    'Next
                    'If t.Count > 0 Then
                    '    Msj.Mensaje(Me.Page, caption, "Período Existe", TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If
                    If Me.Txt_TB_Descrip.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar descripción", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                    Sw = 2

                    If HF_ID_Tmc.Value = "" Then
                        Dim tb As New typ_cls
                        tb.id_typ = Nothing
                        tb.id_P_0023 = DP_TB_TipoMoneda.SelectedValue
                        tb.typ_dde = Txt_TB_Desde.Text
                        tb.typ_des = UCase(Txt_TB_Descrip.Text)
                        tb.typ_est = Est_BT
                        tb.typ_fec = Txt_TB_Fecha.Text()
                        tb.typ_hta = Txt_TB_Hasta.Text
                        tb.typ_mto = Txt_TB_Porc_Tasa.Text
                        tb.typ_spread = Txt_TB_Spr.Text
                        AG.TypInserta(tb)

                        LIMPIA_TB()
                        DESHABILITA_TB()

                        GrillaTb()

                        Msj.Mensaje(Me.Page, caption, "Registro Guardado", TipoDeMensaje._Exclamacion)
                    Else
                        AG.typModifica(HF_ID_Tmc.Value, Txt_TB_Porc_Tasa.Text, Txt_TB_Spr.Text, Txt_TB_Desde.Text, Txt_TB_Hasta.Text, UCase(Txt_TB_Descrip.Text), Est_BT)
                        If Rb_Act.Checked Then
                            CargaTBActivo()
                        End If
                        If Rb_Inac.Checked Then
                            CargaTBInactivo()
                        End If
                        If Rb_Todos.Checked Then
                            GrillaTb()
                        End If
                        'GrillaTb()
                        Msj.Mensaje(Me.Page, caption, "Registro Modificado", TipoDeMensaje._Exclamacion)
                        LIMPIA_TB()
                        DESHABILITA_TB()
                    End If

                Case 3
                    'Tasa Impuesto
                    Dim tim As New tim_cls

                    If RBTI_Activo.Checked Then

                        For i = 0 To Gr_Ti.Rows.Count - 1
                            Txt_TI_Fecha.Text = CDate(Txt_TI_Fecha.Text).ToShortDateString
                            If i <> Val(HF_Tim.Value) Then


                                If Gr_Ti.Rows(i).Cells(1).Text = CDate(Date.Now).ToShortDateString And Gr_Ti.Rows(i).Cells(4).Text = "ACTIVO" Then
                                    Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa para Esta Fecha", TipoDeMensaje._Exclamacion)
                                    Exit Sub
                                End If

                            End If
                        Next

                    End If



                    Dim Est_Ti As String
                    If RBTI_Activo.Checked = True Then
                        Est_Ti = "A"
                    Else
                        Est_Ti = "I"
                    End If
                    Sw = 3

                    If HF_ID_Tmc.Value = "" Then


                        Dim Ti As New tim_cls
                        Ti.id_tim = Nothing
                        Ti.tim_est = Est_Ti
                        Ti.tim_fec = Txt_TI_Fecha.Text
                        Ti.tim_val_pla = Txt_TI_Porc_Plazo.Text
                        Ti.tim_val_vis = Txt_TI_Porc_Vista.Text
                        AG.TimInserta(Ti)
                        LIMPIA_TI()
                        DESHABILITA_TI()
                        GrillaTI()
                        Msj.Mensaje(Me.Page, caption, "Registro Guardado", TipoDeMensaje._Exclamacion)
                    Else
                        If RBTI_Activo.Checked Then

                            For i = 0 To Gr_Ti.Rows.Count - 1
                                Txt_TI_Fecha.Text = CDate(Txt_TI_Fecha.Text).ToShortDateString
                                If Gr_Ti.Rows(i).Cells(4).Text = "ACTIVO" And Gr_Ti.Rows(i).Cells(4).Text = Txt_TI_Fecha.Text Then
                                    Msj.Mensaje(Me.Page, caption, "Ya Existe Tasa Activa", TipoDeMensaje._Exclamacion)
                                    Exit Sub
                                End If
                            Next
                            'Else

                        End If

                        AG.timModifica(HF_ID_Tmc.Value, Txt_TI_Fecha.Text, Txt_TI_Porc_Vista.Text, Txt_TI_Porc_Plazo.Text, Est_Ti)
                        GrillaTI()
                        LIMPIA_TI()
                        DESHABILITA_TI()
                        Msj.Mensaje(Me.Page, caption, "Datos Modificados", TipoDeMensaje._Exclamacion)

                        End If

            End Select
            IB_Guardar.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Function o Sub"


    Sub Cheked()
        If Rb_Tas_Max.Checked = True Then
            Index = 1
            Exit Sub
        End If

        If Rb_Tas_bas.Checked = True Then
            Index = 2
            Exit Sub
        End If

        If Rb_Tas_Impto.Checked = True Then
            Index = 3
            Exit Sub
        End If
    End Sub

    Sub LIMPIA_TMC()
       
        Txt_TMC_Porc_Tasa.Text = ""
        Txt_TML_Mor_Porc.Text = ""
        RBTMC_Activo.Checked = True
        RBTMC_Inactivo.Checked = False
        Txt_TMC_Fecha.ReadOnly = True
        Txt_TMC_Fecha.Text = ""
    End Sub

    Sub HABILITA_TMC()
        Txt_TMC_Fecha.CssClass = "clsMandatorio"
        Txt_TMC_Porc_Tasa.CssClass = "clsMandatorio"
        Txt_TML_Mor_Porc.CssClass = "clsMandatorio"
        Txt_TMC_Porc_Tasa.Text = ""
        Txt_TML_Mor_Porc.Text = ""
        RBTMC_Activo.Checked = True
        RBTMC_Inactivo.Checked = False
        Txt_TMC_Fecha.ReadOnly = False
        Txt_TMC_Porc_Tasa.ReadOnly = False
        Txt_TML_Mor_Porc.ReadOnly = False

        RBTMC_Activo.Enabled = True
        RBTMC_Inactivo.Enabled = True
        Txt_TMC_Fecha.Text = ""
        Txt_TMC_Fecha_CalendarExtender.Enabled = True



    End Sub

    Sub LIMPIA_TB()


        Txt_TB_Fecha.Text = ""
        Txt_TB_Porc_Tasa.Text = ""
        Txt_TB_Spr.Text = ""
        Txt_TB_Desde.Text = ""
        Txt_TB_Hasta.Text = ""
        Txt_TB_Descrip.Text = ""

     
        Txt_TB_Cod.Text = ""
        RBTB_Activo.Checked = True
        RBTB_Inactivo.Checked = False
        RBTB_Activo.Enabled = False
        RBTB_Inactivo.Enabled = False


        DP_TB_TipoMoneda.ClearSelection()
        DP_TB_TipoMoneda.Enabled = False
    End Sub

    Sub DESHABILITA_TB()
        Txt_TB_Fecha.ReadOnly = True
        Txt_TB_Porc_Tasa.ReadOnly = True
        Txt_TB_Spr.ReadOnly = True
        Txt_TB_Desde.ReadOnly = True
        Txt_TB_Hasta.ReadOnly = True
        Txt_TB_Descrip.ReadOnly = True
        Txt_TB_Fecha.CssClass = "clsDisabled"
        Txt_TB_Porc_Tasa.CssClass = "clsDisabled"
        Txt_TB_Spr.CssClass = "clsDisabled"
        Txt_TB_Desde.CssClass = "clsDisabled"
        Txt_TB_Hasta.CssClass = "clsDisabled"
        Txt_TB_Descrip.CssClass = "clsDisabled"
        RBTB_Activo.Enabled = False
        RBTB_Inactivo.Enabled = False
        RBTB_Activo.Enabled = False
        RBTB_Inactivo.Enabled = False
        DP_TB_TipoMoneda.CssClass = "clsDisabled"
        'DP_TB_TipoMoneda.ClearSelection()

    End Sub

    Sub HABILITA_TB()
        Txt_TB_Porc_Tasa.ReadOnly = False
        Txt_TB_Spr.ReadOnly = False
        Txt_TB_Desde.ReadOnly = False
        Txt_TB_Hasta.ReadOnly = False
        Txt_TB_Descrip.ReadOnly = False
        Txt_TB_Fecha.ReadOnly = False
        Txt_TB_Porc_Tasa.CssClass = "clsMandatorio"
        Txt_TB_Spr.CssClass = "clsMandatorio"
        Txt_TB_Desde.CssClass = "clsMandatorio"
        Txt_TB_Hasta.CssClass = "clsMandatorio"
        Txt_TB_Descrip.CssClass = "clsMandatorio"
        Txt_TB_Fecha.CssClass = "clsMandatorio"
        DP_TB_TipoMoneda.CssClass = "clsMandatorio"
        RBTB_Activo.Enabled = True
        RBTB_Inactivo.Enabled = True
        DP_TB_TipoMoneda.Enabled = True


    End Sub
    Sub HABILITA_TI()
        Txt_TI_Fecha.ReadOnly = False
        Txt_TI_Porc_Plazo.ReadOnly = False
        Txt_TI_Porc_Vista.ReadOnly = False
        Txt_TI_Porc_Plazo.CssClass = "clsMandatorio"
        Txt_TI_Porc_Vista.CssClass = "clsMandatorio"
        Txt_TI_Fecha.Text = ""
        Txt_TI_Porc_Plazo.Text = ""
        Txt_TI_Porc_Vista.Text = ""
        RBTI_Activo.Enabled = True
        RBTI_Inactivo.Enabled = True


    End Sub
    Sub DESHABILITA_TI()
        Txt_TI_Fecha.ReadOnly = True
        Txt_TI_Porc_Plazo.ReadOnly = True
        Txt_TI_Porc_Vista.ReadOnly = True
        Txt_TI_Fecha.CssClass = "clsDisabled"
        Txt_TI_Porc_Plazo.CssClass = "clsDisabled"
        Txt_TI_Porc_Vista.CssClass = "clsDisabled"
        Txt_TI_Fecha.Text = ""
        Txt_TI_Porc_Plazo.Text = ""
        Txt_TI_Porc_Vista.Text = ""
        RBTI_Activo.Enabled = False
        RBTI_Inactivo.Enabled = False
    End Sub

    Sub LIMPIA_TI()
        Txt_TI_Fecha.Text = ""
        Txt_TI_Porc_Vista.Text = ""
        Txt_TI_Porc_Plazo.Text = ""
        RBTI_Activo.Checked = True
        RBTI_Inactivo.Checked = False

  

    End Sub

    Sub DESHABILITA_TMC()

        Txt_TMC_Fecha.Text = ""
        Txt_TMC_Porc_Tasa.Text = ""
        Txt_TML_Mor_Porc.Text = ""
        Txt_TMC_Fecha.ReadOnly = True
        Txt_TMC_Porc_Tasa.ReadOnly = True
        Txt_TML_Mor_Porc.ReadOnly = True
        Txt_TMC_Fecha.CssClass = "clsDisabled"
        Txt_TMC_Porc_Tasa.CssClass = "clsDisabled"
        Txt_TML_Mor_Porc.CssClass = "clsDisabled"
        RBTMC_Activo.Enabled = False
        RBTMC_Inactivo.Enabled = False
        Txt_TMC_Fecha_CalendarExtender.Enabled = False
    End Sub

   

    Sub HabilitaBtnNvo()

        Me.IB_Nuevo.Enabled = True
        Me.IB_Guardar.Enabled = True
        Me.IB_Limpiar.Enabled = True
        'Me.IB_Aprobar.Enabled = True

    End Sub

    Sub HabDesBotones()

        If Sw = 1 Then
            'SE DESHABILITAN BOTONES
            Me.IB_Nuevo.Visible = False
            Me.IB_Guardar.Visible = False
            'Me.IB_Limpiar.Visible = False

        End If

        If Sw = 2 Then

            Me.IB_Nuevo.Visible = True
            Me.IB_Guardar.Visible = True
            ' Me.IB_Limpiar.Visible = True

        End If

        If Sw = 3 Then
            Me.IB_Nuevo.Enabled = False
            Me.IB_Guardar.Enabled = False
            'Me.IB_Limpiar.Enabled = False

        End If

    End Sub

    Sub ChkDeshabilitados()
        Rb_Tas_Max.Checked = False
        Rb_Tas_bas.Checked = False
        Rb_Tas_Impto.Checked = False
        IB_Guardar.Enabled = False
    End Sub

    Sub GrillaTMC()
        'HABILITAR Y LLENAR GRILLA TMC
        CG.TmcDevuelve(Gr_Tasas_Max_Con, 15)
        For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1
            If Gr_Tasas_Max_Con.Rows(i).Cells(5).Text = "A" Then
                Gr_Tasas_Max_Con.Rows(i).Cells(5).Text = "ACTIVO"
            ElseIf Gr_Tasas_Max_Con.Rows(i).Cells(5).Text = "I" Then
                Gr_Tasas_Max_Con.Rows(i).Cells(5).Text = "INACTIVO"
            End If
        Next
    End Sub


    Sub GrillaTb()
        CG.TypDevuelveTodo(True, Gr_TB, 11)
        For i = 0 To Gr_TB.Rows.Count - 1
            If Gr_TB.Rows(i).Cells(9).Text = "A" Then
                Gr_TB.Rows(i).Cells(9).Text = "ACTIVO"
            ElseIf Gr_TB.Rows(i).Cells(9).Text = "I" Then
                Gr_TB.Rows(i).Cells(9).Text = "INACTIVO"
            End If
        Next
    End Sub
    Sub CargaTBActivo() 'carga solo estado activo
        CG.TypDevuelveActivo("A", True, Gr_TB)
        For i = 0 To Gr_TB.Rows.Count - 1
            If Gr_TB.Rows(i).Cells(7).Text = "A" Then
                Gr_TB.Rows(i).Cells(7).Text = "ACTIVO"
            End If
        Next
    End Sub
    Sub CargaTBInactivo() 'carga solo estado Inactivo
        CG.TypDevuelveInactivo("I", True, Gr_TB)
        For i = 0 To Gr_TB.Rows.Count - 1
            If Gr_TB.Rows(i).Cells(7).Text = "I" Then
                Gr_TB.Rows(i).Cells(7).Text = "INACTIVO"
            End If
        Next
    End Sub
    Sub GrillaTI()
        'HABILITAR Y LLENAR GRILLA TI hacer funcion para mostrar datos 
        CG.TimDevuelveTodo(True, Gr_Ti, 15)
        For i = 0 To Gr_Ti.Rows.Count - 1
            If Gr_Ti.Rows(i).Cells(5).Text = "A" Then
                Gr_Ti.Rows(i).Cells(5).Text = "ACTIVO"
            ElseIf Gr_Ti.Rows(i).Cells(5).Text = "I" Then
                Gr_Ti.Rows(i).Cells(5).Text = "INACTIVO"
            End If
        Next
    End Sub


    Sub CargaDrop()
        'CG.EstadoDevuelveTodos(True, Dp_TB_Estado)
        'Dp_TB_Estado.Items.Add("Seleccione")
        'Dp_TB_Estado.Items.Add("A")
        'Dp_TB_Estado.Items.Add("I")
        ' Dp_TMC_Estado.Items.Add("Seleccione")
        'Dp_TMC_Estado.Items.Add("A")
        ' Dp_TMC_Estado.Items.Add("I")
        'Dp_TI_Estado.Items.Add("Seleccione")
        'Dp_TI_Estado.Items.Add("A")
        'Dp_TI_Estado.Items.Add("I")


    End Sub
    Sub CargaDropMoneda()
        CG.ParametrosDevuelve(23, True, DP_TB_TipoMoneda)
    End Sub

    Sub panelTMC()

        '****INICIO DE LO QUE ESTA DENTRO DEL PANEL****
        LIMPIA_TMC()
        DESHABILITA_TMC()

        LIMPIA_TB()
        DESHABILITA_TB()
        LIMPIA_TI()
        DESHABILITA_TI()

        Panel_Tasa_Max_Conv.Visible = True
        Txt_TMC_Fecha.ReadOnly = True
        Txt_TMC_Porc_Tasa.ReadOnly = True
        RBTMC_Activo.Checked = True
        RBTMC_Inactivo.Checked = False
        RBTMC_Activo.Enabled = False
        RBTMC_Inactivo.Enabled = False
        '****FIN DE LO QUE ESTA DENTRO DEL PANEL****

        'HABILITAR Y LLENAR GRILLA TMC
        Me.Gr_Tasas_Max_Con.Visible = True
        GrillaTMC()

        Gr_TB.DataSource = Nothing
        Gr_TB.DataBind()

        Gr_Ti.DataSource = Nothing
        Gr_Ti.DataBind()

 
        Me.Panel_Tasa_Base.Visible = False
        Me.Panel_Tasa_Impuesto.Visible = False

    End Sub

    Sub PanelTB()

        '****INICIO DE LO QUE ESTA DENTRO DEL PANEL****
        'LIMPIA_TB()
        LIMPIA_TMC()
        DESHABILITA_TMC()

        LIMPIA_TB()
        DESHABILITA_TB()
        LIMPIA_TI()
        DESHABILITA_TI()
        Me.Panel_Tasa_Base.Visible = True
        'Panel3.Visible = False
        ' Panel1.Visible = True
        'Panel2.Visible = False

        RBTB_Activo.Checked = True
        RBTB_Inactivo.Checked = False
        RBTB_Activo.Enabled = False
        RBTB_Inactivo.Enabled = False



        Txt_TB_Porc_Tasa.ReadOnly = True
        Txt_TB_Desde.ReadOnly = True
        Txt_TB_Hasta.ReadOnly = True
        Txt_TB_Descrip.ReadOnly = True

     
        CargaDropMoneda()

        Rb_Todos.Checked = True
        Rb_Act.Checked = False
        Rb_Inac.Checked = False
        ''****FIN DE LO QUE ESTA DENTRO DEL PANEL****
        'HABILITAR Y LLENAR GRILLA
        Gr_TB.Visible = True
        '  GrillaTb()

        'GRILLAS QUEDAN OCULTOS
        Gr_Tasas_Max_Con.DataSource = Nothing
        Gr_Tasas_Max_Con.DataBind()

        Gr_Ti.DataSource = Nothing
        Gr_Ti.DataBind()
        'PANELES QUEDAN OCULTOS
        Me.Panel_Tasa_Max_Conv.Visible = False
        Me.Panel_Tasa_Impuesto.Visible = False

        'SE HABILITA BOTON NUEVO Y DESHABILITA LOS OTROS BOTONES
        ' HabilitaBtnNvo()
        LIMPIA_TMC()
        LIMPIA_TI()
        LIMPIA_TB()



    End Sub

    Sub PanelTI()

        '****INICIO DE LO QUE ESTA DENTRO DEL PANEL****
        LIMPIA_TI()

        Me.Panel_Tasa_Impuesto.Visible = True
    

        LIMPIA_TMC()
        DESHABILITA_TMC()

        LIMPIA_TB()
        DESHABILITA_TB()
        LIMPIA_TI()
        DESHABILITA_TI()

        Txt_TI_Fecha.ReadOnly = True
        Txt_TI_Porc_Vista.ReadOnly = True
        Txt_TI_Porc_Plazo.ReadOnly = True
     

        RBTI_Activo.Checked = True
        RBTI_Inactivo.Checked = False
        RBTI_Activo.Enabled = False
        RBTI_Inactivo.Enabled = False

        'LLENAR DROP
        'Me.
        GrillaTI()
        Gr_Tasas_Max_Con.DataSource = Nothing
        Gr_Tasas_Max_Con.DataBind()

        Gr_TB.DataSource = Nothing
        Gr_TB.DataBind()
      

        'PANELES QUEDAN OCULTOS
        Me.Panel_Tasa_Max_Conv.Visible = False
        Me.Panel_Tasa_Base.Visible = False

        'SE HABILITA BOTON NUEVO Y DESHABILITA LOS OTROS BOTONES
        ' HabilitaBtnNvo()

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010412, Usr, "PRESIONO NUEVA TASA ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Cheked()
        HF_ID_Tmc.Value = ""
        HF_Po.Value = ""
        IB_Nuevo.Enabled = False

        'If Val(HF_ID_Tmc.Value) = 0 Then
        '    ConfirmButtonExtender1.ConfirmText = "¿Desea Guardar Estos Datos?"
        'Else
        '    ConfirmButtonExtender1.ConfirmText = "¿Desea Modificar Estos Datos?"
        'End If

        Select Case Index
            Case 1

                LIMPIA_TMC()

                HABILITA_TMC()
                'Txt_TMC_Fecha.Text = Date.Now '.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                'Txt_TMC_Fecha.Text = CDate(Txt_TMC_Fecha.Text).ToShortDateString
                Txt_TMC_Fecha.Text = FUNFecReg(Date.Now)
                For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1
                    Gr_Tasas_Max_Con.Rows(i).CssClass = "formatable"
                Next

            Case 2

                LIMPIA_TB()
                HABILITA_TB()
                Txt_TB_Fecha.Text = Date.Now '.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                Txt_TB_Fecha.Text = CDate(Txt_TB_Fecha.Text).ToShortDateString
                Txt_TB_Fecha.ReadOnly = True
                Txt_TB_Fecha.CssClass = "clsDisabled"
                For i = 0 To Gr_TB.Rows.Count - 1
                    Gr_TB.Rows(i).CssClass = "formatable"
                Next

            Case 3

                LIMPIA_TI()
                HABILITA_TI()

                Txt_TI_Fecha.Text = Date.Now ' .Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                Txt_TI_Fecha.Text = CDate(Txt_TI_Fecha.Text).ToShortDateString
                Txt_TI_Fecha.ReadOnly = True
                Txt_TI_Fecha.CssClass = "clsDisabled"
                For i = 0 To Gr_Ti.Rows.Count - 1
                    Gr_Ti.Rows(i).CssClass = "formatable"
                Next
        End Select

        IB_Guardar.Enabled = True
        IB_Limpiar.Enabled = True

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click


        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020412, Usr, "PRESIONO GUARDAR TASA ") Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Cheked()
        Try

            Select Case Index
                Case 1
                    'Tasa Maxima Convencional
                    Dim EstTMC As String

                    If RBTMC_Activo.Checked = False And RBTMC_Inactivo.Checked = False Then
                        Msj.Mensaje(Me.Page, caption, "Seleccione Estado", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If RBTMC_Activo.Checked = True Then
                        EstTMC = "A"
                    ElseIf RBTMC_Inactivo.Checked = True Then
                        EstTMC = "I"
                    End If
                    If Txt_TMC_Porc_Tasa.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Ingrese % de Tasa", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If CInt(Txt_TMC_Porc_Tasa.Text) > 100 Then
                        Msj.Mensaje(Page, caption, "Tasa no puede ser mayor a 100%", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Txt_TML_Mor_Porc.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Ingrese % de Tasa Max. Mora", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If CInt(Txt_TML_Mor_Porc.Text) > 100 Then
                        Msj.Mensaje(Page, caption, "Tasa Mora no puede ser mayor a 100%", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If RBTMC_Activo.Checked = True Then
                        If (CG.TmcDevuelveActiva()) Then
                            Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa Activa", TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If

                        '    If Gr_Tasas_Max_Con.Rows.Count >= 1 Then
                        '        For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1
                        '            If i <> Val(HF_Po.Value) Then

                        '                If Gr_Tasas_Max_Con.Rows(i).Cells(5).Text = "ACTIVO" Then
                        '                    Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa Activa", TipoDeMensaje._Exclamacion)
                        '                    Exit Sub
                        '                End If
                        '            End If
                        '        Next
                        '    End If

                    End If

                    Sw = 1

                Case 2
                    'Tasa Base

                    Dim Est_BT As String

                    If Me.DP_TB_TipoMoneda.SelectedValue = 0 Then
                        Msj.Mensaje(Me.Page, caption, "Debe seleccionar tipo moneda", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If RBTB_Activo.Checked = False And RBTB_Inactivo.Checked = False Then
                        Msj.Mensaje(Me.Page, caption, "Seleccione Estado", TipoDeMensaje._Error)
                        Exit Sub
                    End If

                    If RBTB_Activo.Checked = True Then
                        Est_BT = "A"
                    Else
                        Est_BT = "I"

                    End If
                    If Me.Txt_TB_Fecha.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar fecha", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                    If Me.Txt_TB_Porc_Tasa.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar %tasa", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If CInt(Txt_TB_Porc_Tasa.Text) > 100 Then
                        Msj.Mensaje(Page, caption, "Porcentaje de tasa no puede ser mayor a 100%", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Me.Txt_TB_Spr.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar spread", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If (Me.Txt_TB_Spr.Text) > 100 Then
                        Msj.Mensaje(Page, caption, "Porcentaje de spread no puede ser mayor a 100%", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Me.Txt_TB_Desde.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar Dias desde", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Me.Txt_TB_Hasta.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar Dias hasta", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If CInt(Me.Txt_TB_Desde.Text) > CInt(Me.Txt_TB_Hasta.Text) Then
                        Msj.Mensaje(Me.Page, caption, "Dias desde no pueden ser mayor que dias hasta", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    'Dim t As New Collection
                    'Dim llena = CG.DevuelveTasas(DP_TB_TipoMoneda.SelectedValue, Txt_TB_Desde.Text, Txt_TB_Hasta.Text)

                    'For Each l In llena

                    '    t.Add(l)
                    'Next
                    If HF_Po.Value = "" Then


                        For I = 0 To Me.Gr_TB.Rows.Count - 1

                            If Me.Gr_TB.Rows(I).Cells(1).Text = Me.DP_TB_TipoMoneda.SelectedItem.Text Then

                                If (CDbl(Replace(Me.Txt_TB_Desde.Text, ".", ",")) >= Me.Gr_TB.Rows(I).Cells(4).Text And CDbl(Replace(Me.Txt_TB_Desde.Text, ".", ",")) <= Me.Gr_TB.Rows(I).Cells(5).Text) Or ((CDbl(Replace(Me.Txt_TB_Hasta.Text, ".", ",")) >= Me.Gr_TB.Rows(I).Cells(4).Text And CDbl(Replace(Me.Txt_TB_Hasta.Text, ".", ",")) <= Me.Gr_TB.Rows(I).Cells(5).Text)) Then
                                    Msj.Mensaje(Me, caption, "Ya existe este rango para la tasa  ", TipoDeMensaje._Exclamacion)
                                    Exit Sub
                                End If

                            End If

                        Next

                    End If
                    'If t.Count > 0 Then

                    '    Msj.Mensaje(Me.Page, caption, "Período Existe", TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If
                    If Me.Txt_TB_Descrip.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar descripción", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    Sw = 2


                Case 3
                    'Tasa Impuesto
                    Dim tim As New tim_cls

                    'If RBTI_Activo.Checked Then

                    '    For i = 0 To Gr_Ti.Rows.Count - 1
                    '        If Gr_Ti.Rows(i).Cells(4).Text = "ACTIVO" Then
                    '            Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa Activa", TipoDeMensaje._Exclamacion)

                    '            Exit Sub
                    '        End If
                    '    Next
                    'End If

                    If HF_Po.Value = "" Then

                        For I = 0 To Gr_Ti.Rows.Count - 1
                            ' Txt_TI_Fecha.Text = CDate(Txt_TI_Fecha.Text).ToShortDateString
                            If Gr_Ti.Rows(I).Cells(1).Text = CDate(Date.Now).ToShortDateString Then
                                Msj.Mensaje(Me.Page, caption, "Ya Existe una Tasa para Esta Fecha", TipoDeMensaje._Exclamacion)
                                Exit Sub
                            End If
                        Next
                    End If
                    Dim Est_Ti As String

                    If RBTI_Activo.Checked = False And RBTI_Inactivo.Checked = False Then
                        Msj.Mensaje(Me.Page, caption, "Seleccione Estado", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                    If RBTI_Activo.Checked = True Then
                        Est_Ti = "A"
                    Else
                        Est_Ti = "I"
                    End If

                    If Me.Txt_TI_Porc_Vista.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar %vista", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                    If CInt(Me.Txt_TI_Porc_Vista.Text) > 100 Then
                        Msj.Mensaje(Me.Page, caption, "Porcentaje no debe ser mayor que el 100%", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Me.Txt_TI_Porc_Plazo.Text = "" Then
                        Msj.Mensaje(Me.Page, caption, "Debe ingresar %plazo", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                    If CInt(Me.Txt_TI_Porc_Plazo.Text) > 100 Then
                        Msj.Mensaje(Me.Page, caption, "Porcentage no debe ser mayor que el 100%", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    Sw = 3



            End Select

            If HF_Po.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar este registro?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            Else
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar este registro?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Cheked()

        Me.Panel_Tasa_Max_Conv.Visible = True
        Select Case Index
            Case 1
                'LIMPIA TMC
                LIMPIA_TMC()
                DESHABILITA_TMC()
                ChkDeshabilitados()

                'PANELES QUEDAN OCULTOS
                Me.Panel_Tasa_Max_Conv.Visible = True

                'VACIA GRILLA
                Me.Gr_Tasas_Max_Con.DataSource = Nothing
                Me.Gr_Tasas_Max_Con.DataBind()

                Sw = 3
                HabDesBotones()

            Case 2
                'LIMPIA TB
                LIMPIA_TB()
                DESHABILITA_TB()

                ChkDeshabilitados()

                'PANELES QUEDAN OCULTOS
                Me.Panel_Tasa_Base.Visible = False

                'VACIA GRILLA
                Me.Gr_TB.DataSource = Nothing
                Me.Gr_TB.DataBind()

                Sw = 3
                HabDesBotones()

            Case 3
                'LIMPIA TI
                LIMPIA_TI()
                DESHABILITA_TI()
                ChkDeshabilitados()

                'PANELES QUEDAN OCULTOS
                Me.Panel_Tasa_Impuesto.Visible = False

                'VACIA GRILLA
                'Me.Gr_Tasa_impto.DataSource = Nothing
                'Me.Gr_Tasa_impto.DataBind()

                Sw = 3
                HabDesBotones()

        End Select

    End Sub



#End Region


    Protected Sub IB_Prev_TMC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_TMC.Click
        Try
            If NroPaginacion_TasaMax = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            'NroPaginacion_TasaMax -= 6
            NroPaginacion_TasaMax -= 15
            GrillaTMC()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_TMC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_TMC.Click
        Try
            If Gr_Tasas_Max_Con.Rows.Count < 15 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            'NroPaginacion_TasaMax += 6

            NroPaginacion_TasaMax += 15

            GrillaTMC()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_TB_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_TB.Click
        Try
            If NroPaginacion_TasaBase = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            NroPaginacion_TasaBase -= 11
            GrillaTb()

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_TB_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_TB.Click
        Try

            If Gr_TB.Rows.Count < 11 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            NroPaginacion_TasaBase += 11
            GrillaTb()

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Ti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev_Ti.Click
        Try
            If NroPaginacion_TasaImpuesto = 0 Then
                Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            NroPaginacion_TasaImpuesto -= 15
            GrillaTI()

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Ti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next_Ti.Click
        Try

            If Gr_Ti.Rows.Count < 15 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            NroPaginacion_TasaImpuesto += 15
            GrillaTI()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_ID_Tmc.Value = btn.ToolTip

        For i = 0 To Gr_Tasas_Max_Con.Rows.Count - 1

            If (Gr_Tasas_Max_Con.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Tasas_Max_Con.Rows(i).CssClass = "selectable"
                Else
                    Gr_Tasas_Max_Con.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Tasas_Max_Con.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Tasas_Max_Con.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Tasas_Max_Con.Rows(i).Cells(1).Text = btn.ToolTip) Then

                If (Gr_Tasas_Max_Con.Rows(i).Cells(4).Text = "A") Then
                    RBTMC_Activo.Checked = True
                    RBTMC_Inactivo.Checked = False
                Else
                    RBTMC_Inactivo.Checked = True
                    RBTMC_Activo.Checked = False
                End If
                Txt_TMC_Fecha.Text = Gr_Tasas_Max_Con.Rows(i).Cells(2).Text
                Txt_TMC_Porc_Tasa.Text = Gr_Tasas_Max_Con.Rows(i).Cells(3).Text
                Txt_TML_Mor_Porc.Text = Gr_Tasas_Max_Con.Rows(i).Cells(4).Text
                RBTMC_Activo.Enabled = True
                RBTMC_Inactivo.Enabled = True
                'Txt_TMC_Fecha.ReadOnly = False
                'Txt_TMC_Fecha.CssClass = "clsMandatorio"
                Txt_TMC_Porc_Tasa.ReadOnly = False
                Txt_TMC_Porc_Tasa.CssClass = "clsMandatorio"
                Txt_TML_Mor_Porc.ReadOnly = False
                Txt_TML_Mor_Porc.CssClass = "clsMandatorio"
                'IB_Nuevo.Enabled = False
                IB_Guardar.Enabled = True

            End If
        Next
    End Sub

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_ID_Tmc.Value = btn.ToolTip

        For i = 0 To Gr_TB.Rows.Count - 1

            If (Gr_TB.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_TB.Rows(i).CssClass = "selectable"
                Else
                    Gr_TB.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_TB.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_TB.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_TB.Rows(i).Cells(1).Text = btn.ToolTip) Then
                Txt_TB_Fecha.Text = Gr_TB.Rows(i).Cells(3).Text
                DP_TB_TipoMoneda.SelectedValue = CType(Gr_TB.Rows(i).FindControl("Lb_moneda"), Label).Text
                Txt_TB_Desde.Text = Gr_TB.Rows(i).Cells(6).Text
                Txt_TB_Hasta.Text = Gr_TB.Rows(i).Cells(7).Text

                If (Gr_TB.Rows(i).Cells(9).Text = "ACTIVO") Then
                    RBTB_Activo.Checked = True
                    RBTB_Inactivo.Checked = False
                Else
                    RBTB_Activo.Checked = False
                    RBTB_Inactivo.Checked = True
                End If

                Txt_TB_Descrip.Text = Gr_TB.Rows(i).Cells(8).Text
                Txt_TB_Porc_Tasa.Text = Gr_TB.Rows(i).Cells(4).Text
                Txt_TB_Spr.Text = IIf(Gr_TB.Rows(i).Cells(5).Text = "&nbsp;", "", Gr_TB.Rows(i).Cells(5).Text)
                Txt_TB_Fecha.ReadOnly = False
                Txt_TB_Fecha.CssClass = "clsMandatorio"
                'DP_TB_TipoMoneda.Enabled = True
                'DP_TB_TipoMoneda.CssClass = "clsMandatorio"
                Txt_TB_Desde.ReadOnly = False
                Txt_TB_Desde.CssClass = "clsMandatorio"
                Txt_TB_Hasta.ReadOnly = False
                Txt_TB_Hasta.CssClass = "clsMandatorio"
                RBTB_Activo.Enabled = True
                RBTB_Inactivo.Enabled = True
                Txt_TB_Descrip.ReadOnly = False
                Txt_TB_Descrip.CssClass = "clsMandatorio"
                Txt_TB_Porc_Tasa.ReadOnly = False
                Txt_TB_Porc_Tasa.CssClass = "clsMandatorio"
                Txt_TB_Spr.ReadOnly = False
                Txt_TB_Spr.CssClass = "clsMandatorio"

                IB_Guardar.Enabled = True
                'IB_Nuevo.Enabled = False

            End If

        Next

    End Sub

    Protected Sub Button1_Click2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        HF_ID_Tmc.Value = btn.ToolTip

        For i = 0 To Gr_Ti.Rows.Count - 1
            If (Gr_Ti.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Ti.Rows(i).CssClass = "selectable"
                Else
                    Gr_Ti.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Ti.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Ti.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Ti.Rows(i).Cells(1).Text = btn.ToolTip) Then

                If (Gr_Ti.Rows(i).Cells(5).Text = "A") Then
                    RBTI_Activo.Checked = True
                    RBTI_Inactivo.Checked = False
                Else
                    RBTI_Inactivo.Checked = True
                    RBTI_Activo.Checked = False
                End If

                Txt_TI_Fecha.Text = Gr_Ti.Rows(i).Cells(2).Text
                Txt_TI_Porc_Plazo.Text = Gr_Ti.Rows(i).Cells(3).Text
                Txt_TI_Porc_Vista.Text = Gr_Ti.Rows(i).Cells(4).Text

                RBTI_Activo.Enabled = True
                RBTI_Inactivo.Enabled = True
                Txt_TI_Fecha.ReadOnly = False
                Txt_TI_Fecha.CssClass = "clsMandatorio"
                Txt_TI_Porc_Plazo.ReadOnly = False
                Txt_TI_Porc_Plazo.CssClass = "clsMandatorio"
                Txt_TI_Porc_Vista.ReadOnly = False
                Txt_TI_Porc_Vista.CssClass = "clsMandatorio"

                IB_Guardar.Enabled = True

            End If
        Next
    End Sub
End Class
