<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlArchivoGiros.ascx.cs" Inherits="ctlArchivoGiros" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:DragPanelExtender ID="dpeArchivoGiros" runat="server" TargetControlID="panelArchivoGiros" DragHandleID="panelTitulo" />
<asp:Panel ID="panelArchivoGiros" runat="server" BackColor="White" Style="text-align: right; position: absolute; top: 200px; left: 200px" 
    BorderWidth="1px" Width="500px" Visible="False">
    <asp:Panel ID="panelTitulo" runat="server" Width="100%" Height="20px" CssClass="sidebarheader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center;" width="95%">
                    Generación Archivo Traslados Bancarios
                </td>
                <td style="text-align:center; padding-top: 0px; z-index: 999" width="5%">
                    <asp:ImageButton ID="bntCerrar" runat="server" ImageUrl="../../Images/btnCerrar.jpg" onclick="bntCerrar_Click" />                   
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Panel ID="PanelListadoPersonas" runat="server" BackColor="White" Width="100%" >
        <asp:HiddenField ID="hfCodigo" runat="server" Visible="False" />        
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
            <tr style="background-color:#DEDFDE">                
                <td style="font-size: x-small; text-align: left; width: 50px">
                    Entidad
                </td>
                <td style="font-size: x-small; text-align: left; width: 180px">
                    <asp:TextBox ID="txtEntidad" runat="server" Width="300px" Font-Size="XX-Small" CssClass="textbox"></asp:TextBox>    
                </td>                                
            </tr>
            <tr style="background-color:#DEDFDE">
                <td style="font-size: x-small; text-align: left;">
                    Cuenta Bancaria
                </td>
                <td style="font-size: x-small; text-align: left;">
                    <asp:TextBox ID="txtCuenta" runat="server" Width="300px" Font-Size="XX-Small" CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td style="font-size: x-small; text-align: left;">
                    Estructura
                </td>
                <td style="font-size: x-small; text-align: left;">
                    <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="textbox" Width="310px" />
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td style="font-size: x-small; text-align: left;">
                    Archivo
                </td>
                <td style="font-size: x-small; text-align: left;">
                    <asp:TextBox ID="txtArchivo" runat="server" CssClass="textbox" Width="200px" Enabled="false" />
                </td>
            </tr>    
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnAceptar" runat="server" Height="30px" Text="Aceptar"
                        Width="100px" CssClass="btn8" OnClick="btnAceptar_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn8" Height="30px"
                        Text="Cancelar" Width="100px" OnClick="btnCancelar_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:DropShadowExtender ID="dse" runat="server" TargetControlID="panelArchivoGiros" Opacity=".2" TrackPosition="true" Radius="2" />