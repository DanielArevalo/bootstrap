<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Seguridad_AuditoriaGeneral_ReportePerfil_Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwData" runat="server">
        <asp:Panel ID="pConsulta" runat="server" style="width: 70%;">
            <table style="width: 70%;">
                <tr>
                    <td style="font-size: x-small; text-align:left" colspan="5">
                        <strong>Criterios de Búsqueda :</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align:left">
                        Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" 
                            Width="121px"></asp:TextBox>
                     </td>
                    <td style="width: 120px; text-align:left">
                        Usuario<br />
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" 
                            Width="300px"></asp:TextBox>
                     </td>
                                      
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelListado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align:left">
                    <strong>Listado</strong><br />

                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Horizontal" PageSize="20"
                           AllowPaging="true"   OnPageIndexChanging="gvLista_PageIndexChanging"  
                           style="font-size:small">                           
                            <Columns>                                                            
                                <asp:BoundField DataField="cod_usu" HeaderText="Codigo"><ItemStyle HorizontalAlign="Left" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="identificación" ><ItemStyle HorizontalAlign="Left" Width="35%"/></asp:BoundField>
                                <asp:BoundField DataField="primer_nombre" HeaderText="Nombre Usuario" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="segundo_nombre" HeaderText="Perfil" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="cod_opcion" HeaderText="Codigo Opción" ><ItemStyle HorizontalAlign="Left" Width="20%" /></asp:BoundField>   
                                  <asp:BoundField DataField="nombreopcion" HeaderText="Nombre Opción"><ItemStyle HorizontalAlign="Left" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="Insertar" HeaderText="Insertar" ><ItemStyle HorizontalAlign="Left" Width="35%"/></asp:BoundField>
                                <asp:BoundField DataField="Modificar" HeaderText="Modificar" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="Borrar" HeaderText="Borrar" ><ItemStyle HorizontalAlign="Left" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="Consultar" HeaderText="Consultar" ><ItemStyle HorizontalAlign="Left" Width="20%" /></asp:BoundField>                                                                                                                             
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
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </asp:View>
        </asp:MultiView>
</asp:Content>

