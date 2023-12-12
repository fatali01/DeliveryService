namespace DeliveryTracking.Data;
public class Delivery
{
    public Delivery() {}

    public Delivery(DateTime orderDate, DateTime deliveryDate, DeliveryTrackingStatus status, int itemQuantity, int itemNumber, int customerId)
    {
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        Status = status;
        ItemNumber = itemNumber;
        ItemQuantity = itemQuantity;
        CustomerId = customerId;
    }
    
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public DeliveryTrackingStatus Status { get; set; }

    public int ItemNumber { get; set; }
    public int ItemQuantity { get; set; }
    public int CustomerId { get; set; }

    public override string ToString()
    {
        string str = $"Order Date: {OrderDate}\n" + 
                     $"Delivery Date: {DeliveryDate}\n" + 
                     $"Delivery Statuus: {Status}\n" + 
                     $"Item Number: {ItemNumber}\n" + 
                     $"Item Quantity: {ItemQuantity}\n" + 
                     $"Customer ID: {CustomerId}\n" +
                     "==================================================\n";
        
        return str;
        
    }
}
