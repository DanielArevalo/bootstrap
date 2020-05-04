<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaComentarioDetalle" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx"  TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptManager" />
      <asp:Panel ID="pComentario" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    Comentario
                    <asp:TextBox ID="txtComment" runat="server" CssClass="textbox" Width="640px" ></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox Text="Puede Ver Asociado" ID="chkPuedeVerAsociado" runat="server" />
                </td>
            </tr>            
        </table>        
    </asp:Panel>

    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvComentario" AllowPaging="True" runat="server" Width="100%" PageSize="20"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvComentario_PageIndexChanging" 
                    OnRowDeleting="gvComentario_RowDeleting" style="font-size: x-small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                        <asp:BoundField DataField="idComentario" HeaderText="IdComentario" >
                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" >
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Hora" HeaderText="Hora" >
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Comentario" >
                            <ItemStyle HorizontalAlign="Left" Width="70%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Puede Ver Asociado">
                            <ItemTemplate>
                                <cc1:CheckBoxGrid CommandArgument='<%#((GridViewRow) Container).RowIndex %>' 
                                    ID="chkVerComentario" Checked='<%# Eval("puedeVerAsociado") %>' 
                                    OnCheckedChanged="chkVerComentario_CheckedChanged" AutoPostBack="true" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>