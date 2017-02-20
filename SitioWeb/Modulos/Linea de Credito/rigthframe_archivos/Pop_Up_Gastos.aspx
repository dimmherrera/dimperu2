<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pop_Up_Gastos.aspx.vb" Inherits="Clientes_Pop_Up_Gastos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <title>Gastos por Cliente</title>
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

        #form1
        {
            height: 630px;
            width: 628px;
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

        #Table1
        {
            width: 594px;
            height: 419px;
        }

        #tabla3
        {
            margin-right: 0px;
        }

        .style9
        {
            width: 600px;
        }
        .style10
        {
            filter: progid:dximagetransform.microsoft.gradient(gradienttype=0,startcolorstr=#F6F9FC,endcolorstr=white);
            width: 600px;
            border-left: 1px solid #666666;
            border-right: 1px solid #666666;
            border-bottom: 1px solid #666666;
        }

    </style>
    
    <script language="javascript" src="../FuncionesPrivadasJS/Empresas.js"></script>
    
</head>
<body>
    <form id="form1" runat="server" style="padding: 5px">
      <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    <ContentTemplate>
    <table width="600px">
     <tr>
       <td class="Cabecera" width="600px">  
         <asp:Label ID="Label4" runat="server"  Text="Gastos por Cliente" CssClass="Titulos"></asp:Label>
       </td>
     </tr>
     <tr>
        <td class="Contenido"  align="center" style="text-align:-moz-center">
        <table>
         <tr>
          <td>
         <table width="600px">
           <tr>
            <td align="left">
              <asp:Label ID="Label1" runat="server" CssClass="Label"  Text="Identificación"></asp:Label>
               &nbsp;
               <asp:TextBox ID="Txt_Rut" runat="server" CssClass="clsDisabled" Width="90px">
                            </asp:TextBox>
                            <cc1:MaskedEditExtender ID="Txt_Rut_MaskedEditExtender" runat="server" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="False" 
                                InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                TargetControlID="Txt_Rut">
                            </cc1:MaskedEditExtender>
                            
               <asp:TextBox ID="Txt_Dig" runat="server" CssClass="clsDisabled" Width="15px" 
                                MaxLength="1" AutoPostBack="true">
               </asp:TextBox>
               <cc1:FilteredTextBoxExtender ID="Txt_Dig_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                TargetControlID="Txt_Dig" ValidChars="K,k">
               </cc1:FilteredTextBoxExtender>      
                &nbsp;
                <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" Width="300px">
                         </asp:TextBox>        
            </td>
           </tr>
         </table>  
         <table>
            <tr>
              <td width="600px">
                 <asp:Panel runat="server" ScrollBars="Auto" Width="600px">
                                <asp:GridView ID="gd_gastos" runat="server" AutoGenerateColumns="False"  CssClass="formatUltcell" Width="98%" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ch_sel" runat="server" AutoPostBack="True" 
                                                                                    />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Cod.Gasto">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo Gasto">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripci&#243;n">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_p_0036">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                                    <AlternatingRowStyle CssClass="formatUltcell" />
                                </asp:GridView>
                  </asp:Panel>               
              </td>
            </tr>
         </table>
         </td>
          </tr>
        <%--  <tr>
            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                
                                                
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                                AlternateText="Anterior" Height="25px" />
                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                
                                                
                                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                AlternateText="Siguiente" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
          </tr>--%>
         </table>
        </td>
     </tr>
      <tr>
            <td align="right" height="50">
            
            <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                        OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                        
                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" 
                    Style="position: static" ToolTip="Guardar" Height="25px" />
                    
                    
                        <asp:ImageButton ID="IB_Volver" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" runat="server"
                            ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" ToolTip="Volver" 
                         style="height: 25px">
                        </asp:ImageButton>
            
                    
             </td>
          </tr>
     </table>
    </ContentTemplate>
        <asp:HiddenField ID="SW" runat="server" />
        <uc1:Mensaje ID="Mensaje1" runat="server" />
    </form>
</body>
</html>
