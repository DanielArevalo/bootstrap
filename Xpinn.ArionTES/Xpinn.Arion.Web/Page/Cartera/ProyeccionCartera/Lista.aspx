<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td style="width: 199px; text-align: left">
                <asp:Label ID="LabelFecha" runat="server" Text="Fecha"></asp:Label><br />
                <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="20px" MaxLength="10"
                    Width="188px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                    TargetControlID="txtFechaIni">
                </asp:CalendarExtender>
                <br />
                <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Debe ingresar la fecha" ControlToValidate="txtFechaIni" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />

            </td>
            <td style="width: 443px; text-align: left">
                <asp:Label ID="LabelPeriodos" runat="server" Text="Número Períodos" ></asp:Label><br />
                <asp:TextBox ID="txtPeriodos" runat="server" CssClass="textbox" Height="20px" MaxLength="10" Text="36"
                    Width="65px"></asp:TextBox><br />
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                    ErrorMessage="Debe digitar un valor entre 1 a 99" MaximumValue="99" 
                    MinimumValue="1" style="font-size: x-small" ControlToValidate="txtPeriodos"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="columnForm50" style="text-align: left" colspan="2">
                &nbsp;</td>
            <td class="columnForm50" style="text-align: left">
                &nbsp;</td>
            <td class="columnForm50" style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 199px; text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Text="Consultar" Width="182px"
                    Height="26px" OnClick="btnConsultar_Click" />
                <br />
            </td>
            <td class="ctrlLogin" style="width: 443px">
                &nbsp;
            </td>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdI">
                &nbsp;</td>
            <td class="tdI">
                <br />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>           
            <td style="text-align:left" >
                <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn8" Height="28px"
                    OnClick="btnExportarExcel_Click" Text="Exportar a Excel" Width="124px" />
                <br />
                <br />                
                <div style="overflow:scroll;width:1000px;height:500px" >
                    <asp:GridView ID="gvProyeccion" runat="server" PageSize="20" Height="280px"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                        Style="font-size: x-small" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pagare" HeaderText="Pagare" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" 
                                DataFormatString="{0:d}" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_terminacion" HeaderText="Fecha Terminación" 
                                DataFormatString="{0:d}" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea" HeaderText="Línea" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Días Mora" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:N}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha Próximo Pago" DataFormatString="{0:d}" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                    </asp:GridView>
                </div>
                <asp:Label ID="lblTotRegs" runat="server"></asp:Label>
            </td>
        </tr>         
    </table>                           

</asp:Content>
