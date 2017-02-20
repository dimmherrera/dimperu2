Imports Microsoft.VisualBasic
Imports AjaxControlToolkit
Imports System.Web.UI.WebControls
Imports System.Web.UI


Public Class ClsMensaje

    Public Enum TipoDeMensaje As Short
        _Error = 1
        _Exclamacion = 2
        _Informacion = 3
        _Confirmacion = 4
        _Excepcion = 5
        _Redireccion = 6
    End Enum

    Public Sub Mensaje(ByVal P As Page, ByVal Titulo As String, ByVal Texto As String, ByVal Tipo As TipoDeMensaje, _
                       Optional ByVal LB As String = "", Optional ByVal Contenedora As Boolean = True)


        Dim Modal As ModalPopupExtender
        Dim Image_Mensaje As Image
        Dim Boton_Aceptar As ImageButton
        Dim Boton_Cancel As ImageButton
        Dim Lbl_Titulo As Label
        Dim Lbl_Mensaje As Label

        If Contenedora Then

            Lbl_Titulo = CType(P.FindControl("ctl00$Mensaje1$Lbl_error"), Label)
            Lbl_Mensaje = CType(P.FindControl("ctl00$Mensaje1$Txt_Mensaje"), Label)
            Modal = P.FindControl("ctl00$Mensaje1$Modal_Mensaje")
            Image_Mensaje = P.FindControl("ctl00$Mensaje1$Img_Mensaje")
            Boton_Cancel = P.FindControl("ctl00$Mensaje1$canc")
            Boton_Aceptar = P.FindControl("ctl00$Mensaje1$Okbutton")

        Else

            Lbl_Titulo = CType(P.FindControl("Mensaje1$Lbl_error"), Label)
            Lbl_Mensaje = CType(P.FindControl("Mensaje1$Txt_Mensaje"), Label)

            Modal = P.FindControl("Mensaje1$Modal_Mensaje")
            Image_Mensaje = P.FindControl("Mensaje1$Img_Mensaje")
            Boton_Cancel = P.FindControl("Mensaje1$canc")
            Boton_Aceptar = P.FindControl("Mensaje1$Okbutton")

        End If

        Lbl_Titulo.Text = Titulo
        Lbl_Mensaje.Text = Texto

        Boton_Aceptar.Attributes.Remove("onClick")

        Select Case Tipo

            Case TipoDeMensaje._Error
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/error.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Exclamacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Informacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Redireccion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False

                If LB <> "" Then
                    Boton_Aceptar.Attributes.Add("onClick", "__doPostBack('" & LB & "', '');")
                    'Boton_Aceptar.Attributes.Add("onClick", "__doPostBack('" & LB & "', '')", " Response.Redirect('"MDeudores.aspx"', 'False');")

                End If
            Case TipoDeMensaje._Confirmacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Question.gif"
                Boton_Cancel.Visible = True
                Modal.OkControlID = Boton_Cancel.ClientID

                If LB <> "" Then
                    Boton_Aceptar.Attributes.Add("onClick", "__doPostBack('" & LB & "', '');")

                End If

            Case TipoDeMensaje._Excepcion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False
                Modal.OkControlID = Boton_Aceptar.ClientID
        End Select

        Modal.Show()

    End Sub

    Public Sub Mensaje_WebControl(ByVal P As Page, ByVal Titulo As String, ByVal Texto As String, ByVal Tipo As TipoDeMensaje)



        Dim Modal As ModalPopupExtender
        Dim Image_Mensaje As Image
        Dim Boton_Aceptar As ImageButton
        Dim Boton_Cancel As ImageButton
        Dim Lbl_Titulo As Label
        Dim Lbl_Mensaje As Label


        Lbl_Titulo = CType(P.FindControl("WC_QuePaga1$Mensaje1$Lbl_error"), Label)
        Lbl_Mensaje = CType(P.FindControl("WC_QuePaga1$Mensaje1$Txt_Mensaje"), Label)

        Modal = P.FindControl("WC_QuePaga1$Mensaje1$Modal_Mensaje")
        Image_Mensaje = P.FindControl("WC_QuePaga1$Mensaje1$Img_Mensaje")
        Boton_Cancel = P.FindControl("WC_QuePaga1$Mensaje1$canc")
        Boton_Aceptar = P.FindControl("WC_QuePaga1$Mensaje1$Okbutton")


        Lbl_Titulo.Text = Titulo
        Lbl_Mensaje.Text = Texto

        Boton_Aceptar.Attributes.Remove("onClick")

        Select Case Tipo

            Case TipoDeMensaje._Error
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/error.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Exclamacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Informacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Info.gif"
                Boton_Cancel.Visible = False
            Case TipoDeMensaje._Confirmacion
                Image_Mensaje.ImageUrl = "~/Imagenes/Iconos/Question.gif"
                Boton_Cancel.Visible = True
                Modal.OkControlID = Boton_Cancel.ClientID

                'If LB <> "" Then
                '    Boton_Aceptar.Attributes.Add("onClick", "__doPostBack('" & LB & "', '');")
                'End If

        End Select

        Modal.Show()

    End Sub

End Class
