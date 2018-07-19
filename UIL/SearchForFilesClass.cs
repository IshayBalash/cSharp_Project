using System;
using System.Collections.Generic;
using BOL;
using System.IO;

namespace UIL
{
    public delegate void PrintBlankInfo();
    public class SearchForFilesClass
    {
        #region Events section
        public event Action<SearchResult> WhenFileIsFoundHandler;
        public event PrintBlankInfo NoFielsWereFoundHandler;
        public event Action<string> CantAcsesFolderHandler;
        #endregion

        #region Search for files Functions
        /// <summary>
        /// search in the entire user folders for a file contains the search name
        /// </summary>
        /// <param name="filename"> the name to search for</param>
        /// <returns> a list of search results objects</returns>
        public List<SearchResult> SearchForFiles(string filename)
        {
            //get all the drivers on the user computer
            DriveInfo[] DriverARR = DriveInfo.GetDrives();
            List<string> FolderList = new List<string>();
            foreach (DriveInfo item in DriverARR)
            {
                //get all the main folders in the driver and add them to the folder list
                string[] FoldersInDriver = GetAllFoldersInPath(item.Name);
                if (FoldersInDriver == null)
                {
                    continue;
                }
                foreach (string folder in FoldersInDriver)
                {
                    FolderList.Add(folder);
                }
            }
            List<SearchResult> FileList = new List<SearchResult>();
            //get all the files
            foreach (string item in FolderList)
            {
                string[] FielsInFolder = GetAllFIlesInPath(item);
                if (FielsInFolder == null)
                {
                    continue;
                }
                if (FielsInFolder.Length == 0)
                {
                    continue;
                }
                //convert the fiels to a searchresult list
                foreach (string file in FielsInFolder)
                {
                    SearchResult SingleFile = ConverToSearchResultObjects(file);
                    FileList.Add(SingleFile);
                }
            }
            //filter only files that contains in thier name the search param
            return FilterList(FileList, filename);
        }

        /// <summary>
        /// search in a spesific folder all files contains the search name
        /// </summary>
        /// <param name="filename"> the name of the file to search for</param>
        /// <param name="path">the path to search the files in</param>
        /// <returns>a list of all files in the givven path thar contain the search name</returns>
        public List<SearchResult> SearchForFiles(string filename, string path)
        {
             List<string> Fiels = new List<string>();
            //check if there are files in the folder and add them to the main folder
             string [] filesInFolder= Directory.GetFiles(path);
             if (filesInFolder.Length > 0)
             {
                foreach (string file in filesInFolder)
                {
                    Fiels.Add(file);
                }
            }
            //get all the sub folders on the folder givvan
            string[] FolderList = GetAllFoldersInPath(path);
            if (FolderList.Length > 0)
            {
                //get all fiels in folder
                foreach (string Folder in FolderList)
                {
                    string[] FilesInfoler = GetAllFIlesInPath(Folder);
                    if (FilesInfoler == null)
                    {
                        continue;
                    }
                    else if (FilesInfoler.Length > 0)
                    {
                        foreach (string file in FilesInfoler)
                        {
                            Fiels.Add(file);
                        }
                    }
                }
            }
            if (Fiels.Count == 0)
            {
                NoFielsWereFoundHandler?.Invoke();
                return null;
            }
            else
            {
                List<SearchResult>FileList= new List<SearchResult>();
                foreach (string file in Fiels)
                {
                    FileList.Add(ConverToSearchResultObjects(file));
                }
                return FilterList(FileList, filename);
            }

        }

        #endregion


        
        #region Sub Functions

        /// <summary>
        /// this funck gets all the sub folder in a directory- in case that the user cant acses a spesific folder it pass that folder and continue
        /// </summary>
        /// <param name="path">the main folder</param>
        /// <returns> a list of all path inaide that folder</returns>
        public string[] GetAllFoldersInPath(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {

                CantAcsesFolderHandler?.Invoke(path);
                return null;
            }
        }


        /// <summary>
        /// Get all the files in all sub folders of a path.
        /// </summary>
        /// <param name="path">the path to the folder</param
        /// <returns> a file list in an string arry</returns>
        public string[] GetAllFIlesInPath(string path)
        {

            try
            {
                return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            }
            catch
            {
                CantAcsesFolderHandler?.Invoke(path);
                return null;
            }
        }


        /// <summary>
        /// converts a file list to a search results object list
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public SearchResult ConverToSearchResultObjects(string file)
        {
            SearchResult SingleFile = new SearchResult
            {
                FileName = Path.GetFileName(file),
                Path = Path.GetDirectoryName(file)
            };
            return SingleFile;

        }


        /// <summary>
        /// filter a serach result object list acoording to a givven text
        /// </summary>
        /// <param name="FileList"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<SearchResult> FilterList(List<SearchResult> FileList, string filename)
        {
            List<SearchResult> FilterList = new List<SearchResult>();
            foreach (SearchResult item in FileList)
            {
                if (item.FileName.ToLower().Contains(filename.ToLower()))
                {
                    WhenFileIsFoundHandler?.Invoke(item);
                    FilterList.Add(item);
                }
            }
            if (FilterList.Count == 0)
            {
                NoFielsWereFoundHandler?.Invoke();
            }
            return FilterList;

        }


        /// <summary>
        /// check if a folder exsists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ChekIfFolderExsist(string path)
        {
            if (!Directory.Exists(path))
            {
                //PathNotVaid?.Invoke();
                return false;
            }
            return true;
        }

        #endregion



    }
}
