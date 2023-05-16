using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.Menu.Dtos
{
    public class ProjectFileInfoDtos
    {
        public string FIID { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public string FCID { get; set; }

        public string FlieName { get; set; }

        public string UploadName { get; set; }

        public string Unit { get; set; }

        public string IdCardInform { get; set; }
    }

    public class addProjectFileInfoDtos
    {
        public string FlieName { get; set; }

        public string UploadName { get; set; }

        public string Unit { get; set; }

        public string IdCardInform { get; set; }
    }
}
