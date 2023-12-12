using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryTracking.Data;
using DeliveryTracking.Database;


namespace DeliveryTracking.UI
{
    public class ProgramUI
    {
        private readonly DeliveryRepository deliveryRepository = new DeliveryRepository();

        public ProgramUI()
        {
            deliveryRepository = new DeliveryRepository();
        }

        public void run()
        {
            RunApp();
        }

        private void RunApp()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                System.Console.WriteLine("Welcome to The Delivery Database what would you like to do? \n" +
                                         "1. Add A Delivery\n" +
                                         "2. Remove A Delivery\n" +
                                         "3. List All Deliveries EnRoute\n" +
                                         "4. List All Deliveries That Are Complete\n" +
                                         "5. List All Deliveries\n" +
                                         "6. List Delivery Based On Item Number\n" +
                                         "7. Update a deliveries status\n" +
                                         "8. Close Application\n");

                var userInput = int.Parse(Console.ReadLine()!);

                switch (userInput)
                {
                    case 1:
                        AddDelivery();
                        break;
                    case 2:
                        CancelDelivery();
                        break;
                    case 3:
                        ListAllEnRoute();
                        break;
                    case 4:
                        ListAllCompleted();
                        break;
                    case 5:
                        ListAllDeliveries();
                        break;
                    case 6:
                        GetDeliveryByItemNumber();
                        break;
                    case 7:
                        UpdateDeliveryStatus();
                        break;
                    case 8:
                        isRunning = false;
                        break;
                    default:
                    System.Console.WriteLine("please choose an option that is listed");
                        break;
                }
            }
        }

        private void UpdateDeliveryStatus()
        {
            Console.Clear();
            deliveryRepository.ListDeliveriesInList();

            // get item number
            System.Console.WriteLine("what is the item number of the delivery? ");
            int itemNumber = int.Parse(Console.ReadLine()!);
            // update the delivery with the new status
            System.Console.WriteLine("what is the new deliveries status? please spell it exactly as you see below\n" +
                                     "Scheduled\n" +
                                     "EnRoute\n" +
                                     "Complete\n" +
                                     "Canceled\n");
            Console.WriteLine("Enter a delivery status: \n");
            if (Enum.TryParse(Console.ReadLine(), out DeliveryTrackingStatus newStatus))
            {
                deliveryRepository.UpdateDeliveryStatus(itemNumber, newStatus);
            }
            else
            {
                System.Console.WriteLine("Invalid status input.");
            }
        }
        private void GetDeliveryByItemNumber()
        {
            Console.Clear();
            System.Console.WriteLine("what is the item number of the delivery? ");
            int userInput = int.Parse(Console.ReadLine()!);
            deliveryRepository.GetDeliveryByItemNumber(userInput);
        }

        private void ListAllCompleted()
        {
            Console.Clear();
            deliveryRepository.ListAllCompleted();
        }

        private void ListAllEnRoute()
        {
            Console.Clear();
            deliveryRepository.ListAllEnRoute();
        }
        private void ListAllDeliveries()
        {
            deliveryRepository.ListDeliveriesInList();
        }

        private void CancelDelivery()
        {
            Console.Clear();
            deliveryRepository.ListDeliveriesInList();
            System.Console.WriteLine("What is the item number of the order you want to cancel? ");
            int itemNumber = int.Parse(Console.ReadLine()!);
            deliveryRepository.CancelDelivery(itemNumber);
        }

        private void AddDelivery()
        {
            Console.Clear();
            deliveryRepository.ListDeliveriesInList();
            // new up a empty developer
            Delivery newDelivery = new Delivery();
            // set order date to today
            newDelivery.OrderDate = DateTime.Today;
            
            // set delivery date to 7 days from today
            DateTime d1 = DateTime.Today;
            newDelivery.DeliveryDate = d1.AddDays(7);
            // set the order quantity
            System.Console.WriteLine("What is the Order Quantity?");
            newDelivery.ItemQuantity = int.Parse(Console.ReadLine()!);
            // set the status as scheduled
            newDelivery.Status = DeliveryTrackingStatus.Scheduled;
            // call the method
            deliveryRepository.AddDelivery(newDelivery);
        }
    }
}