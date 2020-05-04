<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="garantiascomunitarias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="gvDiv">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha Reclamación"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha" runat="server" />
                </td>
                <td style="text-align: left" colspan="2">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <br />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <div style="overflow: scroll; height: 500px; width: 100%;">
                        <div style="width: 100%;">
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                              <asp:GridView ID="gvMovGeneral" runat="server" Width="72%" PageSize="3" GridLines="Horizontal"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnRowEditing="gvMovGeneral_RowEditing"
                                SelectedRowStyle-Font-Size="XX-Small"  
                                        Style="font-size: small;  margin-bottom: 0px;">
                                <Columns>
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditar0" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                                        ToolTip="Modificar" /></ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                                <ItemStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                    <asp:BoundField DataField="COD_IDENT" HeaderText="Numero de Reclamación" />
                                    <asp:BoundField DataField="NOMBRES" HeaderText="Usuario Generador" >
                                        <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="FECHARECLAMACION" HeaderText="Fecha Reclamación" />
                                 </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
