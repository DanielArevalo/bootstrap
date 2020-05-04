<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br/><br/>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:ImageButton runat="server" ID="btnRegresar" ImageUrl="~/Images/btnRegresar.jpg" OnClick="btnRegresar_Click" ImageAlign="Right" />
        <asp:ImageButton runat="server" ID="btnGuardar" ImageUrl="~/Images/btnGuardar.jpg" 
            ValidationGroup="vgGuardar" OnClick="btnGuardar_Click" ImageAlign="Right"  />
        <div id="gvDiv">

        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td >
                    <hr  width="100%" noshade />
                </td>
            </tr>
             <tr>
                <td> Fecha 
                    <asp:TextBox ID="txtFecha" CssClass="textbox" maxlength="10" runat="server" Width="70"></asp:TextBox>
                      <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                      <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            PopupButtonID="Image1" 
                            TargetControlID="txtFecha"
                            Format="dd/MM/yyyy" >
                        </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtFecha" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revtxtFecha" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFecha" ForeColor="Red" ErrorMessage="Formato Fecha Invalida"></asp:RegularExpressionValidator>
                    &#160;
                     Oficina 
                    <asp:TextBox ID="txtOficina" enabled="false" CssClass="textbox" runat="server" Width="260px"></asp:TextBox>
                    &#160;&#160;   
                         <asp:Button  CssClass="btn8" 
                               ID="btnConsultar" runat="server" Text="Consultar" 
                    onclick="btnConsultar_Click"  Width="124px" 
                                Height="23px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="Button2" runat="server" CssClass="btn8" 
                                onclick="btnExportarExcel_Click" Text="Exportar a Excel"  Width="124px" 
                                Height="23px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>            
                    <asp:GridView ID="gvMovimiento" runat="server" Width="100%" 
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical" 
                        onpageindexchanging="gvMovimiento_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                              <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha" />
                            <asp:BoundField DataField="nom_tipo_ope" HeaderText="Tipo Operación" />
                            <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo Movimiento" />
                            <asp:BoundField DataField="nom_cajero" HeaderText="Nombre Usuario" />
                            <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto" />
                            <asp:BoundField DataField="cod_linea_credito" HeaderText="Línea" />
                            <asp:BoundField DataField="nombre_atributo" HeaderText="Atributo" />
                            <asp:BoundField DataField="tipo_cta" HeaderText="Tipo Cta" />
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Tipo Cta" />
                            <asp:BoundField DataField="valor_pago" HeaderText="Valor" DataFormatString="{0:N0}" >
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
            <tr>
                <td>            
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align:left">            
                    <strong>TOTALES</strong>
                </td>
            </tr>
            <tr>
                <td>            
                    <asp:GridView ID="gvTotales" runat="server" Width="40%" 
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical" 
                        onpageindexchanging="gvMovimiento_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                            <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo Movimiento" />
                            <asp:BoundField DataField="valor_pago" HeaderText="Valor" DataFormatString="{0:N0}">
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
      </div>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

</asp:Content>

