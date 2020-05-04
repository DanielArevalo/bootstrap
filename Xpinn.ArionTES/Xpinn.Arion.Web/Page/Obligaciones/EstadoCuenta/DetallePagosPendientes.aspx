<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="DetallePagosPendientes.aspx.cs" Inherits="DetallePagosPendientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="gvDiv">
<table cellpadding="5" cellspacing="0" style="width: 95%">
        <tr>
            <td>
             <asp:Panel ID="Panel1" runat="server">
            <table style="width:100%;">
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                        <strong>Detalle de Pagos Pendientes</strong></td>
                </tr>
            </table>
        </asp:Panel>
            </td> 
        </tr>

    <tr style="text-align: left">
        <td>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel3" runat="server">
                <table style="width:100%;">
                    <tr valign="top">
                     <td style="text-align: left">
                            No Obligación<br/>
                            <asp:TextBox ID="txtNroObligacion" runat="server" Enabled="false" 
                                CssClass="textbox" Width="115px" ></asp:TextBox>
                        </td>
                         <td style="text-align: left">
                            Pagaré<br/>
                            <asp:TextBox ID="txtPagare" runat="server" Enabled="false" CssClass="textbox" 
                                 Width="102px" ></asp:TextBox>
                        </td>
                         <td style="text-align: left">
                            Estado<br/>
                            <asp:TextBox ID="txtEstado" runat="server" Enabled="false" CssClass="textbox" 
                                 Width="109px" ></asp:TextBox>
                        </td>
                        <td class="gridIco" style="text-align: left">
                         Entidad<br/>
                             <asp:DropDownList ID="ddlEntidad" runat="server" 
                                Height="27px" Width="148px" Enabled="false" CssClass="dropdown">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            Fecha Desembolso<br/>
                            <asp:TextBox ID="txtFechaDesembolso" runat="server" CssClass="textbox" Enabled="false"
                                MaxLength="12" Width="112px"></asp:TextBox>
                        </td>
                         <td style="text-align: left">
                           Monto Aprobado<br/>
                           <uc1:decimales ID="txtMontoApro" enabled="false" runat="server" CssClass="textbox" width="95px"  />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
     <tr style="text-align: center">
        <td>
         <div id="DivButtons">
            <asp:ImageButton ID="btnImprimir" runat="server" 
                 ImageUrl="~/Images/btnImprimir.jpg" />&#160;
          </div>
        </td>            
    </tr>

     <tr style="text-align: left">
        <td>
            <strong>Distribución de Pagos Pendientes por Cuotas</strong></td>
    </tr>
     <tr>
         
         <td align="left">
                <asp:GridView ID="gvDistribPagPenCuo" runat="server"
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                         <asp:BoundField DataField="nrocuota"  HeaderText="No Cuota" />
                         <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha Cuota" />
                         <asp:BoundField DataField="amort_cap" HeaderText="Capital" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_corriente" HeaderText="Interes Corriente" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_mora" HeaderText="Interes Mora" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
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
            </td>
        </tr>
    </table>

               <script type="text/javascript">
                   $(".numeric").numeric();
                   $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
                   $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
                   $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
                   $("#remove").click(
		function (e) {
		    e.preventDefault();
		    $(".numeric,.integer,.positive").removeNumeric();
		}
	);
        </script>
        </td>
    </tr>
    </table>
    </div> 

</div>
</asp:Content>

