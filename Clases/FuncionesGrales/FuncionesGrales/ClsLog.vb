Public Class ClsLog

    Public Sub eLog(ByVal mensaje As String)

        Dim ArchLog As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "FactoringNet.log"
        Dim sw As New System.IO.StreamWriter(ArchLog, True)
        Dim fecha As String = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")

        mensaje = fecha + "; " + mensaje + ";"

        sw.WriteLine(mensaje)
        sw.Close()

    End Sub

End Class
