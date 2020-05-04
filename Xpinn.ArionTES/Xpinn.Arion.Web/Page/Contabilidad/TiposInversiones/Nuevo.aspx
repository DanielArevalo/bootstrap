<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">Código de Inversión&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Descripción&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido"
                    SetFocusOnError="True" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;color: #FF3300">
                        Se&nbsp;<asp:Label ID="lblMsj" runat="server" ></asp:Label>&nbsp; correctamente los datos
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>

