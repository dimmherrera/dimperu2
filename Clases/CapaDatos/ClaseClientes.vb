Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports CapaDatos

Public Class ClaseClientes

    Dim Var As New FuncionesGenerales.Variables
    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim FC As New FuncionesGenerales.FComunes
    Dim sql As New FuncionesGenerales.SqlQuery

#Region "CONSULTAS "

    Public Function ClienteDevuelvePorRut(ByVal Rut As String) As cli_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un cliente por su Rut 
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 14/06/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************
        Try


            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Clientes As cli_cls = (From Cli In Data.cli_cls _
                          Where Cli.cli_idc = Format(CLng(Rut), Var.FMT_RUT) _
                          Select Cli).First

            Return ClientesDevuelve(Clientes.cli_idc, Clientes.cli_dig_ito)

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    'Public Function ClienteDevuelvePorRut(ByVal Rut As String) As DataSet

    '    Try
    '        Dim Clientes As New DataSet

    '        Clientes = sql.ExecuteDataSet("select * from cli where cli_idc = " & Format(CLng(Rut), Var.FMT_RUT))

    '        If Not IsNothing(Clientes) And Clientes.Tables(0).Rows.Count > 0 Then

    '            Return ClientesDevuelve(Clientes.Tables(0).Rows(0)("cli_idc").ToString(), Clientes.Tables(0).Rows(0)("cli_dig_ito").ToString())

    '        Else
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        Return Nothing
    '    Finally

    '    End Try

    'End Function

    'obtiene el numero de clientes activos 
    'Public Function CantidadClientesActivos() As Integer
    '    Dim cantActivos As Integer
    '    Dim Data As New DataClsFactoringDataContext

    '    cantActivos = (From C In Data.cli_cls _
    '                  Where C.id_P_008 = 1 _
    '                  Select C).Count
    '    Return cantActivos
    'End Function

    Public Function ClientesDeudoresDevuelve(ByVal RutDeu As String, ByVal Estado As Char, ByVal LlenaGrid As Boolean, _
                                            Optional ByVal Gv As GridView = Nothing, _
                                            Optional ByVal CantPag As Integer = 9999) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los clientes asociados a un Deudor 
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 25/05/2008
        'Quien Modifica              Fecha          Descripcion
        'A Saldivar                  14/01/2011     Se agrega paginacion
        'A Saldivar                  14/01/2011     Se agrega variable cantidad de paginacion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim coll As New Collection

            If Estado = "T" Then

                Dim ClientesAsociados = (From DDR In Data.ddr_cls _
                               Where CLng(DDR.deu_ide) = Format(CLng(RutDeu), Var.FMT_RUT) _
                               Order By DDR.cli_idc _
                               Select New With {.cli_idc = DDR.cli_idc, _
                                                .cli_rso = If(DDR.cli_cls.id_P_0044 = 1, _
                                                              DDR.cli_cls.cli_rso.Trim & "  " & _
                                                              DDR.cli_cls.cli_ape_ptn.Trim & "  " & _
                                                              DDR.cli_cls.cli_ape_mtn.Trim, _
                                                              DDR.cli_cls.cli_rso.Trim)}).Skip(sesion.NroPaginacion_Deu).Take(CantPag)


                If LlenaGrid Then
                    Gv.DataSource = ClientesAsociados
                    Gv.DataBind()
                Else
                    Return ClientesAsociados
                End If

            End If

            If Estado = "A" Then

                Dim ClientesAsociados = (From DDR In Data.ddr_cls _
                               Where CLng(DDR.deu_ide) = RutDeu And DDR.cli_cls.id_P_008 = 1 _
                               Select New With {.cli_idc = DDR.cli_idc, _
                                                .cli_rso = If(DDR.cli_cls.id_P_0044 = 1, _
                                                              DDR.cli_cls.cli_rso.Trim & "  " & _
                                                              DDR.cli_cls.cli_ape_ptn.Trim & "  " & _
                                                              DDR.cli_cls.cli_ape_mtn.Trim, _
                                                              DDR.cli_cls.cli_rso.Trim)}).Skip(sesion.NroPaginacion_Deu).Take(CantPag)


                If LlenaGrid Then
                    Gv.DataSource = ClientesAsociados
                    Gv.DataBind()
                Else
                    Return ClientesAsociados
                End If

            End If

            For I = 0 To Gv.Rows.Count - 1

                Dim Rut As Long = Gv.Rows(I).Cells(0).Text
                Gv.Rows(I).Cells(0).Text = Format(Rut, Fmt.FCMSD) & "-" & RG.Vrut(Rut)

            Next

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function ClientesDevuelve(ByVal Rut As String, ByVal digito As String) As cli_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un cliente por su Rut y ejecutivo que esta conectado
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 16/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLagos                      10/05/2008        -Se incorpora la descripcion de parametros
        'JLagos                      26/03/2009        -Se agrega criterio de busqueda por ejecutivo
        'JLagos                      26/03/2009        -Se saca el criterio de busqueda por ejecutivo
        'ASaldivar                   05/07/2010        -Se agrega validacion del estado de cliente
        'Carce                       10/08/2011        -Se saca mensajes por cliente vetado, con problemas, Deshabilitado, no existe y caducado
        '						                        los mensajes se ubican en la pantalla de ingreso o mantencion de clientes ahora.
        '**************************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext


            valida_cliente = ""
            'No trae cliente
            If Rut = "" Then
                valida_cliente = "Debe Ingresar NIT del Cliente"
                Exit Function
            End If

            'No trae digito
            If digito = "" Then
                valida_cliente = "Debe Ingresar Digito Verificador"
                Exit Function
            End If

            Dim Clientes As cli_cls
            Dim sucursales = From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr Select n.id_suc

            Try

                Rut = Rut.Replace(",", "")

                Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.eje_des_cra = Usr Select e).First

                Clientes = (From Cli In Data.cli_cls _
                          Where Cli.cli_idc = Format(CLng(Rut), Var.FMT_RUT) _
                          Select Cli).First

            Catch ex As Exception

            End Try

            If IsNothing(Clientes) Then

                For Each s In sucursales

                    Try

                        Clientes = (From Cli In Data.cli_cls _
                                  Where Cli.cli_idc = Format(CLng(Rut), Var.FMT_RUT) And _
                                        Cli.id_suc = s _
                                  Select Cli).First

                        If Not IsNothing(Clientes) Then
                            Return Clientes
                        End If

                    Catch ex As Exception

                    End Try


                Next

            End If

            If IsNothing(Clientes) Then
                valida_cliente = "Cliente no trabaja con ninguna de las sucursales del ejecutivo conectado..."
                Exit Function
            End If

            ''Digito Incorrecto
            'If UCase(digito) <> UCase(CStr(Clientes.cli_dig_ito)) Then
            '    valida_cliente = "Digito Incorrecto"
            '    Exit Function
            'End If miguel 

            Return Clientes

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Public Function ClientesDevuelve(ByVal Rut As String, ByVal digito As String) As DataSet

    '    Try
    '        Dim Clientes As New DataSet
    '        Dim sucursales As New DataSet
    '        Dim ejecutivo As DataSet
    '        Dim sqlstr As String

    '        valida_cliente = ""
    '        'No trae cliente
    '        If Rut = "" Then
    '            valida_cliente = "Debe Ingresar NIT del Cliente"
    '            Exit Function
    '        End If

    '        'No trae digito
    '        If digito = "" Then
    '            valida_cliente = "Debe Ingresar Digito Verificador"
    '            Exit Function
    '        End If

    '        sqlstr = ""

    '        sucursales = sql.ExecuteDataSet("select n.id_suc from nes n inner join eje e on e.id_eje = n.id_eje and e.eje_des_cra = '" & Usr & "'")

    '        Try
    '            Rut = Rut.Replace(",", "")

    '            ejecutivo = sql.ExecuteDataSet("select * from eje where eje_des_cra = '" & Usr)

    '            Clientes = sql.ExecuteDataSet("select * from cli where cli_idc = " & Format(CLng(Rut), Var.FMT_RUT) & "")

    '        Catch ex As Exception

    '        End Try

    '        If IsNothing(Clientes) Or Clientes.Tables(0).Rows.Count = 0 Then

    '            For i = 0 To sucursales.Tables(0).Rows.Count - 1

    '                Try
    '                    Clientes = sql.ExecuteDataSet("select * from cli where cli_idc = " & Format(CLng(Rut), Var.FMT_RUT) & " and id_suc = " & sucursales.Tables(0).Rows(i)("id_suc").ToString())

    '                    If Not IsNothing(Clientes) Or Clientes.Tables(0).Rows.Count > 0 Then
    '                        Return Clientes
    '                    End If

    '                Catch ex As Exception

    '                End Try

    '            Next
    '        End If

    '        If IsNothing(Clientes) Or Clientes.Tables(0).Rows.Count = 0 Then
    '            valida_cliente = "Cliente no trabaja con ninguna de las sucursales del ejecutivo conectado..."
    '            Exit Function
    '        End If

    '        If UCase(digito) <> UCase(CStr(Clientes.Tables(0).Rows(0)("cli_dig_ito").ToString())) Then
    '            valida_cliente = "Digito Incorrecto"
    '            Exit Function
    '        End If

    '        Return Clientes


    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

    Public Function RelacionClienteDeudorDevuelve(ByVal RutDeu As String, ByVal Estado As Char, ByVal cliente As String) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los clientes asociados a un Deudor 
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 25/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            If Estado = "T" Then

                Dim ClientesAsociados = From DDR In Data.ddr_cls _
                               Where CLng(DDR.deu_ide) = CLng(RutDeu) _
                               Select New With {.cli_idc = DDR.cli_idc, _
                                                        .cli_rso = DDR.cli_cls.cli_rso.Trim & "  " & DDR.cli_cls.cli_ape_ptn.Trim & "  " & DDR.cli_cls.cli_ape_mtn.Trim}

                ''If LlenaGrid Then
                ''    Gv.DataSource = ClientesAsociados
                ''    Gv.DataBind()
                ''Else 
                'Return ClientesAsociados
                '      End If

            End If

            If Estado = "A" Then

                Dim ClientesAsociados = From DDR In Data.ddr_cls _
                               Where CLng(DDR.deu_ide) = CLng(RutDeu) And DDR.cli_cls.id_P_008 = 1 And CLng(DDR.cli_cls.cli_idc) = CLng(cliente) _
                               Select New With {.cli_idc = DDR.cli_idc, _
                                                        .cli_rso = DDR.cli_cls.cli_rso.Trim & "  " & DDR.cli_cls.cli_ape_ptn.Trim & "  " & DDR.cli_cls.cli_ape_mtn.Trim}

                'If LlenaGrid Then
                '    Gv.DataSource = ClientesAsociados
                '    Gv.DataBind()
                'Else

                If ClientesAsociados.Count = 0 Then
                    Return False
                End If

                For Each o In ClientesAsociados

                    If o.cli_idc = cliente Then
                        Return True
                    Else
                        Return False
                    End If

                Next

            End If



        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Sub ClientesActivosDevuelveTodos(ByVal GV As GridView, ByVal Rut_Dsd As Double, ByVal Rut_Hst As Double, _
                                            ByVal CodEje_Dsd As Integer, ByVal CodEje_Hst As Integer, _
                                            ByVal TipCli_Dsd As Integer, ByVal TipCli_Hst As Integer, _
                                            ByVal Razon As String, ByVal CampoDeOrden As Integer, _
                                            Optional ByVal todos As Boolean = False)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los clientes por rango en Rut y ejecutivos que se encuentren activos                  
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 16/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                 
        'JLagos                    28/05/2008         Se agrego el campo de la tabla P_0044 (Tipo de Cliente)
        'JLagos                    28/05/2009         Se agrego el campo digito, por lo que se quita donde lo calculaba
        'CArce                     08/08/2011         Se agrego opcion para que retorne todos los clientes sin importar estado.  
        'JLagos                    16/08/2012         Se cambio el tipo de dato RUT a double
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.eje_des_cra.Trim().ToUpper() = Usr.Trim().ToUpper() Select e).First()


            Select Case CampoDeOrden

                Case 1

                    'Orden Por rut
                    If todos = False Then

                        Dim Clientes = (From Cli In Data.cli_cls _
                                            Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                                  CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                       Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                       Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                       Cli.id_P_0044 >= TipCli_Dsd And _
                                                       Cli.id_P_0044 <= TipCli_Hst And _
                                                       (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) And _
                                                       Cli.id_P_008 = 1 Order By Cli.cli_idc _
                               Select New With {.cli_idc = Cli.cli_idc, _
                                                .digito = Cli.cli_dig_ito, _
                                                .cli_rso = If(Cli.id_P_0044 = 1, _
                                                              Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                              Cli.cli_rso), _
                                                .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next
                        ' agregado por cristian arce salgado
                    Else
                        Dim Clientes = (From Cli In Data.cli_cls _
                                            Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                                  CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                       Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                       Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                       Cli.id_P_0044 >= TipCli_Dsd And _
                                                       Cli.id_P_0044 <= TipCli_Hst And _
                                                      (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                               Select New With {.cli_idc = Cli.cli_idc, _
                                                .digito = Cli.cli_dig_ito, _
                                                .cli_rso = If(Cli.id_P_0044 = 1, _
                                                              Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                              Cli.cli_rso), _
                                                .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next
                    End If
                    'fin
                Case 2
                    If todos = False Then
                        'Orden por Nombre
                        Dim Clientes = (From Cli In Data.cli_cls _
                                             Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                                   CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                       Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                       Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                       Cli.id_P_0044 >= TipCli_Dsd And _
                                                       Cli.id_P_0044 <= TipCli_Hst And _
                                                      (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) And _
                                                       Cli.id_P_008 = 1 Order By Cli.cli_rso _
                               Select New With {.cli_idc = Cli.cli_idc, _
                                                .digito = Cli.cli_dig_ito, _
                                                .cli_rso = If(Cli.id_P_0044 = 1, _
                                                              Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                              Cli.cli_rso), _
                                                .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next
                        ' agregado por cristian arce salgado
                    Else
                        Dim Clientes = (From Cli In Data.cli_cls _
                                            Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                                  CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                      Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                      Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                      Cli.id_P_0044 >= TipCli_Dsd And _
                                                      Cli.id_P_0044 <= TipCli_Hst And _
                                                     (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                              Select New With {.cli_idc = Cli.cli_idc, _
                                               .digito = Cli.cli_dig_ito, _
                                               .cli_rso = If(Cli.id_P_0044 = 1, _
                                                             Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                             Cli.cli_rso), _
                                               .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                               .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next
                    End If
                    'fin

                Case 3
                    If todos = False Then


                        'Por tipo de cliente
                        Dim Clientes = (From Cli In Data.cli_cls _
                                          Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                                CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                     Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                     Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                     Cli.id_P_0044 >= TipCli_Dsd And _
                                                     Cli.id_P_0044 <= TipCli_Hst And _
                                                    (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) And _
                                                     Cli.id_P_008 = 1 Order By Cli.id_P_0044 _
                                                Select New With {.cli_idc = Cli.cli_idc, _
                                                .digito = Cli.cli_dig_ito, _
                                                .cli_rso = If(Cli.id_P_0044 = 1, _
                                                              Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                              Cli.cli_rso), _
                                                .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next

                        ' agregado por cristian arce salgado
                    Else
                        Dim Clientes = (From Cli In Data.cli_cls _
                                         Where CDbl(Cli.cli_idc) >= Rut_Dsd And _
                                               CDbl(Cli.cli_idc) <= Rut_Hst And _
                                                    Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                                    Cli.id_eje_cod_eje <= CodEje_Hst And _
                                                    Cli.id_P_0044 >= TipCli_Dsd And _
                                                    Cli.id_P_0044 <= TipCli_Hst And _
                                                   (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                                              Select New With {.cli_idc = Cli.cli_idc, _
                                               .digito = Cli.cli_dig_ito, _
                                               .cli_rso = If(Cli.id_P_0044 = 1, _
                                                             Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                             Cli.cli_rso), _
                                               .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                               .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Distinct.Skip(Sesion.NroPaginacionCli)

                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CDbl(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next

                    End If

            End Select


            GV.DataSource = Coll
            GV.DataBind()

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Sub ClientesActivosLikeDevuelveTodos(ByVal GV As GridView, ByVal Rut As String, ByVal Nombre As String, ByVal CodEje As Integer, _
                                                ByVal TipCli_Dsd As Integer, ByVal TipCli_Hst As Integer)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los clientes por rango en Rut y ejecutivos que se encuentren activos                  
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 16/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion   
        'Carce                      19/08/2011          se agrega campo estado
        'Gonzalo                    11/08/2016          Se Mejora Nombre, apellido p y apellido M para el buscador
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.eje_des_cra.Trim().ToUpper() = Usr.Trim().ToUpper() Select e).First
            '(Cli.cli_rso.Contains(Nombre) Or Cli.cli_ape_ptn.Contains(Nombre) Or Cli.cli_ape_mtn.Contains(Nombre))
            If Rut <> "0" And Nombre <> "" Then
                'Cli.id_P_008 = 1 And _
                Dim Clientes = (From Cli In Data.cli_cls _
                           Where Cli.cli_idc.Contains(Rut) And _
                                 Cli.id_P_0044 >= TipCli_Dsd And _
                                 Cli.id_P_0044 <= TipCli_Hst And _
                                 (Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn).Contains(Nombre) And _
                                 (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                                            Select New With {.cli_idc = Cli.cli_idc.Trim, _
                                              .cli_rso = If(Cli.id_P_0044 = 1, Cli.cli_rso.Trim.ToUpper & " " & _
                                                                               Cli.cli_ape_ptn.Trim.ToUpper & " " & _
                                                                               Cli.cli_ape_mtn.Trim.ToUpper, _
                                                                               Cli.cli_rso.Trim.ToUpper), _
                                              .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                               .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des, _
                                              .digito = Cli.cli_dig_ito}).Skip(Sesion.NroPaginacionCli)


                For Each Cli In Clientes.Take(8)
                    Cli.cli_idc = Format(CLng(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                    Coll.Add(Cli)
                Next

                GV.DataSource = Coll
                GV.DataBind()


            Else

                If Rut <> "0" Then
                    'Cli.id_P_008 = 1 And _
                    'Rut = Format(CLng(Rut), Var.FMT_RUT)
                    Dim Clientes = (From Cli In Data.cli_cls _
                               Where Cli.cli_idc.Contains(Rut) And _
                                     Cli.id_P_0044 >= TipCli_Dsd And _
                                     Cli.id_P_0044 <= TipCli_Hst And _
                                    (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                               Order By CLng(Cli.cli_idc) _
                                            Select New With {.cli_idc = Cli.cli_idc.Trim, _
                                                  .cli_rso = If(Cli.id_P_0044 = 1, Cli.cli_rso.Trim.ToUpper & " " & _
                                                                                   Cli.cli_ape_ptn.Trim.ToUpper & " " & _
                                                                                   Cli.cli_ape_mtn.Trim.ToUpper, _
                                                                                   Cli.cli_rso.Trim.ToUpper), _
                                                  .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                  .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des, _
                                                  .digito = Cli.cli_dig_ito}).Skip(Sesion.NroPaginacionCli)

                    For Each Cli In Clientes.Take(8)
                        Cli.cli_idc = Format(CLng(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                        Coll.Add(Cli)
                    Next

                    GV.DataSource = Coll
                    GV.DataBind()

                Else
                    '(Cli.cli_rso.Contains(Nombre) Or Cli.cli_ape_ptn.Contains(Nombre) Or Cli.cli_ape_mtn.Contains(Nombre)) And _
                    If Nombre <> "" Then
                        'Cli.id_P_008 = 1 And _
                        Dim Clientes = (From Cli In Data.cli_cls _
                                   Where
                                                 Cli.id_P_0044 >= TipCli_Dsd And _
                                                 Cli.id_P_0044 <= TipCli_Hst And _
                                                (Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn).Contains(Nombre) And _
                                                 (Cli.id_suc = ejecutivo.id_suc Or (From n In Data.nes_cls Where n.eje_cls.eje_des_cra = Usr And n.id_suc = Cli.id_suc).Count > 0) _
                                                 Order By Cli.cli_rso _
                                                 Select New With {.cli_idc = Cli.cli_idc.Trim, _
                                                      .cli_rso = If(Cli.id_P_0044 = 1, Cli.cli_rso.Trim.ToUpper & " " & _
                                                                               Cli.cli_ape_ptn.Trim.ToUpper & " " & _
                                                                               Cli.cli_ape_mtn.Trim.ToUpper, _
                                                                               Cli.cli_rso.Trim.ToUpper), _
                                                      .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                                      .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des, _
                                                      .digito = Cli.cli_dig_ito}).Skip(Sesion.NroPaginacionCli)


                        For Each Cli In Clientes.Take(8)
                            Cli.cli_idc = Format(CLng(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                            Coll.Add(Cli)
                        Next

                        GV.DataSource = Coll
                        GV.DataBind()

                    End If

                End If

            End If

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Function BancosDevuelvePorCliente(ByVal Rut As String, ByVal id_sbl As String) As nbc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Banco asociados a un clientes especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            'Dim Bancos = (From Nbc In Data.nbc_cls Where Nbc.cli_idc = Format(CLng(Rut), Var.FMT_RUT)).First

            Dim Bancos = (From Nbc In Data.nbc_cls Where Nbc.cli_idc = Format(CLng(Rut), Var.FMT_RUT) _
                      Select New With { _
                               .Codigo = Nbc.id_nbc, _
                               .Deposito = IIf(Nbc.nbc_dep = "N", "NO", "SI"), _
                               .Banco = Nbc.sbc_cls.bco_cls.bco_des.ToUpper.Trim() + "-" + Nbc.P_0312_cls.pnu_des.ToUpper.Trim() + "-" + Nbc.nbc_cct.Trim(), _
                               .Descripcion = Nbc.sbc_cls.bco_cls.bco_des.ToUpper.Trim() + "-" + Nbc.P_0312_cls.pnu_des.ToUpper.Trim(), _
                               .Sucursal = Nbc.sbc_cls.sbc_des.ToUpper.Trim(), _
                               .Cuenta_Corriente = Nbc.nbc_cct, _
                               .Tip_Cta = Nbc.id_P_0312, _
                               .Codigo_Sucursal = Nbc.sbc_cls.id_sbc, _
                               .Codigo_Banco = Nbc.sbc_cls.id_bco})

            Return Bancos



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function BancosDevuelvePorCliente(ByVal LlenaDrop As Boolean, ByVal Drop As DropDownList, _
                                             ByVal GV As GridView, ByVal Rut As String, _
                                             Optional ByVal Paginacion As Integer = 9999) As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Bancos asociados a un clientes especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'A. Saldivar                 13/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim Bancos = (From Nbc In Data.nbc_cls Where Nbc.cli_idc = Format(CLng(Rut), Var.FMT_RUT) _
                    Select New With { _
                             .Codigo = Nbc.id_nbc, _
                             .Deposito = IIf(Nbc.nbc_dep = "N", "NO", "SI"), _
                             .Banco = Nbc.sbc_cls.bco_cls.bco_des.ToUpper.Trim() + "-" + Nbc.P_0312_cls.pnu_des.ToUpper.Trim() + "-" + Nbc.nbc_cct.Trim(), _
                             .Descripcion = Nbc.sbc_cls.bco_cls.bco_des.ToUpper.Trim() + "-" + Nbc.P_0312_cls.pnu_des.ToUpper.Trim(), _
                             .Sucursal = Nbc.sbc_cls.sbc_des.ToUpper.Trim(), _
                             .Cuenta_Corriente = Nbc.nbc_cct, _
                             .Tip_Cta = Nbc.id_P_0312, _
                             .Codigo_Sucursal = Nbc.sbc_cls.id_sbc, _
                             .Codigo_Banco = Nbc.sbc_cls.id_bco})

            If Not LlenaDrop Then

                If Not IsNothing(GV) Then
                    GV.DataSource = Bancos
                    GV.DataBind()

                End If

                For Each B In Bancos
                    Coll.Add(B)
                Next

                Return Coll
            Else
                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Bancos, "Codigo", "Banco", Drop)
            End If

        Catch ex As Exception

        Finally

        End Try

    End Function

    Public Function BancosDevuelveTodos(ByVal Dp As DropDownList) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Bancos 
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
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

    Public Function BancosDevuelvePorClienteCtaCte(ByVal LLenaGrid As Boolean, ByVal Rut As String, ByVal CtaCte As String, Optional ByVal GV As GridView = Nothing) As Object

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Banco asociados a un clientes especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 28/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Coll As New Collection

            Dim Bancos = From Nbc In Data.nbc_cls Where Nbc.cli_idc = Format(CLng(Rut), Var.FMT_RUT) And Nbc.nbc_cct = CtaCte _
                                Select Codigo = Nbc.sbc_cls.bco_cls.id_bco, _
                                       Deposito = Nbc.nbc_dep, _
                                       Banco = Nbc.sbc_cls.bco_cls.bco_des, _
                                       Sucursal = Nbc.sbc_cls.sbc_des, _
                                       Cuenta_Corriente = Nbc.nbc_cct, _
                                       Codigo_Sucursal = Nbc.sbc_cls.id_sbc

            'Dim Bancos = From Nbc In Data.nbc_cls _
            '              Join Bco In Data.bco_cls On Nbc.bco_cod Equals Bco.bco_cod _
            '              Join Sbc In Data.sbc_cls On Nbc.sbc_cod Equals Sbc.sbc_cod And Nbc.bco_cod Equals Sbc.bco_cod _
            '             Where Nbc.cli_idc = Rut And Nbc.nbc_cct = CtaCte _
            '             Select Codigo = Nbc.bco_cod, _
            '                       Deposito = Nbc.nbc_dep, _
            '                       Banco = Bco.bco_des, _
            '                       Sucursal = Sbc.sbc_des, _
            '                       Cuenta_Corriente = Nbc.nbc_cct, _
            '                       sbc_cod = Sbc.sbc_cod
            If LLenaGrid Then
                GV.DataSource = Bancos
                GV.DataBind()
            Else
                Return Bancos
            End If




        Catch ex As Exception
        Finally

        End Try

    End Function

    Public Function BancosDevuelvePorClienteYBanco(ByVal Rut As String, ByVal id_bco As Integer) As nbc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Banco asociado a un cliente por rutcliente y banco,
        'Creado por= A. Saldivar                                                                                                                       
        'Fecha Creacion: 25/10/2010                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Bancos = (From nb In Data.nbc_cls Where nb.cli_idc = Format(CLng(Rut), Var.FMT_RUT) And nb.id_nbc = id_bco).First

            Return Bancos
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function SucursalDevuelvePorBanco(ByVal id_bco As Integer) As sbc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Banco asociado a un cliente por rutcliente y banco,
        'Creado por= A. Saldivar                                                                                                                       
        'Fecha Creacion: 25/10/2010                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim sucursal As sbc_cls = (From sb In Data.sbc_cls Where sb.id_bco = id_bco).First

            Return sucursal
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ContactosDevuelveTodos(ByVal TipoContacto As TipoDeContacto, ByVal RutCliDeu As Long, _
                                           Optional ByVal LlenaGrilla As Boolean = True, Optional ByVal GV As GridView = Nothing) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Contactos de un Cliente o Deudor especifico
        'Creado por: Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 02/06/2008                                                                                                                     
        'Quien Modifica: Yonathan Cabezas V.         Fecha: 03/02/2009         
        'Descripcion: Se modifica para llenar los datos de los contactos de un deudor desde verificación
        'A Saldivar         13/01/2011      Se agrega paginacion
        'Carce              05/09/2011      se agrega el campo Fax al select  de  cada case
        'Carce              13/09/2011      se agrega el campo cnc_car al select, para que no se produsca una exeption
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Select Case LlenaGrilla
                Case True
                    Select Case TipoContacto

                        Case Variables.TipoDeContacto.Cliente
                            Dim Contactos = (From C In Data.cnc_cls Where C.cli_idc = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "C" _
                                            Select id_cnc = C.id_cnc, cnc_nom = C.cnc_nom, _
                                            cnc_def = C.cnc_def, cnc_car = C.cnc_car, cnc_tel = C.cnc_tel, cnc_fax = C.cnc_fax, _
                                            cnc_ema = C.cnc_ema, C.cnc_rep_leg).Skip(sesion.NroPaginacion).Take(10)

                            GV.DataSource = Contactos
                            GV.DataBind()

                        Case Else
                            Dim Contactos = (From C In Data.cnc_cls Where C.deu_ide = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "D" _
                                            Select id_cnc = C.id_cnc, cnc_nom = C.cnc_nom, cnc_def = C.cnc_def, cnc_car = C.cnc_car, cnc_tel = C.cnc_tel, cnc_fax = C.cnc_fax, _
                                            cnc_ema = C.cnc_ema, C.cnc_rep_leg).Skip(sesion.NroPaginacion).Take(10)


                            GV.DataSource = Contactos
                            GV.DataBind()

                    End Select
                Case False
                    Select Case TipoContacto

                        Case Variables.TipoDeContacto.Cliente
                            Dim Contactos = From C In Data.cnc_cls Where C.cli_idc = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "C" _
                                            Select id_cnc = C.id_cnc, cnc_nom = C.cnc_nom & "  //  " & C.cnc_car, _
                                            cnc_def = C.cnc_def, cnc_tel = C.cnc_tel, _
                                            cnc_ema = C.cnc_ema, cnc_fax = C.cnc_fax, C.cnc_rep_leg

                            Return Contactos

                        Case Else
                            Dim Contactos = From C In Data.cnc_cls Where C.deu_ide = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "D" _
                                            Select id_cnc = C.id_cnc, cnc_nom = C.cnc_nom & "  //  " & C.cnc_car, cnc_tel = C.cnc_tel, _
                                            cnc_ema = C.cnc_ema, cnc_def = C.cnc_def, cnc_fax = C.cnc_fax, C.cnc_rep_leg

                            Return Contactos

                    End Select
            End Select

        Catch ex As Exception

        Finally

        End Try

    End Function

    Public Function ContactosPorDefectoDevuelve(ByVal TipoContacto As TipoDeContacto, ByVal RutCliDeu As Long) As String

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los Contactos de un Cliente o Deudor especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 02/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Select Case TipoContacto

                Case Variables.TipoDeContacto.Cliente
                    Dim Contactos As cnc_cls = (From C In Data.cnc_cls Where C.cli_idc = Format(CLng(RutCliDeu), Var.FMT_RUT) _
                                                                                   And C.cnc_cli_ddr = "C" _
                                                                                   And C.cnc_def = "S" _
                                                                                   Select C).First

                    Return Contactos.cnc_nom.Trim & " / " & Contactos.cnc_car.Trim

                Case Else
                    Dim Contactos As cnc_cls = (From C In Data.cnc_cls Where C.deu_ide = Format(CLng(RutCliDeu), Var.FMT_RUT) _
                                                                                   And C.cnc_cli_ddr = "D" _
                                                                                   And C.cnc_def = "S" _
                                                                                   Select C).First


                    Return Contactos.cnc_nom.Trim & " / " & Contactos.cnc_car.Trim

            End Select


        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function ContactosDevuelve(ByVal TipoContacto As TipoDeContacto, ByVal RutCliDeu As Long, ByVal NroContacto As Integer) As cnc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve un Contactos de un Cliente o Deudor especifico
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 06/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Select Case TipoContacto

                Case Variables.TipoDeContacto.Cliente
                    Dim Contactos As cnc_cls = (From C In Data.cnc_cls Where C.cli_idc = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "C" And C.id_cnc = NroContacto).First

                    Return Contactos

                Case Else

                    Dim Contactos As cnc_cls = (From C In Data.cnc_cls Where C.deu_ide = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "D" And C.id_cnc = NroContacto).First

                    Return Contactos

            End Select


        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function ContactosDevuelve(ByVal RutCliDeu As Long) As cnc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve un Contactos de un Cliente que sea representante legal
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 06/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext


            Dim Contactos As cnc_cls = (From C In Data.cnc_cls Where C.cli_idc = Format(CLng(RutCliDeu), Var.FMT_RUT) And C.cnc_cli_ddr = "C" And C.cnc_rep_leg = "S").First

            Return Contactos


        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Sub EmpresasDevuelveTodos(ByVal GV As GridView, ByVal RutCliente As Long)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las Empresas de un Cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'A. Saldivar                 13/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Empresas = (From C In Data.emp_cls Where C.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) _
                                    Select C).Skip(sesion.NroPaginacion).Take(10)

            GV.DataSource = Empresas
            GV.DataBind()

            Dim I As Integer = 0

            For Each E In Empresas
                GV.Rows(I).Cells(2).Text = Format(CLng(E.emp_rut), Fmt.FCMSD) & "-" & RG.Vrut(E.emp_rut)
                I += 1
            Next

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Function EmpresasDevuelvePorCliente(ByVal RutCliente As Long, ByVal RutEmpresa As Long) As emp_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve la Empresa asociada al  clientes por su Rut
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes


            Dim Empresa As emp_cls = (From E In Data.emp_cls Where E.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And E.emp_rut = Format(CLng(RutEmpresa), Var.FMT_RUT)).First


            Return Empresa

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Sub OrganigramaDevuelveTodos(ByVal GV As GridView, ByVal RutCliente As Long)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todas las Organigrama de un Cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'A. Saldivar                 13/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Organigrama = (From O In Data.org_cls Where O.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) _
                                          Select O).Skip(sesion.NroPaginacion).Take(10)


            GV.DataSource = Organigrama
            GV.DataBind()

            Dim I As Integer = 0

            For Each O In Organigrama
                GV.Rows(I).Cells(1).Text = Format(CLng(O.org_rut), Fmt.FCMSD) & "-" & RG.Vrut(CLng(O.org_rut))
                I += 1
            Next




        Catch ex As Exception

        Finally

        End Try

    End Sub

    Public Function OrganigramaDevuelvePorCliente(ByVal RutCliente As Long, ByVal RutOrganigrama As Long) As org_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve un Organigrama asociada al  clientes por su Rut
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes


            Dim Organigrama As org_cls = (From E In Data.org_cls Where E.cli_idc = Format(CLng(RutCliente), Var.FMT_RUT) And E.org_rut = Format(CLng(RutOrganigrama), Fmt.FCMSD)).First

            Return Organigrama

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function EjecutivoDevuelve(ByVal id_Eje As Integer) As eje_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un ejecutivo por su id
        'Creado por= A. Saldivar.                                                                                                                       
        'Fecha Creacion: 17/06/2010                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'Fabian Y. Vargas            11-07-2012         Se buscaba info de eje. con id_dsi se remplaza por par. id_Eje
        '*********************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            'Dim Ejecutivo As eje_cls = (From E In data.eje_cls Where E.id_eje = id_dsi).First
            Dim Ejecutivo As eje_cls = (From E In data.eje_cls Where E.id_eje = id_Eje).First

            Return Ejecutivo

        Catch ex As Exception
            Return Nothing

        End Try

    End Function

    Public Function VerificaCliente(ByVal cli As cli_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: devuelve el estado de un cliente                  
        'Creado por= Victor alvarez                                                                                                                      
        'Fecha Creacion: 17/10/2011                                                                                                                 

        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Clientes As cli_cls = (From C In Data.cli_cls Where C.cli_idc = Format(CLng(cli.cli_idc), Var.FMT_RUT) And C.id_P_008 = 6).First

            Return True

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function NroClienteDevuelve(ByVal nro As String) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Devuelve numero cliente por su Rut 
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 09/07/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim SQL As New FuncionesGenerales.SqlQuery
            Dim DT As DataSet

            DT = SQL.ExecuteDataSet("EXEC SP_MA_NCLIENTE_DATOS_UNIFICADOS '" & nro & "'")

            If DT.Tables(0).Rows.Count > 0 Then
                If DT.Tables(0).Rows().Item(0).ToString() = "1" Then
                    Return True
                Else
                    Return False
                End If
            End If

            'Dim Data As New CapaDatos.DataClsFactoringDataContext

            'Dim Cliente As cli_cls = (From Cli In Data.cli_cls _
            '                           Where Cli.cli_nro_cli = nro _
            '                           Select Cli).First

            'If Cliente.cli_nro_cli <> Nothing Or Cliente.cli_nro_cli <> "" Then
            '    Return True
            'Else
            '    Return False
            'End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function SucursalXBancoDevuelve(ByVal id_bco As Integer) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Banco asociado a un cliente por id banco,
        'Creado por= Sebastian Henriquez C.                                                                                                                       
        'Fecha Creacion: 10/07/2012                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext

            Dim Sucursal As Integer = (From sb In Data.sbc_cls Where sb.id_bco = id_bco Select sb.id_sbc).First

            Return Sucursal
        Catch ex As Exception
            Return 0
        End Try

    End Function

#End Region

#Region "INSERT, UPDATE AND DELETE"

    Public Function BancoClienteUpdate(ByVal Banco As nbc_cls, ByVal Ctc As String) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Actualiza un banco de  un cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 14/08/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        'ASaldivar                    05/05/2010         Se agrega Cuentacorriente antigua a la consulta para que coincida
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RW As New FuncionesGenerales.RutinasWeb

            'delete
            Dim EliminaBancoCliente As nbc_cls = (From c In Data.nbc_cls _
                Where c.cli_idc = Format(CLng(Banco.cli_idc), Var.FMT_RUT) And c.nbc_cct = Ctc).First
            Data.nbc_cls.DeleteOnSubmit(EliminaBancoCliente)
            Data.SubmitChanges()


            Data.nbc_cls.InsertOnSubmit(Banco)
            Data.SubmitChanges()

        Catch ex As Exception
            Return False
        Finally

        End Try

    End Function

    Public Function OrganigramaUpdate(ByVal Organigrama As org_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza un Organigrama
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Org As org_cls = (From E In Data.org_cls Where E.cli_idc = Organigrama.cli_idc And E.org_rut = Format(CLng(Organigrama.org_rut), Fmt.FCMSD)).First

            With Org
                .cli_idc = Organigrama.cli_idc
                .org_rut = Organigrama.org_rut
                .org_nom = Organigrama.org_nom
                .org_car = Organigrama.org_car
                .org_atb = Organigrama.org_atb
            End With

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function ClientesDeudoresInserta(ByVal DDR As ddr_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un cliente para deudor
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            'El rut no va con el mismo formato
            Data.ddr_cls.InsertOnSubmit(DDR)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ClienteInsertar(ByVal Cliente As cli_cls, ByVal BienesServicios As String, ByVal id_ciu As Integer, ByVal mail_oficina As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta un clinete
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Cliente.cli_tas_mor = Cliente.cli_tas_mor_aux

            Data.cli_cls.InsertOnSubmit(Cliente)

            Data.SubmitChanges()

            '----------------------------------------------------------------------------------------------------------------
            'Versión: 12122013.V1
            'JLagos 09-01-2013  unificacion de datos, creacion de contacto como representante.
            'Dim cnc As cnc_cls = (From C In Data.cnc_cls Where C.cli_idc = Cliente.cli_idc And C.cnc_cli_ddr = "C" And C.cnc_rep_leg = "S").First

            Dim cnc As cnc_cls = ClienteRepresentanteDevuelveUnificado(Cliente.cli_idc)

            If Not IsNothing(cnc) Then
                cnc.cnc_cli_ddr = "C"
                cnc.cli_idc = Cliente.cli_idc
                Data.cnc_cls.InsertOnSubmit(cnc)
                Data.SubmitChanges()
            End If

            'If mail_oficina <> "" Then
            '    Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.id_eje = Cliente.CLI_GEST_COD).First
            '    ejecutivo.eje_mail = mail_oficina
            '    Data.SubmitChanges()
            'End If

            Dim sql As New FuncionesGenerales.SqlQuery
            Dim strsql As String

            Try

                If IsNothing(cnc) Then

                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Cliente.cli_idc & "', '" & _
                                                                                       Cliente.cli_dig_ito & "', '" & _
                                                                                       Cliente.cli_rso & "', '" & _
                                                                                       Cliente.cli_ape_ptn & "', '" & _
                                                                                       Cliente.cli_ape_mtn & "',  " & _
                                                                                       Cliente.id_P_0313 & ", " & _
                                                                                       Cliente.id_P_0119 & ",  " & _
                                                                                       Cliente.id_P_0044 & ", '" & _
                                                                                       Cliente.cli_nro_cli & "', '" & _
                                                                                       Cliente.cli_dml & "', " & _
                                                                                       id_ciu & ", '', '', '' '" & _
                                                                                       Cliente.id_PL_000006 & "', " & _
                                                                                       Cliente.id_P_0064 & ", " & _
                                                                                       Cliente.cli_sex & ", '" & _
                                                                                       Cliente.id_suc & "', " & _
                                                                                       Cliente.cli_cod_ges & ", '" & _
                                                                                       Cliente.cli_eje_ofc & "', '" & _
                                                                                       Cliente.id_eje_cod_eje & "', '" & _
                                                                                       Cliente.cli_ema & "', '" & _
                                                                                       Nothing & "', '" & _
                                                                                       Nothing & "', " & Nothing


                Else

                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Cliente.cli_idc & "', '" & _
                                                                                       Cliente.cli_dig_ito & "', '" & _
                                                                                       Cliente.cli_rso & "', '" & _
                                                                                       Cliente.cli_ape_ptn & "', '" & _
                                                                                       Cliente.cli_ape_mtn & "',  " & _
                                                                                       Cliente.id_P_0313 & ", " & _
                                                                                       Cliente.id_P_0119 & ",  " & _
                                                                                       Cliente.id_P_0044 & ", '" & _
                                                                                       Cliente.cli_nro_cli & "', '" & _
                                                                                       Cliente.cli_dml & "', " & _
                                                                                       id_ciu & ", '" & _
                                                                                       cnc.cnc_nom & "', '" & _
                                                                                       cnc.cnc_rut & "', '" & _
                                                                                       FC.Vrut(cnc.cnc_rut) & "', '" & _
                                                                                        Cliente.id_PL_000006 & "', " & _
                                                                                       Cliente.id_P_0064 & ", " & _
                                                                                       Cliente.cli_sex & ", '" & _
                                                                                       Cliente.id_suc & "', " & _
                                                                                       Cliente.cli_cod_ges & ", '" & _
                                                                                       Cliente.cli_eje_ofc & "', '" & _
                                                                                       Cliente.id_eje_cod_eje & "', '" & _
                                                                                       Cliente.cli_ema & "', '" & _
                                                                                       Nothing & "', '" & _
                                                                                       Nothing & "', " & Nothing
                End If

                sql.ExecuteNonQuery(strsql)

            Catch ex As Exception
                Return False
            Finally

            End Try
            '----------------------------------------------------------------------------------------------------------------

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ClienteUpdate(ByVal Cliente As cli_cls, ByVal id_ciu As Integer, ByVal mail_oficina As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza un clinete
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        'JLAGOS                     09-12-2009         -SE AGREGA EL CALCULO DE LA TASA MORA
        'SHENRIQUEZ                 27-06-2012         -SE AGREGA  CORASU
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Cli As cli_cls = (From c In Data.cli_cls _
                            Where c.cli_idc = Cliente.cli_idc _
                            Select c).First()

            Cli.id_P_0044 = Cliente.id_P_0044
            Cli.id_P_0313 = Cliente.id_P_0313
            Cli.id_eje_cod_eje = Cliente.id_eje_cod_eje
            Cli.id_suc = Cliente.id_suc
            Cli.id_eje_cod_cob = Cliente.id_eje_cod_cob
            Cli.cli_bci_flg = Cliente.cli_bci_flg
            Cli.cli_rso = Cliente.cli_rso
            Cli.cli_ape_ptn = Cliente.cli_ape_ptn
            Cli.cli_ape_mtn = Cliente.cli_ape_mtn
            Cli.cli_fec_nac = Cliente.cli_fec_nac
            Cli.cli_sex = Cliente.cli_sex
            Cli.cli_dml = Cliente.cli_dml
            Cli.id_cmn = Cliente.id_cmn
            Cli.id_PL_000006 = Cliente.id_PL_000006
            Cli.cli_cod_pot = Cliente.cli_cod_pot
            Cli.cli_ema = Cliente.cli_ema
            Cli.cli_fec_ini_ope = Cliente.cli_fec_ini_ope
            Cli.id_P_007 = Cliente.id_P_007
            Cli.id_P_008 = Cliente.id_P_008
            Cli.cli_cbz = Cliente.cli_cbz
            Cli.id_crt = Cliente.id_crt
            Cli.cli_tas_mor = Cliente.cli_tas_mor_aux
            Cli.id_P_0058 = Cliente.id_P_0058
            Cli.cli_eje_bci = Cliente.cli_eje_bci
            Cli.cli_eje_anx = Cliente.cli_eje_anx
            Cli.cli_eje_ofc = Cliente.cli_eje_ofc
            Cli.cli_fec_act_eje = Cliente.cli_fec_act_eje
            Cli.cli_fec_cre = Cliente.cli_fec_cre
            Cli.id_P_0064 = Cliente.id_P_0064
            Cli.cli_snf = Cliente.cli_snf
            Cli.id_PL_000066 = Cliente.id_PL_000066
            Cli.id_P_0067 = Cliente.id_P_0067
            Cli.id_P_0068 = Cliente.id_P_0068
            Cli.cli_tas_mor_aux = Cliente.cli_tas_mor_aux
            Cli.id_P_0076 = Cliente.id_P_0076
            Cli.cli_cob_ant = Cliente.cli_cob_ant
            Cli.cli_spr_ead_col = Cliente.cli_spr_ead_col
            Cli.id_bco = Cliente.id_bco
            Cli.cli_bys_des = Cliente.cli_bys_des
            Cli.cli_ope_ptl = Cliente.cli_ope_ptl
            Cli.id_p_0118 = Cliente.id_p_0118
            Cli.cli_dia_bas = Cliente.cli_dia_bas
            Cli.CLI_TIP_TAS = Cliente.CLI_TIP_TAS
            Cli.cli_cod_ges = Cliente.cli_cod_ges
            Cli.id_P_0119 = Cliente.id_P_0119
            Cli.cli_dig_ito = Cliente.cli_dig_ito
            'Cli.CLI_CAN_AL = Cliente.CLI_CAN_AL
            'Cli.CLI_SUB_CAN_AL = Cliente.CLI_SUB_CAN_AL
            'Cli.CLI_GEST_COD = Cliente.CLI_GEST_COD

            Data.SubmitChanges()


            'If mail_oficina <> "" Then
            '    Dim ejecutivo As eje_cls = (From e In Data.eje_cls Where e.id_eje = Cliente.CLI_GEST_COD).First
            '    ejecutivo.eje_mail = mail_oficina
            '    Data.SubmitChanges()
            'End If
            '----------------------------------------------------------------------------------------------------------------
            'Versión: 12122013.V1
            Dim cnc As cnc_cls

            Try
                'Dim cnc As cnc_cls = ClienteRepresentanteDevuelveUnificado(Cliente.cli_idc)
                cnc = (From C In Data.cnc_cls Where C.cli_idc = Cliente.cli_idc And C.cnc_cli_ddr = "C" And C.cnc_rep_leg = "S").First

            Catch ex As Exception

            End Try

            Dim sql As New FuncionesGenerales.SqlQuery
            Dim strsql As String

            Try

                If IsNothing(cnc) Then

                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Cliente.cli_idc & "', '" & _
                                                                                       Cliente.cli_dig_ito & "', '" & _
                                                                                       Cliente.cli_rso & "', '" & _
                                                                                       Cliente.cli_ape_ptn & "', '" & _
                                                                                       Cliente.cli_ape_mtn & "',  " & _
                                                                                       Cliente.id_P_0313 & ", " & _
                                                                                       Cliente.id_P_0119 & ",  " & _
                                                                                       Cliente.id_P_0044 & ", '" & _
                                                                                       Cliente.cli_nro_cli & "', '" & _
                                                                                       Cliente.cli_dml & "', " & _
                                                                                       id_ciu & ", '', '', '', '" & _
                                                                                       Cliente.id_PL_000006 & "', " & _
                                                                                       Cliente.id_P_0064 & ", '" & _
                                                                                       Cliente.cli_sex & "', '" & _
                                                                                       Cliente.id_suc & "', '" & _
                                                                                       Cliente.cli_cod_ges & "', '" & _
                                                                                       Cliente.cli_eje_ofc & "', '" & _
                                                                                       Cliente.id_eje_cod_eje & "', '" & _
                                                                                       Cliente.cli_ema & "', '" & _
                                                                                      Nothing & "', '" & _
                                                                                       Nothing & "', " & Nothing



                Else

                    strsql = "Exec SP_MA_ACTUALIZA_DATOS_UNIFICADOS '" & Cliente.cli_idc & "', '" & _
                                                                                       Cliente.cli_dig_ito & "', '" & _
                                                                                       Cliente.cli_rso & "', '" & _
                                                                                       Cliente.cli_ape_ptn & "', '" & _
                                                                                       Cliente.cli_ape_mtn & "',  " & _
                                                                                       Cliente.id_P_0313 & ", " & _
                                                                                       Cliente.id_P_0119 & ",  " & _
                                                                                       Cliente.id_P_0044 & ", '" & _
                                                                                       Cliente.cli_nro_cli & "', '" & _
                                                                                       Cliente.cli_dml & "', " & _
                                                                                       id_ciu & ", '" & _
                                                                                       cnc.cnc_nom & "', '" & _
                                                                                       cnc.cnc_rut & "', '" & _
                                                                                       FC.Vrut(cnc.cnc_rut) & "', '" & _
                                                                                       Cliente.id_PL_000006 & "', " & _
                                                                                       Cliente.id_P_0064 & ", '" & _
                                                                                       Cliente.cli_sex & "', '" & _
                                                                                       Cliente.id_suc & "', '" & _
                                                                                       Cliente.cli_cod_ges & "', '" & _
                                                                                       Cliente.cli_eje_ofc & "', '" & _
                                                                                       Cliente.id_eje_cod_eje & "', '" & _
                                                                                       Cliente.cli_ema & "', '" & _
                                                                                       Nothing & "', '" & _
                                                                                       Nothing & "', " & Nothing
                End If

                sql.ExecuteNonQuery(strsql)

                Return True

            Catch ex As Exception
                Return False
            End Try

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ClienteDelete(ByVal Rut As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra un Cliente en particular
        'Creado por= Jorge Lagos C
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Dim eliminaCliente = (From c In Data.cli_cls _
                                  Where c.cli_idc = Rut)

            Data.ncr_cls.DeleteOnSubmit(eliminaCliente)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function BancoClienteInserta(ByVal Banco As nbc_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un banco por un cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 29/05/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion    
        'S Henriquez C.             10-09-2012         Si nueva cuenta es deposito, se modifican las cuentas existentes del cliente a N
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim RW As New FuncionesGenerales.RutinasWeb

            If Banco.nbc_dep = "S" Then
                Dim Bco_Cli = From B In Data.nbc_cls Where B.cli_idc = Banco.cli_idc Select B

                For Each BCli In Bco_Cli
                    BCli.nbc_dep = "N"
                Next

                Data.SubmitChanges()

            End If

            Data.nbc_cls.InsertOnSubmit(Banco)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function

    Public Function BancoClienteDelete(ByVal Rut As String, ByVal CodBco As Integer, ByVal NroCta As String) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra un Banco de un Cliente en particular y le asigna a otra cuenta del cliente del mismo banco como cta. cte. de deposito
        'Creado por= Jorge Lagos C
        'Fecha Creacion: 28/05/2008
        'Quien Modifica              Fecha              Descripcion
        'S Henriquez C.             10-09-2012         Se agrega nro cuenta como filtro
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            'Elimino el Banco asociado al Cliente

            Dim EliminaBancoCliente As nbc_cls = (From c In Data.nbc_cls _
                                                                        Where c.cli_idc = Format(CLng(Rut), Var.FMT_RUT) And _
                                                                              c.sbc_cls.id_sbc = CodBco And _
                                                                              c.nbc_cct = NroCta).First




            Data.nbc_cls.DeleteOnSubmit(EliminaBancoCliente)

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function ContactoInserta(ByVal Contacto As cnc_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un Nuevo contacto, este puede ser para Cliente o Deudor
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 02/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext


            Select Case Contacto.cnc_cli_ddr
                Case "C"

                    Dim Cnc_Cli = From C In Data.cnc_cls Where C.cli_idc = Format(CLng(Contacto.cli_idc), Var.FMT_RUT) And _
                                                                                      C.cnc_cli_ddr = "C" _
                                                                                       Select C
                    'Verifica si el contacto lo deja como contacto por defecto, y actualiza los demas contactos.
                    If Contacto.cnc_def = "S" Then
                        For Each C In Cnc_Cli
                            C.cnc_def = "N"
                        Next
                    End If

                    If Contacto.cnc_rep_leg = "S" Then
                        For Each C In Cnc_Cli
                            C.cnc_rep_leg = "N"
                        Next
                    End If

                Case "D"

                    Dim Cnc_Deu = From C In Data.cnc_cls Where C.deu_ide = Format(CLng(Contacto.deu_ide), Var.FMT_RUT) And _
                                                                                       C.cnc_cli_ddr = "D" _
                                                                                       Select C
                    'Verifica si el contacto lo deja como contacto por defecto, y actualiza los demas contactos.
                    If Contacto.cnc_def = "S" Then
                        For Each C In Cnc_Deu
                            C.cnc_def = "N"
                        Next
                    End If

                    If Contacto.cnc_rep_leg = "S" Then
                        For Each C In Cnc_Deu
                            C.cnc_rep_leg = "N"
                        Next
                    End If

            End Select

            Data.cnc_cls.InsertOnSubmit(Contacto)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ContactoUpdate(ByVal Contacto As cnc_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza un contacto
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Select Case Contacto.cnc_cli_ddr
                Case "C"
                    'Contacto para el Cliente
                    Dim Cnc_Cli = From C In Data.cnc_cls Where C.cli_idc = Format(CLng(Contacto.cli_idc), Var.FMT_RUT) And _
                                                                                      C.cnc_cli_ddr = "C" _
                                                                                      Select C
                    'Verifica si el contacto lo deja como contacto por defecto, y actualiza los demas contactos.
                    If Contacto.cnc_def = "S" Then
                        For Each C In Cnc_Cli
                            C.cnc_def = "N"
                        Next
                    End If

                    If Contacto.cnc_rep_leg = "S" Then
                        For Each C In Cnc_Cli
                            C.cnc_rep_leg = "N"
                        Next
                    End If
                Case "D"
                    'Contacto para el Deudor
                    Dim Cnc_Deu = From C In Data.cnc_cls Where C.deu_ide = Format(CLng(Contacto.deu_ide), Var.FMT_RUT) And _
                                                                                        C.cnc_cli_ddr = "D" _
                                                                                        Select C
                    'Verifica si el contacto lo deja como contacto por defecto, y actualiza los demas contactos.
                    If Contacto.cnc_def = "S" Then
                        For Each C In Cnc_Deu
                            C.cnc_def = "N"
                        Next
                    End If

                    If Contacto.cnc_rep_leg = "S" Then
                        For Each C In Cnc_Deu
                            C.cnc_rep_leg = "N"
                        Next
                    End If

            End Select

            Data.SubmitChanges()


            Select Case Contacto.cnc_cli_ddr
                Case "C"

                    'Contacto para el Cliente
                    Dim Cnc_Cli As cnc_cls = (From C In Data.cnc_cls Where C.cli_idc = Format(CLng(Contacto.cli_idc), Var.FMT_RUT) And _
                                                                                                       C.id_cnc = Contacto.id_cnc And _
                                                                                                       C.cnc_cli_ddr = "C").First
                    With Cnc_Cli
                        .cli_idc = Contacto.cli_idc
                        .deu_ide = Contacto.deu_ide
                        .id_cnc = Contacto.id_cnc
                        .cnc_cli_ddr = Contacto.cnc_cli_ddr
                        .cnc_nom = Contacto.cnc_nom
                        .cnc_rut = Contacto.cnc_rut
                        .cnc_dig = Contacto.cnc_dig
                        .cnc_car = Contacto.cnc_car
                        .cnc_dir = Contacto.cnc_dir
                        .cnc_obs = Contacto.cnc_obs
                        .cnc_def = Contacto.cnc_def
                        .cnc_rep_leg = Contacto.cnc_rep_leg
                        .cnc_tel = Contacto.cnc_tel
                        .cnc_fax = Contacto.cnc_fax
                        .cnc_ema = Contacto.cnc_ema
                        .cnc_not = Contacto.cnc_not
                        .cnc_tel2 = Contacto.cnc_tel2
                        .cnc_tel3 = Contacto.cnc_tel3
                    End With

                Case "D"

                    'Contacto para el Deudor
                    Dim Cnc_Deu As cnc_cls = (From C In Data.cnc_cls Where C.deu_ide = Format(CLng(Contacto.deu_ide), Var.FMT_RUT) And _
                                                                                                         C.id_cnc = Contacto.id_cnc And _
                                                                                                         C.cnc_cli_ddr = "D").First

                    With Cnc_Deu
                        .cli_idc = Contacto.cli_idc
                        .deu_ide = Contacto.deu_ide
                        .id_cnc = Contacto.id_cnc
                        .cnc_cli_ddr = Contacto.cnc_cli_ddr
                        .cnc_nom = Contacto.cnc_nom
                        .cnc_rut = Contacto.cnc_rut
                        .cnc_dig = Contacto.cnc_dig
                        .cnc_car = Contacto.cnc_car
                        .cnc_dir = Contacto.cnc_dir
                        .cnc_obs = Contacto.cnc_obs
                        .cnc_def = Contacto.cnc_def
                        .cnc_rep_leg = Contacto.cnc_rep_leg
                        .cnc_tel = Contacto.cnc_tel
                        .cnc_fax = Contacto.cnc_fax
                        .cnc_ema = Contacto.cnc_ema
                        .cnc_not = Contacto.cnc_not
                        .cnc_tel2 = Contacto.cnc_tel2
                        .cnc_tel3 = Contacto.cnc_tel3
                    End With

            End Select

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function ContactoDelete(ByVal TipoContacto As TipoDeContacto, ByVal RutCliDeu As Long, ByVal NroContacto As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra un contacto del Cliente o Deudor en particular
        'Creado por= Jorge Lagos C
        'Fecha Creacion: 06/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Select Case TipoContacto

                Case Variables.TipoDeContacto.Cliente
                    Dim Cnc_Cli = From C In Data.cnc_cls Where C.cli_idc = Format(RutCliDeu, Var.FMT_RUT) And _
                                                               C.id_cnc = NroContacto And _
                                                               C.cnc_cli_ddr = "C"
                    Data.cnc_cls.DeleteAllOnSubmit(Cnc_Cli)

                Case Variables.TipoDeContacto.Deudor
                    Dim Cnc_Deu = From C In Data.cnc_cls Where C.deu_ide = Format(RutCliDeu, Var.FMT_RUT) And _
                                                               C.id_cnc = NroContacto And _
                                                               C.cnc_cli_ddr = "D"
                    Data.cnc_cls.DeleteAllOnSubmit(Cnc_Deu)

            End Select

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False

        Finally

        End Try

    End Function

    Public Function EmpresaInserta(ByVal Empresa As emp_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva empresa para Cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Data.emp_cls.InsertOnSubmit(Empresa)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function EmpresaUpdate(ByVal Empresa As emp_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una Empresa
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Emp As emp_cls = (From E In Data.emp_cls Where E.cli_idc = Empresa.cli_idc And E.emp_rut = Format(CLng(Empresa.emp_rut), Var.FMT_RUT)).First

            With Emp
                .cli_idc = Empresa.cli_idc
                .emp_rut = Empresa.emp_rut
                .emp_des = Empresa.emp_des
            End With

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function EmpresaDelete(ByVal Empresa As emp_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza una Empresa
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Emp As emp_cls = (From E In Data.emp_cls Where E.cli_idc = Format(CLng(Empresa.cli_idc), Var.FMT_RUT) And E.emp_rut = Format(CLng(Empresa.emp_rut), Var.FMT_RUT)).First


            Data.emp_cls.DeleteOnSubmit(Emp)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function OrganigramaInserta(ByVal Organigrama As org_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta una nueva organigrama para Cliente
        'Creado por= Jorge Lagos C.                                                                                                                       
        'Fecha Creacion: 10/06/2008                                                                                                                     
        'Quien Modifica              Fecha              Descripcion                                                                                   
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Data.org_cls.InsertOnSubmit(Organigrama)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function OrganigramaDelete(ByVal RutOrg As Long, ByVal RutCliente As Long) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Borra Organigrma
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 10/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Emp As org_cls = (From E In Data.org_cls Where E.cli_idc = Format(RutCliente, Var.FMT_RUT) And E.org_rut = Format(CLng(RutOrg), Fmt.FCMSD)).First

            Data.org_cls.DeleteOnSubmit(Emp)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function ClienteRepresentanteDevuelveUnificado(ByVal nit As String) As cnc_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos unificados por nit para proveedor y pagadores (revisa en todos los sistemas)
        'Creado por=     Jorge Lagos
        'Fecha Creacion: 19/12/2013
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Dim sql As New FuncionesGenerales.SqlQuery
        Dim data As DataSet
        Dim CG As New ConsultasGenerales


        Try

            Dim strsql As String = "Exec SP_MA_DEVUELVE_DATOS_UNIFICADOS_PAG_PVR_II '" & nit & "'"
            Dim cnc As New cnc_cls

            data = sql.ExecuteDataSet(strsql)

            If data.Tables(0).Rows.Count > 0 Then

                If data.Tables(0).Rows(0).Item("CLI_REP_LEG").ToString() <> "" Then
                    cnc.cnc_rep_leg = "S"
                    cnc.cnc_nom = data.Tables(0).Rows(0).Item("CLI_REP_LEG").ToString()
                End If

                If data.Tables(0).Rows(0).Item("NIT_REP_LEG").ToString() <> "" Then
                    cnc.cnc_rut = data.Tables(0).Rows(0).Item("NIT_REP_LEG").ToString()
                End If
                '"DV_DIG"
                If data.Tables(0).Rows(0).Item("DV_DIG").ToString() <> "" Then
                    cnc.cnc_dig = data.Tables(0).Rows(0).Item("DV_DIG").ToString()
                End If

                Return cnc

            End If

        Catch ex As Exception
            Return Nothing
        Finally
            data.Dispose()
            data.Clear()
        End Try

    End Function

    Public Function ClienteDevuelveUnificado(ByVal nit As String) As cli_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos unificados por nit para proveedor y pagadores (revisa en todos los sistemas)
        'Creado por=     Jorge Lagos
        'Fecha Creacion: 19/12/2013
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Dim sql As New FuncionesGenerales.SqlQuery
        Dim data As DataSet
        Dim CG As New ConsultasGenerales

        'Try

        '    Dim strsql As String = "Exec SP_MA_DEVUELVE_DATOS_UNIFICADOS_PAG_PVR '" & nit & "'"
        '    Dim cli As New cli_cls

        '    data = sql.ExecuteDataSet(strsql)

        '    If data.Tables(0).Rows.Count > 0 Then

        '        cli.cli_idc = data.Tables(0).Rows(0).Item("NIT").ToString()
        '        cli.cli_dig_ito = data.Tables(0).Rows(0).Item("DIGITO").ToString()
        '        cli.cli_rso = data.Tables(0).Rows(0).Item("NOMBRE").ToString()
        '        cli.cli_ape_ptn = data.Tables(0).Rows(0).Item("APEPAT").ToString()
        '        cli.cli_ape_mtn = data.Tables(0).Rows(0).Item("APEMAT").ToString()
        '        cli.id_P_0313 = data.Tables(0).Rows(0).Item("COD_CORASU").ToString()
        '        cli.id_P_0119 = data.Tables(0).Rows(0).Item("COD_TIP_IDE").ToString()
        '        cli.id_P_0044 = data.Tables(0).Rows(0).Item("COD_TIP_CLI").ToString()
        '        cli.cli_nro_cli = data.Tables(0).Rows(0).Item("NUM_CLI").ToString()

        '        If data.Tables(0).Rows(0).Item("Dom_cli").ToString() <> "" Then
        '            cli.cli_dml = data.Tables(0).Rows(0).Item("Dom_cli").ToString()
        '        End If

        '        cli.id_suc = data.Tables(0).Rows(0).Item("CLI_COD_SUC").ToString()
        '        cli.id_PL_000006 = data.Tables(0).Rows(0).Item("CLI_GIR_GPG").ToString()
        '        cli.id_P_0064 = data.Tables(0).Rows(0).Item("CLI_SEC_ECO").ToString()
        '        cli.id_eje_cod_eje = data.Tables(0).Rows(0).Item("CLI_COD_EJE").ToString()
        '        cli.cli_eje_ofc = data.Tables(0).Rows(0).Item("CLI_EJE_OFI").ToString()
        '        cli.cli_cod_ges = data.Tables(0).Rows(0).Item("CLI_COD_GES").ToString()
        '        cli.cli_sex = data.Tables(0).Rows(0).Item("CLI_SEX_GPG").ToString()
        '        cli.cli_ema = data.Tables(0).Rows(0).Item("CLI_COR_ELE").ToString()

        '        Return cli

        '    End If

        'Catch ex As Exception
        '    Return Nothing
        'Finally
        '    data.Dispose()
        '    data.Clear()
        'End Try

    End Function

    Public Function ClienteCiudadDevuelveUnificado(ByVal nit As String) As ciu_cls

        '*********************************************************************************************************************************
        'Descripcion: Devuelve los datos unificados por nit para proveedor y pagadores (revisa en todos los sistemas)
        'Creado por=     Jorge Lagos
        'Fecha Creacion: 19/12/2013
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Dim sql As New FuncionesGenerales.SqlQuery
        Dim data As DataSet
        Dim CG As New ConsultasGenerales


        'Try

        '    Dim strsql As String = "Exec SP_MA_DEVUELVE_DATOS_UNIFICADOS_PAG_PVR '" & nit & "'"

        '    data = sql.ExecuteDataSet(strsql)

        '    If data.Tables(0).Rows.Count > 0 Then

        '        If data.Tables(0).Rows(0).Item("CLI_CIU").ToString() <> "0" Then
        '            Return CG.CiudadDevuelve(CInt(data.Tables(0).Rows(0).Item("CLI_CIU").ToString()))
        '        Else
        '            Return Nothing
        '        End If

        '    End If

        'Catch ex As Exception
        '    Return Nothing
        'Finally
        '    data.Dispose()
        '    data.Clear()
        'End Try

    End Function

#End Region

End Class
