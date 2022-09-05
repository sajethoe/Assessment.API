using System.ComponentModel.DataAnnotations;

namespace Assessment.API.Models
{
    public class ServiceCall
    {       

        // Properties
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }            

        public string Description { get; set; } 

        public DateTime CreatedOn { get; set; }

        [Range (1, 3,
            ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public Status Status { get; set; }


    }

    public enum Status
    {
        Open = 1, 
        Planned = 2, 
        Completed = 3
    }
}
