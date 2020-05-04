<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Deducciones.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <table width="50%">
                    <tr>
                        <td style="text-align: left">
                            Linea de Credito
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCodLineaCredito" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Atributo
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlatributo" runat="server" Height="25px" Width="207px" CssClass="textbox"
                                onselectedindexchanged="ddlatributo_SelectedIndexChanged" />
                        </td>
                    </tr>                    
                    <tr>
                        <td style="text-align: left">
                            Tipo de Descuento
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddldescuentostipo" runat="server" Width="207px" AutoPostBack="True"  CssClass="textbox"
                                onselectedindexchanged="ddldescuentostipo_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblTipoLiquidacion" runat="server" Text="Tipo de Liquidación"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlliquidacion" runat="server" Width="207px"  CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Forma de Descuento
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddldescuentosforma" runat="server" Width="207px" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Valor
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtvalor" runat="server" Width="200px"  CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Num Cuotas
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtcuotas" runat="server" Width="200px"  CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Cobra Mora
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="CheckBox" runat="server" Text=" " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Tipo de Impuestos
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlimpuestos" runat="server" Height="25px" Width="207px" 
                                 CssClass="textbox" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Modificable</td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="ChkModifica" runat="server" Text=" " />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
