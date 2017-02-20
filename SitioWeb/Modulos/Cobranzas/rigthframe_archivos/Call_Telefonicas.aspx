<%@ Page Language="VB" MasterPageFile="~/Modulos/Master/MasterPage.master" AutoEventWireup="false"
    CodeFile="Call_Telefonicas.aspx.vb" Inherits="ClsCallTelefonico" Title="Control de Llamadas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" src="../FuncionesProvadasJS/Call_Telefonicas.js"></script>
    <asp:UpdatePanel runat="server" ID="UP_AsigCliente">
        <ContentTemplate>
        
            <table cellspacing="1" cellpadding="0" width="100%" class="Contenido">
                <tbody>
                    <tr>
                        <td style="height: 31px" class = "Cabecera">
                            <asp:Label ID="Label40" runat="server" CssClass="Titulos" Text="Cobranza - Ingreso de Gestiòn (Llamadas)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Contenido" align="center" height="570" valign="top" style="padding: 10px">
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label1" runat="server" CssClass="SubTitulos" 
                                                Text="Seleccionar Cobrador Telefónico"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="left">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align=right >
                                                            <asp:Label ID="Label41" runat="server" CssClass="Label" 
                                                                Text="Cobrador Telefónico"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dp_Ejecutivos" runat="server" CssClass="clsMandatorio" 
                                                                Width="232px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align=right >
                                                            <asp:CheckBox ID="CB_Reemplazo" runat="server" CssClass="Label" AutoPostBack="true"
                                                                Text="Reemplazar Cobrador Telefónico"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="Dp_Ejecutivos_Reemplazo" runat="server" CssClass="clsDisabled" 
                                                                Enabled="False" Width="232px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="SubTitulos" 
                                                Text="Totales por Código de Cobranza"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center" height="300" valign="top" style="padding: 5px">
                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 734px">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Auto" Height="280px">
                                                                <asp:GridView ID="GV_TotalPorCodigo" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                                    CssClass="formatUltcell" EnableTheming="True" ShowHeader="True">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CodigoCobranza" ApplyFormatInEditMode="True" HeaderText="Código">
                                                                            <ItemStyle HorizontalAlign="center" Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DesCodigoCobranza" HeaderText="Descripción">
                                                                            <ItemStyle HorizontalAlign="Left" Width="250px"/>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="CantidadDeudores" HeaderText="# Pagadores" >
                                                                            <ItemStyle Width="90px" HorizontalAlign="Right"/>
                                                                        </asp:BoundField>    
                                                                        <asp:BoundField DataField="CantidadDoctos" HeaderText="# Doctos." >
                                                                            <ItemStyle Width="90px" HorizontalAlign="Right"/>
                                                                        </asp:BoundField>    
                                                                        <asp:BoundField DataField="MontoDoctos" HeaderText="Monto Doctos.$">
                                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                        </asp:BoundField>    
                                                                        <asp:BoundField DataField="Gestionado" HeaderText="Gestionado" >
                                                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle  CssClass="cabeceraGrilla" />
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
                                                </tbody>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="Cabecera" align="left">
                                            <asp:Label ID="Label5" runat="server" CssClass="SubTitulos" Text="Totales"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Contenido" align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label42" runat="server" CssClass="Label" Text="Monto Total de Doctos Gestionados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label43" runat="server" CssClass="Label" Text="# de Doctos. Gestionados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label44" runat="server" CssClass="Label" Text="# de Pagadores Gestionados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label45" runat="server" CssClass="Label" Text="Monto Total de Doctos. NO Gestionados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="# de Doctos. NO Gestionados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="# de Pagadores No Gestionados"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="total1" runat="server" CssClass="clsDisabled" Text="total1" 
                                                            BorderStyle="Solid" Width="95%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="total2" runat="server" CssClass="clsDisabled" Text="total2" 
                                                            BorderStyle="Solid" Width="89%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="total3" runat="server" CssClass="clsDisabled" Text="total3" 
                                                            BorderStyle="Solid" Width="92%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="total4" runat="server" CssClass="clsDisabled" Text="total4" 
                                                            BorderStyle="Solid" Width="87%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="total5" runat="server" CssClass="clsDisabled" Text="total5" 
                                                            BorderStyle="Solid" Width="93%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="total6" runat="server" CssClass="clsDisabled" Text="total6" 
                                                            BorderStyle="Solid" Width="95%" BorderWidth="1px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="IB_Control_Llamadas" runat="server" 
                                                AlternateText="Control de Llamadas" 
                                                 onmouseover="this.src='../../../Imagenes/Botones/Btn_llam_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/Btn_llam_out.gif';" 
                                                ImageUrl="~/Imagenes/Botones/Btn_llam_out.gif" />
                                           
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="IB_Gestionar" onmouseover="this.src='../../../Imagenes/Botones/boton_gestionar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_gestionar_out.gif';" 
                                                runat="server" ImageUrl="~/Imagenes/Botones/boton_gestionar_out.gif" 
                                                AlternateText="Ingreso de Gestion Telefónica">
                                            </asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="IB_Limpiar" onmouseover="this.src='../../../Imagenes/Botones/boton_limpiar_in.gif';"
                                                onmouseout="this.src='../../../Imagenes/Botones/boton_limpiar_out.gif';" 
                                                runat="server" ImageUrl="~/Imagenes/Botones/boton_limpiar_out.gif" 
                                                AlternateText="Limpia Criterio de Búsqueda">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
             
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="IB_Control_Llamadas" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lb_msj_pta" runat="server" ></asp:LinkButton>
    <asp:LinkButton ID="LinkB_Refresca" runat="server"></asp:LinkButton>
</asp:Content>
