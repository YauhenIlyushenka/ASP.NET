using Pcf.ReceivingFromPartner.Core.Abstractions.Repositories;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.DataAccess.Abstractions;
using System;

namespace Pcf.ReceivingFromPartner.DataAccess.Repository
{
	public class PartnerRepository : BaseRepostory<Partner, Guid>, IRepository<Partner, Guid>
	{
		public PartnerRepository(DataContext context) : base(context)
		{ }
	}
}
