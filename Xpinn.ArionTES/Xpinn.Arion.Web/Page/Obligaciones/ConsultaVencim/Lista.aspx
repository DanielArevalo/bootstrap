<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master"   CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
         <tr>
            <td  class="tdI">
            <asp:Panel ID="pConsulta" runat="server">
                <table border="0">
                <tr align="left">
                    <td colspan="2"align="center"><b>Rango de Fechas</b></td>
                    <td colspan="3" align="right"><b>Ordenar Por</b></td>
                </tr>
                <tr align="left">
                    <td> 
                    Entidad<br/>
                         <asp:DropDownList ID="ddlEntidad" Width="174px" class="dropdown" runat="server" Height="23px">
                         </asp:DropDownList>
                    </td>
                    <td>
                        Fecha Inicial <br/> 
                         <asp:TextBox ID="txtFechaIni"  maxlength="10" cssClass="textbox" width="70px"
                            runat="server"></asp:TextBox>
                         <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                          <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                PopupButtonID="Image1" 
                                TargetControlID="txtFechaIni"
                                Format="dd/MM/yyyy" >
                            </asp:CalendarExtender>
                        <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaIni" ForeColor="Red" ErrorMessage="Formato Invalida"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFiltro1" Width="174px" class="dropdown" runat="server" 
                            Height="31px">
                            <asp:ListItem Value="CODOBLIGACION">No Obligación</asp:ListItem>
                            <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                            <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>
                            <asp:ListItem Value="FECHAPROXIMOPAGO">Fecha Próximo Pago</asp:ListItem>
                            <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                            <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                            <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>
                            <asp:ListItem Value="VALOR_CUOTA">Cuota</asp:ListItem>
                            <asp:ListItem Value="ESTADOOBLIGACION">Estado</asp:ListItem>
                         </asp:DropDownList>
                    </td>
                </tr>
                  <tr align="left">
                    <td> 
                     &#160;
                    </td>
                    <td>
                        Fecha Final <br/> 
                         <asp:TextBox ID="txtFechaFin"  maxlength="10" cssClass="textbox" width="70px"
                            runat="server"></asp:TextBox>
                         <img id="Image2" alt="Calendario" src="../../../Images/iconCalendario.png" />
                          <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                PopupButtonID="Image2" 
                                TargetControlID="txtFechaFin"
                                Format="dd/MM/yyyy" >
                            </asp:CalendarExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaFin" ForeColor="Red" ErrorMessage="Formato Invalida"></asp:RegularExpressionValidator>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlFiltro2" Width="174px" class="dropdown" runat="server" Height="31px">
                            <asp:ListItem Value="CODOBLIGACION">No Obligación</asp:ListItem>
                            <asp:ListItem Value="NUMEROPAGARE">Pagaré</asp:ListItem>
                            <asp:ListItem Value="CODENTIDAD">Entidad</asp:ListItem>
                            <asp:ListItem Value="FECHAPROXIMOPAGO">Fecha Próximo Pago</asp:ListItem>
                            <asp:ListItem Value="CODPERIODICIDAD">Periodicidad</asp:ListItem>
                            <asp:ListItem Value="MONTOAPROBADO">Monto Aprobado</asp:ListItem>
                            <asp:ListItem Value="SALDOCAPITAL">Saldo a Capital</asp:ListItem>
                            <asp:ListItem Value="VALOR_CUOTA">Cuota</asp:ListItem>
                            <asp:ListItem Value="ESTADOOBLIGACION">Estado</asp:ListItem>
                         </asp:DropDownList>
                    </td>
                </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="DivButtons">
                    <asp:ImageButton ID="btnImprimir" runat="server" ValidationGroup="valida" 
                        ImageUrl="~/Images/btnImprimir.jpg" />&#160;
                </div>
            </td>
        </tr>
        <tr>
            <td>
            <div id="gvDiv">
                <asp:GridView ID="gvObCredito" runat="server"
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                         <asp:BoundField DataField="codobligacion"  HeaderText="No Obligación" />
                         <asp:BoundField DataField="numeropagare"  HeaderText="Pagaré" />
                         <asp:BoundField DataField="nomentidad"  HeaderText="Entidad" />
                        <asp:BoundField DataField="fecha_aprobacion" DataFormatString="{0:d}" HeaderText="Fecha Desembolso" />
                        <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad" />
                          <asp:BoundField DataField="montoaprobado" HeaderText="Monto Aprobado" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:BoundField DataField="saldocapital" HeaderText="Saldo a Capital" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaproximopago" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="Fecha Próximo Pago" />
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
                </div>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        </table>
</asp:Content>



