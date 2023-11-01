using Application.Dto;

namespace DeliveryOrder.Models
{
    public class AddOrder
    {
        public string AddressSender { get; set; }
        public string AddressRecipient { get; set; }
        public double Weight { get; set; }
        public DateTime PickupDt { get; set; }
        public int AddressSenderId { get; set; }
        public int AddressRecipientId { get; set; }
        public List<CityDto> Cities { get; set; }

    }
}
