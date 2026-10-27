using System;
using System.Collections.Generic;
using System.Globalization;

// Parent class
class Vehicle
{
    public int VehicleId { get; set; }
    public string Brand { get; set; }
    public decimal DailyRate { get; set; }

    public Vehicle(int vehicleId, string brand, decimal dailyRate)
    {
        VehicleId = vehicleId;
        Brand = brand;
        DailyRate = dailyRate;
    }

    public virtual decimal CalculateRentalCost(int days)
    {
        return DailyRate * days;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Vehicle ID: {VehicleId}, Brand: {Brand}");
    }
}

// Child class: Car
class Car : Vehicle
{
    public int NumSeats { get; set; }

    public Car(int vehicleId, string brand, decimal dailyRate, int numSeats)
        : base(vehicleId, brand, dailyRate)
    {
        NumSeats = numSeats;
    }

    public override decimal CalculateRentalCost(int days)
    {
        decimal baseCost = base.CalculateRentalCost(days);
        return baseCost * 1.10m; // 10% insurance surcharge
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(
            $"Car [ID={VehicleId}, Brand={Brand}, Seats={NumSeats}]");
    }
}

// Child class: Motorcycle
class Motorcycle : Vehicle
{
    public int EngineCapacity { get; set; }

    public Motorcycle(int vehicleId, string brand, decimal dailyRate, int engineCapacity)
        : base(vehicleId, brand, dailyRate)
    {
        EngineCapacity = engineCapacity;
    }

    public override decimal CalculateRentalCost(int days)
    {
        decimal baseCost = base.CalculateRentalCost(days);
        return baseCost * 0.95m; // 5% discount
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(
            $"Motorcycle [ID={VehicleId}, Brand={Brand}, CC={EngineCapacity}]");
    }
}

// Customer class
class Customer
{
    public string Name { get; set; }
    public string LicenseNumber { get; set; }

    private Vehicle rentedVehicle;

    public Customer(string name, string licenseNumber)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        rentedVehicle = null;
    }

    public void RentVehicle(Vehicle vehicle)
    {
        rentedVehicle = vehicle;
    }

    public void ReturnVehicle()
    {
        rentedVehicle = null;
    }

    public void PrintRentalSummary(int days)
    {
        Console.WriteLine("\n=== Customer Rental Summary ===");
        Console.WriteLine($"Customer: {Name} (License: {LicenseNumber})");

        if (rentedVehicle != null)
        {
            Console.WriteLine(
                $"Rented: {rentedVehicle.Brand} {rentedVehicle.GetType().Name}");

            Console.WriteLine(
                $"Total Cost for {days} days: $" +
                rentedVehicle.CalculateRentalCost(days)
                    .ToString("F2", CultureInfo.InvariantCulture));
        }
        else
        {
            Console.WriteLine("No vehicle currently rented.");
        }
    }
}

// Main program
class Program
{
    static void Main()
    {
        Car car = new Car(101, "Toyota", 100.00m, 5);
        Motorcycle motorcycle = new Motorcycle(102, "Yamaha", 50.00m, 150);

        // Polymorphism: different vehicle types in one list
        List<Vehicle> vehicles = new List<Vehicle>
        {
            car,
            motorcycle
        };

        Console.WriteLine("=== Vehicle Rental Costs ===");

        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.DisplayInfo();

            Console.WriteLine(
                $" -> Cost for 3 days: $" +
                vehicle.CalculateRentalCost(3)
                    .ToString("F2", CultureInfo.InvariantCulture));
        }

        // Composition: customer has a rented vehicle
        Customer customer = new Customer("Alice", "AB1234");
        customer.RentVehicle(car);

        customer.PrintRentalSummary(5);
    }
}