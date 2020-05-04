<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td style="text-align:left">Código del Tipo<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align:left">Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="260px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="90%" AutoGenerateColumns="False" GridLines="Horizontal"
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" DataKeyNames="cod_tipo">
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                        <asp:BoundField DataField="cod_tipo" HeaderText="Tipo de Activo">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left" Width="65%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>                
            </td>            
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado" />
            </td>
        </tr>
    </table>

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>

