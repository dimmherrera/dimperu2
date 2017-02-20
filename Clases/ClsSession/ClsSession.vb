Imports System.Web

Public NotInheritable Class ClsSession

#Region "VARIAS GENERALES"

    Private _NroPaginacionPag As Integer
    Public Shared Property NroPaginacionPag() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacionPag")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacionPag") = Value
        End Set
    End Property

    Private _NroPaginacionCli As Integer
    Public Shared Property NroPaginacionCli() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacionCli")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacionCli") = Value
        End Set
    End Property

    Private _Skin As Integer
    Public Shared Property Skin() As Integer
        Get
            Return HttpContext.Current.Session("Skin")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("Skin") = Value
        End Set
    End Property

    Private _MES_ As Integer
    Public Shared Property MES_() As Integer
        Get
            Return HttpContext.Current.Session("MES_")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("MES_") = Value
        End Set
    End Property

    Private _arr_gto_fij_ As ArrayList
    Public Shared Property arr_gto_fij_() As ArrayList
        Get
            Return HttpContext.Current.Session("arr_gto_fij")
        End Get
        Set(ByVal Value As ArrayList)
            HttpContext.Current.Session("arr_gto_fij") = Value
        End Set
    End Property

    Private _arr_gto_def_ As ArrayList
    Public Shared Property arr_gto_def_() As ArrayList
        Get
            Return HttpContext.Current.Session("arr_gto_def")
        End Get
        Set(ByVal Value As ArrayList)
            HttpContext.Current.Session("arr_gto_def") = Value
        End Set
    End Property


    Private _AÑO_ As Integer
    Public Shared Property AÑO_() As Integer
        Get
            Return HttpContext.Current.Session("AÑO_")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("AÑO_") = Value
        End Set
    End Property

    Private _Total As Integer
    Public Shared Property Total() As Integer
        Get
            Return HttpContext.Current.Session("Total")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("Total") = Value
        End Set
    End Property

    Private _SW As Integer
    Public Shared Property SW() As Integer
        Get
            Return HttpContext.Current.Session("SW")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("SW") = Value
        End Set
    End Property

    Private _SW_Rec As Integer
    Public Shared Property SW_Rec() As Integer
        Get
            Return HttpContext.Current.Session("SW_Rec")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("SW_Rec") = Value
        End Set
    End Property
    
    'Private _IP As String
    'Public Shared Property IP() As String
    '    Get
    '        Return HttpContext.Current.Session("IP")
    '    End Get
    '    Set(ByVal Value As String)
    '        HttpContext.Current.Session("IP") = Value
    '    End Set
    'End Property

    Private _Modulo As String
    Public Shared Property Modulo() As String
        Get
            Return HttpContext.Current.Session("Modulo")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Modulo") = Value
        End Set
    End Property

    Private _Usr As String
    Public Shared Property Usr() As String
        Get
            Return HttpContext.Current.Session("Usr")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Usr") = Value
        End Set
    End Property

    Private _Pfl As String
    Public Shared Property Pfl() As String
        Get
            Return HttpContext.Current.Session("Pfl")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Pfl") = Value
        End Set
    End Property

    Private _Perfil As String
    Public Shared Property Perfil() As String
        Get
            Return HttpContext.Current.Session("Perfil")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Perfil") = Value
        End Set
    End Property

    Private _Clave As String
    Public Shared Property Clave() As String
        Get
            Return HttpContext.Current.Session("Clave")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("Clave") = Value
        End Set
    End Property

    Private _NomMaq As String
    Public Shared Property NomMaq() As String
        Get
            Return HttpContext.Current.Session("NomMaq")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("NomMaq") = Value
        End Set
    End Property

    Private _NroPass As String
    Public Shared Property NroPass() As String
        Get
            Return HttpContext.Current.Session("NroPass")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("NroPass") = Value
        End Set
    End Property

    Private _CodEje As Integer
    Public Shared Property CodEje() As Integer
        Get
            Return HttpContext.Current.Session("CodEje")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("CodEje") = Value
        End Set
    End Property

    Private _Ejecutivo As Integer
    Public Shared Property Ejecutivo() As Integer
        Get
            Return HttpContext.Current.Session("Ejecutivo")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("Ejecutivo") = Value
        End Set
    End Property

    Private _NombreEjecutivo As String
    Public Shared Property NombreEjecutivo() As String
        Get
            Return HttpContext.Current.Session("NombreEjecutivo")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("NombreEjecutivo") = Value
        End Set
    End Property

    Private _RutCli As Long
    Public Shared Property RutCli() As Long
        Get
            Return HttpContext.Current.Session("RutCli")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("RutCli") = Value
        End Set
    End Property

    Private _RutDeu As Long
    Public Shared Property RutDeu() As Long
        Get
            Return HttpContext.Current.Session("RutDeu")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("RutDeu") = Value
        End Set
    End Property

    Private _NroRow As Int32
    Public Shared Property NroRow() As Int32
        Get
            Return HttpContext.Current.Session("NroRow")
        End Get
        Set(ByVal Value As Int32)
            HttpContext.Current.Session("NroRow") = Value
        End Set

    End Property

    Private _ccfnum As Int32
    Public Shared Property ccfnum() As Int32
        Get
            Return HttpContext.Current.Session("ccfnum")
        End Get
        Set(ByVal Value As Int32)
            HttpContext.Current.Session("ccfnum") = Value
        End Set

    End Property

    Private _modifica As Boolean
    Public Shared Property modifica() As Boolean
        Get
            Return HttpContext.Current.Session("modifica")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("modifica") = Value
        End Set

    End Property

    Private _TipoInsert As String
    Public Shared Property TipoInsert() As String
        Get
            Return HttpContext.Current.Session("TipoInsert")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("TipoInsert") = Value
        End Set
    End Property

    Private _TipoDeContacto As Int32
    Public Shared Property TipoDeContacto() As Int32
        Get
            Return HttpContext.Current.Session("TipoDeContacto")
        End Get
        Set(ByVal Value As Int32)
            HttpContext.Current.Session("TipoDeContacto") = Value
        End Set

    End Property

    Private _NroLineaCredito As Integer
    Public Shared Property NroLineaCredito() As Integer
        Get
            Return HttpContext.Current.Session("NroLineaCredito")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroLineaCredito") = Value
        End Set
    End Property

    Private _MtoLineaCredito As Double
    Public Shared Property MtoLineaCredito() As Double
        Get
            Return HttpContext.Current.Session("MtoLineaCredito")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MtoLineaCredito") = Value
        End Set
    End Property

    Private _MtoLineaCreditoApr As Double
    Public Shared Property MtoLineaCreditoApr() As Double
        Get
            Return HttpContext.Current.Session("MtoLineaCreditoApr")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("MtoLineaCreditoApr") = Value
        End Set
    End Property

    'Private _CodigoSucursal As String
    'Public Shared Property CodigoSucursal() As String
    '    Get
    '        Return HttpContext.Current.Session("CodigoSucursal")
    '    End Get
    '    Set(ByVal Value As String)
    '        HttpContext.Current.Session("CodigoSucursal") = Value
    '    End Set
    'End Property

    Private _NroNegociacion As Integer
    Public Shared Property NroNegociacion() As Integer
        Get
            Return HttpContext.Current.Session("NroNegociacion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroNegociacion") = Value
        End Set
    End Property

    Private _NroEvaluacion As Integer
    Public Shared Property NroEvaluacion() As Integer
        Get
            Return HttpContext.Current.Session("NroEvaluacion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroEvaluacion") = Value
        End Set
    End Property

    Private _NroOperacion As Integer
    Public Shared Property NroOperacion() As Integer
        Get
            Return HttpContext.Current.Session("NroOperacion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroOperacion") = Value
        End Set
    End Property

    Private _NroPaginacion_Condicion As Integer
    Public Shared Property NroPaginacion_Condicion() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Condicion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Condicion") = Value
        End Set
    End Property

    Private _NroPaginacion_DetalleOpe As Integer
    Public Shared Property NroPaginacion_DetalleOpe() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_DetalleOpe")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_DetalleOpe") = Value
        End Set
    End Property

    Private _NroPaginacion_Docto_a_Pagar As Integer
    Public Shared Property NroPaginacion_Docto_a_Pagar() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Docto_a_Pagar")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Docto_a_Pagar") = Value
        End Set
    End Property

    Private _NroPaginacion_Recaudacion As Integer
    Public Shared Property NroPaginacion_Recaudacion() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Recaudacion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Recaudacion") = Value
        End Set
    End Property

    Private _NroPaginacion_TasaMax As Integer
    Public Shared Property NroPaginacion_TasaMax() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_TasaMax")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_TasaMax") = Value
        End Set
    End Property

    Private _NroPaginacion_TasaBase As Integer
    Public Shared Property NroPaginacion_TasaBase() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_TasaBase")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_TasaBase") = Value
        End Set
    End Property


    Private _NroPaginacion_TasaImpuesto As Integer
    Public Shared Property NroPaginacion_TasaImpuesto() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_TasaImpuesto")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_TasaImpuesto") = Value
        End Set
    End Property

    Private _NroPaginacion_Sucursal As Integer
    Public Shared Property NroPaginacion_Sucursal() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Sucursal")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Sucursal") = Value
        End Set
    End Property

    Private _NroPaginacion_Plaza As Integer
    Public Shared Property NroPaginacion_Plaza() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Plaza")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Plaza") = Value
        End Set
    End Property



    Private _caso As Integer
    Public Shared Property caso() As Integer
        Get
            Return HttpContext.Current.Session("caso")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("caso") = Value
        End Set
    End Property

    Private _Sucursal As Integer
    Public Shared Property Sucursal() As Integer
        Get
            Return HttpContext.Current.Session("Sucursal")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("Sucursal") = Value
        End Set
    End Property

    Private _NroPaginacion As Integer
    Public Shared Property NroPaginacion() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion") = Value
        End Set
    End Property

    Private _NroPaginacionCXC As Integer
    Public Shared Property NroPaginacionCXC() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacionCXC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacionCXC") = Value
        End Set
    End Property


    Private _NroPaginacionCXP As Integer
    Public Shared Property NroPaginacionCXP() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacionCXP")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacionCXP") = Value
        End Set
    End Property

    Private _NroPaginacionDNC As Integer
    Public Shared Property NroPaginacionDNC() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacionDNC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacionDNC") = Value
        End Set
    End Property

    Private _NroPaginacion_Deu As Integer
    Public Shared Property NroPaginacion_Deu() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Deu")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Deu") = Value
        End Set
    End Property

    Private _NroPaginacion_DetalleSolProrroga As Integer
    Public Shared Property NroPaginacion_DetalleSolProrroga() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_DetalleSolProrroga")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_DetalleSolProrroga") = Value
        End Set
    End Property

    Private _NroPaginacion_AlertaDoctoxVencer As Integer
    Public Shared Property NroPaginacion_AlertaDoctoxVencer() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_AlertaDoctoxVencer")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_AlertaDoctoxVencer") = Value
        End Set
    End Property

    Private _NroPaginacion_AlertaDoctoEnMora As Integer
    Public Shared Property NroPaginacion_DoctoEnMora() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_AlertaDoctoEnMora")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_AlertaDoctoEnMora") = Value
        End Set
    End Property

    Private _NroPaginacion_AlertaEnLinea As Integer
    Public Shared Property NroPaginacion_AlertaEnLinea() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_AlertaEnLinea")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_AlertaEnLinea") = Value
        End Set
    End Property

    Private _NroPaginacion_AlertaExcedente As Integer
    Public Shared Property NroPaginacion_AlertaExcedente() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_AlertaExcedente")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_AlertaExcedente") = Value
        End Set
    End Property


    Private _NroPaginacion_Eje As Integer
    Public Shared Property NroPaginacion_Eje() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Eje")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Eje") = Value
        End Set
    End Property

    Private _NroPaginacion_Docto As Integer
    Public Shared Property NroPaginacion_Docto() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Docto")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Docto") = Value
        End Set
    End Property

    Private _NroPagina As Integer
    Public Shared Property NroPagina() As Integer
        Get
            Return HttpContext.Current.Session("NroPagina")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPagina") = Value
        End Set
    End Property

    Private _NroPaginaCxC As Integer
    Public Shared Property NroPaginaCxC() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginaCxC")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginaCxC") = Value
        End Set
    End Property


    Private _NroHojaRuta As Integer
    Public Shared Property NroHojaRuta() As Integer
        Get
            Return HttpContext.Current.Session("NroHojaRuta")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroHojaRuta") = Value
        End Set
    End Property

    Private _valida_cliente As String
    Public Shared Property valida_cliente() As String
        Get
            Return HttpContext.Current.Session("valida_cliente")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("valida_cliente") = Value
        End Set
    End Property

#End Region

#Region "OBJETOS GENERALES"

    Private _LineaCredito As Object
    Public Shared Property LineaCredito() As Object
        Get
            Return HttpContext.Current.Session("LineaCredito")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("LineaCredito") = Value
        End Set
    End Property

    Private _ResumenCliente As Object
    Public Shared Property ResumenCliente() As Object
        Get
            Return HttpContext.Current.Session("ResumenCliente")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("ResumenCliente") = Value
        End Set
    End Property

    Private _Anticipos As Object
    Public Shared Property Anticipos() As Object
        Get
            Return HttpContext.Current.Session("Anticipos")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Anticipos") = Value
        End Set
    End Property

    Private _SubLineaDeudor As Object
    Public Shared Property SubLineaDeudor() As Object
        Get
            Return HttpContext.Current.Session("SubLineaDeudor")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("SubLineaDeudor") = Value
        End Set
    End Property

    Private _SubLineaProducto As Object
    Public Shared Property SubLineaProducto() As Object
        Get
            Return HttpContext.Current.Session("SubLineaProducto")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("SubLineaProducto") = Value
        End Set
    End Property

    Private _Negociacion As Object
    Public Shared Property Negociacion() As Object
        Get
            Return HttpContext.Current.Session("Negociacion")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Negociacion") = Value
        End Set
    End Property

    Private _Operacion As Object
    Public Shared Property Operacion() As Object
        Get
            Return HttpContext.Current.Session("Operacion")
        End Get
        Set(ByVal Value As Object)
            HttpContext.Current.Session("Operacion") = Value
        End Set
    End Property

#End Region

#Region "COLLECTION"

    Private _Coll_Cupo As Collection
    Public Shared Property Coll_Cupo() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Cupo")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Cupo") = Value
        End Set
    End Property

    Private _Coll_Eje As Collection
    Public Shared Property Coll_Eje() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Eje")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Eje") = Value
        End Set
    End Property

    Private _Coll_Eva As Collection
    Public Shared Property Coll_Eva() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Eva")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Eva") = Value
        End Set
    End Property

    Private _Coll_Cobranza As Collection
    Public Shared Property Coll_Cobranza() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Cobranza")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Cobranza") = Value
        End Set
    End Property

    Private _Coll_CHRDetalle As Collection
    Public Shared Property Coll_CHRDetalle() As Collection
        Get
            Return HttpContext.Current.Session("Coll_CHRDetalle")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_CHRDetalle") = Value
        End Set
    End Property

    Private _Coll_Pagare As Collection
    Public Shared Property Coll_Pagare() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Pagare")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Pagare") = Value
        End Set
    End Property

    Private _coll_Dvf As Collection
    Public Shared Property coll_Dvf() As Collection
        Get
            Return HttpContext.Current.Session("coll_Dvf")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_Dvf") = Value
        End Set
    End Property

    Private _coll_nce As Collection
    Public Shared Property coll_nce() As Collection
        Get
            Return HttpContext.Current.Session("coll_nce")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_nce") = Value
        End Set
    End Property
    Private _coll_Ver As Collection
    Public Shared Property coll_Ver() As Collection
        Get
            Return HttpContext.Current.Session("coll_Ver")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_Ver") = Value
        End Set
    End Property

    Private _coll_cfc As Collection
    Public Shared Property coll_cfc() As Collection
        Get
            Return HttpContext.Current.Session("coll_cfc")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_cfc") = Value
        End Set
    End Property

    Private _coll_ccf As Collection
    Public Shared Property coll_ccf() As Collection
        Get
            Return HttpContext.Current.Session("coll_ccf")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ccf") = Value
        End Set
    End Property

    Private _coll_dsi_simu As Collection
    Public Shared Property coll_dsi_simu() As Collection
        Get
            Return HttpContext.Current.Session("coll_dsi_simu")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_dsi_simu") = Value
        End Set
    End Property

    Private _coll_sxa As Collection
    Public Shared Property coll_sxa() As Collection
        Get
            Return HttpContext.Current.Session("coll_sxa")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_sxa") = Value
        End Set
    End Property

    Private _coll_ope_anu As Collection
    Public Shared Property coll_ope_anu() As Collection
        Get
            Return HttpContext.Current.Session("coll_ope_anu")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ope_anu") = Value
        End Set
    End Property

    Private _coll_ldc As Collection
    Public Shared Property coll_ldc() As Collection
        Get
            Return HttpContext.Current.Session("coll_ldc")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_ldc") = Value
        End Set
    End Property

    Private _coll_apc As Collection
    Public Shared Property coll_apc() As Collection
        Get
            Return HttpContext.Current.Session("coll_apc")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_apc") = Value
        End Set
    End Property

    Private _coll_pnu As Collection
    Public Shared Property coll_pnu() As Collection
        Get
            Return HttpContext.Current.Session("coll_pnu")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_pnu") = Value
        End Set
    End Property

    Private _Coll_DEU As Collection
    Public Shared Property Coll_DEU() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DEU")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DEU") = Value
        End Set
    End Property

    Private _coll_chr As Collection
    Public Shared Property coll_chr() As Collection
        Get
            Return HttpContext.Current.Session("coll_chr")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_chr") = Value
        End Set
    End Property

    Private _coll_nrd As Collection
    Public Shared Property coll_nrd() As Collection
        Get
            Return HttpContext.Current.Session("coll_nrd")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_nrd") = Value
        End Set
    End Property

    Private _coll_CXC As Collection
    Public Shared Property coll_CXC() As Collection
        Get
            Return HttpContext.Current.Session("coll_CXC")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_CXC") = Value
        End Set
    End Property

    Private _coll_DNC As Collection
    Public Shared Property coll_DNC() As Collection
        Get
            Return HttpContext.Current.Session("coll_DNC")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_DNC") = Value
        End Set
    End Property

    Private _Coll_Ctr As Collection
    Public Shared Property Coll_Ctr() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Ctr")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Ctr") = Value
        End Set
    End Property

#End Region

#Region "MONEDA"

    Private _VALOR_EURO As Double
    Public Shared Property VALOR_EURO() As Double
        Get
            Return HttpContext.Current.Session("VALOR_EURO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("VALOR_EURO") = Value
        End Set
    End Property

    Private _FACTOR_CAMBIO_OBS_HOY As Double
    Public Shared Property FACTOR_CAMBIO_OBS_HOY() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO_OBS_HOY")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO_OBS_HOY") = Value
        End Set
    End Property

    Private _FACTOR_CAMBIO As Double
    Public Shared Property FACTOR_CAMBIO() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO") = Value
        End Set
    End Property

    Private _FACTOR_CAMBIO_HOY As Double
    Public Shared Property FACTOR_CAMBIO_HOY() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO_HOY")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO_HOY") = Value
        End Set
    End Property

    Private _VALOR_UF As String
    Public Shared Property VALOR_UF() As String
        Get
            Return HttpContext.Current.Session("VALOR_UF")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("VALOR_UF") = Value
        End Set
    End Property

    Private _FACTOR_CAMBIO_OBS As Double
    Public Shared Property FACTOR_CAMBIO_OBS() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO_OBS")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO_OBS") = Value
        End Set
    End Property

    Private _FACTOR_CAMBIO_PAGO As Double
    Public Shared Property FACTOR_CAMBIO_PAGO() As Double
        Get
            Return HttpContext.Current.Session("FACTOR_CAMBIO_PAGO")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("FACTOR_CAMBIO_PAGO") = Value
        End Set
    End Property

#End Region

    Public Sub iniciarSesion()

        With HttpContext.Current
            .Session("Skin") = _Skin
            .Session("valida_cliente") = _valida_cliente
            .Session("Usr") = _Usr
            .Session("Pfl") = _Pfl
            .Session("Perfil") = _Perfil
            .Session("Clave") = _Clave
            .Session("NomMaq") = _NomMaq
            .Session("NroPass") = _NroPass
            .Session("CodEje") = _CodEje
            .Session("Ejecutivo") = _Ejecutivo
            .Session("NombreEjecutivo") = _NombreEjecutivo
            .Session("RutCli") = _RutCli
            .Session("RutDeu") = _RutDeu
            .Session("NroRow") = _NroRow
            .Session("TipoDeContacto") = _TipoDeContacto
            .Session("NroEvaluacion") = _NroEvaluacion
            .Session("NroNegociacion") = _NroNegociacion
            .Session("NroOperacion") = _NroOperacion
            .Session("Sucursal") = _Sucursal
            .Session("NroPaginacion") = _NroPaginacion
            .Session("NroPagina") = _NroPagina
            .Session("SW") = _SW

        End With

    End Sub

    Public Sub iniciarSesionMonedas()

        With HttpContext.Current
            .Session("VALOR_EURO") = _VALOR_EURO
            .Session("FACTOR_CAMBIO_OBS_HOY") = _FACTOR_CAMBIO_OBS_HOY
            .Session("FACTOR_CAMBIO") = _FACTOR_CAMBIO
            .Session("FACTOR_CAMBIO_HOY") = _FACTOR_CAMBIO_HOY
            .Session("VALOR_UF") = _VALOR_UF
        End With

    End Sub

End Class




