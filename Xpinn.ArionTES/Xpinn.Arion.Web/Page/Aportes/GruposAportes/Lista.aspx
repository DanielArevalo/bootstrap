<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
    <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="606px">            
        <table cellpadding="5" cellspacing="0" style="width: 99%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="5">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    Código Linea Aporte
                    <br />
                    <asp:TextBox ID="txtCodLinea" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                    <br />
                </td>
                <td style="height: 15px; text-align: left;">
                    &nbsp;</td>
                <td style="height: 15px; text-align: left;">
                    <br />
                </td>
                <td style="height: 15px; text-align: left; width: 185px;">
                </td>
                <td style="height: 15px; text-align: left;">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:90%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="idgrupo" 
                    style="text-align: left">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idgrupo" HeaderText="Grupo" />
                        <asp:BoundField DataField="cod_linea_aporte" HeaderText="Codigo Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Linea">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje Distribución" >
                            <ItemStyle HorizontalAlign="Left" Width="190px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
