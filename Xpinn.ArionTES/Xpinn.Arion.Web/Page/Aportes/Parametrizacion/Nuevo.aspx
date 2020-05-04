<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
 <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>

  <asp:MultiView ID="mtvInformacion" runat="server" ActiveViewIndex="0">
  <asp:View id="vwDatos" runat="server">
    <table style="width: 100%">       
        <tr>
            <td style="text-align: left">
                Tipo de Persona :&nbsp; &nbsp;
                <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="150px" CssClass="textbox"
                    OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                    <asp:ListItem Value="N">Natural</asp:ListItem>
                    <asp:ListItem Value="J">Juridica</asp:ListItem>
                    <asp:ListItem Value="M">Menores de Edad</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelInforAdicional" runat="server" Visible="false">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <br />
                    <strong>Información Adicional de la persona :</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Button ID="btnInfAdicional" runat="server" CssClass="btn8" OnClick="btnInfAdicional_Click"
                        OnClientClick="btnInfAdicional_Click" Text="+ Adicionar Detalle" /><br />
                    <asp:GridView ID="gvInformacionADD" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="0" DataKeyNames="cod_infadicional" ForeColor="Black" GridLines="Both"
                        OnRowDataBound="gvInformacionADD_RowDataBound" OnRowDeleting="gvInformacionADD_RowDeleting"
                        OnRowEditing="gvInformacionADD_RowEditing" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                        Width="550px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                          
                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblcod_infadicional" runat="server" Text='<%# Bind("cod_infadicional") %>' Width="90%">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescrip" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                        text-align: left" Text='<%# Bind("descripcion") %>' Width="90%"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("tipo") %>' Visible="false"></asp:Label>
                                    <cc1:dropdownlistgrid id="ddltipoInf" runat="server" appenddatabounditems="True" AutoPostBack="true" OnSelectedIndexChanged="ddltipoInf_SelectedIndexChanged"
                                        commandargument="<%#Container.DataItemIndex %>" cssclass="textbox" style="font-size: xx-small;
                                        text-align: left" width="90%">
                                                                </cc1:dropdownlistgrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Items de Lista" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItems_lista" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                        text-align: left" Text='<%# Bind("items_lista") %>' Width="90%" Visible="false" placeholder="Item1,Item2,Item3,..."></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="45%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </asp:View>
    <asp:View ID="vwfinal" runat="server">
<table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;color:Red">
                            Los datos fueron
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;</td>
                    </tr>
                </table>    
    </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
