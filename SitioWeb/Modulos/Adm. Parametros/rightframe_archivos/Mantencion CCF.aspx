<%@ Page Language="VB" EnableEventValidation="false" MasterPageFile="~/Modulos/Master/MasterPage.master"
    AutoEventWireup="false" CodeFile="Mantencion CCF.aspx.vb" Inherits="Manccf" Title="Niveles de Riesgos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../FuncionesPrivadasJS/Clasificacion.js"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table3" cellspacing="1" cellpadding="0" border="0" width="100%" class="Contenido">
                <tr>
                    <td style="text-align: -moz-center; width: 100%" align="center" class="Cabecera">
                        <asp:Label ID="Label12" runat="server" CssClass="Titulos" Text="Mantenimiento-Niveles de Riesgos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="center" style="width: 100%; height:570px">
                        <table width="700" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" Width="90px">
                                                <asp:Label ID="Label4" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                                            </td>
                                            <td align="center" width="120px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Nº Clasificación"></asp:Label>
                                            </td>
                                            <td align="center" width="190px">
                                                <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Clasificación"></asp:Label>
                                            </td>
                                            <td align="center" width="110px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LabelCabeceraGrilla" Text="Estado"></asp:Label>
                                            </td>
                                            <td align="center" width="200px">
                                                <asp:Label ID="Label3" runat="server" CssClass="LabelCabeceraGrilla" Text="Quien Aprueba"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="Contenido" align="center">
                                    <div style="overflow: auto; width: 100%; position: static; height: 500px; text-align: center"
                                        align="center">
                                        <asp:GridView ID="Gr_Ccf" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="formatUltcell" ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnDetalle" runat="server" ToolTip='<%# Eval("id_ccf") %>' 
                                                            ImageUrl="~/Images/bt_ver.gif" onclick="BtnDetalle_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_ccf" HeaderText="Código.Clasif.">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ccf_des" HeaderText="Clasificación">
                                                    <ItemStyle Width="190px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ccf_est" HeaderText="Estado">
                                                    <ItemStyle Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ccf_tip_apb" HeaderText="Aprobacion">
                                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="cabeceraGrilla" />
                                                                            <RowStyle CssClass="formatUltcell" />
                                                                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 26px" valign="top">
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="btn_nuevo" runat="server" ToolTip="Nueva Clasificación" onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_mod" runat="server" ToolTip="Modificar Clasificación" onmouseover="this.src='../../../Imagenes/Botones/Boton_detalle_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_detalle_out.gif';" 
                                        ImageUrl="~/Imagenes/Botones/boton_detalle_out.gif" Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_suc" runat="server" ToolTip="Sucursales con que trabaja"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_sucursal_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/boton_sucursal_out.gif';"
                                        ImageUrl="~/Imagenes/Botones/boton_sucursal_out.gif" Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_suc_apb" runat="server" ToolTip="Sucursales que aprueban"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_suc_apb_in.gif';" onmouseout="this.src='../../../Imagenes/Botones/boton_suc_apb_out.gif';"
                                        ImageUrl="~/Imagenes/Botones/boton_suc_apb_out.gif" Enabled="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="BTN_FIRMAS" runat="server" ToolTip="Firmas" onmouseover="this.src='../../../Imagenes/Botones/boton_firma_in.gif';"
                                        onmouseout="this.src='../../../Imagenes/Botones/boton_firma_out.gif';" 
                                        ImageUrl="~/Imagenes/Botones/boton_firma_out.gif" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 49px" align="right">
                        <asp:LinkButton ID="LB_Marca_Grilla" runat="server"></asp:LinkButton>
                        <asp:HiddenField ID="pos" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btn_suc" />
        <asp:PostBackTrigger ControlID="BTN_FIRMAS" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="acceso" runat="server"></asp:LinkButton>
</asp:Content>
