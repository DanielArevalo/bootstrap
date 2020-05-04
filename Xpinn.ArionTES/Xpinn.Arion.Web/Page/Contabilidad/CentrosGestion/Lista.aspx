<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
    <table style="width: 660px">
        <tr>
            <td style="width: 120px; text-align: left">
                Código<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 200px">
                Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="95%" />
            </td>
            <td style="text-align: left; width: 180px">
                Depende de<br />
                <asp:DropDownList ID="ddlDepende" runat="server" CssClass="textbox" Width="95%" />
            </td>
            <td style="text-align: left; width: 160px">
                Nivel<br />
                <asp:TextBox ID="txtNivel" runat="server" CssClass="textbox" Width="95%" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="panelGrilla" runat="server">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="centro_gestion" OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="centro_gestion" HeaderText="Código">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nivel" HeaderText="Nivel">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Creación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_depende" HeaderText="Depende de">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
