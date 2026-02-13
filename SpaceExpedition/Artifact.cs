namespace SpaceExpedition
{
    public class Artifact
    {
        public string EncodedName;
        public string DecodedName;
        public string Planet;
        public string DiscoveryDate;
        public string StorageLocation;
        public string Description;

        public Artifact()
        {
            EncodedName = string.Empty;
            DecodedName = string.Empty;
            Planet = string.Empty;
            DiscoveryDate = string.Empty;
            StorageLocation = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return "Decoded Name: " + DecodedName + " | Encoded Name: " + EncodedName + " | Planet: " + Planet + " | Discovery Date: " + DiscoveryDate + " | Storage Location: " + StorageLocation + " | Description: " + Description;
        }

        public string ToFileLine()
        {
            return EncodedName + "," + Planet + "," + DiscoveryDate + "," + StorageLocation + "," + Description;
        }
    }
}
