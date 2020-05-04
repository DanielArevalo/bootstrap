<%@ Page Title="Tipos de Cupos" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="50%">
            <tr>
            <td style="text-align:left">
            <strong>Filtrar por:</strong>
            </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align:left; width:120px">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="110px"/>
                </td>
                <td class="tdD" style="text-align:left; width:250px">
                    Descripción<br/>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                        MaxLength="100" Width="250px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
    <asp:UpdatePanel ID="panelGrilla" runat="server" Width="60%" ChildrenAsTriggers="true">
        <ContentTemplate>
        <br />
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
            OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            DataKeyNames="tipo_cupo" 
            style="font-size: x-small">
            <Columns>
                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                <asp:BoundField DataField="tipo_cupo" HeaderText="Código">
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                    <ItemStyle HorizontalAlign="Left" Width="78%"/>
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="gridHeader" />
            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
            <RowStyle CssClass="gridItem" />
        </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
