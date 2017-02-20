Imports Microsoft.Office.Interop.Excel
Imports ClsSession.SesionOperaciones
Imports FuncionesGenerales.RutinasWeb
Imports ClsSession.ClsSession
Imports CapaDatos
Imports System.Data.OleDb
Imports System.Data

Partial Class Int_masiva_ddr
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial


#Region "Drops"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            cabecera_grilla.Visible = False

            cg.ParametrosDevuelve(TablaParametro.Moneda, True, Dr_moneda)
          


            Coll_DOC = New Collection


        End If

    End Sub

    Protected Sub Dr_campo_0_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_0.SelectedIndexChanged

        If Me.Dr_campo_0.SelectedValue <> 0 Then

            Me.Dr_campo_1.Enabled = True
            Me.Dr_campo_1.CssClass = "clsMandatorio"

            Me.Dr_campo_0.Enabled = False
            Me.Dr_campo_0.CssClass = "clsDisabled"

            Me.Dr_campo_0.Visible = False
            Label0.Text = Me.Dr_campo_0.SelectedItem.Text
            Label0.Visible = True
            Label0.Width = 100%
            HiddenField1.Value = Me.Dr_campo_0.SelectedIndex

            setea_cabeceras(Me.Dr_campo_0.SelectedValue, Label0)

            For i = 0 To Me.Dr_campo_0.Items.Count - 1

                For x = 1 To 10
                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_0.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_0.SelectedIndex).Enabled = False
                    End If
                    dr.SelectedValue = Val(hf.Value)
                Next
            Next

        End If
        '      Me.Dr_campo_1.Focus()
    End Sub

    Protected Sub Dr_campo_1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_1.SelectedIndexChanged

        If Me.Dr_campo_1.SelectedValue <> 0 Then
            Me.Dr_campo_2.Enabled = True
            Me.Dr_campo_2.CssClass = "clsMandatorio"

            Me.Dr_campo_1.Enabled = False
            Me.Dr_campo_1.CssClass = "clsDisabled"


            Me.Dr_campo_1.Visible = False
            Label1.Text = Me.Dr_campo_1.SelectedItem.Text
            Label1.Visible = True
            Label1.Width = 100%


            HiddenField2.Value = Me.Dr_campo_1.SelectedIndex

            setea_cabeceras(Me.Dr_campo_1.SelectedValue, Label1)

            For i = 0 To Me.Dr_campo_1.Items.Count - 1
                For x = 2 To 10
                    Dim dr As DropDownList
                    Dim hf As HiddenField
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_1.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_1.SelectedIndex).Enabled = False
                    End If
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedValue = Val(hf.Value)

                Next
            Next

        End If


        '       Me.Dr_campo_2.Focus()
    End Sub

    Protected Sub Dr_campo_2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_2.SelectedIndexChanged

        If Me.Dr_campo_2.SelectedValue <> 0 Then

            Me.Dr_campo_3.Enabled = True
            Me.Dr_campo_3.CssClass = "clsMandatorio"

            Me.Dr_campo_2.Enabled = False
            Me.Dr_campo_2.CssClass = "clsDisabled"

            Me.Dr_campo_2.Visible = False
            Label2.Text = Me.Dr_campo_2.SelectedItem.Text
            Label2.Visible = True
            Label2.Width = 100%

            HiddenField3.Value = Me.Dr_campo_2.SelectedIndex

            setea_cabeceras(Me.Dr_campo_2.SelectedValue, Label2)

            For i = 0 To Me.Dr_campo_2.Items.Count - 1
                For x = 3 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If i <> 0 And Val(Me.Dr_campo_2.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i Then

                        dr.Items(i).Enabled = True

                    Else

                        dr.Items(Me.Dr_campo_2.SelectedIndex).Enabled = False

                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)

                Next
            Next

        End If



        '      Me.Dr_campo_3.Focus()

    End Sub

    Protected Sub Dr_campo_3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_3.SelectedIndexChanged

        If Me.Dr_campo_3.SelectedValue <> 0 Then

            Me.Dr_campo_4.Enabled = True
            Me.Dr_campo_4.CssClass = "clsMandatorio"

            Me.Dr_campo_3.Enabled = False
            Me.Dr_campo_3.CssClass = "clsDisabled"

            Me.Dr_campo_3.Visible = False
            Label3.Text = Me.Dr_campo_3.SelectedItem.Text
            Label3.Visible = True
            Label3.Width = 100%

            setea_cabeceras(Me.Dr_campo_3.SelectedValue, Label3)


            HiddenField4.Value = Me.Dr_campo_3.SelectedIndex

            For i = 0 To Me.Dr_campo_3.Items.Count - 1

                For x = 4 To 10

                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_3.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_3.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)

                Next

            Next

        End If

        '     Me.Dr_campo_4.Focus()

    End Sub

    Protected Sub Dr_campo_4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_4.SelectedIndexChanged

        If Me.Dr_campo_4.SelectedValue <> 0 Then

            Me.Dr_campo_5.Enabled = True
            Me.Dr_campo_5.CssClass = "clsMandatorio"

            Me.Dr_campo_4.Enabled = False
            Me.Dr_campo_4.CssClass = "clsDisabled"

            Me.Dr_campo_4.Visible = False
            Label4.Text = Me.Dr_campo_4.SelectedItem.Text
            Label4.Visible = True
            Label4.Width = 100%

            HiddenField5.Value = Me.Dr_campo_4.SelectedIndex

            setea_cabeceras(Me.Dr_campo_4.SelectedValue, Label4)

            For i = 0 To Me.Dr_campo_4.Items.Count - 1
                For x = 5 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_4.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_4.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)
                Next
            Next

        End If


        '      Me.Dr_campo_5.Focus()


    End Sub

    Protected Sub Dr_campo_5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_5.SelectedIndexChanged

        If Me.Dr_campo_5.SelectedValue <> 0 Then

            Me.Dr_campo_6.Enabled = True
            Me.Dr_campo_6.CssClass = "clsMandatorio"

            Me.Dr_campo_5.Enabled = False
            Me.Dr_campo_5.CssClass = "clsDisabled"

            Me.Dr_campo_5.Visible = False
            Label5.Text = Me.Dr_campo_5.SelectedItem.Text
            Label5.Visible = True
            Label5.Width = 100%

            setea_cabeceras(Me.Dr_campo_5.SelectedValue, Label5)

            HiddenField6.Value = Me.Dr_campo_5.SelectedIndex

            For i = 0 To Me.Dr_campo_5.Items.Count - 1
                For x = 6 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_5.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_5.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)
                Next
            Next

        End If

        '      Me.Dr_campo_6.Focus()


    End Sub

    Protected Sub Dr_campo_6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_6.SelectedIndexChanged

        If Me.Dr_campo_6.SelectedValue <> 0 Then

            Me.Dr_campo_7.Enabled = True
            Me.Dr_campo_7.CssClass = "clsMandatorio"

            Me.Dr_campo_6.Enabled = False
            Me.Dr_campo_6.CssClass = "clsDisabled"

            Me.Dr_campo_6.Visible = False
            Label6.Text = Me.Dr_campo_6.SelectedItem.Text
            Label6.Visible = True
            Label6.Width = 100%

            setea_cabeceras(Me.Dr_campo_6.SelectedValue, Label6)

            HiddenField7.Value = Me.Dr_campo_6.SelectedIndex

            For i = 0 To Me.Dr_campo_6.Items.Count - 1
                For x = 7 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_6.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_6.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)
                Next
            Next


        End If


        '   Me.Dr_campo_7.Focus()

    End Sub

    Protected Sub Dr_campo_7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_7.SelectedIndexChanged

        If Me.Dr_campo_7.SelectedValue <> 0 Then

            Me.Dr_campo_8.Enabled = True
            Me.Dr_campo_8.CssClass = "clsMandatorio"


            Me.Dr_campo_7.Enabled = False
            Me.Dr_campo_7.CssClass = "clsDisabled"

            Me.Dr_campo_7.Visible = False
            Label7.Text = Me.Dr_campo_7.SelectedItem.Text
            Label7.Visible = True
            Label7.Width = 100%

            setea_cabeceras(Me.Dr_campo_7.SelectedValue, Label7)

            HiddenField8.Value = Me.Dr_campo_7.SelectedIndex

            For i = 0 To Me.Dr_campo_7.Items.Count - 1
                For x = 8 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_7.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_7.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)
                Next
            Next
        End If

        '   Me.Dr_campo_8.Focus()

    End Sub

    Protected Sub Dr_campo_8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_8.SelectedIndexChanged

        If Me.Dr_campo_8.SelectedValue <> 0 Then

            Me.Dr_campo_9.Enabled = True
            Me.Dr_campo_9.CssClass = "clsMandatorio"


            Me.Dr_campo_8.Enabled = False
            Me.Dr_campo_8.CssClass = "clsDisabled"


            Me.Dr_campo_8.Visible = False
            Label8.Text = Me.Dr_campo_8.SelectedItem.Text
            Label8.Visible = True
            Label8.Width = 100%

            setea_cabeceras(Me.Dr_campo_8.SelectedValue, Label8)


            HiddenField9.Value = Me.Dr_campo_8.SelectedIndex

            For i = 0 To Me.Dr_campo_8.Items.Count - 1
                For x = 9 To 10
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_8.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                       And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                        dr.Items(i).Enabled = True
                    Else
                        dr.Items(Me.Dr_campo_8.SelectedIndex).Enabled = False
                    End If

                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    dr.SelectedItem.Value = Val(hf.Value)
                Next
            Next
        End If

        ' Me.Dr_campo_9.Focus()

    End Sub

    Protected Sub Dr_campo_9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_9.SelectedIndexChanged

        If Hf_tipo_docto.Value <> 3 Then


            Me.Dr_campo_10.Enabled = True
            Me.Dr_campo_10.CssClass = "clsMandatorio"


            Me.Dr_campo_9.Enabled = False
            Me.Dr_campo_9.CssClass = "clsDisabled"

            Me.Dr_campo_9.Visible = False
            Label9.Text = Me.Dr_campo_9.SelectedItem.Text
            Label9.Visible = True
            Label9.Width = 100%

            setea_cabeceras(Me.Dr_campo_9.SelectedValue, Label9)

            HiddenField10.Value = Me.Dr_campo_9.SelectedIndex
        Else



            If Me.Dr_campo_9.SelectedValue <> 0 Then

                Me.Dr_campo_10.Enabled = True
                Me.Dr_campo_10.CssClass = "clsMandatorio"


                Me.Dr_campo_9.Enabled = False
                Me.Dr_campo_9.CssClass = "clsDisabled"

                Me.Dr_campo_9.Visible = False
                Label9.Text = Me.Dr_campo_9.SelectedItem.Text
                Label9.Visible = True
                Label9.Width = 100%

                setea_cabeceras(Me.Dr_campo_9.SelectedValue, Label9)

                HiddenField10.Value = Me.Dr_campo_9.SelectedValue


                For i = 0 To Me.Dr_campo_9.Items.Count - 1
                    For x = 10 To 10
                        Dim dr As DropDownList
                        dr = Page.FindControl("Dr_campo_" & x)


                        If Val(Me.Dr_campo_9.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                           And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                           And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                           And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And Val(HiddenField10.Value) <> i _
                           And Val(HiddenField11.Value) <> i And Val(HiddenField12.Value) <> i And i <> 0 Then

                            dr.Items(i).Enabled = True
                        Else
                            dr.Items(Me.Dr_campo_9.SelectedIndex).Enabled = False
                        End If

                        Dim hf As HiddenField
                        hf = Page.FindControl("HiddenField" & x + 1)
                        dr.SelectedItem.Value = Val(hf.Value)
                    Next
                Next

            End If

        End If
        ' Me.Dr_campo_10.Focus()

    End Sub

    Protected Sub Dr_campo_10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_campo_10.SelectedIndexChanged


        If Me.Dr_campo_10.SelectedValue <> 0 Then


            Me.Dr_campo_10.Enabled = False
            Me.Dr_campo_10.CssClass = "clsDisabled"

            Me.Dr_campo_10.Visible = False
            Label10.Text = Me.Dr_campo_10.SelectedItem.Text
            Label10.Visible = True
            Label10.Width = 100%

            setea_cabeceras(Me.Dr_campo_10.SelectedValue, Label10)

            HiddenField11.Value = Me.Dr_campo_10.SelectedIndex



        End If

        '  Me.Dr_campo_11.Focus()

    End Sub


#End Region

    Private Sub EiminaReferencias(ByRef Referencias As Object)
        Try
            'Bucle de eliminacion
            Do Until _
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(Referencias) <= 0
            Loop
        Catch
        Finally
            Referencias = Nothing
        End Try
    End Sub

    Protected Sub btn_apb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_apb.Click

        Try






            If Not IsNothing(datasetmasivo) Then



                Coll_DOC = New Collection

                arreglo = New ArrayList

                If Hf_tipo_docto.Value <> 3 Then

                    If Dr_campo_0.SelectedValue = 0 Or _
                        Dr_campo_1.SelectedValue = 0 Or _
                        Dr_campo_2.SelectedValue = 0 Or _
                        Dr_campo_3.SelectedValue = 0 Or _
                        Dr_campo_4.SelectedValue = 0 Or _
                        Dr_campo_5.SelectedValue = 0 Or _
                        Dr_campo_6.SelectedValue = 0 Or _
                        Dr_campo_7.SelectedValue = 0 Or _
                        Dr_campo_8.SelectedValue = 0 Then

                        msj.Mensaje(Me, "Atención", "Debe configurar la cabecera completa para poder aprobar", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub

                    End If

                    For i = 0 To Me.GridView1.Rows.Count - 1


                        asigna_campos(1, datasetmasivo.Tables(0).Rows(i).Item(0))

                        asigna_campos(2, datasetmasivo.Tables(0).Rows(i).Item(1))

                        asigna_campos(3, datasetmasivo.Tables(0).Rows(i).Item(2))

                        asigna_campos(4, datasetmasivo.Tables(0).Rows(i).Item(3))

                        asigna_campos(5, datasetmasivo.Tables(0).Rows(i).Item(4))

                        asigna_campos(6, datasetmasivo.Tables(0).Rows(i).Item(5))


                        asigna_campos(7, datasetmasivo.Tables(0).Rows(i).Item(6))

                        asigna_campos(8, datasetmasivo.Tables(0).Rows(i).Item(7))

                        asigna_campos(9, datasetmasivo.Tables(0).Rows(i).Item(8))





                    Next

                Else
                    If Dr_campo_0.SelectedValue = 0 Or _
                        Dr_campo_1.SelectedValue = 0 Or _
                        Dr_campo_2.SelectedValue = 0 Or _
                        Dr_campo_3.SelectedValue = 0 Or _
                        Dr_campo_4.SelectedValue = 0 Or _
                        Dr_campo_5.SelectedValue = 0 Or _
                        Dr_campo_6.SelectedValue = 0 Or _
                        Dr_campo_7.SelectedValue = 0 Or _
                        Dr_campo_8.SelectedValue = 0 Or _
                        Dr_campo_9.SelectedValue = 0 Or _
                        Dr_campo_10.SelectedValue = 0 Then

                        msj.Mensaje(Me, "Atención", "Debe configurar la cabecera completa para poder aprobar", ClsMensaje.TipoDeMensaje._Exclamacion)
                        Exit Sub

                    End If
                    For i = 0 To datasetmasivo.Tables(0).Rows.Count - 1

                        IDX = i

                        asigna_campos(1, datasetmasivo.Tables(0).Rows(i).Item(0))

                        asigna_campos(2, datasetmasivo.Tables(0).Rows(i).Item(1))

                        asigna_campos(3, datasetmasivo.Tables(0).Rows(i).Item(2))

                        asigna_campos(4, datasetmasivo.Tables(0).Rows(i).Item(3))

                        asigna_campos(5, datasetmasivo.Tables(0).Rows(i).Item(4))

                        asigna_campos(6, datasetmasivo.Tables(0).Rows(i).Item(5))



                        asigna_campos(8, datasetmasivo.Tables(0).Rows(i).Item(7))

                        asigna_campos(9, datasetmasivo.Tables(0).Rows(i).Item(8))

                        asigna_campos(10, datasetmasivo.Tables(0).Rows(i).Item(9))

                        asigna_campos(11, datasetmasivo.Tables(0).Rows(i).Item(10))
                        asigna_campos(7, datasetmasivo.Tables(0).Rows(i).Item(6))



                    Next

                End If



            Else
                msj.Mensaje(Me, "Atención", "Debe tener cargado un archivo para poder Procesar", 2, , False)
                Exit Sub

            End If


            Me.btn_guardar.Enabled = True
            msj.Mensaje(Me, "Atención", "Archivo fue procesado con exito , ahora puede guardar", 2, , False)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' Al presionar este boton se realizara el upload del archivo que se haya elegido
    Protected Sub ib_cargar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_cargar.Click


        Descripcion = Server.MapPath("") + FileUpload1.FileName
        Dim ruta As String
        Try

            If (FileUpload1.HasFile) Then

                ruta = Server.MapPath("Archivos_carga_masiva") & "\" & "masiva.xls"
                ruta = ruta.Replace("rightframe_archivos\", "")
                FileUpload1.SaveAs(ruta)

                lb_archivo.Text = "Esta trabajando con el archivo " & FileUpload1.FileName

            Else


                Exit Sub


            End If

            Dim ADAPTADOR1 As New OleDbDataAdapter
            datasetmasivo = New DataSet
            Dim col As New Collection
            Dim cadenaCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & ruta & ";" & "Extended Properties='Excel 8.0;HDR=yes;IMEX=1'"
            Dim con As OleDbConnection = New OleDbConnection(cadenaCon)
            con.Open()

            ADAPTADOR1 = New OleDbDataAdapter("SELECT * FROM  [Hoja1$] ", con)

            datasetmasivo = New DataSet

            ADAPTADOR1.Fill(datasetmasivo, "XLData")

            Dim cant_col As Integer
            cant_col = CInt(DirectCast(DirectCast(datasetmasivo.Tables(0), System.Data.DataTable).Columns, System.Data.DataColumnCollection).Count)

            Dim array As New ArrayList


            Hf_tipo_docto.Value = 1
            cabecera_grilla.Width = 130 * CInt(DirectCast(DirectCast(datasetmasivo.Tables(0), System.Data.DataTable).Columns, System.Data.DataColumnCollection).Count)

            Me.cabecera_grilla.Visible = True

            For i = 0 To DirectCast(DirectCast(datasetmasivo.Tables(0), System.Data.DataTable).Columns, System.Data.DataColumnCollection).Count - 2

                Dim lbl As Label
                Dim str_lbl As String
                str_lbl = "Label" & i
                lbl = Me.FindControl(str_lbl)
                lbl.Visible = True

                lbl.Text = DirectCast(DirectCast(DirectCast(datasetmasivo.Tables(0), System.Data.DataTable).Columns, System.Data.DataColumnCollection).Item(i), System.Data.DataColumn).Caption

            Next


            For i = 0 To datasetmasivo.Tables(0).Rows.Count - 1


                If datasetmasivo.Tables(0).Rows(i).IsNull(0) = True Then


                    array.Add(i)


                End If

            Next


            For i = 0 To array.Count - 1

                datasetmasivo.Tables(0).Rows(array.Item(i)).Delete()
                datasetmasivo.Tables(0).GetChanges()
            Next


            If cant_col = 9 Then
                Hf_tipo_docto.Value = 1
            End If


            If Hf_tipo_docto.Value = 3 Then



                If datasetmasivo.Tables(0).Columns.Count > 11 Then
                    lb_archivo.Text = ""
                    msj.Mensaje(Me, "Atención", "El archivo no corresponde a este tipo de Documento", ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Sub

                End If

                OP.Columna_Archivo_Devuelve(Dr_campo_0, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_1, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_2, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_3, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_4, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_5, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_6, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_7, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_8, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_9, 1)
                OP.Columna_Archivo_Devuelve(Dr_campo_10, 1)
                Dr_campo_9.Visible = True
                Dr_campo_10.Visible = True

                cabecera_grilla.Width = 130 * 11

            Else
                If datasetmasivo.Tables(0).Columns.Count = 11 Then
                    lb_archivo.Text = ""
                    msj.Mensaje(Me, "Atención", "El archivo no corresponde a este tipo de Documento", ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Sub
                End If

                OP.Columna_Archivo_Devuelve(Dr_campo_0, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_1, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_2, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_3, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_4, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_5, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_6, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_7, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_8, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_9, 2)
                OP.Columna_Archivo_Devuelve(Dr_campo_10, 2)
                Dr_campo_9.Visible = False
                Dr_campo_10.Visible = False

                cabecera_grilla.Width = 130 * 9
            End If


            GridView1.DataSource = datasetmasivo.Tables(0)
            GridView1.DataBind()

            lb_archivo1.Text = "El Archivo tiene " & Me.GridView1.Rows.Count & " Registros"

            For i = 0 To Me.GridView1.Rows.Count - 1

                For x = 0 To Me.GridView1.Rows(i).Cells.Count - 1



                    Me.GridView1.Rows(i).Cells(x).Width = 130
                    Me.GridView1.Rows(i).Cells(x).HorizontalAlign = HorizontalAlign.Center





                    'If IsDate(GridView1.Rows(i).Cells(x).Text.Replace(",", ".")) Then

                    '    GridView1.Rows(i).Cells(x).Text = Format(CDate(GridView1.Rows(i).Cells(x).Text), "dd/MM/yyyy")
                    'End If
                Next



            Next

            con.Close()

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion)
        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click




        If Hf_tipo_docto.Value <> 3 Then

            For i = 0 To 8
                Dim dr As DropDownList
                Dim lbl As System.Web.UI.WebControls.Label
                dr = Page.FindControl("Dr_campo_" & i)
                lbl = Page.FindControl("Label" & i)

                If i = 0 Then
                    dr.Enabled = True
                    dr.CssClass = "clsMandatorio"
                Else
                    dr.Enabled = False
                    dr.CssClass = "clsDisabled"


                End If

                lbl.Visible = False
                dr.Visible = True
            Next




            OP.Columna_Archivo_Devuelve(Dr_campo_0, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_1, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_2, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_3, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_4, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_5, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_6, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_7, 2)
            OP.Columna_Archivo_Devuelve(Dr_campo_8, 2)
            ' OP.Columna_Archivo_Devuelve(Dr_campo_9)
            ' OP.Columna_Archivo_Devuelve(Dr_campo_10)


        Else


            For i = 0 To 10
                Dim dr As DropDownList
                Dim lbl As System.Web.UI.WebControls.Label
                dr = Page.FindControl("Dr_campo_" & i)
                lbl = Page.FindControl("Label" & i)

                If i = 0 Then
                    dr.Enabled = True
                    dr.CssClass = "clsMandatorio"
                Else
                    dr.Enabled = False
                    dr.CssClass = "clsDisabled"


                End If

                lbl.Visible = False
                dr.Visible = True
            Next

            '                                                                                                                                                                                                                                                                                                                                                                   
            OP.Columna_Archivo_Devuelve(Dr_campo_0, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_1, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_2, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_3, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_4, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_5, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_6, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_7, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_8, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_9, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_10, 1)


        End If


        For i = 1 To 12
            Dim hf As HiddenField

            hf = Page.FindControl("HiddenField" & i)

            hf.Value = ""
        Next

    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        Try

            'Using ts = New Transactions.TransactionScope


            'OP.documento_masivo_inserta(Coll_DOC, RUT_CLI_RPT, Hf_tipo_docto.Value)
            '   OP.cabeceras_documento_marca(ID_OPE_RPT)
            '  OP.Operación_masiva_cuadra(ID_OPE_RPT)

            msj.Mensaje(Me, "Atención", "Se ha realizado la Integración", 2)

            'ts.Complete()

            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        Dim ADAPTADOR1 As New OleDbDataAdapter
        Dim datasetmasivo As New DataSet
        Dim col As New Collection
        Dim cadenaCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & Descripcion & " ;" & "Extended Properties='Excel 8.0;HDR=yes;IMEX=1'"
        Dim con As OleDbConnection = New OleDbConnection(cadenaCon)
        con.Close()
        con.Open()

        ADAPTADOR1 = New OleDbDataAdapter("SELECT * FROM  [Hoja1$]", con)

        datasetmasivo = New DataSet

        ADAPTADOR1.Fill(datasetmasivo, "XLData")

        Me.GridView1.DataSource = datasetmasivo
        Me.GridView1.DataBind()

        con.Close()

    End Sub

    Public Function calcula_vcto_real(ByVal fecha As Date, ByVal sucursal As Integer, ByVal plaza As String, ByVal tipo As Integer) As Date

        Dim col As New Collection

        col = CMC.DiasDeRetencionDevuelve(sucursal, plaza, tipo)

        Dim DIAS_POR_PLAZA As Integer
        Dim BUSCA_DIA_HABIL As Boolean

        If Not IsNothing(col) Then
            If col.Count > 0 Then


                DIAS_POR_PLAZA = IIf(IsDBNull(col.Item(1)), 0, col.Item(1))
                If IsDBNull(col.Item(2)) Then
                    BUSCA_DIA_HABIL = False
                Else
                    BUSCA_DIA_HABIL = IIf(Trim(col.Item(2)) = "S", True, False)
                End If
            Else
                BUSCA_DIA_HABIL = True
                DIAS_POR_PLAZA = 0
            End If

        End If

        'FECHA_VCTO_AUX = fecha

        'FECHA_VCTO_AUX = DateAdd("D", DIAS_POR_PLAZA, FECHA_VCTO_AUX)
        'If BUSCA_DIA_HABIL Then
        '    FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_AUX)
        'End If

        'If Not IsNothing(col) Then
        '    If col.Count > 0 Then

        '        DIAS_POR_PLAZA = IIf(IsDBNull(col.Item(1)), 0, col.Item(1))
        '        If IsDBNull(col.Item(2)) Then
        '            BUSCA_DIA_HABIL = False
        '        Else
        '            BUSCA_DIA_HABIL = IIf(Trim(col.Item(2)) = "S", True, False)
        '        End If
        '    Else
        '        BUSCA_DIA_HABIL = True
        '        DIAS_POR_PLAZA = 0
        '    End If

        'End If

        '-------------------------------------------------------------------------------------

        FECHA_VCTO_AUX = fecha
        'FECHA_VCTO_AUX = cg.DevuelveCalendarioPagoDeudor(Txt_Rut_Deu.Text, FECHA_VCTO_AUX)

        If BUSCA_DIA_HABIL Then
            FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_AUX)
        End If


        FECHA_VCTO_CALCULO = FECHA_VCTO_AUX

        For i = 1 To DIAS_POR_PLAZA

            FECHA_VCTO_CALCULO = DateAdd("D", 1, FECHA_VCTO_CALCULO)

            If BUSCA_DIA_HABIL Then
                FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
            End If
        Next

        If DIAS_POR_PLAZA = 0 Then

            If BUSCA_DIA_HABIL Then
                FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
            End If

        End If

        'Txt_VctoReal.Text = Format(CDate(FECHA_VCTO_AUX), "dd/MM/yyyy")
        'HF_FacVctoCal.Value = Format(CDate(FECHA_VCTO_CALCULO), "dd/MM/yyyy")

        Return FECHA_VCTO_CALCULO

    End Function

    Public Sub asigna_campos(ByVal caso As Integer, ByVal valor As Object)

        Dim CLSCLI As New ClaseClientes
        Dim dr As HiddenField

        If caso = 1 Then

            dsi = New dim_cls

            deu = New deu_cls

        End If

        dr = Page.FindControl("HiddenField" & caso)

        If Hf_tipo_docto.Value <> 4 Then

            Select Case dr.Value

                Case 1
                    dsi.dim_num = valor
                Case 2
                    dsi.deu_ide = Format(CLng(valor), fmt.FMT_RUT)
                Case 3
                    deu.deu_dig_ito = CStr(valor)
                Case 4
                    '4	 Razón Social Deudor

                    deu.deu_rso = IIf(IsDBNull(valor), "", valor)
                Case 5
                    '5:              Direccion()
                    If IsDBNull(valor) Then


                        deu.deu_dml = ""
                    Else

                        If valor = "&nbsp;" Then

                            deu.deu_dml = ""

                        Else

                            deu.deu_dml = IIf(IsDBNull(valor), "", valor)

                        End If


                    End If


                Case 6
                    '6:              Cod(Comuna)
                    If Val(valor) = 0 Then
                        deu.id_cmn = Nothing
                    Else
                        deu.id_cmn = Val(valor)
                    End If

                Case 7

                    '7:              FECHA(Vencimiento)

                    Dim sist As New sis_cls

                    sist = cg.SistemaDevuelve

                    dsi.dim_fev = CDate(valor)
                    'msj.Mensaje(Me, "Atención", "Fecha de Vencimiento de la fila " & IDX & " no debe ser menor al " & Format(CDate(coll_ope.Item(NroRow).opn_fec).AddDays(sist.sis_dia_vto), "dd/MM/yyyy"), ClsMensaje.TipoDeMensaje._Exclamacion)
                    'If CDate(dsi.dim_fev) < CDate(coll_ope.Item(NroRow).opn_fec).AddDays(sist.sis_dia_vto) Then
                    '    msj.Mensaje(Me, "Atención", "Fecha de Vencimiento de la fila " & IDX & " no debe ser menor al " & Format(CDate(coll_ope.Item(NroRow).opn_fec).AddDays(sist.sis_dia_vto), "dd/MM/yyyy"), ClsMensaje.TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If
                    'dsi.dim_fev_rea = calcula_vcto_real(dsi.dim_fev, coll_ope.Item(NroRow).id_suc, dsi.id_pl_000047, Hf_tipo_docto.Value)
                    'dsi.id_p_0011 = 1
                    'Calcular fecha de vcto real
                    dsi.id_ope = ID_OPE_RPT

                Case 8

                    '8:              MONTO(Documento)

                    dsi.dim_mto = CDbl(valor)
                    dsi.dim_mto_fin = CDbl(valor) * (Me.TextBox1.Text / 100)

                    If Hf_tipo_docto.Value <> 3 Then

                        dsi.id_bco = Nothing
                        dsi.id_pl_000047 = Nothing

                    End If


                Case 9

                    '11:             Numero(Cuota)

                    If valor = 0 And coll_ope.Item(1).ope_cuo = "S" Then
                        dsi.dsi_flj = "S"
                        dsi.dsi_flj_num = CDbl(valor)
                    Else
                        dsi.dsi_flj = "N"
                        dsi.dsi_flj_num = CDbl(valor)
                    End If



            End Select





            If caso = 9 Then
                If Not IsNothing(arreglo) Then

                    If arreglo.Contains(dsi.deu_ide) = False Then

                        arreglo.Add(dsi.deu_ide)


                        If IsNothing(cg.DeudorDevuelvePorRut(CInt(dsi.deu_ide))) Then



                            deu.deu_ide = dsi.deu_ide
                            If deu.deu_ide > 50000000 Then
                                deu.id_p_0044 = 2
                            Else
                                deu.id_p_0044 = 1
                            End If


                            ag.DeudorInserta(deu, 0, "")


                        End If



                        If CLSCLI.RelacionClienteDeudorDevuelve(dsi.deu_ide, "A", RUT_CLI_RPT) = False Then
                            Dim ddr As New ddr_cls

                            ddr.deu_ide = dsi.deu_ide
                            ddr.cli_idc = RUT_CLI_RPT

                            ag.ClientesDeudoresInserta(ddr)

                        End If

                    End If



                Else
                    If arreglo.Contains(dsi.deu_ide) = False Then


                        arreglo.Add(dsi.deu_ide)

                        If IsNothing(cg.DeudorDevuelvePorRut(CInt(dsi.deu_ide))) Then



                            deu.deu_ide = dsi.deu_ide




                        End If



                        If CLSCLI.RelacionClienteDeudorDevuelve(dsi.deu_ide, "A", RUT_CLI_RPT) = False Then
                            Dim ddr As New ddr_cls

                            ddr.deu_ide = dsi.deu_ide
                            ddr.cli_idc = RUT_CLI_RPT

                            ag.ClientesDeudoresInserta(ddr)

                        End If


                    End If
                End If
                Coll_DOC.Add(dsi)

            End If

        Else
            Select Case dr.Value

                Case 1
                    dsi.dim_num = valor
                Case 2
                    dsi.deu_ide = Format(CLng(valor), fmt.FMT_RUT)



                Case 3
                    deu.deu_dig_ito = CStr(valor)
                Case 4
                    '4	 Razón Social Deudor

                    deu.deu_rso = IIf(IsDBNull(valor), "", valor)
                Case 5
                    '5:              Direccion()
                    If IsDBNull(valor) Then


                        deu.deu_dml = ""
                    Else

                        If valor = "&nbsp;" Then

                            deu.deu_dml = ""

                        Else

                            deu.deu_dml = IIf(IsDBNull(valor), "", valor)

                        End If


                    End If


                Case 6
                    '6:              Cod(Comuna)
                    If Val(valor) = 0 Then
                        deu.id_cmn = Nothing
                    Else
                        deu.id_cmn = Val(valor)
                    End If

                Case 7

                    '7:              FECHA(Vencimiento)
                    Dim sist As New sis_cls

                    sist = cg.SistemaDevuelve

                    dsi.dim_fev = CDate(valor)
                    'If CDate(dsi.dim_fev) < CDate(coll_ope.Item(NroRow).opn_fec).AddDays(sist.sis_dia_vto) Then
                    '    msj.Mensaje(Me, "Atención", "Fecha de Vencimiento de la fila " & IDX & " no debe ser menor al " & Format(CDate(coll_ope.Item(NroRow).opn_fec).AddDays(sist.sis_dia_vto), "dd/MM/yyyy"), ClsMensaje.TipoDeMensaje._Exclamacion)
                    '    Exit Sub
                    'End If
                    'dsi.dim_fev_rea = calcula_vcto_real(dsi.dsi_fev, coll_ope.Item(NroRow).id_suc, dsi.id_pl_000047, Hf_tipo_docto.Value)
                    ' dsi.id_p_0011 = 1
                    'Calcular fecha de vcto real
                    dsi.id_ope = ID_OPE_RPT

                Case 8

                    '8:              MONTO(Documento)

                    dsi.dsi_mto = CDbl(valor)
                    dsi.dim_mto_fin = CDbl(valor) * (Me.TextBox1.Text / 100)

                    If Hf_tipo_docto.Value <> 3 Then

                        dsi.id_bco = Nothing
                        dsi.id_pl_000047 = Nothing

                    End If



                Case 9

                    '9:             Banco(Deudor)

                    If IsDBNull(valor) Then

                        dsi.id_bco = Nothing

                    Else

                        If Trim(valor) = "" Then
                            dsi.id_bco = Nothing
                        Else
                            dsi.id_bco = CInt(valor)
                        End If
                    End If

                Case 10

                    '10:             Plaza(Deudor)
                    If IsDBNull(valor) Then


                        dsi.id_PL_000047 = Nothing
                    Else
                        If Trim(valor) = "" Then

                            dsi.id_PL_000047 = Nothing

                        Else

                            dsi.id_PL_000047 = Format(CLng(valor), "000000")

                        End If

                    End If


                Case 11

                    '11:             Numero(Cuota)

                    If valor = 0 And coll_ope.Item(1).ope_cuo = "S" Then
                        dsi.dim_flj = "S"
                        dsi.dim_flj_num = CDbl(valor)
                    Else
                        dsi.dim_flj = "N"
                        dsi.dim_flj_num = CDbl(valor)
                    End If



            End Select




            If caso = 11 Then
                If Not IsNothing(arreglo) Then

                    If arreglo.Contains(dsi.deu_ide) = False Then

                        arreglo.Add(dsi.deu_ide)


                        If IsNothing(cg.DeudorDevuelvePorRut(CInt(dsi.deu_ide))) Then



                            deu.deu_ide = dsi.deu_ide

                            ag.DeudorInserta(deu, 0, "")


                        End If



                        If CLSCLI.RelacionClienteDeudorDevuelve(dsi.deu_ide, "A", RUT_CLI_RPT) = False Then
                            Dim ddr As New ddr_cls

                            ddr.deu_ide = dsi.deu_ide
                            ddr.cli_idc = RUT_CLI_RPT

                            ag.ClientesDeudoresInserta(ddr)

                        End If

                    End If



                Else
                    If arreglo.Contains(dsi.deu_ide) = False Then


                        arreglo.Add(dsi.deu_ide)

                        If IsNothing(cg.DeudorDevuelvePorRut(CInt(dsi.deu_ide))) Then



                            deu.deu_ide = dsi.deu_ide

                            ag.DeudorInserta(deu, 0, "")


                        End If



                        If CLSCLI.RelacionClienteDeudorDevuelve(dsi.deu_ide, "A", RUT_CLI_RPT) = False Then
                            Dim ddr As New ddr_cls

                            ddr.deu_ide = dsi.deu_ide
                            ddr.cli_idc = RUT_CLI_RPT

                            ag.ClientesDeudoresInserta(ddr)

                        End If


                    End If
                End If
                Coll_DOC.Add(dsi)
            End If


        End If




    End Sub

    Private Sub setea_cabeceras(ByVal indice As Integer, ByVal lbl As System.Web.UI.WebControls.Label)

        If indice = 1 Then

            lbl.Text = "Nº Docto."

        ElseIf indice = 4 Then

            lbl.Text = "Deudor"

        ElseIf indice = 5 Then

            lbl.Text = "Dir.Deudor"

        ElseIf indice = 6 Then

            lbl.Text = "Comuna"

        ElseIf indice = 7 Then

            lbl.Text = "Fecha Vcto."

        ElseIf indice = 8 Then

            lbl.Text = "Mto.Docto."

        ElseIf indice = 9 Then

            lbl.Text = "Bco Deudor"

        ElseIf indice = 10 Then

            lbl.Text = "Pza Deudor"

        ElseIf indice = 11 Then

            lbl.Text = "Nº Cuota"

        End If


    End Sub

End Class
