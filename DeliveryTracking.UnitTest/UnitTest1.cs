
using System.Collections.ObjectModel;
using System.Reflection;
using DeliveryTracking.Data;
using DeliveryTracking.Database;
using Microsoft.VisualBasic;

namespace DeliveryTracking.Tests
{
    public class DeliveryRepositoryTests
    {
        private readonly DeliveryRepository _deliveryRepository;

        public DeliveryRepositoryTests()
        {
            // Constructor will be used as setup in xUnit
            _deliveryRepository = new DeliveryRepository();
        }

        [Fact]
        public void AddDelivery_ShouldAddDeliveryToDatabase()
        {
            // Arrange
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Scheduled, 4, 5, 5);

            // Act
            var result = _deliveryRepository.AddDelivery(delivery);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CancelDelivery_ShouldCancelDelivery()
        {
            // Arrange
            // Add a delivery to the database for cancellation
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Scheduled, 4, 5, 5);
            _deliveryRepository.AddDelivery(delivery);

            // Act
            var result = _deliveryRepository.CancelDelivery(delivery.ItemNumber);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ListAllEnRoute_ShouldReturnEnRouteDeliveries()
        {
            // Arrange
            // Add EnRoute deliveries to the database
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.EnRoute, 4, 5, 5);
            _deliveryRepository.AddDelivery(delivery);


            // Act
            var enRouteDeliveries = _deliveryRepository.ListAllEnRoute();

            // Assert
            // check if list of enRouteDeliveries are not null
            Assert.NotNull(enRouteDeliveries);

            // Check if enRouteDeliveries contains the expected deliveries
            foreach (var enRouteDelivery in enRouteDeliveries)
            {
                Assert.Equal(DeliveryTrackingStatus.EnRoute, enRouteDelivery.Status);
            }

        }

        [Fact]
        public void ListAllComplete_ShouldReturnCompletedDeliveries()
        {
            // Arrange
            // Add completed delivery to the database
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Complete, 4, 5, 5);
            _deliveryRepository.AddDelivery(delivery);


            // Act
            var completedDeliveries = _deliveryRepository.ListAllCompleted();

            // Assert
            // check if list of completed deliveries are not null
            Assert.NotNull(completedDeliveries);

            // Check if completed deliveries contains the expected deliveries
            foreach (var completedDelivery in completedDeliveries)
            {
                Assert.Equal(DeliveryTrackingStatus.Complete, completedDelivery.Status);
            }

        }

        [Fact]
        public void ListDeliveriesInList_ShouldReturnDeliveries()
        {
            // Arrange
            // Add deliveries to the repository
            var delivery1 = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Complete, 4, 5, 5);
            var delivery2 = new Delivery(DateTime.Today, DateTime.Today.AddDays(14), DeliveryTrackingStatus.Complete, 4, 5, 5);
            _deliveryRepository.AddDelivery(delivery1);
            _deliveryRepository.AddDelivery(delivery2);

            // Act
            var allDeliveries = _deliveryRepository.ListDeliveriesInList();

            // Assert
            // Check if the list of allDeliveries is not null
            Assert.NotNull(allDeliveries);

            // Check if the list contains the expected deliveries
            Assert.Contains(delivery1, allDeliveries);
            Assert.Contains(delivery2, allDeliveries);
        }

        [Fact]
        public void UpdateDeliveryStatus_ShouldUpdateStatus()
        {
            // Arrange
            // Add a delivery to the repository
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Scheduled, 1, 5, 5);
            _deliveryRepository.AddDelivery(delivery);

            // Act
            var updated = _deliveryRepository.UpdateDeliveryStatus(1, DeliveryTrackingStatus.EnRoute);

            // Assert
            // Check if the status was updated successfully
            Assert.True(updated);
        }
        
        [Fact]
        public void ListDeliveryByItemNumber_ShouldReturnDeliveryByNumber()
        {
            // Arrange
            // Add a delivery to the repository
            var delivery = new Delivery(DateTime.Today, DateTime.Today.AddDays(7), DeliveryTrackingStatus.Scheduled, 1, 5, 5);
            _deliveryRepository.AddDelivery(delivery);

            // Act
            var listedDelivery = _deliveryRepository.GetDeliveryByItemNumber(delivery.ItemNumber);
            // Assert
            Assert.True(listedDelivery == delivery);
        }
    }
}