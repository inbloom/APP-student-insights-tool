<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditFlag.aspx.cs" Inherits="SDAC.UI.Web.Edit" Title="SDAC >> Edit Flag " MasterPageFile="~/StudentAggregate.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JS/analytics.js" type="text/javascript"></script>

	<script src="JS/jquery-1.8.3.js" type="text/javascript"></script>
	<script src="JS/jquery-ui.js" type="text/javascript"></script>
	<link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		$("#add_edit").addClass('rollover');
	</script>
	<script src="JS/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
	<link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
	  function pageLoad(sender, args) {
		  var lstCondition = '<%=condition.Text %>';
		  if (lstCondition == "Is blank or empty") {
			  $("#txtSetVal").hide();
			  $("#txtSetVal2").hide();
		  } 
	  }
	</script>
	<script type="text/javascript">
		$(document).ready(function () {
			var count = 1;
			var DataElementName = [""];
            $('#GridResult tr').each(function () {
				count = count + 1;
				var name = $(this).find("td:nth-child(2)").html();
				var fieldname = $(this).find("td:nth-child(3)").html();
				if (count > 0) {
					DataElementName[count] = "" + name;
				}
			});

			$('#autoComplete').bind("enterKey", function (e) {
				var val = $("#autoComplete").val();
				val = $.trim(val);
				if (val == null || val == "") {
                    $('#GridResult tr').each(function () {
						$(this).removeClass('selectedBgColor');
						$(this).show();
						//$(this).css('background-color', '#FFFFFF');
					});
					$("#hdnDataElementId").val("");
				}

				count = 1;
				var select = 0;
                $('#GridResult tr').each(function () {
					var name = "" + $(this).find("td:nth-child(2)").html();
					name = name.replace(/\s{2,}/g, ' ');

					var FieldName = "" + $(this).find("td:nth-child(3)").html();
					FieldName = FieldName.replace(/\s{2,}/g, ' ');

					name = "" + name.toLowerCase();
					val = "" + val.toLowerCase();
					var DataElementId = $(this).find("td:nth-child(1)").html();

					//alert(name + " and " + FieldName);
					if (name.indexOf(val) != -1 || FieldName.indexOf(val) != -1 || name.indexOf(val) > -1 || FieldName.indexOf(val) > -1) {
						$(this).show();

					}
					else {
						$(this).hide();
						$(this).css('background-color', '#FFFFFF');
					}

				});
			});

			$('#autoComplete').keyup(function (e) {
				if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
				}
				else
				if (e.keyCode == 13 || e.keyCode == 8) {
					$(this).trigger("enterKey");
				}
			});

			// code to select the row selected by user from search 
			$("#autoComplete").keypress(function (e) {
				if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
				}
				else
				if (e.keyCode != 13) {
					$(this).trigger("enterKey");
				}
			});
		});
	</script>

	<script type="text/javascript">
		function blank(a) { if (a.value == a.defaultValue) a.value = ""; }
		function unblank(a) { if (a.value == "") a.value = a.defaultValue; }
	</script>

	<script type="text/javascript">
		Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
		function endRequestHandle(sender, Args) {

		// for autosearch after selecting new domain from drop down

		$('#autoComplete').bind("enterKey", function(e) {
			var val = $("#autoComplete").val();
			val = $.trim(val);
			if (val == null || val == "") {
                $('#GridResult tr').each(function () {
					$(this).removeClass('selectedBgColor');
					$(this).show();
					//$(this).css('background-color', '#FFFFFF');
				});
				$("#hdnDataElementId").val("");
			}

			count = 1;
			var select = 0;
            $('#GridResult tr').each(function () {
				var name = "" + $(this).find("td:nth-child(2)").html();
				name = name.replace(/\s{2,}/g, ' ');

				var FieldName = "" + $(this).find("td:nth-child(3)").html();
				FieldName = FieldName.replace(/\s{2,}/g, ' ');

				name = "" + name.toLowerCase();
				val = "" + val.toLowerCase();
				var DataElementId = $(this).find("td:nth-child(1)").html();

				//alert(name + " and " + FieldName);
				if (name.indexOf(val) != -1 || FieldName.indexOf(val) != -1 || name.indexOf(val) > -1 || FieldName.indexOf(val) > -1) {
					$(this).show();

				} else {
					$(this).hide();
					$(this).css('background-color', '#FFFFFF');
				}

			});
		});

		$('#autoComplete').keyup(function(e) {
			if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
			} else if (e.keyCode == 13 || e.keyCode == 8) {
				$(this).trigger("enterKey");
			}
		});

		// code to select the row selected by user from search 
		$("#autoComplete").keypress(function(e) {
			if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
			} else if (e.keyCode != 13) {
				$(this).trigger("enterKey");
			}
		});
	}
</script>

	<!-- Add / Edit Penal Starts -->
	<div class="contain_size">
		<div class="box">
			<h3>
				Enter information about the flag you would like to create.</h3>
			<div class="flag_imformation">
				<p>
					Name<span>*</span></p>
				<asp:TextBox ID="txtFlagName" ClientIDMode="Static" class="txtbox" CssClass="textfield" runat="server" Rows="5" Width="447px" MaxLength="100"  onkeyup="getLength(this,100,flag_name_count);" onKeyDown="getLength(this,100,flag_name_count);"  onblur="unblank(this)">Enter a name for the flag </asp:TextBox>
				<p id="flag_name_count" class="warning delta"> 100 Characters remaining</p>
				<p id="P1" class="delta"><asp:RequiredFieldValidator ID="RequiredFieldValidatorFlagName" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtFlagName"></asp:RequiredFieldValidator>  </p>
				<div class="c_both">     &nbsp;</div>
				<p> Description<span>*</span></p><asp:TextBox ID="txtDescription" runat="server" CssClass="textarea" class="txtbox_area" ClientIDMode="Static" TextMode="MultiLine" onKeyDown="getLength(this,500,charCount);" 
 onKeyUp="getLength(this,500,charCount);"  onblur="unblank(this)" Rows="4">Enter a detailed description about the Flag. The first two lines of the text will be displayed as a short description. You can also expand the view to display the full description.</asp:TextBox>
					<p id="charCount" class="warning delta"> 500 Characters remaining</p>
				<p id="P2" class="delta">
					<asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtDescription"></asp:RequiredFieldValidator></p> 
					 <div class="c_both">  &nbsp;</div>
				<p>Keyword</p>
				<asp:TextBox ID="txtFlag" class="txtbox" runat="server" onkeyup="getLength(this,100,flag_keyword_count);" onKeyDown="getLength(this,100,flag_keyword_count);" Width="447px" MaxLength="100" onblur="unblank(this)" CssClass="textfield" ClientIDMode="Static">Enter one or more keywords for the flag (optional)</asp:TextBox> 
				 <p id="flag_keyword_count" class="warning delta">  100 Characters remaining</p>
				<p id="P3" class="delta"> </p>
				<div class="c_both">       &nbsp;</div>
				<asp:Panel ID="PanelFlagType" runat="server">
					<p class="f_left sigma"> Type</p>
					<asp:RadioButtonList ID="radioFlagType" CssClass="f_left tblsss" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Selected="True">Public</asp:ListItem> <asp:ListItem>Private</asp:ListItem>
					</asp:RadioButtonList>
					<p class="alfa">(District Admin User Only)</p>
				</asp:Panel>
			</div>
		</div>
		<div class="box">
			<h3>
				Follow the steps below to setup the criteria for how the flag should function.
			</h3>
		</div>
		<div class="box2">
			<div class="instruction">
				<div class="first">
					<h3>
						Select a Data Element</h3>
				</div>
				<div class="two">
					<h3>
						Apply a Condition</h3>
				</div>
				<div class="third">
					<h3>
						Set the Value</h3>
				</div>
				<div class="forth">
					<h3>
						Preview and Save</h3>
				</div>
			</div>
			<!-- Data Class Start -->
			<div class="data">
				<!-- first_penal Class Start -->
				<div class="first_penal">
					<asp:UpdatePanel ID="UpdatePanel2" runat="server">
						 <ContentTemplate>
					<div class="search_bg2">
						<asp:TextBox ID="autoComplete" autoComplete='off' onkeydown="return (event.keyCode!=13);" runat="server" ClientIDMode="Static" EnableViewState="True"></asp:TextBox>
  
  
  <asp:DropDownList ID="DropDownListEntity" runat="server" AutoPostBack="true"  onselectedindexchanged="DropDownListEntity_SelectedIndexChanged"  CssClass="dropdownentity" >
	  </asp:DropDownList>
							
							
							</div>
							<br />
							<table cellspacing="0" cellpadding="3" rules="cols" style="background-color: White; border-width: 0px; border-collapse: collapse; margin-left: 10px; width: 311px;" class="tblhead GridResult" id="Table1">
<tbody><tr align="left" style="color:White;background-color:Black;height:20px;">
	 <th scope="col" 
		 style="border: 1px Solid #AAAAAA; background-color:#EAEAEA; height:20px; width: 157px; font-weight: normal;padding-left: 10px;" >Field Name</th><th scope="col" style="background-color:#EAEAEA;border-color:#AAAAAA;border-width:1px;border-style:Solid;font-weight: normal;padding-left: 10px;">Sample Data</th>
	</tr>
	</tbody></table>


                    <div class="data_scroll table_style3">
                                          
                       
                            <asp:GridView ID="GridResult" runat="server" AutoGenerateColumns="False" DataKeyNames="DataElementId"
                                    class="tblhead GridResult" CellPadding="3" GridLines="Vertical" OnRowCreated="GridResult_RowCreated"
                                    ClientIDMode="Static" BackColor="White" BorderWidth="0px" OnRowDataBound="GridResult_RowCreated"
                                    Width="309px" Style="border-bottom: 1px solid #AAAAAA" ShowHeader="False">
                                    <HeaderStyle CssClass="" Height="20px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="DataElementId" DataField="DataElementId" SortExpression="DataElementId"
                                            HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                            <HeaderStyle CssClass="hideGridColumn" BackColor="#EAEAEA"></HeaderStyle>
                                            <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Field Name" DataField="FieldName" SortExpression="FieldName">
                                            <HeaderStyle BackColor="#EAEAEA" BorderColor="#AAAAAA" BorderStyle="Solid" BorderWidth="1px"
                                                Width="158px" Height="20px" />
                                            <ItemStyle Width="157px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Sample Data" DataField="SampleData" SortExpression="SampleData">
                                            <HeaderStyle BackColor="#EAEAEA" BorderColor="#AAAAAA" BorderStyle="Solid" BorderWidth="1px"
                                                Width="134px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Data Type" DataField="DataType" SortExpression="DataType"
                                            HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                            <HeaderStyle CssClass="hideGridColumn" BackColor="#EAEAEA" BorderColor="#AAAAAA"
                                                BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                            <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ExternalField" DataField="ExternalField" SortExpression="ExternalField"
                                            HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                            <HeaderStyle CssClass="hideGridColumn" BackColor="#EAEAEA" BorderColor="#AAAAAA"
                                                BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                            <ItemStyle CssClass="hideGridColumn"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                        PageButtonCount="4" />
                                    <PagerStyle BackColor="#E8E8E8" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
                                    <SelectedRowStyle Font-Bold="True" ForeColor="White" CssClass="bgColorRed" Height="100px" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                </asp:GridView>
                        <asp:HiddenField ID="hdnDataElementId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnDataType" runat="server" ClientIDMode="Static" />

					  
					</div>

					 <asp:UpdateProgress ID="UpdateProgress2" runat="server">
				<ProgressTemplate>
						<div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: transparent; opacity: 0.7;">
						<asp:Image ID="imgUpdateProgress2" runat="server"  ImageUrl="Images/loading.gif"  AlternateText="Loading ..." ToolTip="Loading ..." Height="32px" Width="32px"  style="padding: 10px;position:fixed;top:45%;left:50%;" />
						</div>
				</ProgressTemplate>                                
		</asp:UpdateProgress>

					  </ContentTemplate> 
				<Triggers><asp:AsyncPostBackTrigger ControlID="DropDownListEntity" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger></Triggers>                                        
		</asp:UpdatePanel> 

				</div>
				<!-- first_penal Class End  -->
				<!-- second_penal Class Start  -->
				<div class="second_penal">
					<div><asp:RequiredFieldValidator ID="RequiredFieldValidatorListCondition" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ClientIDMode="Static" ControlToValidate="lstCondition"></asp:RequiredFieldValidator></div>
					<asp:ListBox ID="lstCondition" runat="server" DataTextField="ConditionName" DataValueField="ConditionId" 
						ClientIDMode="Static" ViewStateMode="Enabled" Rows="18"></asp:ListBox>
				</div>
				<!-- second_penal Class End  -->
				<!-- third_penal Class Start  -->
				<div class="third_penal">
					<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorSetVal" runat="server" ErrorMessage="Required Field" ForeColor="#FF0066" ControlToValidate="txtSetVal"></asp:RequiredFieldValidator>--%>
							<asp:TextBox ID="txtSetVal" runat="server" ClientIDMode="Static" CssClass="txtbox" autoComplete="off"></asp:TextBox><br />
					<br />
					<asp:TextBox ID="txtSetVal2" runat="server" CssClass="txtbox" ClientIDMode="Static"
						Style="display: none;" autoComplete="off"></asp:TextBox>
				</div>
				<!-- third_penal Class End  -->
				<!-- forth_penal Class Start  -->
				<div class="forth_penal">
					<h4>
						Display students where:</h4>
					<h4>
						<asp:Label ID="entity" runat="server" Text="" ClientIDMode="Static" Style="display: none;" CssClass="ggspan"></asp:Label>
						<asp:Label ID="condition" runat="server" Text="" ClientIDMode="Static" Style="display: none;" CssClass="ggspan"></asp:Label>
						<asp:Label ID="val1" runat="server" Text="" ClientIDMode="Static" Style="display: none;" CssClass="ggspan"></asp:Label>
						<asp:Label ID="val2" runat="server" Text="& " Style="display: none;" ClientIDMode="Static" CssClass="ggspan"></asp:Label>
					</h4>
					<div class="table_style3" style="padding-top: 15px;">
					 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
						<asp:GridView ID="gridDisplayResult" runat="server" AutoGenerateColumns="False" class="tblhead"
							CellPadding="3" GridLines="Vertical"  Width="100%">
							<Columns>
								<asp:BoundField HeaderText="Student Name" DataField="student_Name" SortExpression="student_Name">
									<HeaderStyle CssClass="bod_none" />
									<ItemStyle CssClass="bod_none" />
								</asp:BoundField>
								<asp:BoundField HeaderText="" DataField="GPA" SortExpression="">
									<HeaderStyle HorizontalAlign="Center" CssClass="abc" />
								</asp:BoundField>
							</Columns>
							<FooterStyle BackColor="#CCCCCC" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
							<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
							<HeaderStyle />
							<AlternatingRowStyle BackColor="#CCCCCC" />
						</asp:GridView>
						<div class="buttons">
							<asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="btn_small f_left"
								OnClientClick="javascript:gridValidation(); return validation(); " ClientIDMode="Static" 
								onclick="btnPreview_Click" />
							<asp:Button ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" CssClass="btn_small"
								OnClick="btnSave_Click" OnClientClick="javascript:return validation();" />
						</div>
			 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
				<ProgressTemplate>
						<div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: transperent; opacity: 0.7;">
						<asp:Image ID="imgUpdateProgressedit" runat="server"  ImageUrl="Images/loading.gif"  AlternateText="Loading ..." ToolTip="Loading ..." Height="32px" Width="32px"  style="padding: 10px;position:fixed;top:45%;left:50%;" />
						</div>
				</ProgressTemplate>                                
		</asp:UpdateProgress>

		 </ContentTemplate> 
				<Triggers><asp:AsyncPostBackTrigger ControlID="btnPreview" EventName="Click"></asp:AsyncPostBackTrigger></Triggers>                                        
		</asp:UpdatePanel>


					</div>
				</div>
				<!-- forth_penal Class End  -->
			</div>
			<!-- Data Class End -->
		</div>
		<!-- Box2 Class End -->
	</div>
	<!-- Add / Edit Penal Ends -->
	<asp:HiddenField ID="hdDatatpe" runat="server" ClientIDMode="Static" />
	<div id="hidden_clicker" style="display: none;">
		<a class="overlay" id="hiddenclicker" href="#" title="Please correct the above errors and re-submit.">Inline</a>
	</div>
	<div style="display: none;">
		<div style="" class="formProblemText" id="inlinepop-up">
			<div style="display: none;" class="formProblemText" id="dob_errors">
			</div>
		</div>
	</div>

<script type="text/javascript">
 $(document).ready(function () {

	var fLength = $("#txtFlagName").val().length;
	var dLength = $("#txtDescription").val().length;
	var kLength = $("#txtFlag").val().length;
	var fLengthremain = 100 - fLength;
	var dLengthremain = 500 - dLength;
	var kLengthremain = 100 - kLength;
	 $("#flag_name_count").html(fLengthremain + ' Characters remaining');
	$("#charCount").html(dLengthremain + ' Characters remaining');
	$("#flag_keyword_count").html(kLengthremain + ' Characters remaining');
});

 </script>
</asp:Content>
