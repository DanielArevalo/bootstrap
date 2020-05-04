<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>



<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial Desembolso:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final Desembolso:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left"><br />Oficina:
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>
            <td id="categoria" style="text-align: left; display: none"><br />Categoria:<br />
                <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left"><br />Linea de Crédito:<br />
                <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: center; margin-left: 1pc;"><br />Consultar Historico:<br />
                <asp:CheckBox runat="server" Style="margin-left: 1px;" CssClass="chk" ID="chkHistorico" />
            </td>
        </tr>
    </table>


    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"
                            ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowEditing="gvLista_RowEditing"
                            OnPageIndexChanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <%--      <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="nombre" HeaderText="Cod.Oficina" />
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Número Radicación" />
                                <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                                <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido" />
                                <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido" />
                                <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre" />
                                <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre" />
                                <asp:BoundField DataField="fecha_desembolso" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:N0}" HeaderText="Monto Aprobado">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto_desembolsado" DataFormatString="{0:N0}" HeaderText="Valor Desembolsado">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea_credito" HeaderText="Nombre Linea Credito" />
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
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <script>
        $(document).ready(function () {
            $(".chk").children().attr("OnChange", "categoria()")
        });
        function categoria() {
            console.log();
            if ($("#cphMain_chkHistorico").is(":checked"))
                $("#categoria").show();
            else {
                $("#categoria").hide();
                $("#cphMain_ddlCategoria_txtDato").val("");
            }
        }
    </script>


</asp:Content>
