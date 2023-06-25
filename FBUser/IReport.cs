using System;

namespace MyUser
{
    public interface IReport
    {
        string Report();
        bool HasReport();
        void ClearReports();
    }
}
