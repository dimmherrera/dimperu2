
Partial Class Modulos_Operaciones_rightframe_archivos_Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Label1.Text = WeekdayName(Weekday(CDate(TextBox1.Text), Microsoft.VisualBasic.FirstDayOfWeek.Monday), , Microsoft.VisualBasic.FirstDayOfWeek.Monday) & " - " & Weekday(CDate(TextBox1.Text)) & " -" & CDate(TextBox1.Text)
    End Sub
End Class
