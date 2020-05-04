<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Devoluciones :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 800px">
                <tr>
                    <td style="width: 120px; text-align: left">Num. Servicio<br />
                        <asp:TextBox ID="txtNumServ" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 120px">Concepto<br />
                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 140px">Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                    <td style="text-align: left; width: 140px">Código de nómina<br />
                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" />
                        <br />
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="chkExportarCSV" Text="Exportar .csv" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">Fec. Devolución<br />
                        <ucFecha:fecha ID="txtFechaDev" runat="server" CssClass="textbox" Width="90%"/>
                    </td>
                    <td style="text-align: left; width: 120px">Num. Recaudo<br />
                        <asp:TextBox ID="txtNumRec" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">Estado<br />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">Oficina<br />
                        <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="chkExportarSaldosEnCero" Text="Exportar saldos en 0" runat="server" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Lista de Devoluciones</strong><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="num_devolucion,Estado"
                                OnRowDeleting="gvLista_RowDeleting" Style="font-size: x-small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                    <asp:BoundField DataField="num_devolucion" HeaderText="Num. Concepto">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_devolucion" HeaderText="Fec. Devolucion"
                                        DataFormatString="{0:d}"></asp:BoundField>
                                    <asp:BoundField DataField="fecha_descuento" HeaderText="Fec. Descuento"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_recaudo" HeaderText="Num. Recaudo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="iddetalle" HeaderText="ID Detalle">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Origen" DataField="origen" />
                                    <asp:BoundField HeaderText="Estado" DataField="estado" />
                                    <asp:BoundField HeaderText="Oficina" DataField="nom_oficina" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>
