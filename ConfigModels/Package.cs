using System.Collections.Generic;

namespace CoreDevOpsGit
{
    public class Package
    {
        private List<Operation> operations;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string PathDestination { get; set; }
        public string GroupName { get; set; }

        public List<Operation> Operations { get => operations; set => operations = value; }
    }
}