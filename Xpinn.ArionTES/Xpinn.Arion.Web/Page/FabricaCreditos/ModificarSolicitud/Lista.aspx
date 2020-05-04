<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                       <tr>
                           <td class="tdI">
                               <asp:Panel ID="Panel1" runat="server">
                                   <table style="width:100%;">
                                       <tr>
                                           <td class="logo" style="text-align:left" colspan="2">
                                               <asp:CompareValidator ID="cvnumero_radicacion1" runat="server" 
                                                   ControlToValidate="txtnumero_radicacion" Display="Dynamic" 
                                                   ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                                   SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                           </td>
                                           <td style="text-align:left">
                                               &nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td class="logo" style="width: 195px; text-align:left">
                                               Número Solicitud</td>
                                           <td style="width: 138px; text-align:left">
                                               Número Radicación</td>
                                           <td style="width: 138px; text-align:left">
                                               Líneas de Crédito</td>
                                       </tr>
                                       <tr>
                                           <td class="logo" style="width: 195px; text-align:left">
                                               <asp:TextBox ID="txtNumero_Solicitud" runat="server" CssClass="textbox" 
                                                   MaxLength="128" />
                                           </td>
                                           <td class="logo" style="width: 195px; text-align:left">
                                               <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                                   MaxLength="128" />
                                           </td>
                                           <td style="text-align:left">
                                              <asp:DropDownList ID="ddllineacredito" runat="server" CssClass="textbox" Width="191px">
                                               </asp:DropDownList>
                                       </tr>
                                       <tr>
                                           <td class="logo" style="width: 195px; text-align:left">
                                               Identificación</td>
                                            <td class="logo" style="width: 195px; text-align:left">
                                               Nombres Y Apellidos</td>
                                           <td style="width: 138px; text-align:left">
                                               Código de nómina</td>
                                            <td class="logo" style="width: 195px; text-align:left">
                                               Oficina</td>
                                       </tr>
                                       <tr>
                                           </td>
                                               <td class="logo" style="width: 195px; text-align:left">
                                               <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                                   MaxLength="128" />
                                           </td>
                                           <td class="logo" style="width: 195px; text-align:left">
                                               <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" 
                                                   MaxLength="128" Width="187px" />
                                           </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                                            </td>
                                           <td style="text-align:left">
                                               <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" 
                                                   Width="137px">
                                               </asp:DropDownList>
                                           </td>
                                       </tr>
                                   </table>
                               </asp:panel>
            </td>
        </tr>
    </table>
    </asp:panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade />
            </td>
        </tr>
    <tr>
        <td>
            <asp:gridview id="gvLista" runat="server" width="100%" gridlines="Horizontal" autogeneratecolumns="False" allowpaging="True" onpageindexchanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" pagesize="20" headerstyle-cssclass="gridHeader" pagerstyle-cssclass="gridPager" rowstyle-cssclass="gridItem" datakeynames="numero_radicacion">
                    <Columns>
                        <asp:BoundField DataField="numero_solicitud" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="numero_solicitud" HeaderText="Numero Solicitud" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                          <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Credito" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea_credito" HeaderText="Línea" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="fecha_solicitud" HeaderText="F.Solicitud" DataFormatString="{0:d}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}"  >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuotas_pagass" HeaderText="cuota" >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:gridview>
            <asp:label id="lblTotalRegs" runat="server" visible="False" />
        </td>
    </tr>
    </table>
</asp:Content>
