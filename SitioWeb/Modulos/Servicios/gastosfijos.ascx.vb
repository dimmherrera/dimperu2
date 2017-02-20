Imports FuncionesGenerales.gastofijo
Imports FuncionesGenerales.Variables
Imports CapaDatos
Imports FuncionesGenerales.FComunes
Partial Class Modulos_Servicios_gastosfijos
    Inherits System.Web.UI.UserControl

    Dim fc As New FuncionesGenerales.FComunes
    Dim var As New FuncionesGenerales.Variables

    Protected Sub btn_guar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guar.Click

        Dim obj As New FuncionesGenerales.gastofijo
        obj.monto = fc.comasXptos(Me.txt_mto.Text)
        obj.des = Me.txt_des.Text
        'gastofijo.Add(obj)
        'Me.txt_totales.Text = CDbl(Val(Me.txt_totales.Text)) + comasXptos(Me.txt_mto.Text)
        'totalgastofijo = Val(Me.txt_totales.Text)
        'Me.gr_gastofijo.DataSource = gastofijo
        Me.gr_gastofijo.DataBind()
        Me.txt_des.Text = ""
        Me.txt_mto.Text = ""



    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_gastofijo.RowDataBound
        Dim strlineaction As String
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            strlineaction = "MarcaGastos(TabContainer1_fijos_Gastosfijos1_gr_gastofijo, "
            strLineAction &= "'clicktable', 'formatable', 'selectable')"
            e.Row.Attributes.Add("onClick", strLineAction)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'gastofijo.Clear()
        End If
    End Sub

    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click
        Dim i As Integer
        'gastofijo.Remove(CInt(Me.Txt_Itemgas.Value))
        Me.txt_totales.Text = ""
        'For i = 1 To gastofijo.Count
        'Me.txt_totales.Text = Val(txt_totales.Text) + gastofijo(i).Item(0)
        'Next
        'totalgastofijo = Val(Me.txt_totales.Text)
        'Me.gr_gastofijo.DataSource = gastofijo
        Me.gr_gastofijo.DataBind()
    End Sub

    Public Sub llenagastosfijos()

        Dim index As Integer
        'For index = 1 To gastofijo.Count
        '    gastosfijos.gto_cli = Format(CLng(RuT_CLIENTE), FMT_RUT)
        '    gastosfijos.gto_ddr = Format(CLng(0), FMT_RUT)
        '    gastosfijos.gto_ope = ope_num
        '    gastosfijos.gto_tdo = 0
        '    gastosfijos.gto_ndo = 0
        '    gastosfijos.gto_cfl = 0
        '    gastosfijos.gto_fln = 0
        '    gastosfijos.gto_cod = 0
        '    gastosfijos.gto_num = fc.RETORNA_NUMERO_SISTEMA("NRO GASTO", 1)
        '    gastosfijos.gto_tip = 0
        '    gastosfijos.gto_mto = gastofijo.Item(index).MonTO
        '    gastosfijos.gto_des = gastofijo.Item(index).DES
        '    gastosfijos.gto_tot = 0

        '    coll_gastosfijos.Add(gastosfijos)
        'Next


    End Sub
End Class
