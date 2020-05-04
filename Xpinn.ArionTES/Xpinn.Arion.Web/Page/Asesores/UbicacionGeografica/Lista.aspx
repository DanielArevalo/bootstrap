<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" MasterPageFile="~/General/Master/site.master"%>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 100%; text-align: left;">
            <tr>
                <td style="width: 315px">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" Width="70%"
                        CssClass="dropdown" 
                        onselectedindexchanged="ddlOficina_SelectedIndexChanged" 
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="width: 315px">
                </td>
            </tr>
            <tr>
                <td style="width: 315px">
                    Tipo de Consulta<br />
                    <asp:DropDownList ID="ddlZona1" runat="server" AutoPostBack="True" Width="70%"
                        CssClass="dropdown" onselectedindexchanged="ddlZona_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>        
                 <td style="width: 315px; vertical-align: top">
                    <asp:Label ID="lblDropdown" runat="server"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlAsesor" runat="server" AutoPostBack="True" 
                        CssClass="dropdown" 
                        onselectedindexchanged="ddlAsesor_SelectedIndexChanged" Width="70%">
                    </asp:DropDownList>                
                    <asp:DropDownList ID="ddlCliente" runat="server"  Width="70%"
                        CssClass="dropdown" >
                    </asp:DropDownList>
                </td>   
            </tr>
            <tr>
                <td style="width: 315px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:GMap ID="GMap1" runat="server" Height="400px" Width="90%" />
</asp:Content>
