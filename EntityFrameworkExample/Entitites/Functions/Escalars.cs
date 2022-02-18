using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Entitites.Functions
{
    public static class Escalars
    {
        public static void RegisterFunctions(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => BillDetailsAverage(0));
        }

        public static decimal BillDetailsAverage(int facturaId)
        {
            return 0;
        }
    }
}
