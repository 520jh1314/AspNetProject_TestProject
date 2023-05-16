using Microsoft.AspNetCore.Mvc;

namespace QProject.Application;

/// <summary>
/// 系统服务接口
/// </summary>
[ApiDescriptionSettings(true,Order =0,Groups =new string[]{"规范化接口演示"},Name ="系统服务接口")]
public class SystemAppService : IDynamicApiController
{
    private readonly ISystemService _systemService;
    public SystemAppService(ISystemService systemService)
    {
        _systemService = systemService;
    }

    /// <summary>
    /// 获取系统描述
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        return _systemService.GetDescription();
    }
}
