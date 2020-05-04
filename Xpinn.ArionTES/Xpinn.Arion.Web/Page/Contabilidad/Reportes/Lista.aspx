<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Contabilidad_Reportes" Title=".: Xpinn - Asesores :."%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="727px">
        <table style="width:100%;">        
            <tr>
                <td class="ctrlLogin" style="text-align: left; width: 1251px">                
                    Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" 
                        CssClass="dropdown" 
                        onselectedindexchanged="ddlConsultar_SelectedIndexChanged" Height="39px" 
                        Width="469px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">REPORTE INTERESES PAGADOS DE CREDITOS</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvConsultar" runat="server" 
                        ControlToValidate="ddlConsultar" Display="Dynamic" 
                        ErrorMessage="Seleccione un tipo de consulta" ForeColor="Red" 
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                </asp:CompareValidator>
                    <br />
                </td>
                <td align="center" style="width: 622px">
                    <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" Width="209px">
                        <asp:Label ID="LabelFecha_gara2" runat="server" Text="Fecha Inicial"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" cssClass="textbox" Height="23px" 
                            maxlength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="Image1" TargetControlID="txtFechaIni">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label4" runat="server" style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                    &nbsp;
                    <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center" Width="209px">
                        <asp:Label ID="LabelFecha_gara3" runat="server" Text="Fecha Final"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaFin" runat="server" cssClass="textbox" Height="23px" 
                            maxlength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="Image1" TargetControlID="txtFechaFin">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label5" runat="server" style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server" onactiveviewchanged="mvLista_ActiveViewChanged">        
        <asp:View ID="vGridReporte" runat="server">
            <div style="overflow:inherit; height:197px; width:922px;">
            <div style="width:1500px; text-align: left;" class="align-rt">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a excel" />
                <br />
                <br />
                       
                <div style="text-align: left">
                    <asp:GridView ID="gvReportecierre" runat="server" AutoGenerateColumns="False" 
                        GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" Height="130px" 
                        onrowdatabound="gvReportecierre_RowDataBound" 
                        onselectedindexchanged="gvReportecierre_SelectedIndexChanged" 
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True" 
                        style="font-size: x-small" Width="51%">
                        <Columns>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos">
                            <ItemStyle HorizontalAlign="center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="valor" DataFormatString="{0:c}" HeaderText="Valor">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
                <br />
                <asp:Label ID="lblTotalRegs" runat="server" />
            </div>
            </div>
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView> 

</asp:Content>

