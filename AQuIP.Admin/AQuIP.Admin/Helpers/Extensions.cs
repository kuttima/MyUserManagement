using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Specialized;
using AQuIP.Admin.Models;

namespace AQuIP.Admin.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Used to determine the direction of the sort identifier used when filtering lists
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="sortOrder">the current sort order being used on the page</param>
        /// <param name="field">the field that we are attaching this sort identifier to</param>
        /// <returns>MvcHtmlString used to indicate the sort order of the field</returns>
        public static IHtmlString SortIdentifier(this HtmlHelper htmlHelper, string sortOrder, string field)
        {
            if (string.IsNullOrEmpty(sortOrder) || (sortOrder.Trim() != field && sortOrder.Replace("_desc", "").Trim() != field)) return null;

            string glyph = "glyphicon glyphicon-chevron-up";
            if (sortOrder.ToLower().Contains("desc"))
            {
                glyph = "glyphicon glyphicon-chevron-down";
            }

            var span = new TagBuilder("span");
            span.Attributes["class"] = glyph;

            return MvcHtmlString.Create(span.ToString());
        }

        /// <summary>
        /// Converts a NameValueCollection into a RouteValueDictionary containing all of the elements in the collection, and optionally appends
        /// {newKey: newValue} if they are not null
        /// </summary>
        /// <param name="collection">NameValue collection to convert into a RouteValueDictionary</param>
        /// <param name="newKey">the name of a key to add to the RouteValueDictionary</param>
        /// <param name="newValue">the value associated with newKey to add to the RouteValueDictionary</param>
        /// <returns>A RouteValueDictionary containing all of the keys in collection, as well as {newKey: newValue} if they are not null</returns>
        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection collection, string newKey, string newValue)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.AllKeys)
            {
                if (key == null) continue;
                if (routeValueDictionary.ContainsKey(key))
                    routeValueDictionary.Remove(key);

                routeValueDictionary.Add(key, collection[key]);
            }
            if (string.IsNullOrEmpty(newValue))
            {
                routeValueDictionary.Remove(newKey);
            }
            else
            {
                if (routeValueDictionary.ContainsKey(newKey))
                    routeValueDictionary.Remove(newKey);

                routeValueDictionary.Add(newKey, newValue);
            }
            return routeValueDictionary;
        }

        public static List<UserAccount> GetSortedList(List<UserAccount> userList, string sortOrder)
        {
            List<UserAccount> sortedList = new List<UserAccount>();

            switch (sortOrder)
            {
                case "firstName":
                    sortedList = userList.OrderBy(x => x.FirstName).ToList();
                    break;
                case "firstName_desc":
                    sortedList = userList.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "lastName":
                    sortedList = userList.OrderBy(x => x.LastName).ToList();
                    break;
                case "lastName_desc":
                    sortedList = userList.OrderByDescending(x => x.LastName).ToList();
                    break;
                case "userName":
                    sortedList = userList.OrderBy(x => x.UserName).ToList();
                    break;
                case "userName_desc":
                    sortedList = userList.OrderByDescending(x => x.UserName).ToList();
                    break;
                case "organization":
                    sortedList = userList.OrderBy(x => x.Name).ToList();
                    break;
                case "organization_desc":
                    sortedList = userList.OrderByDescending(x => x.Name).ToList();
                    break;
            }

            return sortedList;

        }
    }
}
