<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 50%">
                        Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="140px" />
                         <asp:FilteredTextBoxExtender ID="fteCodigo" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtCodigo" ValidChars="-" />
                    </td>
                    <td style="text-align: left; width: 48%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="88%" />
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Tipo<br />
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Depende de<br />
                        <asp:DropDownList ID="ddlDepende" runat="server" CssClass="textbox" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente los datos ingresados</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>