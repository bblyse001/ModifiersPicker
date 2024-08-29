using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Step 1: Define the modifiers
        List<Modifier> modifiers = new List<Modifier>
        {
            new Modifier("Light On", "Good", 1),
            new Modifier("El Goblino On Break", "Good", 2),
            new Modifier("More Stuff", "Good", 3),
            new Modifier("Didn't Skip Leg Day", "Good", 4),
            new Modifier("Faster, Faster, Faster", "Good", 4),
            new Modifier("Good Time", "Good", 5),
            new Modifier("Bug Spray", "Good", 6),

            new Modifier("MAXIMUM OVERDRIVE", "Very Good", 4),

            new Modifier("Bad Electrical Work", "Slightly Bad", 1),
            new Modifier("Gone Fishing", "Slightly Bad", 0),
            new Modifier("Soundproofing", "Slightly Bad", 0),
            new Modifier("Tripped", "Slightly Bad", 7),
            new Modifier("Itchy", "Slightly Bad", 6),
            new Modifier("Watch Your Step", "Slightly Bad", 8),
            new Modifier("Injuries", "Slightly Bad", 0),

            new Modifier("Uh Oh", "Bad", 9),
            new Modifier("Electrical Work", "Bad", 1),
            new Modifier("El Goblino Was Here", "Bad", 2),
            new Modifier("Less Stuff", "Bad", 3),
            new Modifier("Wear And Tear", "Bad", 3),
            new Modifier("Wet Floor", "Bad", 0),
            new Modifier("Bad Ventilation", "Bad", 0),
            new Modifier("Locked And Loaded", "Bad", 10),
            new Modifier("Nowhere To Hide", "Bad", 0),
            new Modifier("Tripped And Fell", "Bad", 7),
            new Modifier("Last Few Breaths", "Bad", 7),
            new Modifier("My Knees Are Killing Me", "Bad", 4),
            new Modifier("My Legs Are Killing Me", "Bad", 4),
            new Modifier("Bad Time", "Bad", 5),
            new Modifier("I'm Runnin' Here", "Bad", 0),
            new Modifier("I'm Tip-Toein' Here", "Bad", 0),
            new Modifier("Wrong Number", "Bad", 11),
            new Modifier("Think Fast", "Bad", 12),
            new Modifier("Nosey", "Bad", 13),
            new Modifier("Come Back Here", "Bad", 0),
            new Modifier("It Can Run Too", "Bad", 0),
            new Modifier("Afterimage", "Bad", 0),
            new Modifier("Roundabout", "Bad", 0),
            new Modifier("Are Those Pancakes?!", "Bad", 8),
            new Modifier("Stop Right There", "Bad", 14),

            new Modifier("How Unfortunate", "Very Bad", 9),
            new Modifier("Power Shortage", "Very Bad", 1),
            new Modifier("Out Of Stuff", "Very Bad", 3),
            new Modifier("Key Key Key Key", "Very Bad", 10),
            new Modifier("Last Breath", "Very Bad", 7),
            new Modifier("Really Bad Time", "Very Bad", 5),
            new Modifier("Rush Hour", "Very Bad", 0),
            new Modifier("Battle Of Wits", "Very Bad", 11),
            new Modifier("I'm Everywhere", "Very Bad", 0),
            new Modifier("Think Faster", "Very Bad", 12),
            new Modifier("Always Watching", "Very Bad", 13),
            new Modifier("Seeing Double", "Very Bad", 13),
            new Modifier("Back For Seconds", "Very Bad", 15),
            new Modifier("I Love Pancakes!!!", "Very Bad", 8),

            new Modifier("Chaos, Chaos", "Extremely Bad", 9),
            new Modifier("Light Out", "Extremely Bad", 1),
            new Modifier("El Goblino's Payday", "Extremely Bad", 2),
            new Modifier("Out Of Stuff", "Extremely Bad", 3),
            new Modifier("Jammin'", "Extremely Bad", 0),
            new Modifier("Worst Time Ever", "Extremely Bad", 5),
            new Modifier("Four Eyes", "Extremely Bad", 13),
            new Modifier("Again & Again & Again", "Extremely Bad", 15),
            new Modifier("Rent's Due", "Extremely Bad", 0),
            new Modifier("Room For More", "Extremely Bad", 14)
        };

        // Step 2: Group modifiers by type
        var groupedModifiers = modifiers.GroupBy(m => m.Type).ToDictionary(g => g.Key, g => g.ToList());

        // Step 3: Get user's choice for manual or random selection
        bool randomSelection = AskUserForRandomSelection();

        var selectedModifiers = new List<Modifier>();
        var usedCompatibilityNumbers = new HashSet<int>();
        double totalDifficulty = 0;

        if (randomSelection)
        {
            // Ask if Good/Very Good modifiers are allowed
            bool allowGoodModifiers = AskUserForGoodModifiers();

            selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Extremely Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Very Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Slightly Bad", usedCompatibilityNumbers, ref totalDifficulty));

            if (allowGoodModifiers)
            {
                selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Good", usedCompatibilityNumbers, ref totalDifficulty));
                selectedModifiers.AddRange(GetModifiersRandomly(groupedModifiers, "Very Good", usedCompatibilityNumbers, ref totalDifficulty));
            }
        }
        else
        {
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Extremely Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Very Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Slightly Bad", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Good", usedCompatibilityNumbers, ref totalDifficulty));
            selectedModifiers.AddRange(GetModifiersFromUser(groupedModifiers, "Very Good", usedCompatibilityNumbers, ref totalDifficulty));
        }

        // Step 4: Output the selected modifiers with color
        Console.WriteLine("\nSelected Modifiers:");
        foreach (var modifier in selectedModifiers)
        {
            SetModifierColor(modifier.Type);
            Console.WriteLine($"- {modifier.Name} ({modifier.Type})");
            Console.ResetColor(); // Reset color to default after each line
        }

        // Output the total difficulty
        Console.WriteLine($"\nTotal Difficulty: {totalDifficulty}");

        // Wait for user input to close the program
        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }

    static bool AskUserForRandomSelection()
    {
        Console.Write("Do you want to select the number of modifiers manually? (yes/no): ");
        string input = Console.ReadLine().Trim().ToLower();
        return input == "no";
    }

    static bool AskUserForGoodModifiers()
    {
        Console.Write("Do you want to allow Good/Very Good modifiers? (yes/no): ");
        string input = Console.ReadLine().Trim().ToLower();
        return input == "yes";
    }

    static List<Modifier> GetModifiersFromUser(Dictionary<string, List<Modifier>> groupedModifiers, string type, HashSet<int> usedCompatibilityNumbers, ref double totalDifficulty)
    {
        Console.Write($"How many '{type}' modifiers would you like? ");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count < 0)
        {
            Console.Write("Please enter a valid positive number: ");
        }

        return SelectModifiers(groupedModifiers[type], count, usedCompatibilityNumbers, ref totalDifficulty);
    }

    static List<Modifier> GetModifiersRandomly(Dictionary<string, List<Modifier>> groupedModifiers, string type, HashSet<int> usedCompatibilityNumbers, ref double totalDifficulty)
    {
        Random random = new Random();
        int count = random.Next(0, Math.Min(6, groupedModifiers[type].Count) + 1); // Limit random selection to a max of 6 modifiers
        return SelectModifiers(groupedModifiers[type], count, usedCompatibilityNumbers, ref totalDifficulty);
    }

    static List<Modifier> SelectModifiers(List<Modifier> availableModifiers, int count, HashSet<int> usedCompatibilityNumbers, ref double totalDifficulty)
    {
        var selectedModifiers = new List<Modifier>();
        Random random = new Random();

        while (selectedModifiers.Count < count && availableModifiers.Count > 0)
        {
            int index = random.Next(availableModifiers.Count);
            Modifier chosenModifier = availableModifiers[index];

            if (chosenModifier.CompatibilityNumber == 0 || !usedCompatibilityNumbers.Contains(chosenModifier.CompatibilityNumber))
            {
                selectedModifiers.Add(chosenModifier);
                if (chosenModifier.CompatibilityNumber != 0)
                {
                    usedCompatibilityNumbers.Add(chosenModifier.CompatibilityNumber);
                }

                totalDifficulty += GetDifficultyValue(chosenModifier.Type);
            }

            availableModifiers.RemoveAt(index); // Remove the chosen modifier to avoid duplicates
        }

        return selectedModifiers;
    }

    static double GetDifficultyValue(string type)
    {
        switch (type)
        {
            case "Good": return -1;
            case "Very Good": return -3;
            case "Slightly Bad": return 0.5;
            case "Bad": return 1;
            case "Very Bad": return 1.5;
            case "Extremely Bad": return 2;
            default: return 0;
        }
    }

    static void SetModifierColor(string type)
    {
        switch (type)
        {
            case "Good":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "Very Good":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "Slightly Bad":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "Bad":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case "Very Bad":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "Extremely Bad":
                Console.ForegroundColor = ConsoleColor.DarkRed;
                break;
            default:
                Console.ResetColor();
                break;
        }
    }

    class Modifier
    {
        public string Name { get; }
        public string Type { get; }
        public int CompatibilityNumber { get; }

        public Modifier(string name, string type, int compatibilityNumber)
        {
            Name = name;
            Type = type;
            CompatibilityNumber = compatibilityNumber;
        }
    }
}
