<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
        <asp:Panel ID="pBusqueda" runat="server" Height="120px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                        <strong>Criterios de Búsqueda:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Id<br />
                        <asp:TextBox ID="txtId" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">Cód.Persona<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">Identificación<br />
                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">Tipo de Identificación<br />
                        <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="120px" CssClass="textbox"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Código de nómina<br />
                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Nombres<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="true" Width="160px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">Usuario<br />
                        <asp:DropDownList ID="ddlUsuario" runat="server" Width="120px" CssClass="textbox"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Estado<br />
                        <asp:DropDownList ID="ddlEstado" runat="server" Width="120px" CssClass="textbox"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                        OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                        OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idautorizacion" Style="font-size: x-small">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                        ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="idautorizacion" HeaderText="id">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_persona" HeaderText="Cód.Persona">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="primer_nombre" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_producto" HeaderText="Num.Producto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ip" HeaderText="Ip">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_usuario" HeaderText="CodUsuario">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estados" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                        Visible="False" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Visible="False" />
        <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
        <br />
    </asp:Panel>
</asp:Content>
