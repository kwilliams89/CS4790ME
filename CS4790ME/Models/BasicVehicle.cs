using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS4790ME.Models
{

    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(17, ErrorMessage = "Vin must be 17 characters.")]
        public string Vin { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Make must be 20 characters or less.")]
        public string Make { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Model must be 20 characters or less.")]
        public string Model { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Year must be 4 characters.")]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Current Mileage")]
        [StringLength(6, ErrorMessage = "Current mileage must be 6 characters or less.")]
        public string CurrentMileage { get; set; }

    }

    [Table("ServiceEvent")]
    public class ServiceEvent
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Service Date")]
        public string ServiceDate { get; set; }

        [Required]
        [StringLength(17, ErrorMessage = "Vin must be 17 characters.")]
        public string Vin { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Required]
        [Display(Name = "Service Mileage")]
        [StringLength(6, ErrorMessage = "Service mileage must be 6 characters or less.")]
        public string ServiceMileage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Service Amount")]
        public decimal ServiceAmount { get; set; }


    }

    public class BasicVehicleDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceEvent> ServiceEvents { get; set; }
    }


    public class BasicVehicle
    {
        public static VehicleServiceEvent getVehicleAndServiceEvents(int? id)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            VehicleServiceEvent vehServiceEvents = new VehicleServiceEvent();
            vehServiceEvents.vehicle = getVehicle(id);

            var serviceEvents = db.ServiceEvents.Where(s => s.Vin == vehServiceEvents.vehicle.Vin);
            vehServiceEvents.serviceEvents = serviceEvents.ToList();

            return vehServiceEvents;
        }

        public static void deleteVehicle(Vehicle vehicle)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(vehicle).State = EntityState.Deleted;
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
        }

        public static List<Vehicle> getVehicles()
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.Vehicles.ToList();
        }

        public static Vehicle getVehicleDetails(int? id)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.Vehicles.Find(id);
        }

        public static void createVehicle(Vehicle vehicle)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(vehicle).State = EntityState.Added;
            db.Vehicles.Add(vehicle);
            db.SaveChanges();
        }

        public static Vehicle getVehicle(int? id)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.Vehicles.Find(id);
        }

        public static void editVehicle(Vehicle vehicle)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void deleteServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(serviceEvent).State = EntityState.Deleted;
            db.ServiceEvents.Remove(serviceEvent);
            db.SaveChanges();
        }

        public static List<ServiceEvent> getServiceEvents()
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.ServiceEvents.ToList();
        }

        public static ServiceEvent getServiceEventDetails(int? id)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.ServiceEvents.Find(id);
        }

        public static void createServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(serviceEvent).State = EntityState.Added;
            db.ServiceEvents.Add(serviceEvent);
            db.SaveChanges();
        }

        public static ServiceEvent getServiceEvent(int? id)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            return db.ServiceEvents.Find(id);
        }

        public static void editServiceEvent(ServiceEvent serviceEvent)
        {
            BasicVehicleDbContext db = new BasicVehicleDbContext();
            db.Entry(serviceEvent).State = EntityState.Modified;
            db.SaveChanges();
        }


    }
}