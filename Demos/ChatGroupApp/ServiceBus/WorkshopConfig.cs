using System.Windows.Forms;

namespace ChatGroupApp
{
    public static class WorkshopConfig
    {
        public const string Namespace_01_ConnectionString = @"Endpoint=sb://sbnamespacearm.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=p1xvGoUqG04qqeCAsLUBhNY4Q3HBjxZ4tt6VaI9vYRU=";

        public const string SBConnectionString = Namespace_01_ConnectionString;


        public static ListBox ChatArea { get; set; }

        // Order Topic
        public static class ChatGroup
        {
            public const string Name = "ChatHubTopic";
        }
    }
}
