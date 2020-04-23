using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IPrescriptionsDbService
    {
        public IEnumerable<Prescription> GetPrescription(string lekarz);

        public PrescriptionRequest AddPrescription(PrescriptionRequest animal);

    }
}
