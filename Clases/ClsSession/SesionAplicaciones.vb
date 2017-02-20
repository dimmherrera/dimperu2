Imports System.Web

Public NotInheritable Class SesionAplicaciones

    Private _Coll_EXC_Seleccionados As Collection
    Public Shared Property Coll_EXC_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_EXC_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_EXC_Seleccionados") = Value
        End Set
    End Property

    Private _Coll_CXP_Seleccionados As Collection
    Public Shared Property Coll_CXP_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_CXP_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_CXP_Seleccionados") = Value
        End Set
    End Property

    Private _Coll_DNC_Seleccionados As Collection
    Public Shared Property Coll_DNC_Seleccionados() As Collection
        Get
            Return HttpContext.Current.Session("Coll_DNC_Seleccionados")
        End Get
        Set(ByVal Value As Collection)
            HttpContext.Current.Session("Coll_DNC_Seleccionados") = Value
        End Set
    End Property

    


End Class
