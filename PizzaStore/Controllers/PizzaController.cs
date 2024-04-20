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
        #region Pizza Page
        [HttpGet]
        public IActionResult Index(int? page)
        {
            try
            {
                const int pageSize = 15;
                int pageNumber = page ?? 1;

                List<Pizza> products = _context.Pizzas.OrderBy(p => p.PizzaId)
                                                         .Skip((pageNumber - 1) * pageSize)
                                                         .Take(pageSize)
                                                         .ToList();

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = Math.Ceiling((double)_context.Pizzas.Count() / pageSize);
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
        
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
                            // Get the records by using the first line as the column name, and the rest is data.
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                // Populate the value from the CSV to model
                                var pizzaProduct = new Pizza
                                {
                                    PizzaId = record.pizza_id,
                                    PizzaType = record.pizza_type_id,
                                    Size = record.size,
                                    Price = Convert.ToDouble(record.price),
                                };

                                // Populate add stored data from the model to context
                                _context.Pizzas.Add(pizzaProduct);
                            }
                            // Finally, saves all changes made in this context to the underlying database
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Pizza Types Page
        public IActionResult PizzaTypes(int? page)
        {
            try
            {
                const int pageSize = 15;
                int pageNumber = page ?? 1;

                List<PizzaType> products = _context.PizzaTypes.OrderBy(p => p.Category)
                                                         .Skip((pageNumber - 1) * pageSize)
                                                         .Take(pageSize)
                                                         .ToList();

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = Math.Ceiling((double)_context.PizzaTypes.Count() / pageSize);
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
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
                            // Get the records by using the first line as the column name, and the rest is data.
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                // Populate the value from the CSV to model
                                var pizzaType = new PizzaType
                                {
                                    PizzaType1 = record.pizza_type_id,
                                    Name = record.name,
                                    Category = record.category,
                                    Ingredients = record.ingredients,
                                };
                                // Populate add stored data from the model to context
                                _context.PizzaTypes.Add(pizzaType);
                            }
                            // Finally, saves all changes made in this context to the underlying database
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("PizzaTypes");
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region Pizza Orders Page
        public IActionResult Orders(int? page)
        {
            try
            {
                const int pageSize = 1000;
                int pageNumber = page ?? 1;

                List<Order> products = _context.Orders.OrderBy(p => p.OrderId)
                                                         .Skip((pageNumber - 1) * pageSize)
                                                         .Take(pageSize)
                                                         .ToList();

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = Math.Ceiling((double)_context.Orders.Count() / pageSize);
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
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
                            // Get the records by using the first line as the column name, and the rest is data.
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                // Populate the value from the CSV to model
                                var order = new Order
                                {
                                    OrderId = Convert.ToInt32(record.order_id),
                                    Date = Convert.ToDateTime(record.date),
                                    Time = record.time,
                                };
                                // Populate add stored data from the model to context
                                _context.Orders.Add(order);
                            }
                            // Finally, saves all changes made in this context to the underlying database
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("Orders");
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
        
        #region Pizza Order Details Page
        public IActionResult OrderDetails(int? page)
        {
            const int pageSize = 2100;
            int pageNumber = page ?? 1;

            List<OrderDetail> products = _context.OrderDetails.OrderBy(p => p.OrderDetailsId)
                                                     .Skip((pageNumber - 1) * pageSize)
                                                     .Take(pageSize)
                                                     .ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = Math.Ceiling((double)_context.OrderDetails.Count() / pageSize);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> ImportOrderDetails()
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
                            // Get the records by using the first line as the column name, and the rest is data.
                            var records = csvReader.GetRecords<dynamic>().ToList();

                            foreach (var record in records)
                            {
                                // Populate the value from the CSV to model
                                var orderDetail = new OrderDetail
                                {
                                    OrderDetailsId = Convert.ToInt32(record.order_details_id),
                                    OrderId = Convert.ToInt32(record.order_id),
                                    PizzaId = record.pizza_id,
                                    Quntity = Convert.ToInt32(record.quantity),
                                };
                                // Populate add stored data from the model to context
                                _context.OrderDetails.Add(orderDetail);
                            }
                            // Finally, saves all changes made in this context to the underlying database
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return RedirectToAction("OrderDetails");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
