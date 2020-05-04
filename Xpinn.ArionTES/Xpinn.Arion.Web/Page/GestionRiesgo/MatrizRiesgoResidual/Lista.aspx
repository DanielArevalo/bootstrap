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
                <asp:UpdatePanel ID="panelGrilla" runat="server" Width="120%" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <br />
                        <asp:GridView runat="server" ID="gvMatriz" HorizontalAlign="Center" Width="120%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                            Style="font-size: x-small" OnRowDataBound="gvMatriz_RowDataBound" OnRowCreated="gvMatriz_RowCreated"
                            OnPageIndexChanging="gvMatriz_PageIndexChanging" DataKeyNames="cod_matriz">
                            <Columns>
                                <asp:BoundField DataField="cod_factor" HeaderText="Cod. Factor" />
                                <asp:BoundField DataField="desc_factor" HeaderText="Desc. Factor" ItemStyle-Width="200px" />
                                <asp:BoundField DataField="cod_causa" HeaderText="Cod. Causa" />
                                <asp:BoundField DataField="desc_causa" HeaderText="Desc. Causa" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Riesgo Inherente">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRiesgoI" runat="server" CssClass="textbox" Enabled="false"/>
                                        <asp:Label ID="lbRiesgoI" runat="server" CssClass="textbox" Text='<%# Bind("valor_rinherente") %>' Visible="false"/>
                                        <asp:Label ID="lbRiesgoIC" runat="server" CssClass="textbox" Text='<%# Bind("valor_rinherente") %>' Visible="false"/>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Control" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlControl" AutoPostBack="true" CssClass="dropdown" runat="server" Height="26px" DataSource="<%# ListaControl() %>"
                                            DataTextField="cod_control" DataValueField="cod_control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlControl_SelectedIndexChanged"
                                            SelectedValue='<%# Bind("cod_control") %>' CommandArgument='<%#Container.DataItemIndex %>'>
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="desc_control" HeaderText="Desc. Control" />
                                <asp:BoundField DataField="desc_clase" HeaderText="Clase" />                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderStyle-Width="80px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbClase" runat="server" CssClass="textbox" Text='<%# Bind("clase") %>' Visible="false"/>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Forma">
                                    <ItemTemplate>
                                        <table><tr>
                                            <td><cc1:DropDownListGrid ID="ddlForma" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged"
                                                    AppendDataBoundItems="True" DataSource="<%# ListaForma(1) %>" DataTextField="opcion" DataValueField="cod_opcion" SelectedValue='<%# Bind("forma") %>' CommandArgument='<%#Container.DataItemIndex + ";1" %>' >
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </cc1:DropDownListGrid></td>
                                            <td><asp:Label ID="lbForma" runat="server" CssClass="textbox" Visible="true"/></td>
                                        </tr></table>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Ejecuciòn">
                                    <ItemTemplate>
                                        <table><tr>
                                            <td><cc1:DropDownListGrid ID="ddlEjecucion" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged"
                                                    AppendDataBoundItems="True" DataSource="<%# ListaForma(2) %>" DataTextField="opcion" DataValueField="cod_opcion" SelectedValue='<%# Bind("ejecucion") %>' CommandArgument='<%#Container.DataItemIndex + ";2" %>' >
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </cc1:DropDownListGrid></td>
                                            <td><asp:Label ID="lbEjecucion" runat="server" CssClass="textbox" Visible="true"/></td>
                                        </tr></table>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Documentación">
                                    <ItemTemplate>
                                        <table><tr>
                                            <td><cc1:DropDownListGrid ID="ddlDocumentacion" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged"
                                                    AppendDataBoundItems="True" DataSource="<%# ListaForma(3) %>" DataTextField="opcion" DataValueField="cod_opcion" SelectedValue='<%# Bind("documentacion") %>' CommandArgument='<%#Container.DataItemIndex + ";3" %>' >
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </cc1:DropDownListGrid></td>
                                            <td><asp:Label ID="lbDocumentacion" runat="server" CssClass="textbox" Visible="true"/></td>
                                        </tr></table>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Complejidad">
                                     <ItemTemplate>
                                        <table><tr>
                                            <td><cc1:DropDownListGrid ID="ddlComplejidad" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged"
                                                    AppendDataBoundItems="True" DataSource="<%# ListaForma(4) %>" DataTextField="opcion" DataValueField="cod_opcion" SelectedValue='<%# Bind("complejidad") %>' CommandArgument='<%#Container.DataItemIndex + ";4" %>' >
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </cc1:DropDownListGrid></td>
                                            <td><asp:Label ID="lbComplejidad" runat="server" CssClass="textbox" Visible="true"/></td>
                                        </tr></table>
                                     </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Histórico de Fallas">
                                    <ItemTemplate>
                                        <table><tr>
                                            <td><cc1:DropDownListGrid ID="ddlFallas" AutoPostBack="true" CssClass="dropdown" Height="26px" runat="server" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged"
                                                    AppendDataBoundItems="True" DataSource="<%# ListaForma(5) %>" DataTextField="opcion" DataValueField="cod_opcion" SelectedValue='<%# Bind("fallas") %>' CommandArgument='<%#Container.DataItemIndex + ";5" %>' >
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </cc1:DropDownListGrid></td>
                                            <td><asp:Label ID="lbFallas" runat="server" CssClass="textbox" Visible="true"/></td>
                                        </tr></table>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Nivel de Reducciòn">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNivelReduccion" runat="server" Text='<%# Bind("nivel_reduccion") %>' visible="true" />
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
                                        <table>
                                            <tr>
                                                <td><asp:Label ID="lbPuntajeTotal" runat="server" visible="false"/></td>
                                                <td><asp:TextBox ID="txtRiesgoR" runat="server" CssClass="textbox" Enabled="false" /></td>
                                                <td><asp:Label ID="lbRiesgoR" runat="server" Text='<%# Bind("valor_rresidual") %>' visible="true" /></td>
                                            </tr>                                        
                                        </table>
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
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
            </td>
        </tr>
    </table>
    <uc2:ctlmensaje ID="ctlmensaje" runat="server" />
</asp:Content>

