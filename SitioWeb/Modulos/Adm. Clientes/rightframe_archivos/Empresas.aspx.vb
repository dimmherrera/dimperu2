Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Empresas
    Inherits System.Web.UI.Page

#Region "declaracion de variables"

    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Caption As String = "Mantención de Empresas"
    Dim Msj As New ClsMensaje
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

                    Txt_Rut.Attributes.Add("Style", "TEXT-ALIGN: right")
                    Sesion.RutCli = CLng(Request.QueryString("rut"))

                    'NRO DE FILA DE LA COLLECTION EMPRESA
                    If Trim(Request.QueryString("id")) <> "" Then
                        'ACTUALIZA
                        sw.Value = "UPDATE"
                        CargaEmpresa()
                    Else
                        'NUEVO
                        sw.Value = "INSERT"
                    End If

                    Me.CargaGrillas()
                    HabilitaDeshabilita(True)

                End If

            Catch ex As Exception
                'Msj.Mensaje(Me.Page,ex.Message, TipoMsg.Errores)
            End Try

            IB_Volver.Attributes.Add("onClick", "javascript:window.close()")
            'IB_Detalle.Attributes.Add("onClik", "javascript:openseleccionDosEmpresa();")

        End If

    End Sub

    Protected Sub Txt_Dig_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig.TextChanged
        Try

            Dim RG As New FuncionesGenerales.FComunes
            If RG.Vrut(Replace(Txt_Rut.Text, ".", "")) <> UCase(Txt_Dig.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Nit incorrecto", TipoDeMensaje._Exclamacion, "", False)
                Exit Sub
            End If
            Txt_Des.Focus()

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    


    Private Sub HabilitaDeshabilita(ByVal Estado As Boolean)

        Txt_Rut.ReadOnly = Estado
        Txt_Dig.ReadOnly = Estado
        Txt_Des.ReadOnly = Estado

        If Estado Then
            Txt_Rut.CssClass = "clsDisabled"
            Txt_Dig.CssClass = "clsDisabled"
            Txt_Des.CssClass = "clsDisabled"
            Txt_Rut_MaskedEditExtender.Enabled = False
            IB_Guardar.Enabled = False
            IB_Eliminar.Enabled = False

            Txt_Rut.Text = ""
            Txt_Dig.Text = ""
            Txt_Des.Text = ""
        Else
            Txt_Rut.CssClass = "clsMandatorio"
            Txt_Dig.CssClass = "clsMandatorio"
            Txt_Des.CssClass = "clsMandatorio"
            Txt_Rut_MaskedEditExtender.Enabled = True
            IB_Guardar.Enabled = True
            'IB_Eliminar.Enabled = True
            Txt_Rut.Text = ""
            Txt_Dig.Text = ""
            Txt_Des.Text = ""
        End If


    End Sub

    Private Sub CargaEmpresa()

        Dim ClsEmpresa As New ClaseClientes
        Dim Sesion As New ClsSession.ClsSession
        Dim RG As New FuncionesGenerales.FComunes
        
        Try

            Txt_Rut.ReadOnly = True
            Txt_Dig.ReadOnly = True

            Txt_Rut.CssClass = "clsDisabled"
            Txt_Dig.CssClass = "clsDisabled"

            Dim emp As emp_cls = ClsEmpresa.EmpresasDevuelvePorCliente(Sesion.RutCli, RutEmp.Value)
            If Not IsNothing(emp) Then
                Txt_Rut.Text = RG.FormatoMiles(CDbl(emp.emp_rut))
                Txt_Dig.Text = RG.Vrut(Replace(Txt_Rut.Text, ".", ""))
                Txt_Des.Text = emp.emp_des
                Txt_Des.Enabled = True
            End If

            'SetFocus(Me, Txt_Des)

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

    Public Sub CargaGrillas()

        Dim ClsEmpresa As New ClaseClientes
        Dim Sesion As New ClsSession.ClsSession

        Try


            GV_Empresas.DataSource = Nothing
            'GRILLA DE EMPRESAS ASOCIADAS AL CLIENTE
            ClsEmpresa.EmpresasDevuelveTodos(GV_Empresas, ClsSession.ClsSession.RutCli)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Private Sub ActEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActEmpresa.Click

        Try

            sw.Value = "UPDATE"
            HabilitaDeshabilita(False)
            CargaEmpresa()
            'MarcaGrilla()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try
    End Sub

    Protected Sub GV_Empresas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Empresas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '    e.Row.Attributes.Add("onMouseOver", "J_RolClass_Tabla(GV_Empresas, 'selectable')")
            '    e.Row.Attributes.Add("onMouseOut", "J_RolClass_Tabla(GV_Empresas, 'formatable')")
            '    e.Row.Attributes.Add("onClick", "celClickDosEmpresa(GV_Empresas, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

   

#End Region

#Region "Botonera"

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Eliminar.Click

        Try

            If Not agt.ValidaAccesso(20, 20090101, Usr, "PRESIONO ELIMINAR EMPRESA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Sesion As New ClsSession.ClsSession

            If RutEmp.Value <> "" Then
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de eliminar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID, False)
            Else
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar una empresa a eliminar", ClsMensaje.TipoDeMensaje._Exclamacion, "", False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Nuevo.Click

        If Not agt.ValidaAccesso(20, 20100101, Usr, "PRESIONO NUEVA EMPRESA") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Txt_Rut.Focus()
        HabilitaDeshabilita(False)
        sw.Value = "INSERT"
    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Sesion As New ClsSession.ClsSession
        Dim RG As New FuncionesGenerales.FComunes

        Try

            If Not agt.ValidaAccesso(20, 20080101, Usr, "PRESIONO GUARDAR EMPRESA") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Txt_Rut.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese NIT", TipoDeMensaje._Exclamacion, "", False)
                Txt_Rut.Focus()
                Exit Sub
            End If

            If Txt_Dig.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese digito verificador", TipoDeMensaje._Exclamacion, "", False)
                Exit Sub
            End If

            If Txt_Des.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Ingrese razón social", TipoDeMensaje._Exclamacion, "", False)
                Exit Sub
            End If
            If RG.Vrut(Replace(Txt_Rut.Text, ".", "")) <> UCase(Txt_Dig.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Nit incorrecto", TipoDeMensaje._Exclamacion, "", False)
            Else
                Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)
            End If


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click

        Try


            Dim Sesion As New ClsSession.ClsSession
            Dim Empresa As New ClaseClientes
            Dim EMP As New emp_cls

            EMP.emp_rut = RutEmp.Value
            EMP.cli_idc = ClsSession.ClsSession.RutCli

            If Empresa.EmpresaDelete(EMP) Then
                Msj.Mensaje(Me.Page, Caption, "Empresa eliminada", TipoDeMensaje._Informacion, "", False)
                CargaGrillas()
                HabilitaDeshabilita(True)
            Else
                Msj.Mensaje(Me.Page, Caption, "Empresa no se pudo eliminar", TipoDeMensaje._Informacion, "", False)
            End If




        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try


    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsEmpresa As New ClaseClientes

        Try


         
            Dim EMP As New emp_cls
            Dim Var As New FuncionesGenerales.Variables

            EMP.emp_rut = Format(CLng(Txt_Rut.Text), Var.FMT_RUT)
            EMP.emp_des = UCase(Txt_Des.Text)
            EMP.cli_idc = Format(CLng(Sesion.RutCli), Var.FMT_RUT)

            If sw.Value = "UPDATE" Then
                If ClsEmpresa.EmpresaUpdate(EMP) Then
                    Msj.Mensaje(Me.Page, Caption, "Empresa actualizada", TipoDeMensaje._Informacion, "", False)
                End If
            ElseIf sw.Value = "INSERT" Then
                EMP.id_emp = Nothing
                If ClsEmpresa.EmpresaInserta(EMP) Then
                    Msj.Mensaje(Me.Page, Caption, "Empresa ingresada", TipoDeMensaje._Informacion, "", False)
                End If
            End If

            'If Empresa.CodError <> 99 Then
            'CloseModal(Me, "ctl00$ContentPlaceHolder1$TabContainer1$TabPanel1$Empresas1$ActEmpresa")
            'CloseModal(Me, "ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_Empresas1_ActEmpresa")

            CargaGrillas()
            HabilitaDeshabilita(True)
            'End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    
    End Sub

#End Region

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion >= 10 Then
            NroPaginacion -= 10
            CargaGrillas()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        If GV_Empresas.Rows.Count < 10 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If GV_Empresas.Rows.Count = 10 Then
            NroPaginacion += 10
            CargaGrillas()
        End If
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim FC As New FuncionesGenerales.FComunes
        Dim boton_ver As ImageButton = CType(sender, ImageButton)


        Try

            HF_Pos.Value = boton_ver.ToolTip
            
            For I = 0 To GV_Empresas.Rows.Count - 1
                If (boton_ver.ToolTip = GV_Empresas.Rows(I).Cells(1).Text) Then
                    IB_Eliminar.Enabled = True
                    If (I Mod 2) = 0 Then

                        GV_Empresas.Rows(I).CssClass = "selectable"
                    Else
                        GV_Empresas.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_Empresas.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Empresas.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If
                RutEmp.Value = FC.LimpiaRut(GV_Empresas.Rows(I).Cells(2).Text)

                If (HF_Pos.Value = GV_Empresas.Rows(I).Cells(1).Text) Then
                    Txt_Rut.Text = (GV_Empresas.Rows(I).Cells(2).Text)
                    Txt_Des.Text = (GV_Empresas.Rows(I).Cells(3).Text)
                    Txt_Rut.Text = FC.FormatoMiles(RutEmp.Value)
                    Txt_Dig.Text = FC.Vrut(Replace(Txt_Rut.Text, ".", ""))



                End If

            Next
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try

    End Sub
End Class
