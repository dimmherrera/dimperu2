<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Resp_Doctos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_Resp_Doctos" %>

<%@ Register Src="../../Web_Controles/Mensaje.ascx" TagName="Mensaje" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self"></base>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="false"
        EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePartialRendering="true">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table cellpadding="0" cellspacing="0" style="width: 984px">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label14" runat="server" CssClass="SubTitulos" Text="Operaciones - Cheques de Operación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="100%" class="cabeceraGrilla" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="25">
                                            </td>
                                            <td align="center" width="130">
                                                <asp:Label ID="Label2" runat="server" Text="NIT Cliente/Pagador" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="180">
                                                <asp:Label ID="Label8" runat="server" Text="Cliente/Pagador" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label1" runat="server" Text="Nº Cheque" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label3" runat="server" Text="Fecha Vcto." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="Label4" runat="server" Text="Estado" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="Label5" runat="server" Text="Monto" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="Label16" runat="server" Text="Mto.Disp" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label6" runat="server" Text="Nº Ope." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="Panel2" runat="server" Height="150px" ScrollBars="Vertical">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                            Width="98%" CssClass="formatUltcell">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Ch_Cheque" runat="server" AutoPostBack="True" OnCheckedChanged="Ch_Cheque_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="chr_cli_deu">
                                                    <ItemStyle Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField>
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="chr_num">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="chr_fev" DataFormatString="{0:d}">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pnu_des">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="chr_mto" DataFormatString="{0:###,###,###.00}">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:###,###,###.00}">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="id_ope">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label15" runat="server" CssClass="SubTitulos" Text="Documentos Operación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="cabeceraGrilla">
                                    <table>
                                        <tr>
                                            <td width="20">
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label7" runat="server" Text="Identificación" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="220">
                                                <asp:Label ID="Label9" runat="server" Text="Pagador" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label10" runat="server" Text="Nº Docto" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="140">
                                                <asp:Label ID="Label11" runat="server" Text="Monto" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="100">
                                                <asp:Label ID="Label19" runat="server" Text="Fecha Emisión" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="100">
                                                <asp:Label ID="Label12" runat="server" Text="Fecha Vcto." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="Label17" runat="server" Text="Estado" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="Label18" runat="server" Text="Nº Ope." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                                            ShowHeader="False" Width="98%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Ch_doc" runat="server" AutoPostBack="True" OnCheckedChanged="Ch_doc_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="deu_ide">
                                                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deu_rso">
                                                    <ItemStyle Width="220px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_num">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_mto" DataFormatString="{0:###,###,###.00}">
                                                    <ItemStyle Width="140px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DSI_FEC_EMI" DataFormatString="{0:d}">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dsi_fev_rea" DataFormatString="{0:d}">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PNU_DES">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ID_OPE">
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btn_asoc" OnClick=" btn_asoc_Click" runat="server" onmouseout="this.src='../../../Imagenes/Botones/boton_asociar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_asociar_in.gif';" ImageUrl="~/Imagenes/Botones/boton_asociar_out.gif" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton2" runat="server" onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton3" runat="server" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:HiddenField ID="pos_chr" runat="server" />
            <asp:HiddenField ID="txt_itemope" runat="server" />
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imagebutton3" EventName="Click" />
        </Triggers>
        
    </asp:UpdatePanel>
    
    </form>
    
</body>
</html>
