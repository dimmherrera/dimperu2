Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Recaudacion_rigthframe_archivos_ing_factoring
    Inherits System.Web.UI.Page
    Dim msj As New ClsMensaje
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, dr_fact)
            Me.txt_cod.Attributes.Add("Style", "TEXT-ALIGN: right")
        End If
        btn_volver.Attributes.Add("onClick", "javascript:window.close();")
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'Pagina = Mid(Page.AppRelativeVirtualPath, InStr("/", Page.AppRelativeVirtualPath), Page.AppRelativeVirtualPath.Length)
        If Not IsPostBack Then
            Modulo = "Recaudacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If


    End Sub

    Protected Sub dr_fact_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dr_fact.SelectedIndexChanged

        If Me.dr_fact.SelectedValue <> 0 Then
            Dim col As New Collection
            col = cg.ParametrosAfanumericosDevuelveDetalle(TablaAlfanumerico.Factoring, Me.dr_fact.SelectedValue)
            Me.dr_estado.SelectedValue = col.Item(1).pal_est
            Me.txt_cod.Text = Me.dr_fact.SelectedValue

        End If

    End Sub

    Protected Sub btn_nuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_nuevo.Click

        Me.txt_cod.Text = ""
        Me.txt_cod.ReadOnly = False
        Me.txt_cod.CssClass = "clsMandatorio"
        Me.dr_fact.Visible = False
        Me.txt_des.Visible = True
        Me.dr_estado.SelectedValue = 0
        Me.txt_cod.Focus()


    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click


        If Me.dr_fact.Visible = False Then

            If Me.txt_cod.Text = "" Then
                msj.Mensaje(Me, "Atención", "Debe Ingresar codigo", ClsMensaje.TipoDeMensaje._Informacion, , False)
                Exit Sub
            End If


            If Me.txt_des.Text = "" Then
                msj.Mensaje(Me, "Atención", "Debe Ingresar una descripción", ClsMensaje.TipoDeMensaje._Informacion, , False)
                Exit Sub
            End If


            If Me.dr_estado.SelectedValue = "S" Then
                msj.Mensaje(Me, "Atención", "Debe seleccionar un Estado", ClsMensaje.TipoDeMensaje._Informacion, , False)
                Exit Sub
            End If

            Dim FC As New PL_000069_cls

            With FC
                .pal_des = Me.txt_des.Text
                .pal_sis = "N"
                .pal_est = Me.dr_estado.SelectedValue
                .id_PL_000069 = Format(CLng(Me.txt_cod.Text), "000000")

            End With
            ag.FactInserta(FC)
            cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, dr_fact)
            btn_limpiar_Click(Me, e)
        End If

    End Sub


    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click

        If Me.dr_fact.SelectedValue <> 0 Then
            ag.EliminaParametrosAlfa(4, Me.dr_fact.SelectedValue)
        Else
            msj.Mensaje(Me, "Información", "Debe seleccionar un parametro para eliminar", ClsMensaje.TipoDeMensaje._Informacion, False)
        End If
    End Sub

    Protected Sub btn_limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_limpiar.Click

        Me.txt_cod.ReadOnly = True
        Me.txt_cod.Text = ""
        Me.txt_cod.CssClass = "clsDisabled"
        Me.txt_des.Visible = False
        Me.txt_des.Text = ""
        cg.ParametrosAlfanumericoDevuelve(TablaAlfanumerico.Factoring, True, dr_fact)
        Me.dr_fact.Visible = True

        Me.dr_fact.SelectedValue = 0
        Me.dr_estado.SelectedValue = 0



    End Sub
End Class
