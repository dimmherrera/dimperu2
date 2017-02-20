Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.IO

Partial Class Modulos_Linea_de_Credito_rigthframe_archivos_Pop_up_Cal
    Inherits System.Web.UI.Page

#Region "DECLARACION VARIABLES GENERALES PAGINA"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim AG As New ActualizacionesGenerales
    Dim Msj As New ClsMensaje
    Dim ClsCli As New ClaseClientes
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Calificacion por Documento"


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack() Then
            TIP_ING.Value = ""
            Txt_Dsi.Text = Request.QueryString("ID").Trim
            CargaDatos()
            Tabla2.Visible = False
        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")

    End Sub

    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click

        Dim CLF As New clf_cls


        Try

            CLF.id_dsi = CLng(Txt_Dsi.Text.Trim)

            'CLF.cal_oto_gam = Txt_Cal_oto.Text.Trim.ToUpper
            'CLF.cal_obj_eti = Txt_Cal_Obj.Text.Trim.ToUpper
            CLF.cal_sub_jet = DP_Cal_Sub.SelectedValue.ToString.Trim.ToUpper
            CLF.cal_arr_ast = DP_Cal_Arr.SelectedValue.ToString.Trim.ToUpper
            'CLF.cal_def_ini = Txt_Cal_Def.Text.Trim.ToUpper

            If ValidaCampos() Then

                If TIP_ING.Value = "NUEVO" Then

                    If AG.CalificacionXClienteInserta(CLF) Then
                        Msj.Mensaje(Me.Page, Caption, "Calificaciones insertadas correctamente", TipoDeMensaje._Exclamacion)
                        CargaDatos()
                    Else
                        Msj.Mensaje(Me.Page, Caption, "Error al insertar Calificaciones", TipoDeMensaje._Error)
                    End If

                ElseIf TIP_ING.Value = "MODIFICA" Then

                    If AG.CalificaionXClienteUpdate(CLF) Then
                        Msj.Mensaje(Me.Page, Caption, "Calificaciones actualizadas correctamente", TipoDeMensaje._Exclamacion)
                        CargaDatos()
                    Else
                        Msj.Mensaje(Me.Page, Caption, "Error al actualizar Calificaciones", TipoDeMensaje._Error)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub CargaDatos()

        Dim Cal As clf_cls

        Try

            Cal = CG.CalificacionesDevuelvePorCli(CLng(Txt_Dsi.Text.Trim))
            If Not IsNothing(Cal) Then

                TIP_ING.Value = "MODIFICA"
                'Me.Txt_Cal_oto.Text = Cal.cal_oto_gam.ToString()
                If Not IsNothing(Cal.cal_oto_gam) Then
                    Me.Dp_Cal_Oto.SelectedValue = Cal.cal_oto_gam.ToString().Trim
                End If

                If Not IsNothing(Cal.cal_obj_eti) Then
                    Me.DP_Cal_Obj.SelectedValue = Cal.cal_obj_eti.ToString().Trim
                End If

                'Me.Txt_Cal_Obj.Text = Cal.cal_obj_eti.ToString()
                
                'Me.Txt_Cal_Sub.Text = Cal.cal_sub_jet.ToString()
                If Not IsNothing(Cal.cal_sub_jet) Then
                    Me.DP_Cal_Sub.SelectedValue = Cal.cal_sub_jet.ToString().Trim
                End If

                'Me.Txt_Cal_Arr.Text = Cal.cal_arr_ast.ToString()
                If Not IsNothing(Cal.cal_arr_ast) Then
                    Me.DP_Cal_Arr.SelectedValue = Cal.cal_arr_ast.ToString().Trim
                End If

                'Me.Txt_Cal_Def.Text = Cal.cal_def_ini.ToString()
                If Not IsNothing(Cal.cal_def_ini.ToString()) Then
                    Me.DP_Cal_Def.SelectedValue = Cal.cal_def_ini.ToString().Trim
                End If
            Else

                TIP_ING.Value = "NUEVO"

            End If
        Catch ex As Exception

        End Try



    End Sub

    Public Function ValidaCampos() As Boolean

        If Me.DP_Cal_Sub.SelectedIndex = 0 Then
            Msj.Mensaje(Page, Caption, "Debe ingresar calificación subjetiva", TipoDeMensaje._Exclamacion)
            Me.DP_Cal_Sub.Focus()
            Return False
        End If

        If Me.DP_Cal_Arr.SelectedIndex = 0 Then
            Msj.Mensaje(Page, Caption, "Debe ingresar calificación de arrastre", TipoDeMensaje._Exclamacion)
            Me.DP_Cal_Arr.Focus()
            Return False
        End If

        Return True

    End Function

    Protected Sub IB_Adj_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Adj.Click
        Tabla2.Visible = True
        IB_Guardar.Visible = False
        IB_Volver.Visible = False
    End Sub

    Protected Sub Btn_Limpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Limpiar.Click
        Tabla2.Visible = False
        IB_Guardar.Visible = True
        IB_Volver.Visible = True
    End Sub

    Protected Sub Btn_Aceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Aceptar.Click

        Try
            Dim archivo As String = FileUpload_Cal.FileName

            If archivo.Trim <> "" Then

                FileUpload_Cal.SaveAs(MapPath("../Actas/" + FileUpload_Cal.FileName))

                Dim path As String = Server.MapPath("../Actas/" + archivo)
                Dim freader As New StreamReader(path)
                Dim Contenido As String
                Dim Cal As String = ""

                Do
                    Contenido = freader.ReadLine()
                    If Not Contenido Is Nothing Then
                        If Contenido.Length <> 0 Then
                            Cal = Contenido.Trim.Substring(22, 1)
                        End If
                    End If
                Loop Until Contenido Is Nothing
                freader.Close()
                File.Delete(path)
                DP_Cal_Arr.SelectedValue = Cal

                Tabla2.Visible = False
                IB_Guardar.Visible = True
                IB_Volver.Visible = True

            Else
                Msj.Mensaje(Page, Caption, "Seleccione un archivo", TipoDeMensaje._Exclamacion)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, "Error: " + ex.ToString(), TipoDeMensaje._Error)
        End Try

    End Sub
End Class


