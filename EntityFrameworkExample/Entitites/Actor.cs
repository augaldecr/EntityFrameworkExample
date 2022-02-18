using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExample.Entitites
{
    public class Actor
    {
        public int Id { get; set; }
        private string _name;
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                _name = string.Join(' ',
                        value.Split(' ')
                        .Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray());
            }
        
        }
        public string Bio { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime? Birthday { get; set; }
        public HashSet<ActorMovie> ActorsMovies { get; set; }
        public string PhotoURL { get; set; }
        [NotMapped]
        public int? Age
        {
            get
            {
                if (!Birthday.HasValue)
                {
                    return null;
                }

                var birthday = Birthday.Value;

                var age = DateTime.Today.Year - birthday.Year;

                if (new DateTime(DateTime.Today.Year, birthday.Month, birthday.Day) > DateTime.Today)
                {
                    age--;
                }

                return age;

            }
        }
        public Address HomeAddress { get; set; }
        public Address BillingAddress { get; set; }
    }
}
