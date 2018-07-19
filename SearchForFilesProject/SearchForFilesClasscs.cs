using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.IO;

namespace SearchForFilesProject
{
    public class  SearchForFilesClass 
    {
        //evert time 
        public event Action<SearchResult>WhenFileIsFoundHandler;

        public  List<SearchResult> SearchForFiles(string filename)
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
            //get all the files--if in some case the user cant acses the folder it just pass that folder
            foreach (string item in FolderList)
            {
                try
                {
                    string[] FielsInFolder = Directory.GetFiles(item, "*.*", SearchOption.AllDirectories);
                    if (FielsInFolder == null)
                    {
                        continue;
                    }
                    if (FielsInFolder.Length == 0)
                    {
                        continue;
                    }
                    foreach (string file in FielsInFolder)
                    {
                        SearchResult SingleFile = new SearchResult
                        {
                            FileName = Path.GetFileName(file),
                            Path = Path.GetFullPath(file)
                        };
                        FileList.Add(SingleFile);
                    }
                }
                catch
                {
                    continue;
                }
            }
            List<SearchResult> FilterList = new List<SearchResult>();
            //filter only files that contains in thier name the search param
            foreach (SearchResult item in FileList)
            {
                if (item.FileName.Contains(filename))
                {
                    WhenFileIsFoundHandler?.Invoke(item);
                    FilterList.Add(item);
                }
            }
            return FilterList;

        }




        /// <summary>
        /// this funck gets all the sub folder in a directory- in case that the user cant acses a spesific folder it pass that folder and continue
        /// </summary>
        /// <param name="path">the main folder</param>
        /// <returns> a list of all path inaide that folder</returns>
        public static string[] GetAllFoldersInPath(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return null;
            }
        }
    }
}
