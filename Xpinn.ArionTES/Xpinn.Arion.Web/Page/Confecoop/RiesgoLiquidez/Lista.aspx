<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="PanelDatos" runat="server">
                <div style="text-align: right">
                    <asp:ImageButton ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" ImageUrl="~/Images/btnConsultar.jpg" />&#160;
                    <asp:ImageButton ID="btnExportar" runat="server" OnClick="btnExportar_Click" ImageUrl="~/Images/btnExportar.jpg" Visible="false"/>
                </div>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left; width: 150px; height: 50px;">
                            Fecha de Corte<br />
                            <%--<ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />--%>
                            <asp:DropDownList ID="ddlFecha" runat="server" CssClass="textbox" Width="135px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 50px;">
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                    </tr>
                    <tr display="Dynamic">
                        <td style="text-align: left; width: 150px;">
                            Tipo de Archivo<br />
                            <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                Width="222px">
                                <asp:ListItem Value=";">CSV</asp:ListItem>
                                <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                <asp:ListItem Value="|">EXCEL</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">
                            Nombre del Archivo<br />
                            <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingrese una Ruta del Archivo a Generar : C:\Users\..."
                                ValidationGroup="vgConsultar" Display="Dynamic" ControlToValidate="txtArchivo"
                                ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                        <td style="text-align: left;">
                            <asp:UpdatePanel ID="upNiif" runat="server">
                                <ContentTemplate>
                                    Tipo de Norma<br />
                                    <asp:DropDownList ID="ddlTipoCuenta" runat="server" AutoPostBack="true" CssClass="textbox" Width="150px"
                                        onselectedindexchanged="ddlTipoCuenta_SelectedIndexChanged" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="max-width: 100%; width: 100%; overflow: scroll; position: relative; max-height: 550px">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" Width="100%" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" HeaderStyle-Font-Size="x-small" 
                                    onrowdatabound="gvLista_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="unidad_captura" HeaderText="Unidad de Captura">
                                            <ItemStyle HorizontalAlign="left" Width="65px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="renglon" HeaderText="Código de Renglon">
                                            <ItemStyle HorizontalAlign="left" Width="65px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDescripcion" runat="server" Width="380px" AutoPostBack_="false"
                                                    Text='<%# Bind("descripcion") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Saldo a la Fecha">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtSaldo" runat="server" Width="90px" AutoPostBack_="false" Text='<%# Bind("saldo_actual") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<= 1 mes">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha1" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha1") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De 1 a 2 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha2" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha2") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De 2 a 3 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha3" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha3") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De 3 a 6 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha4" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha4") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De 6 a 9 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha5" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha5") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="De 9 a 12 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha6" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha6") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mayor a 12 meses">
                                            <ItemTemplate>
                                                <uc1:decimales ID="txtBrecha7" runat="server" Width="90px" AutoPostBack_="false"
                                                    Text='<%# Bind("vr_brecha7") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo de Centrales de Riesgo Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
