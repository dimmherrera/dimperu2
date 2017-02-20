Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ClsContactos1
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Contactos"
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim Sesion As New ClsSession.ClsSession
    Dim FG As New FuncionesGenerales.FComunes
#End Region

#Region "EVENTOS"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            Response.Expires = -1
            NroPaginacion = 0
            Txt_Tel_Uno.Attributes.Add("style", "text-align: right")
            Txt_Tel_Dos.Attributes.Add("style", "text-align: right")
            Txt_Fax.Attributes.Add("style", "text-align: right")
            Txt_Tel_Tres.Attributes.Add("style", "text-align: right")

            Try
                'Deshabilitamos todos los campos
                Accion("clsDisabled", True)

                If Request.QueryString("Rut") <> "" Then

                    RutDeuCli.Value = Request.QueryString("Rut")
                    CargaGrillas()
                End If

                If Request.QueryString("Rut") = "2" Then
                    CHB_SCliente.Text = "Notificación"
                End If


            Catch ex As Exception
                Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
            End Try

        End If

        '********************************************************************************
        '***************************Victor Alvarez 28/11/2011****************************
        '*****No refresca pantalla ModVerificacion.aspx para que carge los contactos*****
        '********************************************************************************

        'If Request.QueryString("RefrescarModal") <> "" Then
        '    IB_Volver.Attributes.Add("onClick", "javascript:CerrarVentana('LB_Refrescar')")
        'Else
        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")
        'End If

        'IB_Volver.Attributes.Add("onClick", "javascript:CerrarVentana('LB_Refrescar')")

    End Sub

    Public Sub CargaGrillas()

        Dim Contacto As New ClaseClientes
        Dim Sesion As New ClsSession.ClsSession

        Try

            'LLENA GRILLA DE CONTACTOS ASOCIADAS AL CLIENTE

            Select Case Sesion.TipoDeContacto
                Case 1
                    Contacto.ContactosDevuelveTodos(Variables.TipoDeContacto.Cliente, RutDeuCli.Value, True, GV_Contactos)
                Case 2
                    Contacto.ContactosDevuelveTodos(Variables.TipoDeContacto.Deudor, RutDeuCli.Value, True, GV_Contactos)
                Case 3
                    Contacto.ContactosDevuelveTodos(Variables.TipoDeContacto.ClienteDeudor, RutDeuCli.Value, True, GV_Contactos)
            End Select

            For i = 0 To GV_Contactos.Rows.Count - 1

                If GV_Contactos.Rows(i).Cells(3).Text = "N" Or GV_Contactos.Rows(i).Cells(3).Text = "" Then
                    GV_Contactos.Rows(i).Cells(3).Text = "NO"
                Else
                    GV_Contactos.Rows(i).Cells(3).Text = "SI"
                End If

                If GV_Contactos.Rows(i).Cells(4).Text = "N" Or GV_Contactos.Rows(i).Cells(4).Text = "" Then
                    GV_Contactos.Rows(i).Cells(4).Text = "NO"
                Else
                    GV_Contactos.Rows(i).Cells(4).Text = "SI"
                End If
            Next


            If GV_Contactos.Rows.Count = 0 Then
                GV_Contactos.DataSource = Nothing
                GV_Contactos.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub CargaContacto()
        Dim ClsContacto As New ClaseClientes
        Dim Contacto As cnc_cls

        Dim PorDefecto As Char
        Dim Notificacion As Char

        Try


            Contacto = ClsContacto.ContactosDevuelve(Sesion.TipoDeContacto, RutDeuCli.Value, NroContacto.value)

            If Not IsNothing(Contacto) Then

                'Txt_Nro.Text = Contacto.cnc_num
                Txt_Nom.Text = Contacto.cnc_nom.Trim
                Txt_Rut.Text = IIf(IsNothing(Contacto.cnc_rut), "", Contacto.cnc_rut)
                Txt_Dig.Text = IIf(IsNothing(Contacto.cnc_dig), "", Contacto.cnc_dig)
                Txt_Car.Text = IIf(IsNothing(Contacto.cnc_car), "", Contacto.cnc_car)
                Txt_Dir.Text = IIf(IsNothing(Contacto.cnc_dir), "", Contacto.cnc_dir)
                Txt_Obs.Text = IIf(IsNothing(Contacto.cnc_obs), "", Contacto.cnc_obs)

                Txt_Tel_Uno.Text = IIf(IsNothing(Contacto.cnc_tel), "", Contacto.cnc_tel)
                Txt_Tel_Dos.Text = IIf(IsNothing(Contacto.cnc_tel2), "", Contacto.cnc_tel2)
                Txt_Tel_Tres.Text = IIf(IsNothing(Contacto.cnc_tel2), "", Contacto.cnc_tel3)

                Txt_Fax.Text = IIf(IsNothing(Contacto.cnc_fax), "", Contacto.cnc_fax)
                Txt_Mai.Text = IIf(IsNothing(Contacto.cnc_ema), "", Contacto.cnc_ema)

                If IsNothing(Contacto.cnc_def) Then
                    PorDefecto = "N"
                Else
                    PorDefecto = Contacto.cnc_def
                End If

                If PorDefecto = "S" Then
                    Me.CHB_Defecto.Checked = True
                Else
                    Me.CHB_Defecto.Checked = False
                End If

                If IsNothing(Contacto.cnc_not) Then
                    Notificacion = "N"
                Else
                    Notificacion = Contacto.cnc_not
                End If

                If Notificacion = "S" Then
                    Me.CHB_SCliente.Checked = True
                Else
                    Me.CHB_SCliente.Checked = False
                End If

                If Contacto.cnc_rep_leg = "N" Or Contacto.cnc_rep_leg = "" Then
                    Me.CHB_Rep.Checked = False
                Else
                    Me.CHB_Rep.Checked = True
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Private Sub Limpiar()

        Txt_Nom.Text = ""
        Txt_Car.Text = ""
        Txt_Rut.Text = ""
        Txt_Dig.Text = ""
        Txt_Dir.Text = ""
        Txt_Obs.Text = ""
        Txt_Tel_Uno.Text = ""
        Txt_Tel_Dos.Text = ""
        Txt_Tel_Tres.Text = ""
        Txt_Fax.Text = ""
        Txt_Mai.Text = ""
        NroContacto.Value = ""

        CHB_Defecto.Checked = False
        CHB_SCliente.Checked = False
        CHB_Rep.Checked = False
        NroContacto.Value = ""
        IB_Eliminar.Enabled = False
        IB_Guardar.Enabled = False

    End Sub

    Private Sub Accion(ByVal Estilo As String, ByVal Estado As Boolean)

        Txt_Nom.CssClass = Estilo
        Txt_Rut.CssClass = Estilo
        Txt_Dig.CssClass = Estilo
        Txt_Car.CssClass = Estilo
        Txt_Dir.CssClass = Estilo
        Txt_Obs.CssClass = Estilo
        Txt_Tel_Uno.CssClass = Estilo
        Txt_Tel_Dos.CssClass = Estilo
        Txt_Tel_Tres.CssClass = Estilo
        Txt_Fax.CssClass = Estilo
        Txt_Mai.CssClass = Estilo

        Txt_Nom.ReadOnly = Estado
        Txt_Rut.ReadOnly = Estado
        Txt_Dig.ReadOnly = Estado
        Txt_Car.ReadOnly = Estado
        Txt_Obs.ReadOnly = Estado
        Txt_Tel_Uno.ReadOnly = Estado
        Txt_Tel_Dos.ReadOnly = Estado
        Txt_Tel_Tres.ReadOnly = Estado
        Txt_Fax.ReadOnly = Estado
        Txt_Mai.ReadOnly = Estado

        CHB_Defecto.Enabled = Not Estado
        CHB_SCliente.Enabled = Not Estado
        CHB_Rep.Enabled = Not Estado

    End Sub

    Private Sub ActContactos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActContactos.Click
        CargaGrillas()
    End Sub

    Protected Sub GV_Contactos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Contactos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_Contactos, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_Contactos, 'formatable')")
            'e.Row.Attributes.Add("onClick", "celClickContacto(GV_Contactos, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim img As ImageButton = sender

        Me.NroContacto.Value = img.ToolTip


        CargaContacto()
        Accion("clsTxt", False)
        Txt_Nom.CssClass = "clsMandatorio"

        'For I = 0 To GV_Contactos.Rows.Count - 1

        '    If Val(Posicion.Value - 1) = I Then
        '        GV_Contactos.Rows(I).CssClass = "clicktable"
        '    Else
        '        GV_Contactos.Rows(I).CssClass = "formatable"
        '    End If

        'Next

        For I = 0 To GV_Contactos.Rows.Count - 1

            If (NroContacto.Value = GV_Contactos.Rows(I).Cells(0).Text) Then

                If (I Mod 2) = 0 Then
                    GV_Contactos.Rows(I).CssClass = "selectable"
                Else
                    GV_Contactos.Rows(I).CssClass = "selectableAlt"
                End If

            Else
                If (I Mod 2) = 0 Then
                    GV_Contactos.Rows(I).CssClass = "formatUltcell"
                Else
                    GV_Contactos.Rows(I).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

        IB_Eliminar.Enabled = True
        IB_Guardar.Enabled = True

        SW.Value = "UPDATE"


    End Sub

#End Region

#Region "BOTONERA"

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles IB_Guardar.Click

        Try

            If Not agt.ValidaAccesso(20, 20140101, Usr, "PRESIONO GUARDAR CONTACTO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Nom.Text.Trim = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar el Nombre del Contacto", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            'Msj.Mensaje(Me, Caption, "¿Está seguro de guardar contacto?", TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)
            LB_Guardar_Click(Me, e)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 
        If Not agt.ValidaAccesso(20, 20150101, Usr, "PRESIONO ELIMINAR CONTACTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If NroContacto.Value <> "" Or NroContacto.Value <> "0" Then
            'Msj.Mensaje(Me.Page, Caption, "¿Está seguro de eliminar contacto?", TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID, False)
            LB_Eliminar_Click(Me, e)
        Else
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un contacto a eliminar", TipoDeMensaje._Exclamacion, Nothing, False)
        End If

    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Not agt.ValidaAccesso(20, 20160101, Usr, "PRESIONO NUEVO CONTACTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Limpiar()
        IB_Guardar.Enabled = True

        Accion("clsTxt", False)
        Txt_Nom.CssClass = "clsMandatorio"
        SW.Value = "INSERT"

    End Sub

    Protected Sub DetalleCnc_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DetalleCnc.Click


        CargaContacto()
        Accion("clsTxt", False)
        Txt_Nom.CssClass = "clsMandatorio"

        For I = 0 To GV_Contactos.Rows.Count - 1

            If Val(Posicion.Value - 1) = I Then
                GV_Contactos.Rows(I).CssClass = "clicktable"
            Else
                GV_Contactos.Rows(I).CssClass = "formatable"
            End If

        Next
        IB_Eliminar.Enabled = True
        IB_Guardar.Enabled = True

        SW.Value = "UPDATE"


    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim Sesion As New ClsSession.ClsSession
            Dim ClsContacto As New ClaseClientes
            Dim CNC As New cnc_cls
            Dim Var As New FuncionesGenerales.Variables

            CNC.cnc_nom = UCase(Txt_Nom.Text.Trim)
            CNC.cnc_rut = UCase(Txt_Rut.Text.Trim.Replace(".", ""))
            CNC.cnc_dig = UCase(Txt_Dig.Text.Trim)
            
            CNC.cnc_car = UCase(Txt_Car.Text.Trim)
            CNC.cnc_dir = UCase(Txt_Dir.Text.Trim)
            CNC.cnc_obs = Txt_Obs.Text.Trim
            CNC.cnc_tel = Txt_Tel_Uno.Text.Trim
            CNC.cnc_tel2 = Txt_Tel_Dos.Text.Trim
            CNC.cnc_tel3 = Txt_Tel_Tres.Text.Trim
            CNC.cnc_fax = Txt_Fax.Text.Trim
            CNC.cnc_ema = Txt_Mai.Text.Trim
            CNC.cnc_def = CChar(IIf(CHB_Defecto.Checked, "S", "N"))
            CNC.cnc_not = CChar(IIf(CHB_SCliente.Checked, "S", "N"))
            CNC.cnc_rep_leg = CChar(IIf(CHB_Rep.Checked, "S", "N"))

            If Sesion.TipoDeContacto = 1 Then
                CNC.cnc_cli_ddr = "C"
                CNC.cli_idc = Format(CLng(RutDeuCli.Value.Trim), Var.FMT_RUT)
                CNC.deu_ide = Nothing
            Else
                CNC.cnc_cli_ddr = "D"
                CNC.cli_idc = Nothing
                CNC.deu_ide = Format(CLng(RutDeuCli.Value.Trim), Var.FMT_RUT)
            End If

            If SW.Value = "UPDATE" Then

                CNC.id_cnc = NroContacto.Value
                If ClsContacto.ContactoUpdate(CNC) Then
                    Msj.Mensaje(Me.Page, Caption, "Contacto Actualizado", ClsMensaje.TipoDeMensaje._Informacion, "", False)
                End If

            ElseIf SW.Value = "INSERT" Then

                CNC.id_cnc = Nothing
                If ClsContacto.ContactoInserta(CNC) Then
                    Msj.Mensaje(Me.Page, Caption, "Contacto Ingresado", ClsMensaje.TipoDeMensaje._Informacion, "", False)
                End If

            End If

            CargaGrillas()
            Limpiar()
            Accion("clsDisabled", True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click

        Try
            Dim Sesion As New ClsSession.ClsSession
            Dim Contacto As New ClaseClientes


            'Sesion.TipoDeContacto



            'If Request.QueryString("tipo") <> "" Then
            '    If Contacto.ContactoDelete(Variables.TipoDeContacto.Deudor, RutDeuCli.Value, NroContacto.Value) = True Then
            '        Msj.Mensaje(Page, Caption, "Contacto eliminado", ClsMensaje.TipoDeMensaje._Informacion)
            '    End If
            'Else

            '    If Contacto.ContactoDelete(Variables.TipoDeContacto.Cliente, RutDeuCli.Value, NroContacto.Value) = True Then
            '        Msj.Mensaje(Page, Caption, "Contacto eliminado", ClsMensaje.TipoDeMensaje._Informacion)
            '    End If
            'End If

            If Sesion.TipoDeContacto = 2 Then 'deudor
                If Contacto.ContactoDelete(Variables.TipoDeContacto.Deudor, RutDeuCli.Value, NroContacto.Value) = True Then
                    Msj.Mensaje(Page, Caption, "Contacto eliminado", ClsMensaje.TipoDeMensaje._Informacion)
                Else
                    Msj.Mensaje(Page, Caption, "No se pudo eliminar contacto", ClsMensaje.TipoDeMensaje._Informacion)
                End If
            ElseIf Sesion.TipoDeContacto = 1 Then

                If Contacto.ContactoDelete(Variables.TipoDeContacto.Cliente, RutDeuCli.Value, NroContacto.Value) = True Then
                    Msj.Mensaje(Page, Caption, "Contacto eliminado", ClsMensaje.TipoDeMensaje._Informacion)
                End If
            End If


            CargaGrillas()
            Limpiar()
            Accion("clsDisabled", True)



        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion >= 10 Then
                NroPaginacion -= 10
                CargaGrillas()
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GV_Contactos.Rows.Count < 10 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If GV_Contactos.Rows.Count = 10 Then
                NroPaginacion += 10
                CargaGrillas()
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig.TextChanged
        Try
            If Txt_Rut.Text.Trim() <> "" Then
                If Txt_Dig.Text.Trim() <> "" Then
                    If (Txt_Dig.Text.Trim().ToUpper() <> FG.Vrut(Txt_Rut.Text.Trim())) Then
                        Msj.Mensaje(Me, Caption, "Digito Verificador Incorrecto", 2)
                    End If
                Else
                    Msj.Mensaje(Me, Caption, "Debe ingresar digito verificador", 2)
                End If
            Else
                Txt_Rut.Text = ""
                Txt_Dig.Text = ""
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

End Class
