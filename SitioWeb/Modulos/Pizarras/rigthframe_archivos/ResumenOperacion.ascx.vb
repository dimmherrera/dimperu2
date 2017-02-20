Imports ClsSession.ClsSession
Imports ClsSession.SesionOperaciones
Imports CapaDatos

Partial Class ResumenOperacional
    Inherits System.Web.UI.UserControl
    Dim cmc As New ClaseComercial
    Private CG As New ConsultasGenerales
    Private FMT As New FuncionesGenerales.ClsLocateInfo
    Private RG As New FuncionesGenerales.FComunes
    Dim OP As New ClaseOperaciones
    Dim CBZ As New ClaseCobranza
    Dim RW As New FuncionesGenerales.RutinasWeb


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then

            Response.Buffer = True
            Response.AddHeader("Pragma", "no-cache")
            Response.CacheControl = "no-cache"
            Response.Expires = -1

            '   RutCli = 5
          
            If NroOperacion > 0 And NroNegociacion > 0 And RutCli > 0 Then
                Traedatos()
                TraeDatosSimulacion()
            Else
                Limpia()
            End If

            Lbl_Ultimo.Text = "Año " & Date.Now.Year
            Lbl_Penultimo.Text = "Año " & Date.Now.Year - 1
            AlineaDerecha()

        End If

    End Sub

    Private Sub Limpia()

        Txt_MtoLineaApb.Text = ""
        Txt_Spread.Text = ""
        Txt_Tasa.Text = ""
        Txt_MtoUtilizado.Text = ""
        Txt_Spread.Text = ""
        Txt_Tasa.Text = ""
        Txt_MtoLineaApb.Text = ""
        Txt_MtoUtilizado.Text = ""

        Txt_MtoOpeCurse.Text = ""
        Txt_SaldoLinea.Text = ""

        Txt_UltimoAno.Text = ""
        Txt_PenultimoAno.Text = ""


        Txt_MtoAnticipo.Text = ""
        Txt_CostoFondo.Text = ""

        Txt_Spread.Text = ""
        Txt_Tasa.Text = ""

        Txt_PorComision.Text = ""
        Txt_Minimo.Text = ""
        Txt_Maximo.Text = ""
        Txt_Flat.Text = ""

        Txt_ComisionTotal.Text = ""

        Txt_NroDocOpe.Text = ""
        Txt_NroDeuOpe.Text = ""

        Txt_Mto_Doc.Text = ""
        Txt_Mto_Ant.Text = ""
        Txt_Dif_Pre.Text = ""
        Txt_Pre_Com.Text = ""
        Txt_Sal_Pen.Text = ""
        Txt_Sal_Pag.Text = ""

        Txt_Com_Por_Doc.Text = ""
        Txt_Com_Esp.Text = ""
        Txt_Iva.Text = ""
        Txt_Impuestos.Text = ""

        Txt_Descuentos.Text = ""
        Txt_GastosAfectos.Text = ""
        Txt_Gastos.Text = ""
        Txt_Val_GMF.Text = ""
        Txt_Tot_Gir.Text = ""

    End Sub

    Private Sub AlineaDerecha()

        Txt_MtoLineaApb.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoUtilizado.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_MtoOpeCurse.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_SaldoLinea.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_MtoAnticipo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_CostoFondo.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_UltimoAno.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_PenultimoAno.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_NroDocOpe.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_NroDeuOpe.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Spread.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tasa.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_PorComision.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Minimo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Maximo.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Flat.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_ComisionTotal.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Mto_Ant.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Dif_Pre.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Pre_Com.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Sal_Pen.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Sal_Pag.Attributes.Add("Style", "TEXT-ALIGN: right")

        Txt_Com_Por_Doc.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Com_Esp.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_GastosAfectos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Iva.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Gastos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Impuestos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Descuentos.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Val_GMF.Attributes.Add("Style", "TEXT-ALIGN: right")
        Txt_Tot_Gir.Attributes.Add("Style", "TEXT-ALIGN: right")

    End Sub

    Private Sub Traedatos()

        Try

            Dim RSC As Object
            Dim LDC As ldc_cls
            Dim NEG As opn_cls
            Dim OPE As ope_cls
            Dim CLI As cli_cls
            Dim APC As Object
            Dim Mto_Aprobado As Double
            Dim Mto_Ocupado As Double

            'Cargamos los objetos que se utilizaran en caso de que esten ya hayan sido cargados los rescatamos

            CLI = Session("Cliente")

            If Not IsNothing(LineaCredito) Then
                LDC = LineaCredito
            End If

            If Not IsNothing(ResumenCliente) Then
                RSC = ResumenCliente
            End If

            If Not IsNothing(Anticipos) Then
                APC = Anticipos
            End If

            If Not IsNothing(Negociacion) Then
                NEG = Negociacion
            End If

            If Not IsNothing(Operacion) Then
                OPE = Operacion
            End If

            '-----------------------LINEA DE FINANCIAMIENTO-------------------------------------------
            If IsNothing(LDC) Then

                Txt_MtoLineaApb.Text = 0
                Txt_Spread.Text = 0
                Txt_Tasa.Text = 0

                If IsNothing(RSC) Then
                    Txt_MtoUtilizado.Text = 0
                    Mto_Aprobado = 0
                    Mto_Ocupado = 0
                Else
                    Txt_MtoUtilizado.Text = Format(RSC.rsc_mto_ocu, FMT.FCMSD)
                    Mto_Aprobado = 0
                    Mto_Ocupado = RSC.rsc_mto_ocu
                End If

            Else

                If LDC.id_P_0029 = 1 Then
                    Img_LineaVig.ImageUrl = "~/Imagenes/Iconos/Aprueba.gif"
                Else
                    Img_LineaVig.ImageUrl = "~/Imagenes/Iconos/rechaza.gif"
                End If

                Img_LineaVig.DataBind()

                Txt_Spread.Text = IIf(IsNothing(NEG.opn_spr_ead), 0, NEG.opn_spr_ead)
                Txt_Tasa.Text = IIf(IsNothing(NEG.opn_pto_spr), 0, NEG.opn_pto_spr)

                Txt_MtoLineaApb.Text = Format(LDC.ldc_mto_apb, FMT.FCMSD)
                Txt_MtoUtilizado.Text = Format(LDC.ldc_mto_ocp, FMT.FCMSD)

                If Txt_MtoUtilizado.Text.Trim = "" Then
                    Txt_MtoUtilizado.Text = 0
                End If

                Mto_Aprobado = Format(LDC.ldc_mto_apb, FMT.FCMSD)

                If IsNothing(LDC.ldc_mto_ocp) Then
                    Mto_Ocupado = 0
                Else
                    Mto_Ocupado = Format(LDC.ldc_mto_ocp, FMT.FCMSD)
                End If

            End If

            If OPE.opn_cls.id_P_0023 <> 1 Then

                If OPE.opn_cls.id_P_0023 = 2 Then
                    Txt_MtoOpeCurse.Text = Format(OPE.opn_cls.opn_mto_doc, FMT.FCMCD4)
                Else
                    Txt_MtoOpeCurse.Text = Format(OPE.opn_cls.opn_mto_doc, FMT.FCMSD)
                End If
            Else

                Txt_MtoOpeCurse.Text = Format(OPE.ope_mto_ant, FMT.FCMSD)


            End If

            Txt_SaldoLinea.Text = Format(Mto_Aprobado - Mto_Ocupado - (OPE.ope_mto_ant * OPE.ope_fac_cam), FMT.FCMSD)
            Dim Monto As Double
            Dim Rango As Double

            'Monto = Mto_Aprobado - ((Mto_Aprobado - Mto_Ocupado - (OPE.opn_cls.opn_mto_doc * OPE.ope_fac_cam) * -1))
            Monto = CDbl(Txt_SaldoLinea.Text)
            Rango = LDC.ldc_mto_apb / 10

            Image1.Visible = False
            Image2.Visible = False
            Image3.Visible = False
            Image4.Visible = False
            Image5.Visible = False
            Image6.Visible = False
            Image7.Visible = False
            Image8.Visible = False
            Image9.Visible = False
            Image10.Visible = False

            Select Case True
                Case Monto <= (Rango * 1)
                    Image10.ToolTip = RG.FormatoMiles(Monto)
                    Image10.Visible = True
                Case Monto <= (Rango * 2)
                    Image9.ToolTip = RG.FormatoMiles(Monto)
                    Image9.Visible = True
                Case Monto <= (Rango * 3)
                    Image8.ToolTip = RG.FormatoMiles(Monto)
                    Image8.Visible = True
                Case Monto <= (Rango * 4)
                    Image7.ToolTip = RG.FormatoMiles(Monto)
                    Image7.Visible = True
                Case Monto <= (Rango * 5)
                    Image6.ToolTip = RG.FormatoMiles(Monto)
                    Image6.Visible = True
                Case Monto <= (Rango * 6)
                    Image5.ToolTip = RG.FormatoMiles(Monto)
                    Image5.Visible = True
                Case Monto <= (Rango * 7)
                    Image4.ToolTip = RG.FormatoMiles(Monto)
                    Image4.Visible = True
                Case Monto <= (Rango * 8)
                    Image3.ToolTip = RG.FormatoMiles(Monto)
                    Image3.Visible = True
                Case Monto <= (Rango * 9)
                    Image2.ToolTip = RG.FormatoMiles(Monto)
                    Image2.Visible = True
                Case Monto > (Rango * 9)
                    Image1.ToolTip = RG.FormatoMiles(Monto)
                    Image1.Visible = True
            End Select


            Image1.DataBind()
            Image2.DataBind()
            Image3.DataBind()
            Image4.DataBind()
            Image5.DataBind()
            Image6.DataBind()
            Image7.DataBind()
            Image8.DataBind()
            Image9.DataBind()
            Image10.DataBind()

            '-----------------------CANTIDAD DE OPERACIONES CUERSADAS LOS DOS ULTIMOS AÑOS---------------------------------------
            Dim Coll_Ope As Collection

            Coll_Ope = OP.OperacionUltimosAnosDevuelve(Date.Now.Year - 1, Date.Now.Year, RutCli)

                If Coll_Ope.Count = 0 Then
                    Txt_UltimoAno.Text = 0
                    Txt_PenultimoAno.Text = 0
                Else

                    Dim Ultimo As Integer
                    Dim Penultimo As Integer


                    For I = 1 To Coll_Ope.Count

                        If CDate(Coll_Ope.Item(I).opo_fec_oto).Year = Date.Now.Year Then
                            Ultimo += 1
                        End If

                        If CDate(Coll_Ope.Item(I).opo_fec_oto).Year = Date.Now.Year - 1 Then
                            Penultimo += 1
                        End If

                    Next

                    Txt_UltimoAno.Text = Ultimo
                    Txt_PenultimoAno.Text = Penultimo

                End If

            '-----------------------OPERACION-------------------------------------------
            'P.GATICA
            ' Se agrega formato segun moneda , pues estaba omitiendo los decimales

            If OPE.opn_cls.id_P_0023 <> 1 Then

                If OPE.opn_cls.id_P_0023 = 2 Then

                    Txt_CostoFondo.Text = Format(OPE.opn_cls.opn_tas_bas, FMT.FSMCD)

                    Txt_Spread.Text = Format(OPE.opn_cls.opn_spr_ead, FMT.FSMCD)
                    Txt_Tasa.Text = Format((OPE.opn_cls.opn_tas_bas + OPE.opn_cls.opn_spr_ead + OPE.opn_cls.opn_pto_spr), FMT.FSMCD)


                    Txt_PorComision.Text = Format(OPE.opn_cls.opn_por_com, FMT.FSMCD)
                    Txt_MtoAnticipo.Text = Format(OPE.opn_cls.opn_mto_doc, FMT.FCMCD4)
                    Txt_Minimo.Text = Format(OPE.opn_cls.opn_com_min, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                    Txt_Maximo.Text = Format(OPE.opn_cls.opn_com_max, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                    Txt_Flat.Text = Format(OPE.opn_cls.opn_com_fla, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_fla))

                    Txt_ComisionTotal.Text = Format(OPE.ope_com_tot, FMT.FCMCD4)

                Else

                    Txt_CostoFondo.Text = Format(OPE.opn_cls.opn_tas_bas, FMT.FSMCD)

                    Txt_Spread.Text = Format(OPE.opn_cls.opn_spr_ead, FMT.FSMCD)
                    Txt_Tasa.Text = Format((OPE.opn_cls.opn_tas_bas + OPE.opn_cls.opn_spr_ead + OPE.opn_cls.opn_pto_spr), FMT.FSMCD)

                    Txt_PorComision.Text = Format(OPE.opn_cls.opn_por_com, FMT.FSMCD)

                    Txt_MtoAnticipo.Text = Format(OPE.opn_cls.opn_mto_doc, FMT.FCMCD)
                    Txt_Minimo.Text = Format(OPE.opn_cls.opn_com_min, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                    Txt_Maximo.Text = Format(OPE.opn_cls.opn_com_max, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                    Txt_Flat.Text = Format(OPE.opn_cls.opn_com_fla, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_fla))
                    Txt_ComisionTotal.Text = Format(OPE.ope_com_tot, FMT.FCMCD)


                End If
            Else

                Txt_MtoAnticipo.Text = Format(OPE.opn_cls.opn_mto_doc, FMT.FCMSD)
                Txt_CostoFondo.Text = Format(OPE.opn_cls.opn_tas_bas, FMT.FSMCD)

                Txt_Spread.Text = Format(OPE.opn_cls.opn_spr_ead, FMT.FSMCD)
                Txt_Tasa.Text = Format((OPE.opn_cls.opn_tas_bas + OPE.opn_cls.opn_spr_ead + OPE.opn_cls.opn_pto_spr), FMT.FSMCD)

                Txt_PorComision.Text = Format(OPE.opn_cls.opn_por_com, FMT.FSMCD)
                If Not IsNothing(OPE.opn_cls.id_P_0023_com) Then
                    Txt_Minimo.Text = Format(OPE.opn_cls.opn_com_min, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                    Txt_Maximo.Text = Format(OPE.opn_cls.opn_com_max, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023_com))
                Else
                    Txt_Minimo.Text = 0
                    Txt_Maximo.Text = 0
                End If
                
                Dim flat As Double
                Dim monedaflat As Integer

                If IsNothing(OPE.opn_cls.id_P_0023_fla) Then
                    flat = 0
                    monedaflat = 0
                Else
                    flat = OPE.opn_cls.opn_com_fla
                    monedaflat = OPE.opn_cls.id_P_0023_fla
                End If

                Txt_Flat.Text = Format(flat, RG.DevuelveFormatoMoneda(monedaflat))

                Txt_ComisionTotal.Text = Format(OPE.ope_com_tot, FMT.FCMSD)

            End If


            Txt_NroDocOpe.Text = OPE.opn_cls.opn_can_doc
            Txt_NroDeuOpe.Text = OPE.opn_cls.opn_can_ddr
            If Not IsNothing(OPE.opn_cls.id_P_0023_com) Then
                Txt_Minimo.ToolTip = "Comisión minima en " & OPE.opn_cls.P_0023_cls.pnu_des
                Txt_Maximo.ToolTip = "Comisión maxima en " & OPE.opn_cls.P_0023_cls.pnu_des
            End If
            If Not IsNothing(OPE.opn_cls.id_P_0023_fla) Then
                Txt_Flat.ToolTip = "Comisión Flat en " & OPE.opn_cls.P_0023_cls.pnu_des
            End If
            '-----------------------Se Agrega Exepciones/Condiciones ---------------------------
            'ASaldivar
            txt_Excep.Text = OPE.opn_cls.opn_com_neg


            '-----------------------SPREAD Y COMISION-------------------------------------------
            Dim OPO As opo_cls

            OPO = OP.OperacionUltimaDevuelve(RutCli)

            Img_Spread_Aum.Visible = False
            Img_Spread_Man.Visible = False
            Img_Spread_Baj.Visible = False

            If Not IsNothing(OPO) Then

                'SPREAD
                Select Case CDbl(Txt_Spread.Text)
                    Case Is > OPO.ope_cls.opn_cls.opn_spr_ead
                        Img_Spread_Aum.Visible = True
                    Case Is < OPO.ope_cls.opn_cls.opn_spr_ead
                        Img_Spread_Baj.Visible = True
                    Case Is = OPO.ope_cls.opn_cls.opn_spr_ead
                        Img_Spread_Man.Visible = True
                End Select

                Img_Spread_Man.DataBind()
                Img_Spread_Aum.DataBind()
                Img_Spread_Man.DataBind()


                Img_Com_Aum.Visible = False
                Img_Com_Man.Visible = False
                Img_Com_Baj.Visible = False

                'COMISION
                Select Case CDbl(Txt_PorComision.Text)
                    Case Is > OPO.ope_cls.opn_cls.opn_por_com
                        Img_Com_Aum.Visible = True
                    Case Is < OPO.ope_cls.opn_cls.opn_por_com
                        Img_Com_Baj.Visible = True
                    Case Is = OPO.ope_cls.opn_cls.opn_por_com
                        Img_Com_Man.Visible = True
                End Select

                Img_Com_Aum.DataBind()
                Img_Com_Man.DataBind()
                Img_Com_Baj.DataBind()


            End If

            '-----------------------VERIFICACION---------------------------------------
            Dim Coll As Collection

            Coll = CBZ.VerificacionTodosDoctosDevuelve(OPE.id_ope)


            If OPE.opn_cls.opn_can_doc = Coll.Count Then
                Img_EstVer.ImageUrl = "~/Imagenes/Iconos/verde.gif"
            Else
                Img_EstVer.ImageUrl = "~/Imagenes/Iconos/rojo.gif"
            End If

            Img_EstVer.DataBind()

        Catch ex As Exception
            '    RW.Mensaje(Me.Page, "Error detalle operación: /b" + ex.Message)
        End Try

    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click
        If NroOperacion > 0 And NroNegociacion > 0 And RutCli > 0 Then
            Traedatos()
        End If
    End Sub

    Public Sub Refrescar()
        Dim e As New EventArgs

        'Page_Load(Me, e)


        If NroOperacion > 0 And NroNegociacion > 0 And RutCli > 0 Then
            Traedatos()
        End If
    End Sub

    Private Sub TraeDatosSimulacion()

        Try

            Dim OPE As ope_cls

            If Not IsNothing(Operacion) Then
                OPE = Operacion

            Else
                Exit Sub
            End If

            With OPE

                
                'Pais
                'If Not IsNothing(.P_0070_cls) Then
                '    Txt_Pais.Text = .P_0070_cls.pnu_des
                'Else
                '    Txt_Pais.Text = ""
                'End If

                ''Observacion
                'Txt_Observacion.Text = .ope_obs_sim

                'Calculos de simulacion
                Dim formato As String

                If .opn_cls.id_P_0023 = 1 Then
                    formato = FMT.FCMSD
                ElseIf .opn_cls.id_P_0023 = 2 Then
                    formato = FMT.FCMCD4
                Else
                    formato = FMT.FCMCD
                End If


                Txt_Mto_Doc.Text = Format(.opn_cls.opn_mto_doc, formato)
                Txt_Mto_Ant.Text = Format(.ope_mto_ant, formato)
                Txt_Dif_Pre.Text = Format(IIf(IsNothing(.ope_dif_pre), 0, .ope_dif_pre), formato)
                Txt_Pre_Com.Text = Format(IIf(IsNothing(.ope_pre_com), 0, .ope_pre_com), formato)
                Txt_Sal_Pen.Text = Format(IIf(IsNothing(.ope_sal_pen), 0, .ope_sal_pen), formato)
                Txt_Sal_Pag.Text = Format(IIf(IsNothing(.ope_sal_pag), 0, .ope_sal_pag), formato)


                Txt_Com_Por_Doc.Text = Format(IIf(IsNothing(.ope_com_tot), 0, .ope_com_tot), formato)

                If IsNothing(.opn_cls.id_P_0023_fla) Then
                    Me.Txt_Com_Esp.Text = 0
                End If

                If .opn_cls.id_P_0023 = 1 Then
                    If .opn_cls.id_P_0023_fla = 2 Then
                        Txt_Com_Esp.Text = Format((IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla) * .ope_val_duf) / .ope_fac_cam, formato)
                        Txt_Com_Esp.Text = Math.Round(CDbl(Txt_Com_Esp.Text), MidpointRounding.AwayFromZero)
                        Txt_Com_Esp.Text = Format(CDbl(Txt_Com_Esp.Text), FMT.FCMSD)
                    ElseIf .opn_cls.id_P_0023_fla = 3 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * .ope_val_dus) / .ope_fac_cam
                        Txt_Com_Esp.Text = Math.Round(CDbl(Txt_Com_Esp.Text), MidpointRounding.AwayFromZero)
                        Txt_Com_Esp.Text = Format(CDbl(Txt_Com_Esp.Text), FMT.FCMSD)

                    ElseIf .opn_cls.id_P_0023_fla = 4 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * CG.ParidadDevuelve(.opn_cls.id_P_0023_fla, .ope_fec_sim).par_val) / .ope_fac_cam
                        Txt_Com_Esp.Text = Math.Round(CDbl(Txt_Com_Esp.Text), MidpointRounding.AwayFromZero)
                        Txt_Com_Esp.Text = Format(CDbl(Txt_Com_Esp.Text), FMT.FCMSD)
                    ElseIf .opn_cls.id_P_0023_fla = 1 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * 1) / .ope_fac_cam
                        Txt_Com_Esp.Text = Math.Round(CDbl(Txt_Com_Esp.Text), MidpointRounding.AwayFromZero)
                        Txt_Com_Esp.Text = Format(CDbl(Txt_Com_Esp.Text), FMT.FCMSD)
                    End If
                Else
                    If .opn_cls.id_P_0023_fla = 2 Then
                        Txt_Com_Esp.Text = Format((IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla) * .ope_val_duf) / .ope_fac_cam, formato)
                    ElseIf .opn_cls.id_P_0023_fla = 3 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * .ope_val_dus) / .ope_fac_cam
                    ElseIf .opn_cls.id_P_0023_fla = 4 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * CG.ParidadDevuelve(.opn_cls.id_P_0023_fla, .ope_fec_sim).par_val) / .ope_fac_cam
                    ElseIf .opn_cls.id_P_0023_fla = 1 Then
                        Txt_Com_Esp.Text = (Format(IIf(IsNothing(.opn_cls.opn_com_fla), 0, .opn_cls.opn_com_fla), formato) * 1) / .ope_fac_cam
                        Txt_Com_Esp.Text = Math.Round(CDbl(Txt_Com_Esp.Text), MidpointRounding.AwayFromZero)
                    End If
                End If


                If .ope_com_tot = 0 Then
                    Txt_Com_Por_Doc.Text = 0
                Else
                    Txt_Com_Por_Doc.Text = Format(IIf(IsNothing(.ope_com_tot), 0, .ope_com_tot) - Txt_Com_Esp.Text, formato)
                End If


                Txt_Iva.Text = Format(IIf(IsNothing(.ope_iva_com), 0, .ope_iva_com), formato)
                Txt_Impuestos.Text = Format(IIf(IsNothing(.ope_imp_ope), 0, .ope_imp_ope), formato)

                Dim GOS As Object
                Dim ING As Object
                Dim Suma_Ingresos As Double

                'obtengo los descuento de la operacio
                '****************IMPORTANTE VALIDAR CON JAIME QUE ESTE BIEN LA RELACION PORQUE TIENE ASOCIADA EL ID_OPO  NO ID_OPE
                ING = CG.IngresosAnticiposSinGiroDevuelve(.id_ope)

                If IsNothing(ING) Then
                    Suma_Ingresos = 0
                Else
                    For Each I In ING
                        Suma_Ingresos = Suma_Ingresos + I.ing_mto_tot
                    Next
                End If


                ''obtengo los gastos de la operacion
                'Los gastos ya se encuentran concentrados y pasados a la moneda correspondiente
                ' en el campo ope_mon_gas , por lo tanto no se necesita ir a buscarlos a otras
                'tablas .

                'GOS = CG.GastosDeOperacionDevuelve(.id_ope)

                'If IsNothing(GOS) Then
                '    Suma_Gastos = 0
                'Else

                '    For Each g In GOS

                '        If Not IsNothing(g.id_gdn) Then
                '            Suma_Gastos = Suma_Gastos + g.gdn_cls.gto_cls.gto_mto
                '        ElseIf Not IsNothing(g.id_gfn) Then
                '            Suma_Gastos = Suma_Gastos + g.gfn_cls.gfn_mto
                '        End If

                '    Next

                'End If




                'Dim Total_Giro As Double

                'Total_Giro = IIf(IsNothing(OPE.ope_mto_ant), 0, OPE.ope_mto_ant) - _
                '             IIf(IsNothing(OPE.ope_dif_pre), 0, OPE.ope_dif_pre) - _
                '             IIf(IsNothing(Txt_Com_Por_Doc.Text), 0, Txt_Com_Por_Doc.Text) - _
                '             IIf(IsNothing(Txt_Com_Esp.Text), 0, Txt_Com_Esp.Text) - _
                '             IIf(IsNothing(OPE.ope_iva_com), 0, OPE.ope_iva_com) - _
                '             IIf(IsNothing(OPE.ope_imp_ope), 0, OPE.ope_imp_ope) - _
                '             (OPE.ope_mon_gas + OPE.ope_mon_gas_afe) - Suma_Ingresos

                ' Mto_a_Girar = Total_Giro

                'descuentos son los ingresos

                '**************************************************************************************************************
                If Val(.opn_cls.id_P_0012) <> 3 Then
                    'mto ant         dif pre            com tot          iva com           mon gas           impuesto             tot gir
                    Me.Txt_Descuentos.Text = CDbl(IIf(IsDBNull(.ope_mto_ant), 0, .ope_mto_ant) - _
                                                  IIf(IsDBNull(.ope_dif_pre), 0, .ope_dif_pre) - _
                                                  IIf(IsDBNull(.ope_com_tot), 0, .ope_com_tot) - _
                                                  IIf(IsDBNull(.ope_iva_com), 0, .ope_iva_com) - _
                                                  IIf(IsDBNull(.ope_mon_gas), 0, .ope_mon_gas) - _
                                                  IIf(IsDBNull(.ope_mon_gas_afe), 0, .ope_mon_gas_afe) - _
                                                  IIf(IsDBNull(.ope_imp_ope), 0, .ope_imp_ope) - _
                                                  IIf(IsDBNull(.ope_val_gmf), 0, .ope_val_gmf)) - _
                                                  IIf(IsDBNull(.ope_tot_gir), 0, .ope_tot_gir)
                Else

                    'Solo cobranza
                    'mto ant         dif pre            com tot          iva com           mon gas           impuesto             tot gir
                    Me.Txt_Descuentos.Text = CDbl(IIf(IsDBNull(.ope_mto_scb), 0, .ope_mto_scb) - IIf(IsDBNull(.ope_dif_pre), 0, .ope_dif_pre) - IIf(IsDBNull(.ope_com_tot), 0, .ope_com_tot) - IIf(IsDBNull(.ope_iva_com), 0, .ope_iva_com) - IIf(IsDBNull(.ope_imp_ope), 0, .ope_imp_ope)) - IIf(IsDBNull(.ope_tot_gir), 0, .ope_tot_gir)

                End If

                '**************************************************************************************************************

                'Txt_Descuentos.Text = Format(Suma_Ingresos, RG.DevuelveFormatoMoneda(OPE.opn_cls.id_P_0023))

                Txt_Descuentos.Text = Format(CDbl(Txt_Descuentos.Text), formato)

                Txt_Gastos.Text = Format(OPE.ope_mon_gas, formato)
                Txt_GastosAfectos.Text = Format(OPE.ope_mon_gas_afe, formato)
                Txt_Val_GMF.Text = Format(OPE.ope_val_gmf, formato)

                If .opn_cls.id_P_0012 <> 3 Then
                    Txt_Tot_Gir.Text = Format(.ope_tot_gir, formato)
                Else
                    Txt_Tot_Gir.Text = 0
                End If

                Txt_Tot_Gir.Text = Txt_Tot_Gir.Text - Txt_Descuentos.Text
                Txt_Tot_Gir.Text = Format(CDbl(Txt_Tot_Gir.Text), formato)

            End With

        Catch ex As Exception
            'MsgBox(ex.Message, 48, "Detalle Operacion")
        End Try

    End Sub

End Class


