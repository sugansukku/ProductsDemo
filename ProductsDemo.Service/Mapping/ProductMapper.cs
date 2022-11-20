using AutoMapper;
using DataAccess = ProductsDemo.Data.DataAccess;

namespace ProductsDemo.Model.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductRequest, DataAccess.Product>()
                .ForMember(dest => dest.Productid, src => src.MapFrom(c => new Guid(c.ProductId)))
                .ForMember(dest => dest.Sizescaleid, src => src.MapFrom(c => new Guid(c.SizeScaleId)))
                .ForMember(dest => dest.Articles, src => src.MapFrom(c => c.Articles));
            CreateMap<DataAccess.Product, ProductResponse>()
                .ForMember(des => des.Articles , src => src.MapFrom(src => src.Articles));
            CreateMap<DataAccess.Article, Article>()
                .ForMember(des => des.ColourName, src => src.MapFrom(src => src.Colorname))
                .ForMember(des => des.ColourCode, src => src.MapFrom(src => src.Colorcode)); 

            CreateMap<Article, DataAccess.Article>();
                //.ForMember(dest => dest.Articlename, src => src.MapFrom(x => $" {x.pro}{"-"}{x.ColourCode}"));
            //CreateMap<DataAccess.Product, ProductResponse>().ForMember(pr => pr.Articles,
            //    c => c.MapFrom(x => new[] { new Article {
            //        ArticleName = $" {x.Productname}{""}{x.Articles.FirstOrDefault().Colorcode}",
            //        ArticleId = x.Articles.FirstOrDefault().Articleid.ToString(),
            //        ColourCode = x.Articles.FirstOrDefault().Colorcode.ToString(),
            //        ColourName = x.Articles.FirstOrDefault().Colorname,
            //        ColourId = x.Articles.FirstOrDefault().Colourid.ToString(),

            //    }
            //    }));

        }
    }
}
