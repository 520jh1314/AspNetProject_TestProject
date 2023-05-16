using Furion;
using System.Reflection;

namespace QProject.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "QProject.Application",
            "QProject.Core",
            "QProject.EntityFramework.Core",
            "QProject.Web.Core"
        };
    }
}