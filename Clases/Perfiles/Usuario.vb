Imports System.Web

Public Class Usuario

    'Private userLogin As String
    'Private userPasswd As String
    'Private userNombre As String
    'Private codPerfil As String
    'Private fechaCre As String
    'Private fechaVigPass As String
    'Private estado As Integer
    'Private cargo As Integer
    'Private ejecutivo As String
    'Private accesoInt As Integer
    'Private email As String
    'Private privez As Char


    'Public Sub New()
    '    userLogin = ""
    '    userPasswd = ""
    '    userNombre = ""
    '    codPerfil = "0"
    '    estadoUsr = 0
    '    cargo = 0
    '    ejecutivo = ""
    '    accesoInt = 0
    '    email = ""
    'End Sub

    Private _usrLogin As String
    Public Property usrLogin() As String
        Get
            Return _usrLogin
        End Get
        Set(ByVal Value As String)
            _usrLogin = Value
        End Set
    End Property

    Private _usrPWD As String
    Public Property usrPWD() As String
        Get
            Return _usrPWD
        End Get
        Set(ByVal Value As String)
            _usrPWD = Value
        End Set
    End Property

    Private _usrNAME As String
    Public Property usrNAME() As String
        Get
            Return _usrNAME
        End Get
        Set(ByVal Value As String)
            _usrNAME = Value
        End Set
    End Property

    Private _usrAPE As String
    Public Property usrAPE() As String
        Get
            Return _usrAPE
        End Get
        Set(ByVal Value As String)
            _usrAPE = Value
        End Set
    End Property

    Private _idPerfil As Integer
    Public Property idPerfil() As Integer
        Get
            Return _idPerfil
        End Get
        Set(ByVal Value As Integer)
            _idPerfil = Value
        End Set
    End Property

    Private _vigPass As String
    Public Property vigPass() As String
        Get
            Return _vigPass
        End Get
        Set(ByVal Value As String)
            _vigPass = Value
        End Set
    End Property

    Private _FecCreacion As String
    Public Property FecCreacion() As String
        Get
            Return _FecCreacion
        End Get
        Set(ByVal Value As String)
            _FecCreacion = Value
        End Set
    End Property

    Private _estadoUsr As Integer
    Public Property estadoUsr() As Integer
        Get
            Return _estadoUsr
        End Get
        Set(ByVal Value As Integer)
            _estadoUsr = Value
        End Set
    End Property

    Private _cargoUsr As Integer
    Public Property cargoUsr() As Integer
        Get
            Return _cargoUsr
        End Get
        Set(ByVal Value As Integer)
            _cargoUsr = Value
        End Set
    End Property

    Private _usrEjecutivo As String
    Public Property usrEjecutivo() As String
        Get
            Return _usrEjecutivo
        End Get
        Set(ByVal Value As String)
            _usrEjecutivo = Value
        End Set
    End Property

    Private _internetUsr As Integer
    Public Property internetUsr() As Integer
        Get
            Return _internetUsr
        End Get
        Set(ByVal Value As Integer)
            _internetUsr = Value
        End Set
    End Property

    Private _usrMail As String
    Public Property usrMail() As String
        Get
            Return _usrMail
        End Get
        Set(ByVal Value As String)
            _usrMail = Value
        End Set
    End Property

    Private _usrPrivez As Char
    Public Property usrPrivez() As Char
        Get
            Return _usrPrivez
        End Get
        Set(ByVal Value As Char)
            _usrPrivez = Value
        End Set
    End Property


    Private descripcion As String
    Public Property descPfl() As String
        Get
            Return descripcion
        End Get
        Set(ByVal Value As String)
            descripcion = Value
        End Set
    End Property


    Private _sucursalUsr As Integer
    Public Property sucursalUsr() As Integer
        Get
            Return _sucursalUsr
        End Get
        Set(ByVal Value As Integer)
            _sucursalUsr = Value
        End Set
    End Property

    Private _codBanco As String
    Public Property codBanco() As String
        Get
            Return _codBanco
        End Get
        Set(ByVal Value As String)
            _codBanco = Value
        End Set
    End Property

    Public Sub IniciaUsuario(ByVal pusrLogin As String, ByVal pusrPWD As String, _
                                        ByVal pusrNAME As String, ByVal pidPerfil As Integer, _
                                        ByVal pvigPass As String, ByVal pFecCre As String, _
                                        ByVal pestadoUsr As Integer, ByVal pcargo As Integer, _
                                        ByVal pEjecutivo As String, ByVal paccint As Integer, _
                                        ByVal pmail As String, ByVal pPrivez As Char, _
                                        ByVal psuc As Integer, ByVal pcodbco As String)
        With Me
            ._usrLogin = pusrLogin
            .usrPWD = pusrPWD
            ._usrNAME = pusrNAME
            ._idPerfil = pidPerfil
            ._vigPass = pvigPass
            ._FecCreacion = pFecCre
            ._estadoUsr = pestadoUsr
            ._cargoUsr = pcargo
            ._usrEjecutivo = pEjecutivo
            ._internetUsr = paccint
            ._usrMail = pmail
            ._usrPrivez = pPrivez
            ._sucursalUsr = psuc
            ._codBanco = pcodbco
        End With

    End Sub

End Class
