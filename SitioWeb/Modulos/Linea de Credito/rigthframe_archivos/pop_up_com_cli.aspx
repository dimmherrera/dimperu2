<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pop_up_com_cli.aspx.vb" Inherits="Clientes_pop_up_com_cli" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comisiones por Cliente</title>
    <base target="_self" />
    <style type="text/css">
        .clsMandatorio
{
    border: 1px solid #0000FF;
    margin: 1px;
    padding: 1px 1px;
    /*font: 13px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
    font: 12px calibri;
    background-color: #B7FFFF;
    color: #0000FF;
    cursor: text;
    text-transform:uppercase;
}
        

.Titulos {
	color: #333333;
	/*font-family: "Trebuchet MS", "Lucida Grande", "Bitstream Vera Sans", Arial, Helvetica, sans-serif;
	font-size: 16px;*/
	font: 16px calibri;
	font-weight: bold;
	margin: 6px 0 6px 6px;
}

.Label
{
	font: 12px calibri;
	float: none;
}

        .clsDisabled
{
    background-color: #DFDFDF;
    border: 1px solid #808080;
    margin: 1px;
    padding: 1px 1px;
    /*font: 13px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
    font: 12px calibri;
    color: #CC0000;
    text-transform:uppercase;
}

        .style15
        {
            width: 99%;
            height: 111px;
        }

.formatUltcell
{
	font-weight: normal;
	font-size: 12px;
	color: darkblue;
	line-height: normal;
	font-style: normal;
	font-family: "Trebuchet MS", "Lucida Grande", "Bitstream Vera Sans", Arial, Helvetica, sans-serif;
	background-color: #c2dafa;
}

.cabeceraGrilla
{
	
	text-align: center;
	color: #292929;
	font-size: 100% !important;
	background-color: #c6c7c6;
/*	
	padding: 0px;
	margin: 0px;
	border-bottom: solid 1px #666666;
	border-right: solid 1px #666666;
	border-left: solid 1px #666666;
	border-top: solid 1px #666666;
	filter: progid:dximagetransform.microsoft.gradient(gradienttype=1,startcolorstr=#ffffff,endcolorstr=gainsboro);
*/
}

        .style16
        {
            height: 107px;
        }
        #form1
        {
            height: 772px;
            width: 896px;
        }

        .style17
        {
            width: 100%;
            height: 228px;
        }
        

.Cabecera
{
	padding: 0px;
	margin: 0px;
	border-bottom: solid 1px #666666;
	border-right: solid 1px #666666;
	border-left: solid 1px #666666;
	border-top: solid 1px #666666;
	filter: progid:dximagetransform.microsoft.gradient(gradienttype=1,startcolorstr=#ffffff,endcolorstr=gainsboro);
}


.SubTitulos
{
    color: #333399;
    /*font: 14px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
    font: 14px calibri;
    font-weight: bold;
    margin: 6px 0 6px 6px;
}

.Contenido
{
	border-bottom: solid 1px #666666;
	border-right: solid 1px #666666;
	border-left: solid 1px #666666;
	filter: progid:dximagetransform.microsoft.gradient(gradienttype=0,startcolorstr=#F6F9FC,endcolorstr=white);
	
}

.clsTxt
{
    border: 1px solid #808080;
    margin: 1px 1px 1px 0px;
    padding: 1px 1px;
    /*font: 13px "Trebuchet MS" , "Lucida Grande" , "Bitstream Vera Sans" , Arial, Helvetica, sans-serif;*/
    font: 12px calibri;
    color: #333333;
    text-transform:uppercase;
}
        .style23
        {
            width: 100%;
            height: 37px;
            margin-top: 0px;
        }
        .style30
        {
            width: 95px;
            height: 1px;
        }
        .style36
        {
            width: 95px;
            height: 4px;
        }
        .style37
        {
            height: 4px;
        }
        .style38
        {
            width: 95px;
            height: 24px;
        }
        .style39
        {
            height: 24px;
        }
        .style42
        {
            width: 95px;
            height: 12px;
        }
        .style43
        {
            height: 12px;
        }
        .style44
        {
            width: 95px;
            height: 8px;
        }
        .style45
        {
            height: 8px;
        }
        .style46
        {
            width: 95px;
            height: 19px;
        }
        .style47
        {
            height: 19px;
        }
        .style48
        {
            width: 95px;
            height: 18px;
        }
        .style49
        {
            height: 18px;
        }

    </style>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="Label">
    
                
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Contenttemplate>
    

    
         <table border="0" cellpadding="0" cellspacing="0" width="600">                              
          <tr>
            <td  align="left" class="Cabecera">
               <asp:Label ID="Label2" runat="server" Height="20px" Text="Comisiones por Cliente" 
                                    CssClass="Titulos"></asp:Label>
            </td>
          </tr>
           <tr>
                 <td class="Contenido"  align="center" style="text-align:-moz-center">
                        <table class="style17">
                            <tr>
                                <td class="style48" align="left">        
                                    <asp:Label ID="Label1" runat="server" CssClass="Label">NIT</asp:Label>
                                </td>
                                <td align="left" class="style49">
                                     <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsDisabled" Width="90px">
                                     </asp:TextBox>
                                         <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                           CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                                                CultureName="es-ES" CultureThousandsPlaceholder="." CultureTimePlaceholder="" 
                                                                Enabled="True" InputDirection="RightToLeft"
                                          Mask="999,999,999,999" MaskType="Number" 
                                          TargetControlID="Txt_Rut">
                                         </cc1:MaskedEditExtender>
                                   <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsDisabled" Width="15px" 
                                       MaxLength="1" AutoPostBack="true">
                                   </asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                    TargetControlID="Txt_Dig" ValidChars="K,k">
                                   </cc1:FilteredTextBoxExtender>
                                    &nbsp; <asp:TextBox Style="position: static" ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled"
                                                                                    Width="300px"  ReadOnly="True"></asp:TextBox>
                                   </td>
                            </tr>
                            <tr>
                                <td class="style36" align="left" colspan="2">
                                            
                                    <asp:CheckBox ID="Ch_doc" runat="server" AutoPostBack="True" 
                                        CssClass="SubTitulos" Text="Comisión Docto." Width="207%" />
                                            
                                </td>
                            </tr>
                            <tr>
                                <td class="style48" align="left">
                                 <asp:Label ID="Label3" runat="server" Text="% Comisión" CssClass="Label" Width="78px"></asp:Label>
                                </td>
                                <td align="left" class="style49">
                                   <asp:TextBox ID="txt_comision" runat="server" CssClass="clsDisabled" Width="40px" ReadOnly="True">
                                   </asp:TextBox>
                                   <cc1:MaskedEditExtender ID="txt_comision_MaskedEditExtender" runat="server" 
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                    CultureName="es-ES" CultureThousandsPlaceholder="." CultureTimePlaceholder="" 
                                    Enabled="True" InputDirection="RightToLeft" Mask="999.99" 
                                    TargetControlID="txt_comision" MaskType="Number">
                                   </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style46" align="left">               
                                 <asp:Label ID="Label4" runat="server" Text="Moneda" CssClass="Label">
                                  </asp:Label>
                                </td>
                                <td align="left" class="style47">
                                  <asp:DropDownList ID="Dp_moneda" runat="server" CssClass="clsDisabled" 
                                   AutoPostBack="True" Enabled="False">
                                  </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style38" align="left">       
                                  <asp:Label ID="Label5" runat="server" Text="Mínimo" CssClass="Label">
                                  </asp:Label>
                                </td>
                                <td align="left" class="style39">
                                  <asp:TextBox ID="txt_minima" runat="server" CssClass="clsDisabled" Height="18px" Width="89px" ReadOnly="True">
                                  </asp:TextBox>
                                 <cc1:MaskedEditExtender ID="txt_minima_MaskedEditExtender" runat="server" 
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                    CultureThousandsPlaceholder="." CultureTimePlaceholder="" Enabled="True" 
                                    InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                    TargetControlID="txt_minima" AutoComplete="False">
                                </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style44" align="left">     
                                    <asp:Label ID="Label6" runat="server" Text="Máximo" CssClass="Label"></asp:Label>
                                </td>
                                <td align="left" class="style45">
                                    <asp:TextBox ID="txt_maxima" runat="server" CssClass="clsDisabled" Height="18px" Width="89px" 
                                            ReadOnly="True">
                                    </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="txt_maxima_MaskedEditExtender" runat="server" 
                                     CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                     CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                     CultureThousandsPlaceholder="." CultureTimePlaceholder="" Enabled="True" 
                                     InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                     TargetControlID="txt_maxima" AutoComplete="False">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="style30" align="left" colspan="2">              
                                    <asp:CheckBox ID="Ch_flat" runat="server" CssClass="SubTitulos" 
                                        Text="Comisión Flat" Width="300px" AutoPostBack="True" />
                                            
                                </td>
                            </tr>
                            <tr>
                                <td class="style42" align="left">
                                   <asp:Label ID="Label57" runat="server" CssClass="Label" Text="Mon. Com. Flat">
                                   </asp:Label>
                                </td>
                                <td align="left" class="style43">
                                    <asp:DropDownList ID="Dp_ComFla" runat="server" CssClass="clsDisabled" 
                                      AutoPostBack="True" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style36" align="left">
                                    <asp:Label ID="Label58" runat="server" CssClass="Label" Text="Com. Flat"></asp:Label>
                                </td>
                                <td align="left" class="style37">
                                    <asp:TextBox ID="txt_com_f" runat="server" CssClass="clsDisabled" Width="91px" ReadOnly="True"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="txt_com_f_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="," 
                                        CultureThousandsPlaceholder="." CultureTimePlaceholder="" Enabled="True" 
                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                        TargetControlID="txt_com_f">
                                     </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            </table>
                  </td>
             </tr>
             <tr>
              <td align="right" height="40">
                &nbsp;
              
               <asp:ImageButton ID="IB_Guardar" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';"             
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';" OnClick="IB_Guardar_Click"
                    runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif" __designer:wfdid="w376"
                              AlternateText="Guardar Datos" ValidationGroup="Operacion" >
                </asp:ImageButton>
              
                <asp:ImageButton ID="Ib_Eliminar" runat="server" AlternateText="Eliminar Sub Linea"
                    ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" OnClick="Ib_Eliminar_Click"
                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';" 
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                    Style="position: static" />
               <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_out.gif"
                            OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"    
                       onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" Style="position: static"
                            CausesValidation="False" ToolTip="Limpiar" />   
               
               <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                      onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                      ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" ToolTip="Volver" 
                      style="height: 25px">
               </asp:ImageButton>              
              
              </td>
             </tr>
           
         </table>
                 
                
       <asp:HiddenField ID="SW" runat="server" />
        <uc1:Mensaje ID="Mensaje1" runat="server" />
    </Contenttemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Volver" />
        </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
