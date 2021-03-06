﻿namespace SilentNotary.Common.Mapping
{
    public interface IMapperService<out TDest, in TDto>
    {
        TDest GetFrom(TDto model, object mappingData = null);
    }
}
