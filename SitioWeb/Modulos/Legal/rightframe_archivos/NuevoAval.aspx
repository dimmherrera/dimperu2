<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master" CodeFile="NuevoAval.aspx.vb" Inherits="NuevoAval"  Title="Avales"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>



<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                <table width="100%" border="0" cellpadding="0" cellspacing="1"   class="Contenido">
                    <tr>
                        <td class = "Cabecera" style="height:31px" >
                            <asp:Label ID="Label23" runat="server" Text="LEGAL - MANTENCION DE Avales" CssClass="Titulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" style="height:590px; padding:5px" valign="top" align="center">
                            <table id="Tabla General">
                                <tr>
                                    <td>
                                        <table id="Cliente" border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 714px">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label1" runat="server" Text="Cliente" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="NIT Cliente" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="txt_Rut_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                    TargetControlID="Txt_Rut_Cli">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" AutoPostBack="True"
                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txt_Dig_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                    TargetControlID="Txt_Dig_Cli" ValidChars="K,k">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                              <td>
                                                                <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" 
                                                                 ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" 
                                                                />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_Raz_Soc" runat="server" ReadOnly="true" CssClass="clsDisabled"
                                                                    Width="470px"></asp:TextBox>
                                                            </td>
                                                          
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Aval" border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 713px">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label3" runat="server" Text="Aval" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td class="style1" style="width: 69px" align="right">
                                                                <asp:Label ID="Label4" runat="server" Text="NIT Aval" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Rut_Aval" runat="server" CssClass="clsMandatorio" Width="90px"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="txt_Rut_Aval_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" Mask="999,999,999,999" MaskType="Number" 
                                                                    TargetControlID="txt_Rut_Aval" InputDirection="RightToLeft">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Dig_Aval" runat="server" CssClass="clsMandatorio" 
                                                                    Width="20px" MaxLength="1" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txt_Dig_Aval_FilteredTextBoxExtender" 
                                                                    runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                                    TargetControlID="txt_Dig_Aval" ValidChars="K,k">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_RSocialAval" ReadOnly="false" CssClass="clsMandatorio" runat="server"
                                                                    Width="495px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 713px" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label5" runat="server" Text="Domicilo Particular" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table style="width: 706px">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label6" runat="server" Text="Dirección" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txt_Dir_Paticular" runat="server" CssClass="clsMandatorio" 
                                                                    Width="617px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label7" runat="server" Text="Ciudad" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Ciudad" runat="server" CssClass="clsMandatorio" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label8" runat="server" Text="Comuna" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Comuna" runat="server" CssClass="clsMandatorio" 
                                                                    Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Domicilio Comercial" border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 714px">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label9" runat="server" Text="Domicilio Comercial" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido" >
                                                    <table style="width: 708px">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label10" runat="server" Text="Dirección" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txt_Dir_Comercial" runat="server" CssClass="clsMandatorio" 
                                                                    Width="614px" MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style7" align="right">
                                                                <asp:Label ID="Label11" runat="server" Text="Ciudad" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Ciudad_Comercial" runat="server" CssClass="clsMandatorio"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right"> 
                                                                <asp:Label ID="Label12" runat="server" Text="Comuna" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Comuna_Comercial" runat="server" 
                                                                    CssClass="clsMandatorio" Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width:714px" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                        <td class="Cabecera">
                                            <asp:Label ID="Label24" runat="server" Text="Patrimonio" CssClass="SubTitulos"></asp:Label>
                                        </td>
                                        </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table id="Patrimonio" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label13" runat="server" Text="Patrimonio" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Patri" runat="server" CssClass="clsTxt" Width="200px" 
                                                                    MaxLength="50"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label14" runat="server" Text="Reg. Matrimonial" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Reg_Matri" runat="server" CssClass="clsMandatorio">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Datos Generales" style="width: 715px;" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td class="Cabecera">
                                                    <asp:Label ID="Label15" runat="server" Text="Datos Generales" CssClass="SubTitulos"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Contenido">
                                                    <table>
                                                        <tr>
                                                            <td class="style8" align="right" style="width: 110px">
                                                                <asp:Label ID="Label16" runat="server" Text="Tipo Aval" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_TipoAval" runat="server" CssClass="clsMandatorio" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8" align="right" style="width: 110px">
                                                                <asp:Label ID="Label17" runat="server" Text="Estado Aval" CssClass="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drop_Est_Aval" runat="server" CssClass="clsMandatorio" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label18" runat="server" Text="Notaría" CssClass="Label" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Notaria" runat="server" CssClass="clsTxt" Width="400px" 
                                                                    Visible="false" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8" align="right" style="width: 110px">
                                                                <asp:Label ID="Label19" runat="server" Text="F. Est. Situación" CssClass="Label" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_F_Est_Sit" runat="server" CssClass="clsTxt" Width="90px" 
                                                                    MaxLength="10" AutoPostBack="True" Visible="false"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="txt_F_Est_Sit_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_F_Est_Sit">
                                                                </cc1:MaskedEditExtender>
                                                                <cc1:CalendarExtender ID="txt_F_Est_Sit_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                                                    TargetControlID="txt_F_Est_Sit">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label20" runat="server" Text="Monto" CssClass="Label" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Mto" runat="server" CssClass="clsTxt" Visible="false"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="txt_Mto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                    TargetControlID="txt_Mto">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 110px">
                                                                <asp:Label ID="Label21" runat="server" Text="F. Junta Ext." CssClass="Label" Visible="false"></asp:Label>
                                                            </td>
                                                            <td Width="90px">
                                                                <asp:TextBox ID="txt_JuntaExt" runat="server" CssClass="clsTxt" Visible="false" 
                                                                    Width="90px" MaxLength="10" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="txt_JuntaExt_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_JuntaExt">
                                                                </cc1:MaskedEditExtender>
                                                                <cc1:CalendarExtender ID="txt_JuntaExt_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_JuntaExt">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label22" runat="server" Text="Plazo" CssClass="Label" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Plazo" runat="server" CssClass="clsTxt" Width="60px" Visible="false"
                                                                    MaxLength="3"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txt_Plazo_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_Plazo">
                                                                </cc1:FilteredTextBoxExtender>
                                                                
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
                    <tr>
                        <td align="right">
                           
                            <asp:HiddenField ID="HF_id_Aval" runat="server" />
                            <asp:HiddenField ID="HF_Estado" runat="server" />
                            <asp:ImageButton ID="IB_Guardar" runat="server" AlternateText="Guardar" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" />
                           
                         
                           <asp:ImageButton ID="IB_Elimina" runat="server" AlternateText="Elimina" ImageUrl="../../../Imagenes/Botones/Boton_Eliminar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';" 
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';" 
                                Enabled="False" />
                           <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" 
                                ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" />
                            <asp:ImageButton ID="IB_Volver" runat="server" AlternateText="Volver" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" />
                            
                        </td>
                    </tr>
                </table>
                
                <uc1:Mensaje ID="Mensaje1" runat="server" />
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="IB_Volver" />
              
            </Triggers>
        </asp:UpdatePanel>
         <asp:LinkButton ID="LinkB_Elimina" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
        
  </asp:Content>

