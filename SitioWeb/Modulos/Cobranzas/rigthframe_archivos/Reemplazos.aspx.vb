Imports ClsSession.ClsSession
Imports CapaDatos
Imports ClsSession.SesionCobranza
Partial Class Modulos_Cobranzas_rigthframe_archivos_Default
    Inherits System.Web.UI.Page
    Dim Msj As New ClsMensaje
    Dim CG As New ConsultasGenerales
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Sucursal = 1
            NroPaginacion_Reemplazo = 0
            CG.EjecutivosDevuelve(Me.Drop_Cobradores, CodEje, 29)
            GridView2.DataSource = CG.EjecutivosAsignarCobradoresDevuelve(Sucursal, 29, False, Nothing, 10)
            GridView2.DataBind()
        End If
        IB_Volver.Attributes.Add("onClick", "JavaScript:window.close();")
    End Sub
    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click
        Dim agt As New Perfiles.Cls_Principal
        If Not agt.ValidaAccesso(20, 20030207, Usr, "PRESIONO GUARDAR REEMPLAZO") Then
            Msj.Mensaje(Me.Page, "Reemplazos", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        Dim CodigoCobrador As Integer
        Dim CodigoCobradorReemplazo As Integer
        Dim AG As New ActualizacionesGenerales
        If Drop_Cobradores.SelectedValue <> "0" Then
            CodigoCobrador = Drop_Cobradores.SelectedValue
            '/*Borrar Códigos de Reemplazo*/
            If Not AG.ReemplazosCobradoresBorrar(CodigoCobrador) Then
                Msj.Mensaje(Me, "Error", "!Error al Eliminar Registros de Reemplazo!", 1, Nothing, False)
                Exit Sub
            End If
            Dim BanderaGuarda As Boolean = False
            Dim BanderaSeleccion As Boolean = False
            For i = 0 To GridView2.Rows.Count - 1
                Dim cb As CheckBox
                cb = GridView2.Rows(i).FindControl("CheckBox2")
                If cb.Checked Then
                    BanderaSeleccion = True
                    CodigoCobradorReemplazo = GridView2.Rows(i).Cells(1).Text
                    If GridView2.Rows(i).Cells(1).Text = Drop_Cobradores.SelectedValue Then
                        Msj.Mensaje(Me, "Atención", "!No se puede reemplazar el mismo cobrador!", 2, Nothing, False)
                        Exit Sub
                    End If
                    '/*Guardar en LINQ*/
                    If AG.ReemplazosCobradoresInsertar(CodigoCobrador, CodigoCobradorReemplazo) Then
                        BanderaGuarda = True
                    Else
                        BanderaGuarda = False
                    End If
                End If
            Next
            If Not BanderaSeleccion Then
                Msj.Mensaje(Me, "Atención", "!Debe seleccionar al menos un reemplazo!", 2, Nothing, False)
                Exit Sub
            Else
                If BanderaGuarda And BanderaSeleccion Then
                    Msj.Mensaje(Me, "Atención", "!Registro de Reemplazo Exitoso!", 2, Nothing, False)
                Else
                    Msj.Mensaje(Me, "Error", "!Error al Insertar Registros de Reemplazo!", 1, Nothing, False)
                End If
            End If
        Else
            Msj.Mensaje(Me, "Atencion", "!Seleccione un Cobrador!", 1, Nothing, False)
        End If
    End Sub
    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion_Reemplazo = 0 Then
                Msj.Mensaje(Me, "Atención", "Ya ha llegado al comienzo de la lista", 2, False)
                Exit Sub
            End If
            If NroPaginacion_Reemplazo >= 10 Then
                NroPaginacion_Reemplazo -= 10
                GridView2.DataSource = CG.EjecutivosAsignarCobradoresDevuelve(Sucursal, 29, False, Nothing, 10)
                GridView2.DataBind()
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GridView2.Rows.Count < 10 Then
                Msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            If GridView2.Rows.Count = 10 Then
                NroPaginacion_Reemplazo += 10
                GridView2.DataSource = CG.EjecutivosAsignarCobradoresDevuelve(Sucursal, 29, False, Nothing, 10)
                GridView2.DataBind()
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub
End Class
