using TicketPurchaseAPI.Dto.EventDto;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Mapper
{
    public static class EventMapper
    {
        public static Event ToEventCreateDto(this EventCreateDto eventModel)
        {
            return new Event
            {
                Name = eventModel.Name,
                Description = eventModel.Description,
                Venue = eventModel.Venue,
                Capacity = eventModel.Capacity,
            };
        }


        public static EventGetDto ToEventGetDto(this Event eventModel)
        {
            return new EventGetDto
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
                Description = eventModel.Description,
                Venue = eventModel.Venue,
                Capacity = eventModel.Capacity,
            };
        }

        public static Event ToEventUpdateDto(this EventUpdateDto eventModel)
        {
            return new Event
            {
                Name = eventModel.Name,
                Description = eventModel.Description,
                Venue = eventModel.Venue,
                Capacity = eventModel.Capacity,
            };
        }
    }
}


