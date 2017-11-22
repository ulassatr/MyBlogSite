using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Entities
{
    public class EntitiyBase
    {
        //Primary key ve otomatik artan yaptık.
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        //Required : Boş geçilemez 
        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }
        //30 karakterle sınırladık
        [Required,StringLength(30)]
        public string ModifiedUserName { get; set; }
    }
}
