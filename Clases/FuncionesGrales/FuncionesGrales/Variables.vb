Public Class Variables

    Public Titulo As String = "FactoringNet"
    Public Coll_CLI As New Collection

    Public Ejecutivo As Int16 = 15
    'Public  Cobrador As Int16 = 29

    'Public  FMT_DECIMAL As String = "##.##"     'Formato DECIMAL CON 2 DECIMALES
    'Public  FMT_DECIMAL_UF As String = "##.####"     'Formato DECIMAL CON 4 DECIMALES
    Public FMT_FECHA As String = "dd/MM/yyyy"     'Formato Fecha
    Public FMT_RUT As String = "000000000000"     'Formato RUT
    Public FMT_SUC As String = "000"     'Formato SUCURSAL
    Public FMT_BCO As String = "000000"     'Formato SUCURSAL
    Public FMT_FECHA_NULA As String = "01/01/1900"  'Formato Fecha Nula
    'Public  fmt_dec As String = "."
    'Public  fmt_cdm As String = "0,00"
    'Public  fmt_sdm As String = "0"

    'Variables Parámetros
    Public CODIGO_SUCURSAL As String = "001"
    Public TAB_ESTCOB As Int16 = 1        'estado de cobranza
    Public TAB_COMUNA As Int16 = 2        'comuna
    Public TAB_ESTDEU As Int16 = 3        'estado deudor
    Public TAB_SUCFAC As Int16 = 4        'sucursal factoring
    Public TAB_SEXO As Int16 = 5          'sexo
    Public TAB_GIRO As Int16 = 6          'giro
    Public TAB_MODOPE As Int16 = 7        'modo de operación
    Public TAB_ESTCLI As Int16 = 8        'estado cliente
    Public TAB_TIPOPERAT As Int16 = 9     'tipo operatoria
    Public TAB_ESTDOCTO As Int16 = 11     'estado documento
    Public TAB_TIPOPE As Int16 = 12       'tipo operación
    Public TAB_SUCCOB As Int16 = 13       'sucursal factoring
    Public TAB_SUCREC As Int16 = 14       'sucursal recaudación
    Public TAB_CARTATIP As Int16 = 15     'carta tipo
    Public TAB_CIUDAD As Int16 = 16       'ciudad
    Public TAB_ZONA As Int16 = 17         'zona
    Public TAB_FORPAGO As Int16 = 18      'forma de pago
    Public TAB_ESTDOCHOJ As Int16 = 19    'estado de docto. hoja de recaudacion
    Public TAB_TIPDOCPAG As Int16 = 20    'tipo de docto pago
    Public TAB_TIPPAGARE As Int16 = 21    'tipo pagaré
    Public TAB_ESTPAGARE As Int16 = 22    'estado pagaré
    Public TAB_MONEDA As Int16 = 23       'moneda
    Public TAB_TIPGARANT As Int16 = 24    'tipo garantia
    Public TAB_REGMATRIM As Int16 = 25    'regimen matrimonial
    Public TAB_TIPOAVAL As Int16 = 26     'tipo aval
    Public TAB_ESTAVAL As Int16 = 27      'estado aval
    Public TAB_ESTSOLIC As Int16 = 28     'estado solicitud
    Public TAB_ESTLINEA As Int16 = 29     'estado linea
    Public TAB_ESTOPERA As Int16 = 30     'estado operación
    Public TAB_TIPDOCTO As Int16 = 31     'tipo documento
    Public TAB_FOROPERA As Int16 = 32     'forma de operación
    Public TAB_ESTRENOV As Int16 = 33     'estado renovación
    Public TAB_TIPOTASA As Int16 = 34     'tipo tasa
    Public TAB_ESTPROSIM As Int16 = 35    'estado de proceso de simulación
    Public TAB_TIPGASTO As Int16 = 36     'tipo gasto
    Public TAB_TIPDESCTO As Int16 = 37    'tipo descuento
    Public TAB_ORIGENPAG As Int16 = 39    'origen de pago
    Public TAB_ESTVERFIC As Int16 = 40    'estado de verificación
    Public TAB_TIPCTACOB As Int16 = 41    'tipo de cuenta por cobrar
    Public TAB_TIPCOMISI As Int16 = 42    'tipo de comisión
    Public TAB_INSTLETRA As Int16 = 43    'instrucción para letra
    Public TAB_TIPCLIENT As Int16 = 44    'tipo cliente
    Public TAB_TIPEJECUT As Int16 = 45    'tipo ejecutivo
    Public TAB_PLAZABANC As Int16 = 47    'plaza bancos
    Public TAB_ESTEJECUT As Int16 = 48    'estado ejecutivo
    Public TAB_CATRIESGO As Int16 = 58    'Categoria de Riesgo
    Public TAB_MOTIVOPRO As Int16 = 61    'Motivos de Protestos
    Public TAB_BANCLIENTE As Int16 = 66   'Banca cliente
    Public TAB_ABREVIATU As Int16 = 63    'Abreviaturas Razones Sociales
    Public TAB_ACTIECONO As Int16 = 64    'Actividad economica
    Public TAB_SEGMENTO As Int16 = 76     'Actividad economica
    Public EJE_COBRADORT As Int16 = 29    'cobrador telefonico
    Public TAB_ZONARECAUDA As Int16 = 80    'zona de recaudacion

    Public opesimu As DataSet

End Class

Public Class Errores

    Private _CodigoError As Integer
    Property CodigoError() As Integer
        Get
            Return _CodigoError
        End Get
        Set(ByVal value As Integer)
            _CodigoError = value
        End Set
    End Property

    Private _MsgError As String
    Property MsgError() As String
        Get
            Return _MsgError
        End Get
        Set(ByVal value As String)
            _MsgError = value
        End Set
    End Property

    Public Sub New(ByVal pCod As Integer, ByVal Pmsg As String)
        _CodigoError = pCod
        _MsgError = Pmsg
    End Sub

    Public Sub New()
        _CodigoError = 0
        _MsgError = ""
    End Sub

End Class

Public Class validalinea


    Private _id_ldc As Double
    Public Property id_ldc() As Integer
        Get
            Return _id_ldc
        End Get
        Set(ByVal value As Integer)
            _id_ldc = value
        End Set
    End Property

    Private _mto_apro As Double
    Public Property mto_apro() As Double
        Get
            Return _mto_apro
        End Get
        Set(ByVal value As Double)
            _mto_apro = value
        End Set
    End Property

    Private _mto_ocup As Double
    Public Property mto_ocup() As Double
        Get
            Return _mto_ocup
        End Get
        Set(ByVal value As Double)
            _mto_ocup = value
        End Set
    End Property
    Private _saldo As Double
    Public Property saldo() As Double
        Get
            Return _saldo
        End Get
        Set(ByVal value As Double)
            _saldo = value
        End Set
    End Property


End Class

Public Class GASTOSDEFINIDOS
    Private _gto_cli As String
    Public Property gto_cli() As String
        Get
            Return _gto_cli
        End Get
        Set(ByVal value As String)
            _gto_cli = value
        End Set
    End Property

    Private _gto_ope As Long
    Public Property gto_ope() As Long
        Get
            Return _gto_ope
        End Get
        Set(ByVal value As Long)
            _gto_ope = value
        End Set
    End Property

    Private _gto_tdo As String
    Public Property gto_tdo() As String
        Get
            Return _gto_tdo
        End Get
        Set(ByVal value As String)
            _gto_tdo = value
        End Set
    End Property

    Private _gto_ndo As String
    Public Property gto_ndo() As String
        Get
            Return _gto_ndo
        End Get
        Set(ByVal value As String)
            _gto_ndo = value
        End Set
    End Property

    Private _gto_cfl As String
    Public Property gto_cfl() As String
        Get
            Return _gto_cfl
        End Get
        Set(ByVal value As String)
            _gto_cfl = value
        End Set
    End Property

    Private _gto_fln As String
    Public Property gto_fln() As String
        Get
            Return _gto_fln
        End Get
        Set(ByVal value As String)
            _gto_fln = value
        End Set
    End Property

    Private _gto_cod As Integer
    Public Property gto_cod() As Integer
        Get
            Return _gto_cod
        End Get
        Set(ByVal value As Integer)
            _gto_cod = value
        End Set
    End Property

    Private _gto_mto As Double
    Public Property gto_mto() As Double
        Get
            Return _gto_mto
        End Get
        Set(ByVal value As Double)
            _gto_mto = value
        End Set
    End Property


    Private _gto_tip As Integer
    Public Property gto_tip() As Integer
        Get
            Return _gto_tip
        End Get
        Set(ByVal value As Integer)
            _gto_tip = value
        End Set
    End Property

    Private _gto_des As String
    Public Property gto_des() As String
        Get
            Return _gto_des
        End Get
        Set(ByVal value As String)
            _gto_des = value
        End Set
    End Property

    Private _gto_num As Integer
    Public Property gto_num() As Integer
        Get
            Return _gto_num
        End Get
        Set(ByVal value As Integer)
            _gto_num = value
        End Set
    End Property

    Private _gto_tot As Double
    Public Property gto_tot() As Double
        Get
            Return _gto_tot
        End Get
        Set(ByVal value As Double)
            _gto_tot = value
        End Set
    End Property

    Private _gto_ddr As String
    Public Property gto_ddr() As String
        Get
            Return _gto_ddr
        End Get
        Set(ByVal value As String)
            _gto_ddr = value
        End Set
    End Property

End Class

Public Class GASTOSfijos
    Private _gto_cli As String
    Public Property gto_cli() As String
        Get
            Return _gto_cli
        End Get
        Set(ByVal value As String)
            _gto_cli = value
        End Set
    End Property

    Private _gto_ope As Long
    Public Property gto_ope() As Long
        Get
            Return _gto_ope
        End Get
        Set(ByVal value As Long)
            _gto_ope = value
        End Set
    End Property

    Private _gto_tdo As String
    Public Property gto_tdo() As String
        Get
            Return _gto_tdo
        End Get
        Set(ByVal value As String)
            _gto_tdo = value
        End Set
    End Property

    Private _gto_ndo As String
    Public Property gto_ndo() As String
        Get
            Return _gto_ndo
        End Get
        Set(ByVal value As String)
            _gto_ndo = value
        End Set
    End Property

    Private _gto_cfl As String
    Public Property gto_cfl() As String
        Get
            Return _gto_cfl
        End Get
        Set(ByVal value As String)
            _gto_cfl = value
        End Set
    End Property

    Private _gto_fln As String
    Public Property gto_fln() As String
        Get
            Return _gto_fln
        End Get
        Set(ByVal value As String)
            _gto_fln = value
        End Set
    End Property

    Private _gto_cod As Integer
    Public Property gto_cod() As Integer
        Get
            Return _gto_cod
        End Get
        Set(ByVal value As Integer)
            _gto_cod = value
        End Set
    End Property

    Private _gto_mto As Double
    Public Property gto_mto() As Double
        Get
            Return _gto_mto
        End Get
        Set(ByVal value As Double)
            _gto_mto = value
        End Set
    End Property


    Private _gto_tip As Integer
    Public Property gto_tip() As Integer
        Get
            Return _gto_tip
        End Get
        Set(ByVal value As Integer)
            _gto_tip = value
        End Set
    End Property

    Private _gto_des As String
    Public Property gto_des() As String
        Get
            Return _gto_des
        End Get
        Set(ByVal value As String)
            _gto_des = value
        End Set
    End Property

    Private _gto_num As Integer
    Public Property gto_num() As Integer
        Get
            Return _gto_num
        End Get
        Set(ByVal value As Integer)
            _gto_num = value
        End Set
    End Property

    Private _gto_tot As Double
    Public Property gto_tot() As Double
        Get
            Return _gto_tot
        End Get
        Set(ByVal value As Double)
            _gto_tot = value
        End Set
    End Property

    Private _gto_ddr As String
    Public Property gto_ddr() As String
        Get
            Return _gto_ddr
        End Get
        Set(ByVal value As String)
            _gto_ddr = value
        End Set
    End Property
End Class

Public Class impuestos
    Private _gto_cli As String
    Public Property gto_cli() As String
        Get
            Return _gto_cli
        End Get
        Set(ByVal value As String)
            _gto_cli = value
        End Set
    End Property

    Private _gto_ope As Long
    Public Property gto_ope() As Long
        Get
            Return _gto_ope
        End Get
        Set(ByVal value As Long)
            _gto_ope = value
        End Set
    End Property

    Private _gto_tdo As String
    Public Property gto_tdo() As String
        Get
            Return _gto_tdo
        End Get
        Set(ByVal value As String)
            _gto_tdo = value
        End Set
    End Property

    Private _gto_ndo As String
    Public Property gto_ndo() As String
        Get
            Return _gto_ndo
        End Get
        Set(ByVal value As String)
            _gto_ndo = value
        End Set
    End Property

    Private _gto_cfl As String
    Public Property gto_cfl() As String
        Get
            Return _gto_cfl
        End Get
        Set(ByVal value As String)
            _gto_cfl = value
        End Set
    End Property

    Private _gto_fln As String
    Public Property gto_fln() As String
        Get
            Return _gto_fln
        End Get
        Set(ByVal value As String)
            _gto_fln = value
        End Set
    End Property

    Private _gto_cod As Integer
    Public Property gto_cod() As Integer
        Get
            Return _gto_cod
        End Get
        Set(ByVal value As Integer)
            _gto_cod = value
        End Set
    End Property

    Private _gto_mto As Double
    Public Property gto_mto() As Double
        Get
            Return _gto_mto
        End Get
        Set(ByVal value As Double)
            _gto_mto = value
        End Set
    End Property


    Private _gto_tip As Integer
    Public Property gto_tip() As Integer
        Get
            Return _gto_tip
        End Get
        Set(ByVal value As Integer)
            _gto_tip = value
        End Set
    End Property

    Private _gto_des As String
    Public Property gto_des() As String
        Get
            Return _gto_des
        End Get
        Set(ByVal value As String)
            _gto_des = value
        End Set
    End Property

    Private _gto_num As Integer
    Public Property gto_num() As Integer
        Get
            Return _gto_num
        End Get
        Set(ByVal value As Integer)
            _gto_num = value
        End Set
    End Property

    Private _gto_tot As Double
    Public Property gto_tot() As Double
        Get
            Return _gto_tot
        End Get
        Set(ByVal value As Double)
            _gto_tot = value
        End Set
    End Property

    Private _gto_ddr As String
    Public Property gto_ddr() As String
        Get
            Return _gto_ddr
        End Get
        Set(ByVal value As String)
            _gto_ddr = value
        End Set
    End Property
End Class

Public Class gastofijo

    Private _monto As Double
    Public Property monto() As Double
        Get
            Return _monto
        End Get
        Set(ByVal value As Double)
            _monto = value
        End Set
    End Property

    Private _des As String
    Public Property des() As String
        Get
            Return _des
        End Get
        Set(ByVal value As String)
            _des = value
        End Set
    End Property
End Class

Public Class impuesto

    Private _monto As Double
    Public Property monto() As Double
        Get
            Return _monto
        End Get
        Set(ByVal value As Double)
            _monto = value
        End Set
    End Property

    Private _des As String
    Public Property des() As String
        Get
            Return _des
        End Get
        Set(ByVal value As String)
            _des = value
        End Set
    End Property



End Class