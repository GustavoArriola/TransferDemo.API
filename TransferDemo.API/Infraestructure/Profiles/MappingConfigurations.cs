using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure.Profiles
{
    public class MappingConfigurations : AutoMapper.Profile
    {
        public MappingConfigurations()
        {
            CreateMap<Transfer, TransferDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TransferDate, opt => opt.MapFrom(src => src.TransferDate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForPath(dest => dest.BankInformationSource.BankName, opt => opt.MapFrom(src => src.BankNameFrom))
                .ForPath(dest => dest.BankInformationSource.CustomerAccount, opt => opt.MapFrom(src => src.CustomerAccountFrom))
                .ForPath(dest => dest.BankInformationDestination.BankName, opt => opt.MapFrom(src => src.BankNameTo))
                .ForPath(dest => dest.BankInformationDestination.CustomerAccount, opt => opt.MapFrom(src => src.CustomerAccountTo))
                .ReverseMap();
        }
    }
}
