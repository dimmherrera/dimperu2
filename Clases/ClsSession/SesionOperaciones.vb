Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Data

Public Class SesionOperaciones

#Region "COLLECTION"

    Public _clf As Object
    Public Shared Property clf() As Object
        Get
            Return HttpContext.Current.Session("clf")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("clf") = Value
        End Set
    End Property


    Private _Coll_CLF As Collection
    Public Shared Property Coll_CLF() As Collection
        Get
            Return HttpContext.Current.Session("Coll_CLF")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_CLF") = Value
        End Set
    End Property


    Private _coll_ope As Collection
    Public Shared Property coll_ope() As Collection
        Get
            Return HttpContext.Current.Session("coll_ope")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ope") = Value
        End Set
    End Property

    Private _coll_ope_sim As Collection
    Public Shared Property coll_ope_sim() As Collection
        Get
            Return HttpContext.Current.Session("coll_ope_sim")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ope_sim") = Value
        End Set
    End Property

    Private _coll_ope_otg As Collection
    Public Shared Property coll_ope_otg() As Collection
        Get
            Return HttpContext.Current.Session("coll_ope_otg")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ope_otg") = Value
        End Set
    End Property

    Private _coll_ope_pag As Collection
    Public Shared Property coll_ope_pag() As Collection
        Get
            Return HttpContext.Current.Session("coll_ope_pag")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ope_pag") = Value
        End Set
    End Property

    Private _coll_Gto As Collection
    Public Shared Property coll_Gto() As Collection
        Get
            Return HttpContext.Current.Session("coll_Gto")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_Gto") = Value
        End Set
    End Property

    Private _Coll_DSI As Collection
    Public Shared Property Coll_DSI() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DSI")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DSI") = Value
        End Set
    End Property

    Private _Coll_DOC As Collection
    Public Shared Property Coll_DOC() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DOC")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DOC") = Value
        End Set
    End Property

    Private _arreglo As ArrayList
    Public Shared Property arreglo() As ArrayList
        Get
            Return HttpContext.Current.Session("arreglo")
        End Get
        Set(ByVal Value As ArrayList)
            HttpContext.Current.Session("arreglo") = Value
        End Set
    End Property

    
#End Region

#Region "Operacion-Simulación"

    Public Sub iniciarSesionOpe()

        With HttpContext.Current
            .Session("Mto_a_Girar") = _Mto_a_Girar
            .Session("Mto_a_Girar_docto") = _Mto_a_Girar_docto
            .Session("Mto_ant_operacion") = _Mto_ant_operacion
            .Session("Var_Mar") = _Var_Mar
            .Session("INTERES_A_DEVOLVER") = _INTERES_A_DEVOLVER
            .Session("NRO_PAGO_EGRESO_SIN_GIRO") = _NRO_PAGO_EGRESO_SIN_GIRO
            .Session("NRO_PAGO_INGRESO") = _NRO_PAGO_INGRESO
            .Session("CORREL") = _CORREL
            .Session("proceso") = _proceso
            .Session("JDX") = _JDX
            .Session("COD_FLUJO") = _COD_FLUJO
            .Session("NRO_CUOTA") = _NRO_CUOTA
            .Session("TOTAL_ABONADO") = _TOTAL_ABONADO
            .Session("MONTO_ABONADO") = _MONTO_ABONADO
            .Session("MONTO_INTERES") = _MONTO_INTERES
            .Session("PARCIAL_TOTAL") = _PARCIAL_TOTAL
            .Session("DOCTO_CEDIDO") = _DOCTO_CEDIDO
            .Session("TIPO_CTA_X_COB") = _TIPO_CTA_X_COB
            .Session("NRO_CTA_X_COB") = _NRO_CTA_X_COB
            .Session("QUE_SE_PAGA") = _QUE_SE_PAGA
            .Session("TIPO_EGRESO") = _TIPO_EGRESO
            .Session("ANTES_DE_14_HRS") = _ANTES_DE_14_HRS
            .Session("FIJO_DEFINIDO") = _FIJO_DEFINIDO
            .Session("CODIGO_GASTO") = _CODIGO_GASTO
            .Session("NUMERO_GASTO") = _NUMERO_GASTO
            '.Session("VALOR_UF") = _VALOR_UF
            .Session("TASA_BASE") = _TASA_BASE
            .Session("SPREAD") = _SPREAD
            .Session("IMPUESTO") = _IMPUESTO
            .Session("VALOR_US") = _VALOR_US
            .Session("TASA_MAX_CONV") = _TASA_MAX_CONV
            .Session("COMISION_FLAT") = _COMISION_FLAT
            .Session("PORCENTAJE_FLAT") = _PORCENTAJE_FLAT
            .Session("PUNTOS") = _PUNTOS
            .Session("MONEDA_COMISION") = _MONEDA_COMISION
            .Session("MONEDA_COMISION_FLAT") = _MONEDA_COMISION_FLAT
            .Session("PORCENTAJE_COMISION") = _PORCENTAJE_COMISION
            .Session("COMISION_MINIMA") = _COMISION_MINIMA
            .Session("COMISION_MAXIMA") = _COMISION_MAXIMA
            .Session("PORCENTAJE_ANTICIPAR") = _PORCENTAJE_ANTICIPAR
            .Session("MONTO_ANTICIPAR") = _TOTAL_ABONADO
            .Session("PRECIO_COMPRA") = _PRECIO_COMPRA
            .Session("DIFERENCIA_PRECIO") = _DIFERENCIA_PRECIO
            .Session("SALDO_PENDIENTE") = _SALDO_PENDIENTE
            .Session("SALDO_PAGAR") = _SALDO_PAGAR
            '.Session("PAGADOR") = _PAGADOR
            .Session("IVA_COMISION") = _IVA_COMISION
            .Session("TOTAL_GASTOS") = _TOTAL_GASTOS
            .Session("OBS_SIMULACION") = _OBS_SIMULACION
            .Session("MONTO_GIRAR") = _MONTO_GIRAR
            .Session("MONTO_MIN") = _MONTO_MIN
            .Session("MONTO_MAX") = _MONTO_MAX
            .Session("MONTO_DOCUMENTO") = _MONTO_DOCUMENTO
            .Session("MONTO_ANTICIPO") = _MONTO_ANTICIPO
            .Session("FECHA_VCTO_DOCTO") = _FECHA_VCTO_DOCTO
            .Session("FECHA_VCTO_AUX") = _FECHA_VCTO_AUX
            .Session("NRO_DIAS_VCTO") = _NRO_DIAS_VCTO
            .Session("CANT_DIAS") = _CANT_DIAS
            .Session("DIF_PRECIO") = _DIF_PRECIO
            .Session("SALDO_A_PAGAR") = _SALDO_A_PAGAR
            .Session("COMISION_APLICADA") = _COMISION_APLICADA
            '.Session("FACTOR_CAMBIO_HOY") = _FACTOR_CAMBIO_HOY
            .Session("REAJUSTE") = _REAJUSTE
            '.Session("FACTOR_CAMBIO") = _FACTOR_CAMBIO
            .Session("MONTO_INTERES_A_DEVOLVER") = _MONTO_INTERES_A_DEVOLVER
            .Session("iva") = _iva
            .Session("Ponderacion_Anticipo") = _Ponderacion_Anticipo
            .Session("OBSERVACION_EGR") = _OBSERVACION_EGR
            .Session("IND_CXP") = _IND_CXP
            .Session("NRO_CXP") = _NRO_CXP
            .Session("MONTO_CXP") = _MONTO_CXP
            .Session("INTERES_DEVOLVER_OPERACION") = _INTERES_DEVOLVER_OPERACION
            .Session("TIPO_CXP") = _TIPO_CXP
            .Session("DESCRIP_CXP") = _DESCRIP_CXP
            .Session("ESTADO_CXP") = _ESTADO_CXP
            .Session("NRO_OPE_CXP") = _NRO_OPE_CXP
            .Session("CXP_CXC") = _CXP_CXC
            .Session("TIPO_CTA") = _TIPO_CTA
            .Session("FECHA") = _FECHA
            .Session("MONTO") = _MONTO
            .Session("Descripcion") = _Descripcion
            .Session("TIP_DOC") = _TIP_DOC
            .Session("NUM_DOC") = _NUM_DOC
            .Session("CANT_CUO") = _CANT_CUO
            .Session("FOGAPE") = _FOGAPE
            .Session("TIP_BEN") = _TIP_BEN
            .Session("GARANTIA") = _GARANTIA
            .Session("MTO_LIN") = _MTO_LIN
            .Session("FEC_VTO_FOG") = _FEC_VTO_FOG
            .Session("SEGURO ") = _SEGURO
            .Session("PAIS") = _PAIS
            .Session("OBJETIVO") = _OBJETIVO
            .Session("PorComFogape") = _PorComFogape
            .Session("ComisionFogape") = _ComisionFogape
            .Session("BancoEgreso") = _BancoEgreso
            .Session("CtaCteEgreso") = _CtaCteEgreso
            .Session("IDX") = _IDX
            .Session("FACTOR_CAMBIO_OPERACION") = _FACTOR_CAMBIO_OPERACION
            .Session("PAGADO_POR_DEUDOR") = _PAGADO_POR_DEUDOR
            .Session("FormatoSinMiles") = _FormatoSinMiles
            .Session("FormatoConMiles") = _MTO_LIN
            .Session("POS_P") = _POS_P
            '.Session("FACTOR_CAMBIO_OBS") = _FACTOR_CAMBIO_OBS
            '.Session("FACTOR_CAMBIO_PAGO") = _FACTOR_CAMBIO_PAGO
            .Session("NUM_OTOR") = _NUM_OTOR

            ' Continuacion 

            .Session("PASO_DESCUENTOS") = _PASO_DESCUENTOS
            .Session("PORCENTAJE_IVA") = _PORCENTAJE_IVA
            .Session("PAGADO_POR_DEUDOR") = _PAGADO_POR_DEUDOR
            .Session("RUT_DEUDOR_MARGENES") = _RUT_DEUDOR_MARGENES
            .Session("ESPERA_RESPUESTAs") = _ESPERA_RESPUESTA
            .Session("RUT_CLI_RPT") = _RUT_CLI_RPT
            .Session("ID_OPE_RPT") = _ID_OPE_RPT
        End With

    End Sub

    Private _deu As Object
    Public Shared Property deu() As Object
        Get
            Return HttpContext.Current.Session("deu")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("deu") = Value
        End Set
    End Property

    Private _cli_aux As Object
    Public Shared Property cli_aux() As Object
        Get
            Return HttpContext.Current.Session("cli_aux")
        End Get
        Set(ByVal value As Object)
            HttpContext.Current.Session("cli_aux") = value
        End Set
    End Property

    Private _dsi As Object
    Public Shared Property dsi() As Object
        Get
            Return HttpContext.Current.Session("dsi")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("dsi") = Value
        End Set
    End Property



    Private _datasetmasivo As DataSet

    Public Shared Property datasetmasivo() As DataSet
        Get
            Return HttpContext.Current.Session("datasetmasivo")
        End Get
        Set(ByVal Value As DataSet)
            HttpContext.Current.Session("datasetmasivo") = Value
        End Set
    End Property


    Private _Mto_a_Girar As Object
    Public Shared Property Mto_a_Girar() As Object
        Get
            Return HttpContext.Current.Session("Mto_a_Girar")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Mto_a_Girar") = Value
        End Set
    End Property

    Private _Mto_a_Girar_docto As Object
    Public Shared Property Mto_a_Girar_docto() As Object
        Get
            Return HttpContext.Current.Session("Mto_a_Girar_docto")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Mto_a_Girar_docto") = Value
        End Set
    End Property

    Private _Mto_ant_operacion As Object
    Public Shared Property Mto_ant_operacion() As Object
        Get
            Return HttpContext.Current.Session("Mto_ant_operacion")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Mto_ant_operacion") = Value
        End Set
    End Property

    Private _Var_Mar As String
    Public Shared Property Var_Mar() As String
        Get
            Return HttpContext.Current.Session("Var_Mar")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Var_Mar") = Value
        End Set
    End Property

    Private _valida_margen As Boolean
    Public Shared Property valida_margen() As Boolean
        Get
            Return HttpContext.Current.Session("valida_margen")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("valida_margen") = Value
        End Set
    End Property

    Private _INTERES_A_DEVOLVER As Integer
    Public Shared Property INTERES_A_DEVOLVER() As Integer
        Get
            Return HttpContext.Current.Session("INTERES_A_DEVOLVER")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("INTERES_A_DEVOLVER") = Value
        End Set
    End Property


    Private _page_dig As Integer
    Public Shared Property page_dig() As Integer
        Get
            Return HttpContext.Current.Session("page_dig")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_dig") = Value
        End Set
    End Property


    Private _page_sim As Integer
    Public Shared Property page_sim() As Integer
        Get
            Return HttpContext.Current.Session("page_sim")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_sim") = Value
        End Set
    End Property


    Private _page_otg As Integer
    Public Shared Property page_otg() As Integer
        Get
            Return HttpContext.Current.Session("page_otg")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_otg") = Value
        End Set
    End Property


    Private _page_anu As Integer
    Public Shared Property page_anu() As Integer
        Get
            Return HttpContext.Current.Session("page_anu")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_anu") = Value
        End Set
    End Property

    Private _page_recau As Integer
    Public Shared Property page_recau() As Integer
        Get
            Return HttpContext.Current.Session("page_recau")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_recau") = Value
        End Set
    End Property

    Private _page_exc As Integer
    Public Shared Property page_exc() As Integer
        Get
            Return HttpContext.Current.Session("page_exc")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_exc") = Value
        End Set
    End Property

    Private _page_cuen As Integer
    Public Shared Property page_cuen() As Integer
        Get
            Return HttpContext.Current.Session("page_cuen")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_cuen") = Value
        End Set
    End Property

    Private _page_gestion_doc As Integer
    Public Shared Property page_gestion_doc() As Integer
        Get
            Return HttpContext.Current.Session("page_gestion_doc")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_gestion_doc") = Value
        End Set
    End Property

    Private _page_pag As Integer
    Public Shared Property page_pag() As Integer
        Get
            Return HttpContext.Current.Session("page_pag")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("page_pag") = Value
        End Set
    End Property


    Private _id_dsi As Integer
    Public Shared Property id_dsi() As Integer
        Get
            Return HttpContext.Current.Session("id_dsi")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("id_dsi") = Value
        End Set
    End Property

    Private _NRO_PAGO_EGRESO_SIN_GIRO As Long
    Public Shared Property NRO_PAGO_EGRESO_SIN_GIRO() As Long
        Get
            Return HttpContext.Current.Session("NRO_PAGO_EGRESO_SIN_GIRO")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("NRO_PAGO_EGRESO_SIN_GIRO") = Value
        End Set
    End Property

    Private _NRO_PAGO_INGRESO As Long
    Public Shared Property NRO_PAGO_INGRESO() As Long
        Get
            Return HttpContext.Current.Session("NRO_PAGO_INGRESO")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("NRO_PAGO_INGRESO") = Value
        End Set
    End Property

    Private _CORREL As Long
    Public Shared Property CORREL() As Long
        Get
            Return HttpContext.Current.Session("CORREL")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("CORREL") = Value
        End Set
    End Property

    Private _proceso As Boolean
    Public Shared Property proceso() As Boolean
        Get
            Return HttpContext.Current.Session("proceso")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("proceso") = Value
        End Set
    End Property

    Private _JDX As Integer
    Public Shared Property JDX() As Integer
        Get
            Return HttpContext.Current.Session("JDX")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("JDX") = Value
        End Set
    End Property

    Private _COD_FLUJO As Long
    Public Shared Property COD_FLUJO() As Long
        Get
            Return HttpContext.Current.Session("COD_FLUJO")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("COD_FLUJO") = Value
        End Set
    End Property

    Private _NRO_CUOTA As Integer
    Public Shared Property NRO_CUOTA() As Integer
        Get
            Return HttpContext.Current.Session("NRO_CUOTA")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NRO_CUOTA") = Value
        End Set
    End Property

    Private _TOTAL_ABONADO As Double
    Public Shared Property TOTAL_ABONADO() As Double
        Get
            Return HttpContext.Current.Session("TOTAL_ABONADO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("TOTAL_ABONADO") = Value
        End Set
    End Property

    Private _MONTO_ABONADO As Double
    Public Shared Property MONTO_ABONADO() As Double
        Get
            Return HttpContext.Current.Session("MONTO_ABONADO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_ABONADO") = Value
        End Set
    End Property

    Private _MONTO_INTERES As Double
    Public Shared Property MONTO_INTERES() As Double
        Get
            Return HttpContext.Current.Session("MONTO_INTERES")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_INTERES") = Value
        End Set
    End Property

    Private _PARCIAL_TOTAL As String
    Public Shared Property PARCIAL_TOTAL() As String
        Get
            Return HttpContext.Current.Session("PARCIAL_TOTAL")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PARCIAL_TOTAL") = Value
        End Set
    End Property

    Private _DOCTO_CEDIDO As String
    Public Shared Property DOCTO_CEDIDO() As String
        Get
            Return HttpContext.Current.Session("DOCTO_CEDIDO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("DOCTO_CEDIDO") = Value
        End Set
    End Property

    Private _TIPO_CTA_X_COB As Integer
    Public Shared Property TIPO_CTA_X_COB() As Integer
        Get
            Return HttpContext.Current.Session("TIPO_CTA_X_COB")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIPO_CTA_X_COB") = Value
        End Set
    End Property

    Public Shared Property FECHA_VCTO_CALCULO() As String
        Get
            Return HttpContext.Current.Session("FECHA_VCTO_CALCULO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FECHA_VCTO_CALCULO") = Value
        End Set
    End Property

    Private _NRO_CTA_X_COB As Long
    Public Shared Property NRO_CTA_X_COB() As Long
        Get
            Return HttpContext.Current.Session("NRO_CTA_X_COB ")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("NRO_CTA_X_COB ") = Value
        End Set
    End Property

    Private _QUE_SE_PAGA As Integer
    Public Shared Property QUE_SE_PAGA() As Integer
        Get
            Return HttpContext.Current.Session("QUE_SE_PAGA ")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("QUE_SE_PAGA ") = Value
        End Set
    End Property

    Private _TIPO_EGRESO As Integer
    Public Shared Property TIPO_EGRESO() As Integer
        Get
            Return HttpContext.Current.Session("TIPO_EGRESO")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIPO_EGRESO") = Value
        End Set
    End Property

    Private _ANTES_DE_14_HRS As String
    Public Shared Property ANTES_DE_14_HRS() As String
        Get
            Return HttpContext.Current.Session("ANTES_DE_14_HRS")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("ANTES_DE_14_HRS") = Value
        End Set
    End Property

    Private _FIJO_DEFINIDO As String
    Public Shared Property FIJO_DEFINIDO() As String
        Get
            Return HttpContext.Current.Session("FIJO_DEFINIDO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FIJO_DEFINIDO") = Value
        End Set
    End Property

    Private _CODIGO_GASTO As Integer

    Public Shared Property CODIGO_GASTO() As Integer
        Get
            Return HttpContext.Current.Session("CODIGO_GASTO")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("CODIGO_GASTO") = Value
        End Set
    End Property

    Private _NUMERO_GASTO As Double

    Public Shared Property NUMERO_GASTO() As Double
        Get
            Return HttpContext.Current.Session("NUMERO_GASTO ")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("NUMERO_GASTO ") = Value
        End Set
    End Property


    Private _TASA_BASE As String
    Public Shared Property TASA_BASE() As String
        Get
            Return HttpContext.Current.Session("TASA_BASE")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("TASA_BASE") = Value
        End Set
    End Property

    Private _SPREAD As String
    Public Shared Property SPREAD() As String
        Get
            Return HttpContext.Current.Session("SPREAD")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("SPREAD") = Value
        End Set
    End Property

    Private _IMPUESTO As String
    Public Shared Property IMPUESTO() As String
        Get
            Return HttpContext.Current.Session("IMPUESTO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("IMPUESTO") = Value
        End Set
    End Property

    Private _VALOR_US As String
    Public Shared Property VALOR_US() As String
        Get
            Return HttpContext.Current.Session("VALOR_US")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("VALOR_US") = Value
        End Set
    End Property

    Private _TASA_MAX_CONV As String
    Public Shared Property TASA_MAX_CONV() As String
        Get
            Return HttpContext.Current.Session("TASA_MAX_CONV")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("TASA_MAX_CONV") = Value
        End Set
    End Property


    Private _COMISION_FLAT As String
    Public Shared Property COMISION_FLAT() As String
        Get
            Return HttpContext.Current.Session("COMISION_FLAT")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("COMISION_FLAT") = Value
        End Set
    End Property


    Private _PORCENTAJE_FLAT As Object
    Public Shared Property PORCENTAJE_FLAT() As Object
        Get
            Return HttpContext.Current.Session("PORCENTAJE_FLAT")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("PORCENTAJE_FLAT") = Value
        End Set
    End Property


    Private _PUNTOS As String
    Public Shared Property PUNTOS() As String
        Get
            Return HttpContext.Current.Session("PUNTOS")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PUNTOS") = Value
        End Set
    End Property


    Private _MONEDA_COMISION As Integer
    Public Shared Property MONEDA_COMISION() As Integer
        Get
            Return HttpContext.Current.Session("MONEDA_COMISION")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("MONEDA_COMISION") = Value
        End Set
    End Property


    Private _MONEDA_COMISION_FLAT As Integer
    Public Shared Property MONEDA_COMISION_FLAT() As Integer
        Get
            Return HttpContext.Current.Session("MONEDA_COMISION_FLAT")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("MONEDA_COMISION_FLAT") = Value
        End Set
    End Property


    Private _PORCENTAJE_COMISION As String
    Public Shared Property PORCENTAJE_COMISION() As String
        Get
            Return HttpContext.Current.Session("PORCENTAJE_COMISION")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PORCENTAJE_COMISION") = Value
        End Set
    End Property

    Private _COMISION_MINIMA As String
    Public Shared Property COMISION_MINIMA() As String
        Get
            Return HttpContext.Current.Session("COMISION_MINIMA")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("COMISION_MINIMA") = Value
        End Set
    End Property

    Private _COMISION_MAXIMA As String
    Public Shared Property COMISION_MAXIMA() As String
        Get
            Return HttpContext.Current.Session("COMISION_MAXIMA")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("COMISION_MAXIMA") = Value
        End Set
    End Property

    Private _PORCENTAJE_ANTICIPAR As String
    Public Shared Property PORCENTAJE_ANTICIPAR() As String
        Get
            Return HttpContext.Current.Session("PORCENTAJE_ANTICIPAR")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PORCENTAJE_ANTICIPAR") = Value
        End Set
    End Property

    Private _MONTO_ANTICIPAR As Double
    Public Shared Property MONTO_ANTICIPAR() As Double
        Get
            Return HttpContext.Current.Session("MONTO_ANTICIPAR")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_ANTICIPAR") = Value
        End Set
    End Property

    Private _PRECIO_COMPRA As Double
    Public Shared Property PRECIO_COMPRA() As Double
        Get
            Return HttpContext.Current.Session("PRECIO_COMPRA")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("PRECIO_COMPRA") = Value
        End Set
    End Property

    Private _DIFERENCIA_PRECIO As String

    Public Shared Property DIFERENCIA_PRECIO() As String
        Get
            Return HttpContext.Current.Session("DIFERENCIA_PRECIO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("DIFERENCIA_PRECIO") = Value
        End Set
    End Property

    Private _SALDO_PENDIENTE As String

    Public Shared Property SALDO_PENDIENTE() As String
        Get
            Return HttpContext.Current.Session("SALDO_PENDIENTE")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("SALDO_PENDIENTE") = Value
        End Set
    End Property

    Private _SALDO_PAGAR As String

    Public Shared Property SALDO_PAGAR() As String
        Get
            Return HttpContext.Current.Session("SALDO_PAGAR")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("SALDO_PAGAR") = Value
        End Set
    End Property

    Private _IVA_COMISION As String

    Public Shared Property IVA_COMISION() As String
        Get
            Return HttpContext.Current.Session("IVA_COMISION")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("IVA_COMISION") = Value
        End Set
    End Property

    Private _TOTAL_GASTOS As String

    Public Shared Property TOTAL_GASTOS() As String
        Get
            Return HttpContext.Current.Session("TOTAL_GASTOS")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("TOTAL_GASTOS") = Value
        End Set
    End Property

    Private _OBS_SIMULACION As String

    Public Shared Property OBS_SIMULACION() As String
        Get
            Return HttpContext.Current.Session("OBS_SIMULACION")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("OBS_SIMULACION") = Value
        End Set
    End Property

    Private _MONTO_GIRAR As String

    Public Shared Property MONTO_GIRAR() As String
        Get
            Return HttpContext.Current.Session("MONTO_GIRAR")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("MONTO_GIRAR") = Value
        End Set
    End Property

    Private _MONTO_MIN As Double

    Public Shared Property MONTO_MIN() As Double
        Get
            Return HttpContext.Current.Session("MONTO_MIN")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_MIN") = Value
        End Set
    End Property

    Private _MONTO_MAX As Double

    Public Shared Property MONTO_MAX() As Double
        Get
            Return HttpContext.Current.Session("MONTO_MAX")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_MAX") = Value
        End Set
    End Property


    Private _MONTO_DOCUMENTO As Double

    Public Shared Property MONTO_DOCUMENTO() As Double
        Get
            Return HttpContext.Current.Session("MONTO_DOCUMENTO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_DOCUMENTO") = Value
        End Set
    End Property

    Private _MONTO_ANTICIPO As Double

    Public Shared Property MONTO_ANTICIPO() As Double
        Get
            Return HttpContext.Current.Session("MONTO_ANTICIPO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_ANTICIPO") = Value
        End Set
    End Property

    Private _FECHA_VCTO_DOCTO As String

    Public Shared Property FECHA_VCTO_DOCTO() As String
        Get
            Return HttpContext.Current.Session("FECHA_VCTO_DOCTO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FECHA_VCTO_DOCTO") = Value
        End Set
    End Property

    Private _FECHA_VCTO_AUX As String

    Public Shared Property FECHA_VCTO_AUX() As String
        Get
            Return HttpContext.Current.Session("FECHA_VCTO_AUX")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FECHA_VCTO_AUX") = Value
        End Set
    End Property

    Private _NRO_DIAS_VCTO As Integer

    Public Shared Property NRO_DIAS_VCTO() As Integer
        Get
            Return HttpContext.Current.Session("NRO_DIAS_VCTO")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NRO_DIAS_VCTO") = Value
        End Set
    End Property

    Private _CANT_DIAS As Integer

    Public Shared Property CANT_DIAS() As Integer
        Get
            Return HttpContext.Current.Session("CANT_DIAS")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("CANT_DIAS") = Value
        End Set
    End Property

    Private _DIF_PRECIO As Double

    Public Shared Property DIF_PRECIO() As Double
        Get
            Return HttpContext.Current.Session("DIF_PRECIO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("DIF_PRECIO") = Value
        End Set
    End Property

    Private _SALDO_A_PAGAR As Double

    Public Shared Property SALDO_A_PAGAR() As Double
        Get
            Return HttpContext.Current.Session(" SALDO_A_PAGAR ")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session(" SALDO_A_PAGAR ") = Value
        End Set
    End Property

    Private _COMISION_APLICADA As Double

    Public Shared Property COMISION_APLICADA() As Double
        Get
            Return HttpContext.Current.Session("COMISION_APLICADA")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("COMISION_APLICADA") = Value
        End Set
    End Property

    Private _COMISION_TOTAL As Double

    Public Shared Property COMISION_TOTAL() As Double
        Get
            Return HttpContext.Current.Session("COMISION_TOTAL ")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("COMISION_TOTAL ") = Value
        End Set
    End Property


    Private _REAJUSTE As Double

    Public Shared Property REAJUSTE() As Double
        Get
            Return HttpContext.Current.Session("REAJUSTE")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("REAJUSTE") = Value
        End Set
    End Property

    Private _MONTO_INTERES_A_DEVOLVER As Double

    Public Shared Property MONTO_INTERES_A_DEVOLVER() As Double
        Get
            Return HttpContext.Current.Session("MONTO_INTERES_A_DEVOLVER")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_INTERES_A_DEVOLVER") = Value
        End Set
    End Property

    Private _iva As Double

    Public Shared Property iva() As Double
        Get
            Return HttpContext.Current.Session("iva")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("iva") = Value
        End Set
    End Property

    Private _Ponderacion_Anticipo As Double

    Public Shared Property Ponderacion_Anticipo() As Double
        Get
            Return HttpContext.Current.Session("Ponderacion_Anticipo")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("Ponderacion_Anticipo") = Value
        End Set
    End Property

    Private _OBSERVACION_EGR As String

    Public Shared Property OBSERVACION_EGR() As String
        Get
            Return HttpContext.Current.Session("OBSERVACION_EGR")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("OBSERVACION_EGR") = Value
        End Set
    End Property

    Private _IND_CXP As Integer

    Public Shared Property IND_CXP() As Integer
        Get
            Return HttpContext.Current.Session("IND_CXP")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("IND_CXP") = Value
        End Set
    End Property

    Private _NRO_CXP As Long

    Public Shared Property NRO_CXP() As Long
        Get
            Return HttpContext.Current.Session("NRO_CXP")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("NRO_CXP") = Value
        End Set
    End Property

    Private _MONTO_CXP As Double

    Public Shared Property MONTO_CXP() As Double
        Get
            Return HttpContext.Current.Session("MONTO_CXP")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO_CXP") = Value
        End Set
    End Property

    Private _INTERES_DEVOLVER_OPERACION As Double

    Public Shared Property INTERES_DEVOLVER_OPERACION() As Double
        Get
            Return HttpContext.Current.Session("INTERES_DEVOLVER_OPERACION")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("INTERES_DEVOLVER_OPERACION") = Value
        End Set
    End Property

    Private _TIPO_CXP As Integer

    Public Shared Property TIPO_CXP() As Integer
        Get
            Return HttpContext.Current.Session("TIPO_CXP")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIPO_CXP") = Value
        End Set
    End Property

    Private _DESCRIP_CXP As String

    Public Shared Property DESCRIP_CXP() As String
        Get
            Return HttpContext.Current.Session("DESCRIP_CXP")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("DESCRIP_CXP") = Value
        End Set
    End Property

    Private _ESTADO_CXP As Integer

    Public Shared Property ESTADO_CXP() As Integer
        Get
            Return HttpContext.Current.Session("ESTADO_CXP")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("ESTADO_CXP") = Value
        End Set
    End Property

    Private _NRO_OPE_CXP As String

    Public Shared Property NRO_OPE_CXP() As String
        Get
            Return HttpContext.Current.Session("NRO_OPE_CXP ")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("NRO_OPE_CXP ") = Value
        End Set
    End Property

    Private _CXP_CXC As Integer

    Public Shared Property CXP_CXC() As Integer
        Get
            Return HttpContext.Current.Session("CXP_CXC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("CXP_CXC") = Value
        End Set
    End Property

    Private _TIPO_CTA As Integer

    Public Shared Property TIPO_CTA() As Integer
        Get
            Return HttpContext.Current.Session("TIPO_CTA")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIPO_CTA") = Value
        End Set
    End Property

    Private _FECHA As String

    Public Shared Property FECHA() As String
        Get
            Return HttpContext.Current.Session("FECHA")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FECHA") = Value
        End Set
    End Property

    Private _MONTO As Double

    Public Shared Property MONTO() As Double
        Get
            Return HttpContext.Current.Session("MONTO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MONTO") = Value
        End Set
    End Property

    Private _Descripcion As String

    Public Shared Property Descripcion() As String
        Get
            Return HttpContext.Current.Session("Descripcion")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Descripcion") = Value
        End Set
    End Property

    Private _TIP_DOC As Integer

    Public Shared Property TIP_DOC() As Integer
        Get
            Return HttpContext.Current.Session("TIP_DOC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIP_DOC") = Value
        End Set
    End Property

    Private _num_doc As String

    Public Shared Property NUM_DOC() As Integer
        Get
            Return HttpContext.Current.Session("NUM_DOC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NUM_DOC") = Value
        End Set
    End Property

    Private _CANT_CUO As Integer

    Public Shared Property CANT_CUO() As Integer
        Get
            Return HttpContext.Current.Session("CANT_CUO")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("CANT_CUO") = Value
        End Set
    End Property

    Private _FOGAPE As String

    Public Shared Property FOGAPE() As String
        Get
            Return HttpContext.Current.Session("FOGAPE")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FOGAPE") = Value
        End Set
    End Property

    Private _TIP_BEN As Integer

    Public Shared Property TIP_BEN() As Integer
        Get
            Return HttpContext.Current.Session("TIP_BEN")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("TIP_BEN") = Value
        End Set
    End Property

    Private _GARANTIA As String

    Public Shared Property GARANTIA() As String
        Get
            Return HttpContext.Current.Session("GARANTIA")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("GARANTIA") = Value
        End Set
    End Property

    Private _MTO_LIN As Double

    Public Shared Property MTO_LIN() As Double
        Get
            Return HttpContext.Current.Session("MTO_LIN")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MTO_LIN") = Value
        End Set
    End Property
    Private _FEC_VTO_FOG As String

    Public Shared Property FEC_VTO_FOG() As String
        Get
            Return HttpContext.Current.Session("FEC_VTO_FOG")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FEC_VTO_FOG") = Value
        End Set
    End Property
    Private _SEGURO As String

    Public Shared Property SEGURO() As String
        Get
            Return HttpContext.Current.Session("SEGURO")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("SEGURO") = Value
        End Set
    End Property
    Private _PAIS As Integer

    Public Shared Property PAIS() As Integer
        Get
            Return HttpContext.Current.Session("PAIS")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("PAIS") = Value
        End Set
    End Property
    Private _OBJETIVO As Integer

    Public Shared Property OBJETIVO() As Integer
        Get
            Return HttpContext.Current.Session("OBJETIVO")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("OBJETIVO") = Value
        End Set
    End Property

    Private _PorComFogape As String

    Public Shared Property PorComFogape() As String
        Get
            Return HttpContext.Current.Session("PorComFogape")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PorComFogape") = Value
        End Set
    End Property
    Private _ComisionFogape As String

    Public Shared Property ComisionFogape() As String
        Get
            Return HttpContext.Current.Session("ComisionFogape")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("ComisionFogape") = Value
        End Set
    End Property
    Private _BancoEgreso As String

    Public Shared Property BancoEgreso() As String
        Get
            Return HttpContext.Current.Session("BancoEgreso")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("BancoEgreso") = Value
        End Set
    End Property
    Private _CtaCteEgreso As String

    Public Shared Property CtaCteEgreso() As String
        Get
            Return HttpContext.Current.Session("CtaCteEgreso")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("CtaCteEgreso") = Value
        End Set
    End Property
    Private _IDX As Integer

    Public Shared Property IDX() As Integer
        Get
            Return HttpContext.Current.Session("IDX")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("IDX") = Value
        End Set
    End Property
    Private _FACTOR_CAMBIO_OPERACION As Double

    Public Shared Property FACTOR_CAMBIO_OPERACION() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO_OPERACION")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO_OPERACION") = Value
        End Set
    End Property

    Private _PAGADO_POR_DEUDOR As String

    Public Shared Property PAGADO_POR_DEUDOR() As String
        Get
            Return HttpContext.Current.Session("PAGADO_POR_DEUDOR")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("PAGADO_POR_DEUDOR") = Value
        End Set
    End Property
    Private _FormatoSinMiles As String

    Public Shared Property FormatoSinMiles() As String
        Get
            Return HttpContext.Current.Session("FormatoSinMiles")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FormatoSinMiles") = Value
        End Set
    End Property
    Private _FormatoConMiles As String

    Public Shared Property FormatoConMiles() As String
        Get
            Return HttpContext.Current.Session("FormatoConMiles")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("FormatoConMiles") = Value
        End Set
    End Property
    Private _POS_P As Integer

    Public Shared Property POS_P() As Integer
        Get
            Return HttpContext.Current.Session("POS_P")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("POS_P") = Value
        End Set
    End Property

    Private _NUM_OTOR As Integer

    Public Shared Property NUM_OTOR() As Integer
        Get
            Return HttpContext.Current.Session("NUM_OTOR")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NUM_OTOR") = Value
        End Set
    End Property


    Private _PASO_DESCUENTOS As Boolean

    Public Shared Property PASO_DESCUENTOS() As Boolean
        Get
            Return HttpContext.Current.Session("PASO_DESCUENTOS")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("PASO_DESCUENTOS") = Value
        End Set
    End Property

    Private _PORCENTAJE_IVA As Object

    Public Shared Property PORCENTAJE_IVA() As Object
        Get
            Return HttpContext.Current.Session("PORCENTAJE_IVA")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("PORCENTAJE_IVA") = Value
        End Set
    End Property

    Private _RUT_DEUDOR_MARGENES As String

    Public Shared Property RUT_DEUDOR_MARGENES() As String
        Get
            Return HttpContext.Current.Session("RUT_DEUDOR_MARGENES")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("RUT_DEUDOR_MARGENES") = Value
        End Set
    End Property

    Private _ESPERA_RESPUESTA As Boolean

    Public Shared Property ESPERA_RESPUESTA() As Boolean
        Get
            Return HttpContext.Current.Session("ESPERA_RESPUESTA")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("ESPERA_RESPUESTA") = Value
        End Set
    End Property

    Private _RUT_CLI_RPT As String

    Public Shared Property RUT_CLI_RPT() As String
        Get
            Return HttpContext.Current.Session("RUT_CLI_RPT")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("RUT_CLI_RPT") = Value
        End Set
    End Property

    Private _ID_OPE_RPT As Integer

    Public Shared Property ID_OPE_RPT() As Integer
        Get
            Return HttpContext.Current.Session("ID_OPE_RPT")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("ID_OPE_RPT") = Value
        End Set
    End Property

    Private _NroPaginacion_Claf As Integer
    Public Shared Property NroPaginacion_Claf() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Claf")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Claf") = Value
        End Set
    End Property

    Private _GMF As String
    Public Shared Property GMF() As String
        Get
            Return HttpContext.Current.Session("GMF")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("GMF") = Value
        End Set
    End Property
#End Region

#Region "Detalle Documentos Cartola"

    Private _Coll_DetalleDocto As Collection
    Public Shared Property Coll_DetalleDocto() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DetalleDocto")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DetalleDocto") = Value
        End Set
    End Property

#End Region

End Class
