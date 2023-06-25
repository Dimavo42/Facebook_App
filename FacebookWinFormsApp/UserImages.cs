using FacebookWrapper.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;

namespace MyFBApp
{
    //comment
    public class UserImages : IEnumerable<ImageListFormDataBinding>
    {
        private readonly List<ImageListFormDataBinding> imageListFormDataBindings;

        public UserImages(Album i_Album)
        {
            imageListFormDataBindings = fetchPictures(i_Album);
        }

        public IEnumerator<ImageListFormDataBinding> GetEnumerator()
        {
            return imageListFormDataBindings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<ImageListFormDataBinding> fetchPictures(Album i_Album)
        {
            List<ImageListFormDataBinding> imagesAndLike = new List<ImageListFormDataBinding>();
            foreach (Photo photo in i_Album.Photos)
            {
                using (WebClient client = new WebClient())
                {
                    using (MemoryStream memoryStream = new MemoryStream(client.DownloadData(photo.PictureNormalURL)))
                    {
                        Bitmap image = new Bitmap(memoryStream);
                        imagesAndLike.Add(new ImageListFormDataBinding { Image = image, NumberOfLikes = photo.LikedBy.Count() });
                    }
                }
            }
            return imagesAndLike;
        }
        public int Count
        {
            get { return imageListFormDataBindings.Count; }
        }
    }
}
