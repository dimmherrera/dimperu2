Imports CapaDatos
Imports ClsSession.ClsSession

Partial Class Modulos_Alertas_rightframe_archivos_PopUpCondicion
    Inherits System.Web.UI.Page

    Private CD As New ClaseControlDual
    Private CG As New ConsultasGenerales
    Private agt As New Perfiles.Cls_Principal
    Private Caption As String = "Condiciones"
    Private Msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            HF_Codigo.Value = Request.QueryString("id")

            'Estado de Condicion
            CG.ParametrosDevuelve(TablaParametro.EstadoCondicion, True, DP_Estado)

            Dim con As cdn_cls = CD.CondicionesDevuelve(HF_Codigo.Value)

            If Not IsNothing(con) Then
                DP_Estado.ClearSelection()
                Txt_Fecha_Cumplimiento.Text = Format(con.cdn_fec_com, "dd-MM-yyyy")
                Txt_Descripcion.Text = con.cdn_des
                DP_Estado.Items.FindByValue(con.id_p_0111).Selected = True
            End If

        End If

        'btn_volver.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$Lb_buscar');")
        btn_volver.Attributes.Add("onclick", "JavaScript:window.close();")

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20030103, Usr, "PRESIONO AGREGAR CONDICION") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            'Validaciones para agregar una condicion

            If Txt_Fecha_Cumplimiento.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar fecha de cumplimiento de la condición", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If DP_Estado.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar Estado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Descripcion.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar descripción de cumplimiento de la condición", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If CDate(Txt_Fecha_Cumplimiento.Text) <= Date.Now.ToShortDateString Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar fecha de cumplimiento mayor a hoy", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            '**************************************************************************************************************

            Dim CDN As New cdn_cls

            CDN.cdn_fec_com = CDate(Txt_Fecha_Cumplimiento.Text)
            CDN.cdn_des = Txt_Descripcion.Text.Trim.ToUpper
            CDN.id_p_0111 = DP_Estado.SelectedValue

            If DP_Estado.SelectedValue = 2 Then
                CDN.id_eje_apb = CodEje
                CDN.cdn_fec_apb = Date.Today
            End If

            CDN.id_cdn = HF_Codigo.Value

            If CD.CondicionActualiza(CDN) Then
                Msj.Mensaje(Me.Page, Caption, "Se Actualizo la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
            Else
                Msj.Mensaje(Me.Page, Caption, "No se Actualizo la condición de la operación", ClsMensaje.TipoDeMensaje._Exclamacion)
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Exclamacion)
        End Try

    End Sub

End Class
