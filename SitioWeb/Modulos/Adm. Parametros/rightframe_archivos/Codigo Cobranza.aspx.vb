Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ClsCodigoCobranza
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim cbz As New ClaseCobranza

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

     
        If Not IsPostBack Then

            SW = 0

            Coll_Cobranza = New Collection
            CargaGrilla()

        End If

    End Sub


    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20021212, Usr, "PRESIONO NUEVO ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        SW = 1
        Limpiar()

        'ASIGNAR ESTILO A CAJA DE TEXTO
        Me.Txt_CodCobranza.Enabled = True
        Me.Txt_CodCobranza.CssClass = "clsMandatorio"
        Me.Txt_CodCobranza.Focus()
    End Sub


    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20011212, Usr, "PRESIONO GUARDAR ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim CheckGenerar As String
        Dim FechaGestion As String
        Dim Gestion As String



        'VALIDA
        If SW = 0 Then

            If Txt_CodCobranzaOculto.Text = "" And Not Txt_CodCobranza.Text = "" Then
                Txt_CodCobranzaOculto.Text = Txt_CodCobranza.Text

            ElseIf Not Txt_CodCobranzaOculto.Text = "" And Txt_CodCobranza.Text = "" Then
                Txt_CodCobranza.Text = Txt_CodCobranzaOculto.Text

            ElseIf Txt_CodCobranzaOculto.Text = "" And Txt_CodCobranza.Text = "" Then
                msj.Mensaje(Me, "Atencion", "Selecciones registro que desea actualizar o ingrese uno nuevo, presione botón nuevo", 3)
                Exit Sub
            End If


        ElseIf SW = 1 Then

            If Txt_CodCobranzaOculto.Text = "" And Txt_CodCobranza.Text = "" Then
                msj.Mensaje(Me, "Atencion", "Ingrese código de cobranza", 3)
                Exit Sub
            End If

        End If


        If Me.Txt_Prioridad.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Ingrese prioridad", 3)
            Exit Sub
        End If

        If Txt_Descripcion.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Ingrese descripción", 3)
            Exit Sub
        End If





        If Me.Chk_Generar.Checked Then
            CheckGenerar = "N"
        Else
            CheckGenerar = "S"
        End If

        If Me.RB_GestionarSi.Checked Then
            Gestion = "S"
        ElseIf Me.RB_GestionarNo.Checked Then
            Gestion = "N"
        Else
            Gestion = Nothing
        End If

        If RB_FechaVen.Checked Then
            FechaGestion = "V"
        ElseIf RB_FechaGest.Checked Then
            FechaGestion = "G"
        Else
            FechaGestion = Nothing
        End If


        'GUARDA DATOS
        If SW = 1 Then

            If Existe_Prioridad() Then
                msj.Mensaje(Me, "Atencion", "Prioridad  '" & Txt_Prioridad.Text & "'  ya existe, debe ingresar otra prioridad", 3)
                Txt_Prioridad.Text = ""
                Exit Sub
            End If
            Dim str As String

            str = ag.guarda_datos_cobranza(Format(CLng(Me.Txt_CodCobranza.Text), "0000"), Me.Txt_Accion.Text, Me.Txt_Descripcion.Text, Gestion, CheckGenerar, _
                                        Me.Txt_Cod_Acc_Nvo.Text, FechaGestion, Me.Txt_Prioridad.Text, Val(Me.Txt_PlazoDias.Text))


            Limpiar()


            ''CARGA GRILLA
            CargaGrilla()

            msj.Mensaje(Me, "Atencion", str, 2)
            Deshabilita()
        Else

            ag.Actualiza_datos_cobranza(Format(CLng(Me.Txt_CodCobranza.Text), "0000"), Me.Txt_Accion.Text, Me.Txt_Descripcion.Text, Gestion, CheckGenerar, _
                                         Me.Txt_Cod_Acc_Nvo.Text, FechaGestion, Me.Txt_Prioridad.Text, Val(Me.Txt_PlazoDias.Text))
            Limpiar()

            CargaGrilla()

            msj.Mensaje(Me, "Atencion", "Registro actualizado", 2)
            Deshabilita()
        End If




    End Sub


    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim Agt As New Perfiles.Cls_Principal



        SW = 0

        'LIMPIA Y DESHABILITA CAJAS
        Limpiar()
        Deshabilita()

    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20031212, Usr, "PRESIONO ELIMINAR ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        If Me.pos.Value = "" Or Txt_Prioridad.Text = "" Or Txt_Descripcion.Text = "" Then
            msj.Mensaje(Me, "Atencion", "Seleccione datos a eliminar", 3)
            Exit Sub
        End If
        If cbz.BuscaCodigoCobranza(Me.Txt_CodCobranza.Text) = True Then
            msj.Mensaje(Me, "Atencion", "Este Código está asignado a un Documento", 3)
            Exit Sub
        End If

        ag.elimina_cod_cobranza(Format(CLng(Me.Txt_CodCobranza.Text), "0000"))



        'LIMPIA Y DESHABILITA CAJAS
        Limpiar()
        Deshabilita()

        'CARGA GRILLA
        CargaGrilla()
        msj.Mensaje(Me, "Atencion", "Registro eliminado", 3)

    End Sub

    'Protected Sub Gr_Cobranza_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Cobranza.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
    '        e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
    '        e.Row.Attributes.Add("onClick", "SeleccionaGrCobranza(ctl00_ContentPlaceHolder1_Gr_Cobranza, 'clicktable', 'formatable', 'selectable')")
    '    End If

    'End Sub


    Sub Limpiar()

        Me.Txt_Accion.Text = ""
        Me.Txt_Cod_Acc_Nvo.Text = ""
        Me.Txt_CodCobranza.Text = ""
        Me.Txt_CodCobranzaOculto.Text = ""
        Me.Txt_Descripcion.Text = ""
        Me.Txt_PlazoDias.Text = ""
        Me.Txt_Prioridad.Text = ""

        Me.RB_FechaGest.Checked = False
        Me.RB_FechaVen.Checked = False
        Me.RB_GestionarSi.Checked = False
        Me.RB_GestionarNo.Checked = False
        Me.Chk_Generar.Checked = False

        For i = 0 To Gr_Cobranza.Rows.Count - 1
            Gr_Cobranza.Rows(i).CssClass = "selectableAlt"
            If (i Mod 2) = 0 Then
                Gr_Cobranza.Rows(i).CssClass = "formatUltcell"
            Else
                Gr_Cobranza.Rows(i).CssClass = "formatUltcellAlt"
            End If
        Next

    End Sub

    Sub Deshabilita()

        Me.Txt_CodCobranza.Enabled = False
        Me.Txt_CodCobranza.CssClass = ""

    End Sub

    Sub CargaGrilla()


        Coll_Cobranza = cbz.CobranzaDevuelve
        Me.Gr_Cobranza.DataSource = Coll_Cobranza
        Me.Gr_Cobranza.DataBind()

    End Sub

    Function Existe_Prioridad() As Boolean
        Dim I As Integer
        Existe_Prioridad = False
        If Me.Txt_Prioridad.Text <> "0" Then
            For I = 0 To Me.Gr_Cobranza.Rows.Count - 1
                If (Gr_Cobranza.Rows(I).Cells(3).Text = Me.Txt_Prioridad.Text) Then
                    Existe_Prioridad = True
                    Exit For
                End If
            Next
        End If
    End Function

   
    'Protected Sub Detalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detalle.Click

    '    Dim Sesion As New ClsSession.ClsSession

    '    SW = 0
    '    'No Gestionar 

    '    If IsNothing(Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_not_ges) = False Then

    '        If Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_not_ges = "S" Then
    '            Me.Chk_Generar.Checked = False
    '        ElseIf Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_not_ges = "N" Then
    '            Me.Chk_Generar.Checked = True
    '        End If

    '    Else
    '        Me.Chk_Generar.Checked = False
    '    End If

    '    'Gestionar Si - No

    '    If IsNothing(Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_ges_son) = False Then

    '        If Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_ges_son = "S" Then
    '            Me.RB_GestionarSi.Checked = True
    '            Me.RB_GestionarNo.Checked = False
    '        ElseIf Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_ges_son = "N" Then
    '            Me.RB_GestionarNo.Checked = True
    '            Me.RB_GestionarSi.Checked = False
    '        End If

    '    Else
    '        Me.RB_GestionarNo.Checked = True
    '        Me.RB_GestionarSi.Checked = False


    '    End If

    '    'Por Vencimiento  - Por Gestión 

    '    If IsNothing(Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pla_vto_ges) = False Then

    '        If Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pla_vto_ges = "V" Then
    '            Me.RB_FechaGest.Checked = False
    '            Me.RB_FechaVen.Checked = True
    '        ElseIf Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pla_vto_ges = "G" Then
    '            Me.RB_FechaGest.Checked = True
    '            Me.RB_FechaVen.Checked = False
    '        ElseIf Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pla_vto_ges = "" Then
    '            Me.RB_FechaGest.Checked = False
    '            Me.RB_FechaVen.Checked = False
    '        End If
    '    Else
    '        Me.RB_FechaGest.Checked = False
    '        Me.RB_FechaVen.Checked = False
    '    End If

    '    Me.Txt_PlazoDias.Text = Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pzo
    '    Me.Txt_Descripcion.Text = Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_des
    '    Me.Txt_CodCobranza.Text = Me.Gr_Cobranza.Rows(pos.Value).Cells(1).Text
    '    Me.Txt_Cod_Acc_Nvo.Text = Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_nvo_acc
    '    Me.Txt_Accion.Text = Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_acc
    '    Me.Txt_Prioridad.Text = Coll_Cobranza.Item(CInt(pos.Value + 1)).cco_pri



    '    For i = 0 To Me.Gr_Cobranza.Rows.Count - 1

    '        If i = pos.Value Then

    '            If Me.Gr_Cobranza.Rows(i).CssClass = "clicktable" Then

    '                Me.Gr_Cobranza.Rows(i).CssClass = "formatable"

    '                Limpiar()
    '            Else
    '                Me.Gr_Cobranza.Rows(i).CssClass = "clicktable"

    '            End If

    '        Else
    '            Me.Gr_Cobranza.Rows(i).CssClass = "formatable"

    '        End If
    '    Next
    '    Dim txt As TextBox
    '    txt = Me.Gr_Cobranza.Rows(pos.Value).FindControl("foco")
    '    txt.Focus()
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        SW = 0
        For i = 0 To Gr_Cobranza.Rows.Count - 1
            If (Gr_Cobranza.Rows(i).Cells(1).Text = btn.ToolTip) Then
                If (i Mod 2) = 0 Then
                    Gr_Cobranza.Rows(i).CssClass = "selectable"
                Else
                    Gr_Cobranza.Rows(i).CssClass = "selectableAlt"
                End If
            Else
                If (i Mod 2) = 0 Then
                    Gr_Cobranza.Rows(i).CssClass = "formatUltcell"
                Else
                    Gr_Cobranza.Rows(i).CssClass = "formatUltcellAlt"
                End If
            End If

        Next

        For x = 1 To Coll_Cobranza.Count
            If Coll_Cobranza.Item(x).cco_num = btn.ToolTip Then
                pos.Value = x
            End If
        Next

        If IsNothing(Coll_Cobranza.Item(CInt(pos.Value)).cco_not_ges) = False Then

            If Coll_Cobranza.Item(CInt(pos.Value)).cco_not_ges = "S" Then
                Me.Chk_Generar.Checked = False
            ElseIf Coll_Cobranza.Item(CInt(pos.Value)).cco_not_ges = "N" Then
                Me.Chk_Generar.Checked = True
            End If

        Else
            Me.Chk_Generar.Checked = False
        End If

        'Gestionar Si - No

        If IsNothing(Coll_Cobranza.Item(CInt(pos.Value)).cco_ges_son) = False Then

            If Coll_Cobranza.Item(CInt(pos.Value)).cco_ges_son = "S" Then
                Me.RB_GestionarSi.Checked = True
                Me.RB_GestionarNo.Checked = False
            ElseIf Coll_Cobranza.Item(CInt(pos.Value)).cco_ges_son = "N" Then
                Me.RB_GestionarNo.Checked = True
                Me.RB_GestionarSi.Checked = False
            End If

        Else
            Me.RB_GestionarNo.Checked = True
            Me.RB_GestionarSi.Checked = False


        End If

        'Por Vencimiento  - Por Gestión 

        If IsNothing(Coll_Cobranza.Item(CInt(pos.Value)).cco_pla_vto_ges) = False Then

            If Coll_Cobranza.Item(CInt(pos.Value)).cco_pla_vto_ges = "V" Then
                Me.RB_FechaGest.Checked = False
                Me.RB_FechaVen.Checked = True
            ElseIf Coll_Cobranza.Item(CInt(pos.Value)).cco_pla_vto_ges = "G" Then
                Me.RB_FechaGest.Checked = True
                Me.RB_FechaVen.Checked = False
            ElseIf Coll_Cobranza.Item(CInt(pos.Value)).cco_pla_vto_ges = "" Then
                Me.RB_FechaGest.Checked = False
                Me.RB_FechaVen.Checked = False
            End If
        Else
            Me.RB_FechaGest.Checked = False
            Me.RB_FechaVen.Checked = False
        End If

        Me.Txt_PlazoDias.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_pzo
        Me.Txt_Descripcion.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_des
        Me.Txt_CodCobranza.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_num
        Me.Txt_Cod_Acc_Nvo.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_nvo_acc
        Me.Txt_Accion.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_acc
        Me.Txt_Prioridad.Text = Coll_Cobranza.Item(CInt(pos.Value)).cco_pri

        'Dim txt As TextBox
        'txt = Me.Gr_Cobranza.Rows(pos.Value).FindControl("foco")
        'txt.Focus()
    End Sub
End Class
