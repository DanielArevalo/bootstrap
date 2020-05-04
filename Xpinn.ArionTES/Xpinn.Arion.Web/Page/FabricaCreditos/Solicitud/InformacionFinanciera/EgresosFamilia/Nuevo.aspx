<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - EgresosFamilia :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCod_egreso" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" Visible="False" />
                       <asp:TextBox ID="txtEgresos" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" >0</asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Pago 
                           Deudas<br />
                       <asp:TextBox ID="txtPagodeudas" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtPagodeudas" runat="server" targetcontrolid="txtPagodeudas" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                           <br />
                       </td>
                       <td class="tdD">
                       Alimentacion&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtAlimentacion" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtAlimentacion" runat="server" targetcontrolid="txtAlimentacion" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Vivienda&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtVivienda" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtVivienda" runat="server" targetcontrolid="txtVivienda" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                       </td>
                       <td class="tdD">
                       Educacion&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtEducacion" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtEducacion" runat="server" targetcontrolid="txtEducacion" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Servicios Publicos&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtServiciospublicos" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtServiciospublicos" runat="server" targetcontrolid="txtServiciospublicos" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
         
                       </td>
                       <td class="tdD">
                       Transporte&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtTransporte" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtTransporte" runat="server" targetcontrolid="txtTransporte" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
         
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Otros&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtOtros" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtOtros" runat="server" targetcontrolid="txtOtros" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
         
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
    </table>
   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_EGRESO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>