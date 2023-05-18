using LeavePlanner.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeavePlanner.Models.ViewModels
{
    public class CreateViewModel
    {
        #region Properties

        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "Najviše {1} znakova dopušteno")]
        [Display(Name = "Napomena")]
        public string Text { get; set; }
        public int? TextMaxLength { get; set; }

        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Test select lista")]
        public int TestId { get; set; }

        public List<SelectListItem> TestSelectList { get; set; }


        [Required(ErrorMessage = "Obavezno")]
        [Display(Name = "Test select lista")]
        public int CodexId { get; set; }

        public List<SelectListItem> CodexSelectList { get; set; }

        #endregion Properties


        public CreateViewModel()
        {
            TestSelectList = new List<SelectListItem>();
            CodexSelectList = new List<SelectListItem>();
        }

        public void PrepareDataAsync(IUnitOfWork unitOfWork)
        {
            TextMaxLength = 20;
            TestSelectList.Add(new SelectListItem
            {
                Text = "Prvi izbor",
                Value = "1"
            });

            TestSelectList.Add(new SelectListItem
            {
                Text = "Drugi izbor",
                Value = "2"
            });

            CodexSelectList.Add(new SelectListItem
            {
                Text = "Prvi izbor",
                Value = "1"
            });

            CodexSelectList.Add(new SelectListItem
            {
                Text = "Drugi izbor",
                Value = "2"
            });
        }
    }
}
