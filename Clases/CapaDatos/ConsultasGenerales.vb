Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports CapaDatos

Public Class ConsultasGenerales

#Region "Declaracion de variables"

    Dim Var As New FuncionesGenerales.Variables
    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim sql As New FuncionesGenerales.SqlQuery

#End Region

#Region "Agenda"
    Public Function actividad_diaria_carga(ByVal fecha As Date, ByVal cod_eje As Integer) As Collection

        Dim data As New CapaDatos.DataClsFactoringDataContext
        Dim col As New Collection
        Dim agd = From a In data.agd_cls Where a.agd_fec >= CDate(CDate(fecha).ToShortDateString).AddHours(0).AddMinutes(0) And _
                                               a.agd_fec <= CDate(CDate(fecha).ToShortDateString).AddHours(23).AddMinutes(59) _
                                            And a.id_eje = cod_eje Select a.agd_des, _
                                                                          a.agd_hor_dde, _
                                                                          a.agd_hor_hta, _
                                                                          a.P_0115_cls.pnu_des, _
                                                                          a.agd_fec, _
                                                                          a.id_p_0115, _
                                                                          a.id_agd

        For Each p In agd

            col.Add(p)

        Next

        Return col

    End Function

    Public Function estado_actividad_carga() As Collection

        Dim data As New CapaDatos.DataClsFactoringDataContext
        Dim col As New Collection
        Dim est = From e In data.P_0115_cls Select e.id_P_0115, e.pnu_des

        For Each p In est
            col.Add(p)

        Next

        Return col

    End Function
#End Region

#Region "Paridad"
    Public Function plaza_retorna(Optional ByVal Pag As Integer = 9999) As Collection

        'Quien modifica     Fecha       Descripcion
        'A. Saldivar        09/02/2011  Se agrega paginacion

        Try


            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim col As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim pza = (From d In data.pds_cls Select d.id_PL_000047, _
                     d.PL_000047_cls.pal_des, _
                     d.pds_ret).Skip(sesion.NroPaginacion).Take(Pag)

            For Each p In pza
                col.Add(p)
            Next

            Return col

        Catch ex As Exception

        End Try
    End Function
    Public Function Feriados_Devuelve(ByVal mes As Integer, ByVal año As Integer) As Collection
        'Descripcion: Devuelve Feriados 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 3/09/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim fer_Obj As fer_cls


            Dim Paridad = From par In Data.fer_cls Where par.fer_fec.Month = mes And par.fer_fec.Year = año Select par.fer_fec.ToShortDateString


            If Paridad.Count <> 0 Then

                For Each P In Paridad

                    fer_Obj = New fer_cls

                    With fer_Obj
                        .fer_fec = P

                    End With

                    Coll.Add(P)

                Next

                Return Coll

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function RETORNA_VALOR_MONEDA(ByVal MONTO As Double, ByVal TIPO_MONEDA_ORIGEN As Integer, ByVal TIPO_MONEDA_DESTINO As Integer, _
                                      ByVal FECHA_PARIDAD As String, Optional ByVal TipoDocto As Integer = 0, Optional ByVal PagoSN As String = "N", _
                                      Optional ByVal AplicaiconesSN As String = "N") As Double


        Try

            Dim SQL As String
            Dim ValorOrigen, ValorDestino As Double
            Dim Valor_Retorno As Double


            'Valida Moneda Origen = a Moneda Destino
            If TIPO_MONEDA_ORIGEN = TIPO_MONEDA_DESTINO Then
                Valor_Retorno = MONTO
                Return Valor_Retorno
                Exit Function
            End If



            If TIPO_MONEDA_ORIGEN <> 1 Then
                Dim par As New par_cls
                par = ParidadDevuelve(TIPO_MONEDA_ORIGEN, FECHA_PARIDAD)



                If IsNothing(par) = False Then
                    ValorOrigen = par.par_val
                Else
                    ValorOrigen = 0
                End If

                ValorOrigen = ValorOrigen * MONTO
            Else
                ValorOrigen = MONTO
            End If


            If TIPO_MONEDA_DESTINO <> 1 Then
                Dim par As New par_cls
                par = ParidadDevuelve(TIPO_MONEDA_DESTINO, FECHA_PARIDAD)


                If IsNothing(par) = False Then

                    ValorDestino = par.par_val


                    If ValorDestino > 0 Then
                        Valor_Retorno = (ValorOrigen / ValorDestino)
                    Else
                        ValorDestino = 0
                        Valor_Retorno = 0
                    End If
                Else
                    ValorDestino = 0
                    Valor_Retorno = 0
                End If
            Else
                Valor_Retorno = ValorOrigen
            End If

            Return Valor_Retorno

        Catch ex As Exception

        End Try

    End Function

    Public Function RETORNA_VALOR_MONEDA_COBRANZA(ByVal TIPO_MONEDA_ORIGEN As Integer, ByVal TIPO_MONEDA_DESTINO As Integer, _
                                                  ByVal FECHA_PARIDAD As String, ByVal TipoDocto As Integer, _
                                                  ByVal TIPO_MONEDA_PAGO As Integer, ByVal VALOR_CALCULAR As Double) As Double


        Try

            Dim ValorOrigen, ValorDestino As Double
            Dim Valor_Retorno As Double
            Dim TipoDoctoValorCobranza As Char


            'Valida Moneda Origen = a Moneda Destino
            If TIPO_MONEDA_ORIGEN = TIPO_MONEDA_DESTINO Then
                Valor_Retorno = VALOR_CALCULAR
                Return Valor_Retorno
                Exit Function
            End If


            Try

                TipoDoctoValorCobranza = Parametros_Detalle_Devuelve(TablaParametro.TipoDocumento, TipoDocto).Item(1).pnu_atr_012

            Catch ex As Exception
                TipoDoctoValorCobranza = "N"
            End Try

            'Buscamos la los factores de cambio de la fecha solicitada
            Dim par As New par_cls
            par = ParidadDevuelve(TIPO_MONEDA_ORIGEN, FECHA_PARIDAD)
            '-----------------------------------------------------------

            If TIPO_MONEDA_ORIGEN = 3 Then


                If (TipoDoctoValorCobranza = "S" Or TIPO_MONEDA_PAGO <> 3) Then '--Distinto de Dolar

                    If Not IsNothing(par) Then
                        ValorOrigen = par.par_val_cob
                    Else
                        ValorOrigen = 0
                    End If

                    ValorOrigen = ValorOrigen * VALOR_CALCULAR

                Else

                    If Not IsNothing(par) Then
                        ValorOrigen = par.par_val
                    Else
                        ValorOrigen = 0
                    End If

                    ValorOrigen = ValorOrigen * VALOR_CALCULAR
                End If

            Else

                If Not IsNothing(par) Then
                    ValorOrigen = par.par_val
                Else
                    ValorOrigen = 0
                End If

                ValorOrigen = ValorOrigen * VALOR_CALCULAR

            End If



            'si moneda es distinta de Peso
            If TIPO_MONEDA_DESTINO <> 1 Then

                If Not IsNothing(par) Then

                    ValorDestino = par.par_val


                    If ValorDestino > 0 Then
                        Valor_Retorno = (ValorOrigen / ValorDestino)
                    Else
                        ValorDestino = 0
                        Valor_Retorno = 0
                    End If
                Else
                    ValorDestino = 0
                    Valor_Retorno = 0
                End If
            Else
                Valor_Retorno = ValorOrigen
            End If

            Return Valor_Retorno

        Catch ex As Exception

        End Try

    End Function

    Public Function ParidadPorRangoFechasDevuelve(ByVal fecha As DateTime, ByVal tipo As Int16, ByVal fecha_hasta As DateTime) As Collection
        'Descripcion: Devuelve Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     11/11/2008         se cambia nombre de function de DevuelveParidad a ParidadPorRangoFechasDevuelve
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim Par_Obj As par_cls


            Dim Paridad = From par In Data.par_cls Where par.par_fec >= fecha And par.par_fec <= fecha_hasta _
                             And par.id_P_0023 = tipo _
                             Select Moneda = par.id_P_0023, FechaPar = par.par_fec, Valor = par.par_val, valorcob = par.par_val_cob
            If Paridad.Count <> 0 Then

                For Each P In Paridad

                    Par_Obj = New CapaDatos.par_cls

                    With Par_Obj
                        .id_P_0023 = P.Moneda
                        .par_fec = P.FechaPar
                        If tipo = 2 Then
                            .par_val = Format(P.Valor, Fmt.FCMCD4)
                        ElseIf tipo = 3 Or tipo = 4 Then
                            .par_val = Format(P.Valor, Fmt.FCMCD)
                        End If

                        If tipo = 2 Then
                            .par_val_cob = Format(P.valorcob, Fmt.FCMCD4)
                        ElseIf tipo = 3 Or tipo = 4 Then
                            .par_val_cob = Format(P.valorcob, Fmt.FCMCD)
                        End If

                    End With

                    Coll.Add(P)

                Next

                Return Coll

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DevuelveFeriados(ByVal fecha As DateTime) As Collection
        'Descripcion: Devuelve `Feriados Segun Fecha Consultada
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 28/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim fer_Obj As fer_cls


            Dim Feriados = From fer In Data.fer_cls Where fer.fer_fec = fecha _
                             Select Feriado = fer.fer_fec
            If Feriados.Count <> 0 Then

                For Each P In Feriados

                    fer_Obj = New fer_cls

                    With fer_Obj
                        .fer_fec = P
                    End With

                    Coll.Add(P)

                Next

                Return Coll

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ParidadDevuelve(ByVal Moneda As Integer, ByVal Fecha As DateTime) As par_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la paridad segun moneda y fecha
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Par As par_cls = (From P In Data.par_cls Where P.id_P_0023 = Moneda And P.par_fec = Fecha.ToShortDateString).First


            Return Par

        Catch ex As Exception

            Dim Par As New par_cls
            'se cambia condicion de if
            'If Moneda = 1 Or Moneda = 0 Then
            If Moneda = 1 Or Moneda = 3 Then
                Par.par_val = 1
                Par.par_val_cob = 1
            Else
                Par.par_val = 0
                Par.par_val_cob = 1
            End If

            Return Par

        End Try

    End Function

    Public Function ParidadDevuelveRango(ByVal fecha As DateTime, ByVal tipo As Int16, ByVal fecha_hasta As DateTime) As Collection
        'Descripcion: Devuelve Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim Par_Obj As par_cls


            Dim Paridad = From par In Data.par_cls Where par.par_fec >= fecha And par.par_fec <= fecha_hasta _
                             And par.id_P_0023 = tipo _
                             Select Moneda = par.id_P_0023, FechaPar = par.par_fec, Valor = par.par_val, valorcob = par.par_val_cob
            If Paridad.Count <> 0 Then

                For Each P In Paridad

                    Par_Obj = New par_cls

                    With Par_Obj
                        .id_P_0023 = P.Moneda
                        .par_fec = P.FechaPar
                        .par_val = IIf(IsDBNull(P.Valor), 0, P.Valor)
                        .par_val_cob = IIf(IsDBNull(P.valorcob), 0, P.valorcob)
                    End With

                    Coll.Add(P)

                Next

                Return Coll

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ParidadesDelDiaDevuelve(ByVal Fecha As DateTime) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: devuelve las paridades del dia
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Par = From P In Data.par_cls Where P.par_fec = Fecha.ToShortDateString

            For Each P In Par
                Coll.Add(P)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Paridad_Negociacion_Devuelve(ByVal id_opn As Integer, ByVal moneda As Integer) As nmn_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la paridad segun moneda y fecha
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim col As New Collection

            Dim nmn As nmn_cls = (From P In Data.nmn_cls Where P.id_opn = id_opn And P.id_p_0023 = moneda).First



            Return nmn

        Catch ex As Exception


            If moneda = 1 Then

                Dim nmn As New nmn_cls

                nmn.par_val = 1
                nmn.par_val_cob = 1

                Return nmn
            Else
                Return Nothing
            End If



        End Try

    End Function


#End Region

#Region "Funciones Ejecutivos"

    Public Sub EjecutivosDevuelve(ByVal DP As DropDownList, ByVal ejecutivoconectado As Integer, ByVal TipoEjecutivo As Int16, ByVal Sucursal As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve ejecutivos segun funcion que trabajen con la sucursal del ejecutivo conectado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection

            'Dim sucursalesdelejecutivo = (From n In Data.nes_cls Where n.id_eje = ejecutivoconectado Select n).Distinct

            'For Each s In sucursalesdelejecutivo

            Dim Ejecutivos = (From Eje In Data.eje_cls _
                              Join Nes In Data.nes_cls On Nes.id_eje Equals Eje.id_eje _
                              Join Nef In Data.nef_cls On Nef.id_eje Equals Eje.id_eje _
                              Where Nef.id_P0045 = TipoEjecutivo And Nes.id_suc = Sucursal _
                              Select Codigo = Eje.id_eje, _
                                     Descripcion = Eje.eje_cod_temporal & " - " & Eje.eje_nom).Distinct

            For Each e In Ejecutivos

                If Coll.Count = 0 Then
                    Coll.Add(e)
                Else

                    Dim existe As Boolean = False

                    For i = 1 To Coll.Count
                        If Coll(i).codigo = e.Codigo Then
                            existe = True
                        End If
                    Next

                    If Not existe Then
                        Coll.Add(e)
                    End If

                End If

            Next

            'Next

            'If Coll.Count <> 0 Then
            RW.Llenar_Drop(Coll, "Codigo", "Descripcion", DP)
            'End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub EjecutivosDevuelve(ByVal DP As DropDownList, ByVal ejecutivoconectado As Integer, ByVal TipoEjecutivo As Int16)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve ejecutivos segun funcion que trabajen con la sucursal del ejecutivo conectado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim sucursalesdelejecutivo = (From n In Data.nes_cls Where n.id_eje = ejecutivoconectado Select n).Distinct

            For Each s In sucursalesdelejecutivo
                '//se debe cambiar tipo ejecutivo no existe en la tabla
                Dim Ejecutivos = (From Eje In Data.eje_cls _
                                  Join Nes In Data.nes_cls On Nes.id_eje Equals Eje.id_eje _
                                  Join Nef In Data.nef_cls On Nef.id_eje Equals Eje.id_eje _
                                  Where Nef.id_P0045 = TipoEjecutivo And Nes.id_suc = s.id_suc _
                                  Select Codigo = Eje.id_eje, _
                                         Descripcion = Eje.eje_cod_temporal & " - " & Eje.eje_nom).Distinct

                For Each e In Ejecutivos

                    If Coll.Count = 0 Then
                        Coll.Add(e)
                    Else

                        Dim existe As Boolean = False

                        For i = 1 To Coll.Count
                            If Coll(i).codigo = e.Codigo Then
                                existe = True
                            End If
                        Next

                        If Not existe Then
                            Coll.Add(e)
                        End If

                    End If

                Next

            Next

            'If Coll.Count <> 0 Then
            RW.Llenar_Drop(Coll, "Codigo", "Descripcion", DP)
            'End If

        Catch ex As Exception

        End Try

    End Sub
    Public Sub EjecutivosDevuelve(ByVal DP As DropDownList, ByVal CodigoSucursal As String)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve ejecutivos por su Sucursal y tipo de ejecutivos para llenar un dropdownlist
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/07/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Dim Ejecutivos = From Eje In Data.eje_cls _
                                       Join Nes In Data.nes_cls On Nes.id_eje Equals Eje.id_eje _
                                     Where Nes.id_suc = CodigoSucursal _
                                     Select Codigo = Eje.id_eje, Descripcion = Eje.eje_nom

            If Ejecutivos.Count <> 0 Then
                RW.Llenar_Drop(Ejecutivos, "Codigo", "Descripcion", DP)
                End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function AnticipoDevuelvePorLinea(ByVal Linea As Integer, ByVal TipoDocto As Integer) As apc_cls


        '*********************************************************************************************************************************
        'Descripcion: Devuelve los anticipos que ha realizado el cliente por una linea de credito
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Anticipos As apc_cls = (From A In Data.apc_cls Where A.id_ldc = Linea And _
                                                                     A.id_P_0031 = TipoDocto _
                                        Select A).First()


            Return Anticipos


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function EjecutivosAsignarComercialesDevuelve(ByVal CodigoSucursal As String, ByVal TipoEjecutivo As Int16) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve ejecutivos Comerciales por su Sucursal y tipo de ejecutivos
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  07/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Ejecutivos = (From Eje In Data.eje_cls _
                              Join Nes In Data.nes_cls On Nes.id_eje Equals Eje.id_eje _
                              Join Nef In Data.nef_cls On Nef.id_eje Equals Eje.id_eje _
                              Group Join R In Data.rsc_cls On R.cli_cls.id_eje_cod_eje Equals Eje.id_eje Into Rscc = Group, _
                              sumaCli = Sum(R.rsc_ddr_ctd), _
                              sumaDoc = Sum(R.rsc_ctd_doc) _
                              Where Nes.id_suc = CodigoSucursal And Nef.id_P0045 = TipoEjecutivo _
                             Select Codigo = Eje.id_eje, Descripcion = Eje.eje_nom, sumaCli, sumaDoc).Skip(sesion.NroPaginacion_Eje)

            Dim coll As New Collection

            For Each E In Ejecutivos.Take(5)
                coll.Add(E)
            Next

            Return coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function EjecutivosAsignarCobradoresDevuelve(ByVal CodigoSucursal As Integer, ByVal TipoEjecutivo As Integer, _
                                                        ByVal LlenaDrop As Boolean, ByVal Dp As DropDownList, _
                                                        Optional ByVal Pag As Integer = 999) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve ejecutivos Cobradores por su Sucursal y tipo de ejecutivos
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  06/04/2009         se modifica para llenar drop
        'JLagos                      20/05/2009         se elimina funcion con misma funcionalidad
        'A Saldivar                  31/01/2011         Se agrega paginacion en caso que se llene la grilla
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim coll_ejecutivos As New Collection

            Dim Ejecutivos = (From Eje In Data.eje_cls _
                                  Join Nes In Data.nes_cls On Nes.id_eje Equals Eje.id_eje _
                                  Join Nef In Data.nef_cls On Nef.id_eje Equals Eje.id_eje _
                                 Where Nes.id_suc = CodigoSucursal And _
                                       Nef.id_P0045 = TipoEjecutivo _
                                 Select New With {.Codigo = Eje.id_eje, _
                                                  .Nombre = Eje.eje_nom, _
                                                  .sumaDeu = "", _
                                                  .sumaDoc = ""}).Skip(sesion.NroPaginacion_Eje).Take(Pag)
            Dim cantdoc As Integer = 0
            For Each e In Ejecutivos

                Dim resumen = From r In Data.rsd_cls Where r.ddr_cls.deu_cls.id_eje_cod_cob = e.Codigo _
                              Group By r.ddr_cls.deu_ide _
                                Into Count(), sumaDoc = Sum(r.rsd_ctd_doc)

                cantdoc = 0

                For Each r In resumen
                    cantdoc += CInt(r.sumaDoc)
                Next

                e.sumaDeu = resumen.Count
                e.sumaDoc = cantdoc

                coll_ejecutivos.Add(e)

            Next

            If LlenaDrop = True Then
                Dim rg As New FuncionesGenerales.RutinasWeb
                rg.Llenar_Drop(coll_ejecutivos, "Codigo", "Nombre", Dp)
            Else
                Return coll_ejecutivos
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub ClienteReasignaEjecutivo(ByVal GV As GridView, ByVal CodigoEjecutivo_Dsd As Integer, _
                                        ByVal CodigoEjecutivo_Hst As Integer, ByVal RutCliente1 As Long, ByVal RutCliente2 As Long)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los clientes a reasignar para un ejecutivo de cuentas (15)
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'A. Saldivar                 16/08/2010         Se agrega rango amplio en caso que no se seleccione ejecutivo
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim Clientes = (From C In Data.cli_cls _
                           Join R In Data.rsc_cls On C.cli_idc Equals R.cli_idc _
                           Where (C.id_eje_cod_eje >= CodigoEjecutivo_Dsd And C.id_eje_cod_eje <= CodigoEjecutivo_Hst) And _
                                (C.cli_idc >= Format(CLng(RutCliente1), Var.FMT_RUT) And _
                                 C.cli_idc <= Format(CLng(RutCliente2), Var.FMT_RUT)) _
                           Select New With {.cli_idc = C.cli_idc, _
                                            C.cli_dig_ito, _
                                            .cli_rso = If(C.id_P_0044 = 1, _
                                                          C.cli_rso.Trim.ToUpper & " " & C.cli_ape_ptn.Trim.ToUpper & " " & C.cli_ape_mtn.Trim.ToUpper, _
                                                          C.cli_rso.Trim.ToUpper), _
                                            .Deudores = R.rsc_ddr_ctd, _
                                            .Doctos = R.rsc_ctd_doc, _
                                            .cli_fec_act_eje = C.cli_fec_act_eje, _
                                            .id_eje_cod_eje = C.id_eje_cod_eje}).Skip(Sesion.NroPaginacion)


            For Each C In Clientes.Take(15)
                C.cli_idc = Format(CLng(C.cli_idc), Fmt.FCMSD) & "-" & C.cli_dig_ito
                Coll.Add(C)
            Next

            GV.DataSource = Coll
            GV.DataBind()

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Sub DeudorReasignaEjecutivo(ByVal GV As GridView, ByVal CodEjeDsd As Integer, ByVal CodEjeHst As Integer, _
                                        ByVal RutDeudor1 As Long, ByVal RutDeudor2 As Long)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los deudores a reasignar para un ejecutivo de tipo cobrador telefonico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 20/05/2009
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'a. Saldivar                 16/08/2010         se agrega rango amplio en caso que no se seleccione ejecutivo.
        'Cristian Arce               12/09/2011         se agrega paginacion   
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim Deudores = (From D In Data.deu_cls Where D.deu_ide >= Format(RutDeudor1, Var.FMT_RUT) And _
                                                         D.deu_ide <= Format(RutDeudor2, Var.FMT_RUT) And _
                                                        (D.id_P_003 >= 1 And D.id_P_003 <= 2) And _
                                                        (D.id_eje_cod_cob >= CodEjeDsd And D.id_eje_cod_cob <= CodEjeHst) _
               Select New With {.deu_ide = D.deu_ide, _
                                  .deudor = If(D.id_P_0044 = 1, _
                                               D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                               D.deu_rso.Trim), _
                                  .Digito = D.deu_dig_ito, _
                                  .id_eje = D.id_eje_cod_cob, _
                                   D.eje_cls.eje_des_cra, _
                                  .Cantidad = (From R In Data.rsd_cls Where R.ddr_cls.deu_ide = D.deu_ide).Count, _
                                  .Suma = (From R In Data.rsd_cls Where R.ddr_cls.deu_ide = D.deu_ide Select R.rsd_ctd_doc).Sum}).Skip(Sesion.NroPaginacion)

            For Each D In Deudores.Take(7)

                D.deu_ide = Format(CLng(D.deu_ide), Fmt.FCMSD) & "-" & D.Digito

                If IsNothing(D.Suma) Then D.Suma = 0

                Coll.Add(D)

            Next



            GV.DataSource = Coll
            GV.DataBind()

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Function EjecutivosDevuelve(ByVal Sucursal_Desde As Integer, _
                                       ByVal Sucursal_Hasta As Integer, _
                                       ByVal Cargo_Desde As Integer, _
                                       ByVal Cargo_Hasta As Integer, _
                                       ByVal Nombre As String) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los ejecutivos segun criterio de busqueda
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 29/05/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb

            If Nombre.Trim = "" Then

                Dim Ejecutivos = From Eje In Data.eje_cls _
                                           Where (Eje.id_suc >= Sucursal_Desde And _
                                                  Eje.id_suc <= Sucursal_Hasta) And _
                                                 (Eje.id_P_0045 >= Cargo_Desde And _
                                                  Eje.id_P_0045 <= Cargo_Hasta) _
                                           Select Eje.id_eje, _
                                                  Eje.eje_nom, _
                                                  Eje.eje_des_cra, _
                                                  Eje.suc_cls.suc_nom, _
                                                  Eje.P_0045_cls.pnu_des

                Dim Coll As New Collection

                For Each E In Ejecutivos
                    Coll.Add(E)
                Next

                Return Coll

            Else

                Dim Ejecutivos = From Eje In Data.eje_cls _
                                           Where (Eje.id_suc >= Sucursal_Desde And _
                                                  Eje.id_suc <= Sucursal_Hasta) And _
                                                 (Eje.id_P_0045 >= Cargo_Desde And _
                                                  Eje.id_P_0045 <= Cargo_Hasta) And _
                                                  Eje.eje_nom.StartsWith(Nombre) _
                                           Select Eje.id_eje, _
                                                  Eje.eje_nom, _
                                                  Eje.eje_des_cra, _
                                                  Eje.suc_cls.suc_nom, _
                                                  Eje.P_0045_cls.pnu_des

                Dim Coll As New Collection

                For Each E In Ejecutivos
                    Coll.Add(E)
                Next

                Return Coll


            End If


        Catch ex As Exception

        End Try

    End Function

    Public Function EjecutivoDevuelve(ByVal Codigo As Integer) As eje_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos de un ejecutivo por si codigo
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 29/05/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb

            Dim Ejecutivo As eje_cls = (From E In Data.eje_cls Where E.id_eje = Codigo).First

            Return Ejecutivo

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Public Function EjecutivoPorAliasDevuelve(ByVal Login As String) As eje_cls

    '    '*********************************************************************************************************************************
    '    'Descripcion: Devuelve los datos de un ejecutivo por si codigo
    '    'Creado por= Jorge Lagos C.
    '    'Fecha Creacion: 29/05/2009
    '    'Quien Modifica              Fecha              Descripcion
    '    '*********************************************************************************************************************************

    '    Try

    '        Dim Data As New CapaDatos.DataClsFactoringDataContext
    '        Dim RG As New FuncionesGenerales.RutinasWeb

    '        Dim Ejecutivo As eje_cls = (From E In Data.eje_cls Where E.eje_des_cra.Trim.ToUpper = Login.Trim.ToUpper).First

    '        Return Ejecutivo

    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

    Public Function EjecutivoPorAliasDevuelve(ByVal Login As String) As DataSet

        Try
            Dim ds As New DataSet
            Dim sqlstr As String

            sqlstr = "exec SP_WEB_EJECUTIVOPORALIASDEVUELVE '" & Login & "'"

            ds = sql.ExecuteDataSet(sqlstr)

            Return ds

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function EjecutivoDevuelveFunciones(ByVal Codigo As Integer) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las funciones de cargo que cumple un ejecutivo por si id
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 29/05/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb

            Dim Funciones = From N In Data.nef_cls Where N.id_eje = Codigo

            Dim Coll As New Collection

            For Each F In Funciones
                Coll.Add(F)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Funciones Deudores"

    Public Function DeudorDevuelvePorRut(ByVal RutDeudor As Long) As deu_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve un Deudor por su rut
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Deudor As deu_cls = (From D In Data.deu_cls Where CLng(D.deu_ide) = RutDeudor Select D).First

            Return Deudor


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DeudorDevuelveTodos(ByVal RutDeu_Dsd As Long, ByVal RutDeu_Hst As Long, _
                                        ByVal TipoDeudor_Dsd As Integer, ByVal TipoDeudor_Hst As Integer, _
                                        ByVal Segmento_Dsd As Integer, ByVal Segmento_Hst As Integer, _
                                        ByVal Razon As String, ByVal Orden As Int16) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Deudores
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 19/06/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     25/11/2008          Se agrega Tipo de Deudor y Segmentos como criterios de busquedas
        'C Arce                     09/11/2011          se agrega que se cargen hasta 8 personas por pagina.
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            '(D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
            '((D.deu_rso & " " & D.deu_ape_ptn & " " & D.deu_ape_mtn) Like "%" & Razon & "%") _
            If Segmento_Dsd <> Segmento_Hst Then

                Select Case Orden
                    Case 1
                        Dim Deudores = (From D In Data.deu_cls Where (D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           ((D.deu_rso & " " & D.deu_ape_ptn & " " & D.deu_ape_mtn).Contains(Razon)) _
                                                           Order By D.deu_ide _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                    Case 2
                        Dim Deudores = (From D In Data.deu_cls Where CLng(D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.deu_rso _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                    Case 3
                        Dim Deudores = (From D In Data.deu_cls Where CLng(D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.id_P_0044 _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                    Case 4
                        Dim Deudores = (From D In Data.deu_cls Where D.deu_ide.Contains(RutDeu_Dsd) And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.id_P_0044 _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                End Select





            Else


                Select Case Orden
                    Case 1
                        Dim Deudores = (From D In Data.deu_cls Where CLng(D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           CInt(D.id_P_0076) >= Segmento_Dsd And _
                                                           CInt(D.id_P_0076) <= Segmento_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.deu_ide _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                    Case 2
                        Dim Deudores = (From D In Data.deu_cls Where CLng(D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           CInt(D.id_P_0076) >= Segmento_Dsd And _
                                                           CInt(D.id_P_0076) <= Segmento_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.deu_rso _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                              D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                              D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                    Case 3

                        Dim Deudores = (From D In Data.deu_cls Where CLng(D.deu_ide) >= RutDeu_Dsd And _
                                                           CLng(D.deu_ide) <= RutDeu_Hst And _
                                                           CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                           CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                           CInt(D.id_P_0076) >= Segmento_Dsd And _
                                                           CInt(D.id_P_0076) <= Segmento_Hst And _
                                                           (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                           Order By D.id_P_0044 _
                                      Select New With {.deu_ide = D.deu_ide, _
                                                       .deu_rso = If(D.id_P_0044 = 1, _
                                                                     D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                     D.deu_rso.Trim), _
                                                       .pnu_est_des = D.P_003_cls.pnu_des, _
                                                       .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                       .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacion)

                        For Each D In Deudores.Take(8)

                            D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito

                            Coll.Add(D)

                        Next

                End Select

            End If


            Return Coll


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DeudorDevuelveAyuda(ByVal RutDeu_Dsd As Long, ByVal RutDeu_Hst As Long, _
                                       ByVal TipoDeudor_Dsd As Integer, ByVal TipoDeudor_Hst As Integer, _
                                       ByVal Segmento_Dsd As Integer, ByVal Segmento_Hst As Integer, _
                                       ByVal Razon As String, ByVal Orden As Int16) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Deudores
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 19/06/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     25/11/2008          Se agrega Tipo de Deudor y Segmentos como criterios de busquedas
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Select Case Orden
                Case 1
                    Dim Deudores = (From D In Data.deu_cls Where D.deu_ide.Contains(RutDeu_Dsd) And _
                                                       CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                       CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                       (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                       Order By D.deu_ide _
                                  Select New With {.deu_ide = D.deu_ide, _
                                                   .deu_rso = If(D.id_P_0044 = 1, _
                                                                 D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                 D.deu_rso.Trim), _
                                                   .pnu_est_des = D.P_003_cls.pnu_des, _
                                                   .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                   .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacionPag)

                    For Each D In Deudores.Take(8)

                        D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito.Value.ToString().ToUpper()


                        Coll.Add(D)

                    Next

                Case 2
                    Dim Deudores = (From D In Data.deu_cls Where D.deu_ide.Contains(RutDeu_Dsd) And _
                                                       CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                       CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                       (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                       Order By D.deu_rso _
                                  Select New With {.deu_ide = D.deu_ide, _
                                                   .deu_rso = If(D.id_P_0044 = 1, _
                                                                 D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                 D.deu_rso.Trim), _
                                                   .pnu_est_des = D.P_003_cls.pnu_des, _
                                                   .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                   .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacionPag)

                    For Each D In Deudores.Take(8)

                        D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito.Value.ToString().ToUpper()

                        Coll.Add(D)

                    Next

                Case 3
                    Dim Deudores = (From D In Data.deu_cls Where D.deu_ide.Contains(RutDeu_Dsd) And _
                                                       CInt(D.id_P_0044) >= TipoDeudor_Dsd And _
                                                       CInt(D.id_P_0044) <= TipoDeudor_Hst And _
                                                       (D.deu_rso.Contains(Razon) Or D.deu_ape_ptn.Contains(Razon) Or D.deu_ape_mtn.Contains(Razon)) _
                                                       Order By D.id_P_0044 _
                                  Select New With {.deu_ide = D.deu_ide, _
                                                   .deu_rso = If(D.id_P_0044 = 1, _
                                                                 D.deu_rso.Trim & " " & D.deu_ape_ptn.Trim & " " & D.deu_ape_mtn.Trim, _
                                                                 D.deu_rso.Trim), _
                                                   .pnu_est_des = D.P_003_cls.pnu_des, _
                                                   .pnu_tip_deu_des = D.P_0044_cls.pnu_des, _
                                                   .digito = D.deu_dig_ito}).Skip(Sesion.NroPaginacionPag)

                    For Each D In Deudores.Take(8)

                        D.deu_ide = FC.FormatoMiles(CLng(D.deu_ide.Trim)) & "-" & D.digito.Value.ToString().ToUpper()

                        Coll.Add(D)

                    Next

            End Select

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Devuelvelineaglobaldeudor(ByVal RutDeudor As Long) As Collection  'FY 24-05-2012
        '*********************************************************************************************************************************
        'Descripcion: Devuelve informacion sobre la linea de financiamiento correspondiente al deudor
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 24/05/2012
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     14-08-2014          se agrega como criterio para los montos recurso (id_P_0030, dsi_flj)
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim coll As New Collection

            'If(i.doc_pag_ddr Is Nothing, "N", "S") = "N" And _

            Dim Deudor = (From d In Data.deu_mon_cls _
                         Where CLng(d.deu_ide) = RutDeudor _
                         Order By CLng(d.id_p_0023), d.deu_ide, d.id_p_0029, d.deu_mon_fec Descending _
                         Select New With { _
                                .codigo = d.id_p_0023, _
                                .descripcion = d.P_0023_cls.pnu_des, _
                                .monto = d.deu_mon_apr, _
                                d.deu_mon_dis, _
                                d.deu_mon_ocu, _
                                .Ejecutivo = d.eje_cls.eje_des_cra, _
                                .Estado = d.P_0029_cls.pnu_des, _
                                .Observacion = d.deu_mon_obs.Trim(), _
                                .id_estado = d.id_p_0029, _
                                .fecha_vcto = d.deu_fec_vto, _
                                .conrecurso = (From i In Data.doc_cls Where (i.dsi_cls.id_P_0011.Equals(1) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(2) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(4) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(8) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(9) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(11) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(12)) And _
                                                                             i.dsi_cls.deu_ide.Equals(d.deu_ide) And _
                                                                             i.dsi_cls.ope_cls.opn_cls.id_P_0023.Equals(d.id_p_0023) And _
                                                                             i.dsi_cls.ope_cls.opn_cls.opn_res_son.Equals("1") And _
                                                                             i.dsi_cls.dsi_flj = "N" And _
                                                                             i.opo_cls.ope_cls.id_P_0030 = 3 _
                                                                             Group By i.dsi_cls.deu_ide Into monto = Sum(i.doc_sdo_ddr) _
                                                                             Select monto).First(), _
                                .sinrecurso = (From i In Data.doc_cls Where (i.dsi_cls.id_P_0011.Equals(1) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(2) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(4) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(8) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(9) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(11) Or _
                                                                             i.dsi_cls.id_P_0011.Equals(12)) And _
                                                                             i.dsi_cls.deu_ide.Equals(d.deu_ide) And _
                                                                             i.dsi_cls.ope_cls.opn_cls.id_P_0023.Equals(d.id_p_0023) And _
                                                                             i.dsi_cls.ope_cls.opn_cls.opn_res_son.Equals("0") And _
                                                                             i.dsi_cls.dsi_flj = "N" And _
                                                                             i.opo_cls.ope_cls.id_P_0030 = 3 _
                                                                             Group By i.dsi_cls.deu_ide Into monto = Sum(i.doc_sdo_ddr) _
                                                                             Select monto).First()})

            For Each m In Deudor

                If IsNothing(m.conrecurso) Then
                    m.conrecurso = 0
                End If

                If IsNothing(m.sinrecurso) Then
                    m.sinrecurso = 0
                End If

                coll.Add(m)

            Next

            Return coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Devuelvelineaglobaldeudor(ByVal RutDeudor As Long, ByVal moneda As Integer) As Double 'FY 24-05-2012
        '*********************************************************************************************************************************
        'Descripcion: Devuelve informacion sobre la linea de financiamiento correspondiente al deudor
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 24/05/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Cupo As Double = (From d In Data.deu_mon_cls Join mon In Data.P_0023_cls On mon.id_P_0023 Equals d.id_p_0023 _
                           Join d2 In Data.deu_cls On d2.deu_ide Equals d.deu_ide _
                           Where CLng(d.deu_ide) = RutDeudor And d.id_p_0023 = moneda And d.id_p_0029 = 1 Select d.deu_mon_apr).First()
            Return Cupo

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DevuelveCalendarioPagoDeudor(ByVal RutDeudor As Long) As IQueryable
        '-------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve el calendario de pago
        'Creado por= Jorge Lagos
        'Fecha Creacion: 24/05/2012
        '-------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Calendrio = From C In Data.cpg_cls Where C.deu_ide = Format(RutDeudor, Var.FMT_RUT) Order By C.fec_cpg Descending

            Return Calendrio

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DevuelveCalendarioPagoDeudor(ByVal RutDeudor As Long, ByVal fechavcto As String) As String
        '-------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve el fecha 
        'Creado por= Jorge Lagos
        'Fecha Creacion: 24/05/2012
        '-------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Calendrio = From C In Data.cpg_cls Where C.deu_ide = Format(RutDeudor, Var.FMT_RUT) _
                            And C.fec_cpg >= fechavcto Order By C.fec_cpg Ascending

            For Each c In Calendrio
                Return c.fec_cpg
            Next

            Return fechavcto

        Catch ex As Exception
            Return fechavcto
        End Try

    End Function


    Public Function DevuelveCalendarioPagoDeudorEspecial(ByVal RutDeudor As Long, ByVal fechavcto As String) As String
        '-------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve el fecha 
        'Creado por= P.Gatica
        'Fecha Creacion: 05/02/2014
        '-------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Calendrio = From C In Data.cpg_cls Where C.deu_ide = Format(RutDeudor, Var.FMT_RUT) Order By C.fec_cpg Ascending

            For Each c In Calendrio
                Return c.fec_cpg
            Next

            Return ""

        Catch ex As Exception
            Return ""
        End Try

    End Function

#End Region

#Region "Bancos"

    Public Function BancosDevuelveTodos(ByVal Dp As DropDownList) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Bancos 
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RW As New FuncionesGenerales.RutinasWeb


            Dim Bancos = From Bco In Data.bco_cls _
                         Select Codigo = Bco.id_bco, _
                                   Descripcion = Bco.bco_des

            RW.Llenar_Drop(Bancos, "Codigo", "Descripcion", Dp)

            Return Bancos


        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function bancoDevuelve(ByVal Llenagrid As Boolean, _
                                    Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo Tasa y Plazos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim bco = From b In data.bco_cls _
                      Select New With {.bco_Cod = b.id_bco, _
                                      .bco_desc = b.bco_des}
            If Llenagrid Then
                GV.DataSource = bco
                GV.DataBind()
                Return Nothing
            Else
                Return bco
            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CodigoBancoValida(ByVal id_bco As Integer) As bco_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo Tasa y Plazos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim bco As bco_cls = (From b In data.bco_cls Where b.id_bco = id_bco Select b).First()

            Return bco



        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SBCDevuelveporBanco(ByVal Cod_Banco As Integer, ByVal Llenagrid As Boolean, _
                                       Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todas las sucursales por banco
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim bco = From b In data.sbc_cls Where b.id_bco = Cod_Banco _
                      Select New With {.Cod_Suc = b.id_sbc, _
                                      .Desc_Suc = b.sbc_des}
            If Llenagrid Then
                GV.DataSource = bco
                GV.DataBind()
                Return Nothing
            Else
                Return bco
            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SucursalbancocDevuelveObjeto(ByVal Cod_Sbc As Integer) As Object
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim suc As sbc_cls = (From S In data.sbc_cls Where S.id_sbc = Cod_Sbc).First
            Return suc

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


#End Region

#Region "Categoria Riesgo"
    Public Function CategoriaRiesgoDevuelve(ByVal Id_Docto As Integer) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve categoria de Riesgo por id Docto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 28/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim coll As New Collection
            Dim Ctr = From c In data.ctr_cls Where c.id_p_0031 = Id_Docto _
                      Select c.id_ctr, _
                             Codigo = c.id_P_0065, _
                             Descripcion = c.P_0065_cls.pnu_des, _
                             c.id_p_0031, _
                             dsd = c.ctr_dias_des, _
                             hst = c.ctr_dias_hst
            For Each i In Ctr
                coll.Add(i)
            Next

            If coll.Count > 0 Then
                Return coll
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function TipoRiesgoDevuelve() As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve tipo de Riesgo 
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 28/07/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim coll As New Collection
            Dim Ctr = From c In data.P_0065_cls _
                      Select Codigo = c.id_P_0065, _
                             Descripcion = c.pnu_des, _
                             dsd = 0, _
                             hst = 0
            For Each i In Ctr
                coll.Add(i)
            Next

            If coll.Count > 0 Then
                Return coll
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
#End Region

#Region "Gastos"

    Public Function GastosDevuelvePorSucursal(ByVal Id_Suc As Integer, Optional ByVal Paginacion As Integer = 9999) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Gastos Por Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 24/04/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  01/02/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Gastos = (From G In data.gto_cls Where G.id_suc = Id_Suc _
                         Select G.id_gto, _
                                G.id_P_0036, _
                                Tipo_Gasto = G.P_0036_cls.pnu_des, _
                                Suc_Nom = G.suc_cls.suc_nom, _
                                G.gto_des, _
                                Estado = G.gto_est, _
                                G.gto_mto, _
                                G.gto_iva, _
                                G.val_con).Skip(sesion.NroPaginacion).Take(Paginacion)
            Return Gastos

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Ciudad Comuna"

    Public Function ComunaDevuelvePorCiudad(ByVal Codigo_Ciudad As Integer, ByVal llenagrid As Boolean, _
                                             Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve todas las Comunas por Ciudad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Comuna = From C In Data.cmn_cls Where C.id_ciu = Codigo_Ciudad _
                         Select New With {.id_Cmn = C.id_cmn, _
                                          .Nom_Cmn = C.cmn_des, _
                                          .Zona = C.zon_cls.zon_des}


            If llenagrid Then
                GV.DataSource = Comuna
                GV.DataBind()
                Return Nothing
            Else
                Return Comuna
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CiudadDevuelvePorSucursal(ByVal idSuc As Integer, _
                                              Optional ByVal Paginacion As Integer = 9999) As Object

        Try

            Dim coll As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            'Dim Ciudad = (From Z In Data.zon_cls Join C In Data.cmn_cls On Z.id_zon Equals C.id_zon _
            '           Where Z.id_suc = idSuc Group By C.id_ciu, C.ciu_cls.ciu_des, Z.suc_cls.suc_des_cra Into First()).Skip(sesion.NroPaginacion).Take(Paginacion)

            Dim Ciudad = (From C In Data.ciu_cls Order By C.id_ciu _
                          Select C.id_ciu, C.ciu_des).Skip(sesion.NroPaginacion).Take(Paginacion)

            Return Ciudad

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CiudadDevuelvePorSucursal_Gri(ByVal idSuc As Integer, Optional ByVal Paginacion As Integer = 9999) As Object

        Try

            Dim coll As New Collection
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Ciudad = (From Z In Data.zon_cls Join C In Data.cmn_cls On Z.id_zon Equals C.id_zon _
                        Where Z.id_suc = idSuc Group By C.id_ciu, C.ciu_cls.ciu_des, Z.suc_cls.suc_des_cra Into First()).Skip(sesion.NroPaginacion).Take(Paginacion)


            Return Ciudad

            'For Each i In Ciudad
            '    coll.Add(i)
            'Next
            'Return coll

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ComunaDevuelvePorCodigoComuna(ByVal Id_Cmn As Integer) As cmn_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Comuna por Id_cmn
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Comuna As cmn_cls = (From Com In Data.cmn_cls Where Com.id_cmn = Id_Cmn).First


            Return Comuna

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

#End Region

#Region "Cartera"
    Public Sub CarteraDevuelveTodos(ByVal GV As GridView)
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo De Cartera
        'Creado por : Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Cartera = From Crt In data.crt_cls _
            Select New With {.Nro = Crt.id_crt, _
                            .Des = Crt.crt_des, _
                            .NDias = Crt.crt_ctd_dia}
            GV.DataSource = Cartera
            GV.DataBind()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Sucursal/Plaza"

    Public Sub SucursalDevuelveTodo(ByVal gv As GridView, Optional ByVal Paginacion As Integer = 9999, Optional ByVal codigo As String = "")
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo De Cartera
        'Creado por : Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  11/02/2011         Se agrega paginacion   
        'JLagos                      20/10/2012         Se agrega orden y busqueda por codigo de sucursales del banco
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            'Dim Suc = (From s In data.suc_cls _
            '           Where s.suc_cod_ftg.Contains(codigo) _
            '           Order By s.suc_cod_ftg _
            '          Select New With {.suc_cod = s.id_suc, _
            '                           .suc_nom = s.suc_nom, _
            '                           .suc_des_cra = s.suc_des_cra, _
            '                           .pal_pla_doc = s.id_PL_000047, _
            '                           .codigo = s.suc_cod_ftg, _
            '                           .Des_plaza = s.PL_000047_cls.pal_des}).Skip(sesion.NroPaginacion_Sucursal).Take(Paginacion)
            Dim Suc = (From s In data.suc_cls _
                       Where s.suc_cod_ftg.Contains(codigo) _
                       Order By s.suc_cod_ftg _
                      Select New With {.suc_cod = s.id_suc, _
                                       .suc_nom = s.suc_nom, _
                                       .suc_des_cra = s.suc_des_cra, _
                                       .pal_pla_doc = Nothing, _
                                       .codigo = s.suc_cod_ftg, _
                                       .Des_plaza = Nothing}).Skip(sesion.NroPaginacion_Sucursal).Take(Paginacion)

            gv.DataSource = Suc
            gv.DataBind()
            'DropSucursal.DataSource = Suc
            'DropSucursal.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Function SucursalDevuelve(ByVal Id As Integer) As suc_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Objeto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sucursal As suc_cls = (From s In data.suc_cls Where s.id_suc = Id).First

            Return sucursal
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ZonaBancaTerritorialDevuelve(ByVal Tipo As Integer, ByVal codigo As String) As suc_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las sucursales activas                  
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 09/08/2013                                                                                                                    
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Return (From S In Data.suc_cls Where S.SUC_TIP_UNI.Equals(Tipo) And S.suc_cod_ftg.Equals(codigo) _
                    Select S).First()

        Catch ex As Exception

        End Try

    End Function

    Public Function PlazaDevuelveXSucursal(ByVal IdSuc As Integer, _
                                          ByVal llenagrid As Boolean, _
                                          Optional ByVal GV As GridView = Nothing, _
                                          Optional ByVal Paginacion As Integer = 9999) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Plaza Por Id_Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 11/02/2011         Se agrega paginacion                
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim pds = (From p In data.pds_cls Where p.id_suc = IdSuc _
                      Select New With {.pal_Cod = p.id_pds, _
                                      .pal_des = p.PL_000047_cls.pal_des, _
                                      .pds_ret = p.pds_ret}).Skip(sesion.NroPaginacion_Plaza).Take(Paginacion)
            If llenagrid Then
                GV.DataSource = pds
                GV.DataBind()
                Return Nothing
            Else
                Return pds
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PlazaDevuelveXId(ByVal IdP As Integer) As pds_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Plaza
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim plaza As pds_cls = (From p In data.pds_cls Where p.id_pds = IdP).First
            Return plaza
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ZonaDevuelvePorSucursal(ByVal sucursal As Integer, ByVal Llenadrop As Boolean, _
                                            Optional ByVal dp As DropDownList = Nothing) As Object
        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext


            Dim zona = From z In data.zon_cls Where z.id_suc = sucursal And z.suc_cls.suc_par_est = 1 Order By z.id_zon

            If Llenadrop Then
                Dim rg As New FuncionesGenerales.RutinasWeb
                rg.Llenar_Drop(zona, "id_zon", "zon_des", dp, 0, "Seleccionar")
                Return Nothing
            Else
                Return zona
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SucursalesDevuelve(ByVal id_ejecutivo As Integer, Optional ByVal LlenaDrop As Boolean = True, Optional ByVal Dp As DropDownList = Nothing) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las sucursales activas                  
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb
            Dim sucr As Object

            If LlenaDrop Then
                Dim Sucursales = From n In Data.nes_cls _
                                 Where n.id_eje = id_ejecutivo _
                                    And n.suc_cls.SUC_TIP_UNI = 10 _
                                    And n.suc_cls.suc_par_est = 1 _
                                 Order By n.suc_cls.suc_cod_ftg _
                                 Select New With {.Codigo = n.id_suc, _
                                                  .Descripcion = n.suc_cls.suc_cod_ftg & " " & n.suc_cls.suc_nom}

                RG.Llenar_Drop(Sucursales, "Codigo", "Descripcion", Dp)

            Else
                sucr = From n In Data.nes_cls _
                       Where n.id_eje = id_ejecutivo _
                        And n.suc_cls.SUC_TIP_UNI = 10 _
                        And n.suc_cls.suc_par_est = 1 _
                       Order By n.suc_cls.suc_cod_ftg _
                       Select New With {.Codigo = n.id_suc, _
                       .Descripcion = n.suc_cls.suc_cod_ftg & " " & n.suc_cls.suc_nom}

                If Not IsDBNull(sucr) Then
                    Return sucr
                Else
                    Return Nothing
                    End If
            End If

        Catch ex As Exception

        End Try

    End Function

    Public Function SucursalesDevuelveTodas(Optional ByVal LlenaDrop As Boolean = True, Optional ByVal Dp As DropDownList = Nothing) As Object
        'Public Function SucursalesDevuelveTodas(Optional ByVal LlenaDrop As Boolean = True) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las sucursales activas                  
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb

            'Dim Sucursales = From s In Data.suc_cls _
            '                  Where s.SUC_TIP_UNI = 10 _
            '                  And s.suc_par_est = 1 _
            '                     Order By s.id_suc _
            '                     Select New With {.Codigo = s.id_suc, _
            '                                      .Descripcion = s.suc_cod_ftg & " " & s.suc_nom}


            Dim Sucursales = From s In Data.suc_cls _
                              Where s.SUC_TIP_UNI = 10 _
                              And s.suc_par_est = 1 _
                                 Order By s.id_suc _
                                 Select New With {.Codigo = s.id_suc, _
                                                  .Descripcion = s.suc_cod_ftg & " " & s.suc_nom}



            If LlenaDrop Then
                RG.Llenar_Drop(Sucursales, "Codigo", "Descripcion", Dp)

            Else
                If Not IsDBNull(Sucursales) Then
                    Return Sucursales
                Else
                    Return Nothing
                End If
            End If

        Catch ex As Exception

        End Try

    End Function

    Public Sub OficinasDevuelvePorTipo(ByVal tipo As Integer, ByVal Dp As DropDownList)
        '---------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve las oficinas por tipo
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 09/06/2015                                                                                                                
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '---------------------------------------------------------------------------------------------------------------------------------
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb
            Dim sucr As Object

            Dim Sucursales = From n In Data.suc_cls _
                                 Where n.SUC_TIP_UNI = tipo _
                                 And n.suc_par_est = 1 _
                                 Order By n.suc_cod_ftg _
                                 Select New With {.Codigo = n.id_suc, _
                                                  .Descripcion = n.suc_cod_ftg & " " & n.suc_nom.Trim}

            RG.Llenar_Drop(Sucursales, "Codigo", "Descripcion", Dp)

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Tasas"

    Public Function DevuelveTasas(ByVal Moneda As Integer, ByVal dias_dsd As Integer, _
                                  ByVal dias_hst As Integer) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Pregunata si existe un registro con el siguiente  criterio de Tasas y Plazos
        'Creado por : Antonio Saldivar M.
        'Fecha Creacion: 05/05/2005
        'Quien Modifica              Fecha              Descripcion
        'JLagos                     22-05-2009          -se quita rango, lo que se quiere buscar es si existe el rango para una moneda
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            'Dim tasas = From t In data.typ_cls Where t.id_P_0023 = Moneda _
            '                        And (t.typ_dde >= dias_dsd And t.typ_dde >= dias_dsd) _
            '                        And (t.typ_hta >= dias_hst And t.typ_hta <= dias_hst)
            Dim tasas = From t In data.typ_cls Where t.id_P_0023 = Moneda _
                                                And (t.typ_dde >= dias_dsd Or t.typ_hta <= dias_hst)
            '                        And (t.typ_dde <= dias_dsd And t.typ_hta >= dias_dsd)
            '                         Or (t.typ_dde <= dias_hst And t.typ_hta >= dias_hst)

            Return tasas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


#End Region

#Region "Llena Drop con Parametros"

    Public Sub RegionDevuelve(ByVal sucursal As TablaParametro, ByVal Dp As DropDownList)

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object

            parametro = From P In Data.P_001_cls Join S In Data.suc_cls On P.id_P_001 Equals S.suc_cod_reg _
                        Where S.id_suc = sucursal _
                        Select Codigo = P.id_P_001, Descripcion = P.pnu_des, Estado = P.pnu_est

            Dim rg As New FuncionesGenerales.RutinasWeb
            rg.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp, 0, "Seleccionar")

        Catch ex As Exception

        End Try

    End Sub

    'Public Sub CanalDevuelve(ByVal Dp As DropDownList)

    '    Try

    '        Dim SQL As New FuncionesGenerales.SqlQuery
    '        Dim query As String = ""
    '        Dim parametro As New ListItem
    '        Dim dt As DataSet
    '        Dim RG As New FuncionesGenerales.Variables
    '        Dim newListItem As ListItem


    '        query = "Exec USP_CARGA_CANALES_CDLINFORMACION"
    '        dt = SQL.ExecuteDataSet(query)

    '        newListItem = New ListItem("Seleccionar", "0")
    '        Dp.Items.Clear()
    '        Dp.Items.Add(newListItem)

    '        For i = 0 To dt.Tables(0).Rows.Count - 1
    '            newListItem = New ListItem(dt.Tables(0).Rows(i).Item(1), dt.Tables(0).Rows(i).Item(0))
    '            Dp.Items.Add(newListItem)
    '        Next


    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Public Sub SubCanalDevuelve(ByVal canal As String, ByVal Dp As DropDownList)

    '    Try


    '        Dim SQL As New FuncionesGenerales.SqlQuery
    '        Dim query As String = ""
    '        Dim parametro As New ListItem
    '        Dim dt As DataSet
    '        Dim RG As New FuncionesGenerales.Variables
    '        Dim newListItem As ListItem


    '        query = "Exec USP_CARGA_SUBCANALES_CDLINFORMACION '" & canal & "'"

    '        dt = SQL.ExecuteDataSet(query)

    '        newListItem = New ListItem("Seleccionar", "0")
    '        Dp.Items.Clear()
    '        Dp.Items.Add(newListItem)

    '        For i = 0 To dt.Tables(0).Rows.Count - 1
    '            newListItem = New ListItem(dt.Tables(0).Rows(i).Item(3), dt.Tables(0).Rows(i).Item(1))
    '            Dp.Items.Add(newListItem)
    '        Next


    '    Catch ex As Exception

    '    End Try

    'End Sub

    Public Sub GestorDevuelve(ByVal oficina As String, ByVal Dp As DropDownList)

        Try

            Dim SQL As New FuncionesGenerales.SqlQuery
            Dim query As String = ""
            Dim parametro As New ListItem
            Dim dt As DataSet
            Dim RG As New FuncionesGenerales.Variables
            Dim newListItem As ListItem

            query = "Exec USP_PROCESOCARGA_GERENTE_OFICINA " & oficina & ""

            dt = SQL.ExecuteDataSet(query)

            newListItem = New ListItem("Seleccionar", "0")
            Dp.Items.Clear()
            Dp.Items.Add(newListItem)

            For i = 0 To dt.Tables(0).Rows.Count - 1
                newListItem = New ListItem(dt.Tables(0).Rows(i).Item(0), dt.Tables(0).Rows(i).Item(1))
                Dp.Items.Add(newListItem)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Sub GestorNegocioDevuelve(ByVal oficina As String, ByVal Dp As DropDownList)

        Try

            Dim SQL As New FuncionesGenerales.SqlQuery
            Dim query As String = ""
            Dim parametro As New ListItem
            Dim dt As DataSet
            Dim RG As New FuncionesGenerales.Variables
            Dim newListItem As ListItem

            query = "Exec BF_CONFIRMING..SP_MA_DEVUELVE_GESTOR_x_SUCURSAL '" & oficina & "'"

            dt = SQL.ExecuteDataSet(query)

            newListItem = New ListItem("Seleccionar", "0")
            Dp.Items.Clear()
            Dp.Items.Add(newListItem)

            For i = 0 To dt.Tables(0).Rows.Count - 1
                newListItem = New ListItem(dt.Tables(0).Rows(i).Item(1), dt.Tables(0).Rows(i).Item(0))
                Dp.Items.Add(newListItem)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Function ParametrosDevuelve(ByVal CodigoTabla As TablaParametro, Optional ByVal LlenaDrop As Boolean = False, Optional ByVal Dp As DropDownList = Nothing) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Parametros que esten activos y segun codigo de tabla 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  02/02/2009         Se agrego campo estado
        'JLagos                      29/06/2009         Se cambio lo que devuelve la funcion de Object a Collection
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object

            Select Case CodigoTabla

                Case TablaParametro.Region
                    parametro = From P In Data.P_001_cls Where P.pnu_est = "A" _
                                      Select Codigo = P.id_P_001, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ComunaLocalidad
                    parametro = From P In Data.P_002_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_002, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoDeudor
                    parametro = From P In Data.P_003_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_003, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.Niveles
                    parametro = From P In Data.P_005_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_005, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.ModoOperacion
                    parametro = From P In Data.P_007_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_007, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCliente
                    parametro = From P In Data.P_008_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_008, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.EstadoPoderes

                    parametro = From P In Data.P_0010_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0010, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoDocumento
                    parametro = From P In Data.P_0011_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0011, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.TipoOperacion
                    parametro = From P In Data.P_0012_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0012, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CartaTipo
                    parametro = From P In Data.P_0015_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0015, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Zonas
                    parametro = From P In Data.P_0017_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0017, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FormasDePagos
                    parametro = From P In Data.P_0018_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0018, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Sistemas
                    parametro = From P In Data.P_0020_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0020, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.TipoPagare
                    parametro = From P In Data.P_0021_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0021, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoPagare
                    parametro = From P In Data.P_0022_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0022, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.Moneda
                    parametro = From P In Data.P_0023_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0023, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGarantia
                    parametro = From P In Data.P_0024_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0024, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.RegimenMatrimonial
                    parametro = From P In Data.P_0025_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0025, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoAval
                    parametro = From P In Data.P_0026_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0026, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoAval
                    parametro = From P In Data.P_0027_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0027, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoSolicitudLinea
                    parametro = From P In Data.P_0028_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0028, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoLinea

                    parametro = From P In Data.P_0029_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0029, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoOperacion
                    parametro = From P In Data.P_0030_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0030, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoDocumento
                    parametro = From P In Data.P_0031_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0031, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGastoOperacional
                    parametro = From P In Data.P_0036_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0036, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoVerificacion
                    parametro = From P In Data.P_0040_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0040, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCuentasXCobrar
                    parametro = From P In Data.p_0041_cls Where P.pnu_est = "A" _
                                Select Codigo = P.id_P_0041, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCliente
                    parametro = From P In Data.P_0044_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0044, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEjecutivo
                    parametro = From P In Data.P_0045_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0045, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoEjecutivo
                    parametro = From P In Data.P_0048_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0048, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoTelefono
                    parametro = From P In Data.P_0049_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0049, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGastoRecaudacion
                    parametro = From P In Data.P_0051_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0051, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoDctoPago
                    parametro = From P In Data.P_0052_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0052, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.QueSePaga
                    parametro = From P In Data.P_0053_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0053, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoIngreso
                    parametro = From P In Data.p_0054_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_p_0054, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.QueAPagar
                    parametro = From P In Data.P_0055_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0055, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEgreso
                    parametro = From P In Data.P_0056_cls Where P.pnu_est = "A" And (P.pnu_atr_006 = "B" Or P.pnu_atr_006 = "A") _
                                       Select Codigo = P.id_P_0056, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CategoriaRiesgo
                    parametro = From P In Data.P_0058_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0058, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoFactura
                    parametro = From P In Data.P_0060_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0060, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.MotivosDeProtestos
                    parametro = From P In Data.P_0061_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0061, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FacultadesPoder
                    parametro = From P In Data.P_0062_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0062, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.RazonesSociales
                    parametro = From P In Data.P_0063_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0063, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ActividadEconomica
                    parametro = From P In Data.P_0064_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0064, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoRiesgo
                    parametro = From P In Data.P_0065_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0065, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEnvioInformacion
                    parametro = From P In Data.P_0067_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0067, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FormaEnvio
                    parametro = From P In Data.P_0068_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0068, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoClasificacion
                    parametro = From P In Data.P_0069_cls _
                                       Select Codigo = P.id_P_0069, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Pais
                    parametro = From P In Data.P_0070_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0070, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro
                    parametro = From P In Data.P_0071_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0071, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro1
                    parametro = From P In Data.P_0072_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0072, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro2
                    parametro = From P In Data.P_0073_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0073, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro3
                    parametro = From P In Data.P_0074_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0074, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoOperacionContable
                    parametro = From P In Data.P_0075_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0075, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Segmentos
                    parametro = From P In Data.P_0076_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0076, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoBeneficiario
                    parametro = From P In Data.P_0077_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0077, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ActuacionApoderado
                    parametro = From P In Data.P_0078_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0078, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ZonaRecaudacion
                    parametro = From P In Data.P_0080_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0080, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.plataformas
                    parametro = From P In Data.P_0081_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0081, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoNegociacion
                    parametro = From P In Data.P_0082_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0082, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ObjetivoCredito
                    parametro = From P In Data.P_0083_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0083, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Ciudad
                    parametro = From P In Data.P_0084_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0084, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Meses
                    parametro = From P In Data.P_0085_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0085, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadosCuentas
                    parametro = From P In Data.P_0086_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0086, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.OrigenFondo
                    parametro = From P In Data.P_0087_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0087, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEnvio
                    parametro = From P In Data.P_0088_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0088, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoOpeNegociacion
                    parametro = From P In Data.P_0089_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0089, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCobNeg
                    parametro = From P In Data.P_0090_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0090, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCartas
                    parametro = From P In Data.P_0091_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0091, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ParametroConsulta
                    parametro = From P In Data.P_0099_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0099, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoProductos
                    parametro = From P In Data.P_0100_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0100, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoComisionFactoringElectronico
                    parametro = From P In Data.P_0101_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0101, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ParametrosTipoProvisiones
                    parametro = From P In Data.P_0102_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0102, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoNoRecaudado
                    parametro = From P In Data.P_0103_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0103, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CaracteristicaOperación
                    parametro = From P In Data.P_0104_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0104, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoLineaFogape
                    parametro = From P In Data.P_0105_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0105, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoDevolucion
                    parametro = From P In Data.P_0106_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0106, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CargaMasivaDocumento
                    parametro = From P In Data.P_0107_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0107, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CargaMasivaPagoCliente
                    parametro = From P In Data.P_0108_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0108, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CargaMasivaPagoDeudor
                    parametro = From P In Data.P_0109_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0109, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoEvaluacion
                    parametro = From P In Data.P_0110_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0110, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCondicion
                    parametro = From P In Data.p_0111_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_p_0111, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.Custodia
                    parametro = From P In Data.P_0112_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0112, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCheque
                    parametro = From P In Data.p_0113_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_p_0113, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoIdentificacion
                    parametro = From P In Data.P_0119_cls _
                                       Select Codigo = P.id_0119, Descripcion = P.pnu_des

                Case TablaParametro.InformesPorMail
                    parametro = From P In Data.P_0300_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0300, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.HorarioInformesPorMail
                    parametro = From P In Data.P_0301_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0301, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.UsuariosNominaDiariaNegocios
                    parametro = From P In Data.P_0302_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0302, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoServicioLlamada
                    parametro = From P In Data.P_0303_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0303, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EnvioPorEmail
                    parametro = From P In Data.P_0304_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0304, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.SaludosEnvioEmail
                    parametro = From P In Data.P_0305_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0305, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TextoEnvioEmail
                    parametro = From P In Data.P_0306_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0306, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.MensajeDespedidaEnvioEmail
                    parametro = From P In Data.P_0307_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0307, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.MensajePublicidadEnvioEmail
                    parametro = From P In Data.P_0308_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0308, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoUsuarios
                    parametro = From P In Data.P_0309_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0309, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCierreContable
                    parametro = From P In Data.P_0310_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0310, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ClasificacionCliente
                    parametro = From P In Data.P_0118_cls Where P.pnu_est = "A" _
                                       Select Codigo = P.id_P_0118, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCuenta
                    parametro = From P In Data.P_0312_cls Where P.pnu_est = "A" _
                      Select Codigo = P.id_P_0312, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CORASU
                    parametro = From p In Data.P_0313_cls Where p.pnu_est = "A" _
                                Select Codigo = p.id_P_0313, Descripcion = p.pnu_des, Estado = p.pnu_est
            End Select

            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp)

            Else

                Dim Coll As New Collection

                For Each P In parametro
                    Coll.Add(P)
                Next

                Return Coll

            End If




        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ParametrosDevuelveTodos(ByVal CodigoTabla As TablaParametro, Optional ByVal LlenaDrop As Boolean = False, Optional ByVal Dp As DropDownList = Nothing) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Parametros que esten activos y segun codigo de tabla 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object
            Select Case CodigoTabla

                Case TablaParametro.Region

                    parametro = From P In Data.P_001_cls _
                                      Select Codigo = P.id_P_001, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ComunaLocalidad
                    parametro = From P In Data.P_002_cls _
                                       Select Codigo = P.id_P_002, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoDeudor
                    parametro = From P In Data.P_003_cls _
                                       Select Codigo = P.id_P_003, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ModoOperacion
                    parametro = From P In Data.P_007_cls _
                                       Select Codigo = P.id_P_007, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCliente
                    parametro = From P In Data.P_008_cls _
                                       Select Codigo = P.id_P_008, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.EstadoPoderes

                    parametro = From P In Data.P_0010_cls _
                                       Select Codigo = P.id_P_0010, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoDocumento
                    parametro = From P In Data.P_0011_cls _
                                       Select Codigo = P.id_P_0011, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.TipoOperacion

                    parametro = From P In Data.P_0012_cls _
                                       Select Codigo = P.id_P_0012, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CartaTipo

                    parametro = From P In Data.P_0015_cls _
                                       Select Codigo = P.id_P_0015, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Zonas

                    parametro = From P In Data.P_0017_cls _
                                       Select Codigo = P.id_P_0017, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FormasDePagos

                    parametro = From P In Data.P_0018_cls _
                                       Select Codigo = P.id_P_0018, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Sistemas


                    parametro = From P In Data.P_0020_cls _
                                       Select Codigo = P.id_P_0020, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.TipoPagare

                    parametro = From P In Data.P_0021_cls _
                                       Select Codigo = P.id_P_0021, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoPagare

                    parametro = From P In Data.P_0022_cls _
                                       Select Codigo = P.id_P_0022, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.Moneda
                    parametro = From P In Data.P_0023_cls _
                                       Select Codigo = P.id_P_0023, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGarantia

                    parametro = From P In Data.P_0024_cls _
                                       Select Codigo = P.id_P_0024, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.RegimenMatrimonial

                    parametro = From P In Data.P_0025_cls _
                                       Select Codigo = P.id_P_0025, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.TipoAval

                    parametro = From P In Data.P_0026_cls _
                                       Select Codigo = P.id_P_0026, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.EstadoAval

                    parametro = From P In Data.P_0027_cls _
                                       Select Codigo = P.id_P_0027, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.EstadoSolicitudLinea

                    parametro = From P In Data.P_0028_cls _
                                       Select Codigo = P.id_P_0028, Descripcion = P.pnu_des, Estado = P.pnu_est
                Case TablaParametro.EstadoLinea

                    parametro = From P In Data.P_0029_cls _
                                       Select Codigo = P.id_P_0029, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoOperacion

                    parametro = From P In Data.P_0030_cls _
                                       Select Codigo = P.id_P_0030, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoDocumento

                    parametro = From P In Data.P_0031_cls _
                                       Select Codigo = P.id_P_0031, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGastoOperacional

                    parametro = From P In Data.P_0036_cls _
                                       Select Codigo = P.id_P_0036, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadoVerificacion

                    parametro = From P In Data.P_0040_cls _
                                       Select Codigo = P.id_P_0040, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoCuentasXCobrar

                    parametro = From P In Data.p_0041_cls _
                                       Select Codigo = P.id_P_0041, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoCliente
                    parametro = From P In Data.P_0044_cls _
                                       Select Codigo = P.id_P_0044, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.TipoEjecutivo
                    parametro = From P In Data.P_0045_cls _
                                       Select Codigo = P.id_P_0045, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.EstadoEjecutivo
                    parametro = From P In Data.P_0048_cls _
                                       Select Codigo = P.id_P_0048, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.TipoTelefono
                    parametro = From P In Data.P_0049_cls _
                                       Select Codigo = P.id_P_0049, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoGastoRecaudacion
                    parametro = From P In Data.P_0051_cls _
                                       Select Codigo = P.id_P_0051, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadoDctoPago
                    parametro = From P In Data.P_0052_cls _
                                       Select Codigo = P.id_P_0052, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.QueSePaga
                    parametro = From P In Data.P_0053_cls _
                                       Select Codigo = P.id_P_0053, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoIngreso
                    parametro = From P In Data.p_0054_cls _
                                       Select Codigo = P.id_p_0054, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.QueAPagar
                    parametro = From P In Data.P_0055_cls _
                                       Select Codigo = P.id_P_0055, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEgreso
                    parametro = From P In Data.P_0056_cls _
                                       Select Codigo = P.id_P_0056, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CategoriaRiesgo
                    parametro = From P In Data.P_0058_cls _
                                       Select Codigo = P.id_P_0058, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoFactura
                    parametro = From P In Data.P_0060_cls _
                                       Select Codigo = P.id_P_0060, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.MotivosDeProtestos
                    parametro = From P In Data.P_0061_cls _
                                       Select Codigo = P.id_P_0061, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FacultadesPoder
                    parametro = From P In Data.P_0062_cls _
                                       Select Codigo = P.id_P_0062, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.RazonesSociales
                    parametro = From P In Data.P_0063_cls _
                                       Select Codigo = P.id_P_0063, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.ActividadEconomica
                    parametro = From P In Data.P_0064_cls _
                                       Select Codigo = P.id_P_0064, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoRiesgo
                    parametro = From P In Data.P_0065_cls _
                                       Select Codigo = P.id_P_0065, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoEnvioInformacion
                    parametro = From P In Data.P_0067_cls _
                                       Select Codigo = P.id_P_0067, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.FormaEnvio
                    parametro = From P In Data.P_0068_cls _
                                       Select Codigo = P.id_P_0068, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Pais
                    parametro = From P In Data.P_0070_cls _
                                       Select Codigo = P.id_P_0070, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro
                    parametro = From P In Data.P_0071_cls _
                                       Select Codigo = P.id_P_0071, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.otro1
                    parametro = From P In Data.P_0072_cls _
                                       Select Codigo = P.id_P_0072, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.otro2
                    parametro = From P In Data.P_0073_cls _
                                       Select Codigo = P.id_P_0073, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.otro3


                    parametro = From P In Data.P_0074_cls _
                                       Select Codigo = P.id_P_0074, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoOperacionContable


                    parametro = From P In Data.P_0075_cls _
                                       Select Codigo = P.id_P_0075, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.Segmentos


                    parametro = From P In Data.P_0076_cls _
                                       Select Codigo = P.id_P_0076, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoBeneficiario


                    parametro = From P In Data.P_0077_cls _
                                       Select Codigo = P.id_P_0077, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ActuacionApoderado


                    parametro = From P In Data.P_0078_cls _
                                       Select Codigo = P.id_P_0078, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.ZonaRecaudacion


                    parametro = From P In Data.P_0080_cls _
                                       Select Codigo = P.id_P_0080, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.plataformas


                    parametro = From P In Data.P_0081_cls _
                                       Select Codigo = P.id_P_0081, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadoNegociacion


                    parametro = From P In Data.P_0082_cls _
                                       Select Codigo = P.id_P_0082, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ObjetivoCredito


                    parametro = From P In Data.P_0083_cls _
                                       Select Codigo = P.id_P_0083, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.Ciudad


                    parametro = From P In Data.P_0084_cls _
                                       Select Codigo = P.id_P_0084, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Meses


                    parametro = From P In Data.P_0085_cls _
                                       Select Codigo = P.id_P_0085, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadosCuentas


                    parametro = From P In Data.P_0086_cls _
                                       Select Codigo = P.id_P_0086, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.OrigenFondo


                    parametro = From P In Data.P_0087_cls _
                                       Select Codigo = P.id_P_0087, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.TipoEnvio


                    parametro = From P In Data.P_0088_cls _
                                       Select Codigo = P.id_P_0088, Descripcion = P.pnu_des, Estado = P.pnu_est



                Case TablaParametro.EstadoOpeNegociacion


                    parametro = From P In Data.P_0089_cls _
                                       Select Codigo = P.id_P_0089, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadoCobNeg


                    parametro = From P In Data.P_0090_cls _
                                       Select Codigo = P.id_P_0090, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoCartas


                    parametro = From P In Data.P_0091_cls _
                                       Select Codigo = P.id_P_0091, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ParametroConsulta


                    parametro = From P In Data.P_0099_cls _
                                       Select Codigo = P.id_P_0099, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoProductos


                    parametro = From P In Data.P_0100_cls _
                                       Select Codigo = P.id_P_0100, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoComisionFactoringElectronico


                    parametro = From P In Data.P_0101_cls _
                                       Select Codigo = P.id_P_0101, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ParametrosTipoProvisiones


                    parametro = From P In Data.P_0102_cls _
                                       Select Codigo = P.id_P_0102, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoNoRecaudado


                    parametro = From P In Data.P_0103_cls _
                                       Select Codigo = P.id_P_0103, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CaracteristicaOperación


                    parametro = From P In Data.P_0104_cls _
                                       Select Codigo = P.id_P_0104, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EstadoLineaFogape


                    parametro = From P In Data.P_0105_cls _
                                       Select Codigo = P.id_P_0105, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoDevolucion


                    parametro = From P In Data.P_0106_cls _
                                       Select Codigo = P.id_P_0106, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CargaMasivaDocumento


                    parametro = From P In Data.P_0107_cls _
                                       Select Codigo = P.id_P_0107, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CargaMasivaPagoCliente


                    parametro = From P In Data.P_0108_cls _
                                       Select Codigo = P.id_P_0108, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.CargaMasivaPagoDeudor


                    parametro = From P In Data.P_0109_cls _
                                       Select Codigo = P.id_P_0109, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoEvaluacion

                    parametro = From P In Data.P_0110_cls _
                                       Select Codigo = P.id_P_0110, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoCondicion

                    parametro = From P In Data.p_0111_cls _
                                       Select Codigo = P.id_p_0111, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Custodia

                    parametro = From P In Data.P_0112_cls _
                                       Select Codigo = P.id_P_0112, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.InformesPorMail


                    parametro = From P In Data.P_0300_cls _
                                       Select Codigo = P.id_P_0300, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.HorarioInformesPorMail


                    parametro = From P In Data.P_0301_cls _
                                       Select Codigo = P.id_P_0301, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.UsuariosNominaDiariaNegocios


                    parametro = From P In Data.P_0302_cls _
                                       Select Codigo = P.id_P_0302, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TipoServicioLlamada


                    parametro = From P In Data.P_0303_cls _
                                       Select Codigo = P.id_P_0303, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.EnvioPorEmail


                    parametro = From P In Data.P_0304_cls _
                                       Select Codigo = P.id_P_0304, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.SaludosEnvioEmail


                    parametro = From P In Data.P_0305_cls _
                                       Select Codigo = P.id_P_0305, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.TextoEnvioEmail


                    parametro = From P In Data.P_0306_cls _
                                       Select Codigo = P.id_P_0306, Descripcion = P.pnu_des, Estado = P.pnu_est


                Case TablaParametro.MensajeDespedidaEnvioEmail


                    parametro = From P In Data.P_0307_cls _
                                       Select Codigo = P.id_P_0307, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.MensajePublicidadEnvioEmail


                    parametro = From P In Data.P_0308_cls _
                                       Select Codigo = P.id_P_0308, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.EstadoUsuarios


                    parametro = From P In Data.P_0309_cls _
                                       Select Codigo = P.id_P_0309, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCierreContable


                    parametro = From P In Data.P_0310_cls _
                                       Select Codigo = P.id_P_0310, Descripcion = P.pnu_des, Estado = P.pnu_est




                Case TablaParametro.TipoClasificacion
                    parametro = From P In Data.P_0069_cls _
                                       Select Codigo = P.id_P_0069, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.Niveles
                    parametro = From P In Data.P_005_cls _
                                       Select Codigo = P.id_P_005, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.ClasificacionCliente
                    parametro = From P In Data.P_0118_cls _
                                       Select Codigo = P.id_P_0118, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.TipoCuenta
                    parametro = From P In Data.P_0312_cls _
                                Select Codigo = P.id_P_0312, Descripcion = P.pnu_des, Estado = P.pnu_est

                Case TablaParametro.CORASU
                    parametro = From p In Data.P_0313_cls _
                                Select Codigo = p.id_P_0313, Descripcion = p.pnu_des, Estado = p.pnu_est
            End Select

            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp)

            Else

                Return parametro

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ParametrosAlfanumericoDevuelve(ByVal CodigoTabla As TablaAlfanumerico, Optional ByVal LlenaDrop As Boolean = False, Optional ByVal Dp As DropDownList = Nothing) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Parametros Alfanumericos que esten activos y segun codigo de tabla 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 23/05/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  02/02/2009         se Agrego otros campos (estado y sistema)  
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object

            Select Case CodigoTabla

                Case TablaAlfanumerico.Giro
                    parametro = From P In Data.PL_000006_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000006.Trim(), Descripcion = P.pal_des, est = P.pal_est, sistema = P.pal_sis

                Case TablaAlfanumerico.Plazas
                    parametro = From P In Data.PL_000047_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000047, Descripcion = P.pal_des, est = P.pal_est, sistema = P.pal_sis

                Case TablaAlfanumerico.BancaCliente
                    parametro = From P In Data.PL_000066_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000066, Descripcion = P.pal_des, est = P.pal_est, sistema = P.pal_sis

                Case TablaAlfanumerico.Factoring
                    parametro = From P In Data.PL_000069_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000069, Descripcion = P.pal_des, est = P.pal_est, sistema = P.pal_sis

            End Select

            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp)

            Else

                Return parametro

            End If




        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub SucursalesDevuelvePorBanco(ByVal CodigoBanco As Integer, ByVal Dp As DropDownList)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las sucursales activas  por Codigo de Banco
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Sucursales = From Sbc In Data.sbc_cls Where Sbc.id_bco = CodigoBanco _
                             Select Codigo = Sbc.id_sbc, _
                                       Descripcion = Sbc.sbc_des, _
                                       pal_pla_cod = Sbc.id_pl_000047


            Dim RG As New FuncionesGenerales.RutinasWeb
            RG.Llenar_Drop(Sucursales, "Codigo", "Descripcion", Dp)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub CiudadDevuelve(Optional ByVal LlenaDrop As Boolean = True, Optional ByVal Dp As DropDownList = Nothing)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las ciudades
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Ciudades = From P In Data.ciu_cls Order By P.ciu_des _
                                    Select CodigoCiudad = P.id_ciu, _
                                           NombreCiudad = P.ciu_des

            '            NombreCiudad = P.P_0084_cls.pnu_des


            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Ciudades, "CodigoCiudad", "NombreCiudad", Dp)

            Else
                'Return Ciudades
            End If


        Catch ex As Exception

        End Try

    End Sub

    Public Function CiudadDevuelve(ByVal id_ciudad As Integer) As ciu_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las ciudades
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Ciudades As ciu_cls = (From P In Data.ciu_cls Where P.id_ciu = id_ciudad).First

            Return Ciudades

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ComunaDevuelve(ByVal CodigoCiudad As Integer, Optional ByVal LlenaDrop As Boolean = True, Optional ByVal Dp As DropDownList = Nothing) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las comunas asociadas a una ciudad
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 19/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Comuna = From Cmn In Data.cmn_cls Where Cmn.id_ciu = CodigoCiudad Order By Cmn.cmn_des _
                                    Select CodigoComuna = Cmn.id_cmn, _
                                              CodCiudad = Cmn.id_ciu, _
                                              Zona = Cmn.id_zon, _
                                              NombreComuna = Cmn.cmn_des

            If LlenaDrop Then
                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Comuna, "CodigoComuna", "NombreComuna", Dp)
                Return Comuna
            Else
                Return Comuna
            End If


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub SexoDevuelve(ByVal Dp As DropDownList)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los tipos de sexo
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 23/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim RG As New FuncionesGenerales.RutinasWeb
            Dim Sexo_Coll As New Collection
            Dim Sexo As FuncionesGenerales.Class_LlenaCombo

            Sexo = New FuncionesGenerales.Class_LlenaCombo("M", "Masculino")
            Sexo_Coll.Add(Sexo)

            Sexo = New FuncionesGenerales.Class_LlenaCombo("F", "Femenino")
            Sexo_Coll.Add(Sexo)

            RG.Llenar_Drop(Sexo_Coll, "Codigo", "Descripcion", Dp)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub CarteraClienteDevuelve(ByVal TipoConsulta As Int16, ByVal Dp As DropDownList, ByVal TipoCartera As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los tipos de cartera cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 23/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Carteras As Object
            Dim RG As New FuncionesGenerales.RutinasWeb


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            If TipoConsulta = 1 Then
                Carteras = From C In Data.crt_cls _
                Select C.id_crt, C.crt_ctd_dia, C.crt_des
            Else
                Carteras = From C In Data.crt_cls Where C.id_crt = TipoCartera _
                Select C.id_crt, C.crt_ctd_dia, C.crt_des
            End If

            RG.Llenar_Drop(Carteras, "id_crt", "crt_des", Dp)



        Catch ex As Exception

        End Try

    End Sub

    Public Sub ZonasDevuelveTodas(ByVal CodigoSucursal As String, Optional ByVal LlenaDrop As Boolean = True, Optional ByVal Dp As DropDownList = Nothing)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las zonas asociadas a una sucursal
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 25/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Zonas = From Z In Data.zon_cls Where Z.id_suc = CodigoSucursal

            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Zonas, "id_zon", "zon_des", Dp)

            Else
                'Return Ciudades
            End If


        Catch ex As Exception

        End Try

    End Sub

    Public Function ZonasComunaDevuelve(ByVal CodigoSucursal As String, ByVal CodigoComuna As Integer) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las zonas asociados a una comuna y sucursal
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 25/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion  
        'S. Henriquez C.            12/09/2012          Se corrige criterio de busqueda, codigo sucursal, estaba validando contra id_zon
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Zonas = From zon1 In Data.zon_cls Join cmn1 In Data.cmn_cls On cmn1.id_zon Equals zon1.id_zon _
                         Where cmn1.id_cmn = CodigoComuna And zon1.id_suc = CodigoSucursal _
                         Select zon1

            'Dim Zonas = From C In Data.cmn_cls Where C.id_cmn = CodigoComuna And C.zon_cls.id_suc = CodigoSucursal

            Return Zonas

        Catch ex As Exception

        End Try

    End Function

    Public Function ParametrosAlfanumericoDevuelveCodigos(ByVal CodigoTabla As TablaAlfanumerico, Optional ByVal LlenaDrop As Boolean = False, Optional ByVal Dp As DropDownList = Nothing) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Parametros Alfanumericos que esten activos y segun codigo de tabla 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 23/05/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object

            Select Case CodigoTabla

                Case TablaAlfanumerico.Giro
                    parametro = From P In Data.PL_000006_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000006, Descripcion = P.pal_des

                Case TablaAlfanumerico.Plazas
                    parametro = From P In Data.PL_000047_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000047, Descripcion = P.pal_des

                Case TablaAlfanumerico.BancaCliente
                    parametro = From P In Data.PL_000066_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000066, Descripcion = P.pal_des

                Case TablaAlfanumerico.Factoring
                    parametro = From P In Data.PL_000066_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000066, Descripcion = P.pal_des

            End Select

            If LlenaDrop Then

                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(parametro, "Descripcion", "Codigo", Dp)

            Else

                Return parametro

            End If




        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Parametros_Detalle_Devuelve(ByVal CodigoTabla As TablaParametro, ByVal CodigoParametro As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Parametros que esten activos y segun codigo de tabla 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     09-03-2009          se cambia el nombre de la funcion por uno mas descriptivo 
        '                                               (Detalle_Parametros_Devuelve por Parametros_Detalle_Devuelve)
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object
            Dim col As New Collection
            Select Case CodigoTabla


                Case TablaParametro.Region
                    parametro = From P In Data.P_001_cls Where P.id_P_001 = CodigoParametro _
                                      Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_001_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next
                Case TablaParametro.ComunaLocalidad

                    parametro = From P In Data.P_002_cls Where P.id_P_002 = CodigoParametro _
                                      Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_002_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next
                Case TablaParametro.EstadoDeudor

                    parametro = From P In Data.P_003_cls Where P.id_P_003 = CodigoParametro _
                    Select P.pnu_est, P.pnu_atr_003, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_003_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_atr_003 = p.pnu_atr_003
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next
                Case TablaParametro.Niveles
                    parametro = From P In Data.P_005_cls Where P.id_P_005 = CodigoParametro _
                                    Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_005_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next
                Case TablaParametro.ModoOperacion

                    parametro = From P In Data.P_007_cls Where P.id_P_007 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_007_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoCliente

                    parametro = From P In Data.P_008_cls Where P.id_P_008 = CodigoParametro _
                                                  Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_008_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next
                Case TablaParametro.EstadoPoderes

                    parametro = From P In Data.P_0010_cls Where P.id_P_0010 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0010_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next
                Case TablaParametro.EstadoDocumento

                    parametro = From P In Data.P_0011_cls Where P.id_P_0011 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0011_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoOperacion

                    parametro = From P In Data.P_0012_cls Where P.id_P_0012 = CodigoParametro _
                                                  Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0012_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.CartaTipo

                    parametro = From P In Data.P_0015_cls Where P.id_P_0015 = CodigoParametro _
                                                   Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0015_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.Zonas

                    parametro = From P In Data.P_0017_cls Where P.id_P_0017 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0017_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.FormasDePagos

                    parametro = From P In Data.P_0018_cls Where P.id_P_0018 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0018_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.Sistemas

                    parametro = From P In Data.P_0020_cls Where P.id_P_0020 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0020_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoPagare

                    parametro = From P In Data.P_0021_cls Where P.id_P_0021 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0021_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoPagare

                    parametro = From P In Data.P_0022_cls Where P.id_P_0022 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0022_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.Moneda

                    parametro = From P In Data.P_0023_cls Where P.id_P_0023 = CodigoParametro _
                               Select P.pnu_est, P.pnu_atr_003, P.pnu_atr_004, P.pnu_atr_005, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0023_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_atr_003 = p.pnu_atr_003
                        aux.pnu_atr_004 = p.pnu_atr_004
                        aux.pnu_atr_005 = p.pnu_atr_005
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoGarantia

                    parametro = From P In Data.P_0024_cls Where P.id_P_0024 = CodigoParametro _
           Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0024_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.RegimenMatrimonial

                    parametro = From P In Data.P_0025_cls Where P.id_P_0025 = CodigoParametro _
                               Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0025_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoAval

                    parametro = From P In Data.P_0026_cls Where P.id_P_0026 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0026_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoAval

                    parametro = From P In Data.P_0027_cls Where P.id_P_0027 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0027_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoSolicitudLinea


                    parametro = From P In Data.P_0028_cls Where P.id_P_0028 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0028_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoLinea

                    parametro = From P In Data.P_0029_cls Where P.id_P_0029 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0029_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoOperacion

                    parametro = From P In Data.P_0030_cls Where P.id_P_0030 = CodigoParametro _
                     Select Estado = P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0030_cls
                        aux.pnu_est = p.Estado
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoDocumento

                    parametro = From P In Data.P_0031_cls Where P.id_P_0031 = CodigoParametro




                    For Each p In parametro
                        Dim aux As New P_0031_cls
                        aux = p


                        col.Add(aux)
                    Next

                Case TablaParametro.TipoGastoOperacional

                    parametro = From P In Data.P_0036_cls Where P.id_P_0036 = CodigoParametro _
                       Select Estado = P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0036_cls
                        aux.pnu_est = p.Estado
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next


                Case TablaParametro.EstadoVerificacion

                    parametro = From P In Data.P_0040_cls Where P.id_P_0040 = CodigoParametro _
                    Select Estado = P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0040_cls
                        aux.pnu_est = p.estado
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoCuentasXCobrar
                    parametro = From P In Data.p_0041_cls Where P.id_P_0041 = CodigoParametro _
                    Select P.pnu_est, P.pnu_atr_002, P.pnu_atr_003, P.pnu_atr_004, P.pnu_atr_005, P.pnu_des

                    For Each p In parametro
                        Dim aux As New p_0041_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_002 = p.pnu_atr_002
                        aux.pnu_atr_003 = p.pnu_atr_003
                        aux.pnu_atr_004 = p.pnu_atr_004
                        aux.pnu_atr_005 = p.pnu_atr_005


                        col.Add(aux)
                    Next

                Case TablaParametro.TipoCliente
                    parametro = From P In Data.P_0044_cls Where P.id_P_0044 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des, P.pnu_atr_001

                    For Each p In parametro
                        Dim aux As New P_0044_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_001 = p.pnu_atr_001
                        col.Add(aux)
                    Next


                Case TablaParametro.TipoEjecutivo

                    parametro = From P In Data.P_0045_cls Where P.id_P_0045 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0045_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next



                Case TablaParametro.EstadoEjecutivo

                    parametro = From P In Data.P_0048_cls Where P.id_P_0048 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0048_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.TipoTelefono

                    parametro = From P In Data.P_0049_cls Where P.id_P_0049 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0049_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoGastoRecaudacion

                    parametro = From P In Data.P_0051_cls Where P.id_P_0051 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0051_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.EstadoDctoPago

                    parametro = From P In Data.P_0052_cls Where P.id_P_0052 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0052_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.QueSePaga

                    parametro = From P In Data.P_0053_cls Where P.id_P_0053 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0053_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next


                Case TablaParametro.TipoIngreso

                    parametro = From P In Data.p_0054_cls Where P.id_p_0054 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des, P.pnu_atr_001, P.pnu_atr_002, P.pnu_atr_003, P.pnu_atr_004, P.pnu_atr_005

                    For Each p In parametro
                        Dim aux As New p_0054_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_001 = p.pnu_atr_001
                        aux.pnu_atr_002 = p.pnu_atr_002
                        aux.pnu_atr_003 = p.pnu_atr_003
                        aux.pnu_atr_004 = p.pnu_atr_004
                        aux.pnu_atr_005 = p.pnu_atr_005

                        col.Add(aux)
                    Next

                Case TablaParametro.QueAPagar

                    parametro = From P In Data.P_0055_cls Where P.id_P_0055 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0055_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoEgreso

                    parametro = From P In Data.P_0056_cls Where P.id_P_0056 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des, P.pnu_atr_002, P.pnu_atr_003, P.pnu_atr_004, P.pnu_atr_005, P.pnu_atr_006, P.pnu_atr_007

                    For Each p In parametro

                        Dim aux As New P_0056_cls

                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_002 = p.pnu_atr_002
                        aux.pnu_atr_003 = p.pnu_atr_003
                        aux.pnu_atr_004 = p.pnu_atr_004
                        aux.pnu_atr_005 = p.pnu_atr_005
                        aux.pnu_atr_006 = p.pnu_atr_006
                        aux.pnu_atr_007 = p.pnu_atr_007

                        col.Add(aux)

                    Next

                Case TablaParametro.CategoriaRiesgo

                    parametro = From P In Data.P_0058_cls Where P.id_P_0058 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0058_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoFactura

                    parametro = From P In Data.P_0060_cls Where P.id_P_0060 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0060_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.MotivosDeProtestos

                    parametro = From P In Data.P_0061_cls Where P.id_P_0061 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0061_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.FacultadesPoder

                    parametro = From P In Data.P_0062_cls Where P.id_P_0062 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0062_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.RazonesSociales

                    parametro = From P In Data.P_0063_cls Where P.id_P_0063 = CodigoParametro _
                                Select P.pnu_est, P.pnu_atr_007, P.pnu_des

                    'Select Estado = P.pnu_est, P.pnu_abv

                    For Each p In parametro
                        Dim aux As New P_0063_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_007 = p.pnu_atr_007
                        col.Add(aux)
                    Next

                Case TablaParametro.ActividadEconomica

                    parametro = From P In Data.P_0064_cls Where P.id_P_0064 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0064_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoRiesgo

                    parametro = From P In Data.P_0065_cls Where P.id_P_0065 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des, P.pnu_atr_002

                    For Each p In parametro
                        Dim aux As New P_0065_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_002 = p.pnu_atr_002
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoEnvioInformacion

                    parametro = From P In Data.P_0067_cls Where P.id_P_0067 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0067_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.FormaEnvio

                    parametro = From P In Data.P_0068_cls Where P.id_P_0068 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0068_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.Pais

                    parametro = From P In Data.P_0070_cls Where P.id_P_0070 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0070_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.otro
                    parametro = From P In Data.P_0071_cls Where P.id_P_0071 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0071_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.otro1

                    parametro = From P In Data.P_0072_cls Where P.id_P_0072 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0072_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.otro2

                    parametro = From P In Data.P_0073_cls Where P.id_P_0073 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0073_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.otro3

                    parametro = From P In Data.P_0074_cls Where P.id_P_0074 = CodigoParametro _
                                        Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0074_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoOperacionContable


                    parametro = From P In Data.P_0075_cls Where P.id_P_0075 = CodigoParametro _
                      Select P.pnu_est, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0075_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.Segmentos
                    parametro = From P In Data.P_0076_cls Where P.id_P_0076 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0076_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoBeneficiario
                    parametro = From P In Data.P_0077_cls Where P.id_P_0077 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0077_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_est = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.ActuacionApoderado
                    parametro = From P In Data.P_0078_cls Where P.id_P_0078 = CodigoParametro _
                      Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0078_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.Contratos
                    parametro = From p In Data.P_0079_cls Where p.id_P_0079 = CodigoParametro _
                    Select p.pnu_est, p.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0079_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.ZonaRecaudacion

                    parametro = From P In Data.P_0080_cls Where P.id_P_0080 = CodigoParametro _
                      Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0080_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.plataformas


                    parametro = From P In Data.P_0081_cls Where P.id_P_0081 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0081_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoNegociacion

                    parametro = From P In Data.P_0082_cls Where P.id_P_0082 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0082_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.ObjetivoCredito

                    parametro = From P In Data.P_0083_cls Where P.id_P_0083 = CodigoParametro _
                      Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0083_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.Ciudad

                    parametro = From P In Data.P_0084_cls Where P.id_P_0084 = CodigoParametro _
                                        Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0084_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next

                Case TablaParametro.Meses


                    parametro = From P In Data.P_0085_cls Where P.id_P_0085 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0085_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.EstadosCuentas


                    parametro = From P In Data.P_0086_cls Where P.id_P_0086 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0086_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next


                Case TablaParametro.OrigenFondo

                    parametro = From P In Data.P_0087_cls Where P.id_P_0087 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0087_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next



                Case TablaParametro.TipoEnvio


                    parametro = From P In Data.P_0088_cls Where P.id_P_0088 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des


                    For Each p In parametro
                        Dim aux As New P_0088_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.EstadoOpeNegociacion


                    parametro = From P In Data.P_0089_cls Where P.id_P_0089 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0089_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoCobNeg

                    parametro = From P In Data.P_0090_cls Where P.id_P_0090 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0090_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoCartas

                    parametro = From P In Data.P_0091_cls Where P.id_P_0091 = CodigoParametro _
                                        Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0091_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.ParametroConsulta


                    parametro = From P In Data.P_0099_cls Where P.id_P_0099 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0099_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.TipoProductos


                    parametro = From P In Data.P_0100_cls Where P.id_P_0100 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0100_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoComisionFactoringElectronico

                    parametro = From P In Data.P_0101_cls Where P.id_P_0101 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0101_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.ParametrosTipoProvisiones

                    parametro = From P In Data.P_0102_cls Where P.id_P_0102 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0102_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoNoRecaudado

                    parametro = From P In Data.P_0103_cls Where P.id_P_0103 = CodigoParametro _
                       Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0103_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.CaracteristicaOperación

                    parametro = From P In Data.P_0104_cls Where P.id_P_0104 = CodigoParametro _
                       Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0104_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.EstadoLineaFogape

                    parametro = From P In Data.P_0105_cls Where P.id_P_0105 = CodigoParametro _
                                        Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0105_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.TipoDevolucion


                    parametro = From P In Data.P_0106_cls Where P.id_P_0106 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0106_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.CargaMasivaDocumento

                    parametro = From P In Data.P_0107_cls Where P.id_P_0107 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0107_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.CargaMasivaPagoCliente
                    parametro = From P In Data.P_0108_cls Where P.id_P_0108 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0108_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.CargaMasivaPagoDeudor
                    parametro = From P In Data.P_0109_cls Where P.id_P_0109 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0109_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoIdentificacion
                    parametro = From P In Data.P_0119_cls Where P.id_0119 = CodigoParametro _
                     Select P.pnu_des, P.pnu_cod_bco, P.pnu_vld_dig

                    For Each p In parametro

                        Dim aux As New P_0119_cls
                        aux.pnu_des = p.pnu_des
                        aux.pnu_cod_bco = p.pnu_cod_bco
                        aux.pnu_vld_dig = p.pnu_vld_dig

                        col.Add(aux)
                    Next



                Case TablaParametro.InformesPorMail

                    parametro = From P In Data.P_0300_cls Where P.id_P_0300 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0300_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next

                Case TablaParametro.HorarioInformesPorMail

                    parametro = From P In Data.P_0301_cls Where P.id_P_0301 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0301_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.UsuariosNominaDiariaNegocios

                    parametro = From P In Data.P_0302_cls Where P.id_P_0302 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0302_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoServicioLlamada

                    parametro = From P In Data.P_0303_cls Where P.id_P_0303 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0303_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.EnvioPorEmail
                    parametro = From P In Data.P_0304_cls Where P.id_P_0304 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0304_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next


                Case TablaParametro.SaludosEnvioEmail

                    parametro = From P In Data.P_0305_cls Where P.id_P_0305 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0305_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next


                Case TablaParametro.TextoEnvioEmail


                    parametro = From P In Data.P_0306_cls Where P.id_P_0306 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0306_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next


                Case TablaParametro.MensajeDespedidaEnvioEmail


                    parametro = From P In Data.P_0307_cls Where P.id_P_0307 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0307_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.MensajePublicidadEnvioEmail

                    parametro = From P In Data.P_0308_cls Where P.id_P_0308 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0308_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.EstadoUsuarios
                    parametro = From P In Data.P_0309_cls Where P.id_P_0309 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0309_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoCierreContable
                    parametro = From P In Data.P_0310_cls Where P.id_P_0310 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0310_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)
                    Next



                Case TablaParametro.TipoClasificacion

                    parametro = From P In Data.P_0069_cls Where P.id_P_0069 = CodigoParametro _
                    Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0069_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des


                        col.Add(aux)
                    Next
                Case TablaParametro.EstadoEvaluacion

                    parametro = From P In Data.P_0110_cls Where P.id_P_0110 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0110_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next
                Case TablaParametro.EstadoCondicion


                    parametro = From P In Data.p_0111_cls Where P.id_p_0111 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New p_0111_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.Custodia
                    parametro = From P In Data.P_0112_cls Where P.id_P_0112 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des
                    For Each p In parametro
                        Dim aux As New P_0112_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.ClasificacionCliente

                    parametro = From P In Data.P_0118_cls Where P.id_P_0118 = CodigoParametro _
                     Select P.pnu_est, P.pnu_des

                    For Each p In parametro
                        Dim aux As New P_0118_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        col.Add(aux)
                    Next

                Case TablaParametro.TipoCuenta

                    parametro = From P In Data.P_0312_cls Where P.id_P_0312 = CodigoParametro _
                                Select P.pnu_est, P.pnu_des

                    For Each p In parametro

                        Dim aux As New P_0312_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des

                        col.Add(aux)

                    Next

                Case TablaParametro.CORASU

                    parametro = From p In Data.P_0313_cls Where p.id_P_0313 = CodigoParametro _
                                Select p.pnu_est, p.pnu_des, p.pnu_atr_001

                    For Each p In parametro

                        Dim aux As New P_0313_cls
                        aux.pnu_est = p.pnu_est
                        aux.pnu_des = p.pnu_des
                        aux.pnu_atr_001 = p.pnu_atr_001

                        col.Add(aux)

                    Next

            End Select

            If col.Count > 0 Then
                Return col
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ParametrosAfanumericosDevuelveDetalle(ByVal CodigoTabla As TablaAlfanumerico, ByVal CodigoParametro As String) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los detales de los Parametros alfanumericos
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro As Object
            Dim coll As New Collection
            Select Case CodigoTabla
                Case TablaAlfanumerico.Giro
                    parametro = From p In data.PL_000006_cls Where p.id_PL_000006 = CodigoParametro _
                                 Select p.pal_des, p.pal_est, p.pal_sis
                    For Each p In parametro
                        Dim aux As New PL_000006_cls
                        aux.pal_des = p.pal_des
                        aux.pal_sis = p.pal_sis
                        coll.Add(aux)
                    Next

                Case TablaAlfanumerico.Plazas
                    parametro = From p In data.PL_000047_cls Where p.id_PL_000047 = CodigoParametro _
                                Select p.pal_est, p.pal_des, p.pal_sis
                    For Each p In parametro
                        Dim aux As New PL_000047_cls
                        aux.pal_est = p.pal_est
                        aux.pal_des = p.pal_des
                        aux.pal_sis = p.pal_sis
                        coll.Add(aux)
                    Next
                Case TablaAlfanumerico.BancaCliente
                    parametro = From p In data.PL_000066_cls Where p.id_PL_000066 = CodigoParametro _
                                    Select p.pal_est, p.pal_des, p.pal_sis
                    For Each p In parametro
                        Dim aux As New PL_000066_cls
                        aux.pal_est = p.pal_est
                        aux.pal_des = p.pal_des
                        aux.pal_sis = p.pal_sis
                        'aux = p
                        coll.Add(aux)
                    Next
                Case TablaAlfanumerico.Factoring

                    parametro = From p In data.PL_000069_cls Where p.id_PL_000069 = CodigoParametro _
                                    Select p.pal_des, p.pal_est, p.pal_sis
                    For Each p In parametro
                        Dim aux As New PL_000069_cls
                        aux.pal_des = p.pal_des
                        aux.pal_est = p.pal_est
                        aux.pal_sis = p.pal_sis
                        coll.Add(aux)

                    Next
            End Select

            If coll.Count > 0 Then
                Return coll

            End If



        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function MParametrosDevuelve(ByVal TipoPar As String, Optional ByVal LlenaDrop As Boolean = False, Optional ByVal Dp As DropDownList = Nothing) As Collection

        Try

            Dim parametro As Object

            Dim Data As New CapaDatos.DataClsFactoringDataContext



            parametro = From P In Data.mpar_cls Where P.tipo = TipoPar Order By P.Descripcion Ascending



            If LlenaDrop Then



                Dim RG As New FuncionesGenerales.RutinasWeb

                RG.Llenar_Drop(parametro, "id", "Descripcion", Dp)



            Else



                Dim Coll As New Collection



                For Each P In parametro

                    Coll.Add(P)

                Next



                Return Coll



            End If





        Catch ex As Exception

            Return Nothing

        End Try



    End Function

    Public Function Nomina_Retorno(ByVal GV As GridView, ByVal Fecha_desde As String, ByVal Fecha_hasta As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve nominas en ayuda de nominas
        'Creado por Victgor Alvarez
        'Fecha Creacion: 24/01/2011
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************


        Dim i As Integer
        Dim col As New Collection
        Dim Sesion As New ClsSession.ClsSession
        Try


            Dim Data As New DataClsFactoringDataContext
            Fecha_desde = Fecha_desde & " 00:00:01"
            Fecha_hasta = Fecha_hasta & " 23:59:59"

            Dim Nomina = (From N In Data.nma_cls Where N.nma_fec >= Fecha_desde And _
                                                       N.nma_fec <= Fecha_hasta _
                      Select New With {.id_nma = N.id_nma, _
                                       .nma_mto = N.nma_mto}).Skip(Sesion.NroPaginacion)


            For Each P In Nomina.Take(8)
                i = i + 1
                col.Add(P)

            Next

            GV.DataSource = col
            GV.DataBind()

            Return col

        Catch ex As Exception
            Return col
        End Try

    End Function

#End Region

#Region "Llena Grilla de Parametros"

    Public Function CiudadDevuelvePorSucursal(ByVal idSuc As Integer, ByVal llenagrid As Boolean, _
                                              Optional ByVal GV As GridView = Nothing) As Object

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Ciudad = From Z In Data.zon_cls Join C In Data.cmn_cls On Z.id_zon Equals C.id_zon _
                        Where Z.id_suc = idSuc _
                        Select New With {.id_ciu = C.id_ciu, _
                                        .ciu_des = C.ciu_cls.ciu_des, _
                                        .suc_des_cra = Z.suc_cls.suc_des_cra}

            'Dim Ciudad = From Ciu In Data.ciu_cls Where Ciu.id_suc = idSuc _
            '             Select New With {.id_ciu = Ciu.id_ciu, _
            '                            .ciu_des = Ciu.ciu_des, _
            '                            .suc_des_cra = Ciu.suc_cls.suc_nom}
            If llenagrid Then
                GV.DataSource = Ciudad
                GV.DataBind()
                Return Nothing
            Else
                Return Ciudad
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonaDevuelveXSuc(ByVal IdSuc As Integer, _
                                       ByVal Llenagrid As Boolean, _
                                       Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Zona Por Sucursal
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Zona = From z In data.zon_cls Where z.id_suc = IdSuc _
                       Select New With {.Cod_Zon = z.id_zon, _
                                       .Des_Suc = z.suc_cls.suc_nom, _
                                       .Des_Zon = z.zon_des}
            If Llenagrid Then
                GV.DataSource = Zona
                GV.DataBind()
                Return Nothing
            Else
                Return Zona
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub TmcDevuelve(ByVal GV As GridView, Optional ByVal Paginacion As Integer = 9999)
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo Tasa Maxima Convencional
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica         Fecha              Descripcion
        'A. Saldivar            10/02/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Tasamaxcon = (From Tmc In data.tmc_cls _
                 Select New With {.Cod = Tmc.id_tmc, _
                                  .tmc_fec = Tmc.tmc_fec, _
                                  .tmc_val = Tmc.tmc_val, _
                                  .tmc_mor = Tmc.tmc_mor, _
                                  .tmc_est = Tmc.tmc_est}).Skip(sesion.NroPaginacion_TasaMax).Take(Paginacion)



            GV.DataSource = Tasamaxcon
            GV.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    Public Function TmcDevuelveActiva() As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve  Tasa Maxima Convencional Activa
        'Creado por= Fabian Y. Vargas
        'Fecha Creacion: 20/07/2012
        'Quien Modifica         Fecha              Descripcion
        'S Henriquez C.         09/10/2012       Se corrige condicion
        '**************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim TasActiva As Integer = (From Tmc In data.tmc_cls Where Tmc.tmc_est = "A" Select Tmc.tmc_est).Count

            If TasActiva > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return True
        End Try
    End Function

    Public Function TmcDevuelveActivaDevuelve() As Decimal
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve  Tasa Maxima Convencional Activa
        'Creado por= Jorge Lagos
        'Fecha Creacion: 01/11/2012
        'Quien Modifica         Fecha              Descripcion
        '*************************************************************************************************************************************************
        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim TasActiva As Decimal = (From Tmc In data.tmc_cls _
                                        Where Tmc.tmc_est = "A" _
                                        Select Tmc.tmc_val).First

            Return TasActiva

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function CIIUDevuelve(ByVal Dp As DropDownList) As Object

        '--------------------------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve los giros o CIIU de una actividad economica
        'Creado por= Jorge Lagos
        'Fecha Creacion: 23/07/2013
        'Quien Modifica              Fecha              Descripcion
        'jlagos                            12-12-2013      se cambia funcion y trae todos los CIIU (Deysi de BBVA)
        '--------------------------------------------------------------------------------------------------------------------------------------------------
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro = From P In Data.PL_000006_cls Where P.pal_est = "A" _
                                       Select Codigo = P.id_PL_000006.Trim(), Descripcion = P.id_PL_000006.Trim() & " - " & P.pal_des.Trim(), est = P.pal_est, sistema = P.pal_sis


            Dim RG As New FuncionesGenerales.RutinasWeb
            RG.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ActividadEconomicaDevuelve(ByVal id_ciiu As Integer, ByVal Dp As DropDownList) As Object

        '--------------------------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Devuelve la actividad economica dependiente del ciiu seleccionado
        'Creado por= Jorge Lagos
        'Fecha Creacion: 12/12/2013
        '--------------------------------------------------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim parametro = From A In Data.P_0064_cls Join G In Data.PL_000006_cls On A.id_P_0064 Equals G.id_p_0064 _
                                            Where G.id_PL_000006 = id_ciiu And A.pnu_est = "A" _
                                            Select Codigo = A.id_P_0064, Descripcion = A.pnu_des

            Dim RG As New FuncionesGenerales.RutinasWeb
            RG.Llenar_Drop(parametro, "Codigo", "Descripcion", Dp)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function TypDevuelveTodo(ByVal Llenagrid As Boolean, _
                                    Optional ByVal GV As GridView = Nothing, _
                                    Optional ByVal Paginacion As Integer = 9999) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo Tasa y Plazos
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Typ = (From T In data.typ_cls _
                       Select New With {.typ_Cod = T.id_typ, _
                                      .typ_desc = T.P_0023_cls.pnu_des, _
                                      .id_p_0023 = T.id_P_0023, _
                                      .typ_fec = T.typ_fec, _
                                      .typ_mto = T.typ_mto, _
                                      .spread = T.typ_spread, _
                                      .typ_dde = T.typ_dde, _
                                      .typ_hta = T.typ_hta, _
                                      .typ_des = T.typ_des, _
                                      .typ_est = T.typ_est}).Skip(sesion.NroPaginacion_TasaBase).Take(Paginacion)

            If Llenagrid Then
                GV.DataSource = Typ
                GV.DataBind()
                Return Nothing
            Else
                Return Typ
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function TypDevuelveActivo(ByVal Id_Act As String, ByVal Llenagrid As Boolean, _
                                      Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve tasa y Plazos por Estado Activo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Typ = From T In data.typ_cls Where T.typ_est = Id_Act _
                      Select New With {.typ_Cod = T.id_typ, _
                                      .typ_desc = T.P_0023_cls.pnu_des, _
                                      .id_p_0023 = T.id_P_0023, _
                                      .typ_fec = T.typ_fec, _
                                      .typ_mto = T.typ_mto, _
                                      .spread = T.typ_spread, _
                                      .typ_dde = T.typ_dde, _
                                      .typ_hta = T.typ_hta, _
                                      .typ_des = T.typ_des, _
                                      .typ_est = T.typ_est}
            If Llenagrid Then
                GV.DataSource = Typ
                GV.DataBind()
                Return Nothing
            Else
                Return Typ
            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function TypDevuelveInactivo(ByVal Id_Act As String, ByVal Llenagrid As Boolean, _
                                     Optional ByVal GV As GridView = Nothing) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Tasa y Plazos po Estado Inactivo
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Typ = From T In data.typ_cls Where T.typ_est = Id_Act _
                      Select New With {.typ_Cod = T.id_typ, _
                                      .typ_desc = T.P_0023_cls.pnu_des, _
                                      .id_p_0023 = T.id_P_0023, _
                                      .typ_fec = T.typ_fec, _
                                      .typ_mto = T.typ_mto, _
                                      .spread = T.typ_spread, _
                                      .typ_dde = T.typ_dde, _
                                      .typ_hta = T.typ_hta, _
                                      .typ_des = T.typ_des, _
                                      .typ_est = T.typ_est}
            If Llenagrid Then
                GV.DataSource = Typ
                GV.DataBind()
                Return Nothing
            Else
                Return Typ
            End If


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function TimDevuelveTodo(ByVal Llenagrid As Boolean, _
                                    Optional ByVal GV As GridView = Nothing, _
                                     Optional ByVal Paginacion As Integer = 9999) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Todo Tasa Impuesto
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 16/11/2008
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 10/02/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Tim = (From T In data.tim_cls _
                      Select New With {.tim_Cod = T.id_tim, _
                                      .tim_fec = T.tim_fec, _
                                      .tim_val = T.tim_val_pla, _
                                      .tim_TVista = T.tim_val_vis, _
                                      .tim_est = T.tim_est}).Skip(sesion.NroPaginacion_TasaImpuesto).Take(Paginacion)

            If Llenagrid Then
                GV.DataSource = Tim
                GV.DataBind()
                Return Nothing
            Else
                Return Tim
            End If


        Catch ex As Exception
            Return Nothing

        End Try
    End Function

#End Region

#Region "HistorialAsignacionCobradores"

    Public Function HistorialAsignacionDevuelve(ByVal Rut As String, ByVal Rut2 As String, ByVal Fecha1 As DateTime, ByVal Fecha2 As DateTime) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Historial de Asignaciones de Cobrador Telefónico
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 19/05/2008
        'Quien Modifica              Fecha              Descripcion
        'C Arce                     06/09/2011     Se corrige lo que trae el select para que muestre los nombres del deudor y de los ejecutivos
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Historial = (From Has In Data.has_cls _
                Where Has.deu_ide >= Rut And Has.deu_ide <= Rut2 _
                And (Has.fec_asi_rea >= CDate(Fecha1) And Has.fec_asi_rea <= CDate(Fecha2)) _
                Select New With {.RutDeudor = Has.deu_ide, _
                        .NombreDeudor = IIf(Has.deu_cls.id_P_0044 = 1, Has.deu_cls.deu_rso & " " & Has.deu_cls.deu_ape_ptn & " " & Has.deu_cls.deu_ape_mtn, Has.deu_cls.deu_rso), _
                        .CodEjeRasig = Has.eje_cls.eje_nom, _
                        .CodEjeAnterior = Has.eje1.eje_nom, _
                        .FechaAsig = Has.fec_asi_rea, _
                        .CodQuienRealiza = Has.eje_cls.eje_nom}).Skip(NroPaginacion_AlertaDoctoxVencer).Take(5)

            If Historial.Count > 0 Then
                Return Historial
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function


#End Region

#Region "Linea de Credito"


    Public Function LineaDeCreditoDevuelve(ByVal RutCliente As Long, ByVal Estado As Integer) As ldc_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un cliente por su Rut y su estado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                      10/05/2008         Se incorpora la descripcion de parametros
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim LineaDeCredito As ldc_cls = (From L In Data.ldc_cls Where L.cli_idc = Format(RutCliente, Var.FMT_RUT) _
                                                                      And L.id_P_0029 = Estado).First

            'Select New With {.cli_idc = L.cli_idc, _
            '                           .id_ldc = L.id_ldc, _
            '                           .ldc_fec_sol = L.ldc_fec_sol, _
            '                           .ldc_fec_rsn = L.ldc_fec_rsn, _
            '                           .ldc_fec_vig_dde = L.ldc_fec_vig_dde, _
            '                           .ldc_fec_vig_hta = L.ldc_fec_vig_hta, _
            '                           .ldc_adm_mor = L.ldc_adm_mor, _
            '                           .ldc_spr_col = L.ldc_spr_col, _
            '                           .ldc_pto_spr = L.ldc_pto_spr, _
            '                           .ldc_mto_sol = L.ldc_mto_sol, _
            '                           .ldc_mto_apb = L.ldc_mto_apb, _
            '                           .ldc_mto_ocp = L.ldc_mto_ocp, _
            '                           .ldc_est = L.id_P_0029, _
            '                           .ldc_est_des = L.P_0029_cls.pnu_des, _
            '                           .ldc_fec_rvn = L.ldc_fec_rvn, _
            '                           .ldc_tip_com = L.ldc_tip_com, _
            '                           .ldc_des_com = L.ldc_des_com, _
            '                           .ldc_obs = L.ldc_obs}).First

            Return LineaDeCredito



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function LineaDeCreditoDevuelvePorNro(ByVal RutCliente As Long, ByVal NroLinea As Integer) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un cliente por su Rut y ejecutivo que esta conectado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                      10/05/2008         Se incorpora la descripcion de parametros
        'SHenriquez                  15/06/2012         Se incorpora porcentaje exceso permitido
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim LineaDeCredito As Object = (From L In Data.ldc_cls Where L.cli_idc = Format(RutCliente, Var.FMT_RUT) And L.id_ldc = NroLinea _
                                 Select New With {.cli_idc = L.cli_idc, _
                                                            .id_ldc = L.id_ldc, _
                                                            .ldc_fec_sol = L.ldc_fec_sol, _
                                                            .ldc_fec_rsn = L.ldc_fec_rsn, _
                                                            .ldc_fec_vig_dde = L.ldc_fec_vig_dde, _
                                                            .ldc_fec_vig_hta = L.ldc_fec_vig_hta, _
                                                            .ldc_adm_mor = L.ldc_adm_mor, _
                                                            .ldc_spr_col = L.ldc_spr_col, _
                                                            .ldc_pto_spr = L.ldc_pto_spr, _
                                                            .ldc_mto_sol = L.ldc_mto_sol, _
                                                            .ldc_mto_apb = L.ldc_mto_apb, _
                                                            .ldc_mto_ocp = L.ldc_mto_ocp, _
                                                            .ldc_est = L.id_P_0029, _
                                                            .ldc_est_des = L.P_0029_cls.pnu_des, _
                                                            .ldc_fec_rvn = L.ldc_fec_rvn, _
                                                            .ldc_tip_com = L.ldc_tip_com, _
                                                            .ldc_des_com = L.ldc_des_com, _
                                                            .ldc_por_exc = L.ldc_por_exc, _
                                                            .ldc_obs = L.ldc_obs}).First

            Return LineaDeCredito

        Catch ex As Exception

        End Try

    End Function

    Public Sub LineaDeCreditoDevuelvePorCliente(ByVal GV As GridView, ByVal RutCliente As Long, ByVal Estado_Inicio As Integer, ByVal Estado_Termino As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un cliente por su Rut y ejecutivo que esta conectado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                      10/05/2008         Se incorpora la descripcion de parametros
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim LineaDeCredito = From L In Data.ldc_cls Where L.cli_idc = Format(RutCliente, Var.FMT_RUT) And L.id_P_0029 >= Estado_Inicio And L.id_P_0029 <= Estado_Termino _
                                 Order By L.id_ldc Descending _
                                 Select New With {.cli_idc = L.cli_idc, _
                                               .id_ldc = L.id_ldc, _
                                               .ldc_fec_sol = L.ldc_fec_sol, _
                                               .ldc_fec_rsn = L.ldc_fec_rsn, _
                                               .ldc_fec_vig_dde = L.ldc_fec_vig_dde, _
                                               .ldc_fec_vig_hta = L.ldc_fec_vig_hta, _
                                               .ldc_adm_mor = L.ldc_adm_mor, _
                                               .ldc_spr_col = L.ldc_spr_col, _
                                               .ldc_pto_spr = L.ldc_pto_spr, _
                                               .ldc_mto_sol = L.ldc_mto_sol, _
                                               .ldc_mto_apb = L.ldc_mto_apb, _
                                               .ldc_mto_ocp = L.ldc_mto_ocp, _
                                               .ldc_est = L.id_P_0029, _
                                               .ldc_est_des = L.P_0029_cls.pnu_des, _
                                               .ldc_fec_rvn = L.ldc_fec_rvn, _
                                               .ldc_tip_com = L.ldc_tip_com, _
                                               .ldc_des_com = L.ldc_des_com, _
                                               .ldc_obs = L.ldc_obs}



            GV.DataSource = LineaDeCredito
            GV.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Function AnticipoDevuelvePorLinea(ByVal LLenaGrilla As Boolean, ByVal GV As GridView, ByVal Linea_Desde As Integer, ByVal Linea_Hasta As Integer, _
                                                           ByVal TipoDocto_Desde As Integer, ByVal TipoDocto_Hasta As Integer) As Object



        '*********************************************************************************************************************************
        'Descripcion: Devuelve los anticipos que ha realizado el cliente por una linea de credito
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Anticipos = From A In Data.apc_cls Where A.id_ldc >= Linea_Desde And _
                                                                                A.id_ldc <= Linea_Hasta And _
                                                                                A.id_P_0031 >= TipoDocto_Desde And _
                                                                                A.id_P_0031 <= TipoDocto_Hasta _
            Select New With {.apc_num = A.id_apc, _
                             .id_ldc = A.id_ldc, _
                             .id_P_0031 = A.id_P_0031, _
                             .id_P_0031_des = A.P_0031_cls.pnu_des, _
                             .apc_pct = A.apc_pct, _
                             .apc_pzo_pro = A.apc_pzo_pro, _
                             .apc_pzo_max = A.apc_pzo_max, _
                             .apc_ver_son = If(A.apc_ver_son = "N", "NO", "SI"), _
                             .apc_not_son = If(A.apc_not_son = "N", "NO", "SI"), _
                             .apc_cob_son = If(A.apc_cob_son = "N", "NO", "SI")}





            If LLenaGrilla Then

                GV.DataSource = Anticipos
                GV.DataBind()
            Else
                If Anticipos.Count > 0 Then
                    Return Anticipos
                Else
                    Return Nothing
                End If
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function SubLineasDevuelvePorLinea(ByVal NroLinea As Integer, ByVal NroSubLinea As Integer) As sbl_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve una sub linea de un cliente por una linea de credito
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Dim SubLineas As sbl_cls = (From S In Data.sbl_cls Where S.id_ldc = NroLinea And S.id_sbl = NroSubLinea _
                                        Select S).First

            'Select New With {.sbl_num = S.sbl_num, _
            '                          .deu_ide = S.ddr_ide, _
            '                          .deu_nom = S.deu_cls.deu_nom, _
            '                          .sbl_mto = S.sbl_mto, _
            '                          .sbl_mto_ocu = S.sbl_mto_ocu, _
            '                          .sbl_tip_pct_mtp = S.sbl_tip_pct_mto, _
            '                          .sbl_pct = S.sbl_pct, _
            '                          .sbl_tip = S.sbl_tip}).First


            Return SubLineas


        Catch ex As Exception

        End Try

    End Function

    Public Function SubLineasDevuelvePorLinea(ByVal GV As GridView, ByVal NroLinea As Integer, ByVal Tipo As TipoSubLinea, Optional ByVal opcion As Integer = 0) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve las sub lineas de un cliente por una linea de credito
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        'Fabian Y. Vargas              10-07-2012       Se agrega parametro opcional, ya que el formateo de grilla no aplica para  todas las paginas

        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Select Case Tipo
                Case TipoSubLinea.Deudor

                    Dim SubLineas = From S In Data.sbl_cls Where S.id_ldc = NroLinea And _
                                                                                          S.sbl_tip = "D" _
                                             Select New With {.sbl_num = S.id_sbl, _
                                                                       .deu_ide = S.deu_ide, _
                                                                       .deu_dig = S.deu_cls.deu_dig_ito, _
                                                                       .deu_nom = S.deu_cls.deu_rso, _
                                                                       .sbl_mto = S.sbl_mto, _
                                                                       .id_p_0031 = S.id_P_0031, _
                                                                       .sbl_mto_ocu = S.sbl_mto_ocu, _
                                                                       .sbl_tip_pct_mtp = S.sbl_tip_pct_mto, _
                                                                       .sbl_pct = S.sbl_pct}

                    If Not IsNothing(GV) And opcion = 0 Then

                        GV.DataSource = SubLineas
                        GV.DataBind()

                        Dim i As Integer

                        For Each S In SubLineas
                            GV.Rows(i).Cells(0).Text = Format(CLng(S.deu_ide), Fmt.FCMSD) & "-" & S.deu_dig
                            i += 1
                        Next
                        'Else
                        '   Return SubLineas
                    End If

                    If Not IsNothing(GV) And opcion = 1 Then
                        GV.DataSource = SubLineas
                        GV.DataBind()
                        Dim i As Integer

                        For Each S In SubLineas
                            GV.Rows(i).Cells(1).Text = Format(CLng(S.deu_ide), Fmt.FCMSD) & "-" & S.deu_dig
                            i += 1
                        Next
                    End If
                    If IsNothing(GV) Then
                        Return SubLineas
                    End If

                Case TipoSubLinea.TipoDocumento

                    Dim SubLineas = From S In Data.sbl_cls Where S.id_ldc = NroLinea And _
                                                                                          S.sbl_tip = "P" _
                                             Select New With {.sbl_num = S.id_sbl, _
                                                                       .id_P_0031_des = S.P_0031_cls.pnu_des, _
                                                                       .sbl_mto = S.sbl_mto, _
                                                                       .sbl_mto_ocu = S.sbl_mto_ocu, _
                                                                       .sbl_tip_pct_mtp = S.sbl_tip_pct_mto, _
                                                                       .id_p_0031 = S.id_P_0031, _
                                                                       .sbl_pct = S.sbl_pct}

                    If Not IsNothing(GV) Then
                        GV.DataSource = SubLineas
                        GV.DataBind()
                    Else
                        Return SubLineas
                    End If

            End Select



        Catch ex As Exception

        End Try

    End Function

    Public Function SubLineaValida(ByVal TipoDeSubLinea As Int16, ByVal RutCliente As Long, _
                                   ByVal NroLinea As Integer, ByVal NroOperacion As Integer, _
                                   Optional ByVal TipoDocto As Integer = 0) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: valida sub linea por Deudor 
        '1.- Deudor 2.- Documentos
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 13/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************


        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim SumaOpe = From D In Data.dsi_cls Where D.ope_cls.id_ope = NroOperacion _
                                        Group By NroOpe = D.id_ope Into Mto = Sum(D.dsi_mto_fin * D.ope_cls.ope_fac_cam)

            Select Case TipoDeSubLinea
                Case 1 'Deudor



                    '       -- SubLinea Deudor
                    '       select distinct ldc_num,
                    '                       A.ddr_ide,
                    '              (select deu_rso from deu where deu_ide = A.ddr_ide) as ddr_rso,
                    '              A.sbl_mto - A.sbl_mto_ocu as mto_disp,
                    '              (select O.ope_mto_ant * O.ope_fac_cam from ope O where O.ope_num = @nro_operacion
                    '                                                                 and O.cli_idc = A.cli_idc) as mto_ope
                    '       from  sbl A,dsi B
                    '       where A.cli_idc = @rut_cliente
                    '       and   A.ldc_num = @nro_linea
                    '       and   A.sbl_tip = 'D'
                    '       and   B.cli_idc = @rut_cliente
                    '       and   B.ope_num = @nro_operacion
                    '       and   B.cli_idc = A.cli_idc
                    '       and   B.ddr_ide = A.ddr_ide
                    '       and   A.sbl_mto - A.sbl_mto_ocu < (select sum(dsi_mto_fin * O.ope_fac_cam)
                    '                                          from dsi D, ope O where D.cli_idc = @rut_cliente
                    '                                                              and D.ope_num = @nro_operacion
                    '                                                              and D.cli_idc = A.cli_idc
                    '                                                              and D.ddr_ide = A.ddr_ide
                    '                                                              and D.cli_idc = O.cli_idc
                    '                                                              and D.ope_num = O.ope_num
                    '                                                              and O.ope_num = @nro_operacion)



                    For Each Su In SumaOpe

                        Dim SubLinDeu = From S In Data.sbl_cls, D In Data.dsi_cls _
                                        Where S.ldc_cls.cli_idc = RutCliente _
                                           And S.ldc_cls.id_ldc = NroLinea _
                                           And D.ope_cls.id_ope = NroOperacion _
                                           And S.sbl_tip = "D" _
                                           And S.deu_ide = D.deu_ide _
                                           Select ldc_num = S.ldc_cls.id_ldc, _
                                                  RutDeu = S.deu_ide, _
                                                  NomDeu = S.deu_cls.deu_rso, _
                                                  MtoDisp = S.sbl_mto - S.sbl_mto_ocu, _
                                                  MtoOper = D.ope_cls.ope_mto_ant * D.ope_cls.ope_fac_cam

                        'And S.sbl_mto - S.sbl_mto_ocu < Su.Mto _


                        If SubLinDeu.Count > 0 Then
                            For Each s In SubLinDeu
                                If s.MtoDisp - Su.Mto Then
                                    Return False
                                Else
                                    Return True
                                End If
                            Next

                        Else
                            Return True
                        End If


                    Next






                Case 2 'Producto

                    '        -- SubLinea Producto
                    '       select (select pnu_des from pnu where pnu_cod_tbl = 31 and pnu_cod = A.pnu_tip_doc) as tipo_doc,
                    '              A.sbl_mto - A.sbl_mto_ocu as mto_disp,
                    '              (select O.ope_mto_ant * O.ope_fac_cam from ope O where O.ope_num = @nro_operacion
                    '                                                                 and O.cli_idc = A.cli_idc) as mto_ope
                    '       from sbl A
                    '       where A.cli_idc = @rut_cliente
                    '       and   A.sbl_tip = 'P'
                    '       and   A.pnu_tip_doc = @tipo_docto
                    '       and   A.sbl_mto - A.sbl_mto_ocu < (select sum(dsi_mto_fin * ope_fac_cam)
                    '                                              from dsi D, ope O where D.cli_idc = @rut_cliente
                    '                                                                   and D.ope_num = @nro_operacion
                    '                                                                   and D.cli_idc = A.cli_idc
                    '                                                                   and D.pnu_tip_doc = A.pnu_tip_doc
                    '                                                                   and D.cli_idc = O.cli_idc
                    '                                                                   and D.ope_num = O.ope_num
                    '                                                                   and O.ope_num = @nro_operacion)

                    For Each Su In SumaOpe

                        Dim SubLinPro = From S In Data.sbl_cls, O In Data.ope_cls _
                                        Where S.ldc_cls.cli_idc = RutCliente _
                                           And S.ldc_cls.id_ldc = NroLinea _
                                           And O.id_ope = NroOperacion _
                                           And S.sbl_tip = "P" _
                                           And S.id_P_0031 = TipoDocto _
                                           Select TipoDoc = S.P_0031_cls.pnu_des, _
                                                  MtoDisp = S.sbl_mto - S.sbl_mto_ocu, _
                                                  MtoOper = O.ope_mto_ant * O.ope_fac_cam


                        If SubLinPro.Count > 0 Then
                            For Each s In SubLinPro
                                If s.MtoDisp - Su.Mto Then
                                    Return False
                                Else
                                    Return True
                                End If
                            Next

                        Else
                            Return True
                        End If


                    Next


            End Select

            Return True

        Catch ex As Exception

        End Try
    End Function

    Public Function SubLineasDevuelvePorDeudor(ByVal rutdeudor As String) As Double

        '*********************************************************************************************************************************
        'Descripcion: Devuelve el monto de la sub linea aprobada para el deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 18-05-2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim SubLineas As Double = (From S In Data.sbl_cls Where S.deu_ide = Format(CLng(rutdeudor), Var.FMT_RUT) And _
                                                         S.sbl_tip = "D" _
                                                         Select S.sbl_mto).First()

            Return SubLineas

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Sub ActasDevuelvePorLDC(ByVal GV As GridView, ByVal ID_LDC As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Actas asociadas a numero de linea
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 29/05/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Acta = From A In data.ACT_IMG_cls Where A.id_ldc = ID_LDC Select A

            GV.DataSource = Acta
            GV.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub ActasDevuelvePorCli(ByVal gv As GridView, ByVal ID_Cli As String)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Actas asociadas a cliente
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 01/06/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim Acta = From A In data.ACT_IMG_cls Where A.ldc_cls.cli_idc = ID_Cli Select A

            gv.DataSource = Acta
            gv.DataBind()

        Catch ex As Exception

        End Try

    End Sub


#End Region

#Region "Notario"

    Public Sub NotarioDevuelveTodosPorSucursal(ByVal GV As GridView, ByVal Sucursal As String)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los notarios de una sucursal
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'Pgatica                     18/05/2009         Se agregan mas campos a la busqueda
        '*********************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Notarios = From N In Data.ntr_cls Where N.suc_cls.id_suc = Sucursal _
                                  Select New With {.ntr_nom = N.ntr_nom, _
                                                   .suc_nom = N.suc_cls.suc_nom, N.id_ntr, N.id_suc, N.ntr_dml, N.ntr_dml_emp, N.ntr_tel, N.ntr_def}

            GV.DataSource = Notarios
            GV.DataBind()



        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Datos del Sistema"

    Public Function SistemaDevuelve() As sis_cls

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los Datos del sistema
        'Creado por Jaime Santos C.
        'Fecha Creacion: 24/07/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Sistema As sis_cls = (From S In Data.sis_cls).First

            Return Sistema


        Catch ex As Exception
            Return Nothing
        End Try

    End Function


#End Region

#Region "INGRESOS (ING)"

    Public Function IngresosAnticiposSinGiroDevuelve(ByVal NroOpe As Integer) As Object

        '**************************************************************************************************************************************************
        'Descripcion: devuelve los ingresos de un egreso que es de tipo anticipo sin giro
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 16/09/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Ingresos = From I In Data.ing_sec_cls Where I.egr_sec_cls.id_P_0055 = 4 _
                                                    And I.egr_sec_cls.id_P_0056 = 5 _
                                                    And I.egr_sec_cls.doc_cls.id_opo = NroOpe

            If Ingresos.Count > 0 Then
                Return Ingresos
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function IngresosDevuelveSinProcesar(ByVal RutCliente As Long, ByVal RutDeudor As Long, _
                                     ByVal QueSePaga_Desde As Integer, ByVal QueSePaga_Hasta As Integer, _
                                     ByVal FechaPago_Desde As DateTime, ByVal FechaPago_Hasta As DateTime) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los ingresos de un cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 31/12/2008
        'Quien Modifica Fecha Descripcion
        'JLagos         02/02/2009   Se agrega como criterio de busqueda el Deudor y fecha de pago
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim RutCli_Desde As Long
            Dim RutCli_Hasta As Long

            Dim RutDeu_Desde As Long
            Dim RutDeu_Hasta As Long

            If RutCliente = 0 Then
                RutCli_Desde = 0
                RutCli_Hasta = 99999999999
            Else
                RutCli_Desde = RutCliente
                RutCli_Hasta = RutCliente
            End If

            If RutDeudor = 0 Then
                RutDeu_Desde = 0
                RutDeu_Hasta = 99999999999
            Else
                RutDeu_Desde = RutDeudor
                RutDeu_Hasta = RutDeudor
            End If


            Dim Ingresos = From I In data.ing_sec_cls Where (I.cli_idc >= Format(RutCli_Desde, Var.FMT_RUT) And _
                                                             I.cli_idc <= Format(RutCli_Hasta, Var.FMT_RUT)) And _
                                                            (I.ing_vld_rcz = "I" Or _
                                                             I.ing_vld_rcz = "V" Or _
                                                             I.ing_vld_rcz = "L" Or _
                                                             I.ing_vld_rcz = "C") And _
                                                            (I.id_P_0053 >= QueSePaga_Desde And _
                                                             I.id_P_0053 <= QueSePaga_Hasta) And _
                                                            (I.ing_cls.ing_fec >= FechaPago_Desde And _
                                                             I.ing_cls.ing_fec <= FechaPago_Hasta) And _
                                                             I.ing_pro = "N" _
                                       Select I.id_ing, _
                                              I.id_ing_sec, _
                                              I.id_doc, _
                                              I.id_dpo, _
                                              I.id_cxc, _
                                              I.id_nce, _
                                              I.id_P_0053, _
                                              I.ing_mto_abo, _
                                              I.ing_mto_int, _
                                              I.ing_mto_tot, _
                                              I.ing_qpa, _
                                              I.ing_pag_deu, _
                                              I.ing_pro


            For Each x In Ingresos
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function IngresosDevuelve(ByVal RutCliente As Long, ByVal RutDeudor As Long, _
                                     ByVal QueSePaga_Desde As Integer, ByVal QueSePaga_Hasta As Integer, _
                                     ByVal FechaPago_Desde As DateTime, ByVal FechaPago_Hasta As DateTime) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los ingresos de un cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 31/12/2008
        'Quien Modifica Fecha Descripcion
        'JLagos         02/02/2009   Se agrega como criterio de busqueda el Deudor y fecha de pago
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim RutCli_Desde As Long
            Dim RutCli_Hasta As Long

            Dim RutDeu_Desde As Long
            Dim RutDeu_Hasta As Long

            If RutCliente = 0 Then
                RutCli_Desde = 0
                RutCli_Hasta = 99999999999
            Else
                RutCli_Desde = RutCliente
                RutCli_Hasta = RutCliente
            End If

            If RutDeudor = 0 Then
                RutDeu_Desde = 0
                RutDeu_Hasta = 99999999999
            Else
                RutDeu_Desde = RutDeudor
                RutDeu_Hasta = RutDeudor
            End If



            Dim Ingresos = From I In data.ing_sec_cls Where (I.cli_idc >= Format(RutCli_Desde, Var.FMT_RUT) And _
                                                             I.cli_idc <= Format(RutCli_Hasta, Var.FMT_RUT)) And _
                                                            (I.ing_vld_rcz = "I" Or _
                                                             I.ing_vld_rcz = "V" Or _
                                                             I.ing_vld_rcz = "L" Or _
                                                             I.ing_vld_rcz = "C") And _
                                                            (I.id_P_0053 >= QueSePaga_Desde And _
                                                             I.id_P_0053 <= QueSePaga_Hasta) And _
                                                            (I.ing_cls.ing_fec >= FechaPago_Desde And _
                                                             I.ing_cls.ing_fec <= FechaPago_Hasta) _
                                       Select I.id_ing, _
                                              I.id_ing_sec, _
                                              I.id_doc, _
                                              I.id_dpo, _
                                              I.id_cxc, _
                                              I.id_nce, _
                                              I.id_P_0053, _
                                              I.ing_mto_abo, _
                                              I.ing_mto_int, _
                                              I.ing_mto_tot, _
                                              I.ing_qpa, _
                                              I.ing_pag_deu, _
                                              I.ing_pro


            For Each x In Ingresos
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function IngresoDevuelveCabecera(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                            ByVal FechaPago_Desde As DateTime, ByVal FechaPago_Hasta As DateTime) As Collection

        'ByVal RutDeudor_Desde As Integer, ByVal RutDeudor_Hasta As Integer, _

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las los ingresos de un cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 02/02/2009
        'Quien Modifica                         Fecha      Descripcion
        'A Saldivar                             02/09/2010 Se rescata deudor segun tipo(p_0044 )
        'S Henriquez                            02/08/2012 Se Cambia Deudor por Pagador
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim IngresosClientes = From I In data.ing_sec_cls Where (I.cli_idc >= Format(RutCliente_Desde, Var.FMT_RUT) And I.cli_idc <= Format(RutCliente_Hasta, Var.FMT_RUT)) And _
                                                                    (I.ing_cls.ing_fec >= FechaPago_Desde And I.ing_cls.ing_fec <= FechaPago_Hasta) And _
                                                                    (I.ing_vld_rcz = "I" Or I.ing_vld_rcz = "C" Or I.ing_vld_rcz = "R" Or I.ing_vld_rcz = "V" Or I.ing_vld_rcz = "L") And _
                                                                     I.ing_cls.ing_pgo_hre = "N" And _
                                                                     I.ing_qpa = "C" _
                                                    Select New With {.Rut = I.cli_idc, _
                                                                     .Nombre = If(I.cli_cls.id_P_0044 = 1, _
                                                                                  I.cli_cls.cli_rso.Trim & " " & I.cli_cls.cli_ape_ptn.Trim & " " & I.cli_cls.cli_ape_mtn.Trim, _
                                                                                  I.cli_cls.cli_rso.Trim), _
                                                                     .Q = I.ing_qpa, _
                                                                     .Quien = "", _
                                                                     .NroPago = I.id_ing, _
                                                                     .Fecha = I.ing_cls.ing_fec} Distinct

            Dim IngresosDeudores = From I In data.ing_sec_cls Where (I.cli_idc >= Format(RutCliente_Desde, Var.FMT_RUT) And I.cli_idc <= Format(RutCliente_Hasta, Var.FMT_RUT)) And _
                                                                 (I.ing_cls.ing_fec >= FechaPago_Desde And I.ing_cls.ing_fec <= FechaPago_Hasta) And _
                                                                 (I.ing_vld_rcz = "I" Or I.ing_vld_rcz = "C" Or I.ing_vld_rcz = "R" Or I.ing_vld_rcz = "V" Or I.ing_vld_rcz = "L") And _
                                                                  I.ing_cls.ing_pgo_hre = "N" And _
                                                                  I.ing_qpa = "D" _
                                                Select New With {.Rut = I.doc_cls.dsi_cls.deu_ide, _
                                                                 .Nombre = If(I.doc_cls.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                                              I.doc_cls.dsi_cls.deu_cls.deu_rso & " " & I.doc_cls.dsi_cls.deu_cls.deu_ape_ptn & " " & I.doc_cls.dsi_cls.deu_cls.deu_ape_mtn, _
                                                                              I.doc_cls.dsi_cls.deu_cls.deu_rso), _
                                                                 .Q = I.ing_qpa, _
                                                                 .Quien = "", _
                                                                 .NroPago = I.id_ing, _
                                                                 .Fecha = I.ing_cls.ing_fec} Distinct

            For Each x In IngresosClientes
                Select Case x.Q
                    Case "C" : x.Quien = "CLIENTE"
                    Case "D" : x.Quien = "PAGADOR"
                End Select
                Coll.Add(x)
            Next

            For Each x In IngresosDeudores
                Select Case x.Q
                    Case "C" : x.Quien = "CLIENTE"
                    Case "D" : x.Quien = "PAGADOR"
                End Select
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function


    Public Function IngresoDevuelveModoDePago(ByVal ID As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las los documento de pagos (dpo) por su Numero unico
        'Creado por Jorge Lagos
        'Fecha Creacion: 03/02/2008
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            'Dim DoctoPago = From D In data.dpo_cls _
            '                Join I In data.ing_sec_cls On D.id_dpo Equals I.id_dpo Where I.id_ing = ID _
            '                Select D.id_P_0054, _
            '                       Tipo = D.p_0054_cls.pnu_des, _
            '                       D.id_dpo, _
            '                       D.dpo_mto, _
            '                       D.id_bco, _
            '                       Banco = D.bco_cls.bco_des, _
            '                       D.dpo_fec_emi, _
            '                       D.dpo_fev, _
            '                       D.id_PL_000047, _
            '                       Plaza = D.PL_000047_cls.pal_des, _
            '                       D.dpo_cct, _
            '                       D.dpo_aor, _
            '                       I.ing_vld_rcz, _
            '                       D.id_P_0052, _
            '                       Estado = D.P_0052_cls.pnu_des, _
            '                       Moneda = D.P_0023_cls.pnu_atr_003, _
            '                       D.id_P_0023 _
            '                       Distinct

            Dim DoctoPago = From D In data.dpo_cls _
                           Join I In data.ing_sec_cls On D.id_dpo Equals I.id_dpo Where I.id_ing = ID _
                           Select D.id_P_0054, _
                                  Tipo = Nothing, _
                                  D.id_dpo, _
                                  D.dpo_mto, _
                                  D.id_bco, _
                                  Banco = D.bco_cls.bco_des, _
                                  D.dpo_fec_emi, _
                                  D.dpo_fev, _
                                  D.id_PL_000047, _
                                  Plaza = D.PL_000047_cls.pal_des, _
                                  D.dpo_cct, _
                                  D.dpo_aor, _
                                  I.ing_vld_rcz, _
                                  D.id_P_0052, _
                                  Estado = D.P_0052_cls.pnu_des, _
                                  Moneda = D.P_0023_cls.pnu_atr_003, _
                                  D.id_P_0023 _
                                  Distinct

            For Each x In DoctoPago
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function TipoDeIngresoDevuelve(ByVal Id As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los tipos de documento que esta cancelando Doctos. o CxC.
        'Creado por Jorge Lagos
        'Fecha Creacion: 03/02/2008
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try

            Dim Tipos = From I In data.ing_sec_cls Where I.id_ing = Id _
                        Group By Id_Tipo = I.id_P_0053, _
                                 Tipo = I.P_0053_cls.pnu_des _
                                 Into Monto = Sum(I.ing_mto_tot * I.ing_fac_cam) _
                        Select New With {Id_Tipo, Tipo, Monto}

            For Each x In Tipos

                If IsNothing(x.Monto) Then
                    x.Monto = 0
                End If

                Coll.Add(x)

            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function


    Public Function PagosDocumentosCxCDevuelveDetalle(ByVal Id As Integer, ByVal Tipo As Int16) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los tipos de documento que esta cancelando Doctos. o CxC.
        'Creado por Jorge Lagos
        'Fecha Creacion: 03/02/2008
        'Quien Modifica  Fecha      Descripcion
        'JLagos         19/02/2009  Se agrega a los resultados el campo id_nce, para ver si es un documento no cedido   
        'PGatica        05/05/2009  Se agrega opción para traer detalles de documentos no cedidos , además de campos de Dsi necesarios para mostrar en recaudación
        'A Saldivar     05/01/2011  Se agrega paginacion
        'JLagos         10/01/2012 -Se agrega el nombre completo del deudor
        '                          -Al tipo dos se le agrega el id_nce 
        '**************************************************************************************************************************************************


        Dim data As New CapaDatos.DataClsFactoringDataContext
        Dim Coll As New Collection
        Dim sesion As New ClsSession.ClsSession

        Try

            Select Case Tipo

                Case 1 'DEVUELVE DETALLE TIPO INGRESO CUENTAS X COBRAR

                    Dim Cuentas = (From C In data.ing_sec_cls Where C.id_ing = Id And C.id_P_0053 = 1 _
                                Group By C.id_cxc, _
                                         C.cli_idc, _
                                         C.cxc_cls.id_P_0041, _
                                         TipoCuenta = C.cxc_cls.p_0041_cls.pnu_des, _
                                         C.cxc_cls.cxc_fec, _
                                         C.cxc_cls.cxc_des, _
                                         C.id_nce, _
                                         C.ing_fac_cam _
                                       Into _
                                       ing_mto_abo = Sum(C.ing_mto_abo), _
                                       ing_mto_int = Sum(C.ing_mto_int) _
                                       Select id_cxc, cli_idc, TipoCuenta, cxc_fec, cxc_des, _
                                           ing_mto_abo = (ing_mto_abo * ing_fac_cam), _
                                           ing_mto_int = (ing_mto_int * ing_fac_cam)).Skip(sesion.NroPaginacion_Docto_a_Pagar)


                    For Each x In Cuentas.Take(8)
                        Coll.Add(x)
                    Next


                Case 2 'DEVUELVE DETALLE TIPO INGRESO DOCUMENTOS

                    Dim Cuentas = (From C In data.ing_sec_cls Where C.id_ing = Id And C.id_P_0053 = 2 _
                                Group By C.id_doc, _
                                         C.cli_idc, _
                                         Nombre = If(C.cli_cls.id_P_0044 = 1, _
                                                     C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                                     C.cli_cls.cli_rso.Trim), _
                                         RutDeu = C.doc_cls.dsi_cls.deu_ide, _
                                         NombreDeu = If(C.doc_cls.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                        C.doc_cls.dsi_cls.deu_cls.deu_rso.Trim & " " & C.doc_cls.dsi_cls.deu_cls.deu_ape_ptn & " " & C.doc_cls.dsi_cls.deu_cls.deu_ape_ptn, _
                                                        C.doc_cls.dsi_cls.deu_cls.deu_rso.Trim), _
                                         C.doc_cls.opo_cls.ope_cls.opn_cls.id_P_0031, _
                                         C.doc_cls.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                                         C.id_ing, _
                                         C.ing_mto_tot, _
                                         C.ing_tot_par, _
                                         C.doc_cls.dsi_cls.dsi_num, _
                                         C.doc_cls.dsi_cls.dsi_fev_rea, _
                                         C.ing_fac_cam, _
                                         C.id_nce _
                                       Into _
                                        ing_mto_abo = Sum(C.ing_mto_abo), _
                                        ing_mto_int = Sum(C.ing_mto_int) _
                                      Select id_doc, cli_idc, Nombre, RutDeu, NombreDeu, pnu_atr_003, id_ing, _
                                          ing_mto_abo = (ing_mto_abo * ing_fac_cam), _
                                          ing_mto_int = (ing_mto_int * ing_fac_cam), dsi_num, _
                                          dsi_fev_rea, ing_mto_tot, ing_tot_par, id_nce).Skip(sesion.NroPaginacion_Docto_a_Pagar)


                    For Each x In Cuentas.Take(8)
                        Coll.Add(x)
                    Next

                Case 3

                    Dim Cuentas = (From C In data.ing_sec_cls Where C.id_ing = CDec(Id) And C.id_P_0053 = 7 _
                             Select C.id_nce, _
                                    C.cli_idc, _
                                    Nombre = If(C.cli_cls.id_P_0044 = 1, _
                                    C.cli_cls.cli_rso.Trim & " " & C.cli_cls.cli_ape_ptn.Trim & " " & C.cli_cls.cli_ape_mtn.Trim, _
                                    C.cli_cls.cli_rso.Trim), _
                                    RutDeu = C.nce_cls.deu_cls.deu_ide, _
                                    NombreDeu = If(C.nce_cls.deu_cls.id_P_0044 = 1, _
                                                   C.nce_cls.deu_cls.deu_rso.Trim & " " & C.nce_cls.deu_cls.deu_ape_ptn & " " & C.nce_cls.deu_cls.deu_ape_ptn, _
                                                   C.nce_cls.deu_cls.deu_rso.Trim), _
                                    C.nce_cls.id_p_0031, _
                                    C.nce_cls.P_0031_cls.pnu_atr_003, _
                                    C.id_ing, _
                                    C.nce_cls.nce_num_doc, _
                                    C.ing_tot_par, _
                                    C.ing_mto_tot, _
                                    C.ing_fac_cam, _
                                    C.nce_cls.nce_fec_vcto, _
                                    ing_mto_abo = (C.ing_mto_abo * C.ing_fac_cam), _
                                    ing_mto_int = (C.ing_mto_int * C.ing_fac_cam)).Skip(sesion.NroPaginacion_Docto_a_Pagar)



                    For Each x In Cuentas.Take(8)
                        Coll.Add(x)
                    Next

            End Select

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function IngresosDevuelveTodos(ByVal RutCliente As Long, ByVal RutDeudor As Long, _
                                     ByVal QueSePaga_Desde As Integer, ByVal QueSePaga_Hasta As Integer, _
                                     ByVal FechaPago_Desde As DateTime, ByVal FechaPago_Hasta As DateTime) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las los ingresos de un cliente
        'Creado por Jorge Lagos
        'Fecha Creacion: 31/12/2008
        'Quien Modifica Fecha Descripcion
        'JLagos         02/02/2009   Se agrega como criterio de busqueda el Deudor y fecha de pago
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New CapaDatos.DataClsFactoringDataContext

        Try


            Dim Ingresos = From I In data.ing_sec_cls Where (I.cli_idc = Format(RutCliente, Var.FMT_RUT) Or _
                                                             I.doc_cls.dsi_cls.deu_ide = Format(RutCliente, Var.FMT_RUT)) And _
                                                            (I.id_P_0053 >= QueSePaga_Desde And _
                                                             I.id_P_0053 <= QueSePaga_Hasta) And _
                                                            (I.ing_cls.ing_fec >= FechaPago_Desde And _
                                                             I.ing_cls.ing_fec <= FechaPago_Hasta) _
                                       Select I.id_ing, _
                                              I.id_ing_sec, _
                                              I.id_doc, _
                                              I.id_dpo, _
                                              I.id_cxc, _
                                              I.id_nce, _
                                              I.id_P_0053, _
                                              I.ing_mto_abo, _
                                              I.ing_mto_int, _
                                              I.ing_mto_tot, _
                                              I.ing_qpa, _
                                              I.ing_pag_deu, _
                                              I.ing_pro, _
                                              I.ing_vld_rcz



            For Each x In Ingresos
                Coll.Add(x)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

#End Region

#Region "Egresos"

    Public Function Egresos_Devuelve(ByVal NroOpe As Integer) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los egresos de una operacion segun rango de quien paga y el tipo
        'Creado por Pablo Gatica S.
        'Fecha Creacion: 17/10/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Egresos = From E In Data.egr_sec_cls Where E.id_P_0055 = 4 _
                                                    And E.id_P_0056 = 5 _
                                                    And E.doc_cls.id_opo = NroOpe

            If Egresos.Count > 0 Then
                Return Egresos
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function





#End Region

#Region "PRORROGAS"

    Public Function Prorroga_DevuelveSolicitudes(ByVal RutCliente1 As String, ByVal RutCliente2 As String, _
                                                 ByVal fecha1 As DateTime, ByVal fecha2 As DateTime, _
                                                 Optional ByVal est_dde As Integer = 1, Optional ByVal est_hta As Integer = 999) As Object

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve  prorrogas por rango amplio de rut y fechas
        'Creado por :
        'Fecha Creacion: 
        'Quien Modifica         Fecha       Descripcion
        'A. Saldivar            08/09/2019  se modifica la forma de rescatar razon social de cliente
        '**************************************************************************************************************************************************

        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim coll As New Collection

            Dim Temporal_spg = (From SPG In Data.spg_cls _
                               Where SPG.cli_idc >= RutCliente1 And SPG.cli_idc <= RutCliente2 _
                               And SPG.spg_fec >= fecha1 And SPG.spg_fec <= fecha2 And SPG.spg_est >= est_dde And SPG.spg_est <= est_hta Order By SPG.id_spg _
                                 Select SPG.cli_idc, _
                                 SPG.cli_cls.cli_dig_ito, _
                                      Cliente = If(SPG.cli_cls.id_P_0044 = 1, _
                                                   SPG.cli_cls.cli_rso & " " & SPG.cli_cls.cli_ape_ptn & " " & SPG.cli_cls.cli_ape_mtn, _
                                                   SPG.cli_cls.cli_rso), _
                                      SPG.spg_fec, _
                                      SPG.id_spg, _
                                      SPG.eje_cls.eje_des_cra, _
                                      SPG.spg_tas, _
                                      SPG.spg_com, _
                                      SPG.spg_obs, _
                                      SPG.spg_est).Skip(sesion.NroPaginacion)

            For Each t In Temporal_spg.Take(4)
                coll.Add(t)
            Next

            'Return Temporal_spg
            Return coll


        Catch ex As Exception
            Return Nothing

        End Try

    End Function

    Public Function Prorroga_DevuelveDocumentosConSolicitud(ByVal RutCliente As String) As Object
        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_dpg = (From DPG In Data.dpg_cls _
                               Where DPG.spg_cls.cli_idc = RutCliente _
                                    And DPG.spg_cls.spg_est = 1 _
                               Select DPG)

            Return Temporal_dpg

        Catch ex As Exception
            Return Nothing

        End Try

    End Function

    Public Function Prorroga_DevuelveDetalleSolicitud(ByVal id_spg As Long) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve  detalle de prorrogas 
        'Creado por :
        'Fecha Creacion: 
        'Quien Modifica         Fecha       Descripcion
        'A. Saldivar            08/09/2019  se modifica la forma de rescatar razon social de deudor
        '**************************************************************************************************************************************************
        Try
            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim Temporal_spg = (From dpg In Data.dpg_cls _
                               Where dpg.id_spg = id_spg _
                               Select dpg.doc_cls.dsi_cls.deu_ide, _
                                        dpg.doc_cls.dsi_cls.deu_cls.deu_dig_ito, _
                                      Deudor = If(dpg.doc_cls.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                  dpg.doc_cls.dsi_cls.deu_cls.deu_rso & " " & dpg.doc_cls.dsi_cls.deu_cls.deu_ape_ptn & " " & dpg.doc_cls.dsi_cls.deu_cls.deu_ape_mtn, _
                                                  dpg.doc_cls.dsi_cls.deu_cls.deu_rso), _
            dpg.doc_cls.opo_cls.opo_otg, _
            TipoDocto = dpg.doc_cls.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
            dpg.doc_cls.dsi_cls.dsi_num, _
            dpg.doc_cls.dsi_cls.dsi_flj_num, _
            dpg.doc_cls.dsi_cls.dsi_mto_fin, _
            dpg.doc_sdo_cli, _
            dpg.doc_cls.opo_cls.ope_cls.opn_cls.id_P_0023, _
            Moneda = dpg.doc_cls.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
            dpg.doc_fev_rea, _
            dpg.nva_doc_fev_rea, _
            dpg.dpg_int_ere, _
            dpg.dpg_com_isi, _
            dpg.dpg_iva_com, _
            TotalGastos = (dpg.dpg_int_ere + dpg.dpg_com_isi + dpg.dpg_iva_com)).Skip(sesion.NroPaginacion_DetalleSolProrroga)


            For Each x In Temporal_spg.Take(4)
                coll.Add(x)

            Next

            Return coll

            'Return Temporal_spg

        Catch ex As Exception
            Return Nothing

        End Try

    End Function

    Public Function Prorroga_ValidaCambiodeEstadoDocumentosSolicitud(ByVal id_spg As Long) As Object
        'jlagos     26-08-2010    se agrega estado 13 
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_dpg = From DPG In Data.dpg_cls _
                               Where DPG.id_spg = id_spg _
                               And DPG.doc_cls.dsi_cls.id_P_0011 <> 5 _
                               And DPG.doc_cls.dsi_cls.id_P_0011 <> 13 _
                               And DPG.doc_cls.dsi_cls.dsi_flj = "N" _
                               And DPG.doc_cls.dsi_cls.id_P_0011 <> DPG.id_P_0011 _
                               Select DPG.doc_cls.dsi_cls.dsi_num, _
                               DPG.doc_cls.dsi_cls.dsi_flj_num


            Return Temporal_dpg

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Prorroga_ValidaPagoEnLineaDocumentosSolicitud(ByVal id_spg As Long) As Object

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Temporal_dpg = From DPG In Data.dpg_cls _
                               Join ING_SEC In Data.ing_sec_cls On ING_SEC.id_doc Equals DPG.id_doc _
                               Where DPG.id_spg = id_spg _
                               And ING_SEC.ing_pro = "N" _
                               And (ING_SEC.ing_vld_rcz = "S" Or ING_SEC.ing_vld_rcz = "I" Or ING_SEC.ing_vld_rcz = "V" Or ING_SEC.ing_vld_rcz = "C" Or ING_SEC.ing_vld_rcz = "L") _
            Select DPG.doc_cls.dsi_cls.dsi_num, _
            DPG.doc_cls.dsi_cls.dsi_flj_num


            Return Temporal_dpg

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Prorroga_ValidaDocumentosEnOtrasProrrogasDelDia(ByVal id_spg As Long) As Object

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Temporal_dpg = From DPG In Data.dpg_cls Where (DPG.id_spg = id_spg)

            For Each d In Temporal_dpg
                Dim dpg = From dp In Data.dpg_cls Where dp.id_doc = d.id_doc And dp.id_spg <> id_spg And dp.spg_cls.spg_fec = d.spg_cls.spg_fec And dp.spg_cls.spg_est = 2
            Next

            Return Temporal_dpg

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Condiciones por Cliente"

    Public Function BuscaComisionCli(ByVal RutCliente As Long) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Retorna Comision Cliente
        'Creado por= Victor Alvarez R.                                                                                                                      
        'Fecha Creacion: 12/09/2011                                                                                                                  
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            Dim Gastos = From C In data.CDC_cls Where C.cli_idc = Format(RutCliente, Var.FMT_RUT) Select C

            For Each p In Gastos
                Coll.Add(p)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function BuscaGastos(ByVal RutCliente As Long) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Busca gastos para chequer grilla
        'Creado por= Victor Alvarez R
        'Fecha Creacion: 12/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Gastos = From G In Data.GDC_cls Where G.cli_idc = Format(RutCliente, Var.FMT_RUT) Select G

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function VerificaOpGastos(ByVal RutCliente As Long) As Collection

        '*********************************************************************************************************************************
        'Descripcion: verifica la existencia de un Id de gastos para un cliente determinado
        'Creado por= Victor Alvarez R.                                                                                                                      
        'Fecha Creacion: 12/09/2011                                                                                                                  
        '**************************************************************************************************************************************************

        Dim Coll As New Collection
        Dim data As New DataClsFactoringDataContext

        Try

            Dim Gastos = From G In data.GDC_cls Where G.cli_idc = Format(RutCliente, Var.FMT_RUT) Select G

            For Each p In Gastos
                Coll.Add(p)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

#End Region

#Region "APLICACIONES"



    Public Function Aplicacion_Devuelve(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                        ByVal Fecha_Desde As DateTime, ByVal Fecha_Hasta As DateTime, _
                                        ByVal Pendiente_VB_Cursadas As String, ByVal AprobacionComercial As String, _
                                        Optional ByVal Id_Eje_Desde As Integer = 0, Optional ByVal Id_Eje_Hasta As Integer = 999, _
                                        Optional ByVal Pag As Integer = 9999) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Aplicacion de los cliente que se encuentran segun criterio
        'Creado por Jorge Lagos
        'Fecha Creacion: 17/02/2009
        'Quien Modifica         Fecha           Descripcion
        'A. Saldivar            09/11/2010      Se agrega busqueda por rango amplio de ejecutivo
        'A. Saldivar            04/01/2011      Se agrega paginacion
        'J. Lagos               10/01/2012      Se quita busqueda por ejecutivo
        'J. Lagos               12/12/2013      agrega VBagrega VB
        'J. Lagos               27/05/2014      agrega order
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim Aplicaciones = (From A In Data.apl_cls Where _
                                 (CLng(A.cli_idc) >= RutCliente_Desde And CLng(A.cli_idc) <= RutCliente_Hasta) And _
                                 (A.apl_fec >= Fecha_Desde And A.apl_fec <= Fecha_Hasta) _
                                 Order By A.id_apl Descending _
                                 Select New With { _
                                        .Rut = A.cli_idc, _
                                        .Nombre = If(A.cli_cls.id_P_0044 = 1, _
                                                     A.cli_cls.cli_rso.Trim & " " & A.cli_cls.cli_ape_ptn.Trim & " " & A.cli_cls.cli_ape_mtn.Trim, _
                                                     A.cli_cls.cli_rso.Trim), _
                                        .Nro_Apli = A.id_apl, _
                                         A.apl_fec, _
                                        .id_Ejecutivo = A.id_eje, _
                                        .Ejecutivo = A.eje.eje_des_cra, _
                                        .Monto_Exc = A.apl_exc_mto, _
                                        .Monto_Dnc = A.apl_dnc_mto, _
                                        .Monto_Cxp = A.apl_cxp_mto, _
                                        .Monto_Cxc = A.apl_cxc_mto, _
                                        .Monto_Dvg = A.apl_dvg_mto, _
                                        .Monto_Dmr = A.apl_dmr_mto, _
                                        .Devuelto = 0, _
                                        .Tasa_Cli = A.apl_tas_cli, _
                                        .Tasa_Apli = A.apl_tas_apl, _
                                        A.apl_con_dev, _
                                        A.apl_dia_int_dev, _
                                        .TipoEgreso = (From E In Data.egr_sec_cls Where E.egr_cls.id_apl = A.id_apl And E.id_P_0056 <> 5 Select E.id_P_0056).First, _
                                        .Deposito = (From E In Data.egr_sec_cls Where E.egr_cls.id_apl = A.id_apl And E.id_P_0056 <> 5 Select E.egr_dep_ant).First, _
                                        .id_Banco = (From E In Data.egr_sec_cls Where E.egr_cls.id_apl = A.id_apl And E.id_P_0056 <> 5 Select E.id_bco).First, _
                                        .CtaCte = (From E In Data.egr_sec_cls Where E.egr_cls.id_apl = A.id_apl And E.id_P_0056 <> 5 Select E.egr_cta_cte).First, _
                                        A.apl_apb_com, _
                                        .Observacion = A.apl_obs, _
                                        A.apl_fec_apb_com, _
                                        A.apl_fec_anl, _
                                        .VB = (From E In Data.egr_sec_cls Where E.egr_cls.id_apl = A.id_apl And E.id_P_0056 <> 5 Select E.egr_vld_rcz).First}).Skip(NroPaginacion)


            '   For Each A In Aplicaciones.Take(15) Asi estaba con 15 de paginacion
            For Each A In Aplicaciones.Take(Pag)


                A.Devuelto = ((Val(A.Monto_Exc) + Val(A.Monto_Dnc) + Val(A.Monto_Cxp)) - _
                              (Val(A.Monto_Cxc) + Val(A.Monto_Dvg) + Val(A.Monto_Dmr)))

                Coll.Add(A)

                'Dim Com As Char

                'If IsNothing(A.apl_apb_com) Then
                '    Com = ""
                'Else
                '    Com = A.apl_apb_com
                'End If

                'Select Case Pendiente_VB_Cursadas
                '    Case 1 'pendiente
                '        If Com = "" Or Com = Nothing Or Com = "N" Then
                '            Coll.Add(A)
                '        End If
                '    Case 2 'vb
                '        'If Com = AprobacionComercial Then
                '        If Com = "S" Then
                '            Coll.Add(A)
                '        End If
                '    Case 3 'cursadas
                '        If Com <> "" Then
                '            Coll.Add(A)
                '        End If
                'End Select



            Next

            Return Coll


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Aplicacion_Devuelve(ByVal id_Apl As Integer) As apl_cls

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Aplicacion de los cliente que se encuentran segun criterio
        'Creado por Jorge Lagos
        'Fecha Creacion: 17/02/2009
        'Quien Modifica         Fecha           Descripcion
        'A. Saldivar            09/11/2010      Se agrega busqueda por rango amplio de ejecutivo
        'A. Saldivar            04/01/2011      Se agrega paginacion
        'J. Lagos               10/01/2012      Se quita busqueda por ejecutivo
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim Aplicaciones = (From A In Data.apl_cls Where A.id_apl = id_Apl Select A).First()



            Return Aplicaciones


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Excedentes_Devuelve(ByVal RutCliente As Long, ByVal nro_aplicacion As Integer, ByVal DevuelveTodos As Boolean) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Aplicacion de los cliente que se encuentran segun criterio
        'Creado por Jorge Lagos
        'Fecha Creacion: 17/02/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection

            If DevuelveTodos Then

                Dim Excedentes = (From D In Data.doc_cls _
                                  Join I In Data.ing_sec_cls On D.id_doc Equals I.id_doc _
                                 Where D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                       D.opo_cls.ope_cls.id_P_0030 = 3 And _
                                       D.dsi_cls.id_P_0011 = 3 And _
                                       D.doc_sdo_exc > 0 And _
                                       I.ing_gen_exc = "S" And _
                                       D.dsi_cls.dsi_flj = "N" _
                                 Select D.dsi_cls.deu_ide, _
                                        Deudor = If(D.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                       D.dsi_cls.deu_cls.deu_rso.Trim & " " & D.dsi_cls.deu_cls.deu_ape_ptn.Trim & " " & D.dsi_cls.deu_cls.deu_ape_mtn.Trim, _
                                                       D.dsi_cls.deu_cls.deu_rso.Trim), _
                                        D.dsi_cls.deu_cls.deu_dig_ito, _
                                        D.opo_cls.opo_otg, _
                                        D.dsi_cls.id_P_0011, _
                                        D.dsi_cls.P_0011_cls.pnu_des, _
                                        D.dsi_cls.ope_cls.opn_cls.id_P_0031, _
                                        TD = D.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                                        D.id_doc, _
                                        D.dsi_cls.dsi_num, _
                                        D.dsi_cls.dsi_flj_num, _
                                        excedente = (D.doc_sdo_exc), _
                                        id_p_0023 = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023, _
                                        Moneda = D.opo_cls.ope_cls.opn_cls.eva_cls.P_0023_cls.pnu_des, _
                                        Cantidad_Egresos = (From E In Data.egr_sec_cls Where E.id_doc = D.id_doc And _
                                                                                             E.id_P_0055 = 3 And _
                                                                                            (E.egr_vld_rcz = "I" Or _
                                                                                             E.egr_vld_rcz = "V" Or _
                                                                                             E.egr_vld_rcz = "L") And _
                                                                                             E.egr_pro = "N").Count, _
                                        Aplicacion = (From E In Data.egr_sec_cls Where E.id_doc = D.id_doc And E.egr_cls.id_apl = nro_aplicacion).Count, _
                                        Ingresos = If(I.id_P_0053 = 2 And I.ing_vld_rcz = "L" And I.dpo_cls.id_P_0052 = 4, 1, 0)).Distinct

                For Each E In Excedentes
                    Coll.Add(E)
                Next

            Else
                Dim Excedentes = (From D In Data.doc_cls _
                                 Join I In Data.ing_sec_cls On D.id_doc Equals I.id_doc _
                                 Where D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                       D.opo_cls.ope_cls.id_P_0030 = 3 And _
                                       D.dsi_cls.id_P_0011 = 3 And _
                                       D.doc_sdo_exc > 0 And _
                                       I.ing_gen_exc = "S" And _
                                       D.dsi_cls.dsi_flj = "N" And _
                                        Not (I.id_P_0053 = 2 And _
                                    I.ing_vld_rcz = "L" And _
                                    I.dpo_cls.id_P_0052 = 4) _
                                 Select D.dsi_cls.deu_ide, _
                                        Deudor = If(D.dsi_cls.deu_cls.id_P_0044 = 1, _
                                                       D.dsi_cls.deu_cls.deu_rso.Trim & " " & D.dsi_cls.deu_cls.deu_ape_ptn.Trim & " " & D.dsi_cls.deu_cls.deu_ape_mtn.Trim, _
                                                       D.dsi_cls.deu_cls.deu_rso.Trim), _
                                        D.dsi_cls.deu_cls.deu_dig_ito, _
                                        D.opo_cls.opo_otg, _
                                        D.dsi_cls.id_P_0011, _
                                        D.dsi_cls.P_0011_cls.pnu_des, _
                                        D.dsi_cls.ope_cls.opn_cls.id_P_0031, _
                                        TD = D.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_atr_003, _
                                        D.id_doc, _
                                        D.dsi_cls.dsi_num, _
                                        D.dsi_cls.dsi_flj_num, _
                                        excedente = (D.doc_sdo_exc), _
                                        id_p_0023 = D.opo_cls.ope_cls.opn_cls.eva_cls.id_P_0023, _
                                        Moneda = D.opo_cls.ope_cls.opn_cls.eva_cls.P_0023_cls.pnu_des, _
                                        Cantidad_Egresos = (From E In Data.egr_sec_cls Where E.id_doc = D.id_doc And _
                                                                                             E.id_P_0055 = 3 And _
                                                                                            (E.egr_vld_rcz = "I" Or _
                                                                                             E.egr_vld_rcz = "V" Or _
                                                                                             E.egr_vld_rcz = "L") And _
                                                                                             E.egr_pro = "N").Count, _
                                        Aplicacion = (From E In Data.egr_sec_cls Where E.id_doc = D.id_doc And E.egr_cls.id_apl = nro_aplicacion).Count, _
                                        Ingresos = If(I.id_P_0053 = 2 And I.ing_vld_rcz = "L" And I.dpo_cls.id_P_0052 = 4, 1, 0)).Distinct

                For Each E In Excedentes
                    Coll.Add(E)
                Next

            End If


            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosNoCedidos_Devuelve(ByVal RutCliente As Long, ByVal RutDeudor As Long, ByVal NroAplicacion As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos no cedidos de un cliente con o sin deudor
        'Creado por Jorge Lagos
        'Fecha Creacion: 05/03/2009
        'Quien Modifica Fecha Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection

            If RutDeudor = 0 Then

                Dim Doctos_No_Cedidos = From N In Data.nce_cls Where N.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                                     N.nce_pro = "N" _
                                Select New With { _
                                       N.cli_idc, _
                                       N.deu_ide, _
                                      .Deudor = N.deu_cls.deu_rso, _
                                       N.id_p_0031, _
                                      .TD = N.P_0031_cls.pnu_des, _
                                       N.id_nce, _
                                       N.nce_num_doc, _
                                       N.id_p_0023, _
                                      .Moneda = N.P_0023_cls.pnu_des, _
                                      .Monto = N.nce_mto, _
                                      .Cantidad_Egresos = (From E In Data.egr_sec_cls Where E.id_nce = N.id_nce And _
                                                                                 E.egr_cls.id_apl <> NroAplicacion And _
                                                                                (E.egr_vld_rcz = "I" Or _
                                                                                 E.egr_vld_rcz = "V" Or _
                                                                                 E.egr_vld_rcz = "L") And _
                                                                                 E.egr_pro = "N").Count, _
                                      .Aplicacion = (From E In Data.egr_sec_cls Where E.id_nce = N.id_nce And _
                                                                                      E.egr_cls.id_apl = NroAplicacion).Count, _
                                      .Factor = N.fac_cam, _
                                      .Tasa = 0.0}

                For Each D In Doctos_No_Cedidos

                    Dim Ingresos = From I In Data.ing_sec_cls Where I.id_nce = D.id_nce And _
                                                                    I.id_P_0053 = 7 And _
                                                                    I.ing_vld_rcz = "L" Or _
                                                                    I.ing_vld_rcz = "S"

                    If Ingresos.Count > 0 Then

                        For Each I In Ingresos

                            Dim DoctosPagos = From P In Data.dpo_cls Where P.id_dpo = I.id_dpo And _
                                                                      P.id_P_0052 = 4

                            If DoctosPagos.Count <= 0 Then
                                Coll.Add(D)
                            End If

                        Next

                        'Else
                        '    Coll.Add(D)
                    End If

                Next

            Else

                Dim Doctos_No_Cedidos = From N In Data.nce_cls Where N.cli_idc = Format(RutCliente, Var.FMT_RUT) And _
                                                                     N.deu_ide = Format(RutDeudor, Var.FMT_RUT) And _
                                                                     N.nce_pro = "N" And _
                                                                Not (From I In Data.ing_sec_cls Where I.id_nce = N.id_nce And _
                                                                                                      I.id_P_0053 = 2 And _
                                                                                                      I.ing_vld_rcz = "L" And _
                                                                                                      I.ing_vld_rcz = "S" And _
                                                                                                     (From D In Data.dpo_cls Where D.id_dpo = I.id_dpo And _
                                                                                                                                   D.id_P_0052 = 4 _
                                                                                                      Select D.id_dpo).First _
                                                                                                      Select I.id_nce).First And _
                                                                    (From I In Data.ing_sec_cls Where I.id_nce = N.id_nce And _
                                                                                                      I.id_P_0053 = 2 And _
                                                                                                      I.ing_vld_rcz = "L" And _
                                                                                                      I.ing_vld_rcz = "S" And Not _
                                                                                                      I.dpo_cls.id_P_0052 = 4 Select I.id_nce).First _
                            Select New With { _
                                              N.cli_idc, _
                                              N.deu_ide, _
                                             .Deudor = N.deu_cls.deu_rso, _
                                              N.id_p_0031, _
                                             .TD = N.P_0031_cls.pnu_atr_003, _
                                              N.id_nce, _
                                              N.id_p_0023, _
                                             .Moneda = N.P_0023_cls.pnu_atr_003, _
                                             .Monto = N.nce_mto, _
                                             .Cantidad_Egresos = (From E In Data.egr_sec_cls Where E.id_nce = N.id_nce And _
                                                                                        E.egr_cls.id_apl <> NroAplicacion And _
                                                                                       (E.egr_vld_rcz = "I" Or _
                                                                                        E.egr_vld_rcz = "V" Or _
                                                                                        E.egr_vld_rcz = "L") And _
                                                                                        E.egr_pro = "N").Count, _
                                             .Aplicacion = (From E In Data.egr_sec_cls Where E.id_nce = N.id_nce And _
                                                                                        E.egr_cls.id_apl = NroAplicacion).Count, _
                                             .Factor = N.fac_cam, _
                                             .Tasa = 0.0}

                For Each D In Doctos_No_Cedidos
                    Coll.Add(D)
                Next

            End If


            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


#End Region

#Region "ALERTAS"

    Public Function AlertasParametros_Devuelve(ByVal CodigoEjecutivo As Integer) As pta_cls

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los parametros de alertas para un ejecutivo
        'Creado por Jorge Lagos
        'Fecha Creacion: 20/03/2009
        'Quien Modifica     Fecha   Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Alertas As pta_cls = (From A In Data.pta_cls Where A.id_eje = CodigoEjecutivo).First

            Return Alertas

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Alertas_DoctosPorVencer(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                              ByVal CodigoEjecutivo As Integer, ByVal DiasPorVencer As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos por vencer de un cliente de su ejecutivo (sp_al_retorna_datos_alertas)
        'Creado por Jorge Lagos
        'Fecha Creacion: 19/03/2009
        'Quien Modifica     Fecha   Descripcion
        'A. Saldivar        03/01/2011      Se agrega paginacion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Coll As New Collection

            Dim Doctos = (From D In Data.doc_cls Where (CLng(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) >= RutCliente_Desde And _
                                                       CLng(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) <= RutCliente_Hasta) And _
                                                      (DateDiffDay(Date.Now, CDate(D.dsi_cls.dsi_fev_rea)) <= DiasPorVencer And _
                                                       DateDiffDay(Date.Now, CDate(D.dsi_cls.dsi_fev_rea)) >= 0) And _
                                                       D.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje = CodigoEjecutivo And _
                                                       D.dsi_cls.id_P_0011 = 1 And _
                                                       D.dsi_cls.dsi_flj = "N" And _
                                                       D.doc_sdo_cli > 0 _
              Select New With {.Rut_Cliente = D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                               .Razon_Cliente = If(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                   D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim & " " & _
                                                   D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim & " " & _
                                                   D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim, _
                                                   D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim), _
                               .Rut_Deudor = D.dsi_cls.deu_ide, _
                               .Razon_Deudor = IIf(D.dsi_cls.deu_cls.id_P_0044 = 1, D.dsi_cls.deu_cls.deu_rso & " " & D.dsi_cls.deu_cls.deu_ape_ptn & " " & D.dsi_cls.deu_cls.deu_ape_mtn, D.dsi_cls.deu_cls.deu_rso), _
                               .Nro_Opo = D.opo_cls.opo_otg, _
                               .id_p_0031 = D.opo_cls.ope_cls.opn_cls.id_P_0031, _
                               .TD = D.opo_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                               .Nro_Docto = D.dsi_cls.dsi_num, _
                               .Nro_Cuota = D.dsi_cls.dsi_flj_num, _
                               .Fec_Vto_Ori = D.dsi_cls.dsi_fev_ori, _
                               .Fec_Vto_Rea = D.dsi_cls.dsi_fev_rea, _
                               .Saldo_Cli = D.doc_sdo_cli, _
                               .Saldo_Deu = D.doc_sdo_ddr, _
                               .Mon = D.opo_cls.ope_cls.opn_cls.id_P_0023, _
                               .Moneda = D.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                               .Est = D.dsi_cls.id_P_0011, _
                               .Estado = D.dsi_cls.P_0011_cls.pnu_des, _
                               .Nro_Ope = D.id_opo, _
                               .Nro_Opn = D.opo_cls.ope_cls.id_opn, _
                               .Cob_Des = D.cco_cls.cco_num, _
                               D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_dig_ito, _
                               D.dsi_cls.deu_cls.deu_dig_ito}).Skip(sesion.NroPaginacion_AlertaDoctoxVencer)



            For Each D In Doctos.Take(15)
                D.Rut_Cliente = Format(CLng(D.Rut_Cliente), "##,###,##0") & "-" & D.cli_dig_ito
                D.Rut_Deudor = Format(CLng(D.Rut_Deudor), "##,###,##0") & "-" & D.deu_dig_ito
                Coll.Add(D)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Alertas_DoctosEnMora(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                             ByVal CodigoEjecutivo As Integer, ByVal DiasEnMora As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos en mora de un cliente por su ejecutivo (sp_al_retorna_datos_alertas_1)
        'Creado por Jorge Lagos
        'Fecha Creacion: 20/03/2009
        'Quien Modifica     Fecha           Descripcion
        'A. Saldivar        03/01/2011      Se agrega paginacion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim Doctos = (From D In Data.doc_cls Where (CLng(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) >= RutCliente_Desde And _
                                                       CLng(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc) <= RutCliente_Hasta) And _
                                                       DateDiffDay(CDate(D.dsi_cls.dsi_fev_rea), Date.Now) >= DiasEnMora And _
                                                       D.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_eje_cod_eje = CodigoEjecutivo And _
                                                      (D.dsi_cls.id_P_0011 = 2 Or _
                                                       D.dsi_cls.id_P_0011 = 4 Or _
                                                       D.dsi_cls.id_P_0011 = 9 Or _
                                                       D.dsi_cls.id_P_0011 = 11 Or _
                                                       D.dsi_cls.id_P_0011 = 12) And _
                                                       D.dsi_cls.dsi_flj = "N" And _
                                                       D.doc_sdo_cli > 0 _
                        Select Rut_Cliente = D.opo_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                               Razon_Cliente = If(D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.id_P_0044 = 1, _
                                                  D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim & " " & _
                                                  D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim & " " & _
                                                  D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_ape_mtn.Trim, _
                                                  D.opo_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_rso.Trim), _
                               Rut_Deudor = D.dsi_cls.deu_ide, _
                               Razon_Deudor = IIf(D.dsi_cls.deu_cls.id_P_0044 = 1, D.dsi_cls.deu_cls.deu_rso & " " & D.dsi_cls.deu_cls.deu_ape_ptn & " " & D.dsi_cls.deu_cls.deu_ape_mtn, _
                                                  D.dsi_cls.deu_cls.deu_rso), _
                               Nro_Opo = D.opo_cls.opo_otg, _
                               TD = D.opo_cls.ope_cls.opn_cls.id_P_0031, _
                               Nro_Docto = D.dsi_cls.dsi_num, _
                               Nro_Cuota = D.dsi_cls.dsi_flj_num, _
                               Fec_Vto_Ori = D.dsi_cls.dsi_fev_ori, _
                               Fec_Vto_Rea = D.dsi_cls.dsi_fev_rea, _
                               Saldo_Cli = D.doc_sdo_cli, _
                               Saldo_Deu = D.doc_sdo_ddr, _
                               Mon = D.opo_cls.ope_cls.opn_cls.id_P_0023, _
                               Moneda = D.opo_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                               Est = D.opo_cls.ope_cls.id_P_0030, _
                               Estado = D.opo_cls.ope_cls.P_0030_cls.pnu_des, _
                               Nro_Ope = D.id_opo, _
                               Nro_Opn = D.opo_cls.ope_cls.id_opn, _
                               Cob_Des = D.cco_cls.cco_num).Skip(sesion.NroPaginacion_DoctoEnMora)

            For Each D In Doctos.Take(15)
                Coll.Add(D)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Alertas_Lineas(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                    ByVal CodigoEjecutivo As Integer, ByVal DiasDeLinea As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las Linea de credito Vencidas o sobregiradas de un cliente por su ejecutivo (sp_al_retorna_datos_alertas_4)
        'Creado por Jorge Lagos
        'Fecha Creacion: 20/03/2009
        'Quien Modifica     Fecha           Descripcion
        'A. Saldivar        03/01/2011      Se agrega paginacion
        'S. Henriquez       11/07/2012      Se modifica condicion para vencimiento de linea
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim Lineas = (From L In Data.ldc_cls Where (CLng(L.cli_idc) >= RutCliente_Desde And _
                                                       CLng(L.cli_idc) <= RutCliente_Hasta) And _
                                                       (DateDiffDay(Date.Now, CDate(L.ldc_fec_vig_hta)) <= DiasDeLinea Or (L.ldc_mto_apb - L.ldc_mto_ocp) < 0) And _
                                                       L.cli_cls.id_eje_cod_eje = CodigoEjecutivo And _
                                                       L.id_P_0029 = 1 _
                         Select Rut_Cliente = L.cli_idc, _
                                Razon_Cliente = If(L.cli_cls.id_P_0044 = 1, _
                                                   L.cli_cls.cli_rso.Trim & " " & _
                                                   L.cli_cls.cli_ape_ptn.Trim & " " & _
                                                   L.cli_cls.cli_ape_mtn.Trim, _
                                                   L.cli_cls.cli_rso.Trim), _
                                Nro_Linea = L.id_ldc, _
                                Fecha_Vig = L.ldc_fec_vig_dde, _
                                Fecha_Vto = L.ldc_fec_vig_hta, _
                                Adm_Mora = If(L.ldc_adm_mor = "S", "SI", "NO"), _
                                Mto_Aprobado = L.ldc_mto_apb, _
                                Mto_Ocupado = L.ldc_mto_ocp, _
                                Est = L.id_P_0029, _
                                Estado = L.P_0029_cls.pnu_des).Skip(sesion.NroPaginacion_AlertaEnLinea)

            For Each L In Lineas.Take(15)
                Coll.Add(L)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try
        '((DateDiffDay(Date.Now, CDate(L.ldc_fec_vig_dde)) <= DiasDeLinea And _
        '  DateDiffDay(Date.Now, CDate(L.ldc_fec_vig_hta)) >= 0) Or _
    End Function

    Public Function Alertas_Excedentes(ByVal RutCliente_Desde As Long, ByVal RutCliente_Hasta As Long, _
                                            ByVal CodigoEjecutivo As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los excedentes, distribucion de doctos y deuda de los cliente perteneciente a un ejecutivo
        'Creado por Jorge Lagos
        'Fecha Creacion: 20/03/2009
        'Quien Modifica     Fecha   Descripcion
        'A. Saldivar        03/01/2011      Se agrega paginacion
        '**************************************************************************************************************************************************

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll As New Collection



            Dim Excedentes = (From R In Data.rsc_cls _
                             Group Join D In Data.dsb_cls _
                             On R.cli_idc Equals D.cli_idc _
                             Into mor_001 = Sum(D.dsb_mor_001), _
                                  mor_002 = Sum(D.dsb_mor_002), _
                                  mor_003 = Sum(D.dsb_mor_003), _
                                  mor_004 = Sum(D.dsb_mor_004), _
                                  mor_005 = Sum(D.dsb_mor_005) _
                             Where (CLng(R.cli_idc) >= RutCliente_Desde And CLng(R.cli_idc) <= RutCliente_Hasta) And _
                                   R.cli_cls.id_eje_cod_eje = CodigoEjecutivo And _
                                   R.rsc_mto_exd > 0 _
                             Select Rut_Cliente = R.cli_idc, _
                                    Razon_Cli = If(R.cli_cls.id_P_0044 = 1, _
                                                   R.cli_cls.cli_rso.Trim & " " & _
                                                   R.cli_cls.cli_ape_ptn.Trim & " " & _
                                                   R.cli_cls.cli_ape_mtn.Trim, _
                                                   R.cli_cls.cli_rso.Trim), _
                                    Mto_Vig = R.rsc_mto_vig, _
                                    mor_001, _
                                    mor_002, _
                                    mor_003, _
                                    mor_004, _
                                    mor_005, _
                                    Mto_Exc = R.rsc_mto_exd, _
                                    Mto_CxP = R.rsc_mto_cxp, _
                                    Mto_CxC = R.rsc_mto_cxc, _
                                    Mto_DNC = R.rsc_mto_dnc).Skip(sesion.NroPaginacion_AlertaExcedente)



            For Each E In Excedentes.Take(15)
                Coll.Add(E)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Sistema"

    Public Function SistemaDevuelveDatos() As sis_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los datos de sistema
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 16/06/2008
        'Quien Modifica              Fecha              Descripcion

        '*********************************************************************************************************************************
        Dim sistema As New sis_cls

        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            sistema = (From N In Data.sis_cls Select N).First

            Return sistema




        Catch ex As Exception

        End Try

    End Function

#End Region

#Region "Documentos y Otras Condiciones para Comercial"

    Public Function DocConComDevuelve(ByVal DoctoCon As String) As IQueryable

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos comerciales o otras condiciones 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************


        Try

            Dim data As New DataClsFactoringDataContext

            If DoctoCon = "D" Then

                Dim doctos = From R In data.doc_com_cls Order By R.id_doc_com _
                             Select id = R.id_doc_com, des = R.des_doc_com, est = R.est_doc_com, tipo = R.id_tipo
                Return doctos

            Else

                Dim con = From R In data.con_com_cls Order By R.id_con_com _
                          Select id = R.id_con_com, des = R.des_con_com, est = R.est_con_com, tipo = 0

                Return con

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocConComDevuelvePorTipoDocto(ByVal DoctoCon As String, ByVal TipoDeDocumento As Integer, ByVal Tipo As Integer) As IQueryable

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos comerciales o otras condiciones por Tipo de Documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************


        Try

            Dim data As New DataClsFactoringDataContext

            If DoctoCon = "D" Then

                Dim doctos = From R In data.dxd_cls Where R.id_p_031 = TipoDeDocumento And R.doc_com_cls.id_tipo = Tipo And R.doc_com_cls.est_doc_com = "1" Order By R.id_dxd _
                             Select id = R.id_dxd, des = R.doc_com_cls.des_doc_com
                Return doctos

            Else

                Dim con = From R In data.cxd_cls Where R.id_p_0031 = TipoDeDocumento And R.con_com_cls.est_con_com = "1" Order By R.id_cxd _
                          Select id = R.id_cxd, des = R.con_com_cls.des_con_com
                Return con

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocConComDevuelvePorTipoDocto(ByVal DoctoCon As String, ByVal TipoDeDocumento As Integer) As IQueryable

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos comerciales o otras condiciones por Tipo de Documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************


        Try

            Dim data As New DataClsFactoringDataContext

            If DoctoCon = "D" Then

                Dim doctos = From R In data.dxd_cls Where R.id_p_031 = TipoDeDocumento And R.doc_com_cls.est_doc_com = "1" And R.doc_com_cls.id_tipo = 1 Order By R.id_dxd _
                             Select id = R.id_doc_com, des = R.doc_com_cls.des_doc_com, R.id_dxd
                Return doctos

            ElseIf DoctoCon = "L" Then

                Dim doctos = From R In data.dxd_cls Where R.id_p_031 = TipoDeDocumento And R.doc_com_cls.est_doc_com = "1" And R.doc_com_cls.id_tipo = 2 Order By R.id_dxd _
                             Select id = R.id_doc_com, des = R.doc_com_cls.des_doc_com, R.id_dxd
                Return doctos

            Else
                'Condiciones
                Dim con = From R In data.cxd_cls Where R.id_p_0031 = TipoDeDocumento And R.con_com_cls.est_con_com = "1" Order By R.id_cxd _
                          Select id = R.id_con_com, des = R.con_com_cls.des_con_com, R.id_cxd
                Return con

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocConDevuelvePorNegociacionPorTipo(ByVal DoctoCon As String, ByVal NroOpn As Integer) As IQueryable


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos comerciales por negociacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            If DoctoCon = "D" Then

                Dim doctos = From R In data.dxn_cls Where R.id_opn = NroOpn Order By R.id_dxn _
                                 Select id = R.id_dxd
                Return doctos

            Else

                Dim con = From R In data.cxn_cls Where R.id_opn = NroOpn Order By R.id_cxd _
                          Select id = R.id_cxd
                Return con

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocConDevuelvePorNegociacion(ByVal NroOpn As Integer, ByVal tipo As Integer) As IQueryable


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve los documentos comerciales por negociacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 19-05-2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext


            Dim doctos = From R In data.dxn_cls Where R.id_opn = NroOpn And R.dxd_cls.doc_com_cls.id_tipo = tipo Order By R.id_dxn _
                                    Select id = R.id_dxn, des = R.dxd_cls.doc_com_cls.des_doc_com, usuario = R.eje_cls.eje_des_cra, fecha = R.dxn_fec_apb, estado = R.est_dxd

            Return doctos

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Fechas, Municipio, Sucursales y Feriados"

    Public Function calcula_vcto_real(ByVal rutdeu As String, _
                                      ByVal fecha As Date, _
                                      ByVal sucursal As Integer, _
                                      ByVal plaza As String, _
                                      ByVal tipo As Integer) As Date

        Dim CMC As New ClaseComercial
        Dim col As New Collection

        col = CMC.DiasDeRetencionDevuelve(sucursal, plaza, tipo)

        Dim DIAS_POR_PLAZA As Integer
        Dim BUSCA_DIA_HABIL As Boolean

        If Not IsNothing(col) Then
            If col.Count > 0 Then
                DIAS_POR_PLAZA = IIf(IsDBNull(col.Item(1)), 0, col.Item(1))
                If IsDBNull(col.Item(2)) Then
                    BUSCA_DIA_HABIL = False
                Else
                    BUSCA_DIA_HABIL = IIf(Trim(col.Item(2)) = "S", True, False)
                End If
            Else
                BUSCA_DIA_HABIL = True
                DIAS_POR_PLAZA = 0
            End If
        End If

        FECHA_VCTO_AUX = fecha

        '------------------------------------------------------------------------------------
        'jlagos 29-05-2012 se agrega calanderizacion de pagos por deudor

        If rutdeu = "" Then
            rutdeu = 0
        End If

        FECHA_VCTO_AUX = DevuelveCalendarioPagoDeudor(rutdeu, FECHA_VCTO_AUX)
        '------------------------------------------------------------------------------------

        If BUSCA_DIA_HABIL Then
            FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_AUX)
        End If

        FECHA_VCTO_CALCULO = FECHA_VCTO_AUX

        For i = 1 To DIAS_POR_PLAZA
            FECHA_VCTO_CALCULO = DateAdd("D", 1, FECHA_VCTO_CALCULO)

            If BUSCA_DIA_HABIL Then
                FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
            End If
        Next

        If DIAS_POR_PLAZA = 0 Then

            If BUSCA_DIA_HABIL Then

                FECHA_VCTO_CALCULO = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
                FECHA_VCTO_AUX = CMC.DiaHabilDevuelve(FECHA_VCTO_CALCULO)
            Else
                FECHA_VCTO_AUX = fecha
            End If

        End If

        Return FECHA_VCTO_CALCULO

    End Function

    Public Sub MunicipioDevuelve(ByVal depto As Integer, Optional ByVal LlenaDrop As Boolean = True, Optional ByVal dp As DropDownList = Nothing)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las ciudades
        'Creado por= Sebastian Henriquez C.                                                                                                                       
        'Fecha Creacion: 22/05/2012                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext



            Dim Municipios = From P In data.ciu_cls Where P.id_p_001 = depto Order By P.ciu_des _
                           Select CodigoCiudad = P.id_ciu, _
                                  NombreCiudad = P.ciu_des

            If LlenaDrop Then
                Dim Rg As New FuncionesGenerales.RutinasWeb
                Rg.Llenar_Drop(Municipios, "CodigoCiudad", "NombreCiudad", dp)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Public Function SucursalValidaCodInt(ByVal cod As String) As Boolean

        '**************************************************************************************************************************************************

        'Descripcion: Valida si codigo interno se encuentra en la base

        'Creado por : Sebastian Henriquez C.

        'Fecha Creacion: 24/05/2012

        'Quien Modifica              Fecha              Descripcion

        '**************************************************************************************************************************************************

        Try

            Dim data As New CapaDatos.DataClsFactoringDataContext



            Dim suc = (From s In data.suc_cls Where s.suc_cod_ftg = cod Select s.id_suc).First



            If suc <> Nothing Then

                Return False

            Else

                Return True

            End If



        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function Feriados_Año(ByVal año As Integer) As Collection
        'Descripcion: Devuelve Feriados correspondiente al año de consulta
        'Creado por= Fabian Yañez V.
        'Fecha Creacion: 09/06/2012
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim fer_Obj As fer_cls


            Dim Paridad = From par In Data.fer_cls Where par.fer_fec.Year = año Order By par.fer_fec Select par.fer_fec.ToShortDateString


            If Paridad.Count <> 0 Then

                For Each P In Paridad

                    fer_Obj = New fer_cls

                    With fer_Obj
                        .fer_fec = P

                    End With

                    Coll.Add(P)

                Next

                Return Coll

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Varios Metodos"

    Public Sub DoctoDevuelvePorOpe(ByVal GV As GridView, ByVal ID_Ope As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Actas asociadas a numero de linea
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 19/06/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim docto = From D In data.doc_dig_ope_cls Where D.id_ope = ID_Ope Select D

            GV.DataSource = docto
            GV.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub GestionDevuelveDocto(ByVal GV As GridView, ByVal id_d As Integer)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve archivos asociadas a numero de documento
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 21/06/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim id As Integer = (From d In data.doc_cls Where d.id_doc = CDec(id_d) Select d.opo_cls.id_ope).First()

            Dim docto = From D In data.doc_dig_ope_cls Where D.id_ope = id Select D

            GV.DataSource = docto
            GV.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Function DevuelveID_DVF(ByVal id As Integer) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Devuelve id verificacion
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 06/08/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim dato As Integer = (From A In data.dvf_cls Where A.dvf_num = id Select A.id_dvf).First
            Return dato
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function CalificacionesDevuelvePorCli(ByVal ID_DSI As Decimal) As clf_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Calificaciones por cliente
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 04/07/2012
        'Quien Modifica              Fecha              Descripcion
        'S Henriquez                27/09/2012          Se cambia ID
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim datos As clf_cls = (From A In data.clf_cls Where A.id_dsi = ID_DSI Select A).First

            Return datos

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ValidaP_Riesgo(ByVal id As Integer) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Valida si ejecutivo tiene perfil de area riesgo
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 28/09/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext
            Dim riesgoArray() As Integer
            riesgoArray = New Integer() {5, 12, 33}


            Dim Riesgo As Integer = (From r In data.nef_cls Where r.id_eje = id And riesgoArray.Contains(r.id_P0045)).Count()
            If Riesgo > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DevuelveIntDvgMora(ByVal iddoc As Double, ByVal fechapago As DateTime) As Double

        Try

            Dim data As New DataClsFactoringDataContext
            Dim intdvgmor As Double

            Dim fecpag As DateTime = (From d In data.dco_cls _
                                       Join o In data.doc_cls On o.id_doc Equals d.ID_DOC _
                                       Where d.ID_DOC = iddoc And _
                                             (d.DCO_TIP_VAL = "MC" Or d.DCO_TIP_VAL = "MV") And _
                                             d.DCO_FEC_DCO < fechapago _
                                             Order By d.DCO_FEC_DCO Descending _
                                             Select d.DCO_FEC_DCO).First

            Dim interes = From d In data.dco_cls _
                                       Join o In data.doc_cls On o.id_doc Equals d.ID_DOC _
                                       Where d.ID_DOC = iddoc And _
                                             (d.DCO_TIP_VAL = "MC" Or d.DCO_TIP_VAL = "MV") And _
                                             d.DCO_FEC_DCO = fecpag _
                           Select monto = d.DCO_MON_DCO, _
                                  tipo = d.DCO_TIP_VAL, _
                                  FECHA = d.DCO_FEC_DCO, _
                                  dias = fecpag.Subtract(o.dsi_cls.dsi_fev_rea.Value).Days

            'Interes a la fecha de pago
            For Each i In interes

                If i.dias < 90 And i.tipo = "MC" Then
                    intdvgmor = i.monto
                ElseIf i.dias > 90 And i.tipo = "MV" Then
                    intdvgmor = i.monto
                End If

            Next

            Return intdvgmor

        Catch ex As Exception
            'No tiene calculo de intereses (cierre de interes de mora (devengo))
            Return 0
        End Try

    End Function

    Public Function DevuelveIntDelDia(ByVal iddoc As Double, ByVal fechapago As DateTime) As Double

        Try

            Dim data As New DataClsFactoringDataContext
            Dim intdvgmor As Double

            Dim fecpag As DateTime = (From d In data.dco_cls _
                                       Join o In data.doc_cls On o.id_doc Equals d.ID_DOC _
                                       Where d.ID_DOC = iddoc And _
                                             (d.DCO_TIP_VAL = "MC" Or d.DCO_TIP_VAL = "MV") And _
                                             d.DCO_FEC_DCO < fechapago _
                                             Order By d.DCO_FEC_DCO Descending _
                                             Select d.DCO_FEC_DCO).First

            Dim interesdia = From d In data.dco_cls _
                                       Join o In data.doc_cls On o.id_doc Equals d.ID_DOC _
                                       Where d.ID_DOC = iddoc And _
                                             (d.DCO_TIP_VAL = "MC" Or d.DCO_TIP_VAL = "MV") And _
                                             d.DCO_FEC_DCO < Date.Now.ToShortDateString() _
                                             Order By d.DCO_FEC_DCO Descending _
                           Select monto = d.DCO_MON_DCO, _
                                  tipo = d.DCO_TIP_VAL, _
                                  FECHA = d.DCO_FEC_DCO, _
                                  dias = fecpag.Subtract(o.dsi_cls.dsi_fev_rea.Value).Days

            'Interes a la fecha de pago
            For Each i In interesdia

                If i.dias < 90 And i.tipo = "MC" Then
                    intdvgmor = i.monto
                ElseIf i.dias > 90 And i.tipo = "MV" Then
                    intdvgmor = i.monto
                End If

                If intdvgmor <> 0 Then Exit For

            Next

            Return intdvgmor

        Catch ex As Exception
            'No tiene calculo de intereses (cierre de interes de mora (devengo))
            Return 0
        End Try

    End Function

    Public Function DevuelveSiTuvoUnPagoElMismoDia(ByVal iddoc As Double, ByVal fechapago As DateTime) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un verdadero si tuvo un abono durante el mismo dia
        'Creado por= Jorge Lagos
        'Fecha Creacion: 13/08/2013
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim pagos = From d In data.ing_sec_cls _
                                       Where d.id_doc = iddoc And _
                                             d.ing_cls.ing_fec = fechapago And _
                                             (d.ing_vld_rcz.Equals("V") Or d.ing_vld_rcz.Equals("L")) _
                        Select d

            If pagos.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            'No tiene calculo de intereses (cierre de interes de mora (devengo))
            Return 0
        End Try

    End Function

    Public Function DevuelveFechaUltimoPago(ByVal iddoc As Double, ByVal fechapago As DateTime) As DateTime
        '*********************************************************************************************************************************
        'Descripcion: Devuelve la fecha del ultimo pago validado para el documento
        'Creado por= Jorge Lagos
        'Fecha Creacion: 13/08/2013
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim pagos As DateTime = (From d In data.ing_sec_cls _
                                       Where d.id_doc = iddoc And _
                                             (d.ing_vld_rcz.Equals("V") Or d.ing_vld_rcz.Equals("L")) _
                                             Order By d.ing_cls.ing_fec Descending _
                                             Select d.ing_cls.ing_fec).First

            Return pagos

        Catch ex As Exception
            Return fechapago
        End Try

    End Function


    Public Function Devolver_DTF(ByVal fecha As DateTime) As Decimal


        Dim sqlstr As String = ""
        Dim sqlquery As New FuncionesGenerales.SqlQuery
        Dim ds As DataSet
        Dim valor As Decimal = 0

        Try

            sqlstr = "EXEC SP_PLN_DEVOLVER_DTF_SEMANAL '" & RG.FUNFechaJul(fecha.ToShortDateString()) & "'"

            ds = sqlquery.ExecuteDataSet(sqlstr)

            If ds.Tables(0).Rows.Count > 0 Then
                valor = CDec(ds.Tables(0).Rows(0)(1).ToString())
            End If


        Catch ex As Exception
            valor = 0
        Finally
            ds.Dispose()
        End Try

        Return valor


    End Function

    Public Function Devolver_EA(ByVal tasabase As Decimal, ByVal spread As Decimal) As Decimal


        Dim sqlstr As String = ""
        Dim sqlquery As New FuncionesGenerales.SqlQuery
        Dim ds As DataSet
        Dim valor As Decimal = 0

        Try

            sqlstr = "SELECT dbo.UFN_DEVUELVETASA_EA (" & RG.comasXptos(tasabase.ToString()) & ", " _
                                                        & RG.comasXptos(spread.ToString()) & ")"

            ds = sqlquery.ExecuteDataSet(sqlstr)

            If ds.Tables(0).Rows.Count > 0 Then
                valor = CDec(ds.Tables(0).Rows(0)(0).ToString())
            End If


        Catch ex As Exception
            valor = 0
        Finally
            ds.Dispose()
        End Try

        Return valor


    End Function


#End Region

End Class



