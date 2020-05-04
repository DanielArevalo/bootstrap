<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Blance.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_InformacionFinancieraNegocio_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="1" Width="100%"
        Height="100%">
       <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Visible="false">
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Ingresos Familia" ID="TabPanel2">
            <HeaderTemplate>
                Balance General Familia
            </HeaderTemplate>
            <ContentTemplate>
                <iframe frameborder="0" scrolling="no" src="../BalanceGeneralFamilia/Lista.aspx"
                    width="100%" id="I1" name="I1" height="650px"></iframe>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Egresos Familia" ID="TabPanel3">
            <HeaderTemplate>
                Balance Microempresa
            </HeaderTemplate>
            <ContentTemplate>
                <iframe frameborder="0" scrolling="no" src="../ActivosPasivos/Lista.aspx" width="100%"
                    id="I2" name="I2" height="600px"></iframe>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4" Visible="false">
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
