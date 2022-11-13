using Dapper;
using MySqlConnector;

class CustomerDB
{
    private MySqlConnection _sqlConnection;
    public CustomerDB()
    {
        Connect();
    }

    public void Connect()
    {
        _sqlConnection = new MySqlConnection("Server = localhost;Database = interspace_hotel;Uid=root");
    }

    public int InsertCustomer(Customer cu)
    {
        string sql = $@"INSERT INTO `customer` 
            (`email`, `first_name`, `last_name`, `phonenumber`)
            VALUES ('{cu.Email}', '{cu.First_Name}', '{cu.Last_Name}', '{cu.Phonenumber}');
            SELECT LAST_INSERT_ID()";

        int id = _sqlConnection.QuerySingle<int>(sql);
        return id;
    }

    public List<Customer> GetCustomers()
    {
        var customerList = _sqlConnection.Query<Customer>($@"SELECT * FROM `customer`").ToList();
        return customerList;
    }

    public Customer SelectCustomer(int id)
    {
        // https://stackoverflow.com/questions/14171794/how-to-retrieve-data-from-a-sql-server-database-in-c
        Customer cu = new();
        string sql = $@"SELECT * FROM `customer` WHERE `customer`.`id`= {id}";
        cu.ID = id;
        MySqlCommand cmd = new MySqlCommand(sql, _sqlConnection);
        _sqlConnection.Open();
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                cu.Email = reader["email"].ToString();
                cu.First_Name = reader["first_name"].ToString();
                cu.Last_Name = reader["last_name"].ToString();
                cu.Phonenumber = reader["phonenumber"].ToString();
            }
            _sqlConnection.Close();
        }
        return cu;
    }

    public void UpdateCustomer(Customer cu)
    {
        string sql = $@"UPDATE `customer` SET
            (`email`, `first_name`, `last_name`, `phonenumber`)
            VALUES ('{cu.Email}', '{cu.First_Name}', '{cu.Last_Name}', '{cu.Phonenumber}'
            WHERE `id` = {cu.ID});
            SELECT LAST_INSERT_ID()";

        int id = _sqlConnection.QuerySingle<int>(sql);
    }

    public void DeleteCustomer(int id)
    {
        int deleteID = _sqlConnection.Execute($@"DELETE FROM `customer` WHERE `customer`.`id` = {id}");
    }

    public List<Customer> SearchCustomerDB(string search)
    {
        var customerList = _sqlConnection.Query<Customer>($@"
        SELECT* FROM customer 
        WHERE phonenumber OR email OR first_name OR last_name 
        LIKE %{search}%").ToList();
        
        return customerList;
    }
}