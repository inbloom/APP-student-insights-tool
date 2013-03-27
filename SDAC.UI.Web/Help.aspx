<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="SDAC.UI.Web.Help"   %>


<div id="tabs">
    <ul>
        <li><a href="#tabs-1"><strong>Search</strong></a></li>
        <li><a href="#tabs-2"><strong>Results</strong></a></li>
        <li><a href="#tabs-3"><strong>Add/Edit Flag</strong></a></li>
		<li><a href="#tabs-4"><strong>Aggregate Flag</strong></a></li>
		<li><a href="#tabs-5"><strong>About</strong></a></li>
    </ul>
  
    <div id="tabs-1">


				  <div class="help_heading"> Overview</div>

				<div class="help_heading_content">
				The Search page provides the ability to find an existing flag in the
				system.  Flags you have created, as well as, public flags may appear in the
				list.
				</div>

				<div class="help_heading">  Search Bar</div>
				   
				<div class="help_heading_content">
				 Allows you to search for flags based on the name, description or
				keyword defined for each flag. Once you begin typing in the search field,
				the list of flags will start to filter dynamically based on corresponding
				matches found using the letters or words you type.
				Note: The results will only match on the name, description or keyword
				fields.
				</div>

			 <div class="help_heading">Show Public Flags</div>

				<div class="help_heading_content">


				Check this option to include flags that are defined as public in the search
				results.
				<p>
				Note: Public flags can only be created, edited and deleted by someone 
				with Administrator privileges.</p>

				</div>
				

				 <div class="help_heading">Actions</div>
				<div class="help_heading_content">

				For each flag that appears in the Search list, the following actions may be
				performed by clicking on the corresponding icon.

				</p>
				<img class="actionimage"  src="Images/new_run_help.png" title="Run Flag"/> Run the Flag
				</p>
				<img class="actionimage"src="Images/copy_II_help.png" title="Copy Flag" /> Copy the Flag
				</p>
				<img class="actionimage" src="Images/edit_II_help.png" title="Edit Flag" /> Edit the Flag
				</p>
				<img class="actionimage" src="Images/star_help.png" title="Add to Favorite"/> Save the Flag to Favorites
				</p>
				<img class="actionimage" src="Images/new-trash1_help.png" title="Delete Flag"/> Delete the Flag
				</p>
				</div>
				

				 <div class="help_heading"> New Flag</div>

				<div class="help_heading_content">
				<a href="AddFlag.aspx"><img src="Images/newflag.png" /></a><br /> 
				   
				 Click this button to create a new Flag. You will be redirected to the Add Flag page. 
				</div>
			
				 <div class="help_heading"> New Aggregate Flag </div>
				 <div class="help_heading_content">
				<a href="Aggregate.aspx"><img src="Images/aggregate.png" /></a> <br /> Click this button to create a new Aggregate Flag. You will be redirected to the Add Aggregate Flag page.
				 </div>

				

    </div>
    <div id="tabs-2">
   	 <div class="help_heading">   Overview</div>
 <div class="help_heading_content">
The Results page displays a list of students based on the criteria defined for the selected flag.
  The name of the flag and description are prominently displayed above the list of students.
   The information displayed in the table includes Student ID, 
   Student Name, Ethnicity, Gender, Grade Level and the field selected when the user created the flag.  
   </div>

 <div class="help_heading">Actions</div>
<div class="help_heading_content">

Several additional options are available on the Results page including:
<br />
<br />
<img src="Images/addtofav.png" /><br /> <br />
Adds this flag to your list of favorites
<br /><br />
<img src="Images/export_help.png" /><br /> <br />
Outputs the data displayed in the grid to a formatted Excel spreadsheet including column headings
<br /><br />
<img src="Images/copy_helpimage.png" /><br /><br />
Opens the Add/Edit page with pre filled information from the flag allowing you to create a copy of the flag
<br /><br />

<img src="Images/edit_helpimage.png" /><br /><br />
Opens the Add/Edit page allowing you to make changes to the current flag
<br />
<br />

<div class="help_heading">Add Fields</div>
<p>Adding student information to the results of a flag is a simple process.</p>
    <p><img src="Images/add_fields.png" /></p>
<div class="help_heading_content">
<p>Click on the Add New Fields to Results drop down and you will see a list of available fields.  Select the fields by clicking on the checkbox next to a field name.  To deselect a field, simply uncheck the field.</p>

<p>The current version of the SDAC provides the ability to add fields from the Student Identification and Demographics group only.  The list of fields selected are not saved with the flag.  Therefore, you will need to add fields to the results each time you run the flag.</p>

<img src="Images/btn_update.png" />
<p>Once you have selected the fields from the drop down, click the Update button to refresh the list of students.  Once updated, the table will display the additional columns selected.</p>

</div>
<br />
 <div class="help_heading">Notes</div>
 <div class="help_heading_content">
<i>1. Results are limited based on School, Course and Section selected</i><br />
<i>2. Each column in the data grid can be sorted by clicking on the column header</i>
</div>




</div>



    </div>
    <div id="tabs-3">
          <div class="help_heading"> Overview</div>

    <div class="help_heading_content">
   <p>
The Add/Edit page allows you to add or edit a flag using a visually engaging step-by-step approach.
 Prior to defining a flag, you must enter a Name and Description for the flag. 
  You may also enter optional Keywords to add further relevance for each flag and to better assist in the search of flags. 
  <br />  <br />
If you are logged on as a District Admin, the option to set a flag as Public will be made available.
</p>
</div>

<div class="help_heading"> Adding a New Flag</div>
<div class="help_heading_content">

The process for adding a new flag is as follows:

<br />
<br />
<div class="help_heading">Step 1: Select a Data Element</div><br />
<div class="help_heading_content">

The user begins by typing the word that describes the name of the data element they are interesting in using. 
 This causes the list to begin filtering data elements so only the most relevant data elements appear in the list.  
 Once the user clicks on the data element it will appear highlighted and display in the Preview section indicating the field is now selected.
The list of data elements is also sortable by Name or Description columns.  
If the user chooses to do so, they may scroll through the list and select a data element.
</div>
<br />

<div class="help_heading">Step 2: Apply a Condition</div><br />
<div class="help_heading_content">

Depending on the data type of the data element selected, the application will automatically 
disable operators in the list that are invalid for that data type.  For example, when a user selects 
a numeric data type such as GPA, the Starts with, Does not start with, Ends with and Does not end with 
operators will be grayed out.  This will enhance the user experience by eliminating invalid selections.
<br />

The following operators are available for selection.  Only one operator may be selected at a time.
<br />
<br />
<table border="0">
<tr><td>Is equal to </td><td>Starts with</td><td>Is between</td></tr>
<tr><td>Is not equal to</td><td>Does not start with</td><td>Is not between</td></tr>
<tr><td>Is greater than</td><td>Ends with</td><td>Is one of</td></tr>
<tr><td>Is greater than or equal to </td><td>Does not end with</td><td>Is not one of</td></tr>
<tr><td>Is less than</td><td>Contains</td><td>Is blank or empty</td></tr>
<tr><td>Is less than or equal to</td><td>Does not contain</td><td>Is not blank nor empty</td></tr>
</table>
<br />
</div>
<div class="help_heading">Step 3: Set the Value</div><br />
<div class="help_heading_content">

Enter a value in the textbox which will determine the output of the flag.

The application will perform basic validation to ensure input values match the data type of the data element selected. 
 For example, if you select Enrollment Date, the textbox will become a date picker.  Another example, if you select Rank,
  the textbox will only allow numbers to be entered.
  <br /><br />

</div>
<div class="help_heading">Step 4: Preview and Save</div><br />
<div class="help_heading_content">
As you progress from Step 1 through Step 3, the Preview pane continually updates and displays the selected data element,
 operator and values entered.  This provides a visual representation of the flag definition before clicking the Save button.

<div class="buttons">
<input class="btn_small" type="button" value="Preview" name="">
</div>
<br />

Once the flag is defined, click the Preview button to executive the flag.
 The application will retrieve 5 records for you to view in the preview data grid. 
  This ensures you have set the flag as intended and will aid you from having to click Save and run the flag continually in a separate screen.  

<div class="buttons">
<input class="btn_small" type="button" value="Save" name="">
</div>
<br />
You can continue modifying the flag and click Save once you are satisfied the flag has been defined correctly.

<br /><br />

<i>Note 1: The Flag Name must be unique within your list of defined flags.  
Public Flags must also be unique but only within the domain of Public Flags.
<br /><br />
Note 2: The Copy Flag feature is similar to the Edit Flag feature except in that you must define a new Name for the flag.</i>
    </div>
    </div>
    </div>
    <div id="tabs-4">
      

  <div class="help_heading"> Overview</div>

    <div class="help_heading_content">
    <p>
An Aggregate Flag is the combination of multiple flags defined as either private (created by you) or Public (created by an Admin user) and which allows you to perform advanced analysis of student data.  By aggregating individual flags, you can identify students with very specific qualities or factors.  As an example, you may wish to create an Aggregate Flag called High Performing Underprivileged Students that consists of the following flags:
</p>

<ul>
<li>Honor Roll</li>
<li>Free and Reduced Lunch</li>
<li>English Language Learner</li>
</ul>


<p>

The application will perform the necessary aggregation of each data set for the individual flags resulting in the list of students that meet these collective criteria.  The results of an Aggregate Flag only includes students that are inclusive of each individual flag.  Using the example above, the High Performing Underprivileged Students flag will only list students that appear in the Honor Roll AND Free and Reduced Lunch AND English Language Learner flags.
</p>
</div>

  <div class="help_heading">Adding a New Aggregate Flag</div>
      <div class="help_heading_content">
  <p>The steps required to create an Aggregate Flag are as follows.</p>
<ol>
<li><p>Enter a Name for the flag.</p></li>
<li><p>Enter a Description for the flag.</p></li>
<li><p>Enter optional Keywords to add further relevance for each flag and to better assist in the search of flags.</p></li>
<li><p>If you are logged on as a District Admin, the option to set a flag as Public will be made available.</p> </li>
<li><p>Select flags from the list using one of the following methods</p></li>
<ul>
<li><p><strong>Search:</strong> Search for flags based on the name, description or keyword defined for each flag. Once you begin typing in the search field, the list of flags will start to filter dynamically based on corresponding matches found using the letters or words you type.    </p></li>
<li><p><strong>Drag and Drop:</strong> Select a flag from the list of flags and move it to the list on the right by clicking on the right arrow or by dragging and dropping the flag.</p></li>
</ul>
</ol>

<br />

<img src="Images/save_help.png" alt="savehelp"/><br />
<p>Click Save once you are satisfied the Aggregate Flag has been defined correctly.</p>


<p><i>Note: Check the Show Public Flags option to include flags that are defined as public in the search results.</i></p>
</div>
  <div class="help_heading">Run the Aggregate Flag</div>
     <div class="help_heading_content">
 
  <p>Once an Aggregate Flag is saved, you may access and view the flag by using one of the following methods:</p>
<ul>
<li>Select the flag from the My Flags drop down</li>
<li style="float: left;">Locate flags on the Search page which have the following icon &nbsp;&nbsp;<div style="float: right; margin-top: -4px;"><img src="Images/aggregate_list.png" alt="aggregate help" /></div></li></ul>
<br />
 <br />

<i>
<p>
Note: This version of the SDAC limits the number of individual flags that can be added to an Aggregate to five. When combining some flags, the application may prompt you that the selected flags may not be combined due to the source of each flags data element.</p> </i>
    </div>
    </div>
	<div id="tabs-5">
		<div class="help_heading">About</div>
		<div class="help_heading_content">
			<p>Student Data Aggregation Calculator was developed for inBloom, Inc. by Upeo, LLC (<a href="http://www.upeo.com/" target="_blank">www.upeo.com</a>).  This software is made available as open-source under the Apache License, Version 2.0.</p>
			<p>For application support, please contact your school district technology support center.<p>
		</div>
	</div>
</div>