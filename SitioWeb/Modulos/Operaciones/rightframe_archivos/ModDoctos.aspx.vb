Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports ClsSession.SesionAplicaciones

Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_ModDoctos
    Inherits System.Web.UI.Page

    Dim Msj As New ClsMensaje
    Dim Caption As String = "Documentos"
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Sesion_Op As New ClsSession.SesionOperaciones
    Dim OP As New ClaseOperaciones
    Dim FMT As New FuncionesGenerales.ClsLocateInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Txt_Rut_Deu2.Attributes.Add("Style", "TEXT-ALIGN: right")
                If Request.QueryString("Nro") <> "" Then

                    TraeDetalleDoctos(CInt(Trim(Request.QueryString("Posi"))))
                End If
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Guardar_Click(sender As Object, e As ImageClickEventArgs) 'Handles IB_Guardar.Click
        Try
            If ChkB_Deudor.Checked Then
                If Trim(Txt_Rut_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar NIT de deudor", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    'MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) = "" Then
                    Msj.Mensaje(Me.Page, Caption, "Debe ingresar digito de deudor", TipoDeMensaje._Exclamacion)
                    Txt_Dig_Deu2.Focus()
                    'MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If
                If Trim(Txt_Dig_Deu2.Text) <> FC.Vrut(Txt_Rut_Deu2.Text) Then
                    Msj.Mensaje(Me.Page, Caption, "Rut de duedor incorrecto", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    'MlPopupExt_ModDoctos.Show()
                    Exit Sub
                End If

                Dim Deu As deu_cls

                Deu = CG.DeudorDevuelvePorRut(Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT))
                If IsNothing(Deu) Then
                    Msj.Mensaje(Me.Page, Caption, "Deudor no existe", TipoDeMensaje._Exclamacion)
                    Txt_Rut_Deu2.Focus()
                    Exit Sub
                End If
                Txt_Nom_Deu2.Text = Trim(Deu.deu_rso) & " " & Trim(Deu.deu_ape_ptn) & " " & Trim(Deu.deu_ape_mtn)
            End If

            Msj.Mensaje(Me.Page, Caption, "¿Esta seguro de guardar los cambios?", ClsMensaje.TipoDeMensaje._Confirmacion, Link_Guardar.UniqueID, True)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Cancelar2_Click(sender As Object, e As ImageClickEventArgs) Handles IB_Cancelar2.Click
        Response.Redirect("MClientes.aspx", False)
    End Sub

    Protected Sub Link_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link_Guardar.Click
        Try

            Dim StrMsj As String

            StrMsj = OP.OperacionModificaDocto(Format(CLng(Txt_Rut_Cli2.Text), Var.FMT_RUT), Format(CLng(Txt_Rut_Deu2.Text), Var.FMT_RUT), _
                                               Format(CLng(Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).deu_ide), Var.FMT_RUT), _
                                               Txt_Nro_Ope2.Text, Txt_Nro_Doc.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_num, _
                                               Txt_Cuota2.Text, Sesion_Op.Coll_DOC(CInt(Txt_PosGv.Value + 1)).dsi_flj_num)

            'LlenaGrilla()
            'MarcaGrilla()

            Msj.Mensaje(Me.Page, Caption, StrMsj, TipoDeMensaje._Exclamacion)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkB_Docto_CheckedChanged(sender As Object, e As EventArgs) Handles ChkB_Docto.CheckedChanged
        Try
            If ChkB_Docto.Checked Then
                Txt_Nro_Doc.CssClass = "clsMandatorio"
                Txt_Cuota2.CssClass = "clsMandatorio"
                Txt_Nro_Doc.ReadOnly = False
                Txt_Cuota2.ReadOnly = False
                Txt_Nro_Doc.Focus()
            Else
                Txt_Nro_Doc.CssClass = "clsDisabled"
                Txt_Cuota2.CssClass = "clsDisabled"
                Txt_Nro_Doc.ReadOnly = True
                Txt_Cuota2.ReadOnly = True
            End If
            'MlPopupExt_ModDoctos.Show()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub ChkB_Deudor_CheckedChanged(sender As Object, e As EventArgs) Handles ChkB_Deudor.CheckedChanged
        Try
            If ChkB_Deudor.Checked Then
                Txt_Rut_Deu2.CssClass = "clsMandatorio"
                Txt_Rut_Deu2.ReadOnly = False
                Txt_Dig_Deu2.CssClass = "clsMandatorio"
                Txt_Dig_Deu2.ReadOnly = False
                Txt_Rut_Deu2.Focus()
                Txt_Rut_Deu2.Text = ""
                Txt_Dig_Deu2.Text = ""

            Else
                Txt_Rut_Deu2.CssClass = "clsDisabled"
                Txt_Rut_Deu2.ReadOnly = True
                Txt_Dig_Deu2.CssClass = "clsDisabled"
                Txt_Dig_Deu2.ReadOnly = True
            End If
            'MlPopupExt_ModDoctos.Show()
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try
    End Sub

    Private Sub LimpiaControlesModificaDocto()
        Txt_Rut_Cli2.Text = ""
        Txt_Dig_Cli2.Text = ""
        Txt_Nom_Cli2.Text = ""
        Txt_Rut_Deu2.Text = ""
    End Sub

    Private Sub TraeDetalleDoctos(ByVal posicion As Integer)



        Try
            'LimpiaControlesModificaDocto()
            'Cartola Documento


            'Modificación Documento
            Txt_Rut_Cli2.Text = CInt(Sesion_Op.Coll_DOC(CInt(posicion)).cli_idc)
            Txt_Dig_Cli2.Text = FC.Vrut(Txt_Rut_Cli2.Text)
            Txt_Nom_Cli2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).cli_rso
            Txt_Rut_Deu2.Text = CInt(Sesion_Op.Coll_DOC(CInt(posicion)).deu_ide)
            Txt_Dig_Deu2.Text = FC.Vrut(Txt_Rut_Deu2.Text)
            Txt_Nom_Deu2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).deu_rso
            Txt_Nro_Ope2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).id_ope
            Txt_Nro_Oto2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).opo_otg
            Txt_Tip_Doc2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).tipo_docto
            Txt_Est_Doc.Text = Sesion_Op.Coll_DOC(CInt(posicion)).Est_Docto
            Txt_Fec_Vto.Text = Sesion_Op.Coll_DOC(CInt(posicion)).dsi_fev_ori

            Txt_Mon.Text = Sesion_Op.Coll_DOC(CInt(posicion)).moneda

            If Sesion_Op.Coll_DOC(CInt(posicion)).id_P_0023 = 1 Then
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(posicion)).dsi_mto, FMT.FCMSD)
            ElseIf Sesion_Op.Coll_DOC(CInt(posicion)).id_P_0023 = 2 Then
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(posicion)).dsi_mto, FMT.FCMCD4)
            Else
                Txt_Mto.Text = Format(Sesion_Op.Coll_DOC(CInt(posicion)).dsi_mto, FMT.FCMCD)
            End If

            Txt_Nro_Doc.Text = Sesion_Op.Coll_DOC(CInt(posicion)).dsi_num
            Txt_Cuota2.Text = Sesion_Op.Coll_DOC(CInt(posicion)).dsi_flj_num

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub
End Class
