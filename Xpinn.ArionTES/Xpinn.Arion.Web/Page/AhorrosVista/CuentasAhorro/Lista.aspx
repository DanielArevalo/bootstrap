<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro"
    TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="5" cellspacing="0">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong>
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">Cuenta<br />
                    <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">Línea<br />
                    <ctl:ddlLineaAhorro ID="ddllineaahorro" runat="server" Width="250px" />
                </td>
                <td style="height: 15px; text-align: left;">Fecha Apertura<br />
                    <ucFecha:fecha ID="txtFecApertura" runat="server" CssClass="textbox" />
                </td>
               <td style="text-align: left">Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="170px" />
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">Identificación
                    <br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td style="height: 15px; text-align: left;">Nombre del Titular<br />
                    <asp:TextBox ID="txtNomTitular" runat="server" CssClass="textbox" Width="280px" />
                </td>
                <td style="height: 15px; text-align: left;">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" />
                    <br />
                </td>
                <td style="height: 15px; text-align: left;">Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="210px" />
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <asp:Panel ID="PanelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        Text="Exportar a Excel" />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width:100%; max-height:700px; overflow:scroll">
                        <asp:GridView ID="gvLista" runat="server" Width="100%"
                            AutoGenerateColumns="False" GridLines="Horizontal"
                            ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            Style="font-size: xx-small" DataKeyNames="numero_cuenta" PageIndex="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_cuenta" HeaderText="No.Cuenta">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_linea_ahorro" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Nombre Linea">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Titular">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre del Titular">
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_destino" HeaderText="Cod.Dest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="modalidad" HeaderText="Cod.Mod.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="F.Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_cierre" HeaderText="F.Cierre" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:c2}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_canje" HeaderText="Saldo Canje" DataFormatString="{0:c2}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="forma_tasa" HeaderText="Forma Tasa">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_historico" HeaderText="Tipo Histórico">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="desviacion" HeaderText="Desviación" DataFormatString="{0:c2}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_tasa" HeaderText="Tipo Tasa">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tasa" HeaderText="Tasa" DataFormatString="{0:c2}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_interes" HeaderText="F.Interés" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_intereses" HeaderText="Saldo Interés" DataFormatString="{0:c2}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="retencion" HeaderText="Retención" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                        Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
