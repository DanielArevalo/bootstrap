<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ctlmultiempresas.ascx.cs" Inherits="General_Controles_Ctlmultiempresas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:UpdatePanel ID="upListado" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
        <asp:TextBox ID="txtDato" runat="server" Width="145px" ReadOnly="True"></asp:TextBox>
        <asp:PopupControlExtender ID="txtDato_PopupControlExtender" runat="server"
            Enabled="True" ExtenderControlID="" TargetControlID="txtDato" 
            PopupControlID="panelLista" OffsetY="22">
        </asp:PopupControlExtender>
        <asp:Panel ID="panelLista" runat="server" Height="140px" Width="145px" 
            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
            ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">                   
            <asp:CheckBoxList ID="cbListado" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="cbListado_SelectedIndexChanged">
            </asp:CheckBoxList>
        </asp:Panel>
    </ContentTemplate>  
    <Triggers>
        <asp:PostBackTrigger ControlID="txtDato" />
    </Triggers>
</asp:UpdatePanel>