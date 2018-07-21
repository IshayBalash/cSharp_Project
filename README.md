
# c# project

***

* the task of the project is to search for spesific files in the user computer and then updte a db with the search and the results files.
* the project use the n-tier modle so it is divided into 4 different projects -4 dlls(UIL,BOL,DLL,BLL) and one console app.
* the  DB used in this project is a mysql DB in an open connections format. 


## UIL
* Contains the Search files functions

## BOL
* contains two classes 
* the user search class.
* the file result class 

## BLL
* contain the conmunicates functions between the program and the DAL.
* activate the functions in the DAL acording to a logical swqwence.

## DAL
* contains the connections string to the local db.
* contains all the CRUD(create,read,update,delete) functions.


## Data base
** the db contains 3 tables.
* 1 Search tables-contins the search info.
* 2 The file results table.
* 3 A connections table betwwn the search table and the file table




