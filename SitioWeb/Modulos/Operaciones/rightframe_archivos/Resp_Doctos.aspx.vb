Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_Resp_Doctos
    Inherits System.Web.UI.Page

    Dim ag As New ActualizacionesGenerales
    Dim cg As New ConsultasGenerales
    Dim OP As New ClaseOperaciones
    Dim rw As New FuncionesGenerales.FComunes
    Dim msj As New ClsMensaje
    Dim fmt As New FuncionesGenerales.ClsLocateInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Response.Expires = -1

            Me.txt_itemope.Value = Val(Trim(Request.QueryString("itemOPE")))
            coll_nrd = New Collection
            cargacheques()

            cargadoctos()

        End If

        ImageButton3.Attributes.Add("onClick", "javascript:window.close();")

    End Sub

    Public Sub cargacheques()

        For i = 1 To coll_ope.Count

            If coll_ope.Item(i).id_ope = txt_itemope.Value Then
                txt_itemope.Value = i
                Exit For
            End If

        Next

        Dim COLL As New Collection

        coll_chr = New Collection

        coll_chr = OP.cheques_respaldo_retorna(coll_ope.Item(Val(Me.txt_itemope.Value)).id_ope)

        Me.GridView1.DataSource = coll_chr
        Me.GridView1.DataBind()


        For i = 0 To Me.GridView1.Rows.Count - 1
            Me.GridView1.Rows(i).Cells(1).Text = Format(CLng(Me.GridView1.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & rw.Vrut(CLng(Me.GridView1.Rows(i).Cells(1).Text))
            Me.GridView1.Rows(i).Cells(2).Text = OP.Cliente_por_cheque_retorna(coll_chr.Item(i + 1).chr_cli_deu, coll_chr.Item(i + 1).chr_tip_cli)

            Me.GridView1.Rows(i).Cells(7).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(6).Text), fmt.FCMSD)
            Me.GridView1.Rows(i).Cells(6).Text = Format(CDbl(Me.GridView1.Rows(i).Cells(6).Text), fmt.FCMSD)
        Next

    End Sub

    Public Sub cargadoctos()

        For i = 1 To coll_ope.Count

            If coll_ope.Item(i).id_ope = txt_itemope.Value Then
                txt_itemope.Value = i
                Exit For
            End If

        Next
        Coll_DSI = New Collection

        Coll_DSI = OP.Documentos_con_respaldo_retorna(coll_ope.Item(Val(Me.txt_itemope.Value)).id_ope)

        Me.GridView2.DataSource = Coll_DSI
        Me.GridView2.DataBind()


        For i = 0 To Me.GridView2.Rows.Count - 1
            Me.GridView2.Rows(i).Cells(1).Text = Format(CLng(Me.GridView2.Rows(i).Cells(1).Text), fmt.FCMSD) & "-" & rw.Vrut(CLng(Me.GridView2.Rows(i).Cells(1).Text))
            Me.GridView2.Rows(i).Cells(4).Text = Format(CDbl(Me.GridView2.Rows(i).Cells(4).Text), fmt.FCMSD)
        Next


    End Sub

    Protected Sub Ch_Cheque_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim c As Integer = 9999999

        If Me.GridView1.Rows.Count = 0 Then
            msj.Mensaje(Me, "Atención", "No existen cheques para asociar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        If Me.GridView1.Rows.Count = 0 Then
            msj.Mensaje(Me, "Atención", "No existen documentos para asociar", ClsMensaje.TipoDeMensaje._Informacion)
            Exit Sub
        End If

        For i = 0 To Me.GridView1.Rows.Count - 1


            Dim ch As CheckBox

            ch = Me.GridView1.Rows(i).FindControl("Ch_Cheque")

            'Verifica que solo quede habilitado el cheque seleccionado

            If ch.Checked = True Then
                c = i
                Me.pos_chr.Value = c

                'Consulta la Persona que emitio el cheque , si es Deudor , bloquea todos los Documentos que no le pertenezcan
                'En caso que sea cliente , todos quedan aptos para ser cubiertos .

                If coll_chr.Item(i + 1).chr_tip_cli = "D" Then

                    For X = 0 To Me.GridView2.Rows.Count - 1

                        If Trim(Me.GridView2.Rows(X).Cells(1).Text) <> Trim(Me.GridView1.Rows(i).Cells(1).Text) Then

                            Me.GridView2.Rows(X).Enabled = False

                        End If

                    Next

                End If
            Else
                Me.GridView1.Rows(i).Enabled = False

            End If

        Next

        If c = 9999999 Then

            cargacheques()

            cargadoctos()

        End If


    End Sub

    Protected Sub Ch_doc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim monto_disp As Double
        Dim mto_doc As Double
        Dim mto_tot As Double
        Dim mto_ch As Double
        For x = 0 To Me.GridView1.Rows.Count - 1
            Dim chd As CheckBox
            chd = Me.GridView1.Rows(x).FindControl("Ch_Cheque")
            Me.GridView1.Rows(x).Cells(7).Text = Me.GridView1.Rows(x).Cells(6).Text

            If chd.Checked = True Then


                For i = 0 To Me.GridView2.Rows.Count - 1
                    Dim ch As CheckBox
                    ch = Me.GridView2.Rows(i).FindControl("Ch_doc")



                    If ch.Checked = True Then

                        'Validar Montos
                        mto_doc = Me.GridView2.Rows(i).Cells(4).Text
                        mto_ch = Me.GridView1.Rows(x).Cells(6).Text
                        monto_disp = Me.GridView1.Rows(x).Cells(6).Text - mto_tot
                        If mto_doc <= monto_disp Then
                            mto_tot = mto_tot + mto_doc
                            monto_disp = Me.GridView1.Rows(x).Cells(6).Text - mto_tot
                            Me.GridView1.Rows(x).Cells(7).Text = monto_disp
                        Else
                            msj.Mensaje(Me, "Atención", "Saldo del cheque no es suficiente para cubrir este documento", ClsMensaje.TipoDeMensaje._Exclamacion)
                            ch.Checked = False

                        End If

                    End If


                Next
            Else




            End If



        Next




    End Sub


    Protected Sub btn_asoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim insert As Boolean = False
            For i = 0 To Me.GridView2.Rows.Count - 1
                Dim ch As CheckBox
                ch = Me.GridView2.Rows(i).FindControl("Ch_doc")
                If ch.Checked Then
                    Dim nr As New nrd_cls

                    With nr

                        .id_chr = coll_chr.Item(Val(Me.pos_chr.Value + 1)).id_chr
                        .id_dsi = Coll_DSI.Item(i + 1).id_dsi
                        .mto_resp = Coll_DSI.Item(i + 1).dsi_mto
                    End With
                    OP.cheques_respaldo_asocia(nr)
                    insert = True
                End If

            Next

            If insert = True Then
                coll_nrd = New Collection
                cargacheques()

                cargadoctos()
                msj.Mensaje(Me, "Atención", "Documentos asociados correctamente", ClsMensaje.TipoDeMensaje._Informacion)

            Else
                msj.Mensaje(Me, "Atención", "No se han  asociado Documentos", ClsMensaje.TipoDeMensaje._Informacion)
            End If


        Catch ex As Exception

        End Try

    End Sub


    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Me.GridView1.Controls.Clear()
        Me.GridView2.Controls.Clear()

        coll_nrd = New Collection
        cargacheques()

        cargadoctos()

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not IsPostBack Then
            Modulo = "Operacion"
            Pagina = Page.AppRelativeVirtualPath
            CambioTema(Page)
        End If
    End Sub
End Class
