<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table border="0" cellpadding="0" cellspacing="0" width="70%">
        <tr>
            <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pConsulta" runat="server">
                            <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td colspan="4">
                                        <strong>Criterios de Búsqueda</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 130px">Código<br />
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                                    </td>
                                    <td style="text-align: left; width: 150px">Cuenta Contable<br />
                                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                                            Style="text-align: left" BackColor="#F4F5FF" Width="100px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                                        <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                            OnClick="btnListadoPlan_Click" />

                                    </td>
                                    <td style="text-align: left; width: 300px">Nombre de la Cuenta
                                <br />
                                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                        <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                            Width="95%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <table>
                    <tr style="padding-bottom: 20px;">
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-top: 30px; width: 220px">Tipo de Transaccion<br />
                            <ctl:ctlListarCodigo ID="ctlListarCodigoTransaccion" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="width: 100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" DataKeyNames="idparametro"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                    PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg"
                            ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                            ShowDeleteButton="True" />
                        <asp:BoundField DataField="idparametro" HeaderText="Código">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta" HeaderText="Cta Contable">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomCuenta" HeaderText="Nom Cuenta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_tipo_mov" HeaderText="Tipo Movimiento">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomtipo_tran" HeaderText="Tipo Transacción">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomimpuesto" HeaderText="Tipo Impuesto">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomestructura" HeaderText="Estructura">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod.NIFF">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_cuenta_niif" HeaderText="Nombre NIFF">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
