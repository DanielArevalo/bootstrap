<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server"> 
   
     <table style="width:100%;">
            <tr>
                <td>
              <asp:Panel ID="pConsulta" runat="server" style="margin-bottom: 0px">
                    <table cellpadding="5" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="tdI" style="height: 55px">
                                &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td class="tdI" style="height: 55px">
                                <strong>
                                <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                                </strong>
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>

            </tr>
            <tr>
                <td style="height: 25px; font-weight: 700;">
                    <strong>
                    <asp:GridView ID="gvGarantiasReales" runat="server" Width="97%" 
                        AutoGenerateColumns="False" PageSize="5" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Height="10px" ShowFooter="True" 
                        style="font-size: xx-small" 
                        onrowdatabound="gvGarantiasReales_RowDataBound" 
                        onrowcommand="gvGarantiasReales_RowCommand" 
                        onrowediting="gvGarantiasReales_RowEditing" DataKeyNames="consecutivo" 
                        onrowupdated="gvGarantiasReales_RowUpdated" 
                        onrowupdating="gvGarantiasReales_RowUpdating" 
                        onselectedindexchanged="gvGarantiasReales_SelectedIndexChanged" 
                        ondatabound="gvGarantiasReales_DataBound" 
                        onrowcancelingedit="gvGarantiasReales_RowCancelingEdit" 
                        onrowdeleted="gvGarantiasReales_RowDeleted" 
                        onrowdeleting="gvGarantiasReales_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                &nbsp;<asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" 
                                    ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" 
                                    ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" 
                                    CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" 
                                    Width="16px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consecutivo" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtconsecutivo" runat="server" style="margin-top: 0px" 
                                    Text='<%# Bind("consecutivo") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <strong>
                                <asp:Label ID="lblconsecutivo" runat="server" Text='<%# Bind("consecutivo") %>'></asp:Label>
                                </strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Linea Credito">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlineacreditoedit" runat="server" 
                                  DataSource='<%# ListaLineaCredito() %>'  DataTextField="Nombre" DataValueField="Nombre" 
                                    SelectedValue='<%# Bind("Nombre") %>' style="text-align: center">
                                            </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlnewlineacredito" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbllineacredito" runat="server" 
                                    Text='<%# Bind("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo de Garantia">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddltipogarantiaedit" runat="server" 
                                   DataSource='<%# ListaTipoGarantias() %>'  DataTextField="TipoGarantia" DataValueField="TipoGarantia" 
                                    SelectedValue='<%# Bind("TipoGarantia") %>' style="text-align: center">

                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlnewtipogarantia" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbltipogarantia" runat="server" 
                                    Text='<%# Bind("TipoGarantia") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cuenta Orden Debito">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlctadebitoedit" runat="server" 
                                      DataSource='<%# ListaCuentasDebito() %>'  DataTextField="Nombre" DataValueField="Nombre" 
                                    SelectedValue='<%# Eval("CTADEBITO") %>' style="text-align: center">
                                     </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlnewctadebito" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblctadebito" runat="server" 
                                    Text='<%# Bind("CtaDebito") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cuenta Orden Credito">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlctacreditoedit" runat="server" 
                                     DataSource='<%# ListaCuentasCredito() %>'  DataTextField="Nombre" DataValueField="Nombre" 
                                    SelectedValue='<%# Eval("CTACREDITO") %>' style="text-align: center">
                                  </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlnewctacredito" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblctacredito" runat="server" 
                                    Text='<%# Bind("CTACREDITO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gridHeader" />
                    <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>

                    </strong>

                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                </td>
            </tr>
        </table>
        <br />
      <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigoGarantia').focus();
        }
        window.onload = SetFocus;
     </script>
</asp:Content>

