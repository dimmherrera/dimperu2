Imports ClsSession.ClsSession
Imports FuncionesGenerales.RutinasWeb

Partial Class WebControles_Menu_Sup
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            GeneraMenu()
        End If

    End Sub

    Private Sub GeneraMenu()

        Dim agt As New Perfiles.Cls_Principal
        Dim Arr As ArrayList
        Dim msj As New ClsMensaje

        Try

            Arr = agt.ObjetoRetornaPuertasHabitadas("20", "20000000", Usr)

            For Each MenuItem In Arr

                Select Case MenuItem
                    Case "00000"
                        Response.Redirect("~/FinSesion.aspx")
                    Case "20000001"
                        adm.Visible = True

                        Dim Arr1 As ArrayList

                        Arr1 = agt.ObjetoRetornaPuertasHabitadas("20", "20000001", Usr)

                        For Each MenuItem1 In Arr1
                            Select Case MenuItem1
                                Case "20000101"
                                    adm1.Visible = True
                                Case "20000201"
                                    adm2.Visible = True
                                Case "20000301"
                                    adm3.Visible = True
                                Case "20000401"
                                    adm4.Visible = True
                                Case "20000501"
                                    adm5.Visible = True
                            End Select

                        Next

                    Case "20000002"
                        lin.Visible = True

                    Case "20000003"
                        vb.Visible = True

                        Dim Arr2 As ArrayList

                        Arr2 = agt.ObjetoRetornaPuertasHabitadas("20", "20000003", Usr)

                        For Each MenuItem2 In Arr2

                            Select Case MenuItem2
                                Case "20000103"
                                    vb1.Visible = True
                                Case "20000203"
                                    vb2.Visible = True
                                Case "20000303"
                                    vb3.Visible = True
                                Case "20000403"
                                    vb4.Visible = True
                                Case "20000503"
                                    vb5.Visible = True
                                Case "20000603"
                                    vb6.Visible = True
                                Case "20000703"
                                    'vb7.Visible = True
                            End Select

                        Next


                    Case "20000004"
                        com.Visible = True
                        Dim Arr3 As ArrayList

                        Arr3 = agt.ObjetoRetornaPuertasHabitadas("20", "20000004", Usr)

                        For Each MenuItem3 In Arr3

                            Select Case MenuItem3

                                Case "20000104"
                                    com1.Visible = True
                                Case "20000204"
                                    com2.Visible = True
                                Case "20000304"
                                    com3.Visible = True
                                Case "20000404"
                                    com4.Visible = True
                                Case "20000504"
                                    com5.Visible = True
                                Case "20000604"
                                    com6.Visible = True

                            End Select

                        Next


                    Case "20000005"
                        ope.Visible = True

                        Dim Arr4 As ArrayList

                        Arr4 = agt.ObjetoRetornaPuertasHabitadas("20", "20000005", Usr)

                        For Each MenuItem4 In Arr4

                            Select Case MenuItem4

                                Case "20000105"
                                    ope1.Visible = True
                                Case "20000205"
                                    ope2.Visible = True
                                Case "20000305"
                                    ope3.Visible = True
                                Case "20000405"
                                    ope4.Visible = True
                                Case "20000505"
                                    ope5.Visible = True
                                Case "20000605"
                                    ope6.Visible = True
                                Case "20000705"
                                    ope7.Visible = True
                                Case "20000805"
                                    ope8.Visible = True
                                Case "20000905"
                                    ope9.Visible = True
                                Case "20001005"
                                    ope10.Visible = True
                                Case "20000107"
                                    ope11.Visible = True
                            End Select

                        Next


                    Case "20000006"
                        leg.Visible = True
                        Dim Arr5 As ArrayList

                        Arr5 = agt.ObjetoRetornaPuertasHabitadas("20", "20000006", Usr)

                        For Each MenuItem5 In Arr5

                            Select Case MenuItem5

                                Case "20000106"
                                    leg1.Visible = True
                                Case "20000206"
                                    leg2.Visible = True
                                Case "20000306"
                                    leg3.Visible = True
                                Case "20000406"
                                    leg4.Visible = True

                            End Select

                        Next


                    Case "20000007"
                        cob.Visible = True
                        Dim Arr6 As ArrayList

                        Arr6 = agt.ObjetoRetornaPuertasHabitadas("20", "20000007", Usr)

                        For Each MenuItem6 In Arr6

                            Select Case MenuItem6

                                Case "20000107"
                                    'cob1.Visible = True
                                Case "20000207"
                                    cob2.Visible = True
                                Case "20000307"
                                    cob3.Visible = True
                                Case "20000407"
                                    cob4.Visible = True
                                Case "20000507"
                                    cob5.Visible = True
                                Case "20000607"
                                    cob6.Visible = True
                                Case "20000707"
                                    cob7.Visible = True

                            End Select

                        Next

                    Case "20000008"
                        rec.Visible = True
                        Dim Arr7 As ArrayList

                        Arr7 = agt.ObjetoRetornaPuertasHabitadas("20", "20000008", Usr)

                        For Each MenuItem7 In Arr7

                            Select Case MenuItem7

                                Case "20000108"
                                    rec1.Visible = True
                                Case "20000208"
                                    rec2.Visible = True
                                Case "20000308"
                                    rec3.Visible = True
                                Case "20000408"
                                    rec4.Visible = True
                                Case "20000508"
                                    rec5.Visible = True
                            End Select

                        Next


                    Case "20000009"
                        pro.Visible = True

                        Dim Arr8 As Object

                        Arr8 = agt.ObjetoRetornaPuertasHabitadas("20", "20000009", Usr)

                        For Each MenuItem8 In Arr8

                            Select Case MenuItem8
                                Case "20000109"
                                    pro1.Visible = True
                                Case "20000209"
                                    pro2.Visible = True
                                Case "20000309"
                                    pro3.Visible = True
                            End Select

                        Next


                    Case "20000010"
                        can.Visible = True
                        Dim Arr9 As Object


                        Arr9 = agt.ObjetoRetornaPuertasHabitadas("20", "20000010", Usr)

                        For Each MenuItem9 In Arr9

                            Select Case MenuItem9

                                Case "20000110"
                                    can1.Visible = True
                                Case "20000210"
                                    can2.Visible = True
                                Case "20000310"
                                    can3.Visible = True

                            End Select

                        Next

                    Case "20000011"
                        tes.Visible = True
                        Dim Arr10 As Object

                        Arr10 = agt.ObjetoRetornaPuertasHabitadas("20", "20000011", Usr)

                        For Each MenuItem10 In Arr10

                            Select Case MenuItem10
                                Case "20000111"
                                    tes1.Visible = True
                                Case "20000211"
                                    tes2.Visible = True
                                Case "20000311"
                                    tes3.Visible = True
                            End Select

                        Next

                    Case "20000012"
                        man.Visible = True
                        Dim Arr11 As Object

                        Arr11 = agt.ObjetoRetornaPuertasHabitadas("20", "20000012", Usr)

                        For Each MenuItem11 In Arr11

                            Select Case MenuItem11
                                Case "20000112"
                                    'man1.Visible = True
                                Case "20000212"
                                    'man2.Visible = True
                                Case "20000312"
                                    'man3.Visible = True
                                Case "20000412"
                                    'man4.Visible = True
                                Case "20000512"
                                    'man5.Visible = True
                                Case "20000612"
                                    'man6.Visible = True
                                Case "20000712"
                                    'man7.Visible = True
                                Case "20000812"
                                    'man8.Visible = True
                                Case "20000912"
                                    'man9.Visible = True
                                Case "20001012"
                                    'man10.Visible = True
                                Case "20001112"
                                    'man11.Visible = True
                                Case "20001212"
                                    'man12.Visible = True
                                Case "20001312"
                                    'man13.Visible = True
                                Case "20001412"
                                    'man14.Visible = True
                                Case "20001512"
                                    'man15.Visible = True
                                Case "20001612"
                                    man16.Visible = True
                                    'man17.Visible = True
                                    'man18.Visible = True
                            End Select

                        Next


                    Case "20000013"
                        ges.Visible = True

                End Select

            Next

        Catch ex As Exception
            Response.Write("Error: " & agt.getErrMsg)
            'Response.Write("<br/>")
            'Response.Write("sesion: " & Usr)
        End Try

    End Sub

End Class
