using AutoMapper;
using Net.Core.AutoMapper;

namespace AppReviewWebApi.Configuration.AutoMapper
{
    public class ApiMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public int Order => 10;

        public ApiMapperConfiguration()
        {
            CreateMapCategory();
        }

        #region All Mapper

        private void CreateMapCategory()
        {
            //CreateMap<Category, CategoryModel>();
                //.ForMember
        }
        #endregion
    }
}
