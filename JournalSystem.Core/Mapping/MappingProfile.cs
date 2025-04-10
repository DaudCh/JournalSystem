using AutoMapper;
using JournalSystem.Core.DTOS.Account;
using JournalSystem.Core.DTOS.CostCentre;
using JournalSystem.Core.DTOS.Currency;
using JournalSystem.Core.DTOS.Dimension;
using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.DTOS.JournalLines;
using JournalSystem.Core.Entities; 

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Account, CreateAccountDTO>().ReverseMap();
        CreateMap<Account, UpdateAccountDTO>().ReverseMap();
        CreateMap<Account, AccountResponseDTO>().ReverseMap();


        CreateMap<CostCenter, CreateCostCenterDTO>().ReverseMap();
        CreateMap<CostCenter, UpdateCostCenterDTO>().ReverseMap();
        CreateMap<CostCenter, CostCenterResponseDTO>().ReverseMap();


        CreateMap<Currency, CreateCurrencyDTO>().ReverseMap();
        CreateMap<Currency, UpdateCurrencyDTO>().ReverseMap();
        CreateMap<Currency, CurrencyResponseDTO>().ReverseMap();


        CreateMap<Dimension, CreateDimensionDTO>().ReverseMap();
        CreateMap<Dimension, UpdateDimensionDTO>().ReverseMap();
        CreateMap<Dimension, DimensionResponseDTO>().ReverseMap();


        CreateMap<CreateJournalEntryDTO, JournalEntry>()
             .ForMember(dest => dest.JournalLines, opt => opt.MapFrom(src => src.JournalLines));
        CreateMap<JournalEntry, UpdateJournalEntryDTO>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryDTO>()

           .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Id))
           .ForMember(dest => dest.TotalDebit, opt => opt.MapFrom(src => src.JournalLines.Sum(jl => jl.Debit)))
           .ForMember(dest => dest.TotalCredit, opt => opt.MapFrom(src => src.JournalLines.Sum(jl => jl.Credit)))
           .ForMember(dest => dest.JournalLines, opt => opt.MapFrom(src => src.JournalLines));
        CreateMap<JournalEntry, JournalEntryWithLinesDTO>().ReverseMap();


        // In your AutoMapper Profile (e.g., MappingProfile.cs)
        CreateMap<CreateJournalLineDTO, JournalLine>()
            .ForMember(dest => dest.JournalEntry, opt => opt.Ignore())  // Navigation properties
            .ForMember(dest => dest.Account, opt => opt.Ignore())
            .ForMember(dest => dest.CostCenter, opt => opt.Ignore())
            .ForMember(dest => dest.Dimension, opt => opt.Ignore());
        CreateMap<JournalLine, UpdateJournalLineDTO>().ReverseMap();
        CreateMap<JournalLine, JournalLineDTO>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryLinesDTO>().ReverseMap();



    }
}
