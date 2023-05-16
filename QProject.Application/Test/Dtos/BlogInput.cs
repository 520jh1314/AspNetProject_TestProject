using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.test.Dtos
{
    public class BlogInput
    {
        [Required, MinLength(1), MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int CreateUserId { get; set; }

    }
}
