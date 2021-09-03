# AutomationCSharp

Welcome to This Solution. 

This Solution is Built in .NET Core 5.0 hence it is platform independent and can be used in Windows, Linux and Mac.
This Solution has below feature available:-
1. Browser Screenshots, Desktop Screenshots and FullPage(Scrolling) Screenshots available.
2. Parallel Execution and Cross Browser Execution is Available
3. Extent Reporting with embedded screenshots is available. Reporting is scalable as one can add Excel Reporting or its own custom HTML Reporting
4. Built in POM and Keyword Driven Framework is integrated




Framework Structure:

This Framework has been created with 6 projects
1. Helper - This is an Independent Project having the common functionalities such as Reading Excel, Writing to an Excel File, Connecting to DataBase, Image Manupulation, Logger and accessing appSettings.json.
2. KeywordDrivenProject - This is an Console Application which is dependent on Helpers, RunTimeSettings and SeleniumLibrary projects. This follows Test Creation in Keyword Driven Approach. Testcase is written as a sequence of steps in Excel Sheet and each steps has the Keyword which performs necessary action.
3. NunitTest - This is an Nunit Test Project dependent on Helpers, ReportLibrary, RunTimeSettings and SeleniumLibrary project. This follows the Page Object Approach of Test development
4. ReportLibrary - This project is responsible for Extent Report Generation and it is dependent on Helpers project.
5. RunTimeSettings - This Projects has the class models and Constants which is used accross the framework.]
6. SeleniumLibrary - This project is dependent on Helpers, ReportLibrary and RunTimeSetting. This projects has the Selenium keywords, handles all browser activities(Initializing driver, taking screenshots etc) and has Page Objects which is used in NunitTest project.
 
