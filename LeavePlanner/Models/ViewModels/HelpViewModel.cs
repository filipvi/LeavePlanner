namespace LeavePlanner.Models.ViewModels
{
    public class HelpViewModel
    {
        // Naslov modala
        public string Title { get; set; }

        // Poruke za osnovne informacije 
        public string BasicInfoMessagesTitle { get; set; }
        public List<string> BasicInfoMessages { get; set; }

        // Poruke za korake koje je potrebno slijediti 
        public string RequiredStepsMessagesTitle { get; set; }

        public List<string> RequiredStepsMessages { get; set; }
        //public bool RequiredStepsExists { get; set; }

        // Opis tabova / dijelova ekrana 
        public List<TabItem> TabItems { get; set; }

        // Zaključak
        public string ConclusionMessagesTitle { get; set; }

        public List<string> ConclusionMessages { get; set; }
        //public bool ConclusionTabMessagesExists { get; set; }

        public HelpViewModel()
        {
            Title = string.Empty;
            BasicInfoMessagesTitle = string.Empty;
            RequiredStepsMessagesTitle = string.Empty;
            ConclusionMessagesTitle = string.Empty;

            BasicInfoMessages = new List<string>();
            RequiredStepsMessages = new List<string>();
            ConclusionMessages = new List<string>();

            TabItems = new List<TabItem>();
        }

        public class TabItem
        {
            public string MessageTitle { get; set; }
            public List<string> Messages { get; set; }

            public TabItem()
            {
                Messages = new List<string>();
            }
        }
    }
}