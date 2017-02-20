Imports ClsSession.ClsSession
Imports ClsSession.SesionCobranza
Imports CapaDatos

Partial Class Modulos_Cobranzas_rigthframe_archivos_GestionCobranza
    Inherits System.Web.UI.Page

#Region "DECLARACION VARIABLES GENERALES PAGINA"

    Dim RW As New FuncionesGenerales.RutinasWeb
    Dim CG As New ConsultasGenerales
    Dim FC As New FuncionesGenerales.FComunes
    Dim AG As New ActualizacionesGenerales
    Dim VAR As New FuncionesGenerales.Variables
    Dim FMT As New FuncionesGenerales.ClsLocateInfo
    Dim Msj As New ClsMensaje
    Dim Pos As Integer = 0
    Dim Cli As Integer = 0
    Dim CBZ As New ClaseCobranza
    Dim PGO As New ClasePagos
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            'CodEje = 19
            'Sucursal = 1
            Response.Expires = -1

            If CodEje > 0 And Sucursal > 0 Then

                'Retorna Parámetro Estado Documento
                CG.ParametrosDevuelve(TablaParametro.EstadoDocumento, True, dp_EstadoDoctos)

                'Retorna los codigo de cobranza
                CBZ.CodigosCobranzaDevuelve(DP_CCO_DSD)
                CBZ.CodigosCobranzaDevuelve(DP_CCO_HTA)

                'Refresca Pantalla
                RefrescaPantalla(True, True, True)

                CargaGrillaDeudores()

                'Retorna Sucursales Cobranza y Recaudación
                CG.SucursalesDevuelve(CodEje, True, Me.DP_SucCobranza2)
                CG.SucursalesDevuelve(codeje, True, DP_SucRecaudacion)

                TipoDeContacto = 2

                HabilitaCampoDeGestion(False)

            End If

            IB_VolverGestion.Attributes.Add("onClick", "javascript:window.close();")

        Else


            If ID_GV_Doctos.Value <> "" Then

                Dim imagen_factura As String
                Dim Imagen_Carta As String

                imagen_factura = "~/Imagenes/factura.jpg"
                Imagen_Carta = "~/Imagenes/Carta.jpg"

            End If

            If RutDeu > 0 Then

                'Llena Grilla Clientes/Deudores Asociados a ente a Cobrar, cargamos de nuevo los clientes y doctos y lo volvemos a marcar
                GV_Clientes.DataSource = CBZ.DocumentosAGestionar_RetornaCliDeuAsociado(1, 999, "0000", "9999", Format(RutDeu, VAR.FMT_RUT), "CLIENTE")
                GV_Clientes.DataBind()

            End If

        End If

    End Sub

    Protected Sub Dp_Orden_GV_DEUCLI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dp_Orden_GV_DEUCLI.SelectedIndexChanged

        OrdenDeudores()

        RefrescaPantalla(True, True, True)
        CargaDeudores()


    End Sub

    Protected Sub DP_Ciudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Ciudad.SelectedIndexChanged
        'Retorna Comunas
        CG.ComunaDevuelve(CStr(DP_Ciudad.SelectedValue), True, DP_Comuna)
    End Sub

    Protected Sub DP_Comuna_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Comuna.SelectedIndexChanged
        'Retorna Zona
        Dim Temporal_zon = CG.ZonasComunaDevuelve(DP_SucRecaudacion.SelectedItem.Value, DP_Comuna.SelectedItem.Value)

        txt_GESIdZona.Text = 0
        txt_GESZona.Text = ""
        For Each zon1 In Temporal_zon
            txt_GESIdZona.Text = IIf(IsNothing(zon1.id_zon), 0, zon1.id_zon)
            txt_GESZona.Text = IIf(IsNothing(zon1.zon_des), "", zon1.zon_des)
        Next

        Temporal_zon = Nothing
    End Sub

    Protected Sub IB_Seleccionar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Seleccionar.Click

        Try

            For I = 0 To GV_Clientes.Rows.Count - 1

                Dim GV As GridView

                GV = CType(GV_Clientes.Rows(I).FindControl("GV_Doctos"), GridView)

                For x = 0 To GV.Rows.Count - 1

                    CType(GV.Rows(x).FindControl("CHB_SelDocto"), CheckBox).Checked = True

                Next

            Next

            UP_DEUCLI.Update()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DP_CCO_DSD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_CCO_DSD.SelectedIndexChanged

        Try

            Txt_CCO_DSD.Text = DP_CCO_DSD.SelectedValue


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DP_CCO_HTA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_CCO_HTA.SelectedIndexChanged

        Try

            Txt_CCO_HTA.Text = DP_CCO_HTA.SelectedValue

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub LB_Refrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Refrescar.Click

        Try

            CargaDeudores()

            If ID_GV_Contactos.Value <> "" Then
                MarcaGrilla_GV_Contactos(CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("GV_Contactos"), GridView))
            End If


            If RutDeu > 0 Then

                'Llena Grilla Clientes/Deudores Asociados a ente a Cobrar, cargamos de nuevo los clientes y doctos y lo volvemos a marcar
                GV_Clientes.DataSource = CBZ.DocumentosAGestionar_RetornaCliDeuAsociado(1, 999, "0000", "9999", Format(RutDeu, VAR.FMT_RUT), "CLIENTE")
                GV_Clientes.DataBind()

                If ID_GV_CLIENTE.Value <> "" Then
                    MarcaGrilla_GV_Doctos(CType(GV_Clientes.Rows(ID_GV_CLIENTE.Value).FindControl("GV_Doctos"), GridView))
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DP_Depto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_Depto.SelectedIndexChanged
        
        If DP_Depto.SelectedIndex > 0 Then
            CG.MunicipioDevuelve(DP_Depto.SelectedValue, True, DP_Ciudad)
            DP_Ciudad.ClearSelection()
            DP_Comuna.ClearSelection()
        End If

    End Sub

    Protected Sub DP_SucRecaudacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DP_SucRecaudacion.SelectedIndexChanged
        CG.RegionDevuelve(DP_SucRecaudacion.SelectedValue, DP_Depto)
    End Sub

#End Region

#Region "Sub y Function"

    Private Sub OrdenDeudores()
        Select Case Dp_Orden_GV_DEUCLI.SelectedItem.Value
            Case 1
                CampoOrden = "Rut"
            Case 2
                CampoOrden = "NombreDeudor"
            Case 3
                CampoOrden = "PAGO"
            Case 4
                CampoOrden = "SOLO_HOY"
            Case 5
                CampoOrden = "FECHA_SEGUIMIENTO"
            Case 6
                CampoOrden = "MontoDocto"
            Case 7
                CampoOrden = "COBRANZA_ANTICIPADA"
            Case 8
                CampoOrden = "NO_RECAUDADO"
        End Select
    End Sub

    Protected Sub RefrescaPantalla(ByVal Deudor As Boolean, _
                                   ByVal Cliente As Boolean, _
                                   ByVal Datos_Gestion As Boolean)

        If Deudor Then
            GV_DEUCLI.DataSource = Nothing
            GV_DEUCLI.DataBind()
        End If

        If Cliente Then
            GV_Clientes.DataSource = Nothing
            GV_Clientes.DataBind()
        End If

        If Datos_Gestion Then
            'Pago
            txt_FechaPago.Text = ""
            Txt_HoraPagoDde.Text = ""
            Txt_HoraPagoHta.Text = ""
            'ProxGestion
            txt_FechaProxGestion.Text = ""
            txt_HoraProxGestion.Text = ""
            'Sucursales
            DP_SucCobranza2.ClearSelection()
            DP_SucRecaudacion.ClearSelection()
            'Codigos de Cobranza, A recaudar y Confirmar Horario
            DP_CodCobranza.DataSource = CBZ.CodigoCobranza_RetornaGestionar(1) 'Retorna Codigos de Cobranza
            DP_CodCobranza.DataTextField = "descripcion"
            DP_CodCobranza.DataValueField = "id_cco"
            DP_CodCobranza.DataBind()
            DP_CodCobranza.ClearSelection()
            CBX_ARecaudar.Enabled = False
            CBX_ARecaudar.Checked = False
            CBX_ConfirmarHorario.Enabled = False
            CBX_ConfirmarHorario.Checked = False
            'Ciudad, Comuna y Zona
            DP_Ciudad.ClearSelection()
            DP_Comuna.ClearSelection()
            txt_GESZona.Text = ""
            txt_GESIdZona.Text = ""
            'Dirección
            Txt_direccion.Text = ""
            'Documentos Necesarios para retirar pago
            txt_DoctoNecGestion.Text = ""
            'Observación Gestión
            txt_ObservacionGestion.Text = ""
            'A la orden
            RB_Banco.Checked = True
            'RB_Cliente.Checked = False
            ChB_GestionPendiente.Checked = False
            HabilitaCampoDeGestion(False)

        End If


        UP_DEUCLI.Update()
        UP_Gestion.Update()

    End Sub

    Public Sub RetornaDatosDocto(ByVal RutDeudor As String, ByVal RutCliente As String, ByVal Gv As GridView)

        Dim Sesion As New ClsSession.ClsSession
        Dim TipoDocto As Int16
        Dim NroOperacion As Int64
        Dim NroDocto As String
        Dim Cuota As Int16

        Try

            NroDocto = Gv.Rows(ID_GV_Doctos.Value).Cells(2).Text
            Cuota = Gv.Rows(ID_GV_Doctos.Value).Cells(3).Text
            NroOperacion = CType(Gv.Rows(ID_GV_Doctos.Value).FindControl("id_opo"), Label).Text

            Sesion.NroPaginacion = 0

            'Se agrega rangoamplio de monto de documento
            Dim Temporal_doc = PGO.DocumentosOtorgagosPagos_RetornaDoctos(RutCliente, RutCliente, RutDeudor, RutDeudor, _
                                                                         NroOperacion, NroOperacion, _
                                                                         1, 999, _
                                                                         NroDocto, NroDocto, _
                                                                         Cuota, Cuota, _
                                                                         "01/01/1900", "01/01/3000", _
                                                                         1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)

            For Each datosdoctos In Temporal_doc


                'Marca Código de Cobranza
                DP_CodCobranza.SelectedValue = datosdoctos.id_cco

            Next



        Catch ex As Exception

        End Try

    End Sub

    Public Sub RetornaDoctos_GV_Doctos(ByVal rut_deudor As String, ByVal rut_cliente As String, ByVal GV_Doctos As GridView)

        Try

            Dim Entro_NoRecaudado As Boolean

            'If IsNothing(Collection_Cobranza2) Then
            Collection_Cobranza2 = CBZ.DocumentosAGestionar_RetornaDoctosGestionar(rut_cliente, rut_deudor)
            'End If


            GV_Doctos.DataSource = Collection_Cobranza2
            GV_Doctos.DataBind()

            'Set Colores Estado Doccumentos
            For i = 0 To GV_Doctos.Rows.Count - 1
                Dim image1, image2, image3, image4 As Image

                'Gestión Pendientes
                If Collection_Cobranza2.Item(i + 1).dor_hor_prx = Nothing Then
                    image1 = GV_Doctos.Rows(i).Cells(7).FindControl("image1")
                    image1.ImageUrl = "~/Imagenes/iconos/desactivada.gif"
                Else
                    image1 = GV_Doctos.Rows(i).Cells(7).FindControl("image1")
                    image1.ImageUrl = "~/Imagenes/iconos/verde.gif"
                End If

                'Documentos Pendientes
                If Collection_Cobranza2.Item(i + 1).cantdrc = 0 Then
                    image2 = GV_Doctos.Rows(i).Cells(7).FindControl("image2")
                    image2.ImageUrl = "~/Imagenes/iconos/desactivada.gif"
                Else
                    image2 = GV_Doctos.Rows(i).Cells(7).FindControl("image2")
                    image2.ImageUrl = "~/Imagenes/iconos/violeta.gif"
                End If

                'Pago en Línea
                If Collection_Cobranza2.Item(i + 1).pago = 0 Then
                    image3 = GV_Doctos.Rows(i).Cells(7).FindControl("image3")
                    image3.ImageUrl = "~/Imagenes/iconos/desactivada.gif"
                Else
                    image3 = GV_Doctos.Rows(i).Cells(7).FindControl("image3")
                    image3.ImageUrl = "~/Imagenes/iconos/rojo.gif"
                End If

                'No Recaudado
                Dim Temporal_drc = CBZ.RetornaUltimo_DocumentosARecaudar(Collection_Cobranza2.Item(i + 1).id_doc)
                'If Collection_Cobranza2.Item(i + 1).drc_est_rec = Nothing Then
                Entro_NoRecaudado = False
                For Each c In Temporal_drc
                    Entro_NoRecaudado = True
                    If IsNothing(c.id_P_0103) Then
                        image4 = GV_Doctos.Rows(i).Cells(7).FindControl("image4")
                        image4.ImageUrl = "~/Imagenes/iconos/desactivada.gif"
                    Else
                        image4 = GV_Doctos.Rows(i).Cells(7).FindControl("image4")
                        image4.ImageUrl = "~/Imagenes/iconos/amarillo.gif"
                    End If
                Next
                If Not Entro_NoRecaudado Then
                    image4 = GV_Doctos.Rows(i).Cells(7).FindControl("image4")
                    image4.ImageUrl = "~/Imagenes/iconos/desactivada.gif"
                End If


            Next
        Catch ex As Exception
            Msj.Mensaje(Me, "Error", ex.Message, ClsMensaje.TipoDeMensaje._Error, Nothing, False)
        End Try

    End Sub

    Private Function ValidaGuardaGestion() As Boolean

        Try

            ValidaGuardaGestion = False

            'Seleccion de Contacto
            If ID_GV_Contactos.Value = "" Then
                Msj.Mensaje(Me, "Atención", "Seleccione un contacto", 2)
                Exit Function
            End If

            'Fecha Prox. Gestión < ó = a Hoy
            If txt_FechaProxGestion.Text = "" Then
                Msj.Mensaje(Me, "Atención", "Ingrese Fecha de próxima gestión", 2)
                Exit Function
            End If

            If Not IsDate(txt_FechaProxGestion.Text) Then
                txt_FechaProxGestion.Text = ""
                Msj.Mensaje(Page, "Atención", "Fecha de próxima gestión errónea", 2)
                Exit Function
            End If

            If Not IsDate(txt_HoraProxGestion.Text) Then
                Msj.Mensaje(Page, "Atención", "Hora errónea", 2)
                txt_HoraProxGestion.Text = ""
                Exit Function
            End If

            If DateDiff(DateInterval.Day, Date.Now, CDate(txt_FechaProxGestion.Text)) < 0 Then
                txt_FechaProxGestion.Text = ""
                Msj.Mensaje(Page, "Atención", "Fecha de próxima gestión no puede ser menor a hoy", 2)
                Exit Function
            End If

            'Validación Fecha de Pago
            If Trim(txt_FechaPago.Text) <> "" Then
                If Not IsDate(txt_FechaPago.Text) Then
                    Msj.Mensaje(Page, "Atención", "Fecha de pago errónea", 2)
                    txt_FechaPago.Text = ""
                    Exit Function
                End If


                If Trim(Txt_HoraPagoDde.Text) = "" Then

                    Msj.Mensaje(Me, "Atención", "Debe ingresar hora de pago desde", 2)
                    Exit Function
                Else
                    If Not IsDate(Txt_HoraPagoDde.Text) Then
                        Msj.Mensaje(Page, "Atención", "Hora de pago desde errónea", 2)
                        Txt_HoraPagoDde.Text = ""
                        Exit Function
                    End If
                End If

                If Trim(Txt_HoraPagoHta.Text) = "" Then
                    Msj.Mensaje(Me, "Atención", "Debe ingresar hora de pago hasta", 2)
                    Exit Function
                Else
                    If Not IsDate(Txt_HoraPagoHta.Text) Then
                        Txt_HoraPagoHta.Text = ""
                        Msj.Mensaje(Page, "Atención", "Hora de pago hasta errónea", 2)
                        Exit Function
                    End If
                End If

                'If DateTime.Parse(txt_FechaProxGestion.Text).ToShortDateString > DateTime.Parse(txt_FechaPago.Text).ToShortDateString Then
                '    txt_FechaPago.Text = ""
                '    Msj.Mensaje(Page, "Atención", "Fecha de próxima gestión no puede ser mayor a la fecha de pago", 2)
                '    Exit Function
                'End If

                'Valida Fecha Prox. Gestión > a Fecha de Pago
                If DP_CodCobranza.SelectedValue <> 12 Then

                    If DateDiff(DateInterval.Day, CDate(txt_FechaProxGestion.Text), CDate(txt_FechaPago.Text)) < 0 Then 'Trim(txt_FechaPago.Text) <> "" And (CDate(txt_FechaProxGestion.Text) >= CDate(IIf(Trim(txt_FechaPago.Text) = "", "01/01/1900", txt_FechaPago.Text))) Then 
                        Msj.Mensaje(Me, "Atención", "Fecha prox. gestión es mayor a fecha de pago", 2)
                        Exit Function
                    End If
                End If

            End If


            Dim Horadesde As String = Trim(Txt_HoraPagoDde.Text)
            Dim Horahasta As String = Trim(Txt_HoraPagoHta.Text)

            If CDate("01/01/1900 " & Horadesde) > CDate("01/01/1900 " & Horahasta) Then
                Msj.Mensaje(Me, "Atención", "Hora desde debe ser menor a hora hasta", 2)
                Exit Function
            End If

            If DP_Ciudad.SelectedIndex <= 0 Then
                Msj.Mensaje(Me, "Atención", "Debe Seleccionar una Municipio y Localidad", 2)
                Exit Function
            Else
                If DP_Comuna.SelectedIndex <= 0 Then
                    Msj.Mensaje(Me, "Atención", "Debe Seleccionar una Municipio", 2)
                    Exit Function
                Else
                    If Txt_direccion.Text = "" Then
                        Msj.Mensaje(Me, "Atención", "Debe Ingresar la Dirección de Pago", 2)
                        Exit Function
                    End If
                End If

            End If

            'Validación Seleccion de Documento
            Dim CuentaDoctosSeleccionados As Integer = 0

            For X = 0 To GV_Clientes.Rows.Count - 1

                Dim GV_Doctos As GridView = CType(GV_Clientes.Rows(X).FindControl("GV_Doctos"), GridView)

                For i = 0 To GV_Doctos.Rows.Count - 1

                    Dim varCHKBox As CheckBox

                    'Busca Control CheckBox
                    varCHKBox = GV_Doctos.Rows(i).FindControl("CHB_SelDocto")

                    'Valida Seleccion
                    If varCHKBox.Checked Then
                        CuentaDoctosSeleccionados = 1
                        Exit For
                    End If

                Next

            Next

            If CuentaDoctosSeleccionados = 0 Then
                Msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un documento", 2)

                Exit Function
            End If

            'Valida si no existe fecha de pago ingrese Observación
            If Trim(txt_FechaPago.Text) = "" And Trim(txt_ObservacionGestion.Text) = "" Then
                Msj.Mensaje(Me, "Atención", "Debe ingresar al menos observación de gestión", 2)

                Exit Function
            End If

            'Valida Dirección de Pago si ingresa fecha de pago
            If Trim(txt_FechaPago.Text) <> "" And (DP_Ciudad.SelectedIndex = 0 Or DP_Comuna.SelectedIndex = 0 Or _
                                                   DP_Comuna.SelectedIndex = 0 Or Txt_direccion.Text = "") Then
                Msj.Mensaje(Me, "Atención", "Debe seleccionar al menos un documento", 2)
                Msj.Mensaje(Me, "Atención", "Al ingresar fecha de pago debe ingresar tambien :" & Chr(13) & "- Ciudad" _
                                                                           & Chr(13) & "- Comuna" _
                                                                           & Chr(13) & "- Dirección de Pago", 2)
                Exit Function
            End If


            ValidaGuardaGestion = True

        Catch ex As Exception
            Msj.Mensaje(Me, "Gestion de Cobranza", "Error: " & ex.Message, 1)
        End Try

    End Function

    Private Sub HabilitaCampoDeGestion(ByVal Estado As Boolean)

        Try

            If Estado Then

                txt_FechaPago.ReadOnly = False
                Txt_HoraPagoDde.ReadOnly = False
                Txt_HoraPagoHta.ReadOnly = False

                txt_FechaProxGestion.ReadOnly = False
                txt_HoraProxGestion.ReadOnly = False

                DP_SucCobranza2.Enabled = True
                DP_SucRecaudacion.Enabled = True
                DP_CodCobranza.Enabled = True

                CBX_ARecaudar.Enabled = True
                CBX_ConfirmarHorario.Enabled = True

                DP_Depto.Enabled = True
                DP_Ciudad.Enabled = True
                DP_Comuna.Enabled = True

                txt_GESIdZona.ReadOnly = False
                txt_GESZona.ReadOnly = False

                Txt_direccion.ReadOnly = False
                txt_DoctoNecGestion.ReadOnly = False
                txt_ObservacionGestion.ReadOnly = False
                RB_Banco.Enabled = True
                ChB_GestionPendiente.Enabled = True


                'Cambio de estilo
                txt_FechaPago.CssClass = "clsTxt"
                Txt_HoraPagoDde.CssClass = "clsTxt"
                Txt_HoraPagoHta.CssClass = "clsTxt"

                txt_FechaProxGestion.CssClass = "clsMandatorio"
                MaskedEditExtender1.Enabled = True
                CalendarExtender1.Enabled = True

                txt_HoraProxGestion.Enabled = True
                'MaskedEditExtender2.Enabled = True

                txt_FechaPago_MaskedEditExtender.Enabled = True
                CalendarExtender2.Enabled = True

                txt_HoraProxGestion_MaskedEditExtender.Enabled = True
                Txt_HoraPagoHta_MaskedEditExtender.enabled = True
                Txt_HoraPagoDde_MaskedEditExtender.enabled = True
                txt_HoraProxGestion.CssClass = "clsMandatorio"

                DP_SucCobranza2.CssClass = "clsMandatorio"
                DP_SucRecaudacion.CssClass = "clsMandatorio"
                DP_CodCobranza.CssClass = "clsMandatorio"

                DP_Depto.CssClass = "clsMandatorio"
                DP_Ciudad.CssClass = "clsMandatorio"
                DP_Comuna.CssClass = "clsMandatorio"

                Txt_direccion.CssClass = "clsMandatorio"
                txt_DoctoNecGestion.CssClass = "clsTxt"
                txt_ObservacionGestion.CssClass = "clsMandatorio"

                IB_GuardaGestion.Enabled = True

            Else

                txt_FechaPago.ReadOnly = True
                Txt_HoraPagoDde.ReadOnly = True
                Txt_HoraPagoHta.ReadOnly = True

                txt_FechaProxGestion.ReadOnly = True
                txt_HoraProxGestion.ReadOnly = True

                DP_SucCobranza2.Enabled = False
                DP_SucRecaudacion.Enabled = False
                DP_CodCobranza.Enabled = False

                CBX_ARecaudar.Enabled = False
                CBX_ConfirmarHorario.Enabled = False

                DP_Depto.Enabled = False
                DP_Ciudad.Enabled = False
                DP_Comuna.Enabled = False

                txt_GESIdZona.ReadOnly = True
                txt_GESZona.ReadOnly = True

                Txt_direccion.ReadOnly = True
                txt_DoctoNecGestion.ReadOnly = True
                txt_ObservacionGestion.ReadOnly = True

                RB_Banco.Enabled = False
                ChB_GestionPendiente.Enabled = False

                txt_FechaPago.CssClass = "clsDisabled"
                Txt_HoraPagoDde.CssClass = "clsDisabled"
                Txt_HoraPagoHta.CssClass = "clsDisabled"

                txt_FechaProxGestion.CssClass = "clsDisabled"
                txt_HoraProxGestion.CssClass = "clsDisabled"
                MaskedEditExtender1.Enabled = False
                CalendarExtender1.Enabled = False

                Txt_HoraPagoHta_MaskedEditExtender.enabled = False
                Txt_HoraPagoDde_MaskedEditExtender.enabled = False

                'MaskedEditExtender2.Enabled = False
                txt_FechaPago_MaskedEditExtender.Enabled = False
                CalendarExtender2.Enabled = False
                txt_HoraProxGestion.Enabled = False

                txt_HoraProxGestion_MaskedEditExtender.Enabled = False

                DP_SucCobranza2.CssClass = "clsDisabled"
                DP_SucRecaudacion.CssClass = "clsDisabled"
                DP_CodCobranza.CssClass = "clsDisabled"

                DP_Depto.CssClass = "clsDisabled"
                DP_Ciudad.CssClass = "clsDisabled"
                DP_Comuna.CssClass = "clsDisabled"

                Txt_direccion.CssClass = "clsDisabled"
                txt_DoctoNecGestion.CssClass = "clsDisabled"
                txt_ObservacionGestion.CssClass = "clsDisabled"

                IB_GuardaGestion.Enabled = False

            End If

            UP_Gestion.Update()
            UP_Botonera.Update()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CreaEventosParaGridContactos(ByVal GV As GridView, ByVal pos As Integer)

        For I = 0 To GV.Rows.Count - 1
            'GV.Rows(I).Attributes.Add("onMouseOver", "J_RolClass_TablaCob(" & GV.ClientID & ", 'selectableCob')")
            'GV.Rows(I).Attributes.Add("onMouseOut", "J_RolClass_TablaCob(" & GV.ClientID & ", 'formatableCob')")
            'GV.Rows(I).Attributes.Add("onClick", "CelClick_GV_Contactos(" & GV.ClientID & ", 'clicktableCob', 'formatableCob', 'selectableCob')")
            Dim img_ver As ImageButton = CType(GV.Rows(I).FindControl("Img_Ver"), ImageButton)

            img_ver.Attributes.Add("onClick", "CelClick_GV_Contactos(" & pos & ", " & I & ", " & GV.Rows(I).Cells(0).Text.Trim() & ");")

        Next

    End Sub

    Private Sub CreaEventosParaGridDoctos(ByVal GV As GridView, ByVal IndiceCli As Integer, ByVal RutCli As String)

        For I = 0 To GV.Rows.Count - 1

            Dim NroOperacion As Integer
            Dim NroDocto As String
            Dim idDocto As Integer
            Dim Cuota As Integer
            Dim IB_Verif As Image
            Dim IB_Gesti As Image
            Dim IB_Docto As Image
            Dim IB_Carta As Image
            Dim prueba As String

            NroOperacion = CType(GV.Rows(I).FindControl("id_opo"), Label).Text
            idDocto = CType(GV.Rows(I).FindControl("id_doc"), Label).Text

            IB_Verif = GV.Rows(I).FindControl("Img_Veri")
            IB_Gesti = GV.Rows(I).FindControl("Img_GestionesAnt")
            IB_Docto = GV.Rows(I).FindControl("Img_Docto")
            IB_Carta = GV.Rows(I).FindControl("Img_Carta")

            NroDocto = GV.Rows(I).Cells(2).Text
            Cuota = GV.Rows(I).Cells(3).Text

            Dim imagen_factura As String
            Dim Imagen_Carta As String

            prueba = CBZ.Retorna_url_img(idDocto).dsi_img_url 'GV.Rows(I).FindControl("img_doc")
            imagen_factura = prueba '.Text.Trim()
            Imagen_Carta = idDocto

            IB_Verif.Attributes.Add("onMouseOver", "showToolTip(event,'" & idDocto & "')")

            IB_Verif.Attributes.Add("onMouseOut", "hideTooltip(event)")

            IB_Gesti.Attributes.Add("onClick", "WinOpen(2, 'GestionesAnteriores.aspx?deu=" & Format(RutDeu, VAR.FMT_RUT) & "&cli=" & RutCli & "&ope=" & NroOperacion & "&doc=" & NroDocto & "&cuo=" & Cuota & "', 'Gestiones', 700,600, 50,50);")
            IB_Docto.Attributes.Add("onclick", "WinOpen(1, 'VerArch_DoctoDig.aspx?id=" & idDocto & "', 'Documentos_Adjuntos', 650, 350, 15, 15)")
            IB_Carta.Attributes.Add("onClick", "var x=window.showModalDialog('detalle_doctos_ges.aspx?id= " & Imagen_Carta & "', window, 'scroll:no;status:off;dialogWidth:1000px;dialogHeight:1000px;dialogLeft:50px;dialogTop:50px');")
            IB_Gesti.Attributes.Add("Style", "cursor: hand")
            IB_Docto.Attributes.Add("Style", "cursor: hand")
            IB_Carta.Attributes.Add("Style", "cursor: hand")

        Next

    End Sub

    Protected Sub CargaDeudores()

        Pos = 0

        FC.SortCollection(Deudores, CampoOrden, True)

        GV_DEUCLI.DataSource = Deudores
        GV_DEUCLI.DataBind()

        If Deudores.Count <= 0 Then

            ID_GV_DEUDOR.Value = ""
            ID_GV_CLIENTE.Value = ""
            ID_GV_CLIENTE.Value = ""

            Msj.Mensaje(Me, "Cobranza", "No se encontraron Pagadores que tengan Documentos segun criterio de busqueda", ClsMensaje.TipoDeMensaje._Exclamacion)

        End If

    End Sub

    Private Sub CargaGrillaDeudores()

        'Llena Coleccion en variable de sesion

        Dim cco_dsd As String
        Dim cco_hta As String

        Dim est_dsd As Integer
        Dim est_hta As Integer

        If DP_CCO_DSD.SelectedIndex > 0 Then

            cco_dsd = DP_CCO_DSD.SelectedItem.Text.Trim

            If DP_CCO_HTA.SelectedIndex > 0 Then
                cco_hta = DP_CCO_HTA.SelectedItem.Text.Trim
            Else
                Msj.Mensaje(Me, "Atención", "No se tomara codigo de cobranza por no existir codigo hasta", ClsMensaje.TipoDeMensaje._Exclamacion)
                cco_dsd = "0000"
                cco_hta = "9999"
            End If

        Else
            cco_dsd = "0000"
            cco_hta = "9999"
        End If


        If DP_CCO_HTA.SelectedIndex > 0 Then
            cco_hta = DP_CCO_HTA.SelectedItem.Text.Trim
        Else
            cco_dsd = "0000"
            cco_hta = "9999"
        End If

        If dp_EstadoDoctos.SelectedIndex > 0 Then
            est_dsd = dp_EstadoDoctos.SelectedValue
            est_hta = dp_EstadoDoctos.SelectedValue
        Else
            est_dsd = 0
            est_hta = 4
        End If

        'Deudores = CG.DocumentosAGestionar_RetornaCliDeuACobrar(est_dsd, est_hta, cco_dsd, cco_hta, "D", CodEje, "DEUDOR")
        Deudores = CBZ.DocumentosAGestionar_RetornaCliDeuACobrar(est_dsd, est_hta, cco_dsd, cco_hta, "D", Ejecutivo, "DEUDOR")

        OrdenDeudores()

        CargaDeudores()

    End Sub

    Public Sub MarcaGrilla_GV_Contactos(ByVal GV As GridView)

        Dim _rut_deudor As String = CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("Rut_Deudor"), Label).Text.ToString
        Dim rut_deudor As String = Format(CLng(_rut_deudor), VAR.FMT_RUT)
        Dim _Deudor As Label = CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("Deudor"), Label)

        RutDeu = rut_deudor
        _Deudor.ForeColor = Drawing.Color.Red
        _Deudor.Font.Bold = True

        Dim _cpe As AjaxControlToolkit.CollapsiblePanelExtender
        _cpe = CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("cpe"), AjaxControlToolkit.CollapsiblePanelExtender)

        If _cpe.Collapsed Then
            _cpe.Collapsed = False
        Else
            _cpe.Collapsed = True
        End If

        For I = 0 To GV.Rows.Count - 1
            If I = CInt(ID_GV_Contactos.Value) Then
                GV.Rows(I).CssClass = "selectable"
            Else
                GV.Rows(I).CssClass = Nothing
            End If
        Next

    End Sub

    Public Sub MarcaGrilla_GV_Doctos(ByVal GV_Doctos As GridView)

        Dim _rut_cliente As String = CType(GV_Clientes.Rows(ID_GV_CLIENTE.Value).FindControl("Rut_Cliente"), Label).Text.ToString
        Dim rut_cliente As String = Format(CLng(_rut_cliente), VAR.FMT_RUT)


        RutCli = _rut_cliente

        Dim _cpe As AjaxControlToolkit.CollapsiblePanelExtender
        _cpe = CType(GV_Clientes.Rows(ID_GV_CLIENTE.Value).FindControl("cpc"), AjaxControlToolkit.CollapsiblePanelExtender)

        If _cpe.Collapsed Then
            _cpe.Collapsed = False
        Else
            _cpe.Collapsed = True
        End If

        'CreaEventosParaGridDoctos(CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("GV_Contactos"), GridView))

        If ID_GV_Doctos.Value <> "" Then

            For I = 0 To GV_Doctos.Rows.Count - 1
                If I = CInt(ID_GV_Doctos.Value) Then
                    GV_Doctos.Rows(I).CssClass = "selectable"
                Else
                    GV_Doctos.Rows(I).CssClass = Nothing
                End If
            Next

        End If

    End Sub

#End Region

#Region "Manejo de GridView"

    Protected Sub GV_DEUCLI_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_DEUCLI.PageIndexChanging

        ID_GV_DEUDOR.Value = ""
        ID_GV_CLIENTE.Value = ""
        RefrescaPantalla(True, True, True)

        GV_DEUCLI.PageIndex = e.NewPageIndex
        CargaDeudores()


    End Sub

    Protected Sub GV_DEUCLI_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_DEUCLI.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim _Deudor As Label = CType(e.Row.FindControl("Deudor"), Label)
            Dim _rut_deudor As String = CType(e.Row.FindControl("Rut_Deudor"), Label).Text.ToString
            Dim _NombreDeudor As String = CType(e.Row.FindControl("Nombre_Deudor"), Label).Text.ToString
            Dim _Cantidad As String = CType(e.Row.FindControl("Cantidad"), Label).Text.ToString
            Dim _Sumatoria As String = CType(e.Row.FindControl("Sumatoria"), Label).Text.ToString
            Dim _Gestion As String = CType(e.Row.FindControl("Gestion"), Label).Text.ToString

            Dim Rut As String = Format(CLng(_rut_deudor), FMT.FCMSD) & "-" & FC.Vrut(CLng(_rut_deudor))

            _Deudor.Text = "Nit: " & Rut & _
                           " Nombre: " & _NombreDeudor & _
                           " Cantidad: " & Format(CLng(_Cantidad), FMT.FCMSD) & _
                           " Total Pagador: " & Format(CLng(_Sumatoria), FMT.FCMSD)


            Dim GV_Contactos As GridView = CType(e.Row.FindControl("GV_Contactos"), GridView)

            GV_Contactos.DataSource = CBZ.Contactos_RetornaContactosGestion("DEUDOR", _rut_deudor, 0)
            GV_Contactos.DataBind()

            If GV_Contactos.Rows.Count > 0 Then
                'e.Row.Attributes.Add("onClick", "CelClick_GV_DEUCLI(" & Pos & ")")
                CreaEventosParaGridContactos(GV_Contactos, e.Row.RowIndex)
            Else
                'e.Row.Attributes.Add("onClick", "CelClick_GV_DEUDOR(" & Pos & ", " & CInt(_rut_deudor) & ")")
            End If

            If _Gestion > 0 Then
                e.Row.BackColor = Drawing.Color.PaleGreen
            End If

            Pos += 1

        End If

    End Sub

    Protected Sub GV_Clientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Clientes.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onClick", "CelClick_GV_CLIENTE(" & Cli & ")")


            Dim _Cliente As Label = CType(e.Row.FindControl("Cliente"), Label)
            Dim _rut_Cliente As String = CType(e.Row.FindControl("Rut_Cliente"), Label).Text.ToString
            Dim _NombreCliente As String = CType(e.Row.FindControl("Nombre_Cliente"), Label).Text.ToString
            Dim _Cantidad As String = CType(e.Row.FindControl("Cantidad"), Label).Text.ToString
            Dim _Sumatoria As String = CType(e.Row.FindControl("Sumatoria"), Label).Text.ToString
            'Dim _Gestion As String = CType(e.Row.FindControl("Gestion"), Label).Text.ToString

            _Cliente.Text = "Nit: " & Format(CLng(_rut_Cliente), FMT.FCMSD) & "-" & FC.Vrut(CLng(_rut_Cliente)) & _
                           " Nombre: " & _NombreCliente & _
                           " Cantidad: " & Format(CLng(_Cantidad), FMT.FCMSD) & _
                           " Total Cliente: " & Format(CLng(_Sumatoria), FMT.FCMSD)

            Dim _GV_Doctos As GridView = CType(e.Row.FindControl("GV_Doctos"), GridView)

            RetornaDoctos_GV_Doctos(Format(RutDeu, VAR.FMT_RUT), _rut_Cliente.Trim, _GV_Doctos)
            CreaEventosParaGridDoctos(_GV_Doctos, Cli, _rut_Cliente.Trim)


            Cli += 1

        End If

    End Sub

    Protected Sub Busqueda_GV_Contactos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Busqueda_GV_Contactos.Click

        Try

            CargaDeudores()
            MarcaGrilla_GV_Contactos(CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("GV_Contactos"), GridView))

            RefrescaPantalla(False, True, True)
            HabilitaCampoDeGestion(True)


            'Llena Label Deudor Seleccionado

            'Asigna Observación Deudor
            Dim deu1 = CG.DeudorDevuelvePorRut(RutDeu)

            deu1 = Nothing

            'Retorna Dias de Pago por Deudor
            If RutDeu <> 0 Then

                IB_Contactos.Attributes.Add("onClick", "var x=window.showModalDialog('../../Contactos/Contactos.aspx?Rut= " & RutDeu & "', window, 'scroll:no;status:off;dialogWidth:600px;dialogHeight:630px;dialogLeft:400px;dialogTop:200px');")
                IB_Dias_Pago.Attributes.Add("onClick", "WinOpen(2, 'DiasDePago.aspx?RutDeu=" & RutDeu & "', 'DiasDePago', 500, 400, 200, 200);")
                IB_Obs_Deudor.Attributes.Add("onClick", "WinOpen(2, 'ObservacionesDeudor.aspx?RutDeu=" & RutDeu & "', 'ObservacionDeu', 360, 400, 200, 200);")

                IB_Contactos.Enabled = True
                IB_Dias_Pago.Enabled = True
                IB_Obs_Deudor.Enabled = True

            Else

                IB_Contactos.Attributes.Remove("onClick")
                IB_Dias_Pago.Attributes.Remove("onClick")
                IB_Obs_Deudor.Attributes.Remove("onClick")

                IB_Contactos.Enabled = False
                IB_Dias_Pago.Enabled = False
                IB_Obs_Deudor.Enabled = False

            End If

            'Llena Grilla Clientes/Deudores Asociados a ente a Cobrar
            Cli = 0
            GV_Clientes.DataSource = CBZ.DocumentosAGestionar_RetornaCliDeuAsociado(1, 999, "0000", "9999", Format(RutDeu, VAR.FMT_RUT), "CLIENTE")
            GV_Clientes.DataBind()

            'Retorna Direcciones,Ciudad y Comuna y Zona
            Dim Temporal_ddi = CBZ.DireccionDeudorRecaudacion_Devolver(0, 9999999999, Format(RutDeu, VAR.FMT_RUT), Format(RutDeu, VAR.FMT_RUT), 2)

            RB_Direcciones.DataTextField = "gsn_dir_cbz"
            RB_Direcciones.DataValueField = "id_ddi"
            RB_Direcciones.DataSource = Temporal_ddi
            RB_Direcciones.DataBind()
            'Cambia a AcordionPanel de Clientes
            'Accordion1.SelectedIndex = 1
            UP_DEUCLI.Update()
            UP_Botonera.Update()
            UP_Gestion.Update()


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Busqueda_GV_Doctos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Busqueda_GV_Doctos.Click

        Try


        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Botonera"

    Protected Sub IB_GuardaGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_GuardaGestion.Click

        'Valida Info Ingresada Antes de Guardar Gestión
        If Not ValidaGuardaGestion() Then
            Exit Sub
        End If

        'Guardar Gestión
        Msj.Mensaje(Me, "Gestion de Cobranza", "¿Está Seguro de querer Guardar la Gestión?", ClsMensaje.TipoDeMensaje._Confirmacion, LB_Guardar.UniqueID, False)

    End Sub

    Protected Sub IB_CancelarGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_CancelarGestion.Click
        Txt_CCO_DSD.Text = ""
        Txt_CCO_HTA.Text = ""
        txt_FechaProxGestion.Text = ""
        txt_HoraProxGestion.Text = ""
        txt_FechaPago.Text = ""
        Txt_HoraPagoDde.Text = ""
        Txt_HoraPagoHta.Text = ""
        txt_GESIdZona.Text = ""
        txt_GESZona.Text = ""
        Txt_direccion.Text = ""
        DP_SucCobranza2.ClearSelection()
        DP_SucRecaudacion.ClearSelection()
        DP_CodCobranza.ClearSelection()
        dp_EstadoDoctos.ClearSelection()
        Dp_Orden_GV_DEUCLI.ClearSelection()
        DP_CCO_HTA.ClearSelection()
        DP_CCO_DSD.ClearSelection()
        ID_GV_DEUDOR.Value = ""
        ID_GV_CLIENTE.Value = ""
        RefrescaPantalla(True, True, True)
    End Sub

    Protected Sub IB_VolverGestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_VolverGestion.Click
        'Para que actualice montos y grilla en call_telefonicas
        'RW.CloseModal(Me, "ctl00$ContentPlaceHolder1$LinkB_Refresca")

    End Sub

    Protected Sub LB_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LB_Guardar.Click

        Try

            'Cargamos de nuevo los deudores y contactos y lo volvemos a marcar
            If ID_GV_DEUDOR.Value <> "" Then

                CargaDeudores()

                If ID_GV_Contactos.Value <> "" Then
                    MarcaGrilla_GV_Contactos(CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("GV_Contactos"), GridView))
                End If

            End If

            Dim HoraPagoDesde As String
            Dim HoraPagoHasta As String
            Dim rst As Boolean
            Dim cb_rec As Boolean
            Dim ch_pend As Integer
            Dim PP As gsn_cls

            'Ciclo que recorre Documentos Seleccionados
            For X = 0 To GV_Clientes.Rows.Count - 1

                Dim GV_Doctos As GridView = CType(GV_Clientes.Rows(X).FindControl("GV_Doctos"), GridView)

                For i = 0 To GV_Doctos.Rows.Count - 1

                    Dim varCHKBox As CheckBox

                    'Busca Control CheckBox
                    varCHKBox = GV_Doctos.Rows(i).FindControl("CHB_SelDocto")

                    'Valida Seleccion
                    If varCHKBox.Checked Then

                        Dim gsn_ingreso As New gsn_cls

                        If GV_Doctos.Rows(i).Cells(8).Text = "&nbsp;" Then
                            GV_Doctos.Rows(i).Cells(8).Text = 0
                        End If

                        Dim NroDocto As String = CType(GV_Doctos.Rows(i).FindControl("id_doc"), Label).Text
                        Dim TipDocto As Integer = CType(GV_Doctos.Rows(i).FindControl("TipDoc"), Label).Text
                        Dim FechaVto As String = GV_Doctos.Rows(i).Cells(4).Text
                        Dim SaldoCli As Double = Val(GV_Doctos.Rows(i).Cells(8).Text)
                        Dim SaldoDeu As Double = Val(GV_Doctos.Rows(i).Cells(7).Text)

                        gsn_ingreso.id_doc = NroDocto
                        gsn_ingreso.id_P_0011 = TipDocto
                        gsn_ingreso.doc_fev_rea = FechaVto
                        gsn_ingreso.doc_sdo_cli = SaldoCli
                        gsn_ingreso.doc_sdo_ddr = SaldoDeu

                        'gsn_ingreso.id_eje_cob = CodEje
                        gsn_ingreso.id_eje_cob = ClsSession.ClsSession.Ejecutivo
                        gsn_ingreso.id_cnc = ID_Contacto.Value

                        gsn_ingreso.id_cco = DP_CodCobranza.SelectedValue
                        gsn_ingreso.gsn_fec = Date.Now.ToShortDateString
                        gsn_ingreso.gsn_hor = Date.Now ' TimeOfDay

                        Dim ddi As New ddi_cls

                        If txt_FechaPago.Text <> "" Then

                            gsn_ingreso.gsn_fec_pag = IIf(Me.txt_FechaPago.Text.Trim = "", Nothing, CDate(Me.txt_FechaPago.Text.Trim))

                            HoraPagoDesde = Me.Txt_HoraPagoDde.Text.Trim
                            HoraPagoHasta = Me.Txt_HoraPagoHta.Text.Trim

                            gsn_ingreso.gsn_hor_pag_dde = CDate(gsn_ingreso.gsn_fec_pag & " " & IIf(HoraPagoDesde = ":", Nothing, HoraPagoDesde))
                            gsn_ingreso.gsn_hor_pag = CDate(gsn_ingreso.gsn_fec_pag & " " & IIf(HoraPagoHasta = ":", Nothing, HoraPagoHasta))


                            Dim rut_deudor As String = Format(RutDeu, VAR.FMT_RUT)

                            ddi.deu_ide = rut_deudor
                            ddi.ddr_dml_cbz = "S"
                            ' colocar funcion que me valide que no eciste la direccion antes de guardar!
                            If DP_Ciudad.SelectedIndex > 0 And DP_Comuna.SelectedIndex > 0 Then
                                ddi.id_cmn = Me.DP_Comuna.SelectedValue
                            Else
                                ddi.id_cmn = Nothing
                            End If

                        Else

                            gsn_ingreso.gsn_hor_pag_dde = Nothing
                            gsn_ingreso.gsn_hor_pag = Nothing

                        End If


                        Dim GV_Contactos As GridView = CType(GV_DEUCLI.Rows(ID_GV_DEUDOR.Value).FindControl("GV_Contactos"), GridView)

                        gsn_ingreso.gsn_tlf = GV_Contactos.Rows(ID_GV_Contactos.Value).Cells(3).Text.Trim
                        gsn_ingreso.gsn_fax = GV_Contactos.Rows(ID_GV_Contactos.Value).Cells(4).Text.Trim

                        gsn_ingreso.gsn_obs = Mid(Me.txt_ObservacionGestion.Text.Trim, 1, 250)
                        gsn_ingreso.gsn_obs_1 = Mid(Me.txt_ObservacionGestion.Text.Trim, 251, 250)
                        gsn_ingreso.gsn_obs_2 = Mid(Me.txt_ObservacionGestion.Text.Trim, 501, 250)

                        gsn_ingreso.gsn_doc_rtr_pag = IIf(Me.txt_DoctoNecGestion.Text.Trim = "", Nothing, Me.txt_DoctoNecGestion.Text.Trim)

                        gsn_ingreso.gsn_fec_prx = Me.txt_FechaProxGestion.Text.Trim
                        gsn_ingreso.gsn_hor_prx = CDate(Me.txt_FechaProxGestion.Text.Trim & " " & Me.txt_HoraProxGestion.Text.Trim)

                        gsn_ingreso.gsn_dir_cbz = IIf(Me.Txt_direccion.Text.Trim = "", Nothing, Me.Txt_direccion.Text.Trim)


                        If Me.RB_Banco.Checked = True Then
                            gsn_ingreso.gsn_alo = "B"
                        Else
                            gsn_ingreso.gsn_alo = "C"
                        End If

                        gsn_ingreso.gsn_alo_obs = Nothing

                        If Me.CBX_ConfirmarHorario.Checked = True Then
                            gsn_ingreso.gsn_con_hor = "S"
                        Else
                            gsn_ingreso.gsn_con_hor = "N"
                        End If

                        If Me.ChB_GestionPendiente.Checked = True Then
                            ch_pend = 1
                        Else
                            ch_pend = 0
                        End If

                        If Me.CBX_ARecaudar.Checked Then
                            cb_rec = True
                        Else
                            cb_rec = False
                        End If

                        Try

                            PP = CBZ.valida_si_direccion_existe(DP_Comuna.SelectedValue, gsn_ingreso.gsn_dir_cbz)

                            If IsNothing(PP) Then
                                gsn_ingreso.id_ddi = Nothing
                            Else
                                gsn_ingreso.id_ddi = PP.id_ddi
                            End If

                        Catch ex As Exception

                        End Try

                        rst = CBZ.GuardaGestion(gsn_ingreso, ddi, cb_rec, Me.DP_SucRecaudacion.SelectedValue, Me.DP_SucCobranza2.SelectedValue, ch_pend)


                    End If

                Next

            Next

            If rst Then

                Msj.Mensaje(Me, "Atención", "Se ha Guardado la Gestión", 2)

                HabilitaCampoDeGestion(False)
                IB_GuardaGestion.Enabled = False
                RefrescaPantalla(True, True, True)

                CargaGrillaDeudores()

            Else

                Msj.Mensaje(Me, "Atención", CBZ.descripcionconsulta, 1)

            End If

        Catch ex As Exception
            Msj.Mensaje(Me, "Error", "Se producido el siguiente error al guardar: " & ex.Message, ClsMensaje.TipoDeMensaje._Error)
        End Try

    End Sub

    Protected Sub IB_BuscarCriterios_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_BuscarCriterios.Click

        RefrescaPantalla(True, True, True)
        CargaGrillaDeudores()

    End Sub

    Protected Sub RB_Direcciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_Direcciones.SelectedIndexChanged
        Try
            Dim cargadrop As ddi_cls
            Dim x = CBZ.DireccionDeudor(RB_Direcciones.SelectedValue)
            cargadrop = x

            Txt_direccion.Text = Mid(RB_Direcciones.SelectedItem.Text, 1, InStr(RB_Direcciones.SelectedItem.Text, "//") - 1)

            DP_Ciudad.SelectedValue = cargadrop.cmn_cls.id_ciu

            CG.ComunaDevuelve(CStr(DP_Ciudad.SelectedValue), True, DP_Comuna)

            DP_Comuna.SelectedValue = cargadrop.id_cmn

        Catch ex As Exception

        End Try
    End Sub


#End Region

End Class
