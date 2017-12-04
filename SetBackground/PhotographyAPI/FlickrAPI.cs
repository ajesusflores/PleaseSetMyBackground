using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetBackground.PhotographyAPI
{
    public class FlickrAPI : IPhotographyProvider
    {
        Flickr _flickrAPI;
        public FlickrAPI(string apiKey)
        {
            apiKey = "96a7dc47f0de79ed8c6e0f1498324c7c";
            _flickrAPI = new Flickr(apiKey);
        }

        public dynamic GetImageFromText(string textToSearch)
        {
            var options = new PhotoSearchOptions { PerPage = 5 };

            var s = _flickrAPI.PhotosSearch(options);

            return "";
        }
    }
}
