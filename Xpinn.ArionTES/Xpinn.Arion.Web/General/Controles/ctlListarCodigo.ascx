<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlListarCodigo.ascx.cs" Inherits="ctlListarCodigo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    function Forzar() {
        __doPostBack('', '');
    }
</script>
<div id="contenedorPrincipal">
    <asp:HiddenField ID="hiddenCodigo" runat="server" Visible="false" />
    <asp:HiddenField ID="hiddenValue" runat="server" Visible="false" />
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 2%;">
                <asp:ImageButton ID="imgListar" ImageUrl="~/Images/Lupa.jpg"
                    runat="server" Width="20px" CausesValidation="false" OnClientClick="return false;" />
            </td>
            <td style="width: 60%;">
                <asp:TextBox ID="txtDatos" Width="95%" CssClass="textbox" runat="server" ReadOnly="True" Style="text-align: left" />
            </td>
        </tr>
    </table>
    <asp:PopupControlExtender ID="txtDato_PopupControlExtender" runat="server"
        TargetControlID="imgListar"
        PopupControlID="panelLista" OffsetY="22">
    </asp:PopupControlExtender>
    <asp:Panel ID="panelLista" runat="server" Height="140px" Width="300px"
        BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
        ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none; min-width: 250px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 40%;">
                    <asp:TextBox ID="txtBuscarCodigo" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td style="width: 5%;"></td>
                <td style="width: 40%">
                    <asp:TextBox ID="txtBuscarDescripcion" CssClass="textbox" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="imgBuscar" CausesValidation="false" ImageUrl="~/Images/Lupa.jpg" Height="25px" runat="server" OnClick="imgBuscar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvFiltrarCodigo" runat="server" Height="80%" Width="100%" AutoGenerateColumns="False"
                        PageSize="5" ShowHeaderWhenEmpty="True" Style="font-size: xx-small"
                        OnSelectedIndexChanged="gvFiltrarCodigo_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/gr_info.jpg" ShowSelectButton="true" />
                            <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hiddenValue" Value='<%# Eval("HiddenValue") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <HeaderStyle CssClass="gridHeader" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
