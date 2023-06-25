using System.Collections.Generic;
using System.Text;

namespace MyUser
{
    public class CompositeReport : IReport
    {

        private readonly List<IReport> r_Children = new List<IReport>();
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IReport child in r_Children)
            {
                sb.AppendLine(child.Report());
            }
            return sb.ToString();
        }

        public void Add(IReport m_Child)
        {
            r_Children.Add(m_Child);
        }
        public void Remove(IReport m_Child)
        {
            r_Children.Remove(m_Child);
        }

        public bool CheckIfSubScribed(IReport m_Child)
        {
            bool result = false;
            if (r_Children.Contains(m_Child))
            {
                result = true;
            }
            return result;
        }

        public bool HasReport()
        {
            bool result = false;
            foreach (IReport child in r_Children)
            {
                if (child.HasReport())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void ClearReports()
        {
            foreach (IReport child in r_Children)
            {
                child.ClearReports();
            }
        }

        public void MakeSelfReport(object i_Sender)
        {
            Report();
        }

    }
}
