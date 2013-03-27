<%@ Page Language="C#" AutoEventWireup="True" Inherits="SDAC.UI.Web.Result" MasterPageFile="StudentAggregate.Master"
    Title="SDAC >> Results" CodeBehind="Result.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="js/jquery.js" type="text/javascript"></script>
    <script src="JS/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="JS/jquery-ui.js" type="text/javascript"></script>
    <link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    
    
                
    <script src="js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/GridviewFix.js" type="text/javascript"></script> 
    <link href="CSS/demo_table.css" rel="stylesheet" type="text/css" />  

<script type="text/javascript">
    $("#result").addClass('rollover');

</script> 




<style type="text/css">

.dataTables_scrollHead, .dataTables_scrollBody
{
overflow: visible !important;
}

.dataTables_scroll
{
	overflow: auto;
}

</style>


<script type="text/javascript">
    function pageLoad(sender, args) {

    }
    
 </script>

<script type="text/javascript">

    var val = "";
    $(document).ready(function () {
        $('#cbl').hide();

        $("#cbl").mouseout(function () {
            $('#cbl').hide();
        });


        $(".MultipleSelectionDDL").mouseover(function () {
            $("#cbl").show();
        });

    });



    $(document).click(function (event) {

    });

    function toggleList() {
        $('#cbl').toggle();

    }

    function hideDrop() {
        $('#cbl').hide();

    }
        

</script>


 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

         <!-- Search Penal Starts -->
 <div class="contain_size">       
    
           <div class="risk_failure">
                <div class="risk_title"><asp:Label ID="lblFlagName" runat="server" Text=""></asp:Label></div>
                <div class="risk_description"><asp:Label ID="lblFlagDescription" runat="server" Text=""></asp:Label></div>
                <div style="clear:both;"></div>
           </div>
            
           <div class="clearboth"></div>
            
          
                      <div class="actions">
                               
                               <div class="favrite">
                                        <asp:LinkButton ID="btnlnkfavorite" runat="server" 
                                     onclick="btnlnkfavorite_Click" OnClientClick="showfavmsg();">Add to favorite</asp:LinkButton>
                               </div>
                               
                               <div class="export_result">
                                        <asp:LinkButton ID="btnlnkexport" runat="server" onclick="btnlnkexport_Click"> Export Results</asp:LinkButton>
                               </div>
                               
                               <div class="copy_flag">
                                        <asp:LinkButton ID="btnlnkcopy" runat="server" 
                                     onclick="btnlnkcopy_Click">Copy Flag</asp:LinkButton>
                               </div>
                               
                               <div class="edit_flag">
                                        <asp:LinkButton ID="btnlnkeditflag" runat="server" 
                                     onclick="btnlnkeditflag_Click">Edit Flag</asp:LinkButton>
                               </div>
                               
                               
                    </div>


                     <div style="float:right; margin-top: 10px; width:400px;" class="showHide">
                                        <div id="divDDL" class="divDDL" runat="server" onclick="javascript:toggleList();">
                                                <img src="Images/dropdown.png" alt="dropdown"  style="padding-top: 4px;"/>   
   
                                        </div>                                    
                                       <div style="float:right;margin-top:-34px">
                                            <asp:Button ID="btnUpdate" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn_small" onclick="btnUpdate_Click"   /> 
                                       </div>

                                       <asp:Panel ID="pnlDDL" runat="server" CssClass="MultipleSelectionDDL">
                                            <div id="cbl" class="multiselect">
                                                <asp:CheckBoxList ID="cblList" runat="server"  ClientIDMode="Static">
              
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>
                                        <br />
                     </div>

                   

           

            <div class="clearboth"></div>

    </div>
    <!-- Search Penal Starts -->

     <!-- Table Start -->
        <div class="contain_size">
	        <div class="table_style">
                
          
            			<div class="adtofavmag" id="showmsg">Added to Favorites Successfully!</div>
                        <div class="clearboth"></div>

                         
              
               <asp:GridView ID="gvResult" runat="server" ClientIDMode="Static" CssClass="display dbtbll" RowStyle-CssClass="rowStyle"
                    GridLines="Vertical" Width="1000px" class="tblhead" HeaderStyle-CssClass="headerStyle" FooterStyle-CssClass="footerStyle"
                    cellpadding="0" AutoGenerateColumns="False" >
                       
                        <Columns>
                         
                           <asp:BoundField AccessibleHeaderText="Student ID" HeaderText="Student.Student Unique State Id" DataField="Student ID" SortExpression="Student ID" >
                            <HeaderStyle CssClass="" />
                            <ItemStyle CssClass="" />
                            </asp:BoundField>

                            
                                                   
                            <asp:BoundField DataField="Student.First Name" HeaderText="Student.First Name" >
                            <ItemStyle CssClass="bod_none test" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Student.Middle Name" HeaderText="Student.Middle Name" />
                            <asp:BoundField DataField="Student.Last Surname" HeaderText="Student.Last Surname" />

                            <asp:BoundField AccessibleHeaderText="Name" HeaderText="FieldName" DataField="GPA" SortExpression="GPA" >
                            <HeaderStyle CssClass="bod_none test" />
                            <ItemStyle CssClass="bod_none test" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Student.City" HeaderText="Student.City" />
                            <asp:BoundField DataField="Student.Name Of Country" HeaderText="Student.Name Of Country" />
                            <asp:BoundField DataField="Student.Apartment Room Suite Number" HeaderText="Student.Apartment Room Suite Number" />
                            <asp:BoundField DataField="Student.Street Number Name" HeaderText="Student.Street Number Name" />
                            <asp:BoundField DataField="Student.Postal Code" HeaderText="Student.Postal Code" />
                            <asp:BoundField DataField="Student.State Abbreviation" HeaderText="Student.State Abbreviation" />
                            <asp:BoundField DataField="Student.Address Type" HeaderText="Student.Address Type" />
                            <asp:BoundField DataField="Student.Email Address" HeaderText="Student.Email Address" />
                            <asp:BoundField DataField="Student.Email Address Type" HeaderText="Student.Email Address Type" />
                            <asp:BoundField DataField="Student.Telephone Number" HeaderText="Student.Telephone Number" />
                            <asp:BoundField DataField="Student.Telephone Number Type" HeaderText="Student.Telephone Number Type" />
                            <asp:BoundField DataField="Student.Sex" HeaderText="Student.Sex" />
                            <asp:BoundField DataField="Student.Hispanic Latino Ethnicity" HeaderText="Student.Hispanic Latino Ethnicity" />
                            <asp:BoundField DataField="Student.Birth Date" HeaderText="Student.Birth Date" />
                            <asp:BoundField DataField="Student.Id" HeaderText="Student.Id" />
                            <asp:BoundField DataField="Student.Limited English Proficiency" HeaderText="Student.Limited English Proficiency" />
                            <asp:BoundField DataField="Student.Entity Type" HeaderText="Student.Entity Type" />
                           

                            <asp:BoundField DataField="Parent.First Name" HeaderText="Parent.First Name" >
                            <HeaderStyle CssClass="" />
                            <ItemStyle CssClass="bod_none test" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parent.Middle Name" HeaderText="Parent.Middle Name" />
                            <asp:BoundField DataField="Parent.Last Surname" HeaderText="Parent.Last Surname" /> 
                            <asp:BoundField DataField="Parent.City" HeaderText="Parent.City" />
                            <asp:BoundField DataField="Parent.Name Of Country" HeaderText="Parent.Name Of Country" />
                            <asp:BoundField DataField="Parent.Apartment Room Suite Number" HeaderText="Parent.Apartment Room Suite Number" />
                            <asp:BoundField DataField="Parent.Street Number Name" HeaderText="Parent.Street Number Name" />
                            <asp:BoundField DataField="Parent.Postal Code" HeaderText="Parent.Postal Code" />
                            <asp:BoundField DataField="Parent.State Abbreviation" HeaderText="Parent.State Abbreviation" />
                            <asp:BoundField DataField="Parent.Address Type" HeaderText="Parent.Address Type" />
                            <asp:BoundField DataField="Parent.Email Address" HeaderText="Parent.Email Address" />
                            <asp:BoundField DataField="Parent.Email Address Type" HeaderText="Parent.Email Address Type" />
                            <asp:BoundField DataField="Parent.Telephone Number" HeaderText="Parent.Telephone Number" />
                            <asp:BoundField DataField="Parent.Telephone Number Type" HeaderText="Parent.Telephone Number Type" />
                            <asp:BoundField DataField="Parent.Sex" HeaderText="Parent.Sex" />
                            <asp:BoundField DataField="Parent.Parent Unique State Id" HeaderText="Parent.Parent Unique State Id" />                          
                            <asp:BoundField DataField="Parent.Id" HeaderText="Parent.Id" />                           
                            <asp:BoundField DataField="Parent.Entity Type" HeaderText="Parent.Entity Type" />
                             


                                                   
                        </Columns>

<RowStyle CssClass="rowStyle"></RowStyle>

                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />

<FooterStyle CssClass="footerStyle"></FooterStyle>

                        <HeaderStyle BackColor="Black" Font-Bold="True" />
                    </asp:GridView>
                        
                        
                  
                   </div>
            </div>

                  

                    <div class="contain_size">
	        <div class="table_style">

                     <asp:GridView ID="GridViewAggregateFlag" runat="server" CssClass="display"  
                         ClientIDMode="Static"  RowStyle-CssClass="rowStyle"
                    GridLines="Vertical" Width="1000px" class="tblhead" 
                         HeaderStyle-CssClass="headerStyle" FooterStyle-CssClass="footerStyle"
                    cellpadding="0" AutoGenerateColumns="True" 
                         onrowdatabound="GridViewAggregateFlag_RowDataBound">
                    

                    </asp:GridView>
    
     <asp:HiddenField ID="hdnField" runat="server" ClientIDMode ="Static" />

    <script type="text/javascript">

        $(document).ready(function () {




            $("#gvResult").GridviewFix().dataTable({

                "bFilter": false,
                "bStateSave": true,
                "bPaginate": true,

                "sScrollX": "100%",
                "sScrollXInner": "110%",

                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ Students "
                },
                "sPaginationType": "full_numbers"
            });

            //showScrollToRunFlagGrid();

            $("#GridViewAggregateFlag").GridviewFix().dataTable({
                "bFilter": false,
                "bPaginate": true,

                "sScrollX": "100%",
                "sScrollXInner": "110%",
                "oLanguage": {
                    "sInfo": "Showing _START_ to _END_ of _TOTAL_ Students "
                },
                "sPaginationType": "full_numbers"
            });



        });

      

    </script>
        

            
            </div>
		</div>
 <!-- Table End -->

   <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: transparent; opacity: 0.7;">
                                        <asp:Image ID="imgUpdateProgress" runat="server"  ImageUrl="Images/loading.gif"  AlternateText="Loading ..." ToolTip="Loading ..." Height="32px" Width="32px"  style="padding: 10px;position:fixed;top:45%;left:50%;" />
                                    </div>
                            </ProgressTemplate>                                
                    </asp:UpdateProgress>

                    </ContentTemplate>

                     <Triggers>            
                            <asp:AsyncPostBackTrigger ControlID="btnlnkfavorite" EventName="Click"></asp:AsyncPostBackTrigger>      
                            <asp:PostBackTrigger ControlID="btnlnkexport"></asp:PostBackTrigger>  
                            <asp:AsyncPostBackTrigger ControlID="btnlnkcopy" EventName="Click"></asp:AsyncPostBackTrigger> 
                           
                            <asp:AsyncPostBackTrigger ControlID="btnlnkeditflag" EventName="Click"></asp:AsyncPostBackTrigger>   
                                        
                    </Triggers>
            </asp:UpdatePanel>

   <script type="text/javascript">
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
       function endRequestHandle(sender, Args) {

           $('#cbl').hide();

           $("#gvResult").GridviewFix().dataTable({
               "bFilter": false,

               "bStateSave": true,
               "bPaginate": true,

               "sScrollX": "100%",
               "sScrollXInner": "110%",
               "oLanguage": {
                   "sInfo": "Showing _START_ to _END_ of _TOTAL_ Students "
               },
               "sPaginationType": "full_numbers"
           });

           //showScrollToRunFlagGrid();

           $("#GridViewAggregateFlag").GridviewFix().dataTable({
               "bFilter": false,
               "bPaginate": true,

               "sScrollX": "100%",
               "sScrollXInner": "110%",
               "oLanguage": {
                   "sInfo": "Showing _START_ to _END_ of _TOTAL_ Students "
               },
               "sPaginationType": "full_numbers"
           });



           $(document).click(function (event) {
               var id = "" + event.target.id;


               if (id == "" || id.indexOf("cblList") == 0 || id.indexOf("cblList") != -1) {
               }
               else {
                   hideDrop();
               }

               $("#cbl").mouseout(function () {
                   $('#cbl').hide();
               });


               $(".MultipleSelectionDDL").mouseover(function () {
                   $("#cbl").show();
               });

           });

           function toggleList() {

               $('#cbl').toggle();

           }

           function hideDrop() {
               $('#cbl').hide();
           }

       }
   </script>
       



</asp:Content>
    
