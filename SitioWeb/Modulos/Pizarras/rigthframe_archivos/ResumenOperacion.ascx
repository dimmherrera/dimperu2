<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ResumenOperacion.ascx.vb" Inherits="ResumenOperacional" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<link href="../../../CSS/Imagenes.css" rel="stylesheet" type="text/css" />

<table id="tb_Linea_Financiamiento1" cellpadding="0" cellspacing="1" width="950">
    <tr>
        <td class="Cabecera">
            <asp:Label ID="Label9" runat="server" Text="Linea de Financiamiento" CssClass="SubTitulos"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="Contenido" style="padding: 5px">
        
        <table id="tb_Linea_Financiamiento2" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Image ID="Img_LineaVig" runat="server" 
                ImageUrl="~/Imagenes/Iconos/Aprueba.gif" ToolTip="Si Tiene Linea Vigente" />
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Linea Vigente"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Mto. Linea Aprob. (+)"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="Txt_MtoLineaApb" runat="server" CssClass="clsDisabled" 
                ReadOnly="True"></asp:TextBox>
        </td>
        <td align="right">
         <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Mto. Utilizado (-)"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="Txt_MtoUtilizado" runat="server" CssClass="clsDisabled" 
                ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="1" align="center">
            <table border=0 cellpadding=0 cellspacing=0 width="300">
                <tr>
                    <td align="left" width="30">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td valign="bottom" align="center" width="30">
                        <asp:Image ID="Image3" runat="server" 
                            ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10px"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td align="center" width="30">
                        <asp:Image ID="Image9" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    <td  align="right" width="30">
                        <asp:Image ID="Image10" runat="server" ImageUrl="~/Imagenes/Iconos/handle-right.png" Visible="false" Width="10"/>
                    </td>
                    
                </tr>
            </table>
        </td>
        <td align="right">
            <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Mto. Ope. Curse"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="Txt_MtoOpeCurse" runat="server" CssClass="clsDisabled" 
                ReadOnly="True"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Saldo Linea (=)"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="Txt_SaldoLinea" runat="server" CssClass="clsDisabled" 
                ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label40" runat="server" CssClass="Label" Text="Optimo" 
                Font-Size="7pt"></asp:Label>
        </td>
        <td colspan="1" align="center" valign="top">
            <img src="../../../Imagenes/Barras/barra.gif" />
        </td>
        <td align="left">
            <asp:Label ID="Label39" runat="server" CssClass="Label" Text="Critico" 
                Font-Size="7pt"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
        <td align="right">
            &nbsp;</td>
        <td>
            <asp:TextBox ID="Txt_SaldoLinea_Slider" runat="server" CssClass="clsDisabled" 
                Visible="False">0</asp:TextBox>
                        </td>
    </tr>
</table>

        </td>
    </tr>
</table>

<br />

<table border="0" cellpadding="0" cellspacing="1">
    <tr>
        <td valign="top">
            <table id="tb_operacion" cellpadding="0" cellspacing="1">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label10" runat="server" Text="Operación" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 5px">
                        <table>
                            <tr>
                                <td valign="top" style="border: 1px solid #000000;">
                                    <table id="Table5" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right" Width="90px">
                                                <asp:Label ID="Label24" runat="server" Text="Operación" CssClass="Label" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Mto. Operación" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_MtoAnticipo" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="Tasa Negocio" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_CostoFondo" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" Text="Spread" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Spread" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label19" runat="server" Text="Tasa" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Tasa" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="border: 1px solid #000000;">
                                    <table id="Table6" cellpadding="0" cellspacing="0" >
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label23" runat="server" Text="Comisión" CssClass="Label" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="150px">
                                                <asp:Label ID="Label20" runat="server" Text="%" CssClass="Label"></asp:Label>
                                            </td>
                                            <td align="left" width="130px">
                                                <asp:TextBox ID="Txt_PorComision" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label21" runat="server" Text="Min. de docto." CssClass="Label" 
                                                    Width="90px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Minimo" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label41" runat="server" Text="Max. de docto." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Maximo" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="70">
                                                <asp:Label ID="Label22" runat="server" Text="Comisión Flat" CssClass="Label" 
                                                    Width="90px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Flat" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="70">
                                                <asp:Label ID="Label25" runat="server" Text="Com. Total" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_ComisionTotal" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="border: 1px solid #000000;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right" width="90px">
                                                <asp:Label ID="Label42" runat="server" Text="Última Operación" CssClass="Label" Font-Size="8"></asp:Label>
                                            </td>
                                            <td align="center" width="70">
                                                <asp:Label ID="Label35" runat="server" Text="Aumenta" CssClass="Label" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" width="70">
                                                <asp:Label ID="Label37" runat="server" Text="Mantiene" CssClass="Label" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" width="70">
                                                <asp:Label ID="Label38" runat="server" Text="Baja" CssClass="Label" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label36" runat="server" Text="Spread" CssClass="Label"></asp:Label>
                                            </td>
                                            <td align="center">

                                                <asp:Image ID="Img_Spread_Aum" runat="server" ToolTip="Aumenta" ImageUrl="~/Imagenes/Iconos/check.gif"  Visible="false"/>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Img_Spread_Man" runat="server" ToolTip="Mantiene" ImageUrl="~/Imagenes/Iconos/check.gif" Visible="false"/>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Img_Spread_Baj" runat="server" ToolTip="Baja" ImageUrl="~/Imagenes/Iconos/check.gif" Visible="false"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label33" runat="server" Text="Comisión" CssClass="Label"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Img_Com_Aum" runat="server" ToolTip="Aumenta"  ImageUrl="~/Imagenes/Iconos/check.gif" Visible="false"/>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Img_Com_Man" runat="server" ToolTip="Mantiene" ImageUrl="~/Imagenes/Iconos/check.gif" Visible="false"/>
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Img_Com_Baj" runat="server" ToolTip="Baja" ImageUrl="~/Imagenes/Iconos/check.gif" Visible="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" align="left" style="border: 1px solid #000000;">
                                    <table id="Table3" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right" width="150px">
                                                <asp:Label ID="Label16" runat="server" Text="Nº Doctos. Ope. en Curse" CssClass="Label"></asp:Label>
                                            </td>

                                            <td align="left" width="130px" >
                                                <asp:TextBox ID="Txt_NroDocOpe" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="150px">
                                                <asp:Label ID="Label17" runat="server" Text="Nº Deu. Ope. en Curse" CssClass="Label"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_NroDeuOpe" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" style="border: 1px solid #000000;">
                                    <table id="Table2" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="Label15" runat="server" Text="Nº Ope. Cursadas Factoring" CssClass="Label"
                                                    Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="90px">
                                                <asp:Label ID="Lbl_Ultimo" runat="server" Text="Año 2007" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_UltimoAno" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Lbl_Penultimo" runat="server" Text="Año 2006" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_PenultimoAno" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" align="left" style="border: 1px solid #000000;">
                                   <table id="Table8" cellpadding="0" cellspacing="0" style="height: 50px" >
                                        <tr>
                                            <td align="center" width="50">
                                                <asp:Image ID="Img_EstVer" runat="server" ImageUrl="~/Imagenes/Iconos/rojo.gif" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="Estado Verificación" CssClass="Label"
                                                    Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table id="Table10" cellpadding="0" cellspacing="1" width="490">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label2" runat="server" Text="Datos Simulación" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Monto Doctos." CssClass="Label" > </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Mto_Doc" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Comisión Variable" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Com_Por_Doc" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label27" runat="server" Text="Base Negociación" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Mto_Ant" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label32" runat="server" Text="Comisión Fija" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Com_Esp" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label28" runat="server" Text="Descuento" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Dif_Pre" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label44" runat="server" Text="Gastos Afectos" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_GastosAfectos" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label29" runat="server" Text="Precio Compra" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Pre_Com" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label13" runat="server" Text="IVA" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Iva" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label30" runat="server" Text="Reserva" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Sal_Pen" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label34" runat="server" Text="Gastos Exentos" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Gastos" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label31" runat="server" Text="Saldo a Pagar" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Sal_Pag" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label14" runat="server" Text="Impuesto" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Impuestos" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="Label228" runat="server" Text="Aplicación Deuda" 
                                        CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Descuentos" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="Label45" runat="server" Text="GMF" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Val_GMF" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Label ID="Label272" runat="server" Text="Total Desembolso" 
                                        CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Tot_Gir" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="490px">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label43" runat="server" Text="Excepciones/Condiciones" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="Contenido">
                                                <asp:TextBox ID="txt_Excep" runat="server" TextMode="MultiLine" ReadOnly="true"
                                                 CssClass="clsDisabled" Width="98%" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
           
        </td>
    </tr>
</table>

<asp:LinkButton ID="LB_Refrescar" runat="server"></asp:LinkButton>

