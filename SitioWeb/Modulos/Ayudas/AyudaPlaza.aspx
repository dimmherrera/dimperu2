<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AyudaPlaza.aspx.vb" Inherits="Modulos_Ayudas_AyudaPlaza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ayuda Plazas</title>
    <base target="_self"></base>
    <link href="../../CSS/Estilos.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript">
        function Plaza(nro, des) {

            window.dialogArguments.document.forms[0].HF_IdPlaza.value = nro;
            window.dialogArguments.document.forms[0].Txt_PlazaDes.value = des;
            
            window.close();
           
        }
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table class="cabeceraGrilla">
                    <tr>
                        <td width="80">
                            <asp:Label ID="Label1" runat="server" CssClass="LabelCabeceraGrilla" Text="Selección"></asp:Label>
                        </td>
                        <td width="120">
                            <asp:Label ID="Label11" runat="server" CssClass="LabelCabeceraGrilla" Text="Codigo"></asp:Label>
                        </td>
                        <td align="left" width="300">
                            <asp:Label ID="Label12" runat="server" CssClass="LabelCabeceraGrilla" Text="Descripción"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" CssClass="Contenido" Height="250px" 
                    ScrollBars="Vertical">
                    <asp:GridView ID="gr_Plaza" runat="server" AutoGenerateColumns="False" 
                        CssClass="formatUltcell" ShowHeader="False" Width="96%">
                        <Columns>
                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Img_Ver" runat="server" ImageUrl="~/Images/bt_ver.gif" ToolTip='<%# Eval("Codigo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Codigo">
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Descripcion">
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle  CssClass="cabeceraGrilla" />
                        <RowStyle CssClass="formatUltcell" />
                        <AlternatingRowStyle CssClass="formatUltcellAlt" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
