using AutoMapper;

using FM.FrameWork.UomModule.UOMDefinition;
using FM.FrameWork.UomModule.UOMService.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMService.Mapper
{
    /// <summary>
    /// 数据映射
    /// </summary>
    public static class UOMDtoAutoMapper
    {
        /// <summary>
        /// 创建映射对象
        /// </summary>
        /// <param name="configuration"></param>
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UOM, UomEditDto>().ReverseMap();
        }
    }
}
