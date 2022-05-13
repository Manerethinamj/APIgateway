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
        internal void AddtoinventoryMethod(string AirlineCode, DateTime OnboardingTime, string OnboardingPlace, string DistinationPlace, SqlConnection sqlcon)
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
        internal List<InvrntoryModel> searchAirline(string onboardingplace, string DistinationPlace, DateTime onboardingdate, SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from [Tbl_Airline_inventory] where OnboardingTime > getdate() and OnboardingPlace = '" + onboardingplace.ToString() + "' and DistinationPlace = '" + DistinationPlace.ToString() + "';", sqlcon);
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
                            invrntory.Invent_ID = reader.GetInt32(reader.GetOrdinal("Invent_ID"));
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

        internal void AddtoAirlinereg(AirlineModel airlines, SqlConnection sqlcon)
        {

            try
            {
                sqlcon.Open();

                    airlines.AIRPORTID = 1;
                    //var airline = new AirlineModel();
                    SqlCommand cmd = new SqlCommand("sp_airlineregister", sqlcon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AirlineIDCompany", airlines.AirlineIDCompany);
                    cmd.Parameters.AddWithValue("@AirlineCode", airlines.AirlineCode);
                    cmd.Parameters.AddWithValue("@AirplanType", airlines.AirplanType);
                    cmd.Parameters.AddWithValue("@AirplanBusFare", airlines.AirplanBusFare);
                    cmd.Parameters.AddWithValue("@AirplanECOFare", airlines.AirplanECOFare);
                    cmd.Parameters.AddWithValue("@MaxSeat", airlines.MaxSeat);
                    cmd.Parameters.AddWithValue("@AIRPORTID", airlines.AIRPORTID);
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

        internal void AddDiscount(string discountCode, string amounr, SqlConnection sqlcon)
        {
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("sp_add_discount", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DiscountCODE", discountCode);
                cmd.Parameters.AddWithValue("@Amount", amounr);
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

        internal List<AirlineModel> getcancelledAirlinedetails(SqlConnection sqlcon)
        {

                SqlCommand cmd = new SqlCommand("select * from [dbo].[Airline] where AirplanEndTime is not null;", sqlcon);
                var IN_airlines = new List<AirlineModel>();
                try
                {
                    sqlcon.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                AirlineModel airline = new AirlineModel();
                                airline.AirlineID = reader.GetInt32(reader.GetOrdinal("AirlineID"));
                                airline.AirlineIDCompany = reader.GetString(reader.GetOrdinal("AirlineIDCompany"));
                                airline.AirlineCode = reader.GetString(reader.GetOrdinal("AirlineCode"));
                                airline.AirplanType = reader.GetString(reader.GetOrdinal("AirplanType"));
                                airline.AirplanBusFare = reader.GetDecimal(reader.GetOrdinal("AirplanBusFare"));
                                airline.AirplanECOFare = reader.GetDecimal(reader.GetOrdinal("AirplanECOFare"));
                                airline.MaxSeat = reader.GetInt32(reader.GetOrdinal("MaxSeat"));

                                IN_airlines.Add(airline);
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

        internal List<InvrntoryModel> getAirlineinvent(SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from [Tbl_Airline_inventory] where OnboardingTime > getdate() ;", sqlcon);
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
                            invrntory.OnboardingPlace = reader.GetString(reader.GetOrdinal("OnboardingPlace"));
                            invrntory.DistinationPlace = reader.GetString(reader.GetOrdinal("DistinationPlace"));
                            invrntory.Invent_ID = reader.GetInt32(reader.GetOrdinal("Invent_ID"));
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

        internal List<AirlineModel> getAirlinedetails(SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from [dbo].[Airline] where AirplanEndTime is null;", sqlcon);
            var IN_airlines = new List<AirlineModel>();
            try
            {
                sqlcon.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            AirlineModel airline = new AirlineModel();
                            airline.AirlineID = reader.GetInt32(reader.GetOrdinal("AirlineID"));
                            airline.AirlineIDCompany = reader.GetString(reader.GetOrdinal("AirlineIDCompany"));
                            airline.AirlineCode = reader.GetString(reader.GetOrdinal("AirlineCode"));
                            airline.AirplanType = reader.GetString(reader.GetOrdinal("AirplanType"));
                            airline.AirplanBusFare = reader.GetDecimal(reader.GetOrdinal("AirplanBusFare"));
                            airline.AirplanECOFare = reader.GetDecimal(reader.GetOrdinal("AirplanECOFare"));
                            airline.MaxSeat = reader.GetInt32(reader.GetOrdinal("MaxSeat"));

                            IN_airlines.Add(airline);
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

        internal List<DiscountModel> getDiscount(SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from tbl_discount where discount_enddate >= GETDATE()", sqlcon);
            var IN_airlines = new List<DiscountModel>();
            try
            {
                sqlcon.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            DiscountModel discountModel = new DiscountModel();

                            discountModel.Discount_ID = reader.GetInt32(reader.GetOrdinal("Discount_ID"));
                            discountModel.Discount_startDate = reader.GetDateTime(reader.GetOrdinal("Discount_startDate"));
                            discountModel.Discount_EndDate = reader.GetDateTime(reader.GetOrdinal("Discount_EndDate"));
                            discountModel.Discount_Amount = reader.GetDecimal(reader.GetOrdinal("Discount_Amount"));
                            discountModel.Discount_Code = reader.GetString(reader.GetOrdinal("Discount_Code"));
                            

                            IN_airlines.Add(discountModel);
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


        internal DiscountModel getDiscountbycode(string discountcode,SqlConnection sqlcon)
        {

            SqlCommand cmd = new SqlCommand("select * from tbl_discount where Discount_Code ='"+discountcode+"';", sqlcon);
            DiscountModel discountModel = new DiscountModel();
            try
            {
                sqlcon.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            

                            discountModel.Discount_ID = reader.GetInt32(reader.GetOrdinal("Discount_ID"));
                            discountModel.Discount_startDate = reader.GetDateTime(reader.GetOrdinal("Discount_startDate"));
                            discountModel.Discount_EndDate = reader.GetDateTime(reader.GetOrdinal("Discount_EndDate"));
                            discountModel.Discount_Amount = reader.GetDecimal(reader.GetOrdinal("Discount_Amount"));
                            discountModel.Discount_Code = reader.GetString(reader.GetOrdinal("Discount_Code"));

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
            return discountModel;
        }


        internal void cancelSchedule(int inventory_ID,SqlConnection sqlcon)
        {

            SqlCommand command = new SqlCommand("update Tbl_Airline_inventory set OnboardingTime= '1999-09-19 00:00:00.000' where AirlineCode = " + inventory_ID+";", sqlcon);
            sqlcon.Open();
            command.ExecuteNonQuery();
            sqlcon.Close();

        }

        internal void Stopairlineservice(int AirlineID,string Airlinecode, SqlConnection sqlcon)
        {

            SqlCommand command = new SqlCommand("update [Airline] set AirplanEndTime= GETDATE() where AirlineID =" + AirlineID + ";", sqlcon);
            SqlCommand command1 = new SqlCommand("update Tbl_Airline_inventory set OnboardingTime= '1999-09-19 00:00:00.000' where AirlineCode = '" + Airlinecode+"';", sqlcon);
            sqlcon.Open();
            command.ExecuteNonQuery();
            command1.ExecuteNonQuery();
            sqlcon.Close();

        }


        internal void reactivateAirline(int AirlineID, SqlConnection sqlcon)
        {

            SqlCommand command = new SqlCommand("update [Airline] set AirplanEndTime= null where AirlineID =" + AirlineID + ";", sqlcon);
            sqlcon.Open();
            command.ExecuteNonQuery();
            sqlcon.Close();

        }
    }
}
