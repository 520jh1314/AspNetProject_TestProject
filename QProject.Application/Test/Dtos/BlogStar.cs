using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.test.Dtos
{
    public class BlogStar
    {
        [Required,Range(1,int.MaxValue)]
        public int userId { get; set; }
        [Required,Range(1,int.MaxValue)]
        public int blogId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example>0</example>
        [Required,Range(0,1)]
        public int StarStarte { get; set; }
    }
}
