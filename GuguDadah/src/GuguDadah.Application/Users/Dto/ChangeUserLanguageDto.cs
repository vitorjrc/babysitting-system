using System.ComponentModel.DataAnnotations;

namespace GuguDadah.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}