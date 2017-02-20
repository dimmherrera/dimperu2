Imports Microsoft.VisualBasic
Imports System.Data
Imports AjaxControlToolkit
Imports System.Web
Imports ClsSession.ClsSession
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Object
Imports System.Collections
Imports System

Public Module Variables

#Region "VariablesGlobales Módulo Cobranza"
    'Public Collection_Cobranza1 As New Collection 'Utilizado para Guardar los Deudores o Clientes según a quien cobrar
    'Public Collection_Cobranza2 As New Collection 'Utilizado para Guardar los Documentos a Cobrar Según selección de Cliente/Deudor
#End Region

    '#Region "VariablesGlobales Módulo Prorrogas"
    '#Region "Ingreso_Prorroga.aspx"
    '    Public Coll_DocumentosProrroga As New Collection 'Utilizado para Guardar los documentos con solicitud de Prorroga 
    '#End Region
    '#End Region

#Region "Variables Generales"

    Public Normal As New ArrayList
    Public solocob As New ArrayList
    Public normalcng As New ArrayList
    Public TOTAL_IMPUESTO As Double
    Public TOTAL_IMPUESTO_Pagare As Double
    Public DESCUADRE As New ArrayList
    Public Pagina As String
    'Public TAB_SUCURSAL As Integer = 1
    'Public TAB_USUARIO As Integer = 5
    'Public TAB_PERFIL As Integer = 1

#End Region

#Region "Enum y Function"

    Public Enum TipoDeMensaje As Short
        _Error = 1
        _Exclamacion = 2
        _Informacion = 3
        _Confirmacion = 4
        _Redireccion = 5
    End Enum

    Public Enum TipoDeContacto As Short
        Cliente = 1
        Deudor = 2
        ClienteDeudor = 3
    End Enum

    Public Sub CambioTema(ByVal Pg As Page)

        'If Skin = 0 Then
        '    Pg.Theme = "Predeterminado"
        'ElseIf Skin = 1 Then
        '    Pg.Theme = "Azul"
        'ElseIf Skin = 2 Then
        '    Pg.Theme = "Verde"
        'ElseIf Skin = 3 Then
        '    Pg.Theme = "Rojo"
        'ElseIf Skin = 4 Then
        '    Pg.Theme = "Celeste"
        'End If

    End Sub

    Public Enum TablaParametro

        Region = 1
        ComunaLocalidad = 2
        EstadoDeudor = 3
        ModoOperacion = 7
        EstadoCliente = 8
        EstadoPoderes = 10
        EstadoDocumento = 11
        TipoOperacion = 12
        CartaTipo = 15
        Zonas = 17
        FormasDePagos = 18
        Sistemas = 20
        TipoPagare = 21
        EstadoPagare = 22
        Moneda = 23
        TipoGarantia = 24
        RegimenMatrimonial = 25
        TipoAval = 26
        EstadoAval = 27
        EstadoSolicitudLinea = 28
        EstadoLinea = 29
        EstadoOperacion = 30
        TipoDocumento = 31
        TipoGastoOperacional = 36
        EstadoVerificacion = 40
        TipoCuentasXCobrar = 41
        TipoCliente = 44
        TipoEjecutivo = 45
        EstadoEjecutivo = 48
        TipoTelefono = 49
        TipoGastoRecaudacion = 51
        EstadoDctoPago = 52
        QueSePaga = 53
        TipoIngreso = 54
        QueAPagar = 55
        TipoEgreso = 56
        CategoriaRiesgo = 58
        EstadoFactura = 60
        MotivosDeProtestos = 61
        FacultadesPoder = 62
        RazonesSociales = 63
        ActividadEconomica = 64
        TipoRiesgo = 65
        TipoEnvioInformacion = 67
        FormaEnvio = 68
        TipoClasificacion = 69
        Pais = 70
        otro = 71
        otro1 = 72
        otro2 = 73
        otro3 = 74
        TipoOperacionContable = 75
        Segmentos = 76
        TipoBeneficiario = 77
        ActuacionApoderado = 78
        Contratos = 79
        ZonaRecaudacion = 80
        plataformas = 81
        EstadoNegociacion = 82
        ObjetivoCredito = 83
        Ciudad = 84
        Meses = 85
        EstadosCuentas = 86
        OrigenFondo = 87
        TipoEnvio = 88
        EstadoOpeNegociacion = 89
        EstadoCobNeg = 90
        TipoCartas = 91
        ParametroConsulta = 99
        TipoProductos = 100
        TipoComisionFactoringElectronico = 101
        ParametrosTipoProvisiones = 102
        EstadoNoRecaudado = 103
        CaracteristicaOperación = 104
        EstadoLineaFogape = 105
        TipoDevolucion = 106
        CargaMasivaDocumento = 107
        CargaMasivaPagoCliente = 108
        CargaMasivaPagoDeudor = 109
        EstadoEvaluacion = 110
        EstadoCondicion = 111
        Custodia = 112
        EstadoCheque = 113
        TipoIdentificacion = 119
        InformesPorMail = 300
        HorarioInformesPorMail = 301
        UsuariosNominaDiariaNegocios = 302
        TipoServicioLlamada = 303
        EnvioPorEmail = 304
        SaludosEnvioEmail = 305
        TextoEnvioEmail = 306
        MensajeDespedidaEnvioEmail = 307
        MensajePublicidadEnvioEmail = 308
        EstadoUsuarios = 309
        TipoCierreContable = 310
        Niveles = 5
        ClasificacionCliente = 118
        TipoCuenta = 312
        CORASU = 313

    End Enum

    Public Enum TablaAlfanumerico
        Giro = 1
        Plazas = 2
        BancaCliente = 3
        Factoring = 4
    End Enum

    Public Enum TipoSubLinea As Integer
        Deudor = 1
        TipoDocumento = 2
        Ambos = 3
    End Enum

    Public Enum TipoDocumentoAsociar
        EGR = 1
        CXC = 2
        CXP = 3
    End Enum

    Public Function FEC_VCTO_REAL(ByVal SucAct As Integer, ByVal PlazaDocto As String, ByVal TIPO_DOCTO As Integer, ByVal FECHA_VCTO_DOCTO As String) As Collection

        Try

            Dim DIAS_POR_PLAZA As Integer
            Dim BUSCA_DIA_HABIL As Boolean
            Dim CG As New CapaDatos.ClaseComercial
            Dim Coll As Collection
            Dim Datos As New Collection

            Dim FEC_VCTO As String

            Coll = CG.DiasDeRetencionDevuelve(SucAct, PlazaDocto, TIPO_DOCTO)

            If Coll.Count > 0 Then

                DIAS_POR_PLAZA = Coll.Item(1)

                If IsNothing(Coll.Item(2)) Then
                    BUSCA_DIA_HABIL = False
                Else
                    BUSCA_DIA_HABIL = IIf(Coll.Item(2) = "S", True, False)
                End If

            Else
                BUSCA_DIA_HABIL = False
                DIAS_POR_PLAZA = 1
            End If

            FEC_VCTO = FECHA_VCTO_DOCTO
            FEC_VCTO = DateAdd("D", DIAS_POR_PLAZA, FEC_VCTO)
            'DIAS_RETENCION = DIAS_POR_PLAZA

            If BUSCA_DIA_HABIL Then
                FEC_VCTO = CG.DiaHabilDevuelve(FEC_VCTO)
            End If

            Datos.Add(DIAS_POR_PLAZA)
            Datos.Add(FEC_VCTO)

            Return Datos


        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "Objetos"


    Public Class apb_cxs


        Private _id_suc As Integer
        Public Property id_suc() As Integer
            Get
                Return _id_suc
            End Get
            Set(ByVal Value As Integer)
                _id_suc = Value
            End Set
        End Property
        Private _id_cxs As Integer
        Public Property id_cxs() As Integer
            Get
                Return _id_cxs
            End Get
            Set(ByVal Value As Integer)
                _id_cxs = Value
            End Set
        End Property
        Private _nom_suc As String
        Public Property nom_suc() As String
            Get
                Return _nom_suc
            End Get
            Set(ByVal Value As String)
                _nom_suc = Value
            End Set
        End Property
        Private _nom_suc_apb As String
        Public Property nom_suc_apb() As String
            Get
                Return _nom_suc_apb
            End Get
            Set(ByVal Value As String)
                _nom_suc_apb = Value
            End Set
        End Property
    End Class

    Public Class obj_cxs

        Private _id_cxs As Integer
        Public Property id_cxs() As Integer
            Get
                Return _id_cxs
            End Get
            Set(ByVal Value As Integer)
                _id_cxs = Value
            End Set
        End Property
        Private _id_suc As Integer
        Public Property id_suc() As Integer
            Get
                Return _id_suc
            End Get
            Set(ByVal Value As Integer)
                _id_suc = Value
            End Set
        End Property
    End Class

    Public Class obj_rec

        Private _T_DOCTO As Integer
        Public Property T_DOCTO() As Integer
            Get
                Return _T_DOCTO
            End Get
            Set(ByVal Value As Integer)
                _T_DOCTO = Value
            End Set
        End Property

        Private _N_DOCTO As String
        Public Property N_DOCTO() As String
            Get
                Return _N_DOCTO
            End Get
            Set(ByVal Value As String)
                _N_DOCTO = Value
            End Set
        End Property

        Private _ID_DOC As Integer
        Public Property ID_DOC() As Integer
            Get
                Return _ID_DOC
            End Get
            Set(ByVal Value As Integer)
                _ID_DOC = Value
            End Set
        End Property

        Private _ID_NCE As Integer
        Public Property ID_NCE() As Integer
            Get
                Return _ID_NCE
            End Get
            Set(ByVal Value As Integer)
                _ID_NCE = Value
            End Set
        End Property

        Private _INTERES As Double
        Public Property INTERES() As Double
            Get
                Return _INTERES
            End Get
            Set(ByVal Value As Double)
                _INTERES = Value
            End Set
        End Property

        Private _NOTA_CRED As Double
        Public Property NOTA_CRED() As Double
            Get
                Return _NOTA_CRED
            End Get
            Set(ByVal Value As Double)
                _NOTA_CRED = Value
            End Set
        End Property



        Private _R_CLIENTE As String
        Public Property R_CLIENTE() As String
            Get
                Return _R_CLIENTE
            End Get
            Set(ByVal Value As String)
                _R_CLIENTE = Value
            End Set
        End Property

        Private _RUT_CLI As String
        Public Property RUT_CLI() As String
            Get
                Return _RUT_CLI
            End Get
            Set(ByVal Value As String)
                _RUT_CLI = Value
            End Set
        End Property


        Private _D_MONEDA As String
        Public Property D_MONEDA() As String
            Get
                Return _D_MONEDA
            End Get
            Set(ByVal Value As String)
                _D_MONEDA = Value
            End Set
        End Property

        Private _N_CLIENTE As String
        Public Property N_CLIENTE() As String
            Get
                Return _N_CLIENTE
            End Get
            Set(ByVal Value As String)
                _N_CLIENTE = Value
            End Set
        End Property

        Private _RUT_DEUDOR As String
        Public Property RUT_DEUDOR() As String
            Get
                Return _RUT_DEUDOR
            End Get
            Set(ByVal Value As String)
                _RUT_DEUDOR = Value
            End Set
        End Property

        Private _NOMBRE_DEUDOR As String
        Public Property NOMBRE_DEUDOR() As String
            Get
                Return _NOMBRE_DEUDOR
            End Get
            Set(ByVal Value As String)
                _NOMBRE_DEUDOR = Value
            End Set
        End Property

        Private _N_OPERACION As Integer
        Public Property N_OPERACION() As Integer
            Get
                Return _N_OPERACION
            End Get
            Set(ByVal Value As Integer)
                _N_OPERACION = Value
            End Set
        End Property

        Private _D_CEDIDO As String
        Public Property D_CEDIDO() As String
            Get
                Return _D_CEDIDO
            End Get
            Set(ByVal Value As String)
                _D_CEDIDO = Value
            End Set
        End Property

        Private _OPER_LNL As String
        Public Property OPER_LNL() As String
            Get
                Return _OPER_LNL
            End Get
            Set(ByVal Value As String)
                _OPER_LNL = Value
            End Set
        End Property

        Private _E_DOCTO As String
        Public Property E_DOCTO() As String
            Get
                Return _E_DOCTO
            End Get
            Set(ByVal Value As String)
                _E_DOCTO = Value
            End Set
        End Property

        Private _S_DEUDOR As Double
        Public Property S_DEUDOR() As Double
            Get
                Return _S_DEUDOR
            End Get
            Set(ByVal Value As Double)
                _S_DEUDOR = Value
            End Set
        End Property


        Private _MTO_RECAUDADO As Double
        Public Property MTO_RECAUDADO() As Double
            Get
                Return _MTO_RECAUDADO
            End Get
            Set(ByVal Value As Double)
                _MTO_RECAUDADO = Value
            End Set
        End Property


        Private _MTO_A_RECAUDAR As Double
        Public Property MTO_A_RECAUDAR() As Double
            Get
                Return _MTO_A_RECAUDAR
            End Get
            Set(ByVal Value As Double)
                _MTO_A_RECAUDAR = Value
            End Set
        End Property

        Private _TASA_REN As Double
        Public Property TASA_REN() As Double
            Get
                Return _TASA_REN
            End Get
            Set(ByVal Value As Double)
                _TASA_REN = Value
            End Set
        End Property



        Private _DIF_PRECIO As Double
        Public Property DIF_PRECIO() As Double
            Get
                Return _DIF_PRECIO
            End Get
            Set(ByVal Value As Double)
                _DIF_PRECIO = Value
            End Set
        End Property




        Private _S_CLIENTE As Double
        Public Property S_CLIENTE() As Double
            Get
                Return _S_CLIENTE
            End Get
            Set(ByVal Value As Double)
                _S_CLIENTE = Value
            End Set
        End Property


        Private _TASA_MOA As Char
        Public Property TASA_MOA() As Char
            Get
                Return _TASA_MOA
            End Get
            Set(ByVal Value As Char)
                _TASA_MOA = Value
            End Set
        End Property



        Private _TS_APLICACION As Double
        Public Property TS_APLICACION() As Double
            Get
                Return _TS_APLICACION
            End Get
            Set(ByVal Value As Double)
                _TS_APLICACION = Value
            End Set
        End Property


        Private _MTO_ANTICIPO As Double
        Public Property MTO_ANTICIPO() As Double
            Get
                Return _MTO_ANTICIPO
            End Get
            Set(ByVal Value As Double)
                _MTO_ANTICIPO = Value
            End Set
        End Property

        Private _FEC_VCTO As DateTime
        Public Property FEC_VCTO() As DateTime
            Get
                Return _FEC_VCTO
            End Get
            Set(ByVal Value As DateTime)
                _FEC_VCTO = Value
            End Set
        End Property

        Private _DOC_MTO As Double
        Public Property DOC_MTO() As Double
            Get
                Return _DOC_MTO
            End Get
            Set(ByVal Value As Double)
                _DOC_MTO = Value
            End Set
        End Property

        Private _FECHA_SIM As DateTime
        Public Property FECHA_SIM() As DateTime
            Get
                Return _FECHA_SIM
            End Get
            Set(ByVal Value As DateTime)
                _FECHA_SIM = Value
            End Set
        End Property

        Private _CANT_DIAS As Integer
        Public Property CANT_DIAS() As Integer
            Get
                Return _CANT_DIAS
            End Get
            Set(ByVal Value As Integer)
                _CANT_DIAS = Value
            End Set
        End Property


        Private _NRO_REN As Integer
        Public Property NRO_REN() As Integer
            Get
                Return _NRO_REN
            End Get
            Set(ByVal Value As Integer)
                _NRO_REN = Value
            End Set
        End Property

        Private _TIPO_DOC_REC As String
        Public Property TIPO_DOC_REC() As String
            Get
                Return _TIPO_DOC_REC
            End Get
            Set(ByVal Value As String)
                _TIPO_DOC_REC = Value
            End Set
        End Property

        Private _DES_TIP_DOC As String
        Public Property DES_TIP_DOC() As String
            Get
                Return _DES_TIP_DOC
            End Get
            Set(ByVal Value As String)
                _DES_TIP_DOC = Value
            End Set
        End Property
        Private _TIPO_PAGO As String
        Public Property TIPO_PAGO() As String
            Get
                Return _TIPO_PAGO
            End Get
            Set(ByVal Value As String)
                _TIPO_PAGO = Value
            End Set
        End Property


        Private _NRO_CUOTA As Integer
        Public Property NRO_CUOTA() As Integer
            Get
                Return _NRO_CUOTA
            End Get
            Set(ByVal Value As Integer)
                _NRO_CUOTA = Value
            End Set
        End Property



        Private _T_MONEDA As Integer
        Public Property T_MONEDA() As Integer
            Get
                Return _T_MONEDA
            End Get
            Set(ByVal Value As Integer)
                _T_MONEDA = Value
            End Set
        End Property
        
        Private _F_VCTO As DateTime
        Public Property F_VCTO() As DateTime
            Get
                Return _F_VCTO
            End Get
            Set(ByVal Value As DateTime)
                _F_VCTO = Value
            End Set
        End Property

        Private _FVTO_ORI As DateTime
        Public Property FVTO_ORI() As DateTime
            Get
                Return _FVTO_ORI
            End Get
            Set(ByVal Value As DateTime)
                _FVTO_ORI = Value
            End Set
        End Property



        Private _FACTOR_CAMBIO_NCE As Double
        Public Property FACTOR_CAMBIO_NCE() As Double
            Get
                Return _FACTOR_CAMBIO_NCE
            End Get
            Set(ByVal Value As Double)
                _FACTOR_CAMBIO_NCE = Value
            End Set
        End Property
        Private _MONEDA_DOCTO_NCE As Integer
        Public Property MONEDA_DOCTO_NCE() As Integer
            Get
                Return _MONEDA_DOCTO_NCE
            End Get
            Set(ByVal Value As Integer)
                _MONEDA_DOCTO_NCE = Value
            End Set
        End Property

        Private _MON_DOCTO As Double
        Public Property MON_DOCTO() As Double
            Get
                Return _MON_DOCTO
            End Get
            Set(ByVal Value As Double)
                _MON_DOCTO = Value
            End Set
        End Property

        Private _CONTRATO As String
        Public Property CONTRATO() As String
            Get
                Return _CONTRATO
            End Get
            Set(ByVal value As String)
                _CONTRATO = value
            End Set
        End Property


    End Class

    Public Class obj_gastos_rec



        Private _id_gga As Integer
        Public Property id_gga() As Integer
            Get
                Return _id_gga
            End Get
            Set(ByVal Value As Integer)
                _id_gga = Value
            End Set
        End Property
        Private _deu_ide As String
        Public Property deu_ide() As String
            Get
                Return _deu_ide
            End Get
            Set(ByVal Value As String)
                _deu_ide = Value
            End Set
        End Property
        Private _id_p_0051 As Integer
        Public Property id_p_0051() As Integer
            Get
                Return _id_p_0051
            End Get
            Set(ByVal Value As Integer)
                _id_p_0051 = Value
            End Set
        End Property

        Private _tipo_gasto As String
        Public Property tipo_gasto() As String
            Get
                Return _tipo_gasto
            End Get
            Set(ByVal Value As String)
                _tipo_gasto = Value
            End Set
        End Property

        Private _id_hre As Integer
        Public Property id_hre() As Integer
            Get
                Return _id_hre
            End Get
            Set(ByVal Value As Integer)
                _id_hre = Value
            End Set
        End Property
        Private _gga_fec As DateTime
        Public Property gga_fec() As DateTime
            Get
                Return _gga_fec
            End Get
            Set(ByVal Value As DateTime)
                _gga_fec = Value
            End Set
        End Property
        Private _gga_mto As Double
        Public Property gga_mto() As Double
            Get
                Return _gga_mto
            End Get
            Set(ByVal Value As Double)
                _gga_mto = Value
            End Set
        End Property
        Private _gga_vld As Char
        Public Property gga_vld() As Char
            Get
                Return _gga_vld
            End Get
            Set(ByVal Value As Char)
                _gga_vld = Value
            End Set
        End Property
        Private _gga_rec_ext As Char
        Public Property gga_rec_ext() As Char
            Get
                Return _gga_rec_ext
            End Get
            Set(ByVal Value As Char)
                _gga_rec_ext = Value
            End Set
        End Property

        Private _deudor As String
        Public Property deudor() As String
            Get
                Return _deudor
            End Get
            Set(ByVal Value As String)
                _deudor = Value
            End Set
        End Property

    End Class

#End Region

End Module

