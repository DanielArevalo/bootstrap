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
                        <td style="text-align: center; width: 300px;">Código del factor de riesgo<br />
                            <asp:TextBox ID="txtCodigoFactorRiesgo" runat="server" CssClass="textbox" MaxLength="20" Width="150px" />
                        </td>
                        <td style="text-align: center; width: 400px">Descripción del factor de riesgo<br />
                            <asp:TextBox ID="txtDescripcionFactor" runat="server" CssClass="textbox" MaxLength="120" Width="300px" />
                        </td>
                        <td style="text-align: center; width: 300px">Sistema de riesgo<br />
                            <asp:DropDownList ID="ddlSistemaRiesgo" runat="server" CssClass="textbox" Width="200px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 300px">Procedimiento<br />
                            <asp:DropDownList ID="ddlProcedimiento" runat="server" CssClass="textbox" Width="200px"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center; width: 300px">Factor de riesgo<br />
                            <asp:DropDownList ID="ddlFactorRiesgo" runat="server" CssClass="textbox" Width="200px"
                                AutoPostBack="True">
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
                                Style="font-size: x-small" AllowPaging="true" PageSize="20" OnRowEditing="gvFactorRiesgo_RowEditing"
                                OnPageIndexChanging="gvFactorRiesgo_PageIndexChanging" OnRowDeleting="gvFactorRiesgo_RowDeleting"
                                OnRowCommand="gvFactorRiesgo_RowCommand" DataKeyNames="cod_factor">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                                ToolTip="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                ToolTip="Borrar" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="abreviatura" HeaderText="Código del Factor">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_subproceso" HeaderText="Procedimiento">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_factor" HeaderText="Factor de Riesgo">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sigla" HeaderText="Sistema Riesgo">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
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
        <asp:View ID="vReporte" runat="server">
            <table>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerFactor" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            AsyncRendering="false" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\GestionRiesgo\HojaVidaRiesgo\ReporteRiesgo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

