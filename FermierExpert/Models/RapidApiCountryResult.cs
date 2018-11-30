using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Models
{
    public class RapidApiCountryResult
    {

        public string Name { get; set; }
        public string[] TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public long[] CallingCodes { get; set; }
        public string Capital { get; set; }
        public string[] AltSpellings { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public long[] Latlng { get; set; }
        public string Demonym { get; set; }
        public long Area { get; set; }
        public long Gini { get; set; }
        public string[] Timezones { get; set; }
        public string[] Borders { get; set; }
        public string NativeName { get; set; }
        public long NumericCode { get; set; }
        public string[] Currencies { get; set; }
        public string[] Languages { get; set; }
        public RapidApiCountryTranslations Translations { get; set; }
        public long Relevance { get; set; }
                       
    }
    public class RapidApiCountryTranslations
    {
        public string De { get; set; }
        public string Es { get; set; }
        public string Fr { get; set; }
        public string Ja { get; set; }
        public string It { get; set; }
    }
}
