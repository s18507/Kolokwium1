using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
  
        public class Prescription
        {
            public int IdPrescription { get; set; }
            public string Date { get; set; }
            public string DueDate { get; set; }
            public string PatientLastName { get; set; }
            public string DoctorLastName { get; set; }
        }


}
