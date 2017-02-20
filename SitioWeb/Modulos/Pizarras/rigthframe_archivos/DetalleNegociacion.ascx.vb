Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_DetalleNegociacion
    Inherits System.Web.UI.UserControl

    Private CG As New ConsultasGenerales
    Private FMT As New FuncionesGenerales.ClsLocateInfo


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then
            If NroOperacion > 0 And NroNegociacion > 0 And RutCli > 0 Then
                TraeDatosNegociacion()
                'TraeDatosSimulacion()
            End If

        Else
            AlineaDerecha()
        End If

    End Sub

    Private Sub AlineaDerecha()

        Txt_Tas_Bas.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Spread.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Puntos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tas_Neg.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Por_Com.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Minimo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Maximo.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Com_Flat.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Por_Ant.Attributes.Add("Style", "TEXT-ALIGN: right")


        'Txt_Mto_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Mto_Ant.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Dif_Pre.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Pre_Com.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Sal_Pen.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Sal_Pag.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Txt_Com_Por_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Com_Esp.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Iva.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Gastos.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Impuestos.Attributes.Add("Style", "TEXT-ALIGN: right")
        'Txt_Descuentos.Attributes.Add("Style", "TEXT-ALIGN: right")

        'Txt_Tot_Gir.Attributes.Add("Style", "TEXT-ALIGN: right")

    End Sub

    Private Sub TraeDatosNegociacion()

        Try

            Dim NEG As opn_cls

            If Not IsNothing(Session("Negociación")) Then
                NEG = Session("Negociación")
            Else
                Exit Sub
            End If

            With NEG

                Txt_Tas_Bas.Text = Format(.opn_tas_bas, FMT.FSMCD)
                Txt_Spread.Text = Format(.opn_spr_ead, FMT.FSMCD)
                Txt_Puntos.Text = Format(.opn_pto_spr, FMT.FSMCD)
                Txt_Tas_Neg.Text = Format(.opn_tas_neg, FMT.FSMCD)
                Txt_Tip_Ope.Text = .P_0012_cls.pnu_des

                If IsNothing(.P_0023_cls) Then
                    Txt_Moneda.Text = ""
                Else
                    Txt_Moneda.Text = .P_0023_cls.pnu_des
                End If

                Txt_Por_Com.Text = Format(.opn_por_com, FMT.FSMCD)
                Txt_Minimo.Text = Format(.opn_com_min, FMT.FCMSD)
                Txt_Maximo.Text = Format(.opn_com_max, FMT.FCMSD)

                If IsNothing(.P_0023_cls) Then
                    Txt_Mon_Flat.Text = ""
                Else
                    Txt_Mon_Flat.Text = .P_0023_cls.pnu_des
                End If

                Txt_Com_Flat.Text = Format(.opn_com_fla, FMT.FCMSD)
                Txt_Por_Ant.Text = Format(.opn_por_ant, FMT.FSMCD)

                If IsNothing(.P_0056_cls) Then
                    Txt_For_Pag.Text = ""
                Else
                    Txt_For_Pag.Text = .P_0056_cls.pnu_des
                End If

                If IsNothing(.nbc_cls) Then
                    Txt_Banco.Text = ""
                Else
                    Txt_Banco.Text = .nbc_cls.sbc_cls.bco_cls.bco_des
                End If

                Txt_Cta_Cte.Text = .opn_cta_cte

                If Trim(.opn_ant_014) = "S" Then
                    CB_Ant14.Checked = True
                Else
                    CB_Ant14.Checked = False
                End If

                Txt_Comentario.Text = .opn_com_neg
                Txt_Instrucciones.Text = .opn_ins_neg


            End With

        Catch ex As Exception

        End Try
    End Sub

End Class
