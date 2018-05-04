using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class AddressRepo
    {
        private DataContext _db;

        public AddressRepo()
        {
            _db = new DataContext();
        }

        /*public List<AddressListViewModel> GetAllAddresses()
        {
            var addresses = (from a in _db.Addresses              // Skipta þessari breytu inn þegar við erum komin með database link.
                            select new AddressListViewModel
                            {
                                Id = a.Id,
                                Street = a.Street,
                                HouseNumber = a.HouseNumber,
                                City = a.City,
                                Country = a.Country,
                                PostalCode = a.PostalCode
                            }).ToList();
            return addresses;
        }*/
    }
}