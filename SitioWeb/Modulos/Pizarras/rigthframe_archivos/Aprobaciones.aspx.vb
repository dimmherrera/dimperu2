Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Pizarras_rigthframe_archivos_Aprobaciones
    Inherits System.Web.UI.Page

#Region "Declaraciones de Variables"

    Dim CG As New ConsultasGenerales
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim FC As New FuncionesGenerales.FComunes
    Dim Caption As String = "Pizarra Ejecutivo"
    Dim CD As New ClaseControlDual

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            If Request.QueryString("neg") <> "" And Request.QueryString("rut") <> "" And Request.QueryString("sim") <> "" Then

                RutCli = Request.QueryString("rut")
                HF_NroNeg.Value = Request.QueryString("neg")
                NroNegociacion = Request.QueryString("neg")
                NroOperacion = Request.QueryString("sim")


                BuscarClasificaciones()

            End If

        End If

    End Sub

    Protected Sub GV_Clasificacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clasificacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Try

                Dim id As HiddenField
                Dim img As Image

                id = CType(e.Row.FindControl("HF_CCF"), HiddenField)

                img = CType(e.Row.FindControl("Image1"), Image)

                img.Attributes.Add("onMouseOver", "showClasificacion(event,'" & id.Value & "')")
                img.Attributes.Add("onMouseOut", "hideTooltip(event)")

            Catch ex As Exception

            End Try

        End If

    End Sub

#Region "LinkButton para Aprobaciones"

    Protected Sub BuscarClasificaciones()

        Try

            CD.NegociacionClasificacionDevuelve(HF_NroNeg.Value, GV_Clasificacion)

        Catch ex As Exception
            'Msj(Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

#End Region

    Private Sub MarcaGrillaClasificacion(ByVal nrocla As Integer)

        Try

            For I = 0 To GV_Clasificacion.Rows.Count - 1

                Dim ver As ImageButton = CType(GV_Clasificacion.Rows(I).FindControl("Img_Ver"), ImageButton)

                If (nrocla = ver.ToolTip) Then

                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "selectable"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "selectableAlt"
                    End If

                Else
                    If (I Mod 2) = 0 Then
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcell"
                    Else
                        GV_Clasificacion.Rows(I).CssClass = "formatUltcellAlt"
                    End If
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Img_Ver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try

            Dim ver As ImageButton = CType(sender, ImageButton)

            HF_NroNNC.Value = ver.ToolTip

            If HF_NroNNC.Value.Length > 0 Then

                MarcaGrillaClasificacion(HF_NroNNC.Value)

                Table_Firmas.CssClass = "tablanivel"

                CD.NegociacionFirmasDevuelve(HF_NroNNC.Value, CodEje, Table_Firmas)
                Table_Firmas.DataBind()

                If Table_Firmas.Rows.Count > 0 Then
                    Dim sw As Integer = 0

                    Dim coll As Collection

                    coll = CD.AprobacionRescataEstado(HF_NroNNC.Value, Sucursal)

                    If Not IsNothing(coll) Then
                        For i = 1 To coll.Count - 1
                            If coll.Item(i).apb_est_ado = "1" Then
                                sw = 1
                                Exit For
                            End If
                        Next
                    End If

                    
                End If
                
            End If

        Catch ex As Exception

        End Try


    End Sub

  
End Class
