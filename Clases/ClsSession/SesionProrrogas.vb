Imports System.Web

Public Class SesionProrrogas

    Private _Accion_Pro As Integer
    Public Property Accion_Pro() As Integer
        Get
            Return HttpContext.Current.Session("Accion_Pro")
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("Accion_Pro") = value
        End Set
    End Property

    Private _RutCliente As Long
    Public Property RutCliente() As Long
        Get
            Return HttpContext.Current.Session("RutCliente")
        End Get
        Set(ByVal Value As Long)
            HttpContext.Current.Session("RutCliente") = Value
        End Set
    End Property

    Private _ID_SolicitudProrroga As Integer
    Public Property ID_SolicitudProrroga() As Integer
        Get
            Return HttpContext.Current.Session("ID_SolicitudProrroga")
        End Get
        Set(ByVal Value As Integer)
            HttpContext.Current.Session("ID_SolicitudProrroga") = Value
        End Set
    End Property

    Private _Coll_SPG As Collection
    Public Shared Property Coll_SPG() As Collection
        Get
            Return HttpContext.Current.Session("Coll_SPG")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_SPG") = Value
        End Set
    End Property

    Private _Coll_DPG As Collection
    Public Shared Property Coll_DPG() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DPG")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DPG") = Value
        End Set
    End Property

    Private _Coll_DocumentosProrroga As Collection
    Public Shared Property Coll_DocumentosProrroga() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DocumentosProrroga")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DocumentosProrroga") = Value
        End Set
    End Property

    Private _Coll_DocumentosProrroga_Seleccionados As Collection
    Public Shared Property Coll_DocumentosProrroga_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DocumentosProrroga_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DocumentosProrroga_Seleccionados") = Value
        End Set
    End Property

    Public Sub IniciarSesionPagos()

        With HttpContext.Current
            .Session("RutCliente") = _RutCliente
            .Session("ID_SolicitudProrroga") = _ID_SolicitudProrroga
        End With

    End Sub

End Class
