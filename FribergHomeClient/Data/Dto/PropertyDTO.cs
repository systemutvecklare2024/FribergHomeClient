using System.ComponentModel.DataAnnotations;
using static FribergHomeClient.Data.PropertyTypes;

namespace FribergHomeClient.Data.Dto
{
	public class PropertyDTO
	{
		public int? Id { get; set; }

		[DataType(DataType.Currency)]
		public decimal ListingPrice { get; set; }

		public decimal LivingSpace { get; set; }

		public decimal SecondaryArea { get; set; }

		public decimal LotSize { get; set; }
		public string Description { get; set; }
		public int NumberOfRooms { get; set; }

		[DataType(DataType.Currency)]
		public decimal MonthlyFee { get; set; }

		[DataType(DataType.Currency)]
		public decimal OperationalCostPerYear { get; set; }
		public int YearBuilt { get; set; }
		public PropertyType PropertyType { get; set; }

		public string Street { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public int MuncipalityId { get; set; }

		public List<PropertyImageDTO> ImageUrls { get; set; }

    }
}
