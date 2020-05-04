<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           <asp:Panel ID="Panel1" runat="server">
                               <table style="width:100%;" cellpadding="0" cellspacing="0" >
                                   <tr>
                                       <td class="logo" style="text-align:left" colspan="2">
                                           <asp:CompareValidator ID="cvnumero_radicacion1" runat="server" 
                                               ControlToValidate="txtnumero_radicacion" Display="Dynamic" 
                                               ErrorMessage="Solo se admiten n�meros" ForeColor="Red" Operator="DataTypeCheck" 
                                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                       </td>
                                       <td style="text-align:left">
                                           &nbsp;</td>
                                   </tr>
                                   <tr>
                                       <td style="width: 197px; text-align:left">
                                           N�mero Radicaci�n</td>
                                       <td style="width: 300px; text-align:left">
                                           L�nea de Cr�dito</td>
                                       <td style="text-align:left">
                                           &nbsp;Oficina</td>
                                   </tr>
                                   <tr>
                                       <td style="width: 197px; text-align:left">
                                           <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                                               MaxLength="128" />
                                       </td>
                                       <td style="width: 250px; text-align:left">
                                           <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                                               Width="280px" />
                                           <br />
                                       </td>
                                       <td style="text-align:left;" >
                                           &nbsp;<asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" 
                                               Width="191px">
                                           </asp:DropDownList>                                            
                                           <br />
                                       </td>
                                   </tr>
                                   
                               </table>
                           </asp:Panel>
                       </td>
                   </tr>
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 197px; text-align:left">
                                            Identificaci�n</td>
                                        <td style="width: 150px; text-align:left" class="logo">
                                            Nombre Completo</td>
                                        <td style="width: 150px; text-alig  n:left" class="logo">
                                            C�digo de n�mina</td>
                                        <td style="text-align:left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 197px; text-align:left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                            <br />
                                        </td>
                                        <td style="width: 200px; text-align:left">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="280px" />
                                            <br />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                                            <br />
                                        </td>
                                        <td style="text-align:left">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left; height: 25px;">
                                            <asp:CompareValidator ID="cvidentificacion" runat="server" 
                                                ControlToValidate="txtidentificacion" Display="Dynamic" 
                                                ErrorMessage="Solo se admiten n�meros" ForeColor="Red" Operator="DataTypeCheck" 
                                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                        </td>
                                        <td style="width: 300px; text-align:left; height: 25px;">
                                            </td>
                                        <td style="text-align:left; height: 25px;">
                                            </td>
                                    </tr>
                                    
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                </asp:panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:gridview id="gvLista" runat="server" width="100%" gridlines="Horizontal" autogeneratecolumns="False" allowpaging="True" onpageindexchanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" pagesize="20" headerstyle-cssclass="gridHeader" pagerstyle-cssclass="gridPager" rowstyle-cssclass="gridItem" datakeynames="numero_radicacion">
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
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
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicaci�n" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificaci�n" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_nomina" HeaderText="C�digo de n�mina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}"  >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_linea_credito" HeaderText="Linea" >
                            <ItemStyle HorizontalAlign="left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="tipo_credito" HeaderText="Tipo Cr�dito" >
                            <ItemStyle HorizontalAlign="left" />
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
