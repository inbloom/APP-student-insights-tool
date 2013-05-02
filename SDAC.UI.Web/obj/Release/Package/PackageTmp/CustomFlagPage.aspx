<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomFlagPage.aspx.cs" Inherits="SDAC.UI.Web.CustomFlagPage" Title="Custom Flag Page" MasterPageFile="~/StudentAggregate.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JS/analytics.js" type="text/javascript"></script>

	<style type="text/css">
		.customflag
		{
			padding: 0 15px 15px;
		}
	</style>
	<div class="customflag">
		<asp:Button ID="btnPreview" runat="server" Text="Preview" onclick="btnPreview_Click" />
		<br /><br />
		<asp:TextBox ID="TextBoxFlagResponse" runat="server" Height="202px" TextMode="MultiLine" Width="668px"/>
	</div>
</asp:Content>  