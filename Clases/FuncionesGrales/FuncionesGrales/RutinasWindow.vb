Imports System.Windows.Forms

Public Class RutinasWindow


    Public Sub LlenaCombo(ByVal DatosLst As ArrayList, ByVal lst As ComboBox)

        'Llena un combo con datos traidos de ArrayList

        Try

            lst.Items.Clear()

            lst.DisplayMember = "descripcion"
            lst.ValueMember = "codigo"

            'lst.DataSource = DatosLst
            lst.Items.AddRange(DatosLst.ToArray)

            
            lst.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message, 16)
        End Try

    End Sub

    Public Sub BuscaEnCombo(ByVal Codigo As Integer, ByVal Lst As ComboBox)

        'Busca un valor el un combo

        Try

            'For I = 0 To Lst.Items.Count - 1

            '    If Lst.Items(I).codigo = Codigo Then
            '        Lst.Items(I) = Codigo
            '        'Lst.Items(I).selected = True
            '        Exit For
            '    End If

            'Next
            
        Catch ex As Exception
            MsgBox(ex.Message, 16)
        End Try

    End Sub

End Class

Public Class clsRichTextJustify

End Class