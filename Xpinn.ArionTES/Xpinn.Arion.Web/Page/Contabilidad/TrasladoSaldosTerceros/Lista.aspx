<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" style="width: 60%">
            <tr>
                <td style="text-align: center">Código de Cuenta
                </td>
                <td style="text-align: Center">Fecha
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                        Style="text-align: left" BackColor="#F4F5FF" Width="100px" OnTextChanged="txtCodCuenta_TextChanged"/>
                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="180px" Enabled="False" CssClass="textbox" />
                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListadoPlan_Click" />
                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                </td>
                <td style="text-align: center;">
                    <asp:DropDownList Height="30px" ID="ddlFechaCorte" runat="server" CssClass="dropdown" Width="158px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="pListado" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table style="text-align: center; width:100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" AllowPaging="False" HorizontalAlign="Center"
                        OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20"  HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        OnRowEditing="gvLista_RowEditing"
                        RowStyle-CssClass="gridItem" Style="font-size: xx-small" Width="95%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Gestionar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Trasladar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta"/>
                            <asp:BoundField DataField="nombre" HeaderText="Nom. Cuenta"/>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:c}" HeaderText="Saldo"/>
                            <asp:BoundField DataField="tipo" HeaderText="Naturaleza"/>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False"
                        Style="font-size: xx-small" />                    
                </td>
            </tr>
        </table>
        <span style="font-size: xx-small">
            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
        &nbsp; </span>
        </ContentTemplate>        
    </asp:UpdatePanel>
</asp:Content>

