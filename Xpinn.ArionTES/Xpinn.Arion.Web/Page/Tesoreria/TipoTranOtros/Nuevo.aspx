<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

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
                        Codigo:<br />
                        <asp:TextBox ID="txtCodigo" CssClass="textbox" runat="server"></asp:TextBox>
                    </td>

                    <td style="text-align: left; width: 48%">
                        &nbsp; Tipo Movimiento
                        <br />
                        <asp:DropDownList ID="ddlTipoMovimientos" runat="server" AutoPostBack="true" 
                            CssClass="textbox" 
                          Width="160px">
                            <asp:ListItem Value="2">INGRESO</asp:ListItem>
                            <asp:ListItem Value="1">EGRESO</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 40%">
                        Descripcion:<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Width="300px" />
                        <asp:Label ID="lblMensaje" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        <br />
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 2%">
                        &nbsp;
                    </td>
                    <td style="text-align: left; width: 10%">
                        &nbsp;
                    </td>
                </tr>
            </table>
              <td style="text-align: left; width: 50%">
                       <br />
                        <asp:TextBox ID="TextBox1" Visible="false" CssClass="textbox" runat="server"></asp:TextBox>
                    </td>
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
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente los datos ingresados
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
