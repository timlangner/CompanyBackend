using System;
using CompanyApp.Model;

namespace CompanyApp
{
    class Program
    {
        
        const string CONSTRING_TAPPQA = "Data Source=tappqa;Initial Catalog=Training-TN-Company;Integrated Security=True";

        static Controller.LocationController companyController = new Controller.LocationController(CONSTRING_TAPPQA);
        static Controller.EmployeeController employeeController = new Controller.EmployeeController(CONSTRING_TAPPQA);

        static void Main(string[] args)
        {
            string input, action;
            int id = 0;

            Console.WriteLine("Willkommen! Mit welcher Tabelle wollen Sie arbeiten?\n");
            Console.WriteLine("1) Company \n2) Employee \n3) Address");
            action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    #region Company
                    Company companyModel = new Company();
                    Company companyData;

                    Console.WriteLine("\nWas möchten Sie tun?");
                    Console.WriteLine("1) Read \n2) Create \n3) Update \n4) Delete");
                    action = Console.ReadLine();

                    switch (action)
                    {
                        case "1":
                            Console.WriteLine("\nMöchten Sie alle Zeilen ausgeben(1), oder nur eine? Wenn Sie nur eine ausgeben möchten, geben Sie bitte die ID ein.");
                            action = Console.ReadLine();
                            Console.WriteLine();

                            switch (action.ToLower())
                            {
                                case "1":
                                    var companies = companyController.Read();
                                    foreach (var company in companies)
                                    {
                                        Console.WriteLine($"id={company.Id}, name={company.Name}, foudedDate={company.FoundedDate}");
                                    }
                                    break;

                                default:
                                    companyData = companyController.Read(Convert.ToInt32(action));
                                    Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");
                                    break;

                            }
                            break;

                        case "2":
                            Console.WriteLine("\nWie ist der Name ihrer Firma?");
                            companyModel.Name = Console.ReadLine();

                            Console.WriteLine("\nWann wurde Ihre Firma gegründet? (Kann auch leer bleiben)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            companyData = companyController.Create(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

                            break;

                        case "3":
                            Console.WriteLine("\nBitte geben Sie die ID des Datensatzes an, den Sie ändern möchten.");
                            companyModel.Id = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\nWie lautet der neue Firmenname? (Falls er nicht geändert werden soll, einfach leer lassen.)");
                            companyModel.Name = Console.ReadLine();
                            companyModel.Name = companyModel.Name == "" ? null : companyModel.Name;

                            Console.WriteLine("\nWie lautet das neue Gründungsdatum? (Falls er nicht geändert werden soll, einfach leer lassen.)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);


                            companyData = companyController.Update(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

                            break;

                        case "4":
                            Console.WriteLine("\nBitte geben Sie die ID des Datensatzes an, der gelöscht werden soll.");
                            id = Convert.ToInt32(Console.ReadLine());
                            companyController.Delete(id);
                            break;
                    }
                    #endregion
                    break;

                case "employee":
                    // Employee
                    #region Employee
                    //string firstName, lastName;
                    //int? departmentId = null, addressId = null;
                    //DateTime? birthday = null;

                    //Console.WriteLine("What would you do? (Read, Create, Update, Delete)");
                    //action = Console.ReadLine();

                    //switch (action.ToLower())
                    //{
                    //    case "read":

                    //        Console.WriteLine("All column or search a id? ('All' or the id you search for)");
                    //        action = Console.ReadLine();

                    //        switch (action.ToLower())
                    //        {
                    //            case "all":
                    //                var employees = employeeController.Read();
                    //                foreach (var employee in employees)
                    //                {
                    //                    Console.WriteLine(  $"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                    //                                        $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, Zip={employee.Zip}" +
                    //                                        $", City={employee.City}, Street={employee.Street}, Country={employee.Country}");
                    //                }
                    //                break;

                    //            default:
                    //                Model.Employee employeeData = employeeController.Read(Convert.ToInt32(action));
                    //                Console.WriteLine(  $"\nId={employeeData.Id}, FirstName={employeeData.FirstName}, LastName={employeeData.LastName}" +
                    //                                    $", Birthday={employeeData.Birthday}, DepartmentId={employeeData.DepartmentId}, Zip={employeeData.Zip}" +
                    //                                    $", City={employeeData.City}, Street={employeeData.Street}, Country={employeeData.Country}");
                    //                break;

                    //        }
                    //        break;

                    //    case "create":
                    //        Console.WriteLine("What is the first name?");
                    //        firstName = Console.ReadLine();

                    //        Console.WriteLine("What is the last name?");
                    //        lastName = Console.ReadLine();

                    //        Console.WriteLine("What is the birthday (yyyy-mm-dd)? (can be empty)");
                    //        input = Console.ReadLine();
                    //        if (input != "")
                    //            birthday = Convert.ToDateTime(input);

                    //        Console.WriteLine("What is the departmentId?");
                    //        departmentId = Convert.ToInt32(Console.ReadLine());

                    //        Console.WriteLine("What is the adressId? (can be empty)");
                    //        input = Console.ReadLine();
                    //        if (input != "")
                    //            addressId = Convert.ToInt32(Console.ReadLine());

                    //        employeeController.CreateOrUpdate(firstName, lastName, departmentId, birthday, addressId);

                    //        break;

                    //    case "update":
                    //        Console.WriteLine("Enter the ID of the company you want to change");
                    //        id = Convert.ToInt32(Console.ReadLine());

                    //        Console.WriteLine("What is the first name? (no change then press enter)");
                    //        firstName = Console.ReadLine();

                    //        Console.WriteLine("What is the last name? (no change then press enter)");
                    //        lastName = Console.ReadLine();

                    //        Console.WriteLine("What is the birthday (yyyy-mm-dd)? (no change then press enter)");
                    //        input = Console.ReadLine();
                    //        if (input != "")
                    //            birthday = Convert.ToDateTime(input);

                    //        Console.WriteLine("What is the departmentId? (no change then press enter)");
                    //        input = Console.ReadLine();
                    //        if (input != "")
                    //            departmentId = Convert.ToInt32(Console.ReadLine());

                    //        Console.WriteLine("What is the adressId? (can be empty) (no change then press enter)");
                    //        addressId = Convert.ToInt32(Console.ReadLine());

                    //        employeeController.CreateOrUpdate(firstName, lastName, departmentId, birthday, addressId, id);
                    //        break;

                    //    case "delete":
                    //        Console.WriteLine("Geben Sie bitte die id  von dem Datensatz an, den sie löschen möchte");
                    //        id = Convert.ToInt32(Console.ReadLine());
                    //        employeeController.Delete(id);
                    //        break;
                    //}
                    #endregion
                    break;

                case "address":
                    break;
            }


            Console.WriteLine("\nPress Enter to quit");
            Console.ReadKey();

        }
    }
}
