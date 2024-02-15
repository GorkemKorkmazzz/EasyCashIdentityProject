using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.EntityLayer.Concrate
{
    public class CustomerAccount
    {
        public int CustomerAccountID { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountCurrency { get; set; } // hesabın türü örn tl ya da usd mi?
        public decimal CustomerAccountBalance { get; set; } // hesap bakiyesi
        public string BankBranch { get; set; } // hesabın hangi subeye bağlı olduğunun adı

        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }

    }
}
