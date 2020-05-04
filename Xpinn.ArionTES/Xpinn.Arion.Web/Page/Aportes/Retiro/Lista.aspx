<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 92%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
                <td style="height: 15px; text-align: left; font-size: x-small;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    &nbsp;</td>
                <td style="height: 15px; text-align: left;">
                    Número Aporte
                    <br />
                    <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Identificación<br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" 
                        Width="157px" Enabled="False"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Linea Aporte
                    <br />
                    <asp:DropDownList ID="DdlLineaAporte" runat="server" AutoPostBack="True" Width="154px"
                        CssClass="textbox" onselectedindexchanged="DdlLineaAporte_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="99%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_aporte">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_aporte" HeaderText="Num. Aporte" >
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" />
                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="saldo" HeaderText="Saldo Total" />
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
