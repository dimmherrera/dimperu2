Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_Ingreso_Cheques
    Inherits System.Web.UI.Page

    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim rutinas As New FuncionesGenerales.RutinasWeb
    Dim Cuo As Integer
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim var As New FuncionesGenerales.Variables
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)


        Dim cw As New FuncionesGenerales.FComunes
        Dim col As New Collection




        Dim ING As Boolean
        If Me.RB_Cli_deu.SelectedValue = "D" Then
            If Me.dr_deu.SelectedValue = 0 Then

                msj.Mensaje(Me, "Exclamación", "Debe seleccionar un Pagador", 2, , False)
                Exit Sub
            End If
        End If

        If Me.Dr_bco.SelectedValue = 0 Then
            msj.Mensaje(Me, "Exclamación", "Debe seleccionar un Banco", 2, , False)
            Exit Sub
        End If

        'If Me.Dr_plaza.SelectedValue = "Seleccionar" Then
        '    msj.Mensaje(Me, "Exclamación", "Debe seleccionar una Plaza", 2, , False)
        '    Exit Sub
        'End If

        If HF_IdPlaza.Value = "" Then
            msj.Mensaje(Me, "Exclamación", "Debe seleccionar una Plaza", 2, , False)
            Exit Sub
        End If


        If Me.cta_cte.Text = "" Then
            msj.Mensaje(Me, "Exclamación", "Debe ingresar la cuenta corriente", 2, , False)
            Exit Sub

        End If

        If Txt_FecVcto.Text = "" Then
            msj.Mensaje(Me, "Exclamación", "Debe ingresar fecha de vencimiento", 2, , False)
            Exit Sub
        End If

        If Not IsDate(Txt_FecVcto.Text) Then
            msj.Mensaje(Me, "Exclamación", "Fecha erronea", 2, , False)
            Txt_FecVcto.Text = ""
            Txt_FecVcto.Focus()
            Exit Sub
        End If

        If CDate(Txt_FecVcto.Text) > "31/12/2100" Then
            msj.Mensaje(Me, "Atencion", "Fecha Erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
            Txt_FecVcto.Text = ""
            Txt_FecVcto.Focus()
            Exit Sub
        End If


        col = CMC.DiasDeRetencionDevuelve(coll_ope.Item(Val(Me.txt_itemope.Value)).id_suc, HF_IdPlaza.Value, 4)

        If col.Count > 0 Then
            fev_rea.Value = CDate(Me.Txt_FecVcto.Text).AddDays(col.Item(1))
        Else
            fev_rea.Value = CDate(Me.Txt_FecVcto.Text)
        End If


        If Me.RB_Cli_deu.SelectedValue = "C" Then

            Dim chr As New chr_cls

            With chr
                .id_bco = Me.Dr_bco.SelectedValue
                .id_ope = coll_ope.Item(Val(Me.txt_itemope.Value)).id_ope
                '.id_PL_000047 = Me.Dr_plaza.SelectedItem.Text

                .id_PL_000047 = HF_IdPlaza.Value

                If Me.RB_CUST.SelectedValue = "S" Then
                    .id_P_0112 = Me.dr_custodia.SelectedValue
                Else
                    .id_P_0112 = Nothing
                End If

                .cta_cte = Me.cta_cte.Text
                .chr_num = Me.Txt_NroDocto.Text
                .chr_mto = Me.Txt_MontoFinanciar0.Text
                .chr_fev = Me.Txt_FecVcto.Text
                .chr_cli_deu = Format(CLng(Me.Txt_Rut_Deu.Text), var.FMT_RUT)
                .chr_tip_cli = Me.RB_Cli_deu.SelectedValue
                .id_P_0113 = 1
                .chr_fev_rea = CDate(fev_rea.Value)
                .chr_tip = "R"
            End With

            ING = OP.cheques_respaldo_Ingresa(chr)
            If ING Then
                msj.Mensaje(Me, "Información", "Registro Ingresado", 2, "", False)
                HabilitaInhabilitaDatosDeudor("H")
                HabilitaInhabilitaDatosDocumento("H")
                Exit Sub
            End If

        ElseIf Me.RB_Cli_deu.SelectedValue = "D" Then

            Dim chr As New chr_cls

            With chr
                .id_bco = Me.Dr_bco.SelectedValue
                .id_ope = coll_ope.Item(Val(Me.txt_itemope.Value)).id_ope
                .id_PL_000047 = HF_IdPlaza.Value

                If Me.RB_CUST.SelectedValue = "S" Then
                    .id_P_0112 = Me.dr_custodia.SelectedValue
                Else
                    .id_P_0112 = Nothing
                End If

                .cta_cte = Me.cta_cte.Text
                .chr_num = Me.Txt_NroDocto.Text
                .chr_mto = Me.Txt_MontoFinanciar0.Text
                .chr_fev = Me.Txt_FecVcto.Text
                .chr_cli_deu = Format(CLng(Me.Txt_Rut_Deu0.Text), var.FMT_RUT)
                .chr_tip_cli = Me.RB_Cli_deu.SelectedValue
                .id_P_0113 = 1
                .chr_fev_rea = CDate(fev_rea.Value)
                .chr_tip = "R"
            End With

            ING = OP.cheques_respaldo_Ingresa(chr)

            If ING Then
                msj.Mensaje(Me, "Información", "Registro Ingresado", 2, , False)
                HabilitaInhabilitaDatosDeudor("H")

                HabilitaInhabilitaDatosDocumento("I")
                Exit Sub
            End If


        End If




    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Response.Expires = -1
            alinea_textos()
            Me.Txt_Rut_Deu.Focus()

            'CG.ParametrosAlfanumericoDevuelveCodigos(TablaAlfanumerico.Plazas, True, Me.Dr_plaza)
            Me.txt_itemope.Value = Request.QueryString("itemOPE") 'coll_ope.Item(Request.QueryString("itemOPE")).id_ope

            'Bancos
            CG.BancosDevuelveTodos(Dr_bco)

            'Custodia
            CG.ParametrosDevuelve(TablaParametro.Custodia, True, dr_custodia)

            Me.RB_Cli_deu_SelectedIndexChanged(Me, e)

        End If

        btn_volver.Attributes.Add("onClick", "javascript:window.close();")
    End Sub

    Public Sub alinea_textos()
        Me.Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Rut_Deu0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_Dig_Deu0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_NroDocto.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.Txt_MontoFinanciar0.Attributes.Add("Style", "TEXT-ALIGN: right")
        Me.cta_cte.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Private Sub HabilitaInhabilitaDatosDocumento(ByVal HI As String)

        If HI = "I" Then
            'Textos

            Me.Txt_MontoFinanciar0.Text = ""
            Me.Txt_MontoFinanciar0.CssClass = "clsDisabled"
            Me.Txt_MontoFinanciar0.ReadOnly = True

            Me.Txt_NroDocto.Text = ""
            Me.Txt_NroDocto.CssClass = "clsDisabled"
            Me.Txt_NroDocto.ReadOnly = True

            'Me.Dr_plaza.CssClass = "clsDisabled"
            'Me.Dr_plaza.Enabled = False

            Me.Txt_FecVcto.Text = ""
            Me.Txt_FecVcto.CssClass = "clsDisabled"
            Me.Txt_FecVcto.ReadOnly = True

            cta_cte.Text = ""
            Me.Txt_PlazaDes.Text = ""
            Me.Txt_PlazaDes.CssClass = "clsDisabled"
            Me.Txt_PlazaDes.ReadOnly = True

            'RadioButton

            Me.Dr_bco.Enabled = False
            Me.Dr_bco.CssClass = "clsDisabled"

            'Me.dr_custodia.Enabled = False
            'Me.dr_custodia.CssClass = "clsDisabled"
        Else
            'Textos
            Me.Txt_NroDocto.Text = ""
            Me.Txt_NroDocto.CssClass = "clsMandatorio"
            Me.Txt_NroDocto.ReadOnly = False

            Me.Txt_MontoFinanciar0.Text = ""
            Me.Txt_MontoFinanciar0.CssClass = "clsMandatorio"
            Me.Txt_MontoFinanciar0.ReadOnly = False


            Me.Txt_FecVcto.Text = ""
            Me.Txt_FecVcto.CssClass = "clsMandatorio"
            Me.Txt_FecVcto.ReadOnly = False

            Me.Txt_PlazaDes.Text = ""
            Me.Txt_PlazaDes.CssClass = "clsDisabled"
            Me.Txt_PlazaDes.ReadOnly = True


            Me.Dr_bco.ClearSelection()
            cta_cte.Text = ""
            dr_custodia.ClearSelection()

        End If

    End Sub

    Private Sub HabilitaInhabilitaDatosDeudor(ByVal HI As String)

        If HI = "I" Then
            '  Me.txt_raz_soc.Text = ""
            ' Me.Txt_Rut_Deu.Text = ""
            Me.Txt_Rut_Deu.CssClass = "clsDisabled"
            Me.Txt_Rut_Deu.ReadOnly = True
            Me.Txt_Dig_Deu.Text = ""
            Me.Txt_Dig_Deu.CssClass = "clsDisabled"
            Me.Txt_Dig_Deu.ReadOnly = True
            '    Me.txt_raz_soc.Text = ""
            Me.txt_raz_soc.CssClass = "clsDisabled"
            Me.txt_raz_soc.ReadOnly = True


        Else
            'Textos
            If Me.RB_Cli_deu.SelectedValue = "D" Then
                Me.txt_raz_soc.Text = ""

                Me.Txt_Rut_Deu.Text = ""
                Me.Txt_Rut_Deu.CssClass = "clsMandatorio"
                Me.Txt_Rut_Deu.ReadOnly = False

                Me.Txt_Dig_Deu.Text = ""
                Me.Txt_Dig_Deu.CssClass = "clsMandatorio"
                Me.Txt_Dig_Deu.ReadOnly = False

                Me.txt_raz_soc.Text = ""

            End If



        End If
    End Sub

    Protected Sub Txt_PlazaDes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_PlazaDes.TextChanged
        Try
            Dim cw As New FuncionesGenerales.FComunes
            Dim col As New Collection


            col = CMC.DiasDeRetencionDevuelve(coll_ope.Item(Val(Me.txt_itemope.Value)).id_suc, HF_IdPlaza.Value, 4)

            If col.Count > 0 Then
                fev_rea.Value = CDate(Me.Txt_FecVcto.Text).AddDays(col.Item(1))
            Else
                fev_rea.Value = CDate(Me.Txt_FecVcto.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Cli_deu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Cli_deu.SelectedIndexChanged

        If Me.RB_Cli_deu.SelectedValue = "C" Then

            Me.Txt_Rut_Deu0.Visible = False
            Me.dr_deu.Visible = False
            Me.Txt_Dig_Deu0.Visible = False
            Me.Txt_Rut_Deu.Visible = True
            Me.txt_raz_soc.Visible = True
            Me.Txt_Dig_Deu.Visible = True
            Me.Txt_Rut_Deu0.Text = ""
            Me.Txt_Dig_Deu0.Text = ""
            Dim CLI As New Collection

            CLI = OP.Clientes_Deudores_retorna_por_Operación(ID_OPE_RPT, Me.RB_Cli_deu.SelectedValue)

            With CLI.Item(1)
                Me.Txt_Rut_Deu.Text = CDbl(.cli_idc)
                Me.txt_raz_soc.Text = .cli_rso
                Me.Txt_Dig_Deu.Text = RG.Vrut(.cli_idc)


            End With
            Me.Txt_NroDocto.Focus()

        Else

            Me.Txt_Rut_Deu0.Visible = True
            Me.dr_deu.Visible = True
            Me.Txt_Dig_Deu0.Visible = True
            Me.Txt_Rut_Deu.Visible = False
            Me.txt_raz_soc.Visible = False
            Me.Txt_Dig_Deu.Visible = False

            OP.Clientes_Deudores_retorna_por_Operación(ID_OPE_RPT, Me.RB_Cli_deu.SelectedValue, dr_deu, True)
            Me.dr_deu.Focus()
            Me.Txt_NroDocto.Focus()

        End If
    End Sub

    Protected Sub dr_deu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_deu.SelectedIndexChanged
        If Me.dr_deu.SelectedValue <> 0 Then

            Me.Txt_Dig_Deu0.Text = RG.Vrut(Me.dr_deu.SelectedValue)
            Me.Txt_Rut_Deu0.Text = CInt(Me.dr_deu.SelectedValue)
            Me.Txt_NroDocto.Focus()
        End If
    End Sub

    Protected Sub RB_CUST_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_CUST.SelectedIndexChanged
        If Me.RB_CUST.SelectedValue = "S" Then

            Me.dr_custodia.Enabled = True
            Me.dr_custodia.CssClass = "clsMandatorio"
        ElseIf Me.RB_CUST.SelectedValue = "N" Then

            Me.dr_custodia.Enabled = False
            Me.dr_custodia.CssClass = "clsDisabled"
        End If

    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click
        HabilitaInhabilitaDatosDeudor("H")
        HabilitaInhabilitaDatosDocumento("I")
    End Sub

    Protected Sub IB_Plaza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Plaza.Click
        Try
            rutinas.AbrePopup(Me, 2, "../../Ayudas/AyudaPlaza.aspx", "Plazas", 500, 400, 100, 100)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_NroDocto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_NroDocto.TextChanged
        Me.Txt_NroDocto.Text = Format(CDbl(Txt_NroDocto.Text), fmt.FCMSD)
    End Sub

End Class
