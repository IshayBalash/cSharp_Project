using BOL;
using System;
using System.Collections.Generic;


namespace SearchForFilesProject
{
   public static class Functions
    {   
        /// <summary>
        /// prints the option menu
        /// </summary>
        /// <returns>1/2/3- if the user input is valid. 4- if the user input isnt valid</returns>
        public static int PrinttManual()
        {
            Console.WriteLine("Welcome to the search for file app. how can i help you?");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("press --1-- to search a file in the whole file system");
            Console.WriteLine("press --2-- to search a file in a specific folder");
            Console.WriteLine("press --3-- to exit the app.");
            Console.WriteLine("-----------------------------------------------------------");
            bool Isnumber=int.TryParse(Console.ReadLine(), out int UserAnswer);
            if (!Isnumber)
            {
                PrintErrorInputMassage();
                return 4;
            }
            if (UserAnswer > 3 || UserAnswer < 1)
            {
                PrintErrorInputMassage();
                return 4;
            }
            return UserAnswer;
        }


        /// <summary>
        /// print an eroor input massage
        /// </summary>
        static void  PrintErrorInputMassage()
        {
            Console.WriteLine("your input was not on the list");
            Console.WriteLine("press Enter to go back to the main menu");
            Console.ReadLine();
            Console.Clear();

        }

        /// <summary>
        /// sends the user search and the results to the bll,
        /// prints for the user the progress of the db update status and eroors.
        /// </summary>
        /// <param name="Search">the search object</param>
        /// <param name="Results">the list of the search results objects</param>
        public static void SendDataToDB(UserSearch Search, List<SearchResult> Results)
        {
            Console.WriteLine("--------Updating the DB---------");
            if (!BLL.BLL.CheckIfDbConnectionIsValid())
            {
                Console.WriteLine("we cant update the Db... please check the connactions to your DB");
                BackToMainManul();
                return;
            }
            Console.WriteLine("the connections to the DB is valid.... continue..");
            if (!BLL.BLL.SendDataLogic(Search, Results))
            {
                Console.WriteLine("something went bad with updating the DB.....check your search value and the results....");
                BackToMainManul();
                return;
            }
            else
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("the DB was update");
                Console.WriteLine("-----------------------------------\n");
                BackToMainManul();
            }
            
            
        }

        /// <summary>
        ///clear the screen after all data was printed to the user
        /// </summary>
        private static void BackToMainManul()
        {
            Console.WriteLine();
            Console.WriteLine("press enter to go back to main menu");
            Console.ReadLine();
            Console.Clear();
        }



    }






}
