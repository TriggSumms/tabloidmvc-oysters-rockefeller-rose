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

        public int EstimatedReadTime
        {
            get {
                    int roundedTime = 0;

                if (Content != null)
                { 

                    int wordCount = Content.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                    double exactTime = wordCount/265;
                    roundedTime = (int)Math.Ceiling(exactTime);
                        if (roundedTime == 0)
                    {
                        roundedTime = 1;
                    }
                }
                    
                    
                    return roundedTime;
                }
            
        }
    }
}


