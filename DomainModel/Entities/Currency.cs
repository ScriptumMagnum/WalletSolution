using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency
    {
        public Currency(int id, string title, string code)
        {
            Id = id;
            Title = title;
            Code = code;
        }

        /// <summary>
        /// Идентификатор валюты
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование валюты
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Код валюты
        /// </summary>
        public string Code { get; set; }
    }
}
