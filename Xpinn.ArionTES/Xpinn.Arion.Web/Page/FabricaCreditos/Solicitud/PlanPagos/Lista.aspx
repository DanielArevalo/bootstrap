<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="pConsulta" runat="server">
            <br />
            <br />
        </asp:Panel>

        <table style="width:100%;">
            <tr>
                <td style="width: 309px; color: #00CC99;" colspan="2">
                    <strong><em>El proceso de solicitud finalizó correctamente. A continuaciòn puede consultar 
                    el plan de pagos del Crédito y/o puede ver el informe de la solicitud presionando el botòn de visualizar informe.
                </em></strong>
                </td>
            </tr>
            <tr>
                <td style="width: 309px" colspan="2">
                    <strong __designer:mapid="c02">
                    <asp:DropDownList 
                        ID="ddlProceso" runat="server" CssClass="dropdown" Height="32px" 
                         Width="290px" Enabled="False" Visible="False">
                    </asp:DropDownList>
                    </strong></td>
            </tr>
            <tr>
                <td style="width: 309px">
                    <strong>Nùmero de Solicitud</strong>
                        <asp:RequiredFieldValidator ID="rfvNumeroSolicitud" runat="server" 
                            ControlToValidate="txtNumeroSolicitud" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgConsultar" />
                        <br />
                    <asp:TextBox ID="txtNumeroSolicitud" runat="server" CssClass="textbox" 
                        Width="190px" MaxLength="20"></asp:TextBox>
                    <br />
                            <asp:CompareValidator ID="cvNumeroSolicitud" runat="server" 
                                ControlToValidate="txtNumeroSolicitud" Display="Dynamic" 
                                ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                    <br />
                </td>
                <td style="width: 309px">
                    <strong>Numero de Crédito</strong><br />
                    <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="190px" 
                        Enabled="False"></asp:TextBox>
                    <br />
                            <asp:CompareValidator ID="cvNoCredito" runat="server" 
                                ControlToValidate="txtCredito" Display="Dynamic" 
                                ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                </td>
            </tr>
            <tr>
                <td style="width: 400px" colspan="2" >
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" 
                        Width="190px" MaxLength="20" Visible="False"></asp:TextBox>
                    <br />
                 </td>
            </tr>
            <tr>
                <td colspan="2"><hr width="100%" noshade></td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="2">
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="Horizontal" 
                        ShowHeaderWhenEmpty="True" Width="100%" 
                            onpageindexchanging="gvLista_PageIndexChanging" 
                            onselectedindexchanged="gvLista_SelectedIndexChanged" 
                            style="text-align: left" >
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Numero_radicacion" HeaderText="No.Crédito" />
                            <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="Linea" HeaderText="Línea" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="Plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center"/>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <center>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </center>                
                </td>     
            </tr>
        </table>
    <asp:MultiView ID="mvLista" runat="server">  
        <asp:View ID="vGridPlan" runat="server">
          <asp:Button ID="btnInforme0" runat="server" CssClass="btn8" Visible="false"
                onclick="btnInforme0_Click" Text="Visualizar informe" 
                onclientclick="btnInforme0_Click" /> &nbsp;

        </asp:View>
        <asp:View ID="vReportePlan" runat="server">
       
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="963px">

                <localreport reportpath="Page\FabricaCreditos\Solicitud\PlanPagos\ReportSolicitud.rdlc">

                </localreport>

            </rsweb:ReportViewer>
       
            </asp:View>
    </asp:MultiView> 
</asp:Content>