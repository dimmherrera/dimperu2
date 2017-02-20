<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Preliminar.ascx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_Preliminar" %>
<link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />

<table >
    <tr>
        <td>
            <table id="DatosNeg" cellpadding="0" cellspacing="0" width="800">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label9" runat="server" Text="Legal" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table>
                            <tr>
                                <td>
                                    <table id="DatosNegHija" cellpadding="0" cellspacing="0" width="300">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label27" runat="server" Text="Datos Pagaré" CssClass="Label" 
                                                    Font-Bold="True"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Tipo" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Tipo" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Moneda" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Moneda" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Monto" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Monto" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Mandato" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="Rb_Mandato" runat="server" CssClass="Label" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="S">Si</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label10" runat="server" Text="Cant. Dom." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Can_Dom" runat="server" CssClass="clsDisabled" 
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Observación" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="Txt_Observacion" runat="server" CssClass="clsDisabled" 
                                                    ReadOnly="true" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Contratos" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="70">
                                                
                                                <asp:GridView ID="GV_Contratos" runat="server" CssClass="formatUltcell" 
                                                    AutoGenerateColumns="False" Width="400">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Contratos" />
                                                        <asp:BoundField HeaderText="Hora de Solicitud" />
                                                    </Columns>
                                                </asp:GridView>
                                                </asp:Panel>
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
<br />

<table>
    <tr>
        <td>
            <table id="Table1" cellpadding="0" cellspacing="0" width="800">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label7" runat="server" Text="Verificación" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                        <table>
                            <tr>
                                <td>
                                    <table id="Table2" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Tipo Docto." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_TipoDocto" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label16" runat="server" Text="Verificador" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Verificador" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="Observación" CssClass="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Tipo Envio" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_TipoEnvio" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label25" runat="server" Text="Est. Verif." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dp_Estado_Ver" runat="server" CssClass="clsDisabled" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td rowspan="2" valign="top">
                                                <asp:TextBox ID="Txt_Obser_Ver" runat="server" CssClass="clsDisabled" 
                                                    ReadOnly="true" TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="Hora Ing." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Hora_Ing" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text="Hora Verif." CssClass="Label"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="Txt_Hora_Ver" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="50px"></asp:TextBox>
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

<br />

<table>
    <tr>
        <td valign="top">
            <table id="Table3" cellpadding="0" cellspacing="0" width="300">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label17" runat="server" Text="Operaciones" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                       <table id="Table4" cellpadding="0" cellspacing="0" width="300">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" Text="Doctos." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Doctos" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label19" runat="server" Text="Mto. Girar" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Mto_Girar" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label20" runat="server" Text="Mto. Anticipo" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Mto_Ant" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label21" runat="server" Text="Hora Ingreso" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Hora_Ing_Ope" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        </table>
                    </td>
                </tr>
            </table>
        </td>
          <td valign="top">
            <table id="Table5" cellpadding="0" cellspacing="0" width="500">
                <tr>
                    <td class="Cabecera">
                        <asp:Label ID="Label14" runat="server" Text="Asistente" CssClass="SubTitulos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido">
                       <table id="Table6" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label22" runat="server" Text="Hor. Rec. UAC" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Hor_Rec" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label23" runat="server" Text="Hor. Env. Ope." CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Hor_Env_Ope" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="Estado" CssClass="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Estado" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label24" runat="server" Text="Observacion" CssClass="Label"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="Txt_Obser_Asi" runat="server" CssClass="clsDisabled" ReadOnly="true"
                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        </table>
                    </td>
                </tr>
            </table>
        </td>
     </tr>
</table>