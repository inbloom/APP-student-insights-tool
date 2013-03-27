<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ErrorPage.aspx.cs" Inherits="SDAC.UI.Web.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
	<script src="JS/jquery-1.8.2.min.js" type="text/javascript"></script>
	<script src="JS/jquery-ui.js" type="text/javascript"></script>
	<link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="JS/jquery-ui.min.js"></script> 
	<link href="CSS/style.css" rel="stylesheet" type="text/css" />
	<link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>
	<link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
	<style type="text/css">
		.error_page_content
		{
			margin: 0 30px 16px;
		}
		.error_page_content .errortitle
		{
			color: red;
			font-size: 27px;
		}
        .error_page_content p
		{
			font-size: 16px;
		}
    </style>
	<script type="text/javascript">
		function myfunction() {
			// increase the default animation speed to exaggerate the effect
			$.fx.speeds._default = 200;
			$(function () {
				$("#dialog").dialog({
					autoOpen: false,
					show: "blind",
					width: 800,
					height: 500,
					my: "top",
					at: "top"
				});
				$("#opener").click(function () {
					$("#dialog").dialog("open");
					return false;
					$("#tabs").tabs();
				});
				$("#resultdiv").load("Help.aspx", {}, function (data) {
					var tabs = $('#tabs').tabs();
					var pathname = window.location.pathname;
					$('.ui-tabs-anchor').each(function (i) {
						var ctabid = $(this).attr("href");
						if (pathname.indexOf("ErrorPage.aspx") >= 0 && ctabid == "#tabs-1") {
							$(this).trigger("click");
						}
					});
				});
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<div id="body">
			<div class="nav">
				<div class="nav_content">
					<div class="logo_name"><a href="Search.aspx" class="logolink" > <img src="Images/logo.png" /> </a></div>
					<div class="header_right">
						Welcome <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>       
						<div class="setting" id="user">
							<div class="header_right_img"><img alt="Profile Setting" class="gear" src="Images/directional_down.png"></div>
							<ul class="subnav">
								<li id="logOut"><a href="Login.aspx?LogOut=1">LogOut</a></li>
								<br class="clear">
							</ul>
						</div>
					</div>
				</div>            
			</div>
			<div class="top_shedow"></div>
			<div class="content">
				<div class="top">
					<div class="size">
						<div class="heading"><h1>Student Data Aggregation Calculator</h1></div>
						<div class="help" id="opener"><a href="javascript:void(0)" onclick="myfunction();" title="Help">Help</a></div>
					</div>
				</div>
				<div style="clear:both;"></div>
				<br /><br />
				<div class="error_page_content">
					<p class="errortitle">Application Error</p>
					<p>The application has to stop due to an error:</p>
					<p><asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label></p>
					<p>Please contact the system administrtor for help.</p>
				</div>
			</div>     
			<div class="bottom_shedow">&nbsp;</div>
			<div id="dialog" title="Help">
				<div id="Div1" title="Help">
					<div id="resultdiv">
					</div>
				</div>
			</div>
			<div class="contain_size footer">
				<div class="half">
					<p>(C) Shared Learning Collaborative, LLC.</p>
				</div>
				<div class="half2">
					<%-- <a href="PrivacyPolicy.aspx" target="_blank" title="">Privacy Policy</a><span>|</span><a href="TermAndCondition.aspx" target="_blank" title="Terms of Use">Terms of Use</a>--%>
				</div>
			</div>
		</div>
	</form>
</body>
</html>