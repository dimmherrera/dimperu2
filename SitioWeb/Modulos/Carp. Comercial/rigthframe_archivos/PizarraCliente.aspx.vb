Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Pizzarracliente
    Inherits System.Web.UI.Page

    Dim CLSCLI As New ClaseControlDual
    Private Sesion As New ClsSession.ClsSession
    Private Caption As String = "Clientes"
    Private FC As New FuncionesGenerales.FComunes
    Private Msj As New ClsMensaje
    Private agt As New Perfiles.Cls_Principal
    Dim Var As New FuncionesGenerales.Variables
    Dim NumEstadoCliente As Integer

    Private Sub CargaGrilla()
        Try

            Dim Rut_Dsd As String
            Dim Rut_Hst As String
            Dim Tip_Dsd As Integer
            Dim Tip_Hst As Integer
            Dim Eje_Dsd As Integer
            Dim Eje_Hst As Integer

            If Txt_Rut.Text <> "" Then
                Rut_Dsd = Txt_Rut.Text
                Rut_Hst = Txt_Rut.Text
            Else
                Rut_Dsd = "000000000000"
                Rut_Hst = "9999999999999"
            End If

            If DP_TipoCli.SelectedIndex <> 0 Then
                Tip_Dsd = DP_TipoCli.SelectedValue
                Tip_Hst = DP_TipoCli.SelectedValue
            Else
                Tip_Dsd = 0
                Tip_Hst = 999999999
            End If

            If DP_Ejecutivo.SelectedIndex <> 0 Then
                Eje_Dsd = DP_Ejecutivo.SelectedValue
                Eje_Hst = DP_Ejecutivo.SelectedValue
            Else

                Eje_Dsd = 0
                Eje_Hst = 999

            End If

            GV_Clientes.DataSource = Nothing
            GV_Clientes.DataBind()

            Dim CC As New ClaseControlDual

            CC.ClientesPendienteDevuelve(GV_Clientes, Rut_Dsd, Rut_Hst, Eje_Dsd, Eje_Hst, Tip_Dsd, Tip_Hst, Txt_Nom.Text.Trim, True)

            If GV_Clientes.Rows.Count <= 0 Then
                Msj.Mensaje(Me.Page, Caption, "No se encontraron clientes pendientes para aprobar", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then
                Dim Sesion As New ClsSession.ClsSession
                Dim CG As New ConsultasGenerales

                Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
                CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, DP_TipoCli)
                CG.EjecutivosDevuelve(DP_Ejecutivo, CodEje, 15)
                Txt_Orden.Value = "1"
                SW.Value = "UPDATE"
                CargaGrilla()
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Clientes, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_GV_Clientes, 'formatUltcell')")
            'e.Row.Attributes.Add("onClick", "DetalleCliente(ctl00_ContentPlaceHolder1_GV_Clientes, 'clicktable', 'formatUltcell', 'selectable')")
        End If
    End Sub


    Protected Sub IB_Rechazar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Rechazar.Click

        If Not agt.ValidaAccesso(20, 20020703, Usr, "PRESIONO RECHAZAR CLIENTE") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If


        If Not VALIDA_CAMPOS_VACIOS() Then
            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Rechazar.UniqueID)
        End If

    End Sub

    Protected Sub LB_Rechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Rechazar.Click

        Try
            Dim AEC As New ClaseControlDual
            Dim Cliente As cli_cls

            NumEstadoCliente = 3

            Cliente = CargaCliente()
            If AEC.ActualizaEstadoCli(Cliente) Then
                Msj.Mensaje(Me.Page, Caption, "Cliente fue actualizado", TipoDeMensaje._Exclamacion)
                LimpiaPantalla()
                CargaGrilla()
            Else
                Msj.Mensaje(Me.Page, Caption, "No se pudo modificar estado del cliente", TipoDeMensaje._Informacion)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Aprobar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aprobar.Click

        Try


            If Not agt.ValidaAccesso(20, 20010703, Usr, "PRESIONO APROBAR CLIENTE") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                'Exit Sub
            End If

            If Not VALIDA_CAMPOS_VACIOS() Then
                Msj.Mensaje(Me.Page, Caption, "¿Está seguro de actualizar estado?", TipoDeMensaje._Confirmacion, LB_Aprobar.UniqueID)

            End If

        Catch ex As Exception

        End Try

    End Sub
 
    Protected Sub LB_Aprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Aprobar.Click
        Try

            Dim AEC As New ClaseControlDual
            Dim Cliente As cli_cls

            NumEstadoCliente = 1
            Cliente = CargaCliente()

            If AEC.ActualizaEstadoCli(Cliente) Then
                Msj.Mensaje(Me.Page, Caption, "Cliente fue actualizado", TipoDeMensaje._Exclamacion)
                LimpiaPantalla()
                CargaGrilla()
            Else
                Msj.Mensaje(Me.Page, Caption, "No se pudo modificar estado del cliente", TipoDeMensaje._Informacion)
            End If


        Catch ex As Exception

        End Try

    End Sub



    Public Function VALIDA_CAMPOS_VACIOS() As Boolean

        VALIDA_CAMPOS_VACIOS = False

        If Val(Me.Posicion.Value) <= 0 Then
            Msj.Mensaje(Me.Page, Caption, "Debe Seleccionar algún Cliente", TipoDeMensaje._Exclamacion)
            Return True
        End If

    End Function

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        NroPaginacion = 0
        CargaGrilla()
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        LimpiaPantalla()
    End Sub

    Private Sub LimpiaPantalla()
        NroPaginacion = 0
        Txt_Nom.Text = ""
        Txt_Orden.Value = "1"
        Txt_Rut.Text = ""
        DP_TipoCli.ClearSelection()
        DP_Ejecutivo.ClearSelection()
        GV_Clientes.DataSource = New Collection
        GV_Clientes.DataBind()
        Me.Posicion.Value = ""
        CargaGrilla()
    End Sub


    Private Function CargaCliente() As cli_cls

        Try

            Dim CLI As New cli_cls
            Dim Rut As String

            Rut = Replace(Posicion.Value, ".", "")
            Rut = Replace(Rut, "-", "")
            Rut = Rut.Substring(0, Len(Rut) - 1)

            With CLI
                '**************************************************************************************************************************
                .cli_idc = Format(Val(Replace(Rut, ".", "")), Var.FMT_RUT)

                If SW.Value = "UPDATE" Then

                    .id_P_008 = NumEstadoCliente

                End If

            End With

            Return CLI

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
            Return Nothing
        End Try

    End Function

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        Posicion.Value = ""

        For I = 0 To GV_Clientes.Rows.Count - 1

            If GV_Clientes.Rows(I).Cells(1).Text = btn.ToolTip Then
                Posicion.Value = btn.ToolTip
                If (I Mod 2) = 0 Then
                    GV_Clientes.Rows(I).CssClass = "selectable"
                Else
                    GV_Clientes.Rows(I).CssClass = "selectableAlt"
                End If
            Else
                If (I Mod 2) = 0 Then
                    GV_Clientes.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Clientes.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

    End Sub

End Class
