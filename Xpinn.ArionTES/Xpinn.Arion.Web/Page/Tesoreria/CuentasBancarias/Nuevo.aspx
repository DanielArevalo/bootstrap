<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%" cellspacing="2" cellpadding="2">
                <tr>
                    <td style="text-align: left; width: 30%">
                        Código<br />
                        &nbsp;<asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 30%">
                        Banco<br />
                        &nbsp;<asp:DropDownList ID="ddlBanco" runat="server" CssClass="textbox" Width="80%"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 40%">
                        Tipo de Cuenta<br />
                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                            Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Cuenta Contable<br />
                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                            Style="text-align: left" BackColor="#F4F5FF" Width="70%" 
                            ontextchanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                        <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                            OnClick="btnListadoPlan_Click" /><br />
                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                    </td>
                    <td style="text-align: left" colspan="2">
                        Nombre de la Cuenta
                        <br />
                        <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                            Width="70%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Número de Cuenta<br />
                        <asp:TextBox ID="txtnumCuenta" runat="server" CssClass ="textbox" Width="90%"/>
                    </td>
                    <td style="text-align: left">
                      Estado<br />
                        <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Activa</asp:ListItem>
                            <asp:ListItem Value="2">Inactiva</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left">
                        Oficina<br />
                         <asp:DropDownList ID="ddlOficina" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                            Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
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
                        <td style="text-align: center; font-size: large;">                            
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" OnClick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
