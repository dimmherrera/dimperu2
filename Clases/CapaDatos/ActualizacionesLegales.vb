Imports Microsoft.VisualBasic

Public Class ActualizacionesLegales
#Region "Aval"
    Public Function AvalInserta(ByVal AvalesInserta As avl_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Aval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            data.avl_cls.InsertOnSubmit(AvalesInserta)
            data.SubmitChanges()

            Return True
        Catch ex As Exception
            Return False


        End Try
    End Function

    Public Function AvalModifica(ByVal Aval As avl_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda Aval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************

        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModificaAval As avl_cls = (From a In data.avl_cls Where a.id_avl = Aval.id_avl).First
            With ModificaAval
                .avl_dml = Aval.avl_dml
                .avl_dml_com = Aval.avl_dml_com
                .avl_fec_est_sit = Aval.avl_fec_est_sit
                .avl_fec_jun_acc = Aval.avl_fec_jun_acc
                .avl_id_cmn = Aval.avl_id_cmn
                .avl_id_cmn_com = Aval.avl_id_cmn_com
                .avl_mto = Aval.avl_mto
                .avl_nom = Aval.avl_nom
                .avl_not = Aval.avl_not
                .avl_plz = Aval.avl_plz
                .avl_ptm = Aval.avl_ptm
                .id_p_0025 = Aval.id_p_0025
                .id_p_0026 = Aval.id_p_0026
                .id_p_0027 = Aval.id_p_0027
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminaAvales(ByVal CodigoAval As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Aval
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 22/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim AvalElimina As avl_cls = (From a In data.avl_cls Where a.id_avl = CodigoAval).First
            data.avl_cls.DeleteOnSubmit(AvalElimina)
            data.SubmitChanges()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CategoriaRiesgoInserta(ByVal coll As Collection) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Guarda caregiria Riesgo
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

#Region "Pagare"

    Public Function PagareInserta(ByVal InsertaPagare As pgr_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Inserta Pagare
        'Creado por= A. Saldivar
        'Fecha Creacion: 03/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            data.pgr_cls.InsertOnSubmit(InsertaPagare)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function PagareModifica(ByVal pag As pgr_cls) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Pagare
        'Creado por= A. Saldivar
        'Fecha Creacion: 03/06/2009
        'Quien Modifica              Fecha              Descripcion
        'A Saldivar                  04/11/2010         Se agrega numero de pagare (pgr_num)
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim ModificaPagare As pgr_cls = (From p In data.pgr_cls Where p.id_pgr = pag.id_pgr).First
            With ModificaPagare
                .id_P_0021 = pag.id_P_0021
                .id_P_0022 = pag.id_P_0022
                .id_P_0023 = pag.id_P_0023
                .id_P_0061 = pag.id_P_0061
                .pgr_anc = pag.pgr_anc
                .pgr_fec_ing = pag.pgr_fec_ing
                .pgr_fec_prt = pag.pgr_fec_prt
                .pgr_fev = pag.pgr_fev
                .pgr_mdt = pag.pgr_mdt
                .pgr_mto = pag.pgr_mto
                .pgr_num = pag.pgr_num
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GeneraImpuesto(ByVal N_Pgr As Integer, ByVal cxc As Integer, _
                                   ByVal tasa As Integer, ByVal impuesto As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Modifica Pagare ,relaciona pagare con cxc
        'Creado por= A. Saldivar
        'Fecha Creacion: 03/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim ImpuestoGenera As pgr_cls = (From p In data.pgr_cls Where p.id_pgr = N_Pgr).First
            With ImpuestoGenera
                .id_cxc = cxc
                .pgr_imp = impuesto
                .pgr_tim = tasa
            End With
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function PagareElimina(ByVal CorrelativoPgr As Integer) As Boolean
        '**************************************************************************************************************************************************
        'Descripcion: Elimina Pagare
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 09/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            Dim EliminaPgr As pgr_cls = (From p In data.pgr_cls Where p.id_pgr = CorrelativoPgr).First
            data.pgr_cls.DeleteOnSubmit(EliminaPgr)
            data.SubmitChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CxcInserta(ByVal cxc As cxc_cls) As Integer
        '**************************************************************************************************************************************************
        'Descripcion: Guarda y rescata ultimo id insertado
        'Creado por= Antonio Saldivar M.
        'Fecha Creacion: 11/06/2009
        'Quien Modifica              Fecha              Descripcion
        '**************************************************************************************************************************************************
        Try
            Dim data As New DataClsFactoringDataContext
            data.cxc_cls.InsertOnSubmit(cxc)
            data.SubmitChanges()
            Dim Numero As Integer = (From c In data.cxc_cls Order By c.id_cxc Descending Select c.id_cxc).First
            Return Numero
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region

End Class
