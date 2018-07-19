using System;
using System.Collections.Generic;
using BOL;
using UIL;



namespace SearchForFilesProject
{
    class Program
    {
        
        static void Main(string[] args)
        {
            #region Search object events
            //create The SearClass Object and init all the events
            SearchForFilesClass NewSearch = new SearchForFilesClass();
            NewSearch.WhenFileIsFoundHandler += (SearchResult param) => { Console.WriteLine($"File Found--> {param}"); };
            NewSearch.NoFielsWereFoundHandler += () => { Console.WriteLine("!!no files were found!!\n"); };
            NewSearch.CantAcsesFolderHandler += (string param) => { Console.WriteLine($"!!Eroor!!---cant acses the folder:{param}\n"); };
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion



            //while this parameter is true the progrem is running
            bool ToRunTheLoop = true;
            DateTime Today = DateTime.Today;
            while (ToRunTheLoop)
            {
                //print the menu for the user and act acoording to the value the user enterd
                int UserChoise = Functions.PrinttManual();
                if (UserChoise == 1)
                {
                    Console.WriteLine("enter the name to Search for ");
                    string fileName = Console.ReadLine();
                    UserSearch UserSearch = new UserSearch
                    {
                        SearchName = fileName,
                        SearchDate = Today.ToString("d"),
                        //in case 1- the user search in the whole file sysetm
                        SearchFolder = "File system"
                    };
                    Console.WriteLine();
                    Console.WriteLine(UserSearch);
                    Console.WriteLine();
                    //run the search 
                    List<SearchResult>Results=NewSearch.SearchForFiles(UserSearch.SearchName);
                    if (Results == null)
                    {
                        //if there were no results create an empty list.
                        Results = new List<SearchResult>();
                    }
                    //send the search and results to the BLL
                    Functions.SendDataToDB(UserSearch, Results);
                }
                if (UserChoise == 2)
                {
                    Console.WriteLine("enter the name to Search for");
                    string fileName = Console.ReadLine();
                    Console.WriteLine("enter the folder to search files in");
                    string Folder = Console.ReadLine();
                    //check if the path givven is valid
                    if (!NewSearch.ChekIfFolderExsist(Folder))
                    {
                        Console.WriteLine("path to folder not valid............");
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine("press enter to go back to the main namul");
                        Console.WriteLine("------------------------------------------------------");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                    UserSearch UserSearch = new UserSearch
                    {
                        SearchName = fileName,
                        SearchDate = Today.ToString("d"),
                        //in case 2 the user wish is to search for a file in a spesific folder
                        SearchFolder = Folder
                    };
                    Console.WriteLine();
                    Console.WriteLine(UserSearch);
                    Console.WriteLine();
                    List<SearchResult> Results = NewSearch.SearchForFiles(UserSearch.SearchName,UserSearch.SearchFolder);

                    if (Results == null)
                    {
                        //if there were no results create an empty list.
                        Results = new List<SearchResult>();
                    }
                    //send the search and results to the BLL
                    Functions.SendDataToDB(UserSearch, Results);
                }
                //the user wants to exit the progrem
                if (UserChoise == 3)
                {
                    Console.WriteLine("bye bye have a nice day.......");
                    //stop the loop
                    ToRunTheLoop = false;
                }
                //the user input was not valid. start the process from the start
                if (UserChoise == 4)
                {
                    continue;
                }



            }
        }
    }
}
