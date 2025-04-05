using AutoMapper;
using JournalSystem.Core.DTOS.Currency;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JournalSystem.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrencyResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var currencies = await _currencyRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CurrencyResponseDTO>>(currencies);
        }

        public async Task<CurrencyResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid currency ID", nameof(id));

            var currency = await _currencyRepository.GetByIdAsync(id, cancellationToken);
            if (currency == null) return null;

            return _mapper.Map<CurrencyResponseDTO>(currency);
        }

        public async Task AddAsync(CreateCurrencyDTO currencyDTO, CancellationToken cancellationToken = default)
        {
            if (currencyDTO == null) throw new ArgumentNullException(nameof(currencyDTO));

            var currency = _mapper.Map<Currency>(currencyDTO);
            await _currencyRepository.AddAsync(currency, cancellationToken);
        }

        public async Task UpdateAsync(UpdateCurrencyDTO currencyDTO, CancellationToken cancellationToken = default)
        {
            if (currencyDTO == null) throw new ArgumentNullException(nameof(currencyDTO));

            var currency = _mapper.Map<Currency>(currencyDTO);
            await _currencyRepository.UpdateAsync(currency, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid currency ID", nameof(id));

            await _currencyRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
