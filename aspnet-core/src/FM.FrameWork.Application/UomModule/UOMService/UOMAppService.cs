using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;

using FM.FrameWork.Dto;
using FM.FrameWork.FMFrameWorkLazy.FMFrameWorkLazyDefinition;
using FM.FrameWork.UomModule.UOMDefinition.DomainService;
using FM.FrameWork.UomModule.UOMService.Dtos;

using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FM.FrameWork.UomModule.UOMDefinition;
using FM.FrameWork.Extenions;
using Abp.Authorization;
using FM.FrameWork.Authorization;
using Abp.Auditing;

namespace FM.FrameWork.UomModule.UOMService
{
    /// <summary>
    /// Uom应用层实现
    /// </summary>
    [DisableAuditing]
    public class UOMAppService : FrameWorkAppServiceBase, IUOMAppService
    {
        private readonly IFMFrameWorkLazy<IUOMManager> _uOMManagerLazy;
        private IUOMManager _uOMManager => _uOMManagerLazy.Value;
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="uOMManagerLazy"></param>
        public UOMAppService(IFMFrameWorkLazy<IUOMManager> uOMManagerLazy)
        {
            _uOMManagerLazy = uOMManagerLazy;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<NdoDto>> GetNdoPaged(GetNdoPagedInput input)
        {
            var query = _uOMManager.QueryAsNoTracking
                .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), a => a.UOMName.Contains(input.FilterText));

            var totalCount = await query.CountAsync();
            var totalList = await query
                .Select(x => new NdoDto { Id = x.Id, Name = x.UOMName, CreationTime = x.CreationTime })
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NdoDto>(totalCount, totalList);
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUomForEditDto> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetUomForEditDto();
            UomEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _uOMManager.FindNdoByIdWithInclude(input.Id.Value);
                editDto = ObjectMapper.Map<UomEditDto>(entity);
                //操作人信息
                output.OperationTime = entity.LastModificationTime ?? entity.CreationTime;
                output.OperationName = entity.LastModifierUserName ?? entity.CreatorUserName;
            }
            else
            {
                editDto = new UomEditDto();
            }

            output.Uom = editDto;
            return output;
        }

        /// <summary>
        /// 添加或删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateOrUpdate(CreateOrEditUomInput input)
        {
            if (input.Uom.Id.HasValue)
            {
                return await Update(input.Uom);
            }
            else
            {
                return await Create(input.Uom);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Delete(EntityDto<Guid> input)
        {
            var entity = await _uOMManager.FindNdoByIdWithInclude(input.Id);

            // 创建追溯 - 删除
            //await _modelAuditHistoryManager.CreateOnDelete(entity);

            //TODO:删除前的逻辑判断，是否允许删除
            await _uOMManager.DeleteNdo(input.Id);
        }

        public async Task CopyNdo(CopyNdoDto input)
        {
            var uOM = await _uOMManager.FindNdoByIdWithInclude(input.Id);

            if (uOM == null)
            {
                NullError();
            }

            var res = await _uOMManager
                .QueryAsNoTracking
                .FirstOrDefaultAsync(q => q.UOMName == input.Name.Trim());

            if (res != null)
            {
                RepetError(input.Name);
            }

            if (uOM != null)
            {
                uOM.EmptyAudit();

                uOM.UOMName = input.Name;
                uOM.Id = _uOMManager.NewId();
                EmptyDefaultData.Empty(uOM);

                var uOMDto = ObjectMapper.Map<UomEditDto>(uOM);
                await Create(uOMDto);
            }
        }

        #region 私有
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task<Guid> Update(UomEditDto input)
        {
            // 更新前的逻辑判断，是否允许更新，到领域服务中进行完善，不再这里进行判断，原因是为了复用。

            var entity = await _uOMManager.FindNdoById(input.Id.Value);

            //// 记录修改之前的数据
            //var beforeEntity = await _modelAuditHistoryManager.CloneAndFillNavigations(entity);

            //将input属性的值赋值到entity中
            ObjectMapper.Map(input, entity);
            entity = await _uOMManager.UpdateNdo(entity);

            // 创建追溯 - 修改
            //await _modelAuditHistoryManager.CreateOnUpdate(beforeEntity, entity);

            return entity.Id;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task<Guid> Create(UomEditDto input)
        {
            // 新增前的逻辑判断，是否允许新增，到领域服务中进行完善，不再这里进行判断，原因是为了复用。

            var entity = ObjectMapper.Map<UOM>(input);
            //调用领域服务
            entity = await _uOMManager.CreateNdo(entity);

            // 创建追溯 - 新增
            //await _modelAuditHistoryManager.CreateOnAdd(entity);

            return entity.Id;
        }
        #endregion
    }
}
