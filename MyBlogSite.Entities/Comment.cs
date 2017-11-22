using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Entities
{
    [Table("Comments")]
    public class Comment : EntitiyBase
    {
        [Required,StringLength(300)]
        public string Text { get; set; }
       // public bool IsApproval { get; set; }
       //Yorumları onaylamak

        public virtual BlogSiteUser Owner { get; set; }
        public virtual Note Note { get; set; }

    }
}
