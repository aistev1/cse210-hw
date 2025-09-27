using System;

class Program
{
    static void Main(string[] args)
    {
        Address addr1 = new Address("123 Elm Street", "New York", "NY", "USA");
        Address addr2 = new Address("456 Maple Avenue", "Toronto", "ON", "Canada");

        Customer cust1 = new Customer("John Doe", addr1);
        Customer cust2 = new Customer("Alice Smith", addr2);

        Product prod1 = new Product("Laptop", "P1001", 800.0, 1);
        Product prod2 = new Product("Mouse", "P1002", 25.0, 2);
        Product prod3 = new Product("Keyboard", "P1003", 50.0, 1);
        Product prod4 = new Product("Monitor", "P1004", 200.0, 2);

        Order order1 = new Order(cust1);
        order1.AddProduct(prod1);
        order1.AddProduct(prod2);

        Order order2 = new Order(cust2);
        order2.AddProduct(prod3);
        order2.AddProduct(prod4);

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}\n");
    }
}
