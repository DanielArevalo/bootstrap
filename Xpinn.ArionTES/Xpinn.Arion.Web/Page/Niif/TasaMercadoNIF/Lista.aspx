<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pDatos" runat="server">
        
        <table style="width: 80%">
            <tr>
                <td>
                    <br/><br/><br/>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnPageIndexChanging="gvLista_PageIndexChanging" style="font-size: x-small" 
                        OnRowDeleting="gvLista_RowDeleting" DataKeyNames="cod_tasa_mercado" 
                        GridLines="Horizontal">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="../../../Images/gr_edit.jpg" ShowEditButton="true" >
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" >
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="cod_tasa_mercado" HeaderText="Codigo" >
                                <ItemStyle HorizontalAlign="left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha Inicial" DataField="fecha_inicial" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_final" HeaderText="Fecha Final" DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle HorizontalAlign="center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa" HeaderText="Tasa">
                                <ItemStyle HorizontalAlign="center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" >
                                <ItemStyle HorizontalAlign="left" Width="400px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:HiddenField ID="hfNiif" runat="server" />
   
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>