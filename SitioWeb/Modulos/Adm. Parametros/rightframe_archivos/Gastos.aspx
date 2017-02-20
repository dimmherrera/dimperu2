<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Gastos.aspx.vb" Inherits="ClsGastos" Title="Mantenimiento Gastos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../FuncionesPrivadasJS/Grila.js" type="text/javascript"></script>

    <asp:UpdatePanel runat="server" ID="Updatepanel_Gastos">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="1" class="Contenido">
                <tbody>
                    <tr>
                        <td class="Cabecera" width="100%" align="center" style="text-align: -moz-center">
                            <asp:Label ID="Label25" runat="server" Text="Mantenimiento-Gastos" CssClass="Titulos"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" style="padding: 10px; height: 540px; width:100%; text-align:-moz-center" valign="top" align="center" >
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="Cabecera" align="left" style="text-align: -moz-left">
                                        <asp:Label ID="Label12" runat="server" Text="Datos Gastos" CssClass="SubTitulos"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" align="left" >
                                        <table style="width: 858px">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text=" Sucursal"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="Dp_Sucursal" runat="server" CssClass="clsMandatorio" Width="216px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Tipo de Gasto"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="Dp_Tipo_Gasto" runat="server" CssClass="clsMandatorio" Enabled="False"
                                                            Width="216px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Monto Gasto"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Mto_Gasto" runat="server" CssClass="clsMandatorio" MaxLength="10"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="Txt_Mto_Gasto_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number"
                                                            TargetControlID="Txt_Mto_Gasto">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsMandatorio" Height="20px"
                                                            Width="600px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButton ID="Rd_activo" runat="server" CssClass="Label" Enabled="False" GroupName="Gasto"
                                                            Text="Activo" />
                                                        <asp:RadioButton ID="Rd_Inactivo" runat="server" CssClass="Label" Enabled="False"
                                                            GroupName="Gasto" Text="Inactivo" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        &nbsp;
                                                        <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Val.Contable"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_VAL_CON" runat="server" CssClass="clsMandatorio" MaxLength="2"
                                                            Width="30px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                       <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Aplic.IVA"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:CheckBox ID="CB_IVA" runat="server" CssClass="Label" Enabled="false"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:HiddenField ID="HF_Codgto" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="center" style="text-align: -moz-center; height: 350px" valign="top">
                                        <asp:Panel ID="Panel_Gr_Gastos" runat="server" Width="100%" Height="250px" ScrollBars="None">
                                            <asp:GridView ID="Gr_Gastos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                                ShowHeader="true" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Selección">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("id_gto") %>' ImageUrl="~/Images/bt_ver.gif"
                                                                OnClick="Button1_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id_gto" HeaderText="Codigo Gto.">
                                                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo_Gasto" HeaderText="Tipo de Gasto">
                                                        <ItemStyle Width="260px" HorizontalAlign="Left"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="gto_mto" HeaderText="Monto">
                                                        <ItemStyle Width="140px" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="gto_des" HeaderText="Descripción">
                                                        <ItemStyle Width="280px" HorizontalAlign="Left"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                        <ItemStyle Width="100px" HorizontalAlign="Left"/>
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lb_TipGasto" runat="server" Text='<%#Eval("id_p_0036")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="0px"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lb_Val" runat="server" Text='<%#Eval("val_con")%>' Visible="false"></asp:Label> 
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="0px"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lb_IVA" runat="server" Text='<%#Eval("gto_iva")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="0px" />
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
                                    <td align="center">
                                        <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"
                                            AlternateText="Anterior" />
                                        <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                            AlternateText="Siguiente" />
                                    </td>
                                </tr>
                            </table>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%; text-align: -moz-right" valign="top">
                            <br />
                            <asp:HiddenField ID="HF_PosGto" runat="server" />
                            <asp:LinkButton ID="Link_Gto" runat="server"></asp:LinkButton>
                            <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_buscar_out.gif"
                                onmouseout="this.src='../../../Imagenes/Botones/Boton_buscar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';" />
                            <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                                OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_In.gif';" AlternateText="Nuevo">
                            </asp:ImageButton><asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_Out.gif"
                                OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" AlternateText="Guardar"
                                Enabled="false"></asp:ImageButton>
                            <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_Out.gif"
                                OnClick="IB_Eliminar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_Out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_In.gif';" AlternateText="Elimina"
                                Enabled="false"></asp:ImageButton>
                            <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_Out.gif"
                                OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_Out.gif';"
                                onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_In.gif';" AlternateText="Limpiar">
                            </asp:ImageButton>
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="Link_Guardar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Link_Eliminar" runat="server"></asp:LinkButton>
</asp:Content>
