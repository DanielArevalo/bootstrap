<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DetallePago.aspx.cs" Inherits="Pagos" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-group">
        
        <asp:Panel ID="pnlInfo" runat="server">
            <span class="glyphicon glyphicon-alert text-green"></span>&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblContent" runat="server" CssClass="text-green"></asp:Label>
        </asp:Panel>

        <div class="col-sm-12" style="padding-bottom: 8px">
            <h4 class="text-primary text-center">RESUMEN DEL PAGO REALIZADO</h4>
            <hr />
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Nombre o Razón Social</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblNomEmpresa" runat="server" style="font-weight:bold; font-size: 16px;"/></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">NIT</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblNitEmpresa" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Identificación</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblIdentificacion" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Nombre</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblNombre" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">E-mail</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblEmail" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Referencia de Pago</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblReferenciaPago" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Fecha de Pago</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblFechaPago" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Dirección IP</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblDireccionIP" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Valor del Pago</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblVrPago" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">Descripción de Pago</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblDescription" runat="server" /></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right"><span class="text-danger" style="font-size: 15px; font-weight:bold">Banco</span></div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblBanco" runat="server" CssClass="text-danger" style="font-size: 16px; font-weight:bold"/></div>
            <div class="col-sm-2"></div>
        </div>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right"><span class="text-danger" style="font-size: 15px; font-weight:bold">Estado de Transacción</span></div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblEstadoTransac" runat="server" CssClass="text-danger" style="font-size: 16px; font-weight:bold"/></div>
            <div class="col-sm-2"></div>
        </div>

        <%--<div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-4 text-right">CUS Código Único de Seguimiento</div>
            <div class="col-sm-5 text-center">
                <asp:Label ID="lblCodigoCUS" runat="server" style="font-weight:bold"/></div>
            <div class="col-sm-2"></div>
        </div>--%>
        <div class="col-sm-12" style="padding-bottom: 15px">
            <div class="col-sm-1"></div>
            <div class="col-sm-9"><span style="font-weight:bold">Para mayor información de su transacción, comuníquese con nosotros o acérquese a nuestras oficinas</span></div>
            <div class="col-sm-2"></div>
        </div>
    </div>
</asp:Content>

