<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <br />
            <table border:"0" cellpadding:"0" cellspacing:"0" width:"100%">
                 <tr>
                    <td colspan="2" style="text-align: left; width:250px;">Codigo Control <br />
                       <asp:TextBox ID="txtCodControl" runat="server" CssClass="textbox" Width="100px" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField ID="hdIdActividad" runat="server" />
                    </td>
                 </tr>
                <td colspan="2" style="text-align: left">Valor<br />
                           <asp:TextBox ID="txtvalor" runat="server" CssClass="textbox" Width="250px" AutoPostBack="True" ></asp:TextBox>
                    </td>
                 <tr>
                     <td colspan="2" style="text-align: left">Calificacion<br />
                         <asp:DropDownList ID="ddlValor" runat="server" CssClass="textbox" Width="250px">
                             <asp:ListItem >Selecione un valor </asp:ListItem>
                             <asp:ListItem >Eficiente</asp:ListItem>
                             <asp:ListItem >Alto</asp:ListItem>
                             <asp:ListItem >Medio</asp:ListItem>
                             <asp:ListItem >Bajo</asp:ListItem>
                             <asp:ListItem >Ineficaz</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">Descripcion<br />
                           <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="250px" AutoPostBack="True" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">Frecuencia<br />
                           <asp:DropDownList ID="ddlFrecuencia" runat="server" CssClass="textbox" Width="250px" AutoPostBack="True" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Rango Mínimo <br />
                       <asp:TextBox ID="txtRangoMinimo" runat="server" CssClass="textbox" Width="60px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">Rango Máximo <br />
                       <asp:TextBox ID="txtRangoMaximo" runat="server" CssClass="textbox" Width="60px"></asp:TextBox>
                    </td>
                 </tr>
            </table>
        </asp:Panel>
</asp:Content>

