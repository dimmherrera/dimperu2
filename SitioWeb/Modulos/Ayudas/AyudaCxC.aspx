<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AyudaCxC.aspx.vb" Inherits="Modulos_Ayudas_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ayuda Clientes</title>
    <base target="_self"></base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../FuncionesJS/Ventanas.js"></script>
    <script src="FuncionesPrivadasJS/AyudaCliente.js" type="text/javascript"></script>
    <script src="FuncionesPrivadasJS/AyudaCXC.js" type="text/javascript"></script>

    
</head>
<body leftmargin="5" topmargin="5">
<form id="form1" runat="server"> 
<asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                <tr>
                   <td height="15px" class="Cabecera">
                       <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Criterio de Búsqueda"></asp:Label>
                   </td>
                </tr>
                <tr>
                   <td valign="top" class="Contenido">
                       <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" style="position: static">
                            <tr>
                               <td align="right">
                                   <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Identificación" Visible="false"></asp:Label>
                               </td>
                               <td>
                                   <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsTxt" Width="70px" Visible="false"></asp:TextBox>
                                   <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                        TargetControlID="Txt_Rut"></cc1:MaskedEditExtender>
                               </td>
                               <td align="right">
                                  &nbsp;</td>
                              <td>
                                  &nbsp;</td>
                              <td align="center">
                                  &nbsp;</td> 
                            </tr>
                       </table>
                   </td> 
                </tr>
                <tr>
                   <td align="left" valign="top">
                      <asp:Panel ID="Panel_Gr_Doc" runat="server" Height="400px">
                          <asp:GridView ID="GR_Doc" runat="server" AutoGenerateColumns="false" CssClass="formatUltcell" EnableSortingAndPagingCallbacks="true" PageSize="1" Width="100%">
                              <Columns>
                              <asp:BoundField DataField="Contrato" HeaderText="Nº Contrato" HtmlEncode="false" 
                                   HtmlEncodeFormatString="False">
                                   <ItemStyle Width="100px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="deu_ide" HeaderText="Nit Pagador" HtmlEncode="False" 
                                    HtmlEncodeFormatString="False" >
                                  <ItemStyle Width="90px" HorizontalAlign="Right"  />
                              </asp:BoundField>
                              <asp:BoundField DataField="DEUDOR" HeaderText="Pagador" HtmlEncode="False" 
                                    HtmlEncodeFormatString="False" >
                                  <ItemStyle Width="250px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="TipoDocto" HeaderText="Tipo Doc." 
                                    HtmlEncode="False" HtmlEncodeFormatString="False" >
                                  <ItemStyle Width="150px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="opo_otg" HeaderText="Nº Otg." HtmlEncode="False" 
                                    HtmlEncodeFormatString="False">
                                  <ItemStyle Width="90px" HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="dsi_num" HeaderText="Nº Docto" HtmlEncode="False" 
                                    HtmlEncodeFormatString="False">
                                  <ItemStyle Width="90px" HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="moneda" HeaderText="Moneda" HtmlEncode="False" >
                                  <ItemStyle Width="60px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="dsi_mto" HeaderText="Monto Documento">
                                  <ItemStyle Width="100px" HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuota"
                                    HtmlEncode="False" HtmlEncodeFormatString="False" >
                                  <ItemStyle Width="40px" HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="doc_fev_rea" HeaderText="Fec.Vcto." 
                                    DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False"  
                                    HtmlEncodeFormatString="False">
                                  <ItemStyle Width="90px" />
                              </asp:BoundField>
                              <asp:BoundField DataField="EstadoDocto" HeaderText="Estado" HtmlEncode="False" 
                                    HtmlEncodeFormatString="False" >
                                  <ItemStyle Width="100px" />
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_sel.gif" 
                                            ToolTip='<%# Eval("dsi_num") %>' AlternateText='<%# Eval("Contrato") %>' onclick="Img_Ver_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                          </Columns>
                          <HeaderStyle CssClass="cabeceraGrilla" />
                          <RowStyle CssClass="formatUltcell" />
                          <AlternatingRowStyle CssClass="formatUltcellAlt" />
                          </asp:GridView> 
                      </asp:Panel>
                   </td>
                </tr>
                <tr>
                   <td align="center" class="style3">
                      <asp:ImageButton ID="IB_Prev" runat="server" 
                            ImageUrl="../../Imagenes/btn_workspace/flecha_izq_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                      <asp:ImageButton ID="IB_Next" runat="server" 
                            ImageUrl="../../Imagenes/btn_workspace/flecha_der_out.gif" 
                            onmouseout="this.src='../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                            onmouseover="this.src='../../Imagenes/btn_workspace/flecha_der_in.gif';" 
                            Height="28px" Width="26px"/>
                  </td>
                </tr>
           </table>
           <uc1:Mensaje ID="Mensaje1" runat="server" />
        </ContentTemplate>
        </asp:UpdatePanel>
        </form>
</body>
</html>
