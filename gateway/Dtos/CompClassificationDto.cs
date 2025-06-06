using System.ComponentModel.DataAnnotations;
namespace gatewayRoot.Dtos
{
    public enum CompClassificationDto
    {
        Unspecified = 0,
        [Display(Name = "Good Match")] // ! check what it does!
        GoodMatch = 1,
        FuckYou = 2,
        Ecommerce = 3,
        GimmeSomeLove = 4,
    }
}
