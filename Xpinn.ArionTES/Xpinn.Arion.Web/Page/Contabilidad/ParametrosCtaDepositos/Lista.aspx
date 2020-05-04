<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/MensajeGrabar.ascx" TagName="mensaje" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="2" cellspacing="2">
            <tr>
                <td style="text-align: left">Codigo<br />
                    <asp:TextBox ID="txtCodigo" CssClass="textbox" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td style="text-align: left;">Tipo de Ahorro<br />
                    <asp:DropDownList ID="ddlTipoAhorro" AutoPostBack="true" CssClass="textbox" runat="server"
                        OnSelectedIndexChanged="ddlTipoAhorro_SelectedIndexChanged" Width="220px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 220px">Linea de Ahorro<br />
                    <ctl:ctlListarCodigo ID="ctlListarCodigoAhorro" runat="server" />
                </td>
                <td style="text-align: left; width: 220px">Tipo de Transaccion<br />
                    <ctl:ctlListarCodigo ID="ctlListarCodigoTransaccion" runat="server" />
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing" OnRowDataBound="gvLista_RowDataBound"
                    DataKeyNames="Codigo" Style="font-size: xx-small">
                    <%-- OnSelectedIndexChanged="gvLista_SelectedIndexChanged"--%>
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoAhorro" HeaderText="Tipo de Ahorro">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LineaAhorro" HeaderText="Linea de Ahorro">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="tipo_tran" HeaderText="Cod.TipoTran.">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoTrasaccion" HeaderText="Tipo Transaccion" >
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoCuenta" HeaderText="Codigo Cuenta">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreCuenta" HeaderText="Nombre Cuenta">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomtipo_mov" HeaderText="Tipo Movimiento">
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <uc4:mensaje ID="ctlMensaje" runat="server" />
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
</asp:Content>
