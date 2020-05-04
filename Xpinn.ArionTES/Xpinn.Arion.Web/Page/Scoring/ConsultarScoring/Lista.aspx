<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <asp:MultiView ID="mvScoringCreditos" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 309px">
                        No. Crédito
                        <asp:CompareValidator ID="cvNoCredito" runat="server" ControlToValidate="txtCredito"
                            Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                    </td>
                    <td style="width: 309px">
                        Cliente<br />
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                        <br />
                    </td>
                    <td style="width: 309px">
                        Línes de Crédito<br />
                        <asp:DropDownList ID="ddlTipoCredito" runat="server" CssClass="dropdown">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="width: 309px">
                        Estado<br />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">Solicitado</asp:ListItem>
                            <asp:ListItem Value="V">Verificado</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr width="100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" Style="text-align: left">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Numero_radicacion" HeaderText="No.Crédito" />
                                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                <asp:BoundField DataField="Linea" HeaderText="Línea" />
                                <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" />
                                <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="4" style="font-weight: 700">
                        Datos del Cliente</td>
                </tr>
                <tr>
                    <td>
                        Tipo Identificación<br />
                        <asp:Label ID="lblTipoIdentificacion" runat="server"></asp:Label>
                    </td>
                    <td>
                        Número Identificación<br />
                        <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>
                    </td>
                    <td>
                        Nombre del Cliente<br />
                        <asp:Label ID="lblNombre" runat="server"></asp:Label>
                    </td>
                    <td>
                        Dirección de Residencia<br />
                        <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Teléfono de Residencia<br />
                        <asp:Label ID="lblTelefono" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="font-weight: 700">
                        Datos del Crédito</td>
                </tr>
                <tr>
                    <td>
                        Número de Crédito<br />
                        <asp:Label ID="lblNumero" runat="server"></asp:Label>
                    </td>
                    <td>
                        Línea de Crédito<br />
                        <asp:Label ID="lblLinea" runat="server"></asp:Label>
                    </td>
                    <td>
                        Oficina<br />
                        <asp:Label ID="lblOficina" runat="server"></asp:Label>
                    </td>
                    <td>
                        Estado<br />
                        <asp:Label ID="lblEstado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Monto Solicitado<br />
                        <asp:TextBox ID="txtMonto" runat="server" ReadOnly="True" BorderWidth="0px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskMonto" runat="server" TargetControlID="txtMonto"
                            Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                        <br />
                    </td>
                    <td>
                        Ejecutivo<br />
                        <asp:Label ID="lblEjecutivo" runat="server"></asp:Label>
                    </td>
                    <td>
                        Cuota<br />
                        <asp:TextBox ID="txtCuota" runat="server" ReadOnly="True" BorderWidth="0px"></asp:TextBox>
                         <asp:MaskedEditExtender ID="mskCuota" runat="server" TargetControlID="txtCuota"
                            Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                        <br />
                    </td>
                    <td>
                        Plazo - Periodicidad<br />
                        <asp:Label ID="lblPlazo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center" >
                        <strong style="text-align: center">Codeudores</strong></td>
                    <td colspan="2" style="font-weight: 700; text-align: center">
                        RESULTADO SCORING</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvCodeudores" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="Horizontal" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" 
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            ShowHeaderWhenEmpty="True" Style="text-align: left" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="NumeroSolicitud" HeaderText="No.Crédito" />
                                <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" />
                                <asp:BoundField DataField="Nombres" HeaderText="Nombre" />
                                <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs1" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo1" runat="server" 
                            Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                    <td colspan="2" style="font-weight: 700; text-align: center">
                        <br />                        
                        Resultado:&nbsp;<asp:Label ID="lblResultadoScoring" runat="server" Font-Size="Medium" style="color: #FF3300">1</asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        Calificación:&nbsp;<asp:Label ID="lblCalificacionScoring" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        Observaciones:&nbsp;<asp:Label ID="lblObservacion" runat="server" Width="389px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="font-weight: 700">
                        Seguimiento En Scoring</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvSeguimientoScoring" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="Horizontal" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" 
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            ShowHeaderWhenEmpty="True" Style="text-align: left" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />                                
                                <asp:BoundField DataField="usuario" HeaderText="Usuario" 
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="motivo" HeaderText="Motivo" 
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" 
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblTotalRegs0" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo0" runat="server" 
                            Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="View3" runat="server">

            <rsweb:ReportViewer ID="rvScoringCreditos" runat="server" Width="800px" 
                Font-Names="Verdana" Font-Size="8pt" Height="700px" 
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Page\Scoring\ConsultarScoring\Report.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>

        </asp:View>
    </asp:MultiView>
</asp:Content>
