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
            _flickrAPI = new Flickr(apiKey);
        }

        public dynamic GetImageFromText(string textToSearch)
        {
            var options = new PhotoSearchOptions
            {
                PerPage = 5,
                Text = textToSearch,
                SortOrder = PhotoSearchSortOrder.InterestingnessDescending,
                MediaType = MediaType.Photos,
                Extras = PhotoSearchExtras.MediumUrl | PhotoSearchExtras.LargeUrl | PhotoSearchExtras.CountComments | PhotoSearchExtras.CountFaves,
                
            };

            var photos = _flickrAPI.PhotosSearch(options)
                            .OrderByDescending(x => x.CountComments + x.CountFaves);

            

            if (!photos.Any())
                return "";

            return photos.FirstOrDefault().DoesLargeExist ? 
                        photos.FirstOrDefault().LargeUrl : 
                        photos.FirstOrDefault().DoesMediumExist ? 
                            photos.FirstOrDefault().MediumUrl : 
                            photos.FirstOrDefault().SmallUrl;

        }
    }
}
