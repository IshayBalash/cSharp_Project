
# c# project



* the task of the project is to search for spesific files in the user computer and then updte a db with the search and the results files.
* the project use the n-tier modle so it is divided into 4 different projects -4 dlls(UIL,BOL,DLL,BLL) and one console app.
* the  DB used in this project is a mysql DB in an open connections format. 

***

## UIL
* Contains the Search files functions

## BOL
* contains two classes 
1. the user search class.
2. the file result class 

## BLL
* contain the conmunicates functions between the program and the DAL.
* activate the functions in the DAL acording to a logical swqwence.

## DAL
* contains the connections string to the local db.
* contains all the CRUD(create,read,update,delete) functions.


## Data base
* the db contains 3 tables.
 1. Search tables.(search name,search path,search date) .
 2. The file results table.(file name, file path)
 3. A connections table between the search table and the file table(search id, resuld id)




