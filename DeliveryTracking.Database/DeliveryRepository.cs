using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryTracking.Data;

namespace DeliveryTracking.Database
{
    public class DeliveryRepository
    {
        // fake database
        private List<Delivery> _deliveryDb = new List<Delivery>();
        private int _customerIdCount;
        private int _itemNumber;

        // whenever a new instance of DeliveryRepository is newed up this seed method will always add 3 deliveries from the seed method
        public DeliveryRepository()
        {
            _deliveryDb = new List<Delivery>();
            Seed();
        }

        public void Seed()
        {
            Delivery ali = new Delivery(DateTime.Today, new DateTime(2023, 12, 7), DeliveryTrackingStatus.Scheduled, 4, _itemNumber, _customerIdCount);
            _customerIdCount++;
            _itemNumber++;
            Delivery sam = new Delivery(new DateTime(2023, 11, 28), new DateTime(2023, 12, 9), DeliveryTrackingStatus.EnRoute, 6, _itemNumber, _customerIdCount);
            _customerIdCount++;
            _itemNumber++;
            Delivery david = new Delivery(new DateTime(2023, 11, 24), DateTime.Today, DeliveryTrackingStatus.Complete, 5, _itemNumber, _customerIdCount);
            _customerIdCount++;
            _itemNumber++;

            _deliveryDb.Add(ali);
            _deliveryDb.Add(sam);
            _deliveryDb.Add(david);
        }


        // C.R.U.D
        public bool AddDelivery(Delivery delivery)
        {
            if (delivery != null)
            {
                IncrementCustomerId(delivery);
                IncrementItemNumber(delivery);
                return AddDeliveryToDatabase(delivery);
            }
            else
            {
                System.Console.WriteLine($"delivery: {delivery} not found");
                return false;
            }
        }
        public bool CancelDelivery(int itemNumber)
        {
            // find the delivery you want to cancel
            var deliveryToCancel = _deliveryDb.FirstOrDefault(d => d.ItemNumber == itemNumber);

            // change status to canceled
            if (deliveryToCancel != null)
            {
                deliveryToCancel.Status = DeliveryTrackingStatus.Canceled;
                System.Console.WriteLine("Delivery Canceled");
                return true;
            }
            else
            {
                System.Console.WriteLine($"delivery: {deliveryToCancel} not found");
                return false;
            }
        }
        public List<Delivery> ListAllEnRoute()
        {
            // empty list
            List<Delivery> enRoute = new List<Delivery>();
            // for each delivery in are DataBase if it has a status of enroute then add it to the list of enroute
            // Check if the database is not null
            if (_deliveryDb != null)
            {
                // Iterate through each delivery in the database
                foreach (Delivery deliv in _deliveryDb)
                {
                    // Check if delivery has a status of EnRoute
                    if (deliv.Status == DeliveryTrackingStatus.EnRoute)
                    {
                        enRoute.Add(deliv);
                    }
                }
                return enRoute;
            }
            else
            {
                System.Console.WriteLine("there are no deliveries in the database");
                return enRoute;
            }
        }
        public List<Delivery> ListAllCompleted()
        {
            // empty list
            List<Delivery> complete = new List<Delivery>();
            // for each delivery in are DataBase if it has a status of Complete then add it to the list of complete
            // Check if the database is not null
            if (_deliveryDb != null)
            {
                // Iterate through each delivery in the database
                foreach (Delivery deliv in _deliveryDb)
                {
                    // Check if delivery has a status of complete
                    if (deliv.Status == DeliveryTrackingStatus.Complete)
                    {
                        complete.Add(deliv);
                    }
                }
                return complete;
            }
            else
            {
                System.Console.WriteLine("there are no deliveries in the database");
                return complete;
            }
        }
        public List<Delivery> ListDeliveriesInList()
        {
            List<Delivery> deliveries = new List<Delivery>();

            if (_deliveryDb != null)
            {
                foreach (Delivery deliv in _deliveryDb)
                {
                    System.Console.WriteLine(deliv);
                    deliveries.Add(deliv);
                }
            }
            else
            {
                System.Console.WriteLine("no deliveries found");
            }

            return deliveries;
        }
        public bool UpdateDeliveryStatus(int itemNumber, DeliveryTrackingStatus newDelivStatus)
        {
            // write all deliveries to console
            Console.WriteLine("\nList of Deliveries before Update: ");
            foreach (var delivery in _deliveryDb)
            {
                Console.WriteLine(delivery);
            }
            // find delivery based on the itemNumber or return null if not found
            Delivery oldDelivery = _deliveryDb.FirstOrDefault(d => d.ItemNumber == itemNumber)!;

            if (oldDelivery != null)
            {
                oldDelivery.Status = newDelivStatus;
                System.Console.WriteLine($"order status updated to {oldDelivery.Status}");
                return true;
            }
            else
            {
                System.Console.WriteLine($"No Delivery found under the item number: {itemNumber}");
                return false;
            }
        }

        public Delivery GetDeliveryByItemNumber(int itemNumber)
        {
            // find delivery based on itemnumber
            Delivery delivery = _deliveryDb.FirstOrDefault(d => d.ItemNumber == itemNumber)!;
            // print delivery to the console
            System.Console.WriteLine(delivery);
            // return delivery
            return delivery;
        }



        // helper method(s)
        private bool AddDeliveryToDatabase(Delivery delivery)
        {
            _deliveryDb.Add(delivery);
            return true;
        }
        private void IncrementItemNumber(Delivery delivery)
        {
            _itemNumber++;
            delivery.ItemNumber = _itemNumber;
        }
        private void IncrementCustomerId(Delivery delivery)
        {
            _customerIdCount++;
            delivery.CustomerId = _customerIdCount;
        }
    }
}
