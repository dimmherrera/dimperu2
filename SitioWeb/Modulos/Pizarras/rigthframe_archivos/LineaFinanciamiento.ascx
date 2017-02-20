<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LineaFinanciamiento.ascx.vb"
    Inherits="Modulos_Pizarras_rigthframe_archivos_LineaFinanciamiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
<table>
    <tr>
        <td>
            <table style="position: static" cellpadding="0" cellspacing="0" width="300px" border="0">
                <tr>
                    <td class="Cabecera" align="left">
                        <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" Text="Actas Adjuntas"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" valign="top" align="left">
                        <asp:GridView Style="position: static" ID="GV_Actas" runat="server" CssClass="formatUltcell"
                            Width="98%" HorizontalAlign="Center" EnableTheming="true" CellPadding="1" AutoGenerateColumns="false">
                            <FooterStyle BorderStyle="Dashed"></FooterStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IB_Ver" runat="server" ToolTip='<%# Eval("act_img_id") %>' 
                                            ImageUrl="~/Imagenes/Botones/lupa.gif" onclick="IB_Ver_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="1px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="act_img_desc" HeaderText="Descripcion">
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<table style="position: static" cellspacing="2" cellpadding="0" width="1200" border="0">
    <tbody>
        <tr>
            <td valign="top" align="left">
                <table style="position: static" id="Table2" cellspacing="0" cellpadding="0" width="99%"
                    border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label2" runat="server"
                                    CssClass="SubTitulos" Text="Datos Linea"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" height="90">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="right" width="120px">
                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Nro. de Linea"></asp:Label>
                                            </td>
                                            <td valign="middle" align="left">
                                                <asp:TextBox ID="Txt_NroLinea" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label8" runat="server" CssClass="Label" Text="Monto Solicitud"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Mto_Sol" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label5" runat="server" CssClass="Label" Text="Estado Linea"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList Style="position: static" ID="DP_EstadoLinea" runat="server" CssClass="clsDisabled"
                                                    Width="100px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label6" runat="server" CssClass="Label" Text="Monto Disponible"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Mto_Dis" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label7" runat="server" CssClass="Label" Text="Fecha Solicitud"></asp:Label>
                                            </td>
                                            <td style="height: 22px" align="left">
                                                <asp:TextBox ID="Txt_Fec_Sol" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="70px"></asp:TextBox>
                                            </td>
                                            <td style="height: 22px" align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                &nbsp;
                <table style="position: static" id="Table4" cellspacing="0" cellpadding="0" width="99%"
                    border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label27" runat="server"
                                    CssClass="SubTitulos" Text="Tipo de Comisión"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="center" width="60">
                                                <asp:RadioButton Style="position: static" ID="RB_Normal" runat="server" CssClass="Label"
                                                    Text="Normal" Enabled="False" GroupName="Comision"></asp:RadioButton>
                                            </td>
                                            <td valign="middle" align="center" width="60">
                                                <asp:RadioButton Style="position: static" ID="RB_Especial" runat="server" CssClass="Label"
                                                    Text="Especial" Enabled="False" GroupName="Comision"></asp:RadioButton>
                                            </td>
                                            <td valign="middle" align="center">
                                                <asp:TextBox Style="position: static" ID="Txt_Obs_Com" runat="server" CssClass="clsDisabled"
                                                    Width="320px"  ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top" align="left">
                <table style="position: static" id="Table3" cellspacing="0" cellpadding="0" width="100%"
                    border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label9" runat="server"
                                    CssClass="SubTitulos" Text="Aprobación Comite"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" height="90">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="right" width="80px">
                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Fecha Desde"></asp:Label>
                                            </td>
                                            <td valign="middle" align="left">
                                                <asp:TextBox ID="Txt_Fec_Dsd" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="right" valign="middle" width="100px" >
                                                <asp:Label ID="Label19" runat="server" CssClass="Label" Style="position: static"
                                                    Text="Fecha Resolución"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle" >
                                                <asp:TextBox ID="Txt_Fec_Res" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label17" runat="server" CssClass="Label"
                                                    Text="Fecha Hasta"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Fec_Hta" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                                    Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label Style="position: static" ID="Label21" runat="server" CssClass="Label"
                                                    Text="Monto Aprobado"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Txt_Mto_Apr" runat="server" CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table style="position: static" id="Table5" cellspacing="0" cellpadding="0" width="100%"
                    border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label30" runat="server"
                                    CssClass="SubTitulos" Text="Observación"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left">
                                <asp:TextBox Style="position: static" ID="Txt_Observacion" runat="server" CssClass="clsDisabled"
                                    Width="95%" Height="40px" MaxLength="250" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top">
                <table style="position: static" id="Table6" cellspacing="0" cellpadding="0" width="100%"
                    border="0">
                    <tr>
                        <td class="Cabecera" align="left">
                            <asp:Label Style="left: 8px; position: static; top: -14px" ID="Label32" runat="server"
                                CssClass="SubTitulos" Text="% Exceso De Línea Permitido"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" valign="top" align="left">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label33" runat="server" CssClass="Label" Text="Porcentaje: "></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txt_Exc_Apr" runat="server" CssClass="clsDisabled" Width="90px"
                                            MaxLength="5" ReadOnly="True"></asp:TextBox>
                                        <cc2:filteredtextboxextender id="Txt_Exc_Apr_FilteredTextBoxExtender" runat="server"
                                            enabled="True" filtertype="Custom, Numbers" targetcontrolid="Txt_Exc_Apr"
                                            validchars=",">
                                        </cc2:filteredtextboxextender>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<br />
<table style="position: static" id="Tcl_Grillas" height="150" cellspacing="0" cellpadding="0"
    border="0">
    <tbody>
        <tr>
            <td valign="top" align="left">
                <table style="position: static" cellspacing="0" cellpadding="0" width="300" border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label ID="Label26" runat="server" CssClass="SubTitulos" Text="Porcentaje a Anticipar"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left" height="50">
                                <asp:Panel Style="position: static" ID="Panel3" runat="server" Height="90px" ScrollBars="Vertical">
                                    <asp:GridView Style="position: static" ID="GV_PorcentajeAnt" runat="server" CssClass="formatUltcell"
                                        Width="98%" PageSize="3" HorizontalAlign="Center" EnableTheming="True" CellPadding="2"
                                        AutoGenerateColumns="False">
                                        <FooterStyle BorderStyle="Dashed" />
                                        <Columns>
                                            <asp:BoundField DataField="id_P_0031_des" HeaderText="T.P.">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="apc_pct" HeaderText="%">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="apc_ver_son" HeaderText="Ver.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="apc_not_son" HeaderText="Not.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="apc_cob_son" HeaderText="Cob.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top">
                <table style="position: static" cellspacing="0" cellpadding="0" width="220" border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label ID="Label31" runat="server" CssClass="SubTitulos" Text="SubLineas (Productos)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left" height="50">
                                <asp:Panel Style="position: static" ID="Panel1" runat="server" Height="90px" ScrollBars="Vertical">
                                    <asp:GridView Style="position: static" ID="GV_Productos" runat="server" CssClass="formatUltcell"
                                        Width="99%" PageSize="3" HorizontalAlign="Center" EnableTheming="True" CellPadding="2"
                                        AutoGenerateColumns="False">
                                        <FooterStyle BorderStyle="Dashed" />
                                        <Columns>
                                            <asp:BoundField DataField="id_P_0031_des" HeaderText="Producto">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sbl_mto" HeaderText="Monto" DataFormatString="{0:###,###,##0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left" height="1px">
                                <asp:Label ID="LblLineaPro" runat="server" CssClass="SubTitulos" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top">
                <table style="position: static" cellspacing="0" cellpadding="0" width="450" border="0">
                    <tbody>
                        <tr>
                            <td class="Cabecera" align="left">
                                <asp:Label ID="Label42" runat="server" CssClass="SubTitulos" Text="Pagadores"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="center" height="50">
                                <asp:Panel Style="position: static" ID="Panel2" runat="server" Height="90px" ScrollBars="Vertical">
                                    <asp:GridView Style="position: static" ID="GV_Deudor" runat="server" CssClass="formatUltcell"
                                        Width="99%" PageSize="3" HorizontalAlign="Center" EnableTheming="True" CellPadding="2"
                                        AutoGenerateColumns="False">
                                        <FooterStyle BorderStyle="Dashed" />
                                        <Columns>
                                            <asp:BoundField DataField="deu_ide" HeaderText="Identificación">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="deu_nom" HeaderText="Razón Social">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="sbl_mto" HeaderText="Mto. Sub-Linea"
                                                DataFormatString="{0:###,###,##0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sbl_pct" HeaderText="%">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="Contenido" valign="top" align="left" height="1px">
                                <asp:Label ID="LblLineaDeu" runat="server" CssClass="SubTitulos" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<asp:HiddenField ID="NroLinea" runat="server" />
<asp:HiddenField ID="Txt_Pos_Lin" runat="server" />
<asp:LinkButton ID="LB_CargaDatosLinea" runat="server"></asp:LinkButton>