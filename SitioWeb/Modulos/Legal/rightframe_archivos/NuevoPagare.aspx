<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Modulos/Master/MasterPage.master" CodeFile="NuevoPagare.aspx.vb" Inherits="NuevoPagare"  Title="Pagarés"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link href="../../../CSS/radcalendar.css" rel="stylesheet"
        type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Tabla Contenedora" border="0" cellpadding="0" cellspacing="1" width="100%" class="Contenido">
                <tr>
                    <td class="Cabecera" height="31">
                        <asp:Label ID="Label14" runat="server" Text="Pagarés" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 590px" class="Contenido" valign="top" align="center">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="padding: 5px;">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <asp:Label ID="Label1" runat="server" Text="Datos Generales" CssClass="SubTitulos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Contenido">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 115px" align="right">
                                                                        <asp:Label ID="Label2" runat="server" Text="NIT Cliente" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Rut_Cli" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                            TabIndex="1"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_Rut_Cli_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="txt_Rut_Cli">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Dig_Cli" runat="server" CssClass="clsMandatorio" Width="20px"
                                                                            TabIndex="2" AutoPostBack="True" MaxLength="1"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_Dig_Cli_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_Dig_Cli" ValidChars="K,k">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server" AlternateText="Ayuda Clientes" ImageUrl="../../../Imagenes/Iconos/155.ICO"
                                                                            Width="20px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" CssClass="clsDisabled" Width="350px"
                                                                            ReadOnly="true"></asp:TextBox>
                                                                        <asp:LinkButton ID="lb_cli" runat="server" TabIndex="0"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 115px" align="right">
                                                                        <asp:Label ID="Label3" runat="server" Text="Nº de Pagaré" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_NPagare" runat="server" CssClass="clsMandatorio" Width="90px"
                                                                            MaxLength="10" TabIndex="4"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txt_NPagare_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_NPagare">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="CB_SinNum" runat="server" Text="Sin Número" CssClass="Label" AutoPostBack="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 115px" align="right">
                                                                        <asp:Label ID="Label4" runat="server" Text="Tipo de Pagare" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_TipoPagare" runat="server" CssClass="clsMandatorio" TabIndex="5">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right" style="width: 80px">
                                                                        <asp:Label ID="Label5" runat="server" Text="Mandato" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:RadioButtonList ID="RB_Mandato" runat="server" CssClass="Label" RepeatDirection="Horizontal"
                                                                                                    AutoPostBack="true">
                                                                                                    <asp:ListItem Selected="True" Value="S">SI</asp:ListItem>
                                                                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                                                                </asp:RadioButtonList>
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
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 114px" align="right">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Tipo Moneda"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_Moneda" runat="server" AutoPostBack="True" CssClass="clsMandatorio"
                                                                            TabIndex="9">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Monto"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_Monto" runat="server" CssClass="clsTxt" TabIndex="6" Width="140px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_Monto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                                            TargetControlID="txt_Monto" MessageValidatorTip="False">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 114px">
                                                                        <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Fecha Suscripción"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_FSuscripcion" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                            TabIndex="7" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_FSuscripcion_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FSuscripcion">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_FSuscripcion_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_FSuscripcion">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Fecha Vecto."></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_FVecto" runat="server" CssClass="clsMandatorio" MaxLength="10"
                                                                            TabIndex="8" Width="90px" AutoPostBack="false"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txt_FVecto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FVecto">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="txt_FVecto_CalendarExtender" runat="server" CssClass="radcalendar"
                                                                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" TargetControlID="txt_FVecto">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 114px" align="right">
                                                                        <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="Drop_estado" runat="server" CssClass="clsMandatorio" AutoPostBack="True"
                                                                            TabIndex="10">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="1" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 107px" align="right">
                                                                                    <asp:Label ID="Label11" runat="server" Text="Fecha Protesto" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_FProtesto" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                                                        Width="90px" MaxLength="10" AutoPostBack="false"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="txt_FProtesto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                        Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FProtesto">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="txt_FProtesto_CalendarExtender" runat="server" Enabled="False"
                                                                                        TargetControlID="txt_FProtesto" CssClass="radcalendar" FirstDayOfWeek="Monday"
                                                                                        Format="dd-MM-yyyy">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Motivo Protesto" CssClass="Label"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="Drop_MotivoProtesto" runat="server" CssClass="clsDisabled"
                                                                                        Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="Cabecera" style="height: 19px">
                                                                        <asp:Label ID="Label13" runat="server" Text=" Antecedentes" CssClass="SubTitulos"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="Contenido">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox ID="txt_Antecedentes" runat="server" CssClass="clsTxt" TextMode="MultiLine"
                                                                                        Width="420px" TabIndex="9" MaxLength="255"></asp:TextBox>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';"
                            AlternateText="Guardar" TabIndex="10" />
                        <asp:ImageButton ID="IB_Elimina" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Eliminar_Out.gif"
                            AlternateText="Eliminar" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';"
                            Enabled="false" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';" />
                        <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Limpiar_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';"
                            AlternateText="Limpiar" />
                        <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';"
                            AlternateText="Volver" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_Id_Docto" runat="server" />
            <asp:HiddenField ID="HF_Est" runat="server" />
            <asp:LinkButton ID="LinkB_Moneda" runat="server"></asp:LinkButton>
            
                         <uc1:Mensaje ID="Mensaje1" runat="server" />
                       
        </ContentTemplate>
     
    </asp:UpdatePanel>
    
     <asp:LinkButton ID="Link_Guarda" runat="server"></asp:LinkButton>
     <asp:LinkButton ID="LinkB_Eliminar" runat="server"></asp:LinkButton>
</asp:Content>