Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Tipo_Cartera
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"
    Dim CG As New ConsultasGenerales
    Dim Ag As New ActualizacionesGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Caption As String = "Cartera Cliente"
    Dim Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                txt_Des.Focus()
                CargaGrillaCrt()
                txt_NroCartera.Attributes.Add("Style", "TEXT-ALIGN:right")
                txt_NroDiasCobro.Attributes.Add("Style", "TEXT-ALIGN:right")

            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    'Protected Sub Gr_Crt_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Crt.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Crt, 'selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(ctl00_ContentPlaceHolder1_Gr_Crt,'formatable')")
    '        e.Row.Attributes.Add("onClick", "SeleccionaGrCartera(ctl00_ContentPlaceHolder1_Gr_Crt, 'clicktable', 'formatable', 'selectable')")
    '    End If
    'End Sub

    'Protected Sub LinkB_Id_crt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkB_Id_crt.Click
    '    Try

    '        For i = 0 To Gr_Crt.Rows.Count - 1
    '            Gr_Crt.Rows(i).CssClass = "formatable"
    '            If HF_Posicion.Value >= 0 And HF_Idcrt.Value > 0 Then
    '                Gr_Crt.Rows(HF_Posicion.Value).CssClass = "clicktable"
    '            End If
    '            txt_Des.Text = Gr_Crt.Rows(HF_Posicion.Value).Cells(1).Text
    '            txt_NroCartera.Text = HF_Idcrt.Value
    '            txt_NroDiasCobro.Text = Gr_Crt.Rows(HF_Posicion.Value).Cells(2).Text
    '        Next

    '        txt_Des.ReadOnly = False
    '        txt_NroDiasCobro.ReadOnly = False
    '        txt_Des.CssClass = "clsMandatorio"
    '        txt_NroDiasCobro.CssClass = "clsMandatorio"
    '        IB_Eliminar.Enabled = True
    '        IB_Guardar.Enabled = True
    '        IB_Nuevo.Enabled = False
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Link_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Eliminar.Click
        Try
            If Ag.CarteraElimina(txt_NroCartera.Text) = True Then
                CargaGrillaCrt()
                Limpiar()
                IB_Eliminar.Enabled = False
                Msj.Mensaje(Me.Page, Caption, "Datos Eliminados", TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try
            If txt_NroCartera.Text = "" Then
                'If HF_Idcrt.Value < 0 Then

                Dim crt As New crt_cls
                crt.id_crt = Nothing
                crt.crt_des = UCase(txt_Des.Text)
                crt.crt_ctd_dia = txt_NroDiasCobro.Text
                If Ag.CarteraInserta(crt) Then
                    CargaGrillaCrt()
                    Limpiar()
                    IB_Guardar.Enabled = False
                    Msj.Mensaje(Me.Page, Caption, "Datos Ingresados", TipoDeMensaje._Informacion)
                Else
                    Msj.Mensaje(Me.Page, Caption, "Error", TipoDeMensaje._Informacion)
                End If
            Else
                If Ag.CarteraModifica(txt_NroCartera.Text, txt_NroDiasCobro.Text, UCase(txt_Des.Text)) Then
                    CargaGrillaCrt()
                    Limpiar()
                    IB_Guardar.Enabled = False
                    Msj.Mensaje(Me.Page, Caption, "Datos Modificados", TipoDeMensaje._Informacion)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Sub y Function"
    Sub CargaGrillaCrt()
        CG.CarteraDevuelveTodos(Gr_Crt)
    End Sub

    Sub Limpiar()
        txt_NroCartera.Text = ""
        txt_NroDiasCobro.Text = ""
        txt_Des.Text = ""
        For i = 0 To Gr_Crt.Rows.Count - 1

            If (i Mod 2) = 0 Then
                Gr_Crt.Rows(i).CssClass = "formatUltcell"
            Else
                Gr_Crt.Rows(i).CssClass = "formatUltcellAlt"
            End If

        Next
        HF_Idcrt.Value = ""
        HF_Posicion.Value = ""
        IB_Nuevo.Enabled = True
        IB_Eliminar.Enabled = False
        IB_Guardar.Enabled = False
        txt_Des.ReadOnly = True
        txt_NroDiasCobro.ReadOnly = True
        txt_Des.CssClass = "clsDisabled"
        txt_NroDiasCobro.CssClass = "clsDisabled"

    End Sub

#End Region

#Region "Botonera"
    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        txt_Des.CssClass = "clsDisabled"
        txt_NroDiasCobro.CssClass = "clsDisabled"
        Limpiar()
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click
        Try

            If Not Agt.ValidaAccesso(20, 20010812, Usr, "PRESIONO NUEVO ") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Limpiar()
            txt_Des.Focus()
            txt_Des.CssClass = "clsMandatorio"
            txt_NroDiasCobro.CssClass = "clsMandatorio"

            txt_Des.ReadOnly = False
            txt_NroDiasCobro.ReadOnly = False
            IB_Guardar.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Eliminar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20030812, Usr, "PRESIONO ELIMINAR ") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If txt_NroCartera.Text = "" Then
                'If HF_Idcrt.Value = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Seleccione Tipo de Cartera Para Eliminar", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de eliminar estos datos?", TipoDeMensaje._Confirmacion, Link_Eliminar.UniqueID, True)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try
            If Not Agt.ValidaAccesso(20, 20020812, Usr, "PRESIONO GUARDAR ") Then
                Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If txt_Des.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Descripción", TipoDeMensaje._Exclamacion)
                txt_Des.Focus()
                Exit Sub
            End If

            If txt_NroDiasCobro.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese Nro de Dias", TipoDeMensaje._Exclamacion)
                txt_NroDiasCobro.Focus()
                Exit Sub
            End If

            If Not IsNumeric(txt_NroDiasCobro.Text) Then
                Msj.Mensaje(Me.Page, Caption, "ingrese Solo Numeros", TipoDeMensaje._Exclamacion)
                txt_NroDiasCobro.Text = ""
                txt_NroDiasCobro.Focus()
                Exit Sub
            End If

            If txt_NroCartera.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)
            Else
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de modificar estos datos?", TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Btn_ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim btn As ImageButton = CType(sender, ImageButton)

            For i = 0 To Gr_Crt.Rows.Count - 1


                If (Gr_Crt.Rows(i).Cells(1).Text = btn.ToolTip) Then
                    If (i Mod 2) = 0 Then
                        Gr_Crt.Rows(i).CssClass = "selectable"
                    Else
                        Gr_Crt.Rows(i).CssClass = "selectableAlt"
                    End If
                Else
                    If (i Mod 2) = 0 Then
                        Gr_Crt.Rows(i).CssClass = "formatUltcell"
                    Else
                        Gr_Crt.Rows(i).CssClass = "formatUltcellAlt"
                    End If
                End If

                If (Gr_Crt.Rows(i).Cells(1).Text = btn.ToolTip) Then

                    txt_Des.Text = Gr_Crt.Rows(i).Cells(2).Text
                    txt_NroCartera.Text = Gr_Crt.Rows(i).Cells(1).Text
                    txt_NroDiasCobro.Text = Gr_Crt.Rows(i).Cells(3).Text
                    
                End If


            Next

            txt_Des.ReadOnly = False
            txt_NroDiasCobro.ReadOnly = False
            txt_Des.CssClass = "clsMandatorio"
            txt_NroDiasCobro.CssClass = "clsMandatorio"
            IB_Eliminar.Enabled = True
            IB_Guardar.Enabled = True
            IB_Nuevo.Enabled = False

        Catch ex As Exception

        End Try


    End Sub

#End Region

End Class
