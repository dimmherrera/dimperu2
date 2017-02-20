Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class DetalleOperacion
    Inherits System.Web.UI.UserControl

    Private CG As New ConsultasGenerales
    Private FMT As New FuncionesGenerales.ClsLocateInfo
    Private RG As New FuncionesGenerales.FComunes
    Dim OP As New ClaseOperaciones
    Dim VAR As New FuncionesGenerales.Variables
    Dim CBZ As New ClaseCobranza
    Private Caption As String = "Pizarra Aprobaciones"
    Private msj As New ClsMensaje

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then
            Response.Expires = -1

            NroPaginacion_DetalleOpe = 0
          
            If NroOperacion > 0 And RutCli > 0 Then

                CargaDetalleOpe()


                If Gr_Documentos.Rows.Count < 0 Then
                    IB_Next.Enabled = False
                    IB_Prev.Enabled = False
                Else
                    IB_Next.Enabled = True
                    IB_Prev.Enabled = True
                End If
           
                Txt_Verificacion.BackColor = Drawing.Color.RosyBrown
                
            End If

        
        End If

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click

        If NroPaginacion_DetalleOpe = 0 Then
            msj.Mensaje(Page, Caption, "Ha llegado al comienzo de la lista", 2)
            Exit Sub
        End If

        If NroPaginacion_DetalleOpe >= 18 Then
            NroPaginacion_DetalleOpe -= 18
            CargaDetalleOpe()
        End If
    End Sub

    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click

        If Gr_Documentos.Rows.Count < 18 Then
            msj.Mensaje(Page, Caption, "Ya está en la última página de la lista", 2)
            Exit Sub
        End If

        If Gr_Documentos.Rows.Count = 18 Then
            NroPaginacion_DetalleOpe += 18
            CargaDetalleOpe()
        End If
    End Sub


    Private Sub CargaDetalleOpe()

        Try

            Dim Txt As TextBox

            Coll_DSI = New Collection

            Coll_DSI = OP.documentosIngresados_Retorna(NroOperacion, NroOperacion)

            Me.Gr_Documentos.DataSource = Coll_DSI
            Me.Gr_Documentos.DataBind()


            If Not IsNothing(Negociacion) Then

                Dim NEG As opn_cls

                NEG = Negociacion

                For I = 0 To Gr_Documentos.Rows.Count - 1

                    Dim rut As Long
                    Dim Formato As String

                    Rut = RG.LimpiaRut(Gr_Documentos.Rows(I).Cells(1).Text)
                    Gr_Documentos.Rows(I).Cells(1).Text = Format(Rut, FMT.FCMSD) & "-" & RG.Vrut(Rut)
                    Txt = Gr_Documentos.Rows(I).FindControl("Txt_Verificacion")

                    Select Case NEG.id_P_0023
                        Case 1 : Formato = FMT.FCMSD
                        Case 2 : Formato = FMT.FCMCD4
                        Case 3, 4 : Formato = FMT.FCMCD
                    End Select

                    Gr_Documentos.Rows(I).Cells(5).Text = Format(CDbl(Gr_Documentos.Rows(I).Cells(5).Text), Formato)

                    If Gr_Documentos.Rows(I).Cells(7).Text.Trim() = "" Or Gr_Documentos.Rows(I).Cells(7).Text.Trim() = "&nbsp;" Then
                        Txt.Visible = False
                    Else
                        'Verificamos si las fechas son distintas
                        If Gr_Documentos.Rows(I).Cells(6).Text.Trim() = Gr_Documentos.Rows(I).Cells(7).Text.Trim() Then
                            Txt.Visible = False
                        Else
                            Txt.BackColor = Drawing.Color.RosyBrown   'si se cambio la fecha de verificacion
                        End If
                    End If
                Next

                CreaEventosParaGridDetalleOpe()

            End If

        Catch ex As Exception
            msj.Mensaje(Page, Caption, ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Private Sub CreaEventosParaGridDetalleOpe()

        For I = 0 To Gr_Documentos.Rows.Count - 1

            'Dim NroDocto As String
            Dim idDsi As Integer
            Dim IB_Docto As Image
           
            idDsi = CType(Gr_Documentos.Rows(I).FindControl("id_dsi"), Label).Text

            IB_Docto = Gr_Documentos.Rows(I).FindControl("IB_Docto")

            'NroDocto = Gr_Documentos.Rows(I).Cells(3).Text

            IB_Docto.Attributes.Add("onClick", "var x=window.showModalDialog('detalle_doctos_ges.aspx?id= " & idDsi & "', window, 'scroll:no;status:off;dialogWidth:1000;dialogHeight:1000px;dialogLeft:50px;dialogTop:50px');")

            IB_Docto.Attributes.Add("Style", "cursor: hand")

        Next

    End Sub

    Protected Sub IB_Ok_Con_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Ok_Con.Click
        Me.Txt_Msj_Con.Text = ""
    End Sub

End Class


