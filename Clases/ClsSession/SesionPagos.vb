Imports System.Web

Public NotInheritable Class SesionPagos

    Private _Coll_Pagos As Collection
    Public Property Coll_Pagos() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Pagos")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Pagos") = Value
        End Set
    End Property

    Private _Coll_hre As Collection
    Public Property Coll_hre() As Collection
        Get
            Return HttpContext.Current.Session("Coll_hre")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_hre") = Value
        End Set
    End Property
    Private _Coll_Doctos_Seleccionados As Collection
    Public Shared Property Coll_Doctos_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Doctos_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Doctos_Seleccionados") = Value
        End Set
    End Property

    Private _Coll_Cxc_Seleccionados As Collection
    Public Shared Property Coll_Cxc_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Cxc_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Cxc_Seleccionados") = Value
        End Set
    End Property

    Private _Coll_Ing_Sec As Collection
    Public Shared Property Coll_Ing_Sec() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Ing_Sec")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Ing_Sec") = Value
        End Set
    End Property

    Private _Coll_Doctos_Cxc As Collection
    Public Shared Property Coll_Doctos_Cxc() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Doctos_Cxc")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Doctos_Cxc") = Value
        End Set
    End Property

    Private _Coll_DPO As Collection
    Public Shared Property Coll_DPO() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DPO")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DPO") = Value
        End Set
    End Property

    Private _Coll_EXC As Collection
    Public Shared Property Coll_EXC() As Collection
        Get
            Return HttpContext.Current.Session("Coll_EXC")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_EXC") = Value
        End Set
    End Property

    Private _Coll_CXP As Collection
    Public Shared Property Coll_CXP() As Collection
        Get
            Return HttpContext.Current.Session("Coll_CXP")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_CXP") = Value
        End Set
    End Property

    Private _coll_egr As Collection
    Public Shared Property coll_egr() As Collection
        Get
            Return HttpContext.Current.Session("coll_egr")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_egr") = Value
        End Set
    End Property

    Private _coll_egr_giro As Collection
    Public Shared Property coll_egr_giro() As Collection
        Get
            Return HttpContext.Current.Session("coll_egr_giro")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_egr_giro") = Value
        End Set
    End Property

    Private _coll_egr_sec As Collection
    Public Shared Property coll_egr_sec() As Collection
        Get
            Return HttpContext.Current.Session("coll_egr_sec")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_egr_sec") = Value
        End Set
    End Property

    Private _coll_egr_sec_giro As Collection
    Public Shared Property coll_egr_sec_giro() As Collection
        Get
            Return HttpContext.Current.Session("coll_egr_sec_giro")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("coll_egr_sec_giro") = Value
        End Set
    End Property

    Private _Coll_Pagare As Collection
    Public Property Coll_Pagare() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Pagare")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Pagare") = Value
        End Set
    End Property

    Private _Coll_Avales As Collection
    Public Property Coll_Avales() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Avales")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Avales") = Value
        End Set
    End Property

    Private _Coll_Apli As Collection
    Public Property Coll_Apli() As Collection
        Get
            Return HttpContext.Current.Session("Coll_Apli")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_Apli") = Value
        End Set
    End Property
#Region "Propiedades"

    Private _RutCliente As Long
    Public Shared Property RutCliente() As Long
        Get
            Return HttpContext.Current.Session("RutCliente")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("RutCliente") = Value
        End Set
    End Property

    Private _RutDeudor As Long
    Public Shared Property RutDeudor() As Long
        Get
            Return HttpContext.Current.Session("RutDeudor")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("RutDeudor") = Value
        End Set
    End Property

    Private _Pagador As Char
    Public Shared Property Pagador() As Char
        Get
            Return HttpContext.Current.Session("Pagador")
        End Get
        Set(ByVal Value As Char)
            HttpContext.Current.Session("Pagador") = Value
        End Set
    End Property

    Private _DiasRetencionPago As Integer
    Public Shared Property DiasRetencionPago() As Integer
        Get
            Return HttpContext.Current.Session("DiasRetencionPago")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("DiasRetencionPago") = Value
        End Set
    End Property

    Private _TasaInteresCalculo As Decimal
    Public Shared Property TasaInteresCalculo() As Decimal
        Get
            Return HttpContext.Current.Session("TasaInteresCalculo")
        End Get
        Set(ByVal Value As Decimal)
            HttpContext.Current.Session("TasaInteresCalculo") = Value
        End Set
    End Property

    Private _DiasDevolverInteres As Integer
    Public Shared Property DiasDevolverInteres() As Integer
        Get
            Return HttpContext.Current.Session("DiasDevolverInteres")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("DiasDevolverInteres") = Value
        End Set
    End Property

    Private _FechaPago As DateTime
    Public Shared Property FechaPago() As DateTime
        Get
            Return HttpContext.Current.Session("FechaPago")
        End Get
        Set(ByVal Value As DateTime)
            HttpContext.Current.Session("FechaPago") = Value
        End Set
    End Property

    Private _TotalRecaudado As Double
    Public Shared Property TotalRecaudado() As Double
        Get
            Return HttpContext.Current.Session("TotalRecaudado")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("TotalRecaudado") = Value
        End Set
    End Property

    Private _DollarCobranza As Double
    Public Shared Property DollarCobranza() As Double
        Get
            Return HttpContext.Current.Session("DollarCobranza")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("DollarCobranza") = Value
        End Set
    End Property

    Private _DollarObservador As Double
    Public Shared Property DollarObservador() As Double
        Get
            Return HttpContext.Current.Session("DollarObservador")
        End Get
        Set(ByVal Value As Double)
            HttpContext.Current.Session("DollarObservador") = Value
        End Set
    End Property

    Private _Ayuda As Boolean
    Public Shared Property Ayuda() As Boolean
        Get
            Return HttpContext.Current.Session("Ayuda")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("Ayuda") = Value
        End Set
    End Property

    Private _Confirma_seleccion As Boolean

    Public Shared Property Confirma_seleccion() As Boolean
        Get
            Return HttpContext.Current.Session("Confirma_seleccion")
        End Get
        Set(ByVal Value As Boolean)
            HttpContext.Current.Session("Confirma_seleccion") = Value
        End Set
    End Property

    Public Sub IniciarSesionPagos()

        With HttpContext.Current
            .Session("RutCliente") = _RutCliente
            .Session("RutDeudor") = _RutDeudor
            .Session("Pagador") = _Pagador
            .Session("DiasRetencionPago") = _DiasRetencionPago
            .Session("TasaInteresCalculo") = _TasaInteresCalculo
            .Session("DiasDevolverInteres") = _DiasDevolverInteres
            .Session("FechaPago") = _FechaPago
            .Session("ayuda") = _Ayuda

        End With

    End Sub

#End Region

End Class
