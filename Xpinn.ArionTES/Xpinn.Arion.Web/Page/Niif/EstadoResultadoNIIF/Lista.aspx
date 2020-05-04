<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        } 
       
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvEstadoResultados" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" style="width: 68%">
                    <tr >            
                        <td class="tdI" style="text-align:left" colspan="2">
                            Centro De Costo</td>
                        <td class="tdD" colspan="3" style="text-align:left">
                            Nivel</td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left" colspan="2">
                            <asp:DropDownList ID="ddlcentrocosto" Height="30px" runat="server" CssClass="dropdown" 
                                Width="168px" />
                        </td>
                        <td class="tdD" style="text-align:left" colspan="3">
                            <asp:DropDownList ID="ddlNivel" Height="30px" runat="server" CssClass="dropdown" 
                                Width="113px">
                                
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:DropDownList ID="ddlNivel0" runat="server" CssClass="dropdown" vISIBLE="FALSE" Height="30px" Width="113px">
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdD" style="text-align:left" colspan="2">
                            Comparar Fecha<br />
                            <asp:DropDownList ID="ddlFecha1" Height="30px" runat="server" CssClass="dropdown" 
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" colspan="2" style="text-align:left; width: 160px;">
                            Con Fecha<br />
                            <asp:DropDownList ID="ddlFecha2" Height="30px" runat="server" CssClass="dropdown" 
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                        <td class="tdD" style="text-align:left">
                            Moneda<br />
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left">                                    
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Height="23px" Width="130px"
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<br/>
                        </td>
                        <td style="text-align:left" colspan="2">
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn8" Height="23px" Width="130px"
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                        </td>
                        <td class="tdI" colspan="2">
                            <asp:Label ID="Lblerror" runat="server" style="color: #FF0000"></asp:Label>
                        </td>
                        <td class="tdI">
                            <asp:CheckBox ID="ChkOcultarCuenta" visible="false" runat="server" Text="Ocultar Cuenta Imprimir" AutoPostBack="True" OnCheckedChanged="ChkOcultarCuenta_CheckedChanged" />
                        </td>
                        <td class="tdI">
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
                            AutoGenerateColumns="False" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="950px"                             
                            style="font-size: xx-small; margin-top: 0px;" OnRowDataBound="gvLista_RowDataBound" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nom. Cuenta" >
                                <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="balance1" HeaderText="Balance 1" 
                                    DataFormatString="{0:c}" >
                                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    </asp:BoundField>                                    
                                <asp:BoundField DataField="balance2" HeaderText="Balance2" 
                                    DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                    </asp:BoundField>
                                <asp:BoundField DataField="diferencia" DataFormatString="{0:c}" 
                                    HeaderText="Variacion Absoluta">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="porcdif" DataFormatString="{0:P1}" 
                                HeaderText="Variacion Relativa">
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" 
                            style="font-size: xx-small"/>
                        <span style="font-size: xx-small">
                        <asp:Label ID="lblInfo" runat="server" 
                            Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                        &nbsp; </span>
                        </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" OnClick="btnDatos_Click"
                            Height="23px" Width="130px" Text="Visualizar Datos" />
                        &#160;&#160;&#160;&#160;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="23px" Width="130px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="550px" runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="500px">
                            <LocalReport ReportPath="Page\NIIF\EstadoResultadoNIIF\RptEstadoResultadoNIIF.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>           

            &nbsp;
            
            
        </asp:View>
    </asp:MultiView> 
   
</asp:Content>