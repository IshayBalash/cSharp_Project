
# c# project



* The task of the project is to search for specific files in the user's computer and then updte a db with the search and the results files.
* The project uses the n-tier module so it is divided into 4 different projects -4 dlls(UIL,BOL,DLL,BLL) and one console app.
* The  DB used in this project is a mysql DB in an open connections format. 

***

## UIL
* Contains the Search files functions.

## BOL
* Contains two classes. 
1. The user search class.
2. The file result class 

## BLL
* Contains the conmunication functions between the program and the DAL.
* Activates the functions in the DAL according to a logical sequence of events.

## DAL
* Contains the connections string to the local db.
* Contains all of the CRUD functions (create, read, update, delete).


## Data base
* The db contains 3 tables.
 1. Search tables (search name, search path, search date).
 2. The file results table(file name, file path).
 3. A connections table between the search table and the file table (search id, result id).




