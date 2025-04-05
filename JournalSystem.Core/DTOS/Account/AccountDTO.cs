using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.Account
{
    public class CreateAccountDTO
    {
        public string AccountCode { get; set; }
        public string Name { get; set; }
    }

    public class UpdateAccountDTO
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
    }

    public class AccountResponseDTO
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
    }
}
