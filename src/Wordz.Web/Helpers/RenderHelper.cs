using System.Net;
using Wordz.BE;

namespace Wordz.Web.Helpers
{
    public static class RenderHelper
    {
        public static string Button(string id, string caption, string onClickClientEvent = null, string extendedStyle = null)
        {
            return "<div style=\"" + WebUtility.HtmlEncode(extendedStyle.ReturnEmptyIfNull()) + 
                "\" class=\"actionbutton\" id=\"" + id + 
                "\" onclick=\"" + onClickClientEvent + "\" >" +
                   WebUtility.HtmlEncode(caption.ReturnEmptyIfNull()) + "</div>";
        }

        public static string ExerciseTableRow(string methodString, string name, string description, string type, int index)
        {
            return "<tr><td><a onclick=" + methodString + ">" + name + "</a></td><td>" + type + "</td><td>" +
                   description + "</td></tr>";
        }

        public static string ExerciseLiRow(string methodString, ExerciseBase exercise, int index)
        {
            var elementText = "<li class=\"ui-state-default\"/>";
            
            elementText += "<div style=\"overflow: auto;\"><div class=\"sortableItemDivleft\">";

            elementText += "<input type=\"hidden\" value=\"" + index + "\" name=\"exercise_" + exercise.Id + "_" + (int)exercise.ExerciseType + "\"/>";
            elementText += exercise.Name;
            elementText += "</div>";

            elementText += "<div class=\"sortableItemDivRight\">";
            elementText += "<button onclick=\"" + methodString + "\">Правка</button>";
            elementText += "</div>";
            elementText += "</div>";
            elementText += "</li>";
            return elementText;
        }
    }
}