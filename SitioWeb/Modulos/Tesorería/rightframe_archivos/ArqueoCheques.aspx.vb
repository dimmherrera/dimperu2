Imports ClsSession.ClsSession
Imports FuncionesGenerales.FComunes
Imports System.Transactions
Imports ClsSession.SesionProrrogas
Imports ClsSession.SesionPagos
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Partial Class ArqueoCheques
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim Caption As String = "Arqueo de Cheques"
    Dim CG As New ConsultasGenerales
    Dim SesionPagos As New ClsSession.SesionPagos
    Dim sesion As ClsSession.ClsSession
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim FG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim var As New FuncionesGenerales.Variables
    Dim Msj As New ClsMensaje
    Dim TSR As New ClaseTesoreria
#End Region


#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaDrop()
                IB_Imprimir.Enabled = False
                sesion.coll_chr = New Collection
                NroPaginacion = 0

                Txt_Rut_Cli.Attributes.Add("style", "text-align:right")

            End If

            IB_AyudaCli.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','PopUpCliente',650,410,200,150);")

        Catch ex As Exception
            ' Msj(Caption,ex.Message,TipoDeMensaje._Error)
        End Try
    End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then

            Modulo = "Tesoreria"

            'Esto de abajo es para los skins
            Pagina = Page.AppRelativeVirtualPath

            CambioTema(Page)

        End If
    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            If Txt_Rut_Cli.Text <> "" Then
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text <> FG.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                    Msj.Mensaje(Page, Caption, "Dígito incorrecto", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If


                Dim cliente As New ClaseClientes

                Dim cli As cli_cls
                cli = cliente.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)
                Session("Cliente") = cli
                If Not IsNothing(cli) Then
                    If cli.id_P_0044 <> 1 Then
                        Txt_Raz_Soc.Text = cli.cli_rso
                    Else
                        Txt_Raz_Soc.Text = cli.cli_rso & " " & cli.cli_ape_ptn & " " & cli.cli_ape_mtn
                    End If
                    Txt_Dig_Cli.ReadOnly = True
                    Txt_Rut_Cli.ReadOnly = True
                    Txt_Dig_Cli.CssClass = "clsDisabled"
                    Txt_Rut_Cli.CssClass = "clsDisabled"

                    IB_AyudaCli.Enabled = False

                Else
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    '    *************************************************************************************************************************************************************
    'funcion java que de momento no se esta utilisando ya que a un no es necesario mostrar la imagen de un cheque

    'Protected Sub GR_Busqueda_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GR_Busqueda.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GR_Busqueda, 'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GR_Busqueda, 'formatable')")
    '        e.Row.Attributes.Add("onClick", "Click_GV_VB(ctl00_ContentPlaceHolder1_GR_Busqueda, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub

    ' levanta un pop un con la vista de un cheque. (por el momento no hay necesidad de mostrar la imagen del cheque)
    'Protected Sub lb_cheque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_cheque.Click
    '    RW.AbrePopup(Me, 2, "vista_cheque.aspx", "ObservacionVB", 650, 700, 100, 100)
    'End Sub
    '    *************************************************************************************************************************************************************
#End Region

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            Limpia()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            Dim Est_dsd As Integer
            Dim Est_Hst As Integer
            Dim Tip_Chr_Dsd As String
            Dim Tip_Chr_Hst As String


            sesion.coll_chr = New Collection
            GR_Busqueda.DataSource = Nothing
            GR_Busqueda.DataBind()

            If Txt_Rut_Cli.Text <> "" Then
                If Txt_Dig_Cli.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese dígito verificador", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
                If Txt_Dig_Cli.Text <> FG.Vrut(CLng(Txt_Rut_Cli.Text)) Then
                    Msj.Mensaje(Page, Caption, "Dígito incorrecto", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

                Dim cliente As New ClaseClientes
                Dim cli As cli_cls
                cli = cliente.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(",", ""), Txt_Dig_Cli.Text)
                Session("Cliente") = cli
                If IsNothing(cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no encontrado", TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If

            End If

            If txt_FDesde.Text <> "" And txtFHasta.Text <> "" Then
                If Not IsDate(txt_FDesde.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha desde erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txt_FDesde.Text = ""
                    Exit Sub
                End If

                If Not IsDate(txtFHasta.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha hasta erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                    txtFHasta.Text = ""
                    Exit Sub
                End If

                If CDate(txt_FDesde.Text) > CDate(txtFHasta.Text) Then
                    Msj.Mensaje(Page, Caption, "Fecha desde no puede ser mayor a fecha hasta", TipoDeMensaje._Exclamacion)
                    txt_FDesde.Text = ""
                    txtFHasta.Text = ""
                    Exit Sub
                End If

            End If

            If txt_FDesde.Text <> "" Then
                If txtFHasta.Text = "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese fecha hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If


            If txt_FDesde.Text = "" Then
                If txtFHasta.Text <> "" Then
                    Msj.Mensaje(Page, Caption, "Ingrese fecha desde", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Sub
                End If
            End If

            If Drop_Estado.SelectedValue = 0 Then
                Est_dsd = 0
                Est_Hst = 999
            Else
                Est_dsd = Drop_Estado.SelectedValue
                Est_Hst = Drop_Estado.SelectedValue
            End If

            If RB_TipChr.SelectedValue = 1 Then
                Tip_Chr_Dsd = "F"
                Tip_Chr_Hst = "F"
            ElseIf RB_TipChr.SelectedValue = 2 Then
                Tip_Chr_Dsd = "R"
                Tip_Chr_Hst = "R"
            ElseIf RB_TipChr.SelectedValue = 3 Then
                Tip_Chr_Dsd = "F"
                Tip_Chr_Hst = "R"
            End If


            'If RadioButtonList1.Text = "C" Then
            Dim Llena = TSR.Cheques_DevuelveChrPorCliente(txt_FDesde.Text, txtFHasta.Text, Txt_Rut_Cli.Text _
                                                          , Dp_Custodia.SelectedValue, Est_dsd, Est_Hst, Tip_Chr_Dsd, Tip_Chr_Hst, 15)
            ' Dim COll_Cheques As New Collection
            For Each i In Llena
                sesion.coll_chr.Add(i)
            Next

            If Txt_Rut_Cli.Text = "" Then
                Txt_Rut_Cli.Text = 0
            End If

            If sesion.coll_chr.Count > 0 Then
                GR_Busqueda.DataSource = sesion.coll_chr
                GR_Busqueda.DataBind()
                Formato_GV_Busquda()
                IB_Imprimir.Enabled = True
                BloqueaControles()
            Else
                Msj.Mensaje(Page, Caption, "No se encontraron cheques con este criterio de búsqueda", TipoDeMensaje._Exclamacion)
                IB_Imprimir.Enabled = False
            End If
            If Txt_Rut_Cli.Text = 0 Then
                Txt_Rut_Cli.Text = ""
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try


            Dim rut_desde As String
            Dim rut_hasta As String
            Dim cust_desde As Integer
            Dim cust_hasta As Integer
            Dim fDesde, fHasta As String
            Dim Est_dsd, Est_hst As Integer
            Dim Tip_Chr_Dsd As String
            Dim Tip_Chr_Hst As String

            If txt_FDesde.Text = "" Then
                fDesde = "01/01/1900"
            Else
                fDesde = txt_FDesde.Text
            End If

            If txtFHasta.Text = "" Then
                fHasta = "31/12/2999"
            Else
                fHasta = txtFHasta.Text
            End If

            If Txt_Rut_Cli.Text = "" Then
                rut_desde = "000000000000"
                rut_hasta = "999999999999"
            Else
                rut_desde = Txt_Rut_Cli.Text
                rut_hasta = Txt_Rut_Cli.Text
            End If

            If Dp_Custodia.SelectedValue = 0 Then
                cust_desde = 0
                cust_hasta = 999
            Else
                cust_desde = Dp_Custodia.SelectedValue
                cust_hasta = Dp_Custodia.SelectedValue
            End If

            If Drop_Estado.SelectedValue = 0 Then
                Est_dsd = 0
                Est_hst = 999
            Else
                Est_dsd = Drop_Estado.SelectedValue
                Est_hst = Drop_Estado.SelectedValue
            End If

            If RB_TipChr.SelectedValue = 1 Then
                Tip_Chr_Dsd = "F"
                Tip_Chr_Hst = "F"
            ElseIf RB_TipChr.SelectedValue = 2 Then
                Tip_Chr_Dsd = "R"
                Tip_Chr_Hst = "R"
            ElseIf RB_TipChr.SelectedValue = 3 Then
                Tip_Chr_Dsd = "F"
                Tip_Chr_Hst = "R"
            End If


            'RW.AbrePopup(Me, 2, "../../Pagos/rightframe_archivos/InformeDePagos.aspx?id=" & HF_Id_Ing.Value, "Pagos", 1000, 700, 10, 10)
            RW.AbrePopup(Me, 2, "InformedeCheque.aspx?RutDesde=" & rut_desde & "&RutHasta=" & rut_hasta & _
                         "&FechaInicio=" & fDesde & "&FechaTermino=" & fHasta _
                         & "&CustInicio=" & cust_desde & "&CustTermino=" & cust_hasta & "&Estdsd=" & Est_dsd & "&Esthst=" & Est_hst & _
                         "&TipChr_dsd=" & Tip_Chr_Dsd & "&tipchr_hst=" & Tip_Chr_Hst, "Pagos", 1500, 1200, 10, 10)
            IB_Limpiar.Enabled = True
        Catch ex As Exception
            Msj.mensaje(Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub


    'Protected Sub GR_Busqueda_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GR_Busqueda.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '        'e.Row.Attributes.Add("onClick", "ClickColillaAnterior(ctl00_ContentPlaceHolder1_Tabs_Pn_Anteriores_GV_ColAnteriores, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub




#End Region

#Region "Sub y Function"
    Sub CargaDrop()
        CG.ParametrosDevuelve(112, True, Dp_Custodia)
        CG.ParametrosDevuelve(113,True,Drop_Estado)

    End Sub

    Sub BloqueaControles()
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.CssClass = "clsDisabled"
        IB_AyudaCli.Enabled = False
        txt_FDesde.ReadOnly = True
        txtFHasta.ReadOnly = True
        Dp_Custodia.Enabled = False
        txt_FDesde.CssClass = "clsDisabled"
        txtFHasta.CssClass = "clsDisabled"
        Dp_Custodia.CssClass = "clsDisabled"

        txt_FDesde_CalendarExtender.Enabled = False
        txtFHasta_CalendarExtender.Enabled = False
        txt_FDesde_MaskedEditExtender.Enabled = False
        txtFHasta_MaskedEditExtender.Enabled = False
        Txt_Rut_Cli_MaskedEditExtender.Enabled = False


        Drop_Estado.Enabled = False
        Drop_Estado.CssClass = "clsDisabled"
        RB_TipChr.Enabled = False
        RB_TipChr.CssClass = "clsDisabled"

    End Sub
    Sub Limpia()
        txt_FDesde.Text = ""
        Txt_Rut_Cli.Text = ""
        txtFHasta.Text = ""
        Txt_Dig_Cli.Text = ""
        GR_Busqueda.DataSource = Nothing
        GR_Busqueda.DataBind()
        sesion.coll_chr = New Collection
        Dp_Custodia.ClearSelection()
        IB_Imprimir.Enabled = False

        Txt_Dig_Cli.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Raz_Soc.Text = ""


        Txt_Dig_Cli.ReadOnly = False
        Txt_Rut_Cli.ReadOnly = False

        Txt_Dig_Cli.CssClass = "clsText"
        Txt_Rut_Cli.CssClass = "clsText"
        IB_AyudaCli.Enabled = True


        txt_FDesde_CalendarExtender.Enabled = True
        txtFHasta_CalendarExtender.Enabled = True
        txt_FDesde_MaskedEditExtender.Enabled = True
        txtFHasta_MaskedEditExtender.Enabled = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = True
        Dp_Custodia.Enabled = True

        txt_FDesde.CssClass = "clsText"
        txtFHasta.CssClass = "clsText"
        Dp_Custodia.CssClass = "clsText"

        txt_FDesde.ReadOnly = False
        txtFHasta.ReadOnly = False

        RB_TipChr.SelectedValue = 3 'Todos



        Drop_Estado.Enabled = True
        Drop_Estado.CssClass = "clsTxt"
        RB_TipChr.Enabled = True
        RB_TipChr.CssClass = "clsTxt"

        Drop_Estado.ClearSelection()
        NroPaginacion = 0

    End Sub

    Sub Formato_GV_Busquda()
        Try
            For i = 1 To sesion.coll_chr.Count
                GR_Busqueda.Rows(i - 1).Cells(1).Text = UCase(sesion.coll_chr.Item(i).Estado)
                'GR_Busqueda.Rows(i - 1).Cells(7).Text = Format(sesion.coll_chr.Item(i).chr_fev_rea, "dd/MM/yyyy")
                Select Case sesion.coll_chr.Item(i).id_P_0023
                    Case 1 'Pesos
                        GR_Busqueda.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.coll_chr.Item(i).Monto_cheque), FMT.FCMSD)
                    Case 2 'UF
                        GR_Busqueda.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.coll_chr.Item(i).Monto_cheque), FMT.FCMCD4)
                    Case 3, 4
                        GR_Busqueda.Rows(i - 1).Cells(8).Text = Format(CLng(sesion.coll_chr.Item(i).Monto_cheque), FMT.FCMCD)
                End Select
                'GR_Busqueda.Rows(i - 1).Cells(2).Text = Format(CLng(sesion.coll_chr.Item(i).chr_num), FMT.FCMSD)

            Next

        Catch ex As Exception

        End Try
    End Sub



#End Region





    

   
    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            NroPaginacion -= 15
            IB_Buscar_Click(Me, e)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GR_Busqueda.Rows.Count < 15 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            NroPaginacion += 15
            IB_Buscar_Click(Me, e)

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
End Class
