<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_InformacionFinancieraNegocio_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="3" ScrollBars="Auto" Height="700">
        <asp:TabPanel runat="server" HeaderText="Informacion Financiera Negocio" ID="TabPanel1" ScrollBars="Vertical">
            <ContentTemplate>
                <iframe frameborder="0" scrolling="no" src="Lista.aspx" width="100%" id="I3" 
                    name="I3" style="height: 754px"></iframe>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Informacion de Ingresos y Egresos" ID="TabPanel0">
            <HeaderTemplate>
                Total Informacion de Ingresos y Egresos
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%" style="text-align: Center; margin-bottom: 0px;">
                            <tr>
                                <td colspan="3">
                                    Ingreso Mensual
                                </td>
                                <td colspan="3">
                                    Egreso&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    Solicitante
                                </td>
                                <td>
                                    Cónyuge
                                </td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    Solicitante
                                </td>
                                <td style="text-align: left">
                                    Cónyuge
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Sueldo
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsueldo" runat="server" 
                                        OnTextChanged="txtsueldo_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="1">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsueldoconyuge" runat="server" 
                                        OnTextChanged="txtsueldoconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="5">0</asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    Cuota Hipoteca
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCuota" runat="server" OnTextChanged="txtCuota_TextChanged" 
                                        style="text-align: right" AutoPostBack="True" TabIndex="9">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCuotaconyuge" runat="server" 
                                        OnTextChanged="txtCuotaconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="14">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Honorarios
                                </td>
                                <td>
                                    <asp:TextBox ID="txthonorarios" runat="server" 
                                        OnTextChanged="txthonorarios_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="2">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txthonorariosconyuge" runat="server" 
                                        OnTextChanged="txthonorariosconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="6">0</asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    Cuota Tarjeta de Crédito
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcuotatrgcredito" runat="server" 
                                        OnTextChanged="txtcuotatrgcredito_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="10">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcuotatrgcreditoconyuge" runat="server" 
                                        OnTextChanged="txtcuotatrgcreditoconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="15">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Arrendamientos
                                </td>
                                <td>
                                    <asp:TextBox ID="txtarrendamientos" runat="server" 
                                        OnTextChanged="txtarrendamientos_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="3">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtarrendamientosconyuge" runat="server" 
                                        OnTextChanged="txtarrendamientosconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="7">0</asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    Cuota Otros Prestamos
                                </td>
                                <td>
                                    <asp:TextBox ID="txtotrosprestamos" runat="server" 
                                        Style="margin-top: 0px;text-align: right" 
                                        OnTextChanged="txtotrosprestamos_TextChanged" AutoPostBack="True" 
                                        TabIndex="11"  >0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtotrosprestamosconyuge" runat="server" 
                                        OnTextChanged="txtotrosprestamosconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="16">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Otros Ingresos
                                </td>
                                <td>
                                    <asp:TextBox ID="txtotrosingre" runat="server" 
                                        OnTextChanged="txtotrosingre_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="4">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtotrosingreconyuge" runat="server" 
                                        OnTextChanged="txtotrosingreconyuge_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="8">0</asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    Gastos Familiares
                                </td>
                                <td>
                                    <asp:TextBox ID="txtgastosfamiliares" runat="server" 
                                        OnTextChanged="txtgastosfamiliares_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="12">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtgastosfamiliaresconyuge" runat="server" 
                                        OnTextChanged="txtgastosfamiliaresconyuge_TextChanged" 
                                        style="text-align: right" AutoPostBack="True" TabIndex="17">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                    Descuentos por nomina
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdescuentosnomina" runat="server" 
                                        OnTextChanged="txtdescuentosnomina_TextChanged" style="text-align: right" 
                                        AutoPostBack="True" TabIndex="13"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdescuentosnominaconyuge" runat="server" 
                                        OnTextChanged="txtdescuentosnominaconyuge_TextChanged" 
                                        style="text-align: right" AutoPostBack="True" TabIndex="18">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr><td></td></tr>
                            <tr>
                                <td style="text-align: left">
                                    Total Ingresos
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotalingre" runat="server" style="text-align: right" AutoPostBack="True" Enabled="false">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotalingreconyuge" runat="server" style="text-align: right" AutoPostBack="True" Enabled="false">0</asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    Total Egresos
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotalegresos" runat="server" Enabled="False" style="text-align: right" AutoPostBack="True">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttotalegresosconyuge" runat="server" Enabled="False" style="text-align: right" AutoPostBack="True">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
