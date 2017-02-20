Imports System.Data
Imports FuncionesGenerales.Errores
Imports FuncionesGenerales.Variables
Imports Paridad
Imports ClsSession.ClsSession
Imports CapaDatos
Partial Class Paridad
    Inherits System.Web.UI.Page

    Dim par As New ActualizacionesGenerales
    Dim cg As New ConsultasGenerales
    Dim ses As New ClsSession.ClsSession
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim msj As New ClsMensaje


    Protected Sub Dr_mon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_mon.SelectedIndexChanged
        If Me.Dr_mes.SelectedValue <> 0 And Me.Dr_mon.SelectedValue <> 0 Then
            Me.Btn_con1.Enabled = True
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            cg.ParametrosDevuelve(TablaParametro.Moneda, True, Me.Dr_mon)
            Dr_mon.Items.RemoveAt(1)

            Me.Txt_Ano.Text = Date.Now.Year
            Txt_Ano.Attributes.Add("Style", "TEXT-ALIGN: right")
        End If
    End Sub

    Protected Sub llenadrop()

        Try

            Dim idx As Integer = 0

            For idx = 1 To 12
                Me.Dr_mes.Items.Add(UCase(Format("01/" & Format(idx, "0#") & "/1900", "mmmm")))
                idx = idx + 1
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Sub formato_grilla()
        Dim Formato As String = ""
        Dim FormatoVxC As String = ""
        ' Txt_valor.Text = Format(Dr_mon, Formato)

        For i = 0 To Me.Gv_paridades.Rows.Count - 1

            Dim msk As New AjaxControlToolkit.MaskedEditExtender

            msk = (Me.Gv_paridades.Rows(i).FindControl("Txt_valor_MaskedEditExtender"))
            
            Dim mskVxC As New AjaxControlToolkit.MaskedEditExtender

            mskVxC = (Me.Gv_paridades.Rows(i).FindControl("Txt_valor_MaskedEditExtender_1"))
        

            Select Case Dr_mon.SelectedValue

                Case 2 : FormatoVxC = "999,999.9999" 'FMT.FCMCD4
                Case 3, 4 : FormatoVxC = "999,999.99"
            End Select

            mskVxC.Mask = FormatoVxC


            Select Case Dr_mon.SelectedValue

                Case 2 : Formato = "999,999.9999"  'FMT.FCMCD4
                Case 3, 4 : Formato = "999,999.99"
            End Select


            msk.Mask = Formato

        Next
    End Sub



    Private Sub TraeLargomes()

        Try

            Dim Fecha As String
            Dim fecha2 As String
            Dim mes As String
            Dim año As String


            mes = Me.Dr_mes.SelectedValue
            año = Me.Txt_Ano.Text
            Fecha = Format(CDate("01/" & CStr(mes) & "/" & CStr(año)), "dd/MM/yyyy")

            If mes = 12 Then
                mes = 1
                año = año + 1
            Else
                mes = mes + 1
            End If

            fecha2 = FORMATOFECHA(CLng(mes), CLng(año))

            'Me.parid.cargadatos(Fecha, Me.Dr_mon.SelectedValue, fecha2)
            'If CodigoError = 99 Then
            '   msj.mensaje(me,"Error", MsgError, 1)
            'End If
            'LlenaGrilla(Fecha, fecha2)
            'If CodigoError = 99 Then
            '   msj.mensaje(me,"Error", MsgError, 1)
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Function FORMATOFECHA(ByVal Mes1 As Integer, ByVal AnoFi1 As Integer) As String
        Dim meslargo As String
        Dim fecha As String
        Dim Fech As DateTime
        Fech = "01/" & Mes1 & "/" & AnoFi1
        fecha = Fech.AddMonths(-1)
        'fecha = Format(fecha, "dd/MM/yyyy")
        meslargo = Format(Fech.AddDays(-1))
        FORMATOFECHA = Format(Fech.AddDays(-1), "dd/MM/yyyy")
        meslargo = Format(Fech.AddDays(-1), "dd")
        'LlenaDias(meslargo, fecha)
    End Function

    Private Sub LlenaGrilla(ByVal fecha As String, ByVal fecha2 As String)

        Try
            Dim coll_cons As New Collection
            coll_cons = cg.ParidadPorRangoFechasDevuelve(fecha, Me.Dr_mon.SelectedValue, fecha2)
            Me.Gv_paridades.DataSource = coll_cons
            Me.Gv_paridades.DataBind()
            '   formato_grilla()
        Catch ex As Exception

        End Try

    End Sub

    Public Function FMT_FECHA() As String
        FMT_FECHA = "yyyyMMdd"
    End Function

    Function DevolDia(ByVal fec As Date) As Integer
        Try

            'Devuelve un valor que indica si corresponde a festivos , sabados o domingos


            If fec.DayOfWeek = 0 Then
                Return (1)
            ElseIf fec.DayOfWeek = 6 Then
                Return (2)
            Else
                Dim coll As New Collection
                coll = cg.DevuelveFeriados(CDate(fec))

                If Not IsNothing(coll) Then
                    If fec = coll.Item(1) Then

                        Return (3)
                    End If
                End If



            End If
        Catch ex As Exception

        End Try

    End Function

    Private Sub LlenaDias()



        Dim Coll_dia As New Collection
        Dim Coll_Par As New Collection
        Dim col As New Collection
        Dim obj As New par_cls
        Dim FC As New FuncionesGenerales.FComunes
        Dim Cart As New DataTable
        Dim Fecha As DateTime
        Dim fecdsd As String
        Dim FecHst As String
        Dim LargoMes As Integer
        Dim I, X As Integer

        Try

            'SACA EL PRIMER DIA DEL MES
            fecdsd = "01/" & Format(CLng(Dr_mes.SelectedValue), "00") & "/"
            fecdsd = fecdsd & Txt_Ano.Text
            Fecha = CDate(fecdsd)

            ' fecdsd = Format$(CDate(fecdsd), FMT_FECHA)

            'SACA EL LARGO DEL MES
            LargoMes = Meses1(Dr_mes.SelectedValue, CLng(Txt_Ano.Text))

            'SACA EL ULTIMO DIA DEL MES
            FecHst = CStr(LargoMes) & "/" & CStr(Dr_mes.SelectedValue) & "/"
            FecHst = FecHst & Txt_Ano.Text
            'FecHst = Format$(CDate(FecHst), FMT_FECHA)

            'TRAE LOS DIAS QUE TIENEN DATOS






            'Llena la grilla 


            For I = 1 To LargoMes
                Dim fec_corta As String
                obj = New par_cls

                Dim dr As DataRow = Cart.NewRow
                obj.par_fec = Fecha.ToShortDateString
                fec_corta = FC.RetornaDia(Fecha.DayOfWeek)

                Coll_Par = cg.ParidadPorRangoFechasDevuelve(CDate(fecdsd), Dr_mon.SelectedValue, CDate(FecHst))
                'If CodigoError = 99 Then
                '   msj.mensaje(me,"Error", MsgError, 1)
                'End If
                'recorre datos guardados y los compara con los actuales para mostrar los valores que correspondan


                If IsNothing(Coll_Par) = False Then


                    For X = 1 To Coll_Par.Count
                        If Fecha = Coll_Par.Item(X).fechapar Then
                            Me.Gv_paridades.FindControl("txt_valor")
                            If IsDBNull(Coll_Par.Item(X).valor()) Or Coll_Par.Item(X).valor() = 0 Then
                                obj.par_val = 0
                            Else
                                obj.par_val = Coll_Par.Item(X).valor()
                            End If

                            If IsDBNull(Coll_Par.Item(X).valorcob()) Or Coll_Par.Item(X).valorcob() = 0 Then
                                obj.par_val_cob = 0
                            Else
                                obj.par_val_cob = Coll_Par.Item(X).valorcob
                            End If

                        End If
                    Next
                    If obj.par_val = 0 Then
                        'obj.par_val = ("0,0000")


                    End If
                    If obj.par_val_cob = 0 Then
                        'obj.par_val_cob = ("0,0000")

                    End If
                    Coll_dia.Add(obj)
                    col.Add(fec_corta)

                    Fecha = Fecha.AddDays(1)

                ElseIf Me.Dr_mon.SelectedValue = 1 Then
                    obj.par_val = 0
                    obj.par_val_cob = 0
                    Coll_dia.Add(obj)
                    col.Add(fec_corta)
                    Fecha = Fecha.AddDays(1)
                ElseIf Me.Dr_mon.SelectedValue = 2 Then
                    obj.par_val = "0"
                    obj.par_val_cob = "0"
                    Coll_dia.Add(obj)
                    col.Add(fec_corta)
                    Fecha = Fecha.AddDays(1)
                ElseIf Me.Dr_mon.SelectedValue = 3 Or Me.Dr_mon.SelectedValue = 4 Then
                    obj.par_val = "0"
                    obj.par_val_cob = "0"
                    Coll_dia.Add(obj)
                    col.Add(fec_corta)
                    Fecha = Fecha.AddDays(1)

                End If

            Next
            'Me.parid.llenagrid(Coll_dia)

            Gv_paridades.DataSource = Coll_dia
            Gv_paridades.DataBind()
            'Recorre grilla y marca feriados y festivos
            For I = 0 To Gv_paridades.Rows.Count - 1

                Me.Gv_paridades.Rows(I).Cells(0).Text = col.Item(I + 1)

                DevolDia(Gv_paridades.Rows(I).Cells(1).Text)

                If DevolDia(Gv_paridades.Rows(I).Cells(1).Text) = 1 Then
                    Me.Gv_paridades.Rows(I).Cells(0).BackColor = Drawing.Color.DarkRed
                    Me.Gv_paridades.Rows(I).Cells(1).BackColor = Drawing.Color.DarkRed
                    Me.Gv_paridades.Rows(I).Cells(1).ForeColor = Drawing.Color.White
                    Me.Gv_paridades.Rows(I).Cells(0).ForeColor = Drawing.Color.White
                    Me.Gv_paridades.Rows(I).Cells(1).Font.Bold = True
                    Me.Gv_paridades.Rows(I).Cells(0).Font.Bold = True

                End If

                If DevolDia(Gv_paridades.Rows(I).Cells(1).Text) = 2 Then
                    Me.Gv_paridades.Rows(I).Cells(0).BackColor = Drawing.Color.Moccasin
                    Me.Gv_paridades.Rows(I).Cells(1).BackColor = Drawing.Color.Moccasin
                    Me.Gv_paridades.Rows(I).Cells(1).ForeColor = Drawing.Color.Blue
                    Me.Gv_paridades.Rows(I).Cells(0).ForeColor = Drawing.Color.Blue
                    Me.Gv_paridades.Rows(I).Cells(1).Font.Bold = True
                    Me.Gv_paridades.Rows(I).Cells(0).Font.Bold = True
                End If

                If DevolDia(Gv_paridades.Rows(I).Cells(1).Text) = 3 Then
                    Me.Gv_paridades.Rows(I).Cells(0).BackColor = Drawing.Color.DarkRed
                    Me.Gv_paridades.Rows(I).Cells(1).BackColor = Drawing.Color.DarkRed
                    Me.Gv_paridades.Rows(I).Cells(1).ForeColor = Drawing.Color.White
                    Me.Gv_paridades.Rows(I).Cells(0).ForeColor = Drawing.Color.White
                    Me.Gv_paridades.Rows(I).Cells(1).Font.Bold = True
                    Me.Gv_paridades.Rows(I).Cells(0).Font.Bold = True

                End If

                Dim TXT_OBS As TextBox
                Dim TXT_COB As TextBox


                If Me.Dr_mon.SelectedValue = 1 Then

                    TXT_OBS = Me.Gv_paridades.Rows(I).FindControl("Txt_valor")
                    TXT_COB = Me.Gv_paridades.Rows(I).FindControl("Txt_valcob")
                    TXT_OBS.Attributes.Add("Style", "TEXT-ALIGN: right")
                    TXT_COB.Attributes.Add("Style", "TEXT-ALIGN: right")
                    TXT_COB.Text = Format(CDbl(TXT_COB.Text), FMT.FCMSD)
                    TXT_OBS.Text = Format(CDbl(TXT_OBS.Text), FMT.FCMSD)

                ElseIf Me.Dr_mon.SelectedValue = 2 Then

                    TXT_OBS = Me.Gv_paridades.Rows(I).FindControl("Txt_valor")
                    TXT_COB = Me.Gv_paridades.Rows(I).FindControl("Txt_valcob")

                    If TXT_OBS.Text = "" Then
                        TXT_OBS.Text = "0"
                    End If

                    If TXT_COB.Text = "" Then
                        TXT_COB.Text = "0"
                    End If

                    TXT_OBS.Attributes.Add("Style", "TEXT-ALIGN: right")
                    TXT_COB.Attributes.Add("Style", "TEXT-ALIGN: right")
                    TXT_COB.Text = Format(CDbl(TXT_COB.Text), FMT.FCMCD4)
                    TXT_OBS.Text = Format(CDbl(TXT_OBS.Text), FMT.FCMCD4)

                Else

                    TXT_OBS = Me.Gv_paridades.Rows(I).FindControl("Txt_valor")
                    TXT_COB = Me.Gv_paridades.Rows(I).FindControl("Txt_valcob")

                    If TXT_OBS.Text = "" Then
                        TXT_OBS.Text = "0"
                    End If

                    If TXT_COB.Text = "" Then
                        TXT_COB.Text = "0"
                    End If

                    TXT_OBS.Attributes.Add("Style", "TEXT-ALIGN: right")
                    TXT_COB.Attributes.Add("Style", "TEXT-ALIGN: right")

                    TXT_COB.Text = Format(CDbl(TXT_COB.Text), FMT.FCMCD)
                    TXT_OBS.Text = Format(CDbl(TXT_OBS.Text), FMT.FCMCD)

                End If

            Next

            formato_grilla()
        Catch ex As Exception

        End Try

    End Sub

    Function Meses1(ByVal Mes1 As Integer, ByVal Ano1 As Integer) As Integer
        Try


            Dim Fech As DateTime
            If Mes1 = 12 Then
                Mes1 = 1
                Ano1 = Ano1 + 1
            Else
                Mes1 = Mes1 + 1
            End If
            Fech = "01/" & Mes1 & "/" & Ano1
            Fech = Fech.AddDays(-1)
            Meses1 = Val(Fech)
        Catch ex As Exception

        End Try
    End Function

    Protected Sub Dr_mes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            'Habilita el boton consultar

            If Me.Dr_mes.SelectedIndex <> 0 And Me.Dr_mon.SelectedIndex <> 0 Then
                Me.Btn_con1.Enabled = True
            End If
        Catch ex As Exception

        End Try
        Me.Gv_paridades.DataSource = Nothing
        Me.Gv_paridades.DataBind()
        Me.Gv_paridades.Controls.Clear()
    End Sub

    Protected Sub btn_gua1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20020312, Usr, "PRESIONO GUARDAR PARIDAD ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If

        Dim fecha2 As String
        Dim mes As String
        Dim año As String
        Dim fecha1 As DateTime
        Dim i, x As Integer
        Dim fecha As String
        Dim Coll_Par As New Collection
        Dim valor As TextBox
        Dim valorcobrar As TextBox

        Try

            'calcula el largo del mes
            mes = Me.Dr_mes.SelectedValue
            año = Me.Txt_Ano.Text
            fecha1 = Format(CDate("01/" & CStr(mes) & "/" & CStr(año)), "dd/MM/yyyy")

            If mes = 12 Then
                mes = 1
                año = año + 1
            Else
                mes = mes + 1
            End If

            fecha2 = FORMATOFECHA(CLng(mes), CLng(año))


            'Recorre la grilla haciendo el update o insert correspondiente

            For i = 0 To Gv_paridades.Rows.Count - 1
                fecha = Me.Gv_paridades.Rows(i).Cells(1).Text
                valor = CType(Me.Gv_paridades.Rows(i).Cells(2).Controls(1).FindControl("txt_valor"), TextBox)
                valorcobrar = CType(Me.Gv_paridades.Rows(i).Cells(3).Controls(1).FindControl("txt_valcob"), TextBox)
                par.eliminaparidades(CDate(fecha), Me.Dr_mon.SelectedValue)

                'Reemplaza puntos por comas
                If valor.Text <> "" Then
                    '   valor.Text = Replace(CStr(valor.Text), ",", ".")
                    'valorcobrar.Text = Replace(CStr(valorcobrar.Text), ",", ".")
                Else
                    valor.Text = 0
                    valorcobrar.Text = 0
                End If
                'Compara datos de la grilla con los entregados por la consulta para verificar si se debe hacer insert o update

                Coll_Par = cg.ParidadPorRangoFechasDevuelve(Gv_paridades.Rows(0).Cells(1).Text, Dr_mon.SelectedValue, fecha2)



                Me.par.GuardaParidad(fecha, valor.Text, valorcobrar.Text, Me.Dr_mon.SelectedValue)

                msj.Mensaje(Me, "Atención", "Se han guardado los datos", 2)

            Next

            'Limpia la grilla y la vuelve a llenar con los datos actualizados
            Me.Gv_paridades.Controls.Clear()
            Me.LlenaDias()
            formato_grilla()
            Me.Btn_con1.Enabled = False
            msj.Mensaje(Me, "Información", "Registro Ingresado", 2)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Btn_Buscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_con1.Click
        Dim Agt As New Perfiles.Cls_Principal

        If Not Agt.ValidaAccesso(20, 20010312, Usr, "PRESIONO BUSCAR PARIDAD ") Then
            msj.Mensaje(Me, "Atención", "Acceso denegado", ClsMensaje.TipoDeMensaje._Exclamacion)
            Exit Sub
        End If
        LlenaDias()
        If Me.Dr_mes.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "Debes Seleccionar un Mes", 1)
        End If
        If Me.Dr_mon.SelectedValue = 0 Then
            msj.Mensaje(Me, "Atención", "Debes Seleccionar una Moneda", 1)
        End If

        Me.Btn_Guar.Enabled = True
    End Sub

    Protected Sub btn_limp1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_lim.Click
        Me.Dr_mon.SelectedIndex = 0
        Me.Dr_mes.SelectedIndex = 0
        Me.Gv_paridades.Controls.Clear()
        Me.Gv_paridades.Dispose()
        Me.Btn_con1.Enabled = False
        Me.Btn_Guar.Enabled = False
    End Sub




    Protected Sub Dr_mes_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dr_mes.SelectedIndexChanged

        If Me.Dr_mes.SelectedValue <> 0 And Me.Dr_mon.SelectedValue <> 0 Then
            Me.Btn_con1.Enabled = True
        End If

        Me.Gv_paridades.DataSource = Nothing
        Me.Gv_paridades.DataBind()
        Me.Gv_paridades.Controls.Clear()

    End Sub

   
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        ses.Modulo = "Mantencion"
        Pagina = Page.AppRelativeVirtualPath
        CambioTema(Page)
    End Sub
End Class
