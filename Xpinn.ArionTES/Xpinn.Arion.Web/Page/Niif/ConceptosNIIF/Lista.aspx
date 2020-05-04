<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableEventValidation="false" %>

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
            <caption>
                <br />
                <tr>
                    <td colspan="2" style="text-align: center">Tipo Estado Financiero<asp:DropDownList ID="ddlTipoEstadoFinanciero"  Enabled="false" runat="server" CssClass="dropdown">
                        </asp:DropDownList>
                    </td>
                </tr>
            </caption>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" style="align-items: center">
        <tr>
            <td colspan="3" style="text-align: center">

                <asp:UpdatePanel ID="panelGrilla" runat="server" Width="100%" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <strong>
                        <asp:GridView ID="gvConceptos" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="cod_concepto" GridLines="Horizontal" HorizontalAlign="Center" OnSelectedIndexChanged="gvConceptos_SelectedIndexChanged" PageSize="100" ShowHeaderWhenEmpty="True" Style="font-size: x-small" Width="100%" OnPageIndexChanged="gvConceptos_PageIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_concepto" HeaderText="Código" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_concepto" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="depende_de" HeaderText="Depende de" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                 
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        </strong>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="true" Text="" Style="text-align: center" />
            </td>
        </tr>
    </table>
    </asp:Content>

