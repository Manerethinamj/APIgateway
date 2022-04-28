using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using userAPIreg.Models;
using System.Collections;

namespace userAPIreg.Method
{
    public class UserviceMethod
    {
        internal void bookticket(SqlConnection bookconnection, int invrntoryID, string username, int n, string meal, string discount_info, List<travelerinformation> travelerinformations)
        {
            try
            {
                if (discount_info == null)
                {
                    discount_info = "NO CODE";
                }
                bookconnection.Open();
                SqlCommand cmd = new SqlCommand("sp_ticket_booking", bookconnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Invent_ID", invrntoryID);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@seatCount", n);
                cmd.Parameters.AddWithValue("@meal", meal);
                cmd.Parameters.AddWithValue("@discountcode", discount_info);
                cmd.ExecuteNonQuery();

                //hello
                for (int i = 0; i < n; i++)
                {
                    SqlCommand travellercmd = new SqlCommand("sp_add_traveller_info", bookconnection);
                    travellercmd.CommandType = CommandType.StoredProcedure;
                    travellercmd.Parameters.AddWithValue("@name", travelerinformations[i].name.ToString());
                    travellercmd.Parameters.AddWithValue("@age", Convert.ToInt32(travelerinformations[i].age.ToString()));
                    travellercmd.Parameters.AddWithValue("@gender",travelerinformations[i].gender.ToString());
                    travellercmd.Parameters.AddWithValue("@seatnumber", travelerinformations[i].seatinformation.ToString());
                    travellercmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bookconnection.Close();
            }
        }

        internal List<FetchuserModel> fetchuserbypnr(SqlConnection pnrconnection, string pnr)
        {
            List<FetchuserModel> fetchuserModels = new List<FetchuserModel>();
            try
            {
                pnrconnection.Open();
                SqlCommand cmd = new SqlCommand("SP_fetchPNR", pnrconnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pnr", pnr);                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        fetchuserModels.Add(new FetchuserModel
                        {
                            PNR = reader["PNR"].ToString(),
                            traveller_name = reader["traveller name"].ToString(),
                            age = Convert.ToInt32(reader["age"].ToString()),
                            Gender = reader["Gender"].ToString(),
                            AirlineIDCompany = reader["AirlineIDCompany"].ToString(),
                            AirlineCode = reader["AirlineCode"].ToString(),
                            email_id = reader["email_id"].ToString(),
                            booking_state = reader["booking_state"].ToString(),
                            OnboardingPlace = reader["OnboardingPlace"].ToString(),
                            DistinationPlace = reader["DistinationPlace"].ToString(),
                            seat_id = Convert.ToInt32(reader["seat_id"].ToString()),
                            OnboardingTime = Convert.ToDateTime(reader["OnboardingTime"].ToString()),
                            DistinationTime = Convert.ToDateTime(reader["DistinationTime"].ToString()),
                            meal_type = reader["meal_type"].ToString(),

                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                pnrconnection.Close();
            }
            return fetchuserModels;
        }

        internal List<FetchuserModel> fetchuserbyemail(SqlConnection emailconnection, string email_id)
        {
            List<FetchuserModel> fetchuserModels = new List<FetchuserModel>();
            try
            {
                emailconnection.Open();
                SqlCommand cmd = new SqlCommand("SP_fetchEmail", emailconnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email_id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        fetchuserModels.Add(new FetchuserModel
                        {
                            PNR = reader["PNR"].ToString(),
                            traveller_name = reader["traveller name"].ToString(),
                            age = Convert.ToInt32(reader["age"].ToString()),
                            Gender = reader["Gender"].ToString(),
                            AirlineIDCompany = reader["AirlineIDCompany"].ToString(),
                            AirlineCode = reader["AirlineCode"].ToString(),
                            email_id = reader["email_id"].ToString(),
                            booking_state = reader["booking_state"].ToString(),
                            OnboardingPlace = reader["OnboardingPlace"].ToString(),
                            DistinationPlace = reader["DistinationPlace"].ToString(),
                            seat_id = Convert.ToInt32(reader["seat_id"].ToString()),
                            OnboardingTime = Convert.ToDateTime(reader["OnboardingTime"].ToString()),
                            DistinationTime = Convert.ToDateTime(reader["DistinationTime"].ToString()),
                            meal_type = reader["meal_type"].ToString(),

                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                emailconnection.Close();
            }
            return fetchuserModels;
        }

        internal void Cancelbookedticket(SqlConnection sqlcon, string pnr, DateTime dateTime)
        {
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("sp_cancelBookedTicket", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pnr", pnr);
                cmd.Parameters.AddWithValue("@Currentdate", dateTime);
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
    }
}
