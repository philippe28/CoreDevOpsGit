using System.Collections.Generic;

namespace CoreDevOpsGit
{
    public class GitConfig
    {
        private List<Package> packagelist;
        public string EnterpriseName { get; set; }
        public GitCredentials GitCredentials { get; set; }
        public List<Package> PackageList { get => packagelist; set => packagelist = value; }         
    }
}