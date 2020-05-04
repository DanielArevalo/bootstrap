<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <table style="width: 540px">
                <tr>
                    <td colspan="2">                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                        <asp:TextBox ID="txtcodPersona" runat="server" CssClass="textbox" Visible="false" />
                    </td>
                    <td style="width: 400px; text-align: left">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        Fecha de Legalización
                        <br />
                        <ucFecha:fecha ID="txtFecha" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server">
                <table style="width: 95%">
                    <tr>
                        <td style="text-align: left">
                            <strong>Giros para Legalizar</strong><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvDetalle" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" onrowediting="gvDetalle_RowEditing" 
                                style="font-size: x-small" 
                                onpageindexchanging="gvDetalle_PageIndexChanging">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="False" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="No.Comp">
                                        <ItemStyle HorizontalAlign="Left" Width="60px"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_comp" HeaderText="T.Comp">
                                        <ItemStyle HorizontalAlign="Center" Width="40px"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="entidad" HeaderText="Cod.Banco">
                                        <ItemStyle HorizontalAlign="Center" Width="60px"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombrebanco" HeaderText="Nombre Banco">
                                        <ItemStyle HorizontalAlign="Left" Width="160px"  />
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="n_documento" HeaderText="Num.Cheque">
                                        <ItemStyle HorizontalAlign="Left" Width="60px"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Concepto">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c0}">
                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Entregar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkTraslador" runat="server"  AutoPostBack="True" 
                                                oncheckedchanged="chkTraslador_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Total :
                            <asp:TextBox ID="txtValorAaplicar" runat="server" CssClass="textboxAlineadoDerecha" Width="100px" Enabled="false" style="text-align:right"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color: Red">
                            La legalización de giros fue grabada&nbsp;correctamente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc3:mensajegrabar ID="ctlMensaje" runat="server" />

    <uc2:validarBiometria ID="ctlValidarBiometria" runat="server" />

</asp:Content>
