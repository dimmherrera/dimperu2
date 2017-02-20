Imports ClsSession.ClsSession
Imports System.Transactions
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports CapaDatos


Partial Class Dnc
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim caption As String = "Informes"
    Dim CG As New ConsultasGenerales
    Dim CTA As New ClaseCuentas
    Dim FG As New FuncionesGenerales.FComunes
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim sesion As New ClsSession.ClsSession
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim Rut_Dsd As Long
    Dim Rut_Hst As Long
    Dim Modsd As Integer
    Dim mohst As Integer
    Dim Est_dsd As Integer
    Dim Est_hst As Integer
    Dim fdsd As String
    Dim fhst As String
#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Administracion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Response.Expires = -1
            If Not IsPostBack Then
                CargaDrop()
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                Response.Cache.SetNoStore()
                ' sesion.coll_chr = New Collection

                'sesion.coll_CXC = New Collection
                'sesion.coll_DNC = New Collection
                Dim Rut_Dsd As Long = 0
                Dim Rut_Hst As Long = 0
                Dim Modsd As Integer = 0
                Dim mohst As Integer = 0
                Dim Est_dsd As Integer = 0
                Dim Est_hst As Integer = 0
                Dim fdsd As String = ""
                Dim fhst As String = ""
                NroPaginacionCXC = 0

                NroPaginacionDNC = 0
                NroPaginacionCXP = 0
            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Cbx_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cbx_Cli.CheckedChanged
        Try
            If Cbx_Cli.Checked = True Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli.Focus()
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.Text = ""
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                Txt_Raz_Soc.Text = ""
                IB_AyudaCli.Enabled = False

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Drop_Est_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Est.SelectedIndexChanged
        Try
            rb_est.Checked = False
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Drop_Mon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Drop_Mon.SelectedIndexChanged
        Try
            Rb_MonTodas.Checked = False
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub rb_est_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_est.CheckedChanged
        Try
            Drop_Est.ClearSelection()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Rb_MonTodas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rb_MonTodas.CheckedChanged
        Try
            Drop_Mon.ClearSelection()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub LinkCli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkCli.Click
        Try
            ValidadRut()
            Cbx_Cli.Checked = False
            Cbx_Cli.Enabled = False


        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        ValidadRut()
        Cbx_Cli.Checked = False
        Cbx_Cli.Enabled = False

    End Sub
#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Buscar.Click

        Try
            If Not agt.ValidaAccesso(20, 20010501, Usr, "PRESIONO BUSCAR CUENTAS") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            'lleno cxc

            CargaCXC()
            CargaCXP()
            CargaDnc()

            InhabilitaControles()

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click 'Handles IB_Imprimir.Click
        Try
            If Not agt.ValidaAccesso(20, 20020501, Usr, "PRESIONO IMPRIMIR RESULTADO") Then
                Msj.Mensaje(Me.Page, caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim rutdsd As Long
            Dim ruthst As Long
            Dim fdsd As String
            Dim fhst As String
            Dim mondsd As Integer
            Dim monhst As Integer
            Dim estdsd As Integer
            Dim esthst As Integer
            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "000000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Txt_Rut_Cli.Text
                ruthst = Txt_Rut_Cli.Text
            End If

            If txt_Fdsd.Text = "" Then
                'fdsd = FUNFechaJul("01/01/1900")
                fdsd = "1900-01-01"
            Else
                fdsd = FUNFechaJul(txt_Fdsd.Text)
            End If

            If txt_Fhst.Text = "" Then
                'fhst = FUNFechaJul("12/30/2999")
                fhst = "2999-12-30"
            Else
                fhst = FUNFechaJul(txt_Fhst.Text)
            End If

            If Drop_Mon.SelectedValue = 0 Then
                mondsd = 0
                monhst = 999
            Else
                mondsd = Drop_Mon.SelectedValue
                monhst = Drop_Mon.SelectedValue
            End If
            If Drop_Est.SelectedValue = 0 Then
                estdsd = 0
                esthst = 999
            Else
                estdsd = Drop_Est.SelectedValue
                esthst = Drop_Est.SelectedValue
            End If
            Select Case TabContainer1.ActiveTab.HeaderText
                Case "CXC"
                    'If Cbx_Cli.Checked = True And txt_Fdsd.Text <> "" And txt_Fhst.Text <> "" And Rb_MonTodas.Checked = False _
                    ' And rb_est.Checked = False Then
                    If Gr_CXC.Rows.Count = 0 Then
                        Msj.Mensaje(Me.Page, caption, "No existen CXC para imprimir", TipoDeMensaje._Exclamacion, "", True)
                        'TabContainer1.Tabpanel = "CXC"
                        Exit Sub
                    Else
                        IB_Imprimir.Enabled = True
                    End If
                    RW.AbrePopup(Me, 1, "InformeCXC.aspx?rut_dsd=" & rutdsd & "&rut_hst=" & ruthst _
                                       & "&Fecha_dsd=" & fdsd _
                                       & "&Fecha_hst=" & fhst & "&Moneda_dsd=" & mondsd _
                                       & "&Moneda_hst=" & monhst & "&Est_dsd=" & estdsd _
                                       & "&Est_hst=" & esthst & "&numope=" & 0 _
                                       & "&numdoc=" & 0 & "&tpct=" & 5 & "&OtraTPct=" & 0 & "&Informe=" & 2, "CXC", 1300, 900, 10, 10)

                Case "CXP"
                    If Gr_CXP.Rows.Count = 0 Then
                        Msj.Mensaje(Me.Page, caption, "No existen CXP para imprimir", TipoDeMensaje._Exclamacion, "", True)
                        'TabContainer1.Tabpanel = "CXC"
                        Exit Sub
                    Else
                        IB_Imprimir.Enabled = True
                    End If
                    RW.AbrePopup(Me, 1, "InformeCXP.aspx?rut_dsd=" & rutdsd & "&rut_hst=" & ruthst _
                                       & "&Fecha_dsd=" & fdsd _
                                       & "&Fecha_hst=" & fhst & "&Moneda_dsd=" & mondsd _
                                       & "&Moneda_hst=" & monhst & "&Est_dsd=" & estdsd _
                                       & "&Est_hst=" & esthst & "&numope=" & 0 _
                                       & "&numdoc=" & 0 & "&tpct=" & 5 & "&OtraTPct=" & 0 & "&Informe=" & 2, "CXC", 1300, 900, 10, 10)
                Case "DNC"
                    If Gr_dnc.Rows.Count = 0 Then
                        Msj.Mensaje(Me.Page, caption, "No existen DNC para imprimir", TipoDeMensaje._Exclamacion, "", True)
                        'TabContainer1.Tabpanel = "CXC"
                        Exit Sub
                    Else
                        IB_Imprimir.Enabled = True
                    End If
                    RW.AbrePopup(Me, 1, "InformeDNC.aspx?rut_dsd=" & rutdsd & "&rut_hst=" & ruthst & "&Fecha_dsd=" & fdsd _
                                 & "&Fecha_hst=" & fhst & "&Moneda_dsd=" & mondsd & "&Moneda_hst=" & monhst, "InformeDNC", 1300, 900, 10, 10)

            End Select
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Limpiar.Click
        Try
            limpiar()


        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        Select Case TabContainer1.ActiveTab.HeaderText
            Case "CXC"
                If NroPaginacionCXC >= 12 Then
                    NroPaginacionCXC -= 12
                    CargaCXC()
                End If

            Case "CXP"

                If NroPaginacionCXP >= 12 Then
                    NroPaginacionCXP -= 12
                    CargaCXP()
                End If

            Case "DNC"
                If NroPaginacionDNC >= 12 Then
                    NroPaginacionDNC -= 12
                    CargaDnc()
                End If

        End Select


    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Select Case TabContainer1.ActiveTab.HeaderText
            Case "CXC"
                If Gr_CXC.Rows.Count = 12 Then
                    NroPaginacionCXC += 12
                    CargaCXC()
                End If
            Case "CXP"
                If Gr_CXP.Rows.Count = 12 Then
                    NroPaginacionCXP += 12
                    CargaCXP()
                End If

            Case "DNC"
                If Gr_dnc.Rows.Count = 12 Then
                    NroPaginacionDNC += 12
                    CargaDnc()
                End If


        End Select


    End Sub
#End Region

#Region "Sub y Function"

    Sub CargaDrop()
        Try

            CG.ParametrosDevuelve(23, True, Drop_Mon)
            CG.ParametrosDevuelve(86, True, Drop_Est)

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Sub limpiar()
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Raz_Soc.ReadOnly = True
        Txt_Raz_Soc.CssClass = "clsDisabled"

        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        txt_Fdsd.Text = ""
        txt_Fhst.Text = ""


        Drop_Est.ClearSelection()
        Drop_Mon.ClearSelection()

        rb_est.Checked = True
        Rb_MonTodas.Checked = True
        IB_Imprimir.Enabled = False
        Cbx_Cli.Checked = False
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False

        Cbx_Cli.Enabled = True
        txt_Fdsd.ReadOnly = False
        txt_Fhst.ReadOnly = False
        Rb_MonTodas.Enabled = True
        rb_est.Enabled = True
        Drop_Est.Enabled = True
        Drop_Mon.Enabled = True
        IB_Buscar.Enabled = True
        txt_Fdsd_CalendarExtender.Enabled = True
        txt_Fhst_CalendarExtender.Enabled = True

        txt_Fdsd_CalendarExtender.CssClass = "radcalendar"
        txt_Fhst_CalendarExtender.CssClass = "radcalendar"

        txt_Fdsd.CssClass = "clsTxt"
        txt_Fhst.CssClass = "clsTxt"
        Drop_Est.CssClass = "clsTxt"
        Drop_Mon.CssClass = "clsTxt"

        Gr_CXC.DataSource = Nothing
        Gr_CXC.DataBind()

        Gr_CXP.DataSource = Nothing
        Gr_CXP.DataBind()

        Gr_dnc.DataSource = Nothing
        Gr_dnc.DataBind()
        txt_Fdsd_MaskedEditExtender.Enabled = True
        txt_Fhst_MaskedEditExtender.Enabled = True
        Cbx_Cli.Enabled = True

    End Sub

    Private Sub ValidadRut()
        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Me.Page, caption, "Ingrese Dìgito Verificador", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
            If Txt_Dig_Cli.Text.ToUpper <> FG.Vrut(CLng(Txt_Rut_Cli.Text)).ToUpper Then
                Msj.Mensaje(Me.Page, caption, "Dìgito Incorrecto", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If


            Dim cli As cli_cls
            Dim CLSCLI As New ClaseClientes

            cli = CLSCLI.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), UCase(Txt_Dig_Cli.Text))

            If sesion.valida_cliente <> "" Then
                IB_AyudaCli.Enabled = True
                Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                'Asigna Razon Social / Nombre a Campo Cliente
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                IB_AyudaCli.Enabled = False

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Public Sub InhabilitaControles()
        Try

            IB_AyudaCli.Enabled = False
            Cbx_Cli.Enabled = False
            txt_Fdsd.ReadOnly = True
            txt_Fhst.ReadOnly = True
            Rb_MonTodas.Enabled = False
            rb_est.Enabled = False
            Drop_Est.Enabled = False
            Drop_Mon.Enabled = False
            IB_Buscar.Enabled = False
            txt_Fdsd_CalendarExtender.Enabled = False
            txt_Fhst_CalendarExtender.Enabled = False
            txt_Fdsd.CssClass = "clsDisabled"
            txt_Fhst.CssClass = "clsDisabled"
            Drop_Est.CssClass = "clsDisabled"
            Drop_Mon.CssClass = "clsDisabled"
            txt_Fdsd_MaskedEditExtender.Enabled = False
            txt_Fhst_MaskedEditExtender.Enabled = False

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CargaCXC()
        Try

            If Valida() = True Then
                Exit Sub
            End If


            If Cbx_Cli.Checked = True Then
                ValidadRut()
            End If
            coll_CXC = CTA.CuentasPorCobrarDevuelve(Rut_Dsd, rut_hst, 0, 999, Est_dsd, Est_hst, 0, 999, 0, 999, Modsd, mohst, fdsd, fhst)

            Gr_CXC.DataSource = coll_CXC
            Gr_CXC.DataBind()

            For i = 1 To coll_CXC.Count
                If coll_CXC.Item(i).id_p_0023 = 1 Then 'pesos
                    Gr_CXC.Rows(i - 1).Cells(0).Text = Format(CLng(coll_CXC.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXC.Rows(i - 1).Cells(0).Text)
                    Gr_CXC.Rows(i - 1).Cells(9).Text = Format(CLng(coll_CXC.Item(i).cxc_mto), FMT.FCMSD)
                    Gr_CXC.Rows(i - 1).Cells(10).Text = Format(CLng(coll_CXC.Item(i).cxc_sal), FMT.FCMSD)
                ElseIf coll_CXC.Item(i).id_p_0023 = 2 Then 'Uf
                    Gr_CXC.Rows(i - 1).Cells(0).Text = Format(CLng(coll_CXC.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXC.Rows(i - 1).Cells(0).Text)
                    Gr_CXC.Rows(i - 1).Cells(9).Text = Format(CDbl(coll_CXC.Item(i).cxc_mto), FMT.FCMCD4)
                    Gr_CXC.Rows(i - 1).Cells(10).Text = Format(CDbl(coll_CXC.Item(i).cxc_sal), FMT.FCMCD4)
                ElseIf coll_CXC.Item(i).id_p_0023 = 3 Or coll_CXC.Item(i).id_p_0023 = 4 Then ' dolar o euro
                    Gr_CXC.Rows(i - 1).Cells(0).Text = Format(CDbl(coll_CXC.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXC.Rows(i - 1).Cells(0).Text)
                    Gr_CXC.Rows(i - 1).Cells(9).Text = Format(CDbl(coll_CXC.Item(i).cxc_mto), FMT.FCMCD)
                    Gr_CXC.Rows(i - 1).Cells(10).Text = Format(CDbl(coll_CXC.Item(i).cxc_sal), FMT.FCMCD)
                End If

            Next

            If coll_CXC.Count = 0 Then
                Msj.Mensaje(Me.Page, caption, "No existen CXC para criterio", TipoDeMensaje._Exclamacion, "", True)
                'TabContainer1.Tabpanel = "CXC"
            Else
                IB_Imprimir.Enabled = True
            End If


        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


    Private Sub CargaCXP()
        Try
            Dim coll_CXP As New Collection



            If Valida() = True Then
                Exit Sub
            End If

            'lleno cxp
            coll_CXP = CTA.CuentasPorPagarDevuelve(Rut_Dsd, rut_hst, 0, 999, Est_dsd, Est_hst, 0, 999, 0, 999, Modsd, mohst, fdsd, fhst)

            Gr_CXP.DataSource = coll_CXP
            Gr_CXP.DataBind()

            For i = 1 To coll_CXP.Count
                If coll_CXP.Item(i).id_p_0023 = 1 Then 'pesos
                    Gr_CXP.Rows(i - 1).Cells(0).Text = Format(CLng(coll_CXP.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXP.Rows(i - 1).Cells(0).Text)
                    Gr_CXP.Rows(i - 1).Cells(9).Text = Format(CLng(coll_CXP.Item(i).cxp_mto), FMT.FCMSD)

                ElseIf coll_CXP.Item(i).id_p_0023 = 2 Then 'Uf
                    Gr_CXP.Rows(i - 1).Cells(0).Text = Format(CLng(coll_CXP.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXP.Rows(i - 1).Cells(0).Text)
                    Gr_CXP.Rows(i - 1).Cells(9).Text = Format(CLng(coll_CXP.Item(i).cxp_mto), FMT.FCMCD4)

                ElseIf coll_CXP.Item(i).id_p_0023 = 3 Or coll_CXP.Item(i).id_p_0023 = 4 Then ' dolar o euro
                    Gr_CXP.Rows(i - 1).Cells(0).Text = Format(CLng(coll_CXP.Item(i).cli_idc), FMT.FCMSD) & "-" & FG.Vrut(Gr_CXP.Rows(i - 1).Cells(0).Text)
                    Gr_CXP.Rows(i - 1).Cells(9).Text = Format(CLng(coll_CXP.Item(i).cxp_mto), FMT.FCMCD)

                End If

            Next
            If coll_CXP.Count = 0 Then
                Msj.Mensaje(Me.Page, caption, "No existen CXP para criterio", TipoDeMensaje._Exclamacion, "", True)
            Else
                IB_Imprimir.Enabled = True
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CargaDnc()
        Try
            'Dim Rut_Dsd As Long
            'Dim Rut_Hst As Long
            'Dim Modsd As Integer
            'Dim mohst As Integer
            'Dim Est_dsd As Integer
            'Dim Est_hst As Integer
            'Dim fdsd As String
            'Dim fhst As String

            If Valida() = True Then
                Exit Sub
            End If

            'Lleno dnc
            coll_DNC = CTA.DocumentosNOCedidosdevuelveTodos(Rut_Dsd, rut_hst, Modsd, mohst, fdsd, fhst)
            Gr_dnc.DataSource = coll_DNC
            Gr_dnc.DataBind()
            For i = 1 To coll_DNC.Count
                If coll_DNC.Item(i).id_p_0023 = 1 Then 'pesos
                    Gr_dnc.Rows(i - 1).Cells(0).Text = Format(CLng(coll_DNC.Item(i).RutCli), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(0).Text)
                    Gr_dnc.Rows(i - 1).Cells(3).Text = Format(CLng(coll_DNC.Item(i).RutDeudor), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(3).Text)
                    Gr_dnc.Rows(i - 1).Cells(8).Text = Format(CLng(coll_DNC.Item(i).nce_mto), FMT.FCMSD)
                ElseIf coll_DNC.Item(i).id_p_0023 = 2 Then 'Uf
                    Gr_dnc.Rows(i - 1).Cells(0).Text = Format(CLng(coll_DNC.Item(i).RutCli), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(0).Text)
                    Gr_dnc.Rows(i - 1).Cells(3).Text = Format(CLng(coll_DNC.Item(i).RutDeudor), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(3).Text)
                    Gr_dnc.Rows(i - 1).Cells(8).Text = Format(CLng(coll_DNC.Item(i).nce_mto), FMT.FCMCD4)
                ElseIf coll_DNC.Item(i).id_p_0023 = 3 Or coll_DNC.Item(i).id_p_0023 = 4 Then ' dolar o euro
                    Gr_dnc.Rows(i - 1).Cells(0).Text = Format(CLng(coll_DNC.Item(i).RutCli), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(0).Text)
                    Gr_dnc.Rows(i - 1).Cells(3).Text = Format(CLng(coll_DNC.Item(i).RutDeudor), FMT.FCMSD) & "-" & FG.Vrut(Gr_dnc.Rows(i - 1).Cells(3).Text)
                    Gr_dnc.Rows(i - 1).Cells(8).Text = Format(CLng(coll_DNC.Item(i).nce_mto), FMT.FCMCD)
                End If
            Next
            If coll_DNC.Count = 0 Then
                Msj.Mensaje(Me.Page, caption, "No existen DNC para criterio", TipoDeMensaje._Exclamacion, "", True)
            Else
                IB_Imprimir.Enabled = True
            End If

            InhabilitaControles()
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Function Valida() As Boolean

        Try
            If Txt_Rut_Cli.Text = "" Then
                Rut_Dsd = "000000000000"
                Rut_Hst = "999999999999"
            Else
                Rut_Dsd = Txt_Rut_Cli.Text
                rut_hst = Txt_Rut_Cli.Text
            End If

            If Drop_Mon.SelectedValue = 0 Then
                Modsd = "0"
                mohst = "999"
            Else
                Modsd = Drop_Mon.SelectedValue
                mohst = Drop_Mon.SelectedValue
            End If

            If Drop_Est.SelectedValue = 0 Then
                Est_dsd = "0"
                Est_hst = "999"
            Else
                Est_dsd = Drop_Est.SelectedValue
                Est_hst = Drop_Est.SelectedValue
            End If

            If txt_Fdsd.Text = "" Then
                fdsd = "01/01/1900"
                'fhst = "12/12/2999"
            Else
                fdsd = txt_Fdsd.Text
            End If
            If txt_Fhst.Text = "" Then
                fhst = "30/12/2999"
            Else
                fhst = txt_Fhst.Text
            End If

            If txt_Fdsd.Text <> "" Then
                If Not IsDate(txt_Fdsd.Text) Then
                    Msj.Mensaje(Page, caption, "Fecha desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_Fdsd.Text = ""
                    Return True
                    Exit Function
                End If
                If CDate(txt_Fdsd.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, caption, "Fecha desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_Fdsd.Text = ""
                    Return True
                    Exit Function
                End If

            End If

            If txt_Fhst.Text <> "" Then
                If Not IsDate(txt_Fhst.Text) Then
                    Msj.Mensaje(Page, caption, "Fecha hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_Fhst.Text = ""
                    Return True
                    Exit Function
                End If
                If CDate(txt_Fhst.Text) > "31/12/2999" Then
                    Msj.Mensaje(Page, caption, "Fecha hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_Fhst.Text = ""
                    Return True
                    Exit Function
                End If
            End If

            If CDate(fdsd) > CDate(fhst) Then
                Msj.Mensaje(Me.Page, caption, "Fecha desde no puede ser mayor a fecha hasta", TipoDeMensaje._Exclamacion, "", True)
                txt_Fdsd.Text = ""
                txt_Fhst.Text = ""
                Return True
                Exit Function
            End If
            If Cbx_Cli.Checked = True Then


                ValidadRut()

            End If

            Return False

        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Function

#End Region




  
End Class
