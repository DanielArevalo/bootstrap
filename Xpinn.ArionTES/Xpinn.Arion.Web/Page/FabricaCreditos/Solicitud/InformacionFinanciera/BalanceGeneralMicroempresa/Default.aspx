<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_BalanceGeneralMicroempresa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:MultiView ID="mvBalanceGeneralMicroempres" runat="server">
        <asp:View ID="View0" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <iframe frameborder="0" scrolling="no" width="100%"></iframe>
                    </td>
                    <td>
                        <iframe frameborder="0" scrolling="no" width="100%"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <iframe id="I3" frameborder="0" height="600px" name="I3" scrolling="no" src="../InformacionFinancieraNegocio/Lista.aspx"
                            width="100%"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <iframe frameborder="0" height="500px" scrolling="no" src="../BalanceGeneralFamilia/Lista.aspx"
                            width="100%" id="I2" name="I2"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="btnAtras" runat="server" ImageUrl="~/Images/iconAnterior.jpg"
                            OnClick="btnAtras_Click" />
                        <asp:ImageButton ID="btnAdelante" runat="server" ImageUrl="~/Images/iconSiguiente.jpg"
                            OnClick="btnAdelante_Click" OnClientClick="LoadingList()" />
                    </td>
                </tr>
            </table>  
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                        NUMERO DE RADICACION DE LA OBLIGACION:
                        <asp:Label ID="lblNumRadicacion" runat="server"></asp:Label>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
    
</asp:Content>
