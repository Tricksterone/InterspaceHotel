﻿using MySqlConnector;
using Dapper;
internal class Program
{
    const string CONNECTIONSTRING = "Server = localhost;Database = interspace_hotel;Uid=root; Convert Zero Datetime=True";

    private static void Main(string[] args)
    {
        MySqlConnection connection = new MySqlConnection(CONNECTIONSTRING);
        RoomDB roomDB = new(connection);
        //RoomManagement roomManager = new(roomDB);
        EmployeeDB employeeDB = new(connection);
        EmployeeManagement empManager = new(employeeDB);
        ReservationDB reservations = new(connection);

        CustomerDB cDB = new CustomerDB();
        CustomerManagement cm = new(cDB);

        // List<Customer> customerList = cm.GetAllCustomers(); - funkar
        // PrintCustomers(customerList); - funkar 

        // // Room listRooms = new();  EMELIE HAR TILLFÄLLIGT KOMMENTERAT UT DENNA!

        // UI ui = new();
        // ui.Start();

        // AddCustomer(cm); - FUNKAR
        // UpdateCustomer(cm, id); - !!!!!!FUNKAR INTE!!!!!
        // int id = GetCustomer(cm); - FUNKAR
        // RemoveCustomer(cm); - FUNKAR

        MakeReservation(reservations);

            
        //     @$" {roomChoiceConvert}
        
        // int resultat = reservations.CreateRoomReservation(roomChoiceConvert, customerIDConvert, fromDate, durationConvert, totalSumConvert);
        //     Console.WriteLine(Reservation gjord:  + resultat);
            
        //     ");
        //UpdateReservation(reservations);
        //DeleteReservation(reservations);

        //UpdateRoom(roomDB);
        //RemoveRoombyID(roomDB);

        //UpdateEmployee(employeeDB);
        //CreateEmployee(employer);

        // var reservation = reservations.ListReservations();
        // foreach (Reservation item in reservation)
        // {
        //     Console.WriteLine(item);
        // }
        //Skriver ut lista på all personal i databasen med ID samt Namn
        // var emp = employer.ListEmployees();
        // foreach (Employee employees in emp)
        // {
        //     Console.WriteLine(employees);
        // }
    }

    private static void AddCustomer(CustomerManagement customerM)
    {
        // HEADER
        Console.WriteLine("ADD CUSTOMER");
        Console.Write("Firstname : ");
        string firstName = Console.ReadLine();
        Console.Write("Lastname : ");
        string lastName = Console.ReadLine();
        Console.Write("Email : ");
        string email = Console.ReadLine();
        Console.Write("Phonenumber : ");
        string phoneNumber = Console.ReadLine();
        Console.WriteLine(customerM.AddCustomer(email, firstName, lastName, phoneNumber));
    }

    private static void UpdateCustomer(CustomerManagement customerM, int id)
    {
        // HEADER
        Console.WriteLine("UPDATE CUSTOMER");
        Console.Write("Firstname : ");
        string firstName = Console.ReadLine();
        Console.Write("Lastname : ");
        string lastName = Console.ReadLine();
        Console.Write("Email : ");
        string email = Console.ReadLine();
        Console.Write("Phonenumber : ");
        string phoneNumber = Console.ReadLine();
        Console.WriteLine(customerM.UpdateCustomer(email, firstName, lastName, phoneNumber, id));
    }

    private static int GetCustomer(CustomerManagement customerM)
    {
        // HEADER
        int id = 0;
        Console.WriteLine("SELECT CUSTOMER BY ID");
        Console.Write("ID : ");
        try
        {
            id = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.Exception)
        {
            Console.WriteLine("WRONG INPUT");
        }
        Console.WriteLine(customerM.GetCustomer(id));
        return id;
    }

    private static void PrintCustomers(List<Customer> customerList)
    {
        foreach (Customer information in customerList)
        {
            Console.WriteLine(information);
        }
    }

    private static void RemoveCustomer(CustomerManagement customerM)
    {
        // HEADER
        int id = 0;
        Console.WriteLine("SELECT CUSTOMER BY ID");
        Console.Write("ID : ");
        try
        {
            id = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.Exception)
        {
            Console.WriteLine("WRONG INPUT");
        }
        Console.WriteLine(customerM.RemoveCustomer(id));
    }

    private static void DeleteReservation(ReservationDB reservations)
    {
        Console.WriteLine("Ange vilken reservation du vill ta bort, ange id:et");
        string deleteReservation = Console.ReadLine();
        int deleteConvert = Convert.ToInt32(deleteReservation);
        reservations.DeleteReservation(deleteConvert);
    }

    private static void UpdateReservation(ReservationDB reservations)
    {
        try
        {
            Console.WriteLine("ange reservations ID");
            string reservationid = Console.ReadLine();
            int idConvert = Convert.ToInt32(reservationid);
            Reservation reservation = reservations.GetReservationById(idConvert);

            Console.WriteLine("Ange nya rums id:et du vill flytta gästen till");
            string updateRoom = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(updateRoom))
            {
                reservation.room_id = Convert.ToInt32(updateRoom);
            }

            Console.WriteLine("Ange datum ändring");
            string date = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(date))
            {
                reservation.date_in = DateTime.Parse(date);
            }

            Console.WriteLine("Ange antal dagar");
            string duration = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(duration))
            {
                reservation.duration = Convert.ToInt32(duration);
            }

            Console.WriteLine("Ange nytt pris");
            string price = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(price))
            {
                reservation.economy = Convert.ToInt32(price);
            }

            reservations.UpdateReservation(reservation);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void MakeReservation(ReservationDB reservations)
    {
        //Behöver ses över med felhantering då det finns mycket ReadLines samt punkt2. då det just nu kräver användaren att ange ett ID.
        //skulle även behövas en form av validering som kollar ifall rummet är ledigt eller ej.
        try
        {
            Console.WriteLine("Välj rummet du vill boka");
            string roomChoice = Console.ReadLine();
            int roomChoiceConvert = Convert.ToInt32(roomChoice);

            Console.WriteLine("Ange ditt id.");
            string customerID = Console.ReadLine();
            int customerIDConvert = Convert.ToInt32(customerID);

            Console.WriteLine("Ange när du vill reservera rummet. Ange i siffror tex 2022-11-25");
            string dateInput = Console.ReadLine();
            DateTime fromDate = DateTime.Parse(dateInput);

            Console.WriteLine("Du har bokat: " + fromDate);
            Console.WriteLine("Ange hur många dagar du vill stanna.");
            string duration = Console.ReadLine();
            int durationConvert = Convert.ToInt32(duration);

            Console.WriteLine("Ange totalsumma");
            string totalSum = Console.ReadLine();
            int totalSumConvert = Convert.ToInt32(totalSum);
            Console.WriteLine(reservations.ToString());
            int resultat = reservations.CreateRoomReservation(roomChoiceConvert, customerIDConvert, fromDate, durationConvert, totalSumConvert);
            Console.WriteLine("Reservation gjord: " + resultat);

            Console.WriteLine($@"
            Här är ditt kvitto på din reservation:
            Bokat rum: {roomChoiceConvert}
            Inceckning: {fromDate}
            Antal bokade nätter: {duration}
            Kostnad : {totalSumConvert}
            Bokad av : {customerIDConvert}
            
            Vid kontakt gällande ändringar eller avbokning, vänligen uppge {resultat}");
        }
        catch (System.Exception e)
        {

            Console.WriteLine("Fel: " + e);
        }
    }
    private static void UpdateEmployee(EmployeeDB employeeDB)
    {
        Console.WriteLine("Vilken personal vill du uppdatera? skriv in id siffra.");
        string id = Console.ReadLine();
        int employeeIDConvert = Convert.ToInt32(id);
        Employee updateEmployee = employeeDB.GetEmployeeById(employeeIDConvert);

        Console.WriteLine("Skriv in namn");
        string employeeName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(employeeName))
        {
            updateEmployee.name = employeeName;
        }
        Console.WriteLine("Skriv in lösenord");
        string employeePassword = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(employeePassword))
        {
            updateEmployee.password = employeePassword;
        }
        employeeDB.UpdateEmployee(updateEmployee);
    }

    private static void CreateEmployee(EmployeeDB employeeDB)
    {
        Console.WriteLine("Skriv in namn");
        string nameInput = Console.ReadLine();
        Console.WriteLine("Skriv in lösenord");
        string passwordInput = Console.ReadLine();
        var createEmployee = employeeDB.CreateEmployee(nameInput, passwordInput);
        Console.WriteLine($"Lägger in ny personal {nameInput}");
    }

    private static void RemoveRoombyID(RoomDB roomDB)
    {
        Console.WriteLine("Skriv in ett rums id du vill ta bort");
        string idRemove = Console.ReadLine();
        int idRemoveConvert = Convert.ToInt32(idRemove);
        roomDB.DeleteRoom(idRemoveConvert);
    }

    private static void UpdateRoom(RoomDB roomDB)
    {
        Console.WriteLine("type in room id");
        string id = Console.ReadLine();
        int idconvert = Convert.ToInt32(id);
        Room updateRoom = roomDB.GetRoomByid(idconvert);

        Console.WriteLine("type in number of beds");
        string Beds = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(Beds))
        {
            updateRoom.beds = Convert.ToInt32(Beds);
        }

        Console.WriteLine("Type in max amount of guests");
        string guests = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(guests))
        {
            updateRoom.guests = Convert.ToInt32(guests);
        }

        Console.WriteLine("type in size");
        string size = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(size))
        {
            updateRoom.size = Convert.ToInt32(size);
        }

        Console.WriteLine("type in price");
        string price = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(price))
        {
            updateRoom.price = Convert.ToInt32(price);
        }
        roomDB.UpdateRoom(updateRoom);
    }
}