Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports ClsSession.ClsSession
Imports FuncionesGenerales.Errores
Imports System.Web.UI.UserControl
Imports CapaDatos

Public Class Manccf
    Inherits System.Web.UI.Page



#Region " Declaración de Variables privadas"

    Private cg As New ConsultasGenerales
    Private ag As New ActualizacionesGenerales
    Dim cd As New ClaseControlDual
    Private Msj As New ClsMensaje
    Dim Agt As New Perfiles.Cls_Principal
    Dim rw As New FuncionesGenerales.RutinasWeb

#End Region

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Modulo = "Mantencion"
        Pagina = Page.AppRelativeVirtualPath
        CambioTema(Page)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            carga_clasificacion()
            btn_suc_apb.Visible = False

        End If

    End Sub

    Protected Sub btn_nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nuevo.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20011512, Usr, "PRESIONO BOTON NUEVA CLASIFICACION") = False Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        modifica = False
        Response.Redirect("clasificacion.aspx")

    End Sub

    Protected Sub btn_mod_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_mod.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Agt.ValidaAccesso(20, 20021512, Usr, "PRESIONO BOTON MODIFICAR") = False Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        If ccfnum <> 0 Then
            'ccfnum = Me.Gr_Ccf.Rows(pos.Value).Cells(1).Text
            modifica = True
            Response.Redirect("clasificacion.aspx", False)
        Else
            Msj.Mensaje(Me.Page, "Clasificaciones", "Debes Seleccionar una Clasificación", TipoDeMensaje._Exclamacion)
        End If
    End Sub

    Protected Sub LB_Marca_Grilla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Marca_Grilla.Click

    End Sub

    Public Sub carga_clasificacion()

        Dim coll As New Collection

        coll = cd.DevuelveCabeceraClasificacion

        Me.Gr_Ccf.DataSource = coll
        Me.Gr_Ccf.DataBind()


        For I = 0 To Gr_Ccf.Rows.Count - 1

            Select Case Gr_Ccf.Rows(I).Cells(3).Text
                Case "A"
                    Gr_Ccf.Rows(I).Cells(3).Text = "ACTIVO"
                Case "I"
                    Gr_Ccf.Rows(I).Cells(3).Text = "INACTIVO"
            End Select

            Select Case Gr_Ccf.Rows(I).Cells(4).Text
                Case 1
                    Gr_Ccf.Rows(I).Cells(4).Text = "APRUEBA CASA MATRIZ"
                Case 2
                    Gr_Ccf.Rows(I).Cells(4).Text = "SE APRUEBA A SI MISMA"
                Case 3
                    Gr_Ccf.Rows(I).Cells(4).Text = "APROBACION MIXTA"
            End Select

        Next
    End Sub

    Protected Sub acceso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles acceso.Click
        Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
    End Sub

    Protected Sub BtnDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)

        'If Agt.ValidaAccesso(20, 20021512, Usr, "PRESIONO BOTON MODIFICAR") = False Then
        'Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
        'Exit Sub
        'End If

        For m = 0 To Gr_Ccf.Rows.Count - 1
            If (Gr_Ccf.Rows(m).Cells(1).Text = btn.ToolTip) Then
                ccfnum = Me.Gr_Ccf.Rows(m).Cells(1).Text
                If (m Mod 2) = 0 Then
                    Gr_Ccf.Rows(m).CssClass = "selectable"
                Else
                    Gr_Ccf.Rows(m).CssClass = "selectableAlt"
                End If
            Else
                If (m Mod 2) = 0 Then
                    Gr_Ccf.Rows(m).CssClass = "formatUltcell"
                Else
                    Gr_Ccf.Rows(m).CssClass = "formatUltcellAlt"
                End If
            End If
        Next

        btn_mod.Enabled = True
        btn_suc.Enabled = True
        btn_suc_apb.Enabled = True
        BTN_FIRMAS.Enabled = True

    End Sub

    Protected Sub btn_suc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_suc.Click
        If Agt.ValidaAccesso(20, 20041512, Usr, "PRESIONO ASOCIAR SUCURSAL") = False Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
        Else
            'Me.btn_suc.Attributes.Add("onClick", "WinOpen(2,'cfc-suc.aspx','popupasit',400,370,250,250);")
            AbrePopup(Page, 2, "cfc-suc.aspx", "popupasit", 500, 450, 250, 250)
        End If
    End Sub

    Protected Sub btn_suc_apb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_suc_apb.Click
        If Agt.ValidaAccesso(20, 20051512, Usr, "PRESIONO SUCURSAL DE APROBACIONES CLASIFICACION") = False Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
        Else
            'Me.btn_suc_apb.Attributes.Add("onClick", "WinOpen(2,'Suc_Aprobacion.aspx','popupasit',620,670,250,250);")
            AbrePopup(Page, 2, "Suc_Aprobacion.aspx", "popupasit", 630, 650, 250, 250)
        End If
    End Sub

    Protected Sub BTN_FIRMAS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BTN_FIRMAS.Click
        If Agt.ValidaAccesso(20, 20031512, Usr, "PRESIONO BOTON FIRMAS") = False Then
            Msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
        Else
            'Me.BTN_FIRMAS.Attributes.Add("onClick", "WinOpen(2,'Firmas.aspx?id=" & ccfnum & "', 'popupasit',870,380,250,250);")
            AbrePopup(Page, 2, "Firmas.aspx?id=" & ccfnum, "popupasit", 415, 400, 250, 250)
        End If
    End Sub

End Class

