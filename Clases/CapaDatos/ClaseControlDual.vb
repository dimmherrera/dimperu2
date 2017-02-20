Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Text
Imports CapaDatos

Public Class ClaseControlDual

    Dim RG As New FuncionesGenerales.FComunes
    Dim Fmt As New FuncionesGenerales.ClsLocateInfo
    Dim Var As New FuncionesGenerales.Variables

    Private pRutDeudor As String
    Public Property RutDeudor() As String
        Get
            Return pRutDeudor
        End Get
        Set(ByVal value As String)
            pRutDeudor = value
        End Set
    End Property

#Region "Consultas"

#Region "Clasificacion y Firmas"


    Public Function carga_corr_Ccf() As Integer
        Dim i As Int16
        Dim Clasificacion = New ccf_cls()
        Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

        Dim count = (From c In Data.ccf_cls Select c).Count
        If count = 0 Then
            i = 1
            Return i
        Else
            Dim cf = (From d In Data.ccf_cls Select d.id_ccf).Max

            i = cf + 1

            Return i

        End If

    End Function

    Public Function carga_rangos_clasificacion(ByVal cfc As Int32) As Collection

        '------------------------------------------------------------------------------
        'Descripcion: Carga rangos con el fin de validar que no se crucen
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        'Jlagos                     26/06/2009         -Se quita codigo inutil
        '------------------------------------------------------------------------------

        Try

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim col As New Collection

            Dim c = From cf In Data.cfc_cls Select cf Where cf.ccf_cls.ccf_est = "A"

            For Each cf In c
                col.Add(cf)
            Next

            Return col

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Valida_Asociaciones_por_clasificacion(ByVal id_ccf As Integer) As Boolean
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try



            Dim col As New Collection

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim val = From p In Data.nnc_cls Where p.id_ccf = id_ccf


            If val.Count > 0 Then
                Return True
            Else
                Return False

            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CabeceraClasificacion_datos_devuelve(ByVal ID_CCF As Integer) As Collection
        'Descripcion: Devuelve Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            Dim Clasificacion = From c In Data.ccf_cls Where c.id_ccf = ID_CCF _
                    Select Codigo = c.id_ccf, Descripcion = c.ccf_des, c.ccf_est, c.ccf_tip_apb

            If Clasificacion.Count <> 0 Then

                For Each P In Clasificacion

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

    Public Function DevuelveDetalleClasificacion(ByVal id As Integer) As Collection
        'Descripcion: Devuelve Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim cfc As cfc_cls


            Dim Clasificacion = From c In Data.cfc_cls Where c.id_ccf = id _
                                Order By c.id_P_0069 _
                    Select c.id_ccf, c.id_cfc, c.cfc_dde, c.cfc_hta, c.cfc_des, cfc_obs = c.P_0069_cls.pnu_des, ID_P_0069 = c.id_P_0069

            If Clasificacion.Count <> 0 Then

                For Each P In Clasificacion

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

    Public Function carga_perfiles_por_clasificacion(ByVal cf As Integer, ByVal prio As Integer) As Collection
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try


            Dim firma As frm_cls
            Dim col As New Collection

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim pfl = From p In Data.frm_cls Where p.id_ccf = cf And p.id_p_005 = prio And p.frm_est = "A" _
                      Select p

            For Each p In pfl

                firma = New frm_cls

                firma.id_p_0045 = p.id_p_0045

                col.Add(firma)

            Next

            If col.Count > 0 Then

                Return col
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DevuelveCabeceraClasificacion(Optional ByVal LlenaDrop As Boolean = False, Optional ByVal dp As DropDownList = Nothing) As Collection
        'Descripcion: Devuelve Paridades Mensuales segun tipo de Moneda
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 04/08/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim ccf As ccf_cls

            'Where c.ccf_est = "A" _
            Dim Clasificacion = From c In Data.ccf_cls _
                    Select Codigo = c.id_ccf, _
                      Descripcion = c.ccf_des, _
                      Estado = c.ccf_est, _
                      Aprobacion = c.ccf_tip_apb

            If Clasificacion.Count <> 0 Then

                For Each P In Clasificacion

                    ccf = New ccf_cls

                    With ccf
                        .id_ccf = P.Codigo
                        .ccf_des = P.Descripcion
                        .ccf_est = P.Estado
                        .ccf_tip_apb = P.Aprobacion

                    End With

                    Coll.Add(ccf)

                Next



                If LlenaDrop Then

                    Dim RG As New FuncionesGenerales.RutinasWeb
                    RG.Llenar_Drop(Clasificacion, "Codigo", "Descripcion", dp)

                Else

                    Return Coll

                End If



            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Sucursales_carga_por_clasificacion(ByVal cf As Integer) As Collection
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try


            Dim sucr As cxs_cls
            Dim col As New Collection

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim suc = From p In Data.cxs_cls Where p.id_ccf = cf And p.cxs_est = "A" _
                      Select ID_SUC = p.id_suc, _
                             SUC_NOM = p.suc_cls.suc_nom, _
                              p.id_cxs, _
                              p.ccf_cls.ccf_tip_apb




            For Each p In suc
                col.Add(p)
            Next

            If col.Count > 0 Then

                Return col
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Sucursales_Elimina_clasificacion(ByVal cf As Integer) As Boolean

        '---------------------------------------------------------------------------------
        'Descripcion: Elimina sucursales con una clasificacion y sucursales de aprobacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 22/03/2013
        '---------------------------------------------------------------------------------

        Try

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim cxs = From p In Data.cxs_cls Where p.id_ccf = cf

            For Each c In cxs
                Data.sxa_cls.DeleteAllOnSubmit(From p In Data.sxa_cls Where p.id_cxs = c.id_cxs)
            Next

            Data.cxs_cls.DeleteAllOnSubmit(cxs)
            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Sucursales_de_Aprobación_Devuelve(ByVal codigo As Integer) As Collection
        'Descripcion: Devuelve Feriados 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 3/09/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Data As New DataClsFactoringDataContext
            Dim Coll As New Collection
            Dim COLL2 As New Collection
            Dim CXS_Obj As cxs_cls
            Dim OBJ_SXA As New apb_cxs

            Dim cxs = (From cx In Data.cxs_cls Where cx.id_ccf = codigo Select cx.id_cxs, cx.id_suc).Distinct

            If cxs.Count <> 0 Then

                For Each P In cxs

                    CXS_Obj = New cxs_cls

                    With CXS_Obj
                        .id_cxs = P.id_cxs
                    End With

                    Coll.Add(CXS_Obj)

                Next

                For I = 1 To Coll.Count
                    Dim X As Integer

                    X = Coll.Item(I).id_cxs

                    OBJ_SXA = New apb_cxs

                    Dim APB = From APR In Data.sxa_cls Where APR.id_cxs = X And APR.sxa_est = "A" Select COD = APR.id_cxs, ID_SUC = APR.id_suc, SUC1 = APR.suc_cls.suc_nom, SUC2 = APR.cxs_cls.suc_cls.suc_nom


                    For Each APR In APB
                        With OBJ_SXA
                            .id_cxs = APR.COD
                            .id_suc = APR.ID_SUC
                            .nom_suc = APR.SUC2
                            .nom_suc_apb = APR.SUC1


                        End With

                        COLL2.Add(OBJ_SXA)
                    Next


                Next


                Return COLL2

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function carga_corr_sxa() As Integer

        Dim i As Integer
        Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

        Dim count = (From c In Data.cxs_cls Select c).Count

        If count = 0 Then
            i = 1
            Return i
        Else
            Dim sx = (From d In Data.cxs_cls Select d.id_cxs).Max
            i = sx
            Return i
        End If

    End Function

    Public Function ValidaFirmas(ByVal id_ccf As Integer, ByVal id_prioridad As Integer, ByVal Coll_Frm As Collection) As Boolean

        '------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Valida Firma en un clasificacion segun sus niveles de aprobacion
        'Creado por= Jorge Lagos
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '------------------------------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New DataClsFactoringDataContext

            '--------------------------------------------------------------------------------------------------------------------
            'Validamos que exista en un nivel superior al que se va a ingresar, menos cuando sea nivel 1
            '--------------------------------------------------------------------------------------------------------------------
            If id_prioridad > 1 Then

                Dim Niveles_Superior = From F In Data.frm_cls Where F.frm_est = "A" And F.id_ccf = id_ccf And F.id_p_005 = (id_prioridad - 1)

                If Niveles_Superior.Count <= 0 Then
                    Return False
                End If

            End If


            '--------------------------------------------------------------------------------------------------------------------
            'Recorremos la colleccion por cada Perfil
            '--------------------------------------------------------------------------------------------------------------------
            Dim MismosPerfiles As Boolean = False

            For Each Frm As frm_cls In Coll_Frm

                Dim Perfil As Integer = Frm.id_p_0045

                'Buscamos en que niveles se encuentra el perfil para la misma clasificacion
                Dim Firmas = From F In Data.frm_cls Where F.id_ccf = id_ccf And F.id_p_0045 = Frm.id_p_0045 And F.frm_est = "A" Order By F.id_p_005

                If Not IsNothing(Firmas) Then

                    'Si es una firma y si la encuentra en otro nivel de la clasificacion, no de debe permitir agregar
                    If Coll_Frm.Count = 1 And Firmas.Count > 0 Then
                        Return False
                    End If

                    If Firmas.Count > 0 Then
                        MismosPerfiles = True
                    Else
                        MismosPerfiles = False
                    End If
                Else
                    Return False
                End If

                '--------------------------------------------------------------------------------------------------------------------
                'Si no, hay que validar el nivel que esta, sean la misma cantidad de firmas
                '--------------------------------------------------------------------------------------------------------------------

                'Rescatamos los niveles en cuales esta
                Dim Niveles = (From R In Firmas Select R.id_p_005).Distinct

                Dim K_Firmas As Integer = 0

                For Each N In Niveles

                    'Contamos las firmas del nivel
                    K_Firmas = (From P In Data.frm_cls Where P.id_ccf = id_ccf And P.id_p_005 = N And P.frm_est = "A").Count

                    'si la cantidad es mayor a las que se estan ingresando, no se puede agregar nivel
                    If Coll_Frm.Count < K_Firmas Then
                        Return False
                    Else

                        If K_Firmas = 1 Then
                            Return False
                        End If

                    End If

                Next

            Next

            If MismosPerfiles Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception

        End Try

    End Function

    Public Function ValidaFirmasAsociadas(ByVal id_ccf As Integer, ByVal id_prioridad As Integer, ByVal Coll_Frm As Collection) As Boolean

        '------------------------------------------------------------------------------------------------------------------------------
        'Descripcion: Valida Firma en un clasificacion segun sus niveles de aprobacion
        'Creado por= Jorge Lagos
        'Fecha Creacion: 22/04/2009
        'Quien Modifica              Fecha              Descripcion
        '------------------------------------------------------------------------------------------------------------------------------

        Try

            Dim Data As New DataClsFactoringDataContext

            For i = 1 To Coll_Frm.Count
                Dim firma As Integer
                firma = Coll_Frm.Item(i).id_frm
                Dim asoc = From a In Data.apb_cls Where a.id_frm = firma


                If asoc.Count > 0 Then
                    Return False
                End If

            Next





            Return True


        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Módulo de Control DE APROBACIONES"

    Public Sub NegociacionClasificacionDevuelve(ByVal NroNeg As Integer, ByVal GV As GridView)


        '**************************************************************************************************************************************************
        'Descripcion: devuelve las clasificaciones en las cuales cayo la negociacion 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  11/01/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.SesionOperaciones
            Dim coll As New Collection

            Dim neg As opn_cls = (From n In Data.opn_cls Where n.id_opn = NroNeg).First

            Dim NNC = (From N In Data.nnc_cls Where N.id_opn = NroNeg _
                                                        And N.ccf_cls.ccf_est = "A" _
                                                        And N.id_suc = neg.id_suc _
                                                        Select Nro = N.id_nnc, _
                                                               Descripcion = N.ccf_cls.ccf_des, _
                                                               ccf = N.id_ccf).Skip(sesion.NroPaginacion_Claf)


            For Each x In NNC.Take(6)

                coll.Add(x)
            Next
            GV.DataSource = coll
            GV.DataBind()


            'GV.DataSource = NNC
            'GV.DataBind()



        Catch ex As Exception

        End Try

    End Sub

    Public Sub NegociacionFirmasDevuelve(ByVal NroNNC As Integer, ByVal ideje As Integer, ByVal TB As Table)

        '**************************************************************************************************************************************************
        'Descripcion: devuelve las clasificaciones en las cuales cayo la negociacion 
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 28/08/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Aprobaciones = From A In Data.apb_cls Where A.id_nnc = NroNNC And _
                                                            (From n In Data.nes_cls Where n.id_suc = A.nnc_cls.id_suc And n.id_eje = ideje And A.frm_cls.frm_est = "A").Count > 0 _
                                                      Order By A.frm_cls.id_p_005 _
                                                      Select Nro = A.id_apb, _
                                                             Estado = A.apb_est_ado, _
                                                             Perfil = A.frm_cls.P_0045_cls.pnu_des.Trim, _
                                                             Nivel = A.frm_cls.id_p_005, _
                                                             Prioridad = A.frm_cls.P_005_cls.pnu_des.Trim

            Dim Niveles = From N In Data.P_005_cls Order By N.id_P_005

            Dim PriAnt As New Collection
            Dim objrow As TableRow
            Dim objCell As TableCell
            Dim objCellImg As TableCell
            Dim objCellPfl As TableCell
            Dim objImg As Image
            Dim Objtext As Label
            Dim I As Int16 = 0


            For Each N In Niveles

                objrow = New TableRow

                Select Case N.id_P_005
                    Case 1 : objrow.CssClass = "nivel1"
                    Case 2 : objrow.CssClass = "nivel2"
                    Case 3 : objrow.CssClass = "nivel3"
                    Case 4 : objrow.CssClass = "nivel4"
                    Case 5 : objrow.CssClass = "nivel5"
                End Select

                objCell = New TableCell
                Objtext = New Label

                Objtext.Text = N.pnu_des
                Objtext.Width = 70
                Objtext.ForeColor = Drawing.Color.Blue
                Objtext.Font.Bold = True
                objCell.Controls.Add(Objtext)
                objrow.Cells.Add(objCell)

                For Each A In Aprobaciones

                    If N.id_P_005 = A.Nivel Then

                        objCellImg = New TableCell
                        objImg = New Image

                        Select Case Val(A.Estado)
                            Case 1 : objImg.ImageUrl = "../../../imagenes/iconos/verde02.gif"
                            Case 2 : objImg.ImageUrl = "../../../imagenes/iconos/Rojo02.gif"
                            Case Else : objImg.ImageUrl = "../../../imagenes/iconos/Amarillo02.gif"
                        End Select

                        objCellImg.Attributes.Add("onMouseOver", "showObservacion(event,'" & A.Nro & "');")
                        objCellImg.Attributes.Add("onMouseOut", "hideTooltip(event);")

                        objCellImg.Controls.Add(objImg)

                        objCellPfl = New TableCell
                        Objtext = New Label

                        Objtext.Text = A.Perfil.Trim
                        objCellPfl.Controls.Add(Objtext)

                        objrow.Cells.Add(objCellImg)
                        objrow.Cells.Add(objCellPfl)

                        '---------------------------------------------------------------------
                        'jlagos 01-10-2012 -se quita exit for para que continue con las firmas
                        'Exit For
                        '---------------------------------------------------------------------

                    End If

                Next

                TB.Rows.Add(objrow)

                objrow = Nothing

            Next

            TB.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Public Function AprobacionRescataEstado(ByVal Id_nnc As Integer, ByVal nro_suc As Integer) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la Aprobacion 
        'Creado por A Saldivar.
        'Fecha Creacion: 05/08/2010
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim coll As New Collection

            Dim Aprobaciones = From A In data.apb_cls Where A.id_nnc = Id_nnc And _
                                                           A.nnc_cls.id_suc = nro_suc And _
                                                           A.frm_cls.frm_est = "A" _
                                                     Order By A.frm_cls.id_p_005

            For Each a In Aprobaciones
                coll.Add(a)
            Next

            Return coll

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function AprobacionDevuelve(ByVal NroAPB As Integer) As apb_cls

        '**************************************************************************************************************************************************
        'Descripcion: devuelve la observacion y fecha de la aprobacion, (Aprobada o Rechazada)
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 02/09/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Dim Aprobaciones As apb_cls = (From A In Data.apb_cls Where A.id_apb = NroAPB).First

            Return Aprobaciones


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ValidaAprobaciones(ByVal NroOpn As Integer, ByVal Sucursal As Integer) As Boolean

        Try

            '**************************************************************************************************************************************************
            'Descripcion: devuelve si se cumple las aprobaciones para una negociacion por jerarquia de niveles
            'Creado por Jorge Lagos C.
            'Fecha Creacion: 10/11/2008
            'Quien Modifica              Fecha              Descripcion
            'S. Henriquez               26/07/2012          Se corrige aprobacion de niveles
            '**************************************************************************************************************************************************
            Dim Valida_Nivel As Boolean = False
            Dim Valida_Clasificacion As Boolean = False
            Dim Data As New DataClsFactoringDataContext

            Dim neg As opn_cls = (From N In Data.opn_cls Where N.id_opn = NroOpn).First

            Dim Clasificaciones = From N In Data.nnc_cls Where N.id_opn = NroOpn And N.id_suc = neg.id_suc
            Dim TipoOperacion As Integer = (From N In Data.opn_cls Where N.id_opn = NroOpn Select N.id_P_0012).First

            If (TipoOperacion = 4) Then
                Return True
            End If

            Dim Nivel = From P In Data.P_005_cls

            For Each C In Clasificaciones

                Valida_Nivel = False

                For Each Ni In Nivel

                    Dim firmas = From N In Data.apb_cls Where N.id_nnc = C.id_nnc _
                                                          And N.frm_cls.id_ccf = C.id_ccf _
                                                          And N.frm_cls.id_p_005 = Ni.id_P_005 _
                                                          And N.frm_cls.frm_est = "A"

                    For Each f In firmas

                        If IsNothing(f.apb_est_ado) Then
                            Valida_Nivel = False
                            Exit For
                        ElseIf f.apb_est_ado = "1" Then
                            Valida_Nivel = True
                            'Exit For
                        ElseIf f.apb_est_ado = "2" Then
                            Valida_Nivel = False
                            Exit For
                        End If

                    Next

                    If Valida_Nivel Then
                        Exit For
                    End If

                Next

                If Valida_Nivel Then
                    Valida_Clasificacion = True
                Else
                    Valida_Clasificacion = False
                    Exit For
                End If

            Next


            If Valida_Clasificacion Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidaSobregiroDeLineaDelCliente(ByVal nrooperacion As Long, ByVal TotalGiro As Double) As Boolean

        Try

            '**************************************************************************************************************************************************
            'Descripcion: valida si la linea de financiamiento se sobregirara al otorgar la operacion y que no sea puntual
            'Creado por Jorge Lagos C.
            'Fecha Creacion: 13/10/2012
            'Quien Modifica              Fecha              Descripcion
            '**************************************************************************************************************************************************
            Dim Data As New DataClsFactoringDataContext
            Dim descuento As Double

            Dim sistema As sis_cls = (From s In Data.sis_cls).First

            If sistema.sis_vld_lin = "N" Then
                Return True
            End If

            Try

                descuento = (From n In Data.ing_sec_cls _
                                      Join e In Data.sim_egr_cls On e.id_egr Equals n.egr_sec_cls.id_egr _
                                      Where n.id_P_0053 = 2 And _
                                            n.egr_sec_cls.id_P_0055 = 4 And _
                                            e.id_ope = nrooperacion _
                                      Select n.ing_mto_abo).Sum

            Catch ex As Exception
                descuento = 0
            End Try

            Dim MontoOcuOpe As Double
            Dim ExcesoOpe As Decimal

            Dim mtoaprobado As Decimal
            Dim mtoocupado As Decimal
            Dim porexceso As Decimal

            Dim operacion As ope_cls = (From n In Data.ope_cls Where n.id_ope = nrooperacion).First
            Dim linea As ldc_cls = (From l In Data.ldc_cls Where l.cli_idc = operacion.opn_cls.eva_cls.cli_idc And l.id_P_0029 = 1).First

            If Not IsNothing(linea) Then
                mtoocupado = linea.ldc_mto_ocp
                mtoaprobado = linea.ldc_mto_apb
                porexceso = linea.ldc_por_exc
            Else
                mtoocupado = 0
                mtoaprobado = 0
                porexceso = 0
            End If

            'monto operacion + linea ocupado
            'MontoOcuOpe = ((operacion.ope_mto_ant - descuento) + mtoocupado)
            MontoOcuOpe = ((TotalGiro - descuento) + mtoocupado)

            'Porcentaje de exceso calculado
            ExcesoOpe = (MontoOcuOpe) / mtoaprobado


            'La suma del monto anticipado + mas el monto ocupado supera lo aprobado de la linea,
            If MontoOcuOpe > mtoaprobado And operacion.ope_ptl = "N" Then
                If ExcesoOpe - 1 > porexceso Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidaSobregiroDeLineaDelCliente(ByVal nrooperacion As Long) As Boolean

        Try

            '**************************************************************************************************************************************************
            'Descripcion: valida si la linea de financiamiento se sobregirara al otorgar la operacion y que no sea puntual
            'Creado por Jorge Lagos C.
            'Fecha Creacion: 13/10/2012
            'Quien Modifica              Fecha              Descripcion
            '**************************************************************************************************************************************************
            Dim Data As New DataClsFactoringDataContext
            Dim descuento As Double

            Try

                descuento = (From n In Data.ing_sec_cls _
                                      Join e In Data.sim_egr_cls On e.id_egr Equals n.egr_sec_cls.id_egr _
                                      Where n.id_P_0053 = 2 And _
                                            n.egr_sec_cls.id_P_0055 = 4 And _
                                            e.id_ope = nrooperacion _
                                      Select n.ing_mto_abo).Sum

            Catch ex As Exception
                descuento = 0
            End Try

            Dim MontoOcuOpe As Double
            Dim ExcesoOpe As Decimal
            Dim sistema As sis_cls = (From s In Data.sis_cls).First
            Dim mtoaprobado As Decimal
            Dim mtoocupado As Decimal
            Dim porexceso As Decimal

            Dim operacion As ope_cls = (From n In Data.ope_cls Where n.id_ope = nrooperacion).First
            Dim linea As ldc_cls = (From l In Data.ldc_cls Where l.cli_idc = operacion.opn_cls.eva_cls.cli_idc And _
                                                                 l.id_P_0029 = 1).First

            If Not IsNothing(linea) Then
                mtoocupado = linea.ldc_mto_ocp
                mtoaprobado = linea.ldc_mto_apb
                porexceso = linea.ldc_por_exc
            Else
                mtoocupado = 0
                mtoaprobado = 0
                porexceso = 0
            End If

            'monto operacion + linea ocupado
            MontoOcuOpe = ((operacion.ope_mto_ant - descuento) + mtoocupado)

            'Porcentaje de exceso calculado
            ExcesoOpe = (MontoOcuOpe) / mtoaprobado


            'La suma del monto anticipado + mas el monto ocupado supera lo aprobado de la linea,
            If MontoOcuOpe > mtoaprobado And operacion.ope_ptl = "N" Then
                If ExcesoOpe - 1 > porexceso Then
                    If sistema.sis_vld_lin = "S" Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidaSobregiroDeLineaDelClienteSimulacion(ByVal nrooperacion As Long) As Boolean

        Try

            '**************************************************************************************************************************************************
            'Descripcion: valida si la linea de financiamiento se sobregirara al otorgar la operacion y que no sea puntual
            'Creado por Jorge Lagos C.
            'Fecha Creacion: 13/10/2012
            'Quien Modifica              Fecha              Descripcion
            '**************************************************************************************************************************************************
            Dim Data As New DataClsFactoringDataContext
            Dim descuento As Double

            Try

                descuento = (From n In Data.ing_sec_cls _
                                      Join e In Data.sim_egr_cls On e.id_egr Equals n.egr_sec_cls.id_egr _
                                      Where n.id_P_0053 = 2 And _
                                            n.egr_sec_cls.id_P_0055 = 4 And _
                                            e.id_ope = nrooperacion _
                                      Select n.ing_mto_abo).Sum

            Catch ex As Exception
                descuento = 0
            End Try

            Dim MontoOcuOpe As Double
            Dim ExcesoOpe As Decimal
            Dim sistema As sis_cls = (From s In Data.sis_cls).First
            Dim mtoaprobado As Decimal
            Dim mtoocupado As Decimal
            Dim porexceso As Decimal

            Dim operacion As ope_cls = (From n In Data.ope_cls Where n.id_ope = nrooperacion).First

            If IsNothing(operacion.ldc_cls) Then

                Dim linea As ldc_cls = (From l In Data.ldc_cls Where l.cli_idc = operacion.opn_cls.eva_cls.cli_idc And l.id_P_0029 = 1).First

                mtoocupado = linea.ldc_mto_ocp
                mtoaprobado = linea.ldc_mto_apb
                porexceso = linea.ldc_por_exc

            Else

                mtoocupado = operacion.ldc_cls.ldc_mto_ocp
                mtoaprobado = operacion.ldc_cls.ldc_mto_apb
                porexceso = operacion.ldc_cls.ldc_por_exc

            End If

            'monto operacion + linea ocupado
            MontoOcuOpe = ((operacion.ope_mto_ant - descuento) + mtoocupado)

            'Porcentaje de exceso calculado
            ExcesoOpe = (MontoOcuOpe) / mtoaprobado


            'La suma del monto anticipado + mas el monto ocupado supera lo aprobado de la linea,
            If MontoOcuOpe > mtoaprobado And operacion.ope_ptl = "N" Then
                If ExcesoOpe - 1 > porexceso Then
                    If sistema.sis_vld_lin = "S" Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function ValidaSobregiroDeLineaDelPagador(ByVal nrooperacion As Long) As Boolean

        Try

            '**************************************************************************************************************************************************
            'Descripcion: valida si la linea de financiamiento se sobregirara al otorgar la operacion y que no sea puntual
            'Creado por Jorge Lagos C.
            'Fecha Creacion: 03/04/2014
            'Quien Modifica              Fecha              Descripcion
            '**************************************************************************************************************************************************
            Dim Data As New DataClsFactoringDataContext
            Dim descuento As Double

            Try

                descuento = (From n In Data.ing_sec_cls _
                                      Join e In Data.sim_egr_cls On e.id_egr Equals n.egr_sec_cls.id_egr _
                                      Where n.id_P_0053 = 2 And _
                                            n.egr_sec_cls.id_P_0055 = 4 And _
                                            e.id_ope = nrooperacion _
                                      Select n.ing_mto_abo).Sum

            Catch ex As Exception
                descuento = 0
            End Try

            Dim MontoOcuOpe As Double
            Dim ExcesoOpe As Decimal
            Dim sistema As sis_cls = (From s In Data.sis_cls).First
            Dim mtoaprobado As Decimal
            Dim mtoocupado As Decimal
            Dim porexceso As Decimal

            Dim operacion As ope_cls = (From n In Data.ope_cls Where n.id_ope = nrooperacion).First
            Dim pagadores = From d In Data.dsi_cls Where d.id_ope = nrooperacion Select d.deu_ide Distinct

            For Each p In pagadores

                pRutDeudor = CLng(p).ToString()

                Dim linea As deu_mon_cls = (From l In Data.deu_mon_cls Where l.deu_ide = p And l.id_p_0029 = 1 And l.id_p_0023 = operacion.opn_cls.id_P_0023).First

                If Not IsNothing(linea) Then
                    mtoocupado = linea.deu_mon_ocu
                    mtoaprobado = linea.deu_mon_apr
                Else
                    mtoocupado = 0
                    mtoaprobado = 0
                    porexceso = 0
                End If

                'monto operacion + linea ocupado
                MontoOcuOpe = ((operacion.ope_mto_ant - descuento) + mtoocupado)

                'La suma del monto anticipado + mas el monto ocupado supera lo aprobado de la linea,
                If MontoOcuOpe > mtoaprobado And operacion.ope_ptl = "N" And sistema.sis_vld_lin = "S" Then
                    Return False
                End If

            Next

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "REQUISITOS Y CONDICIONES"

    Public Function RequisitosDevuelveTodos(ByVal LlenaGrilla As Boolean, Optional ByVal GV As GridView = Nothing, Optional ByVal todos As Int16 = 0) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Guarda el un requisito
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        'SHenriquez                  18-06-2012         Se corrige formateo grilla
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext
            Dim Coll As New Collection

            If todos = 1 Then
                Dim Requisitos = From R In data.req_cls Order By R.id_req

                For Each R In Requisitos
                    Coll.Add(R)
                Next

            Else
                Dim Requisitos = From R In data.req_cls Where R.req_est = "A" Order By R.id_req

                For Each R In Requisitos
                    Coll.Add(R)
                Next

            End If

            If LlenaGrilla Then

                GV.DataSource = Coll
                GV.DataBind()

                For I = 0 To GV.Rows.Count - 1

                    If GV.Rows(I).Cells(3).Text = "A" Then
                        GV.Rows(I).Cells(3).Text = "ACTIVO"
                    Else
                        GV.Rows(I).Cells(3).Text = "INACTIVO"
                    End If

                Next

            Else
                Return Coll
            End If

        Catch ex As Exception

        End Try

    End Function

    Public Function RequisitosDevuelvePorTipoDocto(ByVal TipoDeDocumento As Integer) As Collection


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve por requisitos de un Tipo de Documento
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim data As New DataClsFactoringDataContext

            Dim Requisitos = From R In data.rxd_cls Where R.id_p_0031 = TipoDeDocumento Order By R.id_rxd

            For Each R In Requisitos
                Coll.Add(R)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function RequisitosDevuelvePorOperacion(ByVal NroOpe As Integer) As Collection


        '**************************************************************************************************************************************************
        'Descripcion: Devuelve por requisitos por Operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Dim Coll As New Collection

        Try

            Dim data As New DataClsFactoringDataContext

            Dim Requisitos = From R In data.rxo_cls Where R.id_ope = NroOpe Order By R.id_rxd _
                             Select NroRxo = R.id_rxo, _
                                    NroRxd = R.id_rxd, _
                                    Rxd_Des = R.rxd_cls.req_cls.req_des, _
                                    Estado = R.rxo_est, _
                                    Usuario = R.eje_cls.eje_des_cra, _
                                    Fecha = R.rxo_fec_apb

            For Each R In Requisitos
                Coll.Add(R)
            Next

            Return Coll

        Catch ex As Exception
            Return Coll
        End Try

    End Function

    Public Function CondicionesDevuelveTodosPorOperacion(ByVal NroOpe As Integer, ByVal LlenaGrilla As Boolean, _
                                                         Optional ByVal GV As GridView = Nothing) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las condiciones de una Operacion especifica
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 07/11/2008
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  04/01/2011         Se agrega paginacion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            Dim Condiciones = (From R In data.cdn_cls Where R.id_ope = NroOpe Order By R.id_cdn _
                              Select id_cdn = R.id_cdn, _
                              id_p_0111 = R.id_p_0111, _
                              cdn_des = R.cdn_des, _
                              cdn_fec_com = R.cdn_fec_com, _
                              cdn_fec_ing = R.cdn_fec_ing, _
                              cdn_fec_apb = R.cdn_fec_apb, _
                              cdn_usr_apb = R.eje_cls.eje_des_cra, _
                              cdn_usr_ing = R.eje_cls.eje_des_cra, _
                              estado = R.p_0111_cls.pnu_des).Skip(sesion.NroPaginacion_Condicion)


            For Each R In Condiciones.Take(12)
                Coll.Add(R)
            Next

            If LlenaGrilla Then

                GV.DataSource = Coll
                GV.DataBind()

                'For I = 1 To Coll.Count

                '    Dim Cb As Image
                '    Cb = GV.Rows(I - 1).FindControl("Img_Condicion")

                '    If Coll.Item(I).id_p_0111 = 2 Or Coll.Item(I).id_p_0111 = 4 Then
                '        Cb.ImageUrl = "../../../Imagenes/Iconos/verde.gif"
                '    Else
                '        Cb.ImageUrl = "../../../Imagenes/Iconos/rojo.gif"
                '    End If

                'Next

            Else
                Return Coll
            End If

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CondicionesDevuelve(ByVal RutCliente As Long, _
                                        ByVal CodEjecutivo As Integer, _
                                        ByVal CodEstado As Integer, _
                                        ByVal LlenaGrilla As Boolean, _
                                        Optional ByVal GV As GridView = Nothing) As Collection

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve las condiciones por cliente o ejecutivo que este solo ingresadas (incumplida) y cuya operacion se otorgo
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 14/07/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext
            Dim sesion As New ClsSession.ClsSession
            Dim Coll As New Collection
            Dim RutCliente2 As Long
            Dim CodEjecutivo2 As Integer
            Dim CodEstado2 As Integer

            If Val(RutCliente) = 0 Then
                RutCliente2 = 999999999
            Else
                RutCliente2 = RutCliente
            End If

            If Val(CodEjecutivo) = 0 Then
                CodEjecutivo2 = 999
            Else
                CodEjecutivo2 = CodEjecutivo
            End If

            If Val(CodEstado) = 0 Then
                CodEstado2 = 999
            Else
                CodEstado2 = CodEstado
            End If

            Dim Condiciones = From R In data.cdn_cls Where (R.ope_cls.opn_cls.eva_cls.cli_idc >= Format(RutCliente, Var.FMT_RUT) And R.ope_cls.opn_cls.eva_cls.cli_idc <= Format(RutCliente2, Var.FMT_RUT)) And _
                               (R.ope_cls.id_eje >= CodEjecutivo And R.ope_cls.id_eje <= CodEjecutivo2) And _
                               (R.id_p_0111 >= CodEstado And R.id_p_0111 <= CodEstado2) And (R.ope_cls.id_P_0030 = 2 Or R.ope_cls.id_P_0030 = 3) _
                               Order By R.id_cdn _
                               Select id_cdn = R.id_cdn, _
                                      id_p_0111 = R.id_p_0111, _
                                      cdn_des = R.cdn_des, _
                                      cdn_fec_com = R.cdn_fec_com, _
                                      cdn_fec_ing = R.cdn_fec_ing, _
                                      cdn_fec_apb = R.cdn_fec_apb, _
                                      cdn_usr_apb = R.eje_cls.eje_des_cra, _
                                      cdn_usr_ing = R.eje_cls.eje_des_cra, _
                                      estado = R.p_0111_cls.pnu_des, _
                                      operacion = R.id_ope

            For Each R In Condiciones
                Coll.Add(R)
            Next

            If LlenaGrilla Then
                GV.DataSource = Coll
                GV.DataBind()
            Else
                Return Coll
            End If

            Return Coll

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function CondicionesDevuelve(ByVal id As Integer) As cdn_cls

        '**************************************************************************************************************************************************
        'Descripcion: Devuelve una condicion por su ID unico
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 17/07/2012
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim Condicion As cdn_cls = (From R In data.cdn_cls Where R.id_cdn = id Select R).First()

            Return Condicion

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Sub ClientesPendienteDevuelve(ByVal GV As GridView, ByVal Rut_Dsd As String, ByVal Rut_Hst As String, _
                                     ByVal CodEje_Dsd As Integer, ByVal CodEje_Hst As Integer, _
                                     ByVal TipCli_Dsd As Integer, ByVal TipCli_Hst As Integer, _
                                     ByVal Razon As String, Optional ByVal todos As Boolean = False)

        '*********************************************************************************************************************************
        'Descripcion: Devuelve todos los clientes por rango en Rut y ejecutivos que se encuentren activos                  
        'Creado por=  victor alvarez rojas                                                                                                                     
        'Fecha Creacion: 17/10/2011                                                                                                                 

        '*********************************************************************************************************************************
        Try

            Dim Data As New CapaDatos.DataClsFactoringDataContext
            Dim FC As New FuncionesGenerales.FComunes
            Dim Sesion As New ClsSession.ClsSession
            Dim Coll As New Collection

            'Orden Por rut


            Dim Clientes = (From Cli In Data.cli_cls _
                                Join Nes In Data.nes_cls On Nes.id_eje Equals Cli.id_eje_cod_eje _
                                Where CInt(Cli.cli_idc) >= Rut_Dsd And _
                                      CInt(Cli.cli_idc) <= Rut_Hst And _
                                           Cli.id_eje_cod_eje >= CodEje_Dsd And _
                                           Cli.id_eje_cod_eje <= CodEje_Hst And _
                                           Cli.id_P_0044 >= TipCli_Dsd And _
                                           Cli.id_P_0044 <= TipCli_Hst And _
                                           Cli.id_P_008 = 6 Order By Cli.cli_idc _
                   Select New With {.cli_idc = Cli.cli_idc, _
                                    .digito = Cli.cli_dig_ito, _
                                    .cli_rso = If(Cli.id_P_0044 = 1, _
                                                  Cli.cli_rso & " " & Cli.cli_ape_ptn & " " & Cli.cli_ape_mtn, _
                                                  Cli.cli_rso), _
                                    .PNU_CLI_TIP_DES = Cli.P_0044_cls.pnu_des, _
                                    .PNU_CLI_EST_DES = Cli.P_008_cls.pnu_des}).Skip(Sesion.NroPaginacion)

            For Each Cli In Clientes.Take(8)
                Cli.cli_idc = Format(CLng(Cli.cli_idc), Fmt.FCMSD) & "-" & Cli.digito
                Coll.Add(Cli)
            Next



            GV.DataSource = Coll
            GV.DataBind()


        Catch ex As Exception

        Finally

        End Try

    End Sub

#End Region

#End Region

#Region "Actualizaciones"

#Region "Clasificación y Firmas"

    Public Function guarda_detalle_clasificacion(ByVal id_ccf As Integer, ByVal cfc_des As String, ByVal id_p_0069 As Integer, ByVal rgo_dde As Double, ByVal rgo_hta As Double) As Boolean
        'Descripcion: Guarda detalle asociado a la cabecera de una clasificacion
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New cfc_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Clasificacion.id_ccf = id_ccf
            Clasificacion.cfc_des = cfc_des
            Clasificacion.id_P_0069 = id_p_0069
            Clasificacion.cfc_dde = rgo_dde
            Clasificacion.cfc_hta = rgo_hta

            Data.cfc_cls.InsertOnSubmit(Clasificacion)

            Data.SubmitChanges()

            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function modifica_detalle_clasificacion(ByVal id_ccf As Integer, ByVal id_cfc As Integer, ByVal cfc_des As String, ByVal id_p_0069 As Integer, ByVal rgo_dde As Double, ByVal rgo_hta As Double) As Boolean
        'Descripcion: Guarda detalle asociado a la cabecera de una clasificacion
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New cfc_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim c As cfc_cls = (From cf In Data.cfc_cls Where cf.id_ccf = id_ccf And cf.id_cfc = id_cfc).First

            If IsNothing(c) = False Then

                c.id_P_0069 = id_p_0069
                c.cfc_dde = rgo_dde
                c.cfc_hta = rgo_hta
                Data.SubmitChanges()
                Return True

            Else
                Return Nothing
            End If



        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function guarda_cabecera_clasificacion(ByVal id_ccf As Int32, ByVal ccf_est As String, ByVal ccf_des As String, ByVal ccf_apb As Integer) As Boolean
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New ccf_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()



            Clasificacion.ccf_est = ccf_est
            Clasificacion.ccf_des = ccf_des
            Clasificacion.ccf_tip_apb = ccf_apb



            Data.ccf_cls.InsertOnSubmit(Clasificacion)

            Data.SubmitChanges()



            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function cabecera_clasificacion_modifica(ByVal id_ccf As Int32, ByVal ccf_est As String, ByVal ccf_des As String, ByVal ccf_apb As Integer) As Boolean
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New ccf_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()


            Dim cf As ccf_cls = (From c In Data.ccf_cls Where c.id_ccf = id_ccf Select c).First




            cf.ccf_est = ccf_est
            cf.ccf_des = ccf_des
            cf.ccf_tip_apb = ccf_apb






            Data.SubmitChanges()



            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function borra_detalle_clasificacion(ByVal id_ccf As Int32, ByVal id_cfc As Integer) As Boolean
        'Descripcion: borra detalle asociado a la cabecera de una clasificacion
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New cfc_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim c As cfc_cls = (From cf In Data.cfc_cls Where cf.id_ccf = id_ccf And cf.id_cfc = id_cfc).First

            If IsNothing(c) = False Then

                Data.cfc_cls.DeleteOnSubmit(c)
                Data.SubmitChanges()
                Return True

            End If





        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Elimina_cabecera_clasificacion(ByVal id_ccf As Int32) As Boolean
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim Clasificacion = New ccf_cls()
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()
            Dim idx As Integer

            Dim cont = (From cf In Data.cfc_cls Where cf.id_ccf = id_ccf Select cf).Count


            If cont > 0 Then
                For idx = 0 To cont
                    Dim c As cfc_cls = (From cf In Data.cfc_cls Where cf.id_ccf = id_ccf Select cf).First
                    Data.cfc_cls.DeleteOnSubmit(c)
                    Data.SubmitChanges()
                Next
                'Valida si existen detalles de la cabecera , si hay los borra y reingresa
            End If
            Dim cabecera As ccf_cls = (From cc In Data.ccf_cls Where cc.id_ccf = id_ccf).First
            Data.ccf_cls.DeleteOnSubmit(cabecera)
            Data.SubmitChanges()




            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function guarda_firma_clasificacion(ByVal COL As Collection, ByVal id_ccf As Integer, ByVal prio As Integer) As Boolean

        '****************************************************************************************************************************
        'Descripcion: Guarda Cabecera de Clasificaciones
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 30/05/2008
        'Quien Modifica              Fecha              Descripcion
        '****************************************************************************************************************************

        Try

            Dim firma As frm_cls

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim I As Integer


            Dim c = From cf In Data.frm_cls Where cf.id_ccf = id_ccf And cf.id_p_005 = prio And cf.frm_est = "A"

            Data.frm_cls.DeleteAllOnSubmit(c)

            For I = 1 To COL.Count

                firma = New frm_cls
                firma.id_p_005 = COL.Item(I).id_p_005
                firma.id_ccf = COL.Item(I).id_ccf
                firma.id_p_0045 = COL.Item(I).id_p_0045

                Data.frm_cls.InsertOnSubmit(firma)

            Next

            Data.SubmitChanges()


            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function clasificacion_x_sucursal_guarda(ByVal suc As Integer, ByVal ccf As Integer) As Boolean
        'Descripcion: Guarda Relacion de Clasificaciones con Sucursales 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 04/09/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try

            Dim cxs As cxs_cls

            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()




            Dim c = From cf In Data.cxs_cls Where cf.id_ccf = ccf And cf.id_suc = suc And cf.cxs_est = "A"



            For Each P In c


                cxs = New cxs_cls

                With cxs

                    .cxs_est = "I"

                End With
            Next

            cxs = New cxs_cls

            cxs.cxs_est = "A"
            cxs.id_ccf = ccf
            cxs.id_suc = suc

            Data.cxs_cls.InsertOnSubmit(cxs)
            Data.SubmitChanges()


            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Sucursal_de_aprobacion_guarda(ByVal col_sxa As Collection, ByVal ccf As Integer) As Boolean
        'Descripcion: Guarda Relacion de Clasificaciones con Sucursales 
        'Creado por= Pablo Gatica S.
        'Fecha Creacion: 04/09/2008
        'Quien Modifica              Fecha              Descripcion
        '
        Try
            Dim COLL As New Collection
            Dim CXS_Obj
            Dim sxa As sxa_cls
            Dim I As Integer
            Dim Data As DataClsFactoringDataContext = New DataClsFactoringDataContext()

            Dim cxs = From cx In Data.cxs_cls Where cx.id_ccf = ccf Select cx.id_cxs


            If cxs.Count <> 0 Then

                For Each P In cxs

                    CXS_Obj = New cxs_cls

                    With CXS_Obj
                        .id_cxs = P
                    End With

                    COLL.Add(CXS_Obj)

                Next
            End If


            For I = 1 To COLL.Count
                Dim X As Integer

                X = COLL.Item(I).id_cxs

                Dim APB = From APR In Data.sxa_cls Where APR.id_cxs = X And APR.sxa_est = "A"


                For Each APR In APB
                    With APR
                        .sxa_est = "I"
                    End With
                Next

            Next

            For I = 1 To col_sxa.Count

                sxa = New sxa_cls


                sxa.id_cxs = col_sxa.Item(I).id_cxs
                sxa.id_suc = col_sxa.Item(I).id_suc
                sxa.sxa_est = "A"
                Data.sxa_cls.InsertOnSubmit(sxa)

            Next

            Data.SubmitChanges()

            Return True


        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#Region "Pizarra Aprobacion"

    Public Function AprobacionInserta(ByVal APB As apb_cls) As Integer
        '*********************************************************************************************************************************
        'Descripcion: Inserta una aprobacion de un ejecutivo
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 02/09/2008
        'Quien Modifica              Fecha              Descripcion
        '*********************************************************************************************************************************

        Try

            Dim Data As New DataClsFactoringDataContext

            Data.apb_cls.InsertOnSubmit(APB)
            Data.SubmitChanges()

            Return 0

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function AprobacionUpdate(ByVal NroNeg As Integer, ByVal NroCCF As Integer, ByVal Estado As Char, _
                                 ByVal Obs As String, ByVal NroPerfil As Integer, ByVal NroEje As Integer) As Boolean
        '*********************************************************************************************************************************
        'Descripcion: Actualiza una aprobacion de un ejecutivo, Negociacion y clasificacion
        'Creado por= Jorge Lagos C.
        'Fecha Creacion: 02/09/2008
        'Quien Modifica              Fecha              Descripcion
        'jlagos                     26-07-2012          se agrega funciones del ejecutivo en vez de solo el perfil
        '*********************************************************************************************************************************

        Try


            Dim Data As New DataClsFactoringDataContext

            '---------------------------------------------------------------------------------------------------------------------------------------
            'Trae todas las funciones del ejecutivo
            '---------------------------------------------------------------------------------------------------------------------------------------
            Dim funciones = From E In Data.nef_cls Where E.id_eje = NroEje

            For Each f In funciones

                '---------------------------------------------------------------------------------------------------------------------------------------
                'Trae los niveles donde esta el perfil por su N° de Negociacion y N° de nivel de riesgo
                '---------------------------------------------------------------------------------------------------------------------------------------
                Dim Aprobacion = From A In Data.apb_cls Where A.nnc_cls.id_opn = NroNeg And _
                                                              A.frm_cls.id_p_0045 = f.id_P0045 _
                                                              And A.nnc_cls.id_ccf = NroCCF

                '---------------------------------------------------------------------------------------------------------------------------------------
                'Actualizamos los campos de la aprobacion o rechazo
                '---------------------------------------------------------------------------------------------------------------------------------------
                For Each Apb In Aprobacion

                    With Apb
                        .id_eje = NroEje
                        .apb_est_ado = Estado
                        .apb_obs_apb = Obs
                        .apb_fec_apb = Date.Now
                    End With

                Next

                '---------------------------------------------------------------------------------------------------------------------------------------
                'Si esta aprobando, se deben enviar mail a la firma siguiente (Usuario que tiene el perfil)
                '---------------------------------------------------------------------------------------------------------------------------------------
                If Estado = "1" Then

                    '---------------------------------------------------------------------------------------------------------------------------------------
                    'Trae todos los niveles por su N° de Negociacion y N° de nivel de riesgo
                    '---------------------------------------------------------------------------------------------------------------------------------------
                    Dim Aprobaciones = From A In Data.apb_cls Where A.nnc_cls.id_opn = NroNeg And _
                                                                    A.nnc_cls.id_ccf = NroCCF And _
                                                                    A.apb_est_ado = Nothing

                    Dim Busqueda As Boolean = False
                    Dim Coll_Perfiles As New Collection

                    For Each A In Aprobaciones
                        '---------------------------------------------------------------------------------------------------------------------------------------
                        'Si encontro el perfil, avanza para saber el siguiente perfil requerido
                        '---------------------------------------------------------------------------------------------------------------------------------------
                        'If Busqueda Then
                        Coll_Perfiles.Add(A.frm_cls.id_p_0045)
                        '    Busqueda = False
                        'End If

                        For Each Apb In Aprobacion
                            '---------------------------------------------------------------------------------------------------------------------------------------
                            'Si es el perfil que firma
                            '---------------------------------------------------------------------------------------------------------------------------------------
                            If A.frm_cls.id_p_0045 = Apb.frm_cls.id_p_0045 Then
                                Busqueda = True
                                Exit For
                            End If

                        Next

                    Next

                    '---------------------------------------------------------------------------------------------------------------------------------------
                    'Traemos la informacion de la negociacion
                    '---------------------------------------------------------------------------------------------------------------------------------------
                    Dim Negociacion As ope_cls = (From N In Data.ope_cls Where N.id_opn = NroNeg).First
                    Dim Cliente As String
                    Dim RutCliente As String

                    RutCliente = Format(CLng(Negociacion.opn_cls.eva_cls.cli_idc), Fmt.FCMSD) & "-" & Negociacion.opn_cls.eva_cls.cli_cls.cli_dig_ito

                    If Negociacion.opn_cls.eva_cls.cli_cls.id_P_0044 <> 1 Then
                        Cliente = Negociacion.opn_cls.eva_cls.cli_cls.cli_rso.Trim
                    Else
                        Cliente = Negociacion.opn_cls.eva_cls.cli_cls.cli_rso.Trim & Negociacion.opn_cls.eva_cls.cli_cls.cli_ape_ptn.Trim
                    End If

                    '---------------------------------------------------------------------------------------------------------------------------------------
                    'Si encontro los perfiles que requieren su firma
                    '---------------------------------------------------------------------------------------------------------------------------------------
                    If Coll_Perfiles.Count > 0 Then

                        Dim RG As New FuncionesGenerales.FComunes
                        Dim Coll_Mails As New Collection

                        For I = 1 To Coll_Perfiles.Count

                            '---------------------------------------------------------------------------------------------------------------------------------------
                            'Buscamos los usuario que tienen el perfil requerido para enviarles un mail de aviso
                            '---------------------------------------------------------------------------------------------------------------------------------------
                            Dim Usuarios = From U In Data.eje_cls Where U.id_P_0045 = CInt(Coll_Perfiles.Item(I)) Select U

                            For Each U In Usuarios
                                Coll_Mails.Add(U)
                            Next

                        Next

                        If Coll_Mails.Count > 0 Then

                            Dim Mail As New Email

                            For Each E In Coll_Mails

                                '---------------------------------------------------------------------------------------------------------------------------------------
                                'Si encontro usuarios, envia los mail.
                                '---------------------------------------------------------------------------------------------------------------------------------------
                                If E.id_pfl <> NroPerfil Then


                                    Dim sb As New StringBuilder

                                    sb.Append(vbCrLf)
                                    sb.Append("<table style=""font-family: verdana; font-size: 15px; color: #0000FF"">")

                                    sb.Append("<tr><td>")
                                    sb.Append(" <span>Señor: </span>")
                                    sb.Append(" <span style=""font-weight: bold""> " & E.eje_nom & "</span>")
                                    sb.Append("</td></tr>")

                                    sb.Append("<tr><td>")
                                    sb.Append(" <span>Existe operación de confirmación del cliente:</span>")
                                    sb.Append(" <span style=""font-weight: bold""> " & RutCliente & " " & Cliente & " </span>")
                                    sb.Append("</td></tr>")

                                    sb.Append("<tr><td>")
                                    sb.Append(" <span>Con numero de operación:</span>")
                                    sb.Append(" <span style=""font-weight: bold""> " & Negociacion.id_ope & " </span>")
                                    sb.Append("</td></tr>")

                                    sb.Append("<tr><td>")
                                    sb.Append(" <span>Observación:</span>")
                                    sb.Append(" <span> " & Obs.Trim & "</span>")
                                    sb.Append("</td></tr>")

                                    sb.Append("<tr><td valign=bottom  height=100>")
                                    sb.Append(" <span>Saluda atentamente a Ud.</span><br/>")
                                    sb.Append(" <span>Factorclick</span><br/>")
                                    sb.Append("</td></tr>")

                                    sb.Append("</table>")
                                    sb.Append(vbCrLf)

                                    Mail.enviomail(Coll_Mails, "FactoringNet@dim.cl", "Solicitud de Firma para Operación N° " & CStr(Negociacion.id_ope), sb)

                                End If

                            Next

                        End If

                    End If

                End If

            Next

            '---------------------------------------------------------------------------------------------------------------------------------------
            'Efectuamos los cambios realizados y retornamos en verdadero para decir que fue exitoso
            '---------------------------------------------------------------------------------------------------------------------------------------

            Data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function



#End Region

#Region "REQUISITOS Y CONDICIONES"

    Public Function RequisitosInserta(ByVal REQ As req_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta el un requisito
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.req_cls.InsertOnSubmit(REQ)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RequisitosActualiza(ByVal REQ As req_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el un requisito
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim RE As req_cls = (From R In data.req_cls Where R.id_req = REQ.id_req).First

            RE.req_des = REQ.req_des
            RE.req_est = REQ.req_est

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RequisitosPorDoctoInserta(ByVal rxd As rxd_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta el un requisito
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Dim data As New DataClsFactoringDataContext
        Dim Req As rxd_cls

        Try

            Req = (From R In data.rxd_cls Where R.id_req = rxd.id_req And R.id_p_0031 = rxd.id_p_0031).First

        Catch ex As Exception

            Try

                If IsNothing(Req) Then
                    data.rxd_cls.InsertOnSubmit(rxd)
                End If

                data.SubmitChanges()

                Return True

            Catch ex1 As Exception
                Return False
            End Try

        End Try


    End Function

    Public Function RequisitosPorOperacionAprueba(ByVal rxo As rxo_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza el registro de requisitos por operacion para su aprbacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext


            Dim requisitos As rxo_cls = (From R In data.rxo_cls Where R.id_rxo = rxo.id_rxo And R.id_ope = rxo.id_ope).First

            'If IsNothing(requisitos.id_usr) Or (requisitos.rxo_est <> rxo.rxo_est) Then
            requisitos.id_eje = rxo.id_eje
            requisitos.rxo_est = rxo.rxo_est
            requisitos.rxo_fec_apb = rxo.rxo_fec_apb
            'End If

            data.SubmitChanges()

            Return True

        Catch ex1 As Exception
            Return False
        End Try

    End Function

    Public Function CondicionInserta(ByVal CDN As cdn_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Inserta una condicion para otorgar una operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            data.cdn_cls.InsertOnSubmit(CDN)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CondicionActualiza(ByVal CDN As cdn_cls) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Actualiza una condicion para otorgar una operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim Condicion As cdn_cls = (From C In data.cdn_cls Where C.id_cdn = CDN.id_cdn).First

            Condicion.id_p_0111 = CDN.id_p_0111
            Condicion.id_eje_apb = CDN.id_eje_apb
            Condicion.cdn_des = CDN.cdn_des
            Condicion.cdn_fec_com = CDN.cdn_fec_com
            Condicion.cdn_fec_apb = CDN.cdn_fec_apb

            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function CondicionElimina(ByVal Id_Cdn As Integer) As Boolean

        '**************************************************************************************************************************************************
        'Descripcion: Elimina una condicion para otorgar una operacion
        'Creado por Jorge Lagos C.
        'Fecha Creacion: 05/11/2008
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try

            Dim data As New DataClsFactoringDataContext

            Dim Condicion As cdn_cls = (From C In data.cdn_cls Where C.id_cdn = Id_Cdn).First

            data.cdn_cls.DeleteOnSubmit(Condicion)
            data.SubmitChanges()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

    Public Function ActualizaEstadoCli(ByVal Cliente As cli_cls) As Boolean

        '*********************************************************************************************************************************
        'Descripcion: Actualiza estado de un clinete
        'Creado por= victor felipe alvarez
        'Fecha Creacion: 18/10/2011

        '*********************************************************************************************************************************

        Try



            Dim Data As New CapaDatos.DataClsFactoringDataContext

            Dim Cli As cli_cls = (From c In Data.cli_cls _
                            Where c.cli_idc = Cliente.cli_idc _
                            Select c).First()


            Cli.id_P_008 = Cliente.id_P_008


            Data.SubmitChanges()
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

End Class
