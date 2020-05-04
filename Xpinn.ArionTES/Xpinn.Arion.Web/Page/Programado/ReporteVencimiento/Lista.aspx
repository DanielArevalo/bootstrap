<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales"TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align: left; width: 150px;">Cuenta
                                        </td>
                                        <td style="text-align: left; width: 150px;">Fecha Inicial: </td>
                                        <td style="text-align: left; width: 150px">Fecha Final:
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 220px">Oficina:
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 220px">Estado</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left;">
                                            <ucFecha:fecha ID="txt_fechainicial" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="text-align: left;">
                                            <ucFecha:fecha ID="txt_fechafinal" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                                                Width="95%" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox"
                                                Width="95%" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">&nbsp;</td>
                                        <td style="text-align: left;">&nbsp;</td>
                                        <td style="text-align: left;">&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                           
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr width="100%" />
            <asp:Panel ID="pDatos" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                    <tr>
                        <td style="text-align: left"><strong>Lista de Ahorros Programados próximos a vencer</strong></td>
                    </tr>
                    <tr>
                        <td>
                            <div style="max-width: 100%; max-height: 700px; overflow: scroll">
                                <br />
                                <asp:GridView ID="gvLista" Width="100%" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" DataKeyNames="numero_programado" OnRowEditing="gvLista_RowEditing"   OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowDeleting="gvLista_RowDeleting"
                                    PagerStyle-CssClass="gridPager" Style="font-size: xx-small"
                                    ShowHeaderWhenEmpty="True">
                                    <Columns>                                     
                                       <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Prorrogar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/calendario.jpg"
                                    ToolTip="Prorrogar" Width="16px" />
                            </ItemTemplate>
                                           <HeaderStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Cerrar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Cerrar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco" />
                        </asp:TemplateField>

                           <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Renovar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnRenovar" runat="server" CommandName="Delete" ImageUrl="~/Images/iconUtilitarios.jpg"
                                    ToolTip="Renovar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco" />
                        </asp:TemplateField>


                                            <asp:BoundField DataField="numero_programado" HeaderText="Numero Programado" />
                                        <asp:BoundField DataField="nomlinea" HeaderText="Linea" />
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombres"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                        <asp:BoundField DataField="nomoficina" HeaderText="Modalidad" />
                                        <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="nom_estado" HeaderText="Estado" />
                                        <asp:BoundField DataField="valor_cuota" HeaderText="Vr Cuota" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                        <asp:BoundField DataField="plazo" HeaderText="Plazo" DataFormatString="{0:N0}"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>

                            </div>
                        </td>
                    </tr>
                </table>
                <table style="width:100%">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="lblTotReg" runat="server" />
                            &#160;&#160;&#160;<b><asp:Label ID="lblTotalCDAT" runat="server" Text=" Total de Saldos" /></b>
                            <uc2:decimales ID="txtTotalSaldo" runat="server" style="text-align: right;" Enabled="false" />
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnDatos_Click"
                        Text="Visualizar Datos" Height="30px"/>
                    <br />
                    <br />
                    <hr width="100%" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                        Width="100%"><LocalReport ReportPath="Page\Programado\ReporteVencimiento\rptVencimiento.rdlc"><DataSources><rsweb:ReportDataSource /></DataSources></LocalReport></rsweb:ReportViewer>
                </td>
            </tr>
        </table>
            </asp:View>
        <asp:View ID="View2" runat="server">
            <br /><br />
            <table width="100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8"  Height="25px" Width="130px"
                             Text="Visualizar Datos" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
                            Text="Imprimir"  />
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
                                  
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>

