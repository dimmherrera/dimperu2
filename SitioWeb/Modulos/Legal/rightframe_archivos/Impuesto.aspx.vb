Imports CapaDatos

Partial Class Impuesto

    Inherits System.Web.UI.Page

#Region "Variables"
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CG As New ConsultasGenerales
    Dim AG As New ActualizacionesGenerales
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim FC As New FuncionesGenerales.FComunes
    Dim cta As New ClaseCuentas

    Dim Moneda As Integer
    Dim caption As String = "Impuesto"
    Dim Msj As New ClsMensaje
    Dim CL As New ConsultasLegales
    Dim AL As New ActualizacionesLegales

#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Response.Expires = -1
                If Request.QueryString("Docto") <> 0 Then
                    HF_Id_Docto.Value = Request.QueryString("Docto")
                    CargaDatos()
                End If
            End If

            txt_NPgr.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_Mto.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_Mto_Imp.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_Tasa.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_FactCambio.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_Contrato.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_Contrato.Attributes.Add("readonly", "true")
            txt_nro_doc.Attributes.Add("Style", "TEXT-ALIGN:right")
            txt_nro_doc.Attributes.Add("readonly", "true")


            IB_AyudaDoc.Attributes.Add("onClick", "WinOpen(2,'../../Ayudas/AyudaCxC.aspx?Rut=" & CL.PagareDevuelveCliente(HF_Id_Docto.Value) & "&DNC=" & 1 & "', 'PopUpCuentas Por Pagar',1220,610,200,150);")


        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Function y Sub"
    Sub CargaDatos()
        Try
            Dim DIF As VariantType
            Dim F_Vg As String
            Dim F_Vto As String
            'Dim CALCULA_IMPUESTO As Integer
            Dim VALOR As String


            Dim p As New pgr_cls
            p = CL.PagareDevuelveObjeto(HF_Id_Docto.Value)
            HF_Rut.Value = p.cli_idc
            'Pgr = p.id_pgr

            txt_NPgr.Text = p.pgr_num
            txt_Fecha.Text = Format(CDate(Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year))
            txt_Moneda.Text = p.P_0023_cls.pnu_des
            txt_Mto.Text = p.pgr_mto
            txt_TipoPgr.Text = p.P_0021_cls.pnu_des
            F_Vg = p.pgr_fec_ing
            F_Vto = p.pgr_fev
            HF_Id_Moneda.Value = p.id_P_0023

            Dim t As New tim_cls
            t = CL.TasaImpuestoDevuelveObjeto()
            HF_T_Vista.Value = t.tim_val_vis
            HF_T_Plaza.Value = t.tim_val_pla
            'Trae el factor de cambio
            Dim J As New par_cls
            J = CG.ParidadDevuelve(HF_Id_Moneda.Value, Date.Today)
            If J.par_val_cob <> 0 Then
                txt_factCambio.Text = J.par_val_cob
                'da formato al tipo de moneda seleccionado
                p = CL.PagareDevuelveObjeto(HF_Id_Docto.Value)

                txt_factCambio.ReadOnly = True
                Select Case p.id_P_0023
                    Case 1 'pesos
                        txt_factCambio.Text = Format(CLng(txt_factCambio.Text), FMT.FCMSD)
                        txt_factCambio_MaskedEditExtender.Mask = "999,999,999,999"
                    Case 2 'uf
                        txt_factCambio.Text = Format(CLng(txt_factCambio.Text), FMT.FCMCD4)
                        txt_factCambio_MaskedEditExtender.Mask = "999,999,999,999.9999"
                    Case 3, 4 ' dolar y euro
                        txt_factCambio.Text = Format(CLng(txt_factCambio.Text), FMT.FCMCD)
                        txt_factCambio_MaskedEditExtender.Mask = "999,999,999,999.99"
                End Select
            Else
                Msj.Mensaje(Me.Page, caption, "No se a ingresado el factor de cambio", TipoDeMensaje._Exclamacion, "", True)
                IB_Aceptar.Enabled = False

            End If
            IB_Aceptar.Focus()
            '*************************************************************************************************
            'funcion que calcula impuesto

            If Val(HF_T_Vista.Value) > 0 Then
                '   HF_T_Vista.Value = Mid(HF_T_Vista.Value, 1, InStr(HF_T_Vista.Value, ".") - 1) & "," & Mid(HF_T_Vista.Value, InStr(HF_T_Vista.Value, ".") + 1)
                HF_T_Vista.Value = Val(HF_T_Vista.Value)
            End If
            If Val(HF_T_Plaza.Value) > 0 Then
                'HF_T_Plaza.Value = Mid(HF_T_Plaza.Value, 1, InStr(HF_T_Plaza.Value, ".") - 1) & "," & Mid(HF_T_Plaza.Value, InStr(HF_T_Plaza.Value, ".") + 1)
                HF_T_Plaza.Value = Val(HF_T_Plaza.Value)
            End If

            txt_Mto.Text = Format(CLng((txt_Mto.Text)), FC.DevuelveFormatoMoneda(p.id_P_0023))
            If p.id_P_0021 = 1 Then
                txt_Tasa.Text = Format(CLng(HF_T_Vista.Value), "###,###0.00")
                VALOR = txt_Mto.Text
                HF_Imp.Value = txt_Mto.Text * (HF_T_Vista.Value / 100)
            Else
                txt_Tasa.Text = Format(CLng(HF_T_Plaza.Value), "###,###0.00")
                HF_Imp.Value = txt_Mto.Text * (HF_T_Plaza.Value / 100)
                If F_Vto = "" Then
                    DIF = 0
                Else
                    DIF = Int((DateDiff("d", CDate(F_Vg), CDate(F_Vto)) / 30) * -1) * -1
                    'DIF = (DateDiff("DateInterval.Day", (F_Vg), (F_Vto)) / 30) * -1
                End If
                HF_Imp.Value = Val(HF_Imp.Value) * CDec(DIF) * (HF_T_Plaza.Value / 100)
            End If
            If p.id_P_0023 <> 1 Then
                HF_Imp.Value = HF_Imp.Value * CDec(txt_factCambio.Text)
            End If
            HF_Imp.Value = Format(CLng(HF_Imp.Value), FMT.FCMCD4)
            If HF_Imp.Value = 0 Then
                txt_Mto_Imp.Text = 0
            Else
                txt_Mto_Imp.Text = HF_Imp.Value
                txt_Mto_Imp.Text = Format(CLng((txt_Mto_Imp.Text)), FC.DevuelveFormatoMoneda(p.id_P_0023))
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region


#Region "Botonera"
    Protected Sub IB_Volver_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Volver.Click
        Try
            RW.ClosePag(Me)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub IB_Aceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Aceptar.Click
        Try

            If HF_Id_Docto.Value > 0 Then
                If txt_Contrato.Text <> "" Then
                    If txt_Mto_Imp.Text > 0 Then
                        Dim c As New cxc_cls
                        c.cli_idc = Format(CLng(HF_Rut.Value), "000000000000")
                        c.cxc_des = "IMPUESTO PAGARE Nº " & HF_Id_Docto.Value
                        c.cxc_fac_cam = 1
                        c.cxc_fct = "N"
                        c.cxc_fec = Date.Now
                        c.cxc_ful_pgo = Date.Now
                        c.cxc_int = 0
                        c.cxc_mto = txt_Mto_Imp.Text
                        c.cxc_sal = txt_Mto_Imp.Text
                        c.id_doc = cta.NroDoctoDevuelve(CInt(Me.txt_nro_doc.Text))
                        c.id_P_0023 = HF_Id_Moneda.Value
                        c.id_P_0041 = 16
                        c.id_P_0057 = 1
                        Dim numero As Integer
                        numero = AL.CxcInserta(c)
                        If numero > 0 Then
                            If AL.GeneraImpuesto(HF_Id_Docto.Value, numero, txt_Tasa.Text, txt_Mto_Imp.Text) = True Then
                                Msj.Mensaje(Me.Page, caption, "impuesto generado", ClsMensaje.TipoDeMensaje._Informacion, "", False)
                                RW.CloseModal(Me, "ctl00$ContentPlaceHolder1$Link_Actualiza")
                            End If
                        End If
                    Else
                        Msj.Mensaje(Me.Page, caption, "El valor del impuesto es cero, no se podra guardar", TipoDeMensaje._Exclamacion, "", True)
                    End If
                Else
                    Msj.Mensaje(Me.Page, caption, "Debe seleccionar contrato desde la ayuda", TipoDeMensaje._Exclamacion, "", False)
                    Exit Sub
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region





End Class
