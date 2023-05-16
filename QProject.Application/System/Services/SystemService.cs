using Furion.FriendlyException;

namespace QProject.Application;

public class SystemService : ISystemService, ITransient
{
    public string GetDescription()
    {
        var a = "你好";
        var b = a != "nihao1" ? throw Oops.Oh("错误") : a;
        return "让 .NET 开发更简单，更通用，更流行。";
    }
}
