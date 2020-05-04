<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:panel id="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">                  
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left">
                                            Identificación</td>
                                        <td style="width: 197px; text-align:left" class="logo">
                                            Nombre Completo</td>
                                        <td style="text-align:left">
                                            Código de nómina</td>
                                        <td style="text-align:left">
                                            Oficina</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="180px" />
                                            <br />
                                        </td>
                                        <td style="width: 342px; text-align:left">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="350px" />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" />
                                            <br />
                                        </td>
                                        <td style="text-align:left">
                                            <asp:DropDownList ID="ddlOficinas" runat="server" AppendDataBoundItems="true" CssClass="textbox" 
                                                Height="30px" Width="191px">
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 197px; text-align:left; height: 25px;">
                                            <asp:CompareValidator ID="cvidentificacion" runat="server" 
                                                ControlToValidate="txtidentificacion" Display="Dynamic" 
                                                ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                                                SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                                        </td>
                                        <td style="width: 342px; text-align:left; height: 25px;">
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
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:gridview id="gvLista" runat="server" width="100%" autogeneratecolumns="False"
                    allowpaging="True" pagesize="5" gridlines="Horizontal" showheaderwhenempty="True"
                    onpageindexchanging="gvLista_PageIndexChanging" onrowediting="gvLista_RowEditing"
                    onrowcommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="cod_persona" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Reestructuración" CommandName="Reestructuración" CommandArgument='<%#Eval("cod_persona")%>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre" />
                        <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre" />
                        <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido" />
                        <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:gridview>
                <asp:label id="lblTotalRegs" runat="server" visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
