Imports Microsoft.VisualBasic
Imports System.Data.Linq
Imports System.Data.Linq.SqlClient.SqlMethods
Imports System.Web.UI.WebControls
Imports ClsSession.SesionOperaciones
Imports ClsSession.ClsSession
Imports System.Transactions
Imports CapaDatos

Public Class ClaseCobranza

    Dim Var As New FuncionesGenerales.Variables
    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim RW As New FuncionesGenerales.RutinasWeb

    Private _estadoconsulta As Integer
    Public Property estadoconsulta() As Integer
        Get
            Return _estadoconsulta
        End Get
        Set(ByVal value As Integer)
            _estadoconsulta = value
        End Set
    End Property

    Private _descripcionconsulta As String
    Public Property descripcionconsulta() As String
        Get
            Return _descripcionconsulta
        End Get
        Set(ByVal value As String)
            _descripcionconsulta = value
        End Set
    End Property

#Region "Consultas"

#Region "COBRANZAS - GESTION"
    Public Function Retorna_url_img(ByVal id As Integer) As dsi_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve ruta donde se encuentra guardada la imagen
        'Creado por Cristian Arce
        'Fecha Creacion: 13/10/2011
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim url_img As dsi_cls = (From d In Data.doc_cls Where d.id_doc = id _
            Select d.dsi_cls).First()
            Return url_img

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Retorna_url_img_ope(ByVal id As Integer) As dsi_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve ruta donde se encuentra guardada la imagen
        'Creado por Cristian Arce
        'Fecha Creacion: 13/10/2011
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim url_img As dsi_cls = (From d In Data.dsi_cls Where d.id_dsi = id _
            Select d).First()
            Return url_img

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CobranzaDevuelve() As Collection
        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los notarios de una sucursal
        'Creado por=Pablo Gatica S.
        'Fecha Creacion: 18/06/2008
        'Quien Modifica              Fecha              Descripcion

        '*********************************************************************************************************************************
        Dim i As Integer
        Dim col As New Collection
        Try


            Dim Data As New DataClsFactoringDataContext

            Dim Cobranza = From N In Data.cco_cls Select N

            For Each p In Cobranza
                i = i + 1
                col.Add(p)

            Next

            Return col

        Catch ex As Exception

        End Try

    End Function

    Public Function BuscaCodigoCobranza(ByVal cod_cob As String) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Verifica si un codigo de cobranza esta asociado a un dsiumento
        'Creado por=Pablo Gatica S.
        'Fecha Creacion: 18/06/2008
        'Quien Modifica              Fecha              Descripcion

        '*********************************************************************************************************************************

        Dim col As New Collection
        Try


            Dim Data As New DataClsFactoringDataContext

            Dim Cobranza = From N In Data.doc_cls Select N Where N.id_cco = cod_cob

            If Cobranza.Count > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception

        End Try

    End Function


    Public Sub EjecutivosRetornaReemplazos(ByVal DP As DropDownList, ByVal CodigoCobrador As Int16)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve cobradores telefonicos de Reemplazo por su Sucursal para llenar un dropdownlist
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim RG As New FuncionesGenerales.RutinasWeb

            'Join eje In Data.eje_cls On eje.eje_cod Equals R.eje_cod_cob_rpz _

            Dim Reemplazo = From R In Data.ncr_cls _
                            Where R.id_eje = CodigoCobrador _
                            Select Descripcion = R.eje_cls.eje_nom, R.id_eje, Codigo = R.id_eje_rpz


            RG.Llenar_Drop(Reemplazo, "Codigo", "Descripcion", DP)

        Catch ex As Exception

        End Try

    End Sub

    Public Function DocumentosAGestionar_TotalesPorCodigo(ByVal CodigoCobrador As Integer) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Cantidad de Deudores Agrupados Por Codigo de Cobranza dentro de los doctos. a cobrar
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 26/05/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     26/08/2010          se agrega que sea distinto de los estado 5 y 13 
        'A Saldivar                 07/02/2011          Se agrega paginacion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            'Cantidad de Documentos --> DOR
            'Cantidad de Deudores --> DOR
            'Suma (Monto de Documentos(DOR) * Factor de Cambio(OPO))
            'Codigo de Cobranza --> DOR
            'Descripción de Código --> CCO
            'Estado de Gestion --> DOR


            'Join opo1 In Data.opo_cls On dor1.ope_num Equals opo1.opo_num _
            'Join deu1 In Data.deu_cls On dor1.ddr_ide Equals deu1.deu_ide _

            'dor1 In cco1.dor_cls _

            Dim CollTemporal = (From dor1 In Data.dor_cls _
                                Where dor1.doc_cls.dsi_cls.deu_cls.id_eje_cod_cob = CInt(CodigoCobrador) _
                                  And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                                Group By CodigoCobranza = dor1.doc_cls.cco_cls.cco_num, _
                                         DesCodigoCobranza = dor1.doc_cls.cco_cls.cco_des, _
                                         deudotre = dor1.doc_cls.dsi_cls.deu_cls.deu_ide, _
                                         Gestionado = dor1.dor_est _
                                                  Into CantidadDoctos = Count(), _
                                                       MontoDoctos = Sum(dor1.doc_cls.doc_sdo_ddr * dor1.doc_cls.dsi_cls.ope_cls.ope_fac_cam) _
                                Select CodigoCobranza, DesCodigoCobranza, CantidadDoctos, MontoDoctos, deudotre, Gestionado).Skip(sesion.NroPaginacion).Take(10)



            Dim final = From a In CollTemporal _
                        Group By CodigoCobranza = a.CodigoCobranza.Trim, DesCodigoCobranza = a.DesCodigoCobranza.Trim, Gestionado = a.Gestionado Into CantidadDeudores = Count(a.deudotre), CantidadDoctos = Sum(a.CantidadDoctos), MontoDoctos = Sum(a.MontoDoctos) _
                        Select CodigoCobranza, DesCodigoCobranza, CantidadDeudores, CantidadDoctos, MontoDoctos, Gestionado

            Return final

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosAGestionar_RetornaTotales(ByVal CodigoCobrador As Integer) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Totales de Doctos, Deudores y Montos Gestionados/No Gestionados
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 02/06/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     26-08-2010          se agrega que sea distinto de los estado 5 y 13 
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            'Monto Total de Doctos. Gestionado
            '# de Doctos Gestionados
            '# de Deudores Gestionados
            'Monto Total de Doctos No Gestionados
            '# de Doctos No Gestionados
            '# de Deudores No Gestionados

            'Join opo1 In Data.opo_cls On dor1.ope_num Equals opo1.opo_num _
            'Join deu1 In Data.deu_cls On dor1.ddr_ide Equals deu1.deu_ide _

            Dim CollTemporal = From dor1 In Data.dor_cls _
                                Where dor1.doc_cls.dsi_cls.deu_cls.id_eje_cod_cob = CInt(CodigoCobrador) _
                                  And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                                Group By deudotre = dor1.doc_cls.dsi_cls.deu_cls.deu_ide, _
                                         Gestionado = dor1.dor_est _
                                                  Into CantidadDoctos = Count(), _
                                                       MontoDoctos = Sum(dor1.doc_cls.doc_sdo_ddr * dor1.doc_cls.dsi_cls.ope_cls.ope_fac_cam) _
                                Select CantidadDoctos, MontoDoctos, deudotre, Gestionado

            Dim final = From a In CollTemporal _
                        Group By Gestionado = a.Gestionado Into CantidadDeudores = Count(a.deudotre), CantidadDoctos = Sum(a.CantidadDoctos), MontoDoctos = Sum(a.MontoDoctos) _
                        Select CantidadDeudores, CantidadDoctos, MontoDoctos, Gestionado

            Return final

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function DocumentosAGestionar_RetornaCliDeuACobrar(ByVal EstadoDocto1 As Int16, ByVal EstadoDocto2 As Int16, _
                                                            ByVal CodCobranza1 As String, ByVal CodCobranza2 As String, _
                                                            ByVal MuestraCliDeu As String, ByVal CodigoEjecutivo As Int16, _
                                                            ByVal Cliente_o_Deudor As String) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Clientes o Deudores a Cobrar por un cobrador Teléfonico
        'Creado por Jaime Santos C.
        'Fecha Creacion: 04/06/2008
        'Modificado por victor alvarez.
        'Fecha Modificado: 15/11/2011 se agrego nombre completo deudor.
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Dim colle_CliDeuACobrar As New Collection


            Select Case Cliente_o_Deudor
                Case "DEUDOR"

                    Dim Temporal_dor = From dor1 In Data.dor_cls _
                    Where dor1.doc_cls.dsi_cls.deu_cls.id_eje_cod_cob = CodigoEjecutivo _
                      And dor1.dor_est = "N" _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                      And dor1.doc_cls.cco_cls.cco_num >= CodCobranza1 And dor1.doc_cls.cco_cls.cco_num <= CodCobranza2 _
                      And dor1.doc_cls.dsi_cls.id_P_0011 >= EstadoDocto1 And dor1.doc_cls.dsi_cls.id_P_0011 <= EstadoDocto2 _
                    Group By Rut = dor1.doc_cls.dsi_cls.deu_ide, _
                             NombreDeudor = dor1.doc_cls.dsi_cls.deu_cls.deu_rso & " " & dor1.doc_cls.dsi_cls.deu_cls.deu_ape_ptn & " " & dor1.doc_cls.dsi_cls.deu_cls.deu_ape_mtn _
                    Into MontoDocto = Sum(dor1.doc_cls.doc_sdo_ddr * dor1.doc_cls.dsi_cls.ope_cls.ope_fac_cam), _
                        dat_cob_ant = Count(dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_cob_ant = "S"), _
                           Cantidad = Count(), _
                           Gestion = Count(If(dor1.dor_hor_prx Is Nothing, "N", "S") = "S") _
                    Select New With {.Rut = Rut, _
                                     .NombreDeudor = NombreDeudor, _
                                     .PAGO = 0, _
                                     .SOLO_HOY = 0, _
                                     .FECHA_SEGUIMIENTO = "01-01-2014", _
                                     .MontoDocto = MontoDocto, _
                                     .COBRANZA_ANTICIPADA = dat_cob_ant, _
                                     .NO_RECAUDADO = 0, _
                                     .Cantidad = Cantidad, _
                                      Gestion}

                    'Dim Temporal_dor = From dor1 In Data.dor_cls _
                    '                   Group Join drc1 In Data.drc_cls On drc1.gsn_cls.doc_cls.dsi_cls.deu_ide Equals dor1.doc_cls.dsi_cls.deu_ide _
                    '                                            Into dat_sol_hoy = Count(drc1.drc_fec_pag = Now.Date.ToShortDateString), _
                    '                                                  dat_no_rec = Count(drc1.id_P_0103 > 0) _
                    '                   Group Join gsn1 In Data.gsn_cls On gsn1.doc_cls.dsi_cls.deu_ide Equals dor1.doc_cls.dsi_cls.deu_ide _
                    '                                            Into dat_fec_seg = Count(gsn1.gsn_fec_prx = Now.Date.ToShortDateString) _
                    '                   Group Join ing_sec1 In Data.ing_sec_cls On ing_sec1.doc_cls.dsi_cls.deu_ide Equals dor1.doc_cls.dsi_cls.deu_ide _
                    '                                            Into PAGO = Count(ing_sec1.id_P_0053 = 2 _
                    '                                                              And (ing_sec1.ing_vld_rcz = "I" Or _
                    '                                                                   ing_sec1.ing_vld_rcz = "S" Or _
                    '                                                                   ing_sec1.ing_vld_rcz = "V" Or _
                    '                                                                   ing_sec1.ing_vld_rcz = "C") _
                    '                                                              And ing_sec1.ing_cls.id_hre > 0) _
                    'Where dor1.doc_cls.dsi_cls.deu_cls.id_eje_cod_cob = CodigoEjecutivo _
                    '  And dor1.dor_est = "N" _
                    '  And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 _
                    '  And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                    '  And dor1.doc_cls.cco_cls.cco_num >= CodCobranza1 And dor1.doc_cls.cco_cls.cco_num <= CodCobranza2 _
                    '  And dor1.doc_cls.dsi_cls.id_P_0011 >= EstadoDocto1 And dor1.doc_cls.dsi_cls.id_P_0011 <= EstadoDocto2 _
                    'Group By Rut = dor1.doc_cls.dsi_cls.deu_ide, _
                    '         NombreDeudor = dor1.doc_cls.dsi_cls.deu_cls.deu_rso & " " & dor1.doc_cls.dsi_cls.deu_cls.deu_ape_ptn & " " & dor1.doc_cls.dsi_cls.deu_cls.deu_ape_mtn, _
                    '         dat_fec_seg, _
                    '         dat_no_rec, _
                    '         dat_sol_hoy, _
                    '         PAGO _
                    'Into MontoDocto = Sum(dor1.doc_cls.doc_sdo_ddr * dor1.doc_cls.dsi_cls.ope_cls.ope_fac_cam), _
                    '    dat_cob_ant = Count(dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_cls.cli_cob_ant = "S"), _
                    '       Cantidad = Count(), _
                    '       Gestion = Count(If(dor1.dor_hor_prx Is Nothing, "N", "S") = "S") _
                    'Select New With {.Rut = Rut, _
                    '                 .NombreDeudor = NombreDeudor, _
                    '                 .PAGO = PAGO, _
                    '                 .SOLO_HOY = dat_sol_hoy, _
                    '                 .FECHA_SEGUIMIENTO = dat_fec_seg, _
                    '                 .MontoDocto = MontoDocto, _
                    '                 .COBRANZA_ANTICIPADA = dat_cob_ant, _
                    '                 .NO_RECAUDADO = dat_no_rec, _
                    '                 .Cantidad = Cantidad, _
                    '                  Gestion}

                    For Each a In Temporal_dor
                        colle_CliDeuACobrar.Add(a)
                    Next

                    Return colle_CliDeuACobrar

                Case "CLIENTE"
                    'Dim Temporal_dor = From dor1 In Data.dor_cls _
                    '                   Join cli1 In Data.cli_cls On cli1.cli_idc Equals dor1.cli_idc _
                    '                   Join opo1 In Data.opo_cls On dor1.ope_num Equals opo1.opo_num _
                    '                   Group Join drc1 In Data.drc_cls On drc1.cli_idc Equals dor1.cli_idc _
                    '                                            And drc1.ddr_ide Equals dor1.ddr_ide _
                    '                                            And drc1.id_P_0031 Equals dor1.id_P_0031 _
                    '                                            And drc1.ope_num Equals dor1.ope_num _
                    '                                            And drc1.drc_num_doc Equals dor1.dor_num_doc _
                    '                                            And drc1.doc_flj_num Equals dor1.doc_flj_num _
                    '                                            Into dat_sol_hoy = Count(drc1.drc_fec_pag = Now.Date.ToShortDateString) _
                    '                   Group Join gsn1 In Data.gsn_cls On gsn1.cli_idc Equals dor1.cli_idc _
                    '                                            And gsn1.ddr_ide Equals dor1.ddr_ide _
                    '                                            And gsn1.id_P_0031 Equals dor1.id_P_0031 _
                    '                                            And gsn1.ope_num Equals dor1.ope_num _
                    '                                            And gsn1.gsn_num_doc Equals dor1.dor_num_doc _
                    '                                            And gsn1.doc_flj_num Equals dor1.doc_flj_num _
                    '                                            Into dat_fec_seg = Count(gsn1.gsn_fec_prx = Now.Date.ToShortDateString) _
                    '                   Group Join drc2 In Data.drc_cls On drc2.cli_idc Equals dor1.cli_idc _
                    '                                            And drc2.ddr_ide Equals dor1.ddr_ide _
                    '                                            And drc2.id_P_0031 Equals dor1.id_P_0031 _
                    '                                            And drc2.ope_num Equals dor1.ope_num _
                    '                                            And drc2.drc_num_doc Equals dor1.dor_num_doc _
                    '                                            And drc2.doc_flj_num Equals dor1.doc_flj_num _
                    '                                            Into dat_no_rec = Count(drc2.drc_est_rec > 0) _
                    '                   Group Join ing1 In Data.ing_cls On ing1.cli_idc Equals dor1.cli_idc _
                    '                                            Into PAGO = Count(ing1.pnu_ing_qin = 2 And ing1.ing_doc_nce = "N" _
                    '  And (ing1.ing_vld_rcz = "I" Or ing1.ing_vld_rcz = "S" Or ing1.ing_vld_rcz = "V" Or ing1.ing_vld_rcz = "C") And ing1.hre_num > 0) _
                    'Where cli1.eje_cod_cob = CodigoEjecutivo _
                    '  And dor1.dor_est = "N" _
                    '  And dor1.cco_num >= CodCobranza1 And dor1.cco_num <= CodCobranza2 _
                    '  And dor1.pnu_est_doc >= EstadoDocto1 And dor1.pnu_est_doc <= EstadoDocto2 _
                    'Group By Rut = dor1.cli_idc, NombreDocto = cli1.cli_rso, dat_fec_seg, dat_no_rec, dat_sol_hoy, PAGO _
                    'Into MontoDocto = Sum(dor1.doc_sdo_ddr * opo1.opo_fac_cam), dat_cob_ant = Count(cli1.cli_cob_ant = "S") _
                    'Select Rut, NombreDocto, PAGO, SOLO_HOY = dat_sol_hoy, FECHA_SEGUIMIENTO = dat_fec_seg, _
                    '       MontoDocto, COBRANZA_ANTICIPADA = dat_cob_ant, NO_RECAUDADO = dat_no_rec
                    'For Each a In Temporal_dor
                    '    colle_CliDeuACobrar.Add(a)
                    'Next
                    'Return colle_CliDeuACobrar
                    Return Nothing
            End Select

        Catch ex As Exception
        End Try

    End Function




    Public Function DiasDePago_Retorna(ByVal rut_deudor As String) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Dias de pago por Deudor
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 19/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim data As New DataClsFactoringDataContext


            Dim Temporal_dpa = From dpa1 In data.dpa_cls _
                               Where dpa1.deu_ide = rut_deudor _
                               Select dpa1

            Return Temporal_dpa

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Gestiones_Retorna(ByVal Id_gsn1 As Int64, ByVal Id_gsn2 As Int64, _
                                      ByVal rut_cliente1 As String, ByVal rut_cliente2 As String, _
                                      ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, _
                                      ByVal nro_operacion1 As Int64, ByVal nro_operacion2 As Int64, _
                                      ByVal tipo_docto1 As Int16, ByVal tipo_docto2 As Int16, _
                                      ByVal nro_docto1 As String, ByVal nro_docto2 As String, _
                                      ByVal nro_cuota1 As Int16, ByVal nro_cuota2 As Int16) As Object
        '*********************************************************************************************************************************
        'Descripcion: Devuelve Gestiones 
        '!!!!IMPORTANTE!!!! Agregar Criterios de acuerdo a necesidades genericas 
        'Creado por= Jaime Santos C.
        'Fecha Creacion: 30/06/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext

            Dim Temporal_gsn = From gsn1 In data.gsn_cls _
                               Where (gsn1.id_gsn >= Id_gsn1 And gsn1.id_gsn <= Id_gsn2) _
                                 And (gsn1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc >= rut_cliente1 And gsn1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc <= rut_cliente2) _
                                 And (gsn1.doc_cls.dsi_cls.deu_ide >= rut_deudor1 And gsn1.doc_cls.dsi_cls.deu_ide <= rut_deudor2) _
                                 And (gsn1.doc_cls.id_opo >= nro_operacion1 And gsn1.doc_cls.id_opo <= nro_operacion2) _
                                 And (gsn1.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031 >= tipo_docto1 And gsn1.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031 <= tipo_docto2) _
                                 And (gsn1.doc_cls.dsi_cls.dsi_num >= nro_docto1 And gsn1.doc_cls.dsi_cls.dsi_num <= nro_docto2) _
                                 And (gsn1.doc_cls.dsi_cls.dsi_flj_num >= nro_cuota1 And gsn1.doc_cls.dsi_cls.dsi_flj_num <= nro_cuota2) _
                                 Select gsn1.gsn_fec_pag, gsn1.gsn_hor_pag_dde, gsn1.gsn_hor_pag, gsn1.gsn_fec_prx, gsn1.gsn_hor_prx, _
                                        gsn1.gsn_alo_obs, gsn1.cco_cls.cco_num, gsn1.cco_cls.cco_des, gsn1.id_gsn, _
                                        gsn1.ddi_cls.cmn_cls.cmn_des, gsn1.ddi_cls.cmn_cls.ciu_cls.ciu_des, gsn1.ddi_cls.cmn_cls.zon_cls.zon_des, _
                                        gsn1.gsn_dir_cbz, gsn1.gsn_doc_rtr_pag, gsn1.gsn_fec, gsn1.gsn_hor, gsn1.doc_cls.dsi_cls.ope_cls.opn_cls.eje_cls.eje_nom, _
                                        gsn_obs = gsn1.gsn_obs & " " & gsn1.gsn_obs_1 & " " & gsn1.gsn_obs_2

            'cambie de id_doc por doc_num
            'gsn1.eje_cls.eje_nom
            Return Temporal_gsn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DocumentosAGestionar_RetornaCliDeuAsociado(ByVal EstadoDocto1 As Int16, ByVal EstadoDocto2 As Int16, _
                                                                ByVal CodCobranza1 As String, ByVal CodCobranza2 As String, _
                                                                ByVal RutDeuCli As String, ByVal Cliente_o_Deudor As String) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Clientes o Deudores Asociados a un Cliente o Deudor a Cobrar por un cobrador Teléfonico
        'Creado por Jaime Santos C.
        'Fecha Creacion: 04/06/2008
        'Quien Modifica              Fecha              Descripcion
        ' JLagos                    26-08-2010         -Se quita como agrupacion la hora de proxima gestion
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext
            Select Case Cliente_o_Deudor
                Case "DEUDOR"
                    Dim Temporal_dor = From dor1 In Data.dor_cls _
                                       Join deu1 In Data.deu_cls On deu1.deu_ide Equals dor1.doc_cls.dsi_cls.deu_ide _
                    Where dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = RutDeuCli _
                      And dor1.dor_est = "N" _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                      And dor1.doc_cls.cco_cls.cco_num >= CodCobranza1 And dor1.doc_cls.cco_cls.cco_num <= CodCobranza2 _
                      And dor1.doc_cls.dsi_cls.id_P_0011 >= EstadoDocto1 And dor1.doc_cls.dsi_cls.id_P_0011 <= EstadoDocto2 _
                      Order By dor1.doc_cls.dsi_cls.deu_ide _
                      Select Rut = dor1.doc_cls.dsi_cls.deu_ide, RazonSocial = deu1.deu_rso.Trim _
                      Distinct
                    Return Temporal_dor

                Case "CLIENTE"


                    Dim Temporal_dor = From dor1 In Data.dor_cls _
                                       Join cli1 In Data.cli_cls On cli1.cli_idc Equals dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc _
                    Where (dor1.doc_cls.cco_cls.cco_num >= CodCobranza1 And dor1.doc_cls.cco_cls.cco_num <= CodCobranza2) _
                      And (dor1.doc_cls.dsi_cls.id_P_0011 >= EstadoDocto1 And dor1.doc_cls.dsi_cls.id_P_0011 <= EstadoDocto2) _
                      And dor1.doc_cls.dsi_cls.deu_ide = RutDeuCli _
                      And dor1.dor_est = "N" _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 _
                      And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                      Group By dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc, _
                               RazonSocial = cli1.cli_rso.Trim & " " & cli1.cli_ape_ptn.Trim & " " & cli1.cli_ape_mtn.Trim _
                      Into Suma = Sum(dor1.doc_cls.doc_sdo_ddr * dor1.doc_cls.opo_cls.ope_cls.ope_fac_cam), _
                           Cant = Count() _
                      Select Rut = cli_idc, _
                             RazonSocial, _
                             Suma, _
                             Cant

                    Return Temporal_dor

            End Select

        Catch ex As Exception
        End Try
    End Function

    Public Function Contactos_RetornaContactosGestion(ByVal CliDeu As String, ByVal RutDeudor As String, ByVal RutCliente As String) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Clientes o Deudores Asociados a un Cliente o Deudor a Cobrar por un cobrador Teléfonico
        'Creado por Jaime Santos C.
        'Fecha Creacion: 04/06/2008
        'Quien Modifica              Fecha              Descripcion
        'P.Gatica                    14/05/2009         Se modifica para caso deudor , preguntando solo por este .
        '**************************************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext

            Select Case CliDeu
                Case "DEUDOR"
                    Dim Temporal_cnc = From cnc1 In Data.cnc_cls _
                                       Where _
                                        cnc1.deu_ide = RutDeudor _
                                       And cnc1.cnc_cli_ddr = "D" _
                                       Select cnc1
                    Return Temporal_cnc
                Case "CLIENTE"
                    Dim Temporal_cnc = From cnc1 In Data.cnc_cls _
                                       Where cnc1.cli_idc = RutCliente _
                                       And cnc1.cnc_cli_ddr = "C" _
                                       Select cnc1
                    Return Temporal_cnc
            End Select

        Catch ex As Exception
        End Try

    End Function

    Public Function DocumentosAGestionar_RetornaDoctosGestionar(ByVal rut_cliente As String, ByVal rut_deudor As String) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Clientes o Deudores Asociados a un Cliente o Deudor a Cobrar por un cobrador Teléfonico
        'Creado por Jaime Santos C.
        'Fecha Creacion: 04/06/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim colle_DoctosACobrar As New Collection



            Dim Temporal_dor = From dor1 In Data.dor_cls _
                                Group Join drc1 In Data.drc_cls On drc1.gsn_cls.id_doc Equals dor1.id_doc _
                                              Into cantdrc = Count(drc1.drc_pen = "S") _
                                Group Join ing_sec1 In Data.ing_sec_cls On ing_sec1.id_doc Equals dor1.id_doc _
                                                               Into pago = Count(ing_sec1.id_P_0053 = 2 _
                                                                             And (ing_sec1.ing_vld_rcz = "I" Or _
                                                                                  ing_sec1.ing_vld_rcz = "S" Or _
                                                                                  ing_sec1.ing_vld_rcz = "V" Or _
                                                                                  ing_sec1.ing_vld_rcz = "C")) _
            Where dor1.doc_cls.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc = rut_cliente _
                And dor1.doc_cls.dsi_cls.deu_ide = rut_deudor _
                And dor1.dor_est = "N" _
                And dor1.doc_cls.dsi_cls.id_P_0011 <> 5 _
                And dor1.doc_cls.dsi_cls.id_P_0011 <> 13 _
                And dor1.doc_cls.dsi_cls.dsi_flj = "N" _
                Select New With {dor1.id_doc, _
                       dor1.id_suc_orn, _
                       dor1.suc_cls.suc_des_cra, _
                       dor1.dor_hor_prx, _
                       dor1.doc_cls.id_opo, _
                       .pnu_des = dor1.doc_cls.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                       .dor_num_doc = dor1.doc_cls.dsi_cls.dsi_num, _
                       .doc_flj_num = dor1.doc_cls.dsi_cls.dsi_flj_num, _
                       .doc_fev_rea = dor1.doc_cls.dsi_cls.dsi_fev_rea, _
                       .doc_fev_ori = dor1.doc_cls.dsi_cls.dsi_fev_ori, _
                       .Estado = dor1.doc_cls.dsi_cls.P_0011_cls.pnu_des, _
                       .Estado_Ver = dor1.doc_cls.dsi_cls.P_0040_cls.pnu_des, _
                       .id_cco = dor1.doc_cls.id_cco, _
                       .cco_num = dor1.doc_cls.cco_cls.cco_num, _
                       dor1.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0023, _
                       .Moneda = dor1.doc_cls.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_atr_003, _
                       dor1.doc_cls.dsi_cls.ope_cls.opn_cls.id_P_0031, _
                       cantdrc, _
                       dor1.doc_cls.dsi_cls.id_P_0011, _
                       pago, _
                       .doc_sdo_cli = dor1.doc_cls.doc_sdo_cli, _
                       .doc_sdo_ddr = dor1.doc_cls.doc_sdo_ddr, _
                       .MONTO = dor1.doc_cls.dsi_cls.dsi_mto}

            Dim SaldoCli As Double
            Dim SaldoDeu As Double

            'Return Temporal_dor
            For Each a In Temporal_dor

                Try

                    SaldoCli = (From I In Data.ing_sec_cls Where I.id_doc = a.id_doc And _
                                                                              I.id_P_0053 = 2 And _
                                                                             (I.ing_vld_rcz = "S" Or _
                                                                              I.ing_vld_rcz = "I" Or _
                                                                              I.ing_vld_rcz = "V" Or _
                                                                              I.ing_vld_rcz = "C" Or _
                                                                              I.ing_vld_rcz = "L") And _
                                                                              I.ing_pro = "N" And _
                                                                              I.egr_sec_cls.egr_cls.id_apl <> 0 _
                                                                   Select (I.ing_mto_abo / I.ing_fac_cam)).Sum

                Catch ex As Exception

                End Try

                Try

                    SaldoDeu = (From I In Data.ing_sec_cls Where I.id_doc = a.id_doc And _
                                                                  I.id_P_0053 = 2 And _
                                                                 (I.ing_vld_rcz = "S" Or _
                                                                  I.ing_vld_rcz = "I" Or _
                                                                  I.ing_vld_rcz = "V" Or _
                                                                  I.ing_vld_rcz = "C" Or _
                                                                  I.ing_vld_rcz = "L") And _
                                                                  I.ing_pro = "N" And _
                                                                  I.ing_qpa = "D" And _
                                                                  I.egr_sec_cls.egr_cls.id_apl <> 0 _
                                                       Select (I.ing_mto_tot / I.ing_fac_cam)).Sum


                Catch ex As Exception

                End Try

                a.doc_sdo_cli = a.doc_sdo_cli - SaldoCli
                a.doc_sdo_ddr = a.doc_sdo_ddr - SaldoDeu

                colle_DoctosACobrar.Add(a)

            Next

            Return colle_DoctosACobrar

        Catch ex As Exception
        End Try
    End Function


    Public Function DireccionDeudorRecaudacion_Devolver(ByVal CodigoDireccion1 As String, ByVal CodigoDireccion2 As String, _
                                                        ByVal rut_deudor1 As String, ByVal rut_deudor2 As String, Optional ByVal tipo As Integer = 1) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Direcciones de çrecaudacion asociadas a un deudor en particular
        'Creado por Jaime Santos C.
        'Fecha Creacion: 17/06/2008
        'Quien Modifica              Fecha              Descripcion
        'C Arce                     08/09/2011          se agrega direccion del deudor.
        '**************************************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim RW As New FuncionesGenerales.RutinasWeb

            If tipo = 1 Then

                Dim Temporal_ddi = From ddi1 In Data.ddi_cls _
           Where ddi1.id_ddi >= CodigoDireccion1 And ddi1.id_ddi <= CodigoDireccion2 _
           And ddi1.deu_ide >= rut_deudor1 And ddi1.deu_ide <= rut_deudor2 _
           Select ddi1.id_ddi, _
                  ddi1.deu_ide, _
                  ddi1.cmn_cls.zon_cls.id_suc, _
                  ddi1.cmn_cls.id_ciu, _
                  ddi1.id_cmn, _
                  ddi1.ddr_dml_cbz, _
                  ddi1.deu_cls.deu_dml

                Return Temporal_ddi
            End If

            If tipo = 2 Then
                Dim direccion = (From d In Data.gsn_cls Where d.ddi_cls.deu_ide = rut_deudor1 And d.ddi_cls.deu_ide = rut_deudor2 _
                                        Select d.ddi_cls.id_cmn, gsn_dir_cbz = d.gsn_dir_cbz & "//" & d.ddi_cls.cmn_cls.cmn_des, d.ddi_cls.deu_cls.deu_dml, d.ddi_cls.id_ddi).Distinct
                Return direccion

            End If

        Catch ex As Exception

            Return Nothing

        End Try


    End Function

    Public Function DireccionDeudor(ByVal id As Integer) As ddi_cls
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve Direcciones de çrecaudacion asociadas a un deudor en particular
        'Creado por Jaime Santos C.
        'Fecha Creacion: 17/06/2008
        'Quien Modifica              Fecha              Descripcion
        'C Arce                     08/09/2011          se agrega direccion del deudor.
        '**************************************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim RW As New FuncionesGenerales.RutinasWeb
            Dim Temporal_ddi = (From ddi1 In Data.ddi_cls Where ddi1.id_ddi = id _
            Select ddi1).First
            'Select ddi1.deu_ide, _
            '       ddi1.cmn_cls.zon_cls.id_suc, _
            '       ddi1.cmn_cls.id_ciu, _
            '       ddi1.id_cmn, _
            '       ddi1.ddr_dml_cbz, _
            '       ddi1.deu_cls.deu_dml
            Return Temporal_ddi

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Asig_anteriorInserta(ByVal asignacion As Collection) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Inserta asignacion anterior
        'Creado por= Cristian Arce Salgado.
        'Fecha Creacion: 06/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext

            For I = 1 To asignacion.Count
                Dim ant As New has_cls

                Dim Rut As String = RG.LimpiaRut(asignacion.Item(I).deu_ide)
                Rut = Format(CLng(Rut), Var.FMT_RUT)
                ant.deu_ide = Rut
                ant.id_eje_nue = asignacion.Item(I).id_eje_nue
                ant.id_eje_ant = asignacion.Item(I).id_eje_ant
                ant.fec_asi_rea = asignacion.Item(I).fec_asi_rea
                ant.id_eje = asignacion.Item(I).id_eje
                Data.has_cls.InsertOnSubmit(ant)
            Next

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function rescata_dvf(ByVal rut_deu As String, ByVal nro_doc As Int64, ByVal mto_doc As Double, ByVal tipo As Integer) As dvf_cls
        '*********************************************************************************************************************************
        'Descripcion: devuelve dvf
        'Creado por: Cristian Arce Salgado.
        'Fecha Creacion: 07/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim dvf As dvf_cls = (From d In Data.dvf_cls Where d.deu_ide = rut_deu And _
                                                               d.dvf_num = nro_doc And _
                                                               d.dvf_mto = mto_doc And _
                                                               d.id_P_0031 = tipo _
                                  Select d).First
            Return dvf

        Catch ex As Exception
            Return (Nothing)
        End Try

    End Function

    Public Function Retorna_id_dsi(ByVal id_Doc As Integer) As dsi_cls
        '*********************************************************************************************************************************
        'Descripcion: Retorna dsi 
        'Creado por:Cristian Arce Salgado.
        'Fecha Creacion:08/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext
            Dim c As dsi_cls = (From x In Data.doc_cls Where x.id_doc = id_Doc Select x.dsi_cls).First
            Return (c)
        Catch ex As Exception
            Return (Nothing)
        End Try

    End Function

    Public Function deudor(ByVal rut_deu As Long) As deu_cls
        '*********************************************************************************************************************************
        'Descripcion: Retorna deu 
        'Creado por:Cristian Arce Salgado.
        'Fecha Creacion:08/09/2011
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try
            Dim Data As New DataClsFactoringDataContext
            Dim id As deu_cls = (From x In Data.deu_cls Where x.deu_ide = rut_deu Select x).First
            Return (id)
        Catch ex As Exception
            Return (Nothing)
        End Try

    End Function

    Public Function CodigoCobranza_RetornaGestionar(ByVal TipoConsulta As Int16, Optional ByVal CodigoCobranza As String = "") As Object
        '**************************************************************************************************************************************************
        'Descripcion: Retorna Códigos de Cobranza
        'Creado por Jaime Santos C.
        'Fecha Creacion: 22/07/2008
        'Quien Modifica: Yonathan Cabezas V. Fecha: 11/03/2009    Descripcion: Se modifica para retornar Cobranza 
        '                                                                      para un Código en particular (case 0)
        '               Cristian Arce                                           se agrega Return al case 0
        '**************************************************************************************************************************************************
        Try

            Dim data As New DataClsFactoringDataContext

            Select Case TipoConsulta
                Case 0
                    Dim Temporal_cco = (From c In data.cco_cls _
                                      Where c.cco_num = CodigoCobranza _
                                      Select c).First
                    Return Temporal_cco
                Case 1
                    Dim Temporal_cco = From c In data.cco_cls _
                       Select c.id_cco, descripcion = CStr(c.cco_num) + "     " + CStr(c.cco_des)


                    Return Temporal_cco
                Case Else
                    Dim Temporal_cco = From c In data.cco_cls _
                                   Where c.cco_pri > 0 _
                                   Order By c.cco_pri _
                       Select c.id_cco, descripcion = CStr(c.cco_num) + "     " + CStr(c.cco_des)

                    Return Temporal_cco
            End Select

        Catch ex As Exception
            Return (Nothing)
        End Try
    End Function

    Public Function RetornaUltimo_DocumentosARecaudar(ByVal id_doc As Long) As Object
        '**************************************************************************************************************************************************
        'Descripcion: Devuelve último DRC relacionado a un documento
        'Creado por Jaime Santos C.
        'Fecha Creacion: 04/06/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim data As New DataClsFactoringDataContext

            Dim Temporal_drc = From c In data.drc_cls _
                Aggregate maxid In data.drc_cls Into maximoid = Max(maxid.id_drc) _
                   Where c.gsn_cls.id_doc = id_doc _
                     And c.id_drc = maximoid _
                   Select c


            Return Temporal_drc


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub CobranzaEstadosDevuelve(ByVal Dp As DropDownList)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve cobranzas
        'Creado por: Jorge Lagos
        'Fecha Creacion: 16/06/2009
        'Quien Modifica              Fecha              Descripcion
        'Se agrega por la funcion CobranzaDevuelve no esta cumpliendo correctamente
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Cob = From C In Data.cco_cls Select Codigo = C.id_cco, Descripcion = C.cco_num & " " & C.cco_des.Trim

            If Cob.Count <> 0 Then
                RW.Llenar_Drop(Cob, "Codigo", "Descripcion", Dp)
            End If


        Catch ex As Exception
        End Try

    End Sub

    Public Sub CodigosCobranzaDevuelve(ByVal Dp As DropDownList)
        '*********************************************************************************************************************************
        'Descripcion: Devuelve cobranzas
        'Creado por: Jorge Lagos
        'Fecha Creacion: 16/06/2009
        'Quien Modifica              Fecha              Descripcion
        'Se agrega por la funcion CobranzaDevuelve no esta cumpliendo correctamente
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Cob = From C In Data.cco_cls Select Codigo = C.cco_des, Descripcion = C.cco_num.Trim

            If Cob.Count <> 0 Then
                RW.Llenar_Drop(Cob, "Codigo", "Descripcion", Dp)
            End If


        Catch ex As Exception
        End Try

    End Sub

    Public Function Doctos_asig_esp_retorna(ByVal rut_cli As String, ByVal rut_deu As String, ByVal rut_deu1 As String, ByVal con_deudor As Boolean, ByVal modulo As Integer, _
                                            ByVal fecha_dde As DateTime, ByVal fecha_hta As DateTime, ByVal num_otg As Integer, ByVal num_otg1 As Int64, _
                                            ByVal num_doc As String, ByVal num_doc1 As String, ByVal mto_des As Double, ByVal mto_has As Double, _
                                            ByVal estado As Integer, ByVal estado1 As Integer, ByVal tipo As Int64, ByVal tipo1 As Int64, Optional ByVal condicion As String = "") As Collection

        '*********************************************************************************************************************************
        'Descripcion: Devuelve Documentos para asignar a Recaudador 
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 20/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          
        'jlagos                     26-08-2010          se agrega estado 13(anulado)
        'jlagos                     15-12-2011          se agrega apellido paterno y materno al deudor
        '*********************************************************************************************************************************

        Dim Data As New DataClsFactoringDataContext

        Dim col As New Collection



        If modulo = 1 Then



            Dim doc1 = From d In Data.doc_cls _
                 Where (CLng(d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc) >= CLng(rut_cli) And CLng(d.dsi_cls.ope_cls.opn_cls.eva_cls.cli_idc) <= CLng(rut_cli)) _
                And (CLng(d.dsi_cls.deu_ide) >= CLng(rut_deu) And CLng(d.dsi_cls.deu_ide) <= CLng(rut_deu1)) _
                And (d.dsi_cls.dsi_mto >= mto_des _
                 And d.dsi_cls.dsi_mto <= mto_has) _
                 And (d.dsi_cls.dsi_fev_rea >= CDate(fecha_dde) And d.dsi_cls.dsi_fev_rea <= CDate(fecha_hta)) _
                 And d.dsi_cls.ope_cls.id_P_0030 = 3 _
                 And (d.opo_cls.opo_otg >= num_otg And d.opo_cls.opo_otg <= num_otg1) _
                 And (d.dsi_cls.dsi_num >= num_doc And d.dsi_cls.dsi_num <= num_doc1) _
                    And (d.dsi_cls.id_P_0011 >= estado And d.dsi_cls.id_P_0011 <= estado1) _
                    And d.dsi_cls.id_P_0011 <> 3 _
                     And d.dsi_cls.id_P_0011 <> 5 _
                      And d.dsi_cls.id_P_0011 <> 6 _
                       And d.dsi_cls.id_P_0011 <> 7 _
                        And d.dsi_cls.id_P_0011 <> 8 _
                         And d.dsi_cls.id_P_0011 <> 10 _
                          And d.dsi_cls.id_P_0011 <> 13 _
                    And (d.dsi_cls.ope_cls.opn_cls.id_P_0031 >= tipo And d.dsi_cls.ope_cls.opn_cls.id_P_0031 <= tipo1) Order By d.dsi_cls.dsi_fev_rea _
Select New With {d.dsi_cls.deu_ide, _
              .deu_rso = IIf(d.dsi_cls.deu_cls.id_P_0044 = 1, d.dsi_cls.deu_cls.deu_rso & " " & d.dsi_cls.deu_cls.deu_ape_ptn & " " & d.dsi_cls.deu_cls.deu_ape_mtn, d.dsi_cls.deu_cls.deu_rso), _
              d.dsi_cls.ope_cls.opn_cls.id_P_0031, _
              d.dsi_cls.ope_cls.opn_cls.P_0031_cls.pnu_des, _
              d.opo_cls.opo_otg, _
              d.dsi_cls.dsi_num, _
              d.dsi_cls.ope_cls.opn_cls.id_P_0023, _
              d.dsi_cls.dsi_flj_num, _
              d.dsi_cls.dsi_fev_rea, _
              d.dsi_cls.dsi_mto, _
              d.dsi_cls.id_P_0011, _
              .Moneda_ope = d.dsi_cls.ope_cls.opn_cls.P_0023_cls.pnu_des, _
              d.doc_sdo_cli, _
              d.id_doc, _
              d.dsi_cls.id_ope, _
              d.doc_sdo_ddr, _
              .id_dor = If(((From dor In Data.dor_cls Where dor.id_doc = d.id_doc _
                                                         And dor.dor_est = "N" _
                                                         Select dor.id_dor).Count > 0), _
                              (From dor In Data.dor_cls Where dor.id_doc = d.id_doc _
                                                          And dor.dor_est = "N" _
                                                          Select dor.id_dor).First, 0), _
              d.id_cco, _
              d.cco_cls.cco_num}

            For Each p In doc1
                col.Add(p)
            Next

            Return col

            'Modulo Cobranza - Con Deudor

        Else

            'Modulo Recaudación - Con Deudor

            If con_deudor Then

                Dim doc = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                        Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi _
                                     Where d.dsi_cls.dsi_mto >= mto_des _
                                        And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                        And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                        And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                        And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                   And d.dsi_cls.id_P_0011 <> 3 _
                     And d.dsi_cls.id_P_0011 <> 5 _
                      And d.dsi_cls.id_P_0011 <> 6 _
                       And d.dsi_cls.id_P_0011 <> 7 _
                        And d.dsi_cls.id_P_0011 <> 8 _
                         And d.dsi_cls.id_P_0011 <> 10 _
                          And d.dsi_cls.id_P_0011 <> 13 _
                                           And dsi.deu_ide = rut_deu _
                                           And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                                                                               Select d.id_doc, d.id_dsi

                For Each p In doc

                    Dim val As Integer

                    Try


                        Dim dr = From drc In Data.drc_cls Where drc.gsn_cls.id_doc = p.id_doc Select drc

                        If dr.Count = 0 Then


                            Dim doc1 = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                                 Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
                                 And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                 And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                 And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                 And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                     And d.dsi_cls.id_P_0011 <> 3 _
                     And d.dsi_cls.id_P_0011 <> 5 _
                      And d.dsi_cls.id_P_0011 <> 6 _
                       And d.dsi_cls.id_P_0011 <> 7 _
                        And d.dsi_cls.id_P_0011 <> 8 _
                         And d.dsi_cls.id_P_0011 <> 10 _
                          And d.dsi_cls.id_P_0011 <> 13 _
                                    And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                    And d.id_doc = p.id_doc _
                                    And d.dsi_cls.deu_ide = rut_deu _
                            Select dsi.deu_ide, _
                            deu_rso = IIf(dsi.deu_cls.id_P_0044 = 1, _
                                          dsi.deu_cls.deu_rso & " " & dsi.deu_cls.deu_ape_ptn & " " & dsi.deu_cls.deu_ape_mtn, _
                                          dsi.deu_cls.deu_rso), _
                            dsi.ope_cls.opn_cls.id_P_0031, _
                            dsi.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            opo.opo_otg, _
                            dsi.dsi_num, _
                            dsi.dsi_flj_num, _
                            dsi.dsi_fev_rea, _
                            dsi.dsi_mto, _
                            dsi.id_P_0011, _
                            dsi.ope_cls.opn_cls.id_P_0023, _
                            Moneda_ope = dsi.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            d.doc_sdo_cli, _
                            dsi.id_ope, _
                            d.doc_sdo_ddr, _
                            d.id_doc, _
                            d.id_cco

                            For Each px In doc1
                                col.Add(px)
                            Next

                        End If

                    Catch ex As Exception
                        val = 1
                    End Try

                Next

                Return col

            Else
                'Modulo Recaudación - Sin Deudor
                Dim doc = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
             Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
             And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
             And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
             And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
             And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                 And d.dsi_cls.id_P_0011 <> 3 _
                     And d.dsi_cls.id_P_0011 <> 5 _
                      And d.dsi_cls.id_P_0011 <> 6 _
                       And d.dsi_cls.id_P_0011 <> 7 _
                        And d.dsi_cls.id_P_0011 <> 8 _
                         And d.dsi_cls.id_P_0011 <> 10 _
                          And d.dsi_cls.id_P_0011 <> 13 _
                And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                Select d.id_doc, d.id_dsi


                'And Not (From drc In Data.drc_cls Where drc.gsn_cls.id_doc = d.id_doc Select drc.gsn_cls.id_doc).First _

                For Each p In doc

                    Dim val As Integer


                    Try


                        Dim dr = From drc In Data.drc_cls Where drc.gsn_cls.id_doc = p.id_doc Select drc

                        If dr.Count = 0 Then


                            Dim doc1 = From d In Data.doc_cls Join opo In Data.opo_cls On d.id_opo Equals opo.id_opo _
                                 Join dsi In Data.dsi_cls On d.id_dsi Equals dsi.id_dsi Where d.dsi_cls.dsi_mto >= mto_des _
                                 And d.dsi_cls.dsi_mto <= mto_has And dsi.dsi_fev_rea >= fecha_dde And dsi.dsi_fev_rea <= fecha_hta _
                                 And dsi.dsi_flj = "N" And opo.ope_cls.id_P_0030 = 3 _
                                 And opo.opo_otg >= num_otg And opo.opo_otg <= num_otg1 _
                                 And dsi.dsi_num >= num_doc And dsi.dsi_num <= num_doc1 _
                                     And d.dsi_cls.id_P_0011 <> 3 _
                     And d.dsi_cls.id_P_0011 <> 5 _
                      And d.dsi_cls.id_P_0011 <> 6 _
                       And d.dsi_cls.id_P_0011 <> 7 _
                        And d.dsi_cls.id_P_0011 <> 8 _
                         And d.dsi_cls.id_P_0011 <> 10 _
                          And d.dsi_cls.id_P_0011 <> 13 _
                                    And dsi.ope_cls.opn_cls.eva_cls.cli_idc = rut_cli _
                                    And d.id_doc = p.id_doc _
                            Select dsi.deu_ide, _
                            deu_rso = IIf(dsi.deu_cls.id_P_0044 = 1, _
                                           dsi.deu_cls.deu_rso, _
                                           dsi.deu_cls.deu_rso & " " & dsi.deu_cls.deu_ape_ptn & " " & dsi.deu_cls.deu_ape_mtn), _
                            dsi.ope_cls.opn_cls.id_P_0031, _
                            dsi.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                            opo.opo_otg, _
                            dsi.dsi_num, _
                            dsi.dsi_flj_num, _
                            dsi.dsi_fev_rea, _
                            dsi.dsi_mto, _
                            dsi.id_P_0011, _
                            dsi.ope_cls.opn_cls.id_P_0023, _
                            Moneda_ope = dsi.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                            d.doc_sdo_cli, _
                            dsi.id_ope, _
                            d.doc_sdo_ddr, _
                            d.id_doc, _
                            d.id_cco

                            For Each px In doc1
                                col.Add(px)
                            Next

                        End If

                    Catch ex As Exception
                        val = 1
                    End Try

                Next

                Return col

            End If



        End If
    End Function

#End Region

#Region "Consulta Cartera Vigente/Morosa"

    Public Function CobranzaDevuelve(ByVal TipoConsulta As Int16, Optional ByVal NroCbz As String = "") As cco_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve cobranzas
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 09/02/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Cob As cco_cls

            Select Case TipoConsulta
                Case 1
                    Cob = From C In Data.cco_cls Select C
                Case 2
                    Cob = (From C In Data.cco_cls Where C.cco_num = NroCbz Select C).First
                Case 3
                    Cob = From C In Data.cco_cls Where C.cco_pri < 0 Select C
            End Select

            Return Cob

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "VERIFICACIONES"

    Public Function VerificacionPorDoctosDevuelve(ByVal id_doc As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve estado y observacion de la verificacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 04/08/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim DoctosVerificados = From V In Data.dvf_cls _
                                    Join D In Data.doc_cls On V.id_dsi Equals D.id_dsi _
                                    Where D.id_doc = id_doc _
                                    Select EstadoVeri = D.dsi_cls.P_0040_cls.pnu_des, _
                                           V.dvf_obs, V.dvf_obs_001

            For Each DVF In DoctosVerificados
                Coll.Add(DVF)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function VerificacionTodosDoctosDevuelve(ByVal NroOpe As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: devuelve todos los documentos verificados de una operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 09/09/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim DoctosVerificados = From D In Data.dvf_cls Where D.dsi_cls.ope_cls.id_ope = NroOpe

            For Each DVF In DoctosVerificados
                Coll.Add(DVF)
            Next

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function VerificaDeudorDevuelve(ByVal Rut As String) As deu_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve un deudor por rut
        'Creado por= Yonathan Cabezas V.
        'Fecha Creacion: 12/01/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Dim Data As New DataClsFactoringDataContext

        Try
            Dim Deudor As deu_cls = (From Deu In Data.deu_cls _
                          Where Deu.deu_ide = Format(CLng(Rut), Var.FMT_RUT) _
                          Select Deu).First

            Return Deudor

        Catch ex As Exception

            Return Nothing

        End Try
    End Function

    Public Function DiasPagoDevuelve(ByVal RutCli As String, ByVal RutDeu As String) As dpa_cls
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los días de pago asociados al deudor
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 09/02/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim Data As New DataClsFactoringDataContext

            Dim DiasPago As dpa_cls = (From DP In Data.dpa_cls _
                                       Where DP.deu_ide = Format(CLng(RutDeu), Var.FMT_RUT) _
                                       Select DP).First

            Return DiasPago

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function DocumentoAVerificar_Devuelve(ByVal RutCli As String, ByVal RutDeu As String, ByVal MtoDsd As Double, ByVal MtoHta As Double, _
                                        ByVal FecDsd As Date, ByVal FecHta As Date, ByVal NroOpe1 As Integer, ByVal NroOpe2 As Integer, _
                                        ByVal NroDoc1 As Integer, ByVal NroDoc2 As Integer, ByVal EstVeri1 As Integer, ByVal EstVeri2 As Integer, _
                                        ByVal TipDoc1 As Integer, ByVal TipDoc2 As Integer, ByVal TipMon As Integer, _
                                        ByVal LlenaGrilla As Boolean, Optional ByVal GV As GridView = Nothing) As Collection
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim CollDoctos As New Collection

            Dim Doctos = From d In Data.dsi_cls _
                         Where d.ope_cls.opn_cls.eva_cls.cli_idc = Format(CLng(RutCli), Var.FMT_RUT) _
                         And d.deu_ide = Format(CLng(RutDeu), Var.FMT_RUT) _
                         And d.dsi_flj = "N" _
                         And (d.dsi_mto >= MtoDsd And d.dsi_mto <= MtoHta) _
                         And (d.id_ope >= NroOpe1 And d.id_ope <= NroOpe2) _
                         And (d.dsi_num >= NroDoc1 And d.dsi_num <= NroDoc2) _
                         And (d.ope_cls.opn_cls.opn_fec >= Format(CDate(FecDsd), "yyyy/MM/dd") And d.ope_cls.opn_cls.opn_fec <= Format(CDate(FecHta), "yyyy/MM/dd")) _
                         And (d.ope_cls.opn_cls.id_P_0031 >= TipDoc1 And d.ope_cls.opn_cls.id_P_0031 <= TipDoc2) _
                         And (d.ope_cls.id_P_0030 = 1 Or d.ope_cls.id_P_0030 = 2) _
                         And d.ope_cls.opn_cls.id_P_0023 = TipMon _
                         Group By ope = d.id_ope, _
                         NroDoc = d.dsi_num, _
                         deu = d.deu_ide, _
                         TipoDoc = d.ope_cls.opn_cls.id_P_0031, _
                         DesTipoDoc = d.ope_cls.opn_cls.P_0031_cls.pnu_des, _
                         FecVto = d.dsi_fev, _
                         TipoVer = d.id_P_0040, _
                         DesTipoVer = d.P_0040_cls.pnu_des, _
                         TipoMon = d.ope_cls.opn_cls.id_P_0023, _
                         DesTipoMon = d.ope_cls.opn_cls.P_0023_cls.pnu_des, _
                         dsi_num = d.dsi_num Into monto = Sum(d.dsi_mto) _
                         Select ope, NroDoc, deu, monto, FecVto, _
                                TipoDoc, DesTipoDoc, _
                                TipoVer, DesTipoVer, _
                                TipoMon, DesTipoMon, dsi_num

            If LlenaGrilla Then
                GV.DataSource = Doctos
                GV.DataBind()
            End If

            For Each D In Doctos
                CollDoctos.Add(D)
            Next

            Return CollDoctos

        Catch e As Exception
            Return Nothing
        End Try
    End Function

    Public Function DeudorCliente_DoctoLista(ByVal GV As GridView, ByVal RutCli As String, ByVal MtoDsd As Double, ByVal MtoHta As Double, _
                                      ByVal FecDsd As Date, ByVal FecHta As Date, ByVal NroOpe1 As Integer, ByVal NroOpe2 As Integer, _
                                      ByVal NroDoc1 As Integer, ByVal NroDoc2 As Integer, ByVal EstVeri1 As Integer, ByVal EstVeri2 As Integer, _
                                      ByVal TipDoc1 As Integer, ByVal TipDoc2 As Integer, ByVal TipMon As Integer, ByVal NroPagina As Integer) As Boolean
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim formato As String

            If TipMon = 3 Or TipMon = 4 Then
                formato = Fmt.FCMCD
            ElseIf TipMon = 2 Then
                formato = Fmt.FCMCD4
            ElseIf TipMon = 1 Then
                formato = Fmt.FCMSD
            End If

            'Format(CDate(fecha_venc), "yyyy/MM/dd")

            'd.ope_cls.id_P_0030 = 3 'jlagos 12-06-2012

            Dim Doctos = (From d In Data.dsi_cls _
                                               Where d.ope_cls.opn_cls.eva_cls.cli_idc = RutCli _
                                               And (d.dsi_mto >= MtoDsd And d.dsi_mto <= MtoHta) _
                                               And (d.id_ope >= NroOpe1 And d.id_ope <= NroOpe2) _
                                               And (d.dsi_num >= NroDoc1 And d.dsi_num <= NroDoc2) _
                                               And d.dsi_flj = "N" _
                                               And (d.ope_cls.opn_cls.opn_fec >= Format(CDate(FecDsd), "yyyy/MM/dd") And d.ope_cls.opn_cls.opn_fec <= Format(CDate(FecHta), "yyyy/MM/dd" & " 23:59:59")) _
                                               And (d.ope_cls.opn_cls.id_P_0031 >= TipDoc1 And d.ope_cls.opn_cls.id_P_0031 <= TipDoc2) _
                                               And (d.ope_cls.id_P_0030 = 1 Or d.ope_cls.id_P_0030 = 2) _
                                               And d.ope_cls.opn_cls.id_P_0023 = TipMon _
                                               Order By d.deu_ide _
                                               Select New With {.RutDeu = d.deu_ide, _
                                                                .DivDeu = d.deu_cls.deu_dig_ito, _
                                                                .NomDeu = IIf(d.deu_cls.id_P_0044 = 1, d.deu_cls.deu_rso & " " & d.deu_cls.deu_ape_ptn & " " & d.deu_cls.deu_ape_mtn, d.deu_cls.deu_rso), _
                                                                .DsiNum = d.dsi_num, _
                                                                .Monto = d.dsi_mto, _
                                                                .EstVer = d.id_P_0040, _
                                                                .Factor = d.ope_cls.ope_fac_cam, _
                                                                .Suma = "", _
                                                                .Contador = 0}) '.Skip(NroPagina).Take(10)


            Dim col_grilla As New Collection
            Dim col As New Collection
            Dim Rut As String = ""
            Dim RazSoc As String = ""
            Dim PrimeraVez As Boolean = True
            Dim Contador As Integer
            Dim Sumatoria As Double


            For Each p In Doctos
                col.Add(p)
            Next

            For I = 1 To col.Count

                Dim estadover As Integer

                If Not IsNothing(col.Item(I).EstVer) Then
                    estadover = col.Item(I).EstVer
                Else
                    estadover = 0
                End If

                If estadover >= EstVeri1 And estadover <= EstVeri2 Then

                    Rut = col.Item(I).RutDeu
                    Contador = 0
                    Sumatoria = 0

                    While Rut = col.Item(I).RutDeu

                        Contador += 1
                        Sumatoria += col.Item(I).Monto ' * col.Item(I).Factor

                        I += 1
                        If I > col.Count Then
                            Exit While
                        End If
                        'Rut = col.Item(I).RutDeu

                    End While

                    I -= 1

                    col.Item(I).Contador = Contador
                    col.Item(I).Suma = Format(Sumatoria, formato)
                    col.Item(I).RutDeu = Format(CLng(col.Item(I).RutDeu), Fmt.FCMSD) & "-" & col.Item(I).DivDeu

                    col_grilla.Add(col.Item(I))

                End If

            Next

            GV.DataSource = col_grilla
            GV.DataBind()

            Return True


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DocumentosDvfDevuelve(ByVal Rut_Cli As String, ByVal Rut_Deu_Desde As String, ByVal Rut_Deu_Hasta As String, _
                                          ByVal Fec_Ini As String, ByVal Fec_Ter As String, _
                                          ByVal Tip_Doc1 As Integer, ByVal Tip_Doc2 As Integer, _
                                          ByVal LlenaGrid As Boolean, Optional ByVal GV As GridView = Nothing) As Collection
        '*********************************************************************************************************************************
        'Descripcion: Devuelve los Documentos ingersados en verificación ademas del contacto asociado al deudor
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 04/02/2009
        'Quien Modifica              Fecha              Descripcion
        'A. Saldivar                 31/01/2011         Se agrega paginacion
        '*********************************************************************************************************************************

        Try
            Dim CollDvf As New Collection
            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession

            Dim Dvf = (From d In Data.dvf_cls _
                Where d.cli_idc = Format(CLng(Rut_Cli), Var.FMT_RUT) _
                 And (d.deu_ide >= Format(CLng(Rut_Deu_Desde), Var.FMT_RUT) And d.deu_ide <= Format(CLng(Rut_Deu_Hasta), Var.FMT_RUT)) _
                 And (d.dvf_fec_vfc >= Fec_Ini And d.dvf_fec_vfc <= Fec_Ter) _
                 And (d.id_P_0031 >= Tip_Doc1 And d.id_P_0031 <= Tip_Doc2) _
                Select New With {d.id_dvf, _
                                 d.cli_idc, _
                                 d.deu_ide, _
                                 .deu_rso = IIf(d.deu_cls.id_P_0044 = 1, d.deu_cls.deu_rso & " " & d.deu_cls.deu_ape_ptn & " " & d.deu_cls.deu_ape_mtn, d.deu_cls.deu_rso), _
                                 d.dvf_num, _
                                 d.dvf_mto, _
                                 d.dvf_fev, _
                                 d.dvf_fev_rea, _
                                 d.id_P_0031, _
                                 .pnu_des = d.P_0031_cls.pnu_atr_003, _
                                 d.id_P_0040, _
                                 .EstadoVer = d.P_0040_cls.pnu_des, _
                                 d.dvf_obs, _
                                 d.dvf_fec_lle, _
                                 d.dvf_hor_lle, _
                                 d.dvf_fec_vfc, _
                                 d.dvf_hor_vfc, _
                                 d.dvf_fec_pag, _
                                 d.dvf_fec_pri_gsn, _
                                 d.id_eje_dvf, _
                                 d.dvf_fec_ing, _
                                 d.dvf_obs_001, _
                                 d.cnc_cls.cnc_nom, _
                                 d.cnc_cls.cnc_car, _
                                 d.cnc_cls.cnc_tel, _
                                 d.dvf_zon_rgo_rec, _
                                 .Moneda = d.P_0023_cls.pnu_des, _
                                 d.id_P_0023}).Skip(sesion.NroPaginacion_Docto).Take(5)

            If LlenaGrid Then

                GV.DataSource = Dvf
                GV.DataBind()

                For i = 0 To GV.Rows.Count - 1
                    GV.Rows(i).Cells(1).Text = RG.FormatoMiles(CLng(GV.Rows(i).Cells(1).Text)) & "-" & RG.Vrut(GV.Rows(i).Cells(1).Text)
                    GV.Rows(i).Cells(4).Text = RG.FormatoMiles(Val(GV.Rows(i).Cells(4).Text))
                Next

            End If

            For Each I In Dvf
                CollDvf.Add(I)
            Next

            Return CollDvf

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


#End Region

#End Region

#Region "Actualizaciones"

    Public Function Gestion_guarda(ByVal gsn As gsn_cls) As Integer

        Dim Data As New DataClsFactoringDataContext

        Try

            Dim gsn_con = From g In Data.gsn_cls Where g.id_doc = gsn.id_doc And g.gsn_fec = gsn.gsn_fec

            For Each p In gsn_con

                Return p.id_gsn
            Next


            Data.gsn_cls.InsertOnSubmit(gsn)
            Data.SubmitChanges()

            Return gsn.id_gsn

        Catch ex As Exception
            Return 0
        End Try
    End Function

#Region "VERIFICACION"

    Public Function DocumentoDvfInserta(ByVal DoctoVeri As dvf_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Inserta un Documento en verificación
        'Creado por= Yonathan Cabezas V.
        'Fecha Creacion: 14/01/2009
        'Quien Modifica              Fecha              Descripcion
        'Jlagos                     18-06-2012          -se agrega fecha de vencimiento real (calculo)
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext


            'Consulta si el Docto. esta verificado
            Dim Dvf = (From D In Data.dvf_cls _
                       Where D.cli_idc = Format(CLng(DoctoVeri.cli_idc), Var.FMT_RUT) _
                         And D.deu_ide = Format(CLng(DoctoVeri.deu_ide), Var.FMT_RUT) _
                         And D.dvf_num = DoctoVeri.dvf_num _
                         And D.id_P_0031 = DoctoVeri.id_P_0031).FirstOrDefault

            If IsNothing(Dvf) Then

                Data.dvf_cls.InsertOnSubmit(DoctoVeri)

            Else

                Dvf.cli_idc = Format(CLng(DoctoVeri.cli_idc), Var.FMT_RUT)
                Dvf.deu_ide = Format(CLng(DoctoVeri.deu_ide), Var.FMT_RUT)
                Dvf.dvf_num = DoctoVeri.dvf_num
                Dvf.id_P_0031 = DoctoVeri.id_P_0031
                Dvf.dvf_mto = DoctoVeri.dvf_mto
                Dvf.dvf_fev = DoctoVeri.dvf_fev
                Dvf.id_P_0023 = DoctoVeri.id_P_0023
                Dvf.dvf_fev_rea = DoctoVeri.dvf_fev_rea

            End If

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DsiUpdate(ByVal Rut_Cli As String, ByVal Rut_Deu As String, ByVal Num_Doc As String, _
                              ByVal EstVeri As Int16, ByVal Tip_Doc As Int16) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Modifica el estado de verificación de un documento
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 14/01/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Dsi = (From D In Data.dsi_cls Where D.ope_cls.opn_cls.eva_cls.cli_idc = Rut_Cli _
                      And D.deu_ide = Rut_Deu _
                      And D.dsi_num = Num_Doc _
                      And D.ope_cls.opn_cls.id_P_0031 = Tip_Doc).FirstOrDefault

            If Not IsNothing(Dsi) Then
                Dsi.id_P_0040 = EstVeri
            End If

            Data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DocumentosDvfDelete(ByVal Iddvf As Integer) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Elimina un documento de verificación
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 04/02/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim DoctoDvf As dvf_cls = (From V In Data.dvf_cls Where V.dvf_num = Iddvf).First

            Data.dvf_cls.DeleteOnSubmit(DoctoDvf)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

    Public Function DocumentoDvfUpdate(ByVal Dvf As dvf_cls, Optional ByVal id As Integer = 0) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Modifica un documento de verificación
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 06/02/2008
        'Quien Modifica:              Fecha:              Descripcion:
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            If id = 0 Then
                id = Dvf.id_dvf
            End If


            'Dim DoctoDvf As dvf_cls = (From V In Data.dvf_cls Where V.id_dvf = Dvf.id_dvf).First
            Dim DoctoDvf As dvf_cls = (From V In Data.dvf_cls Where V.id_dvf = id).First

            With DoctoDvf
                .id_P_0040 = Dvf.id_P_0040
                .dvf_fev = Dvf.dvf_fev
                .dvf_fev_rea = Dvf.dvf_fev_rea
                .dvf_obs = Dvf.dvf_obs
                .dvf_fec_lle = .dvf_fec_lle
                .dvf_hor_lle = Dvf.dvf_hor_lle
                .dvf_fec_vfc = Dvf.dvf_fec_vfc
                .dvf_hor_vfc = Dvf.dvf_hor_vfc
                .dvf_fec_pag = Dvf.dvf_fec_pag
                .dvf_fec_pri_gsn = Dvf.dvf_fec_pri_gsn
                .dvf_pro = "V"
                .dvf_fec_ing = Dvf.dvf_fec_ing
                .dvf_obs_001 = Dvf.dvf_obs_001
                .dvf_obs_cob = Dvf.dvf_obs_cob
                .dvf_zon_rgo_rec = Dvf.dvf_zon_rgo_rec
                .cli_idc = Dvf.cli_idc
                .deu_ide = Dvf.deu_ide
                .dvf_fev = Dvf.dvf_fev
                .dvf_fev_rea = Dvf.dvf_fev_rea
                .dvf_mto = Dvf.dvf_mto
                .id_P_0023 = Dvf.id_P_0023
                .id_P_0031 = Dvf.id_P_0031
                .id_P_0040 = Dvf.id_P_0040
            End With

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ObservacionDeudorUpdate(ByVal RutDeu As String, ByVal FechaObs As DateTime, ByVal Obs As String) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Modifica observación de deudor
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 10/02/2009
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Deu As deu_cls = (From D In Data.deu_cls Where D.deu_ide = Format(CLng(RutDeu), Var.FMT_RUT) Select D).First

            If Not IsNothing(Deu) Then
                Deu.deu_fec_obs = FechaObs
                Deu.deu_obs_gsn = Obs
            End If

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DocumentoAVerificarUpdate(ByVal DoctoDvf As dvf_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Modifica documento a verificar
        'Creado por: Yonathan Cabezas V.
        'Fecha Creacion: 18/02/2009
        'Quien Modifica              Fecha              Descripcion
        '   JLagos                  16-06-2009         - !!!! No esta guardando, nose probo !!!!
        '                                                Si el documento ya tiene una verificacion, esta se cambia por los nuevos parametros
        '                                                y en el caso que no existe, se crea una nueva verificacion   
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Dvf = From D In Data.dvf_cls _
                     Where D.deu_ide = Format(CLng(DoctoDvf.deu_ide), Var.FMT_RUT) _
                       And D.cli_idc = Format(CLng(DoctoDvf.cli_idc), Var.FMT_RUT) _
                       And D.dvf_num = DoctoDvf.dvf_num _
                       And D.id_P_0031 = DoctoDvf.id_P_0031

            If Dvf.Count > 0 Then

                For Each D In Dvf
                    D.id_cnc = DoctoDvf.id_cnc
                    D.id_P_0040 = DoctoDvf.id_P_0040
                    D.dvf_fec_lle = DoctoDvf.dvf_fec_lle
                    D.dvf_hor_lle = DoctoDvf.dvf_hor_lle 'DoctoDvf.dvf_fec_lle & " " & DoctoDvf.dvf_hor_lle
                    D.dvf_mto = DoctoDvf.dvf_mto
                    D.dvf_fec_pag = DoctoDvf.dvf_fec_pag
                    D.dvf_fec_pri_gsn = DoctoDvf.dvf_fec_pri_gsn

                    D.dvf_fec_vfc = DoctoDvf.dvf_fec_vfc
                    D.dvf_hor_vfc = DoctoDvf.dvf_hor_vfc 'DoctoDvf.dvf_fec_vfc & " " & DoctoDvf.dvf_hor_vfc
                    D.dvf_fev = DoctoDvf.dvf_fev
                    D.dvf_obs = DoctoDvf.dvf_obs
                Next

            Else

                If DocumentoDvfInserta(DoctoDvf) _
                           And DsiUpdate(Format(DoctoDvf.cli_idc, Var.FMT_RUT), _
                                         Format(DoctoDvf.deu_ide, Var.FMT_RUT), _
                                         DoctoDvf.dvf_num, _
                                         DoctoDvf.id_P_0040, _
                                         DoctoDvf.id_P_0031) Then
                End If

            End If



            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "PASAR A MODULOS CENTRALES - ACTUALIZACION"

    Public Function dor_inserta(ByVal dor As dor_cls) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Guarda documentos a gestionar
        'Creado por= Pablo Gatica S.                                                                                                                       
        'Fecha Creacion: 21/04/2009                                                                                                                     
        'Quien Modifica              Fecha              Descripcion          



        '*********************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext

        Try
            Dim dorc As dor_cls = (From d In data.dor_cls Where d.id_doc = dor.id_doc Select d).First

            dorc.dor_est = "N"
            data.SubmitChanges()

            Return True
        Catch ex As Exception

        End Try

        data.dor_cls.InsertOnSubmit(dor)
        data.SubmitChanges()

        Return True

    End Function
    Public Function valida_si_direccion_existe(ByVal id_comuna As Integer, ByVal dir_cbz As String) As Object
        '**************************************************************************************************************************************************
        'Descripcion: valida si la direccion existe antes de guardar en la tabla gsn
        'Creado por Cristian Arce Salgado.
        'Fecha Creacion: 15/09/2011
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try

            Dim Data As New DataClsFactoringDataContext

            Dim direccion = (From c In Data.gsn_cls Where c.ddi_cls.id_cmn = id_comuna And c.gsn_dir_cbz = dir_cbz Select c).First

            Return direccion

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GuardaGestion(ByVal gsn_ingreso As gsn_cls, ByVal ddi As ddi_cls, ByVal cbx_recaudar As Boolean, ByVal suc_rec As Integer, ByVal suc_cob As Integer, ByVal ges_pen As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: 
        'Creado por 
        'Fecha Creacion: 
        'Quien Modifica              Fecha              Descripcion
        'Cristian Arce Salgado	   20/09/2011		se agrega que si la gsn_ingreso.id_ddi no es vacia no se le de un nuevo valor de id_ddi
        '**************************************************************************************************************************************************

        Try

            GuardaGestion = False

            Dim Data As New DataClsFactoringDataContext
            Dim StrQuery As String = ""
            Dim SQL As New FuncionesGenerales.SqlQuery

            estadoconsulta = 1
            descripcionconsulta = ""

            '------------------------------------------------------------------------------------------------------------------------------
            'TRANSACCION
            '------------------------------------------------------------------------------------------------------------------------------
            Using ts = New TransactionScope

                '------------------------------------------------------------------------------------------------------------------------------
                'Guarda Direccion Deudor, si tiene fecha de pago
                '------------------------------------------------------------------------------------------------------------------------------

                If Not IsNothing(ddi.deu_ide) Then

                    Data.ddi_cls.InsertOnSubmit(ddi)
                    Data.SubmitChanges()
                    If IsNothing(gsn_ingreso.id_ddi) Then
                        gsn_ingreso.id_ddi = ddi.id_ddi
                    End If
                End If

                '------------------------------------------------------------------------------------------------------------------------------
                'Guarda Gestión 
                '------------------------------------------------------------------------------------------------------------------------------

                Data.gsn_cls.InsertOnSubmit(gsn_ingreso)
                Data.SubmitChanges()

                estadoconsulta = 1
                descripcionconsulta = "Guarda Gestion"

                '------------------------------------------------------------------------------------------------------------------------------
                'Si Gestion obtuvo Fecha de Pago Se ingresa Hoja de Recaudación
                '------------------------------------------------------------------------------------------------------------------------------
                Try


                    If Not IsNothing(gsn_ingreso.gsn_fec_pag) Then

                        If cbx_recaudar = True Then


                            '--------------------------------------------------------------------------------------------------------------------------
                            'Buscar Ejecutivo (Recaudador) según Zona
                            '--------------------------------------------------------------------------------------------------------------------------
                            Dim CodigoRecaudador As String = "0"

                            If Val(gsn_ingreso.ddi_cls.cmn_cls.id_zon) <> 0 Then

                                Dim Temporal_eje = From nez In Data.nez_cls Where nez.id_zon = gsn_ingreso.ddi_cls.cmn_cls.id_zon _
                                                   Select nez.id_eje

                                For Each p In Temporal_eje
                                    CodigoRecaudador = p.ToString()
                                Next

                            Else
                                Dim Temporal_eje = From EJE In Data.eje_cls _
                                                   Join Nes In Data.nes_cls On Nes.id_eje Equals EJE.id_eje _
                                            Where EJE.id_P_0045 = 14 _
                                               And Nes.id_suc = suc_rec _
                                            Select Codigo = EJE.id_eje, Descripcion = EJE.eje_nom

                                For Each p In Temporal_eje
                                    CodigoRecaudador = p.Codigo
                                Next

                            End If

                            'Si no existe Recaudador para sucursal y Zona se asigna cualquier recaudador con AUXILIAR
                            If CodigoRecaudador = "0" Or CodigoRecaudador = Nothing Then
                                Dim Temporal_eje2 = From nef In Data.nef_cls _
                                                    Join eje In Data.eje_cls On nef.id_eje Equals eje.id_eje _
                                            Where nef.id_P0045 = 14 _
                                            Select Codigo = eje.id_eje, Descripcion = eje.eje_nom

                                For Each p In Temporal_eje2
                                    CodigoRecaudador = p.Codigo
                                Next
                            End If

                            If CodigoRecaudador = "0" Or CodigoRecaudador = Nothing Then
                                estadoconsulta = 2
                                descripcionconsulta = "No existen recaudadores para la localidad/barrio asignada."
                                Return False
                            End If

                            '--------------------------------------------------------------------------------------------------------------------------
                            'Grabar Hoja de Ruta (Insertar si no existe)
                            '--------------------------------------------------------------------------------------------------------------------------
                            Dim AUX_AM_PM As String
                            Dim CodigoHojaRecaudacion As Int32
                            AUX_AM_PM = IIf(CDate(Format(gsn_ingreso.gsn_hor_pag_dde, "HH:mm:ss")) > CDate("13:30:00"), "P", "A")

                            Dim Temporal_hre = From HRE In Data.hre_cls _
                                Where HRE.id_eje = CodigoRecaudador _
                                And HRE.hre_fec = gsn_ingreso.gsn_fec_pag _
                                And HRE.hre_mna_tde = AUX_AM_PM _
                                Select idhre = HRE.id_hre

                            For Each p In Temporal_hre
                                CodigoHojaRecaudacion = p
                            Next

                            If CodigoHojaRecaudacion = Nothing Or CodigoHojaRecaudacion = 0 Then
                                Dim hre_ingreso As New hre_cls

                                hre_ingreso.id_eje = CodigoRecaudador
                                hre_ingreso.hre_fec = gsn_ingreso.gsn_fec_pag
                                hre_ingreso.hre_mna_tde = AUX_AM_PM
                                hre_ingreso.hre_fec_ges = Now.Date
                                hre_ingreso.hre_hor_ges = Now.Date

                                Data.hre_cls.InsertOnSubmit(hre_ingreso)
                                Data.SubmitChanges()

                                CodigoHojaRecaudacion = hre_ingreso.id_hre

                            End If

                            '--------------------------------------------------------------------------------------------------------------------------
                            'Grabar Documentos a Recaudar
                            '--------------------------------------------------------------------------------------------------------------------------
                            Dim CodigoDocumentoRecaudar As Int32
                            Dim Temporal_drc = From DRC In Data.drc_cls _
                                               Where DRC.gsn_cls.doc_cls.id_doc = gsn_ingreso.id_doc _
                                               And DRC.id_hre = CodigoHojaRecaudacion _
                                               And DRC.drc_fec_pag = gsn_ingreso.gsn_fec_pag _
                            Select DRC

                            For Each p In Temporal_drc
                                CodigoDocumentoRecaudar = p.id_drc
                                Exit For
                            Next

                            If CodigoDocumentoRecaudar = Nothing Or CodigoDocumentoRecaudar = 0 Then
                                Dim drc_ingreso As New drc_cls

                                drc_ingreso.id_hre = CodigoHojaRecaudacion
                                drc_ingreso.id_gsn = gsn_ingreso.id_gsn
                                drc_ingreso.id_P_0103 = Nothing
                                drc_ingreso.drc_est = "N"
                                drc_ingreso.drc_fec_pag = gsn_ingreso.gsn_fec_pag
                                drc_ingreso.drc_ntf_rec = Nothing
                                drc_ingreso.drc_pen = Nothing
                                drc_ingreso.drc_fec_pen = Nothing

                                Data.drc_cls.InsertOnSubmit(drc_ingreso)
                                Data.SubmitChanges()

                            Else

                                Dim drc_modifica As drc_cls = (From DRC In Data.drc_cls _
                                        Where DRC.id_drc = CodigoDocumentoRecaudar _
                                        Select DRC).First()

                                drc_modifica.id_hre = CodigoHojaRecaudacion
                                drc_modifica.id_gsn = gsn_ingreso.id_gsn
                                drc_modifica.id_P_0103 = Nothing
                                drc_modifica.drc_est = "N"
                                drc_modifica.drc_fec_pag = gsn_ingreso.gsn_fec_pag
                                drc_modifica.drc_ntf_rec = Nothing
                                drc_modifica.drc_pen = Nothing
                                drc_modifica.drc_fec_pen = Nothing

                                Data.SubmitChanges()
                            End If

                        End If

                    End If

                Catch ex As Exception
                    estadoconsulta = 2
                    descripcionconsulta = "Error: " & ex.Message
                    Return False
                End Try

                ts.Complete()

            End Using


            '--------------------------------------------------------------------------------------------------------------------------
            'Grabar Modificacion de Documentos (DOC/DSI)
            '--------------------------------------------------------------------------------------------------------------------------
            StrQuery = "Update DOC set id_suc_cbz = " & suc_cob & ", id_suc_rcd = " & suc_rec & ", id_cco = " & gsn_ingreso.id_cco & ", doc_fec_cco = '" & RG.FUNFechaJul(gsn_ingreso.gsn_fec) & "' " & _
                       "Where id_doc = " & gsn_ingreso.id_doc

            SQL.ExecuteNonQuery(StrQuery)

            'Dim doc = (From d In Data.doc_cls _
            '                               Where d.id_doc = 78293 _
            '                               Select d).ToList()(0)

            'Dim doc_modifica = From d In Data.doc_cls _
            '                               Where d.id_doc = gsn_ingreso.id_doc _
            '                               Select d

            'For Each d In doc_modifica

            '    d.id_suc_cbz = suc_cob
            '    d.id_suc_rcd = suc_rec

            '    If d.id_cco <> gsn_ingreso.id_cco Then
            '        d.id_cco = gsn_ingreso.id_cco
            '        d.doc_fec_cco = gsn_ingreso.gsn_fec
            '    End If

            'Next

            '--------------------------------------------------------------------------------------------------------------------------
            'Grabar Estados de DOR
            '--------------------------------------------------------------------------------------------------------------------------
            If ges_pen = 1 Then
                StrQuery = " Update DOR set dor_hor_prx = '" & RG.FUNFechaJul(gsn_ingreso.gsn_hor_prx) & " " & CDate(gsn_ingreso.gsn_hor_prx.ToString).ToLongTimeString & "', dor_est = 'S' Where id_doc =" & gsn_ingreso.id_doc
                SQL.ExecuteNonQuery(StrQuery)
            End If


            'Try

            '    Dim dor_modifica As dor_cls = (From DOR In Data.dor_cls _
            '                                   Where DOR.id_doc = gsn_ingreso.id_doc _
            '                                   Select DOR).First

            '    If ges_pen = 1 Then
            '        dor_modifica.dor_hor_prx = gsn_ingreso.gsn_hor_prx
            '    Else
            '        dor_modifica.dor_est = "S"
            '    End If

            '    Data.SubmitChanges()

            'Catch ex As Exception
            '    estadoconsulta = 2
            '    descripcionconsulta = "Error: " & ex.Message
            '    Return False
            'End Try


            estadoconsulta = 1
            descripcionconsulta = "Proceso Exitoso"

            Return True

        Catch ex As Exception

            estadoconsulta = 2
            descripcionconsulta = "Error: " & ex.Message

            Return False

        End Try

    End Function





#End Region

#End Region

#Region "RadicacionFacturas"

    Public Function ExistenDoctosRad(ByVal fec As DateTime) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Verifica si existen doctos a radicar
        'Creado por: Sebastian Henriquez.
        'Fecha Creacion: 08/08/2012
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext

            Dim Fact = data.sp_Reporte_Radicar_Facturas(fec)

            If Fact.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

End Class
