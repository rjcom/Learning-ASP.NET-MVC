using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace ISkillsStore.Models
{

    public class BillingInfo
    {
        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a credit card number")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter an expiration date")]
        public string ExpirationMonth { get; set; }

        public SelectList Months()
        {
            return new SelectList(new string[] { "January", "February", "March",
                "April", "May", "June", "July", "August", "September", "October",
                "November", "December" });
        }

        [Required(ErrorMessage = "Please enter an expiration date")]
        public string ExpirationYear { get; set; }

        public SelectList Years()
        {
            return new SelectList(new string[] { "2014", "2015", "2016",
                "2017", "2018", "2019", "2020", "2021", "2022", "2023",
                "2024", "2025" });
        }

    }
}
