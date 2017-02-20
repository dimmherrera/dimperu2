Imports System.Web.Mail
Imports System.Text
Public Class Email

    'Public Sub EnviarMail(ByVal Para As String, _
    '                           ByVal De As String, _
    '                           ByVal Asunto As String, _
    '                           ByVal body As StringBuilder)



    '    Dim email As New MailMessage

    '    email.From = De.ToString
    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/smtsperver") = "SMTPServerName"
    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25
    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2

    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/sendusername") = "SMTPAUTHUser"
    '    'email.Fields("http://schemas.microsoft.com/cdo/configuration/sendpassword") = "SMTPAUTHPassword"

    '    email.To = Para.ToString
    '    email.Subject = Asunto.ToString
    '    email.BodyFormat = MailFormat.Text
    '    email.Body = body.ToString
    '    email.Priority = MailPriority.High

    '    Try

    '        'SmtpMail.SmtpServer = "192.168.0.38"
    '        SmtpMail.SmtpServer = "correo.dim"
    '        SmtpMail.Send(email)


    '    Catch exc As Exception


    '    End Try


    'End Sub

    Public Function enviomail(ByVal Para As Collection, _
                               ByVal De As String, _
                               ByVal Asunto As String, _
                               ByVal body As StringBuilder) As Boolean

        Dim _message As New System.Net.Mail.MailMessage
        Dim _SMTP As New System.Net.Mail.SmtpClient
        Dim i As Integer

        'CONFIGURACIàN DEL STMP

        'IMPORTE CAMBIAR CORREO SALIENTE !!!!
        _SMTP.Credentials = New System.Net.NetworkCredential("asaldivar@dim.com", "as0406") ' la cuenta que lo envia

        _SMTP.Host = "mail.dim.cl"

        _SMTP.Port = 25

        _SMTP.EnableSsl = False


        ' CONFIGURACION DEL MENSAJE
        For i = 1 To Para.Count
            _message.To.Add(Para.Item(i).eje_mail) 'Cuenta de Correo al que se le quiere enviar el e-mail
        Next

        _message.From = New System.Net.Mail.MailAddress(De, "FactorClick", System.Text.Encoding.UTF8) 'Quien lo env¡a
        _message.Subject = Asunto.ToString 'Sujeto del e-mail
        _message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
        _message.Body = body.ToString 'contenido del mail
        _message.BodyEncoding = System.Text.Encoding.UTF8
        _message.Priority = System.Net.Mail.MailPriority.Normal
        '_message.IsBodyHtml = False
        _message.IsBodyHtml = True


        '//Agregamos como recurso ligado, el logo del proyecto.
        'LinkedResource lr = new LinkedResource([la ruta de nuestra imagen]);
        'lr.ContentId = “idDeLaImagen”;
        '//Incluimos la vista en HTML como vista del correo a enviar.
        Dim av As System.Net.Mail.AlternateView
        av = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body.ToString, Nothing, System.Net.Mime.MediaTypeNames.Text.Html)

        'av.LinkedResources.Add(lr);
        _message.AlternateViews.Add(av)


        'ADICION DE DATOS ADJUNTOS

        'Dim _File As String = Application.Item..DirectoryPath & "archivo" 'archivo que se quiere adjuntar
        'Dim _Attachment As New System.Net.Mail.Attachment(_File, System.Net.Mime.MediaTypeNames.Application.Octet)
        '_Message.Attachments.Add(_Attachment)

        'ENVIO
        Try

            _SMTP.Send(_message)

        Catch ex As System.Net.Mail.SmtpException

        End Try

    End Function

End Class
