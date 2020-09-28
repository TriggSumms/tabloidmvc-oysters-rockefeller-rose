using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DisplayName("Header Image URL")]
        [Url]
        public string ImageLocation { get; set; }

        public DateTime CreateDateTime { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.Date)]
        public DateTime? PublishDateTime { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        // Tacking on an estimated reading time to be displayed with each post
        public int EstimatedReadTime
        {
                // getting, but not setting here
            get {
                        // you want to return an est readtime, so start the counter at 0
                    int roundedTime = 0;

                    //if the Content field is filled out - do the following
                if (Content != null)
                { 
                        // Take the Content field input as a string and use the Split method to pass in an 
                        // empty string to represent a whitespace character (' ') as the parameter.
                        // This will create a new string for every word by slitting it on the whitespace.
                        // Now use .Length to count the number of whitspace occurances. 
                        // and store that number in the variable int wordCount.
                    int wordCount = Content.Split(' ').Length;
                        // Now get the readtime by dividing the wordcount by 250(words per minute)
                        // and store that number in the variable double exactTime
                    double exactTime = wordCount/250;
                        // Next, take the exactTime and use the Math.Ceiling method it.
                        // This will round up to the next integer and store it in the roundedTime variable
                    roundedTime = (int)Math.Ceiling(exactTime);
                            // if roundedTime is less than 1 minute, then round it up to 1 minute readtime
                        if (roundedTime == 0)
                    {
                        roundedTime = 1;
                    }
                }
                        // return the rounded up integer to be displayed with the post details on the browser
                    return roundedTime;
                }
            
        }
    }
}


