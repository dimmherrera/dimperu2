<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Cargando.ascx.vb" Inherits="WebControles_Cargando" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div id="progressBackgroundFilter" style="position: absolute; top: 0px; bottom: 0px;
    left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000;
    filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
</div>    
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="processMessage" Radius="10">
    </cc1:RoundedCornersExtender>

    <asp:Panel ID="processMessage" runat="server" Style="z-index: 1000; position: absolute;
        top: 40%; left: 35%; height: 150px; width: 300px; background-color: WhiteSmoke">
        <table>
            <tr>
                <td align="center">
                    <img src="../../../Imagenes/Iconos/DIMENSION_LOGO_LENTO.gif" width="85" />
                    <img src="../../../Imagenes/dimension.png" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img src="../../../Imagenes/Iconos/procesando.gif" />
                </td>
            </tr>
        </table>
    </asp:Panel>


