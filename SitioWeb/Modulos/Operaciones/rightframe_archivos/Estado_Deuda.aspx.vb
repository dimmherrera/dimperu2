Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Estado_Deuda
    Inherits System.Web.UI.Page


#Region "Variables"
   
    Dim CG As New ConsultasGenerales
    Dim caption As String = "Estado de Deuda"
    Dim sesion As New ClsSession.SesionOperaciones
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim FC As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim clasecli As New ClaseClientes
    Dim Msj As New ClsMensaje
    Dim sesioncls As New ClsSession.ClsSession
    Dim Agt As New Perfiles.Cls_Principal
    Dim OP As New ClaseOperaciones
#End Region


#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try

            'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
            If Not IsPostBack Then
                Modulo = "Operacion"
                Pagina = Page.AppRelativeVirtualPath
                CambioTema(Page)
            End If

        Catch ex As Exception
            msj.Mensaje(Me.Page, "Error", ex.Message, 1)
        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Expires = -1
            If Not IsPostBack Then
                valida_cliente = ""
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")

                CargaDrop()
                LimpiaControles()
                sesion.Coll_DOC = New Collection
                NroPaginacion = 0
            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Chebox_Suc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chebox_Suc.CheckedChanged
        Try
            If Chebox_Suc.Checked = False Then
                Drop_Suc.Enabled = True
                Drop_Suc.CssClass = "clsMandatorio"
            Else
                Drop_Suc.Enabled = False
                Drop_Suc.CssClass = "clsDisabled"
                Drop_Suc.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Check_Resp_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Resp.CheckedChanged
        Try

            If Check_Resp.Checked = False Then
                Drop_Resp.Enabled = True
                Drop_Resp.CssClass = "clsMandatorio"
            Else
                Drop_Resp.Enabled = False
                Drop_Resp.CssClass = "clsDisabled"
                Drop_Resp.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Check_Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Check_Cli.CheckedChanged
        Try
            If Check_Cli.Checked = True Then
                Txt_Rut_Cli.ReadOnly = False
                Txt_Dig_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCli.Enabled = True
                Txt_Rut_Cli.Focus()
            Else
                Txt_Rut_Cli.ReadOnly = True
                Txt_Dig_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Txt_Raz_Soc.Text = ""
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False
                IB_AyudaCli.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            ValidaRut()


        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub lb_cli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cli.Click
    '    Try
    '        ValidaRut()
    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region

#Region "Function y Sub"
    Public Sub CargaDrop()
        CG.SucursalesDevuelve(codeje, True, Drop_Suc)
    End Sub

    Sub LimpiaControles()
        Drop_Resp.ClearSelection()
        Drop_Suc.ClearSelection()
        IB_Imprimir.Enabled = False
        Drop_Resp.Enabled = False
        Drop_Suc.Enabled = False
        Txt_Dig_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Rut_Cli.Text = ""
        Drop_Resp.CssClass = "clsDisabled"
        Drop_Suc.CssClass = "clsDisabled"
        Chebox_Suc.Checked = True
        Check_Resp.Checked = True
        Check_Cli.Checked = False
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False

        IB_Buscar.Enabled = True
        IB_Imprimir.Enabled = False

        Check_Cli.Enabled = True
        Check_Resp.Enabled = True
        Chebox_Suc.Enabled = True
        sesion.Coll_DOC = New Collection
        IB_AyudaCli.Enabled = False
        Gr_Doc.DataSource = Nothing
        Gr_Doc.DataBind()
    End Sub

    Public Sub FormatoGr_Doc()

        Try

            For i = 1 To sesion.Coll_DOC.Count
                Gr_Doc.Rows(i - 1).Cells(0).Text = Format(CLng(sesion.Coll_DOC.Item(i).cli_idc), FMT.FCMSD) & "-" & FC.Vrut(Gr_Doc.Rows(i - 1).Cells(0).Text)
                Gr_Doc.Rows(i - 1).Cells(2).Text = Format(CLng(sesion.Coll_DOC.Item(i).deu_ide), FMT.FCMSD) & "-" & FC.Vrut(Gr_Doc.Rows(i - 1).Cells(2).Text)
                'Gr_Doc.Rows(i - 1).Cells(5).Text = Format(CLng(sesion.Coll_DOC.Item(i).n_doc), FMT.FCMSD)

                If sesion.Coll_DOC.Item(i).id_P_0023 = 1 Then 'pesos
                    Gr_Doc.Rows(i - 1).Cells(7).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_doc), FMT.FCMSD)
                    Gr_Doc.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_anticipo), FMT.FCMSD)
                    Gr_Doc.Rows(i - 1).Cells(9).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_neto), FMT.FCMSD)
                    Gr_Doc.Rows(i - 1).Cells(10).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_mora), FMT.FCMSD)
                ElseIf sesion.Coll_DOC.Item(i).id_P_0023 = 2 Then ' UF
                    Gr_Doc.Rows(i - 1).Cells(7).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_doc), FMT.FCMCD4)
                    Gr_Doc.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_anticipo), FMT.FCMCD4)
                    Gr_Doc.Rows(i - 1).Cells(9).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_neto), FMT.FCMCD4)
                    Gr_Doc.Rows(i - 1).Cells(10).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_mora), FMT.FCMCD4)

                ElseIf sesion.Coll_DOC.Item(i).id_P_0023 = 3 Or sesion.Coll_DOC.Item(i).id_P_0023 = 4 Then ' dolar y euro
                    Gr_Doc.Rows(i - 1).Cells(7).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_doc), FMT.FCMCD)
                    Gr_Doc.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.Coll_DOC.Item(i).monto_anticipo), FMT.FCMCD)
                    Gr_Doc.Rows(i - 1).Cells(9).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_neto), FMT.FCMCD)
                    Gr_Doc.Rows(i - 1).Cells(10).Text = Format(CLng(sesion.Coll_DOC.Item(i).saldo_mora), FMT.FCMCD)
                End If
                If sesion.Coll_DOC.Item(i).ope_res_son = 1 Then
                    Gr_Doc.Rows(i - 1).Cells(14).Text = "Si"
                Else 'If sesion.Coll_DOC.Item(i).ope_res_son = 0 Then
                    Gr_Doc.Rows(i - 1).Cells(14).Text = "No"
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Sub ValidaRut()
        Try
            Dim FG As New FuncionesGenerales.FComunes


  


            Dim cli As cli_cls
            cli = clasecli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Me.Txt_Dig_Cli.Text)
            Session("Cliente") = cli

            If valida_cliente <> "" Then
                Msj.Mensaje(Me, "Atención", valida_cliente, ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            Else
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Txt_Rut_Cli.Text = ""
                    Txt_Dig_Cli.Text = ""
                    Exit Sub
                End If
                Txt_Rut_Cli.ReadOnly = True
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Txt_Dig_Cli.ReadOnly = True
                Txt_Dig_Cli.CssClass = "clsDisabled"
                Txt_Raz_Soc.ReadOnly = True
                Txt_Raz_Soc.CssClass = "clsDisabled"
                Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
                Check_Cli.Checked = False
                Check_Cli.Enabled = False
                IB_AyudaCli.Enabled = False

            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            LimpiaControles()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Imprimir.Click
        Try

            If Not Agt.ValidaAccesso(20, 20020905, Usr, "PRESIONA BOTON IMPRIMIR") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Dim rut_dsd As String
            Dim rut_hst As String
            Dim resp_dsd As Integer
            Dim resp_hst As Integer
            Dim suc_dsd As Integer
            Dim suc_hst As Integer
            Dim nomsucursal As String
            If Txt_Rut_Cli.Text = "" Then
                rut_dsd = "000000000000"
                rut_hst = "999999999999"
            Else
                rut_dsd = Txt_Rut_Cli.Text
                rut_hst = Txt_Rut_Cli.Text
            End If

            If Drop_Suc.SelectedValue = 0 Then
                suc_dsd = 0
                suc_hst = 999
                nomsucursal = "Todas"
            Else
                suc_dsd = Drop_Suc.SelectedValue
                suc_hst = Drop_Suc.SelectedValue
                nomsucursal = Drop_Suc.SelectedItem.Text
            End If

            If Drop_Resp.SelectedValue = 99 Then
                resp_dsd = 0
                resp_hst = 999
            Else
                resp_dsd = Drop_Resp.SelectedValue
                resp_hst = Drop_Resp.SelectedValue
            End If


            RW.AbrePopup(Me, 1, "Reporte_EstadoDoc.aspx?rutdsd=" & rut_dsd & "&ruthst=" & rut_hst & "&sucdsd=" & suc_dsd _
                         & "&suchst=" & suc_hst & "&respdsd=" & resp_dsd & "&resphst=" & resp_hst & "&NomSucursal=" _
                         & nomsucursal & "&Contador=" & HF_Cont.Value, "Informe_Est_Doc", 1200, 900, 250, 250)



        Catch ex As Exception

        End Try
    End Sub


    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20010905, Usr, "PRESIONA BOTON BUSCAR ESTADO DE DEUDA") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            Dim rut_dsd As String
            Dim rut_hst As String
            Dim resp_dsd As Integer
            Dim resp_hst As Integer
            Dim suc_dsd As Integer
            Dim suc_hst As Integer

            If Check_Cli.Checked Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Page, caption, "Ingrese NIT", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Page, caption, "Ingrese digito", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                ValidaRut()

            End If
            If Chebox_Suc.Checked = False Then
                If Drop_Suc.SelectedValue = 0 Then
                    Msj.Mensaje(Page, caption, "Seleccione sucursal", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If Check_Resp.Checked = False Then
                If Drop_Resp.SelectedValue = 99 Then
                    Msj.Mensaje(Page, caption, "Seleccione C/S Recurso", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If Txt_Rut_Cli.Text = "" Then
                rut_dsd = "000000000000"
                rut_hst = "9999999999999"
            Else
                rut_dsd = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                rut_hst = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
            End If

            If Drop_Resp.SelectedValue = 99 Then
                resp_dsd = 0
                resp_hst = 999
            Else
                resp_dsd = Drop_Resp.SelectedValue
                resp_hst = Drop_Resp.SelectedValue
            End If
            If Drop_Suc.SelectedValue = 0 Then
                suc_dsd = 0
                suc_hst = 999
            Else
                suc_dsd = Drop_Suc.SelectedValue
                suc_hst = Drop_Suc.SelectedValue
            End If
            Dim Coll_Doc As New Collection
            sesion.Coll_DOC = New Collection
            Dim Llena = OP.DocumentosDevuelve(rut_dsd, rut_hst, resp_dsd, resp_hst, suc_dsd, suc_hst)
            For Each L In Llena
                sesion.Coll_DOC.Add(L)
                'Coll_Doc.Add(L)

            Next

            Gr_Doc.DataSource = sesion.Coll_DOC
            Gr_Doc.DataBind()
            If Not IsNothing(sesion.Coll_DOC) Then
                If sesion.Coll_DOC.Count > 0 Then
                    IB_Buscar.Enabled = False
                    IB_Imprimir.Enabled = True
                    Check_Cli.Enabled = False
                    Check_Resp.Enabled = False
                    Chebox_Suc.Enabled = False
                    HF_Cont.Value = sesion.Coll_DOC.Count 'cuenta las filas
                    FormatoGr_Doc()
                End If
                'Else
                '    Msj.Mensaje(Page, caption, "No se encontraron datos", TipoDeMensaje._Exclamacion)
            End If

            If Me.Gr_Doc.Rows.Count = 0 Then
                Msj.Mensaje(Me, "Atención", "No se encontraron datos según criterio de búsqueda", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region



   
    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 15 Then
            NroPaginacion -= 15
            IB_Buscar_Click(Me, e)

        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If Gr_Doc.Rows.Count < 15 Then
            Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If Gr_Doc.Rows.Count = 15 Then
            NroPaginacion += 15
            IB_Buscar_Click(Me, e)

        End If
    End Sub
End Class
