using static FribergHomeClient.Data.PropertyTypes;
using System.ComponentModel.DataAnnotations;
using FribergHomeClient.Data.Dto;
using AutoMapper;

namespace FribergHomeClient.Data.ViewModel
{
    public class PropertyFormViewModel
    {
        public int? Id { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal ListingPrice { get; set; }

        [Required]
        public decimal LivingSpace { get; set; }

        public decimal SecondaryArea { get; set; }

        public decimal LotSize { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public int NumberOfRooms { get; set; }

        [DataType(DataType.Currency)]
        public decimal MonthlyFee { get; set; }

        [DataType(DataType.Currency)]
        public decimal OperationalCostPerYear { get; set; }

        [Required]
        public int YearBuilt { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int MuncipalityId { get; set; }


        public string ImageUrls { get; set; }

        //Fredrik
        //public static implicit operator PropertyFormViewModel?(PropertyDTO? vmDto)
        //{
        //    PropertyFormViewModel model = Mapper.Map<PropertyFormViewModel>(vmDto);
        //    return model;
        //}
    }
}
