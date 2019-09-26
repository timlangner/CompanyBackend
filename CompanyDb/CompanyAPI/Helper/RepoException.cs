using System;

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
        SQLERROR,
        NOTFOUND,
        WRONGPARAMETER,
        NOCONTENT
    }
}
