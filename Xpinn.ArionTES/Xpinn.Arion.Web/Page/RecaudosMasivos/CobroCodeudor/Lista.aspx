<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table>
        <tr>
            <td class="logo">No. Radicación:<br />
                <asp:TextBox ID="txtRadicacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                <br />
            </td>
            <td class="logo"">
            <asp:Panel ID="Panel1" runat="server" Width="130px">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                <br />  
                <asp:TextBox ID="txtFecha" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                    PopupButtonID="imgCalendario"
                    TargetControlID="txtFecha"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imgCalendario" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </asp:Panel>
                &nbsp;<br />
            </td>
            <td class="logo">Cod.Deudor<br />
                <asp:TextBox ID="txtCodDeudor" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                    Width="120px"></asp:TextBox>
                <br />
            </td>
            <td class="logo">Ident.Deudor<br />
                <asp:TextBox ID="txtIdentDeudor" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                    Width="120px"></asp:TextBox>
                <br />
            </td>
            <td class="logo">Cod.Codeudor<br />
                <asp:TextBox ID="txtCodCodeudor" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                    Width="120px"></asp:TextBox>
                <br />
            </td>
            <td class="logo">Ident.Codeudor<br />
                <asp:TextBox ID="txtIdentCodeudor" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                    Width="120px"></asp:TextBox>
                <br />
            </td>
        </tr>
    </table>
    <hr style="width: 100%" />
    <table style="width: 100%;">
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvListaCreditosCodeudores" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal"
                    PageSize="20"
                    ShowHeaderWhenEmpty="True" Width="100%"
                    OnPageIndexChanging="gvListaCreditosCodeudores_PageIndexChanging"
                    OnSelectedIndexChanged="gvListaCreditosCodeudores_SelectedIndexChanged"
                    OnRowCommand="OnRowCommandDeleting"
                    Onrowdeleting="gvListaCreditosCodeudores_RowDeleting"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" >
                            <ItemStyle Width="16px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="idcobrocodeud" HeaderText="Codigo" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Radicación" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField DataField="cod_deudor" HeaderText="Cod.Deudor" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="identificacion_deudor" HeaderText="Ident. Deudor" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField DataField="nombre_deudor" HeaderText="Nombre Deudor" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField DataField="cod_persona" HeaderText="Cod.Codeudor" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="identificacion_codeudor" HeaderText="Ident. Codeudor"  ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField DataField="nombre_codeudor" HeaderText="Nombre Codeudor" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje"  ItemStyle-HorizontalAlign="Left" DataFormatString="% {0:n}" />
                        <asp:BoundField DataField="valor" HeaderText="Valor"  ItemStyle-HorizontalAlign="Left" DataFormatString="$ {0:n}" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
