Imports Microsoft.Office.Interop.Excel
Imports ClsSession.SesionOperaciones
Imports System.Data
Imports System.Data.OleDb
Imports ClsSession.ClsSession
Imports CapaDatos

Partial Class Modulos_Operaciones_rightframe_archivos_carga_masiva
    Inherits System.Web.UI.Page

    Dim cg As New ConsultasGenerales
    Dim fmt As New FuncionesGenerales.Variables
    Dim ag As New ActualizacionesGenerales
    Dim msj As New ClsMensaje
    Dim OP As New ClaseOperaciones
    Dim CMC As New ClaseComercial
    Dim FC As New FuncionesGenerales.FComunes
    Dim rw As New FuncionesGenerales.RutinasWeb


#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Response.Expires = -1

            Coll_DOC = New Collection
            coll_ope = New Collection
            Coll_CLF = New Collection
            Session("IDX") = 0

            coll_ope = OP.OperacionesPorClienteDevuelve(RUT_CLI_RPT, 1, False, Nothing)

            'If coll_ope.Item(NroRow).id_p_0031 <> 3 Then

            cabecera_grilla.Width = 1170
            GridView1.Width = 1170

            OP.Columna_Archivo_Devuelve(Dr_campo_0, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_1, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_2, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_3, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_4, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_5, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_6, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_7, 1)
            OP.Columna_Archivo_Devuelve(Dr_campo_8, 1)

            Coll_DOC = New Collection



        End If

        'Me.btn_volver.Attributes.Add("onclick", "JavaScript:CerrarVentana('ctl00$ContentPlaceHolder1$Lb_buscar');")
        Me.btn_volver.Attributes.Add("onclick", "window.close();")

    End Sub

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

    Protected Sub ib_cargar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ib_cargar.Click

        If FileUpload1.FileName <> "" Then

            Descripcion = Server.MapPath("") + "\" + FileUpload1.FileName

            Dim ruta As String
            
            Try

                If (FileUpload1.HasFile) Then

                    ruta = Server.MapPath("archivos_carga_masiva") & "\" & "masiva" & Date.Now.ToString("yyyyMMdd_hhmmss") & ".xls"
                    ruta = ruta.Replace("rightframe_archivos\", "")
                    FileUpload1.SaveAs(ruta)

                    lb_archivo.Text = "Esta trabajando con el archivo " & FileUpload1.FileName

                Else
                    Exit Sub
                End If

                Dim ADAPTADOR1 As New OleDbDataAdapter
                datasetmasivo = New DataSet
                Dim datasetmasivoaux = New DataSet
                Dim col As New Collection
                Dim cadenaCon As String = ""

                If FC.ExtraerExtension(FileUpload1.FileName, ".") = "xls" Then
                    cadenaCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ruta & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
                ElseIf FC.ExtraerExtension(FileUpload1.FileName, ".") = "xlsx" Then
                    cadenaCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ruta & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                End If

                Dim con As OleDbConnection = New OleDbConnection(cadenaCon)

                con.Open()
                ADAPTADOR1 = New OleDbDataAdapter("SELECT * FROM  [Hoja1$]", con)
                datasetmasivoaux = New DataSet
                datasetmasivo = New DataSet
                ADAPTADOR1.Fill(datasetmasivoaux, "XLData")

                Dim array As New ArrayList

                Dim fila_dv As Integer
                Dim fila_rut As Long
                fila_dv = Nothing
                fila_rut = Nothing

                Dim aux_for As Integer
                Dim cant_filas As Integer
                cant_filas = datasetmasivoaux.Tables(0).Rows.Count - 1
                datasetmasivo.Tables.Add("XLData")

                For I = 0 To cant_filas
                    If datasetmasivoaux.Tables(0).Rows(I).Item(0).ToString = "" Then
                        aux_for = I
                        Exit For
                    End If
                    If aux_for = 25001 Then
                        Exit For
                    End If
                Next

                con.Close()

                If aux_for > 1000 Then

                    Me.GridView1.PagerSettings.Visible = False
                    Me.GridView1.AllowPaging = True
                    Me.GridView1.PageSize = 20

                    datasetmasivo = datasetmasivoaux

                Else
                    datasetmasivo = datasetmasivoaux
                End If

                fila_dv = Nothing
                fila_rut = Nothing

                For i = OBJETIVO To datasetmasivo.Tables(0).Columns.Count - 1

                    If datasetmasivo.Tables(0).Columns.Item(i).ColumnName = "dv" Then
                        fila_dv = i + 1
                    End If

                    If UCase(datasetmasivo.Tables(0).Columns.Item(i).ColumnName).Contains("RUT") Then
                        fila_rut = i + 1
                    End If

                    If fila_dv > 0 And fila_rut > 0 Then
                        Exit For
                    End If

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

                If datasetmasivo.Tables(0).Columns.Count <> 9 Then
                    lb_archivo.Text = ""
                    msj.Mensaje(Me, "Atención", "El archivo no corresponde a este tipo de Documento", ClsMensaje.TipoDeMensaje._Informacion)
                    Exit Sub
                End If


                GridView1.DataSource = datasetmasivo.Tables(0)
                GridView1.DataBind()

                lb_archivo1.Text = "El Archivo tiene " & datasetmasivo.Tables(0).Rows.Count & " Registros"
                fila_dv = fila_dv - 1
                fila_rut = fila_rut - 1

                For i = 0 To Me.GridView1.Rows.Count - 1
                    Me.GridView1.Rows(i).Width = 130
                    For x = 0 To Me.GridView1.Rows(i).Cells.Count - 1
                        If x = fila_dv Then
                            If Me.GridView1.Rows(i).Cells(x).Text = "" Or Me.GridView1.Rows(i).Cells(x).Text = "&nbsp;" Then
                                Me.GridView1.Rows(i).Cells(x).Text = FC.Vrut(Me.GridView1.Rows(i).Cells(fila_rut).Text)
                            End If
                        End If

                        Me.GridView1.Rows(i).Cells(x).Width = 130
                        Me.GridView1.Rows(i).Cells(x).HorizontalAlign = HorizontalAlign.Center

                        If IsDate(GridView1.Rows(i).Cells(x).Text) Then
                            GridView1.Rows(i).Cells(x).Text = Format(CDate(GridView1.Rows(i).Cells(x).Text), "dd/MM/yyyy")
                        End If

                    Next

                Next

            Catch ex As Exception
                msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Informacion)
            Finally

            End Try

        End If

    End Sub

    Private Sub setea_cabeceras(ByVal indice As Integer, ByVal lbl As System.Web.UI.WebControls.Label)

        If indice = 1 Then
            lbl.Text = "Nombre Cliente"
        ElseIf indice = 2 Then
            lbl.Text = "Nit Cliente"
        ElseIf indice = 3 Then
            lbl.Text = "Digito Cliente"
        ElseIf indice = 4 Then
            lbl.Text = "Nº Docto."
        ElseIf indice = 5 Then
            lbl.Text = "Fecha Pago"
        ElseIf indice = 6 Then
            lbl.Text = "Monto Docto."
        ElseIf indice = 7 Then
            lbl.Text = "Nombre Pagador"
        ElseIf indice = 8 Then
            lbl.Text = "Nit Pagador"
        ElseIf indice = 9 Then
            lbl.Text = "Digito Pagador"
        End If

    End Sub

    Protected Sub btn_apb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_apb.Click

        Try


            If Not IsNothing(datasetmasivo) Then

                If Timer1.Enabled = False Then


                    Coll_DOC = New Collection
                    coll_Ver = New Collection

                    arreglo = New ArrayList

                    btn_apb.Enabled = False

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

                    Try

                        Dim ldc As ldc_cls
                        ldc = cg.LineaDeCreditoDevuelve(coll_ope.Item(NroRow).cli_idc, 1)


                        Dim ant As apc_cls
                        ant = cg.AnticipoDevuelvePorLinea(ldc.id_ldc, _
                                                          coll_ope.Item(NroRow).id_p_0031)

                        Anticipos = ant

                    Catch ex As Exception

                    End Try
                End If

                Dim mensaje As String = asigna_campos_aux()

                If mensaje <> "" Then
                    coll_Ver = Nothing
                    msj.Mensaje(Me, "Atención", mensaje, 2, , False)
                    Exit Sub
                End If

            Else
                msj.Mensaje(Me, "Atención", "Debe tener cargado un archivo para poder Procesar", 2, , False)
                Exit Sub

            End If

            Dim cuota As String = coll_ope.Item(NroRow).ope_cuo

            'Validamos que los numeros de documento no se encuentren repetidos, solo para cuotas.
            If Not ValidaNumerosDocumentos(cuota) Then
                msj.Mensaje(Me, "Atención", Session("Mensaje"), 2, , False)
                Exit Sub
            End If

            Me.btn_guardar.Enabled = True
            msj.Mensaje(Me, "Atención", "Archivo fue procesado con exito , ahora puede guardar", 2, , False)
            btn_apb.Enabled = True


        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, ClsMensaje.TipoDeMensaje._Error & "" & IDX)
        End Try

    End Sub

    Public Function asigna_campos_aux() As String

        Dim CLSCLI As New ClaseClientes
        Dim dr As HiddenField
        Dim mensaje As String
        Dim var As New FuncionesGenerales.Variables
        Dim sist As sis_cls

        clf = New clf_cls

        Try


            Dim Nombre_Cliente, _
            Nit_Cliente, _
            Digito_Cliente, _
            N_Docto, _
            FECHA_Pago, _
            MONTO_docto, _
            nombre_pagador, _
            Nit_pagador, _
            Digito_Pagador As Integer

            Dim i As Integer

            For i = 1 To 9
                dr = Page.FindControl("HiddenField" & i)
                Select Case dr.Value
                    Case 1
                        Nombre_Cliente = i - 1
                    Case 2
                        Nit_Cliente = i - 1
                    Case 3
                        Digito_Cliente = i - 1
                    Case 4
                        N_Docto = i - 1
                    Case 5
                        FECHA_Pago = i - 1
                    Case 6
                        MONTO_docto = i - 1
                    Case 7
                        nombre_pagador = i - 1
                    Case 8
                        Nit_pagador = i - 1
                    Case 9
                        Digito_Pagador = i - 1
                End Select
            Next

            Dim rut_deu As String = ""
            sist = cg.SistemaDevuelve

            Dim lista As New List(Of Object)

            For i = 0 To datasetmasivo.Tables(0).DefaultView.Count - 1

                dsi = New dsi_cls
                cli_aux = New cli_cls
                deu = New deu_cls

                cli_aux.cli_rso = IIf(IsDBNull(datasetmasivo.Tables(0).DefaultView(i).Item(Nombre_Cliente)), "", datasetmasivo.Tables(0).DefaultView(i).Item(Nombre_Cliente))
                cli_aux.cli_idc = Format(CLng(datasetmasivo.Tables(0).DefaultView(i).Item(Nit_Cliente)), fmt.FMT_RUT)
                cli_aux.cli_dig_ito = CStr(datasetmasivo.Tables(0).DefaultView(i).Item(Digito_Cliente))

                If cli_aux.cli_idc <> coll_ope.Item(NroRow).cli_idc Then
                    mensaje = "Cliente debe ser el mismo de la operacion " & Format(CLng(cli_aux.cli_idc), "###,###,###") & "-" & FC.Vrut(cli_aux.cli_idc)
                    Return mensaje
                End If

                If (cli_aux.cli_dig_ito <> Nothing) Then
                    If (cli_aux.cli_dig_ito <> FC.Vrut(coll_ope.Item(NroRow).cli_idc)) Then
                        mensaje = "Digito verificador del cliente no es correcto, para el nit " & cli_aux.cli_idc
                        Return mensaje
                    End If
                End If

                If datasetmasivo.Tables(0).DefaultView(i).Item(N_Docto).ToString.Length > 20 Then
                    mensaje = "N° del documento no puede ser superior a 20 digitos" & datasetmasivo.Tables(0).DefaultView(i).Item(3)
                    Return mensaje
                End If

                If IsDBNull(datasetmasivo.Tables(0).DefaultView(i).Item(N_Docto)) Then
                    mensaje = "Numero de documento no puede ser vacio de la fila:" & i
                    Return mensaje
                End If

                If datasetmasivo.Tables(0).DefaultView(i).Item(N_Docto).ToString() = "" Or datasetmasivo.Tables(0).DefaultView(i).Item(N_Docto).ToString() = "0" Then
                    mensaje = "Numero de documento no puede ser 0 ó vacio de la fila:" & i
                    Return mensaje
                End If

                dsi.dsi_num = datasetmasivo.Tables(0).DefaultView(i).Item(N_Docto)

                If (Not IsNothing(Anticipos)) Then
                    Dim ant As apc_cls

                    ant = Anticipos

                    dsi.dsi_cbz = ant.apc_cob_son
                    dsi.dsi_cbz_son = ant.apc_cob_son
                    dsi.dsi_ntf = ant.apc_not_son
                Else
                    dsi.dsi_cbz = "N"
                    dsi.dsi_cbz_son = "N"
                    dsi.dsi_ntf = "N"
                End If

                dsi.dsi_fec_emi = coll_ope.Item(NroRow).opn_fec
                dsi.dsi_est_rsp = "N"

                If dsi.dsi_num <= "" Then
                    mensaje = "Numero de documento no puede ser vacio" & cli_aux.cli_idc
                    Return mensaje
                End If

                dsi.dsi_fev = CDate(datasetmasivo.Tables(0).DefaultView(i).Item(FECHA_Pago))

                If CDate(dsi.dsi_fev) < CDate(coll_ope.Item(NroRow).opn_fec_neg).AddDays(sist.sis_dia_vto) Then
                    mensaje = "Fecha de Vencimiento de la fila " & IDX & " no debe ser menor al " & Format(CDate(coll_ope.Item(NroRow).opn_fec_neg).AddDays(sist.sis_dia_vto), "dd/MM/yyyy")
                    Return mensaje
                    Exit Function
                End If

                Dim cadenas As String

                cadenas = datasetmasivo.Tables(0).DefaultView(i).Item(Nombre_Cliente)

                ' Split string based on spaces
                Dim words As String() = cadenas.Split(New Char() {";"})

                ' Use For Each loop over words and display them
                Dim word As String
                Dim cont As Integer = 0

                For Each word In words
                    cont = cont + 1
                    If cont = 2 Then
                        dsi.dsi_fev_rea = word
                    ElseIf cont = 3 Then
                        dsi.dsi_fev_cal = word
                    End If
                Next

                dsi.dsi_fec_emi = Date.Now
                dsi.id_p_0011 = 1
                dsi.id_ope = ID_OPE_RPT

                dsi.dsi_mto = CDbl(datasetmasivo.Tables(0).DefaultView(i).Item(MONTO_docto))
                dsi.dsi_mto_fin = CDbl(datasetmasivo.Tables(0).DefaultView(i).Item(MONTO_docto))

                If dsi.dsi_mto <= 0 Then
                    mensaje = "Valor del documento no puede ser 0 ó vacio" & cli_aux.cli_idc
                    Return mensaje
                End If

                deu.id_p_003 = 1
                deu.deu_rso = IIf(IsDBNull(datasetmasivo.Tables(0).DefaultView(i).Item(nombre_pagador)), "", datasetmasivo.Tables(0).DefaultView(i).Item(nombre_pagador))
                dsi.deu_ide = Format(CLng(datasetmasivo.Tables(0).DefaultView(i).Item(Nit_pagador)), fmt.FMT_RUT)
                deu.deu_dig_ito = datasetmasivo.Tables(0).DefaultView(i).Item(Digito_Pagador).ToString()

                'deu.deu_dig_ito = FC.Vrut(dsi.deu_ide)

                rut_deu = dsi.deu_ide

                If (dsi.deu_ide <> "") Then
                    If (deu.deu_dig_ito <> FC.Vrut(dsi.deu_ide)) Then
                        mensaje = "Digito verificador del pagador no es correcto, para el nit " & dsi.deu_ide
                        Return mensaje
                    End If
                End If

                If dsi.deu_ide = coll_ope.Item(NroRow).cli_idc Then
                    mensaje = "Cliente no puede ser utilizado como pagador en la misma operación " & Format(CLng(dsi.deu_ide), "###,###,###") & "-" & FC.Vrut(dsi.deu_ide)
                    Return mensaje
                End If

                If (deu.deu_dig_ito <> Nothing) Then
                    If (deu.deu_dig_ito <> FC.Vrut(dsi.deu_ide)) Then
                        mensaje = "Digito verificador del pagador no es correcto, para el nit " & dsi.deu_ide
                        Return mensaje
                    End If
                End If


                'caso = 9
                'If caso = 9 Then
                dsi.dsi_flj = "N"

                If coll_ope.Item(NroRow).ope_cuo = "S" Then
                    dsi.dsi_flj_num = 1
                Else
                    dsi.dsi_flj_num = 0
                End If

                Dim doc = From l In lista Where l.dsi_num = dsi.dsi_num Group By doc_num = l.dsi_num Into cantidad = Count() Select doc_num, cantidad

                For Each d In doc
                    dsi.dsi_flj_num = d.cantidad + 1
                Next

                If Not IsNothing(arreglo) Then
                    If arreglo.Contains(dsi.deu_ide) = False Then
                        arreglo.Add(dsi.deu_ide)

                        If IsNothing(cg.DeudorDevuelvePorRut(CLng(dsi.deu_ide))) Then
                            deu.deu_ide = dsi.deu_ide
                            deu.id_p_0044 = 2
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

                        If IsNothing(cg.DeudorDevuelvePorRut(CLng(dsi.deu_ide))) Then
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

                Dim query As String = ""

                query = "Exec insert_masivo '" & dsi.deu_ide & "'," & dsi.id_Ope & ", '" & dsi.dsi_num & "'," & dsi.dsi_mto & ","
                query = query & dsi.dsi_mto_fin & ",'" & dsi.dsi_flj & "'," & dsi.dsi_flj_num & ",'" & dsi.dsi_ntf & "','" & FC.FUNFechaJul(dsi.dsi_fec_emi.ToString()) & "','"
                query = query & FC.FUNFechaJul(dsi.dsi_fev.ToString()) & "','" & FC.FUNFechaJul(dsi.dsi_fev.ToString()) & "','" & FC.FUNFechaJul(dsi.dsi_fev_rea.ToString()) & "',"
                query = query & 0 & "," & 0 & ",'" & dsi.dsi_cbz & "','" & dsi.dsi_cbz_son & "','" & FC.FUNFechaJul(dsi.dsi_fev_cal.ToString()) & "'"

                coll_Ver.Add(query)
                Coll_DOC.Add(dsi)

                lista.Add(Coll_DOC(i + 1))

            Next

            coll_Ver.Add("Exec clf_masivo " & coll_ope.Item(Val(Request.QueryString("itemOPE"))).id_ope & ", '" & coll_ope.Item(Val(Request.QueryString("itemOPE"))).cal_oto_gam & "'")

        Catch ex As Exception

        End Try

    End Function

    Public Function calcula_vcto_real_masiva(ByVal sucursal As Integer, _
                                             ByVal plaza As String, _
                                             ByVal tipo As Integer) As Boolean
        Dim dr As HiddenField
        Dim buscagpg As Boolean = True
        Dim CMC As New ClaseComercial
        Dim col As New Collection
        Dim rut_deu As String
        Dim FECHA_Pago, _
        Nombre_Cliente, _
        Nit_pagador As Integer


        For i = 1 To 9
            dr = Page.FindControl("HiddenField" & i)
            Select Case dr.Value
                Case 1
                    Nombre_Cliente = i - 1
                Case 5
                    FECHA_Pago = i - 1
                Case 8
                    Nit_pagador = i - 1
            End Select
        Next

        datasetmasivo.Tables(0).DefaultView.Sort = datasetmasivo.Tables(0).Columns(FECHA_Pago).ColumnName & " ASC"

        col = CMC.DiasDeRetencionDevuelve(sucursal, plaza, tipo)

        Dim DIAS_POR_PLAZA As Integer
        Dim BUSCA_DIA_HABIL As Boolean
        Dim fecha_orig_anterior As String = ""


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


        For i = 0 To datasetmasivo.Tables(0).DefaultView.Count - 1


            If rut_deu = datasetmasivo.Tables(0).DefaultView(i).Item(Nit_pagador) Then

                rut_deu = datasetmasivo.Tables(0).DefaultView(i).Item(Nit_pagador)

                Dim fecha_actual As String

                fecha_actual = datasetmasivo.Tables(0).DefaultView(i).Item(FECHA_Pago)

                If fecha_orig_anterior <> fecha_actual Then


                    FECHA_VCTO_AUX = datasetmasivo.Tables(0).DefaultView(i).Item(FECHA_Pago)

                    '------------------------------------------------------------------------------------
                    'jlagos 29-05-2012 se agrega calanderizacion de pagos por deudor

                    If rut_deu = "" Then
                        rut_deu = 0
                    End If


                    If buscagpg = True Then
                        FECHA_VCTO_AUX = cg.DevuelveCalendarioPagoDeudor(rut_deu, FECHA_VCTO_AUX)
                    End If


                    '---------------------------------------------------------------------------------



                    fecha_orig_anterior = FECHA_VCTO_AUX

                    FECHA_VCTO_CALCULO = FECHA_VCTO_AUX

                    For x = 1 To DIAS_POR_PLAZA
                        FECHA_VCTO_CALCULO = DateAdd("D", 1, FECHA_VCTO_CALCULO)

                        If BUSCA_DIA_HABIL Then
                            FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                        End If
                    Next

                    If DIAS_POR_PLAZA = 0 Then

                        If BUSCA_DIA_HABIL Then

                            FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                            FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                        Else
                            FECHA_VCTO_AUX = FECHA
                        End If

                    End If


                End If



            Else



                rut_deu = datasetmasivo.Tables(0).DefaultView(i).Item(Nit_pagador)

                FECHA_VCTO_AUX = datasetmasivo.Tables(0).DefaultView(i).Item(FECHA_Pago)

                '------------------------------------------------------------------------------------
                'jlagos 29-05-2012 se agrega calanderizacion de pagos por deudor

                If rut_deu = "" Then
                    rut_deu = 0
                End If

                Dim fecha_gpp_esp As String

                fecha_gpp_esp = cg.DevuelveCalendarioPagoDeudorEspecial(rut_deu, FECHA_VCTO_AUX)


                If fecha_gpp_esp <> "" Then
                    FECHA_VCTO_AUX = cg.DevuelveCalendarioPagoDeudor(rut_deu, FECHA_VCTO_AUX)
                Else
                    buscagpg = False

                End If

                '---------------------------------------------------------------------------------

                fecha_orig_anterior = FECHA_VCTO_AUX

                FECHA_VCTO_CALCULO = FECHA_VCTO_AUX

                For x = 1 To DIAS_POR_PLAZA
                    FECHA_VCTO_CALCULO = DateAdd("D", 1, FECHA_VCTO_CALCULO)

                    If BUSCA_DIA_HABIL Then
                        FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                    End If
                Next

                If DIAS_POR_PLAZA = 0 Then

                    If BUSCA_DIA_HABIL Then

                        FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                        FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                    Else
                        FECHA_VCTO_AUX = FECHA
                    End If

                End If


            End If



            datasetmasivo.Tables(0).DefaultView(i).Item(Nombre_Cliente) = datasetmasivo.Tables(0).DefaultView(i).Item(Nombre_Cliente) & ";" & FECHA_VCTO_AUX & ";" & FECHA_VCTO_CALCULO

        Next

        Return True

    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click


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

        For i = 1 To 13
            Dim hf As HiddenField

            hf = Page.FindControl("HiddenField" & i)

            'hf.Value = ""
        Next

        'If coll_ope.Item(NroRow).id_p_0031 <> 3 Then

        OP.Columna_Archivo_Devuelve(Dr_campo_0, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_1, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_2, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_3, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_4, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_5, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_6, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_7, 1)
        OP.Columna_Archivo_Devuelve(Dr_campo_8, 1)

        Me.btn_guardar.Enabled = False
        Me.btn_apb.Enabled = True
        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()
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

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_guardar.Click

        Try

            If Not IsNothing(coll_Ver) Then
                If coll_Ver.Count > 0 Then
                    OP.Guarda_dsi_masivo_query(coll_Ver)
                    OP.cabeceras_documento_marca(ID_OPE_RPT)
                    'OP.CuotaDocumentos(ID_OPE_RPT)
                End If
            End If


            msj.Mensaje(Me, "Atención", "Se ha realizado la Integración", 2)

            OP.Operación_masiva_cuadra(ID_OPE_RPT)

            btn_apb.Enabled = False
            btn_guardar.Enabled = False

            coll_Ver = Nothing

        Catch ex As Exception
            msj.Mensaje(Me, "Atención", ex.Message, 2)

        End Try

    End Sub

    Private Function ValidaNumerosDocumentos(ByVal tipo As String) As Boolean

        Dim i As Integer
        Dim lista As New List(Of Object)

        For i = 1 To Coll_DOC.Count
            lista.Add(Coll_DOC(i))
        Next

        If tipo = "N" Then

            Dim doc = From l In lista Group By doc_num = l.dsi_num Into cantidad = Count() Select doc_num, cantidad

            For Each d In doc
                If d.cantidad > 1 Then
                    Session("Mensaje") = "Documento repetido en la planilla N° " & d.doc_num
                    Return False
                End If
            Next

        End If

        Dim MontoCabecera As Double
        Dim MontoCuotas As Double
        Dim Arreglocuo As ArrayList
        Dim NroDocto As String

        MontoCabecera = 0

        If tipo = "S" Then

            For i = 1 To Coll_DOC.Count

                MontoCuotas = 0
                Arreglocuo = New ArrayList

                If Coll_DOC(i).dsi_flj_num = 0 Then

                    If NroDocto = Coll_DOC(i).dsi_num Then
                        Session("Mensaje") = "Documento repetido en la planilla N° " & Coll_DOC(i).dsi_num
                        Return False
                    Else
                        MontoCabecera = Coll_DOC(i).dsi_mto
                        NroDocto = Coll_DOC(i).dsi_num
                    End If

                End If

            Next

        End If

        Dim RST As Integer

        For i = 1 To Coll_DOC.Count

            RST = OP.Documentos_cuota_valida(Coll_DOC(i).dsi_num, _
                                             coll_ope.Item(NroRow).cli_idc, _
                                             coll_ope.Item(NroRow).ope_cuo, _
                                             coll_ope.Item(NroRow).id_p_0031)

            If RST = 999 Then
                Session("Mensaje") = "Documento " & Coll_DOC(i).dsi_num & " ya se encuentra en los registros de Factoring"
                Return False
             End If

        Next

        Return True

    End Function

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim b As System.Web.UI.ImageClickEventArgs
        btn_apb_Click(Me, b)

    End Sub

#End Region

#Region "Drops"

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

                For x = 1 To 8
                    Dim hf As HiddenField
                    hf = Page.FindControl("HiddenField" & x + 1)
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_0.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And i <> 0 Then

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
                For x = 2 To 8
                    Dim dr As DropDownList
                    Dim hf As HiddenField
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_1.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And i <> 0 Then

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
                For x = 3 To 8
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If i <> 0 And Val(Me.Dr_campo_2.SelectedValue) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And  i <> 0 Then

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

                For x = 4 To 8

                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_3.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And i <> 0 Then

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
                For x = 5 To 8
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_4.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And  i <> 0 Then

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
                For x = 6 To 8
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_5.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And  i <> 0 Then

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
                For x = 7 To 8
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_6.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And i <> 0 Then

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
                For x = 8 To 8
                    Dim dr As DropDownList
                    dr = Page.FindControl("Dr_campo_" & x)


                    If Val(Me.Dr_campo_7.SelectedIndex) <> i And Val(HiddenField2.Value) <> i _
                       And Val(HiddenField1.Value) <> i And Val(HiddenField3.Value) <> i And Val(HiddenField4.Value) <> i _
                       And Val(HiddenField5.Value) <> i And Val(HiddenField6.Value) <> i And Val(HiddenField7.Value) <> i _
                       And Val(HiddenField8.Value) <> i And Val(HiddenField9.Value) <> i And i <> 0 Then

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


            Me.Dr_campo_8.Enabled = False
            Me.Dr_campo_8.CssClass = "clsDisabled"

            Me.Dr_campo_8.Visible = False
            Label8.Text = Me.Dr_campo_8.SelectedItem.Text
            Label8.Visible = True
            Label8.Width = 100%

            setea_cabeceras(Me.Dr_campo_8.SelectedValue, Label8)

            HiddenField9.Value = Me.Dr_campo_8.SelectedIndex


            calcula_vcto_real_masiva(coll_ope.Item(NroRow).id_suc, _
                                     "", _
                                     coll_ope.Item(NroRow).id_p_0031)

        End If


    End Sub


#End Region

    
End Class
