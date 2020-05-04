<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 90%">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;">Tipo de Persona<br />
                    <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="130px"
                        CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="N">Natural</asp:ListItem>
                        <asp:ListItem Value="J">Juridica</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;">Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left;">Identificación<br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox"
                        Width="100px"></asp:TextBox>
                </td>
                <td style="text-align: left;">Nombres<br />
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox"
                        Enabled="true" Width="160px"></asp:TextBox>
                </td>
                <td style="text-align: left;">Apellidos<br />
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox"
                        Enabled="true" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left;">Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" AppendDataBoundItems="True">
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
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_persona"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:BoundField DataField="cod_persona" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo Identificación">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Foto">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Huella">
                            <ItemTemplate>
                                <asp:Image ID="imgHuella" runat="server" Width="40" Height="40" ImageUrl='<%# "HandlerHuella.ashx?id=" + Eval("cod_persona")  %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Foto">
                            <ItemTemplate>
                                <asp:Image ID="imgFoto" runat="server" Width="40" Height="40" ImageUrl='<%# "HandlerFoto.ashx?id=" + Eval("identificacion")  %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
