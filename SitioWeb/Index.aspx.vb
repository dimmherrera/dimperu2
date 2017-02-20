Imports System
Imports System.Net
Imports System.IO
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports System.Text
Imports FuncionesGenerales
Imports System.Data

Partial Class Index
    Inherits System.Web.UI.Page

    Dim Msj As New ClsMensaje


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Response.Expires = -1


            If Not IsPostBack Then

                'Response.Redirect("Index.aspx", False)

                Dim Sesion As New ClsSession.ClsSession
                Dim CG As New ConsultasGenerales

                Sesion.iniciarSesion()
                Sesion.iniciarSesionMonedas()

                If Request.QueryString("usr") = "" Then
                    Msj.Mensaje(Me, "DESCUENTO TITULO VALOR", "acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion, Nothing, False)
                    ClosePag(Me)
                    Exit Sub
                Else
                    Usr = Request.QueryString("usr")
                End If

                '--------------------------------------------------------------
                'SOLO PARA PRUEBAS Y DEPURACION
                '--------------------------------------------------------------
                'Usr = "admin"
                '--------------------------------------------------------------
                Dim sql As New FuncionesGenerales.SqlQuery

                If Not sql.ValidaConexion() Then
                    Msj.Mensaje(Me, "DESCUENTO TITULO VALOR", "Error al conectar con BD: " & sql.mensaje, ClsMensaje.TipoDeMensaje._Error, Nothing, False)
                    Exit Sub
                End If

                Dim Agt As New Perfiles.Cls_Principal

                If Agt.ValidaAccesso(20, 20000000, Usr, "VALIDA INGRESO AL SISTEMA DE DESCUENTO TITULO VALOR") Then


                    Dim Ejecutivo As New DataSet
                    
                    Ejecutivo = CG.EjecutivoPorAliasDevuelve(Usr)

                    If Not IsNothing(Ejecutivo) And Ejecutivo.Tables(0).Rows.Count > 0 Then
                        Sucursal = Convert.ToInt32(Ejecutivo.Tables(0).Rows(0)("id_suc").ToString())
                        CodEje = Convert.ToInt32(Ejecutivo.Tables(0).Rows(0)("id_eje").ToString())
                        Pfl = Convert.ToInt32(Ejecutivo.Tables(0).Rows(0)("id_P_0045").ToString())
                        Perfil = Ejecutivo.Tables(0).Rows(0)("pnu_des").Trim()

                        Session.Item("valida_sesion") = CodEje
                        Response.Redirect("~/Modulos/Inicio/rightframe_archivos/Inicio.aspx", True)

                    Else
                        Msj.Mensaje(Me, "DESCUENTO TITULO VALOR", "No se encuentra el ejecutivo", ClsMensaje.TipoDeMensaje._Exclamacion, Nothing, False)
                    End If

                Else
                    Msj.Mensaje(Me, "DESCUENTO TITULO VALOR", "Acceso Denegado: " & Agt.getErrMsg, ClsMensaje.TipoDeMensaje._Exclamacion, Nothing, False)
                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, "FACTORING", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub


End Class
