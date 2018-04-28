using Nancy;
using Nancy.ModelBinding;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
  
namespace NancyApplication
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
            Get("/test/{name}", args => new Person() { Name = args.name});
            Post("/listFiles/", args => {
                ListFilesRequest req = this.Bind<ListFilesRequest>();
                Console.WriteLine("Received request to list files for " + req.FilePath);
                string[] directories = Directory.GetDirectories(req.FilePath);
                List<FileEntry> dirEntries = directories.Select(fileName => {
                    FileEntry fileEntry = new FileEntry();
                    DirectoryInfo dirInfo = new DirectoryInfo(fileName);
                    fileEntry.FileName = dirInfo.Name;
                    fileEntry.FileSize = 0;
                    fileEntry.Directory = true;
                    return fileEntry;
                    }).ToList();

                string[] files = Directory.GetFiles(req.FilePath);

                List<FileEntry> fileEntries = files.Select(fileName => {
                    FileEntry fileEntry = new FileEntry();
                    FileInfo fileInfo = new FileInfo(fileName);
                    fileEntry.FileName = fileInfo.Name;
                    fileEntry.FileSize = fileInfo.Length;
                    fileEntry.Directory = false;
                    return fileEntry;
                    }).ToList();
                

                return dirEntries.Concat(fileEntries);
            });
        }
    }

    public class ListFilesRequest 
    {
        public string FilePath {get; set;}
    }

    public class ListFilesResponse 
    {
        public FileEntry[] files {get; set;}
    }

    public class FileEntry 
    {
        public string FileName {get; set;}
        public long FileSize{get; set;}

        public bool Directory{get; set;}
    }
 
    public class Person
    {
        public string Name { get; set; }

    }
}