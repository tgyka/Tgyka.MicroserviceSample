﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgyka.Microservice.Rabbitmq.Events
{
    public class ProductUpdateEvent
    {
        public ProductUpdateEvent(int id, string name, string description, int price, int stock, int categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
