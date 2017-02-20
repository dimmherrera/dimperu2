Imports System.Web.UI.WebControls
Imports System.Web.UI


Public Class RutinasWeb
    '*****************************************************
    'Limpia un Combo o DropDownList (funciona solo para .NET)
    Public Sub LimpiaCombo(ByVal cmb As DropDownList)

        cmb.SelectedIndex = 0
        cmb.SelectedItem.Value = 0

    End Sub
    '*****************************************************
    Public Shared Sub AbrirPgn(ByVal Pg As Object, ByVal Pgn As String)

        Dim scriptStr As String

        scriptStr = "<script language=JavaScript>window.open('" & Pgn.ToString & "','_blank', 'toolbar=no,directories=no,menubar=no,status=no');</script>"
        'scriptStr = "<script language=JavaScript>window.open('" & Pgn.ToString & "','_blank');</script>"
        Pg.RegisterStartupScript("OpenPgn", scriptStr)

    End Sub
    '*****************************************************
    Public Shared Sub Imprimir(ByVal Pg As Object)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript>imprimir();</script>"
        Pg.RegisterStartupScript("Imprimir", scriptStr)

    End Sub
    '*****************************************************
    'Mensaje de Alerta de JavaScript 
    Public Shared Sub Mensaje(ByVal Pg As Object, ByVal StrMensaje As String)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript>alert('" & Replace(StrMensaje, "'", " ") & " ')</script>"
        Pg.RegisterStartupScript("Mensaje", scriptStr)

    End Sub
    '*****************************************************//////------/-/-/-/-/--/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-//-/-/-/-/-/-/-/-//-/-/--/-/-/-/-/-///-/-/-//-/---/-//-/-//-/
    'Mensaje de Confirmación de JavaScript que se encuentra en un Archivo con funciones de JavaScript
    Public Shared Sub Mensaje_Conf(ByVal Pg As Object, ByVal StrMensaje As String, ByVal pEven As String)

        Dim scriptString As String
        scriptString = "<script language=JavaScript>MsjConfirm('" & StrMensaje & "','" & pEven & "')</script>"
        Pg.RegisterStartupScript("Mensaje_Conf", scriptString)

    End Sub
    '*****************************************************
    'Abre una nueva ventana en forma de  PopUp
    Public Shared Sub AbrePopup(ByVal Pg As Object, ByVal TipoVentana As Int16, ByVal Pagina As String, ByVal nombrePag As String, ByVal pW As Integer, ByVal pH As Integer, ByVal pL As Integer, ByVal pT As Integer)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript>WinOpen(" & TipoVentana & ", '" & Pagina & "','" & nombrePag & "'," & pW & "," & pH & "," & pL & ", " & pT & "); </script>"
        Pg.RegisterStartupScript("AbrePopup", scriptStr)

    End Sub

    Public Shared Sub AbrePopupUpdatePanel(ByVal up As UpdatePanel, ByVal TipoVentana As Int16, ByVal Pagina As String, ByVal nombrePag As String, ByVal pW As Integer, ByVal pH As Integer, ByVal pL As Integer, ByVal pT As Integer)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript>WinOpen(" & TipoVentana & ", '" & Pagina & "','" & nombrePag & "'," & pW & "," & pH & "," & pL & ", " & pT & "); </script>"

        Dim guidKey As New Guid

        ScriptManager.RegisterStartupScript(up, up.GetType(), guidKey.ToString(), scriptStr, True)

    End Sub

    Public Shared Sub OpenModal(ByVal Pg As Object, ByVal Pagina As String, ByVal nombrePag As String)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript> WinOpenModal('" & Pagina & "','" & nombrePag & "'); </script>"
        Pg.RegisterStartupScript("AbrePopup", scriptStr)

    End Sub

    Public Shared Sub CloseOpener(ByVal Pg As Object, Optional ByVal evenTarget As String = "xx")

        Dim scriptString As String
        scriptString = "<script language='JavaScript'>"
        scriptString &= "WinCloseOpener('" & evenTarget & "');"
        scriptString &= "</script>"
        Pg.RegisterStartupScript("CloseModal", scriptString)

    End Sub

    Public Shared Sub LlenaCombo(ByVal DatosLst As ArrayList, ByVal lst As DropDownList)

        lst.DataTextField = "Descripcion"
        lst.DataValueField = "Codigo"
        lst.DataSource = DatosLst
        lst.DataBind()

    End Sub

    Public Shared Sub BuscaCombo(ByRef Lst As DropDownList, ByVal PClave As String)

        'If Lst.Items.Count > 0 And Trim(PClave) <> "" Then
        '    Lst.ClearSelection()
        '    Lst.Items.FindByValue(PClave).Selected = True
        'End If

        Dim i As Integer
        For i = 0 To Lst.Items.Count - 1
            If Lst.Items.Item(i).Value = PClave.Trim() Then
                Lst.Items.Item(i).Selected = True
                Exit For
            End If
        Next

    End Sub

    Public Shared Sub ValOpener(ByVal Pg As Object, ByVal operTarget As String, Optional ByVal evenTarget As String = "xx")
        Dim scriptString As String
        scriptString = "<script language=JavaScript>"
        scriptString &= "ValidaOpener('" & operTarget & "','" & evenTarget & "');"
        scriptString &= "</script>"
        Pg.RegisterStartupScript("valOperner", scriptString)
    End Sub

    Public Shared Sub CargaGrillas(ByVal Pg As Object)
        Dim scriptString As String
        scriptString = "<script language=JavaScript>"
        scriptString &= "CargaGrillas();"
        scriptString &= "</script>"
        Pg.RegisterStartupScript("CargaGrillas", scriptString)
    End Sub

    Public Shared Sub ClosePag(ByVal Pg As Object)
        Dim scriptString As String

        scriptString = "<script language='JavaScript'>"
        scriptString &= "window.close();"
        scriptString &= "</script>"

        Pg.RegisterStartupScript("ClosePag", scriptString.ToString)

    End Sub

    Public Shared Sub CloseModal(ByVal Pg As Object, Optional ByVal evenTarget As String = "xx")
        Dim scriptString As String
        scriptString = "<script language='JavaScript'>"
        scriptString &= "CerrarVentana('" & evenTarget & "');"
        scriptString &= "</script>"
        Pg.RegisterStartupScript("CloseModal", scriptString.ToString)
    End Sub

    Public Shared Sub BtnEnabled(ByVal Pg As Object, ByVal Btn As String, ByVal Estado As Boolean)
        Dim scriptString As String
        scriptString = "<script language='JavaScript'> window.document.Form1." & Btn & ".disabled = '" & Estado & "'; </script>"
        Pg.RegisterStartupScript("BtnEnabled", scriptString.ToString)
    End Sub

    Public Shared Sub SetFocus(ByVal Pg As Object, ByVal ctrl As System.Web.UI.Control)
        Dim strFocus As String
        strFocus = ""
        strFocus = strFocus + "<SCRIPT language='javascript'>"
        strFocus = strFocus + "document.getElementById('" & ctrl.ID & "').focus();"
        strFocus = strFocus + "if ( document.getElementById('" & ctrl.ID & "').select != null ){document.getElementById('" & ctrl.ID & "').select();}"
        strFocus = strFocus + "</SCRIPT>"
        Pg.RegisterStartupScript("mySetFocus", strFocus.ToString)
    End Sub

    Public Sub Llenar_Drop(ByVal Coleccion As Object, ByVal StrCampoId As String, ByVal StrCampoDes As String, ByVal Lst As DropDownList, _
                                                 Optional ByVal IdSelect As Long = Nothing, Optional ByVal StrItemDefaul As String = "")

        Try

            Dim encontrado As Boolean
            encontrado = False '
            'encontrado = True


            Lst.Items.Clear() '
            Lst.DataSource = Coleccion
            Lst.DataValueField = StrCampoId
            Lst.DataTextField = StrCampoDes
            Lst.DataBind()
            Lst.ClearSelection() '

            Lst.Items.Insert(0, New ListItem("Seleccionar", 0))
            Lst.Items.Item(0).Selected = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Shared Sub Llenar_RadioList(ByVal Coleccion As Collection, ByVal StrCampoId As String, ByVal StrCampoDes As String, ByVal Lst As RadioButtonList, _
                                  Optional ByVal IdSelect As Long = Nothing, Optional ByVal StrItemDefaul As String = "")

        Dim i As Integer
        Dim encontrado As Boolean
        encontrado = False

        If Coleccion.Count > 0 Then

            Lst.Items.Clear()
            Lst.DataSource = Coleccion
            Lst.DataValueField = StrCampoId
            Lst.DataTextField = StrCampoDes
            Lst.DataBind()

            If Not IsNothing(IdSelect) Then
                For i = 0 To Lst.Items.Count - 1
                    If IdSelect = Lst.Items.Item(i).Value Then
                        Lst.Items.Item(i).Selected = True
                        encontrado = True
                        Exit For
                    End If
                Next
            End If

            If Len(Trim(StrItemDefaul)) <> 0 Then
                Lst.Items.Insert(0, New ListItem(StrItemDefaul, 0))
            End If
            If Not encontrado Then Lst.Items.Item(0).Selected = True

        End If

    End Sub

    Public Shared Sub EjecutaJScript(ByVal Pg As Object, ByVal FunctionName As String)

        Dim scriptStr As String
        scriptStr = "<script language=JavaScript>" & FunctionName & ";</script>"
        Pg.RegisterStartupScript("FunctionName", scriptStr)

    End Sub

    Public Shared Sub EjecutaJScript(ByVal up As UpdatePanel, ByVal FunctionName As String)

        Dim scriptStr As String

        scriptStr = "<script language=JavaScript>" & FunctionName & ";</script>"

        Dim guidKey As New Guid

        ScriptManager.RegisterStartupScript(up, up.GetType(), guidKey.ToString(), scriptStr, True)


    End Sub

    '----------------------------------------------------------------------------------------------------
    'TREEVIEW
    '----------------------------------------------------------------------------------------------------
    Public Sub BuscaNodo(ByVal N As TreeNode, ByVal PClave As String)
        Dim i As Integer = 0
        Dim aux As New TreeNode


        For Each aux In N.ChildNodes
            If aux.Value = PClave Then
                aux.Checked = True
                Exit Sub
            End If
            BuscaNodo(aux, PClave)
            i = i + 1
        Next

    End Sub

    Public Sub BuscaPadre(ByVal N As TreeNode, ByVal PClave As Integer, ByVal Est As Boolean)
        Dim i As Integer = 0
        Dim aux As TreeNode

        For Each aux In N.ChildNodes
            If aux.Value = PClave Then
                Hijos(aux, Est)
            End If
            BuscaPadre(aux, PClave, Est)
            i = i - 1
        Next aux
    End Sub

    Public Sub Hijos(ByVal auxtree As TreeNode, ByVal ch As Boolean)
        Dim IdxAux As TreeNode
        Dim x As Integer = 0

        Try

            For Each IdxAux In auxtree.ChildNodes
                IdxAux.Checked = ch
                Hijos(IdxAux, ch)
                x = x + 1
            Next

        Catch E As Exception
            E.ToString()
        End Try
    End Sub

    Public Sub Padres(ByVal auxtree As TreeNode, ByVal ch As Boolean)
        Try

            'For Each IdxAux In auxtree.Nodes
            auxtree.Checked = ch
            If Not auxtree.Checked Then Exit Sub
            Padres(auxtree.Parent, ch)
            'Next

        Catch E As Exception
            E.ToString()
        End Try
    End Sub


End Class

