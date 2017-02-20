Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_gastos
    Inherits System.Web.UI.Page
    Private RG As New FuncionesGenerales.FComunes
    Dim ag As New ActualizacionesGenerales
    Dim rw As New FuncionesGenerales.RutinasWeb

    Dim cg As New ConsultasGenerales
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim msj As New ClsMensaje
    Dim CMC As New ClaseComercial
    Dim OP As New ClaseOperaciones

    Protected Sub AgregaGastos()

        Try

            Dim i As Integer
            Dim ch As New CheckBox

            coll_Gto = New Collection
            coll_chr = New Collection

            Me.txt_total.Text = 0
            txt_totales.Text = 0

            CMC.Gastos_Elimina(Me.HF_Id_Opn.Value)

            'Agrega gastos definidos
            For i = 0 To Me.gd_gastdef.Rows.Count - 1

                ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

                If ch.Checked = True Then

                    Dim gd As New gdn_cls
                    Dim Gasto As New ClsGastos


                    Gasto.Código = gd_gastdef.Rows(i).Cells(1).Text
                    Gasto.Descripción = gd_gastdef.Rows(i).Cells(4).Text
                    Gasto.Monto = gd_gastdef.Rows(i).Cells(3).Text
                    Gasto.Tipo = "D"
                    Gasto.GastoPor = gd_gastdef.Rows(i).Cells(6).Text
                    Gasto.AfectoExento = gd_gastdef.Rows(i).Cells(5).Text

                    gd.id_opn = HF_Id_Opn.Value
                    gd.id_gdn = Nothing
                    gd.id_gto = Gasto.Código

                    CMC.GastosDefinidosInserta(gd, HF_Id_Ope.Value, Hf_can_doc.Value, Hf_can_ddr.Value)
                    coll_chr.Add(Gasto)

                    Select Case Gasto.GastoPor
                        Case 1
                            Me.txt_total.Text = CDbl(Val(Me.txt_total.Text)) + CDbl(Gasto.Monto)
                        Case 2
                            Me.txt_total.Text = CDbl(Val(Me.txt_total.Text)) + (CDbl(Gasto.Monto) * Val(Hf_can_ddr.Value))
                        Case 3
                            Me.txt_total.Text = CDbl(Val(Me.txt_total.Text)) + (CDbl(Gasto.Monto) * Val(Hf_can_doc.Value))
                    End Select

                End If

            Next
            Dim cb As CheckBox

            For i = 0 To Me.gr_gastofijo.Rows.Count - 1
                cb = CType(gr_gastofijo.Rows(i).FindControl("Ch_fijos"), CheckBox)

                If cb.Checked = True Then



                    Dim Monto As Double = gr_gastofijo.Rows(i).Cells(3).Text

                    gr_gastofijo.Rows(i).Cells(3).Text = Format(Monto, FMT.FCMSD)

                    Dim gf As New gfn_cls

                    Dim Gasto As New ClsGastos

                    'Gasto.Codigo = gd_gastdef.Rows(i).Cells(1).Text
                    Gasto.Descripción = gr_gastofijo.Rows(i).Cells(2).Text
                    Gasto.Monto = gr_gastofijo.Rows(i).Cells(3).Text
                    Gasto.Tipo = "F"

                    gf.gfn_des = gr_gastofijo.Rows(i).Cells(2).Text
                    gf.gfn_mto = gr_gastofijo.Rows(i).Cells(3).Text
                    gf.id_opn = HF_Id_Opn.Value

                    CMC.GastosFijosInserta(gf, Me.HF_Id_Ope.Value)

                    coll_chr.Add(Gasto)

                    txt_totales.Text = CDbl(Val(Me.txt_totales.Text)) + CDbl(Gasto.Monto)
                End If

            Next


            'LB_TotalGastos.Text = Format(CDbl(Me.txt_total.Text) + CDbl(txt_totales.Text), FMT.FCMSD)
            'Txt_Gastos.Text = Format(CDbl(LB_TotalGastos.Text), FMT.FCMSD)

            OP.OperacionActualizaGastos(Me.HF_Id_Ope.Value, CDbl(Me.txt_total.Text) + CDbl(Me.txt_totales.Text))

            If gr_gastofijo.Rows.Count > 0 Then
                txt_totales.Text = Format(CDbl(txt_totales.Text), FMT.FCMSD)
            End If

            Session("Gastos") = coll_chr

            If gd_gastdef.Rows.Count > 0 Then
                txt_total.Text = Format(CDbl(txt_total.Text), FMT.FCMSD)
            End If

        Catch ex As Exception
            ' Msj(Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub btn_guar_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guar.Click

        Try
            If txt_mto.Text = "" Then
                msj.Mensaje(Page, "Atencion", "Ingrese monto", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            If txt_des.Text = "" Then
                msj.Mensaje(Page, "Atencion", "Ingrese descripción", ClsMensaje.TipoDeMensaje._Exclamacion)
                Exit Sub
            End If

            Dim Gas As New ClsGastos
            Gas.Código = 0
            Gas.Monto = RG.comasXptos(Me.txt_mto.Text)
            Gas.Descripción = Me.txt_des.Text
            Gas.Tipo = "F"

            'coll_Gto = New Collection

            '   Me.txt_totales.Text = CDbl(Val(Me.txt_totales.Text)) + RG.comasXptos(Me.txt_mto.Text)

            If Not IsNothing(Session("GastosFijos")) Then
                coll_Gto = Session("GastosFijos")
            End If

            coll_Gto.Add(Gas)

            Session("GastosFijos") = coll_Gto

            Me.gr_gastofijo.DataSource = Session("GastosFijos")
            Me.gr_gastofijo.DataBind()

            For i = 0 To gr_gastofijo.Rows.Count - 1
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)
            Next


            txt_totales.Text = ""

            Me.txt_des.Text = ""
            Me.txt_mto.Text = ""



        Catch ex As Exception

        End Try

    End Sub

    Private Function GrabaGastos() As Boolean

        Try

            If IsNothing(Session("Gastos")) Then
                Return True
            End If

            Dim Gasto As ClsGastos

            coll_Gto = Session("Gastos")

            CMC.Gastos_Elimina(HF_Id_Opn.Value)

            For I = 1 To coll_Gto.Count

                Gasto = coll_Gto.Item(I)

                Select Case Gasto.Tipo
                    Case "F" 'Fijos

                        Dim gfn As New gfn_cls

                        gfn.id_gfn = Nothing
                        gfn.id_opn = HF_Id_Opn.Value
                        gfn.gfn_mto = Gasto.Monto
                        gfn.gfn_des = Gasto.Descripción

                        If CMC.GastosFijosInserta(gfn, Val(HF_Id_Ope.Value)) Then

                        End If

                    Case "D" 'Definidos
                        Dim cant_deu As Integer
                        Dim gdn As New gdn_cls

                        gdn.id_gdn = Nothing
                        gdn.id_opn = HF_Id_Opn.Value
                        gdn.id_gto = Gasto.Código


                        cant_deu = OP.CANTIDAD_DEUDORES_DEVUELVE(HF_Id_Ope.Value)

                        If CMC.GastosDefinidosInserta(gdn, Val(HF_Id_Ope), Val(Hf_can_doc.Value), cant_deu) Then

                        End If

                End Select


            Next

            Return True

        Catch ex As Exception
            ' Msj(Caption, ex.Message, TipoDeMensaje._Error)
            Return False
        End Try

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Expires = -1
        If Not Me.IsPostBack Then
            txt_totales.Attributes.Add("style", "text-align: right")
            txt_total.Attributes.Add("style", "text-align: right")
            txt_mto.Attributes.Add("style", "text-align: right")

            Dim cant_Deu As Integer


            HF_Id_Ope.Value = Request.QueryString("id_ope")
            HF_Id_Opn.Value = Request.QueryString("id_opn")

            cant_Deu = OP.CANTIDAD_DEUDORES_DEVUELVE(HF_Id_Ope.Value)

            Hf_can_doc.Value = Request.QueryString("can_doc")
            Hf_can_ddr.Value = cant_Deu

            'CargaGastos()
            resc_gastos()

        End If

        btn_volver.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$LB_RefrescaGastos');")

    End Sub

    Private Sub resc_gastos()

        Dim Def As Object
        Dim Fij As Object
        Dim Gastos As ClsGastos

        Def = CMC.GastosDeNegociacionDevuelve(HF_Id_Opn.Value, 1)
        Fij = CMC.GastosDeNegociacionDevuelve(HF_Id_Opn.Value, 2)

        Me.gd_gastdef.DataSource = CMC.GastosDefinidosDevuelve(Sucursal)
        Me.gd_gastdef.DataBind()

        If Not IsNothing(Def) Then

            For Each D In Def

                Gastos = New ClsGastos
                Gastos.Código = D.id_gto
                Gastos.Descripción = D.gto_des
                Gastos.Monto = D.gto_mto
                Gastos.Tipo = "D"
                Gastos.AfectoExento = D.AfectoExento

                For i = 0 To Me.gd_gastdef.Rows.Count - 1

                    If Me.gd_gastdef.Rows(i).Cells(1).Text = Gastos.Código Then

                        Dim ch As CheckBox
                        ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

                        ch.Checked = True
                        Exit For

                    End If

                Next

            Next

        End If

        For i = 0 To Me.gd_gastdef.Rows.Count - 1

            If Me.txt_total.Text = "" Then
                Me.txt_total.Text = 0
            End If

            Me.txt_total.Text = CDbl(Me.txt_total.Text) + CDbl(gd_gastdef.Rows(i).Cells(3).Text)

            gd_gastdef.Rows(i).Cells(3).Text = Format(CDbl(gd_gastdef.Rows(i).Cells(3).Text), FMT.FCMSD)

            If gd_gastdef.Rows(i).Cells(5).Text = "S" Then
                gd_gastdef.Rows(i).Cells(5).Text = "SI"
            Else
                gd_gastdef.Rows(i).Cells(5).Text = "NO"
            End If

        Next

        'Me.txt_total.Text = Format(CDbl(Me.txt_total.Text), FMT.FCMSD) como encuentra las tablas vacia se cae lo comento hasta encontrar solucion Miguel Herrera


        Dim gastofijo
        gastofijo = New Collection

        For Each F In Fij

            Gastos = New ClsGastos
            Gastos.Código = F.id_gfn
            Gastos.Descripción = F.gfn_des
            Gastos.Monto = F.gfn_mto
            Gastos.Tipo = "F"
            Gastos.AfectoExento = "N"

            gastofijo.Add(Gastos)

            txt_totales.Text = CDbl(Val(Me.txt_totales.Text)) + CDbl(Gastos.Monto)

        Next

        Dim Total_GastosDefenidos As Double
        Dim Total_GastosFijos As Double

        If txt_total.Text.Trim = "" Then
            Total_GastosDefenidos = 0
        Else
            Total_GastosDefenidos = CDbl(Me.txt_total.Text)
        End If

        If txt_totales.Text.Trim = "" Then
            Total_GastosFijos = 0
        Else
            Total_GastosFijos = CDbl(Me.txt_totales.Text)
            txt_totales.Text = Format(CDbl(txt_totales.Text), FMT.FCMSD)
        End If

        Session("GastosFijos") = gastofijo

        coll_Gto = Session("GastosFijos")

        gr_gastofijo.DataSource = Session("GastosFijos")
        gr_gastofijo.DataBind()

        For i = 0 To Me.gr_gastofijo.Rows.Count - 1

            Dim ch As CheckBox
            ch = Me.gr_gastofijo.Rows(i).FindControl("Ch_fijos")

            ch.Checked = True
            gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)

        Next

        FormatoGR()
    End Sub

    Private Sub CargaGastos()

        Try

            Me.gd_gastdef.DataSource = CMC.GastosDefinidosDevuelve(Sucursal)
            Me.gd_gastdef.DataBind()

            Me.gr_gastofijo.DataSource = CMC.GastosDeNegociacionDevuelve(HF_Id_Opn.Value, 2)
            Me.gr_gastofijo.DataBind()

            Me.txt_total.Text = 0
            For I = 0 To gd_gastdef.Rows.Count - 1
                Dim ch As CheckBox

                Dim Monto As Double = gd_gastdef.Rows(I).Cells(3).Text
                gd_gastdef.Rows(I).Cells(3).Text = Format(Monto, FMT.FCMSD)
                If Not IsNothing(arr_gto_def_) Then


                    If arr_gto_def_.Contains(I) Then

                        ch = Me.gd_gastdef.Rows(I).FindControl("ch_sel")
                        ch.Checked = True



                        Me.txt_total.Text = CDbl(Me.txt_total.Text) + CDbl(Me.gd_gastdef.Rows(I).Cells(3).Text)
                        Me.txt_total.Text = Format(CDbl(Me.txt_total.Text), FMT.FCMSD)



                    End If

                End If

            Next
           


            Me.gr_gastofijo.DataSource = coll_chr
            Me.gr_gastofijo.DataBind()


        Catch ex As Exception
            '  Msj(Caption, ex.Message, TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub Ch_fijos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        arr_gto_fij_ = New ArrayList
        Dim mtotot As Double
        Dim mtogto As Double
        Me.txt_totales.Text = 0
        For i = 0 To Me.gr_gastofijo.Rows.Count - 1
            Dim ch As CheckBox
            ch = Me.gr_gastofijo.Rows(i).FindControl("Ch_fijos")

            arr_gto_fij_.Add(i)
            If ch.Checked Then
                mtotot = CDbl(Me.txt_totales.Text)
                mtogto = CDbl(Me.gr_gastofijo.Rows(i).Cells(3).Text)

                Me.txt_totales.Text = mtotot + mtogto
                Me.txt_totales.Text = Format(CDbl(Me.txt_totales.Text), FMT.FCMSD)

            End If
            'coll_Gto.Add(i)

        Next
    End Sub

    Protected Sub ch_sel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        arr_gto_def_ = New ArrayList

        Me.txt_total.Text = 0
        For i = 0 To Me.gd_gastdef.Rows.Count - 1
            Dim ch As CheckBox
            ch = Me.gd_gastdef.Rows(i).FindControl("ch_sel")

            If ch.Checked Then

                arr_gto_def_.Add(i)

                Me.txt_total.Text = CDbl(Me.txt_total.Text) + CDbl(Me.gd_gastdef.Rows(i).Cells(3).Text)
                Me.txt_total.Text = Format(CDbl(Me.txt_total.Text), FMT.FCMSD)

            End If

        Next

    End Sub


    Protected Sub Btn_AceptarGastos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_AceptarGastos.Click
        AgregaGastos()
        msj.Mensaje(Me, "Atención", "Gastos asociados correctamente , ahora cierre la ventana o presione volver", ClsMensaje.TipoDeMensaje._Exclamacion, , False)
    End Sub

    Protected Sub btn_eli_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_eli.Click
        Try
            'coll_Gto = New Collection

            'For x = 0 To gr_gastofijo.Rows.Count - 1
            '    coll_Gto.Add(x)
            'Next

            Dim cb As CheckBox
            Dim var As Integer = 1
            Dim sw As Boolean = False

            For i = 0 To gr_gastofijo.Rows.Count - 1
                cb = gr_gastofijo.Rows(i).FindControl("Ch_fijos")
                If cb.Checked = True Then
                    coll_Gto.Remove(i + var)
                    var = var - 1
                    sw = True
                End If
            Next
            'Session("GastosFijos") = New Collection
            Session("GastosFijos") = coll_Gto

            If sw = False Then
                Exit Sub
            End If
            gr_gastofijo.DataSource = coll_Gto
            gr_gastofijo.DataBind()

            For i = 0 To gr_gastofijo.Rows.Count - 1
                txt_totales.Text = gr_gastofijo.Rows(i).Cells(3).Text + gr_gastofijo.Rows(i).Cells(3).Text
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)
            Next

            txt_totales.Text = ""

            'If gr_gastofijo.Rows.Count = Nothing Then
            '    txt_totales.Text = ""
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Sub FormatoGR()
        Try
            For i = 0 To gr_gastofijo.Rows.Count - 1
                gr_gastofijo.Rows(i).Cells(3).Text = Format(CDbl(gr_gastofijo.Rows(i).Cells(3).Text), FMT.FCMSD)
            Next

            For i = 0 To gd_gastdef.Rows.Count - 1
                gd_gastdef.Rows(i).Cells(3).Text = Format(CDbl(gd_gastdef.Rows(i).Cells(3).Text), FMT.FCMSD)
            Next
        Catch ex As Exception

        End Try
    End Sub



End Class

Public Class ClsGastos

    Public Sub New()
        _Código = 0
        _Descripción = ""
        _Monto = 0
        _Tipo = ""
        _GastoPor = 0
    End Sub

    Private _Código As Integer
    Public Property Código() As Integer
        Get
            Return _Código
        End Get
        Set(ByVal value As Integer)
            _Código = value
        End Set
    End Property

    Private _Descripción As String
    Public Property Descripción() As String
        Get
            Return _Descripción
        End Get
        Set(ByVal value As String)
            _Descripción = value
        End Set
    End Property

    Private _Monto As Double
    Public Property Monto() As Double
        Get
            Return _Monto
        End Get
        Set(ByVal value As Double)
            _Monto = value
        End Set
    End Property

    Private _Tipo As Char
    Public Property Tipo() As Char
        Get
            Return _Tipo
        End Get
        Set(ByVal value As Char)
            _Tipo = value
        End Set
    End Property

    Private _AfectoExento As Char
    Public Property AfectoExento() As Char
        Get
            Return _AfectoExento
        End Get
        Set(ByVal value As Char)
            _AfectoExento = value
        End Set
    End Property

    Private _GastoPor As Int16
    Public Property GastoPor() As Int16
        Get
            Return _GastoPor
        End Get
        Set(ByVal value As Int16)
            _GastoPor = value
        End Set
    End Property


End Class

