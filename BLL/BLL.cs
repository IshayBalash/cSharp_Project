using System.Collections.Generic;
using BOL;
using DAL;

namespace BLL
{
    public static class BLL
    {
        /// <summary>
        /// activate the DBmanager-check if the connection to the DB is valid 
        /// </summary>
        /// <returns>true if it does, false if it doesnt</returns>
        public static bool CheckIfDbConnectionIsValid()
        {
            return DBmanager.CheckIfTheConnectionValid();
        }


        /// <summary>
        /// this functions update and modify the data in the DB using the DBManager class in the DAL
        /// </summary>
        /// <param name="Searchparam">the user Search object</param>
        /// <param name="SearchResultListparam">the list of search rsult objects</param>
        /// <returns></returns>
        public static bool SendDataLogic(UserSearch Searchparam, List<SearchResult> SearchResultListparam)
        {
            ///////////////first STEP- THE SEARCH/////////////////
            //cheek if  there is a search with the param givven in the DB
            int SearchId = DBmanager.GetUserSearchId(Searchparam.SearchName,Searchparam.SearchFolder);
            //if the search exsist update his date
            if (SearchId != 0)
            {
                if (!DBmanager.UpdateSearchDate(SearchId, Searchparam.SearchDate))
                {
                    return false;
                }
            }
            //else create a new search and get his Search id
            else
            {
                //try to insert new value
                if (DBmanager.InsertNewSearch(Searchparam.SearchName, Searchparam.SearchFolder, Searchparam.SearchDate))
                {
                    SearchId = DBmanager.GetUserSearchId(Searchparam.SearchName,Searchparam.SearchFolder);
                }
                //If the insert had faild-stop the process
                else
                {
                    return false;
                }

            }
            ///////////////////////////////////////////////////////////////
            //if there were no searh result the funck ends here
            if (SearchResultListparam.Count == 0)
            {
                return true;
            }
            //////////////////second step the search results list and the connections/////////
            foreach (SearchResult result in SearchResultListparam)
            {
                //get the resalt id if exsist
                int ResultId = DBmanager.GetSearchResultID(result.Path, result.FileName);
                //if it does not exsist create a new one and get his 
                if (ResultId == 0)
                {
                    //if the insert result has faild stop the Program
                    if (!DBmanager.InsertNewSearchResult(result.Path, result.FileName))
                    {
                        return false;
                    }
                    ResultId = DBmanager.GetSearchResultID(result.Path, result.FileName);
                }
                //set the connection between the search and the result
                if (DBmanager.CheckIfConnectionExsist(SearchId, ResultId))
                {
                    continue;
                }
                else
                {
                    if (!DBmanager.InsertNewConnection(SearchId, ResultId))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
