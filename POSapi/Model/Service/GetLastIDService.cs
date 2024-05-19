namespace POSapi.Model.Service
{
    public interface GetLastIDService
    {
        int GetLastID();
    }

    public class GetID : GetLastIDService { 
        private readonly DemoDbContext _context;

        public GetID(DemoDbContext context)
        {
            _context = context;
        }

        public int GetLastID()
        {
            var lastID = _context.Invoices.OrderByDescending(i=>i.InvoiceId).FirstOrDefault();
            if(lastID != null)
            {
                return lastID.InvoiceId;
            }
            else
            {
                throw new Exception("No invoice found in the database.");
            }
        }
    }
}
