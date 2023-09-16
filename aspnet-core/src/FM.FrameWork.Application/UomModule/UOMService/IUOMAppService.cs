using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FM.FrameWork.Dto;
using FM.FrameWork.Roles.Dto;
using FM.FrameWork.UomModule.UOMService.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMService
{
    /// <summary>
    /// Uom应用层接口
    /// </summary>
    public interface IUOMAppService : IApplicationService
    {
        /// <summary>
        ///     获取的分页列表
        /// </summary>
        /// <param name="input">分页查询入参</param>
        /// <returns></returns>
        Task<PagedResultDto<NdoDto>> GetNdoPaged(GetNdoPagedInput input);

        /// <summary>
        ///     返回实体的EditDto
        /// </summary>
        /// <param name="input">单位id</param>
        /// <returns></returns>
        Task<GetUomForEditDto> GetForEdit(NullableIdDto<Guid> input);

        /// <summary>
        ///     添加或者修改的公共方法
        /// </summary>
        /// <param name="input">添加或者修改入参</param>
        /// <returns></returns>
        Task<Guid> CreateOrUpdate(CreateOrEditUomInput input);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="input">单位id</param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);

        /// <summary>
        ///     Ndo复制功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CopyNdo(CopyNdoDto input);
    }
}
