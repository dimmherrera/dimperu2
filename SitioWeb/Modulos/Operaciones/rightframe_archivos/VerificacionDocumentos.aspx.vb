Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_VerificacionDocumentos
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim Sesion As New ClsSession.ClsSession
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FC As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Verificación de Documentos"
    Dim AG As New ActualizacionesGenerales
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim ClsCli As New ClaseClientes
    Dim documentos As Collection

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then
                Response.Expires = -1
                CargaDrop()
                Txt_Rut_Cli.Focus()
            End If

            IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaCli.aspx','Ayuda',640,400,100,100);")

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If Not IsPostBack Then
            Modulo = "Operacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If

    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            'If Txt_Nro_ope.Text.Trim() = "" Then
            '    Msj.Mensaje(Me.Page, Caption, "Debe ingresar el numero de la operación a buscar", TipoDeMensaje._Exclamacion)
            '    Txt_Rut_Cli.Focus()
            '    Exit Sub
            'End If

            Txt_Nro_ope.ReadOnly = True
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True

            Txt_Nro_ope.CssClass = "clsDisabled"
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"

            Session("documentos") = New Collection
            NroPaginacion = 0
            HF_NroOpe.Value = 0

            CargaGrillaOperaciones()

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try
            If Trim(Txt_Rut_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar Identificación", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If
            If Trim(Txt_Dig_Cli.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar Dígito verificador", TipoDeMensaje._Exclamacion)
                Txt_Dig_Cli.Focus()
                Exit Sub
            End If
            If UCase(Txt_Dig_Cli.Text) <> FC.Vrut(Trim(Replace(Txt_Rut_Cli.Text, ",", ""))).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "Identificación incorrecta", TipoDeMensaje._Exclamacion)
                Txt_Rut_Cli.Focus()
                Exit Sub
            End If

            ClienteDevuelve()

        Catch ex As Exception

        End Try
    End Sub

    Sub Limpia()

        Txt_Rut_Cli.Focus()
        Txt_Rut_Cli.Text = ""
        Txt_Rut_Cli.CssClass = "clsDisabled"
        Txt_Rut_Cli.ReadOnly = False
        Txt_Dig_Cli.Text = ""
        Txt_Dig_Cli.CssClass = "clsDisabled"
        Txt_Dig_Cli.ReadOnly = False
        Txt_Raz_Soc.Text = ""
        Txt_Raz_Soc.CssClass = "clsDisabled"
        Txt_Nro_ope.Text = ""
        Txt_Raz_Soc.ReadOnly = False
        Txt_Rut_Cli.CssClass = "clsMandatorio"
        Txt_Dig_Cli.CssClass = "clsMandatorio"

        Txt_Nro_ope.ReadOnly = False
        Txt_Nro_ope.CssClass = "clsMandatorio"
        Txt_Nro_ope.Text = ""

        IB_AyudaCli.Enabled = True
        Txt_Rut_Cli_MaskedEditExtender.Enabled = True

        GV_Operaciones.DataSource = Nothing
        GV_Operaciones.DataBind()

        Gr_DocCom.DataSource = Nothing
        Gr_DocCom.DataBind()

    End Sub

    Sub CargaDrop()

        Try

            'CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Tip_Mon)
            'CG.ParametrosDevuelve(TablaParametro.EstadoVerificacion, True, Dp_Est_Ver)
            'CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_Tip_Doc)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Sub ClienteDevuelve()

        Try

            'Se elimina la function VerificaClienteDevuelve por existir una funcion que hace lo mismo ClientesDevuelve

            Dim CLSCLI As New ClaseClientes
            Dim Cliente As cli_cls

            Cliente = CLSCLI.ClientesDevuelve(CLng(Txt_Rut_Cli.Text), Txt_Dig_Cli.Text)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Informacion)
                Exit Sub
            Else

                Txt_Raz_Soc.CssClass = "clsDisabled"
                Txt_Rut_Cli.CssClass = "clsDisabled"
                Me.Txt_Rut_Cli.ReadOnly = True
                Me.Txt_Dig_Cli.CssClass = "clsDisabled"
                Me.Txt_Dig_Cli.ReadOnly = True

                Txt_Raz_Soc.ReadOnly = True
                IB_AyudaCli.Enabled = False
                Txt_Rut_Cli_MaskedEditExtender.Enabled = False

            End If

            With Cliente
                If CInt(Cliente.id_P_0044) = 1 Then
                    'NATURAL
                    Txt_Raz_Soc.Text = .cli_rso.Trim.ToUpper & " " & .cli_ape_ptn.Trim.ToUpper & " " & .cli_ape_mtn.Trim.ToUpper
                Else
                    'JURIDICO
                    Txt_Raz_Soc.Text = .cli_rso.Trim.ToUpper
                End If
            End With

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click
        Limpia()
    End Sub

    Private Sub LLenaDocumentacion()

        Dim a = HF_NroOpe.Value

        Dim doctos As IQueryable = CG.DocConDevuelvePorNegociacion(HF_NroOpe.Value, DP_Tipo.SelectedValue)

        Me.Gr_DocCom.DataSource = doctos
        Me.Gr_DocCom.DataBind()

        Dim cb As CheckBox

        documentos = Session("documentos")

        For Each d In doctos

            For i = 0 To Gr_DocCom.Rows.Count - 1

                cb = CType(Gr_DocCom.Rows(i).FindControl("CB_DOC"), CheckBox)

                If cb.ToolTip = d.id And d.estado = "A" Then

                    Dim existe As Boolean = False

                    For x = 1 To documentos.Count
                        If documentos.Item(x) = cb.ToolTip Then
                            existe = True
                            Exit For
                        End If
                    Next

                    If Not existe Then
                        documentos.Add(cb.ToolTip)
                    End If

                    cb.Checked = True
                    Exit For

                End If

            Next

        Next

        If Gr_DocCom.Rows.Count <= 0 Then
            Msj.Mensaje(Me.Page, "Verificación", "No se encuentran Documentación a Verificar", ClsMensaje.TipoDeMensaje._Informacion)
        End If

        Session("documentos") = documentos

    End Sub

    Protected Sub DP_Tipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Tipo.SelectedIndexChanged
        LLenaDocumentacion()
    End Sub

    Protected Sub IB_TodosDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        documentos = Session("documentos")

        Dim cb As New CheckBox

        For i = 0 To Gr_DocCom.Rows.Count - 1
            cb = Gr_DocCom.Rows(i).FindControl("CB_DOC")
            If Not documentos.Contains(cb.ToolTip) Then
                documentos.Add(cb.ToolTip)
                cb.Checked = True
            End If
        Next

        Session("documentos") = documentos

    End Sub

    Protected Sub CB_DOC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Agrega o quita documento de collection
        documentos = Session("documentos")
        Dim cb As CheckBox = sender

        If cb.Checked Then

            Dim existe As Boolean = False

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    existe = True
                    Exit For
                End If
            Next

            If existe Then
                Exit Sub
            Else
                documentos.Add(cb.ToolTip)
            End If

        Else

            For I = 1 To documentos.Count
                If documentos.Item(I) = cb.ToolTip Then
                    documentos.Remove(I)
                    Exit For
                End If
            Next

        End If

        Session("documentos") = documentos

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Msj.Mensaje(Me.Page, "Verificación", "¿Está seguro de guadar?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID)

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click
        documentos = Session("documentos")

        'Limpiamos todos antes de actualizar los aprobados
        AG.DocComsPorOperacionAoR_LImpia(HF_NroOpe.Value)

        For x = 1 To documentos.Count

            Dim dxn As New dxn_cls

            dxn.id_dxn = documentos(x).ToString()
            dxn.id_opn = HF_NroOpe.Value
            dxn.id_eje = CodEje
            dxn.est_dxd = "A"
            dxn.dxn_fec_apb = Date.Now

            AG.DocComsPorOperacionAoR_Actualiza(dxn)

        Next

        Msj.Mensaje(Me.Page, "Verificación", "Confirmación de Documentos Guardados para esta Operación", ClsMensaje.TipoDeMensaje._Informacion)

        LLenaDocumentacion()
        MarcaGrillaOperaciones()

    End Sub

    Private Sub CargaGrillaOperaciones()

        Try

            Dim OP As New ClaseOperaciones
            Dim Ejecutivo_Desde As Integer = 0
            Dim Ejecutivo_Hasta As Integer = 999999999
            Dim Cliente_Desde As Long
            Dim Cliente_Hasta As Long
            Dim Operacion_Desde As Integer = 0
            Dim Operacion_Hasta As Integer = 999999999

            If Txt_Rut_Cli.Text <> "" Then
                Cliente_Desde = Txt_Rut_Cli.Text
                Cliente_Hasta = Txt_Rut_Cli.Text
            Else
                Cliente_Desde = 0
                Cliente_Hasta = 9999999999999
            End If

            If Txt_Nro_ope.Text <> "" Then
                Operacion_Desde = Txt_Nro_ope.Text
                Operacion_Hasta = Txt_Nro_ope.Text
            Else
                Operacion_Desde = 0
                Operacion_Hasta = 999999999
            End If

            coll_ope = New Collection
            
            coll_ope = OP.OperacionesTodasDevuelve(Cliente_Desde, Cliente_Hasta, _
                                                                                         Ejecutivo_Desde, Ejecutivo_Hasta, _
                                                                                         1, 1, _
                                                                                         "01-01-1900", "01-01-2100", _
                                                                                         0, 999, _
                                                                                         0, 999, _
                                                                                         Operacion_Desde, Operacion_Hasta, _
                                                                                         5)

            If coll_ope.Count = 0 Then
                Msj.Mensaje(Me.Page, Caption, "No se encontraron operaciones ingresadas o simuladas", ClsMensaje.TipoDeMensaje._Informacion)
            Else

              
                GV_Operaciones.DataSource = coll_ope
                GV_Operaciones.DataBind()

                Dim Formato_Moneda As String = ""
                Dim Fmt As New FuncionesGenerales.ClsLocateInfo

                For I = 0 To GV_Operaciones.Rows.Count - 1


                    GV_Operaciones.Rows(I).Cells(2).Text = FC.FormatoMiles(GV_Operaciones.Rows(I).Cells(2).Text) & "-" & coll_ope.Item(I + 1).Digito

                    Select Case coll_ope.Item(I + 1).ID_p_0023
                        Case 1 : Formato_Moneda = Fmt.FCMSD
                        Case 2 : Formato_Moneda = Fmt.FCMCD4
                        Case 3, 4 : Formato_Moneda = Fmt.FCMCD
                    End Select

                    GV_Operaciones.Rows(I).Cells(8).Text = Format(CDbl(GV_Operaciones.Rows(I).Cells(8).Text), Formato_Moneda)

                    

                Next

                MarcaGrillaOperaciones()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion = 0 Then
            Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        HF_NroOpe.Value = ""
        Gr_DocCom.DataSource = Nothing
        Gr_DocCom.DataBind()

        If NroPaginacion >= 5 Then
            NroPaginacion -= 5
            CargaGrillaOperaciones()
        End If

    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        If GV_Operaciones.Rows.Count < 5 Then
            Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        HF_NroOpe.Value = ""
        Gr_DocCom.DataSource = Nothing
        Gr_DocCom.DataBind()

        If GV_Operaciones.Rows.Count = 5 Then
            NroPaginacion += 5
            CargaGrillaOperaciones()
        End If


    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim img_ver As ImageButton = CType(sender, ImageButton)

            HF_NroOpe.Value = img_ver.ToolTip

            If HF_NroOpe.Value = "" Then
                Msj.Mensaje(Me, "Atención", "Debe seleccionar una operación", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            MarcaGrillaOperaciones()

            LLenaDocumentacion()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub MarcaGrillaOperaciones()

        Try

            For I = 0 To GV_Operaciones.Rows.Count - 1

                If HF_NroOpe.Value = GV_Operaciones.Rows(I).Cells(1).Text Then
                    If (I Mod 2) = 0 Then
                        GV_Operaciones.Rows(I).CssClass = "selectable"
                    Else
                        GV_Operaciones.Rows(I).CssClass = "selectableAlt"
                    End If
                Else
                    If (I Mod 2) = 0 Then
                        GV_Operaciones.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Operaciones.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
        
    End Sub

End Class
