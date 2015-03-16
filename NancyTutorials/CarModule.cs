using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;

namespace NancyTutorial
{
    public class CarModule : NancyModule
    {
        public CarModule()
        {
            //Get is a method of NancyModule which takes key-value pair: key is the string and value is the lambda: Func<dynamic, dynamic> (function that receives dynamic param and returns dynamic type)
            //public RouteBuilder Get{
            //    get
            //    {
            //        return new RouteBuilder("GET", this);
            //   }
            //}
            Get["/status"] = _ => "Hello"; //"_" is a variable that we don't care about
            Get["/car/{id}"] = parameters =>
                                {
                                    int id = parameters.id;
                                    return Negotiate
                                        .WithStatusCode(HttpStatusCode.OK)
                                        .WithModel(id);
                                };
            Get["/{make}/{model}/"] = parameters =>
            {
                var carQuery = this.Bind<BrowseCarQuery>();
                var listOfCars = new List<Car>
                                        {
                                            new Car
                                                {
                                                    Id = 1,
                                                    Make = carQuery.Make,
                                                    Model = carQuery.Model
                                                },
                                            new Car
                                                {
                                                    Id = 2,
                                                    Make = carQuery.Make,
                                                    Model = carQuery.Model
                                                },
                                            new Car
                                                {
                                                    Id = 3,
                                                    Make = carQuery.Make,
                                                    Model = carQuery.Model
                                                }
                                        };
                                                                   
 
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(listOfCars.ToArray());
            };
                    
        }
    }

    public class BrowseCarQuery 
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}