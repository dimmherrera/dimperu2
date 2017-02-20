<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Bancos.aspx.vb" Inherits="Bancos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Mantenedor de Bancos</title>
   
    <base target =_self></base>
     <link href="../../../CSS/Estilos.css"rel="stylesheet" type="text/css" />
    <script language="javascript" src="../FuncionesPrivadasJS/Bancos.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Funciones.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Grilla.js"></script>
    <script language="javascript" src="../../../FuncionesJS/Ventanas.js"></script>

</head>

<body bottommargin="5" leftmargin="15" rightmargin="5" topmargin="20">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
        <table border="0" cellpadding="-1" cellspacing="-1" width="700px">
               <tr>
                <td class="Cabecera">
                   <asp:Label ID="Label4" runat="server" CssClass="SubTitulos" Text="Datos Generales del Banco"></asp:Label>
                 </td>
            </tr>
            <tr>
                <td class="Contenido">
                                 
                    <table border="0" cellspacing="1" width="100%">
                        <tr>
                            <td align="right" colspan="1">
                                <asp:Label ID="Label1" runat="server" Text="Banco" CssClass="Label"></asp:Label></td>
                            <td align="left">
                                <asp:DropDownList ID="DP_Bancos" runat="server" AutoPostBack="True" CssClass="clsDisabled"
                                    Width="250px" Enabled="False">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="CCI"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Txt_Cta_Cte" runat="server" CssClass="clsDisabled" 
                                    Width="250px" ReadOnly="True" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td align="right">
                               <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Tipo Cuenta"></asp:Label>
                           </td>
                           <td align="left">
                              <asp:DropDownList ID="DP_Tip_Cta" runat="server" CssClass="clsDisabled" Width="250px" Enabled="false"></asp:DropDownList> 
                           </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Tipo Moneda"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DP_Tip_Cta0" runat="server" CssClass="clsDisabled" Enabled="false" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                </td>
                            <td align="left">
                                <asp:CheckBox ID="CB_Deposito" runat="server" CssClass="Label" Text="Depósito" 
                                    Enabled="False" /></td>
                        </tr>
                       <tr>
                            <td align="center" colspan="2">
                                <asp:Panel ID="Panel_GV_Bancos" runat="server" height="250px">
                                
                                    <asp:GridView ID="GV_Bancos" runat="server"  AutoGenerateColumns="False" CssClass="formatUltcell"
                                        EnableTheming="True" ShowHeader="true">
                                        <PagerSettings Position="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("codigo") %>'
                                                        OnClick="Img_Ver_Click" style="height: 13px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Deposito" HeaderText="Dep." ItemStyle-HorizontalAlign="Center">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" Width="50px" />
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Banco" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle Font-Bold="True" Width="200px" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="Codigo_Sucursal" HeaderText="id Suc.">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>--%>
                                           <%-- <asp:BoundField DataField="Sucursal" HeaderText="Sucursal">
                                                <HeaderStyle Font-Bold="True" Width="150px" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Cuenta_Corriente" HeaderText="Cta. Cte.">
                                                <HeaderStyle Font-Bold="True" Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HF_TipCta" runat="server" Value= '<%#Eval("Tip_Cta")%> '/>
                                                    <asp:HiddenField ID="HF_Bco" runat="server" Value='<%#Eval("Codigo_Banco")%>' />  
                                                    <asp:HiddenField ID="HF_Suc" runat="server" Value='<%#Eval("Codigo_Sucursal")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="0px"/>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <RowStyle CssClass="formatUltcell" />
                                    <AlternatingRowStyle CssClass="formatUltcellAlt" />
                                    <HeaderStyle Font-Bold="True" CssClass="cabeceraGrilla" />        
                                    </asp:GridView>
                                </asp:Panel>
                      
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                        
                                            <asp:ImageButton ID="IB_Prev" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_izq_out.gif"
                                                    onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_izq_out.gif';" 
                                                    onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_izq_in.gif';" 
                                                     AlternateText="Anterior" Height="25px"  />
                                            <asp:ImageButton ID="IB_Next" runat="server" ImageUrl="../../../Imagenes/btn_workspace/flecha_der_out.gif"
                                                        onmouseout="this.src='../../../Imagenes/btn_workspace/flecha_der_out.gif';" onmouseover="this.src='../../../Imagenes/btn_workspace/flecha_der_in.gif';"
                                                     AlternateText="Siguiente"/>
                                                </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>
                    
                  </td>
            </tr>
            <tr>
                <td align="right" height="50">
                 
                    <asp:ImageButton ID="IB_Guardar" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Guardar_Out.gif"
                        OnClick="IB_Guardar_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Guardar_Out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Guardar_In.gif';" 
                        Style="position: static" AlternateText="Guardar banco" />
                 
                    <asp:ImageButton ID="IB_EliminarBanco" runat="server"
                        ImageUrl="~/Imagenes/Botones/Boton_Eliminar_out.gif" OnClick="IB_EliminarBanco_Click"
                        onmouseout="this.src='../../../Imagenes/Botones/Boton_Eliminar_out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Eliminar_in.gif';"
                        Style="position: static" ToolTip="Eliminar" />
                 
                    <asp:ImageButton ID="IB_NuevoBanco" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Nuevo_out.gif"
                        OnClick="IB_NuevoBanco_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Nuevo_out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Nuevo_in.gif';" 
                        Style="position: static" AlternateText="Nuevo banco" />
                 
                    <asp:ImageButton ID="IB_Volver" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                        OnClick="IB_Volver_Click" onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" 
                        Style="position: static" AlternateText="Volver a pantalla anterior" />
                   
                     <asp:ImageButton ID="ME" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                         onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" 
                        Style="position: static" AlternateText="Cuenta corriente Moneda Nacional" />
                    
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../Imagenes/Botones/Boton_Volver_Out.gif"
                         onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';"
                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_In.gif';" 
                        Style="position: static" AlternateText="Cuenta corriente Moneda Nacional" />
                    &nbsp;</td>
            </tr>
            
        </table>
           <asp:HiddenField ID="HF_Ctc" runat="server" />
           <asp:HiddenField ID="CodBco" runat="server" />
           <asp:HiddenField ID="HF_Pos" runat="server" />
           <asp:HiddenField ID="SW" runat="server" />
            
         
          
           <asp:LinkButton ID="ActBancos" runat="server"></asp:LinkButton>
            
            <uc1:Mensaje ID="Mensaje1" runat="server" />
    
    </ContentTemplate>
    </asp:UpdatePanel>
          <asp:LinkButton ID="LB_Guardar" runat="server"></asp:LinkButton>  
           <asp:LinkButton ID="LB_Eliminar" runat="server"></asp:LinkButton>
    </form>
      
</body>
</html>
