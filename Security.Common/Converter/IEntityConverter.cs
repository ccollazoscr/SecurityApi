using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Common.Converter
{
    public interface IEntityConverter<M, P> where M : class where P : class
    {
        P FromModelToEntity(M source);
        M FromEntityToModel(P source);
    }
}
