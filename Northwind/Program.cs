using Northwind.Models;
using System.Net.Sockets;

namespace Northwind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NorthwndContext context = new NorthwndContext())
            {
                while (true)
                {
                Main:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("============================================================================================");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("                          Choose the Operation Northwind database                           ");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("============================================================================================");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("1.\tPlace new order - Input from Console\r\n2.\tCreate new category with multiple products - get all details from console \r\n3.\tAdd a new employee with manager (both should be new) \r\n4.\tAdd a new employee to existing manager \r\n5.\tUpdate existing emplyee to new manager \r\n6.\tFind customers who placed orders in 1997 but not in 1998.\r\n7.\tList customers and their most recent order date. \r\n8.\tShow all customers whose total order value exceeds 50000. \r\n9.\tList each category with the average unit price of products. r\n10.\tShow products that have never been ordered.\r\n11.\tFind the top 3 most ordered products (by total quantity sold).\r\n12.\tList products along with their supplier name and category name.\r\n13.\tDisplay all products where UnitPrice > Category Average Price.\r\n14.\tList employees with the total sales amount they handled. \r\n15.\tShow the employee who handled the most orders in 1997.\r\n16.\tFind employees who share the same territory.\r\n17.\tDisplay each employee with the number of distinct customers they served.\r\n18.\tList employees along with the first order they ever handled.\r\n19.\tFor each shipper, calculate the average delivery time (ShippedDate – OrderDate).\r\n20.\tList orders that took more than 30 days to deliver.\r\n21.\tFind the top shipper based on the number of orders shipped. \r\n22.\tShow the top employee per year based on total sales.\r\n23.\tFind all products that were ordered by every customer.\r\n24.\tFind suppliers who supply more than 5 products.\r\n25.\tList the customer(s) with the single highest order value.\r\n26.\tList customers who have ordered all products in a given category (e.g., Beverages).\r\n27.\tShow the most profitable product (highest total sales revenue).\r\n");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("============================================================================================");
                    Console.ResetColor();

                    int Choice = 0;

                choice:
                    try
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter the Choice : ");
                        Console.ResetColor();
                        int choice = int.Parse(Console.ReadLine());
                        if (ValidationMethods.isChoice(choice))
                        {
                            Choice = choice;
                        }
                        else
                        {
                            goto choice;
                        }
                    }
                    catch (OverflowException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalide Choice! Please Enter the Correct Choice!");
                        Console.ResetColor();
                        goto choice;
                    }
                    catch (FormatException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Choice! Please Enter the Choice in Digit");
                        Console.ResetColor();
                        goto choice;
                    }

                    switch (Choice)
                    {
                        
                        case 1:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("1.\tPlace new order - Input from Console CustomerID, EmployeeID, Multiple products (ProductID and Quantity for each)");
                            Console.ResetColor();
                            Console.WriteLine();
                        customerIDList:
                            string _CustomerID = ""; //Customer ID
                            var transaction = context.Database.BeginTransaction();
                            try
                            {
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("Enter the CustomerID : ");
                                    Console.ResetColor();
                                    string customerID = Console.ReadLine();

                                    var customerIDList = context.Customers.Select(x => x.CustomerId).ToList();
                                    if (!customerIDList.Contains(customerID))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Customer not found!..Please Enter valid CustomerID!");
                                        Console.ResetColor();
                                        goto customerIDList;
                                    }

                                    if (ValidationMethods.isValidID(customerID.Trim()))
                                    {
                                        _CustomerID = customerID.Trim();
                                    }
                                    else
                                    {
                                        goto customerIDList;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Something Went Wrong!! Please try Again!");
                                    Console.ResetColor();
                                    goto customerIDList;
                                }


                                var employeeIDList = context.Employees.Select(l => l.EmployeeId).ToList();
                                Random rand = new Random();
                                int RandId = rand.Next(employeeIDList.Count);
                                int id = employeeIDList[RandId];
                                int _EmployeeID = id;  //Employeee ID


                                DateTime _OrderDate = DateTime.Now;
                                DateTime _RequiredDate = _OrderDate.AddDays(4);

                                var customerDetails = context.Customers.Where(a => a.CustomerId == _CustomerID).ToList();

                                String _ShipName = customerDetails[0].CompanyName;
                                String _ShipAddress = customerDetails[0].Address;
                                string _ShipCity = customerDetails[0].City;
                                string _ShipRegion = customerDetails[0].Region;
                                string _ShipPostalCode = customerDetails[0].PostalCode;
                                string _ShipCountry = customerDetails[0].Country;


                                Order order = new Order()
                                {
                                    CustomerId = _CustomerID,
                                    EmployeeId = _EmployeeID,
                                    OrderDate = _OrderDate,
                                    RequiredDate = _RequiredDate,
                                    ShippedDate = null,
                                    ShipVia = null,
                                    Freight = 0,
                                    ShipName = _ShipName,
                                    ShipAddress = _ShipAddress,
                                    ShipCity = _ShipCity,
                                    ShipRegion = _ShipRegion,
                                    ShipPostalCode = _ShipPostalCode,
                                    ShipCountry = _ShipCountry,
                                    OrderDetails = new List<OrderDetail>(),
                                };

                                //var OrdersDetails = 

                            OrderDetailsMain:
                            productID:
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("----------------------------------------------------------------------------");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("                             Choose the Product                             ");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("----------------------------------------------------------------------------");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"| {"ID",-6} | {"Product Name",-40} | {"Unit Price",-12} |");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("----------------------------------------------------------------------------");
                                Console.ResetColor();

                                var products = context.Products.ToList();

                                if (context.Products.Any(a => a.UnitsInStock != 0))
                                {
                                    foreach (var product in products)
                                    {
                                        Console.WriteLine($"| {product.ProductId,-6} | {product.ProductName,-40} | {product.UnitPrice,-12} |");
                                    }
                                }

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("----------------------------------------------------------------------------");
                                Console.ResetColor();


                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Choose the Product ID : ");
                                Console.ResetColor();
                                int _ProductID = int.Parse(Console.ReadLine());

                                if (!context.Products.Any(a => a.ProductId == _ProductID))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Product ID not Found! Please enter the Correct Product ID");
                                    Console.ResetColor();
                                    goto productID;
                                }

                                decimal? _UnitPrice = context.Products.Where(a=>a.ProductId==_ProductID).Select(a => a.UnitPrice).First();

                            quantity:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the Product Quantity: ");
                                Console.ResetColor();
                                short _Quantity = short.Parse(Console.ReadLine());

                                if (context.Products.Where(a=>a.ProductId==_ProductID).Any(a => a.UnitsInStock < _Quantity))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"We have Quantity for {_ProductID} is {context.Products.Where(a => a.ProductId == _ProductID).Select(a=>a.UnitsInStock).FirstOrDefault()}");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("If you want to Re-Enter the Product Quantity (Y/N) : ");
                                    Console.ResetColor();
                                    char ch = char.Parse(Console.ReadLine());

                                    if (ch == 'y' || ch == 'Y')
                                    {
                                        goto quantity;
                                    }
                                    else
                                    {
                                        goto productID;
                                    }

                                }

                                float _Descount = 0;

                                OrderDetail orderDetail = new OrderDetail()
                                {
                                    //OrderId = orderID,
                                    ProductId = _ProductID,
                                    UnitPrice = _UnitPrice ?? 0m,
                                    Quantity = _Quantity,
                                    Discount = _Descount
                                };

                                order.OrderDetails.Add(orderDetail);

                            confirmDecision:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("If you want to Add Product? (Y/N): ");
                                Console.ResetColor();
                                char ch1 = char.Parse(Console.ReadLine());
                                if (ch1 == 'y' || ch1 == 'Y')
                                {
                                    goto OrderDetailsMain;
                                }
                                else if (ch1 == 'n' || ch1 == 'N')
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Press Any Key to Confrim the Order !'");
                                    Console.ResetColor();
                                    Console.ReadKey(false);

                                    context.Orders.Add(order);
                                    context.SaveChanges();
                                    transaction.Commit();

                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("Product as been Succussfully Ordered! :)");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    //break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Pease Enter valid Decision! Please");
                                    Console.ResetColor();
                                    goto confirmDecision;
                                }
                            }

                            catch(Exception ex)
                            {
                                transaction.Rollback();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Something Went Wrong! try Again : "+ex.Message);
                                Console.ResetColor();
                                goto Main;
                            }
                            break;

                        case 2:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("2.\tCreate new category with multiple products - get all details from console - Output: CategoryId | CategoryName | ProductId | ProdutName | UnitPrice");
                            Console.ResetColor();
                            Console.WriteLine();


                            break;
                    }
                    
                }
            }
        }
    }
}
