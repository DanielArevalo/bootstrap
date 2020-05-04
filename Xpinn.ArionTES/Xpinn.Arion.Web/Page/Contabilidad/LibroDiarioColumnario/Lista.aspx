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
    <asp:MultiView ID="mvLibroDiario" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="60%">
                           <tr>
                               <td colspan="10" style="text-align:left">                                   
                                    <asp:Label ID="lblTipoNorma" runat="server" Text="" />
                               </td>
                           </tr>
                           <tr >            
                               <td class="tdI" style="text-align:left" height="5">
                                   Fecha de corte</td>
                               <td class="tdD" colspan="2" style="text-align:left" height="5">
                                   <asp:Label ID="lblCentroCosto" runat="server" Text="Centro Costo"></asp:Label></td>
                               <td class="tdD" colspan="2" height="5" style="text-align:left">
                                   Nivel&nbsp;</td>
                               <td class="tdD" height="5">
                                   <asp:Label ID="lblMoneda0" runat="server" Text="Moneda"></asp:Label>
                               </td>
                               <td class="tdD" style="text-align:left">
                                    Consecutivo
                               </td>
                           </tr>
                            <tr>
                                <td class="tdI" style="text-align:left">
                                    <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                                       Width="158px" Height="25px">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdD" style="text-align:left" colspan="2">
                                    <asp:DropDownList ID="ddlcentrocosto" runat="server" CssClass="dropdown" 
                                        Width="168px" Height="25px" />
                                </td>
                                <td class="tdD" style="text-align:left">
                                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                                        Height="25px" Width="113px">
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="tdD" colspan="2" style="text-align:left">
                                    <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                                </td>
                                <td class="tdD" colspan="4" style="text-align:left">
                                    <asp:TextBox ID="txtConsecutivo" runat="server" Width="80px" CssClass="textbox" />
                                    <asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtConsecutivo"
                                        ValidChars="0123456789" />
                                </td>
                            </tr>
                           <tr>
                               <td class="tdI" colspan="2">
                                   <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                       OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                       Text="Visualizar informe" />
                                   &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                       onclick="btnExportar_Click" Text="Exportar a Excel" />
                                   <br/>
                               </td>
                               <td class="tdI" colspan="4">
                                   <asp:Label ID="Lblerror" runat="server" style="color: #FF0000"></asp:Label>
                               </td>
                           </tr>
                        </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td><hr style="width: 90%; text-align: left" /></td>
                </tr>
                <tr>
                    <td>                                                         
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="98%" >
                            <Columns>                        
                                <asp:BoundField DataField="tipocomp" HeaderText="Tipo Comp." >
                                    <ItemStyle HorizontalAlign="Justify"  Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="70px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="debito" HeaderText="Débitos" 
                                    DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right"  Width="120px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="credito" DataFormatString="{0:c}" 
                                    HeaderText="Créditos" >
                                    <ItemStyle HorizontalAlign="Right" Width="120px"/>
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
            <br /><br />
            <hr width="100%" />
            &nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            <rsweb:ReportViewer ID="RptReporte" runat="server" Width="905px" Height="777px">
            <localreport reportpath="Page\Contabilidad\LibroDiarioColumnario\RptLibro.DiarioColum.rdlc"></localreport></rsweb:ReportViewer>         
        </asp:View>
          </asp:MultiView> 
   
</asp:Content>