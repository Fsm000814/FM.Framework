using System.ComponentModel.DataAnnotations;

namespace FM.FrameWork.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}