<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - BalanceFamilia :." %>

<%@ Register Src="../../../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                    DataKeyNames="COD_BALANCE" HorizontalAlign="Right">
                    <Columns>
                        <asp:BoundField DataField="COD_BALANCE" HeaderText="Cod_balance" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:BoundField DataField="TERRENOSYEDIFICIOS" HeaderText="Terrenos y Edificios"  DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="OTROS" HeaderText="Otros" DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TOTALACTIVO" HeaderText="Total Activo"  DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="CORRIENTE" HeaderText="Corriente"  DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="LARGOPLAZO" HeaderText="Largo Plazo"  DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TOTALPASIVO" HeaderText="Total Pasivo"  DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right"  />
                        <asp:BoundField DataField="PATRIMONIO" HeaderText="Patrimonio"  DataFormatString="{0:N0}"  ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TOTALPASIVOYPATRIMONIO" HeaderText="Total Pasivo y Patrimonio" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod_balance&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_balance" runat="server" CssClass="textbox" MaxLength="128"
                        Enabled="False" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                ACTIVO
            </td>
            <td class="tdD">
                PASIVO
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Terrenos y Edificios<br />
                <uc1:decimales ID="txtTerrenosyedificios" runat="server" />
            </td>
            <td class="tdD">
                Corriente<br />
                <uc1:decimales ID="txtCorriente" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Otros<br />
                <uc1:decimales ID="txtOtros" runat="server" />
                <br />
            </td>
            <td class="tdD">
                Largo Plazo<br />
                <uc1:decimales ID="txtLargoplazo" runat="server" />
                <br />
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_BALANCE').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
