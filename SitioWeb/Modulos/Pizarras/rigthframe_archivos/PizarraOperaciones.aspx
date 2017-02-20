<%@ Page Title="Pizarra de Operaciones" Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false" CodeFile="PizarraOperaciones.aspx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_PizarraOperaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../FuncionesPrivadasJS/PizarraOperaciones.js" type="text/javascript"></script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table id="tb_gral" cellspacing="1" cellpadding="0" width="100%" border="0" class="Contenido">
        <tr>
            <td class = "Cabecera" height="31px">
                <asp:Label ID="Label40" runat="server" CssClass="Titulos" 
                    Text="Operaciones - Recepción de Operaciones"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido" style="padding: 10px; height: 570px;" valign="top" align="center">
            
                <table id="Table1" cellspacing="2" cellpadding="2" border="0">
                    <tr>
                        <td>
                            <table id="tb_Ejecutivos" cellspacing="0" cellpadding="0" border="0" width="330">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" Text="Ejecutivo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 30px; padding: 3px" align="left">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CB_TodosEje" runat="server" CssClass="Label" Text="Todos" AutoPostBack="True" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DP_Ejecutivos" runat="server" CssClass="clsMandatorio" Enabled="true"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table id="tb_cliente" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td class="Cabecera" align="left">
                                        <asp:CheckBox ID="CB_Cliente" runat="server" CssClass="SubTitulos" Text="Por Cliente especifico"
                                            AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Contenido" valign="top" style="height: 30px; padding: 3px" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Identificación" __designer:wfdid="w285"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Style="position: static" ID="Txt_Rut_Cli" TabIndex="1" runat="server"
                                                            CssClass="clsDisabled" Width="90px" ReadOnly="true" ></asp:TextBox>
                                                        <cc2:MaskedEditExtender ID="Txt_Rut_Cli_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="false" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                                                            Mask="999,999,999,999" MaskType="Number" TargetControlID="Txt_Rut_Cli">
                                                        </cc2:MaskedEditExtender>
                                                        <asp:TextBox Style="position: static" ID="Txt_Dig_Cli" TabIndex="1" runat="server"
                                                            CssClass="clsDisabled" Width="15px" ReadOnly="true" MaxLength="1" 
                                                            onkeypress="fnTrapKD(ctl00_ContentPlaceHolder1_Lb_buscar);" AutoPostBack="True"></asp:TextBox>
                                                        <asp:ImageButton ID="IB_AyudaCli" runat="server"
                                                                   ImageUrl="../../../Imagenes/Iconos/155.ICO" Width="20px" Enabled="False"/>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label13" runat="server" __designer:wfdid="w288" CssClass="Label" Style="position: static"
                                                            Text="Razón Soc." Width="70px"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="Txt_Raz_Soc" runat="server" __designer:wfdid="w289" CssClass="clsDisabled"
                                                             ReadOnly="True" Style="position: static" Width="400px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <asp:Panel ID="Panel_GV_Operaciones" runat="server" height="430px">
                    <asp:GridView ID="GV_Operaciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                        HeaderStyle-CssClass="cabeceraGrilla" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("NroOpe") %>' OnClick="Img_Ver_Click" />
                                    <asp:HiddenField ID="HF_RutCli" runat="server" Value='<%# Eval("rut") %>'/>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NroNeg" HeaderText="Nro.">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rut" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cliente" HeaderText="Cliente">
                                <ItemStyle HorizontalAlign="left" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipDoc" HeaderText="Tipo Docto.">
                                <ItemStyle HorizontalAlign="Center" Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaNeg" HeaderText="Fecha Ing.">
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PorAnt" HeaderText="% Anticipo">
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MtoOpe" HeaderText="Monto Doctos.">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoOperacion" HeaderText="Tipo Operación">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle  CssClass="cabeceraGrilla" />
                        <RowStyle CssClass="formatUltcell" />
                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                    </asp:GridView>
                </asp:Panel>
                                                                                        
                <table width="100%">
                <tr>
                <td align="center">
                    <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" />
                    <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" />
                </td>
                </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td align="right" valign="middle" height="50">
            
                
            
                <asp:ImageButton ID="IB_Buscar" runat="server" ImageUrl="~/Imagenes/Botones/Boton_buscar_out.gif" 
                     onmouseover="this.src='../../../Imagenes/Botones/Boton_Buscar_in.gif';"
                     onmouseout="this.src='../../../Imagenes/Botones/Boton_Buscar_out.gif';" 
                     ToolTip="Buscar Operaciones Ingresadas" />
                     
                <asp:ImageButton ID="IB_Limpiar" runat="server" AlternateText="Limpiar" 
                 ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif"
                 onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';" 
                 onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" />  
            </td>
        </tr>
    </table>
    
    <asp:LinkButton ID="lb_ope" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="Lb_buscar" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lb_temas" runat="server"></asp:LinkButton>        
    
    <asp:HiddenField ID="HF_NroOpe" runat="server" />
    
    
    </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <uc1:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>



                            
</asp:Content>

