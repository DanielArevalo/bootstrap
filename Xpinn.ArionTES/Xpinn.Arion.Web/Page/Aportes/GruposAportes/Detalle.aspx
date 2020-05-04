<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 92%">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <span style="color: #0099FF;"><strong style="text-align: center; background-color: #FFFFFF;">
                        <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700;"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Numero Grupo<br />
                    <asp:TextBox ID="txtIdGrupo" runat="server" CssClass="textbox" Width="212px"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 253px;">
                    &nbsp;
                </td>
                <td style="text-align: left; width: 124px;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="4">
                    Tipo de distribución<br />
                    <asp:DropDownList ID="ddlTipoDistribucion" runat="server" CssClass="textbox" Width="225px"
                        AutoPostBack="true" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelCuoExt" runat="server">
                    <ContentTemplate>
                        <strong>
                            <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                        </strong>
                        <br />
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            DataKeyNames="COD_LINEA_aporte" ForeColor="Black" Height="61px" OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowCommand="gvLista_RowCommand" OnRowEditing="gvLista_RowEditing" PageSize="6"
                            ShowFooter="True" Style="font-size: x-small" Width="99%">
                            <Columns>
                                <asp:TemplateField HeaderText="Cod Linea">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodlinea" runat="server" Text='<%# Bind("COD_LINEA_aporte") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Linea">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLinea" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="400px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="porcentaje" HeaderText="porcentaje" />
                                <asp:TemplateField HeaderText="principal">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chkprincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                            Enabled="False" EnableViewState="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
 
</asp:Content>
