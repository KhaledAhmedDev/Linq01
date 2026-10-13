using LINQ.Models;
using System.Collections;
using static LINQ.DataSources.Source;
namespace LINQ
{
	internal class Program
	{
		static void Main(string[] args)
		{
			#region var vs dynamic
			//var name = "Khaled";  //Compiler Detecets the type : string Compilation Time
			//name="Ahmed"; //Valid
			//name = 99; //Compilation error

			//Console.WriteLine(name.GetType()); //System.String

			//Console.WriteLine(name.Length);

			//var y = null; //Compilation error : Compiler cannot detect type of Y from Null

			//var y; // Compiler error : without intialization, the compiler cannot infer the type of y

			//var x = "Khaled";
			//x = null; ; //Valid : because string is a reference type and can be null



			// Key word : Dynamic
			//dynamic value;  // can be declared without initialization
			//value = 42;
			//Console.WriteLine(value.GetType());  //System.Int32

			//value = true;
			//Console.WriteLine(value.GetType());  //System.Boolean

			//value = new Customer() { CustomerName = "Ali" };
			//Console.WriteLine(value.GetType());

			//Console.WriteLine(value.CustomerName);

			//value = "Hamada";
			//Console.WriteLine(value.CustomerName); //Microsoft.CSharp.RuntimeBinder.RuntimeBinderException: ''



			#endregion

			#region Anonymous Types
			//var employee1 = new { Id = 1, Name = "Ahmed", Salary = 5000 };   // Anynmous  0`3 [Id:int -name:string -salary:int]
			//																 //Console.WriteLine(employee1);

			////Console.WriteLine(employee1.GetType());  //<>f__AnonymousType0`3[System.Int32,System.String,System.Int32]

			//var employee2 = new { Id = 2, Name = "Khaled", Salary = 7000 };

			////Console.WriteLine(employee2.GetType());  //<>f__AnonymousType0`3[System.Int32,System.String,System.Int32]
			////Same Property Names and Types and order => Same Anynmous Type


			//var employee3 = new { id = 3, name = "Omar", salary = 3000 };
			////Console.WriteLine(employee3.GetType());   // <>f__AnonymousType1`3[System.Int32,System.String,System.Int32]  Different Property Names => Different Anynmous Type


			//var employee4 = new { Id = 4, Salary = 10000, Name = "Ali" }; //<>f__AnonymousType2`3[System.Int32,System.Int32,System.String]
			//Console.WriteLine(employee4.GetType());  // Different Property Order => Different Anynmous Type


			//===================Equality===========================

			// Equals() => Comapare Object based on Data not address
			// == > Comapare Object based on Address not Data

			//var employee01Copy = new { Id = 1, Name = "Ahmed", Salary = 5000 };
			////Console.WriteLine(employee01Copy.GetType());

			////Console.WriteLine(employee1.Equals(employee01Copy)); // Same Values => True

			////Console.WriteLine(employee1==employee01Copy); // Not same reference => False


			////===================Immutabilty=========================

			//// All properties of an anonymous type are read-only (immutable) and cannot be changed after initialization.

			////employee1.Name = "Ali"; // Compilation Error : Cannot modify the value of an anonymous type property


			//// To change Value: or to make new objects with same values of another Object but i need to change one property

			////1 Projection Intializzers

			//var employee5 = new { employee1.Id, employee1.Name, Salary = 6000 };

			////Console.WriteLine(employee5);

			////Console.WriteLine(employee1.GetType()==employee5.GetType());


			//// 2 With Expression [C#10]

			//var employee6 = employee1 with { Salary = 10000 };

			////Console.WriteLine(employee6);

			////Console.WriteLine(employee6.GetType()==employee1.GetType());

			////Console.WriteLine(employee1);


			#endregion

			#region Query Syntax vs Method Syntax [Fluent Syntax]

			//List<Student> students = new List<Student>
			//{
			//	new Student(){Name="Sara",Grade=90,Major="CS"},
			//	new Student(){Name="Ali",Grade=85,Major="Math"},
			//	new Student(){Name="Omar",Grade=62,Major="CS"},
			//	new Student(){Name="Lina",Grade=88,Major="Math"},
			//	new Student(){Name="Huda",Grade=95,Major="CS"},
			//};

			//  Select S.name,S.grade
			// from Students S
			// where S.Grade > 80 && S.Major == "CS"

			//Query Syntax[Query Expression]
			// SQL like syntax , Must start with From Clause and end with Select or Group By Clause

			//var queryResult = from s in students  // S:range variable => each and every student in the students collection
			//				  where s.Grade > 80 && s.Major == "CS" // filtering condition
			//				  select new {s.Name, s.Grade }; //Select



			//foreach(var student in queryResult)
			//{
			//	Console.WriteLine($"Name: {student.Name}, Grade: {student.Grade}");
			//}
			////Method Syntax [Fluent Syntax]


			//Console.WriteLine("===========================");


			//var methodResult=students
			//	.Where(S=>S.Grade>=80&&S.Major=="CS")
			//	.Select(S => new { S.Name, S.Grade });

			//foreach (var student in methodResult)
			//{
			//	Console.WriteLine($"Name: {student.Name}, Grade: {student.Grade}");
			//}
			#endregion

			#region Deferred vs Immediate Execution

			//var numbers = new List<int> { 1, 2, 3, 4, 5 };

			////var result01 = numbers.Where(n => n % 2 == 0);


			//numbers.Add(6);


			////foreach (var num in result01) // Execution Happens Here ,Because where is a deferred operator [Will wait until the result is printing]
			////{
			////	Console.WriteLine(num);  // Output: 2, 4, 6
			////}



			//var numbers = new List<int> { 1, 2, 3, 4, 5 };

			//var result02 = numbers.Where(N => N % 2 == 0).ToList(); // Immediate Execution: Force Query to run rightnow regarding any change after


			//numbers.Add(6);

			//foreach (var num in result02) 
			//{
			//	Console.WriteLine(num);  
			//}







			// ===== Which operators are which? =====
			// Deferred:  operators that return IEnumerable<T>
			//            Where, Select, OrderBy, Take, Skip, Distinct ...
			// Immediate: operators that return a single value or a ready collection
			//            ToList, ToArray, ToDictionary, Count, Sum, Max, First, Single, Any ...
			#endregion

			#region Filtering - Restriction

			#region Where

			//// Out of stock products

			//var outOfStock=	ProductList.Where(P => P.UnitsInStock == 0); // FluentSyntax


			//	outOfStock=from P in ProductList
			//			   where P.UnitsInStock == 0
			//			   select P; // QuerySyntax


			//	foreach (var product in outOfStock)
			//	{
			//		Console.WriteLine(product);
			//	}





			//// In stock AND price > $20 — combine conditions with && (or ||)

			//var expensiveAndInStock=ProductList.Where(P=> P.UnitsInStock>0 &&P.UnitPrice>20); // FluentSyntax

			//expensiveAndInStock = from P in ProductList
			//					  where P.UnitsInStock > 0 && P.UnitPrice > 20
			//					  select P; // QuerySyntax


			//foreach (var product in expensiveAndInStock)
			//{
			//	Console.WriteLine(product);
			//}



			#endregion

			#region Indexed Where
			// Overload that also gives us the INDEX of each element (zero-based position in the source).
			// Available in method syntax ONLY — no query syntax equivalent.

			// Out of stock products among the first 20 products (index 0 → 19)

			//var outOfStockProductsAmong20 = ProductList.Where((P, I) => P.UnitsInStock == 0 && I < 20);




			//foreach (var product in outOfStockProductsAmong20)
			//{
			//	Console.WriteLine(product);
			//}

			#endregion

			#region OfType<>
			// Filters elements by their TYPE and returns IEnumerable<T>.
			// where Where() is not available because the elements are stored as object.

			//ArrayList mixed = new ArrayList() { 1, "hello", 3.14, "world", true, "lina" };


			//var strings = mixed.OfType<string>();  //keep strings Only ,Skip everytging else

			//foreach (var str in strings)
			//{
			//	Console.WriteLine(str);
			//}


			//var cast = mixed.Cast<string>();

			//foreach (var str in cast)
			//{
			//	Console.WriteLine(str);
			//}

			//System.InvalidCastException: 'Unable to cast object of type 'System.Int32' to typ


			//OffType vs Cast
			//OFFType : filter by runtime type and skip everything else
			// Cast : try to cast everything to the specified type and throw exception if it fails




			#endregion

			#endregion

			#region Projection

			#region Select

			//var productNames = ProductList.Select(P => P.ProductName);  // Projection : Select only the ProductName property from each product


			//productNames=from P in ProductList
			//			 select P.ProductName; // Query Syntax


			//foreach (var name in productNames)
			//{
			//	Console.WriteLine(name);
			//}


			//var productsWithBasicInfo = ProductList.Select(P => new { P.ProductName, P.Category, P.UnitPrice }); //Fluent Syntax

			//productsWithBasicInfo=from P in ProductList
			//					  select new { P.ProductName, P.Category, P.UnitPrice }; // Query Syntax

			//foreach (var product in productsWithBasicInfo)
			//{
			//	Console.WriteLine(product);
			//}

			// Indexed overload — also gives the zero-based index of each element.
			// Method syntax ONLY — no query syntax equivalent.


			//// InPlace = true when the number's value equals its position in the array

			//int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7 };




			//var numbersInPlace = numbers.Select((n, index) => new { Num = n, InPlace = n == index });  // anynomous Object [num,INpLACE]

			//numbers[8] = 8;
			//foreach (var item in numbersInPlace)
			//{
			//	Console.WriteLine($"Num={item.Num}, InPlace={item.InPlace}");
			//}

			#endregion

			#region SelectMany
			// With Select: we get a collection of collections (one orders collection per customer)

			//var ordersPerCustomer = CustomerList.Select(C => C.Orders);

			//foreach(var CustomerOrders in ordersPerCustomer)

			//{
			//	foreach(var order in CustomerOrders)
			//	{
			//		Console.WriteLine(order);
			//	}
			//}			

			// With SelectMany: all orders flattened into ONE sequence
			// (customers with no orders simply produce nothing)


			//var allOrders = CustomerList.SelectMany(C => C.Orders);


			//foreach(var order in allOrders)
			//{
			//	Console.WriteLine(order);
			//}


			// SelectMany with result selector:
			// the second lambda receives BOTH the parent (c) and the child (o),
			// so we can keep customer data next to each order


			//var CustomerOrderInfo = CustomerList.SelectMany((C)=> C.Orders, (c, o) => new { c.CustomerID, o.OrderID, o.Total });

			//foreach (var item in CustomerOrderInfo)
			//{
			//	Console.WriteLine($"CustomerID={item.CustomerID}, OrderID={item.OrderID}, Total={item.Total:C2}");
			//}



			// Indexed SelectMany — ci is the customer's index


			//var customerOrdersWithIndex = CustomerList.SelectMany((c, ci) => c.Orders
			//.Select(o => $"Customer # {ci + 1} [{c.CustomerID}==>order Id:{o.OrderID}]"));

			//foreach(var order in customerOrdersWithIndex)
			//{
			//	Console.WriteLine(order);
			//}




			// Indexed on both levels — oi is the order's index and restarts from 0 for each customer


			//var customerOrdersWithIndex = CustomerList.SelectMany((c, ci) => c.Orders
			//															   .Select((o,oi) => $"Customer # {ci + 1} [{c.CustomerID}==>Order #{oi+1} order Id:{o.OrderID}]"));


			//foreach (var order in customerOrdersWithIndex)
			//{
			//	Console.WriteLine(order);
			//}


			// 1st overload : SelectMany() : 1 Func (1 Parameter )=> nested collection as Flat sequence
			// 2nd overload: SelectMany(,index) : 1 Func (2 Parameters) => nested Collection as Flat sequence
			// 3rd overload : SelectMany() : 2 Func [1st Func take one parameter and return flat sequence ],[2nd func(2 parameters) ] : keep customer with eaach order
			// 4rd overload: SelectMany() : 2 Func [1st Func take two parameter and return flat sequence ],[2nd func(2 parameters) ] : keep customer with eaach order along with Customer Inde

			// Query syntax: multiple 'from' clauses = SelectMany
			// Orders with Total < $500

			//var customerOrders = from c in CustomerList
			//					 from o in c.Orders
			//					 where o.Total < 500
			//					 select new { c.CustomerID, o.OrderID, o.Total };


			//foreach(var order in customerOrders)
			//	Console.WriteLine(order);


			// Same query in method syntax



			//var customerOrdersLessThan500 = CustomerList.
			//	SelectMany(c => c.Orders, (c, o) => new { c.CustomerID, o.OrderID, o.Total })
			//	.Where(o=>o.Total<500);




			//foreach (var order in customerOrdersLessThan500)
			//	Console.WriteLine(order);


			#endregion

			#endregion


		}
	}
}
