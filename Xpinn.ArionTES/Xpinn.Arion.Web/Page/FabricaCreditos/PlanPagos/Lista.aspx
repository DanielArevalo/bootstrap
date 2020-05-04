<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />        
        <table style="width:100%;">
            <tr>
                <td colspan="2" style="width: 20%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    No. Crédito:<br />
                    <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox"  Width="194px"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="cvNoCredito" runat="server" 
                        ControlToValidate="txtCredito" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                        SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                 </td>
                 <td style="width: 30%">
                   Identificación :<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="190px"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="txtCredito" Display="Dynamic" 
                        ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                        SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                 </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%;">
        <tr>
            <td colspan="2"><hr width="100%" /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" 
                    ShowHeaderWhenEmpty="True" Width="100%" 
                    onpageindexchanging="gvLista_PageIndexChanging" 
                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                    style="text-align: left" >
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                <asp:Label Text='<%#Eval("NumeroSolicitud") %>' Visible="false" ID="lblNumeroSolicitud" runat="server" />
                            </ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Numero_radicacion" HeaderText="No.Crédito" />
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="Linea" HeaderText="Línea" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto Solicitado" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="MontoApr" HeaderText="Monto Aprobado" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="left"/>
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="tipo_linea" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center"/>
                    </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <center>
            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
            </center>
            </td>     
        </tr>
    </table>
</asp:Content>