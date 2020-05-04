<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" EnableEventValidation="false" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="align-items: center">
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Visible="False" ForeColor="Blue" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>               
                <td colspan="2" style="text-align: center">Sistema de riesgo
                    <asp:DropDownList ID="ddlSistemaRiesgo" CssClass="dropdown" runat="server"></asp:DropDownList><br/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server">
        <tr>
            <td colspan="2" style="text-align: center">

                <asp:UpdatePanel ID="panelGrilla" runat="server" Width="100%" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <br />
                        <asp:GridView runat="server" ID="gvMatriz" HorizontalAlign="Center" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                            Style="font-size: x-small" OnRowDataBound="gvMatriz_RowDataBound"
                            OnPageIndexChanging="gvMatriz_PageIndexChanging" DataKeyNames="cod_matriz">
                            <Columns>
                                <asp:BoundField DataField="cod_factor" HeaderText="Cod. Factor" />
                                <asp:BoundField DataField="desc_factor" HeaderText="Desc. Factor" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Inherente">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRiesgoI" runat="server" CssClass="textbox" Enabled="false"/>
                                        <asp:Label ID="lbRiesgoI" runat="server" CssClass="textbox" Text='<%# Bind("valor_rinherente") %>' Visible="false"/>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Valoración Control">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValorC" runat="server" CssClass="textbox" Enabled="false"/>
                                        <asp:Label ID="lbValorC" runat="server" Text='<%# Bind("valor_control") %>' visible="false"/>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Residual">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRiesgoR" runat="server" CssClass="textbox" Enabled="false" />
                                        <asp:Label ID="lbRiesgoR" runat="server" Text='<%# Bind("valor_rresidual") %>' visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Tipo Monitoreo" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlTipoM" AutoPostBack="true" CssClass="dropdown" runat="server" Height="26px" OnSelectedIndexChanged="ddlTipoM_SelectedIndexChanged"
                                            DataSource="<%# ListaTipoMonitoreo() %>" DataTextField="cod_monitoreo" DataValueField="cod_monitoreo" AppendDataBoundItems="True"
                                            SelectedValue='<%# Bind("cod_monitoreo") %>' CommandArgument='<%#Container.DataItemIndex %>'>
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Estado Alerta">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEstadoA" runat="server" CssClass="textbox" Enabled="false" />
                                        <asp:Label ID="lbEstadoA" runat="server" Text='<%# Bind("cod_alerta") %>' visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="descripcion" HeaderText="Periodicidad" />
                                <asp:BoundField DataField="desc_area" HeaderText="Área Ejecución " />
                                <asp:BoundField DataField="desc_cargo" HeaderText="Responsable Ejecución" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
            </td>
        </tr>
    </table>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

