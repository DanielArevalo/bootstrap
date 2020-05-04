<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="pGeneral" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="align-items: center">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblMensaje" runat="server" Visible="False" ForeColor="Blue" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <br />
                        <td colspan="2" style="text-align: center">Sistema de riesgo
                            <asp:DropDownList ID="ddlSistemaRiesgo" CssClass="dropdown" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>        
            <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" style="align-items: center">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <br />
                        <asp:GridView runat="server" ID="gvMatriz" HorizontalAlign="Center" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                            Style="font-size: x-small" OnRowDataBound="gvMatriz_RowDataBound" 
                            OnPageIndexChanging="gvMatriz_PageIndexChanging" DataKeyNames="cod_matriz" OnRowCommand="gvlista_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/gr_imp.gif" ToolTip="Imprimir" CommandName="Imprimir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Factor Riesgo" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlFactor" AutoPostBack="true" CssClass="dropdown" runat="server" Height="26px" OnSelectedIndexChanged="ddlFactor_SelectedIndexChanged"
                                            DataSource="<%# ListaFactor() %>" DataTextField="abreviatura" DataValueField="cod_factor" AppendDataBoundItems="True"
                                            SelectedValue='<%# Bind("cod_factor") %>' CommandArgument='<%#Container.DataItemIndex %>' Enabled="False">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="desc_factor" HeaderText="Desc. Factor Riesgo" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Causa Riesgo" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlCausa" AutoPostBack="true" CssClass="dropdown" runat="server" Height="26px" OnSelectedIndexChanged="ddlCausa_SelectedIndexChanged"
                                            DataSource="<%# ListaCausa() %>" DataTextField="cod_causa" DataValueField="cod_causa" AppendDataBoundItems="True"
                                            SelectedValue='<%# Bind("cod_causa") %>' CommandArgument='<%#Container.DataItemIndex %>' Enabled="False">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción Causa" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Probabilidad">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlProbabilidad" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlProbabilidad_SelectedIndexChanged"
                                            DataSource="<%# ListaProbabilidad() %>" DataTextField="nivel" DataValueField="cod_probabilidad" AppendDataBoundItems="True"
                                            SelectedValue='<%# Bind("cod_probabilidad") %>' CommandArgument='<%#Container.DataItemIndex %>'  Enabled="False">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Impacto">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlImpacto" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlImpacto_SelectedIndexChanged"
                                            DataSource="<%# ListaImpacto() %>" DataTextField="nivel" DataValueField="cod_impacto" AppendDataBoundItems="True"
                                            SelectedValue='<%# Bind("cod_impacto") %>' CommandArgument='<%#Container.DataItemIndex %>'  Enabled="False">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Inherente">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRiesgoI" runat="server" CssClass="textbox" Enabled="false"/>
                                        <asp:HiddenField ID="hdValorRiesgo" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pReporte" runat="server">
            <table>
                <tr>
                    <td>
                        <br /><br />
                        <rsweb:ReportViewer ID="ReportViewerFactor" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            AsyncRendering="false" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\GestionRiesgo\ReporteControl\ReporteControlRiesgo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:Panel>

    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

