<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <br />
    <table cellpadding="5" cellspacing="0" style="width: 65%; text-align:left">
        <tr>
            <td colspan="3">
                 <strong __designer:mapid="193a">
                <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td width="10%">
                Código &#160;&#160;<br/>
         <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" CssClass="textbox" Width="95%"></asp:TextBox>
            
            </td>
            <td colspan="2">
                Nombre <br/>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="False" CssClass="textbox" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                Oficina <br/>
                <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="100%"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td>
                Fecha Creación<br/>  
                <asp:TextBox ID="txtFecha" Width="172px" enabled="false" runat="server" 
                    CssClass="textbox"></asp:TextBox>
            </td>   
            <td>
                Estado<br/>
                <asp:TextBox ID="txtEstadoAct" enabled="false" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="95%"></asp:TextBox><br/>
            </td>   
            <td valign="top" style="width: 200px">
                Es Caja Principal <br/>
                <asp:CheckBox ID="chkPrincipal" Enabled="false" ClientIDMode="Static" runat="server" />
            </td>
        </tr>
    </table>
    <hr />
    <table cellpadding="5" cellspacing="0" style="width: 70%; text-align:left">
        <tr>
            <td style="width: 180px">
                Nuevo Estado <br/>
                <asp:CheckBox ID="chkNuevoEstado" ClientIDMode="Static" runat="server" />
            </td>
             <td>
                 Fecha Nuevo Estado<br/>
                <asp:TextBox ID="tbxFechaNuevoProceso" enabled="false" runat="server" CssClass="textbox" 
                    MaxLength="10" Width="120px"></asp:TextBox>
            </td>
            <td style="width: 192px">
                Horario&#160;&#160;<br/>
                <asp:DropDownList ID="ddlTipoHorarioNuevo" Enabled="false" CssClass="textbox"  runat="server" Width="182px">
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="2">Adicional</asp:ListItem>
                </asp:DropDownList>    
            </td>
           
        </tr>
        <tr>
            <td>
                &nbsp;</td>
             <td>
                 &nbsp;</td>
            <td style="width: 192px">
                &nbsp;</td>
           
        </tr>
    </table>
</asp:Content>

