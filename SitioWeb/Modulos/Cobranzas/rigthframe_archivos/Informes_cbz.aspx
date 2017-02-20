<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Informes_cbz.aspx.vb" Inherits="Modulos_Cobranzas_rigthframe_archivos_Informes_cbz" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target=_self></base>
    <title></title>
 
</head>
<body>
    <form id="form1" runat="server">
        
    <div>

        <table >
            <tr>
                <td>
    
        
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px" Height="700">
        </rsweb:ReportViewer>

        
    
                </td>
            </tr>
            <tr>
                <td align="right">
                                    <asp:ImageButton ID="btn_volver" runat="server" 
                                    ImageUrl="~/Imagenes/Botones/Boton_Volver_Out.gif" 
                                    onmouseout="this.src='../../../Imagenes/Botones/Boton_Volver_Out.gif';" 
                                    onmouseover="this.src='../../../Imagenes/Botones/Boton_Volver_in.gif';" 
                                    TabIndex="31" ToolTip="Volver" />
                            
                </td>
            </tr>
        </table>
    
    </div>
                            
    </form>
</body>
</html>
