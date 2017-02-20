Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Bancos
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Sesion As New ClsSession.ClsSession
    Dim CG As New ConsultasGenerales
    Dim CLSCLI As New ClaseClientes
    Dim AG As New ActualizacionesGenerales
    Dim Caption As String = "Mantención de Bancos"
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal

#End Region

#Region "EVENTOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Me.Page.IsPostBack Then

                NroPaginacion = 0
                Response.Expires = -1

                CG.BancosDevuelveTodos(DP_Bancos)
                CargaDrop_Tip_Cta()
                HabilitaDesabilita(False)

                If Request.QueryString("rut") <> "" And Request.QueryString("id") <> "" Then
                    'ACTUALIZA
                    'SW.Text = "UPDATE"
                    'CargaBanco()
                Else
                    'NUEVO
                    Sesion.RutCli = Request.QueryString("rut")
                    SW.Value = "INSERT"
                End If

                CargaGrillaBanco()

            End If

            IB_Volver.Attributes.Add("onClick", "javascript:window.close()")



        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub CargaBanco()

        Try

            Dim Bco As nbc_cls
            Bco = CLSCLI.BancosDevuelvePorCliente(ClsSession.ClsSession.RutCli, CodBco.Value)

            DP_Bancos.ClearSelection()
            DP_Bancos.Items.FindByValue(Bco.sbc_cls.id_bco).Selected = True

            Txt_Cta_Cte.Text = Bco.nbc_cct
            If Bco.nbc_dep = "S" Then
                Me.CB_Deposito.Checked = True
            Else
                Me.CB_Deposito.Checked = False
            End If
            HabilitaDesabilita(True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDrop_Tip_Cta()

        Try
            CG.ParametrosDevuelve(TablaParametro.TipoCuenta, True, DP_Tip_Cta)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            'Pagina = Page.AppRelativeVirtualPath
            'CambioTema(Page)
        End If
    End Sub

    Public Sub CargaGrillaBanco()
        Try
            'GRILLA DE BANCOS ASOCIADOS AL CLIENTE
            CLSCLI.BancosDevuelvePorCliente(False, Nothing, GV_Bancos, CLng(ClsSession.ClsSession.RutCli), 10)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub HabilitaDesabilita(ByVal Estado As Boolean)

        DP_Bancos.Enabled = Estado
        DP_Tip_Cta.Enabled = Estado
        Txt_Cta_Cte.ReadOnly = Not Estado
        CB_Deposito.Enabled = Estado

        If Estado Then
            DP_Bancos.CssClass = "clsMandatorio"
            DP_Tip_Cta.CssClass = "clsMandatorio"
            Txt_Cta_Cte.CssClass = "clsMandatorio"
            '  Txt_Cta_Cte.Text = ""
            CB_Deposito.Checked = True
            IB_EliminarBanco.Enabled = False
            IB_Guardar.Enabled = True
            DP_Tip_Cta.ClearSelection()
        Else
            DP_Bancos.CssClass = "clsDisabled"
            DP_Tip_Cta.CssClass = "clsDisabled"
            Txt_Cta_Cte.CssClass = "clsDisabled"
            DP_Bancos.ClearSelection()
            DP_Tip_Cta.ClearSelection()
            Txt_Cta_Cte.Text = ""
            CB_Deposito.Checked = False
            IB_EliminarBanco.Enabled = False
            IB_Guardar.Enabled = False
        End If

    End Sub

    Protected Sub GV_Bancos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Bancos.RowDataBound
    End Sub

    Protected Sub ActBancos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActBancos.Click
        Try

            SW.Value = "UPDATE"
            CargaBanco()
            'MarcaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 10 Then
            NroPaginacion -= 10
            CargaGrillaBanco()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Bancos.Rows.Count < 10 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Bancos.Rows.Count = 10 Then
            NroPaginacion += 10
            CargaGrillaBanco()
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim rg As New FuncionesGenerales.FComunes
        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        Try

            For I = 0 To GV_Bancos.Rows.Count - 1

                If (boton_ver.ToolTip = CType(GV_Bancos.Rows(I).FindControl("Img_Ver"), ImageButton).ToolTip) Then

                    IB_EliminarBanco.Enabled = True

                    If (I Mod 2) = 0 Then
                        GV_Bancos.Rows(I).CssClass = "selectable"
                    Else
                        GV_Bancos.Rows(I).CssClass = "selectableAlt"
                    End If

                    Txt_Cta_Cte.Text = (GV_Bancos.Rows(I).Cells(4).Text)
                    DP_Tip_Cta.SelectedValue = CType(GV_Bancos.Rows(I).FindControl("HF_TipCta"), HiddenField).Value
                    DP_Bancos.SelectedValue = CType(GV_Bancos.Rows(I).FindControl("HF_Bco"), HiddenField).Value
                    CodBco.Value = CType(GV_Bancos.Rows(I).FindControl("HF_Suc"), HiddenField).Value

                    If GV_Bancos.Rows(I).Cells(1).Text = "SI" Then
                        CB_Deposito.Checked = True
                    Else
                        CB_Deposito.Checked = False
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Bancos.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Bancos.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_EliminarBanco_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_EliminarBanco.Click

        If Not agt.ValidaAccesso(20, 20060101, Usr, "PRESIONO ELIMINAR BANCO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Msj.Mensaje(Me, Caption, "¿Esta seguro de eliminar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID, False)

    End Sub

    Protected Sub IB_NuevoBanco_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_NuevoBanco.Click

        If Not agt.ValidaAccesso(20, 20070101, Usr, "PRESIONO NUEVO BANCO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        HabilitaDesabilita(True)

        Txt_Cta_Cte.Text = ""
        DP_Bancos.ClearSelection()

        IB_EliminarBanco.Enabled = False
        CB_Deposito.Checked = False
        SW.Value = "INSERT"

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20050101, Usr, "PRESIONO GUARDAR BANCO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_Bancos.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione banco", TipoDeMensaje._Informacion, "", False)
                DP_Bancos.Focus()
                Exit Sub
            End If

            If DP_Tip_Cta.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione tipo de cuenta", TipoDeMensaje._Informacion, "", False)
                DP_Tip_Cta.Focus()
                Exit Sub
            End If


            If Txt_Cta_Cte.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese cuenta corriente", TipoDeMensaje._Informacion, "", False)
                Txt_Cta_Cte.Focus()
                Exit Sub
            End If

            Msj.Mensaje(Me, Caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        Try

            Dim BCO As New nbc_cls
            Dim Var As New FuncionesGenerales.Variables

            BCO.cli_idc = Format(CLng(Sesion.RutCli), Var.FMT_RUT)

            Dim aux As Integer = CLSCLI.SucursalXBancoDevuelve(Me.DP_Bancos.SelectedValue)

            If aux <> 0 Then
                BCO.id_sbc = aux
            Else
                Msj.Mensaje(Page, "Administración Cliente", "Banco no tiene sucursal asociada", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            BCO.id_P_0312 = DP_Tip_Cta.SelectedValue
            BCO.nbc_cct = Txt_Cta_Cte.Text.Trim
            BCO.nbc_dep = CChar(IIf(Me.CB_Deposito.Checked, "S", "N"))

            If SW.Value = "INSERT" Then
                If CLSCLI.BancoClienteInserta(BCO) Then
                    'Msj.Mensaje(Me.Page, Caption, "Banco Ingresado", TipoDeMensaje._Informacion)
                Else
                    'Msj.Mensaje(Me.Page, Caption, "Banco no se pudo ingresar", TipoDeMensaje._Informacion)
                End If
            ElseIf SW.Value = "UPDATE" Then
                If CLSCLI.BancoClienteUpdate(BCO, HF_Ctc.Value) Then
                    'Msj.Mensaje(Me.Page, Caption, "Banco Modificado", TipoDeMensaje._Informacion)
                Else
                    'Msj.Mensaje(Me.Page, Caption, "Banco no se pudo modificar", TipoDeMensaje._Informacion)
                End If

            End If

            CargaGrillaBanco()
            HabilitaDesabilita(False)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click

        Try
            Dim Sesion As New ClsSession.ClsSession
            If CLSCLI.BancoClienteDelete(Sesion.RutCli, CodBco.Value, Txt_Cta_Cte.Text.trim) Then
                CargaGrillaBanco()
                HabilitaDesabilita(False)
                Msj.Mensaje(Me.Page, Caption, "Banco Eliminado", TipoDeMensaje._Informacion)
            Else
                Msj.Mensaje(Me.Page, Caption, "Banco No se puede eliminar, esta asosiado a operaciones...", TipoDeMensaje._Informacion)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

#End Region

   
End Class


 



