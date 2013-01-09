using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtectedDownload
{
    public class FileListDetails
    {
        private int fileID;
		private string description;
		private string fileName;
               
		public int ID
		{
			get {return fileID;}
			set {fileID = value;}
		}
		public string FileName
		{
			get {return fileName;}
			set {fileName = value;}
		}
		public string Description
		{
			get {return description;}
			set {description = value;}
		}
		
		public FileListDetails(int fileID, string fileName, string description)
		{
			this.fileID = fileID;
			this.fileName = fileName;
            this.description = description;
		}

        public FileListDetails() { }

    }
}