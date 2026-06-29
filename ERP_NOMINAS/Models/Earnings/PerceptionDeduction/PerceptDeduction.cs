using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ERP_NOMINAS.Models.PerceptionDeduction
{
   public class PerceptDeduction
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Tipo")]
        public string Type { get; set; }

        [DisplayName("Id Concepto")]
        public decimal IdConcept { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

        [DisplayName("Nombre Concepto")]
        public string NameConcept { get; set; }

        [DisplayName("Acumula")]
        public string Accumulate { get; set; }

        [DisplayName("Aplica 1")]
        public int Apply1 { get; set; }

        [DisplayName("Aplica 2")]
        public int Apply2 { get; set; }

        [DisplayName("Aplica 3")]
        public int Apply3 { get; set; }

        [DisplayName("Aplica 4")]
        public int Apply4 { get; set; }

        [DisplayName("Aplica 5")]
        public int Apply5 { get; set; }

        [DisplayName("Aplica 6")]
        public int Apply6 { get; set; }

        [DisplayName("Aplica 7")]
        public int Apply7 { get; set; }

        [DisplayName("Aplica 8")]
        public int Apply8 { get; set; }

        [DisplayName("Aplica 9")]
        public int Apply9 { get; set; }

        [DisplayName("Aplica 10")]
        public int Apply10 { get; set; }

        
        public int Order { get; set; }
        public int GraParcExe { get; set; }
    }
}
