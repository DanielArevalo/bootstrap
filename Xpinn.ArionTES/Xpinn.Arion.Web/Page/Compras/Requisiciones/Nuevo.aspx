<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Areas :." %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" EnablePartialRendering="true" AsyncPostBackTimeout="0"  runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
            Id requisicion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipocomprobante" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
       
            <td class="tdI" style="text-align:left">
            Fecha requisicion :&nbsp;*&nbsp;<br />
            <uc1:fecha ID="txtFechaRequisicion" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left" />
            </td>
            <td class="tdD">
            </td>
      
            <td class="tdI" style="text-align:left">
                Fecha Estimada Entrega  :&nbsp;*&nbsp;<br />
               <uc1:fecha ID="txtFechaEntrega" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left" />
            </td>
            <td class="tdD">
            </td>
        </tr>

          <tr>
            <td class="tdI" style="text-align:left">
            Usuario:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtxtSerial" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" MaxLength="128"  />
            </td>
            <td class="tdD">
            </td>
        
            <td class="tdI" style="text-align:left">
            Cod Solicitud:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtxtMarca" runat="server" ControlToValidate="txtSolicitud" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtSolicitud" runat="server" CssClass="textbox" MaxLength="128"  />
            </td>
            <td class="tdD">
            </td>
      
            <td class="tdI" style="text-align:left">
            Destino:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvReferencia" runat="server" ControlToValidate="txtDestino" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="txtDestino" runat="server" CssClass="textbox" MaxLength="128"  />
            </td>
            <td class="tdD">
            </td>
        </tr>

       <tr>
            <td class="tdI" style="text-align:left">
            Area:&nbsp;*&nbsp;<br />
            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" CssClass="textbox"
            OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Style="text-align: left"
                                                            Width="170px">
                                                        </asp:DropDownList>

            </td>
            <td class="tdD">
            </td>
           
            <td class="tdI" style="text-align:left">
            Observaciones:&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtObservaciones" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
            <asp:TextBox ID="TxtObservaciones" runat="server" CssClass="textbox" MaxLength="128"  />
            </td>
            <td class="tdD">
            </td>
        </tr>

        
    </table> <br />

                    <ContentTemplate>
             <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvListaArticulo" runat="server" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="True" BackColor="White" BorderColor="#DEDFDE" 
                                    BorderWidth="1px" ForeColor="Black" OnRowDeleting="gvListaArticulo_RowDeleting"
                                    OnRowCommand="gvListaArticulo_RowCommand" Visible ="true"
                                    PageSize="10" DataKeyNames="IdArticulo" ShowFooter="True" Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" Height="16px" />
                                            </FooterTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle Width="16px" />
                                        </asp:CommandField>                                        
                                        <asp:TemplateField HeaderText="IdArticulo">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtArticulo" runat="server" Text='<%# Bind("IdArticulo") %>'
                                                    OnTextChanged="txtArticulo_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblArticulo" runat="server" Text='<%# Bind("IdArticulo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SERIAL" HeaderText="SERIAL" />
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                                             <asp:TemplateField HeaderText="Cantidad">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" 
                                                     Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltxtCantidad" runat="server"  Text='<%# Bind("Cantidad") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MARCA" HeaderText="MARCA" />
                                        <asp:BoundField DataField="REFERENCIA" HeaderText="REFERENCIA" />
                                  
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                              </asp:UpdatePanel>
                    </ContentTemplate>
           
</asp:Content>