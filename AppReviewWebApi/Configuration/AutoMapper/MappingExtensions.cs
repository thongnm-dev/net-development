using Net.Core.AutoMapper;
using Net.Core;
using Net.WebCore.Model;

namespace AppReviewWebApi.Configuration.AutoMapper
{
    public static class MappingExtensions
    {
        private static TDes Map<TDes>(this object source)
        {
            return AutoMapperConfiguration.Mapper.Map<TDes>(source);
        }

        public static TModel ToModel<TModel>(this BaseEntity entity) where TModel : BaseModel
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity.Map<TModel>();
        }
    }
}
