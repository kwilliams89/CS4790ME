[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CS4790ME.MVCGridConfig), "RegisterGrids")]

namespace CS4790ME
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using MVCGrid.Models;
    using MVCGrid.Web;
    using Models;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {

            ColumnDefaults colDefaults = new ColumnDefaults()
            {
                EnableSorting = true
            };

  //          MVCGridDefinitionTable.Add("Vehicles", new MVCGridBuilder<Vehicle>(colDefaults)
  //              .WithAuthorizationType(AuthorizationType.AllowAnonymous)
  //              .AddColumns(cols =>
  //              {
  //                  cols.Add("Id").WithSorting(false)
  //                      .WithValueExpression(p => p.Id.ToString());
  //                  cols.Add("Vin").WithHeaderText("Vin")
  //                      .WithValueExpression(p => p.Vin);
  //                  cols.Add("Make").WithHeaderText("Make")
  //                      .WithValueExpression(p => p.Make);
  //                  cols.Add("Model").WithHeaderText("Model")
  //                      .WithValueExpression(p => p.Model);
  //                  cols.Add("Year").WithHeaderText("Year")
  //                      .WithValueExpression(p => p.Year);
  //                  cols.Add("CurrentMileage").WithHeaderText("Current Mileage")
  //                      .WithValueExpression(p => p.CurrentMileage);
  //              })
  ////              .WithSorting(true, "Make")
  //              .WithPaging(true, 2)
  //              .WithRetrieveDataMethod((context) =>
  //              {
  //                  var options = context.QueryOptions;
  //                  var result = new QueryResult<Vehicle>();


  //                      var query = Repository.getVehicles().AsQueryable();
  //                      result.TotalRecords = query.Count();
  //                      //if (!String.IsNullOrWhiteSpace(options.SortColumnName))
  //                      //{
  //                      //    switch (options.SortColumnName.ToLower())
  //                      //    {
  //                      //        case "vin":
  //                      //            query = query.OrderBy(p => p.Vin, options.SortDirection);
  //                      //            break;
  //                      //        case "make":
  //                      //            query = query.OrderBy(p => p.Make, options.SortDirection);
  //                      //            break;
  //                      //    }
  //                      //}
  //                      if (options.GetLimitOffset().HasValue)
  //                      {
  //                          query = query.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);
  //                      }
  //                      result.Items = query.ToList();
                    
  //                  return result;
  //              })
  //          );

            MVCGridDefinitionTable.Add("Vehicles", new MVCGridBuilder<Vehicle>(colDefaults)
    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
    .WithSorting(sorting: true, defaultSortColumn: "Id", defaultSortDirection: SortDirection.Dsc)
    .WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
    .WithAdditionalQueryOptionNames("search")
    .AddColumns(cols =>
    {
        cols.Add("Id").WithValueExpression((p, c) => c.UrlHelper.Action("detail", "demo", new { id = p.Id }))
            .WithValueTemplate("<a href='{Value}'>{Model.Id}</a>", false)
            .WithPlainTextValueExpression(p => p.Id.ToString());
        cols.Add("Make").WithHeaderText("Make")
            .WithVisibility(true, true)
            .WithValueExpression(p => p.Make);
        cols.Add("Model").WithHeaderText("Model")
            .WithVisibility(true, true)
            .WithValueExpression(p => p.Model);
        cols.Add("Year").WithHeaderText("Year")
            .WithVisibility(true, true)
            .WithValueExpression(p => p.Year);
        cols.Add("Vin").WithHeaderText("Vin")
            .WithVisibility(true, true)
            .WithValueExpression(p => p.Vin);
        cols.Add("CurrentMileage").WithHeaderText("Current Mileage")
            .WithVisibility(true, true)
            .WithValueExpression(p => p.CurrentMileage);

    })
    //.WithAdditionalSetting(MVCGrid.Rendering.BootstrapRenderingEngine.SettingNameTableClass, "notreal") // Example of changing table css class
    .WithRetrieveDataMethod((context) =>
    {
        var options = context.QueryOptions;

        int totalRecords;
        var repo = DependencyResolver.Current.GetService<VehicleRepository>();

        string globalSearch = options.GetAdditionalQueryOptionString("search");

        string sortColumn = options.GetSortColumnData<string>();

        var items = repo.GetData(out totalRecords, globalSearch, options.GetLimitOffset(), options.GetLimitRowcount(),
            sortColumn, options.SortDirection == SortDirection.Dsc);

        return new QueryResult<Vehicle>()
        {
            Items = items,
            TotalRecords = totalRecords
        };
    })
);

        }
    }
}