﻿namespace TicketShop.ShoppingBasket.Dtos.Basket
{
    public class BasketDataTransferObject
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfItems { get; set; }
    }
}
