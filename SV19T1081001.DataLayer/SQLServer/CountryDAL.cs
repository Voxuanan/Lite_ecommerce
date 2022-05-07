using SV19T1081001.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081001.DataLayer.SQLServer
{
    public class CountryDAL : _BaseDAL, ICommonDAL<Country>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Country data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Country Get(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Country> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = OpenConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    @"SELECT CountryName FROM Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"]),
                    });
                }

                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// Không sử dụng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Country data)
        {
            throw new NotImplementedException();
        }
    }
}
