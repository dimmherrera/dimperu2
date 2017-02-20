Imports System.Web

Public NotInheritable Class SesionCobranza


    Private _CampoOrden As String
    Public Shared Property CampoOrden() As String
        Get
            Return HttpContext.Current.Session("CampoOrden")
        End Get
        Set(ByVal Value As String)
            HttpContext.Current.Session("CampoOrden") = Value
        End Set
    End Property

    Private _Deudores As Collection
    Public Shared Property Deudores() As Collection
        Get
            Return HttpContext.Current.Session("Deudores")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Deudores") = Value
        End Set
    End Property


    Private _Collection_Cobranza2 As Collection
    Public Shared Property Collection_Cobranza2() As Collection
        Get
            Return HttpContext.Current.Session("Collection_Cobranza2")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Collection_Cobranza2") = Value
        End Set
    End Property

    Private _NroPaginacion_Reemplazo As Integer
    Public Shared Property NroPaginacion_Reemplazo() As Integer
        Get
            Return HttpContext.Current.Session("NroPaginacion_Reemplazo")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("NroPaginacion_Reemplazo") = Value
        End Set
    End Property

End Class
