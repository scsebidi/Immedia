using FlickrNet;
using LandMarks_Sipho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LandMarks_Sipho.Controllers
{
    public class LocationsController : ApiController
    {
        public IEnumerable<string> Get(string searchLocation)
        {
             Flickr flickr = new Flickr(Properties.Settings.Default.flickrKey);
            var searchTerm = searchLocation;
            var options = new PhotoSearchOptions { Tags = searchTerm };
            PhotoCollection photos = flickr.PhotosSearch(options);
            List<string> locations = new List<string>();
            using (locationsEntities1 context = new locationsEntities1())
            {
                try
                {
                    foreach (Photo photo in photos)
                    {
                        locations.Add(photo.LargeUrl);
                        var locationsSearched = new SearchedLocation();
                        locationsSearched.location = photo.Title;
                        locationsSearched.title = photo.Title;
                        locationsSearched.imageUrl = photo.Medium640Url;
                        locationsSearched.weburl = photo.WebUrl;

                        context.SearchedLocations.Add(locationsSearched);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
                
            }
            return locations;
        }
    }
}
