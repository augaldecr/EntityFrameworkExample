using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkExample.Entitites.Conversions
{
    public class CurrencyToSymbolConverter: ValueConverter<Currency, string>
    {
        public CurrencyToSymbolConverter()
            :base(
                    value => MapCurrencyString(value),
                    value => MapStringCurrency(value)
                 )
        {

        }

        private static string MapCurrencyString(Currency value)
        {
            return value switch
            {
                Currency.DominicanPeso => "RD$",
                Currency.Dollar => "$",
                Currency.Euro => "€",
                _ => ""
            };
        }

        private static Currency MapStringCurrency(string value)
        {
            return value switch
            {
                "RD$" => Currency.DominicanPeso,
                "$" => Currency.Dollar,
                "€" => Currency.Euro,
                _ => Currency.Unknown
            };
        }
    }
}
