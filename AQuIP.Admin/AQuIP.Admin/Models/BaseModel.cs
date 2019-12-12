using System.Web;

namespace AQuIP.Admin.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            if (string.IsNullOrEmpty(this.LastChangedUser))
            {
                this.LastChangedUser = HttpContext.Current.User.Identity.Name;
            }
        }
        public string LastChangedUser { get; set; }
    }
}