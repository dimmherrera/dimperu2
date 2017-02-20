<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Codigo Cobranza.aspx.vb" Inherits="ClsCodigoCobranza" Title="Estados de Cobranza" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="Updatepanel_Codigo_Cobranza">
        <ContentTemplate>
            <table cellspacing="1" cellpadding="0" width="100%" border="0" style="position: static;
                text-align: -moz-center" class="Contenido">
                <tr>
                    <td style="text-align: -moz-center" class="Cabecera"
                        align="center">
                        <asp:Label ID="Label12" runat="server" Text="Mantenimiento-Estados de Cobranzas" CssClass="Titulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%" class="Contenido">
                        <table width="100%" border="0">
                            <tr>
                                <td class="Contenido" height="540px" style="padding: 5px; text-align: -moz-center;
                                    width: 100%; border: 0" valign="top" align="center">
                                    <table cellspacing="0" cellpadding="0" border="0" class="Contenido" style="position: static;
                                        text-align: -moz-center">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Código cobranza"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_CodCobranza" runat="server" CssClass="clsDisabled" Enabled="False"
                                                    MaxLength="4" ValidationGroup="Ingreso" Width="50px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_CodCobranza_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_CodCobranza">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="NReq" runat="server" ControlToValidate="Txt_CodCobranza"
                                                    Display="None" ErrorMessage="&lt;b&gt;Código Cobranza&lt;/b&gt;&lt;br /&gt;Ingrese Código de Cobranza."
                                                    ValidationGroup="ingreso" />
                                                <cc1:ValidatorCalloutExtender ID="NReqE" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="NReq" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Prioridad"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Prioridad" runat="server" CssClass="clsMandatorio" MaxLength="3"
                                                    ValidationGroup="ingreso" Width="50px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_Prioridad_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_Prioridad">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="Priori" runat="server" ControlToValidate="Txt_Prioridad"
                                                    Display="None" ErrorMessage="&lt;b&gt;Prioridad&lt;/b&gt;&lt;br /&gt;Ingrese Prioridad."
                                                    ValidationGroup="ingreso" />
                                                <cc1:ValidatorCalloutExtender ID="Validatorcalloutextender1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="Priori" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Plazo (días)"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_PlazoDias" runat="server" CssClass="clstxt" MaxLength="3" Width="50px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_PlazoDias_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_PlazoDias">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Código de acción nuevo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Cod_Acc_Nvo" runat="server" CssClass="clstxt" MaxLength="4"
                                                    Width="50px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="Txt_Cod_Acc_Nvo_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="Txt_Cod_Acc_Nvo">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsMandatorio" ValidationGroup="ingreso"
                                                    Width="350px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Des_Cob" runat="server" ControlToValidate="Txt_Descripcion"
                                                    Display="None" ErrorMessage="&lt;b&gt;Descripción&lt;/b&gt;&lt;br /&gt;Ingrese Descripción."
                                                    ValidationGroup="ingreso" />
                                                <cc1:ValidatorCalloutExtender ID="Validatorcalloutextender2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="Des_Cob" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Acción"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Accion" runat="server" CssClass="clstxt" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Por Fechas"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButton ID="RB_FechaVen" runat="server" CssClass="Label" GroupName="Fecha"
                                                    Text="Por Vcto." Checked="True" />
                                                <asp:RadioButton ID="RB_FechaGest" runat="server" CssClass="Label" GroupName="Fecha"
                                                    Text="Por Gestión" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Gestionar"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:RadioButton ID="RB_GestionarSi" runat="server" CssClass="Label" GroupName="Gestion"
                                                    Text="Si" Checked="True" />
                                                <asp:RadioButton ID="RB_GestionarNo" runat="server" CssClass="Label" GroupName="Gestion"
                                                    Text="No" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="Chk_Generar" runat="server" CssClass="Label" Text="No Generar Gestion (fecha de proxima  gestion igual a fecha del dia)" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" class="Cabecera" style="width: 850px">
                                        <tr>
                                            <td align="center" Width="90px">
                                                <asp:Label ID="Label7" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                                            </td>
                                            <td align="center" width="150px">
                                                <asp:Label ID="Label13" runat="server" CssClass="LabelCabeceraGrilla" Text="Código"></asp:Label>
                                            </td>
                                            <td align="center" width="250px">
                                                <asp:Label ID="Label14" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripción"></asp:Label>
                                            </td>
                                            <td align="center" width="150px">
                                                <asp:Label ID="Label15" runat="server" CssClass="LabelCabeceraGrilla" Text="Prioridad"></asp:Label>
                                            </td>
                                            <td align="center" width="150px">
                                                <asp:Label ID="Label16" runat="server" CssClass="LabelCabeceraGrilla" Text="Gen.Gestión"></asp:Label>
                                            </td>
                                            <td align="center" colspan="0" rowspan="0" width="151px">
                                                <asp:Label ID="Label17" runat="server" CssClass="LabelCabeceraGrilla" Text="Gestión"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="Contenido" Height="250px" ScrollBars="Auto"
                                        Width="850px">
                                        <asp:GridView ID="Gr_Cobranza" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            fPageSize="1000" ShowHeader="False" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Button1" runat="server" ToolTip='<%# Eval("cco_num") %>' 
                                                            ImageUrl="~/Images/bt_ver.gif" onclick="Button1_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cco_num" HeaderText="Código">
                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cco_des" HeaderText="Descripcion">
                                                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cco_pri" HeaderText="Prioridad" NullDisplayText="0">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cco_not_ges" HeaderText="Gen. Gestion" NullDisplayText="N">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cco_ges_son" HeaderText="Gestionar" NullDisplayText="N">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="foco" runat="server" BorderStyle="None" BorderWidth="0px" Height="0px"
                                                            Width="0px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="0px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                            <RowStyle CssClass="formatUltcell" />
                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            </table>
                            <tr>
                                <td align="right">
                                    <br />
                                    <asp:TextBox ID="Txt_CodCobranzaOculto" runat="server" BorderWidth="0px" Width="0px"></asp:TextBox>
                                    <asp:ImageButton ID="IB_Nuevo" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                                        OnClick="IB_Nuevo_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" 
                                        ToolTip="Nuevo" Visible="False" />
                                    <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Guardar_out.gif"
                                        OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_in.gif';" ValidationGroup="ingreso"
                                        ToolTip="Guardar" />
                                    <asp:ImageButton ID="IB_Eliminar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif"
                                        OnClick="IB_Eliminar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';" ToolTip="Eliminar" />
                                    <asp:ImageButton ID="IB_Limpiar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Limpiar_OUT.GIF"
                                        OnClick="IB_Limpiar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Limpiar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Limpiar_in.gif';" ToolTip="Limpiar" />
                                </td>
                            </tr>
                        
                        <tr>
                            <td>
                            </td>
                        </tr>
                </tr>
            </table>
            <asp:HiddenField ID="pos" runat="server" />
            <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="Detalle" runat="server"></asp:LinkButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
