<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <br />
            <table border:"0" cellpadding:"0" cellspacing:"0" width:"100%">
                 <tr>
                    <td colspan="2" style="text-align: left; width:250px;">Codigo Alerta <br />
                       <asp:TextBox ID="txtCodalerta" runat="server" CssClass="textbox" Width="100px" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField ID="hdIdActividad" runat="server" />
                    </td>
                 </tr>
                <td colspan="2" style="text-align: left">Nombre Alerta<br />
                    <asp:TextBox id="txtnomAlerta" runat="server" CssClass="textbox" TextMode="multiline" Columns="44" Rows="3" runat="server"  />       
                    
                    </td>

                <tr>
                    <td colspan="2" style="text-align: left">Indicadores<br />
                        <asp:TextBox id="txtIndicador" runat="server" CssClass="textbox" TextMode="multiline" Columns="44" Rows="3" runat="server"  />   
                        
                    </td>
                </tr>

                 <tr>
                    <td colspan="2" style="text-align: left">formula-Sql<br />
                        <asp:TextBox id="txtSentenciasql" runat="server" CssClass="textbox" TextMode="multiline" Columns="44" Rows="3" runat="server"  />   
                        
                    </td>
                </tr>
                <tr>
                 <td colspan="2" style="text-align: left">Descripcion de la alerta<br />
                           <asp:TextBox id="txtdescripcion" runat="server" CssClass="textbox" TextMode="multiline" Columns="44" Rows="3" runat="server"  />
                    </td>
                    </tr>
                <tr>
                 <td colspan="2" style="text-align: left">periocidad<br />
                           <asp:DropDownList ID="ddlPeriocidad" runat="server" CssClass="textbox" Width="250px" AutoPostBack="True" ></asp:DropDownList>
                    </td>
                    </tr>
                
                
                </tr>
              </table>
        </asp:Panel>
</asp:Content>
