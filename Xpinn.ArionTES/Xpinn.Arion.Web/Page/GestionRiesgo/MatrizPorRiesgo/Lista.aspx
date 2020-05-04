<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvFactores" runat="server" ActiveViewIndex="0">
        <asp:View ID="vListado" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align:left;width: 300px">Factor de Riesgo<br />
                            <asp:DropDownList ID="ddlFactorRiesgo" runat="server" CssClass="textbox" Width="200px"
                                AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Panel ID="panelGrilla" runat="server">
                            <br />
                            <asp:GridView runat="server" ID="gvFactorRiesgo" HorizontalAlign="Center" Width="99%" AutoGenerateColumns="false"
                                Style="font-size: x-small" AllowPaging="true" PageSize="20" OnRowDataBound="gvFactor_RowDataBound"
                                OnPageIndexChanging="gvFactorRiesgo_PageIndexChanging"
                                OnRowCommand="gvFactorRiesgo_RowCommand" DataKeyNames="cod_factor">
                                <Columns>
                                    <asp:BoundField DataField="abreviatura" HeaderText="Código del Factor">
                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Inherente">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPuntajeI" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("nom_subproceso") %>' Width="130px" CssClass="textbox" Visible="false" />
                                            <asp:TextBox ID="puntajeI" runat="server" CssClass="textbox" Enabled="true"/>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Valoración">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPuntajeI" runat="server" Text='<%# Bind("nom_subproceso") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Residual">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPuntajeR" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("nom_factor") %>' Width="130px" CssClass="textbox" Visible="false" />
                                            <asp:TextBox ID="puntajeR" runat="server" CssClass="textbox" Enabled="true"/>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Valoración">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPuntajeR" runat="server" Text='<%# Bind("nom_factor") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="sigla" HeaderText="Valor de Control">
                                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                                    </asp:BoundField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSRiesgo" runat="server" Text='<%# Bind("cod_riesgo") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>

                        </asp:Panel>
                        <br />
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

