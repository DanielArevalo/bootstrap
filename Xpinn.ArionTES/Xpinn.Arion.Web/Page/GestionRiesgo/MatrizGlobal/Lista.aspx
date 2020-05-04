<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="text-align:center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server">
            <tr>
                <td colspan="3" style="text-align: center">
                    <asp:UpdatePanel ID="panelGrilla" runat="server" Width="100%" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <br />

                            <asp:GridView runat="server" ID="gvMatriz" HorizontalAlign="Center" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                                Style="font-size: small; align-self: center" OnRowDataBound="gvMatriz_RowDataBound" DataKeyNames="cod_matriz">
                                <Columns>
                                    <asp:BoundField DataField="descripcion" HeaderText="Nombre Sistema" />
                                    <asp:BoundField DataField="nivel" HeaderText="SIGLA" />
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Inherente">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRiesgoI" runat="server" CssClass="textbox" Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Valoración Control">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValorC" runat="server" CssClass="textbox" Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Residual">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRiesgoR" runat="server" CssClass="textbox" Enabled="false"/>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
        </table>
        <table style="margin-top: 20px; ">
            <tr>
                <asp:Label ForeColor="#6699ff" Font-Size="Medium" ID="lbl" runat="server" CssClass="ajax__tab_header" >PROMEDIO  GENERAL  SISTEMA SIAR </asp:Label>
            </tr>
            <tr>
                <td>Promedio Global Riesgo Inherente
                <asp:TextBox ID="txtRiesgoIG" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td>Promedio Global Valoración de Control
                <asp:TextBox ID="txtValorCG" runat="server" CssClass="textbox" Enabled="False"/>
                </td>
                <td>Promedio Global Riesgo Residual
                <asp:TextBox ID="txtRiesgoRG" runat="server" CssClass="textbox" Enabled="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

