<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DetalleNegociacion.ascx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_DetalleNegociacion" %>
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

<table cellpadding="0" cellspacing="0" >
    <tr>
        <td>
            <table id="DatosNeg" cellpadding="0" cellspacing="0" width="600">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label9" runat="server" Text="Tasas" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table id="DatosNegHija" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" Text="% Tasa Base" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Tas_Bas" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true" Width="70px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="% Spread" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Spread" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true" Width="70px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="% Puntos" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Puntos" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true" Width="70px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="% Negocio" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Tas_Neg" runat="server" CssClass="clsDisabled" 
                                        ReadOnly="true" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table id="Table1" cellpadding="0" cellspacing="0" width="200">
    <tr>
        <td class="Cabecera">
            <asp:Label ID="Label5" runat="server" Text="Tipo Operacion" CssClass="SubTitulos"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="Contenido" align="center">
            <asp:TextBox ID="Txt_Tip_Ope" runat="server" CssClass="clsDisabled" ReadOnly="true" Width="180"></asp:TextBox>
        </td>
    </tr>
</table>
        </td>
    </tr>
</table>
<br />

<table>

    <tr>
        <td>
            <table id="Table2" cellpadding="0" cellspacing="0" width="400">
    <tr>
        <td class="Cabecera">
            <asp:Label ID="Label6" runat="server" Text="Comision Por Documento" 
                CssClass="SubTitulos"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="Contenido">
            <table id="Table3" cellpadding=0 cellspacing=0 width="100%">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label7" runat="server" Text="Moneda" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Moneda" runat="server" CssClass="clsDisabled" 
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label8" runat="server" Text="Mínimo" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Minimo" runat="server" CssClass="clsDisabled" 
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label10" runat="server" Text="% Com." CssClass="Label"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="Txt_Por_Com" runat="server" CssClass="clsDisabled" ReadOnly="true" 
                            Width="70px"></asp:TextBox>
        </td>
                    <td align="right">
                        <asp:Label ID="Label11" runat="server" Text="Máximo" CssClass="Label"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="Txt_Maximo" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
        </td>
                </tr>
            </table>
      </td>
    </tr>
    
</table>
        </td>
        <td>
            <table id="Table4" cellpadding="0" cellspacing="0" width="400">
    <tr>
        <td class="Cabecera">
            <asp:Label ID="Label12" runat="server" Text="Comision Flat" CssClass="SubTitulos"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="Contenido">
            <table id="Table5" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label13" runat="server" Text="Moneda" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Mon_Flat" runat="server" CssClass="clsDisabled" 
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label14" runat="server" Text="% Anticipar" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Por_Ant" runat="server" CssClass="clsDisabled" 
                            ReadOnly="true" Width="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label15" runat="server" Text="Comisión" CssClass="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Com_Flat" runat="server" CssClass="clsDisabled" 
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right">
                      
                    </td>
                    <td>
                      
                    </td>
                </tr>
            </table>
      </td>
    </tr>
    
</table>
        </td>
    </tr>
    
</table>

<br />

<table>

    <tr>
        <td valign="top">
            <table id="Table6" cellpadding="0" cellspacing="0" width="400">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label16" runat="server" Text="Pago" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table id="Table7" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label17" runat="server" Text="Forma Pago" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_For_Pag" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label19" runat="server" Text="Banco" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Banco" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label18" runat="server" Text="Cta. Cte." CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                        Width="200"></asp:TextBox>
                                    <asp:CheckBox ID="CB_Ant14" runat="server" CssClass="Label" 
                                        Text="Antes 14 Hrs." />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table id="Table8" cellpadding="0" cellspacing="0" width="400">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label21" runat="server" Text="Observaciones" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table id="Table9" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="Label22" runat="server" Text="Comentario" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Comentario" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                        Height="35px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="Label24" runat="server" Text="Instrucciones" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Instrucciones" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                        Height="35px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    
</table>
