<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnConsultar" ImageUrl="~/Images/btnConsultar.jpg" OnClick="btnConsultar_Click" ImageAlign="Right" />    
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table width="100%">
            <tr>
                <td colspan="5" style="font-size: x-small">
                    &nbsp; 
                </td>
            </tr>
            <tr>
                <td style="font-size: x-small; text-align:left" colspan="5">
                    <strong>Críterios de Búsqueda:</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align:left">No.Comprobante<br />
                    <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                </td>
                <td style="text-align:left">Tipo de Comprobante<br />
                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="95%">
                        <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:left">Fecha<br />
                    <uc1:fecha ID="txtFechaIni" runat="server" />
                    a
                    <uc1:fecha ID="txtFechaFin" runat="server" />
                </td>
                <td style="text-align:left">No.Sop<br />
                    <asp:TextBox ID="txtNumSop" runat="server" CssClass="textbox" Width="50px"></asp:TextBox>
                </td>
                <td style="text-align:left">Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="True" CssClass="textbox" Width="120px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="E">Elaborado</asp:ListItem>
                        <asp:ListItem Value="A">Aprobado</asp:ListItem>
                        <asp:ListItem Value="N">aNulado</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="5">
                    <table width="100%">
                        <tr>
                            <td style="text-align: left;width:25%">
                                Concepto<br />
                                <asp:DropDownList ID="ddlConcepto" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="95%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left;width:25%">
                                Ciudad<br />
                                <asp:DropDownList ID="ddlCiudad" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="95%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left;width:25%">
                                Ordenar por<br />
                                <asp:DropDownList ID="ddlOrden1" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="70%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkDesc" runat="server" Text="Desc" TextAlign="Left" style="font-size:x-small"/>
                            </td>
                            <td style="text-align: left;width:25%">
                                Luego por<br />
                                <asp:DropDownList ID="ddlOrdenLuego" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                    Width="70%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkDescLuego" runat="server" Text="Desc" TextAlign="Left" style="font-size:x-small"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align:left">
                    Identificación<br />
                    <asp:TextBox ID="ddlIdentificacion" runat="server" AppendDataBoundItems="True" 
                        CssClass="textbox" Width="95px"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." 
                        Height="26px" onclick="btnConsultaPersonas_Click" />
                    <uc1:ListadoPersonas id="ctlBusquedaPersonas" runat="server" />
                </td>
                <td style="text-align:left">
                    Nombres<br />
                    <asp:TextBox ID="ddlNombres" runat="server" AppendDataBoundItems="True" 
                        CssClass="textbox" Width="90%"></asp:TextBox>
                </td>
                <td style="text-align:left">
                    Apellidos/Razón Social<br />
                    <asp:TextBox ID="ddlApellidos" runat="server" AppendDataBoundItems="True" 
                        CssClass="textbox" Width="90%"></asp:TextBox>
                </td>
                <td style="text-align:left" colspan="2">
                    Valor Total<br />
                    <uc1:decimales ID="txtValorTotal" runat="server" Habilitado="True" Width_="80" />
                </td>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td style="font-size: x-small">
                    Cod.Cuenta<br />
                    <asp:DropDownList ID="ddlCodCuenta" runat="server" CssClass="textbox" Width="100px" AppendDataBoundItems="True">                                                    
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="font-size: x-small">
                    Detalle<br />
                    <asp:TextBox ID="txtDetalle" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>                        
                </td>
                <td style="font-size: x-small">
                    Moneda<br />
                    <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="textbox" Width="80px"                             
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="font-size: x-small">
                    Valor<br />
                    <uc1:decimales ID="txtValor" runat="server" Width_="80" />
                </td>
                <td style="font-size: x-small">
                    Centro de Costo<br />
                    <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" Width="120px"                             
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
                <td style="font-size: x-small">
                    Centro Gestion<br />
                    <asp:DropDownList ID="ddlCentroGestion" runat="server" CssClass="textbox" Width="120px"                             
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
            </tr>            
        </table>
    </asp:Panel>
    <hr />
    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprimir2" runat="server" CssClass="btn8" Height="20px" onclick="btnImprimir2_Click" Text="Imprimir Comp. Individuales" Width="169px" />
                        <br />
                        <br />
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            onrowediting="gvLista_RowEditing" 
                            onpageindexchanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="soporte" HeaderText="Num.Sop." />
                                <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                                <asp:BoundField DataField="razon_social" HeaderText="Razón Social" />
                                <asp:BoundField DataField="elaboro" HeaderText="Elaborado por" />
                                <asp:BoundField DataField="aprobo" HeaderText="Aprobado por" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="totalcom" DataFormatString="{0:N0}" HeaderText="Valor"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="desembolso" DataFormatString="{0:N0}" HeaderText="Girado"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
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
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="Reporte" runat="server"  Width="100%">
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td style="text-align: left">
                    <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" OnClick="btnRegresar_Click"
                        Text="Regresar a la Consulta" Height="25px" />
                    &#160;&#160;
                    <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                        Text="Imprimir" OnClick="btnImprime_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                        height="500px" runat="server" style="border-style: dotted; float: left;"></iframe>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="RpviewComprobante" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Page\Contabilidad\Comprobante\ReportListado.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <br />
                    <rsweb:ReportViewer ID="RpviewComprobanteInd" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Page\Contabilidad\Comprobante\ReportListadoInd.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
