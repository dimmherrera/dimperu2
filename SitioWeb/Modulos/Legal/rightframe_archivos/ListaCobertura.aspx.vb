Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ListaCobertura
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim Msj As New ClsMensaje
    Dim CL As New ConsultasLegales
    Dim caption As String = "Lista Cobertura"
    Dim CG As New ConsultasGenerales
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim VAR As New FuncionesGenerales.Variables
    Dim FG As New FuncionesGenerales.FComunes
    Dim sesion As New ClsSession.ClsSession
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Sucdsd As Integer
    Dim suchst As Integer
    Dim rutdsd As String
    Dim ruthst As String

#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Legal"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                NroPaginacion = 0
                txt_Fecha_Cob.Text = CDate(Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year)
                txt_Dif.Attributes.Add("Style", "TEXT-ALIGN:right")
                txt_totG1.Attributes.Add("Style", "TEXT-ALIGN:right")
                txt_totG2.Attributes.Add("Style", "TEXT-ALIGN:right")
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN:right")
            End If
          
            IB_AyudaCLi.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',570 ,400,100,100);")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_PorCliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_PorCliente.CheckedChanged
        Try
            If RB_PorCliente.Checked = True Then
                RB_Todos_CLi.Checked = False
                RB_Cli_Con_Ope.Checked = False
                Txt_Rut_Cli.ReadOnly = False
                Txt_Rut_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.ReadOnly = False
                Txt_Dig_Cli.CssClass = "clsMandatorio"
                Txt_Dig_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                txt_Rut_Cli_MaskedEditExtender.Enabled = True
                IB_AyudaCLi.Enabled = True
                Txt_Rut_Cli.Focus()
            Else
                BloqueCli()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Todos_CLi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Todos_CLi.CheckedChanged
        Try
            If RB_Todos_CLi.Checked = True Then
                RB_PorCliente.Checked = False
                RB_Cli_Con_Ope.Checked = False
                BloqueCli()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RB_Cli_Con_Ope_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Cli_Con_Ope.CheckedChanged
        Try
            If RB_Cli_Con_Ope.Checked = True Then
                RB_Todos_CLi.Checked = False
                RB_PorCliente.Checked = False
                BloqueCli()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try
            TraeCliente()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Function y Sub"
    Private Sub LimpiaControles()
        Txt_Rut_Cli.Text = ""
        Txt_Dig_Cli.Text = ""
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        CB_Suc.Checked = False
        CB_Suc.Enabled = True
        RB_Cli_Con_Ope.Checked = False
        RB_Cli_Con_Ope.Enabled = True
        RB_PorCliente.Checked = False
        RB_PorCliente.Enabled = True

        RB_Todos_CLi.Checked = False
        RB_Todos_CLi.Enabled = True
        Txt_Raz_Soc.Text = ""
        txt_Rut_Cli_MaskedEditExtender.Enabled = False
        Gr_ListaCobertura.DataSource = Nothing
        Gr_ListaCobertura.DataBind()
        IB_Imprimir.Enabled = False
        txt_totG1.Text = ""
        txt_totG2.Text = ""
        txt_Dif.Text = ""

        RB_Clientes.SelectedItem.Value = 1
        RB_Clientes.Enabled = True

        NroPaginacion = 0
    End Sub

    Private Sub TraeCliente()

        Dim CLSCLI As New ClaseClientes
        Dim cli As cli_cls

        cli = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

        If sesion.valida_cliente <> "" Then
            Msj.Mensaje(Me.Page, caption, sesion.valida_cliente, TipoDeMensaje._Informacion)
            Exit Sub
        Else

            If IsNothing(cli) Then
                Msj.Mensaje(Me, "Atención", "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Text = ""
                Txt_Dig_Cli.Text = ""
                Exit Sub
            End If



            Txt_Rut_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.ReadOnly = True
            Txt_Dig_Cli.CssClass = "clsDisabled"
            Txt_Raz_Soc.Text = Trim(cli.cli_rso) & " " & Trim(cli.cli_ape_ptn) & " " & Trim(cli.cli_ape_mtn)
            IB_AyudaCLi.Enabled = False
        End If

    End Sub

    Private Sub BloqueCli()
        Txt_Rut_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = True
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.Text = ""
        Txt_Rut_Cli.Text = ""
        Txt_Raz_Soc.Text = ""
        Txt_Raz_Soc.CssClass = "clsDisabled"
        txt_Rut_Cli_MaskedEditExtender.Enabled = False
        IB_AyudaCLi.Enabled = False
    End Sub

    Public Sub BloqueaControles()
        Txt_Rut_Cli.ReadOnly = True
        Txt_Dig_Cli.ReadOnly = True
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.CssClass = "clsDisabled"
        CB_Suc.Enabled = False
        RB_Cli_Con_Ope.Enabled = False
        RB_PorCliente.Enabled = False
        RB_Todos_CLi.Enabled = False
        Txt_Raz_Soc.Text = ""
        txt_Rut_Cli_MaskedEditExtender.Enabled = False
        'Gr_ListaCobertura.DataSource = Nothing
        'Gr_ListaCobertura.DataBind()
        IB_Imprimir.Enabled = True

        RB_Clientes.SelectedItem.Value = 1
        RB_Clientes.Enabled = False


    End Sub





#End Region

#Region "Botonera"

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Try
            LimpiaControles()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20010306, Usr, "PRESIONA BOTON BUSCAR PAGARE") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If RB_Todos_CLi.Checked = False And RB_Cli_Con_Ope.Checked = False And RB_PorCliente.Checked = False Then
                Msj.Mensaje(Me.Page, caption, "Seleccione un criterio de búsqueda", TipoDeMensaje._Exclamacion, "", True)
                Exit Sub
            End If
            If RB_PorCliente.Checked = True Then
                If Txt_Rut_Cli.Text = "" Then
                    Msj.Mensaje(Me.Page, caption, "Ingrese NIT", TipoDeMensaje._Informacion, "", True)
                    Exit Sub
                End If
            End If

            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "000000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Txt_Rut_Cli.Text
                ruthst = Txt_Rut_Cli.Text
            End If

            If CB_Suc.Checked = True Then
                Sucdsd = 0
                suchst = 999
            Else
                Sucdsd = sesion.Sucursal
                suchst = sesion.Sucursal
            End If
            Dim coll_Cobertura As New Collection

            coll_Cobertura = CL.ListaCobertura(rutdsd, ruthst, Sucdsd, suchst, txt_Fecha_Cob.Text, CodEje)
            txt_totG1.Text = 0
            txt_totG2.Text = 0
            If Not IsNothing(coll_Cobertura) Then
                If coll_Cobertura.Count > 0 Then
                    Gr_ListaCobertura.DataSource = coll_Cobertura
                    Gr_ListaCobertura.DataBind()
                    IB_Imprimir.Enabled = True
                    IB_Buscar.Enabled = True
                    For i = 1 To coll_Cobertura.Count
                        Gr_ListaCobertura.Rows(i - 1).Cells(0).Text = Format(CLng(coll_Cobertura.Item(i).cli_idc), FMT.FCMSD) & "-" & coll_Cobertura.Item(i).cli_dig_ito
                        Gr_ListaCobertura.Rows(i - 1).Cells(2).Text = Format(CLng(coll_Cobertura.Item(i).Mto_ocu), FMT.FCMSD)
                        Gr_ListaCobertura.Rows(i - 1).Cells(3).Text = Format(CLng(coll_Cobertura.Item(i).Monto), FMT.FCMSD)

                        If coll_Cobertura.Item(i).pgr_mdt = "N" Then
                            Gr_ListaCobertura.Rows(i - 1).Cells(4).Text = "No"
                        Else
                            Gr_ListaCobertura.Rows(i - 1).Cells(4).Text = "Si"
                        End If
                        txt_totG1.Text = txt_totG1.Text + coll_Cobertura.Item(i).Monto
                        txt_totG2.Text = txt_totG2.Text + coll_Cobertura.Item(i).Mto_ocu
                    Next
                    BloqueaControles()
                Else
                    Msj.Mensaje(Me.Page, caption, "No se encontraron datos", TipoDeMensaje._Informacion, "", True)
                End If
            Else
                Msj.Mensaje(Me.Page, caption, "No se encontraron datos", TipoDeMensaje._Informacion, "", True)
            End If
            txt_totG1.Text = Format(CDbl(txt_totG1.Text), "###,###,###,###0")
            txt_totG2.Text = Format(CDbl(txt_totG2.Text), "###,###,###,###0")
            txt_Dif.Text = Format(CDbl(txt_totG1.Text - txt_totG2.Text), "###,###,###,###0")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Imprimir.Click
        Try
            Dim Agt As New Perfiles.Cls_Principal

            If Not Agt.ValidaAccesso(20, 20020306, Usr, "PRESIONA BOTON IMPRIMIR INFORME") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Txt_Rut_Cli.Text = "" Then
                rutdsd = "000000000000"
                ruthst = "999999999999"
            Else
                rutdsd = Txt_Rut_Cli.Text
                ruthst = Txt_Rut_Cli.Text
            End If

            If CB_Suc.Checked = True Then
                Sucdsd = 0
                suchst = 999
            Else
                Sucdsd = sesion.Sucursal
                suchst = sesion.Sucursal
            End If
            RW.AbrePopup(Me, 1, "InformeCobertura.aspx?fecha=" & txt_Fecha_Cob.Text _
                         & "&rutdsd=" & rutdsd & "&ruthst=" & ruthst & "&sucdsd=" & Sucdsd _
                         & "&suchst=" & suchst & "&user=" & sesion.CodEje, "InformeCobertura", 740, 830, 250, 250)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 14 Then
            NroPaginacion -= 14
            IB_Buscar_Click(Me, e)
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If Gr_ListaCobertura.Rows.Count < 14 Then
                Msj.Mensaje(Me, caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If Gr_ListaCobertura.Rows.Count >= 14 Then
                NroPaginacion += 14
                IB_Buscar_Click(Me, e)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub
#End Region

   
   
    
End Class
