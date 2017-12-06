using CS4790ME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS4790ME.Models
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetData(out int totalRecords, string globalSearch, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
        IEnumerable<Vehicle> GetData(out int totalRecords, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
    }
    public class VehicleRepository : IVehicleRepository
    {
        public IEnumerable<Vehicle> GetData(out int totalRecords,  int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, null,  limitOffset, limitRowCount, orderBy, desc);
        }

        public IEnumerable<Vehicle> GetData(out int totalRecords, string globalSearch, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, globalSearch, null, null, null, limitOffset, limitRowCount, orderBy, desc);
        }

        public IEnumerable<Vehicle> GetData(out int totalRecords, string globalSearch, string filterFirstName, string filterLastName, bool? filterActive, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            using (var db = new BasicVehicleDbContext())
            {
                var query = db.Vehicles.AsQueryable();

                if (!String.IsNullOrWhiteSpace(globalSearch))
                {
                    query = query.Where(p => (p.Make + " " + p.Model + " " + p.Vin + " " + p.Year + " " + p.CurrentMileage).Contains(globalSearch));
                }

                totalRecords = query.Count();

                if (!String.IsNullOrWhiteSpace(orderBy))
                {
                    switch (orderBy.ToLower())
                    {
                        case "make":
                            if (!desc)
                                query = query.OrderBy(p => p.Make);
                            else
                                query = query.OrderByDescending(p => p.Make);
                            break;
                        case "model":
                            if (!desc)
                                query = query.OrderBy(p => p.Model);
                            else
                                query = query.OrderByDescending(p => p.Model);
                            break;
                        case "year":
                            if (!desc)
                                query = query.OrderBy(p => p.Year);
                            else
                                query = query.OrderByDescending(p => p.Year);
                            break;
                        case "vin":
                            if (!desc)
                                query = query.OrderBy(p => p.Vin);
                            else
                                query = query.OrderByDescending(p => p.Vin);
                            break;
                        case "currentmileage":
                            if (!desc)
                                query = query.OrderBy(p => p.CurrentMileage);
                            else
                                query = query.OrderByDescending(p => p.CurrentMileage);
                            break;
                        case "id":
                            if (!desc)
                                query = query.OrderBy(p => p.Id);
                            else
                                query = query.OrderByDescending(p => p.Id);
                            break;
                    }
                }


                if (limitOffset.HasValue)
                {
                    query = query.Skip(limitOffset.Value).Take(limitRowCount.Value);
                }

                return query.ToList();
            }
        }
    }
}