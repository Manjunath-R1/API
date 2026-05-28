using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class DocFileResponse : BaseResponse
    {
        public DocFileResponse()
        {
            directories = new List<File>();
            files = new List<File>();
            tree = new List<File>();
        }

        public Result result { get; set; }
        public List<File> directories { get; set; }
        public string Projects { get; set; }
        public File directory { get; set; }
        public List<File> files { get; set; }
        public Config config { get; set; }
        public List<File> tree { get; set; }
        //Go to File location variables
        public Guid SelectedDiskId { get; set; }
        public string SelectedDisk { get; set; }
        public string SelectedParentPath { get; set; }
    }

    public class Result
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class Properties
    {
        public bool hasSubdirectories { get; set; }
        public bool subdirectoriesLoaded { get; set; }
        public bool showSubdirectories { get; set; }
    }

    public class File
    {
        public File()
        {
            props = new Properties();
        }
        public Guid id { get; set; }
        public Guid parentId { get; set; }
        public string type { get; set; }
        public string path { get; set; }
        public string timestamp { get; set; }
        public string dirname { get; set; }
        public string basename { get; set; }
        public Properties props { get; set; }
        public double size { get; set; }
        public string filename { get; set; }
        public string navigationUrl { get; set; }
        public string extension { get; set; }
        public string StorageKey { get; set; }
        public string Content { get; set; }
    }  

    public class Config
    {
        public string leftDisk { get; set; }
        public string rightDisk { get; set; }
        public int windowsConfig { get; set; }
        public string lang { get; set; }
        public List<Disk> disks { get; set; }               
    }

    public class Disk
    {
        public Guid id { get; set; }
        public string key { get; set; }
        public string driver { get; set; }
    }
}
