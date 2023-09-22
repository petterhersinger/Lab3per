using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class ViewModelPA
    {
        public IEnumerable<PersonAktivitetModel> PersonAktivitetModelLista { get; set; }
        public IEnumerable<AktivitetModel> AktivitetModelLista { get; set; }
        public IEnumerable<AktivitetModel> AktivitetModelLista2 { get; set; }
    }
}
