using System;

namespace SpaceExpedition
{
    public class Menu
    {
        private InventoryManager inventory;

        public Menu(InventoryManager inventoryManager)
        {
            inventory = inventoryManager;
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("== Galacticc Vault Control Panel ==");
                Console.WriteLine("1. Add Artifact");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Save and Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                    AddArtifactOption();
                else if (choice == "2")
                    inventory.ViewInventory();
                else if (choice == "3")
                {
                    SaveAndExit();
                    exit = true;
                }
                else
                    Console.WriteLine("Invalid Choice. Please select 1 , 2 , 3");
            }
        }

        private void AddArtifactOption()
        {
            Console.Write("Enter the artifact name (without extension): ");
            string name = Console.ReadLine();
            inventory.AddArtifactFromFile(name);
        }

        private void SaveAndExit()
        {
            string outputFile = "expedition_summary.txt";
            inventory.SaveToFile(outputFile);
            Console.WriteLine("Exiting program. Safe travels, explorer.");
        }
    }
}
