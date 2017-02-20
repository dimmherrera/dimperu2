<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetallePagos.aspx.vb" Inherits="Modulos_Tesorería_rightframe_archivos_DetallePagos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../../WebControles/Cargando.ascx" tagname="Cargando" tagprefix="uc7" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de Pago</title>
    <base target="_self" />
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../../../FuncionesJS/Funciones.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Ventanas.js" type="text/javascript"></script>
    <script src="../../../FuncionesJS/Grilla.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/Pagos.js" type="text/javascript"></script>
    <script src="../FuncionesPrivadasJS/Pagos.js" type="text/javascript"></script>
    <link href="../../../CSS/radcalendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <asp:UpdatePanel ID="UP_General" runat="server" UpdateMode="Conditional" >
    
        <ContentTemplate>
        
            <table id="tb_gral" cellspacing="0" cellpadding="0" width="950px" 
        border="0">
                <tr>
                    <td class = "Cabecera" height="31px">
                        <asp:Label ID="Label40" runat="server" CssClass="Titulos" 
                            Text="Detalle  de Pago"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Contenido" style="padding: 10px 0px 0px 10px; height: 500px" valign="top" >
                    
                        <table width="99%" cellpadding="0" border="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera">
                                    <asp:Label ID="Label1" runat="server" Text="Modos de Pagos" 
                                        CssClass="SubTitulos"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top">
                                <div id="Div1" style="overflow: scroll; width: 99%; height: 120px" >
                                    <asp:GridView ID="GV_Pagos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo Documento">
                                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="id_dpo" HeaderText="N° Docto. Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_mto" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="banco" HeaderText="Banco">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_fec_emi" HeaderText="Fecha Emi." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dpo_fev" HeaderText="Fecha Vcto." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="plaza" HeaderText="Plaza">
                                                <ItemStyle HorizontalAlign="Right" Width="150px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />                                        

                                    </asp:GridView>
                                    
                                </div> 
                                </td>
                               
                            </tr>
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0" border="1" width="100">
                                    <tr>
                                        <td class="Label" bgcolor="#FFCC99" align="center">D.N.C.</td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="99%" cellpadding="0" border="0" cellspacing="0">
                            <tr>
                                <td class="Cabecera" style="width:300px">
                                    <asp:Label ID="Label4" runat="server" Text="Detalle de Pagos" CssClass="SubTitulos"></asp:Label>
                                </td> 
                                <td class="Cabecera" style="width:700px">
                                    <asp:Label ID="Label2" runat="server" Text="Documentos o Cuentas por Cobrar" CssClass="SubTitulos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Contenido" valign="top" style="width:350px">
                                <div id="Div2" style="overflow: scroll; width: 350; height: 150px" >
                                    <asp:GridView ID="GV_Detalle" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Seleccion">
                                               <ItemTemplate>
                                                  <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" OnClick="Button1_Click"
                                                    ToolTip='<%# Eval("Id_Tipo") %>' /></ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                <ItemStyle HorizontalAlign="Left" Width="180px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Monto" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                </div> 
                                </td>
                                <td class="Contenido" valign="top">
                                
                                <div id="Div3" style="overflow: scroll; width: 600px; height: 150px" >
                                    <asp:GridView ID="Gv_Doctos" runat="server" AutoGenerateColumns="False" 
                                        CssClass="formatUltcell">
                                        <Columns>
                                            <asp:BoundField DataField="RutDeu" HeaderText="NIT Pagador">
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NombreDeu" HeaderText="Razón social">
                                                <ItemStyle HorizontalAlign="left" Width="150px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pnu_atr_003" HeaderText="T.D.">
                                                <ItemStyle HorizontalAlign="Center" Width="50px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dsi_num" HeaderText="N° Docto.">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_abo" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_int" HeaderText="Interes">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cabeceraGrilla" />
                                        <RowStyle CssClass="formatUltcell" />
                                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    </asp:GridView>
                                    <asp:GridView ID="GV_CxC" runat="server" AutoGenerateColumns="False" 
                                        CssClass="formatUltcell">
                                        <Columns>
                                            <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo Cuenta">
                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="id_cxc" HeaderText="N°Cuenta">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cxc_fec" HeaderText="Fecha Gen." DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cxc_des" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="left" Width="250px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_abo" HeaderText="Monto">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ing_mto_int" HeaderText="Interes">
                                                <ItemStyle HorizontalAlign="Right" Width="100px"/>
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
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Lb_Motivo" runat="server" Text="Motivo Protesto" CssClass="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DP_Motivo" runat="server" CssClass="clsMandatorio" Width="150">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Lb_Moneda" runat="server" CssClass="Label" Text="Moneda"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DP_Moneda" runat="server" CssClass="clsMandatorio" 
                                        Width="100">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Lb_Comision" runat="server" CssClass="Label" 
                                        Text="Comision Prot."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Comision" runat="server" AutoPostBack="True" 
                                        CssClass="clsMandatorio"></asp:TextBox>
                                    <cc2:MaskedEditExtender ID="Txt_Comision_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        InputDirection="RightToLeft" Mask="999,999,999,999" MaskType="Number" 
                                        TargetControlID="Txt_Comision">
                                    </cc2:MaskedEditExtender>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Lb_Iva" runat="server" CssClass="Label" Text="Iva"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txt_Iva" runat="server" CssClass="clsDisabled" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" height="50">
                        <asp:ImageButton ID="IB_Aceptar" runat="server" 
                            ImageUrl="~/Imagenes/Botones/Boton_Aprobar_out.gif" 
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Aprobar_out.gif';" 
                            onmouseover="this.src='../../../Imagenes/Botones/Boton_Aprobar_in.gif';" 
                            TabIndex="1" ToolTip="Aceptar Anulación de Pago" Height="25px" />
                        <asp:ImageButton ID="IB_Cancelar" runat="server" 
                            onmouseover="this.src='../../../Imagenes/Botones/boton_volver_in.gif';"
                            onmouseout="this.src='../../../Imagenes/Botones/boton_volver_out.gif';" 
                            ImageUrl="~/Imagenes/Botones/boton_volver_out.gif"
                            AlternateText="Cancelar" TabIndex="3" ToolTip="Cancelar"></asp:ImageButton> 
                    </td>
                </tr>
            </table>
            
            
           
            <asp:LinkButton ID="LB_BuscarDetallePago" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_Buscar" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_CargaPagos" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscaDocCxC" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="LB_BuscarCliente" runat="server"></asp:LinkButton>
            
            <asp:HiddenField ID="HF_Pos_Ing" runat="server" />
            <asp:HiddenField ID="HF_Id_Ing" runat="server" />
            
            <asp:HiddenField ID="HF_Pos_Doc_CxC" runat="server" />
            <asp:HiddenField ID="HF_Tipo" runat="server" />
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="IB_Cancelar" />
        </Triggers>
   
        
    </asp:UpdatePanel>  
    
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_General">
        <ProgressTemplate>
            <uc7:Cargando ID="Cargando1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    
    </form>
    
</body>

</html>
