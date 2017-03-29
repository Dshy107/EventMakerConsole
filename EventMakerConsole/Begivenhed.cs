namespace EventMakerConsole
{
    internal class Begivenhed
    {
        public int Event_Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string DateTime { get; set; }

        public override string ToString()
        {
            return string.Format("Event_Id: {0}, Name: {1}, Description: {2}, Place: {3}, Date: {4}", Event_Id, Name, Description, Place, DateTime);
        }
    }
}