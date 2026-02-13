using System;
using System.IO;
namespace SpaceExpedition
{
    public class InventoryManager
    {
        private Artifact[] artifacts; private int count;

        public InventoryManager()
        {
            artifacts = new Artifact[10];
            count = 0;
        }

        private void EnsureCapacity()
        {
            if (count < artifacts.Length) return;

            int newSize = artifacts.Length * 2;
            if (newSize <= 0) newSize = 10;

            Artifact[] temp = new Artifact[newSize];
            for (int i = 0; i < count; i++)
                temp[i] = artifacts[i];

            artifacts = temp;
        }

        private void AddInitial(Artifact artifact)
        {
            if (artifact == null) return;

            EnsureCapacity();
            artifacts[count] = artifact;
            count++;
        }

        public void LoadFromVault(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Galactic vault file not found: " + path);
                return;
            }

            try
            {
                StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Artifact artifact = FileParser.ParseVaultLine(line);
                    if (artifact != null)
                        AddInitial(artifact);
                }
                reader.Close();

                SortInventory();
                Console.WriteLine("Loaded " + count + " artifacts from the Galactic Vault.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Galactic Vault: " + ex.Message);
            }
        }

        private void SortInventory()
        {
            for (int i = 1; i < count; i++)
            {
                Artifact current = artifacts[i];
                int j = i - 1;

                while (j >= 0 && String.Compare(artifacts[j].DecodedName, current.DecodedName, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    artifacts[j + 1] = artifacts[j];
                    j--;
                }

                artifacts[j + 1] = current;
            }
        }

        private int BinarySearch(string target)
        {
            int left = 0;
            int right = count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int compare = String.Compare(artifacts[mid].DecodedName, target, StringComparison.OrdinalIgnoreCase);

                if (compare == 0) return mid;

                if (compare < 0)
                    left = mid + 1;
                else right = mid - 1;
            }

            return -(left + 1);
        }

        private void InsertAt(Artifact artifact, int index)
        {
            EnsureCapacity();

            for (int i = count; i > index; i--)
                artifacts[i] = artifacts[i - 1];

            artifacts[index] = artifact;
            count++;
        }

        public void AddArtifactFromFile(string artifactName)
        {
            if (string.IsNullOrWhiteSpace(artifactName))
            {
                Console.WriteLine("Artifact name cannot be empty.");
                return;
            }

            string fileName = artifactName;
            if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                fileName = fileName + ".txt";

            Artifact newArtifact = FileParser.ParseArtifactFile(fileName);
            if (newArtifact == null)
            {
                Console.WriteLine("Could not load artifact from file.");
                return;
            }

            int index = BinarySearch(newArtifact.DecodedName);

            if (index >= 0)
            {
                Console.WriteLine("Artifact already exists in the Galactic Vault:");
                Console.WriteLine(artifacts[index].ToString());
                return;
            }

            int insertIndex = -(index + 1);
            InsertAt(newArtifact, insertIndex);

            Console.WriteLine("New artifact added at position " + insertIndex + ":");
            Console.WriteLine(newArtifact.ToString());
        }

        public void ViewInventory()
        {
            if (count == 0)
            {
                Console.WriteLine("The Galactic Vault is currently empty.");
                return;
            }

            for (int i = 0; i < count; i++)
                Console.WriteLine((i + 1) + ". " + artifacts[i].ToString());
        }

        public void SaveToFile(string path)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, false);
                for (int i = 0; i < count; i++)
                    writer.WriteLine(artifacts[i].ToFileLine());
                writer.Close();

                Console.WriteLine("Expedition summary saved to: " + path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving expedition summary: " + ex.Message);
            }
        }
    }
}

