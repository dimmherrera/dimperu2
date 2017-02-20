<%@ Page Language="VB" EnableEventValidation="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="Clasificacion.aspx.vb" Inherits="ClsIngOpe"
    Title="Niveles de Riesgos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Clasificacion.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table3" cellspacing="1" cellpadding="0" border="0" width="100%" class="Contenido">
                <tr>
                    <td style="height:31px;text-align: -moz-center; width: 100%" align="center" class="Cabecera"
                        valign="middle">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantencion-Clasificación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 560px; width: 100%" class="Contenido" valign="top" align="center">
                        <table>
                            <tr>
                                <td valign="top" align="center">
                                    <table class="Contenido">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Código"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_cod" runat="server" CssClass="clsDisabled" Width="48px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_descripcion" runat="server" CssClass="clsMandatorio" Width="250px"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label21" runat="server" CssClass="Label" Height="16px" Text="Tipo Aprobación"
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dr_apb" runat="server" CssClass="clsMandatorio" Width="226px"
                                                    Enabled="False">
                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="1">Aprobada por C.Matriz</asp:ListItem>
                                                    <asp:ListItem Value="2">Se aprueba a si Misma</asp:ListItem>
                                                    <asp:ListItem Value="3">Aprobación Mixta</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Estado Clasificación"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dr_est" runat="server" CssClass="clsMandatorio" Width="250px">
                                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="A">Activo</asp:ListItem>
                                                    <asp:ListItem Value="I">Inactivo</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Criterio"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="Dr_tipo_cfc" runat="server" CssClass="clsMandatorio" Enabled="False"
                                                    Width="226px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Desde"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_dde" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txt_dde_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                    TargetControlID="txt_dde">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Hasta"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_hta" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="txt_hta_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                    TargetControlID="txt_hta">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="Cabecera">
                                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" Width="90px">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                                                        </td>
                                                        <td align="center" width="150px">
                                                            <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Criterio Condición"></asp:Label>
                                                        </td>
                                                        <td align="center" width="100px">
                                                            <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Desde"></asp:Label>
                                                        </td>
                                                        <td align="center" width="100px">
                                                            <asp:Label ID="Label3" runat="server" CssClass="LabelCabeceraGrilla" Text="Hasta"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="Contenido" align="center">
                                                <div style="overflow: auto; width: 100%; position: static; height: 220px; text-align: center"
                                                    align="center">
                                                    <asp:GridView ID="gr_cfc" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        ShowHeader="False" CssClass="formatUltcell">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Selección">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("cfc_obs") %>' 
                                                                        ImageUrl="~/Images/bt_ver.gif" onclick="Button1_Click"
                                                                        />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="cfc_obs" HeaderText="Criterio Condicion">
                                                                <ItemStyle Width="160px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cfc_dde" HeaderText="Desde" DataFormatString="{0:##,###,###,##0.##}">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="cfc_hta" HeaderText="Hasta" DataFormatString="{0:##,###,###,##0.##}">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                                        <RowStyle CssClass="formatUltcell" />
                                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td>
                                                <asp:ImageButton ID="btn_agregar" runat="server" ImageUrl="~/Imagenes/btn_workspace/Agregar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Agregar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Agregar_in.gif';"
                                                    Visible="False" />
                                                <asp:ImageButton ID="btn_quitar" runat="server" ImageUrl="~/Imagenes/btn_workspace/Quitar_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/Quitar_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/Quitar_in.gif';"
                                                    ToolTip="Quitar criterio" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <asp:ImageButton ID="IB_Guadar" runat="server" ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';"
                            ToolTip="Guardar" />
                        <asp:ImageButton ID="btn_limpiar" runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                            ToolTip="Limpiar" />
                        <asp:ImageButton ID="btn_volver" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                            ToolTip="Volver" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:TextBox ID="txt_pos" runat="server" Height="0px" Width="0px"></asp:TextBox>
                        <asp:LinkButton ID="LB_CargaDetalle" runat="server"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <%--Descripcion de clasificacion--%>
            <asp:RequiredFieldValidator ID="RF_Descripcion" runat="server" ControlToValidate="txt_descripcion"
                Display="None" ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br /&gt;Ingrese Descripción."
                Font-Size="8pt" ValidationGroup="Clasificacion" />
            <cc1:ValidatorCalloutExtender ID="VC1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_Descripcion" />
            <%--Tipo de aprobacion--%>
            <asp:RequiredFieldValidator ID="RF_TipoApb" runat="server" ControlToValidate="dr_apb"
                Display="None" ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br /&gt;Seleccione un tipo de Aprobación."
                Font-Size="8pt" ValidationGroup="Clasificacion" InitialValue="0" />
            <cc1:ValidatorCalloutExtender ID="VC2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_TipoApb" />
            <%--Estado--%>
            <asp:RequiredFieldValidator ID="RF_Estado" runat="server" ControlToValidate="dr_est"
                Display="None" ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br/&gt;Seleccione un Estado."
                Font-Size="8pt" ValidationGroup="Clasificacion" InitialValue="0" />
            <cc1:ValidatorCalloutExtender ID="VC3" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_Estado" />
            <%--------------------------------------------------------------------------------------------------------------------------%>
            <%--tipo de clasificacion--%>
            <asp:RequiredFieldValidator ID="RF_Tipo" runat="server" ControlToValidate="Dr_tipo_cfc"
                Display="None" ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br /&gt;Seleccione un Criterio."
                Font-Size="8pt" ValidationGroup="Rango" InitialValue="0" />
            <cc1:ValidatorCalloutExtender ID="VC4" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_Tipo" />
            <%--Desde--%>
            <asp:RequiredFieldValidator ID="RF_Desde" runat="server" ControlToValidate="txt_dde"
                Display="None" ErrorMessage="&lt;b&gt;Clasificación&lt;/b&gt;&lt;br/&gt;Indique el rango Desde."
                Font-Size="8pt" ValidationGroup="Rango" />
            <cc1:ValidatorCalloutExtender ID="VC5" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_Desde" />
            <%--Hasta--%>
            <asp:RequiredFieldValidator ID="RF_Hasta" runat="server" ControlToValidate="txt_hta"
                Display="None" ErrorMessage="<b>Clasificación</b><br/>Indique el rango Hasta."
                ValidationGroup="Rango" Font-Size="8pt" />
            <cc1:ValidatorCalloutExtender ID="VC6" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="RF_Hasta" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
</asp:Content>
