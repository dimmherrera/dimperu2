<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_asig_riesg_doctos.aspx.vb" Inherits="Modulos_Operaciones_rightframe_archivos_popup_asig_riesg_doctos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register src="../../Web_Controles/Mensaje.ascx" tagname="Mensaje" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target=_self></base>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <title>Asignación de Riesgo</title>

</head>
<body>
        <script language="javascript">

function DoScroll()

{

var _gridView = document.getElementById("GridViewDiv");

var _header = document.getElementById("HeaderDiv");

_header.scrollLeft = _gridView.scrollLeft;

}

</script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
              <table class="style1">
                         <tr>
                             <td>
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td class="Cuadrado">
                                  <div id="HeaderDiv" style="overflow: hidden; width: 950px">
                 <table class="cabeceraGrilla"  
                        style="border: thin solid #000000; width:1670px" >
                     <tr>
                         <td  style="width:30px"> </td>
                         <td  style="width:80px" align="right">
                            <asp:Label ID="Label13" runat="server" Text="NIT Pag." Width="70px" CssClass="LabelCabeceraGrilla"></asp:Label></td>
                         <td  style="width:200px">
                              <asp:Label ID="Label14" runat="server" Text="Pagador" CssClass="LabelCabeceraGrilla"></asp:Label></td>
                         <td  style="width:100px" align="left">
                              <asp:Label ID="Label72" runat="server" Text="Tipo Doc." Width="70px" CssClass="LabelCabeceraGrilla"></asp:Label></td>
                         <td  style="width:80px">
                             <asp:Label ID="Label25351" runat="server" Text="Nº Otg." Width="52px" CssClass="LabelCabeceraGrilla"></asp:Label>
                         </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label17" runat="server" Text="Nº Docto" Width="70px" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:50px">
                             <asp:Label ID="Label18" runat="server" Text="Cuota" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                             </td>
                         <td  style="width:80px" align="right">
                              <asp:Label ID="Label19" runat="server" Text="Fec.Vcto" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:100px">
                             <asp:Label ID="Label20" runat="server" Text="Estado" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                             </td>
                         <td  style="width:100px">
                              <asp:Label ID="Label21" runat="server" Text="Moneda" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:160px">
                             <asp:Label ID="Label22" runat="server" Text="Saldo Cli." Width="70px" CssClass="LabelCabeceraGrilla"></asp:Label>
                             </td>
                         <td  style="width:160px">
                              <asp:Label ID="Label23" runat="server" Text="Saldo Pag." Width="70px" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:160px">
                              <asp:Label ID="Label24" runat="server" Text="Not.Credito" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label25" runat="server" Text="Notif." Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label26" runat="server" Text="Con Cob." Width="90px" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:100px">
                              <asp:Label ID="Label27" runat="server" Text="Demanda" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label28" runat="server" Text="Obs.Cobranza" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:80px">
                             <asp:Label ID="Label29" runat="server" Text="Pag.Ddr" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                             </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label30" runat="server" Text="Fec.Otorg" Width="100%" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                         <td  style="width:80px">
                              <asp:Label ID="Label31" runat="server" Text="Cod.Cobr" CssClass="LabelCabeceraGrilla"></asp:Label>
                              </td>
                     </tr>
                 </table>
                 </div>
                <div  id="GridViewDiv" style="overflow:scroll; width:950px; height:300px" 
                    onscroll="DoScroll()">
                    <asp:GridView ID="Gr_Documentos" runat="server" AutoGenerateColumns="False" CssClass="formatUltcell"
                        PageSize="1" ShowHeader="False" Width="1670px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ch_sel" runat="server" Width="20" />
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="deu_ide" HeaderText="NIT Pagador" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEUDOR" HeaderText="Pagador" >
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoDocto" HeaderText="Tipo Doc." >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="opo_otg" HeaderText="N&#186; Otorg.">
                              
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dsi_num" HeaderText="N&#186; Docto.">
                            
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dsi_flj_num" HeaderText="Cuo."
                                HtmlEncode="False" >
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_fev_rea" HeaderText="Fec.Vcto" 
                                DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EstadoDocto" HeaderText="Estado" >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="moneda" HeaderText="Moneda" >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_sdo_cli" HeaderText="Saldo Cli." 
                                DataFormatString="{0:###,###,##0}" >
                                <ItemStyle Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_sdo_ddr" HeaderText="Saldo Pag." 
                                DataFormatString="{0:###,###,##0}" >
                                <ItemStyle Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_not_cre" HeaderText="Not.Credito" >
                                <ItemStyle Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dsi_ntf" HeaderText="Notifi." >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dsi_cbz_son" HeaderText="Con Cob." >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOC_FEC_DEM" HeaderText="Demanda" 
                                DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOC_FEC_CAS" HeaderText="Castigo" 
                                DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOC_OBS_COB" HeaderText="Obser.Cobranza" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DOC_PAG_DDR" HeaderText="Pag.Ddr" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OPO_FEC_OTO" HeaderText="Fec.Otorg" 
                                DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="deu_con_cbz" HeaderText="Cod.Cobr" >
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle  CssClass="cabeceraGrilla" />
                        <RowStyle CssClass="formatUltcell" />
                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                    </asp:GridView>
                    
                     
                    
                     </div>
                                 <asp:Label ID="Label1" runat="server" Text="Calificación Subjetiva:" CssClass="Label"></asp:Label>
                                 <asp:DropDownList ID="dr_riesgo" runat="server" CssClass="clsMandatorio">
                                 </asp:DropDownList>  
                 </td>
                         </tr>
                         <tr>
                             <td align="right" valign="top">
                             
                                 <asp:ImageButton ID="btn_guardar" runat="server" 
                                     ImageUrl="~/Imagenes/Botones/boton_guardar_out.gif" 
                                     onmouseout="this.src='../../../Imagenes/Botones/boton_guardar_out.gif';" 
                                        
                                     onmouseover="this.src='../../../Imagenes/Botones/boton_guardar_in.gif';" 
                                     ToolTip="Guardar Asignación" />    
                                  <asp:ImageButton ID="btn_volver" runat="server" 
                                  onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                        onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';"
                                     ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" ToolTip="Volver" />     
                                  
                                  </td>
                         </tr>
                    </table>
                    <uc1:Mensaje ID="Mensaje1" runat="server" />
                    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_volver" />
        </Triggers>
    </asp:UpdatePanel>
             
    
             
    <asp:LinkButton ID="lb_guardar" runat="server"></asp:LinkButton>
             
    
             
    </form>
</body>
</html>
