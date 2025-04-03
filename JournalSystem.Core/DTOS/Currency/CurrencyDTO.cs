using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.Currency
{
    public class CreateCurrencyDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float ExchangeRate { get; set; }
    }

    public class UpdateCurrencyDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float ExchangeRate { get; set; }
    }

    public class CurrencyResponseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float ExchangeRate { get; set; }
    }
}
