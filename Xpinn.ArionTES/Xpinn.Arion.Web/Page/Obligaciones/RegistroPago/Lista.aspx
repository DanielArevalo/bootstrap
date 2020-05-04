<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master"  AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>    
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=gvObCredito.ClientID%>').gridviewScroll({
                width: 980,
                height: 400,
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
    <table cellpadding="5" cellspacing="0" style="width: 100%">
         <tr>
            <td  class="tdI">
              <asp:Panel ID="pConsulta" runat="server">
                <table border="0" width="66%">
                <tr align="left">
                <td align="left">
                        Entidad<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" class="dropdown" Height="31px" 
                            Width="204px">
                        </asp:DropDownList>
                    </td>
                    <td> Número de Obligación<br/>
                            <asp:TextBox ID="txtNumeObl"  maxlength="20"  Width="140px" CssClass="textbox" runat="server"></asp:TextBox>
                    </td>
                    <td align="center">
                        Fecha Próximo Pago <br/> 
                         <asp:TextBox ID="txtFechaProxPago" width="90px" maxlength="10" cssClass="textbox" 
                            runat="server"></asp:TextBox>
                         <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                          <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                PopupButtonID="Image1" 
                                TargetControlID="txtFechaProxPago"
                                Format="dd/MM/yyyy" >
                            </asp:CalendarExtender>
                        <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server" ValidationGroup="valida" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaProxPago" ForeColor="Red" ErrorMessage="Formato Invalida"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td>
            <div id="gvDiv">
                <asp:GridView ID="gvObCredito" runat="server"
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="gvObCredito_SelectedIndexChanged" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="codobligacion"  HeaderText="No Obligación" />
                         <asp:BoundField DataField="numeropagare"  HeaderText="Pagaré" />
                         <asp:BoundField DataField="nomentidad"  HeaderText="Entidad" />
                        <asp:BoundField DataField="fechaproximopago" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="Fecha Próximo Pago" />
                        <asp:BoundField DataField="montoaprobado" HeaderText="Monto Aprobado" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldocapital" HeaderText="Saldo" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                           <asp:BoundField DataField="cuota" HeaderText="Cuota" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
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
        <tr>
            <td style="text-align:center">
                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>


