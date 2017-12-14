using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Entities
{
    //DataAnnotations  
    [Table("Categories")]
   public class Category : EntitiyBase
    {   
        [Required,StringLength(50)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
        
        //Başka bir classla ilişkili olduğu için virtual olarak tanımladık.
        public virtual List<Note> Notes { get; set; }

        public Category()
        {

            Notes = new List<Note>();
        }
       

    }
}
