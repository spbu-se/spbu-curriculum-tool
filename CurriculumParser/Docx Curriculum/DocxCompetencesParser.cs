using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurriculumParser
{
    /// <summary>
    /// Содержит методы парсинга компетенций, формируемых всеми дисциплинами учебного плана
    /// </summary>
    static class DocxCompetencesParser
    {
        /// <summary>
        /// Парсит информацию о формируемых у студента компетенциях
        /// </summary>
        /// <param name="body">Тело документа</param>
        /// <returns>Список компетенций</returns>
        public static List<Competence> Parse(Body body)
        {
            try
            {
                var competences = new List<Competence>();
                var table = body.Elements<Table>().Skip(2).First();
                foreach (var row in table.Elements<TableRow>().Skip(1))
                {
                    var code = row.Elements<TableCell>().ElementAt(0).InnerText;
                    var description = row.Elements<TableCell>().ElementAt(1).InnerText;
                    competences.Add(new Competence(code, description));
                }
                return competences;
            }
            catch (Exception e)
            {
                throw new CurriculumParsingException("Ошибка парсинга компетенций .docx", e);
            }
        }
    }
}
