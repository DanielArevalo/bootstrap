<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Proceso Contable :." %>

<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="50%">
            <tr>
                <td colspan="2" style="text-align: left">Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="113px" />
                </td>
                <td class="tdD">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">Tipo de Operación<br />
                    <asp:DropDownList ID="ddlTipoOpe" runat="server" Height="25px" Width="350px" CssClass="dropdown"
                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoOpe_OnIndexChanged">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="width: 203px; text-align: left">Tipo de Comprobante<br />
                    <asp:DropDownList ID="ddlTipoComp" runat="server" Height="25px" Width="200px" CssClass="dropdown"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Fecha Inicial<br />
                    <asp:fecha ID="txtFechaInicial" runat="server" style="width: 50px" />
                </td>
                <td style="text-align: left;">Fecha Final<br />
                    <asp:fecha ID="txtFechaFinal" runat="server" style="width: 50px" />
                </td>
                <td style="text-align: left; width:200px;">Concepto<br />
                    <ctl:ctlListarCodigo ID="ctlListarCodigo" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">Cod.Cuenta<br />
                    <asp:DropDownList ID="ddlCodCuenta" runat="server" Height="26px" Width="100px" CssClass="dropdown"
                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCodCuenta_SelectedIndexChanged"
                        Visible="false">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlNomCuenta" runat="server" Height="26px" Width="250px" CssClass="dropdown"
                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlNomCuenta_SelectedIndexChanged"
                        Visible="false">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblCuentaContable" runat="server" Text=""></asp:Label><br />
                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                        Style="text-align: left" BackColor="#F4F5FF" Width="80px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListadoPlan_Click" />
                    <br />
                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="95%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                </td>
                <td style="text-align: left">Tipo Mov.<br />
                    <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="200px" CssClass="dropdown"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="1">Débito</asp:ListItem>
                        <asp:ListItem Value="2">Crédito</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">Estructura de Detalle<br />
                    <asp:DropDownList ID="ddlEstructura" runat="server" Width="350px" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align: left">&nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvTipoGiros" runat="server" Width="50%" AutoGenerateColumns="False"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="iddetgiro,idprocesogiro"
                    Style="font-size: small">
                    <Columns>
                        <asp:BoundField DataField="Descripcion" HeaderText="Tipo de Giro">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Cod.Cuenta">
                            <ItemTemplate>
                                <cc1:TextBoxGrid ID="txtGVCodCuenta" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: left"
                                    BackColor="#F4F5FF" Width="80px"
                                    OnTextChanged="txtGVCodCuenta_TextChanged" Text='<%#Bind("cod_cuenta")%>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                <cc1:ButtonGrid ID="btnGVListadoPlan" CssClass="btnListado" runat="server"
                                    Text="..." OnClick="btnGVListadoPlan_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                <uc1:ListadoPlanCtas ID="ctGVlListadoPlan" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Cuenta">
                            <ItemTemplate>
                                <cc1:TextBoxGrid ID="txtGVNomCuenta" runat="server" Style="font-size: xx-small; text-align: left"
                                    BackColor="#F4F5FF" Width="180px" Text='<%#Bind("descripcion_cod_cuenta")%>' CommandArgument='<%#Container.DataItemIndex %>'
                                    Enabled="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
