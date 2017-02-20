Imports FuncionesGenerales.FComunes
Imports FuncionesGenerales.Variables
Imports CapaDatos
Partial Class Modulos_Servicios_gastosdefinidos
    Inherits System.Web.UI.UserControl

    'Dim ope As New FacWebCiti.ClsOperacion.ClsOPE
    Dim fc As New FuncionesGenerales.FComunes
    Dim CG As New ConsultasGenerales
    Dim CMC As New ClaseComercial
    Dim gasdef As New ArrayList

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'gasto = ope.retornagastos("001")
            Me.gd_gastdef.DataSource = CMC.GastosDefinidosDevuelve(1)
            Me.gd_gastdef.DataBind()
            gasdef.Clear()
        End If
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        llenaarray()
    End Sub

    Protected Sub llenaarray()

        Dim i As Integer
        Dim ch As New CheckBox

        For i = 0 To Me.gd_gastdef.Rows.Count - 1
            ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

            If ch.Checked = True Then
                If gasdef.Contains(gd_gastdef.Rows(i).Cells(1).Text) Then

                Else
                    gasdef.Add(Me.gd_gastdef.Rows(i).Cells(1).Text)
                    Me.txt_total.Text = CDbl(Val(Me.txt_total.Text)) + CDbl(Me.gd_gastdef.Rows(i).Cells(3).Text)
                    'totalgastodef = Me.txt_total.Text
                End If


            Else
                If gasdef.Contains(Me.gd_gastdef.Rows(i).Cells(1).Text) Then
                    gasdef.Remove(Me.gd_gastdef.Rows(i).Cells(1).Text)
                    Me.txt_total.Text = CDbl(Val(Me.txt_total.Text)) - CDbl(Me.gd_gastdef.Rows(i).Cells(3).Text)
                    'totalgastodef = Me.txt_total.Text
                End If

            End If
        Next

    End Sub

    Public Sub llenaarreglos()
        Dim gastosdefinidos As New FuncionesGenerales.GASTOSDEFINIDOS
        Dim ind As Integer
        Dim coll_docs As New Collection


        'Set RUTINAS = CreateObject("RUTINAS_GENERALES.RUTINAS")
        'NRO_GASTO = RUTINAS.RETORNA_NUMERO_SISTEMA("NRO GASTO", TOTAL_REGISTROS)
        'Set RUTINAS = Nothing

        For ind = 0 To gd_gastdef.Rows.Count - 1

            'If gasdef.Contains(gd_gastdef.Rows(ind).Cells(1).Text) Then


            '    coll_docs = ope.dev_doctos_gastos(RuT_CLIENTE, ope_num)


            '    If Me.gd_gastdef.Rows(ind).Cells(2).Text = "POR DOCUMENTO" Then



            '        gastosdefinidos.gto_cli = Format(CLng(RuT_CLIENTE), FMT_RUT)
            '        gastosdefinidos.gto_ddr = Format(CLng(coll_docs.Item(1)), FMT_RUT)
            '        gastosdefinidos.gto_ope = ope_num
            '        gastosdefinidos.gto_tdo = coll_docs.Item(2)
            '        gastosdefinidos.gto_ndo = coll_docs.Item(3)
            '        gastosdefinidos.gto_cfl = coll_docs.Item(4)
            '        gastosdefinidos.gto_fln = coll_docs.Item(5)
            '        gastosdefinidos.gto_cod = Me.gd_gastdef.Rows(ind - 1).Cells(1).Text
            '        gastosdefinidos.gto_num = fc.RETORNA_NUMERO_SISTEMA("NRO GASTO", 1)
            '        gastosdefinidos.gto_tip = 3
            '        gastosdefinidos.gto_mto = Me.gd_gastdef.Rows(ind - 1).Cells(3).Text
            '        gastosdefinidos.gto_des = Me.gd_gastdef.Rows(ind - 1).Cells(4).Text
            '        gastosdefinidos.gto_tot = 0


            '        coll_gastosdefinidos.Add(gastosdefinidos)

            '    ElseIf Me.gd_gastdef.Rows(ind).Cells(2).Text = "POR DEUDOR" Then


            '        gastosdefinidos.gto_cli = Format(CLng(RuT_CLIENTE), FMT_RUT)
            '        gastosdefinidos.gto_ddr = Format(CLng(coll_docs.Item(1)), FMT_RUT)
            '        gastosdefinidos.gto_ope = ope_num
            '        gastosdefinidos.gto_tdo = 0
            '        gastosdefinidos.gto_ndo = 0
            '        gastosdefinidos.gto_cfl = 0
            '        gastosdefinidos.gto_fln = 0
            '        gastosdefinidos.gto_cod = Me.gd_gastdef.Rows(ind - 1).Cells(1).Text
            '        gastosdefinidos.gto_num = fc.RETORNA_NUMERO_SISTEMA("NRO GASTO", 1)
            '        gastosdefinidos.gto_tip = 2
            '        gastosdefinidos.gto_mto = Me.gd_gastdef.Rows(ind - 1).Cells(3).Text
            '        gastosdefinidos.gto_des = Me.gd_gastdef.Rows(ind - 1).Cells(4).Text
            '        gastosdefinidos.gto_tot = 0

            '        coll_gastosdefinidos.Add(gastosdefinidos)
            '    ElseIf Me.gd_gastdef.Rows(ind).Cells(2).Text = "POR OPERACION" Then


            '        gastosdefinidos.gto_cli = Format(CLng(RuT_CLIENTE), FMT_RUT)
            '        gastosdefinidos.gto_ddr = Format(CLng(0), "000000000000")
            '        gastosdefinidos.gto_ope = ope_num
            '        gastosdefinidos.gto_tdo = 0
            '        gastosdefinidos.gto_ndo = 0
            '        gastosdefinidos.gto_cfl = 0
            '        gastosdefinidos.gto_fln = 0
            '        gastosdefinidos.gto_cod = 0
            '        gastosdefinidos.gto_num = fc.RETORNA_NUMERO_SISTEMA("NRO GASTO", 1)
            '        gastosdefinidos.gto_tip = 1
            '        gastosdefinidos.gto_mto = Me.gd_gastdef.Rows(ind - 1).Cells(1).Text
            '        gastosdefinidos.gto_des = Me.gd_gastdef.Rows(ind - 1).Cells(3).Text
            '        gastosdefinidos.gto_tot = 0


            '        coll_gastosdefinidos.Add(gastosdefinidos)
            '    End If


            'End If


        Next


    End Sub

End Class
