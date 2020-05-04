<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvEdades" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="2" cellspacing="0" width="70%">
                    <tr >            
                        <td class="tdI" style="text-align:left" height="5">
                            Fecha de corte</td>
                        <td class="tdD" style="text-align:left" height="5">
                        </td>
                        <td class="tdD" height="5" style="text-align:left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; width: 150px">
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="textbox" 
                                Width="150px">
                            </asp:DropDownList>
                            <uc:fecha ID="txtFechaActual" runat="server" Visible="false" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:CheckBox ID="chkFecha" runat="server" AutoPostBack="True" 
                                oncheckedchanged="chkFecha_CheckedChanged" style="font-size: x-small" 
                                Text="Generar a Fecha Actual" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left" colspan="3">
                            <asp:CheckBoxList ID="cblRangos" runat="server" RepeatDirection="Horizontal" 
                                CellSpacing="0" ToolTip="Rangos de Clasificación de Cartera" 
                                Width="90%" Height="20px">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" 
                                CssClass="textbox" 
                                onselectedindexchanged="ddlOficina_SelectedIndexChanged" Width="150px">
                            </asp:DropDownList>                            
                        </td>
                        <td class="tdD" style="text-align:left">
                            Asesor<br />
                            <asp:DropDownList ID="ddlAsesores" runat="server" AutoPostBack="True" 
                                CssClass="textbox" Width="250px">
                                <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left" colspan="3">
                            <asp:Label ID="Lblerror" runat="server" 
                                style="color: #FF0000; font-size: x-small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="3">
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                            <br/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr style="width: 100%; text-align: left" />
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td>
                        <div style="overflow: scroll; height:400px; width:900px; margin-right: 0px;">                                  
                            <asp:GridView ID="gvLista" runat="server" 
                                AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                                OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                                RowStyle-CssClass="gridItem" style="font-size: x-small" Width="2000px" >
                                <Columns>                        
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha"  DataFormatString="{0:dd-MM-yyyy}"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_oficina" HeaderText="Cod.Ofi." DataFormatString="{0:N0}" ><ItemStyle HorizontalAlign="Left" Width="30px" /></asp:BoundField>
                                    <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" ><ItemStyle HorizontalAlign="Left"  Width="120px" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_linea" HeaderText="Cod.Linea"  ><ItemStyle HorizontalAlign="Center" Width="70px" /></asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Nombre Linea"  ><ItemStyle HorizontalAlign="Left" Width="120px" /></asp:BoundField>
                                    <asp:BoundField DataField="numero_radicacion" HeaderText="No.Radic." DataFormatString="{0:N0}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_deudor" HeaderText="Cod.Deudor" DataFormatString="{0:N0}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identific." ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="nombres" HeaderText="Nombres" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F.Prox.Pago" DataFormatString="{0:dd-MM-yyyy}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_asesor" HeaderText="Cod.Ase" DataFormatString="{0:N0}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                    <asp:BoundField DataField="nom_asesor" HeaderText="Asesor" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="monto_aprobado" HeaderText="Monto" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="dias_mora" HeaderText="Dias Mora" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_1" HeaderText="Valor 1" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_2" HeaderText="Valor 2" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_3" HeaderText="Valor 3" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_4" HeaderText="Valor 4" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_5" HeaderText="Valor 5" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_6" HeaderText="Valor 6" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_7" HeaderText="Valor 7" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="clasificacion_mora_8" HeaderText="Valor 8" DataFormatString="{0:c}" Visible="false"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>                  
        <asp:View ID="vwReporte" runat="server">         
            <br /><br /><br />
            <rsweb:ReportViewer ID="rvEdades" runat="server" Width="905px" Height="777px"><localreport reportpath="Page\Cartera\ReporteEdades\RepEdades.rdlc"></localreport></rsweb:ReportViewer>         
        </asp:View>
    </asp:MultiView> 
   
</asp:Content>