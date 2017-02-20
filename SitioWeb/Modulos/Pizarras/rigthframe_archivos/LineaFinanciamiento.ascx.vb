Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_LineaFinanciamiento
    Inherits System.Web.UI.UserControl

    Dim FC As New FuncionesGenerales.FComunes
    Dim Msj As New ClsMensaje


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ScriptManager.GetCurrent(Page).RegisterPostBackControl(GV_Actas)

        If Not Page.IsPostBack Then
            CargaDrop()
            AlineaDerecha()
        Else

            If NroOperacion > 0 And NroNegociacion > 0 And RutCli > 0 Then
                CargaDatosLineaCredito()
            End If

        End If

    End Sub

    Private Sub AlineaDerecha()
        Txt_Mto_Sol.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Dis.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Apr.Attributes.Add("Style", "TEXT-ALIGN: right")
    End Sub

    Private Sub CargaDrop()

        Try
            Dim Drop As New ConsultasGenerales

            Drop.ParametrosDevuelve(TablaParametro.EstadoLinea, True, Me.DP_EstadoLinea)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CargaDatosLineaCredito()

        Try

            Dim CG As New ConsultasGenerales

            
            'CG.ActasDevuelvePorLDC(GV_Actas, CInt(GV_LineaCredito.Rows(0).Cells(0).Text))
            CargaDatos()


        Catch ex As Exception
            'Msj(ex.Message, TipoMsg.Errores)
        End Try

    End Sub

    Private Sub CargaDatos()

        Try


            Dim ClsLds As New ConsultasGenerales
            Dim sesion As New ClsSession.ClsSession
            Dim FMT As New FuncionesGenerales.ClsLocateInfo
            Dim LDC As ldc_cls

            
            LDC = LineaCredito

                'ClsLds.LineaDeCreditoDevuelvePorNro(sesion.RutCli, Txt_NroLinea.Text)
            If Not IsNothing(LDC) Then

                With LDC

                    Txt_NroLinea.Text = .id_ldc
                    Me.DP_EstadoLinea.ClearSelection()
                    Me.DP_EstadoLinea.Items.FindByValue(.id_P_0029).Selected = True
                    Me.Txt_Mto_Dis.Text = Format((.ldc_mto_apb - .ldc_mto_ocp), FMT.FCMSD)
                    Me.Txt_Fec_Sol.Text = .ldc_fec_sol
                    Me.Txt_Mto_Sol.Text = Format(.ldc_mto_sol, FMT.FCMSD)

                    Me.Txt_Fec_Dsd.Text = .ldc_fec_vig_dde
                    Me.Txt_Fec_Hta.Text = .ldc_fec_vig_hta
                    Me.Txt_Fec_Res.Text = .ldc_fec_rsn
                    Me.Txt_Mto_Apr.Text = Format(.ldc_mto_apb, FMT.FCMSD)

                    Me.Txt_Exc_Apr.Text = Format(.ldc_por_exc, FMT.FCMCD)

                    Select Case .ldc_tip_com
                        Case "N" : Me.RB_Normal.Checked = True : Me.RB_Especial.Checked = False : Me.Txt_Obs_Com.Text = .ldc_des_com
                        Case "E" : Me.RB_Especial.Checked = True : Me.RB_Normal.Checked = False : Me.Txt_Obs_Com.Text = ""
                    End Select

                    Me.Txt_Observacion.Text = .ldc_obs

                    ClsLds.ActasDevuelvePorLDC(GV_Actas, CInt(.id_ldc))
                    CargaDatosAnticipos()
                    CargaDatosSubLineas()

                End With

            End If
        Catch ex As Exception
            Msj.Mensaje(Page, "Actas Adjuntas", "Error " & ex.ToString, TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CargaDatosAnticipos()

        Try

            GV_PorcentajeAnt.DataSource = Anticipos
            GV_PorcentajeAnt.DataBind()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub CargaDatosSubLineas()

        Try

            Dim ClsSBL As New ConsultasGenerales
            Dim Fmt As New FuncionesGenerales.ClsLocateInfo

            GV_Deudor.DataSource = SubLineaDeudor
            GV_Deudor.DataBind()

            Dim I As Integer = 0

            For Each S In SubLineaDeudor
                'GV_Deudor.Rows(I).Cells(0).Text = Format(CLng(SubLineaDeudor.deu_ide), "###,###,###") & "-" & S.deu_dig
                GV_Deudor.Rows(I).Cells(0).Text = Format(CLng(GV_Deudor.Rows(I).Cells(0).Text), Fmt.FCMSD) & "-" & FC.Vrut(CLng(GV_Deudor.Rows(I).Cells(0).Text))
                I += 1
            Next

            GV_Productos.DataSource = SubLineaProducto
            GV_Productos.DataBind()

            'Si existe una Operacion Seleccionada, valida sus sub lineas y arroja una alerta de estas si son sobrepasadas
            If NroOperacion > 0 Then

                Dim Opn As opn_cls
                Opn = Negociacion

                'Valida Sub Linea Deudor
                If Not ClsSBL.SubLineaValida(1, RutCli, NroLineaCredito, NroOperacion) Then
                    LblLineaDeu.Text = "Cliente Presenta Sub-Lineas Sobregiradas"
                End If

                'Valida Sub Linea Tipo Docto
                If Not ClsSBL.SubLineaValida(2, RutCli, NroLineaCredito, NroOperacion, Opn.id_P_0031) Then
                    LblLineaPro.Text = "Cliente Presenta Sub-Lineas Sobregiradas"
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GV_Actas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Actas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onClick", "var x=window.showModalDialog('OpenActa.aspx?id=" & CType(e.Row.FindControl("IB_Ver"), ImageButton).ToolTip & "&File=" & FC.EliminarAcentos(e.Row.Cells(1).Text.Replace(" ", "_")) & "', window, 'scroll:no;status:off;dialogWidth:100;dialogHeight:100px;dialogLeft:50px;dialogTop:50px');")
        End If
    End Sub

    Protected Sub IB_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim IB As ImageButton = sender
        Dim ID As Integer = CInt(IB.ToolTip)
        Dim nombrearchivo As String = ""

        For I = 0 To GV_Actas.Rows.Count - 1
            If ID = CType(GV_Actas.Rows(I).FindControl("IB_Ver"), ImageButton).ToolTip Then
                nombrearchivo = GV_Actas.Rows(I).Cells(1).Text.Trim
                Exit For
            End If
        Next

        Dim CA As New ClaseArchivos
        Dim fc As New FuncionesGenerales.FComunes
        Dim archivo As Byte() = CA.DevuelveActa(ID)
        Dim Extension As String = fc.ExtraerExtension(nombrearchivo, ".")
        Dim _ctype As String = ""

        If archivo.Length <> 0 Then

            Dim path2 As String = "Acta_" & ID.ToString
            Select Case Extension
                Case "pdf" : _ctype = "application/pdf"
                Case "exe" : _ctype = "application/octet-stream"
                Case "zip" : _ctype = "application/zip"
                Case "doc" : _ctype = "application/msword"
                Case "xls" : _ctype = "application/vnd.ms-excel"
                Case "ppt" : _ctype = "application/vnd.ms-powerpoint"
                Case "gif" : _ctype = "image/gif"
                Case "png" : _ctype = "image/png"
                Case "jpeg", "jpg" : _ctype = "image/jpg"
                Case Else : _ctype = "application/force-download"
            End Select

            Response.Buffer = False
            Response.Expires = -1
            Response.AddHeader("Content-Type", _ctype)
            Response.AddHeader("Content-Length", archivo.Length.ToString)
            Response.AddHeader("Content-Disposition", "attachment;filename=" & nombrearchivo & "")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.BinaryWrite(archivo)
            Response.End()
        End If

    End Sub

End Class
