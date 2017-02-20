Imports FuncionesGenerales.Variables
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.IO

Partial Class Modulos_Operaciones_rightframe_archivos_Pop_up_MasivaCal
    Inherits System.Web.UI.Page

#Region "DECLARACION VARIABLES GENERALES PAGINA"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim AG As New ActualizacionesGenerales
    Dim CO As New ClaseOperaciones
    Dim Msj As New ClsMensaje
    Dim ClsCli As New ClaseClientes
    Dim Var As New FuncionesGenerales.Variables
    Dim Caption As String = "Calificacion De Arrastre"


#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack() Then

        End If
        Btn_Volver.Attributes.Add("onClick", "javascript:window.close()")
    End Sub

    Protected Sub Btn_Aceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Aceptar.Click
        Try
            Dim archivo As String = FileUpload_Cal.FileName

            If archivo.Trim <> "" Then

                FileUpload_Cal.SaveAs(MapPath("../../Linea de Credito/Actas/" + FileUpload_Cal.FileName))

                Dim path As String = Server.MapPath("../../Linea de Credito/Actas/" + archivo)
                Dim freader As New StreamReader(path)
                Dim Contenido As String
                Dim Cal As String = "" 'Calificacion de arrastre
                Dim Cto As String = "" 'Contrato

                Do

                    Contenido = freader.ReadLine()

                    If Not Contenido Is Nothing Then

                        If Contenido.Length <> 0 Then

                            Cto = Contenido.Trim.Substring(0, 18)

                            Select Case Contenido.Trim.Substring(22, 1)
                                Case "1" : Cal = "A"
                                Case "2" : Cal = "B"
                                Case "3" : Cal = "C"
                                Case "4" : Cal = "D"
                                Case "5" : Cal = "E"
                            End Select

                            CO.Guarda_CalificacionArraste(Cal, Cto)

                        End If

                    End If

                Loop Until Contenido Is Nothing

                freader.Close()
                File.Delete(path)
                Msj.Mensaje(Page, Caption, "Calificación ingresada correctamente", TipoDeMensaje._Exclamacion)

            Else
                Msj.Mensaje(Page, Caption, "Seleccione un archivo", TipoDeMensaje._Exclamacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, "Error: " + ex.ToString(), TipoDeMensaje._Error)
        End Try

    End Sub

End Class
