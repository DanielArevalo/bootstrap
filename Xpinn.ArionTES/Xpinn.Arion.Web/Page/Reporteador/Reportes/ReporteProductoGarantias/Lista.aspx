<%@ Page Title=".: Xpinn - Reporte Producto garantias:." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 150px;">Fecha de corte<br />
                <ucFecha:fecha ID="ucFechaCorte" runat="server" style="text-align: center"/><br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 150px;">Tipo de Archivo<br />
                EXCEL
            </td>
            <td style="text-align: left; vertical-align: top">Nombre del Archivo<br />
                <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvArchivo" runat="server"
                    ErrorMessage="Ingrese el Nombre del archivo a Generar"
                    ValidationGroup="vgExportar" Display="Dynamic" ControlToValidate="txtArchivo"
                    ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                <br />
            </td>
            <td style="text-align: left; width: 284px;">&nbsp;</td>
            <td style="text-align: left; width: 284px;">&nbsp;</td>
        </tr>
        <tr>
         
            <td colspan="4">
                <br />  <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="false"  HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="100%" PageSize="20"  >
                      <Columns>
                          <asp:TemplateField>
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="Tipo_producto" HeaderText="Tipo_producto" />
                  <asp:BoundField DataField="Nombre_producto" HeaderText="Nombre_producto" />
                          <asp:TemplateField HeaderText="NICHO_MERCADO">
                              <ItemTemplate>
                                  <asp:Label ID="lblNicho" runat="server" Visible="false"></asp:Label>
                                  <asp:TextBox ID="txtNicho" runat="server" CssClass="textbox" Width="100"></asp:TextBox>
                              </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="MONTO_MINIMO">
                              <ItemTemplate>
                                  <asp:Label ID="lblMonto" runat="server" Visible="false"></asp:Label>
                                  <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Width="100"></asp:TextBox>
                              </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="TASA_RENTABILIDAD">
                              <ItemTemplate>
                                  <asp:Label ID="lblTasa" runat="server" Visible="false"></asp:Label>
                                  <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="100"></asp:TextBox>
                              </ItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                  </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

