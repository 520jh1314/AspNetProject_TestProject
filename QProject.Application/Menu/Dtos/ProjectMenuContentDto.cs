using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.Menu.Dtos
{
    public class ProjectMenuContentDto
    {
       
        public string FCID { get; set; }

        public string FatherNode { get; set; }

        public string ListName { get; set; }

        public string ListMenuCode { get; set; }

        public int? MenuLevel { get; set; }

        //二级子菜单
        public List<ProjectMenuContentDto> SecondLevelChildMenu { get; set; }

        //三级子菜单
        public List<ProjectMenuContentDto> ThreeLevelChildMenu { get; set; }

    }


    public class ProjectMenuContentDtoObject
    {

        public string FCID { get; set; }

        public string FatherNode { get; set; }

        public string ListName { get; set; }

        public string ListMenuCode { get; set; }

        public string MenuLevel { get; set; }

    }

}
