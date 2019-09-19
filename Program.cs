using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using LibGit2Sharp;

namespace CoreDevOpsGit
{
    class Program
    {           
        static void Main(string[] args)
        {
            if (ExistsConfigurationFile("appsettings.json") == true)
            {
                GitConfig gitConf = LoadConfig("appsettings.json");
                
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" " + gitConf.EnterpriseName + " ");
                Console.WriteLine("");
                Console.WriteLine(" Package management ");
                Console.WriteLine("");
                Console.WriteLine(" Available Repositories ");
                Console.WriteLine("");
                foreach (Package package in gitConf.PackageList)
                {
                    Console.WriteLine("--- Repository: Id = " + package.Id.ToString() + " name '"  + package.Name + "' package: '" + package.GroupName + "'.");
                }
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine(" 1 - Clone a repositories package");
                Console.WriteLine(" 2 - Clone a repository");
                Console.WriteLine(" 3 - Cancel and exit");
                int opcion = Int32.Parse(Console.ReadLine());
                Console.WriteLine(opcion.ToString());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("You have chosen to clone a package, enter the name:");
                        string nombre = Console.ReadLine();
                        if (ClonePackage(gitConf, nombre) == true){
                            Console.WriteLine("Finalized");
                        }
                        else
                        {
                            Console.WriteLine("The process has not been performed, are you sure the package '"+ nombre + "' exists?");
                        }
                        break;
                    case 2:
                        Console.WriteLine("You have choosen to select a repository, enter the Id:");
                        int id = Int32.Parse(Console.ReadLine());
                        if (CloneRepository(gitConf, id) == true)
                        {
                            Console.WriteLine("Finalized");
                        }
                        else
                        {
                            Console.WriteLine("The process has not been performed, are you sure the reposiroty: '"+ id.ToString() + "'exists?");
                        }
                        break;
                    case 3:
                        Console.WriteLine("GoodBye");
                        break;
                }                
            }
            else
            {
                Console.WriteLine("Package management");
                Console.WriteLine("The appsettings.json configuration file does not exist. The process is canceled.");
            }
        }

        private static bool ExistsConfigurationFile(string filename){
            return File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + filename);
        }

        private static GitConfig LoadConfig(string filename){
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filename);
            return builder.Build().GetSection("config").Get<GitConfig>();
        }

        private static bool ClonePackage(GitConfig config, string nombre){
            bool packagefound = false;
            foreach (Package item in config.PackageList)
            {
                if (item.GroupName.ToUpper() == nombre.ToUpper())
                {
                    CloneGit(item.Url, item.PathDestination, config.GitCredentials.Username, config.GitCredentials.Password);
                    RunOperations(item);
                }
            }
            return packagefound;
        }

        private static bool CloneRepository(GitConfig config, int id){
            bool repofound = false;
            foreach (Package item in config.PackageList)
            {
                repofound = item.Id == id;
                if (repofound == true){
                    CloneGit(item.Url, item.PathDestination, config.GitCredentials.Username, config.GitCredentials.Password);
                    RunOperations(item);
                    break;
                }
            }
            return repofound;
        }

        private static void CloneGit(string url, string destinationpath, string user, string pass){
            CloneOptions co = new CloneOptions();
            co.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials {Username = user, Password = pass};
            Repository.Clone(url, destinationpath, co);
        }

        private static void RunOperations(Package item)
        {
            if (item.Operations.Count > 0)
            {
                foreach (Operation op in item.Operations)
                {
                    RunOperation(item.PathDestination, op);
                }
            }
        }

        private static void RunOperation(string destinationpath, Operation operationtorun){
            switch (operationtorun.Command)
            {
                case "cp":
                    if (File.Exists(destinationpath + Path.PathSeparator + operationtorun.Origin))
                    {
                        File.Copy(destinationpath + Path.PathSeparator + operationtorun.Origin, destinationpath + Path.PathSeparator + operationtorun.Destination, true);
                    }
                    break;
            }
        }
    }
}
