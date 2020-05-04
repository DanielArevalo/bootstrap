<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_BalanceGeneralMicroempresa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:MultiView ID="mvBalanceGeneralMicroempres" runat="server">
        <asp:View ID="View0" runat="server">
            <table width="100%">
                <tr>
                    <td >
                        <iframe frameborder="0" scrolling="no" width="100%" height="100%" 
                            src="ProductosEnProceso/Lista.aspx" ID="ifProductosEnProceso" onload="autoResize(this.id);"></iframe>
                    </td>
                    <td >
                        <iframe frameborder="0" scrolling="no" width="100%" height="100%" 
                            src="ProductosTerminados/Lista.aspx" ID="ifProductosTerminados" onload="autoResize(this.id);"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="btnAtras" runat="server" ImageUrl="~/Images/iconAnterior.jpg"
                            OnClick="btnAtras_Click" />
                        &nbsp;<asp:ImageButton ID="btnAdelante" runat="server" ImageUrl="~/Images/btnComposicionPasivo.jpg"
                            OnClick="btnAdelante_Click" OnClientClick="LoadingList()" />
                    </td>
                </tr>
            </table>  
        </asp:View>

    </asp:MultiView>
    
</asp:Content>
