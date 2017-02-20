Imports ClsSession.ClsSession
Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.ClsLocateInfo
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_ModOpe_otor
    Inherits System.Web.UI.Page

    Dim fc As New FuncionesGenerales.FComunes
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim cg As New ConsultasGenerales
    Dim modo As String
    Dim ag As New ActualizacionesGenerales
    Dim OP As New ClaseOperaciones
    Dim caption As String = "Operación"
    Dim msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Response.Expires = -1

            DropDownList1.ClearSelection()
            DP_Sucursal.ClearSelection()

            cg.ParametrosDevuelve(TablaParametro.CaracteristicaOperación, True, Me.DropDownList1)
            cg.SucursalesDevuelve(CodEje, True, DP_Sucursal)

            txt_itemope.Value = Val(Trim(Request.QueryString("itemOPE"))) + 1
            rb_res.SelectedValue = coll_ope_otg.Item(Val(Me.txt_itemope.Value)).ope_res_son
            DP_Sucursal.SelectedValue = coll_ope_otg.Item(Val(Me.txt_itemope.Value)).id_suc
            DropDownList1.SelectedValue = coll_ope_otg.Item(Val(Me.txt_itemope.Value)).id_P_0104

        End If

        Btn_Cerrar.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Public Sub ACTUALIZA_DATOS()

        Try

            Dim ope As New ope_cls
           
            If coll_ope_otg.Item(Val(Me.txt_itemope.Value)).ope_ptl = "SI" Then
                ope.ope_ptl = "S"
            Else
                ope.ope_ptl = "N"
            End If

            If coll_ope_otg.Item(Val(Me.txt_itemope.Value)).ope_lnl = "SI" Then
                ope.ope_lnl = "S"
            Else
                ope.ope_lnl = "N"
            End If

            ope.ope_res_son = Me.rb_res.SelectedValue
            ope.id_P_0104 = Me.DropDownList1.SelectedValue
            
            ope.id_ope = coll_ope_otg.Item(Val(Me.txt_itemope.Value)).ID_OPE
            ope.id_opn = coll_ope_otg.Item(Val(Me.txt_itemope.Value)).ID_OPN

            OP.OperacionModifica(ope, 2, DP_Sucursal.SelectedValue)

            Select Case OP.EstadoOperacion
                Case 1, 2
                    msj.Mensaje(Me, "Atención", OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
                Case 999
                    msj.Mensaje(Me, "Atención", "Error: " & OP.MensajeOperacion, ClsMensaje.TipoDeMensaje._Informacion)
            End Select


        Catch ex As Exception
            msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub





    Protected Sub btn_acepta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_acepta.Click

        Try

            If DropDownList1.SelectedValue = 0 Then
                msj.Mensaje(Me, caption, "Seleccione caracteristica de operación", ClsMensaje.TipoDeMensaje._Exclamacion, False)
                Exit Sub
            End If

            ACTUALIZA_DATOS()

        Catch ex As Exception
            msj.Mensaje(Page, caption, ex.Message, ClsMensaje.TipoDeMensaje._Error, , False)
        End Try

    End Sub



End Class
