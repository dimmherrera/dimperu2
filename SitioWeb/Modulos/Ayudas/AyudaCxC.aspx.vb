Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports CapaDatos
Imports ClsSession.SesionPagos
Partial Class Modulos_Ayudas_Default
    Inherits System.Web.UI.Page
    Dim sesion As New ClsSession.ClsSession
    Dim ses_ope As New ClsSession.SesionOperaciones
    Dim clasecli As New ClaseClientes
    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim RG As New FuncionesGenerales.FComunes
    Dim var As New FuncionesGenerales.Variables
    Dim fmt As New FuncionesGenerales.ClsLocateInfo
    Dim rw As New FuncionesGenerales.RutinasWeb
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mk As String
        If Not Me.IsPostBack Then
            valida_cliente = ""
            page_dig = 0
            mk = Request.QueryString("Rut")
            Txt_Rut.Text = mk
            cargagrillas()
        End If

    End Sub
    Private Sub cargagrillas()
        Try

            Dim rut_cli As Long, rut_cli1 As Long, SUC1 As Integer, suc2 As Integer, eje1 As String, eje2 As String, _
            nro_otg1 As Integer, nro_otg2 As Int64, mon1 As String, mon2 As String, estado As Integer, estado1 As Integer, _
            nro_doc1 As String, nro_doc2 As String, fec_otg As String, fec_otg1 As String, tipo, rut_deu As Long, _
            rut_deu1 As Long, fec_vcto1 As Date, fec_vcto2 As Date, cobr1 As String, cobr2 As String, obl As String, _
            obl2 As String, TipoDocto_Dsd As Integer, TipoDocto_Hst As Integer

            rut_cli = Me.Txt_Rut.Text
            rut_cli1 = Me.Txt_Rut.Text

            rut_deu = 0
            rut_deu1 = 999999999999

            'Sucursal
            SUC1 = 1
            suc2 = 1
            'Ejecutivo
            eje1 = 0
            eje2 = 9999
            'Moneda
            mon1 = 0
            mon2 = 9999
            'Responsabilidad
            cobr1 = "S"
            cobr2 = "N"


            tipo = 2

            estado = 0
            estado1 = 999


            nro_otg1 = 0
            nro_otg2 = 9999999999

            nro_doc1 = "0"
            nro_doc2 = "Z"

            fec_otg = CDate("01/01/1900")
            fec_otg1 = CDate("31/12/9999")

            fec_vcto1 = CDate("01/01/1900")
            fec_vcto2 = CDate("31/12/9999")

            obl = "S"
            obl2 = "N"

            TipoDocto_Dsd = 0
            TipoDocto_Hst = 9999

            Coll_DOC = New Collection
            Coll_DOC = OP.DocumentosOtorgados_a_Modificar_Retorna(rut_cli, rut_cli1, rut_deu, rut_deu1, 0, 99999999, _
                        TipoDocto_Dsd, TipoDocto_Hst, nro_doc1, nro_doc2, 0, 9999, fec_vcto1, fec_vcto2, 1, 2, 4, _
                        9, 11, 12, 12, 12, 12, 12, 12, 12, nro_otg1, nro_otg2, cobr1, cobr2, fec_otg, fec_otg1, obl, obl2)

            Me.Gr_Doc.DataSource = Coll_DOC

            Me.Gr_Doc.DataBind()

            If Me.Gr_Doc.Rows.Count > 0 Then

            End If

            If Me.Gr_Doc.Rows.Count = 0 Then

                Exit Sub
            End If
            Dim I As Integer
            For I = 0 To Gr_Doc.Rows.Count - 1
                GR_Doc.Rows(I).Cells(7).Text = Format(CDbl(GR_Doc.Rows(I).Cells(7).Text), fmt.FCMSD)
                GR_Doc.Rows(I).Cells(1).Text = Format(CLng(Me.GR_Doc.Rows(I).Cells(1).Text), fmt.FCMSD) & "-" & RG.Vrut(CLng(Me.GR_Doc.Rows(I).Cells(1).Text))
            Next
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Gr_Doc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gr_Doc.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim img As ImageButton = CType(e.Row.FindControl("Img_Ver"), ImageButton)

            If Request.QueryString("DNC") <> "" Then
                img.Attributes.Add("onClick", "javascript:AceptaDNC('" & img.AlternateText & "', '" & img.ToolTip & "');")
            Else
                img.Attributes.Add("onClick", "javascript:AceptaCXC('" & img.AlternateText & "', '" & img.ToolTip & "');")
            End If



        End If


    End Sub
    Protected Sub IB_Next_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Next.Click
        Try

            If Me.Gr_Doc.Rows.Count < 15 Then
                msj.Mensaje(Me, "Atención", "Ya está en la última página de la lista", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.GR_Doc.Rows.Count = 15 Then
                page_dig = page_dig + 15
                cargagrillas()
            End If

        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_Prev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Prev.Click
        Try
            If page_dig = 0 Then
                msj.Mensaje(Me, "Atención", "Ya está en la primera página de la lista", TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If Me.GR_Doc.Rows.Count <= 15 Then
                page_dig = page_dig - 15
                cargagrillas()
            End If
            
        Catch ex As Exception
            msj.Mensaje(Page, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try
    End Sub


    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub
End Class
