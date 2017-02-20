Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Transactions
Imports ClsSession.SesionOperaciones
Imports System.Web.UI.WebControls
Imports ClsSession.ClsSession

Public Class ActualizacionesGenerales

    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim sql As New FuncionesGenerales.SqlQuery

#Region "Agenda"

    Public Function actividad_diaria_modifica(ByVal codigo As Integer, ByVal agda As agd_cls) As Collection

        Dim data As New DataClsFactoringDataContext
        Dim col As New Collection
        Dim agd = From a In data.agd_cls Where a.id_agd = codigo Select a

        For Each p In agd

            p.id_p_0115 = agda.id_p_0115
            p.agd_des = agda.agd_des

            col.Add(p)

        Next

        Return col

    End Function

    Public Function actividad_guarda(ByVal obj As agd_cls) As Boolean

        Dim data As New DataClsFactoringDataContext

        data.agd_cls.InsertOnSubmit(obj)
        data.SubmitChanges()

        Return True

    End Function

    Public Function actividad_diaria_elimina(ByVal codigo As Int64) As Boolean

        Dim data As New DataClsFactoringDataContext
        Dim col As New Collection
        Dim agd As agd_cls = (From a In data.agd_cls Where a.id_agd = codigo).First

        data.agd_cls.DeleteOnSubmit(agd)
        data.SubmitChanges()

        Return True
    End Function

#End Region

#Region "Parametros Alfanumericos"

    Public Function GiroInserta(ByVal IdGiro_obj As PL_000006_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Giro
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.PL_000006_cls.InsertOnSubmit(IdGiro_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PlazaInserta(ByVal Idpl_obj As PL_000047_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Plaza
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.PL_000047_cls.InsertOnSubmit(Idpl_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function BCliInserta(ByVal IdBCli_obj As PL_000066_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Banca_Cliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.PL_000066_cls.InsertOnSubmit(IdBCli_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FactInserta(ByVal IdFact_obj As PL_000069_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Factoring
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            data.PL_000069_cls.InsertOnSubmit(IdFact_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GiroModi(ByVal Id As Integer, _
                                              ByVal des As String, _
                                              ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Giro
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As PL_000006_cls = (From p In data.PL_000006_cls Where p.id_PL_000006 = Id).First
            With Modi
                .pal_des = des
                .pal_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PlazasModi(ByVal Id As Integer, _
                                                ByVal des As String, _
                                                ByVal est As String, _
                                                ByVal reg As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Plazas
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As PL_000047_cls = (From p In data.PL_000047_cls Where p.id_PL_000047 = Id).First
            With Modi
                .pal_des = des
                .pal_est = est
                .pal_reg = reg
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function BancaClienteModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica BancaCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As PL_000066_cls = (From p In data.PL_000066_cls Where p.id_PL_000066 = Id).First
            With Modi
                .pal_des = des
                .pal_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FactoringModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Factoring
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As PL_000069_cls = (From p In data.PL_000069_cls Where p.id_PL_000069 = Id).First
            With Modi
                .pal_des = des
                .pal_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminaParametrosAlfa(ByVal Codigotabla As TablaAlfanumerico, ByVal CodigoParametro As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim parametro As Object
            Select Case Codigotabla
                Case TablaAlfanumerico.Giro
                    parametro = (From p In data.PL_000006_cls Where p.id_PL_000006 = CodigoParametro).First
                    data.PL_000006_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaAlfanumerico.Plazas
                    parametro = (From p In data.PL_000047_cls Where p.id_PL_000047 = CodigoParametro).First
                    data.PL_000047_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaAlfanumerico.BancaCliente
                    parametro = (From p In data.PL_000066_cls Where p.id_PL_000066 = CodigoParametro).First
                    data.PL_000066_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaAlfanumerico.Factoring
                    parametro = (From p In data.PL_000069_cls Where p.id_PL_000069 = CodigoParametro).First
                    data.PL_000069_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
            End Select

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Tasas"

    Public Function Tmcinserta(ByVal Idtmc_obj As tmc_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Tasa Maxima Convencional
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.tmc_cls.InsertOnSubmit(Idtmc_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TmcModifica(ByVal Id_Tmc As Integer, _
                                     ByVal Tasa As Double, _
                                     ByVal TasaM As Double, _
                                     ByVal Est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Tasa Maxima Convencional
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim TasaMaxCon As tmc_cls = (From T In data.tmc_cls Where T.id_tmc = Id_Tmc).First
            With TasaMaxCon
                .tmc_est = Est
                .tmc_val = Tasa
                .tmc_mor = TasaM
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TimInserta(ByVal IdTim_obj As tim_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Tasa Impuesto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.tim_cls.InsertOnSubmit(IdTim_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TypInserta(ByVal IdTyp_obj As typ_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Tasa y Plazo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            If IdTyp_obj.typ_est = "A" Then

                Dim piso = From P In data.typ_cls Where P.typ_est = "A"

                For Each p In piso
                    p.typ_est = "I"
                Next

            End If

            data.typ_cls.InsertOnSubmit(IdTyp_obj)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function typModifica(ByVal Id_Typ As Integer, _
                                 ByVal Monto As Double, _
                                 ByVal Spread As Double, _
                                 ByVal Dsd As Double, _
                                 ByVal Hst As Double, _
                                 ByVal Des As String, _
                                 ByVal Est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Tasa y Plazo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim tModifica As typ_cls = (From T In data.typ_cls Where T.id_typ = Id_Typ).First
            With tModifica
                .typ_mto = Monto
                .typ_spread = Spread
                .typ_dde = Dsd
                .typ_hta = Hst
                .typ_des = Des
                .typ_est = Est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Public Function timModifica(ByVal Id_Tim As Integer, _
                                ByVal Fecha As Date, _
                                ByVal Vista As Double, _
                                ByVal Plazo As Double, _
                                ByVal Est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Tasa Impuesto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim tModifica As tim_cls = (From T In data.tim_cls Where T.id_tim = Id_Tim).First
            With tModifica
                .tim_fec = Fecha
                .tim_val_vis = Vista
                .tim_val_pla = Plazo
                .tim_est = Est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Zona"

    Public Function ZonaInserta(ByVal Id_obj As zon_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Zona
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.zon_cls.InsertOnSubmit(Id_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonaModifica(ByVal idz As Integer, _
                                 ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Zona
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModiZona As zon_cls = (From z In data.zon_cls Where z.id_zon = idz).First
            With ModiZona
                .zon_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonaElimina(ByVal Idz As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Zona
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim EliminaZona As zon_cls = (From z In data.zon_cls Where z.id_zon = Idz).First
            data.zon_cls.DeleteOnSubmit(EliminaZona)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Categoria Riesgo"
    Public Function CategoriaRiesgoInserta(ByVal coll As Collection) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda categoria Riesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 27/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            For i = 1 To coll.Count
                data.ctr_cls.InsertOnSubmit(coll.Item(i))
            Next

            data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False


        End Try
    End Function


    Public Function CategoriaRiesgoElimina(ByVal Id_Docto As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Categoria de Riesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 27/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim CtrElimina = From a In data.ctr_cls Where a.id_p_0031 = Id_Docto
            For Each i In CtrElimina
                data.ctr_cls.DeleteOnSubmit(i)
            Next
            data.SubmitChanges()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

#Region "Condiciones por Cliente"

    Public Function ComisionInserta(ByVal Comision As CDC_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva comision para Cliente
        'Creado por= Victor Alvarez Rojas.                                                                                                                       
        'Fecha Creacion: 05/09/2011                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Data.CDC_cls.InsertOnSubmit(Comision)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function GastosInserta(ByVal Gastos As GDC_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un nuevo gasto por Cliente
        'Creado por= Victor Alvarez Rojas.                                                                                                                       
        'Fecha Creacion: 09/09/2011                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Data.GDC_cls.InsertOnSubmit(Gastos)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function EliminaGastosCli(ByVal RutCliente As Long) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Borra Gastos
        'Creado por= Victor Alvarez R
        'Fecha Creacion: 09/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Gastos = From G In Data.gdc_cls Where G.cli_idc = Format(RutCliente, Var.FMT_RUT) Select G

            Data.gdc_cls.DeleteAllOnSubmit(Gastos)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function




    Public Function EliminaComisionCli(ByVal RutCliente As Long) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Borra Gastos
        'Creado por= Victor Alvarez R
        'Fecha Creacion: 09/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Comision = From C In Data.cdc_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) Select C

            Data.cdc_cls.DeleteAllOnSubmit(Comision)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function
#End Region

#Region "Bancos"


    Public Function BancoInserta(ByVal Bco_Obj As bco_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Banco
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext

            Data.bco_cls.InsertOnSubmit(Bco_Obj)
            Data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SBCInserta(ByVal Sbc_Obj As sbc_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda SBC Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Data.sbc_cls.InsertOnSubmit(Sbc_Obj)
            Data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function BancoModifica(ByVal Codigo_Banco As Integer, _
                                  ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Banco
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModificaBanco As bco_cls = (From b In data.bco_cls Where b.id_bco = Codigo_Banco).First
            With ModificaBanco
                .bco_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SucModifica(ByVal Codigo_Sucursal As Integer, _
                                ByVal des_Suc As String, _
                                ByVal Codigo_Plaza As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModificaSucursal As sbc_cls = (From s In data.sbc_cls Where s.id_sbc = Codigo_Sucursal).First
            With ModificaSucursal
                .sbc_des = des_Suc
                .id_pl_000047 = Codigo_Plaza
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Gastos"

    Public Function GtoInserta(ByVal IdGto_obj As gto_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Gastos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.gto_cls.InsertOnSubmit(IdGto_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GtoModifica(ByVal IdGto As Integer, _
                                    ByVal Est As String, _
                                    ByVal contable As String, _
                                    ByVal CS_Iva As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Gastos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim GModifica As gto_cls = (From Gt In data.gto_cls Where Gt.id_gto = IdGto).First

            With GModifica
                .gto_est = Est
                .val_con = contable
                .gto_iva = CS_Iva
            End With

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GtoElimina(ByVal Id_gto As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Gastos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim gastoElimina As gto_cls = (From gt In data.gto_cls Where gt.id_gto = Id_gto).First
            data.gto_cls.DeleteOnSubmit(gastoElimina)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Cartera"
    Public Function CarteraInserta(ByVal Idcrt_obj As crt_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Cartera
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.crt_cls.InsertOnSubmit(Idcrt_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CarteraModifica(ByVal Id_Crt As Integer, _
                                    ByVal Id_Dia As Integer, _
                                    ByVal Des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Cartera
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModificaCartera As crt_cls = (From crt In data.crt_cls Where crt.id_crt = Id_Crt).First
            With ModificaCartera
                .crt_ctd_dia = Id_Dia
                .crt_des = Des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CarteraElimina(ByVal Id_Crt As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Cartera
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim EliminaCartera As crt_cls = (From Crt In data.crt_cls Where Crt.id_crt = Id_Crt).First
            data.crt_cls.DeleteOnSubmit(EliminaCartera)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Sucursal Plaza"

    Public Function SucursalInserta(ByVal Ids_obj As suc_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.suc_cls.InsertOnSubmit(Ids_obj)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SucursalModifica(ByVal IdS As Integer, _
                                     ByVal IdInt As String, _
                                     ByVal Des As String, _
                                     ByVal DesCorto As String, _
                                     ByVal Pbanco As String, _
                                     ByVal Rg As String, _
                                     ByVal Direccion As String, _
                                     ByVal fono As String, _
                                     ByVal fax As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim SucModifica As suc_cls = (From sc In data.suc_cls Where sc.id_suc = IdS).First
            With SucModifica
                .suc_cod_ftg = IdInt
                .suc_nom = Des
                .suc_des_cra = DesCorto
                '.id_PL_000047 = Pbanco
                .suc_cod_reg = Rg
                .suc_dir = Direccion
                .suc_tel = fono
                .suc_fax = fax
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PlazaInserta(ByVal Cod As pds_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda pds
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.pds_cls.InsertOnSubmit(Cod)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function PlazaModifica(ByVal idpl As Integer, _
                                  ByVal dias As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Pds
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim plazaModi As pds_cls = (From p In data.pds_cls Where p.id_pds = idpl).First
            With plazaModi
                .pds_ret = dias
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Comuna"
    Public Function ComunaInserta(ByVal IdCmn_obj As cmn_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Comuna
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext
            Data.cmn_cls.InsertOnSubmit(IdCmn_obj)
            Data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ComunaModifica(ByVal Id_Cmn As Integer, _
                                   ByVal Id_Zon As Integer, _
                                   ByVal Des_Cmn As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Comuna
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     18-05-2009         - se quita la variable de entrada id_bco por no estar ocupandose 
        '**************************************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext
            Dim ModificaComuna As cmn_cls = (From Cmn In Data.cmn_cls Where Cmn.id_cmn = Id_Cmn).First
            With ModificaComuna
                '.id_ciu = Id_Ciu Saque Modificar Ciudad
                .id_zon = Id_Zon
                '.id_bco = Id_Bco
                .cmn_des = Des_Cmn
            End With
            Data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Parametros Numericos Inserta"

    Public Function RegInserta(ByVal IdReg As P_001_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Region
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_001_cls.InsertOnSubmit(IdReg)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ComLocInserta(ByVal IdCL As P_002_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ComunaLocalidad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            data.P_002_cls.InsertOnSubmit(IdCL)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function NivelesInserta(ByVal IdN As P_005_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Niveles
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            data.P_005_cls.InsertOnSubmit(IdN)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstDeuInserta(ByVal IdEstDeu As P_003_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoDeudor
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_003_cls.InsertOnSubmit(IdEstDeu)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ModOpeInserta(ByVal IdModOpe As P_007_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ModoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_007_cls.InsertOnSubmit(IdModOpe)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstCliInserta(ByVal IdEstCli As P_008_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_008_cls.InsertOnSubmit(IdEstCli)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstPoInserta(ByVal IdEstPo As P_0010_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoPoderes
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0010_cls.InsertOnSubmit(IdEstPo)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstDocInserta(ByVal IdEstDoc As P_0011_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoDocumento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0011_cls.InsertOnSubmit(IdEstDoc)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiOpeInserta(ByVal IdTiOp As P_0012_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0012_cls.InsertOnSubmit(IdTiOp)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CarTiInserta(ByVal IdCarTi As P_0015_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CartaTipo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0015_cls.InsertOnSubmit(IdCarTi)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonInserta(ByVal IdZon As P_0017_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Zona
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0017_cls.InsertOnSubmit(IdZon)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ForPaInserta(ByVal IdForPa As P_0018_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda FormadePagos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0018_cls.InsertOnSubmit(IdForPa)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SisInserta(ByVal IdSis As P_0020_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Sistemas
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0020_cls.InsertOnSubmit(IdSis)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiPagInserta(ByVal IdTiP As P_0021_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoPagare
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0021_cls.InsertOnSubmit(IdTiP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstPagInserta(ByVal IdEstPa As P_0022_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoPagare
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0022_cls.InsertOnSubmit(IdEstPa)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MonInserta(ByVal IdMon As P_0023_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Moneda
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0023_cls.InsertOnSubmit(IdMon)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipGaInserta(ByVal IdTg As P_0024_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoGarantia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0024_cls.InsertOnSubmit(IdTg)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReMaInserta(ByVal IdRM As P_0025_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda RegimenMatrimonial
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0025_cls.InsertOnSubmit(IdRM)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiAvalInserta(ByVal IdTa As P_0026_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoAval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0026_cls.InsertOnSubmit(IdTa)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstAvalInserta(ByVal IdEA As P_0027_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoAval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0027_cls.InsertOnSubmit(IdEA)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstSoDeLinInserta(ByVal IdEsS As P_0028_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoSolicitudLinea
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0028_cls.InsertOnSubmit(IdEsS)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstDeLinInserta(ByVal IdEL As P_0029_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoLinea
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0029_cls.InsertOnSubmit(IdEL)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstDeOpInserta(ByVal IdEO As P_0030_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0030_cls.InsertOnSubmit(IdEO)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TDocInserta(ByVal IdTD As P_0031_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoDocumento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0031_cls.InsertOnSubmit(IdTD)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TGOperaInserta(ByVal IdTGO As P_0036_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoGastoOperacional
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0036_cls.InsertOnSubmit(IdTGO)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstVeriInserta(ByVal IdEstVe As P_0040_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoVerificacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0040_cls.InsertOnSubmit(IdEstVe)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoCuentaXCobrarInserta(ByVal Id As p_0041_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoCxC
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.p_0041_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TpCliInserta(ByVal IdTCli As P_0044_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0044_cls.InsertOnSubmit(IdTCli)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TpEjeInserta(ByVal IdTEje As P_0045_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoDeEjecutivo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0045_cls.InsertOnSubmit(IdTEje)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstEjeInserta(ByVal IdEstEje As P_0048_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoEjecutivo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0048_cls.InsertOnSubmit(IdEstEje)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiFonoInserta(ByVal IdTFono As P_0049_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoFono
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0049_cls.InsertOnSubmit(IdTFono)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiGasRecInserta(ByVal IdTGR As P_0051_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoGastoRecaudacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0051_cls.InsertOnSubmit(IdTGR)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EsDocPgInserta(ByVal IdEdp As P_0052_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoDocumentodePago
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0052_cls.InsertOnSubmit(IdEdp)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function QsPagaInserta(ByVal IdQP As P_0053_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda QueSePaga
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0053_cls.InsertOnSubmit(IdQP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipIngrInserta(ByVal IdTI As p_0054_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoIngreso
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.p_0054_cls.InsertOnSubmit(IdTI)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function QueAPagInserta(ByVal IdQaP As P_0055_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda QueAPagar
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0055_cls.InsertOnSubmit(IdQaP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TiEgreInserta(ByVal IdTE As P_0056_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoDeEgreso
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0056_cls.InsertOnSubmit(IdTE)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CatRiesInserta(ByVal IdCtRies As P_0058_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CategoriaDeRiesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0058_cls.InsertOnSubmit(IdCtRies)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstFacInserta(ByVal IdEstF As P_0060_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoFactura
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0060_cls.InsertOnSubmit(IdEstF)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MotProInserta(ByVal IdMP As P_0061_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda MotivosDeProtestos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0061_cls.InsertOnSubmit(IdMP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FacPodInserta(ByVal IdFP As P_0062_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda facultadesDePoder
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0062_cls.InsertOnSubmit(IdFP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RazonSocialInserta(ByVal IdRS As P_0063_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda RazonSocial
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0063_cls.InsertOnSubmit(IdRS)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActividadEconomicaInserta(ByVal IdAE As P_0064_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ActividadEconomica
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0064_cls.InsertOnSubmit(IdAE)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoRiesgoInserta(ByVal IdTR As P_0065_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoRiesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0065_cls.InsertOnSubmit(IdTR)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoEnvioInfoInserta(ByVal IdTEI As P_0067_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoEnvioInformacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0067_cls.InsertOnSubmit(IdTEI)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FormaEnvioInserta(ByVal IdFE As P_0068_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda FormaDeEnvio
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0068_cls.InsertOnSubmit(IdFE)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoClasificacionInserta(ByVal Id As P_0069_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoClasificacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0069_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PaisInserta(ByVal Idp As P_0070_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Pais
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0070_cls.InsertOnSubmit(Idp)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function OtroInserta(ByVal IdO As P_0071_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Otro
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0071_cls.InsertOnSubmit(IdO)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Otro1Inserta(ByVal IdO1 As P_0072_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Otro1
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0072_cls.InsertOnSubmit(IdO1)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Otro2Inserta(ByVal IdO2 As P_0073_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Otro2
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0073_cls.InsertOnSubmit(IdO2)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Otro3Inserta(ByVal IdO3 As P_0074_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Otro3
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0074_cls.InsertOnSubmit(IdO3)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoOpeContInserta(ByVal IdToC As P_0075_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoOperacionContable
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0075_cls.InsertOnSubmit(IdToC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SegmentoInserta(ByVal IdS As P_0076_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Segmento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0076_cls.InsertOnSubmit(IdS)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoBeneficiarioInserta(ByVal IdTB As P_0077_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoBeneficiario
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0077_cls.InsertOnSubmit(IdTB)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActuacionApodInserta(ByVal IdA As P_0078_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ActuacionApoderado
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0078_cls.InsertOnSubmit(IdA)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ContratoInserta(ByVal IdC As P_0079_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Segmento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0079_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonRiesgoRecaudacionInserta(ByVal IdZ As P_0080_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ZonaRiesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0080_cls.InsertOnSubmit(IdZ)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PlataformaInserta(ByVal IdP As P_0081_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Plataforma
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0081_cls.InsertOnSubmit(IdP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstNegoInserta(ByVal IdP As P_0082_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0082_cls.InsertOnSubmit(IdP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ObjetivoCreditoInserta(ByVal IdOC As P_0083_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ObjetivoCredito
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0083_cls.InsertOnSubmit(IdOC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CiuInserta(ByVal IdC As P_0084_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Ciudad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0084_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MesesInserta(ByVal IdM As P_0085_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Meses
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0085_cls.InsertOnSubmit(IdM)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstCuentaInserta(ByVal IdEs As P_0086_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoCuenta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0086_cls.InsertOnSubmit(IdEs)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function OrigenFondoInserta(ByVal IdOF As P_0087_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda OrigenFondo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0087_cls.InsertOnSubmit(IdOF)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoEnvioInserta(ByVal IdTE As P_0088_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoEnvio
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0088_cls.InsertOnSubmit(IdTE)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstaOpeNegociacionInserta(ByVal IdEON As P_0089_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoOperacionNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0089_cls.InsertOnSubmit(IdEON)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstaCobroNegociacionInserta(ByVal IdECN As P_0090_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoCobroNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0090_cls.InsertOnSubmit(IdECN)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipodeCartaInserta(ByVal IdTC As P_0091_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoDeCarta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0091_cls.InsertOnSubmit(IdTC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ParametroConsultaApiInserta(ByVal IdP As P_0099_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ParametroConsulta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0099_cls.InsertOnSubmit(IdP)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipodProductoInserta(ByVal IdT As P_0100_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoProducto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0100_cls.InsertOnSubmit(IdT)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoComisionFacElecInserta(ByVal IdT As P_0101_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoComisionFactoringElectronico
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0101_cls.InsertOnSubmit(IdT)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ParaTipoProvisionesInserta(ByVal IdT As P_0102_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda ParaTipoProvisiones
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0102_cls.InsertOnSubmit(IdT)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstNoRecaudadoInserta(ByVal IdEst As P_0103_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoNoRecaudado
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0103_cls.InsertOnSubmit(IdEst)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CaracterisOperacionInserta(ByVal IdC As P_0104_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CaracteristicaOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0104_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstadoLíneaFogapeInserta(ByVal IdEst As P_0105_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoLineaFogape
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0105_cls.InsertOnSubmit(IdEst)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoDevolucionInserta(ByVal IdT As P_0106_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipòDevolucion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0106_cls.InsertOnSubmit(IdT)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CargaMasivaDocInserta(ByVal IdC As P_0107_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CargaMasivaDocumento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0107_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CargaMasivaPagoClienteInserta(ByVal IdC As P_0108_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CargaMasivaPagoCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0108_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CargaMasivaPagoDeudorInserta(ByVal IdC As P_0109_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda CargaMasivaPagoDeudor
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0109_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstadoEvaluacionInserta(ByVal IdC As P_0110_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoEvaluacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0110_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstadoCondicionInserta(ByVal IdC As p_0111_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoCondicion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.p_0111_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CustodiaInserta(ByVal IdC As P_0112_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Custodia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0112_cls.InsertOnSubmit(IdC)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function InformePorMailInserta(ByVal IdI As P_0300_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda InformePorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0300_cls.InsertOnSubmit(IdI)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function HorarioInformesPorMailInserta(ByVal IdH As P_0301_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda HorarioInformePorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0301_cls.InsertOnSubmit(IdH)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function UsuariosNominaDiaNegociosInserta(ByVal Id As P_0302_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda UsuarioNominaDiariaNegocio
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0302_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoServicioLlamadaInserta(ByVal Id As P_0303_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoServicioLlamada
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0303_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EnvioPorMailInserta(ByVal Id As P_0304_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EnvioPorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0304_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaludosEnvioPorMailInserta(ByVal Id As P_0305_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda SaludosEnvioPorMail       
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0305_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TextoEnvioPorMailInserta(ByVal Id As P_0306_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Segmento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0306_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MensajeDespedidaEnvioEmailInserta(ByVal Id As P_0307_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda MensajeDespedidaEnvioMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0307_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MensajePublicidadEnvioEmailInserta(ByVal Id As P_0308_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda MensajePublicidadEnvioMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0308_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EstadoUsuarioInserta(ByVal Id As P_0309_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda EstadoUsuario
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0309_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoCierreContableInserta(ByVal Id As P_0310_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda TipoCierreContable
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0310_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Parametros Numericos Modifica"

    Public Function RegModifica(ByVal Id As Integer, _
                                ByVal est As String, _
                                ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Region
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_001_cls = (From r In data.P_001_cls Where r.id_P_001 = Id).First
            With Modi
                .pnu_est = est
                .pnu_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function ComunaLocalidadModi(ByVal Id As Integer, _
                                      ByVal est As String, _
                                ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ComunaLocalidad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_002_cls = (From p In data.P_002_cls Where p.id_P_002 = Id).First
            With Modi
                .pnu_est = est
                .pnu_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoDeuModi(ByVal Id As Integer, _
                                  ByVal est As String, _
                                  ByVal des As String, _
                                  ByVal sig As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoDeudor
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_003_cls = (From p In data.P_003_cls Where p.id_P_003 = Id).First
            With Modi
                .pnu_est = est
                .pnu_des = des
                .pnu_atr_003 = sig
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function NivelesModi(ByVal Id As Integer, _
                                  ByVal est As String, _
                                  ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Niveles
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_005_cls = (From p In data.P_005_cls Where p.id_P_005 = Id).First
            With Modi
                .pnu_est = est
                .pnu_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ModoOperacionModi(ByVal Id As Integer, _
                                    ByVal est As String, _
                                    ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ModoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_007_cls = (From p In data.P_007_cls Where p.id_P_007 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoClienteModi(ByVal Id As Integer, _
                                    ByVal est As String, _
                                    ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_008_cls = (From p In data.P_008_cls Where p.id_P_008 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoPoderesModi(ByVal Id As Integer, _
                                   ByVal est As String, _
                                   ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoPoderes
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0010_cls = (From p In data.P_0010_cls Where p.id_P_0010 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoDocModi(ByVal Id As Integer, _
                                      ByVal est As String, _
                                      ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoDocumento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0011_cls = (From p In data.P_0011_cls Where p.id_P_0011 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoOperacionModi(ByVal Id As Integer, _
                                   ByVal est As String, _
                                   ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0012_cls = (From p In data.P_0012_cls Where p.id_P_0012 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CartaTipoModi(ByVal Id As Integer, _
                                   ByVal est As String, _
                                   ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CartaTipo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0015_cls = (From p In data.P_0015_cls Where p.id_P_0015 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ZonasModi(ByVal Id As Integer, _
                                ByVal est As String, _
                                ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Zonas
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0017_cls = (From p In data.P_0017_cls Where p.id_P_0017 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function FormadePagoModi(ByVal Id As Integer, _
                                ByVal est As String, _
                                ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica FormaDePago
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0018_cls = (From p In data.P_0018_cls Where p.id_P_0018 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SistemaModi(ByVal Id As Integer, _
                               ByVal est As String, _
                               ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Sistema
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0020_cls = (From p In data.P_0020_cls Where p.id_P_0020 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoPagareModi(ByVal Id As Integer, _
                               ByVal est As String, _
                               ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoPagare
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0021_cls = (From p In data.P_0021_cls Where p.id_P_0021 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoPagareModi(ByVal Id As Integer, _
                            ByVal est As String, _
                            ByVal des As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoPagare
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0022_cls = (From p In data.P_0022_cls Where p.id_P_0022 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MonedaModi(ByVal Id As Integer, _
                            ByVal des As String, _
                            ByVal est As String, _
                            ByVal c_Int As String, _
                            ByVal c_d24 As Integer, _
                            ByVal c_fog As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Moneda
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0023_cls = (From p In data.P_0023_cls Where p.id_P_0023 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_003 = c_Int
                .pnu_atr_004 = c_d24
                .pnu_atr_005 = c_fog
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipoGarantiaModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoGarantia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0024_cls = (From p In data.P_0024_cls Where p.id_P_0024 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RegimenMetrimoModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica RegimenMatrimonial
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_002_cls = (From p In data.P_002_cls Where p.id_P_002 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoAvalModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoAval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0026_cls = (From p In data.P_0026_cls Where p.id_P_0026 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoAvalModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoAval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0027_cls = (From p In data.P_0027_cls Where p.id_P_0027 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstSolicitudLinealModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoSolicitudLinea
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0028_cls = (From p In data.P_0028_cls Where p.id_P_0028 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstdeLinealModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoDeLinea
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0029_cls = (From p In data.P_0029_cls Where p.id_P_0029 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstOperacionModi(ByVal Id As Integer, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0030_cls = (From p In data.P_0030_cls Where p.id_P_0030 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoDocumentoModi(ByVal Id As Integer, _
                           ByVal dias As Integer, _
                           ByVal Tdoc As String, _
                           ByVal Sig As String, _
                           ByVal dc As String, _
                           ByVal BusDH As String, _
                           ByVal dr As String, _
                           ByVal Tmon As String, _
                           ByVal Comi As String, _
                           ByVal min As String, _
                           ByVal max As String, _
                           ByVal TDaGest As String, _
                           ByVal PtDolar As String, _
                           ByVal des As String, _
                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDocumento
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0031_cls = (From p In data.P_0031_cls Where p.id_P_0031 = Id).First
            With Modi
                .pnu_atr_001 = dias
                .pnu_atr_002 = Tdoc
                .pnu_atr_003 = Sig
                .pnu_atr_004 = dc
                .pnu_atr_005 = BusDH
                .pnu_atr_006 = dr
                .pnu_atr_007 = Tmon
                .pnu_atr_008 = Comi
                .pnu_atr_009 = min
                .pnu_atr_010 = max
                .pnu_atr_011 = TDaGest
                .pnu_atr_012 = PtDolar
                .pnu_est = est
                .pnu_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipGastoOperacionModi(ByVal Id As Integer, _
                               ByVal des As String, _
                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoGastoOperacional
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0036_cls = (From p In data.P_0036_cls Where p.id_P_0036 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoVerificacionModi(ByVal Id As Integer, _
                               ByVal des As String, _
                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoVerificacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0040_cls = (From p In data.P_0040_cls Where p.id_P_0040 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoCuentaXCobrarModi(ByVal Id As Integer, _
                               ByVal des As String, _
                               ByVal est As String, _
                               ByVal cob_pag As Integer, _
                               ByVal forcrec As String, _
                               ByVal cobint As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoCuentaPorCobrar
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As p_0041_cls = (From p In data.p_0041_cls Where p.id_P_0041 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_005 = cobint
                .pnu_atr_003 = forcrec
                '.pnu_atr_002 =
                .pnu_atr_004 = cob_pag

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function




    Public Function TipoClienteModi(ByVal Id As Integer, _
                               ByVal des As String, _
                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipooCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0044_cls = (From p In data.P_0044_cls Where p.id_P_0044 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoEjecutivoModi(ByVal Id As Integer, _
                               ByVal des As String, _
                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoEjecutivo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0045_cls = (From p In data.P_0045_cls Where p.id_P_0045 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoEjecutivoModi(ByVal Id As Integer, _
                                   ByVal des As String, _
                                   ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoEjecutivo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0048_cls = (From p In data.P_0048_cls Where p.id_P_0048 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoFonoModi(ByVal Id As Integer, _
                                       ByVal des As String, _
                                       ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoFono
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0049_cls = (From p In data.P_0049_cls Where p.id_P_0049 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoGastoRecaudacionModi(ByVal Id As Integer, _
                                       ByVal des As String, _
                                       ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoGastoRecaudacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0051_cls = (From p In data.P_0051_cls Where p.id_P_0051 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EsDocdePagoModi(ByVal Id As Integer, _
                                      ByVal des As String, _
                                      ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoDocumentoDePago
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0052_cls = (From p In data.P_0052_cls Where p.id_P_0052 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function QuesePagaModi(ByVal Id As Integer, _
                                      ByVal des As String, _
                                      ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica QueSePaga
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0053_cls = (From p In data.P_0053_cls Where p.id_P_0053 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoDeIngresoModi(ByVal Id As Integer, _
                                      ByVal des As String, _
                                      ByVal est As String, _
                                      ByVal ar As String, _
                                      ByVal dr As Integer, _
                                      ByVal ip As String, _
                                      ByVal lex As Integer, _
                                      ByVal nomDepo As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeIngreso
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As p_0054_cls = (From p In data.p_0054_cls Where p.id_p_0054 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_003 = ip
                .pnu_atr_001 = dr
                .pnu_atr_005 = nomDepo
                .pnu_atr_004 = lex
                .pnu_atr_002 = ar
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function QueAPagarModi(ByVal Id As Integer, _
                                     ByVal des As String, _
                                     ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica QueAPagar
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0055_cls = (From p In data.P_0055_cls Where p.id_P_0055 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TipodeEgresoModi(ByVal Id As Integer, _
                                     ByVal des As String, _
                                     ByVal est As String, _
                                     ByVal cgo As String, _
                                     ByVal Ip As String, _
                                     ByVal NominaEgre As String, _
                                     ByVal ctacte As String, _
                                     ByVal sis As String, _
                                     ByVal gmf As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeEgreso
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                  18-06-2012         Se agrega parametro de identificacion de sistema en el cual se utilizara
        'SHenriquez                  26-06-2012         Se agrega parametro de aplicacion de GMF
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0056_cls = (From p In data.P_0056_cls Where p.id_P_0056 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_002 = NominaEgre
                .pnu_atr_003 = ctacte
                .pnu_atr_004 = Ip
                .pnu_atr_005 = cgo
                .pnu_atr_006 = sis
                .pnu_atr_007 = gmf
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CategoriadeRiesgoModi(ByVal Id As Integer, _
                                     ByVal des As String, _
                                     ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CategoriaRiesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0058_cls = (From p In data.P_0058_cls Where p.id_P_0058 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadodeFacturasModi(ByVal Id As Integer, _
                                         ByVal des As String, _
                                         ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoDeFactura
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0060_cls = (From p In data.P_0060_cls Where p.id_P_0060 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function MotivosdeProtestosModi(ByVal Id As Integer, _
                                             ByVal des As String, _
                                             ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica MotivosDeProtestos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0061_cls = (From p In data.P_0061_cls Where p.id_P_0061 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function FacultadesdePoderModi(ByVal Id As Integer, _
                                             ByVal des As String, _
                                             ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica FacultadesdePoder
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0062_cls = (From p In data.P_0062_cls Where p.id_P_0062 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RazonesSocialesModi(ByVal Id As Integer, _
                                             ByVal des As String, _
                                             ByVal est As String, _
                                             ByVal abv As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica RazonesSociales
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0063_cls = (From p In data.P_0063_cls Where p.id_P_0063 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_007 = abv
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ActividadEconómicaModi(ByVal Id As Integer, _
                                             ByVal des As String, _
                                             ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ActividadEconomica
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0064_cls = (From p In data.P_0064_cls Where p.id_P_0064 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipodeRiesgosModi(ByVal Id As Integer, _
                                             ByVal des As String, _
                                             ByVal est As String, _
                                             ByVal porc As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeRiesgo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0065_cls = (From p In data.P_0065_cls Where p.id_P_0065 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_002 = porc
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoEnvioInfoModi(ByVal Id As Integer, _
                                                 ByVal des As String, _
                                                 ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoEnvioInformacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0067_cls = (From p In data.P_0067_cls Where p.id_P_0067 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function FormadeEnvíoModi(ByVal Id As Integer, _
                                                 ByVal des As String, _
                                                 ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica FormaDeEnvio
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0068_cls = (From p In data.P_0068_cls Where p.id_P_0068 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoClasificacionModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoClasificacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0069_cls = (From p In data.P_0069_cls Where p.id_P_0069 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function PaisModi(ByVal Id As Integer, _
                             ByVal des As String, _
                             ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Pais
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0070_cls = (From p In data.P_0070_cls Where p.id_P_0070 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function OtroModi(ByVal Id As Integer, _
                                 ByVal des As String, _
                                 ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Otro
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0071_cls = (From p In data.P_0071_cls Where p.id_P_0071 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Otro1Modi(ByVal Id As Integer, _
                                 ByVal des As String, _
                                 ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Otro1
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0072_cls = (From p In data.P_0072_cls Where p.id_P_0072 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Otro2Modi(ByVal Id As Integer, _
                                     ByVal des As String, _
                                     ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Otro2
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0073_cls = (From p In data.P_0073_cls Where p.id_P_0073 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Otro3Modi(ByVal Id As Integer, _
                                     ByVal des As String, _
                                     ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Otro3
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0074_cls = (From p In data.P_0074_cls Where p.id_P_0074 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoOperaciónContableModi(ByVal Id As Integer, _
                                         ByVal des As String, _
                                         ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoOperacionContable
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0075_cls = (From p In data.P_0075_cls Where p.id_P_0075 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SegmentosModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Segmentos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0076_cls = (From p In data.P_0076_cls Where p.id_P_0076 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoBeneficiarioModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoBeneficiario
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0077_cls = (From p In data.P_0077_cls Where p.id_P_0077 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ActuacionApoderadoModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ActuacionApoderado
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0078_cls = (From p In data.P_0078_cls Where p.id_P_0078 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ContratosModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Contratos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0079_cls = (From p In data.P_0079_cls Where p.id_P_0079 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonadeRiesgoRecaudaciónModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ZonaDeRiesgoRecaudacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0080_cls = (From p In data.P_0080_cls Where p.id_P_0080 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function PlataformasModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Plataformas
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0081_cls = (From p In data.P_0081_cls Where p.id_P_0081 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoNegociaciónModi(ByVal Id As Integer, _
                                                ByVal des As String, _
                                                ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0082_cls = (From p In data.P_0082_cls Where p.id_P_0082 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CiudadModi(ByVal Id As Integer, _
                                                ByVal des As String, _
                                                ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Ciudad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0084_cls = (From p In data.P_0084_cls Where p.id_P_0084 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function MesesModi(ByVal Id As Integer, _
                                                ByVal des As String, _
                                                ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Meses
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0085_cls = (From p In data.P_0085_cls Where p.id_P_0085 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadosdeCuentasModi(ByVal Id As Integer, _
                                                ByVal des As String, _
                                                ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadodeCuentas
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0086_cls = (From p In data.P_0086_cls Where p.id_P_0086 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function OrigendeFondoModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica OrigenDeFondo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0087_cls = (From p In data.P_0087_cls Where p.id_P_0087 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipodeEnvíoModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeEnvio
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0088_cls = (From p In data.P_0088_cls Where p.id_P_0088 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoOpeNegociacionModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoOperacionNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0089_cls = (From p In data.P_0089_cls Where p.id_P_0089 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoCobNegModi(ByVal Id As Integer, _
                                                    ByVal des As String, _
                                                    ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoCobroNegociacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0090_cls = (From p In data.P_0090_cls Where p.id_P_0090 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipodeCartasModi(ByVal Id As Integer, _
                                                   ByVal des As String, _
                                                   ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeCarta
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0091_cls = (From p In data.P_0091_cls Where p.id_P_0091 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ParametroConsultaModi(ByVal Id As Integer, _
                                                   ByVal des As String, _
                                                   ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ParametroConsultaApi
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0099_cls = (From p In data.P_0099_cls Where p.id_P_0099 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipodeProductosModi(ByVal Id As Integer, _
                                                   ByVal des As String, _
                                                   ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeProductos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0100_cls = (From p In data.P_0100_cls Where p.id_P_0100 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoComisionFactoringElectronicoModi(ByVal Id As Integer, _
                                                       ByVal des As String, _
                                                       ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoComicionFactoringElectronico
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0101_cls = (From p In data.P_0101_cls Where p.id_P_0101 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ParametrosTipoProvisionesModi(ByVal Id As Integer, _
                                                           ByVal des As String, _
                                                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica ParametrosTipoProvisiones
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0102_cls = (From p In data.P_0102_cls Where p.id_P_0102 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadonoRecaudadoModi(ByVal Id As Integer, _
                                                           ByVal des As String, _
                                                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoNoRecaudado
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0103_cls = (From p In data.P_0103_cls Where p.id_P_0103 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CaracteristicaOperaciónModi(ByVal Id As Integer, _
                                                               ByVal des As String, _
                                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CaracteristicaOperacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0104_cls = (From p In data.P_0104_cls Where p.id_P_0104 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoLíneaFogapeModi(ByVal Id As Integer, _
                                                               ByVal des As String, _
                                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadolineaFogape
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0105_cls = (From p In data.P_0105_cls Where p.id_P_0105 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoDevoluciónModi(ByVal Id As Integer, _
                                       ByVal des As String, _
                                       ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDevolucion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0106_cls = (From p In data.P_0106_cls Where p.id_P_0106 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CargaMasivaDocumentoModi(ByVal Id As Integer, _
                                           ByVal des As String, _
                                           ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CargasMasivasDocumentos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0107_cls = (From p In data.P_0107_cls Where p.id_P_0107 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CargaMasivaPagoClienteModi(ByVal Id As Integer, _
                                               ByVal des As String, _
                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CargasMasivasPagoCliente
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0108_cls = (From p In data.P_0108_cls Where p.id_P_0108 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CargaMasivaPagoDeudorModi(ByVal Id As Integer, _
                                               ByVal des As String, _
                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CargaMasivaPagoDeudor
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0109_cls = (From p In data.P_0109_cls Where p.id_P_0109 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoEvaluacionModi(ByVal Id As Integer, _
                                               ByVal des As String, _
                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoEvaluacion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0110_cls = (From p In data.P_0110_cls Where p.id_P_0110 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoCondicionModi(ByVal Id As Integer, _
                                              ByVal des As String, _
                                              ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoCondicion
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As p_0111_cls = (From p In data.p_0111_cls Where p.id_p_0111 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CustodiaModi(ByVal Id As Integer, _
                                              ByVal des As String, _
                                              ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Custodia
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0112_cls = (From p In data.P_0112_cls Where p.id_P_0112 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function InformesporMailModi(ByVal Id As Integer, _
                                               ByVal des As String, _
                                               ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica informesPorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0300_cls = (From p In data.P_0300_cls Where p.id_P_0300 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function HorarioInformesporMailModi(ByVal Id As Integer, _
                                                   ByVal des As String, _
                                                   ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica HorarioInformesPorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0301_cls = (From p In data.P_0301_cls Where p.id_P_0301 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function UsuariosparaNominaDiariadeNegociosModi(ByVal Id As Integer, _
                                                      ByVal des As String, _
                                                      ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica UsuariosParaNominaDiariaDeNegocios
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0302_cls = (From p In data.P_0302_cls Where p.id_P_0302 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipodeServiciodeLlamadaModi(ByVal Id As Integer, _
                                                          ByVal des As String, _
                                                          ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoDeServicioLlmada
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0303_cls = (From p In data.P_0303_cls Where p.id_P_0303 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EnvioporEmailModi(ByVal Id As Integer, _
                                      ByVal des As String, _
                                      ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EnvioPorMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0304_cls = (From p In data.P_0304_cls Where p.id_P_0304 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SaludosEnvioEmailModi(ByVal Id As Integer, _
                                          ByVal des As String, _
                                          ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica SaludosEnvioMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0305_cls = (From p In data.P_0305_cls Where p.id_P_0305 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TextodelEnvioEmailModi(ByVal Id As Integer, _
                                              ByVal des As String, _
                                              ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TextoDeEnvioMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0306_cls = (From p In data.P_0306_cls Where p.id_P_0306 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function MensajedeDespedidadelEnvioEmailModi(ByVal Id As Integer, _
                                                  ByVal des As String, _
                                                  ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica MensajeDeDespedidaEnvioMail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0307_cls = (From p In data.P_0307_cls Where p.id_P_0307 = Id).First
            With Modi
                .pnu_est = est
                .pnu_des = des
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function MensajedePublicidaddelEnvioEmailModi(ByVal Id As Integer, _
                                                      ByVal des As String, _
                                                      ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica MensajeDePublicidadEnvioEmail
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0308_cls = (From p In data.P_0308_cls Where p.id_P_0308 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est

            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EstadoUsuariosModi(ByVal Id As Integer, _
                                        ByVal des As String, _
                                        ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica EstadoUsuario
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0309_cls = (From p In data.P_0309_cls Where p.id_P_0309 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function TipoCierreContableModi(ByVal Id As Integer, _
                                            ByVal des As String, _
                                            ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica TipoCierreContable
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0310_cls = (From p In data.P_0310_cls Where p.id_P_0310 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "Parametros Numericos Elimina"
    Public Function EliminaParametros(ByVal Codigotabla As TablaParametro, ByVal CodigoParametro As Integer) As Boolean
        Try
            Dim data As New DataClsFactoringDataContext
            Dim parametro As Object
            Select Case Codigotabla

                Case TablaParametro.Region

                    parametro = (From p In data.P_001_cls Where p.id_P_001 = CodigoParametro).First
                    data.P_001_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.ComunaLocalidad
                    parametro = (From p In data.P_002_cls Where p.id_P_002 = CodigoParametro).First
                    data.P_002_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.EstadoDeudor
                    parametro = (From p In data.P_003_cls Where p.id_P_003 = CodigoParametro).First
                    data.P_003_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Niveles
                    parametro = (From p In data.P_005_cls Where p.id_P_005 = CodigoParametro).First
                    data.P_005_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ModoOperacion
                    parametro = (From p In data.P_007_cls Where p.id_P_007 = CodigoParametro).First
                    data.P_007_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoCliente
                    parametro = (From p In data.P_008_cls Where p.id_P_008 = CodigoParametro).First
                    data.P_008_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoPoderes
                    parametro = (From p In data.P_0010_cls Where p.id_P_0010 = CodigoParametro).First
                    data.P_0010_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoDocumento
                    parametro = (From p In data.P_0011_cls Where p.id_P_0011 = CodigoParametro).First
                    data.P_0011_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoOperacion
                    parametro = (From p In data.P_0012_cls Where p.id_P_0012 = CodigoParametro).First
                    data.P_0012_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.CartaTipo
                    parametro = (From p In data.P_0015_cls Where p.id_P_0015 = CodigoParametro).First
                    data.P_0015_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Zonas
                    parametro = (From p In data.P_0017_cls Where p.id_P_0017 = CodigoParametro).First
                    data.P_0017_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.FormasDePagos
                    parametro = (From p In data.P_0018_cls Where p.id_P_0018 = CodigoParametro).First
                    data.P_0018_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Sistemas
                    parametro = (From p In data.P_0020_cls Where p.id_P_0020 = CodigoParametro).First
                    data.P_0020_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoPagare
                    parametro = (From p In data.P_0021_cls Where p.id_P_0021 = CodigoParametro).First
                    data.P_0021_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoPagare
                    parametro = (From p In data.P_0022_cls Where p.id_P_0022 = CodigoParametro).First
                    data.P_0022_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Moneda
                    parametro = (From p In data.P_0023_cls Where p.id_P_0023 = CodigoParametro).First
                    data.P_0023_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoGarantia
                    parametro = (From p In data.P_0024_cls Where p.id_P_0024 = CodigoParametro).First
                    data.P_0024_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.RegimenMatrimonial
                    parametro = (From p In data.P_002_cls Where p.id_P_002 = CodigoParametro).First
                    data.P_002_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoAval
                    parametro = (From p In data.P_0026_cls Where p.id_P_0026 = CodigoParametro).First
                    data.P_0026_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoAval
                    parametro = (From p In data.P_0027_cls Where p.id_P_0027 = CodigoParametro).First
                    data.P_0027_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoSolicitudLinea
                    parametro = (From p In data.P_0028_cls Where p.id_P_0028 = CodigoParametro).First
                    data.P_0028_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoLinea
                    parametro = (From p In data.P_0029_cls Where p.id_P_0029 = CodigoParametro).First
                    data.P_0029_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoOperacion
                    parametro = (From p In data.P_0030_cls Where p.id_P_0030 = CodigoParametro).First
                    data.P_0030_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoDocumento
                    parametro = (From p In data.P_0031_cls Where p.id_P_0031 = CodigoParametro).First
                    data.P_0031_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoGastoOperacional
                    parametro = (From p In data.P_0036_cls Where p.id_P_0036 = CodigoParametro).First
                    data.P_0036_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoVerificacion
                    parametro = (From p In data.P_0040_cls Where p.id_P_0040 = CodigoParametro).First
                    data.P_0040_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoCuentasXCobrar
                    parametro = (From p In data.p_0041_cls Where p.id_P_0041 = CodigoParametro).First
                    data.p_0041_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoCliente
                    parametro = (From p In data.P_0044_cls Where p.id_P_0044 = CodigoParametro).First
                    data.P_0044_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoEjecutivo
                    parametro = (From p In data.P_0045_cls Where p.id_P_0045 = CodigoParametro).First
                    data.P_0045_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoEjecutivo
                    parametro = (From p In data.P_0048_cls Where p.id_P_0048 = CodigoParametro).First
                    data.P_0048_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoTelefono
                    parametro = (From p In data.P_0049_cls Where p.id_P_0049 = CodigoParametro).First
                    data.P_0049_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoGastoRecaudacion
                    parametro = (From p In data.P_0051_cls Where p.id_P_0051 = CodigoParametro).First
                    data.P_0051_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoDctoPago
                    parametro = (From p In data.P_0052_cls Where p.id_P_0052 = CodigoParametro).First
                    data.P_0052_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.QueSePaga
                    parametro = (From p In data.P_0053_cls Where p.id_P_0053 = CodigoParametro).First
                    data.P_0053_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoIngreso
                    parametro = (From p In data.p_0054_cls Where p.id_p_0054 = CodigoParametro).First
                    data.p_0054_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.QueAPagar
                    parametro = (From p In data.P_0055_cls Where p.id_P_0055 = CodigoParametro).First
                    data.P_0055_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoEgreso
                    parametro = (From p In data.P_0056_cls Where p.id_P_0056 = CodigoParametro).First
                    data.P_0056_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CategoriaRiesgo
                    parametro = (From p In data.P_0058_cls Where p.id_P_0058 = CodigoParametro).First
                    data.P_0058_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoFactura
                    parametro = (From p In data.P_0060_cls Where p.id_P_0060 = CodigoParametro).First
                    data.P_0060_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.MotivosDeProtestos
                    parametro = (From p In data.P_0061_cls Where p.id_P_0061 = CodigoParametro).First
                    data.P_0061_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.FacultadesPoder
                    parametro = (From p In data.P_0062_cls Where p.id_P_0062 = CodigoParametro).First
                    data.P_0062_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.RazonesSociales
                    parametro = (From p In data.P_0063_cls Where p.id_P_0063 = CodigoParametro).First
                    data.P_0063_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ActividadEconomica
                    parametro = (From p In data.P_0064_cls Where p.id_P_0064 = CodigoParametro).First
                    data.P_0063_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoRiesgo
                    parametro = (From p In data.P_0065_cls Where p.id_P_0065 = CodigoParametro).First
                    data.P_0065_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoEnvioInformacion
                    parametro = (From p In data.P_0067_cls Where p.id_P_0067 = CodigoParametro).First
                    data.P_0067_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.FormaEnvio
                    parametro = (From p In data.P_0068_cls Where p.id_P_0068 = CodigoParametro).First
                    data.P_0068_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Pais
                    parametro = (From p In data.P_0070_cls Where p.id_P_0070 = CodigoParametro).First
                    data.P_0070_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.otro
                    parametro = (From p In data.P_0071_cls Where p.id_P_0071 = CodigoParametro).First
                    data.P_0071_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.otro1
                    parametro = (From p In data.P_0072_cls Where p.id_P_0072 = CodigoParametro).First
                    data.P_0072_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.otro2
                    parametro = (From p In data.P_0073_cls Where p.id_P_0073 = CodigoParametro).First
                    data.P_0073_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.otro3
                    parametro = (From p In data.P_0074_cls Where p.id_P_0074 = CodigoParametro).First
                    data.P_0074_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoOperacionContable
                    parametro = (From p In data.P_0075_cls Where p.id_P_0075 = CodigoParametro).First
                    data.P_0075_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Segmentos
                    parametro = (From p In data.P_0076_cls Where p.id_P_0076 = CodigoParametro).First
                    data.P_0076_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoBeneficiario
                    parametro = (From p In data.P_0077_cls Where p.id_P_0077 = CodigoParametro).First
                    data.P_0077_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ActuacionApoderado
                    parametro = (From p In data.P_0078_cls Where p.id_P_0078 = CodigoParametro).First
                    data.P_0078_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Contratos
                    parametro = (From p In data.P_0079_cls Where p.id_P_0079 = CodigoParametro).First
                    data.P_0079_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ZonaRecaudacion
                    parametro = (From p In data.P_0080_cls Where p.id_P_0080 = CodigoParametro).First
                    data.P_0080_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.plataformas
                    parametro = (From p In data.P_0081_cls Where p.id_P_0081 = CodigoParametro).First
                    data.P_0081_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoNegociacion
                    parametro = (From p In data.P_0082_cls Where p.id_P_0082 = CodigoParametro).First
                    data.P_0082_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ObjetivoCredito
                    parametro = (From p In data.P_0083_cls Where p.id_P_0083 = CodigoParametro).First
                    data.P_0083_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Ciudad
                    parametro = (From p In data.P_0084_cls Where p.id_P_0084 = CodigoParametro).First
                    data.P_0084_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.Meses
                    parametro = (From p In data.P_0085_cls Where p.id_P_0085 = CodigoParametro).First
                    data.P_0085_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadosCuentas
                    parametro = (From p In data.P_0086_cls Where p.id_P_0086 = CodigoParametro).First
                    data.P_0086_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.OrigenFondo
                    parametro = (From p In data.P_0087_cls Where p.id_P_0087 = CodigoParametro).First
                    data.P_0087_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoEnvio
                    parametro = (From p In data.P_0088_cls Where p.id_P_0088 = CodigoParametro).First
                    data.P_0088_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoOpeNegociacion
                    parametro = (From p In data.P_0089_cls Where p.id_P_0089 = CodigoParametro).First
                    data.P_0089_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoCobNeg
                    parametro = (From p In data.P_0090_cls Where p.id_P_0090 = CodigoParametro).First
                    data.P_0090_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoCartas
                    parametro = (From p In data.P_0091_cls Where p.id_P_0091 = CodigoParametro).First
                    data.P_0091_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ParametroConsulta
                    parametro = (From p In data.P_0099_cls Where p.id_P_0099 = CodigoParametro).First
                    data.P_0099_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoProductos
                    parametro = (From p In data.P_0100_cls Where p.id_P_0100 = CodigoParametro).First
                    data.P_0100_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoComisionFactoringElectronico
                    parametro = (From p In data.P_0101_cls Where p.id_P_0101 = CodigoParametro).First
                    data.P_0101_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ParametrosTipoProvisiones
                    parametro = (From p In data.P_0102_cls Where p.id_P_0102 = CodigoParametro).First
                    data.P_0102_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoNoRecaudado
                    parametro = (From p In data.P_0103_cls Where p.id_P_0103 = CodigoParametro).First
                    data.P_0103_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CaracteristicaOperación
                    parametro = (From p In data.P_0104_cls Where p.id_P_0104 = CodigoParametro).First
                    data.P_0104_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoLineaFogape
                    parametro = (From p In data.P_0105_cls Where p.id_P_0105 = CodigoParametro).First
                    data.P_0105_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoDevolucion
                    parametro = (From p In data.P_0106_cls Where p.id_P_0106 = CodigoParametro).First
                    data.P_0106_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CargaMasivaDocumento
                    parametro = (From p In data.P_0107_cls Where p.id_P_0107 = CodigoParametro).First
                    data.P_0107_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CargaMasivaPagoCliente
                    parametro = (From p In data.P_0108_cls Where p.id_P_0108 = CodigoParametro).First
                    data.P_0108_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CargaMasivaPagoDeudor
                    parametro = (From p In data.P_0109_cls Where p.id_P_0109 = CodigoParametro).First
                    data.P_0109_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.EstadoEvaluacion
                    parametro = (From p In data.P_0110_cls Where p.id_P_0110 = CodigoParametro).First
                    data.P_0110_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.EstadoCondicion
                    parametro = (From p In data.p_0111_cls Where p.id_p_0111 = CodigoParametro).First
                    data.p_0111_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

                Case TablaParametro.Custodia

                    parametro = (From p In data.P_0112_cls Where p.id_P_0112 = CodigoParametro).First
                    data.P_0112_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()


                Case TablaParametro.InformesPorMail
                    parametro = (From p In data.P_0300_cls Where p.id_P_0300 = CodigoParametro).First
                    data.P_0300_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.HorarioInformesPorMail
                    parametro = (From p In data.P_0301_cls Where p.id_P_0301 = CodigoParametro).First
                    data.P_0301_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.UsuariosNominaDiariaNegocios
                    parametro = (From p In data.P_0302_cls Where p.id_P_0302 = CodigoParametro).First
                    data.P_0302_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoServicioLlamada
                    parametro = (From p In data.P_0303_cls Where p.id_P_0303 = CodigoParametro).First
                    data.P_0303_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EnvioPorEmail
                    parametro = (From p In data.P_0304_cls Where p.id_P_0304 = CodigoParametro).First
                    data.P_0304_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.SaludosEnvioEmail
                    parametro = (From p In data.P_0305_cls Where p.id_P_0305 = CodigoParametro).First
                    data.P_0305_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TextoEnvioEmail
                    parametro = (From p In data.P_0306_cls Where p.id_P_0306 = CodigoParametro).First
                    data.P_0306_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.MensajeDespedidaEnvioEmail
                    parametro = (From p In data.P_0307_cls Where p.id_P_0307 = CodigoParametro).First
                    data.P_0307_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.MensajePublicidadEnvioEmail
                    parametro = (From p In data.P_0308_cls Where p.id_P_0308 = CodigoParametro).First
                    data.P_0308_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.EstadoUsuarios
                    parametro = (From p In data.P_0309_cls Where p.id_P_0309 = CodigoParametro).First
                    data.P_0309_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoCierreContable
                    parametro = (From p In data.P_0310_cls Where p.id_P_0310 = CodigoParametro).First
                    data.P_0310_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoClasificacion
                    parametro = (From p In data.P_0069_cls Where p.id_P_0069 = CodigoParametro).First
                    data.P_0069_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.ClasificacionCliente
                    parametro = (From p In data.P_0118_cls Where p.id_P_0118 = CodigoParametro).First
                    data.P_0118_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.TipoCuenta
                    parametro = (From p In data.P_0312_cls Where p.id_P_0312 = CodigoParametro).First
                    data.P_0312_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()
                Case TablaParametro.CORASU
                    parametro = (From p In data.P_0313_cls Where p.id_P_0313 = CodigoParametro).First
                    data.P_0313_cls.DeleteOnSubmit(parametro)
                    data.SubmitChanges()

            End Select

            Return True


        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Reemplazo Cobradores Telefónicos"
    Public Function ReemplazosCobradoresInsertar(ByVal CobradorReemplazo As Integer, ByVal CobradorOrigen As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta Reemplazo para un Cobrador en particular
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 19/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext
            Dim Reemplazo = New ncr_cls

            Reemplazo.id_eje = CobradorReemplazo
            Reemplazo.id_eje_rpz = CobradorOrigen

            Data.ncr_cls.InsertOnSubmit(Reemplazo)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function ReemplazosCobradoresBorrar(ByVal CobradorReemplazo As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra Reemplazos para un Cobrador en particular
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 19/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim eliminaReemplazo = (From c In Data.ncr_cls _
                                  Where c.id_eje = CInt(CobradorReemplazo)) '.ToList()(0)

            Data.ncr_cls.DeleteAllOnSubmit(eliminaReemplazo)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

#End Region

#Region "Paridad"
    Public Function Feriados_Guarda(ByVal LSTB As ListBox) As Boolean
        '    'Descripcion: Guarda feriados
        '    'Creado por= Pablo Gatica S.
        '    'Fecha Creacion: 03/09/2008
        '    'Quien Modifica              Fecha              Descripcion
        '    '
        Try

            Dim fer As fer_cls

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim I As Integer

            For I = 0 To LSTB.Items.Count - 1
                Dim c = From cf In Data.fer_cls Where cf.fer_fec = LSTB.Items(I).Value
                If c.Count > 0 Then
                Else
                    fer = New fer_cls
                    fer.fer_fec = LSTB.Items(I).Value
                    Data.fer_cls.InsertOnSubmit(fer)
                    Data.SubmitChanges()
                End If
            Next

            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Feriados_Elimina(ByVal FECHA As DateTime) As Boolean
        '    'Descripcion: Elimina Feriado seleccionado
        '    'Creado por= Pablo Gatica S.
        '    'Fecha Creacion: 03/09/2008
        '    'Quien Modifica              Fecha              Descripcion
        '    '
        Try


            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim x = From cf In Data.fer_cls Where cf.fer_fec = FECHA

            If x.Count > 0 Then
                Dim c = (From cf In Data.fer_cls Where cf.fer_fec = FECHA).First

                If Not IsDBNull(c) Then


                    Data.fer_cls.DeleteOnSubmit(c)
                    Data.SubmitChanges()
                    Return True
                Else
                    Return False
                End If

            End If



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GuardaParidad(ByVal fecha As DateTime, ByVal valor As Double, ByVal valorcob As Double, ByVal tipmon As Int32) As Boolean
        'Descripcion: Guardar Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try


            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim Paridad = New par_cls()

            Paridad.par_fec = fecha
            Paridad.par_val = valor
            Paridad.par_val_cob = valorcob
            Paridad.id_P_0023 = tipmon



            Data.par_cls.InsertOnSubmit(Paridad)
            Data.SubmitChanges()

            Return True



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function eliminaparidades(ByVal fecha As Date, ByVal tipmon As Int32) As Boolean
        Try
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim eliminaParidad = (From c In Data.par_cls _
                                  Where c.par_fec = fecha And c.id_P_0023 = tipmon) '.ToList()(0)

            Data.par_cls.DeleteAllOnSubmit(eliminaParidad)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function GuardaParidades(ByVal Coll_MonNeg As Collection) As Boolean
        '********************************************************************************************************************************
        'Descripcion: Guardar las Paridades del dia de la negociacion
        'Creado por Jorge Lagos
        'Fecha Creacion: 30/01/2009
        'Quien Modifica              Fecha              Descripcion
        '********************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext

            Try

                Dim id As Decimal = CDec(Coll_MonNeg.Item(1).id_opn)
                Dim nmn = From N In Data.nmn_cls Where N.id_opn = id

                Data.nmn_cls.DeleteAllOnSubmit(nmn)

            Catch ex As Exception

            End Try

            For I = 1 To Coll_MonNeg.Count
                Data.nmn_cls.InsertOnSubmit(Coll_MonNeg.Item(I))
            Next

            Data.SubmitChanges()

            Return True



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Ejecutivos"

    'Public Function AsignaEjecutivoDeCuentas(ByVal Coll_Clientes_Asignar As Collection) As Boolean

    '    '*********************************************************************************************************************************
    '    'Descripcion: Asigna a un cliente un ejecutivo de cuentas, recibe una collection de cliente a actualizar
    '    'Creado por= Jorge Lagos C.
    '    'Fecha Creacion: 10/06/2008
    '    'Quien Modifica              Fecha              Descripcion
    '    '*********************************************************************************************************************************

    '    Try

    '        Dim Data As New DataClsFactoringDataContext
    '        Dim CodigoEjecutivo As Integer = Coll_Clientes_Asignar.Item(1).id_eje_cod_eje
    '        Dim CodigoSucursal As String

    '        Dim Ejecutivo = From E In Data.eje_cls Where E.id_eje = CodigoEjecutivo

    '        For Each Eje In Ejecutivo
    '            CodigoSucursal = Eje.id_suc
    '        Next

    '        For I = 1 To Coll_Clientes_Asignar.Count

    '            Dim Rut As String = Coll_Clientes_Asignar.Item(I).cli_idc

    '            Dim Cli As cli_cls = (From C In Data.cli_cls Where C.cli_idc = Rut).First

    '            Cli.id_eje_cod_eje = Coll_Clientes_Asignar.Item(I).id_eje_cod_eje
    '            Cli.cli_fec_act_eje = Coll_Clientes_Asignar.Item(I).cli_fec_act_eje
    '            Cli.id_suc = CodigoSucursal

    '            Dim Rsc As rsc_cls = (From R In Data.rsc_cls Where R.cli_idc = Rut).First

    '            'Rsc.id_eje_cod_eje = Coll_Clientes_Asignar.Item(I).id_eje_cod_eje
    '            'Rsc.suc_cod_ftg = CodigoSucursal

    '        Next

    '        Data.SubmitChanges()

    '        'update cli SET id_eje_cod_eje     = @codigo_eje,
    '        '                    cli_fec_act_eje = @cli_fec_act_eje,
    '        '                    suc_cod_ftg     = @cod_suc
    '        '     where cli_idc = @rut_cliente

    '        'update rsc SET id_eje_cod_eje = @codigo_eje,
    '        '               suc_cod_ftg = @cod_suc
    '        'where cli_idc = @rut_cliente



    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    Finally
    '    End Try

    'End Function

    Public Function AsignaEjecutivoDeCuentas(ByVal Coll_Clientes_Asignar As Collection) As Boolean

        Try
            Dim ds As New DataSet
            Dim CodigoEjecutivo As Integer = Coll_Clientes_Asignar.Item(1).id_eje_cod_eje
            Dim CodigoSucursal As String
            Dim sqlstr As String

            sqlstr = "SELECT * FROM EJE WHERE ID_EJE = " & CodigoEjecutivo

            ds = Sql.ExecuteDataSet(sqlstr)

            If Not IsNothing(ds) And ds.Tables(0).Rows.Count > 0 Then

                CodigoSucursal = Integer.Parse(ds.Tables(0).Rows(0)("id_suc").ToString())

                For I = 1 To Coll_Clientes_Asignar.Count

                    Dim Rut As String = Coll_Clientes_Asignar.Item(I).cli_idc

                    sql = Nothing

                    sql = "exec sp_web_AsignaEjecutivoDeCuentas " & Rut & ", " & Coll_Clientes_Asignar.Item(I).id_eje_cod_eje & ", '" & FC.FechaJuliana(Coll_Clientes_Asignar.Item(I).cli_fec_act_eje.ToString()) & "'," & CodigoSucursal & ""

                    If sql.ExecuteNonQuery(sqlstr) > 0 Then
                        Return True
                    Else
                        Return False
                    End If


                Next

                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AsignaEjecutivoCobrador(ByVal Coll_Deudores_Asignar As Collection) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Asigna cobrador a deudores
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 20/05/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim CodigoEjecutivo As Integer = Coll_Deudores_Asignar.Item(1).id_eje_cod_cob

            For I = 1 To Coll_Deudores_Asignar.Count

                Dim Rut As String = RG.LimpiaRut(Coll_Deudores_Asignar.Item(I).deu_ide)

                Rut = Format(CLng(Rut), Var.FMT_RUT)

                Dim Deu As deu_cls = (From C In Data.deu_cls Where C.deu_ide = Rut).First

                Deu.id_eje_cod_cob = Coll_Deudores_Asignar.Item(I).id_eje_cod_cob

            Next

            Data.SubmitChanges()

            '--------------------------------------------------------------------------------

            'UPDATE deu SET eje_cod_cob = @codigo_cobrador
            'WHERE deu_ide = @rut_deudor

            'No aplica
            'UPDATE rsd SET eje_cod_cob = @codigo_cobrador
            'WHERE ddr_ide = @rut_deudor



            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function


#End Region

#Region "Linea de Credito"

    Public Function LineaDeCreditoInserta(ByVal LDC As ldc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Dim Data As New DataClsFactoringDataContext

        Try

            Dim mto_adeudado As Double = (From d In Data.doc_cls Where d.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = LDC.cli_idc And ( _
                                          d.dsi_cls.id_P_0011 = 1 _
                                          Or d.dsi_cls.id_P_0011 = 2 _
                                          Or d.dsi_cls.id_P_0011 = 4 _
                                          Or d.dsi_cls.id_P_0011 = 9 _
                                          Or d.dsi_cls.id_P_0011 = 11 _
                                          Or d.dsi_cls.id_P_0011 = 12) And _
                                          d.dsi_cls.ope_cls.opn_cls.opn_res_son = "1" _
                                          Group By d.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc Into Sum(d.doc_sdo_cli) Select Sum).First
            'Dim ldc_vig As ldc_cls = (From l In Data.ldc_cls Where l.cli_idc = LDC.cli_idc And l.id_P_0029 <> 4 Order By l.id_ldc Descending).First

            If LDC.ldc_mto_sol < mto_adeudado Then
                Return False
            Else
                Data.ldc_cls.InsertOnSubmit(LDC)
                Data.SubmitChanges()
                Return True
            End If

        Catch ex As Exception

            Try

                Data.ldc_cls.InsertOnSubmit(LDC)
                Data.SubmitChanges()

                Return True

            Catch e As Exception
                Return False
            End Try

        End Try

    End Function

    Public Function LineaDeCreditoUpdate(ByVal LDC As ldc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una nueva Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                  15/06/2012         Se incorpora porcentaje exceso permitido
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim LineaAprobada As ldc_cls

            'Busca linea que va aprobar (vigente)
            LineaAprobada = (From L In Data.ldc_cls Where L.id_ldc = LDC.id_ldc And L.cli_idc = LDC.cli_idc).First

            With LineaAprobada
                .ldc_fec_sol = LDC.ldc_fec_sol
                .ldc_fec_rsn = LDC.ldc_fec_rsn
                .ldc_fec_vig_dde = LDC.ldc_fec_vig_dde
                .ldc_fec_vig_hta = LDC.ldc_fec_vig_hta
                .ldc_adm_mor = LDC.ldc_adm_mor
                .ldc_mto_sol = LDC.ldc_mto_sol
                .ldc_por_exc = LDC.ldc_por_exc
                .ldc_mto_apb = LDC.ldc_mto_apb
                .ldc_mto_ocp = LDC.ldc_mto_ocp
                .id_P_0029 = LDC.id_P_0029
                .ldc_tip_com = LDC.ldc_tip_com
                .ldc_des_com = LDC.ldc_des_com
                .ldc_obs = LDC.ldc_obs
            End With

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function SubLineaDeCreditoInserta(ByVal SBL As sbl_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva Sub Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim SubLineas = From S In Data.sbl_cls Where S.id_ldc = SBL.id_ldc And S.id_P_0031 = SBL.id_P_0031

            If SubLineas.Count > 0 Then

                For Each s In SubLineas

                    If SBL.sbl_tip_pct_mto = "M" Then
                        s.sbl_tip_pct_mto = "M"
                        s.sbl_mto = SBL.sbl_mto
                    End If

                    If SBL.sbl_tip_pct_mto = "P" Then
                        s.sbl_tip_pct_mto = "P"
                        s.sbl_pct = SBL.sbl_pct
                    End If

                Next

            Else
                Data.sbl_cls.InsertOnSubmit(SBL)
            End If

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function SubLineaDeCreditoUpdate(ByVal SBL As sbl_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una sub linea para una nueva Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim SubL = (From L In Data.sbl_cls Where L.id_sbl = SBL.id_sbl And L.id_ldc = SBL.id_ldc).First()

            Dim SubLinea As sbl_cls = (From L In Data.sbl_cls Where L.id_sbl = SBL.id_sbl).First()

            With SubLinea
                .id_P_0031 = SBL.id_P_0031
                .sbl_mto = SBL.sbl_mto
                .sbl_pct = SBL.sbl_pct
                .sbl_tip = SBL.sbl_tip
                .sbl_tip_pct_mto = SBL.sbl_tip_pct_mto
            End With

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function SubLineaDeCreditoDelete(ByVal SBL As sbl_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Elimina una nueva Sub Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim SubLinea As sbl_cls = (From S In Data.sbl_cls Where S.id_sbl = SBL.id_sbl And S.id_ldc = SBL.id_ldc).First

            Data.sbl_cls.DeleteOnSubmit(SubLinea)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function AnticipoLineaDeCreditoInserta(ByVal APC As apc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un Anticipo para una Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 17/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Data.apc_cls.InsertOnSubmit(APC)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function AnticipoLineaDeCreditoUpdate(ByVal APC As apc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza un Anticipo para una Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 17/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Ap As apc_cls = (From A In Data.apc_cls Where A.id_apc = APC.id_apc And A.id_ldc = APC.id_ldc).First

            Ap.id_P_0031 = APC.id_P_0031
            Ap.apc_pct = APC.apc_pct
            Ap.apc_cob_son = APC.apc_cob_son
            Ap.apc_not_son = APC.apc_not_son
            Ap.apc_ver_son = APC.apc_ver_son

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function LineaDeCreditoVB(ByVal LDC As ldc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una nueva Linea de Credito para un Cliente
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                  15/06/2012         Se incorpora porcentaje exceso permitido
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            'Dim LineaVigente As ldc_cls
            Dim LineaAprobada As ldc_cls
            Dim MontoOcupado As Double = 0

            'Busca linea que va aprobar (vigente)
            LineaAprobada = (From L In Data.ldc_cls Where L.id_ldc = LDC.id_ldc And L.cli_idc = LDC.cli_idc).First

            Try

                'traemos la linea mayor (ultima)
                Dim LineaVigente = From L In Data.ldc_cls Where L.cli_idc = LDC.cli_idc And L.id_P_0029 <> 4 And L.id_P_0029 <> 2 Order By L.id_ldc Ascending

                For Each L In LineaVigente

                    L.id_P_0029 = 2 'Caducada

                    'Sublineas deben asociarse a la nueva linea vigente
                    Dim sublineas = From S In Data.sbl_cls Where S.id_ldc = L.id_ldc

                    For Each s In sublineas
                        s.id_ldc = LineaAprobada.id_ldc
                    Next

                    'Anticipos deben asociarse a la nueva linea vigente
                    Dim anticipos = From A In Data.apc_cls Where A.id_ldc = L.id_ldc

                    For Each a In anticipos
                        a.id_ldc = LineaAprobada.id_ldc
                    Next

                    'Actas digitales deben asociarse a la nueva linea vigente
                    Dim actas = From A In Data.act_img_cls Where A.id_ldc = L.id_ldc

                    For Each a In actas
                        a.id_ldc = LineaAprobada.id_ldc
                    Next

                    'ficha juridica deben asociarse a la nueva linea vigente
                    'Dim ficha = From A In Data.FIC_IMG_CLS Where A.id_ldc = LineaVigente.id_ldc

                    'For Each a In ficha
                    '    a.id_ldc = LineaAprobada.id_ldc
                    'Next

                    MontoOcupado = L.ldc_mto_ocp

                Next

                Data.SubmitChanges()

            Catch ex As Exception
                MontoOcupado = 0
            End Try

            With LineaAprobada
                .ldc_fec_sol = LDC.ldc_fec_sol
                .ldc_fec_rsn = LDC.ldc_fec_rsn
                .ldc_fec_vig_dde = LDC.ldc_fec_vig_dde
                .ldc_fec_vig_hta = LDC.ldc_fec_vig_hta
                .ldc_adm_mor = LDC.ldc_adm_mor
                .ldc_mto_sol = LDC.ldc_mto_sol
                .ldc_por_exc = LDC.ldc_por_exc
                .ldc_mto_apb = LDC.ldc_mto_apb
                .ldc_mto_ocp = MontoOcupado
                .id_P_0029 = LDC.id_P_0029
                .ldc_tip_com = LDC.ldc_tip_com
                .ldc_des_com = LDC.ldc_des_com
                .ldc_obs = LDC.ldc_obs
            End With

            Data.SubmitChanges()

            Dim sql As New FuncionesGenerales.SqlQuery

            sql.ExecuteNonQuery("Exec sp_op_cierre_cliente '" & LDC.cli_idc & "', '" & LDC.cli_idc & "', '" & DateTime.Now.ToString("yyyMMdd") & "'")


            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Mantencion de Deudores"

    Public Function ClientesDeudoresInserta(ByVal DDR As ddr_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un cliente para deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            'El rut no va con el mismo formato
            Try

                Dim dr = (From d In Data.ddr_cls Where d.cli_idc = DDR.cli_idc And d.deu_ide = DDR.deu_ide).First

                If dr.deu_ide <> "" Then
                    Return False
                End If

            Catch ex As Exception

            End Try

            Data.ddr_cls.InsertOnSubmit(DDR)
            Data.SubmitChanges()



            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DeudorUpdate(ByVal Deudor As deu_cls, ByVal id_ciu As Integer, ByVal mail_oficina As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza un Deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 24/06/2008
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                  22-06-2012         Se agregan parametros para radicacion de facturas               
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Deu As deu_cls = (From D In Data.deu_cls _
                            Where D.deu_ide = Format(CLng(Deudor.deu_ide), Var.FMT_RUT) _
                            Select D).First()

            Deu.deu_nom = Deudor.deu_nom
            Deu.id_suc = Deudor.id_suc
            Deu.id_P_0044 = Deudor.id_P_0044
            Deu.deu_nom = Deudor.deu_nom
            Deu.deu_rso = Deudor.deu_rso
            Deu.deu_ape_ptn = Deudor.deu_ape_ptn
            Deu.deu_ape_mtn = Deudor.deu_ape_mtn
            Deu.deu_dml = Deudor.deu_dml
            'Deu.ciu_num = Deudor.ciu_num
            Deu.id_cmn = Deudor.id_cmn
            Deu.id_P_003 = Deudor.id_P_003
            Deu.deu_pct_vta = Deudor.deu_pct_vta
            Deu.deu_ntf = Deudor.deu_ntf
            Deu.deu_con_cbz = Deudor.deu_con_cbz
            Deu.deu_chq_gir = Deudor.deu_chq_gir
            Deu.deu_cbz = Deudor.deu_cbz
            Deu.id_eje_cod_cob = Deudor.id_eje_cod_cob
            Deu.deu_obs_gsn = Deudor.deu_obs_gsn
            Deu.deu_fec_obs = Deudor.deu_fec_obs
            'Deu.deu_doc_rtr_pag = Deudor.deu_doc_rtr_pag
            'Deu.deu_dir_cbz = Deudor.deu_dir_cbz
            'Deu.cmn_num_cbz = Deudor.cmn_num_cbz
            'Deu.ciu_num_cbz = Deudor.ciu_num_cbz
            'Deu.deu_zon_cbz = Deudor.deu_zon_cbz
            'Deu.suc_cod_rcd = Deudor.suc_cod_rcd
            Deu.id_PL_000006 = Deudor.id_PL_000006
            Deu.deu_nom = Deudor.deu_nom
            Deu.deu_ape_ptn = Deudor.deu_ape_ptn
            Deu.deu_ape_mtn = Deudor.deu_ape_mtn
            Deu.id_P_0063 = Deudor.id_P_0063
            Deu.id_P_0064 = Deudor.id_P_0064
            Deu.deu_var_ddr = Deudor.deu_var_ddr
            Deu.id_P_0076 = Deudor.id_P_0076
            'Deu.deu_obs_ult_gsn = Deudor.deu_obs_ult_gsn
            'Deu.deu_zon_rgo_rec = Deudor.deu_zon_rgo_rec
            Deu.deu_atr_car = Deudor.deu_atr_car
            Deu.deu_des_car = Deudor.deu_des_car
            Deu.deu_rad_ori_fac = Deudor.deu_rad_ori_fac
            Deu.deu_rad_dia_vcto = Deudor.deu_rad_dia_vcto
            Deu.id_suc = Deudor.id_suc
            Deu.id_P_0313 = Deudor.id_P_0313

            Deu.id_eje = Deudor.id_eje
            Deu.ejecutivo = Deudor.ejecutivo
            Deu.deu_cod_ges = Deudor.deu_cod_ges
            Deu.id_p_0119 = Deudor.id_p_0119
            Deu.deu_dig_ito = Deudor.deu_dig_ito


            '----------------------------------------------------------------------------------------------------------------
            'Versión: 12122013.V1
            Deu.deu_cod_ges = Deudor.deu_cod_ges
            Deu.id_p_0119 = Deudor.id_p_0119
            Deu.deu_nro_cli = Deudor.deu_nro_cli

            'Deu.DEU_CAN_AL = Deudor.DEU_CAN_AL
            'Deu.DEU_SUB_CAN_AL = Deudor.DEU_SUB_CAN_AL

            'Deu.DEU_GEST_COD = Deudor.DEU_GEST_COD

            Data.SubmitChanges()

            Try

                If mail_oficina <> "" Then
                    Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.id_eje = Deudor.id_eje).First
                    ejecutivo.eje_mail = mail_oficina
                    Data.SubmitChanges()
                End If

                Dim sql As New FuncionesGenerales.SqlQuery
                Dim cnc As cnc_cls

                Try
                    cnc = (From c In Data.cnc_cls Where c.deu_ide = Deudor.deu_ide And c.cnc_rep_leg = "S" And c.cnc_cli_ddr = "D").First
                Catch ex As Exception

                End Try

                'Dim clscli As New ClaseClientes
                'Dim cnc As cnc_cls = clscli.ClienteRepresentanteDevuelveUnificado(Deudor.deu_ide)


                'If Not IsNothing(cnc) Then
                '    cnc.cnc_cli_ddr = "D"
                '    cnc.deu_ide = Deudor.deu_ide
                '    Data.cnc_cls.InsertOnSubmit(cnc)
                '    Data.SubmitChanges()
                'End If

                Try

                    Dim strsql As String = ""

                    If IsNothing(cnc) Then

                        strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Deudor.deu_ide & "', '" & _
                                                                                           Deudor.deu_dig_ito & "', '" & _
                                                                                           Deudor.deu_rso & "', '" & _
                                                                                           Deudor.deu_ape_ptn & "', '" & _
                                                                                           Deudor.deu_ape_mtn & "',  " & _
                                                                                           Deudor.id_P_0313 & ", " & _
                                                                                           Deudor.id_p_0119 & ",  " & _
                                                                                           Deudor.id_P_0044 & ", '" & _
                                                                                           Deudor.deu_nro_cli & "', '" & _
                                                                                           Deudor.deu_dml & "', " & _
                                                                                           id_ciu & ", '', '', '', '" & _
                                                                                           Deudor.id_PL_000006 & "', " & _
                                                                                           Deudor.id_P_0064 & ", '','" & _
                                                                                           Deudor.id_suc & "', " & _
                                                                                           Deudor.deu_cod_ges & ", '" & _
                                                                                           Deudor.ejecutivo & "', '" & _
                                                                                           Deudor.id_eje & "', '', '" & _
                                                                                           Nothing & "', '" & _
                                                                                           Nothing & "', " & _
                                                                                           Nothing
                        'Deudor.DEU_CAN_AL & "', '" & _
                        '                                                                   Deudor.DEU_SUB_CAN_AL & "', " & _
                        '                                                                   Deudor.DEU_GEST_COD
                    Else

                        strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Deudor.deu_ide & "', '" & _
                                                                                           Deudor.deu_dig_ito & "', '" & _
                                                                                           Deudor.deu_rso & "', '" & _
                                                                                           Deudor.deu_ape_ptn & "', '" & _
                                                                                           Deudor.deu_ape_mtn & "',  " & _
                                                                                           Deudor.id_P_0313 & ", " & _
                                                                                           Deudor.id_p_0119 & ",  " & _
                                                                                           Deudor.id_P_0044 & ", '" & _
                                                                                           Deudor.deu_nro_cli & "', '" & _
                                                                                           Trim(Deudor.deu_dml) & "', " & _
                                                                                           id_ciu & ", '" & _
                                                                                           Trim(cnc.cnc_nom) & "', '" & _
                                                                                           cnc.cnc_rut & "', '" & _
                                                                                           FC.Vrut(cnc.cnc_rut) & "', '" & _
                                                                                           Deudor.id_PL_000006 & "', " & _
                                                                                           Deudor.id_P_0064 & ", '','" & _
                                                                                           Deudor.id_suc & "', " & _
                                                                                           Deudor.deu_cod_ges & ", '" & _
                                                                                           Deudor.ejecutivo & "', '" & _
                                                                                           Deudor.id_eje & "', '', '" & _
                                                                                           Nothing & "', '" & _
                                                                                           Nothing & "', " & _
                                                                                           Nothing

                        'Deudor.DEU_CAN_AL & "', '" & _
                        '                                                                   Deudor.DEU_SUB_CAN_AL & "', " & _
                        '                                                                   Deudor.DEU_GEST_COD
                    End If

                    sql.ExecuteNonQuery(strsql)


                Catch ex As Exception
                    Return False
                Finally

                End Try

            Catch ex As Exception
                Return False
            Finally

            End Try

            '----------------------------------------------------------------------------------------------------------------

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    Public Function DeudorInserta(ByVal Deudor As deu_cls, ByVal id_ciu As Integer, ByVal mail_oficina As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un Deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 24/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim clscli As New ClaseClientes

            Data.deu_cls.InsertOnSubmit(Deudor)
            Data.SubmitChanges()

            Dim sql As New FuncionesGenerales.SqlQuery
            Dim strsql As String = ""

            Try

                If mail_oficina <> "" Then
                    Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.id_eje = Deudor.id_eje).First
                    ejecutivo.eje_mail = mail_oficina
                    Data.SubmitChanges()
                End If

                Dim cnc As cnc_cls = clscli.ClienteRepresentanteDevuelveUnificado(Deudor.deu_ide)

                If Not IsNothing(cnc) Then
                    cnc.cnc_cli_ddr = "D"
                    cnc.deu_ide = Deudor.deu_ide
                    Data.cnc_cls.InsertOnSubmit(cnc)
                    Data.SubmitChanges()
                End If

                If IsNothing(cnc) Then

                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Deudor.deu_ide & "', '" & _
                                                                                       Deudor.deu_dig_ito & "', '" & _
                                                                                       Deudor.deu_rso & "', '" & _
                                                                                       Deudor.deu_ape_ptn & "', '" & _
                                                                                       Deudor.deu_ape_mtn & "',  " & _
                                                                                       Deudor.id_P_0313 & ", " & _
                                                                                       Deudor.id_p_0119 & ",  " & _
                                                                                       Deudor.id_P_0044 & ", '" & _
                                                                                       Deudor.deu_nro_cli & "', '" & _
                                                                                       Deudor.deu_dml & "', " & _
                                                                                       id_ciu & ", '', '', '', '" & _
                                                                                       Deudor.id_PL_000006 & "', " & _
                                                                                       Deudor.id_P_0064 & ", '','" & _
                                                                                       Deudor.id_suc & "', '" & _
                                                                                       Deudor.deu_cod_ges & "', '" & _
                                                                                       Deudor.ejecutivo & "', '" & _
                                                                                       Deudor.id_eje & "', ''"

                Else
                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Deudor.deu_ide & "', '" & _
                                                                                       Deudor.deu_dig_ito & "', '" & _
                                                                                       Deudor.deu_rso & "', '" & _
                                                                                       Deudor.deu_ape_ptn & "', '" & _
                                                                                       Deudor.deu_ape_mtn & "',  " & _
                                                                                       Deudor.id_P_0313 & ", " & _
                                                                                       Deudor.id_p_0119 & ",  " & _
                                                                                       Deudor.id_P_0044 & ", '" & _
                                                                                       Deudor.deu_nro_cli & "', '" & _
                                                                                       Deudor.deu_dml & "', " & _
                                                                                       id_ciu & ", '" & _
                                                                                       cnc.cnc_nom & "', '" & _
                                                                                       cnc.cnc_rut & "', '" & _
                                                                                       FC.Vrut(cnc.cnc_rut) & "', '" & _
                                                                                       Deudor.id_PL_000006 & "', " & _
                                                                                       Deudor.id_P_0064 & ", '','" & _
                                                                                       Deudor.id_suc & "', " & _
                                                                                       Deudor.deu_cod_ges & ", '" & _
                                                                                       Deudor.ejecutivo & "', '" & _
                                                                                       Deudor.id_eje & "', ''"
                End If


                sql.ExecuteNonQuery(strsql)


            Catch ex As Exception
                Return False
            Finally

            End Try

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function


    Public Function TipoCuentaInserta(ByVal Id As P_0312_cls) As Boolean

        '**************************************************************************************************************************************************

        'Descripcion: Guarda TipoCuenta

        'Creado por= Sebastian Henriquez.

        'Fecha Creacion: 19/05/2012

        'Quien Modifica              Fecha              Descripcion

        '**************************************************************************************************************************************************



        Try

            Dim data As New DataClsFactoringDataContext

            data.P_0312_cls.InsertOnSubmit(Id)

            data.SubmitChanges()

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function TipoCuentaModi(ByVal Id As Integer, _
                                       ByVal des As String, _
                                       ByVal est As String) As Boolean

        '**************************************************************************************************************************************************

        'Descripcion: Modifica TipoCuenta

        'Creado por= Sebastian Henriquez.

        'Fecha Creacion: 19/05/2012

        'Quien Modifica              Fecha              Descripcion

        '**************************************************************************************************************************************************



        Try

            Dim data As New DataClsFactoringDataContext

            Dim Modi As P_0312_cls = (From p In data.P_0312_cls Where p.id_P_0312 = Id).First

            With Modi

                .pnu_des = des

                .pnu_est = est

            End With

            data.SubmitChanges()

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function DeuMonBorra(ByVal RUT_DEU As String) As Boolean 'FY 26-05-2012
        '*********************************************************************************************************************************
        'Descripcion: Inserta un Deudor
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 26/05/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim deu_mon = From d In Data.deu_mon_cls Where CInt(d.deu_ide) = Format(CLng(RUT_DEU), Var.FMT_RUT)

            Data.deu_mon_cls.DeleteAllOnSubmit(deu_mon)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeuMonBorra(ByVal RUT_DEU As String, ByVal moneda As Integer) As Boolean 'FY 26-05-2012
        '*********************************************************************************************************************************
        'Descripcion: Inserta un Deudor
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 26/05/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim deu_mon = From d In Data.deu_mon_cls Where CInt(d.deu_ide) = Format(CLng(RUT_DEU), Var.FMT_RUT) And d.id_p_0023 = moneda

            'Dim Deudor As deu_cls = (From D In Data.deu_cls Where CInt(D.deu_ide) = RutDeudor Select D).First

            Data.deu_mon_cls.DeleteAllOnSubmit(deu_mon)
            Data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DeuMoninserta(ByVal deu_mon As deu_mon_cls) As Boolean 'FY 26-05-2012
        '*********************************************************************************************************************************
        'Descripcion: Inserta un Deudor
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 26/05/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim cupos = From d In Data.deu_mon_cls Where d.deu_ide = deu_mon.deu_ide And d.id_p_0023 = deu_mon.id_p_0023

            For Each c In cupos
                c.id_p_0029 = 2 'caduca por que se creo una nueva
            Next

            Data.SubmitChanges()

            Data.deu_mon_cls.InsertOnSubmit(deu_mon)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DeudorCalendarioInserta(ByVal cpg As cpg_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un fecha de calendario de pago
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 29-05-2012
        'Quien Modifica              Fecha              Descripcion
        'Jlagos                     14-09-2012          Se agrego validacion por deudor para el dia de pago
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Try

                Dim cal = (From d In Data.cpg_cls Where d.fec_cpg = cpg.fec_cpg And d.deu_ide = cpg.deu_ide Select d).First

                If cal.id_cpg <> 0 Then
                    Return False
                End If

            Catch e As Exception

            End Try

            Data.cpg_cls.InsertOnSubmit(cpg)
            Data.SubmitChanges()


            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function DeudorCalendarioElimina(ByVal idcpg As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un fecha de calendario de pago
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 29-05-2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Data.cpg_cls.DeleteAllOnSubmit(From c In Data.cpg_cls Where c.id_cpg = idcpg)
            Data.SubmitChanges()


            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

#End Region

#Region "Dias De Pago"

    Public Function DiasDePagoInsertar(ByVal rut_deudor As String, _
                                   ByVal horarioLU As String, ByVal horarioMA As String, ByVal horarioMI As String, ByVal horarioJU As String, ByVal horarioVI As String, _
                                   ByVal restricLU As String, ByVal restricMA As String, ByVal restricMI As String, ByVal restricJU As String, ByVal restricVI As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta Dias de Pago para un Deudor en particular
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 20/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim Temporal_dpa = New dpa_cls()

            Temporal_dpa.deu_ide = rut_deudor
            Temporal_dpa.cli_idc = Nothing
            Temporal_dpa.dpa_hor_lun = horarioLU
            Temporal_dpa.dpa_res_lun = restricLU
            Temporal_dpa.dpa_hor_mar = horarioMA
            Temporal_dpa.dpa_res_mar = restricMA
            Temporal_dpa.dpa_hor_mie = horarioMI
            Temporal_dpa.dpa_res_mie = restricMI
            Temporal_dpa.dpa_hor_jue = horarioJU
            Temporal_dpa.dpa_res_jue = restricJU
            Temporal_dpa.dpa_hor_vie = horarioVI
            Temporal_dpa.dpa_res_vie = restricVI

            Data.dpa_cls.InsertOnSubmit(Temporal_dpa)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function DiasDePagoBorrar(ByVal ID_dpa_ide As Long) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra Dias de Pago para un Deudor en particular
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 20/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim Temporal_dpa As dpa_cls = (From dpa1 In Data.dpa_cls _
                                Where dpa1.deu_ide = ID_dpa_ide Select dpa1).First

            Data.dpa_cls.DeleteOnSubmit(Temporal_dpa)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function DiasPagoActualiza(ByVal DPA As dpa_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta o Modifica los días de pago asociados al deudor
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 09/02/2009
        'Quien Modifica              Fecha              Descripcion
        'JLAAGOS                    09-03-2009          SE CAMBIA NOMBRE DE DiasPagoInsert -> DiasPagoActualiza
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext

            Dim DiasPago1 = From DP In Data.dpa_cls _
                                       Where DP.deu_ide = Format(CLng(DPA.deu_ide), Var.FMT_RUT)


            If DiasPago1.Count <> 0 Then

                Dim DiasPago As dpa_cls = (From DP In Data.dpa_cls _
                                       Where DP.deu_ide = Format(CLng(DPA.deu_ide), Var.FMT_RUT)).First

                With DiasPago
                    .dpa_hor_lun = DPA.dpa_hor_lun
                    .dpa_res_lun = DPA.dpa_res_lun
                    .dpa_hor_mar = DPA.dpa_hor_mar
                    .dpa_res_mar = DPA.dpa_res_mar
                    .dpa_hor_mie = DPA.dpa_hor_mie
                    .dpa_res_mie = DPA.dpa_res_mie
                    .dpa_hor_jue = DPA.dpa_hor_jue
                    .dpa_res_jue = DPA.dpa_res_jue
                    .dpa_hor_vie = DPA.dpa_hor_vie
                    .dpa_res_vie = DPA.dpa_res_vie
                End With
                Data.SubmitChanges()
            Else
                Dim DiasPago2 As New dpa_cls
                With DiasPago2
                    .deu_ide = Format(CLng(DPA.deu_ide), Var.FMT_RUT)
                    .dpa_hor_lun = DPA.dpa_hor_lun
                    .dpa_res_lun = DPA.dpa_res_lun
                    .dpa_hor_mar = DPA.dpa_hor_mar
                    .dpa_res_mar = DPA.dpa_res_mar
                    .dpa_hor_mie = DPA.dpa_hor_mie
                    .dpa_res_mie = DPA.dpa_res_mie
                    .dpa_hor_jue = DPA.dpa_hor_jue
                    .dpa_res_jue = DPA.dpa_res_jue
                    .dpa_hor_vie = DPA.dpa_hor_vie
                    .dpa_res_vie = DPA.dpa_res_vie
                End With
                Data.dpa_cls.InsertOnSubmit(DiasPago2)
                Data.SubmitChanges()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "PRORROGA"

    Public Sub Prorroga_GuardaProrroga(ByVal id_spg As Long, ByVal estado As Int16, Optional ByVal obs_vb As String = Nothing)

        'estado = 0 Temporal
        'estado = 1 Solicitud (Ingresado
        'Estado = 2 Aprovado
        'Estado = 3 Rechazado
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Prorroga
        'Creado por 
        'Fecha Creacion: 
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  11/08/2010         se agrega rut cliente para ingresarlo en cxc
        'Fabian Y.Vargas             17/07/2012         se incorpora parametro opcional con observacion de vb prorroga
        'S. Henriquez C.             28/08/2012         Se agrega procedimiento almacenado asignacontrato
        '**************************************************************************************************************************************************

        Dim Data As New DataClsFactoringDataContext
        Dim Sesion As New ClsSession.ClsSession
        Dim query As String = ""
        Dim sql As New FuncionesGenerales.SqlQuery
        Dim ds As DataSet

        Try

            If estado = 2 Then '''''' Validado

                Dim Temporal_dpg = From DPG In Data.dpg_cls _
                                   Where DPG.id_spg = id_spg And DPG.spg_cls.spg_est = 1 _
                                   Select DPG.id_doc, DPG.doc_cls.id_dsi, DPG.nva_doc_fev_rea, DPG.doc_fev_cal, DPG.spg_cls.spg_tas, DPG.dpg_int_ere, DPG.dpg_com_isi, DPG.dpg_iva_com, DPG.dpg_fac_cam, _
                                   DPG.doc_cls.dsi_cls.dsi_num, DPG.doc_cls.dsi_cls.dsi_flj_num, DPG.doc_cls.opo_cls.ope_cls.opn_cls.id_P_0023, DPG.doc_cls.dsi_cls.id_ope, DPG.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc, DPG.doc_cls.dsi_cls.id_P_0011

                For Each Cursor_dpg In Temporal_dpg

                    query = "select dsi_num, id_ope, COUNT(1) cantidad  from dsi where dsi_num = '" & Cursor_dpg.dsi_num & "' and dsi_flj_num = " & Cursor_dpg.dsi_flj_num & " and id_ope = " & Cursor_dpg.id_ope & " and id_p_0011 = 1 group by dsi_num, id_ope having COUNT(1)>1 "
                    ds = sql.ExecuteDataSet(query)

                    If ds.Tables(0).Rows.Count > 0 Then
                        Exit Sub
                    End If
                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Cambio estado documentos Prorrogado
                    '-----------------------------------------------------------------------------------------------------------------------------
                    Dim dsi_modifica As dsi_cls

                    dsi_modifica = (From dsi In Data.dsi_cls _
                                    Where dsi.id_dsi = Cursor_dpg.id_dsi).First

                    dsi_modifica.id_P_0011 = 5
                    Data.SubmitChanges()

                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Inserta Nuevos Registros por cada Documento Prorrogados
                    '-----------------------------------------------------------------------------------------------------------------------------
                    Dim cursor_dsi = (From DSI In Data.dsi_cls Where DSI.id_dsi = dsi_modifica.id_dsi Select DSI).First

                    Dim dsi_ingreso As New dsi_cls

                    With dsi_ingreso
                        .deu_ide = cursor_dsi.deu_ide
                        .id_ope = cursor_dsi.id_ope
                        .id_P_0011 = 1 'Cursor_dpg.id_P_0011 ''''''' Estado de documento en solicitud de Prorroga
                        .id_P_0040 = cursor_dsi.id_P_0040
                        .id_PL_000047 = cursor_dsi.id_PL_000047
                        .id_P_0061 = cursor_dsi.id_P_0061
                        .id_P_0065 = cursor_dsi.id_P_0065
                        .dsi_num = cursor_dsi.dsi_num
                        .dsi_mto = cursor_dsi.dsi_mto
                        .dsi_mto_fin = cursor_dsi.dsi_mto_fin
                        .dsi_flj = cursor_dsi.dsi_flj
                        .dsi_flj_num = cursor_dsi.dsi_flj_num
                        .dsi_num_ren = cursor_dsi.dsi_num_ren + 1 'nro de renovavion
                        .dsi_cei = cursor_dsi.dsi_cei
                        .dsi_ntf = cursor_dsi.dsi_ntf
                        .dsi_fec_emi = cursor_dsi.dsi_fec_emi
                        .dsi_fev_ori = cursor_dsi.dsi_fev_ori
                        .dsi_fev = cursor_dsi.dsi_fev
                        .dsi_fev_rea = Cursor_dpg.nva_doc_fev_rea ''''''''' Nueva Fecha de Prorroga
                        .dsi_mto_ant = cursor_dsi.dsi_mto_ant
                        .dsi_ctd_dia = cursor_dsi.dsi_ctd_dia
                        .dsi_pre_com = cursor_dsi.dsi_pre_com
                        .dsi_dif_pre = cursor_dsi.dsi_dif_pre
                        .dsi_sal_pen = cursor_dsi.dsi_sal_pen
                        .dsi_sal_pag = cursor_dsi.dsi_sal_pag
                        .dsi_cms = cursor_dsi.dsi_cms
                        .dsi_iva_cms = cursor_dsi.dsi_iva_cms
                        .dsi_gto = cursor_dsi.dsi_gto
                        .dsi_tot_gir = cursor_dsi.dsi_tot_gir
                        .dsi_cbz = cursor_dsi.dsi_cbz
                        .dsi_cbz_son = cursor_dsi.dsi_cbz_son
                        .dsi_inc = cursor_dsi.dsi_inc
                        .dsi_env_bci = cursor_dsi.dsi_env_bci
                        .dsi_sec_ing = cursor_dsi.dsi_sec_ing
                        .id_P_0112 = cursor_dsi.id_P_0112
                        .id_bco = cursor_dsi.id_bco
                        .cta_cte = cursor_dsi.cta_cte
                        .dsi_rsp = cursor_dsi.dsi_rsp
                        .dsi_est_rsp = cursor_dsi.dsi_est_rsp
                        .dsi_fev_cal = Cursor_dpg.doc_fev_cal
                    End With

                    Data.dsi_cls.InsertOnSubmit(dsi_ingreso)
                    Data.SubmitChanges()


                    Dim id_doc As Long

                    query = "insert into doc " & _
                            "select " & dsi_ingreso.id_dsi & " , id_opo, id_fct, id_fdd, id_suc_cbz, id_suc_rcd, id_cco, doc_num_ren, doc_gto, doc_ful_pgo, doc_int_dvg, doc_ful_dvg, doc_fct, doc_fec_cco, doc_sdo_cli, doc_sdo_ddr, " & _
                            "doc_tas_ren,doc_cms_ren,doc_iva_ren,doc_rea_tot,doc_not_cre,doc_pag_ddr,doc_int_cal_ant, doc_dvg, doc_rgo_adi, doc_cob_doi, doc_obs_cob, doc_int_dev, doc_dev_cli, doc_fec_dem, " & _
                            "doc_fec_cas, doc_con_obl, doc_sdo_exc, doc_cod_tmp, doc_int_mor_dvg, doc_INT_dvg_mor, doc_INT_cal_ant_mor, doc_ful_dvg_mor, doc_cod_tmp_old, cal_def_ini_old, doc_cod_tmp_ctr, doc_ful_dvg_mor_ant, doc_cod_tmp_mto_ctr, doc_cod_tmp_fec_ctr " & _
                            "from doc where id_dsi = " & dsi_modifica.id_dsi

                    sql.ExecuteNonQuery(query)

                    query = "select max(id_doc) from doc where id_dsi = " & dsi_ingreso.id_dsi
                    ds = sql.ExecuteDataSet(query)

                    If ds.Tables(0).Rows.Count > 0 Then
                        id_doc = ds.Tables(0).Rows(0).Item(0).ToString()
                    End If

                    query = "insert into sld_med " & _
                            "select " & id_doc & ", fecha, saldo  from sld_med where id_doc = " & Cursor_dpg.id_doc

                    sql.ExecuteNonQuery(query)


                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Crea CXC por tipo 15 Interes de Prorroga
                    '-----------------------------------------------------------------------------------------------------------------------------
                    If Cursor_dpg.dpg_int_ere > 0 Then

                        Dim cxc_ingreso_15 As New cxc_cls

                        cxc_ingreso_15.id_doc = id_doc
                        cxc_ingreso_15.id_P_0041 = 15
                        cxc_ingreso_15.cxc_fec = Date.Now
                        cxc_ingreso_15.cxc_mto = Cursor_dpg.dpg_int_ere
                        cxc_ingreso_15.cxc_des = "INT.PRORROGA DOCTO. NRO." & CStr(Cursor_dpg.dsi_num) & "-" & CStr(Cursor_dpg.dsi_flj_num)
                        cxc_ingreso_15.cxc_sal = Cursor_dpg.dpg_int_ere
                        cxc_ingreso_15.id_P_0057 = 1
                        cxc_ingreso_15.cxc_ful_pgo = Date.Now
                        cxc_ingreso_15.id_P_0023 = Cursor_dpg.id_P_0023
                        cxc_ingreso_15.cxc_fac_cam = Cursor_dpg.dpg_fac_cam
                        cxc_ingreso_15.cli_idc = Cursor_dpg.cli_idc
                        cxc_ingreso_15.id_eje = CodEje

                        Data.cxc_cls.InsertOnSubmit(cxc_ingreso_15)
                        Data.SubmitChanges()

                    End If

                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Crea cxc por tipo 4 Comision de Prorroga
                    '-----------------------------------------------------------------------------------------------------------------------------
                    If (Cursor_dpg.dpg_com_isi + Cursor_dpg.dpg_iva_com) > 0 Then

                        Dim cxc_ingreso_4 As New cxc_cls

                        cxc_ingreso_4.id_doc = id_doc
                        cxc_ingreso_4.id_P_0041 = 4
                        cxc_ingreso_4.cxc_fec = Date.Now
                        cxc_ingreso_4.cxc_mto = Cursor_dpg.dpg_com_isi + Cursor_dpg.dpg_iva_com
                        cxc_ingreso_4.cxc_des = "COMISION PRORROGA DOCTO. NRO." & CStr(Cursor_dpg.dsi_num) & "-" & CStr(Cursor_dpg.dsi_flj_num)
                        cxc_ingreso_4.cxc_sal = Cursor_dpg.dpg_com_isi + Cursor_dpg.dpg_iva_com
                        cxc_ingreso_4.id_P_0057 = 1
                        cxc_ingreso_4.cxc_ful_pgo = Date.Now
                        cxc_ingreso_4.id_P_0023 = Cursor_dpg.id_P_0023
                        cxc_ingreso_4.cxc_fac_cam = Cursor_dpg.dpg_fac_cam
                        cxc_ingreso_4.cli_idc = Cursor_dpg.cli_idc
                        cxc_ingreso_4.id_eje = CodEje

                        Data.cxc_cls.InsertOnSubmit(cxc_ingreso_4)
                        Data.SubmitChanges()

                    End If

                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Cambia Fecha de Vcto Operación
                    '-----------------------------------------------------------------------------------------------------------------------------
                    Dim max_fevrea_dsi = (From dsi_max_fevrea In Data.dsi_cls _
                    Where dsi_max_fevrea.id_ope = Cursor_dpg.id_ope _
                    Select dsi_max_fevrea.dsi_fev_rea).Max

                    Dim ope_modifica As ope_cls

                    ope_modifica = (From ope In Data.ope_cls _
                                   Where ope.id_ope = Cursor_dpg.id_ope).First

                    ope_modifica.ope_fev = max_fevrea_dsi.Value

                    Data.SubmitChanges()

                    '-----------------------------------------------------------------------------------------------------------------------------
                    'Debe asociar clf, doc_con, cxc, cxp, nce al nuevo documento (dsi y doc)
                    '-----------------------------------------------------------------------------------------------------------------------------

                    Try

                        'Crea nueva clf 
                        Dim clf = (From c In Data.clf_cls Where c.id_dsi = cursor_dsi.id_dsi).First
                        Dim clasificacion As New clf_cls

                        clasificacion.id_dsi = dsi_ingreso.id_dsi
                        clasificacion.cal_arr_ast = clf.cal_arr_ast
                        clasificacion.cal_def_ini = clf.cal_def_ini
                        clasificacion.cal_def_ini_old = clf.cal_def_ini_old
                        clasificacion.cal_def_ini_old_mto = clf.cal_def_ini_old_mto
                        clasificacion.cal_obj_eti = clf.cal_obj_eti
                        clasificacion.cal_oto_gam = clf.cal_oto_gam
                        clasificacion.cal_sub_jet = clf.cal_sub_jet

                        Data.clf_cls.InsertOnSubmit(clasificacion)
                        Data.SubmitChanges()

                        'Crea doc_con
                        Dim contrato = (From c In Data.doc_con_cls Where c.id_doc = Cursor_dpg.id_doc).First

                        Dim con As New doc_con_cls

                        con.id_doc = id_doc
                        con.cod_emp = contrato.cod_emp
                        con.ofi_cin = contrato.ofi_cin
                        con.dig_ver_uno = contrato.dig_ver_uno
                        con.pro_duc = contrato.pro_duc
                        con.con_tra = contrato.con_tra
                        con.dig_ver_doc = contrato.dig_ver_doc

                        Data.doc_con_cls.InsertOnSubmit(con)
                        Data.SubmitChanges()

                        'Actualiza gestiones del documento
                        Dim ges = From c In Data.gsn_cls Where c.id_doc = Cursor_dpg.id_doc
                        For Each ge In ges
                            ge.id_doc = id_doc
                        Next

                        'Actualiza cxc por el nuevo id_doc
                        Dim cxc = From c In Data.cxc_cls Where c.id_doc = Cursor_dpg.id_doc

                        For Each c In cxc
                            c.id_doc = id_doc
                        Next

                        'Actualiza cxp por el nuevo id_doc
                        Dim cxp = From c In Data.cxp_cls Where c.id_doc = Cursor_dpg.id_doc

                        For Each c In cxp
                            c.id_doc = id_doc
                        Next

                        'Actualiza nce por el nuevo id_doc
                        Dim nce = From c In Data.nce_cls Where c.id_doc = Cursor_dpg.id_doc

                        For Each c In nce
                            c.id_doc = id_doc
                        Next

                        'Actualiza pagos
                        Dim ing = From c In Data.ing_sec_cls Where c.id_doc = Cursor_dpg.id_doc

                        For Each c In ing
                            c.id_doc = id_doc
                        Next

                        'Actualiza egresos
                        Dim egr = From c In Data.egr_sec_cls Where c.id_doc = Cursor_dpg.id_doc

                        For Each c In egr
                            c.id_doc = id_doc
                        Next

                        '23-11-2015 JLAGOS -Rechazamos si existe otra solicitud para algun documento de esta prorroga
                        Dim spg = From s In Data.dpg_cls Where s.id_doc = Cursor_dpg.id_doc And s.spg_cls.spg_est = 1 And s.id_spg <> id_spg

                        For Each s In spg
                            s.spg_cls.spg_est = 3
                        Next

                        Data.SubmitChanges()

                    Catch ex As Exception

                    End Try

                Next

            End If

            If estado = 4 Then
                query = "Exec sp_anula_prorroga " & id_spg
                sql.ExecuteNonQuery(query)
            End If

            '-----------------------------------------------------------------------------------------------------------------------------
            'Modifica Estado de Prorroga
            '-----------------------------------------------------------------------------------------------------------------------------
            Dim spg_modifica As spg_cls

            spg_modifica = (From SPG In Data.spg_cls Where SPG.id_spg = id_spg).First

            spg_modifica.spg_fec_apb = Date.Now
            spg_modifica.id_eje_apb = Sesion.CodEje
            spg_modifica.spg_est = estado

            If Not IsNothing(obs_vb) Then
                spg_modifica.spg_obs_vb = obs_vb
            End If

            Data.SubmitChanges()

            

        Catch ex As Exception

        End Try

    End Sub

    Public Function Prorroga_GuardaSolicitud(ByVal spg_ingreso As spg_cls) As Boolean

        Try

            Prorroga_GuardaSolicitud = False

            Dim Data As New DataClsFactoringDataContext

            '------------------------------------------------------------------------------------------------------------------------------
            'Guarda Solicitud de Prorroga
            '------------------------------------------------------------------------------------------------------------------------------
            Data.spg_cls.InsertOnSubmit(spg_ingreso)
            Data.SubmitChanges()

            Prorroga_GuardaSolicitud = True

        Catch ex As Exception

        End Try

    End Function

    Public Function Prorroga_GuardaDetalle(ByVal dpg_ingreso As dpg_cls) As Boolean

        Try

            Prorroga_GuardaDetalle = False

            Dim Data As New DataClsFactoringDataContext

            '------------------------------------------------------------------------------------------------------------------------------
            'Guarda Detalle de Prorroga
            '------------------------------------------------------------------------------------------------------------------------------
            Data.dpg_cls.InsertOnSubmit(dpg_ingreso)
            Data.SubmitChanges()

            Prorroga_GuardaDetalle = True

        Catch ex As Exception

        End Try

    End Function

    Public Function Prorroga_EliminaSolicitudTemporal(ByVal RutCliente As String) As Boolean

        Try
            Dim Data As New DataClsFactoringDataContext

            Dim temporal_dpg = From A In Data.dpg_cls Where A.spg_cls.cli_idc = RutCliente And A.spg_cls.spg_est = 0

            Data.dpg_cls.DeleteAllOnSubmit(temporal_dpg)
            Data.SubmitChanges()

            Dim temporal_spg = From B In Data.spg_cls Where B.cli_idc = RutCliente And B.spg_est = 0

            Data.spg_cls.DeleteAllOnSubmit(temporal_spg)
            Data.SubmitChanges()

        Catch ex As Exception

        End Try

    End Function

    Public Function Prorroga_ModificaEstadoSolicitud(ByVal RutClienteSolicitud As String, ByVal NroSolictud As Long, ByVal EstadoSolicitud As Int16) As Boolean

        Try

            Prorroga_ModificaEstadoSolicitud = False

            '--------------------------------------------------------------------------------------------------------------------------

            'Grabar Modificacion de Solicitud a Estado (Según valor pasado por parametro)

            '--------------------------------------------------------------------------------------------------------------------------

            Dim Data As New DataClsFactoringDataContext

            Dim spg_modifica As spg_cls = (From SPG In Data.spg_cls _
                                            Where SPG.id_spg = NroSolictud _
                                           And SPG.cli_idc = RutClienteSolicitud _
                                            Select SPG).First()

            spg_modifica.spg_est = EstadoSolicitud

            Data.SubmitChanges()

            Prorroga_ModificaEstadoSolicitud = True

        Catch ex As Exception

        End Try

    End Function

    Public Function Prorroga_ValidaDocumentos(ByVal id_doc As String) As Boolean

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim temporal_dpg = From A In Data.dpg_cls Where A.id_doc = id_doc And A.spg_cls.spg_est = 1

            If temporal_dpg.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "NOMINAS"

    Public Function NominaIngreso_Inserta(ByVal Nomina As nma_cls, ByVal Coll_Dpo As Collection) As Integer

        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva nomina
        'Creado por= Jorge Lagos
        'Fecha Creacion: 27/02/2009
        'Quien Modifica   Fecha         Descripcion
        'A Saldivar       24/11/2010    Se modifica para que devuelva el ultimo id_nma 
        'J. Lagos            02/12/2013    Se modifica proceso, ya que hacia el cierre por cada vuelta

        '*********************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext
            Dim coll_clientes As New Collection
            Dim FG As New FuncionesGenerales.FComunes

            data.nma_cls.InsertOnSubmit(Nomina)
            data.SubmitChanges()

            For I = 1 To Coll_Dpo.Count

                Dim Id As Integer = CInt(Coll_Dpo.Item(I))

                Dim Dpo = From D In data.ing_sec_cls Where D.id_ing = Id Select D

                For Each D In Dpo

                    D.dpo_cls.id_nma = Nomina.id_nma
                    D.dpo_cls.id_P_0052 = 4
                    D.ing_vld_rcz = "C" 'Canje

                    If Not FG.BuscaCollection(coll_clientes, D.cli_idc) Then
                        coll_clientes.Add(D.cli_idc)
                    End If

                Next

                data.SubmitChanges()

            Next

            '------------------------------------------------------------------------------------
            'jlagos 25-07-2012 -se agrega la rebaja automatica de saldos
            Dim SQL As New FuncionesGenerales.SqlQuery

            For I = 1 To coll_clientes.Count

                sql.ExecuteNonQuery("Exec sp_op_cierre_cliente '" & coll_clientes(I) & "', '" & coll_clientes(I) & "', '" & DateTime.Now.ToString("yyyMMdd") & "'")

                'data.sp_op_cierre_cliente(coll_clientes(I), _
                '                          coll_clientes(I), _
                '                          DateTime.Now())

            Next
            '------------------------------------------------------------------------------------

            Return Nomina.id_nma

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function NominaEgreso_Inserta(ByVal Nomina As nma_cls, ByVal DoctoPago As dpo_cls, ByVal Coll_Egresos As Collection)

        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva nomina
        'Creado por= Jorge Lagos
        'Fecha Creacion: 27/02/2009
        'Quien Modifica   Fecha     Descripcion
        '*********************************************************************************************************************************

        Try

            Dim id_egr As Integer
            Dim data As New DataClsFactoringDataContext
            Dim RutCliente As String

            data.nma_cls.InsertOnSubmit(Nomina)
            data.SubmitChanges()

            DoctoPago.id_nma = Nomina.id_nma

            data.dpo_cls.InsertOnSubmit(DoctoPago)
            data.SubmitChanges()

            For I = 1 To Coll_Egresos.Count

                'id_egr = Coll_Egresos.Item(I).id_egr
                id_egr = CInt(Coll_Egresos.Item(I).ToString)

                Dim Egreso = From E In data.egr_sec_cls Where E.id_egr = id_egr

                For Each E In Egreso

                    Select Case E.id_P_0055

                        Case 1 'CxP
                            Dim Cxp = From C In data.cxp_cls Where C.id_cxp = E.id_cxp
                            If Cxp.Count <= 0 Then Exit Function

                        Case 2 'Documentos no cedidos NCE (siempre es en pesos)
                            Dim Nce = From n In data.nce_cls Where n.id_nce = E.id_nce
                            If Nce.Count <= 0 Then Exit Function

                        Case 3 'Excedentes
                            Dim Exc = From O In data.doc_cls Where O.id_doc = E.id_doc
                            If Exc.Count <= 0 Then Exit Function

                        Case 4 'anticipo
                            'Nada

                    End Select

                    E.id_dpo = DoctoPago.id_dpo
                    E.egr_vld_rcz = "L"
                    E.egr_ent = "S"
                    E.egr_abo_man_aut = "M"
                    E.egr_pro = "S"

                    data.SubmitChanges()

                    RutCliente = E.egr_cls.cli_idc

                Next

                '-------------------------------------------------------------------------------
                'jlagos 25-07-2012 -se agrega la rebaja automatica de saldos
                'data.sp_op_cierre_cliente(RutCliente, RutCliente, DateTime.Now())
                Dim sql As New FuncionesGenerales.SqlQuery

                sql.ExecuteNonQuery("Exec sp_op_cierre_cliente '" & RutCliente & "', '" & RutCliente & "', '" & DateTime.Now.ToString("yyyMMdd") & "'")
                '-------------------------------------------------------------------------------

            Next

            Return DoctoPago.id_nma

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    
#End Region

#Region "ALERTAS"

    Public Function AlertasParametro_Inserta(ByVal Parametros As pta_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Guarda parametros de alertas por un ejecutivo
        'Creado por: Jorge Lagos C.
        'Fecha Creacion: 24/03/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim ts = New TransactionScope

            Using ts

                Data.pta_cls.InsertOnSubmit(Parametros)
                Data.SubmitChanges()

                ts.Complete()

            End Using

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AlertasParametro_Modifica(ByVal Parametros As pta_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Modifica un parametros de alertas por un ejecutivo
        'Creado por: Jorge Lagos C.
        'Fecha Creacion: 24/03/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Pta = From P In Data.pta_cls Where P.id_eje = Parametros.id_eje

            For Each p In Pta
                p.pta_dia_ven = Parametros.pta_dia_ven
                p.pta_dia_mor = Parametros.pta_dia_mor
                p.pta_dia_lin = Parametros.pta_dia_lin
            Next

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function


#End Region

#Region "Sistema"

    Public Function guarda_datos_sistema(ByVal dia_eli_cli As Integer, ByVal dia_int_dev As Int32, _
                                         ByVal valmar As Int32, ByVal dia_val_fec_gsn As Int32, _
                                         ByVal iva As Decimal, ByVal dia_vto As Integer, _
                                         ByVal dia_prorroga As Integer, ByVal gmf As Integer, _
                                         ByVal vto_ldc As Integer, ByVal vld_lin As Char) As Boolean
        '-----------------------------------------------------------------------------------------------
        'Descripcion: Guardar datos de sistema
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     04-06-2012          -se agrega dias antes de vencimiento de prorroga
        'SHenriquez                 16-06-2012          -se agrega dias vencimiento linea de credito
        'JLagos                     04-06-2012          -se agrega si valida linea para otorgar si tiene sobregiro
        '-----------------------------------------------------------------------------------------------

        Try



            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim Sistema = (From N In Data.sis_cls Select N).First

            With Sistema

                .sis_iva = iva
                .sis_val_fec_gsn = dia_val_fec_gsn
                .sis_val_mar = valmar
                .sis_dia_eli_cli = dia_eli_cli
                .sis_dia_dev = dia_int_dev
                .sis_dia_vto = dia_vto
                .sis_dia_pro = dia_prorroga
                .sis_can_gmf = gmf
                .sis_vto_ldc = vto_ldc
                .sis_vld_lin = vld_lin
                '.sis_dia_pgo = dia_dev_pag
                '.sis_mes_fic = mes_fic_jur
            End With

            Data.SubmitChanges()

        Catch ex As Exception
            Dim data1 As New DataClsFactoringDataContext
            Dim sist As New sis_cls

            With sist
                .sis_iva = iva
                .sis_val_fec_gsn = dia_val_fec_gsn
                .sis_val_mar = valmar
                .sis_dia_eli_cli = dia_eli_cli
                .sis_dia_dev = dia_int_dev
                .sis_dia_vto = dia_vto
                .sis_dia_pro = dia_prorroga
                .sis_can_gmf = gmf
                .sis_vto_ldc = vto_ldc
                .sis_vld_lin = vld_lin
                '.sis_dia_pgo = dia_dev_pag
                '.sis_mes_fic = mes_fic_jur
            End With

            data1.sis_cls.InsertOnSubmit(sist)
            data1.SubmitChanges()


        End Try

    End Function

  

#End Region

#Region "Notario"

    Public Function GuardaNotario(ByVal cod As Integer, ByVal suc As Integer, ByVal dir As String, ByVal dir_emp As String, ByVal nombre As String, ByVal telefono As String, ByVal Def As String) As Boolean
        'Descripcion: Guardar Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/06/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try


            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim Notario = New ntr_cls()

            If Def = "S" Then
                Dim Cnc_Cli = From C In Data.ntr_cls Where C.id_suc = suc

                For Each C In Cnc_Cli
                    C.ntr_def = "N"
                Next
                Data.SubmitChanges()

            End If

            If cod <> 0 Then

                Dim Cnc = From c In Data.ntr_cls Where c.id_ntr = cod And c.id_suc = suc


                For Each C In Cnc



                    C.ntr_nom = nombre
                    C.ntr_dml = dir
                    C.ntr_dml_emp = dir_emp
                    C.ntr_tel = telefono
                    C.ntr_def = Def


                Next
                Data.SubmitChanges()

                Return True
            Else
                Notario.id_suc = suc

                Notario.ntr_nom = nombre
                Notario.ntr_dml = dir
                Notario.ntr_dml_emp = dir_emp
                Notario.ntr_tel = telefono
                Notario.ntr_def = Def

                Data.ntr_cls.InsertOnSubmit(Notario)
                Data.SubmitChanges()

                Return True
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub eliminanotario(ByVal cod As Int32, ByVal suc As String, ByVal def As String)
        '*********************************************************************************************************************************
        'Descripcion: Elimina notario por su correlativo
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/06/2008
        'Quien Modifica              Fecha              Descripcion

        '*********************************************************************************************************************************


        Try
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()


            If def = "S" Then


                'Contacto para el Cliente
                Dim a As Integer = (From ntr1 In Data.ntr_cls Select ntr1.id_ntr).Min
                Dim Cnc_Cli = From C In Data.ntr_cls Where C.id_ntr = cod
                For Each C In Cnc_Cli
                    C.ntr_def = "N"
                Next
                Data.SubmitChanges()
            End If

            Dim notr As ntr_cls = (From ntr In Data.ntr_cls Where ntr.id_ntr = cod And ntr.id_suc = suc).First


            Data.ntr_cls.DeleteOnSubmit(notr)
            Data.SubmitChanges()



        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Codigo Cobranza"

    Public Function guarda_datos_cobranza(ByVal cconum As String, ByVal cco_acc As String, ByVal cco_des As String, _
                                          ByVal cco_ges_son As String, ByVal cco_not_ges As String, ByVal cco_nvo_acc As String, _
                                          ByVal cco_pla_vto_ges As String, ByVal cco_pri As Integer, ByVal cco_pzo As Integer) As String

        '**********************************************************************************************************************************
        'Descripcion: Guardar Datos de Cobranza
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 23/06/2008
        'Quien Modifica              Fecha              Descripcion
        '**********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim ret As String

            If cco_pri > 0 Then
                Dim retorno = From N In Data.cco_cls Where N.cco_pri = cco_pri

                If retorno.Count > 0 Then
                    ret = "Existe Prioridad"
                    Return ret
                    Exit Function
                End If
            End If

            Dim retorno_num = From N In Data.cco_cls Where N.cco_num = cconum

            If retorno_num.Count > 0 Then
                ret = "Existe Numero"
                Return ret
                Exit Function
            End If


            Dim Cobranza As New cco_cls

            Cobranza.cco_num = cconum
            Cobranza.cco_des = cco_des
            Cobranza.cco_pzo = cco_pzo
            Cobranza.cco_pri = cco_pri

            If cco_acc = "" Then
                Cobranza.cco_acc = Nothing
            Else
                Cobranza.cco_acc = cco_acc
            End If

            If cco_ges_son = "" Then
                Cobranza.cco_ges_son = Nothing
            Else
                Cobranza.cco_ges_son = cco_ges_son
            End If

            If cco_nvo_acc = "" Then
                Cobranza.cco_nvo_acc = Nothing
            Else
                Cobranza.cco_nvo_acc = cco_nvo_acc
            End If

            If cco_not_ges = "" Then
                Cobranza.cco_not_ges = Nothing
            Else
                Cobranza.cco_not_ges = cco_not_ges
            End If

            If cco_pla_vto_ges = "" Then
                Cobranza.cco_pla_vto_ges = Nothing
            Else
                Cobranza.cco_pla_vto_ges = cco_pla_vto_ges
            End If




            Data.cco_cls.InsertOnSubmit(Cobranza)
            Data.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict)
            ret = "Registro Ingresado"
            Return ret


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Cambia_Prioridad_Cobranza(ByVal cconum As String, ByVal cco_acc As Integer, ByVal cco_des As String, ByVal cco_ges_son As String, ByVal cco_not_ges As String, ByVal cco_nvo_acc As String, ByVal cco_pla_vto_ges As String, ByVal cco_pri As Integer) As String
        'Descripcion:Modifica la prioridad de un codigo de cobranza
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 23/06/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim Cobranza = New cco_cls()


            Dim retorno = From N In Data.cco_cls Where N.cco_pri = cco_pri


            For Each n In retorno

                Cobranza.cco_acc = cco_acc
                Cobranza.cco_des = cco_des
                Cobranza.cco_ges_son = cco_ges_son
                Cobranza.cco_not_ges = cco_not_ges
                Cobranza.cco_nvo_acc = cco_nvo_acc
                Cobranza.cco_pla_vto_ges = cco_pla_vto_ges
                Cobranza.cco_pri = cco_pri
                Cobranza.cco_num = cconum
                Data.SubmitChanges()

            Next

            If retorno.Count Then
                Return True
            Else
                Return False
            End If




        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub elimina_cod_cobranza(ByVal cod As String)

        '*********************************************************************************************************************************
        'Descripcion: Elimina Codigo Cobranza por su correlativo
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 02/07/2008
        'Quien Modifica              Fecha              Descripcion

        '*********************************************************************************************************************************

        Try

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim Cob As cco_cls = (From cco In Data.cco_cls Where cco.cco_num = cod).First

            Data.cco_cls.DeleteOnSubmit(Cob)
            Data.SubmitChanges()



        Catch ex As Exception

        End Try

    End Sub

    Public Function Actualiza_datos_cobranza(ByVal cconum As String, ByVal cco_acc As String, ByVal cco_des As String, _
                                      ByVal cco_ges_son As String, ByVal cco_not_ges As String, ByVal cco_nvo_acc As String, _
                                      ByVal cco_pla_vto_ges As String, ByVal cco_pri As Integer, ByVal cco_pzo As Integer) As String

        '**********************************************************************************************************************************
        'Descripcion: Guardar Datos de Cobranza
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 23/06/2008
        'Quien Modifica              Fecha              Descripcion
        '**********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim ret As String




            Dim Cobranza As New cco_cls

            Cobranza.cco_num = cconum
            Cobranza.cco_des = cco_des
            Cobranza.cco_pzo = cco_pzo
            Cobranza.cco_pri = cco_pri

            If cco_acc = "" Then
                Cobranza.cco_acc = Nothing
            Else
                Cobranza.cco_acc = cco_acc
            End If

            If cco_ges_son = "" Then
                Cobranza.cco_ges_son = Nothing
            Else
                Cobranza.cco_ges_son = cco_ges_son
            End If

            If cco_nvo_acc = "" Then
                Cobranza.cco_nvo_acc = Nothing
            Else
                Cobranza.cco_nvo_acc = cco_nvo_acc
            End If

            If cco_not_ges = "" Then
                Cobranza.cco_not_ges = Nothing
            Else
                Cobranza.cco_not_ges = cco_not_ges
            End If

            If cco_pla_vto_ges = "" Then
                Cobranza.cco_pla_vto_ges = Nothing
            Else
                Cobranza.cco_pla_vto_ges = cco_pla_vto_ges
            End If

            Dim retorno_num = From N In Data.cco_cls Where N.cco_num = cconum

            For Each n In retorno_num

                n.cco_num = cconum
                n.cco_des = cco_des
                n.cco_pzo = cco_pzo
                n.cco_pri = cco_pri

                If cco_acc = "" Then
                    n.cco_acc = Nothing
                Else
                    n.cco_acc = cco_acc
                End If

                If cco_ges_son = "" Then
                    n.cco_ges_son = Nothing
                Else
                    n.cco_ges_son = cco_ges_son
                End If

                If cco_nvo_acc = "" Then
                    n.cco_nvo_acc = Nothing
                Else
                    n.cco_nvo_acc = cco_nvo_acc
                End If

                If cco_not_ges = "" Then
                    n.cco_not_ges = Nothing
                Else
                    n.cco_not_ges = cco_not_ges
                End If

                If cco_pla_vto_ges = "" Then
                    n.cco_pla_vto_ges = Nothing
                Else
                    n.cco_pla_vto_ges = cco_pla_vto_ges
                End If
            Next
            Data.SubmitChanges()

            ret = "Registro Actualizar"
            Return ret


        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#Region "Clasificacion"

    Public Function ClacificacionClienteInsert(ByVal Id As P_0118_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Clacificacion Cliente
        'Creado por= Victor Alvarez R.
        'Fecha Creacion: 07/02/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.P_0118_cls.InsertOnSubmit(Id)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function ClasificacionClienteUpdate(ByVal Id As Integer, _
                                                 ByVal des As String, _
                                                 ByVal est As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Clasificacion de clinte
        'Creado por= Victor Alvarez
        'Fecha Creacion: 07/02/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim Modi As P_0118_cls = (From p In data.P_0118_cls Where p.id_P_0118 = Id).First
            With Modi
                .pnu_des = des
                .pnu_est = est
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Documentos y Otras Condiciones para Comercial"

    Public Function DocComInserta(ByVal doc As doc_com_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documentos comercial
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 18-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.doc_com_cls.InsertOnSubmit(doc)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DocComActualiza(ByVal doc As doc_com_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza un documento comercial
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 18-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim RE As doc_com_cls = (From R In data.doc_com_cls Where R.id_doc_com = doc.id_doc_com).First

            RE.des_doc_com = doc.des_doc_com
            RE.est_doc_com = doc.est_doc_com
            RE.id_tipo = doc.id_tipo

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DocComPorDoctoInserta(ByVal dxd As dxd_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documento por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext
        Dim Req As dxd_cls

        Try

            Req = (From R In data.dxd_cls Where R.id_doc_com = dxd.id_doc_com And _
                                                R.id_p_031 = dxd.id_p_031).First

        Catch ex As Exception

            Try

                If IsNothing(Req) Then
                    data.dxd_cls.InsertOnSubmit(dxd)
                End If

                data.SubmitChanges()

                Return True

            Catch ex1 As Exception
                Return False
            End Try

        End Try


    End Function

    Public Function DocComsPorOperacionAoR(ByVal dxn As dxn_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el estado de documentos por negociacion (aprueba o rechaza)
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.dxn_cls.InsertOnSubmit(dxn)
            data.SubmitChanges()

            Return True

        Catch ex1 As Exception
            Return False
        End Try

    End Function

    Public Function DocComsPorOperacionAoR_LImpia(ByVal id_opn As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el estado de documentos por negociacion (aprueba o rechaza)
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim documentacion = From d In data.dxn_cls Where d.id_opn = id_opn

            For Each d In documentacion
                d.id_eje = Nothing
                d.dxn_fec_apb = Nothing
                d.est_dxd = "P"
            Next

            data.SubmitChanges()

            Return True

        Catch ex1 As Exception
            Return False
        End Try

    End Function

    Public Function DocComsPorOperacionAoR_Actualiza(ByVal dxn As dxn_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el estado de documentos por negociacion (aprueba o rechaza)
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim documentacion = From d In data.dxn_cls Where d.id_dxn = dxn.id_dxn And d.id_opn = dxn.id_opn

            For Each d In documentacion
                d.id_eje = dxn.id_eje
                d.dxn_fec_apb = dxn.dxn_fec_apb
                d.est_dxd = dxn.est_dxd
            Next

            data.SubmitChanges()

            Return True

        Catch ex1 As Exception
            Return False
        End Try

    End Function


    Public Function DocComPorDoctoElimina(ByVal id As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documento por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.dxd_cls Where d.id_p_031 = id

            data.dxd_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try


    End Function

    Public Function DocComPorDoctoElimina(ByVal id As Integer, ByVal id2 As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Se Eliminan Condicion pr documento
        'Creado por Sebastian Henriquez C.
        'Fecha Creacion: 10-10-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.dxd_cls Where d.id_p_031 = id And d.id_doc_com = id2

            'data.dxd_cls.DeleteOnSubmit(rsl)
            data.dxd_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try

    End Function

    Public Function DocComPorDoctoNegElimina(ByVal id As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documento por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.dxn_cls Where d.id_opn = id

            data.dxn_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try


    End Function

    '--------------------------------------------------------------------------------------------------------------------

    Public Function ConComInserta(ByVal con As con_com_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documentos comercial
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 18-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.con_com_cls.InsertOnSubmit(con)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ConComActualiza(ByVal con As con_com_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza un documento comercial
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 18-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim RE As con_com_cls = (From R In data.con_com_cls Where R.id_con_com = con.id_con_com).First

            RE.des_con_com = con.des_con_com
            RE.est_con_com = con.est_con_com

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ConComPorDoctoInserta(ByVal cxd As cxd_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta condiciones por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext
        Dim Req As cxd_cls

        Try

            Req = (From R In data.cxd_cls Where R.id_con_com = cxd.id_con_com And _
                                                R.id_p_0031 = cxd.id_p_0031).First

        Catch ex As Exception

            Try

                If IsNothing(Req) Then
                    data.cxd_cls.InsertOnSubmit(cxd)
                End If

                data.SubmitChanges()

                Return True

            Catch ex1 As Exception
                Return False
            End Try

        End Try


    End Function

    Public Function ConComsPorOperacionAoR(ByVal cxn As cxn_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el estado de documentos por negociacion (aprueba o rechaza)
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.cxn_cls.InsertOnSubmit(cxn)
            data.SubmitChanges()

            Return True

        Catch ex1 As Exception
            Return False
        End Try

    End Function


    Public Function ConComPorDoctoElimina(ByVal id As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documento por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.cxd_cls Where d.id_p_0031 = id

            data.cxd_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try


    End Function

    Public Function ConComPorDoctoElimina(ByVal id As Integer, ByVal id2 As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta una condicion por tipo de documento
        'Creado por Sebastian Henriquez.
        'Fecha Creacion: 10-10-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.cxd_cls Where d.id_p_0031 = id And d.id_con_com = id2

            data.cxd_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try


    End Function

    Public Function ConComPorDoctoNegElimina(ByVal id As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta un documento por tipo de documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try

            Dim rsl = From d In data.cxn_cls Where d.id_opn = id

            data.cxn_cls.DeleteAllOnSubmit(rsl)
            data.SubmitChanges()


        Catch ex As Exception

        End Try


    End Function

#End Region

    Public Function CORASUInserta(ByVal id As P_0313_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Guarda CORASU
        'Creado por= Sebastian Henriquez.
        'Fecha Creacion: 27/06/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext

            data.P_0313_cls.InsertOnSubmit(id)

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CORASUModifica(ByVal id As Integer, ByVal des As String, ByVal est As String, ByVal atr As String) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica CORASU
        'Creado por= Sebastian Henriquez.
        'Fecha Creacion: 27/06/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext

            Dim modi As P_0313_cls = (From p In data.P_0313_cls Where p.id_P_0313 = id).First

            With modi
                .pnu_des = des
                .pnu_est = est
                .pnu_atr_001 = atr
            End With

            data.SubmitChanges()

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function CalificacionXClienteInserta(ByVal LCF As clf_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta calificacion para un Cliente
        'Creado por= Sebastian Henriquez.
        'Fecha Creacion: 04/07/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            data.clf_cls.InsertOnSubmit(LCF)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function CalificaionXClienteUpdate(ByVal LCF As clf_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta calificacion para un Cliente
        'Creado por= Sebastian Henriquez.
        'Fecha Creacion: 04/07/2012
        'Quien Modifica              Fecha              Descripcion
        'S Henriquez                27/09/2012          Se quita calificacion de otorgamiento y se cambia id
        '*********************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim cla As clf_cls = (From a In data.clf_cls Where a.id_dsi = LCF.id_dsi).First

            cla.cal_sub_jet = LCF.cal_sub_jet
            cla.cal_arr_ast = LCF.cal_arr_ast

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
