Imports ClsSession.ClsSession
Imports CapaDatos
Imports FuncionesGenerales.RutinasWeb
Imports FuncionesGenerales.FComunes


Partial Class Clientes_Pop_Up_Gastos
    Inherits System.Web.UI.Page

#Region "Declaracion de variables para la clase"
    Private CG As New ConsultasGenerales
    Private FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim Caption As String = "Mantención Comision"
    Dim Sesion As New ClsSession.ClsSession
    Dim Var As New FuncionesGenerales.Variables
    Dim CMC As New ClaseComercial
    Dim RG As New FuncionesGenerales.FComunes
    Dim CLSQRY As New ConsultasGenerales
    Dim CLSGDC As New ActualizacionesGenerales
#End Region


    Private Sub CargaGastos()

        Try

            Me.gd_gastos.DataSource = CMC.GastosDefinidosDevuelve(Sucursal)
            Me.gd_gastos.DataBind()

            For I = 0 To gd_gastos.Rows.Count - 1
                Dim Monto As Double = gd_gastos.Rows(I).Cells(3).Text
                gd_gastos.Rows(I).Cells(3).Text = Format(Monto, FMT.FCMSD)
            Next

        Catch ex As Exception
            Msj.Mensaje(Me, Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Me.Txt_Rut.Text = Request.QueryString("rut")
            Me.Txt_Dig.Text = RG.Vrut(Me.Txt_Rut.Text)
            CargaGastos()
            CargaChkGrd()
            CargaDatosCliente()
            Txt_Rut.Attributes.Add("Style", "Text-Align:right")
            Txt_Dig.Attributes.Add("Style", "Text-Align:right")
            Txt_Rut.CssClass = "clsDisabled"
        End If
      
        IB_Volver.Attributes.Add("onClick", "javascript:window.close()")

    End Sub

    Private Sub CargaChkGrd()
        Dim ch As CheckBox
        Dim col_gdc As New Collection
        For i = 0 To Me.gd_gastos.Rows.Count - 1
            Dim GDC As New GDC_cls
            ch = gd_gastos.Rows(i).FindControl("ch_sel")

            'Busca gastos para Chk
            col_gdc = CLSQRY.VerificaOpGastos(Request.QueryString("rut"))

            If Not IsNothing(col_gdc) Then
                If col_gdc.Count > 0 Then

                    For x = 1 To col_gdc.Count

                        If col_gdc.Item(x).id_gto = gd_gastos.Rows(i).Cells(1).Text Then

                            ch.Checked = True

                        End If

                    Next

                End If
            End If

        Next

    End Sub
    Protected Sub IB_Guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Guardar.Click 'Handles IB_Guardar.Click
        Try
            Dim ch As CheckBox

            'Elimina gasto cliente 
            CLSGDC.EliminaGastosCli(Me.Txt_Rut.Text)

            For i = 0 To Me.gd_gastos.Rows.Count - 1
                Dim GDC As New GDC_cls
                ch = gd_gastos.Rows(i).FindControl("ch_sel")

                If ch.Checked Then

                    GDC.id_gto = gd_gastos.Rows(i).Cells(1).Text
                    GDC.cli_idc = Format(CLng(Me.Txt_Rut.Text), Var.FMT_RUT)
                    CLSGDC.GastosInserta(GDC)

                End If
            Next

            Msj.Mensaje(Me, "Atención", "Datos actualizados", ClsMensaje.TipoDeMensaje._Exclamacion)
            'Msj.Mensaje(Me, "Atención", "Datos actualizados", TipoDeMensaje._Informacion, "", False)

        Catch ex As Exception
            Msj.Mensaje(Me.Page, Caption, ex.Message, TipoDeMensaje._Error, "", False)
        End Try
    End Sub


    Private Function CargaDatosCliente() As Boolean

        Dim ClsCli As New ClaseClientes
        Dim RG As New FuncionesGenerales.FComunes
        Dim Sesion As New ClsSession.ClsSession
        Dim Cli As cli_cls



        Try


            Cli = ClsCli.ClientesDevuelve(Replace(Txt_Rut.Text, ".", ""), Me.Txt_Dig.Text)


            If valida_cliente <> "" Then

                Msj.Mensaje(Me.Page, Caption, valida_cliente, TipoDeMensaje._Informacion)
                Exit Function
            Else
                If IsNothing(Cli) Then
                    Msj.Mensaje(Page, Caption, "Cliente no existe", ClsMensaje.TipoDeMensaje._Exclamacion)
                    Exit Function
                End If


                Me.Txt_Raz_Soc.Text = Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn

                Return True
            End If
        Catch ex As Exception

            Return False

        End Try


    End Function
End Class
