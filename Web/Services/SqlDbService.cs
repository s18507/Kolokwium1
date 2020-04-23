using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class SqlDbService : IPrescriptionsDbService
    {
        private const string connection = "Data Source=db-mssql;" +
                                         "Initial Catalog=s18507;Integrated Security=True";
        public IEnumerable<Prescription> GetPrescription(string lekarz)
        {
            var prescrip = new List<Prescription>();

            using (var con = new SqlConnection(connection))
            using (var commands = new SqlCommand())
            {
                commands.Connection = con;
                commands.CommandText = "SELECT p.IdPrescription, p.Date, p.DueDate, d.LastName 'DoctorsLastName', pat.LastName 'PatientsLastName' " +
                                       "FROM Doctor d " +
                                       "INNER JOIN Prescription p ON d.IdDoctor = p.IdDoctor " +
                                       "INNER JOIN Patient pat ON p.IdPatient = pat.IdPatient " +
                                       "WHERE d.LastName = @lekarz  Order by Date Desc";

                commands.Parameters.Add(new SqlParameter("lekarz", lekarz));

                con.Open();

                var dr = commands.ExecuteReader();

                while (dr.Read())
                {
                    prescrip.Add(
                        new Prescription
                        {
                            IdPrescription = Convert.ToInt32(dr["IdPrescription"].ToString()),
                            Date = dr["Date"].ToString(),
                            DueDate = dr["DueDate"].ToString(),
                            PatientLastName = dr["PatientsLastName"].ToString(),
                            DoctorLastName = dr["DoctorsLastName"].ToString()
                        });
                }
                dr.Close();
            }
        
            return prescrip;

        }
            

  
        public PrescriptionRequest AddPrescription(PrescriptionRequest request)
        {
            using (SqlConnection con = new SqlConnection(connection))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;

                com.CommandText = "Insert Into Prescription (Date, DueDate, IdPatient, IdDoctor) Values (@date, @dueDate, @IdPatient, @IdDoctor)";
                com.Parameters.AddWithValue("Date", request.date);
                com.Parameters.AddWithValue("DueDate", request.dueDate);
                com.Parameters.AddWithValue("IdPatient", request.IdPatient);
                com.Parameters.AddWithValue("IdDoctor", request.IdDoctor);

                com.ExecuteNonQuery();
                transaction.Commit();
            }
            return request;
        }

    }
}
