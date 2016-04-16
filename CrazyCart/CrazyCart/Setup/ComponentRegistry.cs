using AutoMapper;
using CrazyCart.Factories;
using CrazyCart.Models;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;
using Ninject;

namespace CrazyCart.Setup
{
    public static class ComponentRegistry
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IApplicationSettings>().To<ApplicationSettings>().InSingletonScope();

            //UseMongoDbStorage(kernel);
            UseInMemoryStorage(kernel);

            RegisterAutoMapper();
        }

        private static void UseInMemoryStorage(IKernel kernel)
        {
            kernel.Bind<IProductStorageFactory>().ToConstructor(x => new InMemoryProductStorageFactory());
            kernel.Bind<IProductStorage>().ToMethod(x => x.Kernel.Get<IProductStorageFactory>().Build()).InSingletonScope();

            kernel.Bind<ICartStorageFactory>().ToConstructor(x => new InMemoryCartStorageFactory());
            kernel.Bind<ICartStorage>().ToMethod(x => x.Kernel.Get<ICartStorageFactory>().Build()).InSingletonScope();
        }

        private static void UseMongoDbStorage(IKernel kernel)
        {
            var mongoDbConnectionString = ApplicationSettingsInstance.Instance.MongoDbConnectionString;
            kernel.Bind<IProductStorageFactory>().ToConstructor(x => new MongoDbProductStorageFactory());
            kernel.Bind<IProductStorage>().ToMethod(x => x.Kernel.Get<IProductStorageFactory>().Build(mongoDbConnectionString));

            kernel.Bind<ICartStorageFactory>().ToConstructor(x => new MongoDbCartStorageFactory());
            kernel.Bind<ICartStorage>().ToMethod(x => x.Kernel.Get<ICartStorageFactory>().Build(mongoDbConnectionString));
        }

        public static void RegisterAutoMapper()
        {
            Mapper.CreateMap<ProductModel, Product>();
            Mapper.CreateMap<Product, ProductModel>();

            Mapper.CreateMap<CartModel, Cart>()
                .ForMember(x => x.Rows, expression => expression.MapFrom(y => y.Rows));
            Mapper.CreateMap<Cart, CartModel>();

            Mapper.CreateMap<ProductRowModel, ProductRow>()
                .ForMember(x => x.Product, opt => opt.MapFrom(y => y));

            Mapper.CreateMap<ProductRow, ProductRowModel>()
                .ForMember(x => x.Quantity, opt => opt.MapFrom(y => y.Quantity))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Product.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Product.Name))
                .ForMember(x => x.PriceIncVat, opt => opt.MapFrom(y => y.Product.PriceIncVat))
                .ForMember(x => x.VatPercentage, opt => opt.MapFrom(y => y.Product.VatPercentage));

            Mapper.CreateMap<ProductRowModel, Product>();
            Mapper.CreateMap<Product, ProductRowModel>().ForMember(x => x.Quantity, opt => opt.Ignore());
        }
    }
}