<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br/><br/>
    <asp:Panel ID="panelGeneral" runat="server">
        <div id="DivButtonsP">
            <asp:ImageButton runat="server" ID="btnCancelar" 
                    ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelar_Click" 
                    ImageAlign="Right"/>
            <asp:ImageButton runat="server" ID="btnGuardar" ImageUrl="~/Images/btnGuardar.jpg" 
                    ValidationGroup="vgGuardar" OnClick="btnGuardar_Click" ImageAlign="Right"  />
            <asp:ImageButton runat="server" ID="btnRegresar" 
                    ImageUrl="~/Images/btnRegresar.jpg" OnClick="btnRegresar_Click" Visible="false" 
                    ImageAlign="Right" />     
        </div>
        <div id="gvDiv"> 
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View2" runat="server">
                    <table cellpadding="5" cellspacing="0" style="width: 80%">
                        <tr>
                            <td class="tdI" style="text-align:left; width: 130px;">                      
                                Fecha de Cierre <br/>
                                <asp:TextBox ID="txtFechaCierreCaja" Enabled="false" runat="server" CssClass="textbox" MaxLength="10" Width="104px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left; width: 271px;">
                                Horario<br/>
                                <asp:TextBox ID="txtHorario" Enabled="false" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left" colspan="2">
                             Cajero Principal a Quien se le Entregará el Cuadre<br/>
                                <asp:TextBox ID="txtCajeroPricipal" enabled="false" runat="server" 
                                    CssClass="textbox" Width="528px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align:left" colspan="2">
                                Oficina <br/>
                                <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" 
                                    Width="269px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left">
                                Caja <br/>
                                <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox" 
                                    Width="177px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left; width: 810px;">
                                Cajero <br/>
                                <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox" 
                                    Width="341px" ></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align:left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="5" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="3">
                                <div id="DivButtons">
                                    <asp:Button ID="btnGenerarArqueo" runat="server" Text="Generar Arqueo" 
                                        onclick="btnGenerarArqueo_Click" />&#160;
                                    <asp:Button ID="btnImprimirArqueo" runat="server" Text="Imprimir Arqueo" 
                                        onclick="btnImprimirArqueo_Click" Visible="False"/>&#160;
                                    <asp:Button ID="btnVerMovimientos" runat="server" Text="Ver Movimientos" 
                                        onclick="btnVerMovimientos_Click" />&#160;
                                </div>
                            </td>
                        </tr>
                        <tr> 
                            <td  style="text-align: center; color: #FFFFFF; background-color: #0066FF" 
                                colspan="2">
                                Saldos
                            </td>
                            <td  style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                                Cheques Para Entregar
                            </td>
                        </tr>
                        <tr> 
                            <td valign="top" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>          
                                 <asp:GridView ID="gvSaldos" runat="server" BackColor="White" 
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    ForeColor="Black" AutoGenerateColumns="False" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="White" />
                                    <columns>
                                      <asp:BoundField DataField="orden" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                      <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />                                    
                                      <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                                       <asp:BoundField DataField="nom_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                       <asp:BoundField DataField="concepto" HeaderText="concepto">
                                        <ItemStyle HorizontalAlign="Left" />                   
                                       </asp:BoundField>
                                      <asp:BoundField DataField="efectivo" HeaderText="Efectivo" DataFormatString="{0:N0}">
                                       <ItemStyle HorizontalAlign="Right" />
                                      </asp:BoundField>
                                        <asp:BoundField DataField="cheque" HeaderText="Cheque" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                       </asp:BoundField>
                                       <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                       </asp:BoundField>                    
                                    </columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                                  </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:GridView ID="gvChquesXEntregar" runat="server" BackColor="White" 
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    ForeColor="Black" AutoGenerateColumns="false" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="White" />
                                    <columns>
                                      <asp:BoundField DataField="cod_movimiento" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                      <asp:BoundField DataField="num_documento" HeaderText="Num Cheque" />
                                      <asp:BoundField DataField="nom_banco" HeaderText="Entidad Bancaria" /> 
                                      <asp:BoundField DataField="titular" HeaderText="Títular" />
                                      <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}" />
                                    </columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                 </asp:GridView>
                                 <br />
                                 <br />
                                 <br />               
                            </td>      
                        </tr>           
                    </table> 
                </asp:View>           
            </asp:MultiView>
            <asp:MultiView ID="mvReporte" runat="server">
                <asp:View ID="vReporte" runat="server">   
                    <asp:TextBox ID="Txttotal" runat="server" Visible="False" DataFormatString="{0:n0}"></asp:TextBox>
                    <asp:TextBox ID="Txtconcepto" runat="server" Visible="False"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="Txtefecitivo" runat="server" Visible="False" DataFormatString="{0:n0}"></asp:TextBox>
                    <asp:TextBox ID="Txtcheque" runat="server" Visible="False" DataFormatString="{0:n0}"></asp:TextBox>
                    <asp:TextBox ID="Txtmoneda" runat="server" Visible="False"   ></asp:TextBox>
                    <asp:Label ID="LBLESTADO" runat="server" Visible="False" Text=""></asp:Label>
                    <br />
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" Height="435px" InteractiveDeviceInfos="(Colección)" 
                        Visible="false" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
                        Width="100%">
                        <localreport reportpath="Page\CajaFin\CierreCaja\timbrearqueo.rdlc">
                        </localreport>
                    </rsweb:ReportViewer>
                </asp:View>
            </asp:MultiView>       
        </div>        
    </asp:Panel>

    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
</asp:Content>