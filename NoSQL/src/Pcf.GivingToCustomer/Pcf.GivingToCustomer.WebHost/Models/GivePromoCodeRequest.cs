using Pcf.GivingToCustomer.Core.Domain.Enums;
using System;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    public class GivePromoCodeRequest
    {
        public string ServiceInfo { get; set; }
        public string PromoCode { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public Guid PartnerId { get; set; }
        public Preference Preference { get; set; }
    }
}