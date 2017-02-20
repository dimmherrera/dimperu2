<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Menu_Sup.ascx.vb" Inherits="WebControles_Menu_Sup" %>

<div id="menu">
    <ul class="menu">
        <li runat="server" id="adm" visible="false"><a href="#"><span>Administración</span></a>
            <ul style="text-align: left">
                <li runat="server" id="adm1" visible="false"><a href="../../../Modulos/Adm. Clientes/rightframe_archivos/MClientes.aspx"><span>
                    Proveedor</span></a></li>
                <li runat="server" id="adm2" visible="false"><a href="../../../Modulos/Adm.Deudores/rightframe_archivos/MDeudores.aspx"><span>
                    Pagadores</span></a></li>
                <li runat="server" id="adm3" visible="false"><a href="../../../Modulos/Adm.%20Cuentas/rightframe_archivos/CtasCxC.aspx"><span>
                    Cuentas Por Cobrar</span></a></li>
                <li runat="server" id="adm4" visible="false"><a href="../../../Modulos/Adm.%20Cuentas/rightframe_archivos/CtasCxP.aspx"><span>
                    Cuentas Por Pagar</span></a></li>
                <li runat="server" id="adm5" visible="false"><a href="../../../Modulos/Adm.%20Cuentas/rightframe_archivos/Informes.aspx"><span>
                    Informe De Cuentas y NCE</span></a></li>
            </ul>
        </li>
        <li runat="server" id="lin" visible="false"><a href="#"><span>Línea De Financiamiento</span></a>
            <ul style="text-align: left">
                <li><a href="../../../Modulos/Linea de Credito/rigthframe_archivos/MLineaCredito.aspx?Tipo=1">
                    <span>Adm. Línea De Finan.</span></a></li>
            </ul>
        </li>
        <li runat="server" id="vb" visible="false"><a href="#"><span>Control Dual</span></a>
            <ul style="text-align: left">
                <li runat="server" id="vb1" visible="false"><a href="../../../Modulos/Pizarras/rigthframe_archivos/PizarraAprobaciones.aspx">
                    <span>Aprobaciones</span></a></li>
                <li runat="server" id="vb2" visible="false"><a href="../../../Modulos/Linea de Credito/rigthframe_archivos/MLineaCredito.aspx?Tipo=2">
                    <span>Línea Financiamiento</span></a></li>
                <li runat="server" id="vb3" visible="false"><a href="../../../Modulos/Tesorería/rightframe_archivos/VB_Pagos.aspx"><span>Pagos</span></a></li>
                <li runat="server" id="vb4" visible="false"><a href="../../../Modulos/Prorrogas/rightframe_archivos/VBProrroga.aspx"><span>Prorroga</span></a></li>
                <li runat="server" id="vb5" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/vb_pago_rec.aspx"><span>
                    Recaudación</span></a></li>
                <li runat="server" id="vb6" visible="false"><a href="../../../Modulos/Carp.%20Comercial/rigthframe_archivos/VB_Aplicacion.aspx">
                    <span>Aplicaciones</span></a></li>
                <%--<li runat="server" id="vb7" visible="false"><a href="../../../Modulos/Carp.%20Comercial/rigthframe_archivos/pizarracliente.aspx">
                    <span>Pizarra Clientes</span></a></li>--%>
            </ul>
        </li>
        <li runat="server" id="com" visible="false"><a href="#"><span>Comercial</span></a>
            <ul style="text-align: left">
                <li runat="server" id="com1" visible="false"><a href="../../../Modulos/Pizarras/rigthframe_archivos/PizarraEjecutivos.aspx"><span>
                    Módulo de Control</span></a></li>
                <li runat="server" id="com2" visible="false"><a href="../../../Modulos/Carp.%20Comercial/rigthframe_archivos/Asig_Ejecutivo.aspx">
                    <span>Asig. Ejecutivo</span></a></li>
                <li runat="server" id="com3" visible="false"><a href="../../../Modulos/Carp.%20Comercial/rigthframe_archivos/Evaluacion.aspx">
                    <span>Evaluación Cli./Pag.</span></a></li>
                <li runat="server" id="com4" visible="false"><a href="../../../Modulos/Carp. Comercial/rigthframe_archivos/Negociacion.aspx">
                    <span>Negociación</span></a></li>
                <li runat="server" id="com5" visible="false"><a href="../../../Modulos/Alertas/rightframe_archivos/Alertas.aspx"><span>Alertas</span></a></li>
                <li runat="server" id="com6" visible="false"><a href="../../../Modulos/Carp. Comercial/rigthframe_archivos/Aplicaciones.aspx">
                <span>Aplicación de Reservas</span></a></li>
            </ul>
        </li>
        <li runat="server" id="ope" visible="false"><a href="#"><span>Operación</span></a>
            <ul style="text-align: left">
                <li runat="server" id="ope1" visible="false"><a href="../../../Modulos/Pizarras/rigthframe_archivos/PizarraOperaciones.aspx">
                    <span>Módulo de Control</span></a></li>
                <li runat="server" id="ope2" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/ing-ope.aspx"><span>Ingreso
                    Ope.</span></a></li>
                <li runat="server" id="ope3" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/Simulation.aspx"><span>
                    Simulacion</span></a></li>
                <li runat="server" id="ope4" visible="false"><a href="../../../Modulos/Pizarras/rigthframe_archivos/PizarraOtorgamiento.aspx">
                    <span>Otorgamiento</span></a></li>
                <li runat="server" id="ope5" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/anticipo.aspx"><span>Abono
                    Anticipo</span></a></li>
                <li runat="server" id="ope6" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/Mod_doctos_otg.aspx"><span>
                    Consulta Doctos.</span></a></li>
                <li runat="server" id="ope7" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/Operaciones.aspx"><span>
                    Consulta Operaciones</span></a></li>
                <li runat="server" id="ope8" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/CartolaDoctos.aspx"><span>
                    Cartola Doctos.</span></a></li>
                <li runat="server" id="ope9" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/estado_deuda.aspx"><span>
                    Estado De Deuda</span></a></li>
                <li runat="server" id="ope10" visible="false"><a href="../../../Modulos/Alertas/rightframe_archivos/AlertasCondiciones.aspx"><span>
                    Alertas de Condiciones</span></a></li>
                <li runat="server" id="ope11" visible="false"><a href="../../../Modulos/Operaciones/rightframe_archivos/VerificacionDocumentos.aspx"><span>
                    Verificación</span></a></li>
            </ul>
        </li>
        <li runat="server" id="leg" visible="false"><a href="#"><span>Legal</span></a>
            <ul style="text-align: left">
                <li runat="server" id="leg1" visible="false"><a href="../../../Modulos/Legal/rightframe_archivos/Pagare.aspx"><span>Pagares</span></a></li>
                <li runat="server" id="leg2" visible="false"><a href="../../../Modulos/Legal/rightframe_archivos/Avales.aspx"><span>Avales</span></a></li>
                <li runat="server" id="leg3" visible="false"><a href="../../../Modulos/Legal/rightframe_archivos/ListaCobertura.aspx"><span>Coberturas</span></a></li>
                <li runat="server" id="leg4" visible="false"><a href="../../../Modulos/Legal/rightframe_archivos/InformesGenerales.aspx"><span>
                    Informes Grales.</span></a></li>
            </ul>
        </li>
        <li runat="server" id="cob" visible="false"><a href="#"><span>Cobranza</span></a>
            <ul style="text-align: left">
                <li runat="server" id="cob2" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/Asig_Cobrador.aspx"><span>
                    Asig. Cob. Tel.</span></a></li>
                <li runat="server" id="cob3" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/Asignacion_esp.aspx"><span>
                    Asig. Especial</span></a></li>
                <li runat="server" id="cob4" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/Call_Telefonicas.aspx"><span>
                    Ingreso Gestion</span></a></li>
                <li runat="server" id="cob5" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/CarteraVigMor.aspx"><span>
                    Cartera Vig./Mora.</span></a></li>
                <li runat="server" id="cob6" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/GestCobranza.aspx"><span>
                    Gestion Cobranza</span></a></li>
                <li runat="server" id="cob7" visible="false"><a href="../../../Modulos/Cobranzas/rigthframe_archivos/RadicarFact.aspx"><span>
                    Radicación de Facturas</span></a></li>
            </ul>
        </li>
        <li runat="server" id="rec" visible="false"><a href="#"><span>Recaudación</span></a>
            <ul style="text-align: left">
                <li runat="server" id="rec1" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/Asig_rec.aspx"><span>Asig.
                    Recaudador</span></a></li>
                <li runat="server" id="rec2" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/Hoja_ruta.aspx"><span>
                    Emision Hoja Ruta</span></a></li>
                <li runat="server" id="rec3" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/Hoja_rec.aspx"><span>Ing.
                    Hoja Recaudacion</span></a></li>
                <li runat="server" id="rec4" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/GASTOS_rec.aspx"><span>
                    Ing. Gastos Recaud.</span></a></li>
                <li runat="server" id="rec5" visible="false"><a href="../../../Modulos/Recaudacion/rigthframe_archivos/asig_fnc.aspx"><span>Otro
                    Factoring</span></a></li>
            </ul>
        </li>
        <li runat="server" id="pro" visible="false"><a href="#"><span>Prorroga</span></a>
            <ul style="text-align: left">
                <li runat="server" id="pro1" visible="false"><a href="../../../Modulos/Prorrogas/rightframe_archivos/Ingreso_Prorroga.aspx"><span>Ingreso Doctos.</span></a></li>
                <li runat="server" id="pro2" visible="false"><a href="../../../Modulos/Prorrogas/rightframe_archivos/VBProrroga.aspx"><span>Visto Bueno Prorroga</span></a></li>
                <li runat="server" id="pro3" visible="false"><a href="../../../Modulos/Prorrogas/rightframe_archivos/AnulacionProrroga.aspx"><span>Anular Prorroga</span></a></li>                    
            </ul>
        </li>
        <li runat="server" id="can" visible="false"><a href="#"><span>Cancelación</span></a>
            <ul style="text-align: left">
                <li runat="server" id="can1" visible="false"><a href="../../../Modulos/Pagos/rightframe_archivos/PagoDirecto.aspx"><span>Pago
                    Directo</span></a></li>
                <li runat="server" id="can2" visible="false"><a href="../../../Modulos/Tesorería/rightframe_archivos/ColillaDeposito.aspx"><span>
                    Colilla Deposito</span></a></li>
                <li runat="server" id="can3" visible="false"><a href="../../../Modulos/Pagos/rightframe_archivos/Inf_pagos.aspx"><span>Inf. Pagos</span></a></li>
            </ul>
        </li>
        <li runat="server" id="tes" visible="false"><a href="#"><span>Tesorería</span></a>
            <ul style="text-align: left">
                <li runat="server" id="tes1" visible="false"><a href="../../../Modulos/Tesorería/rightframe_archivos/PizarraTesoreria.aspx"><span>
                    Módulo de Control</span></a></li>
                <li runat="server" id="tes2" visible="false"><a href="../../../Modulos/Tesorería/rightframe_archivos/AnulacionProtesto.aspx">
                    <span>Anulación Pago</span></a></li>
                <li runat="server" id="tes3" visible="false"><a href="../../../Modulos/Tesorería/rightframe_archivos/arqueocheques.aspx"><span>
                    Listado De Cheques.</span></a></li>
            </ul>
        </li>
        <li runat="server" id="man" visible="false"><a href="#"><span>Mantenimiento</span></a>
            <ul style="text-align: left">
                <%--
                <li runat="server" id="man1" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/MParametros.aspx">
                    <span>Parámetros</span></a></li>
                <li runat="server" id="man2" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/feriados.aspx"><span>
                    Feriados</span></a></li>
                <li runat="server" id="man3" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Paridad.aspx"><span>
                    Paridades</span></a></li>
                <li runat="server" id="man4" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Tasas.aspx"><span>
                    Tasas</span></a></li>
                <li runat="server" id="man5" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Gastos.aspx"><span>
                    Gastos</span></a></li>
                <li runat="server" id="man6" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Sucursal_Plaza.aspx">
                    <span>Sucursal/Plazas</span></a></li>
                <li runat="server" id="man7" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/CiudadComuna.aspx">
                    <span>Municipio/Localidad</span></a></li>
                <li runat="server" id="man8" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Tipo_Cartera.aspx">
                    <span>Tipo Cartera</span></a></li>
                <li runat="server" id="man9" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Notario.aspx"><span>
                    Notarios</span></a></li>
                <li runat="server" id="man10" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Requisitos.aspx">
                    <span>Requisitos</span></a></li>
                <li runat="server" id="man11" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/RequisitosPorTipoDocto.aspx">
                    <span>Req. Por Docto.</span></a></li>
                <li runat="server" id="man12" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Codigo Cobranza.aspx">
                    <span>Codigos Cobranza</span></a></li>
                <li runat="server" id="man13" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Bancos.aspx"><span>
                    Bancos</span></a></li>
                <li runat="server" id="man14" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Sistema.aspx"><span>
                    Sistema</span></a></li>
                <li runat="server" id="man15" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/Mantencion CCF.aspx">
                    <span>Planilla Aprobaciones</span></a></li>
                --%>
                <li runat="server" id="man16" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/gene_arch.aspx">
                    <span>Generación De Archivos</span></a></li>
                <%--
                <li runat="server" id="man17" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/MDoctosCondiciones.aspx">
                    <span>Doctos. & Otras Condiciones</span></a></li>    
                <li runat="server" id="man18" visible="false"><a href="../../../Modulos/Adm.%20Parametros/rightframe_archivos/DoctosCondicionesPorTipoDocto.aspx">
                    <span>Doctos. & Cond. X Tipo Docto.</span></a></li>    
                --%>
            </ul>
        </li>
        <li runat="server" id="ges" visible="false"><a href="#"><span>Gestión</span></a>
            <ul style="text-align: left">
                <li><a href="../../../Modulos/gestion/rigthframe_archivos/Gestion.aspx"><span>Informes
                    De Gestión</span></a></li>
            </ul>
        </li>
    </ul>
</div>
