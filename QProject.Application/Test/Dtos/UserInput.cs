using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.test.Dtos
{
    public class UserInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>小白的豪豪</example>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>2022-02-07</example>
        public DateTime Birthday { get; set; }
    }


    public class UserInputId : UserInput
    {
        public int Id { get; set; }
    }
}
