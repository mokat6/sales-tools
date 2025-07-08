using System.ComponentModel.DataAnnotations;
namespace gatewayRoot.Dtos
{
    public enum CompClassificationDto
    {
        Unspecified = 0,

        [Display(Name = "Good Match")]
        GoodMatch = 1 << 0, // 1

        [Display(Name = "Maybe")]
        Maybe = 1 << 1,     // 2 

        [Display(Name = "Ecommerce")]
        Ecommerce = 1 << 2, // 4

        [Display(Name = "Gimme Some Love")]
        GimmeSomeLove = 1 << 3 // 8
    }
}
