<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Reporteador_Reportes_ReportesExogena_Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 150px;" colspan="3">
               <asp:Label id="lblMensaje" runat="server" ForeColor="#99CC00"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 150px;">Año del Reporte<br />
               <asp:TextBox id="txtAño" runat="server"></asp:TextBox>
            </td>
        </tr>
      <%--  <tr>
            <td  style="text-align: left;">
                Tipo de Archivo<br />
                <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal" Width="200px">
                    <asp:ListItem Value=";">CSV</asp:ListItem>
                    <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                    <asp:ListItem Value="|">EXCEL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: left;">&nbsp;</td>
            <td style="text-align: left;">
              <asp:RadioButtonList ID="rdbCuantias" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Value="1" Selected="true">Cuantias Mayores </asp:ListItem>
                  <asp:ListItem Value="2">Cuantias Menores</asp:ListItem>
              </asp:RadioButtonList>
            </td>
        </tr>--%>
        <tr>
            <td style="text-align: left;"  colspan="2">
                Formato :
                <asp:DropDownList ID="ddlTipoFormato" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlTipoFormato_SelectedIndexChanged"
                   AutoPostBack="true" Width="240px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left; vertical-align: top">Nombre del Archivo<br />
                <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvArchivo" runat="server"
                    ErrorMessage="Ingrese el Nombre del archivo a Generar"
                    ValidationGroup="vgExportar" Display="Dynamic" ControlToValidate="txtArchivo"
                    ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" >
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

