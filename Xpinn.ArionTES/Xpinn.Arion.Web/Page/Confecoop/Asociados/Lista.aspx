<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="PanelDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <asp:ImageButton ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" ImageUrl="~/Images/btnConsultar.jpg" /> <%--OnClientClick="mostrar_procesar();"--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 150px; height: 50px;">
                            Fecha de Corte<br />
                            <%--<ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />--%>
                            <asp:DropDownList ID="ddlFecha" runat="server" CssClass="textbox" Width="135px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 50px;">
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                    </tr>
                    <tr display="Dynamic">
                        <td style="text-align: left; width: 150px;">
                            Tipo de Archivo<br />
                            <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                Width="222px">
                                <asp:ListItem Value=";">CSV</asp:ListItem>
                                <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                <asp:ListItem Value="|">EXCEL</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="text-align: left">
                            Nombre del Archivo<br />
                            <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                        <caption>
                            <span ID="procesando_div" style="display: none; text-align: center">
                            <img src="../../../Images/loading.gif" />
                            <div>
                                procesando...
                            </div>
                            </span>
                        </caption>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo Generado Correctamente"
                                Style="color: #FF0000; font-weight: 600"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function mostrar_procesar() {
            document.getElementById('procesando_div').style.display = "";
            setTimeout('document.images["loading_gif"].src="../../../Images/loading.gif"', 200);
            //$("#procesando_div").delay(150000).hide(600);

            $.ajax({ type: "POST", url: "Lista.aspx/btnConsultar_Click", data: "{}", contentType: "application/json; charset=utf-8", dataType: "json",
                success: function (msg) { alert(msg); } 
            });
        }

    </script>
</asp:Content>
