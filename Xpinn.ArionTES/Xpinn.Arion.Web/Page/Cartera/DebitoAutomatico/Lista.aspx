<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"  EnableEventValidation = "false" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Débito Automático :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
      <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
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
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           <asp:Panel ID="Panel1" runat="server">
                               <table style="width:100%;">
                                   <tr>
                                       <td class="logo" style="text-align:left">
                                         
                                       </td>
                                       <td style="text-align:left">
                                           &nbsp;</td>
                                   </tr>
                                   
                               </table>
                           </asp:Panel>
                       </td>
                   </tr>
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left">
                                            Cod. Persona</td>
                                        <td style="width: 197px; text-align:left" class="logo">
                                            Identificación</td>
                                        <td style="text-align:left">
                                            Nombres</td>
                                        <td style="text-align:left">Apellidos</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left">
                                            <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" />
                                            <br />
                                        </td>
                                        <td style="width: 342px; text-align:left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                        </td>
                                        <td style="width: 342px; text-align:left">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="327px" />
                                        </td>
                                         <td style="width: 342px; text-align:left">
                                             <asp:TextBox ID="TxtApellidos" runat="server" CssClass="textbox" Width="327px" />
                                        </td>
                                        <td style="width: 342px; text-align:left">&nbsp;</td>
                                        <td style="text-align:left">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left; height: 25px;">
                                            &nbsp;</td>
                                        <td style="width: 342px; text-align:left; height: 25px;">
                                            <asp:CompareValidator ID="cvidentificacion" runat="server" ControlToValidate="txtidentificacion" Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                            </td>
                                        <td style="text-align:left; ">
                                            Oficina<br />
                                            <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Height="30px" Width="191px">
                                            </asp:DropDownList>
                                            </td>
                                        <td style="text-align:left; ">
                                            <asp:CheckBox ID="chkcuentaahorros" runat="server" AutoPostBack="True" Visible="true" style="font-size: x-small" Text="Clientes Asociados A Cuentas Para Debito Automático" />
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
                <hr width="100%" />
            </td>
        </tr>
        <tr>
            <td>
               

                 <asp:gridview id="gvLista" runat="server" width="100%"   ShowHeaderWhenEmpty="True"
                    gridlines="Horizontal" autogeneratecolumns="False" allowpaging="True"
                     onpageindexchanging="gvLista_PageIndexChanging"
                     onselectedindexchanged="gvLista_SelectedIndexChanged"
                     onrowediting="gvLista_RowEditing" pagesize="20" 
                    headerstyle-cssclass="gridHeader" 
                    pagerstyle-cssclass="gridPager" 
                    rowstyle-cssclass="gridItem" datakeynames="consecutivo">

                    <Columns>
                        <asp:BoundField DataField="cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
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
                                           
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >                  
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_persona" HeaderText="Código Persona" >                       
                        </asp:BoundField>

                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" >                    
                        </asp:BoundField>
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" >                  
                        </asp:BoundField>                                               
                        <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" >                        
                        </asp:BoundField>
                         <asp:BoundField DataField="numero_cuenta_ahorro" HeaderText="Cuenta A Debitar" >                   
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
