﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.FMFrameWorkLazy.FMFrameWorkLazyDefinition
{
    public interface IFMFrameWorkLazy<T> 
    {
        T Value { get; }
    }
}
