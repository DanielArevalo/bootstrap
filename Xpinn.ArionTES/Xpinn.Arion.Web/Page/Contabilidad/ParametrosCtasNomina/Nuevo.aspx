<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cta Contables :." %>

<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvParametros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="650px">
                    <tr>
                        <td style="text-align: left; width: 76px;">Código<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="width: 500;">&nbsp;
                        </td>
                    </tr>
                   <%-- <tr>
                        <td colspan="2" style="text-align: left">Estructura<br />
                            <asp:DropDownList ID="ddlEstructura" runat="server" Width="90%" CssClass="textbox"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 76px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 130px; text-align: left">Cuenta Contable<br />
                                                <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                                    CssClass="textbox" OnTextChanged="txtCodCuenta_TextChanged" Style="text-align: left"
                                                    Width="120px"></cc1:TextBoxGrid>
                                                <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                            </td>
                                            <td style="width: 25px; text-align: center">
                                                <br />
                                                <cc1:ButtonGrid ID="btnListadoPlan" runat="server" CssClass="btnListado" OnClick="btnListadoPlan_Click"
                                                    Width="95%" Text="..." />
                                            </td>
                                            <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="300px" />
                                            </td>
                                            <td style="width: 190px; text-align: left">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 76px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 130px; text-align: left">Cod Cuenta NIIF
                                <br />
                                                <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                                                    CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                                                </cc1:TextBoxGrid>
                                                <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />
                                            </td>
                                            <td style="width: 25px; text-align: center">
                                                <br />
                                                <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..."
                                                    Width="95%" OnClick="btnListadoPlanNIF_Click" />
                                            </td>
                                            <td style="width: 230px; text-align: left">Nombre de la Cuenta
                                <br />
                                                <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                                                    Width="300px" Enabled="False">
                                                </cc1:TextBoxGrid>
                                            </td>
                                            <td style="width: 190px; text-align: left">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 76px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 130px; text-align: left">Cod Cuenta Nomina<br />
                                                <cc1:TextBoxGrid ID="txtCodCuentaNomina" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                                    CssClass="textbox" OnTextChanged="txtCodCuentaNomina_TextChanged" Style="text-align: left"
                                                    Width="120px"></cc1:TextBoxGrid>
                                                <uc1:ListadoPlanCtas ID="ctlCuentasNomina" runat="server" />
                                            </td>
                                            <td style="width: 25px; text-align: center">
                                                <br />
                                                <cc1:ButtonGrid ID="btnListarCuentaNomina" runat="server" CssClass="btnListado" OnClick="btnListadoPlanNomina_Click"
                                                    Width="95%" Text="..." />
                                            </td>
                                            <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                <cc1:TextBoxGrid ID="txtNombreCuentaNomina" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="300px" />
                                            </td>
                                            <td style="width: 190px; text-align: left">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Area - Departamento
                            <br />
                              <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Area" />
                            <br />
                            <asp:GridView ID="gv_Areas" runat="server" AutoGenerateColumns="false"
                                   DataKeyNames="consecutivo" OnRowDataBound="gvArea_RowDataBound"  OnRowDeleting="gvArea_RowDeleting">
                                <Columns>
                                      <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                     <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("consecutivo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                      <asp:TemplateField HeaderText="Area">
                          <ItemTemplate>
                                <asp:Label ID="lblTipoImpuesto" runat="server" Text='<%# Bind("IdArea") %>'
                                                        Visible="false"></asp:Label>
                              <cc1:DropDownListGrid ID="ddlAreas" runat="server"  AppendDataBoundItems="True"
                              CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"></cc1:DropDownListGrid>
                          </ItemTemplate>
                      </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Cod Cuenta Contable">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCodCuentaGV" runat="server" AutoPostBack="True" Style="text-align: left"
                                                    BackColor="#F4F5FF" Width="90px" Text='<%# Bind("cod_cuenta") %>'  OnTextChanged="txtCodCuenta1_TextChanged"
                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>&nbsp;<cc1:ButtonGrid ID="btnListadoPlanHomo" CssClass="btnListado"
                                                    runat="server" Text="..." OnClick="btnListadoPlanHomo_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                <uc1:ListadoPlanCtas ID="ctlCuentasNomina1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;<cc1:TextBoxGrid ID="lblNombreCuenta" runat="server" 
                                                    Style="padding-left: 15px" Width="350px" Text='<%# Bind("nombre_cuenta") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                </Columns>
                                  <HeaderStyle CssClass="gridHeader" />
                 <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                 <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left; width: 200px; padding-right: 60px;">Tipo de Movimiento<br />
                                        <asp:DropDownList ID="ddlTipoMov" runat="server" Width="100%" CssClass="textbox" />
                                    </td>
                                   <%-- <td style="text-align: left; width: 350px">Tipo de Transacción<br />
                                        <ctl:ctlListarCodigo ID="ctlListarCodigoTransaccion" runat="server" />
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width: 150px; text-align:left">Tercero
                                                <br />
                                                <asp:TextBox ID="txtIdentificacion" AutoPostBack="true" onkeypress="return isNumber(event)"  runat="server" CssClass="textbox" Width="100px"
                                                    MaxLength="12" OnTextChanged="txtIdentificacion_TextChanged" />
                                                <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                                                    OnClick="btnConsultaPersonas_Click" />
                                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                                <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox"
                                                    Enabled="False" MaxLength="12" Visible="False" Width="10px"></asp:TextBox>
                                            <td style="width: 300px; text-align: left">Nombre Tercero<br />
                                                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="300px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">Concepto Nomina <br />
                            <asp:DropDownList runat="server" ID="ddlConceptoNomina" Width="30%" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFin" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblmsj" runat="server" Style="color: Red" Font-Bold="True" /><br />
                        <asp:Button ID="btnFin" runat="server" CssClass="btn8" Height="28px"
                            Text="  Regresar  " OnClick="btnFin_Click" />
                    </td>
                </tr>
            </table>

        </asp:View>
    </asp:MultiView>

</asp:Content>
