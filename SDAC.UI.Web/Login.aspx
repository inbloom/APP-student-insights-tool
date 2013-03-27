<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SDAC.UI.Web.Login" Title="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<script src="http://code.jquery.com/jquery-latest.js" type="text/javascript"></script>
	<script src="http://jzaefferer.github.com/jquery-validation/jquery.validate.js" type="text/javascript"></script> 
	<script type="text/javascript">
		$(document).ready(function(){
			$("#frmLogin").validate();
		}); 
	</script>	
	<link href="CSS/style.css" rel="stylesheet" type="text/css" />
	<title>Login</title>
</head>
<body>
	<!-- nav Div: Start -->
	<div class="nav">
		<div class="nav_content">
			<div class="logo"></div>
			<div class="logo_name">SDAC</div>
		</div>
	</div>
	<!-- nav Div: End -->
	<!-- Main Login div -->
	<div>
		<form name="frmLogin" id="frmLogin" action="Search.aspx" method="POST">
			<div class="login_box">
				<div class="loginbox2">
					<div class="loginlabel">Username:</div><br/>
					<div> <input type="text" id="loginLogo" name="txtUsername" size="46" class="required logintxtbox" /></div>
					<div class="loginclear"></div>
					<div class="loginlabel">Password:</div><br/>
					<div><input type="password" name="txtPass" id="loginlogo_pass" size="46" class="required logintxtbox"/></div>
					<div class="loginclear"></div>
					<div><input type="submit" name="submit"  value="Login" class="submit uibuttonlogin"/></div>
					<!--
						<form id="form1" runat="server">
							<div align="center">        
								<asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate"></asp:Login>
							</div>
						</form> -->
				</div>
			</div>
		</form>
	</div> <!-- End of Main Login div -->
</body>
</html>