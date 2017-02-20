Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class ClsCallTelefonico
    Inherits System.Web.UI.Page

#Region "DECLARACION VARIABLES GENERALES PAGINA"

    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim AG As New ActualizacionesGenerales
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Caption As String = "Control de LLamadas"
    Dim Msj As New ClsMensaje
    Dim Sesion As New ClsSession.ClsSession
    Dim CBZ As New ClaseCobranza

#End Region

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Modulo = "Cobranza"
        Pagina = Page.AppRelativeVirtualPath
        CambioTema(Page)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                NroPaginacion = 0

                LimpiaTotales()

                'Retorna Cobradores Telefónicoa
                CG.EjecutivosDevuelve(Dp_Ejecutivos, CodEje, 29)
                
                IB_Gestionar.Enabled = False
            End If

            If Page.IsPostBack Then
                If Dp_Ejecutivos.SelectedValue <> "0" Then
                    IB_Gestionar.Enabled = True
                Else
                    IB_Gestionar.Enabled = False
                End If
            End If

            'Dim Agt As New Perfiles.Cls_Principal

            'If Agt.ValidaAccesso(20, 20020407, Usr, "PRESIONO BOTON INGRESO DE GESTION ") = False Then

            '    IB_Gestionar.Attributes.Add("onclick", "javascript:Denega_Acceso();")
            'Else
            IB_Gestionar.Attributes.Add("onclick", "WinOpen(1, 'GestionCobranza.aspx', 'Popup', 1320, 1000, 0, 0);")
            'End If

            'If Agt.ValidaAccesso(20, 20010407, Usr, "PRESIONO BOTON CONTROL DE LLAMADAS ") = False Then
            '    IB_Control_Llamadas.Attributes.Add("onclick", "javascript:Denega_Acceso();")
            'Else
            IB_Control_Llamadas.Attributes.Add("onclick", "WinOpen(1, 'Informe_Llamadas.aspx', 'Llamadas', 1050, 705, 10, 10);")
            'End If

            If GV_TotalPorCodigo.Rows.Count > 0 Then

                If Dp_Ejecutivos.SelectedValue > 0 Then

                    'Retorna Totales Pro Código para Cobrador Seleccionado
                    GV_TotalPorCodigo.DataSource = CBZ.DocumentosAGestionar_TotalesPorCodigo(Dp_Ejecutivos.SelectedValue)
                    GV_TotalPorCodigo.DataBind()
                    SetFooter_GrillaTotalPorCodigo()

                    If GV_TotalPorCodigo.Rows.Count > 0 Then
                        'Retorna Totales para Cobrador Seleccionado
                        SetTotales_Cobrador(Dp_Ejecutivos.SelectedValue)
                    End If

                End If

            End If

        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dp_Ejecutivos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Ejecutivos.SelectedIndexChanged
        Try
            'Refresca Seleccion de Reemplazo
            CB_Reemplazo.Checked = False
            Dp_Ejecutivos_Reemplazo.ClearSelection()
            Dp_Ejecutivos_Reemplazo.Enabled = False
            Dp_Ejecutivos_Reemplazo.CssClass = "clsDisabled"
            'Retorna Reemplazo de Cobrador Telefónico Seleccionado
            CBZ.EjecutivosRetornaReemplazos(Dp_Ejecutivos_Reemplazo, Dp_Ejecutivos.SelectedValue)
            'Retorna Totales Pro Código para Cobrador Seleccionado
            GV_TotalPorCodigo.DataSource = CBZ.DocumentosAGestionar_TotalesPorCodigo(Dp_Ejecutivos.SelectedValue)
            GV_TotalPorCodigo.DataBind()
            If GV_TotalPorCodigo.Rows.Count > 0 Then
                SetFooter_GrillaTotalPorCodigo()
                'Retorna Totales para Cobrador Seleccionado
                SetTotales_Cobrador(Dp_Ejecutivos.SelectedValue)
                'Se cambia variable CodEje 
                'CodEje = Dp_Ejecutivos.SelectedItem.Value
                Sesion.Ejecutivo = Dp_Ejecutivos.SelectedValue
                IB_Gestionar.Enabled = True
            Else

                Msj.Mensaje(Me.Page, Caption, "No se encontraron Pagadores para este ejecutivo", ClsMensaje.TipoDeMensaje._Informacion)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub Dp_Ejecutivos_Reemplazo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Ejecutivos_Reemplazo.SelectedIndexChanged
        Try
            'Valida que permita seleccionar reemplazo
            If CB_Reemplazo.Checked Then
                'Retorna Totales Pro Código para Cobrador Seleccionado
                GV_TotalPorCodigo.DataSource = CBZ.DocumentosAGestionar_TotalesPorCodigo(Dp_Ejecutivos_Reemplazo.SelectedValue)
                GV_TotalPorCodigo.DataBind()
                SetFooter_GrillaTotalPorCodigo()
                'Retorna Totales para Cobrador Seleccionado
                SetTotales_Cobrador(Dp_Ejecutivos_Reemplazo.SelectedValue)
            End If
        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub CB_Reemplazo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Reemplazo.CheckedChanged
        Try
            If Dp_Ejecutivos_Reemplazo.Items.Count = 1 Then
                CB_Reemplazo.Checked = False
                Msj.Mensaje(Me.Page, Caption, "No Existen Cobradores para Reemplazo", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If
            If CB_Reemplazo.Checked Then
                Dp_Ejecutivos_Reemplazo.Enabled = True
                Dp_Ejecutivos_Reemplazo.CssClass = "clsMandatorio"
            Else
                Dp_Ejecutivos_Reemplazo.Enabled = False
                Dp_Ejecutivos_Reemplazo.CssClass = "clsDisabled"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lb_msj_pta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_msj_pta.Click
        Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If NroPaginacion = 0 Then
                Msj.Mensaje(Me, Caption, "Ya ha llegado al comienzo de la lista", 2)
                Exit Sub
            End If
            If NroPaginacion >= 10 Then
                NroPaginacion -= 10
                Dp_Ejecutivos_SelectedIndexChanged(Me, e)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try
            If GV_TotalPorCodigo.Rows.Count < 10 Then
                Msj.Mensaje(Me, Caption, "Ya está en la última página de la lista", 2)
                Exit Sub
            End If
            If GV_TotalPorCodigo.Rows.Count = 10 Then
                NroPaginacion += 10
                Dp_Ejecutivos_SelectedIndexChanged(Me, e)
            End If
        Catch ex As Exception
            Msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try

            GV_TotalPorCodigo.DataSource = Nothing
            GV_TotalPorCodigo.DataBind()

            Dp_Ejecutivos_Reemplazo.ClearSelection()
            Dp_Ejecutivos.ClearSelection()

            total1.Text = 0
            total2.Text = 0
            total3.Text = 0
            total4.Text = 0
            total5.Text = 0
            total6.Text = 0

            CB_Reemplazo.Checked = False
            Dp_Ejecutivos_Reemplazo.Enabled = False
            Dp_Ejecutivos_Reemplazo.CssClass = "clsDisabled"
            Dp_Ejecutivos.Focus()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Sub"

    Protected Sub SetFooter_GrillaTotalPorCodigo()

        Dim TotalMto As Double
        Dim TotalDeudores As Int16
        Dim TotalDoctos As Int32

        For i = 0 To GV_TotalPorCodigo.Rows.Count - 1
            TotalDeudores = TotalDeudores + CInt(GV_TotalPorCodigo.Rows(i).Cells(2).Text)
            TotalDoctos = TotalDoctos + CInt(GV_TotalPorCodigo.Rows(i).Cells(3).Text)
            TotalMto = TotalMto + CDbl(GV_TotalPorCodigo.Rows(i).Cells(4).Text)
            GV_TotalPorCodigo.Rows(i).Cells(4).Text = Format(CDbl(GV_TotalPorCodigo.Rows(i).Cells(4).Text), Fmt.FCMSD)
            Select Case GV_TotalPorCodigo.Rows(i).Cells(5).Text.Trim.ToUpper
                Case "S"
                    GV_TotalPorCodigo.Rows(i).Cells(5).Text = "SI"
                Case "N"
                    GV_TotalPorCodigo.Rows(i).Cells(5).Text = "NO"
            End Select
        Next
        'GV_TotalPorCodigo.Columns(2).FooterText = TotalDeudores
        'GV_TotalPorCodigo.Columns(3).FooterText = TotalDoctos
        'GV_TotalPorCodigo.Columns(4).FooterText = TotalMto
        'GV_TotalPorCodigo.Columns(4).FooterText = Format(TotalMto, Fmt.FCMSD)
        'GV_TotalPorCodigo.DataBind()
    End Sub

    Protected Sub SetTotales_Cobrador(ByVal CobradorTelefonico As Int16)
        Dim SetTotales = CBZ.DocumentosAGestionar_RetornaTotales(CobradorTelefonico)
        LimpiaTotales()
        For Each Ciclo In SetTotales
            If Ciclo.Gestionado = "S" Then
                total1.Text = Format(Ciclo.MontoDoctos, Fmt.FCMSD)
                total2.Text = Format(Ciclo.CantidadDoctos, Fmt.FCMSD)
                total3.Text = Format(Ciclo.CantidadDeudores, Fmt.FCMSD)
            Else
                total4.Text = Format(Ciclo.MontoDoctos, Fmt.FCMSD)
                total5.Text = Format(Ciclo.CantidadDoctos, Fmt.FCMSD)
                total6.Text = Format(Ciclo.CantidadDeudores, Fmt.FCMSD)
            End If
        Next
    End Sub

    Protected Sub LimpiaTotales()
        total1.Text = Format(0, Fmt.FCMSD)
        total2.Text = Format(0, Fmt.FCMSD)
        total3.Text = Format(0, Fmt.FCMSD)
        total4.Text = Format(0, Fmt.FCMSD)
        total5.Text = Format(0, Fmt.FCMSD)
        total6.Text = Format(0, Fmt.FCMSD)
    End Sub

#End Region
    
End Class