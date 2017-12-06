using System.Collections.Generic;


namespace CS4790ME.Models
{
    public class Repository
    {
        public static VehicleServiceEvent getVehicleAndServiceEvents(int? id)
        {
            VehicleServiceEvent vehicleServiceEvent = BasicVehicle.getVehicleAndServiceEvents(id);
            return vehicleServiceEvent;
        }

        public static List<Vehicle> getVehicles()
        {
            return BasicVehicle.getVehicles();
        }

        public static Vehicle getVehicleDetails(int? id)
        {
            return BasicVehicle.getVehicleDetails(id);
        }

        public static void createVehicle(Vehicle vehicle)
        {
            BasicVehicle.createVehicle(vehicle);
        }

        public static void deleteVehicle(Vehicle vehicle)
        {
            BasicVehicle.deleteVehicle(vehicle);
        }

        public static Vehicle getVehicle(int? id)
        {
            return BasicVehicle.getVehicle(id);
        }

        public static void editVehicle(Vehicle vehicle)
        {
            BasicVehicle.editVehicle(vehicle);
        }

        public static List<ServiceEvent> getServiceEvents()
        {
            return BasicVehicle.getServiceEvents();
        }

        public static ServiceEvent getServiceEventDetails(int? id)
        {
            return BasicVehicle.getServiceEventDetails(id);
        }

        public static void createServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicle.createServiceEvent(serviceEvent);
        }

        public static void deleteServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicle.deleteServiceEvent(serviceEvent);
        }

        public static ServiceEvent getServiceEvent(int? id)
        {
            return BasicVehicle.getServiceEvent(id);
        }

        public static void editServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicle.editServiceEvent(serviceEvent);
        }
    }

    public class VehicleServiceEvent
    {
        public VehicleServiceEvent()
        {
        }

        public Vehicle vehicle { get; set; }
        public List<ServiceEvent> serviceEvents { get; set; }
    }

}