<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - ComposicionPasivo :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">


                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="ENTIDAD" HeaderText="Entidad" />
                        <asp:BoundField DataField="CUPO" HeaderText="Cupo" />
                        <asp:BoundField DataField="SALDO" HeaderText="Saldo a la Fecha" />
                        <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor cuota Mensual" />
                       </Columns>
                </asp:GridView>



                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
   
        <tr>
            <td colspan="2">
                        <hr />
            </td>
        </tr>
        <tr>
            <td style="height: 27px">
                       <asp:TextBox ID="txtIdpasivo" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" Visible="False" />
                       </td>
            <td style="height: 27px">
                       <asp:TextBox ID="txtCod_inffin" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       </td>
        </tr>
        <tr>
            <td>
                       Entidad<br />
                       <asp:TextBox ID="txtentidad" runat="server" CssClass="textbox" 
                            MaxLength="128" Width="50%" />
                       </td>
            <td>
                       Cupo&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtcupo" runat="server" CssClass="textbox"  Width="50%" 
                    MaxLength="128" />
                       </td>
        </tr>
        <tr>
            <td>
                       Saldo a la Fecha&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtsaldo" runat="server" CssClass="textbox"  Width="50%" 
                            MaxLength="128" />
                       </td>
            <td>
                       Valor Cuota &nbsp;&nbsp;Mensual<br />
                       <asp:TextBox ID="txtcuota" runat="server" CssClass="textbox"  Width="50%" 
                    MaxLength="128" />
                       </td>
        </tr>
      
    </table>
    <%-- <asp:BoundField DataField="DESTINO" HeaderText="Destino" />--%>
</asp:Content>