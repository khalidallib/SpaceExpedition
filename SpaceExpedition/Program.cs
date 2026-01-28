using System;

namespace SpaceExpedition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Galactic Vault..");
            InventoryManager inventory = new InventoryManager();

            string vaultFile = "galactic_vault.txt";
            inventory.LoadFromVault(vaultFile);

            Menu menu = new Menu(inventory);
            menu.Run();
        }
    }
}
