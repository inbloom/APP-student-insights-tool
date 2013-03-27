var DataTpe = "";

$(document).ready(function () {
    if ($("#hdnDataType").val() != "")
        DataTpe = $("#hdnDataType").val();
    else
        DataTpe = $("#hdDatatpe").val();

    if (typeof ($("#txtSetVal")) != "undefined") {
        var txtSetVal = $("#txtSetVal").val();
        var txtSetVal2 = $("#txtSetVal2").val();

        $("#txtSetVal").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: "1Y"
        });
        $("#txtSetVal").datepicker("option", "dateFormat", "yy-mm-dd");

        $("#txtSetVal2").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: "1Y"
        });
        $("#txtSetVal2").datepicker("option", "dateFormat", "yy-mm-dd");

        $("#txtSetVal").val(txtSetVal);
        $("#txtSetVal2").val(txtSetVal2);
    }

	/*$("#flag_name").each(function () {
    });*/

    $(".overlay").fancybox({
        'titlePosition': 'inside',
        'transitionIn': 'none',
        'transitionOut': 'none',
        'overlayColor': '#fff',
        'overlayOpacity': 0.5
    });

    $(".GridResult tr").live("click", function (event) {

        var DataElementId = $(this).find("td:nth-child(1)").html();
        var DataElementName = $(this).find("td:nth-child(2)").html();
        DataElementName = DataElementName.replace("&nbsp;", " ");
        DataTpe = $(this).find("td:nth-child(4)").html();
        $('.abc').html(DataElementName);
        $("#hdnDataElementId").val(DataElementId);
        $('#hdnDataType').val(DataTpe);
        $('#autocomplete').val(DataElementName);
        $('#entity').html(DataElementName);
        $('#entity').show();
        $('.abc').html(DataElementName);
        $("#lstCondition").removeAttr('selected');
        $("#lstCondition option:selected").attr("selected", false)
        $('#lstCondition option').each(function () {
            $(this).css("display", "block");
        });
        if (DataTpe == "int") {
            $('#lstCondition option').each(function () {
                switch ($(this).text()) {
                    case "Starts with":
                        $(this).css("display", "none");
                        break;
                    case "Does not start with":
                        $(this).css("display", "none");
                        break;
                    case "Ends with":
                        $(this).css("display", "none");
                        break;
                    case "Does not end with":
                        $(this).css("display", "none");
                        break;
                    case "Contains":
                        $(this).css("display", "none");
                        break;
                    case "Does not contain":
                        $(this).css("display", "none");
                        break;
                }
            });
        }
        else if (DataTpe == "string") {
            $('#lstCondition option').each(function () {
                switch ($(this).text()) {

                    case "Is greater than":
                        $(this).css("display", "none");
                        break;
                    case "Is greater than or equal to":
                        $(this).css("display", "none");
                        break;
                    case "Is less than":
                        $(this).css("display", "none");
                        break;
                    case "Is less than or equal to":
                        $(this).css("display", "none");
                        break;
                    case "Is between":
                        $(this).css("display", "none");
                        break;
                    case "Is not between":
                        $(this).css("display", "none");
                        break;
                }
            });
        }
        else if (DataTpe == "boolean") {
            $('#lstCondition option').each(function () {
                switch ($(this).text()) {
                    case "Is greater than":
                        $(this).css("display", "none");
                        break;
                    case "Is not one of":
                        $(this).css("display", "none");
                        break;
                    case "Is one of":
                        $(this).css("display", "none");
                        break;
                    case "Is greater than or equal to":
                        $(this).css("display", "none");
                        break;
                    case "Is less than":
                        $(this).css("display", "none");
                        break;
                    case "Is less than or equal to":
                        $(this).css("display", "none");
                        break;
                    case "Starts with":
                        $(this).css("display", "none");
                        break;
                    case "Does not start with":
                        $(this).css("display", "none");
                        break;
                    case "Ends with":
                        $(this).css("display", "none");
                        break;
                    case "Does not end with":
                        $(this).css("display", "none");
                        break;
                    case "Contains":
                        $(this).css("display", "none");
                        break;
                    case "Does not contain":
                        $(this).css("display", "none");
                        break;
                    case "Is between":
                        $(this).css("display", "none");
                        break;
                    case "Is not between":
                        $(this).css("display", "none");
                        break;
                    case "Is not one of":
                        $(this).css("display", "none");
                        break;
                }
            });
        }
        else if (DataTpe == "date") {
            $('#lstCondition option').each(function () {
                switch ($(this).text()) {
                    case "Starts with":
                        $(this).css("display", "none");
                        break;
                    case "Does not start with":
                        $(this).css("display", "none");
                        break;
                    case "Ends with":
                        $(this).css("display", "none");
                        break;
                    case "Does not end with":
                        $(this).css("display", "none");
                        break;
                    case "Contains":
                        $(this).css("display", "none");
                        break;
                    case "Does not contain":
                        $(this).css("display", "none");
                        break;
                }
            });
        }
        else if (DataTpe == "double") {
            $('#lstCondition option').each(function () {
                switch ($(this).text()) {
                    case "Starts with":
                        $(this).css("display", "none");
                        break;
                    case "Does not start with":
                        $(this).css("display", "none");
                        break;
                    case "Ends with":
                        $(this).css("display", "none");
                        break;
                    case "Does not end with":
                        $(this).css("display", "none");
                        break;
                    case "Contains":
                        $(this).css("display", "none");
                        break;
                    case "Does not contain":
                        $(this).css("display", "none");
                        break;
                }
            });
        }
    });
    $("#lstCondition").click(function () {
        //var val = $("#lstCondition").val();
        var val = $("#lstCondition option:selected").text();
        if (val == "Is between" || val == "Is not between") {
            // $("#txtSetVal2").val("");
            $("#txtSetVal2").show();
            //$("#val2").html("");
            $("#val2").show();
        }
        else {
            $("#txtSetVal").val("");
            $("#txtSetVal2").val("");
            $("#txtSetVal2").hide();
            $("#val2").hide();
            $("#val1").html("");
            $("#val2").html("");
        }
        if (val == "Is blank or empty" || val == "Is not blank nor empty") {
            $("#txtSetVal").hide();
            $("#txtSetVal2").hide();
            $('#txtSetVal').text('');
            $('#txtSetVal2').text('');
            $("#val1").html("");
            $("#val2").html("");
        } else {
            $("#txtSetVal").show();
        }
        showCondition();
    });
    $('#btnSave').live('click', function () {
        return validation();
    });
    $('#btnPreview').live('click', function () {
        return validation();
    });
    $("li#logOut").click(function () {
        //alert("logout");
    });
    $("#lstCondition").click(function () {
        //var val = $("#lstCondition").val();
        var val = $("#lstCondition option:selected").text();
        if (val == "Is between" || val == "Is not between") {
            // $("#txtSetVal2").val("");
            $("#txtSetVal2").show();
            //$("#val2").html("");
            $("#val2").show();
        }
        else {
            $("#txtSetVal").val("");
            $("#txtSetVal2").val("");
            $("#txtSetVal2").hide();
            $("#val2").hide();
            $("#val1").html("");
            $("#val2").html("");
        }
        if (val == "Is blank or empty" || val == "Is not blank nor empty") {
            $("#txtSetVal").hide();
            $("#txtSetVal2").hide();
            $('#txtSetVal').text('');
            $('#txtSetVal2').text('');
            $("#val1").html("");
            $("#val2").html("");
        } else {
            $("#txtSetVal").show();
        }
        showCondition();
    });
    $("#txtSetVal").click(function (e) {
        CheckHideUIDatePicker();
    });
    $("#txtSetVal").focus(function (e) {
        CheckHideUIDatePicker();
    });
    $("#txtSetVal").bind("contextmenu", function (e) {
        if (DataTpe == "date") {
            return false;
        }
    });
    $('#txtSetVal').bind("paste", function (e) {
        if (DataTpe == "date")
            e.preventDefault();
    });
    $("#txtSetVal").change(function (e) {
        var val = $("#txtSetVal").val();
        if (val == null || val == "") {
            $("#val1").hide();
        }
        else {
            showValue1();
        }
    });
    $("#txtSetVal").keypress(function (e) {
        if (DataTpe == "date") {
            e.preventDefault();
        }
    });
    $("#txtSetVal").keyup(function (e) {
        //var k = String.fromCharCode(e.keyCode);
        var val = $("#txtSetVal").val();
        if (val == null || val == "") {
            $("#val1").hide();
        }
        else {
            showValue1();
        }
    });
    $("#txtSetVal2").click(function (e) {
        CheckHideUIDatePicker();
    });
    $("#txtSetVal2").focus(function (e) {
        CheckHideUIDatePicker();
    });

    $("#txtSetVal2").bind("contextmenu", function (e) {
        if (DataTpe == "date")
            return false;
    });

    $('#txtSetVal2').bind("paste", function (e) {
        if (DataTpe == "date")
            e.preventDefault();
    });

    $("#txtSetVal2").change(function (e) {
        if (DataTpe == "date") {
            e.preventDefault();
            return false;
        }

        var val = $("#txtSetVal2").val();
        if (val == null || val == "") {
            $("#val2").hide();
        }
        else {
            val = " & " + val;
            $("#val2").html(val);
            $("#val2").show();
        }
    });
    $("#txtSetVal2").keypress(function (e) {
        if (DataTpe == "date") {
            e.preventDefault();
        }
    });
    $("#txtSetVal2").keyup(function (e) {
        var val = $("#txtSetVal2").val();
        if (val == null || val == "") {
            $("#val2").hide();
        }
        else {
            val = " & " + val;
            $("#val2").html(val);
            $("#val2").show();
        }
    });
});


function DisplayErrorPopup(Message) {
    $('#inlinepop-up').empty();
    $('#inlinepop-up').append(Message);
    callFancy('#inlinepop-up');
}

function callFancy(my_href) {
    var j1 = document.getElementById("hiddenclicker");
    j1.href = my_href;
    $('#hiddenclicker').trigger('click');
}

function abc() {
    var count = 0;
    $("#lstcategory option").each(function (e) {
        count = count + 1;
    });
    //count=$("#HiddenFieldFlagCount").val();
    $('#gridStudentInfo tr input:checkbox').each(function () {
        if (this.checked) {
            count = count + 1;
        }
    });
    if (count > 5) {
        DisplayErrorPopup("You can select only 5 flags.");
        return false;
    }
    return true;
}

function showCondition() {
    var entity = $('#entity').html();
    if (entity == null || entity == "") {

    }
    else {
        var val = $("#lstCondition option:selected").text();
        $("#condition").html(val);
        $("#condition").show();
    }
}

function showValue1() {
    var condition = $("#condition").html();
    if (condition == null || condition == "") {
    }
    else {
        var val = $("#txtSetVal").val();
        $("#val1").html(val);
        $("#val1").show();
    }
}

function gridValidation() {
    var DataElementId = $("#hdnDataElementId").val();
    //alert(DataElementId);
    if (DataElementId == null || DataElementId == "") {
        $("#gridValidation").html(" <label style='color:red;'>Required Field</label>");
        return false;
    }
    else {
        $("#gridValidation").html("");
        return true;
    }
}

function ChangeRowColor(rowID) {
    var row = $("#" + rowID);
	$('#GridResult tr').each(function () {
        $(this).removeClass('selectedBgColor');
    });
    $("#txtSetVal").val('');
    $("#txtSetVal2").val('');
    $("#entity").html('');
    $("#condition").html('');
    $("#val1").html('');
    $("#val2").html('');
    $(row).addClass('selectedBgColor');
}

function getLength(field, maxlimit, countfield) { 
    if (field.value.length > maxlimit) {
        field.value = field.value.substring(0, maxlimit);
    }
    else
        countfield.innerHTML = maxlimit - field.value.length + ' characters remaining';
}

function textupper() {
	document.form1.text1.value = document.write(document.form1.text1.value.toUpperCase());
}

function showfavmsg() {
    $("#favorite").show();
    $("#favorite").fadeOut(2000);
}

function showunfavmsg() {
    $("#unfavorite").show();
    $("#unfavorite").fadeOut(2000);
}

function checkTest(e) {
    var ClientMessage = "";
    var count = 0;
    var chkLength = $("input[id*='chkBoxSelect']:checked").length;
	//  chkLength = chkLength - 1;    
    if (chkLength > 5) {
        ClientMessage += "<p>A maximum of 5 flags may be selected.</p><br />";       
        $(e).attr('checked', false);
        DisplayErrorPopup(ClientMessage);
        return false;
    }

    var c = $("#lstcategory").find("option").length;
    var count = chkLength + c;
    if (count > 5) {
        ClientMessage += "<p>A maximum of 5 flags may be selected.</p><br />";
        $(e).attr('checked', false);
        DisplayErrorPopup(ClientMessage);
        return false;
    }


}

function checkMax() {
   
    var dd = 0;
    var ClientMessage = "";
    // var chkLength = $(':checkbox:checked').length - 1;
    var chkLength = $("input[id*='chkBoxSelect']:checked").length;
    var a = $("#lstcategory").find("option").length;



    if (chkLength == 0) {
        DisplayErrorPopup("No flags were selected. Select one or more flags then click right arrow.");
        return false;

    }
    var dd = a + chkLength;
    if (dd > 5) {
        dd = dd - 1;
        ClientMessage += "<p>A maximum of 5 flags may be selected.</p><br />";
        DisplayErrorPopup(ClientMessage);
        return false;
    }
    var listlength = 0;
    var listlength = $("#lstcategory").find("option").length;
    var c = $("#lstcategory").length;
    var cb = $("#lstcategory").find("option").length + 1;
    if (cb > 5) {
        var cb = $("#lstcategory").find("option").remove;
        return false;              
        alert("max 5 elements here");
    }

}

function ValidateListBox() {

    var ListBox = document.getElementById('<%=lstcategory.ClientID %>');
    var length = ListBox.length;
    var i = 0;
    var SelectedItemCount = 0;
    if (ListBox.length == 0)
        DisplayErrorPopup("No items in listbox.");

    for (i = 0; i < length; i++) {
        if (ListBox.options[i].selected) {
            SelectedItemCount = SelectedItemCount + 1;
        }

        if (SelectedItemCount < 1) {
            DisplayErrorPopup('Please select the flag from the list.');
            return false;
        }
    }

  
    function checklist() {

        var dd = 0;
     
        // var chkLength = $(':checkbox:checked').length - 1;
        var chkLength = $("input[id*='cblList']:checked").length;
       // var a = $("#lstcategory").find("option").length;

        alert(chkLength);


    }




    function blank(a) { if (a.value == a.defaultValue) a.value = ""; }
    function unblank(a) { if (a.value == "") a.value = a.defaultValue; }

    

}




function validation() {
    var dataTp = $("#hdnDataType").val();

    var txtFlagName1 = $("#txtFlagName").val();
    var ClientMessage = "";    
    var txtDescription1 = $.trim($("#txtDescription").val());

    //  var dataTp = $("#hdnDataType").val();

    // Validation for Flag Name.
    if (txtFlagName1 == null || txtFlagName1 == "" || txtFlagName1.match("Enter a name for the flag")) {

        ClientMessage = "<p>Please enter flag name.</p><br />";
    }
    // Validation forDescription.
    if (txtDescription1 == null || txtDescription1 == "" || txtDescription1.match("Enter a detailed description about the Flag.")) {

        ClientMessage += "<p>Please enter flag description.</p><br />";
    }
    //alert(ClientMessage);


    var selected = $("#lstCondition").find(':selected').text();
    if (selected == null || selected == "") {
        ClientMessage += "<p>Please select the condition.</p><br />";
    }


    var txtSetVal = $.trim($("#txtSetVal").val());
    var val = $("#lstCondition option:selected").text();
    if (val != "Is blank or empty" && val != "Is not blank nor empty") {

        if (txtSetVal == null || txtSetVal == "") {
            $('#txtSetVal').val("");
            ClientMessage += "<p>Please enter a value.</p><br />";
        }

    }
    var DataElementId = $("#hdnDataElementId").val();
    if (DataElementId == null || DataElementId == "") {
        $("#gridValidation").html(" <label style='color:red;'>Required Field</label>");
        ClientMessage += "<p>Please select the data element.</p><br />";
    }



    //validation for date
    if (dataTp == 'date') {

        ClientMessage += "";
        var date = document.getElementById("txtSetVal").value;
        var date1 = document.getElementById("txtSetVal2").value;
        var val = $("#lstCondition option:selected").text();

        if (val != "Is blank or empty" && val != "Is not blank nor empty") {

            if (date == " " || date == null || !date.toString().match(/^[0-9]{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])/)) {
                ClientMessage += "<p>Please enter a date using YYYY-MM-DD format in input field.</p><br />";
            }

        }

        if (val == "Is between" || val == "Is not between") {
            /*   if (date1 == "" || date1 == null || !date1.toString().match(/^[0-9]{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])/)) {
            ClientMessage += "<p>Please enter a date using YYYY-MM-DD format in Second input field.</p><br />";
            }*/

            if (date == null || !date.toString().match(/^[0-9]{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])/) || date1 == null || !date1.toString().match(/^[0-9]{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])/)) {
                ClientMessage += "<p>Please enter a date using YYYY-MM-DD format in both input field.</p><br />";
            }

            if (date > date1) {

                ClientMessage += "<p>Please enter first date value less than second date value.</p><br />";
                ///return false;
            }
        }

        else {
            ClientMessage += "";
        }

    }

    //validation to check integer value
    if (dataTp == "int") {
        var var1 = document.getElementById("txtSetVal").value;

        var var2 = document.getElementById("txtSetVal2").value;
        var val = $("#lstCondition option:selected").text();

        if (val != "Is blank or empty" || val != "Is not blank nor empty") {
            if (var1 == null || !var1.toString().toString().match(/^[-]?\d*\.?\d*$/)) {
                ClientMessage += "<p>Please enter numbers only.</p><br />";
            }

        }

        if (val == "Is between" || val == "Is not between") {
            if (var1 == null || !var1.toString().toString().match(/^[-]?\d*\.?\d*$/) || var2 == "" || var2 == null || !var2.toString().match(/^[-]?\d*\.?\d*$/)) {
                ClientMessage += "<p>Please enter numbers only in second input field.</p><br />";
            }

            else if (var1 == null || !var1.toString().match(/^[-]?\d*\.?\d*$/) && var2 == null || !var2.toString().match(/^[-]?\d*\.?\d*$/)) {
                ClientMessage += "<p>Please enter numbers only in both input field.</p><br />";
            }

        }

        else {
            ClientMessage += "";
        }

    }


    //validatin to check string
    if (dataTp == "string") {
        var var1 = document.getElementById("txtSetVal").value;
        var var2 = document.getElementById("txtSetVal2").value;
        //  var pattern = /^[0-9a-bA-B]+$/;
        var val = $("#lstCondition option:selected").text();
        if (val != "Is blank or empty" && val != "Is not blank nor empty") {


            if (var1 == null || !var1.toString().match(/\b([A-Za-z]+)\b/)) {
                ClientMessage += "<p>Please enter characters only.</p><br />";

            }
        }


        else if (val == "Is between" || val == "Is not between") {
            var val = $("#txtSetVal2").val();

            if (var2 == null || !var2.toString().match(/\b([A-Za-z]+)\b/)) {
                ClientMessage += "<p>Please enter a value in the second input field.</p><br />";
            }
        }
        else {
            ClientMessage += "";
        }


    }


    //validation to check boolean
    if (dataTp == "boolean") {
        var var1 = document.getElementById("txtSetVal").value;
        var val = $("#lstCondition option:selected").text();
        var1 = var1.toUpperCase();
        if (val != "Is blank or empty" && val != "Is not blank nor empty") {


            if (var1 == null || !var1.toString().match("TRUE") && !var1.toString().match("TRUE")) {
                ClientMessage += "<p>Please enter either True or False only.</p><br />";

            }
        }
        else {
            ClientMessage += "";

        }

    }

    //validation to check double
    if (dataTp == "double") {
        var val = $("#lstCondition option:selected").text();

        var var1 = document.getElementById("txtSetVal").value;
        var var2 = document.getElementById("txtSetVal2").value;

        var regx = /^(\+?((([0-9]+(\.)?)|([0-9]*\.[0-9]+))([eE][+-]?[0-9]+)?))$/;

        if (val != "Is blank or empty" && val != "Is not blank nor empty") {

            if (var1 == "" || var1 == null || !var1.toString().match(/^(\+?((([0-9]+(\.)?)|([0-9]*\.[0-9]+))([eE][+-]?[0-9]+)?))$/)) {
                ClientMessage += "<p>Please enter numbers or decimals only.</p><br />";
            }

        }

        if (val == "Is between" || val == "Is not between") {
            if (var2 == null || !var2.toString().match(/^(\+?((([0-9]+(\.)?)|([0-9]*\.[0-9]+))([eE][+-]?[0-9]+)?))$/)) {
                ClientMessage += "<p>Please enter numbers or decimals only in second input field.</p><br />";
            }

            else if (var1 == null && !var1.toString().match(/^(\+?((([0-9]+(\.)?)|([0-9]*\.[0-9]+))([eE][+-]?[0-9]+)?))$/) && var2 == null && !var2.toString().match(/^(\+?((([0-9]+(\.)?)|([0-9]*\.[0-9]+))([eE][+-]?[0-9]+)?))$/)) {
                ClientMessage += "<p>Please enter numbers only in both input field.</p><br />";
            }

        }

        else {
            ClientMessage += "";
        }

    }



    else {
        $("#gridValidation").html("");
    }


    var val = $("#lstCondition option:selected").text();
   
    if (val == "Is between" || val == "Is not between") {
        var firstVal = $("#txtSetVal").val();
        var secVal = $("#txtSetVal2").val();
        if (parseInt(firstVal) > parseInt(secVal)) {
            ClientMessage += "<p>Range values must be entered so that smaller values precede larger values.  Please switch the order of these values.</p><br />";
            ///return false;
        }
    }


    if (ClientMessage == "") {
        return true;
    }
    else {
        DisplayErrorPopup(ClientMessage)
        return false;
    }

}

// Opens date picker only for date type
function CheckHideUIDatePicker() {
    if (DataTpe != "date") {
        $("#ui-datepicker-div").hide();
    }
}