Imports CapaDatos
Partial Class NuevoAval
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim caption As String = "Avales"
    Dim CG As New ConsultasGenerales
    Dim FG As New FuncionesGenerales.FComunes
    Dim CL As New ConsultasLegales
    Dim AL As New ActualizacionesLegales
    Dim Msj As New ClsMensaje
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim sesion As New ClsSession.ClsSession

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LlenaDrop()
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Rut_Aval.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Mto.Attributes.Add("Style", "TEXT-ALIGN: right")
                txt_Plazo.Attributes.Add("Style", "TEXT-ALIGN: right")
                If Request.QueryString("Id_aval") <> 0 Then
                    HF_id_Aval.Value = Request.QueryString("Id_aval")
                    Txt_Rut_Cli.ReadOnly = True
                    IB_AyudaCli.Enabled = False
                    txt_Rut_MaskedEditExtender.Enabled = False
                    IB_Limpiar.Enabled = False
                    CargaDatos()
                End If
            End If
            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',570 ,400,100,100);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Ciudad_Comercial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Ciudad_Comercial.SelectedIndexChanged
        Try
            CL.ComunaDevuelvePorIdCiudad(Drop_Ciudad_Comercial.SelectedValue, True, Drop_Comuna_Comercial)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_Ciudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Ciudad.SelectedIndexChanged
        Try
            CL.ComunaDevuelvePorIdCiudad(Drop_Ciudad.SelectedValue, True, Drop_Comuna)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Drop_TipoAval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_TipoAval.SelectedIndexChanged
        Try
            If Drop_TipoAval.SelectedValue = 1 Then ' Natural

                Label18.Visible = False
                txt_Notaria.Visible = False
                Label19.Visible = False
                txt_F_Est_Sit.Visible = False
                Label21.Visible = False
                txt_JuntaExt.Visible = False
                Label20.Visible = False
                txt_Mto.Visible = False
                Label22.Visible = False
                txt_Plazo.Visible = False
            End If

            If Drop_TipoAval.SelectedValue = 2 Then 'Juridico
                Label18.Visible = True
                Label19.Visible = True
                txt_F_Est_Sit.Visible = True
                txt_Notaria.Visible = True
                Label21.Visible = True
                txt_JuntaExt.Visible = True
                Label20.Visible = True
                txt_Mto.Visible = True
                Label22.Visible = True
                txt_Plazo.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        Try

            'Select Case HF_Estado.Value
            '    Case 1 'Guardar
            Dim mto As Double
            Dim Plz As Integer
            Dim F_Est_Sit As String
            Dim F_J_A As String

            If txt_Mto.Text = "" Then
                mto = 0
            Else
                mto = Format(CLng(txt_Mto.Text))
            End If
            If txt_Plazo.Text = "" Then
                Plz = 0
            Else
                Plz = txt_Plazo.Text
            End If

            If txt_F_Est_Sit.Text = "" Then
                F_Est_Sit = CDate("01/01/1900")
            Else
                F_Est_Sit = txt_F_Est_Sit.Text
            End If

            If txt_JuntaExt.Text = "" Then
                F_J_A = CDate("01/01/1900")
            Else
                F_J_A = txt_JuntaExt.Text
            End If
            ' no guarda mas  de 1 ves el mismo aval
            If HF_id_Aval.Value = "" Then
                Dim coll As New Collection
                coll = CL.AvalDevueve("000000000000", "999999999999", 0, 9999, 0, 9999, 0, 9999, txt_Rut_Aval.Text, txt_Rut_Aval.Text)
                If coll.Count > 0 Then
                    Msj.Mensaje(Me.Page, caption, "Ya existe este aval para otro cliente", TipoDeMensaje._Informacion, "", True)
                    Exit Sub
                End If

                Dim a As New avl_cls
                'a.id_avl = HF_id_Aval.Value
                a.avl_dig_ito = UCase(txt_Dig_Aval.Text)
                a.avl_dml = UCase(txt_Dir_Paticular.Text)
                a.avl_dml_com = UCase(txt_Dir_Comercial.Text)
                a.avl_fec_est_sit = F_Est_Sit
                a.avl_fec_jun_acc = F_J_A
                a.avl_id_cmn = Drop_Comuna.SelectedValue
                a.avl_id_cmn_com = Drop_Comuna_Comercial.SelectedValue
                a.avl_ida = Format(CLng(txt_Rut_Aval.Text), "000000000000")
                a.avl_mto = mto
                a.avl_nom = UCase(txt_RSocialAval.Text)
                a.avl_not = txt_Notaria.Text
                a.avl_plz = Plz
                a.avl_ptm = txt_Patri.Text
                a.cli_idc = Format(CLng(Txt_Rut_Cli.Text), "000000000000")

                a.id_p_0025 = Drop_Reg_Matri.SelectedValue
                a.id_p_0026 = Drop_TipoAval.SelectedValue
                a.id_p_0027 = Drop_Est_Aval.SelectedValue

                If AL.AvalInserta(a) = True Then
                    Response.Redirect("Avales.aspx", False)
                Else
                    Msj.Mensaje(Me.Page, caption, "No se guardo", TipoDeMensaje._Informacion, "", True)
                End If
                LimpiaControles()
            Else
                Dim a As New avl_cls
                a.id_avl = HF_id_Aval.Value
                a.avl_dml = UCase(txt_Dir_Paticular.Text)
                a.avl_dml_com = UCase(txt_Dir_Comercial.Text)
                a.avl_fec_est_sit = F_Est_Sit
                a.avl_fec_jun_acc = F_J_A
                a.avl_id_cmn = Drop_Comuna.SelectedValue
                a.avl_id_cmn_com = Drop_Comuna_Comercial.SelectedValue
                a.avl_ida = Format(CLng(txt_Rut_Aval.Text), "000000000000")
                a.avl_mto = mto
                a.avl_nom = UCase(txt_RSocialAval.Text)
                a.avl_not = txt_Notaria.Text
                a.avl_plz = Plz
                a.avl_ptm = UCase(txt_Patri.Text)
                a.cli_idc = Format(CLng(Txt_Rut_Cli.Text), "000000000000")
                a.id_p_0025 = Drop_Reg_Matri.SelectedValue
                a.id_p_0026 = Drop_TipoAval.SelectedValue
                a.id_p_0027 = Drop_Est_Aval.SelectedValue
                If AL.AvalModifica(a) = True Then
                    'Msj.Mensaje(me.Page,caption,"Aval modificado",TipoDeMensaje._Informacion,"",True)
                    Response.Redirect("Avales.aspx", False)

                Else
                    Msj.Mensaje(Me.Page, caption, "No se modifico", TipoDeMensaje._Informacion, "", True)
                End If


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LinkB_Elimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Elimina.Click
        Try
            If HF_id_Aval.Value > 0 Then
                AL.EliminaAvales(HF_id_Aval.Value)
                'Msj.Mensaje(Me.Page, caption, "Aval Eliminado", TipoDeMensaje._Exclamacion)
                Response.Redirect("Avales.aspx", False)
                LimpiaControles()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            TraeCliente()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dig_Aval_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Dig_Aval.TextChanged
        Try
            If txt_Dig_Aval.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito verificador aval", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If UCase(txt_Dig_Aval.Text) <> FG.Vrut(CLng(txt_Rut_Aval.Text)) Then
                Msj.Mensaje(Me.Page, caption, "Dígito incorrecto", TipoDeMensaje._Exclamacion)
                txt_Dig_Aval.Text = ""
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_F_Est_Sit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_F_Est_Sit.TextChanged
        Try
            If txt_F_Est_Sit.Text <> "" Then
                If Not IsDate(txt_F_Est_Sit.Text) Then
                    Msj.Mensaje(Page, caption, "Fecha estado situación errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_F_Est_Sit.Text = ""
                    txt_F_Est_Sit.Focus()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_JuntaExt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_JuntaExt.TextChanged
        Try
            If txt_JuntaExt.Text <> "" Then
                If Not IsDate(txt_JuntaExt.Text) Then
                    Msj.Mensaje(Page, caption, "Fecha junta errónea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_JuntaExt.Text = ""
                    txt_JuntaExt.Focus()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Function y Sub"
    Private Sub TraeCliente()
        Try
            Dim CLSCLI As New ClaseClientes
            Dim cli As cli_cls

            cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Sub
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                txt_Rut_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LlenaDrop()
        Try
            'CG.ParametrosDevuelve(84, True, Drop_Ciudad)
            CG.ParametrosDevuelve(25, True, Drop_Reg_Matri)
            CG.ParametrosDevuelve(26, True, Drop_TipoAval)
            CG.ParametrosDevuelve(27, True, Drop_Est_Aval)
            'Drop_Est_Aval.SelectedValue = 1
            CG.CiudadDevuelve(True, Drop_Ciudad)
            CG.CiudadDevuelve(True, Drop_Ciudad_Comercial)
        Catch ex As Exception

        End Try
    End Sub

    Sub LimpiaControles()
        Txt_Dig_Cli.Text = ""
        txt_Dig_Aval.Text = ""
        txt_Dir_Comercial.Text = ""
        txt_Dir_Paticular.Text = ""
        txt_F_Est_Sit.Text = ""
        txt_JuntaExt.Text = ""
        txt_Mto.Text = ""
        txt_Notaria.Text = ""
        txt_Patri.Text = ""
        txt_Plazo.Text = ""
        Txt_Raz_Soc.Text = ""
        txt_RSocialAval.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.ReadOnly = False
        Txt_Dig_Cli.ReadOnly = False
        txt_Dig_Aval.ReadOnly = False
        txt_Rut_Aval.ReadOnly = False


        txt_Rut_Aval.Text = ""

        Drop_Ciudad.ClearSelection()
        Drop_Ciudad_Comercial.ClearSelection()
        'Drop_Comuna.ClearSelection()
        'Drop_Comuna_Comercial.ClearSelection()
        Drop_Comuna.DataSource = ""
        Drop_Comuna.DataBind()

        Drop_Comuna_Comercial.DataSource = ""
        Drop_Comuna_Comercial.DataBind()

        Drop_Est_Aval.ClearSelection()
        Drop_Reg_Matri.ClearSelection()
        Drop_TipoAval.ClearSelection()
        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"
        txt_Rut_Aval.CssClass = "clsMandatorio"
        txt_Dig_Aval.CssClass = "clsMandatorio"
        txt_RSocialAval.ReadOnly = False
        txt_RSocialAval.CssClass = "clsMandatorio"
        txt_Rut_Aval_MaskedEditExtender.Enabled = True
        txt_Rut_MaskedEditExtender.Enabled = True
        IB_AyudaCli.Enabled = True
        HF_Estado.Value = ""
        HF_id_Aval.Value = ""



        Label19.Visible = False
        txt_F_Est_Sit.Visible = False
        Label21.Visible = False
        txt_JuntaExt.Visible = False
        Label18.Visible = False
        txt_Notaria.Visible = False
        Label20.Visible = False
        txt_Mto.Visible = False
        Label22.Visible = False
        txt_Plazo.Visible = False





    End Sub

    Private Sub CargaDatos()
        Try
            LlenaDrop()


            CL.ComunaDevuelveTodas(True, Drop_Comuna)
            CL.ComunaDevuelveTodas(True, Drop_Comuna_Comercial)

            Dim a As New avl_cls
            a = CL.AvlDevuelveObjeto(HF_id_Aval.Value)
            txt_Dig_Aval.Text = a.avl_dig_ito
            txt_Dir_Paticular.Text = a.avl_dml
            txt_Dir_Comercial.Text = a.avl_dml_com
            If a.avl_fec_est_sit <> CDate("01/01/1900") Then
                txt_F_Est_Sit.Text = a.avl_fec_est_sit
            End If

            If a.avl_fec_jun_acc <> CDate("01/01/1900") Then
                txt_JuntaExt.Text = a.avl_fec_jun_acc
            End If

            Drop_Ciudad.SelectedValue = a.cmn_cls.id_ciu

            Drop_Comuna.SelectedValue = a.avl_id_cmn

            Drop_Ciudad_Comercial.SelectedValue = a.cmn_cls.id_ciu


            Drop_Comuna_Comercial.SelectedValue = a.avl_id_cmn_com
            txt_Mto.Text = a.avl_mto
            txt_RSocialAval.Text = a.avl_nom
            txt_Notaria.Text = a.avl_not
            txt_Plazo.Text = a.avl_plz
            txt_Patri.Text = a.avl_ptm

            If a.id_p_0025 > 0 Then
                Drop_Reg_Matri.SelectedValue = a.id_p_0025
            End If

            If a.id_p_0026 > 0 Then
                Drop_TipoAval.SelectedValue = a.id_p_0026
            End If


            If a.id_p_0026 = 2 Then ' Juridico
                Label19.Visible = True
                Label21.Visible = True
                Label18.Visible = True
                Label20.Visible = True
                Label22.Visible = True

                txt_F_Est_Sit.Visible = True
                txt_JuntaExt.Visible = True
                txt_Notaria.Visible = True
                txt_Mto.Visible = True
                txt_Plazo.Visible = True

            End If



            If a.id_p_0027 > 0 Then
                Drop_Est_Aval.SelectedValue = a.id_p_0027
            End If

            Txt_Dig_Cli.Text = a.cli_cls.cli_dig_ito
            Txt_Rut_Cli.Text = Format(CLng(a.cli_idc), FMT.FCMSD)
            txt_Rut_Aval.Text = Format(CLng(a.avl_ida), FMT.FCMSD)

            Txt_Raz_Soc.Text = a.cli_cls.cli_rso & " " & a.cli_cls.cli_ape_ptn

            Txt_Dig_Cli.ReadOnly = True
            txt_Dig_Aval.ReadOnly = True
            Txt_Raz_Soc.ReadOnly = True
            txt_RSocialAval.ReadOnly = True
            txt_RSocialAval.CssClass = "clsDisabled"
            Txt_Rut_Cli.ReadOnly = True
            txt_Rut_Aval.ReadOnly = True
            Txt_Dig_Cli.CssClass = "clsDisabled"
            txt_Dig_Aval.CssClass = "clsDisabled"
            Txt_Raz_Soc.CssClass = "clsDisabled"
            Txt_Rut_Cli.CssClass = "clsDisabled"
            txt_Rut_Aval.CssClass = "clsDisabled"
            txt_Rut_Aval_MaskedEditExtender.Enabled = False
            'txt_Rut_MaskedEditExtender.Enabled = True
            IB_Elimina.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            'RW.ClosePag(Me)
            Response.Redirect("Avales.aspx", False)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            LimpiaControles()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try

            If txt_Rut_Aval.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT aval", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
            If txt_Dig_Aval.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dígito aval", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If txt_RSocialAval.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese razón social aval", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If txt_Dir_Paticular.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dirección particular", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Drop_Ciudad.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione ciudad particular", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
            If Drop_Comuna.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione comuna particular", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If txt_Dir_Comercial.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese dirección comercial", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
            If Drop_Ciudad_Comercial.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione ciudad comercial", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Drop_Comuna_Comercial.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione comuna comercial", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Drop_Reg_Matri.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione regimen matrimonial", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Drop_TipoAval.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione tipo de aval", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Drop_Est_Aval.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, caption, "Seleccione estado de aval", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            Dim CLSCLI As New ClaseClientes
            Dim cli As New cli_cls

            cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

            If sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Sub
            End If


            If HF_id_Aval.Value = "" Then
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, True)
                ' HF_Estado.Value = 1 'Guardar
            Else
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de modificar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Elimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Elimina.Click
        Try
            If HF_id_Aval.Value = "" Then
                Msj.Mensaje(Me, caption, "Seleccione un aval para eliminar", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            Else
                Msj.Mensaje(Me.Page, caption, "¿Esta seguro de eliminar?", TipoDeMensaje._Confirmacion, LinkB_Elimina.UniqueID, True)
            End If
        Catch ex As Exception

        End Try
    End Sub



#End Region

 
    Protected Sub txt_Rut_Aval_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Rut_Aval.TextChanged
        If txt_Rut_Aval.Text = Txt_Rut_Cli.Text Then
            Msj.Mensaje(Me, caption, "El aval no puede ser el cliente", TipoDeMensaje._Exclamacion, "", True)
            txt_Rut_Aval.Text = ""
            txt_Rut_Aval.Focus()

        End If
    End Sub
End Class
