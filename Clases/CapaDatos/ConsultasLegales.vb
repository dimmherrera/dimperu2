Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports CapaDatos
Public Class ConsultasLegales

#Region "Aval"
    Dim Var As New FuncionesGenerales.Variables

    Public Function ComunaDevuelvePorIdCiudad(ByVal Codigo_Ciudad As Integer, Optional ByVal Llenadrop As Boolean = True, _
                                            Optional ByVal DP As DropDownList = Nothing)
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Comunas por id Ciudad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 19/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim Comuna = From C In Data.cmn_cls Where C.id_ciu = Codigo_Ciudad

            If Llenadrop Then
                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Comuna, "id_cmn", "cmn_des", DP, 0, "Seleccionar")
                'Else
                '    Return Comuna
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function AvalDevueve(ByVal RutDsd As String, ByVal RutHst As String, _
                               ByVal SucDesde As Integer, ByVal SucHasta As Integer, _
                               ByVal EjeDesde As Integer, ByVal EjeHasta As Integer, _
                               ByVal AvlDesde As Integer, ByVal AvlHasta As Integer, ByVal Rut_AvlDsd As Integer, _
                               ByVal Rut_AvlHst As Long) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Aval po Amplio Rango
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 23/06/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/01/2011         Se agrega paginacion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim var As New FuncionesGenerales.Variables
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim aval = (From a In data.avl_cls Where a.cli_idc >= Format(CLng(RutDsd), var.FMT_RUT) And a.cli_idc <= Format(CLng(RutHst), var.FMT_RUT) And _
                       (a.cli_cls.id_suc >= SucDesde And a.cli_cls.id_suc <= SucHasta) And _
                       (a.cli_cls.id_eje_cod_eje >= EjeDesde And a.cli_cls.id_eje_cod_eje <= EjeHasta) And _
                       (a.id_p_0026 >= AvlDesde And a.id_p_0026 <= AvlHasta) And _
                       (a.avl_ida >= Format(CLng(Rut_AvlDsd), var.FMT_RUT) And a.avl_ida <= Format(CLng(Rut_AvlHst), var.FMT_RUT)) _
                       Select Rut_Cli = a.cli_idc, _
                              Dig_Cli = a.cli_cls.cli_dig_ito, _
                              Cliente = If(a.cli_cls.id_P_0044 = 1, _
                                           a.cli_cls.cli_rso.Trim & " " & a.cli_cls.cli_ape_ptn.Trim & " " & a.cli_cls.cli_ape_mtn.Trim, _
                                           a.cli_cls.cli_rso.Trim), _
                              Rut_Aval = a.avl_ida, _
                              Dig_Aval = a.avl_dig_ito, _
                                Nombre_Aval = a.avl_nom, _
                                Domicilio_Particular = a.avl_dml, _
                              Domicilio_Comercial = a.avl_dml_com, _
                              Fecha_est = a.avl_fec_est_sit, _
                              Fecha_Ju = a.avl_fec_jun_acc, _
                              a.avl_id_cmn, _
                              Comuna_Particualar = a.cmn_cls.cmn_des, _
                              Ciu_Particular = a.cmn_cls.ciu_cls.ciu_des, _
                              a.avl_id_cmn_com, _
                              Comuna_Comercial = a.cmn_cls.cmn_des, _
                              Ciu_Comercial = a.cmn_cls.ciu_cls.ciu_des, _
                              a.cli_cls.id_suc, _
                              Sucursal = a.cli_cls.suc_cls.suc_nom, _
                              a.cli_cls.id_eje_cod_eje, _
                              Ejecutivo = (From e In data.eje_cls Where e.id_eje = a.cli_cls.id_eje_cod_eje Select e.eje_nom).First, _
                              Monto = a.avl_mto, _
                              Notaria = a.avl_not, _
                              Plazo = a.avl_plz, _
                              Patrimonio = a.avl_ptm, _
                              a.id_avl, _
                                a.id_p_0025, _
                                Reg_Matrimonial = a.P_0025_cls.pnu_des, _
                                a.id_p_0026, _
                                Tipo_Aval = a.P_0026_cls.pnu_des, _
                                a.id_p_0027, _
                                Est_Aval = a.P_0027_cls.pnu_des).Skip(sesion.NroPaginacion).Take(12)

            For Each a In aval
                coll.Add(a)
            Next

            If coll.Count > 0 Then
                Return coll
            Else
                Return coll
            End If
            'Ejecutivo = a.cli_cls.eje_cls.eje_nom, _
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AvlDevuelveObjeto(ByVal Codigo_Avales As Integer) As avl_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Aval id Aval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 24/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim avl As avl_cls = (From a In data.avl_cls Where a.id_avl = Codigo_Avales).First
            Return avl
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ComunaDevuelveTodas(Optional ByVal Llenadrop As Boolean = True, _
                                           Optional ByVal DP As DropDownList = Nothing)
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Comunas por id Ciudad
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 19/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim Comuna = From C In Data.cmn_cls

            If Llenadrop Then
                Dim RG As New FuncionesGenerales.RutinasWeb
                RG.Llenar_Drop(Comuna, "id_cmn", "cmn_des", DP, 0, "Seleccionar")
                'Else
                '    Return Comuna
            End If

        Catch ex As Exception
            Return False
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
            Dim data As New DataClsFactoringDataContext
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
            Dim data As New DataClsFactoringDataContext
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

#Region "Cobertura"
    Public Function PagareDevuelveObjeto(ByVal Numero_Pgr As Integer) As Object
        Try
            Dim data As New DataClsFactoringDataContext
            Dim pgrdevuelve As pgr_cls = (From p In data.pgr_cls Where p.id_pgr = Numero_Pgr).First
            If Not IsNothing(pgrdevuelve) Then
                Return pgrdevuelve
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ListaCobertura(ByVal Rutdsd As String, ByVal Ruthst As String, _
                                ByVal Sucdsd As Integer, ByVal Suchst As Integer, _
                                ByVal Fecha As String, ByVal eje As Integer) As Collection
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Lista Cobertura por Amplio Rango
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 23/06/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                 27/08/2010          Se agrega rango amplio de ejecutivo
        'A Saldivar                 13/01/2011          Se agrega paginacion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim var As New FuncionesGenerales.Variables
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim LisCobertura = (From pgr In data.pgr_cls Where (pgr.cli_idc >= Format(CLng(Rutdsd), var.FMT_RUT) And _
                                                               pgr.cli_idc <= Format(CLng(Ruthst), var.FMT_RUT)) And _
                                                               (pgr.cli_cls.id_suc >= Sucdsd And pgr.cli_cls.id_suc <= Suchst) And _
                                                               pgr.id_P_0022 = 1 And _
                                                               pgr.pgr_fec_ing <= Fecha _
                                                               Group By pgr.cli_idc, pgr.id_P_0023, pgr.pgr_mdt, pgr.cli_cls.cli_rso, _
                                                               pgr.cli_cls.cli_dig_ito Into suma = Sum(pgr.pgr_mto) _
                                                       Select New With {cli_idc, .Monto = suma, id_P_0023, .Mto_ocu = 0.0, pgr_mdt, _
                                                                        cli_rso, cli_dig_ito}).Skip(sesion.NroPaginacion).Take(14)




            For Each p In LisCobertura

                Dim Factor As Double

                Try
                    Factor = (From pr In data.par_cls Where pr.id_P_0023 = p.id_P_0023 And pr.par_fec = Fecha Select pr.par_val).First
                Catch ex As Exception
                    Factor = 1
                End Try
                p.Monto = p.Monto * Factor
                Dim RUT As String
                RUT = p.cli_idc
                Dim ocp As Double = 0

                Try

                    ocp = (From doc In data.doc_cls Where doc.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = RUT _
                                                    And (doc.dsi_cls.id_P_0011 = 1 Or _
                                                         doc.dsi_cls.id_P_0011 = 2 Or _
                                                         doc.dsi_cls.id_P_0011 = 4 Or _
                                                         doc.dsi_cls.id_P_0011 = 9 Or _
                                                         doc.dsi_cls.id_P_0011 = 11 Or _
                                                         doc.dsi_cls.id_P_0011 = 12) _
                                                        And doc.doc_sdo_cli > 0 _
                                               Select doc.doc_sdo_cli).Sum


                    'For Each x In op

                    '    ocp = ocp + x.Value


                    'Next

                Catch ex As Exception

                End Try

                p.Mto_ocu = ocp * Factor
                coll.Add(p)

            Next


            If coll.Count > 0 Then
                Return coll
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Pagare"

    Public Function PagareDevuelve(ByVal rutdsd As String, ByVal ruthst As String, ByVal FechaVigDsd As String, _
                                   ByVal FechaVigHSt As String, ByVal FechaProDsd As String, _
                                   ByVal FechaProHst As String, ByVal TipoPgrDsd As Integer, _
                                   ByVal TipoPgrHst As Integer, ByVal MtoDsd As Double, ByVal MtoHst As Double, _
                                   ByVal MandatoDesde As String, ByVal MandatoHasta As String, _
                                   ByVal EjeDsd As Integer, ByVal EjeHst As Integer, ByVal id_dsd As Integer, _
                                   ByVal id_hst As Integer, ByVal SucDsd As Integer, _
                                   ByVal SucHst As Integer, ByVal DoctoDsd As Integer, ByVal DoctoHst As Integer, _
                                   ByVal EstClidsd As Integer, ByVal EstClihst As Integer, ByVal CantPag As Integer) As Collection
        '*********************************************************************************************************************************
        'Estaba la paginacion en 13
        'Descripcion: Devuelve Pagare por rango amplio
        'Creado por= A. Saldivar
        'Fecha Creacion: 04/06/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 13/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim pagare = (From p In data.pgr_cls Where (p.cli_idc >= Format(CLng(rutdsd), Var.FMT_RUT) And _
                         p.cli_idc <= Format(CLng(ruthst), Var.FMT_RUT)) And _
                         (p.pgr_fev >= FechaVigDsd And p.pgr_fev <= FechaVigHSt) And _
                         (p.pgr_fec_prt >= FechaProDsd And p.pgr_fec_prt <= FechaProHst) And _
                         (p.id_P_0021 >= TipoPgrDsd And p.id_P_0021 <= TipoPgrHst) And _
                         (CDbl(p.pgr_mto) >= CDbl(MtoDsd) And CDbl(p.pgr_mto) <= CDbl(MtoHst)) And _
                         (p.pgr_mdt >= MandatoDesde And p.pgr_mdt <= MandatoHasta) And _
                         (p.cli_cls.id_eje_cod_eje >= EjeDsd And p.cli_cls.id_eje_cod_eje <= EjeHst) And _
                         (p.id_pgr >= id_dsd And p.id_pgr <= id_hst) And _
                         (p.cli_cls.id_suc >= SucDsd And p.cli_cls.id_suc <= SucHst) And _
                         (p.pgr_num >= DoctoDsd And p.pgr_num <= DoctoHst) And _
                         (p.cli_cls.id_P_008 >= EstClidsd And p.cli_cls.id_P_008 <= EstClihst) _
                           Select p.id_pgr, _
                                Rut_Cliente = p.cli_idc, _
                                Razon_Social = If(p.cli_cls.id_P_0044 = 1, _
                                                  p.cli_cls.cli_rso.Trim & " " & p.cli_cls.cli_ape_ptn.Trim & " " & p.cli_cls.cli_ape_mtn.Trim, _
                                                  p.cli_cls.cli_rso.Trim), _
                                p.cli_cls.cli_dig_ito, _
                                NumeroDocto = p.pgr_num, _
                                p.id_P_0021, _
                                Tipo_Pagare = p.P_0021_cls.pnu_des, _
                                Mandato = p.pgr_mdt, _
                                Fecha_Sucripcion = p.pgr_fec_ing, _
                                FechaVecto = p.pgr_fev, _
                                Monto = p.pgr_mto, _
                                Moneda = p.P_0023_cls.pnu_des, _
                                p.id_P_0023, _
                                Fecha_Protesto = p.pgr_fec_prt, _
                                Antecedentes = p.pgr_anc, _
                                Estado = p.P_0022_cls.pnu_des, _
                                p.id_P_0022, _
                                Impuesto_Pagare = p.pgr_imp, _
                                CXC_Cob_Imp = p.pgr_tim, _
                                Motivo_Protesto = p.P_0061_cls.pnu_des, _
                                p.id_P_0061, _
                                Deuda_Total = 0, _
                                p.id_cxc).Skip(sesion.NroPaginacion).Take(CantPag)


            For Each p In pagare
                coll.Add(p)
            Next

            If coll.Count > 0 Then
                Return coll
            Else
                Return coll
            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    
    Public Function PagareDevuelveTipoPagare(ByVal Suc_Dsd As Integer, ByVal Suc_Hst As Integer, ByVal EstCli_Dsd As Integer, _
                               ByVal EstCli_Hst As Integer, ByVal TipoPgr_Dsd As Integer, ByVal TipoPgr_Hst As Integer, _
                               ByVal F_Dsd As String, ByVal F_Hst As String) As Collection
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Pagare por rango amplio
        'Creado por= A. Saldivar
        'Fecha Creacion: 06/08/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                 27/08/2010          Se modifica para que no repita los registros

        '*********************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim coll As New Collection
            Dim sesion As New ClsSession.ClsSession

            Dim pagare = (From p In data.pgr_cls Group Join v In data.par_cls On p.id_P_0023 Equals v.id_P_0023 _
                         Into valor = Sum(v.par_val), children = Group _
                         Where (p.cli_idc = p.cli_cls.cli_idc) And _
                         p.id_P_0022 = 1 And _
                        (p.pgr_fev >= F_Dsd And p.pgr_fev <= F_Hst) And _
                        (p.id_P_0021 >= TipoPgr_Dsd And p.id_P_0021 <= TipoPgr_Hst) And _
                        (p.cli_cls.id_suc >= Suc_Dsd And p.cli_cls.id_suc <= Suc_Hst) _
                          Select New With {.Rut_Cliente = p.cli_idc, _
                                          p.cli_idc, _
                                          p.cli_cls.cli_dig_ito, _
                                          .Razon_Social = If(p.cli_cls.id_P_0044 = 1, _
                                                             p.cli_cls.cli_rso.Trim & " " & p.cli_cls.cli_ape_ptn.Trim & " " & p.cli_cls.cli_ape_mtn.Trim, _
                                                             p.cli_cls.cli_rso.Trim), _
                                          .FechaVecto = p.pgr_fev, _
                                          .NumeroDocto = p.pgr_num, _
                                          p.id_P_0023, _
                                          .Moneda = p.P_0023_cls.pnu_des, _
                                          .Mandato = p.P_0021_cls.pnu_des, _
                                          p.id_P_0021, _
                                          .Tipo_Pagare = p.P_0021_cls.pnu_des, _
                                          valor, _
                                          p.pgr_mto, _
                                          .Monto = 0, _
                                          .Deuda_Total = 0}).Skip(sesion.NroPaginacion).Take(12)



            For Each p In Pagare
                Dim Factor As Double = 0
                Dim Deu As Double = 0
                If p.id_P_0023 = 1 Then
                    Factor = 1
                Else
                    Factor = p.valor
                End If
                p.Monto = Factor * p.pgr_mto

                If (From r In data.rsc_cls Where p.cli_idc = r.cli_idc).Count > 0 Then
                    'Deu =
                    p.Deuda_Total = (From r In data.rsc_cls Where p.cli_idc = r.cli_idc Select r.rsc_mto_ocu).First
                Else
                    p.Deuda_Total = Deu
                End If
                '(select rsc_mto_ocu from rsc r where r.cli_idc = p.cli_idc) as 'DEUDA TOTAL',

                coll.Add(p)
            Next
            If coll.Count > 0 Then
                Return coll
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function TasaImpuestoDevuelveObjeto() As Object
        Try
            Dim data As New DataClsFactoringDataContext
            Dim TasaDevuelve As tim_cls = (From t In data.tim_cls Where t.tim_est = "A").First

            Return TasaDevuelve

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function PagareDevuelveCliente(ByVal id As Integer) As String
        '*********************************************************************************************************************************
        'Descripcion: Devuelve id cliente asociado a pagare 
        'Creado por= Sebastian Henriquez C.
        'Fecha Creacion: 14/07/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New CapaDatos.DataClsFactoringDataContext

            Dim cli As String = (From p In data.pgr_cls Where p.id_pgr = id Select p.cli_idc).First

            Return (CInt(cli)).ToString()

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function ValidaMonto(ByVal mto As Double) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la tasa maxima convencional
        'Creado por A Saldivar.
        'Fecha Creacion: 22/06/2010
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim cls = From c In data.cfc_cls

        Catch ex As Exception

        End Try
    End Function

#End Region

End Class
