using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.test.Dtos
{
    public class BlogOutInput
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public string UserName { get; set; }

        public bool IsStar { get; set; }
    }
}
