<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 100%; margin-right: 22px;">
                <tr>
                    <td class="logo" style="width: 20%">
                        C&oacute;digo&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtCod_opcion" runat="server" CssClass="textbox" />
                    </td>
                    <td align="left">
                        Descripci&oacute;n&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox"  
                            width="40%" />
                    </td>
                </tr>


                <tr>
                    <td style="height: 15px; text-align: left; width: 41px;">
                        <br />
                        <asp:TextBox ID="txtcodigo" runat="server" CssClass="textbox" Width="140px" Visible="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 118px;">
                        <asp:Label ID="Labelerror" runat="server" Style="color: #FF0000; font-weight: 700;
                            font-size: x-small;" colspan="5" Text=""></asp:Label>
                    </td>
                   
                </tr>

                
                <tr>
                    <td style="text-align: left" colspan="4">
                    </td>
                </tr>
            </table>
            <div style="overflow:scroll;height:500px;width:100%;">
                <asp:GridView ID="gvLista" runat="server" Width="88%" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True" OnRowDataBound="gvLista_RowDataBound" Style="margin-right: 43px" 
                    OnRowDeleting="gvLista_RowDeleting" DataKeyNames="tipo_motivo,descripcion"
                   OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco"><ItemTemplate><asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco"><ItemTemplate><asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco"><ItemTemplate><asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" /></ItemTemplate><ItemStyle CssClass="gI" /></asp:TemplateField>
                        <asp:BoundField DataField="tipo_motivo" HeaderText="Código"><ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" /></asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción"><ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80%" /></asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </div>
        </asp:View>
    </asp:MultiView>
        <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
