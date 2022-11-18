 class Reservation
{
    public int id { get; set; }
    public int room_id { get; set; }
    public int customer_id { get; set; }
    public int economy { get; set; }
    public DateTime date_in { get; set; }
    // public DateOnly FromDate { get; set; }
    public int duration { get; set; }

    public Reservation()
    {
        
    }

    public override string ToString()
    {
        return
        @$"
        Reservation id: {id}
        Room id: {room_id}
        Customer id: {customer_id}
        Date: {date_in}
        Number of Days: {duration}
        Total: {economy}
        ";
    }

}