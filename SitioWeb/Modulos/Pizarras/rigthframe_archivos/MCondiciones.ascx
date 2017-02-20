<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MCondiciones.ascx.vb" Inherits="Modulos_Pizarras_rigthframe_archivos_MCondiciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>


<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
    
        <table cellspacing="10" width="900px">
            <tr>
                <td valign="top" align="left">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Labe2l18" runat="server" CssClass="Label" Text="Fecha Cumplimiento"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Fecha_Cumplimiento" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                    Width="90px"></asp:TextBox>
                                <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999" TargetControlID="Txt_Fecha_Cumplimiento">
                                </cc2:MaskedEditExtender>  
                                <cc2:CalendarExtender ID="Txt_Fecha_Cumplimiento_CalendarExtender" runat="server"
                                    CssClass="radcalendar" Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                    TargetControlID="Txt_Fecha_Cumplimiento">
                                </cc2:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Descripción"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Descripcion" runat="server" CssClass="clsDisabled" ReadOnly="True"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Estado"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DP_EstadoCondicion" runat="server" CssClass="clsDisabled" Enabled="False"
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="IB_Agregar" runat="server" 
                        ImageUrl="~/Imagenes/btn_workspace/Agregar_Out.gif" 
                        onmouseover="this.src='../../../Imagenes/btn_workspace/Agregar_in.gif';"
                        onmouseout="this.src='../../../Imagenes/btn_workspace/Agregar_Out.gif';" 
                        ToolTip="Agregar" />
                    &nbsp;
                    <asp:ImageButton ID="IB_Quitar" runat="server" 
                        ImageUrl="~/Imagenes/btn_workspace/Quitar_Out.gif" 
                        onmouseover="this.src='../../../Imagenes/btn_workspace/Quitar_in.gif';"
                        onmouseout="this.src='../../../Imagenes/btn_workspace/Quitar_Out.gif';" 
                        ToolTip="Quitar" />
                </td>
            </tr>
            <tr>
                <td>
                   
                    <asp:Panel ID="Panel_GV_Condiciones" runat="server" width="100%" height="200px">
                    
                        <asp:GridView ID="GV_Condiciones" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                            ShowHeader="true">
                            <Columns>
                                <asp:BoundField DataField="id_cdn" HeaderText="Código.">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_des" HeaderText="Descripción">
                                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_fec_com" HeaderText="Fecha Cump." DataFormatString="{0:d}">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_usr_ing" HeaderText="Usuario Ingresa">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cdn_usr_apb" HeaderText="Usuario Aprueba.">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("id_cdn") %>' OnClick="Img_Ver_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle  CssClass="cabeceraGrilla" />
                            <RowStyle CssClass="formatUltcell" />
                            <AlternatingRowStyle CssClass="formatUltcellAlt" />
                        </asp:GridView>
                    </asp:Panel>
                    <%--</div>--%>
                </td>
            </tr>
            <tr>
            <td>
            <table width="100%">
            <tr>
            <td align="center">
      
            
                <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                                            onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';"/>
                <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                                            onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" 
                                                                            
                    onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';" 
                    style="width: 25px" />
            </td>
            </tr>
            
            </table>
            </td>
            </tr>
            
        </table>
        
        <asp:HiddenField ID="HF_NroCon" runat="server" />
        <asp:HiddenField ID="HF_Ope" runat="server" />
        
        <%--Controles de Mensaje--%>
        <asp:LinkButton ID="LinkMensajeCon" runat="server"></asp:LinkButton>
        
        <cc2:modalpopupextender id="ModalPopupExtender1" runat="server" targetcontrolid="LinkMensajeCon"
            enableviewstate="False" popupcontrolid="Panel_Msj_Con" backgroundcssclass="modalBackground"
            popupdraghandlecontrolid="Panel_Msj_Con"></cc2:modalpopupextender>
            
        <asp:Panel ID="Panel_Msj_Con" runat="server" Width="300px" Height="200px" Style="display: none;">
            <table class="Contenido">
                <tbody>
                    <tr>
                        <td style="width: 325px" align="left">
                            <nobr>
                                                                <asp:Image ID="imginfo12" runat="server" ImageUrl="~/Imagenes/Iconos/Info.gif"></asp:Image>
                                                                <asp:Label ID="Lbl_errorCon" runat="server" CssClass="Titulos" Width="200px" Height="12px"></asp:Label>&nbsp;
                                                            </nobr>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 325px; height: 91px">
                            <asp:TextBox ID="Txt_Msj_Con" runat="server" CssClass="clsTxt" Width="299px" Height="78px"
                                TextMode="MultiLine" Text="Prueba"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="IB_Ok_Con" OnClick="IB_Ok_Con_Click" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                                BorderColor="Black"></asp:ImageButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="IB_Prev" />
    <asp:PostBackTrigger ControlID="IB_Next" />
    </Triggers>
</asp:UpdatePanel>
