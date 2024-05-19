namespace POSapi
{
    using AutoMapper;
    using POSapi.Model.Data;
    using POSapi.Model.Request;

    public class AppMapperProfile:Profile
    {
        public AppMapperProfile() {
            CreateMap<PORequest, PurchaseOrder>();
            CreateMap<PODetailRequest, PODetail>();
            CreateMap<InvoiceRequest, Invoice>();
            CreateMap<RequestInvoiceItem, InvoiceItem>();
        }
    }
}
