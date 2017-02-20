<%@ Page Language="VB" AutoEventWireup="false" CodeFile="modif_doc_otg.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_protesto" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
        <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Auto" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
        
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                          <table cellspacing="0">
                              <tr>
                                  <td class="Cabecera">
                                      <asp:Label ID="Label25366" runat="server" CssClass="Titulos" 
                                          Text="Protesto de Documentos de Operación"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="Contenido">
                                      <table cellspacing="0" width="900" style="width: 452px">
                                          <tr>
                                              <td class="Contenido">
                                                  <table>
                                                      <tr>
                                                          <td>
                                                              <asp:Label ID="lbl_moneda" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:DropDownList ID="dr_moneda" runat="server" CssClass="clsMandatorio" 
                                                                  Width="200px" AutoPostBack="True">
                                                              </asp:DropDownList>
                                                          </td>
                                                          <td>
                                                              <asp:Label ID="lbl_comi" runat="server" CssClass="Label" Text="Mto.Comisión"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txt_mto_comi" runat="server" CssClass="clsMandatorio" 
                                                                  
                                                                  Width="90px" AutoPostBack="True"></asp:TextBox>
                                                                 <cc1:MaskedEditExtender ID="Txt_valor_MaskedEditExtender" runat="server" 
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                                            CultureThousandsPlaceholder="." CultureTimePlaceholder="" Enabled="True" 
                                                            InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                                            TargetControlID="txt_mto_comi" 
                                                            CultureName="es-ES">
                                                        </cc1:MaskedEditExtender>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                              <asp:Label ID="lbl_motivo" runat="server" CssClass="Label" Text="Motivo"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:DropDownList ID="dr_mot" runat="server" CssClass="clsMandatorio" 
                                                                  Width="200px">
                                                              </asp:DropDownList>
                                                          </td>
                                                          <td align="right">
                                                              <asp:Label ID="lbl_iva" runat="server" CssClass="Label" Text="Iva"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txt_iva" runat="server" CssClass="clsDisabled" 
                                                                  Width="90px" ReadOnly="True"></asp:TextBox>
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
                                      <asp:ImageButton ID="btn_guardar" runat="server" 
                                          ImageUrl="~/Imagenes/Botones/Boton_guardar_out.gif" 
                                          onmouseout="this.src='../../../Imagenes/Botones/Boton_guardar_out.gif';" 
                                          onmouseover="this.src='../../../Imagenes/Botones/Boton_guardar_in.gif';" 
                                          ToolTip="Guardar" />
                                      <asp:ImageButton ID="btn_volver" runat="server" 
                                          ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" 
                                          onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                          onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                                          TabIndex="31" ToolTip="Volver" />
                                  </td>
                              </tr>
                          </table>
                           <uc1:Mensaje ID="Mensaje1" runat="server" />
                          <asp:LinkButton ID="lb_formato" runat="server" OnClick="lb_formato_click"></asp:LinkButton>
                     </ContentTemplate>
                </asp:UpdatePanel>

        
    
    
    <asp:LinkButton ID="lb_guardar" runat="server"></asp:LinkButton>

        
    
    </form>
 
</body>
</html>
