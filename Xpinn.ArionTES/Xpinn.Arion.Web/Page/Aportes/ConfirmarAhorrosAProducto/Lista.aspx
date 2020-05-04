<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Confirmar Transacción :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj) {
                obj.click();
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwData" runat="server">
                <asp:Panel ID="pConsulta" runat="server">
                    <table style="width: 80%;">
                        <tr>
                            <td style="text-align: left; font-size: x-small" colspan="5">
                                <strong>Criterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Fecha<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar"
                                    Width="100px" />
                            </td>
                            <td style="text-align: left">Identificación<br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtIdentificacion"
                                    Display="Dynamic" ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                    SetFocusOnError="True" Style="font-size: x-small" Type="Integer" ValidationGroup="vgGuardar" />
                            </td>
                            <td style="text-align: left">Nombres<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="230px" />
                            </td>
                            <td style="text-align: left">Apellidos<br />
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="230px" />
                            </td>
                            <td style="text-align: left">Oficina :<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="180px"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">Zonas<br />
                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox">                                
                            </asp:DropDownList>
                        </td>
                        <asp:Panel runat="server" ID="pnlAse" Visible="false">
                            <td style="text-align: left">Asesor<br />
                                <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="textbox">                                
                                </asp:DropDownList>
                            </td>
                        </asp:Panel>
                            <td style="text-align: left;display: flex;align-items: center;width: 150px;">&nbsp<br />
                            <br /><br />
                            <asp:CheckBox runat="server" ID="chkSinAsesor" Text="Incluir sin asesor" />
                        </td>              
                        </tr>
                        <tr>
                         <td style="font-size: x-small; text-align: left">
                             <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                        </td>
                    </tr>

                    </table>
                    <hr style="width: 100%" />
                </asp:Panel>
                <asp:Panel ID="pDatos" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="font-size: x-small; text-align: left">
                                <strong>Listado de cruces pendientes por aplicar</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" DataKeyNames="idcruceahorro, tipo_producto, tipo_tran"
                                    OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image"
                                            ShowDeleteButton="True" DeleteImageUrl="~/Images/gr_elim.jpg" />
                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg"
                                            ShowEditButton="True" />
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idcruceahorro" HeaderText="Código">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_pago" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Cod persona">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombres y Apellidos">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_cuenta" HeaderText="Num Cuenta">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" Visible="false">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_producto" HeaderText="Num Producto">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tran" HeaderText="Tipo Tran" Visible="false">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tipo Tran">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="concepto" HeaderText="Generación">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_pago" HeaderText="Valor Pago" DataFormatString="{0:N0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="otro_atributo" HeaderText="Asesor">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <center>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado." /></center>
            </asp:View>
            <asp:View ID="mvFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Registros modificados correctamente"></asp:Label><br />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: center; font-size: large; width: 35%">&#160;             
                                    </td>
                                    <td style="text-align: center; font-size: large; width: 30%">
                                        <br />
                                        <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px"
                                            CellPadding="0" ForeColor="Black" GridLines="Vertical" PageSize="5" Style="font-size: x-small; text-align: left;"
                                            Width="100%" DataKeyNames="num_comp">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="num_comp" DataNavigateUrlFormatString="../../Contabilidad/Comprobante/Lista.aspx?num_comp={0}"
                                                    DataTextField="num_comp" HeaderText="Num.Comp" Target="_blank" Text="Número de Comprobante" />
                                                <%--<asp:BoundField DataField="num_comp" HeaderText="Nro. Comprobante">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>--%>
                                                <asp:BoundField DataField="tipo_comp" HeaderText="Tipo.Comp" />                                                
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
                                    </td>
                                    <td style="text-align: center; font-size: large; width: 35%">&#160;             
                                    </td>
                                </tr>
                            </table>
                        </td>
                </table>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
