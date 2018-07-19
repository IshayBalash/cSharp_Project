using System.Data.SqlClient;

namespace DAL 
{
      public static class DBmanager
    {
        private static string ConnectionString ="Data Source=DESKTOP-8FERJR4\\SQL_SERVER;Initial Catalog=SearchForFilesProjectDB;Integrated Security=True";
        private static SqlConnection Sql = null;


        /// <summary>
        /// check if the connections to the Db is valid.
        /// </summary>
        /// <returns>true-the connection is good /false-the connections is bad </returns>
        public static bool CheckIfTheConnectionValid()
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// search for the Search Id object ib the DB
        /// </summary>
        /// <param name="UserSearch">the file name of the search</param>
        /// <param name="SearchPath">the path of the search</param>
        /// <returns>in case the serach exsist on the DB returns his ID in case it doesnt resturn 0</returns>
        public static int GetUserSearchId(string UserSearch,string SearchPath)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand Query = new SqlCommand($"select SearchId from UserSearches where SearchName='{UserSearch}'and SearchFolder='{SearchPath}'", Sql);
                    int Result = (int)Query.ExecuteScalar();
                    return Result;
                }


            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// insert a new Search object to the DB
        /// </summary>
        /// <param name="SearchName">the search name</param>
        /// <param name="SearchFolder">the search folder</param>
        /// <param name="Searchdate">the date of search</param>
        /// <returns>true if the process finished sucsesfuly\ false if it doesnt</returns>
        public static bool InsertNewSearch(string SearchName, string SearchFolder, string Searchdate)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand query = new SqlCommand($"execute InsertSearchValue '{SearchName}','{SearchFolder}','{Searchdate}'", Sql);
                    int Result = query.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// update the date of an exsisiting Search object
        /// </summary>
        /// <param name="UserSearchId">the object ID fron the Db</param>
        /// <param name="SearchDate">a string contains the date</param>
        /// <returns>true if the process finished sucsesfuly\ false if it doesnt</returns>
        public static bool UpdateSearchDate(int UserSearchId, string SearchDate)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand query = new SqlCommand($"execute UpdateSearchDate {UserSearchId},'{SearchDate}'", Sql);
                    int Result = query.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// search for the Resuld object Id in the DB
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static int GetSearchResultID(string filepath, string filename)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand Query = new SqlCommand($"select SearchResultId from SearchResults where FileName='{filename}' AND FilePath='{filepath}'", Sql);
                    int Result = (int)Query.ExecuteScalar();
                    return Result;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Insert a new Search result object to the DB
        /// </summary>
        /// <param name="filepath">the path to the file</param>
        /// <param name="filename">the name of the file</param>
        /// <returns>true if the process was done sucsesfuly. false if it doesnt</returns>
        public static bool InsertNewSearchResult(string filepath, string filename)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand query = new SqlCommand($"EXECUTE InsertResultValue '{filepath}','{filename}'", Sql);
                    int Result = query.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// check if a connection between the Seach and the results exsist in the Db
        /// </summary>
        /// <param name="searchid">the search id from the DB</param>
        /// <param name="resultID">the result id from the DB</param>
        /// <returns>true if exsist. false if it doesnt</returns>
        public static bool CheckIfConnectionExsist(int searchid, int resultID)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand query = new SqlCommand($"select ID from SearchResultConnections WHERE SearchId={searchid} AND ResultId={resultID}", Sql);
                    int Result = (int)query.ExecuteScalar();
                    if (Result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// create a new connection between the search object and the result object
        /// </summary>
        /// <param name="searchid">the Search id from the search table</param>
        /// <param name="resultID">the result id from the result table</param>
        /// <returns></returns>
        public static bool InsertNewConnection(int searchid, int resultID)
        {
            try
            {
                using (Sql = new SqlConnection(ConnectionString))
                {
                    Sql.Open();
                    SqlCommand query = new SqlCommand($"execute InsertConnection {searchid},{resultID}", Sql);
                    int Result = query.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
