using System.Drawing;

namespace MyUser
{
    public abstract class Exit
    {
        public abstract void ClearDataFields();
        public abstract void Searlize();
        public abstract void Logout(Point i_LastWindowLocation, Size i_LastWindowSize);


        public void LogoutAndSearlize(Point i_LastWindowLocation, Size i_LastWindowSize)
        {
            Logout(i_LastWindowLocation, i_LastWindowSize);
            Searlize();
            ClearDataFields();
        }
    }
}
