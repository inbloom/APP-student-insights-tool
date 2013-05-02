<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyAggregateFlag.aspx.cs" Title="SDAC >> Copy Aggregate Flag" Inherits="SDAC.UI.Web.CopyAggregateFlag" MasterPageFile="~/StudentAggregate.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JS/analytics.js" type="text/javascript"></script>

	<script src="JS/jquery-1.8.3.js" type="text/javascript"></script>
	<script src="JS/jquery-ui.js" type="text/javascript"></script>
	<link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
		$("#aggregate").addClass('rollover');	
    </script> 
	<script type="text/javascript">
	    $(document).ready(function (e) {

        $('#autocomplete').keyup(function (e) {
            if (e.keyCode == 13 || e.keyCode == 8) {
                $(this).trigger("enterKey");
            }
        });


        $("#autocomplete").keypress(function (e) {
            if (e.keyCode != 13) {
                $(this).trigger("enterKey");
            }
        });


        $('#autocomplete').bind("enterKey", function (e) {

            var count = 0;
            var val = $("#autocomplete").val();

            $('#gridStudentInfo tr').each(function () {
                $(this).show();
            });

            if (val == null || val == "") {

            }
            else {
                $('#gridStudentInfo tr').each(function () {

                    var name = "" + $(this).find("td:nth-child(2)").html();
                    var Description = "" + $(this).find("td:nth-child(3)").html();
                    var KeyWords = "" + $(this).find("td:nth-child(4)").html();

                    //alert("name:" + name + " Description:" + Description + " KeyWords:" + KeyWords);
                    //alert("Description:" + Description);
                    //alert("KeyWords:" + KeyWords);

                    name = "" + name.toLowerCase()
                    Description = "" + Description.toLowerCase();
                    KeyWords = "" + KeyWords.toLowerCase();

                    val = val.toLowerCase();

                    if (name.indexOf(val) != -1 || Description.indexOf(val) != -1 || KeyWords.indexOf(val) != -1 ) {

                        count = 1;

                    }
                    else {

                        $(this).hide();

                    }
                });
            }

        });







    });


</script>


<script type="text/javascript">

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

    $(document).ready(function () {

        // clear the element to avoid the problem from pagination but it also clear the id when edit the flag
        //$("#hdnDataElementId").val(""); 



        // Accept only Alphabets for child first name and parent first name.
        /* $('#PrimaryFirstName').bind('keyup blur',function(){
        //$(this).val( $(this).val().replace(/[^A-Za-z_]/g,'') ); }
        $(this).val( $(this).val().replace(/[^-a-zA-Z _]/g,'') ); }
        );
        $('#ParentFirstName').bind('keyup blur',function(){
        $(this).val( $(this).val().replace(/[^-a-zA-Z _]/g,'') ); }
        );
        $('#PromoCode').bind('keyup blur',function(){
        $(this).val( $(this).val().replace(/[^A-Za-z0-9]/g,'') ); }
        );*/

        $('#btnSave').bind('click', function () {
            return validation();
        });

        $('#btnForward').bind('click', function () {
            return checkMax();
        });

        $('#chkBoxSelect').bind('click', function () {
            return checkTest();
        });

        $('#btnBackword').bind('click', function () {
            return ValidateListBox();
        });

        $(".overlay").fancybox({
            'titlePosition': 'inside',
            'transitionIn': 'none',
            'transitionOut': 'none',
            'overlayColor': '#fff',
            'overlayOpacity': 0.5
        });
    });



    function validation() {
        var txtFlagName1 = $("#txtFlagName").val();
        var ClientMessage = "";
        var txtDescription1 = $.trim($("#txtDescription").val());
        //var txtFlag1 = $("#txtFlag").val();

        // Validation for Flag Name.
        if (txtFlagName1 == null || txtFlagName1 == "" || txtFlagName1.match("Enter a name for the flag")) {

            ClientMessage = "<p>Please enter flag name.</p><br />";
        }
        // Validation forDescription.
        if (txtDescription1 == null || txtDescription1 == "" || txtDescription1.match("Enter a detailed description about the Flag.")) {

            ClientMessage += "<p>Please enter flag description.</p><br />";
        }

        /*if (txtFlag1 == null || txtFlag1 == "" || txtFlag1.match("Enter one or more keywords for the flag (optional)")) {

            ClientMessage += "<p>Please enter flag keywords.</p><br />";
        }*/


        var count = 0;
        $("#lstcategory option").each(function (e) {
            count = count + 1;
        });

        //alert(count);
        if (count >= 1 && count <= 5) {
            $("#flaglist").hide();
        }
        else
            if (count > 5) {
                ClientMessage += "<p>A maximum of 5 flags may be selected.</p><br />";
            }
            else {
                $("#flaglist").html("Please select the flag");
                $("#flaglist").show();
                ClientMessage += "<p>Please select flag.</p><br />";
            }

        if (ClientMessage == "") {
            return true;
        }
        else {
            DisplayErrorPopup(ClientMessage)
            return false;
        }



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


        if (count >= 5) {

            DisplayErrorPopup("A maximum of 5 flags may be selected.");
            return false;
        }

        return true;

    }

    function chklistempty() {
        var a = $("#lstcategory").find("option").length;
        var list = document.getElementById('lstcategory');
        var indx = list.selectedIndex;

        if (a == 0) {
            DisplayErrorPopup("No flags were selected. Select one or more flags then click left arrow.");
            return false;
        }
        else
            if (indx == -1) {
                DisplayErrorPopup("No flags were selected. Select one or more flags then click left arrow.");
                return false;
            }
    }



</script>


<script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
    function endRequestHandle(sender, Args) {


        $('#autocomplete').keyup(function (e) {
            if (e.keyCode == 13 || e.keyCode == 8) {
                $(this).trigger("enterKey");
            }
        });


        $("#autocomplete").keypress(function (e) {
            if (e.keyCode != 13) {
                $(this).trigger("enterKey");
            }
        });


        $('#autocomplete').bind("enterKey", function (e) {

            var count = 0;
            var val = $("#autocomplete").val();

            $('#gridStudentInfo tr').each(function () {
                $(this).show();
            });

            if (val == null || val == "") {

            }
            else {
                $('#gridStudentInfo tr').each(function () {

                    var name = "" + $(this).find("td:nth-child(2)").html();
                    var Description = "" + $(this).find("td:nth-child(3)").html();
                    var KeyWords = "" + $(this).find("td:nth-child(4)").html();

                    //alert("name:" + name + " Description:" + Description + " KeyWords:" + KeyWords);
                    //alert("Description:" + Description);
                    //alert("KeyWords:" + KeyWords);

                    name = "" + name.toLowerCase()
                    Description = "" + Description.toLowerCase();
                    KeyWords = "" + KeyWords.toLowerCase();

                    val = val.toLowerCase();

                    if (name.indexOf(val) != -1 || Description.indexOf(val) != -1 || KeyWords.indexOf(val) != -1 || count == 0) {

                        count = 1;

                    }
                    else {

                        $(this).hide();

                    }
                });
            }

        });
    }


        function checkMax() {


            var dd = 0;
            var ClientMessage = "";
            //   var chkLength = $(':checkbox:checked').length - 1;
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


        function chklistempty() {
            var a = $("#lstcategory").find("option").length;
            var list = document.getElementById('lstcategory');
            var indx = list.selectedIndex;

            if (a == 0) {
                DisplayErrorPopup("No flags were selected. Select one or more flags then click left arrow");
                return false;
            }
            else
                if (indx == -1) {
                    DisplayErrorPopup("No flags were selected. Select one or more flags then click left arrow.");

                }
        }

        </script>
    
     <script type="text/javascript">
         function blank(a) { if (a.value == a.defaultValue) a.value = ""; }
         function unblank(a) { if (a.value == "") a.value = a.defaultValue; }
</script>




    <!-- Add / Edit Penal Starts -->
<div class="contain_size">
        <div class="box">
                <h3>Enter information about the flag you would like to create.</h3>
                <div class="flag_imformation">
                        <p>Name<span>*</span></p>
                        <asp:TextBox ID="txtFlagName" ClientIDMode="Static" class="txtbox" CssClass="textfield" runat="server" Rows="5" Width="447px" MaxLength="100"  onkeyup="getLength(this,100,flag_name_count);" onKeyDown="getLength(this,100,flag_name_count);"  onblur="unblank(this)" >Enter a name for the flag </asp:TextBox>
                        <p id="flag_name_count" class="warning delta"> 100 Characters remaining</p>
                        <p id="P1" class="delta"> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFlagName" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtFlagName"></asp:RequiredFieldValidator> 
                        </p>

                        <div class="c_both">&nbsp;</div>

                        <p>Description<span>*</span></p>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textarea" class="txtbox_area" ClientIDMode="Static" TextMode="MultiLine" onKeyDown="getLength(this,500,charCount);" 
 onKeyUp="getLength(this,500,charCount);" Rows="4" onblur="unblank(this)" >Enter a detailed description about the Flag. The first two lines of the text will be displayed as a short description. You can also expand the view to display the full description.</asp:TextBox>
                        <p id="charCount" class="warning delta">500 Characters remaining</p>
                        <p id="P2" class="delta">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server"  ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                        </p>

                        <div class="c_both">&nbsp;</div>

                        <p>Keyword</p>
                        <asp:TextBox ID="txtFlag" class="txtbox" runat="server" onkeyup="getLength(this,100,flag_keyword_count);" onKeyDown="getLength(this,100,flag_keyword_count);"  Width="447px" MaxLength="100" onblur="unblank(this)" CssClass="textfield" ClientIDMode="Static" >Enter one or more keywords for the flag (optional)</asp:TextBox>
                        <p id="flag_keyword_count" class="warning delta">100 Characters remaining</p>
                        <p id="P3" class="delta">
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorKeyword" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtFlag"></asp:RequiredFieldValidator> --%>
                        </p>

                        <div class="c_both">&nbsp;</div>

                        <asp:Panel ID="PanelFlagType" runat="server">
                                <p class="f_left sigma">Type</p>
                                <asp:RadioButtonList ID="radioFlagType" CssClass="f_left tblsss" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Public</asp:ListItem>
                                            <asp:ListItem>Private</asp:ListItem>
                                </asp:RadioButtonList>
                                <p class="alfa">(District Admin User Only)</p>
                        </asp:Panel>

                </div>
        </div>
    <div class="box">
    <h3>Select up to 5 flags to be included in the aggregate flag.  Use the arrows or just drag and drop to add or remove flags. </h3>
</div>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>

    <div class="search_penal">
                <div class="search_bg"><input id="autocomplete" title="type&quot;a&quot;"></div>
                <p class="mar_right" style="float:left;">
                        <asp:CheckBox ID="CheckBoxShowTag" runat="server" Text="Show Public Flags" Checked="true" oncheckedchanged="CheckBoxShowTag_CheckedChanged" AutoPostBack="true" />
                </p>
    </div>

    <div class="left_table">

       <table cellspacing="0" cellpadding="3" rules="cols" style="  border: 1px solid #AAAAAA; width: 650px; border-bottom:none;" class="tblhead GridResult" id="Table1">
<tbody><tr align="left" style="color:White;background-color:Black;height:20px;">
     <th scope="col" class="copy_aggregate_id" ></th>
     <th scope="col" class="copy_aggregate_name">Flag Name</th>
     <th scope="col" class="copy_aggr_descr" >Description</th>
     <th scope="col" class="copy_aggr_key" >Keyword</th>
    </tr>
    </tbody></table>


            <div class="table_style" style="height: 220px;  margin: -1px 0 0;overflow:scroll;border:1px solid #ccc;  ">
                <asp:Panel ID="Panel1" runat="server">
                            <asp:GridView ID="gridStudentInfo" runat="server" AutoGenerateColumns="False" 
                                ClientIDMode="Static" class="tblhead" CellPadding="0"  GridLines="Vertical"  
                                style="border-bottom:1px solid #AAAAAA" ShowHeader="False">
                                 <HeaderStyle CssClass="" Height="20px" />
                                 <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                        <asp:CheckBox ID="chkBoxSelect" runat="server" ClientIDMode="Static"  onclick="checkTest(this);"/>                                                  
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="bod_none" Width="24px" />
                                                <ItemStyle CssClass="bod_none" Width="20px" />
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Name" DataField="FlagName" SortExpression="FlagName"  >
                                                <HeaderStyle CssClass="bod_none abc" Width="114px"  />
                                                <ItemStyle CssClass="bod_none" Width="109px" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Description" DataField="FlagDescription" 
                                                SortExpression="FlagDescription" >
                                                <HeaderStyle CssClass="bod_none" Width="324px"  />
                                                <ItemStyle CssClass="bod_none" Width="320px"  />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Keyword" DataField="Keyword" SortExpression="Keyword" >
                                                <HeaderStyle CssClass="bod_none abc" Width="120px" BorderColor="#AAAAAA" 
                                                BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemStyle CssClass="bod_none" Width="95px"  />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="FlagId" DataField="FlagId" SortExpression="FlagId" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >
                                                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>
                                                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="IsPublic" DataField="IsPublic" SortExpression="IsPublic" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >
                                                <HeaderStyle CssClass="hideGridColumn"></HeaderStyle>
                                                <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                            </asp:BoundField>

                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle  />
                                    <AlternatingRowStyle CssClass="t1" />
                            </asp:GridView>
                </asp:Panel>
            </div>
    </div>

    <div class="move_buttons">
        <p> <asp:Button ID="btnForward" runat="server" Text="" CssClass="nextbutton" onclick="btnForward_Click" OnClientClick="javascript:return checkMax();" /> </p>
        <p><asp:Button ID="btnBackword" CssClass="back"  runat="server" Text="" onclick="btnBackword_Click" OnClientClick="javascript:return chklistempty();" /></p>
    </div>

    <div class="right_penal">
            <div id="flaglist"></div>
            <asp:ListBox ID="lstcategory" runat="server" Height="246px" Width="225px" ClientIDMode="Static" SelectionMode="Multiple" CssClass="listbox_style" style="font-size:12px" > </asp:ListBox>
            <div class="buttons">
                    <asp:Button ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" CssClass="btn_small" onclick="btnSave_Click" OnClientClick="javascript:return validation();"  />
            </div>
    </div>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: transparent; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server"  ImageUrl="Images/loading.gif"  AlternateText="Loading ..." ToolTip="Loading ..." Height="32px" Width="32px"  style="padding: 10px;position:fixed;top:45%;left:50%;" />
                    </div>
            </ProgressTemplate>                                
    </asp:UpdateProgress>

</ContentTemplate>

 <Triggers>            
            <asp:AsyncPostBackTrigger ControlID="btnForward" EventName="Click"></asp:AsyncPostBackTrigger>     
            <asp:AsyncPostBackTrigger ControlID="btnBackword" EventName="Click"></asp:AsyncPostBackTrigger>  
            <asp:AsyncPostBackTrigger ControlID="CheckBoxShowTag" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>           
</Triggers>  




</asp:UpdatePanel>

    </div>



<div id="hidden_clicker" style="display:none;">
        <a class="overlay" id="hiddenclicker" href="#" title="Please correct the above errors and re-submit.">Inline</a>
</div>

<div style="display: none;">
        <div style="" class="formProblemText" id="inlinepop-up">
                <div style="display:none;" class="formProblemText" id="dob_errors"> </div>
        </div>
</div>

   
<script type = "text/javascript">
    $(document).ready(function () {

        var fLength = $("#txtFlagName").val().length;
        var dLength = $("#txtDescription").val().length;
        var kLength = $("#txtFlag").val().length;

        var fLengthremain = 100 - fLength;
        var dLengthremain = 500 - dLength;
        var kLengthremain = 100 - kLength;
        //  alert(kLength);
        $("#flag_name_count").html(fLengthremain + ' Characters remaining');
        $("#charCount").html(dLengthremain + ' Characters remaining');
        $("#flag_keyword_count").html(kLengthremain + ' Characters remaining');
    });
                   
 </script>
           
  </asp:Content>
