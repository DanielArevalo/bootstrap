<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
    
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<style type="text/css">
    #divCombos
    {border: solid 2px #cccccc;
    border-top: none;
    border-left: none;
    border-right: none;
    padding: 24px;
    text-align: left;
    padding-top: 59px;}
     .diccla{
    border: solid 2px #cccccc;
    border-top: none;
    border-left: none;
    border-right: none;
    padding: 24px;
    text-align: left;
    padding-top: 59px;
    }
    
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <div id="divCombos" class="diccla" >
            <div style="display:inline; padding-right:55px;">
                Oficinas: 
                
                <asp:DropDownList ID="ddlOficina" ClientIDMode="Static" CssClass="textbox" runat="server">
                </asp:DropDownList>
            </div>
            <div style="display:inline">
            Ciudad: 
                
                <asp:DropDownList ID="ddlCiudad" Width="191px" ClientIDMode="Static" CssClass="textbox" runat="server">
                </asp:DropDownList>
            </div>
        </div>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" ForeColor="Black" GridLines="Horizontal" PageSize="20" OnPageIndexChanging="gvLista_PageIndexChanging"
                    Style="font-size: x-small" OnRowDeleting="gvLista_RowDeleting" OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idoficiudad">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                        <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre_Oficina" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codciudad" HeaderText="Cod. Ciudad">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre_Ciudad" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
        <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
