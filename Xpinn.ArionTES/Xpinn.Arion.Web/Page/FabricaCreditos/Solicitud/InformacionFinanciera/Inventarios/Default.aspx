<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionFinanciera_InventarioActivoFijo_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="JavaScript">

//        function autoResize(id) {
//            var newheight;
//            // var newwidth;

//            if (document.getElementById) {
//                newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
//                //  newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
//            }

//            document.getElementById(id).height = (newheight) + "px";
//            //           document.getElementById(id).width = (newwidth) + "px";
//        }

    </script>
    <table width="100%">
        <tr>
            <td>
                <iframe scrolling="auto" frameborder="0" src="InventarioActivoFijo/Lista.aspx" width="100%"
                    height="100%" id="ifActivoFijo" onload="autoResize(this.id);"></iframe>
            </td>
            <td>
                <iframe scrolling="auto" frameborder="0" src="InventarioMateriaPrima/Lista.aspx" width="100%"
                    height="100%" id="ifMateriaPrima" onload="autoResize(this.id);"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>
