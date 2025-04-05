using AutoMapper;
using JournalSystem.Core.DTOS.Account;
using JournalSystem.Core.DTOS.CostCentre;
using JournalSystem.Core.DTOS.Currency;
using JournalSystem.Core.DTOS.Dimension;
using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.DTOS.JournalLines;
using JournalSystem.Core.Entities; // Ensure this namespace contains your entity classes

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


        CreateMap<CreateJournalEntryDTO, JournalEntry>();
        CreateMap<JournalEntry, UpdateJournalEntryDTO>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryDTO>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryWithLinesDTO>().ReverseMap();


        CreateMap<CreateJournalLineDTO, JournalLine>();
        CreateMap<JournalLine, UpdateJournalLineDTO>().ReverseMap();
        CreateMap<JournalLine, JournalLineDTO>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryLinesDTO>().ReverseMap();



    }
}
