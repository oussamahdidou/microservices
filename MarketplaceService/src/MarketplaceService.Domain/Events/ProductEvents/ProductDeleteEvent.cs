﻿using EventsContracts.EventsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceService.Domain.Events.ProductEvents
{
    public class ProductDeleteEvent : IProductDeleteEvent
    {
        public string Id { get; set; }
    }
}