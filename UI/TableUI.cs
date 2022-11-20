class TableUI
{
    // https://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c
    private int TableWidth;
    public void PrintRooms(List<Room> objectList)
    {
        TableWidth = 85;
        Console.Clear();
        PrintLine();
        PrintRow("ID", "NAME", "BEDS", "GUESTS", "SIZE", "PRICE");
        PrintLine();
        foreach (var item in objectList)
        {
            PrintRow(item.id.ToString(), item.name, item.beds.ToString(), item.guests.ToString(), item.size.ToString(), item.price.ToString());
            PrintLine();
        }
    }
    public void PrintCustomers(List<Customer> objectList)
    {
        TableWidth = 70;
        Console.Clear();
        PrintLine();
        PrintRow("ID", "NAME", "EMAIL", "PHONENUMBER");
        PrintLine();
        foreach (var item in objectList)
        {
            PrintRow(item.ID.ToString(), item.Name, item.Email, item.Phonenumber.ToString());
            PrintLine();
        }
    }

    public void PrintReceipt(int roomID, DateTime dateTime, int duration, int totalSumConvert, Customer cust, int reservationID)
    {
        TableWidth = 85;
        Console.Clear();
        PrintLine();
        PrintRow("RESERVATION ID",reservationID.ToString());
        PrintLine();
        PrintRow("BOOKED ROOM", roomID.ToString());
        PrintLine();
        PrintRow("CHECK-IN DATE", dateTime.ToString("yyyy-MM-dd"));
        PrintLine();
        PrintRow("NUMBER OF NIGTHS", duration.ToString());
        PrintLine();
        PrintRow("TOTAL COST", totalSumConvert.ToString());
        PrintLine();
        PrintRow("BOOKED BY", cust.Name);
        PrintLine();
    }

    public void PrintEmployees(List<Employee> objectList)
    {
        TableWidth = 50;
        Console.Clear();
        PrintLine();
        PrintRow("ID", "NAME");
        PrintLine();
        foreach (var item in objectList)
        {
            PrintRow(item.id.ToString(), item.name);
            PrintLine();
        }
    }

    private void PrintLine()
    {
        Console.WriteLine(new string('-', TableWidth));
    }

    private void PrintRow(params string[] columns)
    {
        int width = (TableWidth - columns.Length) / columns.Length;
        string row = "|";

        foreach (string column in columns)
        {
            row += AlignCentre(column, width) + "|";
        }

        Console.WriteLine(row);
    }

    private string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
