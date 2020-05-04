<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                freezesize: 2,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvBalance" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
                    <tr >            
                        <td class="tdI" style="text-align:left">
                            Fecha de corte</td>
                        <td class="tdD" colspan="2" style="text-align:left" >
                            <asp:Label ID="lblCentroCosto" runat="server" Text="Centro Costo"></asp:Label></td>
                        <td class="tdD" colspan="3" style="text-align:left">
                            Nivel&nbsp;</td>
                        <td class="tdD" colspan="2" style="text-align:left">
                            <asp:Label ID="lblMoneda" runat="server" Text="Moneda"></asp:Label>
                        </td>
                        <td class="tdD" style="text-align:left">
                            Consecutivo
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                                Width="158px" AutoPostBack="True" 
                                onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left" colspan="2">
                            <asp:DropDownList ID="ddlcentrocosto" runat="server" CssClass="dropdown" 
                                Width="168px" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                                Width="113px">
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" colspan="4" style="text-align:left">
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" 
                                Width="151px" />
                        </td>
                        <td class="tdD" colspan="4" style="text-align:left">
                            <asp:TextBox ID="txtConsecutivo" runat="server" Width="80px" CssClass="textbox" />
                            <asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtConsecutivo"
                                ValidChars="0123456789" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pOtrConsulta" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 60%">
                    <tr>
                        <td class="tdI" style="text-align:left">
                            <asp:CheckBox ID="chkCuentasCero" runat="server" 
                                Text="Mostrar Cuentas En Cero" 
                                style="font-size: x-small" />
                        </td>
                        <td class="tdD">
                            <asp:CheckBox ID="chkTerceros" runat="server" 
                                Text="Generar por Terceros" 
                                style="font-size: x-small" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:CheckBox ID="chkNivel" runat="server" 
                                Text="Solo Nivel Seleccionado"
                                style="font-size: x-small" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkExcedentes" runat="server" 
                                Text="Excedentes"
                                style="font-size: x-small" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:CheckBox ID="chkmes13" runat="server" style="font-size: x-small" Text="Mes 13" Visible="False" OnCheckedChanged="chkmes13_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td class="tdD" style="text-align:left">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD">
                            &nbsp;</td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel1" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="tdI" colspan="4">                                    
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                            <br/>
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td><hr style="width: 100%; text-align: left" /></td>
                </tr>
                <tr>
                    <td>                          
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="910px" style="font-size: x-small" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_cuenta" HeaderText="Nombre Cuenta" >
                                <ItemStyle HorizontalAlign="Left" Width="400px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="saldo_inicial_debito" HeaderText="Saldo Inicial Débito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_inicial_credito" HeaderText="Saldo Inicial Crédito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="debito" HeaderText="Débito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="credito" HeaderText="Crédito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_final_debito" HeaderText="Saldo Final Débito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_final_credito" HeaderText="Saldo Final Crédito" DataFormatString="{0:c2}" >
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        &nbsp;
                     </td>
                </tr>
            </table>
        </asp:View>
                  
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="25px" Width="130px" OnClick="btnDatos_Click"
                            Text="Visualizar Datos" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="600px" runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="600px">
                            <LocalReport ReportPath="Page\Contabilidad\LibroMayor\RptLibroMayor.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView> 
   
</asp:Content>