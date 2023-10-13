using Car.Models;
using Car.SqlFunction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private sqlFunction sqlFunction { get; } = null!;

        public CarController()
        {
            sqlFunction = new sqlFunction();
        }

        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public ResponseData<Cars> Get(int id)
        {
            ResponseData<Cars> responseData = new ResponseData<Cars>();
            string getdata = "SELECT * FROM Carlist where   Price>=@Price   ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value = id;
            parameters.Add(parameter); 
            DataTable dataTable = sqlFunction.startquery(getdata, parameters.ToArray());
            Cars cars = new Cars();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                cars.Id = Convert.ToInt32(dataRow["Id"]);
                cars.Brand = dataRow["Brand"].ToString() ??"";
                cars.Model = dataRow["Model"].ToString() ?? "";
                cars.CreatedAt = Convert.ToDateTime(dataRow["CreatedAt"]);
                cars.UpdatedAt = Convert.ToDateTime(dataRow["UpdatedAt"]);
                cars.Price = Convert.ToInt32(dataRow["Price"]);

            }
            //responseData.Response = "OK";
            int total = sqlFunction.numberadd(3,7);
            responseData.Response = total.ToString();

            return responseData;
        }

        // POST api/<CarController>
        [HttpPost]
        public ResponseData<string> Post([FromBody] Cars cars)
        {
            ResponseData<string> responseData = new ResponseData<string>();


            string insert = "INSERT INTO Carlist (Id,Brand,Model,CreatedAt,Price) VALUES (@Id,@Brand,@Model,@CreatedAt,@Price)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value = cars.Id;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Brand", SqlDbType.NVarChar, 500);
            parameter.Value = cars.Brand;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Model", SqlDbType.NVarChar, 50);
            parameter.Value = cars.Model;
            parameters.Add(parameter);
            parameter = new SqlParameter("@CreatedAt", SqlDbType.NVarChar, 50);
            parameter.Value = DateTime.UtcNow;
            parameters.Add(parameter);
            parameter = new SqlParameter("@UpdatedAt",SqlDbType.NVarChar, 50);
            parameter.Value = DateTime.UtcNow;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Price", SqlDbType.Int);
            parameter.Value = cars.Price;
            parameters.Add(parameter);

            int inserted = sqlFunction.startNonquery(insert, parameters.ToArray());

            if(inserted > 0)
            {
                responseData.Result = "OK";
            }
            return responseData;




        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public ResponseData<string> Put(int id, [FromBody]  Cars cars)
        {
            ResponseData<string> responseData = new ResponseData<string>();
            string updatedata = "UPDATE Carlist SET  Id = @Id, Brand = @Brand , Model = @Model , UpdatedAt = @UpdatedAt , Price = @Price    WHERE Id = @Id ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value = id;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Brand",SqlDbType.NVarChar, 500);
            parameter.Value = cars.Brand;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Model", SqlDbType.NVarChar, 50);
            parameter.Value = cars.Model;
            parameters.Add(parameter);
            parameter = new SqlParameter("@UpdatedAt", SqlDbType.DateTime);
            parameter.Value = DateTime.UtcNow;
            parameters.Add(parameter);
            parameter = new SqlParameter("@Price",SqlDbType.Int);
            parameter.Value = cars.Price;
            parameters.Add(parameter);
            int updated = sqlFunction.startNonquery(updatedata, parameters.ToArray());
            if (updated == 1)
            {
                responseData.Response = "更新資料成功";
            }
            return responseData;
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public ResponseData<string> Delete(int id)
        {
            ResponseData<string> responseData = new ResponseData<string>();
            string deletedata = "DELETE Carlist   WHERE Id = @Id ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter parameter;
            parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value=id;
            parameters.Add(parameter);
            int deleted = sqlFunction.startNonquery(deletedata, parameters.ToArray());

            responseData.Response = "OK";
            return responseData;


        }
    }
}
