﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
        <asp:Panel ID="pConsulta" runat="server">
            <table style="width:100%;">
                <tr>
                    <td class="logo" style="width: 150px">
                        No. Crédito:<br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="cvNoCredito" runat="server" 
                            ControlToValidate="txtCredito" Display="Dynamic" 
                            ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                    </td>
                    <td class="logo" style="width: 200px">
                        Cod.Deudor:<br />
                        <asp:TextBox ID="txtCodDeudor" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="cvDeudor" runat="server" 
                            ControlToValidate="txtCodDeudor" Display="Dynamic" 
                            ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                    </td>
                    <td class="logo" style="width: 200px">
                        No. Identificación:<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                            Width="190px"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="cvIdentificacion" runat="server" 
                            ControlToValidate="txtIdentificacion" Display="Dynamic" 
                            ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                            SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                    </td>
                    <td class="logo" style="width: 200px">
                        Código de nómina<br />  
                        <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="140px"/>
                        <br />
                    </td>
                    <td style="text-align: left; width: 160px">
                        <asp:Label ID="lbloficina" runat="server" Text="Oficina" />
                        s<br />
                        <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox" 
                            Width="90%" />
                    </td>
                    <td style="text-align: left; width: 160px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <table style="width:100%;">
             <tr>
                <td>
                    <hr style="width: 100%" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" GridLines="Horizontal" 
                        PageSize="20" 
                        ShowHeaderWhenEmpty="True" Width="97%" 
                        onpageindexchanging="gvLista_PageIndexChanging" 
                        onselectedindexchanged="gvLista_SelectedIndexChanged" 
                        style="font-size: small">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="No. Crédito" />
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />  
                             <asp:BoundField DataField="cod_deudor" HeaderText="Cod. Cliente" />
                           <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fecha Próximo Pago" /> 
                              <asp:BoundField DataField="valor_a_pagar" HeaderText="Vr. Total a Pagar"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />                      
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
</asp:Content>