using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Wallet
{
    public class WalletDTO
    {
        public Guid Id { get; set; }
        public long Balance { get; set; }
    }
}
