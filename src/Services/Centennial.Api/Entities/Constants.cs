using System;
namespace Centennial.Api.Entities
{
    public class Constants
    {
        public enum StatusIds
        {
            Created = 1,
            Modified,
            Approved,
            Communicated,
            Closed,
            Deleted,
            Partial,
            PendingApproval,
            Rejected
        }

        public enum TransactionTypes
        {
            SentTo = 1,
            ReceivedFrom,
            AddedByStockRecord,
            SentToCustomer
        }
    }
}
