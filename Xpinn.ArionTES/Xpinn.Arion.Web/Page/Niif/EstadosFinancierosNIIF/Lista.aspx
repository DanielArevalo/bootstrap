<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimalesGrid.ascx" TagName="decimalesGrid" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvEstadosFinancieros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEstadosFinancieros" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Style="margin-bottom: 0px">                
                <table style="width: 50%;">
                    <tr>
                        <td colspan="4">                            
                            <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; height: 25px;" colspan="4">                            
                            <asp:Label ID="LblTitulo" runat="server" Text="Paramétros de Búsqueda" 
                                style="font-weight: 700; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <br />
                            <br />
                        </td>
                        <td style="text-align:left">
                            Tipo EstadoFinanciero<br />
                            <asp:DropDownList ID="ddlTipoEstadoFinanciero" runat="server" Width="321px" AppendDataBoundItems="True">
                                 <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align:left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align:left; height: 25px;" colspan="3">                            
                        <asp:Label ID="lblTituloLista" runat="server" Text="Listado de Tipos de Estados Financieros" 
                            style="font-weight: 700; font-size: small;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; font-weight: 700;" colspan="4">
                        <strong>
                            <asp:GridView ID="gvEstadosFinancieros" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" DataKeyNames="codigo" GridLines="Horizontal" HorizontalAlign="Center"
                                   PageSize="100" ShowHeaderWhenEmpty="True" Style="font-size: x-small"
                                 Width="100%" OnSelectedIndexChanged="gvEstadosFinancieros_SelectedIndexChanged">
                                <Columns>
                                   
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" 
                                                ToolTip="Editar" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                  
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <br />
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" colspan="4">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" colspan="3">
                        &nbsp;
                    </td>
                    <td style="height: 25px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
