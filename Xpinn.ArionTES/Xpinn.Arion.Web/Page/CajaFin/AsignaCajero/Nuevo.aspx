<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td class="tdI">Oficina &#160;&#160;
            <asp:DropDownList ID="ddlOficinas" CssClass="dropdown" runat="server"
                Height="27px" Width="182px" AutoPostBack="True" Enabled="false">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI">Caja &#160;&#160;&#160;&#160;&#160;&#160;
            <asp:DropDownList ID="ddlCajas" CssClass="dropdown" runat="server"
                Height="27px" Width="182px" AutoPostBack="True"
                OnSelectedIndexChanged="ddlCajas_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCajeros" runat="server" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                            ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False"
                            OnRowDataBound="gvCajeros_OnRowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_persona" HeaderText="Cód. Cajero" />
                                <asp:BoundField DataField="nom_cajero" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Asignar" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <contenttemplate>
                                        <cc1:checkboxgrid id="chkPermiso" runat="server" autopostback="true"
                                            commandargument='<%# DataBinder.Eval(Container, "RowIndex") %>' oncheckedchanged="chkSeleccionar_CheckedChanged" />
                                    </contenttemplate>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Caja a trasladar">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCajaPrincipal" runat="server" CssClass="textbox" AppendDataBoundItems="true"
                                            DataTextField="nombre" DataValueField="cod_caja"
                                            Width="130px">
                                            <asp:ListItem Value="0" Text="Seleccione un Item" />
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="lblEsPrincipal" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCajas" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
