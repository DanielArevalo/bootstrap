<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 100%; margin-right: 22px;">
                <tr>
                    <td class="logo" style="width: 41px">
                        C&oacute;digo&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtCod_opcion" runat="server" CssClass="textbox" />
                    </td>
                    <td>
                        Descripci&oacute;n&nbsp;*&nbsp;<br />
                        <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox" 
                            Width="279px" />
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
            </table>
            <div style="overflow:scroll;height:500px;width:100%;">
                <asp:GridView ID="gvLista" runat="server" Width="88%" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True" OnRowDataBound="gvLista_RowDataBound" OnRowEditing="gvLista_RowEditing" Style="margin-right: 43px">
                    <Columns>
                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                                ShowEditButton="True" />
                        <asp:BoundField DataField="cod_concepto" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                 <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
            </div>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos Modificados Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
