<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="pConsulta" runat="server" Style="width: 70%;">
            <table style="width: 70%;">
                <tr>
                    <td style="font-size: x-small; text-align: left" colspan="3">
                        <strong>Filtrar por :</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; text-align: left">
                        <asp:TextBox ID="txtNroProducto" runat="server" CssClass="textbox" Width="121px"
                            Visible="false"></asp:TextBox>
                        Línea<br />
                        <asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" Width="310px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fecha Inicial<br />
                        <ucFecha:fecha ID="txtFechaIni" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 80%;">
                <tr>
                    <td colspan="3" style="margin-left: 40px">
                        <strong>Listado de Cuentas</strong><br />
                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Horizontal" PageSize="20"
                            Style="font-size: x-small"  DataKeyNames="NumeroCuenta" 
                            onpageindexchanging="gvLista_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="numero_cuenta" HeaderText="Numero Cuenta" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="Linea" HeaderText="Linea" />
                                <asp:BoundField DataField="Identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                               <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Fecha_Apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Saldo" HeaderText="Saldo Inicial">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                             <%--   <asp:BoundField DataField="Saldo_Base" HeaderText="Saldo base">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <%-- <asp:BoundField DataField="Tasa_interes" HeaderText="Tasa Interes">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="dias" HeaderText="Dias Liquidados">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Tasa_interes" HeaderText="Tasa Interes">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Interes" HeaderText="Interes">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Retefuente" HeaderText="Retención">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_Neto" HeaderText="Valor Neto">
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
                        <asp:Label ID="lblInfo" runat="server" 
                            Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
                
                  
                
            </table>
            <table style="width: 35%;">
                <tr>
                    <td style="margin-left: 40px">
                        &nbsp;</td>
                    <td style="margin-left: 40px">
                        <asp:Label ID="lblInteres" runat="server" style="float:left; margin-left:33px" 
                            Visible="False" Width="90px">Total Interes</asp:Label>
                        <br />
                        <asp:TextBox ID="txtInteres" runat="server" CssClass="textbox" 
                             ReadOnly="true" 
                            style="float:left; margin-left:29px" Visible="false"></asp:TextBox>
                    </td>
                    <td style="margin-left: 40px">
                        <asp:Label ID="lblRetencion" runat="server" 
                            style="float:left; margin-left:33px" Visible="False">Total Retención</asp:Label>
                        <br />
                        <asp:TextBox ID="txtTotalRetencion" runat="server" CssClass="textbox" 
                            ReadOnly="true" style="float:left; margin-left:29px" Visible="false"></asp:TextBox>
                    </td>
                    <td style="margin-left: 40px; width: 166px;">
                        <asp:Label ID="lblNeto" runat="server" style="float:left; margin-left:33px" 
                            Visible="False">Total Valor Neto</asp:Label>
                        <asp:TextBox ID="txtNeto" runat="server" CssClass="textbox" ReadOnly="true" 
                            style="float:left; margin-left:29px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
