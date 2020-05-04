<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="ctrl_menu" %>


<div class="sidebar" id="mySidebar">
    <h1 style="text-align: center;background-color: var(--side-menu-color);padding-top: 12px;"  >
        <asp:Label ID="lblModulo" class="sidebar_title" runat="server" Text=""></asp:Label>
    </h1>
    <asp:PlaceHolder ID="phOpciones" runat="server"></asp:PlaceHolder>
</div>
