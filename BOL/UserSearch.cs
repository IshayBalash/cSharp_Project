using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class UserSearch
    {

        public string SearchName { get; set; }

        public string SearchFolder { get; set; }

        public string SearchDate { get; set; }


        public override string ToString()
        {
            return $"the search for files contain the word:{SearchName} in {SearchFolder} was made in {SearchDate}";
        }

    }
}
