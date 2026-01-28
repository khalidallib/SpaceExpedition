using System;
using System.IO;

namespace SpaceExpedition
{
    public static class FileParser
    {
        public static Artifact ParseVaultLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            int first = line.IndexOf(',');
            if (first < 0) return null;

            int second = line.IndexOf(',', first + 1);
            if (second < 0) return null;

            int third = line.IndexOf(',', second + 1);
            if (third < 0) return null;

            int fourth = line.IndexOf(',', third + 1);
            if (fourth < 0) return null;

            string encodedPart = line.Substring(0, first).Trim();
            string planet = line.Substring(first + 1, second - first - 1).Trim();
            string discovery = line.Substring(second + 1, third - second - 1).Trim();
            string storage = line.Substring(third + 1, fourth - third - 1).Trim();
            string description = line.Substring(fourth + 1).Trim();

            encodedPart = encodedPart.Trim('"');
            string decoded = Decoder.DecodeName(encodedPart);

            Artifact artifact = new Artifact();
            artifact.EncodedName = encodedPart;
            artifact.DecodedName = decoded;
            artifact.Planet = planet;
            artifact.DiscoveryDate = discovery;
            artifact.StorageLocation = storage;
            artifact.Description = description;

            return artifact;
        }

        public static Artifact ParseArtifactFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Artifact file not found: " + path);
                return null;
            }

            try
            {
                StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        Artifact artifact = ParseVaultLine(line);
                        if (artifact != null)
                        {
                            reader.Close();
                            return artifact;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading artifact file: " + ex.Message);
            }

            return null;
        }
    }
}

