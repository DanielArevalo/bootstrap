<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ActiveViewIndex="0" ID="mvParametro" runat="server">
        <asp:View runat="server">
            <br />
            <br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table style="width: 50%; text-align: left; margin-left: 20px;">
                        <tr>
                            <td colspan="2">
                                <asp:Label runat="server" ID="lblTipoCobro"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlTipoCobro" AppendDataBoundItems="true" Width="100%" OnSelectedIndexChanged="ddlTipoCobro_SelectedIndexChanged" CssClass="textbox">
                                    <asp:ListItem Text="Seleccione un Item" Value="" />
                                    <asp:ListItem Text="Por días de mora" Value="0" />
                                    <asp:ListItem Text="Por cuotas" Value="1" />
                                    <asp:ListItem Text="Por categorias" Value="2" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlFormaCobro" AppendDataBoundItems="true" Width="100%" OnSelectedIndexChanged="ddlFormaCobro_SelectedIndexChanged" CssClass="textbox">                                    
                                    <asp:ListItem Text="Porcentaje" Value="0" />
                                    <asp:ListItem Text="Valor" Value="1" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left"  colspan="2">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" Text="Agregar"
                                    OnClick="btnAgregar_Click" Width="140px" Height="22px" />
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" Width="50%"
                                    DataKeyNames="idparametro"
                                    Style="font-size: x-small" onrowdatabound="gvLista_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="idparametro" HeaderText="Id." ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Mínimo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtMinimo" Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Maximo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtMaximo" Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="% Cobro" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPorcentaje" onkeypress="return isDecimalNumber(event)" Width="140px" Text='<%# Bind("porcentaje") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Valor Cobro" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtValor" onkeypress="return isDecimalNumber(event)" Width="140px" Text='<%# Bind("valor") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </td>
                        </tr>                    
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Modificación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
