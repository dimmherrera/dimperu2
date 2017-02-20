<%@ Page Language="VB" AutoEventWireup="false" CodeFile="doctos_deudor.aspx.vb" Inherits="Modulos_Recaudacion_doctos_deudor" %>


       <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<base target=_self></base>
    <title>Documentos para Pagar</title>
    
    <script language=javascript src="../../../FuncionesJS/Ventanas.js"></script>
<script language=javascript>

    function SelecionaDocto(Posicion) {
        window.document.forms[0].hf_posicion.value = Posicion;
        return;
    }




//    function DoScroll() {
//        var _gridView = document.getElementById("GridViewDiv");
//        var _header = document.getElementById("HeaderDiv");
//        _header.scrollLeft = _gridView.scrollLeft;
//    }
</script>
    <link href="../../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table >
        <tr>
            <td class="Cabecera">
                <asp:Label ID="Label33" runat="server" CssClass="SubTitulos" 
                    Text="Documentos para Pagar"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Contenido">
   
                <%--<div id="HeaderDiv" style="overflow: hidden; width: 960px">
                                    <table class="Cabecera" width="1820px">
                                        <tr>
                                            <td width="20" align="center">
                                                <asp:ImageButton ID="IB_SeleccionDoctos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif" />
                                            </td>
                                            <td width="80" align="left">
                                                <asp:Label ID="Label16" runat="server" Text="Identificación" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="250" align="left">
                                                <asp:Label ID="Label17" runat="server" Text="Cliente" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="60" align="left">
                                                <asp:Label ID="Label18" runat="server" Text="T.D." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80" align="left">
                                                <asp:Label ID="Label19" runat="server" Text="N° Docto." 
                                                    CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="60" align="center">
                                                <asp:Label ID="Label20" runat="server" Text="N°Cuo." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80" align="center">
                                                <asp:Label ID="Label21" runat="server" Text="Fec.Vcto" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80" align="center">
                                                <asp:Label ID="Label22" runat="server" Text="Moneda" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td  width="120" align="left">
                                                <asp:Label ID="Label31" runat="server" Text="Saldo Cli" 
                                                    CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120" align="left">
                                                <asp:Label ID="LB_Saldo" runat="server" Text="Saldo Deu" 
                                                    CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="LB_Saldo0" runat="server" Text="Tasa M/N" 
                                                    CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120" align="left">
                                                <asp:Label ID="Label24" runat="server" Text="Interes" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120" align="left">
                                                <asp:Label ID="Label25" runat="server" Text="Nota Credito" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="150" align="left">
                                                <asp:Label ID="Label26" runat="server" Text="Total a Pagar" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120" align="left">
                                                <asp:Label ID="Label27" runat="server" Text="Mto. a Pagar" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            <td width="120" align="left">
                                                <asp:Label ID="Label28" runat="server" Text="Factor Cambio" CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                              <td width="120" align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Monto Doc." CssClass="LabelCabeceraGrilla"></asp:Label>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </div>--%>
                
                
                <%-- <div id="GridViewDiv" style="overflow: scroll; width: 960px; height: 150px" 
                    onscroll="DoScroll()">--%>
                    
                <asp:Panel ID="Panel_gr_documentos" runat="server" width="960px" height="230px" ScrollBars="Vertical">
                
                    <asp:GridView ID="gr_documentos" runat="server" AutoGenerateColumns="False" 
                        CssClass="formatUltcell" Width="1820px" ShowHeader="true">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="IB_SeleccionDoctos" runat="server" ImageUrl="~/Imagenes/Iconos/check.gif"
                                     OnClick="IB_SeleccionDoctos_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ch_Doc" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="Ch_Doc_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CLI_IDC" HeaderText="Rut" >
                            <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente">
                            <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoDoctoCorta" HeaderText="T.D.">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DSI_NUM" HeaderText="N° Docto.">
                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dsi_flj_num" HeaderText="N°Cuo.">
                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_fev" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fec.Vcto">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_sdo_cli" NullDisplayText="0" HeaderText="Saldo Cli">
                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_sdo_ddr" NullDisplayText="0" HeaderText="Saldo Deu">
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Tasa M/N">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_tmc" runat="server" Width="90px" text='<%#Eval("tasa") %>' 
                                        OnTextChanged="txt_tmc_textchanged" AutoPostBack="True" 
                                        CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="Interes" NullDisplayText="0" HeaderText="Interes" >
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Nota Credito">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_nota_cre" runat="server" AutoPostBack="True" 
                                        ontextchanged="txt_nota_cre_TextChanged" Width="120px" Text="" 
                                        CssClass="clsDisabled" ReadOnly="True"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DSI_MTO" HeaderText="Total a Pagar" >
                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Mto. a Pagar">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_mto_pag" runat="server" AutoPostBack="True" 
                                        ontextchanged="txt_mto_pag_TextChanged" Width="120px" 
                                        CssClass="clsDisabled" ReadOnly="True"  ></asp:TextBox>
                                  
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ope_fac_cam" HeaderText="Factor Cambio">
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DSI_MTO" HeaderText="Monto Doc.">
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="cabeceraGrilla" />
                    </asp:GridView>
                    </asp:Panel>
                   <%-- </div>
--%>
            </td>
        </tr>
        <tr>
            <td align="right">
             <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" CssClass="Label" 
                                            Text="Total Seleccionado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_tot_pag" runat="server" CssClass="clsDisabled" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" BackColor="#FFCC99" Width="150px">Documentos con Pago</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btn_aceptar" runat="server" 
                                            ImageUrl="~/Imagenes/Botones/boton_aceptar_out.gif" OnClick="Btn_aceptar_Click" />
                                    </td>
                                </tr>
                            </table>
                           <asp:HiddenField ID="hf_posicion" runat="server" />
            </td>
        </tr>
    </table>
  
    <asp:LinkButton ID="LinkPagos" runat="server"></asp:LinkButton>
        <cc2:modalpopupextender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkPagos"
            EnableViewState="False" PopupControlID="Panel_PagosAnt" BackgroundCssClass="modalBackground"
            PopupDragHandleControlID="Panel_PagosAnt">
        </cc2:modalpopupextender>
        <asp:Panel ID="Panel_PagosAnt" runat="server" Style="display: none">
            <table id="nn" class="Contenido">
                <tr>
                    <td>
                        <asp:GridView ID="GridPagos" runat="server" CssClass="formatUltcell" AutoGenerateColumns="False"
                            PageSize="1" AllowSorting="True" ShowHeader="False">
                            <FooterStyle CssClass="cabeceraGrilla" />
                            <Columns>
                                <asp:BoundField DataField="id_doc" HeaderText="N° Docto.">
                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                </asp:BoundField>
                                <asp:BoundField DataField="id_cxc" HeaderText="N° cxc">
                                    <ItemStyle HorizontalAlign="Right" Width="80" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Est. Ingreso">
                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ing_fec" HeaderText="Fec. Ingr" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="center" Width="80" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ing_mto_tot" HeaderText="Monto" DataFormatString="{0:###,###,###}">
                                    <ItemStyle HorizontalAlign="Right" Width="150" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Modulo" HeaderText="Modulo">
                                    <ItemStyle HorizontalAlign="center" Width="100" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ejecutivo" HeaderText="Recaudor">
                                    <ItemStyle HorizontalAlign="center" Width="300" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="cabeceraGrilla"></HeaderStyle>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="IB_Imprir_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Imprimir_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Imprimir_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Imprimir_in.gif';" />
                        <asp:ImageButton ID="IB_Cerrar_Pagos" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif"
                            onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" />
                    </td>
                </tr>
            </table>
        </asp:Panel>  
  
  
  
    <asp:HiddenField ID="hf_tmc" runat="server" />
        <asp:HiddenField ID="hf_deu" runat="server" />
                <%--Controles de Mensaje--%>
       <asp:LinkButton ID="LinkMensaje" runat="server"></asp:LinkButton>
       <cc2:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="LinkMensaje"
                                      EnableViewState="False" PopupControlID="Panel_Mensaje" BackgroundCssClass="modalBackground"
                                      PopupDragHandleControlID="Panel_Mensaje">
                                  </cc2:ModalPopupExtender>
       <asp:Panel ID="Panel_Mensaje" runat="server" Width="300px" Height="200px" style="display:none">
                                      <table class="Contenido">
                                          <tbody>
                                              <tr>
                                                  <td style="width: 325px" align="left">
                                                      <nobr>
                                            <asp:Image ID="imgexclam" runat="server" ImageUrl="~/Imagenes/Iconos/Info.gif"
                                                BorderStyle="None" Visible="False"></asp:Image>
                                            <asp:Image ID="imgerror" runat="server" ImageUrl="~/Imagenes/Iconos/error.gif" __designer:wfdid="w2"
                                                Visible="False"></asp:Image>
                                            <asp:Image ID="imginfo" runat="server" ImageUrl="~/Imagenes/Iconos/Info.gif"
                                                __designer:wfdid="w3" Visible="False"></asp:Image>
                                            <asp:Image ID="Img_pregunta" runat="server" ImageUrl="~/Imagenes/Iconos/Question.gif"
                                                Width="27px" __designer:wfdid="w24" Visible="False"></asp:Image>
                                            <asp:Label ID="Lbl_error" runat="server" CssClass="Titulos" Width="200px" Height="12px"></asp:Label>&nbsp;
                                        </nobr>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 325px; height: 91px">
                                                      <asp:TextBox ID="TextBox1" runat="server" CssClass="clsTxt" Width="299px" Height="78px"
                                                          TextMode="MultiLine"></asp:TextBox>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="center">
                                                      <asp:ImageButton ID="Okbutton"  runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                                                          BorderColor="Black"></asp:ImageButton>
                                                      <asp:ImageButton ID="btn_acepta" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Aceptar_Out.gif"
                                                          BorderColor="Black"></asp:ImageButton>
                                                      <asp:ImageButton ID="canc" runat="server" ImageUrl="~/Imagenes/Botones/Boton_Cancelar_Out.gif"
                                                          Visible="False"></asp:ImageButton>
                                                  </td>
                                              </tr>
                                          </tbody>
                                      </table>
                                  </asp:Panel>
       <%--*********************************************************************************************--%>
  
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
