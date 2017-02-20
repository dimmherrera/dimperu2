Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Organigramas
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Sesion As New ClsSession.ClsSession
    Dim Var As New FuncionesGenerales.Variables
    Dim FC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Mantención de Organigrama"
    Dim Msj As New ClsMensaje
    Dim CLSCLI As New ClaseClientes
    Dim CG As New ConsultasGenerales
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim agt As New Perfiles.Cls_Principal

#End Region

#Region "EVENTOS"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Response.Expires = -1
            NroPaginacion = 0
            Try

                'RUT CLIENTE
                If Trim(Request.QueryString("rut")) <> "" Then
                    Dim Sesion As New ClsSession.ClsSession

                    Sesion.RutCli = CLng(Request.QueryString("rut"))
                    Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")

                    'NRO DE FILA DE LA COLLECTION EMPRESA
                    If Trim(Request.QueryString("id")) <> "" Then
                        'ACTUALIZA
                        sw.Value = "UPDATE"
                    Else
                        'NUEVO
                        sw.Value = "INSERT"
                    End If

                    CargaGrillaOrganigrama()
                    HabilitaTextos(True)

                    Txt_Rut.ReadOnly = True
                    Txt_Dig.ReadOnly = True

                    Txt_Rut.CssClass = "clsDisabled"
                    Txt_Dig.CssClass = "clsDisabled"

                End If

            Catch ex As Exception
                'Msj.Mensaje(Me.Page,ex.Message, TipoMsg.Errores)
            End Try

        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Private Sub CargaOrganigrama()


        Dim Sesion As New ClsSession.ClsSession
        Dim RG As New FuncionesGenerales.FComunes
        
        Try

            Txt_Rut.ReadOnly = True
            Txt_Dig.ReadOnly = True

            Txt_Rut.CssClass = "clsDisabled"
            Txt_Dig.CssClass = "clsDisabled"

            HabilitaTextos(False)
            
            Dim Org As org_cls = CLSCLI.OrganigramaDevuelvePorCliente(Sesion.RutCli, RutOrg.Value)

            If RutOrg.Value <> "" Then
                IB_Eliminar.Enabled = False

                Txt_Rut.Text = RG.FormatoMiles(Org.org_rut)
                Txt_Dig.Text = RG.Vrut(Replace(Txt_Rut.Text, ".", ""))

                Txt_Nom.Text = Org.org_nom
                Txt_Car.Text = Org.org_car
                Txt_Atb.Text = Org.org_atb
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Private Sub CargaGrillaOrganigrama()

        Try

            CLSCLI.OrganigramaDevuelveTodos(GV_Organigramas, ClsSession.ClsSession.RutCli)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try
    End Sub

    Protected Sub GV_Organigramas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Organigramas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_Organigramas, 'selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_Organigramas, 'formatable')")
            'e.Row.Attributes.Add("onClick", "celClickDosOrganigrama(GV_Organigramas, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub ActOrganigrama_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActOrganigrama.Click
        Try

            sw.Value = "UPDATE"
            CargaOrganigrama()
            'MarcaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try
    End Sub

    Private Sub HabilitaTextos(ByVal Estado As Boolean)

        'Txt_Rut.ReadOnly = Estado
        'Txt_Dig.ReadOnly = Estado

        Txt_Nom.ReadOnly = Estado
        Txt_Car.ReadOnly = Estado
        Txt_Atb.ReadOnly = Estado

        If Estado Then
            Txt_Nom.CssClass = "clsDisabled"
            Txt_Car.CssClass = "clsDisabled"
            Txt_Atb.CssClass = "clsDisabled"
            Txt_Nom.Text = ""
            Txt_Car.Text = ""
            Txt_Atb.Text = ""
            Txt_Rut_MaskedEditExtender.Enabled = False
            IB_Guardar.Enabled = False
            IB_Eliminar.Enabled = False
            Txt_Rut.Text = ""
            Txt_Dig.Text = ""
        Else
            Txt_Nom.CssClass = "clsMandatorio"
            Txt_Car.CssClass = "clsMandatorio"
            Txt_Atb.CssClass = "clsMandatorio"
            Txt_Nom.Text = ""
            Txt_Car.Text = ""
            Txt_Atb.Text = ""
            Txt_Rut.Text = ""
            Txt_Dig.Text = ""
            Txt_Rut_MaskedEditExtender.Enabled = True
            IB_Guardar.Enabled = True
        End If

    End Sub

     
    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig.TextChanged
        Try

            Dim RG As New FuncionesGenerales.FComunes
            If RG.Vrut(Replace(Txt_Rut.Text, ".", "")) <> UCase(Txt_Dig.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Nit incorrecto", TipoDeMensaje._Exclamacion, "", False)
                Exit Sub
            End If
            Txt_Nom.Focus()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub
#End Region

#Region "Botonera"

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Try
            If Not agt.ValidaAccesso(20, 20110101, Usr, "PRESIONO GUARDAR ORGANIGRAMA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            If Txt_Rut.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese NIT", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            If Txt_Dig.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese digito", TipoDeMensaje._Informacion, "", False)
                Exit Sub
            End If

            If Trim(Txt_Nom.Text) = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese nombre", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Trim(Txt_Car.Text) = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese cargo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Trim(Txt_Atb.Text) = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese atributo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If FC.Vrut(Replace(Txt_Rut.Text, ".", "")) <> UCase(Txt_Dig.Text) Then
                Msj.Mensaje(Me.Page, Caption, "NIT incorrecto", TipoDeMensaje._Exclamacion, "", False)
            Else
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Eliminar.Click
        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        Try


            If Not agt.ValidaAccesso(20, 20120101, Usr, "PRESIONO ELIMINAR ORGANIGRAMA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Sesion As New ClsSession.ClsSession

            If boton_ver.ToolTip <> "" Then
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de eliminar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID, False)
            Else
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una empresa a eliminar", ClsMensaje.TipoDeMensaje._Exclamacion, "", False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try


    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20130101, Usr, "PRESIONO NUEVO ORGANIGRAMA") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        SW.Value = "INSERT"
        Txt_Rut.ReadOnly = False
        Txt_Dig.ReadOnly = False

        Txt_Rut.CssClass = "clsMandatorio"
        Txt_Dig.CssClass = "clsMandatorio"

        HabilitaTextos(False)
        RutOrg.Value = ""
        Txt_Rut.Focus()

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Dim ORG As New org_cls

            ORG.cli_idc = Format(CLng(Sesion.RutCli), Var.FMT_RUT)
            ORG.org_rut = Txt_Rut.Text
            ORG.org_nom = UCase(Txt_Nom.Text)
            ORG.org_car = UCase(Txt_Car.Text)
            ORG.org_atb = UCase(Txt_Atb.Text)

            If sw.Value = "UPDATE" Then
                If CLSCLI.OrganigramaUpdate(ORG) Then
                    'Msj.Mensaje(Me.Page, Caption, "Organigrama Actualizado", TipoDeMensaje._Informacion, "", False)
                Else
                    'Msj.Mensaje(Me.Page, Caption, "Organigrama no se pudo actualizar", TipoDeMensaje._Informacion, "", False)
                End If
            ElseIf sw.Value = "INSERT" Then
                If CLSCLI.OrganigramaInserta(ORG) Then
                    'Msj.Mensaje(Me.Page, Caption, "Organigrama Insertado", TipoDeMensaje._Informacion, "", False)
                Else
                    'Msj.Mensaje(Me.Page, Caption, "Organigrama no se pudo insertar", TipoDeMensaje._Informacion, "", False)
                End If
            End If

            CargaGrillaOrganigrama()
            HabilitaTextos(True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click

        Try

            CLSCLI.OrganigramaDelete(RutOrg.Value, ClsSession.ClsSession.RutCli)

            CargaGrillaOrganigrama()
            HabilitaTextos(True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 10 Then
            NroPaginacion -= 10
            CargaGrillaOrganigrama()

        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        If GV_Organigramas.Rows.Count < 10 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Organigramas.Rows.Count = 10 Then
            NroPaginacion += 10
            CargaGrillaOrganigrama()
        End If

    End Sub
#End Region

  
  
    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim rg As New FuncionesGenerales.FComunes
        Dim boton_ver As ImageButton = CType(sender, ImageButton)

        RutOrg.Value = FC.LimpiaRut(boton_ver.ToolTip)

        Try

            For I = 0 To GV_Organigramas.Rows.Count - 1

                If (Format(CLng(boton_ver.ToolTip), FMT.FCMSD) & "-" & FC.Vrut(CInt(boton_ver.ToolTip)) = GV_Organigramas.Rows(I).Cells(1).Text) Then
                    IB_Eliminar.Enabled = True

                    Txt_Rut.Text = (GV_Organigramas.Rows(I).Cells(1).Text)
                    Txt_Nom.Text = (GV_Organigramas.Rows(I).Cells(2).Text)
                    Txt_Car.Text = (GV_Organigramas.Rows(I).Cells(3).Text)
                    Txt_Atb.Text = (GV_Organigramas.Rows(I).Cells(4).Text)
                    Txt_Rut.Text = rg.FormatoMiles(boton_ver.ToolTip)
                    Txt_Dig.Text = rg.Vrut(Replace(Txt_Rut.Text, ".", ""))

                    RutOrg.Value = FC.LimpiaRut(GV_Organigramas.Rows(I).Cells(1).Text)

                    If (I Mod 2) = 0 Then
                        GV_Organigramas.Rows(I).CssClass = "selectable"
                    Else
                        GV_Organigramas.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_Organigramas.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Organigramas.Rows(I).CssClass = "formatUltcellAlt"

                    End If

                End If
            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

   

End Class
