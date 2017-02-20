Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class BusquedaDeAplicaciones
    Inherits System.Web.UI.Page

#Region "DECLARACION DE VARIABLES"

    Dim Caption As String = "Aplicaciones"
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim Var As New FuncionesGenerales.Variables
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje

#End Region

#Region "EVENTOS"

    Protected Sub Lb_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lb_buscar.Click

        Try

            If CargaDatosCliente(Txt_Rut_Cli.Text) Then

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Private Function CargaDatosCliente(ByVal RutCliente As Long) As Boolean
        Try
            Dim Sesion As New ClsSession.ClsSession
            Dim ClsCli As New ClaseClientes
            Dim CLI As cli_cls

            CLI = ClsCli.ClientesDevuelve(Txt_Rut_Cli.Text.Replace(".", ""), Txt_Dig_Cli.Text.ToUpper)

            If Sesion.valida_cliente <> "" Then
                Msj.Mensaje(Me.Page, Caption, Sesion.valida_cliente, TipoDeMensaje._Exclamacion)
                Exit Function
            End If
            If IsNothing(CLI) Then
                Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Function

            End If
            Session("Cliente") = CLI

            'Tipo de cliente (Natural / Juridico)
            If CLI.id_P_0044 = 1 Then
                Me.Txt_Raz_Soc.Text = CLI.cli_rso.Trim & " " & CLI.cli_ape_ptn.Trim & " " & CLI.cli_ape_mtn.Trim
            Else
                Me.Txt_Raz_Soc.Text = CLI.cli_rso
            End If

            Return True

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
            Return False
        End Try

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                CG.EjecutivosDevuelve(DP_Ejecutivos, CodEje, 15)

                Txt_Fecha_Desde.Text = Date.Now.ToShortDateString
                Txt_Fecha_Hasta.Text = Date.Now.ToShortDateString
                Txt_Rut_Cli.Attributes.Add("Style", "TEXT-ALIGN: right")
                RB_Pendiente.Checked = True
                RB_VB.Checked = False
                RB_Cursadas.Checked = False

                CB_TodosEje.Checked = True

                If Not IsNothing(Session("Cliente")) Then
                    CB_Cliente.Checked = True
                    Txt_Rut_Cli.ReadOnly = True
                    Txt_Rut_Cli.CssClass = "clsDisabled"
                    Txt_Dig_Cli.ReadOnly = True
                    Txt_Dig_Cli.CssClass = "clsDisabled"
                    Txt_Rut_Cli.Text = CInt(Session("Cliente").cli_idc)
                    Txt_Dig_Cli.Text = FC.Vrut(CLng(Txt_Rut_Cli.Text))
                    CargaDatosCliente(Txt_Rut_Cli.Text)
                End If

                IB_AyudaCli.Attributes.Add("Onclick", "WinOpen(2,'../../Ayudas/AyudaClipopup.aspx','Ayuda',620 ,400,100,100);")

            End If
        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub IB_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Buscar.Click
        Try

            Dim agt As New Perfiles.Cls_Principal

            If Not agt.ValidaAccesso(20, 20020604, Usr, "PRESIONO VER APLICACIONES DE EXCEDENTES") Then
                Msj.Mensaje(Me.Page, Caption, "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If


            Dim Coll As Collection
            Dim Estado As Integer

            Select Case True
                Case RB_Pendiente.Checked : Estado = 1
                Case RB_VB.Checked : Estado = 2
                Case RB_Cursadas.Checked : Estado = 3
            End Select

            Coll = CG.Aplicacion_Devuelve(Txt_Rut_Cli.Text, Txt_Rut_Cli.Text, _
                                          Txt_Fecha_Desde.Text, Txt_Fecha_Hasta.Text, _
                                          Estado, "")

            GV_Aplicaciones.DataSource = Coll
            GV_Aplicaciones.DataBind()

            If Coll.Count > 0 Then

                For I = 0 To GV_Aplicaciones.Rows.Count - 1

                    Dim rut As Long = GV_Aplicaciones.Rows(I).Cells(0).Text
                    GV_Aplicaciones.Rows(I).Cells(0).Text = Format(Rut, Fmt.FCMSD) & "-" & FC.Vrut(Rut)
                    GV_Aplicaciones.Rows(I).Cells(4).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(4).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(5).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(5).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(6).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(6).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(7).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(7).Text), Fmt.FCMSD)
                    GV_Aplicaciones.Rows(I).Cells(8).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(8).Text), Fmt.FCMSD)

                    GV_Aplicaciones.Rows(I).Cells(9).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(9).Text), Fmt.FCMCD)
                    GV_Aplicaciones.Rows(I).Cells(10).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(10).Text), Fmt.FCMCD)

                    GV_Aplicaciones.Rows(I).Cells(11).Text = Format(CDbl(GV_Aplicaciones.Rows(I).Cells(11).Text), Fmt.FCMSD)

                    If Coll.Item(I + 1).apl_apb_com = "S" Then
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#CCFFCC")
                        GV_Aplicaciones.Rows(I).BackColor = col
                    Else
                        Dim col As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml("#99CCFF")
                        GV_Aplicaciones.Rows(I).BackColor = col
                    End If

                Next

            Else
                Msj.Mensaje(Me, Caption, "No se encontraron aplicaciones", TipoDeMensaje._Informacion, Nothing, False)
            End If

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub

    Protected Sub CB_Cliente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_Cliente.CheckedChanged

        If CB_Cliente.Checked Then
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Rut_Cli_MaskedEditExtender.Enabled = True
            Txt_Rut_Cli.ReadOnly = False
            Txt_Dig_Cli.ReadOnly = False
            Txt_Rut_Cli.CssClass = "clsMandatorio"
            Txt_Dig_Cli.CssClass = "clsMandatorio"
            IB_AyudaCli.Enabled = True
        Else
            Txt_Rut_Cli.Text = ""
            Txt_Dig_Cli.Text = ""
            Txt_Raz_Soc.Text = ""
            Txt_Rut_Cli_MaskedEditExtender.Enabled = False
            Txt_Rut_Cli.ReadOnly = True
            Txt_Dig_Cli.ReadOnly = True
            Txt_Rut_Cli.CssClass = "clsDisabled"
            Txt_Dig_Cli.CssClass = "clsDisabled"
            IB_AyudaCli.Enabled = False
        End If

    End Sub

    Protected Sub CB_TodosEje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB_TodosEje.CheckedChanged

        If CB_TodosEje.Checked Then
            DP_Ejecutivos.ClearSelection()
            DP_Ejecutivos.CssClass = "clsMandatorio"
            DP_Ejecutivos.Enabled = True
        Else
            DP_Ejecutivos.ClearSelection()
            DP_Ejecutivos.CssClass = "clsDisabled"
            DP_Ejecutivos.Enabled = False
        End If

    End Sub

    Protected Sub Txt_Dig_Cli_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Dig_Cli.TextChanged
        Try

            If Txt_Rut_Cli.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese NIT", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If

            If Txt_Dig_Cli.Text = "" Then
                Msj.Mensaje(Page, Caption, "Ingrese dígito", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
                Exit Sub
            End If


            CargaDatosCliente(Txt_Rut_Cli.Text)

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error, Nothing, False)
        End Try
    End Sub
#End Region



  
End Class
