using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class ItemService : DataBaseService<Item>
    {
        public ItemService(IRepository<Item> itemRepository) : base(itemRepository) { }

        public void UpdateManufacturers(Manufacturer manufacturer)
        {
            var setName= Builders<Item>.Update.Set(i => i.Manufacturer.Name, manufacturer.Name);
            repository.UpdateMany(i => i.Manufacturer.Id == manufacturer.Id, setName);
        }

        public void UpdateCutomers(Customer customer)
        {
            var setName = Builders<Item>.Update.Set(i => i.Customer.Name, customer.Name);
            repository.UpdateMany(i => i.Customer.Id == customer.Id, setName);
        }

        public void DeleteCutomers(int id)
        {
            var setCustomer = Builders<Item>.Update.Set(i => i.Customer, null);
            repository.UpdateMany(i => i.Customer.Id == id, setCustomer);
        }

        public void OnManufacturerDelete(int id)
        {
            repository.DeleteMany(i => i.Manufacturer.Id == id);
        }
    }
}
