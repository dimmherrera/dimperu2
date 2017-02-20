Imports ClsSession.ClsSession
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports CapaDatos
Imports System.IO

Partial Class Zonas
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim Caption As String = "Zonas"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim CG As New ConsultasGenerales
    Dim Sesion As New ClsSession.ClsSession
    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Response.Expires = -1
            If Not IsPostBack Then
                If Request.QueryString("Sucursal") <> 0 Then
                    txt_Cod_Suc.Text = Request.QueryString("Sucursal")
                    CargaGr()
                End If
                Inhabilitar()
                Ib_Eliminar.Enabled = False

                'If Sesion.Sucursal <> 0 Then
                '    txt_Cod_Suc.Text = Sesion.Sucursal

                '    CargaGr()
                '    ' Else
                '    '    RW.ClosePag(Me)
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ib_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Volver.Click
        Try
            RW.ClosePag(Me)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ib_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Limpiar.Click
        Try
            limpiar()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ib_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Nuevo.Click
        Try
            txt_Cod_Zon.Text = ""
            txt_Des.Text = ""
            txt_Des.CssClass = "clsMandatorio"
            txt_Des.Enabled = True
            txt_Des.Focus()
            Ib_Guardar.Enabled = True

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Ib_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Guardar.Click
        Try
            If txt_Cod_Suc.Text = "" Then
                Msj.Mensaje(Me.Page, 2, "Seleccione Sucursal", TipoDeMensaje._Exclamacion,"",False)
                Exit Sub
            End If

            If txt_Des.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese descripcion", TipoDeMensaje._Exclamacion,"",False)
                Exit Sub
            End If
            If txt_Cod_Zon.Text = "" Then
                Dim z As New zon_cls
                z.id_zon = Nothing
                z.id_suc = txt_Cod_Suc.Text
                z.zon_des = UCase(txt_Des.Text)
                AG.ZonaInserta(z)
                Msj.Mensaje(Me.Page, Caption, "Zona Guardada", TipoDeMensaje._Informacion, "", False)
                CargaGr()
                limpiar()
            Else
                AG.ZonaModifica(txt_Cod_Zon.Text, UCase(txt_Des.Text))
                Msj.Mensaje(Me.Page, Caption, "Zona Modificada", TipoDeMensaje._Informacion, "", False)
                CargaGr()
                limpiar()
            End If
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub Gr_Zona_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
    '    Try
    '        'txt_Des.Enabled = True
    '        'txt_Des.CssClass = "clsMandatorio"
    '        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then
    '            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '            e.Row.Attributes.Add("onClick", "SeleccionaGrZona(Gr_Zona, 'clicktable', 'formatable', 'selectable')")
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Ib_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ib_Eliminar.Click
        Try
            If txt_Cod_Zon.Text = "" Then
                Msj.Mensaje(Me.Page,Caption, "Seleccione Zona", TipoDeMensaje._Exclamacion,"",False)
                Exit Sub
            End If
            If AG.ZonaElimina(txt_Cod_Zon.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Zona Eliminada", TipoDeMensaje._Exclamacion, "", False)
                CargaGr()
            End If

        Catch ex As Exception

        End Try
    End Sub


#End Region


#Region "Sub y Function"
    Sub CargaGr()
        CG.ZonaDevuelveXSuc(txt_Cod_Suc.Text,True,Gr_Zona)
    End Sub


    Sub limpiar()
        ' txt_Cod_Suc.Text = ""
        txt_Cod_Zon.Text = ""
        txt_Des.Text = ""
        txt_Des.CssClass = "clsDisabled"
        Ib_Guardar.Enabled = False
        Ib_Nuevo.Enabled = True
    End Sub
    Sub Inhabilitar()
        txt_Cod_Suc.Enabled = False
        txt_Cod_Zon.Enabled = False
        txt_Des.Enabled = False
        Ib_Guardar.Enabled = False

    End Sub

#End Region

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        For i = 0 To Gr_Zona.Rows.Count - 1

            If (btn.ToolTip = Gr_Zona.Rows(i).Cells(1).Text) Then

                If (i Mod 2) = 0 Then
                    Gr_Zona.Rows(i).CssClass = "selectable"
                Else
                    Gr_Zona.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Zona.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Zona.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

            If (Gr_Zona.Rows(i).Cells(1).Text = btn.ToolTip) Then

                txt_Cod_Zon.Text = Gr_Zona.Rows(i).Cells(1).Text
                txt_Des.Text = Gr_Zona.Rows(i).Cells(3).Text

                txt_Des.Enabled = True
                txt_Des.CssClass = "clsMandatorio"

                Ib_Nuevo.Enabled = False
                Ib_Guardar.Enabled = True

            End If
        Next
    End Sub
End Class
