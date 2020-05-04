<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Verificación Referencias :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="70%">
                   <tr>
                       <td class="tdI">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                    <tr>
                        <td class="tdI" style="text-align:left" Width="200px">
                            Número radicación<asp:CompareValidator ID="cvNumero_radicacion" runat="server" 
                                ControlToValidate="txtnumero_radicacion" Display="Dynamic" 
                                ErrorMessage=" Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                            <br/>
                            <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                MaxLength="128" Width="150px" />
                        </td>                        
                        <td class="tdD" style="text-align:left" Width="350px">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="dropdown" 
                                Width="150px">
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>                   
                    <tr>
                       <td class="tdD" style="text-align:left">
                           Identificación&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtIdentificacion" Display="Dynamic" 
                                ErrorMessage=" Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                           <br/>
                           <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                Width="150px" />                           
                       </td>
                       <td class="tdD" style="text-align:left" Width="350px">
                           Nombre<br/>
                           <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" 
                               MaxLength="128" Width="300px" />
                        </td>
                        <td style="text-align: left;">
                            Código de nómina<br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                        </td>
                   </tr>
                   <tr>
                       <td class="tdI" style="text-align:left"">
                           Línea de crédito&nbsp;<br/>
                           <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                               MaxLength="128" Width="150px" />
                       </td>
                       <td class="tdI" style="text-align:left"">
                           Monto solicitado &nbsp;<br/>
                           <asp:TextBox ID="Txtmonto_solicitado" runat="server" CssClass="textbox" 
                               MaxLength="128" Width="150px" />
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
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
                <asp:gridview id="gvLista" runat="server" width="100%" gridlines="Horizontal" autogeneratecolumns="False" onrowdatabound="gvLista_RowDataBound" onrowdeleting="gvLista_RowDeleting" allowpaging="True" onpageindexchanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" pagesize="20" headerstyle-cssclass="gridHeader" pagerstyle-cssclass="gridPager" rowstyle-cssclass="gridItem" datakeynames="cod_referencia">
                    <Columns>                 
                        <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" > <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>                 
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre completo" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago"  >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="linea_credito" HeaderText="Línea"  >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>                 
                    </Columns>
                </asp:gridview>
                <asp:label id="lblTotalRegs" runat="server" visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
