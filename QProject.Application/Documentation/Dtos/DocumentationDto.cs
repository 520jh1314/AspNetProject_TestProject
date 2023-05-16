using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.Documentation.Dtos
{
    /// <summary>
    /// 查询
    /// </summary>
    public class DocumentationDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string FCID { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string SuffixName { get; set; }

        public string FilePath { get; set; }

        /// <summary>
        /// 文件以byte格式存储
        /// </summary>
        public byte[] FilePathByte { get; set; }
    }

    /// <summary>
    /// 添加
    /// </summary>
    public class addDocumentationDto
    {
        public string Name { get; set; }
        public string SuffixName { get; set; }
        public string FilePath { get; set; }
        public int IsDeleted { get; set; }
    }
}
