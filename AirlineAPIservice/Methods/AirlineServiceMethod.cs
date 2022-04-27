using AirlineAPIservice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPIservice
{
    public class AirlineServiceMethod
    {
        public void AddtoinventoryMethod(string AirlineCode,DateTime OnboardingTime,string OnboardingPlace,string DistinationPlace, SqlConnection sqlcon)
        {
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("sp_Addtoinventorywithschedule", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AirlineCode", AirlineCode);
                cmd.Parameters.AddWithValue("@OnboardingTime", OnboardingTime);
                cmd.Parameters.AddWithValue("@OnboardingPlace", OnboardingPlace);
                cmd.Parameters.AddWithValue("@DistinationPlace", DistinationPlace);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public List<InvrntoryModel> searchAirline(string onboardingplace, string DistinationPlace, DateTime onboardingdate, SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from [Tbl_Airline_inventory] where OnboardingTime > getdate() and OnboardingPlace = '" + onboardingplace.ToString() + "';", sqlcon);
            var IN_airlines = new List<InvrntoryModel>();
            try
            {
                sqlcon.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            InvrntoryModel invrntory = new InvrntoryModel();

                            invrntory.AirlineIDCompany = reader.GetString(reader.GetOrdinal("AirlineIDCompany"));
                            invrntory.AirlineCode = reader.GetString(reader.GetOrdinal("AirlineCode"));
                            invrntory.AirplanType = reader.GetString(reader.GetOrdinal("AirplanType"));
                            invrntory.AirplanECOFare = reader.GetDecimal(reader.GetOrdinal("AirplanECOFare"));
                            invrntory.OnboardingTime = reader.GetDateTime(reader.GetOrdinal("OnboardingTime"));
                            invrntory.DistinationTime = reader.GetDateTime(reader.GetOrdinal("DistinationTime"));
                            invrntory.OnboardingPlace = onboardingplace;
                            invrntory.DistinationPlace = DistinationPlace;
                            IN_airlines.Add(invrntory);
                        }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                sqlcon.Close();
            }
            return IN_airlines;
        }

        internal void AddtoAirlinereg(List<AirlineModel> airlines, SqlConnection sqlcon)
        {
            
            try
            {
                sqlcon.Open();
                for (int i = 0; i < airlines.Count; i++)
                {
                    //var airline = new AirlineModel();
                    SqlCommand cmd = new SqlCommand("sp_airlineregister", sqlcon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AirlineIDCompany",airlines[i].AirlineIDCompany );
                    cmd.Parameters.AddWithValue("@AirlineCode", airlines[i].AirlineCode);
                    cmd.Parameters.AddWithValue("@AirplanType", airlines[i].AirplanType);
                    cmd.Parameters.AddWithValue("@AirplanBusFare", airlines[i].AirplanBusFare);
                    cmd.Parameters.AddWithValue("@AirplanECOFare", airlines[i].AirplanECOFare);
                    cmd.Parameters.AddWithValue("@MaxSeat", airlines[i].MaxSeat);
                    cmd.Parameters.AddWithValue("@AIRPORTID", airlines[i].AIRPORTID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }
}
