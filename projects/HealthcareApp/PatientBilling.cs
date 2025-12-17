using System;
using System.Collections.Generic;

class PatientBilling
{
    static void Main()
    {
        // Step 1: Define available services and their costs
        Dictionary<int, (string Name, int Cost)> availableServices = new Dictionary<int, (string, int)>()
        {
            { 1, ("Lab Test", 600) },
            { 2, ("X-Ray", 300) },
            { 3, ("ECG", 1500) },
            { 4, ("MRI", 2500) },
            { 5, ("Ultrasound", 1200) },
            { 6, ("Blood Work", 400) }
        };

        // Step 2: Set base consultation fee
        int baseConsultationFee = 500;

        // Step 3: Display available services
        Console.WriteLine("Available Services:");
        foreach (var service in availableServices)
        {
            Console.WriteLine($"{service.Key}. {service.Value.Name} - {service.Value.Cost}");
        }

        // Step 4: Prompt user to select services by number
        Console.WriteLine("\nEnter the numbers of optional services used (comma-separated):");
        string servicesInput = Console.ReadLine();
        List<string> selectedServiceNumbers = new List<string>(servicesInput.Split(','));

        // Step 5: Read insurance coverage percentage
        Console.Write("Enter insurance coverage percentage (e.g., 20 for 20%): ");
        string insuranceInput = Console.ReadLine();
        double insurancePercent = 0;
        if (!double.TryParse(insuranceInput, out insurancePercent) || insurancePercent < 0 || insurancePercent > 100)
        {
            Console.WriteLine("Invalid input. Setting insurance coverage to 0%.");
            insurancePercent = 0;
        }

        // Step 6: Read admit days
        const int admitFeePerDay = 2000;
        Console.Write("Enter number of admit days (0 if not admitted): ");
        string admitInput = Console.ReadLine();
        int admitDays = 0;
        if (!int.TryParse(admitInput, out admitDays) || admitDays < 0)
        {
            Console.WriteLine("Invalid input. Setting admit days to 0.");
            admitDays = 0;
        }

        // Step 7: Initialize billing variables
        int optionalServicesAmount = 0;
        int totalAmount = baseConsultationFee;
        int discountAmount = 0;
        int insuranceAmount = 0;
        int finalAmount = 0;
        int admitAmount = admitDays * admitFeePerDay;

        // Step 8: Calculate optional services total
        foreach (string number in selectedServiceNumbers)
        {
            if (int.TryParse(number.Trim(), out int serviceNumber) && availableServices.ContainsKey(serviceNumber))
            {
                optionalServicesAmount += availableServices[serviceNumber].Cost;
            }
            else
            {
                Console.WriteLine($"Service number '{number.Trim()}' not recognized. Skipping.");
            }
        }

        totalAmount += optionalServicesAmount + admitAmount;

        // Step 9: Apply discount if total exceeds ₹2000
        if (totalAmount > 2000)
        {
            discountAmount = (int)(totalAmount * 0.05); // 5% discount
        }

        int discountedAmount = totalAmount - discountAmount;

        // Step 10: Apply insurance coverage
        insuranceAmount = (int)(discountedAmount * (insurancePercent / 100.0));
        int afterInsurance = discountedAmount - insuranceAmount;

        // Step 11: Apply tax (8%)
        int taxAmount = (int)(afterInsurance * 0.08);
        finalAmount = afterInsurance + taxAmount;

        // Step 12: Output results
        Console.WriteLine("\n--- Billing Summary ---");
        Console.WriteLine($"Consultation Fee: {baseConsultationFee}");
        Console.WriteLine($"Optional Services Total: {optionalServicesAmount}");
        Console.WriteLine($"Admit Fee ({admitDays} days @ {admitFeePerDay}/day): {admitAmount}");
        Console.WriteLine($"Total Amount (before discount): {totalAmount}");
        Console.WriteLine($"Discount Applied: {discountAmount}");
        Console.WriteLine($"Insurance Deduction ({insurancePercent}%): {insuranceAmount}");
        Console.WriteLine($"Amount after Discount & Insurance: {afterInsurance}");
        Console.WriteLine($"Tax (8%): {taxAmount}");
        Console.WriteLine($"Final Amount Payable by Patient: {finalAmount}");
    }
}
