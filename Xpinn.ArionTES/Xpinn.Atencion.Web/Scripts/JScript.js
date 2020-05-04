/// Visualiza la imagen de carga para registro o edicion de informacion
function Loading() {
    if (Page_ClientValidate()) {
        var imgLoading = document.getElementById("imgLoading");
        imgLoading.style.visibility = "visible";
    }
}

/// Visualiza la imagen de carga para consulta de informacion
function LoadingList() {
    var imgLoading = document.getElementById("imgLoading");
    imgLoading.style.visibility = "visible";
}

/// Visualiza la imagen de carga para consulta de informacion
function OcultaLoading() {
    var imgLoading = document.getElementById("imgLoading");
    imgLoading.Visible = "false";
}

//function disable(Mask) {
//    var behavior = $find(Mask); // "MaskedEditExtenderEx" - BehaviorID
//    // to prevent the base dispose() method call - it removes the behavior from components list
//    //------------------------------------------------------------------------
////    var savedDispose = AjaxControlToolkit.MaskedEditBehavior.callBaseMethod;
////    AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = function (instance, name) {
////    };
//    //------------------------------------------------------------------------
//    behavior.dispose();
//    // restore the base dispose() method
////    AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = savedDispose;
//}
//function enable() { // enable it again
//    var behavior = $find("MaskedEditExtenderEx"); // "MaskedEditExtenderEx" - BehaviorID
//    behavior.initialize();
//}

//function clearTextBox(textBoxID) {
//    document.getElementById(textBoxID).value = "ss";
//}

function blur7(textbox) {
    var str2 = textbox.value;
    var formateado = "";
    var str1 = parseInt(str2.replace(/\./g, ""));
    if (str1 > 0) {
        var str = str1.toString();
        var long = str.length;
        var cen = str.substring(long - 3, long);
        var mil = str.substring(long - 6, long - 3);
        mill = str.substring(0, long - 6);

        if (long > 0 && long <= 3)
        { formateado = parseInt(cen); }
        else if (long > 3 && long <= 6)
        { formateado = parseInt(mil) + "." + cen; }
        else if (long > 6 && long <= 9)
        { formateado = parseInt(mill) + "." + mil + "." + cen; }
        else
        { formateado = "0"; }
    }
    else {
        var formateado = "0";
    }
    //          document.getElementById('<%= TxtValor3.ClientID %>').value = formateado;  
    document.getElementById(textbox.id).value = formateado;
}



function autoResize(id) {
    var newheight;
    // var newwidth;

    if (document.getElementById) {
        newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
        //  newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
    }

    document.getElementById(id).height = (newheight) + "px";
    //           document.getElementById(id).width = (newwidth) + "px";
}