Imports FuncionesGenerales.FComunes
Imports ClsSession.ClsSession
Imports FuncionesGenerales.Errores
Imports CapaDatos
Partial Class feriados
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje


    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub

    Protected Sub Calen_fer_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calen_fer.SelectionChanged
        Dim i As Integer

        For i = 0 To Lb_fer.Items.Count - 1
            If Me.Calen_fer.SelectedDate.Date = Lb_fer.Items(i).Value Then
                Exit Sub
            End If
        Next

        Me.Lb_fer.Items.Add(Me.Calen_fer.SelectedDate.Date)




    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Calen_fer.SelectedDate = Date.Now
            Me.TraeFeriados()
        End If

    End Sub
 

    Function FORMATOFECHA(ByVal Mes1 As Integer, ByVal AnoFi1 As Integer) As String
        Dim Fech As Date
        Fech = "01/" & Mes1 & "/" & AnoFi1
        FORMATOFECHA = Fech.AddDays(-1)
        FORMATOFECHA = Format(Fech.AddDays(-1), "dd/MM/yyyy")
    End Function

    Protected Sub Calen_fer_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles Calen_fer.VisibleMonthChanged

        Calen_fer.SelectedDate = Calen_fer.VisibleDate
        TraeFeriados()

    End Sub

    Private Sub TraeFeriados()
        Try


            Lb_fer.Items.Clear()
            Lb_fer.DataSource = cg.Feriados_Año(Me.Calen_fer.SelectedDate.Date.Year) 'FY 09-06-2012
            Lb_fer.DataBind()



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BorraFeriados()


        Try

            ag.FERIADOS_ELIMINA(Me.Lb_fer.SelectedItem.Value)

        Catch ex As Exception

        End Try
    End Sub




    Protected Sub btn_gua1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Guardar.Click

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010212, Usr, "PRESIONO GUARDAR FERIADO ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        msj.Mensaje(Me, "Atención", "¿Desea guardar los feriados?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_gua.UniqueID)


    End Sub

    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_quit.Click


        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020212, Usr, "PRESIONO ELIMINAR FERIADO") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        msj.Mensaje(Me, "Atención", "¿Desea Eliminar ?", ClsMensaje.TipoDeMensaje._Confirmacion, lb_eli.UniqueID)


    End Sub

    Protected Sub btn_limp1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.Lb_fer.Items.Clear()
    End Sub


    Protected Sub lb_gua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_gua.Click
        ag.Feriados_Guarda(Me.Lb_fer)
        msj.Mensaje(Me.Page, "Atención", "Se ha Guardado correctamente", 2)
        Calen_fer.VisibleDate = Date.Now
        Calen_fer.SelectedDate = Calen_fer.VisibleDate
        Me.TraeFeriados()

    End Sub

    Protected Sub lb_eli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_eli.Click


        If Not IsNothing(Me.Lb_fer.SelectedItem) Then
            BorraFeriados()
            msj.Mensaje(Me.Page, "Atención", "Se ha Eliminado correctamente", 2)
            Lb_fer.Items.Remove(Me.Lb_fer.SelectedItem.Value)
            Me.Label1.Visible = True
        Else
            msj.Mensaje(Me.Page, "Atención", "Debes Seleccionar una fecha para eliminar", 2)
            Me.Label1.Visible = False
        End If

    End Sub
End Class


