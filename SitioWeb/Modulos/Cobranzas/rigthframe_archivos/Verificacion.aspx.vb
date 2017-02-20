Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_Verificacion
    Inherits System.Web.UI.Page
   
#Region "Declaracion de Variables"

    Dim Caption As String = "Verificación de Documentos"
    Dim Sesion As New ClsSession.ClsSession
    Dim FC As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim AG As New ActualizacionesGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim Deudor As deu_cls
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim agt As New Perfiles.Cls_Principal
    Dim CBZ As New ClaseCobranza
    Dim ope As New ClaseOperaciones
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                Response.Expires = -1

                Dim querystringSeguro As TSHAK.Components.SecureQueryString
                'instanciamos el objeto y le pasamos como argumento el mismo array 'de bits mas el parámetro data, que viene de la llamada de la 
                'pagina default.aspx que contiene todo el queryString
                querystringSeguro = New TSHAK.Components.SecureQueryString(New Byte() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 8}, Request("data"))

                Txt_Rut_Cli.Text = querystringSeguro("Id_Cli")
                Txt_Dig_Cli.Text = FC.Vrut(CLng(Replace(Txt_Rut_Cli.Text, ",", "")))
                Txt_Raz_Soc_Cli.Text = querystringSeguro("Nom_Cli")

                Txt_Fec_Ini.Text = Format(Date.Now, Var.FMT_FECHA)
                Txt_Fec_Ter.Text = Format(Date.Now, Var.FMT_FECHA)

                NroPaginacion_Docto = 0

                AlinearDerecha()
                CargaDrop()
                Txt_Rut_Deu.Focus()

            Else

                If Txt_Rut_Deu.Text.Trim() <> "" Then

                    If Dp_Raz_Cnt.SelectedIndex > 0 Then
                        HF_CNC.Value = Dp_Raz_Cnt.SelectedValue
                    End If

                    CargaContactos()

                    Dp_Raz_Cnt_SelectedIndexChanged(Me, e)

                End If

            End If

            IB_AyudaDeu.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaDeudor.aspx','PopUpDeudor',580,410,200,150);")
            Sesion.TipoDeContacto = 2
            IB_Contactos.Attributes.Add("onClick", "WinOpen(2,'../../Contactos/Contactos.aspx?Rut=" & Txt_Rut_Deu.Text & "&RefrescarModal=1" & "&tipo=1" & "', 'Contactos', 620,650,100,100);")
            Txt_Mto_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub Dp_Tip_Deu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Tip_Deu.SelectedIndexChanged
        Try
            Select Case Dp_Tip_Deu.SelectedValue
                Case 1
                    Lbl_Nom_Deu.Text = "Nombre"
                    Lbl_Ape_Pat_Deu.Visible = True
                    Txt_Ape_Pat.Visible = True
                    Lbl_Ape_Mat_Deu.Visible = True
                    Txt_Ape_Mat.Visible = True

                Case 2
                    Lbl_Nom_Deu.Text = "Razón Soc."
                    Lbl_Ape_Pat_Deu.Visible = False
                    Txt_Ape_Pat.Visible = False
                    Lbl_Ape_Mat_Deu.Visible = False
                    Txt_Ape_Mat.Visible = False

            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub Dp_Tip_Mon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Tip_Mon.SelectedIndexChanged
        Try

            If Me.Txt_Mto_Doc.Text = "" Then
                Me.Txt_Mto_Doc.Text = 0
            End If

            Select Case Dp_Tip_Mon.SelectedValue

                Case 1
                    'Formato_Moneda = FMT.FCMSD 'Pesos
                    Txt_Mto_Doc.Text = Format(CDbl(Txt_Mto_Doc.Text), FMT.FCMSD)
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999"

                Case 2
                    'Formato_Moneda = FMT.FCMCD4 'UF
                    Txt_Mto_Doc.Text = Format(CDbl(Txt_Mto_Doc.Text), FMT.FCMCD4)
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999.9999"

                Case 3, 4
                    'Formato_Moneda = FMT.FCMCD 'Dollar y Euro
                    Txt_Mto_Doc.Text = Format(CDbl(Txt_Mto_Doc.Text), FMT.FCMCD)
                    Txt_Mto_Doc_MaskedEditExtender.Mask = "999,999,999,999.99"

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Dp_Tip_Doc_Bus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Tip_Doc_Bus.SelectedIndexChanged
        Try
            Select Case Dp_Tip_Doc_Bus.SelectedValue
                Case 0
                    RB_Todos.Checked = True
                Case Else
                    RB_Todos.Checked = False
            End Select
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub Dp_Raz_Cnt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Raz_Cnt.SelectedIndexChanged

        If HF_CNC.Value <> "" Then

            Dim C As cnc_cls
            Dim CLSCLI As New ClaseClientes

            C = CLSCLI.ContactosDevuelve(Variables.TipoDeContacto.Deudor, Txt_Rut_Deu.Text, HF_CNC.Value)

            If Not IsNothing(C) Then
                Txt_Fono_Cnt.Text = C.cnc_tel
                Txt_Fax_Cnt.Text = C.cnc_fax
                Txt_Mail_Cnt.Text = C.cnc_ema
                Dp_Raz_Cnt.ClearSelection()
                Dp_Raz_Cnt.Items.FindByValue(HF_CNC.Value).Selected = True
            End If

        End If


    End Sub

    Protected Sub GV_DoctosDvf_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_DoctosDvf.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            'e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")
            'e.Row.Attributes.Add("onClick", "CelClick_GV_DoctosDvf(GV_DoctosDvf, 'clicktable', 'formatable', 'selectable')")
        End If
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion_Docto = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If

            If NroPaginacion_Docto = 5 Then
                NroPaginacion_Docto -= 5
                DocumentosVerificacionLista()
                FormatoGrillaDoctosAVerificar()

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GV_DoctosDvf.Rows.Count < 5 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If

            If GV_DoctosDvf.Rows.Count = 5 Then
                NroPaginacion_Docto += 5
                DocumentosVerificacionLista()
                FormatoGrillaDoctosAVerificar()

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try
    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim img_ver As ImageButton = CType(sender, ImageButton)
        Dim x As Integer
        Try

            For I = 0 To GV_DoctosDvf.Rows.Count - 1
                If (img_ver.ToolTip = GV_DoctosDvf.Rows(I).Cells(3).Text) Then
                    Txt_PosGV.Value = I + 1

                    'hola 
                    If Txt_PosGV.Value = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un registro del listado", TipoDeMensaje._Exclamacion, Nothing, False)
                        Exit Sub
                    End If

                    For x = 1 To Sesion.coll_Dvf.Count


                        If x = CInt(Txt_PosGV.Value) Then


                            'No permite modificar
                            Txt_Nro_Doc.ReadOnly = True
                            Dp_Tip_Doc.Enabled = False

                            Txt_Nro_Doc.CssClass = "clsDisabled"
                            Dp_Tip_Doc.CssClass = "clsDisabled"

                            'Permitidos Modificar
                            Dp_Est_Veri.Enabled = True
                            Dp_Tip_Mon.Enabled = True

                            Txt_Obs_Docto.ReadOnly = False
                            Txt_Fec_Vcto.ReadOnly = False
                            Txt_Mto_Doc.ReadOnly = False

                            Txt_Fec_LLeg.ReadOnly = False
                            Txt_Hor_LLeg.ReadOnly = False
                            Txt_Fec_Pag.ReadOnly = False
                            Txt_Fec_Veri.ReadOnly = False
                            Txt_Hor_Veri.ReadOnly = False
                            Txt_Fec_Ges.ReadOnly = False
                            'Txt_Fec_Ini.ReadOnly = False
                            'Txt_Fec_Ter.ReadOnly = False


                            Dp_Est_Veri.CssClass = "clsMandatorio"
                            Txt_Obs_Docto.CssClass = "clsTxt"
                            Txt_Fec_Vcto.CssClass = "clsMandatorio"
                            Txt_Mto_Doc.CssClass = "clsMandatorio"
                            Dp_Tip_Mon.CssClass = "clsMandatorio"

                            Txt_Fec_LLeg.CssClass = "clsTxt"
                            Txt_Hor_LLeg.CssClass = "clsTxt"
                            Txt_Fec_Pag.CssClass = "clsTxt"
                            Txt_Fec_Veri.CssClass = "clsMandatorio"
                            Txt_Hor_Veri.CssClass = "clsTxt"
                            Txt_Fec_Ges.CssClass = "clsTxt"

                            Dp_Tip_Doc_Bus.CssClass = "clsMandatorio"
                            'Txt_Fec_Ini.CssClass = "clsMandatorio"
                            'Txt_Fec_Ter.CssClass = "clsMandatorio"

                            Txt_Rut_Deu.Text = CInt(Sesion.coll_Dvf.Item(x).deu_ide)

                            DeudorDevuelve(Txt_Rut_Deu.Text)
                            CargaContactos()

                            Dp_Tip_Mon.ClearSelection()
                            Dp_Tip_Doc.ClearSelection()
                            Dp_Est_Veri.ClearSelection()

                            Txt_Nro_Doc.Text = Sesion.coll_Dvf.Item(x).dvf_num
                            Txt_Obs_Docto.Text = Sesion.coll_Dvf.Item(x).dvf_obs
                            Txt_Fec_Vcto.Text = Sesion.coll_Dvf.Item(x).dvf_fev
                            Txt_Fec_Vto_Real.Text = Sesion.coll_Dvf.Item(x).dvf_fev_rea

                            Txt_Fec_Vcto_CalendarExtender.Enabled = True
                            Txt_Fec_Vcto_MaskedEditExtender.Enabled = True
                            Txt_Mto_Doc.Enabled = True
                            Txt_Fec_Veri_CalendarExtender.Enabled = True
                            Txt_Fec_Vcto_MaskedEditExtender.Enabled = True
                            Txt_Hor_Veri_MaskedEditExtender.Enabled = True
                            Txt_Fec_Ges_MaskedEditExtender.Enabled = True
                            Txt_Fec_Veri_CalendarExtender.Enabled = True


                            Txt_Fec_Ges_CalendarExtender.Enabled = True
                            Dp_Tip_Doc.SelectedValue = Sesion.coll_Dvf.Item(x).Id_P_0031
                            Dp_Est_Veri.SelectedValue = Sesion.coll_Dvf.Item(x).id_P_0040
                            Dp_Tip_Mon.SelectedValue = Sesion.coll_Dvf.Item(x).id_p_0023


                            Dim Formato_Moneda As String

                            Select Case Sesion.coll_Dvf.Item(x).id_p_0023
                                Case 1
                                    Formato_Moneda = FMT.FCMSD 'Pesos
                                Case 2
                                    Formato_Moneda = FMT.FCMCD4 'UF
                                Case 3, 4
                                    Formato_Moneda = FMT.FCMCD 'Dollar y Euro
                            End Select

                            Txt_Mto_Doc.Text = Format(CLng(Sesion.coll_Dvf.Item(x).dvf_mto), Formato_Moneda)

                            If Sesion.coll_Dvf.Item(x).dvf_fec_lle <> "01-01-1900" Then
                                Txt_Fec_LLeg.Text = Sesion.coll_Dvf.Item(x).dvf_fec_lle
                                Txt_Hor_LLeg.Text = Format(Sesion.coll_Dvf.Item(x).dvf_fec_lle, "hh:mm")
                            End If

                            If Sesion.coll_Dvf.Item(x).dvf_fec_pag <> "01-01-1900" Then
                                Txt_Fec_Pag.Text = Sesion.coll_Dvf.Item(x).dvf_fec_pag
                            End If

                            If Sesion.coll_Dvf.Item(x).dvf_fec_vfc <> "01-01-1900" Then
                                Txt_Fec_Veri.Text = Sesion.coll_Dvf.Item(x).dvf_fec_vfc
                                Txt_Hor_Veri.Text = Format(Sesion.coll_Dvf.Item(x).dvf_fec_vfc, "hh:mm")
                            End If

                            If Sesion.coll_Dvf.Item(x).dvf_fec_pri_gsn <> "01-01-1900" Then
                                Txt_Fec_Ges.Text = Sesion.coll_Dvf.Item(x).dvf_fec_pri_gsn
                            End If

                            IB_Guardar.Enabled = True
                            Txt_SW.Value = "MODIFICAR DOCUMENTO"
                            IB_Inf_Confir.Enabled = True
                            Exit For

                        End If

                    Next

                    If (I Mod 2) = 0 Then
                        GV_DoctosDvf.Rows(I).CssClass = "selectable"
                    Else
                        GV_DoctosDvf.Rows(I).CssClass = "selectableAlt"
                    End If

                Else

                    If (I Mod 2) = 0 Then
                        GV_DoctosDvf.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_DoctosDvf.Rows(I).CssClass = "formatUltcellAlt"
                    End If

                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Txt_Fec_Vcto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Vcto.TextChanged

        Try


            If Txt_Fec_Vcto.Text = "" Then
                Msj.Mensaje(Me, "Atencion", "Ingrese Fecha Vencimiento", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Vcto.Text) Then
                Msj.Mensaje(Me, "Atencion", "Fecha Erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Fec_Vcto.Text = ""
                Txt_Fec_Vto_Real.Text = ""
                Txt_Fec_Vcto.Focus()
                Exit Sub

            End If

            If CDate(Txt_Fec_Vcto.Text) > "31/12/2100" Then
                Msj.Mensaje(Me, "Atencion", "Fecha Erronea", ClsMensaje.TipoDeMensaje._Exclamacion)
                Txt_Fec_Vcto.Text = ""
                Txt_Fec_Vto_Real.Text = ""
                Txt_Fec_Vcto.Focus()
                Exit Sub
            End If

            If Txt_Fec_Vcto.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar fecha de vcto. de los documentos", TipoDeMensaje._Informacion)
                Exit Sub
            End If

            'Trae la fecha de vencimiento real
            CG.calcula_vcto_real(Txt_Rut_Deu.Text, _
                                 Me.Txt_Fec_Vcto.Text, _
                                 Sucursal, _
                                 "", _
                                 Dp_Tip_Doc.SelectedValue)

            Txt_Fec_Vto_Real.Text = Format(CDate(FECHA_VCTO_CALCULO), "dd/MM/yyyy")

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_Inf_Confir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Inf_Confir.Click
        AbrePopup(Me, 1, "InformeVerificacion.aspx?REGISTRO=1&RutCli=" & Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT), "InformeDocumentos_a_Verificar", 1200, 750, 50, 50)
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click

        Try

            If Txt_Rut_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                Exit Sub
            End If

            If Trim(Txt_Fec_Ini.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha de inicio", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Ini.Focus()
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Ini.Text) Then
                Msj.Mensaje(Page, Caption, "Fecha de inicio errónea", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Txt_Fec_Ini.Text = ""
                Exit Sub
            End If

            If Trim(Txt_Fec_Ter.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha de termino", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Ter.Focus()
                Exit Sub
            End If

            If Not IsDate(Txt_Fec_Ter.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha de termino errónea", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Ter.Focus()
                Exit Sub
            End If

            If CDate(Txt_Fec_Ini.Text) > CDate(Txt_Fec_Ter.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha de inicio debe ser menor a fecha de termino", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Ini.Focus()
                Exit Sub
            End If


            If Trim(Txt_Rut_Deu.Text) <> "" Then

                Deudor = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), "000000000000"))

                If IsNothing(Deudor) Then
                    Msj.Mensaje(Me.Page, Caption, "Pagador no existe, ¿Desea ingresarlo?", TipoDeMensaje._Confirmacion)
                Else
                    Dp_Raz_Cnt.Enabled = True
                    Dp_Raz_Cnt.CssClass = "clsMandatorio"
                    IB_Contactos.Enabled = True
                    IB_Dias_Pago.Enabled = True
                    IB_Obs_Deudor.Enabled = True
                    DeudorDevuelve(Format(CLng(Txt_Rut_Deu.Text), "000000000000"))

                    IB_Nuevo.Enabled = True
                End If

            End If

            DocumentosVerificacionLista()

            'FormatoGrillaDoctosAVerificar()

            If Not IsNothing(Sesion.coll_Dvf) Then
                IB_Eliminar.Enabled = True
                'IB_Modificar.Enabled = True

            Else
                IB_Eliminar.Enabled = False
                'IB_Modificar.Enabled = False
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

        If GV_DoctosDvf.Rows.Count = 0 Then
            Msj.Mensaje(Me.Page, Caption, "No se encontraron archivos", TipoDeMensaje._Exclamacion, Nothing, False)
            Txt_Fec_Ter.Focus()
            Exit Sub
        End If

    End Sub

    Protected Sub IB_Dias_Pago_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Dias_Pago.Click
        Try
            AbrePopup(Me, 2, "DiasDePago.aspx?RutCli=" & Txt_Rut_Cli.Text & "&RutDeu=" & Txt_Rut_Deu.Text & "", "Días de Pago", 470, 260, 300, 300)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Eliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Not agt.ValidaAccesso(20, 20040107, Usr, "PRESIONO ELIMINAR DOCUMENTO") Then
            Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If Txt_PosGV.Value = "" Then
            Msj.Mensaje(Me.Page, Caption, "Debe seleccionar un registro del listado", TipoDeMensaje._Exclamacion, Nothing, False)
            Exit Sub
        End If

        Msj.Mensaje(Me.Page, Caption, "¿Esta Seguro de Eliminar un Documento?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Eliminar.UniqueID, False)


    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Try

            Dim agt As New Perfiles.Cls_Principal


            Select Case Txt_SW.Value

                Case "GUARDAR DEUDOR" 'Guarda un nuevo duedor

                    If Trim(Txt_Rut_Deu.Text) = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                        Txt_Rut_Deu.Focus()
                        Exit Sub
                    End If

                    If Trim(Txt_Dig_Deu.Text) = "" Then
                        Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verficador", TipoDeMensaje._Exclamacion, Nothing, False)
                        Txt_Dig_Deu.Focus()
                        Exit Sub
                    End If

                    If Trim(Txt_Dig_Deu.Text) <> FC.Vrut(CLng(Txt_Rut_Deu.Text)) Then
                        Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion, Nothing, False)
                        Txt_Rut_Deu.Focus()
                        Exit Sub
                    End If

                    Select Case Dp_Tip_Deu.SelectedValue
                        Case 0
                            Msj.Mensaje(Me.Page, Caption, "Debe selecionar tipo Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                            Dp_Tip_Deu.Focus()
                            Exit Sub
                        Case 1
                            If Trim(Txt_Ape_Pat.Text) = "" Then
                                Msj.Mensaje(Me.Page, Caption, "Debe ingresar apellido paterno", TipoDeMensaje._Error, Nothing, False)
                                Txt_Ape_Pat.Focus()
                                Exit Sub
                            End If
                            If Trim(Txt_Ape_Mat.Text) = "" Then
                                Msj.Mensaje(Me.Page, Caption, "Debe ingresar apellido materno", TipoDeMensaje._Error, Nothing, False)
                                Txt_Ape_Mat.Focus()
                                Exit Sub
                            End If
                            If Trim(Txt_Rso_Deu.Text) = "" Then
                                Msj.Mensaje(Me.Page, Caption, "Debe ingresar nombre Pagador", TipoDeMensaje._Error, Nothing, False)
                                Txt_Rso_Deu.Focus()
                                Exit Sub
                            End If
                        Case 2
                            If Trim(Txt_Rso_Deu.Text) = "" Then
                                Msj.Mensaje(Me.Page, Caption, "Debe ingresar razón social Pagador", TipoDeMensaje._Error, Nothing, False)
                                Txt_Rso_Deu.Focus()
                                Exit Sub
                            End If
                    End Select


                    Msj.Mensaje(Me.Page, Caption, "¿Esta Seguro de Ingresar un Pagador?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)



                Case "GUARDAR DOCUMENTO" 'Ingresa un documento en verificación

                    If Not agt.ValidaAccesso(20, 20020107, Usr, "PRESIONO GUARDAR DOCUMENTO INGRESO") Then
                        Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                    If Not ValidaBlancosVerificacion() Then
                        Exit Sub
                    End If

                    Msj.Mensaje(Me.Page, Caption, "¿Esta Seguro de Ingresar Documento?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)

                Case "MODIFICAR DOCUMENTO"

                    If Not agt.ValidaAccesso(20, 20060107, Usr, "PRESIONO GUARDAR DOCUMENTO MODIFICACION") Then
                        Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub
                    End If


                    If Not ValidaBlancosVerificacion() Then
                        Exit Sub
                    End If


                    Msj.Mensaje(Me.Page, Caption, "¿Esta Seguro de Modificar Documento?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)

            End Select

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Imprimir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            If Txt_Rut_Deu.Text = "" Or Txt_Dig_Deu.Text = "" Or Txt_Rso_Deu.Text = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe Ingresar un Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Ini.Focus()
                Exit Sub
            End If

            If GV_DoctosDvf.Rows.Count = 0 Then
                Msj.Mensaje(Page, Caption, "No existen registros para  imprimir", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            AbrePopup(Me, 1, "InformeVerificacion.aspx?RutCli=" & Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT) & _
                      "&RutDeudsd=" & Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT) & _
                      "&RutDeuhst=" & Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT) & _
                      "&FecIni=" & Format(CDate(Txt_Fec_Ini.Text), "yyyyMMdd") & _
                      "&FecFin=" & Format(CDate(Txt_Fec_Ter.Text), "yyyyMMdd") & _
                      "&TipDoc=" & Dp_Tip_Doc.SelectedValue & "", "Informe Documentos a Verificar", 1200, 750, 50, 50)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try

            HabilitaDeudor(False)

            Limpiar()

            IB_Nuevo.Enabled = False
            'IB_Modificar.Enabled = False
            IB_Eliminar.Enabled = False
            IB_Guardar.Enabled = False
            IB_Imprimir.Enabled = False
            IB_Contactos.Enabled = False
            IB_Dias_Pago.Enabled = False
            IB_Obs_Deudor.Enabled = False
            IB_AyudaDeu.Enabled = True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            If Not agt.ValidaAccesso(20, 20030107, Usr, "PRESIONO NUEVO DOCUMENTO") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Nuevo()
            IB_Guardar.Enabled = True
            Txt_SW.Value = "GUARDAR DOCUMENTO"


            Txt_Fec_Veri.Text = Format(Date.Now, Var.FMT_FECHA)
            Txt_Fec_Ini.Text = Format(Date.Now, Var.FMT_FECHA)
            Txt_Fec_Ter.Text = Format(Date.Now, Var.FMT_FECHA)

            Txt_Nro_Doc.Focus()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Obs_Deudor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Obs_Deudor.Click
        Try
            AbrePopup(Me, 2, "ObservacionesDeudor.aspx?RutCli=" & Txt_Rut_Cli.Text & "&RutDeu=" & Txt_Rut_Deu.Text & "", "Observación Pagador", 350, 350, 300, 300)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            ClosePag(Me)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub LB_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Eliminar.Click
        Try

            For I = 1 To Sesion.coll_Dvf.Count

                If I = CInt(Txt_PosGV.Value) Then

                    If CBZ.DocumentosDvfDelete(Sesion.coll_Dvf.Item(I).dvf_num) Then

                        Txt_SW.Value = ""
                        Msj.Mensaje(Me.Page, Caption, "Documento eliminado", TipoDeMensaje._Informacion, Nothing, False)
                        DocumentosVerificacionLista()
                        Exit For

                    Else
                        Msj.Mensaje(Me.Page, Caption, "Documento no eliminado", TipoDeMensaje._Informacion, Nothing, False)
                    End If

                End If

            Next

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            Select Case Txt_SW.Value

                Case "GUARDAR DEUDOR" 'Guarda un nuevo duedor

                    Deudor = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT))

                    If IsNothing(Deudor) Then

                        Deudor = CargaDeudor()

                        If Not IsNothing(Deudor) Then

                            If AG.DeudorInserta(Deudor, 0, "") Then
                                Msj.Mensaje(Me.Page, Caption, "Pagador ingresado", TipoDeMensaje._Informacion, Nothing, False)
                                HabilitaDeudor(False)
                            Else
                                Msj.Mensaje(Me.Page, Caption, "Pagador no ingresado", TipoDeMensaje._Informacion, Nothing, False)
                            End If

                        End If

                    End If

                    Txt_SW.Value = ""

                Case "GUARDAR DOCUMENTO" 'Ingresa un documento en verificación

                    Dim DoctoDvf As New dvf_cls

                    DoctoDvf = CargaDoctoDvf()

                    If Not IsNothing(DoctoDvf) Then

                        If CBZ.DocumentoDvfInserta(DoctoDvf) _
                                And CBZ.DsiUpdate(Format(CLng(Trim(Txt_Rut_Cli.Text)), Var.FMT_RUT), _
                                                  Format(CLng(Trim(Txt_Rut_Deu.Text)), Var.FMT_RUT), _
                                                  Txt_Nro_Doc.Text, _
                                                  Dp_Est_Veri.SelectedValue, _
                                                  Dp_Tip_Doc.SelectedValue) Then

                            ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), Var.FMT_RUT), _
                                                        Txt_Nro_Doc.Text, _
                                                        Txt_Mto_Doc.Text, _
                                                        Dp_Tip_Doc.SelectedValue, _
                                                        2)

                            Msj.Mensaje(Me.Page, Caption, "Documento ingresado", TipoDeMensaje._Informacion, Nothing, False)

                            Txt_Nro_Doc.Text = ""
                            Dp_Tip_Doc.ClearSelection()
                            Dp_Est_Veri.ClearSelection()
                            Txt_Obs_Docto.Text = ""
                            Txt_Fec_Vcto.Text = ""
                            Txt_Mto_Doc.Text = ""
                            Dp_Tip_Mon.ClearSelection()
                            Txt_Fec_LLeg.Text = ""
                            Txt_Hor_LLeg.Text = ""
                            Txt_Fec_Pag.Text = ""
                            Txt_Fec_Veri.Text = ""
                            Txt_Hor_Veri.Text = ""
                            Txt_Fec_Ges.Text = ""
                            Dp_Tip_Doc_Bus.ClearSelection()

                            Txt_Nro_Doc.ReadOnly = True
                            Dp_Tip_Doc.Enabled = False
                            Dp_Est_Veri.Enabled = False
                            Txt_Obs_Docto.ReadOnly = True
                            Txt_Fec_Vcto.ReadOnly = True
                            Txt_Mto_Doc.ReadOnly = True
                            Dp_Tip_Mon.Enabled = False
                            Txt_Fec_LLeg.ReadOnly = True
                            Txt_Hor_LLeg.ReadOnly = True
                            Txt_Fec_Pag.ReadOnly = True
                            Txt_Fec_Veri.ReadOnly = True
                            Txt_Hor_Veri.ReadOnly = True
                            Txt_Fec_Ges.ReadOnly = True
                            'Dp_Tip_Doc_Bus.Enabled = False
                            'Txt_Fec_Ini.ReadOnly = True
                            'Txt_Fec_Ter.ReadOnly = True

                            Txt_Nro_Doc.CssClass = "clsDisabled"
                            Dp_Tip_Doc.CssClass = "clsDisabled"
                            Dp_Est_Veri.CssClass = "clsDisabled"
                            Txt_Obs_Docto.CssClass = "clsDisabled"
                            Txt_Fec_Vcto.CssClass = "clsDisabled"
                            Txt_Mto_Doc.CssClass = "clsDisabled"
                            Dp_Tip_Mon.CssClass = "clsDisabled"
                            Txt_Fec_LLeg.CssClass = "clsDisabled"
                            Txt_Hor_LLeg.CssClass = "clsDisabled"
                            Txt_Fec_Pag.CssClass = "clsDisabled"
                            Txt_Fec_Veri.CssClass = "clsDisabled"
                            Txt_Hor_Veri.CssClass = "clsDisabled"
                            Txt_Fec_Ges.CssClass = "clsDisabled"
                            HF_CNC.Value = ""
                            Txt_Fono_Cnt.Text = ""
                            Txt_Fax_Cnt.Text = ""
                            Txt_Mail_Cnt.Text = ""
                            'Dp_Tip_Doc_Bus.CssClass = "clsDisabled"
                            'Txt_Fec_Ini.CssClass = "clsDisabled"
                            'Txt_Fec_Ter.CssClass = "clsDisabled"

                            DocumentosVerificacionLista()

                            Txt_SW.Value = ""

                        Else
                            Msj.Mensaje(Me.Page, Caption, "Documento no ingresado", TipoDeMensaje._Exclamacion, Nothing, False)
                        End If

                    End If


                Case "MODIFICAR DOCUMENTO"


                    Dim DoctoDvf As New dvf_cls

                    DoctoDvf = CargaDoctoDvf()

                    If Not IsNothing(DoctoDvf) Then
                        Dim id_dvf As Integer = CG.DevuelveID_DVF(CInt(Txt_Nro_Doc.Text))
                        If CBZ.DocumentoDvfUpdate(DoctoDvf, id_dvf) Then

                            ope.sincroniza_verificacion(Format(CLng(Trim(Txt_Rut_Deu.Text)), Var.FMT_RUT), _
                                                        Txt_Nro_Doc.Text, _
                                                        Txt_Mto_Doc.Text, _
                                                        Dp_Tip_Doc.SelectedValue, _
                                                        2)

                            Msj.Mensaje(Me.Page, Caption, "Documento modificado", TipoDeMensaje._Exclamacion, Nothing, False)
                            DocumentosVerificacionLista()
                            Txt_SW.Value = ""
                            HF_CNC.Value = ""

                        End If
                    End If

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click

        Try

            CargaContactos()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RB_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Todos.CheckedChanged
        Try
            If RB_Todos.Checked Then
                Dp_Tip_Doc_Bus.ClearSelection()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LB_Buscar_Deu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Buscar_Deu.Click

        Try

            If Trim(Txt_Rut_Deu.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT del Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            If Trim(Txt_Dig_Deu.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito verficador", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Dig_Deu.Focus()
                Exit Sub
            End If

            If Trim(Txt_Dig_Deu.Text).ToUpper <> FC.Vrut(CLng(Txt_Rut_Deu.Text)).ToUpper Then
                Msj.Mensaje(Me.Page, Caption, "Rut incorrecto", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Rut_Deu.Focus()
                Exit Sub
            End If

            'Deudor = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu.Text), "000000000000"))

            If IsNothing(DeudorDevuelve(Format(CLng(Txt_Rut_Deu.Text), "000000000000"))) Then
                Msj.Mensaje(Me.Page, Caption, "Pagador no existe, ¿Desea ingresarlo?", TipoDeMensaje._Confirmacion)
            Else
                IB_Contactos.Enabled = True
                IB_Dias_Pago.Enabled = True
                IB_Obs_Deudor.Enabled = True



                Txt_Rut_Deu.ReadOnly = True
                Txt_Dig_Deu.ReadOnly = True

                Txt_Rut_Deu.CssClass = "clsDisabled"
                Txt_Dig_Deu.CssClass = "clsDisabled"

            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Protected Sub LB_Valida_Fechas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Valida_Fechas.Click
        Try
            If Trim(Txt_Hor_LLeg.Text) <> "" Then
                If Not IsDate(Trim(Txt_Hor_LLeg.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Hora de llegada incorrecta", TipoDeMensaje._Exclamacion, Nothing, False)
                    Txt_Hor_LLeg.Focus()
                    Exit Sub
                End If
            End If

            If Trim(Txt_Hor_Veri.Text) <> "" Then
                If Not IsDate(Trim(Txt_Hor_Veri.Text)) Then
                    Msj.Mensaje(Me.Page, Caption, "Hora de verificación incorrecta", TipoDeMensaje._Exclamacion, Nothing, False)
                    Txt_Hor_Veri.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Cal_Pago_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Cal_Pago.Click

        Try

            RutDeu = CLng(Txt_Rut_Deu.Text).ToString()

            AbrePopup(Me, 2, "../../Adm.Deudores/rightframe_archivos/CalendarioDePago.aspx", "Calendario de Pago", 650, 600, 400, 200)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

#End Region

#Region "TextChanged Fechas"

    Protected Sub Txt_Fec_Pag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Pag.TextChanged
        If Not IsDate(Txt_Fec_Pag.Text) Then
            Msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
            Me.Txt_Fec_Pag.Text = ""
        End If
        Txt_Fec_Pag.Focus()
    End Sub

    Protected Sub Txt_Fec_Ges_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Fec_Ges.TextChanged
        If Not IsDate(Txt_Fec_Ges.Text) Then
            Msj.Mensaje(Me, "Atención", "Fecha Incorrecta", ClsMensaje.TipoDeMensaje._Exclamacion)
            Me.Txt_Fec_Ges.Text = ""
        End If
        Txt_Fec_Ges.Focus()
    End Sub

    Protected Sub Txt_Dig_Deu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Deu.TextChanged
        Try
            If Txt_Rut_Deu.Text <> "" Then
                If Txt_Rut_Deu.Text = Txt_Rut_Cli.Text Then
                    Me.Txt_Rut_Deu.Text = ""
                    Me.Txt_Dig_Deu.Text = ""
                    Msj.Mensaje(Me, "Atención", "No puede ingresar como Pagador al cliente ", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                    Exit Sub
                End If
                If DeudorDevuelve(Txt_Rut_Deu.Text) = True Then

                    Txt_Rut_Deu.Text = Format(CDbl(Txt_Rut_Deu.Text), FMT.FSMSD)
                    Txt_Rut_Deu.ReadOnly = True
                    Txt_Dig_Deu.ReadOnly = True

                    Txt_Rut_Deu.CssClass = "clsDisabled"
                    Txt_Dig_Deu.CssClass = "clsDisabled"
                    IB_AyudaDeu.Enabled = False



                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Procedimientos y Funciones Generales"

    Private Sub AlinearDerecha()

        Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Rut_Deu.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Nro_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Fax_Cnt.Attributes.Add("Style", "TEXT-ALING: rigth")
        Txt_Fono_Cnt.Attributes.Add("Style", "TEXT-ALING: rigth")
    End Sub

    Private Sub CargaDrop()

        Try
            CG.ParametrosDevuelve(TablaParametro.TipoCliente, True, Dp_Tip_Deu)
            CG.ParametrosDevuelve(TablaParametro.RazonesSociales, True, Dp_Abr_Raz_Soc)
            CG.ParametrosDevuelve(TablaParametro.ZonaRecaudacion, True, Dp_Zon_Rie)
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_Tip_Doc)
            CG.ParametrosDevuelve(TablaParametro.EstadoVerificacion, True, Dp_Est_Veri)
            CG.ParametrosDevuelve(TablaParametro.Moneda, True, Dp_Tip_Mon)
            CG.ParametrosDevuelve(TablaParametro.TipoDocumento, True, Dp_Tip_Doc_Bus)
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Private Function DeudorDevuelve(ByVal RutDeu As String) As Boolean
        Try

            Deudor = CBZ.VerificaDeudorDevuelve(Trim(RutDeu))

            If IsNothing(Deudor) Then
                Return False
            End If

            With Deudor
                If CInt(Dp_Tip_Deu.SelectedItem.Value) = 1 Then
                    'NATURAL
                    Txt_Rso_Deu.Text = .deu_rso.Trim.ToUpper
                    Txt_Ape_Pat.Text = .deu_ape_ptn.Trim.ToUpper
                    Txt_Ape_Mat.Text = .deu_ape_mtn.ToUpper
                Else
                    'JURIDICO
                    Txt_Rso_Deu.Text = .deu_rso.Trim.ToUpper
                End If

                If Not IsNothing(.id_P_0063) Then
                    Dp_Abr_Raz_Soc.ClearSelection()
                    Dp_Abr_Raz_Soc.SelectedValue = .id_P_0063
                End If
                Dp_Tip_Deu.SelectedValue = .id_P_0044
                If Not IsNothing(.deu_con_cbz) Then
                    If .deu_con_cbz = "S" Then
                        ImgRojo.Visible = True
                        ImgVerde.Visible = False
                    Else
                        ImgRojo.Visible = False
                        ImgVerde.Visible = True
                    End If
                Else
                    ImgRojo.Visible = False
                    ImgVerde.Visible = True
                End If

                'Dp_Zon_Rie.ClearSelection()
                'Dp_Zon_Rie.SelectedValue = .dsi_cls

                CargaContactos()

                Return True

            End With

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
            Return False

        End Try

    End Function

    Private Sub CargaContactos()

        Try

            Dim Cnc As New Object
            Dim CLSCLI As New ClaseClientes

            Cnc = CLSCLI.ContactosDevuelveTodos(Variables.TipoDeContacto.Deudor, Txt_Rut_Deu.Text, False)

            RW.Llenar_Drop(Cnc, "id_cnc", "cnc_nom", Dp_Raz_Cnt)

            For Each C In Cnc
                If C.cnc_def = "S" Then
                    Dp_Raz_Cnt.SelectedValue = C.id_cnc
                    Txt_Fono_Cnt.Text = C.cnc_tel
                    Txt_Fax_Cnt.Text = C.cnc_fax
                    Txt_Mail_Cnt.Text = C.cnc_ema
                End If
            Next

            Dp_Raz_Cnt.Enabled = True
            Dp_Raz_Cnt.CssClass = "clsMandatorio"
            IB_Nuevo.Enabled = True
            IB_Contactos.Enabled = True
            IB_Dias_Pago.Enabled = True
            IB_Obs_Deudor.Enabled = True
            IB_Cal_Pago.Enabled = True


        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Private Function CargaDeudor() As deu_cls
        Try
            Dim Deudor As New deu_cls

            With Deudor

                .deu_ide = Format(CLng(Replace(Txt_Rut_Deu.Text, ",", "")), "000000000000")
                .id_P_0044 = Dp_Tip_Deu.SelectedValue

                'NATURAL
                .deu_nom = Txt_Rso_Deu.Text.ToUpper
                .deu_ape_ptn = Txt_Ape_Pat.Text.ToUpper
                .deu_ape_mtn = Txt_Ape_Mat.Text.ToUpper

                'JURIDICO
                .deu_rso = Txt_Rso_Deu.Text.ToUpper

                .id_P_0063 = Dp_Abr_Raz_Soc.SelectedValue
                .deu_con_cbz = "N"
            End With

            Return Deudor

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
            Return Nothing
        End Try
    End Function

    Private Function CargaDoctoDvf() As dvf_cls

        Try

            Dim Dvf As New dvf_cls
            Dim Verificacion As String
            Dim Gestion As String
            Dim LLegada As String
            Dim Pago As String

            With Dvf
                .cli_idc = Format(CLng(Txt_Rut_Cli.Text), Var.FMT_RUT)
                .deu_ide = Format(CLng(Txt_Rut_Deu.Text), Var.FMT_RUT)
                .id_P_0031 = Dp_Tip_Doc.SelectedValue
                .id_P_0023 = Dp_Tip_Mon.SelectedValue()
                .dvf_num = CInt(Txt_Nro_Doc.Text)
                .id_P_0040 = Dp_Est_Veri.SelectedValue
                .dvf_fev = CDate(Txt_Fec_Vcto.Text)
                .dvf_fev_rea = CDate(Txt_Fec_Vto_Real.Text)
                .dvf_obs = Txt_Obs_Docto.Text.ToUpper
                .dvf_mto = CDbl(Txt_Mto_Doc.Text)

                If Txt_Fec_LLeg.Text = "" Then
                    LLegada = "01-01-1900 00:00:00"
                Else
                    If Txt_Hor_LLeg.Text = "" Then
                        LLegada = CDate(Txt_Fec_LLeg.Text) & " 00:00:00"
                    Else
                        LLegada = CDate(Txt_Fec_LLeg.Text) & " " & Txt_Hor_LLeg.Text
                    End If
                End If

                .dvf_fec_lle = LLegada

                If Txt_Fec_Veri.Text = "" Then
                    Verificacion = "01-01-1900 00:00:00"
                Else
                    If Txt_Hor_Veri.Text = "" Then
                        Verificacion = CDate(Txt_Fec_Veri.Text) & " 00:00:00"
                    Else
                        Verificacion = CDate(Txt_Fec_Veri.Text) & " " & Txt_Hor_Veri.Text
                    End If

                End If

                .dvf_fec_vfc = Verificacion

                If Txt_Fec_Pag.Text = "" Then
                    Pago = "01-01-1900 00:00:00"
                Else
                    Pago = CDate(Txt_Fec_Pag.Text) & " " & Date.Now.ToShortTimeString
                End If

                .dvf_fec_pag = Pago

                If Txt_Fec_Ges.Text = "" Then
                    Gestion = "01-01-1900 00:00:00"
                Else
                    Gestion = CDate(Txt_Fec_Ges.Text) & " " & Date.Now.ToShortTimeString
                End If

                .dvf_fec_pri_gsn = Gestion

                .dvf_pro = "V"
                .dvf_fec_ing = Date.Now
                .dvf_obs_001 = Txt_Obs_Docto.Text.ToUpper
                .dvf_obs_cob = ""

                If Dp_Zon_Rie.SelectedValue > 0 Then
                    .dvf_zon_rgo_rec = Dp_Zon_Rie.SelectedValue
                End If

                .id_cnc = Dp_Raz_Cnt.SelectedValue
                .id_eje_dvf = Sesion.CodEje
            End With

            Return Dvf

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub Nuevo()
        Txt_Nro_Doc.Text = ""
        Dp_Tip_Doc.ClearSelection()
        Dp_Est_Veri.ClearSelection()
        Txt_Obs_Docto.Text = ""
        Txt_Fec_Vcto.Text = ""
        Txt_Mto_Doc.Text = ""
        Dp_Tip_Mon.ClearSelection()
        Txt_Fec_LLeg.Text = ""
        Txt_Hor_LLeg.Text = ""
        Txt_Fec_Pag.Text = ""
        Txt_Fec_Veri.Text = ""
        Txt_Hor_Veri.Text = ""
        Txt_Fec_Ges.Text = ""
        Dp_Tip_Doc_Bus.ClearSelection()

        'Txt_Fec_Ini.Text = ""
        'Txt_Fec_Ter.Text = ""

        Txt_Nro_Doc.ReadOnly = False
        Dp_Tip_Doc.Enabled = True
        Dp_Est_Veri.Enabled = True
        Txt_Obs_Docto.ReadOnly = False
        Txt_Fec_Vcto.ReadOnly = False
        Txt_Mto_Doc.ReadOnly = False
        Dp_Tip_Mon.Enabled = True
        Txt_Fec_LLeg.ReadOnly = False
        Txt_Hor_LLeg.ReadOnly = False
        Txt_Fec_Pag.ReadOnly = False
        Txt_Fec_Veri.ReadOnly = False
        Txt_Hor_Veri.ReadOnly = False
        Txt_Fec_Ges.ReadOnly = False
        Dp_Tip_Doc_Bus.Enabled = True
        'Txt_Fec_Ini.ReadOnly = False
        'Txt_Fec_Ter.ReadOnly = False

        Txt_Nro_Doc.CssClass = "clsMandatorio"
        Dp_Tip_Doc.CssClass = "clsMandatorio"
        Dp_Est_Veri.CssClass = "clsMandatorio"
        Txt_Obs_Docto.CssClass = "clsTxt"
        Txt_Fec_Vcto.CssClass = "clsMandatorio"
        Txt_Mto_Doc.CssClass = "clsMandatorio"
        Dp_Tip_Mon.CssClass = "clsMandatorio"

        Txt_Fec_LLeg.CssClass = "clsTxt"
        Txt_Hor_LLeg.CssClass = "clsTxt"
        Txt_Fec_Pag.CssClass = "clsTxt"
        Txt_Fec_Veri.CssClass = "clsMandatorio"
        Txt_Hor_Veri.CssClass = "clsTxt"
        Txt_Fec_Ges.CssClass = "clsTxt"

        Dp_Tip_Doc_Bus.CssClass = "clsMandatorio"
        Txt_Fec_Ini.CssClass = "clsMandatorio"
        Txt_Fec_Ter.CssClass = "clsMandatorio"

    End Sub

    Private Sub DocumentosVerificacionLista()

        Try

            Dim TipoDoc1 As Integer
            Dim TipoDoc2 As Integer
            Dim Deu_Desde As Long
            Dim Deu_Hasta As Long
            Dim Fecha_Inicio As Date
            Dim Fecha_Termino As Date

            If Dp_Tip_Doc_Bus.SelectedValue = 0 Then
                TipoDoc1 = "0"
                TipoDoc2 = 999
            Else
                TipoDoc1 = Dp_Tip_Doc_Bus.SelectedValue
                TipoDoc2 = Dp_Tip_Doc_Bus.SelectedValue
            End If

            If Txt_Rut_Deu.Text = "" Then
                Deu_Desde = 0
                Deu_Hasta = 9999999999999
            Else
                Deu_Desde = Txt_Rut_Deu.Text
                Deu_Hasta = Txt_Rut_Deu.Text
            End If

            Fecha_Inicio = Format(CDate(Txt_Fec_Ini.Text), Var.FMT_FECHA) & " 00:00:00"
            Fecha_Termino = Format(CDate(Txt_Fec_Ter.Text), Var.FMT_FECHA) & " 23:59:59"

            Sesion.coll_Dvf = CBZ.DocumentosDvfDevuelve(Txt_Rut_Cli.Text, Deu_Desde, Deu_Hasta, _
                                                       Fecha_Inicio, Fecha_Termino, TipoDoc1, TipoDoc2, _
                                                       True, GV_DoctosDvf)
            If GV_DoctosDvf.Rows.Count > 0 Then
                IB_Imprimir.Enabled = True
                FormatoGrillaDoctosAVerificar()
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Private Sub Limpiar()

        Txt_Rut_Deu.Text = ""
        Txt_Dig_Deu.Text = ""
        Txt_Rso_Deu.Text = ""
        Txt_Ape_Pat.Text = ""
        Txt_Ape_Mat.Text = ""

        Txt_Fono_Cnt.Text = ""
        Txt_Fax_Cnt.Text = ""
        Txt_Mail_Cnt.Text = ""

        Txt_Nro_Doc.Text = ""
        Txt_Obs_Docto.Text = ""
        Txt_Fec_Vcto.Text = ""
        Txt_Mto_Doc.Text = ""

        Txt_Fec_LLeg.Text = ""
        Txt_Hor_LLeg.Text = ""
        Txt_Fec_Pag.Text = ""
        Txt_Fec_Veri.Text = ""
        Txt_Hor_Veri.Text = ""
        Txt_Fec_Ges.Text = ""

        Dp_Tip_Deu.ClearSelection()
        Dp_Abr_Raz_Soc.ClearSelection()
        Dp_Zon_Rie.ClearSelection()
        Dp_Raz_Cnt.ClearSelection()
        Dp_Tip_Doc.ClearSelection()
        Dp_Est_Veri.ClearSelection()
        Dp_Tip_Mon.ClearSelection()

        GV_DoctosDvf.DataSource = Nothing
        GV_DoctosDvf.DataBind()

        Dp_Raz_Cnt.DataSource = Nothing
        Dp_Raz_Cnt.DataBind()
        Dp_Est_Veri.ClearSelection()
        Txt_Obs_Docto.ReadOnly = True
        Txt_Fec_Vcto.ReadOnly = True
        Txt_Mto_Doc.ReadOnly = True
        Dp_Tip_Mon.Enabled = False

        Txt_Fec_Vcto.CssClass = "clsDisabled"
        Txt_Mto_Doc.CssClass = "clsDisabled"
        Dp_Tip_Mon.CssClass = "clsDisabled"
        Txt_Rut_Deu.Focus()
        Dp_Est_Veri.ClearSelection()
        Dp_Est_Veri.Enabled = False
        Dp_Est_Veri.CssClass = "clsDisabled"
        Txt_Fec_Veri.ReadOnly = True
        Txt_Fec_Veri.CssClass = "clsDisabled"
        Txt_Fec_Veri.Text = ""

        Txt_Hor_Veri.Text = ""
        Txt_Hor_Veri.CssClass = "clsDisabled"
        Txt_Hor_Veri.ReadOnly = True
        Txt_Fec_Ges.ReadOnly = True
        Txt_Fec_Ges.CssClass = "clsDisabled"

        Txt_Rut_Deu.ReadOnly = False
        Txt_Dig_Deu.ReadOnly = False

        Txt_Rut_Deu.CssClass = "clsMandatorio"
        Txt_Dig_Deu.CssClass = "clsMandatorio"

        'Txt_Fec_Veri_CalendarExtender.Enabled = False
        'Txt_Fec_Vcto_MaskedEditExtender.Enabled = False
        'Txt_Fec_Ges_MaskedEditExtender.Enabled = False
        'Txt_Fec_Ges_CalendarExtender.Enabled = False
        'Txt_Fec_Vcto_CalendarExtender.Enabled = False
        'Txt_Hor_Veri_MaskedEditExtender.Enabled = False
        'Txt_Mto_Doc_MaskedEditExtender.Enabled = False

    End Sub

    Private Function ValidaBlancosVerificacion() As Boolean
        Try

            'Validamos si el documento existe en una operacion distinta a ingresado
            Dim clsope As New ClaseOperaciones

            If Txt_Nro_Doc.Text = "" Then
                Msj.Mensaje(Me, Caption, "Debe Ingresar el N° del documento", TipoDeMensaje._Informacion)
                Txt_Nro_Doc.Focus()
                Return False
            End If

            If Not clsope.Documentos_verificar_valida_DSI(CInt(Txt_Nro_Doc.Text), _
                                                          Me.Txt_Rut_Deu.Text, _
                                                          CInt(Dp_Tip_Doc.SelectedValue)) Then
                Msj.Mensaje(Me, Caption, "Documento existe en una operacion con estado " & clsope.EstadoOperacion, TipoDeMensaje._Informacion)
                Txt_Nro_Doc.Focus()
                Return False
            End If

            'Debe tener seleccionado un contacto
            If Dp_Raz_Cnt.SelectedIndex = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccion un contacto del Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                Dp_Abr_Raz_Soc.Focus()
                Return False
            End If

            If Dp_Raz_Cnt.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccion un contacto del Pagador", TipoDeMensaje._Exclamacion, Nothing, False)
                Dp_Abr_Raz_Soc.Focus()
                Return False
            End If

            If Trim(Txt_Nro_Doc.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar N° de documento", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Nro_Doc.Focus()
                Return False
            End If

            If Dp_Tip_Doc.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar tipo documento", TipoDeMensaje._Exclamacion, Nothing, False)
                Dp_Tip_Doc.Focus()
                Return False
            End If

            If Dp_Est_Veri.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar estado de verificación", TipoDeMensaje._Exclamacion, Nothing, False)
                Dp_Est_Veri.Focus()
                Return False
            End If

            If Trim(Txt_Fec_Vcto.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha de vencimiento", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Vcto.Focus()
                Return False
            End If

            If Trim(Txt_Mto_Doc.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar monto del documento", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Mto_Doc.Focus()
                Return False
            End If

            If Dp_Tip_Mon.SelectedValue = 0 Then
                Msj.Mensaje(Me.Page, Caption, "Debe seleccionar tipo moneda", TipoDeMensaje._Exclamacion, Nothing, False)
                Dp_Tip_Mon.Focus()
                Return False
            End If



            If Trim(Txt_Fec_Veri.Text) = "" Then
                Msj.Mensaje(Me.Page, Caption, "Debe ingresar fecha de verificación", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Veri.Focus()
                Return False
            End If

            If Not IsDate(Txt_Fec_Veri.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha de verificación errónea", TipoDeMensaje._Exclamacion, Nothing, False)
                Txt_Fec_Veri.Text = ""
                Return False
            End If

            'If Trim(Txt_Fec_LLeg.Text) = "" Then
            '    Msj.Mensaje(me.page, Caption, "Debe ingresar fecha de llegada", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Fec_LLeg.Focus()
            '    Return False
            'End If

            'If Trim(Txt_Hor_LLeg.Text) = "" Then
            '    Msj.Mensaje(me.page, Caption, "Debe ingresar hora de llegada", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Hor_LLeg.Focus()
            '    Return False
            'End If

            'If Not IsDate(Trim(Txt_Hor_LLeg.Text)) Then
            '    Msj.Mensaje(me.page, Caption, "Hora de llegada incorrecta", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Hor_LLeg.Focus()
            '    Return False
            'End If

            'If Trim(Txt_Fec_Pag.Text) = "" Then
            '    Msj.Mensaje(me.page, Caption, "Debe ingresar fecha de pago", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Fec_Pag.Focus()
            '    Return False
            'End If


            'If Trim(Txt_Hor_Veri.Text) = "" Then
            '    Msj.Mensaje(me.page, Caption, "Debe ingresar hora de verificación", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Hor_Veri.Focus()
            '    Return False
            'End If

            'If Not IsDate(Trim(Txt_Hor_Veri.Text)) Then
            '    Msj.Mensaje(me.page, Caption, "Hora de verificación incorrecta", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Hor_Veri.Focus()
            '    Return False
            '    Exit Function
            'End If

            'If Trim(Txt_Fec_Ges.Text) = "" Then
            '    Msj.Mensaje(me.page, Caption, "Debe ingresar fecha de gestión", TipoDeMensaje._Exclamacion, Nothing, false)
            '    Txt_Fec_Ges.Focus()
            '    Return False
            'End If

            If Not IsDate(Txt_Fec_Vcto.Text) Then
                Msj.Mensaje(Me.Page, Caption, "Fecha vencimiento no es valida", TipoDeMensaje._Exclamacion, Nothing, False)
                Return False
            End If


            Return True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, "Error en Validación", ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Function

    Private Sub FormatoGrillaDoctosAVerificar()

        For i = 1 To Sesion.coll_Dvf.Count
            GV_DoctosDvf.Rows(i - 1).Cells(4).Text = Format(CLng(Sesion.coll_Dvf(i).dvf_mto), FMT.FCMSD)
            GV_DoctosDvf.Rows(i - 1).Cells(6).Text = Format(Sesion.coll_Dvf(i).dvf_fev_rea, Var.FMT_FECHA)
            GV_DoctosDvf.Rows(i - 1).Cells(9).Text = Format(Sesion.coll_Dvf(i).dvf_fec_pag, Var.FMT_FECHA)
            GV_DoctosDvf.Rows(i - 1).Cells(10).Text = Format(Sesion.coll_Dvf(i).dvf_fec_pri_gsn, Var.FMT_FECHA)
            GV_DoctosDvf.Rows(i - 1).Cells(11).Text = Format(Sesion.coll_Dvf(i).dvf_fec_vfc, Var.FMT_FECHA)
            GV_DoctosDvf.Rows(i - 1).Cells(12).Text = Format(Sesion.coll_Dvf(i).dvf_fec_ing, Var.FMT_FECHA)
        Next

    End Sub

    Private Sub HabilitaDeudor(ByVal Estado As Boolean)

        If Estado Then

            Txt_Rso_Deu.CssClass = "clsMandatorio"
            Txt_Ape_Pat.CssClass = "clsMandatorio"
            Txt_Ape_Mat.CssClass = "clsMandatorio"

            Dp_Tip_Deu.CssClass = "clsMandatorio"
            Dp_Abr_Raz_Soc.CssClass = "clsMandatorio"
            Dp_Zon_Rie.CssClass = "clsMandatorio"
            Dp_Raz_Cnt.CssClass = "clsMandatorio"

            Txt_Rso_Deu.ReadOnly = False
            Txt_Ape_Pat.ReadOnly = False
            Txt_Ape_Mat.ReadOnly = False

            Dp_Tip_Deu.Enabled = True
            Dp_Abr_Raz_Soc.Enabled = True
            Dp_Zon_Rie.Enabled = True
            Dp_Raz_Cnt.Enabled = True

        Else

            Txt_Rso_Deu.CssClass = "clsDisabled"
            Txt_Ape_Pat.CssClass = "clsDisabled"
            Txt_Ape_Mat.CssClass = "clsDisabled"

            Dp_Tip_Deu.CssClass = "clsDisabled"
            Dp_Abr_Raz_Soc.CssClass = "clsDisabled"
            Dp_Zon_Rie.CssClass = "clsDisabled"
            Dp_Raz_Cnt.CssClass = "clsDisabled"

            Txt_Rso_Deu.ReadOnly = True
            Txt_Ape_Pat.ReadOnly = True
            Txt_Ape_Mat.ReadOnly = True

            Dp_Tip_Deu.Enabled = False
            Dp_Abr_Raz_Soc.Enabled = False
            Dp_Zon_Rie.Enabled = False
            Dp_Raz_Cnt.Enabled = False

        End If

    End Sub

#End Region


    
   
    
End Class