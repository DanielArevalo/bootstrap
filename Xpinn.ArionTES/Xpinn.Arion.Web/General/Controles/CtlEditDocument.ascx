<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlEditDocument.ascx.cs" Inherits="General_Controles_CtlEditDocument" %>

<script src="../../../Scripts/ckeditor/ckeditor.js"></script>

<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.4/toastr.css" />
<div style="margin-top: 2%; display: flex">
    <textarea id="FreeTextBox2" runat="Server" text="" width="900px"></textarea>
    <asp:DropDownList ID="ddlVariables" runat="server" multiple="" CssClass="textbox" onchange="CopiarTexto()">
    </asp:DropDownList>
</div>

<script>
    CKEDITOR.replace('cphMain_CtlEditDocument_FreeTextBox2');
    function CopiarTexto() {
        var aux = document.createElement("input");
        aux.setAttribute("value", $("#cphMain_ddlVariables option:selected")[0].value);
        document.body.appendChild(aux);
        aux.select();
        document.execCommand("copy");
        document.body.removeChild(aux);
        toastr.success('debe pegarlo en el texto.', 'Campo Copiado', { timeOut: 2000 });
    }
</script>