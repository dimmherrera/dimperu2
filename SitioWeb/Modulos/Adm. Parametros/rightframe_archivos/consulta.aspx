<%--<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consulta.aspx.vb" Inherits="Modulos_Adm._Parametros_rightframe_archivos_consulta" %>
--%>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consulta.aspx.vb" Inherits="consulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<script language="javascript">
 
 function DoScrollAlfa()
 {
    var _gridView = document.getElementById("GridViewDivAlfa");
    var _header = document.getElementById("HeaderDivAlfa");
     _header.scrollLeft = _gridView.scrollLeft;
 }

function DoScrollNume()
 {
    var _gridView = document.getElementById("GridViewDiv");
    var _header = document.getElementById("HeaderDiv");
     _header.scrollLeft = _gridView.scrollLeft;
 }

</script>
   <%-- <script src="../FuncionesPrivadasJS/Grila.js" type="text/javascript"></script>--%>
    <title>Consultas</title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" language="javascript" src="../../../FuncionesJS/Grilla.js"></script>--%>

   
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 737px">
            <tr>
                <td class="Cabecera">
                    <asp:Label ID="Label1" runat="server" CssClass="Titulos" Style="position: static"
                        Text="Consulta de Parametros"></asp:Label></td>
            </tr>
            <tr>
                <td class="Contenido">
          
                        <table style="width: 704px">
                            <tr>
                                <td align="left" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:RadioButton ID="Rb_n" runat="server" AutoPostBack="True" CssClass="Label" Font-Bold="False"
                                                    Text="Numérico" GroupName="con" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:RadioButton ID="Rb_a" runat="server" CssClass="Label" Font-Bold="False" Text="Alfanumérico"
                                                    GroupName="con" AutoPostBack="True" />
                                            </td>
                                            <td align="center">
                                                <asp:DropDownList ID="Drop_Nume" runat="server" Width="166px" AutoPostBack="True" CssClass="clsMandatorio">
                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="1">Region</asp:ListItem>
                                                    <asp:ListItem Value="2">Comuna-Localidad</asp:ListItem>
                                                    <asp:ListItem Value="3">Estado Deudor</asp:ListItem>
                                                    <asp:ListItem Value="5">Niveles</asp:ListItem>
                                                    <asp:ListItem Value="7">Modo Operación</asp:ListItem>
                                                    <asp:ListItem Value="8">Estado Cliente</asp:ListItem>
                                                    <asp:ListItem Value="10">Estado de Poderes</asp:ListItem>
                                                    <asp:ListItem Value="11">Estado Docto </asp:ListItem>
                                                    <asp:ListItem Value="12">Tipo Operación</asp:ListItem>
                                                    <asp:ListItem Value="15">Carta Tipo</asp:ListItem>
                                                    <asp:ListItem Value="17">Zonas</asp:ListItem>
                                                    <asp:ListItem Value="18">Forma de Pago</asp:ListItem>
                                                    <asp:ListItem Value="20">Sistemas</asp:ListItem>
                                                    <asp:ListItem Value="21">Tipo Pagaré</asp:ListItem>
                                                    <asp:ListItem Value="22">Estado Pagaré</asp:ListItem>
                                                    <asp:ListItem Value="23">Moneda</asp:ListItem>
                                                    <asp:ListItem Value="24">Tipo Garantía</asp:ListItem>
                                                    <asp:ListItem Value="25">Regimen Matrimonial</asp:ListItem>
                                                    <asp:ListItem Value="26">Tipo Aval</asp:ListItem>
                                                    <asp:ListItem Value="27">Estado Aval</asp:ListItem>
                                                    <asp:ListItem Value="28">Estado Solicitud de Linea</asp:ListItem>
                                                    <asp:ListItem Value="29">Estado de Linea</asp:ListItem>
                                                    <asp:ListItem Value="30">Estado de Operación</asp:ListItem>
                                                    <asp:ListItem Value="31">Tipo de Documento</asp:ListItem>
                                                    <asp:ListItem Value="36">Tipo de Gasto Operacional</asp:ListItem>
                                                    <asp:ListItem Value="40">Estado de Verificacion</asp:ListItem>
                                                    <asp:ListItem Value="44">Tipo de Cliente</asp:ListItem>
                                                    <asp:ListItem Value="45">Tipo de Ejecutivo</asp:ListItem>
                                                    <asp:ListItem Value="48">Estado de Ejecutivo</asp:ListItem>
                                                    <asp:ListItem Value="49">Tipo de Telefono</asp:ListItem>
                                                    <asp:ListItem Value="51">Tipo Gasto Recaudacion</asp:ListItem>
                                                    <asp:ListItem Value="52">Estado Docto de pago</asp:ListItem>
                                                    <asp:ListItem Value="53">Que se Paga</asp:ListItem>
                                                    <asp:ListItem Value="54">Tipo de Ingreso</asp:ListItem>
                                                    <asp:ListItem Value="55">Que a Pagar</asp:ListItem>
                                                    <asp:ListItem Value="56">Tipo de Egreso</asp:ListItem>
                                                    <asp:ListItem Value="58">Categoría de Riesgo</asp:ListItem>
                                                    <asp:ListItem Value="60">Estado de Facturas</asp:ListItem>
                                                    <asp:ListItem Value="61">Motivos de Protestos</asp:ListItem>
                                                    <asp:ListItem Value="62">Facultades de Poder</asp:ListItem>
                                                    <asp:ListItem Value="63">Razones Sociales</asp:ListItem>
                                                    <asp:ListItem Value="64">Actividad Económica </asp:ListItem>
                                                    <asp:ListItem Value="65">Tipo de Riesgos</asp:ListItem>
                                                    <asp:ListItem Value="67">Tipo de Envio Información</asp:ListItem>
                                                    <asp:ListItem Value="68">Forma de Envío</asp:ListItem>
                                                    <asp:ListItem Value="70">País</asp:ListItem>
                                                    <asp:ListItem Value="71">Otro</asp:ListItem>
                                                    <asp:ListItem Value="72">Otro 1 </asp:ListItem>
                                                    <asp:ListItem Value="73">Otro 2</asp:ListItem>
                                                    <asp:ListItem Value="74">Otro 3</asp:ListItem>
                                                    <asp:ListItem Value="75">Tipo Operación Contable</asp:ListItem>
                                                    <asp:ListItem Value="76">Segmentos</asp:ListItem>
                                                    <asp:ListItem Value="77">Tipo Beneficiario </asp:ListItem>
                                                    <asp:ListItem Value="78">Actuacion Apoderado</asp:ListItem>
                                                    <asp:ListItem Value="79">Contratos</asp:ListItem>
                                                    <asp:ListItem Value="80">Zona de Riesgo Recaudación</asp:ListItem>
                                                    <asp:ListItem Value="81">Plataformas</asp:ListItem>
                                                    <asp:ListItem Value="82">Estado Negociación</asp:ListItem>
                                                    <asp:ListItem Value="83">Objetivo Credito</asp:ListItem>
                                                    <asp:ListItem Value="84">Ciudad</asp:ListItem>
                                                    <asp:ListItem Value="85">Meses</asp:ListItem>
                                                    <asp:ListItem Value="86">Estados de Cuentas</asp:ListItem>
                                                    <asp:ListItem Value="87">Origen de Fondo</asp:ListItem>
                                                    <asp:ListItem Value="88">Tipo de Envío</asp:ListItem>
                                                    <asp:ListItem Value="89">Estado Ope-Negociación</asp:ListItem>
                                                    <asp:ListItem Value="90">Estado Cob-Neg</asp:ListItem>
                                                    <asp:ListItem Value="91">Tipo de Cartas</asp:ListItem>
                                                    <asp:ListItem Value="99">Parametros Consulta Api </asp:ListItem>
                                                    <asp:ListItem Value="100">Tipos de Productos</asp:ListItem>
                                                    <asp:ListItem Value="101">Tipo Comision Factoring Electronico</asp:ListItem>
                                                    <asp:ListItem Value="102">Parametros Tipo Provisiones</asp:ListItem>
                                                    <asp:ListItem Value="103">Estado no Recaudado</asp:ListItem>
                                                    <asp:ListItem Value="104">Caracteristica Operación</asp:ListItem>
                                                    <asp:ListItem Value="105">Estado Línea Fogape</asp:ListItem>
                                                    <asp:ListItem Value="106">Tipo Devolución</asp:ListItem>
                                                    <asp:ListItem Value="107">Carga Masiva Documento</asp:ListItem>
                                                    <asp:ListItem Value="108">Carga Masiva Pago Cliente</asp:ListItem>
                                                    <asp:ListItem Value="109">Carga Masiva Pago Deudor</asp:ListItem>
                                                    <asp:ListItem Value="300">Informes por Mail</asp:ListItem>
                                                    <asp:ListItem Value="301">Horario Informes por Mail</asp:ListItem>
                                                    <asp:ListItem Value="302">Usuarios para Nomina Diaria de Negocios</asp:ListItem>
                                                    <asp:ListItem Value="303">Tipo de Servicio de Llamada</asp:ListItem>
                                                    <asp:ListItem Value="304">Envio por Email</asp:ListItem>
                                                    <asp:ListItem Value="305">Saludos Envio Email</asp:ListItem>
                                                    <asp:ListItem Value="306">Texto del Envio Email</asp:ListItem>
                                                    <asp:ListItem Value="307">Mensaje de Despedida del Envio Email</asp:ListItem>
                                                    <asp:ListItem Value="308">Mensaje de Publicidad del Envio Email</asp:ListItem>
                                                    <asp:ListItem Value="309">Estado Usuarios </asp:ListItem>
                                                    <asp:ListItem Value="310">Tipo Cierre Contable</asp:ListItem>
                                                    <asp:ListItem Value="69">Tipo Clisificacion</asp:ListItem>
                                                    <asp:ListItem Value="41">Tipo Cuentas por Cobrar</asp:ListItem>
                                                    <asp:ListItem Value="110">Estado de Evaluacion</asp:ListItem>
                                                    <asp:ListItem Value="111">Estado de Condicion</asp:ListItem>
                                                    <asp:ListItem Value="112">Custodia</asp:ListItem>
                                                    <asp:ListItem Value="118">Clasificación Cliente</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="Drop_Alfa" runat="server" Width="166px" AutoPostBack="True"
                                                    CssClass="clsMandatorio">
                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="1">Giro</asp:ListItem>
                                                    <asp:ListItem Value="2">Plazas</asp:ListItem>
                                                    <asp:ListItem Value="3">Banca Clientes</asp:ListItem>
                                                    <asp:ListItem Value="4">Factoring</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:MultiView ID="MultiView1" runat="server">
                                                    <asp:View ID="View1" runat="server">
                                                        <div id="HeaderDiv" style="overflow: hidden; width: 720px">
                                                            <table class="cabeceraGrilla" width="698px">
                                                                <tr>
                                                                    <td style="width: 120px" align="left">
                                                                        <asp:Label ID="Label2" runat="server" Text="Codigo" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 460px" align="center">
                                                                        <asp:Label ID="Label3" runat="server" Text="Descripción" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label4" runat="server" Text="Estado" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="GridViewDiv" style="overflow: scroll; width: 720px; height: 300px" onscroll="DoScrollNume()">
                                                            <asp:GridView ID="Gr_con" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                PageSize="15" Width="698px" ShowHeader="False">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="C&#243;digo" DataField="Codigo">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Descripci&#243;n" DataField="Descripcion"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Estado" DataField="Estado">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="View2" runat="server">
                                                        <div id="HeaderDivAlfa" style="overflow: hidden; width: 720px">
                                                            <table width="698px" class="cabeceraGrilla">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label5" runat="server" Text="Codigo" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 400">
                                                                        <asp:Label ID="Label6" runat="server" Text="Descripción" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text="Estado" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label8" runat="server" Text="Sistema" CssClass="LabelCabeceraGrilla"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="GridViewDivAlfa" style="overflow: scroll; width: 720px; height: 300" onscroll="DoScrollAlfa()">
                                                            <asp:GridView ID="Gr_alfa" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                                PageSize="15" Width="698px" ShowHeader="False">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Codigo" DataField="Codigo">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Descripci&#243;n" DataField="Descripcion"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="Estado" DataField="est">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Sistema" DataField="sistema">
                                                                        <ItemStyle Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="cabeceraGrilla" />
                                                            </asp:GridView>
                                                        </div>
                                                    </asp:View>
                                                </asp:MultiView><br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                         
                        </table>
              
            
                </td>
            </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" 
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                        onmouseout="this.src='../../../Imagenes/Botones/BBoton_Volver_out.gif';" 
                            ToolTip="Volver"  />
                    </td>
                </tr>
        </table>
        </ContentTemplate>
          <Triggers>
              <asp:PostBackTrigger ControlID="IB_Volver" />
        </Triggers>
        
        </asp:UpdatePanel>
        
        
    </form>
</body>
</html>
