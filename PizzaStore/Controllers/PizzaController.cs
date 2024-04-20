using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PizzaStore.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaHubContext _context;

        public PizzaController(PizzaHubContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Pizzas.ToList();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> ImportPizza()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file != null && file.Length > 0)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                var pizzaProduct = new Pizza
                                {
                                    PizzaId = record.pizza_id,
                                    PizzaType = record.pizza_type_id,
                                    Size = record.size,
                                    Price = Convert.ToDouble(record.price),
                                };

                                _context.Pizzas.Add(pizzaProduct);
                            }

                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult PizzaTypes()
        {
            var products = _context.PizzaTypes.ToList();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> ImportPizzaType()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file != null && file.Length > 0)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                var pizzaType = new PizzaType
                                {
                                    PizzaType1 = record.pizza_type_id,
                                    Name = record.name,
                                    Category = record.category,
                                    Ingredients = record.ingredients,
                                };

                                _context.PizzaTypes.Add(pizzaType);
                            }

                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("PizzaTypes");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult Orders()
        {
            var products = _context.Orders.ToList();
            return View(products);

        }

        [HttpPost]
        public async Task<IActionResult> ImportOrders()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file != null && file.Length > 0)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                var order = new Order
                                {
                                    OrderId = Convert.ToInt32(record.order_id),
                                    Date = Convert.ToDateTime(record.date),
                                    Time = record.time,
                                };

                                _context.Orders.Add(order);
                            }

                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("Orders");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
