Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Servicios_Operaciones
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim sesion As New ClsSession.ClsSession
    Dim ses_ope As New ClsSession.SesionOperaciones
    Dim OP As New ClaseOperaciones
    Dim Caption As String = "Operaciones"
    Dim CG As New ConsultasGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1
            Response.Cache.SetNoStore()

            Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
            Txt_Rut_Cli.Focus()

        Else

            If Txt_Rut_Cli.Text = "" Then

                If Not IsNothing(Session("Cliente")) Then

                    Dim cli As cli_cls

                    cli = Session("Cliente")

                    Txt_Rut_Cli.Text = CDbl(cli.cli_idc)
                    Txt_Dig_Cli.Text = RG.Vrut(CDbl(cli.cli_idc))

                    If CargaDatosCliente() Then
                        IB_Informe.Enabled = True
                        IB_Buscar.Enabled = False
                    End If

                End If

            End If

        End If

        IB_Volver.Attributes.Add("onClick", "javascript:window.close();")

        'IB_Nuevo.Attributes.Add("onClick", "Negociación('PopUpNegociacion.aspx', 1280, 1024, 0, 0);")
        'IB_Detalle.Attributes.Add("onClick", "DetalleNegociacion('PopUpNegociacion.aspx', 1280, 1024, 0, 0);")

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged

        Try

            If CargaDatosCliente() Then
                Txt_Fecha_Dsd.Focus()
            Else
                Txt_Rut_Cli.Focus()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function CargaDatosCliente() As Boolean

        Dim Sesion As New ClsSession.ClsSession
        Dim ClsCli As New ClaseClientes
        Dim CLI As cli_cls

        Try

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Function
            End If



            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            HabilitaDesabilitaCliente(False)

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
            Return False
        End Try

    End Function

    Private Sub HabilitaDesabilitaCliente(ByVal Estado As Boolean)

        Txt_Rut_Cli.ReadOnly = Not Estado
        Txt_Dig_Cli.ReadOnly = Not Estado

        If Not Estado Then
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
        Else
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
        End If


    End Sub

    Public Sub CargaGrillas()

        Try

            Dim rut_cli As Integer, rut_cli1 As Int64, _
                SUC1 As Integer, suc2 As Integer, _
                eje1 As String, eje2 As String, _
                nro_otg1 As Integer, nro_otg2 As Int64, _
                mon1 As String, mon2 As String, _
                estado As String, _
                nro_doc1 As String, nro_doc2 As String, _
                fec_des As Date, fec_has As Date, _
                tipo, _
                rut_deu As Long, rut_deu1 As Int64, _
                fec_vcto1 As Date, fec_vcto2 As Date, _
                fog As String, _
                con_resp1 As String, con_resp2 As String

            Dim jdx As Integer
            Dim indice As Integer



            rut_cli = Me.Txt_Rut_Cli.Text
            rut_cli1 = Me.Txt_Rut_Cli.Text

            rut_deu = 0
            rut_deu1 = 9999999999999



            SUC1 = 0 'Sucursal
            suc2 = 999 'Sucursal

            eje1 = 0 'CodEje
            eje2 = 999 'CodEje

            mon1 = 0
            mon2 = 9999

            fec_des = CDate(Txt_Fecha_Dsd.Text)
            fec_has = CDate(Txt_Fecha_Hst.Text)

            'tipo = 1
            tipo = 2

            indice = 6
            IDX = 1

            nro_otg1 = 0
            nro_otg2 = 999999999

            nro_doc1 = "0"
            nro_doc2 = 999999999

            fec_vcto1 = CDate("01/01/1900")
            fec_vcto2 = CDate("31/12/9999")

            con_resp1 = 1
            con_resp2 = 2

            fog = ""


            'INGRESADAS
            '    page_dig = 0
            hf_nro_pag_dig.Value = 1
            lb_pag_dig.Text = "Página " & hf_nro_pag_dig.Value & ""

            ses_ope.coll_ope = OP.Operaciones_varios_criterios_Devuelve(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 1, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, False, Nothing)
            Me.gr_dig.DataSource = ses_ope.coll_ope
            Me.gr_dig.DataBind()



            'SIMULADAS 
            ' page_sim = 0
            hf_nro_pag_sim.Value = 1
            lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""

            ses_ope.coll_ope_sim = OP.Operaciones_varios_criterios_Devuelve(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 2, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, False, Nothing)

            Me.gr_sim.DataSource = ses_ope.coll_ope_sim
            Me.gr_sim.DataBind()




            'OTORGADAS
            '   page_otg = 0

            hf_can_pag.Value = 1
            Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""
            ses_ope.coll_ope_otg = OP.Operaciones_varios_criterios_Devuelve(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 3, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, False, Nothing)

            Me.gr_otg.DataSource = ses_ope.coll_ope_otg
            Me.gr_otg.DataBind()



            ' ANULADAS

            '      page_anu = 0

            sesion.coll_ope_anu = OP.Operaciones_varios_criterios_Devuelve(rut_cli, rut_cli1, fec_des, fec_has, fec_vcto1, fec_vcto2, SUC1, suc2, nro_otg1, nro_otg2, 4, nro_doc1, nro_doc2, mon1, mon2, con_resp1, con_resp2, rut_deu, rut_deu1, False, Nothing)

            Me.gr_anul.DataSource = sesion.coll_ope_anu
            Me.gr_anul.DataBind()

            For I = 0 To Me.gr_dig.Rows.Count - 1

                Me.gr_dig.Rows(I).Cells(1).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(1).Text), FMT.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_dig.Rows(I).Cells(1).Text))
                If coll_ope.Item(I + 1).id_p_0023 = 1 Then
                    Me.gr_dig.Rows(I).Cells(7).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(7).Text), FMT.FCMSD)
                    Me.gr_dig.Rows(I).Cells(8).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(8).Text), FMT.FCMSD)
                    Me.gr_dig.Rows(I).Cells(9).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(9).Text), FMT.FCMSD)

                ElseIf coll_ope.Item(I + 1).id_p_0023 > 2 Then
                    Me.gr_dig.Rows(I).Cells(7).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(7).Text), FMT.FCMCD)
                    Me.gr_dig.Rows(I).Cells(8).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(8).Text), FMT.FCMCD)
                    Me.gr_dig.Rows(I).Cells(9).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(9).Text), FMT.FCMCD)

                ElseIf coll_ope.Item(I + 1).id_p_0023 = 2 Then

                    Me.gr_dig.Rows(I).Cells(7).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(7).Text), FMT.FCMCD4)
                    Me.gr_dig.Rows(I).Cells(8).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(8).Text), FMT.FCMCD4)
                    Me.gr_dig.Rows(I).Cells(9).Text = Format(CLng(Me.gr_dig.Rows(I).Cells(9).Text), FMT.FCMCD4)


                End If
            Next


            For I = 0 To Me.gr_sim.Rows.Count - 1

                Me.gr_sim.Rows(I).Cells(1).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(1).Text), FMT.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_sim.Rows(I).Cells(1).Text))
                If coll_ope_sim.Item(I + 1).id_p_0023 = 1 Then
                    Me.gr_sim.Rows(I).Cells(7).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(7).Text), FMT.FCMSD)
                    Me.gr_sim.Rows(I).Cells(8).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(8).Text), FMT.FCMSD)
                    Me.gr_sim.Rows(I).Cells(9).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(9).Text), FMT.FCMSD)

                ElseIf coll_ope_sim.Item(I + 1).id_p_0023 > 2 Then
                    Me.gr_sim.Rows(I).Cells(7).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(7).Text), FMT.FCMCD)
                    Me.gr_sim.Rows(I).Cells(8).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(8).Text), FMT.FCMCD)
                    Me.gr_sim.Rows(I).Cells(9).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(9).Text), FMT.FCMCD)

                ElseIf coll_ope_sim.Item(I + 1).id_p_0023 = 2 Then

                    Me.gr_sim.Rows(I).Cells(7).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(7).Text), FMT.FCMCD4)
                    Me.gr_sim.Rows(I).Cells(8).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(8).Text), FMT.FCMCD4)
                    Me.gr_sim.Rows(I).Cells(9).Text = Format(CLng(Me.gr_sim.Rows(I).Cells(9).Text), FMT.FCMCD4)


                End If
            Next

            For I = 0 To Me.gr_otg.Rows.Count - 1

                Me.gr_otg.Rows(I).Cells(2).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(2).Text), FMT.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_otg.Rows(I).Cells(2).Text))
                If coll_ope_otg.Item(I + 1).id_p_0023 = 1 Then
                    Me.gr_otg.Rows(I).Cells(8).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(8).Text), FMT.FCMSD)
                    Me.gr_otg.Rows(I).Cells(9).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(9).Text), FMT.FCMSD)
                    Me.gr_otg.Rows(I).Cells(10).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(10).Text), FMT.FCMSD)

                ElseIf coll_ope_otg.Item(I + 1).id_p_0023 > 2 Then
                    Me.gr_otg.Rows(I).Cells(8).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(8).Text), FMT.FCMCD)
                    Me.gr_otg.Rows(I).Cells(9).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(9).Text), FMT.FCMCD)
                    Me.gr_otg.Rows(I).Cells(10).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(10).Text), FMT.FCMCD)

                ElseIf coll_ope_otg.Item(I + 1).id_p_0023 = 2 Then
                    Me.gr_otg.Rows(I).Cells(8).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(8).Text), FMT.FCMCD4)
                    Me.gr_otg.Rows(I).Cells(9).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(9).Text), FMT.FCMCD4)
                    Me.gr_otg.Rows(I).Cells(10).Text = Format(CLng(Me.gr_otg.Rows(I).Cells(10).Text), FMT.FCMCD4)


                End If
            Next

            For I = 0 To Me.gr_anul.Rows.Count - 1

                Me.gr_anul.Rows(I).Cells(2).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(2).Text), FMT.FCMSD) & "-" & RG.Vrut(CLng(Me.gr_anul.Rows(I).Cells(2).Text))
                If coll_ope_anu.Item(I + 1).id_p_0023 = 1 Then
                    Me.gr_anul.Rows(I).Cells(7).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(7).Text), FMT.FCMSD)
                    Me.gr_anul.Rows(I).Cells(8).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(8).Text), FMT.FCMSD)
                    Me.gr_anul.Rows(I).Cells(9).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(9).Text), FMT.FCMSD)

                ElseIf coll_ope_anu.Item(I + 1).id_p_0023 > 2 Then
                    Me.gr_anul.Rows(I).Cells(7).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(7).Text), FMT.FCMCD)
                    Me.gr_anul.Rows(I).Cells(8).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(8).Text), FMT.FCMCD)
                    Me.gr_anul.Rows(I).Cells(9).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(9).Text), FMT.FCMCD)

                ElseIf coll_ope_anu.Item(I + 1).id_p_0023 = 2 Then

                    Me.gr_anul.Rows(I).Cells(7).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(7).Text), FMT.FCMCD4)
                    Me.gr_anul.Rows(I).Cells(8).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(8).Text), FMT.FCMCD4)
                    Me.gr_anul.Rows(I).Cells(9).Text = Format(CLng(Me.gr_anul.Rows(I).Cells(9).Text), FMT.FCMCD4)


                End If
            Next

            If Me.gr_dig.Rows.Count = 0 And gr_sim.Rows.Count = 0 And gr_anul.Rows.Count = 0 And gr_otg.Rows.Count = 0 And gr_pag.Rows.Count = 0 Then

                Msj.Mensaje(Me, "Atención", "No se encontraron Operaciones segun los criterios proporcionados", 2)
            End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub gr_dig_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_dig.RowDataBound
        Dim strLineAction As String
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'Genera String de Acción por Fila de Grilla

            strLineAction = "TraspasoCollCamposSvc(TabGrillas_TabPanel1_gr_dig, "
            strLineAction &= "1, "
            'strLineAction &= "'" & GrIdx & "',"
            strLineAction &= "'clicktable', 'formatable', 'selectable')"

            e.Row.Attributes.Add("onClick", strLineAction)

        End If
    End Sub

    Protected Sub gr_sim_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_sim.RowDataBound
        Dim strLineAction As String
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'Genera String de Acción por Fila de Grilla

            strLineAction = "TraspasoCollCamposSvc(TabGrillas_TabPanel2_gr_sim, "
            strLineAction &= "2, "
            'strLineAction &= "'" & GrIdx & "',"
            strLineAction &= "'clicktable', 'formatable', 'selectable')"

            e.Row.Attributes.Add("onClick", strLineAction)
        End If
    End Sub

    Protected Sub gr_otg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_otg.RowDataBound
        Dim strLineAction As String
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'Genera String de Acción por Fila de Grilla

            strLineAction = "TraspasoCollCamposSvc(TabGrillas_TabPanel3_gr_otg, "
            strLineAction &= "3, "
            'strLineAction &= "'" & GrIdx & "',"
            strLineAction &= "'clicktable', 'formatable', 'selectable')"

            e.Row.Attributes.Add("onClick", strLineAction)

        End If
    End Sub

    Protected Sub gr_pag_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_pag.RowDataBound
        Dim strLineAction As String
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'Genera String de Acción por Fila de Grilla
            strLineAction = "TraspasoCollCamposSvc(TabGrillas_TabPanel4_gr_pag, "
            strLineAction &= "4, "
            'strLineAction &= "'" & GrIdx & "',"
            strLineAction &= "'clicktable', 'formatable', 'selectable')"

            e.Row.Attributes.Add("onClick", strLineAction)

        End If
    End Sub

    Protected Sub gr_anul_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gr_anul.RowDataBound
        Dim strLineAction As String
        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            e.Row.Attributes.Add("onMouseOver", "J_RolClass('selectable')")
            e.Row.Attributes.Add("onMouseOut", "J_RolClass('formatable')")

            'Genera String de Acción por Fila de Grilla

            strLineAction = "TraspasoCollCamposSvc(TabGrillas_TabPanel5_gr_anul, "
            strLineAction &= "5, "
            'strLineAction &= "'" & GrIdx & "',"
            strLineAction &= "'clicktable', 'formatable', 'selectable')"

            e.Row.Attributes.Add("onClick", strLineAction)

        End If
    End Sub



#End Region

#Region "Botonera"

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        CargaGrillas()
        IB_Informe.Enabled = True
    End Sub

    Protected Sub IB_Informe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Informe.Click

        Try

            If Txt_ItemOPE.Value = "" Then
                Msj.Mensaje(Me, "Servicio de Operaciones", "Debe selecionar una operación ", ClsMensaje.TipoDeMensaje._Exclamacion, Nothing, False)
                Exit Sub
            End If


            Dim rut_cli As String, rut_cli1 As String, _
                cli_rso As String, deu_rso As String, _
                SUC1 As Integer, suc2 As Integer, _
                eje1 As String, eje2 As String, _
                nro_otg1 As Integer, nro_otg2 As Int64, _
                mon1 As Integer, mon2 As Integer, _
                estado As Integer, estado2 As Integer, _
                nro_doc1 As String, nro_doc2 As String, _
                fec_des As Date, fec_has As Date, _
                rut_deu As String, rut_deu1 As String, _
                fec_vcto1 As Date, fec_vcto2 As Date


            Dim Var As New FuncionesGenerales.Variables

            rut_cli = Format(CLng(Me.Txt_Rut_Cli.Text), Var.FMT_RUT)
            rut_cli1 = Format(CLng(Me.Txt_Rut_Cli.Text), Var.FMT_RUT)
            cli_rso = Me.Txt_Raz_Soc.Text

            rut_deu = "000000000000"
            rut_deu1 = "9999999999999"
            deu_rso = ""

            SUC1 = 0
            suc2 = 999

            eje1 = 0
            eje2 = 9999

            mon1 = 0
            mon2 = 9999

            fec_des = CDate("01/01/1900")
            fec_has = CDate("31/12/9999")

            estado = 0
            estado2 = 9999999

            nro_otg1 = 0
            nro_otg2 = 999999999

            nro_doc1 = "0"
            nro_doc2 = 999999999

            fec_vcto1 = CDate("01/01/1900")
            fec_vcto2 = CDate("31/12/9999")

            'sesion.RutCli = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value)).CLI_IDC
            'sesion.NroOperacion = ses_ope.coll_ope_otg.Item(Val(Me.pos_otg.Value)).ID_OPE

            Select Case TabGrillas.ActiveTabIndex

                Case 0 'INGRESADAS

                    ses_ope.ID_OPE_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Value)).ID_OPE
                    ses_ope.RUT_CLI_RPT = ses_ope.coll_ope.Item(Val(Me.Txt_ItemOPE.Value)).CLI_IDC

                Case 1 'SIMULADAS

                    ses_ope.ID_OPE_RPT = ses_ope.coll_ope_sim.Item(Val(Me.Txt_ItemOPE.Value)).ID_OPE
                    ses_ope.RUT_CLI_RPT = ses_ope.coll_ope_sim.Item(Val(Me.Txt_ItemOPE.Value)).CLI_IDC

                    RW.AbrePopup(Me, 2, "../Operaciones/rightframe_archivos/reporte_sim.aspx", "Informes", 1000, 900, 100, 0)

                Case 2 'OTORGADAS

                    NroOperacion = ses_ope.coll_ope_otg.Item(Val(Me.Txt_ItemOPE.Value)).ID_OPE
                    RutCli = ses_ope.coll_ope_otg.Item(Val(Me.Txt_ItemOPE.Value)).CLI_IDC

                    RW.AbrePopup(Me, 2, "../Pizarras/Reportes/report_otg.aspx", "RepOtor", 1280, 1024, 0, 0)

                Case 3 'ANULADAS

                Case 4 'PAGADAS

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click

    End Sub

    Protected Sub IB_Limpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Limpiar.Click

        Try

            'Limpiamos TextBox
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Fecha_Dsd.Text = ""

            gr_dig.DataSource = Nothing
            gr_dig.DataBind()

            gr_sim.DataSource = Nothing
            gr_sim.DataBind()

            gr_otg.DataSource = Nothing
            gr_otg.DataBind()

            gr_anul.DataSource = Nothing
            gr_anul.DataBind()

            gr_pag.DataSource = Nothing
            gr_pag.DataBind()

            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False

            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            Session("Cliente") = Nothing

            Txt_Rut_Cli.Focus()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_next_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_next_otg.Click

        Dim val As Integer

        Select Case Me.TabGrillas.ActiveTabIndex

            Case 0

                If Me.gr_dig.Rows.Count < 12 Then
                    Msj.Mensaje(Me, "Atención", "Ya está en la ultima página de la lista", 2)
                    Exit Sub
                End If
                page_dig = page_dig + 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 1
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

            Case 1

                If Me.gr_sim.Rows.Count < 12 Then
                    Msj.Mensaje(Me, "Atención", "Ya está en la ultima página de la lista", 2)
                    Exit Sub
                End If

                page_sim = page_sim + 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 2
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

                hf_nro_pag_sim.Value = hf_nro_pag_sim.Value - 1
                lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""

            Case 2

                If Me.gr_otg.Rows.Count < 12 Then
                    Msj.Mensaje(Me, "Atención", "Ya está en la ultima página de la lista", 2)
                    Exit Sub
                End If

                page_otg = page_otg + 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 3
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

                hf_can_pag.Value = hf_can_pag.Value + 1
                Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""

            Case 3

                page_anu = page_anu + 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 4
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

            Case 4

                page_pag = page_pag + 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 5
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val
        End Select

    End Sub

    Protected Sub btn_prev_otg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_prev_otg.Click

        Dim val As Integer

        Select Case Me.TabGrillas.ActiveTabIndex
            Case 0

                If page_dig = 0 Then

                    Msj.Mensaje(Me, "Atención", "Ya has llegado al comienzo de la lista", 2)
                    Exit Sub

                End If
                page_dig = page_dig - 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 1
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

                hf_nro_pag_dig.Value = hf_nro_pag_dig.Value - 1
                lb_pag_dig.Text = "Página " & hf_nro_pag_dig.Value & ""



            Case 1

                If page_sim = 0 Then

                    Msj.Mensaje(Me, "Atención", "Ya has llegado al comienzo de la lista", 2)
                    Exit Sub

                End If
                page_sim = page_sim - 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 2
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

                hf_nro_pag_sim.Value = hf_nro_pag_sim.Value - 1
                lb_pag_sim.Text = "Página " & hf_nro_pag_sim.Value & ""

            Case 2



                If page_otg = 0 Then

                    Msj.Mensaje(Me, "Atención", "Ya has llegado al comienzo de la lista", 2)
                    Exit Sub

                End If


                page_otg = page_otg - 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 3
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

                hf_can_pag.Value = hf_can_pag.Value - 1
                Lbl_pg_otg.Text = "Página " & hf_can_pag.Value & ""

            Case 3

                page_anu = page_anu - 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 4
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val

            Case 4

                page_pag = page_pag - 12
                'val = Me.dr_estado.SelectedValue
                'Me.dr_estado.SelectedValue = 5
                CargaGrillas()
                'Me.dr_estado.SelectedValue = val


        End Select

    End Sub

#End Region

End Class
