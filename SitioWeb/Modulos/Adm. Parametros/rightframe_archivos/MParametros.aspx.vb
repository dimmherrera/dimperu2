Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class MParametro
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim index As Integer
    Dim caption As String = "Parámetros"
    Dim msj As New ClsMensaje
    Dim var As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
#End Region

    
#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            If Not IsPostBack Then
                Response.Expires = -1
               
                Modulo = "Mantencion"

                'Esto de abajo es para los skins
                Pagina = Page.AppRelativeVirtualPath

                CambioTema(Page)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Session.Item("valida_sesion") = 1
                'Me.MultiView1.Visible = False
                Drop_TablaAlfa.Enabled = False
                Drop_TablaAlfa.CssClass = "clsDisabled"
                Dt_est.Enabled = False
                Dt_est.CssClass = "clsDisabled"
                txt_Des.CssClass = "clsDisabled"
                txt_Des.Enabled = False
                Dt_par.Enabled = False
                Dt_par.CssClass = "clsDisabled"
                CG.ParametrosDevuelve(23, True, Dp_mon)
                CG.MParametrosDevuelve("N", True, Dd_Tablas)
                CG.MParametrosDevuelve("A", True, Drop_TablaAlfa)
                InhabilitarBoton()
                Txt_min.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_max.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_comi.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_dias.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_diancob.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_codigo.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_RegPla.Attributes.Add("Style", "TEXT-ALIGN: right")
                Txt_prov.Attributes.Add("Style", "TEXT-ALIGN: right")


            End If

            btn_cons.Attributes.Add("onclick", "WinOpen(2,'consulta.aspx','Popup',740,480,200,200);")

            'B_CategoriaRiesgo.Attributes.Add("onclick", "WinOpen(2,'CategoriaRiesgo.aspx','CtrPopUp',550,500,200,200);")


        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_num_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_num.CheckedChanged
        Try

            If Me.Rb_num.Checked = True Then
                Me.Rb_alfa.Checked = False
                Dt_par.Items.Clear()
                Me.Dd_Tablas.Enabled = True
                Me.Dd_Tablas.CssClass = "clsMandatorio"
                txt_Des.CssClass = "clsDisabled"
                'Me.Dd_Tablas0.Enabled = False
                'Me.Dd_Tablas0.CssClass = "clsDisabled"
                Drop_TablaAlfa.Enabled = False
                Drop_TablaAlfa.CssClass = "clsDisabled"

            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_alfa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_alfa.CheckedChanged
        Try
            If Me.Rb_alfa.Checked = True Then
                Me.Rb_num.Checked = False
                Dt_par.Items.Clear()
                Drop_TablaAlfa.Enabled = True
                Drop_TablaAlfa.CssClass = "clsMandatorio"

                ' Me.Dd_Tablas0.Enabled = True
                ' Me.Dd_Tablas0.CssClass = "clsMandatorio"
                Me.Dd_Tablas.Enabled = False
                Me.Dd_Tablas.CssClass = "clsDisabled"
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dd_Tablas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dd_Tablas.SelectedIndexChanged
        Try
            'CG.ParametrosDevuelve(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
            CG.ParametrosDevuelveTodos(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
            Me.MultiView1.ActiveViewIndex = -1

            Me.Txt_codigo.Text = ""

            Dt_par.Enabled = True
            Dt_par.CssClass = "clsMandatorio"
            Dd_Tablas.Enabled = False
            Dd_Tablas.CssClass = "clsDisabled"
            Rb_alfa.Enabled = False
            Rb_num.Enabled = False

            Dt_par.Enabled = True
            Dt_par.CssClass = "clsMandatorio"
            Dt_est.Enabled = True
            Dt_est.CssClass = "clsMandatorio"
            txt_Des.Enabled = True
            txt_Des.CssClass = "clsMandatorio"




            btn_nue.Enabled = True


            btn_nue.Enabled = True

            If Me.Dd_Tablas.SelectedValue = 313 Then
                Label46.Visible = True
                txt_cod_int.Visible = True
                txt_cod_int.Text = ""
            Else
                Label46.Visible = False
                txt_cod_int.Visible = False
                txt_cod_int.Text = ""
            End If

            If Me.Dd_Tablas.SelectedValue = 31 Then
                Me.MultiView1.SetActiveView(View2)
            ElseIf Me.Dd_Tablas.SelectedValue = 41 Then
                Me.MultiView1.SetActiveView(View1)
            ElseIf Dd_Tablas.SelectedValue = 3 Then
                Me.MultiView1.SetActiveView(View3)
            ElseIf Dd_Tablas.SelectedValue = 65 Then
                Me.MultiView1.SetActiveView(View4)
            ElseIf Dd_Tablas.SelectedValue = 63 Then
                Me.MultiView1.SetActiveView(View5)
            ElseIf Dd_Tablas.SelectedValue = 23 Then
                Me.MultiView1.SetActiveView(View6)
            ElseIf Dd_Tablas.SelectedValue = 56 Then
                Me.MultiView1.SetActiveView(View7)
            ElseIf Dd_Tablas.SelectedValue = 54 Then
                Me.MultiView1.SetActiveView(View8)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Drop_TablaAlfa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_TablaAlfa.SelectedIndexChanged
        Try
            CG.ParametrosAlfanumericoDevuelve(Me.Drop_TablaAlfa.SelectedItem.Value, True, Dt_par)

            Dt_par.CssClass = "clsMandatorio"
            Dt_par.Enabled = True

            Drop_TablaAlfa.Enabled = False
            Drop_TablaAlfa.CssClass = "clsDisabled"
            Rb_alfa.Enabled = False
            Rb_num.Enabled = False

            Dt_est.Enabled = True
            Dt_est.CssClass = "clsDisabled"
            txt_Des.Enabled = True
            txt_Des.CssClass = "clsMandatorio"


            Dt_par.Enabled = True
            Dt_par.CssClass = "clsMandatorio"
            Dt_est.Enabled = True
            Dt_est.CssClass = "clsMandatorio"

            'btn_eli.Enabled = True
            btn_nue.Enabled = True
            If Drop_TablaAlfa.SelectedValue = 2 Then
                MultiView1.SetActiveView(View9_Plazas)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dt_par_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dt_par.SelectedIndexChanged
        Try
            If Me.Dt_par.SelectedIndex <> 0 Then
                btn_gua.Enabled = True
                Me.Txt_codigo.Text = Me.Dt_par.SelectedValue

                If Me.Rb_num.Checked = True Then
                    Me.CargaAtr()
                End If
                If Rb_alfa.Checked = True Then
                    CargaAtrAlfa()
                End If

            End If
            btn_eli.Enabled = True

            If Dd_Tablas.SelectedValue = 31 Then
                If Dt_par.SelectedValue <> 0 Then
                    B_CategoriaRiesgo.Visible = True
                End If
            Else
                B_CategoriaRiesgo.Visible = False
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub CheckBox4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ch_ptd.CheckedChanged

    End Sub

    Protected Sub btn_cons_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cons.Click
        Try

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)

        End Try
    End Sub

    Protected Sub Dt_est_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dt_est.SelectedIndexChanged
        Try
            btn_nue.Enabled = True
            btn_gua.Enabled = True
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub btn_nue1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nue.Click
        'Try
        '    If Dd_Tablas.SelectedValue <> 0 Then
        '        Txt_codigo.Enabled = False
        '        txt_Des.Visible = True
        '        txt_Des.CssClass = "clsMandatorio"
        '        Label30.Visible = True

        '        Exit Sub
        '    End If
        '    If Drop_TablaAlfa.SelectedValue <> 0 Then
        '        Txt_codigo.Enabled = True
        '        txt_Des.Visible = True
        '        Txt_codigo.CssClass = "clsMandatorio"
        '        txt_Des.CssClass = "clsMandatorio"
        '        Label30.Visible = True
        '        Exit Sub
        '    End If

        '    LimpiaTodosControles()
        'Catch ex As Exception
        '    msj.mensaje(me,caption, ex.Message, TipoDeMensaje._Error)
        'End Try

    End Sub

    Protected Sub Rb_TiIngreso_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_TiIngreso.CheckedChanged
        Try
            Rb_Plaza.Checked = False
            Label35.Visible = True
            txt_NDR.Visible = True
        Catch ex As Exception
            msj.Mensaje(Me, 2, ex.Message, 2)
        End Try
    End Sub

    Protected Sub Rb_Plaza_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_Plaza.CheckedChanged
        Try
            Rb_TiIngreso.Checked = False
            txt_NDR.Visible = False
            Label35.Visible = False
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub RB_Si_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Si.CheckedChanged
        Try
            If RB_Si.Checked = True Then
                RB_No.Checked = False
            End If
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub RB_No_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_No.CheckedChanged
        Try
            If RB_No.Checked = True Then
                RB_Si.Checked = False
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_tdcto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_tdcto.CheckedChanged
        Try
            Txt_dias.Visible = True
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_pza_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_pza.CheckedChanged
        Try
            Txt_dias.Visible = False
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_ND_Si_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_ND_Si.CheckedChanged
        Try
            If Rb_ND_Si.Checked = True Then
                Rb_ND_No.Checked = False

            End If
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_ND_No_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_ND_No.CheckedChanged
        Try
            If Rb_ND_No.Checked = True Then
                Rb_ND_Si.Checked = False
            End If
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dp_mon_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_mon.TextChanged
        Try
            Select Case Dp_mon.SelectedValue
                Case 1

                    Txt_max.Text = 0
                    Txt_min.Text = 0
                    Txt_comi.Text = 0
                Case 2, 3, 4
                    Txt_max.Text = "0.00"
                    Txt_min.Text = "0.00"
                    Txt_comi.Text = "0.00"
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_max_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_max.TextChanged
        Try
            Select Case Dp_mon.SelectedValue
                Case 1 'Pesos
                    Txt_max.Text = Format(CLng(Txt_max.Text), FMT.FCMSD)
                Case 2 'UF
                    Txt_max.Text = Format(CLng(Txt_max.Text), FMT.FCMCD4)
                Case 3 'Dolar , Uf
                    Txt_max.Text = Format(CLng(Txt_max.Text), FMT.FCMCD)
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_min_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_min.TextChanged
        Try
            Select Case Dp_mon.SelectedValue
                Case 1 'Pesos
                    Txt_min.Text = Format(CLng(Txt_min.Text), FMT.FCMSD)
                Case 2 'UF
                    Txt_min.Text = Format(CLng(Txt_min.Text), FMT.FCMCD4)
                Case 3 'Dolar , Uf
                    Txt_min.Text = Format(CLng(Txt_min.Text), FMT.FCMCD)
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            If Rb_num.Checked = True Then
                If Txt_codigo.Text = "" Then

                    If Dd_Tablas.SelectedValue = 1 Then 'Region
                        Dim r As New P_001_cls
                        r.id_P_001 = Nothing
                        r.pnu_des = UCase(UCase(txt_Des.Text))
                        r.pnu_est = Dt_est.SelectedValue
                        AG.RegInserta(r)
                    End If
                    If Dd_Tablas.SelectedValue = 2 Then 'Comuna-Localidad
                        Dim cl As New P_002_cls
                        cl.id_P_002 = Nothing
                        cl.pnu_des = UCase(UCase(txt_Des.Text))
                        cl.pnu_est = Dt_est.SelectedValue
                        AG.ComLocInserta(cl)
                    End If

                    If Dd_Tablas.SelectedValue = 3 Then 'Estado-Deudor
                        Dim ed As New P_003_cls
                        ed.id_P_003 = Nothing
                        ed.pnu_des = UCase(UCase(txt_Des.Text))
                        ed.pnu_est = Dt_est.SelectedValue
                        ed.pnu_atr_003 = Txt_sig.Text
                        AG.EstDeuInserta(ed)
                    End If
                    If Dd_Tablas.SelectedValue = 5 Then 'Niveles
                        Dim cl As New P_005_cls
                        cl.id_P_005 = Nothing
                        cl.pnu_des = UCase(UCase(txt_Des.Text))
                        cl.pnu_est = Dt_est.SelectedValue
                        AG.NivelesInserta(cl)
                    End If
                    If Dd_Tablas.SelectedValue = 8 Then 'Estado-Cliente
                        Dim ec As New P_008_cls
                        ec.id_P_008 = Nothing
                        ec.pnu_des = UCase(UCase(txt_Des.Text))
                        ec.pnu_est = Dt_est.SelectedValue
                        AG.EstCliInserta(ec)
                    End If

                    If Dd_Tablas.SelectedValue = 7 Then 'Modo-Operacion
                        Dim p As New P_007_cls
                        p.id_P_007 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ModOpeInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 10 Then 'Estado de Poderes
                        Dim p As New P_0010_cls
                        p.id_P_0010 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstPoInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 11 Then 'Estado documento
                        Dim p As New P_0011_cls
                        p.id_P_0011 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstDocInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 12 Then 'Tipo operacion
                        Dim p As New P_0012_cls
                        p.id_P_0012 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiOpeInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 15 Then 'carta tipo
                        Dim p As New P_0015_cls
                        p.id_P_0015 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CarTiInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 17 Then 'Zonas
                        Dim p As New P_0017_cls
                        p.id_P_0017 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ZonInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 18 Then 'forma de pago
                        Dim p As New P_0018_cls
                        p.id_P_0018 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ForPaInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 20 Then 'Sistemas
                        Dim p As New P_0020_cls
                        p.id_P_0020 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.SisInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 21 Then 'tipo pagare
                        Dim p As New P_0021_cls
                        p.id_P_0021 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiPagInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 22 Then 'Estado pagare
                        Dim p As New P_0022_cls
                        p.id_P_0022 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstPagInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 23 Then 'Moneda
                        Dim p As New P_0023_cls
                        p.id_P_0023 = Nothing
                        p.pnu_atr_003 = Txt_ci.Text
                        p.pnu_atr_004 = Txt_cod24.Text
                        p.pnu_atr_005 = Txt_cfogap.Text
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.MonInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 24 Then 'tipo garantia
                        Dim p As New P_0024_cls
                        p.id_P_0024 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipGaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 25 Then 'Regimen Matrimonial
                        Dim p As New P_0025_cls
                        p.id_P_0025 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ReMaInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 26 Then 'tipo aval
                        Dim p As New P_0026_cls
                        p.id_P_0026 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiAvalInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 27 Then 'estado aval
                        Dim p As New P_0027_cls
                        p.id_P_0027 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstAvalInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 28 Then 'estado solicitud en linea
                        Dim p As New P_0028_cls
                        p.id_P_0028 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstSoDeLinInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 29 Then 'estado de linea
                        Dim p As New P_0029_cls
                        p.id_P_0029 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstDeLinInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 30 Then 'estado de operacion
                        Dim p As New P_0030_cls
                        p.id_P_0030 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstDeOpInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 31 Then 'tipo de documento
                        If Rb_tdcto.Checked = True Then
                            If Txt_dias.Text = "" Then
                                msj.Mensaje(Me, caption, "Ingrese cantidad de dias", TipoDeMensaje._Informacion)
                                Exit Sub
                            End If
                            If Not IsNumeric(Txt_dias.Text) Then
                                msj.Mensaje(Me, caption, "Ingrese solo numeros", TipoDeMensaje._Exclamacion)
                                Exit Sub
                            End If
                        End If
                        If Txt_sigla.Text = "" Then
                            msj.Mensaje(Me, caption, "Ingrese sigla", TipoDeMensaje._Exclamacion)
                            Exit Sub
                        End If
                        Dim p As New P_0031_cls
                        p.id_P_0031 = Nothing
                        p.pnu_atr_001 = Txt_dias.Text

                        If Rb_tdcto.Checked = True Then
                            p.pnu_atr_002 = "T"
                        ElseIf Rb_pza.Checked = True Then
                            p.pnu_atr_002 = "P"
                        End If

                        p.pnu_atr_003 = UCase(Txt_sigla.Text)
                        p.pnu_atr_004 = Txt_diancob.Text


                        If Me.Ch_busdh.Checked = True Then
                            p.pnu_atr_005 = "S"
                        Else
                            p.pnu_atr_005 = "N"
                        End If


                        If Ch_diasret.Checked = True Then
                            p.pnu_atr_006 = "S"
                        Else
                            p.pnu_atr_006 = "N"
                        End If

                        p.pnu_atr_007 = Dp_mon.SelectedValue
                        p.pnu_atr_008 = Txt_comi.Text
                        p.pnu_atr_009 = Txt_min.Text
                        p.pnu_atr_010 = Txt_max.Text


                        If Ch_tdctogest.Checked = True Then
                            p.pnu_atr_011 = "N"
                        ElseIf Ch_tdctogest.Checked = False Then
                            p.pnu_atr_011 = "S"
                        End If

                        If Ch_ptd.Checked = True Then
                            p.pnu_atr_012 = "S"

                        End If

                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue

                        AG.TDocInserta(p)

                    End If


                    If Dd_Tablas.SelectedValue = 36 Then 'tipo de gasto operacional
                        Dim p As New P_0036_cls
                        p.id_P_0036 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TGOperaInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 40 Then 'estado de verificacion
                        Dim p As New P_0040_cls
                        p.id_P_0040 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstVeriInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 41 Then 'Tipo cuentas x cobrar
                        Dim p As New p_0041_cls
                        p.id_P_0041 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        If Rb_manual.Checked = True Then
                            p.pnu_atr_003 = "M"
                        End If
                        If Rb_auto.Checked = True Then
                            p.pnu_atr_003 = "A"
                        End If

                        If Rb_cinteresi.Checked = True Then
                            p.pnu_atr_005 = "S"
                        End If

                        If Rb_cinteresNO.Checked = True Then
                            p.pnu_atr_005 = "N"
                        End If
                        If Ch_cobra.Checked = True Then
                            p.pnu_atr_004 = 1
                        ElseIf Ch_paga.Checked = True Then
                            p.pnu_atr_004 = 2
                        ElseIf Ch_cobra.Checked = True And Ch_paga.Checked = True Then
                            p.pnu_atr_004 = 3

                            'If coll.Item(1).pnu_cob_pag = "1" Then
                            '    Ch_cobra.Checked = True
                            'ElseIf coll.Item(1).pnu_cob_pag = "2" Then
                            '    Ch_paga.Checked = True
                            'ElseIf coll.Item(1).pnu_cob_pag = "3" Then
                            '    Ch_paga.Checked = True
                            '    Ch_cobra.Checked = True
                            'ElseIf coll.Item(1).pnu_cob_pag = "" Then
                            '    Ch_paga.Checked = False
                            '    Ch_cobra.Checked = False
                            'End If



                        End If
                        'If Ch_paga.Checked = True Then
                        '    p.pnu_cob_pag = "p"
                        'End If
                        AG.TipoCuentaXCobrarInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 44 Then 'tipo de cliente
                        Dim p As New P_0044_cls
                        p.id_P_0044 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TpCliInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 45 Then 'tipo de ejecutivo
                        Dim p As New P_0045_cls
                        p.id_P_0045 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TpEjeInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 48 Then 'estado de ejecutivo
                        Dim p As New P_0048_cls
                        p.id_P_0048 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstEjeInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 49 Then 'Tipo de Telefono
                        Dim p As New P_0049_cls
                        p.id_P_0049 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiFonoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 51 Then 'Tipo Gasto Recaudacion
                        Dim p As New P_0051_cls
                        p.id_P_0051 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiGasRecInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 52 Then 'Estado Docto de pago
                        Dim p As New P_0052_cls
                        p.id_P_0052 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EsDocPgInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 53 Then 'Que se Paga
                        Dim p As New P_0053_cls
                        p.id_P_0053 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.QsPagaInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 54 Then 'Tipo de Ingreso
                        Dim p As New p_0054_cls
                        p.id_p_0054 = Nothing
                        If Rb_TiIngreso.Checked = True Then
                            p.pnu_atr_002 = "T"
                            p.pnu_atr_001 = txt_NDR.Text
                        ElseIf Rb_Plaza.Checked = True Then
                            p.pnu_atr_002 = "P"
                            txt_NDR.Text = ""
                        End If

                        p.pnu_atr_004 = txt_DLE.Text
                        If RB_Si.Checked = True Then
                            p.pnu_atr_003 = "S"
                        ElseIf RB_No.Checked = True Then
                            p.pnu_atr_003 = "N"
                        End If

                        If Rb_ND_Si.Checked = True Then
                            p.pnu_atr_005 = "S"
                        ElseIf Rb_ND_No.Checked = True Then
                            p.pnu_atr_005 = "N"
                        End If
                        p.pnu_atr_004 = txt_DLE.Text
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipIngrInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 55 Then 'Que a Pagar
                        Dim p As New P_0055_cls
                        p.id_P_0055 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.QueAPagInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 56 Then 'Tipo de Egreso
                        Dim p As New P_0056_cls
                        p.id_P_0056 = Nothing
                        If Rb_dep.Checked = True Then
                            p.pnu_atr_002 = "A"
                        ElseIf Rb_sdep.Checked = True Then
                            p.pnu_atr_002 = "S"
                        ElseIf rb_transelec.Checked = True Then
                            p.pnu_atr_002 = "T"
                        End If
                        If Rb_valsi.Checked = True Then
                            p.pnu_atr_003 = "S"
                        ElseIf Rb_valno.Checked = True Then
                            p.pnu_atr_003 = "N"
                        End If

                        If Rb_indcpag.Checked = True Then
                            p.pnu_atr_004 = "S"
                        ElseIf Rb_noingdcpag.Checked = True Then
                            p.pnu_atr_004 = "N"
                        End If

                        If rb_cargosi.Checked Then
                            p.pnu_atr_005 = "S"
                        ElseIf Rb_cargono.Checked Then
                            p.pnu_atr_005 = "N"
                        End If

                        If Rb_SisA.Checked Then
                            p.pnu_atr_006 = "A"
                        ElseIf Rb_SisB.Checked Then
                            p.pnu_atr_006 = "B"
                        ElseIf Rb_SisW.Checked Then
                            p.pnu_atr_006 = "W"
                        End If

                        If Rb_GMF_S.Checked Then
                            p.pnu_atr_007 = "S"
                        ElseIf Rb_GMF_N.Checked Then
                            p.pnu_atr_007 = "N"
                        End If

                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TiEgreInserta(p)
                    End If




                    If Dd_Tablas.SelectedValue = 58 Then 'Categoría de Riesgo
                        Dim p As New P_0058_cls
                        p.id_P_0058 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CatRiesInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 60 Then 'estado Factura
                        Dim p As New P_0060_cls
                        p.id_P_0060 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstFacInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 61 Then 'motivos de protestos
                        Dim p As New P_0061_cls
                        p.id_P_0061 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.MotProInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 62 Then 'Facultades de Poder
                        Dim p As New P_0062_cls
                        p.id_P_0062 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.FacPodInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 63 Then 'Razones Sociales
                        Dim p As New P_0063_cls
                        p.id_P_0063 = Nothing
                        p.pnu_atr_007 = UCase(txt_razsoc.Text)
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.RazonSocialInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 64 Then 'Actividad Económica
                        Dim p As New P_0064_cls
                        p.id_P_0064 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ActividadEconomicaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 65 Then 'Tipo de Riesgos
                        If Not IsNumeric(Txt_prov.Text) Then

                            Exit Sub
                        End If
                        Dim p As New P_0065_cls
                        p.id_P_0065 = Nothing
                        p.pnu_atr_002 = Txt_prov.Text
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoRiesgoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 67 Then 'Tipo de Envio Información
                        Dim p As New P_0067_cls
                        p.id_P_0067 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoEnvioInfoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 68 Then 'Forma de Envío
                        Dim p As New P_0068_cls
                        p.id_P_0068 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.FormaEnvioInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 68 Then 'Tipo Clisificacion
                        Dim p As New P_0069_cls
                        p.id_P_0069 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoClasificacionInserta(p)
                    End If


                    If Dd_Tablas.SelectedValue = 70 Then 'País
                        Dim p As New P_0070_cls
                        p.id_P_0070 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.PaisInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 71 Then 'Otro
                        Dim p As New P_0071_cls
                        p.id_P_0071 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.OtroInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 72 Then 'Otro 1
                        Dim p As New P_0072_cls
                        p.id_P_0072 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.Otro1Inserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 73 Then 'Otro 2
                        Dim p As New P_0073_cls
                        p.id_P_0073 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.Otro2Inserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 74 Then 'Otro 3
                        Dim p As New P_0074_cls
                        p.id_P_0074 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.Otro3Inserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 75 Then 'Tipo Operación Contable
                        Dim p As New P_0075_cls
                        p.id_P_0075 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoOpeContInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 76 Then 'Segmentos
                        Dim p As New P_0076_cls
                        p.id_P_0076 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.SegmentoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 77 Then 'Tipo Beneficiario
                        Dim p As New P_0077_cls
                        p.id_P_0077 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoBeneficiarioInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 78 Then 'Actuacion Apoderado
                        Dim p As New P_0078_cls
                        p.id_P_0078 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ActuacionApodInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 79 Then 'Contratos
                        Dim p As New P_0079_cls
                        p.id_P_0079 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ContratoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 80 Then 'Zona de Riesgo Recaudación
                        Dim p As New P_0080_cls
                        p.id_P_0080 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ZonRiesgoRecaudacionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 81 Then 'Plataformas
                        Dim p As New P_0081_cls
                        p.id_P_0081 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.PlataformaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 82 Then 'Estado Negociación
                        Dim p As New P_0082_cls
                        p.id_P_0082 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstNegoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 83 Then 'Objetivo Credito
                        Dim p As New P_0083_cls
                        p.id_P_0083 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ObjetivoCreditoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 84 Then 'Ciudad
                        Dim p As New P_0084_cls
                        p.id_P_0084 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CiuInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 85 Then 'Meses
                        Dim p As New P_0085_cls
                        p.id_P_0085 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.MesesInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 86 Then 'Estados de Cuentas
                        Dim p As New P_0086_cls
                        p.id_P_0086 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstCuentaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 87 Then 'Origen de Fondo
                        Dim p As New P_0087_cls
                        p.id_P_0087 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.OrigenFondoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 88 Then 'Tipo de Envío
                        Dim p As New P_0088_cls
                        p.id_P_0088 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoEnvioInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 89 Then 'Estado Ope-Negociación
                        Dim p As New P_0089_cls
                        p.id_P_0089 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstaOpeNegociacionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 90 Then 'Estado Cob-Neg
                        Dim p As New P_0090_cls
                        p.id_P_0090 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstaCobroNegociacionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 91 Then 'Tipo de Cartas
                        Dim p As New P_0091_cls
                        p.id_P_0091 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipodeCartaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 99 Then 'Parametros Consulta Api 
                        Dim p As New P_0099_cls
                        p.id_P_0099 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ParametroConsultaApiInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 100 Then 'Tipos de Productos
                        Dim p As New P_0100_cls
                        p.id_P_0100 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipodProductoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 101 Then 'Tipo Comision Factoring Electronico
                        Dim p As New P_0101_cls
                        p.id_P_0101 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoComisionFacElecInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 102 Then 'Parametros Tipo Provisiones
                        Dim p As New P_0102_cls
                        p.id_P_0102 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ParaTipoProvisionesInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 103 Then 'Estado no Recaudado
                        Dim p As New P_0103_cls
                        p.id_P_0103 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstNoRecaudadoInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 104 Then 'Caracteristica Operación
                        Dim p As New P_0104_cls
                        p.id_P_0104 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CaracterisOperacionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 105 Then 'Estado Línea Fogape
                        Dim p As New P_0105_cls
                        p.id_P_0105 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstadoLíneaFogapeInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 106 Then 'Tipo Devolución
                        Dim p As New P_0106_cls
                        p.id_P_0106 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoDevolucionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 107 Then 'Carga Masiva Documento
                        Dim p As New P_0107_cls
                        p.id_P_0107 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CargaMasivaDocInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 108 Then 'Carga Masiva Pago Cliente
                        Dim p As New P_0108_cls
                        p.id_P_0108 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CargaMasivaPagoClienteInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 109 Then 'Carga Masiva Pago Deudor
                        Dim p As New P_0109_cls
                        p.id_P_0109 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CargaMasivaPagoDeudorInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 110 Then 'estado evaluacion
                        Dim p As New P_0110_cls
                        p.id_P_0110 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstadoEvaluacionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 111 Then 'estado condicion
                        Dim p As New p_0111_cls
                        p.id_p_0111 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstadoCondicionInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 112 Then 'custodia
                        Dim p As New P_0112_cls
                        p.id_P_0112 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.CustodiaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 300 Then 'Informes por Mail
                        Dim p As New P_0300_cls
                        p.id_P_0300 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.InformePorMailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 301 Then 'Horario Informes por Mail
                        Dim p As New P_0301_cls
                        p.id_P_0301 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.HorarioInformesPorMailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 302 Then 'Usuarios para Nomina Diaria de Negocios
                        Dim p As New P_0302_cls
                        p.id_P_0302 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.UsuariosNominaDiaNegociosInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 303 Then 'Tipo de Servicio de Llamada
                        Dim p As New P_0303_cls
                        p.id_P_0303 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoServicioLlamadaInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 304 Then 'Envio por Email
                        Dim p As New P_0304_cls
                        p.id_P_0304 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EnvioPorMailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 305 Then 'Saludos Envio Email
                        Dim p As New P_0305_cls
                        p.id_P_0305 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.SaludosEnvioPorMailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 306 Then 'Texto del Envio Email
                        Dim p As New P_0306_cls
                        p.id_P_0306 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TextoEnvioPorMailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 307 Then 'Mensaje de Despedida del Envio Email
                        Dim p As New P_0307_cls
                        p.id_P_0307 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.MensajeDespedidaEnvioEmailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 308 Then 'Mensaje de Publicidad del Envio Email
                        Dim p As New P_0308_cls
                        p.id_P_0308 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.MensajePublicidadEnvioEmailInserta(p)
                    End If
                    If Dd_Tablas.SelectedValue = 309 Then 'Estado Usuarios 
                        Dim p As New P_0309_cls
                        p.id_P_0309 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.EstadoUsuarioInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 310 Then 'Tipo Cierre Contable
                        Dim p As New P_0310_cls
                        p.id_P_0310 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoCierreContableInserta(p)
                    End If



                    If Dd_Tablas.SelectedValue = 118 Then 'Clacificacion Cliente
                        Dim p As New P_0118_cls
                        p.id_P_0118 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.ClacificacionClienteInsert(p)
                    End If

                    If Dd_Tablas.SelectedValue = 312 Then
                        Dim p As New P_0312_cls
                        p.id_P_0312 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        AG.TipoCuentaInserta(p)
                    End If

                    If Dd_Tablas.SelectedValue = 313 Then
                        Dim p As New P_0313_cls
                        p.id_P_0313 = Nothing
                        p.pnu_des = UCase(UCase(txt_Des.Text))
                        p.pnu_est = Dt_est.SelectedValue
                        p.pnu_atr_001 = UCase(UCase(txt_cod_int.Text))
                        AG.CORASUInserta(p)
                    End If


                    'CG.ParametrosDevuelve(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
                    CG.ParametrosDevuelveTodos(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
                    msj.Mensaje(Me, caption, "Parametro insertado", TipoDeMensaje._Informacion)
                Else  'Modifica
                    If Dd_Tablas.SelectedValue = 1 Then 'region
                        AG.RegModifica(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 2 Then 'Comuna localidad
                        AG.ComunaLocalidadModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 3 Then 'estado deudor
                        AG.EstadoDeuModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)), Txt_sig.Text)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 5 Then 'Niveles
                        AG.NivelesModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 8 Then 'Estado-Cliente
                        AG.EstadoClienteModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 7 Then 'Modo-Operacion
                        AG.ModoOperacionModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 10 Then 'Estado de Poderes
                        AG.EstadoPoderesModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 11 Then 'Estado Docto 
                        AG.EstadoDocModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 12 Then 'Tipo Operación
                        AG.TipoOperacionModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 15 Then 'Carta Tipo
                        AG.CartaTipoModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 17 Then 'Zonas
                        AG.ZonasModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 18 Then 'Forma de Pago
                        AG.FormadePagoModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 20 Then 'Sistemas
                        AG.SistemaModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 21 Then 'Tipo Pagaré
                        AG.TipoPagareModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 22 Then 'Estado Pagaré
                        AG.EstadoPagareModi(Txt_codigo.Text, Dt_est.SelectedValue, UCase(UCase(txt_Des.Text)))
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 23 Then 'Moneda
                        AG.MonedaModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, Txt_ci.Text, Txt_cod24.Text, Txt_cfogap.Text)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 24 Then 'Tipo Garantía
                        AG.TipoGarantiaModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 25 Then 'Regimen Matrimonial
                        AG.RegimenMetrimoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 26 Then 'Tipo Aval
                        AG.TipoAvalModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 27 Then 'Estado Aval
                        AG.EstadoAvalModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 28 Then 'Estado Solicitud de Linea
                        AG.EstSolicitudLinealModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 29 Then 'Estado de Linea
                        AG.EstdeLinealModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 30 Then 'Estado de Operación
                        AG.EstOperacionModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                        'Exit Sub
                    End If
                    If Dd_Tablas.SelectedValue = 31 Then 'Tipo de Documento
                        Dim Tdoc As String 'Tipo de documento
                        Dim diasrete As String 'dias retencion
                        Dim TdocGes As String 'Tipo de documento a gestionar
                        Dim ptd As String 'pago tipo dolar
                        Dim bda As String 'busca dia habil

                        If Rb_tdcto.Checked = True Then
                            Tdoc = "T"
                        Else
                            Tdoc = "P"
                        End If

                        If Ch_diasret.Checked = True Then
                            diasrete = "N"
                        End If
                        If Ch_tdctogest.Checked = True Then
                            TdocGes = "S"
                        Else
                            TdocGes = "N"
                        End If

                        If Ch_ptd.Checked = True Then
                            ptd = "S"
                        Else
                            ptd = "N"
                        End If

                        If Ch_busdh.Checked = True Then
                            bda = "S"
                        Else
                            bda = "N"
                        End If

                        AG.TipoDocumentoModi(Txt_codigo.Text, Txt_dias.Text, Tdoc, UCase(Txt_sigla.Text), Txt_diancob.Text, bda, diasrete, Dp_mon.SelectedValue _
                                             , Txt_comi.Text, Txt_min.Text, Txt_max.Text, TdocGes, ptd, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)

                    End If
                    If Dd_Tablas.SelectedValue = 36 Then 'Tipo de Gasto Operacional
                        AG.TipGastoOperacionModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 40 Then 'Estado de Verificacion
                        AG.EstadoVerificacionModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 41 Then 'Tipo de cuentas x cobrar
                        Dim fc As String
                        Dim ci As String
                        Dim cp As Integer
                        If Rb_auto.Checked = True Then
                            fc = "A"
                        Else
                            fc = "M"
                        End If
                        If Rb_cinteresi.Checked = True Then
                            ci = "S"
                        Else
                            ci = "N"
                        End If
                        If Ch_cobra.Checked = True Then
                            cp = "1"
                        End If
                        If Ch_paga.Checked = True Then
                            cp = "2"
                        End If
                        If Ch_cobra.Checked = True And Ch_paga.Checked = True Then
                            cp = "3"
                        End If
                        AG.TipoCuentaXCobrarModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, cp, fc, ci)
                    End If
                    If Dd_Tablas.SelectedValue = 44 Then 'Tipo de Cliente
                        AG.TipoClienteModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 45 Then 'Tipo de Ejecutivo
                        AG.TipoEjecutivoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 48 Then 'Estado de Ejecutivo
                        AG.EstadoEjecutivoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 49 Then 'Tipo de Telefono
                        AG.TipoFonoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 51 Then 'Tipo Gasto Recaudacion
                        AG.TipoGastoRecaudacionModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 52 Then 'Estado Docto de pago
                        AG.EsDocdePagoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 53 Then 'Que se Paga
                        AG.QuesePagaModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If



                    If Dd_Tablas.SelectedValue = 54 Then 'Tipo de Ingreso
                        Dim ar As String 'Aplicar retencion
                        Dim ip As String 'ingreso de pago
                        Dim nd As String 'Nomina deposito

                        If Rb_TiIngreso.Checked = True Then
                            ar = "T"
                        Else
                            ar = "P"
                            txt_NDR.Text = ""
                        End If
                        If RB_Si.Checked = True Then
                            ip = "S"
                        Else
                            ip = "N"
                        End If
                        If Rb_ND_Si.Checked = True Then
                            nd = "S"
                        Else 'Rb_ND_No.Checked = True Then
                            nd = "N"
                        End If

                        'If Rb_TiIngreso.Checked = True And RB_Si.Checked = True Then
                        AG.TipoDeIngresoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, ar, txt_NDR.Text, ip, txt_DLE.Text _
                                             , nd)
                        'End If
                        'If Rb_TiIngreso.Checked = False And Rb_pza.Checked = True And RB_Si.Checked = True Then
                        '    AG.TipoDeIngresoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, "N", txt_NDR.Text, "S", txt_DLE.Text _
                        '                         , "T")
                        'End If
                        'If Rb_TiIngreso.Checked = False And Rb_pza.Checked = True And RB_Si.Checked = False Then
                        '    AG.TipoDeIngresoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, "N", txt_NDR.Text, "N", txt_DLE.Text _
                        '                         , "T")
                        'End If
                    End If




                    If Dd_Tablas.SelectedValue = 55 Then 'Que a Pagar
                        AG.QueAPagarModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 56 Then 'Tipo de Egreso
                        Dim dep As String 'Nomina egreso
                        Dim carg As String 'carga/abono
                        Dim ingdocpag As String 'ingresa doc de pago
                        Dim cta As String 'valida cuenta corriente
                        Dim sis As String 'Valida sistema en que se usa parametro
                        Dim gmf As String 'Valida si parametro aplica GMF
                        If rb_cargosi.Checked = True Then
                            carg = "S"
                        Else
                            carg = "N"
                        End If
                        If Rb_dep.Checked = True Then
                            dep = "A"
                            'End If
                        ElseIf rb_transelec.Checked = True Then
                            dep = "T"
                            'End If
                        ElseIf Rb_sdep.Checked = True Then
                            dep = "S"
                        End If

                        If Rb_indcpag.Checked = True Then
                            ingdocpag = "S"
                        Else
                            ingdocpag = "N"
                        End If
                        If Rb_valsi.Checked = True Then
                            cta = "S"
                        Else
                            cta = "N"
                        End If

                        If Rb_SisA.Checked Then
                            sis = "A"
                        ElseIf Rb_SisB.Checked Then
                            sis = "B"
                        ElseIf Rb_SisW.Checked Then
                            sis = "W"
                        End If

                        If Rb_GMF_S.Checked Then
                            gmf = "S"
                        Else
                            gmf = "N"
                        End If

                        AG.TipodeEgresoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, carg, ingdocpag, dep, cta, sis, gmf)
                    End If
                    If Dd_Tablas.SelectedValue = 58 Then 'Categor ía de Riesgo
                        AG.CategoriadeRiesgoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 60 Then 'Estado de Facturas
                        AG.EstadodeFacturasModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 61 Then 'Motivos de Protestos
                        AG.MotivosdeProtestosModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 62 Then 'Facultades de Poder
                        AG.FacultadesdePoderModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 63 Then 'Razones Sociales
                        AG.RazonesSocialesModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, txt_razsoc.Text)
                    End If
                    If Dd_Tablas.SelectedValue = 64 Then 'Actividad Económica 
                        AG.ActividadEconómicaModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 65 Then 'Tipo de Riesgos
                        AG.TipodeRiesgosModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, Txt_prov.Text)
                    End If
                    If Dd_Tablas.SelectedValue = 67 Then 'Tipo de Envio Información
                        AG.TipoEnvioInfoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 68 Then 'Forma de Envío
                        AG.FormadeEnvíoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If

                    If Dd_Tablas.SelectedValue = 70 Then 'País
                        AG.PaisModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 71 Then 'Otro
                        AG.OtroModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 72 Then 'Otro 1
                        AG.Otro1Modi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 73 Then 'Otro 2
                        AG.Otro2Modi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 74 Then 'Otro 3
                        AG.Otro3Modi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 75 Then 'Tipo Operación Contable
                        AG.TipoOperaciónContableModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 76 Then 'Segmentos
                        AG.SegmentosModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 77 Then 'Tipo Beneficiario
                        AG.TipoBeneficiarioModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 78 Then 'Actuacion Apoderado
                        AG.ActuacionApoderadoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 79 Then 'Contratos
                        AG.ContratosModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 80 Then 'Zona de Riesgo Recaudación
                        AG.ZonadeRiesgoRecaudaciónModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 81 Then 'Plataformas
                        AG.PlataformasModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 82 Then 'Estado Negociación
                        AG.EstadoNegociaciónModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 83 Then 'Objetivo Credito
                        AG.EstadoNegociaciónModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 84 Then 'Ciudad
                        AG.CiudadModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 85 Then 'Meses
                        AG.MesesModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 86 Then 'Estados de Cuentas
                        AG.EstadosdeCuentasModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 87 Then 'Origen de Fondo
                        AG.OrigendeFondoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 88 Then 'Tipo de Envío
                        AG.TipodeEnvíoModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 89 Then 'Estado Ope-Negociación
                        AG.EstadoOpeNegociacionModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 90 Then 'Estado Cob-Neg
                        AG.EstadoCobNegModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 91 Then 'Tipo de Cartas
                        AG.TipodeCartasModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 99 Then 'Parametros Consulta Api
                        AG.ParametroConsultaModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 100 Then 'Tipos de Productos
                        AG.TipodeProductosModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 101 Then 'Tipo Comision Factoring Electronico
                        AG.TipoComisionFactoringElectronicoModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 102 Then 'Parametros Tipo Provisiones
                        AG.ParametrosTipoProvisionesModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 103 Then 'Estado no Recaudado
                        AG.EstadonoRecaudadoModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 104 Then 'Caracteristica Operación
                        AG.CaracteristicaOperaciónModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 105 Then 'Estado Línea Fogape
                        AG.EstadoLíneaFogapeModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 106 Then 'Tipo Devolución
                        AG.TipoDevoluciónModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 107 Then 'Carga Masiva Documento
                        AG.CargaMasivaDocumentoModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 108 Then 'Carga Masiva Pago Cliente
                        AG.CargaMasivaPagoClienteModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 109 Then 'Carga Masiva Pago Deudor
                        AG.CargaMasivaPagoDeudorModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 300 Then 'Informes por Mail
                        AG.InformesporMailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 301 Then 'Horario Informes por Mail
                        AG.HorarioInformesporMailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 302 Then 'Usuarios para Nomina Diaria de Negocios
                        AG.UsuariosparaNominaDiariadeNegociosModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 303 Then 'Tipo de Servicio de Llamada 
                        AG.TipodeServiciodeLlamadaModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 304 Then 'Envio por Email
                        AG.EnvioporEmailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 305 Then 'Saludos Envio Email
                        AG.SaludosEnvioEmailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 306 Then 'Texto del Envio Email
                        AG.TextodelEnvioEmailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 307 Then 'Mensaje de Despedida del Envio Email
                        AG.MensajedeDespedidadelEnvioEmailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 308 Then 'Mensaje de Publicidad del Envio Email
                        AG.MensajedePublicidaddelEnvioEmailModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 309 Then 'Estado Usuarios
                        AG.EstadoUsuariosModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 310 Then 'Tipo Cierre Contable
                        AG.TipoCierreContableModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 312 Then 'Tipo Cuenta
                        AG.TipoCuentaModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 313 Then 'CORASU
                        AG.CORASUModifica(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue, UCase(txt_cod_int.Text.Trim()))
                    End If
                    If Dd_Tablas.SelectedValue = 69 Then 'Tipo Clasificacion
                        AG.TipoClasificacionModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 110 Then 'Estado de evaluacion
                        AG.EstadoEvaluacionModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 111 Then 'Estado condicion
                        AG.EstadoCondicionModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Dd_Tablas.SelectedValue = 112 Then 'Custodia
                        AG.CustodiaModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If

                    If Dd_Tablas.SelectedValue = 118 Then 'Clasificacion de cliente
                        AG.ClasificacionClienteUpdate(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If


                    msj.Mensaje(Me, caption, "Parámetro Modificado", TipoDeMensaje._Informacion)
                    'cg.ParametrosDevuelve(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
                End If
                'CG.ParametrosDevuelve(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
                CG.ParametrosDevuelveTodos(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
            End If








            If Rb_alfa.Checked = True Then
                'If Txt_codigo.Text = "" Then
                Dim coll As New Collection

                coll = CG.ParametrosAfanumericosDevuelveDetalle(Drop_TablaAlfa.SelectedValue, Dt_par.SelectedValue)

                If IsNothing(coll) Then


                    'If HF_Estado.Value = 1 Then 'Nuevo
                    If Txt_codigo.Text = "" Then
                        msj.Mensaje(Me, caption, "Ingrese Parámetro", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If

                    If Drop_TablaAlfa.SelectedValue = 1 Then 'Giro
                        ' Dim gi As New PL_000006_cls
                        'gi = cg.GiroDevuelveUltimo()
                        'gi.id_PL_000006 = HF_Gi_ult.Value

                        Dim g As New PL_000006_cls
                        'g.id_PL_000006 = HF_Gi_ult.Value
                        g.id_PL_000006 = Format(CLng(Txt_codigo.Text), "000000")
                        g.pal_des = UCase(UCase(txt_Des.Text))
                        g.pal_est = Dt_est.SelectedValue
                        g.pal_sis = "N"
                        AG.GiroInserta(g)
                    End If

                    If Drop_TablaAlfa.SelectedValue = 2 Then 'Plazas
                        Dim p As New PL_000047_cls
                        p.id_PL_000047 = Format(CLng(Txt_codigo.Text), "000000")
                        p.pal_des = UCase(UCase(txt_Des.Text))
                        p.pal_est = Dt_est.SelectedValue
                        p.pal_reg = txt_RegPla.Text
                        AG.PlazaInserta(p)
                    End If


                    If Drop_TablaAlfa.SelectedValue = 3 Then 'Banca Clientes
                        Dim bc As New PL_000066_cls
                        bc.id_PL_000066 = Format(CLng(Txt_codigo.Text), "000000")
                        bc.pal_des = UCase(UCase(txt_Des.Text))
                        bc.pal_est = Dt_est.SelectedValue
                        bc.pal_sis = "N"
                        AG.BCliInserta(bc)
                    End If
                    If Drop_TablaAlfa.SelectedValue = 4 Then 'Factoring
                        Dim f As New PL_000069_cls
                        f.id_PL_000069 = Format(CLng(Txt_codigo.Text), "000000")
                        f.pal_des = UCase(UCase(txt_Des.Text))
                        f.pal_est = Dt_est.SelectedValue
                        f.pal_sis = "N"
                        AG.FactInserta(f)
                    End If
                    msj.Mensaje(Me, caption, "Parámetro insertado", TipoDeMensaje._Informacion)
                Else 'Modifica
                    If Drop_TablaAlfa.SelectedValue = 1 Then
                        AG.GiroModi(Txt_codigo.Text, UCase(txt_Des.Text), Dt_est.SelectedValue)
                    End If
                    If Drop_TablaAlfa.SelectedValue = 2 Then
                        AG.PlazasModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue, txt_RegPla.Text)
                    End If
                    If Drop_TablaAlfa.SelectedValue = 3 Then
                        AG.BancaClienteModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    If Drop_TablaAlfa.SelectedValue = 4 Then
                        AG.FactoringModi(Txt_codigo.Text, UCase(UCase(txt_Des.Text)), Dt_est.SelectedValue)
                    End If
                    msj.Mensaje(Me, caption, "Parámetro modificado", TipoDeMensaje._Informacion)

                    CargaAtrAlfa()
                    CG.ParametrosAlfanumericoDevuelve(Me.Drop_TablaAlfa.SelectedItem.Value, True, Dt_par)
                End If

            End If
            'End If

            LimpiaTodosControles()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Eliminar.Click
        Try

            If Dd_Tablas.SelectedValue <> 0 Then
                If AG.EliminaParametros(Dd_Tablas.SelectedValue, Txt_codigo.Text) Then
                    msj.Mensaje(Me, caption, "Parámetro Eliminado", TipoDeMensaje._Informacion)
                    CG.ParametrosDevuelveTodos(Me.Dd_Tablas.SelectedItem.Value, True, Dt_par)
                Else
                    msj.Mensaje(Me, caption, "Parámetro no se puede eliminar por que esta asociado a una entidad", TipoDeMensaje._Informacion)
                End If
                'Me.CargaAtr()

                '  Txt_codigo.Text = ""
                ' Dt_est.ClearSelection()
            End If
            'End Select
            If Drop_TablaAlfa.SelectedValue <> 0 Then

                If AG.EliminaParametrosAlfa(Drop_TablaAlfa.SelectedValue, Txt_codigo.Text) Then
                    msj.Mensaje(Me, caption, "Parámetro Eliminado", TipoDeMensaje._Informacion)
                    CG.ParametrosAlfanumericoDevuelveCodigos(Drop_TablaAlfa.SelectedValue, True, Dt_par)
                Else
                    msj.Mensaje(Me, caption, "Parámetro no se puede eliminar por que esta asociado a una entidad", TipoDeMensaje._Informacion)
                End If

            End If
            'Me.Label30.Visible = False
            Me.txt_Des.Enabled = False
            Me.txt_Des.CssClass = "clsDisabled"
            Me.Label46.Visible = False
            Me.txt_cod_int.Text = ""
            Me.txt_cod_int.Visible = False
            Me.Txt_codigo.Text = ""
            Me.Dd_Tablas.SelectedValue = 0
            Me.Dt_par.SelectedValue = 0
            Me.Dt_est.SelectedValue = "X"
            Rb_alfa.Enabled = True
            Rb_num.Enabled = True
            Rb_num.Checked = True
            Rb_alfa.Checked = False


            Dd_Tablas.Enabled = True
            Dd_Tablas.CssClass = "clsMandatorio"
            Drop_TablaAlfa.Enabled = False
            Drop_TablaAlfa.CssClass = "clsDisabled"

            Dt_par.Enabled = False
            Dt_par.CssClass = "clsDisabled"
            Dt_est.Enabled = False
            Dt_est.CssClass = "clsDisabled"
            Drop_TablaAlfa.ClearSelection()
            Txt_codigo.ReadOnly = True
            Txt_codigo.CssClass = "clsDisabled"

            btn_eli.Enabled = False
            btn_gua.Enabled = False
            btn_nue.Enabled = False
            txt_Des.Text = ""
            txt_RegPla.Text = ""
            Me.MultiView1.ActiveViewIndex = -1
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Sub y Function"

    Private Sub CargaAtrAlfa()
        Dim coll As New Collection
        Dim x As String

        coll = cg.ParametrosAfanumericosDevuelveDetalle(Drop_TablaAlfa.SelectedValue, Dt_par.SelectedValue)

        x = Drop_TablaAlfa.SelectedValue()
        Dt_est.SelectedValue = coll.Item(1).pal_est
        txt_Des.Text = coll.Item(1).pal_des

        'If x = 2 Then
        '    txt_RegPla.Text = coll.Item(1).pal_sis
        'End If

        If Drop_TablaAlfa.SelectedValue = 2 Then
            txt_RegPla.Text = coll.Item(1).pal_sis
        End If
    End Sub

    Private Sub CargaAtr()

        Dim coll As New Collection
        Dim i As String

        coll = CG.Parametros_Detalle_Devuelve(Dd_Tablas.SelectedValue, Dt_par.SelectedValue)

        i = Me.Dd_Tablas.SelectedValue

        'Me.Dt_est.SelectedValue = coll.Item(1).pnu_est
        'txt_Des.Text = coll.Item(1).pnu_des

        Me.Dt_est.SelectedValue = coll.Item(1).pnu_est.ToString().Trim()
        txt_Des.Text = coll.Item(1).pnu_des.ToString().Trim()

        If i = 313 Then
            txt_cod_int.Text = coll.Item(1).pnu_atr_001.ToString().Trim()
        End If


        If i = 31 Then

            CG.ParametrosDevuelve(23, True, Me.Dp_mon)
            Me.Txt_dias.Text = coll.Item(1).pnu_atr_001

            If coll.Item(1).pnu_atr_002 = "T" Then
                Me.Rb_tdcto.Checked = True
                Label14.Visible = True
                Txt_dias.Visible = True

                Rb_pza.Checked = False
            ElseIf coll.Item(1).pnu_atr_002 = "P" Then
                Rb_tdcto.Checked = False
                Me.Rb_pza.Checked = True
                Label14.Visible = False
                Txt_dias.Visible = False
            End If

            Me.Txt_sigla.Text = coll.Item(1).pnu_atr_003
            Me.Txt_diancob.Text = coll.Item(1).pnu_atr_004

            If coll.Item(1).pnu_atr_005 = "N" Then
                Me.Ch_busdh.Checked = False
            Else
                Me.Ch_busdh.Checked = True
            End If

            If coll.Item(1).pnu_atr_006 = "N" Then
                Me.Ch_diasret.Checked = True
                'Else
                '    Me.Ch_diasret.Checked = False
            End If

            If coll.Item(1).pnu_atr_007 = "" Then
                Dp_mon.ClearSelection()
                Dp_mon.Items.FindByValue(0).Selected = True
            End If

            If coll.Item(1).pnu_atr_007 = "1" Then
                Dp_mon.ClearSelection()
                Me.Dp_mon.Items.FindByValue(1).Selected = True
            ElseIf coll.Item(1).pnu_atr_007 = "2" Then
                Dp_mon.ClearSelection()
                Me.Dp_mon.Items.FindByValue(2).Selected = True
            ElseIf coll.Item(1).pnu_atr_007 = "3" Then
                Dp_mon.ClearSelection()
                Me.Dp_mon.Items.FindByValue(3).Selected = True
            ElseIf coll.Item(1).pnu_atr_007 = "4" Then
                Dp_mon.ClearSelection()
                Me.Dp_mon.Items.FindByValue(4).Selected = True
            End If

            Me.Txt_comi.Text = coll.Item(1).pnu_atr_008
            Me.Txt_min.Text = coll.Item(1).pnu_atr_009
            Me.Txt_max.Text = coll.Item(1).pnu_atr_010

            If coll.Item(1).pnu_atr_011 = "S" Then
                Me.Ch_tdctogest.Checked = True
            ElseIf coll.Item(1).pnu_atr_011 = "N" Then
                Me.Ch_tdctogest.Checked = False
            End If

            If coll.Item(1).pnu_atr_012 = "S" Then
                Me.Ch_ptd.Checked = True
            End If
            '  Me.Txt_dprorr.Text = coll.Item(1).pnu_atr_013
        End If




        If i = 41 Then
            'Dt_est.selecvalue = coll.Item(1).pnu_est
            'Me.Dt_est.SelectedValue = coll.Item(1).pnu_est


            If coll.Item(1).pnu_atr_003 = "A" Then
                Rb_auto.Checked = True
            ElseIf coll.Item(1).pnu_atr_003 = "M" Then
                Me.Rb_manual.Checked = True
            ElseIf coll.Item(1).pnu_frm_cre = "" Then
                Rb_auto.Checked = False
                Rb_manual.Checked = False
            End If

            If coll.Item(1).pnu_atr_004 = "1" Then
                Ch_cobra.Checked = True
            ElseIf coll.Item(1).pnu_atr_004 = "2" Then
                Ch_paga.Checked = True
            ElseIf coll.Item(1).pnu_atr_004 = "3" Then
                Ch_paga.Checked = True
                Ch_cobra.Checked = True
            ElseIf coll.Item(1).pnu_atr_004 = "" Then
                Ch_paga.Checked = False
                Ch_cobra.Checked = False
            End If

            If coll.Item(1).pnu_atr_005 = "S" Then
                Rb_cinteresi.Checked = True
            ElseIf coll.Item(1).pnu_atr_005 = "N" Then
                Me.Rb_cinteresNO.Checked = True
                'ElseIf coll.Item(1).pnu_atr_005 = "" Then
                '   Rb_cinteresi.Checked = False
                '  Me.Rb_cinteresNO.Checked = False
            End If
        End If

        If i = 3 Then
            Me.Txt_sig.Text = coll.Item(1).pnu_atr_003

        End If

        If i = 65 Then
            Me.Txt_prov.Text = coll.Item(1).pnu_atr_002
            'Dt_est.SelectedValue = coll.Item(3).pnu_est


        End If

        If i = 63 Then 'razon  social
            Me.txt_razsoc.Text = coll.Item(1).pnu_atr_007
        End If

        If i = 23 Then

            Me.Txt_ci.Text = coll.Item(1).pnu_atr_003
            Me.Txt_cfogap.Text = coll.Item(1).pnu_atr_004
            Me.Txt_cod24.Text = coll.Item(1).pnu_atr_005


        End If

        If i = 56 Then 'tipo de egreso

            Me.Rb_dep.Checked = False
            Me.Rb_sdep.Checked = False
            Me.rb_transelec.Checked = False

            Me.Rb_SisA.Checked = False
            Me.Rb_SisB.Checked = False
            Me.Rb_SisW.Checked = False

            Me.Rb_GMF_S.Checked = False
            Me.Rb_GMF_N.Checked = False

            Me.Rb_valsi.Checked = False
            Me.Rb_valno.Checked = False

            Select Case coll.Item(1).pnu_atr_002
                Case "S"
                    Me.Rb_sdep.Checked = True
                Case "A"
                    Me.Rb_dep.Checked = True
                Case "T"
                    Me.rb_transelec.Checked = True
            End Select

            Select Case coll.Item(1).pnu_atr_003
                Case "N"
                    Me.Rb_valno.Checked = True
                Case "S"
                    Me.Rb_valsi.Checked = True
            End Select

            Select Case coll.Item(1).pnu_atr_004
                Case "N"
                    Me.Rb_noingdcpag.Checked = True

                Case "S"
                    Me.Rb_indcpag.Checked = True

            End Select

            Select Case coll.Item(1).pnu_atr_005
                Case "N"
                    Me.Rb_cargono.Checked = True
                Case "S"
                    Me.rb_cargosi.Checked = True
            End Select

            Select Case coll.Item(1).pnu_atr_006
                Case "A"
                    Me.Rb_SisA.Checked = True
                Case "B"
                    Me.Rb_SisB.Checked = True
                Case "W"
                    Me.Rb_SisW.Checked = True
            End Select

            Select Case coll.Item(1).pnu_atr_007
                Case "S"
                    Me.Rb_GMF_S.Checked = True
                Case "N"
                    Me.Rb_GMF_N.Checked = True
            End Select

        End If

        If i = 54 Then 'tipo de ingreso

            txt_DLE.Text = coll.Item(1).pnu_atr_004

            Select Case coll.Item(1).pnu_atr_002
                Case "T"
                    Rb_TiIngreso.Checked = True
                    Rb_Plaza.Checked = False
                    Label35.Visible = True
                    txt_NDR.Visible = True
                    txt_NDR.Text = coll.Item(1).pnu_atr_001
                Case "P"
                    Rb_Plaza.Checked = True
                    Rb_TiIngreso.Checked = False



            End Select


            Select Case coll.Item(1).pnu_atr_005
                Case "S"
                    Rb_ND_Si.Checked = True
                    Rb_ND_No.Checked = False
                Case "N"
                    Rb_ND_No.Checked = True
                    Rb_ND_Si.Checked = False
            End Select


            Select Case coll.Item(1).pnu_atr_003
                Case "S"
                    RB_Si.Checked = True
                    RB_No.Checked = False

                Case "N"
                    RB_No.Checked = True
                    RB_Si.Checked = False
            End Select

            'If coll.Item(1).pnu_dia_ret <> 0 Then
            '    txt_NDR.Visible = True
            '    txt_NDR.Text = coll.Item(1).pnu_dia_ret
            'End If


        End If

        If i = 41 Then

        End If

    End Sub


    Sub LimpiaTodosControles()
        Dt_est.ClearSelection()


        txt_Des.Text = ""
        Txt_codigo.Text = ""
        Txt_cfogap.Text = ""
        Txt_ci.Text = ""
        Txt_cod24.Text = ""
        Txt_comi.Text = ""
        Txt_diancob.Text = ""
        Txt_dias.Text = ""
        txt_DLE.Text = ""
        Txt_max.Text = ""
        Txt_min.Text = ""
        txt_NDR.Text = ""
        Txt_prov.Text = ""
        txt_razsoc.Text = ""
        txt_RegPla.Text = ""
        Txt_sig.Text = ""
        Txt_sigla.Text = ""
        txt_cod_int.Text = ""


        'Rb_alfa.Checked = False
        Rb_auto.Checked = False
        Rb_cargono.Checked = False
        rb_cargosi.Checked = False
        Rb_cinteresi.Checked = False
        Rb_cinteresNO.Checked = False
        Rb_dep.Checked = False
        Rb_indcpag.Checked = False
        Rb_manual.Checked = False
        Rb_ND_No.Checked = False
        Rb_ND_Si.Checked = False
        RB_No.Checked = False
        RB_Si.Checked = False
        'Rb_num.Checked = False
        Rb_Plaza.Checked = False
        Rb_pza.Checked = False
        Rb_sdep.Checked = False
        Rb_tdcto.Checked = False
        Rb_TiIngreso.Checked = False
        rb_transelec.Checked = False
        Rb_noingdcpag.Checked = False
        Rb_valno.Checked = False
        Rb_valsi.Checked = False

        Rb_SisA.Checked = False
        Rb_SisB.Checked = False
        Rb_SisW.Checked = False

        Rb_GMF_S.Checked = False
        Rb_GMF_N.Checked = False

        Ch_busdh.Checked = False
        Ch_cobra.Checked = False
        Ch_diasret.Checked = False
        Ch_paga.Checked = False
        Ch_ptd.Checked = False
        Ch_tdctogest.Checked = False
        Dp_mon.ClearSelection()
        Dt_par.ClearSelection()

        txt_Des.Focus()


    End Sub


    Sub InhabilitarBoton()
        btn_eli.Enabled = False
        btn_gua.Enabled = False
        btn_nue.Enabled = False
        Label35.Visible = False
        txt_NDR.Visible = False

    End Sub


#End Region

#Region "Botonera"

    Protected Sub btn_nue_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nue.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20020112, Usr, "PRESIONO BOTON NUEVO PARAMETRO ") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            btn_gua.Enabled = True
            Label30.Visible = True
            'btn_nue_Click()
            txt_Des.Visible = True


            LimpiaTodosControles()
            Try
                If Dd_Tablas.SelectedValue <> 0 Then
                    Txt_codigo.ReadOnly = True
                    txt_Des.Visible = True
                    txt_Des.CssClass = "clsMandatorio"
                    Label30.Visible = True

                    Exit Sub
                End If
                If Drop_TablaAlfa.SelectedValue <> 0 Then
                    Txt_codigo.ReadOnly = False
                    txt_Des.Visible = True
                    Txt_codigo.CssClass = "clsMandatorio"
                    txt_Des.CssClass = "clsMandatorio"
                    Label30.Visible = True
                    HF_Estado.Value = 1
                    'Exit Sub
                End If


            Catch ex As Exception
                msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
            End Try
        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_gua1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gua.Click 'Handles btn_gua.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20040112, Usr, "PRESIONO BOTON GUARDAR PARAMETRO ") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        Try
            If UCase(txt_Des.Text) = "" Then
                msj.Mensaje(Me, caption, "Ingrese Descripcion", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Dt_est.SelectedValue = "X" Then
                msj.Mensaje(Me, caption, "Seleccione Estado", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Dt_est.SelectedValue = "X" And Dd_Tablas.SelectedValue = 18 Then
                msj.Mensaje(Me, caption, "Seleccione Sistema", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If Rb_num.Checked = True Then

                If Dd_Tablas.SelectedValue = 23 Then 'Moneda
                    If Txt_ci.Text = "" Then
                        msj.Mensaje(Me, caption, "Ingrese codigo interno", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                    If Txt_cod24.Text = "" Then
                        msj.Mensaje(Me, caption, "Ingrese codigo D24", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                    If Txt_cfogap.Text = "" Then
                        msj.Mensaje(Me, caption, "Ingrese codigo fogape", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If
            End If

            If Dd_Tablas.SelectedValue = 313 Then
                If txt_cod_int.Text = "" Then
                    msj.Mensaje(Me, caption, "Ingrese Codigo Interno", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If Dd_Tablas.SelectedValue = 31 Then 'tipo de documento
                If Rb_tdcto.Checked = True Then
                    If Txt_dias.Text = "" Then
                        msj.Mensaje(Me, caption, "Ingrese cantidad de dias", TipoDeMensaje._Informacion)
                        Exit Sub
                    End If
                    If Not IsNumeric(Txt_dias.Text) Then
                        msj.Mensaje(Me, caption, "Ingrese solo numeros", TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If
                End If
                If Txt_sigla.Text = "" Then
                    msj.Mensaje(Me, caption, "Ingrese sigla", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                'If Txt_min.Text = "" Then
                '    msj.Mensaje(Me.Page, caption, "Ingrese monto Mínimo", TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                'If Txt_max.Text = "" Then
                '    msj.Mensaje(Me.Page, caption, "Ingrese monto Máximo", TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                'If Txt_min.Text > Txt_max.Text Then
                '    msj.Mensaje(Me.Page, caption, "Monto Mínimo no puiede ser mayor a monto Máximo", TipoDeMensaje._Exclamacion)
                '    Exit Sub
                'End If

                If Dp_mon.SelectedValue = 0 Then
                    msj.Mensaje(Me.Page, caption, "Seleccione moneda", TipoDeMensaje._Exclamacion, "", True)
                    Exit Sub
                End If
            End If



            If Txt_codigo.Text = "" Then
                msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            Else
                msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_lim1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_lim.Click
        Try

            Me.MultiView1.ActiveViewIndex = -1

            Me.Txt_codigo.Text = ""
            Me.Dd_Tablas.SelectedValue = 0
            Me.Dt_par.SelectedValue = 0
            Me.Dt_est.SelectedValue = "X"
            Rb_alfa.Enabled = True
            Rb_num.Enabled = True
            Rb_num.Checked = True
            Rb_alfa.Checked = False
            Rb_cargono.Checked = False
            Rb_auto.Checked = False
            rb_cargosi.Checked = False
            Rb_cinteresi.Checked = False
            Rb_cinteresNO.Checked = False
            Rb_dep.Checked = False
            Rb_indcpag.Checked = False
            Rb_manual.Checked = False
            Rb_ND_No.Checked = False
            Rb_ND_Si.Checked = False
            RB_No.Checked = False

            Rb_SisA.Checked = False
            Rb_SisB.Checked = False
            Rb_SisW.Checked = False

            Rb_GMF_S.Checked = False
            Rb_GMF_N.Checked = False

            Dd_Tablas.Enabled = True
            Dd_Tablas.CssClass = "clsMandatorio"
            Drop_TablaAlfa.Enabled = False
            Drop_TablaAlfa.CssClass = "clsDisabled"

            Dt_par.Enabled = False
            Dt_par.CssClass = "clsDisabled"
            Dt_est.Enabled = False
            Dt_est.CssClass = "clsDisabled"
            Drop_TablaAlfa.ClearSelection()
            Txt_codigo.ReadOnly = True
            Txt_codigo.CssClass = "clsDisabled"

            Label46.Visible = False
            txt_cod_int.Visible = False
            txt_cod_int.Text = ""

            btn_eli.Enabled = False
            btn_gua.Enabled = False
            btn_nue.Enabled = False
            txt_Des.Text = ""
            txt_RegPla.Text = ""
            Txt_ci.Text = ""
            Txt_cod24.Text = ""
            Txt_cfogap.Text = ""
            Label30.Visible = True
            txt_Des.CssClass = "clsDisabled"
            txt_Des.Enabled = False
            B_CategoriaRiesgo.Visible = False


            LimpiaTodosControles()

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_eli1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click 'Handles btn_eli.Click



        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20030112, Usr, "PRESIONO BOTON ELIMINAR PARAMETRO ") = False Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Try
            If Dd_Tablas.SelectedValue <> 0 Then
                msj.Mensaje(Me.Page, caption, "¿Esta seguro de eliminar este parámetro?", TipoDeMensaje._Confirmacion, Link_Eliminar.UniqueID, True)
            End If

            If Drop_TablaAlfa.SelectedValue <> 0 Then
                msj.Mensaje(Me.Page, caption, "¿Esta seguro de eliminar este parámetro?", TipoDeMensaje._Confirmacion, Link_Eliminar.UniqueID, True)
            End If

        Catch ex As Exception
            msj.Mensaje(Me, caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub btn_gua_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_gua.Click

    End Sub

    Protected Sub B_CategoriaRiesgo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles B_CategoriaRiesgo.Click
        Try
            ' B_CategoriaRiesgo.Attributes.Add("onclick", "WinOpen(2,'CategoriaRiesgo.aspx','CtrPopUp',550,500,200,200);")
            RW.AbrePopup(Me, 2, "CategoriaRiesgo.aspx?Id_Docto=" & Dt_par.SelectedValue & "&Documento=" & txt_Des.Text, "CtrPoPup", 650, 500, 200, 200)

        Catch ex As Exception

        End Try
    End Sub

#End Region

  
   

End Class
