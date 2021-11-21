using Wordz.BE;

namespace Wordz.Web.Helpers
{
    public static class UrlHelper
    {
        public static string GetViewUrl(this ExerciseType exerciseType)
        {
            switch (exerciseType)
            {
                case ExerciseType.Text:
                    return "/ExerciseText"; 
                case ExerciseType.Select:
                    return "/ExerciseSelect.aspx?Id=";
                case ExerciseType.TextAnswer:
                    return "/ExerciseTAnswer";
                case ExerciseType.SelectText:
                    return "/ExerciseSelectText.aspx?Id=";
                case ExerciseType.SkipText:
                    return "/ExerciseSkipText";
                default:
                    return "/";
            }
        }
    }
}