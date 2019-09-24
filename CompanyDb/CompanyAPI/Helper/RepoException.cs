using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Helper
{
    [Serializable]
    public class RepoException : Exception
    {
        public RepoResultType ExType { get; set; }
        public RepoException(RepoResultType exType)
        {
            ExType = exType;
        }
        public RepoException(string message, RepoResultType exType) : base(message)
        {
            ExType = exType;
        }
        public RepoException(string message, Exception inner, RepoResultType exType) : base(message, inner)
        {
            ExType = exType;
        }
    }

    public enum RepoResultType
    {
        SQLERROR = 0,
        NOTFOUND = 1,
        WRONGPARAMETER =  2
    }
}
