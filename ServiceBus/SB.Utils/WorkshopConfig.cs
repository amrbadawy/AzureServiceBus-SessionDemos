namespace SB.Utils
{
    public static class WorkshopConfig
    {
        public static string BlobsPath = @"..\..\Blobs\";
        public static string Blob01 = "Pub_Sub.png";

        /*
            Endpoint=sb://sbnamespacearm.servicebus.windows.net/;
            SharedAccessKeyName=RootManageSharedAccessKey;
            SharedAccessKey=p1xvGoUqG04qqeCAsLUBhNY4Q3HBjxZ4tt6VaI9vYRU89=;

        */

        public const string Namespace_01_ConnectionString = @"Endpoint=sb://sbnamespacearm.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=p1xvGoUqG04qqeCAsLUBhNY4Q3HBjxZ4tt6VaI9vYRU=";

        public const string SBConnectionString = Namespace_01_ConnectionString; 

        // Allow Duplication
        public const string AzureRiyadh_Queue_01 = "AzureRiyadh_Queue_01";

        // Duplication Detection
        public const string AzureRiyadh_Queue_02 = "AzureRiyadh_Queue_02";

        public const string Session_Queue_01 = "Session_Queue_01";

        // Order Topic
        public static class OrderTopic
        {
            public const string TopicName = "Topic_Orders";
            public const string Sub01 = "OrderSub_01";
            public const string Sub02 = "OrderSub_02";
            public const string Sub03 = "OrderSub_03";
        }

        // Attendee Topic
        public static class AttendeeTopic
        {
            public const string TopicName = "Attendee_Topic";
            public const string All = "AllAttendees";
            public const string Riaydh = Cities.Riyadh;
            public const string VIP = Categories.VIP;
        }
    }
}
