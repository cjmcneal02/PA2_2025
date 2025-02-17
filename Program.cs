using System; // Importing the System namespace for basic functionalities
using System.IO; // Importing the System.IO namespace for file operations

namespace PA2 // Declaring the namespace PA2
{
    class Program // Declaring the Program class
    //extras: 
    // prompt to continue after invalid input
    // added logging to the main menu choice and currency choice
    {
        static void Main(string[] args) // Main method, the entry point of the program
        {
            string userChoice = GetMenuChoice(); // Get the initial menu choice from the user
            while (userChoice != "3") // Loop until the user chooses to exit (option "3")
            {
                RoutEm(userChoice); // Route the user choice to the appropriate method
                userChoice = GetMenuChoice(); // Get the next menu choice from the user
            }
            RoutEm(userChoice); // Handle the exit choice
        }

        static string GetMenuChoice() // Method to display the main menu and get the user's choice
        {
            // Display the main menu options
            Console.WriteLine("Main Menu\n choose an option\n 1.Convert Currency \n 2. View Shipping Invoice \n 3. Exit");
            string userChoice = Console.ReadLine(); // Read the user's choice
            LogAction($"User selected menu option: {userChoice}");
            return userChoice; // Return the user's choice
        }

        static void RoutEm(string userChoice) // Method to route the user's choice to the appropriate method
        {
            switch (userChoice) // Switch statement to handle different user choices
            {
                case "1": // If the user chooses option "1"
                    ConvertCurrency(); // Call the ConvertCurrency method
                    break;
                case "2": // If the user chooses option "2"
                    ViewShippingInvoice(); // Call the ViewShippingInvoice method
                    break;
                case "3": // If the user chooses option "3"
                    Console.WriteLine("Goodbye! Exiting Program..."); // Print exit message
                    PromptToContinue(); // Prompt the user to continue
                    break;
                default: // If the user enters an invalid option
                    Console.WriteLine("Invalid option"); // Print invalid option message
                    PromptToContinue(); // Prompt the user to continue
                    break;
            }
        }

        static void ConvertCurrency() // Method to handle currency conversion
        {
            string toCurrency = GetCurrencyChoice(); // Get the target currency choice from the user
            Console.WriteLine("Enter the amount in USD to convert:"); // Prompt the user to enter the amount in USD
            if (double.TryParse(Console.ReadLine(), out double amount)) // Try to parse the entered amount to a double
            {
                double convertedAmount = ConvertAmount(toCurrency, amount); // Convert the amount to the target currency
                string currencyName = GetCurrencyName(toCurrency); // Get the name of the target currency
                Console.WriteLine($"Converted amount: {convertedAmount} {currencyName}"); // Print the converted amount and currency name
                LogAction($"Converted {amount} USD to {convertedAmount} {currencyName}");
            }
            else // If the entered amount is invalid
            {
                Console.WriteLine("Invalid amount entered. Please enter a valid number."); // Print invalid amount message
                PromptToContinue(); // Prompt the user to continue
            }
        }

        static string GetCurrencyChoice() // Method to get the target currency choice from the user
        {
            // Display the currency options
            Console.WriteLine("Choose a currency to convert to\n 1. Japanese Yen \n 2. Chinese Yuan \n 3. British Pound \n 4. Doubloons \n 5. Algerian Dinar \n 6. Back to Main Menu");
            string currencyChoice = Console.ReadLine(); // Read the user's currency choice
            LogAction($"User selected currency option: {currencyChoice}");
            return currencyChoice; // Return the user's currency choice
        }

        static double ConvertAmount(string toCurrency, double amount) // Method to convert the amount to the target currency
        {
            double toRate = GetCurrencyRate(toCurrency); // Get the conversion rate for the target currency
            return amount * toRate; // Return the converted amount
        }

        static double GetCurrencyRate(string currency) // Method to get the conversion rate for the target currency
        {
            switch (currency) // Switch statement to handle different currency choices
            {
                case "1": return 0.0064; // Japanese Yen
                case "2": return 0.14; // Chinese Yuan
                case "3": return 1.22; // British Pound
                case "4": return 8.40; // Doubloons
                case "5": return 0.0074; // Algerian Dinar
                default: return 1.0; // Default to USD
            }
        }

        static string GetCurrencyName(string currency) // Method to get the name of the target currency
        {
            switch (currency) // Switch statement to handle different currency choices
            {
                case "1": return "Japanese Yen"; // Japanese Yen
                case "2": return "Chinese Yuan"; // Chinese Yuan
                case "3": return "British Pounds"; // British Pound
                case "4": return "Doubloons"; // Doubloons
                case "5": return "Algerian Dinar"; // Algerian Dinar
                default: return "USD"; // Default to USD
            }
        }

        static void ViewShippingInvoice() // Method to handle viewing the shipping invoice
        {
            Console.WriteLine("Enter the weight of the item or container in tons:"); // Prompt the user to enter the weight
            if (double.TryParse(Console.ReadLine(), out double weight)) // Try to parse the entered weight to a double
            {
                double basePrice = weight * 220.40; // Calculate the base price
                Console.WriteLine("Does the parcel contain perishable items? (yes/no):"); // Ask if the parcel contains perishable items
                string perishable = Console.ReadLine().ToLower(); // Read the user's input and convert to lowercase
                if (perishable == "yes")
                {
                    basePrice += weight * 230; // Add $230 per ton for perishable items
                }

                Console.WriteLine("Is this an express shipment? (yes/no):"); // Ask if the shipment is express
                string express = Console.ReadLine().ToLower(); // Read the user's input and convert to lowercase
                if (express == "yes")
                {
                    basePrice *= 1.25; // Add 25% increase for express shipping
                }

                Console.WriteLine($"The total shipping cost is: ${basePrice}"); // Print the total shipping t

                Console.WriteLine("Enter the amount paid:"); // Prompt the user to enter the amount paid
                if (double.TryParse(Console.ReadLine(), out double amountPaid)) // Try to parse the entered amount to a double
                {
                    if (amountPaid < basePrice) // If the amount paid is less than the amount owed
                    {
                        Console.WriteLine("Error: Amount paid is less than the amount owed."); // Print error message
                        PromptToContinue(); // Prompt the user to continue
                    }
                    else
                    {
                        double changeDue = amountPaid - basePrice; // Calculate the change due
                        Console.WriteLine($"Change due: ${changeDue}"); // Print the change due
                        LogAction($"Shipping invoice: weight={weight}, basePrice={basePrice}, amountPaid={amountPaid}, changeDue={changeDue}");
                    }
                }
                else // If the entered amount is invalid
                {
                    Console.WriteLine("Invalid amount entered. Please enter a valid number."); // Print invalid amount message
                    PromptToContinue(); // Prompt the user to continue
                }
            }
            else // If the entered weight is invalid
            {
                Console.WriteLine("Invalid weight entered. Please enter a valid number."); // Print invalid weight message
                PromptToContinue(); // Prompt the user to continue
            }
        }

        static void PromptToContinue() // Method to prompt the user to continue
        {
            Console.WriteLine("Press any key to continue..."); // Prompt the user to press any key to continue
            Console.ReadKey(); // Wait for the user to press any key
            Console.Clear(); // Clear the console screen
        }

        static void LogAction(string message) // Method to log actions to a file
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}"); // Write the message to the log file with the current timestamp
            }
        }
    }
}