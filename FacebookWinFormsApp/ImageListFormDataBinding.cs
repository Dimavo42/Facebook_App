using System;
using System.Drawing;

namespace MyFBApp
{
    public class ImageListFormDataBinding : IComparable<ImageListFormDataBinding>
    {
        public Image Image { get; set; }
        public int NumberOfLikes { get; set; }

        public int CompareTo(ImageListFormDataBinding other)
        {
            return NumberOfLikes.CompareTo(other.NumberOfLikes);
        }
    }
}
