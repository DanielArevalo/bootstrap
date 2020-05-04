<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align: left" colspan="5">
                    <strong>Criterio de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Cod.Giro:<br />
                    <asp:TextBox ID="txtCodGiro" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    F.Giro:<br />
                    <asp:TextBox ID="txtFechaGiro" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Identificacion:<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Nombres:<br />
                    <asp:TextBox ID="txtNombres" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Apellidos:<br />
                    <asp:TextBox ID="txtApellidos" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Num.Com:<br />
                    <asp:TextBox ID="txtNumCom" runat="server" ClientIDMode="Static" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    Tipo Comp.:<br />
                    <asp:DropDownList ID="ddlTipoCompro" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align: left">
                    Generado en:
                    <br />
                    <asp:DropDownList ID="ddlGenerado" runat="server" CssClass="textbox" Width="148px"
                        ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Forma Pago:
                    <br />
                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="145px"
                        ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Banco Giro:
                    <br />
                    <asp:DropDownList ID="ddlBancoGiro" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Cuenta Giro:
                    <br />
                    <asp:DropDownList ID="ddlCuentaGiro" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Usuario:
                    <br />
                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Width="148px"
                        ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Ordenar Por:
                    <br />
                    <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <br />
                    <asp:DropDownList ID="ddlOrdes" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    Y luego por
                    <br />
                    <asp:DropDownList ID="ddlLuegoPor" runat="server" CssClass="textbox" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <strong>Listado de Giros por distribuir</strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" ForeColor="Black" GridLines="Horizontal" PageSize="20" OnPageIndexChanging="gvLista_PageIndexChanging"
                    Style="font-size: x-small" OnRowEditing="gvLista_RowEditing" OnRowDataBound="gvLista_RowDataBound"
                    DataKeyNames="idgiro,identificacion,nombre">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idgiro" HeaderText="Codigo">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_distribucion" DataFormatString="{0:d}" HeaderText="Fecha Giro">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_Operacion" HeaderText="Cod_Operacion">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero_Com" HeaderText="Num.Com">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_Comp" HeaderText="Tipo.Com">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Generada">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="forma_Pago" HeaderText="Forma Pago">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="banco" HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuenta" HeaderText="Cuenta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="banc" HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Numcue_des" HeaderText="cta. Bancaria">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor" HeaderText="Valor">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
