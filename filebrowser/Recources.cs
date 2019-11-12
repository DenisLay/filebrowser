using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filebrowser
{
    class Recources
    {
        string file;
        string folder;
        string hdd;

        public Recources()
        {
            file = "/Resources/file.ico";
            folder = "/Resources/folder.ico";
            hdd = "/Resources/hdd.ico";
        }

        public string File
        {
            get
            {
                return file;
            }
        }

        public string Folder
        {
            get
            {
                return folder;
            }
        }

        public string Hdd
        {
            get
            {
                return hdd;
            }
        }

    }
}
