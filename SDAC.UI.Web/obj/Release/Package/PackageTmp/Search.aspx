<%@ Page Language="C#" AutoEventWireup="true" Title="Welcome to SDAC" Inherits="SDAC.UI.Web.Search" MasterPageFile="StudentAggregate.Master" Codebehind="Search.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JS/analytics.js" type="text/javascript"></script>

  <script src="js/jquery.js" type="text/javascript"></script>
    <script src="JS/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="JS/jquery-ui.js" type="text/javascript"></script>
    <link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />     

  
    <script src="js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/GridviewFix.js" type="text/javascript"></script> 
    <link href="CSS/demo_table.css" rel="stylesheet" type="text/css" />  
    <%--<link href="CSS/demo_table_jui.css" rel="stylesheet" type="text/css" />
    <link href="CSS/demo_page.css" rel="stylesheet" type="text/css" />--%>

<script type="text/javascript">
    $("#search").addClass('rollover');
</script> 

<script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
    function endRequestHandle(sender, Args) {



        var droplistflagcnt = 0;

        //Use the class you used for the drop down here, I used ‘custom-opt’
        $('#dropDownListFlag option').each(function () {

            if ($(this).attr("class") == 'abc') {
                if (droplistflagcnt == 1) {
                    $(this).replaceWith('</optgroup>');
                    droplistflagcnt = 0;
                }
                else {

                    droplistflagcnt = 1;
                }

                var label = $(this).text();
                $(this).replaceWith("<optgroup label='" + label + "'>");

            }

        });

    }


    </script>

<script  type="text/javascript">

    function abc() {

        alert($(this).val());
        event.preventDefault();

    }
    

	</script>   

  
    <title>Search Page</title>
    
    <asp:Panel ID="PanelSearch" runat="server">
    

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


                 <!-- Search Penal Starts -->
                 <div class="contain_size">
                        <div class="search_penal searchpageflagcss">
                                <p class="mar_right searchpageflagcssp"><asp:CheckBox ID="CheckBoxPublicFlag" runat="server" Text="Show Public Flags"  AutoPostBack="true" oncheckedchanged="CheckBoxPublicFlag_CheckedChanged" Checked="True" ClientIDMode="Static" /></p>
                                <div class="blue">
                                        <ul>
                                            <li><a href="AddFlag.aspx" title="New Flag"><span>New Flag</span></a></li>
                                            <li><a href="Aggregate.aspx" title="Aggregate Flag"><span>Aggregate Flag</span></a></li>
                                        </ul>
                                </div>
                        </div>
                </div>
                <!-- Search Penal Starts -->

                <!-- Table Start -->
                <div class="contain_size">
	                    <div class="table_style">
                    
                            <asp:GridView ID="gridViewFlag" runat="server" AutoGenerateColumns="False" CssClass="display"  ClientIDMode="Static"   
                                RowStyle-CssClass="rowStyle" HeaderStyle-CssClass="headerStyle" FooterStyle-CssClass="footerStyle"  DataKeyNames="FlagId" 
                                onrowcommand="gridViewFlag_RowCommand" onrowdatabound="gridViewFlag_RowDataBound"  >
                                <Columns>
                                                     
                                        <asp:BoundField DataField="FlagName" HeaderText="Name" ReadOnly="True" SortExpression="FlagName" />
            
                                        <asp:BoundField DataField="FlagDescription" HeaderText="Description" ReadOnly="True" SortExpression="FlagDescription" />              

                                        <asp:BoundField DataField="Keyword" HeaderText="Keyword" ReadOnly="True" SortExpression="Keyword" >
               
                                        <HeaderStyle Width="150px" />
                                        </asp:BoundField>
               
                                        <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="alignCenter" ItemStyle-Width="175px" >                         
                                                    <ItemTemplate >
                                                            <%--<a href="Result.aspx?id=<%#Eval("FlagId") %>"><img class="play" src="Images/blank.gif" title="Run Flag"/></a>--%>
                                                            <asp:ImageButton ID="ImageBtnRun" CssClass="play" runat="server"  CommandName="CmdRunFlag" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Run Flag" ImageUrl="~/Images/blank.gif" />
                                                            <asp:ImageButton ID="ImgBtnCopy" CssClass="list" runat="server"  CommandName="CmdCopyFlag" CommandArgument='<%#Eval("FlagId") %>' ToolTip="Copy Flag" ImageUrl="~/Images/blank.gif" />
                                                            <asp:ImageButton ID="ImgBtnEdit" CssClass="pin" runat="server" CommandName="CmdEditFlag" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Edit Flag" ImageUrl="~/Images/blank.gif" />
                                                            <asp:ImageButton ID="ImgBtnFav" CssClass="fav" runat="server"  CommandName="CmdFavorite" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="~/Images/blank.gif"  />                                            
                                                            <asp:ImageButton ID="ImgBtndelete" CssClass="delete" runat="server" CommandName="CmdDelete" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Delete Flag" OnClientClick="return confirm('Are you sure you want to delete this flag?');" ImageUrl="~/Images/blank.gif" />                                           
                                            
                                                    </ItemTemplate>
                                                                                                       
                                                    <HeaderStyle CssClass="action" />
                                                                                                       
                                                    <ItemStyle Width="175px" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="IsPublic" HeaderText="IsPublic" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >                                                       
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="IsFavorite" DataField="IsFavorite" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" >                                                        
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                                                SortExpression="CreatedDate" >                                                       
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>
                                                
                                        <asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" 
                                                SortExpression="Type" >                                              
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" /> 
                                        </asp:BoundField> 
                                        <asp:BoundField DataField="FlagId" HeaderText="FlagId" ReadOnly="True" SortExpression="FlagId" >
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="IsAdmin" HeaderText="IsAdmin" ReadOnly="True" SortExpression="IsAdmin" >
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" SortExpression="UserId" >
                                                <HeaderStyle CssClass="hideGridColumn" />
                                                <ItemStyle CssClass="hideGridColumn" />
                                        </asp:BoundField>

                                </Columns>
                                <FooterStyle CssClass="footerStyle" />
                                <HeaderStyle CssClass="headerStyle" />
                                <RowStyle CssClass="rowStyle" />
                            </asp:GridView>   
                                    
   
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
                <asp:AsyncPostBackTrigger ControlID="gridViewFlag" EventName="RowCommand"></asp:AsyncPostBackTrigger>      
                <asp:AsyncPostBackTrigger ControlID="CheckBoxPublicFlag" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>                
        </Triggers>
          
                                     
    </asp:UpdatePanel> 


 <script type="text/javascript">
     $(document).ready(function () {
         //initTable();
         // tableActions();
         $("#gridViewFlag").GridviewFix().dataTable({
             "bFilter": true,
             "bStateSave": true,
             "bPaginate": true,
             "sPaginationType": "full_numbers",
             "aoColumns": [
                  null,
                  null,
                  null,
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false, "bSortable": false }
                ]

         });


     });
    </script>

     <!-- Table End -->

 </asp:Panel> 

<script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
    function endRequestHandle(sender, Args) {
        $("#gridViewFlag").GridviewFix().dataTable({
            "bFilter": true,
            "bStateSave": true,
            // "bJQueryUI": true,
            "bPaginate": true,
            "sPaginationType": "full_numbers",
            "aoColumns": [
                  null,
                  null,
                  null,
                  { "bSearchable": false, "bSortable": false },
                  { "bSearchable": false },
                  { "bSearchable": false },
                  { "bSearchable": false },
                  { "bSearchable": false },
                  { "bSearchable": false },
                  { "bSearchable": false },
                  { "bSearchable": false }
                ]

        });

    }   


</script>


</asp:Content>


